using System;
using System.Collections;

namespace OpenDentBusiness {
	///<summary>Used in EHR only.  Stores an entry indicating whether the office has accepted or denied the amendment.  Amendments can be verbal or written requests to add information to the patient's record.  The provider can either scan / import the document or create a detailed description that indicates what was verbally requested or where the document can be found.</summary>
	[Serializable]
	public class EhrAmendment:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrAmendmentNum;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary>Indicates whether the amendment was accepted or denied.</summary>
		public bool IsAccepted;
		///<summary>User-defined description of the amendment</summary>
		public string Description;
		///<summary>Enum:AmendmentSource Patient, Provider, Organization, Other.</summary>
		public AmendmentSource Source;
		///<summary>Date and time of the amendment entry.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTCreated;
		///<summary>The file is stored in the A-Z folder in 'EhrAmendments' folder.  This field stores the name of the file.  The files are named automatically based on Date/time along with EhrAmendmentNum for uniqueness.</summary>
		public string FileName;
		///<summary>The raw file data encoded as base64.  Only used if there is no AtoZ folder.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string RawBase64;

		///<summary></summary>
		public EhrAmendment Clone() {
			return (EhrAmendment)this.MemberwiseClone();
		}

		///<summary>Source Enumeration</summary>
		public enum AmendmentSource {
			///<summary>0</summary>
			Patient,
			///<summary>1</summary>
			Provider,
			///<summary>2</summary>
			Organization,
			///<summary>3</summary>
			Other
		}

	}


}
