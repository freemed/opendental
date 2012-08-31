package com.opendental.odweb.client.ui;

import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.PopupPanel;

public class MsgBox extends PopupPanel {
	public MsgBox(String text){
		//PopupPanel's constructor takes 'auto-hide'.
		//If this is set, the panel closes itself automatically when the user clicks outside of it.
		super(true);
		//Will gray out widgets behind the popup.
		setGlassEnabled(true);
		//Now set the contents of the widget.
		setWidget(new Label(text));
	}
}
