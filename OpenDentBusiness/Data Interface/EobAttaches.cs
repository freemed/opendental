using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EobAttaches{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EobAttaches.</summary>
		private static List<EobAttach> listt;

		///<summary>A list of all EobAttaches.</summary>
		public static List<EobAttach> Listt{
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
			string command="SELECT * FROM eobattach ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EobAttach";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EobAttachCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EobAttach> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EobAttach>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM eobattach WHERE PatNum = "+POut.Long(patNum);
			return Crud.EobAttachCrud.SelectMany(command);
		}

		///<summary>Gets one EobAttach from the db.</summary>
		public static EobAttach GetOne(long eobAttachNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EobAttach>(MethodBase.GetCurrentMethod(),eobAttachNum);
			}
			return Crud.EobAttachCrud.SelectOne(eobAttachNum);
		}

		///<summary></summary>
		public static long Insert(EobAttach eobAttach){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				eobAttach.EobAttachNum=Meth.GetLong(MethodBase.GetCurrentMethod(),eobAttach);
				return eobAttach.EobAttachNum;
			}
			return Crud.EobAttachCrud.Insert(eobAttach);
		}

		///<summary></summary>
		public static void Update(EobAttach eobAttach){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),eobAttach);
				return;
			}
			Crud.EobAttachCrud.Update(eobAttach);
		}

		///<summary></summary>
		public static void Delete(long eobAttachNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),eobAttachNum);
				return;
			}
			string command= "DELETE FROM eobattach WHERE EobAttachNum = "+POut.Long(eobAttachNum);
			Db.NonQ(command);
		}
		*/



	}
}