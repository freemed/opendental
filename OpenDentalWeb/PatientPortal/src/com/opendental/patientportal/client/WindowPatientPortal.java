package com.opendental.patientportal.client;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.RunAsyncCallback;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.event.logical.shared.SelectionEvent;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.uibinder.client.UiHandler;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.TabPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.opendentbusiness.tabletypes.Patient;
import com.opendental.patientportal.client.tabs.TabAccount;
import com.opendental.patientportal.client.tabs.TabFamily;
import com.opendental.patientportal.client.tabs.TabMedical;

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
	@UiField SimplePanel panelFamily;
	@UiField SimplePanel panelAccount;
	@UiField SimplePanel panelMedical;
	private TabAccount tabAccount;
	private TabFamily tabFamily;
	private TabMedical tabMedical;
	
	public WindowPatientPortal(Patient pat,LogOffHandler handler) {
		logOffHandler=handler;
		patCur=pat;
		//Initialize the UI binder.
		initWidget(uiBinder.createAndBindUi(this));
		labelPatName.setText(patCur.LName+", "+patCur.FName);
		//Start with the Medical tab showing.
		tabMain.selectTab(2);
	}
	
	@UiHandler("tabMain")
	void tabMain_SelectionChange(SelectionEvent<Integer> event) {
		//Each tab is surrounded with an AsyncCallback which will download the javascript code when needed.  This saves on loading times and calls to the database.
		switch(event.getSelectedItem()) { 
			case 0://Family tab
				GWT.runAsync(new RunAsyncCallback() {
					public void onFailure(Throwable reason) {
						MsgBox.show("Failed to download the family tab:\r\n"+reason.getMessage());
					}
					public void onSuccess() {
						if(tabFamily==null) {
							tabFamily=new TabFamily(patCur);
						}
						panelFamily.setWidget(tabFamily);
					}
				});
				break;
			case 1://Account tab
				GWT.runAsync(new RunAsyncCallback() {
					public void onFailure(Throwable reason) {
						MsgBox.show("Failed to download the account tab:\r\n"+reason.getMessage());
					}
					public void onSuccess() {
						if(tabAccount==null) {
							tabAccount=new TabAccount(patCur);
						}
						panelAccount.setWidget(tabAccount);
					}
				});
				break;
			case 2://Medical tab
				GWT.runAsync(new RunAsyncCallback() {
					public void onFailure(Throwable reason) {
						MsgBox.show("Failed to download the medical tab:\r\n"+reason.getMessage());
					}
					public void onSuccess() {
						if(tabMedical==null) {
							tabMedical=new TabMedical(patCur);
						}
						panelMedical.setWidget(tabMedical);
					}
				});
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
