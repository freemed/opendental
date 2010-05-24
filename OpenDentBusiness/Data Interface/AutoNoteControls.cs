using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness {
	public class AutoNoteControls {
		/// <summary>A list of all the Prompts.  Caching could be handled better for fewer refreshes.</summary>
		public static List<AutoNoteControl> Listt;

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM autonotecontrol ORDER BY Descript";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AutoNoteControl";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			Listt=Crud.AutoNoteControlCrud.TableToList(table);
		}

		public static long Insert(AutoNoteControl autoNoteControl) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				autoNoteControl.AutoNoteControlNum=Meth.GetLong(MethodBase.GetCurrentMethod(),autoNoteControl);
				return autoNoteControl.AutoNoteControlNum;
			}
			return Crud.AutoNoteControlCrud.Insert(autoNoteControl);
		}


		public static void Update(AutoNoteControl autoNoteControl) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),autoNoteControl);
				return;
			}
			Crud.AutoNoteControlCrud.Update(autoNoteControl);
		}

		public static void Delete(long autoNoteControlNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),autoNoteControlNum);
				return;
			}
			//no validation for now.
			string command="DELETE FROM autonotecontrol WHERE AutoNoteControlNum="+POut.Long(autoNoteControlNum);
			Db.NonQ(command);
		}

		///<summary>Will return null if can't match.</summary>
		public static AutoNoteControl GetByDescript(string descript) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].Descript==descript) {
					return Listt[i];
				}
			}
			return null;
		}

	}
}

