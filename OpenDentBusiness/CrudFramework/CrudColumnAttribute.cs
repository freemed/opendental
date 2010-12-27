using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	[AttributeUsage(AttributeTargets.Field,AllowMultiple=false)]
	public class CrudColumnAttribute : Attribute {
		public CrudColumnAttribute() {
			this.isPriKey=false;
			this.specialType=CrudSpecialColType.None;
			this.isNotDbColumn=false;
			this.isPriKeyMobile1=false;
			this.isPriKeyMobile2=false;
		}

		private bool isPriKey;
		public bool IsPriKey {
			get { return isPriKey; }
			set { isPriKey=value; }
		}

		private CrudSpecialColType specialType;
		public CrudSpecialColType SpecialType {
			get { return specialType; }
			set { specialType=value; }
		}

		private bool isNotDbColumn;
		public bool IsNotDbColumn {
			get { return isNotDbColumn; }
			set { isNotDbColumn=value; }
		}

		private bool isPriKeyMobile1;
		///<summary>Always present in a mobile table.  Always CustomerNum, FK to PatNum.</summary>
		public bool IsPriKeyMobile1 {
			get { return isPriKeyMobile1; }
			set { isPriKeyMobile1=value; }
		}

		private bool isPriKeyMobile2;
		///<summary>Always present in a mobile table.  Always the ordinary priKey of the table, used together with CustomerNum.</summary>
		public bool IsPriKeyMobile2 {
			get { return isPriKeyMobile2; }
			set { isPriKeyMobile2=value; }
		}

	}

	public enum CrudSpecialColType {
		None,
		///<summary>User not allowed to change.  Insert uses NOW(), Update exludes this column, Select treats this like a date.</summary>
		DateEntry,
		///<summary>Insert uses NOW(), Update and Select treat this like a Date.</summary>
		DateEntryEditable,
		///<summary>Is set and updated by MySQL.  Leave these columns completely out of Insert and Update statements.</summary>
		TimeStamp,
		///<summary>Same C# type as Date, but the MySQL database uses datetime instead of date.</summary>
		DateT,
		///<summary>User not allowed to change.  Insert uses NOW(), Update exludes this column, Select treats this like a DateT.</summary>
		DateTEntry,
		///<summary>Insert uses NOW(), Update and Select treat this like a DateT.</summary>
		DateTEntryEditable,
		///<summary>Database type is tinyint unsigned.  C# type is int.  Range -128 to 128.  The validation does not check to make sure the db is unsigned.  The programmer must do that.  So far, only used for percent fields because -1 is required to be accepted.  For most other fields, such as enums and itemorders, the solution is to change the field in C# to a byte, findicating a range of 0-255.  It usually doesn't matter whether the database accepts values to 255 or only to 127.  The validation does not check that.</summary>
		TinyIntUnsigned,
		///<summary>We do not want this column updated except as part of a separate routine.  Warning! The logic fails if this is used on the last column in a table.</summary>
		ExcludeFromUpdate,
		///<summary>Instead of storing this enum as an int in the db, it is stored as a string.  Very rarely used.</summary>
		EnumAsString,
		///<summary>For most C# TimeSpans, the default db type is TimeOfDay.  But for the few that need to use negative values or values greater than 24 hours, they get marked as this special type.  Handled differently in MySQL vs Oracle.</summary>
		TimeSpanNeg,
		///<summary>For most C# TimeSpans, the default db type is TimeOfDay.  But for the few that need to use negative values or values greater than 24 hours, they get marked as this special type.  Handled differently in MySQL vs Oracle.</summary>
		Text
	}
}
