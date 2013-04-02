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
			string command = "SHOW TABLES LIKE 'wikilist\\_"+listName+"'";
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
			DataTable tableDescript = Db.GetTable("DESCRIBE wikilist_"+listName);
			string command = "SELECT * FROM wikilist_"+listName;
			switch(tableDescript.Rows.Count) {
				case 0://should never happen
					break;
				case 1:
					command+=" ORDER BY "+listName+"Num";//order by PK
					break;
				default:
					command+=" ORDER BY "+tableDescript.Rows[1].ItemArray[0];
					//Order by the second column and then the PK. Also, reorder list so that empty items go on the bottom.
					//command+=" WHERE "+tableDescript.Rows[1].ItemArray[0]+" IS NOT NULL ORDER BY "+tableDescript.Rows[1].ItemArray[0]+","+listName+"Num) "
					//  +"UNION "
					//  +"(SELECT * FROM wikilist_"+listName
					//  +" WHERE "+tableDescript.Rows[1].ItemArray[0]+" IS NULL ORDER BY "+tableDescript.Rows[1].ItemArray[0]+","+listName+"Num)";
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
			//TODO: should we recheck/check that the list name is properly formed.
			string command = "CREATE TABLE wikilist_"+PIn.String(listName)+" ("+PIn.String(listName)+"Num BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY )";
			Db.NonQ(command);
			WikiListHeaderWidth newPKCol = new WikiListHeaderWidth();
			newPKCol.ColName=PIn.String(listName)+"Num";
			newPKCol.ColWidth=100;
			newPKCol.ListName=listName;
			WikiListHeaderWidths.InsertNew(newPKCol);
		}

		///<summary>Column is automatically named "Column#" where # is the number of columns+1.</summary>
		public static void AddColumn(string listName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listName);
				return;
			}
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
			string command = "ALTER TABLE wikilist_"+listName+" ADD COLUMN "+newColumnName+" TEXT NOT NULL";
			Db.NonQ(command);
			//Add column widths to wikiListHeaderWidth Table-----------------------------------------------------------------
			WikiListHeaderWidth newWidth = new WikiListHeaderWidth();
			newWidth.ColName=newColumnName;
			newWidth.ListName=listName;
			newWidth.ColWidth=100;
			WikiListHeaderWidths.InsertNew(newWidth);
		}

		///<summary>Check to see if column can be deleted, returns true is the column contains only nulls.</summary>
		public static bool CheckColumnEmpty(string listName,string colName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),listName,colName);
			}
			string command = "SELECT COUNT(*) FROM wikilist_"+listName+" WHERE "+colName+"!=''";
			return Db.GetCount(command).Equals("0");
		}

		///<summary>Check to see if column can be deleted, returns true is the column contains only nulls.</summary>
		public static void DeleteColumn(string listName,string colName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetBool(MethodBase.GetCurrentMethod(),listName,colName);
				return;
			}
			string command = "ALTER TABLE wikilist_"+listName+" DROP "+colName;
			Db.NonQ(command);
			WikiListHeaderWidths.Delete(listName,colName);
		}

		///<summary>Column is automatically named "Column#" where # is the number of columns+1.</summary>
		public static void AddItem(string listName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listName);
				return;
			}
			string command = "INSERT INTO wikilist_"+listName+" VALUES ()";//inserts empty row with auto generated PK.
			Db.NonQ(command);
		}

		/// <summary></summary>
		/// <param name="Item">Should be a DataTable object with a single DataRow containing the item.</param>
		public static void UpdateItem(string listName,DataTable ItemTable) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listName,ItemTable);
				return;
			}
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
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),listName,itemNum);
			}
			DataTable retVal = new DataTable();
			string command = "SELECT * FROM wikilist_"+listName+" WHERE "+listName+"Num = "+itemNum;
			retVal=Db.GetTable(command);
			return retVal;
		}

		public static void DeleteItem(string listName,long itemNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listName,itemNum);
				return;
			}
			string command = "DELETE FROM wikilist_"+listName+" WHERE "+listName+"Num = "+itemNum;
			Db.NonQ(command);
		}

		public static List<string> GetAllLists() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod());
			}
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