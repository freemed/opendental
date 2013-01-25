package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.RunAsyncCallback;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.CheckBox;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.ListBox;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.ui.ModuleWidget;
import com.opendental.odweb.client.ui.ODGrid;

public class ContrManage extends ModuleWidget {
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static ContrManageUiBinder uiBinder=GWT.create(ContrManageUiBinder.class);
	interface ContrManageUiBinder extends UiBinder<Widget, ContrManage> {
	}
	
	@UiField SimplePanel panelContainer;
	@UiField Button butSendClaims;
	@UiField Button butClaimPay;
	@UiField Button butBilling;
	@UiField Button butDeposit;
	@UiField Button butSupply;
	@UiField Button butTasks;
	@UiField Button butSendRx;
	@UiField Button butBackup;
	@UiField Button butAccounting;
	@UiField(provided=true) ODGrid gridEmp;
	@UiField Button butManage;
	@UiField Button butTimeCard;
	@UiField Button butBreaks;
	@UiField Label labelTime;
	@UiField Button butClockIn;
	@UiField Button butClockOut;
	@UiField ListBox listStatus;
	@UiField ListBox listTo;
	@UiField ListBox listFrom;
	@UiField ListBox listExtras;
	@UiField ListBox listTMessages;	
	@UiField TextBox textMessage;
	@UiField Label labelSending;
	@UiField Button butSend;
	@UiField CheckBox checkIncludeAck;
	@UiField TextBox textDays;
	@UiField Button butAck;
	@UiField ListBox listViewUser;
	@UiField(provided=true) ODGrid gridMessages;
	
	public ContrManage() {
		gridEmp=new ODGrid("Employee");
		gridEmp.setWidthAndHeight(300, 200);
		gridMessages=new ODGrid("Message History");
		gridMessages.setWidthAndHeight(500, 400);
		uiBinder.createAndBindUi(this);
		this.add(panelContainer);
	}

	public Widget onInitialize() {
		return null;
	}
	
	protected void asyncOnInitialize(final AsyncCallback<Widget> callback) {
		GWT.runAsync(ContrManage.class, new RunAsyncCallback() {
		      public void onFailure(Throwable error) {
		        callback.onFailure(error);
		      }
		      public void onSuccess() {
		    	  callback.onSuccess(onInitialize());
		      }
		});
	}
}
