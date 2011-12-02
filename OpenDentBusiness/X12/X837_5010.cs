using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness
{ 
	///<summary></summary>
	public class X837_5010:X12object{

		///<summary>Data element separator character. Almost always '*', the ASCII hexadecimal value of 2A. For Denti-Cal, ASCII hexadecimal value of 1D which is an unprintable character.</summary>
		private static char s='*';
		///<summary>Component element separator character. Almost always ':', the ASCII hexadecimal value of 3A. For Denti-Cal, ASCII hexadecimal value of 22 which is '"'.</summary>
		private static char isa16=':';
		///<summary>Segment terminator character. Almost always '~', the ASCII hexadecimal value of 7E. For Denti-Cal, ASCII hexadecimal value of 1C which is an unprintable character.</summary>
		private static char endSegment='~';

		public X837_5010(string messageText):base(messageText){
		
		}
		
		public static void GenerateMessageText(StreamWriter sw,Clearinghouse clearhouse,int batchNum,List<ClaimSendQueueItem> listQueueItems,EnumClaimMedType medType) {
			if(clearhouse.SeparatorData=="") {
				s='*';
			}
			else {
				s=Encoding.ASCII.GetChars(new byte[] { Convert.ToByte(clearhouse.SeparatorData,16) })[0]; //Validated to be a 2 digit hexadecimal number in UI.
			}
			if(clearhouse.ISA16=="") {
				isa16=':';
			}
			else {
				isa16=Encoding.ASCII.GetChars(new byte[] { Convert.ToByte(clearhouse.ISA16,16) })[0]; //Validated to be a 2 digit hexadecimal number in UI.
			}
			if(clearhouse.SeparatorSegment=="") {
				endSegment='~';
			}
			else {
				endSegment=Encoding.ASCII.GetChars(new byte[] { Convert.ToByte(clearhouse.SeparatorSegment,16) })[0]; //Validated to be a 2 digit hexadecimal number in UI.
			}
			//Interchange Control Header (Interchange number tracked separately from transactionNum)
			//We set it to between 1 and 999 for simplicity
			sw.WriteLine("ISA"+s
				+"00"+s//ISA01 2/2 Authorization Information Qualifier: 00=No Authorization Information Present (No meaningful information in ISA02).
				+Sout(clearhouse.ISA02,10,10)+s//ISA02 10/10 Authorization Information: Blank
				+"00"+s//ISA03 2/2 Security Information Qualifier: 00=No Security Information Present (No meaningful information in ISA04).
				+Sout(clearhouse.ISA04,10,10)+s//ISA04 10/10 Security Information: Blank
				+clearhouse.ISA05+s//ISA05 2/2 Interchange ID Qualifier: ZZ=mutually defined. 30=TIN. Validated
				+X12Generator.GetISA06(clearhouse)+s//ISA06 15/15 Interchange Sender ID: Sender ID(TIN) Or might be TIN of Open Dental.
				+clearhouse.ISA07+s//ISA07 2/2 Interchange ID Qualifier: ZZ=mutually defined. 30=TIN. Validated
				+Sout(clearhouse.ISA08,15,15)+s//ISA08 15/15 Interchange Receiver ID: Validated to make sure length is at least 2.
				+DateTime.Today.ToString("yyMMdd")+s//ISA09 6/6 Interchange Date: today's date.
				+DateTime.Now.ToString("HHmm")+s//ISA10 4/4 Interchange Time: current time
				+"^"+s//ISA11 1/1 Repetition Separator:
				+"00501"+s//ISA12 5/5 Interchange Control Version Number:
				+batchNum.ToString().PadLeft(9,'0')+s//ISA13 9/9 Interchange Control Number:
				+"0"+s//ISA14 1/1 Acknowledgement Requested: 0=No Interchange Acknowledgment Requested.
				+clearhouse.ISA15+s//ISA15 1/1 Interchange Usage Indicator: T=Test, P=Production. Validated.
				+isa16//ISA16 1/1 Component Element Separator:
				+endSegment);
			//Just one functional group.
			WriteFunctionalGroup(sw,listQueueItems,batchNum,clearhouse,medType);
			//Interchange Control Trailer
			sw.WriteLine("IEA"+s
				+"1"+s//IEA01 1/5 Number of Included Functional Groups:
				+batchNum.ToString().PadLeft(9,'0')//IEA02 9/9 Interchange Control Number:
				+endSegment);
		}

		private static void WriteFunctionalGroup(StreamWriter sw,List<ClaimSendQueueItem> queueItems,int batchNum,Clearinghouse clearhouse,EnumClaimMedType medType) {
			#region Functional Group Header
			int transactionNum=1;//Gets incremented for each carrier. Can be reused in other functional groups and interchanges, so not persisted
			//Functional Group Header
			string groupControlNumber=batchNum.ToString();//Must be unique within file.  We will use batchNum
			string industryIdentifierCode="";
			if(medType==EnumClaimMedType.Medical) {
				industryIdentifierCode="005010X222A1";
			}
			else if(medType==EnumClaimMedType.Institutional) {
				industryIdentifierCode="005010X223A2";
			}
			else if(medType==EnumClaimMedType.Dental) {
				industryIdentifierCode="005010X224A2";
			}
			sw.WriteLine("GS*HC*"//GS01 2/2 Functional Identifier Code: Health Care Claim.
				+X12Generator.GetGS02(clearhouse)+"*"//GS02 2/15 Application Sender's Code: Sometimes Jordan Sparks.  Sometimes the sending clinic.
				+Sout(clearhouse.GS03,15,2)+"*"//GS03 2/15 Application Receiver's Code:
				+DateTime.Today.ToString("yyyyMMdd")+"*"//GS04 8/8 Date: today's date.
				+DateTime.Now.ToString("HHmm")+"*"//GS05 4/8 TIME: current time.
				+groupControlNumber+"*"//GS06 1/9 Group Control Number: No padding necessary.
				+"X*"//GS07 1/2 Responsible Agency Code: X=Accredited Standards Committee X12.
				+industryIdentifierCode//GS08 1/12 Version/Release/Industry Identifier Code:
				+endSegment);
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
			sw.WriteLine("ST*837*"//ST01 3/3 Transaction Set Identifier Code: 
				+transactionNum.ToString().PadLeft(4,'0')+"*"//ST02 4/9 Transaction Set Control Number: 
				+industryIdentifierCode//ST03 1/35 Implementation Convention Reference:
				+endSegment);
			seg++;
			sw.WriteLine("BHT*0019*"//BHT01 4/4 Hierarchical Structure Code: 0019=Information Source, Subscriber, Dependant.
				+"00*"//BHT02 2/2 Transaction Set Purpose Code: 00=Original transmissions are transmissions which have never been sent to the reciever.
				+transactionNum.ToString().PadLeft(4,'0')+"*"//BHT03 1/50 Reference Identification: Can be same as ST02.
				+DateTime.Now.ToString("yyyyMMdd")+"*"//BHT04 8/8 Date: 
				+DateTime.Now.ToString("HHmmss")+"*"//BHT05 4/8 Time: 
				+"CH"//BHT06 2/2 Transaction Type Code: CH=Chargable.
				+endSegment);
			//1000A Submitter is OPEN DENTAL and sometimes it's the practice
			//(depends on clearinghouse and Partnership agreements)
			//See 2010AA PER (after REF) for the new billing provider contact phone number
			//1000A NM1: 41 (medical,institutional,dental) Submitter Name.
			seg++;
			Write1000A_NM1(sw,clearhouse);
			//1000A PER: IC (medical,institutional,dental) Submitter EDI Contact Information. Contact number.
			seg++;
			Write1000A_PER(sw,clearhouse);
			//1000B NM1: 40 (medical,institutional,dental) Receiver Name. Always the Clearinghouse.
			seg++;
			sw.WriteLine("NM1*40*"//NM101 2/3 Entity Identifier Code: 40=Receiver.
				+"2*"//NM102 1/1 Entity Type Qualifier: 2=Non-Person Entity.
				+Sout(clearhouse.Description,60)+"*"//NM103 1/60 Name Last or Organization Name: Receiver Name.
				+"*"//NM104 1/35 Name First: Not Used.
				+"*"//NM105 1/25 Name Middle: Not Used.
				+"*"//NM106 1/10 Name Prefix: Not Used.
				+"*"//NM107 1/10 Name Suffix: Not Used.
				+"46*"//NM108 1/2 Identification Code Qualifier: 46=Electronic Transmitter Identification Number (ETIN).
				+Sout(clearhouse.ISA08,80,2)//NM109 2/80 Identification Code: Receiver ID Code. aka ETIN#.
				+endSegment);//NM110 through NM112 are not used.
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
				//2000A HL: (medical,instituational,dental) Billing Provider Hierarchical Level.
				seg++;
				sw.WriteLine("HL*"+HLcount.ToString()+"*"//HL01 1/12 Heirarchical ID Number: 
					+"*"//HL02 1/12 Hierarchical Parent ID Number: Not used.
					+"20*"//HL03 1/2 Heirarchical Level Code: 20=Information Source.
					+"1"//HL04 1/1 Heirarchical Child Code. 1=Additional Subordinate HL Data Segment in This Hierarchical Structure.
					+endSegment);
				//billProv=ProviderC.ListLong[Providers.GetIndexLong(claimItems[i].ProvBill1)];
				billProv=Providers.GetProv(claim.ProvBill);
				//2000A PRV: BI (medical,institutional,dental) Billing Provider Specialty Information. Situational. Required when billing provider is treating provider.
				if(claim.ProvBill==claim.ProvTreat) {
					seg++;
					sw.WriteLine("PRV*BI*"//PRV01 1/3 Provider Code: BI=Billing.
						+"PXC*"//PRV02 2/3 Reference Identification Qualifier: PXC=Health Care Provider Taxonomy Code.
						+X12Generator.GetTaxonomy(billProv)//PRV03 1/50 Reference Identification: Provider Taxonomy Code.
						+endSegment);//PRV04 through PRV06 are not used.
				}
				//2000A CUR: (medical,instituational,dental) Foreign Currency Information. Situational. We do not need to specify because united states dollars are default.
				//2010AA NM1: 85 (medical,institutional,dental) Billing Provider Name.
				seg++;
				sw.Write("NM1*85*");//NM101 2/3 Entity Identifier Code: 85=Billing Provider.
				if(medType==EnumClaimMedType.Institutional) {
					sw.Write("2*");//NM102 1/1 Entity Type Qualifier: 2=Non-Person Entity.
				}
				else { //(medical,dental)
					sw.Write((billProv.IsNotPerson?"2":"1")+"*");//NM102 1/1 Entity Type Qualifier: 1=Person, 2=Non-Person Entity.
				}
				sw.Write(Sout(billProv.LName,60)+"*");//NM103 1/60 Name Last or Organization Name:
				if(medType==EnumClaimMedType.Institutional) {
					sw.Write("*"//NM104 1/35 Name First: Not used.
						+"*");//NM105 1/25 Name Middle: Not used.
				}
				else { //(medical,dental)
					sw.Write(Sout(billProv.FName,35)+"*"//NM104 1/35 Name First: Situational. Required when NM102=1. Might be blank.
						+Sout(billProv.MI,25)+"*");//NM105 1/25 Name Middle: Since this is situational there is no minimum length.
				}
				sw.WriteLine("*"//NM106 1/10 Name Prefix: Not Used.
					+"*"//NM107 1/10 Name Suffix: Not Used in instituational. Situational in medical and dental, but we don't support.
					+"XX*"//NM108 1/2 Identification Code Qualifier: XX=Centers for Medicare and Medicaid Services National Provider Identifier (NPI).
					+Sout(billProv.NationalProvID,80)//NM109 2/80 Identification Code: NPI. Validated.
					+endSegment);//NM110 through NM112 Not Used.
				//2010AA N3: (medical,institutional,dental) Billing Provider Address.
				seg++;
				string billingAddress1="";
				string billingAddress2="";
				string billingCity="";
				string billingState="";
				string billingZip="";
				if(PrefC.GetBool(PrefName.UseBillingAddressOnClaims)) {
					billingAddress1=PrefC.GetString(PrefName.PracticeBillingAddress);
					billingAddress2=PrefC.GetString(PrefName.PracticeBillingAddress2);
					billingCity=PrefC.GetString(PrefName.PracticeBillingCity);
					billingState=PrefC.GetString(PrefName.PracticeBillingST);
					billingZip=PrefC.GetString(PrefName.PracticeBillingZip);
				}
				else if(clinic==null) {
					billingAddress1=PrefC.GetString(PrefName.PracticeAddress);
					billingAddress2=PrefC.GetString(PrefName.PracticeAddress2);
					billingCity=PrefC.GetString(PrefName.PracticeCity);
					billingState=PrefC.GetString(PrefName.PracticeST);
					billingZip=PrefC.GetString(PrefName.PracticeZip);
				}
				else {
					billingAddress1=clinic.Address;
					billingAddress2=clinic.Address2;
					billingCity=clinic.City;
					billingState=clinic.State;
					billingZip=clinic.Zip;
				}
				sw.Write("N3*"+Sout(billingAddress1,55));//N301 1/55 Address Information:
				if(billingAddress2!="") {
					sw.Write("*"+Sout(billingAddress2,55));//N302 1/55 Address Information:
				}
				sw.WriteLine(endSegment);
				//2010AA N4: (medical,institutional,dental) Billing Provider City, State, Zip Code.
				seg++;
				sw.WriteLine("N4*"+Sout(billingCity,30)+"*"//N401 2/30 City Name: 
						+Sout(billingState,2,2)+"*"//N402 2/2 State or Province Code: 
						+Sout(billingZip.Replace("-",""),15)//N403 3/15 Postal Code: 
						+endSegment);//NM404 through NM407 are either situational with United States as default, or not used, so we don't specify any of them.
				//2010AA REF: EI (medical,institutional,dental) Billing Provider Tax Identification.
				seg++;
				sw.Write("REF*");
				if(medType==EnumClaimMedType.Medical) {
					sw.Write(billProv.UsingTIN?"EI*":"SY*");//REF01 2/3 Reference Identification Qualifier: EI=Employer's Identification Number (EIN). SY=Social Security Number (SSN).
				}
				else if(medType==EnumClaimMedType.Institutional) {
					sw.Write("EI*");//REF01 2/3 Reference Identification Qualifier: EI=Employer's Identification Number (EIN).
				}
				else if(medType==EnumClaimMedType.Dental) {
					sw.Write(billProv.UsingTIN?"EI*":"SY*");//REF01 2/3 Reference Identification Qualifier: EI=Employer's Identification Number (EIN). SY=Social Security Number (SSN).
				}
				sw.WriteLine(Sout(billProv.SSN,50)//REF02 1/50 Reference Identification. Tax ID #.
					+endSegment);//REF03 and REF04 are not used.
				if(medType==EnumClaimMedType.Medical || medType==EnumClaimMedType.Dental) {
					//2010AA REF: (medical,dental) Billing Provider UIPN/License Information: Situational. We do not use. Max repeat 2.
				}
				if(medType==EnumClaimMedType.Dental) {
					//2010AA REF: (dental) State License Number: Required by RECS and Emdeon clearinghouses. Everyone else should find it useful too. We do NOT validate that it's entered because seding it with non-persons causes problems.
					if(billProv.StateLicense!=""){
						seg++;
						sw.WriteLine("REF*0B*"//REF01 2/3 Reference Identification Qualifier: 0B=State License Number.
							+Sout(billProv.StateLicense,50)//REF02 1/50 Reference Identification: 
							+endSegment);//REF03 and REF04 are not used.
					}
					//2010AA REF G5 (dental) Site Identification Number: NOT IN X12 5010 STANDARD DOCUMENTATION. Only required by Emdeon.
					if(IsEmdeon(clearhouse)) {
						seg+=Write2010AASiteIDforEmdeon(sw,billProv,carrier.ElectID);
					}
				}
				//2010AA PER: IC (medical,institutional,dental) Billing Provider Contact Information: Probably required by a number of carriers and by Emdeon.
				seg++;
				sw.Write("PER*IC*"//PER01 2/2 Contact Function Code: IC=Information Contact.
					+Sout(PrefC.GetString(PrefName.PracticeTitle),60)+"*"//PER02 1/60 Name: Practice Title.
					+"TE*");//PER03 2/2 Communication Number Qualifier: TE=Telephone.
				if(clinic==null){
					sw.Write(Sout(PrefC.GetString(PrefName.PracticePhone),256));//PER04  1/256 Communication Number: Telephone number.
				}
				else{
					sw.Write(Sout(clinic.Phone,256));//PER04  1/256 Communication Number: Telephone number.
				}
				sw.WriteLine(endSegment);//PER05 through PER08 are situational and PER09 is not used. We do not use.
				if(PrefC.GetString(PrefName.PracticePayToAddress)!="") {
					//2010AB NM1: 87 (medical,institutional,dental) Pay-To Address Name.
					seg++;
					sw.Write("NM1*87*");//NM101 2/3 Entity Identifier Code: 87=Pay-to Provider.
					if(medType==EnumClaimMedType.Institutional) {
						sw.Write("2");//NM102 1/1 Entity Type Qualifier: 2=Non-Person Entity.
					}
					else { //(medical,dental)
						sw.Write((billProv.IsNotPerson?"2":"1"));//NM102 1/1 Entity Type Qualifier: 1=Person, 2=Non-Person Entity.
					}
					sw.WriteLine(endSegment); //NM103 through NM112 are not used.
					//2010AB N3: (medical,institutional,dental) Pay-To Address.
					seg++;
					sw.Write("N3*"+Sout(PrefC.GetString(PrefName.PracticePayToAddress),55));//N301 1/55 Address Information:
					if(PrefC.GetString(PrefName.PracticePayToAddress2)!="") {
						sw.Write("*"+Sout(PrefC.GetString(PrefName.PracticePayToAddress2),55));//N302 1/55 Address Information:
					}
					sw.WriteLine(endSegment);
					//2010AB N4: (medical,institutional,dental) Pay-To Address City, State, Zip Code.
					seg++;
					sw.WriteLine("N4*"+Sout(PrefC.GetString(PrefName.PracticePayToCity),30)+"*"//N401 2/30 City Name: 
						+Sout(PrefC.GetString(PrefName.PracticePayToST),2,2)+"*"//N402 2/2 State or Province Code: 
						+Sout(PrefC.GetString(PrefName.PracticePayToZip).Replace("-",""),15)//N403 3/15 Postal Code: 
						+endSegment);//NM404 through NM407 are either situational with United States as default, or not used, so we don't specify any of them.
				}
				//2010AC NM1: 98 (medical,institutional,dental) Pay-To Plan Name. We do not use.
				//2010AC N3: (medical,institutional,dental) Pay-To Plan Address. We do not use.
				//2010AC N4: (medical,institutional,dental) Pay-To Plan City, State, Zip Code. We do not use.
				//2010AC REF: (medical,institutional,dental) Pay-To Plan Secondary Identification. We do not use.
				//2010AC REF: (medical,institutional,dental) Pay-To Plan Tax Identification Number. We do not use.
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
				//2000B HL: (medical,institutional,dental) Subscriber Hierarchical Level.
				seg++;
				sw.WriteLine("HL*"+HLcount.ToString()+"*"//HL01 1/12 Hierarchical ID Number:
					+parentProv.ToString()+"*"//HL02 1/12 Hierarchical Parent ID Number: parent HL is always the billing provider HL.
					+"22*"//HL03 1/2 Hierarchical Level Code: 22=Subscriber.
					+hasSubord//HL04 1/1 Hierarchical Child Code: 0=No subordinate HL segment in this hierarchical structure. 1=Additional subordinate HL data segment in this hierarchical structure.
					+endSegment);
				//2000B SBR: (medical,institutional,dental) Subscriber Information.
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
				sw.Write(relationshipCode+"*");//SBR02 2/2 Individual Relationship Code: 18=Self (The only option besides blank).
				sw.Write(Sout(insPlan.GroupNum,50)+"*");//SBR03 1/50 Reference Identification: Does not need to be validated because group number is optional.
				//SBR04 1/60 Name: Situational. Required when SBR03 is not used. Does not need to be validated because group name is optional.
				if(insPlan.GroupNum!="") {
					sw.Write("*");
				} 
				else {
					sw.Write(Sout(insPlan.GroupName,60)+"*");
				}
				sw.Write("*"//SBR05 1/3 Insurance Type Code. Situational.  Skip because we don't support secondary Medicare.
					+"*"//SBR06 1/1 Coordination of Benefits Code: Not used.
					+"*"//SBR07 1/1 Yes/No Condition or Respose Code: Not used.
					+"*"//SBR08 2/2 Employment Status Code: Not used.
					+GetFilingCode(insPlan)//SBR09: 12=PPO,17=DMO,BL=BCBS,CI=CommercialIns,FI=FEP,HM=HMO
					+endSegment);
				if(medType==EnumClaimMedType.Medical) {
					//TODO: Do we need to do this?
					//2000B PAT: (medical) Patient Information. Situational. Required when the patient is the subscriber or considered to be the subscriber and at least one of the element requirements are met. We do not use.
				}
				//2010BA NM1: IL (medical,institutional,dental) Subscriber Name.
				seg++;
				sw.WriteLine("NM1*IL*"//NM101 2/3 Entity Identifier Code: IL=Insured or Subscriber.
					+"1*"//NM102 1/1 Entity Type Qualifier: 1=Person, 2=Non-Person Entity.
					+Sout(subscriber.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
					+Sout(subscriber.FName,35)+"*"//NM104 1/35 Name First:
					+Sout(subscriber.MiddleI,25)+"*"//NM105 1/25 Name Middle:
					+"*"//NM106 1/10 Name Prefix: Not Used.
					+"*"//NM107 1/10 Name Suffix: Situational. Not present in Open Dental yet so we leave blank.
					+"MI*"//NM108 1/2 Identification Code Qualifier: MI=Member Identification Number.
					+Sout(sub.SubscriberID.Replace("-",""),80,2)//NM109 2/80 Identification Code: Situational. Required when NM102=1.
					+endSegment);//NM110 through NM112 are not used.
				//At the request of Emdeon, we always include N3,N4,and DMG even if patient is not subscriber.
				//This does not make the transaction non-compliant, and they find it useful.
				if(subscriber.PatNum==patient.PatNum) {
					//2010BA N3: (medical,institutional,dental) Subscriber Address. Situational. Required when the patient is the subscriber.
					seg++;
					sw.Write("N3*"+Sout(subscriber.Address,55));//N301 1/55 Address Information:
					if(subscriber.Address2!="") {
						sw.Write("*"+Sout(subscriber.Address2,55));//N302 1/55 Address Information:
					}
					sw.WriteLine(endSegment);
					//2010BA N4: (medical,institutional,dental) Subscriber City, State, Zip Code. Situational. Required when the patient is the subscriber.
					seg++;
					sw.WriteLine("N4*"
							+Sout(subscriber.City,30)+"*"//N401 2/30 City Name:
							+Sout(subscriber.State,2,2)+"*"//N402 2/2 State or Provice Code:
							+Sout(subscriber.Zip.Replace("-",""),15)//N403 3/15 Postal Code:
							+endSegment);//N404 through N407 either not used or required for addresses outside of the United States.
					//2010BA DMG: (medical,institutional,dental) Subscriber Demographic Information. Situational. Required when the patient is the subscriber.
					seg++;
					sw.Write("DMG*D8*");//DMG01 2/3 Date Time Period Format Qualifier: D8=Date Expressed in Format CCYYMMDD.
//todo: Validate subscriber BD?
					if(subscriber.Birthdate.Year<1900) {
						sw.Write("19000101*");//DMG02 1/35 Date Time Period: Birthdate.
					}
					else {
						sw.Write(subscriber.Birthdate.ToString("yyyyMMdd")+"*");//DMG02 1/35 Date Time Period: Birthdate.
					}
					sw.WriteLine(GetGender(subscriber.Gender)//DMG03 1/1 Gender Code: F=Female, M=Male, U=Unknown.
						+endSegment);
				}
				//2010BA REF: SY (medical,institutional,dental) Secondary Secondary Identification: Situational. Required when an additional identification number to that provided in NM109 of this loop is necessary. We do not use this.
				//2010BA REF: Y4 (medical,institutional,dental) Property and Casualty Claim Number: Required when the services included in this claim are to be considered as part of a property and casualty claim. We do not use this.
				//2010BA PER: IC (medical) Property and Casualty Subscriber Contact information: Situational. We do not use this.
				//2010BB NM1: PR (medical,institutional,dental) Payer Name.
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
				sw.Write("*"//NM104 1/35 Name First: Not used.
					+"*"//NM105 1/25 Name Middle: Not used.
					+"*"//NM106 1/10 Name Prefix: Not used.
					+"*"//NM107 1/10 Name Suffix: Not used.
					+"PI*");//NM108 1/2 Identification Code Qualifier: PI=Payor Identification.
				string electid=carrier.ElectID;
				if(electid=="" && clearhouse.ISA08=="113504607") {//only for Tesia
					electid="00000";
				}
				if(electid.Length<3) {
					electid="06126";
				}
				sw.WriteLine(Sout(electid,80,2)//NM109 2/80 Identification Code: PayorID.
					+endSegment);//NM110 through NM112 Not Used.
				//2010BB N3: (medical,institutional,dental) Payer Address.
				seg++;
				sw.Write("N3*"+Sout(carrier.Address,55));//N301 1/55 Address Information:
				if(carrier.Address2!="") {
					sw.WriteLine("*"+Sout(carrier.Address2,55));//N302 1/55 Address Information: Required when there is a second address line.
				}
				sw.WriteLine(endSegment);
				//2010BB N4: (medical,institutional,dental) Payer City, State, Zip Code.
				seg++;
				sw.WriteLine("N4*"
					+Sout(carrier.City,30)+"*"//N401 2/30 City Name:
					+Sout(carrier.State,2)+"*"//N402 2/2 State or Province Code:
					+Sout(carrier.Zip.Replace("-",""),15)//N403 3/15 Postal Code:
					+endSegment);//N404 through N407 are either not used or are for addresses outside of the United States.
				//2010BB REF 2U,EI,FY,NF (dental) Payer Secondary Identificaiton. Situational.
				//2010BB REF G2,LU Billing Provider Secondary Identification. Situational. Required when NM109 (NPI) of loop 2010AA is not used.
				if(IsEmdeon(clearhouse)) {//Required by Emdeon
					seg+=WriteProv_REFG2(sw,billProv,carrier.ElectID);
				}
				parentSubsc=HLcount;
				HLcount++;
				#endregion
				#region Patient
				if(patient.PatNum!=subscriber.PatNum){//if patient is not the subscriber
					//2000C HL: (medical,institutional,dental) Patient Hierarchical Level.
					seg++;
					sw.WriteLine("HL*"+HLcount.ToString()+"*"//HL01 1/12 Hierarchical ID Number:
						+parentSubsc.ToString()+"*"//HL02 1/12 Hierarchical Parent ID Number: Parent is always the subscriber HL.
						+"23*"//HL03 1/2 Hierarchical Level Code: 23=Dependent.
						+"0"//HL04 1/1 Hierarchical Child Code: 0=No subordinate HL segment in this hierarchical structure.
						+endSegment);
					//2000C PAT: (medical,institutional,dental) Patient Information.
					seg++;
					if(IsEmdeon(clearhouse)) {
						sw.WriteLine("PAT*"
							+GetRelat(claim.PatRelat)+"*"//PAT01 2/2 Individual Relationship Code:
							+"*"//PAT02 1/1 Patient Location Code: Not used.
							+"*"//PAT03 2/2 Employment Status Code: Not used.
							+GetStudentEmdeon(patient.StudentStatus)//PAT04 1/1 Student Status Code: Not used. Emdeon wants us to sent this code corresponding to version 4010, even through it is not standard X12.
							+endSegment);//PAT05 through PAT08 not used in institutional or dental, but is sometimes used in medical. We do not use.
					}
					else {
						sw.WriteLine("PAT*"
							+GetRelat(claim.PatRelat)+"*"//PAT01 2/2 Individual Relationship Code:
							+endSegment);//PAT02 through PAT04 Not used. PAT05 through PAT08 not used in institutional or dental, but is sometimes used in medical. We do not use.
					}
					//2010CA NM1: QC (medical,institutional,dental) Patient Name.
					seg++;
					sw.Write("NM1*QC*"//NM101 2/3 Entity Identifier Code: QC=Patient.
						+"1*"//NM102 1/1 Entity Type Qualifier: 1=Person.
						+Sout(patient.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
						+Sout(patient.FName,35));//NM104 1/35 Name First:
						if(patient.MiddleI!="") {
							sw.WriteLine("*"+Sout(patient.MiddleI,25));//NM105 1/25 Name Middle
						}
						sw.WriteLine(endSegment);
						//NM106 not used. NM107, No suffix field in Open Dental. NM108 through NM112 not used.
						//instead of including a patID here, the patient should get their own subsriber loop.
//TODO: js 9/5/11 Treat like a subscriber whenever patID is present.  Test.
					//2010CA N3: (medical,institutional,dental) Patient Address.
					seg++;
					sw.Write("N3*"+
						Sout(patient.Address,55));//N301 1/55 Address Information
					if(patient.Address2!="") {
						sw.WriteLine("*"+Sout(patient.Address2,55));//N302 1/55 Address Information:
					}
					sw.WriteLine(endSegment);
					//2010CA N4: (medical,institutional,dental) Patient City, State, Zip Code.
					seg++;
					sw.WriteLine("N4*"
						+Sout(patient.City,30)+"*"//N401 2/30 City Name:
						+Sout(patient.State,2,2)+"*"//N402 2/2 State or Provice Code: 
						+Sout(patient.Zip.Replace("-",""),15)//N403 3/15 Postal Code: 
						+endSegment);//N404 through N407 are either not used or only required for addresses outside the United States.
					//2010CA DMG: (medical,institutional,dental) Patient Demographic Information.
					seg++;
					sw.WriteLine("DMG*D8*"//DMG01 2/3 Date Time Period Format Qualifier: D8=Date Expressed in Format CCYYMMDD.
						+patient.Birthdate.ToString("yyyyMMdd")+"*"//DMG02 1/35 Date Time Period:
						+GetGender(patient.Gender)//DMG03 1/1 Gender Code: F=Female,M=Male,U=Unknown.
						+endSegment);//DMG04 through DMG11 are not used.
					//2010CA REF: (medical,instituional,dental) Property and Casualty Claim Number. Situational. We do not use this.
					//2010CA REF: (medical,institutional) Property and Casualty Patient Identifier. Situational.  We do not use.
					//2010CA PER: (medical) Property and Casualty Patient Contact Information. Situational. We do not use.
					HLcount++;
				}
				#endregion
				#region Claim CLM
				//2300 CLM: (medical,institutional,dental) Claim Information.
				seg++;
				sw.Write("CLM*"
					+Sout(claim.PatNum.ToString()+"/"+claim.ClaimNum.ToString(),38)+"*"//CLM01 1/38 Claim Submitter's Identifier: A unique id.  By using both PatNum and ClaimNum, it is possible to search for a patient as well as to ensure uniqueness.
//todo: add field to allow user to override for claims based on preauths.
					+claim.ClaimFee.ToString()+"*"//CLM02 1/18 Monetary Amount:
					+"*"//CLM03 1/2 Claim Filing Indicator Code: Not used.
					+"*");//CLM04 1/2 Non-Institutional Claim Type Code: Not used.
				//CLM05 (medical,institutional,dental) Health Care Services Location Information.
				if(medType==EnumClaimMedType.Medical) {
					sw.Write(GetPlaceService(claim.PlaceService)+":"//CLM05-1 1/2  Facility Code Value: Place of Service.
						+"B:"//CLM05-2 1/2 Facility Code Qualifier, B=Place of Service Codes.
//todo: js 9/10/11 Consider supporting corrected and replacement.
						+"1"+"*");//CLM05-3 1/1 Claim Frequency Type Code: Code source 235: Claim Frequency Type Code. 1=original, 6=corrected, 7=replacement, 8=void(in limited jursidictions).  We currently only support 1-original.
				}
				else if(medType==EnumClaimMedType.Institutional) {
					//claim.UniformBillType validated to be exactly 3 char
					//Example: 771: 7=clinic, 7=FQHC, 1=Only claim.  713: 7=clinic, 1=rural health clinic, 3=continuing claim.
					sw.Write(claim.UniformBillType.Substring(0,2)+":"//CLM05-1 1/2  Facility Code Value: First and second position of UniformBillType.
						+"A:"//CLM05-2 1/2 Facility Code Qualifier, A=Uniform Billing Claim Form Bill Type.
						+claim.UniformBillType.Substring(2)+"*");//CLM05-3 1/1 Claim Frequency Type Code: Third position of UniformBillType.
				}
				else{//dental.
					sw.Write(GetPlaceService(claim.PlaceService)+":"//CLM05-1 1/2  Facility Code Value: Place of Service.
					+"B:"//CLM05-2 1/2 Facility Code Qualifier, B=Place of Service Codes.
//todo: js 9/10/11 Consider supporting corrected and replacement.
					+"1"+"*");//CLM05-3 1/1 Claim Frequency Type Code: Code source 235: Claim Frequency Type Code. 1=original, 6=corrected, 7=replacement, 8=void(in limited jursidictions).  We currently only support 1-original.
				}
				if(medType==EnumClaimMedType.Medical) {
					sw.Write("Y*");//CLM06 1/1 Yes/No Condition or Response Code: prov sig on file (always yes)
				}
				else if(medType==EnumClaimMedType.Institutional) {
					sw.Write("*");//CLM06 1/1 Yes/No Condition or Response Code: Not used.
				}
				else if(medType==EnumClaimMedType.Dental) {
					sw.Write("Y*");//CLM06 1/1 Yes/No Condition or Response Code: prov sig on file (always yes)
				}
				sw.Write("A*");//CLM07 1/1 Provider Accept Assignment Code: Prov accepts medicaid assignment. OD has no field for this, so no choice.
				sw.Write((sub.AssignBen?"Y":"N")+"*");//CLM08 1/1 Yes/No Condition or Response Code: We do not support W.
				sw.Write(sub.ReleaseInfo?"Y":"I");//CLM09 1/1 Release of Information Code: Y or I(which is equivalent to No)
				if(medType==EnumClaimMedType.Medical) {
					//sw.Write("*"//end of CLM09
					//  +"*");//CLM10 1/1 Patient Signature Source Code: Situational. We do not use.
					////CLM11 Related Causes Information. Situational. Required when accident date is specified in DTP 439 of loop 2300.
					//if(claim.AccidentDate.Year>1880) {
//todo						
					//}
					//sw.Write("*");//End of CLM11
					sw.WriteLine(endSegment);//CLM10 through CLM20 are mostly not used, but some are situational and we will probably need to implement. For now, we do not use.
				}
				else if(medType==EnumClaimMedType.Institutional) {
					//CLM10-19 not used, 20 not supported.
					sw.WriteLine(endSegment);
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
					sw.WriteLine(endSegment);
				}
				#endregion Claim CLM
				#region Claim DTP
				if(medType==EnumClaimMedType.Medical) {
					//2300 DTP: 431 (medical) Date Onset of Current Illness or Symptom. Situational. We do not use.
					//2300 DTP: 454 (medical) Initial Treatment Date. Situational. We do not use. (spinal manipulation).
					//2300 DTP: 304 (medical) Date Last Seen. Situational. We do not use. (foot care)
					//2300 DTP: 453 (medical) Date Accute Manifestation. Situational. We do not use. (spinal manipulation)
					//2300 DTP: 439 (medical) Date Accident. Situational.
					if(claim.AccidentDate.Year>1880) {
						seg++;
						sw.WriteLine("DTP*439*"//DTP01 3/3 Date/Time Qualifier: 439=accident
							+"D8*"//DTP02 2/3 Date Time Period Format Qualifer: D8=Date Expressed in Format CCYYMMDD.
							+claim.AccidentDate.ToString("yyyyMMdd")//DTP03 1/35 Date Time Period:
							+endSegment);
					}
					//2300 DTP: 484 (medical) Last Menstrual Period Date. Situational. We do not use.
					//2300 DTP: 455 (medical) Last X-ray Date. Situational. We do not use.
					//2300 DTP: 471 (medical) Hearing and Vision Prescription Date. Situational. We do not use.
					//2300 DTP: 314,360,361 (medical) Disability Dates. Situational. We do not use.
					//2300 DTP: 297 (medical) Date Last Worked. Situational. We do not use.
					//2300 DTP: 296 (medical) Date Authorized Return to Work. Situational. We do not use.
					//2300 DTP: 435 (medical) Date Admission. Situational. We do not use.
					//2300 DTP: 096 (medical) Date Discharge. Situational. We do not use.
					//2300 DTP: 090 (medical) Date Assumed and Relinquished Care Dates. Situational. We do not use.
					//2300 DTP: 444 (medical) Date Property and Casualty Date of First Contact. Situational. We do not use.
					//2300 DTP: 050 (medical) Repricer Received Date. Situational. We do not use.
				}
				else if(medType==EnumClaimMedType.Institutional) {
					//2300 DTP 096 (institutional) Discharge Hour. Situational. We do not use. Inpatient. 
					//2300 DTP 434 (instititional) Statement Dates.
//todo:how to handle preauths?
					if(claim.DateService.Year>1880) {//DateService validated
						seg++;
						sw.WriteLine("DTP*434*"//DTP01 3/3 Date/Time Qualifier: 434=Statement.
							+"RD8*"//DTP02 2/3 Date Time Period Format Qualifier: RD8=Range of Dates Expressed in Format CCYYMMDD-CCYYMMDD.
							+claim.DateService.ToString("yyyyMMdd")+"-"+claim.DateService.ToString("yyyyMMdd")//DTP03 1/35 Date Time Period:
							+endSegment);
					}
					//2300 DTP 435 (institutional) Admission Date/Hour. Situational. We do not use. Inpatient.
					//For the UB04 we are using claim.DateService for this field.
					//2300 DTP 050 (institutional) Repricer Received Date. Situational. Not supported.
				}
				else if(medType==EnumClaimMedType.Dental) {
					//2300 DTP 439 (dental) Date accident. Situational. Required when there was an accident.
					if(claim.AccidentDate.Year>1880) {
						seg++;
						sw.WriteLine("DTP*439*"//DTP01 3/3 Date/Time Qualifier: 439=accident
							+"D8*"//DTP02 2/3 Date Time Period Format Qualifer: D8=Date Expressed in Format CCYYMMDD.
							+claim.AccidentDate.ToString("yyyyMMdd")//DTP03 1/35 Date Time Period:
							+endSegment);
					}
					//2300 DTP 452 (dental) Date Appliance Placement. Situational. Values can be overriden in loop 2400 for individual service items, but we don't support that.
					if(claim.OrthoDate.Year>1880) {
						seg++;
						sw.WriteLine("DTP*452*"//DTP01 3/3 Date/Time Qualifier: 452=Appliance Placement.
							+"D8*"//DTP02 2/3 Date Time Period Format Qualifier: D8=Date Expressed in Format CCYYMMDD.
							+claim.OrthoDate.ToString("yyyyMMdd")//DTP03 1/35 Date Time Period:
							+endSegment);
					}
					//2300 DTP 472 (dental) Service Date. Not used if predeterm.
					if(claim.ClaimType!="PreAuth") {
						if(claim.DateService.Year>1880) {
							seg++;
							sw.WriteLine("DTP*472*"//DTP01 3/3 Date/Time Qualifier: 472=Service.
								+"D8*"//DTP02 2/3 Date Time Period Format Qualifier: D8=Date Expressed in Format CCYYMMDD.
								+claim.DateService.ToString("yyyyMMdd")//DTP03 1/35 Date Time Period:
								+endSegment);
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
							+endSegment);//DN103 is not used and DN104 is situational but we do not use it.
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
							+"M*"//DN202: M=Missing, I=Impacted, E=To be extracted.
							+"*"//DN203 1/15 Quantity: Not used.
							+"*"//DN204 2/3 Date Time Period Format Qualifier: Not used.
							+"*"//DN205 1/35 Date Time Period: Not used.
							+"JP"//DN206 1/3 Code List Qualifier Code: Required. JP=JP Universal National Tooth Designation System.
							+endSegment);
					}
				}
				if(medType==EnumClaimMedType.Institutional) {
					//2300 CL1: Institutional Claim Code. Required
					seg++;
					sw.Write("CL1*"
						+claim.AdmissionTypeCode//CL101 1/1 Admission Type Code. Required. Validated.
						//TODO //CL102 1/1 Admission source code. Required for inpatient services. Validated.
						//TODO //CL103 1/2 Patient status code. Required. Validated.
						//CL104 1/1 Nursing Home Residential Status Code: Not used.
						);
				}
				#endregion Claim DN CL1
				#region Claim PWK
				//2300 PWK: (medical,institutional,dental) Claim Supplemental Information. Paperwork. Used to identify attachments.
				/*if(clearhouse.ISA08=="113504607" && claim.Attachments.Count>0) {//If Tesia and has attachments
					seg++;
					sw.WriteLine("PWK*"
						+"OZ*"//PWK01: ReportTypeCode. OZ=Support data for claim.
						+"EL*"//PWK02: Report Transmission Code. EL=Electronic
						+"**"//PWK03 and 04: not used
						+"AC*"//PWK05: Identification Code Qualifier. AC=Attachment Control Number
						+"TES"+claim.ClaimNum.ToString()//PWK06: Identification Code.
				 		+endSegment);
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
					idCode="00";
				}
				idCode=Sout(idCode,80,2);
				if(pwk01.Trim()!="") {
					seg++;
					sw.WriteLine("PWK*"
						+pwk01+"*"//PWK01 2/2 Report Type Code:
						+pwk02+"*"//PWK02 1/2 Report Transmission Code: EL=Electronically Only, BM=By Mail.
						+"**"//PWK03 and PWK04: Not Used.
						+"AC*"//PWK05 1/2 Identification Code Qualifier: AC=Attachment Control Number.
						+idCode//PWK06 2/80 Identification Code:
						+endSegment);//PWK07 through PWK09 are not used.
				}
				#endregion Claim PWK
				#region Claim CN1 AMT
				//2300 CN1: (medical,institutional,dental)Contract Information. Situational. We do not use this.
				//2300 AMT: (institutional) Patient Estimated Amount Due.
				//2300 AMT: (medical,dental) Patient Amount Paid. Situational. We do not use this.
				#endregion Claim CN1 AMT
				#region Claim REF
				if(medType==EnumClaimMedType.Dental) {
					//2300 REF: G3 (dental) Predetermination Identification. Situational.  Required when sending claim for previously predetermined services. Do not send prior authorization number here.
					if(claim.PreAuthString!="") {//validated to be empty for medical and inst
						seg++;
						sw.WriteLine("REF*G3*"//REF01 2/3 G3=Predetermination of Benefits Identification Number.
							+Sout(claim.PreAuthString,50)//REF02 1/50 Predeterm of Benfits Identifier.
							+endSegment);//REF03 and REF04 are not used.
					}
				}
				//2300 REF: 4N (medical,institutional,dental) Service Authorization Exception Code. Situational. Required if services were performed without authorization.
//todo: ServiceAuthException
				//2300 REF: F5 (medical) Mandatory Medicare (Section 4081) Crossover Indicator. Situational. Required when submitter is Medicare and the claim is a Medigap or COB crossover claim. We do not use.
				//2300 REF: EW (medical) Mammography Certification Number. Situational. We do not use.
				//2300 REF: F8 (medical,institutional,dental) Payer Claim Control Number: Situational. Required if this is a replacement or a void. F8=Original Reference Number.
					//aka Original Document Control Number/Internal Control Number (DCN/ICN).
					//aka Transaction Control Number (TCN).  
					//aka Claim Reference Number. 
					//Seems to be required by Medicaid when voiding a claim or resubmitting a claim by setting the CLM05-3.
//todo: Implement
				//2300 REF: 9F (medical,institutional,dental) Referral Number. Situational. 
				if(claim.RefNumString!="") {
					seg++;
					sw.WriteLine("REF*9F*"//REF01 2/3 Reference Identification Qualifier: 9F=Referral Number.
						+Sout(claim.RefNumString,30)//REF02 1/50 Reference Identification:
						+endSegment);//REF03 and REF04 are not used.
				}
				//2300 REF: X4 (medical) Clinical Laboratory Improvement Amendment (CLIA) Number. Situational. We do not use.
				//2300 REF: G1 (medical,institutional,dental) Prior Authorization. Situational. Do not report predetermination of benefits id number here. G1 and G3 were muddled in 4010.  
				if(claim.PriorAuthorizationNumber!="") {
					seg++;
					sw.WriteLine("REF*G1*"//REF01 2/3 Reference Identification Qualifier: G1=Prior Authorization Number.
						+Sout(claim.PriorAuthorizationNumber,50)//REF02 1/50 Reference Identification: Prior Authorization Number.
						+endSegment);//REF03 and REF04 are not used.
				}					
				//2300 REF: 9A (medical,institutional,dental) Repriced Claim Number. Situational. We do not use. 
				//2300 REF: 9C (medical,institutional,dental) Adjusted Repriced Claim Number. Situational. We do not use.
				//2300 REF: D9 (medical,institutional,dental) Claim Identifier For Transmission Intermediaries. Situational. We do not use.
				//2300 REF: LX (medical,institutional) Investigational Device Exemption Number. Situational. Required for FDA IDE.
				//2300 REF: LU (institutional) Auto Accident State. Situational. Seems to me to be a duplicate of the info in CLM11.
				//2300 REF: EA (medical,institutional) Medical Record Number. Situational. We do not use.
				//2300 REF: P4 (medical,institutional) Demonstration Project Identifier. Situational. We do not use. Seems very unimportant.
				//2300 REF: G4 (institutional) Peer Review Organization (PRO) Approval Number. Situational. We do not use.
				//2300 REF: 1J (medical) Care Plan Oversight. Situational. We do not use.
				#endregion Claim REF
				#region Claim K3 NTE CRx
				//2300 K3: File info (medical,institutional,dental). Situational. We do not use this.
				//NTE loops------------------------------------------------------------------------------------------------------
				//2300 NTE: (medical,institutional,dental) Claim Note. Situational. A number of NTE01 codes other than 'ADD', which we don't support.
				string note="";
				if(claim.AttachmentID!="" && !claim.ClaimNote.StartsWith(claim.AttachmentID)) {
					note=claim.AttachmentID+" ";
				}
				note+=claim.ClaimNote;
				if(note!="") {
					seg++;
					sw.WriteLine("NTE*ADD*"//NTE01 3/3 Note Reference Code: ADD=Additional information.
						+Sout(note,80)//NTE02 1/80 Description:
						+endSegment);
				}
				//2300 NTE: (institutional) Billing Note. Situational. We do not use.
				//CRx loops------------------------------------------------------------------------------------------------------
				//2300 CR1: LB (medical) Ambulance Transport Information. Situational. We do not use.
				//2300 CR2: (medical) Spinal Manipulation Service Information. Situational. We do not use.
				//2300 CRC: (medical) Ambulance Certification. Situational. We do not use.
				//2300 CRC: (medical) Patient Condition Information Vision. Situational. We do not use.
				//2300 CRC: (medcial) Homebound Indicator. Situational. We do not use.
				//2300 CRC: (medical,institutional) EPSDT Referral. Situational. Required on EPSDT claims when the screening service is being billed in this claim. We do not use.
				#endregion Claim K3 NTE CRx
				#region Claim HI HCP
				//HI loops-------------------------------------------------------------------------------------------------------
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
				//2300 HI: BK (medical,dental) Health Care Diagnosis Code. Situational. For OMS or anesthesiology.
//todo: validate at least one diagnosis
				if(medType==EnumClaimMedType.Institutional) {
					seg++;
					sw.Write("HI*"
						+"BK:"//HI01-1 1/3 Code List Qualifier Code: BK=ICD-9 Principal Diagnosis.
						+Sout((string)diagnosisList[0],30).Replace(".",""));//HI01-2 1/30 Industry Code: Diagnosis code. No periods.
					sw.WriteLine(endSegment);
				} else if(medType==EnumClaimMedType.Medical) {
					seg++;
					sw.Write("HI*"
						+"BK:"//HI01-1 1/3 Code List Qualifier Code: BK=ICD-9 Principal Diagnosis.
						+Sout((string)diagnosisList[0],30).Replace(".",""));//HI01-2 1/30 Industry Code: Diagnosis code. No periods.
					for(int j=1;j<diagnosisList.Count;j++) {
						if(j>11) {//maximum of 12 diagnoses
							break;
						}
						sw.Write("*"//this is the * from the _previous_ field.
							+"BF:"//HI0#-1 1/3 Code List Qualifier Code: BF=ICD-9 Diagnosis
							+Sout((string)diagnosisList[j],30).Replace(".",""));//HI0#-2 1/30 Industry Code: Diagnosis code. No periods.
					}
					sw.WriteLine(endSegment);
				}
				//2300 HI: BP (medical) Anesthesia Related Procedure. Situational. We do not use.
				//2300 HI: BJ (institutional) Admitting Diagnosis. Situational. Required for inpatient admission. We do not use.
				//2300 HI: PR (institutional) Patient's Reason for Visit. Situational. Required for outpatient visits.
//todo: probably required
				//2300 HI: BN (institutional) External Cause of Injury. Situational. We do not use.
				//2300 HI: (institutional) Diagnosis Related Group (DRG) Information. Situational. We do not use. For inpatient hospital under DRG contract.
				//2300 HI: BF (institutional) Other Diagnosis Information. Situational. We do not use. When other conditions coexist or develop.
				//2300 HI: BR (institutional) Principal Procedure Information. Situational. We do not use. Required on inpatient claims when a procedure was performed.
				//2300 HI: BQ (institutional) Other Procedure Information. Situational. We do not use. Inpatient claims for additional procedures.
				//2300 HI: BI (institutional) Occurence Span Information. Situational. We do not use. For an occurence span code.
				//2300 HI: BH (institutional) Occurence Information. Situational. We do not use. For an occurence code.
				//2300 HI: BE (institutional) Value Information. Situational. We do not use. For a value code.
				//2300 HI: BG (medical,institutional) Condition Information. Situational. We do not use. For a condition code.
				//2300 HI: TC (institutional) Treatment Code Information. Situational. We do not use. When home health agencies need to report plan of treatment information under contracts.
				//2300 HCP: (medical,institutional,dental) Claim Pricing/Repricing Information. Situational. We do not use.
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
							+X12Generator.GetTaxonomy(provTreat)//PRV03: Taxonomy code
							+endSegment);
					}*/
					//2310A NM1: (medical) Referring Provider Name. Situational. We do not use.
					//2310A REF: (medical) Referring Provider Secondary Identification. Situational. We do not use.
					//2010B NM1: (medical) Rendering Provider Name. Situational. We do not use.
					//2310B PRV: (medical) Rendering Provider Specialty Information. Situational. We do not use.
					//2310B REF: (medical) Rendering Provider Secondary Identification. Situational. We do not use.
					//2310C NM1: (medical) Service Facility Location Name. Situational. We do not use.
					//2310C N3: (medical) Service Facility Location Address. We do not use.
					//2310C N4: (medical) Service Facility Location City, State, Zip Code. We do not use.
					//2310C REF: (medical) Service Facility Location Secondary Identification. Situational. We do not use.
					//2310C PER: (medical) Service Facility Contact Information. Situational. We do not use.
					//2310D NM1: (medical) Supervising Provider Name. Situational. We do not use.
					//2310D REF: (medical) Supervising Provider Secondary Identification. Situational. We do not use.
					//2310E NM1: (medical) Ambulance Pick-up Location. Situational. We do not use.
					//2310E N3: (medical) Ambulance Pick-up Location Address. We do not use.
					//2310E N4: (medical) Ambulance Pick-up Location City, State, Zip Code. We do not use.
					//2310F NM1: (medical) Ambulance Drop-off Location. Situational. We do not use.
					//2310F N3: (medical) Ambulance Drop-off Location Address. We do not use.
					//2310F N4: (medical) Ambulance Drop-off Location City, State, Zip Code. We do not use.
				}
				#endregion 2310 Claim Providers (medical)
				#region 2310 Claim Providers (inst)
				if(medType==EnumClaimMedType.Institutional) {
					//2310A NM1: 71 (institutional) Attending Provider Name. Situational.
						//required. Provider with overall responsibility for care and treatment reported on this claim.
	//todo: attending provider.  We will use our treating provider field.
					//2310A PRV: AT (institutional) Attending Provider Specialty Information. Situational.
					//2310A REF: (institutional) Attending Provider Secondary Identification. Situational.
					//2310B NM1: 72 (institutional) Operating Physician Name. Situational. For surgical procedure codes.
					//2310C REF: ZZ (institutional) Secondary Physician Secondary Identification. Situational.
					//2310C NM1: ZZ (institutional) Other Operating Physician Name. Situational.
					//2310C REF: ZZ (institutional) Other Operating Physician Secondary Identification. Situational.
					//2310D NM1: 82 (institutional) Rendering Provider Name. Situational. If different from attending provider AND when regulations require both facility and professional components.
					//2310D REF: ZZ (institutional) Rendering Provider Secondary Identificaiton. Situational.
					//2310E NM1: 77 (institutional) Service Facility Location Name. Situational.
	//todo:
					//2310E N3: (institutional) Service Facility Location Address. Required when place of service is different from loop 2010AA billing provider.
					//2310E N4: (institutional) Service Facility Location City, State, Zip Code.
					//2310E REF: ZZ (institutional) Service Facility Location Secondary Identificiation. Situational.
					//2310F NM1: DN (institutional) Referring Provider Name. Situational. Required when referring provider is different from attending provider.
					//2310F REF: (institutional) Referring Provider Secondary Identification. Situational. 
				}
				#endregion 2310 Claim Providers (inst)
				#region 2310 Claim Providers (dental)
				if(medType==EnumClaimMedType.Dental) {
					//2310A NM1: DN (dental) Referring Provider Name. Situational. //js 9/5/11 Why not?  I thought this was a field on the claim that we DID send.
					//2310A PRV: (dental) Referring Provider Specialty Information. Situational.
					//2310A REF: G2 (dental) Referring Provider Secondary Identification. Situational.
					if(claim.ProvTreat!=claim.ProvBill) {
						//2310B NM1: 82 (dental) Rendering Provider Name. Situational. Only required if different from the billing provider. Emdeon will reject the claim if this segment is the same as the billing provider for all claims in the batch.
						provTreat=Providers.GetProv(claim.ProvTreat);
						seg++;
						sw.WriteLine("NM1*82*"//NM101 2/3 Entity Identifier Code: 82=Rendering Provider.
							+"1*"//NM102 1/1 Entity Type Qualifier: 1=Person.
							+Sout(provTreat.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
							+Sout(provTreat.FName,35)+"*"//NM104 1/35 Name First:
							+Sout(provTreat.MI,25)+"*"//NM105 1/25 Name Middle:
							+"*"//NM106 1/10 Name Prefix: Not Used.
							+"*"//NM107 1/10 Name Suffix: We don't support.
							+"XX*"//NM108 1/2 Identification Code Qualifier: Situational. Required since after the HIPAA date. XX=NPI.
							+Sout(provTreat.NationalProvID,80)//NM109 2/80 Identification Code:  NPI validated.
							+endSegment);//NM110 through NM112 are not used.
						//2310B PRV: PE (dental) Rendering Provider Specialty Information.
						seg++;
						sw.WriteLine("PRV*"
							+"PE*"//PRV01 1/3 Provider Code: PE=Performing.
							+"PXC*"//PRV02 2/3 Reference Identification Qualifier: PXC=Health Care Provider Taxonomy Code.
							+X12Generator.GetTaxonomy(provTreat)//PRV03 1/50 Reference Identification: Taxonomy Code.
							+endSegment);//PRV04 through PRV06 are not used.
						//2310B REF: (dental) Rendering Provider Secondary Identification. Situational. Max repeat of 4.
						//todo: is StateLicense validated?
						if(provTreat.StateLicense!="") {
							seg++;
							sw.WriteLine("REF*0B*"//REF01 2/3 Reference Identification Qualifier: 0B=State License Number.
								+Sout(provTreat.StateLicense,50)//REF02 1/50 Reference Identification:
								+endSegment);//REF03 and REF04 are not used.
						}
						seg+=WriteProv_REFG2(sw,provTreat,carrier.ElectID);
					}
					//2310C NM1: 77 (dental) Service Facility Location Name. Situational. Only required if PlaceService is 21,22,31, or 35. 35 does not exist in CPT, so we assume 33.
					if(claim.PlaceService==PlaceOfService.InpatHospital || claim.PlaceService==PlaceOfService.OutpatHospital
						|| claim.PlaceService==PlaceOfService.SkilledNursFac || claim.PlaceService==PlaceOfService.CustodialCareFacility) {//AdultLivCareFac
						seg++;
						sw.WriteLine("NM1*77*"//NM101 2/3 Entity Identifier Code: 77=Service Location.
							+"2*"//NM102 1/1 Entity Type Qualifier: 2=Non-Person Entity.
							+Sout(billProv.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
							+"*"//NM104 1/35 Name First: Not used.
							+"*"//NM105 1/25 Name Middle: Not used.
							+"*"//NM106 1/10 Name Prefix: Not used.
							+"*"//NM107 1/10 Name Suffix: Not used.
							+"XX*"//NM108 1/2 Identification Code Qualifier: XX=NPI.
							+Sout(billProv.NationalProvID,80)//NM109 2/80 Identification Code: Validated.
							+endSegment);//NM110 through NM112 not used.
						//2310C N3: (dental) Service Facility Location Address.
						seg++;
						sw.Write("N3*"+Sout(billingAddress1,55));//N301 1/55 Address Information:
						if(billingAddress2!="") {
							sw.Write("*"+Sout(billingAddress2,55));//N302 1/55 Address Information: Only required when there is a secondary address line.
						}
						sw.WriteLine(endSegment);
						//2310C N4: (dental) Service Facility Location City, State, Zip Code.
						seg++;
						sw.WriteLine("N4*"
							+Sout(billingCity,30)+"*"//N401 2/30 City Name:
							+Sout(billingState,2,2)+"*"//N402 2/2 State or Provice Code:
							+Sout(billingZip,15)+"*"//N403 3/15 Postal Code:
							+endSegment);//N404 through N407 are either not used or only required when outside of the United States.
						//2310C REF: (dental) Service Facility Location Secondary Identification. Situational. We do not use this.
					}
					//2310D NM1: (dental) Assistant Surgeon Name. Situational. We do not support.
					//2310D PRV: (dental) Assistant Surgeon Specialty Information. We do not support.
					//2310D REF: (dental) Assistant Surgeon Secondary Identification. We do not support.
					//2310E NM1: (dental) Supervising Provider Name. Situational. We do not support.
					//2310E REF: (dental) Supervising Provider Secondary Identification. Situational. We do not support.
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
						+Sout(otherPlan.GroupNum,50)+"*");//SBR03 1/50 Reference Identification:
					//SBR04 1/60 Name: Situational. Required when SBR03 is not specified.
					if(otherPlan.GroupNum!="") {
						sw.Write("*");
					}
					else {
						sw.Write(Sout(otherPlan.GroupName,60)+"*");
					}
					sw.WriteLine("*"//SBR05 1/3 Insurance Type Code: Situational. Required when the payer in loop 2330B is Medicare and Medicare is not the primary payer. Medical and Dental only. TODO: implement.
						+"*"//SBR06 1/1 Coordination of Benefits Code: Not used.
						+"*"//SBR07 1/1 Yes/No Condition or Response Code: Not Used.
						+"*"//SBR08 2/2 Employment Status Code: Not Used.
						+"CI"//SBR09 1/2 Claim Filing Indicator Code: 12=PPO,17=DMO,BL=BCBS,CI=CommercialIns,FI=FEP,HM=HMO. There are too many. I'm just going to use CI for everyone. I don't think anyone will care.
						+endSegment);
					if(claim.ClaimType!="P") {
						double claimWriteoff=0;
						double claimDeductible=0;
						double claimPaidOtherIns=0;
						for(int j=0;j<claimProcs.Count;j++) {
							for(int k=0;k<claimProcList.Count;k++) {
								if(ClaimProcs.IsValidClaimAdj(claimProcList[k],claimProcs[j].ProcNum,claimProcs[j].InsSubNum)) {
									claimWriteoff+=claimProcList[k].WriteOff;
									claimDeductible+=claimProcList[k].DedApplied;
									claimPaidOtherIns+=claimProcList[k].InsPayAmt;
								}
							}
						}
						double claimPatientPortion=Math.Max(0,claim.ClaimFee-claimWriteoff-claimDeductible-claimPaidOtherIns);
						//2320 CAS: (medical,institutional,dental) Claim Level Adjustments. Situational. We use this to show patient responsibility, because the adjustments here plus AMT D below must equal claim amount in CLM02 for Emdeon.
						//Claim Adjustment Reason Codes can be found on the Washington Publishing Company website at: http://www.wpc-edi.com/reference/codelists/healthcare/claim-adjustment-reason-codes/
						if(claimWriteoff>0) {
							seg++;
							sw.WriteLine("CAS*CO*"//CAS01 1/2 Claim Adjustment Group Code: CO=Contractual Obligations.
								+"45*"//CAS02 1/5 Claim Adjustment Reason Code: 45=Charge exceeds fee schedule/maximum allowable or contracted/legislated fee arrangement.
								+AmountToStrNoLeading(claimWriteoff)//CAS03 1/18 Monetary Amount:
								+endSegment);
						}
						if(claimDeductible>0 || claimPatientPortion>0) {
							seg++;
							sw.Write("CAS*PR");//CAS01 1/2 Claim Adjustment Group Code: PR=Patient Responsibility.
							if(claimDeductible>0) {
								sw.Write("*"//end of previous field
									+"1*"//CAS02 1/5 Claim Adjustment Reason Code: 1=Deductible.
									+AmountToStrNoLeading(claimDeductible)+"*"//CAS03 1/18 Monetary Amount:
									+"1");//CAS04 1/15 Quantity:
							}
							if(claimPatientPortion>0) {
								sw.Write("*"//end of previous field
									+"3*"//CAS02 or CAS05 1/5 Claim Adjustment Reason Code: 3=Co-payment Amount.
									+AmountToStrNoLeading(claimPatientPortion));//CAS03 or CAS06 1/18 Monetary Amount:
							}
							sw.WriteLine(endSegment);
						}
						//2320 AMT: D (medical,institutional,dental) COB Payer Paid Amount. Situational. Required when the claim has been adjudicated by payer in loop 2330B.
						seg++;
						sw.WriteLine("AMT*D*"//AMT01 1/3 Amount Qualifier Code: D=Payor Amount Paid.
							+AmountToStrNoLeading(claimPaidOtherIns)//AMT02 1/18 Monetary Amount:
							+endSegment);//AMT03 Not used.
						//2320 AMT: EAF (medical,institutional,dental) Remaining Patient Liability. Situational. Required when claim has been adjudicated by payer in loop 2330B.
						seg++;
						sw.WriteLine("AMT*EAF*"//AMT01 1/3 Amount Qualifier Code: EAF=Amount Owed.
							+AmountToStrNoLeading(claimPatientPortion)//AMT02 1/18 Monetary Amount:
							+endSegment);//AMT03 Not used.
						//2320 AMT: A8 (medical,institutional,dental) COB Total Non-Covered Amount. Situational. Can be set when primary claim was not adjudicated. We do not use.
					}
					//2320 OI: (medical,institutional,dental) Other Insurance Coverage Information.
					seg++;
					sw.Write("OI*"
						+"*"//OI01 1/2 Claim Filing Indicator Code: Not used.
						+"*"//OI02 2/2 Claim Submission Reason Code: Not used.
						+(otherSub.AssignBen?"Y":"N")+"*"//OI03 1/1 Yes/No Condition or Response Code:
						+"*"//OI04 1/1 Patient Signature Source Code: Not used in institutional or dental. Situational in medical, but we do not support.
						+"*"//OI05 1/1 Provider Agreement Code: Not used.
						+(otherSub.ReleaseInfo?"Y":"I")//OI06 1/1 Release of Information Code:
						+endSegment);
					//2320 MIA: (institutional) Inpatient Adjudication Information. Situational. We do not support.
					//2320 MOA: (medical,institutional,dental) Outpatient Adjudication Information. Situational. For reporting remark codes from ERAs. We don't support.
					#endregion 2320 Other subscriber information
					#region 2330A Other subscriber Name
					//2330A NM1: IL (medical,institutional,dental) Other Subscriber Name.
					seg++;
					sw.WriteLine("NM1*IL*"//NM101 2/3 Entity Identifier Code: IL=Insured or Subscriber.
						+"1*"//NM102 1/1 Entity Type Qualifier: 1=Person.
						+Sout(otherSubsc.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
						+Sout(otherSubsc.FName,35)+"*"//NM104 1/35 Name First:
						+Sout(otherSubsc.MiddleI,25)+"*"//NM105 1/25 Middle Name:
						+"*"//NM106 1/10 Name Prefix: Not used.
						+"*"//NM107 1/10 Name Suffix: Situational. No corresponding field in OD.
						+"MI*"//NM108 1/2 Identification Code Qualifier: MI=Member Identification Number.
						+Sout(otherSub.SubscriberID,80)//NM109 2/80 Identification Code:
						+endSegment);//NM110 through NM112 are not used.
					//2330A N3: Other Subscriber Address.
					seg++;
					sw.Write("N3*"+Sout(otherSubsc.Address,55));//N301 1/55 Address Information:
					if(otherSubsc.Address2!="") {
						sw.Write("*"+Sout(otherSubsc.Address2,55));
					}
					sw.Write(endSegment);//N302 1/55 Address Information:
					//2330A N4: (medical,institutional,dental) Other Subscriber City, State, Zip Code.
					seg++;
					sw.WriteLine("N4*"
						+Sout(otherSubsc.City,30)+"*"//N401 2/30 City Name:
						+Sout(otherSubsc.State,2,2)+"*"//N402 2/2 State or Province Code:
						+Sout(otherSubsc.Zip,15)//N403 3/15 Postal Code:
						+endSegment);//N404 through N407 are either not required or are required when the address is outside of the United States.
					//2330A REF: (medical,institutional,dental) Other Subscriber Secondary Identification. Situational. Not supported.
					#endregion 2330A Other subscriber Name
					#region Other payer
					//2330B NM1: (medical,institutional,dental) Other Payer Name.
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
						 "*"//NM104 1/35 Name First: Not used.
						+"*"//NM105 1/25 Name Middle: Not used.
						+"*"//NM106 1/10 Name Prefix: Not used.
						+"*"//NM107 1/10 Name Suffix: Not used.
						+"PI*");//NM108 1/2 Identification Code Qualifier: PI=Payor Identification. XV must be used after national plan ID mandated.
					//NM109 2/80 Identification Code:
					if(otherCarrier.ElectID.Length<3) {
						sw.WriteLine("06126");
					}
					else {
						sw.WriteLine(Sout(otherCarrier.ElectID,80,2));
					}
					sw.WriteLine(endSegment);//NM110 through NM112 not used.
					//2230B N3: (medical,institutional,dental) Other Payer Address. Situational. We don't support.
					//2330B N4: (medical,institutional,dental) Other Payer City, State, Zip Code. Situational. We don't support.
					//2330B DTP: 573 (medical,institutional,dental) Claim Check or Remittance Date. Situational. Claim Paid date.
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
							+datePaidOtherIns.ToString("yyyyMMdd")//DTP03 1/35 Date Time Period:
							+endSegment);
					}
					//2330B REF: (medical,institutional,dental) Other Payer Secondary Identifier. Situational. We do not use.
					//2330B REF: G1 (medical,institutional,dental) Other Payer Prior Authorization Number. Situational. We do not use.
					//2330B REF: 9F (medical,institutional,dental) Other Payer Referral Number. Situational. We do not use.
					//2330B REF: T4 (medical,institutional,dental) Other Payer Claim Adjustment Indicator. Situational. We do not use.
					//2330B REF: G3 (dental) Other Payer Predetermination Identification. Situational. We do not use.
					//2230B REF: F8 (medical,institutional,dental) Other Payer Claim Control Number. Situational. We do not use.					
					if(medType==EnumClaimMedType.Medical) {
						//2330C NM1: (medical) Other Payer Referring Provider. Situational. Only used in crosswalking COBs. We do not use.
						//2330C REF: (medical) Other Payer Referring Provider Secondary Identification. We do not use.
						//2330D NM1: 82 (medical) Other Payer Rendering Provider. Situational. Only used in crosswalking COBs. We do not use.
						//2330D REF: (medical) Other Payer Rendering Provider Secondary Identificaiton. We do not use.
						//2330E NM1: 77 (medical) Other Payer Service Facility Location. Situational. We do not use.
						//2330E REF: (medical) Other Payer Service Facility Location Secondary Identification. We do not use.
						//2330F NM1: DQ (medical) Other Payer Supervising Provider. Situational. We do not use.
						//2330F REF: (medical) Other Payer Supervising Provider Secondary Identificaiton. We do not use.
						//2330G NM1: 85 (medical) Other Payer Billing Provider. Situational. We do not use.
						//2330G REF: (medical) Other Payer Billing Provider Secondary Identification. We do not use.
					}
					else if(medType==EnumClaimMedType.Institutional) {
						//2330C NM1: 71 (institutional) Other Payer Attending Provider. Situational. Only used in crosswalking COBs. We do not use.
						//2330C REF: (institutional) Other Payer Attending Provider Secondary Identification. We do not use.
						//2330D NM1: 72 (institutional) Other Payer Operating Physician. Situational.
						//2330D REF: (institutional) Other Payer Operating Physician Secondary Identificaiton. We do not use.
						//2330E NM1: ZZ (institutional) Other Payer Other Operating Physician. Situational. We do not use.
						//2330E REF: (institutional) Other Payer Other Operating Physician Secondary Identificaiton. We do not use.
						//2330F NM1: 77 (institutional) Other Payer Service Facility Location. Situational. We do not use.
						//2330F REF: (institutional) Other Payer Service Facility Location Secondary Identification. We do not use.
						//2330G NM1: 82 (institutional) Other Payer Rendering Provider Name. Situatiuonal. We do not use.
						//2330G REF: (institutional) Other Payer Rendering Provider Secondary Identificaiton. We do not use.
						//2330H NM1: DN (institutional) Other Payer Referring Provider. Situational. We do not use.
						//2330H REF: (institutional) Other Payer Referring Provider Secondary Identificaiton. We do not use.
						//2330I NM1: 85 (institutional) Other Payer Billing Provider. Situational. We do not use.
						//2330I REF: (institutional) Other Payer Billing Provider Secondary Identification. We do not use.
					}
					else if(medType==EnumClaimMedType.Dental) {
						//2330C NM1: (dental) Other Payer Referring Provider. Situational. Only used in crosswalking COBs. We do not use.
						//2330C REF: (dental) Other Payer Referring Provider Secondary Identification. We do not use.
						//2330D NM1: 82 (dental) Other Payer Rendering Provider. Situational. Only used in crosswalking COBs. We do not use.
						//2330D REF: (dental) Other Payer Rendering Provider Secondary Identificaiton. We do not use.
						//2330E NM1: DQ (dental) Other Payer Supervising Provider. Situational. We do not use.
						//2330E REF: (dental) Other Payer Supervising Provider Secondary Identificaiton. We do not use.
						//2330F NM1: 85 (dental) Other Payer Billing Provider. Situational. We do not use.
						//2330F REF: (dental) Other Payer Billing Provider Secondary Identification. We do not use.
						//2330G NM1: 77 (dental) Other Payer Service Facility Location. Situational. We do not use.
						//2330G REF: (dental) Other Payer Service Facility Location Secondary Identification. We do not use.
						//2330H NM1: DD (dental) Other Payer Assistant Sugeon. Situational. We do not use.
						//2330H REF: (dental) Other Payer Assistant Surgeon Secondary Identifier. We do not use.
					}
					#endregion Other payer
				}
				for(int j=0;j<claimProcs.Count;j++) {
					#region Service Line
					proc=Procedures.GetProcFromList(procList,claimProcs[j].ProcNum);
					procCode=ProcedureCodes.GetProcCode(proc.CodeNum);
					//2400 LX: Service Line Number.
					seg++;
					sw.WriteLine("LX*"+(j+1).ToString()//LX01 1/6 Assigned Number:
						+endSegment);
					if(medType==EnumClaimMedType.Medical) {
						//2400 SV1: Professional Service.
						seg++;
						sw.Write("SV1*"
							//SV101 Composite Medical Procedure Identifier
							+"HC:"//SV101-1 2/2 Product/Service ID Qualifier: HC=Health Care.
							+Sout(proc.MedicalCode));//SV101-2 1/48 Product/Service ID: Procedure code. The rest of SV101 is not supported
						if(proc.ClaimNote!="") {
							sw.Write(":"//SV101-3 2/2 Procedure Modifier: Situational. We do not use.
								+":"//SV101-4 2/2 Procedure Modifier: Situational. We do not use.
								+":"//SV101-5 2/2 Procedure Modifier: Situational. We do not use.
								+":"//SV101-6 2/2 Procedure Modifier: Situational. We do not use.
								+":"+Sout(proc.ClaimNote,80));//SV301-7 1/80 Description: Situational.
						}
						sw.Write("*");//SV101-8 is not used.
						sw.Write(claimProcs[j].FeeBilled.ToString()+"*"//SV102 1/18 Monetary Amount: Charge Amt.
							+"MJ*"//SV103 2/2 Unit or Basis for Measurement Code: MJ=minutes.
							+"0*");//SV104 1/15 Quantity: Quantity of anesthesia. We don't support, so always 0.							
						if(proc.PlaceService!=claim.PlaceService) {
							sw.Write(GetPlaceService(proc.PlaceService));
						}
						sw.Write("*");//SV105 1/2 Facility Code Value: Place of Service Code if different from claim.
						sw.Write("*");//SV106 1/2 Service Type Code: Not used.
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
						//SV107-2 through SV107-4: Other diagnoses, which we don't support yet.
						sw.WriteLine(endSegment);//SV108 through SV121 are not used or situational. We do not use.
					}
					else if(medType==EnumClaimMedType.Institutional) {
						//2400 SV2: Institutional Service Line.
						seg++;
						sw.Write("SV2*"
							+Sout(proc.RevCode,48)+"*"//SV201 1/48 Product/Service ID: Revenue Code, validated.
							//SV202 Composite Medical Procedure Identifier
							+"HC:"//SV202-1 2/2 Product/Service ID Qualifier: HC=Health Care. Includes CPT codes.
							+Sout(claimProcs[j].CodeSent));//SV202-2 1/48 Product/Service ID: Procedure code. 
						//mods validated to be exactly 2 char long or else blank.
						sw.Write(":"+Sout(proc.CodeMod1));//SV202-3 2/2 Procedure Modifier: Situational.
						sw.Write(":"+Sout(proc.CodeMod2));//SV202-4 2/2 Procedure Modifier: Situational.
						sw.Write(":"+Sout(proc.CodeMod3));//SV202-5 2/2 Procedure Modifier: Situational.
						sw.Write(":"+Sout(proc.CodeMod4));//SV202-6 2/2 Procedure Modifier: Situational.
						sw.Write(":"+Sout(proc.ClaimNote,80));//SV202-7 1/80 Description: Situational.
						sw.WriteLine("*"//SV202-8 is not used.
							+claimProcs[j].FeeBilled.ToString()+"*"//SV203 1/18 Monetary Amount: Charge Amt.
							+"UN*"//SV204 2/2 Unit or Basis for Measurement Code: UN=Unit. We don't support Days yet.
							+proc.UnitQty.ToString()//SV205 1/15 Quantity:
							+endSegment);//SV206,208,209 and 210 are not used, SV207 is situational but we do not use.
					}
					else if(medType==EnumClaimMedType.Dental) {
						//2400 SV3: Dental Service.
						seg++;
						sw.Write("SV3*"
								+"AD:"//SV301-1 2/2 Product/Service ID Qualifier: AD=American Dental Association Codes
								+Sout(claimProcs[j].CodeSent,5));//SV301-2 1/48 Product/Service ID: Procedure code
						if(proc.ClaimNote!="") {
							sw.Write(":"//SV301-3 2/2 Procedure Modifier: Situational. We do not use.
								+":"//SV301-4 2/2 Procedure Modifier: Situational. We do not use.
								+":"//SV301-5 2/2 Procedure Modifier: Situational. We do not use.
								+":"//SV301-6 2/2 Procedure Modifier: Situational. We do not use.
								+":"+Sout(proc.ClaimNote,80));//SV301-7 1/80 Description: Situational.
						}
						sw.Write("*"//SV301-8 is not used.
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
							+endSegment);//SV307 throug SV311 are either not used or are situational and we do not use.
						//2400 TOO: Tooth Information. Number/Surface.
						if(procCode.TreatArea==TreatmentArea.Tooth) {
							seg++;
							sw.WriteLine("TOO*JP*"//TOO01 1/3 Code List Qualifier Code: JP=Universal National Tooth Designation System.
								+proc.ToothNum//TOO02 1/30 Industry Code: Tooth number.
								+endSegment);//TOO03 Tooth Surface: Situational. Not applicable.
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
							sw.WriteLine(endSegment);
						}
						else if(procCode.TreatArea==TreatmentArea.ToothRange) {
							string[] individTeeth=proc.ToothRange.Split(',');
							for(int t=0;t<individTeeth.Length;t++) {
								seg++;
								sw.WriteLine("TOO*JP*"//TOO01 1/3 Code List Qualifier Code: JP=Universal National Tooth Designation System.
									+individTeeth[t]//TOO02 1/30 Industry Code: Tooth number.
									+endSegment);//TOO03 Tooth Surface: Situational. Not applicable.
							}
						}
					}//dental
					#endregion Service Line
					//2400 PWK: (institutional) Line Supplemental Information. Situational. We do not use.
					//2400 CRC: (medical) Condition Indicator/Durable Medical Equipment. Situational. We do not use.
					#region Service DTP
					//2400 DTP: 472 (medical,institutional,dental) Service Date. Situaitonal. Required for medical. Required if different from claim. Emdeon complains if this date is specified at is not needed.
					if(claim.ClaimType!="PreAuth" && proc.ProcDate!=claim.DateService) {
						seg++;
						sw.WriteLine("DTP*472*"//DTP01 3/3 Date/Time Qualifier: 472=Service.
							+"D8*"//DTP02 2/3 Date Time Period Format Qualifier: D8=Date Expressed in Format CCYYMMDD.
							+proc.ProcDate.ToString("yyyyMMdd")//DTP03 1/35 Date Time Period:
							+endSegment);
					}
					//2400 DTP: 139/441 (dental) Date Prior Placement. Situational. Required when replacement.
					if(proc.Prosthesis=="R") {//already validated date
						seg++;
						sw.WriteLine("DTP*441*"//DTP01 3/3 Date/Time Qualifier: 441=Prior Placement.
							+"D8*"//DTP02 2/3 Date Time Period Format Qualifier: D8=Date Expressed in Format CCYYMMDD.
							+proc.DateOriginalProsth.ToString("yyyyMMdd")//DTP03 1/35 Date Time Period:
							+endSegment);
					}
					//2400 DTP: 452 (dental) Date Appliance Placement. Situational. Ortho appliance placement. We do not use.
					//2400 DTP: 446 (dental) Date Replacement. Date ortho appliance replaced. We do not use.
					//2400 DTP: 196 (medical,dental) Date Treatment Start. Situational. Rx date. We do not use.
					//2400 DTP: 198 (dental) Date Treatment Completion. Situational. We do not use.
					//2400 DTP: 471 (medical) Prescription Date: Situational. We do not use.
					//2400 DTP: 607 (medical) Date Certification Revision/Recertification. Situational. Not supported.
					//2400 DTP: 463 (medical) Date Begin Therapy. Situational. Not supported.
					//2400 DTP: 461 (medical) Date Last Certification. Situational. Not supported.
					//2400 DTP: 304 (medical) Date Last Seen. Situational. Not supported.
					//2400 DTP: 738/739 (medical) Test Date. Situational. For Dialysis. Not supported.
					//2400 DTP: 011 (medical) Date Shipped. Situational. Not supported.
					//2400 DTP: 455 (medical) Date Last X-Ray. Situational. Not supported.
					//2400 DTP: 454 (medical) Date Initial Treatment. Situational. Not supported.					
					#endregion Service DTP
					#region Service QTY MEA CN1
					//2400 QTY: PT (medical) Ambulance Patient Count. Situational. Not supported.
					//2400 QTY: FL (medical) Obstetric Anesthesia Additional Units. Situational. Anesthesia quantity. We do not use.
					//2400 MEA: (medical) Test Result. Situational. We do not use.
					//2400 CN1: (medical,dental) Contract Information. Situational. We do not use.
					#endregion Service QTY MEA CN1
					#region Service REF
					//2400 REF: G3 (dental) Service Predetermination Identification. Situational. Pretermination ID. We do not use.
					//2400 REF: G1 (medical,dental) Prior Authorization. Situational. We do not use.
					//2400 REF: 9F (medical,dental) Referral Number. Situational. We do not use.
					//2400 REF: 9A (dental) Repriced Claim Number. Situational. We do not use.
					//2400 REF: 9B (medical,institutional) Repriced Line Item Reference Number. Situational. We do not use.
					//2400 REF: 9C (dental) Adjusted Repriced claim Number. Situational. We do not use.
					//2400 REF: 9D (medical,instituitonal) Adjusted Repriced Line Item Reference Number. Situational. We do not use.
					//2400 REF: 6R (medical,institutional,dental) Line Item Control Number. ProcNum. Will later be used for ERAs.
					seg++;
					sw.WriteLine("REF*6R*"//REF01 2/3 Reference Identification Qualifier: 6R=Procedure Control Number.
						+proc.ProcNum.ToString()//REF02 1/50 Reference Identification: 
						+endSegment);//REF03 and REF04 are not used.
					//2400 REF: EW (medical) Mammography Certification Number. Situational. We do not use.
					//2400 REF: X4 (medical) Clinical Laboratory Improvement Amendment (CLIA) Number. Situational. We do not use.
					//2400 REF: F4 (medical) Referring Clinical Laboratory Improvement Amendment (CLIA) Facility Identification. Situational. We do not use.
					//2400 REF: BT (medical) Immunization Batch Number. Situational. We do not use.
					#endregion Service REF
					#region Service AMT K3 NTE PS1 HCP LIN CTP
					//2400 AMT: T (medical,dental) Sales Tax Amount. Situational. Not supported.
					//2400 AMT: F4 (medical) Postage Claimed Amount. Situational. We do not use.
					//2400 AMT GT (institutional) Service Tax Amount. Situational. Not supported.
					//2400 AMT N8 (institutional) Facility Tax Amount. Situational. Not supported.
					//2400 K3: (medical,dental) File Information. Situational. Not supported.
					//2400 NTE: ADD/DCP (medical) Line Note. Situational. We do not use.
					//2400 NTE TPO (medical,institutional) Third Party Organization Notes. Situational. Not sent by providers. Not supported.
					//2400 PS1: (medical) Purchased Service Information. Situational. We do not use.
					//2400 HCP: (medical,institutional,dental) Line Pricing/Repricing Information. Situational. Not used by providers. Not supported.
					#endregion Service AMT K3 NTE PS1 HCP
					#region 2410 Service Drug Identification
					//2410 LIN,CTP,REF: (medical) ?
					if(medType==EnumClaimMedType.Medical || medType==EnumClaimMedType.Institutional) {
						//2410 LIN: (medical,institutional) Drug Identification
						if(procCode.DrugNDC!="" && proc.DrugQty>0){
							seg++;
							sw.WriteLine("LIN**"//LIN01 1/20 Assigned Identification: Not used.
								+"N4*"//LIN02 2/2 Product/Service ID Qualifier: N4=NDC code in 5-4-2 format, no dashes.
								+procCode.DrugNDC//LIN03 1/48 Product/Service ID: NDC.
								+endSegment);//LIN04 through LIN31 not used.
							//2410 CTP: (medical,institutional) Drug Quantity.
							seg++;
							sw.WriteLine("CTP****"//CTP01 through CTP03 not used.
								+proc.DrugQty.ToString()+"*"//CTP04 1/15 Quantity:
								+GetDrugUnitCode(proc.DrugUnit)//CTP05-1 2/2 Unit or Basis for Measurement Code: Code Qualifier, validated to not be None.
								+endSegment);//CTP05-2 through CTP05-15 not used. CTP06 through CTP11 not used.
							//2410 REF (inst) Rx or compound drug association number.  Not supported.
						}
					}
					#endregion 2410 Service Drug Identification
					//2410 REF: VY/XZ (medical,institutional) Prescription or Compound Drug Association Number. Situational. We do not use.
					#region 2420 Service Providers (medical)
					if(medType==EnumClaimMedType.Medical) {
						if(claim.ProvTreat!=proc.ProvNum
							&& PrefC.GetBool(PrefName.EclaimsSeparateTreatProv)) {
							//2420A NM1: 82 (medical) Rendering Provider Name. Only if different from the claim.
							provTreat=Providers.GetProv(proc.ProvNum);
							seg++;
							sw.WriteLine("NM1*82*"//NM101 2/3 Entity Identifier Code: 82=Rendering Provider.
								+"1*"//NM102 1/1 Entity Type Qualifier: 1=Person.
								+Sout(provTreat.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
								+Sout(provTreat.FName,35)+"*"//NM104 1/35 Name First:
								+Sout(provTreat.MI,25)+"*"//NM105 1/25 Name Middle:
								+"*"//NM106 1/10 Name Prefix: Not used.
								+"*"//NM107 1/10 Name Suffix: Situational. Not Supported.
								+"XX*"//NM108 1/2 Identification Code Qualifier: XX=NPI. After NPI date, so always use NPI.
								+Sout(provTreat.NationalProvID,80,2)//NM109 2/80 Identification Code: NPI validated.
								+endSegment);//NM110 through NM112 not used.
							//2420A PRV: (medical) Rendering Provider Specialty Information.
							seg++;
							sw.Write("PRV*PE*");//PRV01 1/3 Provider Code: PE=Performing.
							sw.Write("PXC*");//PRV02 2/3 Reference Identification Qualifier: PXC=Health Care Provider Taxonomy Code.
							sw.WriteLine(X12Generator.GetTaxonomy(provTreat)//PRV03 1/50 Reference Identification: Taxonomy Code.
								+endSegment);//PRV04 through PRV06 not used.
							//2420A REF: (medical) Rendering Provider Secondary Identification.
							seg++;
							sw.WriteLine("REF*0B*"//REF01 2/3 Reference Identification Qualifier: 0B=State License Number.
								+Sout(provTreat.StateLicense,50)+"*"//REF02 1/50 Reference Identification: 
								+"*"//REF03 1/80 Description: Not used.
								+endSegment);//REF04 Reference Identifier: Situational. Not used when REF01 is 0B or 1G.
						}
						//2420B NM1: Purchased Service Provider Name. Situational. We do not use.
						//2420B REF: Purchased Service Provider Secondary Identificaiton. Situational. We do not use.
						//2420C NM1: 77 (medical) Service Facility Location Name. Situational. We enforce all procs on a claim being performed at the same location so we don't need this.
						//2420C N3: (medical) Service Facility Location Address. We do not use.
						//2420C N4: (medical) Service Facility Location City, State, Zip Code. We do not use.
						//2420C REF: (medical) Service Facility Location Secondary Identification. Situational. We do not use.
						//2420D NM1: DQ (medical) Supervising Provider Name. Situational. We do not support.
						//2420D REF: (medical) Supervising Provider Secondary Identification. Situational. We do not support.
						//2420E NM1: DK (medical) Ordering Provider Name. Situational. We do not use.
						//2420E N3: (medical) Ordering Provider Address. Situational. We do not use.
						//2420E N4: (medical) Ordering Provider City, State, Zip Code. Situational. We do not use.
						//2420E REF: (medical) Ordering Provider Secondary Identification. Situational. We do not use.
						//2420E PER: (medical) Ordering Provider Contact Information. Situational. We do not use.
						//2420F NM1: (medical) Referring Provider Name. Situational. We do not use.
						//2420F REF: (medical) Referring Provider Secondary Identification. Situational. We do not use.
						//2420G NM1: PW (medical) Ambulance Pick-up Location. Situational. We do not use.
						//2420G N3: (medical) Ambulance Pick-up Location Address. We do not use.
						//2420G N4: (medical) Ambulance Pick-up Location City, State, Zip Code. We do not use.
						//2420H NM1: (medical) Ambulance Drop-off Location. Situational. We do not use.
						//2420H N3: (medical) Ambulance Drop-off Location Address. We do not use.
						//2420H N4: (medical) Ambulance Drop-off Location City, State, Zip Code. We do not use.
					}
					#endregion 2420 Service Providers (medical)
					#region 2420 Service Providers (inst)
					if(medType==EnumClaimMedType.Institutional) {
						//2420A NM1: 72 (institutional) Operating Physician Name. Situational. Only for surgical procedures. We don't support.
						//2420A REF: (instititional) Operating Physician Secondary Identification. Situational. Only for surgical procedures. We don't support.						
						//2420B NM1: ZZ (institutional) Other Operating Physician Name. Situational. We don't support.
						//2420B REF: (institutional) Other Operating Physician Secondary Identification. Situational. We don't support.
						if(claim.ProvTreat!=proc.ProvNum
							&& PrefC.GetBool(PrefName.EclaimsSeparateTreatProv)) 
						{
							provTreat=Providers.GetProv(proc.ProvNum);
							//2420C NM1: 82 (institutional) Rendering Provider Name. Situational. Only if different than claim attending (treating) prov.
							seg++;
							sw.WriteLine("NM1*82*"//NM101 2/3 Entity Identifier Code: 82=Rendering Provider.
								+"1*"//NM102 1/1 Entity Type Qualifier: 1=Person. Validated.
								+Sout(provTreat.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
								+Sout(provTreat.FName,35)+"*"//NM104 1/35 Name First:
								+Sout(provTreat.MI,25)+"*"//NM105 1/25 Name Middle:
								+"*"//NM106 1/10 Name Prefix: Not used.
								+"*"//NM107 1/10 Name Suffix: Situational. Not supported.
								+"XX*"//NM108 1/2 Identification Code Qualifer: XX=Centers for Medicare and Medicaid Services National Provider Identifier (NPI).
								+Sout(provTreat.NationalProvID,80,2)//NM109 2/80 Identification Code: ID. NPI validated.
								+endSegment);//NM110 through NM112 not used.
							//2420C REF: Rendering Provider Secondary Identification. Situational.
							seg++;
							sw.WriteLine("REF*0B*"//REF01 2/3 Reference Identification Qualifier: 0B=State License Number.
								+Sout(provTreat.StateLicense,50)//REF02 1/50 Reference Identification: Valided to be present.
								+"*");//REF03 through REF04 are not used or situational.
						}
						//2420D NM1: DN (institutional) Referring Provider Name. Situational. We do not use.
						//2420D REF: (institutional) Referring Provider Secondary Identification. Situational. We do not use.
					}
					#endregion 2420 Service Providers (inst)
					#region 2420 Service Providers (dental)
					if(medType==EnumClaimMedType.Dental) {
						if(claim.ProvTreat!=proc.ProvNum
							&& PrefC.GetBool(PrefName.EclaimsSeparateTreatProv)) 
						{
							//2420A NM1: 82 (dental) Rendering Provider Name. Only if different from the claim.
							provTreat=Providers.GetProv(proc.ProvNum);
							seg++;
							sw.WriteLine("NM1*82*"//NM101 2/3 Entity Identifier Code: 82=Rendering Provider.
								+"1*"//NM102 1/1 Entity Type Qualifier: 1=Person.
								+Sout(provTreat.LName,60)+"*"//NM103 1/60 Name Last or Organization Name:
								+Sout(provTreat.FName,35)+"*"//NM104 1/35 Name First:
								+Sout(provTreat.MI,25)+"*"//NM105 1/25 Name Middle:
								+"*"//NM106 1/10 Name Prefix: Not used.
								+"*"//NM107 1/10 Name Suffix: Situational. Not Supported.
								+"XX*"//NM108 1/2 Identification Code Qualifier: XX=NPI. After NPI date, so always use NPI.
								+Sout(provTreat.NationalProvID,80,2)//NM109 2/80 Identification Code: NPI validated.
								+endSegment);//NM110 through NM112 not used.
							//2420A PRV: (dental) Rendering Provider Specialty Information.
							seg++;
							sw.Write("PRV*PE*");//PRV01 1/3 Provider Code: PE=Performing.
							sw.Write("PXC*");//PRV02 2/3 Reference Identification Qualifier: PXC=Health Care Provider Taxonomy Code.
							sw.WriteLine(X12Generator.GetTaxonomy(provTreat)//PRV03 1/50 Reference Identification: Taxonomy Code.
								+endSegment);//PRV04 through PRV06 not used.
							//2420A REF: (dental) Rendering Provider Secondary Identification.
							seg++;
							sw.WriteLine("REF*0B*"//REF01 2/3 Reference Identification Qualifier: 0B=State License Number.
								+Sout(provTreat.StateLicense,50)+"*"//REF02 1/50 Reference Identification: 
								+"*"//REF03 1/80 Description: Not used.
								+endSegment);//REF04 Reference Identifier: Situational. Not used when REF01 is 0B or 1G.
						}
						//2420B NM1: DD (dental) Assistant Surgeon Name. Situational. We do not support.
						//2420B PRV: AS (dental) Assistant Surgeon Specialty Information. Situational. We do not support.
						//2420B REF: (dental) Assistant Surgeon Secondary Identification. Situational. We do not support.
						//2420C NM1: DQ (dental) Supervising Provider Name. Situational. We do not support.
						//2420C REF: (dental) Supervising Provider Secondary Identification. Situational. We do not support.
						//2420D NM1: 77 (dental) Service Facility Location Name. Situational. We enforce all procs on a claim being performed at the same location so we don't need this.
						//2420D N3: (dental) Service Facility Location Address. We do not use.
						//2420D N4: (dental) Service Facility Location City, State, Zip Code. We do not use.
						//2420D REF: (dental) Service Facility Location Secondary Identification. Situational. We do not use.
					}
					#endregion 2420 Service Providers (dental)
					//2430 SVD: (medical,institutional,dental) Line Adjudication Information. Situational. We do not support.
					//2430 CAS: (medical,institutional,dental) Line Adjustment. Situational. We do not support.
					//2430 DTP: (medical,institutional,dental) Line Check or Remittance Date. We do not support.
					//2430 AMT: (medical,institutional,dental) Remaining Patient Liability. We do not support.
					//2440 LQ: (medical) Form Identification Code. Situational. We do not use.
					//2440 FRM: (medical) Supporting Documentation. We do not use.
				}
			}
			#region Trailers
			//Transaction Trailer
			seg++;
			sw.WriteLine("SE*"
				+seg.ToString()+"*"//SE01 1/10 Number of Included Segments: Total segments, including ST & SE.
				+transactionNum.ToString().PadLeft(4,'0')//SE02 4/9 Transaction Set Control Number:
				+endSegment);
			//Functional Group Trailer
			sw.WriteLine("GE*"+transactionNum.ToString()+"*"//GE01 1/6 Number of Transaction Sets Included:
				+groupControlNumber//GE02 1/9 Group Control Number: Must be identical to GS06.
				+endSegment);
			#endregion Trailers
		}

		private static bool IsEmdeon(Clearinghouse clearinghouse) {
			return (clearinghouse.ISA08=="0135WCH00" || clearinghouse.ISA08=="133052274");
		}

		///<summary>Sometimes writes the name information for Open Dental. Sometimes it writes practice info.</summary>
		private static void Write1000A_NM1(StreamWriter sw,Clearinghouse clearhouse) {
			string name="OPEN DENTAL SOFTWARE";
			string idCode="810624427";
			if(clearhouse.SenderTIN!="") {
				name=clearhouse.SenderName;
				idCode=clearhouse.SenderTIN;
			}
			sw.WriteLine("NM1*41*"//NM101 2/3 Entity Indentifier Code: 41=submitter
				+"2*"//NM102 1/1 Entity Type Qualifier: 2=Non-Person Entity.
				+Sout(name,60)+"*"//NM103 1/60 Name Last or Organization Name: 
				+"*"//NM104 1/35 Name First: Situational.
				+"*"//NM105 1/25 Name Middle: Situational.
				+"*"//NM106 1/10 Name Prefix: Not Used.
				+"*"//NM107 1/10 Name Suffix: Not Used.
				+"46*"//NM108 1/2 Identification Code Qualifier: 46=Electronic Transmitter Identification Number (ETIN).
				+Sout(idCode,80,2)//NM109 2/80 Identification Code: ETIN#. Validated to be at least 2.
				+endSegment);//NM110 through NM112 are not used.
		}

		///<summary>Usually writes the contact information for Open Dental. But for inmediata and AOS clearinghouses, it writes practice contact info.</summary>
		private static void Write1000A_PER(StreamWriter sw,Clearinghouse clearhouse) {
			string phone="8776861248";
			if(clearhouse.SenderTIN!="") {
				phone=clearhouse.SenderTelephone;
			}
			sw.WriteLine("PER*IC*"//PER01 2/2 Contact Function Code: IC=Information Contact.
				+"*"//PER02 1/60 Name: Situational. Do not send since same as in NM1 segment for loop 1000A.
				+"TE*"//PER03 2/2 Communication Number Qualifier: TE=Telephone.
				+phone//PER04 1/256 Communication Number: Telephone Number. Validated to be exactly 10 digits.
				+endSegment);//PER05 through PER08 are situational. We do not use. PER09 is not used.
		}

		///<summary>Generates SiteID REF G5 for Emdeon only. Returns number of segments generated.</summary>
		private static int Write2010AASiteIDforEmdeon(StreamWriter sw,Provider prov,string payorID) {
			ProviderIdent[] provIdents=ProviderIdents.GetForPayor(prov.ProvNum,payorID);
			for(int i=0;i<provIdents.Length;i++) {
				if(provIdents[i].SuppIDType==ProviderSupplementalID.SiteNumber) {
					sw.WriteLine("REF*"
						+"G5*"//REF01 2/3 Reference Identification Qualifier: 
						+Sout(provIdents[i].IDNumber,50)//REF02 1/50 Reference Identification:
						+endSegment);//REF03 and REF04 are not used.
					return 1;
				}
			}
			return 0;
		}

		///<summary>This is depedent only on the electronic payor id # rather than the clearinghouse. Used for billing prov and also for treating prov. Writes 0 or 1 G2 segments. Returns the number of segments written.</summary>
		private static int WriteProv_REFG2(StreamWriter sw,Provider prov,string payorID) {
			string provID="";
			ElectID electID=ElectIDs.GetID(payorID);
			if(electID!=null && electID.IsMedicaid) {
				provID=prov.MedicaidID;
			}
			else {
				ProviderIdent[] provIdents=ProviderIdents.GetForPayor(prov.ProvNum,payorID);//Should always return 1 value unless user set it up wrong.
				if(provIdents.Length>0) {
					provID=provIdents[0].IDNumber;
				}
			}
			if(provID!="") {
				sw.WriteLine("REF*"
		      +"G2*"//REF01 2/3 Reference Identification Qualifier: 1D=Medicaid.
		      +Sout(provID,50)//REF02 1/50 Reference Identification:
		      +endSegment);//REF03 and REF04 are not used.
				return 1;
			}
			return 0;
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

		private static string GetStudentEmdeon(string studentStatus) {
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
		public static void Validate(ClaimSendQueueItem queueItem){//,out string warning) {
			StringBuilder strb=new StringBuilder();
			string warning="";
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
			//billProv
			if(billProv.LName=="") {
				Comma(strb);
				strb.Append("Billing Prov LName");
			}
			if(!billProv.IsNotPerson && billProv.FName=="") {//this is allowed to be blank if it's a non-person.
				Comma(strb);
				strb.Append("Billing Prov FName");
			}
			if(billProv.SSN.Length<2) {
				Comma(strb);
				strb.Append("Billing Prov SSN");
			}
			if(billProv.NationalProvID.Length<2) {
				Comma(strb);
				strb.Append("Billing Prov NPI");
			}
			if(CultureInfo.CurrentCulture.Name.EndsWith("US")) {//United States
				if(!Regex.IsMatch(billProv.SSN,"^[0-9]{9}$")) {
					Comma(strb);
					strb.Append("Billing Prov SSN/TIN must be a 9 digit number");
				}
				if(!Regex.IsMatch(billProv.NationalProvID,"^(80840)?[0-9]{10}$")) {
					Comma(strb);
					strb.Append("Billing Prov NPI must be a 10 digit number with an optional prefix of 80840");
				}
			}
			if(PrefC.GetBool(PrefName.UseBillingAddressOnClaims)) {
				string zip=PrefC.GetString(PrefName.PracticeBillingZip);
				if(!Regex.IsMatch(zip,"^[0-9]{5}\\-?[0-9]{4}$")) {
					Comma(strb);
					strb.Append("Practice billing zip must contain nine digits");
				}
				if(Regex.IsMatch(PrefC.GetString(PrefName.PracticeBillingAddress),".*P\\.?O\\.? .*",RegexOptions.IgnoreCase)) {
					Comma(strb);
					strb.Append("Practice billing address cannot be a P.O. BOX when used for e-claims.");
				}
			}
			else if(clinic==null) {
				string zip=PrefC.GetString(PrefName.PracticeZip);
				if(!Regex.IsMatch(zip,"^[0-9]{5}\\-?[0-9]{4}$")) {
					Comma(strb);
					strb.Append("Practice zip must contain nine digits");
				}
				if(Regex.IsMatch(PrefC.GetString(PrefName.PracticeAddress),".*P\\.?O\\.? .*",RegexOptions.IgnoreCase)) {
					Comma(strb);
					strb.Append("Practice address cannot be a P.O. BOX when used for e-claims.");
				}
			}
			else {
				string zip=clinic.Zip;
				if(!Regex.IsMatch(zip,"^[0-9]{5}\\-?[0-9]{4}$")) {
					Comma(strb);
					strb.Append("Clinic zip must contain nine digits");
				}
				if(Regex.IsMatch(clinic.Address,".*P\\.?O\\.? .*",RegexOptions.IgnoreCase)) {
					Comma(strb);
					strb.Append("Clinic address cannot be a P.O. BOX when used for e-claims.");
				}
			}
			//treatProv
			if(treatProv.LName=="") {
				Comma(strb);
				strb.Append("Treating Prov LName for claim");
			}
			if(treatProv.FName=="") {
				Comma(strb);
				strb.Append("Treating Prov FName for claim");
			}
			if(treatProv.SSN.Length<2) {
				Comma(strb);
				strb.Append("Treating Prov SSN/TIN for claim");
			}
			if(treatProv.NationalProvID.Length<2) {
				Comma(strb);
				strb.Append("Treating Prov NPI for claim");
			}
			if(CultureInfo.CurrentCulture.Name.EndsWith("US")) {//United States
				if(!Regex.IsMatch(treatProv.SSN,"^[0-9]{9}$")) {
					Comma(strb);
					strb.Append("Treating Prov SSN/TIN for claim must be a 9 digit number");
				}
				if(!Regex.IsMatch(treatProv.NationalProvID,"^(80840)?[0-9]{10}$")) {
					Comma(strb);
					strb.Append("Treating Prov NPI for claim must be a 10 digit number with an optional prefix of 80840");
				}
			}
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
						strb.Append("Treating Prov LName for proc "+procCode.ProcCode);
					}
					if(treatProv.FName=="") {
						Comma(strb);
						strb.Append("Treating Prov FName for proc "+procCode.ProcCode);
					}
					if(treatProv.IsNotPerson) {
						Comma(strb);
						strb.Append("Treating Prov IsNotPerson for proc "+procCode.ProcCode);//required to be a person
					}
					if(treatProv.SSN.Length<2) {
						Comma(strb);
						strb.Append("Treating Prov SSN/TIN for proc "+procCode.ProcCode);
					}
					if(treatProv.NationalProvID.Length<2) {
						Comma(strb);
						strb.Append("Treating Prov NPI for proc "+procCode.ProcCode);
					}
					if(CultureInfo.CurrentCulture.Name.EndsWith("US")) {//United States
						if(!Regex.IsMatch(treatProv.SSN,"^[0-9]{9}$")) {
							Comma(strb);
							strb.Append("Treating Prov SSN/TIN for proc "+procCode.ProcCode+" must be a 9 digit number");
						}
						if(!Regex.IsMatch(treatProv.NationalProvID,"^(80840)?[0-9]{10}$")) {
							Comma(strb);
							strb.Append("Treating Prov NPI for proc "+procCode.ProcCode+" must be a 10 digit number with an optional prefix of 80840");
						}
					}
					//will add any other checks as needed. Can't think of any others at the moment.
				}
			}//for int i claimProcs
			if(claim.MedType==EnumClaimMedType.Medical && !princDiagExists) {
				Comma(strb);
				strb.Append("Princ Diagnosis");
			}

			/*
						if(==""){
							Comma(strb);
							strb.Append("";
						}*/

			//return strb.ToString();
			queueItem.Warnings=warning;
			queueItem.MissingData=strb.ToString();
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


		///<summary>Removes leading zeros in numbers such as 0.00 or 0.99, for Emdeon and maybe others.</summary>
		private static string AmountToStrNoLeading(double amount) {
			string result=amount.ToString("F");
			int i=0;
			while(i<result.Length-1 && result[i]=='0') {
				i++;
			}
			return result.Substring(i);
		}


	}
}
