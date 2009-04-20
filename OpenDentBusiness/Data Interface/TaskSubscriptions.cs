using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TaskSubscriptions {

		/*
		///<summary>Gets all TaskSubscriptions for one user.</summary>
		public static List<TaskSubscription> CreateObjects(int userNum) {
			string command="SELECT * FROM tasksubscription "
				+"WHERE UserNum="+POut.PInt(userNum)
				+" ORDER BY ";
			return new List<TaskSubscription>(DataObjectFactory<TaskSubscription>.CreateObjects(command));
		}*/
	
		///<summary></summary>
		public static void WriteObject(TaskSubscription subsc){
			DataObjectFactory<TaskSubscription>.WriteObject(subsc);
		}

		/*
		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(TaskSubscription subsc){
			DataObjectFactory<TaskSubscription>.DeleteObject(subsc);
		}*/

		///<summary>Creates a subscription to a list.</summary>
		public static void SubscList(int taskListNum,int userNum){
			string command="SELECT COUNT(*) FROM tasksubscription "
				+"WHERE UserNum="+POut.PInt(userNum)
				+" AND TaskListNum="+POut.PInt(taskListNum);
			if(Db.GetCount(command)!="0"){
				throw new ApplicationException(Lan.g("TaskSubscriptions","User already subscribed."));
			}
			TaskSubscription subsc=new TaskSubscription();
			subsc.UserNum=userNum;
			subsc.TaskListNum=taskListNum;
			WriteObject(subsc);
		}

		/*
		///<summary>Creates a subscription to a task.</summary>
		public static void SubscTask(int taskNum,int userNum) {
			string command="SELECT COUNT(*) FROM tasksubscription "
				+"WHERE UserNum="+POut.PInt(userNum)
				+" AND TaskNum="+POut.PInt(taskNum);
			if(Db.GetCount(command)!="0") {
				throw new ApplicationException(Lan.g("TaskSubscriptions","User already subscribed."));
			}
			TaskSubscription subsc=new TaskSubscription();
			subsc.UserNum=userNum;
			subsc.TaskNum=taskNum;
			WriteObject(subsc);
		}*/

		///<summary>Removes a subscription to a list.</summary>
		public static void UnsubscList(int taskListNum,int userNum) {
			string command="DELETE FROM tasksubscription "
				+"WHERE UserNum="+POut.PInt(userNum)
				+" AND TaskListNum="+POut.PInt(taskListNum);
			Db.NonQ(command);
		}

		/*
		///<summary>Removes a subscription to a task.</summary>
		public static void UnsubscTask(int taskNum,int userNum) {
			string command="DELETE FROM tasksubscription "
				+"WHERE UserNum="+POut.PInt(userNum)
				+" AND TaskNum="+POut.PInt(taskNum);
			Db.NonQ(command);
		}*/
		
		
		


	}

	


	


}









