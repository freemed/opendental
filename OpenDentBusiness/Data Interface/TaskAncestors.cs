using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TaskAncestors {
	
		///<summary></summary>
		public static void WriteObject(TaskAncestor ancestor){
			DataObjectFactory<TaskAncestor>.WriteObject(ancestor);
		}

		/*
		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(TaskAncestor subsc){
			DataObjectFactory<TaskAncestor>.DeleteObject(subsc);
		}*/

		public static void Synch(Task task){
			string command="DELETE FROM taskancestor WHERE TaskNum="+POut.PInt(task.TaskNum);
			Db.NonQ(command);
			int taskListNum=0;
			int parentNum=task.TaskListNum;
			DataTable table;
			TaskAncestor ancestor;
			while(true){
				if(parentNum==0){
					break;//no parent to mark
				}
				//get the parent
				command="SELECT TaskListNum,Parent FROM tasklist WHERE TaskListNum="+POut.PInt(parentNum);
				table=Db.GetTable(command);
				if(table.Rows.Count==0){//in case of database inconsistency
					break;
				}
				taskListNum=PIn.PInt(table.Rows[0]["TaskListNum"].ToString());
				parentNum=PIn.PInt(table.Rows[0]["Parent"].ToString());
				ancestor=new TaskAncestor();
				ancestor.TaskNum=task.TaskNum;
				ancestor.TaskListNum=taskListNum;
				WriteObject(ancestor);
			}
		}
		
		///<summary>Only run once after the upgrade to version 5.5.</summary>
		public static void SynchAll(){
			List<Task> listTasks=Tasks.RefreshAll();
			for(int i=0;i<listTasks.Count;i++){
				Synch(listTasks[i]);
			}
		}
		


	}

	


	


}









