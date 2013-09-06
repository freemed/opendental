using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrAmendments{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrAmendments.</summary>
		private static List<EhrAmendment> listt;

		///<summary>A list of all EhrAmendments.</summary>
		public static List<EhrAmendment> Listt{
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
			string command="SELECT * FROM ehramendment ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrAmendment";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrAmendmentCrud.TableToList(table);
		}
		#endregion
		*/

		///<summary></summary>
		public static List<EhrAmendment> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrAmendment>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehramendment WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrAmendmentCrud.SelectMany(command);
		}
/*
		///<summary>Gets one EhrAmendment from the db.</summary>
		public static EhrAmendment GetOne(long ehrAmendmentNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrAmendment>(MethodBase.GetCurrentMethod(),ehrAmendmentNum);
			}
			return Crud.EhrAmendmentCrud.SelectOne(ehrAmendmentNum);
		}
		*/
		///<summary></summary>
		public static long Insert(EhrAmendment ehrAmendment){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrAmendment.EhrAmendmentNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrAmendment);
				return ehrAmendment.EhrAmendmentNum;
			}
			return Crud.EhrAmendmentCrud.Insert(ehrAmendment);
		}

		///<summary></summary>
		public static void Update(EhrAmendment ehrAmendment){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrAmendment);
				return;
			}
			Crud.EhrAmendmentCrud.Update(ehrAmendment);
		}
		
		///<summary></summary>
		public static void Delete(long ehrAmendmentNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrAmendmentNum);
				return;
			}
			string command= "DELETE FROM ehramendment WHERE EhrAmendmentNum = "+POut.Long(ehrAmendmentNum);
			Db.NonQ(command);
		}
		



	}
}