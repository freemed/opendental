using System;
using System.Collections;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness{

	///<summary>There are so many extra fields required for Canadian claims that we made a new table for them.  Each row in this table will have a 1:1 relationship with a claim.</summary>
	public class CanadianClaim{
		//<summary>Primary key.</summary>
		//public int CanadianClaimNum;
		///<summary>FK to claim.ClaimNum. Also the primary key.</summary>
		public int ClaimNum;
		///<summary>A08.  Any combination of E(email), C(correspondence), M(models), X(x-rays), and I(images).  So up to 5 char.  Gets converted to a single char A-Z for e-claims.</summary>
		public string MaterialsForwarded;
		///<summary>B05.  Optional. The 9-digit CDA number of the referring provider, or identifier of referring party up to 10 characters in length.</summary>
		public string ReferralProviderNum;
		///<summary>B06.  A number 0(none) through 13.</summary>
		public int ReferralReason;
		///<summary>E18.  Y, N, or X(yes, but details unknown).  User must enter without default.</summary>
		public string SecondaryCoverage;
		///<summary>F18.  Y, N, or X(not a lower denture, crown, or bridge).</summary>
		public string IsInitialLower;
		///<summary>F19.  Mandatory if F18 is N.</summary>
		public DateTime DateInitialLower;
		///<summary>F21.  If crown, not required.  If denture or bridge, required if F18 is N.  Single digit number code, 0-6.  We added type 7, which is crown.</summary>
		public int MandProsthMaterial;
		///<summary>F15.  Y, N, or X(not an upper denture, crown, or bridge).</summary>
		public string IsInitialUpper;
		///<summary>F04.  Mandatory if F15 is N.</summary>
		public DateTime DateInitialUpper;
		///<summary>F20.  If crown, not required.  If denture or bridge, required if F15 is N.  Single digit number code, 0-6.  We added type 7, which is crown.</summary>
		public int MaxProsthMaterial;
		///<summary>C09.  A single digit 1-4.  0 is not acceptable for e-claims.</summary>
		public int EligibilityCode;
		///<summary>C10.  Can't initially copy from patient.SchoolName because not allowed to default.  Must ask patient each time.</summary>
		public string SchoolName;
		///<summary>F01.  Mandatory. Single digit 1-4.</summary>
		public int PayeeCode;

		/*
		///<summary></summary>
		public CanadianClaim Copy() {
			Account a=new Account();
			a.AccountNum=AccountNum;
			a.Description=Description;
			a.AcctType=AcctType;
			a.BankNumber=BankNumber;
			a.Inactive=Inactive;
			a.AccountColor=AccountColor;
			return a;
		}*/
	}

	
}




