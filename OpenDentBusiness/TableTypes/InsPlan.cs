using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Subscribers can share insplans by using the InsSub table.  The patplan table determines coverage for individual patients.  InsPlans can also exist without any subscriber.</summary>
	[Serializable]
	public class InsPlan:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PlanNum;
		///<summary>Optional</summary>
		public string GroupName;
		///<summary>Optional.  In Canada, this is called the Plan Number.</summary>
		public string GroupNum;
		///<summary>Note for this plan.  Same for all subscribers.</summary>
		public string PlanNote;
		///<summary>FK to feesched.FeeSchedNum.</summary>
		public long FeeSched;
		///<summary>""=percentage(the default),"p"=ppo_percentage,"f"=flatCopay,"c"=capitation.</summary>
		public string PlanType;
		///<summary>FK to claimform.ClaimFormNum. eg. "1" for ADA2002.  For ADA2006, it varies by office.</summary>
		public long ClaimFormNum;
		///<summary>0=no,1=yes.  could later be extended if more alternates required</summary>
		public bool UseAltCode;
		///<summary>Fee billed on claim should be the UCR fee for the patient's provider.</summary>
		public bool ClaimsUseUCR;
		///<summary>FK to feesched.FeeSchedNum. Not usually used. This fee schedule holds only co-pays(patient portions).  Only used for Capitation or for fixed copay plans.</summary>
		public long CopayFeeSched;
		///<summary>FK to employer.EmployerNum.</summary>
		public long EmployerNum;
		///<summary>FK to carrier.CarrierNum.</summary>
		public long CarrierNum;
		///<summary>FK to feesched.FeeSchedNum. Not usually used.  This fee schedule holds amounts allowed by carriers.</summary>
		public long AllowedFeeSched;
		///<summary>.</summary>
		public string TrojanID;
		///<summary>Only used in Canada. It's a suffix to the plan number (group number).</summary>
		public string DivisionNo;
		///<summary>True if this is medical insurance rather than dental insurance.</summary>
		public bool IsMedical;
		///<summary>FK to insfilingcode.InsFilingCodeNum.  Used for e-claims.  Also used for some complex reports in public health.  The e-claim usage might become obsolete when PlanID implemented by HIPAA.  Can be 0 to indicate none.  Then 'CI' will go out on claims.</summary>
		public long FilingCode;
		///<summary>Canadian e-claim field. D11 and E07.  Zero indicates empty.  Mandatory value for Dentaide.  Not used for all others.  2 digit.</summary>
		public byte DentaideCardSequence;
		///<summary>If checked, the units Qty will show the base units assigned to a procedure on the claim form.</summary>
		public bool ShowBaseUnits;
		///<summary>Set to true to not allow procedure code downgrade substitution on this insurance plan.</summary>
		public bool CodeSubstNone;
		///<summary>Set to true to hide it from the pick list and from the main list.</summary>
		public bool IsHidden;
		///<summary>The month, 1 through 12 when the insurance plan renews.  It will renew on the first of the month.  To indicate calendar year, set renew month to 0.</summary>
		public byte MonthRenew;
		///<summary>FK to insfilingcodesubtype.insfilingcodesubtypenum</summary>
		public long FilingCodeSubtype;
		///<summary>Canadian C12.  Single char, usually blank.  A=Newfoundland MCP Plan - Provincial Medical Plan.  V=Veteran's Affairs Plan.  N=NIHB(poorly documented).  If N, then it does not go out in messages, blank is sent instead.  At least that's the way to make in comply with the scripts; see Elig5.</summary>
		public string CanadianPlanFlag;
		

		///<summary>This is not a database column.  It is just used to display the number of plans with the same info.</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public int NumberSubscribers;

		/*
		///<summary>IComparable.CompareTo implementation.  This is used to determine if plans are identical.  The criteria is that they have 6 fields in common: Employer, Carrier, GroupName, GroupNum, DivisionNo, and IsMedical.  There is no less than or greater than; just not equal.</summary>
		public long CompareTo(object obj) {
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













