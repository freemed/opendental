package com.opendental.patientportal.client;

import com.google.gwt.core.client.GWT;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.user.client.ui.ResizeComposite;
import com.google.gwt.user.client.ui.Widget;

public class WindowPatientPortal extends ResizeComposite {
	private static WindowPatientPortalUiBinder uiBinder = GWT.create(WindowPatientPortalUiBinder.class);
	interface WindowPatientPortalUiBinder extends UiBinder<Widget, WindowPatientPortal> {
	}
	
	public WindowPatientPortal() {
		//Initialize the UI binder.
		initWidget(uiBinder.createAndBindUi(this));
	}
	
}
