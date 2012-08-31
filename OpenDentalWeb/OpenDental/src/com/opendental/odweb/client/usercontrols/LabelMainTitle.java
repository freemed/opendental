package com.opendental.odweb.client.usercontrols;

import com.google.gwt.user.client.ui.Label;

public class LabelMainTitle extends Label{
	private String title;
	
	public LabelMainTitle(){
		title="Open Dental";
		// TODO Get the name of the database for display and set the value to text.
		this.setText(title);
	}
	
	/**
	 * Updates the main title bar with the the patient's information.    
	 */
	public void UpdateTitle(){
		// TODO Make this function accept a patient object to update the title bar with the selected pt info.
		this.setText("");
	}
}
