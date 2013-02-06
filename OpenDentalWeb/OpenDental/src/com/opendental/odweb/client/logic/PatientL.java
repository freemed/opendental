package com.opendental.odweb.client.logic;

import com.opendental.odweb.client.datainterface.Patients;
import com.opendental.odweb.client.datainterface.Prefs;
import com.opendental.odweb.client.datainterface.Prefs.PrefName;
import com.opendental.odweb.client.dbmultitable.Security;
import com.opendental.odweb.client.tabletypes.Patient;

public class PatientL {
	
	/** Returns the string that displays at the top of the screen to show information about the selected patient.  Accepts null. */
	public static String getMainTitle(Patient pat) {
		String retVal=Prefs.getString(PrefName.MainWindowTitle);
		if(Security.getCurUser()!=null) {
			retVal+=" {"+Security.getCurUser().UserName+"} ";
		}
		if(pat==null || pat.PatNum==0 || pat.PatNum==-1) {
			return retVal;
		}
		retVal+=""+Patients.getNameLF(pat);
		if(Prefs.getLong(PrefName.ShowIDinTitleBar)==1) {
			retVal+=" - "+pat.PatNum;
		}
		else if(Prefs.getLong(PrefName.ShowIDinTitleBar)==2) {
			retVal+=" - "+pat.ChartNumber;
		}
		else if(Prefs.getLong(PrefName.ShowIDinTitleBar)==3) {
			if(pat.Birthdate.getYear()>1880) {
				// TODO Enhance this to display the correct date time string format.
				retVal+=" - "+pat.Birthdate.toLocaleString();
			}
		}
		// TODO Enhance to get the description of the patients Site.
		return retVal;
	}

}
