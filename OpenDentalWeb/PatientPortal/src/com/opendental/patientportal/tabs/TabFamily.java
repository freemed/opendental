package com.opendental.patientportal.tabs;

import com.google.gwt.core.client.GWT;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.user.client.ui.SimpleLayoutPanel;
import com.google.gwt.user.client.ui.Widget;

public class TabFamily extends SimpleLayoutPanel {
	private static TabFamilyUiBinder uiBinder = GWT.create(TabFamilyUiBinder.class);
	interface TabFamilyUiBinder extends UiBinder<Widget, TabFamily> {
	}
	
	public TabFamily() {
		//Initialize the UI binder.
		uiBinder.createAndBindUi(this);
	}

}
