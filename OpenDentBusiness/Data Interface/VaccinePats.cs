using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class VaccinePats{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all VaccinePats.</summary>
		private static List<VaccinePat> listt;

		///<summary>A list of all VaccinePats.</summary>
		public static List<VaccinePat> Listt{
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
			string command="SELECT * FROM vaccinepat ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="VaccinePat";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.VaccinePatCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<VaccinePat> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<VaccinePat>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM vaccinepat WHERE PatNum = "+POut.Long(patNum);
			return Crud.VaccinePatCrud.SelectMany(command);
		}

		///<summary>Gets one VaccinePat from the db.</summary>
		public static VaccinePat GetOne(long vaccinePatNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<VaccinePat>(MethodBase.GetCurrentMethod(),vaccinePatNum);
			}
			return Crud.VaccinePatCrud.SelectOne(vaccinePatNum);
		}

		///<summary></summary>
		public static long Insert(VaccinePat vaccinePat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				vaccinePat.VaccinePatNum=Meth.GetLong(MethodBase.GetCurrentMethod(),vaccinePat);
				return vaccinePat.VaccinePatNum;
			}
			return Crud.VaccinePatCrud.Insert(vaccinePat);
		}

		///<summary></summary>
		public static void Update(VaccinePat vaccinePat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),vaccinePat);
				return;
			}
			Crud.VaccinePatCrud.Update(vaccinePat);
		}

		///<summary></summary>
		public static void Delete(long vaccinePatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),vaccinePatNum);
				return;
			}
			string command= "DELETE FROM vaccinepat WHERE VaccinePatNum = "+POut.Long(vaccinePatNum);
			Db.NonQ(command);
		}
		*/



	}
}