using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Used by the Central Manager.  Stores the information needed to establish a connection to a remote database.</summary>
	[Serializable()]
	public class CentralConnection{//:TableBase {//not yet
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long CentralConnectionNum;
		///<summary>If direct db connection.  Can be ip address.</summary>
		public string ServerName;
		///<summary>If direct db connection.</summary>
		public string DatabaseName;
		///<summary>If direct db connection.</summary>
		public string MySqlUser;
		///<summary>If direct db connection.  Symmetrically encrypted.</summary>
		public string MySqlPassword;
		///<summary>If connecting to the web service. Can be on VPN, or can be over https.</summary>
		public string ServiceURI;
		///<summary>If connecting to the web service.</summary>
		public string OdUser;
		///<summary>If connecting to the web service.  Symmetrically encrypted.</summary>
		public string OdPassword;

		///<summary>Returns a copy.</summary>
		public CentralConnection Copy() {
			return (CentralConnection)this.MemberwiseClone();
		}

	}
	


}













