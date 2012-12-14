package com.opendental.odweb.client.mainmodules;

import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.ui.ModuleWidget;

public class ContrAccount extends ModuleWidget {
	//** Using for testing to see the different modules load.  This will probably not be used for production. */
	private static final String NAME="Account";
	
	public ContrAccount() {
		super(NAME);
	}

	@Override
	public Widget onInitialize() {
		return null;
	}

	@Override
	protected void asyncOnInitialize(AsyncCallback<Widget> callback) {
		// TODO Auto-generated method stub
	}
}
