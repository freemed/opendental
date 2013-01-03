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
			//string command="SELECT * FROM wikipage WHERE PageTitle='"+POut.String(pageTitle)+"' and DateTimeSaved=(SELECT MAX(DateTimeSaved) FROM wikipage WHERE PageTitle='"+POut.String(pageTitle)+"');";
			string command="SELECT * FROM wikipage WHERE PageTitle='"+POut.String(pageTitle)+"'";
			return Crud.WikiPageCrud.SelectOne(command);
		}

		///<summary>Returns a list of pages with PageTitle LIKE '%searchText%'.</summary>
		public static List<WikiPage> GetByTitleContains(string searchText) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod(),searchText);
			}
			string command="SELECT * FROM wikipage WHERE PageTitle NOT LIKE '\\_%' "
				+"AND PageTitle LIKE '%"+POut.String(searchText)+"%' ORDER BY PageTitle";
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

		//public static List<WikiPage> GetForSearch(string searchText,bool searchDeleted,bool ignoreContent) {
		//  if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
		//    return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod());
		//  }
		//  string command="";
		//  if(searchText=="") {//otherwise the search below is meaning less.
		//    command="SELECT * FROM "+(searchDeleted?"wikiPageHist ":"wikiPage ")
		//      +"WHERE PageTitle NOT LIKE '\\_%' "
		//      +(searchDeleted?"AND IsDeleted=1;":";");
		//    return Crud.WikiPageCrud.SelectMany(command);
		//  }
		//  List<WikiPage> retVal=new List<WikiPage>();
		//  List<WikiPage> listWikiPageTemp=new List<WikiPage>();
		//  if(searchDeleted){
		//    //Match title First-----------------------------------------------------------------------------------
		//    command=
		//    "SELECT * FROM wikiPageHist "
		//    +"WHERE PagetTitle LIKE '%"+searchText+"%' "
		//    +"AND PageTitle NOT LIKE '\\_%' "
		//    +"AND IsDeleted=1 "
		//    +"ORDER BY PageTitle;";
		//    retVal=Crud.WikiPageCrud.SelectMany(command);
		//    //Match Content Second-----------------------------------------------------------------------------------
		//    if(!ignoreContent){
		//      command=
		//      "SELECT * FROM wikiPageHist "
		//      +"WHERE PageContent LIKE '%"+searchText+"%' "
		//      +"AND PageTitle NOT LIKE '\\_%' "
		//      +"AND IsDeleted=1 "
		//      +"ORDER BY PageTitle;";
		//      listWikiPageTemp=Crud.WikiPageCrud.SelectMany(command);
		//      foreach(WikiPage wikiPage in listWikiPageTemp) {
		//        bool alreadyFound=false;
		//        for(int i=0;i<retVal.Count;i++) {
		//          if(retVal[i].PageTitle==wikiPage.PageTitle) {
		//            alreadyFound=true;
		//            break;
		//          }
		//        }
		//        if(!alreadyFound) {
		//          retVal.Add(wikiPage);
		//        }
		//      }//end listWikiPageTemp
		//    }//end if ignoreContent
		//  }//end if searchDeleted
		//  else{
		//    //Match keywords first-----------------------------------------------------------------------------------
		//    command=
		//    "SELECT * FROM wikiPage "
		//    +"WHERE KeyWords LIKE '%"+searchText+"%' "
		//    +"AND PageTitle NOT LIKE '\\_%' "
		//    +"ORDER BY PageTitle;";
		//    retVal=Crud.WikiPageCrud.SelectMany(command);
		//    //Match PageTitle Second-----------------------------------------------------------------------------------
		//    command=
		//    "SELECT * FROM wikiPage "
		//    +"WHERE PageTitle LIKE '%"+searchText+"%' "
		//    +"AND PageTitle NOT LIKE '\\_%' "
		//    +"ORDER BY PageTitle;";
		//    listWikiPageTemp=Crud.WikiPageCrud.SelectMany(command);
		//    foreach(WikiPage wikiPage in listWikiPageTemp) {
		//      bool alreadyFound=false;
		//      for(int i=0;i<retVal.Count;i++) {
		//        if(retVal[i].PageTitle==wikiPage.PageTitle) {
		//          alreadyFound=true;
		//          break;
		//        }
		//      }
		//      if(!alreadyFound) {
		//        retVal.Add(wikiPage);
		//      }
		//    }
		//    //Match Content third-----------------------------------------------------------------------------------
		//    if(!ignoreContent) {
		//      command=
		//      "SELECT * FROM wikiPage "
		//      +"WHERE PageContent LIKE '%"+searchText+"%' "
		//      +"AND PageTitle NOT LIKE '\\_%' "
		//      +"ORDER BY PageTitle;";
		//      listWikiPageTemp=Crud.WikiPageCrud.SelectMany(command);
		//      foreach(WikiPage wikiPage in listWikiPageTemp) {
		//        bool alreadyFound=false;
		//        for(int i=0;i<retVal.Count;i++) {
		//          if(retVal[i].PageTitle==wikiPage.PageTitle) {
		//            alreadyFound=true;
		//            break;
		//          }
		//        }
		//        if(!alreadyFound) {
		//          retVal.Add(wikiPage);
		//        }
		//      }
		//    }//end !ignoreContent
		//  }
		//  return retVal;
		//}

		///<summary></summary>
		public static List<string> GetForSearch(string searchText,bool searchDeleted,bool ignoreContent) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod(),searchText,searchDeleted,ignoreContent);
			}
			List<string> retVal=new List<string>();
			DataTable tableResults=new DataTable();
			string command="";
			if(searchDeleted) {//SearchDeleted------------------------------------------------------------------------------
				//  //No Search Text-----------------------------------------------------------------------------------
				//  if(searchText=="") {
				//    command=
				//    "SELECT PageTitle FROM wikiPageHist "
				//    +"WHERE PageTitle NOT LIKE '\\_%' "
				//    +"AND IsDeleted=1 "
				//    +"GROUP BY PageTitle "
				//    +"ORDER BY PageTitle";
				//    tableResults=Db.GetTable(command);
				//    for(int i=0;i<tableResults.Rows.Count;i++) {
				//      if(!retVal.Contains(tableResults.Rows[i]["PageTitle"].ToString())){
				//        retVal.Add(tableResults.Rows[i]["PageTitle"].ToString());
				//      }
				//    }
				//    return retVal;
				//  }
				//Match title First-----------------------------------------------------------------------------------
				command=
				"SELECT PageTitle FROM wikiPageHist "
				+"WHERE PageTitle LIKE '%"+searchText+"%' "
				+"AND PageTitle NOT LIKE '\\_%' "
				+"AND IsDeleted=1 "
				+"GROUP BY PageTitle "
				+"ORDER BY PageTitle";
				tableResults=Db.GetTable(command);
				for(int i=0;i<tableResults.Rows.Count;i++) {
					if(!retVal.Contains(tableResults.Rows[i]["PageTitle"].ToString())) {
						retVal.Add(tableResults.Rows[i]["PageTitle"].ToString());
					}
				}
				//Match Content Second-----------------------------------------------------------------------------------
				if(!ignoreContent) {
					command=
					"SELECT PageTitle FROM wikiPageHist "
					+"WHERE PageContent LIKE '%"+searchText+"%' "
					+"AND PageTitle NOT LIKE '\\_%' "
					+"AND IsDeleted=1 "
					+"GROUP BY PageTitle "
					+"ORDER BY PageTitle";
					tableResults=Db.GetTable(command);
					for(int i=0;i<tableResults.Rows.Count;i++) {
						if(!retVal.Contains(tableResults.Rows[i]["PageTitle"].ToString())) {
							retVal.Add(tableResults.Rows[i]["PageTitle"].ToString());
						}
					}
				}
			}
			else {//!SearchDeleted------------------------------------------------------------------------------
				//Match keywords first-----------------------------------------------------------------------------------
				command=
				"SELECT PageTitle FROM wikiPage "
				+"WHERE KeyWords LIKE '%"+searchText+"%' "
				+"AND PageTitle NOT LIKE '\\_%' "
				+"GROUP BY PageTitle "
				+"ORDER BY PageTitle";
					tableResults=Db.GetTable(command);
					for(int i=0;i<tableResults.Rows.Count;i++) {
						if(!retVal.Contains(tableResults.Rows[i]["PageTitle"].ToString())){
							retVal.Add(tableResults.Rows[i]["PageTitle"].ToString());
						}
					}
				//Match PageTitle Second-----------------------------------------------------------------------------------
				command=
				"SELECT PageTitle FROM wikiPage "
				+"WHERE PageTitle LIKE '%"+searchText+"%' "
				+"AND PageTitle NOT LIKE '\\_%' "
				+"GROUP BY PageTitle "
				+"ORDER BY PageTitle";
					tableResults=Db.GetTable(command);
					for(int i=0;i<tableResults.Rows.Count;i++) {
						if(!retVal.Contains(tableResults.Rows[i]["PageTitle"].ToString())){
							retVal.Add(tableResults.Rows[i]["PageTitle"].ToString());
						}
					}
				//Match Content third-----------------------------------------------------------------------------------
				if(!ignoreContent) {
					command=
					"SELECT PageTitle FROM wikiPage "
					+"WHERE PageContent LIKE '%"+searchText+"%' "
					+"AND PageTitle NOT LIKE '\\_%' "
					+"GROUP BY PageTitle "
					+"ORDER BY PageTitle";
					tableResults=Db.GetTable(command);
					for(int i=0;i<tableResults.Rows.Count;i++) {
						if(!retVal.Contains(tableResults.Rows[i]["PageTitle"].ToString())){
							retVal.Add(tableResults.Rows[i]["PageTitle"].ToString());
						}
					}
				}
			}//end search non deleted
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
			string command="UPDATE wikipage SET PageTitle='"+POut.String(newPageTitle)+"'WHERE PageTitle='"+POut.String(wikiPage.PageTitle)+"'";
			Db.NonQ(command);
			command="UPDATE wikipagehist SET PageTitle='"+POut.String(newPageTitle)+"'WHERE PageTitle='"+POut.String(wikiPage.PageTitle)+"'";
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
			#region Basic Xml Validation
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
					return MasterPage.PageContent.Replace("@@@body@@@",ex.Message);
				}
			}
			#endregion
			#region regex replacements
			//[[img:myimage.gif]]------------------------------------------------------------------------------------------------------------
			MatchCollection matches;
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
			//[[InternalLink]]--------------------------------------------------------------------------------------------------------------
			matches=Regex.Matches(s,@"\[\[.+?\]\]");
			foreach(Match match in matches) {
				string tmpStyle="";
				if(GetByTitle(match.Value.Trim('[',']'))==null){//Later, instead of GetByTitle, we should just use a bool method. 
					tmpStyle="class='PageNotExists' ";
				}
				s=s.Replace(match.Value,"<a "+tmpStyle+"href=\""+"wiki:"+match.Value.Trim('[',']')/*.Replace(" ","_")*/+"\">"+match.Value.Trim('[',']')+"</a>");
			}
			//Unordered List----------------------------------------------------------------------------------------------------------------
			//Instead of using a regex, this will hunt through the rows in sequence.
			//later nesting by running ***, then **, then *
			s=ProcessList(s,"*");
			//numbered list---------------------------------------------------------------------------------------------------------------------
			s=ProcessList(s,"#");
			//{{color|red|text}}----------------------------------------------------------------------------------------------------------------
			matches = Regex.Matches(s,"{{(color)(.*?)}}");//.*? matches as few as possible.
			foreach(Match match in matches) {
				string tempText="<span style=\"color:";
				string[] tokens = match.Value.Split('|');
				if(tokens.Length<2) {//not enough tokens
					continue;
				}
				if(tokens[0].Split(':').Length!=2) {//Must have a color token and a color value seperated by a colon, no more no less.
					continue;
				}
				for(int i=0;i<tokens.Length;i++){
					if(i==0) {
						tempText+=tokens[0].Split(':')[1]+";\">";//close <span> tag
						continue;
					}
					tempText+=(i>1?"|":"")+tokens[i];
				}
				tempText=tempText.TrimEnd('}');
				tempText+="</span>";
				s=s.Replace(match.Value,tempText);
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
				if(s.Substring(iScanInParagraph).StartsWith("<b")) {
					tagName="b";
				}
				else if(s.Substring(iScanInParagraph).StartsWith("<img")) {//must be before "i"
					tagName="img";//does not have an ending tag...
				}
				else if(s.Substring(iScanInParagraph).StartsWith("<i")) {
					tagName="i";
				}
				else if(s.Substring(iScanInParagraph).StartsWith("<a")) {
					tagName="a";
				}
				else if(s.Substring(iScanInParagraph).StartsWith("<span")) {
					tagName="span";
				}
				else if(s.Substring(iScanInParagraph).StartsWith("<ul")) {
					tagName="ul";
				}
				else if(s.Substring(iScanInParagraph).StartsWith("<ol")) {
					tagName="ol";
				}
				else if(s.Substring(iScanInParagraph).StartsWith("<h1")) {
					tagName="h1";
				}
				else if(s.Substring(iScanInParagraph).StartsWith("<h2")) {
					tagName="h2";
				}
				else if(s.Substring(iScanInParagraph).StartsWith("<h3")) {
					tagName="h3";
				}
				else if(s.Substring(iScanInParagraph).StartsWith("<table")) {
					tagName="table";
				}
				else {
					throw new ApplicationException("Unexpected tag: "+s.Substring(iScanInParagraph));
				}
				if(tagName=="b" || tagName=="i" || tagName=="a" || tagName=="span"){			
					//scan to the ending tag because this is paragraph content.
					iScanInParagraph=s.IndexOf("</"+tagName+">",iScanInParagraph)+3+tagName.Length;
					//we are still within the paragraph, so loop to keep looking for the end.
				}
				else{
					//the found tag is the beginning of some sibling element such as a list or table.
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
					//move the non-paragraph content over to s new.
					strbSnew.Append(s.Substring(0,iScanSibling));
					s=s.Substring(iScanSibling);
					//scanning will start a totally new paragraph
				}
			}
			strbSnew.Append("</body>");
			#endregion
			#region faulty paragraph grouping
			/*This mostly works, but has at least one critical bug that requires a different approach
			//any line that is just blank with \r\n needs to have a <p> tag so that it won't disappear when we switch to xml
			string[] lines=s.Split(new string[] { "\r\n" },StringSplitOptions.None);//includes empty elements
			StringBuilder strbEmptyLines=new StringBuilder();
			for(int i=0;i<lines.Length;i++) {
				if(lines[i]=="") {
					strbEmptyLines.AppendLine("<p>[[nbsp]]</p>");//because &nbsp; doesn't work in xml.  This gets converted to &nbsp; further down.
				}
				else if(lines[i]=="<body>") {//this means there was a leading CR.
					strbEmptyLines.AppendLine("<body><p>[[nbsp]]</p>");
				}
				else {
					strbEmptyLines.AppendLine(lines[i]);
				}
			}
			s=strbEmptyLines.ToString();
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
			//It's time to look for paragraphs.
			//The basic assumption is that <h123>, <img>, <table>, <ol>, and <ul> tags are external to paragraphs.
			//xhtml does NOT allow a list inside of a paragraph.
			//These objects are siblings to paragraphs.  They will never be surrounded with <p> tags.
			//On the other hand, <b>, <i>, <a>, and <span> tags are always internal to others, including <p>, <h123>, <table>, <ol>, and <ul>.
			//<br> tags will not be allowed anywhere.
			//undecided how to handle newlines within table cells
			StringBuilder strb2=new StringBuilder();
			strb2.Append("<body>");
			string myParagraph="";
			for(int i=0;i<node.ChildNodes.Count;i++) {
				if(node.ChildNodes[i].NodeType==XmlNodeType.Element){
					if(node.ChildNodes[i].Name=="b"
						|| node.ChildNodes[i].Name=="i"
						|| node.ChildNodes[i].Name=="a"
						|| node.ChildNodes[i].Name=="span") 
					{
						myParagraph+=node.ChildNodes[i].OuterXml;
					}
					else {//<h123>, <image>, <table>, <ol>, and <ul>
						if(myParagraph!="") {
							myParagraph=ProcessParagraph(myParagraph);
							strb2.Append(myParagraph);
							myParagraph="";//reset
						}
						strb2.Append(node.ChildNodes[i].OuterXml);
					}
				}
				else if(node.ChildNodes[i].NodeType==XmlNodeType.Text) {
					myParagraph+=node.ChildNodes[i].OuterXml;
				}
				//there shouldn't be any more types.
			}
			if(myParagraph!="") {//the last paragraph was not yet added
				myParagraph=ProcessParagraph(myParagraph);
				strb2.Append(myParagraph);
			}
			strb2.Append("</body>");*/
			#endregion faulty paragraph grouping
			#region aggregation
			doc=new XmlDocument();
			using(StringReader reader=new StringReader(strbSnew.ToString())) {
				try {
					doc.Load(reader);
				}
				catch(Exception ex) {
					return MasterPage.PageContent.Replace("@@@body@@@",ex.Message);
				}
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
			//strbOut.Replace("[[nbsp]]","&nbsp;");//Maybe add back later if needed, but be sure to be careful to not catch this as an internal link to the "nbsp" page in the lines of code above.
			strbOut.Replace("<p></p>","<p>&nbsp;</p>");//probably redundant but harmless
			//aggregate with master
			s=MasterPage.PageContent.Replace("@@@body@@@",strbOut.ToString());
			#endregion aggregation
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
						s=s.Replace(blockOriginal,strb.ToString());
						blockOriginal=null;
					}
				}
			}
			return s;
		}

		///<summary>This will wrap the text in p tags as well as handle internal carriage returns.  startsWithCR is only used on the first paragraph for the unusual case where the entire content starts with a CR.  This prevents stripping it off.</summary>
		private static string ProcessParagraph(string paragraph,bool startsWithCR) {
			if(paragraph=="") {
				return "";
			}
			if(paragraph.StartsWith("\r\n") && !startsWithCR){
				paragraph=paragraph.Substring(2);
			}
			if(paragraph.EndsWith("\r\n")) {//trailing CR remove
				paragraph=paragraph.Substring(0,paragraph.Length-2);
			}
			paragraph="<p>"+paragraph+"</p>";
			paragraph=paragraph.Replace("\r\n","</p><p>");
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