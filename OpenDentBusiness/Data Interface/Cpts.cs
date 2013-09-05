using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Cpts{
		//If this table type will exist as cached data, uncomment the CachePattern region below.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all Cpts.</summary>
		private static List<Cpt> listt;

		///<summary>A list of all Cpts.</summary>
		public static List<Cpt> Listt{
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
			string command="SELECT * FROM cpt ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Cpt";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.CptCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Cpt> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Cpt>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM cpt WHERE PatNum = "+POut.Long(patNum);
			return Crud.CptCrud.SelectMany(command);
		}

		///<summary>Gets one Cpt from the db.</summary>
		public static Cpt GetOne(long cptNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Cpt>(MethodBase.GetCurrentMethod(),cptNum);
			}
			return Crud.CptCrud.SelectOne(cptNum);
		}

		///<summary></summary>
		public static long Insert(Cpt cpt){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				cpt.CptNum=Meth.GetLong(MethodBase.GetCurrentMethod(),cpt);
				return cpt.CptNum;
			}
			return Crud.CptCrud.Insert(cpt);
		}

		///<summary></summary>
		public static void Update(Cpt cpt){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cpt);
				return;
			}
			Crud.CptCrud.Update(cpt);
		}

		///<summary></summary>
		public static void Delete(long cptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cptNum);
				return;
			}
			string command= "DELETE FROM cpt WHERE CptNum = "+POut.Long(cptNum);
			Db.NonQ(command);
		}
		*/



	}
}