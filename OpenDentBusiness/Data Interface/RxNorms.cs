using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class RxNorms{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all RxNorms.</summary>
		private static List<RxNorm> listt;

		///<summary>A list of all RxNorms.</summary>
		public static List<RxNorm> Listt{
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
			string command="SELECT * FROM rxnorm ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="RxNorm";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.RxNormCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<RxNorm> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<RxNorm>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM rxnorm WHERE PatNum = "+POut.Long(patNum);
			return Crud.RxNormCrud.SelectMany(command);
		}

		///<summary>Gets one RxNorm from the db.</summary>
		public static RxNorm GetOne(long rxNormNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<RxNorm>(MethodBase.GetCurrentMethod(),rxNormNum);
			}
			return Crud.RxNormCrud.SelectOne(rxNormNum);
		}

		///<summary></summary>
		public static long Insert(RxNorm rxNorm){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				rxNorm.RxNormNum=Meth.GetLong(MethodBase.GetCurrentMethod(),rxNorm);
				return rxNorm.RxNormNum;
			}
			return Crud.RxNormCrud.Insert(rxNorm);
		}

		///<summary></summary>
		public static void Update(RxNorm rxNorm){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),rxNorm);
				return;
			}
			Crud.RxNormCrud.Update(rxNorm);
		}

		///<summary></summary>
		public static void Delete(long rxNormNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),rxNormNum);
				return;
			}
			string command= "DELETE FROM rxnorm WHERE RxNormNum = "+POut.Long(rxNormNum);
			Db.NonQ(command);
		}
		*/



	}
}