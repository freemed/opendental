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
					TempCoreNum bigint NOT NULL,
					TimeOfDayTest time NOT NULL DEFAULT '00:00:00',
					TimeStampTest timestamp,
					DateTest date NOT NULL DEFAULT '0001-01-01',
					DateTimeTest datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
					TimeSpanTest time NOT NULL DEFAULT '00:00:00',
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
				catch(Exception e) {}
				command=@"CREATE TABLE tempcore (
					TempCoreNum number(20) NOT NULL,
					TimeOfDayTest date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
					TimeStampTest timestamp DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
					DateTest date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
					DateTimeTest date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
					TimeSpanTest varchar2(255),
					CurrencyTest number(38,8) NOT NULL,
					BoolTest number(3) NOT NULL,
					TextSmallTest varchar2(255),
					TextMediumTest clob,
					TextLargeTest clob,
					VarCharTest varchar2(255)
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