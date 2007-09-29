using System;

namespace OpenDentBusiness{
	///<summary>This is an enumeration of all the enumeration types that are used in the database.  This is used in the reporting classes to make the data human-readable.  May need to be updated with recent additions.</summary>
	public enum EnumType{
		///<summary></summary>
		YN,
		///<summary></summary>
		Relat,
		///<summary></summary>
		Month,
		///<summary></summary>
		ProcStat,
		///<summary></summary>
		DefCat,
		///<summary></summary>
		TreatmentArea,
		///<summary></summary>
		DentalSpecialty,
		///<summary></summary>
		ApptStatus,
		///<summary></summary>
		PatientStatus,
		///<summary></summary>
		PatientGender,
		///<summary></summary>
		PatientPosition,
		///<summary></summary>
		ScheduleType,
		///<summary></summary>
		LabCase,
		///<summary></summary>
		PlaceOfService,
		///<summary></summary>
		PaintType,
		///<summary></summary>
		SchedStatus,
		///<summary></summary>
		AutoCondition,
		///<summary></summary>
		ClaimProcStatus,
		///<summary></summary>
		CommItemType,
		///<summary></summary>
		ToolBarsAvail
	}
	///<summary>Unknown,Yes, or No.</summary>
	public enum YN{
		///<summary>0</summary>
		Unknown,
		///<summary>1</summary>
		Yes,
		///<summary>2</summary>
		No}
	///<summary>Relationship to subscriber for insurance.</summary>
	public enum Relat{
		///<summary>0</summary>
		Self,
		///<summary>1</summary>
		Spouse,
		///<summary>2</summary>
		Child,
		///<summary>3</summary>
		Employee,
		///<summary>4</summary>
		HandicapDep,
		///<summary>5</summary>
		SignifOther,
		///<summary>6</summary>
		InjuredPlaintiff,
		///<summary>7</summary>
		LifePartner,
		///<summary>8</summary>
		Dependent}
	///<summary></summary>
	public enum Month{
		///<summary>1</summary>
		Jan=1,
		///<summary>2</summary>
		Feb,
		///<summary>3</summary>
		Mar,
		///<summary>4</summary>
		Apr,
		///<summary>5</summary>
		May,
		///<summary>6</summary>
		Jun,
		///<summary>7</summary>
		Jul,
		///<summary>8</summary>
		Aug,
		///<summary>9</summary>
		Sep,
		///<summary>10</summary>
		Oct,
		///<summary>11</summary>
		Nov,
		///<summary>12</summary>
		Dec}
	///<summary>Progress notes line type. Used when displaying lines in the Chart module.</summary>
	public enum ProgType{
		///<summary>1</summary>
		Proc=1,
		///<summary>2</summary>
		Rx}
	///<summary>Primary, secondary, or total. Used in some insurance estimates to specify which kind of estimate is needed.</summary>
	public enum PriSecTot{
		///<summary>0</summary>
		Pri,
		///<summary>1</summary>
		Sec,
		///<summary>2</summary>
		Tot}
		//<summary>3</summary>
		//Other}
	///<summary>Procedure Status.</summary>
	public enum ProcStat{
		///<summary>1- Treatment Plan.</summary>
		TP=1,
		///<summary>2- Complete.</summary>
		C,
		///<summary>3- Existing Current Provider.</summary>
		EC,
		///<summary>4- Existing Other Provider.</summary>
		EO,
		///<summary>5- Referred Out.</summary>
		R,
		///<summary>6- Deleted.</summary>
		D
	}
		//?new?
	///<summary>Definition Category. Go to the definition setup window in the program to see how each of these categories is used.</summary>
	public enum DefCat{
		///<summary>0- Colors to display in Account module.</summary>
		AccountColors,
		///<summary>1- Adjustment types.</summary>
		AdjTypes,
		///<summary>2- Appointment confirmed types.</summary>
		ApptConfirmed,
		///<summary>3- Procedure quick add list for appointments.</summary>
		ApptProcsQuickAdd,
		///<summary>4- Billing types.</summary>
		BillingTypes,
		///<summary>5- Not used.</summary>
		ClaimFormats,
		///<summary>6- Not used.</summary>
		DunningMessages,
		///<summary>7- Fee schedule names.</summary>
		FeeSchedNames,
		///<summary>8- Medical notes for quick paste.</summary>
		MedicalNotes,
		///<summary>9- No longer used</summary>
		OperatoriesOld,
		///<summary>10- Payment types.</summary>
		PaymentTypes,
		///<summary>11- Procedure code categories.</summary>
		ProcCodeCats,
		///<summary>12- Progress note colors.</summary>
		ProgNoteColors,
		///<summary>13- Statuses for recall, unscheduled, and next appointments.</summary>
		RecallUnschedStatus,
		///<summary>14- Service notes for quick paste.</summary>
		ServiceNotes,
		///<summary>15- Discount types.</summary>
		DiscountTypes,
		///<summary>16- Diagnosis types.</summary>
		Diagnosis,
		///<summary>17- Colors to display in the Appointments module.</summary>
		AppointmentColors,
		///<summary>18- Image categories.</summary>
		ImageCats,
		///<summary>19- Quick add notes for the ApptPhoneNotes, which is getting phased out.</summary>
		ApptPhoneNotes,
		///<summary>20- Treatment plan priority names.</summary>
		TxPriorities,
		///<summary>21- Miscellaneous color options.</summary>
		MiscColors,
		///<summary>22- Colors for the graphical tooth chart.</summary>
		ChartGraphicColors,
		///<summary>23- Categories for the Contact list.</summary>
		ContactCategories,
		///<summary>24- Categories for Letter Merge.</summary>
		LetterMergeCats,
		///<summary>25- Types of Schedule Blockouts.</summary>
		BlockoutTypes,
		///<summary>26- Categories of procedure buttons in Chart module</summary>
		ProcButtonCats,
		///<Summary>27- Types of commlog entries.</Summary>
		CommLogTypes
	}
	//public enum StudentStat{None,Full,Part};
	///<summary>Used in procedurecode setup to specify the treatment area for a procedure.  This determines what fields are available when editing an appointment.</summary>
	public enum TreatmentArea{
		///<summary>0-never used</summary>
		None,
		///<summary>1</summary>
		Surf,
		///<summary>2</summary>
		Tooth,
		///<summary>3</summary>
		Mouth,
		///<summary>4</summary>
		Quad,
		///<summary>5</summary>
		Sextant,
		///<summary>6</summary>
		Arch,
		///<summary>7</summary>
		ToothRange}
	///<summary>When the autorefresh message is sent to the other computers, this is the type.  Keep in mind that this is a default Int32 type,so the max is 2,147,483,647 unless we change the type.</summary>
	[Flags]
	public enum InvalidTypes{
		///<summary>0</summary>
		None=0,
		///<summary>1- Not used with any other flags</summary>
		Date=1,
		///<summary>2</summary>
		ProcCodes=2,
		///<summary>4</summary>
		Prefs=4,
		///<summary>8- Also includes appt rules</summary>
		Views=8,
		///<summary>16</summary>
		AutoCodes=16,
		///<summary>32</summary>
		ProcButtons=32,
		///<summary>64</summary>
		Carriers=64,
		///<summary>128- Also includes Signal/message defs.</summary>
		ClearHouses=128,
		///<summary>256</summary>
		Computers=256,
		///<summary>512- Also includes DisplayFields</summary>
		InsCats=512,
		///<summary>1024. Also includes payperiods.</summary>
		Employees=1024,
		///<summary>2048</summary>
		Startup=2048,
		///<summary>4096</summary>
		Defs=4096,
		///<summary>8192. Also includes diseases.</summary>
		Email=8192,
		///<summary>16384</summary>
		Fees=16384,
		///<summary>32768</summary>
		Letters=32768,
		///<summary>65536</summary>
		QuickPaste=65536,
		///<summary>131072</summary>
		Security=131072,
		///<summary>262144</summary>
		Programs=262144,
		///<summary>524288  Also includes Image Mounts</summary>
		ToolBut=524288,
		///<summary>1048576  Also includes clinics.</summary>
		Providers=1048576,
		///<summary>2097152. No longer used.</summary>
		SchedOld=2097152,
		///<summary>4194304</summary>
		ClaimForms=4194304,
		///<summary>8388608  Also includes patientfields</summary>
		ZipCodes=8388608,
		///<summary>16777216</summary>
		LetterMerge=16777216,
		///<summary>33554432</summary>
		DentalSchools=33554432,
		///<summary>67108864. Includes AccountingAutoPay.</summary>
		Operatories=67108864,
		///<summary>All flags combined except Date.</summary>
		AllLocal=134217728-1-1
	}
	//<summary></summary>
	/*public enum ButtonType{
		///<summary></summary>
		ButPush,
		///<summary></summary>
		Text}*/
	///<summary></summary>
	public enum DentalSpecialty{
		///<summary>0</summary>
		General,
		///<summary>1</summary>
		Hygienist,
		///<summary>2</summary>
		Endodontics,
		///<summary>3</summary>
		Pediatric,
		///<summary>4</summary>
		Perio,
		///<summary>5</summary>
		Prosth,
		///<summary>6</summary>
		Ortho,
		///<summary>7</summary>
		Denturist,
		///<summary>8</summary>
		Surgery,
		///<summary>9</summary>
		Assistant,
		///<summary>10</summary>
		LabTech,
		///<summary>11</summary>
		Pathology,
		///<summary>12</summary>
		PublicHealth,
		///<summary>13</summary>
		Radiology
	}
	///<summary>Appointment status.</summary>
	public enum ApptStatus{
		///<summary>0- No appointment should ever have this status.</summary>
		None,
		///<summary>1- Shows as a regularly scheduled appointment.</summary>
		Scheduled,
		///<summary>2- Shows greyed out.</summary>
		Complete,
		///<summary>3- Only shows on unscheduled list.</summary>
		UnschedList,
		///<summary>4- Functions the same as Scheduled for now.</summary>
		ASAP,
		///<summary>5- Shows with a big X on it.</summary>
		Broken,
		///<summary>6- Planned appointment.  Only shows in Chart module. User not allowed to change this status, and it does not display as one of the options.</summary>
		Planned,
		///<summary>7- Patient "post-it" note on the schedule. Shows light yellow. Shows on day scheduled just like appt, as well as in prog notes, etc.</summary>
		PtNote,
		///<summary>8- Patient "post-it" note completed</summary>
		PtNoteCompleted}

	///<summary></summary>
	public enum PatientStatus{
		///<summary>0</summary>
		Patient,
		///<summary>1</summary>
		NonPatient,
		///<summary>2</summary>
		Inactive,
		///<summary>3</summary>
		Archived,
		///<summary>4</summary>
		Deleted,
		///<summary>5</summary>
		Deceased}
	///<summary></summary>
	public enum PatientGender{
		///<summary>0</summary>
		Male,
		///<summary>1</summary>
		Female,
		///<summary>2- This is not a joke. Required by HIPAA for privacy.</summary>
		Unknown}
	///<summary></summary>
	public enum PatientPosition{
		///<summary>0</summary>
		Single,
		///<summary>1</summary>
		Married,
		///<summary>2</summary>
		Child,
		///<summary>3</summary>
		Widowed,
		///<summary>4</summary>
		Divorced}
	///<summary>For schedule timeblocks.</summary>
	public enum ScheduleType{
		///<summary>0</summary>
		Practice,
		///<summary>1</summary>
		Provider,
		///<summary>2</summary>
		Blockout,
		///<summary>3</summary>
		Employee}
	//<summary>Used in the appointment edit window.</summary>
	/*public enum LabCaseOld{
		///<summary>0</summary>
		None,
		///<summary>1</summary>
		Sent,
		///<summary>2</summary>
		Received,
		///<summary>3</summary>
		QualityChecked};*/
	///<summary></summary>
	public enum PlaceOfService{
		///<summary>0. CPT code 11</summary>
		Office,
		///<summary>1. CPT code 12</summary>
		PatientsHome,
		///<summary>2. CPT code 21</summary>
		InpatHospital,
		///<summary>3. CPT code 22</summary>
		OutpatHospital,
		///<summary>4. CPT code 31</summary>
		SkilledNursFac,
		///<summary>5. CPT code 33</summary>
		AdultLivCareFac,
		///<summary>6. CPT code ?</summary>
		OtherLocation,
		///<summary>7. CPT code 15</summary>
		MobileUnit,
		///<summary>8. CPT code 03</summary>
		School,
		///<summary>9. CPT code 26</summary>
		MilitaryTreatFac,
		///<summary>10. CPT code 50</summary>
		FederalHealthCenter,
		///<summary>11. CPT code 71</summary>
		PublicHealthClinic,
		///<summary>12. CPT code 72</summary>
		RuralHealthClinic,
	}
	///<summary>Used in the other appointments window to keep track of the result when closing.</summary>
	public enum OtherResult{
		///<summary></summary>
		Cancel,
		///<summary></summary>
		CreateNew,
		///<summary></summary>
		GoTo,
		///<summary></summary>
		CopyToPinBoard,
		///<summary></summary>
		NewToPinBoard,
		///<summary>Currently only used when scheduling a recall.  Puts it on the pinboard, and then launches a search, jumping to a new date in the process.</summary>
		PinboardAndSearch
	}
	//public enum SearchPatType{Lname,Fname,HmPhone,Address}
	///<summary></summary>
	public enum PaintType{
		///<summary>0</summary>
		Extraction,
		///<summary>1</summary>
		FillingSolid,
		///<summary>2</summary>
		FillingOutline,
		///<summary>3</summary>
		RCT,
		///<summary>4</summary>
		Post,
		///<summary>5</summary>
		CrownSolid,
		///<summary>6</summary>
		CrownOutline,
		///<summary>7</summary>
		CrownHatch,
		///<summary>8</summary>
		Implant,
		///<summary>9</summary>
		Sealant,
		///<summary>10</summary>
		PonticSolid,
		///<summary>11</summary>
		PonticOutline,
		///<summary>12</summary>
		PonticHatch,
		///<summary>13</summary>
		RetainerSolid,
		///<summary>14</summary>
		RetainerOutline,
		///<summary>15</summary>
		RetainerHatch}
	///<summary>Schedule status.  Open=0,Closed=1,Holiday=2.</summary>
  public enum SchedStatus{
		///<summary>0</summary>
		Open,
		///<summary>1</summary>
		Closed,
		///<summary>2</summary>
		Holiday}
	//<summary></summary>
  /*public enum BackupType{
		///<summary></summary>
		CopyFiles,
		///<summary></summary>
		CopyToServer,
		///<summary></summary>
		DataDump}*/
	///<summary></summary>
  public enum AutoCondition{
		///<summary>0</summary>
		Anterior,
		///<summary>1</summary>
		Posterior,
		///<summary>2</summary>
		Premolar,
		///<summary>3</summary>
		Molar,
		///<summary>4</summary>
		One_Surf,
		///<summary>5</summary>
		Two_Surf,
		///<summary>6</summary>
		Three_Surf,
		///<summary>7</summary>
		Four_Surf,
		///<summary>8</summary>
		Five_Surf,
		///<summary>9</summary>
		First,
		///<summary>10</summary>
		EachAdditional,
		///<summary>11</summary>
		Maxillary,
		///<summary>12</summary>
		Mandibular,
		///<summary>13</summary>
		Primary,
		///<summary>14</summary>
		Permanent,
		///<summary>15</summary>
		Pontic,
		///<summary>16</summary>
		Retainer,
		///<summary>17</summary>
		AgeOver18}
	///<Summary>Used for insurance substitutions conditions of procedurecodes.  Mostly for posterior composites.</Summary>
	public enum SubstitutionCondition{
		///<Summary>0</Summary>
		Always,
		///<Summary>1</Summary>
		Molar,
		///<Summary>2</Summary>
		SecondMolar
	}
	///<summary>Claimproc Status.  The status must generally be the same as the claim, although it is sometimes not strictly enforced.</summary>
	public enum ClaimProcStatus{
		///<summary>0: For claims that have been created or sent, but have not been received.</summary>
		NotReceived,
		///<summary>1: For claims that have been received.</summary>
		Received,
		///<summary>2: For preauthorizations.</summary>
		Preauth,
		///<summary>3: The only place that this status is used is to make adjustments to benefits from the coverage window.  It is never attached to a claim.</summary>
		Adjustment,
		///<summary>4:This differs from Received only slightly.  It's for additional payments on procedures already received.  Most fields are blank.</summary>
		Supplemental,
		///<summary>5: CapClaim is used when you want to send a claim to a capitation insurance company.  These are similar to Supplemental in that there will always be a duplicate claimproc for a procedure. The first claimproc tracks the copay and writeoff, has a status of CapComplete, and is never attached to a claim. The second claimproc has status of CapClaim.</summary>
		CapClaim,
		///<summary>6: Estimates have replaced the fields that were in the procedure table.  Once a procedure is complete, the claimprocstatus will still be Estimate.  An Estimate can be attached to a claim and status gets changed to NotReceived.</summary>
		Estimate,
		///<summary>7: For capitation procedures that are complete.  This replaces the old procedurelog.CapCoPay field. This stores the copay and writeoff amounts.  The copay is only there for reference, while it is the writeoff that actually affects the balance. Never attached to a claim. If procedure is TP, then status will be CapEstimate.  Only set to CapComplete if procedure is Complete.</summary>
		CapComplete,
		///<summary>8: For capitation procedures that are still estimates rather than complete.  When procedure is completed, this can be changed to CapComplete, but never to anything else.</summary>
		CapEstimate
	}
	//<summary>CommItemType of 0 is reserved for later use with user defined types.</summary>
	/*public enum CommItemType{
		///<Summary>Used temporarily while we get rid of StatementSent.</Summary>
		None=0,
		///<summary>1- auto. </summary>
		StatementSentOld=1,
		///<summary>2- Any activity related to appointment scheduling.</summary>
		ApptRelated,
		///<summary>3- </summary>
		Insurance,
		///<summary>4- </summary>
		Financial,
		///<summary>5- </summary>
		Recall,
		///<summary>6- </summary>
		Misc//LetterSent used to be 6
		//clinical not implemented yet.
	}*/

	///<summary></summary>
	public enum CommItemMode{
		///<summary>0- </summary>
		None,
		///<summary>1- </summary>
		Email,
		///<summary>2</summary>
		Mail,
		///<summary>3</summary>
		Phone,
		///<summary>4</summary>
		InPerson,
		///<summary>5</summary>
		AutoItem
	}

	///<summary>0=neither, 1=sent, 2=received.</summary>
	public enum CommSentOrReceived{
		///<summary>0</summary>
		Neither,
		///<summary>1</summary>
		Sent,
		///<summary>2</summary>
		Received
	}

	///<summary></summary>
	public enum ToolBarsAvail{
		///<summary>0</summary>
		AccountModule,
		///<summary>1</summary>
		ApptModule,
		///<summary>2</summary>
		ChartModule,
		///<summary>3</summary>
		ImagesModule,
		///<summary>4</summary>
		FamilyModule,
		///<summary>5</summary>
		TreatmentPlanModule,
		///<summary>6</summary>
		ClaimsSend
	}

	///<summary></summary>
	public enum TimeClockStatus{
		///<summary>0</summary>
		Home,
		///<summary>1</summary>
		Lunch,
		///<summary>2</summary>
		Break
	}

	///<summary>In perio, the type of measurements for a given row.</summary>
	public enum PerioSequenceType{
		///<summary>0</summary>
		Mobility,
		///<summary>1</summary>
		Furcation,
		///<summary>2-AKA recession.</summary>
		GingMargin,
		///<summary>3-MucoGingivalJunction- the division between attached and unattached mucosa.</summary>
		MGJ,
		///<summary>4</summary>
		Probing,
		///<summary>5</summary>
		SkipTooth,
		///<summary>6</summary>
		Bleeding,
		///<summary>7. But this type is never saved to the db. It is always calculated on the fly.</summary>
		CAL
	}

	///<summary>Race and ethnicity for patient. Used by public health.</summary>
	public enum PatientRace{
		///<summary>0</summary>
		Unknown,
		///<summary>1</summary>
		Multiracial,
		///<summary>2</summary>
		HispanicLatino,
		///<summary>3</summary>
		AfricanAmerican,
		///<summary>4</summary>
		White,
		///<summary>5</summary>
		HawaiiOrPacIsland,
		///<summary>6</summary>
		AmericanIndian,
		///<summary>7</summary>
		Asian,
		///<summary>8</summary>
		Other,
		///<summary>9</summary>
		Aboriginal
	}

	///<summary>Grade level used in public health.</summary>
	public enum PatientGrade{
		///<summary>0</summary>
		Unknown,
		///<summary>1</summary>
		First,
		///<summary>2</summary>
		Second,
		///<summary>3</summary>
		Third,
		///<summary>4</summary>
		Fourth,
		///<summary>5</summary>
		Fifth,
		///<summary>6</summary>
		Sixth,
		///<summary>7</summary>
		Seventh,
		///<summary>8</summary>
		Eighth,
		///<summary>9</summary>
		Ninth,
		///<summary>10</summary>
		Tenth,
		///<summary>11</summary>
		Eleventh,
		///<summary>12</summary>
		Twelfth,
		///<summary>13</summary>
		PrenatalWIC,
		///<summary>14</summary>
		PreK,
		///<summary>15</summary>
		Kindergarten,
		///<summary>16</summary>
		Other
	}

	///<summary>For public health.  Unknown, NoProblems, NeedsCarE, or Urgent.</summary>
	public enum TreatmentUrgency{
		///<summary></summary>
		Unknown,
		///<summary></summary>
		NoProblems,
		///<summary></summary>
		NeedsCare,
		///<summary></summary>
		Urgent
	}

	///<summary>The type of image for images module.</summary>
	public enum ImageType{
		///<summary>0- Includes scanned documents and screenshots.</summary>
		Document,
		///<summary>1</summary>
		Radiograph,
		///<summary>2</summary>
		Photo,
		///<summary>3- For instance a Word document or a spreadsheet. Not an image.</summary>
		File,
		///<summary>4- For xray mount sets.</summary>
		Mount,
	}

	///<summary>Used by QuickPasteCat to determine which category to default to when opening.</summary>
	public enum QuickPasteType{
		///<summary>0</summary>
		None,
		///<summary>1</summary>
		Procedure,
		///<summary>2</summary>
		Appointment,
		///<summary>3</summary>
		CommLog,
		///<summary>4</summary>
		Adjustment,
		///<summary>5</summary>
		Claim,
		///<summary>6</summary>
		Email,
		///<summary>7</summary>
		InsPlan,
		///<summary>8</summary>
		Letter,
		///<summary>9</summary>
		MedicalSummary,
		///<summary>10</summary>
		ServiceNotes,
		///<summary>11</summary>
		MedicalHistory,
		///<summary>12</summary>
		MedicationEdit,
		///<summary>13</summary>
		MedicationPat,
		///<summary>14</summary>
		PatAddressNote,
		///<summary>15</summary>
		Payment,
		///<summary>16</summary>
		PayPlan,
		///<summary>17</summary>
		Query,
		///<summary>18</summary>
		Referral,
		///<summary>19</summary>
		Rx,
		///<summary>20</summary>
		FinancialNotes,
		///<summary>21</summary>
		ChartTreatment,
		///<summary>22</summary>
    MedicalUrgent,
		///<summary>23</summary>
    Statement,
		///<summary>24</summary>
    Recall
	}

	///<summary>For every type of electronic claim format that Open Dental can create, there will be an item in this enumeration.  All e-claim formats are hard coded due to complexity.</summary>
	public enum ElectronicClaimFormat{
		///<summary>0-Not in database, but used in various places in program.</summary>
		None,
		///<summary>1-The American standard. HIPAA mandated.</summary>
		X12,
		///<summary>2-Proprietary format for Renaissance.</summary>
		Renaissance,
		///<summary>3-CDAnet format version 4.</summary>
		Canadian
	}

	///<summary>Used when submitting e-claims to some carriers who require extra provider identifiers.  Usage varies by company.  Only used as needed.</summary>
	public enum ProviderSupplementalID{
		///<summary>0</summary>
		BlueCross,
		///<summary>1</summary>
		BlueShield,
		///<summary>2</summary>
		SiteNumber,
		///<summary>3</summary>
		CommercialNumber
	}

	///<summary>Each clearinghouse can have a hard-coded comm bridge which handles all the communications of transfering the claim files to the clearinghouse/carrier.  Does not just include X12, but can include any format at all.</summary>
	public enum EclaimsCommBridge{
		///<summary>0-No comm bridge will be activated. The claim files will be created to the specified path, but they will not be uploaded.</summary>
		None,
		///<summary>1</summary>
		WebMD,
		///<summary>2</summary>
		BCBSGA,
		///<summary>3</summary>
		Renaissance,
		///<summary>4</summary>
		ClaimConnect,
		///<summary>5</summary>
		RECS,
		///<summary>6</summary>
		Inmediata,
		///<summary>7</summary>
		AOS,
		///<summary>8</summary>
		PostnTrack,
		///<summary>9 Canada</summary>
		CDAnet,
		///<summary>10</summary>
		Tesia
	}

	///<summary></summary>
	public enum PrintSituation{
		///<summary>0- Covers any printing situation not listed separately.</summary>
		Default,
		///<summary></summary>
		Statement,
		///<summary></summary>
		LabelSingle,
		///<summary></summary>
		Claim,
		///<summary>TP and perio</summary>
		TPPerio,
		///<summary></summary>
		Rx,
		///<summary></summary>
		LabelSheet,
		///<summary></summary>
		Postcard,
		///<summary></summary>
		Appointments
	}

	///<summary></summary>
	public enum TaskDateType{
		///<summary>0</summary>
		None,
		///<summary>1</summary>
		Day,
		///<summary>2</summary>
		Week,
		///<summary>3</summary>
		Month
	}

	///<summary>Used when attaching objects to tasks.  These are the choices.</summary>
	public enum TaskObjectType{
		///<summary>0</summary>
		None,
		///<summary>1</summary>
		Patient,
		///<summary>2</summary>
		Appointment
	}

	///<summary>Used as the enumeration of FieldValueType.ForeignKey.  Basically, this allows lists to be included in the parameter list.  The lists are those common short lists that are used so frequently.  The user can only select one from the list, and the primary key of that item will be used as the parameter.</summary>
	public enum ReportFKType{
		///<summary>0</summary>
		None,
		///<summary>The schoolclass table in the database. Used for dental schools.</summary>
		SchoolClass,
		///<summary>The schoolcourse table in the database. Used for dental schools.</summary>
		SchoolCourse
	}

	///<summary>A hard-coded list of permissions which may be granted to usergroups.</summary>
	public enum Permissions{
		///<summary>0</summary>
		None,
		///<summary>1</summary>
		AppointmentsModule,
		///<summary>2</summary>
		FamilyModule,
		///<summary>3</summary>
		AccountModule,
		///<summary>4</summary>
		TPModule,
		///<summary>5</summary>
		ChartModule,
		///<summary>6</summary>
		ImagesModule,
		///<summary>7</summary>
		ManageModule,
		///<summary>8. Currently covers a wide variety of setup functions.</summary>
		Setup,
		///<summary>9</summary>
		RxCreate,
		///<summary>10</summary>
		ProcComplEdit,
		///<summary>11</summary>
		ChooseDatabase,
		///<summary>12</summary>
		Schedules,
		///<summary>13</summary>
		Blockouts,
		///<summary>14</summary>
		ClaimsSentEdit,
		///<summary>15</summary>
		PaymentCreate,
		///<summary>16</summary>
		PaymentEdit,
		///<summary>17</summary>
		AdjustmentCreate,
		///<summary>18</summary>
		AdjustmentEdit,
		///<summary>19</summary>
		UserQuery,
		///<summary>20.  Not used anymore.</summary>
		StartupSingleUserOld,
		///<summary>21 Not used anymore.</summary>
		StartupMultiUserOld,
		///<summary>22</summary>
		Reports,
		///<summary>23. Includes setting procedures complete.</summary>
		ProcComplCreate,
		///<summary>24. At least one user must have this permission.</summary>
		SecurityAdmin,
		///<summary>25</summary>
		AppointmentCreate,
		///<summary>26</summary>
		AppointmentMove,
		///<summary>27</summary>
		AppointmentEdit,
		///<summary>28</summary>
		Backup,
		///<summary>29</summary>
		TimecardsEditAll,
		///<summary>30</summary>
		DepositSlips,
		///<summary>31</summary>
		AccountingEdit,
		///<summary>32</summary>
		AccountingCreate,
		///<summary>33</summary>
		Accounting
	}

	///<summary>The type of signal being sent.</summary>
	public enum SignalType{
		///<summary>0- Includes text messages.</summary>
		Button,
		///<summary>1</summary>
		Invalid
	}

	///<summary>Used in the benefit table.  Corresponds to X12 EB01.</summary>
	public enum InsBenefitType{
		///<summary>0- Not usually used.  Would only be used if you are just indicating that the patient is covered, but without any specifics.</summary>
		ActiveCoverage,
		///<summary>1- This corresponds to the X12 Co-Insurance type.  Except it's the opposite because it's the insurance coverage percentage, whereas X12 sends the percentage as the patient's responsibility portion.</summary>
		Percentage,
		///<summary>2- The deductible amount.  Might be two entries if, for instance, deductible is waived on preventive.</summary>
		Deductible,
		///<summary>3- A dollar amount.</summary>
		CoPayment,
		///<summary>4- Services that are simply not covered at all.</summary>
		Exclusions,
		///<summary>5- Covers a variety of limitations, including Max, frequency, fee reductions, etc.</summary>
		Limitations
	}

	///<summary>Used in the benefit table.  Corresponds to X12 EB06.</summary>
	public enum BenefitTimePeriod{
		///<summary>0- A timeperiod is frequenly not needed.  For example, percentages.</summary>
		None,
		///<summary>1- The renewal month is not Jan.  In this case, we need to know the effective date so that we know which month the benefits start over in.</summary>
		ServiceYear,
		///<summary>2- Renewal month is Jan.</summary>
		CalendarYear,
		///<summary>3- Usually used for ortho max.</summary>
		Lifetime,
		///<summary>4- Wouldn't be used alone.  Years would again be specified in the quantity field along with a number.</summary>
		Years
	}

	///<summary>Used in the benefit table in conjunction with an integer quantity.</summary>
	public enum BenefitQuantity{
		///<summary>0- This is used a lot. Most benefits do not need any sort of quantity.</summary>
		None,
		///<summary>1- For example, two exams per year</summary>
		NumberOfServices,
		///<summary>2- For example, 18 when flouride only covered to 18 y.o.</summary>
		AgeLimit,
		///<summary>3- For example, copay per 1 visit.</summary>
		Visits,
		///<summary>4- For example, pano every 5 years.</summary>
		Years,
		///<summary>5- For example, BWs every 6 months.</summary>
		Months
	}

	///<summary>Used in the benefit table </summary>
	public enum BenefitCoverageLevel{
		///<summary>0- Most common, and assumed if not supplied.</summary>
		Individual,
		///<summary>1- For example, family deductible or family maximum.</summary>
		Family
	}
	

	///<summary>The X12 benefit categories.  Used to link the user-defined CovCats to the corresponding X12 category.</summary>
	public enum EbenefitCategory{
		///<summary>0- Default</summary>
		None,
		///<summary>1- X12: 30 and 35. All ADA codes.</summary>
		General,
		///<summary>2- X12: 23. ADA D0000-D0999</summary>
		Diagnostic,
		///<summary>3- X12: 24. ADA D4000</summary>
		Periodontics,
		///<summary>4- X12: 25. ADA D2000</summary>
		Restorative,
		///<summary>5- X12: 26. ADA D3000</summary>
		Endodontics,
		///<summary>6- X12: 27. ADA D5900-D5999</summary>
		MaxillofacialProsth,
		///<summary>7- X12: 36. Subcategory of restorative.</summary>
		Crowns,
		///<summary>8- X12: 37. ADA range?</summary>
		Accident,
		///<summary>9- X12: 38. ADA D8000</summary>
		Orthodontics,
		///<summary>10- X12: 39. ADA D5000-D5899 (removable), and D6200-D6900 (fixed)</summary>
		Prosthodontics,
		///<summary>11- X12: 40. ADA D7000</summary>
		OralSurgery,
		///<summary>12- X12: 41. ADA D1000</summary>
		RoutinePreventive
	}

	///<summary>Used in accounting for chart of accounts.</summary>
	public enum AccountType{
		///<summary>0</summary>
		Asset,
		///<summary>1</summary>
		Liability,
		///<summary>2</summary>
		Equity,
		///<summary>3</summary>
		Income,
		///<summary>4</summary>
		Expense
	}

	///<summary>Replaces the graphic type table in the database for the new 3D tooth chart.  We cannot yet do veneers.</summary>
	public enum ToothPaintingType{
		///<summary>0</summary>
		None,
		///<summary>1</summary>
		Extraction,
		///<summary>2</summary>
		Implant,
		///<summary>3</summary>
		RCT,
		///<summary>4</summary>
		PostBU,
		///<summary>5</summary>
		FillingDark,
		///<summary>6</summary>
		FillingLight,
		///<summary>7</summary>
		CrownDark,
		///<summary>8</summary>
		CrownLight,
		///<summary>9</summary>
		BridgeDark,
		///<summary>10</summary>
		BridgeLight,
		///<summary>11</summary>
		DentureDark,
		///<summary>12</summary>
		DentureLight,
		///<summary>13</summary>
		Sealant
	}

	///<summary></summary>
	public enum ToothInitialType{
		///<summary>0</summary>
		Missing,
		///<summary>1</summary>
		Hidden,
		///<summary>2</summary>
		Primary,
		///<summary>3</summary>
		ShiftM,
		///<summary>4</summary>
		ShiftO,
		///<summary>5</summary>
		ShiftB,
		///<summary>6</summary>
		Rotate,
		///<summary>7</summary>
		TipM,
		///<summary>8</summary>
		TipB
	}
	
	///<summary>Indicates at what point the patient is in the sequence. 0=standby, 1=PatientInfo, 2=Medical, 3=UpdateOnly.</summary>
	public enum TerminalStatusEnum{
		///<summary>0</summary>
		Standby,
		///<summary>1</summary>
		PatientInfo,
		///<summary>2</summary>
		Medical,
		///<summary>3. Only the patient info tab will be visible.  This is just to let patient up date their address and phone number.</summary>
		UpdateOnly
	}

	///<summary>0=FreeformText, 1=YesNoUnknown. Allows for later adding other types, 3=picklist, 4, etc</summary>
	public enum QuestionType{
		///<summary>0</summary>
		FreeformText,
		///<summary>1</summary>
		YesNoUnknown
	}

	///<summary>0=User,1=Extra,2=Message.</summary>
	public enum SignalElementType{
		///<summary>0-To and From lists.  Not tied in any way to the users that are part of security.</summary>
		User,
		///<summary>Typically used to insert "family" before "phone" signals.</summary>
		Extra,
		///<summary>Elements of this type show in the last column and trigger the message to be sent.</summary>
		Message
	}

	///<summary></summary>
	public enum InsFilingCode{
		///<summary>0</summary>
		Commercial_Insurance,
		///<summary>1</summary>
		SelfPay,
		///<summary>2</summary>
		OtherNonFed,
		///<summary>3</summary>
		PPO,
		///<summary>4</summary>
		POS,
		///<summary>5</summary>
		EPO,
		///<summary>6</summary>
		Indemnity,
		///<summary>7</summary>
		HMO_MedicareRisk,
		///<summary>8</summary>
		DMO,
		///<summary>9</summary>
		BCBS,
		///<summary>10</summary>
		Champus,
		///<summary>11</summary>
		Disability,
		///<summary>12</summary>
		FEP,
		///<summary>13</summary>
		HMO,
		///<summary>14</summary>
		LiabilityMedical,
		///<summary>15</summary>
		MedicarePartB,
		///<summary>16</summary>
		Medicaid,
		///<summary>17</summary>
		ManagedCare_NonHMO,
		///<summary>18</summary>
		OtherFederalProgram,
		///<summary>19</summary>
		SelfAdministered,
		///<summary>20</summary>
		Veterans,
		///<summary>21</summary>
		WorkersComp,
		///<summary>22</summary>
		MutuallyDefined
	}

	///<summary>The _CA of some types should get stripped off when displaying to users.</summary>
	public enum EtransType{
		///<summary>0 X12-837</summary>
		ClaimSent,
		///<summary>1 claim</summary>
		ClaimPrinted,
		///<summary>2 Canada. Type 01</summary>
		Claim_CA,
		///<summary>3 Renaissance</summary>
		Claim_Ren,
		///<summary>4 Canada. Type 11</summary>
		ClaimAck_CA,
		///<summary>5 Canada. Type 21</summary>
		ClaimEOB_CA,
		///<summary>6 Canada. Type 08</summary>
		Eligibility_CA,
		///<summary>7 Canada. Type 18</summary>
		EligResponse_CA,
		///<summary>8 Canada. Type 02</summary>
		ClaimReversal_CA,
		///<summary>9 Canada. Type 03</summary>
		Predeterm_CA,
		///<summary>10 Canada. Type 04</summary>
		RequestOutstand_CA,
		///<summary>11 Canada. Type 05</summary>
		RequestSumm_CA,
		///<summary>12 Canada. Type 06</summary>
		RequestPay_CA,
		///<summary>13 Canada. Type 07</summary>
		ClaimCOB_CA,
		///<summary>14 Canada. Type 12</summary>
		ReverseResponse_CA,
		///<summary>15 Canada. Type 13</summary>
		PredetermAck_CA,
		///<summary>16 Canada. Type 23</summary>
		PredetermEOB_CA,
		///<summary>17 Canada. Type 14</summary>
		OutstandingAck_CA,
		///<summary>18 Canada. Type 24</summary>
		EmailResponse_CA,
		///<summary>19 Canada. Type 16</summary>
		PaymentResponse_CA,
		///<summary>20 Canada. Type 15</summary>
		SummaryResponse_CA,
		///<summary>21 Ack from clearinghouse. X12-997.</summary>
		Acknowledge_997,
		///<summary>22 X12-277. Unsolicited claim status notification.</summary>
		StatusNotify_277,
		///<summary>23 Text report from clearinghouse in human readable format.</summary>
		TextReport
	}

	///<summary></summary>
	public enum ContactMethod{
		///<summary>0</summary>
		None,
		///<summary>1</summary>
		DoNotCall,
		///<summary>2</summary>
		HmPhone,
		///<summary>3</summary>
		WkPhone,
		///<summary>4</summary>
		WirelessPh,
		///<summary>5</summary>
		Email,
		///<summary>6</summary>
		SeeNotes,
		///<summary>7</summary>
		Mail
	}

	///<summary>0=None,1=Declined,2=Scheduled,3=Consulted,4=InTreatment,5=Complete</summary>
	public enum ReferralToStatus{
		///<summary>0</summary>
		None,
		///<summary>1</summary>
		Declined,
		///<summary>2</summary>
		Scheduled,
		///<summary>3</summary>
		Consulted,
		///<summary>4</summary>
		InTreatment,
		///<summary>5</summary>
		Complete
	}

	

	



}





