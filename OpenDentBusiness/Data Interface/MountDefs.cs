using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class MountDefs {
		
		///<summary>Gets a list of all MountDefs when program first opens.  Also refreshes MountItemDefs and attaches all items to the appropriate mounts.</summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			//MountItemDefs.Refresh();
			string command="SELECT * FROM mountdef ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="MountDef";
			FillCache(table);
			return table;
		}	

		private static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			MountDefC.Listt=new List<MountDef>();
			MountDef mount;
			for(int i=0;i<table.Rows.Count;i++) {
				mount=new MountDef();
				mount.MountDefNum =PIn.Long   (table.Rows[i][0].ToString());
				mount.Description =PIn.String(table.Rows[i][1].ToString());
				mount.ItemOrder   =PIn.Int   (table.Rows[i][2].ToString());
				mount.IsRadiograph=PIn.Bool  (table.Rows[i][3].ToString());
				mount.Width       =PIn.Int   (table.Rows[i][4].ToString());
				mount.Height      =PIn.Int   (table.Rows[i][5].ToString());
				MountDefC.Listt.Add(mount);
			}
		}

		///<summary></summary>
		public static void Update(MountDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			string command="UPDATE mountdef SET " 
				+"Description = '"   +POut.String(def.Description)+"'"
				+",ItemOrder = '" +POut.Long(def.ItemOrder)+"'"
				+",IsRadiograph = '" +POut.Bool(def.IsRadiograph)+"'"
				+",Width = '" +POut.Long(def.Width)+"'"
				+",Height = '" +POut.Long(def.Height)+"'"
				+" WHERE MountDefNum  ='"+POut.Long (def.MountDefNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(MountDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				def.MountDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),def);
				return def.MountDefNum;
			}
			if(PrefC.RandomKeys) {
				def.MountDefNum=ReplicationServers.GetKey("mountdef","MountDefNum");
			}
			string command="INSERT INTO mountdef (";
			if(PrefC.RandomKeys) {
				command+="MountDefNum,";
			}
			command+="Description,ItemOrder,IsRadiograph,Width,Height) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(def.MountDefNum)+", ";
			}
			command+=
				 "'"+POut.String(def.Description)+"', "
				+"'"+POut.Long(def.ItemOrder)+"', "
				+"'"+POut.Bool(def.IsRadiograph)+"', "
				+"'"+POut.Long(def.Width)+"', "
				+"'"+POut.Long(def.Height)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else{
				def.MountDefNum=Db.NonQ(command,true);
			}
			return def.MountDefNum;
		}

		///<summary>No need to surround with try/catch, because all deletions are allowed.</summary>
		public static void Delete(long mountDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),mountDefNum);
				return;
			}
			string command="DELETE FROM mountdef WHERE MountDefNum="+POut.Long(mountDefNum);
			Db.NonQ(command);
			command="DELETE FROM mountitemdef WHERE MountDefNum ="+POut.Long(mountDefNum);
			Db.NonQ(command);
		}

		
		
		
	}

		



		
	

	

	


}










