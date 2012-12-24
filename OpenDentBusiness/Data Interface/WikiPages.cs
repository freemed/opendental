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

		///<summary>Returns a list of all historical version of "PageName"</summary>
		public static List<WikiPage> GetHistoryByTitle(string PageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod(),PageTitle);
			}
			string command="SELECT * FROM wikipage WHERE PageTitle='"+PageTitle+"' ORDER BY DateTimeSaved DESC;";
			return Crud.WikiPageCrud.SelectMany(command);
		}

		///<summary>Returns a list of all historical version of "PageName"</summary>
		public static List<WikiPage> GetIncomingLinks(string PageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod(),PageTitle);
			}
			List<WikiPage> retVal=new List<WikiPage>();
			string command="SELECT PageTitle FROM wikipage WHERE PageContent LIKE '%[["+PageTitle+"]]%' GROUP BY PageTitle;";
			Db.GetTable(command);
			//historical versions may contain a link when the current version does not. Filter those results out.
			foreach(DataRow pageTitleRow in Db.GetTable(command).Rows){
				WikiPage tempPage=GetByTitle(pageTitleRow[0].ToString());
				if(tempPage.PageContent.Contains("[["+PageTitle+"]]")){
					retVal.Add(tempPage);
				}
			}
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
			command="UPDATE wikipage SET PageContent=REPLACE(t.PageContent,'[["+OriginalPageTitle+@"]]', '[["+NewPageTitle+@"]]')";
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
			//Replace internal Links
			MatchCollection matches = Regex.Matches(retVal,"\\[\\[.*?\\]\\]");//.*? matches as few as possible.
			foreach(Match link in matches) {
				string tmpStyle="";
				if(GetByTitle(link.Value.Trim("[]".ToCharArray()))==null || 
					GetByTitle(link.Value.Trim("[]".ToCharArray())).IsDeleted) 
				{
					tmpStyle="class='PageNotExists '";
				}
				retVal=retVal.Replace(link.Value,"<a "+tmpStyle+"href=\""+"wiki:"+link.Value.Trim("[]".ToCharArray())+"\">"+link.Value.Trim("[]".ToCharArray())+"</a>");
			}


			retVal=retVal.Replace("\r\n\r\n","</p>\r\n<p>").Replace("<p></p>","<p>&nbsp;</p>");
			retVal=retVal.Replace("     ","&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("  ","&nbsp;&nbsp;");
			//retVal.Replace("\r\n","<br />");
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
			//TODO: Image Tags
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




	}
}