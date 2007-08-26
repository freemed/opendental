using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using OpenDentBusiness;

namespace OpenDentBusiness {
	///<summary></summary>
	public class MountDefs {
		///<summary>A list of all MountDefs.</summary>
		public static List<MountDef> Listt;

		///<summary>Gets a list of all MountDefs when program first opens.  Also refreshes MountItemDefs and attaches all items to the appropriate mounts.</summary>
		public static void Refresh() {
			MountItemDefs.Refresh();
			string command="SELECT * FROM mountdef ORDER BY ItemOrder";
			DataTable table=General2.GetTable(command);
			Listt=new List<MountDef>();
			MountDef mount;
			for(int i=0;i<table.Rows.Count;i++) {
				mount=new MountDef();
				mount.MountDefNum =PIn.PInt   (table.Rows[i][0].ToString());
				mount.Description =PIn.PString(table.Rows[i][1].ToString());
				mount.ItemOrder   =PIn.PInt   (table.Rows[i][2].ToString());
				mount.IsRadiograph=PIn.PBool  (table.Rows[i][3].ToString());
				mount.Width       =PIn.PInt   (table.Rows[i][4].ToString());
				mount.Height      =PIn.PInt   (table.Rows[i][5].ToString());
				Listt.Add(mount);
			}
		}	

		///<summary></summary>
		public static void Update(MountDef def) {
			string command="UPDATE mountdef SET " 
				+"Description = '"   +POut.PString(def.Description)+"'"
				+",ItemOrder = '" +POut.PInt(def.ItemOrder)+"'"
				+",IsRadiograph = '" +POut.PBool(def.IsRadiograph)+"'"
				+",Width = '" +POut.PInt(def.Width)+"'"
				+",Height = '" +POut.PInt(def.Height)+"'"
				+" WHERE MountDefNum  ='"+POut.PInt (def.MountDefNum)+"'";
			General2.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(MountDef def) {
			string command="INSERT INTO mountdef (Description,ItemOrder,IsRadiograph,Width,Height"
				+") VALUES("
				+"'"+POut.PString(def.Description)+"', "
				+"'"+POut.PInt(def.ItemOrder)+"', "
				+"'"+POut.PBool(def.IsRadiograph)+"', "
				+"'"+POut.PInt(def.Width)+"', "
				+"'"+POut.PInt(def.Height)+"')";
			def.MountDefNum=General2.NonQ(command,true);
		}

		///<summary>No need to surround with try/catch, because all deletions are allowed.</summary>
		public static void Delete(int mountDefNum) {
			string command="DELETE FROM mountdef WHERE MountDefNum="+POut.PInt(mountDefNum);
			General2.NonQ(command);
			command="DELETE FROM mountitemdef WHERE MountDefNum ="+POut.PInt(mountDefNum);
			General2.NonQ(command);
		}

		
		
		
	}

		



		
	

	

	


}










