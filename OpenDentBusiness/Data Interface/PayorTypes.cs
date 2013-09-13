using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PayorTypes{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all PayorTypes.</summary>
		private static List<PayorType> listt;

		///<summary>A list of all PayorTypes.</summary>
		public static List<PayorType> Listt{
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
			string command="SELECT * FROM payortype ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="PayorType";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.PayorTypeCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<PayorType> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PayorType>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM payortype WHERE PatNum = "+POut.Long(patNum);
			return Crud.PayorTypeCrud.SelectMany(command);
		}

		///<summary>Gets one PayorType from the db.</summary>
		public static PayorType GetOne(long payorTypeNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<PayorType>(MethodBase.GetCurrentMethod(),payorTypeNum);
			}
			return Crud.PayorTypeCrud.SelectOne(payorTypeNum);
		}

		///<summary></summary>
		public static long Insert(PayorType payorType){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				payorType.PayorTypeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),payorType);
				return payorType.PayorTypeNum;
			}
			return Crud.PayorTypeCrud.Insert(payorType);
		}

		///<summary></summary>
		public static void Update(PayorType payorType){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),payorType);
				return;
			}
			Crud.PayorTypeCrud.Update(payorType);
		}

		///<summary></summary>
		public static void Delete(long payorTypeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),payorTypeNum);
				return;
			}
			string command= "DELETE FROM payortype WHERE PayorTypeNum = "+POut.Long(payorTypeNum);
			Db.NonQ(command);
		}
		*/



	}
}