using System;
using System.Collections.Generic;
using System.Data;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using CodeBase;

namespace OpenDentBusiness {
	///<summary>(Users OD)</summary>
	public class Userods {
		//<summary>This gets filled automatically when Refresh is called.  If using remoting, then the calling program is responsible for filling this RawData on the client since the automated part only happens on the server.  So there are TWO RawDatas in a server situation, but only one in a small office that connects directly to the database.</summary>
		//public static DataTable RawData;
		///<summary></summary>
		public static DataTable RefreshCache() {
			string command="SELECT * FROM userod ORDER BY UserName";
			DataTable table=General.GetTable(command);
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
				UserodC.Listt.Add(user);
			}
		}			

		///<summary></summary>
		public static Userod GetUser(int userNum) {
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
				+",EmployeeNum = '"  +POut.PInt(user.EmployeeNum)+"'"
				+",ClinicNum = '"    +POut.PInt(user.ClinicNum)+"'"
				+",ProvNum = '"      +POut.PInt(user.ProvNum)+"'"
				+",IsHidden = '"     +POut.PBool  (user.IsHidden)+"'"
				+" WHERE UserNum = '"+POut.PInt   (user.UserNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(Userod user){
			string command= "INSERT INTO userod (UserName,Password,UserGroupNum,EmployeeNum,ClinicNum,ProvNum,IsHidden) VALUES("
				+"'"+POut.PString(user.UserName)+"', "
				+"'"+POut.PString(user.Password)+"', "
				+"'"+POut.PInt   (user.UserGroupNum)+"', "
				+"'"+POut.PInt(user.EmployeeNum)+"', "
				+"'"+POut.PInt(user.ClinicNum)+"', "
				+"'"+POut.PInt(user.ProvNum)+"', "
				+"'"+POut.PBool  (user.IsHidden)+"')";
 			user.UserNum=General.NonQ(command,true);
		}

		///<summary></summary>
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

	


	}
}
