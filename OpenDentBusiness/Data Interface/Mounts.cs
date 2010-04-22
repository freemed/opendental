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
			return Crud.MountCrud.Insert(mount);
		}

		public static void Update(Mount mount){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),mount);
				return;
			}
			Crud.MountCrud.Update(mount);
		}

		public static void Delete(Mount mount){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),mount);
				return;
			}
			string command="DELETE FROM mount WHERE MountNum='"+POut.Long(mount.MountNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Returns a single mount object corresponding to the given mount number key.</summary>
		public static Mount GetByNum(long mountNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Mount>(MethodBase.GetCurrentMethod(),mountNum);
			}
			Mount mount= Crud.MountCrud.SelectOne(mountNum);
			if(mount==null){
				return new Mount();
			}
			return mount;
		}

	}
}