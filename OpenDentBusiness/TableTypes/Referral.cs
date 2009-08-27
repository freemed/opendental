using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>All info about a referral is stored with that referral even if a patient.  That way, it's available for easy queries.</summary>
	public class Referral{
		///<summary>Primary key.</summary>
		public long ReferralNum;
		///<summary>Last name.</summary>
		public string LName;
		///<summary>First name.</summary>
		public string FName;
		///<summary>Middle name or initial.</summary>
		public string MName;
		///<summary>SSN or TIN, no punctuation.  For Canada, this holds the referring provider CDA num for claims.</summary>
		public string SSN;
		///<summary>Specificies if SSN is real SSN.</summary>
		public bool UsingTIN;
		///<summary>Enum:DentalSpecialty</summary>
		public DentalSpecialty Specialty;
		///<summary>State</summary>
		public string ST;
		///<summary>Primary phone, restrictive, must only be 10 digits and only numbers.</summary>
		public string Telephone;
		///<summary>.</summary>
		public string Address;
		///<summary>.</summary>
		public string Address2;
		///<summary>.</summary>
		public string City;
		///<summary>.</summary>
		public string Zip;
		///<summary>Holds important info about the referral.</summary>
		public string Note;
		///<summary>Additional phone no restrictions</summary>
		public string Phone2;
		///<summary>Can't delete a referral, but can hide if not needed any more.</summary>
		public bool IsHidden;
		///<summary>Set to true for referralls such as Yellow Pages.</summary>
		public bool NotPerson;
		///<summary>i.e. DMD or DDS</summary>
		public string Title;
		///<summary>.</summary>
		public string EMail;
		///<summary>FK to patient.PatNum for referrals that are patients.</summary>
		public long PatNum;
		///<summary>NPI for the referral</summary>
		public string NationalProvID;
		///<summary>FK to sheetdef.SheetDefNum.  Referral slips can be set for individual referral sources.  If zero, then the default internal referral slip will be used instead of a custom referral slip.</summary>
		public long Slip;

		///<summary>Returns a copy of this Referral.</summary>
		public Referral Copy(){
			return (Referral)this.MemberwiseClone();
		}
		

	}


}













