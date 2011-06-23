using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>Can also be a CCR.  Sent CCDs are not saved here, only received CCDs/CCRs.  To display a saved Ccd, it is combined with an internal stylesheet.</summary>
	[Serializable]
	public class EhrSummaryCcd:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrSummaryCcdNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>Date that this Ccd was received.</summary>
		public DateTime DateSummary;
		///<summary>The xml content of the received text file.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string ContentSummary;

		///<summary></summary>
		public EhrSummaryCcd Copy() {
			return (EhrSummaryCcd)MemberwiseClone();
		}

	}

	

}
