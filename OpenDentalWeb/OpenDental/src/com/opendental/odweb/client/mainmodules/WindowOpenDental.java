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
import com.google.gwt.user.client.ui.ResizeComposite;
import com.google.gwt.user.client.ui.SimpleLayoutPanel;
import com.google.gwt.user.client.ui.Widget;
import com.google.gwt.view.client.SelectionChangeEvent;
import com.google.gwt.view.client.SingleSelectionModel;
import com.opendental.odweb.client.ui.ModuleWidget;
import com.opendental.odweb.client.usercontrols.*;

/** This is where the shell of the Open Dental Web App lives. */
public class WindowOpenDental extends ResizeComposite {
	private static WindowOpenDentalUiBinder uiBinder = GWT.create(WindowOpenDentalUiBinder.class);
	interface WindowOpenDentalUiBinder extends UiBinder<Widget, WindowOpenDental> {
	}
	
	/** The current {@link ModuleWidget} being displayed. */
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
	/** The outlook bar on the left used to navigate to different modules.  (provided=true) Means we will instantiate the object ourselves.  
	 *  This is because the OutlookBar class requires constructor args and I'm not comfortable with UiFactory or UiConstructor yet. */
  @UiField(provided=true) OutlookBar outlookBar;
  /** The main menu.  Holds options like Log Off, File, Setup, etc. */
  @UiField MenuBarMain mainMenu;
  /** The main tool bar.  Holds options like Select Patient, Commlog, etc. */
  @UiField MenuBarMainPatient mainToolBar;
  
	public WindowOpenDental() {
		final SingleSelectionModel<OutlookButton> selectionModel=new SingleSelectionModel<OutlookButton>();
		outlookBar=new OutlookBar(selectionModel);
		//Create an event handler for when users click between modules.
		selectionModel.addSelectionChangeHandler(new SelectionChangeEvent.Handler() {
      public void onSelectionChange(SelectionChangeEvent event) {
        setModule(selectionModel.getSelectedObject().getButtonIndex());
      }
    });
    //Initialize the UI binder.
    initWidget(uiBinder.createAndBindUi(this));  
    //Default the module to null so that a nice Open Dental logo shows instead of wasting time loading a module the user might not be interested in.
    setModule(-1);
	}
	
	/** Sets the module to display depending on the index of the buttons.  Pass -1 to treat clear out the modules.  This will be used for loading the app and when a user logs off.
   * @param index The index of the module that needs to be displayed. */
  public void setModule(int index) {
    //Clear the old handler.
    if(apptViewSourceHandler!=null) {
    	apptViewSourceHandler.removeHandler();
    	apptViewSourceHandler=null;
    }
    moduleCur=getModuleAtIndex(index);
    if(moduleCur==null) {
    	//This is where we can disable the tool bar buttons and such when no patient is selected or the user logs off.
      contentPanel.setWidget(null);
      return;
    }
    // TODO Setup the main tool bar here.
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
			//Disable all the widgets?
			//Have a default Open Dental logo with welcom text.  This would save time loading in case the user does not need the appts module yet.
			return;
		}
		contentPanel.setWidget(moduleCur);
	}
	
	
	
}
