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

namespace OpenDental.Eclaims
{
	/// <summary>
	/// aka Mercury Dental Exchange.
	/// </summary>
	public class MercuryDE{
		///<summary></summary>
		public MercuryDE()
		{
			
		}

		///<summary>Returns true if the communications were successful, and false if they failed.</summary>
		public static bool Launch(Clearinghouse clearhouse,int batchNum){
			//Step 1: Retrieve reports regarding the existing pending claim statuses.
			//Step 2: Send new claims in a new batch.
			if(batchNum==0){
				//Only retrieving reports so do not send.
				return true;
			}






			return true;
		}
	}

}
