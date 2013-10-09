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
		///<summary>The only wiki page that gets cached is the master page.</summary>
		private static WikiPage masterPage;

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
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM wikipage WHERE PageTitle='_Master'";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="WikiPage";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			masterPage=Crud.WikiPageCrud.TableToList(table)[0];
		}
		#endregion CachePattern

		///<summary>Returns null if page does not exist.</summary>
		public static WikiPage GetByTitle(string pageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<WikiPage>(MethodBase.GetCurrentMethod(),pageTitle);
			}
			string command="SELECT * FROM wikipage WHERE PageTitle='"+POut.String(pageTitle)+"'";
			return Crud.WikiPageCrud.SelectOne(command);
		}

		///<summary>Returns a list of pages with PageTitle LIKE '%searchText%'.  Excludes titles that start with underscore.</summary>
		public static List<WikiPage> GetByTitleContains(string searchText) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod(),searchText);
			}
			string command="SELECT * FROM wikipage WHERE PageTitle NOT LIKE '\\_%' "
				+"AND PageTitle LIKE '%"+POut.String(searchText)+"%' ORDER BY PageTitle";
			return Crud.WikiPageCrud.SelectMany(command);
		}

		///<summary>Used when saving a page to check and fix the capitalization on each internal link. So the returned pagetitle might have different capitalization than the supplied pagetitle</summary>
		public static string GetTitle(string pageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),pageTitle);
			}
			string command="SELECT PageTitle FROM wikipage WHERE PageTitle = '"+POut.String(pageTitle)+"'";
			return Db.GetScalar(command);
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

		///<summary>Searches keywords, title, content.</summary>
		public static List<string> GetForSearch(string searchText,bool ignoreContent) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod(),searchText,ignoreContent);
			}
			List<string> retVal=new List<string>();
			DataTable tableResults=new DataTable();
			string[] searchTokens = POut.String(searchText).Split(' ');
			string command="";
			//Match keywords first-----------------------------------------------------------------------------------
			command=
				"SELECT PageTitle FROM wikiPage "
				// \_ represents a literal _ because _ has a special meaning in LIKE clauses.
				//The second \ is just to escape the first \.  The other option would be to pass the \ through POut.String.
				+"WHERE PageTitle NOT LIKE '\\_%' ";
			for(int i=0;i<searchTokens.Length;i++) {
				command+="AND KeyWords LIKE '%"+POut.String(searchTokens[i])+"%' ";
			}
			command+=
				"GROUP BY PageTitle "
				+"ORDER BY PageTitle";
			tableResults=Db.GetTable(command);
			for(int i=0;i<tableResults.Rows.Count;i++) {
				if(!retVal.Contains(tableResults.Rows[i]["PageTitle"].ToString())) {
					retVal.Add(tableResults.Rows[i]["PageTitle"].ToString());
				}
			}
			//Match PageTitle Second-----------------------------------------------------------------------------------
			command=
				"SELECT PageTitle FROM wikiPage "
				+"WHERE PageTitle NOT LIKE '\\_%' ";
			for(int i=0;i<searchTokens.Length;i++) {
				command+="AND PageTitle LIKE '%"+POut.String(searchTokens[i])+"%' ";
			}
			command+=
				"GROUP BY PageTitle "
				+"ORDER BY PageTitle";
			tableResults=Db.GetTable(command);
			for(int i=0;i<tableResults.Rows.Count;i++) {
				if(!retVal.Contains(tableResults.Rows[i]["PageTitle"].ToString())) {
					retVal.Add(tableResults.Rows[i]["PageTitle"].ToString());
				}
			}
			//Match Content third-----------------------------------------------------------------------------------
			if(!ignoreContent) {
				command=
					"SELECT PageTitle FROM wikiPage "
					+"WHERE PageTitle NOT LIKE '\\_%' ";
				for(int i=0;i<searchTokens.Length;i++) {
					command+="AND PageContent LIKE '%"+POut.String(searchTokens[i])+"%' ";
				}
				command+=
					"GROUP BY PageTitle "
					+"ORDER BY PageTitle";
				tableResults=Db.GetTable(command);
				for(int i=0;i<tableResults.Rows.Count;i++) {
					if(!retVal.Contains(tableResults.Rows[i]["PageTitle"].ToString())) {
						retVal.Add(tableResults.Rows[i]["PageTitle"].ToString());
					}
				}
			}
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
			wikiPage.UserNum=Security.CurUser.UserNum;
			InsertAndArchive(wikiPage);
			//Rename all pages in both tables: wikiPage and wikiPageHist.
			string command="UPDATE wikipage SET PageTitle='"+POut.String(newPageTitle)+"'WHERE PageTitle='"+POut.String(wikiPage.PageTitle)+"'";
			Db.NonQ(command);
			command="UPDATE wikipagehist SET PageTitle='"+POut.String(newPageTitle)+"'WHERE PageTitle='"+POut.String(wikiPage.PageTitle)+"'";
			Db.NonQ(command);
			//For now, we will simply fix existing links in history.
			//The way this is written currently is case sensitive.  That's fine, but it means that all existing links must be perfect, including case, or they will not get updated.
			//To enforce proper case, we fix it when saving each page in the WikiEdit window.
			command="UPDATE wikipage SET PageContent=REPLACE(PageContent,'[["+POut.String(wikiPage.PageTitle)+"]]', '[["+POut.String(newPageTitle)+"]]')";
			Db.NonQ(command);
			command="UPDATE wikipagehist SET PageContent=REPLACE(PageContent,'[["+POut.String(wikiPage.PageTitle)+"]]', '[["+POut.String(newPageTitle)+"]]')";
			Db.NonQ(command);
			return;
		}

		///<summary>Used in TranslateToXhtml to know whether to mark a page as not exists.</summary>
		public static List<bool> CheckPageNamesExist(List<string> pageTitles) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<bool>>(MethodBase.GetCurrentMethod(),pageTitles);
			}
			string command="SELECT PageTitle FROM wikipage WHERE ";
			for(int i=0;i<pageTitles.Count;i++){
				if(i>0) {
					command+="OR ";
				}
				command+="PageTitle='"+POut.String(pageTitles[i])+"' ";
			}
			DataTable table=Db.GetTable(command);
			List<bool> retVal=new List<bool>();
			for(int p=0;p<pageTitles.Count;p++) {
				bool valForThisPage=false;
				for(int i=0;i<table.Rows.Count;i++) {
					if(table.Rows[i]["PageTitle"].ToString().ToLower()==pageTitles[p].ToLower()) {
						valForThisPage=true;
						break;
					}
				}
				retVal.Add(valForThisPage);
			}
			return retVal;
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

		///<summary>Surround with try/catch.  Also aggregates the content into the master page.  If isPreviewOnly, then the internal links will not be checked to see if the page exists, as it would make the refresh sluggish.  And isPreviewOnly also changes the pointer so that the page looks non-clickable.</summary>
		public static string TranslateToXhtml(string wikiContent,bool isPreviewOnly) {
			//No need to check RemotingRole; no call to db.
			#region Basic Xml Validation
			string s=wikiContent;
			MatchCollection matches;
			//"<",">", and "&"-----------------------------------------------------------------------------------------------------------
			s=s.Replace("&","&amp;");
			s=s.Replace("&amp;<","&lt;");//because "&" was changed to "&amp;" in the line above.
			s=s.Replace("&amp;>","&gt;");//because "&" was changed to "&amp;" in the line above.
			s="<body>"+s+"</body>";
			XmlDocument doc=new XmlDocument();
			using(StringReader reader=new StringReader(s)) {
				//try {
				doc.Load(reader);
				//}
				//catch(Exception ex) {
				//	return MasterPage.PageContent.Replace("@@@body@@@",ex.Message);
				//}
			}
			#endregion
			#region regex replacements
			//[[img:myimage.gif]]------------------------------------------------------------------------------------------------------------
			//MatchCollection matches;
			matches=Regex.Matches(s,@"\[\[(img:).+?\]\]");
			foreach(Match match in matches) {
				string imgName = match.Value.Substring(match.Value.IndexOf(":")+1).TrimEnd("]".ToCharArray());
				string fullPath=CodeBase.ODFileUtils.CombinePaths(GetWikiPath(),POut.String(imgName));
				s=s.Replace(match.Value,"<img src=\"file:///"+fullPath.Replace("\\","/")+"\"></img>");//"\" />");
			}
			//[[keywords: key1, key2, etc.]]------------------------------------------------------------------------------------------------
			matches=Regex.Matches(s,@"\[\[(keywords:).*?\]\]");
			foreach(Match match in matches) {//should be only one
				s=s.Replace(match.Value,"<span class=\"keywords\">keywords:"+match.Value.Substring(11).TrimEnd("]".ToCharArray())+"</span>");
			}
			//[[file:C:\eaula.txt]]------------------------------------------------------------------------------------------------
			matches=Regex.Matches(s,@"\[\[(file:).*?\]\]");
			foreach(Match match in matches) {
				string fileName=match.Value.Replace("[[file:","").TrimEnd(']');
				s=s.Replace(match.Value,"<a href=\"wikifile:"+fileName+"\">file:"+fileName+"</a>");
			}
			//[[folder:\\serverfiles\storage\]]------------------------------------------------------------------------------------------------
			matches=Regex.Matches(s,@"\[\[(folder:).*?\]\]");
			foreach(Match match in matches) {
				string folderName=match.Value.Replace("[[folder:","").TrimEnd(']');
				s=s.Replace(match.Value,"<a href=\"folder:"+folderName+"\">folder:"+folderName+"</a>");
			}
			//[[list:listname]]------------------------------------------------------------------------------------------------
			//matches=Regex.Matches(s,@"\[\[(list:).*?\]\]");
			//foreach(Match match in matches) {
			//	s=s.Replace(match.Value,WikiLists.TranslateToHTML(match.Value.Substring(7).Trim(']')));
			//}
			//[[color:red|text]]----------------------------------------------------------------------------------------------------------------
			matches = Regex.Matches(s,@"\[\[(color:).*?\]\]");//.*? matches as few as possible.
			foreach(Match match in matches) {
				//string[] paragraphs = match.Value.Split(new string[] { "\r\n" },StringSplitOptions.None);
				string tempText="<span style=\"color:";
				string[] tokens = match.Value.Split('|');
				if(tokens.Length<2) {//not enough tokens
					continue;
				}
				if(tokens[0].Split(':').Length!=2) {//Must have a color token and a color value seperated by a colon, no more no less.
					continue;
				}
				for(int i=0;i<tokens.Length;i++) {
					if(i==0) {
						tempText+=tokens[0].Split(':')[1]+";\">";//close <span> tag
						continue;
					}
					tempText+=(i>1?"|":"")+tokens[i];
				}
				tempText=tempText.TrimEnd(']');
				tempText+="</span>";
				s=s.Replace(match.Value,tempText);
			}
			//[[InternalLink]]--------------------------------------------------------------------------------------------------------------
			matches=Regex.Matches(s,@"\[\[.+?\]\]");
			List<string> pageNamesToCheck=new List<string>();
			List<bool> pageNamesExist=new List<bool>();
			if(!isPreviewOnly) {
				foreach(Match match in matches) {
					pageNamesToCheck.Add(match.Value.Trim('[',']'));
				}
				if(pageNamesToCheck.Count>0) {
					pageNamesExist=CheckPageNamesExist(pageNamesToCheck);//this gets a list of bools for all pagenames in one shot.  One query.
				}
			}
			foreach(Match match in matches) {
				string styleNotExists="";
				if(!isPreviewOnly) {
					string pageName=match.Value.Trim('[',']');
					int idx=pageNamesToCheck.IndexOf(pageName);
					if(!pageNamesExist[idx]){
						styleNotExists="class='PageNotExists' ";
					}
				}
				s=s.Replace(match.Value,"<a "+styleNotExists+"href=\""+"wiki:"+match.Value.Trim('[',']')/*.Replace(" ","_")*/+"\">"+match.Value.Trim('[',']')+"</a>");
			}
			//Unordered List----------------------------------------------------------------------------------------------------------------
			//Instead of using a regex, this will hunt through the rows in sequence.
			//later nesting by running ***, then **, then *
			s=ProcessList(s,"*");
			//numbered list---------------------------------------------------------------------------------------------------------------------
			s=ProcessList(s,"#");
			//table-------------------------------------------------------------------------------------------------------------------------
			//{|
			//!Width="100"|Column Heading 1!!Width="150"|Column Heading 2!!Width=""|Column Heading 3
			//|- 
			//|Cell 1||Cell 2||Cell 3 
			//|-
			//|Cell A||Cell B||Cell C 
			//|}
			//There are many ways to parse this.  Our strategy is to do it in a way that the generated xml is never invalid.
			//As the user types, the above example will frequently be in a state of partial completeness, and the parsing should gracefully continue anyway.
			//rigorous enforcement only happens when validating during a save, not here.
			matches=Regex.Matches(s,@"\{\|(.+?)\|\}",RegexOptions.Singleline);
			foreach(Match match in matches) {
				string tableStrOrig=match.Value;
				StringBuilder strbTable=new StringBuilder();
				string[] lines=tableStrOrig.Split(new string[] { "{|\r\n","\r\n|-\r\n","\r\n|}" },StringSplitOptions.RemoveEmptyEntries);
				strbTable.AppendLine("<table>");
				List<string> colWidths=new List<string>();
				for(int i=0;i<lines.Length;i++) {
					if(lines[i].StartsWith("!")) {//header
						strbTable.AppendLine("<tr>");
						lines[i]=lines[i].Substring(1);//strips off the leading !
						string[] cells=lines[i].Split(new string[] {"!!"},StringSplitOptions.None);
						colWidths.Clear();
						for(int c=0;c<cells.Length;c++){
							if(Regex.IsMatch(cells[c],@"(Width="")\d+""\|")){//e.g. Width="90"|
								strbTable.Append("<th ");
								string width=cells[c].Substring(7);//90"|Column Heading 1
								width=width.Substring(0,width.IndexOf("\""));//90
								colWidths.Add(width);
								strbTable.Append("Width=\""+width+"\">");
								strbTable.Append(ProcessParagraph(cells[c].Substring(cells[c].IndexOf("|")+1),false));//surround with p tags. Allow CR in header.
								strbTable.AppendLine("</th>");
							}
							else {
								strbTable.Append("<th>");
								strbTable.Append(ProcessParagraph(cells[c],false));//surround with p tags. Allow CR in header.
								strbTable.AppendLine("</th>");
							}
						}
						strbTable.AppendLine("</tr>");
					}
					else if(lines[i].Trim()=="|-"){
						//totally ignore these rows
					}
					else{//normal row
						strbTable.AppendLine("<tr>");
						lines[i]=lines[i].Substring(1);//strips off the leading |
						string[] cells=lines[i].Split(new string[] {"||"},StringSplitOptions.None);
						for(int c=0;c<cells.Length;c++) {
							strbTable.Append("<td Width=\""+colWidths[c]+"\">");
							strbTable.Append(ProcessParagraph(cells[c],false));
							strbTable.AppendLine("</td>");
						}
						strbTable.AppendLine("</tr>");
					}
				}
				strbTable.Append("</table>");
				s=s.Replace(tableStrOrig,strbTable.ToString());
			}
			#endregion regex replacements
			#region paragraph grouping
			StringBuilder strbSnew=new StringBuilder();
			//a paragraph is defined as all text between sibling tags, even if just a \r\n.
			int iScanInParagraph=0;//scan starting at the beginning of s.  S gets chopped from the start each time we grab a paragraph or a sibiling element.
			//The scanning position represents the verified paragraph content, and does not advance beyond that.
			//move <body> tag over.
			strbSnew.Append("<body>");
			s=s.Substring(6);
			bool startsWithCR=false;//todo: handle one leading CR if there is no text preceding it.
			if(s.StartsWith("\r\n")) {
				startsWithCR=true;
			}
			string tagName; 
			Match tagCurMatch;
			while(true) {//loop to either construct a paragraph, or to immediately add the next tag to strbSnew.
				iScanInParagraph=s.IndexOf("<",iScanInParagraph);//Advance the scanner to the start of the next tag
				if(iScanInParagraph==-1) {//there aren't any more tags, so current paragraph goes to end of string.  This won't happen
					throw new ApplicationException("No tags found.");
					//strbSnew.Append(ProcessParagraph(s));
				}
				if(s.Substring(iScanInParagraph).StartsWith("</body>")) {
					strbSnew.Append(ProcessParagraph(s.Substring(0,iScanInParagraph),startsWithCR));
					//startsWithCR=false;
					//strbSnew.Append("</body>");
					s="";
					iScanInParagraph=0;
					break;
				}
				tagName="";
				tagCurMatch=Regex.Match(s.Substring(iScanInParagraph),"^<.*?>");//regMatch);//.*? means any char, zero or more, as few as possible
				if(tagCurMatch==null) {
					//shouldn't happen unless closing bracket is missing
					throw new ApplicationException("Unexpected tag: "+s.Substring(iScanInParagraph));//message not seen by user, look in FormWikiEdit for relevant error messages.
				}
				if(tagCurMatch.Value.Trim('<','>').EndsWith("/")) {
					//self terminating tags NOT are allowed
					//this should catch all non-allowed self-terminating tags i.e. <br />, <inherits />, etc...
					throw new ApplicationException("All elements must have a beginning and ending tag. Unexpected tag: "+s.Substring(iScanInParagraph));//not seen by user
				}
				//Nesting of identical tags causes problems: 
				//<h1><h1>some text</h1></h1>
				//The first <h1> will match with the first </h1>.
				//We don't have time to support this outlier, so we will catch it in the validator when they save.
				//One possible strategy here might be:
				//idxNestedDuplicate=s.IndexOf("<"+tagName+">");
				//if(idxNestedDuplicate<s.IndexOf("</"+tagName+">"){
				//
				//}
				//Another possible strategy might be to use regular expressions.
				tagName=tagCurMatch.Value.Split(new string[] { "<"," ",">" },StringSplitOptions.RemoveEmptyEntries)[0];//works with tags like <i>, <span ...>, and <img .../>
				if(s.IndexOf("</"+tagName+">")==-1) {//this will happen if no ending tag.
					throw new ApplicationException("No ending tag: "+s.Substring(iScanInParagraph));
				}
				switch(tagName){
					case "a":
					case "b": 
					case "i": 
					case "span":
						iScanInParagraph=s.IndexOf("</"+tagName+">",iScanInParagraph)+3+tagName.Length;
						continue;//continues scanning this paragraph.
					case "h1": 
					case "h2": 
					case "h3": 
					case "ol": 
					case "ul": 
					case "table":
					case "img"://can NOT be self-terminating
						if(iScanInParagraph==0) {//s starts with a non-paragraph tag, so there is no partially assembled paragraph to process.
							//do nothing
						}
						else {//we are already part way into assembling a paragraph.  
							strbSnew.Append(ProcessParagraph(s.Substring(0,iScanInParagraph),startsWithCR));
							startsWithCR=false;//subsequent paragraphs will not need this
							s=s.Substring(iScanInParagraph);//chop off start of s
							iScanInParagraph=0;
						}
						//scan to the end of this element
						int iScanSibling=s.IndexOf("</"+tagName+">")+3+tagName.Length;
						//tags without a closing tag were caught above.
						//move the non-paragraph content over to s new.
						strbSnew.Append(s.Substring(0,iScanSibling));
						s=s.Substring(iScanSibling);
						//scanning will start a totally new paragraph
						break;
					default:
						throw new ApplicationException("Unexpected tag: "+s.Substring(iScanInParagraph));
				}
			}
			strbSnew.Append("</body>");
			#endregion
			#region aggregation
			doc=new XmlDocument();
			using(StringReader reader=new StringReader(strbSnew.ToString())) {
				//try {
				doc.Load(reader);
				//}
				//catch(Exception ex) {
				//	return MasterPage.PageContent.Replace("@@@body@@@",ex.Message);
				//}
			}
			StringBuilder strbOut=new StringBuilder();
			XmlWriterSettings settings=new XmlWriterSettings();
			settings.Indent=true;
			settings.IndentChars="\t";
			settings.OmitXmlDeclaration=true;
			settings.NewLineChars="\r\n";
			using(XmlWriter writer=XmlWriter.Create(strbOut,settings)) {
				doc.WriteTo(writer);
			}
			//spaces can't be handled prior to this point because &nbsp; crashes the xml parser.
			strbOut.Replace("  ","&nbsp;&nbsp;");//handle extra spaces. 
			strbOut.Replace("<td></td>","<td>&nbsp;</td>");//force blank table cells to show not collapsed
			strbOut.Replace("<th></th>","<th>&nbsp;</th>");//and blank table headers
			strbOut.Replace("{{nbsp}}","&nbsp;");//couldn't add the &nbsp; earlier because 
			strbOut.Replace("<p></p>","<p>&nbsp;</p>");//probably redundant but harmless
			//aggregate with master
			s=MasterPage.PageContent.Replace("@@@body@@@",strbOut.ToString());
			#endregion aggregation
			/*
			//js This code is buggy.  It will need very detailed comments and careful review before/if we ever turn it back on.
			if(isPreviewOnly) {
				//do not change cursor from pointer to IBeam to Hand as you move the cursor around the preview page
				s=s.Replace("*{\r\n\t","*{\r\n\tcursor:default;\r\n\t");
				//do not underline links if you hover over them in the preview window
				s=s.Replace("a:hover{\r\n\ttext-decoration:underline;","a:hover{\r\n\t");
			}*/
			return s;
		}

		///<summary>This will get called repeatedly.  prefixChars is, for now, * or #.  Returns the altered text of the full document.</summary>
		private static string ProcessList(string s,string prefixChars){
			string[] lines=s.Split(new string[] { "\r\n" },StringSplitOptions.None);//includes empty elements
			string blockOriginal=null;//once a list is found, this string will be filled with the original text.
			StringBuilder strb=null;//this will contain the final output enclosed in <ul> or <ol> tags.
			for(int i=0;i<lines.Length;i++) {
				if(blockOriginal==null) {//we are still hunting for the first line of a list.
					if(lines[i].StartsWith(prefixChars)) {//we found the first line of a list.
						blockOriginal=lines[i]+"\r\n";
						strb=new StringBuilder();
						if(prefixChars.Contains("*")) {
							strb.Append("<ul>\r\n");
						}
						else if(prefixChars.Contains("#")) {
							strb.Append("<ol>\r\n");
						}
						lines[i]=lines[i].Substring(prefixChars.Length);//strip off the prefixChars
						strb.Append("<li><span class='ListItemContent'>");
						//lines[i]=lines[i].Replace("  ","[[nbsp]][[nbsp]]");//handle extra spaces.  We may move this to someplace more global
						strb.Append(lines[i]);
						strb.Append("</span></li>\r\n");
					}
					else {//no list
						//nothing to do
					}
				}
				else {//we are already building our list
					if(lines[i].StartsWith(prefixChars)) {//we found another line of a list.  Could be a middle line or the last line.
						blockOriginal+=lines[i]+"\r\n";
						lines[i]=lines[i].Substring(prefixChars.Length);//strip off the prefixChars
						strb.Append("<li><span class='ListItemContent'>");
						//lines[i]=lines[i].Replace("  ","[[nbsp]][[nbsp]]");//handle extra spaces.  We may move this to someplace more global
						strb.Append(lines[i]);
						strb.Append("</span></li>\r\n");
					}
					else {//end of list.  The previous line was the last line.
						if(prefixChars.Contains("*")) {
							strb.Append("</ul>\r\n");
						}
						else if(prefixChars.Contains("#")) {
							strb.Append("</ol>\r\n");
						}
						//manually replace just the first occurance of the identified list.
						s=s.Substring(0,s.IndexOf(blockOriginal))
							+strb.ToString()
							+s.Substring(s.IndexOf(blockOriginal)+blockOriginal.Length);
						//s=s.Replace(blockOriginal,strb.ToString()); //old strategy, buggy.
						blockOriginal=null;
					}
				}
			}
			return s;
		}

		///<summary>This will wrap the text in p tags as well as handle internal carriage returns.  startsWithCR is only used on the first paragraph for the unusual case where the entire content starts with a CR.  This prevents stripping it off.</summary>
		private static string ProcessParagraph(string paragraph,bool startsWithCR) {
			if(paragraph.StartsWith("\r\n") && !startsWithCR){
				paragraph=paragraph.Substring(2);
			}
			if(paragraph=="") {//this must come after the first CR is stripped off, but before the ending CR is stripped off.
				return "";
			}
			if(paragraph.EndsWith("\r\n")) {//trailing CR remove
				paragraph=paragraph.Substring(0,paragraph.Length-2);
			}
			//if the paragraph starts with any number of spaces followed by a tag such as <b> or <span>, then we need to force those spaces to show.
			if(paragraph.StartsWith(" ") && paragraph.TrimStart(' ').StartsWith("<")) {
				paragraph="{{nbsp}}"+paragraph.Substring(1);//this will later be converted to &nbsp;
			}
			paragraph="<p>"+paragraph+"</p>";
			paragraph=paragraph.Replace("\r\n","</p><p>");
			//spaces at beginnings of lines
			paragraph=paragraph.Replace("<p> ","<p>{{nbsp}}");
			return paragraph;
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