using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;
using System.Data;
using CodeBase;

namespace OpenDental {
	class MountItems {

		public static int Insert(MountItem mountItem) {
			string command="INSERT INTO mountitem (MountItemNum,MountNum,Xpos,Ypos) VALUES ("
				+"'"+POut.PInt(mountItem.MountItemNum)+"',"
				+"'"+POut.PInt(mountItem.MountNum)+"',"
				+"'"+POut.PInt(mountItem.Xpos)+"',"
				+"'"+POut.PInt(mountItem.Ypos)+"')";
			return General.NonQEx(command);
		}

		public static int Update(MountItem mountItem) {
			string command="UPDATE mountitem SET "
				+"MountNum='"+POut.PInt(mountItem.MountNum)+"',"
				+"Xpos='"+POut.PInt(mountItem.Xpos)+"',"
				+"Ypos='"+POut.PInt(mountItem.Ypos)+"' "
				+"WHERE MountItemNum='"+POut.PInt(mountItem.MountItemNum)+"'";
			return General.NonQEx(command);
		}

		public static void Delete(MountItem mountItem) {
			string command="DELETE FROM mountitem WHERE MountItemNum='"+POut.PInt(mountItem.MountItemNum)+"'";
			General.NonQEx(command);
		}

		///<summary>Converts the given datarow to a mountitem, assuming that the row represents a mountitem.</summary>
		public static MountItem Fill(DataRow mountItemRow){
			MountItem mountItem=new MountItem();
			mountItem.MountItemNum=PIn.PInt(mountItemRow["MountItemNum"].ToString());
			mountItem.MountNum=PIn.PInt(mountItemRow["MountNum"].ToString());
			mountItem.Xpos=PIn.PInt(mountItemRow["Xpos"].ToString());
			mountItem.Ypos=PIn.PInt(mountItemRow["Ypos"].ToString());
			return mountItem;
		}

		///<summary>Returns the list of mount items associated with the given mount key.</summary>
		public static MountItem[] GetItemsForMount(int mountNum){
			string command="SELECT * FROM mountitem WHERE MountNum='"+POut.PInt(mountNum)+"'";
			DataTable result=General.GetTable(command);
			MountItem[] mountItems=new MountItem[result.Rows.Count];
			for(int i=0;i<mountItems.Length;i++){
				mountItems[i]=Fill(result.Rows[i]);
			}
			return mountItems;
		}

	}
}