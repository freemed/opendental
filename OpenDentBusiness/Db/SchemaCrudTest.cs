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
					TimeOfDayTest time NOT NULL DEFAULT '01-01-0001',
					TimeStampTest timestamp NOT NULL DEFAULT '01-01-0001',
					DateTest date NOT NULL DEFAULT '01-01-0001',
					DateTimeTest datetime NOT NULL DEFAULT '01-01-0001',
					TimeSpanTest time NOT NULL DEFAULT '01-01-0001',
					CurrencyTest double NOT NULL DEFAULT 0,
					BoolTest tinyint NOT NULL DEFAULT 0,
					TextSmallTest varchar(255) NOT NULL DEFAULT "",
					TextMediumTest text NOT NULL DEFAULT "",
					TextLargeTest text NOT NULL DEFAULT "",
					VarCharTest varchar(255) NOT NULL DEFAULT ""
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
					TimeStampTest date,
					DateTest date,
					DateTimeTest date,
					TimeSpanTest varchar2(255),
					CurrencyTest number(38,8),
					BoolTest number(3),
					TextSmallTest varchar2(255),
					TextMediumTest varchar2(4000),
					TextLargeTest varchar2(4000),
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
					TimeSpanTest DEFAULT "",
					CurrencyTest DEFAULT 0,
					BoolTest DEFAULT 0,
					TextSmallTest DEFAULT "",
					TextMediumTest DEFAULT "",
					TextLargeTest DEFAULT "",
					VarCharTest DEFAULT "",
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
				command="UPDATE tempcore SET ColEndClob = \"\" WHERE ColEndClob IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE tempcore MODIFY ColEndClob NOT NULL";
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
		//AddTable
		//etc.


	}
}