using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Transactions {
		///<summary>Since transactions are always viewed individually, this function returns one transaction</summary>
		public static Transaction GetTrans(long transactionNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Transaction>(MethodBase.GetCurrentMethod(),transactionNum);
			}
			return Crud.TransactionCrud.SelectOne(transactionNum);
		}

		///<summary>Gets one transaction directly from the database which has this deposit attached to it.  If none exist, then returns null.</summary>
		public static Transaction GetAttachedToDeposit(long depositNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Transaction>(MethodBase.GetCurrentMethod(),depositNum);
			}
			string command=
				"SELECT * FROM transaction "
				+"WHERE DepositNum="+POut.Long(depositNum);
			return Crud.TransactionCrud.SelectOne(command);
		}

		///<summary>Gets one transaction directly from the database which has this payment attached to it.  If none exist, then returns null.  There should never be more than one, so that's why it doesn't return more than one.</summary>
		public static Transaction GetAttachedToPayment(long payNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Transaction>(MethodBase.GetCurrentMethod(),payNum);
			}
			string command=
				"SELECT * FROM transaction "
				+"WHERE PayNum="+POut.Long(payNum);
			return Crud.TransactionCrud.SelectOne(command);
		}

		///<summary></summary>
		public static long Insert(Transaction trans) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				trans.TransactionNum=Meth.GetLong(MethodBase.GetCurrentMethod(),trans);
				return trans.TransactionNum;
			}
			return Crud.TransactionCrud.Insert(trans);
		}

		///<summary></summary>
		public static void Update(Transaction trans) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),trans);
				return;
			}
			Crud.TransactionCrud.Update(trans);
		}

		///<summary>Also deletes all journal entries for the transaction.  Will later throw an error if journal entries attached to any reconciles.  Be sure to surround with try-catch.</summary>
		public static void Delete(Transaction trans) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),trans);
				return;
			}
			string command="DELETE FROM journalentry WHERE TransactionNum="+POut.Long(trans.TransactionNum);
			Db.NonQ(command);
			command= "DELETE FROM transaction WHERE TransactionNum = "+POut.Long(trans.TransactionNum);
			Db.NonQ(command);
		}

	
	
		///<summary></summary>
		public static bool IsReconciled(Transaction trans){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),trans);
			}
			string command="SELECT COUNT(*) FROM journalentry WHERE ReconcileNum !=0"
				+" AND TransactionNum="+POut.Long(trans.TransactionNum);
			if(Db.GetCount(command)=="0") {
				return false;
			}
			return true;
		}



	}

	
}




