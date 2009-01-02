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
		///<summary>The total height of the grid.</summary>
		private int GridH;
		///<summary>The total width of the grid.</summary>
		private int GridW;
		///<summary>This array has one element for each row.  For each row, it keeps track of the vertical height of the row in pixels, not counting the note portion of the row.</summary>
		private int[] RowHeights;
		///<summary>This array has one element for each row.  For each row, it keeps track of the vertical location at which to start drawing this row in pixels.  This makes it much easier to paint rows.</summary>
		private int[] RowLocs;
		///<summary>Set at the very beginning of OnPaint.  Uses the ColWidth of each column to set up this array with one element for each column.  Contains the columns Pos for that column.</summary>
		private List<int> ColPos;
		private int headerHeight=15;
		private Color cGridLine=Color.FromArgb(180,180,180);
		private Color cTitleBackG=Color.FromArgb(210,210,210);
		private Color cBlueOutline=Color.FromArgb(119,119,146);
		private Font headerFont=new Font(FontFamily.GenericSansSerif,8.5f,FontStyle.Bold);
		private Font cellFont=new Font(FontFamily.GenericSansSerif,8.5f,FontStyle.Regular);
		private bool IsUpdating;
		///<summary></summary>
		public event ODGridClickEventHandler CellClick=null;

		public ODGrid() {
			InitializeComponent();
			columns=new ODGridColumnList();
			rows=new ODGridRowList();
			wrapText=true;
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
			ColPos=new List<int>();
			for(int i=0;i<columns.Count;i++){
				if(i==0){
					ColPos.Add(0);
				}
				else{
					ColPos.Add(ColPos[i-1]+columns[i-1].ColWidth);
				}
			}
			if(columns.Count>0){
				GridW=ColPos[ColPos.Count-1]+columns[columns.Count-1].ColWidth;
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
						textWidth=g.MeasureString(rows[i].Cells[j].Text,this.cellFont).Width+5;//the 5 is a buffer.
						rowCount=(int)Math.Ceiling((double)(textWidth/(float)columns[j].ColWidth));//approximation.
						cellH=(int)(rowCount*textHeight)+1;
						if(cellH>RowHeights[i]) {
							RowHeights[i]=cellH;
						}
					}
				}
				else{
					RowHeights[i]=(int)g.MeasureString("Any",this.cellFont).Height+1;
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
			if(y<1+headerHeight){
				return-1;
			}
			for(int i=0;i<rows.Count;i++){
				//if(y>-vScroll.Value+1+titleHeight+headerHeight+RowLocs[i]+RowHeights[i]+NoteHeights[i]){
				if(y>1+headerHeight+RowLocs[i]+RowHeights[i]){
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
					colRight+=columns[c].ColWidth;
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

		//protected override void OnPaint(PaintEventArgs pe) {
			// TODO: Add custom paint code here

			// Calling the base class OnPaint
		//	base.OnPaint(pe);
		//}
		
		///<summary>Runs any time the control is invalidated.</summary>
		protected override void OnPaint(PaintEventArgs e){
			if(IsUpdating){
				return;
			}
			if(Width<1 || Height<1) {
				return;
			}
			ComputeColumns();//it's only here because I can't figure out how to do it when columns are added. It will be removed.
			Bitmap doubleBuffer=new Bitmap(Width,Height);//,e.Graphics);//e.Graphics would have specified the resolution of the bitmap.
			Graphics g=Graphics.FromImage(doubleBuffer);
			DrawBackG(g);
			DrawRows(g);
			DrawTitleAndHeaders(g);//this will draw on top of any grid stuff
			DrawOutline(g);
			e.Graphics.DrawImage(doubleBuffer,0,0);
			g.Dispose();
			base.OnPaint(e);
		}

		///<summary>Draws a solid gray background.</summary>
		private void DrawBackG(Graphics g){
			g.FillRectangle(new SolidBrush(Color.FromArgb(224,223,227)),
				0,headerHeight+1,
				Width,this.Height-headerHeight-1);
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
			//normal row color
			//need to draw over the gray background
			g.FillRectangle(new SolidBrush(Color.White),
				1,
				1+headerHeight+RowLocs[rowI]+1,
				GridW,//this is a really simple width value that always works well
				RowHeights[rowI]-1);
			for(int i=0;i<columns.Count;i++){
				//right vertical gridline
				if(rowI==0){
					g.DrawLine(gridPen,
						1+ColPos[i]+columns[i].ColWidth,
						1+headerHeight+RowLocs[rowI],
						1+ColPos[i]+columns[i].ColWidth,
						1+headerHeight+RowLocs[rowI]+RowHeights[rowI]);
				}
				else{
					g.DrawLine(gridPen,
						1+ColPos[i]+columns[i].ColWidth,
						1+headerHeight+RowLocs[rowI]+1,
						1+ColPos[i]+columns[i].ColWidth,
						1+headerHeight+RowLocs[rowI]+RowHeights[rowI]);
				}
				//lower horizontal gridline
				if(i==0){
					g.DrawLine(lowerPen,
						1+ColPos[i],
						1+headerHeight+RowLocs[rowI]+RowHeights[rowI],
						1+ColPos[i]+columns[i].ColWidth,
						1+headerHeight+RowLocs[rowI]+RowHeights[rowI]);
				}
				else{
					g.DrawLine(lowerPen,
						1+ColPos[i]+1,
						1+headerHeight+RowLocs[rowI]+RowHeights[rowI],
						1+ColPos[i]+columns[i].ColWidth,
						1+headerHeight+RowLocs[rowI]+RowHeights[rowI]);
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
				int vertical=1+headerHeight+RowLocs[rowI]+1;
				int horizontal=1+ColPos[i]+2;
				int cellW=columns[i].ColWidth-2;
				int cellH=RowHeights[rowI];
				if(columns[i].TextAlign==HorizontalAlignment.Right){
					horizontal-=2;
					cellW+=2;
				}
				textRect=new RectangleF(horizontal,vertical,cellW,cellH);
				g.DrawString(rows[rowI].Cells[i].Text,cellFont,textBrush,textRect,format);
			}
		}

		///<summary>There isn't actually a title in this compact version.</summary>
		private void DrawTitleAndHeaders(Graphics g){
			//Column Headers-----------------------------------------------------------------------------------------
			g.FillRectangle(new SolidBrush(this.cTitleBackG),0,0,Width,headerHeight);//background
			g.DrawLine(new Pen(Color.FromArgb(102,102,122)),0,0,Width,0);//line between title and headers
			for(int i=0;i<columns.Count;i++){
				if(i!=0){
					//vertical lines separating column headers
					g.DrawLine(new Pen(Color.FromArgb(120,120,120)),1+ColPos[i],3,
						1+ColPos[i],headerHeight-2);
					g.DrawLine(new Pen(Color.White),1+ColPos[i]+1,3,
						1+ColPos[i]+1,headerHeight-2);
				}
				g.DrawString(columns[i].Heading,headerFont,new SolidBrush(Color.Black),
					ColPos[i]+columns[i].ColWidth/2-g.MeasureString(columns[i].Heading,headerFont).Width/2,
					2);
			}
			//line below headers
			g.DrawLine(new Pen(Color.FromArgb(120,120,120)),0,headerHeight,Width,headerHeight);
		}

		///<summary>Draws outline around entire control.</summary>
		private void DrawOutline(Graphics g){
			g.DrawRectangle(new Pen(this.cBlueOutline),0,0,Width-1,Height-1);
		}
		#endregion Painting

		#region Clicking

		#endregion Clicking

		#region BeginEndUpdate
		///<summary>Call this before adding any rows.  You would typically call Rows.Clear after this.</summary>
		public void BeginUpdate(){
			IsUpdating=true;
		}

		///<summary>Must be called after adding rows.  This computes the columns, computes the rows, lays out the scrollbars, clears SelectedIndices, and invalidates.  Does not zero out scrollVal.  Sometimes, it seems like scrollVal needs to be reset somehow because it's an inappropriate number, and when you first grab the scrollbar, it jumps.  No time to investigate.</summary>
		public void EndUpdate(){
			ComputeColumns();
			ComputeRows();
			//unique to mobile version, we always make the grid the same size as the data:
			this.Width=GridW;
			this.Height=headerHeight+GridH+1;
			IsUpdating=false;
			Invalidate();
		}
		#endregion BeginEndUpdate

		
	}
}
