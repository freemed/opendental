using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class SigElementDefs {
		///<summary>A list of all SigElementDefs.</summary>
		private static SigElementDef[] list;

		public static SigElementDef[] List {
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

		///<summary>Gets a list of all SigElementDefs when program first opens.</summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM sigelementdef ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="SigElementDef";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=new SigElementDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				list[i]=new SigElementDef();
				list[i].SigElementDefNum= PIn.PLong(table.Rows[i][0].ToString());
				list[i].LightRow        = PIn.PInt(table.Rows[i][1].ToString());
				list[i].LightColor      = Color.FromArgb(PIn.PInt(table.Rows[i][2].ToString()));
				list[i].SigElementType  = (SignalElementType)PIn.PLong(table.Rows[i][3].ToString());
				list[i].SigText         = PIn.PString(table.Rows[i][4].ToString());
				list[i].Sound           = PIn.PString(table.Rows[i][5].ToString());
				list[i].ItemOrder       = PIn.PInt(table.Rows[i][6].ToString());
			}
		}
	
		///<summary></summary>
		public static void Update(SigElementDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			string command="UPDATE sigelementdef SET " 
				+"LightRow = '"       +POut.PLong   (def.LightRow)+"'"
				+",LightColor = '"    +POut.PLong   (def.LightColor.ToArgb())+"'"
				+",SigElementType = '"+POut.PLong   ((int)def.SigElementType)+"'"
				+",SigText = '"       +POut.PString(def.SigText)+"'"
				+",Sound = '"         +POut.PString(def.Sound)+"'"
				+",ItemOrder = '"     +POut.PLong   (def.ItemOrder)+"'"
				+" WHERE SigElementDefNum  ='"+POut.PLong   (def.SigElementDefNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(SigElementDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				def.SigElementDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),def);
				return def.SigElementDefNum;
			}
			if(PrefC.RandomKeys) {
				def.SigElementDefNum=ReplicationServers.GetKey("sigelementdef","SigElementDefNum");
			}
			string command="INSERT INTO sigelementdef (";
			if(PrefC.RandomKeys) {
				command+="SigElementDefNum,";
			}
			command+="LightRow,LightColor,SigElementType,SigText,Sound,"
				+"ItemOrder) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PLong(def.SigElementDefNum)+", ";
			}
			command+=
				 "'"+POut.PLong   (def.LightRow)+"', "
				+"'"+POut.PLong   (def.LightColor.ToArgb())+"', "
				+"'"+POut.PLong   ((int)def.SigElementType)+"', "
				+"'"+POut.PString(def.SigText)+"', "
				+"'"+POut.PString(def.Sound)+"', "
				+"'"+POut.PLong   (def.ItemOrder)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				def.SigElementDefNum=Db.NonQ(command,true);
			}
			return def.SigElementDefNum;
		}

		///<summary>No need to surround with try/catch, because all deletions are allowed.  This routine, deletes references in the SigButDefElement table.  References in the SigElement table are left hanging.  The user interface needs to be able to handle missing elementdefs.</summary>
		public static void Delete(SigElementDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			string command="DELETE FROM sigbutdefelement WHERE SigElementDefNum="+POut.PLong(def.SigElementDefNum);
			Db.NonQ(command);
			command="DELETE FROM sigelementdef WHERE SigElementDefNum ="+POut.PLong(def.SigElementDefNum);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static SigElementDef[] GetSubList(SignalElementType sigElementType){
			//No need to check RemotingRole; no call to db.
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(sigElementType==List[i].SigElementType){
					AL.Add(List[i]);
				}
			}
			SigElementDef[] retVal=new SigElementDef[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Moves the selected item up in the supplied sub list.</summary>
		public static void MoveUp(int selected,SigElementDef[] subList){
			//No need to check RemotingRole; no call to db.
			if(selected<0) {
				throw new ApplicationException(Lans.g("SigElementDefs","Please select an item first."));
			}
			if(selected==0) {//already at top
				return;
			}
			if(selected>subList.Length-1){
				throw new ApplicationException(Lans.g("SigElementDefs","Invalid selection."));
			}
			SetOrder(selected-1,subList[selected].ItemOrder,subList);
			SetOrder(selected,subList[selected].ItemOrder-1,subList);
			//Selected-=1;
		}

		///<summary></summary>
		public static void MoveDown(int selected,SigElementDef[] subList) {
			//No need to check RemotingRole; no call to db.
			if(selected<0) {
				throw new ApplicationException(Lans.g("SigElementDefs","Please select an item first."));
			}
			if(selected==subList.Length-1){//already at bottom
				return;
			}
			if(selected>subList.Length-1) {
				throw new ApplicationException(Lans.g("SigElementDefs","Invalid selection."));
			}
			SetOrder(selected+1,subList[selected].ItemOrder,subList);
			SetOrder(selected,subList[selected].ItemOrder+1,subList);
			//selected+=1;
		}

		///<summary>Used by MoveUp and MoveDown.</summary>
		private static void SetOrder(int mySelNum,int myItemOrder,SigElementDef[] subList) {
			//No need to check RemotingRole; no call to db.
			SigElementDef temp=subList[mySelNum];
			temp.ItemOrder=myItemOrder;
			Update(temp);
		}

		///<summary>Returns the SigElementDef with the specified num.</summary>
		public static SigElementDef GetElement(long SigElementDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++) {
				if(List[i].SigElementDefNum==SigElementDefNum) {
					return List[i].Copy();
				}
			}
			return null;
		}
		
		
	}

		



		
	

	

	


}










