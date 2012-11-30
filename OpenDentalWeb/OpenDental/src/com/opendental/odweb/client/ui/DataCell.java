package com.opendental.odweb.client.ui;

public class DataCell {

	private String Text;
	
	/** Creates an empty DataCell and sets the Text to an empty string. */
	public DataCell() {
		Text="";
	}
	
	/** Creates a new DataCell and sets the Text to the passed in string. */
	public DataCell(String text) {
		SetText(text);
	}

	public String GetText() {
		return Text;
	}

	public void SetText(String text) {
		Text = text;
	}
}
