using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Pharmacies{
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM pharmacy ORDER BY StoreName";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="Pharmacy";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			PharmacyC.Listt=Crud.PharmacyCrud.TableToList(table);
		}

		///<Summary>Gets one Pharmacy from the database.</Summary>
		public static Pharmacy GetOne(long pharmacyNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Pharmacy>(MethodBase.GetCurrentMethod(),pharmacyNum);
			}
			return Crud.PharmacyCrud.SelectOne(pharmacyNum);
		}

		///<summary></summary>
		public static long Insert(Pharmacy pharmacy){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				pharmacy.PharmacyNum=Meth.GetLong(MethodBase.GetCurrentMethod(),pharmacy);
				return pharmacy.PharmacyNum;
			}
			return Crud.PharmacyCrud.Insert(pharmacy);
		}

		///<summary></summary>
		public static void Update(Pharmacy pharmacy){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pharmacy);
				return;
			}
			Crud.PharmacyCrud.Update(pharmacy);
		}

		///<summary></summary>
		public static void DeleteObject(long pharmacyNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pharmacyNum);
				return;
			}
			Crud.PharmacyCrud.Delete(pharmacyNum);
		}

		public static string GetDescription(long PharmacyNum) {
			//No need to check RemotingRole; no call to db.
			if(PharmacyNum==0){
				return "";
			}
			for(int i=0;i<PharmacyC.Listt.Count;i++){
				if(PharmacyC.Listt[i].PharmacyNum==PharmacyNum){
					return PharmacyC.Listt[i].StoreName;
				}
			}
			return "";
		}

	}
}