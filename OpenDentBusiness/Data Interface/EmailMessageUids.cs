using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EmailMessageUids{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EmailMessageUids.</summary>
		private static List<EmailMessageUid> listt;

		///<summary>A list of all EmailMessageUids.</summary>
		public static List<EmailMessageUid> Listt{
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
			string command="SELECT * FROM emailmessageuid ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EmailMessageUid";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EmailMessageUidCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EmailMessageUid> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EmailMessageUid>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM emailmessageuid WHERE PatNum = "+POut.Long(patNum);
			return Crud.EmailMessageUidCrud.SelectMany(command);
		}

		///<summary>Gets one EmailMessageUid from the db.</summary>
		public static EmailMessageUid GetOne(long emailMessageUidNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EmailMessageUid>(MethodBase.GetCurrentMethod(),emailMessageUidNum);
			}
			return Crud.EmailMessageUidCrud.SelectOne(emailMessageUidNum);
		}

		///<summary></summary>
		public static long Insert(EmailMessageUid emailMessageUid){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				emailMessageUid.EmailMessageUidNum=Meth.GetLong(MethodBase.GetCurrentMethod(),emailMessageUid);
				return emailMessageUid.EmailMessageUidNum;
			}
			return Crud.EmailMessageUidCrud.Insert(emailMessageUid);
		}

		///<summary></summary>
		public static void Update(EmailMessageUid emailMessageUid){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),emailMessageUid);
				return;
			}
			Crud.EmailMessageUidCrud.Update(emailMessageUid);
		}

		///<summary></summary>
		public static void Delete(long emailMessageUidNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),emailMessageUidNum);
				return;
			}
			string command= "DELETE FROM emailmessageuid WHERE EmailMessageUidNum = "+POut.Long(emailMessageUidNum);
			Db.NonQ(command);
		}
		*/



	}
}