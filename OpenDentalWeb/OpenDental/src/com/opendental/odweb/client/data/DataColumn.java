package com.opendental.odweb.client.data;

public class DataColumn {

	/** The name or header of the column. */
	private String Heading;
	
	/** Creates an empty DataColumn. */
	public DataColumn() {
		setHeading("");
	}
	
	/** Creates an empty DataColumn and sets the heading to the passed in value. */
	public DataColumn(String heading) {
		setHeading(heading);
	}
	
	public String getHeading() {
		return Heading;
	}
	
	public void setHeading(String heading) {
		Heading=heading;
	}
	
	
}
