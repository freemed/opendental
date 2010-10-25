using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PayPlans {
		///<summary>Gets a list of all payplans for a given patient, whether they are the patient or the guarantor.  This is only used in one place, when deleting a patient to check dependencies.</summary>
		public static int GetDependencyCount(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT COUNT(*) FROM payplan"
				+" WHERE PatNum = "+POut.Long(patNum)
				+" OR Guarantor = "+POut.Long(patNum);
			return PIn.Int(Db.GetScalar(command));
		}

		public static PayPlan GetOne(long payPlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PayPlan>(MethodBase.GetCurrentMethod(),payPlanNum);
			}
			return Crud.PayPlanCrud.SelectOne(payPlanNum);
		}

		///<summary>Determines if there are any valid plans with that patient as the guarantor.</summary>
		public static List<PayPlan> GetValidPlansNoIns(long guarNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PayPlan>>(MethodBase.GetCurrentMethod(),guarNum);
			}
			string command="SELECT * FROM payplan"
				+" WHERE Guarantor = "+POut.Long(guarNum)
				+" AND PlanNum = 0"
				+" ORDER BY payplandate";
			return Crud.PayPlanCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(PayPlan payPlan){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				payPlan.PayPlanNum=Meth.GetLong(MethodBase.GetCurrentMethod(),payPlan);
				return payPlan.PayPlanNum;
			}
			return Crud.PayPlanCrud.Insert(payPlan);
		}

		///<summary></summary>
		public static void Update(PayPlan payPlan) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),payPlan);
				return;
			}
			Crud.PayPlanCrud.Update(payPlan);
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
			return PIn.Double(table.Rows[0][0].ToString());
		}

		/// <summary>Gets info directly from database. Used when adding a payment.</summary>
		public static bool PlanIsPaidOff(long payPlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),payPlanNum);
			}
			string command="SELECT SUM(paysplit.SplitAmt) FROM paysplit "
				+"WHERE PayPlanNum = "+POut.Long(payPlanNum);// +"' "
				//+" GROUP BY paysplit.PayPlanNum";
			double amtPaid=PIn.Double(Db.GetScalar(command));
			command="SELECT SUM(Principal+Interest) FROM payplancharge "
				+"WHERE PayPlanNum = "+POut.Long(payPlanNum);
			double totalCost=PIn.Double(Db.GetScalar(command));
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










