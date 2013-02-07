package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.RunAsyncCallback;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.ui.ModuleWidget;
import com.opendental.odweb.client.ui.ODGrid;

public class ContrTreatPlan extends ModuleWidget {
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static ContrTreatPlanUiBinder uiBinder=GWT.create(ContrTreatPlanUiBinder.class);
	interface ContrTreatPlanUiBinder extends UiBinder<Widget, ContrTreatPlan> {
	}
	
	@UiField SimplePanel panelContainer;
	@UiField(provided=true) ODGrid gridMain;
	
	public ContrTreatPlan() {
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
