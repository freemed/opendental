using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{

	public class XChargeTransactions { 

		///<summary></summary>
		public static long Insert(XChargeTransaction xChargeTransaction) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				xChargeTransaction.XChargeTransactionNum=Meth.GetLong(MethodBase.GetCurrentMethod(),xChargeTransaction);
				return xChargeTransaction.XChargeTransactionNum;
			}
			return Crud.XChargeTransactionCrud.Insert(xChargeTransaction);
		}

		///<summary>Gets one XChargeTransaction from the db by batchNum and itemNum. For example: ("1515","0001").</summary>
		public static XChargeTransaction CheckByBatchItem(string batchNum,string itemNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<XChargeTransaction>(MethodBase.GetCurrentMethod(),batchNum,itemNum);
			}
			string command="SELECT * FROM xchargetransaction WHERE BatchNum = '"+POut.String(batchNum)+"' AND ItemNum = '"+POut.String(itemNum)+"'";
			return Crud.XChargeTransactionCrud.SelectOne(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<XChargeTransaction> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<XChargeTransaction>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM xchargetransaction WHERE PatNum = "+POut.Long(patNum);
			return Crud.XChargeTransactionCrud.SelectMany(command);
		}

		///<summary></summary>
		public static void Update(XChargeTransaction xChargeTransaction){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),xChargeTransaction);
				return;
			}
			Crud.XChargeTransactionCrud.Update(xChargeTransaction);
		}

		///<summary></summary>
		public static void Delete(long xChargeTransactionNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),xChargeTransactionNum);
				return;
			}
			string command= "DELETE FROM xchargetransaction WHERE XChargeTransactionNum = "+POut.Long(xChargeTransactionNum);
			Db.NonQ(command);
		}
		*/



	}
}