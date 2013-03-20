package com.opendental.patientportal.client;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.Scheduler;
import com.google.gwt.core.client.Scheduler.ScheduledCommand;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.uibinder.client.UiHandler;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.DockPanel;
import com.google.gwt.user.client.ui.HTML;
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
	/** The maximum width we want pop ups to be. */
	private final int MAX_WIDTH=600;
	
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
		text.setText(msg);
		setVisible(false);//Hide until we resize the message box.
		//We have to use a deferred scheduler to set the width because the widgets have yet to be drawn.
		Scheduler.get().scheduleDeferred(new ScheduledCommand() {
			@Override
			public void execute() {
				//Check if the content panel is wider than the max width.
				if(contentPanel.getOffsetWidth()>MAX_WIDTH) {
					contentPanel.setWidth(MAX_WIDTH+"px");
				}
				//Center the popup panel.
				center();
				//This is so the user can't see the resizing take place.
				setVisible(true);
			}
		});
		//Now set the contents of the widget.
		setWidget(contentPanel);
	}

	public static void show(String text) {
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
