using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ProcButtonItems {
		///<summary>All procbuttonitems for all buttons.</summary>
		private static ProcButtonItem[] list;

		public static ProcButtonItem[] List {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM procbuttonitem";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ProcButtonItem";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List=new ProcButtonItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new ProcButtonItem();
				List[i].ProcButtonItemNum=PIn.PInt(table.Rows[i][0].ToString());
				List[i].ProcButtonNum=PIn.PInt(table.Rows[i][1].ToString());
				List[i].OldCode=PIn.PString(table.Rows[i][2].ToString());
				List[i].AutoCodeNum=PIn.PInt(table.Rows[i][3].ToString());
				List[i].CodeNum=PIn.PInt(table.Rows[i][4].ToString());
			}
		}

		///<summary>Must have already checked procCode for nonduplicate.</summary>
		public static void Insert(ProcButtonItem item) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),item);
				return;
			}
			string command="INSERT INTO procbuttonitem (ProcButtonNum,OldCode,AutoCodeNum,CodeNum) VALUES("
				+"'"+POut.PInt   (item.ProcButtonNum)+"', "
				+"'"+POut.PString(item.OldCode)+"', "
				+"'"+POut.PInt   (item.AutoCodeNum)+"', "
				+"'"+POut.PInt   (item.CodeNum)+"')";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Update(ProcButtonItem item) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),item);
				return;
			}
			string command="UPDATE procbuttonitem SET " 
				+ "ProcButtonNum='"+POut.PInt   (item.ProcButtonNum)+"'"
				+ ",OldCode='"     +POut.PString(item.OldCode)+"'"
				+ ",AutoCodeNum='" +POut.PInt   (item.AutoCodeNum)+"'"
				+ ",CodeNum='" +POut.PInt   (item.CodeNum)+"'"
				+" WHERE ProcButtonItemNum = '"+POut.PInt(item.ProcButtonItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ProcButtonItem item) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),item);
				return;
			}
			string command="DELETE FROM procbuttonitem WHERE ProcButtonItemNum = '"+POut.PInt(item.ProcButtonItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static int[] GetCodeNumListForButton(int procButtonNum){
			//No need to check RemotingRole; no call to db.
			ArrayList ALCodes=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcButtonNum==procButtonNum && List[i].AutoCodeNum==0){
					ALCodes.Add(List[i].CodeNum);
				} 
			}
			int[] codeList=new int[ALCodes.Count];
			if(ALCodes.Count > 0){
				ALCodes.CopyTo(codeList);
			}
			return codeList;
		}

		///<summary></summary>
		public static int[] GetAutoListForButton(int procButtonNum) {
			//No need to check RemotingRole; no call to db.
			ArrayList ALautoCodes=new ArrayList();
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcButtonNum==procButtonNum && List[i].AutoCodeNum > 0){
					ALautoCodes.Add(List[i].AutoCodeNum);
				}
			}
			int[] autoCodeList=new int[ALautoCodes.Count];
			if(ALautoCodes.Count > 0) {
				ALautoCodes.CopyTo(autoCodeList);
			}
			return autoCodeList;
		}

		///<summary></summary>
		public static void DeleteAllForButton(int procButtonNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procButtonNum);
				return;
			}
			string command= "DELETE from procbuttonitem WHERE procbuttonnum = '"+POut.PInt(procButtonNum)+"'";
			Db.NonQ(command);
		}

	}

	




}










