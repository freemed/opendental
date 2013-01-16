package com.opendental.odweb.client.ui;

import java.util.ArrayList;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.Scheduler;
import com.google.gwt.core.client.Scheduler.ScheduledCommand;
import com.google.gwt.dom.client.Element;
import com.google.gwt.dom.client.NativeEvent;
import com.google.gwt.dom.client.TableCellElement;
import com.google.gwt.dom.client.TableRowElement;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.event.dom.client.ClickHandler;
import com.google.gwt.event.dom.client.HasMouseDownHandlers;
import com.google.gwt.event.dom.client.HasMouseMoveHandlers;
import com.google.gwt.event.dom.client.HasMouseUpHandlers;
import com.google.gwt.event.dom.client.MouseDownEvent;
import com.google.gwt.event.dom.client.MouseDownHandler;
import com.google.gwt.event.dom.client.MouseEvent;
import com.google.gwt.event.dom.client.MouseMoveEvent;
import com.google.gwt.event.dom.client.MouseMoveHandler;
import com.google.gwt.event.dom.client.MouseUpEvent;
import com.google.gwt.event.dom.client.MouseUpHandler;
import com.google.gwt.event.shared.HandlerRegistration;
import com.google.gwt.resources.client.CssResource;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.Event;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.DockPanel;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.HTMLTable.Cell;
import com.google.gwt.user.client.ui.HasHorizontalAlignment;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.ScrollPanel;
import com.google.gwt.user.client.ui.Widget;

/** A custom data grid.  This class contains panels, grids, labels, and other "widgets" that compose our data grid for the UI. Treat ODGrid like a Panel. */
public class ODGrid extends Composite {
	private static ODGridUiBinder uiBinder = GWT.create(ODGridUiBinder.class);
	interface ODGridUiBinder extends UiBinder<Widget, ODGrid> {
	}
	
	/** This is going to allow me to have programmatic access to specified UiBinder styles. */
	interface RowStyle extends CssResource {
		/** This row formatter is used to color selected rows in the main grid. */
		String tableMainSelectedRowFormatter();
	}
	/** RowStyle is strictly used to refer to the CSS portion of the UiBinder file programmatically. */
	@UiField RowStyle style;
	
	/** A simple panel that will contain all the widgets that compose the ODGrid. */
	@UiField DockPanel containerPanel;
	/** The title of the table. */
	private String TableTitle;
	/** The bar that contains the title of the table. */
	@UiField Label labelTitle;
	/** This table will strictly be for holding the column headers.  It's in it's own table so that the body of tableMain can be contained in a scrollable panel. */
	@UiField GridWithMouse tableColumnHeaders;
	/** The main table portion of the ODGrid.  The columns, rows, and cells.  This will be a simple HTMLTable to start with. */
	@UiField GridWithMouse tableMain;
	/** The scroll panel that will contain the column header and main grids.  It will automatically create horizontal and vertical scroll bars as needed. */
	@UiField ScrollPanel scrollPanel;
	/** This is used so that the resizing of the grid does not happen until all the information is done being added. */
	private boolean IsUpdating;
	/** The total height of the grid. */
	@SuppressWarnings("unused")
	private int GridH;
	/** The total width of the grid. */
	private int GridW;
	/** Uses the ColWidth of each column to set up this array with one element for each column.  Contains the columns position for that column. */
	private int[] ColPos;
	/** Holds the width of every column.  Mainly used because figuring out the last column width is tricky. */
	private int[] ColWidths;
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
	/** The currently selected row in the grid.  Defaults to -1 for no row selected. */
	public ArrayList<Integer> SelectedIndices;
	private ArrayList<Integer> SelectedIndicesWhenMouseDown;
	/** The type of selection mode the ODGrid is in.  Defaults to one. */
	private GridSelectionMode SelectionMode=GridSelectionMode.One;
	private boolean MouseIsDown;
	private int MouseDownRow;
	
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
		SelectedIndices=new ArrayList<Integer>();
		labelTitle.setHorizontalAlignment(HasHorizontalAlignment.ALIGN_CENTER);
		setTableTitle(title);
		containerPanel.setCellHeight(scrollPanel, "100%");//This is so that tableMain takes up the most space.
		//Add click handlers to column headers.
		tableColumnHeaders.addClickHandler(new tableColumnHeaders_Click());
		//Add mouse handlers to table main.
		tableMain.addMouseDownHandler(new tableMain_MouseDown());
		tableMain.addMouseUpHandler(new tableMain_MouseUp());
		tableMain.addMouseMoveHandler(new tableMain_MouseMove());
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
	public void setHeightAndWidth(final int width,final int height) {
		setHeight(height);
		setWidth(width);
		containerPanel.setSize(width+"px", height+"px");
		scrollPanel.setSize(width+"px", height+"px");//Scroll panel height will get set correctly with the deferred scheduler.  This is just so the window centers itself correctly in the browser.
		//We have to use a deferred scheduler to set the height of the scroll panel because the widgets have yet to be drawn therefore their dimensions are 0.
		Scheduler.get().scheduleDeferred(new ScheduledCommand() {
			@Override
			public void execute() {
				int scrollHeight=height-labelTitle.getElement().getClientHeight();
				if(scrollHeight<10) {
					scrollHeight=10;//Just in case.
				}
				scrollPanel.setSize(width+"px", (scrollHeight)+"px");
			}
		});
	}

	public String getTableTitle() {
		return TableTitle;
	}

	public void setTableTitle(String tableTitle) {
		TableTitle=tableTitle;
		labelTitle.setText(TableTitle);
	}

	public GridSelectionMode getSelectionMode() {
		return SelectionMode;
	}

	public void setSelectionMode(GridSelectionMode selectionMode) {
		SelectionMode=selectionMode;
	}

	/**  */
	public String getColumnText(int rowIndex,int columnIndex) {
		return "";
	}

	//Computations---------------------------------------------------------------------------------------------------------------
	
	/** Computes the position of each column and the overall width.  Called from endUpdate. */
	private void computeColumns() {
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
		ColWidths=new int[Columns.size()];
		//Manipulate the table column headers.
		tableColumnHeaders.clear();
		tableColumnHeaders.resize(1, Columns.size());
		tableColumnHeaders.setSize("100%", "100%");
		//Loop through all the columns and set the cells text to the column header.
		for(int i=0;i<Columns.size();i++) {
			tableColumnHeaders.setText(0,i,Columns.get(i).getHeading());
			int columnWidth=Columns.get(i).getColWidth();
			//Set the width of the cell.
			tableColumnHeaders.getCellFormatter().setWidth(0,i,columnWidth+"px");
			if(i==Columns.size()-1) {//This is the last column in the list, make it span the rest of the grid if there is no horizontal scroll bar.
				if(Width>GridW) {//The grid is wide enough to contain all the columns.
					int columnSpacing=(5*Columns.size())-(2*(Columns.size()));//Every cell has spacing before and after it.  This accommodates for that space.
					columnWidth=Width-ColPos[ColPos.length-1]-columnSpacing;//Take the entire width of the container minus the set widths, minus the spacing.
					tableColumnHeaders.getCellFormatter().setWidth(0,i,(columnWidth)+"px");//Make this column span to the end of the grid.
				}
			}
			ColWidths[i]=columnWidth;
		}
	}
	
	/**  */
	private void drawRows() {
		tableMain.setVisible(false);
		tableMain.resize(Rows.size(), Columns.size());
		tableMain.setSize("100%", "100%");
		for(int i=0;i<Rows.size();i++) {
			if(i==0) {
				//Only show the main table if there are rows to show.  Otherwise a white anomaly will show.
				tableMain.setVisible(true);
			}
			drawRow(i);
		}
		//Set the selected rows.
		for(int i=0;i<SelectedIndices.size();i++) {
			tableMain.getRowFormatter().addStyleName(SelectedIndices.get(i), style.tableMainSelectedRowFormatter());
		}
	}
	
	/** Must be called after the column headers are drawn.  That is where ColWidths gets filled. */
	private void drawRow(int row) {
		//Remove the selected row format.  The row will get reselected later if needed.
		tableMain.getRowFormatter().removeStyleName(row, style.tableMainSelectedRowFormatter());
		//Draw all of the columns.
		for(int column=0;column<Columns.size();column++) {
			tableMain.setText(row, column, Rows.get(row).Cells.get(column).getText());
			//Set the width of the cell.
			tableMain.getCellFormatter().setWidth(row,column,ColWidths[column]+"px");
		}
	}
	
	//Clicking-------------------------------------------------------------------------------------------------------------------
	
	/** Click handler for table column headers. This is where the sorting will happen if we decide to implement that functionality. */
	public class tableColumnHeaders_Click implements ClickHandler {
		@Override
		public void onClick(ClickEvent event) {
			//Check if the click event was on the column headers.
			Cell cell=tableColumnHeaders.getCellForEvent(event);
			//Can be null if the event did not hit the table or specific cell.
			if(cell==null) {
				return; 
			}
			// TODO enhance to handle sorting columns here.
		}
	}
	
	//Selections-----------------------------------------------------------------------------------------------------------------

	/** Sets the grids selected index to the passed in index. */
	public void setSelected(int index,boolean setValue) {
		int[] iArray={ index };
		setSelected(iArray,setValue);
	}

	/** Sets the grids selected indices to the passed in index array.  Does not select negative indices. */
	public void setSelected(int[] iArray,boolean setValue) {
		SelectedIndices.clear();
		for(int i=0;i<iArray.length;i++) {
			if(iArray[i]<0) {
				continue;
			}
			SelectedIndices.add(iArray[i]);
		}
		drawRows();
	}

	/** Sets all rows to specified value if the selection mode is set to MultiExtended OR One and setValue is set to false. */
	public void setSelected(boolean setValue) {
		if(SelectionMode==GridSelectionMode.None) {
			return;
		}
		if(SelectionMode==GridSelectionMode.One && setValue==true) {
			return;
		}
		if(SelectionMode==GridSelectionMode.OneCell) {
			return;
		}
		SelectedIndices.clear();
		if(setValue) {//Select all the rows.
			for(int i=0;i<Rows.size();i++) {
				SelectedIndices.add(i);
			}
		}
		drawRows();
	}
	
	/** If one row is selected, it returns the index to that row.  If more than one row are selected, it returns the first selected row.
	 *  Really only useful for SelectionMode.One.  If no rows selected, returns -1. */
	public int getSelectedIndex() {
		if(SelectedIndices.size()>0) {
			return SelectedIndices.get(0);
		}
		return -1;
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
		IsUpdating=false;
		SelectedIndices=new ArrayList<Integer>();
		//Fill the data grid and refresh it so that it displays correctly.
		onPaint();
	}
	
	//MouseEvents----------------------------------------------------------------------------------------------------------------
	
	//Table Main Mouse Events
	
	public class tableMain_MouseDown implements MouseDownHandler {
		@Override
		public void onMouseDown(MouseDownEvent event) {
			event.preventDefault();
			int button=event.getNativeButton();
			if(button==NativeEvent.BUTTON_RIGHT) {
				if(SelectedIndices.size()>0) {//If there are already rows selected, then ignore right click.
					return;
				}
				//Otherwise, row will be selected.  Useful when using context menu.
			}
			MouseIsDown=true;
			//Figure out which cell was clicked.
			Cell cell=((GridWithMouse)event.getSource()).getCellForEvent(event);
			if(cell==null) {
				return;//Mouse did not click on a cell.
			}
			MouseDownRow=cell.getRowIndex();
			//Check what type of selection mode the grid is in.
			switch(getSelectionMode()) {
				case None:
					return;
				case One:
					setSelected(MouseDownRow,true);
					break;
				case OneCell:
					// TODO Enhance ODGrid to allow text boxes in cells.
					break;
				case MultiExtended:
					if(event.isControlKeyDown()) {
						//We need to remember exactly which rows were selected the moment the mouse down started.
						//Then, if the mouse gets dragged up or down, the rows between mouse start and mouse end
						//will be set to the opposite of these remembered values.
						SelectedIndicesWhenMouseDown=new ArrayList<Integer>(SelectedIndices);
						if(SelectedIndicesWhenMouseDown.contains(MouseDownRow)) {
							SelectedIndices.remove(MouseDownRow);
						}
						else {
							SelectedIndices.add(MouseDownRow);
						}
					}
					else if(event.isShiftKeyDown()) {
						if(SelectedIndices.size()==0) {
							SelectedIndices.add(MouseDownRow);
						}
						else {
							int fromRow=SelectedIndices.get(0);
							SelectedIndices.clear();
							if(MouseDownRow<fromRow) {//dragging down
								for(int i=MouseDownRow;i<=fromRow;i++) {
									SelectedIndices.add(i);
								}
							}
							else {
								for(int i=fromRow;i<=MouseDownRow;i++) {
									SelectedIndices.add(i);
								}
							}
						}
					}
					else {//Ctrl or Shift not down.
						setSelected(MouseDownRow,true);
					}
					break;
			}
			drawRows();
		}
	}
	
	public class tableMain_MouseUp implements MouseUpHandler {
		@Override
		public void onMouseUp(MouseUpEvent event) {
			MouseIsDown=false;
		}
	}
	
	public class tableMain_MouseMove implements MouseMoveHandler {
		@Override
		public void onMouseMove(MouseMoveEvent event) {
			if(!MouseIsDown) {
				return;
			}
			if(SelectionMode!=GridSelectionMode.MultiExtended) {
				return;
			}
			Cell cell=((GridWithMouse)event.getSource()).getCellForEvent(event);
			if(cell==null) {
				return;
			}
			int curRow=cell.getRowIndex();
			//Because the mouse might have moved faster than the computer could keep up, we have to loop through all rows between.
			if(event.isControlKeyDown()) {
				if(SelectedIndicesWhenMouseDown==null) {
					SelectedIndices=new ArrayList<Integer>();
				}
				else {
					SelectedIndices=new ArrayList<Integer>(SelectedIndicesWhenMouseDown);
				}
			}
			else {
				SelectedIndices=new ArrayList<Integer>();
			}
			if(MouseDownRow<curRow) {//dragging down
				for(int i=MouseDownRow;i<=curRow;i++) {
					if(i==-1) {
						continue;
					}
					if(SelectedIndices.contains(i)) {
						SelectedIndices.remove(i);
					}
					else {
						SelectedIndices.add(i);
					}
				}
			}
			else {//dragging up
				for(int i=curRow;i<=MouseDownRow;i++) {
					if(SelectedIndices.contains(i)) {
						SelectedIndices.remove(i);
					}
					else {
						SelectedIndices.add(i);
					}
				}
			}
			drawRows();
		}
	}

	/** A grid that has mouse events.  Used to figure out what cell the user has clicked on and allows for click and drag functionalities. */
	public static class GridWithMouse extends Grid implements HasMouseMoveHandlers,HasMouseDownHandlers,HasMouseUpHandlers {
		@SuppressWarnings("rawtypes")
		public Cell getCellForEvent(MouseEvent event) {
			Element td=getEventTargetCell(Event.as(event.getNativeEvent()));
			if(td==null) {
				return null;
			}
			int row=TableRowElement.as(td.getParentElement()).getSectionRowIndex();
		int column=TableCellElement.as(td).getCellIndex();
		return new Cell(row, column);
		}
		
		@Override
		public HandlerRegistration addMouseMoveHandler(MouseMoveHandler handler) {
			return this.addDomHandler(handler, MouseMoveEvent.getType());
		}

		@Override
		public HandlerRegistration addMouseUpHandler(MouseUpHandler handler) {
			return this.addDomHandler(handler, MouseUpEvent.getType());
		}

		@Override
		public HandlerRegistration addMouseDownHandler(MouseDownHandler handler) {
			return this.addDomHandler(handler, MouseDownEvent.getType());
		}
		
		public class Cell extends com.google.gwt.user.client.ui.HTMLTable.Cell {
			public Cell(int rowIndex, int cellIndex) {
			super(rowIndex, cellIndex);
		}
		}
	}

	/** Specifies the selection behavior of an ODGrid. */
	public enum GridSelectionMode {
		/** 0=None */
		None,
		/** 1=One */
		One,
		/** 2=OneCell */
		OneCell,
		/** 3=MultiExtended */
		MultiExtended
	}

	
	
	
	
}
