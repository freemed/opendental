using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace OpenDentBusiness{
	///<summary></summary>
	public class WikiLists{
				
		/*We will probably add this back in later. (TranslateToHTML)
		/// <summary>Returns an html formatted table generated from a "SELECT * FROM wikiList_&lt;tablename&gt;" query.</summary>
		public static string TranslateToHTML(string tableName) {
			string command = "SELECT * FROM wikiList_"+tableName;//TODO: userOD table used just for testing purposes.
			DataTable Table = Db.GetTable(command);
			StringBuilder TableBuilder = new StringBuilder();
			TableBuilder.AppendLine("\t<table>");
					TableBuilder.AppendLine("\t\t<tr>");
					TableBuilder.AppendLine("\t\t\t<td align=\"center\" colspan=\""+Table.Columns.Count+"\">");
					TableBuilder.AppendLine("\t\t\t<h3>List : "+tableName+"</h3>");
					TableBuilder.AppendLine("\t\t\t</td>");
					TableBuilder.AppendLine("\t\t</tr>");
				TableBuilder.AppendLine("\t\t<tr>");
				foreach(DataColumn col in Table.Columns){
					TableBuilder.AppendLine("\t\t\t<th>");
					TableBuilder.AppendLine("\t\t\t\t<b>"+col.ColumnName+"</b>");
					TableBuilder.AppendLine("\t\t\t</th>");
				}
				TableBuilder.AppendLine("\t\t</tr>");
			//TODO: table headers
			foreach(DataRow row in Table.Rows) {
				TableBuilder.AppendLine("\t\t<tr>");
				foreach(object cell in row.ItemArray) {
					TableBuilder.AppendLine("\t\t\t<td>");
					TableBuilder.AppendLine("\t\t\t\t"+cell.ToString());
					TableBuilder.AppendLine("\t\t\t</td>");
				}
				TableBuilder.AppendLine("\t\t</tr>");
			}
			TableBuilder.AppendLine("\t</table>");
			return TableBuilder.ToString();
		}*/

		public static bool CheckExists(string listName) {
			string command = "SHOW TABLES LIKE 'wikilist\\_"+listName+"'";
			if(Db.GetTable(command).Rows.Count==1) {
				//found exacty one table with that name
				return true;
			}
			//no table found with that name
			return false;
		}

		public static DataTable GetByName(string listName) {
			DataTable tableDescript = Db.GetTable("DESCRIBE wikilist_"+listName);
			string orderBy;
			switch(tableDescript.Rows.Count) {
				case 0:
					orderBy="";//no order by
					break;
				case 1:
					orderBy=" ORDER BY 1";//order by PK
					break;
				default:
					orderBy=" ORDER BY 2,1";//order by second column and then PK
					break;
			}
			string command = "SELECT * FROM wikilist_"+listName+orderBy;

			return Db.GetTable(command);
		}

		/// <summary>Creates empty table with a single column for PK. List name must be formatted correctly before being passed here. Ie. no spaces, all lowercase.</summary>
		public static void CreateNewWikiList(string listName) {
			//TODO: should we recheck/check that the list name is properly formed.
			string command = "CREATE TABLE wikilist_"+PIn.String(listName)+" ("+PIn.String(listName)+"Num BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY )";
			Db.NonQ(command);
		}

		///<summary>Column is automatically named "Column#" where # is the number of columns+1.</summary>
		public static void AddColumn(string listName) {
			//Find Valid column name-----------------------------------------------------------------------------------------
			DataTable columnNames = Db.GetTable("DESCRIBE wikilist_"+listName);
			string newColumnName="Column1";//default in case table has no columns. Should never happen.
			for(int i=0;i<columnNames.Rows.Count+1;i++) {//+1 to guarantee we can find a valid name.
				newColumnName="Column"+(1+i);//ie. Column1, Column2, Column3...
				foreach(DataRow row in columnNames.Rows) {
					if(newColumnName==row.ItemArray[0].ToString()) {
						newColumnName="";
						break;
					}
				}
				if(newColumnName!="") {
					break;//found a valid name.
				}
			}
			if(newColumnName=="") {
				//should never happen.
				throw new ApplicationException("Could not create valid column name.");
			}
			//Add new column name--------------------------------------------------------------------------------------------
			string newColName = "Column"+columnNames.Rows.Count;//i.e. Column1
			string command = "ALTER TABLE wikilist_"+listName+" ADD COLUMN Column"+Db.GetTable("DESCRIBE wikilist_"+listName).Rows.Count+" TEXT";
			Db.NonQ(command);
		}

		///<summary>Column is automatically named "Column#" where # is the number of columns+1.</summary>
		public static void AddItem(string listName) {
			string command = "INSERT INTO wikilist_"+listName+" VALUES ()";//inserts empty row with auto generated PK.
			Db.NonQ(command);
		}

		/// <summary></summary>
		/// <param name="listName"></param>
		/// <param name="Item">Should be a DataTable object with a single DataRow containing the item.</param>
		public static void UpdateItem(string listName, DataTable ItemTable) {
			if(ItemTable.Columns.Count<2) {
				//if the table contains only a PK column.
				return;
			}
			string command = "UPDATE wikilist_"+listName+" SET ";//inserts empty row with auto generated PK.
			for(int i=1;i<ItemTable.Columns.Count;i++) {//start at 1 because we do not need to update the PK
				command+=ItemTable.Columns[i].ColumnName+"='"+ItemTable.Rows[0][i].ToString()+"',";
			}
			command=command.Trim(',')+" WHERE "+listName+"Num = "+ItemTable.Rows[0][0];
			Db.NonQ(command);
		}

		public static DataTable GetItem(string listName,long itemNum) {
			DataTable retVal = new DataTable();
			string command = "SELECT * FROM wikilist_"+listName+" WHERE "+listName+"Num = "+itemNum;
			retVal=Db.GetTable(command);
			return retVal;
		}

		public static void DeleteItem(string listName,long itemNum) {
			string command = "DELETE FROM wikilist_"+listName+" WHERE "+listName+"Num = "+itemNum;
			Db.NonQ(command);
		}

		/////<summary>Lazy method, possibly dangerous to list data, drops table if exists and re-inserts. This takes care of all new and/or rearranged columns and values.
		/////Column Names should already be formatted in the Data Table.</summary>
		//public static void UpdateList(string listName, DataTable listData) {
		//  //Drop existing Table-------------------------------------------------------------------------------------
		//  string command = "DROP TABLE IF EXISTS wikilist_"+listName;
		//  Db.NonQ(command);
		//  //Drop Re-Define Table------------------------------------------------------------------------------------
		//  command = "CREATE TABLE wikilist_"+PIn.String(listName)+" (";
		//  for(int i=0;i<listData.Columns.Count;i++) {//string columnName in columnNames) {
		//    if(i==0) {//First Column is ALWAYS primary key, or at least should be.
		//      command+=PIn.String(listData.Columns[i].ColumnName)+" BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY ";//capitalize PK
		//      continue;
		//    }
		//    command+=", "+PIn.String(listData.Columns[i].ColumnName)+" TEXT";//Capitalize each column name.
		//  }
		//  command+=")";
		//  Db.NonQ(command);
		//  //Insert New Data-------------------------------------------------------------------------------------
		//  //"INSERT INTO <tablename> (<colNames>) VALUES (<data>);
		//  string commandBase="INSERT INTO wikilist_"+listName+" (";
		//  foreach(DataColumn column in listData.Columns) {
		//    commandBase+=column.ColumnName+",";
		//  }
		//  commandBase=commandBase.Trim(',')+ ") VALUES (";
		//  foreach(DataRow row in listData.Rows) {
		//    command=commandBase;
		//    foreach(object item in row.ItemArray) {
		//      command+="'"+item.ToString()+"', ";
		//    }
		//    command=command.Trim(',')+")";
		//    Db.NonQ(command);
		//  }
		//  //done
		//}

		public static List<string> GetAllLists() {
			List<string> retVal = new List<string>();
			string command = "SHOW TABLES LIKE 'wikilist\\_%'";
			DataTable Table = Db.GetTable(command);
			foreach(DataRow row in Table.Rows) {
				retVal.Add(row[0].ToString());
			}
			return retVal;
		}

	}
}