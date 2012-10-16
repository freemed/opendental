using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenDentBusiness
{
	///<summary>X12 835 Health Care Claim Payment/Advice. This transaction type is a response to an 837 claim submission. The 835 will always come after a 277 is received and a 277 will always come after a 999. Neither the 277 nor the 999 are required, so it is possible that an 835 will be received directly after the 837.</summary>
	public class X835:X12object{

    private List<X12Segment> segments;
		///<summary>CLP of loop 2100. Claim payment information.</summary>
		private List<int> segNumsCLP;
		///<summary>SCC of loop 2110. Service (procedure) payment information.</summary>
		private List<int> segNumsSVC;

    public static bool Is835(X12object xobj) {
      if(xobj.FunctGroups.Count!=1) {
        return false;
      }
      if(xobj.FunctGroups[0].Header.Get(1)=="HP") {
        return true;
      }
      return false;
    }

    public X835(string messageText):base(messageText) {
      segments=FunctGroups[0].Transactions[0].Segments;
			segNumsCLP=new List<int>();
			segNumsSVC=new List<int>();
			for(int i=0;i<segments.Count;i++) { //All segments which have unique names within the 835 format can go inside this loop.
				if(segments[i].SegmentID=="CLP") { //The only place CLP segments exist is within the 2100 loop.
					segNumsCLP.Add(i);
				}
				else if(segments[i].SegmentID=="SVC") { //The only place SVC segments exist is within the 2110 loop.
					segNumsSVC.Add(i);
				}
			}
    }

		///<summary>CLP01 in loop 2100. Referred to in this format as a Patient Control Number. Do this first to get a list of all claim tracking numbers that are contained within this 835.  Then, for each claim tracking number, we can later retrieve specific information for that single claim. The claim tracking numbers correspond to CLM01 exactly as submitted in the 837. We refer to CLM01 as the claim identifier on our end. We allow more than just digits in our claim identifiers, so we must return a list of strings.</summary>
		public List<string> GetClaimTrackingNumbers() {
			List<string> retVal=new List<string>();
			for(int i=0;i<segNumsCLP.Count;i++) {
				X12Segment seg=segments[segNumsCLP[i]];//CLP segment.
				retVal.Add(seg.Get(1));//CLP01
			}
			return retVal;
		}

//    ///<summary>Result will contain strings in the following order: Claim Status Code (CLP02), Monetary Amount of submitted charges for this claim (CLP03), Monetary Amount paid on this claim (CLP04), Monetary Amount of patient responsibility (CLP05), Payer Claim Control Number (CLP07).</summary>
    public string[] GetClaimInfo(string trackingNumber) {
      string[] result=new string[5];
      for(int i=0;i<result.Length;i++) {
        result[i]="";
      }
      for(int i=0;i<segNumsCLP.Count;i++) {
        int segNum=segNumsCLP[i];
				X12Segment seg=segments[segNum];//CLP segment.
				if(seg.Get(1)!=trackingNumber) {//CLP01
					continue;
				}
				result[0]=seg.Get(2);//CLP02
				result[1]=seg.Get(3);//CLP03
				result[2]=seg.Get(4);//CLP04
				result[3]=seg.Get(5);//CLP05
				result[4]=seg.Get(7);//CLP07
				break;
      }
      return result;
    }

//    public string GetHumanReadable() {
//      string result=
//        "Claim Status Reponse From "+GetInformationSourceType()+" "+GetInformationSourceName()+Environment.NewLine
//        +"Receipt Date: "+GetInformationSourceReceiptDate().ToShortDateString()+Environment.NewLine
//        +"Process Date: "+GetInformationSourceProcessDate().ToShortDateString()+Environment.NewLine
//        +"Quantity Accepted: "+GetQuantityAccepted()+Environment.NewLine
//        +"Quantity Rejected: "+GetQuantityRejected()+Environment.NewLine
//        +"Amount Accepted: "+GetAmountAccepted()+Environment.NewLine
//        +"Amount Rejected: "+GetAmountRejected()+Environment.NewLine
//        +"Individual Claim Status List: "+Environment.NewLine
//        +"Tracking Num  LName  FName  MName  Status  PayorControlNum  InstBillType DateServiceStart  DateServiceEnd";
//      List <string> claimTrackingNumbers=GetClaimTrackingNumbers();
//      for(int i=0;i<claimTrackingNumbers.Count;i++) {
//        string[] claimInfo=GetClaimInfo(claimTrackingNumbers[i]);
//        for(int j=0;j<claimInfo.Length;j++) {
//          result+=claimInfo[j]+"\\t";
//        }
//        result+=Environment.NewLine;
//      }
//      return result;
//    }

  }
}

//Example 1 From 835 Specification:
//ST*835*1234~
//BPR*C*150000*C*ACH*CTX*01*999999992*DA*123456*1512345678*01*999988880*DA*98765*20020913~
//TRN*1*12345*1512345678~
//DTM*405*20020916~
//N1*PR*INSURANCE COMPANY OF TIMBUCKTU~
//N3*1 MAIN STREET~
//N4*TIMBUCKTU*AK*89111~
//REF*2U*999~
//N1*PE*REGIONAL HOPE HOSPITAL*XX*6543210903~
//LX*110212~
//TS3*6543210903*11*20021231*1*211366.97****138018.4**73348.57~
//TS2*2178.45*1919.71**56.82*197.69*4.23~
//CLP*666123*1*211366.97*138018.4**MA*1999999444444*11*1~
//CAS*CO*45*73348.57~
//NM1*QC*1*JONES*SAM*O***HN*666666666A~
//MIA*0***138018.4~
//DTM*232*20020816~
//DTM*233*20020824~
//QTY*CA*8~
//LX*130212~
//TS3*6543210909*13*19961231*1*15000****11980.33**3019.67~
//CLP*777777*1*150000*11980.33**MB*1999999444445*13*1~
//CAS*CO*35*3019.67~
//NM1*QC*1*BORDER*LIZ*E***HN*996669999B~
//MOA***MA02~
//DTM*232*20020512~
//PLB*6543210903*20021231*CV:CP*-1.27~
//SE*28*1234~

//Example 2 From 835 Specification:
//ST*835*12233~
//BPR*I*945*C*ACH*CCP*01*888999777*DA*24681012*1935665544*01*111333555*DA*144444*20020316~
//TRN*1*71700666555*1935665544~
//DTM*405*20020314~
//N1*PR*RUSHMORE LIFE~
//N3*10 SOUTH AVENUE~
//N4*RAPID CITY*SD*55111~
//N1*PE*ACME MEDICAL CENTER*XX*5544667733~
//REF*TJ*777667755~
//LX*1~
//CLP*55545554444*1*800*450*300*12*94060555410000~
//CAS*CO*A2*50~
//NM1*QC*1*BUDD*WILLIAM****MI*33344555510~
//SVC*HC:99211*800*500~
//DTM*150*20020301~
//DTM*151*20020304~
//CAS*PR*1*300~
//CLP*8765432112*1*1200*495*600*12*9407779923000~
//CAS*CO*A2*55~
//NM1*QC*1*SETTLE*SUSAN****MI*44455666610~
//SVC*HC:93555*1200*550~
//DTM*150*20020310~
//DTM*151*20020312~
//CAS*PR*1*600~
//CAS*CO*45*50~
//SE*25*112233~

//Example 3 From 835 Specification:
//ST*835*0001~
//BPR*I*1222*C*CHK************20050412~
//TRN*1*0012524965*1559123456~
//REF*EV*030240928~
//DTM*405*20050412~
//N1*PR*YOUR TAX DOLLARS AT WORK~
//N3*481A00 DEER RUN ROAD~
//N4*WEST PALM BCH*FL*11114~
//N1*PE*ACME MEDICAL CENTER*FI*5999944521~
//N3*PO BOX 863382~
//N4*ORLANDO*FL*55115~
//REF*PQ*10488~
//LX*1~
//CLP*L0004828311*2*10323.64*912**12*05090256390*11*1~
//CAS*OA*23*9411.64~
//NM1*QC*1*TOWNSEND*WILLIAM*P***MI*XXX123456789~
//NM1*82*2*ACME MEDICAL CENTER*****BD*987~
//DTM*232*20050303~
//DTM*233*20050304~
//AMT*AU*912~
//LX*2~
//CLP*0001000053*2*751.50*310*220*12*50630626430~
//NM1*QC*1*BAKI*ANGI****MI*456789123~
//NM1*82*2*SMITH JONES PA*****BS*34426~
//DTM*232*20050106~
//DTM*233*20050106~
//SVC*HC>12345>26*166.5*30**1~
//DTM*472*20050106~
//CAS*OA*23*136.50~
//REF*1B*43285~
//AMT*AU*150~
//SVC*HC>66543>26*585*280*220*1~
//DTM*472*20050106~
//CAS*PR*1*150**2*70~
//CAS*CO*42*85~
//REF*1B*43285~
//AMT*AU*500~
//SE*38*0001~

//Example 4 From 835 Specification:
//ST*835*0001~
//BPR*I*187.50*C*CHK************20050412~
//TRN*1*0012524879*1559123456~
//REF*EV*030240928~
//DTM*405*20050412~
//N1*PR*YOUR TAX DOLLARS AT WORK~
//N3*481A00 DEER RUN ROAD~
//N4*WEST PALM BCH*FL*11114~
//N1*PE*ACME MEDICAL CENTER*FI*599944521~
//N3*PO BOX 863382~
//N4*ORLANDO*FL*55115~
//REF*PQ*10488~
//LX*1~
//CLP*0001000054*3*1766.5*187.50**12*50580155533~
//NM1*QC*1*ISLAND*ELLIS*E****MI*789123456~
//NM1*82*2*JONES JONES ASSOCIATES*****BS*AB34U~
//DTM*232*20050120~
//SVC*HC*24599*1766.5*187.50**1~
//DTM*472*20050120~
//CAS*OA*23*1579~
//REF*1B*44280~
//AMT*AU*1700~
//SE*38*0001~

//Example 5 From 835 Specification:
//ST*835*0001~
//BPR*I*34.00*C*CHK************20050318~
//TRN*1*0063158ABC*1566339911~
//REF*EV*030240928~
//DTM*405*20050318~
//N1*PR*YOUR TAX DOLLARS AT WORK~
//N3*481A00 DEER RUN ROAD~
//N4*WEST PALM BCH*FL*11114~
//N1*PE*ATONEWITHHEALTH*FI*3UR334563~
//N3*3501 JOHNSON STREET~
//N4*SUNSHINE*FL*12345~
//REF*PQ*11861~
//LX*1~
//CLP*0001000055*2*541*34**12*50650619501~
//NM1*QC*1*BRUCK*RAYMOND*W***MI*987654321~
//NM1*82*2*PROFESSIONAL TEST 1*****BS*34426~
//DTM*232*20050202~
//DTM*233*20050202~
//SVC*HC>55669*541*34**1~
//DTM*472*20050202~
//CAS*OA*23*516~
//CAS*OA*94*-9~
//REF*1B*44280~
//AMT*AU*550~
//SE*38*0001~