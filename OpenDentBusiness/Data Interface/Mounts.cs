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
				mount.MountNum=Meth.GetLong(MethodBase.GetCurrentMethod(),mount);
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
				command+=POut.Long(mount.MountNum)+", ";
			}
			command+=
				 "'"+POut.Long(mount.MountNum)+"',"
				+"'"+POut.Long(mount.PatNum)+"',"
				+"'"+POut.Long(mount.DocCategory)+"',"
				+POut.Date(mount.DateCreated)+","
				+"'"+POut.String(mount.Description)+"',"
				+"'"+POut.String(mount.Note)+"',"
				+"'"+POut.Long((int)mount.ImgType)+"',"
				+"'"+POut.Long(mount.Width)+"',"
				+"'"+POut.Long(mount.Height)+"')";
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
				return Meth.GetLong(MethodBase.GetCurrentMethod(),mount);
			}
			string command="UPDATE mount SET "
				+"PatNum='"+POut.Long(mount.PatNum)+"',"
				+"DocCategory='"+POut.Long(mount.DocCategory)+"',"
				+"DateCreated="+POut.Date(mount.DateCreated)+","
				+"Description='"+POut.String(mount.Description)+"',"
				+"Note='"+POut.String(mount.Note)+"',"
				+"ImgType='"+POut.Long((int)mount.ImgType)+"',"
				+"Width='"+POut.Long(mount.Width)+"',"
				+"Height='"+POut.Long(mount.Height)+"' "
				+"WHERE MountNum='"+POut.Long(mount.MountNum)+"'";
			return Db.NonQ(command);
		}

		public static void Delete(Mount mount){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),mount);
				return;
			}
			string command="DELETE FROM mount WHERE MountNum='"+POut.Long(mount.MountNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Converts the given datarow into a mount object.</summary>
		public static Mount Fill(DataRow mountRow){
			//No need to check RemotingRole; no call to db.
			Mount mount=new Mount();
			mount.MountNum=PIn.Long(mountRow["MountNum"].ToString());
			mount.PatNum=PIn.Long(mountRow["PatNum"].ToString());
			mount.DocCategory=PIn.Long(mountRow["DocCategory"].ToString());
			mount.DateCreated=PIn.Date(mountRow["DateCreated"].ToString());
			mount.Description=PIn.String(mountRow["Description"].ToString());
			mount.Note=PIn.String(mountRow["Note"].ToString());
			mount.ImgType=(ImageType)PIn.Long(mountRow["ImgType"].ToString());
			mount.Width=PIn.Int(mountRow["Width"].ToString());
			mount.Height=PIn.Int(mountRow["Height"].ToString());
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