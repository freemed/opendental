using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ICD9s{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all ICD9s.</summary>
		private static List<ICD9> listt;

		///<summary>A list of all ICD9s.</summary>
		public static List<ICD9> Listt{
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
			string command="SELECT * FROM icd9 ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ICD9";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.ICD9Crud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<ICD9> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ICD9>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM icd9 WHERE PatNum = "+POut.Long(patNum);
			return Crud.ICD9Crud.SelectMany(command);
		}

		///<summary>Gets one ICD9 from the db.</summary>
		public static ICD9 GetOne(long iCD9Num){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ICD9>(MethodBase.GetCurrentMethod(),iCD9Num);
			}
			return Crud.ICD9Crud.SelectOne(iCD9Num);
		}

		///<summary></summary>
		public static long Insert(ICD9 iCD9){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				iCD9.ICD9Num=Meth.GetLong(MethodBase.GetCurrentMethod(),iCD9);
				return iCD9.ICD9Num;
			}
			return Crud.ICD9Crud.Insert(iCD9);
		}

		///<summary></summary>
		public static void Update(ICD9 iCD9){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),iCD9);
				return;
			}
			Crud.ICD9Crud.Update(iCD9);
		}

		///<summary></summary>
		public static void Delete(long iCD9Num) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),iCD9Num);
				return;
			}
			string command= "DELETE FROM icd9 WHERE ICD9Num = "+POut.Long(iCD9Num);
			Db.NonQ(command);
		}
		*/



	}
}