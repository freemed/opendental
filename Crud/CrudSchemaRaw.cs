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

		///<summary>Generates C# code to add a table.</summary>
		public static string AddTable(string tableName,List<DbSchemaCol> cols,int tabInset,bool isMobile) {
			StringBuilder strb = new StringBuilder();
			List<DbSchemaCol> indexes = new List<DbSchemaCol>();
			tb="";//must reset tabs each time method is called
			for(int i=0;i<tabInset;i++) {//defines the base tabs to be added to all lines
				tb+="\t";
			}
			#region MySQL
			strb.Append(tb+"if(DataConnection.DBtype==DatabaseType.MySql) {");
			strb.Append(rn+tb+t1+"command=\"DROP TABLE IF EXISTS "+tableName+"\";");
			strb.Append(rn+tb+t1+"Db.NonQ(command);");
			strb.Append(rn+tb+t1+"command=@\"CREATE TABLE "+tableName+" (");
			for(int i=0;i<cols.Count;i++) {
				strb.Append(rn+tb+t2+cols[i].ColumnName+" "+GetMySqlType(cols[i]));
				if(GetMySqlType(cols[i]) != "timestamp"){
					strb.Append(" NOT NULL");
				}
				if(GetMySqlBlankData(cols[i])!="\"\"" && GetMySqlBlankData(cols[i])!="0" && GetMySqlType(cols[i])!="timestamp"){				
					strb.Append(" DEFAULT "+GetMySqlBlankData(cols[i]));
				}
				if(i==0 && !isMobile){
					strb.Append(" auto_increment PRIMARY KEY");
					//indexes.Add(cols[i]);//oracle needs to be changed to handle the primary key
				}
				else if(cols[i].DataType==OdDbType.Long) {//All bigints are assumed to be either keys or foreign keys.
					indexes.Add(cols[i]);//for oracle
				}
				if(i<cols.Count-1) {
					strb.Append(",");
				}
			}
			for(int i=0;i<indexes.Count;i++) {
				if(i<indexes.Count-1) {
					strb.Append(",");
				}
				strb.Append(rn+tb+t2+"INDEX("+indexes[i].ColumnName+")"+(i==indexes.Count-1?"":","));
			}
			strb.Append(rn+tb+t2+") DEFAULT CHARSET=utf8\";");
			strb.Append(rn+tb+t1+"Db.NonQ(command);");
			//for(int i=0;i<indexes.Count;i++) {
			//	strb.Append(rn+tb+t1+"command=@\"ALTER TABLE "+tableName+" ADD INDEX ("+indexes[i].ColumnName+")\";");
			//	strb.Append(rn+tb+t1+"Db.NonQ(command);");
			//}
			strb.Append(rn+tb+"}");
			if(isMobile) {
				return strb.ToString();//no oracle
			}
			#endregion
			#region Oracle
			strb.Append(rn+tb+"else {//oracle");
			strb.Append(rn+tb+t1+"command=\"BEGIN EXECUTE IMMEDIATE 'DROP TABLE "+tableName+"'; EXCEPTION WHEN OTHERS THEN NULL; END;\";");//Equivalent to "drop table if exists <table>"
			strb.Append(rn+tb+t1+"Db.NonQ(command);");
			strb.Append(rn+tb+t1+"command=@\"CREATE TABLE "+tableName+" (");
			for(int i=0;i<cols.Count;i++) {
				string tempData = GetOracleBlankData(cols[i]);//to save calls to the function, and shorten the following line of code.
				strb.Append(rn+tb+t2+cols[i].ColumnName+" "+GetOracleType(cols[i])+(tempData==null?"":(tempData=="0"?" NOT NULL":" DEFAULT "+tempData+" NOT NULL"))+",");
//        Columns are added to index from the MySQL section above. Technically the same columns should have indexes in MySql and Oracle.
//				if(cols[i].DataType==OdDbType.Long) {//all OdDbType.Long columns are assumed to be either keys or foreign keys.
//					indexes.Add(cols[i]);
//				}
			}
			strb.Append(rn+tb+t2+"CONSTRAINT "+tableName+"_"+cols[0].ColumnName+" PRIMARY KEY ("+cols[0].ColumnName+")");
			strb.Append(rn+tb+t2+")\";");
			strb.Append(rn+tb+t1+"Db.NonQ(command);");
			////Generate timestamp triggers if they need to be created.
			//for(int i=0;i<cols.Count;i++) {//check for timestamp columns
			//  if(cols[i].DataType == OdDbType.DateTimeStamp) {
			//    strb.Append(rn+tb+t1+"command=@\"CREATE OR REPLACE TRIGGER "+tableName+"_timestamp");
			//    strb.Append(rn+tb+t1+"           BEFORE UPDATE ON "+tableName);
			//    strb.Append(rn+tb+t1+"           FOR EACH ROW");
			//    strb.Append(rn+tb+t1+"           BEGIN");
			//    for(int j=0;j<cols.Count;j++) {//Each column in the table must be set up to change timestamp when changed
			//      strb.Append(rn+tb+t2+"           IF :OLD."+cols[j].ColumnName+" <> :NEW."+cols[j].ColumnName+" THEN");
			//      strb.Append(rn+tb+t2+"           :NEW."+cols[i].ColumnName+" := SYSDATE;");
			//      strb.Append(rn+tb+t2+"           END IF");
			//    }
			//    strb.Append(rn+tb+t1+"           END "+tableName+"_timestamp;\";");
			//    strb.Append(rn+tb+t1+"Db.NonQ(command);");
			//  }
			//}
			for(int i=0;i<indexes.Count;i++) {//Generate Triggers if need be
				strb.Append(rn+tb+t1+"command=@\"CREATE INDEX "+tableName+"_"+indexes[i].ColumnName+" ON "+tableName+" ("+indexes[i].ColumnName+")\";");
				strb.Append(rn+tb+t1+"Db.NonQ(command);");
			}
			strb.Append(rn+tb+"}");
			#endregion
			return strb.ToString();
		}

		/// <summary>Generates C# code to Add Column to table. List of DbSchemaCol cols should not contain the new column to be added.</summary>
		public static string AddColumnEnd(string tableName,DbSchemaCol col,int tabInset) {
			StringBuilder strb = new StringBuilder();
			tb="";//must reset tabs each time method is called
			for(int i=0;i<tabInset;i++){//defines the base tabs to be added to all lines
				tb+="\t";
			}
			strb.Append(tb+"if(DataConnection.DBtype==DatabaseType.MySql) {");
			strb.Append(rn+tb+t1+"command=\"ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetMySqlType(col)+(col.DataType==OdDbType.DateTimeStamp?"":" NOT NULL")+"\";");
//			strb.Append(rn+tb+t1+"//If ColEnd might be over 65k characters, use mediumtext");
			strb.Append(rn+tb+t1+"Db.NonQ(command);");
			if(col.DataType==OdDbType.DateTimeStamp) {//set value of new timestamp column to now()
				strb.Append(rn+tb+t1+"command=\"UPDATE "+tableName+" SET "+col.ColumnName+" = NOW()\";");
				strb.Append(rn+tb+t1+"Db.NonQ(command);");
			}
			if(col.DataType==OdDbType.Long) {//key or foreign key
				strb.Append(rn+tb+t1+"command=\"ALTER TABLE "+tableName+" ADD INDEX ("+col.ColumnName+")\";");
				strb.Append(rn+tb+t1+"Db.NonQ(command);");
			}
			strb.Append(rn+tb+"}");
			strb.Append(rn+tb+"else {//oracle");
			strb.Append(rn+tb+t1+"command=\"ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetOracleType(col)+"\";");
			strb.Append(rn+tb+t1+"Db.NonQ(command);");
			if(col.DataType==OdDbType.DateTimeStamp) {//set value of new timestamp column to SYSTIMESTAMP
				strb.Append(rn+tb+t1+"command=\"UPDATE "+tableName+" SET "+col.ColumnName+" = SYSTIMESTAMP\";");
				strb.Append(rn+tb+t1+"Db.NonQ(command);");
			}
			if(GetOracleBlankData(col)!=null) {//Do not add NOT NULL constraint because empty strings are stored as NULL in Oracle
			//Non string types must be filled with "blank" data and set to NOT NULL
				strb.Append(rn+tb+t1+"command=\"UPDATE "+tableName+" SET "+col.ColumnName+" = "+GetOracleBlankData(col)+" WHERE "+col.ColumnName+" IS NULL\";");
				strb.Append(rn+tb+t1+"Db.NonQ(command);");
				strb.Append(rn+tb+t1+"command=\"ALTER TABLE "+tableName+" MODIFY "+col.ColumnName+" NOT NULL\";");
				strb.Append(rn+tb+t1+"Db.NonQ(command);");
				if(col.DataType==OdDbType.Long) {//key or foreign key
					strb.Append(rn+tb+t1+"command=@\"CREATE INDEX "+tableName+"_"+col.ColumnName+" ON "+tableName+" ("+col.ColumnName+")\";");
					strb.Append(rn+tb+t1+"Db.NonQ(command);");
				}
			}
			//if(cols != null) {//this should be removed once the nulls have been removed from the function calls.
			//  cols.Add(col);
			//  for(int i=0;i<cols.Count;i++) {//check for timestamp columns
			//    if(cols[i].DataType == OdDbType.DateTimeStamp) {
			//      strb.Append(rn+tb+t1+"command=@\"CREATE OR REPLACE TRIGGER "+tableName+"_timestamp");
			//      strb.Append(rn+tb+t1+"           BEFORE UPDATE ON "+tableName);
			//      strb.Append(rn+tb+t1+"           FOR EACH ROW");
			//      strb.Append(rn+tb+t1+"           BEGIN");
			//      for(int j=0;j<cols.Count;j++) {//Each column in the table must be set up to change timestamp when changed
			//        strb.Append(rn+tb+t2+"           IF :OLD."+cols[j].ColumnName+" <> :NEW."+cols[j].ColumnName+" THEN");
			//        strb.Append(rn+tb+t2+"           :NEW."+cols[i].ColumnName+" := SYSDATE;");
			//        strb.Append(rn+tb+t2+"           END IF");
			//      }
			//      strb.Append(rn+tb+t1+"           END "+tableName+"_timestamp;\";");
			//      strb.Append(rn+tb+t1+"Db.NonQ(command);");
			//    }
			//  }
			//}
			strb.Append(rn+tb+"}");
			return strb.ToString();
		}

		///<summary></summary>
		public static string AddIndex(string tableName,string colName,int tabInset) {
			StringBuilder strb = new StringBuilder();
			tb="";//must reset tabs each time method is called
			for(int i=0;i<tabInset;i++) {//defines the base tabs to be added to all lines
				tb+="\t";
			}
			strb.Append(tb+"if(DataConnection.DBtype==DatabaseType.MySql) {");
			strb.Append(rn+tb+t1+"command=\"ALTER TABLE "+tableName+" ADD INDEX("+colName+")\";");
			strb.Append(rn+tb+t1+"Db.NonQ(command);");
			strb.Append(rn+tb+"}");
			strb.Append(rn+tb+"else {//oracle");
			strb.Append(rn+tb+t1+"command=\"CREATE INDEX "+tableName+"_"+colName+" ON "+tableName+" ("+colName+")\";");
			strb.Append(rn+tb+t1+"Db.NonQ(command);");
			strb.Append(rn+tb+"}");
			return strb.ToString();
		}

		///<summary>Does not work for Timestamp because of Oracle triggers.</summary>
		public static string DropColumn(string tableName,string colName,int tabInset) {
			StringBuilder strb = new StringBuilder();
			tb="";//must reset tabs each time method is called
			for(int i=0;i<tabInset;i++) {//defines the base tabs to be added to all lines
				tb+="\t";
			}
			strb.Append(tb+"if(DataConnection.DBtype==DatabaseType.MySql) {");
			strb.Append(rn+tb+t1+"command=\"ALTER TABLE "+tableName+" DROP COLUMN "+colName+"\";");
			strb.Append(rn+tb+t1+"Db.NonQ(command);");
			strb.Append(rn+tb+"}");
			strb.Append(rn+tb+"else {//oracle");
			strb.Append(rn+tb+t1+"command=\"ALTER TABLE "+tableName+" DROP COLUMN "+colName+"\";");
			strb.Append(rn+tb+t1+"Db.NonQ(command);");
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
					return "'0001-01-01'";//sets date to 01 JAN 2001, 00:00:00
				case OdDbType.DateTimeStamp:
					return "NOW()";
				case OdDbType.DateTime:
					return "'0001-01-01 00:00:00'";
				case OdDbType.TimeOfDay:
				case OdDbType.TimeSpan:
					return "'00:00:00'";
				case OdDbType.Text:
				case OdDbType.VarChar255:
					return "\"\"";//sets to empty string
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
					return "timestamp";
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
				case OdDbType.TimeOfDay:
					return "TO_DATE('0001-01-01','YYYY-MM-DD')";
				case OdDbType.DateTimeStamp://timestamp is stored as a date and trigger combination
					return null;
				case OdDbType.Text:
				case OdDbType.TimeSpan:
				case OdDbType.VarChar255:
					return null;//stored as NULL, 
				default:
					throw new ApplicationException("type not found");
			}
		}

		/////<summary>Rebuilds timestamp triggers for Oracle timestamps.</summary>
		//private static string TimeStampTriggerBuilderOracle(string tableName,List<DbSchemaCol> cols,int tabInset) {
		//  StringBuilder strb = new StringBuilder();
		//  tb="";//must reset tabs each time method is called
		//  for(int i=0;i<tabInset;i++) {//defines the base tabs to be added to all lines
		//    tb+="\t";
		//  }
		//  if(DataConnection.DBtype==DatabaseType.Oracle) {
		//    for(int i=0;i<cols.Count;i++) {//check for timestamp columns
		//      if(cols[i].DataType == OdDbType.DateTimeStamp) {
		//        strb.Append(rn+tb+t1+"command=@\"CREATE OR REPLACE TRIGGER "+tableName+"_timestamp");
		//        strb.Append(rn+tb+t1+"           BEFORE UPDATE ON "+tableName);
		//        strb.Append(rn+tb+t1+"           FOR EACH ROW");
		//        strb.Append(rn+tb+t1+"           BEGIN");
		//        for(int j=0;j<cols.Count;j++) {//Each column in the table must be set up to change timestamp when changed
		//          strb.Append(rn+tb+t2+"           IF :OLD."+cols[j].ColumnName+" <> :NEW."+cols[j].ColumnName+" THEN");
		//          strb.Append(rn+tb+t2+"           :NEW."+cols[i].ColumnName+" := SYSDATE;");
		//          strb.Append(rn+tb+t2+"           END IF");
		//        }
		//        strb.Append(rn+tb+t1+"           END "+tableName+"_timestamp;\";");
		//        strb.Append(rn+tb+t1+"Db.NonQ(command);");
		//      }
		//    }
		//  }
		//  return strb.ToString();
		//}


	}
}
