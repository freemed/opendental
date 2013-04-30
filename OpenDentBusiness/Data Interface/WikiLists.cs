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
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),listName);
			}
			string command = "SHOW TABLES LIKE 'wikilist\\_"+POut.String(listName)+"'";
			if(Db.GetTable(command).Rows.Count==1) {
				//found exacty one table with that name
				return true;
			}
			//no table found with that name
			return false;
		}

		public static DataTable GetByName(string listName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),listName);
			}
			DataTable tableDescript = Db.GetTable("DESCRIBE wikilist_"+POut.String(listName));
			string command = "SELECT * FROM wikilist_"+POut.String(listName);
			switch(tableDescript.Rows.Count) {
				case 0://should never happen
					break;
				case 1:
					command+=" ORDER BY "+POut.String(listName)+"Num";//order by PK
					break;
				default://order by the second column, even though we show the primary key
					command+=" ORDER BY "+tableDescript.Rows[1]["Field"];
					break;
			}
			return Db.GetTable(command);
		}

		/// <summary>Creates empty table with a single column for PK. List name must be formatted correctly before being passed here. Ie. no spaces, all lowercase.</summary>
		public static void CreateNewWikiList(string listName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listName);
				return;
			}
			//listname is checked in the UI for proper format.
			string command = "CREATE TABLE wikilist_"+POut.String(listName)+" ("+POut.String(listName)+"Num bigint NOT NULL auto_increment PRIMARY KEY ) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			WikiListHeaderWidth headerWidth = new WikiListHeaderWidth();
			headerWidth.ColName=listName+"Num";
			headerWidth.ColWidth=100;
			headerWidth.ListName=listName;
			WikiListHeaderWidths.InsertNew(headerWidth);
		}

		///<summary>Column is automatically named "Column#" where # is the number of columns+1.</summary>
		public static void AddColumn(string listName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listName);
				return;
			}
			//Find Valid column name-----------------------------------------------------------------------------------------
			DataTable columnNames = Db.GetTable("DESCRIBE wikilist_"+POut.String(listName));
			string newColumnName="Column1";//default in case table has no columns. Should never happen.
			for(int i=0;i<columnNames.Rows.Count+1;i++) {//+1 to guarantee we can find a valid name.
				newColumnName="Column"+(1+i);//ie. Column1, Column2, Column3...
				for(int j=0;j<columnNames.Rows.Count;j++) {
					if(newColumnName==columnNames.Rows[j]["Field"].ToString()) {
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
			string command = "ALTER TABLE wikilist_"+POut.String(listName)+" ADD COLUMN "+POut.String(newColumnName)+" TEXT NOT NULL";
			Db.NonQ(command);
			//Add column widths to wikiListHeaderWidth Table-----------------------------------------------------------------
			WikiListHeaderWidth headerWidth = new WikiListHeaderWidth();
			headerWidth.ColName=newColumnName;
			headerWidth.ListName=listName;
			headerWidth.ColWidth=100;
			WikiListHeaderWidths.InsertNew(headerWidth);
		}

		///<summary>Check to see if column can be deleted, returns true is the column contains only nulls.</summary>
		public static bool CheckColumnEmpty(string listName,string colName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),listName,colName);
			}
			string command = "SELECT COUNT(*) FROM wikilist_"+POut.String(listName)+" WHERE "+POut.String(colName)+"!=''";
			return Db.GetCount(command).Equals("0");
		}

		///<summary>Check to see if column can be deleted, returns true is the column contains only nulls.</summary>
		public static void DeleteColumn(string listName,string colName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetBool(MethodBase.GetCurrentMethod(),listName,colName);
				return;
			}
			string command = "ALTER TABLE wikilist_"+POut.String(listName)+" DROP "+POut.String(colName);
			Db.NonQ(command);
			WikiListHeaderWidths.Delete(listName,colName);
		}

		/// <summary>Shifts the column to the left, does nothing if trying to shift leftmost two columns.</summary>
		public static void ShiftColumnLeft(string listName,string colName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetBool(MethodBase.GetCurrentMethod(),listName,colName);
				return;
			}
			DataTable columnNames = Db.GetTable("DESCRIBE wikilist_"+POut.String(listName));
			if(columnNames.Rows.Count<3) {
				//not enough columns to reorder.
				return;
			}
			if(colName==columnNames.Rows[0][0].ToString()
			|| colName==columnNames.Rows[1][0].ToString()) {
				//trying to re-order PK or first column. 
				//No need to return here, but also no need to continue.
				return;
			}
			string command="";
			for(int i=2;i<columnNames.Rows.Count;i++) {
				if(columnNames.Rows[i][0].ToString()==colName) {
					command = "ALTER TABLE wikilist_"+POut.String(listName)+" MODIFY "+POut.String(colName)+" TEXT NOT NULL AFTER "+POut.String(columnNames.Rows[i-2][0].ToString());
					Db.NonQ(command);
					return;
				}
			}
			//no column found. Should never reach this location.
		}

		/// <summary>Shifts the column to the right, does nothing if trying to shift the rightmost column.</summary>
		public static void ShiftColumnRight(string listName,string colName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetBool(MethodBase.GetCurrentMethod(),listName,colName);
				return;
			}
			DataTable columnNames = Db.GetTable("DESCRIBE wikilist_"+POut.String(listName));
			if(columnNames.Rows.Count<3) {
				//not enough columns to reorder.
				return;
			}
			if(colName==columnNames.Rows[0][0].ToString()	//don't shift the PK
			|| colName==columnNames.Rows[columnNames.Rows.Count-1][0].ToString()) { //don't shift the last column
				//No need to return here, but also no need to continue.
				return;
			}
			string command="";
			for(int i=1;i<columnNames.Rows.Count-1;i++) {
				if(columnNames.Rows[i][0].ToString()==colName) {
					command = "ALTER TABLE wikilist_"+POut.String(listName)+" MODIFY "+POut.String(colName)+" TEXT NOT NULL AFTER "+POut.String(columnNames.Rows[i+1][0].ToString());
					Db.NonQ(command);
					return;
				}
			}
			//no column found. Should never reach this location.
		}

		///<summary>Column is automatically named "Column#" where # is the number of columns+1.</summary>
		public static void AddItem(string listName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listName);
				return;
			}
			string command = "INSERT INTO wikilist_"+POut.String(listName)+" VALUES ()";//inserts empty row with auto generated PK.
			Db.NonQ(command);
		}

		/// <summary></summary>
		/// <param name="ItemTable">Should be a DataTable object with a single DataRow containing the item.</param>
		public static void UpdateItem(string listName,DataTable ItemTable) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listName,ItemTable);
				return;
			}
			if(ItemTable.Columns.Count<2) {
				//if the table contains only a PK column.
				return;
			}
			string command = "UPDATE wikilist_"+POut.String(listName)+" SET ";//inserts empty row with auto generated PK.
			for(int i=1;i<ItemTable.Columns.Count;i++) {//start at 1 because we do not need to update the PK
				command+=POut.String(ItemTable.Columns[i].ColumnName)+"='"+POut.String(ItemTable.Rows[0][i].ToString())+"',";
			}
			command=command.Trim(',')+" WHERE "+POut.String(listName)+"Num = "+ItemTable.Rows[0][0];
			Db.NonQ(command);
		}

		public static DataTable GetItem(string listName,long itemNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),listName,itemNum);
			}
			DataTable retVal = new DataTable();
			string command = "SELECT * FROM wikilist_"+POut.String(listName)+" WHERE "+POut.String(listName)+"Num = "+POut.Long(itemNum);
			retVal=Db.GetTable(command);
			return retVal;
		}

		public static void DeleteItem(string listName,long itemNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listName,itemNum);
				return;
			}
			string command = "DELETE FROM wikilist_"+POut.String(listName)+" WHERE "+POut.String(listName)+"Num = "+POut.Long(itemNum);
			Db.NonQ(command);
		}

		public static void DeleteList(string listName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listName);
				return;
			}
			string command = "DROP TABLE wikilist_"+POut.String(listName);
			Db.NonQ(command);
			WikiListHeaderWidths.DeleteForList(listName);
		}

		public static List<string> GetAllLists() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod());
			}
			List<string> retVal = new List<string>();
			string command = "SHOW TABLES LIKE 'wikilist\\_%'";//must escape _ (underscore) otherwise it is interpreted as a wildcard character.
			DataTable Table = Db.GetTable(command);
			foreach(DataRow row in Table.Rows) {
				retVal.Add(row[0].ToString());
			}
			return retVal;
		}

	}
}