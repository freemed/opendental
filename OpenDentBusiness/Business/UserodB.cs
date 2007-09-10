using System;
using System.Collections.Generic;
using System.Data;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using CodeBase;

namespace OpenDentBusiness {
	public class UserodB {
		///<summary>This gets filled automatically when Refresh is called.  If using remoting, then the calling program is responsible for filling this RawData on the client since the automated part only happens on the server.  So there are TWO RawDatas in a server situation, but only one in a small office that connects directly to the database.</summary>
		public static DataTable RawData;

		///<summary></summary>
		public static DataSet Refresh() {
			string command="SELECT * FROM userod ORDER BY UserName";
			DataConnection dcon=new DataConnection();
			RawData=dcon.GetTable(command);
			DataSet retVal=new DataSet();
			retVal.Tables.Add(RawData);
			return retVal;
		}

		///<summary></summary>
		public static Userod GetUser(int userNum) {
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
		}		

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

	


	}
}
