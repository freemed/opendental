using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TaskUnreads{

		///<summary></summary>
		public static long Insert(TaskUnread taskUnread){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				taskUnread.TaskUnreadNum=Meth.GetLong(MethodBase.GetCurrentMethod(),taskUnread);
				return taskUnread.TaskUnreadNum;
			}
			return Crud.TaskUnreadCrud.Insert(taskUnread);
		}

		///<summary>Sets a task read by a user by deleting all the matching taskunreads.  Quick and efficient to run any time.</summary>
		public static void SetRead(long userNum,long taskNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),userNum,taskNum);
				return;
			}
			string command="DELETE FROM taskunread WHERE UserNum = "+POut.Long(userNum)+" "
				+"AND TaskNum = "+POut.Long(taskNum);
			Db.NonQ(command);
		}

		public static void AddUnreads(long taskNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),taskNum);
				return;
			}
			//if the task is done, don't add unreads
			string command="SELECT TaskStatus FROM task WHERE TaskNum = "+POut.Long(taskNum);
			TaskStatusEnum taskStatus=(TaskStatusEnum)PIn.Int(Db.GetScalar(command));
			if(taskStatus==TaskStatusEnum.Done) {
				return;
			}
			//task subscriptions are not cached yet, so we use a query.
			//Get a list of all subscribers to this task
			command="SELECT tasksubscription.UserNum "
				+"FROM tasksubscription,taskancestor,tasklist "
				+"WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND taskancestor.TaskNum = "+POut.Long(taskNum)+" "
				+"AND tasksubscription.TaskListNum=tasklist.TaskListNum";
			DataTable table=Db.GetTable(command);//Crud.TaskSubscriptionCrud.SelectMany(
			long userNum;
			for(int i=0;i<table.Rows.Count;i++) {
				userNum=PIn.Long(table.Rows[i]["UserNum"].ToString());
				SetUnread(userNum,taskNum);//This no longer results in duplicates like it used to
			}
			//Now, we also want to set it unread for the originator of the task if they have an inbox:

		}

		public static bool IsUnread(long userNum,long taskNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),userNum,taskNum);
			}
			string command="SELECT COUNT(*) FROM taskunread WHERE UserNum = "+POut.Long(userNum)+" "
				+"AND TaskNum = "+POut.Long(taskNum);
			if(Db.GetCount(command)=="0") {
				return false;
			}
			return true;
		}

		///<summary>Sets unread for a single user.  Works well without duplicates, whether it's already set to Unread(new) or not.</summary>
		public static void SetUnread(long userNum,long taskNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),userNum,taskNum);
				return;
			}
			if(IsUnread(userNum,taskNum)) {
				return;//Already set to unread, so nothing else to do
			}
			TaskUnread taskUnread=new TaskUnread();
			taskUnread.TaskNum=taskNum;
			taskUnread.UserNum=userNum;
			Insert(taskUnread);
		}


	}
}