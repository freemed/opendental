using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AdministrativeSexes{
		//If this table type will exist as cached data, uncomment the CachePattern region below.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all AdministrativeGenders.</summary>
		private static List<AdministrativeGender> listt;

		///<summary>A list of all AdministrativeGenders.</summary>
		public static List<AdministrativeGender> Listt{
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
			string command="SELECT * FROM administrativegender ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AdministrativeGender";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.AdministrativeGenderCrud.TableToList(table);
		}
		#endregion
		*/

		///<summary></summary>
		public static long Insert(AdministrativeSex administrativeSex) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				administrativeSex.AdministrativeSexNum=Meth.GetLong(MethodBase.GetCurrentMethod(),administrativeSex);
				return administrativeSex.AdministrativeSexNum;
			}
			return Crud.AdministrativeSexCrud.Insert(administrativeSex);
		}

		///<summary></summary>
		public static List<AdministrativeSex> GetAll(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<AdministrativeSex>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM administrativesex";
			return Crud.AdministrativeSexCrud.SelectMany(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary>Gets one AdministrativeGender from the db.</summary>
		public static AdministrativeGender GetOne(long administrativeGenderNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<AdministrativeGender>(MethodBase.GetCurrentMethod(),administrativeGenderNum);
			}
			return Crud.AdministrativeGenderCrud.SelectOne(administrativeGenderNum);
		}

		///<summary></summary>
		public static long Insert(AdministrativeGender administrativeGender){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				administrativeGender.AdministrativeGenderNum=Meth.GetLong(MethodBase.GetCurrentMethod(),administrativeGender);
				return administrativeGender.AdministrativeGenderNum;
			}
			return Crud.AdministrativeGenderCrud.Insert(administrativeGender);
		}

		///<summary></summary>
		public static void Update(AdministrativeGender administrativeGender){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),administrativeGender);
				return;
			}
			Crud.AdministrativeGenderCrud.Update(administrativeGender);
		}

		///<summary></summary>
		public static void Delete(long administrativeGenderNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),administrativeGenderNum);
				return;
			}
			string command= "DELETE FROM administrativegender WHERE AdministrativeGenderNum = "+POut.Long(administrativeGenderNum);
			Db.NonQ(command);
		}
		*/



	}
}