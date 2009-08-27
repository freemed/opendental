using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Database table is procedurelog.  A procedure for a patient.  Can be treatment planned or completed.  Once it's completed, it gets tracked more closely be the security portion of the program.  A procedure can NEVER be deleted.  Status can just be changed to "deleted".</summary>
	public class Procedure{
		///<summary>Primary key.</summary>
		public long ProcNum;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary>FK to appointment.AptNum.  Only allowed to attach proc to one appt(not counting planned appt)</summary>
		public long AptNum;
		///<summary>No longer used.</summary>
		public string OldCode;
		///<summary>Procedure date/time that will show in the account as the date performed.  If just treatment planned, the date can be the date it was tp'd, or the date can be min val if we don't care.</summary>
		public DateTime ProcDate;
		///<summary>Procedure fee.</summary>
		public double ProcFee;
		///<summary>Surfaces, or use "UL" etc for quadrant, "2" etc for sextant, "U","L" for arches.</summary>
		public string Surf;
		///<summary>May be blank, otherwise 1-32, 51-82, A-T, or AS-TS, 1 or 2 char.</summary>
		public string ToothNum;
		///<summary>May be blank, otherwise is series of toothnumbers separated by commas.</summary>
		public string ToothRange;
		///<summary>FK to definition.DefNum, which contains the text of the priority.</summary>
		public long Priority;
		///<summary>Enum:ProcStat TP=1,Complete=2,Existing Cur Prov=3,Existing Other Prov=4,Referred=5,Deleted=6,Condition=7.</summary>
		public ProcStat ProcStatus;
		///<summary>FK to provider.ProvNum.</summary>
		public long ProvNum;
		///<summary>FK to definition.DefNum, which contains text of the Diagnosis.</summary>
		public long Dx;
		///<summary>FK to appointment.AptNum.  Was called NextAptNum in older versions.  Allows this procedure to be attached to a Planned appointment as well as a standard appointment.</summary>
		public long PlannedAptNum;
		///<summary>Enum:PlaceOfService  Only used in Public Health. Zero(Office) until procedure set complete. Then it's set to the value of the DefaultProcedurePlaceService preference.</summary>
		public PlaceOfService PlaceService;
		///<summary>Single char. Blank=no, or Initial, or Replacement.</summary>
		public string Prosthesis;
		///<summary>For a prosthesis Replacement, this is the original date.</summary>
		public DateTime DateOriginalProsth;
		///<summary>This note will go on e-claim. For By Report, prep dates, or initial endo date.</summary>
		public string ClaimNote;
		///<summary>This is the date this procedure was entered or set complete.  If not status C, then the value is ignored, so it might be minValue 0001-01-01 or any other date.  It gets updated when set complete.  User never allowed to edit.  This will be enhanced later.</summary>
		public DateTime DateEntryC;
		///<summary>FK to clinic.ClinicNum.  0 if no clinic.</summary>
		public long ClinicNum;
		///<summary>FK to procedurecode.ProcCode. Optional.</summary>
		public string MedicalCode;
		///<summary>Simple text for ICD-9 code. Gets sent with medical claims.</summary>
		public string DiagnosticCode;
		///<summary>Set true if this medical diagnostic code is the principal diagnosis for the visit.  If no principal diagnosis is marked for any procedures on a medical e-claim, then it won't be allowed to be sent.  If more than one is marked, then it will just use one at random.</summary>
		public bool IsPrincDiag;
		//<summary>This is only used in Canada because their insurance companies require this number.</summary>
		//public double LabFee;//need to delete.
		//<summary>FK to procedurecode.ProcCode.  Only used in Canada.</summary>
		//public string LabProcCode;//need to delete.
		///<summary>FK to procedurelog.ProcNum. Only used in Canada. If not zero, then this proc is a lab fee and this indicates to which actual procedure the lab fee is attached.  For ordinary use, they are treated like two separate procedures.  It's only for insurance claims that we need to know which lab fee belongs to which procedure.  For now, we limit one fee attached to one procedure.</summary>
		public long ProcNumLab;
		///<summary>FK to definition.DefNum. Lets some users track charges for certain types of reports.  For example, a Medicaid billing type could be assigned to a procedure, flagging it for inclusion in a report mandated by goverment.  Would be more useful if it was automated to flow down based on insurance plan type, but that can be added later.  Not visible if prefs.EasyHideMedicaid is true.</summary>
		public long BillingTypeOne;
		///<summary>FK to definition.DefNum.  Same as BillingTypeOne, but used when there is a secondary billing type to account for.</summary>
		public long BillingTypeTwo;
		///<summary>FK to procedurecode.CodeNum</summary>
		public long CodeNum;
		///<summary>Modifier for certain CPT codes.  Not used yet.</summary>
		public string CodeMod1;
		///<summary>Modifier for certain CPT codes.  Not used yet.</summary>
		public string CodeMod2;
		///<summary>Modifier for certain CPT codes.  Not used yet.</summary>
		public string CodeMod3;
		///<summary>Modifier for certain CPT codes.  Not used yet.</summary>
		public string CodeMod4;
		///<summary>Revenue code for medical billing.  Not used yet.  Only used on UB92 claimforms.</summary>
		public string RevCode;
		/// <summary>Unit support for things like anesthesia billing and such.-dt</summary>
		public string UnitCode;
		///<summary>For certain CPT codes.  Default is now 1.  It was 0 in previous versions, causing all kinds of problems.</summary>
		public long UnitQty;
		///<summary>Base units used for some billing codes.  Default is 0.</summary>
		public long BaseUnits;
		///<summary>Start time in military</summary>
		public long StartTime;
		///<summary>Stop time in military</summary>
		public long StopTime;
		///<summary>The date that the procedure was originally treatment planned.  Does not change when marked complete.</summary>
		public DateTime DateTP;
		///<summary>FK to site.SiteNum.</summary>
		public long SiteNum;

		///<summary>Not a database column.  Saved in database in the procnote table.  This note is only the most recent note from that table.  If user changes it, then the business layer handles it by adding another procnote to that table.</summary>
		public string Note;
		///<summary>Not a database column.  Just used for now to set the user so that it can be saved with the ProcNote.</summary>
		public long UserNum;
		///<summary>Not a database column.  If viewing an individual procedure, then this will contain the encrypted signature.  If viewing a procedure list, this will typically just contain an "X" if a signature is present.  If user signs note, the signature will be encrypted before placing into this field.  Then it will be passed down and saved directly as is.</summary>
		public string Signature;
		///<summary>Not a database column.</summary>
		public bool SigIsTopaz;

		public Procedure(){
			UnitQty=1;
		}

		///<summary>Returns a copy of the procedure.</summary>
		public Procedure Copy() {
			return (Procedure)this.MemberwiseClone();
		}
	}

	


}
