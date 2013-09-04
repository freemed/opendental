using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Cvxs{
		//If this table type will exist as cached data, uncomment the CachePattern region below.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all Cvxs.</summary>
		private static List<Cvx> listt;

		///<summary>A list of all Cvxs.</summary>
		public static List<Cvx> Listt{
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
			string command="SELECT * FROM cvx ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Cvx";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.CvxCrud.TableToList(table);
		}
		#endregion
		*/

		///<summary></summary>
		public static long Insert(Cvx cvx){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				cvx.CvxNum=Meth.GetLong(MethodBase.GetCurrentMethod(),cvx);
				return cvx.CvxNum;
			}
			return Crud.CvxCrud.Insert(cvx);
		}

		///<summary>Returns a list of just the codes for use in update or insert logic.</summary>
		public static List<string> GetAllCodes() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod());
			}
			List<string> retVal=new List<string>();
			string command="SELECT CvxCode FROM cvx";
			DataTable table=DataCore.GetTable(command);
			foreach(DataRow row in table.Rows) {
				retVal.Add(row.ItemArray[0].ToString());
			}
			return retVal;
		}


		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Cvx> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Cvx>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM cvx WHERE PatNum = "+POut.Long(patNum);
			return Crud.CvxCrud.SelectMany(command);
		}

		///<summary>Gets one Cvx from the db.</summary>
		public static Cvx GetOne(long cvxNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Cvx>(MethodBase.GetCurrentMethod(),cvxNum);
			}
			return Crud.CvxCrud.SelectOne(cvxNum);
		}

		///<summary></summary>
		public static void Update(Cvx cvx){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cvx);
				return;
			}
			Crud.CvxCrud.Update(cvx);
		}

		///<summary></summary>
		public static void Delete(long cvxNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cvxNum);
				return;
			}
			string command= "DELETE FROM cvx WHERE CvxNum = "+POut.Long(cvxNum);
			Db.NonQ(command);
		}
		*/



	}
}