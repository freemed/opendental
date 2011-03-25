using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Allergies{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all Allergies.</summary>
		private static List<Allergy> listt;

		///<summary>A list of all Allergies.</summary>
		public static List<Allergy> Listt{
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
			string command="SELECT * FROM allergy ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Allergy";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.AllergyCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Allergy> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Allergy>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM allergy WHERE PatNum = "+POut.Long(patNum);
			return Crud.AllergyCrud.SelectMany(command);
		}

		///<summary>Gets one Allergy from the db.</summary>
		public static Allergy GetOne(long allergyNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Allergy>(MethodBase.GetCurrentMethod(),allergyNum);
			}
			return Crud.AllergyCrud.SelectOne(allergyNum);
		}

		///<summary></summary>
		public static long Insert(Allergy allergy){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				allergy.AllergyNum=Meth.GetLong(MethodBase.GetCurrentMethod(),allergy);
				return allergy.AllergyNum;
			}
			return Crud.AllergyCrud.Insert(allergy);
		}

		///<summary></summary>
		public static void Update(Allergy allergy){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),allergy);
				return;
			}
			Crud.AllergyCrud.Update(allergy);
		}

		///<summary></summary>
		public static void Delete(long allergyNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),allergyNum);
				return;
			}
			string command= "DELETE FROM allergy WHERE AllergyNum = "+POut.Long(allergyNum);
			Db.NonQ(command);
		}
		*/



	}
}