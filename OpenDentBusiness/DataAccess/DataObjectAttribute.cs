using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDental.DataAccess {
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
	public class DataObjectAttribute : Attribute {
		public DataObjectAttribute(string tableName) {
			this.tableName = tableName;
		}

		private string tableName;
		public string TableName {
			get { return tableName; }
		}
	}
}
