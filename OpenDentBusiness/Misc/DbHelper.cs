using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	///<summary>This class contains methods used to generate database independent SQL.</summary>
	public class DbHelper {

		///<summary>Uses LIMIT for MySQL databases and RowNum less than or equal for Oracle. When used with GROUP BY, GROUP BY must be in a subquery.</summary>
		public static string Limit(int n) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "RowNum <= " + n;
			}
			else {
				return "LIMIT " + n;
			}
		}

		///<summary>Takes an SQL string including a group by and modifies the query to return only the top n rows independent of the database.</summary>
		public static string LimitGroupBy(string query, int n) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "SELECT * FROM (" + query + ") RowNum <= " + n;
			}
			else {
				return query + " LIMIT " + n;
			}
		}


	}
}
