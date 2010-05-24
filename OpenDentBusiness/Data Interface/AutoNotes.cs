using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness {
	public class AutoNotes {
		///<summary>A list of all Auto Notes.  Caching could be handled better for fewer refreshes.</summary>
		public static List<AutoNote> Listt;

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM autonote ORDER BY AutoNoteName";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AutoNote";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			Listt=Crud.AutoNoteCrud.TableToList(table);
		}

		///<summary></summary>
		public static long Insert(AutoNote autonote) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				autonote.AutoNoteNum=Meth.GetLong(MethodBase.GetCurrentMethod(),autonote);
				return autonote.AutoNoteNum;
			}
			return Crud.AutoNoteCrud.Insert(autonote);
		}

		///<summary></summary>
		public static void Update(AutoNote autonote) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),autonote);
				return;
			}
			Crud.AutoNoteCrud.Update(autonote);
		}

		///<summary></summary>
		public static void Delete(long autoNoteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),autoNoteNum);
				return;
			}
			string command="DELETE FROM autonote "
				+"WHERE AutoNoteNum = "+POut.Long(autoNoteNum);
			Db.NonQ(command);
		}

	
	}
}
