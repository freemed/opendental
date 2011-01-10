using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	///<summary>This class contains methods used to generate database independent SQL.</summary>
	public class DbHelper {

		///<summary>Use when you already have a WHERE clause in the query. Uses AND RowNum... for Oracle.</summary>
		public static string LimitAnd(int n) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "AND RowNum <= " + n;
			}
			else {
				return "LIMIT " + n;
			}
		}

		///<summary>Use when you do not otherwise have a WHERE clause in the query. Uses WHERE RowNum... for Oracle.</summary>
		public static string LimitWhere(int n) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "WHERE RowNum <= " + n;
			}
			else {
				return "LIMIT " + n;
			}
		}

		///<summary>Use when there is an ORDER BY clause in the query. Uses RowNum... for Oracle.</summary>
		public static string LimitOrderBy(string query,int n) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "SELECT * FROM (" + query + ") WHERE RowNum <= " + n;
			}
			else {
				return query + " LIMIT " + n;
			}
		}

		/// <summary>If passing in a literal, surround with single quotes first.</summary>
		public static string Concat(params string[] values) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				string result="(";
				for(int i=0;i<values.Length;i++) {
					if(i!=0) {
						result+=" || ";
					}
					result+=values[i];
				}
				result+=")";
				return result;
			}
			else {
				string result="CONCAT(";
				for(int i=0;i<values.Length; i++) {
					if(i!=0) {
						result+=",";
					}
					result+=values[i];
				}
				result+=")";
				return result;
			}
		}

		///<summary>Specify column for equivalent of "GROUP_CONCAT(column)" in MySQL.</summary>
		public static string GroupConcat(string column) {
			return GroupConcat(column,false);
		}

		///<summary>Specify column for equivalent of "GROUP_CONCAT(column)" in MySQL. Adds DISTINCT in MySQL if specified.</summary>
		public static string GroupConcat(string column,bool distinct) {
			return GroupConcat(column,distinct,false);
		}

		///<summary>Specify column for equivalent of "GROUP_CONCAT(column)" in MySQL. Adds DISTINCT (MySQL only) and ORDERBY as specified.</summary>
		public static string GroupConcat(string column,bool distinct,bool orderby) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				if(orderby) {
					return "RTRIM(REPLACE(REPLACE(XMLAgg(XMLElement(\"x\","+column+") ORDER BY "+column+"),'<x>'),'</x>',','))";
				}
				else {
					return "RTRIM(REPLACE(REPLACE(XMLAgg(XMLElement(\"x\","+column+")),'<x>'),'</x>',','))";
				}//Distinct ignored for Oracle case.
			}
			else {
				if(distinct && orderby) {
					return "GROUP_CONCAT(DISTINCT "+column+" ORDER BY "+column+")";
				}
				if(distinct &&  !orderby) {
					return "GROUP_CONCAT(DISTINCT "+column+")";
				}
				if(!distinct && orderby) {
					return "GROUP_CONCAT("+column+" ORDER BY "+column+")";
				}
				else {
					return "GROUP_CONCAT("+column+")";
				}
			}
		}

		///<summary>In Oracle, union order by combos can only use ordinals and not column names. Values for ordinal start at 1.</summary>
		public static string UnionOrderBy(string colName,int ordinal) {
			//Using POut doesn't name sense for column names or ordinal numbers because they are not values they are part of the query.
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return ordinal.ToString();
			}
			return colName;
		}

		public static string DateAddDay(string date,string days) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return date+" +"+days;//Can handle negatives even with '+' hardcoded.
			}
			return "ADDDATE("+date+","+days+")";
		}

		public static string DateAddMonth(string date,string months) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "ADD_MONTHS("+date+","+months+")";
			}
			return "ADDDATE("+date+",INTERVAL "+months+" MONTH)";
		}

		public static string DateAddYear(string date,string years) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "ADD_MONTHS("+date+","+years+"*12)";
			}
			return "ADDDATE("+date+",INTERVAL "+years+" YEAR)";
		}

		public static string DateColumn(string colName) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "TO_DATE("+colName+")";
			}
			return "DATE("+colName+")";
		}

		///<summary>The format must be the MySQL format. The following formats are currently acceptable as input: %c/%d/%Y , %m/%d/%Y</summary>
		public static string DateFormatColumn(string colName,string format) {
			//MySQL DATE_FORMAT() reference: http://dev.mysql.com/doc/refman/5.0/en/date-and-time-functions.html#function_date-format
			//Oracle TO_CHAR() reference: http://download.oracle.com/docs/cd/B19306_01/server.102/b14200/sql_elements004.htm#i34510
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				if(format=="%c/%d/%Y") {
					return "TO_CHAR("+colName+",'MM/DD/YYYY')";//Sadly, not exactly the same but closest option.
				}
				else if(format=="%m/%d/%Y") {
					return "TO_CHAR("+colName+",'MM/DD/YYYY')";//Sadly, not exactly the same but closest option.
				}
				throw new Exception("Unrecognized date format string.");
			}
			return "DATE_FORMAT("+colName+",'"+format+"')";
		}

		/* Not used
		///<summary>Helper for Oracle that will return equivalent of MySql CURTIME().</summary>
		public static string Curtime() {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "SYSDATE";
			}
			return "CURTIME()";
		}*/

		///<summary>Helper for Oracle that will return equivalent of MySql CURDATE()</summary>
		public static string Curdate() {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				//return "(SELECT TO_CHAR(SYSDATE,'YYYY-MM-DD') FROM DUAL)";
				return "SYSDATE";
			}
			return "CURDATE()";
		}

		///<summary>Helper for Oracle that will return equivalent of MySql NOW()</summary>
		public static string Now() {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				//return "(SELECT TO_CHAR(SYSDATE,'YYYY-MM-DD HH24:MI:SS') FROM DUAL)";
				return "SYSDATE";
			}
			return "NOW()";
		}

		///<summary>Helper for Oracle that will return equivalent of MySql YEAR()</summary>
		public static string Year(string date) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "CAST(TO_CHAR("+date+",'YYYY') AS NUMBER)";
			}
			return "YEAR("+date+")";
		}

		public static string Regexp(string input,string pattern) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "REGEXP_INSTR("+input+",'"+pattern+"')";
			}
			return input+" REGEXP '"+pattern+"'";
		}

		///<summary>Gets the database specific character used for parameters.  For example, : or @.</summary>
		public static string ParamChar {
			get {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					return ":";
				}
				return "@";
			}
		}

		///<summary>Gets the maximum value for the specified field within the specified table. This key will always be the MAX(field)+1 and will usually be the correct key to use for new inserts, but not always.</summary>
		public static long GetNextOracleKey(string tablename,string field) {
			//When inserting a new record with the key value returned by this function, these are some possible errors that can occur. 
			//The actual error text starts after the ... on each line. Note especially the duplicate key exception, as this exception 
			//must be considered by the insertion algorithm:
			//DUPLICATE PRIMARY KEY....ORA-00001: unique constraint (DEV77.PRIMARY_87) violated
			//MISSING WHOLE TABLE......ORA-00942: table or view does not exist
			//MISSING TABLE COLUMN.....ORA-00904: "ITEMORDER": invalid identifier
			//MISSING OPENING PAREND...ORA-00926: missing VALUES keyword
			//CONNECTION LOST..........ORA-03113: end-of-file on communication channel
			string command="SELECT MAX("+field+")+1 FROM "+tablename;
			long retval=PIn.Long(Db.GetCount(command));
			if(retval==0) {//Happens when the table has no records
				return 1;
			}
			return retval;
		}

	}
}
