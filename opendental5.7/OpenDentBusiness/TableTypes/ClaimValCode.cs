using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {

	public class ClaimValCode {
		///<summary>Primary key.</summary>
		public int ClaimValCodeLogNum;
		///<summary>FK to claim.ClaimNum.</summary>
		public int ClaimNum;
		///<summary>Descriptive abbreviation to help place field on form (Ex: "FL55" for field 55).</summary>
		public string ClaimField;
		///<summary>Value Code.</summary>
		public string ValCode;
		///<summary>Value Code Amount.</summary>
		public double ValAmount;
		///<summary>Order of Value Code</summary>
		public int Ordinal;
	}
}
