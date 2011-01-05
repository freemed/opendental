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
				Db.NonQ(command);
				command=@"CREATE TABLE tempcore (
					TimeOfDayTest time NOT NULL default '00:00:00',
					TimeStampTest timestamp,
					DateTest date NOT NULL default '0001-01-01',
					DateTimeTest datetime NOT NULL default '0001-01-01 00:00:00',
					TimeSpanTest time NOT NULL default '00:00:00',
					CurrencyTest double NOT NULL,
					BoolTest tinyint NOT NULL,
					TextSmallTest text NOT NULL,
					TextMediumTest text NOT NULL,
					TextLargeTest mediumtext NOT NULL,
					varCharTest varchar(255) NOT NULL
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				try {
					command="DROP TABLE tempcore";
					Db.NonQ(command);
				}
				catch {
				}
				command=@"CREATE TABLE tempcore (
					TimeOfDayTest time,
					TimeStampTest timestamp,
					DateTest date ,
					DateTimeTest datetime ,
					TimeSpanTest time,
					CurrencyTest double,
					BoolTest tinyint,
					TextSmallTest text,
					TextMediumTest text,
					TextLargeTest mediumtext,
					varCharTest varchar(255)
					)";
				Db.NonQ(command);
				command=@"ALTER TABLE tempcore MODIFY(
					TimeOfDayTest NOT NULL,
					DateTest NOT NULL,
					DateTimeTest NOT NULL,
					TimeSpanTest NOT NULL,
					CurrencyTest NOT NULL,
					BoolTest NOT NULL,
					)";
				Db.NonQ(command);
				command=@"ALTER TABLE tempcore MODIFY(
					TimeOfDayTest default '01-JAN-0001',
					TimeStampTest default '01-JAN-0001',
					DateTest default '01-JAN-0001',
					DateTimeTest default '01-JAN-0001',
					TimeSpanTest default 0,
					CurrencyTest default 0,
					BoolTest default 0
					)";
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