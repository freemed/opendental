using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	
	///<summary>One row for each patient.  Includes deleted patients.</summary>
	[DataObject("patient")]
	public class Patient : DataObjectBase {
		///<summary>Obsolete.  See the toothinitial table.</summary>
		private string PrimaryTeeth;

		[DataField("PatNum", PrimaryKey=true, AutoNumber=true)]
		private int patNum;
		bool patNumChanged;
		/// <summary>Primary key.</summary>
		public int PatNum {
			get { return patNum; }
			set { patNum = value; MarkDirty(); patNumChanged = true; }
		}
		public bool PatNumChanged {
			get { return patNumChanged; }
		}

		[DataField("LName")]
		private string lName;
		bool lNameChanged;
		/// <summary>Last name.</summary>
		public string LName {
			get { return lName; }
			set { lName = value; MarkDirty(); lNameChanged = true; }
		}
		public bool LNameChanged {
			get { return lNameChanged; }
		}

		[DataField("FName")]
		private string fName;
		bool fNameChanged;
		/// <summary>First name.</summary>
		public string FName {
			get { return fName; }
			set { fName = value; MarkDirty(); fNameChanged = true; }
		}
		public bool FNameChanged {
			get { return fNameChanged; }
		}

		[DataField("MiddleI")]
		private string middleI;
		bool middleIChanged;
		/// <summary>Middle initial or name.</summary>
		public string MiddleI {
			get { return middleI; }
			set { middleI = value; MarkDirty(); middleIChanged = true; }
		}
		public bool MiddleIChanged {
			get { return middleIChanged; }
		}

		[DataField("Preferred")]
		private string preferred;
		bool preferredChanged;
		/// <summary>Preferred name, aka nickname.</summary>
		public string Preferred {
			get { return preferred; }
			set { preferred = value; MarkDirty(); preferredChanged = true; }
		}
		public bool PreferredChanged {
			get { return preferredChanged; }
		}

		[DataField("PatStatus")]
		private PatientStatus patStatus;
		bool patStatusChanged;
		/// <summary>Enum:PatientStatus</summary>
		public PatientStatus PatStatus {
			get { return patStatus; }
			set { patStatus = value; MarkDirty(); patStatusChanged = true; }
		}
		public bool PatStatusChanged {
			get { return patStatusChanged; }
		}

		[DataField("Gender")]
		private PatientGender gender;
		bool genderChanged;
		/// <summary>Enum:PatientGender</summary>
		public PatientGender Gender {
			get { return gender; }
			set { gender = value; MarkDirty(); genderChanged = true; }
		}
		public bool GenderChanged {
			get { return genderChanged; }
		}

		[DataField("Position")]
		private PatientPosition position;
		bool positionChanged;
		/// <summary>Enum:PatientPosition Marital status would probably be a better name for this column.</summary>
		public PatientPosition Position {
			get { return position; }
			set { position = value; MarkDirty(); positionChanged = true; }
		}
		public bool PositionChanged {
			get { return positionChanged; }
		}

		[DataField("Birthdate")]
		private DateTime birthdate;
		bool birthdateChanged;
		/// <summary>Age is not stored in the database.  Age is always calculated as needed from birthdate.</summary>
		public DateTime Birthdate {
			get { return birthdate; }
			set { birthdate = value; MarkDirty(); birthdateChanged = true; }
		}
		public bool BirthdateChanged {
			get { return birthdateChanged; }
		}

		[DataField("SSN")]
		private string sSN;
		bool sSNChanged;
		/// <summary>In the US, this is 9 digits, no dashes. For all other countries, any punctuation or format is allowed.</summary>
		public string SSN {
			get { return sSN; }
			set { sSN = value; MarkDirty(); sSNChanged = true; }
		}
		public bool SSNChanged {
			get { return sSNChanged; }
		}

		[DataField("Address")]
		private string address;
		bool addressChanged;
		/// <summary>.</summary>
		public string Address {
			get { return address; }
			set { address = value; MarkDirty(); addressChanged = true; }
		}
		public bool AddressChanged {
			get { return addressChanged; }
		}

		[DataField("Address2")]
		private string address2;
		bool address2Changed;
		/// <summary>.</summary>
		public string Address2 {
			get { return address2; }
			set { address2 = value; MarkDirty(); address2Changed = true; }
		}
		public bool Address2Changed {
			get { return address2Changed; }
		}

		[DataField("City")]
		private string city;
		bool cityChanged;
		/// <summary>.</summary>
		public string City {
			get { return city; }
			set { city = value; MarkDirty(); cityChanged = true; }
		}
		public bool CityChanged {
			get { return cityChanged; }
		}

		[DataField("State")]
		private string state;
		bool stateChanged;
		/// <summary>2 Char in USA</summary>
		public string State {
			get { return state; }
			set { state = value; MarkDirty(); stateChanged = true; }
		}
		public bool StateChanged {
			get { return stateChanged; }
		}

		[DataField("Zip")]
		private string zip;
		bool zipChanged;
		/// <summary>Postal code.  For Canadian claims, it must be ANANAN.  No validation gets done except there.</summary>
		public string Zip {
			get { return zip; }
			set { zip = value; MarkDirty(); zipChanged = true; }
		}
		public bool ZipChanged {
			get { return zipChanged; }
		}

		[DataField("HmPhone")]
		private string hmPhone;
		bool hmPhoneChanged;
		/// <summary>Home phone. Includes any punctuation</summary>
		public string HmPhone {
			get { return hmPhone; }
			set { hmPhone = value; MarkDirty(); hmPhoneChanged = true; }
		}
		public bool HmPhoneChanged {
			get { return hmPhoneChanged; }
		}

		[DataField("WkPhone")]
		private string wkPhone;
		bool wkPhoneChanged;
		/// <summary>.</summary>
		public string WkPhone {
			get { return wkPhone; }
			set { wkPhone = value; MarkDirty(); wkPhoneChanged = true; }
		}
		public bool WkPhoneChanged {
			get { return wkPhoneChanged; }
		}

		[DataField("WirelessPhone")]
		private string wirelessPhone;
		bool wirelessPhoneChanged;
		/// <summary>.</summary>
		public string WirelessPhone {
			get { return wirelessPhone; }
			set { wirelessPhone = value; MarkDirty(); wirelessPhoneChanged = true; }
		}
		public bool WirelessPhoneChanged {
			get { return wirelessPhoneChanged; }
		}

		[DataField("Guarantor")]
		private int guarantor;
		bool guarantorChanged;
		/// <summary>FK to patient.PatNum.  Head of household.</summary>
		public int Guarantor {
			get { return guarantor; }
			set { guarantor = value; MarkDirty(); guarantorChanged = true; }
		}
		public bool GuarantorChanged {
			get { return guarantorChanged; }
		}

		private int age;
		/// <summary>Derived from Birthdate.  Not in the database table.</summary>
		public int Age {
			get { return age; }
			set { age = value; }
		}

		[DataField("CreditType")]
		private string creditType;
		bool creditTypeChanged;
		/// <summary>Single char. Shows at upper left corner of appointments.  Suggested use is A,B,or C to designate creditworthiness, but it can actually be used for any purpose.</summary>
		public string CreditType {
			get { return creditType; }
			set { creditType = value; MarkDirty(); creditTypeChanged = true; }
		}
		public bool CreditTypeChanged {
			get { return creditTypeChanged; }
		}

		[DataField("Email")]
		private string email;
		bool emailChanged;
		/// <summary>.</summary>
		public string Email {
			get { return email; }
			set { email = value; MarkDirty(); emailChanged = true; }
		}
		public bool EmailChanged {
			get { return emailChanged; }
		}

		[DataField("Salutation")]
		private string salutation;
		bool salutationChanged;
		/// <summary>For example: Dear Mr. Smith.  Not used by the program in any way.</summary>
		public string Salutation {
			get { return salutation; }
			set { salutation = value; MarkDirty(); salutationChanged = true; }
		}
		public bool SalutationChanged {
			get { return salutationChanged; }
		}

		[DataField("EstBalance")]
		private double estBalance;
		bool estBalanceChanged;
		/// <summary>Current patient balance.(not family). If user has checked BalancesDontSubtractIns in setup, then this will not take into account insurance.  Otherwise, the insurance estimate pending will have already been subtracted.</summary>
		public double EstBalance {
			get { return estBalance; }
			set { estBalance = value; MarkDirty(); estBalanceChanged = true; }
		}
		public bool EstBalanceChanged {
			get { return estBalanceChanged; }
		}

		[DataField("NextAptNum")]
		private int nextAptNum;
		bool nextAptNumChanged;
		/// <summary>May be 0. Also see the PlannedIsDone field. Otherwise it is the foreign key to appointment.AptNum.  This is the appointment that will show in the Chart module and in the Planned appointment tracker.  It will never show in the Appointments module. In other words, it is the suggested next appoinment rather than an appointment that has already been scheduled.</summary>
		public int NextAptNum {
			get { return nextAptNum; }
			set { nextAptNum = value; MarkDirty(); nextAptNumChanged = true; }
		}
		public bool NextAptNumChanged {
			get { return nextAptNumChanged; }
		}

		[DataField("PriProv")]
		private int priProv;
		bool priProvChanged;
		/// <summary>FK to provider.ProvNum.  The patient's primary provider.  Required.  The database maintenance tool ensures that every patient always has this number set, so the program no longer has to handle 0.</summary>
		public int PriProv {
			get { return priProv; }
			set { priProv = value; MarkDirty(); priProvChanged = true; }
		}
		public bool PriProvChanged {
			get { return priProvChanged; }
		}

		[DataField("SecProv")]
		private int secProv;
		bool secProvChanged;
		/// <summary>FK to provider.ProvNum.  Secondary provider (hygienist). Optional.</summary>
		public int SecProv {
			get { return secProv; }
			set { secProv = value; MarkDirty(); secProvChanged = true; }
		}
		public bool SecProvChanged {
			get { return secProvChanged; }
		}
//
		[DataField("FeeSched")]
		private int feeSched;
		bool feeSchedChanged;
		/// <summary>FK to definition.DefNum.  Fee schedule for this patient.  Usually not used.  If missing, the practice default fee schedule is used. If patient has insurance, then the fee schedule for the insplan is used.</summary>
		public int FeeSched {
			get { return feeSched; }
			set { feeSched = value; MarkDirty(); feeSchedChanged = true; }
		}
		public bool FeeSchedChanged {
			get { return feeSchedChanged; }
		}

		[DataField("BillingType")]
		private int billingType;
		bool billingTypeChanged;
		/// <summary>FK to definition.DefNum.  Must have a value, or the patient will not show on some reports.</summary>
		public int BillingType {
			get { return billingType; }
			set { billingType = value; MarkDirty(); billingTypeChanged = true; }
		}
		public bool BillingTypeChanged {
			get { return billingTypeChanged; }
		}

		[DataField("ImageFolder")]
		private string imageFolder;
		bool imageFolderChanged;
		/// <summary>Name of folder where images will be stored. Not editable for now.</summary>
		public string ImageFolder {
			get { return imageFolder; }
			set { imageFolder = value; MarkDirty(); imageFolderChanged = true; }
		}
		public bool ImageFolderChanged {
			get { return imageFolderChanged; }
		}

		[DataField("AddrNote")]
		private string addrNote;
		bool addrNoteChanged;
		/// <summary>Address or phone note.  Unlimited length in order to handle data from other programs during a conversion.</summary>
		public string AddrNote {
			get { return addrNote; }
			set { addrNote = value; MarkDirty(); addrNoteChanged = true; }
		}
		public bool AddrNoteChanged {
			get { return addrNoteChanged; }
		}

		[DataField("FamFinUrgNote")]
		private string famFinUrgNote;
		bool famFinUrgNoteChanged;
		/// <summary>Family financial urgent note.  Only stored with guarantor, and shared for family.</summary>
		public string FamFinUrgNote {
			get { return famFinUrgNote; }
			set { famFinUrgNote = value; MarkDirty(); famFinUrgNoteChanged = true; }
		}
		public bool FamFinUrgNoteChanged {
			get { return famFinUrgNoteChanged; }
		}

		[DataField("MedUrgNote")]
		private string medUrgNote;
		bool medUrgNoteChanged;
		/// <summary>Individual patient note for Urgent medical.</summary>
		public string MedUrgNote {
			get { return medUrgNote; }
			set { medUrgNote = value; MarkDirty(); medUrgNoteChanged = true; }
		}
		public bool MedUrgNoteChanged {
			get { return medUrgNoteChanged; }
		}

		[DataField("ApptModNote")]
		private string apptModNote;
		bool apptModNoteChanged;
		/// <summary>Individual patient note for Appointment module note.</summary>
		public string ApptModNote {
			get { return apptModNote; }
			set { apptModNote = value; MarkDirty(); apptModNoteChanged = true; }
		}
		public bool ApptModNoteChanged {
			get { return apptModNoteChanged; }
		}

		[DataField("StudentStatus")]
		private string studentStatus;
		bool studentStatusChanged;
		/// <summary>Single char for Nonstudent, Parttime, or Fulltime.  Blank=Nonstudent</summary>
		public string StudentStatus {
			get { return studentStatus; }
			set { studentStatus = value; MarkDirty(); studentStatusChanged = true; }
		}
		public bool StudentStatusChanged {
			get { return studentStatusChanged; }
		}

		[DataField("SchoolName")]
		private string schoolName;
		bool schoolNameChanged;
		/// <summary>College name.</summary>
		public string SchoolName {
			get { return schoolName; }
			set { schoolName = value; MarkDirty(); schoolNameChanged = true; }
		}
		public bool SchoolNameChanged {
			get { return schoolNameChanged; }
		}

		[DataField("ChartNumber")]
		private string chartNumber;
		bool chartNumberChanged;
		/// <summary>Max 15 char.  Used for reference to previous programs.</summary>
		public string ChartNumber {
			get { return chartNumber; }
			set { chartNumber = value; MarkDirty(); chartNumberChanged = true; }
		}
		public bool ChartNumberChanged {
			get { return chartNumberChanged; }
		}

		[DataField("MedicaidID")]
		private string medicaidID;
		bool medicaidIDChanged;
		/// <summary>Optional. The Medicaid ID for this patient.</summary>
		public string MedicaidID {
			get { return medicaidID; }
			set { medicaidID = value; MarkDirty(); medicaidIDChanged = true; }
		}
		public bool MedicaidIDChanged {
			get { return medicaidIDChanged; }
		}

		[DataField("Bal_0_30")]
		private double bal_0_30;
		bool bal_0_30Changed;
		/// <summary>Aged balance from 0 to 30 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double Bal_0_30 {
			get { return bal_0_30; }
			set { bal_0_30 = value; MarkDirty(); bal_0_30Changed = true; }
		}
		public bool Bal_0_30Changed {
			get { return bal_0_30Changed; }
		}

		[DataField("Bal_31_60")]
		private double bal_31_60;
		bool bal_31_60Changed;
		/// <summary>Aged balance from 31 to 60 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double Bal_31_60 {
			get { return bal_31_60; }
			set { bal_31_60 = value; MarkDirty(); bal_31_60Changed = true; }
		}
		public bool Bal_31_60Changed {
			get { return bal_31_60Changed; }
		}

		[DataField("Bal_61_90")]
		private double bal_61_90;
		bool bal_61_90Changed;
		/// <summary>Aged balance from 61 to 90 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double Bal_61_90 {
			get { return bal_61_90; }
			set { bal_61_90 = value; MarkDirty(); bal_61_90Changed = true; }
		}
		public bool Bal_61_90Changed {
			get { return bal_61_90Changed; }
		}

		[DataField("BalOver90")]
		private double balOver90;
		bool balOver90Changed;
		/// <summary>Aged balance over 90 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double BalOver90 {
			get { return balOver90; }
			set { balOver90 = value; MarkDirty(); balOver90Changed = true; }
		}
		public bool BalOver90Changed {
			get { return balOver90Changed; }
		}

		[DataField("InsEst")]
		private double insEst;
		bool insEstChanged;
		/// <summary>Insurance Estimate for entire family.</summary>
		public double InsEst {
			get { return insEst; }
			set { insEst = value; MarkDirty(); insEstChanged = true; }
		}
		public bool InsEstChanged {
			get { return insEstChanged; }
		}

		[Obsolete("No longer used.  See toothinital table instead.")]
		private string primaryTeethOld;
		/// <summary>No longer used.  See toothinital table instead.</summary>
		public string PrimaryTeethOld {
			get { return primaryTeethOld; }
			set { primaryTeethOld = value; }
		}

		[DataField("BalTotal")]
		private double balTotal;
		bool balTotalChanged;
		/// <summary>Total balance for entire family before insurance estimate.  Not the same as the sum of the 4 aging balances because this can be negative.  Only stored with guarantor.</summary>
		public double BalTotal {
			get { return balTotal; }
			set { balTotal = value; MarkDirty(); balTotalChanged = true; }
		}
		public bool BalTotalChanged {
			get { return balTotalChanged; }
		}

		[DataField("EmployerNum")]
		private int employerNum;
		bool employerNumChanged;
		/// <summary>FK to employer.EmployerNum.</summary>
		public int EmployerNum {
			get { return employerNum; }
			set { employerNum = value; MarkDirty(); employerNumChanged = true; }
		}
		public bool EmployerNumChanged {
			get { return employerNumChanged; }
		}

		[DataField("EmploymentNote")]
		private string employmentNote;
		bool employmentNoteChanged;
		/// <summary>Not used since version 2.8.</summary>
		public string EmploymentNote {
			get { return employmentNote; }
			set { employmentNote = value; MarkDirty(); employmentNoteChanged = true; }
		}
		public bool EmploymentNoteChanged {
			get { return employmentNoteChanged; }
		}

		[DataField("Race")]
		private PatientRace race;
		bool raceChanged;
		/// <summary>Enum:PatientRace Race and ethnicity.</summary>
		public PatientRace Race {
			get { return race; }
			set { race = value; MarkDirty(); raceChanged = true; }
		}
		public bool RaceChanged {
			get { return raceChanged; }
		}

		[DataField("County")]
		private string county;
		bool countyChanged;
		/// <summary>FK to county.CountyName, although it will not crash if key absent.</summary>
		public string County {
			get { return county; }
			set { county = value; MarkDirty(); countyChanged = true; }
		}
		public bool CountyChanged {
			get { return countyChanged; }
		}

		[DataField("GradeSchool")]
		private string gradeSchool;
		bool gradeSchoolChanged;
		/// <summary>FK to school.SchoolName, although it will not crash if key absent.  Name of gradeschool or highschool.</summary>
		public string GradeSchool {
			get { return gradeSchool; }
			set { gradeSchool = value; MarkDirty(); gradeSchoolChanged = true; }
		}
		public bool GradeSchoolChanged {
			get { return gradeSchoolChanged; }
		}

		[DataField("GradeLevel")]
		private PatientGrade gradeLevel;
		bool gradeLevelChanged;
		/// <summary>Enum:PatientGrade Gradelevel.</summary>
		public PatientGrade GradeLevel {
			get { return gradeLevel; }
			set { gradeLevel = value; MarkDirty(); gradeLevelChanged = true; }
		}
		public bool GradeLevelChanged {
			get { return gradeLevelChanged; }
		}

		[DataField("Urgency")]
		private TreatmentUrgency urgency;
		bool urgencyChanged;
		/// <summary>Enum:TreatmentUrgency Used in public health screenings.</summary>
		public TreatmentUrgency Urgency {
			get { return urgency; }
			set { urgency = value; MarkDirty(); urgencyChanged = true; }
		}
		public bool UrgencyChanged {
			get { return urgencyChanged; }
		}

		[DataField("DateFirstVisit")]
		private DateTime dateFirstVisit;
		bool dateFirstVisitChanged;
		/// <summary>The date that the patient first visited the office.  Automated.</summary>
		public DateTime DateFirstVisit {
			get { return dateFirstVisit; }
			set { dateFirstVisit = value; MarkDirty(); dateFirstVisitChanged = true; }
		}
		public bool DateFirstVisitChanged {
			get { return dateFirstVisitChanged; }
		}

		[DataField("ClinicNum")]
		private int clinicNum;
		bool clinicNumChanged;
		/// <summary>FK to clinic.ClinicNum. Can be zero if not attached to a clinic or no clinics set up.</summary>
		public int ClinicNum {
			get { return clinicNum; }
			set { clinicNum = value; MarkDirty(); clinicNumChanged = true; }
		}
		public bool ClinicNumChanged {
			get { return clinicNumChanged; }
		}

		[DataField("HasIns")]
		private string hasIns;
		bool hasInsChanged;
		/// <summary>For now, an 'I' indicates that the patient has insurance.  This is only used when displaying appointments.  It will later be expanded.  User can't edit.</summary>
		public string HasIns {
			get { return hasIns; }
			set { hasIns = value; MarkDirty(); hasInsChanged = true; }
		}
		public bool HasInsChanged {
			get { return hasInsChanged; }
		}

		[DataField("TrophyFolder")]
		private string trophyFolder;
		bool trophyFolderChanged;
		/// <summary>The Trophy bridge is inadequate.  This is an attempt to make it usable for offices that have already invested in Trophy hardware.</summary>
		public string TrophyFolder {
			get { return trophyFolder; }
			set { trophyFolder = value; MarkDirty(); trophyFolderChanged = true; }
		}
		public bool TrophyFolderChanged {
			get { return trophyFolderChanged; }
		}

		[DataField("PlannedIsDone")]
		private bool plannedIsDone;
		bool plannedIsDoneChanged;
		/// <summary>This simply indicates whether the 'done' box is checked in the chart module.  Used to be handled as a -1 in the NextAptNum field, but now that field is unsigned.</summary>
		public bool PlannedIsDone {
			get { return plannedIsDone; }
			set { plannedIsDone = value; MarkDirty(); plannedIsDoneChanged = true; }
		}
		public bool PlannedIsDoneChanged {
			get { return plannedIsDoneChanged; }
		}

		[DataField("Premed")]
		private bool premed;
		bool premedChanged;
		/// <summary>Set to true if patient needs to be premedicated for appointments, includes PAC, halcion, etc.</summary>
		public bool Premed {
			get { return premed; }
			set { premed = value; MarkDirty(); premedChanged = true; }
		}
		public bool PremedChanged {
			get { return premedChanged; }
		}

		[DataField("Ward")]
		private string ward;
		bool wardChanged;
		/// <summary>Only used in hospitals.</summary>
		public string Ward {
			get { return ward; }
			set { ward = value; MarkDirty(); wardChanged = true; }
		}
		public bool WardChanged {
			get { return wardChanged; }
		}

		[DataField("PreferConfirmMethod")]
		private ContactMethod preferConfirmMethod;
		bool preferConfirmMethodChanged;
		/// <summary>Enum:ContactMethod</summary>
		public ContactMethod PreferConfirmMethod {
			get { return preferConfirmMethod; }
			set { preferConfirmMethod = value; MarkDirty(); preferConfirmMethodChanged = true; }
		}
		public bool PreferConfirmMethodChanged {
			get { return preferConfirmMethodChanged; }
		}

		[DataField("PreferContactMethod")]
		private ContactMethod preferContactMethod;
		bool preferContactMethodChanged;
		/// <summary>Enum:ContactMethod</summary>
		public ContactMethod PreferContactMethod {
			get { return preferContactMethod; }
			set { preferContactMethod = value; MarkDirty(); preferContactMethodChanged = true; }
		}
		public bool PreferContactMethodChanged {
			get { return preferContactMethodChanged; }
		}

		[DataField("PreferRecallMethod")]
		private ContactMethod preferRecallMethod;
		bool preferRecallMethodChanged;
		/// <summary>Enum:ContactMethod</summary>
		public ContactMethod PreferRecallMethod {
			get { return preferRecallMethod; }
			set { preferRecallMethod = value; MarkDirty(); preferRecallMethodChanged = true; }
		}
		public bool PreferRecallMethodChanged {
			get { return preferRecallMethodChanged; }
		}

		[DataField("SchedBeforeTime")]
		private TimeSpan schedBeforeTime;
		bool schedBeforeTimeChanged;
		/// <summary>Only the time portion of the TimeSpan is used.</summary>
		public TimeSpan SchedBeforeTime {
			get { return schedBeforeTime; }
			set { schedBeforeTime = value; MarkDirty(); schedBeforeTimeChanged = true; }
		}
		public bool SchedBeforeTimeChanged {
			get { return schedBeforeTimeChanged; }
		}

		[DataField("SchedAfterTime")]
		private TimeSpan schedAfterTime;
		bool schedAfterTimeChanged;
		/// <summary></summary>
		public TimeSpan SchedAfterTime {
			get { return schedAfterTime; }
			set { schedAfterTime = value; MarkDirty(); schedAfterTimeChanged = true; }
		}
		public bool SchedAfterTimeChanged {
			get { return schedAfterTimeChanged; }
		}

		[DataField("SchedDayOfWeek")]
		private byte schedDayOfWeek;
		bool schedDayOfWeekChanged;
		/// <summary>We do not use this, but some users do, so here it is. 0=none. Otherwise, 1-7 for day.</summary>
		public byte SchedDayOfWeek {
			get { return schedDayOfWeek; }
			set { schedDayOfWeek = value; MarkDirty(); schedDayOfWeekChanged = true; }
		}
		public bool SchedDayOfWeekChanged {
			get { return schedDayOfWeekChanged; }
		}

		[DataField("Language")]
		private string language;
		bool languageChanged;
		/// <summary>The primary language of the patient.  Typically en, fr, es, or similar.  We might later also allow cultures and user-defined languages.</summary>
		public string Language {
			get { return language; }
			set { language = value; MarkDirty(); languageChanged = true; }
		}
		public bool LanguageChanged {
			get { return languageChanged; }
		}

		[DataField("AdmitDate")]
		private DateTime admitDate;
		bool admitDateChanged;
		/// <summary>Used in hospitals.  It can be before the first visit date.  It typically gets set automatically by the hospital system.</summary>
		public DateTime AdmitDate {
			get { return admitDate; }
			set { admitDate = value; MarkDirty(); admitDateChanged = true; }
		}
		public bool AdmitDateChanged {
			get { return admitDateChanged; }
		}

		[DataField("Title")]
		private string title;
		bool titleChanged;
		/// <summary>Includes any punctuation.  For example, Mr., Mrs., Miss, Dr., etc.  There is no selection mechanism yet for user; they must simply type it in.</summary>
		public string Title {
			get { return title; }
			set { title = value; MarkDirty(); titleChanged = true; }
		}
		public bool TitleChanged {
			get { return titleChanged; }
		}


		//<summary>Decided not to add since this data is already available and synchronizing would take too much time.  Will add later.  Not editable. If the patient happens to have a future appointment, this will contain the date of that appointment.  Once appointment is set complete, this date is deleted.  If there is more than one appointment scheduled, this will only contain the earliest one.  Used mostly to exclude patients from recall lists.  If you want all future appointments, use Appointments.GetForPat() instead. You can loop through that list and exclude appointments with dates earlier than today.</summary>
		//public DateTime DateScheduled;

		///<summary>Returns a copy of this Patient.</summary>
		public Patient Copy(){
			return (Patient)this.MemberwiseClone();
		}

		///<summary>Returns a formatted name, Last, First.</summary>
		public string GetNameLF(){
			string retVal="";
			if(Title!=""){
			//	retVal+=Title+" ";
			}
			retVal+=LName+", ";
			if(Preferred!=""){
				retVal+="'"+Preferred+"' ";
			}
			retVal+=FName;
			if(MiddleI!=""){
				retVal+=" "+MiddleI;
			}
			return retVal;
		}

		///<summary></summary>
		public string GetNameFL(){
			string retVal="";
			if(Title!="") {
				//retVal+=Title+" ";
			}
			retVal+=FName+" ";
			if(Preferred!="") {
				retVal+="'"+Preferred+"' ";
			}
			if(MiddleI!=""){
				retVal+=MiddleI+" ";
			}
			retVal+=LName;
			return retVal;
		}

		///<summary></summary>
		public string GetNameFLFormal() {
			string retVal="";
			if(Title!="") {
				retVal+=Title+" ";
			}
			retVal+=FName+" "+MiddleI+" "+LName;
			return retVal;
		}
	}

}










