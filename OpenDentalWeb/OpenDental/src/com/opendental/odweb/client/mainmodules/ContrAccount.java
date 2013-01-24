package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.RunAsyncCallback;
import com.google.gwt.dom.client.Style.FontWeight;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.CheckBox;
import com.google.gwt.user.client.ui.HorizontalPanel;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.TabPanel;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.ui.ModuleWidget;
import com.opendental.odweb.client.ui.ODGrid;

public class ContrAccount extends ModuleWidget {
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static ContrAccountUiBinder uiBinder=GWT.create(ContrAccountUiBinder.class);
	interface ContrAccountUiBinder extends UiBinder<Widget, ContrAccount> {
	}
	
	@UiField SimplePanel panelContainer;
	@UiField(provided=true) ODGrid gridRepeat;
	@UiField(provided=true) ODGrid gridPayPlan;
	@UiField(provided=true) ODGrid gridAccount;
	@UiField(provided=true) ODGrid gridComm;
	@UiField(provided=true) ODGrid gridProg;
	@UiField TabPanel tabControlShow;
	@UiField Button butTrojan;
	@UiField Button butComm;
	private Label labelUrgFinNote;
	private TextBox textUrgFinNote;
	private Button butCreditCard;
	private ODGrid gridAcctPat;
	private Label labelFamFinancial;
	private TextBox textFinNotes;
	private Button butToday;
	private Button but45days;
	private Button but90days;
	private Button butDatesAll;
	private Button butRefresh;
	private CheckBox checkShowDetail;
	private CheckBox checkShowFamilyComm;
	private Label labelStartDate;
	private TextBox textDateStart;
	private Label labelEndDate;
	private TextBox textDateEnd;
	
	public ContrAccount() {
		//Instantiate all the grids.
		gridRepeat=new ODGrid("Repeating Charges");
		gridRepeat.setWidthAndHeight(700, 75);
		gridPayPlan=new ODGrid("Payment Plans");
		gridPayPlan.setWidthAndHeight(700, 100);
		gridAccount=new ODGrid("Patient Account");
		gridAccount.setWidthAndHeight(700, 100);
		gridComm=new ODGrid("Communications Log");
		gridComm.setWidthAndHeight(700, 100);
		gridProg=new ODGrid("Progress Notes");
		gridProg.setWidthAndHeight(700, 100);
		uiBinder.createAndBindUi(this);
		fillTabControlShow();
		this.add(panelContainer);
	}
	
	private void fillTabControlShow() {
		tabControlShow.add(getTabMain(),"Main");
		tabControlShow.add(getTabShow(),"Show");
		//Select the first tab.
		tabControlShow.selectTab(0);
	}
	
	private Widget getTabMain() {
		VerticalPanel vp=new VerticalPanel();
		labelUrgFinNote=new Label("Fam Urgent Fin Note");
		labelUrgFinNote.getElement().getStyle().setFontWeight(FontWeight.BOLD);
		textUrgFinNote=new TextBox();
		textUrgFinNote.setSize("170px", "75px");
		butCreditCard=new Button("Credit Card Manage");
		gridAcctPat=new ODGrid("Select Patient");
		gridAcctPat.setWidthAndHeight(200, 120);
		fillPats();
		labelFamFinancial=new Label("Family Financial Notes");
		textFinNotes=new TextBox();
		textFinNotes.setSize("170px", "85px");
		vp.add(labelUrgFinNote);
		vp.add(textUrgFinNote);
		vp.add(butCreditCard);
		vp.add(gridAcctPat);
		vp.add(labelFamFinancial);
		vp.add(textFinNotes);
		return vp;
	}

	private Widget getTabShow() {
		VerticalPanel vp=new VerticalPanel();
		HorizontalPanel hpStart=new HorizontalPanel();
		labelStartDate=new Label("Start Date");
		textDateStart=new TextBox();
		hpStart.add(labelStartDate);
		hpStart.add(textDateStart);
		HorizontalPanel hpEnd=new HorizontalPanel();
		labelEndDate=new Label("End Date");
		textDateEnd=new TextBox();
		hpEnd.add(labelEndDate);
		hpEnd.add(textDateEnd);
		butToday=new Button();
		butToday.setText("Today");
		but45days=new Button();
		but45days.setText("Last 45 Days");
		but90days=new Button();
		but90days.setText("Last 90 Days");
		butDatesAll=new Button();
		butDatesAll.setText("All Dates");
		butRefresh=new Button();
		butRefresh.setText("Refresh");
		checkShowDetail=new CheckBox("Show Proc Breakdowns");
		checkShowDetail.setValue(true);
		checkShowFamilyComm=new CheckBox("Show Family Comm Entries");
		vp.add(hpStart);
		vp.add(hpEnd);
		vp.add(butToday);
		vp.add(but45days);
		vp.add(but90days);
		vp.add(butDatesAll);
		vp.add(butRefresh);
		vp.add(checkShowDetail);
		vp.add(checkShowFamilyComm);
		return vp;
	}
	
	private void fillPats() {
		
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
