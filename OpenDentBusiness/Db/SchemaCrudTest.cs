using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Please ignore this class.  It's used only for testing.</summary>
	public class SchemaCrudTest {
		///<summary>Example only</summary>
		public static void AddTableTempcore() {
			string command="";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS tempcore";
				Db.NonQ(command);
				command=@"CREATE TABLE tempcore (
					TempCoreNum bigint NOT NULL DEFAULT 0,
					TimeOfDayTest time NOT NULL DEFAULT '00:00:00',
					TimeStampTest timestamp,
					DateTest date NOT NULL DEFAULT '0001-01-01',
					DateTimeTest datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
					TimeSpanTest time NOT NULL DEFAULT '00:00:00',
					CurrencyTest double NOT NULL DEFAULT 0,
					BoolTest tinyint NOT NULL DEFAULT 0,
					TextSmallTest varchar(255),
					TextMediumTest text,
					TextLargeTest text,
					VarCharTest varchar(255)
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
					TempCoreNum number(20),
					TimeOfDayTest date,
					TimeStampTest timestamp,
					DateTest date,
					DateTimeTest date,
					TimeSpanTest varchar2(255),
					CurrencyTest number(38,8),
					BoolTest number(3),
					TextSmallTest varchar2(255),
					TextMediumTest clob,
					TextLargeTest clob,
					VarCharTest varchar2(255)
					)";
				Db.NonQ(command);
				command=@"ALTER TABLE tempcore MODIFY(
					TempCoreNum NOT NULL,
					TimeOfDayTest NOT NULL,
					TimeStampTest NOT NULL,
					DateTest NOT NULL,
					DateTimeTest NOT NULL,
					CurrencyTest NOT NULL,
					BoolTest NOT NULL
					)";
				Db.NonQ(command);
				command=@"ALTER TABLE tempcore MODIFY(
					TempCoreNum DEFAULT 0,
					TimeOfDayTest DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD'),
					TimeStampTest DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD'),
					DateTest DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD'),
					DateTimeTest DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD'),
					CurrencyTest DEFAULT 0,
					BoolTest DEFAULT 0
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