using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using OpenDentBusiness;

namespace OpenDentBusiness {
	///<summary></summary>
	public class MountItemDefs {
		/*
		///<summary>A list of all MountItemDefs.</summary>
		public static List<MountItemDef> Listt;

		///<summary>Gets a list of all MountItemDefs when program first opens.</summary>
		public static void Refresh() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="SELECT * FROM mountitemdef";
			DataTable table=Db.GetTable(command);
			Listt=new List<MountItemDef>();
			MountItemDef mount;
			for(int i=0;i<table.Rows.Count;i++) {
				mount=new MountItemDef();
				mount.MountItemDefNum=PIn.PInt   (table.Rows[i][0].ToString());
				mount.MountDefNum    =PIn.PInt   (table.Rows[i][1].ToString());
				mount.Xpos           =PIn.PInt   (table.Rows[i][2].ToString());
				mount.Ypos           =PIn.PInt   (table.Rows[i][3].ToString());
				mount.Width          =PIn.PInt   (table.Rows[i][4].ToString());
				mount.Height         =PIn.PInt   (table.Rows[i][5].ToString());
				Listt.Add(mount);
			}
		}*/

		///<summary></summary>
		public static void Update(MountItemDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
			}
			string command="UPDATE mountitemdef SET " 
				+"MountDefNum = '"+POut.Long(def.MountDefNum)+"'"
				+",Xpos = '"      +POut.Long(def.Xpos)+"'"
				+",Ypos = '"      +POut.Long(def.Ypos)+"'"
				+",Width = '"     +POut.Long(def.Width)+"'"
				+",Height = '"    +POut.Long(def.Height)+"'"
				+" WHERE MountItemDefNum  ='"+POut.Long (def.MountItemDefNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(MountItemDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				def.MountItemDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),def);
				return def.MountItemDefNum;
			}
			if(PrefC.RandomKeys) {
				def.MountItemDefNum=ReplicationServers.GetKey("mountitemdef","MountItemDefNum");
			}
			string command="INSERT INTO mountitemdef (";
			if(PrefC.RandomKeys) {
				command+="MountItemDefNum,";
			}
			command+="MountDefNum,Xpos,Ypos,Width,Height) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(def.MountItemDefNum)+", ";
			}
			command+=
				 "'"+POut.Long(def.MountDefNum)+"', "
				+"'"+POut.Long(def.Xpos)+"', "
				+"'"+POut.Long(def.Ypos)+"', "
				+"'"+POut.Long(def.Width)+"', "
				+"'"+POut.Long(def.Height)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				def.MountItemDefNum=Db.NonQ(command,true);
			}
			return def.MountItemDefNum;
		}

		///<summary>No need to surround with try/catch, because all deletions are allowed.</summary>
		public static void Delete(long mountItemDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),mountItemDefNum);
				return;
			}
			string command="DELETE FROM mountitemdef WHERE MountItemDefNum="+POut.Long(mountItemDefNum);
			Db.NonQ(command);
		}

		
		
		
	}

		



		
	

	

	


}










