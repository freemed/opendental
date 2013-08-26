using System;

namespace OpenDentBusiness{

	///<summary>One Rx for one patient. Copied from rxdef rather than linked to it.</summary>
	[Serializable]
	public class RxPat:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long RxNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>Date of Rx.</summary>
		public DateTime RxDate;
		///<summary>Drug name. Example: PenVK 500 mg capsules. Example: Percocet 5/500 tablets.</summary>
		public string Drug;
		///<summary>Directions. Example: Take 2 tablets qid. (qid means 4 times a day)</summary>
		public string Sig;
		///<summary>Amount to dispense. Example: 12 (twelve)</summary>
		public string Disp;
		///<summary>Number of refills. Example: 3.  Example: 1 per month.</summary>
		public string Refills;
		///<summary>FK to provider.ProvNum.</summary>
		public long ProvNum;
		///<summary>Notes specific to this Rx.  Will not show on the printout.  For staff use only.</summary>
		public string Notes;
		///<summary>FK to pharmacy.PharmacyNum.</summary>
		public long PharmacyNum;
		///<summary>Is a controlled substance.  This will affect the way it prints.</summary>
		public bool IsControlled;
		///<summary>The last date and time this row was altered.  Not user editable.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;
		///<summary>Enum:RxSendStatus </summary>
		public RxSendStatus SendStatus;
		///<summary>RxNorm Code identifier.  We should have used a string type.</summary>
		public long RxCui;
		///<summary>NCI Pharmaceutical Dosage Form code.  Only used with ehr.  For example, C48542 is the code for “Tablet dosing unit”.  User enters code manually, and it's only used for Rx Send, which will be deprecated with 2014 cert.  Guaranteed that nobody actually uses or cares about this field.</summary>
		public string DosageCode;
		///<summary>NewCrop returns this unique identifier to use for electronic Rx.</summary>
		public string NewCropGuid;

		///<summary></summary>
		public RxPat Copy() {
			return (RxPat)this.MemberwiseClone();
		}

	}

	public enum RxSendStatus {
		///<summary>0</summary>
		Unsent,
		///<summary>1- This will never be used in production.  It was only used for proof of concept when building EHR.</summary>
		InElectQueue,
		///<summary>2</summary>
		SentElect,
		///<summary>3</summary>
		Printed
	}


}













