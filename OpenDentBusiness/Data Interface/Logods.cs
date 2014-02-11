using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Logods{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all Logods.</summary>
		private static List<Logod> listt;

		///<summary>A list of all Logods.</summary>
		public static List<Logod> Listt{
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
			string command="SELECT * FROM logod ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Logod";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.LogodCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Logod> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Logod>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM logod WHERE PatNum = "+POut.Long(patNum);
			return Crud.LogodCrud.SelectMany(command);
		}

		///<summary>Gets one Logod from the db.</summary>
		public static Logod GetOne(long logNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Logod>(MethodBase.GetCurrentMethod(),logNum);
			}
			return Crud.LogodCrud.SelectOne(logNum);
		}

		///<summary></summary>
		public static long Insert(Logod logod){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				logod.LogNum=Meth.GetLong(MethodBase.GetCurrentMethod(),logod);
				return logod.LogNum;
			}
			return Crud.LogodCrud.Insert(logod);
		}

		///<summary></summary>
		public static void Update(Logod logod){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),logod);
				return;
			}
			Crud.LogodCrud.Update(logod);
		}

		///<summary></summary>
		public static void Delete(long logNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),logNum);
				return;
			}
			string command= "DELETE FROM logod WHERE LogNum = "+POut.Long(logNum);
			Db.NonQ(command);
		}
		*/



	}
}