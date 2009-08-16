using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness
{
	///<summary></summary>
	public class X270:X12object{

		public X270(string messageText):base(messageText){
		
		}

		///<summary>In progress.  Probably needs a different name.  Info must be validated first.</summary>
		public static string GenerateMessageText(Clearinghouse clearhouse,Carrier carrier,Provider billProv,Clinic clinic,InsPlan insPlan,Patient subscriber) {
			int batchNum=Clearinghouses.GetNextBatchNumber(clearhouse);
			string groupControlNumber=batchNum.ToString();//Must be unique within file.  We will use batchNum
			int transactionNum=1;
			StringBuilder strb=new StringBuilder();
			//Interchange Control Header
			strb.AppendLine("ISA*00*          *"//ISA01,ISA02: 00 + 10 spaces
				+"00*          *"//ISA03,ISA04: 00 + 10 spaces
				+clearhouse.ISA05+"*"//ISA05: Sender ID type: ZZ=mutually defined. 30=TIN. Validated
				+X12Generator.GetISA06(clearhouse)+"*"//ISA06: Sender ID(TIN). Or might be TIN of Open Dental
				+clearhouse.ISA07+"*"//ISA07: Receiver ID type: ZZ=mutually defined. 30=TIN. Validated
				+Sout(clearhouse.ISA08,15,15)+"*"//ISA08: Receiver ID. Validated to make sure length is at least 2.
				+DateTime.Today.ToString("yyMMdd")+"*"//ISA09: today's date
				+DateTime.Now.ToString("HHmm")+"*"//ISA10: current time
				+"U*00401*"//ISA11 and ISA12. 
				//ISA13: interchange control number, right aligned:
				+batchNum.ToString().PadLeft(9,'0')+"*"
				+"0*"//ISA14: no acknowledgment requested
				+clearhouse.ISA15+"*"//ISA15: T=Test P=Production. Validated.
				+":~");//ISA16: use ':'
			//Functional Group Header
			strb.AppendLine("GS*HS*"//GS01: HS for 270 benefit inquiry
				+X12Generator.GetGS02(clearhouse)+"*"//GS02: Senders Code. Sometimes Jordan Sparks.  Sometimes the sending clinic.
				+Sout(clearhouse.GS03,15,2)+"*"//GS03: Application Receiver's Code
				+DateTime.Today.ToString("yyyyMMdd")+"*"//GS04: today's date
				+DateTime.Now.ToString("HHmm")+"*"//GS05: current time
				+groupControlNumber+"*"//GS06: Group control number. Max length 9. No padding necessary.
				+"X*"//GS07: X
				+"004010X092~");//GS08: Version
			//Beginning of transaction--------------------------------------------------------------------------------
			int seg=0;//count segments for the ST-SE transaction
			//Transaction Set Header
			//ST02 Transact. control #. Must be unique within ISA
			seg++;
			strb.AppendLine("ST*270*"//ST01
				+transactionNum.ToString().PadLeft(4,'0')+"~");//ST02
			seg++;
			strb.AppendLine("BHT*0022*13*"//BHT02: 13=request
				+transactionNum.ToString().PadLeft(4,'0')+"*"//BHT03. Can be same as ST02
				+DateTime.Now.ToString("yyyyMMdd")+"*"//BHT04: Date
				+DateTime.Now.ToString("HHmmss")+"~");//BHT05: Time, BHT06: not used
			//HL Loops-----------------------------------------------------------------------------------------------
			int HLcount=1;
			//2000A HL: Information Source--------------------------------------------------------------------------
			seg++;
			strb.AppendLine("HL*"+HLcount.ToString()+"*"//HL01: Heirarchical ID.  Here, it's always 1.
				+"*"//HL02: No parent. Not used
				+"20*"//HL03: Heirarchical level code. 20=Information source
				+"1~");//HL04: Heirarchical child code. 1=child HL present
			//2100A NM1
			seg++;
			strb.AppendLine("NM1*PR*"//NM101: PR=Payer
				+"2*"//NM102: 2=Non person
				+Sout(carrier.CarrierName,35)+"*"//NM103: Name Last.
				+"****"//NM104-07 not used
				+"PI*"//NM108: PI=PayorID
				+Sout(carrier.ElectID,80,2)+"~");//NM109: PayorID
			HLcount++;
			//2000B HL: Information Receiver------------------------------------------------------------------------
			seg++;
			strb.AppendLine("HL*"+HLcount.ToString()+"*"//HL01: Heirarchical ID.  Here, it's always 2.
				+"1*"//HL02: Heirarchical parent id number.  1 in this simple message.
				+"21*"//HL03: Heirarchical level code. 21=Information receiver
				+"1~");//HL04: Heirarchical child code. 1=child HL present
			seg++;
			//2100B NM1: Information Receiver Name
			strb.AppendLine("NM1*1P*"//NM101: 1P=Provider
				+"1*"//NM102: 1=person,2=non-person
				+Sout(billProv.LName,35)+"*"//NM103: Last name
				+Sout(billProv.FName,25)+"*"//NM104: First name
				+Sout(billProv.MI,25,1)+"*"//NM105: Middle name
				+"*"//NM106: not used
				+"*"//NM107: Name suffix. not used
				+"XX*"//NM108: ID code qualifier. 24=EIN. 34=SSN, XX=NPI
				+Sout(billProv.NationalProvID,80)+"~");//NM109: ID code. NPI validated
			//2100B REF: Information Receiver ID
			seg++;
			strb.Append("REF*");
			if(billProv.UsingTIN) {
				strb.Append("TJ*");//REF01: qualifier. TJ=Federal TIN
			}
			else {//SSN
				strb.Append("SY*");//REF01: qualifier. SY=SSN
			}
			strb.AppendLine(Sout(billProv.SSN,30)+"~");//REF02: ID 
			//2100B N3: Information Receiver Address
			seg++;
			if(PrefC.GetBool("UseBillingAddressOnClaims")) {
				strb.Append("N3*"+Sout(PrefC.GetString("PracticeBillingAddress"),55));//N301: Address
			}
			else if(clinic==null) {
				strb.Append("N3*"+Sout(PrefC.GetString("PracticeAddress"),55));//N301: Address
			}
			else {
				strb.Append("N3*"+Sout(clinic.Address,55));//N301: Address
			}
			if(PrefC.GetBool("UseBillingAddressOnClaims")) {
				if(PrefC.GetString("PracticeBillingAddress2")=="") {
					strb.AppendLine("~");
				}
				else {
					//N302: Address2. Optional.
					strb.AppendLine("*"+Sout(PrefC.GetString("PracticeBillingAddress2"),55)+"~");
				}
			}
			else if(clinic==null) {
				if(PrefC.GetString("PracticeAddress2")=="") {
					strb.AppendLine("~");
				}
				else {
					//N302: Address2. Optional.
					strb.AppendLine("*"+Sout(PrefC.GetString("PracticeAddress2"),55)+"~");
				}
			}
			else {
				if(clinic.Address2=="") {
					strb.AppendLine("~");
				}
				else {
					//N302: Address2. Optional.
					strb.AppendLine("*"+Sout(clinic.Address2,55)+"~");
				}
			}
			//2100B N4: Information Receiver City/State/Zip
			seg++;
			if(PrefC.GetBool("UseBillingAddressOnClaims")) {
				strb.AppendLine("N4*"+Sout(PrefC.GetString("PracticeBillingCity"),30)+"*"//N401: City
					+Sout(PrefC.GetString("PracticeBillingST"),2)+"*"//N402: State
					+Sout(PrefC.GetString("PracticeBillingZip").Replace("-",""),15)+"~");//N403: Zip
			}
			else if(clinic==null) {
				strb.AppendLine("N4*"+Sout(PrefC.GetString("PracticeCity"),30)+"*"//N401: City
					+Sout(PrefC.GetString("PracticeST"),2)+"*"//N402: State
					+Sout(PrefC.GetString("PracticeZip").Replace("-",""),15)+"~");//N403: Zip
			}
			else {
				strb.AppendLine("N4*"+Sout(clinic.City,30)+"*"//N401: City
					+Sout(clinic.State,2)+"*"//N402: State
					+Sout(clinic.Zip.Replace("-",""),15)+"~");//N403: Zip
			}
			//2100B PRV: Information Receiver Provider Info
			seg++;
			//PRV*PE*ZZ*1223G0001X~
			strb.AppendLine("PRV*PE*"//PRV01: Provider Code. PE=Performing.  There are many other choices.
				+"ZZ*"//PRV02: ZZ=Mutually defined = health care provider taxonomy code
				+X12Generator.GetTaxonomy(billProv.Specialty)+"~");//PRV03: Specialty code
			HLcount++;
			//2000C HL: Subscriber-----------------------------------------------------------------------------------
			seg++;
			strb.AppendLine("HL*"+HLcount.ToString()+"*"//HL01: Heirarchical ID.  Here, it's always 3.
				+"2*"//HL02: Heirarchical parent id number.  2 in this simple message.
				+"22*"//HL03: Heirarchical level code. 22=Subscriber
				+"0~");//HL04: Heirarchical child code. 0=no child HL present (no dependent)
			//2000C TRN: Subscriber Trace Number
			seg++;
			strb.AppendLine("TRN*1*"//TRN01: Trace Type Code.  1=Current Transaction Trace Numbers
				+"1*"//TRN02: Trace Number.  We don't really have a good primary key yet.  Keep it simple. Use 1.
				+"1"+billProv.SSN+"~");//TRN03: Entity Identifier. First digit is 1=EIN.  Next 9 digits are EIN.  Length validated.
			//2000C NM1: Subscriber Name
			seg++;
			strb.AppendLine("NM1*IL*"//NM101: IL=Insured or Subscriber
				+"1*"//NM102: 1=Person
				+Sout(subscriber.LName,35)+"*"//NM103: LName
				+Sout(subscriber.FName,25)+"*"//NM104: FName
				+Sout(subscriber.MiddleI,25)+"*"//NM105: MiddleName
				+"*"//NM106: not used
				+"*"//NM107: suffix. Not present in Open Dental yet.
				+"MI*"//NM108: MI=MemberID
				+Sout(insPlan.SubscriberID.Replace("-",""),80)+"~");//NM109: Subscriber ID. Validated to be L>2.
			//2000C DMG: Subscriber Demographic Information
			seg++;
			strb.AppendLine("DMG*D8*"//DMG01: Date Time Period Qualifier.  D8=CCYYMMDD
				+subscriber.Birthdate.ToString("yyyyMMdd")+"~");//DMG02: Subscriber birthdate.  Validated
				//DMG03: Gender code.  Situational.  F or M.  Since this was left out in the example,
				//and since we don't want to send the wrong gender, we will not send this element.
			//2000C DTP: Subscriber Date.  Deduced through trial and error that this is required by EHG even though not by X12 specs.
			seg++;
			strb.AppendLine("DTP*307*"//DTP01: Qualifier.  307=Eligibility
				+"D8*"//DTP02: Format Qualifier.
				+DateTime.Today.ToString("yyyyMMdd")+"~");//DTP03: Date
			//2110C EQ: Subscriber Eligibility or Benefit Enquiry Information
			//We can loop this 99 times to request very specific benefits.
			seg++;
			strb.AppendLine("EQ*30~");//EQ01: 30=General Coverage
			//Transaction Trailer
			seg++;
			strb.AppendLine("SE*"
				+seg.ToString()+"*"//SE01: Total segments, including ST & SE
				+transactionNum.ToString().PadLeft(4,'0')+"~");
			//End of transaction--------------------------------------------------------------------------------------
			//Functional Group Trailer
			strb.AppendLine("GE*"+transactionNum.ToString()+"*"//GE01: Number of transaction sets included
				+groupControlNumber+"~");//GE02: Group Control number. Must be identical to GS06
			//Interchange Control Trailer
			strb.AppendLine("IEA*1*"//IEA01: number of functional groups
				+batchNum.ToString().PadLeft(9,'0')+"~");//IEA02: Interchange control number
			return strb.ToString();
			/*
			return @"
ISA*00*          *00*          *30*AA0989922      *30*330989922      *030519*1608*U*00401*000012145*1*T*:~
GS*HS*AA0989922*330989922*20030519*1608*12145*X*004010X092~
ST*270*0001~
BHT*0022*13*ASX012145WEB*20030519*1608~
HL*1**20*1~
NM1*PR*2*Metlife*****PI*65978~
HL*2*1*21*1~
NM1*1P*1*PROVLAST*PROVFIRST****XX*1234567893~
REF*TJ*200384584~
N3*JUNIT ROAD~
N4*CHICAGO*IL*60602~
PRV*PE*ZZ*1223G0001X~
HL*3*2*22*0~
TRN*1*12145*1AA0989922~
NM1*IL*1*SUBLASTNAME*SUBFIRSTNAME****MI*123456789~
DMG*D8*19750323~
DTP*307*D8*20030519~
EQ*30~
SE*17*0001~
GE*1*12145~
IEA*1*000012145~";
			*/

			//return "ISA*00*          *00*          *30*AA0989922      *30*330989922      *030519*1608*U*00401*000012145*1*T*:~GS*HS*AA0989922*330989922*20030519*1608*12145*X*004010X092~ST*270*0001~BHT*0022*13*ASX012145WEB*20030519*1608~HL*1**20*1~NM1*PR*2*Metlife*****PI*65978~HL*2*1*21*1~NM1*1P*1*PROVLAST*PROVFIRST****XX*1234567893~REF*TJ*200384584~N3*JUNIT ROAD~N4*CHICAGO*IL*60602~PRV*PE*ZZ*1223G0001X~HL*3*2*22*0~TRN*1*12145*1AA0989922~NM1*IL*1*SUBLASTNAME*SUBFIRSTNAME****MI*123456789~DMG*D8*19750323~DTP*307*D8*20030519~EQ*30~SE*17*0001~GE*1*12145~IEA*1*000012145~";
		}

		

		///<summary>Converts any string to an acceptable format for X12. Converts to all caps and strips off all invalid characters. Optionally shortens the string to the specified length and/or makes sure the string is long enough by padding with spaces.</summary>
		private static string Sout(string inputStr,int maxL,int minL) {
			return X12Generator.Sout(inputStr,maxL,minL);
		}

		///<summary>Converts any string to an acceptable format for X12. Converts to all caps and strips off all invalid characters. Optionally shortens the string to the specified length and/or makes sure the string is long enough by padding with spaces.</summary>
		private static string Sout(string str,int maxL) {
			return X12Generator.Sout(str,maxL,-1);
		}

		///<summary>Converts any string to an acceptable format for X12. Converts to all caps and strips off all invalid characters. Optionally shortens the string to the specified length and/or makes sure the string is long enough by padding with spaces.</summary>
		private static string Sout(string str) {
			return X12Generator.Sout(str,-1,-1);
		}

		public static string Validate(Clearinghouse clearhouse,Carrier carrier,Provider billProv,Clinic clinic,InsPlan insPlan,Patient subscriber) {
			StringBuilder strb=new StringBuilder();
			X12Validate.ISA(clearhouse,strb);
			X12Validate.Carrier(carrier,strb);
			if(carrier.ElectID=="") {
				if(strb.Length!=0) {
					strb.Append(",");
				}
				strb.Append("Electronic ID");
			}
			if(billProv.SSN.Length!=9) {
				if(strb.Length!=0) {
					strb.Append(",");
				}
				strb.Append("Prov TIN 9 digits");
			}
			X12Validate.BillProv(billProv,strb);
			if(clinic==null) {
				X12Validate.PracticeAddress(strb);
			}
			else {
				X12Validate.Clinic(clinic,strb);
			}
			if(insPlan.SubscriberID.Length<2) {
				if(strb.Length!=0) {
					strb.Append(",");
				}
				strb.Append("SubscriberID");
			}
			if(subscriber.Birthdate.Year<1880) {
				if(strb.Length!=0) {
					strb.Append(",");
				}
				strb.Append("Subscriber Birthdate");
			}
			return strb.ToString();
		}
		


	}
}
