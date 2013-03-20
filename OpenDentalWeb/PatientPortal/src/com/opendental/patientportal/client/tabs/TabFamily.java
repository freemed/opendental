package com.opendental.patientportal.client.tabs;

import java.util.ArrayList;

import com.google.gwt.core.client.GWT;
import com.google.gwt.i18n.client.DateTimeFormat;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.opendentbusiness.remoting.RequestHelper.RequestCallbackResult;
import com.opendental.opendentbusiness.tabletypes.Patient;
import com.opendental.patientportal.client.MsgBox;
import com.opendental.patientportal.client.datainterface.Patients;

public class TabFamily extends Composite {
	private static TabFamilyUiBinder uiBinder = GWT.create(TabFamilyUiBinder.class);
	interface TabFamilyUiBinder extends UiBinder<Widget, TabFamily> {
	}
	
	private Patient patCur;
	@UiField Grid gridFamily;
	@UiField Grid gridPatient;
	@UiField VerticalPanel contentPanel;
	
	public TabFamily(Patient pat) {
		patCur=pat;
		//Initialize the UI binder.
		uiBinder.createAndBindUi(this);
		getFamily();
		fillPatient();
		initWidget(contentPanel);
	}
	
	private void getFamily() {
		//Get the family members from the database.
		Patients.getFamilyPatientPortal(patCur.PatNum, new getFamily_Callback());
	}
	
	private class getFamily_Callback implements RequestCallbackResult {
		@SuppressWarnings("unchecked")
		public void onSuccess(Object obj) {
			fillFamily((ArrayList<Patient>)obj);
		}
		
		public void onError(String error) {
			MsgBox.show(error);
		}
		
	}

	/** This gets called whenever the results from the database come back.  This happens very quickly and I think it will be fine that it loads slightly slower than the rest.  */
	private void fillFamily(ArrayList<Patient> family) {
		int famCount=family.size();
		if(famCount==0) {
			return;
		}
		int rows=famCount+1;//+1 is for the column headers.
		int columns=5;
		gridFamily.resize(rows, columns);
		for(int r=0;r<rows+1;r++) {
			if(r==0) {//If the first row.
				//Add the column headers.
				gridFamily.setText(r, 0, "Name");
				gridFamily.setText(r, 1, "Position");
				gridFamily.setText(r, 2, "Gender");
				gridFamily.setText(r, 3, "Status");
				gridFamily.setText(r, 4, "Age");
				continue;
			}
			for(int c=0;c<columns;c++) {
				switch(c) {
					case 0://Name
						gridFamily.setText(r, c, family.get(r-1).LName+", "+family.get(r-1).FName);
						break;
					case 1://Position
						gridFamily.setText(r, c, family.get(r-1).Position.toString());
						break;
					case 2://Gender
						gridFamily.setText(r, c, family.get(r-1).Gender.toString());
						break;
					case 3://Status
						gridFamily.setText(r, c, family.get(r-1).PatStatus.toString());
						break;
					case 4://Age
						gridFamily.setText(r, c, Integer.toString(family.get(r-1).Age));
						break;
				}
			}
		}
	}

	@SuppressWarnings("deprecation")
	private void fillPatient() {
		gridPatient.resize(16, 2);
		for(int i=0;i<16;i++) {
			switch(i) {
				case 0://Last Name
					gridPatient.setText(i, 0, "Last Name:");
					gridPatient.setText(i, 1, patCur.LName);
					break;
				case 1://First Name
					gridPatient.setText(i, 0, "First Name:");
					gridPatient.setText(i, 1, patCur.FName);
					break;
				case 2://Middle Name
					gridPatient.setText(i, 0, "Middle Name:");
					gridPatient.setText(i, 1, patCur.MiddleI);
					break;
				case 3://Nick Name
					gridPatient.setText(i, 0, "Nick Name:");
					gridPatient.setText(i, 1, patCur.Preferred);
					break;
				case 4://Status
					gridPatient.setText(i, 0, "Status:");
					if(patCur.PatStatus!=null) {
						gridPatient.setText(i, 1, patCur.PatStatus.toString());
					}
					break;
				case 5://Gender
					gridPatient.setText(i, 0, "Gender:");
					if(patCur.Gender!=null) {
						gridPatient.setText(i, 1, patCur.Gender.toString());
					}
					break;
				case 6://Birthdate
					gridPatient.setText(i, 0, "Birthdate:");
					if(patCur.Birthdate.getYear()>-20) {//Greater than 1880.  getYear is the result of year-1900.
						//The default getShortDateFormat only returns MM/dd/yy so we'll use a custom format to get 4 digits to the year.  This will be a problem for foreign users.
						DateTimeFormat dateFmt=DateTimeFormat.getFormat("MM/dd/yyyy");
						gridPatient.setText(i, 1, dateFmt.format(patCur.Birthdate));
					}
					break;
				case 7://Address
					gridPatient.setText(i, 0, "Address:");
					gridPatient.setText(i, 1, patCur.Address);
					break;
				case 8://Address2
					gridPatient.setText(i, 0, "Address2:");
					gridPatient.setText(i, 1, patCur.Address2);
					break;
				case 9://City
					gridPatient.setText(i, 0, "City:");
					gridPatient.setText(i, 1, patCur.City);
					break;
				case 10://State
					gridPatient.setText(i, 0, "State:");
					gridPatient.setText(i, 1, patCur.State);
					break;
				case 11://Zip
					gridPatient.setText(i, 0, "Zip:");
					gridPatient.setText(i, 1, patCur.Zip);
					break;
				case 12://Home Phone
					gridPatient.setText(i, 0, "Home Phone:");
					gridPatient.setText(i, 1, patCur.HmPhone);
					break;
				case 13://Work Phone
					gridPatient.setText(i, 0, "Work Phone:");
					gridPatient.setText(i, 1, patCur.WkPhone);
					break;
				case 14://Cell Phone
					gridPatient.setText(i, 0, "Cell Phone:");
					gridPatient.setText(i, 1, patCur.WirelessPhone);
					break;
				case 15://Email
					gridPatient.setText(i, 0, "Email:");
					gridPatient.setText(i, 1, patCur.Email);
					break;
			}
		}
	}

}
