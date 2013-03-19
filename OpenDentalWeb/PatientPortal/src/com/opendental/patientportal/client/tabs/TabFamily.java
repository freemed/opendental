package com.opendental.patientportal.client.tabs;

import com.google.gwt.core.client.GWT;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;

public class TabFamily extends Composite {
	private static TabFamilyUiBinder uiBinder = GWT.create(TabFamilyUiBinder.class);
	interface TabFamilyUiBinder extends UiBinder<Widget, TabFamily> {
	}
	
	@UiField Grid gridFamily;
	@UiField Grid gridPatient;
	@UiField VerticalPanel contentPanel;
	
	public TabFamily() {
		//Initialize the UI binder.
		uiBinder.createAndBindUi(this);
		fillFamily();
		fillPatient();
		initWidget(contentPanel);
	}

	private void fillFamily() {
		// TODO Dynamically set the number of rows.
		int rows=3;
		int columns=5;
		gridFamily.resize(rows, columns);
		for(int r=0;r<rows+1;r++) {
			// TODO Enhance to loop through the rows.
			for(int c=0;c<columns;c++) {
				if(r==0) {//If the first row.
					//Add the column headers as bold.
					gridFamily.setText(r, 0, "Name");
					gridFamily.setText(r, 1, "Position");
					gridFamily.setText(r, 2, "Gender");
					gridFamily.setText(r, 3, "Status");
					gridFamily.setText(r, 4, "Age");
					break;
				}
				// TODO Set the Name, Position, Gender, Status, and Age of the patient.
			}
		}
	}

	private void fillPatient() {
		int rows=16;
		int columns=2;
		gridPatient.resize(rows, columns);
		// TODO Enhance to fill in the patients data.
		for(int i=0;i<rows;i++) {
			switch(i) {
				case 0://Last Name
					gridPatient.setText(i, 0, "Last Name:");
					gridPatient.setText(i, 1, "");
					break;
				case 1://First Name
					gridPatient.setText(i, 0, "First Name:");
					gridPatient.setText(i, 1, "");
					break;
				case 2://Middle Name
					gridPatient.setText(i, 0, "Middle Name:");
					gridPatient.setText(i, 1, "");
					break;
				case 3://Nick Name
					gridPatient.setText(i, 0, "Nick Name:");
					gridPatient.setText(i, 1, "");
					break;
				case 4://Status
					gridPatient.setText(i, 0, "Status:");
					gridPatient.setText(i, 1, "");
					break;
				case 5://Gender
					gridPatient.setText(i, 0, "Gender:");
					gridPatient.setText(i, 1, "");
					break;
				case 6://Birthdate
					gridPatient.setText(i, 0, "Birthdate:");
					gridPatient.setText(i, 1, "");
					break;
				case 7://Address
					gridPatient.setText(i, 0, "Address:");
					gridPatient.setText(i, 1, "");
					break;
				case 8://Address2
					gridPatient.setText(i, 0, "Address2:");
					gridPatient.setText(i, 1, "");
					break;
				case 9://City
					gridPatient.setText(i, 0, "City:");
					gridPatient.setText(i, 1, "");
					break;
				case 10://State
					gridPatient.setText(i, 0, "State:");
					gridPatient.setText(i, 1, "");
					break;
				case 11://Zip
					gridPatient.setText(i, 0, "Zip:");
					gridPatient.setText(i, 1, "");
					break;
				case 12://Home Phone
					gridPatient.setText(i, 0, "Home Phone:");
					gridPatient.setText(i, 1, "");
					break;
				case 13://Work Phone
					gridPatient.setText(i, 0, "Work Phone:");
					gridPatient.setText(i, 1, "");
					break;
				case 14://Cell Phone
					gridPatient.setText(i, 0, "Cell Phone:");
					gridPatient.setText(i, 1, "");
					break;
				case 15://Email
					gridPatient.setText(i, 0, "Email:");
					gridPatient.setText(i, 1, "");
					break;
			}
		}
	}

}
