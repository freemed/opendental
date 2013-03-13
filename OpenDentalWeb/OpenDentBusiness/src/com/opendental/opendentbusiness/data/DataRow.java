package com.opendental.opendentbusiness.data;

public class DataRow {

	private DataCellList Cells;
	
	/** Creates an empty DataRow. */
	public DataRow() {
		Cells=new DataCellList();
	}

	public DataCellList getCells() {
		return Cells;
	}

	public void addCell(String value) {
		Cells.add(new DataCell(value));
	}
	
	public void clearCells() {
		Cells.clear();
	}
	
	
}
