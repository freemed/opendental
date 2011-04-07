using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class FormularyMeds{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all FormularyMeds.</summary>
		private static List<FormularyMed> listt;

		///<summary>A list of all FormularyMeds.</summary>
		public static List<FormularyMed> Listt{
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
			string command="SELECT * FROM formularymed ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="FormularyMed";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.FormularyMedCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<FormularyMed> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<FormularyMed>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM formularymed WHERE PatNum = "+POut.Long(patNum);
			return Crud.FormularyMedCrud.SelectMany(command);
		}

		///<summary>Gets one FormularyMed from the db.</summary>
		public static FormularyMed GetOne(long formularyMedNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<FormularyMed>(MethodBase.GetCurrentMethod(),formularyMedNum);
			}
			return Crud.FormularyMedCrud.SelectOne(formularyMedNum);
		}

		///<summary></summary>
		public static long Insert(FormularyMed formularyMed){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				formularyMed.FormularyMedNum=Meth.GetLong(MethodBase.GetCurrentMethod(),formularyMed);
				return formularyMed.FormularyMedNum;
			}
			return Crud.FormularyMedCrud.Insert(formularyMed);
		}

		///<summary></summary>
		public static void Update(FormularyMed formularyMed){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),formularyMed);
				return;
			}
			Crud.FormularyMedCrud.Update(formularyMed);
		}

		///<summary></summary>
		public static void Delete(long formularyMedNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),formularyMedNum);
				return;
			}
			string command= "DELETE FROM formularymed WHERE FormularyMedNum = "+POut.Long(formularyMedNum);
			Db.NonQ(command);
		}
		*/



	}
}