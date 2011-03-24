using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class MedicalOrders{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all MedicalOrders.</summary>
		private static List<MedicalOrder> listt;

		///<summary>A list of all MedicalOrders.</summary>
		public static List<MedicalOrder> Listt{
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
			string command="SELECT * FROM medicalorder ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="MedicalOrder";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.MedicalOrderCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<MedicalOrder> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<MedicalOrder>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM medicalorder WHERE PatNum = "+POut.Long(patNum);
			return Crud.MedicalOrderCrud.SelectMany(command);
		}

		///<summary>Gets one MedicalOrder from the db.</summary>
		public static MedicalOrder GetOne(long medicalOrderNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<MedicalOrder>(MethodBase.GetCurrentMethod(),medicalOrderNum);
			}
			return Crud.MedicalOrderCrud.SelectOne(medicalOrderNum);
		}

		///<summary></summary>
		public static long Insert(MedicalOrder medicalOrder){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				medicalOrder.MedicalOrderNum=Meth.GetLong(MethodBase.GetCurrentMethod(),medicalOrder);
				return medicalOrder.MedicalOrderNum;
			}
			return Crud.MedicalOrderCrud.Insert(medicalOrder);
		}

		///<summary></summary>
		public static void Update(MedicalOrder medicalOrder){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),medicalOrder);
				return;
			}
			Crud.MedicalOrderCrud.Update(medicalOrder);
		}

		///<summary></summary>
		public static void Delete(long medicalOrderNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),medicalOrderNum);
				return;
			}
			string command= "DELETE FROM medicalorder WHERE MedicalOrderNum = "+POut.Long(medicalOrderNum);
			Db.NonQ(command);
		}
		*/



	}
}