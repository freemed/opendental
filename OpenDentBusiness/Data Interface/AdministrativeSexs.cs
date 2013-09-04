using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AdministrativeSexs{
		//If this table type will exist as cached data, uncomment the CachePattern region below.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all AdministrativeSexs.</summary>
		private static List<AdministrativeSex> listt;

		///<summary>A list of all AdministrativeSexs.</summary>
		public static List<AdministrativeSex> Listt{
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
			string command="SELECT * FROM administrativesex ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AdministrativeSex";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.AdministrativeSexCrud.TableToList(table);
		}
		#endregion
		*/

		///<summary></summary>
		public static long Insert(AdministrativeSex administrativeSex){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				administrativeSex.AdministrativeSexNum=Meth.GetLong(MethodBase.GetCurrentMethod(),administrativeSex);
				return administrativeSex.AdministrativeSexNum;
			}
			return Crud.AdministrativeSexCrud.Insert(administrativeSex);
		}


		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<AdministrativeSex> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<AdministrativeSex>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM administrativesex WHERE PatNum = "+POut.Long(patNum);
			return Crud.AdministrativeSexCrud.SelectMany(command);
		}

		///<summary>Gets one AdministrativeSex from the db.</summary>
		public static AdministrativeSex GetOne(long administrativeSexNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<AdministrativeSex>(MethodBase.GetCurrentMethod(),administrativeSexNum);
			}
			return Crud.AdministrativeSexCrud.SelectOne(administrativeSexNum);
		}

		///<summary></summary>
		public static void Update(AdministrativeSex administrativeSex){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),administrativeSex);
				return;
			}
			Crud.AdministrativeSexCrud.Update(administrativeSex);
		}

		///<summary></summary>
		public static void Delete(long administrativeSexNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),administrativeSexNum);
				return;
			}
			string command= "DELETE FROM administrativesex WHERE AdministrativeSexNum = "+POut.Long(administrativeSexNum);
			Db.NonQ(command);
		}
		*/



	}
}