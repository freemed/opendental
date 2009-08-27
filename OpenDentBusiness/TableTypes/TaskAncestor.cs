using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary>Represents one ancestor of one task.  Each task will have at least one ancestor unless it is directly on a main trunk.  An ancestor is defined as a tasklist that is higher in the heirarchy for the task, regardless of how many levels up it is.  This allows us to mark task lists as having "new" tasks, and it allows us to quickly check for new tasks for a user on startup.</summary>
	[DataObject("taskancestor")]
	public class TaskAncestor : DataObjectBase {
		[DataField("TaskAncestorNum", PrimaryKey=true, AutoNumber=true)]
		private long taskAncestorNum;
		bool taskAncestorNumChanged;
		/// <summary>Primary key.</summary>
		public long TaskAncestorNum {
			get { return taskAncestorNum; }
			set { if(taskAncestorNum!=value){taskAncestorNum = value; MarkDirty(); taskAncestorNumChanged = true; }}
		}
		public bool TaskAncestorNumChanged {
			get { return taskAncestorNumChanged; }
		}

		[DataField("TaskNum")]
		private long taskNum;
		bool taskNumChanged;
		/// <summary>FK to task.TaskNum</summary>
		public long TaskNum {
			get { return taskNum; }
			set { if(taskNum!=value){taskNum = value; MarkDirty(); taskNumChanged = true;} }
		}
		public bool TaskNumChanged {
			get { return taskNumChanged; }
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









