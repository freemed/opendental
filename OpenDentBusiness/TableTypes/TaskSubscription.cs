using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary>A subscription of one user to either a tasklist or to a task.</summary>
	[Serializable()]
	public class TaskSubscription : TableBase {
		/// <summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long TaskSubscriptionNum;
		/// <summary>FK to userod.UserNum</summary>
		public long UserNum;
		/// <summary>FK to tasklist.TaskListNum</summary>
		public long TaskListNum;

		

		
		

			
	}

	

}









