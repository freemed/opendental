using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness
{
	///<summary></summary>
	public class X837_5010:X12object{

		public X837_5010(string messageText):base(messageText){
		
		}
		
		public static void GenerateMessageText(StreamWriter sw,Clearinghouse clearhouse,int batchNum,List<ClaimSendQueueItem> listQueueItems,EnumClaimMedType medType) {
			//Interchange Control Header (Interchange number tracked separately from transactionNum)
			//We set it to between 1 and 999 for simplicity
			sw.Write("ISA*00*"//ISA01 2/2 Authorization Information Qualifier: 00=No Authorization Information Present (No meaningful information in ISA02).
				+"          *"//ISA02 10/10 Authorization Information: Blank
				+"00*"//ISA03 2/2 Security Information Qualifier: 00=No Security Information Present (No meaningful information in ISA04).
				+"          *"//ISA04 10/10 Security Information: Blank
				+clearhouse.ISA05+"*"//ISA05 2/2 Interchange ID Qualifier: ZZ=mutually defined. 30=TIN. Validated
				+X12Generator.GetISA06(clearhouse)+"*"//ISA06 15/15 Interchange Sender ID: Sender ID(TIN) Or might be TIN of Open Dental.
				+clearhouse.ISA07+"*"//ISA07 2/2 Interchange ID Qualifier: ZZ=mutually defined. 30=TIN. Validated
				+Sout(clearhouse.ISA08,15,15)+"*"//ISA08 15/15 Interchange Receiver ID: Validated to make sure length is at least 2.
				+DateTime.Today.ToString("yyMMdd")+"*"//ISA09 6/6 Interchange Date: today's date.
				+DateTime.Now.ToString("HHmm")+"*"//ISA10 4/4 Interchange Time: current time
				+"U*"//ISA11 1/1 Repetition Separator:
				+"00501*"//ISA12 5/5 Interchange Control Version Number
				+batchNum.ToString().PadLeft(9,'0')+"*"//ISA13 9/9 Interchange Control Number:
				+"0*"//ISA14 1/1 Acknowledgement Requested: 0=No Interchange Acknowledgment Requested.
				+clearhouse.ISA15+"*");//ISA15 1/1 Interchange Usage Indicator: T=Test P=Production. Validated.
			sw.WriteLine(":~");//ISA16 1/1 Component Element Separator: Use colon.
			//Just one functional group.
			WriteFunctionalGroup(sw,listQueueItems,batchNum,clearhouse,medType);
			//Interchange Control Trailer
			sw.WriteLine("IEA*1*"//IEA01 1/5 Number of Included Functional Groups:
					+batchNum.ToString().PadLeft(9,'0')+"~");//IEA02 9/9 Interchange Control Number:
		}

		private static void WriteFunctionalGroup(StreamWriter sw,List<ClaimSendQueueItem> queueItems,int batchNum,Clearinghouse clearhouse,EnumClaimMedType medType) {
			#region Functional Group Header
			int transactionNum=1;//Gets incremented for each carrier. Can be reused in other functional groups and interchanges, so not persisted
			//Functional Group Header
			string groupControlNumber=batchNum.ToString();//Must be unique within file.  We will use batchNum
			if(medType==EnumClaimMedType.Medical) {
				//groupControlNumber="2";//this works for now because only two groups
				sw.WriteLine("GS*HC*"//GS01: Health Care Claim
					+X12Generator.GetGS02(clearhouse)+"*"//GS02: Senders Code. Sometimes OpenDental.  Sometimes the sending clinic. Validated
					+Sout(clearhouse.GS03,15,2)+"*"//GS03: Application Receiver's Code
					+DateTime.Today.ToString("yyyyMMdd")+"*"//GS04: today's date
					+DateTime.Now.ToString("HHmm")+"*"//GS05: current time
					+groupControlNumber+"*"//GS06: Group control number. Max length 9. No padding necessary. 
					+"X*"//GS07: X
					+"005010X222~");//GS08: Version
			}
			else if(medType==EnumClaimMedType.Institutional) {
				sw.WriteLine("GS*HC*"//GS01 2/2 Functional Identifier Code: Health Care Claim.
					+X12Generator.GetGS02(clearhouse)+"*"//GS02 2/15 Application Sender's Code: Sometimes Jordan Sparks.  Sometimes the sending clinic.
					+Sout(clearhouse.GS03,15,2)+"*"//GS03 2/15 Application Receiver's Code:
					+DateTime.Today.ToString("yyyyMMdd")+"*"//GS04 8/8 Date: today's date.
					+DateTime.Now.ToString("HHmm")+"*"//GS05 4/8 TIME: current time.
					+groupControlNumber+"*"//GS06 1/9 Group Control Number: No padding necessary.
					+"X*"//GS07 1/2 Responsible Agency Code: X=Accredited Standards Committee X12.
					+"005010X223A2~");//GS08 1/12 Version/Release/Industry Identifier Code:
			}
			else if(medType==EnumClaimMedType.Dental) {
				//groupControlNumber="1";
				sw.WriteLine("GS*HC*"//GS01 2/2 Functional Identifier Code: Health Care Claim.
					+X12Generator.GetGS02(clearhouse)+"*"//GS02 2/15 Application Sender's Code: Sometimes Jordan Sparks.  Sometimes the sending clinic.
					+Sout(clearhouse.GS03,15,2)+"*"//GS03 2/15 Application Receiver's Code:
					+DateTime.Today.ToString("yyyyMMdd")+"*"//GS04 8/8 Date: today's date.
					+DateTime.Now.ToString("HHmm")+"*"//GS05 4/8 TIME: current time.
					+groupControlNumber+"*"//GS06 1/9 Group Control Number: No padding necessary.
					+"X*"//GS07 1/2 Responsible Agency Code: X=Accredited Standards Committee X12.
					+"005010X224A2~");//GS08 1/12 Version/Release/Industry Identifier Code:
			}
			#endregion Functional Group Header
			#region Define Variables
			int HLcount=1;
			int parentProv=0;//the HL sequence # of the current provider.
			int parentSubsc=0;//the HL sequence # of the current subscriber.
			string hasSubord="";//0 if no subordinate, 1 if at least one subordinate
			Claim claim;
			InsPlan insPlan;
			InsPlan otherPlan=new InsPlan();
			InsSub sub;
			InsSub otherSub=new InsSub();
			Patient patient;
			Patient subscriber;
			Patient otherSubsc=new Patient();
			Carrier carrier;
			Carrier otherCarrier=new Carrier();
			List<ClaimProc> claimProcList;//all claimProcs for a patient.
			List<ClaimProc> claimProcs;
			List<Procedure> procList;
			List<ToothInitial> initialList;
			List<PatPlan> patPlans;
			Procedure proc;
			ProcedureCode procCode;
			Provider provTreat;//might be different for each proc
			Provider billProv=null;
			Clinic clinic=null;
			bool isSecondaryPreauth=false;
			int seg=0;//segments for a particular ST-SE transaction
			#endregion Define Variables
			#region Transaction Set Header
			//if(i==0//if this is the first claim
			//	|| claimItems[i].PayorId0 != claimItems[i-1].PayorId0)//or the payorID has changed
			//{
			//	newTrans=true;
			//	seg=0;
			//}
			//else newTrans=false;
			//if(newTrans) {
			//Transaction Set Header (one for each carrier)
			//transactionNum gets incremented in SE section
			//ST02 Transact. control #. Must be unique within ISA
			//NO: So we used combination of transaction and group, eg 00011
			seg++;
			if(medType==EnumClaimMedType.Medical) {
				sw.WriteLine("ST*837*"//ST01
					+transactionNum.ToString().PadLeft(4,'0')+"*"//ST02 4/9
					+"005010X222~");//ST03: Implementation convention reference
			}
			else if(medType==EnumClaimMedType.Institutional) {
				sw.WriteLine("ST*837*"//ST01 3/3 Transaction Set Identifier Code: 
					+transactionNum.ToString().PadLeft(4,'0')+"*"//ST02 4/9 Transaction Set Control Number: 
					+"005010X223A2~");//ST03 1/35 Implementation Convention Reference
			}
			else if(medType==EnumClaimMedType.Dental) {
				sw.WriteLine("ST*837*"//ST01 3/3 Transaction Set Identifier Code: 
					+transactionNum.ToString().PadLeft(4,'0')+"*"//ST02 4/9 Transaction Set Control Number: 
					+"005010X224A2~");//ST03 1/35 Implementation Convention Reference
			}
			seg++;
			sw.WriteLine("BHT*0019*"//BHT01 4/4 Hierarchical Structure Code: 0019=Information Source, Subscriber, Dependant.
				+"00*"//BHT02 2/2 Transaction Set Purpose Code: 00=Original transmissions are transmissions which have never been sent to the reciever.
				+transactionNum.ToString().PadLeft(4,'0')+"*"//BHT03 1/50 Reference Identification: Can be same as ST02.
				+DateTime.Now.ToString("yyyyMMdd")+"*"//BHT04 8/8 Date: 
				+DateTime.Now.ToString("HHmmss")+"*"//BHT05 4/8 Time: 
				+"CH~");//BHT06 2/2 Transaction Type Code: CH=Chargable.
			//1000A Submitter is OPEN DENTAL and sometimes it's the practice
			//(depends on clearinghouse and Partnership agreements)
			//See 2010AA PER (after REF) for the new billing provider contact phone number
			//1000A NM1: required
			seg++;
			Write1000A_NM1(sw,clearhouse);
			//1000A PER: required. Contact number.
			seg++;//always one seg
			Write1000A_PER(sw,clearhouse);
			//1000B Receiver is always the Clearinghouse
			//1000B NM1: required
			seg++;
			sw.WriteLine("NM1*40*"//NM101 2/3 Entity Identifier Code: 40=Receiver.
				+"2*"//NM102 1/1 Entity Type Qualifier: 2=Non-Person Entity.
				+Sout(clearhouse.Description,35,1)+"*"//NM103 1/60 Name Last or Organization Name: Receiver Name.
				+"*"//NM104 1/35 Name First: Not Used.
				+"*"//NM105 1/25 Name Middle: Not Used.
				+"*"//NM106 1/10 Name Prefix: Not Used.
				+"*"//NM107 1/10 Name Suffix: Not Used.
				+"46*"//NM108 1/2 Identification Code: 46=ETIN.
				+Sout(clearhouse.ISA08,80,2)+"~");//NM109 2/80 Identification Code: Receiver ID Code. aka ETIN#.
				//NM110 through NM112 not used so not specified.
			HLcount=1;
			parentProv=0;//the HL sequence # of the current provider.
			parentSubsc=0;//the HL sequence # of the current subscriber.
			hasSubord="";//0 if no subordinate, 1 if at least one subordinate
			//}
			#endregion
			for(int i=0;i<queueItems.Count;i++) {
				#region Initialize Variables
				claim=Claims.GetClaim(queueItems[i].ClaimNum);
				insPlan=InsPlans.GetPlan(claim.PlanNum,new List<InsPlan>());
				sub=InsSubs.GetSub(claim.InsSubNum,null);
				//insPlan could be null if db corruption. No error checking for that
				if(claim.PlanNum2>0) {
					otherPlan=InsPlans.GetPlan(claim.PlanNum2,new List<InsPlan>());
					otherSub=InsSubs.GetSub(claim.InsSubNum2,null);
					otherSubsc=Patients.GetPat(otherSub.Subscriber);
					otherCarrier=Carriers.GetCarrier(otherPlan.CarrierNum);
				}
				patient=Patients.GetPat(claim.PatNum);
				subscriber=Patients.GetPat(sub.Subscriber);
				carrier=Carriers.GetCarrier(insPlan.CarrierNum);
				claimProcList=ClaimProcs.Refresh(patient.PatNum);
				claimProcs=ClaimProcs.GetForSendClaim(claimProcList,claim.ClaimNum);
				procList=Procedures.Refresh(claim.PatNum);
				initialList=ToothInitials.Refresh(claim.PatNum);
				patPlans=PatPlans.Refresh(patient.PatNum);
				#endregion Initialize Variables
				#region Billing Provider
				//Billing address is based on clinic, not provider.  All claims in a batch are guaranteed to be from a single clinic.  That validation is done in FormClaimSend.
				//Although, now that we have a separate loop for each claim, we might be able to allow a batch with mixed clinics.
				if(!PrefC.GetBool(PrefName.EasyNoClinics)) {//if using clinics
					clinic=Clinics.GetClinic(claim.ClinicNum);//might be null
				}
				//2000A HL: Billing provider HL loop
				seg++;
				sw.WriteLine("HL*"+HLcount.ToString()+"*"//HL01 1/12 Heirarchical ID Number: 
					+"*"//HL02 Hierarchical Parent ID Number: Not used.
					+"20*"//HL03 1/2 Heirarchical Level Code: 20=Information Source.
					+"1~");//HL04 1/1 Heirarchical Child Code. 1=Additional Subordinate HL Data Segment in This Hierarchical Structure.
				//billProv=ProviderC.ListLong[Providers.GetIndexLong(claimItems[i].ProvBill1)];
				billProv=Providers.GetProv(claim.ProvBill);
				//2000A PRV: Provider Specialty Information 
				//used instead of 2310B. js 9/5/11 this comment is confusing.
				seg++;
				sw.WriteLine("PRV*BI*"//PRV01 1/3 Provider Code: BI=Billing.
					+"PXC*"//PRV02 2/3 Reference Identification Qualifier: PXC=Health Care Provider Taxonomy Code.
					+X12Generator.GetTaxonomy(billProv)+"~");//PRV03 1/50 Provider Taxonomy Code: 
				//PRV04 through PRV06 are not used.
				//2000A CUR: Foreign Currency Information. Situational. We do not need to specify because united states dollars are default.
				//Loop 2010 includes loops 2010AA, 2010AB, and 2010AC.  We only include 2010AA-------------------------------------------
				//2010AA NM1: Billing provider
				seg++;
				sw.Write("NM1*85*");//NM101 2/3 Entity Identifier Code: 85=Billing Provider.
//TODO: 9/5/11 We need a provider.IsNotPerson field.  So default is 0/false.  The exception is true and can be used for certain dummy billing providers.  Will be used here.
				if(billProv.FName=="") {
					sw.Write("2*");//NM102 1/1 Entity Type Qualifier: 1=Person, 2=Non-Person Entity.
				}
				else {
					sw.Write("1*");
				}
				sw.Write(Sout(billProv.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
					+Sout(billProv.FName,35)+"*"//NM104 1/35 Name First: Situational. Required when NM102=1. Might be blank.
					+Sout(billProv.MI,25)+"*"//NM105 1/25 Name Middle: Since this is situational there is no minimum length.
					+"*"//NM106 1/10 Name Prefix: Not Used.
					+"*");//NM107 1/10 Name Suffix: Not Used.
				sw.Write("XX*");//NM108 1/2 Identification Code Qualifier: 24=EIN, 34=SSN, XX=NPI. It's after the NPI date now, so only one choice here.
				sw.WriteLine(Sout(billProv.NationalProvID,80)+"~");//NM109 2/80 Identification Code: NPI Validated.
				//NM110 through NM112 Not Used.
				//2010AA N3: Billing provider address
				seg++;
				string address1="";
				if(PrefC.GetBool(PrefName.UseBillingAddressOnClaims)) {
					address1=PrefC.GetString(PrefName.PracticeBillingAddress);
				}
				else if(clinic==null) {
					address1=PrefC.GetString(PrefName.PracticeAddress);
				}
				else {
					address1=clinic.Address;
				}
				sw.Write("N3*"+Sout(address1,55));//N301 1/55 Address Information: 
				string address2="";
				if(PrefC.GetBool(PrefName.UseBillingAddressOnClaims)) {
					address2=PrefC.GetString(PrefName.PracticeBillingAddress2);
				}
				else if(clinic==null) {
					address2=PrefC.GetString(PrefName.PracticeAddress2);
				}
				else {
					address2=clinic.Address2;
				}
				if(address2=="") {
					sw.WriteLine("~");
				}
				else {
					sw.WriteLine("*"+Sout(address2,55)+"~");//N302 1/55 Address Information: Situational. Only specify if there is a second address line.
				}
				//2010AA N4: Billing Provider City,State,Zip Code.
				seg++;
				string city="";
				string state="";
				string zip="";
				if(PrefC.GetBool(PrefName.UseBillingAddressOnClaims)) {
					city=PrefC.GetString(PrefName.PracticeBillingCity);
					state=PrefC.GetString(PrefName.PracticeBillingST);
					zip=PrefC.GetString(PrefName.PracticeBillingZip);
				}
				else if(clinic==null) {
					city=PrefC.GetString(PrefName.PracticeCity);
					state=PrefC.GetString(PrefName.PracticeST);
					zip=PrefC.GetString(PrefName.PracticeZip);
				}
				else {
					city=clinic.City;
					state=clinic.State;
					zip=clinic.Zip;
				}
				sw.WriteLine("N4*"+Sout(city,30)+"*"//N401 2/30 City Name: 
						+Sout(state,2)+"*"//N402 2/2 State or Province Code: 
						+Sout(zip.Replace("-",""),15)//N403 3/15 Postal Code: 
						+"~");//NM404 through NM407 are either situational with United States as default, or not used, so we don't specify any of them.
				//2010AA REF - Billing Provider Tax Identification
				seg++;
				bool isSSN=false;
				if(medType==EnumClaimMedType.Dental) {//and probably medical
					//SSN is allowed instead of TIN
					if(!billProv.UsingTIN){
						isSSN=true;
					}
				}
				sw.Write("REF*");
				if(isSSN){
					sw.Write("SY*");//REF01: qualifier. SY=SSN
				}
				else{
					sw.Write("EI*");//REF01: qualifier. EI=EIN
				}
				sw.WriteLine(Sout(billProv.SSN,30)+"~");//REF02 1/50 Tax ID #.  Must be a string of exactly 9 digits
				if(medType==EnumClaimMedType.Dental) {//and medical?
					//2010AA REF - Billing Provider UIPN/License Information, max repeat 2
					//2010AA REF: License #. Required by RECS clearinghouse,
					//but everyone else should find it useful too.
					//We do NOT validate that it's entered because seding it with non-persons causes problems
					if(billProv.StateLicense!=""){
						seg++;
						sw.WriteLine("REF*0B*"//REF01 2/3 Reference Identification Qualifier: 0B=State License Number.
							+Sout(billProv.StateLicense,50)//REF02 1/50 Reference Identification: 
							+"~");//REF03 and REF04 are not used.
					}
					//2010AA REF: Secondary ID number(s). Only required by some carriers.
					//seg+=WriteProv_REF(sw,billProv,claimItems[i].PayorId0);
					//js 9/5/11 secondary ID numbers are no longer allowed now.  I wonder what BCBS will do now.
				}
				//2010AA PER - Billing Provider Contact Information
				//probably required by a number of carriers and by Emdeon
				seg++;
				sw.Write("PER*IC*"//PER01 2/2 Contact Function Code: IC=Information Contact.
					+Sout(PrefC.GetString(PrefName.PracticeTitle),60,1)+"*"//PER02 1/60 Name: Practice Title.
					+"TE*");//PER03 2/2 Communication Number Qualifier: TE=Telephone.
				if(clinic==null){
					sw.WriteLine(Sout(PrefC.GetString(PrefName.PracticePhone),256,1)+"~");//PER04  1/256 Comm Number: telephone number
				}
				else{
					sw.WriteLine(Sout(clinic.Phone,256,1)+"~");
				}
				//PER05 through PER09 are all situational or not used. Skipped here.
				//2010AB Not used.
				//2010AC Not used
				parentProv=HLcount;
				HLcount++;
				#endregion Billing Provider
				#region Attachments
				/*if(clearhouse.ISA08=="113504607" && claim.Attachments.Count>0){//If Tesia and has attachments
					claim.ClaimNote="TESIA#"+claim.ClaimNum.ToString()+" "+claim.ClaimNote;
					string saveFolder=clearhouse.ExportPath;//we've already tested to make sure this exists.
					string saveFile;
					string storedFile;
					for(int c=0;c<claim.Attachments.Count;c++){
						saveFile=ODFileUtils.CombinePaths(saveFolder,
							claim.ClaimNum.ToString()+"_"+(c+1).ToString()+Path.GetExtension(claim.Attachments[c].DisplayedFileName)+".IMG");
						storedFile=ODFileUtils.CombinePaths(FormEmailMessageEdit.GetAttachPath(),claim.Attachments[c].ActualFileName);
						File.Clone(storedFile,saveFile,true);
					}					
				}*/
				#endregion
				#region Subscriber
				if(patient.PatNum==subscriber.PatNum){//if patient is the subscriber
					hasSubord="0";//-claim level will follow
					//subordinate patients will not follow in this loop.  The subscriber loop will be duplicated for them.
				}
				else {//patient is not the subscriber
					hasSubord="1";//-patient will always follow
				}
				//2000B HL: Subscriber HL loop
				seg++;
				sw.WriteLine("HL*"+HLcount.ToString()+"*"//HL01 1/12 Hierarchical ID Number:
					+parentProv.ToString()+"*"//HL02 1/12 Hierarchical Parent ID Number: parent HL is always the billing provider HL.
					+"22*"//HL03 1/2 Hierarchical Level Code: 22=Subscriber (Only one option).
					+hasSubord+"~");//HL04 1/1 Hierarchical Child Code: 0=No subordinate HL segment in this hierarchical structure. 1=Additional subordinate HL data segment in this hierarchical structure.
				//2000B SBR:
				seg++;
				sw.Write("SBR*");
				string claimType="";
				if(claim.ClaimType=="PreAuth") {
					if(PatPlans.GetOrdinal(claim.InsSubNum,patPlans)==2 && claim.PlanNum2!=0) {
						isSecondaryPreauth=true;
						claimType="S";//secondary
					}
					else {
						claimType="P";//primary
					}
				}
				else if(claim.ClaimType=="P") {
					claimType="P";//primary
				}
				else if(claim.ClaimType=="S") {
					claimType="S";//secondary
				}
				else {
					claimType="T";//tertiary
				}
				sw.Write(claimType+"*");//SBR01 1/1 Payer Responsibility Sequence Number Code: 
				//todo: what about Cap?
				string relationshipCode="";//empty if patient is not subscriber.
				if(patient.PatNum==subscriber.PatNum) {//if patient is the subscriber
					relationshipCode="18";
				}
				sw.Write(relationshipCode+"*");//SBR02 2/2 Individual Relationship Code: 18=Self (The only option if not blank).
				//groupNumber and groupName do not need to be validated because they are optional.
				sw.Write(Sout(insPlan.GroupNum,50)+"*"//SBR03 1/50 Reference Identification: Group Number.
					+Sout(insPlan.GroupName,60)+"*"//SBR04 1/60 Name: Group Name.
					+"*");//SBR05 1/3 Insurance Type Code. Situational.  Skip because we don't support secondary Medicare.
				sw.Write("***");//SBR06, 07, & 08 not used.
				sw.WriteLine(GetFilingCode(insPlan)+"~");//"CI~");//SBR09: 12=PPO,17=DMO,BL=BCBS,CI=CommercialIns,FI=FEP,HM=HMO
				//2010BA NM1: Subscriber Name
				seg++;
				sw.WriteLine("NM1*IL*"//NM101 2/3 Entity Identifier Code: IL=Insured or Subscriber (The only option).
					+"1*"//NM102 1/1 Entity Type Qualifier: 1=Person, 2=Non-Person Entity.
					+Sout(subscriber.LName,60)+"*"//NM103 1/60 Name Last:
					+Sout(subscriber.FName,35)+"*"//NM104 1/35 Name First:
					+Sout(subscriber.MiddleI,25)+"*"//NM105 1/25 Name Middle:
					+"*"//NM106 1/10 Name Prefix: Not Used.
					+"*"//NM107 1/10 Name Suffix: Situational. Not present in Open Dental yet so we leave blank.
					+"MI*"//NM108 1/2 Identification Code Qualifier: MI=Member Identification Number.
					+Sout(sub.SubscriberID.Replace("-",""),80,2)//NM109 2/80 Identification Code: Situational. Required when NM102=1.
					+"~");//NM110 through NM112 are not used.
				//At the request of WebMD, we always include N3,N4,and DMG even if patient is not subscriber.
				//This does not make the transaction non-compliant, and they find it useful.
				//2010BA N3: Subscriber Address. Situational. Required when the patient is the subscriber.
				seg++;
				sw.Write("N3*"+Sout(subscriber.Address,55,1));//N301 1/55 Address Information:
				if(subscriber.Address2=="") {
					sw.WriteLine("~");
				}
				else {
					//N302 1/55 Address Information: Situational. Required when there is a second address line.
					sw.WriteLine("*"+Sout(subscriber.Address2,55)+"~");
				}
				//2010BA N4: Subscriber City, State, Zip Code. Situational. Required when the patient is the subscriber.
				seg++;
				sw.WriteLine("N4*"
						+Sout(subscriber.City,30,2)+"*"//N401 2/30 City Name:
						+Sout(subscriber.State,2,2)+"*"//N402 2/2 State or Provice Code:
						+Sout(subscriber.Zip.Replace("-",""),15,3)//N403 3/15 Postal Code:
						+"~");//N404 through N407 either not used or required for addresses outside of the United States.
				//2010BA DMG: Subscriber Demographic Information. Situational. Required when the patient is the subscriber.
				seg++;
				sw.Write("DMG*D8*");//DMG01 2/3 Date Time Period Format Qualifier: D8=Date Expressed in Format CCYYMMDD.
//todo: Validate subscriber BD?
				if(subscriber.Birthdate.Year<1900) {
					sw.Write("19000101*");//DMG02 1/35 Birthdate
				}
				else {
					sw.Write(subscriber.Birthdate.ToString("yyyyMMdd")+"*");//DMG02 1/35 Birthdate
				}
				sw.WriteLine(GetGender(subscriber.Gender)+"~");//DMG03 1/1 Gender Code: F=Female, M=Male, U=Unknown.
				//2010BA REF: Secondary Secondary Identification. Situational. Required when an additional identification number to that provided in NM109 of this loop is necessary. We do not use this.
				//2010BA REF: Property and Casualty Claim Number. Required when the services included in this claim are to be considered as part of a property and casualty claim. We do not use this.
				//Medical: 2010BA PER: Property and casualty subscriber contact info. We do not use this.
				//2010BB NM1: Payer Name.
				seg++;
				sw.Write("NM1*PR*"//NM101 2/3 Entity Identifier Code: PR=Payer.
					+"2*");//NM102 1/1 Entity Type Qualifier: 2=Non-Person Entity.
				//NM103 1/60 Name Last or Organization Name:
				if(clearhouse.ISA08=="EMS") {
					//This is a special situation requested by EMS.  This tacks the employer onto the end of the carrier.
					sw.Write(Sout(carrier.CarrierName,30)+"|"+Sout(Employers.GetName(insPlan.EmployerNum),30)+"*");
				}
				else {
					sw.Write(Sout(carrier.CarrierName,60)+"*");
				}
				sw.Write("****"//NM104 through NM107 Not Used.
					+"PI*");//NM108 1/2 Identification Code Qualifier: PI=Payor Identification.
				string electid=carrier.ElectID;
				if(electid=="" && clearhouse.ISA08=="113504607") {//only for Tesia
					electid="00000";
				}
				if(electid.Length<3) {
					electid="06126";
				}
				sw.WriteLine(Sout(electid,80,2)//NM109 2/80 Identification Code: PayorID.
					+"~");//NM110 through NM112 Not Used.
				//2010BB N3: Payer Address.
				seg++;
				sw.Write("N3*"+Sout(carrier.Address,55));//N301 1/55 Address Information:
				if(carrier.Address2=="") {
					sw.WriteLine("~");
				}
				else {
					//N302 1/55 Address Information: Required when there is a second address line.
					sw.WriteLine("*"+Sout(carrier.Address2,55)+"~");
				}
				//2010BB N4: Payer City, State, Zip Code.
				seg++;
				sw.WriteLine("N4*"
					+Sout(carrier.City,30,2)+"*"//N401 2/30 City Name:
					+Sout(carrier.State,2,2)+"*"//N402 2/2 State or Province Code:
					+Sout(carrier.Zip.Replace("-",""),15,3)//N403 3/15 Postal Code:
					+"~");//N404 through N407 are either not used or are for addresses outside of the United States.
				//2010BB REF: Payer Secondary Identificaiton. Situational. We do not use this.
				//2010BB REF: Billing Provider Secondary Identification. Situational. Required when NM109 (NPI) of loop 2010AA is not used. Since we are using NM109  in loop 2010AA, we do not use this segment.
				parentSubsc=HLcount;
				HLcount++;
				#endregion
				#region Patient
				if(patient.PatNum!=subscriber.PatNum){//if patient is not the subscriber
					//2000C Patient Hierarchical Level
					seg++;
					sw.WriteLine("HL*"+HLcount.ToString()+"*"//HL01 1/12 Hierarchical ID Number:
						+parentSubsc.ToString()+"*"//HL02 1/12 Hierarchical Parent ID Number: Parent is always the subscriber HL.
						+"23*"//HL03 1/2 Hierarchical Level Code: 23=Dependent.
						+"0~");//HL04 1/1 Hierarchical Child Code: 0=No subordinate HL segment in this hierarchical structure.
					//2000C PAT: Patient Information
					seg++;
					sw.WriteLine("PAT*"
						+GetRelat(claim.PatRelat)+"*"//PAT01 2/2 Individual Relationship Code:
						+"~");//PAT02 through PAT09 Not Used.
					//2010CA NM1: Patient Name
					seg++;
					sw.Write("NM1*QC*"//NM101 2/3 Entity Identifier Code: QC=Patient.
						+"1*"//NM102 1/1 Entity Type Qualifier: 1=Person.
						+Sout(patient.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
						+Sout(patient.FName,35));//NM104 1/35 Name First
						if(patient.MiddleI=="") {
							sw.WriteLine("~");
						}
						else {
							sw.WriteLine("*"+Sout(patient.MiddleI,25)+"~");//NM105 1/25 Name Middle
						}
						//NM106: prefix not used. NM107: No suffix field in Open Dental
						//NM108-NM112 no longer allowed to be used.
						//instead of including a patID here, the patient should get their own subsriber loop.
//TODO: js 9/5/11 Treat like a subscriber whenever patID is present.  Test.
					//2010CA N3: Patient Address.
					seg++;
					sw.Write("N3*"+
						Sout(patient.Address,55));//N301 1/55 Address Information
					if(patient.Address2=="") {
						sw.WriteLine("~");
					}
					else {
						//N302 1/55 Address Information: Only required when there is a second address line.
						sw.WriteLine("*"+Sout(patient.Address2,55)+"~");
					}
					//2010CA N4: Patient City, State, Zip Code.
					seg++;
					sw.WriteLine("N4*"
						+Sout(patient.City,30,2)+"*"//N401 2/30 City Name: 
						+Sout(patient.State,2,2)+"*"//N402 2/2 State or Provice Code: 
						+Sout(patient.Zip.Replace("-",""),15)//N403 3/15 Postal Code: 
						+"~");//N404 through N407 are either not used or only required for addresses outside the United States.
					//2010CA DMG: Patient Demographic Information.
					seg++;
					sw.WriteLine("DMG*D8*"//DMG01 2/3 Date Time Period Format Qualifier: D8=Date Expressed in Format CCYYMMDD.
						+patient.Birthdate.ToString("yyyyMMdd")+"*"//DMG02 1/35 Date Time Period:
						+GetGender(patient.Gender)//DMG03 1/1 Gender Code: F=Female,M=Male,U=Unknown.
						+"~");//DMG04 through DMG11 are Not Used.
					//2010CA REF: Property and Casualty Claim Number. Situational. We do not use this.
					//2010CA REF: (institutional) Property and Casualty Patient Identifier. Situational.  We do not use.
					HLcount++;
				}
				#endregion
				#region Claim CLM
				//2300 CLM: Claim Information.
				seg++;
				sw.Write("CLM*"
					+Sout(claim.PatNum.ToString()+"/"+claim.ClaimNum.ToString(),38)+"*"//CLM01 1/38 Claim Submitter's Identifier: A unique id.  By using both PatNum and ClaimNum, it is possible to search for a patient as well as to ensure uniqueness.
//todo: add field to allow user to override for claims based on preauths.
					+claim.ClaimFee.ToString()+"*"//CLM02 1/18 Monetary Amount:
					+"*"//CLM03 1/2 Claim Filing Indicator Code: Not Used.
					+"*");//CLM04 1/2 Non-Institutional Claim Type Code: Not Used.
					//CLM05 Health Care Service Location Information
				if(medType==EnumClaimMedType.Medical) {
					//todo
				}
				else if(medType==EnumClaimMedType.Institutional) {
					//claim.UniformBillType validated to be exactly 3 char
					//Example: 771: 7=clinic, 7=FQHC, 1=Only claim.  713: 7=clinic, 1=rural health clinic, 3=continuing claim
					sw.Write(claim.UniformBillType.Substring(0,2)+":"//CLM05-1 1/2  Facility Code Value: First and second position of UniformBillType
						+"A:"//CLM05-2 1/2 Facility Code Qualifier, A=Uniform Billing Claim Form Bill Type
						+claim.UniformBillType.Substring(2,1)+"*");//CLM05-3 1/1 Claim Frequency Type Code: Third position of UniformBillType
				}
				else{//dental.
					sw.Write(GetPlaceService(claim.PlaceService)+":"//CLM05-1 1/2  Facility Code Value: Place of Service
					+"B:"//CLM05-2 1/2 Facility Code Qualifier, B=Place of Service Codes
//todo: js 9/10/11 Consider supporting corrected and replacement.
					+"1"+"*");//CLM05-3 1/1 Claim Frequency Type Code: Code source 235: Claim Frequency Type Code. 1=original, 6=corrected, 7=replacement, 8=void(in limited jursidictions).  We currently only support 1-original.
				}
				sw.Write("Y*"//CLM06 1/1 Yes/No Condition or Response Code: prov sig on file (always yes)
					+"A*");//CLM07 1/1 Provider Accept Assignment Code: prov accepts medicaid assignment. OD has no field for this, so no choice
				sw.Write((sub.AssignBen?"Y":"N")+"*");//CLM08 1/1 Yes/No Condition or Response Code: We do not support W.
				sw.Write(sub.ReleaseInfo?"Y":"I");//CLM09 1/1 Release of Information Code: Y or I(which is equivalent to No)
				if(medType==EnumClaimMedType.Medical) {
					//todo
				}
				else if(medType==EnumClaimMedType.Institutional) {
					//CLM10-19 not used, 20 not supported.
					sw.WriteLine("~");
				}
				else if(medType==EnumClaimMedType.Dental) {
					if(claim.AccidentRelated!="" || claim.SpecialProgramCode!=EnumClaimSpecialProgram.None || claim.ClaimType=="PreAuth") {//if val for 11,12, or 19
						sw.Write("**"//* for CLM09. CLM10 not used
							+GetRelatedCauses(claim));//CLM11 2/3:2/3:2/3:2/2:2/3 Related Causes Information: Situational. Accident related, including state. Might be blank.
					}
					if(claim.SpecialProgramCode!=EnumClaimSpecialProgram.None || claim.ClaimType=="PreAuth") {//if val for 12, or 19
						sw.Write("*"//* for CLM11.
							+GetSpecialProgramCode(claim.SpecialProgramCode));//CLM12 2/3 Special Program Code: Situational. Example EPSTD.
					}
					if(claim.ClaimType=="PreAuth") {//if val for 19
						sw.Write("*"//* for CLM12.
							+"******"//CLM13-18 not used
							+"PB");//CLM19 2/2 Claim Submission Reason Code: PB=Predetermination of Benefits. Not allowed in medical claims. What is the replacement??
							//CLM20 1/2 Delay Reason Code: Situational. Required when claim is submitted late. Not supported.
					}
					sw.WriteLine("~");
				}
				#endregion Claim CLM
				#region Claim DTP
				if(medType==EnumClaimMedType.Medical) {
					//2300 DTP: Date of onset of current illness (medical)
					//2300 DTP: Initial treatment date (spinal manipulation) (medical)
					//2300 DTP: Date last seen (foot care) (medical)
					//2300 DTP: Date accute manifestation (spinal manipulation) (medical)
					//2300 DTP: Date referral
					//todo
				}
				else if(medType==EnumClaimMedType.Institutional) {
					//2300 DTP 096 (inst) Discharge hour. Inpatient.
					//2300 DTP 434 (inst) Statement. Required.
//todo:how to handle preauths?
					if(claim.DateService.Year>1880) {//DateService validated
						seg++;
						sw.WriteLine("DTP*434*"//DTP01 3/3 Date/Time Qualifier: 434=Statement.
							+"RD8*"//DTP02 2/3 RD8=Date range CCYYMMDD-CCYYMMDD.
							+claim.DateService.ToString("yyyyMMdd")+"-"+claim.DateService.ToString("yyyyMMdd")+"~");//DTP03 1/35 Date Time range
					}
					//2300 DTP 435 (inst) Admission date/hour. Inpatient.
					//2300 DTP 050 (inst) Repricer Received Date.  Not supported.
				}
				else if(medType==EnumClaimMedType.Dental) {
					//2300 DTP 439 (dental) Date accident. Situational. Required when there was an accident.
					if(claim.AccidentDate.Year>1880) {
						seg++;
						sw.WriteLine("DTP*439*"//DTP01 3/3 Date/Time Qualifier: 439=accident
							+"D8*"//DTP02 2/3 Date Time Period Format Qualifer: D8=Date Expressed in Format CCYYMMDD.
							+claim.AccidentDate.ToString("yyyyMMdd")+"~");//DTP03 1/35 Date Time Period:
					}
					//2300 DTP 452 (dental) Date Appliance Placement. Situational. Values can be overriden in loop 2400 for individual service items, but we don't support that.
					if(claim.OrthoDate.Year>1880) {
						seg++;
						sw.WriteLine("DTP*452*"//DTP01 3/3 Date/Time Qualifier: 452=Appliance Placement.
							+"D8*"//DTP02 2/3 Date Time Period Format Qualifier: D8=Date Expressed in Format CCYYMMDD.
							+claim.OrthoDate.ToString("yyyyMMdd")+"~");//DTP03 1/35 Date Time Period:
					}
					//2300 DTP 472 (dental) Service Date. Not used if predeterm.
					if(claim.ClaimType!="PreAuth") {
						if(claim.DateService.Year>1880) {
							seg++;
							sw.WriteLine("DTP*472*"//DTP01 3/3 Date/Time Qualifier: 472=Service.
								+"D8*"//DTP02 2/3 Date Time Period Format Qualifier: D8=Date Expressed in Format CCYYMMDD.
								+claim.DateService.ToString("yyyyMMdd")+"~");//DTP03 1/35 Date Time Period:
						}
					}
					//2300 DTP 050 (dental) Repricer Received Date.  Not supported.
				}
				#endregion Claim DTP
				#region Claim DN CL1
				if(medType==EnumClaimMedType.Dental) {
					//2300 DN1: Orthodontic Total Months of Treatment.
					if(claim.IsOrtho) {
						seg++;
						sw.WriteLine("DN1*"
							+"*"//DN101 1/15 Quantity: Not used because no field yet in OD.
							+claim.OrthoRemainM.ToString()//DN102 1/15 Quantity: Number of treatment months remaining.
							+"~");//DN103 is not used and DN104 is situational but we do not use it.
					}
					//2300 DN2: Tooth Status. Missing teeth.
					List<string> missingTeeth=ToothInitials.GetMissingOrHiddenTeeth(initialList);
					bool doSkip;
					int countMissing=0;
					for(int j=0;j<missingTeeth.Count;j++) {
						//if the missing tooth is missing because of an extraction being billed here, then exclude it
						//still needed, even though missing teeth are not based on procedures any longer
						doSkip=false;
						for(int p=0;p<claimProcs.Count;p++) {
							proc=Procedures.GetProcFromList(procList,claimProcs[p].ProcNum);
							procCode=ProcedureCodes.GetProcCode(proc.CodeNum);
							if(procCode.PaintType==ToothPaintingType.Extraction && proc.ToothNum==missingTeeth[j]) {
								doSkip=true;
								break;
							}
						}
						if(doSkip) {
							continue;
						}
						countMissing++;
						if(countMissing>35) {//segment max use 35
							continue;
						}
						seg++;
						sw.WriteLine("DN2*"
							+missingTeeth[j]+"*"//DN201 1/50 Reference Identification: Tooth number.
//todo: js 9/10/11 support E
							+"M~");//DN202 1/2 Tooth Status Code: M=Missing, E=To be extracted.
					}
				}
				if(medType==EnumClaimMedType.Institutional) {
					//2300 CL1: Institutional Claim Code. Required
					seg++;
					sw.Write("CL1*"
						+claim.AdmissionTypeCode//CL101 1/1 Admission Type Code. Required. Validated.

						//CL102 1/1 Admission source code. Required. Validated.

						//CL103 1/2 Patient status code. Required. Validated.
						);
				}
				#endregion Claim DN CL1
				#region Claim PWK
				//PWK loops-------------------------------------------------------------------------------------------------------
				//2300 PWK: Claim Supplemental Information. Paperwork. Used to identify attachments.
				/*if(clearhouse.ISA08=="113504607" && claim.Attachments.Count>0) {//If Tesia and has attachments
					seg++;
					sw.WriteLine("PWK*"
						+"OZ*"//PWK01: ReportTypeCode. OZ=Support data for claim.
						+"EL*"//PWK02: Report Transmission Code. EL=Electronic
						+"**"//PWK03 and 04: not used
						+"AC*"//PWK05: Identification Code Qualifier. AC=Attachment Control Number
						+"TES"+claim.ClaimNum.ToString()+"~");//PWK06: Identification Code.
				}*/
				//No validation is done.  However, warnings are displayed if:
				//Warning if attachments are listed as Mail even though we are sending electronically.
				//Warning if any PWK segments are needed, and there is no ID code.
				//PWK can repeat max 10 times.
				string pwk01="  ";
				if(claim.AttachedFlags.Contains("EoB")) {
					pwk01="EB";
				}
				if(claim.AttachedFlags.Contains("Note")) {
					pwk01="OB";
				}
				if(claim.AttachedFlags.Contains("Perio")) {
					pwk01="P6";
				}
				if(claim.AttachedFlags.Contains("Misc") || claim.AttachedImages>0) {
					pwk01="OZ";
				}
				if(claim.Radiographs>0) {
					pwk01="RB";
				}
				if(claim.AttachedModels>0) {
					pwk01="DA";
				}
				string pwk02="  ";
				if(claim.AttachedFlags.Contains("Mail")) {
					pwk02="BM";//By Mail
				}
				else {
					pwk02="EL";//Elect
				}
				string idCode=claim.AttachmentID;
				if(idCode=="") {//must be min of two char, so we need to make one up.
					idCode="  ";
				}
				idCode=Sout(idCode,80,2);
				seg++;
				sw.WriteLine("PWK*"
						+pwk01+"*"//PWK01 2/2 Report Type Code:
						+pwk02+"*"//PWK02 1/2 Report Transmission Code: EL=Electronically Only, BM=By Mail.
						+"**"//PWK03 and PWK04: Not Used.
						+"AC*"//PWK05 1/2 Identification Code Qualifier: AC=Attachment Control Number.
						+idCode+"~");//PWK06 2/80 Identification Code:
				#endregion Claim PWK
				#region Claim CN1 AMT
				//2300 CN1: Contract Information. Situational. We do not use this.
				//2300 AMT: Patient estimated amount due (inst)
				//2300 AMT: Patient Amount Paid. Situational. We do not use this.
				//2300 AMT: Total Purchased Service Amt (medical)
				#endregion Claim CN1 AMT
				#region Claim REF
				//All loops should be listed for medical (still todo), dental, and inst.  Order varies between types, so is not important.
				//2300 REF G3 (dental) Predetermination Identification. Situational. 
				//Required when sending claim for previously predetermined services. 
				//Do not send prior authorization number here.
				if(claim.PreAuthString!="") {//validated to be empty for medical and inst
					seg++;
					sw.WriteLine("REF*G3*"//REF01 2/3 G3=Predetermination of Benefits Identification Number.
						+Sout(claim.PreAuthString,50)//REF02 1/50 Predeterm of Benfits Identifier.
						+"~");//REF03 and REF04 are not used.
				}
				//2300 REF 4N (inst,dental) Service Authorization Exception Code. Situational. 
					//Required if services were performed without authorization.
//todo: ServiceAuthException
				//2300 REF F8 (inst, dental) Payer Claim Control Number
					//Situational. Required if this is a replacement or a void. 
					//F8=Original Reference Number 
					//aka Original Document Control Number/Internal Control Number (DCN/ICN).
					//aka Transaction Control Number (TCN).  
					//aka Claim Reference Number. 
					//Seems to be required by Medicaid when voiding a claim or resubmitting a claim by setting the CLM05-3.
//todo: Implement
				//2300 REF 9F (med,inst,dental) Referral Number. Situational. 
				if(claim.RefNumString!="") {
					seg++;
					sw.WriteLine("REF*9F*"//REF01 2/3 Reference Identification Qualifier: 9F=Referral number. 
						+Sout(claim.RefNumString,30)//REF02 1/50 Reference Identification:
						+"~");//REF03 and REF04 are not used.
				}
				//2300 REF G1 (inst,dental) Prior Authorization. Situational. 
				//Do not report predetermination of benefits id number here.
				//G1 and G3 were muddled in 4010.  
				if(claim.PriorAuthorizationNumber!="") {
					seg++;
					sw.WriteLine("REF*G1*"//REF01 2/3 G1=Prior Authorization Number
						+Sout(claim.PriorAuthorizationNumber,50)//REF02 1/50 Prior Auth Number
						+"~");//REF03 and REF04 are not used.
				}					
				//2300 REF 9A (inst,dental) Repriced Claim Number. Situational. We do not use. 
				//2300 REF 9C (inst,dental) Adjusted Repriced Claim Number. Situational. We do not use.
				//2300 REF D9 (inst,dental) Claim Identifier For Transmission Intermediaries. Situational. We do not use.
				//2300 REF LX (inst) Investigational Device Exemption Number 
					//required for FDA IDE.
				//2300 REF LU (inst) Auto Accident State 
					//seems to me to be a duplicate of the info in CLM11
				//2300 REF EA (inst) Medical Record Number 
				//2300 REF P4 (inst) Demonstration Project Identifier 
					//seems very unimportant
				//2300 REF G4 (inst) Peer Review Organization (PRO) Approval Number 
				#endregion Claim REF
				#region Claim K3 NTE CRx
				//2300 K3: File info (medical, dental, inst). Situational. We do not use this.
				//NTE loops------------------------------------------------------------------------------------------------------
				//2300 NTE: Claim Note. Situational. A number of NTE01 codes other than 'ADD', which we don't support. (inst, dental)
				string note="";
				if(claim.AttachmentID!="" && !claim.ClaimNote.StartsWith(claim.AttachmentID)) {
					note=claim.AttachmentID+" ";
				}
				note+=claim.ClaimNote;
				if(note!="") {
					seg++;
					sw.WriteLine("NTE*ADD*"//NTE01 3/3 Note Reference Code: ADD=Additional information.
						+Sout(note,80)+"~");//NTE02 1/80 Additional Information:
				}
				//2300 NTE: Billing Note. Situational. (inst)
				//CRx loops------------------------------------------------------------------------------------------------------
				//2300 CR1: (medical)Ambulance transport info
				//2300 CR2: (medical) Spinal Manipulation Service Info
				//2300 CRC: (medical) About 3 irrelevant segments
				//2300 CRC: (inst) EPSDT Referral
					//required on EPSDT claims when the screening service is being billed in this claim.
				#endregion Claim K3 NTE CRx
				#region Claim HI HCP
				//HI loops-------------------------------------------------------------------------------------------------------
				//All HI loops should be listed for medical (still todo), dental, and inst. 
				List<string> diagnosisList=new List<string>();//princDiag will always be the first element.
				if(medType==EnumClaimMedType.Medical || medType==EnumClaimMedType.Institutional){
					for(int j=0;j<claimProcs.Count;j++) {
						proc=Procedures.GetProcFromList(procList,claimProcs[j].ProcNum);
						if(proc.DiagnosticCode=="") {
							continue;
						}
						if(proc.IsPrincDiag) {
							if(diagnosisList.Contains(proc.DiagnosticCode)) {
								diagnosisList.Remove(proc.DiagnosticCode);
							}
							diagnosisList.Insert(0,proc.DiagnosticCode);//princDiag always goes first. There will always be one.
						}
						else {//not princDiag
							if(!diagnosisList.Contains(proc.DiagnosticCode)) {
								diagnosisList.Add(proc.DiagnosticCode);
							}
						}
					}
				}
				//2300 HI: Principal Diagnosis BK (inst)
				//required
				if(medType==EnumClaimMedType.Institutional) {
					seg++;
					sw.Write("HI*"
						+"BK:"//HI01-1: BK=ICD-9 Principal Diagnosis
//todo: validate at least one diagnosis
						+Sout((string)diagnosisList[0],30).Replace(".",""));//HI01-2: Diagnosis code. No periods.
					sw.WriteLine("~");
				}
				//medical stub for Principal Diagnosis BK
				/*if(medType==EnumClaimMedType.Institutional) {
					seg++;
					sw.Write("HI*"
						+"BK:"//HI01-1: BK=ICD-9 Principal Diagnosis
						+Sout((string)diagnosisList[0],30).Replace(".",""));//HI01-2: Diagnosis code. No periods.
					for(int j=1;j<diagnosisList.Count;j++) {
						if(j>11) {//maximum of 12 diagnoses
							continue;
						}
						sw.Write("*"//this is the * from the _previous_ field.
							+"BF:"//HI0#-1: BF=ICD-9 Diagnosis
							+Sout((string)diagnosisList[j],30).Replace(".",""));//HI0#-2: Diagnosis code. No periods.
					}
					sw.WriteLine("~");
				}*/
				//2300 HI: Health Care Diagnosis Code. Situational. BK (dental)
					//todo: might be a good idea
					//for OMS or anesthesiology
				//2300 HI: Admitting Diagnosis BJ (inst)
					//required for inpatient admission.
				//2300 HI: Patient's Reason for Visit PR (inst)
//todo: probably required
					//required for outpatient visits.
				//2300 HI: External Cause of Injury BN (inst)
				//2300 HI: Diagnosis Related Group (DRG) Information (inst)
					//for inpatient hospital under DRG contract
				//2300 HI: Other Diagnosis Information BF (inst)
					//when other conditions coexist or develop
				//2300 HI: Principal Procedure Information BR (inst)
					//required on inpatient claims when a procedure was performed
				//2300 HI: Other Procedure Information BQ (inst)
					//inpatient claims for additional procedures.
				//2300 HI: Occurence Span Information BI (inst)
					//for an occurence span code
				//2300 HI: Occurence Information BH (inst)
					//for an occurence code
				//2300 HI: Value Information BE (inst)
					//for a value code
				//2300 HI: Condition Information BG (inst)
					//for a condition code
				//2300 HI: Treatment Code Information TC (inst)
					//when home health agencies need to report plan of treatment information under contracts
				//2300 HCP: Claim Pricing/Repricing Information. Situational. We do not use. (medical, inst, dental)
				#endregion Claim HI HCP
				#region 2310 Claim Providers (medical)
				//Since order might be important, we have to handle medical, institutional, and dental separately.
				if(medType==EnumClaimMedType.Medical) {
//todo: (medical) provider loops.  The medical loops below were just thrown in here from somewhere else and are not even started.
					/*
					if(medType==EnumClaimMedType.Medical) {
						seg++;
						sw.WriteLine("PRV*"
							+"PE*"//PRV01: PE=Performing
							+"PXC*"//PRV02: PXC=Health Care Provider Taxonomy Code
							+X12Generator.GetTaxonomy(provTreat)+"~");//PRV03: Taxonomy code
					}*/
					//or 2310D (medical)NM1: Service facility location. Required if different from 2010AA. Not supported.
					//2310D (medical)N3,N4,REF,PER: not supported.
					//2310E (medical)NM1,REF Supervising Provider. Not supported.
					//2310F (medical)NM1,N3,N4 Ambulance Pickup location. Not supported.
					//2310G (medical)NM1,N3,N4 Ambulance Dropoff location. Not supported.
				}
				#endregion 2310 Claim Providers (medical)
				#region 2310 Claim Providers (inst)
				if(medType==EnumClaimMedType.Institutional) {
					//2310A (inst) Attending Provider 71 
						//required. Provider with overall responsibility for care and treatment reported on this claim.
	//todo: attending provider.  We will use our treating provider field.
					//2310A (inst) NM1
					//2310A (inst) PRV
					//2310B (inst) Operating Physician Name 72 
					//for surgical procedure codes
					//2310C (inst) Other Operating Physician Name ZZ
					//2310D (inst) Rendering Provider Name 82
					//if different from attending provider AND when regulations require both facility and professional components.
					//2310E (inst) Service Facility Location Name 77
	//todo:
						//NM1
						//N3
						//N4
					//required when place of service is different from loop 2010AA billing provider
					//2310F (inst) Referring Provider Name DN
					//required when referring provider is different from attending provider
				}
				#endregion 2310 Claim Providers (inst)
				#region 2310 Claim Providers (dental)
				if(medType==EnumClaimMedType.Dental) {
					//2310A (dental) Referring Provider------------------------------------------------------------------
					//2310A NM1 (dental) //js 9/5/11 Why not?  I thought this was a field on the claim that we DID send.
					//2310A PRV: Referring Provider Specialty Information. Situational. 
					//2310B (dental) Rendering Provider------------------------------------------------------------------
					//2310B NM1: Rendering Provider Name. Situational. Only required if different from the billing provider, but required by WebClaim, so we will always include it. (dental)
					provTreat=Providers.GetProv(claim.ProvTreat);
					seg++;
					sw.Write("NM1*82*"//NM101 2/3 Entity Identifier Code: 82=Rendering Provider.
						+"1*"//NM102 1/1 Entity Type Qualifier: 1=Person.
						+Sout(provTreat.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
						+Sout(provTreat.FName,35)+"*"//NM104 1/35 Name First:
						+Sout(provTreat.MI,25)+"*"//NM105 1/25 Name Middle:
						+"*"//NM106 1/10 Name Prefix: Not Used.
						+"*");//NM107 1/10 Name Suffix: We don't support.
					sw.Write("XX*");//NM108 1/2 Identification Code Qualifier: Situational. Required since after the HIPAA date. XX=NPI.
					sw.WriteLine(Sout(provTreat.NationalProvID,80)//NM109 2/80 Identification Code:  NPI validated.
						+"~");//NM110 through NM112 are not used.
					//2310B PRV: Rendering Provider Specialty Information. (dental)
					seg++;
					sw.WriteLine("PRV*"
						+"PE*"//PRV01 1/3 Provider Code: PE=Performing.
						+"PXC*"//PRV02 2/3 Reference Identification Qualifier: PXC=Health Care Provider Taxonomy Code.
						+X12Generator.GetTaxonomy(provTreat)//PRV03 1/50 Reference Identification: Taxonomy Code.
						+"~");//PRV04 through PRV06 are not used.
					//2310B REF: Rendering Provider Secondary Identification. Situational.
	//todo: is this validated?
					seg++;
					sw.WriteLine("REF*0B*"//REF01 2/3 Reference Identification Qualifier: 0B=State License Number.
						+Sout(provTreat.StateLicense,50)//REF02 1/50 Reference Identification:
						+"~");//REF03 and REF04 are not used.
					//2310C (dental) Service Facility Location ----------------------------------------------------------
					//2310C NM1: Service Facility Location Name. Situational. Only required if PlaceService is 21,22,31, or 35. 35 does not exist in CPT, so we assume 33. (dental)
					if(claim.PlaceService==PlaceOfService.InpatHospital || claim.PlaceService==PlaceOfService.OutpatHospital
						|| claim.PlaceService==PlaceOfService.SkilledNursFac || claim.PlaceService==PlaceOfService.CustodialCareFacility) {//AdultLivCareFac
						seg++;
						sw.WriteLine("NM1*77*"//NM101 2/3 Entity Identifier Code: 77=Service Location.
							+"2*"//NM102 1/1 Entity Type Qualifier: 2=Non-Person Entity.
							+Sout(billProv.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
							+"*"//NM104 1/35 Name First: Not Used.
							+"*"//NM105 1/25 Name Middle: Not Used.
							+"*"//NM106 1/10 Name Prefix: Not Used.
							+"*"//NM107 1/10 Name Suffix: Not Used.
							+"XX*"//NM108 1/2 Identification Code Qualifier: XX=NPI.
							+Sout(billProv.NationalProvID,80)//NM109 2/80 Identification Code: Validated.
							+"~");//NM110 through NM112 not used.
					}
					//2310C N3: Service Facility Location Address.
					seg++;
					sw.WriteLine("N3*"
						+Sout(address1,55,1)+"*"//N301 1/55 Address Information:
						+Sout(address2),55,1+"~");//N302 1/55 Address Information: Only required when there is a secondary address line.
					//2310C N4: Service Facility Location City, State, Zip Code.
					seg++;
					sw.WriteLine("N4*"
						+Sout(city,30,2)+"*"//N401 2/30 City Name:
						+Sout(state,2,2)+"*"//N402 2/2 State or Provice Code:
						+Sout(zip,15,3)+"*"//N403 3/15 Postal Code:
						+"~");//N404 through N407 are either not used or only required when outside of the United States.
					//2310C REF: Service Facility Location Secondary Identification. Situational. We do not use this. (dental)
					//2310D (dental) Assistant Surgeon--------------------------------------------------------------------
					//we do not support
					//2310E (dental) Supervising Provider-----------------------------------------------------------------
					//we do not support
				}
				#endregion 2310 Claim Providers (dental)
				#region 2320 Other subscriber information
				//2320 Other subscriber------------------------------------------------------------------------------------------
				if(claim.PlanNum2>0) {
					//2320 SBR: Other Subscriber Information. Situational.
					seg++;
					sw.Write("SBR*");
					//SBR01 1/1 Payer Responsibility Sequence Number Code:
					if(claim.ClaimType=="PreAuth") {
						if(isSecondaryPreauth) {
							sw.Write("P*");
						}
						else {
							sw.Write("S*");
						}
					}
					else if(claim.ClaimType=="S") {
						sw.Write("P*");
					}
					else if(claim.ClaimType=="P") {
						sw.Write("S*");
					}
					else {
						sw.Write("T*");//T=Tertiary
					}
					sw.Write(GetRelat(claim.PatRelat2)+"*"//SBR02 2/2 Individual Relationship Code:
						+Sout(otherPlan.GroupNum,50)+"*"//SBR03 1/50 Reference Identification:
						+Sout(otherPlan.GroupName,60)+"*"//SBR04 1/60 Name: Group Name.
						+"*"//SBR05 1/3 Insurance Type Code: Situational. Required when they payer in loop 2330B is Medicare and Medicare is not the primary payer. TODO: implement.
						+"*"//SBR06 1/1 Coordination of Benefits Code: Not used.
						+"*"//SBR07 1/1 Yes/No Condition or Response Code: Not Used.
						+"*"//SBR08 2/2 Employment Status Code: Not Used.
						+"CI~");//SBR09 1/2 Claim Filing Indicator Code: 12=PPO,17=DMO,BL=BCBS,CI=CommercialIns,FI=FEP,HM=HMO. There are too many. I'm just going to use CI for everyone. I don't think anyone will care.
					//2320 CAS: Claim Level Adjustments. Situational. We do not use this. (dental)
					//2320 AMT-------------------------------------------------------------------------------------------------------
					//2320 AMT: COB Payer Paid Amount. Situational. Required when the claim has been adjudicated by payer in loop 2330B. D (inst,dental)
					if(claim.ClaimType!="P") {
						double paidOtherIns=0;
						for(int j=0;j<claimProcs.Count;j++) {
							paidOtherIns+=ClaimProcs.ProcInsPayPri(claimProcList,claimProcs[j].ProcNum,claimProcs[j].PlanNum);
						}
						seg++;
						sw.WriteLine("AMT*D*"//AMT01 1/3 Amount Qualifier Code: D=Payor Amount Paid.
							+paidOtherIns.ToString("F")//AMT02 1/18 Monetary Amount:
							+"~");//AMT03 Not Used.
					}
					//2320 AMT: Remaining Patient Liability. Situational. Required when claim has been adjudicated by payer in loop 2330B. EAF (medical,inst,dental)
//todo:
					//2320 AMT: COB Total Non-Covered Amount. Situational. Can be set when primary claim was not adjudicated. A8 (medical,inst,dental)
					//2320 OI: Other Insurance Coverage Information.
					seg++;
					sw.Write("OI*"
						+"*"//OI01 1/2 Claim Filing Indicator Code: Not Used.
						+"*"//OI02 2/2 Claim Submission Reason Code: Not Used.
						+(otherSub.AssignBen?"Y":"N")+"*"//OI03 1/1 Yes/No Condition or Response Code:
						+"*"//OI04 1/1 Patient Signature Source Code: Not Used.
						+"*"//OI05 1/1 Provider Agreement Code: Not Used.
						+(otherSub.ReleaseInfo?"Y":"I")//OI06 1/1 Release of Information Code:
						+"~");
					//2320 MIA: (inst) Inpatient Adjudication Information
					//We don't support.
					//2320 MOA: Outpatient Adjudication Information. Situational. For reporting remark codes from ERAs. We don't support. (medical,inst,dental)
					#endregion 2320 Other subscriber information
					#region 2330A Other subscriber Name
					//2330A Other subscriber -----------------------------------------------------------------------------------------
					//2330A NM1: Other Subscriber Name.
					seg++;
					sw.WriteLine("NM1*IL*"//NM010 2/3 Entity Identifier Code: IL=Insured or Subscriber.
						+"1*"//NM102 1/1 Entity Type Qualifier: 1=Person.
						+Sout(otherSubsc.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
						+Sout(otherSubsc.FName,35)+"*"//NM104 1/35 Name First:
						+Sout(otherSubsc.MiddleI,25)+"*"//NM105 1/25 Middle Name:
						+"*"//NM106 1/10 Name Prefix: Not Used.
						+"*"//NM107 1/10 Name Suffix: Situational. No corresponding field in OD.
						+"MI*"//NM108 1/2 Identification Code Qualifier: MI=Member Identification Number.
						+Sout(otherSub.SubscriberID,80)//NM109 2/80 Identification Code:
						+"~");//NM110 through NM112 are not used.
					//2330A N3: Other Subscriber Address.
					seg++;
					sw.Write("N3*"
						+Sout(otherSubsc.Address,55)+"*"//N301 1/55 Address Information:
						+Sout(otherSubsc.Address2,55)+"~");//N302 1/55 Address Information:
					//2330A N4: Other Subscriber City, State, Zip Code.
					seg++;
					sw.WriteLine("N4*"
						+Sout(otherSubsc.City,30,2)+"*"//N401 2/30 City Name:
						+Sout(otherSubsc.State,2,2)+"*"//N402 2/2 State or Province Code:
						+Sout(otherSubsc.Zip,15,3)//N403 3/15 Postal Code:
						+"~");//N404 through N407 are either not required or are required when the address is outside of the United States.
					//2330A REF: Other Subscriber Secondary Identification. Situational. Not supported.
					#endregion 2330A Other subscriber Name
					#region Other payer
					//2330B Other payer--------------------------------------------------------------------------------------------
					//2330B NM1: Other Payer Name.
					seg++;
					sw.Write("NM1*PR*"//NM101 2/3 Entity Code Identifier: PR=Payer.
						+"2*");//NM102 1/1 Entity Type Qualifier: 2=Non-Person.
					//NM103 1/60 Name Last or Organization Name:
					if(clearhouse.ISA08=="EMS") {
						//This is a special situation requested by EMS.  This tacks the employer onto the end of the carrier.
						sw.Write(Sout(otherCarrier.CarrierName,30)+"|"+Sout(Employers.GetName(otherPlan.EmployerNum),30)+"*");
					}
					else {
						sw.Write(Sout(otherCarrier.CarrierName,60)+"*");
					}
					sw.Write(
						 "*"//NM104 1/35 Name First: Not Used.
						+"*"//NM105 1/25 Name Middle: Not Used.
						+"*"//NM106 1/10 Name Prefix: Not Used.
						+"*"//NM107 1/10 Name Suffix: Not Used.
						+"PI*");//NM108 1/2 Identification Code Qualifier: PI=Payor Identification. XV must be used after national plan ID mandated.
					//NM109 2/80 Identification Code:
					if(otherCarrier.ElectID.Length<3) {
						sw.WriteLine("06126");
					}
					else {
						sw.WriteLine(Sout(otherCarrier.ElectID,80,2));
					}
					sw.WriteLine("~");//NM110 through NM112 not used.
					//2230B N3: Other Payer Address. Situational. We don't support. (medical,dental)
					//2330B N4: Other Payer City, State, Zip Code. Situational. We don't support. (medical,dental)
					//2330B DTP: Claim Check or Remittance Date. Situational. Claim Paid date. (dental)
					if(claim.ClaimType!="P") {
						DateTime datePaidOtherIns=DateTime.Today;
						DateTime dtThisCP;
						for(int j=0;j<claimProcs.Count;j++) {
							dtThisCP=ClaimProcs.GetDatePaid(claimProcList,claimProcs[j].ProcNum,claimProcs[j].PlanNum);
							if(dtThisCP>datePaidOtherIns) {
								datePaidOtherIns=dtThisCP;
							}
						}
						//it's a required segment, so always include it.
						seg++;
						sw.WriteLine("DTP*573*"//DTP01 3/3 Date/Time Qualifier: 573=Date Claim Paid.
							+"D8*"//DTP02 2/3 Date Time Period Format Qualifier: D8=Date Expressed in Format CCYYMMDD.
							+datePaidOtherIns.ToString("yyyyMMdd")+"~");//DTP03 1/35 Date Time Period:
					}
					//2330B DTP: (medical) Claim adjudication date. We might need to add this
					//2330B REF: Other Payer Secondary Identifier. Situational. We do not use. (dental)
					//2330B REF: Other Payer Prior Authorization Number: Situational. We do not use. (dental)
					//2330B REF: Other Payer Referral Number. Situational. We do not use. (dental)
					//2330B REF: Other Payer Claim Adjustment Indicator. Situational. We do not use. (dental)
					//2330B REF: Other Payer Predetermination Identification. Situational. We do not use. (dental)
					//2230B REF: Other Payer Claim Control Number. Situational. We do not use. (dental)
					//2330C NM1: Other Payer Referring Provider. Situational. Only used in crosswalking COBs. We do not use. (dental)
					//2330C REF: Other Payer Referring Provider Secondary Identification. We do not use. (dental)
					//2330D NM1: Other Payer Rendering Provider. Situational. Only used in crosswalking COBs. We do not use. (dental)
					//2330D REF: Other Payer Rendering Provider Secondary Identificaiton. We do not use. (dental)
					//2330E NM1: Other Payer Supervising Provider. Situational. We do not use. (medical,dental)
					//2330E REF: Other Payer Supervising Provider Secondary Identificaiton. We do not use. (medical,dental)
					//2330F NM1: Other Payer Billing Provider. Situational. We do not use. (medical,dental)
					//2330F REF: Other Payer Billing Provider Secondary Identification. We do not use. (medical,dental)
					//2330G NM1: Other Payer Service Facility Location. Situational. We do not use. (medical,dental)
					//2330G REF: Other Payer Service Facility Location Secondary Identification. We do not use. (medical,dental)
					//2330H NM1: Other Payer Assistant Sugeon. Situational. We do not use. (medical,dental)
					//2330H REF: Other Payer Assistant Surgeon Secondary Identifier. We do not use. (medical,dental)
					//2330I: (medical) not supported
					#endregion Other payer
				}
				for(int j=0;j<claimProcs.Count;j++) {
					#region Service Line
					proc=Procedures.GetProcFromList(procList,claimProcs[j].ProcNum);
					procCode=ProcedureCodes.GetProcCode(proc.CodeNum);
					//2400 LX: Service Line Number.
					seg++;
					sw.WriteLine("LX*"+(j+1).ToString()+"~");
					if(medType==EnumClaimMedType.Medical) {
						//2400 SV1: Professional Service
						seg++;
						sw.Write("SV1*"
							//SV101 Composite Medical Procedure Identifier
							+"HC:"//SV101-1: HC=Health Care
							+Sout(claimProcs[j].CodeSent)+"*"//SV101-2: Procedure code. The rest of SV101 is not supported
							+claimProcs[j].FeeBilled.ToString()+"*"//SV102: Charge Amt
							+"MJ*"//SV103: MJ=minutes
							+"0*");//SV104: Quantity of anesthesia. We don't support, so always 0.
						if(proc.PlaceService==claim.PlaceService) {
							sw.Write("*");//SV105: Place of Service Code if different from claim
						}
						else {
							sw.Write(GetPlaceService(proc.PlaceService)+"*");
						}
						sw.Write("*");//SV106: not used
						//SV107: Composite Diagnosis Code Pointer. Required when 2300HI(Health Care Diagnosis Code) is used (always).
						//SV107-1: Primary diagnosis. Only allowed pointers 1-8 even though 2300HI supports 12 diagnoses.
						//We don't validate that there are not more than 8 diagnoses on one claim.
						//If the diagnosis we need is not in the first 8, then we will use the primary.
						if(proc.DiagnosticCode=="") {//If the diagnosis is blank, we will use the primary.
							sw.Write("1");//use primary.
						}
						else {
							int diagI=1;
							for(int d=0;d<diagnosisList.Count;d++) {
								if(d>7) {//we can't point to any except first 8.
									continue;
								}
								if((string)diagnosisList[d]==proc.DiagnosticCode) {
									diagI=d+1;
								}
							}
							sw.Write(diagI.ToString());
						}
						//SV107-2 through 4: Other diagnoses, which we don't support yet.
						sw.WriteLine("~");
						//sw.Write("*");//SV108: not used
						//sw.Write("*");//SV109: Emergency indicator. Required if emergency. Y or blank. Not supported.
						//sw.Write("*");//SV110: not used
						//sw.Write("*");//SV111: EPSTD indicator (Medicaid from screening). Y or blank. Not supported.
						//sw.Write("*");//SV112: Family planning indicator for Medicaid. Y or blank. Not supported.
						//sw.Write("**");//SV113 and SV114: not used
						//SV115: Copay status code: 0 or blank. Not supported
					}//medical
					else if(medType==EnumClaimMedType.Institutional) {
						//2400 SV2: Institutional Service Line
						seg++;
						sw.Write("SV2*"
							+Sout(proc.RevCode,48)+"*"//SV201 1/48 Product/Service ID, Revenue Code, validated
							//SV202 Composite Medical Procedure Identifier
							+"HC:"//SV202-1: HC=Health Care. Includes CPT codes.
							+Sout(claimProcs[j].CodeSent));//SV202-2: Procedure code. 
						//mods validated to be exactly 2 char long or else blank.
						//SV202-3,4,5,6 2/2 Modifiers
						if(proc.CodeMod1!=""){
							sw.Write(":"+Sout(proc.CodeMod1));
						}
						if(proc.CodeMod2!=""){
							sw.Write(":"+Sout(proc.CodeMod2));
						}
						if(proc.CodeMod3!=""){
							sw.Write(":"+Sout(proc.CodeMod3));
						}
						if(proc.CodeMod4!=""){
							sw.Write(":"+Sout(proc.CodeMod4));
						}
						sw.WriteLine("*"//end of SV202
							+claimProcs[j].FeeBilled.ToString()+"*"//SV203: Charge Amt
							+"UN*"//SV204: UN=Units. We don't support Days yet.
							+proc.UnitQty.ToString()+"~");//SV205: Quantity 
					}//inst
					else if(medType==EnumClaimMedType.Dental) {
						//2400 SV3: Dental Service.
						seg++;
						sw.Write("SV3*"
							+"AD:"+Sout(claimProcs[j].CodeSent,5)+"*"//SV301-1 2/2 Product/Service ID Qualifier: AD=American Dental Association Codes; SV301-2 1/48 Product/Service ID: Procedure code; SV301-3 through SV301-8 are Situational. We do not use.
							+claimProcs[j].FeeBilled.ToString()+"*");//SV302 1/18 Monetary Amount: Charge Amount.
						string placeService="";
						if(proc.PlaceService!=claim.PlaceService) {
							placeService=GetPlaceService(proc.PlaceService);
						}
						//SV303 1/2 Facility Code Value: Location Code if different from claim.
						sw.WriteLine(placeService+"*"
							+GetArea(proc,procCode)+"*"//SV304 Oral Cavity Designation: SV304-1 1/3 Oral Cavity Designation Code: Area. SV304-2 through SV304-5 are situational and we do not use.
							+proc.Prosthesis+"*"//SV305 1/1 Prothesis, Crown or Inlay Code: I=Initial Placement. R=Replacement.
							+proc.UnitQty.ToString()//SV306 1/15 Quantity: Situational. Procedure count.
							+"~");//SV307 throug SV311 are either not used or are situational and we do not use.
						//2400 TOO: Tooth Information. Number/Surface.
						if(procCode.TreatArea==TreatmentArea.Tooth) {
							seg++;
							sw.WriteLine("TOO*JP*"//TOO01 1/3 Code List Qualifier Code: JP=Universal National Tooth Designation System.
								+proc.ToothNum//TOO02 1/30 Industry Code: Tooth number.
								+"~");//TOO03 Tooth Surface: Situational. Not applicable.
						}
						else if(procCode.TreatArea==TreatmentArea.Surf) {
							seg++;
							sw.Write("TOO*JP*"//TOO01 1/3 Code List Qualifier Code: JP=Universal National Tooth Designation System.
								+proc.ToothNum+"*");//TOO02 1/30 Industry Code: Tooth number.
							string validSurfaces=Tooth.SurfTidyForClaims(proc.Surf,proc.ToothNum);
							for(int k=0;k<validSurfaces.Length;k++) {
								if(k>0) {
									sw.Write(":");
								}
								sw.Write(validSurfaces.Substring(k,1));//TOO03 Tooth Surface: TOO03-1 through TOO03-5 are for individual surfaces.
							}
							sw.WriteLine("~");
						}
						else if(procCode.TreatArea==TreatmentArea.ToothRange) {
							string[] individTeeth=proc.ToothRange.Split(',');
							for(int t=0;t<individTeeth.Length;t++) {
								seg++;
								sw.WriteLine("TOO*JP*"//TOO01 1/3 Code List Qualifier Code: JP=Universal National Tooth Designation System.
									+individTeeth[t]//TOO02 1/30 Industry Code: Tooth number.
									+"~");//TOO03 Tooth Surface: Situational. Not applicable.
							}
						}
					}//dental
					#endregion Service Line
					#region Service DTP
					//2400 DTP: Service Date. Situaitonal. Required if different from claim, but we will always show the date. Better compatibility. 472 (inst,dental)
					//Required if medical anyway?
					if(claim.ClaimType!="PreAuth") {
						seg++;
						sw.WriteLine("DTP*472*"//DTP01 3/3 Date/Time Qualifier: 472=Service.
							+"D8*"//DTP02 2/3 Date Time Period Format Qualifier: D8=Date Expressed in Format CCYYMMDD.
							+proc.ProcDate.ToString("yyyyMMdd")+"~");//DTP03 1/35 Date Time Period:
					}
					//2400 DTP: Date Prior Placement. Situational. Required when replacement. 441 (dental)
					if(proc.Prosthesis=="R") {//already validated date
						seg++;
						sw.WriteLine("DTP*441*"//DTP01 3/3 Date/Time Qualifier: 441=Prior Placement.
							+"D8*"//DTP02 2/3 Date Time Period Format Qualifier: D8=Date Expressed in Format CCYYMMDD.
							+proc.DateOriginalProsth.ToString("yyyyMMdd")+"~");//DTP03 1/35 Date Time Period:
					}
					//2400 DTP: Date Appliance Placement. Situational. Ortho appliance placement. We do not use. 452 (dental)
					//2400 DTP: Date Replacement. Date ortho appliance replaced. We do not use. 446 (dental)
					//2400 DTP: Date Treatment Start. Situational. Rx date. We do not use. 196 (medical,dental)
					//2400 DTP: Date Treatment Completion. Situational. We do not use. 198 (dental)
					//2400 DTP: (medical) Date Certification Revision. Not supported.
					//2400 DTP: (medical) Date begin therapy. Not supported.
					//2400 DTP: (medical) Date last certification. Not supported.
					//2400 DTP: (medical) Date last seen. Not supported.
					//2400 DTP: (medical) Dialysis test dates. Not supported.
					//2400 DTP: (medical) Date blood gas test. Not supported.
					//2400 DTP: (medical) Date shipped. Not supported.
					//2400 DTP: (medical) Date last x-ray. Not supported.
					//2400 DTP: (medical) Date initial tx. Not supported.
					#endregion Service DTP
					#region Service QTY MEA CN1
					//2400 QTY: (medical) Ambulance patient count. Not supported.
					//2400 QTY: (medical) Anesthesia quantity. Not used.
					//2400 MEA (medical)
					//2400 CN1: Contract Information. Situational. We do not use. (medical,dental)
					#endregion Service QTY MEA CN1
					#region Service REF
					//2400 REF: G3 (dental) Service Predetermination Identification. Situational. Pretermination ID. We do not use.
					//2400 REF: G1 (dental) Prior Authorization. Situational. We do not use.
					//2400 REF 9F (dental) Referral Number. Situational. We do not use.
					//2400 REF 9A (dental) Repriced Claim Number. Situational. We do not use.
					//2400 REF 9B (inst) Repriced line item reference number.  Not used.
					//2400 REF 9C (dental) Adjusted Repriced claim Number. Situational. We do not use.
					//2400 REF 9D (inst) Adjusted repriced line item reference Number.  Not used.
					//2400 REF 6R (inst,dental) Line Item Control Number (ProcNum).
					//will later be used for ERAs
					seg++;
					sw.WriteLine("REF*6R*"//REF01 2/3 Reference Identification Qualifier: 6R=Procedure Control Number.
						+proc.ProcNum.ToString()//REF02 1/50 Reference Identification: 
						+"~");//REF03 and REF04 are not used.
					#endregion Service REF
					#region Service AMT K3 NTE PS1 HCP LIN CTP
					//2400 AMT: T (dental) Sales Tax Amount. Situational. Not supported.
					//2400 AMT GT (inst) Service Tax Amount. Not supported.
					//2400 AMT N8 (inst) Facility Tax Amount. Not supported.
					//2400 K3: (medical,dental) File Information. Situational. Not supported.
					//2400 NTE TPO (inst) Third Party Organization Notes. Not sent by providers. Not supported.
					//2400 PS1 (medical)
					//2400 HCP: (medical,inst,dental) Line Pricing/Repricing Information. Not used by providers. Not supported.
					#endregion Service AMT K3 NTE PS1 HCP
					#region 2410 Service Drug Identification
					//2410 LIN,CTP,REF: (medical) ?
					if(medType==EnumClaimMedType.Institutional) {
						//2410 LIN (inst) Drug Identification
						if(procCode.DrugNDC!="" && proc.DrugQty>0){
							seg++;
							sw.WriteLine("LIN**"//LIN01 not used
								+"N4*"//LIN02 2/2 N4=NDC code in 5-4-2 format, no dashes.
								+procCode.DrugNDC+"~");//LIN03 1/48 NDC
							//2410 CTP (inst) Drug Quantity
							seg++;
							sw.WriteLine("CTP****"//CTP01-3 not used
								+proc.DrugQty.ToString()+"*"//CTP04 Quantity
								+GetDrugUnitCode(proc.DrugUnit)+"~");//CTP05-1 2/2 Code Qualifier, validated to not be None.
							//2410 REF (inst) Rx or compound drug association number.  Not supported.
						}
					}
					#endregion 2410 Service Drug Identification
					#region 2420 Service Providers (medical)
					if(medType==EnumClaimMedType.Medical) {

					}
					#endregion 2420 Service Providers (medical)
					#region 2420 Service Providers (inst)
					if(medType==EnumClaimMedType.Institutional) {
						//2420A (inst) Operating Physician
						//Only for surgical procedures. We don't support
						//2420B (inst) Other Operating Physician
						//we don't support
						//2420C (inst) Rendering Provider
						//Only if different than claim attending (treating) prov.
						//2420C (inst) NM1: Rendering provider name. 
						if(claim.ProvTreat!=proc.ProvNum
							&& PrefC.GetBool(PrefName.EclaimsSeparateTreatProv)) 
						{
							provTreat=Providers.GetProv(proc.ProvNum);
							//2420C (inst) NM1 Name
							seg++;
							sw.Write("NM1*82*"//NM101 82=rendering prov
								+"1*"//NM102: 1=person, validated
								+Sout(provTreat.LName,60)+"*"//NM103: LName
								+Sout(provTreat.FName,35)+"*"//NM104: FName
								+Sout(provTreat.MI,25)+"*"//NM105: MiddleName
								+"*"//NM106: not used.
								+"*");//NM107: suffix. not supported.
							sw.Write("XX*");//NM108: XX=NPI
							sw.Write(Sout(provTreat.NationalProvID,80));//NM109: ID.  NPI validated.
							sw.WriteLine("~");
							//2420C (inst) REF: Rendering provider secondary ID. 
							seg++;
							sw.WriteLine("REF*0B*"//REF01: 0B=state license #
								+Sout(provTreat.StateLicense,50)+"*");//REF02 valided to be present
						}
						//2420D (inst) Referring Provider
						//2430 (inst) Line Adjudication
					}
					#endregion 2420 Service Providers (inst)
					#region 2420 Service Providers (dental)
					if(medType==EnumClaimMedType.Dental) {
						//2420A (dental) Rendering Provider.
						//Only if different from the claim.
						//2420A NM1: Rendering Provider Name.
						if(claim.ProvTreat!=proc.ProvNum
							&& PrefC.GetBool(PrefName.EclaimsSeparateTreatProv)) 
						{
							provTreat=Providers.GetProv(proc.ProvNum);
							seg++;
							sw.Write("NM1*82*"//NM101 2/3 Entity Identifier Code: 82=Rendering Provider.
								+"1*"//NM102 1/1 Entity Type Qualifier: 1=Person.
								+Sout(provTreat.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
								+Sout(provTreat.FName,35)+"*"//NM104 1/35 Name First:
								+Sout(provTreat.MI,25)+"*"//NM105 1/25 Name Middle:
								+"*"//NM106 1/10 Name Prefix: Not Used.
								+"*");//NM107 1/10 Name Suffix: Situational. Not Supported.
							//After NPI date, so always do it one way:
							sw.Write("XX*");//NM108 1/2 Identification Code Qualifier: XX=NPI.
							sw.Write(Sout(provTreat.NationalProvID,80));//NM109 2/80 Identification Code: NPI validated.
							sw.WriteLine("~");//NM110 through NM112 not used.
							//2420A PRV: Rendering Provider Specialty Information.
							seg++;
							sw.Write("PRV*PE*");//PRV01 1/3 Provider Code: PE=Performing.
							sw.Write("PXC*");//PRV02 2/3 Reference Identification Qualifier: PXC=Health Care Provider Taxonomy Code.
							sw.WriteLine(X12Generator.GetTaxonomy(provTreat)//PRV03 1/50 Reference Identification: Taxonomy Code.
								+"~");//PRV04 through PRV06 not used.
							//2420A REF: Rendering Provider Secondary Identification.
							seg++;
							sw.WriteLine("REF*0B*"//REF01 2/3 Reference Identification Qualifier: 0B=State License Number.
								+Sout(provTreat.StateLicense,50)+"*"//REF02 1/50 Reference Identification: 
								+"*"//REF03 1/80 Description: Not Used.
								+"~");//REF04 Reference Identifier: Situational. Not used when REF01 is 0B or 1G.
						}
						//2420B (dental) Assistant Surgeon
						//we don't support
						//2420C (dental) Supervising Provider
						//we don't support
						//2420D (dental) Service Facility Location
						//we enforce all procs on a claim being performed at the same location
						//2430 (dental) Line Adjudication Information
						//we don't support
					}
					#endregion 2420 Service Providers (dental)
				}
			}
			#region Trailers
			//Transaction Trailer
			seg++;
			sw.WriteLine("SE*"
				+seg.ToString()+"*"//SE01: Total segments, including ST & SE
				+transactionNum.ToString().PadLeft(4,'0')+"~");
			//Functional Group Trailer
			sw.WriteLine("GE*"+transactionNum.ToString()+"*"//GE01 1/6 Number of Transaction Sets Included:
				+groupControlNumber+"~");//GE02 1/9 Group Control Number: Must be identical to GS06.
			#endregion Trailers
		}

		///<summary>Sometimes writes the name information for Open Dental. Sometimes it writes practice info.</summary>
		private static void Write1000A_NM1(StreamWriter sw,Clearinghouse clearhouse) {
			if(clearhouse.SenderTIN=="") {//use OD
				sw.WriteLine("NM1*41*"//NM101 2/3 Entity Indentifier Code: 41=submitter
					+"2*"//NM102 1/1 Entity Type Qualifier: 2=nonperson
					+"OPEN DENTAL SOFTWARE*"//NM103 1/60 Name Last or Organization Name: 
					+"*"//NM104 1/35 Name First: Situational.
					+"*"//NM105 1/25 Name Middle: Situational.
					+"*"//NM106 1/10 Name Prefix: Not Used.
					+"*"//NM107 1/10 Name Suffix: Not Used.
					+"46*"//NM108 1/2 Identification Code Qualifier: 46=ETIN
					+"810624427~");//NM109 2/80 Identification Code: ETIN#.
			}
			else {
				sw.WriteLine("NM1*41*"//NM101 2/3 Entity Indentifier Code: 41=submitter
					+"2*"//NM102 1/1 Entity Type Qualifier: 2=nonperson
					+Sout(clearhouse.SenderName,35,1)+"*"//NM103 1/60 Name Last or Organization Name: 
					+"*"//NM104 1/35 Name First: Situational.
					+"*"//NM105 1/25 Name Middle: Situational.
					+"*"//NM106 1/10 Name Prefix: Not Used.
					+"*"//NM107 1/10 Name Suffix: Not Used.
					+"46*"//NM108 1/2 Identification Code Qualifier: 46=ETIN
					+Sout(clearhouse.SenderTIN,80,2)+"~");//NM109 2/80 Identification Code: ETIN#. Validated to be at least 2.
			}
		}

		///<summary>Usually writes the contact information for Open Dental. But for inmediata and AOS clearinghouses, it writes practice contact info.</summary>
		private static void Write1000A_PER(StreamWriter sw,Clearinghouse clearhouse) {
			if(clearhouse.SenderTIN=="") {//use OD
				sw.WriteLine("PER*IC*"//PER01 2/2 Contact Function Code: IC=Information Contact.
					+"*"//PER02 1/60 Name: Situational. Do not send since same as in NM1 segment for loop 1000A.
					+"TE*"//PER03 2/2 Communication Number Qualifier: TE=Telephone.
					+"8776861248~");//PER04 1/256 Communication Number: Telephone Number.
				//PER05 through PER08 are situational and we are not allowed to send if not required.
				//PER09 is not used so we do not send.
			}
			else {
				sw.WriteLine("PER*IC*"//PER01 2/2 Contact Function Code: IC=Information Contact.
					+"*"//PER02 1/60 Name: Situational. Do not send since same as in NM1 segment for loop 1000A.
					+"TE*"//PER03 2/2 Communication Number Qualifier: TE=Telephone.
					+clearhouse.SenderTelephone+"~");//PER04 1/256 Communication Number: Telephone Number. Validated to be exactly 10 digits.
				//PER05 through PER08 are situational and we are not allowed to send if not required.
				//PER09 is not used so we do not send.
			}
		}

		///<summary>This is depedent only on the electronic payor id # rather than the clearinghouse.  Used for billing prov and also for treating prov. Returns the number of segments written</summary>
		private static int WriteProv_REF(StreamWriter sw,Provider prov,string payorID) {
			int retVal=0;
			ElectID electID=ElectIDs.GetID(payorID);
			if(electID!=null && electID.IsMedicaid) {
				retVal++;
				sw.WriteLine("REF*"
					+"1D*"//REF01 2/3 Reference Identification Qualifier: 1D=Medicaid.
					+Sout(prov.MedicaidID,50,1)//REF02 1/50 Reference Identification:
					+"~");//REF03 and REF04 are not used.
			}
			//I don't think there would be additional id's if Medicaid, but just in case, no return.
			ProviderIdent[] provIdents=ProviderIdents.GetForPayor(prov.ProvNum,payorID);
			for(int i=0;i<provIdents.Length;i++) {
				retVal++;
				sw.WriteLine("REF*"
					+GetProvTypeQualifier(provIdents[i].SuppIDType)+"*"//REF01 2/3 Reference Identification Qualifier: 
					+Sout(provIdents[i].IDNumber,50,1)//REF02 1/50 Reference Identification:
					+"~");//REF03 and REF04 are not used.
			}
			return retVal;
		}

		private static string GetProvTypeQualifier(ProviderSupplementalID provType) {
			switch(provType) {
				case ProviderSupplementalID.BlueCross:
					return "1A";
				case ProviderSupplementalID.BlueShield:
					return "1B";
				case ProviderSupplementalID.SiteNumber:
					return "G5";
				case ProviderSupplementalID.CommercialNumber:
					return "G2";
			}
			return "  ";
		}

		private static string GetGender(PatientGender patGender) {
			switch(patGender) {
				case PatientGender.Male:
					return "M";
				case PatientGender.Female:
					return "F";
				case PatientGender.Unknown:
					return "U";
			}
			return "";
		}

		private static string GetRelat(Relat relat) {
			switch(relat) {
				case (Relat.Self):
					return "18";
				case (Relat.Child):
					return "19";
				case (Relat.Dependent):
					return "76";
				case (Relat.Employee):
					return "20";
				case (Relat.HandicapDep):
					return "22";
				case (Relat.InjuredPlaintiff):
					return "41";
				case (Relat.LifePartner):
					return "53";
				case (Relat.SignifOther):
					return "29";
				case (Relat.Spouse):
					return "01";
			}
			return "";
		}

		private static string GetStudent(string studentStatus) {
			if(studentStatus=="P") {
				return "P";
			}
			if(studentStatus=="F") {
				return "F";
			}
			return "N";//either N or blank
		}

		private static string GetPlaceService(PlaceOfService place) {
			switch(place) {
				case PlaceOfService.AmbulatorySurgicalCenter:
					return "24";
				case PlaceOfService.CustodialCareFacility:
					return "33";
				case PlaceOfService.EmergencyRoomHospital:
					return "23";
				case PlaceOfService.FederalHealthCenter:
					return "50";
				case PlaceOfService.InpatHospital:
					return "21";
				case PlaceOfService.MilitaryTreatFac:
					return "26";
				case PlaceOfService.MobileUnit:
					return "15";
				case PlaceOfService.Office:
				case PlaceOfService.OtherLocation:
					return "11";
				case PlaceOfService.OutpatHospital:
					return "22";
				case PlaceOfService.PatientsHome:
					return "12";
				case PlaceOfService.PublicHealthClinic:
					return "71";
				case PlaceOfService.RuralHealthClinic:
					return "72";
				case PlaceOfService.School:
					return "03";
				case PlaceOfService.SkilledNursFac:
					return "31";
			}
			return "11";
		}

		private static string GetRelatedCauses(Claim claim) {
			if(claim.AccidentRelated=="") {
				return "";
			}
			//even though the specs let you submit all three types at once, we only allow one of the three
			if(claim.AccidentRelated=="A") {//auto accident
				return "AA:::"+Sout(claim.AccidentST,2,2);
			}
			else if(claim.AccidentRelated=="E") {//employment
				return "EM";
			}
			else {// if(claim.AccidentRelated=="O"){ //other accident
				return "OA";
			}
		}

		///<summary>Will return blank if no special code.</summary>
		private static string GetSpecialProgramCode(EnumClaimSpecialProgram code) {
			switch(code){
				default:
					return "";
				case EnumClaimSpecialProgram.EPSDT_1:
					return "1";
				case EnumClaimSpecialProgram.Handicapped_2:
					return "2";
				case EnumClaimSpecialProgram.SpecialFederal_3:
					return "3";
				case EnumClaimSpecialProgram.Disability_5:
					return "5";
			}
		}
		

		///<summary>This used to be an enumeration.</summary>
		private static string GetFilingCode(InsPlan plan) {
			string filingcode=InsFilingCodes.GetEclaimCode(plan.FilingCode);
			//must be one or two char in length.
			if(filingcode=="" || filingcode.Length>2) {
				return "CI";
			}
			return Sout(filingcode,2,1);
			/*
			switch(plan.FilingCode){
				case InsFilingCodeOld.SelfPay:
					return "09";
				case InsFilingCodeOld.OtherNonFed:
					return "11";
				case InsFilingCodeOld.PPO:
					return "12";
				case InsFilingCodeOld.POS:
					return "13";
				case InsFilingCodeOld.EPO:
					return "14";
				case InsFilingCodeOld.Indemnity:
					return "15";
				case InsFilingCodeOld.HMO_MedicareRisk:
					return "16";
				case InsFilingCodeOld.DMO:
					return "17";
				case InsFilingCodeOld.BCBS:
					return "BL";
				case InsFilingCodeOld.Champus:
					return "CH";
				case InsFilingCodeOld.Commercial_Insurance:
					return "CI";
				case InsFilingCodeOld.Disability:
					return "DS";
				case InsFilingCodeOld.FEP:
					return "FI";
				case InsFilingCodeOld.HMO:
					return "HM";
				case InsFilingCodeOld.LiabilityMedical:
					return "LM";
				case InsFilingCodeOld.MedicarePartB:
					return "MB";
				case InsFilingCodeOld.Medicaid:
					return "MC";
				case InsFilingCodeOld.ManagedCare_NonHMO:
					return "MH";
				case InsFilingCodeOld.OtherFederalProgram:
					return "OF";
				case InsFilingCodeOld.SelfAdministered:
					return "SA";
				case InsFilingCodeOld.Veterans:
					return "VA";
				case InsFilingCodeOld.WorkersComp:
					return "WC";
				case InsFilingCodeOld.MutuallyDefined:
					return "ZZ";
				default:
					return "CI";
			}
			*/
		}

		private static string GetArea(Procedure proc,ProcedureCode procCode) {
			if(procCode.TreatArea==TreatmentArea.Arch) {
				if(proc.Surf=="U") {
					return "01";
				}
				if(proc.Surf=="L") {
					return "02";
				}
			}
			if(procCode.TreatArea==TreatmentArea.Mouth) {
				return "";
			}
			if(procCode.TreatArea==TreatmentArea.Quad) {
				if(proc.Surf=="UR") {
					return "10";
				}
				if(proc.Surf=="UL") {
					return "20";
				}
				if(proc.Surf=="LR") {
					return "40";
				}
				if(proc.Surf=="LL") {
					return "30";
				}
			}
			if(procCode.TreatArea==TreatmentArea.Sextant) {
				//we will assume that these are very rarely billed to ins
				return "";
			}
			if(procCode.TreatArea==TreatmentArea.Surf) {
				return "";//might need to enhance this
			}
			if(procCode.TreatArea==TreatmentArea.Tooth) {
				return "";//might need to enhance this
			}
			if(procCode.TreatArea==TreatmentArea.ToothRange) {
				//already checked for blank tooth range
				if(Tooth.IsMaxillary(proc.ToothRange.Split(',')[0])) {
					return "01";
				}
				else {
					return "02";
				}
			}
			return "";
		}

		///<summary></summary>
		public static string GetDrugUnitCode(EnumProcDrugUnit drugUnit){
			switch(drugUnit){
				//case EnumProcDrugUnit.None://validated so it won't happen
				case EnumProcDrugUnit.Gram:
					return "GR";
				case EnumProcDrugUnit.InternationalUnit:
					return "F2";
				case EnumProcDrugUnit.Milligram:
					return "ME";
				case EnumProcDrugUnit.Milliliter:
					return "ML";
				case EnumProcDrugUnit.Unit:
					return "UN";
				default:
					return "UN";//just in case
			}
		}

		///<summary>Converts any string to an acceptable format for X12. Converts to all caps and strips off all invalid characters. Optionally shortens the string to the specified length and/or makes sure the string is long enough by padding with spaces.</summary>
		private static string Sout(string intputStr,int maxL,int minL) {
			string retStr=intputStr.ToUpper();
			//Debug.Write(retStr+",");
			retStr=Regex.Replace(retStr,//replaces characters in this input string
				//Allowed: !"&'()+,-./;?=(space)#   # is actually part of extended character set
				"[^\\w!\"&'\\(\\)\\+,-\\./;\\?= #]",//[](any single char)^(that is not)\w(A-Z or 0-9) or one of the above chars.
				"");
			retStr=Regex.Replace(retStr,"[_]","");//replaces _
			retStr=retStr.Trim();//removes leading and trailing spaces.
			if(maxL!=-1) {
				if(retStr.Length>maxL) {
					retStr=retStr.Substring(0,maxL);
				}
			}
			if(minL!=-1) {
				if(retStr.Length<minL) {
					retStr=retStr.PadRight(minL,' ');
				}
			}
			//Debug.WriteLine(retStr);
			return retStr;
		}

		///<summary>Converts any string to an acceptable format for X12. Converts to all caps and strips off all invalid characters. Optionally shortens the string to the specified length and/or makes sure the string is long enough by padding with spaces.</summary>
		private static string Sout(string str,int maxL) {
			return Sout(str,maxL,-1);
		}

		///<summary>Converts any string to an acceptable format for X12. Converts to all caps and strips off all invalid characters. Optionally shortens the string to the specified length and/or makes sure the string is long enough by padding with spaces.</summary>
		private static string Sout(string str) {
			return Sout(str,-1,-1);
		}

		///<summary>Returns a string describing all missing data on this claim.  Claim will not be allowed to be sent electronically unless this string comes back empty.  There is also an out parameter containing any warnings.  Warnings will not block sending.</summary>
		public static string Validate(ClaimSendQueueItem queueItem,out string warning) {
			StringBuilder strb=new StringBuilder();
			warning="";
			Clearinghouse clearhouse=null;//ClearinghouseL.GetClearinghouse(queueItem.ClearinghouseNum);
			for(int i=0;i<Clearinghouses.Listt.Length;i++) {
				if(Clearinghouses.Listt[i].ClearinghouseNum==queueItem.ClearinghouseNum) {
					clearhouse= Clearinghouses.Listt[i];
				}
			}
			if(clearhouse==null) {
				throw new ApplicationException("Error. Could not locate Clearinghouse.");
			}
			Claim claim=Claims.GetClaim(queueItem.ClaimNum);
			Clinic clinic=Clinics.GetClinic(claim.ClinicNum);
			//if(clearhouse.Eformat==ElectronicClaimFormat.X12){//not needed since this is always true
			X12Validate.ISA(clearhouse,strb);
			if(clearhouse.GS03.Length<2) {
				if(strb.Length!=0) {
					strb.Append(",");
				}
				strb.Append("Clearinghouse GS03");
			}
			List<X12TransactionItem> claimItems=Claims.GetX12TransactionInfo(((ClaimSendQueueItem)queueItem).ClaimNum);//just to get prov. Needs work.
			Provider billProv=ProviderC.ListLong[Providers.GetIndexLong(claimItems[0].ProvBill1)];
			Provider treatProv=ProviderC.ListLong[Providers.GetIndexLong(claim.ProvTreat)];
			InsPlan insPlan=InsPlans.GetPlan(claim.PlanNum,null);
			InsSub sub=InsSubs.GetSub(claim.InsSubNum,null);
			//if(insPlan.IsMedical) {
			//	return "Medical e-claims not allowed";
			//}
			//billProv
			//bool isPerson=(billProv.FName!="");//this was our old hack for indicating a person
			X12Validate.BillProv(billProv,strb,!billProv.IsNotPerson);
			//treatProv
			if(treatProv.LName=="") {
				Comma(strb);
				strb.Append("Treating Prov LName");
			}
			if(treatProv.FName=="") {
				Comma(strb);
				strb.Append("Treating Prov FName");
			}
			if(treatProv.SSN.Length<2) {
				Comma(strb);
				strb.Append("Treating Prov SSN");
			}
			if(treatProv.NationalProvID.Length<2) {
				Comma(strb);
				strb.Append("Treating Prov NPI");
			}
			if(treatProv.StateLicense=="") {
				Comma(strb);
				strb.Append("Treating Prov Lic #");
			}
			//if(insPlan.IsMedical) {
			if(treatProv.NationalProvID.Length<2) {
				Comma(strb);
				strb.Append("Treating Prov NPI");
			}
			//}
			if(PrefC.GetString(PrefName.PracticeTitle)=="") {
				Comma(strb);
				strb.Append("Practice Title");
			}
			if(clinic==null) {
				X12Validate.PracticeAddress(strb);
			}
			else {
				X12Validate.Clinic(clinic,strb);
			}      
			if(!sub.ReleaseInfo) {
				Comma(strb);
				strb.Append("InsPlan Release of Info");
			}
			Carrier carrier=Carriers.GetCarrier(insPlan.CarrierNum);
			X12Validate.Carrier(carrier,strb);
			ElectID electID=ElectIDs.GetID(carrier.ElectID);
			if(electID!=null && electID.IsMedicaid && billProv.MedicaidID=="") {
				Comma(strb);
				strb.Append("BillProv Medicaid ID");
			}
			if(claim.PlanNum2>0) {
				InsPlan insPlan2=InsPlans.GetPlan(claim.PlanNum2,new List<InsPlan>());
				InsSub sub2=InsSubs.GetSub(claim.InsSubNum2,null);
				Carrier carrier2=Carriers.GetCarrier(insPlan2.CarrierNum);
				if(carrier2.Address=="") {
					Comma(strb);
					strb.Append("Secondary Carrier Address");
				}
				if(carrier2.City.Length<2) {
					Comma(strb);
					strb.Append("Secondary Carrier City");
				}
				if(carrier2.State.Length!=2) {
					Comma(strb);
					strb.Append("Secondary Carrier State(2 char)");
				}
				if(carrier2.Zip.Length<3) {
					Comma(strb);
					strb.Append("Secondary Carrier Zip");
				}
				if(claim.PatNum != sub2.Subscriber//if patient is not subscriber
					&& claim.PatRelat2==Relat.Self) {//and relat is self
					Comma(strb);
					strb.Append("Secondary Relationship");
				}
			}
			//Provider Idents:
			/*ProviderSupplementalID[] providerIdents=ElectIDs.GetRequiredIdents(carrier.ElectID);
				//No longer any required supplemental IDs
			for(int i=0;i<providerIdents.Length;i++){
				if(!ProviderIdents.IdentExists(providerIdents[i],billProv.ProvNum,carrier.ElectID)){
					if(retVal!="")
						strb.Append(",";
					strb.Append("Billing Prov Supplemental ID:"+providerIdents[i].ToString();
				}
			}*/
			if(sub.SubscriberID.Length<2) {
				Comma(strb);
				strb.Append("SubscriberID");
			}
			Patient patient=Patients.GetPat(claim.PatNum);
			Patient subscriber=Patients.GetPat(sub.Subscriber);
			if(claim.PatNum != sub.Subscriber//if patient is not subscriber
				&& claim.PatRelat==Relat.Self) {//and relat is self
				Comma(strb);
				strb.Append("Claim Relationship");
			}
			if(patient.Address=="") {
				Comma(strb);
				strb.Append("Patient Address");
			}
			if(patient.City.Length<2) {
				Comma(strb);
				strb.Append("Patient City");
			}
			if(patient.State.Length!=2) {
				Comma(strb);
				strb.Append("Patient State");
			}
			if(patient.Zip.Length<3) {
				Comma(strb);
				strb.Append("Patient Zip");
			}
			if(patient.Birthdate.Year<1880) {
				Comma(strb);
				strb.Append("Patient Birthdate");
			}
			if(claim.AccidentRelated=="A" && claim.AccidentST.Length!=2) {//auto accident with no state
				Comma(strb);
				strb.Append("Auto accident State");
			}
			/*if(clearhouse.ISA08=="113504607" && claim.Attachments.Count>0) {//If Tesia and has attachments
				string storedFile;
				for(int c=0;c<claim.Attachments.Count;c++) {
					storedFile=ODFileUtils.CombinePaths(FormEmailMessageEdit.GetAttachPath(),claim.Attachments[c].ActualFileName);
					if(!File.Exists(storedFile)){
						if(retVal!="")
							strb.Append(",";
						strb.Append("attachments missing";
						break;
					}
				}
			}*/
			//Warning if attachments are listed as Mail even though we are sending electronically.
			bool pwkNeeded=false;
			if(claim.AttachedFlags!="Mail") {//in other words, if there are additional flags.
				pwkNeeded=true;
			}
			if(claim.Radiographs>0 || claim.AttachedImages>0 || claim.AttachedModels>0) {
				pwkNeeded=true;
			}
			if(claim.AttachedFlags.Contains("Mail") && pwkNeeded) {
				if(warning!="")
					warning+=",";
				warning+="Attachments set to Mail";
			}
			//Warning if any PWK segments are needed, and there is no ID code.
			if(pwkNeeded && claim.AttachmentID=="") {
				if(warning!="")
					warning+=",";
				warning+="Attachment ID missing";
			}
			if(claim.MedType==EnumClaimMedType.Institutional) {
				if(claim.UniformBillType.Length!=3) {
					Comma(strb);
					strb.Append("BillType");
				}
				if(claim.AdmissionTypeCode.Length!=1) {
					Comma(strb);
					strb.Append("AdmissionType");
				}
				if(claim.AdmissionSourceCode.Length!=1) {
					Comma(strb);
					strb.Append("AdmissionSource");
				}
				if(claim.PatientStatusCode.Length!=2) {
					Comma(strb);
					strb.Append("PatientStatusCode");
				}
			}
			if(claim.MedType==EnumClaimMedType.Institutional) {
//todo js 9/11/11 why is this just institutional?
				if(claim.DateService.Year<1880) {
					Comma(strb);
					strb.Append("DateService");
				}
			}
			if(claim.MedType==EnumClaimMedType.Institutional
				|| claim.MedType==EnumClaimMedType.Medical) 
			{
				if(claim.PreAuthString!="") {
					Comma(strb);
					strb.Append("Predeterm number not allowed");
				}
			}
			List<ClaimProc> claimProcList=ClaimProcs.Refresh(patient.PatNum);
			List<ClaimProc> claimProcs=ClaimProcs.GetForSendClaim(claimProcList,claim.ClaimNum);
			List<Procedure> procList=Procedures.Refresh(claim.PatNum);
			Procedure proc;
			ProcedureCode procCode;
			bool princDiagExists=false;
			for(int i=0;i<claimProcs.Count;i++) {
				string p="proc"+(i+1).ToString()+"-";
				proc=Procedures.GetProcFromList(procList,claimProcs[i].ProcNum);
				procCode=ProcedureCodes.GetProcCode(proc.CodeNum);
				if(claim.MedType==EnumClaimMedType.Medical) {
					if(proc.DiagnosticCode=="") {
						Comma(strb);
						strb.Append(procCode.AbbrDesc+"Procedure Diagnosis");
					}
					if(proc.IsPrincDiag && proc.DiagnosticCode!="") {
						princDiagExists=true;
					}
				}
				else if(claim.MedType==EnumClaimMedType.Institutional) {
					if(proc.RevCode==""){
						Comma(strb);
						strb.Append(p+"RevenueCode");
					}
					if(proc.CodeMod1.Length!=0 && proc.CodeMod1.Length!=2){
						Comma(strb);
						strb.Append(procCode.AbbrDesc+" mod1");
					}
					if(proc.CodeMod2.Length!=0 && proc.CodeMod2.Length!=2){
						Comma(strb);
						strb.Append(procCode.AbbrDesc+" mod2");
					}
					if(proc.CodeMod3.Length!=0 && proc.CodeMod3.Length!=2){
						Comma(strb);
						strb.Append(procCode.AbbrDesc+" mod3");
					}
					if(proc.CodeMod4.Length!=0 && proc.CodeMod4.Length!=2){
						Comma(strb);
						strb.Append(procCode.AbbrDesc+" mod4");
					}
					if(procCode.DrugNDC!="" && proc.DrugQty>0){
						if(proc.DrugUnit==EnumProcDrugUnit.None){
							Comma(strb);
							strb.Append(procCode.AbbrDesc+" drug unit");
						}
					}
				}
				else if(claim.MedType==EnumClaimMedType.Dental) {
					if(procCode.TreatArea==TreatmentArea.Arch && proc.Surf=="") {
						Comma(strb);
						strb.Append(procCode.AbbrDesc+" missing arch");
					}
					if(procCode.TreatArea==TreatmentArea.ToothRange && proc.ToothRange=="") {
						Comma(strb);
						strb.Append(procCode.AbbrDesc+" tooth range");
					}
					if((procCode.TreatArea==TreatmentArea.Tooth || procCode.TreatArea==TreatmentArea.Surf)
						&& !Tooth.IsValidDB(proc.ToothNum)) 
					{
						Comma(strb);
						strb.Append(procCode.AbbrDesc+" tooth number");
					}
					if(procCode.IsProsth) {
						if(proc.Prosthesis=="") {//they didn't enter whether Initial or Replacement
							Comma(strb);
							strb.Append(procCode.AbbrDesc+" Prosthesis");
						}
						if(proc.Prosthesis=="R"	&& proc.DateOriginalProsth.Year<1880) {//if a replacement, they didn't enter a date
							Comma(strb);
							strb.Append(procCode.AbbrDesc+" Prosth Date");
						}
					}
				}
				//Providers
				if(claim.ProvTreat!=proc.ProvNum && PrefC.GetBool(PrefName.EclaimsSeparateTreatProv)) {
					treatProv=ProviderC.ListLong[Providers.GetIndexLong(proc.ProvNum)];
					if(treatProv.LName=="") {
						Comma(strb);
						strb.Append("Treating Prov LName");
					}
					if(treatProv.FName=="") {
						Comma(strb);
						strb.Append("Treating Prov FName");
					}
					if(treatProv.IsNotPerson) {
						Comma(strb);
						strb.Append("Treating Prov IsNotPerson");//required to be a person
					}
					if(treatProv.SSN.Length<2) {
						Comma(strb);
						strb.Append("Treating Prov SSN");
					}
					if(treatProv.NationalProvID.Length<2) {
						Comma(strb);
						strb.Append("Treating Prov NPI");
					}
					if(treatProv.StateLicense=="") {
						Comma(strb);
						strb.Append("Treating Prov Lic #");
					}
					//will add any other checks as needed. Can't think of any others at the moment.
				}
			}//for int i claimProcs
			if(insPlan.IsMedical && !princDiagExists) {
				Comma(strb);
				strb.Append("Princ Diagnosis");
			}

			/*
						if(==""){
							Comma(strb);
							strb.Append("";
						}*/

			return strb.ToString();
		}

		private static void Comma(StringBuilder strb){
			if(strb.Length!=0) {
				strb.Append(", ");
			}
		}
		
		///<summary>Loops through the 837 to find the transaction number for the specified claim. Will return 0 if can't find.</summary>
		public int GetTransNum(long claimNum) {
			string curTransNumStr="";
			for(int i=0;i<Segments.Count;i++) {
				if(Segments[i].SegmentID=="ST"){
					curTransNumStr=Segments[i].Get(2);
				}
				if(Segments[i].SegmentID=="CLM"){
					if(Segments[i].Get(1).TrimStart(new char[] {'0'})==claimNum.ToString()){//if for specified claim
						try {
							return PIn.Int(curTransNumStr);
						}
						catch {
							return 0;
						}
					}
				}
			}
			return 0;
		}

		///<summary>Loops through the 837 to see if attachments were sent.</summary>
		public bool AttachmentsWereSent(long claimNum) {
			bool isCurrentClaim=false;
			for(int i=0;i<Segments.Count;i++) {
				if(Segments[i].SegmentID=="CLM") {
					if(Segments[i].Get(1).TrimStart(new char[] { '0' })==claimNum.ToString()) {//if for specified claim
						isCurrentClaim=true;
					}
					else{
						isCurrentClaim=false;
					}
				}
				if(Segments[i].SegmentID=="PWK" && isCurrentClaim) {
					return true;
				}
			}
			return false;
		}
		


	}
}
