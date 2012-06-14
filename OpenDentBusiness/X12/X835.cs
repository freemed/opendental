using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenDentBusiness
{
	///<summary>X12 835 Health Care Claim Payment/Advice. This transaction type is a response to an 837 claim submission. The 835 will always come after a 277 is received and a 277 will always come after a 999. Neither the 277 nor the 999 are required, so it is possible that an 835 will be received directly after the 837.</summary>
	public class X835:X12object{

    private List<X12Segment> segments;
//    ///<summary>NM1 of loop 2100A.</summary>
//    private int segNumInfoSourceNM101;
//    ///<summary>NM1 of loop 2100B.</summary>
//    private int segNumInfoReceiverNM101;
//    ///<summary>NM1 of loop 2100C.</summary>
//    private List<int> segNumsBillingProviderNM1;
//    ///<summary>NM1 of loop 2100D.</summary>
//    private List<int> segNumsPatientDetailNM1;
//    ///<summary>TRN of loop 2200D.</summary>
//    private List<int> segNumsClaimTrackingNumberTRN;

    public static bool Is835(X12object xobj) {
//      if(xobj.FunctGroups.Count!=1) {
//        return false;
//      }
//      if(xobj.FunctGroups[0].Header.Get(1)=="HN") {
//        return true;
//      }
      return false;
    }

    public X835(string messageText):base(messageText) {
      segments=FunctGroups[0].Transactions[0].Segments;
//      segNumInfoSourceNM101=-1;
//      segNumInfoReceiverNM101=-1;
//      segNumsBillingProviderNM1=new List<int>();
//      segNumsPatientDetailNM1=new List<int>();
//      segNumsClaimTrackingNumberTRN=new List<int>();
//      for(int i=0;i<segments.Count;i++) {
//        X12Segment seg=segments[i];
//        if(seg.SegmentID=="NM1") {
//          string entityIdentifierCode=seg.Get(1);
//          if(entityIdentifierCode=="AY" || entityIdentifierCode=="PR") {
//            segNumInfoSourceNM101=i;
//            i+=4;
//          }
//          else if(entityIdentifierCode=="41") {
//            segNumInfoReceiverNM101=i;
//            i+=3;
//            seg=segments[i];
//            while(seg.SegmentID=="STC") {
//              i++;
//              seg=segments[i];
//            }
//            i+=4;
//          }
//          else if(entityIdentifierCode=="85") {
//            segNumsBillingProviderNM1.Add(i);
//          }
//          else if(entityIdentifierCode=="QC") {
//            segNumsPatientDetailNM1.Add(i);
//            //Loop 2200D: There can be multiple TRN segments for each NM1*QC.
//            do {
//              i++;
//              segNumsClaimTrackingNumberTRN.Add(i);//a TRN segment is required at this location.
//              i++;
//              seg=segments[i];//at least one STC segment is required at this location.
//              while(seg.SegmentID=="STC") {//there may be multiple STC segments.
//                i++;
//                seg=segments[i];
//              }
//              //Followed by 0 to 3 situational REF segments.
//              for(int j=0;j<3 && (seg.SegmentID=="REF");j++) {
//                i++;
//                seg=segments[i];
//              }
//              //Followed by 0 or 1 DTP segments. 
//              if(seg.SegmentID=="DTP") {
//                i++;
//                seg=segments[i];
//              }
//              //An entire iteration of loop 2200D is now finished. If another iteration is present, it will begin with a TRN segment.
//            } while(seg.SegmentID=="TRN");
//          }
//        }
//      }
    }

//    ///<summary>NM101 of loop 2100A.</summary>
//    public string GetInformationSourceType() {
//      if(segNumInfoSourceNM101!=-1) {
//        if(segments[segNumInfoSourceNM101].Get(1)=="AY") {
//          return "Clearinghouse";
//        }
//        return "Payor";
//      }
//      return "";
//    }

//    ///<summary>NM103 of loop 2100A.</summary>
//    public string GetInformationSourceName() {
//      if(segNumInfoSourceNM101!=-1) {
//        return segments[segNumInfoSourceNM101].Get(3);
//      }
//      return "";
//    }

//    ///<summary>DTP03 of loop 2200A.</summary>
//    public DateTime GetInformationSourceReceiptDate() {
//      if(segNumInfoSourceNM101!=-1) {
//        try {
//          string dateStr=segments[segNumInfoSourceNM101+2].Get(3);
//          int dateYear=PIn.Int(dateStr.Substring(0,4));
//          int dateMonth=PIn.Int(dateStr.Substring(4,2));
//          int dateDay=PIn.Int(dateStr.Substring(6,2));
//          return new DateTime(dateYear,dateMonth,dateDay);
//        }
//        catch {
//        }
//      }
//      return DateTime.MinValue;
//    }

//    ///<summary>DTP03 of loop 2200A.</summary>
//    public DateTime GetInformationSourceProcessDate() {
//      if(segNumInfoSourceNM101!=-1) {
//        try {
//          string dateStr=segments[segNumInfoSourceNM101+3].Get(3);
//          int dateYear=PIn.Int(dateStr.Substring(0,4));
//          int dateMonth=PIn.Int(dateStr.Substring(4,2));
//          int dateDay=PIn.Int(dateStr.Substring(6,2));
//          return new DateTime(dateYear,dateMonth,dateDay);
//        }
//        catch {
//        }
//      }
//      return DateTime.MinValue;
//    }

//    ///<summary>Last STC segment in loop 2200B. Returns -1 on error.</summary>
//    private int GetSegNumLastSTC2200B() {
//      if(segNumInfoReceiverNM101!=-1) {
//        int segNum=segNumInfoReceiverNM101+2;
//        X12Segment seg=segments[segNum];
//        while(seg.SegmentID=="STC") {
//          segNum++;
//          //End of message can happen because the QTY and AMT segments are situational, and so are the two HL segments after this.
//          if(segNum>=segments.Count) {
//            return segNum-1;
//          }
//          seg=segments[segNum];
//        }
//        return segNum-1;
//      }
//      return -1;
//    }

//    ///<summary>QTY02 of loop 2200B.</summary>
//    public long GetQuantityAccepted() {
//      int segNum=GetSegNumLastSTC2200B();
//      if(segNum!=-1) {
//        segNum++;
//        if(segNum<segments.Count) {
//          X12Segment seg=segments[segNum];
//          if(seg.SegmentID=="QTY" && seg.Get(1)=="90") {
//            return long.Parse(seg.Get(2));
//          }
//        }
//      }
//      return 0;
//    }

//    ///<summary>QTY02 of loop 2200B.</summary>
//    public long GetQuantityRejected() {
//      int segNum=GetSegNumLastSTC2200B();
//      if(segNum!=-1) {
//        segNum++;
//        if(segNum<segments.Count) {
//          X12Segment seg=segments[segNum];
//          if(seg.SegmentID=="QTY") {
//            try {
//              if(seg.Get(1)=="AA") {
//                return long.Parse(seg.Get(2));
//              }
//              else {
//                segNum++;
//                if(segNum<segments.Count) {
//                  seg=segments[segNum];
//                  if(seg.SegmentID=="QTY" && seg.Get(1)=="AA") {
//                    return long.Parse(seg.Get(2));
//                  }
//                }
//              }
//            }
//            catch {
//            }
//          }
//        }
//      }
//      return 0;
//    }

//    ///<summary>AMT02 of loop 2200B.</summary>
//    public double GetAmountAccepted() {
//      int segNum=GetSegNumLastSTC2200B();
//      if(segNum!=-1) {
//        segNum++;
//        if(segNum<segments.Count) {
//          X12Segment seg=segments[segNum];
//          while(seg.SegmentID=="QTY") {
//            segNum++;
//            if(segNum>=segments.Count) {
//              return 0;
//            }
//            seg=segments[segNum];
//          }
//          if(seg.SegmentID=="AMT" && seg.Get(1)=="YU") {
//            return double.Parse(seg.Get(2));
//          }
//        }
//      }
//      return 0;
//    }

//    ///<summary>AMT02 of loop 2200B.</summary>
//    public double GetAmountRejected() {
//      int segNum=GetSegNumLastSTC2200B();
//      if(segNum!=-1) {
//        segNum++;
//        if(segNum<segments.Count) {
//          X12Segment seg=segments[segNum];
//          while(seg.SegmentID=="QTY") {
//            segNum++;
//            if(segNum>=segments.Count) {
//              return 0;
//            }
//            seg=segments[segNum];
//          }
//          if(seg.SegmentID=="AMT") {
//            if(seg.Get(1)=="YY") {
//              return double.Parse(seg.Get(2));
//            }
//            else {
//              segNum++;
//              if(segNum<segments.Count) {
//                seg=segments[segNum];
//                if(seg.SegmentID=="AMT" && seg.Get(1)=="YY") {
//                  return double.Parse(seg.Get(2));
//                }
//              }
//            }
//          }
//        }
//      }
//      return 0;
//    }

//    ///<summary>TRN02 in loop 2200D. Do this first to get a list of all claim tracking numbers that are contained within this 835.  Then, for each claim tracking number, we can later retrieve the AckCode for that single claim. The claim tracking numbers correspond to CLM01 exactly as submitted in the 837. We refer to CLM01 as the claim identifier on our end. We allow more than just digits in our claim identifiers, so we must return a list of strings.</summary>
//    public List<string> GetClaimTrackingNumbers() {
//      List<string> retVal=new List<string>();
//      for(int i=0;i<segNumsClaimTrackingNumberTRN.Count;i++) {
//        X12Segment seg=segments[segNumsClaimTrackingNumberTRN[i]];//TRN segment.
//        retVal.Add(seg.Get(2));
//      }
//      return retVal;
//    }

//    ///<summary>Result will contain strings in the following order: Patient Last Name (NM103), Patient First Name (NM104), Patient Middle Name (NM105), Claim Status (STC03), Payor's Claim Control Number (REF02), Institutional Type of Bill (REF02), Claim Date Service Start (DTP03), Claim Date Service End (DTP03).</summary>
//    public string[] GetClaimInfo(string trackingNumber) {
//      string[] result=new string[8];
//      for(int i=0;i<result.Length;i++) {
//        result[i]="";
//      }
//      for(int i=0;i<segNumsClaimTrackingNumberTRN.Count;i++) {
//        int segNum=segNumsClaimTrackingNumberTRN[i];
//        X12Segment seg=segments[segNum];//TRN segment.
//        if(seg.Get(2)==trackingNumber) { //TRN02
//          //Locate the NM1 segment corresponding to the claim tracking number. One NM1 segment can be shared with multiple TRN segments.
//          //The strategy is to locate the NM1 segment furthest down in the message that is above the TRN segment for the tracking number.
//          int segNumNM1=segNumsPatientDetailNM1[segNumsPatientDetailNM1.Count-1];//very last NM1 segment
//          for(int j=0;j<segNumsPatientDetailNM1.Count-1;j++) {
//            if(segNum>segNumsPatientDetailNM1[j] && segNum<segNumsPatientDetailNM1[j+1]) {
//              segNumNM1=segNumsPatientDetailNM1[j];
//              break;
//            }
//          }
//          seg=segments[segNumNM1];//NM1 segment.
//          result[0]=seg.Get(3);//NM103 Last Name
//          result[1]=seg.Get(4);//NM104 First Name
//          result[2]=seg.Get(5);//NM105 Middle Name
//          segNum++;
//          seg=segments[segNum];//STC segment. At least one, maybe multiple, but we only care about the first one.
//          if(seg.Get(3)=="WQ") { //STC03 = WQ
//            result[3]="A";
//          }
//          else { //STC03 = U
//            result[3]="R";
//          }
//          //Skip the remaining STC segments (if any).
//          segNum++;
//          seg=segments[segNum];
//          while(seg.SegmentID=="STC") {
//            segNum++;
//            seg=segments[segNum];
//          }
//          while(seg.SegmentID=="REF") {
//            string refIdQualifier=seg.Get(1);
//            if(refIdQualifier=="1K") {
//              result[4]=seg.Get(2);//REF02 Payor's Claim Control Number.
//            }
//            else if(refIdQualifier=="D9") {
//              //REF02 Claim Identifier Number for Clearinghouse and Other Transmission Intermediary from the 837.
//              //When we send this it is the same as the claim identifier/claim tracking number, so we don't use this for now.
//            }
//            else if(refIdQualifier=="BLT") {
//              //REF02 Institutional Type of Bill that was sent in the 837.
//              result[5]=seg.Get(2);
//            }
//            segNum++;
//            seg=segments[segNum];
//          }
//          //The DTP segment for the date of service will not be present when an invalid date was originally sent to the carrier (even though the specifications have it marked as a required segment).
//          if(seg.SegmentID=="DTP") {
//            string dateServiceStr=seg.Get(3);
//            int dateServiceStartYear=PIn.Int(dateServiceStr.Substring(0,4));
//            int dateServiceStartMonth=PIn.Int(dateServiceStr.Substring(4,2));
//            int dateServiceStartDay=PIn.Int(dateServiceStr.Substring(6,2));
//            result[6]=(new DateTime(dateServiceStartYear,dateServiceStartMonth,dateServiceStartDay)).ToShortDateString();
//            if(dateServiceStr.Length==17) { //Date range.
//              int dateServiceEndYear=PIn.Int(dateServiceStr.Substring(9,4));
//              int dateServiceEndMonth=PIn.Int(dateServiceStr.Substring(13,2));
//              int dateServiceEndDay=PIn.Int(dateServiceStr.Substring(15,2));
//              result[7]=(new DateTime(dateServiceEndYear,dateServiceEndMonth,dateServiceEndDay)).ToShortDateString();
//            }
//          }
//        }
//      }
//      return result;
//    }

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