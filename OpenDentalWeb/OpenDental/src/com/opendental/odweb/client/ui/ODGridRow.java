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
		SetBold(false);
		SetTag(null);
		SetNote("");
		SetHeight(0);
	}

	public int GetHeight() {
		return Height;
	}

	/** If not set (0), then the row height is computed automatically. */
	public void SetHeight(int height) {
		Height = height;
	}

	public String GetNote() {
		return Note;
	}

	public void SetNote(String note) {
		Note = note;
	}

	public Object GetTag() {
		return Tag;
	}

	/** Used to store any kind of object that is associated with the row. */
	public void SetTag(Object tag) {
		Tag = tag;
	}

	public boolean IsBold() {
		return Bold;
	}

	public void SetBold(boolean bold) {
		Bold = bold;
	}
	
	
}
