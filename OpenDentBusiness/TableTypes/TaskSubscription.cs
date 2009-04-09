using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary>A subscription of one user to either a tasklist or to a task.</summary>
	[DataObject("tasksubscription")]
	public class TaskSubscription : DataObjectBase {
		[DataField("TaskSubscriptionNum", PrimaryKey=true, AutoNumber=true)]
		private int taskSubscriptionNum;
		bool taskSubscriptionNumChanged;
		/// <summary>Primary key.</summary>
		public int TaskSubscriptionNum {
			get { return taskSubscriptionNum; }
			set { if(taskSubscriptionNum!=value){taskSubscriptionNum = value; MarkDirty(); taskSubscriptionNumChanged = true; }}
		}
		public bool TaskSubscriptionNumChanged {
			get { return taskSubscriptionNumChanged; }
		}

		[DataField("UserNum")]
		private int userNum;
		bool userNumChanged;
		/// <summary>FK to userod.UserNum</summary>
		public int UserNum {
			get { return userNum; }
			set { if(userNum!=value){userNum = value; MarkDirty(); userNumChanged = true;} }
		}
		public bool UserNumChanged {
			get { return userNumChanged; }
		}

		[DataField("TaskListNum")]
		private int taskListNum;
		bool taskListNumChanged;
		/// <summary>FK to tasklist.TaskListNum</summary>
		public int TaskListNum {
			get { return taskListNum; }
			set { if(taskListNum!=value) { taskListNum = value; MarkDirty(); taskListNumChanged = true; } }
		}
		public bool TaskListNumChanged {
			get { return taskListNumChanged; }
		}

		

		
		

			
	}

	

}









