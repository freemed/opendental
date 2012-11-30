package com.opendental.odweb.client.ui;

public class DataColumn {

	/** The name or header of the column. */
	private String Heading;
	/** The width of the column. */
	private int ColWidth;
	
	/** Creates an empty DataColumn and sets the heading to blank and column width to 80. */
	public DataColumn() {
		SetHeading("");
		SetColWidth(80);
	}
	
	/** Creates an empty DataColumn and sets the heading to the passed in value and column width to 80. */
	public DataColumn(String heading) {
		SetHeading(heading);
		SetColWidth(80);
	}
	
	/** Creates a new DataColumn and sets the heading and column width to the passed in values. */
	public DataColumn(String heading,int colWidth) {
		SetHeading(heading);
		SetColWidth(colWidth);
	}
	
	public int GetColWidth() {
		return ColWidth;
	}
	
	public void SetColWidth(int colWidth) {
		ColWidth = colWidth;
	}
	
	public String GetHeading() {
		return Heading;
	}
	
	public void SetHeading(String heading) {
		Heading = heading;
	}
	
	
}
