using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary></summary>
	public class WikiPages{

		///<summary>Gets one WikiPage from the db.</summary>
		public static WikiPage GetMaster() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<WikiPage>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM wikipage WHERE PageTitle='_Master' and DateTimeSaved=(SELECT MAX(DateTimeSaved) FROM wikipage WHERE PageTitle='_Master');";
			return Crud.WikiPageCrud.SelectOne(command);
		}

		public static WikiPage GetStyle() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<WikiPage>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM wikipage WHERE PageTitle='_Style' and DateTimeSaved=(SELECT MAX(DateTimeSaved) FROM wikipage WHERE PageTitle='_Style');";
			return Crud.WikiPageCrud.SelectOne(command);
		}

		///<summary>Returns null if page does not exist.</summary>
		public static WikiPage GetByTitle(string PageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<WikiPage>(MethodBase.GetCurrentMethod(),PageTitle);
			}
			string command="SELECT * FROM wikipage WHERE PageTitle='"+PageTitle+"' and DateTimeSaved=(SELECT MAX(DateTimeSaved) FROM wikipage WHERE PageTitle='"+PageTitle+"');";
			return Crud.WikiPageCrud.SelectOne(command);
		}

		///<summary>Returns a list of all historical version of "PageTitle"</summary>
		public static List<WikiPage> GetHistoryByTitle(string PageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod(),PageTitle);
			}
			string command="SELECT * FROM wikipage WHERE PageTitle='"+PageTitle+"' ORDER BY DateTimeSaved DESC;";
			return Crud.WikiPageCrud.SelectMany(command);
		}

		///<summary>Returns a list of all pages that reference "PageTitle"</summary>
		public static List<WikiPage> GetIncomingLinks(string PageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod(),PageTitle);
			}
			List<WikiPage> retVal=new List<WikiPage>();
			string command="SELECT PageTitle FROM wikipage WHERE PageContent LIKE '%[["+PageTitle+"]]%' GROUP BY PageTitle;";
			//Db.GetTable(command);
			//historical versions may contain a link when the current version does not. Filter those results out.
			foreach(DataRow pageTitleRow in Db.GetTable(command).Rows){
				WikiPage tempPage=GetByTitle(pageTitleRow[0].ToString());
				if(tempPage.PageContent.Contains("[["+PageTitle+"]]")){
					retVal.Add(tempPage);
				}
			}
			return retVal;
		}

		///<summary>Returns a list of all current versions of all pages.</summary>
		public static List<WikiPage> GetAllCurrent() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod());
			}
			List<WikiPage> retVal=new List<WikiPage>();
			string command="SELECT * FROM wikipage WHERE PageTitle NOT LIKE '\\_%' ORDER BY DateTimeSaved DESC;";
			List<WikiPage> listAllWikiPages=Crud.WikiPageCrud.SelectMany(command);
			//Return only the newest version of each page. Since they are odered by date, the newest versions will be the first added and all subsequent editions will be idgnored.
			foreach(WikiPage wikiPage in listAllWikiPages) {
				bool found=true;
				foreach(WikiPage foundPage in retVal) {
					if(wikiPage.PageTitle==foundPage.PageTitle) {
						found=false;
						break;
					}
				}
				if(found) {
					retVal.Add(wikiPage);
				}
			}
			retVal.Sort(SortWikiPagesByName);
			return retVal;
		}

		///<summary></summary>
		public static long Insert(WikiPage WikiPage){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				WikiPage.WikiPageNum=Meth.GetLong(MethodBase.GetCurrentMethod(),WikiPage);
				return WikiPage.WikiPageNum;
			}
			return Crud.WikiPageCrud.Insert(WikiPage);
		}

		///<summary></summary>
		public static void Rename(string OriginalPageTitle, string NewPageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),OriginalPageTitle,NewPageTitle);
				return;
			}
			//Create new entry to track changes by user.
			WikiPage tempWP = GetByTitle(OriginalPageTitle);
			tempWP.UserNum=Security.CurUser.UserNum;
			Insert(tempWP);
			//Actually rename pages and all previous versions.
			string command="UPDATE wikipage SET PageTitle='"+NewPageTitle+"'WHERE PageTitle='"+OriginalPageTitle+"';";
			Db.NonQ(command);
			//Fix all broken internal links by inserting a new copy of each page if teh newest revision of that page has a link to the renamed page.
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
			//Alternate, simple fix-links query
			command="UPDATE wikipage SET PageContent=REPLACE(PageContent,'[["+OriginalPageTitle+@"]]', '[["+NewPageTitle+@"]]')";
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

		public static string TranslateToXhtml(string wikiContent) {
			string retVal="";
			retVal+=wikiContent;
			retVal=retVal.Replace("&<","&lt;").Replace("&>","&gt;");
			retVal=retVal.Replace("<body>","<body><p>").Replace("</body>","</p></body>");
			//replace image tags [[img:myimage.gif]]
			MatchCollection matches = Regex.Matches(retVal,"\\[\\[(img:).*?\\]\\]");//^(\\s|\\d) because color codes are being replaced.
			foreach(Match imageMatch in matches) {
				string imgName = imageMatch.Value.Substring(imageMatch.Value.IndexOf(":")+1).TrimEnd("]".ToCharArray());
				//retVal=retVal.Replace(imageMatch.Value,"<img src=\"file://///server/OpenDentImages/wiki/"+imgName+"\" />");
				retVal=retVal.Replace(imageMatch.Value,"<img src=\"file://///serverfiles/Storage/My/Ryan/images/"+imgName+"\" />");
			}
			//replace image tags [[img:myimage.gif]]
			matches = Regex.Matches(retVal,"\\[\\[(keywords:).*?\\]\\]");//^(\\s|\\d) because color codes are being replaced.
			foreach(Match keywords in matches) {//should be only one
				retVal=retVal.Replace(keywords.Value,"<span class=\"keywords\">keywords:"+keywords.Value.Substring(11).TrimEnd("]".ToCharArray()));
			}
			//Replace internal Links
			matches = Regex.Matches(retVal,"\\[\\[.*?\\]\\]");//.*? matches as few as possible.
			foreach(Match link in matches) {
				string tmpStyle="";
				if(GetByTitle(link.Value.Trim("[]".ToCharArray()))==null || 
					GetByTitle(link.Value.Trim("[]".ToCharArray())).IsDeleted) 
				{
					tmpStyle="class='PageNotExists '";
				}
				retVal=retVal.Replace(link.Value,"<a "+tmpStyle+"href=\""+"wiki:"+link.Value.Trim("[]".ToCharArray())+"\">"+link.Value.Trim("[]".ToCharArray())+"</a>");
			}
			//construct unordered lists.
			matches = Regex.Matches(retVal,"\\*[\\S](.|[\\r\\n][^(\\r\\n)])+");//(.|[\\n][^\\n])+[\\n][\\n]");
			foreach(Match unorderedList in matches) {
				string[] tokens = unorderedList.Value.Split('*');
				StringBuilder listBuilder = new StringBuilder();
				listBuilder.Append("<ul>\r\n");
				foreach(string listItem in tokens) {
					if(listItem!="") {
						listBuilder.Append("<li>"+listItem+"</li>\r\n");
					}
				}
				listBuilder.Append("</ul>\r\n");
				retVal=retVal.Replace(unorderedList.Value,listBuilder.ToString());
			}
			//construct ordered lists.
			matches = Regex.Matches(retVal,"#[^(\\s|\\d)](.|[\\r\\n][^(\\r\\n)])+");//^(\\s|\\d) because color codes are being replaced.
			foreach(Match unorderedList in matches) {
				string[] tokens = unorderedList.Value.Split('#');
				StringBuilder listBuilder = new StringBuilder();
				listBuilder.Append("<ol>\r\n");
				foreach(string listItem in tokens) {
					if(listItem!="") {
						listBuilder.Append("<li>"+listItem+"</li>\r\n");
					}
				}
				listBuilder.Append("</ol>\r\n");
				retVal=retVal.Replace(unorderedList.Value,listBuilder.ToString());
			}
			retVal=retVal.Replace("\r\n\r\n","</p>\r\n<p>").Replace("<p></p>","<p>&nbsp;</p>");
			retVal=retVal.Replace("     ","&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("  ","&nbsp;&nbsp;");
			//retVal.Replace("\r\n","<br />");
			//Color tags
			matches = Regex.Matches(retVal,"{{(color)(.*?)}}");//.*? matches as few as possible.
			foreach(Match colorSegment in matches) {
				string[] tokens = colorSegment.Value.Split('|');
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
				retVal=retVal.Replace(colorSegment.Value,"<span style=\"color:"+tokens[1]+";\">"+tempText+"</span>");
			}
			return retVal;
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

		private static int SortWikiPagesByName(WikiPage wp1,WikiPage wp2) {
			return wp1.PageTitle.CompareTo(wp2.PageTitle);
		}



	}
}