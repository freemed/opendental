package com.opendental.odweb.client.windows;

import com.google.gwt.core.client.GWT;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.uibinder.client.UiHandler;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.ui.ODWindow;

public class BasicTemplate extends ODWindow {
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static BasicTemplateUiBinder uiBinder=GWT.create(BasicTemplateUiBinder.class);
	interface BasicTemplateUiBinder extends UiBinder<Widget, BasicTemplate> {
	}
	
	@UiField Button butOK;
	@UiField Button butCancel;
	
	public BasicTemplate() {
		super("Basic Template");
		uiBinder.createAndBindUi(this);
	}
	
	@UiHandler("butOK")
	void butOK_Click(ClickEvent event) {
		this.hide();
	}
	
	@UiHandler("butCancel")
	void butCancel_Click(ClickEvent event) {
		this.hide();
	}
}
