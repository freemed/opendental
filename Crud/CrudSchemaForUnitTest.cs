using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace Crud {
	///<summary>See UnitTests.SchemaT.cs.  This class generates the SchemaCrudTest file.</summary>
	public class CrudSchemaForUnitTest {
		public static string Create() {
			StringBuilder strb=new StringBuilder();
			//This is a stub that is to be replaced with some good code generation:
			strb.Append(@"using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Please ignore this class.  It's used only for testing.</summary>
	public class SchemaCrudTest {
		///<summary>Example only</summary>
		public static void AddTable() {
			string command="""";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command=""DROP TABLE IF EXISTS tempcore;""
					+@""CREATE TABLE tempcore (
					ColOne int NOT NULL
					) DEFAULT CHARSET=utf8"";
				Db.NonQ(command);
			}
			else {//oracle
				command=""""
					+@""CREATE TABLE tempcore (
					ColOne int NOT NULL
					)"";
				Db.NonQ(command);
			}
		}

		///<summary>Example only</summary>
		public static void AddColumnEnd() {
			string command="""";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command=""ALTER TABLE tempcore ADD ColEnd int NOT NULL"";
				Db.NonQ(command);
			}
			else {//oracle
				command=""ALTER TABLE tempcore ADD ColEnd number(11);""
					+""UPDATE TABLE tempcore SET ColEnd = 0 WHERE ColEnd IS NULL;""
					+""ALTER TABLE tempcore MODIFY ColEnd NOT NULL"";
				Db.NonQ(command);
			}
		}

		//AddColumnEndTimeStamp
		//AddIndex
		//AddColumnAfter
		//DropColumn
		//DropColumnTimeStamp
		//DropIndex
		//AddTable
		//etc.


	}
}");
			return strb.ToString();
		}



	}
}
