using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary></summary>
	[Serializable]
	public class ICD9:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ICD9Num;
		///<summary>Not allowed to edit this column once saved in the database.</summary>
		public string ICD9Code;
		///<summary>Description.</summary>
		public string Description;

		///<summary></summary>
		public ICD9 Copy() {
			return (ICD9)this.MemberwiseClone();
		}

	}
}