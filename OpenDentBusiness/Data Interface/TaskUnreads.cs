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

		public static void AddUnreads(long taskNum,long curUserNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),taskNum,curUserNum);
				return;
			}
			//if the task is done, don't add unreads
			string command="SELECT TaskStatus,UserNum FROM task WHERE TaskNum = "+POut.Long(taskNum);
			DataTable table=Db.GetTable(command);
			TaskStatusEnum taskStatus=(TaskStatusEnum)PIn.Int(table.Rows[0]["TaskStatus"].ToString());
			long userNumOwner=PIn.Long(table.Rows[0]["UserNum"].ToString());
			if(taskStatus==TaskStatusEnum.Done) {
				return;
			}
			//Set it unread for the original owner of the task.
			if(userNumOwner!=curUserNum) {//but only if it's some other user
				SetUnread(userNumOwner,taskNum);
			}
			//Then, for anyone subscribed
			long userNum;
			//task subscriptions are not cached yet, so we use a query.
			//Get a list of all subscribers to this task
			command="SELECT tasksubscription.UserNum "
				+"FROM tasksubscription,taskancestor,tasklist "
				+"WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND taskancestor.TaskNum = "+POut.Long(taskNum)+" "
				+"AND tasksubscription.TaskListNum=tasklist.TaskListNum";
			table=Db.GetTable(command);//Crud.TaskSubscriptionCrud.SelectMany(
			for(int i=0;i<table.Rows.Count;i++) {
				userNum=PIn.Long(table.Rows[i]["UserNum"].ToString());
				if(userNum==userNumOwner) {
					continue;//already set
				}
				if(userNum==curUserNum) {//If the current user is subscribed to this task.
					continue;//User has obviously already read it.
				}
				SetUnread(userNum,taskNum);//This no longer results in duplicates like it used to
			}
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