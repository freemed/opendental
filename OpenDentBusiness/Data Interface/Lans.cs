using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace OpenDentBusiness {
	///<summary>Handles database commands for the language table in the database.</summary>
	public class Lans {
		///<summary>stub</summary>
		internal static string g(string sender,string text){
			return text;
		}

		///<summary>This had to be added because SilverLight does not allow globally setting the current culture format.</summary>
		public static string GetShortDateTimeFormat(){
			if(CultureInfo.CurrentCulture.Name=="en-US"){
				//DateTimeFormatInfo formatinfo=(DateTimeFormatInfo)CultureInfo.CurrentCulture.DateTimeFormat.Clone();
				//formatinfo.ShortDatePattern="MM/dd/yyyy";
				//return formatinfo;
				return "MM/dd/yyyy";
			}
			else{
				return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
			}
		}

		///<summary>key=ClassType+English.  Value =Language object.</summary>
		private static Dictionary<string,Language> HList;
		///<summary>Used by g to keep track of whether any language items were inserted into db. If so a refresh gets done.</summary>
		private static bool itemInserted;

		///<summary>Refreshed automatically to always be kept current with all phrases, regardless of whether there are any entries in LanguageForeign table.</summary>
		public static void Refresh() {
			HList=new Dictionary<string,Language>();
			if(CultureInfo.CurrentCulture.Name=="en-US") {
				return;
			}
			string command="SELECT * FROM language";
			DataTable table=Db.GetTable(command);
			Language langTemp;
			//list=new Language[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				langTemp=new Language();
				//List[i].EnglishCommentsOld= PIn.PString(table.Rows[i][0].ToString());
				langTemp.ClassType      = PIn.PString(table.Rows[i][1].ToString());
				langTemp.English        = PIn.PString(table.Rows[i][2].ToString());
				langTemp.IsObsolete     = PIn.PBool(table.Rows[i][3].ToString());
				if(!HList.ContainsKey(langTemp.ClassType+langTemp.English)) {
					HList.Add(langTemp.ClassType+langTemp.English,langTemp);
				}
			}
			//MessageBox.Show(List.Length.ToString());
		}

		///<summary>Tries to insert, but ignores the insert if this row already exists. This prevents the previous frequent crashes.</summary>
		public static void Insert(Language Cur) {
			//In Oracle, one must specify logging options for logging to occur, otherwise logging is not used.
			string ignoreClause="";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				ignoreClause="IGNORE";
			}
			string command = "INSERT "+ignoreClause+" INTO language (ClassType,English,EnglishComments,IsObsolete) "
				+"VALUES("
				+"'"+POut.PString(Cur.ClassType)+"', "
				+"'"+POut.PString(Cur.English)+"','',0)";
			//MessageBox.Show(command);
			Db.NonQ(command);
		}

		/*
		///<summary>not used to update the english version of text.  Create new instead.</summary>
		public static void UpdateCur(){
			string command="UPDATE language SET "
				+"EnglishComments = '" +POut.PString(Cur.EnglishComments)+"'"
				+",IsObsolete = '"     +POut.PBool  (Cur.IsObsolete)+"'"
				+" WHERE ClassType = BINARY '"+POut.PString(Cur.ClassType)+"'"
				+" AND English = BINARY '"     +POut.PString(Cur.English)+"'";
			NonQ(false);
		}*/

		///<summary></summary>
		public static string[] GetListCat() {
			string command="SELECT Distinct ClassType FROM language ORDER BY ClassType ";
			DataTable table=Db.GetTable(command);
			string[] ListCat=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				ListCat[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return ListCat;
		}

		///<summary>Only used in translation tool to get list for one category</summary>
		public static Language[] GetListForCat(string classType) {
			string command="SELECT * FROM language "
				+"WHERE ClassType = BINARY '"+POut.PString(classType)+"' ORDER BY English";
			DataTable table=Db.GetTable(command);
			Language[] ListForCat=new Language[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				ListForCat[i]=new Language();
				//ListForCat[i].EnglishComments= PIn.PString(table.Rows[i][0].ToString());
				ListForCat[i].ClassType      = PIn.PString(table.Rows[i][1].ToString());
				ListForCat[i].English        = PIn.PString(table.Rows[i][2].ToString());
				ListForCat[i].IsObsolete     = PIn.PBool(table.Rows[i][3].ToString());
			}
			return ListForCat;
		}

		public static string ConvertString(string classType,string text) {
			if(CultureInfo.CurrentCulture.Name=="en-US") {
				return text;
			}
			if(text=="") {
				return "";
			}
			if(HList==null) {
				return text;
			}
			itemInserted=false;
			if(!HList.ContainsKey(classType+text)) {
				Language Cur=new Language();
				Cur.ClassType=classType;
				Cur.English=text;
				HList.Add(Cur.ClassType+Cur.English,Cur);
				Insert(Cur);
				itemInserted=true;
				return text;
			}
			if(LanguageForeigns.HList.Contains(classType+text)) {
				if(((LanguageForeign)LanguageForeigns.HList[classType+text]).Translation=="") {
					//if translation is empty
					return text;//return the English version
				}
				return ((LanguageForeign)LanguageForeigns.HList[classType+text]).Translation;
			}
			else {
				return text;
			}
		}

		///<summary>Gets a short time format for displaying in appt and schedule along the sides. Pass in a clone of the current culture; it will get altered. Returns a string format.</summary>
		public static string GetShortTimeFormat(CultureInfo ci) {
			//No need to check RemotingRole; no call to db.
			string hFormat="";
			ci.DateTimeFormat.AMDesignator=ci.DateTimeFormat.AMDesignator.ToLower();
			ci.DateTimeFormat.PMDesignator=ci.DateTimeFormat.PMDesignator.ToLower();
			string shortTimePattern=ci.DateTimeFormat.ShortTimePattern;
			if(shortTimePattern.IndexOf("hh")!=-1) {//if hour is 01-12
				hFormat+="hh";
			}
			else if(shortTimePattern.IndexOf("h")!=-1) {//or if hour is 1-12
				hFormat+="h";
			}
			else if(shortTimePattern.IndexOf("HH")!=-1) {//or if hour is 00-23
				hFormat+="HH";
			}
			else {//hour is 0-23
				hFormat+="H";
			}
			if(shortTimePattern.IndexOf("t")!=-1) {//if there is an am/pm designator
				hFormat+="tt";
			}
			else {//if no am/pm designator, then use :00
				hFormat+=":00";//time separator will actually change according to region
			}
			return hFormat;
		}

		///<summary>No need to refresh after this.</summary>
		public static void DeleteItems(string classType,List<string> englishList) {
			string command="DELETE FROM language WHERE ClassType='"+POut.PString(classType)+"' AND (";
			for(int i=0;i<englishList.Count;i++) {
				if(i>0) {
					command+="OR ";
				}
				command+="English='"+POut.PString(englishList[i])+"' ";
				if(HList.ContainsKey(classType+englishList[i])) {
					HList.Remove(classType+englishList[i]);
				}
			}
			command+=")";
			Db.NonQ(command);
		}
	}
}
