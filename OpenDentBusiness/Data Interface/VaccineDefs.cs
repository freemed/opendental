using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class VaccineDefs{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all VaccineDefs.</summary>
		private static List<VaccineDef> listt;

		///<summary>A list of all VaccineDefs.</summary>
		public static List<VaccineDef> Listt{
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
			string command="SELECT * FROM vaccinedef ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="VaccineDef";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.VaccineDefCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<VaccineDef> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<VaccineDef>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM vaccinedef WHERE PatNum = "+POut.Long(patNum);
			return Crud.VaccineDefCrud.SelectMany(command);
		}

		///<summary>Gets one VaccineDef from the db.</summary>
		public static VaccineDef GetOne(long vaccineDefNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<VaccineDef>(MethodBase.GetCurrentMethod(),vaccineDefNum);
			}
			return Crud.VaccineDefCrud.SelectOne(vaccineDefNum);
		}

		///<summary></summary>
		public static long Insert(VaccineDef vaccineDef){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				vaccineDef.VaccineDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),vaccineDef);
				return vaccineDef.VaccineDefNum;
			}
			return Crud.VaccineDefCrud.Insert(vaccineDef);
		}

		///<summary></summary>
		public static void Update(VaccineDef vaccineDef){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),vaccineDef);
				return;
			}
			Crud.VaccineDefCrud.Update(vaccineDef);
		}

		///<summary></summary>
		public static void Delete(long vaccineDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),vaccineDefNum);
				return;
			}
			string command= "DELETE FROM vaccinedef WHERE VaccineDefNum = "+POut.Long(vaccineDefNum);
			Db.NonQ(command);
		}
		*/



	}
}