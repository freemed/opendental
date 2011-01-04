using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace Crud {
	///<summary>This is the class that actually generates snippets of raw schema code.</summary>
	public class CrudSchemaRaw {
		private const string rn="\r\n";
		private const string t1="\t";
		private const string t2="\t\t";
		private const string t3="\t\t\t";
		private const string t4="\t\t\t\t";
		private const string t5="\t\t\t\t\t";
		private static string tb = "";

		/// <summary>Currently only works for string types.  Needs work.</summary>
		public static string AddColumnEnd(string tableName,DbSchemaCol col,int tabInset) {
			//After the rewrite, this will return C# with queries in it instead of actually running them here.
			StringBuilder strb = new StringBuilder();
			tb="";//must reset tabs each time method is called
			for(int i=0;i<tabInset;i++){//defines the base tabs to be added to all lines
				tb+="\t";
			}
			strb.Append(tb+"if(DataConnection.DBtype==DatabaseType.MySql) {");
			strb.Append(rn+tb+t1+"command=\"ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetMySqlType(col)+" NOT NULL\";");
//			strb.Append(rn+tb+t1+"//If ColEnd might be over 65k characters, use mediumtext");
			strb.Append(rn+tb+t1+"Db.NonQ(command);");
			strb.Append(rn+tb+"}");
			strb.Append(rn+tb+"else {//oracle");
			strb.Append(rn+tb+t1+"command=\"ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetOracleType(col)+"\";");
			strb.Append(rn+tb+t1+"Db.NonQ(command);");
			if(GetOracleBlankData(col)=="") {//Do not add NOT NULL constraint because empty strings are stored as NULL in Oracle
			}
			else {//Non string types must be filled with "blank" data and set to NOT NULL
				strb.Append(rn+tb+t1+"command=\"UPDATE "+tableName+" SET "+col.ColumnName+" = "+GetOracleBlankData(col)+" WHERE "+col.ColumnName+" IS NULL\";");
				strb.Append(rn+tb+t1+"Db.NonQ(command);");
				strb.Append(rn+tb+t1+"command=\"ALTER TABLE "+tableName+" MODIFY "+col.ColumnName+" NOT NULL\";");
				strb.Append(rn+tb+t1+"Db.NonQ(command);");
			}
			strb.Append(rn+tb+"}");
			return strb.ToString();
		}

		/// <summary>For example, might return "bigint NOT NULL".</summary>
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
				case OdDbType.Enum:
					return "float";
				case OdDbType.Int:
					if(col.IntUseSmallInt) {
						return "smallint";
					}
					else {
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
				default:
					throw new ApplicationException("type not found");
			}
		}

		///<summary>For example, might returns "0", "", or "01-01-0001" for cols with types OdDbType.Byte, OdDbType.Text, and OdDbType.DateTime respectively.</summary>
		private static string GetMySqlBlankData(DbSchemaCol col) {
			switch(col.DataType) {
				case OdDbType.Bool:
				case OdDbType.Byte:
				case OdDbType.Currency:
				case OdDbType.Enum:
				case OdDbType.Float:
				case OdDbType.Int:
				case OdDbType.Long:
					return "0";
				case OdDbType.Date:
				case OdDbType.DateTime:
				case OdDbType.DateTimeStamp:
				case OdDbType.TimeOfDay://causes database warning, Sets time to 00:00:01
				case OdDbType.TimeSpan://causes database warning, Sets time to 00:00:01
					return "01-01-0001";//sets date to 01 JAN 2001, 00:00:00
				case OdDbType.Text:
				case OdDbType.VarChar255:
					return "";//sets to empty string
				default:
					throw new ApplicationException("type not found");
			}
		}

		///<summary>For example, might return "NUMBER(11) NOT NULL".</summary>
		private static string GetOracleType(DbSchemaCol col) {
			switch(col.DataType) {
				case OdDbType.Bool:
					return "number(3)";
				case OdDbType.Byte:
					return "number(3)";
				case OdDbType.Currency:
					return "number(38,8)";
				case OdDbType.Date:
					return "date";
				case OdDbType.DateTime:
					return "date";
				case OdDbType.DateTimeStamp:
					//also requires trigger, trigger code is automatically created above.
					return "date";
				case OdDbType.Float:
					return "number(38,8)";
				case OdDbType.Enum:
					return "number(38,8)";
				case OdDbType.Int:
					return "number(11)";
				case OdDbType.Long:
					return "number(20)";
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
				default:
					throw new ApplicationException("type not found");
			}
		}

		///<summary>For example, might returns "0", "", or "01-JAN-0001" for cols with types OdDbType.Byte, OdDbType.Text, and OdDbType.DateTime respectively.</summary>
		private static string GetOracleBlankData(DbSchemaCol col) {
			switch(col.DataType) {
				case OdDbType.Bool:
				case OdDbType.Byte:
				case OdDbType.Currency:
				case OdDbType.Float:
				case OdDbType.Enum:
				case OdDbType.Int:
				case OdDbType.Long:
					return "0";
				case OdDbType.Date:
				case OdDbType.DateTime:
				case OdDbType.DateTimeStamp://timestamp is stored as a date and trigger combination
				case OdDbType.TimeOfDay:
					return "01-JAN-0001";
				case OdDbType.Text:
				case OdDbType.TimeSpan:
				case OdDbType.VarChar255:
					return "";//stored as NULL, 
				default:
					throw new ApplicationException("type not found");
			}
		}


	}
}
