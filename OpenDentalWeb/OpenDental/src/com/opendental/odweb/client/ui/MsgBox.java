package com.opendental.odweb.client.ui;

import com.google.gwt.core.client.GWT;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.uibinder.client.UiHandler;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.DockPanel;
import com.google.gwt.user.client.ui.HTML;
import com.google.gwt.user.client.ui.HasHorizontalAlignment;
import com.google.gwt.user.client.ui.HorizontalPanel;
import com.google.gwt.user.client.ui.PopupPanel;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;

public class MsgBox extends PopupPanel {

	private static MsgBoxUiBinder uiBinder = GWT.create(MsgBoxUiBinder.class);
	interface MsgBoxUiBinder extends UiBinder<Widget, MsgBox> {
	}
	
	@UiField VerticalPanel contentPanel;
	@UiField DockPanel messagePanel;
	@UiField HorizontalPanel buttonPanel;
	@UiField HTML text;
	@UiField Button butOK;
	@UiField Button butClose;
	
	public MsgBox(String msg){
		//PopupPanel's constructor takes 'auto-hide'.
		//Passing true would cause the panel to close itself automatically when the user clicks outside of it.
		super(false);
		//Will gray out widgets behind the popup.
		setGlassEnabled(true);
		//Fills the @UiField objects.
		uiBinder.createAndBindUi(this);
		messagePanel.setSpacing(5);
		buttonPanel.setSpacing(3);
		buttonPanel.setHorizontalAlignment(HasHorizontalAlignment.ALIGN_RIGHT);
		//Escape characters that won't display in html:
		msg=msg.replace("&", "&amp;");
		msg=msg.replace("<", "&lt;");
		msg=msg.replace(">", "&gt;");
		msg=msg.replace("\r\n", "<br>");
		text.setText(msg);
		//Now set the contents of the widget.
		setWidget(contentPanel);
	}

	public static void Show(String text) {
		MsgBox msgBox=new MsgBox(text);
		msgBox.show();
	}
	
	@UiHandler("butOK")
	void butOK_Click(ClickEvent event) {
		this.hide();
	}
	
	@UiHandler("butClose")
	void butClose_Click(ClickEvent event) {
		this.hide();
	}
}
