using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>An Aggregation Path contains the information needed to establish a connection to a remote office with the aggregate reporting console which has not yet been built.  It is assumed that this table will only be filled with rows on a special mostly-blank database located at the enterprise HQ, so the passwords are not encrypted.</summary>
	[Serializable()]
	public class CentralConnection{//:TableBase {//temporarily not TableBase
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long AggPathNum;
		///<summary>The address used to gain access to the remote location.  Can be on VPN, or can be over https.</summary>
		public string RemoteURI;
		///<summary>The username used to log in to the remote computer.</summary>
		public string RemoteUserName;
		///<summary>The password that corresponds to the RemoteUserName</summary>
		public string RemotePassword;

		///<summary>Returns a copy.</summary>
		public CentralConnection Copy() {
			return (CentralConnection)this.MemberwiseClone();
		}

	}
	


}













