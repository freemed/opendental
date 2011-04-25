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
			string command="SELECT * FROM drugunit ORDER BY UnitIdentifier";
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

		///<summary>Gets one DrugUnit from the db.</summary>
		public static DrugUnit GetOne(long drugUnitNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<DrugUnit>(MethodBase.GetCurrentMethod(),drugUnitNum);
			}
			return Crud.DrugUnitCrud.SelectOne(drugUnitNum);
		}

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

		///<summary>Surround with a try/catch.  Will fail if drug unit is in use.</summary>
		public static void Delete(long drugUnitNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),drugUnitNum);
				return;
			}
			//validation
			string command;
			command="SELECT COUNT(*) FROM labresult WHERE DrugUnitNum="+POut.Long(drugUnitNum);
			if(Db.GetCount(command)!="0") {
				throw new ApplicationException(Lans.g("FormDrugUnitEdit","Not allowed to delete because of attached labresults."));
			}
			command="SELECT COUNT(*) FROM vaccinepat WHERE DrugUnitNum="+POut.Long(drugUnitNum);
			if(Db.GetCount(command)!="0") {
				throw new ApplicationException(Lans.g("FormDrugUnitEdit","Not allowed to delete because of attached vaccinepats."));
			}
			//delete
			command= "DELETE FROM drugunit WHERE DrugUnitNum = "+POut.Long(drugUnitNum);
			Db.NonQ(command);
		}

		///<summary>For example, mL</summary>
		public static string GetIdentifier(long drugUnitNum) {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			for(int i=0;i<Listt.Count;i++) {//using public Listt in case it's null.
				if(Listt[i].DrugUnitNum==drugUnitNum) {
					return Listt[i].UnitIdentifier;
				}
			}
			return "";//should never happen
		}
	}
}