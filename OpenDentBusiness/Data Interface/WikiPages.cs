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

		///<summary></summary>
		public static long Insert(WikiPage wikiPage){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				wikiPage.WikiPageNum=Meth.GetLong(MethodBase.GetCurrentMethod(),wikiPage);
				return wikiPage.WikiPageNum;
			}
			return Crud.WikiPageCrud.Insert(wikiPage);
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
				retVal=retVal.Replace(link.Value,"<a href=\""+"wiki:"+link.Value.Trim("[]".ToCharArray())+"\">"+link.Value.Trim("[]".ToCharArray())+"</a>");
			}
			retVal=retVal.Replace("\r\n\r\n","</p>\r\n<p>").Replace("<p></p>","<p>&nbsp;</p>");
			retVal=retVal.Replace("     ","&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("  ","&nbsp;&nbsp;");
			//retVal.Replace("\r\n","<br />");
			matches = Regex.Matches(retVal,"\\{\\{color|.*?}}");//.*? matches as few as possible.
			foreach(Match colorSegment in matches) {
				string[] tokens = colorSegment.Value.Split('|');
				string tempText="";//text to be colored
				for(int i=0;i<tokens.Length;i++){
					if(i<2){//ignore the color and "#00FF00" values
						continue;
					}
					if(i==tokens.Length-1) {//last token
						tempText+=(i>3?"|":"")+tokens[i].TrimEnd('}');//This allows pipes "|" to be included in the colored text.
					}
					tempText+=(i>3?"|":"")+tokens[i];//This allows pipes "|" to be included in the colored text.
				}
				retVal=retVal.Replace(colorSegment.Value,"<span style=\"color:"+tokens[1]+"\">"+tempText+"</span>");//"+"wiki:"+link.Value.Trim("[]".ToCharArray())+"\">"+link.Value.Trim("[]".ToCharArray())+"</a>");
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