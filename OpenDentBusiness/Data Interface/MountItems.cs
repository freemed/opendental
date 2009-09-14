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
				mountItem.MountItemNum=Meth.GetInt(MethodBase.GetCurrentMethod(),mountItem);
				return mountItem.MountItemNum;
			}
			if(PrefC.RandomKeys) {
				mountItem.MountItemNum=ReplicationServers.GetKey("mountitem","MountItemNum");
			}
			string command="INSERT INTO mountitem (";
			if(PrefC.RandomKeys) {
				command+="MountItemNum,";
			}
			command+="MountItemNum,MountNum,Xpos,Ypos,OrdinalPos,Width,Height) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PLong(mountItem.MountItemNum)+", ";
			}
			command+=
				 "'"+POut.PLong(mountItem.MountItemNum)+"',"
				+"'"+POut.PLong(mountItem.MountNum)+"',"
				+"'"+POut.PLong(mountItem.Xpos)+"',"
				+"'"+POut.PLong(mountItem.Ypos)+"',"
				+"'"+POut.PLong(mountItem.OrdinalPos)+"',"
				+"'"+POut.PLong(mountItem.Width)+"',"
				+"'"+POut.PLong(mountItem.Height)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else{
				mountItem.MountItemNum=Db.NonQ(command,true);
			}
			return mountItem.MountItemNum;
		}

		public static long Update(MountItem mountItem) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),mountItem);
			}
			string command="UPDATE mountitem SET "
				+"MountNum='"+POut.PLong(mountItem.MountNum)+"',"
				+"Xpos='"+POut.PLong(mountItem.Xpos)+"',"
				+"Ypos='"+POut.PLong(mountItem.Ypos)+"',"
				+"OrdinalPos='"+POut.PLong(mountItem.OrdinalPos)+"',"
				+"Width='"+POut.PLong(mountItem.Width)+"',"
				+"Height='"+POut.PLong(mountItem.Height)+"' "
				+"WHERE MountItemNum='"+POut.PLong(mountItem.MountItemNum)+"'";
			return Db.NonQ(command);
		}

		public static void Delete(MountItem mountItem) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),mountItem);
				return;
			}
			string command="DELETE FROM mountitem WHERE MountItemNum='"+POut.PLong(mountItem.MountItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Converts the given datarow to a mountitem, assuming that the row represents a mountitem.</summary>
		public static MountItem Fill(DataRow mountItemRow){
			//No need to check RemotingRole; no call to db.
			MountItem mountItem=new MountItem();
			mountItem.MountItemNum=PIn.PLong(mountItemRow["MountItemNum"].ToString());
			mountItem.MountNum=PIn.PLong(mountItemRow["MountNum"].ToString());
			mountItem.Xpos=PIn.PInt(mountItemRow["Xpos"].ToString());
			mountItem.Ypos=PIn.PInt(mountItemRow["Ypos"].ToString());
			mountItem.OrdinalPos=PIn.PInt(mountItemRow["OrdinalPos"].ToString());
			mountItem.Width=PIn.PInt(mountItemRow["Width"].ToString());
			mountItem.Height=PIn.PInt(mountItemRow["Height"].ToString());
			return mountItem;
		}

		///<summary>Returns the list of mount items associated with the given mount key.</summary>
		public static List<MountItem> GetItemsForMount(long mountNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List <MountItem>>(MethodBase.GetCurrentMethod(),mountNum);
			}
			string command="SELECT * FROM mountitem WHERE MountNum='"+POut.PLong(mountNum)+"' ORDER BY OrdinalPos";
			DataTable result=Db.GetTable(command);
			List <MountItem> mountItems=new List <MountItem> ();
			for(int i=0;i<result.Rows.Count;i++) {
				mountItems.Add(Fill(result.Rows[i]));
			}
			return mountItems;
		}

	}
}