using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	[AttributeUsage(AttributeTargets.Class,AllowMultiple=false)]
	public class CrudTableAttribute : Attribute {
		public CrudTableAttribute() {
			this.tableName="";
			this.isDeleteForbidden=false;
			this.isMissingInGeneral=false;
		}

		private string tableName;
		///<summary>If tablename is different than the lowercase class name.</summary>
		public string TableName {
			get { return tableName; }
			set { tableName=value; }
		}

		private bool isDeleteForbidden;
		///<summary>Set to true for tables where rows are not deleted.</summary>
		public bool IsDeleteForbidden {
			get { return isDeleteForbidden; }
			set { isDeleteForbidden=value; }
		}

		private bool isMissingInGeneral;
		///<summary>Set to true for tables that are part of internal tools and not found in the general release.  The Crud generator will gracefully skip these tables if missing from the database that it's running against.  It also won't try to generate a dataInterface s class.</summary>
		public bool IsMissingInGeneral {
			get { return isMissingInGeneral; }
			set { isMissingInGeneral=value; }
		}

	}
}
