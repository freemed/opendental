using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class ProcedureLogic{

		///<summary>The supplied DataRows must include the following columns: ProcStatus(optional),Priority(optional),ToothRange,ToothNum,ProcCode.  This sorts procedures based on priority, then tooth number, then procCode.  It does not care about dates or status.  Currently used in TP module and Chart module sorting.</summary>
		public static int CompareProcedures(DataRow x,DataRow y) {
			//first, by status
			if(x.Table.Columns.Contains("ProcStatus") && y.Table.Columns.Contains("ProcStatus")) {
				if(x["ProcStatus"].ToString()!=y["ProcStatus"].ToString()) {
					//Cn,TP,R,EO,EC,C,D
					int xIdx=0;
					switch(x["ProcStatus"].ToString()) {
						case "7"://Cn
							xIdx=0;
							break;
						case "1"://TP
							xIdx=1;
							break;
						case "5"://R
							xIdx=2;
							break;
						case "4"://EO
							xIdx=3;
							break;
						case "3"://EC
							xIdx=4;
							break;
						case "2"://C
							xIdx=5;
							break;
						case "6"://D
							xIdx=6;
							break;
					}
					int yIdx=0;
					switch(y["ProcStatus"].ToString()) {
						case "7"://Cn
							yIdx=0;
							break;
						case "1"://TP
							yIdx=1;
							break;
						case "5"://R
							yIdx=2;
							break;
						case "4"://EO
							yIdx=3;
							break;
						case "3"://EC
							yIdx=4;
							break;
						case "2"://C
							yIdx=5;
							break;
						case "6"://D
							yIdx=6;
							break;
					}
					return xIdx.CompareTo(yIdx);
				}
			}
			//by priority
			if(x.Table.Columns.Contains("Priority") && y.Table.Columns.Contains("Priority")){
				if(x["Priority"].ToString()!=y["Priority"].ToString()) {//if priorities are different
					if(x["Priority"].ToString()=="0") {
						return 1;//x is greater than y. Priorities always come first.
					}
					if(y["Priority"].ToString()=="0") {
						return -1;//x is less than y. Priorities always come first.
					}
					return DefC.GetOrder(DefCat.TxPriorities,PIn.Long(x["Priority"].ToString())).CompareTo
						(DefC.GetOrder(DefCat.TxPriorities,PIn.Long(y["Priority"].ToString())));
				}
			}
			//priorities are the same, so sort by toothrange
			if(x["ToothRange"].ToString()!=y["ToothRange"].ToString()) {
				//empty toothranges come before filled toothrange values
				return x["ToothRange"].ToString().CompareTo(y["ToothRange"].ToString());
			}
			//toothranges are the same (usually empty), so compare toothnumbers
			if(x["ToothNum"].ToString()!=y["ToothNum"].ToString()) {
				//this also puts invalid or empty toothnumbers before the others.
				return Tooth.ToInt(x["ToothNum"].ToString()).CompareTo(Tooth.ToInt(y["ToothNum"].ToString()));
			}
			//priority and toothnums are the same, so sort by proccode.
			return x["ProcCode"].ToString().CompareTo(y["ProcCode"].ToString());
			//return 0;//priority, tooth number, and proccode are all the same
		}







	}


}
