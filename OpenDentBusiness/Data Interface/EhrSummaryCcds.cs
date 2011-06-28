using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrSummaryCcds{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrSummaryCcds.</summary>
		private static List<EhrSummaryCcd> listt;

		///<summary>A list of all EhrSummaryCcds.</summary>
		public static List<EhrSummaryCcd> Listt{
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
			string command="SELECT * FROM ehrsummaryccd ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrSummaryCcd";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrSummaryCcdCrud.TableToList(table);
		}
		#endregion
		
		///<summary></summary>
		public static List<EhrSummaryCcd> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrSummaryCcd>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrsummaryccd WHERE PatNum = "+POut.Long(patNum)+" ORDER BY DateSummary ASC";
			return Crud.EhrSummaryCcdCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(EhrSummaryCcd ehrSummaryCcd){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrSummaryCcd.EhrSummaryCcdNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrSummaryCcd);
				return ehrSummaryCcd.EhrSummaryCcdNum;
			}
			return Crud.EhrSummaryCcdCrud.Insert(ehrSummaryCcd);
		}
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.


		///<summary>Gets one EhrSummaryCcd from the db.</summary>
		public static EhrSummaryCcd GetOne(long ehrSummaryCcdNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrSummaryCcd>(MethodBase.GetCurrentMethod(),ehrSummaryCcdNum);
			}
			return Crud.EhrSummaryCcdCrud.SelectOne(ehrSummaryCcdNum);
		}

		///<summary></summary>
		public static void Update(EhrSummaryCcd ehrSummaryCcd){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrSummaryCcd);
				return;
			}
			Crud.EhrSummaryCcdCrud.Update(ehrSummaryCcd);
		}

		///<summary></summary>
		public static void Delete(long ehrSummaryCcdNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrSummaryCcdNum);
				return;
			}
			string command= "DELETE FROM ehrsummaryccd WHERE EhrSummaryCcdNum = "+POut.Long(ehrSummaryCcdNum);
			Db.NonQ(command);
		}
		*/



	}
}