package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.RunAsyncCallback;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.TabPanel;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.ui.ModuleWidget;
import com.opendental.odweb.client.ui.ODGrid;

public class ContrAccount extends ModuleWidget {
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static ContrAccountUiBinder uiBinder=GWT.create(ContrAccountUiBinder.class);
	interface ContrAccountUiBinder extends UiBinder<Widget, ContrAccount> {
	}
	
	@UiField SimplePanel panelContainer;
	@UiField(provided=true) ODGrid gridAccount;
	@UiField TabPanel tabControlShow;
	@UiField Label labelUrgFinNote;
	@UiField TextBox textUrgFinNote;
	@UiField(provided=true) ODGrid gridAcctPat;
	@UiField Label labelFamFinancial;
	@UiField TextBox textFinNotes;
	@UiField Button butPayment;
	@UiField Button butAdj;
	
	public ContrAccount() {
		//Instantiate all the grids.
		gridAccount=new ODGrid("Patient Account");
		gridAccount.setWidthAndHeight(700, 100);
		gridAcctPat=new ODGrid("Select Patient");
		gridAcctPat.setWidthAndHeight(200, 120);
		uiBinder.createAndBindUi(this);
		tabControlShow.selectTab(0);
		this.add(panelContainer);
	}

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
