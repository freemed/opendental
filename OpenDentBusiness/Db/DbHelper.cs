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

	}
}
