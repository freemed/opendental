using System;

namespace OpenDentBusiness{

	///<summary>A treatment plan saved by a user.  Does not include the default tp, which is just a list of procedurelog entries with a status of tp.  A treatplan has many proctp's attached to it.</summary>
	public class TreatPlan{
		///<summary>Primary key.</summary>
		public int TreatPlanNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>The date of the treatment plan</summary>
		public DateTime DateTP;
		///<summary>The heading that shows at the top of the treatment plan.  Usually 'Proposed Treatment Plan'</summary>
		public string Heading;
		///<summary>A note specific to this treatment plan that shows at the bottom.</summary>
		public string Note;
		
		///<summary></summary>
		public TreatPlan Copy(){
			TreatPlan t=new TreatPlan();
			t.TreatPlanNum=TreatPlanNum;
			t.PatNum=PatNum;
			t.DateTP=DateTP;
			t.Heading=Heading;
			t.Note=Note;
			return t;
		}

	
	}

	

	


}




















