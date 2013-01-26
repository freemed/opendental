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
import com.google.gwt.user.client.ui.RadioButton;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.TabPanel;
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
	@UiField TabPanel tabProc;
	@UiField(provided=true) ODGrid gridProg;
	@UiField TextBox textSurf;
	@UiField Button butBF;
	@UiField Button butV;
	@UiField Button butM;
	@UiField Button butOI;
	@UiField Button butD;
	@UiField Button butL;
	@UiField CheckBox checkToday;
	@UiField TextBox textDate;
	@UiField RadioButton radioEntryTP;
	@UiField RadioButton radioEntryC;
	@UiField RadioButton radioEntryEC;
	@UiField RadioButton radioEntryEO;
	@UiField RadioButton radioEntryR;
	@UiField RadioButton radioEntryCn;
	@UiField Label labelDx;
	@UiField ListBox listDx;
	@UiField Label labelPrognosis;
	@UiField ListBox listPrognosis;
	@UiField Label labelPriority;
	@UiField ListBox listPriority;
	@UiField Button butAddProc;
	@UiField ListBox listButtonCats;
	@UiField TextBox textProcCode;
	@UiField Button butOK;
	@UiField Button butCMO;
	@UiField Button butCMOD;
	@UiField Button butCO;
	@UiField Button butCDO;
	@UiField Button butCSeal;
	@UiField Button butCOL;
	@UiField Button butCOB;
	@UiField Button butCMODL;
	@UiField Button butCMODB;
	@UiField Button butDL;
	@UiField Button butMDL;
	@UiField Button butML;
	@UiField Button butAMO;
	@UiField Button butAMOD;
	@UiField Button butAO;
	@UiField Button butADO;
	@UiField Button butAOL;
	@UiField Button butAOB;
	@UiField Button butAMODL;
	@UiField Button butAMODB;
	
	public ContrChart() {
		gridPtInfo=new ODGrid("Patient Info");
		gridPtInfo.setWidthAndHeight(400, 320);
		gridProg=new ODGrid("Progress Notes");
		gridProg.setWidthAndHeight(400, 300);
		uiBinder.createAndBindUi(this);
		tabProc.selectTab(0);
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
