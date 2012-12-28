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

		///<summary>Returns null if page does not exist.</summary>
		public static WikiPage GetByTitle(string pageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<WikiPage>(MethodBase.GetCurrentMethod(),pageTitle);
			}
			string command="SELECT * FROM wikipage WHERE PageTitle='"+POut.String(pageTitle)+"' and DateTimeSaved=(SELECT MAX(DateTimeSaved) FROM wikipage WHERE PageTitle='"+POut.String(pageTitle)+"');";
			return Crud.WikiPageCrud.SelectOne(command);
		}

		///<summary>Returns a list of all historical version of "PageTitle"</summary>
		public static List<WikiPage> GetHistoryByTitle(string pageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod(),pageTitle);
			}
			string command="SELECT * FROM wikipage WHERE PageTitle='"+POut.String(pageTitle)+"' ORDER BY DateTimeSaved;";
			return Crud.WikiPageCrud.SelectMany(command);
		}

		///<summary>Returns a list of all pages that reference "PageTitle".  No historical pages.  This is broken, but don't bother to fix it because we will add the new table to fix the problem.</summary>
		public static List<WikiPage> GetIncomingLinks(string PageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod(),PageTitle);
			}
			List<WikiPage> retVal=new List<WikiPage>();
			string command="SELECT * FROM wikipage WHERE PageContent LIKE '%[["+POut.String(PageTitle)+"]]%' "
				+"AND IsDeleted = 0 "
				+"GROUP BY PageTitle;";
			return Crud.WikiPageCrud.SelectMany(command);
		}

		///<summary>Returns a list current versions of pages.</summary>
		public static List<WikiPage> GetCurrent(string searchText) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod(),searchText);
			}
			List<WikiPage> retVal=new List<WikiPage>();
			string command="SELECT * FROM wikipage WHERE PageTitle NOT LIKE '\\_%' "
				+"AND PageTitle LIKE '%"+POut.String(searchText)+"%' ORDER BY DateTimeSaved DESC;";
			List<WikiPage> listAllWikiPages=Crud.WikiPageCrud.SelectMany(command);
			//Return only the newest version of each page. Since they are ordered by date, the newest versions will be the first added and all subsequent editions will be idgnored.
			for(int i=0;i<listAllWikiPages.Count;i++) {
				bool found=false;
				for(int r=0;r<retVal.Count;r++) {
					if(retVal[r].PageTitle==listAllWikiPages[i].PageTitle) {
						found=true;
						break;
					}
				}
				if(found) {
					continue;
				}
				retVal.Add(listAllWikiPages[i]);
			}
			//This is still clumsy.  Solution is a second db table.
			retVal.Sort(SortWikiPagesByName);
			return retVal;
		}

		private static int SortWikiPagesByName(WikiPage wp1,WikiPage wp2) {
			return wp1.PageTitle.CompareTo(wp2.PageTitle);
		}

		///<summary></summary>
		public static long Insert(WikiPage wikiPage){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				wikiPage.WikiPageNum=Meth.GetLong(MethodBase.GetCurrentMethod(),wikiPage);
				return wikiPage.WikiPageNum;
			}
			return Crud.WikiPageCrud.Insert(wikiPage);
		}

		///<summary></summary>
		public static void Rename(string originalPageTitle, string newPageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),originalPageTitle,newPageTitle);
				return;
			}
			//Create new entry to track changes by user.
			WikiPage tempWP = GetByTitle(originalPageTitle);
			tempWP.UserNum=Security.CurUser.UserNum;
			Insert(tempWP);
			//Actually rename pages and all previous versions.
			string command="UPDATE wikipage SET PageTitle='"+POut.String(newPageTitle)+"'WHERE PageTitle='"+POut.String(originalPageTitle)+"';";
			Db.NonQ(command);
			//Fix all broken internal links by inserting a new copy of each page if teh newest revision of that page has a link to the renamed page.
			//js- We can NOT do it this way.  It breaks our pattern of inserts.  In particular, it would break replication and Oracle.
//      command=@"INSERT INTO wikipage (UserNum, DateTimeSaved, PageTitle, PageContent)  
//							(SELECT "+Security.CurUser.UserNum+@",NOW(),t.PageTitle, REPLACE(t.PageContent,'[["+OriginalPageTitle+@"]]', '[["+NewPageTitle+@"]]')
//							FROM(
//								SELECT PageTitle, PageContent FROM wikipage 
//								WHERE PageContent LIKE '%[["+OriginalPageTitle+@"]]%' 
//								AND DateTimeSaved=(
//									SELECT MAX(DateTimeSaved) 
//									FROM wikipage 
//									WHERE PageTitle IN (
//										SELECT PageTitle 
//										FROM wikipage 
//										WHERE PageContent LIKE '%[["+OriginalPageTitle+@"]]%'
//									)))t
//							);";
//      Db.NonQ(command);
			//For now, we will simply fix existing links in history
			command="UPDATE wikipage SET PageContent=REPLACE(PageContent,'[["+POut.String(originalPageTitle)+@"]]', '[["+POut.String(newPageTitle)+@"]]')";
			Db.NonQ(command);
			return;
		}

		/*
		///<summary>Update may be implemented when versioning is improved.</summary>
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
			matches=Regex.Matches(s,@"\[\[(img:).*?\]\]");
			foreach(Match match in matches) {
				string imgName = match.Value.Substring(match.Value.IndexOf(":")+1).TrimEnd("]".ToCharArray());
				string fullPath=CodeBase.ODFileUtils.CombinePaths(GetWikiPath(),imgName);
				s=s.Replace(match.Value,"<img src=\"file:///\""+fullPath.Replace("\\","/")+" />");
			}
			//[[keywords: key1, key2, etc.]]------------------------------------------------------------------------------------------------
			matches=Regex.Matches(s,@"\[\[(keywords:).*?\]\]");//^(\\s|\\d) because color codes are being replaced.
			foreach(Match match in matches) {//should be only one
				s=s.Replace(match.Value,"<span class=\"keywords\">keywords:"+match.Value.Substring(11).TrimEnd("]".ToCharArray())+"</span>");
			}
			//[[InternalLink]]--------------------------------------------------------------------------------------------------------------
			matches=Regex.Matches(s,@"\[\[.*?\]\]");//.*? matches as few as possible.
			foreach(Match match in matches) {
				string tmpStyle="";
				if(GetByTitle(match.Value.Trim('[',']'))==null){//Later, instead of GetByTitle, we should just use a bool method. 
					tmpStyle="class='PageNotExists' ";
				}
				s=s.Replace(match.Value,"<a "+tmpStyle+"href=\""+"wiki:"+match.Value.Trim('[',']').Replace(" ","_")+"\">"+match.Value.Trim('[',']')+"</a>");
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
			doc=new XmlDocument();
			using(StringReader reader=new StringReader(s)) {
				try {
					doc.Load(reader);
				}
				catch(Exception ex) {
					return MasterPage.PageContent.Replace("@@@Content@@@",ex.Message);
				}
			}
			XmlNode node=doc.DocumentElement;
			//It's now time to look for paragraphs.
			//My basic assumption will be that <h123>, <image>, <table>, <ol>, and <ul> tags are external to paragraphs.
			//i.e. they are siblings.  They will never be surrounded with <p> tags.
			//On the other hand, <b>, <i>, <a>, and <span> tags are always internal to others, including <p>, <h123>, <table>, <ol>, and <ul>.
			//<br> tags will only be found within <p> (and <table>?)
			for(int i=0;i<node.ChildNodes.Count;i++) {

			}
			


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
			s=s.Replace("\r\n","<br />");
			//{{color|red|text}}
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
			}*/
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

		///<summary></summary>
		public static void Delete(long wikiPageNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),wikiPageNum);
				return;
			}
			string command= "DELETE FROM wikipage WHERE WikiPageNum = "+POut.Long(wikiPageNum);
			Db.NonQ(command);
		}
		*/

	



	}
}