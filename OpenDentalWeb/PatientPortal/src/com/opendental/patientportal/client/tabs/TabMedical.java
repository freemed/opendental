package com.opendental.patientportal.client.tabs;

import com.google.gwt.core.client.GWT;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;

public class TabMedical extends Composite {
	private static TabMedicalUiBinder uiBinder = GWT.create(TabMedicalUiBinder.class);
	interface TabMedicalUiBinder extends UiBinder<Widget, TabMedical> {
	}
	
	@UiField VerticalPanel panelContainer;
	@UiField Grid gridPanels;
	@UiField Grid gridMedications;
	@UiField Grid gridProblems;
	@UiField Grid gridAllergies;
	@UiField Label labelPanels;
	@UiField Label labelMedications;
	@UiField Label labelProblems;
	@UiField Label labelAllergies;
	
	public TabMedical() {
		//Initialize the UI binder.
		uiBinder.createAndBindUi(this);
		fillPanels();
		fillMedications();
		fillProblems();
		fillAllergies();
		initWidget(panelContainer);
	}

	private void fillPanels() {
		int rows=1+1;//+1 for the column headers.
		int columns=2;
		gridPanels.resize(rows, columns);
		for(int i=0;i<rows;i++) {
			if(i==0) {
				labelPanels.setVisible(false);
				gridPanels.setText(0, 0, "Lab Name & Address");
				gridPanels.setText(0, 1, "Lab Results");
				continue;
			}
			gridPanels.setText(i, 0, "Lab 1 Name and Address");
			if(true) {// TODO Enhance to determine if results exist for the related lab.
				Grid gridResults=new Grid(4,3);// TODO Dynamically determine the number of rows.
				for(int j=0;j<4;j++) {
					if(j==0) {
						//Set the headers.
						gridResults.setText(0, 0, "Date Time");
						gridResults.setText(0, 1, "Test Name");
						gridResults.setText(0, 2, "Observed Value");
						continue;
					}
					gridResults.setText(j, 0, "3/19/2013 1:37:29 PM");
					gridResults.setText(j, 1, "Performed Name");
					gridResults.setText(j, 2, "Result Value");
				}
				gridPanels.setWidget(i, 1, gridResults);
			}
		}
	}

	private void fillMedications() {
		int rows=1+1;//+1 for the column headers.
		int columns=1;
		gridMedications.resize(rows, columns);
		for(int i=0;i<rows;i++) {
			if(i==0) {
				labelMedications.setVisible(false);
				gridMedications.setText(0, 0, "Medication Name");
				continue;
			}
			gridMedications.setText(i, 0, "Amlodipine");
		}
	}

	private void fillProblems() {
		int rows=1+1;//+1 for the column headers.
		int columns=1;
		gridProblems.resize(rows, columns);
		for(int i=0;i<rows;i++) {
			if(i==0) {
				labelProblems.setVisible(false);
				gridProblems.setText(0, 0, "ICD");
				continue;
			}
			gridProblems.setText(i, 0, "PARATYPHOID FEVER C");
		}
	}

	private void fillAllergies() {
		int rows=1+1;//+1 for the column headers.
		int columns=2;
		gridAllergies.resize(rows, columns);
		for(int i=0;i<rows;i++) {
			if(i==0) {
				labelAllergies.setVisible(false);
				gridAllergies.setText(0, 0, "Allergy");
				gridAllergies.setText(0, 1, "Reaction");
				continue;
			}
			gridAllergies.setText(i, 0, "Penicillin");
			gridAllergies.setText(i, 1, "Hives, itchy eyes, and swollen lips.");
		}
	}

}
