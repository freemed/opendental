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

		public static void SetRead(long userNum,long taskNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),userNum,taskNum);
				return;
			}
			string command="DELETE FROM taskunread WHERE userNum = "+POut.Long(userNum)+" "
				+"AND taskNum = "+POut.Long(taskNum);
			Db.NonQ(command);
		}

		public static void AddUnreads(long taskNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),taskNum);
				return;
			}
			TaskUnread taskUnread;
			//task subscriptions are not cached yet, so we use a query.
			//Get a list of all subscribers to this task
			string command="SELECT tasksubscription.UserNum FROM tasksubscription,taskancestor,tasklist "
				+"WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND taskancestor.TaskNum = "+POut.Long(taskNum)+" "
				+"AND tasksubscription.TaskListNum=tasklist.TaskListNum";
			DataTable table=Db.GetTable(command);//Crud.TaskSubscriptionCrud.SelectMany(
			for(int i=0;i<table.Rows.Count;i++) {
				taskUnread=new TaskUnread();
				taskUnread.TaskNum=taskNum;
				taskUnread.UserNum=PIn.Long(table.Rows[i]["UserNum"].ToString());
				Insert(taskUnread);//Yes, this will frequently create duplicates in this table, but the query to show tasks in the New tab handles the duplicates just fine.
			}
		}


	}
}