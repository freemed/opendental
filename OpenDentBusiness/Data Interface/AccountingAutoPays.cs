using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AccountingAutoPays {
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
			AccountingAutoPay[] List=new AccountingAutoPay[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new AccountingAutoPay();
				List[i].AccountingAutoPayNum = PIn.PInt(table.Rows[i][0].ToString());
				List[i].PayType              = PIn.PInt(table.Rows[i][1].ToString());
				List[i].PickList             = PIn.PString(table.Rows[i][2].ToString());
			}
			AccountingAutoPayC.AList=new ArrayList();
			for(int i=0;i<List.Length;i++){
				AccountingAutoPayC.AList.Add(List[i]);
			}
		}
		
		///<summary></summary>
		public static long Insert(AccountingAutoPay pay) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				pay.AccountingAutoPayNum=Meth.GetInt(MethodBase.GetCurrentMethod(),pay);
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
				command+=POut.PInt(pay.AccountingAutoPayNum)+", ";
			}
			command+=
				 "'"+POut.PInt   (pay.PayType)+"', "
				+"'"+POut.PString(pay.PickList)+"')";
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
				retVal+=AccountC.GetDescript(PIn.PInt(numArray[i]));
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
				AL.Add(PIn.PInt(numArray[i]));
			}
			long[] retVal=new long[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Loops through the AList to find one with the specified payType (defNum).  If none is found, then it returns null.</summary>
		public static AccountingAutoPay GetForPayType(long payType) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<AccountingAutoPayC.AList.Count;i++){
				if(((AccountingAutoPay)AccountingAutoPayC.AList[i]).PayType==payType){
					return (AccountingAutoPay)AccountingAutoPayC.AList[i];
				}
			}
			return null;
		}

		///<summary>Saves the list of accountingAutoPays to the database.  Deletes all existing ones first.</summary>
		public static void SaveList(ArrayList AL) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),AL);
				return;
			}
			string command="DELETE FROM accountingautopay";
			Db.NonQ(command);
			for(int i=0;i<AL.Count;i++){
				Insert((AccountingAutoPay)AL[i]);
			}
		}

	}

}