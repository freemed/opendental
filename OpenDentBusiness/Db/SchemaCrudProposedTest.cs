using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Please ignore this class.  It's used only for testing.</summary>
	public class SchemaCrudProposedTest {
		///<summary>Example only</summary>
		public static void AddTableTempcore() {
			string command="";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS tempcore";
				Db.NonQ(command);
				command=@"CREATE TABLE tempcore (
					TempCoreNum bigint NOT NULL DEFAULT 0,
					TimeOfDayTest time NOT NULL default '00:00:00',
					TimeStampTest timestamp,
					DateTest date NOT NULL default '0001-01-01',
					DateTimeTest datetime NOT NULL default '0001-01-01 00:00:00',
					TimeSpanTest time NOT NULL default '00:00:00',
					CurrencyTest double NOT NULL,
					BoolTest tinyint NOT NULL,
					TextSmallTest varchar(255) NOT NULL,
					TextMediumTest text NOT NULL,
					TextLargeTest text NOT NULL,
					VarCharTest varchar(255) NOT NULL
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				try {
					command="DROP TABLE tempcore";
					Db.NonQ(command);
				}
				catch(Exception e) {
				}
				command=@"CREATE TABLE tempcore (
					TimeOfDayTest date,
					TimeStampTest timestamp,
					DateTest date,
					DateTimeTest date,
					TimeSpanTest date,
					CurrencyTest number(20),
					BoolTest number(3),
					TextSmallTest varchar2(255),
					TextMediumTest clob,
					TextLargeTest clob,
					VarCharTest varchar2(255)
					)";
				Db.NonQ(command);
				command=@"ALTER TABLE tempcore MODIFY(
					TimeOfDayTest NOT NULL,
					DateTest NOT NULL,
					DateTimeTest NOT NULL,
					TimeSpanTest NOT NULL,
					CurrencyTest NOT NULL,
					BoolTest NOT NULL
					)";
				Db.NonQ(command);
				command=@"ALTER TABLE tempcore MODIFY(
					TimeOfDayTest default TO_DATE('0001-01-01','YYYY-MM-DD'),
					TimeStampTest default TO_DATE('0001-01-01','YYYY-MM-DD'),
					DateTest default TO_DATE('0001-01-01','YYYY-MM-DD'),
					DateTimeTest default TO_DATE('0001-01-01','YYYY-MM-DD'),
					TimeSpanTest default TO_DATE('0001-01-01','YYYY-MM-DD'),
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