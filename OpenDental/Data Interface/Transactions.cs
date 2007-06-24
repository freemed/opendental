using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Transactions {
		///<summary>Since transactions are always viewed individually, this function returns one transaction</summary>
		public static Transaction GetTrans(int transactionNum) {
			string command=
				"SELECT * FROM transaction "
				+"WHERE TransactionNum="+POut.PInt(transactionNum);
			return RefreshAndFill(command);
		}

		///<summary>For now, all transactions are retrieved singly.  Returns null if no match found.</summary>
		private static Transaction RefreshAndFill(string command) {
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0) {
				return null;
			}
			Transaction trans=new Transaction();
			trans=new Transaction();
			trans.TransactionNum= PIn.PInt(table.Rows[0][0].ToString());
			trans.DateTimeEntry = PIn.PDateT(table.Rows[0][1].ToString());
			trans.UserNum       = PIn.PInt(table.Rows[0][2].ToString());
			trans.DepositNum    = PIn.PInt(table.Rows[0][3].ToString());
			trans.PayNum        = PIn.PInt(table.Rows[0][4].ToString());
			return trans;
		}

		///<summary>Gets one transaction directly from the database which has this deposit attached to it.  If none exist, then returns null.</summary>
		public static Transaction GetAttachedToDeposit(int depositNum) {
			string command=
				"SELECT * FROM transaction "
				+"WHERE DepositNum="+POut.PInt(depositNum);
			return RefreshAndFill(command);
		}

		///<summary>Gets one transaction directly from the database which has this payment attached to it.  If none exist, then returns null.  There should never be more than one, so that's why it doesn't return more than one.</summary>
		public static Transaction GetAttachedToPayment(int payNum) {
			string command=
				"SELECT * FROM transaction "
				+"WHERE PayNum="+POut.PInt(payNum);
			return RefreshAndFill(command);
		}

		///<summary></summary>
		public static void Insert(Transaction trans) {
			if(PrefB.RandomKeys) {
				trans.TransactionNum=MiscData.GetKey("transaction","TransactionNum");
			}
			string command="INSERT INTO transaction (";
			if(PrefB.RandomKeys) {
				command+="TransactionNum,";
			}
			command+="DateTimeEntry,UserNum,DepositNum,PayNum) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(trans.TransactionNum)+"', ";
			}
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				command+=POut.PDateT(MiscData.GetNowDateTime());
			}
			else {//Assume MySQL
				command+="NOW()";
			}
			command+=
				 ", "//DateTimeEntry set to current server time
				+"'"+POut.PInt   (trans.UserNum)+"', "
				+"'"+POut.PInt   (trans.DepositNum)+"', "
				+"'"+POut.PInt   (trans.PayNum)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				trans.TransactionNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(Transaction trans) {
			string command= "UPDATE transaction SET "
				+"DateTimeEntry = " +POut.PDateT (trans.DateTimeEntry)+" "
				+",UserNum = '"      +POut.PInt   (trans.UserNum)+"' "
				+",DepositNum = '"   +POut.PInt   (trans.DepositNum)+"' "
				+",PayNum = '"       +POut.PInt   (trans.PayNum)+"' "
				+"WHERE TransactionNum = '"+POut.PInt(trans.TransactionNum)+"'";
			General.NonQ(command);
		}

		///<summary>Also deletes all journal entries for the transaction.  Will later throw an error if journal entries attached to any reconciles.  Be sure to surround with try-catch.</summary>
		public static void Delete(Transaction trans) {
			string command="DELETE FROM journalentry WHERE TransactionNum="+POut.PInt(trans.TransactionNum);
			General.NonQ(command);
			command= "DELETE FROM transaction WHERE TransactionNum = "+POut.PInt(trans.TransactionNum);
			General.NonQ(command);
		}

	
	
		///<summary></summary>
		public static bool IsReconciled(Transaction trans){
			string command="SELECT COUNT(*) FROM journalentry WHERE ReconcileNum !=0"
				+" AND TransactionNum="+POut.PInt(trans.TransactionNum);
			if(General.GetCount(command)=="0") {
				return false;
			}
			return true;
		}



	}

	
}




