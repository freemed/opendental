using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PayPlans {
		///<summary>Gets a list of all payplans for a given patient, whether they are the guarantor or the patient.  This is also used in UpdateAll to store all payment plans in entire database.</summary>
		public static PayPlan[] Refresh(long guarantor,long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PayPlan[]>(MethodBase.GetCurrentMethod(),guarantor,patNum);
			}
			string command="SELECT * FROM payplan"
				+" WHERE PatNum = "+patNum.ToString()
				+" OR Guarantor = "+guarantor.ToString()+" ORDER BY payplandate";
			return RefreshAndFill(Db.GetTable(command)).ToArray();
		}

		public static PayPlan GetOne(long payPlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PayPlan>(MethodBase.GetCurrentMethod(),payPlanNum);
			}
			string command="SELECT * FROM payplan"
				+" WHERE PayPlanNum = "+POut.PInt(payPlanNum);
			return RefreshAndFill(Db.GetTable(command))[0];
		}

		///<summary>Refreshes the list for the specified guarantor, and then determines if there are any valid plans with that patient as the guarantor.  If more than one valid payment plan, displays list to select from.  If any valid plans, then it returns that plan, else returns null.</summary>
		public static List<PayPlan> GetValidPlansNoIns(long guarNum) {//,bool isIns){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PayPlan>>(MethodBase.GetCurrentMethod(),guarNum);
			}
			string command="SELECT * FROM payplan"
				+" WHERE Guarantor = "+POut.PInt(guarNum);
			//if(isIns){
			//	command+=" AND PlanNum != 0";
			//}
			//else{
				command+=" AND PlanNum = 0";
			//}
			command+=" ORDER BY payplandate";
			return RefreshAndFill(Db.GetTable(command));
		}

		private static List<PayPlan> RefreshAndFill(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<PayPlan> retVal=new List<PayPlan>();
			//PayPlan[] List=new PayPlan[table.Rows.Count];
			PayPlan payplan;
			for(int i=0;i<table.Rows.Count;i++) {
				payplan=new PayPlan();
				payplan.PayPlanNum    = PIn.PInt   (table.Rows[i][0].ToString());
				payplan.PatNum        = PIn.PInt   (table.Rows[i][1].ToString());
				payplan.Guarantor     = PIn.PInt   (table.Rows[i][2].ToString());
				payplan.PayPlanDate   = PIn.PDate  (table.Rows[i][3].ToString());
				payplan.APR           = PIn.PDouble(table.Rows[i][4].ToString());
				payplan.Note          = PIn.PString(table.Rows[i][5].ToString());
				payplan.PlanNum       = PIn.PInt   (table.Rows[i][6].ToString());
				payplan.CompletedAmt  = PIn.PDouble(table.Rows[i][7].ToString());
				retVal.Add(payplan);
			}
			return retVal;
		}

		///<summary></summary>
		public static void Update(PayPlan plan){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),plan);
				return;
			}
			string command="UPDATE payplan SET " 
				+"PatNum = '"         +POut.PInt   (plan.PatNum)+"'"
				+",Guarantor = '"     +POut.PInt   (plan.Guarantor)+"'"
				+",PayPlanDate = "    +POut.PDate  (plan.PayPlanDate)
				+",APR = '"           +POut.PDouble(plan.APR)+"'"
				+",Note = '"          +POut.PString(plan.Note)+"'"
				+",PlanNum = '"       +POut.PInt   (plan.PlanNum)+"'"
				+",CompletedAmt = '"  +POut.PDouble(plan.CompletedAmt)+"'"
				+" WHERE PayPlanNum = '" +POut.PInt(plan.PayPlanNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(PayPlan plan) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				plan.PayPlanNum=Meth.GetInt(MethodBase.GetCurrentMethod(),plan);
				return plan.PayPlanNum;
			}
			if(PrefC.RandomKeys){
				plan.PayPlanNum=ReplicationServers.GetKey("payplan","PayPlanNum");
			}
			string command="INSERT INTO payplan (";
			if(PrefC.RandomKeys){
				command+="PayPlanNum,";
			}
			command+="PatNum,Guarantor,PayPlanDate,APR,Note,PlanNum,CompletedAmt) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(plan.PayPlanNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (plan.PatNum)+"', "
				+"'"+POut.PInt   (plan.Guarantor)+"', "
				+POut.PDate  (plan.PayPlanDate)+", "
				+"'"+POut.PDouble(plan.APR)+"', "
				+"'"+POut.PString(plan.Note)+"', "
				+"'"+POut.PInt   (plan.PlanNum)+"', "
				+"'"+POut.PDouble(plan.CompletedAmt)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				plan.PayPlanNum=Db.NonQ(command,true);
			}
			return plan.PayPlanNum;
		}

		///<summary>Called from FormPayPlan.  Also deletes all attached payplancharges.  Throws exception if there are any paysplits attached.</summary>
		public static void Delete(PayPlan plan){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),plan);
				return;
			}
			string command="SELECT COUNT(*) FROM paysplit WHERE PayPlanNum="+plan.PayPlanNum.ToString();
			if(Db.GetCount(command)!="0"){
				throw new ApplicationException
					(Lans.g("PayPlans","You cannot delete a payment plan with payments attached.  Unattach the payments first."));
			}
			command="DELETE FROM payplancharge WHERE PayPlanNum="+plan.PayPlanNum.ToString();
			Db.NonQ(command);
			command="DELETE FROM payplan WHERE PayPlanNum ="+plan.PayPlanNum.ToString();
			Db.NonQ(command);
		}

		/// <summary>Gets info directly from database. Used from PayPlan and Account windows to get the amount paid so far on one payment plan.</summary>
		public static double GetAmtPaid(long payPlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<double>(MethodBase.GetCurrentMethod(),payPlanNum);
			}
			string command="SELECT SUM(paysplit.SplitAmt) FROM paysplit "
				+"WHERE paysplit.PayPlanNum = '"+payPlanNum.ToString()+"' "
				+"GROUP BY paysplit.PayPlanNum";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return 0;
			}
			return PIn.PDouble(table.Rows[0][0].ToString());
		}

		/// <summary>Gets info directly from database. Used when adding a payment.</summary>
		public static bool PlanIsPaidOff(long payPlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),payPlanNum);
			}
			string command="SELECT SUM(paysplit.SplitAmt) FROM paysplit "
				+"WHERE PayPlanNum = "+POut.PInt(payPlanNum);// +"' "
				//+" GROUP BY paysplit.PayPlanNum";
			double amtPaid=PIn.PDouble(Db.GetScalar(command));
			command="SELECT SUM(Principal+Interest) FROM payplancharge "
				+"WHERE PayPlanNum = "+POut.PInt(payPlanNum);
			double totalCost=PIn.PDouble(Db.GetScalar(command));
			if(totalCost-amtPaid < .01) {
				return true;
			}
			return false;
		}

		///<summary>Used from FormPayPlan, Account, and ComputeBal to get the accumulated amount due for a payment plan based on today's date.  Includes interest, but does not include payments made so far.  The chargelist must include all charges for this payplan, but it can include more as well.</summary>
		public static double GetAccumDue(long payPlanNum, List<PayPlanCharge> chargeList){
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<chargeList.Count;i++){
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
		public static double GetPrincPaid(double amtPaid,long payPlanNum,List<PayPlanCharge> chargeList) {
			//No need to check RemotingRole; no call to db.
			//amtPaid gets reduced to 0 throughout this loop.
			double retVal=0;
			for(int i=0;i<chargeList.Count;i++){
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
		public static double GetTotalPrinc(long payPlanNum,List<PayPlanCharge> chargeList) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<chargeList.Count;i++){
				if(chargeList[i].PayPlanNum!=payPlanNum){
					continue;
				}
				retVal+=chargeList[i].Principal;
			}
			return retVal;
		}

		///<summary>Returns the sum of all payment plan entries for guarantor and/or patient.</summary>
		public static double ComputeBal(long patNum,PayPlan[] list,List<PayPlanCharge> chargeList) {
			//No need to check RemotingRole; no call to db.
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

		


	}

	

	


}










