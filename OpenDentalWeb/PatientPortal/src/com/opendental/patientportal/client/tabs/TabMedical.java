package com.opendental.patientportal.client.tabs;

import com.google.gwt.core.client.GWT;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.SimpleLayoutPanel;
import com.google.gwt.user.client.ui.Widget;

public class TabMedical extends SimpleLayoutPanel {
	private static TabMedicalUiBinder uiBinder = GWT.create(TabMedicalUiBinder.class);
	interface TabMedicalUiBinder extends UiBinder<Widget, TabMedical> {
	}
	
	public TabMedical() {
		//Initialize the UI binder.
		uiBinder.createAndBindUi(this);
	}

}
