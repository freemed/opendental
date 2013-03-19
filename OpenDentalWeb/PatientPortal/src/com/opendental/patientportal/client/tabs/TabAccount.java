package com.opendental.patientportal.client.tabs;

import com.google.gwt.core.client.GWT;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.Widget;

public class TabAccount extends Composite {
	private static TabAccountUiBinder uiBinder = GWT.create(TabAccountUiBinder.class);
	interface TabAccountUiBinder extends UiBinder<Widget, TabAccount> {
	}
	
	public TabAccount() {
		//Initialize the UI binder.
		initWidget(uiBinder.createAndBindUi(this));
	}

}
