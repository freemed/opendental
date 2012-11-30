package com.opendental.odweb.client.ui;

public class DataRow {

	private DataCellList Cells;
	
	/** Creates an empty DataRow. */
	public DataRow() {
		Cells=new DataCellList();
	}

	public DataCellList GetCells() {
		return Cells;
	}

	public void AddCell(String value) {
		Cells.add(new DataCell(value));
	}
	
	public void ClearCells() {
		Cells.clear();
	}
	
	
}
