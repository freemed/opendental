using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class PayPlans {
		///<summary>Gets a list of all payplans for a given patient, whether they are the guarantor or the patient.  This is also used in UpdateAll to store all payment plans in entire database.</summary>
		public static PayPlan[] Refresh(int guarantor,int patNum) {
			string command="SELECT * from payplan"
				+" WHERE PatNum = "+patNum.ToString()
				+" OR Guarantor = "+guarantor.ToString()+" ORDER BY payplandate";
			DataTable table=General.GetTable(command);
			PayPlan[] List=new PayPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new PayPlan();
				List[i].PayPlanNum    = PIn.PInt(table.Rows[i][0].ToString());
				List[i].PatNum        = PIn.PInt(table.Rows[i][1].ToString());
				List[i].Guarantor     = PIn.PInt(table.Rows[i][2].ToString());
				List[i].PayPlanDate   = PIn.PDate(table.Rows[i][3].ToString());
				List[i].APR           = PIn.PDouble(table.Rows[i][4].ToString());
				List[i].Note          = PIn.PString(table.Rows[i][5].ToString());
				List[i].PlanNum       = PIn.PInt(table.Rows[i][6].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void InsertOrUpdate(PayPlan plan, bool isNew){
			//if(){
			//	throw new Exception(Lan.g(this,""));
			//}
			if(isNew){
				Insert(plan);
			}
			else{
				Update(plan);
			}
		}

		///<summary></summary>
		private static void Update(PayPlan plan){
			string command="UPDATE payplan SET " 
				+"PatNum = '"         +POut.PInt   (plan.PatNum)+"'"
				+",Guarantor = '"     +POut.PInt   (plan.Guarantor)+"'"
				+",PayPlanDate = "   +POut.PDate  (plan.PayPlanDate)
				+",APR = '"           +POut.PDouble(plan.APR)+"'"
				+",Note = '"          +POut.PString(plan.Note)+"'"
				+",PlanNum = '"       +POut.PInt   (plan.PlanNum)+"'"
				+" WHERE PayPlanNum = '" +POut.PInt(plan.PayPlanNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(PayPlan plan){
			if(PrefB.RandomKeys){
				plan.PayPlanNum=MiscData.GetKey("payplan","PayPlanNum");
			}
			string command="INSERT INTO payplan (";
			if(PrefB.RandomKeys){
				command+="PayPlanNum,";
			}
			command+="PatNum,Guarantor,PayPlanDate,APR,Note,PlanNum) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(plan.PayPlanNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (plan.PatNum)+"', "
				+"'"+POut.PInt   (plan.Guarantor)+"', "
				+POut.PDate  (plan.PayPlanDate)+", "
				+"'"+POut.PDouble(plan.APR)+"', "
				+"'"+POut.PString(plan.Note)+"', "
				+"'"+POut.PInt   (plan.PlanNum)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				plan.PayPlanNum=General.NonQ(command,true);
			}
		}

		///<summary>Called from FormPayPlan.  Also deletes all attached payplancharges.  Throws exception if there are any paysplits attached.</summary>
		public static void Delete(PayPlan plan){
			string command="SELECT COUNT(*) FROM paysplit WHERE PayPlanNum="+plan.PayPlanNum.ToString();
			if(General.GetCount(command)!="0"){
				throw new ApplicationException
					(Lan.g("PayPlans","You cannot delete a payment plan with payments attached.  Unattach the payments first."));
			}
			command="DELETE FROM payplancharge WHERE PayPlanNum="+plan.PayPlanNum.ToString();
			General.NonQ(command);
			command="DELETE FROM payplan WHERE PayPlanNum ="+plan.PayPlanNum.ToString();
			General.NonQ(command);
		}

		/// <summary>Gets info directly from database. Used from PayPlan and Account windows to get the amount paid so far on one payment plan.</summary>
		public static double GetAmtPaid(int payPlanNum){
			string command="SELECT SUM(paysplit.SplitAmt) FROM paysplit "
				+"WHERE paysplit.PayPlanNum = '"+payPlanNum.ToString()+"' "
				+"GROUP BY paysplit.PayPlanNum";
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return 0;
			}
			return PIn.PDouble(table.Rows[0][0].ToString());
		}

		///<summary>Used from FormPayPlan, Account, and ComputeBal to get the accumulated amount due for a payment plan based on today's date.  Includes interest, but does not include payments made so far.  The chargelist must include all charges for this payplan, but it can include more as well.</summary>
		public static double GetAccumDue(int payPlanNum, PayPlanCharge[] chargeList){
			double retVal=0;
			for(int i=0;i<chargeList.Length;i++){
				if(chargeList[i].PayPlanNum!=payPlanNum){
					continue;
				}
				if(chargeList[i].ChargeDate>DateTime.Today){//not due yet
					continue;
				}
				retVal+=chargeList[i].Principal+chargeList[i].Interest;
			}
			return retVal;
		}

		/// <summary>Used from Account window to get the amount paid so far on one payment plan.  Must pass in the total amount paid and the returned value will not be more than this.  The chargelist must include all charges for this payplan, but it can include more as well.  It will loop sequentially through the charges to get just the principal portion.</summary>
		public static double GetPrincPaid(double amtPaid,int payPlanNum,PayPlanCharge[] chargeList){
			//amtPaid gets reduced to 0 throughout this loop.
			double retVal=0;
			for(int i=0;i<chargeList.Length;i++){
				if(chargeList[i].PayPlanNum!=payPlanNum){
					continue;
				}
				//For this charge, first apply payment to interest
				if(amtPaid>chargeList[i].Interest){
					amtPaid-=chargeList[i].Interest;
				}
				else{//interest will eat up the remainder of the payment
					amtPaid=0;
					break;
				}
				//Then, apply payment to principal
				if(amtPaid>chargeList[i].Principal){
					retVal+=chargeList[i].Principal;
					amtPaid-=chargeList[i].Principal;
				}
				else{//principal will eat up the remainder of the payment
					retVal+=amtPaid;
					amtPaid=0;
					break;
				}
			}
			return retVal;
		}

		/// <summary>Used from Account and ComputeBal to get the total amount of the original principal for one payment plan.  Does not include any interest.The chargelist must include all charges for this payplan, but it can include more as well.</summary>
		public static double GetTotalPrinc(int payPlanNum, PayPlanCharge[] chargeList){
			double retVal=0;
			for(int i=0;i<chargeList.Length;i++){
				if(chargeList[i].PayPlanNum!=payPlanNum){
					continue;
				}
				retVal+=chargeList[i].Principal;
			}
			return retVal;
		}

		///<summary>Returns the sum of all payment plan entries for guarantor and/or patient.</summary>
		public static double ComputeBal(int patNum,PayPlan[] list,PayPlanCharge[] chargeList){
			double retVal=0;
			for(int i=0;i<list.Length;i++){
				//one or both of these conditions may be met:
				if(list[i].Guarantor==patNum){
					retVal+=GetAccumDue(list[i].PayPlanNum,chargeList);
				}
				if(list[i].PatNum==patNum){
					retVal-=GetTotalPrinc(list[i].PayPlanNum,chargeList);
				}
			}
			return retVal;
		}

		///<summary>Refreshes the list for the specified guarantor, and then determines if there are any valid plans with that patient as the guarantor.  If more than one valid payment plan, displays list to select from.  If any valid plans, then it returns that plan, else returns null.</summary>
		public static PayPlan GetValidPlan(int guarNum,bool isIns){
			PayPlan[] PlanListAll=Refresh(guarNum,0);
			PayPlan[] PayPlanList=GetListOneType(PlanListAll,isIns);
			if(PayPlanList.Length==0){
				return null;
			}
			if(PayPlanList.Length==1){ //if there is only one valid payplan
				return PayPlanList[0].Copy();
			}
			PayPlanCharge[] ChargeList=PayPlanCharges.Refresh(guarNum);
			//enhancement needed to weed out payment plans that are all paid off
			//more than one valid PayPlan
			FormPayPlanSelect FormPPS=new FormPayPlanSelect(PayPlanList,ChargeList);
			FormPPS.ShowDialog();
			if(FormPPS.DialogResult==DialogResult.OK){
				return PayPlanList[FormPPS.IndexSelected].Copy();
			}
			else{
				return null;
			}
		}

		///<summary>Supply a list of all payment plans for a guarantor.  Based on the isIns setting, it will either return a list of all regular payment plans or only those for ins.  Used just before displaying FormPayPlanSelect.</summary>
		public static PayPlan[] GetListOneType(PayPlan[] payPlanList,bool isIns){
			ArrayList AL=new ArrayList();
			for(int i=0;i<payPlanList.Length;i++){
				if(isIns && payPlanList[i].PlanNum>0){
					AL.Add(payPlanList[i]);
				}
				else if(!isIns && payPlanList[i].PlanNum==0){
					AL.Add(payPlanList[i]);
				}
			}
			PayPlan[] retVal=new PayPlan[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}


	}

	

	


}










