using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	[AttributeUsage(AttributeTargets.Field,AllowMultiple=false)]
	public class CrudColumnAttribute : Attribute {
		public CrudColumnAttribute() {
			this.isPriKey=false;
			this.specialType=EnumCrudSpecialColType.None;
			this.isNotDbColumn=false;
		}

		private bool isPriKey;
		public bool IsPriKey {
			get { return isPriKey; }
			set { isPriKey=value; }
		}

		private EnumCrudSpecialColType specialType;
		public EnumCrudSpecialColType SpecialType {
			get { return specialType; }
			set { specialType=value; }
		}

		private bool isNotDbColumn;
		public bool IsNotDbColumn {
			get { return isNotDbColumn; }
			set { isNotDbColumn=value; }
		}
	}

	public enum EnumCrudSpecialColType {
		None,
		///<summary>User not allowed to change.  Insert uses NOW(), Update exludes this column, Select treats this like a date.</summary>
		DateEntry,
		///<summary>Insert uses NOW(), Update and Select treat this like a Date.</summary>
		DateEntryEditable,
		///<summary>Gets set and updated by MySQL.  Leave these columns completely out of Insert and Update statements.</summary>
		TimeStamp,
		///<summary>Same C# type as Date, but the MySQL database uses datetime instead of date.</summary>
		DateT,
		///<summary>User not allowed to change.  Insert uses NOW(), Update exludes this column, Select treats this like a DateT.</summary>
		DateTEntry,
		///<summary>Insert uses NOW(), Update and Select treat this like a DateT.</summary>
		DateTEntryEditable,
	}
}
