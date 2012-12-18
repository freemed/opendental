package com.opendental.odweb.client.ui;

import java.util.ArrayList;

public class ODGridRow {
	public ODGridCellList Cells;
	private int Height;
	private boolean Bold;
	private Object Tag;
	private String Note;
	
	/** A simple list of ODGridCells. */
	@SuppressWarnings("serial")
	public class ODGridCellList extends ArrayList<ODGridCell> {
		public void Add(String text) {
			super.add(new ODGridCell(text));			
		}
	}
	
	/** Creates a new ODGridRow. */
	public ODGridRow() {
		Cells=new ODGridCellList();
		setBold(false);
		setTag(null);
		setNote("");
		setHeight(0);
	}

	public int getHeight() {
		return Height;
	}

	/** If not set (0), then the row height is computed automatically. */
	public void setHeight(int height) {
		Height = height;
	}

	public String getNote() {
		return Note;
	}

	public void setNote(String note) {
		Note = note;
	}

	public Object getTag() {
		return Tag;
	}

	/** Used to store any kind of object that is associated with the row. */
	public void setTag(Object tag) {
		Tag = tag;
	}

	public boolean isBold() {
		return Bold;
	}

	public void setBold(boolean bold) {
		Bold = bold;
	}
	
	
}
