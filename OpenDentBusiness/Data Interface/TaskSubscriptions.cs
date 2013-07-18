using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TaskSubscriptions {
	
		///<summary></summary>
		public static long Insert(TaskSubscription subsc){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				subsc.TaskSubscriptionNum=Meth.GetLong(MethodBase.GetCurrentMethod(),subsc);
				return subsc.TaskSubscriptionNum;
			}
			return Crud.TaskSubscriptionCrud.Insert(subsc);
		}

		/*
		///<summary></summary>
		public static void Update(TaskSubscription subsc) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),subsc);
				return;
			}
			Crud.TaskSubscriptionCrud.Update(subsc);
		}*/

		///<summary>Creates a subscription to a list.</summary>
		public static void SubscList(long taskListNum,long userNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),taskListNum,userNum);
				return;
			}
			string command="SELECT COUNT(*) FROM tasksubscription "
				+"WHERE UserNum="+POut.Long(userNum)
				+" AND TaskListNum="+POut.Long(taskListNum);
			if(Db.GetCount(command)!="0"){
				throw new ApplicationException(Lans.g("TaskSubscriptions","User already subscribed."));
			}
			TaskSubscription subsc=new TaskSubscription();
			subsc.IsNew=true;
			subsc.UserNum=userNum;
			subsc.TaskListNum=taskListNum;
			Insert(subsc);
		}

		///<summary>Removes a subscription to a list.</summary>
		public static void UnsubscList(long taskListNum,long userNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),taskListNum,userNum);
				return;
			}
			string command="DELETE FROM tasksubscription "
				+"WHERE UserNum="+POut.Long(userNum)
				+" AND TaskListNum="+POut.Long(taskListNum);
			Db.NonQ(command);
		}

		///<summary>Moves all subscriptions from taskListOld to taskListNew. Used when cutting and pasting a tasklist. Can also be used when deleting a tasklist to remove all subscriptions from the tasklist by sending in 0 as taskListNumNew.</summary>
		public static void UpdateAllSubs(long taskListNumOld,long taskListNumNew) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),taskListNumOld,taskListNumNew);
				return;
			}
			string command="";
			if(taskListNumNew==0) {
				command="DELETE FROM tasksubscription WHERE TaskListNum="+POut.Long(taskListNumOld);
			}
			else {
				command="UPDATE tasksubscription SET TaskListNum="+POut.Long(taskListNumNew)+" WHERE TaskListNum="+POut.Long(taskListNumOld);
			}
			Db.NonQ(command);
		}
		
		


	}

	


	


}









