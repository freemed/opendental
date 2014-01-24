using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrAptObses{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrAptObses.</summary>
		private static List<EhrAptObs> listt;

		///<summary>A list of all EhrAptObses.</summary>
		public static List<EhrAptObs> Listt{
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
			string command="SELECT * FROM ehraptobs ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrAptObs";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrAptObsCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrAptObs> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrAptObs>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehraptobs WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrAptObsCrud.SelectMany(command);
		}

		///<summary>Gets one EhrAptObs from the db.</summary>
		public static EhrAptObs GetOne(long ehrAptObsNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrAptObs>(MethodBase.GetCurrentMethod(),ehrAptObsNum);
			}
			return Crud.EhrAptObsCrud.SelectOne(ehrAptObsNum);
		}

		///<summary></summary>
		public static long Insert(EhrAptObs ehrAptObs){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrAptObs.EhrAptObsNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrAptObs);
				return ehrAptObs.EhrAptObsNum;
			}
			return Crud.EhrAptObsCrud.Insert(ehrAptObs);
		}

		///<summary></summary>
		public static void Update(EhrAptObs ehrAptObs){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrAptObs);
				return;
			}
			Crud.EhrAptObsCrud.Update(ehrAptObs);
		}

		///<summary></summary>
		public static void Delete(long ehrAptObsNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrAptObsNum);
				return;
			}
			string command= "DELETE FROM ehraptobs WHERE EhrAptObsNum = "+POut.Long(ehrAptObsNum);
			Db.NonQ(command);
		}
		*/



	}
}