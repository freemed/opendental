package com.opendental.odweb.client.windows;

import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.event.dom.client.ClickHandler;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.DialogBox;

public class BasicTemplate extends DialogBox{
	public BasicTemplate(){
		Button butOK=new Button("OK");
		butOK.addClickHandler(new butOK_Click());
		Button butCancel=new Button("Cancel");
		butCancel.addClickHandler(new butCancel_Click());
	}
	
	private void Close(){
		this.hide();
	}
	
	private class butOK_Click implements ClickHandler {
		public void onClick(ClickEvent event) {
			Close();
		}
	}
	
	private class butCancel_Click implements ClickHandler {
		public void onClick(ClickEvent event) {
			Close();
		}
	}
}
