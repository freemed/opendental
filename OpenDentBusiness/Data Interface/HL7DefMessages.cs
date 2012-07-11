using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class HL7DefMessages{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all HL7DefMessages.</summary>
		private static List<HL7DefMessage> listt;

		///<summary>A list of all HL7DefMessages.</summary>
		public static List<HL7DefMessage> Listt{
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
			string command="SELECT * FROM hl7defmessage ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="HL7DefMessage";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.HL7DefMessageCrud.TableToList(table);
		}
		#endregion

		public static List<HL7DefMessage> GetForDef(long hl7DefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7DefMessage>>(MethodBase.GetCurrentMethod(),hl7DefNum);
			}
			string command="SELECT * FROM hl7defmessage WHERE HL7DefNum='"+POut.Long(hl7DefNum)+"'";
			return Crud.HL7DefMessageCrud.SelectMany(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<HL7DefMessage> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7DefMessage>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM hl7defmessage WHERE PatNum = "+POut.Long(patNum);
			return Crud.HL7DefMessageCrud.SelectMany(command);
		}

		///<summary>Gets one HL7DefMessage from the db.</summary>
		public static HL7DefMessage GetOne(long hL7DefMessageNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<HL7DefMessage>(MethodBase.GetCurrentMethod(),hL7DefMessageNum);
			}
			return Crud.HL7DefMessageCrud.SelectOne(hL7DefMessageNum);
		}

		///<summary></summary>
		public static long Insert(HL7DefMessage hL7DefMessage){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				hL7DefMessage.HL7DefMessageNum=Meth.GetLong(MethodBase.GetCurrentMethod(),hL7DefMessage);
				return hL7DefMessage.HL7DefMessageNum;
			}
			return Crud.HL7DefMessageCrud.Insert(hL7DefMessage);
		}

		///<summary></summary>
		public static void Update(HL7DefMessage hL7DefMessage){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),hL7DefMessage);
				return;
			}
			Crud.HL7DefMessageCrud.Update(hL7DefMessage);
		}

		///<summary></summary>
		public static void Delete(long hL7DefMessageNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),hL7DefMessageNum);
				return;
			}
			string command= "DELETE FROM hl7defmessage WHERE HL7DefMessageNum = "+POut.Long(hL7DefMessageNum);
			Db.NonQ(command);
		}
		*/



	}
}