package com.opendental.odweb.client.data;

public class DataColumn {

	/** The name or header of the column. */
	private String Heading;
	
	/** Creates an empty DataColumn. */
	public DataColumn() {
		SetHeading("");
	}
	
	/** Creates an empty DataColumn and sets the heading to the passed in value. */
	public DataColumn(String heading) {
		SetHeading(heading);
	}
	
	public String GetHeading() {
		return Heading;
	}
	
	public void SetHeading(String heading) {
		Heading=heading;
	}
	
	
}
