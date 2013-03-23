package com.opendental.patientportal.client.tabs;

import com.google.gwt.core.client.GWT;
import com.google.gwt.i18n.client.NumberFormat;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.opendentbusiness.tabletypes.Patient;

public class TabAccount extends Composite {
	private static TabAccountUiBinder uiBinder = GWT.create(TabAccountUiBinder.class);
	interface TabAccountUiBinder extends UiBinder<Widget, TabAccount> {
	}

	private Patient patCur;
	@UiField VerticalPanel panelContainer;
	@UiField Grid gridAccount;
	@UiField Grid gridStatements;
	@UiField Label labelStatements;
	
	public TabAccount(Patient pat) {
		patCur=pat;
		//Initialize the UI binder.
		uiBinder.createAndBindUi(this);
		fillAccount();
		fillStatements();
		initWidget(panelContainer);
	}

	private void fillAccount() {
		NumberFormat formatString=NumberFormat.getFormat("#,##0.00");
		String balTotal="$"+formatString.format(patCur.BalTotal);
		String insEst="$"+formatString.format(patCur.InsEst);
		String afterIns="$"+formatString.format(patCur.BalTotal-patCur.InsEst);
		if(patCur.BalTotal<0) {
			balTotal="-"+balTotal;
		}
		else {
			balTotal=" "+balTotal;
		}
		if(patCur.InsEst<0) {
			insEst="-"+insEst;
		}
		else {
			insEst=" "+insEst;
		}
		if((patCur.BalTotal-patCur.InsEst)<0) {
			afterIns="-"+afterIns;
		}
		else {
			afterIns=" "+afterIns;
		}
		int rows=3;
		int columns=3;
		gridAccount.resize(rows, columns);
		gridAccount.setText(0, 0, "Balance");
		gridAccount.setText(0, 1, "=");
		gridAccount.setText(0, 2, balTotal);
		gridAccount.setText(1, 0, "Ins Pending");
		gridAccount.setText(1, 1, "=");
		gridAccount.setText(1, 2, insEst);
		gridAccount.setText(2, 0, "After Ins");
		gridAccount.setText(2, 1, "=");
		gridAccount.setText(2, 2, afterIns);
	}

	private void fillStatements() {
		// TODO Dynamically set the rows based on the number of 
		int rows=2;
		int columns=2;
		gridStatements.resize(rows, columns);
		for(int i=0;i<rows;i++) {
			if(i==0) {
				labelStatements.setVisible(false);
			}
			gridStatements.setText(i, 0, "Statement 10/14/2010");
			if(true) {// TODO Check if the pdf is available.
				// TODO Create a link so the patient can view the statement.  Look into adding a link into the cell.
				gridStatements.setText(i, 1, "View Statement");
			}
		}
	}

}
