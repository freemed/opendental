package com.opendental.odweb.client.ui;

public class ODGridCell {
	private String text;
	
	/** Creates a new ODGridCell. */
	public ODGridCell() {
		SetText("");
	}
	
	/** Creates a new ODGridCell and sets the text to the passed in value. */
	public ODGridCell(String text) {
		SetText(text);
	}

	/** Gets the text for the current cell. */
	public String getText() {
		return text;
	}

	public void SetText(String text) {
		this.text = text;
	}

}
