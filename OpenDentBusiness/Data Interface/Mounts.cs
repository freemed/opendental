using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;
using System.Data;

namespace OpenDentBusiness {
	public class Mounts {

		public static int Insert(Mount mount){
			string command="INSERT INTO mount (MountNum,PatNum,DocCategory,DateCreated,Description,Note,ImgType,Width,Height) VALUES ("
				+"'"+POut.PInt(mount.MountNum)+"',"
				+"'"+POut.PInt(mount.PatNum)+"',"
				+"'"+POut.PInt(mount.DocCategory)+"',"
				+POut.PDate(mount.DateCreated)+","
				+"'"+POut.PString(mount.Description)+"',"
				+"'"+POut.PString(mount.Note)+"',"
				+"'"+POut.PInt((int)mount.ImgType)+"',"
				+"'"+POut.PInt(mount.Width)+"',"
				+"'"+POut.PInt(mount.Height)+"')";
			return General2.NonQEx(command,true);
		}

		public static int Update(Mount mount){
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
			return General2.NonQEx(command);
		}

		public static void Delete(Mount mount){
			string command="DELETE FROM mount WHERE MountNum='"+POut.PInt(mount.MountNum)+"'";
			General2.NonQEx(command);
		}

		///<summary>Converts the given datarow into a mount object.</summary>
		public static Mount Fill(DataRow mountRow){
			Mount mount=new Mount();
			mount.MountNum=PIn.PInt(mountRow["MountNum"].ToString());
			mount.PatNum=PIn.PInt(mountRow["PatNum"].ToString());
			mount.DocCategory=PIn.PInt(mountRow["DocCategory"].ToString());
			mount.DateCreated=PIn.PDate(mountRow["DateCreated"].ToString());
			mount.Description=PIn.PString(mountRow["Description"].ToString());
			mount.Note=PIn.PString(mountRow["Note"].ToString());
			mount.ImgType=(ImageType)PIn.PInt(mountRow["ImgType"].ToString());
			mount.Width=PIn.PInt(mountRow["Width"].ToString());
			mount.Height=PIn.PInt(mountRow["Height"].ToString());
			return mount;
		}

		///<summary>Returns a single mount object corresponding to the given mount number key.</summary>
		public static Mount GetByNum(int mountNum){
			string command="SELECT * FROM mount WHERE MountNum='"+mountNum+"'";
			DataTable table=General2.GetTable(command);
			if(table.Rows.Count<0){
				return new Mount();
			}
			return Fill(table.Rows[0]);
		}

	}
}