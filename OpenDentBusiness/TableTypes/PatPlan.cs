using System;
using System.Collections;

namespace OpenDentBusiness{
	
	/// <summary>Each row represents the linking of one insplan to one patient for current coverage.  Dropping a plan will delete the entry in this table.  Deleting a plan will delete the actual insplan (if no dependencies).</summary>
	public class PatPlan{
		/// <summary>Primary key</summary>
		public int PatPlanNum;
		/// <summary>FK to  patient.PatNum.  The patient who currently has the insurance.  Not the same as the subscriber.</summary>
		public int PatNum;
		///<summary>FK to insplan.PlanNum.  The insurance plan attached to the patient.</summary>
		public int PlanNum;
		///<summary>Number like 1, 2, 3, etc.  Represents primary ins, secondary ins, tertiary ins, etc. 0 is not used</summary>
		public int Ordinal;
		///<summary>For informational purposes only. For now, we lose the previous feature which let us set isPending without entering a plan.  Now, you have to enter the plan in order to check this box.</summary>
		public bool IsPending;
		///<summary>Enum:Relat Remember that this may need to be changed in the Claim also, if already created.</summary>
		public Relat Relationship;
		///<summary>An optional patient ID which will override the SSN on eclaims if present.  Subscriber ID is part of the insplan.</summary>
		public string PatID;

		///<summary></summary>
		public PatPlan Copy(){
			PatPlan p=new PatPlan();
			p.PatPlanNum=PatPlanNum;
			p.PatNum=PatNum;
			p.PlanNum=PlanNum;
			p.Ordinal=Ordinal;
			p.IsPending=IsPending;
			p.Relationship=Relationship;
			p.PatID=PatID;
			return p;
		}



		
	}

	

	


}










