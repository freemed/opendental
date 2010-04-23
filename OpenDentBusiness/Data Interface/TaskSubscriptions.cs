using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TaskSubscriptions {
	
		///<summary></summary>
		public static long WriteObject(TaskSubscription subsc){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				subsc.TaskSubscriptionNum=Meth.GetLong(MethodBase.GetCurrentMethod(),subsc);
				return subsc.TaskSubscriptionNum;
			}
			if(subsc.IsNew){
				return Crud.TaskSubscriptionCrud.Insert(subsc);
			}
			else{
				Crud.TaskSubscriptionCrud.Update(subsc);
				return subsc.TaskSubscriptionNum;
			}
		}

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
			subsc.UserNum=userNum;
			subsc.TaskListNum=taskListNum;
			WriteObject(subsc);
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


		
		


	}

	


	


}









