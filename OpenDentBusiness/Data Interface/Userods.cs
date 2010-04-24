using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Web;
using CodeBase;
using System.Windows.Forms;

namespace OpenDentBusiness {
	///<summary>(Users OD)</summary>
	public class Userods {
		//private static bool webServerConfigHasLoadedd=false;
		
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM userod ORDER BY UserName";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Userod";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			UserodC.Listt=Crud.UserodCrud.TableToList(table);
		}			

		///<summary></summary>
		public static Userod GetUser(long userNum) {
			//No need to check RemotingRole; no call to db.
			if(UserodC.Listt==null){
				RefreshCache();
			}
			for(int i=0;i<UserodC.Listt.Count;i++) {
				if(UserodC.Listt[i].UserNum==userNum){
					return UserodC.Listt[i];
				}
			}
			return null;
		}

		///<summary>Returns null if not found.</summary>
		public static Userod GetUserByName(string userName) {
			//No need to check RemotingRole; no call to db.
			if(UserodC.Listt==null) {
				RefreshCache();
			}
			for(int i=0;i<UserodC.Listt.Count;i++) {
				if(UserodC.Listt[i].UserName==userName) {
					return UserodC.Listt[i];
				}
			}
			return null;
		}

		public static string GetName(long userNum){
			//No need to check RemotingRole; no call to db.
			if(userNum==0){
				return "";
			}
			Userod user=GetUser(userNum);
			if(user==null){
				return "";
			}
			return user.UserName;
		}


		///<summary>Only used in one place on the server when first attempting to log on.  Must pass in a hash of the actual password since we don't want to be moving the real password around.  It will be checked against the one in the database.  Passhash should be empty string if user does not have a password.  Returns a user object if user and password are valid.  Otherwise, returns null.</summary>
		public static Userod CheckUserAndPassword(string username, string passhash){
			//No need to check RemotingRole; no call to db.
			RefreshCache();
			Userod user=GetUserByName(username);
			if(user==null){
				return null;
			}
			if(user.Password==passhash) {
				return user;
			}
			return null;
		}

		///<summary>Used by Server.  Throws exception if bad username or passhash or if either are blank.  It uses cached user list, refreshing it if null.  This is used everywhere except in the log on screen.</summary>
		public static void CheckCredentials(Credentials cred){
			//No need to check RemotingRole; no call to db.
			if(cred.Username=="" || cred.PassHash==""){
				throw new ApplicationException("Invalid username or password.");
			}
			Userod userod=null;
			for(int i=0;i<UserodC.Listt.Count;i++){
				if(UserodC.Listt[i].UserName==cred.Username){
					userod=UserodC.Listt[i];
					break;
				}
			}
			if(userod==null){
				throw new ApplicationException("Invalid username or password.");
			}
			if(userod.Password!=cred.PassHash){
				throw new ApplicationException("Invalid username or password.");
			}
		}

		///<summary>Will throw an exception if it fails for any reason.  This will directly access the config file on the disk, read the values, and set the DataConnection to the new database.  This is only triggered when someone tries to log on.</summary>
		public static void LoadDatabaseInfoFromFile(string configFilePath){
			//No need to check RemotingRole; no call to db.
			if(!File.Exists(configFilePath)){
				throw new Exception("Could not find "+configFilePath+" on the web server.");
			}
			XmlDocument doc=new XmlDocument();
			try {
				doc.Load(configFilePath);
			}
			catch{
				throw new Exception("Web server "+configFilePath+" could not be opened or is in an invalid format.");
			}
			XPathNavigator Navigator=doc.CreateNavigator();
			//always picks the first database entry in the file:
			XPathNavigator navConn=Navigator.SelectSingleNode("//DatabaseConnection");//[Database='"+database+"']");
			if(navConn==null) {
				throw new Exception(configFilePath+" does not contain a valid database entry.");//database+" is not an allowed database.");
			}
			//return navOne.SelectSingleNode("summary").Value;
			//now, get the values for this connection
			string server=navConn.SelectSingleNode("ComputerName").Value;
			string database=navConn.SelectSingleNode("Database").Value;
			string mysqlUser=navConn.SelectSingleNode("User").Value;
			string mysqlPassword=navConn.SelectSingleNode("Password").Value;
			string mysqlUserLow=navConn.SelectSingleNode("UserLow").Value;
			string mysqlPasswordLow=navConn.SelectSingleNode("PasswordLow").Value;
			XPathNavigator dbTypeNav=navConn.SelectSingleNode("DatabaseType");
			DatabaseType dbtype=DatabaseType.MySql;
			if(dbTypeNav!=null){
				if(dbTypeNav.Value=="Oracle"){
					dbtype=DatabaseType.Oracle;
				}
			}
			DataConnection dcon=new DataConnection();
			try {
				dcon.SetDb(server,database,mysqlUser,mysqlPassword,mysqlUserLow,mysqlPasswordLow,dbtype);
			}
			catch(Exception e){
				throw new Exception(e.Message+"\r\n"+"Connection to database failed.  Check the values in the config file on the web server "+configFilePath);
			}
			//todo?: make sure no users have blank passwords.
		}

		/*
		///<summary>Used by the SL logon window to validate credentials.  Send in the password unhashed.  If invalid, it will always throw an exception of some type.  If it is valid, then it will return the hashed password.  This is the only place where the config file is read, and it's only read on startup.  So the web service needs to be restarted if the config file changes.</summary>
		public static string CheckDbUserPassword(string configFilePath,string username,string password){
			//for some reason, this static variable was remaining true even if the webservice was restarted.
			//So we're not going to use it anymore.  Always load from file.
			//if(!webServerConfigHasLoadedd){
				LoadDatabaseInfoFromFile(configFilePath);
			//	webServerConfigHasLoadedd=true;
			//}
			DataConnection dcon=new DataConnection();
			//Then, check username and password
			string passhash="";
			string command="SELECT Password FROM userod WHERE UserName='"+POut.PString(username)+"'";
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count!=0){
				passhash=table.Rows[0][0].ToString();
			}
			if(passhash=="" || passhash!=EncryptPassword(password)){
				throw new Exception("Invalid username or password.");
			}
			return passhash;
		}*/

		///<summary></summary>
		public static string EncryptPassword(string inputPass) {
			//No need to check RemotingRole; no call to db.
			return EncryptPassword(inputPass,false);
		}

		///<summary></summary>
		public static string EncryptPassword(string inputPass,bool skipECW) {
			//No need to check RemotingRole; no call to db.
			if(inputPass=="") {
				return "";
			}
			HashAlgorithm algorithm=HashAlgorithm.Create("MD5");
			if(!skipECW && Programs.IsEnabled("eClinicalWorks")) {
				byte[] asciiBytes=Encoding.ASCII.GetBytes(inputPass);
				byte[] hashbytes=algorithm.ComputeHash(asciiBytes);//length=16
				byte digit1;
				byte digit2;
				string char1;
				string char2;
				StringBuilder strbuild=new StringBuilder();
				for(int i=0;i<hashbytes.Length;i++) {
					if(hashbytes[i]==0) {
						digit1=0;
						digit2=0;
					}
					else {
						digit1=(byte)Math.Floor((double)hashbytes[i]/16d);
						//double remainder=Math.IEEERemainder((double)hashbytes[i],16d);
						digit2=(byte)(hashbytes[i]-(byte)(16*digit1));
					}
					char1=ByteToStr(digit1);
					char2=ByteToStr(digit2);
					strbuild.Append(char1);
					strbuild.Append(char2);
				}
				return strbuild.ToString();
			}
			else {//typical
				byte[] unicodeBytes=Encoding.Unicode.GetBytes(inputPass);
				byte[] hashbytes2=algorithm.ComputeHash(unicodeBytes);
				return Convert.ToBase64String(hashbytes2);
			}
		}


		///<summary>The only valid input is a value between 0 and 15.  Text returned will be 1-9 or a-f.</summary>
		private static string ByteToStr(Byte byteVal) {
			//No need to check RemotingRole; no call to db.
			switch(byteVal) {
				case 10:
					return "a";
				case 11:
					return "b";
				case 12:
					return "c";
				case 13:
					return "d";
				case 14:
					return "e";
				case 15:
					return "f";
				default:
					return byteVal.ToString();
			}
		}

		///<summary></summary>
		public static bool CheckPassword(string inputPass,string hashedPass) {
			//No need to check RemotingRole; no call to db.
			if(hashedPass=="") {
				return inputPass=="";
			}
			string hashedInput=EncryptPassword(inputPass);
			//MessageBox.Show(
			//Debug.WriteLine(hashedInput+","+hashedPass);
			return hashedInput==hashedPass;
		}		

		///<summary>usertype can be 'all', 'prov', 'emp', or 'other'.</summary>
		public static DataTable RefreshSecurity(string usertype,long schoolClassNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),usertype,schoolClassNum);
			}
			string command;
			if(usertype=="prov" && schoolClassNum>0){
				command="SELECT userod.* FROM userod,provider "
					+"WHERE userod.ProvNum=provider.ProvNum "
					+"AND SchoolClassNum="+POut.Long(schoolClassNum)
					+" ORDER BY UserName";
				return Db.GetTable(command);
			}
			command="SELECT * FROM userod ";
			if(usertype=="emp"){
				command+="WHERE EmployeeNum!=0 ";
			}
			else if(usertype=="prov") {//and all schoolclassnums
				command+="WHERE ProvNum!=0 ";
			}
			else if(usertype=="all") {
				//command+="";
			}
			else if(usertype=="other") {
				command+="WHERE ProvNum=0 AND EmployeeNum=0 ";
			}
			command+="ORDER BY UserName";
			return Db.GetTable(command);
		}

		///<summary></summary>
		private static void Update(Userod user){
			//No need to check RemotingRole because it is checked in InsertOrUpdate before calling this.
			Crud.UserodCrud.Update(user);
		}

		///<summary></summary>
		private static long Insert(Userod user){
			//No need to check RemotingRole because it is checked in InsertOrUpdate before calling this.
			return Crud.UserodCrud.Insert(user);
		}

		///<summary>Surround with try/catch because it can throw exceptions.</summary>
		public static void InsertOrUpdate(bool isNew,Userod user){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),isNew,user);
				return;
			}
			//should add a check that employeenum and provnum are not both set.
			//make sure username is not already taken
			string command;
			long excludeUserNum;
			if(isNew){
				excludeUserNum=0;
			}
			else{
				excludeUserNum=user.UserNum;//it's ok if the name matches the current username
			}
			if(!IsUserNameUnique(user.UserName,excludeUserNum)){
				throw new Exception(Lans.g("Userods","UserName already in use."));
			}
			//make sure that there would still be at least one user with security admin permissions
			if(!isNew){
				command="SELECT COUNT(*) FROM grouppermission "
					+"WHERE PermType='"+POut.Long((int)Permissions.SecurityAdmin)+"' "
					+"AND UserGroupNum="+POut.Long(user.UserGroupNum);
				if(Db.GetCount(command)=="0"){//if this user would not have admin
					//make sure someone else has admin
					command="SELECT COUNT(*) FROM userod,grouppermission "
						+"WHERE grouppermission.PermType='"+POut.Long((int)Permissions.SecurityAdmin)+"'"
						+" AND userod.UserGroupNum=grouppermission.UserGroupNum"
						+" AND userod.IsHidden =0"
						+" AND userod.UserNum != "+POut.Long(user.UserNum);
					if(Db.GetCount(command)=="0"){//there are no other users with this permission
						throw new Exception(Lans.g("Users","At least one user must have Security Admin permission."));
					}
				}
			}
			//an admin user can never be hidden
			command="SELECT COUNT(*) FROM grouppermission "
				+"WHERE PermType='"+POut.Long((int)Permissions.SecurityAdmin)+"' "
				+"AND UserGroupNum="+POut.Long(user.UserGroupNum);
			if(Db.GetCount(command)!="0"//if this user is admin
				&& user.IsHidden)//and hidden
			{
				throw new Exception(Lans.g("Userods","Admins cannot be hidden."));
			}
			if(isNew){
				Insert(user);
			}
			else{
				Update(user);
			}
		}

		///<summary>Supply 0 or -1 for the excludeUserNum to not exclude any.</summary>
		public static bool IsUserNameUnique(string username,long excludeUserNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),username,excludeUserNum);
			}
			if(username==""){
				return false;
			}
			string command="SELECT COUNT(*) FROM userod WHERE UserName='"+POut.String(username)+"' "
				+"AND UserNum !="+POut.Long(excludeUserNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()=="0") {
				return true;
			}
			return false;
		}

		///<summary>Used in FormSecurity.FillTreeUsers</summary>
		public static List<Userod> GetForGroup(long userGroupNum) {
			//No need to check RemotingRole; no call to db.
			//ArrayList al=new ArrayList();
			List<Userod> retVal=new List<Userod>();
			for(int i=0;i<UserodC.Listt.Count;i++){
				if(UserodC.Listt[i].UserGroupNum==userGroupNum){
					retVal.Add(UserodC.Listt[i]);
				}
			}
			//User[] retVal=new User[al.Count];
			//al.CopyTo(retVal);
			return retVal;
		}

		///<summary>This always returns one admin user.  There must be one and there is rarely more than one.  Only used on startup to determine if security is being used.</summary>
		public static Userod GetAdminUser() {
			//No need to check RemotingRole; no call to db.
			//just find any permission for security admin.  There has to be one.
			for(int i=0;i<GroupPermissionC.List.Length;i++) {
				if(GroupPermissionC.List[i].PermType!=Permissions.SecurityAdmin) {
					continue;
				}
				for(int j=0;j<UserodC.Listt.Count;j++) {
					if(UserodC.Listt[j].UserGroupNum==GroupPermissionC.List[i].UserGroupNum) {
						return UserodC.Listt[j];
					}
				}
			}
			return null;//will never happen
		}

		/// <summary>Will return 0 if no inbox found for user.</summary>
		public static long GetInbox(long userNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<UserodC.Listt.Count;i++) {
				if(UserodC.Listt[i].UserNum==userNum){
					return UserodC.Listt[i].TaskListInBox;
				}
			}
			return 0;
		}

		///<summary></summary>
		public static List<Userod> GetNotHidden(){
			//No need to check RemotingRole; no call to db.
			List<Userod> retVal=new List<Userod>();
			for(int i=0;i<UserodC.Listt.Count;i++){
				if(!UserodC.Listt[i].IsHidden){
					retVal.Add(UserodC.Listt[i].Copy());
				}
			}
			//retVal.Sort(//in a hurry, so skipping
			return retVal;
		}

		//Return 3, which is non-admin provider type
		public static long GetAnesthProvType(long anesthProvType) {
			//No need to check RemotingRole; no call to db.
			for(int i = 0;i < UserodC.Listt.Count;i++) {
				if(UserodC.Listt[i].AnesthProvType == anesthProvType) {
					return UserodC.Listt[i].AnesthProvType;
				}
			}
			return 3;
		}


	}
}
