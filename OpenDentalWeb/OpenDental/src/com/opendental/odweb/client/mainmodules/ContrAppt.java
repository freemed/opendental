package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.RunAsyncCallback;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.Widget;
import com.google.gwt.user.datepicker.client.DatePicker;
import com.opendental.odweb.client.ui.ModuleWidget;

public class ContrAppt extends ModuleWidget {
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static ContrApptUiBinder uiBinder=GWT.create(ContrApptUiBinder.class);
	interface ContrApptUiBinder extends UiBinder<Widget, ContrAppt> {
	}
	
	@UiField SimplePanel panelContainer;
	@UiField SimplePanel panelSchedule;
	@UiField DatePicker calendar;
	@UiField Label pinBoard;
	@UiField Button butDelete;
	@UiField Button butMakeAppt;
	
	public ContrAppt() {
		uiBinder.createAndBindUi(this);
		this.add(panelContainer);
	}

	/** Loads up all of the information for the appointment module. */
	public Widget onInitialize() {
		return null;
	}

	protected void asyncOnInitialize(final AsyncCallback<Widget> callback) {
		GWT.runAsync(ContrAppt.class, new RunAsyncCallback() {
			public void onFailure(Throwable error) {
				callback.onFailure(error);
			}
			public void onSuccess() {
				callback.onSuccess(onInitialize());
			}
		});
	}
}
