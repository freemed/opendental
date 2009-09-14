using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using OpenDentBusiness;

namespace OpenDentBusiness {
	public class Mounts {

		public static long Insert(Mount mount){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				mount.MountNum=Meth.GetInt(MethodBase.GetCurrentMethod(),mount);
				return mount.MountNum;
			}
			if(PrefC.RandomKeys) {
				mount.MountNum=ReplicationServers.GetKey("mount","MountNum");
			}
			string command="INSERT INTO mount (";
			if(PrefC.RandomKeys) {
				command+="MountNum,";
			}
			command+="MountNum,PatNum,DocCategory,DateCreated,Description,Note,ImgType,Width,Height) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PInt(mount.MountNum)+", ";
			}
			command+=
				 "'"+POut.PInt(mount.MountNum)+"',"
				+"'"+POut.PInt(mount.PatNum)+"',"
				+"'"+POut.PInt(mount.DocCategory)+"',"
				+POut.PDate(mount.DateCreated)+","
				+"'"+POut.PString(mount.Description)+"',"
				+"'"+POut.PString(mount.Note)+"',"
				+"'"+POut.PInt((int)mount.ImgType)+"',"
				+"'"+POut.PInt(mount.Width)+"',"
				+"'"+POut.PInt(mount.Height)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else{
				mount.MountNum=Db.NonQ(command,true);
			}
			return mount.MountNum;
		}

		public static long Update(Mount mount){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),mount);
			}
			string command="UPDATE mount SET "
				+"PatNum='"+POut.PInt(mount.PatNum)+"',"
				+"DocCategory='"+POut.PInt(mount.DocCategory)+"',"
				+"DateCreated="+POut.PDate(mount.DateCreated)+","
				+"Description='"+POut.PString(mount.Description)+"',"
				+"Note='"+POut.PString(mount.Note)+"',"
				+"ImgType='"+POut.PInt((int)mount.ImgType)+"',"
				+"Width='"+POut.PInt(mount.Width)+"',"
				+"Height='"+POut.PInt(mount.Height)+"' "
				+"WHERE MountNum='"+POut.PInt(mount.MountNum)+"'";
			return Db.NonQ(command);
		}

		public static void Delete(Mount mount){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),mount);
				return;
			}
			string command="DELETE FROM mount WHERE MountNum='"+POut.PInt(mount.MountNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Converts the given datarow into a mount object.</summary>
		public static Mount Fill(DataRow mountRow){
			//No need to check RemotingRole; no call to db.
			Mount mount=new Mount();
			mount.MountNum=PIn.PInt(mountRow["MountNum"].ToString());
			mount.PatNum=PIn.PInt(mountRow["PatNum"].ToString());
			mount.DocCategory=PIn.PInt(mountRow["DocCategory"].ToString());
			mount.DateCreated=PIn.PDate(mountRow["DateCreated"].ToString());
			mount.Description=PIn.PString(mountRow["Description"].ToString());
			mount.Note=PIn.PString(mountRow["Note"].ToString());
			mount.ImgType=(ImageType)PIn.PInt(mountRow["ImgType"].ToString());
			mount.Width=PIn.PInt32(mountRow["Width"].ToString());
			mount.Height=PIn.PInt32(mountRow["Height"].ToString());
			return mount;
		}

		///<summary>Returns a single mount object corresponding to the given mount number key.</summary>
		public static Mount GetByNum(long mountNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Mount>(MethodBase.GetCurrentMethod(),mountNum);
			}
			string command="SELECT * FROM mount WHERE MountNum='"+mountNum+"'";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count<0){
				return new Mount();
			}
			return Fill(table.Rows[0]);
		}

	}
}