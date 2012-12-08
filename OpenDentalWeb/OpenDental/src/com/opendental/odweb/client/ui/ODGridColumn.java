package com.opendental.odweb.client.ui;

public class ODGridColumn {
	private String Heading;
	private int ColWidth;
	
	/** Creates a new ODGridColumn with a default column width of 80. */
	public ODGridColumn() {
		SetHeading("");//So that Heading will not be null.
		SetColWidth(80);
	}
	
	/** Creates a new ODGridColumn and sets the heading and column width to the passed in values. */
	public ODGridColumn(String heading,int colWidth) {
		SetHeading(heading);
		SetColWidth(colWidth);
	}

	public String GetHeading() {
		return Heading;
	}

	public void SetHeading(String heading) {
		Heading = heading;
	}

	public int GetColWidth() {
		return ColWidth;
	}

	public void SetColWidth(int colWidth) {
		ColWidth = colWidth;
	}
}
