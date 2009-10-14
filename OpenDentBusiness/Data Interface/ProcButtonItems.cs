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
				List[i].ProcButtonItemNum=PIn.PLong(table.Rows[i][0].ToString());
				List[i].ProcButtonNum=PIn.PLong(table.Rows[i][1].ToString());
				List[i].OldCode=PIn.PString(table.Rows[i][2].ToString());
				List[i].AutoCodeNum=PIn.PLong(table.Rows[i][3].ToString());
				List[i].CodeNum=PIn.PLong(table.Rows[i][4].ToString());
			}
		}

		///<summary>Must have already checked procCode for nonduplicate.</summary>
		public static long Insert(ProcButtonItem item) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				item.ProcButtonItemNum=Meth.GetLong(MethodBase.GetCurrentMethod(),item);
				return item.ProcButtonItemNum;
			}
			if(PrefC.RandomKeys) {
				item.ProcButtonItemNum=ReplicationServers.GetKey("procbuttonitem","ProcButtonItemNum");
			}
			string command="INSERT INTO procbuttonitem (";
			if(PrefC.RandomKeys) {
				command+="ProcButtonItemNum,";
			}
			command+="ProcButtonNum,OldCode,AutoCodeNum,CodeNum) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PLong(item.ProcButtonItemNum)+", ";
			}
			command+=
				 "'"+POut.PLong   (item.ProcButtonNum)+"', "
				+"'"+POut.PString(item.OldCode)+"', "
				+"'"+POut.PLong   (item.AutoCodeNum)+"', "
				+"'"+POut.PLong   (item.CodeNum)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else{
				item.ProcButtonItemNum=Db.NonQ(command,true);
			}
			return item.ProcButtonItemNum;
		}

		///<summary></summary>
		public static void Update(ProcButtonItem item) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),item);
				return;
			}
			string command="UPDATE procbuttonitem SET " 
				+ "ProcButtonNum='"+POut.PLong   (item.ProcButtonNum)+"'"
				+ ",OldCode='"     +POut.PString(item.OldCode)+"'"
				+ ",AutoCodeNum='" +POut.PLong   (item.AutoCodeNum)+"'"
				+ ",CodeNum='" +POut.PLong   (item.CodeNum)+"'"
				+" WHERE ProcButtonItemNum = '"+POut.PLong(item.ProcButtonItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ProcButtonItem item) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),item);
				return;
			}
			string command="DELETE FROM procbuttonitem WHERE ProcButtonItemNum = '"+POut.PLong(item.ProcButtonItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long[] GetCodeNumListForButton(long procButtonNum) {
			//No need to check RemotingRole; no call to db.
			ArrayList ALCodes=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcButtonNum==procButtonNum && List[i].AutoCodeNum==0){
					ALCodes.Add(List[i].CodeNum);
				} 
			}
			long[] codeList=new long[ALCodes.Count];
			if(ALCodes.Count > 0){
				ALCodes.CopyTo(codeList);
			}
			return codeList;
		}

		///<summary></summary>
		public static long[] GetAutoListForButton(long procButtonNum) {
			//No need to check RemotingRole; no call to db.
			ArrayList ALautoCodes=new ArrayList();
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcButtonNum==procButtonNum && List[i].AutoCodeNum > 0){
					ALautoCodes.Add(List[i].AutoCodeNum);
				}
			}
			long[] autoCodeList=new long[ALautoCodes.Count];
			if(ALautoCodes.Count > 0) {
				ALautoCodes.CopyTo(autoCodeList);
			}
			return autoCodeList;
		}

		///<summary></summary>
		public static void DeleteAllForButton(long procButtonNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procButtonNum);
				return;
			}
			string command= "DELETE from procbuttonitem WHERE procbuttonnum = '"+POut.PLong(procButtonNum)+"'";
			Db.NonQ(command);
		}

	}

	




}










