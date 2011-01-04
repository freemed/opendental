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
				command="DROP TABLE IF EXISTS tempcore";
				DataCore.NonQ(command);
				command=@"CREATE TABLE tempcore (
					TimeOfDayTest time NOT NULL default '00:00:00',
					TimeStampTest timestamp,
					DateTest date NOT NULL default '0001-01-01',
					DateTimeTest datetime NOT NULL default '0001-01-01 00:00:00',
					TimeSpanTest time NOT NULL default '00:00:00',
					CurrencyTest double NOT NULL,
					BoolTest tinyint NOT NULL,
					TextTest text NOT NULL,
					CharTest char(1) NOT NULL,
					ClobTest mediumtext NOT NULL,
					BlobTest mediumblob NOT NULL
					) DEFAULT CHARSET=utf8";
				DataCore.NonQ(command);
			}
			else {//oracle
				try {
					command="DROP TABLE tempcore";
					Db.NonQ(command);
				}
				catch {
				}
/*				command = @"CREATE TABLE tempcore (
					TimeOfDayTest time NOT NULL default '00:00:00',
					TimeStampTest timestamp,
					DateTest date NOT NULL default '0001-01-01',
					DateTimeTest datetime NOT NULL default '0001-01-01 00:00:00',
					TimeSpanTest time NOT NULL default '00:00:00',
					CurrencyTest double NOT NULL,
					BoolTest tinyint NOT NULL,
					TextTest text NOT NULL,
					CharTest char(1) NOT NULL,
					ClobTest mediumtext NOT NULL,
					BlobTest mediumblob NOT NULL
				)";*/
				Db.NonQ(command);
			}
		}

		///<summary>Example only</summary>
		public static void AddColumnEndClob() {
			string command="";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE tempcore ADD ColEndClob text NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE tempcore ADD ColEndClob clob";
				Db.NonQ(command);
			}
		}

		///<summary>Example only</summary>
		public static void AddColumnEndInt() {
			string command="";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE tempcore ADD ColEndInt int NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE tempcore ADD ColEndInt number(11)";
				Db.NonQ(command);
				command="UPDATE tempcore SET ColEndInt = 0 WHERE ColEndInt IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE tempcore MODIFY ColEndInt NOT NULL";
				Db.NonQ(command);
			}
		}

		//AddColumnEndTimeStamp
		//AddIndex
		//AddColumnAfter
		//DropColumn
		//DropColumnTimeStamp
		//DropIndex
		//etc.


	}
}