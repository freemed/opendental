package com.opendental.odweb.client.ui;

public class ODGridColumn {
	private String Heading;
	private int ColWidth;

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
