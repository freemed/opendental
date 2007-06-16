using System;
using System.Collections;
using System.Data;
using System.Text;

namespace OpenDentBusiness{

		///<summary>The claim table holds information about individual claims.  Each row represents one claim.</summary>
	public class Claim{
		///<summary>Primary key</summary>
		public int ClaimNum;
		///<summary>FK to patient.PatNum</summary>
		public int PatNum;//
		///<summary>Usually the same date as the procedures, but it can be changed if you wish.</summary>
		public DateTime DateService;//
		///<summary>Usually the date it was created.  It might be sent a few days later if you don't send your e-claims every day.</summary>
		public DateTime DateSent;
		///<summary>Single char: U,H,W,P,S,or R.  U=Unsent, H=Hold until pri received, W=Waiting in queue, S=Sent, R=Received.  A(adj) is no longer used.  P(prob sent) is no longer used.</summary>
		public string ClaimStatus;
		///<summary>Date the claim was received.</summary>
		public DateTime DateReceived;
		///<summary>FK to insplan.PlanNum.  Every claim is attached to one plan.</summary>
		public int PlanNum;
		///<summary>FK to provider.ProvNum.  Treating provider.</summary>
		public int ProvTreat;
		///<summary>Total fee of claim.</summary>
		public double ClaimFee;
		///<summary>Amount insurance is estimated to pay on this claim.</summary>
		public double InsPayEst;
		///<summary>Amount insurance actually paid.</summary>
		public double InsPayAmt;
		///<summary>Deductible applied to this claim.</summary>
		public double DedApplied;
		///<summary>The preauth number received from ins.</summary>
		public string PreAuthString;
		///<summary>Single char for No, Initial, or Replacement.</summary>
		public string IsProsthesis;
		///<summary>Date prior prosthesis was placed.  Note that this is only for paper claims.  E-claims have a date field on each individual procedure.</summary>
		public DateTime PriorDate;
		///<summary>Note for patient for why insurance didn't pay as expected.</summary>
		public string ReasonUnderPaid;
		///<summary>Note to be sent to insurance. Max 255 char.  E-claims also have notes on each procedure.</summary>
		public string ClaimNote;
		///<summary>"P"=primary, "S"=secondary, "PreAuth"=preauth, "Other"=other, "Cap"=capitation.  Not allowed to be blank. Might need to add "Med"=medical claim.</summary>
		public string ClaimType;
		///<summary>FK to provider.ProvNum.  Billing provider.  Assignment can be automated from the setup section.</summary>
		public int ProvBill;
		///<summary>FK to referral.ReferralNum.</summary>
		public int ReferringProv;
		///<summary>Referral number for this claim.</summary>
		public string RefNumString;
		///<summary>Enum:PlaceOfService .</summary>
		public PlaceOfService PlaceService;
		///<summary>blank or A=Auto, E=Employment, O=Other.</summary>
		public string AccidentRelated;
		///<summary>Date of accident, if applicable.</summary>
		public DateTime AccidentDate;
		///<summary>Accident state.</summary>
		public string AccidentST;
		///<summary>Enum:YN .</summary>
		public YN EmployRelated;
		///<summary>True if is ortho.</summary>
		public bool IsOrtho;
		///<summary>Remaining months of ortho. Valid values are 1-36.</summary>
		public int OrthoRemainM;
		///<summary>Date ortho appliance placed.</summary>
		public DateTime OrthoDate;
		///<summary>Enum:Relat  Relationship to subscriber.  The relationship is copied from InsPlan when the claim is created.  It might need to be changed in both places.</summary>
		public Relat PatRelat;
		///<summary>FK to insplan.PlanNum.  Other coverage plan number.  0 if none.  This provides the user with total control over what other coverage shows. This obviously limits the coverage on a single claim to two insurance companies.</summary>
		public int PlanNum2;
		///<summary>Enum:Relat  The relationship to the subscriber for other coverage on this claim.</summary>
		public Relat PatRelat2;
		///<summary>Sum of ClaimProc.Writeoff for this claim.</summary>
		public double WriteOff;
		///<summary>The number of x-rays enclosed.</summary>
		public int Radiographs;
		///<summary>FK to clinic.ClinicNum.  0 if no clinic.</summary>
		public int ClinicNum;
		///<summary>FK to claimform.ClaimFormNum.  0 if not assigned to use the claimform for the insplan.</summary>
		public int ClaimForm;
		///<summary>Enum:EtransType to define a specific version of an e-claim.  Only used for medical claims right now.</summary>
		public EtransType EFormat;

		///<summary>Returns a copy of the claim.</summary>
		public Claim Copy() {
			Claim c=new Claim();
			c.ClaimNum=ClaimNum;
			c.PatNum=PatNum;
			c.DateService=DateService;
			c.DateSent=DateSent;
			c.ClaimStatus=ClaimStatus;
			c.DateReceived=DateReceived;
			c.PlanNum=PlanNum;
			c.ProvTreat=ProvTreat;
			c.ClaimFee=ClaimFee;
			c.InsPayEst=InsPayEst;
			c.InsPayAmt=InsPayAmt;
			c.DedApplied=DedApplied;
			c.PreAuthString=PreAuthString;
			c.IsProsthesis=IsProsthesis;
			c.PriorDate=PriorDate;
			c.ReasonUnderPaid=ReasonUnderPaid;
			c.ClaimNote=ClaimNote;
			c.ClaimType=ClaimType;
			c.ProvBill=ProvBill;
			c.ReferringProv=ReferringProv;
			c.RefNumString=RefNumString;
			c.PlaceService=PlaceService;
			c.AccidentRelated=AccidentRelated;
			c.AccidentDate=AccidentDate;
			c.AccidentST=AccidentST;
			c.EmployRelated=EmployRelated;
			c.IsOrtho=IsOrtho;
			c.OrthoRemainM=OrthoRemainM;
			c.OrthoDate=OrthoDate;
			c.PatRelat=PatRelat;
			c.PlanNum2=PlanNum2;
			c.PatRelat2=PatRelat2;
			c.WriteOff=WriteOff;
			c.Radiographs=Radiographs;
			c.ClinicNum=ClinicNum;
			c.ClaimForm=ClaimForm;
			c.EFormat=EFormat;
			return c;
		}
	
	}

}
