using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using OpenDentBusiness;

namespace OpenDental.Eclaims {
	/// <summary>
	/// United Kindgdom National Health Service (NHS).
	/// </summary>
	public class NHS {
		///<summary></summary>
		public NHS() {

		}

		///<summary>Returns true if the communications were successful, and false if they failed. If they failed, a rollback will happen automatically by deleting the previously created FP17 file. The batchnum is supplied for the possible rollback.</summary>
		public static bool Launch(Clearinghouse clearhouse,int batchNum) {
			bool retVal=true;


			return retVal;
		}
	}
}
