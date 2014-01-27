using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CDSPermissions {
		//TODO: implement caching;

		public static CDSPermission GetForUser(long usernum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<CDSPermission>(MethodBase.GetCurrentMethod(),usernum);
			}
			string command="SELECT * FROM cdspermission WHERE UserNum="+POut.Long(usernum);
			CDSPermission retval=Crud.CDSPermissionCrud.SelectOne(command);
			if(retval!=null) {
				return retval;
			}
			return new CDSPermission();//return new CDS permission that has no permissions granted.
		}

		///<summary></summary>
		public static List<CDSPermission> GetAll() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<CDSPermission>>(MethodBase.GetCurrentMethod());
			}
			InsertMissingValues();
			string command="SELECT * FROM cdspermission";
			return Crud.CDSPermissionCrud.SelectMany(command);
		}

		///<summary>Inserts one row per UserOD if they do not have one already.</summary>
		private static void InsertMissingValues() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="SELECT * FROM userod WHERE IsHidden=0 AND UserNum NOT IN (SELECT UserNum from cdsPermission)";
			List<Userod> uods=Crud.UserodCrud.SelectMany(command);
			CDSPermission cdsp;
			for(int i=0;i<uods.Count;i++){
				cdsp=new CDSPermission();
				cdsp.UserNum=uods[i].UserNum;
				CDSPermissions.Insert(cdsp);
			}
			return;
		}

		///<summary></summary>
		public static long Insert(CDSPermission cDSPermission) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				cDSPermission.CDSPermissionNum=Meth.GetLong(MethodBase.GetCurrentMethod(),cDSPermission);
				return cDSPermission.CDSPermissionNum;
			}
			return Crud.CDSPermissionCrud.Insert(cDSPermission);
		}

		///<summary></summary>
		public static void Update(CDSPermission cDSPermission) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cDSPermission);
				return;
			}
			Crud.CDSPermissionCrud.Update(cDSPermission);
		}

		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all CDSPermissions.</summary>
		private static List<CDSPermission> listt;

		///<summary>A list of all CDSPermissions.</summary>
		public static List<CDSPermission> Listt{
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
			string command="SELECT * FROM cdspermission ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="CDSPermission";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.CDSPermissionCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<CDSPermission> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<CDSPermission>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM cdspermission WHERE PatNum = "+POut.Long(patNum);
			return Crud.CDSPermissionCrud.SelectMany(command);
		}

		///<summary>Gets one CDSPermission from the db.</summary>
		public static CDSPermission GetOne(long cDSPermissionNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<CDSPermission>(MethodBase.GetCurrentMethod(),cDSPermissionNum);
			}
			return Crud.CDSPermissionCrud.SelectOne(cDSPermissionNum);
		}

		///<summary></summary>
		public static void Delete(long cDSPermissionNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cDSPermissionNum);
				return;
			}
			string command= "DELETE FROM cdspermission WHERE CDSPermissionNum = "+POut.Long(cDSPermissionNum);
			Db.NonQ(command);
		}
		*/



	}
}