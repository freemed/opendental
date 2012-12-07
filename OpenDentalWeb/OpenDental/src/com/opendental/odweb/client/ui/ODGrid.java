package com.opendental.odweb.client.ui;

import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.VerticalPanel;

/** A custom data grid.  This class contains panels, grids, labels, and other "widgets" that compose our data grid for the UI. Treat ODGrid like a Panel. */
public class ODGrid extends Composite {
	/** A simple panel that will contain all the widgets that compose the ODGrid. */
	private VerticalPanel Container;
	/** The body portion of the table.  The columns, rows, and cells. */
	private TableBody Body;
	/** The column headers for the table. */
	private TableColumnHeaders ColumnHeaders;
	/** This is used so that the resizing of the grid does not happen until all the information is done being added. */
	private boolean IsUpdating;
	/** The total height of the grid. */
	private int GridH;
	/** The total width of the grid. */
	private int GridW;
	/** Uses the ColWidth of each column to set up this array with one element for each column.  Contains the columns Pos for that column. */
	private int[] ColPos;
	/**  */
	public ODGridColumnCollection Columns;
	/**  */
	public ODGridRowCollection Rows;
	
	public ODGrid() {
		Container=new VerticalPanel();
		Body=new TableBody();
		ColumnHeaders=new TableColumnHeaders();
		//We have to call initWidget in the constructor because this class extends Composite. 
		initWidget(Container);
	}
	
	//Computations---------------------------------------------------------------------------------------------------------------
	
	/** Computes the position of each column and the overall width.  Called from endUpdate. */
	private void ComputeColumns() {
		// TODO Enhance ODGrid to have a horizontal scrollbar.
		//Layout the horizontal scrollbar here.
		ColPos=new int[Columns.size()];
		for(int i=0;i<ColPos.length;i++) {
			ColPos[i]=0;
			if(i>0) {
				ColPos[i]=ColPos[i-1]+Columns.get(i-1).GetColWidth();
			}
		}
		if(Columns.size()>0) {
			GridW=ColPos[ColPos.length-1]+Columns.get(Columns.size()-1).GetColWidth();
		}
	}
	
	/** After adding rows to the grid, this calculates the height of each row because some rows may have text wrap and if column images are used, rows will be enlarged to make space for the images. */
	private void ComputeRows() {
		
	}
	
	//BeginEndUpdate-------------------------------------------------------------------------------------------------------------
	
	/** Call this before adding any rows.  You would typically call Rows.clear() after this. */
	public void BeginUpdate() {
		IsUpdating=true;
	}
	
	/** Must be called after adding rows.  This computes the columns, computes the rows, lays out the scrollbars, clears SelectedIndices, etc. */
	public void EndUpdate() {
		ComputeColumns();
		ComputeRows();
		// TODO Enhance ODGrid to have a scrollbar.
		//Layout the scrollbar and set the values here.
		IsUpdating=false;
	}
	
	
	//Properties-----------------------------------------------------------------------------------------------------------------
	
	/** Adds a column to the table with a header of the passed in text. */
	public void AddColumn(String header) {
		//Add the column.
		Body.AddColumn();
		//Now add the column header.
		ColumnHeaders.AddColumn(header);
	}

	/** Adds a column to the table with a header of the passed in text. */
	public void AddRow() {
		//Add the row.
		Body.AddRow();
	}
	
	/**  */
	public String GetColumnText(int rowIndex,int columnIndex) {
		return "";
	}
	
	/**  */
	public void setColumnHeader(int columnIndex,String text) {
		
	}
	
	//Column Headers-------------------------------------------------------------------------------------------------------------
	
	/** This class holds the information regarding the column headers.  This is just so users can scroll through the rows and the headers will stay.
	 *  Using a Grid for now that way the columns will definitely line up with the TableBody columns without having to do much extra logic. */
	private static class TableColumnHeaders extends Grid {
		private int ColumnCount;
		
		/** Empty constructor for now. */
		public TableColumnHeaders() {
			
		}
		
		private void AddColumn(String header) {
			ColumnCount++;
			this.resize(1, ColumnCount);
		}
		
	}
	
	//Table Body-----------------------------------------------------------------------------------------------------------------
	
	/** This class holds the information regarding the body of the table.  The columns, rows, and cells.  Extends Grid which is just an HTMLTable. */
	private static class TableBody extends Grid {
		private int ColumnCount;
		private int RowCount;
		
		/** Empty constructor for now. */
		public TableBody() {
			
		}
		
		private void AddColumn() {
			ColumnCount++;
			this.resize(RowCount,ColumnCount);
		}
		
		private void AddRow() {
			RowCount++;
			this.resize(RowCount,ColumnCount);
		}
	}
	
	
}
