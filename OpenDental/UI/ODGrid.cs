using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.UI {
	///<summary></summary>
	public delegate void ODGridClickEventHandler(object sender,ODGridClickEventArgs e);

	///<summary>A new and improved grid control to replace the inherited ContrTable that is used so extensively in the program.</summary>
	[DefaultEvent("CellDoubleClick")]
	public class ODGrid:System.Windows.Forms.UserControl {
		///<summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		private ODGridColumnCollection columns;
		///<summary></summary>
		[Category("Action"),Description("Occurs when a cell is double clicked.")]
		public event ODGridClickEventHandler CellDoubleClick=null;
		///<summary></summary>
		[Category("Action"),Description("Occurs when a cell is single clicked.")]
		public event ODGridClickEventHandler CellClick=null;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when user types in a textbox.")]
		public event EventHandler CellTextChanged=null;
		private string title;
		//private Font titleFont=new Font(FontFamily.GenericSansSerif,10,FontStyle.Bold);
		//private Font headerFont=new Font(FontFamily.GenericSansSerif,8.5f,FontStyle.Bold);
		//private Font cellFont=new Font(FontFamily.GenericSansSerif,8.5f);
		private float cellFontSize=8.5f;
		private int titleHeight=18;
		private int headerHeight=15;
		private Color cGridLine=Color.FromArgb(180,180,180);
		//Color.FromArgb(211,211,211);
		//Color.FromArgb(192,192,192);
		//Color.FromArgb(157,157,161);
		private Color cTitleBackG=Color.FromArgb(210,210,210);
		//(224,223,227);
		private Color cBlueOutline=Color.FromArgb(119,119,146);
		private System.Windows.Forms.VScrollBar vScroll;
		private System.Windows.Forms.HScrollBar hScroll;
		private ODGridRowCollection rows;
		private bool IsUpdating;
		///<summary>The total height of the grid.</summary>
		private int GridH;
		///<summary>The total width of the grid.</summary>
		private int GridW;
		///<summary>This array has one element for each row.  For each row, it keeps track of the vertical height of the row in pixels, not counting the note portion of the row.</summary>
		private int[] RowHeights;
		///<summary>This array has one element for each row.  For each row, it keeps track of the vertical height of only the note portion of the row in pixels.  Usually 0, unless you want notes showing.</summary>
		private int[] NoteHeights;
		///<summary>This array has one element for each row.  For each row, it keeps track of the vertical location at which to start drawing this row in pixels.  This makes it much easier to paint rows.</summary>
		private int[] RowLocs;
		private bool hScrollVisible;
		///<summary>Set at the very beginning of OnPaint.  Uses the ColWidth of each column to set up this array with one element for each column.  Contains the columns Pos for that column.</summary>
		private int[] ColPos;
		private ArrayList selectedIndices;
		private Point selectedCell;
		private int MouseDownRow;
		private int MouseDownCol;
		private bool ControlIsDown;
		private bool ShiftIsDown;
		//private bool UpDownKey;
		private GridSelectionMode selectionMode;
		private bool MouseIsDown;
		private bool MouseIsOver;//helps automate scrolling
		private string translationName;
		private Color selectedRowColor;
		private bool allowSelection;
		private bool wrapText;
		private int noteSpanStart;
		private int noteSpanStop;
		private TextBox editBox;
		private MouseButtons lastButtonPressed;
		private ArrayList selectedIndicesWhenMouseDown;
		private bool allowSortingByColumn;
		private bool mouseIsDownInHeader;
		///<summary>Typically -1 to show no triangle.  Or, specify a column to show a triangle.  The actual sorting happens at mouse down.</summary>
		private int sortedByColumnIdx;
		///<summary>True to show a triangle pointing up.  False to show a triangle pointing down.  Only works if sortedByColumnIdx is not -1.</summary>
		private bool sortedIsAscending;
		private List<List<int>> multiPageNoteHeights;//If NoteHeights[i] won't fit on one page, get various page heights here (printing).
		private List<List<string>> multiPageNoteSection;//Section of the note that is split up across multiple pages.

		///<summary></summary>
		public ODGrid() {
			//InitializeComponent();// Required for Windows.Forms Class Composition Designer support
			//Add any constructor code after InitializeComponent call
			columns=new ODGridColumnCollection();
			rows=new ODGridRowCollection();
			vScroll=new VScrollBar();
			vScroll.Scroll+=new ScrollEventHandler(vScroll_Scroll);
			vScroll.MouseEnter+=new EventHandler(vScroll_MouseEnter);
			vScroll.MouseLeave+=new EventHandler(vScroll_MouseLeave);
			vScroll.MouseMove+=new MouseEventHandler(vScroll_MouseMove);
			hScroll=new HScrollBar();
			hScroll.Scroll+=new ScrollEventHandler(hScroll_Scroll);
			hScroll.MouseEnter+=new EventHandler(hScroll_MouseEnter);
			hScroll.MouseLeave+=new EventHandler(hScroll_MouseLeave);
			hScroll.MouseMove+=new MouseEventHandler(hScroll_MouseMove);
			this.Controls.Add(vScroll);
			this.Controls.Add(hScroll);
			selectedIndices=new ArrayList();
			selectedCell=new Point(-1,-1);
			selectionMode=GridSelectionMode.One;
			selectedRowColor=Color.Silver;
			allowSelection=true;
			wrapText=true;
			noteSpanStart=0;
			noteSpanStop=0;
			sortedByColumnIdx=-1;
		}

		///<summary>Clean up any resources being used.</summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			components = new System.ComponentModel.Container();
		}
		#endregion

		///<summary></summary>
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			if(this.Parent!=null) {
				this.Parent.MouseWheel+=new MouseEventHandler(Parent_MouseWheel);
				this.Parent.KeyDown+=new KeyEventHandler(Parent_KeyDown);
				this.Parent.KeyUp+=new KeyEventHandler(Parent_KeyUp);
			}
		}

		///<summary></summary>
		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
			LayoutScrollBars();
			Invalidate();
		}

		#region Properties
		///<summary>Gets the collection of ODGridColumns assigned to the ODGrid control.</summary>
		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		//[Editor(typeof(System.ComponentModel.Design.CollectionEditor),typeof(System.Drawing.Design.UITypeEditor))]
		//[Browsable(false)]//only because MS is buggy.
		public ODGridColumnCollection Columns {
			get {
				return columns;
			}
		}

		///<summary>Gets the collection of ODGridRows assigned to the ODGrid control.</summary>
		[Browsable(false)]
		public ODGridRowCollection Rows {
			get {
				return rows;
			}
		}

		///<summary>The title of the grid which shows across the top.</summary>
		[Category("Appearance"),Description("The title of the grid which shows across the top.")]
		public string Title {
			get {
				return title;
			}
			set {
				title=value;
				Invalidate();
			}
		}

		///<summary>Set true to show a horizontal scrollbar.  Vertical scrollbar always shows, but is disabled if not needed.  If hScroll is not visible, then grid will auto reset width to match width of columns.</summary>
		[Category("Appearance"),Description("Set true to show a horizontal scrollbar.")]
		public bool HScrollVisible {
			get {
				return hScrollVisible;
			}
			set {
				hScrollVisible=value;
				LayoutScrollBars();
				Invalidate();
			}
		}

		///<summary>Gets or sets the position of the vertical scrollbar.  Does all error checking and invalidates.</summary>
		[Browsable(false)]
		public int ScrollValue {
			get {
				return vScroll.Value;
			}
			set {
				if(!vScroll.Enabled) {
					return;
				}
				if(value>vScroll.Maximum-vScroll.LargeChange)
					vScroll.Value=vScroll.Maximum-vScroll.LargeChange;
				else if(value<vScroll.Minimum)
					vScroll.Value=vScroll.Minimum;
				else
					vScroll.Value=value;
				if(editBox!=null) {
					editBox.Dispose();
				}
				Invalidate();
			}
		}

		protected override void OnSizeChanged(EventArgs e) {
			base.OnSizeChanged(e);
			ScrollValue=vScroll.Value;
		}

		///<summary>Holds the int values of the indices of the selected rows.  To set selected indices, use SetSelected().</summary>
		[Browsable(false)]
		public int[] SelectedIndices {
			get {
				int[] retVal=new int[selectedIndices.Count];
				selectedIndices.CopyTo(retVal);
				Array.Sort(retVal);//they must be in numerical order
				return retVal;
			}
		}

		///<summary>Holds the x,y values of the selected cell if in OneCell mode.  -1,-1 represents no cell selected.</summary>
		[Browsable(false)]
		public Point SelectedCell {
			get {
				return selectedCell;
			}
		}

		///<summary></summary>
		[Category("Behavior"),Description("Just like the listBox.SelectionMode, except no MultiSimple, and added OneCell.")]
		[DefaultValue(typeof(GridSelectionMode),"One")]
		public GridSelectionMode SelectionMode {
			get {
				return selectionMode;
			}
			set {
				//if((GridSelectionMode)value==SelectionMode.MultiSimple){
				//	MessageBox.Show("MultiSimple not supported.");
				//	return;
				//}
				if((GridSelectionMode)value==GridSelectionMode.OneCell) {
					selectedCell=new Point(-1,-1);//?
					selectedIndices=new ArrayList();
				}
				selectionMode=value;
			}
		}

		///<summary></summary>
		[Category("Behavior"),Description("Set false to disable row selection when user clicks.  Row selection should then be handled by the form using the cellClick event.")]
		[DefaultValue(true)]
		public bool AllowSelection {
			get {
				return allowSelection;
			}
			set {
				allowSelection=value;
			}
		}

		///<summary>Uniquely identifies the grid for translation to another language.</summary>
		[Category("Appearance"),Description("Uniquely identifies the grid for translation to another language.")]
		public string TranslationName {
			get {
				return translationName;
			}
			set {
				translationName=value;
			}
		}

		///<summary>The background color that is used for selected rows.</summary>
		[Category("Appearance"),Description("The background color that is used for selected rows.")]
		[DefaultValue(typeof(Color),"Silver")]
		public Color SelectedRowColor {
			get {
				return selectedRowColor;
			}
			set {
				selectedRowColor=value;
			}
		}

		///<summary>Text within each cell will wrap, making some rows taller.</summary>
		[Category("Behavior"),Description("Text within each cell will wrap, making some rows taller.")]
		[DefaultValue(true)]
		public bool WrapText {
			get {
				return wrapText;
			}
			set {
				wrapText=value;
			}
		}

		///<summary>The starting column for notes on each row.  Notes are not part of the main row, but are displayed on subsequent lines.</summary>
		[Category("Appearance"),Description("The starting column for notes on each row.")]
		[DefaultValue(0)]//typeof(int),0)]
		public int NoteSpanStart {
			get {
				return noteSpanStart;
			}
			set {
				noteSpanStart=value;
			}
		}

		///<summary>The starting column for notes on each row.  Notes are not part of the main row, but are displayed on subsequent lines.  If this remains 0, then notes will be entirey skipped for this grid.</summary>
		[Category("Appearance"),Description("The ending column for notes on each row.")]
		[DefaultValue(0)]//typeof(int),0)]
		public int NoteSpanStop {
			get {
				return noteSpanStop;
			}
			set {
				noteSpanStop=value;
			}
		}

		///<summary>Used when drawing to PDF. We need the width of all columns summed.</summary>
		public int WidthAllColumns {
			get {
				int retVal=0;
				for(int i=0;i<columns.Count;i++) {
					retVal+=columns[i].ColWidth;
				}
				return retVal;
			}
		}

		///<summary>Set true to allow user to click on column headers to sort rows, alternating between ascending and descending.</summary>
		[Category("Behavior"),Description("Set true to allow user to click on column headers to sort rows, alternating between ascending and descending.")]
		[DefaultValue(false)]
		public bool AllowSortingByColumn {
			get {
				return allowSortingByColumn;
			}
			set {
				allowSortingByColumn=value;
				if(!allowSortingByColumn) {
					sortedByColumnIdx=-1;
				}
			}
		}

		#endregion Properties

		#region Computations
		///<summary>Computes the position of each column and the overall width.  Called from endUpdate and also from OnPaint.</summary>
		private void ComputeColumns() {
			if(!hScrollVisible) {//this used to be in the resize logic
				int minGridW=0;//sum of columns widths except last one.
				for(int i=0;i<columns.Count;i++) {
					if(i<columns.Count-1) {
						minGridW+=columns[i].ColWidth;
					}
				}
				if(Width<minGridW+2+vScroll.Width+5) {//trying to make it too narrow
					this.Width=minGridW
					+2//outline
					+vScroll.Width
					+5;
				}
				else if(columns.Count>0) {//resize the last column automatically
					columns[columns.Count-1].ColWidth=Width-2-vScroll.Width-minGridW;
				}
			}
			ColPos=new int[columns.Count];
			for(int i=0;i<ColPos.Length;i++) {
				if(i==0)
					ColPos[i]=0;
				else
					ColPos[i]=ColPos[i-1]+columns[i-1].ColWidth;
			}
			if(columns.Count>0) {
				GridW=ColPos[ColPos.Length-1]+columns[columns.Count-1].ColWidth;
			}
		}

		///<summary>After adding rows to the grid, this calculates the height of each row because some rows may have text wrap and will take up more than one row.  Also, rows with notes, must be made much larger, because notes start on the second line.  If column images are used, rows will be enlarged to make space for the images.</summary>
		private void ComputeRows() {
			using(Graphics g=this.CreateGraphics()) {
				using(Font cellFont=new Font(FontFamily.GenericSansSerif,cellFontSize)) {
					RowHeights=new int[rows.Count];
					NoteHeights=new int[rows.Count];
					multiPageNoteHeights=new List<List<int>>();
					multiPageNoteSection=new List<List<string>>();
					for(int i=0;i<rows.Count;i++) {
						List<int> intList=new List<int>();
						multiPageNoteHeights.Add(intList);
						List<string> stringList=new List<string>();
						multiPageNoteSection.Add(stringList);
					}
					RowLocs=new int[rows.Count];
					GridH=0;
					int cellH;
					int noteW=0;
					if(NoteSpanStop>0 && NoteSpanStart<columns.Count) {
						for(int i=NoteSpanStart;i<=NoteSpanStop;i++) {
							noteW+=columns[i].ColWidth;
						}
					}
					int imageH=0;
					for(int i=0;i<columns.Count;i++) {
						if(columns[i].ImageList!=null) {
							if(columns[i].ImageList.ImageSize.Height>imageH) {
								imageH=columns[i].ImageList.ImageSize.Height+1;
							}
						}
					}
					for(int i=0;i<rows.Count;i++) {
						RowHeights[i]=0;
						if(wrapText) {
							//find the tallest col
							for(int j=0;j<rows[i].Cells.Count;j++) {
								if(rows[i].Height==0) {//not set
									cellH=(int)g.MeasureString(rows[i].Cells[j].Text,cellFont,columns[j].ColWidth).Height+1;
								}
								else {
									cellH=rows[i].Height;
								}
								if(cellH>RowHeights[i]) {
									RowHeights[i]=cellH;
								}
							}
						}
						else {
							if(rows[i].Height==0) {//not set
								RowHeights[i]=(int)g.MeasureString("Any",cellFont).Height+1;
							}
							else {
								RowHeights[i]=rows[i].Height;
							}
						}
						if(imageH>RowHeights[i]) {
							RowHeights[i]=imageH;
						}
						if(noteW>0 && rows[i].Note!="") {
							NoteHeights[i]=(int)g.MeasureString(rows[i].Note,cellFont,noteW).Height;
						}
						if(i==0) {
							RowLocs[i]=0;
						}
						else {
							RowLocs[i]=RowLocs[i-1]+RowHeights[i-1]+NoteHeights[i-1];
						}
						GridH+=RowHeights[i]+NoteHeights[i];
					}
				}
			}
		}

		///<summary>Returns row. -1 if no valid row.  Supply the y position in pixels.</summary>
		public int PointToRow(int y) {
			if(y<1+titleHeight+headerHeight) {
				return -1;
			}
			for(int i=0;i<rows.Count;i++) {
				if(y>-vScroll.Value+1+titleHeight+headerHeight+RowLocs[i]+RowHeights[i]+NoteHeights[i]) {
					continue;//clicked below this row.
				}
				return i;
			}
			return -1;
		}

		///<summary>Returns col.  Supply the x position in pixels. -1 if no valid column.</summary>
		public int PointToCol(int x) {
			int colRight;//the right edge of each column
			for(int i=0;i<columns.Count;i++) {
				colRight=0;
				for(int c=0;c<i+1;c++) {
					colRight+=columns[c].ColWidth;
				}
				if(x>-hScroll.Value+colRight) {
					continue;//clicked to the right of this col
				}
				return i;
			}
			return -1;
		}
		#endregion Computations

		#region Painting
		///<summary></summary>
		protected override void OnPaintBackground(PaintEventArgs pea) {
			//base.OnPaintBackground (pea);
			//don't paint background.  This reduces flickering.
		}

		///<summary>Runs any time the control is invalidated.</summary>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			if(IsUpdating) {
				return;
			}
			if(Width<1 || Height<1) {
				return;
			}
			ComputeColumns();//it's only here because I can't figure out how to do it when columns are added. It will be removed.
			Bitmap doubleBuffer=new Bitmap(Width,Height,e.Graphics);
			using(Graphics g=Graphics.FromImage(doubleBuffer)) {
				g.SmoothingMode=SmoothingMode.HighQuality;//for the up/down triangles
				DrawBackG(g);
				DrawRows(g);
				DrawTitleAndHeaders(g);//this will draw on top of any grid stuff
				DrawOutline(g);
				e.Graphics.DrawImageUnscaled(doubleBuffer,0,0);
			}
			doubleBuffer.Dispose();
			doubleBuffer=null;
		}

		///<summary>Draws a solid gray background.</summary>
		private void DrawBackG(Graphics g) {
			//if(vScroll.Enabled){//all backg white, since no gray will show
			//	g.FillRectangle(new SolidBrush(Color.White),
			//		0,titleHeight+headerHeight+1,
			//		Width,this.Height-titleHeight-headerHeight-1);
			//}
			//else{
			g.FillRectangle(new SolidBrush(Color.FromArgb(224,223,227)),
				0,titleHeight+headerHeight,
				Width,Height-titleHeight-headerHeight);
			//}
		}

		///<summary>Draws the background, lines, and text for all rows that are visible.</summary>
		private void DrawRows(Graphics g) {
			Font cellFont=new Font(FontFamily.GenericSansSerif,cellFontSize);
			try {
				for(int i=0;i<rows.Count;i++) {
					if(-vScroll.Value+RowLocs[i]+RowHeights[i]+NoteHeights[i]<0) {
						continue;//lower edge of row above top of grid area
					}
					if(-vScroll.Value+1+titleHeight+headerHeight+RowLocs[i]>Height) {
						return;//row below lower edge of control
					}
					DrawRow(i,g,cellFont);
				}
			}
			finally {
				if(cellFont!=null) {
					cellFont.Dispose();
				}
			}
		}

		///<summary>Draws background, lines, image, and text for a single row.</summary>
		private void DrawRow(int rowI,Graphics g,Font cellFont) {
			RectangleF textRect;
			StringFormat format=new StringFormat();
			Pen gridPen=new Pen(this.cGridLine);
			Pen lowerPen=new Pen(this.cGridLine);
			if(rowI==rows.Count-1) {//last row
				lowerPen=new Pen(Color.FromArgb(120,120,120));
			}
			else
				if(rows[rowI].ColorLborder!=Color.Empty) {
					lowerPen=new Pen(rows[rowI].ColorLborder);
				}
			SolidBrush textBrush;
			//selected row color
			if(selectedIndices.Contains(rowI)) {
				g.FillRectangle(new SolidBrush(selectedRowColor),
					1,
					-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+1,
					GridW,
					RowHeights[rowI]+NoteHeights[rowI]-1);
			}
			//colored row background
			else if(rows[rowI].ColorBackG!=Color.White) {
				g.FillRectangle(new SolidBrush(rows[rowI].ColorBackG),
					1,
					-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+1,
					GridW,
					RowHeights[rowI]+NoteHeights[rowI]-1);
			}
			//normal row color
			else {//need to draw over the gray background
				g.FillRectangle(new SolidBrush(rows[rowI].ColorBackG),
					1,
					-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+1,
					GridW,//this is a really simple width value that always works well
					RowHeights[rowI]+NoteHeights[rowI]-1);
			}
			if(selectionMode==GridSelectionMode.OneCell && selectedCell.X!=-1 && selectedCell.Y!=-1
			&& selectedCell.Y==rowI) {
				g.FillRectangle(new SolidBrush(selectedRowColor),
					-hScroll.Value+1+ColPos[selectedCell.X],
					-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+1,
					columns[selectedCell.X].ColWidth,
					RowHeights[rowI]+NoteHeights[rowI]-1);
			}
			//lines for note section
			if(NoteHeights[rowI]>0) {
				//left vertical gridline
				if(NoteSpanStart!=0) {
					g.DrawLine(gridPen,
						-hScroll.Value+1+ColPos[NoteSpanStart],
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI],
						-hScroll.Value+1+ColPos[NoteSpanStart],
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI]+NoteHeights[rowI]);
				}
				//Horizontal line which divides the main part of the row from the notes section of the row
				g.DrawLine(gridPen,
					-hScroll.Value+1+ColPos[0]+1,
					-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI],
					-hScroll.Value+1+ColPos[columns.Count-1]+columns[columns.Count-1].ColWidth,
					-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI]);

			}
			for(int i=0;i<columns.Count;i++) {
				//right vertical gridline
				if(rowI==0) {
					g.DrawLine(gridPen,
						-hScroll.Value+1+ColPos[i]+columns[i].ColWidth,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI],
						-hScroll.Value+1+ColPos[i]+columns[i].ColWidth,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI]);
				}
				else {
					g.DrawLine(gridPen,
						-hScroll.Value+1+ColPos[i]+columns[i].ColWidth,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+1,
						-hScroll.Value+1+ColPos[i]+columns[i].ColWidth,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI]);
				}
				//lower horizontal gridline
				if(i==0) {
					g.DrawLine(lowerPen,
						-hScroll.Value+1+ColPos[i],
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI]+NoteHeights[rowI],
						-hScroll.Value+1+ColPos[i]+columns[i].ColWidth,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI]+NoteHeights[rowI]);
				}
				else {
					g.DrawLine(lowerPen,
						-hScroll.Value+1+ColPos[i]+1,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI]+NoteHeights[rowI],
						-hScroll.Value+1+ColPos[i]+columns[i].ColWidth,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI]+NoteHeights[rowI]);
				}
				//text
				if(rows[rowI].Cells.Count-1<i) {
					continue;
				}
				switch(columns[i].TextAlign) {
					case HorizontalAlignment.Left:
						format.Alignment=StringAlignment.Near;
						break;
					case HorizontalAlignment.Center:
						format.Alignment=StringAlignment.Center;
						break;
					case HorizontalAlignment.Right:
						format.Alignment=StringAlignment.Far;
						break;
				}
				int vertical=-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+1;
				int horizontal=-hScroll.Value+1+ColPos[i]+1;
				int cellW=columns[i].ColWidth;
				int cellH=RowHeights[rowI];
				if(rows[rowI].Height!=0) {//eg 19 for handling textbox
					vertical+=2;
					cellH-=3;
				}
				if(columns[i].TextAlign==HorizontalAlignment.Right) {
					if(rows[rowI].Height!=0) {//eg 19 for handling textbox
						horizontal-=4;
						cellW+=2;
					}
					else {
						horizontal-=2;
						cellW+=2;
					}
				}
				textRect=new RectangleF(horizontal,vertical,cellW,cellH);
				if(rows[rowI].Cells[i].ColorText==Color.Empty) {
					textBrush=new SolidBrush(rows[rowI].ColorText);
				}
				else {
					textBrush=new SolidBrush(rows[rowI].Cells[i].ColorText);
				}
				if(rows[rowI].Cells[i].Bold==YN.Yes) {
					cellFont=new Font(cellFont,FontStyle.Bold);
				}
				else if(rows[rowI].Cells[i].Bold==YN.No) {
					cellFont=new Font(cellFont,FontStyle.Regular);
				}
				else {//unknown.  Use row bold
					if(rows[rowI].Bold) {
						cellFont=new Font(cellFont,FontStyle.Bold);
					}
					else {
						cellFont=new Font(cellFont,FontStyle.Regular);
					}
				}
				if(columns[i].ImageList==null) {
					g.DrawString(rows[rowI].Cells[i].Text,cellFont,textBrush,textRect,format);
				}
				else {
					int imageIndex=-1;
					if(rows[rowI].Cells[i].Text!="") {
						imageIndex=PIn.Int(rows[rowI].Cells[i].Text);
					}
					if(imageIndex!=-1) {
						Image img=columns[i].ImageList.Images[imageIndex];
						g.DrawImage(img,horizontal,vertical-1);
					}
				}
			}
			//note text
			if(NoteHeights[rowI]>0 && NoteSpanStop>0 && NoteSpanStart<columns.Count) {
				int noteW=0;
				for(int i=NoteSpanStart;i<=NoteSpanStop;i++) {
					noteW+=columns[i].ColWidth;
				}
				if(rows[rowI].Bold) {
					cellFont=new Font(cellFont,FontStyle.Bold);
				}
				else {
					cellFont=new Font(cellFont,FontStyle.Regular);
				}
				textBrush=new SolidBrush(rows[rowI].ColorText);
				textRect=new RectangleF(
					-hScroll.Value+1+ColPos[NoteSpanStart]+1,
					-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI]+1,
					ColPos[NoteSpanStop]+columns[NoteSpanStop].ColWidth-ColPos[NoteSpanStart],
					NoteHeights[rowI]);
				format.Alignment=StringAlignment.Near;
				g.DrawString(rows[rowI].Note,cellFont,textBrush,textRect,format);
			}
		}

		private void DrawTitleAndHeaders(Graphics g) {
			//Title----------------------------------------------------------------------------------------------------
			g.FillRectangle(new LinearGradientBrush(new Rectangle(0,0,Width,titleHeight+5),
				//Color.FromArgb(172,171,196)
				Color.White,Color.FromArgb(200,200,215),LinearGradientMode.Vertical),0,0,Width,titleHeight);
			using(Font titleFont=new Font(FontFamily.GenericSansSerif,10,FontStyle.Bold)) {
				g.DrawString(title,titleFont,Brushes.Black,Width/2-g.MeasureString(title,titleFont).Width/2,2);
			}
			//Column Headers-----------------------------------------------------------------------------------------
			g.FillRectangle(new SolidBrush(this.cTitleBackG),0,titleHeight,Width,headerHeight);//background
			g.DrawLine(new Pen(Color.FromArgb(102,102,122)),0,titleHeight,Width,titleHeight);//line between title and headers
			using(Font headerFont=new Font(FontFamily.GenericSansSerif,8.5f,FontStyle.Bold)) {
				for(int i=0;i<columns.Count;i++) {
					if(i!=0) {
						//vertical lines separating column headers
						g.DrawLine(new Pen(Color.FromArgb(120,120,120)),-hScroll.Value+1+ColPos[i],titleHeight+3,
							-hScroll.Value+1+ColPos[i],titleHeight+headerHeight-2);
						g.DrawLine(new Pen(Color.White),-hScroll.Value+1+ColPos[i]+1,titleHeight+3,
							-hScroll.Value+1+ColPos[i]+1,titleHeight+headerHeight-2);
					}
					g.DrawString(columns[i].Heading,headerFont,Brushes.Black,
						-hScroll.Value+ColPos[i]+columns[i].ColWidth/2-g.MeasureString(columns[i].Heading,headerFont).Width/2,
						titleHeight+2);
					if(sortedByColumnIdx==i) {
						PointF p=new PointF(-hScroll.Value+1+ColPos[i]+6,titleHeight+(float)headerHeight/2f);
						if(sortedIsAscending) {//pointing up
							g.FillPolygon(Brushes.White,new PointF[] {
								new PointF(p.X-4.9f,p.Y+2f),//LLstub
								new PointF(p.X-4.9f,p.Y+2.5f),//LLbase
								new PointF(p.X+4.9f,p.Y+2.5f),//LRbase
								new PointF(p.X+4.9f,p.Y+2f),//LRstub
								new PointF(p.X,p.Y-2.8f)});//Top
							g.FillPolygon(Brushes.Black,new PointF[] {
								new PointF(p.X-4,p.Y+2),//LL
								new PointF(p.X+4,p.Y+2),//LR
								new PointF(p.X,p.Y-2)});//Top
						}
						else {//pointing down
							g.FillPolygon(Brushes.White,new PointF[] {//shaped like home plate
								new PointF(p.X-4.9f,p.Y-2f),//ULstub
								new PointF(p.X-4.9f,p.Y-2.7f),//ULtop
								new PointF(p.X+4.9f,p.Y-2.7f),//URtop
								new PointF(p.X+4.9f,p.Y-2f),//URstub
								new PointF(p.X,p.Y+2.8f)});//Bottom
							g.FillPolygon(Brushes.Black,new PointF[] {
								new PointF(p.X-4,p.Y-2),//UL
								new PointF(p.X+4,p.Y-2),//UR
								new PointF(p.X,p.Y+2)});//Bottom
						}
					}
				}
			}
			//line below headers
			g.DrawLine(new Pen(Color.FromArgb(120,120,120)),0,titleHeight+headerHeight,Width,titleHeight+headerHeight);
		}

		///<summary>Draws outline around entire control.</summary>
		private void DrawOutline(Graphics g) {
			if(hScroll.Visible) {//for the little square at the lower right between the two scrollbars
				g.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Control)),Width-vScroll.Width-1,
					Height-hScroll.Height-1,vScroll.Width,hScroll.Height);
			}
			g.DrawRectangle(new Pen(this.cBlueOutline),0,0,Width-1,Height-1);
		}
		#endregion

		#region Clicking
		///<summary></summary>
		protected void OnCellDoubleClick(int col,int row) {
			ODGridClickEventArgs gArgs=new ODGridClickEventArgs(col,row,MouseButtons.Left);
			if(CellDoubleClick!=null) {
				CellDoubleClick(this,gArgs);
			}
		}

		///<summary></summary>
		protected override void OnDoubleClick(EventArgs e) {
			base.OnDoubleClick(e);
			if(MouseDownRow==-1) {
				return;//double click was in the title or header section
			}
			if(MouseDownCol==-1) {
				return;//click was to the right of the columns
			}
			OnCellDoubleClick(MouseDownCol,MouseDownRow);
		}

		///<summary></summary>
		protected void OnCellClick(int col,int row,MouseButtons button) {
			ODGridClickEventArgs gArgs=new ODGridClickEventArgs(col,row,button);
			if(CellClick!=null) {
				CellClick(this,gArgs);
			}
		}

		///<summary></summary>
		protected override void OnClick(EventArgs e) {
			base.OnClick(e);
			if(MouseDownRow==-1) {
				return;//click was in the title or header section
			}
			if(MouseDownCol==-1) {
				return;//click was to the right of the columns
			}
			OnCellClick(MouseDownCol,MouseDownRow,lastButtonPressed);
		}
		#endregion Clicking

		#region BeginEndUpdate
		///<summary>Call this before adding any rows.  You would typically call Rows.Clear after this.</summary>
		public void BeginUpdate() {
			IsUpdating=true;
		}

		///<summary>Must be called after adding rows.  This computes the columns, computes the rows, lays out the scrollbars, clears SelectedIndices, and invalidates.  Does not zero out scrollVal.  Sometimes, it seems like scrollVal needs to be reset somehow because it's an inappropriate number, and when you first grab the scrollbar, it jumps.  No time to investigate.</summary>
		public void EndUpdate() {
			ComputeColumns();
			ComputeRows();
			LayoutScrollBars();
			//ScrollValue=0;
			selectedIndices=new ArrayList();
			selectedCell=new Point(-1,-1);
			if(editBox!=null) {
				editBox.Dispose();
			}
			sortedByColumnIdx=-1;
			IsUpdating=false;
			Invalidate();
		}
		#endregion BeginEndUpdate

		#region Printing
		///<summary>When using the included printing function, this tells you how many pages the printing will take.  The first page does not need to start at 0, but can start further down.</summary>
		public int GetNumberOfPages(Rectangle bounds,int marginTopFirstPage) {
			float adj=100f/96f;
			int noteMark=-1;
			int totalPages=0;
			int rowsPrinted=0;
			float yPos=marginTopFirstPage+headerHeight;//set for first page
			ComputeRows();//To refresh multiPageNotes.
			while(rowsPrinted<rows.Count) {
				//Row
				float rowHeight;
				if(noteMark==-1) {
					rowHeight=RowHeights[rowsPrinted];
					if(yPos+adj*rowHeight>bounds.Bottom) {//The row is too tall to fit on the current page.
						totalPages++;
						yPos=bounds.Top;//Reset y for next page. We only print header for first page.
					}
					yPos+=rowHeight;//Must always add rowHeight to yPos, because if a row spills onto the next page, it immediately gets printed at the top of the page (bounds.Top+rowHeight).
				}
				//Note
				if(noteMark==-1) {
					rowHeight=adj*NoteHeights[rowsPrinted];
				}
				else {
					rowHeight=multiPageNoteHeights[rowsPrinted][noteMark];
				}
				if(yPos+adj*rowHeight>bounds.Bottom) {//The row is too tall to fit on the current page. Break it into a mutli-page note.
					noteMark=0;//noteMark>-1 means we are printing a multi-page note.
					Graphics g=this.CreateGraphics();//Used for MeasureString.
					int noteW=0;
					if(NoteSpanStop>0 && NoteSpanStart<columns.Count) {
						for(int i=NoteSpanStart;i<=NoteSpanStop;i++) {
							noteW+=(int)((float)columns[i].ColWidth)-1;//Subtract 1 to attempt to fix a MeasureString problem.
						}
					}
					noteW+=2;//Tweaking for MeasureString issues. Gives us a more accurate measurement.
					StringFormat format=new StringFormat();
					Font cellFont=new Font(FontFamily.GenericSansSerif,cellFontSize);
					int totalCharactersFitted=0;
					int charactersFitted;
					int linesFilled;
					rowHeight=(bounds.Bottom-yPos)/adj-1;//Fix a row height for first row. Ensures failure of the (yPos+adj*rowHeight>bounds.Bottom) test in the future.
					multiPageNoteHeights[rowsPrinted].Add(Convert.ToInt32(rowHeight));//First part of this note, index 0.
					SizeF sizeF;
					if(rowHeight<15) {
						charactersFitted=0;
					}
					else {
						sizeF=g.MeasureString(rows[rowsPrinted].Note,cellFont,new SizeF(noteW,multiPageNoteHeights[rowsPrinted][noteMark]-10),format,out charactersFitted,out linesFilled);//Figure out how much text fits.
					}
					multiPageNoteSection[rowsPrinted].Add(rows[rowsPrinted].Note.Substring(totalCharactersFitted,charactersFitted));//Put that text into the list.
					totalCharactersFitted+=charactersFitted;
					while(totalCharactersFitted<rows[rowsPrinted].Note.Length) {//Chop up the note into pieces.
						noteMark++;
						multiPageNoteHeights[rowsPrinted].Add(Convert.ToInt32((bounds.Bottom - bounds.Top - headerHeight)/adj-1));//This is a full page length. Is shortened if we reach the end of the note. Index should be noteMark.
						sizeF=g.MeasureString(rows[rowsPrinted].Note.Substring(totalCharactersFitted),cellFont,new SizeF(noteW,multiPageNoteHeights[rowsPrinted][noteMark]-5),
							format,out charactersFitted,out linesFilled);//Figures how much text fits in the give sizeF.
						multiPageNoteSection[rowsPrinted].Add(rows[rowsPrinted].Note.Substring(totalCharactersFitted,charactersFitted));
						totalCharactersFitted+=charactersFitted;
					}
					multiPageNoteHeights[rowsPrinted][noteMark]=(int)g.MeasureString(multiPageNoteSection[rowsPrinted][noteMark],cellFont,noteW
						,format).Height-12;//Last note height is not a full page.
					rowHeight=multiPageNoteHeights[rowsPrinted][0];//Move current selection back to 0 index.
					noteMark=0;
				}
				yPos+=rowHeight;//Must always add rowHeight to yPos, because if a row spills onto the next page, it immediately gets printed at the top of the page (bounds.Top+rowHeight).
				if(noteMark==-1 || noteMark==multiPageNoteHeights[rowsPrinted].Count-1) {
					rowsPrinted++;
					noteMark=-1;
				}
				else {
					noteMark++;//Simulates printing a note section (not the last) for counting pages.
					totalPages++;//If we're still printing a note, then it has run off the end of the page.
					yPos=bounds.Top+headerHeight;//Reset to top of page.
				}
			}
			return totalPages+1;
		}

		///<summary>Use in conjunction with GetNumberOfPages.  Prints the requested pageNumber based on the supplied printing bounds and start of the first page.  Returns the yPos of where the printing stopped so that the calling function can print below it if needed.</summary>
		public int PrintPage(Graphics g,int pageNumber,Rectangle bounds,int marginTopFirstPage) {
			float adj=100f/96f;//This is a hack for an MS problem.  100 is printer dpi, and 96 is screen dpi.
			int noteMark=-1;
			int currentPage=0;//this is for looping.  We need to loop through all pages each time
			int rowsPrinted=0;
			int yPos=bounds.Top;
			if(pageNumber==0) {
				yPos=marginTopFirstPage;
			}
			int xPos=bounds.Left;
			//now, try to center in bounds
			if(adj*(float)GridW<bounds.Width) {
				xPos=(int)(bounds.Left+bounds.Width/2-adj*(float)GridW/2);
			}
			StringFormat format=new StringFormat();
			Pen gridPen;
			Pen lowerPen;
			SolidBrush textBrush;
			RectangleF textRect;
			Font cellFont=new Font(FontFamily.GenericSansSerif,cellFontSize);
			//Initialize our pens for drawing.
			gridPen=new Pen(this.cGridLine);
			lowerPen=new Pen(this.cGridLine);
			if(rows[rowsPrinted].ColorLborder!=Color.Empty) {
				lowerPen=new Pen(rows[rowsPrinted].ColorLborder);
			}
			try {
				#region Headers
				g.FillRectangle(Brushes.LightGray,xPos+ColPos[0],yPos,adj*(float)GridW,headerHeight);
				g.DrawRectangle(Pens.Black,xPos+ColPos[0],yPos,adj*(float)GridW,headerHeight);
				for(int i=1;i<ColPos.Length;i++) {
					g.DrawLine(Pens.Black,xPos+adj*(float)ColPos[i],yPos,xPos+adj*(float)ColPos[i],yPos+headerHeight);
				}
				using(Font headerFont=new Font(FontFamily.GenericSansSerif,8.5f,FontStyle.Bold)) {
					for(int i=0;i<columns.Count;i++) {
						g.DrawString(columns[i].Heading,headerFont,Brushes.Black,xPos+adj*(float)ColPos[i],yPos);
					}
				}
				#endregion Headers
				#region Rows
				yPos=marginTopFirstPage+headerHeight;//set for first page
				while(rowsPrinted<rows.Count) {
					//Print Row
					if(noteMark==-1) {//Don't print the row if in the middle of printing the note.
						if(yPos+adj*(float)RowHeights[rowsPrinted] > bounds.Bottom) {//The row is too tall to fit
							currentPage++;//There is not enough room for even this. 
							yPos=bounds.Top+headerHeight;//reset y for next page. Header was already printed.
						}
						if(currentPage>pageNumber) {
							break;
						}
						if(currentPage==pageNumber) {
							//left vertical gridline
							g.DrawLine(gridPen,
								xPos+ColPos[0],
								yPos,
								xPos+ColPos[0],
								yPos+adj*(float)RowHeights[rowsPrinted]);
							for(int i=0;i<columns.Count;i++) {
								//right vertical gridline
								g.DrawLine(gridPen,
									xPos+adj*(float)ColPos[i]+adj*(float)columns[i].ColWidth,
									yPos,
									xPos+adj*(float)ColPos[i]+adj*(float)columns[i].ColWidth,
									yPos+adj*(float)RowHeights[rowsPrinted]);
								if(NoteHeights[rowsPrinted]>0) {
									//Horizontal line which divides the main part of the row from the notes section of the row
									g.DrawLine(gridPen,
										xPos+ColPos[0],
										yPos+adj*(float)RowHeights[rowsPrinted],
										xPos+adj*(float)ColPos[columns.Count-1]+adj*(float)columns[columns.Count-1].ColWidth,
										yPos+adj*(float)RowHeights[rowsPrinted]);
								}
								//text
								if(rows[rowsPrinted].Cells.Count-1<i) {
									continue;
								}
								switch(columns[i].TextAlign) {
									case HorizontalAlignment.Left:
										format.Alignment=StringAlignment.Near;
										break;
									case HorizontalAlignment.Center:
										format.Alignment=StringAlignment.Center;
										break;
									case HorizontalAlignment.Right:
										format.Alignment=StringAlignment.Far;
										break;
								}
								if(rows[rowsPrinted].Cells[i].ColorText==Color.Empty) {
									textBrush=new SolidBrush(rows[rowsPrinted].ColorText);
								}
								else {
									textBrush=new SolidBrush(rows[rowsPrinted].Cells[i].ColorText);
								}
								if(rows[rowsPrinted].Cells[i].Bold==YN.Yes) {
									cellFont=new Font(cellFont,FontStyle.Bold);
								}
								else if(rows[rowsPrinted].Cells[i].Bold==YN.No) {
									cellFont=new Font(cellFont,FontStyle.Regular);
								}
								else {//unknown.  Use row bold
									if(rows[rowsPrinted].Bold) {
										cellFont=new Font(cellFont,FontStyle.Bold);
									}
									else {
										cellFont=new Font(cellFont,FontStyle.Regular);
									}
								}
								//Some printers will malfunction (BSOD) if print bold colored fonts.  This prevents the error.
								if(textBrush.Color!=Color.Black && cellFont.Bold) {
									cellFont=new Font(cellFont,FontStyle.Regular);
								}
								if(columns[i].TextAlign==HorizontalAlignment.Right) {
									textRect=new RectangleF(
										xPos+adj*(float)ColPos[i]-2,
										yPos,
										adj*(float)columns[i].ColWidth+2,
										adj*(float)RowHeights[rowsPrinted]);
									//shift the rect to account for MS issue with strings of different lengths
									textRect.Location=new PointF
										(textRect.X+adj*g.MeasureString(rows[rowsPrinted].Cells[i].Text,cellFont).Width/textRect.Width,
										textRect.Y);
									g.DrawString(rows[rowsPrinted].Cells[i].Text,cellFont,textBrush,textRect,format);

								}
								else {
									textRect=new RectangleF(
										xPos+adj*(float)ColPos[i],
										yPos,
										adj*(float)columns[i].ColWidth,
										adj*(float)RowHeights[rowsPrinted]);
									g.DrawString(rows[rowsPrinted].Cells[i].Text,cellFont,textBrush,textRect,format);
								}
								g.DrawString(rows[rowsPrinted].Cells[i].Text,cellFont,textBrush,textRect,format);
							}
						}
						yPos+=(int)(adj*(float)RowHeights[rowsPrinted]);//Move yPos down the length of the row (not the note).
					}
					//End Print Row
					//Print Note
					float noteHeight;
					if(noteMark==-1) {
						noteHeight=(float)NoteHeights[rowsPrinted];
						if(yPos+adj*noteHeight > bounds.Bottom) {//The note is too tall to fit and we must use the multiPageNoteSection peices.
							noteHeight=multiPageNoteHeights[rowsPrinted][0];//Move noteHeight to 0 index.
							noteMark=0;//Indicates we are now printing mutli-page note sections.
						}
					}
					else {
						noteHeight=multiPageNoteHeights[rowsPrinted][noteMark];
					}
					if(currentPage>pageNumber) {
						break;
					}
					if(currentPage==pageNumber) {
						//lines for note section
						if(noteHeight>0) {
							//left vertical gridline
							if(NoteSpanStart!=0) {
								g.DrawLine(gridPen,
									xPos+adj*(float)ColPos[NoteSpanStart],
									yPos,
									xPos+adj*(float)ColPos[NoteSpanStart],
									yPos+adj*noteHeight);
							}
							//right vertical gridline
							g.DrawLine(gridPen,
								xPos+adj*(float)ColPos[columns.Count-1]+adj*(float)columns[columns.Count-1].ColWidth,
								yPos,
								xPos+adj*(float)ColPos[columns.Count-1]+adj*(float)columns[columns.Count-1].ColWidth,
								yPos+adj*noteHeight);
							//left vertical gridline
							g.DrawLine(gridPen,
								xPos+ColPos[0],
								yPos,
								xPos+ColPos[0],
								yPos+adj*noteHeight);
						}
						//lower horizontal gridline
						g.DrawLine(lowerPen,
							xPos+ColPos[0],
							yPos+adj*noteHeight,
							xPos+adj*(float)ColPos[columns.Count-1]+adj*(float)columns[columns.Count-1].ColWidth,
							yPos+adj*noteHeight);
						//note text
						if(noteHeight>0 && NoteSpanStop>0 && NoteSpanStart<columns.Count) {
							int noteW=0;
							for(int i=NoteSpanStart;i<=NoteSpanStop;i++) {
								noteW+=(int)(adj*(float)columns[i].ColWidth);
							}
							if(rows[rowsPrinted].Bold) {
								cellFont=new Font(cellFont,FontStyle.Bold);
							}
							else {
								cellFont=new Font(cellFont,FontStyle.Regular);
							}
							textBrush=new SolidBrush(rows[rowsPrinted].ColorText);
							textRect=new RectangleF(
								xPos+adj*(float)ColPos[NoteSpanStart]+1,
								yPos,
								adj*(float)ColPos[NoteSpanStop]+adj*(float)columns[NoteSpanStop].ColWidth-adj*(float)ColPos[NoteSpanStart],
								adj*noteHeight);
							format.Alignment=StringAlignment.Near;
							if(noteMark==-1) {//Note fits on one page.
								g.DrawString(rows[rowsPrinted].Note,cellFont,textBrush,textRect,format);
							}
							else {//Print the section for this page.
								g.DrawString(multiPageNoteSection[rowsPrinted][noteMark],cellFont,textBrush,textRect,format);
							}
						}
					}
					yPos+=Convert.ToInt32(adj*noteHeight);
					if(noteMark==-1 || noteMark==multiPageNoteHeights[rowsPrinted].Count-1) {//Either the note, or the last piece of a long note.
						rowsPrinted++;
						noteMark=-1;//Usually does nothing. Necessary though.
					}
					else {
						noteMark++;//Note section printed.
						currentPage++;//End of page was reached.
						yPos=bounds.Top+headerHeight;//Reset to top of page.
					}
				}
				#endregion Rows
			}
			finally {
				if(cellFont!=null) {
					cellFont.Dispose();
				}
			}
			return yPos+4;
		}

		#endregion Printing

		#region Selections
		///<summary>Use to set a row selected or not.  Can handle values outside the acceptable range.</summary>
		public void SetSelected(int index,bool setValue) {
			if(setValue) {//select specified index
				if(selectionMode==GridSelectionMode.None) {
					throw new Exception("Selection mode is none.");
				}
				if(index<0 || index>rows.Count-1) {//check to see if index is within the valid range of values
					return;//if not, then ignore.
				}
				if(selectionMode==GridSelectionMode.One) {
					selectedIndices.Clear();//clear existing selection before assigning the new one.
				}
				if(!selectedIndices.Contains(index)) {
					selectedIndices.Add(index);
				}
			}
			else {//unselect specified index
				if(selectedIndices.Contains(index)) {
					selectedIndices.Remove(index);
				}
			}
			Invalidate();
		}

		///<summary>Allows setting multiple values all at once</summary>
		public void SetSelected(int[] iArray,bool setValue) {
			if(selectionMode==GridSelectionMode.None) {
				throw new Exception("Selection mode is none.");
			}
			if(selectionMode==GridSelectionMode.One) {
				throw new Exception("Selection mode is one.");
			}
			for(int i=0;i<iArray.Length;i++) {
				if(setValue) {//select specified index
					if(iArray[i]<0 || iArray[i]>rows.Count-1) {//check to see if index is within the valid range of values
						return;//if not, then ignore.
					}
					if(!selectedIndices.Contains(iArray[i])) {
						selectedIndices.Add(iArray[i]);
					}
				}
				else {//unselect specified index
					if(selectedIndices.Contains(iArray[i])) {
						selectedIndices.Remove(iArray[i]);
					}
				}
			}
			Invalidate();
		}

		///<summary>Sets all rows to specified value.</summary>
		public void SetSelected(bool setValue) {
			if(selectionMode==GridSelectionMode.None) {
				throw new Exception("Selection mode is none.");
			}
			if(selectionMode==GridSelectionMode.One && setValue==true) {
				throw new Exception("Selection mode is one.");
			}
			if(selectionMode==GridSelectionMode.OneCell) {
				throw new Exception("Selection mode is OneCell.");
			}
			selectedIndices.Clear();
			if(setValue) {//select all
				for(int i=0;i<rows.Count;i++) {
					selectedIndices.Add(i);
				}
			}
			Invalidate();
		}

		///<summary></summary>
		public void SetSelected(Point setCell) {
			if(selectionMode!=GridSelectionMode.OneCell) {
				throw new Exception("Selection mode must be OneCell.");
			}
			selectedCell=setCell;
			if(editBox!=null) {
				editBox.Dispose();
			}
			if(Columns[selectedCell.X].IsEditable) {
				CreateEditBox();
			}
			Invalidate();
		}

		///<summary>If one row is selected, it returns the index to that row.  Really only useful for SelectionMode.One.  If no rows selected, returns -1.</summary>
		public int GetSelectedIndex() {
			if(selectedIndices.Count>0) {
				return (int)selectedIndices[0];
			}
			return -1;
		}
		#endregion Selections

		#region Scrolling
		private void LayoutScrollBars() {
			vScroll.Location=new Point(this.Width-vScroll.Width-1,titleHeight+headerHeight+1);
			if(this.hScrollVisible) {
				hScroll.Visible=true;
				vScroll.Height=this.Height-titleHeight-headerHeight-hScroll.Height-2;
				hScroll.Location=new Point(1,this.Height-hScroll.Height-1);
				hScroll.Width=this.Width-vScroll.Width-2;
				if(GridW<hScroll.Width) {
					hScroll.Value=0;
					hScroll.Enabled=false;
				}
				else {
					hScroll.Enabled=true;
					hScroll.Minimum = 0;
					hScroll.Maximum=GridW;
					hScroll.LargeChange=(hScroll.Width<0?0:hScroll.Width);//Don't allow negative width (will throw exception).
					hScroll.SmallChange=(int)(50);
				}

			}
			else {
				hScroll.Visible=false;
				vScroll.Height=this.Height-titleHeight-headerHeight-2;
			}
			if(vScroll.Height<=0) {
				return;
			}
			//hScroll support incomplete
			if(GridH<vScroll.Height) {
				vScroll.Value=0;
				vScroll.Enabled=false;
			}
			else {
				vScroll.Enabled=true;
				vScroll.Minimum = 0;
				vScroll.Maximum=GridH;
				vScroll.LargeChange=vScroll.Height;//it used to crash right here as it tried to assign a negative number.
				vScroll.SmallChange=(int)(14*3.4);//it's not an even number so that it is obvious to user that rows moved
			}
			//vScroll.Value=0;
		}

		private void vScroll_Scroll(object sender,System.Windows.Forms.ScrollEventArgs e) {
			if(editBox!=null) {
				editBox.Dispose();
			}
			Invalidate();
			this.Focus();
		}

		private void hScroll_Scroll(object sender,System.Windows.Forms.ScrollEventArgs e) {
			//if(UpDownKey) return;
			Invalidate();
			this.Focus();
		}

		///<summary>Usually called after entering a new list to automatically scroll to the end.</summary>
		public void ScrollToEnd() {
			ScrollValue=vScroll.Maximum;//this does all error checking and invalidates
		}
		#endregion Scrolling

		#region Sorting
		///<summary>Gets run on mouse down on a column header.</summary>
		private void SortByColumn(int mouseDownCol) {
			if(mouseDownCol==-1) {
				return;
			}
			if(sortedByColumnIdx==mouseDownCol) {//already sorting by this column
				sortedIsAscending=!sortedIsAscending;//switch ascending/descending.
			}
			else {
				sortedIsAscending=true;//start out ascending
				sortedByColumnIdx=mouseDownCol;
			}
			List<ODGridRow> rowsSorted=new List<ODGridRow>();
			for(int i=0;i<rows.Count;i++) {
				rowsSorted.Add(rows[i]);
			}
			if(columns[sortedByColumnIdx].SortingStrategy==GridSortingStrategy.StringCompare) {
				rowsSorted.Sort(SortStringCompare);
			}
			else if(columns[sortedByColumnIdx].SortingStrategy==GridSortingStrategy.DateParse) {
				rowsSorted.Sort(SortDateParse);
			}
			else if(columns[sortedByColumnIdx].SortingStrategy==GridSortingStrategy.ToothNumberParse) {
				rowsSorted.Sort(SortToothNumberParse);
			}
			else if(columns[sortedByColumnIdx].SortingStrategy==GridSortingStrategy.AmountParse) {
				rowsSorted.Sort(SortAmountParse);
			}
			BeginUpdate();
			rows.Clear();
			for(int i=0;i<rowsSorted.Count;i++) {
				rows.Add(rowsSorted[i]);
			}
			EndUpdate();
			sortedByColumnIdx=mouseDownCol;//Must be set again since set to -1 in EndUpdate();
		}

		private int SortStringCompare(ODGridRow row1,ODGridRow row2) {
			return (sortedIsAscending?1:-1)*row1.Cells[sortedByColumnIdx].Text.CompareTo(row2.Cells[sortedByColumnIdx].Text);
		}

		private int SortDateParse(ODGridRow row1,ODGridRow row2) {
			string raw1=row1.Cells[sortedByColumnIdx].Text;
			string raw2=row2.Cells[sortedByColumnIdx].Text;
			DateTime date1=DateTime.MinValue;
			DateTime date2=DateTime.MinValue;
			if(raw1!="") {
				try {
					date1=DateTime.Parse(raw1);
				}
				catch {
					return 0;//shouldn't happen
				}
			}
			if(raw2!="") {
				try {
					date2=DateTime.Parse(raw2);
				}
				catch {
					return 0;//shouldn't happen
				}
			}
			return (sortedIsAscending?1:-1)*date1.CompareTo(date2);
		}

		private int SortToothNumberParse(ODGridRow row1,ODGridRow row2) {
			//remember that teeth could be in international format.
			//fail gracefully
			string raw1=row1.Cells[sortedByColumnIdx].Text;
			string raw2=row2.Cells[sortedByColumnIdx].Text;
			if(!Tooth.IsValidEntry(raw1) && !Tooth.IsValidEntry(raw2)) {//both invalid
				return 0;
			}
			int retVal=0;
			if(!Tooth.IsValidEntry(raw1)) {//only first invalid
				retVal=-1; ;
			}
			else if(!Tooth.IsValidEntry(raw2)) {//only second invalid
				retVal=1; ;
			}
			else {//both valid
				string tooth1=Tooth.FromInternat(raw1);
				string tooth2=Tooth.FromInternat(raw2);
				int toothInt1=Tooth.ToInt(tooth1);
				int toothInt2=Tooth.ToInt(tooth2);
				retVal=toothInt1.CompareTo(toothInt2);
			}
			return (sortedIsAscending?1:-1)*retVal;
		}

		private int SortAmountParse(ODGridRow row1,ODGridRow row2) {
			string raw1=row1.Cells[sortedByColumnIdx].Text;
			string raw2=row2.Cells[sortedByColumnIdx].Text;
			Decimal amt1=0;
			Decimal amt2=0;
			if(raw1!="") {
				try {
					amt1=Decimal.Parse(raw1);
				}
				catch {
					return 0;//shouldn't happen
				}
			}
			if(raw2!="") {
				try {
					amt2=Decimal.Parse(raw2);
				}
				catch {
					return 0;//shouldn't happen
				}
			}
			return (sortedIsAscending?1:-1)*amt1.CompareTo(amt2);
		}

		#endregion Sorting

		#region MouseEvents

		///<summary></summary>
		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
			lastButtonPressed=e.Button;//used in the click event.
			if(e.Button==MouseButtons.Right) {
				if(selectedIndices.Count>0) {//if there are already rows selected, then ignore right click
					return;
				}
				//otherwise, row will be selected. Useful when using context menu.
			}
			MouseIsDown=true;
			MouseDownRow=PointToRow(e.Y);
			MouseDownCol=PointToCol(e.X);
			if(e.Y < 1+titleHeight) {//mouse down was in the title section
				return;
			}
			if(e.Y < 1+titleHeight+headerHeight) {//mouse down was on a column header
				mouseIsDownInHeader=true;
				if(allowSortingByColumn) {
					if(MouseDownCol==-1) {
						return;
					}
					SortByColumn(MouseDownCol);
					Invalidate();
					return;
				}
				else {
					return;
				}
			}
			if(MouseDownRow==-1) {//mouse down was below the grid rows
				return;
			}
			if(MouseDownCol==-1) {//mouse down was to the right of columns
				return;
			}
			if(!allowSelection) {
				return;//clicks do not trigger selection of rows, but cell click event still gets fired
			}
			switch(selectionMode) {
				case GridSelectionMode.None:
					return;
				case GridSelectionMode.One:
					selectedIndices.Clear();
					selectedIndices.Add(MouseDownRow);
					break;
				case GridSelectionMode.OneCell:
					selectedIndices.Clear();
					//Point oldSelectedCell=selectedCell;
					selectedCell=new Point(MouseDownCol,MouseDownRow);
					//if(oldSelectedCell.X!=selectedCell.X || oldSelectedCell.Y!=selectedCell.Y){
					if(editBox!=null) {
						editBox.Dispose();
					}
					if(Columns[selectedCell.X].IsEditable) {
						CreateEditBox();
					}
					//}
					break;
				case GridSelectionMode.MultiExtended:
					if(ControlIsDown) {
						//we need to remember exactly which rows were selected the moment the mouse down started.
						//Then, if the mouse gets dragged up or down, the rows between mouse start and mouse end
						//will be set to the opposite of these remembered values.
						selectedIndicesWhenMouseDown=new ArrayList(selectedIndices);
						if(selectedIndices.Contains(MouseDownRow)) {
							selectedIndices.Remove(MouseDownRow);
						}
						else {
							selectedIndices.Add(MouseDownRow);
						}
					}
					else if(ShiftIsDown) {
						if(selectedIndices.Count==0) {
							selectedIndices.Add(MouseDownRow);
						}
						else {
							int fromRow=(int)selectedIndices[0];
							selectedIndices.Clear();
							if(MouseDownRow<fromRow) {//dragging down
								for(int i=MouseDownRow;i<=fromRow;i++) {
									selectedIndices.Add(i);
								}
							}
							else {
								for(int i=fromRow;i<=MouseDownRow;i++) {
									selectedIndices.Add(i);
								}
							}
						}
					}
					else {//ctrl or shift not down
						selectedIndices.Clear();
						selectedIndices.Add(MouseDownRow);
					}
					break;
			}
			Invalidate();
		}

		///<summary></summary>
		protected override void OnMouseUp(MouseEventArgs e) {
			base.OnMouseUp(e);
			//if(e.Button==MouseButtons.Right){
			//	return;
			//}
			MouseIsDown=false;
			mouseIsDownInHeader=false;
		}

		///<summary>When selection mode is OneCell, and user clicks in a column that isEditable, then this edit box will appear.  Pass in the location from the click event so that we can determine where to place the text cursor in the box.</summary>
		private void CreateEditBox() {
			editBox=new TextBox();
			//The problem is that it ignores the height.
			editBox.Size=new Size(Columns[selectedCell.X].ColWidth+1,RowHeights[selectedCell.Y]+1);//+NoteHeights[rowI]-1);
			//editBox.Multiline=true;
			editBox.Location=new Point(-hScroll.Value+1+ColPos[selectedCell.X],
				-vScroll.Value+1+titleHeight+headerHeight+RowLocs[selectedCell.Y]);
			editBox.Text=Rows[selectedCell.Y].Cells[selectedCell.X].Text;
			editBox.TextChanged+=new EventHandler(editBox_TextChanged);
			editBox.KeyDown+=new KeyEventHandler(editBox_KeyDown);
			if(Columns[selectedCell.X].TextAlign==HorizontalAlignment.Right) {
				editBox.TextAlign=HorizontalAlignment.Right;
			}
			this.Controls.Add(editBox);
			editBox.SelectAll();
			editBox.Focus();
		}

		void editBox_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Enter) {
				editBox.Dispose();
				//find the next editable cell to the right.
				int nextCellToRight=-1;
				for(int i=selectedCell.X+1;i<columns.Count;i++) {
					if(columns[i].IsEditable) {
						nextCellToRight=i;
						break;
					}
				}
				if(nextCellToRight!=-1) {
					selectedCell=new Point(nextCellToRight,selectedCell.Y);
					CreateEditBox();
					return;
				}
				//can't move to the right, so attempt to move down.
				if(selectedCell.Y==rows.Count-1) {
					return;//can't move down
				}
				nextCellToRight=-1;
				for(int i=0;i<columns.Count;i++) {
					if(columns[i].IsEditable) {
						nextCellToRight=i;
						break;
					}
				}
				//guaranteed to have a value
				selectedCell=new Point(nextCellToRight,selectedCell.Y+1);
				CreateEditBox();
			}
			if(e.KeyCode==Keys.Down) {
				if(selectedCell.Y<rows.Count-1) {
					editBox.Dispose();
					selectedCell=new Point(selectedCell.X,selectedCell.Y+1);
					CreateEditBox();
				}
			}
			if(e.KeyCode==Keys.Up) {
				if(selectedCell.Y>0) {
					editBox.Dispose();
					selectedCell=new Point(selectedCell.X,selectedCell.Y-1);
					CreateEditBox();
				}
			}
		}

		void editBox_TextChanged(object sender,EventArgs e) {
			Rows[selectedCell.Y].Cells[selectedCell.X].Text=editBox.Text;
			OnCellTextChanged();
		}

		///<summary>The purpose of this is to allow dragging to select multiple rows.  Only makes sense if selectionMode==MultiExtended.  Doesn't matter whether ctrl is down, because that only affects the mouse down event.</summary>
		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			MouseIsOver=true;
			if(!MouseIsDown) {
				return;
			}
			if(selectionMode!=GridSelectionMode.MultiExtended) {
				return;
			}
			if(!allowSelection) {
				return;//dragging does not change selection of rows
			}
			if(mouseIsDownInHeader) {
				return;//started drag in header, so not allowed to select anything.
			}
			int curRow=PointToRow(e.Y);
			if(curRow==-1 || curRow==MouseDownRow) {
				return;
			}
			//because mouse might have moved faster than computer could keep up, we have to loop through all rows between
			if(ControlIsDown) {
				if(selectedIndicesWhenMouseDown==null) {
					selectedIndices=new ArrayList();
				}
				else {
					selectedIndices=new ArrayList(selectedIndicesWhenMouseDown);
				}
			}
			else {
				selectedIndices=new ArrayList();
			}
			if(MouseDownRow<curRow) {//dragging down
				for(int i=MouseDownRow;i<=curRow;i++) {
					if(i==-1) {
						continue;
					}
					if(selectedIndices.Contains(i)) {
						selectedIndices.Remove(i);
					}
					else {
						selectedIndices.Add(i);
					}
				}
			}
			else {//dragging up
				for(int i=curRow;i<=MouseDownRow;i++) {
					if(selectedIndices.Contains(i)) {
						selectedIndices.Remove(i);
					}
					else {
						selectedIndices.Add(i);
					}
				}
			}
			Invalidate();
		}

		///<summary></summary>
		protected override void OnMouseEnter(EventArgs e) {
			base.OnMouseEnter(e);
			MouseIsOver=true;
		}

		///<summary></summary>
		protected override void OnMouseLeave(EventArgs e) {
			base.OnMouseLeave(e);
			MouseIsOver=false;
		}

		private void vScroll_MouseEnter(Object sender,EventArgs e) {
			MouseIsOver=true;
		}

		private void vScroll_MouseLeave(Object sender,EventArgs e) {
			MouseIsOver=false;
		}

		private void vScroll_MouseMove(Object sender,MouseEventArgs e) {
			MouseIsOver=true;
		}

		private void hScroll_MouseEnter(Object sender,EventArgs e) {
			MouseIsOver=true;
		}

		private void hScroll_MouseLeave(Object sender,EventArgs e) {
			MouseIsOver=false;
		}

		private void hScroll_MouseMove(Object sender,MouseEventArgs e) {
			MouseIsOver=true;
		}

		private void Parent_MouseWheel(Object sender,MouseEventArgs e) {
			if(MouseIsOver) {
				//this.ac
				this.Select();//?
				//this.Focus();
			}
		}

		///<summary></summary>
		protected override void OnMouseWheel(MouseEventArgs e) {
			base.OnMouseWheel(e);
			ScrollValue-=e.Delta/3;
		}

		#endregion MouseEvents

		#region KeyEvents
		///<summary></summary>
		protected override void OnKeyDown(KeyEventArgs e) {
			base.OnKeyDown(e);
			if(e.KeyCode==Keys.ControlKey) {
				ControlIsDown=true;
			}
			if(e.KeyCode==Keys.ShiftKey) {
				ShiftIsDown=true;
			}
		}

		///<summary></summary>
		protected override void OnKeyUp(KeyEventArgs e) {
			base.OnKeyUp(e);
			if(e.KeyCode==Keys.ControlKey) {
				ControlIsDown=false;
			}
			if(e.KeyCode==Keys.ShiftKey) {
				ShiftIsDown=false;
			}
		}

		private void Parent_KeyDown(Object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.ControlKey) {
				ControlIsDown=true;
			}
			if(e.KeyCode==Keys.ShiftKey) {
				ShiftIsDown=true;
			}
			if(selectionMode==GridSelectionMode.One) {
				if(e.KeyCode==Keys.Down) {
					if(selectedIndices.Count>0 && (int)selectedIndices[0] < rows.Count-1) {
						int prevRow=(int)selectedIndices[0];
						selectedIndices.Clear();
						selectedIndices.Add(prevRow+1);
						hScroll.Value=hScroll.Minimum;
					}
				}
				else if(e.KeyCode==Keys.Up) {
					if(selectedIndices.Count>0 && (int)selectedIndices[0] > 0) {
						int prevRow=(int)selectedIndices[0];
						selectedIndices.Clear();
						selectedIndices.Add(prevRow-1);
					}
				}
			}
		}

		private void Parent_KeyUp(Object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.ControlKey) {
				ControlIsDown=false;
			}
			if(e.KeyCode==Keys.ShiftKey) {
				ShiftIsDown=false;
			}
			//if(e.KeyCode==Keys.Down | e.KeyCode==Keys.Up){
			//	UpDownKey=false;
			//	hScroll.Value=hScroll.Minimum;
			//}
		}

		protected void OnCellTextChanged() {
			if(CellTextChanged!=null) {
				CellTextChanged(this,new EventArgs());
			}
		}
		#endregion KeyEvents





















	}


	///<summary></summary>
	public class ODGridClickEventArgs {
		private int col;
		private int row;
		private MouseButtons button;

		///<summary></summary>
		public ODGridClickEventArgs(int col,int row,MouseButtons button) {
			this.col=col;
			this.row=row;
			this.button=button;
		}

		///<summary></summary>
		public int Row {
			get {
				return row;
			}
		}

		///<summary></summary>
		public int Col {
			get {
				return col;
			}
		}

		///<summary>Gets which mouse button was pressed.</summary>
		public MouseButtons Button {
			get {
				return button;
			}
		}

	}

	///<summary>Specifies the selection behavior of an ODGrid.</summary>   
	//[ComVisible(true)]
	public enum GridSelectionMode {
		///<summary>0-No items can be selected.</summary>  
		None=0,
		///<summary>1-Only one row can be selected.</summary>  
		One=1,
		///<summary>2-Only one cell can be selected.</summary>
		OneCell=2,
		///<summary>3-Multiple items can be selected, and the user can use the SHIFT, CTRL, and arrow keys to make selections</summary>   
		MultiExtended=3,
	}

}






/*This is a template of typical grid code which can be quickly pasted into any form.
 
		using OpenDental.UI;

		FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("Table",""),);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("Table",""),);
			gridMain.Columns.Add(col);
			 
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<List.Length;i++){
				row=new ODGridRow();
				row.Cells.Add("");
				row.Cells.Add("");
			  
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

*/