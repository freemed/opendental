package com.opendental.odweb.client.usercontrols;

import com.google.gwt.user.client.Command;
import com.google.gwt.user.client.ui.MenuBar;
import com.google.gwt.user.client.ui.MenuItem;
import com.google.gwt.user.client.ui.MenuItemSeparator;
import com.opendental.odweb.client.mainmodules.WindowOpenDental;

public class MenuBarMain extends MenuBar{
	private WindowOpenDental WindowOD;
	
	public MenuBarMain(WindowOpenDental windowOD) {
		WindowOD=windowOD;
		MenuItem menuLogOff = new MenuItem("Log Off", false, new Command() {
			public void execute() {
				WindowOD.setModule(-1);
			}
		});
		this.addItem(menuLogOff);
		
		MenuBar subMenuFile=new MenuBar(true);
		MenuItem menuFile = new MenuItem("File", false, subMenuFile);
		AddSubMenusFile(subMenuFile);
		this.addItem(menuFile);
		
		MenuBar subMenuSetup=new MenuBar(true);
		MenuItem menuSetup = new MenuItem("Setup", false, subMenuSetup);
		AddSubMenusSetup(subMenuSetup);
		this.addItem(menuSetup);
		
		MenuBar subMenuLists=new MenuBar(true);
		MenuItem menuLists = new MenuItem("Lists", false, subMenuLists);
		AddSubMenusLists(subMenuLists);
		this.addItem(menuLists);
		
		MenuItem menuReports = new MenuItem("Reports", false, (Command) null);
		this.addItem(menuReports);
		
		MenuBar subMenuTools=new MenuBar(true);
		MenuItem menuTools = new MenuItem("Tools", false, subMenuTools);
		AddSubMenusTools(subMenuTools);
		this.addItem(menuTools);

		MenuBar subMenuHelp = new MenuBar(true);
		MenuItem menuHelp = new MenuItem("Help", false, subMenuHelp);
		AddSubMenusHelp(subMenuHelp);
		this.addItem(menuHelp);		
	}
	
	private void AddSubMenusFile(MenuBar subMenuFile){
		MenuItem menuItemPassword = new MenuItem("Change Password", false, (Command) null);
		subMenuFile.addItem(menuItemPassword);
		
		MenuItem mntmExit = new MenuItem("Exit", false, (Command) null);
		subMenuFile.addItem(mntmExit);
	}
	
	private void AddSubMenusSetup(MenuBar subMenuSetup){
		MenuItem menuApptFieldDefs = new MenuItem("Appt Field Defs", false, (Command) null);
		subMenuSetup.addItem(menuApptFieldDefs);
		
		MenuItem menuItemApptRules = new MenuItem("Appt Rules", false, (Command) null);
		subMenuSetup.addItem(menuItemApptRules);
		
		MenuItem menuItemAutoCodes = new MenuItem("Auto Codes", false, (Command) null);
		subMenuSetup.addItem(menuItemAutoCodes);
		
		MenuItem menuItemAutomation = new MenuItem("Automation", false, (Command) null);
		subMenuSetup.addItem(menuItemAutomation);
		
		MenuItem menuItemAutoNotes = new MenuItem("Auto Notes", false, (Command) null);
		subMenuSetup.addItem(menuItemAutoNotes);
		
		MenuItem menuItemClearinghouses = new MenuItem("Clearinghouses", false, (Command) null);
		subMenuSetup.addItem(menuItemClearinghouses);
		
		MenuItem menuItemDataPath = new MenuItem("Data Paths", false, (Command) null);
		subMenuSetup.addItem(menuItemDataPath);
		
		MenuItem menuItemDefinitions = new MenuItem("Definitions", false, (Command) null);
		subMenuSetup.addItem(menuItemDefinitions);
		
		MenuItem menuItemDisplayFields = new MenuItem("Display Fields", false, (Command) null);
		subMenuSetup.addItem(menuItemDisplayFields);
		
		MenuItem menuItemFeeScheds = new MenuItem("Fee Schedules", false, (Command) null);
		subMenuSetup.addItem(menuItemFeeScheds);
		
		MenuItem menuItemInsCats = new MenuItem("Insurance Categories", false, (Command) null);
		subMenuSetup.addItem(menuItemInsCats);
		
		MenuItem menuItemInsFilingCodes = new MenuItem("Insurance Filing Codes", false, (Command) null);
		subMenuSetup.addItem(menuItemInsFilingCodes);
		
		MenuItem menuItemLaboratories = new MenuItem("Laboratories", false, (Command) null);
		subMenuSetup.addItem(menuItemLaboratories);
		
		MenuItem menuItemMisc = new MenuItem("Miscellaneous", false, (Command) null);
		subMenuSetup.addItem(menuItemMisc);
		
		MenuItem menuItemModules = new MenuItem("Modules", false, (Command) null);
		subMenuSetup.addItem(menuItemModules);
		
		MenuItem menuItemOperatories = new MenuItem("Operatories", false, (Command) null);
		subMenuSetup.addItem(menuItemOperatories);
		
		MenuItem menuItemPatFieldDefs = new MenuItem("Pat Field Defs", false, (Command) null);
		subMenuSetup.addItem(menuItemPatFieldDefs);
		
		MenuItem menuItemPayerIDs = new MenuItem("Payer IDs", false, (Command) null);
		subMenuSetup.addItem(menuItemPayerIDs);
		
		MenuItem menuItemPractice = new MenuItem("Practice", false, (Command) null);
		subMenuSetup.addItem(menuItemPractice);
		
		MenuItem menuItemProblems = new MenuItem("Problems", false, (Command) null);
		subMenuSetup.addItem(menuItemProblems);
		
		MenuItem menuItemProcedureButtons = new MenuItem("Procedure Buttons", false, (Command) null);
		subMenuSetup.addItem(menuItemProcedureButtons);
		
		MenuItem menuItemLinks = new MenuItem("Program Links", false, (Command) null);
		subMenuSetup.addItem(menuItemLinks);
		
		MenuItem menuItemQuestions = new MenuItem("Questionnaire", false, (Command) null);
		subMenuSetup.addItem(menuItemQuestions);
		
		MenuItem menuItemRecall = new MenuItem("Recall", false, (Command) null);
		subMenuSetup.addItem(menuItemRecall);
		
		MenuItem menuItemRecallTypes = new MenuItem("RecallTypes", false, (Command) null);
		subMenuSetup.addItem(menuItemRecallTypes);
		
		MenuItem menuItemSecurity = new MenuItem("Security", false, (Command) null);
		subMenuSetup.addItem(menuItemSecurity);
		
		MenuItem menuItemSched = new MenuItem("Schedules", false, (Command) null);
		subMenuSetup.addItem(menuItemSched);
		
		MenuItem menuItemShowFeatures = new MenuItem("Show Features", false, (Command) null);
		subMenuSetup.addItem(menuItemShowFeatures);
		
		MenuItem menuItemTimeCards = new MenuItem("Time Cards", false, (Command) null);
		subMenuSetup.addItem(menuItemTimeCards);
	}
	
	private void AddSubMenusLists(MenuBar subMenuLists){
		MenuItem menuItemProcCodes = new MenuItem("Procedure Codes", false, (Command) null);
		subMenuLists.addItem(menuItemProcCodes);
		
		MenuItemSeparator separator = new MenuItemSeparator();
		subMenuLists.addSeparator(separator);
		
		MenuItem menuItemClinics = new MenuItem("Clinics", false, (Command) null);
		subMenuLists.addItem(menuItemClinics);
		
		MenuItem menuItemContacts = new MenuItem("Contacts", false, (Command) null);
		subMenuLists.addItem(menuItemContacts);
		
		MenuItem menuItemCounties = new MenuItem("Counties", false, (Command) null);
		subMenuLists.addItem(menuItemCounties);
		
		MenuItem menuItemEmployees = new MenuItem("Employees", false, (Command) null);
		subMenuLists.addItem(menuItemEmployees);
		
		MenuItem menuItemEmployers = new MenuItem("Employers", false, (Command) null);
		subMenuLists.addItem(menuItemEmployers);
		
		MenuItem menuItemCarriers = new MenuItem("Insurance Carriers", false, (Command) null);
		subMenuLists.addItem(menuItemCarriers);
		
		MenuItem menuItemInsPlans = new MenuItem("Insurance Plans", false, (Command) null);
		subMenuLists.addItem(menuItemInsPlans);
		
		MenuItem menuItemLabCases = new MenuItem("Lab Cases", false, (Command) null);
		subMenuLists.addItem(menuItemLabCases);
		
		MenuItem menuItemMedications = new MenuItem("Medications", false, (Command) null);
		subMenuLists.addItem(menuItemMedications);
		
		MenuItem menuItemPharmacies = new MenuItem("Pharmacies", false, (Command) null);
		subMenuLists.addItem(menuItemPharmacies);
		
		MenuItem menuItemProviders = new MenuItem("Providers", false, (Command) null);
		subMenuLists.addItem(menuItemProviders);
		
		MenuItem menuItemPrescriptions = new MenuItem("Prescriptions", false, (Command) null);
		subMenuLists.addItem(menuItemPrescriptions);
		
		MenuItem menuItemReferrals = new MenuItem("Referrals", false, (Command) null);
		subMenuLists.addItem(menuItemReferrals);
		
		MenuItem menuItemSchools = new MenuItem("Sites", false, (Command) null);
		subMenuLists.addItem(menuItemSchools);
		
		MenuItem menuItemZipCodes = new MenuItem("Zip Codes", false, (Command) null);
		subMenuLists.addItem(menuItemZipCodes);
	}
	
	private void AddSubMenusTools(MenuBar subMenuTools){
		MenuBar menuBarMiscTools = new MenuBar(true);
		
		MenuItem subMenuMiscTools = new MenuItem("Misc Tools", false, menuBarMiscTools);
		
		MenuItem menuItemTelephone = new MenuItem("Telephone Numbers", false, (Command) null);
		menuBarMiscTools.addItem(menuItemTelephone);
		
		MenuItem menuItemMergePatients = new MenuItem("Merge Patients", false, (Command) null);
		menuBarMiscTools.addItem(menuItemMergePatients);
		
		MenuItem menuItemDuplicateBlockouts = new MenuItem("Clear Duplicate Blackouts", false, (Command) null);
		menuBarMiscTools.addItem(menuItemDuplicateBlockouts);
		
		MenuItem menuItemShutdown = new MenuItem("Shutdown All Workstations", false, (Command) null);
		menuBarMiscTools.addItem(menuItemShutdown);
		subMenuTools.addItem(subMenuMiscTools);
		
		MenuItemSeparator separator_1 = new MenuItemSeparator();
		subMenuTools.addSeparator(separator_1);
		
		MenuItem menuItemAuditTrail = new MenuItem("Audit Trail", false, (Command) null);
		subMenuTools.addItem(menuItemAuditTrail);
	}
	
	private void AddSubMenusHelp(MenuBar subMenuHelp){
		MenuItem menuItemRemote = new MenuItem("Online Support", false, (Command) null);
		subMenuHelp.addItem(menuItemRemote);
		
		MenuItem menuItemHelpContents = new MenuItem("Online Help - Contents", false, (Command) null);
		subMenuHelp.addItem(menuItemHelpContents);
		
		MenuItem menuItemHelpIndex = new MenuItem("Online Help - Index", false, (Command) null);
		subMenuHelp.addItem(menuItemHelpIndex);
		
		MenuItem menuItemRequestFeatures = new MenuItem("Request Features", false, (Command) null);
		subMenuHelp.addItem(menuItemRequestFeatures);
		
		MenuItem menuItemUpdate = new MenuItem("Update", false, (Command) null);
		subMenuHelp.addItem(menuItemUpdate);
	}

}
