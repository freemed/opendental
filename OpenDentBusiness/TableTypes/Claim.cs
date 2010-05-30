using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness{

	///<summary>The claim table holds information about individual claims.  Each row represents one claim.</summary>
	[Serializable()]
	public class Claim:TableBase{
		///<summary>Primary key</summary>
		[CrudColumn(IsPriKey=true)]
		public long ClaimNum;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;//
		///<summary>Usually the same date as the procedures, but it can be changed if you wish.</summary>
		public DateTime DateService;
		///<summary>Usually the date it was created.  It might be sent a few days later if you don't send your e-claims every day.</summary>
		public DateTime DateSent;
		///<summary>Single char: U,H,W,P,S,or R.  U=Unsent, H=Hold until pri received, W=Waiting in queue, S=Sent, R=Received.  A(adj) is no longer used.  P(prob sent) is no longer used.</summary>
		public string ClaimStatus;
		///<summary>Date the claim was received.</summary>
		public DateTime DateReceived;
		///<summary>FK to insplan.PlanNum.  Every claim is attached to one plan.</summary>
		public long PlanNum;
		///<summary>FK to provider.ProvNum.  Treating provider.</summary>
		public long ProvTreat;
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
		public long ProvBill;
		///<summary>FK to referral.ReferralNum.</summary>
		public long ReferringProv;
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
		public byte OrthoRemainM;
		///<summary>Date ortho appliance placed.</summary>
		public DateTime OrthoDate;
		///<summary>Enum:Relat  Relationship to subscriber.  The relationship is copied from InsPlan when the claim is created.  It might need to be changed in both places.</summary>
		public Relat PatRelat;
		///<summary>FK to insplan.PlanNum.  Other coverage plan number.  0 if none.  This provides the user with total control over what other coverage shows. This obviously limits the coverage on a single claim to two insurance companies.</summary>
		public long PlanNum2;
		///<summary>Enum:Relat  The relationship to the subscriber for other coverage on this claim.</summary>
		public Relat PatRelat2;
		///<summary>Sum of ClaimProc.Writeoff for this claim.</summary>
		public double WriteOff;
		///<summary>The number of x-rays enclosed.</summary>
		public byte Radiographs;
		///<summary>FK to clinic.ClinicNum.  0 if no clinic.  Since one claim cannot have procs from multiple clinics, the clinicNum is set when creating the claim and then cannot be changed.  The claim would have to be deleted and recreated.  Otherwise, if changing at the claim level, a feature would have to be added that synched all procs, claimprocs, and probably some other tables.</summary>
		public long ClinicNum;
		///<summary>FK to claimform.ClaimFormNum.  0 if not assigned to use the claimform for the insplan.</summary>
		public long ClaimForm;
		///<summary>Enum:EtransType to define a specific version of an e-claim.  Only used for medical claims right now.</summary>
		public EtransType EFormat;
		///<summary>The number of intraoral images attached.  Not the number of files attached.  This is the value that goes on the 2006 claimform.</summary>
		public int AttachedImages;
		///<summary>The number of models attached.</summary>
		public int AttachedModels;
		///<summary>A comma-delimited set of flag keywords.  Can have one or more of the following: EoB,Note,Perio,Misc.  Must also contain one of these: Mail or Elect.</summary>
		public string AttachedFlags;
		///<summary>Example: NEA#1234567.  If present, and if the claim note does not already start with this Id, then it will be prepended to the claim note for both e-claims and mail.  If using e-claims, this same ID will be used for all PWK segements.</summary>
		public string AttachmentID;
		///<summary>A08.  Any combination of E(email), C(correspondence), M(models), X(x-rays), and I(images).  So up to 5 char.  Gets converted to a single char A-Z for e-claims.</summary>
		public string CanadianMaterialsForwarded;
		///<summary>B05.  Optional. The 9-digit CDA number of the referring provider, or identifier of referring party up to 10 characters in length.</summary>
		public string CanadianReferralProviderNum;
		///<summary>B06.  A number 0(none) through 13.</summary>
		public byte CanadianReferralReason;
		///<summary>F18.  Y, N, or X(not a lower denture, crown, or bridge).</summary>
		public string CanadianIsInitialLower;
		///<summary>F19.  Mandatory if F18 is N.</summary>
		public DateTime CanadianDateInitialLower;
		///<summary>F21.  If crown, not required.  If denture or bridge, required if F18 is N.  Single digit number code, 0-6.  We added type 7, which is crown.</summary>
		public byte CanadianMandProsthMaterial;
		///<summary>F15.  Y, N, or X(not an upper denture, crown, or bridge).</summary>
		public string CanadianIsInitialUpper;
		///<summary>F04.  Mandatory if F15 is N.</summary>
		public DateTime CanadianDateInitialUpper;
		///<summary>F20.  If crown, not required.  If denture or bridge, required if F15 is N.  0 indicates empty response.  Single digit number code, 1-6.  We added type 7, which is crown.</summary>
		public byte CanadianMaxProsthMaterial;

		///<summary>Not a data column.</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public List<ClaimAttach> Attachments;

		public Claim(){
			AttachedFlags="";
			Attachments=new List<ClaimAttach>();
		}

		///<summary>Returns a copy of the claim.</summary>
		public Claim Copy() {
			Claim c=(Claim)MemberwiseClone();
			c.Attachments=new List<ClaimAttach>();
			for(int i=0;i<Attachments.Count;i++){
				c.Attachments.Add(Attachments[i].Copy());
			}
			return c;
		}

		public override bool Equals(object obj){
			if(obj == null || GetType() != obj.GetType()){
				return false;
			}
			Claim c = (Claim)obj;
			return (ClaimNum == c.ClaimNum);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}
	}
}
