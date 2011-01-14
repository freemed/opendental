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
					TempCoreNum bigint NOT NULL auto_increment PRIMARY KEY,
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
					VarCharTest varchar(255) NOT NULL,
					DropableColumn tinyint NOT NULL
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE tempcore'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
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
					VarCharTest varchar2(255),
					DropableColumn number(3) NOT NULL,
					CONSTRAINT TempCoreNum PRIMARY KEY (TempCoreNum)
					)";
				Db.NonQ(command);
				command=@"CREATE OR REPLACE TRIGGER tempcore_timestamp
				           BEFORE UPDATE ON tempcore
				           FOR EACH ROW
				           BEGIN
					           IF :OLD.TempCoreNum <> :NEW.TempCoreNum THEN
					           :NEW.TimeStampTest := SYSDATE;
					           END IF
					           IF :OLD.TimeOfDayTest <> :NEW.TimeOfDayTest THEN
					           :NEW.TimeStampTest := SYSDATE;
					           END IF
					           IF :OLD.TimeStampTest <> :NEW.TimeStampTest THEN
					           :NEW.TimeStampTest := SYSDATE;
					           END IF
					           IF :OLD.DateTest <> :NEW.DateTest THEN
					           :NEW.TimeStampTest := SYSDATE;
					           END IF
					           IF :OLD.DateTimeTest <> :NEW.DateTimeTest THEN
					           :NEW.TimeStampTest := SYSDATE;
					           END IF
					           IF :OLD.TimeSpanTest <> :NEW.TimeSpanTest THEN
					           :NEW.TimeStampTest := SYSDATE;
					           END IF
					           IF :OLD.CurrencyTest <> :NEW.CurrencyTest THEN
					           :NEW.TimeStampTest := SYSDATE;
					           END IF
					           IF :OLD.BoolTest <> :NEW.BoolTest THEN
					           :NEW.TimeStampTest := SYSDATE;
					           END IF
					           IF :OLD.TextSmallTest <> :NEW.TextSmallTest THEN
					           :NEW.TimeStampTest := SYSDATE;
					           END IF
					           IF :OLD.TextMediumTest <> :NEW.TextMediumTest THEN
					           :NEW.TimeStampTest := SYSDATE;
					           END IF
					           IF :OLD.TextLargeTest <> :NEW.TextLargeTest THEN
					           :NEW.TimeStampTest := SYSDATE;
					           END IF
					           IF :OLD.VarCharTest <> :NEW.VarCharTest THEN
					           :NEW.TimeStampTest := SYSDATE;
					           END IF
					           IF :OLD.DropableColumn <> :NEW.DropableColumn THEN
					           :NEW.TimeStampTest := SYSDATE;
					           END IF
				           END tempcore_timestamp;";
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

		///<summary>Example only</summary>
		public static void AddColumnEndTimeStamp() {
			string command="";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE tempcore ADD ColEndTimeStamp timestamp";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE tempcore ADD ColEndTimeStamp date";
				Db.NonQ(command);
				command=@"CREATE OR REPLACE TRIGGER tempcore_timestamp
				BEFORE UPDATE ON tempcore
				FOR EACH ROW
				BEGIN
					IF :OLD.TempCoreNum <> :NEW.OpName THEN
					:NEW.ColEndTimeStamp := SYSDATE;
					END IF;
					... REPEAT FOR EACH COLLUMN IN TABLE...
					... REPEAT FOR EACH COLLUMN IN TABLE...
					... REPEAT FOR EACH COLLUMN IN TABLE...
					... REPEAT FOR EACH COLLUMN IN TABLE...
					... REPEAT FOR EACH COLLUMN IN TABLE...
					... REPEAT FOR EACH COLLUMN IN TABLE...
				END tempcore_timestamp";
				Db.NonQ(command);
			}
		}

		///<summary>Example only</summary>
		public static void AddIndex() {
			string command="";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE tempcore ADD INDEX IDX_TEMPCORE_TEMPCORENUM (tempCoreNum)";
				Db.NonQ(command);
			}
			else {//oracle
				command="CREATE INDEX IDX_TEMPCORE_TEMPCORENUM ON tempcore (tempCoreNum)";
				Db.NonQ(command);
			}
		}

		///<summary>Example only</summary>
		public static void DropColumn() {
			string command="";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE tempcore DROP COLUMN DropableColumn";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE tempcore DROP COLUMN DropableColumn";
				Db.NonQ(command);
			}
		}

		//AddColumnEndTimeStamp
		//AddColumnAfter
		//DropColumnTimeStamp
		//DropIndex
		//etc.


	}
}