using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AccountingAutoPays {
		///<summary></summary>
		private static List<AccountingAutoPay> listt;

		public static List<AccountingAutoPay> Listt {
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary>Gets a list of all AccountingAutoPays.</summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM accountingautopay";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AccountingAutoPay";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.AccountingAutoPayCrud.TableToList(table);
		}
		
		///<summary></summary>
		public static long Insert(AccountingAutoPay pay) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				pay.AccountingAutoPayNum=Meth.GetLong(MethodBase.GetCurrentMethod(),pay);
				return pay.AccountingAutoPayNum;
			}
			if(PrefC.RandomKeys) {
				pay.AccountingAutoPayNum=ReplicationServers.GetKey("accountingautopay","AccountingAutoPayNum");
			}
			string command="INSERT INTO accountingautopay (";
			if(PrefC.RandomKeys) {
				command+="AccountingAutoPayNum,";
			}
			command+="PayType,PickList) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(pay.AccountingAutoPayNum)+", ";
			}
			command+=
				 "'"+POut.Long   (pay.PayType)+"', "
				+"'"+POut.String(pay.PickList)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				pay.AccountingAutoPayNum=Db.NonQ(command,true);
			}
			return pay.AccountingAutoPayNum;
		}

		///<summary>Converts the comma delimited list of AccountNums into full descriptions separated by carriage returns.</summary>
		public static string GetPickListDesc(AccountingAutoPay pay){
			//No need to check RemotingRole; no call to db.
			string[] numArray=pay.PickList.Split(new char[] { ',' });
			string retVal="";
			for(int i=0;i<numArray.Length;i++) {
				if(numArray[i]=="") {
					continue;
				}
				if(retVal!=""){
					retVal+="\r\n";
				}
				retVal+=Accounts.GetDescript(PIn.Long(numArray[i]));
			}
			return retVal;
		}

		///<summary>Converts the comma delimited list of AccountNums into an array of AccountNums.</summary>
		public static long[] GetPickListAccounts(AccountingAutoPay pay) {
			//No need to check RemotingRole; no call to db.
			string[] numArray=pay.PickList.Split(new char[] { ',' });
			ArrayList AL=new ArrayList();
			for(int i=0;i<numArray.Length;i++) {
				if(numArray[i]=="") {
					continue;
				}
				AL.Add(PIn.Long(numArray[i]));
			}
			long[] retVal=new long[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Loops through the AList to find one with the specified payType (defNum).  If none is found, then it returns null.</summary>
		public static AccountingAutoPay GetForPayType(long payType) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<AccountingAutoPays.Listt.Count;i++){
				if(AccountingAutoPays.Listt[i].PayType==payType){
					return AccountingAutoPays.Listt[i];
				}
			}
			return null;
		}

		///<summary>Saves the list of accountingAutoPays to the database.  Deletes all existing ones first.</summary>
		public static void SaveList(List<AccountingAutoPay> list) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),list);
				return;
			}
			string command="DELETE FROM accountingautopay";
			Db.NonQ(command);
			for(int i=0;i<list.Count;i++){
				Insert(list[i]);
			}
		}

	}

}