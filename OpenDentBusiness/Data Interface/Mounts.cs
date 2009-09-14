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
				command+=POut.PLong(mount.MountNum)+", ";
			}
			command+=
				 "'"+POut.PLong(mount.MountNum)+"',"
				+"'"+POut.PLong(mount.PatNum)+"',"
				+"'"+POut.PLong(mount.DocCategory)+"',"
				+POut.PDate(mount.DateCreated)+","
				+"'"+POut.PString(mount.Description)+"',"
				+"'"+POut.PString(mount.Note)+"',"
				+"'"+POut.PLong((int)mount.ImgType)+"',"
				+"'"+POut.PLong(mount.Width)+"',"
				+"'"+POut.PLong(mount.Height)+"')";
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
				+"PatNum='"+POut.PLong(mount.PatNum)+"',"
				+"DocCategory='"+POut.PLong(mount.DocCategory)+"',"
				+"DateCreated="+POut.PDate(mount.DateCreated)+","
				+"Description='"+POut.PString(mount.Description)+"',"
				+"Note='"+POut.PString(mount.Note)+"',"
				+"ImgType='"+POut.PLong((int)mount.ImgType)+"',"
				+"Width='"+POut.PLong(mount.Width)+"',"
				+"Height='"+POut.PLong(mount.Height)+"' "
				+"WHERE MountNum='"+POut.PLong(mount.MountNum)+"'";
			return Db.NonQ(command);
		}

		public static void Delete(Mount mount){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),mount);
				return;
			}
			string command="DELETE FROM mount WHERE MountNum='"+POut.PLong(mount.MountNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Converts the given datarow into a mount object.</summary>
		public static Mount Fill(DataRow mountRow){
			//No need to check RemotingRole; no call to db.
			Mount mount=new Mount();
			mount.MountNum=PIn.PLong(mountRow["MountNum"].ToString());
			mount.PatNum=PIn.PLong(mountRow["PatNum"].ToString());
			mount.DocCategory=PIn.PLong(mountRow["DocCategory"].ToString());
			mount.DateCreated=PIn.PDate(mountRow["DateCreated"].ToString());
			mount.Description=PIn.PString(mountRow["Description"].ToString());
			mount.Note=PIn.PString(mountRow["Note"].ToString());
			mount.ImgType=(ImageType)PIn.PLong(mountRow["ImgType"].ToString());
			mount.Width=PIn.PInt(mountRow["Width"].ToString());
			mount.Height=PIn.PInt(mountRow["Height"].ToString());
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