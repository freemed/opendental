package com.opendental.patientportal.client;

import com.google.gwt.core.client.GWT;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.ui.ResizeComposite;
import com.google.gwt.user.client.ui.SimpleLayoutPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.opendentbusiness.tabletypes.Patient;
import com.opendental.patientportal.client.WindowLogIn.LogInHandler;
import com.opendental.patientportal.client.WindowPatientPortal.LogOffHandler;

/** This class acts as a content container.  It basically directs traffic and navigates through the widgets when users log on and off. */
public class WindowContent extends ResizeComposite {
	private static WindowContentlUiBinder uiBinder = GWT.create(WindowContentlUiBinder.class);
	interface WindowContentlUiBinder extends UiBinder<Widget, WindowContent> {
	}
	
	/** The panel that holds the content. */
	@UiField SimpleLayoutPanel contentPanel;
	
	public WindowContent() {
		//Initialize the UI binder.
		initWidget(uiBinder.createAndBindUi(this));
		//For now we always load the page to the Log In widget.
		contentPanel.setWidget(new WindowLogIn(new PatientLoggedIn()));
	}
	
	/** Sets the content panel to show the passed in widget. */
	public void setContent(Widget widget) {
		contentPanel.setWidget(widget);
	}
	
	private class PatientLoggedIn implements LogInHandler {
		/** Called when a patient has successfully logged into the patient portal.  The patient portal widget will be loaded into the contentPanel. */
		public void onSuccess(Patient patCur) {
			setContent(new WindowPatientPortal(patCur,new PatientLoggedOff()));
		}
	}
	
	private class PatientLoggedOff implements LogOffHandler {
		/** Called when the user logs off.  The log in widget will be loaded into the contentPanel. */
		public void logOff() {
			setContent(new WindowLogIn(new PatientLoggedIn()));
		}
	}
	
}
