using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Stores an ongoing record of database activity for security purposes.  User not allowed to edit.</summary>
	[Serializable]
	public class SecurityLog:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long SecurityLogNum;
		///<summary>Enum:Permissions</summary>
		public Permissions PermType;
		///<summary>FK to user.UserNum</summary>
		public long UserNum;
		///<summary>The date and time of the entry.  It's value is set when inserting and can never change.  Even if a user changes the date on their ocmputer, this remains accurate because it uses server time.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntry)]
		public DateTime LogDateTime;
		///<summary>The description of exactly what was done. Varies by permission type.</summary>
		public string LogText;
		///<summary>FK to patient.PatNum.  Can be 0 if not applicable.</summary>
		public long PatNum;
		///<summary></summary>
		public string CompName;

		

	}

	


}













