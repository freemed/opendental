package com.opendental.odweb.client.ui;

public class ODGridColumn {
	private String Heading;
	private int ColWidth;
	
	/** Creates a new ODGridColumn with a default column width of 80. */
	public ODGridColumn() {
		setHeading("");//So that Heading will not be null.
		setColWidth(80);
	}
	
	/** Creates a new ODGridColumn and sets the heading and column width to the passed in values. */
	public ODGridColumn(String heading,int colWidth) {
		setHeading(heading);
		setColWidth(colWidth);
	}

	public String getHeading() {
		return Heading;
	}

	public void setHeading(String heading) {
		Heading = heading;
	}

	public int getColWidth() {
		return ColWidth;
	}

	public void setColWidth(int colWidth) {
		ColWidth = colWidth;
	}
}
