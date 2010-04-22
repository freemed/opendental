using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class MountDefs {
		
		///<summary>Gets a list of all MountDefs when program first opens.  Also refreshes MountItemDefs and attaches all items to the appropriate mounts.</summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			//MountItemDefs.Refresh();
			string command="SELECT * FROM mountdef ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="MountDef";
			FillCache(table);
			return table;
		}	

		private static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			MountDefC.Listt=Crud.MountDefCrud.TableToList(table);
		}

		///<summary></summary>
		public static void Update(MountDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			Crud.MountDefCrud.Update(def);
		}

		///<summary></summary>
		public static long Insert(MountDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				def.MountDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),def);
				return def.MountDefNum;
			}
			return Crud.MountDefCrud.Insert(def);
		}

		///<summary>No need to surround with try/catch, because all deletions are allowed.</summary>
		public static void Delete(long mountDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),mountDefNum);
				return;
			}
			string command="DELETE FROM mountdef WHERE MountDefNum="+POut.Long(mountDefNum);
			Db.NonQ(command);
			command="DELETE FROM mountitemdef WHERE MountDefNum ="+POut.Long(mountDefNum);
			Db.NonQ(command);
		}

		
		
		
	}

		



		
	

	

	


}










