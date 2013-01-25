package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.RunAsyncCallback;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.CheckBox;
import com.google.gwt.user.client.ui.DecoratorPanel;
import com.google.gwt.user.client.ui.HorizontalPanel;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.ListBox;
import com.google.gwt.user.client.ui.RadioButton;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.TabPanel;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.VerticalPanel;
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
	private TextBox textSurf;
	private Button butBF;
	private Button butV;
	private Button butM;
	private Button butOI;
	private Button butD;
	private Button butL;
	private CheckBox checkToday;
	private TextBox textDate;
	private RadioButton radioEntryTP;
	private RadioButton radioEntryC;
	private RadioButton radioEntryEC;
	private RadioButton radioEntryEO;
	private RadioButton radioEntryR;
	private RadioButton radioEntryCn;
	private Label labelDx;
	private ListBox listDx;
	private Label labelPrognosis;
	private ListBox listPrognosis;
	private Label labelPriority;
	private ListBox listPriority;
	private Button butAddProc;
	private ListBox listButtonCats;
	private TextBox textProcCode;
	private Button butOK;
	private Button butCMO;
	private Button butCMOD;
	private Button butCO;
	private Button butCDO;
	private Button butCSeal;
	private Button butCOL;
	private Button butCOB;
	private Button butCMODL;
	private Button butCMODB;
	private Button butDL;
	private Button butMDL;
	private Button butML;
	private Button butAMO;
	private Button butAMOD;
	private Button butAO;
	private Button butADO;
	private Button butAOL;
	private Button butAOB;
	private Button butAMODL;
	private Button butAMODB;
	
	public ContrChart() {
		gridPtInfo=new ODGrid("Patient Info");
		gridPtInfo.setWidthAndHeight(400, 320);
		gridProg=new ODGrid("Progress Notes");
		gridProg.setWidthAndHeight(400, 300);
		uiBinder.createAndBindUi(this);
		fillTabProc();
		this.add(panelContainer);
	}
	
	//Tab proc--------------------------------------------------------------------------------------------------------------------------------
	
	private void fillTabProc() {
		tabProc.add(getTabEnterTx(),"Enter Treatment");
		tabProc.add(getTabMissing(),"Missing Teeth");
		tabProc.add(getTabMovements(),"Movements");
		tabProc.add(getTabPrimary(),"Primary");
		tabProc.add(getTabPlanned(),"Planned Appts");
		tabProc.add(getTabShow(),"Show");
		tabProc.add(getTabDraw(),"Draw");
		tabProc.add(getTabPatInfo(),"Pat Info");
		tabProc.selectTab(0);
	}
	
	private Widget getTabEnterTx() {
		HorizontalPanel hp=new HorizontalPanel();
		//Status---------------------------------------------------------------------
		VerticalPanel vpStatus=new VerticalPanel();
		textSurf=new TextBox();
		HorizontalPanel hpBFV=new HorizontalPanel();
		butBF=new Button("B/F");
		butV=new Button("V");
		hpBFV.add(butBF);
		hpBFV.add(butV);
		HorizontalPanel hpMOID=new HorizontalPanel();
		butM=new Button("M");
		butOI=new Button("O/I");
		butD=new Button("D");
		butL=new Button("L");
		hpMOID.add(butM);
		hpMOID.add(butOI);
		hpMOID.add(butD);
		DecoratorPanel dp=new DecoratorPanel();
		VerticalPanel vDecPanel=new VerticalPanel();
		vDecPanel.add(new Label("Entry Status"));
		radioEntryTP=new RadioButton("Status","Treat Plan");
		radioEntryC=new RadioButton("Status","Complete");
		radioEntryEC=new RadioButton("Status","ExistCurProv");
		radioEntryEO=new RadioButton("Status","ExistOther");
		radioEntryR=new RadioButton("Status","Referred");
		radioEntryCn=new RadioButton("Status","Condition");
		vDecPanel.add(radioEntryTP);
		vDecPanel.add(radioEntryC);
		vDecPanel.add(radioEntryEC);
		vDecPanel.add(radioEntryEO);
		vDecPanel.add(radioEntryR);
		vDecPanel.add(radioEntryCn);
		dp.add(vDecPanel);
		checkToday=new CheckBox("Today");
		textDate=new TextBox();
		vpStatus.add(textSurf);
		vpStatus.add(hpBFV);
		vpStatus.add(hpMOID);
		vpStatus.add(butL);
		vpStatus.add(dp);
		vpStatus.add(checkToday);
		vpStatus.add(textDate);
		//Diagnosis------------------------------------------------------------------
		VerticalPanel vpDiag=new VerticalPanel();
		labelDx=new Label("Diagnosis");
		listDx=new ListBox(true);
		listDx.setWidth("100px");
		listDx.setHeight("150px");
		labelPrognosis=new Label("Prognosis");
		listPrognosis=new ListBox();
		labelPriority=new Label("Priority");
		listPriority=new ListBox();
		vpDiag.add(labelDx);
		vpDiag.add(listDx);
		vpDiag.add(labelPrognosis);
		vpDiag.add(listPrognosis);
		vpDiag.add(labelPriority);
		vpDiag.add(listPriority);
		//Button cats----------------------------------------------------------------
		VerticalPanel vpButCats=new VerticalPanel();
		butAddProc=new Button("Procedure List");
		listButtonCats=new ListBox(true);
		listButtonCats.setWidth("150px");
		listButtonCats.setHeight("240px");
		vpButCats.add(butAddProc);
		vpButCats.add(listButtonCats);
		//Quick buttons--------------------------------------------------------------
		VerticalPanel vpSurf=new VerticalPanel();
		HorizontalPanel hpProcCode=new HorizontalPanel();
		Label labelOr=new Label("Or");
		textProcCode=new TextBox();
		textProcCode.setText("Type Proc Code");
		butOK=new Button("OK");
		hpProcCode.add(labelOr);
		hpProcCode.add(textProcCode);
		hpProcCode.add(butOK);
		Label labelOrSingle=new Label("Or Single Click:");
		DecoratorPanel dpProcs=new DecoratorPanel();
		VerticalPanel vpQuickButtons=new VerticalPanel();
		Label labelPstComp=new Label("Post. Composite");
		HorizontalPanel hpCM=new HorizontalPanel();
		butCMO=new Button("MO");
		butCMOD=new Button("MOD");
		butCO=new Button("O");
		butCDO=new Button("DO");
		butCSeal=new Button("Seal");
		hpCM.add(butCMO);
		hpCM.add(butCMOD);
		hpCM.add(butCO);
		hpCM.add(butCDO);
		hpCM.add(butCSeal);
		HorizontalPanel hpCO=new HorizontalPanel();
		butCOL=new Button("OL");
		butCOB=new Button("OB");
		butCMODL=new Button("MODL");
		butCMODB=new Button("MODB");	
		hpCO.add(butCOL);
		hpCO.add(butCOB);
		hpCO.add(butCMODL);
		hpCO.add(butCMODB);
		Label labelAntComp=new Label("Ant. Composite");
		HorizontalPanel hpD=new HorizontalPanel();
		butDL=new Button("DL");
		butMDL=new Button("MDL");
		butML=new Button("ML");
		hpD.add(butDL);
		hpD.add(butMDL);
		hpD.add(butML);
		VerticalPanel vpAmalgam=new VerticalPanel();
		Label labelAmalgam=new Label("Amalgam");
		HorizontalPanel hpAM=new HorizontalPanel();
		butAMO=new Button("MO");
		butAMOD=new Button("MOD");
		butAO=new Button("O");
		butADO=new Button("DO");
		hpAM.add(butAMO);
		hpAM.add(butAMOD);
		hpAM.add(butAO);
		hpAM.add(butADO);
		HorizontalPanel hpAO=new HorizontalPanel();
		butAOL=new Button("OL");
		butAOB=new Button("OB");
		butAMODL=new Button("MODL");
		butAMODB=new Button("MODB");
		hpAO.add(butAOL);
		hpAO.add(butAOB);
		hpAO.add(butAMODL);
		hpAO.add(butAMODB);
		vpAmalgam.add(labelAmalgam);
		vpAmalgam.add(hpAM);
		vpAmalgam.add(hpAO);
		vpQuickButtons.add(labelPstComp);
		vpQuickButtons.add(hpCM);
		vpQuickButtons.add(hpCO);
		vpQuickButtons.add(labelAntComp);
		vpQuickButtons.add(hpD);
		vpQuickButtons.add(vpAmalgam);
		dpProcs.add(vpQuickButtons);
		vpSurf.add(hpProcCode);
		vpSurf.add(labelOrSingle);
		vpSurf.add(dpProcs);
		hp.add(vpStatus);
		hp.add(vpDiag);
		hp.add(vpButCats);
		hp.add(vpSurf);
		return hp;
	}

	private Widget getTabMissing() {
		return new Label("Missing Teeth");
	}

	private Widget getTabMovements() {
		return new Label("Movements");
	}

	private Widget getTabPrimary() {
		return new Label("Primary");
	}

	private Widget getTabPlanned() {
		return new Label("Planned Appointments");
	}

	private Widget getTabShow() {
		return new Label("Show");
	}

	private Widget getTabDraw() {
		return new Label("Draw");
	}

	private Widget getTabPatInfo() {
		return new Label("Patient Information");
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
