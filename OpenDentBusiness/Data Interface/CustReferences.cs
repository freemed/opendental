using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CustReferences{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all CustReferences.</summary>
		private static List<CustReference> listt;

		///<summary>A list of all CustReferences.</summary>
		public static List<CustReference> Listt{
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
			string command="SELECT * FROM custreference ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="CustReference";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.CustReferenceCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<CustReference> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<CustReference>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM custreference WHERE PatNum = "+POut.Long(patNum);
			return Crud.CustReferenceCrud.SelectMany(command);
		}

		///<summary>Gets one CustReference from the db.</summary>
		public static CustReference GetOne(long custReferenceNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<CustReference>(MethodBase.GetCurrentMethod(),custReferenceNum);
			}
			return Crud.CustReferenceCrud.SelectOne(custReferenceNum);
		}

		///<summary></summary>
		public static long Insert(CustReference custReference){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				custReference.CustReferenceNum=Meth.GetLong(MethodBase.GetCurrentMethod(),custReference);
				return custReference.CustReferenceNum;
			}
			return Crud.CustReferenceCrud.Insert(custReference);
		}

		///<summary></summary>
		public static void Update(CustReference custReference){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),custReference);
				return;
			}
			Crud.CustReferenceCrud.Update(custReference);
		}

		///<summary></summary>
		public static void Delete(long custReferenceNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),custReferenceNum);
				return;
			}
			string command= "DELETE FROM custreference WHERE CustReferenceNum = "+POut.Long(custReferenceNum);
			Db.NonQ(command);
		}
		*/



	}
}