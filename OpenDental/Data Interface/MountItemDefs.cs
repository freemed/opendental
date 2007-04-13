using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	///<summary></summary>
	public class MountItemDefs {
		///<summary>A list of all MountItemDefs.</summary>
		public static List<MountItemDef> Listt;

		///<summary>Gets a list of all MountItemDefs when program first opens.</summary>
		public static void Refresh() {
			string command="SELECT * FROM mountitemdef";
			DataTable table=General.GetTable(command);
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
		}	

		///<summary></summary>
		public static void Update(MountItemDef def) {
			string command="UPDATE mountitemdef SET " 
				+"MountDefNum = '"+POut.PInt(def.MountDefNum)+"'"
				+",Xpos = '"      +POut.PInt(def.Xpos)+"'"
				+",Ypos = '"      +POut.PInt(def.Ypos)+"'"
				+",Width = '"     +POut.PInt(def.Width)+"'"
				+",Height = '"    +POut.PInt(def.Height)+"'"
				+" WHERE MountItemDefNum  ='"+POut.PInt (def.MountItemDefNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(MountItemDef def) {
			string command="INSERT INTO mountitemdef (MountDefNum,Xpos,Ypos,Width,Height"
				+") VALUES("
				+"'"+POut.PInt(def.MountDefNum)+"', "
				+"'"+POut.PInt(def.Xpos)+"', "
				+"'"+POut.PInt(def.Ypos)+"', "
				+"'"+POut.PInt(def.Width)+"', "
				+"'"+POut.PInt(def.Height)+"')";
			def.MountItemDefNum=General.NonQ(command,true);
		}

		///<summary>No need to surround with try/catch, because all deletions are allowed.</summary>
		public static void Delete(int mountItemDefNum) {
			string command="DELETE FROM mountitemdef WHERE MountItemDefNum="+POut.PInt(mountItemDefNum);
			General.NonQ(command);
		}

		
		
		
	}

		



		
	

	

	


}










