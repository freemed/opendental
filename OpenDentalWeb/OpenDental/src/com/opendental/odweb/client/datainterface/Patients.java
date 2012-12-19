package com.opendental.odweb.client.datainterface;

import java.util.Date;

import com.opendental.odweb.client.remoting.DtoGetTable;
import com.opendental.odweb.client.remoting.Meth;
import com.opendental.odweb.client.ui.MsgBox;

public class Patients {

	/** Returns a serialized DtoGetTable.  Only used for the Select Patient dialog. */
	public static String getPtDataTable(boolean limit,String lname,String fname,String phone,
			String address,boolean hideInactive,String city,String state,String ssn,String patnum,String chartnumber,
			int billingtype,boolean guarOnly,boolean showArchived,int clinicNum,Date birthdate,
			int siteNum,String subscriberId,String email) {
		DtoGetTable dto=null;
		//The parameter types in the C# method that we will be calling.
		String[] paramTypes=new String[] { 
				"bool","string","string","string","string","bool","string","string",
				"string","string","string","long","bool","bool","long","DateTime",
				"long","string","string" };
		try {
			dto=Meth.getTable("Patients.GetPtDataTable",paramTypes,
					limit,lname,fname,phone,address,hideInactive,city,
					state,ssn,patnum,chartnumber,billingtype,guarOnly,
					showArchived,clinicNum,birthdate,siteNum,subscriberId,email);
		} catch (Exception e) {
			MsgBox.show("Error:\r\n"+e.getMessage());
		}
		return dto.serialize();
	}

}