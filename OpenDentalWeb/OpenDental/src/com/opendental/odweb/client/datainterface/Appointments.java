package com.opendental.odweb.client.datainterface;

import com.opendental.odweb.client.remoting.DtoGetObject;
import com.opendental.odweb.client.remoting.Meth;
import com.opendental.odweb.client.ui.MsgBox;

public class Appointments {

	/** Server will return a list of Appointments.  This method returns a serialized DtoGetObject meant for Db.SendRequest.  Gets list of ASAP appointments. */
	public static String refreshASAP(int provNum,int siteNum,int clinicNum) {
		DtoGetObject dto=null;
		try {
			dto=Meth.getObject("Appointments.RefreshASAP", new String[] { "long","long","long"	},"List<OpenDentBusiness.Account>", provNum,siteNum,clinicNum);
		}
		catch (Exception e) {
			MsgBox.show("Error:\r\n"+e.getMessage());
		}
		return dto.serialize();
	}

}