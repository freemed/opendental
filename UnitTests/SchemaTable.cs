using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	class SchemaTable {
		//This defines a test table from which the schema insert table will be generated.
		public static string TableName;
		public static List<DbSchemaCol> Cols;

		public SchemaTable(string tabName, List<DbSchemaCol> cols) {
			TableName = tabName;
			Cols = cols;
		}

		public SchemaTable() {
			TableName = "tempcore";
			Cols.Add(new DbSchemaCol("colOneInt",OdDbType.Int));
			Cols.Add(new DbSchemaCol("colTwoText",OdDbType.Text,TextSizeMySqlOracle.Medium));
			Cols.Add(new DbSchemaCol("colThrDateT",OdDbType.DateTime));
		}

	}
}
