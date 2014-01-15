using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrPatients{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrPatients.</summary>
		private static List<EhrPatient> listt;

		///<summary>A list of all EhrPatients.</summary>
		public static List<EhrPatient> Listt{
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
			string command="SELECT * FROM ehrpatient ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrPatient";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrPatientCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrPatient> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrPatient>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrpatient WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrPatientCrud.SelectMany(command);
		}

		///<summary>Gets one EhrPatient from the db.</summary>
		public static EhrPatient GetOne(long ehrPatientNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrPatient>(MethodBase.GetCurrentMethod(),ehrPatientNum);
			}
			return Crud.EhrPatientCrud.SelectOne(ehrPatientNum);
		}

		///<summary></summary>
		public static long Insert(EhrPatient ehrPatient){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrPatient.EhrPatientNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrPatient);
				return ehrPatient.EhrPatientNum;
			}
			return Crud.EhrPatientCrud.Insert(ehrPatient);
		}

		///<summary></summary>
		public static void Update(EhrPatient ehrPatient){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrPatient);
				return;
			}
			Crud.EhrPatientCrud.Update(ehrPatient);
		}

		///<summary></summary>
		public static void Delete(long ehrPatientNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrPatientNum);
				return;
			}
			string command= "DELETE FROM ehrpatient WHERE EhrPatientNum = "+POut.Long(ehrPatientNum);
			Db.NonQ(command);
		}
		*/



	}
}