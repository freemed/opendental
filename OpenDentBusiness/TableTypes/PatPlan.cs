using System;
using System.Collections;

namespace OpenDentBusiness{

	/// <summary>Each row represents the linking of one insplan to one patient for current coverage.  Dropping a plan will delete the entry in this table.  Deleting a plan will delete the actual insplan (if no dependencies).</summary>
	[Serializable]
	public class PatPlan:TableBase {
		/// <summary>Primary key</summary>
		[CrudColumn(IsPriKey=true)]
		public long PatPlanNum;
		/// <summary>FK to  patient.PatNum.  The patient who currently has the insurance.  Not the same as the subscriber.</summary>
		public long PatNum;
		///<summary>Number like 1, 2, 3, etc.  Represents primary ins, secondary ins, tertiary ins, etc. 0 is not used</summary>
		public byte Ordinal;
		///<summary>For informational purposes only. For now, we lose the previous feature which let us set isPending without entering a plan.  Now, you have to enter the plan in order to check this box.</summary>
		public bool IsPending;
		///<summary>Enum:Relat Remember that this may need to be changed in the Claim also, if already created.</summary>
		public Relat Relationship;
		///<summary>An optional patient ID which will override the insplan.SubscriberID on eclaims.  For Canada, this holds the Dependent Code, C17 and E17, and in that use it doesn't override subscriber id, but instead supplements it.</summary>
		public string PatID;
		/// <summary>FK to inssub.InsSubNum.  Gives info about the subscriber.</summary>
		public long InsSubNum;

		///<summary></summary>
		public PatPlan Copy(){
			return (PatPlan)this.MemberwiseClone();
		}



		
	}

	

	


}










