using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;
using System.Threading;
using WebForms;

namespace MobileWeb {
	public partial class ProcessLogin:System.Web.UI.Page {
		private Util util=new Util();

		protected void Page_Load(object sender,EventArgs e) {
			try {
				String username="";
				String password="";
				bool RememberMe=false;
				Message.Text="";
					if(Request.Form["username"]!=null) {
						username=Request.Form["username"].ToString().Trim();
					}
					if(Request.Form["password"]!=null) {
						password=Request.Form["password"].ToString().Trim();
					}
					if(Request.Form["rememberusername"]!=null) {
						if(Request.Form["rememberusername"].ToString().Trim()=="true") {
							RememberMe=true;
						}
					}
					//if(username=="" && password=="") {
						util.SetMobileDbConnection();
						Session["CustomerNum"]=1486;
						Message.Text="CorrectLogin";
					//}
					//else {
					//	Message.Text="LoginFailed";
					//}
					HttpCookie UserNameCookie=new HttpCookie("UserNameCookie");
					if(RememberMe) {
						UserNameCookie.Value=username;
						UserNameCookie.Expires=DateTime.Now.AddYears(1);
						Response.Cookies.Add(UserNameCookie);
					}
					else {
						UserNameCookie.Expires=DateTime.Now.AddDays(-1);
						Response.Cookies.Add(UserNameCookie);
					}
			
			}
			catch(Exception ex) {
				Logger.LogError(ex);
			}
		}
		
		//Dennis: don't see the point in adding a 'salt' for now
		private string MD5Encrypt(string data) {
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
			return Encoding.UTF8.GetString(result);
		}




	}
}