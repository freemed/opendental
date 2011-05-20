using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrMeasureEvents{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrMeasureEvents.</summary>
		private static List<EhrMeasureEvent> listt;

		///<summary>A list of all EhrMeasureEvents.</summary>
		public static List<EhrMeasureEvent> Listt{
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
			string command="SELECT * FROM ehrmeasureevent ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrMeasureEvent";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrMeasureEventCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrMeasureEvent> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrMeasureEvent>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrmeasureevent WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrMeasureEventCrud.SelectMany(command);
		}

		///<summary>Gets one EhrMeasureEvent from the db.</summary>
		public static EhrMeasureEvent GetOne(long ehrMeasureEventNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrMeasureEvent>(MethodBase.GetCurrentMethod(),ehrMeasureEventNum);
			}
			return Crud.EhrMeasureEventCrud.SelectOne(ehrMeasureEventNum);
		}

		///<summary></summary>
		public static long Insert(EhrMeasureEvent ehrMeasureEvent){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrMeasureEvent.EhrMeasureEventNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrMeasureEvent);
				return ehrMeasureEvent.EhrMeasureEventNum;
			}
			return Crud.EhrMeasureEventCrud.Insert(ehrMeasureEvent);
		}

		///<summary></summary>
		public static void Update(EhrMeasureEvent ehrMeasureEvent){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrMeasureEvent);
				return;
			}
			Crud.EhrMeasureEventCrud.Update(ehrMeasureEvent);
		}

		///<summary></summary>
		public static void Delete(long ehrMeasureEventNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrMeasureEventNum);
				return;
			}
			string command= "DELETE FROM ehrmeasureevent WHERE EhrMeasureEventNum = "+POut.Long(ehrMeasureEventNum);
			Db.NonQ(command);
		}
		*/



	}
}