using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TaskNotes{
		///<summary></summary>
		public static List<TaskNote> GetForTask(long taskNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TaskNote>>(MethodBase.GetCurrentMethod(),taskNum);
			}
			string command="SELECT * FROM tasknote WHERE TaskNum = "+POut.Long(taskNum)+" ORDER BY DateTimeNote";
			return Crud.TaskNoteCrud.SelectMany(command);
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

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary>Gets one TaskNote from the db.</summary>
		public static TaskNote GetOne(long taskNoteNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<TaskNote>(MethodBase.GetCurrentMethod(),taskNoteNum);
			}
			return Crud.TaskNoteCrud.SelectOne(taskNoteNum);
		}

		

	
		*/



	}
}