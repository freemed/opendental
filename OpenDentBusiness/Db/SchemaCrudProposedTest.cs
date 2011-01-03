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
				command="DROP TABLE IF EXISTS ryantempcore;"
					+@"CREATE TABLE ryantempcore (
					ColOne int NOT NULL
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command=@"CREATE TABLE ryantempcore (
					ColOne int NOT NULL
					)";
				Db.NonQ(command);
			}
		}

		///<summary>Example only</summary>
		public static void AddColumnEnd() {
			string command="";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE ryantempcore ADD ColEnd int NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE ryantempcore ADD ColEnd number(11);";
				Db.NonQ(command);
				command="UPDATE tempcore SET ColEnd = 0 WHERE ColEnd IS NULL;";
				Db.NonQ(command);
				command="ALTER TABLE tempcore MODIFY ColEnd NOT NULL";
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
