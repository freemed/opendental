using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SecurityLogHashs{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all SecurityLogHashs.</summary>
		private static List<SecurityLogHash> listt;

		///<summary>A list of all SecurityLogHashs.</summary>
		public static List<SecurityLogHash> Listt{
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
			string command="SELECT * FROM securityloghash ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="SecurityLogHash";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.SecurityLogHashCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<SecurityLogHash> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<SecurityLogHash>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM securityloghash WHERE PatNum = "+POut.Long(patNum);
			return Crud.SecurityLogHashCrud.SelectMany(command);
		}

		///<summary>Gets one SecurityLogHash from the db.</summary>
		public static SecurityLogHash GetOne(long securityLogHashNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<SecurityLogHash>(MethodBase.GetCurrentMethod(),securityLogHashNum);
			}
			return Crud.SecurityLogHashCrud.SelectOne(securityLogHashNum);
		}

		///<summary></summary>
		public static long Insert(SecurityLogHash securityLogHash){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				securityLogHash.SecurityLogHashNum=Meth.GetLong(MethodBase.GetCurrentMethod(),securityLogHash);
				return securityLogHash.SecurityLogHashNum;
			}
			return Crud.SecurityLogHashCrud.Insert(securityLogHash);
		}

		///<summary></summary>
		public static void Update(SecurityLogHash securityLogHash){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),securityLogHash);
				return;
			}
			Crud.SecurityLogHashCrud.Update(securityLogHash);
		}

		///<summary></summary>
		public static void Delete(long securityLogHashNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),securityLogHashNum);
				return;
			}
			string command= "DELETE FROM securityloghash WHERE SecurityLogHashNum = "+POut.Long(securityLogHashNum);
			Db.NonQ(command);
		}
		*/



	}
}