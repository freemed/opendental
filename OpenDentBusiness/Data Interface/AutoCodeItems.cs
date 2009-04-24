using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AutoCodeItems{
		///<summary></summary>
		public static AutoCodeItem[] ListForCode;//all items for a specific AutoCode

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemovelyIfNeeded().
			string command="SELECT * FROM autocodeitem";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AutoCodeItem";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			AutoCodeItemC.HList=new Hashtable();
			AutoCodeItemC.List=new AutoCodeItem[table.Rows.Count];
			for(int i=0;i<AutoCodeItemC.List.Length;i++){
				AutoCodeItemC.List[i]=new AutoCodeItem();
				AutoCodeItemC.List[i].AutoCodeItemNum= PIn.PInt   (table.Rows[i][0].ToString());
				AutoCodeItemC.List[i].AutoCodeNum    = PIn.PInt   (table.Rows[i][1].ToString());
				//List[i].OldCode      = PIn.PString(table.Rows[i][2].ToString());
				AutoCodeItemC.List[i].CodeNum        = PIn.PInt   (table.Rows[i][3].ToString());
				if(!AutoCodeItemC.HList.ContainsKey(AutoCodeItemC.List[i].CodeNum)){
					AutoCodeItemC.HList.Add(AutoCodeItemC.List[i].CodeNum,AutoCodeItemC.List[i].AutoCodeNum);
				}
			}
		}

		///<summary></summary>
		public static void Insert(AutoCodeItem Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "INSERT INTO autocodeitem (autocodenum,OldCode,CodeNum) "
				+"VALUES ("
				+"'"+POut.PInt   (Cur.AutoCodeNum)+"', "
				+"'"+POut.PString(Cur.OldCode)+"', "
				+"'"+POut.PInt   (Cur.CodeNum)+"')";
			//MessageBox.Show(string command);
			Cur.AutoCodeItemNum=Db.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(AutoCodeItem Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "UPDATE autocodeitem SET "
				+"AutoCodeNum='"+POut.PInt   (Cur.AutoCodeNum)+"'"
				//+",Oldcode ='"  +POut.PString(Cur.OldCode)+"'"
				+",CodeNum ='"  +POut.PInt   (Cur.CodeNum)+"'"
				+" WHERE AutoCodeItemNum = '"+POut.PInt(Cur.AutoCodeItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(AutoCodeItem Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "DELETE FROM autocodeitem WHERE AutoCodeItemNum = '"
				+POut.PInt(Cur.AutoCodeItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(int autoCodeNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),autoCodeNum);
				return;
			}
			string command= "DELETE FROM autocodeitem WHERE AutoCodeNum = '"
				+POut.PInt(autoCodeNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void GetListForCode(int autoCodeNum){
			//No need to check RemotingRole; no call to db.
			//loop through AutoCodeItems.List to fill ListForCode
			ArrayList ALtemp=new ArrayList();
			for(int i=0;i<AutoCodeItemC.List.Length;i++){
				if(AutoCodeItemC.List[i].AutoCodeNum==autoCodeNum){
					ALtemp.Add(AutoCodeItemC.List[i]);
				} 
			}
			ListForCode=new AutoCodeItem[ALtemp.Count];
			if(ALtemp.Count>0){
				ALtemp.CopyTo(ListForCode);
			}     
		}

		//-----

		///<summary>Only called from ContrChart.listProcButtons_Click.  Called once for each tooth selected and for each autocode item attached to the button.</summary>
		public static int GetCodeNum(int autoCodeNum,string toothNum,string surf,bool isAdditional,int patNum,int age) {
			//No need to check RemotingRole; no call to db.
			bool allCondsMet;
			AutoCodeItems.GetListForCode(autoCodeNum);
			if(AutoCodeItems.ListForCode.Length==0) {
				return 0;
			}
			bool willBeMissing=Procedures.WillBeMissing(toothNum,patNum);
			for(int i=0;i<AutoCodeItems.ListForCode.Length;i++) {
				AutoCodeConds.GetListForItem(AutoCodeItems.ListForCode[i].AutoCodeItemNum);
				allCondsMet=true;
				for(int j=0;j<AutoCodeConds.ListForItem.Length;j++) {
					if(!AutoCodeConds.ConditionIsMet
						(AutoCodeConds.ListForItem[j].Cond,toothNum,surf,isAdditional,willBeMissing,age)) {
						allCondsMet=false;
					}
				}
				if(allCondsMet) {
					return AutoCodeItems.ListForCode[i].CodeNum;
				}
			}
			return AutoCodeItems.ListForCode[0].CodeNum;//if couldn't find a better match
		}

		///<summary>Only called when closing the procedure edit window. Usually returns the supplied CodeNum, unless a better match is found.</summary>
		public static int VerifyCode(int codeNum,string toothNum,string surf,bool isAdditional,int patNum,int age,
			out AutoCode AutoCodeCur) {
			//No need to check RemotingRole; no call to db.
			bool allCondsMet;
			AutoCodeCur=null;
			if(!AutoCodeItemC.HList.ContainsKey(codeNum)) {
				return codeNum;
			}
			if(!AutoCodeC.HList.ContainsKey((int)AutoCodeItemC.HList[codeNum])) {
				return codeNum;//just in case.
			}
			AutoCodeCur=(AutoCode)AutoCodeC.HList[(int)AutoCodeItemC.HList[codeNum]];
			if(AutoCodeCur.LessIntrusive) {
				return codeNum;
			}
			bool willBeMissing=Procedures.WillBeMissing(toothNum,patNum);
			AutoCodeItems.GetListForCode((int)AutoCodeItemC.HList[codeNum]);
			for(int i=0;i<AutoCodeItems.ListForCode.Length;i++) {
				AutoCodeConds.GetListForItem(AutoCodeItems.ListForCode[i].AutoCodeItemNum);
				allCondsMet=true;
				for(int j=0;j<AutoCodeConds.ListForItem.Length;j++) {
					if(!AutoCodeConds.ConditionIsMet
						(AutoCodeConds.ListForItem[j].Cond,toothNum,surf,isAdditional,willBeMissing,age)) {
						allCondsMet=false;
					}
				}
				if(allCondsMet) {
					return AutoCodeItems.ListForCode[i].CodeNum;
				}
			}
			return codeNum;//if couldn't find a better match
		}

		



	}

	
	


}









