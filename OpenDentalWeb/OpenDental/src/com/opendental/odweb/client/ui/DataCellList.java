package com.opendental.odweb.client.ui;

import java.util.ArrayList;

@SuppressWarnings("serial")
public class DataCellList extends ArrayList<DataCell> {

	public void Add(String value) {
		this.add(new DataCell(value));
	}
	
}
