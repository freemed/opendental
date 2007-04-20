using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;
using System.Data;

namespace OpenDental {
	class Mounts {

		public static int Insert(Mount mount){
			string command="INSERT INTO mount (MountNum,PatNum,DocCategory,DateCreated,Description,ImgType) VALUES ("
				+"'"+POut.PInt(mount.MountNum)+"',"
				+"'"+POut.PInt(mount.PatNum)+"',"
				+"'"+POut.PInt(mount.DocCategory)+"',"
				+"'"+POut.PDate(mount.DateCreated)+"',"
				+"'"+POut.PString(mount.Description)+"',"
				+"'"+POut.PInt((int)mount.ImgType)+"')";
			return General.NonQEx(command);
		}

		public static int Update(Mount mount){
			string command="UPDATE mount SET "
				+"PatNum='"+POut.PInt(mount.PatNum)+"',"
				+"DocCategory='"+POut.PInt(mount.DocCategory)+"',"
				+"DateCreated='"+POut.PDate(mount.DateCreated)+"',"
				+"Description='"+POut.PString(mount.Description)+"'"
				+"ImgType='"+POut.PInt((int)mount.ImgType)+"' "
				+"WHERE MountNum='"+POut.PInt(mount.MountNum)+"'";
			return General.NonQEx(command);
		}

		public static void Delete(Mount mount){
			string command="DELETE FROM mount WHERE MountNum='"+POut.PInt(mount.MountNum)+"'";
			General.NonQEx(command);
		}

		///<summary>Converts the given datarow into a mount object.</summary>
		public static Mount Fill(DataRow mountRow){
			Mount mount=new Mount();
			mount.MountNum=PIn.PInt(mountRow["MountNum"].ToString());
			mount.PatNum=PIn.PInt(mountRow["PatNum"].ToString());
			mount.DocCategory=PIn.PInt(mountRow["DocCategory"].ToString());
			mount.DateCreated=PIn.PDate(mountRow["DateCreated"].ToString());
			mount.Description=PIn.PString(mountRow["Description"].ToString());
			mount.ImgType=(ImageType)PIn.PInt(mountRow["ImgType"].ToString());
			return mount;
		}

		///<summary>Returns a list of all the current mount objects for the given patient key.</summary>
		public static Mount[] Refresh(int patNum){
			string command="SELECT * FROM mount WHERE PatNum='"+patNum+"'";
			DataTable result=General.GetTable(command);
			Mount[] mounts=new Mount[result.Rows.Count];
			for(int i=0;i<mounts.Length;i++){
				mounts[i]=Fill(result.Rows[i]);
			}
			return mounts;
		}

	}
}