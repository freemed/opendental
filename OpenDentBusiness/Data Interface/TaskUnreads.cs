using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TaskUnreads{
		/*
		///<summary></summary>
		public static List<TaskUnread> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TaskUnread>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM taskunread WHERE PatNum = "+POut.Long(patNum);
			return Crud.TaskUnreadCrud.SelectMany(command);
		}

		///<summary>Gets one TaskUnread from the db.</summary>
		public static TaskUnread GetOne(long taskUnreadNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<TaskUnread>(MethodBase.GetCurrentMethod(),taskUnreadNum);
			}
			return Crud.TaskUnreadCrud.SelectOne(taskUnreadNum);
		}

		///<summary></summary>
		public static long Insert(TaskUnread taskUnread){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				taskUnread.TaskUnreadNum=Meth.GetLong(MethodBase.GetCurrentMethod(),taskUnread);
				return taskUnread.TaskUnreadNum;
			}
			return Crud.TaskUnreadCrud.Insert(taskUnread);
		}

		///<summary></summary>
		public static void Update(TaskUnread taskUnread){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),taskUnread);
				return;
			}
			Crud.TaskUnreadCrud.Update(taskUnread);
		}*/




	}
}