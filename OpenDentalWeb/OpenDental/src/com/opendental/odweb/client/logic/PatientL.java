package com.opendental.odweb.client.logic;

import com.opendental.odweb.client.tabletypes.Patient;

public class PatientL {
	
	/** Returns the string that displays at the top of the screen to show information about the selected patient.  Accepts null. */
	public static String getMainTitle(Patient pat) {
		String retVal=pat.PatNum+" "+pat.FName+" "+pat.LName;
		return retVal;
	}

}
