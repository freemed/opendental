package com.opendental.patientportal.client.datainterface;

import com.google.gwt.http.client.RequestException;
import com.opendental.opendentbusiness.remoting.DtoGetTable;
import com.opendental.opendentbusiness.remoting.Meth;
import com.opendental.opendentbusiness.remoting.RequestHelper;
import com.opendental.opendentbusiness.remoting.RequestHelper.RequestCallbackResult;
import com.opendental.patientportal.client.MsgBox;

public class Diseases {

	/** Gets all of the names of the problems the patient currently has from the database. 
	 *  @return DataTable with one column: Description. */
	public static void getActiveDiseasesPatientPortal(int patNum,RequestCallbackResult requestCallback) {
		DtoGetTable dto=null;
		try {
			dto=Meth.getTable("Diseases.GetActiveDiseasesPatientPortal", new String[] { "long" }, patNum);
		}
		catch (Exception e) {
			MsgBox.show("Diseases.getActiveDiseasesPatientPortal getTable error:\r\n"+e.getMessage());
		}
		try {
			RequestHelper.sendRequest(dto.serialize(), requestCallback);
		}
		catch (RequestException e) {
			MsgBox.show("Diseases.getActiveDiseasesPatientPortal sendRequest error:\r\n"+e.getMessage());
		}
	}


}