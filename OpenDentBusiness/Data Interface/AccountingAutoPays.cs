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
			string command="SELECT * FROM accountingautopay";
			DataTable table=Meth.GetTable(MethodInfo.GetCurrentMethod(),command);
			table.TableName="AccountingAutoPay";
			FillCache(table);
			return table;
		}

		private static void FillCache(DataTable table){
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
		public static void Insert(AccountingAutoPay pay){
			string command= "INSERT INTO accountingautopay (PayType,PickList) VALUES("
				+"'"+POut.PInt   (pay.PayType)+"', "
				+"'"+POut.PString(pay.PickList)+"')";
			pay.AccountingAutoPayNum=General.NonQ(command,true);
		}

		///<summary>Converts the comma delimited list of AccountNums into full descriptions separated by carriage returns.</summary>
		public static string GetPickListDesc(AccountingAutoPay pay){
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
		public static int[] GetPickListAccounts(AccountingAutoPay pay) {
			string[] numArray=pay.PickList.Split(new char[] { ',' });
			ArrayList AL=new ArrayList();
			for(int i=0;i<numArray.Length;i++) {
				if(numArray[i]=="") {
					continue;
				}
				AL.Add(PIn.PInt(numArray[i]));
			}
			int[] retVal=new int[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Loops through the AList to find one with the specified payType (defNum).  If none is found, then it returns null.</summary>
		public static AccountingAutoPay GetForPayType(int payType){
			for(int i=0;i<AccountingAutoPayC.AList.Count;i++){
				if(((AccountingAutoPay)AccountingAutoPayC.AList[i]).PayType==payType){
					return (AccountingAutoPay)AccountingAutoPayC.AList[i];
				}
			}
			return null;
		}

		///<summary>Saves the list of accountingAutoPays to the database.  Deletes all existing ones first.</summary>
		public static void SaveList(ArrayList AL) {
			string command="DELETE FROM accountingautopay";
			General.NonQ(command);
			for(int i=0;i<AL.Count;i++){
				Insert((AccountingAutoPay)AL[i]);
			}
		}

		

	}
	


}













