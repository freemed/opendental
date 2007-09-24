using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>There is a separate insplan in this table for each subscriber.  Subscribers never share insplans, although multiple patients can have the plan assigned to them (with the one subscriber).  But plans can have identical information in them as other plans.  In this case, they are considered similar or identical, and some synchronization can be done.  For some offices, this synchronization will be extensive, with hundreds of identical insplans.</summary>
	public class InsPlan{//:IComparable{
		///<summary>Primary key.</summary>
		public int PlanNum;
		///<summary>FK to patient.PatNum.</summary>
		public int Subscriber;
		///<summary>Date plan became effective.</summary>
		public DateTime DateEffective;
		///<summary>Date plan was terminated</summary>
		public DateTime DateTerm;
		///<summary>Optional</summary>
		public string GroupName;
		///<summary>Optional.  In Canada, this is called the Plan Number.</summary>
		public string GroupNum;
		///<summary>Note for all plans identical to this one.  Always stays in synch with other identical plans regardless of user actions.  If they change it on one, it gets changed on all.</summary>
		public string PlanNote;
		///<summary>FK to definition.DefNum.  Name of fee schedule is stored in definition.ItemName.</summary>
		public int FeeSched;
		///<summary>Release of information signature is on file.</summary>
		public bool ReleaseInfo;
		///<summary>Assignment of benefits signature is on file.</summary>
		public bool AssignBen;
		///<summary>""=percentage(the default),"p"=ppo_percentage,"f"=flatCopay,"c"=capitation.</summary>
		public string PlanType;
		///<summary>FK to claimform.ClaimFormNum. eg. "1" for ADA2002.  For ADA2006, it varies by office.</summary>
		public int ClaimFormNum;
		///<summary>0=no,1=yes.  could later be extended if more alternates required</summary>
		public bool UseAltCode;
		///<summary>Fee billed on claim should be the UCR fee for the patient's provider.</summary>
		public bool ClaimsUseUCR;
		///<summary>FK to definition.DefNum. Not usually used. This fee schedule holds only co-pays(patient portions).  Only used for Capitation or for fixed copay plans.</summary>
		public int CopayFeeSched;
		///<summary>Usually SSN, but can also be changed by user.  No dashes. Not allowed to be blank.</summary>
		public string SubscriberID;
		///<summary>FK to employer.EmployerNum.</summary>
		public int EmployerNum;
		///<summary>FK to carrier.CarrierNum.</summary>
		public int CarrierNum;
		///<summary>FK to Definition.DefNum. Not usually used.  This fee schedule holds amounts allowed by carriers.</summary>
		public int AllowedFeeSched;
		///<summary>.</summary>
		public string TrojanID;
		///<summary>Only used in Canada. It's a suffix to the plan number (group number).</summary>
		public string DivisionNo;
		///<summary>User doesn't usually put these in.  Only used when automatically requesting benefits, such as with Trojan.  All the benefits get stored here in text form for later reference.  Specific to one plan and not synchronized because might be specific to subscriber.  If blank, we might add a function to try to find any benefitNote for a similar plan.</summary>
		public string BenefitNotes;
		///<summary>True if this is medical insurance rather than dental insurance.</summary>
		public bool IsMedical;
		///<summary>Specific to an individual plan and not synchronized in any way.  Use to store any other info that affects coverage.</summary>
		public string SubscNote;
		///<summary>Enum:InsFilingCode Only used for e-claims.  Should become obsolete when PlanID implemented by HIPAA.</summary>
		public InsFilingCode FilingCode;
		///<summary>Canadian e-claim field. D11 and E07.  Mandatory for Dentaide.  Value must be greater than 0.  Not used for all others.  2 digit.</summary>
		public int DentaideCardSequence;
		///<summary>If checked, the units Qty will show the base units assigned to a procedure on the claim form.</summary>
		public bool ShowBaseUnits;
		///<Summary>Apply deductible before percentage when calculating estimates.</Summary>
		public bool DedBeforePerc;
		///<summary>This is not a database column.  It is just used to display the number of plans with the same info.</summary>
		public int NumberPlans;

		/*
		///<summary>IComparable.CompareTo implementation.  This is used to determine if plans are identical.  The criteria is that they have 6 fields in common: Employer, Carrier, GroupName, GroupNum, DivisionNo, and IsMedical.  There is no less than or greater than; just not equal.</summary>
		public int CompareTo(object obj) {
			if(!(obj is InsPlan)) {
				throw new ArgumentException("object is not an InsPlan");
			}
			InsPlan plan=(InsPlan)obj;
			if(plan.EmployerNum==EmployerNum
				&& plan.CarrierNum==CarrierNum
				&& plan.GroupName==GroupName
				&& plan.GroupNum==GroupNum
				&& plan.DivisionNo==DivisionNo
				&& plan.IsMedical==IsMedical)
			{
				return 0;//they are the same
			}
			return -1;
		}*/

		///<summary>Returns a copy of this InsPlan.</summary>
		public InsPlan Copy(){
			return (InsPlan)this.MemberwiseClone();
		}

		

		

		
		



	}

	

	

	


}













