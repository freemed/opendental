using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TaskNotes{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all TaskNotes.</summary>
		private static List<TaskNote> listt;

		///<summary>A list of all TaskNotes.</summary>
		public static List<TaskNote> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM tasknote ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="TaskNote";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.TaskNoteCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<TaskNote> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TaskNote>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM tasknote WHERE PatNum = "+POut.Long(patNum);
			return Crud.TaskNoteCrud.SelectMany(command);
		}

		///<summary>Gets one TaskNote from the db.</summary>
		public static TaskNote GetOne(long taskNoteNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<TaskNote>(MethodBase.GetCurrentMethod(),taskNoteNum);
			}
			return Crud.TaskNoteCrud.SelectOne(taskNoteNum);
		}

		///<summary></summary>
		public static long Insert(TaskNote taskNote){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				taskNote.TaskNoteNum=Meth.GetLong(MethodBase.GetCurrentMethod(),taskNote);
				return taskNote.TaskNoteNum;
			}
			return Crud.TaskNoteCrud.Insert(taskNote);
		}

		///<summary></summary>
		public static void Update(TaskNote taskNote){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),taskNote);
				return;
			}
			Crud.TaskNoteCrud.Update(taskNote);
		}

		///<summary></summary>
		public static void Delete(long taskNoteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),taskNoteNum);
				return;
			}
			string command= "DELETE FROM tasknote WHERE TaskNoteNum = "+POut.Long(taskNoteNum);
			Db.NonQ(command);
		}
		*/



	}
}