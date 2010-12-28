using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	public class DbSchema {
		
		/// <summary></summary>
		public static void AddColumnEnd(string tableName,DbSchemaCol col) {
			string command = "";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command = "ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetMySqlType(col)+";";
			}
			else {//oracle
				command = "ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetOracleType(col)+";";
				if(col.DataType==OdDbType.DateTimeStamp) {
				//Add code for building trigger here.
				}
			}
			Db.NonQ(command);
		}

		/// <summary>Specify textSize if there's any chance of it being greater than 4000 char.</summary>
		public static void AddColumnAfter(string tableName,DbSchemaCol col,string afterColumn) {
			string command = "";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command = "ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetMySqlType(col)+" AFTER "+afterColumn+";";
			}
			else {//oracle
				//query to find the column names

				//Workaround, must create a new table and copy data to add new column anywhere except the end.
				if(col.DataType==OdDbType.DateTimeStamp) {
					//Add code for building trigger here.
				}
			}
			Db.NonQ(command);
		}

		/// <summary>Specify textSize if there's any chance of it being greater than 4000 char.</summary>
		public static void AddColumnFirst(string tableName,DbSchemaCol col) {
			string command = "";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command = "ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetMySqlType(col)+";";
			}
			else {//oracle
				//Workaround, must create a new table and copy data to add new column anywhere except the end.
				if(col.DataType==OdDbType.DateTimeStamp) {
					//Add code for building trigger here.
				}
			}
			Db.NonQ(command);
		}

		/// <summary></summary>
		public static void DropColumn(string tableName,string columnName) {
			string command;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command= "ALTER TABLE "+tableName+" DROP COLUMN "+columnName+" ;";
			}
			else {//oracle
				command= "ALTER TABLE "+tableName+" DROP COLUMN "+columnName+" ;";
//todo: check for existing trigger or index
			}
			Db.NonQ(command);
		}

		/// <summary>First column is always a bigint, primary key, autoincrement.</summary>
		public static void AddTable(string tableName,List<DbSchemaCol> cols) {
			if(DataConnection.DBtype==DatabaseType.MySql) {

			}
			else {//oracle

			}
		}

		/// <summary></summary>
		public static void RenameColumn(string tableName,string columnName,string newColumnName) {
			if(DataConnection.DBtype==DatabaseType.MySql) {

			}
			else {//oracle

			}
		}

		/// <summary></summary>
		public static void ChangeColumnType(string tableName,string columnName,OdDbType newType) {
			if(DataConnection.DBtype==DatabaseType.MySql) {

			}
			else {//oracle

			}
		}

		/// <summary></summary>
		public static void AddKey(string tableName,string columnName) {
			if(DataConnection.DBtype==DatabaseType.MySql) {

			}
			else {//oracle

			}
		}

		/// <summary></summary>
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
					break;
				case OdDbType.Byte:
					return "tinyint unsigned";
					break;
				case OdDbType.Currency:
					return "double";
					break;
				case OdDbType.Date:
					return "date";
					break;
				case OdDbType.DateTime:
					return "datetime";
					break;
				case OdDbType.DateTimeStamp:
					return "timestamp";
					break;
				case OdDbType.Float:
					return "float";
					break;
				case OdDbType.Int:
					if(col.IntUseSmallInt){
						return "smallint";
					}
					else{
						return "int";
					}
					break;
				case OdDbType.Long:
					return "bigint";
					break;
				case OdDbType.Text:
					if(col.TextSize==TextSizeMySqlOracle.Small || col.TextSize==TextSizeMySqlOracle.Medium) {
						return "text";
					}
					else {//textSize==TextSizeMySqlOracle.large
						return "mediumtext";
					}
					break;
				case OdDbType.TimeOfDay:
					return "time";
					break;
				case OdDbType.TimeSpan:
					return "time";
					break;
				case OdDbType.VarChar255:
					return "varchar(255)";
					break;
			}
			return "";
		}

		private static string GetOracleType(DbSchemaCol col) {
			switch(col.DataType) {
				case OdDbType.Bool:
					return "NUMBER(3)";
					break;
				case OdDbType.Byte:
					return "NUMBER(3)";
					break;
				case OdDbType.Currency:
					return "NUMBER(38,8)";
					break;
				case OdDbType.Date:
					return "date";
					break;
				case OdDbType.DateTime:
					return "date";
					break;
				case OdDbType.DateTimeStamp:
					//also requires a trigger to be made
					return "date";
					break;
				case OdDbType.Float:
					return "number(38,8)";
					break;
				case OdDbType.Int:
					return "NUMBER(11)";
					break;
				case OdDbType.Long:
					return "NUMBER(20)";
					break;
				case OdDbType.Text:
					if(col.TextSize==TextSizeMySqlOracle.Small) {
						return "varchar2(4000)";
					}
					else {//textSize == medium or large
						return "clob";
					}
					break;
				case OdDbType.TimeOfDay:
					return "date";
					break;
				case OdDbType.TimeSpan:
					return "varchar2(255)";
					break;
				case OdDbType.VarChar255:
					return "varchar2(255)";
					break;
			}
			return "";
		}

		/// <summary>Fills new table by selecting each column before index from old table, creates new column at index, continues to select columns from old table, drops old table, renames new table.</summary>
		private static void oracleAddAtIndexHelper(string tableName,int indexOfNewColumn) {
			//Oracle does not support adding columns to tables anywhere other than the end of the table. Workaround is to create a new table, fill it with data, drop the old table, and rename the new table.
			//MySql does not need to use this function ever.
		}



	}
}
