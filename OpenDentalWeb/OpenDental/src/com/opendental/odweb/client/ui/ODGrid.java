package com.opendental.odweb.client.ui;

import com.google.gwt.canvas.client.Canvas;
import com.google.gwt.canvas.dom.client.Context2d;
import com.google.gwt.canvas.dom.client.CssColor;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.event.dom.client.ClickHandler;
import com.google.gwt.user.client.ui.AbsolutePanel;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.Label;

/** A custom data grid.  This class contains panels, grids, labels, and other "widgets" that compose our data grid for the UI. Treat ODGrid like a Panel. */
public class ODGrid extends Composite implements ClickHandler {
	/** A simple panel that will contain all the widgets that compose the ODGrid. */
	private AbsolutePanel Container;
	/** The title of the table. */
	private String TableTitle;
	/** The bar that contains the title of the table. */
	private Label LabelTitle;
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
	/** Uses the ColWidth of each column to set up this array with one element for each column.  Contains the columns position for that column. */
	private int[] ColPos;
	/**  */
	private int[] RowHeights;
	/**  */
	private int[] RowLocs;
	/** The collection of ODGridColumns assigned to ODGrid. */
	public ODGridColumnCollection Columns;
	/** The collection of ODGridRows assigned to ODGrid. */
	public ODGridRowCollection Rows;
	//Testing
	private Canvas canvas;
	
	/** Creates a new ODGrid. */
	public ODGrid() {
		this("");//Calls another constructor.
	}
	
	/** Creates a new ODGrid and sets the title to the passed in value. */
	public ODGrid(String title) {
		SetTableTitle(title);//So that TableTitle is not null.
		Columns=new ODGridColumnCollection();
		Rows=new ODGridRowCollection();
		Container=new AbsolutePanel();
		LabelTitle=new Label(title);
		ColumnHeaders=new TableColumnHeaders();
		Body=new TableBody();		
		canvas=Canvas.createIfSupported();
		canvas.setHeight("100px");
		canvas.setWidth("100px");
		canvas.setCoordinateSpaceHeight(100);
		canvas.setCoordinateSpaceWidth(100);
		Container.add(LabelTitle);
		Container.add(ColumnHeaders);
		Container.add(Body);
		if(canvas==null) {
			Container.add(new Label("Your browser does not support HTML5.  Please upgrade your browser to view this window."));
		}
		else {
			Container.add(canvas);
		}
		//We have to call initWidget in the constructor because this class extends Composite. 
		initWidget(Container);
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
	
	public String GetTableTitle() {
		return TableTitle;
	}

	public void SetTableTitle(String tableTitle) {
		TableTitle = tableTitle;
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
		RowHeights=new int[Rows.size()];
		RowLocs=new int[Rows.size()];
		GridH=0;
		int cellH;
		for(int i=0;i<Rows.size();i++) {
			RowHeights[i]=0;
			RowLocs[i]=0;
			//Find the tallest column.
			for(int j=0;j<Rows.get(i).Cells.size();j++) {
				int rowHeight=Rows.get(i).GetHeight();
				if(rowHeight>0) {
					cellH=rowHeight;
				}
				else {
					cellH=10;// TODO Figure out how to measure text in the row.
				}
				if(cellH>RowHeights[i]) {
					RowHeights[i]=cellH;
				}
			}
			if(i>0) {
				RowLocs[i]=RowLocs[i-1]+RowHeights[i-1];
			}
			GridH+=RowHeights[i];
		}
	}

	//Painting-------------------------------------------------------------------------------------------------------------------
	
	/** Resets the title and column headers in case the table's size has changed. */
	private void OnPaint() {
		if(IsUpdating) {
			return;
		}
		DrawBackG();
		DrawRows();
		DrawTitleAndHeaders();
		DrawOutline();
	}
	
	/**  */
	private void DrawBackG() {
		//Since we are using an AbsolutePanel as the container, we'll be able to have a widget pose as the background.
		Context2d context=canvas.getContext2d();
		context.setFillStyle(CssColor.make("red"));
		context.fillRect(0, 0, canvas.getCoordinateSpaceWidth(), canvas.getCoordinateSpaceHeight());
	}
	
	/**  */
	private void DrawRows() {
		for(int i=0;i<Rows.size();i++) {
			// TODO Figure out if the row is visible here.
			//The row is visible so draw it.
			DrawRow(i);
		}
	}
	
	/**  */
	private void DrawRow(int rowI) {
		Context2d context=canvas.getContext2d();
		context.lineTo(50.5,0.5);
		context.stroke();
		context.lineTo(50.5,50.5);
		context.stroke();
		context.lineTo(0.5, 50.5);
		context.stroke();
		context.lineTo(0.5, 0.5);
		context.stroke();
		
		//Figure out if the row is selected.
		//Draw all of the columns.
		for(int i=0;i<Columns.size();i++) {
		}
	}
	
	/**  */
	private void DrawTitleAndHeaders() {
		
	}
	
	/**  */
	private void DrawOutline() {
		//Since we are using an AbsolutePanel as the container, we'll be able to have a widget pose as the outline.
	}
	
	//Clicking-------------------------------------------------------------------------------------------------------------------
	
	/** Click handler for the entire ODGrid. */
	@Override
	public void onClick(ClickEvent event) {
		// TODO Enhance the ODGrid to handle click events.		
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
		//Fill the data grid and refresh it so that it displays correctly.
		OnPaint();
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
