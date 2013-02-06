/*=============================================================================================================
Open Dental's Web Application is a dental practice management web app.
Copyright (C) 2012 Jordan Sparks, DMD.  http://www.opendental.com

This program is free software; you can redistribute it and/or modify it under the terms of the
GNU Db Public License as published by the Free Software Foundation; either version 2 of the License,
or (at your option) any later version.

This program is distributed in the hope that it will be useful, but without any warranty. See the GNU Db Public License
for more details, available at http://www.opensource.org/licenses/gpl-license.php

Any changes to this program must follow the guidelines of the GPL license if a modified version is to be
redistributed.

Web app programmer: Jason Salmon
===============================================================================================================*/

package com.opendental.odweb.client.mainmodules;

import java.util.ArrayList;

import com.google.gwt.core.client.GWT;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.Command;
import com.google.gwt.user.client.ui.MenuItem;
import com.google.gwt.user.client.ui.ResizeComposite;
import com.google.gwt.user.client.ui.SimpleLayoutPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.datainterface.*;
import com.opendental.odweb.client.logic.PatientL;
import com.opendental.odweb.client.mainmodules.ContrLogOn.LogOnHandler;
import com.opendental.odweb.client.remoting.Db.RequestCallbackResult;
import com.opendental.odweb.client.tabletypes.*;
import com.opendental.odweb.client.ui.DialogResultCallbackOkCancel;
import com.opendental.odweb.client.ui.ModuleWidget;
import com.opendental.odweb.client.usercontrols.*;
import com.opendental.odweb.client.usercontrols.OutlookBar.OutlookBarClickHandler;
import com.opendental.odweb.client.windows.WindowPatientSelect;

/** This is where the shell of the Open Dental Web App lives. */
public class WindowOpenDental extends ResizeComposite {
	private static WindowOpenDentalUiBinder uiBinder = GWT.create(WindowOpenDentalUiBinder.class);
	interface WindowOpenDentalUiBinder extends UiBinder<Widget, WindowOpenDental> {
	}
	
	/** The current ModuleWidget being displayed. */
	private ModuleWidget moduleCur;
	//Have a variable for each module so that we don't have to talk to the database for modules we have already loaded.
	private ModuleWidget contrAppt;
	private ModuleWidget contrFamily;
	private ModuleWidget contrAccount;
	private ModuleWidget contrTreatPlan;
	private ModuleWidget contrChart;
	private ModuleWidget contrImages;
	private ModuleWidget contrManage;
	/** The panel that holds the content. */
	@UiField SimpleLayoutPanel contentPanel;
	/** The label towards the top of the page that displays the information regarding the currently selected patient. */
	@UiField LabelMainTitle labelMainTitle;
	/** The outlook bar on the left used to navigate to different modules.  (provided=true) Means we will instantiate the object ourselves.  
	*  This is because the OutlookBar class requires constructor args and I'm not comfortable with UiFactory or UiConstructor yet. */
	@UiField(provided=true) OutlookBar outlookBar;
	/** The currently selected patient.  Can be null. */
	private Patient patCur;
	//Main menu bar----------------------------------------------
	//Log Off
	@UiField MenuItem rootMenuItemLogOff;
	//File
	@UiField MenuItem rootMenuItemFile;
	@UiField MenuItem menuItemPassword;
	@UiField MenuItem menuItemExit;
	//Setup
	@UiField MenuItem rootMenuItemSetup;
	@UiField MenuItem menuItemApptFieldDefs;
	@UiField MenuItem menuItemApptRules;
	@UiField MenuItem menuItemAutoCodes;
	@UiField MenuItem menuItemAutomation;
	@UiField MenuItem menuItemAutoNotes;
	@UiField MenuItem menuItemClearinghouses;
	@UiField MenuItem menuItemDataPath;
	@UiField MenuItem menuItemDefinitions;
	@UiField MenuItem menuItemDisplayFields;
	@UiField MenuItem menuItemFeeScheds;
	@UiField MenuItem menuItemInsCats;
	@UiField MenuItem menuItemInsFilingCodes;
	@UiField MenuItem menuItemLaboratories;
	@UiField MenuItem menuItemMisc;
	@UiField MenuItem menuItemModules;
	@UiField MenuItem menuItemOperatories;
	@UiField MenuItem menuItemPatFieldDefs;
	@UiField MenuItem menuItemPayerIDs;
	@UiField MenuItem menuItemPractice;
	@UiField MenuItem menuItemProblems;
	@UiField MenuItem menuItemProcedureButtons;
	@UiField MenuItem menuItemLinks;
	@UiField MenuItem menuItemQuestions;
	@UiField MenuItem menuItemRecall;
	@UiField MenuItem menuItemRecallTypes;
	@UiField MenuItem menuItemSecurity;
	@UiField MenuItem menuItemSched;
	@UiField MenuItem menuItemShowFeatures;
	@UiField MenuItem menuItemTimeCards;
	//Lists
	@UiField MenuItem rootMenuItemLists;
	@UiField MenuItem menuItemClinics;
	@UiField MenuItem menuItemContacts;
	@UiField MenuItem menuItemCounties;
	@UiField MenuItem menuItemEmployees;
	@UiField MenuItem menuItemEmployers;
	@UiField MenuItem menuItemCarriers;
	@UiField MenuItem menuItemInsPlans;
	@UiField MenuItem menuItemLabCases;
	@UiField MenuItem menuItemMedications;
	@UiField MenuItem menuItemPharmacies;
	@UiField MenuItem menuItemProviders;
	@UiField MenuItem menuItemPrescriptions;
	@UiField MenuItem menuItemReferrals;
	@UiField MenuItem menuItemSchools;
	@UiField MenuItem menuItemZipCodes;
	//Reports
	@UiField MenuItem rootMenuItemReports;
	//Tools
	@UiField MenuItem rootMenuItemTools;
	@UiField MenuItem menuItemPrintScreen;
	//Misc Tools
	@UiField MenuItem menuItemTelephone;
	@UiField MenuItem menuItemMergePatients;
	@UiField MenuItem menuItemDuplicateBlockouts;
	@UiField MenuItem menuItemShutdown;
	//End Misc Tools
	@UiField MenuItem menuItemAging;
	@UiField MenuItem menuItemAuditTrail;
	@UiField MenuItem menuItemBillingFinance;
	@UiField MenuItem menuItemDatabaseMaint;
	@UiField MenuItem menuItemMobileSynch;
	@UiField MenuItem menuItemRepeatingCharges;
	@UiField MenuItem menuItemWebForms;
	//Help
	@UiField MenuItem rootMenuItemHelp;
	@UiField MenuItem menuItemRemote;
	@UiField MenuItem menuItemHelpContents;
	@UiField MenuItem menuItemHelpIndex;
	@UiField MenuItem menuItemRequestFeatures;
	@UiField MenuItem menuItemUpdate;
	//The main tool bar----------------------------------------------------------
	@UiField MenuItem menuItemSelectPatient;
	@UiField MenuItem menuItemCommlog;
	
  
	public WindowOpenDental() {
		outlookBar=new OutlookBar(new outlookBar_Click());
		//Initialize the UI binder.
		initWidget(uiBinder.createAndBindUi(this));
		//Attach command handlers to all of the menu items.
		//Main menu commands---------------------------------------------------------------
		rootMenuItemLogOff.setCommand(new LogOff_Command());
		//ToolBar commands-----------------------------------------------------------------
		menuItemSelectPatient.setCommand(new SelectPatient_Command());
		menuItemCommlog.setCommand(new Commlog_Command());
		//Default the module to null so that a nice Open Dental logo shows instead of wasting time loading a module the user might not be interested in.
		setModule(-1);
	}
	
	private class LogOnUser implements LogOnHandler {
		public void onSuccess(Userod user) {
			// TODO Update the title bar with the database name and the newly logged in user.
			outlookBar.getButtonAtIndex(0).clickButton();
		}
	}
	
	public Patient getPatCur() {
		return patCur;
	}

	public void setPatCur(Patient pat) {
		patCur=pat;
	}
	
	private void fillPatientButton(Patient pat) {
		if(pat==null) {
			pat=new Patient();
		}
		patCur=pat;
		setMainTitle(patCur);
	}

	private void setMainTitle(Patient pat) {
		labelMainTitle.setText(PatientL.getMainTitle(pat));
	}
	
	private class outlookBar_Click implements OutlookBarClickHandler {
		public void onClick(ArrayList<Integer> selectedIndices) {
			for(int i=0;i<selectedIndices.size();i++) {
				//For now we only support modules being selected.  This should be enhanced to handle messaging buttons as well.
				setModule(selectedIndices.get(i));
			}
		}
	}
	
	/** Sets the module to display depending on the index of the buttons.  Pass -1 to treat clear out the modules.  This will be used for loading the app and when a user logs off.
	 *  @param index The index of the module that needs to be displayed. */
	public void setModule(int index) {
		moduleCur=getModuleAtIndex(index);
		showModule();
	}

  /** Gets the corresponding module in regards to the selected index of the module buttons. */
	private ModuleWidget getModuleAtIndex(int index) {
		switch(index) {
			case -1:
				return null;
			case 0:
				if(contrAppt==null) {
					contrAppt=new ContrAppt();
				}					
				return contrAppt;
			case 1:
				if(contrFamily==null) {
					contrFamily=new ContrFamily();
				}
				return contrFamily;
			case 2:
				if(contrAccount==null) {
					contrAccount=new ContrAccount();
				}
				return contrAccount;
			case 3:
				if(contrTreatPlan==null) {
					contrTreatPlan=new ContrTreatPlan();
				}
				return contrTreatPlan;
			case 4:
				if(contrChart==null) {
					contrChart=new ContrChart();
				}
				return contrChart;
			case 5:
				if(contrImages==null) {
					contrImages=new ContrImages();
				}
				return contrImages;
			case 6:
				if(contrManage==null) {
					contrManage=new ContrManage();
				}
				return contrManage;
		}
		// TODO Handle messaging buttons here?
		return null;
	}

	/** Sets the visible module in the content panel to moduleCur.  OK if moduleCur is null. */
	private void showModule() {
		setMenuItemsEnabled(true);
		setModuleButtonsEnabled(true);
		if(moduleCur==null) {
			disableWidgets();
			moduleCur=new ContrLogOn(new LogOnUser());
		}
		contentPanel.setWidget(moduleCur);
	}
	
	/** Typically called when the user logs off.  This will clear out the title bar, disable the menu bars and disable the module buttons. */
	private void disableWidgets() {
		labelMainTitle.setMainTitle("");
		setMenuItemsEnabled(false);
		setModuleButtonsEnabled(false);
	}

	/** Enables or disables all menu items. */
	private void setMenuItemsEnabled(boolean enabled) {
		if(enabled && rootMenuItemLogOff.isEnabled()
				|| !enabled && !rootMenuItemLogOff.isEnabled()) 
		{
			//Menu items are already enabled OR disabled so simply return.
			return;
		}
		//Main menu
		rootMenuItemLogOff.setEnabled(enabled);
		rootMenuItemFile.setEnabled(enabled);
		rootMenuItemSetup.setEnabled(enabled);
		rootMenuItemLists.setEnabled(enabled);
		rootMenuItemReports.setEnabled(enabled);
		rootMenuItemTools.setEnabled(enabled);
		rootMenuItemHelp.setEnabled(enabled);
		//Tool bar
		menuItemSelectPatient.setEnabled(enabled);
		menuItemCommlog.setEnabled(enabled);
	}
	
	private void setModuleButtonsEnabled(boolean enabled) {
		if(enabled && outlookBar.isEnabled()
				|| !enabled && !outlookBar.isEnabled()) 
		{
			//Menu items are already enabled OR disabled so simply return.
			return;
		}
		if(!enabled) {
			outlookBar.unselectMainModules();
		}
		outlookBar.setEnabled(enabled);
	}
	
	private void logOffNow() {
		setModule(-1);
	}
	
	//MainMenuCommands------------------------------------------------------------------------------------------------------------------------
	
	private class LogOff_Command implements Command {
		public void execute() {
			logOffNow();
		}
	}
	
	//ToolBarCommands-------------------------------------------------------------------------------------------------------------------------
	
	private class SelectPatient_Command implements Command {
		public void execute() {
			final WindowPatientSelect windowPS=new WindowPatientSelect();
			windowPS.show();
			windowPS.center();
			//Add a DialogResultCallback to listen for the dialog result.
			windowPS.DialogResultCallback=new DialogResultCallbackOkCancel() {
				public void OK() {
					Patients.getPat(windowPS.getSelectedPatNum(),new SelectPatientCallback());
				}
				public void Cancel() {
				}
			};
		}
	}
	
	private class SelectPatientCallback implements RequestCallbackResult {
		public void onSuccess(Object obj) {
			fillPatientButton((Patient)obj);
		}
	}
	
	private class Commlog_Command implements Command {
		public void execute() {
		}
	}
	
}
