using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class WikiPageHists{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all WikiPageHists.</summary>
		private static List<WikiPageHist> listt;

		///<summary>A list of all WikiPageHists.</summary>
		public static List<WikiPageHist> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM wikipagehist ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="WikiPageHist";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.WikiPageHistCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<WikiPageHist> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPageHist>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM wikipagehist WHERE PatNum = "+POut.Long(patNum);
			return Crud.WikiPageHistCrud.SelectMany(command);
		}

		///<summary>Gets one WikiPageHist from the db.</summary>
		public static WikiPageHist GetOne(long wikiPageNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<WikiPageHist>(MethodBase.GetCurrentMethod(),wikiPageNum);
			}
			return Crud.WikiPageHistCrud.SelectOne(wikiPageNum);
		}

		///<summary></summary>
		public static long Insert(WikiPageHist wikiPageHist){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				wikiPageHist.WikiPageNum=Meth.GetLong(MethodBase.GetCurrentMethod(),wikiPageHist);
				return wikiPageHist.WikiPageNum;
			}
			return Crud.WikiPageHistCrud.Insert(wikiPageHist);
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