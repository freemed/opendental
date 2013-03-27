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

		public static bool CheckExists(string tableName) {
			string command = "SHOW TABLES LIKE 'wikilist\\_"+tableName+"'";
			if(Db.GetTable(command).Rows.Count==1) {
				//found exacty one table with that name
				return true;
			}
			//no table found with that name
			return false;
		}

		public static DataTable GetByName(string tableName) {
			string command = "SELECT * FROM wikilist_"+tableName+"";
			return Db.GetTable(command);
		}

		/// <summary>Does not validate or format inputs.</summary>
		public static void CreateNewWikiList(string tableName, List<string> columnNames) {
			string command = "CREATE TABLE wikilist_"+PIn.String(tableName)+" (";
			for(int i=0;i<columnNames.Count;i++){//string columnName in columnNames) {
				if(i==0) {//First Column is ALWAYS primary key, or at least should be.
					command+=PIn.String(columnNames[i])+" BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY ";
					continue;
				}
				command+=", "+PIn.String(columnNames[i].ToUpper()[0]+columnNames[i].Substring(1))+" TEXT";//Capitalize each column name.
			}
			command+=")";
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