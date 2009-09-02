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
				+"WHERE Guarantor='"+POut.PInt(patNum)+"' "
				+"OR PatNum='"+POut.PInt(patNum)+"' "
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
				+"WHERE PayPlanNum="+POut.PInt(payPlanNum)
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
				+"WHERE PayPlanChargeNum="+POut.PInt(payPlanChargeNum);
			return RefreshAndFill(Db.GetTable(command))[0];
		}

		private static List<PayPlanCharge> RefreshAndFill(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<PayPlanCharge> retVal=new List<PayPlanCharge>();
			PayPlanCharge ppcharge;
			for(int i=0;i<table.Rows.Count;i++) {
				ppcharge=new PayPlanCharge();
				ppcharge.PayPlanChargeNum= PIn.PInt(table.Rows[i][0].ToString());
				ppcharge.PayPlanNum      = PIn.PInt(table.Rows[i][1].ToString());
				ppcharge.Guarantor       = PIn.PInt(table.Rows[i][2].ToString());
				ppcharge.PatNum          = PIn.PInt(table.Rows[i][3].ToString());
				ppcharge.ChargeDate      = PIn.PDate(table.Rows[i][4].ToString());
				ppcharge.Principal       = PIn.PDouble(table.Rows[i][5].ToString());
				ppcharge.Interest        = PIn.PDouble(table.Rows[i][6].ToString());
				ppcharge.Note            = PIn.PString(table.Rows[i][7].ToString());
				ppcharge.ProvNum         = PIn.PInt(table.Rows[i][8].ToString());
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
			string command= "UPDATE payplancharge SET " 
				+"PayPlanChargeNum = '"+POut.PInt   (charge.PayPlanChargeNum)+"'"
				+",PayPlanNum = '"     +POut.PInt   (charge.PayPlanNum)+"'"
				+",Guarantor = '"      +POut.PInt   (charge.Guarantor)+"'"
				+",PatNum = '"         +POut.PInt   (charge.PatNum)+"'"
				+",ChargeDate = "     +POut.PDate  (charge.ChargeDate)
				+",Principal = '"      +POut.PDouble(charge.Principal)+"'"
				+",Interest = '"       +POut.PDouble(charge.Interest)+"'"
				+",Note = '"           +POut.PString(charge.Note)+"'"
				+",ProvNum = '"        +POut.PInt   (charge.ProvNum)+"'"
				+" WHERE PayPlanChargeNum = '"+POut.PInt(charge.PayPlanChargeNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(PayPlanCharge charge) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				charge.PayPlanChargeNum=Meth.GetInt(MethodBase.GetCurrentMethod(),charge);
				return charge.PayPlanChargeNum;
			}
			if(PrefC.RandomKeys){
				charge.PayPlanChargeNum=MiscData.GetKey("payplancharge","PayPlanChargeNum");
			}
			string command= "INSERT INTO payplancharge (";
			if(PrefC.RandomKeys){
				command+="PayPlanChargeNum,";
			}
			command+="PayPlanNum,Guarantor,PatNum,ChargeDate,Principal,Interest,Note,ProvNum) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(charge.PayPlanChargeNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (charge.PayPlanNum)+"', "
				+"'"+POut.PInt   (charge.Guarantor)+"', "
				+"'"+POut.PInt   (charge.PatNum)+"', "
				+POut.PDate  (charge.ChargeDate)+", "
				+"'"+POut.PDouble(charge.Principal)+"', "
				+"'"+POut.PDouble(charge.Interest)+"', "
				+"'"+POut.PString(charge.Note)+"', "
				+"'"+POut.PInt   (charge.ProvNum)+"')";
 			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				charge.PayPlanChargeNum=Db.NonQ(command,true);
			}
			return charge.PayPlanChargeNum;
		}

		///<summary></summary>
		public static void Delete(PayPlanCharge charge){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),charge);
				return;
			}
			string command= "DELETE from payplancharge WHERE PayPlanChargeNum = '"
				+POut.PInt(charge.PayPlanChargeNum)+"'";
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
	
	}
}