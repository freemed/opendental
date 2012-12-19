package com.opendental.odweb.client.ui;

import com.google.gwt.core.client.GWT;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.event.dom.client.ClickHandler;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.DockPanel;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.HasHorizontalAlignment;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.ScrollPanel;
import com.google.gwt.user.client.ui.Widget;

/** A custom data grid.  This class contains panels, grids, labels, and other "widgets" that compose our data grid for the UI. Treat ODGrid like a Panel. */
public class ODGrid extends Composite implements ClickHandler {
	private static ODGridUiBinder uiBinder = GWT.create(ODGridUiBinder.class);
	interface ODGridUiBinder extends UiBinder<Widget, ODGrid> {
	}
	
	/** A simple panel that will contain all the widgets that compose the ODGrid. */
	@UiField DockPanel containerPanel;
	/** The title of the table. */
	private String TableTitle;
	/** The bar that contains the title of the table. */
	@UiField Label labelTitle;
	/** This table will strictly be for holding the column headers.  It's in it's own table so that the body of tableMain can be contained in a scrollable panel. */
	@UiField Grid tableColumnHeaders;
	/** The main table portion of the ODGrid.  The columns, rows, and cells.  This will be a simple HTMLTable to start with. */
	@UiField Grid tableMain;
	/**  */
	@UiField ScrollPanel scrollPanel;
	/** This is used so that the resizing of the grid does not happen until all the information is done being added. */
	private boolean IsUpdating;
	/** The total height of the grid. */
	@SuppressWarnings("unused")
	private int GridH;
	/** The total width of the grid. */
	@SuppressWarnings("unused")
	private int GridW;
	/** Uses the ColWidth of each column to set up this array with one element for each column.  Contains the columns position for that column. */
	private int[] ColPos;
	/** Array of the heights of each row. */
	private int[] RowHeights;
	/** Array of each row location. */
	private int[] RowLocs;
	/** The collection of ODGridColumns assigned to ODGrid. */
	public ODGridColumnCollection Columns;
	/** The collection of ODGridRows assigned to ODGrid. */
	public ODGridRowCollection Rows;
	/** The height of the entire control.  Mainly for dictating how to draw the grid to the parent control. */
	public int Height;
	/** The width of the entire control.  Mainly for dictating how to draw the grid to the parent control. */
	public int Width;
	
	/** Creates a new ODGrid. */
	public ODGrid() {
		this("");//Calls another constructor.
	}
	
	/** Creates a new ODGrid and sets the title to the passed in value. */
	public ODGrid(String title) {
		//Fills the @UiField objects.
		uiBinder.createAndBindUi(this);
		Columns=new ODGridColumnCollection();
		Rows=new ODGridRowCollection();
		labelTitle.setHorizontalAlignment(HasHorizontalAlignment.ALIGN_CENTER);
		setTableTitle(title);
		containerPanel.setCellHeight(scrollPanel, "100%");//This is so that tableMain takes up the most space.
		//We have to call initWidget in the constructor because this class extends Composite. 
		initWidget(containerPanel);
	}
	
	//Properties-----------------------------------------------------------------------------------------------------------------
	
	public int getHeight() {
		return Height;
	}

	public void setHeight(int height) {
		Height=height;
	}

	public int getWidth() {
		return Width;
	}

	public void setWidth(int width) {
		Width=width;
	}
	
	/** Sets the height and width and updates the container's size.  This should be called right after instantiating ODGrid so that the window knows how big to draw a possibly empty grid. */
	public void setHeightAndWidth(int width,int height) {
		setHeight(height);
		setWidth(width);
		containerPanel.setSize(width+"px", height+"px");
	}

	public String getTableTitle() {
		return TableTitle;
	}

	public void setTableTitle(String tableTitle) {
		TableTitle=tableTitle;
		labelTitle.setText(TableTitle);
	}
	
	/**  */
	public String getColumnText(int rowIndex,int columnIndex) {
		return "";
	}

	
	//Computations---------------------------------------------------------------------------------------------------------------
	
	/** Computes the position of each column and the overall width.  Called from endUpdate. */
	private void computeColumns() {
		// TODO Enhance ODGrid to have a horizontal scrollbar.
		//Layout the horizontal scrollbar here.
		ColPos=new int[Columns.size()];
		for(int i=0;i<ColPos.length;i++) {
			ColPos[i]=0;
			if(i>0) {
				ColPos[i]=ColPos[i-1]+Columns.get(i-1).getColWidth();
			}
		}
		if(Columns.size()>0) {
			GridW=ColPos[ColPos.length-1]+Columns.get(Columns.size()-1).getColWidth();
		}
	}
	
	/** After adding rows to the grid, this calculates the height of each row because some rows may have text wrap and if column images are used, rows will be enlarged to make space for the images. */
	private void computeRows() {
		RowHeights=new int[Rows.size()];
		RowLocs=new int[Rows.size()];
		GridH=0;
		int cellH;
		for(int i=0;i<Rows.size();i++) {
			RowHeights[i]=0;
			RowLocs[i]=0;
			//Find the tallest column.
			for(int j=0;j<Rows.get(i).Cells.size();j++) {
				int rowHeight=Rows.get(i).getHeight();
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
	private void onPaint() {
		if(IsUpdating) {
			return;
		}
		drawColumnHeaders();
		drawRows();
	}
	
	/** Must be called after computing the columns and rows so that the dimensions of the table are known. */
	private void drawColumnHeaders() {
		//Manipulate the table column headers.
		tableColumnHeaders.clear();
		tableColumnHeaders.resize(1, Columns.size());
		//Loop through all the columns and set the cells text to the column header.
		for(int i=0;i<Columns.size();i++) {
			if(i==Columns.size()-1) {//If this is the last column in the list, make it span the rest of the grid.
				tableColumnHeaders.getCellFormatter().setWidth(0,i,Width-ColPos[ColPos.length-1]+"px");//Make this column span to the end of the grid.
			}
			else {//Not the last column in the list.
				//Set the width of the cell.
				tableColumnHeaders.getCellFormatter().setWidth(0,i,Columns.get(i).getColWidth()+"px");
				//Set the cell style.
				tableColumnHeaders.getCellFormatter().setStyleName(0,i,"ODGrid_tableColumnHeaders_Cells");
			}
			tableColumnHeaders.setText(0,i,Columns.get(i).getHeading());
		}
	}
	
	/**  */
	private void drawRows() {
		tableMain.clear();
		tableMain.resize(Rows.size(), Columns.size());		
		for(int i=0;i<Rows.size();i++) {
			// TODO Figure out if the row is visible here.
			drawRow(i);//The row is visible so draw it.
		}
	}
	
	/**  */
	private void drawRow(int row) {
		// TODO Figure out if the row is selected here.
		//Draw all of the columns.
		for(int column=0;column<Columns.size();column++) {
			tableMain.setText(row, column, Rows.get(row).Cells.get(column).getText());
			//If this is the last column in the list, make it span the rest of the grid.
			if(column==Columns.size()-1) {
				tableMain.getCellFormatter().setWidth(row,column,Width-ColPos[ColPos.length-1]+"px");//Make this column span to the end of the grid.
			}
			else {//Not the last column in the list.
				//Set the width of the cell.
				tableMain.getCellFormatter().setWidth(row,column,Columns.get(column).getColWidth()+"px");
				//Set the style for the cell.
				if((column % 2)==0) {//Even cells.
					tableMain.getCellFormatter().setStyleName(row,column,"ODGrid_tableMain_EvenCells");
				}
				else {//Odd cells.
					tableMain.getCellFormatter().setStyleName(row,column,"ODGrid_tableMain_OddCells");
				}
			}
		}
	}
	
	//Clicking-------------------------------------------------------------------------------------------------------------------
	
	/** Click handler for the entire ODGrid. */
	@Override
	public void onClick(ClickEvent event) {
		// TODO Enhance the ODGrid to handle click events.		
	}
	
	//BeginEndUpdate-------------------------------------------------------------------------------------------------------------
	
	/** Call this before adding any rows.  You would typically call Rows.clear() after this. */
	public void beginUpdate() {
		IsUpdating=true;
	}
	
	/** Must be called after adding rows.  This computes the columns, computes the rows, lays out the scrollbars, clears SelectedIndices, etc. */
	public void endUpdate() {
		computeColumns();
		computeRows();
		// TODO Enhance ODGrid to have a scrollbar.
		//Layout the scrollbar and set the values here.
		IsUpdating=false;
		//Fill the data grid and refresh it so that it displays correctly.
		onPaint();
	}


	
	
	
	
}
