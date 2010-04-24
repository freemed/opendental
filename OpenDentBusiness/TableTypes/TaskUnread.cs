using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary></summary>
	[Serializable()]
	public class TaskUnread:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long TaskUnreadNum;
		///<summary>FK to task.TaskNum.</summary>
		public long TaskNum;
		///<summary>FK to userod.UserNum.</summary>
		public long UserNum;

		///<summary></summary>
		public Account Clone() {
			return (Account)this.MemberwiseClone();
		}

	}
}




