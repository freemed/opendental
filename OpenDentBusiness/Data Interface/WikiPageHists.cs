using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class WikiPageHists{

		///<summary>Ordered by dateTimeSaved.</summary>
		public static List<WikiPageHist> GetByTitle(string pageTitle){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPageHist>>(MethodBase.GetCurrentMethod(),pageTitle);
			}
			string command="SELECT * FROM wikipagehist WHERE PageTitle = '"+POut.String(pageTitle)+"' ORDER BY DateTimeSaved";
			return Crud.WikiPageHistCrud.SelectMany(command);
		}

		///<summary>Only returns the most recently deleted version of the page. Returns null if not found.</summary>
		public static WikiPageHist GetDeletedByTitle(string pageTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<WikiPageHist>(MethodBase.GetCurrentMethod(),pageTitle);
			}
			string command="SELECT * FROM wikipagehist "
										+"WHERE PageTitle = '"+POut.String(pageTitle)+"' "
										+"AND IsDeleted=1 "
										+"AND DateTimeSaved="
											+"(SELECT MAX(DateTimeSaved) "
											+"FROM wikipagehist "
											+"WHERE PageTitle = '"+POut.String(pageTitle)+"' "
											+"AND IsDeleted=1)"
											;
			return Crud.WikiPageHistCrud.SelectOne(command);
		}

		///<summary></summary>
		public static long Insert(WikiPageHist wikiPageHist){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				wikiPageHist.WikiPageNum=Meth.GetLong(MethodBase.GetCurrentMethod(),wikiPageHist);
				return wikiPageHist.WikiPageNum;
			}
			return Crud.WikiPageHistCrud.Insert(wikiPageHist);
		}



		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary>Gets one WikiPageHist from the db.</summary>
		public static WikiPageHist GetOne(long wikiPageNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<WikiPageHist>(MethodBase.GetCurrentMethod(),wikiPageNum);
			}
			return Crud.WikiPageHistCrud.SelectOne(wikiPageNum);
		}

		///<summary></summary>
		public static void Update(WikiPageHist wikiPageHist){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),wikiPageHist);
				return;
			}
			Crud.WikiPageHistCrud.Update(wikiPageHist);
		}

		///<summary></summary>
		public static void Delete(long wikiPageNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),wikiPageNum);
				return;
			}
			string command= "DELETE FROM wikipagehist WHERE WikiPageNum = "+POut.Long(wikiPageNum);
			Db.NonQ(command);
		}
		*/



	}
}