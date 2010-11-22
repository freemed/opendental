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
			this.isMobile=false;
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

		private bool isMobile;
		///<summary>Set to true for tables that are used on server for mobile services.  These are 'lite' versions of the main tables, and end with m.  A composite primary key will be expected.  The Crud generator will generate these crud files in a different place than the other crud files.  It will also generate the dataInterface 'ms' class to a different location.  It also won't validate that the table exists in the test database.</summary>
		public bool IsMobile {
			get { return isMobile; }
			set { isMobile=value; }
		}

	}
}
