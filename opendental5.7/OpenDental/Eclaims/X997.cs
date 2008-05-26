using System;
using System.Collections.Generic;
using OpenDentBusiness;

namespace OpenDental.Eclaims{
	///<summary></summary>
	public class X997:X12object{

		public X997(string messageText):base(messageText){
		
		}
		
		///<summary>In X12 lingo, the batchNumber is known as the functional group.</summary>
		public int GetBatchNumber(){
			if(this.FunctGroups[0].Transactions.Count!=1) {
				return 0;
			}
			X12Segment seg=FunctGroups[0].Transactions[0].GetSegmentByID("AK1");
			if(seg==null) {
				return 0;
			}
			string num=seg.Get(2);
			try{
				return PIn.PInt(num);
			}
			catch{
				return 0;
			}
		}

		///<summary>Do this first to get a list of all trans nums that are contained within this 997.  Then, for each trans num, we can later retrieve the AckCode for that single trans num.</summary>
		public List<int> GetTransNums(){
			List<int> retVal=new List<int>();
			X12Segment seg;
			int transNum=0;
			for(int i=0;i<FunctGroups[0].Transactions[0].Segments.Count;i++){
				seg=FunctGroups[0].Transactions[0].Segments[i];
				if(seg.SegmentID=="AK2"){
					transNum=0;
					try{
						transNum=PIn.PInt(seg.Get(2));
					}
					catch{
						transNum=0;
					}
					if(transNum!=0){
						retVal.Add(transNum);
					}
				}
			}
			return retVal;
		}

		///<summary>Use after GetTransNums.  Will return A=Accepted, R=Rejected, or "" if can't determine.</summary>
		public string GetAckForTrans(int transNum){
			X12Segment seg;
			bool foundTransNum=false;
			int thisTransNum=0;
			for(int i=0;i<FunctGroups[0].Transactions[0].Segments.Count;i++){
				seg=FunctGroups[0].Transactions[0].Segments[i];
				if(foundTransNum){
					if(seg.SegmentID!="AK5"){
						continue;
					}
					string code=seg.Get(1);
					if(code=="A" || code=="E") {//Accepted or accepted with Errors.
						return "A";
					}
					return "R";//rejected
				}
				if(seg.SegmentID=="AK2"){
					thisTransNum=0;
					try {
						thisTransNum=PIn.PInt(seg.Get(2));
					}
					catch {
						thisTransNum=0;
					}
					if(thisTransNum==transNum) {
						foundTransNum=true;
					}
				}
			}
			return "";
		}

		///<summary>Will return "" if unable to determine.  But would normally return A=Accepted or R=Rejected or P=Partially accepted if only some of the transactions were accepted.</summary>
		public string GetBatchAckCode(){
			if(this.FunctGroups[0].Transactions.Count!=1){
				return "";
			}
			X12Segment seg=FunctGroups[0].Transactions[0].GetSegmentByID("AK9");
			if(seg==null){
				return "";
			}
			string code=seg.Get(1);
			if(code=="A" || code=="E"){//Accepted or accepted with Errors.
				return "A";
			}
			if(code=="P") {//Partially accepted
				return "P";
			}
			return "R";//rejected
		}		

		/*Example 997
		ISA*00*          *00*          *ZZ*113504607      *ZZ*               *070813*0930*U*00401*705547511*0*P*:~
		GS*FA*113504607**20070813*0930*705547511*X*004010X097A1~
		ST*997*0001~
		AK1*HC*0001~
		AK2*837*0001~
		AK5*A~
		AK9*A*1*1*1~
		SE*6*0001~
		GE*1*705547511~
		IEA*1*705547511~
		*/
		//the only rows that we evaluate are AK2, which has transaction# (batchNumber), and AK5 which has ack code.


	}
}
