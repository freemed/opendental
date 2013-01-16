package com.opendental.odweb.client.datainterface;

import java.util.Date;

import com.opendental.odweb.client.remoting.*;
import com.opendental.odweb.client.ui.MsgBox;

public class Patients {

	/** Returns a serialized DtoGetObject.  This is a way to get a single patient from the database if you don't already have a family object to use.  Will return null if not found. */
	public static String GetPat(int patNum) {
		DtoGetObject dto=null;
		try {
			dto=Meth.getObject("Patients.GetPat", new String[] { "long"	},"OpenDentBusiness.Patient", patNum);
		}
		catch (Exception e) {
			MsgBox.show("Error:\r\n"+e.getMessage());
		}
		return dto.serialize();
	}

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
	
	/** Converts a date to an age. If age is over 115, then returns 0. */
	@SuppressWarnings("deprecation")
	public static int dateToAge(Date date) {
		if(date.getYear() < 1880) {
			return 0;
		}  
		//Instantiating a new Date will contain the current date and time.
		Date now=new Date();
		if(date.getMonth() < now.getMonth()) {//Birthday in previous month.
			return now.getYear()-date.getYear();
		}
		if(date.getMonth()==now.getMonth() 
				&& date.getDay()<=now.getDay()) 
		{
			//Birthday in this month.
			return now.getYear()-date.getYear();
		}
		return now.getYear()-date.getYear()-1;
	}

}