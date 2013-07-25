using System;
using System.Collections;

namespace OpenDentBusiness {
	///<summary>Stores encrypted hashes of audit logs for security purposes.  User not allowed to edit.</summary>
	[Serializable]
	public class SecurityLogHash:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long SecurityLogHashNum;
		///<summary>FK to securityLog.SecurityLogNum.</summary>
		public long SecurityLogNum;
		///<summary>The SHA-256 hash of the entire security log.  Used to encrypt the audit trail and detect if the entry has been altered outside of Open Dental.</summary>
		public string LogHash;


		///<summary></summary>
		public SecurityLogHash Clone() {
			return (SecurityLogHash)this.MemberwiseClone();
		}

	}
}