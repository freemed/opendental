using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrNotPerformeds{
		//If this table type will exist as cached data, uncomment the CachePattern region below.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrNotPerformeds.</summary>
		private static List<EhrNotPerformed> listt;

		///<summary>A list of all EhrNotPerformeds.</summary>
		public static List<EhrNotPerformed> Listt{
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
			string command="SELECT * FROM ehrnotperformed ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrNotPerformed";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrNotPerformedCrud.TableToList(table);
		}
		#endregion
		*/

		///<summary></summary>
		public static List<EhrNotPerformed> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrNotPerformed>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrnotperformed WHERE PatNum = "+POut.Long(patNum)+" ORDER BY DateEntry";
			return Crud.EhrNotPerformedCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(EhrNotPerformed ehrNotPerformed){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrNotPerformed.EhrNotPerformedNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrNotPerformed);
				return ehrNotPerformed.EhrNotPerformedNum;
			}
			return Crud.EhrNotPerformedCrud.Insert(ehrNotPerformed);
		}

		///<summary></summary>
		public static void Update(EhrNotPerformed ehrNotPerformed){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrNotPerformed);
				return;
			}
			Crud.EhrNotPerformedCrud.Update(ehrNotPerformed);
		}

		///<summary></summary>
		public static void Delete(long ehrNotPerformedNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrNotPerformedNum);
				return;
			}
			string command= "DELETE FROM ehrnotperformed WHERE EhrNotPerformedNum = "+POut.Long(ehrNotPerformedNum);
			Db.NonQ(command);
		}

		///<summary>Gets one EhrNotPerformed from the db.</summary>
		public static EhrNotPerformed GetOne(long ehrNotPerformedNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<EhrNotPerformed>(MethodBase.GetCurrentMethod(),ehrNotPerformedNum);
			}
			return Crud.EhrNotPerformedCrud.SelectOne(ehrNotPerformedNum);
		}



	}
}