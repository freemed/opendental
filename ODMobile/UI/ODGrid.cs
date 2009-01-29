using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDentMobile.UI {
	public partial class ODGrid:Control {
		private ODGridColumnList columns;
		private ODGridRowList rows;
		private bool wrapText;
		///<summary>The total height of the grid.  Already corrected for resolution.</summary>
		private int GridH;
		///<summary>The total width of the grid.  Already corrected for resolution.</summary>
		private int GridW;
		///<summary>This array has one element for each row.  For each row, it keeps track of the vertical height of the row in pixels, not counting the note portion of the row.  Already corrected for resolution.</summary>
		private int[] RowHeights;
		///<summary>This array has one element for each row.  For each row, it keeps track of the vertical location at which to start drawing this row in pixels.  This makes it much easier to paint rows.  Already corrected for resolution.</summary>
		private int[] RowLocs;
		///<summary>Set at the very beginning of OnPaint.  Uses the ColWidth of each column to set up this array with one element for each column.  Contains the columns Pos for that column.  Already corrected for resolution.</summary>
		private List<int> ColPos;
		///<summary>Not corrected for resolution.  That needs to be done wherever this variable is used.</summary>
		private int headerHeight=15;
		///<summary>If resolution is 96, this will be 1.  If resolution is 192, this will be 2.</summary>
		private int pixelCorrection;
		///<summary>If resolution is 96, this will be 1f.  If resolution is 192, this will be 2f.</summary>
		private float pixelCorrectionF;
		private Color cGridLine=Color.FromArgb(180,180,180);
		private Color cTitleBackG=Color.FromArgb(210,210,210);
		private Color cBlueOutline=Color.FromArgb(119,119,146);
		///<summary>Seems to already be corrected for resolution.</summary>
		private Font headerFont=new Font(FontFamily.GenericSansSerif,8.5f,FontStyle.Bold);
		///<summary>Seems to already be corrected for resolution.</summary>
		private Font cellFont=new Font(FontFamily.GenericSansSerif,8.5f,FontStyle.Regular);
		private bool IsUpdating;
		private int MouseDownRow;
		private int MouseDownCol;
		private List<int> selectedIndices;
		private GridSelectionMode selectionMode;
		private bool MouseIsDown;
		private Color selectedRowColor;
		public string DebugString;
		///<summary></summary>
		public event ODGridClickEventHandler CellClick=null;

		public ODGrid() {
			InitializeComponent();
			columns=new ODGridColumnList();
			rows=new ODGridRowList();
			wrapText=true;
			selectedIndices=new List<int>();
			selectionMode=GridSelectionMode.One;
			selectedRowColor=Color.Silver;
		}

		#region Properties
		///<summary>Gets the collection of ODGridColumns assigned to the ODGrid control.</summary>
		public ODGridColumnList Columns{
			get{
				return columns;
			}
		}

		///<summary>Gets the collection of ODGridRows assigned to the ODGrid control.</summary>
		public ODGridRowList Rows{
			get{
				return rows;
			}
		}

		///<summary>Holds the int values of the indices of the selected rows.  To set selected indices, use SetSelected().</summary>
		public int[] SelectedIndices{
			get{
				int[] retVal=new int[selectedIndices.Count];
				selectedIndices.CopyTo(retVal);
				Array.Sort(retVal);//they must be in numerical order
				return retVal; 
			}
		}

		///<summary></summary>
		public GridSelectionMode SelectionMode{
			get{ 
				return selectionMode; 
			}
			set{
				//if((GridSelectionMode)value==SelectionMode.MultiSimple){
				//	MessageBox.Show("MultiSimple not supported.");
				//	return;
				//}
				selectionMode=value;
			}
		}

		///<summary>Text within each cell will wrap, making some rows taller.</summary>
		[DefaultValue(true)]
		public bool WrapText{
			get {
				return wrapText;
			}
			set {
				wrapText=value;
			}
		}
		#endregion Properties

		#region Computations
		///<summary>Computes the position of each column and the overall width.  Called from endUpdate and also from OnPaint.</summary>
		private void ComputeColumns(){
			Graphics g=this.CreateGraphics();
			//dpi could be 96 or 192.  If 192, we have to double our pixel calculations
			if(g.DpiX==192f) {
				pixelCorrection=2;
				pixelCorrectionF=2f;
			}
			else {
				pixelCorrection=1;
				pixelCorrectionF=1f;
			}
			ColPos=new List<int>();
			for(int i=0;i<columns.Count;i++){
				if(i==0){
					ColPos.Add(0);
				}
				else{
					ColPos.Add(ColPos[i-1]+columns[i-1].ColWidth*pixelCorrection);
				}
			}
			if(columns.Count>0){
				GridW=ColPos[ColPos.Count-1]+columns[columns.Count-1].ColWidth*pixelCorrection;
			}
		}

		///<summary>After adding rows to the grid, this calculates the height of each row because some rows may have text wrap and will take up more than one row.  Also, rows with notes, must be made much larger, because notes start on the second line.  If column images are used, rows will be enlarged to make space for the images.</summary>
		private void ComputeRows(){
			Graphics g=this.CreateGraphics();
			RowHeights=new int[rows.Count];
			RowLocs=new int[rows.Count];
			GridH=0;
			int cellH;
			float textWidth;//we can't use MeasureString
			float textHeight=g.MeasureString("Any",this.cellFont).Height;
			int rowCount;
			for(int i=0;i<rows.Count;i++){
				RowHeights[i]=0;
				if(wrapText){
					//find the tallest col
					for(int j=0;j<rows[i].Cells.Count;j++) {
						textWidth=g.MeasureString(rows[i].Cells[j].Text,this.cellFont).Width+5f*pixelCorrectionF;//the 5 is a buffer.
						rowCount=(int)Math.Ceiling((double)(textWidth/((float)columns[j].ColWidth*pixelCorrectionF)));//approximation.
						cellH=(int)(rowCount*textHeight)+1;
						if(cellH>RowHeights[i]) {
							RowHeights[i]=cellH;
						}
						if(i==0 && j==0){
							this.DebugString="text='"+rows[i].Cells[j].Text+"'"
								+",textWidth="+textWidth.ToString()
								+",colWidth="+(columns[j].ColWidth*pixelCorrectionF).ToString()
								+",rowCount="+rowCount.ToString()
								+",cellH="+cellH.ToString();
						}
					}
				}
				else{
					RowHeights[i]=(int)g.MeasureString("Any",this.cellFont).Height+1*pixelCorrection;
				}
				if(i==0){
					RowLocs[i]=0;
				}
				else{
					RowLocs[i]=RowLocs[i-1]+RowHeights[i-1];
				}
				GridH+=RowHeights[i];
			}
			g.Dispose();
		}

		///<summary>Returns row. -1 if no valid row.  Supply the y position in pixels.</summary>
		public int PointToRow(int y){
			if(y<(1+headerHeight)*pixelCorrection) {
				return-1;
			}
			for(int i=0;i<rows.Count;i++){
				//if(y>-vScroll.Value+1+titleHeight+headerHeight+RowLocs[i]+RowHeights[i]+NoteHeights[i]){
				if(y>(1+headerHeight)*pixelCorrection+RowLocs[i]+RowHeights[i]) {
					continue;//clicked below this row.
				}
				return i;
			}
			return -1;
		}

		///<summary>Returns col.  Supply the x position in pixels. -1 if no valid column.</summary>
		public int PointToCol(int x){
			int colRight;//the right edge of each column
			for(int i=0;i<columns.Count;i++){
				colRight=0;
				for(int c=0;c<i+1;c++){
					colRight+=columns[c].ColWidth*pixelCorrection;
				}
				if(x>colRight){
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
		protected override void OnPaint(PaintEventArgs e){
			if(IsUpdating){
				return;
			}
			if(Width<1 || Height<1) {
				return;
			}
			ComputeColumns();//it's only here because I can't figure out how to do it when columns are added. It will be removed.
			//Bitmap doubleBuffer=new Bitmap(Width,Height);//,e.Graphics);//e.Graphics would have specified the resolution of the bitmap.
			Graphics g=e.Graphics;
				//Graphics.FromImage(doubleBuffer);
			DrawBackG(g);
			DrawRows(g);
			DrawTitleAndHeaders(g);//this will draw on top of any grid stuff
			DrawOutline(g);
			//e.Graphics.DrawImage(doubleBuffer,0,0);
			g.Dispose();
			base.OnPaint(e);
		}

		///<summary>Draws a solid gray background.</summary>
		private void DrawBackG(Graphics g){
			g.FillRectangle(new SolidBrush(Color.FromArgb(224,223,227)),
				0,(headerHeight+1)*pixelCorrection,
				Width,this.Height-(headerHeight-1)*pixelCorrection);
		}

		///<summary>Draws the background, lines, and text for all rows that are visible.</summary>
		private void DrawRows(Graphics g){
			for(int i=0;i<rows.Count;i++){
				if(RowLocs[i]+RowHeights[i]<0){
					continue;//lower edge of row above top of grid area
				}
				if(1+headerHeight+RowLocs[i]>Height){
					return;//row below lower edge of control
				}
				DrawRow(i,g);
			}
		}

		///<summary>Draws background, lines, image, and text for a single row.</summary>
		private void DrawRow(int rowI,Graphics g){
			RectangleF textRect;
			StringFormat format=new StringFormat();
			Pen gridPen=new Pen(this.cGridLine);
			Pen lowerPen=new Pen(this.cGridLine);
			if(rowI==rows.Count-1){//last row
				lowerPen=new Pen(Color.FromArgb(120,120,120));
			}
			SolidBrush textBrush=new SolidBrush(Color.Black);
			//selected row color
			if(selectedIndices.Contains(rowI)){
				g.FillRectangle(new SolidBrush(selectedRowColor),
					1*pixelCorrection,
					(2+headerHeight)*pixelCorrection+RowLocs[rowI],
					GridW,
					RowHeights[rowI]-1*pixelCorrection);
			}
			//normal row color
			else{//need to draw over the gray background
				g.FillRectangle(new SolidBrush(Color.White),
					1*pixelCorrection,
					(2+headerHeight)*pixelCorrection+RowLocs[rowI],
					GridW,//this is a really simple width value that always works well
					RowHeights[rowI]-1*pixelCorrection);
			}
			for(int i=0;i<columns.Count;i++){
				//right vertical gridline
				if(rowI==0){
					g.DrawLine(gridPen,
						1*pixelCorrection+ColPos[i]+columns[i].ColWidth*pixelCorrection,
						(1+headerHeight)*pixelCorrection+RowLocs[rowI],
						1*pixelCorrection+ColPos[i]+columns[i].ColWidth*pixelCorrection,
						(1+headerHeight)*pixelCorrection+RowLocs[rowI]+RowHeights[rowI]);
				}
				else{
					g.DrawLine(gridPen,
						1*pixelCorrection+ColPos[i]+columns[i].ColWidth*pixelCorrection,
						(2+headerHeight)*pixelCorrection+RowLocs[rowI],
						1*pixelCorrection+ColPos[i]+columns[i].ColWidth*pixelCorrection,
						(1+headerHeight)*pixelCorrection+RowLocs[rowI]+RowHeights[rowI]);
				}
				//lower horizontal gridline
				if(i==0){
					g.DrawLine(lowerPen,
						1*pixelCorrection+ColPos[i],
						(1+headerHeight)*pixelCorrection+RowLocs[rowI]+RowHeights[rowI],
						1*pixelCorrection+ColPos[i]+columns[i].ColWidth*pixelCorrection,
						(1+headerHeight)*pixelCorrection+RowLocs[rowI]+RowHeights[rowI]);
				}
				else{
					g.DrawLine(lowerPen,
						2*pixelCorrection+ColPos[i],
						(1+headerHeight)*pixelCorrection+RowLocs[rowI]+RowHeights[rowI],
						1*pixelCorrection+ColPos[i]+columns[i].ColWidth*pixelCorrection,
						(1+headerHeight)*pixelCorrection+RowLocs[rowI]+RowHeights[rowI]);
				}
				//text
				if(rows[rowI].Cells.Count-1<i){
					continue;
				}
				switch(columns[i].TextAlign){
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
				int vertical=(1+headerHeight)*pixelCorrection+RowLocs[rowI]+1;
				int horizontal=3*pixelCorrection+ColPos[i];
				int cellW=(columns[i].ColWidth-2)*pixelCorrection;
				int cellH=RowHeights[rowI];
				if(columns[i].TextAlign==HorizontalAlignment.Right){
					horizontal-=2*pixelCorrection;
					cellW+=2*pixelCorrection;
				}
				textRect=new RectangleF(horizontal,vertical,cellW,cellH);
				g.DrawString(rows[rowI].Cells[i].Text,cellFont,textBrush,textRect,format);
			}
		}

		///<summary>There isn't actually a title in this compact version.</summary>
		private void DrawTitleAndHeaders(Graphics g){
			//Column Headers-----------------------------------------------------------------------------------------
			g.FillRectangle(new SolidBrush(this.cTitleBackG),0,0,Width,headerHeight*pixelCorrection);//background
			g.DrawLine(new Pen(Color.FromArgb(102,102,122)),0,0,Width,0);//line between title and headers
			for(int i=0;i<columns.Count;i++){
				if(i!=0){
					//vertical lines separating column headers
					g.DrawLine(new Pen(Color.FromArgb(120,120,120)),1*pixelCorrection+ColPos[i],3*pixelCorrection,
						1*pixelCorrection+ColPos[i],(headerHeight-2)*pixelCorrection);
					g.DrawLine(new Pen(Color.White),2*pixelCorrection+ColPos[i],3*pixelCorrection,
						2*pixelCorrection+ColPos[i],(headerHeight-2)*pixelCorrection);
				}
				g.DrawString(columns[i].Heading,headerFont,new SolidBrush(Color.Black),
					ColPos[i]+columns[i].ColWidth*pixelCorrection/2-g.MeasureString(columns[i].Heading,headerFont).Width/2,
					2*pixelCorrection);
			}
			//line below headers
			g.DrawLine(new Pen(Color.FromArgb(120,120,120)),0,headerHeight*pixelCorrection,Width,headerHeight*pixelCorrection);
		}

		///<summary>Draws outline around entire control.</summary>
		private void DrawOutline(Graphics g){
			g.DrawRectangle(new Pen(this.cBlueOutline,pixelCorrectionF),0,0,Width-1*pixelCorrection,Height-1*pixelCorrection);
		}
		#endregion Painting

		#region Clicking
		///<summary></summary>
		protected void OnCellClick(int col,int row){
			ODGridClickEventArgs gArgs=new ODGridClickEventArgs(col,row);
			if(CellClick!=null){
				CellClick(this,gArgs);
			}
		}

		///<summary></summary>
		protected override void OnClick(EventArgs e){
			base.OnClick (e);
			if(MouseDownRow==-1){
				return;//click was in the title or header section
			}
			if(MouseDownCol==-1){
				return;//click was to the right of the columns
			}
			OnCellClick(MouseDownCol,MouseDownRow);
		}
		#endregion Clicking

		#region BeginEndUpdate
		///<summary>Call this before adding any rows.  You would typically call Rows.Clear after this.</summary>
		public void BeginUpdate(){
			IsUpdating=true;
		}

		///<summary>Must be called after adding rows.  This computes the columns, computes the rows, resets the size to match the data (unique to mobile version), and invalidates.</summary>
		public void EndUpdate(){
			ComputeColumns();
			ComputeRows();
			//unique to mobile version, we always make the grid the same size as the data:
			this.Width=GridW;
			this.Height=headerHeight*pixelCorrection+GridH+1;
			IsUpdating=false;
			Invalidate();
		}
		#endregion BeginEndUpdate

		#region Selections
		///<summary>Use to set a row selected or not.  Can handle values outside the acceptable range.</summary>
		public void SetSelected(int index,bool setValue){
			if(setValue){//select specified index
				if(selectionMode==GridSelectionMode.None){
					throw new Exception("Selection mode is none.");
				}
				if(index<0 || index>rows.Count-1){//check to see if index is within the valid range of values
					return;//if not, then ignore.
				}
				//if(selectionMode==GridSelectionMode.One){
				//	selectedIndices.Clear();//clear existing selection before assigning the new one.
				//}
				if(!selectedIndices.Contains(index)){
					selectedIndices.Add(index);
				}
			}
			else{//unselect specified index
				if(selectedIndices.Contains(index)){
					selectedIndices.Remove(index);
				}
			}
      Invalidate();
		}

		///<summary>Allows setting multiple values all at once</summary>
		public void SetSelected(int[] iArray,bool setValue){
			if(selectionMode==GridSelectionMode.None){
				throw new Exception("Selection mode is none.");
			}
			if(selectionMode==GridSelectionMode.One){
				throw new Exception("Selection mode is one.");
			}
			for(int i=0;i<iArray.Length;i++){
				if(setValue){//select specified index
					if(iArray[i]<0 || iArray[i]>rows.Count-1) {//check to see if index is within the valid range of values
						return;//if not, then ignore.
					}
					if(!selectedIndices.Contains(iArray[i])){
						selectedIndices.Add(iArray[i]);
					}
				}
				else{//unselect specified index
					if(selectedIndices.Contains(iArray[i])){
						selectedIndices.Remove(iArray[i]);
					}
				}
			}
			Invalidate();
		}

		///<summary>Sets all rows to specified value.</summary>
		public void SetSelected(bool setValue){
			if(selectionMode==GridSelectionMode.None){
				throw new Exception("Selection mode is none.");
			}
			if(selectionMode==GridSelectionMode.One && setValue==true){
				throw new Exception("Selection mode is one.");
			}
			if(selectionMode==GridSelectionMode.OneCell){
				throw new Exception("Selection mode is OneCell.");
			}
			selectedIndices.Clear();
			if(setValue){//select all
				for(int i=0;i<rows.Count;i++){
					selectedIndices.Add(i);
				}
			}
			Invalidate();
		}

		///<summary>If one row is selected, it returns the index to that row.  Really only useful for SelectionMode.One.  If no rows selected, returns -1.</summary>
		public int GetSelectedIndex(){
			if(selectedIndices.Count>0){
				return (int)selectedIndices[0];
			}
			return -1;
		}
		#endregion Selections

		#region MouseEvents
		///<summary></summary>
		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
			MouseIsDown=true;
			MouseDownRow=PointToRow(e.Y);
			MouseDownCol=PointToCol(e.X);
			if(MouseDownRow==-1){//mouse down was in the title or header section
				return;
			}
			if(MouseDownCol==-1){//mouse down was to the right of columns
				return;
			}
			switch(selectionMode){
				case GridSelectionMode.None:
					return;
				case GridSelectionMode.One:
					selectedIndices.Clear();
					selectedIndices.Add(MouseDownRow);
					break;
				//GridSelectionMode.OneCell
				case GridSelectionMode.MultiExtended:
					if(selectedIndices.Contains(MouseDownRow)){
						selectedIndices.Remove(MouseDownRow);
					}
          else{
						selectedIndices.Add(MouseDownRow);
          }
					break;
			}
			Invalidate();
		}

		///<summary></summary>
		protected override void OnMouseUp(MouseEventArgs e){
			base.OnMouseUp(e);
			MouseIsDown=false;
		}

		#endregion MouseEvents

		
	}

	///<summary>Specifies the selection behavior of an ODGrid.</summary>   
	public enum GridSelectionMode {
		///<summary>0-No items can be selected.</summary>  
		None=0,
		///<summary>1-Only one row can be selected.</summary>  
		One=1,
		///<summary>2-Not implemented.</summary>
		OneCell=2,
		///<summary>3-Multiple items can be selected.</summary>   
		MultiExtended=3,
	}
}
