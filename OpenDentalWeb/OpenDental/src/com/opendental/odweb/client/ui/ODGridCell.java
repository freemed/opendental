package com.opendental.odweb.client.ui;

public class ODGridCell {
	private String text;
	
	/** Creates a new ODGridCell. */
	public ODGridCell() {
		setText("");
	}
	
	/** Creates a new ODGridCell and sets the text to the passed in value. */
	public ODGridCell(String text) {
		setText(text);
	}

	/** Gets the text for the current cell. */
	public String getText() {
		return text;
	}

	public void setText(String text) {
		this.text = text;
	}

}
