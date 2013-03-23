package com.opendental.patientportal.client.datainterface;

import com.google.gwt.http.client.RequestException;
import com.opendental.opendentbusiness.remoting.DtoGetObject;
import com.opendental.opendentbusiness.remoting.Meth;
import com.opendental.opendentbusiness.remoting.RequestHelper;
import com.opendental.opendentbusiness.remoting.RequestHelper.RequestCallbackResult;
import com.opendental.patientportal.client.MsgBox;

public class LabPanels {

	/** Gets all of the lab panels from the database. 
	 *  @return ArrayList of LabPanel. */
	public static void getAllPatientPortal(int patNum,RequestCallbackResult requestCallback) {
		DtoGetObject dto=null;
		try {
			dto=Meth.getObject("LabPanels.GetAllPatientPortal", new String[] { "long"	},"List<OpenDentBusiness.LabPanel>", patNum);
		}
		catch (Exception e) {
			MsgBox.show("LabPanels.getAllPatientPortal getObject error:\r\n"+e.getMessage());
		}
		try {
			RequestHelper.sendRequest(dto.serialize(), requestCallback);
		}
		catch (RequestException e) {
			MsgBox.show("LabPanels.getAllPatientPortal sendRequest error:\r\n"+e.getMessage());
		}
	}

}