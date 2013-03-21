package com.opendental.patientportal.client;

import com.google.gwt.core.client.GWT;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.uibinder.client.UiHandler;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.PasswordTextBox;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.opendentbusiness.remoting.RequestHelper.RequestCallbackResult;
import com.opendental.opendentbusiness.tabletypes.Patient;
import com.opendental.patientportal.client.datainterface.Patients;

public class WindowLogIn extends Composite {
	private static WindowLogInlUiBinder uiBinder = GWT.create(WindowLogInlUiBinder.class);
	interface WindowLogInlUiBinder extends UiBinder<Widget, WindowLogIn> {
	}
	
	@UiField TextBox textUserName;
	@UiField PasswordTextBox textPassword;
	@UiField Button butLogIn;
	private LogInHandler logOnHandler;
	
	public WindowLogIn(LogInHandler handler) {
		logOnHandler=handler;
		//Initialize the UI binder.
		initWidget(uiBinder.createAndBindUi(this));
	}

	@UiHandler("butLogIn")
	void butLogIn_Click(ClickEvent event) {
		// TODO Validate the text boxes and show messages if there are errors.
		// TODO Verify the credentials.  Returns null patient if the credentials are not valid. 
		// TODO Show a message that lets the user know the credentials are invalid and do nothing.
		// The method should return a patient object if the credentials are valid that we will pass on.
		Patients.getOnePatientPortal(textUserName.getText(), textPassword.getText(), new getOnePatientPortal_Callback());
	}
	
	private class getOnePatientPortal_Callback implements RequestCallbackResult {
		public void onSuccess(Object obj) {
			if(obj==null) {
				// TODO Show login failure message.
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
