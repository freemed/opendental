package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.RunAsyncCallback;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.ListBox;
import com.google.gwt.user.client.ui.RadioButton;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.TabLayoutPanel;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.Widget;
import com.google.gwt.user.datepicker.client.DatePicker;
import com.opendental.odweb.client.ui.ModuleWidget;

public class ContrAppt extends ModuleWidget {
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static ContrApptUiBinder uiBinder=GWT.create(ContrApptUiBinder.class);
	interface ContrApptUiBinder extends UiBinder<Widget, ContrAppt> {
	}
	
	@UiField SimplePanel panelSchedule;
	@UiField DatePicker calendar;
	@UiField RadioButton radioDay;
	@UiField RadioButton radioWeek;
	@UiField ListBox comboView;
	@UiField Button butLab;
	@UiField TextBox textLab;
	@UiField TextBox textProduction;
	@UiField Button butUnsched;
	@UiField Button butBreak;
	@UiField Button butComplete;
	@UiField Button butDelete;
	@UiField ListBox listConfirmed;
	@UiField Button butMakeAppt;
	@UiField Button butMakeRecall;
	@UiField Button butFamRecall;
	@UiField Button butViewAppts;
	@UiField TabLayoutPanel tabControl;
	
	/** Constructor. */
	public ContrAppt() {
		uiBinder.createAndBindUi(this);
		panelSchedule.setSize("235 px", "500 px");
	}

	/** Loads up all of the information for the appointment module. */
	public Widget onInitialize() {
		return this;
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
