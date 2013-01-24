package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.RunAsyncCallback;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.CheckBox;
import com.google.gwt.user.client.ui.ListBox;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.ui.ModuleWidget;
import com.opendental.odweb.client.ui.ODGrid;

public class ContrTreatPlan extends ModuleWidget {
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static ContrTreatPlanUiBinder uiBinder=GWT.create(ContrTreatPlanUiBinder.class);
	interface ContrTreatPlanUiBinder extends UiBinder<Widget, ContrTreatPlan> {
	}
	
	@UiField SimplePanel panelContainer;
	@UiField(provided=true) ODGrid gridPlans;
	@UiField(provided=true) ODGrid gridPreAuth;
	@UiField(provided=true) ODGrid gridMain;
	@UiField CheckBox checkShowCompleted;
	@UiField CheckBox checkShowMaxDed;
	@UiField CheckBox checkShowFees;
	@UiField CheckBox checkShowIns;
	@UiField CheckBox checkShowDiscount;
	@UiField CheckBox checkShowSubtotals;
	@UiField CheckBox checkShowTotals;
	@UiField ListBox listSetPr;
	@UiField TextBox textNote;
	@UiField TextBox textFamPriMax;
	@UiField TextBox textFamSecMax;
	@UiField TextBox textFamPriDed;
	@UiField TextBox textFamSecDed;
	@UiField TextBox textPriMax;
	@UiField TextBox textSecMax;
	@UiField TextBox textPriDed;
	@UiField TextBox textSecDed;
	@UiField TextBox textPriDedRem;
	@UiField TextBox textSecDedRem;
	@UiField TextBox textPriUsed;
	@UiField TextBox textSecUsed;
	@UiField TextBox textPriPend;
	@UiField TextBox textSecPend;
	@UiField TextBox textPriRem;
	@UiField TextBox textSecRem;
	
	public ContrTreatPlan() {
		gridPlans=new ODGrid("Treatment Plans");
		gridPlans.setWidthAndHeight(400, 100);
		gridPreAuth=new ODGrid("Pre Authorizations");
		gridPreAuth.setWidthAndHeight(250, 100);
		gridMain=new ODGrid("Procedures");
		gridMain.setWidthAndHeight(750, 450);
		uiBinder.createAndBindUi(this);
		this.add(panelContainer);
	}

	public Widget onInitialize() {
		return null;
	}
	
	protected void asyncOnInitialize(final AsyncCallback<Widget> callback) {
		GWT.runAsync(ContrTreatPlan.class, new RunAsyncCallback() {
		      public void onFailure(Throwable error) {
		        callback.onFailure(error);
		      }
		      public void onSuccess() {
		    	  callback.onSuccess(onInitialize());
		      }
		});
	}
}
