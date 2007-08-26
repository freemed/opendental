using System;
using OpenDentBusiness;

namespace OpenDental.Eclaims
{
	///<summary></summary>
	public class X997:X12object{
		
		public int GetBatchNumber(){
			if(this.functGroups[0].Transactions.Count!=1) {
				return 0;
			}
			X12Segment seg=functGroups[0].Transactions[0].GetSegmentByID("AK2");
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

		///<summary>Will return "" if unable to determine.  But would normally return A=Accepted or R=Rejected.</summary>
		public string GetAckCode(){
			if(this.functGroups[0].Transactions.Count!=1){
				return "";
			}
			X12Segment seg=functGroups[0].Transactions[0].GetSegmentByID("AK5");
			if(seg==null){
				return "";
			}
			string code=seg.Get(1);
			if(code=="A" || code=="E"){//Accepted or accepted with Errors.
				return "A";
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
