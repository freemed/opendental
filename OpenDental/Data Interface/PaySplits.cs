using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class PaySplits {
		///<summary>Returns all paySplits for the given patNum, organized by procDate.  WARNING! Also includes related paysplits that aren't actually attached to patient.  Includes any split where payment is for this patient.</summary>
		public static PaySplit[] Refresh(int patNum) {
			string command=
				"SELECT DISTINCT paysplit.* FROM paysplit,payment "
				+"WHERE paysplit.PayNum=payment.PayNum "
				+"AND (paysplit.PatNum = '"+POut.PInt(patNum)+"' OR payment.PatNum = '"+POut.PInt(patNum)+"') "
				+"ORDER BY ProcDate";
			return RefreshAndFill(command).ToArray();
		}

		private static List<PaySplit> RefreshAndFill(string command) {
			DataTable table=General.GetTable(command);
			List<PaySplit> retVal=new List<PaySplit>();
			PaySplit split;
			for(int i=0;i<table.Rows.Count;i++) {
				split=new PaySplit();
				split.SplitNum    = PIn.PInt(table.Rows[i][0].ToString());
				split.SplitAmt    = PIn.PDouble(table.Rows[i][1].ToString());
				split.PatNum      = PIn.PInt(table.Rows[i][2].ToString());
				split.ProcDate    = PIn.PDate(table.Rows[i][3].ToString());
				split.PayNum      = PIn.PInt(table.Rows[i][4].ToString());
				//List[i].IsDiscount  = PIn.PBool  (table.Rows[i][5].ToString());
				//List[i].DiscountType= PIn.PInt   (table.Rows[i][6].ToString());
				split.ProvNum     = PIn.PInt(table.Rows[i][7].ToString());
				split.PayPlanNum  = PIn.PInt(table.Rows[i][8].ToString());
				split.DatePay     = PIn.PDate(table.Rows[i][9].ToString());
				split.ProcNum     = PIn.PInt(table.Rows[i][10].ToString());
				split.DateEntry   = PIn.PDate(table.Rows[i][11].ToString());
				retVal.Add(split);
			}
			return retVal;
		}

		///<summary>Used from payment window to get all paysplits for the payment.</summary>
		public static List<PaySplit> GetForPayment(int payNum) {
			string command=
				"SELECT * FROM paysplit "
				+"WHERE PayNum="+POut.PInt(payNum);
			return RefreshAndFill(command);
		}

		///<summary></summary>
		public static void Update(PaySplit split){
			string command="UPDATE paysplit SET " 
				+ "SplitAmt = '"     +POut.PDouble(split.SplitAmt)+"'"
				+ ",PatNum = '"      +POut.PInt   (split.PatNum)+"'"
				+ ",ProcDate = "    +POut.PDate  (split.ProcDate)
				+ ",PayNum = '"      +POut.PInt   (split.PayNum)+"'"
				+ ",ProvNum = '"     +POut.PInt   (split.ProvNum)+"'"
				+ ",PayPlanNum = '"  +POut.PInt   (split.PayPlanNum)+"'"
				+ ",DatePay = "     +POut.PDate  (split.DatePay)
				+ ",ProcNum = '"     +POut.PInt   (split.ProcNum)+"'"
				//+ ",DateEntry = '"   +POut.PDate  (DateEntry)+"'"//not allowed to change
				+" WHERE splitNum = '" +POut.PInt (split.SplitNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(PaySplit split){
			if(PrefB.RandomKeys){
				split.SplitNum=MiscData.GetKey("paysplit","SplitNum");
			}
			string command= "INSERT INTO paysplit (";
			if(PrefB.RandomKeys){
				command+="SplitNum,";
			}
			command+="SplitAmt,PatNum,ProcDate, "
				+"PayNum,IsDiscount,DiscountType,ProvNum,PayPlanNum,DatePay,ProcNum,DateEntry) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(split.SplitNum)+"', ";
			}
			command+=
				 "'"+POut.PDouble(split.SplitAmt)+"', "
				+"'"+POut.PInt   (split.PatNum)+"', "
				+POut.PDate  (split.ProcDate)+", "
				+"'"+POut.PInt   (split.PayNum)+"', "
				+"'0', "//IsDiscount
				+"'0', "//DiscountType
				+"'"+POut.PInt   (split.ProvNum)+"', "
				+"'"+POut.PInt   (split.PayPlanNum)+"', "
				+POut.PDate  (split.DatePay)+", "
				+"'"+POut.PInt   (split.ProcNum)+"', ";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				command+=POut.PDateT(MiscData.GetNowDateTime());
			}else{//Assume MySQL
				command+="NOW()";
			}
			command+=")";//DateEntry: date of server
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				split.SplitNum=General.NonQ(command,true);
			}
		}

		///<summary>Deletes the paysplit.</summary>
		public static void Delete(PaySplit split){
			string command= "DELETE from paysplit WHERE splitNum = "+POut.PInt(split.SplitNum);
 			General.NonQ(command);
		}

		///<summary>Returns all paySplits for the given procNum. Must supply a list of all paysplits for the patient.</summary>
		public static ArrayList GetForProc(int procNum,PaySplit[] List){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					retVal.Add(List[i]);
				}
			}
			return retVal;
		}

		///<summary>Used from ContrAccount and ProcEdit to display and calculate payments attached to procs. Used once in FormProcEdit</summary>
		public static double GetTotForProc(int procNum,PaySplit[] List){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					retVal+=List[i].SplitAmt;
				}
			}
			return retVal;
		}

		///<summary>Used from FormPaySplitEdit.  Returns total payments for a procedure for all paysplits other than the supplied excluded paysplit.</summary>
		public static double GetTotForProc(int procNum,PaySplit[] List,int excludeSplitNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].SplitNum==excludeSplitNum){
					continue;
				}
				if(List[i].ProcNum==procNum){
					retVal+=List[i].SplitAmt;
				}
			}
			return retVal;
		}

		///<summary>Used once in ContrAccount.  WARNING!  The returned list of 'paysplits' are not real paysplits.  They are actually grouped by patient and date.  Only the ProcDate, SplitAmt, PatNum, and ProcNum(one of many) are filled. Must supply a list which would include all paysplits for this payment.</summary>
		public static ArrayList GetGroupedForPayment(int payNum,PaySplit[] List){
			ArrayList retVal=new ArrayList();
			int matchI;
			for(int i=0;i<List.Length;i++){
				if(List[i].PayNum==payNum){
					//find a 'paysplit' with matching procdate and patnum
					matchI=-1;
					for(int j=0;j<retVal.Count;j++){
						if(((PaySplit)retVal[j]).ProcDate==List[i].ProcDate && ((PaySplit)retVal[j]).PatNum==List[i].PatNum){
							matchI=j;
							break;
						}
					}
					if(matchI==-1){
						retVal.Add(new PaySplit());
						matchI=retVal.Count-1;
						((PaySplit)retVal[matchI]).ProcDate=List[i].ProcDate;
						((PaySplit)retVal[matchI]).PatNum=List[i].PatNum;
					}
					if(((PaySplit)retVal[matchI]).ProcNum==0 && List[i].ProcNum!=0){
						((PaySplit)retVal[matchI]).ProcNum=List[i].ProcNum;
					}
					((PaySplit)retVal[matchI]).SplitAmt+=List[i].SplitAmt;
				}
			}
			return retVal;
		}

		///<summary>Only those amounts that have the same paynum, procDate, and patNum as the payment, and are not attached to procedures.</summary>
		public static double GetAmountForPayment(int payNum,DateTime payDate,int patNum, PaySplit[] paySplitList){
			double retVal=0;
			for(int i=0;i<paySplitList.Length;i++){
				if(paySplitList[i].PayNum!=payNum) {
					continue;
				}
				if(paySplitList[i].PatNum!=patNum){
					continue;
				}
				if(paySplitList[i].ProcDate!=payDate){
					continue;
				}
				if(paySplitList[i].ProcNum!=0){
					continue;
				}
				retVal+=paySplitList[i].SplitAmt;
			}
			return retVal;
		}

		///<summary>Used once in ContrAccount to just get the splits for a single patient.  The supplied list also contains splits that are not necessarily for this one patient.</summary>
		public static PaySplit[] GetForPatient(int patNum,PaySplit[] List){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].PatNum==patNum){
					retVal.Add(List[i]);
				}
			}
			PaySplit[] retList=new PaySplit[retVal.Count];
			retVal.CopyTo(retList);
			return retList;
		}

		///<summary>Used once in ContrAccount.  Usually returns 0 unless there is a payplan for this payment and patient.</summary>
		public static int GetPayPlanNum(int payNum,int patNum,PaySplit[] List){
			for(int i=0;i<List.Length;i++){
				if(List[i].PayNum==payNum && List[i].PatNum==patNum && List[i].PayPlanNum!=0){
					return List[i].PayPlanNum;
				}
			}
			return 0;
		}

		///<summary>Used in ComputeBalances to compute balance for a single patient. Supply a list of all paysplits for the patient.</summary>
		public static double ComputeBal(PaySplit[] list){//
			double retVal=0;
			for(int i=0;i<list.Length;i++){
				retVal+=list[i].SplitAmt;
			}
			return retVal;
		}

		///<summary>Used in FormPayment to sych database with changes user made to the paySplit list for a payment.  Must supply an old list for comparison.  Only the differences are saved.</summary>
		public static void UpdateList(List<PaySplit> oldSplitList,List<PaySplit> newSplitList) {
			PaySplit newPaySplit;
			for(int i=0;i<oldSplitList.Count;i++) {//loop through the old list
				newPaySplit=null;
				for(int j=0;j<newSplitList.Count;j++) {
					if(newSplitList[j]==null || newSplitList[j].SplitNum==0) {
						continue;
					}
					if(((PaySplit)oldSplitList[i]).SplitNum==((PaySplit)newSplitList[j]).SplitNum) {
						newPaySplit=newSplitList[j];
						break;
					}
				}
				if(newPaySplit==null) {
					//PaySplit with matching SplitNum was not found, so it must have been deleted
					PaySplits.Delete(oldSplitList[i]);
					continue;
				}
				//PaySplit was found with matching SplitNum, so check for changes
				if(newPaySplit.DateEntry != oldSplitList[i].DateEntry
					|| newPaySplit.DatePay != oldSplitList[i].DatePay
					|| newPaySplit.PatNum != oldSplitList[i].PatNum
					|| newPaySplit.PayNum != oldSplitList[i].PayNum
					|| newPaySplit.PayPlanNum != oldSplitList[i].PayPlanNum
					|| newPaySplit.ProcDate != oldSplitList[i].ProcDate
					|| newPaySplit.ProcNum != oldSplitList[i].ProcNum
					|| newPaySplit.ProvNum != oldSplitList[i].ProvNum
					|| newPaySplit.SplitAmt != oldSplitList[i].SplitAmt) 
				{
					PaySplits.Update(newPaySplit);
				}
			}
			for(int i=0;i<newSplitList.Count;i++) {//loop through the new list
				if(newSplitList[i]==null) {
					continue;
				}
				if(newSplitList[i].SplitNum!=0) {
					continue;
				}
				//entry with SplitNum=0, so it's new
				PaySplits.Insert(newSplitList[i]);
			}
		}

		

	}

	

	


}










