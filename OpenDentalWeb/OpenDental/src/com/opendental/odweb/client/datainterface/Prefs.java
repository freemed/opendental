package com.opendental.odweb.client.datainterface;

import java.util.HashMap;

import com.opendental.odweb.client.data.DataTable;
import com.opendental.odweb.client.data.PIn;
import com.opendental.odweb.client.remoting.Db;
import com.opendental.odweb.client.remoting.DtoGetTable;
import com.opendental.odweb.client.remoting.Meth;
import com.opendental.odweb.client.remoting.Db.RequestCallbackResult;
import com.opendental.odweb.client.tabletypes.Pref;
import com.opendental.odweb.client.ui.MsgBox;

public class Prefs {
	//Cache----------------------------------------------------------------------------------------------------------------
	//The preference cache pattern is not the standard way to handle cache.  Do not use this pattern as a reference.
	private static HashMap<String,Pref> Dict;
	
	public static void refreshCache(RequestCallbackResult callback) {
		DtoGetTable dto=null;
		try {
			dto=Meth.getTable("Prefs.RefreshCache");
		}
		catch (Exception e) {
			MsgBox.show("Error:\r\n"+e.getMessage());
		}
		Db.sendRequest(dto.serialize(),callback);
	}
	
	public static void fillCache(DataTable table) {
		Dict=new HashMap<String,Pref>();
		Pref pref;
		for(int i=0;i<table.Rows.size();i++) {
			pref=new Pref();
			pref.PrefNum=PIn.Int(table.getCellText(i, "PrefNum"));
			pref.PrefName=PIn.String(table.getCellText(i, "PrefName"));
			pref.ValueString=PIn.String(table.getCellText(i, "ValueString"));
			Dict.put(pref.PrefName, pref);
		}
	}
	
	//PrefC----------------------------------------------------------------------------------------------------------------
	//This section of the preference cache will contain the methods that are in OpenDentBusiness\Cache\PrefC.cs
	
	public static long getLong(PrefName prefName) {
		if(!Dict.containsKey(prefName.toString())) {
			MsgBox.show("Unknown PrefName: "+prefName.toString());
		}
		return PIn.Long(Dict.get(prefName.toString()).ValueString);
	}
	
	public static int getInt(PrefName prefName) {
		if(!Dict.containsKey(prefName.toString())) {
			MsgBox.show("Unknown PrefName: "+prefName.toString());
		}
		return PIn.Int(Dict.get(prefName.toString()).ValueString);
	}
	
	public static double getDouble(PrefName prefName) {
		if(!Dict.containsKey(prefName.toString())) {
			MsgBox.show("Unknown PrefName: "+prefName.toString());
		}
		return PIn.Double(Dict.get(prefName.toString()).ValueString);
	}
	
	public static boolean getBool(PrefName prefName) {
		if(!Dict.containsKey(prefName.toString())) {
			MsgBox.show("Unknown PrefName: "+prefName.toString());
		}
		return PIn.Bool((Dict.get(prefName.toString()).ValueString));
	}
	
	public static String getString(PrefName prefName) {
		if(!Dict.containsKey(prefName.toString())) {
			MsgBox.show("Unknown PrefName: "+prefName.toString());
		}
		return PIn.String((Dict.get(prefName.toString()).ValueString));
	}	
	
	//Preference Callbacks-------------------------------------------------------------------------------------------------
	
		
	
	//Preference enums-----------------------------------------------------------------------------------------------------
	
	/** Because this enum is stored in the database as strings rather than as numbers, we can do the order alphabetically and we can change it whenever we want. */
	public enum PrefName {
		AccountingCashIncomeAccount,
		AccountingDepositAccounts,
		AccountingIncomeAccount,
		AccountingLockDate,
		/** Enum:AccountingSoftware 0=None, 1=Open Dental, 2=QuickBooks */
		AccountingSoftware,
		AccountShowPaymentNums,
		ADAComplianceDateTime,
		ADAdescriptionsReset,
		AgingCalculatedMonthlyInsteadOfDaily,
		/** FK to allergydef.AllergyDefNum */
		AllergiesIndicateNone,
		AllowedFeeSchedsAutomate,
		AllowSettingProcsComplete,
		AppointmentBubblesDisabled,
		/** Enum:SearchBehaviorCriteria 0=ProviderTime, 1=ProviderTimeOperatory */
		AppointmentSearchBehavior,
		AppointmentTimeArrivedTrigger,
		AppointmentTimeDismissedTrigger,
		AppointmentTimeIncrement,
		/** Set to true if appointment times are locked by default. */
		AppointmentTimeIsLocked,
		AppointmentTimeSeatedTrigger,
		ApptBubbleDelay,
		ApptExclamationShowForUnsentIns,
		ApptModuleRefreshesEveryMinute,
		/** Integer */
		ApptPrintColumnsPerPage,
		/** Float */
		ApptPrintFontSize,
		ApptPrintTimeStart,
		ApptPrintTimeStop,
		/** Deprecated, but must remain here to avoid breaking updates. */
		AtoZfolderNotRequired,
		/** Normally 1.  If this is set to 0, then that means images are being stored in the database.  This used to be called AtoZfolderNotRequired, but that name was confusing. */
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
		/** Value is a string, either Billing or Finance. */
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
		/** 0=no,1=EHG,2=POS(xml file) */
		BillingUseElectronic,
		BirthdayPostcardMsg,
		BrokenAppointmentAdjustmentType,
		BrokenApptCommLogNotAdjustment,
		/** This is the hash of the password that is needed to open the Central Manager tool. */
		CentralManagerPassHash,
		ChartQuickAddHideAmalgam,
		/** If set to true (1), then after adding a proc, a row will be added to datatable instead of rebuilding entire datatable by making queries to the database. */
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
		/** FK to definition.DefNum.  Initially 0. */
		ConfirmStatusEmailed,
		/** FK to definition.DefNum. */
		ConfirmStatusTextMessaged,
		/** The message that goes out to patients when doing a batch confirmation. */
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
		/** Boolean.  Set to 1 to indicate that this database holds customers instead of patients.  Used by OD HQ. */
		DistributorKey,
		DockPhonePanelShow,
		/** The AtoZ folder path. */
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
		/** This pref is hidden, so no practical way for user to turn this on.  Only used for ehr testing. */
		EHREmailToAddress,
		/** FK to EmailAddress.EmailAddressNum.  It is not required that a default be set. */
		EmailDefaultAddressNum,
		/** Deprecated. Use emailaddress.EmailPassword instead. */
		EmailPassword,
		/** Deprecated. Use emailaddress.ServerPort instead. */
		EmailPort,
		/** Deprecated. Use emailaddress.SenderAddress instead. */
		EmailSenderAddress,
		/** Deprecated. Use emailaddress.SMTPserver instead. */
		EmailSMTPserver,
		/** Deprecated. Use emailaddress.EmailUsername instead. */
		EmailUsername,
		/** Deprecated. Use emailaddress.UseSSL instead. */
		EmailUseSSL,
		/** Boolean. 0 means false and means it is not an EHR Emergency, and emergency access to the family module is not granted. */
		EhrEmergencyNow,
		/** There is no UI for this.  It's only used by OD HQ. */
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
		/** procedurelog.DiagnosticCode will be set to this for new procedures and complete procedures if this field was blank when set complete. */
		ICD9DefaultForNewProcs,
		ImagesModuleTreeIsCollapsed,
		ImageWindowingMax,
		ImageWindowingMin,
		/** 0=Default practice provider, -1=Treating Provider. Otherwise, FK to provider.ProvNum. */
		InsBillingProv,
		InsDefaultCobRule,
		InsDefaultPPOpercent,
		InsDefaultShowUCRonClaims,
		InsDefaultAssignBen,
		/** 0=unknown, user did not make a selection.  1=Yes, 2=No. */
		InsPlanConverstion_7_5_17_AutoMergeYN,
		InsurancePlansShared,
		IntermingleFamilyDefault,
		LabelPatientDefaultSheetDefNum,
		/** Comma-delimited list of two-letter language names and custom language names.  The custom language names are the full string name and are not necessarily supported by Microsoft. */
		LanguagesUsedByPatients,
		LetterMergePath,
		MainWindowTitle,
		/** New procs will use the fee amount tied to the medical code instead of the ADA code. */
		MedicalFeeUsedForNewProcs,
		/** FK to medication.MedicationNum */
		MedicationsIndicateNone,
		MobileSyncDateTimeLastRun,
		/** Used one time after the conversion to 7.9 for initial synch of the provider table. */
		MobileSynchNewTables79Done,
		/** Used one time after the conversion to 11.2 for re-synch of the patient records because a)2 columns BalTotal and InsEst have been added to the patientm table. b) the table documentm has been added */
		MobileSynchNewTables112Done,
		/** Used one time after the conversion to 12.1 for the recallm table being added and for upload of the practice Title. */
		MobileSynchNewTables121Done,
		MobileSyncIntervalMinutes,
		MobileSyncServerURL,
		MobileSyncWorkstationName,
		MobileExcludeApptsBeforeDate,
		MobileUserName,
		MySqlVersion,
		/** There is no UI for user to change this.  Format, if OD customer, is PatNum-(RandomString)(CheckSum).  Example: 1234-W6c43.  Format for resellers is up to them. */
		NewCropAccountId,
		/** There is no UI for user to change this. For resellers, this is part of the credentials.  OD credentials are not stored here, but are hard-coded. */
		NewCropName,
		/** There is no UI for user to change this. For resellers, this is part of the credentials.  OD credentials are not stored here, but are hard-coded. */
		NewCropPassword,
		OpenDentalVendor,
		OracleInsertId,
		PasswordsMustBeStrong,
		PatientSelectUsesSearchButton,
		PatientFormsShowConsent,
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
		/** In USA and Canada, enforced to be exactly 10 digits or blank. */
		PracticeFax,
		PracticePayToAddress,
		PracticePayToAddress2,
		PracticePayToCity,
		PracticePayToST,
		PracticePayToZip,
		/** In USA and Canada, enforced to be exactly 10 digits or blank. */
		PracticePhone,
		PracticeST,
		PracticeTitle,
		PracticeZip,
		/** FK to diseasedef.DiseaseDefNum */
		ProblemsIndicateNone,
		/** In FormProcCodes, this is the default for the ShowHidden checkbox. */
		ProcCodeListShowHidden,
		ProcLockingIsAllowed,
		ProcessSigsIntervalInSecs,
		ProcGroupNoteDoesAggregate,
		ProgramVersion,
		ProviderIncomeTransferShows,
		/** FK to sheet.SheetNum. Must be an exam sheet. Only makes sense if PublicHealthScreeningUsePat is true. */
		PublicHealthScreeningSheet,
		/** Boolean. Initially set to 0.  When in this mode, screenings will be attached to actual PatNums rather than just freeform text names. */
		PublicHealthScreeningUsePat,
		QuickBooksCompanyFile,
		QuickBooksDepositAccounts,
		QuickBooksIncomeAccount,
		RandomPrimaryKeys,
		RecallAdjustDown,
		RecallAdjustRight,
		/** Defaults to 12 for new customers.  The number in this field is considered adult.  Only used when automatically adding procedures to a new recall appointment. */
		RecallAgeAdult,
		RecallCardsShowReturnAdd,
		/** -1 indicates min for all dates */
		RecallDaysFuture,
		/** -1 indicates min for all dates */
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
		/** Used if younger than 12 on the recall date. */
		RecallTypeSpecialChildProphy,
		RecallTypeSpecialPerio,
		RecallTypeSpecialProphy,
		/** Comma-delimited list. FK to recalltype.RecallTypeNum. */
		RecallTypesShowingInList,
		/** If false, then it will only use email in the recall list if email is the preferred recall method. */
		RecallUseEmailIfHasEmailAddress,
		RegistrationKey,
		RegistrationKeyIsDisabled,
		RegistrationNumberClaim,
		RenaissanceLastBatchNumber,
		/** If replication has failed, this indicates the server_id.  No computer will be able to connect to this single server until this flag is cleared. */
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
		/** Set to 0 to always grant permission. 1 means only today. */
		SecurityLockDays,
		SecurityLockIncludesAdmin,
		/** Set to 0 to disable auto logoff. */
		SecurityLogOffAfterMinutes,
		SecurityLogOffWithWindows,
		ShowAccountFamilyCommEntries,
		ShowFeatureEhr,
		ShowFeatureMedicalInsurance,
		ShowFeatureSuperfamilies,
		/** 0=None, 1=PatNum, 2=ChartNumber, 3=Birthdate */
		ShowIDinTitleBar,
		ShowProgressNotesInsteadofCommLog,
		ShowUrgFinNoteInProgressNotes,
		SolidBlockouts,
		StatementAccountsUseChartNumber,
		StatementsCalcDueDate,
		StatementShowCreditCard,
		/** Show payment notes. */
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
		/** 0=Sun,1=Mon...6=Sat */
		TimeCardOvertimeFirstDayOfWeek,
		TimecardSecurityEnabled,
		TimeCardsMakesAdjustmentsForOverBreaks,
		/** bool */
		TimeCardsUseDecimalInsteadOfColon,
		TimecardUsersDontEditOwnCard,
		TitleBarShowSite,
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
		/** Described in the Update Setup window and in the manual.  Can contain multiple db names separated by commas.  Should not include current db name. */
		UpdateMultipleDatabases,
		UpdateServerAddress,
		UpdateShowMsiButtons,
		UpdateWebProxyAddress,
		UpdateWebProxyPassword,
		UpdateWebProxyUserName,
		UpdateWebsitePath,
		UpdateWindowShowsClassicView,
		UseBillingAddressOnClaims,
		/** Enum:ToothNumberingNomenclature 0=Universal(American), 1=FDI, 2=Haderup, 3=Palmer */
		UseInternationalToothNumbers,
		/** Only used for sheet synch.  See Mobile... for URL for mobile synch. */
		WebHostSynchServerURL,
		WebServiceServerName,
		WordProcessorPath,
		XRayExposureLevel
	}

	/** Used by pref "SearchBehavior".  */
	public enum SearchBehaviorCriteria {
		ProviderTime,
		ProviderTimeOperatory
	}

	/** Used by pref "AccountingSoftware".  0=OpenDental, 1=QuickBooks */
	public enum AccountingSoftware {
		OpenDental,
		QuickBooks
	}
	
	

}