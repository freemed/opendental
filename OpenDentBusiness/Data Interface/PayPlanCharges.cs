using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PayPlanCharges {
		///<summary>Gets all PayPlanCharges for a guarantor or patient, ordered by date.</summary>
		public static List<PayPlanCharge> Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PayPlanCharge>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * FROM payplancharge "
				+"WHERE Guarantor='"+POut.Long(patNum)+"' "
				+"OR PatNum='"+POut.Long(patNum)+"' "
				+"ORDER BY ChargeDate";
			return RefreshAndFill(Db.GetTable(command));
		}

		///<summary></summary>
		public static List<PayPlanCharge> GetForPayPlan(long payPlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PayPlanCharge>>(MethodBase.GetCurrentMethod(),payPlanNum);
			}
			string command=
				"SELECT * FROM payplancharge "
				+"WHERE PayPlanNum="+POut.Long(payPlanNum)
				+" ORDER BY ChargeDate";
			return RefreshAndFill(Db.GetTable(command));
		}

		///<summary></summary>
		public static PayPlanCharge GetOne(long payPlanChargeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PayPlanCharge>(MethodBase.GetCurrentMethod(),payPlanChargeNum);
			}
			string command=
				"SELECT * FROM payplancharge "
				+"WHERE PayPlanChargeNum="+POut.Long(payPlanChargeNum);
			return RefreshAndFill(Db.GetTable(command))[0];
		}

		private static List<PayPlanCharge> RefreshAndFill(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<PayPlanCharge> retVal=new List<PayPlanCharge>();
			PayPlanCharge ppcharge;
			for(int i=0;i<table.Rows.Count;i++) {
				ppcharge=new PayPlanCharge();
				ppcharge.PayPlanChargeNum= PIn.Long(table.Rows[i][0].ToString());
				ppcharge.PayPlanNum      = PIn.Long(table.Rows[i][1].ToString());
				ppcharge.Guarantor       = PIn.Long(table.Rows[i][2].ToString());
				ppcharge.PatNum          = PIn.Long(table.Rows[i][3].ToString());
				ppcharge.ChargeDate      = PIn.Date(table.Rows[i][4].ToString());
				ppcharge.Principal       = PIn.Double(table.Rows[i][5].ToString());
				ppcharge.Interest        = PIn.Double(table.Rows[i][6].ToString());
				ppcharge.Note            = PIn.String(table.Rows[i][7].ToString());
				ppcharge.ProvNum         = PIn.Long(table.Rows[i][8].ToString());
				ppcharge.ClinicNum       = PIn.Long(table.Rows[i][9].ToString());
				retVal.Add(ppcharge);
			}
			return retVal;
		}

		///<summary></summary>
		public static void Update(PayPlanCharge charge){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),charge);
				return;
			}
			Crud.PayPlanChargeCrud.Update(charge);
		}

		///<summary></summary>
		public static long Insert(PayPlanCharge charge) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				charge.PayPlanChargeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),charge);
				return charge.PayPlanChargeNum;
			}
			return Crud.PayPlanChargeCrud.Insert(charge);
		}

		///<summary></summary>
		public static void Delete(PayPlanCharge charge){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),charge);
				return;
			}
			string command= "DELETE from payplancharge WHERE PayPlanChargeNum = '"
				+POut.Long(charge.PayPlanChargeNum)+"'";
 			Db.NonQ(command);
		}	

		///<summary></summary>
		public static void DeleteAllInPlan(long payPlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),payPlanNum);
				return;
			}
			string command="DELETE FROM payplancharge WHERE PayPlanNum="+payPlanNum.ToString();
			Db.NonQ(command);
		}

		///<summary>When closing the payplan window, this sets all the charges to the appropriate provider and clinic.  This is the only way to set those fields.</summary>
		public static void SetProvAndClinic(long payPlanNum,long provNum,long clinicNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),payPlanNum,provNum,clinicNum);
				return;
			}
			string command="UPDATE payplancharge SET ProvNum="+POut.Long(provNum)+", "
				+"ClinicNum="+POut.Long(clinicNum)+" "
				+"WHERE PayPlanNum="+POut.Long(payPlanNum);
			Db.NonQ(command);
		}
	
	}
}