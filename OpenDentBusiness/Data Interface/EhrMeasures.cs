using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrMeasures{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrMeasures.</summary>
		private static List<EhrMeasure> listt;

		///<summary>A list of all EhrMeasures.</summary>
		public static List<EhrMeasure> Listt{
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
			string command="SELECT * FROM ehrmeasure ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrMeasure";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrMeasureCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrMeasure> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrMeasure>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrmeasure WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrMeasureCrud.SelectMany(command);
		}

		///<summary>Gets one EhrMeasure from the db.</summary>
		public static EhrMeasure GetOne(long ehrMeasureNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrMeasure>(MethodBase.GetCurrentMethod(),ehrMeasureNum);
			}
			return Crud.EhrMeasureCrud.SelectOne(ehrMeasureNum);
		}

		///<summary></summary>
		public static long Insert(EhrMeasure ehrMeasure){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrMeasure.EhrMeasureNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrMeasure);
				return ehrMeasure.EhrMeasureNum;
			}
			return Crud.EhrMeasureCrud.Insert(ehrMeasure);
		}

		///<summary></summary>
		public static void Update(EhrMeasure ehrMeasure){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrMeasure);
				return;
			}
			Crud.EhrMeasureCrud.Update(ehrMeasure);
		}

		///<summary></summary>
		public static void Delete(long ehrMeasureNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrMeasureNum);
				return;
			}
			string command= "DELETE FROM ehrmeasure WHERE EhrMeasureNum = "+POut.Long(ehrMeasureNum);
			Db.NonQ(command);
		}
		*/



	}
}