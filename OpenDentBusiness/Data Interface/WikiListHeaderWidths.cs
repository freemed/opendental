using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class WikiListHeaderWidths{
		///<summary>Used temporarily.</summary>
		public static string dummyColName="Xrxzes";

		#region CachePattern
		///<summary>A list of all WikiListHeaderWidths.</summary>
		private static List<WikiListHeaderWidth> listt;

		///<summary>A list of all WikiListHeaderWidths.</summary>
		public static List<WikiListHeaderWidth> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM wikilistheaderwidth";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="WikiListHeaderWidth";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			Listt=Crud.WikiListHeaderWidthCrud.TableToList(table);
		}
		#endregion

		/*///<summary>Returns header widths for list sorted in the same order as the columns appear in the DB. Can be more efficient than using cache.</summary>
		public static List<WikiListHeaderWidth> GetForListNoCache(string listName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiListHeaderWidth>>(MethodBase.GetCurrentMethod(),listName);
			}
			List<WikiListHeaderWidth> retVal = new List<WikiListHeaderWidth>();
			List<WikiListHeaderWidth> tempList = new List<WikiListHeaderWidth>();
			string command="DESCRIBE wikilist_"+POut.String(listName);
			DataTable listDescription=Db.GetTable(command);
			command="SELECT * FROM wikilistheaderwidth WHERE ListName='"+POut.String(listName)+"'";
			tempList=Crud.WikiListHeaderWidthCrud.SelectMany(command);
			for(int i=0;i<listDescription.Rows.Count;i++) {
				for(int j=0;j<tempList.Count;j++) {
					//Add WikiListHeaderWidth from tempList to retVal if it is the next row in listDescription.
					if(listDescription.Rows[i][0].ToString()==tempList[j].ColName) {
						retVal.Add(tempList[j]);
						break;
					}
				}
				//next description row.
			}
			return retVal;
		}*/

		///<summary>Returns header widths for list sorted in the same order as the columns appear in the DB. Uses cache.</summary>
		public static List<WikiListHeaderWidth> GetForList(string listName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiListHeaderWidth>>(MethodBase.GetCurrentMethod(),listName);
			}
			List<WikiListHeaderWidth> retVal = new List<WikiListHeaderWidth>();
			string command="DESCRIBE wikilist_"+POut.String(listName);
			DataTable listDescription=Db.GetTable(command);
			for(int i=0;i<listDescription.Rows.Count;i++) {
				for(int j=0;j<Listt.Count;j++) {
					//Add WikiListHeaderWidth from tempList to retVal if it is the next row in listDescription.
					if(listDescription.Rows[i][0].ToString()==Listt[j].ColName) {
						retVal.Add(Listt[j]);
						break;
					}
				}
				//next description row.
			}
			return retVal;
		}

		///<summary>Also alters the db table for the list itself.  Throws exception if number of columns does not match.</summary>
		public static void UpdateNamesAndWidths(string listName,List<WikiListHeaderWidth> columnDefs) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),columnDefs);
				return;
			}
			string command="DESCRIBE wikilist_"+POut.String(listName);
			DataTable listDescription=Db.GetTable(command);
			if(listDescription.Rows.Count!=columnDefs.Count) {
				throw new ApplicationException("List schema has been altered. Unable to save changes to list.");
			}
			//rename Columns with dummy names in case user is renaming a new column with an old name.---------------------------------------------
			for(int i=0;i<listDescription.Rows.Count;i++) {
				if(listDescription.Rows[i][0].ToString().ToLower()==POut.String(listName)+"num") {
					//skip primary key
					continue;
				}
				command="ALTER TABLE wikilist_"+POut.String(listName)+" CHANGE "+POut.String(listDescription.Rows[i][0].ToString())+" "+POut.String(dummyColName+i)+" TEXT NOT NULL";
				Db.NonQ(command);
				command=
				"UPDATE wikiListHeaderWidth SET ColName='"+POut.String(dummyColName+i)+"' "
				+"WHERE ListName='"+POut.String(listName)+"' "
				+"AND ColName='"+POut.String(listDescription.Rows[i][0].ToString())+"'";
				Db.NonQ(command);
			}
			//rename columns names and widths-------------------------------------------------------------------------------------------------------
			for(int i=0;i<listDescription.Rows.Count;i++) {
				if(listDescription.Rows[i][0].ToString().ToLower()==listName+"num") {//not a query, no POut.
					//skip primary key
					continue;
				}
				command="ALTER TABLE wikilist_"+POut.String(listName)+" CHANGE  "+POut.String(dummyColName+i)+" "+POut.String(columnDefs[i].ColName)+" TEXT NOT NULL";
				Db.NonQ(command);
				command="UPDATE wikiListHeaderWidth "
					+"SET ColName='"+POut.String(columnDefs[i].ColName)+"', ColWidth='"+POut.Int(columnDefs[i].ColWidth)+"' "
					+"WHERE ListName='"+POut.String(listName)+"' "
					+"AND ColName='"+POut.String(dummyColName+i)+"'";
				Db.NonQ(command);
			}
			//handle width of PK seperately because we do not rename the PK column, ever.
			command="UPDATE wikiListHeaderWidth SET ColWidth='"+POut.Int(columnDefs[0].ColWidth)+"' "
			+"WHERE ListName='"+POut.String(listName)+"' AND ColName='"+POut.String(columnDefs[0].ColName)+"'";
			Db.NonQ(command);
			RefreshCache();
		}

		///<summary>No error checking. Only called from WikiLists.</summary>
		public static void InsertNew(WikiListHeaderWidth newWidth) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),newWidth);
				return;
			}
			Crud.WikiListHeaderWidthCrud.Insert(newWidth);
			RefreshCache();
		}

		///<summary>No error checking. Only called from WikiLists after the corresponding column has been dropped from its respective table.</summary>
		public static void Delete(string listName,string colName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listName,colName);
				return;
			}
			string command = "DELETE FROM wikilistheaderwidth WHERE ListName='"+POut.String(listName)+"' AND ColName='"+POut.String(colName)+"'";
			Db.NonQ(command);
			RefreshCache();
		}

		public static void DeleteForList(string listName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listName);
				return;
			}
			string command = "DELETE FROM wikilistheaderwidth WHERE ListName='"+POut.String(listName)+"'";
			Db.NonQ(command);
			RefreshCache();
		}


	}
}