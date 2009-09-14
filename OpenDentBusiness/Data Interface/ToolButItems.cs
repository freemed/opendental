using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
  ///<summary></summary>
	public class ToolButItems{
		///<summary></summary>
		private static ToolButItem[] list;
		//<summary></summary>
		//public static ArrayList ForProgram;

		public static ToolButItem[] List {
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

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * from toolbutitem";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ToolButItem";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=new ToolButItem[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				list[i]=new ToolButItem();
				list[i].ToolButItemNum  =PIn.PInt   (table.Rows[i][0].ToString());
				list[i].ProgramNum      =PIn.PInt   (table.Rows[i][1].ToString());
				list[i].ToolBar         =(ToolBarsAvail)PIn.PInt(table.Rows[i][2].ToString());
				list[i].ButtonText      =PIn.PString(table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static long Insert(ToolButItem Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.ToolButItemNum=Meth.GetInt(MethodBase.GetCurrentMethod(),Cur);
				return Cur.ToolButItemNum;
			}
			if(PrefC.RandomKeys) {
				Cur.ToolButItemNum=ReplicationServers.GetKey("toolbutitem","ToolButItemNum");
			}
			string command="INSERT INTO toolbutitem (";
			if(PrefC.RandomKeys) {
				command+="ToolButItemNum,";
			}
			command+=") VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PInt(Cur.ToolButItemNum)+", ";
			}
			command+=
				 "'"+POut.PInt   (Cur.ProgramNum)+"', "
				+"'"+POut.PInt   ((int)Cur.ToolBar)+"', "
				+"'"+POut.PString(Cur.ButtonText)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else{
				Cur.ToolButItemNum=Db.NonQ(command,true);
			}
			return Cur.ToolButItemNum;
		}

		///<summary>This in not currently being used.</summary>
		public static void Update(ToolButItem Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "UPDATE toolbutitem SET "
				+"ProgramNum ='" +POut.PInt   (Cur.ProgramNum)+"'"
				+",ToolBar ='"   +POut.PInt   ((int)Cur.ToolBar)+"'"
				+",ButtonText ='"+POut.PString(Cur.ButtonText)+"'"
				+" WHERE ToolButItemNum = '"+POut.PInt(Cur.ToolButItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary>This is not currently being used.</summary>
		public static void Delete(ToolButItem Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE from toolbutitem WHERE ToolButItemNum = '"
				+POut.PInt(Cur.ToolButItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Deletes all ToolButItems for the Programs.Cur.  This is used regularly when saving a Program link because of the way the user interface works.</summary>
		public static void DeleteAllForProgram(long programNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),programNum);
				return;
			}
			string command = "DELETE from toolbutitem WHERE ProgramNum = '"
				+POut.PInt(programNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Fills ForProgram with toolbutitems attached to the Programs.Cur</summary>
		public static List<ToolButItem> GetForProgram(long programNum) {
			//No need to check RemotingRole; no call to db.
			if(List==null) {
				RefreshCache();
			}
			List<ToolButItem> ForProgram=new List<ToolButItem>();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProgramNum==programNum){
					ForProgram.Add(List[i]);
				}
			}
			return ForProgram;
		}

		///<summary>Returns a list of toolbutitems for the specified toolbar. Used when laying out toolbars.</summary>
		public static ArrayList GetForToolBar(ToolBarsAvail toolbar) {
			//No need to check RemotingRole; no call to db.
			if(List==null) {
				RefreshCache();
			}
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ToolBar==toolbar && Programs.IsEnabled(List[i].ProgramNum)){
					retVal.Add(List[i]);
				}
			}
			return retVal;
		}


	}

	

}













