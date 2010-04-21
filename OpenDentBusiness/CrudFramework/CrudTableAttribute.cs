using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	[AttributeUsage(AttributeTargets.Class,AllowMultiple=false)]
	public class CrudTableAttribute : Attribute {
		public CrudTableAttribute() {
			this.tableName="";
		}

		private string tableName;
		///<summary>If tablename is different than the lowercase class name.</summary>
		public string TableName {
			get { return tableName; }
			set { tableName=value; }
		}
	}
}
