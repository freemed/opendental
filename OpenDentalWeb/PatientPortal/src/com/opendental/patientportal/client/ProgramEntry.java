package com.opendental.patientportal.client;

import com.google.gwt.core.client.EntryPoint;
import com.google.gwt.user.client.ui.RootLayoutPanel;

public class ProgramEntry implements EntryPoint {

	public void onModuleLoad() {
		//Add the shell to the root panel.  This makes the app visible.
		RootLayoutPanel.get().add(new WindowPatientPortal());
	}
	
	
}
