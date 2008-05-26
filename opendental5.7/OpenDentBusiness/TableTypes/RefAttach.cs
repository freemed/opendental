using System;

namespace OpenDentBusiness{

	///<summary>Attaches a referral to a patient.</summary>
	public class RefAttach{  
		///<summary>Primary key.</summary>
		public int RefAttachNum;
		///<summary>FK to referral.ReferralNum.</summary>
		public int ReferralNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Order to display in patient info. Will be automated more in future.</summary>
		public int ItemOrder;
		///<summary>Date of referral.</summary>
		public DateTime RefDate;//
		///<summary>true=from, false=to</summary>
		public bool IsFrom;
		///<summary>Enum:ReferralToStatus 0=None,1=Declined,2=Scheduled,3=Consulted,4=InTreatment,5=Complete.</summary>
		public ReferralToStatus RefToStatus;
		///<summary>Why the patient was referred out, or less commonly, the circumstances of the referral source.</summary>
		public string Note;

		///<summary>Returns a copy of this RefAttach.</summary>
		public RefAttach Copy(){
			return (RefAttach)this.MemberwiseClone();
		}


	}

	

	

}













