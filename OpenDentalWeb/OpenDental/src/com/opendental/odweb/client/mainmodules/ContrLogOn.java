package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.GWT;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.uibinder.client.UiHandler;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.ui.ModuleWidget;

public class ContrLogOn extends ModuleWidget {
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static ContrLogOnUiBinder uiBinder=GWT.create(ContrLogOnUiBinder.class);
	interface ContrLogOnUiBinder extends UiBinder<Widget, ContrLogOn> {
	}
	
	@UiField VerticalPanel panelContainer;
	@UiField TextBox textUser;
	@UiField TextBox textPassword;
	
	public ContrLogOn() {
		super("Log On");
		uiBinder.createAndBindUi(this);
		this.add(panelContainer);
	}

	@UiHandler("butOK")
	void butOK_Click(ClickEvent event) {
	}

	@Override
	public Widget onInitialize() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	protected void asyncOnInitialize(AsyncCallback<Widget> callback) {
		// TODO Auto-generated method stub
		
	}

}
