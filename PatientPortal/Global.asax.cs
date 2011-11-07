using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PatientPortalMVC {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication:System.Web.HttpApplication {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			/*
			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home",action = "Index",id = UrlParameter.Optional } // Parameter defaults
			);
			*/ 
			routes.MapRoute(
			"Default", // Route name
			"{controller}/{action}/{id}", // URL with parameters
			new { controller = "Account",action = "Login",id = UrlParameter.Optional } // Parameter defaults
			);


		}
		/// <summary>
		/// Dennis Mathew: Added this on 2011-11-07 for backward compatibily with older urls that may have been used by patients to login. This may be removed in the future.
		/// Older format: https://opendentalsoft.com/PatientPortal/Login.aspx?DentalOfficeID=6566 new format: https://opendentalsoft.com/PatientPortal/?DentalOfficeID=6566
		/// </summary>
		protected void Application_BeginRequest(Object sender,EventArgs e) {
			if(Request.Url.AbsolutePath.Contains("Login.aspx")) {
				long DentalOfficeID=0;
				HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
                RouteData routeData = RouteTable.Routes.GetRouteData(currentContext);
				if(Request.QueryString["DentalOfficeID"]!=null)
					Int64.TryParse(Request.QueryString["DentalOfficeID"].ToString(),out DentalOfficeID);
					Response.Redirect("/?DentalOfficeID="+DentalOfficeID);
				}
		}

		protected void Application_Start() {
			new Util().SetDbConnection();
			AreaRegistration.RegisterAllAreas();
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}


	}



}