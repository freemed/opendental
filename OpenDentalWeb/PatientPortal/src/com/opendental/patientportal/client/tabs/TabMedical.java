package com.opendental.patientportal.client.tabs;

import java.util.ArrayList;

import com.google.gwt.core.client.GWT;
import com.google.gwt.i18n.client.DateTimeFormat;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.opendentbusiness.data.DataTable;
import com.opendental.opendentbusiness.remoting.RequestHelper.RequestCallbackResult;
import com.opendental.opendentbusiness.tabletypes.LabPanel;
import com.opendental.opendentbusiness.tabletypes.LabResult;
import com.opendental.opendentbusiness.tabletypes.Patient;
import com.opendental.patientportal.client.MsgBox;
import com.opendental.patientportal.client.datainterface.Allergies;
import com.opendental.patientportal.client.datainterface.Diseases;
import com.opendental.patientportal.client.datainterface.LabPanels;
import com.opendental.patientportal.client.datainterface.LabResults;
import com.opendental.patientportal.client.datainterface.Medications;

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
	private Patient patCur;
	private ArrayList<LabPanel> listPanels;
	private ArrayList<LabResult> listResults;
	
	public TabMedical(Patient pat) {
		patCur=pat;
		//Initialize the UI binder.
		uiBinder.createAndBindUi(this);
		getLabPanels();
		initWidget(panelContainer);
	}

	private void getLabPanels() {
		LabPanels.getAllPatientPortal(patCur.PatNum, new getLabPanels_Callback());
	}
	
	private class getLabPanels_Callback implements RequestCallbackResult {
		@SuppressWarnings("unchecked")
		public void onSuccess(Object obj) {
			listPanels=(ArrayList<LabPanel>)obj;
			getLabResults();
		}
		
		public void onError(String error) {
			MsgBox.show(error);
		}		
	}
	
	private void getLabResults() {
		ArrayList<Integer> panelNums=new ArrayList<Integer>();
		if(listPanels!=null) {//If there are lab panels, get the results.
			for(int i=0;i<listPanels.size();i++) {
				panelNums.add(listPanels.get(i).LabPanelNum);
			}
		}
		LabResults.getResultsFromPanelsPatientPortal(panelNums, new getLabResults_Callback());
	}
	
	private class getLabResults_Callback implements RequestCallbackResult {
		@SuppressWarnings("unchecked")
		public void onSuccess(Object obj) {
			listResults=(ArrayList<LabResult>) obj;
			fillLabPanels();
			getMedications();
		}
		
		public void onError(String error) {
			MsgBox.show(error);
		}		
	}

	private void fillLabPanels() {
		if(listPanels==null) {
			return;
		}
		int labPanelCount=listPanels.size();
		if(labPanelCount==0) {
			return;
		}
		labelPanels.setVisible(false);
		int rows=labPanelCount+1;//+1 for the column headers.
		int columns=2;
		gridPanels.resize(rows, columns);
		gridPanels.setText(0, 0, "Lab Name & Address");
		gridPanels.setText(0, 1, "Lab Results");
		for(int i=0;i<labPanelCount;i++) {
			gridPanels.setText(i+1, 0, listPanels.get(i).LabNameAddress);
			gridPanels.setWidget(i+1, 1, getGridResultsForPanel(listPanels.get(i).LabPanelNum));
		}
	}
	
	/** Gets a grid of lab results for the panel passed in.  Only used for display purposes. */
	@SuppressWarnings("deprecation")
	private Grid getGridResultsForPanel(int panelNum) {
		Grid gridResults=new Grid();
		if(listResults==null) {
			return gridResults;
		}
		ArrayList<LabResult> listResultMatches=new ArrayList<LabResult>();
		//Loop through all of the results from the database and see if any results exist for the panel.
		for(int i=0;i<listResults.size();i++) {
			if(listResults.get(i).LabPanelNum==panelNum) {
				listResultMatches.add(listResults.get(i));
			}
		}
		int rows=listResultMatches.size()+1;//+1 for the column headers.
		int columns=3;
		gridResults.resize(rows, columns);
		//Set the headers.
		gridResults.setText(0, 0, "Date Time");
		gridResults.setText(0, 1, "Test Name");
		gridResults.setText(0, 2, "Observed Value");
		for(int i=0;i<listResultMatches.size();i++) {
			String dateTimeTest="";
			if(listResultMatches.get(i).DateTimeTest.getYear()>-20) {//If patient was born after 1880.  1880-1900 = -20
				dateTimeTest=DateTimeFormat.getFormat("MM/dd/yyyy hh:mm:ss a").format(listResultMatches.get(i).DateTimeTest);
			}
			gridResults.setText(i+1, 0, dateTimeTest);
			gridResults.setText(i+1, 1, listResultMatches.get(i).TestName);
			gridResults.setText(i+1, 2, listResultMatches.get(i).ObsValue);
		}
		return gridResults;
	}

	private void getMedications() {
		Medications.getAllMedNamesPatientPortal(patCur.PatNum, new getMedications_Callback());
	}
	
	private class getMedications_Callback implements RequestCallbackResult {
		public void onSuccess(Object obj) {
			ArrayList<String> medNames=new ArrayList<String>();
			DataTable table=(DataTable)obj;
			for(int i=0;i<table.Rows.size();i++) {
				medNames.add(table.getCellText(i, "MedName"));
			}
			fillMedications(medNames);
			getProblems();
		}
		
		public void onError(String error) {
			MsgBox.show(error);
		}		
	}
	
	private void fillMedications(ArrayList<String> medNames) {
		int medCount=medNames.size();
		if(medCount==0) {
			return;
		}
		labelMedications.setVisible(false);
		int rows=medCount+1;//+1 for the column headers.
		int columns=1;
		gridMedications.resize(rows, columns);
		gridMedications.setText(0, 0, "Medication Name");
		for(int i=0;i<medCount;i++) {
			gridMedications.setText(i+1, 0, medNames.get(i));
		}
	}

	private void getProblems() {
		Diseases.getActiveDiseasesPatientPortal(patCur.PatNum, new getProblems_Callback());
	}
	
	private class getProblems_Callback implements RequestCallbackResult {
		public void onSuccess(Object obj) {
			ArrayList<String> problems=new ArrayList<String>();
			DataTable table=(DataTable)obj;
			for(int i=0;i<table.Rows.size();i++) {
				problems.add(table.getCellText(i, "Description"));
			}
			fillProblems(problems);
			getAllergies();
		}
		
		public void onError(String error) {
			MsgBox.show(error);
		}		
	}

	private void fillProblems(ArrayList<String> problems) {
		int probCount=problems.size();
		if(probCount==0) {
			return;
		}
		labelProblems.setVisible(false);
		int rows=probCount+1;//+1 for the column headers.
		int columns=1;
		gridProblems.resize(rows, columns);
		gridProblems.setText(0, 0, "ICD");
		for(int i=0;i<probCount;i++) {
			gridProblems.setText(i+1, 0, problems.get(i));
		}
	}
	
	private void getAllergies() {
		Allergies.getActiveAllergiesPatientPortal(patCur.PatNum, new getAllergies_Callback());
	}
	
	private class getAllergies_Callback implements RequestCallbackResult {
		public void onSuccess(Object obj) {
			fillAllergies((DataTable)obj);
		}
		
		public void onError(String error) {
			MsgBox.show(error);
		}		
	}

	private void fillAllergies(DataTable allergies) {
		int allergyCount=allergies.Rows.size();
		if(allergyCount==0) {
			return;
		}
		labelAllergies.setVisible(false);
		int rows=allergyCount+1;//+1 for the column headers.
		int columns=2;
		gridAllergies.resize(rows, columns);
		gridAllergies.setText(0, 0, "Allergy");
		gridAllergies.setText(0, 1, "Reaction");
		for(int i=0;i<allergyCount;i++) {
			gridAllergies.setText(i+1, 0, allergies.getCellText(i, "Description"));
			gridAllergies.setText(i+1, 1, allergies.getCellText(i, "Reaction"));
		}
	}

}
