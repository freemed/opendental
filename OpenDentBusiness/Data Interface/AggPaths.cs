using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AggPaths{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all AggPaths.</summary>
		private static List<AggPath> listt;

		///<summary>A list of all AggPaths.</summary>
		public static List<AggPath> Listt{
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
			string command="SELECT * FROM aggpath ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AggPath";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.AggPathCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<AggPath> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<AggPath>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM aggpath WHERE PatNum = "+POut.Long(patNum);
			return Crud.AggPathCrud.SelectMany(command);
		}

		///<summary>Gets one AggPath from the db.</summary>
		public static AggPath GetOne(long aggPathNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<AggPath>(MethodBase.GetCurrentMethod(),aggPathNum);
			}
			return Crud.AggPathCrud.SelectOne(aggPathNum);
		}

		///<summary></summary>
		public static long Insert(AggPath aggPath){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				aggPath.AggPathNum=Meth.GetLong(MethodBase.GetCurrentMethod(),aggPath);
				return aggPath.AggPathNum;
			}
			return Crud.AggPathCrud.Insert(aggPath);
		}

		///<summary></summary>
		public static void Update(AggPath aggPath){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aggPath);
				return;
			}
			Crud.AggPathCrud.Update(aggPath);
		}

		///<summary></summary>
		public static void Delete(long aggPathNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aggPathNum);
				return;
			}
			string command= "DELETE FROM aggpath WHERE AggPathNum = "+POut.Long(aggPathNum);
			Db.NonQ(command);
		}
		*/



	}
}