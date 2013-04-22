using System;

namespace OpenDentBusiness {

	///<summary>Only used at HQ.  If a row is present in this table, then this customer is a reseller.  Also holds their credentials for the reseller portal.</summary>
	[Serializable]
	public class Reseller:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ResellerNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>User name used to log into the reseller portal with.</summary>
		public string UserName;
		///<summary>Password used to log into the reseller portal with.  Stored as plain text.</summary>
		public string ResellerPassword;

		///<summary>Returns a copy of this Reseller.</summary>
		public Reseller Copy() {
			return (Reseller)this.MemberwiseClone();
		}


	}





}













