using System;
using System.Collections;
using System.Xml.Serialization;


namespace OpenDentBusiness{
	///<summary>One row for each patient.  Includes deleted patients.</summary>
	[Serializable()]
	public class Patient : TableBase {
		/// <summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PatNum;
		/// <summary>Last name.</summary>
		public string LName;
		/// <summary>First name.</summary>
		public string FName;
		/// <summary>Middle initial or name.</summary>
		public string MiddleI;
		/// <summary>Preferred name, aka nickname.</summary>
		public string Preferred;
		/// <summary>Enum:PatientStatus</summary>
		public PatientStatus PatStatus;
		/// <summary>Enum:PatientGender</summary>
		public PatientGender Gender;
		/// <summary>Enum:PatientPosition Marital status would probably be a better name for this column.</summary>
		public PatientPosition Position;
		/// <summary>Age is not stored in the database.  Age is always calculated as needed from birthdate.</summary>
		public DateTime Birthdate;
		/// <summary>In the US, this is 9 digits, no dashes. For all other countries, any punctuation or format is allowed.</summary>
		public string SSN;
		/// <summary>.</summary>
		public string Address;
		/// <summary>Optional second address line.</summary>
		public string Address2;
		/// <summary>.</summary>
		public string City;
		/// <summary>2 Char in USA</summary>
		public string State;
		/// <summary>Postal code.  For Canadian claims, it must be ANANAN.  No validation gets done except there.</summary>
		public string Zip;
		/// <summary>Home phone. Includes any punctuation</summary>
		public string HmPhone;
		/// <summary>.</summary>
		public string WkPhone;
		/// <summary>.</summary>
		public string WirelessPhone;
		/// <summary>FK to patient.PatNum.  Head of household.</summary>
		public long Guarantor;
		/// <summary>Derived from Birthdate.  Not in the database table.</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public int Age;
		/// <summary>Single char. Shows at upper right corner of appointments.  Suggested use is A,B,or C to designate creditworthiness, but it can actually be used for any purpose.</summary>
		public string CreditType;
		/// <summary>.</summary>
		public string Email;
		/// <summary>Dear __.  This field does not include the "Dear" or a trailing comma.  If this field is blank, then the typical salutation is FName.  Or, if a Preferred name is present, that is used instead of FName.</summary>
		public string Salutation;
		/// <summary>Current patient balance.(not family). Never subtracts insurance estimates.</summary>
		public double EstBalance;
		/// <summary>FK to provider.ProvNum.  The patient's primary provider.  Required.  The database maintenance tool ensures that every patient always has this number set, so the program no longer has to handle 0.</summary>
		public long PriProv;
		/// <summary>FK to provider.ProvNum.  Secondary provider (hygienist). Optional.</summary>
		public long SecProv;
		/// <summary>FK to feesched.FeeSchedNum.  Fee schedule for this patient.  Usually not used.  If missing, the practice default fee schedule is used. If patient has insurance, then the fee schedule for the insplan is used.</summary>
		public long FeeSched;
		/// <summary>FK to definition.DefNum.  Must have a value, or the patient will not show on some reports.</summary>
		public long BillingType;
		/// <summary>Name of folder where images will be stored. Not editable for now.</summary>
		public string ImageFolder;
		/// <summary>Address or phone note.  Unlimited length in order to handle data from other programs during a conversion.</summary>
		public string AddrNote;
		/// <summary>Family financial urgent note.  Only stored with guarantor, and shared for family.</summary>
		public string FamFinUrgNote;
		/// <summary>Individual patient note for Urgent medical.</summary>
		public string MedUrgNote;
		/// <summary>Individual patient note for Appointment module note.</summary>
		public string ApptModNote;
		/// <summary>Single char for Nonstudent, Parttime, or Fulltime.  Blank also=Nonstudent</summary>
		public string StudentStatus;
		/// <summary>College name.  If Canadian, then this is field C10 and must be filled if C9 (patient.CanadianEligibilityCode) is 1 and patient is 18 or older.</summary>
		public string SchoolName;
		/// <summary>Max 15 char.  Used for reference to previous programs.</summary>
		public string ChartNumber;
		/// <summary>Optional. The Medicaid ID for this patient.</summary>
		public string MedicaidID;
		/// <summary>Aged balance from 0 to 30 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double Bal_0_30;
		/// <summary>Aged balance from 31 to 60 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double Bal_31_60;
		/// <summary>Aged balance from 61 to 90 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double Bal_61_90;
		/// <summary>Aged balance over 90 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double BalOver90;
		/// <summary>Insurance Estimate for entire family.</summary>
		public double InsEst;
		/// <summary>Total balance for entire family before insurance estimate.  Not the same as the sum of the 4 aging balances because this can be negative.  Only stored with guarantor.</summary>
		public double BalTotal;
		/// <summary>FK to employer.EmployerNum.</summary>
		public long EmployerNum;
		/// <summary>Not used since version 2.8.</summary>
		public string EmploymentNote;
		/// <summary>Enum:PatientRace Race and ethnicity.</summary>
		public PatientRace Race;
		/// <summary>FK to county.CountyName, although it will not crash if key absent.</summary>
		public string County;
		/// <summary>Enum:PatientGrade Gradelevel.</summary>
		public PatientGrade GradeLevel;
		/// <summary>Enum:TreatmentUrgency Used in public health screenings.</summary>
		public TreatmentUrgency Urgency;
		/// <summary>The date that the patient first visited the office.  Automated.</summary>
		public DateTime DateFirstVisit;
		/// <summary>FK to clinic.ClinicNum. Can be zero if not attached to a clinic or no clinics set up.</summary>
		public long ClinicNum;
		/// <summary>For now, an 'I' indicates that the patient has insurance.  This is only used when displaying appointments.  It will later be expanded.  User can't edit.</summary>
		public string HasIns;
		/// <summary>The Trophy bridge is inadequate.  This is an attempt to make it usable for offices that have already invested in Trophy hardware.</summary>
		public string TrophyFolder;
		/// <summary>This simply indicates whether the 'done' box is checked in the chart module.  Used to be handled as a -1 in the NextAptNum field, but now that field is unsigned.</summary>
		public bool PlannedIsDone;
		/// <summary>Set to true if patient needs to be premedicated for appointments, includes PAC, halcion, etc.</summary>
		public bool Premed;
		/// <summary>Only used in hospitals.</summary>
		public string Ward;
		/// <summary>Enum:ContactMethod</summary>
		public ContactMethod PreferConfirmMethod;
		/// <summary>Enum:ContactMethod</summary>
		public ContactMethod PreferContactMethod;
		/// <summary>Enum:ContactMethod</summary>
		public ContactMethod PreferRecallMethod;
		/// <summary></summary>
		[XmlIgnore]
		public TimeSpan SchedBeforeTime;
		/// <summary></summary>
		[XmlIgnore]
		public TimeSpan SchedAfterTime;
		/// <summary>We do not use this, but some users do, so here it is. 0=none. Otherwise, 1-7 for day.</summary>
		public byte SchedDayOfWeek;
		/// <summary>The primary language of the patient.  Typically en, fr, es, or similar.  We might later also allow cultures and user-defined languages.</summary>
		public string Language;
		/// <summary>Used in hospitals.  It can be before the first visit date.  It typically gets set automatically by the hospital system.</summary>
		public DateTime AdmitDate;
		/// <summary>Includes any punctuation.  For example, Mr., Mrs., Miss, Dr., etc.  There is no selection mechanism yet for user; they must simply type it in.</summary>
		public string Title;
		/// <summary></summary>
		public double PayPlanDue;
		///<summary>FK to site.SiteNum. Can be zero. Replaces the old GradeSchool field with a proper foreign key.</summary>
		public long SiteNum;
		///<summary>The last date and time this row was altered.  Not user editable.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;
		///<summary>FK to patient.PatNum. Can be zero.  Person responsible for medical decisions rather than finances.  Guarantor is still responsible for finances.  This is useful for nursing home residents.  Part of public health.</summary>
		public long ResponsParty;
		///<summary>C09.  Eligibility Exception Code.  A single digit 1-4.  0 is not acceptable for e-claims. 1=FT student, 2=disabled, 3=disabled student, 4=code not applicable.  Warning.  4 is a 0 if using CDAnet version 02.</summary>
		public byte CanadianEligibilityCode;
		///<summary>Number of minutes patient is asked to come early to appointments.</summary>
		public int AskToArriveEarly;
		///<summary>If this is blank, then the chart info for this patient will not be uploaded.  If this has a value, then this is the password that a patient must use to access their info online.</summary>
		public string OnlinePassword;
		///<summary>Enum:SmokingStatus  0=UnknownIfEver,1=SmokerUnkownCurrent,2=NeverSmoked,3=FormerSmoker,4=CurrentSomeDay,5=CurrentEveryDay</summary>
		public SmokingStatus SmokeStatus;


		//<summary>Decided not to add since this data is already available and synchronizing would take too much time.  Will add later.  Not editable. If the patient happens to have a future appointment, this will contain the date of that appointment.  Once appointment is set complete, this date is deleted.  If there is more than one appointment scheduled, this will only contain the earliest one.  Used mostly to exclude patients from recall lists.  If you want all future appointments, use Appointments.GetForPat() instead. You can loop through that list and exclude appointments with dates earlier than today.</summary>
		//public DateTime DateScheduled;

		///<summary>Used only for serialization purposes</summary>
		[XmlElement("SchedBeforeTime",typeof(long))]
		public long SchedBeforeTimeXml {
			get {
				return SchedBeforeTime.Ticks;
			}
			set {
				SchedBeforeTime = TimeSpan.FromTicks(value);
			}
		}

		///<summary>Used only for serialization purposes</summary>
		[XmlElement("SchedAfterTime",typeof(long))]
		public long SchedAfterTimeXml {
			get {
				return SchedAfterTime.Ticks;
			}
			set {
				SchedAfterTime = TimeSpan.FromTicks(value);
			}
		}

		///<summary>Returns a copy of this Patient.</summary>
		public Patient Copy(){
			return (Patient)this.MemberwiseClone();
		}

		public override string ToString() {
			return "Patient: "+GetNameLF();
		}

		///<summary>LName, 'Preferred' FName M</summary>
		public string GetNameLF(){
			return Patients.GetNameLF(LName,FName,Preferred,MiddleI);
		}

		///<summary>FName 'Preferred' M LName</summary>
		public string GetNameFL(){
			return Patients.GetNameFL(LName,FName,Preferred,MiddleI);
		}

		///<summary>FName M LName</summary>
		public string GetNameFLnoPref() {
			return Patients.GetNameFLnoPref(LName,FName,MiddleI);
		}

		///<summary>FName/Preferred LName</summary>
		public string GetNameFirstOrPrefL(){
			return Patients.GetNameFirstOrPrefL(LName,FName,Preferred);
		}

		///<summary>FName/Preferred M. LName</summary>
		public string GetNameFirstOrPrefML(){
			return Patients.GetNameFirstOrPrefML(LName,FName,Preferred,MiddleI);
		}

		///<summary>Title FName M LName</summary>
		public string GetNameFLFormal() {
			return Patients.GetNameFLFormal(LName,FName,MiddleI,Title);
		}

		///<summary>Includes preferred.</summary>
		public string GetNameFirst() {
			return Patients.GetNameFirst(FName,Preferred);
		}

		///<summary></summary>
		public string GetNameFirstOrPreferred() {
			return Patients.GetNameFirstOrPreferred(FName,Preferred);
		}

		///<summary>Dear __.  Does not include the "Dear" or the comma.</summary>
		public string GetSalutation(){
			return Patients.GetSalutation(Salutation,Preferred,FName);
		}
	}

}










