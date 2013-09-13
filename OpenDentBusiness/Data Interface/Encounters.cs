using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Encounters{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all Encounters.</summary>
		private static List<Encounter> listt;

		///<summary>A list of all Encounters.</summary>
		public static List<Encounter> Listt{
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
			string command="SELECT * FROM encounter ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Encounter";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EncounterCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Encounter> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Encounter>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM encounter WHERE PatNum = "+POut.Long(patNum);
			return Crud.EncounterCrud.SelectMany(command);
		}

		///<summary>Gets one Encounter from the db.</summary>
		public static Encounter GetOne(long encounterNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Encounter>(MethodBase.GetCurrentMethod(),encounterNum);
			}
			return Crud.EncounterCrud.SelectOne(encounterNum);
		}

		///<summary></summary>
		public static long Insert(Encounter encounter){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				encounter.EncounterNum=Meth.GetLong(MethodBase.GetCurrentMethod(),encounter);
				return encounter.EncounterNum;
			}
			return Crud.EncounterCrud.Insert(encounter);
		}

		///<summary></summary>
		public static void Update(Encounter encounter){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),encounter);
				return;
			}
			Crud.EncounterCrud.Update(encounter);
		}

		///<summary></summary>
		public static void Delete(long encounterNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),encounterNum);
				return;
			}
			string command= "DELETE FROM encounter WHERE EncounterNum = "+POut.Long(encounterNum);
			Db.NonQ(command);
		}
		*/



	}
}