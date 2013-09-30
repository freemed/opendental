using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SecurityLogHashes{

		///<summary>Inserts securityloghash into Db.</summary>
		public static long Insert(SecurityLogHash securityLogHash) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				securityLogHash.SecurityLogHashNum=Meth.GetLong(MethodBase.GetCurrentMethod(),securityLogHash);
				return securityLogHash.SecurityLogHashNum;
			}
			return Crud.SecurityLogHashCrud.Insert(securityLogHash);
		}

		///<summary>Creates a new SecurityLogHash entry in the Db.</summary>
		public static void InsertSecurityLogHash(long securityLogNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),securityLogNum);
				return;
			}
			SecurityLog securityLog=SecurityLogs.GetOne(securityLogNum); //need a fresh copy because of time stamps, etc.
			SecurityLogHash securityLogHash=new SecurityLogHash();
			//Set the FK
			securityLogHash.SecurityLogNum=securityLog.SecurityLogNum;
			//Hash the securityLog
			securityLogHash.LogHash=GetHashString(securityLog);
			Insert(securityLogHash);
		}

		///<summary>Does not make a call to the db.  Returns a SHA-256 hash of the entire security log.  Length of 32 bytes.  Only called from CreateSecurityLogHash() and FormAudit.FillGrid()</summary>
		public static string GetHashString(SecurityLog securityLog) {
			//No need to check RemotingRole; no call to db.
			HashAlgorithm algorithm=SHA256.Create();
			//Build string to hash
			string logString="";
			//logString+=securityLog.SecurityLogNum;
			logString+=((int)securityLog.PermType).ToString();
			logString+=securityLog.UserNum;
			logString+=POut.DateT(securityLog.LogDateTime,false);
			logString+=securityLog.LogText;
			//logString+=securityLog.CompName;
			logString+=securityLog.PatNum;
			//logString+=securityLog.FKey.ToString();
			byte[] unicodeBytes=Encoding.Unicode.GetBytes(logString);
			byte[] hashbytes=algorithm.ComputeHash(unicodeBytes);
			return Convert.ToBase64String(hashbytes);
		}
		
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