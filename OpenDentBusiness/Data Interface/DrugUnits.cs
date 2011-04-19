using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class DrugUnits{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types. (Done.)

		///<summary>A list of all DrugUnits.</summary>
		private static List<DrugUnit> listt;

		///<summary>A list of all DrugUnits.</summary>
		public static List<DrugUnit> Listt{
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
			string command="SELECT * FROM drugunit ORDER BY UnitText";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="DrugUnit";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.DrugUnitCrud.TableToList(table);
		}
		#endregion

		///<summary></summary>
		public static long Insert(DrugUnit drugUnit){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				drugUnit.DrugUnitNum=Meth.GetLong(MethodBase.GetCurrentMethod(),drugUnit);
				return drugUnit.DrugUnitNum;
			}
			return Crud.DrugUnitCrud.Insert(drugUnit);
		}

		///<summary></summary>
		public static void Update(DrugUnit drugUnit){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),drugUnit);
				return;
			}
			Crud.DrugUnitCrud.Update(drugUnit);
		}

		///<summary></summary>
		public static void Delete(long drugUnitNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),drugUnitNum);
				return;
			}
			string command= "DELETE FROM drugunit WHERE DrugUnitNum = "+POut.Long(drugUnitNum);
			Db.NonQ(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<DrugUnit> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<DrugUnit>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM drugunit WHERE PatNum = "+POut.Long(patNum);
			return Crud.DrugUnitCrud.SelectMany(command);
		}

		///<summary>Gets one DrugUnit from the db.</summary>
		public static DrugUnit GetOne(long drugUnitNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<DrugUnit>(MethodBase.GetCurrentMethod(),drugUnitNum);
			}
			return Crud.DrugUnitCrud.SelectOne(drugUnitNum);
		}
		*/



	}
}