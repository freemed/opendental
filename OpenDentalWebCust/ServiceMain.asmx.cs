using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace OpenDentalWebCust {
	/// <summary>Summary description for Service1</summary>
	[WebService(Namespace = "http://www.opendental.com/OpenDentalWebCust/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ServiceMain:System.Web.Services.WebService {

		[WebMethod]
		public string HelloWorld() {
			return "Hello World";
		}
	}
}