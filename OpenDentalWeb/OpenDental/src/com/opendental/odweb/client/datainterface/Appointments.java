package com.opendental.odweb.client.datainterface;

import java.util.Date;

import com.opendental.odweb.client.remoting.*;
import com.opendental.odweb.client.remoting.Db.RequestCallbackResult;
import com.opendental.odweb.client.ui.MsgBox;

public class Appointments {
	
	/** Gets the image of the schedule for the day passed in as a Base64 string.  
	 *  Side note: We need to store the images as small as possible byte wise.  Some browsers max out their URL sizes at 32 KBs, others in MBs. */
	public static void getScheduleAsImage(Date date,RequestCallbackResult requestCallback) {
		DtoGetString dto=null;
		try {
			dto=Meth.getString("Appointments.GetScheduleAsImage", new String[] { "DateTime"	}, date);
		}
		catch (Exception e) {
			MsgBox.show("Error:\r\n"+e.getMessage());
		}
		Db.sendRequest(dto.serialize(), requestCallback);
	}

}