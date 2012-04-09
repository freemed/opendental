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
		ToolBarsAvail,
		///<summary></summary>
		ProblemStatus,
		///<summary></summary>
		EZTwainErrorCode
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
	///<summary>Relationship of a child to a parent/guardian.</summary>
	public enum GuardianRelationship{
		///<summary>0</summary>
		Father,
		///<summary>1</summary>
		Mother,
		///<summary>2</summary>
		Stepfather,
		///<summary>3</summary>
		Stepmother,
		///<summary>4</summary>
		Grandfather,
		///<summary>5</summary>
		Grandmother,
		///<summary>6</summary>
		Sitter
	}
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
		D,
		///<summary>7- Condition.</summary>
		Cn
	}
		//?new?
	
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
	///<summary>When the autorefresh message is sent to the other computers, this is the type.</summary>
	public enum InvalidType{
		///<summary>0</summary>
		None,
		///<summary>1 Not used with any other flags</summary>
		Date,
		///<summary>2 Deprecated.  Inefficient.  All flags combined except Date and Tasks.</summary>
		AllLocal,
		///<summary>3 Not used with any other flags.  Used to just indicate added tasks, but now it indicates any change at all except those where a popup is needed.  If we also want a popup, then use TaskPopup.</summary>
		Task,
		///<summary>4</summary>
		ProcCodes,
		///<summary>5</summary>
		Prefs,
		///<summary>6 ApptViews, ApptViewItems, AppointmentRules, ProcApptColors.</summary>
		Views,
		///<summary>7</summary>
		AutoCodes,
		///<summary>8</summary>
		Carriers,
		///<summary>9</summary>
		ClearHouses,
		///<summary>10</summary>
		Computers,
		///<summary>11</summary>
		InsCats,
		///<summary>12- Also includes payperiods.</summary>
		Employees,
		///<summary>13- Deprecated.</summary>
		StartupOld,
		///<summary>14</summary>
		Defs,
		///<summary>15</summary>
		Email,
		///<summary>16</summary>
		Fees,
		///<summary>17</summary>
		Letters,
		///<summary>18</summary>
		QuickPaste,
		///<summary>19- Userod and UserGroup</summary>
		Security,
		///<summary>20</summary>
		Programs,
		///<summary>21- Also includes Image Mounts</summary>
		ToolBut,
		///<summary>22- Also includes clinics.</summary>
		Providers,
		///<summary>23</summary>
		ClaimForms,
		///<summary>24</summary>
		ZipCodes,
		///<summary>25</summary>
		LetterMerge,
		///<summary>26</summary>
		DentalSchools,
		///<summary>27</summary>
		Operatories,
		///<summary>28</summary>
		TaskPopup,
		///<summary>29</summary>
		Sites,
		///<summary>30</summary>
		Pharmacies,
		///<summary>31</summary>
		Sheets,
		///<summary>32</summary>
		RecallTypes,
		///<summary>33</summary>
		FeeScheds,
		///<summary>34. This is used internally by OD, Inc with the phonenumber table and the phone server.</summary>
		PhoneNumbers,
		///<summary>35. Signal/message defs</summary>
		Signals,
		///<summary>36</summary>
		DisplayFields,
		///<summary>37. And ApptFields.</summary>
		PatFields,
		///<summary>38</summary>
		AccountingAutoPays,
		///<summary>39</summary>
		ProcButtons,
		///<summary>40.  Includes ICD9s.</summary>
		Diseases,
		///<summary>41</summary>
		Languages,
		///<summary>42</summary>
		AutoNotes,
		///<summary>43</summary>
		ElectIDs,
		///<summary>44</summary>
		Employers,
		///<summary>45</summary>
		ProviderIdents,
		///<summary>46</summary>
		ShutDownNow,
		///<summary>47</summary>
		InsFilingCodes,
		///<summary>48</summary>
		ReplicationServers,
		///<summary>49</summary>
		Automation,
		///<summary>50. This is used internally by OD, Inc with the phone server to trigger the phone system to reload after changing which call groups users are in.</summary>
		PhoneAsteriskReload,
		///<summary>51</summary>
		TimeCardRules,
		///<summary>52. Includes DrugManufacturers and DrugUnits.</summary>
		Vaccines
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
		///<summary>4- Functions almost the same as Scheduled, but also causes the appointment to show on the ASAP list.</summary>
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
		Deceased,
		///<summary>6- Not an actual patient yet.</summary>
		Prospective
	}
	///<summary></summary>
	public enum PatientGender{
		///<summary>0</summary>
		Male,
		///<summary>1</summary>
		Female,
		///<summary>2- This is not a joke. Required by HIPAA for privacy.  Required by ehr to track missing entries.</summary>
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
		///<summary>5. CPT code 33.  In X12, a similar code AdultLivCareFac 35 is mentioned.</summary>
		CustodialCareFacility,
		///<summary>6. CPT code ?.  We use 11 for office.</summary>
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
		///<summary>13. CPT code 23</summary>
		EmergencyRoomHospital,
		///<summary>14. CPT code 24</summary>
		AmbulatorySurgicalCenter
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
		ClaimsSend,
		///<summary>7 Shows in the toolbar at the top that is common to all modules.</summary>
		AllModules
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
		///<summary>5-For the skiptooth type, set surf to none, and ToothValue to 1.</summary>
		SkipTooth,
		///<summary>6. Sum of flags for bleeding(1), suppuration(2), plaque(4), and calculus(8).</summary>
		Bleeding,
		///<summary>7. But this type is never saved to the db. It is always calculated on the fly.</summary>
		CAL
	}

	///<summary>Race and ethnicity for patient. Used by public health.  The problem is that everyone seems to want different choices.  If we give these choices their own table, then we also need to include mapping functions.  These are currently used in ArizonaReports, HL7 w ECW, and EHR.  Foreign users would like their own mappings.</summary>
	public enum PatientRace {
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
		Aboriginal,
		///<summary>10 - Required by EHR, even though it's stupid.</summary>
		BlackHispanic
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
    Recall,
		///<summary>25</summary>
		Popup,
		///<summary>26</summary>
		TxtMsg
	}

	///<summary>For every type of electronic claim format that Open Dental can create, there will be an item in this enumeration.  All e-claim formats are hard coded due to complexity.</summary>
	public enum ElectronicClaimFormat{
		///<summary>0-Not in database, but used in various places in program.</summary>
		None,
		///<summary>1-The American standard through 12/31/11.</summary>
		x837D_4010,
		///<summary>2-Proprietary format for Renaissance.</summary>
		Renaissance,
		///<summary>3-CDAnet format version 4.</summary>
		Canadian,
		///<summary>4-CSV file adaptable for use in Netherlands.</summary>
		Dutch,
		///<summary>5-The American standard starting on 1/1/12.</summary>
		x837D_5010_dental,
		///<summary>6-Either professional or medical.  The distiction is stored at the claim level.</summary>
		x837_5010_med_inst
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
		Tesia,
		///<summary>11</summary>
		MercuryDE,
		///<summary>12</summary>
		ClaimX,
		///<summary>13</summary>
		DentiCal,
		///<summary>14</summary>
		EmdeonMedical
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
		Appointments,
		///<summary></summary>
		RxControlled,
		///<summary></summary>
		Receipt
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

	public enum TaskStatusEnum{
		///<summary>0</summary>
		New,
		///<summary>1</summary>
		Viewed,
		///<summary>2</summary>
		Done
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
		///<summary>1- Used for percentages to indicate portion that insurance will cover.  When interpreting electronic benefit information, this is the opposite percentage, the percentage that the patient will pay after deductible.</summary>
		CoInsurance,
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

	///<summary>Used in the benefit table.</summary>
	public enum BenefitCoverageLevel{
		///<summary>0- Since this is a situational X12 field, we can also have none.  Typical for percentages and copayments.</summary>
		None,
		///<summary>1- The default for deductibles and maximums.</summary>
		Individual,
		///<summary>2- For example, family deductible or family maximum.</summary>
		Family
		
	}
	

	///<summary>The X12 benefit categories.  Used to link the user-defined CovCats to the corresponding X12 category.</summary>
	public enum EbenefitCategory{
		///<summary>0- Default.  Applies to all codes.</summary>
		None,
		///<summary>1- X12: 30 and 35. All ADA codes except ortho.  D0000-D7999 and D9000-D9999</summary>
		General,
		///<summary>2- X12: 23. ADA D0000-D0999.  This includes DiagnosticXray.</summary>
		Diagnostic,
		///<summary>3- X12: 24. ADA D4000</summary>
		Periodontics,
		///<summary>4- X12: 25. ADA D2000-D2699, and D2800-D2999.</summary>
		Restorative,
		///<summary>5- X12: 26. ADA D3000</summary>
		Endodontics,
		///<summary>6- X12: 27. ADA D5900-D5999</summary>
		MaxillofacialProsth,
		///<summary>7- X12: 36. Exclusive subcategory of restorative.  D2700-D2799</summary>
		Crowns,
		///<summary>8- X12: 37. ADA range?</summary>
		Accident,
		///<summary>9- X12: 38. ADA D8000-D8999</summary>
		Orthodontics,
		///<summary>10- X12: 39. ADA D5000-D5899 (removable), and D6200-D6899 (fixed)</summary>
		Prosthodontics,
		///<summary>11- X12: 40. ADA D7000</summary>
		OralSurgery,
		///<summary>12- X12: 41. ADA D1000</summary>
		RoutinePreventive,
		///<summary>13- X12: 4. ADA D0200-D0399.  So this is like an optional category which is otherwise considered to be diagnosic.</summary>
		DiagnosticXRay,
		///<summary>14- X12: 28. ADA D9000-D9999</summary>
		Adjunctive
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

	///<summary></summary>
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
		Sealant,
		///<summary>14</summary>
		Veneer,
		///<summary>15</summary>
		Watch
	}

	///<summary></summary>
	public enum ToothInitialType{
		///<summary>0</summary>
		Missing,
		///<summary>1 - Also hides the number.  This is now also allowed for primary teeth.</summary>
		Hidden,
		///<summary>2 - Only used with 1-32.  "sets" this tooth as a primary tooth.  The result is that the primary tooth shows in addition to the perm, and that the letter shows in addition to the number.  It also does a Shift0 -12 and some other handy movements.  Even if this is set to true, there can be a separate entry for a missing primary tooth; this would be almost equivalent to not even setting the tooth as primary, but would also allow user to select the letter.</summary>
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
		TipB,
		///<summary>9 One segment of a drawing.</summary>
		Drawing
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
	public enum InsFilingCodeOldOld{
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
		Mail,
		///<summary>8</summary>
		TextMessage
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

	///<summary></summary>
	public enum StatementMode{
		///<summary>0</summary>
		Mail,
		///<summary>1</summary>
		InPerson,
		///<summary>2</summary>
		Email,
		///<summary>3</summary>
		Electronic
	}
	/* dennis add other objects type here */
	public enum DeletedObjectType{
		///<summary>0</summary>
		Appointment,
		///<summary>1 - A schedule object.  Only provider schedules are tracked for deletion.</summary>
		ScheduleProv,
		///<summary>2 - When a recall row is deleted, this records the PatNum for which it was deleted.</summary>
		RecallPatNum,
		RxPat,
		LabPanel,
		LabResult,
		DrugUnit,
		Medication,
		MedicationPat,
		Allergy,
		AllergyDef,
		Disease,
		DiseaseDef,
		ICD9,
		Provider,
		Pharmacy,
		Statement,
		Document,
		Recall
	}

	public enum HL7MessageStatus {
		///<summary>0</summary>
		OutPending,
		///<summary>1</summary>
		OutSent,
		///<summary>2</summary>
		InReceived,
		///<summary>3</summary>
		InProcessed
	}

	///<summary>0=UnknownIfEver,1=SmokerUnkownCurrent,2=NeverSmoked,3=FormerSmoker,4=CurrentSomeDay,5=CurrentEveryDay</summary>
	public enum SmokingStatus {
		///<summary>0</summary>
		UnknownIfEver_Recode9,
		///<summary>1</summary>
		SmokerUnknownCurrent_Recode5,
		///<summary>2</summary>
		NeverSmoked_Recode4,
		///<summary>3</summary>
		FormerSmoker_Recode3,
		///<summary>4</summary>
		CurrentSomeDay_Recode2,
		///<summary>5</summary>
		CurrentEveryDay_Recode1
	}

	///<summary>0=Active, 1=Resolved, 2=Inactive</summary>
	public enum ProblemStatus{
		/// <summary>0</summary>
		Active,
		/// <summary>1</summary>
		Resolved,
		/// <summary>2</summary>
		Inactive
	}

	///<summary>EZTwain Error Codes. 43 EZTEC_NO_PDF is because of a missing DLL.</summary>
	public enum EZTwainErrorCode {
		///<summary>0</summary>
		EZTEC_NONE,
		///<summary>1</summary>
		EZTEC_START_TRIPLET_ERRS,
		///<summary>3</summary>
		EZTEC_CAP_GET,
		///<summary>3</summary>
		EZTEC_CAP_SET,
		///<summary>4</summary>
		EZTEC_DSM_FAILURE,
		///<summary>5</summary>
		EZTEC_DS_FAILURE,
		///<summary>6</summary>
		EZTEC_XFER_FAILURE,
		///<summary>7</summary>
		EZTEC_END_TRIPLET_ERRS,
		///<summary>8</summary>
		EZTEC_OPEN_DSM,
		///<summary>9</summary>
		EZTEC_OPEN_DEFAULT_DS,
		///<summary>10</summary>
		EZTEC_NOT_STATE_4,
		///<summary>11</summary>
		EZTEC_NULL_HCON,
		///<summary>12</summary>
		EZTEC_BAD_HCON,
		///<summary>13</summary>
		EZTEC_BAD_CONTYPE,
		///<summary>14</summary>
		EZTEC_BAD_ITEMTYPE,
		///<summary>15</summary>
		EZTEC_CAP_GET_EMPTY,
		///<summary>16</summary>
		EZTEC_CAP_SET_EMPTY,
		///<summary>17</summary>
		EZTEC_INVALID_HWND,
		///<summary>18</summary>
		EZTEC_PROXY_WINDOW,
		///<summary>19</summary>
		EZTEC_USER_CANCEL,
		///<summary>20</summary>
		EZTEC_RESOLUTION,
		///<summary>21</summary>
		EZTEC_LICENSE,
		///<summary>22</summary>
		EZTEC_JPEG_DLL,
		///<summary>23</summary>
		EZTEC_SOURCE_EXCEPTION,
		///<summary>24</summary>
		EZTEC_LOAD_DSM,
		///<summary>25</summary>
		EZTEC_NO_SUCH_DS,
		///<summary>26</summary>
		EZTEC_OPEN_DS,
		///<summary>27</summary>
		EZTEC_ENABLE_FAILED,
		///<summary>28</summary>
		EZTEC_BAD_MEMXFER,
		///<summary>29</summary>
		EZTEC_JPEG_GRAY_OR_RGB,
		///<summary>30</summary>
		EZTEC_JPEG_BAD_Q ,
		///<summary>31</summary>
		EZTEC_BAD_DIB,
		///<summary>32</summary>
		EZTEC_BAD_FILENAME,
		///<summary>33</summary>
		EZTEC_FILE_NOT_FOUND,
		///<summary>34</summary>
		EZTEC_FILE_ACCESS,
		///<summary>35</summary>
		EZTEC_MEMORY,
		///<summary>36</summary>
		EZTEC_JPEG_ERR,
		///<summary>37</summary>
		EZTEC_JPEG_ERR_REPORTED,
		///<summary>38</summary>
		EZTEC_0_PAGES,
		///<summary>39</summary>
		EZTEC_UNK_WRITE_FF,
		///<summary>40</summary>
		EZTEC_NO_TIFF,
		///<summary>41</summary>
		EZTEC_TIFF_ERR,
		///<summary>42</summary>
		EZTEC_PDF_WRITE_ERR,
		///<summary>43</summary>
		EZTEC_NO_PDF,
		///<summary>44</summary>
		EZTEC_GIFCON,
		///<summary>45</summary>
		EZTEC_FILE_READ_ERR,
		///<summary>46</summary>
		EZTEC_BAD_REGION,
		///<summary>47</summary>
		EZTEC_FILE_WRITE,
		///<summary>48</summary>
		EZTEC_NO_DS_OPEN,
		///<summary>49</summary>
		EZTEC_DCXCON,
		///<summary>50</summary>
		EZTEC_NO_BARCODE,
		///<summary>51</summary>
		EZTEC_UNK_READ_FF,
		///<summary>52</summary>
		EZTEC_DIB_FORMAT,
		///<summary>53</summary>
		EZTEC_PRINT_ERR,
		///<summary>54</summary>
		EZTEC_NO_DCX,
		///<summary>55</summary>
		EZTEC_APP_BAD_CON,
		///<summary>56</summary>
		EZTEC_LIC_KEY,
		///<summary>57</summary>
		EZTEC_INVALID_PARAM,
		///<summary>58</summary>
		EZTEC_INTERNAL,
		///<summary>59</summary>
		EZTEC_LOAD_DLL,
		///<summary>60</summary>
		EZTEC_CURL,
		///<summary>61</summary>
		EZTEC_MULTIPAGE_OPEN,
		///<summary>62</summary>
		EZTEC_BAD_SHUTDOWN,
		///<summary>63</summary>
		EZTEC_DLL_VERSION,
		///<summary>64</summary>
		EZTEC_OCR_ERR,
		///<summary>65</summary>
		EZTEC_ONLY_TO_PDF,
		///<summary>66</summary>
		EZTEC_APP_TITLE,
		///<summary>67</summary>
		EZTEC_PATH_CREATE,
		///<summary>68</summary>
		EZTEC_LATE_LIC,
		///<summary>69</summary>
		EZTEC_PDF_PASSWORD,
		///<summary>70</summary>
		EZTEC_PDF_UNSUPPORTED,
		///<summary>71</summary>
		EZTEC_PDF_BAFFLED,
		///<summary>72</summary>
		EZTEC_PDF_INVALID,
		///<summary>73</summary>
		EZTEC_PDF_COMPRESSION,
		///<summary>74</summary>
		EZTEC_NOT_ENOUGH_PAGES,
		///<summary>75</summary>
		EZTEC_DIB_ARRAY_OVERFLOW,
		///<summary>76</summary>
		EZTEC_DEVICE_PAPERJAM,
		///<summary>77</summary>
		EZTEC_DEVICE_DOUBLEFEED,
		///<summary>78</summary>
		EZTEC_DEVICE_COMM,
		///<summary>79</summary>
		EZTEC_DEVICE_INTERLOCK,
		///<summary>80</summary>
		EZTEC_BAD_DOC,
		///<summary>81</summary>
		EZTEC_OTHER_DS_OPEN
	}

	
}





