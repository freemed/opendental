using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Sops{
		#region CachePattern
		///<summary>A list of all Sops.</summary>
		private static List<Sop> listt;

		///<summary>A list of all Sops.</summary>
		public static List<Sop> Listt{
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
			string command="SELECT * FROM sop";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Sop";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.SopCrud.TableToList(table);
		}
		#endregion

		///<summary></summary>
		public static long Insert(Sop sop){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				sop.SopNum=Meth.GetLong(MethodBase.GetCurrentMethod(),sop);
				return sop.SopNum;
			}
			return Crud.SopCrud.Insert(sop);
		}

		///<summary>Returns one SopNum. If SopCode not found returns 0.</summary>
		public static long GetOneNum(string SopCode) {
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].SopCode==SopCode) {
					return i;
				}
			}
			return 0;
		}

		///<summary>Returns a list of just the codes for use in update or insert logic.</summary>
		public static List<string> GetAllCodes() {
			List<string> retVal=new List<string>();
			for(int i=0;i<Listt.Count;i++) {
				retVal.Add(Listt[i].SopCode);
			}
			return retVal;
		}

		///<summary>Returns an Sop description based on the given payor type info.</summary>
		public static string GetRecentPayorTypeDescription(string sopCode) {
			string desc="";
			for(int i=0;i<Listt.Count;i++) {
				if(sopCode==Listt[i].SopCode) {
					desc=Listt[i].Description;
				}
			}
			return desc;
		}

		///<summary>Returns one code for use in update or insert logic.</summary>
		public static string GetOneCode(long SopNum) {
			int i = (int)SopNum;
			return Listt[i].SopCode;
		}

		///<summary>Returns a list of just the descriptions for use in update or insert logic.</summary>
		public static List<string> GetAllDescriptions() {
			List<string> retVal=new List<string>();
			for(int i=0;i<Listt.Count;i++) {
				retVal[i]=Listt[i].Description;
			}
			return retVal;
		}

		///<summary>Returns the description for the specified SopCode.  Returns an empty string if no code is found.</summary>
		public static string GetOneDescription(string SopCode) {
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].SopCode==SopCode) {
					return Listt[i].Description;
				}
			}
			return "";
		}

		///<summary></summary>
		public static void TruncateAll() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="TRUNCATE TABLE sop";//Oracle compatible
			DataCore.NonQ(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Sop> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Sop>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM sop WHERE PatNum = "+POut.Long(patNum);
			return Crud.SopCrud.SelectMany(command);
		}

		///<summary>Gets one Sop from the db.</summary>
		public static Sop GetOne(long sopNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Sop>(MethodBase.GetCurrentMethod(),sopNum);
			}
			return Crud.SopCrud.SelectOne(sopNum);
		}

		///<summary></summary>
		public static void Update(Sop sop){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sop);
				return;
			}
			Crud.SopCrud.Update(sop);
		}

		///<summary></summary>
		public static void Delete(long sopNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sopNum);
				return;
			}
			string command= "DELETE FROM sop WHERE SopNum = "+POut.Long(sopNum);
			Db.NonQ(command);
		}
		*/



	}
}