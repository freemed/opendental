package com.opendental.patientportal.client.datainterface;

import java.util.ArrayList;

import com.google.gwt.http.client.RequestException;
import com.opendental.opendentbusiness.remoting.DtoGetObject;
import com.opendental.opendentbusiness.remoting.Meth;
import com.opendental.opendentbusiness.remoting.RequestHelper;
import com.opendental.opendentbusiness.remoting.RequestHelper.RequestCallbackResult;
import com.opendental.patientportal.client.MsgBox;

public class LabResults {
	
	/** Gets all of the lab results from all of the panels passed in. 
	 *  @return ArrayList of LabResult. */
	public static void getResultsFromPanelsPatientPortal(ArrayList<Integer> panelNums,RequestCallbackResult requestCallback) {
		DtoGetObject dto=null;
		try {
			dto=Meth.getObject("LabResults.GetResultsFromPanelsPatientPortal", new String[] { "List<long>"	},"List<OpenDentBusiness.LabResult>", panelNums);
		}
		catch (Exception e) {
			MsgBox.show("LabResults.getResultsFromPanelsPatientPortal getObject error:\r\n"+e.getMessage());
		}
		try {
			RequestHelper.sendRequest(dto.serialize(), requestCallback);
		}
		catch (RequestException e) {
			MsgBox.show("LabResults.getResultsFromPanelsPatientPortal sendRequest error:\r\n"+e.getMessage());
		}
	}


}