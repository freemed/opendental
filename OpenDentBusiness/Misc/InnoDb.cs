using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace OpenDentBusiness {
	public class InnoDb {

		/// <summary>Returns the default storage engine.</summary>
		public static string GetDefaultEngine() {
			string command="SELECT @@default_storage_engine";
			string defaultengine=Db.GetScalar(command).ToString();
			return defaultengine;
		}

		public static bool IsInnodbAvail() {
			string command="SELECT @@have_innodb";
			string innoDbOn=Db.GetScalar(command).ToString();
			return innoDbOn=="YES";
		}

		/// <summary>Returns the number of MyISAM tables and the number of InnoDB tables in the current database.</summary>
		public static string GetEngineCount() {
			string command=@"SELECT SUM(CASE WHEN information_schema.tables.engine='MyISAM' THEN 1 ELSE 0 END) AS 'myisam',
				SUM(CASE WHEN information_schema.tables.engine='InnoDB' THEN 1 ELSE 0 END) AS 'innodb'
				FROM information_schema.tables
				WHERE table_schema=(SELECT DATABASE())";
			DataTable results=Db.GetTable(command);
			string retval=Lans.g("FormInnoDb","Number of MyISAM tables: ");
			retval+=Lans.g("FormInnoDb",results.Rows[0]["myisam"].ToString())+"\r\n";
			retval+=Lans.g("FormInnoDb","Number of InnoDB tables: ");
			retval+=Lans.g("FormInnoDb",results.Rows[0]["innodb"].ToString())+"\r\n";
			return retval;
		}

		///<summary>The only allowed parameters are "InnoDB" or "MyISAM".  Converts tables to toEngine type and returns the number of tables converted.</summary>
		public static int ConvertTables(string fromEngine,string toEngine) {
			int numtables=0;
			string command="SELECT DATABASE()";
			string database=Db.GetScalar(command);
			command=@"SELECT table_name
				FROM information_schema.tables
				WHERE table_schema='"+database+"' AND information_schema.tables.engine='"+fromEngine+"'";
			DataTable results=Db.GetTable(command);
			command="";
			if(results.Rows.Count==0) {
				return numtables;
			}
			for(int i=0;i<results.Rows.Count;i++) {
				command+="ALTER TABLE "+database+"."+POut.String(results.Rows[i]["table_name"].ToString())+" ENGINE='"+toEngine+"'; ";
				numtables++;
			}
			Db.NonQ(command);
			return numtables;
		}
	}
}
