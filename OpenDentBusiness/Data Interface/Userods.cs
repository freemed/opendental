using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
			string command="SELECT * FROM userod ORDER BY UserName";
			DataTable table=General.GetTable(command);
			table.TableName="Userod";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			UserodC.Listt=new List<Userod>();//[UserB.RawData.Rows.Count];
			Userod user;
			for(int i=0;i<table.Rows.Count;i++) {
				user=new Userod();
				user.UserNum       = PIn.PInt   (table.Rows[i][0].ToString());
				user.UserName      = PIn.PString(table.Rows[i][1].ToString());
				user.Password      = PIn.PString(table.Rows[i][2].ToString());
				user.UserGroupNum  = PIn.PInt   (table.Rows[i][3].ToString());
				user.EmployeeNum   = PIn.PInt   (table.Rows[i][4].ToString());
				user.ClinicNum     = PIn.PInt   (table.Rows[i][5].ToString());
				user.ProvNum       = PIn.PInt   (table.Rows[i][6].ToString());
				user.IsHidden      = PIn.PBool  (table.Rows[i][7].ToString());
				user.TaskListInBox = PIn.PInt   (table.Rows[i][8].ToString());
				UserodC.Listt.Add(user);
			}
		}			

		///<summary></summary>
		public static Userod GetUser(int userNum) {
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

		
		/*
			Userod user=null;
			for(int i=0;i<RawData.Rows.Count;i++) {
				if(RawData.Rows[i]["UserNum"].ToString()!=userNum.ToString()){
					continue;
				}
				user=new Userod();
				user.UserNum       = PIn.PInt   (RawData.Rows[i][0].ToString());
				user.UserName      = PIn.PString(RawData.Rows[i][1].ToString());
				user.Password      = PIn.PString(RawData.Rows[i][2].ToString());
				user.UserGroupNum  = PIn.PInt   (RawData.Rows[i][3].ToString());
				user.EmployeeNum   = PIn.PInt(RawData.Rows[i][4].ToString());
				user.ClinicNum     = PIn.PInt(RawData.Rows[i][5].ToString());
				user.ProvNum       = PIn.PInt(RawData.Rows[i][6].ToString());
				user.IsHidden      = PIn.PBool  (RawData.Rows[i][7].ToString());
			}
			return user;
		}		*/

		public static string GetName(int userNum){
			if(userNum==0){
				return "";
			}
			Userod user=GetUser(userNum);
			if(user==null){
				return "";
			}
			return user.UserName;
		}


		///<summary>Must pass in a hash of the actual password since we don't want to be moving the real password around.  It will be checked against the one in the database.  Passhash should be empty string if user does not have a password.  Returns true if user and password valid.</summary>
		public static bool CheckUserAndPassword(string username, string passhash){
			string command="SELECT Password FROM userod WHERE UserName='"+POut.PString(username)+"'";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){//user does not exist
				return false;
			}
			string actualHash=table.Rows[0][0].ToString();
			if(actualHash==passhash){
				return true;
			}
			return false;
		}

		///<summary>Used by SilverLight.  Throws exception if bad username or passhash or if either are blank.  It uses cached user list, refreshing it if null.  This is use everywhere except in the log on screen.</summary>
		public static void CheckCredentials(Credentials cred){
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

		///<summary>Will throw an exception if it fails for any reason.  This will directly access the config file on the disk, read the values, and set the DataConnection to the new database.  This is only triggered on startup of SL the first time someone tries to log on.</summary>
		private static void LoadDatabaseInfoFromFile(string configFilePath){
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
				OpenDental.DataAccess.DataSettings.CreateConnectionString(server,database, mysqlUser,mysqlPassword);
			}
			catch(Exception e){
				throw new Exception(e.Message+"\r\n"+"Connection to database failed.  Check the values in the config file on the web server "+configFilePath);
			}
			//todo?: make sure no users have blank passwords.
		}

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
		}

		///<summary></summary>
		public static string EncryptPassword(string inputPass) {
			if(inputPass=="") {
				return "";
			}
			HashAlgorithm algorithm=HashAlgorithm.Create("MD5");
			//I should have used UTF-8 here, but we can't change it now.
			byte[] unicodeBytes=Encoding.Unicode.GetBytes(inputPass);
			byte[] hashbytes=algorithm.ComputeHash(unicodeBytes);
			return Convert.ToBase64String(hashbytes);
		}

		///<summary></summary>
		public static bool CheckPassword(string inputPass,string hashedPass) {
			string hashedInput=EncryptPassword(inputPass);
			//MessageBox.Show(
			//Debug.WriteLine(hashedInput+","+hashedPass);
			return hashedInput==hashedPass;
		}		

		///<summary>usertype can be 'all', 'prov', 'emp', or 'other'.</summary>
		public static DataTable RefreshSecurity(string usertype,int schoolClassNum){
			string command;
			if(usertype=="prov" && schoolClassNum>0){
				command="SELECT userod.* FROM userod,provider "
					+"WHERE userod.ProvNum=provider.ProvNum "
					+"AND SchoolClassNum="+POut.PInt(schoolClassNum)
					+" ORDER BY UserName";
				return General.GetTable(command);
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
			return General.GetTable(command);
		}

		///<summary></summary>
		private static void Update(Userod user){
			string command= "UPDATE userod SET " 
				+"UserName = '"      +POut.PString(user.UserName)+"'"
				+",Password = '"     +POut.PString(user.Password)+"'"
				+",UserGroupNum = '" +POut.PInt   (user.UserGroupNum)+"'"
				+",EmployeeNum = '"  +POut.PInt   (user.EmployeeNum)+"'"
				+",ClinicNum = '"    +POut.PInt   (user.ClinicNum)+"'"
				+",ProvNum = '"      +POut.PInt   (user.ProvNum)+"'"
				+",IsHidden = '"     +POut.PBool  (user.IsHidden)+"'"
				+",TaskListInBox = '"+POut.PInt   (user.TaskListInBox)+"'"
				+" WHERE UserNum = '"+POut.PInt   (user.UserNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(Userod user){
			string command= "INSERT INTO userod (UserName,Password,UserGroupNum,EmployeeNum,ClinicNum,ProvNum,IsHidden,"
				+"TaskListInBox) VALUES("
				+"'"+POut.PString(user.UserName)+"', "
				+"'"+POut.PString(user.Password)+"', "
				+"'"+POut.PInt   (user.UserGroupNum)+"', "
				+"'"+POut.PInt   (user.EmployeeNum)+"', "
				+"'"+POut.PInt   (user.ClinicNum)+"', "
				+"'"+POut.PInt   (user.ProvNum)+"', "
				+"'"+POut.PBool  (user.IsHidden)+"', "
				+"'"+POut.PInt   (user.TaskListInBox)+"')";
 			user.UserNum=General.NonQ(command,true);
		}

		///<summary>Surround with try/catch because it can throw exceptions.</summary>
		public static void InsertOrUpdate(bool isNew,Userod user){
			//should add a check that employeenum and provnum are not both set.
			//make sure username is not already taken
			string command;
			int excludeUserNum;
			if(isNew){
				excludeUserNum=0;
			}
			else{
				excludeUserNum=user.UserNum;//it's ok if the name matches the current username
			}
			if(!IsUserNameUnique(user.UserName,excludeUserNum)){
				throw new Exception(Lan.g("Userods","UserName already in use."));
			}
			//make sure that there would still be at least one user with security admin permissions
			if(!isNew){
				command="SELECT COUNT(*) FROM grouppermission "
					+"WHERE PermType='"+POut.PInt((int)Permissions.SecurityAdmin)+"' "
					+"AND UserGroupNum="+POut.PInt(user.UserGroupNum);
				if(General.GetCount(command)=="0"){//if this user would not have admin
					//make sure someone else has admin
					command="SELECT COUNT(*) FROM userod,grouppermission "
						+"WHERE grouppermission.PermType='"+POut.PInt((int)Permissions.SecurityAdmin)+"'"
						+" AND userod.UserGroupNum=grouppermission.UserGroupNum"
						+" AND userod.IsHidden =0"
						+" AND userod.UserNum != "+POut.PInt(user.UserNum);
					if(General.GetCount(command)=="0"){//there are no other users with this permission
						throw new Exception(Lan.g("Users","At least one user must have Security Admin permission."));
					}
				}
			}
			if(isNew){
				Insert(user);
			}
			else{
				Update(user);
			}
		}

		///<summary>Supply 0 or -1 for the excludeUserNum to not exclude any.</summary>
		public static bool IsUserNameUnique(string username,int excludeUserNum){
			if(username==""){
				return false;
			}
			string command="SELECT COUNT(*) FROM userod WHERE UserName='"+POut.PString(username)+"' "
				+"AND UserNum !="+POut.PInt(excludeUserNum);
			DataTable table=General.GetTable(command);
			if(table.Rows[0][0].ToString()=="0") {
				return true;
			}
			return false;
		}

		///<summary>Used in FormSecurity.FillTreeUsers</summary>
		public static List<Userod> GetForGroup(int userGroupNum){
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
		public static int GetInbox(int userNum){
			for(int i=0;i<UserodC.Listt.Count;i++) {
				if(UserodC.Listt[i].UserNum==userNum){
					return UserodC.Listt[i].TaskListInBox;
				}
			}
			return 0;
		}

		///<summary></summary>
		public static List<Userod> GetNotHidden(){
			List<Userod> retVal=new List<Userod>();
			for(int i=0;i<UserodC.Listt.Count;i++){
				if(!UserodC.Listt[i].IsHidden){
					retVal.Add(UserodC.Listt[i]);
				}
			}
			//retVal.Sort(//in a hurry, so skipping
			return retVal;
		}


	}
}
