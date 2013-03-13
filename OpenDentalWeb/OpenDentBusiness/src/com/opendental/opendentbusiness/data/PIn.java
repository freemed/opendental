package com.opendental.opendentbusiness.data;

public class PIn {
	
	public static boolean Bool(String myString) {
		if(myString.equals("") || myString.equals("0")) {
			return false;
		}
		return true;
	}

	/** If blank or invalid, returns 0. Otherwise, parses. */
	public static double Double(String myString) {
		if(myString.equals("")) {
			return 0;
		}
		try {
			return Double.parseDouble(myString);
		}
		catch(Exception e) {
			return 0;
		}
	}

	/** If blank or invalid, returns 0. Otherwise, parses. */
	public static int Int(String myString) {
		if(myString.equals("")) {
			return 0;
		}
		return Integer.parseInt(myString);
	}
	
	public static long Long (String myString){
		if(myString=="") {
			return 0;
		}
		else {
			return Long.parseLong(myString);
		}
	}

	/** Currently does nothing. */
	public static String String(String myString) {
		return myString;
	}

}
