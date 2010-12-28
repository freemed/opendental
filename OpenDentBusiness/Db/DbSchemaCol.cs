using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	public class DbSchemaCol {
		public string ColumnName;
		public OdDbType DataType;
		public bool Indexed;
		///<summary>Specify textSize if there's any chance of it being greater than 4000 char.</summary>
		public TextSizeMySqlOracle TextSize;
		///<summary>If specifying an int, it uses int by default.  Set this to true to instead use smallint.</summary>
		public bool IntUseSmallInt;

		public DbSchemaCol(string columnName,OdDbType dataType) {
			ColumnName=columnName;
			DataType=dataType;
			Indexed=false;
		}

	}

	public enum TextSizeMySqlOracle {
		///<summary>255-4k, MySql: text, Oracle: varchar2</summary>
		Small,
		///<summary>4k-65k, MySql: text, Oracle: clob</summary>
		Medium,
		///<summary>65k+, MySql: mediumtext, Oracle: clob</summary>
		Large
	}


}
