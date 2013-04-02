using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class WikiListHeaderWidths{
		//todo: implement
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
			listt=Crud.WikiListHeaderWidthCrud.TableToList(table);
		}
		#endregion

		public static List<WikiListHeaderWidth> GetForList(string listName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiListHeaderWidth>>(MethodBase.GetCurrentMethod(),listName);
			}
//todo: loop through cache instead
			string command="SELECT * FROM wikilistheaderwidth WHERE ListName='"+POut.String(listName)+"'";
			return Crud.WikiListHeaderWidthCrud.SelectMany(command);
		}

		///<summary>Throws exception if number of columns does not match.</summary>
		public static void UpdateNamesAndWidths(string listName,List<WikiListHeaderWidth> columnDefs) {
//todo: LOTS of parameterization needed:
//not lots. only column names are user input, and those have been strictly filtered already and pose little, if any, threat.
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),columnDefs);
				return;
			}
			string command="DESCRIBE wikilist_"+listName;
			DataTable listDescription=Db.GetTable(command);
			if(listDescription.Rows.Count!=columnDefs.Count) {
				throw new ApplicationException("List schema has been altered. Unable to save changes to list.");
			}
			//rename Columns with dummy names in case user is renaming a new column with an old name.---------------------------------------------
			for(int i=1;i<listDescription.Rows.Count;i++) {//start at 1, do not rename primary key.
				command="ALTER TABLE wikilist_"+listName+" CHANGE "+listDescription.Rows[i][0]+" "+dummyColName+i+" TEXT NOT NULL";
				Db.NonQ(command);
				command="UPDATE wikiListHeaderWidth SET ColName='"+dummyColName+i+"' WHERE ListName='"+listName+"' AND ColName='"+listDescription.Rows[i][0]+"'";
				Db.NonQ(command);
			}
			//rename columns names and widths-------------------------------------------------------------------------------------------------------
			for(int i=1;i<listDescription.Rows.Count;i++) {//start at 1, do not rename primary key.
				command="ALTER TABLE wikilist_"+listName+" CHANGE  "+dummyColName+i+" "+columnDefs[i].ColName+" TEXT NOT NULL";
				Db.NonQ(command);
				command="UPDATE wikiListHeaderWidth "
					+"SET ColName='"+PIn.String(columnDefs[i].ColName)+"', ColWidth='"+columnDefs[i].ColWidth+"' "
					+"WHERE ListName='"+listName+"' "
					+"AND ColName='"+dummyColName+i+"'";
				Db.NonQ(command);
			}
			//handle width of PK seperately.
			command="UPDATE wikiListHeaderWidth SET ColWidth='"+columnDefs[0].ColWidth+"' WHERE ListName='"+listName+"' AND ColName='"+PIn.String(columnDefs[0].ColName)+"'";
			Db.NonQ(command);
		}

		///<summary>No error checking. Only called from WikiLists.</summary>
		public static void InsertNew(WikiListHeaderWidth newWidth) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),newWidth);
				return;
			}
			Crud.WikiListHeaderWidthCrud.Insert(newWidth);
		}

		///<summary>No error checking. Only called from WikiLists.</summary>
		public static void Delete(string listName,string colName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listName,colName);
				return;
			}
			string command = "DELETE FROM wikilistHeaderWidth WHERE ListName='"+listName+"' AND ColName='"+colName+"'";
			Db.NonQ(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<WikiListHeaderWidth> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiListHeaderWidth>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM wikilistheaderwidth WHERE PatNum = "+POut.Long(patNum);
			return Crud.WikiListHeaderWidthCrud.SelectMany(command);
		}

		///<summary>Gets one WikiListHeaderWidth from the db.</summary>
		public static WikiListHeaderWidth GetOne(long wikiListHeaderWidthNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<WikiListHeaderWidth>(MethodBase.GetCurrentMethod(),wikiListHeaderWidthNum);
			}
			return Crud.WikiListHeaderWidthCrud.SelectOne(wikiListHeaderWidthNum);
		}

		///<summary></summary>
		public static long Insert(WikiListHeaderWidth wikiListHeaderWidth){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				wikiListHeaderWidth.WikiListHeaderWidthNum=Meth.GetLong(MethodBase.GetCurrentMethod(),wikiListHeaderWidth);
				return wikiListHeaderWidth.WikiListHeaderWidthNum;
			}
			return Crud.WikiListHeaderWidthCrud.Insert(wikiListHeaderWidth);
		}

		///<summary></summary>
		public static void Update(WikiListHeaderWidth wikiListHeaderWidth){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),wikiListHeaderWidth);
				return;
			}
			Crud.WikiListHeaderWidthCrud.Update(wikiListHeaderWidth);
		}

		///<summary></summary>
		public static void Delete(long wikiListHeaderWidthNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),wikiListHeaderWidthNum);
				return;
			}
			string command= "DELETE FROM wikilistheaderwidth WHERE WikiListHeaderWidthNum = "+POut.Long(wikiListHeaderWidthNum);
			Db.NonQ(command);
		}
		*/



	}
}