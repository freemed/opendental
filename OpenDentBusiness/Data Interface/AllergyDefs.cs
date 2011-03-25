using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AllergyDefs{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all AllergyDefs.</summary>
		private static List<AllergyDef> listt;

		///<summary>A list of all AllergyDefs.</summary>
		public static List<AllergyDef> Listt{
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
			string command="SELECT * FROM allergydef ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AllergyDef";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.AllergyDefCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<AllergyDef> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<AllergyDef>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM allergydef WHERE PatNum = "+POut.Long(patNum);
			return Crud.AllergyDefCrud.SelectMany(command);
		}

		///<summary>Gets one AllergyDef from the db.</summary>
		public static AllergyDef GetOne(long allergyDefNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<AllergyDef>(MethodBase.GetCurrentMethod(),allergyDefNum);
			}
			return Crud.AllergyDefCrud.SelectOne(allergyDefNum);
		}

		///<summary></summary>
		public static long Insert(AllergyDef allergyDef){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				allergyDef.AllergyDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),allergyDef);
				return allergyDef.AllergyDefNum;
			}
			return Crud.AllergyDefCrud.Insert(allergyDef);
		}

		///<summary></summary>
		public static void Update(AllergyDef allergyDef){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),allergyDef);
				return;
			}
			Crud.AllergyDefCrud.Update(allergyDef);
		}

		///<summary></summary>
		public static void Delete(long allergyDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),allergyDefNum);
				return;
			}
			string command= "DELETE FROM allergydef WHERE AllergyDefNum = "+POut.Long(allergyDefNum);
			Db.NonQ(command);
		}
		*/



	}
}