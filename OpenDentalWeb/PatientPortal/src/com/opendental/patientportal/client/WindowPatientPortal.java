package com.opendental.patientportal.client;

import com.google.gwt.core.client.GWT;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.event.logical.shared.SelectionEvent;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.uibinder.client.UiHandler;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.SimpleLayoutPanel;
import com.google.gwt.user.client.ui.TabPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.opendentbusiness.tabletypes.Patient;

/** This class will contain the heart of the Patient Portal */
public class WindowPatientPortal extends Composite {
	private static WindowPatientPortalUiBinder uiBinder = GWT.create(WindowPatientPortalUiBinder.class);
	interface WindowPatientPortalUiBinder extends UiBinder<Widget, WindowPatientPortal> {
	}
	
	private Patient patCur;
	@UiField Label labelPatName;
	@UiField Button butLogOff;
	private LogOffHandler logOffHandler;
	@UiField TabPanel tabMain;
	@UiField SimpleLayoutPanel panelMedical;
//	private TabMedical tabMedical;
	
	public WindowPatientPortal(Patient pat,LogOffHandler handler) {
		logOffHandler=handler;
		patCur=pat;
		//Initialize the UI binder.
		initWidget(uiBinder.createAndBindUi(this));
		labelPatName.setText(patCur.LName+", "+patCur.FName);
		//Start with the Medical tab selected.
		tabMain.selectTab(2);
	}
	
	@UiHandler("tabMain")
	void tabMain_SelectionChange(SelectionEvent<Integer> event) {
		switch(event.getSelectedItem()) { 
			case 0:
				// TODO Make asynch call to get the family tab.
				break;
			case 1:
				// TODO Make asynch call to get the account tab.
				break;
			case 2:
				// TODO Make asynch call to get the medical tab.
//				GWT.runAsync(TabMedical.class,new RunAsyncCallback() {
//					public void onFailure(Throwable reason) {
//						// TODO Show the reason for the failure?
//					}
//					public void onSuccess() {
//						if(tabMedical==null) {
//							tabMedical=new TabMedical();
//						}
//						panelMedical.setWidget(tabMedical);
//					}
//				});
				break;
		}
	}
	
	@UiHandler("butLogOff")
	void butLogOff_Click(ClickEvent event) {
		logOffHandler.logOff();
	}
	
	/** logOff will get called when the user clicks the log off button. */
	public interface LogOffHandler {
		void logOff();
	}
	
}
