package com.opendental.odweb.client.datainterface;

import com.opendental.odweb.client.remoting.DtoGetTable;
import com.opendental.odweb.client.remoting.Meth;
import com.opendental.odweb.client.ui.MsgBox;

public class Patients {

	/** Returns a serialized DtoGetTable.  Only used for the Select Patient dialog. */
	public static String GetPtDataTable() {
		DtoGetTable dto=null;
		String[] paramTypes=new String[0];//{ "long","AccountType" };//The parameter types in the C# method that we will be calling.
		try {
			dto=Meth.GetTable("Patients.GetPtDataTableTest",paramTypes);
		} catch (Exception e) {
			MsgBox.Show("Error:\r\n"+e.getMessage());
		}
		return dto.Serialize();
	}

}