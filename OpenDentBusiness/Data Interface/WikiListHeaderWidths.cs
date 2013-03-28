using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class WikiListHeaderWidths{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all WikiListHeaderWidths.</summary>
		private static List<WikiListHeaderWidth> listt;

		///<summary>A list of all WikiListHeaderWidths.</summary>
		public static List<WikiListHeaderWidth> Listt{
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
			string command="SELECT * FROM wikilistheaderwidth ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="WikiListHeaderWidth";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.WikiListHeaderWidthCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<WikiListHeaderWidth> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiListHeaderWidth>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM wikilistheaderwidth WHERE PatNum = "+POut.Long(patNum);
			return Crud.WikiListHeaderWidthCrud.SelectMany(command);
		}

		///<summary>Gets one WikiListHeaderWidth from the db.</summary>
		public static WikiListHeaderWidth GetOne(long wikiListHeaderWidthNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<WikiListHeaderWidth>(MethodBase.GetCurrentMethod(),wikiListHeaderWidthNum);
			}
			return Crud.WikiListHeaderWidthCrud.SelectOne(wikiListHeaderWidthNum);
		}

		///<summary></summary>
		public static long Insert(WikiListHeaderWidth wikiListHeaderWidth){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				wikiListHeaderWidth.WikiListHeaderWidthNum=Meth.GetLong(MethodBase.GetCurrentMethod(),wikiListHeaderWidth);
				return wikiListHeaderWidth.WikiListHeaderWidthNum;
			}
			return Crud.WikiListHeaderWidthCrud.Insert(wikiListHeaderWidth);
		}

		///<summary></summary>
		public static void Update(WikiListHeaderWidth wikiListHeaderWidth){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),wikiListHeaderWidth);
				return;
			}
			Crud.WikiListHeaderWidthCrud.Update(wikiListHeaderWidth);
		}

		///<summary></summary>
		public static void Delete(long wikiListHeaderWidthNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),wikiListHeaderWidthNum);
				return;
			}
			string command= "DELETE FROM wikilistheaderwidth WHERE WikiListHeaderWidthNum = "+POut.Long(wikiListHeaderWidthNum);
			Db.NonQ(command);
		}
		*/



	}
}