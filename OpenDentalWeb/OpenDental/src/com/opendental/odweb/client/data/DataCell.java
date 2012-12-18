package com.opendental.odweb.client.data;

public class DataCell {

	private String Text;
	
	/** Creates an empty DataCell and sets the Text to an empty string. */
	public DataCell() {
		Text="";
	}
	
	/** Creates a new DataCell and sets the Text to the passed in string. */
	public DataCell(String text) {
		setText(text);
	}

	public String getText() {
		return Text;
	}

	public void setText(String text) {
		Text=text;
	}
}
