package com.opendental.odweb.client.datainterface;

import java.util.HashMap;

import com.google.gwt.http.client.RequestException;
import com.opendental.opendentbusiness.data.DataTable;
import com.opendental.opendentbusiness.data.PIn;
import com.opendental.opendentbusiness.remoting.DtoGetTable;
import com.opendental.opendentbusiness.remoting.Meth;
import com.opendental.opendentbusiness.remoting.RequestHelper;
import com.opendental.opendentbusiness.remoting.RequestHelper.RequestCallbackResult;
import com.opendental.opendentbusiness.tabletypes.Pref;
import com.opendental.odweb.client.ui.MsgBox;

public class Prefs {
	//Cache----------------------------------------------------------------------------------------------------------------
	//The preference cache pattern is not the standard way to handle cache.  Do not use this pattern as a reference.
	private static HashMap<String,Pref> Dict;
	
	public static void refreshCache(RequestCallbackResult callback) {
		DtoGetTable dto=null;
		try {
			dto=Meth.getTable("Prefs.RefreshCache");
		}
		catch (Exception e) {
			MsgBox.show("Prefs.refreshCache getTable Error:\r\n"+e.getMessage());
		}
		try {
			RequestHelper.sendRequest(dto.serialize(),callback);
		}
		catch (RequestException e) {
			MsgBox.show("Prefs.refreshCache sendRequest Error:\r\n"+e.getMessage());
		}
	}
	
	public static void fillCache(DataTable table) {
		Dict=new HashMap<String,Pref>();
		Pref pref;
		for(int i=0;i<table.Rows.size();i++) {
			pref=new Pref();
			pref.PrefNum=PIn.Int(table.getCellText(i, "PrefNum"));
			pref.PrefName=PIn.String(table.getCellText(i, "PrefName"));
			pref.ValueString=PIn.String(table.getCellText(i, "ValueString"));
			Dict.put(pref.PrefName, pref);
		}
	}
	
	//PrefC----------------------------------------------------------------------------------------------------------------
	//This section of the preference cache will contain the methods that are in OpenDentBusiness\Cache\PrefC.cs
	
	public static long getLong(PrefName prefName) {
		if(!Dict.containsKey(prefName.toString())) {
			MsgBox.show("Unknown PrefName: "+prefName.toString());
		}
		return PIn.Long(Dict.get(prefName.toString()).ValueString);
	}
	
	public static int getInt(PrefName prefName) {
		if(!Dict.containsKey(prefName.toString())) {
			MsgBox.show("Unknown PrefName: "+prefName.toString());
		}
		return PIn.Int(Dict.get(prefName.toString()).ValueString);
	}
	
	public static double getDouble(PrefName prefName) {
		if(!Dict.containsKey(prefName.toString())) {
			MsgBox.show("Unknown PrefName: "+prefName.toString());
		}
		return PIn.Double(Dict.get(prefName.toString()).ValueString);
	}
	
	public static boolean getBool(PrefName prefName) {
		if(!Dict.containsKey(prefName.toString())) {
			MsgBox.show("Unknown PrefName: "+prefName.toString());
		}
		return PIn.Bool((Dict.get(prefName.toString()).ValueString));
	}
	
	public static String getString(PrefName prefName) {
		if(!Dict.containsKey(prefName.toString())) {
			MsgBox.show("Unknown PrefName: "+prefName.toString());
		}
		return PIn.String((Dict.get(prefName.toString()).ValueString));
	}	
	
	//Preference Callbacks-------------------------------------------------------------------------------------------------
	
		
	
	//Preference enums-----------------------------------------------------------------------------------------------------
	
	/** Because this enum is stored in the database as strings rather than as numbers, we can do the order alphabetically and we can change it whenever we want. 
	 *  Any preferences added here need to be included in the Prefs.RefreshCache query in the C# S class. */
	public enum PrefName {
		MainWindowTitle,
		PatientSelectUsesSearchButton,
		ShowIDinTitleBar
	}
	
	

}