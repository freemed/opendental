using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary>A subscription of one user to either a tasklist or to a task.</summary>
	[DataObject("tasksubscription")]
	public class TaskSubscription : DataObjectBase {
		[DataField("TaskSubscriptionNum", PrimaryKey=true, AutoNumber=true)]
		private long taskSubscriptionNum;
		bool taskSubscriptionNumChanged;
		/// <summary>Primary key.</summary>
		public long TaskSubscriptionNum {
			get { return taskSubscriptionNum; }
			set { if(taskSubscriptionNum!=value){taskSubscriptionNum = value; MarkDirty(); taskSubscriptionNumChanged = true; }}
		}
		public bool TaskSubscriptionNumChanged {
			get { return taskSubscriptionNumChanged; }
		}

		[DataField("UserNum")]
		private long userNum;
		bool userNumChanged;
		/// <summary>FK to userod.UserNum</summary>
		public long UserNum {
			get { return userNum; }
			set { if(userNum!=value){userNum = value; MarkDirty(); userNumChanged = true;} }
		}
		public bool UserNumChanged {
			get { return userNumChanged; }
		}

		[DataField("TaskListNum")]
		private long taskListNum;
		bool taskListNumChanged;
		/// <summary>FK to tasklist.TaskListNum</summary>
		public long TaskListNum {
			get { return taskListNum; }
			set { if(taskListNum!=value) { taskListNum = value; MarkDirty(); taskListNumChanged = true; } }
		}
		public bool TaskListNumChanged {
			get { return taskListNumChanged; }
		}

		

		
		

			
	}

	

}









