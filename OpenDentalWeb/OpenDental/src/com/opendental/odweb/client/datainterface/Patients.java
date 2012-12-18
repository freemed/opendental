package com.opendental.odweb.client.datainterface;

import com.opendental.odweb.client.remoting.DtoGetTable;
import com.opendental.odweb.client.remoting.Meth;
import com.opendental.odweb.client.ui.MsgBox;

public class Patients {

	/** Returns a serialized DtoGetTable.  Only used for the Select Patient dialog. */
	public static String GetPtDataTableTest(int guarNum,int excludePayNum) {
		DtoGetTable dto=null;
		String[] paramTypes=new String[] { "long","long" };//The parameter types in the C# method that we will be calling.
		try {
			dto=Meth.getTable("Patients.GetPtDataTableTest",paramTypes,guarNum,excludePayNum);
		} catch (Exception e) {
			MsgBox.show("Error:\r\n"+e.getMessage());
		}
		return dto.serialize();
	}

}