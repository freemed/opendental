using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Stores small bits of data for a wide variety of purposes.  Any data that's too small to warrant its own table will usually end up here.</summary>
	[Serializable]
	[CrudTable(TableName="preference")]
	public class Pref:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PrefNum;
		///<summary>The text 'key' in the key/value pairing.</summary>
		public string PrefName;
		///<summary>The stored value.</summary>
		public string ValueString;
		///<summary>Documentation on usage and values of each pref.  Mostly deprecated now in favor of using XML comments in the code.</summary>
		public string Comments;
	}

	///<summary>Because this enum is stored in the database as strings rather than as numbers, we can do the order alphabetically and we can change it whenever we want.</summary>
	public enum PrefName {
		AccountingCashIncomeAccount,
		AccountingDepositAccounts,
		AccountingIncomeAccount,
		AccountingLockDate,
		///<summary>Enum:AccountingSoftware 0=None, 1=Open Dental, 2=QuickBooks</summary>
		AccountingSoftware,
		AccountShowPaymentNums,
		ADAComplianceDateTime,
		ADAdescriptionsReset,
		AgingCalculatedMonthlyInsteadOfDaily,
		///<summary>FK to allergydef.AllergyDefNum</summary>
		AllergiesIndicateNone,
		AllowedFeeSchedsAutomate,
		AllowSettingProcsComplete,
		AppointmentBubblesDisabled,
		///<summary>Enum:SearchBehaviorCriteria 0=ProviderTime, 1=ProviderTimeOperatory</summary>
		AppointmentSearchBehavior,
		AppointmentTimeArrivedTrigger,
		AppointmentTimeDismissedTrigger,
		AppointmentTimeIncrement,
		///<summary>Set to true if appointment times are locked by default.</summary>
		AppointmentTimeIsLocked,
		AppointmentTimeSeatedTrigger,
		ApptBubbleDelay,
		ApptExclamationShowForUnsentIns,
		///<summary>Keeps the waiting room indicator times current.  Initially 1.</summary>
		ApptModuleRefreshesEveryMinute,
		///<summary>Integer</summary>
		ApptPrintColumnsPerPage,
		///<summary>Float</summary>
		ApptPrintFontSize,
		ApptPrintTimeStart,
		ApptPrintTimeStop,
		///<summary>Deprecated, but must remain here to avoid breaking updates.</summary>
		AtoZfolderNotRequired,
		///<summary>Normally 1.  If this is set to 0, then that means images are being stored in the database.  This used to be called AtoZfolderNotRequired, but that name was confusing.</summary>
		AtoZfolderUsed,
		AutoResetTPEntryStatus,
		BackupExcludeImageFolder,
		BackupFromPath,
		BackupReminderLastDateRun,
		BackupRestoreAtoZToPath,
		BackupRestoreFromPath,
		BackupRestoreToPath,
		BackupToPath,
		BalancesDontSubtractIns,
		BankAddress,
		BankRouting,
		BillingAgeOfAccount,
		BillingChargeAdjustmentType,
		BillingChargeAmount,
		BillingChargeLastRun,
		///<summary>Value is a string, either Billing or Finance.</summary>
		BillingChargeOrFinanceIsDefault,
		BillingDefaultsInvoiceNote,
		BillingDefaultsIntermingle,
		BillingDefaultsLastDays,
		BillingDefaultsNote,
		BillingElectClientAcctNumber,
		BillingElectCreditCardChoices,
		BillingElectPassword,
		BillingElectUserName,
		BillingElectVendorId,
		BillingElectVendorPMSCode,
		BillingEmailBodyText,
		BillingEmailSubject,
		BillingExcludeBadAddresses,
		BillingExcludeIfUnsentProcs,
		BillingExcludeInactive,
		BillingExcludeInsPending,
		BillingExcludeLessThan,
		BillingExcludeNegative,
		BillingIgnoreInPerson,
		BillingIncludeChanged,
		BillingSelectBillingTypes,
		/// <summary>0=no,1=EHG,2=POS(xml file)</summary>
		BillingUseElectronic,
		BirthdayPostcardMsg,
		BrokenAppointmentAdjustmentType,
		BrokenApptCommLogNotAdjustment,
		///<summary>For Ontario Dental Association fee schedules.</summary>
		CanadaODAMemberNumber,
		///<summary>For Ontario Dental Association fee schedules.</summary>
		CanadaODAMemberPass,
		///<summary>This is the hash of the password that is needed to open the Central Manager tool.</summary>
		CentralManagerPassHash,
		ChartQuickAddHideAmalgam,
		///<summary>If set to true (1), then after adding a proc, a row will be added to datatable instead of rebuilding entire datatable by making queries to the database.</summary>
		ChartAddProcNoRefreshGrid,
		ClaimAttachExportPath,
		ClaimFormTreatDentSaysSigOnFile,
		ClaimMedTypeIsInstWhenInsPlanIsMedical,
		ClaimsValidateACN,
		ClearinghouseDefaultDent,
		ClearinghouseDefaultMed,
		ConfirmEmailMessage,
		ConfirmEmailSubject,
		ConfirmPostcardMessage,
		///<summary>FK to definition.DefNum.  Initially 0.</summary>
		ConfirmStatusEmailed,
		///<summary>FK to definition.DefNum.</summary>
		ConfirmStatusTextMessaged,
		///<summary>The message that goes out to patients when doing a batch confirmation.</summary>
		ConfirmTextMessage,
		CoPay_FeeSchedule_BlankLikeZero,
		CorruptedDatabase,
		CropDelta,
		CustomizedForPracticeWeb,
		DatabaseConvertedForMySql41,
		DataBaseVersion,
		DateDepositsStarted,
		DateLastAging,
		DefaultClaimForm,
		DefaultProcedurePlaceService,
		///<summary>Boolean.  Set to 1 to indicate that this database holds customers instead of patients.  Used by OD HQ.</summary>
		DistributorKey,
		DockPhonePanelShow,
		///<summary>The AtoZ folder path.</summary>
		DocPath,
		EasyBasicModules,
		EasyHideAdvancedIns,
		EasyHideCapitation,
		EasyHideClinical,
		EasyHideDentalSchools,
		EasyHideHospitals,
		EasyHideInsurance,
		EasyHideMedicaid,
		EasyHidePrinters,
		EasyHidePublicHealth,
		EasyHideRepeatCharges,
		EasyNoClinics,
		EclaimsSeparateTreatProv,
		EHREmailFromAddress,
		EHREmailPassword,
		EHREmailPOPserver,
		EHREmailPort,
		///<summary>Boolean.  Initially 0.  Set to 1 to indicate that only high significant Rx alerts will show.  0 to show all significance level Rx alerts.</summary>
		EhrRxAlertHighSeverity,
		///<summary>This pref is hidden, so no practical way for user to turn this on.  Only used for ehr testing.</summary>
		EHREmailToAddress,
		///<summary>Date when user upgraded to 13.1.14 and started using NewCrop Guids on Rxs.</summary>
		ElectronicRxDateStartedUsing131,
		/// <summary>FK to EmailAddress.EmailAddressNum.  It is not required that a default be set.</summary>
		EmailDefaultAddressNum,
		/// <summary>Deprecated. Use emailaddress.EmailPassword instead.</summary>
		EmailPassword,
		/// <summary>Deprecated. Use emailaddress.ServerPort instead.</summary>
		EmailPort,
		/// <summary>Deprecated. Use emailaddress.SenderAddress instead.</summary>
		EmailSenderAddress,
		/// <summary>Deprecated. Use emailaddress.SMTPserver instead.</summary>
		EmailSMTPserver,
		/// <summary>Deprecated. Use emailaddress.EmailUsername instead.</summary>
		EmailUsername,
		/// <summary>Deprecated. Use emailaddress.UseSSL instead.</summary>
		EmailUseSSL,
		/// <summary>Boolean. 0 means false and means it is not an EHR Emergency, and emergency access to the family module is not granted.</summary>
		EhrEmergencyNow,
		///<summary>There is no UI for this.  It's only used by OD HQ.</summary>
		EhrProvKeyGeneratorPath,
		EnableAnesthMod,
		ExportPath,
		FinanceChargeAdjustmentType,
		FinanceChargeAPR,
		FinanceChargeLastRun,
		FuchsListSelectionColor,
		FuchsOptionsOn,
		GenericEClaimsForm,
		HL7FolderOut,
		HL7FolderIn,
		///<summary>procedurelog.DiagnosticCode will be set to this for new procedures and complete procedures if this field was blank when set complete.</summary>
		ICD9DefaultForNewProcs,
		ImagesModuleTreeIsCollapsed,
		ImageWindowingMax,
		ImageWindowingMin,
		///<summary>0=Default practice provider, -1=Treating Provider. Otherwise, FK to provider.ProvNum.</summary>
		InsBillingProv,
		InsDefaultCobRule,
		InsDefaultPPOpercent,
		InsDefaultShowUCRonClaims,
		InsDefaultAssignBen,
		///<summary>0=unknown, user did not make a selection.  1=Yes, 2=No.</summary>
		InsPlanConverstion_7_5_17_AutoMergeYN,
		InsurancePlansShared,
		IntermingleFamilyDefault,
		LabelPatientDefaultSheetDefNum,
		///<summary>Initially set to Declined to Specify.  Indicates which language from the LanguagesUsedByPatients preference is the language that indicates the patient declined to specify.  Text must exactly match a language in the list of available languages.  Can be blank if the user deletes the language from the list of available languages.</summary>
		LanguagesIndicateNone,
		///<summary>Comma-delimited list of two-letter language names and custom language names.  The custom language names are the full string name and are not necessarily supported by Microsoft.</summary>
		LanguagesUsedByPatients,
		LetterMergePath,
		MainWindowTitle,
		///<summary>New procs will use the fee amount tied to the medical code instead of the ADA code.</summary>
		MedicalFeeUsedForNewProcs,
		///<summary>FK to medication.MedicationNum</summary>
		MedicationsIndicateNone,
		MobileSyncDateTimeLastRun,
		///<summary>Used one time after the conversion to 7.9 for initial synch of the provider table.</summary>
		MobileSynchNewTables79Done,
		///<summary>Used one time after the conversion to 11.2 for re-synch of the patient records because a)2 columns BalTotal and InsEst have been added to the patientm table. b) the table documentm has been added</summary>
		MobileSynchNewTables112Done,
		///<summary>Used one time after the conversion to 12.1 for the recallm table being added and for upload of the practice Title.</summary>
		MobileSynchNewTables121Done,
		MobileSyncIntervalMinutes,
		MobileSyncServerURL,
		MobileSyncWorkstationName,
		MobileExcludeApptsBeforeDate,
		MobileUserName,
		//MobileSyncLastFileNumber,
		//MobileSyncPath,
		MySqlVersion,
		///<summary>There is no UI for user to change this.  Format, if OD customer, is PatNum-(RandomString)(CheckSum).  Example: 1234-W6c43.  Format for resellers is up to them.</summary>
		NewCropAccountId,
		/// <summary>There is no UI for user to change this. For resellers, this is part of the credentials.  OD credentials are not stored here, but are hard-coded.</summary>
		NewCropName,
		/// <summary>There is no UI for user to change this. For resellers, this is part of the credentials.  OD credentials are not stored here, but are hard-coded.</summary>
		NewCropPartnerName,
		/// <summary>There is no UI for user to change this. For resellers, this is part of the credentials.  OD credentials are not stored here, but are hard-coded.</summary>
		NewCropPassword,
		OpenDentalVendor,
		OracleInsertId,
		PasswordsMustBeStrong,
		PatientFormsShowConsent,
		PatientPortalURL,
		PatientSelectUsesSearchButton,
		PayPlansBillInAdvanceDays,
		PerioColorCAL,
		PerioColorFurcations,
		PerioColorFurcationsRed,
		PerioColorGM,
		PerioColorMGJ,
		PerioColorProbing,
		PerioColorProbingRed,
		PerioRedCAL,
		PerioRedFurc,
		PerioRedGing,
		PerioRedMGJ,
		PerioRedMob,
		PerioRedProb,
		PlannedApptTreatedAsRegularAppt,
		PracticeAddress,
		PracticeAddress2,
		PracticeBankNumber,
		PracticeBillingAddress,
		PracticeBillingAddress2,
		PracticeBillingCity,
		PracticeBillingST,
		PracticeBillingZip,
		PracticeCity,
		PracticeDefaultBillType,
		PracticeDefaultProv,
		///<summary>In USA and Canada, enforced to be exactly 10 digits or blank.</summary>
		PracticeFax,
		PracticePayToAddress,
		PracticePayToAddress2,
		PracticePayToCity,
		PracticePayToST,
		PracticePayToZip,
		///<summary>In USA and Canada, enforced to be exactly 10 digits or blank.</summary>
		PracticePhone,
		PracticeST,
		PracticeTitle,
		PracticeZip,
		///<summary>FK to diseasedef.DiseaseDefNum</summary>
		ProblemsIndicateNone,
		///<summary>In FormProcCodes, this is the default for the ShowHidden checkbox.</summary>
		ProcCodeListShowHidden,
		ProcLockingIsAllowed,
		ProcessSigsIntervalInSecs,
		ProcGroupNoteDoesAggregate,
		ProgramVersion,
		ProviderIncomeTransferShows,
		///<summary>FK to sheet.SheetNum. Must be an exam sheet. Only makes sense if PublicHealthScreeningUsePat is true.</summary>
		PublicHealthScreeningSheet,
		///<summary>Boolean. Initially set to 0.  When in this mode, screenings will be attached to actual PatNums rather than just freeform text names.</summary>
		PublicHealthScreeningUsePat,
		QuickBooksCompanyFile,
		QuickBooksDepositAccounts,
		QuickBooksIncomeAccount,
		RandomPrimaryKeys,
		RecallAdjustDown,
		RecallAdjustRight,
		///<summary>Defaults to 12 for new customers.  The number in this field is considered adult.  Only used when automatically adding procedures to a new recall appointment.</summary>
		RecallAgeAdult,
		RecallCardsShowReturnAdd,
		///<summary>-1 indicates min for all dates</summary>
		RecallDaysFuture,
		///<summary>-1 indicates min for all dates</summary>
		RecallDaysPast,
		RecallEmailFamMsg,
		RecallEmailFamMsg2,
		RecallEmailFamMsg3,
		RecallEmailMessage,
		RecallEmailMessage2,
		RecallEmailMessage3,
		RecallEmailSubject,
		RecallEmailSubject2,
		RecallEmailSubject3,
		RecallExcludeIfAnyFutureAppt,
		RecallGroupByFamily,
		RecallMaxNumberReminders,
		RecallPostcardFamMsg,
		RecallPostcardFamMsg2,
		RecallPostcardFamMsg3,
		RecallPostcardMessage,
		RecallPostcardMessage2,
		RecallPostcardMessage3,
		RecallPostcardsPerSheet,
		RecallShowIfDaysFirstReminder,
		RecallShowIfDaysSecondReminder,
		RecallStatusEmailed,
		RecallStatusMailed,
		///<summary>Used if younger than 12 on the recall date.</summary>
		RecallTypeSpecialChildProphy,
		RecallTypeSpecialPerio,
		RecallTypeSpecialProphy,
		///<summary>Comma-delimited list. FK to recalltype.RecallTypeNum.</summary>
		RecallTypesShowingInList,
		///<summary>If false, then it will only use email in the recall list if email is the preferred recall method.</summary>
		RecallUseEmailIfHasEmailAddress,
		RegistrationKey,
		RegistrationKeyIsDisabled,
		RegistrationNumberClaim,
		RenaissanceLastBatchNumber,
		///<summary>If replication has failed, this indicates the server_id.  No computer will be able to connect to this single server until this flag is cleared.</summary>
		ReplicationFailureAtServer_id,
		ReportFolderName,
		ReportsPPOwriteoffDefaultToProcDate,
		ReportsShowPatNum,
		ReportPandIschedProdSubtractsWO,
		RxSendNewToQueue,
		SalesTaxPercentage,
		ScannerCompression,
		ScannerResolution,
		ScannerSuppressDialog,
		ScheduleProvUnassigned,
		SecurityLockDate,
		///<summary>Set to 0 to always grant permission. 1 means only today.</summary>
		SecurityLockDays,
		SecurityLockIncludesAdmin,
		///<summary>Set to 0 to disable auto logoff.</summary>
		SecurityLogOffAfterMinutes,
		SecurityLogOffWithWindows,
		ShowAccountFamilyCommEntries,
		ShowFeatureEhr,
		ShowFeatureMedicalInsurance,
		ShowFeatureSuperfamilies,
		///<summary>0=None, 1=PatNum, 2=ChartNumber, 3=Birthdate</summary>
		ShowIDinTitleBar,
		ShowProgressNotesInsteadofCommLog,
		ShowUrgFinNoteInProgressNotes,
		SolidBlockouts,
		SpellCheckIsEnabled,
		StatementAccountsUseChartNumber,
		StatementsCalcDueDate,
		StatementShowCreditCard,
		///<summary>Show payment notes.</summary>
		StatementShowNotes,
		StatementShowAdjNotes,
		StatementShowProcBreakdown,
		StatementShowReturnAddress,
		StatementSummaryShowInsInfo,
		StoreCCnumbers,
		StoreCCtokens,
		SubscriberAllowChangeAlways,
		TaskAncestorsAllSetInVersion55,
		TaskListAlwaysShowsAtBottom,
		TasksCheckOnStartup,
		TasksNewTrackedByUser,
		TasksShowOpenTickets,
		TerminalClosePassword,
		TextMsgOkStatusTreatAsNo,
		///<summary>0=Sun,1=Mon...6=Sat</summary>
		TimeCardOvertimeFirstDayOfWeek,
		TimecardSecurityEnabled,
		TimeCardsMakesAdjustmentsForOverBreaks,
		///<summary>bool</summary>
		TimeCardsUseDecimalInsteadOfColon,
		TimecardUsersDontEditOwnCard,
		TitleBarShowSite,
		///<summary>Deprecated.  Not used anywhere.</summary>
		ToothChartMoveMenuToRight,
		TreatmentPlanNote,
		TreatPlanPriorityForDeclined,
		TreatPlanShowCompleted,
		TreatPlanShowGraphics,
		TreatPlanShowIns,
		TrojanExpressCollectBillingType,
		TrojanExpressCollectPassword,
		TrojanExpressCollectPath,
		TrojanExpressCollectPreviousFileNumber,
		UpdateCode,
		UpdateInProgressOnComputerName,
		///<summary>Described in the Update Setup window and in the manual.  Can contain multiple db names separated by commas.  Should not include current db name.</summary>
		UpdateMultipleDatabases,
		UpdateServerAddress,
		UpdateShowMsiButtons,
		UpdateWebProxyAddress,
		UpdateWebProxyPassword,
		UpdateWebProxyUserName,
		UpdateWebsitePath,
		UpdateWindowShowsClassicView,
		UseBillingAddressOnClaims,
		///<summary>Enum:ToothNumberingNomenclature 0=Universal(American), 1=FDI, 2=Haderup, 3=Palmer</summary>
		UseInternationalToothNumbers,
		///<summary>Only used for sheet synch.  See Mobile... for URL for mobile synch.</summary>
		WebHostSynchServerURL,
		WebServiceServerName,
		WordProcessorPath,
		XRayExposureLevel
	}

	///<summary>Used by pref "SearchBehavior". </summary>
	public enum SearchBehaviorCriteria {
		ProviderTime,
		ProviderTimeOperatory
	}

	///<summary>Used by pref "AccountingSoftware".  0=OpenDental, 1=QuickBooks</summary>
	public enum AccountingSoftware {
		OpenDental,
		QuickBooks
	}
	



}
