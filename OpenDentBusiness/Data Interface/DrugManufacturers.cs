using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class DrugManufacturers{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types. (Done.)

		///<summary>A list of all DrugManufacturers.</summary>
		private static List<DrugManufacturer> listt;

		///<summary>A list of all DrugManufacturers.</summary>
		public static List<DrugManufacturer> Listt{
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
			string command="SELECT * FROM drugmanufacturer ORDER BY ManufacturerName";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="DrugManufacturer";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.DrugManufacturerCrud.TableToList(table);
		}
		#endregion

		///<summary></summary>
		public static long Insert(DrugManufacturer drugManufacturer){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				drugManufacturer.DrugManufacturerNum=Meth.GetLong(MethodBase.GetCurrentMethod(),drugManufacturer);
				return drugManufacturer.DrugManufacturerNum;
			}
			return Crud.DrugManufacturerCrud.Insert(drugManufacturer);
		}

		///<summary></summary>
		public static void Update(DrugManufacturer drugManufacturer){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),drugManufacturer);
				return;
			}
			Crud.DrugManufacturerCrud.Update(drugManufacturer);
		}

		///<summary></summary>
		public static void Delete(long drugManufacturerNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),drugManufacturerNum);
				return;
			}
			string command= "DELETE FROM drugmanufacturer WHERE DrugManufacturerNum = "+POut.Long(drugManufacturerNum);
			Db.NonQ(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<DrugManufacturer> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<DrugManufacturer>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM drugmanufacturer WHERE PatNum = "+POut.Long(patNum);
			return Crud.DrugManufacturerCrud.SelectMany(command);
		}

		///<summary>Gets one DrugManufacturer from the db.</summary>
		public static DrugManufacturer GetOne(long drugManufacturerNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<DrugManufacturer>(MethodBase.GetCurrentMethod(),drugManufacturerNum);
			}
			return Crud.DrugManufacturerCrud.SelectOne(drugManufacturerNum);
		}
		*/



	}
}