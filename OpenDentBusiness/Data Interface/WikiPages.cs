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
	public class WikiPages{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary></summary>
		private static WikiPage masterPage;
		///<summary></summary>
		private static WikiPage styleSheet;

		///<summary></summary>
		public static WikiPage MasterPage {
			get {
				if(masterPage==null) {
					RefreshCache();
				}
				return masterPage;
			}
			set {
				masterPage=value;
			}
		}

		///<summary></summary>
		public static WikiPage StyleSheet {
			get {
				if(styleSheet==null) {
					RefreshCache();
				}
				return styleSheet;
			}
			set {
				styleSheet=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM wikipage WHERE PageTitle='_Style' OR PageTitle='_Master'";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="WikiPage";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List<WikiPage> listPages=Crud.WikiPageCrud.TableToList(table);
			for(int i=0;i<listPages.Count;i++) {
				if(listPages[i].PageTitle=="_Master") {
					masterPage=listPages[i];
				}
				if(listPages[i].PageTitle=="_Style") {
					styleSheet=listPages[i];
				}
			}
		}
		#endregion CachePattern

		///<summary>Returns null if page does not exist.</summary>
		public static WikiPage GetByTitle(string pageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<WikiPage>(MethodBase.GetCurrentMethod(),pageTitle);
			}
			//string command="SELECT * FROM wikipage WHERE PageTitle='"+POut.String(pageTitle)+"' and DateTimeSaved=(SELECT MAX(DateTimeSaved) FROM wikipage WHERE PageTitle='"+POut.String(pageTitle)+"');";
			string command="SELECT * FROM wikipage WHERE PageTitle='"+POut.String(pageTitle)+"';";
			return Crud.WikiPageCrud.SelectOne(command);
		}

		///<summary>Returns a list of pages with PageTitle LIKE '%searchText%'.</summary>
		public static List<WikiPage> GetByTitleContains(string searchText) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod(),searchText);
			}
			string command="SELECT * FROM wikipage WHERE PageTitle NOT LIKE '\\_%' "
				+"AND PageTitle LIKE '%"+POut.String(searchText)+"%' ORDER BY PageTitle;";
			return Crud.WikiPageCrud.SelectMany(command);
		}

		///<summary>Archives first by moving to WikiPageHist if it already exists.  Then, in either case, it inserts the new page.</summary>
		public static long InsertAndArchive(WikiPage wikiPage) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				wikiPage.WikiPageNum=Meth.GetLong(MethodBase.GetCurrentMethod(),wikiPage);
				return wikiPage.WikiPageNum;
			}
			WikiPage wpExisting=GetByTitle(wikiPage.PageTitle);
			if(wpExisting!=null) {
				WikiPageHist wpHist=PageToHist(wpExisting);
				WikiPageHists.Insert(wpHist);
				string command= "DELETE FROM wikipage WHERE PageTitle = '"+POut.String(wikiPage.PageTitle)+"'";
				Db.NonQ(command);
			}
			return Crud.WikiPageCrud.Insert(wikiPage);
		}

		///<summary></summary>
		public static List<WikiPage> GetForSearch(string searchText,bool searchDeleted,bool ignoreContent) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod());
			}
			string command="";
			if(searchText=="") {//otherwise the search below is meaning less.
				command="SELECT * FROM "+(searchDeleted?"wikiPageHist ":"wikiPage ")
					+"WHERE PageTitle NOT LIKE '\\_%' "
					+(searchDeleted?"AND IsDeleted=1;":";");
				return Crud.WikiPageCrud.SelectMany(command);
			}
			List<WikiPage> retVal=new List<WikiPage>();
			List<WikiPage> listWikiPageTemp=new List<WikiPage>();
			//Match keywords first
			command=
			"SELECT * FROM "+(searchDeleted?"wikiPageHist ":"wikiPage ")
			+"WHERE PageContent LIKE '%[[keywords:%"+searchText+"%]]' "//This part needs work.
			+"AND PageTitle NOT LIKE '\\_%' "
			+(searchDeleted?"AND IsDeleted=1 ":"")
			+"ORDER BY PageTitle;";
			retVal=Crud.WikiPageCrud.SelectMany(command);
			//Match PageTitle Second
			command=
			"SELECT * FROM "+(searchDeleted?"wikiPageHist ":"wikiPage ")
			+"WHERE PageTitle LIKE '%"+searchText+"%' "
			+"AND PageTitle NOT LIKE '\\_%' "
			+(searchDeleted?"AND IsDeleted=1 ":"")
			+"ORDER BY PageTitle;";
			listWikiPageTemp=Crud.WikiPageCrud.SelectMany(command);
			foreach(WikiPage wikiPage in listWikiPageTemp) {
				if(retVal.Contains(wikiPage)) {
					continue;
				}
				retVal.Add(wikiPage);
			}
			//Match Content third
			if(!ignoreContent) {
				command=
				"SELECT * FROM "+(searchDeleted?"wikiPageHist ":"wikiPage ")
				+"WHERE PageContent LIKE '%"+searchText+"%' "
				+"AND PageTitle NOT LIKE '\\_%' "
				+(searchDeleted?"AND IsDeleted=1 ":"")
				+"ORDER BY PageTitle;";
				listWikiPageTemp=Crud.WikiPageCrud.SelectMany(command);
				foreach(WikiPage wikiPage in listWikiPageTemp) {
					if(retVal.Contains(wikiPage)) {
						continue;
					}
					retVal.Add(wikiPage);
				}
			}//end !ignoreContent
			return retVal;
		}

		///<summary>Returns a list of all pages that reference "PageTitle".  No historical pages.</summary>
		public static List<WikiPage> GetIncomingLinks(string pageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod(),pageTitle);
			}
			List<WikiPage> retVal=new List<WikiPage>();
			string command="SELECT * FROM wikipage WHERE PageContent LIKE '%[["+POut.String(pageTitle)+"]]%' ORDER BY PageTitle";
			return Crud.WikiPageCrud.SelectMany(command);
		}

		///<summary>Validation was already done in FormWikiRename to make sure that the page does not already exist in WikiPage table.  But what if the page already exists in WikiPageHistory?  In that case, previous history for the other page would start showing as history for the newly renamed page, which is fine.</summary>
		public static void Rename(WikiPage wikiPage, string newPageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),wikiPage,newPageTitle);
				return;
			}
			//a later improvement would be to validate again here in the business layer.
			//WikiPage wikiPage=GetByTitle(originalPageTitle);
			//I think these next two lines already get done as part of InsertAndArchive
			//WikiPageHist wikiPageHist=PageToHist(wikiPage);
			//WikiPageHists.Insert(wikiPageHist);//Save the current page to history.
			wikiPage.UserNum=Security.CurUser.UserNum;
			InsertAndArchive(wikiPage);
			//Rename all pages in both tables: wikiPage and wikiPageHist.
			string command="UPDATE wikipage SET PageTitle='"+POut.String(newPageTitle)+"'WHERE PageTitle='"+POut.String(wikiPage.PageTitle)+"';";
			Db.NonQ(command);
			command="UPDATE wikipagehist SET PageTitle='"+POut.String(newPageTitle)+"'WHERE PageTitle='"+POut.String(wikiPage.PageTitle)+"';";
			Db.NonQ(command);
			//For now, we will simply fix existing links in history
			command="UPDATE wikipage SET PageContent=REPLACE(PageContent,'[["+POut.String(wikiPage.PageTitle)+"]]', '[["+POut.String(newPageTitle)+"]]')";
			Db.NonQ(command);
			command="UPDATE wikipagehist SET PageContent=REPLACE(PageContent,'[["+POut.String(wikiPage.PageTitle)+"]]', '[["+POut.String(newPageTitle)+"]]')";
			Db.NonQ(command);
			return;
		}

		/*///<summary>Update may be implemented when versioning is improved.</summary>
		public static void Update(WikiPage wikiPage){
			Insert(wikiPage);
			//if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
			//  Meth.GetVoid(MethodBase.GetCurrentMethod(),wikiPage);
			//  return;
			//}
			//Crud.WikiPageCrud.Update(wikiPage);
		}*/

		///<summary>Typically returns something similar to \\SERVER\OpenDentImages\Wiki</summary>
		public static string GetWikiPath() {
			//No need to check RemotingRole; no call to db.
			string wikiPath;
			if(!PrefC.AtoZfolderUsed) {
				throw new ApplicationException("Must be using AtoZ folders.");
			}
			wikiPath=Path.Combine(ImageStore.GetPreferredAtoZpath(),"Wiki");
			if(!Directory.Exists(wikiPath)) {
				Directory.CreateDirectory(wikiPath);
			}
			return wikiPath;
		}

		///<summary>Also aggregates the content into the master page.</summary>
		public static string TranslateToXhtml(string wikiContent) {
			//No need to check RemotingRole; no call to db.
			string s=wikiContent;
			//"<" and ">"-----------------------------------------------------------------------------------------------------------
			s=s.Replace("&<","&lt;");
			s=s.Replace("&>","&gt;");
			s="<body>"+s+"</body>";
			XmlDocument doc=new XmlDocument();
			using(StringReader reader=new StringReader(s)) {
				try {
					doc.Load(reader);
				}
				catch(Exception ex) {
					return MasterPage.PageContent.Replace("@@@Content@@@",ex.Message);
				}
			}
			//Ryan, feel free to do minor debugging.  I didn't test any of this.
			
			//(foreach is ok for Matches, as you used them below.  That's one of the exceptions.)

			//[[img:myimage.gif]]------------------------------------------------------------------------------------------------------------
			MatchCollection matches;
			matches=Regex.Matches(s,@"\[\[(img:).+?\]\]");
			foreach(Match match in matches) {
				string imgName = match.Value.Substring(match.Value.IndexOf(":")+1).TrimEnd("]".ToCharArray());
				string fullPath=CodeBase.ODFileUtils.CombinePaths(GetWikiPath(),imgName);
				s=s.Replace(match.Value,"<img src=\"file:///"+fullPath.Replace("\\","/")+"\" />");
			}
			//[[keywords: key1, key2, etc.]]------------------------------------------------------------------------------------------------
			matches=Regex.Matches(s,@"\[\[(keywords:).*?\]\]");//^(\\s|\\d) because color codes are being replaced.
			foreach(Match match in matches) {//should be only one
				s=s.Replace(match.Value,"<span class=\"keywords\">keywords:"+match.Value.Substring(11).TrimEnd("]".ToCharArray())+"</span>");
			}
			//[[InternalLink]]--------------------------------------------------------------------------------------------------------------
			matches=Regex.Matches(s,@"\[\[.+?\]\]");//.*? matches as few as possible.
			foreach(Match match in matches) {
				string tmpStyle="";
				if(GetByTitle(match.Value.Trim('[',']'))==null){//Later, instead of GetByTitle, we should just use a bool method. 
					tmpStyle="class='PageNotExists' ";
				}
				s=s.Replace(match.Value,"<a "+tmpStyle+"href=\""+"wiki:"+match.Value.Trim('[',']')/*.Replace(" ","_")*/+"\">"+match.Value.Trim('[',']')+"</a>");
			}
			//Unordered List----------------------------------------------------------------------------------------------------------------
			//Instead of using a regex, this will hunt through the rows in sequence.
			//This is going to be critical as this section gets more complex with nesting and mixing.
			string[] lines=s.Split(new string[]{"\r\n"},StringSplitOptions.None);//includes empty elements
			string blockOriginal=null;//once a list is found, this string will be filled with the original text.
			StringBuilder strb=null;//this will contain the final output enclosed in <ul> tags.
			for(int i=0;i<lines.Length;i++) {
				if(blockOriginal==null) {//we are still hunting for the first line of an UL.
					if(lines[i].Trim().StartsWith("*")) {//we found the first line of an UL.
						blockOriginal=lines[i]+"\r\n";
						strb=new StringBuilder();
						strb.Append("<ul>\r\n");
						strb.Append("<li>");
						//handle leading spaces later (not very important)
						string curline=lines[i];
						curline=curline.Remove(lines[i].IndexOf("*"),1);//for now, we will just remove the first * that we find.
						curline=curline.Replace("  ","&nbsp;&nbsp;");//handle extra spaces.  This is the only place we will do this for <ul>.  We will improve it later.
						strb.Append(curline);
						strb.Append("</li>\r\n");
					}
					else {//no list
						//nothing to do
					}
				}
				else {//we are already building our list
					if(lines[i].Trim().StartsWith("*")) {//we found another line of an UL.  Could be a middle line or the last line.
						blockOriginal+=lines[i]+"\r\n";
						strb.Append("<li>");
						//handle leading spaces later (not very important)
						string curline=lines[i];
						curline=curline.Remove(lines[i].IndexOf("*"),1);//for now, we will just remove the first * that we find.
						curline=curline.Replace("  ","&nbsp;&nbsp;");//handle extra spaces.  This is the only place we will do this for <ul>.  We will improve it later.
						strb.Append(curline);
						strb.Append("</li>\r\n");
					}
					else {//end of list.  The previous line was the last line.
						strb.Append("</ul>\r\n");
						s=s.Replace(blockOriginal,strb.ToString());
						blockOriginal=null;
					}
				}
			}
			//numbered list---------------------------------------------------------------------------------------------------------------------
			//todo: similar to above.
			//{{color|red|text}}----------------------------------------------------------------------------------------------------------------
			matches = Regex.Matches(s,"{{(color)(.*?)}}");//.*? matches as few as possible.
			foreach(Match match in matches) {
				string[] tokens = match.Value.Split('|');
				if(tokens.Length<3) {//not enough tokens
					continue;
				}
				string tempText="";//text to be colored
				for(int i=0;i<tokens.Length;i++){
					if(i<2){//ignore the color and "#00FF00" values
						continue;
					}
					if(i==tokens.Length-1) {//last token
						tempText+=(i>2?"|":"")+tokens[i].TrimEnd('}');//This allows pipes "|" to be included in the colored text.
						continue;
					}
					tempText+=(i>2?"|":"")+tokens[i];//This allows pipes "|" to be included in the colored text.
				}
				s=s.Replace(match.Value,"<span style=\"color:"+tokens[1]+";\">"+tempText+"</span>");
			}
			//now, we switch to working in xml
			doc=new XmlDocument();
			using(StringReader reader=new StringReader(s)) {
				try {
					doc.Load(reader);
				}
				catch(Exception ex) {
					return MasterPage.PageContent.Replace("@@@body@@@",ex.Message);
				}
			}
			XmlNode node=doc.DocumentElement;
			//It's now time to look for paragraphs.
			//My basic assumption will be that <h123>, <image>, <table>, <ol>, and <ul> tags are external to paragraphs.
			//i.e. they are siblings.  They will never be surrounded with <p> tags.
			//On the other hand, <b>, <i>, <a>, and <span> tags are always internal to others, including <p>, <h123>, <table>, <ol>, and <ul>.
			//<br> tags will only be found within <p> (and <table>?)
			//for(int i=0;i<node.ChildNodes.Count;i++) {

			//}
			


			/*
			//<p>
			//double carriage returns have already been stripped out for lists.
			s=s.Replace("\r\n\r\n","</p>\r\n<p>");
			s=s.Replace("<p></p>","<p>&nbsp;</p>");//so that empty paragraphs will show up.
			//"     "(tabs) and double spaces.
			s=s.Replace("  ","&nbsp;&nbsp;");//because single space should always show in html.
			//s=s.Replace("     ","&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("  ","&nbsp;&nbsp;");
			//<br />
			//use a regex match to only affect within paragraphs.
			s=s.Replace("\r\n","<br />");*/
			//aggregate with master
			s=MasterPage.PageContent.Replace("@@@body@@@",s);
			s=s.Replace("@@@Style@@@",StyleSheet.PageContent);
			return s;
		}

		///<summary>Creates historical entry of deletion into wikiPageHist, and deletes current page from WikiPage.</summary>
		public static void Delete(string pageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pageTitle);
				return;
			}
			WikiPage wikiPage=GetByTitle(pageTitle);
			WikiPageHist wikiPageHist=PageToHist(wikiPage);
			//preserve the existing page with user credentials
			WikiPageHists.Insert(wikiPageHist);
			//make entry to show who deleted the page
			wikiPageHist.IsDeleted=true;
			wikiPageHist.UserNum=Security.CurUser.UserNum;
			WikiPageHists.Insert(wikiPageHist);
			string command= "DELETE FROM wikipage WHERE PageTitle = '"+POut.String(pageTitle)+"'";
			Db.NonQ(command);
		}

		public static WikiPageHist PageToHist(WikiPage wikiPage) {
			//No need to check RemotingRole; no call to db.
			WikiPageHist wikiPageHist=new WikiPageHist();
			wikiPageHist.WikiPageNum=-1;//todo:handle this -1, shouldn't be a problem since we always get pages by Title.
			wikiPageHist.UserNum=wikiPage.UserNum;
			wikiPageHist.PageTitle=wikiPage.PageTitle;
			wikiPageHist.PageContent=wikiPage.PageContent;
			wikiPageHist.DateTimeSaved=wikiPage.DateTimeSaved;//This gets set to NOW if this page is then inserted
			wikiPageHist.IsDeleted=false;
			return wikiPageHist;
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<WikiPage> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM wikipage WHERE PatNum = "+POut.Long(patNum);
			return Crud.WikiPageCrud.SelectMany(command);
		}

		///<summary>Gets one WikiPage from the db.</summary>
		public static WikiPage GetOne(long wikiPageNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<WikiPage>(MethodBase.GetCurrentMethod(),wikiPageNum);
			}
			return Crud.WikiPageCrud.SelectOne(wikiPageNum);
		}
		*/

	



	}
}