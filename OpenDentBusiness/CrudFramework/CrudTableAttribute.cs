using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	[AttributeUsage(AttributeTargets.Class,AllowMultiple=false)]
	public class CrudTableAttribute : Attribute {
		public CrudTableAttribute() {
			this.tableName="";
			this.isDeleteForbidden=false;
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

	}
}
