using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>An allergy attached to a patient and linked to an AllergyDef.</summary>
	[Serializable]
	public class Allergy:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long AllergyNum;
		///<summary>FK to allergydef.AllergyDefNum</summary>
		public long AllergyDefNum;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary></summary>
		public string Reaction;
		///<summary></summary>
		public bool StatusIsActive;
		///<summary>To be used for synch with web server for CertTimelyAccess.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;

		///<summary></summary>
		public Allergy Copy() {
			return (Allergy)this.MemberwiseClone();
		}
	}
}