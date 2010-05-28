using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClaimPayments {

		///<summary></summary>
		public static DataTable GetForClaim(long claimNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),claimNum);
			}
			DataTable table=new DataTable();
			DataRow row;
			table.Columns.Add("amount");
			table.Columns.Add("BankBranch");
			table.Columns.Add("ClaimPaymentNum");
			table.Columns.Add("checkDate");
			table.Columns.Add("CheckNum");
			table.Columns.Add("Note");
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT BankBranch,claimpayment.ClaimPaymentNum,CheckNum,CheckDate,"
				+"SUM(claimproc.InsPayAmt) amount,Note "
				+"FROM claimpayment,claimproc "
				+"WHERE claimpayment.ClaimPaymentNum = claimproc.ClaimPaymentNum "
				+"AND claimproc.ClaimNum = '"+POut.Long(claimNum)+"' "
				+"GROUP BY claimpayment.ClaimPaymentNum, BankBranch, CheckDate, CheckNum, Note";//required by Oracle
			DataTable rawT=Db.GetTable(command);
			DateTime date;
			for(int i=0;i<rawT.Rows.Count;i++) {
				row=table.NewRow();
				row["amount"]=PIn.Double(rawT.Rows[i]["amount"].ToString()).ToString("F");
				row["BankBranch"]=rawT.Rows[i]["BankBranch"].ToString();
				row["ClaimPaymentNum"]=rawT.Rows[i]["ClaimPaymentNum"].ToString();
				date=PIn.Date(rawT.Rows[i]["CheckDate"].ToString());
				row["checkDate"]=date.ToShortDateString();
				row["CheckNum"]=rawT.Rows[i]["CheckNum"].ToString();
				row["Note"]=rawT.Rows[i]["Note"].ToString();
				rows.Add(row);
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		///<summary>Gets all unattached claimpayments for display in a new deposit.  Excludes payments before dateStart.</summary>
		public static ClaimPayment[] GetForDeposit(DateTime dateStart,long clinicNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ClaimPayment[]>(MethodBase.GetCurrentMethod(),dateStart,clinicNum);
			}
			string command=
				"SELECT ClaimPaymentNum,CheckDate,CheckAmt,"
				+"Checknum,BankBranch,Note,"
				+"ClinicNum,DepositNum,CarrierName "
				+"FROM claimpayment "
				+"WHERE DepositNum = 0 "
				+"AND CheckDate >= "+POut.Date(dateStart);
			if(clinicNum!=0){
				command+=" AND ClinicNum="+POut.Long(clinicNum);
			}
			command+=" ORDER BY CheckDate";
			return Crud.ClaimPaymentCrud.SelectMany(command).ToArray();
		}

		///<summary>Gets all claimpayments for one specific deposit.</summary>
		public static ClaimPayment[] GetForDeposit(long depositNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ClaimPayment[]>(MethodBase.GetCurrentMethod(),depositNum);
			}
			string command=
				"SELECT ClaimPaymentNum,CheckDate,CheckAmt,"
				+"Checknum,BankBranch,Note,"
				+"ClinicNum,DepositNum,CarrierName "
				+"FROM claimpayment "
				+"WHERE DepositNum = "+POut.Long(depositNum)
				+" ORDER BY CheckDate";
			return Crud.ClaimPaymentCrud.SelectMany(command).ToArray();
		}

		///<summary>Gets one claimpayment directly from database.</summary>
		public static ClaimPayment GetOne(long claimPaymentNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ClaimPayment>(MethodBase.GetCurrentMethod(),claimPaymentNum);
			}
			string command=
				"SELECT * FROM claimpayment "
				+"WHERE ClaimPaymentNum = "+POut.Long(claimPaymentNum);
			return Crud.ClaimPaymentCrud.SelectOne(command);
		}

		///<summary></summary>
		public static long Insert(ClaimPayment cp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				cp.ClaimPaymentNum=Meth.GetLong(MethodBase.GetCurrentMethod(),cp);
				return cp.ClaimPaymentNum;
			}
			return Crud.ClaimPaymentCrud.Insert(cp);
		}

		///<summary>If trying to change the amount and attached to a deposit, it will throw an error, so surround with try catch.</summary>
		public static void Update(ClaimPayment cp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cp);
				return;
			}
			string command="SELECT DepositNum,CheckAmt FROM claimpayment "
				+"WHERE ClaimPaymentNum="+POut.Long(cp.ClaimPaymentNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			if(table.Rows[0][0].ToString()!="0"//if claimpayment is already attached to a deposit
				&& PIn.Double(table.Rows[0][1].ToString())!=cp.CheckAmt)//and checkAmt changes
			{
				throw new ApplicationException(Lans.g("ClaimPayments","Not allowed to change the amount on checks attached to deposits."));
			}
			Crud.ClaimPaymentCrud.Update(cp);
		}

		///<summary>Surround by try catch, because it will throw an exception if trying to delete a claimpayment attached to a deposit.</summary>
		public static void Delete(ClaimPayment cp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cp);
				return;
			}
			string command="SELECT DepositNum FROM claimpayment "
				+"WHERE ClaimPaymentNum="+POut.Long(cp.ClaimPaymentNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			if(table.Rows[0][0].ToString()!="0"){//if claimpayment is already attached to a deposit
				#if !DEBUG
				throw new ApplicationException(Lans.g("ClaimPayments","Not allowed to delete a payment attached to a deposit."));
				#endif
			}
			command= "UPDATE claimproc SET "
				+"ClaimPaymentNum=0 "
				+"WHERE claimpaymentNum="+POut.Long(cp.ClaimPaymentNum);
			//MessageBox.Show(string command);
			Db.NonQ(command);
			command= "DELETE FROM claimpayment "
				+"WHERE ClaimPaymentnum ="+POut.Long(cp.ClaimPaymentNum);
			//MessageBox.Show(string command);
 			Db.NonQ(command);
		}


	
		
	}

	

	


}









