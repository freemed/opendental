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
import com.google.gwt.event.shared.HandlerRegistration;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.Command;
import com.google.gwt.user.client.ui.MenuItem;
import com.google.gwt.user.client.ui.ResizeComposite;
import com.google.gwt.user.client.ui.SimpleLayoutPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.datainterface.*;
import com.opendental.odweb.client.logic.PatientL;
import com.opendental.odweb.client.remoting.Db;
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
	/** Array list that contains the index of the selected module.  It might get enhanced to handle messaging buttons as well. */
	public ArrayList<Integer> selectedIndicies=new ArrayList<Integer>();
	/** The handler used to handle the user changing appointment views. */
	private HandlerRegistration apptViewSourceHandler;
	/** The panel that holds the content. */
	@UiField SimpleLayoutPanel contentPanel;
	/** The label towards the top of the page that displays the information regarding the currently selected patient. */
	@UiField LabelMainTitle labelMainTitle;
	/** The outlook bar on the left used to navigate to different modules.  (provided=true) Means we will instantiate the object ourselves.  
	*  This is because the OutlookBar class requires constructor args and I'm not comfortable with UiFactory or UiConstructor yet. */
	@UiField(provided=true) OutlookBar outlookBar;
	/** The main menu.  Holds options like Log Off, File, Setup, etc. */
	@UiField(provided=true) MenuBarMain mainMenu;
	/** The main tool bar.  Holds options like Select Patient, Commlog, etc. */
	@UiField MenuItem menuItemSelectPatient;
	@UiField MenuItem menuItemCommlog;
	/** The currently selected patient.  Can be null. */
	private Patient PatCur;
  
	public WindowOpenDental() {
		mainMenu=new MenuBarMain(this);
		outlookBar=new OutlookBar(new outlookBar_Click());
		//Initialize the UI binder.
		initWidget(uiBinder.createAndBindUi(this));
		//Attach commands to all of the menu items.
		menuItemSelectPatient.setCommand(new SelectPatient_Command());
		menuItemCommlog.setCommand(new Commlog_Command());
		//Default the module to null so that a nice Open Dental logo shows instead of wasting time loading a module the user might not be interested in.
		setModule(-1);
	}
	
	public Patient getPatCur() {
		return PatCur;
	}

	public void setPatCur(Patient patCur) {
		PatCur=patCur;
	}
	
	private void fillPatientButton(Patient pat) {
		if(pat==null) {
			pat=new Patient();
		}
		PatCur=pat;
		setMainTitle(PatCur);
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
	    //Clear the old handler.
	    if(apptViewSourceHandler!=null) {
	    	apptViewSourceHandler.removeHandler();
	    	apptViewSourceHandler=null;
	    }
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
		if(moduleCur==null) {
			//Disable all widgets.
			labelMainTitle.setMainTitle("");
			//Have a default Open Dental logo with welcome text.  This would save time loading in case the user does not need the appts module yet.
			moduleCur=new ContrLogOn();
		}
		contentPanel.setWidget(moduleCur);
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
					Db.sendRequest(Patients.getPat(windowPS.getSelectedPatNum()),new SelectPatientCallback());
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
