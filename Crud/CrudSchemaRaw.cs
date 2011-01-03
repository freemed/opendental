using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace Crud {
	///<summary>This is the class that actually generates snippets of raw schema code.</summary>
	public class CrudSchemaRaw {
		private const string rn="\r\n";
		private const string t="\t";
		private const string t2="\t\t";
		private const string t3="\t\t\t";
		private const string t4="\t\t\t\t";
		private const string t5="\t\t\t\t\t";

		/// <summary></summary>
		public static string AddColumnEnd(string tableName,DbSchemaCol col) {
			//After the rewrite, this will return C# with queries in it instead of actually running them here.
			



			/*
			string command = "";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command = "ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetMySqlType7_7(col)" NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command = "ALTER TABLE "+tableName+" ADD "+col.ColumnName+" "+GetOracleType7_7(col);
				Db.NonQ(command);
				if(getBlankOracleData(col)!="''") {//only set column to NOT NULL if it is not a string type column.
					command = "UPDATE TABLE "+tableName+" SET "+col.ColumnName+" = "+getBlankOracleData(col)+" WHERE "+col.ColumnName+" IS NULL";//fills column with 'blank' data
					Db.NonQ(command);
					command = "ALTER TABLE "+tableName+" MODIFY "+col.ColumnName+" NOT NULL";
					try {
						Db.NonQ(command);
					}
					catch(Exception e) {
						//fail silently. If this fails that means that the column is already set to NOT NULL Which should theoretically never be the case.
					}
				}
			}*/
			return "";
		}
	}
}
