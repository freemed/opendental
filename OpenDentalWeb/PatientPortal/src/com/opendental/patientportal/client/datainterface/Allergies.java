package com.opendental.patientportal.client.datainterface;

import com.google.gwt.http.client.RequestException;
import com.opendental.opendentbusiness.remoting.DtoGetTable;
import com.opendental.opendentbusiness.remoting.Meth;
import com.opendental.opendentbusiness.remoting.RequestHelper;
import com.opendental.opendentbusiness.remoting.RequestHelper.RequestCallbackResult;
import com.opendental.patientportal.client.MsgBox;

public class Allergies {

	/** Gets the description and the reaction for all active allergies. 
	 *  @return DataTable with two columns: Description, Reaction. */
	public static void getActiveAllergiesPatientPortal(int patNum,RequestCallbackResult requestCallback) {
		DtoGetTable dto=null;
		try {
			dto=Meth.getTable("Allergies.GetActiveAllergiesPatientPortal", new String[] { "long" }, patNum);
		}
		catch (Exception e) {
			MsgBox.show("Allergies.getActiveAllergiesPatientPortal getTable error:\r\n"+e.getMessage());
		}
		try {
			RequestHelper.sendRequest(dto.serialize(), requestCallback);
		}
		catch (RequestException e) {
			MsgBox.show("Allergies.getActiveAllergiesPatientPortal sendRequest error:\r\n"+e.getMessage());
		}
	}


}