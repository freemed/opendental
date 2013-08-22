using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>Other tables generally use the ICD9Code string as their foreign key.  Currently synched to mobile server in a very inefficient manner.  It is implied that these are all ICD9CMs, although that may not be the case in the future.</summary>
	[Serializable]
	public class Icd10:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long Icd10Num;
		///<summary>Not allowed to edit this column once saved in the database.</summary>
		public string Icd10Code;
		///<summary>Description.</summary>
		public string Description;
		///<summary>The last date and time this row was altered.  Not user editable.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;

		///<summary></summary>
		public ICD9 Copy() {
			return (ICD9)this.MemberwiseClone();
		}

	}
}