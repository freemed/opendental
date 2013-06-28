using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AutoCodeItems{

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM autocodeitem";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AutoCodeItem";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			AutoCodeItemC.HList=new Hashtable();
			AutoCodeItemC.List=Crud.AutoCodeItemCrud.TableToList(table).ToArray();
			for(int i=0;i<AutoCodeItemC.List.Length;i++){
				if(!AutoCodeItemC.HList.ContainsKey(AutoCodeItemC.List[i].CodeNum)){
					AutoCodeItemC.HList.Add(AutoCodeItemC.List[i].CodeNum,AutoCodeItemC.List[i].AutoCodeNum);
				}
			}
		}

		///<summary></summary>
		public static void ClearCache() {
			AutoCodeItemC.HList=null;
			AutoCodeItemC.List=null;
		}

		///<summary></summary>
		public static long Insert(AutoCodeItem Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.AutoCodeItemNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.AutoCodeItemNum;
			}
			return Crud.AutoCodeItemCrud.Insert(Cur);
		}

		///<summary></summary>
		public static void Update(AutoCodeItem Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			Crud.AutoCodeItemCrud.Update(Cur);
		}

		///<summary></summary>
		public static void Delete(AutoCodeItem Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "DELETE FROM autocodeitem WHERE AutoCodeItemNum = '"
				+POut.Long(Cur.AutoCodeItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(long autoCodeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),autoCodeNum);
				return;
			}
			string command= "DELETE FROM autocodeitem WHERE AutoCodeNum = '"
				+POut.Long(autoCodeNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Gets from cache.  No call to db.</summary>
		public static List<AutoCodeItem> GetListForCode(long autoCodeNum) {
			//No need to check RemotingRole; no call to db.
			//loop through AutoCodeItems.List to fill ListForCode
			List<AutoCodeItem> retVal=new List<AutoCodeItem>();
			for(int i=0;i<AutoCodeItemC.List.Length;i++){
				if(AutoCodeItemC.List[i].AutoCodeNum==autoCodeNum){
					retVal.Add(AutoCodeItemC.List[i]);
				} 
			}
			return retVal;    
		}

		//-----

		///<summary>Only called from ContrChart.listProcButtons_Click.  Called once for each tooth selected and for each autocode item attached to the button.</summary>
		public static long GetCodeNum(long autoCodeNum,string toothNum,string surf,bool isAdditional,long patNum,int age) {
			//No need to check RemotingRole; no call to db.
			bool allCondsMet;
			List<AutoCodeItem> listForCode=AutoCodeItems.GetListForCode(autoCodeNum);
			if(listForCode.Count==0) {
				return 0;
			}
			bool willBeMissing=Procedures.WillBeMissing(toothNum,patNum);
			List<AutoCodeCond> condList;
			for(int i=0;i<listForCode.Count;i++) {
				condList=AutoCodeConds.GetListForItem(listForCode[i].AutoCodeItemNum);
				allCondsMet=true;
				for(int j=0;j<condList.Count;j++) {
					if(!AutoCodeConds.ConditionIsMet(condList[j].Cond,toothNum,surf,isAdditional,willBeMissing,age)) {
						allCondsMet=false;
					}
				}
				if(allCondsMet) {
					return listForCode[i].CodeNum;
				}
			}
			return listForCode[0].CodeNum;//if couldn't find a better match
		}

		///<summary>Only called when closing the procedure edit window. Usually returns the supplied CodeNum, unless a better match is found.</summary>
		public static long VerifyCode(long codeNum,string toothNum,string surf,bool isAdditional,long patNum,int age,
			out AutoCode AutoCodeCur) {
			//No need to check RemotingRole; no call to db.
			bool allCondsMet;
			AutoCodeCur=null;
			if(!AutoCodeItemC.HList.ContainsKey(codeNum)) {
				return codeNum;
			}
			if(!AutoCodeC.HList.ContainsKey((long)AutoCodeItemC.HList[codeNum])) {
				return codeNum;//just in case.
			}
			AutoCodeCur=(AutoCode)AutoCodeC.HList[(long)AutoCodeItemC.HList[codeNum]];
			if(AutoCodeCur.LessIntrusive) {
				return codeNum;
			}
			bool willBeMissing=Procedures.WillBeMissing(toothNum,patNum);
			List<AutoCodeItem> listForCode=AutoCodeItems.GetListForCode((long)AutoCodeItemC.HList[codeNum]);
			List<AutoCodeCond> condList;
			for(int i=0;i<listForCode.Count;i++) {
				condList=AutoCodeConds.GetListForItem(listForCode[i].AutoCodeItemNum);
				allCondsMet=true;
				for(int j=0;j<condList.Count;j++) {
					if(!AutoCodeConds.ConditionIsMet(condList[j].Cond,toothNum,surf,isAdditional,willBeMissing,age)) {
						allCondsMet=false;
					}
				}
				if(allCondsMet) {
					return listForCode[i].CodeNum;
				}
			}
			return codeNum;//if couldn't find a better match
		}

		



	}

	
	


}









