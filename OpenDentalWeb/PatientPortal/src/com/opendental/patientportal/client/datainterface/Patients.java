package com.opendental.patientportal.client.datainterface;

import java.util.Date;

import com.google.gwt.http.client.RequestException;
import com.opendental.opendentbusiness.remoting.RequestHelper;
import com.opendental.opendentbusiness.remoting.RequestHelper.RequestCallbackResult;
import com.opendental.opendentbusiness.remoting.DtoGetObject;
import com.opendental.opendentbusiness.remoting.Meth;
import com.opendental.patientportal.client.MsgBox;

public class Patients {
	
	/** Gets all of the patient's family members.
	 *  @return List of patients.  Empty list if no family members exist. */
	public static void getFamilyPatientPortal(int patNum,RequestCallbackResult requestCallback) {
		DtoGetObject dto=null;
		try {
			dto=Meth.getObject("Patients.GetFamilyPatientPortal", new String[] { "long"	},"List<OpenDentBusiness.Patient>", patNum);
		}
		catch (Exception e) {
			MsgBox.show("Patients.getFamilyPatientPortal getObject error:\r\n"+e.getMessage());
		}
		try {
			RequestHelper.sendRequest(dto.serialize(), requestCallback);
		}
		catch (RequestException e) {
			MsgBox.show("Patients.getFamilyPatientPortal sendRequest error:\r\n"+e.getMessage());
		}
	}
	
	
	/** Converts a date to an age. If age is over 115, then returns 0. */
	@SuppressWarnings("deprecation")
	public static int dateToAge(Date date) {
		if(date.getYear() < 1880) {
			return 0;
		}  
		//Instantiating a new Date will contain the current date and time.
		Date now=new Date();
		if(date.getMonth() < now.getMonth()) {//Birthday in previous month.
			return now.getYear()-date.getYear();
		}
		if(date.getMonth()==now.getMonth() 
				&& date.getDay()<=now.getDay()) 
		{
			//Birthday in this month.
			return now.getYear()-date.getYear();
		}
		return now.getYear()-date.getYear()-1;
	}
	
}
