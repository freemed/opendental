using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using OpenDentBusiness;
using CodeBase;

namespace OpenDentBusiness {
	public class MountItems {

		public static long Insert(MountItem mountItem) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				mountItem.MountItemNum=Meth.GetLong(MethodBase.GetCurrentMethod(),mountItem);
				return mountItem.MountItemNum;
			}
			if(PrefC.RandomKeys) {
				mountItem.MountItemNum=ReplicationServers.GetKey("mountitem","MountItemNum");
			}
			string command="INSERT INTO mountitem (";
			if(PrefC.RandomKeys) {
				command+="MountItemNum,";
			}
			command+="MountNum,Xpos,Ypos,OrdinalPos,Width,Height) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.Long(mountItem.MountItemNum)+"',";
			}
			command+=
				 "'"+POut.Long(mountItem.MountNum)+"',"
				+"'"+POut.Long(mountItem.Xpos)+"',"
				+"'"+POut.Long(mountItem.Ypos)+"',"
				+"'"+POut.Long(mountItem.OrdinalPos)+"',"
				+"'"+POut.Long(mountItem.Width)+"',"
				+"'"+POut.Long(mountItem.Height)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			} else {
				mountItem.MountItemNum=Db.NonQ(command,true);
			}
			return mountItem.MountItemNum;
		}

		public static long Update(MountItem mountItem) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),mountItem);
			}
			string command="UPDATE mountitem SET "
				+"MountNum='"+POut.Long(mountItem.MountNum)+"',"
				+"Xpos='"+POut.Long(mountItem.Xpos)+"',"
				+"Ypos='"+POut.Long(mountItem.Ypos)+"',"
				+"OrdinalPos='"+POut.Long(mountItem.OrdinalPos)+"',"
				+"Width='"+POut.Long(mountItem.Width)+"',"
				+"Height='"+POut.Long(mountItem.Height)+"' "
				+"WHERE MountItemNum='"+POut.Long(mountItem.MountItemNum)+"'";
			return Db.NonQ(command);
		}

		public static void Delete(MountItem mountItem) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),mountItem);
				return;
			}
			string command="DELETE FROM mountitem WHERE MountItemNum='"+POut.Long(mountItem.MountItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Converts the given datarow to a mountitem, assuming that the row represents a mountitem.</summary>
		public static MountItem Fill(DataRow mountItemRow){
			//No need to check RemotingRole; no call to db.
			MountItem mountItem=new MountItem();
			mountItem.MountItemNum=PIn.Long(mountItemRow["MountItemNum"].ToString());
			mountItem.MountNum=PIn.Long(mountItemRow["MountNum"].ToString());
			mountItem.Xpos=PIn.Int(mountItemRow["Xpos"].ToString());
			mountItem.Ypos=PIn.Int(mountItemRow["Ypos"].ToString());
			mountItem.OrdinalPos=PIn.Int(mountItemRow["OrdinalPos"].ToString());
			mountItem.Width=PIn.Int(mountItemRow["Width"].ToString());
			mountItem.Height=PIn.Int(mountItemRow["Height"].ToString());
			return mountItem;
		}

		///<summary>Returns the list of mount items associated with the given mount key.</summary>
		public static List<MountItem> GetItemsForMount(long mountNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List <MountItem>>(MethodBase.GetCurrentMethod(),mountNum);
			}
			string command="SELECT * FROM mountitem WHERE MountNum='"+POut.Long(mountNum)+"' ORDER BY OrdinalPos";
			DataTable result=Db.GetTable(command);
			List <MountItem> mountItems=new List <MountItem> ();
			for(int i=0;i<result.Rows.Count;i++) {
				mountItems.Add(Fill(result.Rows[i]));
			}
			return mountItems;
		}

	}
}