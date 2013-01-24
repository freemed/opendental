package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.RunAsyncCallback;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.TabLayoutPanel;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.ui.ModuleWidget;
import com.opendental.odweb.client.ui.ODGrid;

public class ContrChart extends ModuleWidget {
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static ContrChartUiBinder uiBinder=GWT.create(ContrChartUiBinder.class);
	interface ContrChartUiBinder extends UiBinder<Widget, ContrChart> {
	}
	
	@UiField SimplePanel panelContainer;
	@UiField TextBox textTreatmentNotes;
	@UiField(provided=true) ODGrid gridPtInfo;
	@UiField TabLayoutPanel tabProc;
	@UiField(provided=true) ODGrid gridProg;
	
	public ContrChart() {
		gridPtInfo=new ODGrid("Patient Info");
		gridPtInfo.setWidthAndHeight(400, 320);
		gridProg=new ODGrid("Progress Notes");
		gridProg.setWidthAndHeight(400, 300);
		uiBinder.createAndBindUi(this);
		this.add(panelContainer);
	}

	public Widget onInitialize() {
		return null;
	}
	
	protected void asyncOnInitialize(final AsyncCallback<Widget> callback) {
		GWT.runAsync(ContrChart.class, new RunAsyncCallback() {
		      public void onFailure(Throwable error) {
		        callback.onFailure(error);
		      }
		      public void onSuccess() {
		    	  callback.onSuccess(onInitialize());
		      }
		});
	}

}
