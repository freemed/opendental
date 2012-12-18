package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.RunAsyncCallback;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.AbsolutePanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.ui.ModuleWidget;

public class ContrAppt extends ModuleWidget {
	//** Using for testing to see the different modules load.  This will probably not be used for production. */
	private static final String NAME="Appts";

//  @UiField
//  ListBox listView;
	
	/** Constructor. */
	public ContrAppt() {
		super(NAME);
	}

	/** Loads up all of the information for the appointment module. */
	@Override
	public Widget onInitialize() {
		// TODO Create a panel, add widgets to the panel to compose the appointment module and return the panel.
		AbsolutePanel absolutePanel=new AbsolutePanel();
    absolutePanel.setSize("250px", "250px");
    //Add more widgets to the absolutePanel to comprise the Appts module.
//    //Event handler for listView.
//    listView.addChangeHandler(new ChangeHandler() {
//      public void onChange(ChangeEvent event) {
//        showApptView();
//      }
//    });
		return absolutePanel;
	}

	@Override
	protected void asyncOnInitialize(final AsyncCallback<Widget> callback) {
		GWT.runAsync(ContrAppt.class, new RunAsyncCallback() {

      public void onFailure(Throwable error) {
        callback.onFailure(error);
      }

      public void onSuccess() {
        callback.onSuccess(onInitialize());
      }
    });
	}
}
