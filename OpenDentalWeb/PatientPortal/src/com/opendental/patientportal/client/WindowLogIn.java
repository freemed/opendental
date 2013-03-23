package com.opendental.patientportal.client;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.Scheduler;
import com.google.gwt.core.client.Scheduler.ScheduledCommand;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.event.dom.client.KeyCodes;
import com.google.gwt.event.dom.client.KeyDownEvent;
import com.google.gwt.event.logical.shared.ValueChangeEvent;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.uibinder.client.UiHandler;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.PasswordTextBox;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.opendentbusiness.remoting.RequestHelper.RequestCallbackResult;
import com.opendental.opendentbusiness.tabletypes.Patient;
import com.opendental.patientportal.client.datainterface.Patients;

public class WindowLogIn extends Composite {
	private static WindowLogInlUiBinder uiBinder = GWT.create(WindowLogInlUiBinder.class);
	interface WindowLogInlUiBinder extends UiBinder<Widget, WindowLogIn> {
	}
	
	@UiField VerticalPanel panelContainer;
	@UiField TextBox textUserName;
	@UiField Label labelUserName;
	@UiField PasswordTextBox textPassword;
	@UiField Label labelPassword;
	@UiField Button butLogIn;
	@UiField Label labelLogIn;
	private LogInHandler logOnHandler;
	
	public WindowLogIn(LogInHandler handler) {
		logOnHandler=handler;
		//Initialize the UI binder.
		uiBinder.createAndBindUi(this);
		initWidget(panelContainer);
		//Start with focus on the user name text box.
		Scheduler.get().scheduleDeferred(new ScheduledCommand() {
			public void execute() {
				textUserName.setFocus(true);
			}
		});
	}
	
	/** Validates the user name and password text boxes then checks the credentials on the database. */
	private void logIn() {
		if(textUserName.getText().equals("")) {
			labelUserName.setVisible(true);
			textUserName.setFocus(true);
			return;
		}
		if(textPassword.getText().equals("")) {
			labelPassword.setVisible(true);
			textPassword.setFocus(true);
			return;
		}
		Patients.getOnePatientPortal(textUserName.getText(), textPassword.getText(), new getOnePatientPortal_Callback());
	}
	
	@UiHandler("textUserName")
	void textUserName_ValueChanged(ValueChangeEvent<String> event) {
		//Hide the warning labels.
		labelUserName.setVisible(false);
		labelLogIn.setVisible(false);
	}

	@UiHandler("textUserName")
	public void textUserName_KeyDown(KeyDownEvent event) {
		if(event.getNativeKeyCode()==KeyCodes.KEY_ENTER) {
			logIn();
		}
	}
	
	@UiHandler("textPassword")
	void textPassword_ValueChanged(ValueChangeEvent<String> event) {
		//Hide the warning labels.
		labelPassword.setVisible(false);
		labelLogIn.setVisible(false);
	}

	@UiHandler("textPassword")
	public void textPassword_KeyDown(KeyDownEvent event) {
		if(event.getNativeKeyCode()==KeyCodes.KEY_ENTER) {
			logIn();
		}
	}

	@UiHandler("butLogIn")
	void butLogIn_Click(ClickEvent event) {
		logIn();
	}
	
	/** A patient object will be returned if the log in was successful.  Null if patient credentials were invalid. */
	private class getOnePatientPortal_Callback implements RequestCallbackResult {
		public void onSuccess(Object obj) {
			if(obj==null) {
				labelLogIn.setVisible(true);
				return;
			}
			//User credentials are correct so load up the Patient Portal widget with the patient that just logged in.
			logOnHandler.onSuccess((Patient)obj);
		}
		
		public void onError(String error) {
			MsgBox.show(error);
		}
		
	}
	
	/** onSuccess will get called and will contain the patient object when a patient enters valid credentials */
	public interface LogInHandler {
		void onSuccess(Patient patCur);
	}


}
