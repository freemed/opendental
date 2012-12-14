package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.EntryPoint;
import com.google.gwt.user.client.ui.RootLayoutPanel;

/** Entry point classes must define onModuleLoad(). */
public class ProgramEntry implements EntryPoint {
	/** The main shell of the entire Open Dental web application. */
	private WindowOpenDental shell;

	@Override
	public void onModuleLoad() {
		//Instantiate the shell that holds the layout of the web application.
		shell=new WindowOpenDental();
		//Add the shell to the root panel.  This makes the app visible.
    RootLayoutPanel.get().add(shell);
	}

}
