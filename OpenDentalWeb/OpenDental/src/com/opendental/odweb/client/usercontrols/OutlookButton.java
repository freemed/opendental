package com.opendental.odweb.client.usercontrols;

import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.event.dom.client.ClickHandler;
import com.google.gwt.user.client.ui.ToggleButton;

public class OutlookButton extends ToggleButton implements ClickHandler{
	private String caption;
	private int imageIndex;
	private OutlookBar outlookBar;
	
	public OutlookButton(String caption,int imageIndex,OutlookBar outlookBar) {
		super(caption);//This calls the parent constructor and sets the upText of our button to caption.
		this.caption=caption;
		this.imageIndex=imageIndex;
		this.outlookBar=outlookBar;
		addClickHandler(this);
	}

	@Override
	public void onClick(ClickEvent event) { 
		this.outlookBar.ToggleModuleButtons(this.imageIndex);
	}

	public int getImageIndex() {
		return imageIndex;
	}

	public String getCaption() {
		return caption;
	}
}
