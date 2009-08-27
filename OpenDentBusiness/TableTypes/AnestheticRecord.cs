using System;

namespace OpenDentBusiness{

		///<summary>One anesthetic record for one patient on one date.</summary>
		public class AnestheticRecord{
		///<summary>Primary key.</summary>
		public long AnestheticRecordNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary></summary>
		public DateTime AnestheticDate;
		///<summary>FK to provider.ProvNum.</summary>
		public long ProvNum;

		public AnestheticRecord Copy()
		{
			return (AnestheticRecord)this.MemberwiseClone();
		}
	}







}







