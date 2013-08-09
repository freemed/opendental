using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SecurityLogHashs {

		///<summary></summary>
		public static long Insert(SecurityLogHash securityLogHash) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				securityLogHash.SecurityLogHashNum=Meth.GetLong(MethodBase.GetCurrentMethod(),securityLogHash);
				return securityLogHash.SecurityLogHashNum;
			}
			return Crud.SecurityLogHashCrud.Insert(securityLogHash);
		}

		///<summary>Creates a new SecurityLogHash entry in the Db.</summary>
		public static void CreateSecurityLogHash(long securityLogNum) {
			SecurityLog securityLog=SecurityLogs.GetOne(securityLogNum);
			SecurityLogHash securityLogHash=new SecurityLogHash();
			//Set the FK
			securityLogHash.SecurityLogNum=securityLog.SecurityLogNum;
			//Hash the securityLog
			securityLogHash.LogHash=EncryptSecurityLog(securityLog);
			Insert(securityLogHash);
		}

		///<summary>Returns a SHA-256 hash of the entire security log.  Only called from CreateSecurityLogHash() and FormAudit.FillGrid()</summary>
		public static string EncryptSecurityLog(SecurityLog securityLog) {
			HashAlgorithm algorithm=HashAlgorithm.Create("SHA-256");
			//Build string to hash
			string logString="";
			logString+=securityLog.SecurityLogNum;
			logString+=securityLog.PermType;
			logString+=securityLog.UserNum;
			//Now add YearMonthDayHourMinuteSecondMillisecond
			logString+=securityLog.LogDateTime.Year.ToString()
				+securityLog.LogDateTime.Month.ToString()
				+securityLog.LogDateTime.Day.ToString()
				+securityLog.LogDateTime.Hour.ToString()
				+securityLog.LogDateTime.Minute.ToString()
				+securityLog.LogDateTime.Second.ToString()
				+securityLog.LogDateTime.Millisecond.ToString();
			logString+=securityLog.LogText;
			logString+=securityLog.CompName;
			logString+=securityLog.PatNum;
			logString+=securityLog.FKey.ToString();
			byte[] unicodeBytes=Encoding.Unicode.GetBytes(logString);
			byte[] hashbytes=algorithm.ComputeHash(unicodeBytes);
			return Convert.ToBase64String(hashbytes);
		}

		///<summary>Returns HashLog entry for SecurityLogNum.  If no entry exists, returns empty string.</summary>
		public static string GetLogHashForSecurityLog(long securityLogNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),securityLogNum);
			}
			SecurityLogHash securityLogHash=GetForSecurityLog(securityLogNum);
			if(securityLogHash==null) {
				return "";
			}
			return securityLogHash.LogHash;
		}

		///<summary>Gets a SecurityLogHash entery from the db.</summary>
		public static SecurityLogHash GetForSecurityLog(long securityLogNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<SecurityLogHash>(MethodBase.GetCurrentMethod(),securityLogNum);
			}
			string command = "SELECT * FROM securityloghash WHERE SecurityLogNum = "+POut.Long(securityLogNum);
			return Crud.SecurityLogHashCrud.SelectOne(command);
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