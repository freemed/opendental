using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrLabs{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrLabs.</summary>
		private static List<EhrLab> listt;

		///<summary>A list of all EhrLabs.</summary>
		public static List<EhrLab> Listt{
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
			string command="SELECT * FROM ehrlab ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrLab";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrLabCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrLab> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrLab>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrlab WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrLabCrud.SelectMany(command);
		}

		///<summary>Gets one EhrLab from the db.</summary>
		public static EhrLab GetOne(long ehrLabNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrLab>(MethodBase.GetCurrentMethod(),ehrLabNum);
			}
			return Crud.EhrLabCrud.SelectOne(ehrLabNum);
		}

		///<summary></summary>
		public static long Insert(EhrLab ehrLab){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrLab.EhrLabNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrLab);
				return ehrLab.EhrLabNum;
			}
			return Crud.EhrLabCrud.Insert(ehrLab);
		}

		///<summary></summary>
		public static void Update(EhrLab ehrLab){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrLab);
				return;
			}
			Crud.EhrLabCrud.Update(ehrLab);
		}

		///<summary></summary>
		public static void Delete(long ehrLabNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrLabNum);
				return;
			}
			string command= "DELETE FROM ehrlab WHERE EhrLabNum = "+POut.Long(ehrLabNum);
			Db.NonQ(command);
		}
		*/



	}
}