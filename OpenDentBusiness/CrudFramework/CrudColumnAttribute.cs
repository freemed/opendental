using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	[AttributeUsage(AttributeTargets.Field,AllowMultiple=false)]
	public class CrudColumnAttribute : Attribute {
		public CrudColumnAttribute() {
			this.isPriKey=false;
			this.specialType=EnumCrudSpecialColType.None;
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
	}

	public enum EnumCrudSpecialColType {
		None,
		///<summary>User not allowed to change.  Insert must use NOW().</summary>
		DateEntry,
		///<summary>Gets set and updated by MySQL.  Leave these columns completely out of Insert and Update statements.</summary>
		TimeStamp
		//Some more that we might add:
		//DateT, Timespan
	}
}
