using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Formularies{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all Formularies.</summary>
		private static List<Formulary> listt;

		///<summary>A list of all Formularies.</summary>
		public static List<Formulary> Listt{
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
			string command="SELECT * FROM formulary ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Formulary";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.FormularyCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Formulary> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Formulary>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM formulary WHERE PatNum = "+POut.Long(patNum);
			return Crud.FormularyCrud.SelectMany(command);
		}

		///<summary>Gets one Formulary from the db.</summary>
		public static Formulary GetOne(long formularyNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Formulary>(MethodBase.GetCurrentMethod(),formularyNum);
			}
			return Crud.FormularyCrud.SelectOne(formularyNum);
		}

		///<summary></summary>
		public static long Insert(Formulary formulary){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				formulary.FormularyNum=Meth.GetLong(MethodBase.GetCurrentMethod(),formulary);
				return formulary.FormularyNum;
			}
			return Crud.FormularyCrud.Insert(formulary);
		}

		///<summary></summary>
		public static void Update(Formulary formulary){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),formulary);
				return;
			}
			Crud.FormularyCrud.Update(formulary);
		}

		///<summary></summary>
		public static void Delete(long formularyNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),formularyNum);
				return;
			}
			string command= "DELETE FROM formulary WHERE FormularyNum = "+POut.Long(formularyNum);
			Db.NonQ(command);
		}
		*/



	}
}