using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>An Aggrigation Path contains the information needed to establish a connection to a remote office with the aggregate reporting console.</summary>
	[Serializable()]
	public class AggPath:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long AggPathNum;
		///<summary>The address used to gain access to the remote location.</summary>
		public string RemoteURI;
		///<summary>The username used to log into the remote computer.</summary>
		public string RemoteUserName;
		///<summary>The password that corresponds to the RemoteUserName</summary>
		public string RemotePassword;

		///<summary>Returns a copy of this Clinic.</summary>
		public AggPath Copy() {
			return (AggPath)this.MemberwiseClone();
		}

	}
	


}













