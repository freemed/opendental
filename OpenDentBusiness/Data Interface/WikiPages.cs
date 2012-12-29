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
		///<summary>Improvements can be made later for caching these properly.</summary>
		public static WikiPage MasterPage;
		public static WikiPage StyleSheet;

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

		///<summary>If new, insert. Otherwise, copy old version to wikiPageHist, delete it from wikipage, and then insert.</summary>
		public static long InsertOrUpdate(WikiPage wikiPage){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				wikiPage.WikiPageNum=Meth.GetLong(MethodBase.GetCurrentMethod(),wikiPage);
				return wikiPage.WikiPageNum;
			}
			if(GetByTitle(wikiPage.PageTitle)!=null) {
				WikiPageHists.Insert(GetByTitle(wikiPage.PageTitle).ToWikiPageHist());
				string command= "DELETE FROM wikipage WHERE PageTitle = '"+POut.String(wikiPage.PageTitle)+"'";
				Db.NonQ(command);
			}
			return Crud.WikiPageCrud.Insert(wikiPage);
		}

		///<summary></summary>
		public static void Rename(string originalPageTitle, string newPageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),originalPageTitle,newPageTitle);
				return;
			}
			//Track history.
			WikiPage tempWP = GetByTitle(originalPageTitle);
			WikiPageHists.Insert(tempWP.ToWikiPageHist());
			tempWP.UserNum=Security.CurUser.UserNum;
			InsertOrUpdate(tempWP);//will create a new DateTimeSaved value and new PK.
			//Rename pages in both tables, wikiPage and wikiPageHist.
			string command="UPDATE wikipage SET PageTitle='"+POut.String(newPageTitle)+"'WHERE PageTitle='"+POut.String(originalPageTitle)+"';";
			Db.NonQ(command);
			command="UPDATE wikipagehist SET PageTitle='"+POut.String(newPageTitle)+"'WHERE PageTitle='"+POut.String(originalPageTitle)+"';";
			Db.NonQ(command);
			//For now, we will simply fix existing links in history
			command="UPDATE wikipage SET PageContent=REPLACE(PageContent,'[["+POut.String(originalPageTitle)+@"]]', '[["+POut.String(newPageTitle)+@"]]')";
			Db.NonQ(command);
			command="UPDATE wikipagehist SET PageContent=REPLACE(PageContent,'[["+POut.String(originalPageTitle)+@"]]', '[["+POut.String(newPageTitle)+@"]]')";
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
			/*
			matches = Regex.Matches(s,@"\*[\S](.|[\r\n][^(\r\n)])+");//(.|[\\n][^\\n])+[\\n][\\n]");
			foreach(Match match in matches) {
				string[] tokens = match.Value.Split('*');
				StringBuilder listBuilder = new StringBuilder();
				listBuilder.Append("<ul>\r\n");
				foreach(string listItem in tokens) {
					if(listItem!="") {
						listBuilder.Append("<li>"+listItem+"</li>\r\n");
					}
				}
				listBuilder.Append("</ul>\r\n");
				s=s.Replace(match.Value,listBuilder.ToString());
			}*/
			//numbered list----------------------------------------------------------------------------------------------------------------
			//todo: similar to above.
			/*
			matches = Regex.Matches(s,@"#[^(\s|\d)](.|[\r\n][^(\r\n)])+");//^(\\s|\\d) because color codes are being replaced.
			foreach(Match match in matches) {
				string[] tokens = match.Value.Split('#');
				StringBuilder listBuilder = new StringBuilder();
				listBuilder.Append("<ol>\r\n");
				foreach(string listItem in tokens) {
					if(listItem!="") {
						listBuilder.Append("<li>"+listItem+"</li>\r\n");
					}
				}
				listBuilder.Append("</ol>\r\n");
				s=s.Replace(match.Value,listBuilder.ToString());
			}*/
			//now, we switch to working in xml
			//////doc=new XmlDocument();
			//////using(StringReader reader=new StringReader(s)) {
			//////  try {
			//////    doc.Load(reader);
			//////  }
			//////  catch(Exception ex) {
			//////    return MasterPage.PageContent.Replace("@@@Content@@@",ex.Message);
			//////  }
			//////}
			//////XmlNode node=doc.DocumentElement;
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
			if(MasterPage==null){
				MasterPage=GetByTitle("_Master");
			}
			if(StyleSheet==null){
				StyleSheet=GetByTitle("_Style");
			}
			s=MasterPage.PageContent.Replace("@@@Content@@@",s);
			s=s.Replace("@@@Style@@@",StyleSheet.PageContent);
			return s;
		}

		///<summary>Creates historical entries into wikiPageHist.</summary>
		public static void Delete(string pageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pageTitle);
				return;
			}
			WikiPageHist wikiPageHist=GetByTitle(pageTitle).ToWikiPageHist();
			//preserve the existing page with user credentials
			WikiPageHists.Insert(wikiPageHist);
			//make entry to show who deleted the page
			wikiPageHist.IsDeleted=true;
			wikiPageHist.UserNum=Security.CurUser.UserNum;
			WikiPageHists.Insert(wikiPageHist);
			string command= "DELETE FROM wikipage WHERE PageTitle = '"+POut.String(pageTitle)+"'";
			Db.NonQ(command);
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