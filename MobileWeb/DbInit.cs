using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;
using WebForms;

namespace MobileWeb {
	public static class DbInit {
		/// <summary>
		/// being a static constructor this is only called when Init() is called the first time. Further calls to Init() does not call this constructor.
		/// </summary>
		static DbInit() {
			string connectStr=Properties.Settings.Default.DBMobileWeb;
			OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
			dc.SetDb(connectStr,"",DatabaseType.MySql,true);
		}
		
		/// <summary>
		/// Empty method with the sole purpose of making sure that that the constructor of this class is called
		/// </summary>
		public static void Init(){
		}
	}
}