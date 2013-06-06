using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PatientRaces{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all PatientRaces.</summary>
		private static List<PatientRace> listt;

		///<summary>A list of all PatientRaces.</summary>
		public static List<PatientRace> Listt{
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
			string command="SELECT * FROM patientrace ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="PatientRace";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.PatientRaceCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<PatientRace> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PatientRace>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM patientrace WHERE PatNum = "+POut.Long(patNum);
			return Crud.PatientRaceCrud.SelectMany(command);
		}

		///<summary>Gets one PatientRace from the db.</summary>
		public static PatientRace GetOne(long patientRaceNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<PatientRace>(MethodBase.GetCurrentMethod(),patientRaceNum);
			}
			return Crud.PatientRaceCrud.SelectOne(patientRaceNum);
		}

		///<summary></summary>
		public static long Insert(PatientRace patientRace){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				patientRace.PatientRaceNum=Meth.GetLong(MethodBase.GetCurrentMethod(),patientRace);
				return patientRace.PatientRaceNum;
			}
			return Crud.PatientRaceCrud.Insert(patientRace);
		}

		///<summary></summary>
		public static void Update(PatientRace patientRace){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patientRace);
				return;
			}
			Crud.PatientRaceCrud.Update(patientRace);
		}

		///<summary></summary>
		public static void Delete(long patientRaceNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patientRaceNum);
				return;
			}
			string command= "DELETE FROM patientrace WHERE PatientRaceNum = "+POut.Long(patientRaceNum);
			Db.NonQ(command);
		}
		*/



	}
}