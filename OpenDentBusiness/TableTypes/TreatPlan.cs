using System;

namespace OpenDentBusiness{

	///<summary>A treatment plan saved by a user.  Does not include the default tp, which is just a list of procedurelog entries with a status of tp.  A treatplan has many proctp's attached to it.</summary>
	public class TreatPlan{
		///<summary>Primary key.</summary>
		public long TreatPlanNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>The date of the treatment plan</summary>
		public DateTime DateTP;
		///<summary>The heading that shows at the top of the treatment plan.  Usually 'Proposed Treatment Plan'</summary>
		public string Heading;
		///<summary>A note specific to this treatment plan that shows at the bottom.</summary>
		public string Note;
		///<summary>The encrypted and bound signature in base64 format.  The signature is bound to the byte sequence of the original image.</summary>
		public string Signature;
		///<summary>True if the signature is in Topaz format rather than OD format.</summary>
		public bool SigIsTopaz;
		///<summary>FK to patient.PatNum. Can be 0.  The patient responsible for approving the treatment.  Public health field not visible to everyone else.</summary>
		public long ResponsParty;
		
		///<summary></summary>
		public TreatPlan Copy(){
			return (TreatPlan)MemberwiseClone();
		}

		

	
	}

	

	


}




















