using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	public class DbSchema {
		
		/// <summary>Completed</summary>
		public static void AddColumnEnd(string tableName,DbSchemaCol col) {
			string command = "";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command = "ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetMySqlType(col)+";";
			}
			else {//oracle
				command = "ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetOracleType(col)+";";
				OracleValidateDateTStampTriggerHelper(tableName);
			}
			Db.NonQ(command);
		}

		/// <summary>TODO:this function; Specify textSize if there's any chance of it being greater than 4000 char.</summary>
		public static void AddColumnAfter(string tableName,DbSchemaCol col,string afterColumn) {
			string command = "";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command = "ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetMySqlType(col)+" AFTER "+afterColumn+";";
				Db.NonQ(command);
			}
			else {//oracle
				int addAtIndex=0;
				command ="Select TABLE_NAME, COLUMN_NAME from user_tab_columns where table_name='"+tableName.ToUpper()+"';";
				DataTable tempTable = Db.GetTable(command);//get list of columns
				for(int i=0;i<tempTable.Rows.Count;i++) {//find column index of column that matches afterColumn
					if(tempTable.Rows[i][1].ToString()==afterColumn) {
						addAtIndex = i+1;
					}
				}
				OracleAddAtIndexHelper(tableName,col,addAtIndex);
				OracleValidateDateTStampTriggerHelper(tableName);
			}
		}

		/// <summary>TODO:this function; Specify textSize if there's any chance of it being greater than 4000 char.</summary>
		public static void AddColumnFirst(string tableName,DbSchemaCol col) {
			string command = "";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command = "ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetMySqlType(col)+";";
			}
			else {//oracle
				OracleAddAtIndexHelper(tableName,col,0);
				OracleValidateDateTStampTriggerHelper(tableName);
			}
			Db.NonQ(command);
		}

		/// <summary>TODO: trigger cleanup for oracle</summary>
		public static void DropColumn(string tableName,string columnName) {
			string command;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command= "ALTER TABLE "+tableName+" DROP COLUMN "+columnName+" ;";
			}
			else {//oracle
				command= "ALTER TABLE "+tableName+" DROP COLUMN "+columnName+" ;";
				OracleValidateDateTStampTriggerHelper(tableName);
//todo: check for existing trigger or index
			}
			Db.NonQ(command);
		}

		/// <summary>First column is always a bigint, primary key, autoincrement.</summary>
		public static void AddOrReplaceTable(string tableName,List<DbSchemaCol> cols) {
			string command;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				//redefine keys
				//redefine indexes and sequences
			}
			else {//oracle
				command = "CREATE TABLE newtemptable ( ";
				for(int i=0;i<cols.Count;i++) {
					command+=cols[i].ColumnName+" "+GetOracleType(cols[i])+(i==0?" primary key ":"")+(i==cols.Count-1?", ":" );");
				}
				//redefine keys
				//redefine indexes and sequences
				//fill new table with old data
				//check sequences
				//check other triggers

				command = "ALTER TABLE newtemptable RENAME TO "+tableName+";";
				Db.NonQ(command);
				OracleValidateDateTStampTriggerHelper(tableName);
			}
		}

		/// <summary>TODO.this</summary>
		public static void RenameColumn(string tableName,string columnName,string newColumnName) {
			if(DataConnection.DBtype==DatabaseType.MySql) {

			}
			else {//oracle

			}
		}

		/// <summary>TODO.this</summary>
		public static void ChangeColumnType(string tableName,string columnName,OdDbType newType) {
			if(DataConnection.DBtype==DatabaseType.MySql) {

			}
			else {//oracle

			}
		}

		/// <summary>TODO.this</summary>
		public static void AddKey(string tableName,string columnName) {
			if(DataConnection.DBtype==DatabaseType.MySql) {

			}
			else {//oracle

			}
		}

		/// <summary>TODO.this</summary>
		public static void RemoveKey(string tableName,string columnName) {
			if(DataConnection.DBtype==DatabaseType.MySql) {

			}
			else {//oracle

			}
		}

		private static string GetMySqlType(DbSchemaCol col) {
			switch(col.DataType) {
				case OdDbType.Bool:
					return "tinyint";
				case OdDbType.Byte:
					return "tinyint unsigned";
				case OdDbType.Currency:
					return "double";
				case OdDbType.Date:
					return "date";
				case OdDbType.DateTime:
					return "datetime";
				case OdDbType.DateTimeStamp:
					return "timestamp";
				case OdDbType.Float:
					return "float";
				case OdDbType.Int:
					if(col.IntUseSmallInt){
						return "smallint";
					}
					else{
						return "int";
					}
				case OdDbType.Long:
					return "bigint";
				case OdDbType.Text:
					if(col.TextSize==TextSizeMySqlOracle.Small || col.TextSize==TextSizeMySqlOracle.Medium) {
						return "text";
					}
					else {//textSize==TextSizeMySqlOracle.large
						return "mediumtext";
					}
				case OdDbType.TimeOfDay:
					return "time";
				case OdDbType.TimeSpan:
					return "time";
				case OdDbType.VarChar255:
					return "varchar(255)";
			}
			return "";
		}

		private static string GetOracleType(DbSchemaCol col) {
			switch(col.DataType) {
				case OdDbType.Bool:
					return "NUMBER(3)";
				case OdDbType.Byte:
					return "NUMBER(3)";
				case OdDbType.Currency:
					return "NUMBER(38,8)";
				case OdDbType.Date:
					return "date";
				case OdDbType.DateTime:
					return "date";
				case OdDbType.DateTimeStamp:
					//also requires trigger, trigger code is automatically created above.
					return "date";
				case OdDbType.Float:
					return "number(38,8)";
				case OdDbType.Int:
					return "NUMBER(11)";
				case OdDbType.Long:
					return "NUMBER(20)";
				case OdDbType.Text:
					if(col.TextSize==TextSizeMySqlOracle.Small) {
						return "varchar2(4000)";
					}
					else {//textSize == medium or large
						return "clob";
					}
				case OdDbType.TimeOfDay:
					return "date";
				case OdDbType.TimeSpan:
					return "varchar2(255)";
				case OdDbType.VarChar255:
					return "varchar2(255)";
			}
			return "";
		}

		/// <summary>validates any table's dateTStamp triggers for Oracle.</summary>
		private static void OracleValidateDateTStampTriggerHelper(string tableName){
			bool triggerNeeded = false;
			bool needDropTrigger = false;
			string command ="Select TABLE_NAME, COLUMN_NAME from user_tab_columns where table_name='"+tableName.ToUpper()+"';";
			DataTable tempTable = Db.GetTable(command);//get list of columns
			for(int i=0;i<tempTable.Rows.Count;i++){//check columns for a DateTStamp
				if(tempTable.Rows[i][1].ToString()=="DateTStamp"){
					triggerNeeded=true;
				}
			}
			if(triggerNeeded) {//table needs a timestamp trigger, regaurdless of existing triggers
				command = "CREATE OR REPLACE TRIGGER "+tableName+"_timestamp BEFORE UPDATE ON "+tableName+"FOR EACH ROW "
										+"BEGIN";
				for(int i=0;i<tempTable.Rows.Count;i++) {//iterate through each column to see if it was changed
					command+="	IF :OLD."+tempTable.Rows[i][1].ToString()+" <> :NEW."+tempTable.Rows[i][1].ToString()+" THEN"
										+"	:NEW.DateTStamp := SYSDATE;"
										+"	END IF;";
				}
				command+="END "+tableName+"_timespan;";
			}
			else {//table needs to have zero DateTStamp triggers
				command = "Select TRIGGER_NAME from user_triggers WHERE tablename = '"+tableName.ToUpper()+"';";
				DataTable tempTriggerTable = Db.GetTable(command);
				for(int i=0;i<tempTriggerTable.Rows.Count;i++) {//check for timestamp triggers before trying to delete
					if(tempTriggerTable.Rows[i][0].ToString().Contains("_TIMESTAMP")) {
						needDropTrigger = true;
					}
				}
				if(needDropTrigger) {//Delete timestamp triggers if they exist
					command = "DROP TRIGGER "+tableName.ToUpper()+"_TIMESTAMP WHERE TABLE_NAME = '"+tableName.ToUpper()+"';";
					Db.NonQ(command);
				}
			}
		}

		/// <summary>Fills new table by selecting each column before index from old table, creates new column at index, continues to select columns from old table, drops old table, renames new table.</summary>
		private static void OracleAddAtIndexHelper(string tableName,DbSchemaCol col, int indexOfNewColumn) {
			string command;
			DbSchemaCol newCol;
			command = "Select * FROM user_tab_columns WHERE table_name='"+tableName.ToUpper()+"';";
			DataTable tempTable = Db.GetTable(command);//get list of columns
			List<DbSchemaCol> newTableCols = new List<DbSchemaCol>();
			for(int i=0;i<tempTable.Rows.Count;i++) {
				if(i==indexOfNewColumn){//once we reach the index of the new column add it to the table
					newCol = new DbSchemaCol(col.ColumnName,col.DataType);//add new column here
					newTableCols.Add(newCol);
				}
				newCol = new DbSchemaCol(tempTable.Rows[i][1].ToString(),OdDbType.Bool /* must convert this:tempTable.Rows[i][2] to OdDbTypes*/);
				newTableCols.Add(newCol);
			}
			AddOrReplaceTable(tableName,newTableCols);
			//Validating timestamps occurs in the code above
			//drop old table
			//rename new table_name to old table_name
		}



	}
}
