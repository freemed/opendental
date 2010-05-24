using System;
using System.Collections;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness{

	///<summary>Represents one extraction that goes out on a Canadian claim.  This is needed because they have stricter requirements in this area, and they also need date of extraction if prosthesis.  It also provides a permanent record of what was sent.</summary>
	[Serializable()]
	public class CanadianExtract:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long CanadianExtractNum;
		///<summary>FK to claim.ClaimNum.</summary>
		public long ClaimNum;
		///<summary>Always 1-32 or A-T.  1 or 2 char.  Get's converted to international tooth numbers just before display.</summary>
		public string ToothNum;
		///<summary>Will be min val if no date.</summary>
		public DateTime DateExtraction;

		///<summary></summary>
		public CanadianExtract Copy() {
			return (CanadianExtract)this.MemberwiseClone();
		}
	}

	
}




