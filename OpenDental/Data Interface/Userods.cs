using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>(Users OD)</summary>
	public class Userods {
		///<summary>A list of all users.</summary>
		public static List<Userod> Listt;

		///<summary></summary>
		public static void Refresh() {
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					UserodB.Refresh();
				}
				else {
					DtoUserodRefresh dto=new DtoUserodRefresh();
					DataSet ds=RemotingClient.ProcessQuery(dto);
					UserodB.RawData=ds.Tables[0];
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return;
			}
			Listt=new List<Userod>();//[UserB.RawData.Rows.Count];
			Userod user;
			for(int i=0;i<UserodB.RawData.Rows.Count;i++) {
				user=new Userod();
				user.UserNum       = PIn.PInt   (UserodB.RawData.Rows[i][0].ToString());
				user.UserName      = PIn.PString(UserodB.RawData.Rows[i][1].ToString());
				user.Password      = PIn.PString(UserodB.RawData.Rows[i][2].ToString());
				user.UserGroupNum  = PIn.PInt   (UserodB.RawData.Rows[i][3].ToString());
				user.EmployeeNum   = PIn.PInt(UserodB.RawData.Rows[i][4].ToString());
				user.ClinicNum     = PIn.PInt(UserodB.RawData.Rows[i][5].ToString());
				Listt.Add(user);
			}
		}


		///<summary></summary>
		private static void Update(Userod user){
			string command= "UPDATE userod SET " 
				+"UserName = '"      +POut.PString(user.UserName)+"'"
				+",Password = '"     +POut.PString(user.Password)+"'"
				+",UserGroupNum = '" +POut.PInt   (user.UserGroupNum)+"'"
				+",EmployeeNum = '"  +POut.PInt(user.EmployeeNum)+"'"
				+",ClinicNum = '"    +POut.PInt(user.ClinicNum)+"'"
				+" WHERE UserNum = '"+POut.PInt   (user.UserNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(Userod user){
			string command= "INSERT INTO userod (UserName,Password,UserGroupNum,EmployeeNum,ClinicNum) VALUES("
				+"'"+POut.PString(user.UserName)+"', "
				+"'"+POut.PString(user.Password)+"', "
				+"'"+POut.PInt   (user.UserGroupNum)+"', "
				+"'"+POut.PInt(user.EmployeeNum)+"', "
				+"'"+POut.PInt(user.ClinicNum)+"')";
 			user.UserNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void InsertOrUpdate(bool isNew,Userod user){
			//make sure username is not already taken
			string command;
			if(isNew){
				command="SELECT COUNT(*) FROM userod WHERE UserName='"+POut.PString(user.UserName)+"'";
			}
			else{
				command="SELECT COUNT(*) FROM userod WHERE UserName='"+POut.PString(user.UserName)+"' "
					+"AND UserNum !="+POut.PInt(user.UserNum);//it's ok if the name matches the current username
			}
			DataTable table=General.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
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

		/*  Too dangerous.
		///<summary></summary>
		public static void Delete(User user){
			//check to make sure this is not the last user with security admin permissions
			string command="SELECT COUNT(*) FROM user,grouppermission "
				+"WHERE grouppermission.PermType='"+POut.PInt((int)Permissions.SecurityAdmin)+"'"
				+" AND user.UserGroupNum=grouppermission.UserGroupNum"
				+" AND user.UserNum != "+POut.PInt(UserNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows[0][0].ToString()=="0"){//there are no other users with this permission
				throw new Exception(Lan.g(this,"At least one user must have Security Admin permission."));
			}
			//check to make sure this user has never been referenced in security log
			command="SELECT COUNT(*) FROM securitylog WHERE UserNum="+POut.PInt(UserNum);
			table=dcon.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
				throw new Exception(Lan.g(this,"User cannot be deleted because they have already been recorded in the security log."));
			}
			command="DELETE from user WHERE UserNum = "+POut.PInt(UserNum);
 			General.NonQ(command);
		}*/

		/*
		///<summary></summary>
		public static User GetUser(int userNum) {
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].UserNum==userNum) {
					return Listt[i].Copy();
				}
			}
			return null;
		}*/

		/*
		///<summary>Displays user/password dialog.  Returns a valid authenticated user or null.  Pass in the English string to display.  It will get converted to other languages just before display.</summary>
		public static User Authenticate(string display){
			FormPassword FormP=new FormPassword(display);
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return null;//bad password
			}
			return FormP.AuthenticatedUser;
		}*/

		///<summary>Used in FormSecurity.FillTreeUsers</summary>
		public static List<Userod> GetForGroup(int userGroupNum){
			//ArrayList al=new ArrayList();
			List<Userod> retVal=new List<Userod>();
			for(int i=0;i<Listt.Count;i++){
				if(Listt[i].UserGroupNum==userGroupNum){
					retVal.Add(Listt[i]);
				}
			}
			//User[] retVal=new User[al.Count];
			//al.CopyTo(retVal);
			return retVal;
		}

		///<summary>This always returns one admin user.  There must be one and there is rarely more than one.  Only used on startup to determine if security is being used.</summary>
		public static Userod GetAdminUser() {
			//just find any permission for security admin.  There has to be one.
			for(int i=0;i<GroupPermissions.List.Length;i++) {
				if(GroupPermissions.List[i].PermType!=Permissions.SecurityAdmin) {
					continue;
				}
				for(int j=0;j<Userods.Listt.Count;j++) {
					if(Userods.Listt[j].UserGroupNum==GroupPermissions.List[i].UserGroupNum) {
						return Userods.Listt[j];
					}
				}
			}
			return null;//will never happen
		}

		/*
		///<summary></summary>
		public static string EncryptPassword(string inputPass) {
			if(inputPass==""){
				return "";
			}
			HashAlgorithm hash=HashAlgorithm.Create("MD5");
			byte[] unicodeBytes=Encoding.Unicode.GetBytes(inputPass);
			byte[] hashbytes=hash.ComputeHash(unicodeBytes);
			return Convert.ToBase64String(hashbytes);
		}

		///<summary></summary>
		public static bool CheckPassword(string inputPass,string hashedPass) {
			string hashedInput=EncryptPassword(inputPass);
			//MessageBox.Show(
			//Debug.WriteLine(hashedInput+","+hashedPass);
			return hashedInput==hashedPass;
		}*/

		

	}
 
	

	
}













