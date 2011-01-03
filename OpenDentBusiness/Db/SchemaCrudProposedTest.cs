using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Please ignore this class.  It's used only for testing.</summary>
	public class SchemaCrudProposedTest {
		///<summary>Example only</summary>
		public static void AddTable() {
			string command="";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS tempcore;"
					+@"CREATE TABLE tempcore (
					ColOne int NOT NULL
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command="DROP TABLE IF EXISTS tempcore;"
					+@"CREATE TABLE tempcore (
					ColOne int NOT NULL
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
		}

		///<summary>Example only</summary>
		public static void AddColumnEnd() {
			string command="";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE tempcore ADD ColEnd int NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE tempcore ADD ColEnd number(11);"
					+"UPDATE TABLE tempcore SET ColEnd = 0 WHERE ColEnd IS NULL;"
					+"ALTER TABLE tempcore MODIFY ColEnd NOT NULL";
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
}
