package com.opendental.odweb.client.ui;

import com.google.gwt.user.client.ui.HTML;
import com.google.gwt.user.client.ui.PopupPanel;

public class MsgBox extends PopupPanel {
	public MsgBox(String text){
		//PopupPanel's constructor takes 'auto-hide'.
		//If this is set, the panel closes itself automatically when the user clicks outside of it.
		super(true);
		//Will gray out widgets behind the popup.
		setGlassEnabled(true);
		//Escape characters that won't display in html:
		text=text.replace("&", "&amp;");
		text=text.replace("<", "&lt;");
		text=text.replace(">", "&gt;");
		text=text.replace("\r\n", "<br>");
		//Now set the contents of the widget.
		setWidget(new HTML(text));
	}

	public static void Show(String text) {
		MsgBox msgBox=new MsgBox(text);
		msgBox.show();
	}
}
