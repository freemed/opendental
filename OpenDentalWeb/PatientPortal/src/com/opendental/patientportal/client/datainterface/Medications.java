package com.opendental.patientportal.client.datainterface;

import com.google.gwt.http.client.RequestException;
import com.opendental.opendentbusiness.remoting.DtoGetTable;
import com.opendental.opendentbusiness.remoting.Meth;
import com.opendental.opendentbusiness.remoting.RequestHelper;
import com.opendental.opendentbusiness.remoting.RequestHelper.RequestCallbackResult;
import com.opendental.patientportal.client.MsgBox;

public class Medications {

	/** Gets all of the names of the medications the patient is currently taking from the database. 
	 *  @return DataTable with one column: MedName. */
	public static void getAllMedNamesPatientPortal(int patNum,RequestCallbackResult requestCallback) {
		DtoGetTable dto=null;
		try {
			dto=Meth.getTable("Medications.GetAllMedNamesPatientPortal", new String[] { "long" }, patNum);
		}
		catch (Exception e) {
			MsgBox.show("Medications.getAllMedNamesPatientPortal getTable error:\r\n"+e.getMessage());
		}
		try {
			RequestHelper.sendRequest(dto.serialize(), requestCallback);
		}
		catch (RequestException e) {
			MsgBox.show("Medications.getAllMedNamesPatientPortal sendRequest error:\r\n"+e.getMessage());
		}
	}


}