using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental
{
	///<summary>Summary description for ContrPerio.</summary>
	public class ContrPerio : System.Windows.Forms.Control{
		///<summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		///<summary>Width of one measurement. A tooth is 3 times this amount, not counting borders.</summary> 
		private int Wmeas;
		///<summary>Height of one row of probing. Gives a little extra room for bleeding.</summary> 
		private int Hprob;
		///<summary>Height of one of the shorter rows (non probing).</summary> 
		private int Hshort;
		///<summary>Rows of probing depths.</summary> 
		private int RowsProbing;
		///<summary>First dimension is either UF, UL, LF, or LL. Second dim is an array of the types of rows showing in that section.</summary>
		private PerioSequenceType[][] RowTypes;
		///<summary>Width of the left column that holds descriptions and dates.</summary> 
		private int Wleft;
		///<summary>Height of the 'tooth' sections. Right now, it just holds the tooth number.</summary> 
		private int Htooth;
		///<summary>Color of the outer border and the major dividers.</summary>
		private Color cBorder;
		///<summary>Color of the background section of the shorter inner rows.</summary>
		private Color cBackShort;
		///<summary>Color of a highlighted cell.</summary>
		private Color cHi;
		///<summary>Color of the text of a skipped tooth.</summary>
		private Color cSkip;
		///<summary>Color of the vertical lines between each tooth.</summary>
		private Color cVertical;
		///<summary>Color of the minor horizontal lines between rows.</summary>
		private Color cHoriz;
		///<summary>Color of the main background.</summary>
		private Color cBack;
		///<summary>Color of the horizontal lines in the shorter inner rows.</summary>
		private Color cHorizShort;
		///<summary>Color of red probing depths.</summary>
		private Color cRedText;
		///<summary>Color of the dot over a number representing blood.</summary>
		private Color cBlood;
		///<summary>Color of the dot over a number representing suppuration.</summary>
		private Color cSupp;
		///<summary>Color of the dot over a number representing plaque.</summary>
		private Color cPlaque;
		///<summary>Color of the dot over a number representing calculus.</summary>
		private Color cCalc;
		///<summary>Color of previous measurements from a different exam. Slightly grey.</summary>
		private Color cOldText;
		///<summary>Color of previous red measurements from a different exam. Lighter red.</summary>
		private Color cOldTextRed;
		///<summary>This data array gets filled when loading an exam. It is altered as the user makes changes, and then the results are saved to the db by reading from this array.</summary>
		private PerioCell[,] DataArray;
		///<summary>Since it is complex to compute Y coordinate of each cell, the values are stored in this array.  Used by GetBounds.</summary>
		private float[] TopCoordinates;
		///<summary>Stores the column,row of the currently selected cell. Null if none selected.</summary>
		private Point CurCell;
		///<summary>Set true to go right, false to go left.</summary>
		public bool DirectionIsRight;
		///<summary>The index in PerioExams.List of the currently selected exam.</summary>
		private int selectedExam;
		///<summary>the offset when there are more rows than will display. Value is set at the same time as SelectedExam. So usually 0. Otherwise 1,2,3 or....</summary>
		private int ProbingOffset;
		///<summary>Keeps track of what has changed for current exam. Dim 1 is sequence. Dim 2 is toothNum.</summary>
		public bool[,] HasChanged;
		///<summary>Valid values 1-32 int. User can highlight teeth to mark them as skip tooth. The highighting is done completely separately from the normal highlighting functionality because multiple teeth can be highlighted.</summary>
		private ArrayList selectedTeeth;
		///<summary>Valid values 1-32 int. Applies only to the current exam. Loaded from the db durring LoadData().</summary>
		private ArrayList skippedTeeth;
		///<summary></summary>
		[Category("Property Changed"),Description("Occurs when the control needs to change the auto advance direction to right.")]
		public event EventHandler DirectionChangedRight = null;
		///<summary></summary>
		[Category("Property Changed"),Description("Occurs when the control needs to change the auto advance direction to left.")]
		public event EventHandler DirectionChangedLeft = null;
		///<summary>Causes each data entry to be entered three times. Also, if the data is a bleeding flag entry, then it changes the behavior by causing it to advance also.</summary>
		public bool ThreeAtATime;
		//public PerioExam PerioExamCur;

		///<summary>The index in PerioExams.List of the currently selected exam.</summary>
		public int SelectedExam{
			get{
				return selectedExam;
			}
			set{
				selectedExam=value;
				ProbingOffset=0;
				if(selectedExam>RowsProbing-1)
					ProbingOffset=selectedExam-RowsProbing+1;
			}
		}

		///<summary></summary>
		protected override Size DefaultSize{
			get{
				return new Size(590,665);
			}
		}

		///<summary></summary>
		public ContrPerio(){
			//InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			this.BackColor = System.Drawing.SystemColors.Window;
			cBorder=Color.Black;
			//cBackShort=Color.FromArgb(237,237,237);//larger numbers will make it whiter
			cBackShort=Color.FromArgb(225,225,225);
			cHi=Color.FromArgb(158,146,142);//Color.DarkSalmon;
			cSkip=Color.LightGray;
			cVertical=Color.Silver;
			cHoriz=Color.LightGray;
			cBack=Color.White;
			cHorizShort=Color.Silver;//or darkgrey
			cRedText=Color.Red;
			SetColors();
			cOldText=Color.FromArgb(120,120,120);
			cOldTextRed=Color.FromArgb(200,80,80);
			RowsProbing=6;
			RowTypes=new PerioSequenceType[4][];
			//Upper facial:
			RowTypes[0]=new PerioSequenceType[5+RowsProbing];
			RowTypes[0][0]=PerioSequenceType.Mobility;
			RowTypes[0][1]=PerioSequenceType.Furcation;
			RowTypes[0][2]=PerioSequenceType.CAL;
			RowTypes[0][3]=PerioSequenceType.GingMargin;
			RowTypes[0][4]=PerioSequenceType.MGJ;
			for(int i=0;i<RowsProbing;i++){
				RowTypes[0][5+i]=PerioSequenceType.Probing;
			}
			//Upper lingual:
			RowTypes[1]=new PerioSequenceType[3+RowsProbing];
			RowTypes[1][0]=PerioSequenceType.Furcation;
			RowTypes[1][1]=PerioSequenceType.CAL;
			RowTypes[1][2]=PerioSequenceType.GingMargin;
			for(int i=0;i<RowsProbing;i++){
				RowTypes[1][3+i]=PerioSequenceType.Probing;
			}
			//Lower lingual:
			RowTypes[2]=new PerioSequenceType[4+RowsProbing];
			RowTypes[2][0]=PerioSequenceType.Furcation;
			RowTypes[2][1]=PerioSequenceType.CAL;
			RowTypes[2][2]=PerioSequenceType.GingMargin;
			RowTypes[2][3]=PerioSequenceType.MGJ;
			for(int i=0;i<RowsProbing;i++){
				RowTypes[2][4+i]=PerioSequenceType.Probing;
			}
			//Lower facial:
			RowTypes[3]=new PerioSequenceType[5+RowsProbing];
			RowTypes[3][0]=PerioSequenceType.Mobility;
			RowTypes[3][1]=PerioSequenceType.Furcation;
			RowTypes[3][2]=PerioSequenceType.CAL;
			RowTypes[3][3]=PerioSequenceType.GingMargin;
			RowTypes[3][4]=PerioSequenceType.MGJ;
			for(int i=0;i<RowsProbing;i++){
				RowTypes[3][5+i]=PerioSequenceType.Probing;
			}
			Wmeas=10;
			Wleft=65;
			Hprob=16;
			Hshort=12;
			Htooth=16;
			ClearDataArray();
			FillTopCoordinates();
			CurCell=new Point(-1,-1);//my way of setting it to null.
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/*
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// ContrPerio
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Name = "ContrPerio";
		}
		#endregion
		*/

		///<summary>Sets the user editable colors</summary>
		public void SetColors(){
			if(DefB.Long==null){
				cBlood=Color.FromArgb(240,20,20);
				cSupp=Color.FromArgb(255,160,0);
				cPlaque=Color.FromArgb(240,20,20);
				cCalc=Color.FromArgb(255,160,0);
			}
			else{
				cBlood=DefB.Long[(int)DefCat.MiscColors][1].ItemColor;
				cSupp=DefB.Long[(int)DefCat.MiscColors][2].ItemColor;
				cPlaque=DefB.Long[(int)DefCat.MiscColors][4].ItemColor;
				cCalc=DefB.Long[(int)DefCat.MiscColors][5].ItemColor;
			}
		}

		///<summary></summary>
		protected override void OnPaint(PaintEventArgs e){
			//base.OnPaint (e);
			//Graphics g=e.Graphics;
			DrawBackground(e);
			DrawSkippedTeeth(e);
			DrawSelectedTeeth(e);
			DrawCurCell(e);
			DrawGridlines(e);
			DrawText(e);
			//DrawTempDots(e);
		}
	
		//private void DrawTempDots(System.Windows.Forms.PaintEventArgs e){
			//Graphics g=e.Graphics;
			//for(int i=0;i<TopCoordinates.Length;i++){
			//	g.DrawLine(Pens.Red,20,TopCoordinates[24],25,TopCoordinates[24]);
			//}
		//}

		private void DrawBackground(System.Windows.Forms.PaintEventArgs e){
			Graphics g=e.Graphics;
			int top;
			int bottom;
			//rect 1
			int yPos1=1+RowsProbing*(Hprob+1);
			int yPos2=yPos1+(RowTypes[0].Length-RowsProbing)*(Hshort+1)-1;
			top=yPos1;
			bottom=yPos2;
			if(e.ClipRectangle.Bottom>=top && e.ClipRectangle.Top<=bottom){
				if(e.ClipRectangle.Bottom<=bottom){
					bottom=e.ClipRectangle.Bottom;
				}
				if(e.ClipRectangle.Top>=top){
					top=e.ClipRectangle.Top;
				}
				g.FillRectangle(new SolidBrush(cBackShort),e.ClipRectangle.X,top
					,e.ClipRectangle.Width,bottom-top);
			}
			//rect 2
			yPos1=yPos2+1+Htooth+1;
			yPos2=yPos1+(RowTypes[1].Length-RowsProbing)*(Hshort+1)-1;
			top=yPos1;
			bottom=yPos2;
			if(e.ClipRectangle.Bottom>=top && e.ClipRectangle.Top<=bottom){
				if(e.ClipRectangle.Bottom<=bottom){
					bottom=e.ClipRectangle.Bottom;
				}
				if(e.ClipRectangle.Top>=top){
					top=e.ClipRectangle.Top;
				}
				g.FillRectangle(new SolidBrush(cBackShort),e.ClipRectangle.X,top
					,e.ClipRectangle.Width,bottom-top);
			}
			//rect 3
			yPos1=yPos2+1+RowsProbing*(Hprob+1)+1+RowsProbing*(Hprob+1);
			yPos2=yPos1+(RowTypes[2].Length-RowsProbing)*(Hshort+1)-1;
			top=yPos1;
			bottom=yPos2;
			if(e.ClipRectangle.Bottom>=top && e.ClipRectangle.Top<=bottom){
				if(e.ClipRectangle.Bottom<=bottom){
					bottom=e.ClipRectangle.Bottom;
				}
				if(e.ClipRectangle.Top>=top){
					top=e.ClipRectangle.Top;
				}
				g.FillRectangle(new SolidBrush(cBackShort),e.ClipRectangle.X,top
					,e.ClipRectangle.Width,bottom-top);
			}
			//rect 4
			yPos1=yPos2+1+Htooth+1;
			yPos2=yPos1+(RowTypes[3].Length-RowsProbing)*(Hshort+1)-1;
			top=yPos1;
			bottom=yPos2;
			if(e.ClipRectangle.Bottom>=top && e.ClipRectangle.Top<=bottom){
				if(e.ClipRectangle.Bottom<=bottom){
					bottom=e.ClipRectangle.Bottom;
				}
				if(e.ClipRectangle.Top>=top){
					top=e.ClipRectangle.Top;
				}
				g.FillRectangle(new SolidBrush(cBackShort),e.ClipRectangle.X,top
					,e.ClipRectangle.Width,bottom-top);
			}
		}

		///<summary>Draws the greyed out background for the skipped teeth.</summary>
		private void DrawSkippedTeeth(System.Windows.Forms.PaintEventArgs e){
			if(skippedTeeth==null || skippedTeeth.Count==0)
				return;
			Graphics g=e.Graphics;
			float xLoc=0;
			float yLoc=0;
			float h=0;
			float w=0;
			RectangleF bounds;//used in the loop to represent the bounds of the entire tooth to be greyed
			for(int i=0;i<skippedTeeth.Count;i++){
				if((int)skippedTeeth[i]<17){//max tooth
					xLoc=1+Wleft+1+((int)skippedTeeth[i]-1)*3*(Wmeas+1);
					//xLoc=1+Wleft+1+(col-1)*(Wmeas+1);
					yLoc=1;
					h=TopCoordinates[GetTableRow(1,RowTypes[1].Length-1)]-yLoc+Hprob;
					w=3*(Wmeas+1);
				}
				else{//mand tooth
					xLoc=1+Wleft+1+(33-(int)skippedTeeth[i]-1)*3*(Wmeas+1);
					yLoc=TopCoordinates[GetTableRow(2,RowTypes[2].Length-1)];
					h=TopCoordinates[GetTableRow(3,RowTypes[3].Length-1)]-yLoc+Hprob;
					w=3*(Wmeas+1);
				}
				bounds=new RectangleF(xLoc,yLoc,w,h);
				int top=(int)bounds.Top;
				int bottom=(int)bounds.Bottom;
				int left=(int)bounds.Left;
				int right=(int)bounds.Right;
				//test clipping rect later
				//MessageBox.Show(bounds.ToString());
				g.FillRectangle(new SolidBrush(cBackShort),left,top
					,right-left,bottom-top);
			}
		}

		///<summary>Draws the highlighted background for selected teeth(not used very often unless user has been clicking on tooth numbers in preparation for setting skipped teeth. Then, highlighting goes away.</summary>
		private void DrawSelectedTeeth(System.Windows.Forms.PaintEventArgs e){
			if(selectedTeeth==null || selectedTeeth.Count==0)
				return;
			Graphics g=e.Graphics;
			float xLoc=0;
			float yLoc=0;
			float h=0;
			float w=0;
			RectangleF bounds;//used in the loop to represent the bounds to be greyed
			for(int i=0;i<selectedTeeth.Count;i++){
				if((int)selectedTeeth[i]<17){//max tooth
					xLoc=1+Wleft+1+((int)selectedTeeth[i]-1)*3*(Wmeas+1);
					yLoc=TopCoordinates[GetTableRow(true)];
					h=Htooth;
					w=3*(Wmeas+1);
				}
				else{//mand tooth
					xLoc=1+Wleft+1+(33-(int)selectedTeeth[i]-1)*3*(Wmeas+1);
					yLoc=TopCoordinates[GetTableRow(false)];
					h=Htooth;
					w=3*(Wmeas+1);
				}
				bounds=new RectangleF(xLoc,yLoc,w,h);
				int top=(int)bounds.Top;
				int bottom=(int)bounds.Bottom;
				int left=(int)bounds.Left;
				int right=(int)bounds.Right;
				//test clipping rect later
				g.FillRectangle(new SolidBrush(cHi),left,top
					,right-left,bottom-top);
			}
		}

		///<summary>Draws the highlighted background for the current cell.</summary>
		private void DrawCurCell(System.Windows.Forms.PaintEventArgs e){
			if(CurCell.X==-1){
				return;
			}
			Graphics g=e.Graphics;
			RectangleF bounds=GetBounds(CurCell.X,CurCell.Y);
			if(RowTypes[GetSection(CurCell.Y)][GetSectionRow(CurCell.Y)]==PerioSequenceType.Probing){
				bounds=new RectangleF(bounds.X,bounds.Y+Hprob-Hshort,
					bounds.Width,Hshort);
			}
			int top=(int)bounds.Top;
			int bottom=(int)bounds.Bottom;
			int left=(int)bounds.Left;
			int right=(int)bounds.Right;
			if(e.ClipRectangle.Bottom>=bounds.Top && e.ClipRectangle.Top<=bounds.Bottom
				&& e.ClipRectangle.Right>=bounds.Left && e.ClipRectangle.Left<=bounds.Right)
			{//if the clipping rect includes any part of the CurCell
				if(e.ClipRectangle.Bottom<=bottom){
					bottom=e.ClipRectangle.Bottom;
				}
				if(e.ClipRectangle.Top>=top){
					top=e.ClipRectangle.Top;
				}
				if(e.ClipRectangle.Right<=right){
					right=e.ClipRectangle.Right;
				}
				if(e.ClipRectangle.Left>=left){
					left=e.ClipRectangle.Left;
				}
				g.FillRectangle(new SolidBrush(cHi),left,top
					,right-left,bottom-top);
			}
		}

		private void DrawGridlines(System.Windows.Forms.PaintEventArgs e){
			Graphics g=e.Graphics;
			int yPos=0;
			if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
				g.DrawLine(new Pen(cBorder),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
			//inside each loop, we are drawing the bottom line of each cell
			//U facial
			for(int i=RowTypes[0].Length-1;i>=0;i--){
				if(RowTypes[0][i]==PerioSequenceType.Probing){
					yPos+=1+Hprob;
					if(i==RowTypes[0].Length-RowsProbing){//if last row, eg 4==10-6
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cBorder),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
					else{//regular rows
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cHoriz),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
				}
				else{//short rows
					yPos+=1+Hshort;
					if(i==0){//if last row
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cBorder),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
					else{//regular rows
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cHorizShort),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
				}
			}
			yPos+=1+Htooth;
			if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
				g.DrawLine(new Pen(cBorder),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
			//upper lingual
			for(int i=0;i<RowTypes[1].Length;i++){
				if(RowTypes[1][i]==PerioSequenceType.Probing){
					yPos+=1+Hprob;
					if(i==RowTypes[1].Length-1){//if last row
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cBorder),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
					else{//regular rows
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cHoriz),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
				}
				else{//short rows
					yPos+=1+Hshort;
					if(i==RowTypes[1].Length-RowsProbing-1){//if last row. eg 8-6-1=1
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cBorder),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
					else{//regular rows
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cHorizShort),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
				}
			}
			yPos+=1;//makes a double line between u and L
			if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
				g.DrawLine(new Pen(cBorder),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
			//lower lingual
			for(int i=RowTypes[2].Length-1;i>=0;i--){
				if(RowTypes[2][i]==PerioSequenceType.Probing){
					yPos+=1+Hprob;
					if(i==RowTypes[2].Length-RowsProbing){//if last row, eg 4==10-6
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cBorder),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
					else{//regular rows
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cHoriz),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
				}
				else{//short rows
					yPos+=1+Hshort;
					if(i==0){//if last row
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cBorder),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
					else{//regular rows
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cHorizShort),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
				}
			}
			yPos+=1+Htooth;
			if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
				g.DrawLine(new Pen(cBorder),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
			//lower facial
			for(int i=0;i<RowTypes[3].Length;i++){
				if(RowTypes[3][i]==PerioSequenceType.Probing){
					yPos+=1+Hprob;
					if(i==RowTypes[3].Length-1){//if last row
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cBorder),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
					else{//regular rows
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cHoriz),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
				}
				else{//short rows
					yPos+=1+Hshort;
					if(i==RowTypes[3].Length-RowsProbing-1){//if last row. eg 8-6-1=1
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cBorder),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
					else{//regular rows
						if(e.ClipRectangle.Top<=yPos && e.ClipRectangle.Bottom>=yPos)
							g.DrawLine(new Pen(cHorizShort),e.ClipRectangle.Left,yPos,e.ClipRectangle.Right,yPos);
					}
				}
			}
			//VERTICAL LINES
			int xPos=0;
			if(e.ClipRectangle.Left<=xPos && e.ClipRectangle.Right>=xPos)
				g.DrawLine(new Pen(cBorder),xPos,e.ClipRectangle.Top,xPos,e.ClipRectangle.Bottom);
			xPos+=Wleft+1;
			if(e.ClipRectangle.Left<=xPos && e.ClipRectangle.Right>=xPos)
				g.DrawLine(new Pen(cBorder),xPos,e.ClipRectangle.Top,xPos,e.ClipRectangle.Bottom);
			for(int i=0;i<16;i++){
				xPos+=3*(Wmeas+1);
				if(e.ClipRectangle.Left<=xPos && e.ClipRectangle.Right>=xPos){
					if(i==7 || i==15)
						g.DrawLine(new Pen(cBorder),xPos,e.ClipRectangle.Top,xPos,e.ClipRectangle.Bottom);
					else
						g.DrawLine(new Pen(cVertical),xPos,e.ClipRectangle.Top,xPos,e.ClipRectangle.Bottom);
				}
			}
		}

		private void DrawText(System.Windows.Forms.PaintEventArgs e){
			//Graphics g=e.Graphics;
			for(int i=0;i<DataArray.GetLength(1);i++){//loop through all rows in the table
				//test for clip later
				DrawRow(i,e);
			}
		}

		private void DrawRow(int row,System.Windows.Forms.PaintEventArgs e){
			e.Graphics.SmoothingMode=SmoothingMode.HighQuality;//.AntiAlias;
			//e.Graphics.PixelOffsetMode=PixelOffsetMode.HighQuality;
			//e.Graphics.TextRenderingHint=TextRenderingHint.AntiAlias;
			Font font;
			Color textColor;
			StringFormat format=new StringFormat();
			//format.LineAlignment is useless for small vertical changes like we need
			RectangleF rect;
			bool drawOld;
			int redThresh=0;
			int cellValue=0;
			for(int i=0;i<DataArray.GetLength(0);i++){//loop through all columns in the row
				rect=GetBounds(i,row);
				font=Font;
				textColor=Color.Black;
				//test for clip later
				if(i==0){//first column
					format.Alignment=StringAlignment.Far;//align right
					e.Graphics.DrawString(DataArray[i,row].Text,font,
						new SolidBrush(textColor),rect,format);
					//e.Graphics.DrawString("test",new Font("Arial",8),Brushes.Black,rect);
					continue;
				}
				else if(GetSection(row)==-1){//tooth row
					font=new Font(Font,FontStyle.Bold);
					format.Alignment=StringAlignment.Center;
					rect=new RectangleF(rect.X,rect.Y+2,rect.Width,rect.Height);
					e.Graphics.DrawString(DataArray[i,row].Text,font,
						new SolidBrush(textColor),rect,format);
					//e.Graphics.DrawString(DataArray[i,row].Text,Font,Brushes.Black,rect);
					continue;
				}
				format.Alignment=StringAlignment.Center;//center left/right
				if(RowTypes[GetSection(row)][GetSectionRow(row)]==PerioSequenceType.Probing){
					if((DataArray[i,row].Bleeding & BleedingFlags.Plaque) > 0){
						e.Graphics.FillRectangle(new SolidBrush(cPlaque),rect.X+0,rect.Y,2.5f,3.5f);
					}
					if((DataArray[i,row].Bleeding & BleedingFlags.Calculus) > 0){
						e.Graphics.FillRectangle(new SolidBrush(cCalc),rect.X+2.5f,rect.Y,2.5f,3.5f);
					}
					if((DataArray[i,row].Bleeding & BleedingFlags.Blood) > 0){
						e.Graphics.FillRectangle(new SolidBrush(cBlood),rect.X+5f,rect.Y,2.5f,3.5f);
					}
					if((DataArray[i,row].Bleeding & BleedingFlags.Suppuration) > 0){
						e.Graphics.FillRectangle(new SolidBrush(cSupp),rect.X+7.5f,rect.Y,2.5f,3.5f);
					}
					rect=new RectangleF(rect.X,rect.Y+4,rect.Width,rect.Height);
				}
				if((DataArray[i,row].Text==null || DataArray[i,row].Text=="")
						&& (DataArray[i,row].OldText==null || DataArray[i,row].OldText=="")){
					continue;//no text to draw
				}
				if(DataArray[i,row].Text==null || DataArray[i,row].Text==""){
					drawOld=true;
					cellValue=PIn.PInt(DataArray[i,row].OldText);
					textColor=Color.Gray;
				}
				else{
					drawOld=false;
					cellValue=PIn.PInt(DataArray[i,row].Text);
					textColor=Color.Black;
				}
				//test for red
				switch(RowTypes[GetSection(row)][GetSectionRow(row)]){
					case PerioSequenceType.Probing:
						redThresh=PIn.PInt(((Pref)PrefB.HList["PerioRedProb"]).ValueString);
						break;
					case PerioSequenceType.MGJ:
						redThresh=PIn.PInt(((Pref)PrefB.HList["PerioRedMGJ"]).ValueString);
						break;
					case PerioSequenceType.GingMargin:
						redThresh=PIn.PInt(((Pref)PrefB.HList["PerioRedGing"]).ValueString);
						break;
					case PerioSequenceType.CAL:
						redThresh=PIn.PInt(((Pref)PrefB.HList["PerioRedCAL"]).ValueString);
						break;
					case PerioSequenceType.Furcation:
						redThresh=PIn.PInt(((Pref)PrefB.HList["PerioRedFurc"]).ValueString);
						break;
					case PerioSequenceType.Mobility:
						redThresh=PIn.PInt(((Pref)PrefB.HList["PerioRedMob"]).ValueString);
						break;
				}
				if((RowTypes[GetSection(row)][GetSectionRow(row)]
					==PerioSequenceType.MGJ && cellValue<=redThresh)
					||(RowTypes[GetSection(row)][GetSectionRow(row)]
					!=PerioSequenceType.MGJ && cellValue>=redThresh))
				{
					if(drawOld)
						textColor=cOldTextRed;
					else
						textColor=cRedText;
					font=new Font(Font,FontStyle.Bold);
				}
				//if number is two digits:
				if(cellValue>9){
					font=new Font("SmallFont",7);
					rect=new RectangleF(rect.X-2,rect.Y+1,rect.Width+5,rect.Height);
				}
				//e.Graphics.DrawString(cellValue.ToString(),Font,Brushes.Black,rect);
				e.Graphics.DrawString(cellValue.ToString(),font,
					new SolidBrush(textColor),rect,format);
			}//i col
		}

		///<summary>Gets the bounds for a single cell.</summary>
		private RectangleF GetBounds(int col,int row){
			float xLoc;
			if(col==0){
				xLoc=0;
			}
			else{
				xLoc=1+Wleft+1+(col-1)*(Wmeas+1);
			}
			if(GetSection(row)==-1){//tooth row
				xLoc-=Wmeas;
			}
			float h=0;
			//if(row==24){
			//	MessageBox.Show(RowTypes[GetSection(row)][GetSectionRow(row)].ToString());
				//MessageBox.Show(GetSection(row).ToString()+","+GetSectionRow(row).ToString());
			//}
			//try{
			if(GetSection(row)==-1){//tooth row
				h=Htooth;
			}
			else if(RowTypes[GetSection(row)][GetSectionRow(row)]==PerioSequenceType.Probing){//probing
				h=Hprob;
			}
			else{
				h=Hshort+1;//added the 1 so that a lower case y can drop a little lower.
			}
			//}
			//catch{
			//	MessageBox.Show(row.ToString());
			//}
			float w;
			if(GetSection(row)==-1){//tooth row
				w=Wmeas*3;
			}
			else if(col==0)
				w=Wleft+1;
			else
				w=Wmeas;
			//try{
			//if(row==10)
			//	MessageBox.Show(TopCoordinates[row].ToString());
			return new RectangleF(xLoc,TopCoordinates[row],w,h);
			//}
			//catch{
				//MessageBox.Show(row.ToString());
			//}
			//return new RectangleF(0,0,70,20);
		}
		
		///<summary>Fills an array with all the Y coordinates of each row for faster retrieval by GetBounds, and calculation of click events.</summary>
		private void FillTopCoordinates(){
			TopCoordinates=new float[DataArray.GetLength(1)];
			//int curRow=0;
			int yPos=1;
			//U facial
			for(int i=RowTypes[0].Length-1;i>=0;i--){
				TopCoordinates[GetTableRow(0,i)]=yPos;
				//MessageBox.Show(GetTableRow(0,i));
				if(RowTypes[0][i]==PerioSequenceType.Probing){
					yPos+=Hprob+1;
				}
				else{
					yPos+=Hshort+1;
				}
			}
			TopCoordinates[GetTableRow(true)]=yPos;
			yPos+=Htooth+1;
			//upper lingual
			for(int i=0;i<RowTypes[1].Length;i++){
				TopCoordinates[GetTableRow(1,i)]=yPos;
				if(RowTypes[1][i]==PerioSequenceType.Probing){
					yPos+=Hprob+1;
				}
				else{
					yPos+=Hshort+1;
				}
			}
			yPos+=1;//makes a double line between u and L
			//lower lingual
			//MessageBox.Show(GetTableRow(2,3).ToString());
			for(int i=RowTypes[2].Length-1;i>=0;i--){
				TopCoordinates[GetTableRow(2,i)]=yPos;
				if(RowTypes[2][i]==PerioSequenceType.Probing){
					yPos+=Hprob+1;
				}
				else{
					yPos+=Hshort+1;
				}
			}
			TopCoordinates[GetTableRow(false)]=yPos;
			yPos+=Htooth+1;
			//lower facial
			for(int i=0;i<RowTypes[3].Length;i++){
				TopCoordinates[GetTableRow(3,i)]=yPos;
				if(RowTypes[3][i]==PerioSequenceType.Probing){
					yPos+=Hprob+1;
				}
				else{
					yPos+=Hshort+1;
				}
			}
		}

		///<summary>Loads data from the PerioMeasures lists into the visible grid.</summary>
		public void LoadData(){
			ClearDataArray();
			selectedTeeth=new ArrayList();
			skippedTeeth=new ArrayList();
			//reset all HasChanged values to false
			HasChanged=new bool[PerioMeasures.List.GetLength(1),33]; 
			if(selectedExam==-1){
				return;
			}
			FillDates();
			Point curCell;
			//int examI=selectedExam;
			string cellText="";
			int cellBleed=0;
			for(int examI=0;examI<=selectedExam;examI++){//exams, earliest through current
				for(int seqI=0;seqI<PerioMeasures.List.GetLength(1);seqI++){//sequences
					for(int toothI=1;toothI<PerioMeasures.List.GetLength(2);toothI++){//measurements
						if(PerioMeasures.List[examI,seqI,toothI]==null)//.PerioMeasureNum==0)
							continue;//no measurement for this seq and tooth
						for(int surfI=0;surfI<Enum.GetNames(typeof(PerioSurf)).Length;surfI++){//surfaces(6or7)
							if(seqI==(int)PerioSequenceType.SkipTooth){
								//There is nothing in the data array to fill because it is not user editable.
								if(surfI!=(int)PerioSurf.None){
									continue;
								}
								if(examI!=selectedExam){//only mark skipped teeth for current exam
									continue;
								}
								if(PerioMeasures.List[examI,seqI,toothI].ToothValue==1){
									skippedTeeth.Add(toothI);
								}
								continue;
							}
							else if(seqI==(int)PerioSequenceType.Mobility){
								if(surfI!=(int)PerioSurf.None){
									continue;
								}
								curCell=GetCell(examI,PerioMeasures.List[examI,seqI,toothI].SequenceType
									,toothI,PerioSurf.B);
								cellText=PerioMeasures.List[examI,seqI,toothI].ToothValue.ToString();
								if(cellText=="-1")
									cellText="";
								if(examI==selectedExam)
									DataArray[curCell.X,curCell.Y].Text=cellText;
								else
									DataArray[curCell.X,curCell.Y].OldText=cellText;
								continue;
							}
							else if(seqI==(int)PerioSequenceType.Bleeding){
								if(surfI==(int)PerioSurf.None){
									continue;
								}
								curCell=GetCell(examI,PerioSequenceType.Probing,toothI,(PerioSurf)surfI);
								if(curCell.X==-1 || curCell.Y==-1)
									//if there are more exams than will fit.
									continue;
								switch(surfI){
									case (int)PerioSurf.B:
										cellBleed=PerioMeasures.List[examI,seqI,toothI].Bvalue;
										break;
									case (int)PerioSurf.DB:
										cellBleed=PerioMeasures.List[examI,seqI,toothI].DBvalue;
										break;
									case (int)PerioSurf.DL:
										cellBleed=PerioMeasures.List[examI,seqI,toothI].DLvalue;
										break;
									case (int)PerioSurf.L:
										cellBleed=PerioMeasures.List[examI,seqI,toothI].Lvalue;
										break;
									case (int)PerioSurf.MB:
										cellBleed=PerioMeasures.List[examI,seqI,toothI].MBvalue;
										break;
									case (int)PerioSurf.ML:
										cellBleed=PerioMeasures.List[examI,seqI,toothI].MLvalue;
										break;
								}
								if(cellBleed==-1)//this shouldn't happen, but just in case
									cellBleed=0;
								DataArray[curCell.X,curCell.Y].Bleeding=(BleedingFlags)cellBleed;
								continue;
							}
							curCell=GetCell(examI,PerioMeasures.List[examI,seqI,toothI].SequenceType
								,toothI,(PerioSurf)surfI);
							if(curCell.X==-1 || curCell.Y==-1)
								//for instance, MGJ on Palatal doesn't exist.
								//also, on probing rows, if there are more exams than will fit.
								continue;
							switch(surfI){
								case (int)PerioSurf.B:
									cellText=PerioMeasures.List[examI,seqI,toothI].Bvalue.ToString();
									break;
								case (int)PerioSurf.DB:
									cellText=PerioMeasures.List[examI,seqI,toothI].DBvalue.ToString();
									break;
								case (int)PerioSurf.DL:
									cellText=PerioMeasures.List[examI,seqI,toothI].DLvalue.ToString();
									break;
								case (int)PerioSurf.L:
									cellText=PerioMeasures.List[examI,seqI,toothI].Lvalue.ToString();
									break;
								case (int)PerioSurf.MB:
									cellText=PerioMeasures.List[examI,seqI,toothI].MBvalue.ToString();
									break;
								case (int)PerioSurf.ML:
									cellText=PerioMeasures.List[examI,seqI,toothI].MLvalue.ToString();
									break;
							}//switch surfI
							if(cellText=="-1")
								cellText="";
							if(examI==selectedExam)
								DataArray[curCell.X,curCell.Y].Text=cellText;
							else
								DataArray[curCell.X,curCell.Y].OldText=cellText;
							//calculate CAL. All ging will have been done before any probing.
							if(seqI==(int)PerioSequenceType.Probing){
								CalculateCAL(curCell,false);
							}
						}//for surfI
					}//for toothI
				}//for seqI
			}//for examI
			CurCell=new Point(1,GetTableRow(selectedExam,0,PerioSequenceType.Probing));
		}

		///<summary>Used in LoadData.</summary>
		private void FillDates(){
			int tableRow;
			for(int examI=0;examI<selectedExam+1;examI++){//-ProbingOffset;examI++){
				for(int section=0;section<4;section++){
					tableRow=GetTableRow(examI,section,PerioSequenceType.Probing);
					if(tableRow==-1)
						continue;
					DataArray[0,tableRow].Text
						=PerioExams.List[examI].ExamDate.ToShortDateString();
						//=PerioExams.List[examI+ProbingOffset].ExamDate.ToShortDateString();
				}
			}
		}

		///<summary>Used in LoadData.</summary>
		private Point GetCell(int examIndex,PerioSequenceType seqType,int intTooth,PerioSurf surf){
			int col=0;
			int row=0;
			if(intTooth<=16){
				col=(intTooth*3)-2;//left-most column
				if(surf==PerioSurf.B || surf==PerioSurf.L){
					col++;
				}
				if(intTooth<=8){
					if(surf==PerioSurf.MB || surf==PerioSurf.ML)
						col+=2;
				}
				else{//9-16
					if(surf==PerioSurf.DB || surf==PerioSurf.DL)
						col+=2;
				}
			}
			else{//17-32
				col=((33-intTooth)*3)-2;//left-most column
				if(surf==PerioSurf.B || surf==PerioSurf.L){
					col++;
				}
				if(intTooth>=25){
					if(surf==PerioSurf.MB || surf==PerioSurf.ML)
						col+=2;
				}
				else{//17-24
					if(surf==PerioSurf.DB || surf==PerioSurf.DL)
						col+=2;
				}
			}
			int section;
			if(intTooth<=16){
				if(surf==PerioSurf.MB || surf==PerioSurf.B || surf==PerioSurf.DB){
					section=0;
				}
				else{//Lingual
					section=1;
				}
			}
			else{//17-32
				if(surf==PerioSurf.MB || surf==PerioSurf.B || surf==PerioSurf.DB){
					section=3;
				}
				else{//Lingual
					section=2;
				}
			}
			row=GetTableRow(examIndex,section,seqType);
			return new Point(col,row);
		}

		///<summary>Saves the cur exam measurements to the db by looping through each tooth and deciding whether the data for that tooth has changed.  If so, it either updates or inserts a measurement.  It won't delete a measurement if all values for that tooth are cleared, but just sets that measurement to all -1's.</summary>
		public void SaveCurExam(int perioExamNum){
			PerioMeasure PerioMeasureCur;
			for(int seqI=0;seqI<PerioMeasures.List.GetLength(1);seqI++){
				for(int toothI=1;toothI<PerioMeasures.List.GetLength(2);toothI++){
					if(
						//if bleeding, then check the probing row for change
						(seqI==(int)PerioSequenceType.Bleeding 
						&& HasChanged[(int)PerioSequenceType.Probing,toothI])
						//or, for any other type, check the corresponding row
						|| HasChanged[seqI,toothI])
					{
						//new measurement
						if(PerioMeasures.List[selectedExam,seqI,toothI]==null){//.PerioMeasureNum==0){
							//MessageBox.Show(toothI.ToString());
							PerioMeasureCur=new PerioMeasure();
							PerioMeasureCur.PerioExamNum=perioExamNum;
							PerioMeasureCur.SequenceType=(PerioSequenceType)seqI;
							PerioMeasureCur.IntTooth=toothI;
						}
						else{
							PerioMeasureCur=PerioMeasures.List[selectedExam,seqI,toothI];
							//PerioExam
							//Sequence
							//tooth
						}
						if(seqI==(int)PerioSequenceType.Mobility || seqI==(int)PerioSequenceType.SkipTooth){
							PerioMeasureCur.MBvalue=-1;
							PerioMeasureCur.Bvalue=-1;
							PerioMeasureCur.DBvalue=-1;
							PerioMeasureCur.MLvalue=-1;
							PerioMeasureCur.Lvalue=-1;
							PerioMeasureCur.DLvalue=-1;
							if(seqI==(int)PerioSequenceType.Mobility){
								PerioMeasureCur.ToothValue
									=GetCellValue(selectedExam,(PerioSequenceType)seqI,toothI,PerioSurf.B);
							}
							else{//skiptooth
								//skipped teeth are only saved when user marks them, not as part of regular saving.
							}
						}
						else if(seqI==(int)PerioSequenceType.Bleeding){
							PerioMeasureCur.ToothValue=-1;
							PerioMeasureCur.MBvalue
								=GetCellBleedValue(selectedExam,toothI,PerioSurf.MB);
							PerioMeasureCur.Bvalue
								=GetCellBleedValue(selectedExam,toothI,PerioSurf.B);
							PerioMeasureCur.DBvalue
								=GetCellBleedValue(selectedExam,toothI,PerioSurf.DB);
							PerioMeasureCur.MLvalue
								=GetCellBleedValue(selectedExam,toothI,PerioSurf.ML);
							PerioMeasureCur.Lvalue
								=GetCellBleedValue(selectedExam,toothI,PerioSurf.L);
							PerioMeasureCur.DLvalue
								=GetCellBleedValue(selectedExam,toothI,PerioSurf.DL);
						}
						else{
							PerioMeasureCur.ToothValue=-1;
							PerioMeasureCur.MBvalue
								=GetCellValue(selectedExam,(PerioSequenceType)seqI,toothI,PerioSurf.MB);
							PerioMeasureCur.Bvalue
								=GetCellValue(selectedExam,(PerioSequenceType)seqI,toothI,PerioSurf.B);
							PerioMeasureCur.DBvalue
								=GetCellValue(selectedExam,(PerioSequenceType)seqI,toothI,PerioSurf.DB);
							PerioMeasureCur.MLvalue
								=GetCellValue(selectedExam,(PerioSequenceType)seqI,toothI,PerioSurf.ML);
							PerioMeasureCur.Lvalue
								=GetCellValue(selectedExam,(PerioSequenceType)seqI,toothI,PerioSurf.L);
							PerioMeasureCur.DLvalue
								=GetCellValue(selectedExam,(PerioSequenceType)seqI,toothI,PerioSurf.DL);
						}
						//then to the database
						if(PerioMeasures.List[selectedExam,seqI,toothI]==null){
							PerioMeasures.Insert(PerioMeasureCur);
						}
						else{
							PerioMeasures.Update(PerioMeasureCur);
						}
					}//if haschanged
				}//for toothI
			}//for seqI
		}

		///<summary>Used in SaveCurExam to retrieve data from grid to save it.</summary>
		private int GetCellValue(int examIndex,PerioSequenceType seqType,int intTooth,PerioSurf surf){
			Point curCell=GetCell(examIndex,seqType,intTooth,surf);
			if(curCell.X==-1 || curCell.Y==-1){
				return -1;
			}
			//if(intTooth==4)
			//MessageBox.Show(DataArray[curCell.X,curCell.Y].Text);
			if(DataArray[curCell.X,curCell.Y].Text==null || DataArray[curCell.X,curCell.Y].Text==""){
				//MessageBox.Show("empty");
				return -1;
			}
			//MessageBox.Show("full");
			return PIn.PInt(DataArray[curCell.X,curCell.Y].Text);
		}

		///<summary>Used in SaveCurExam to retrieve data from grid to save it.</summary>
		private int GetCellBleedValue(int examIndex,int intTooth,PerioSurf surf){
			Point curCell=GetCell(examIndex,PerioSequenceType.Probing,intTooth,surf);
			if(curCell.X==-1 || curCell.Y==-1){
				return 0;
			}
			return (int)DataArray[curCell.X,curCell.Y].Bleeding;
		}

		private void ClearDataArray(){
			//MessageBox.Show("clearing");
			DataArray=new PerioCell[49,RowTypes[0].Length+RowTypes[1].Length
				+RowTypes[2].Length+RowTypes[3].Length+2];//the 2 is for the tooth cells.
			//int curX=0;
			int curY=0;
			for(int sect=0;sect<4;sect++){
				for(int i=0;i<RowTypes[sect].Length;i++){
					curY=GetTableRow(sect,i);
					switch(RowTypes[sect][i]){
						case PerioSequenceType.Mobility:
							DataArray[0,curY].Text=Lan.g(this,"Mobility");
							break;
						case PerioSequenceType.Furcation:
							DataArray[0,curY].Text=Lan.g(this,"Furc");
							break;
						case PerioSequenceType.CAL:
							DataArray[0,curY].Text=Lan.g(this,"auto CAL");
							break;
						case PerioSequenceType.GingMargin:
							DataArray[0,curY].Text=Lan.g(this,"Ging Marg");
							break;
						case PerioSequenceType.MGJ:
							DataArray[0,curY].Text=Lan.g(this,"MGJ");
							break;
						case PerioSequenceType.Probing:
							break;
						default:
							MessageBox.Show("Error in FillDataArray");
							break;
					}
				}
			}
			//draw tooth numbers
			curY=GetTableRow(true);
			try{
				for(int i=1;i<=16;i++){
					DataArray[3*i-1,curY].Text=Tooth.ToInternat(i.ToString());
				}
				curY=GetTableRow(false);
				for(int i=1;i<=16;i++){
					DataArray[3*i-1,curY].Text=Tooth.ToInternat((33-i).ToString());
				}
			}
			catch{
				//won't work in design mode
			}
		}

		///<summary>Used in GetCell during LoadData. Also used in AdvanceCell when looping to a new section.</summary>
		private int GetTableRow(int examIndex,int section,PerioSequenceType seqType){
			if(seqType==PerioSequenceType.Probing || seqType==PerioSequenceType.Bleeding){
				if(examIndex-ProbingOffset<0)//older exam that won't fit.
					return -1;
				int sectionRow=examIndex-ProbingOffset//correct for offset
					+RowTypes[section].Length-RowsProbing;//plus number of non-probing rows
				return GetTableRow(section,sectionRow);
			}
			//for types other than probing and bleeding, do a loop through the non-probing rows:
			for(int i=0;i<RowTypes[section].Length-RowsProbing;i++){
				if(RowTypes[section][i]==seqType)
					return GetTableRow(section,i);
			}
			//MessageBox.Show("Error in GetTableRows: seqType not found");
			return -1;
		}

		private int GetTableRow(int section,int sectionRow){
			int retVal=0;
			if(section==0){
				retVal=RowTypes[0].Length-sectionRow-1;
			}
			else if(section==1){
				retVal=RowTypes[0].Length+1+sectionRow;
			}
			else if(section==2){
				retVal=RowTypes[0].Length+1+RowTypes[1].Length+RowTypes[2].Length-sectionRow-1;
			}
			else
				retVal=RowTypes[0].Length+1+RowTypes[1].Length+RowTypes[2].Length+1+sectionRow;
			return retVal;
		}

		///<summary>If true, then returns the row of the max teeth, otherwise mand.</summary>
		private int GetTableRow(bool getMax){
			if(getMax){
				return RowTypes[0].Length;
			}
			return RowTypes[0].Length+1+RowTypes[1].Length+RowTypes[2].Length;
		}

		
		///<summary>Returns -1 if a tooth row and not a section row. Used in GetBounds, DrawRow, and OnMouseDown.</summary>
		private int GetSection(int tableRow){
			if(tableRow<RowTypes[0].Length){
				return 0;
			}
			if(tableRow==RowTypes[0].Length){
				return -1;//max teeth
			}
			if(tableRow<RowTypes[0].Length+1+RowTypes[1].Length){
				return 1;
			}
			if(tableRow<RowTypes[0].Length+1+RowTypes[1].Length+RowTypes[2].Length){
				return 2;
			}
			if(tableRow==RowTypes[0].Length+1+RowTypes[1].Length+RowTypes[2].Length){
				return -1;//mand teeth
			}
			return 3;
		}

		///<summary>Returns -1 if a tooth row and not a section row.  Used in GetBounds,SetHasChanged, AdvanceCell, and DrawRow</summary>
		private int GetSectionRow(int tableRow){
			if(tableRow<RowTypes[0].Length){
				return RowTypes[0].Length-tableRow-1;
			}
			//return 0;
			if(tableRow==RowTypes[0].Length){
				return -1;//max teeth
			}
			if(tableRow<RowTypes[0].Length+1+RowTypes[1].Length){
				return tableRow-RowTypes[0].Length-1;
			}
			if(tableRow<RowTypes[0].Length+1+RowTypes[1].Length+RowTypes[2].Length){
				return RowTypes[0].Length+1+RowTypes[1].Length+RowTypes[2].Length-tableRow-1;//-1?
			}
			if(tableRow==RowTypes[0].Length+1+RowTypes[1].Length+RowTypes[2].Length){
				return -1;//mand teeth
			}
			return tableRow-RowTypes[0].Length-1-RowTypes[1].Length-RowTypes[2].Length-1;//-1?
		}

		///<summary>Gets the current cell as a col,row based on the x-y pixel coordinate supplied.</summary>
		private Point GetCellFromPixel(int x,int y){
			int row=0;
			for(int i=0;i<TopCoordinates.Length;i++){
				if(y<TopCoordinates[i]){
					row=i-1;
					break;
				}
				if(i==TopCoordinates.Length-1){//last row
					row=i;
				}
			}
			if(y==-1)
				y=0;
			int col=0;
			if(x<=Wleft+1){
				col=0;
			}
			else{
				//1+Wleft+1+(col-1)*(Wmeas+1);
				col=(int)Math.Floor(((double)(x-Wleft-1))/((double)(Wmeas+1)))+1;
			}
			if(col==49)
				col=48;
			return new Point(col,row);
		}

		///<summary></summary>
		protected override void OnMouseDown(MouseEventArgs e){
			base.OnMouseDown(e);
			Point newCell=GetCellFromPixel(e.X,e.Y);
			if(newCell.X==0){
				return;//Left column only for dates
			}
			int section=GetSection(newCell.Y);
			if(section==-1){//clicked on a toothNum
				int intTooth=(int)Math.Ceiling((double)newCell.X/3);
				if(GetTableRow(false)==newCell.Y){//if clicked on mand
					intTooth=33-intTooth;
				}
				if(selectedTeeth.Contains(intTooth)){//tooth was already selected
					selectedTeeth.Remove(intTooth);
				}
				else{//tooth was not selected
					selectedTeeth.Add(intTooth);
				}
				Invalidate();//incomplete: just invalidate the area around the tooth num.
				return;
			}
			if(selectedTeeth.Count>0){//if not clicked on a toothnum, but teeth were selected,
				//then deselect all.
				//todo(some day): loop through each individually and only invalidate small area.
				selectedTeeth=new ArrayList();
				Invalidate();
			}
			int sectRow=GetSectionRow(newCell.Y);
			if(RowTypes[section][sectRow]==PerioSequenceType.Probing){
				if(this.selectedExam-ProbingOffset//correct for offset
					+RowTypes[section].Length-RowsProbing//plus non-probing rows
					!=sectRow)
				{
					return;//not allowed to click on probing rows other than selectedRow
				}
			}
			else if(RowTypes[section][sectRow]==PerioSequenceType.Mobility){
				if(Math.IEEERemainder(((double)newCell.X+1),3) != 0){//{2,5,8,11};examples of acceptable cols
					return;//for mobility, not allowed to click on anything but B
				}
			}
			else if(RowTypes[section][sectRow]==PerioSequenceType.CAL){
				return;//never allowed to edit CAL
			}
			if(section==0)
				OnDirectionChangedLeft();
			else if(section==1)
				OnDirectionChangedRight();
			else if(section==2)
				OnDirectionChangedRight();
			else if(section==3)
				OnDirectionChangedLeft();
			SetNewCell(newCell.X,newCell.Y);
			Focus();
		}

		///<summary></summary>
		protected override bool IsInputKey(Keys keyData) {
			if(keyData==Keys.Left
				|| keyData==Keys.Right
				|| keyData==Keys.Up
				|| keyData==Keys.Down)
				return true;
			return base.IsInputKey(keyData);
		}

		///<summary></summary>
		protected override void OnKeyDown(KeyEventArgs e) {
			if(selectedExam==-1){
				MessageBox.Show(Lan.g(this,"Please add or select an exam first in the list to the left."));
				return;
			}
			//MessageBox.Show("key down");
			//e.Handled=true;
			//base.OnKeyDown (e);
			if(e.KeyValue>=96 && e.KeyValue<=105){//keypad 0 through 9
				if(e.Control){
					ButtonPressed(e.KeyValue-96+10);
				}
				else{
					ButtonPressed(e.KeyValue-96);
				}
			}
			else if(e.KeyValue>=48 && e.KeyValue<=57){//0 through 9
				if(e.Control){
					ButtonPressed(e.KeyValue-48+10);
				}
				else{
					ButtonPressed(e.KeyValue-48);
				}
			}
			else if(e.KeyCode==Keys.B){
				ButtonPressed("b");
			}
			else if(e.KeyCode==Keys.Space){
				ButtonPressed("b");
			}
			else if(e.KeyCode==Keys.S){
				ButtonPressed("s");
			}
			else if(e.KeyCode==Keys.P){
				ButtonPressed("p");
			}
			else if(e.KeyCode==Keys.C){
				ButtonPressed("c");
			}
			else if(e.KeyCode==Keys.Back){
				if(ThreeAtATime){
					for(int i=0;i<3;i++){
						AdvanceCell(true);
						ClearValue();
					}
				}
				else{
					AdvanceCell(true);
					ClearValue();
				}
			}
			else if(e.KeyCode==Keys.Delete){
				ClearValue();
			}
			else if(e.KeyCode==Keys.Left){
				if(ThreeAtATime){
					for(int i=0;i<3;i++){
						if(DirectionIsRight)
							AdvanceCell();
						else
							AdvanceCell(true);
					}
				}
				else{
					if(DirectionIsRight)
						AdvanceCell();
					else
						AdvanceCell(true);
				}
			}
			else if(e.KeyCode==Keys.Right){
				if(ThreeAtATime){
					for(int i=0;i<3;i++){
						if(DirectionIsRight)
							AdvanceCell(true);
						else
							AdvanceCell();
					}
				}
				else{
					if(DirectionIsRight)
						AdvanceCell(true);
					else
						AdvanceCell();
				}
			}
			//else{
			//	return;
			//}
		}
 
		///<summary>Accepts button clicks from window rather than the usual keyboard entry.  All validation MUST be done before the value is sent here.  Only valid values are b,s,p,or c. Numbers entered using overload.</summary>
		public void ButtonPressed(string keyValue){
			if(ThreeAtATime){
				for(int i=0;i<3;i++)
					EnterValue(keyValue);
			}
			else
				EnterValue(keyValue);
		}

		///<summary>Accepts button clicks from window rather than the usual keyboard entry.  All validation MUST be done before the value is sent here.  Only valid values are numbers 0 through 19.</summary>
		public void ButtonPressed(int keyValue){
			if(ThreeAtATime){
				for(int i=0;i<3;i++)
					EnterValue(keyValue);
			}
			else
				EnterValue(keyValue);
		}

		///<summary>Only valid values are b,s,p, and c.</summary>
		private void EnterValue(string keyValue){
			if(keyValue !="b" && keyValue !="s" && keyValue !="p" && keyValue !="c"){
				MessageBox.Show("Only b,s,p, and c are allowed");//just for debugging
				return;
			}
			PerioCell cur=DataArray[CurCell.X,CurCell.Y];
			bool curCellHasText=false;
			if(ThreeAtATime){
				//don't backup
			}
			else if(cur.Text!=null && cur.Text!=""){
				curCellHasText=true;
				//so enter value for current cell
			}
			else{
				curCellHasText=false;
				AdvanceCell(true);//so backup
				cur=DataArray[CurCell.X,CurCell.Y];
				//enter value, then advance.
			}
			if(keyValue=="b"){
				if((cur.Bleeding & BleedingFlags.Blood)==0)//if it was off
					cur.Bleeding=cur.Bleeding | BleedingFlags.Blood;//turn it on
				else//if it was on
					cur.Bleeding=cur.Bleeding & ~BleedingFlags.Blood; //turn it off
			}
			if(keyValue=="s"){
				if((cur.Bleeding & BleedingFlags.Suppuration)==0)
					cur.Bleeding=cur.Bleeding | BleedingFlags.Suppuration;
				else
					cur.Bleeding=cur.Bleeding & ~BleedingFlags.Suppuration;
			}
			if(keyValue=="p"){
				if((cur.Bleeding & BleedingFlags.Plaque)==0)
					cur.Bleeding=cur.Bleeding | BleedingFlags.Plaque;
				else
					cur.Bleeding=cur.Bleeding & ~BleedingFlags.Plaque;
			}
			if(keyValue=="c"){
				if((cur.Bleeding & BleedingFlags.Calculus)==0)
					cur.Bleeding=cur.Bleeding | BleedingFlags.Calculus;
				else
					cur.Bleeding=cur.Bleeding & ~BleedingFlags.Calculus;
			}
			DataArray[CurCell.X,CurCell.Y]=cur;
			Invalidate(Rectangle.Ceiling(GetBounds(CurCell.X,CurCell.Y)));
			SetHasChanged(CurCell.X,CurCell.Y);
			if(ThreeAtATime){
				AdvanceCell();
			}
			else if(!curCellHasText){
				AdvanceCell();//to return to original location
			}
		}

		

		///<summary>Only valid values are numbers 0 through 19. Validation should be done before sending here.</summary>
		private void EnterValue(int keyValue){
			if(keyValue < 0 || keyValue > 19){
				MessageBox.Show("Only values 0 through 19 allowed");//just for debugging
				return;
			}
			PerioCell cur=DataArray[CurCell.X,CurCell.Y];
			//might be able to eliminate these two lines
			cur.Text=keyValue.ToString();
			DataArray[CurCell.X,CurCell.Y]=cur;
			Invalidate(Rectangle.Ceiling(GetBounds(CurCell.X,CurCell.Y)));
			SetHasChanged(CurCell.X,CurCell.Y);
			if(RowTypes[GetSection(CurCell.Y)][GetSectionRow(CurCell.Y)]==PerioSequenceType.Probing){ 
				CalculateCAL(CurCell,true);
			}
			else if(RowTypes[GetSection(CurCell.Y)][GetSectionRow(CurCell.Y)]==PerioSequenceType.GingMargin){
				CalculateCAL(new Point(CurCell.X,GetTableRow
					(selectedExam,GetSection(CurCell.Y),PerioSequenceType.Probing)),true);
			}
			AdvanceCell();
		}

		private void CalculateCAL(Point probingCell,bool alsoInvalidate){
			Point calLoc=new Point(probingCell.X,GetTableRow
				(selectedExam,GetSection(probingCell.Y),PerioSequenceType.CAL));
			Point gingLoc=new Point(probingCell.X,GetTableRow
				(selectedExam,GetSection(probingCell.Y),PerioSequenceType.GingMargin));
			//PerioCell calCell=DataArray[calLoc.X,calLoc.Y];
			if(DataArray[probingCell.X,probingCell.Y].Text==null 
				|| DataArray[probingCell.X,probingCell.Y].Text==""
				|| DataArray[gingLoc.X,gingLoc.Y].Text==null 
				|| DataArray[gingLoc.X,gingLoc.Y].Text=="")
			{
				DataArray[calLoc.X,calLoc.Y].Text="";
				if(alsoInvalidate){
					Invalidate(Rectangle.Ceiling(GetBounds(calLoc.X,calLoc.Y)));
				}
				return;
			}
			int probValue=PIn.PInt(DataArray[probingCell.X,probingCell.Y].Text);
			int gingValue=PIn.PInt(DataArray[gingLoc.X,gingLoc.Y].Text);
			DataArray[calLoc.X,calLoc.Y].Text=(gingValue+probValue).ToString();
			if(alsoInvalidate){
				Invalidate(Rectangle.Ceiling(GetBounds(calLoc.X,calLoc.Y)));
			}
		}

		private void SetHasChanged(int col,int row){
			int section=GetSection(row);
			int intTooth=(int)Math.Ceiling((double)col/3);//1-16
			if(section==2 || section==3){
				intTooth=33-intTooth;
			}
			int seqI=(int)RowTypes[section][GetSectionRow(row)];
			HasChanged[seqI,intTooth]=true;
			//MessageBox.Show(intTooth.ToString());
		}

		///<summary>Used in OnMouseDown to change the currently selected cell.</summary>
		private void SetNewCell(int x,int y){
			//MessageBox.Show(x.ToString()+","+y.ToString());
			RectangleF oldRect=new Rectangle(0,0,0,0);
			bool invalidateOld=false;
			if(CurCell.X!=-1){
				oldRect=GetBounds(CurCell.X,CurCell.Y);
				invalidateOld=true;
			}
			CurCell=new Point(x,y);
			if(invalidateOld){
				Invalidate(Rectangle.Ceiling(oldRect));
			}
			Invalidate(Rectangle.Ceiling(GetBounds(CurCell.X,CurCell.Y)));
		}

		private void AdvanceCell(bool isReverse){
			PerioSequenceType seqType=RowTypes[GetSection(CurCell.Y)][GetSectionRow(CurCell.Y)];
			int newRow=0;//used when jumping between sections
			int intTooth=1;//used to test skipped teeth
			int section=0;//used to test skipped teeth
			int newSection=0;
			bool locIsValid=false;//used when testing for skipped tooth and mobility location
			bool startedOnSkipped=false;//special situation:
			intTooth=(int)Math.Ceiling((double)CurCell.X/3);
			section=GetSection(CurCell.Y);
			newSection=section;//in case it doesn't change
			if(section==2 || section==3){//if on mand
				intTooth=33-intTooth;
			}
			if(skippedTeeth.Contains(intTooth)){
				startedOnSkipped=true;
			}
			int limit=0;
			while(limit<400){//the 400 limit is just a last resort. Should always break manually.
				limit++;
				//to the right
				section=GetSection(CurCell.Y);
				if((!DirectionIsRight && isReverse) || (DirectionIsRight && !isReverse)){
					if(CurCell.X==1){//if first column
						if(section==0){//usually in reverse
							return;//no jump.  This is the starting point.
						}
						else if(section==1){
							newSection=3;
							newRow=GetTableRow(selectedExam,newSection,seqType);
							OnDirectionChangedLeft();
						}
						else if(section==2){
							return;//no jump.  End of all sequences.
						}
						else if(section==3){//usually in reverse
							newSection=1;
							newRow=GetTableRow(selectedExam,newSection,seqType);	
							if(newRow!=-1)
								OnDirectionChangedRight();
						}
						if(newRow==-1){//MGJ and mobility aren't present in all 4 sections, so can't loop normally
							if(RowTypes[section][GetSectionRow(CurCell.Y)]==PerioSequenceType.Mobility){
								if(section==3){//usually in reverse
									newSection=0;
									SetNewCell(1+16*3,GetTableRow(selectedExam,newSection,PerioSequenceType.Mobility));
								}
							}
							else if(RowTypes[section][GetSectionRow(CurCell.Y)]==PerioSequenceType.MGJ){
								//section 3. in reverse
								newSection=0;
								SetNewCell(16*3,GetTableRow(selectedExam,newSection,PerioSequenceType.MGJ));
							}
						}
						else{
							SetNewCell(CurCell.X,newRow);
						}
					}
					else{//standard advance with no jumping
						SetNewCell(CurCell.X-1,CurCell.Y);
					}
				}
				//to the left
				else{//((DirectionIsRight && isBackspace) || !DirectionIsRight){
					if(CurCell.X==DataArray.GetLength(0)-1){//if last column
						if(section==0){
							newSection=1;
							newRow=GetTableRow(selectedExam,newSection,seqType);
							if(newRow!=-1)
								OnDirectionChangedRight();
						}
						else if(section==1){//usually in reverse
							newSection=0;
							newRow=GetTableRow(selectedExam,newSection,seqType);
							OnDirectionChangedLeft();
						}
						else if(section==2){//usually in reverse
							newSection=3;
							newRow=GetTableRow(selectedExam,newSection,seqType);
							OnDirectionChangedLeft();
						}
						else if(section==3){
							newSection=2;
							newRow=GetTableRow(selectedExam,newSection,seqType);
							if(newRow!=-1)
								OnDirectionChangedRight();
						}
						if(newRow==-1){//MGJ and mobility aren't present in all 4 sections, so can't loop normally
							if(RowTypes[section][GetSectionRow(CurCell.Y)]==PerioSequenceType.Mobility){
								if(section==0){
									newSection=3;
									SetNewCell(1,GetTableRow(selectedExam,newSection,PerioSequenceType.Mobility));
								}
								if(section==3){
									return;//end of sequence
								}
							}
							else if(RowTypes[section][GetSectionRow(CurCell.Y)]==PerioSequenceType.MGJ){
								//section 0
								newSection=3;
								SetNewCell(1,GetTableRow(selectedExam,newSection,PerioSequenceType.MGJ));
							}
						}
						else{
							SetNewCell(CurCell.X,newRow);
						}
					}
					else{//standard advance with no jumping
						SetNewCell(CurCell.X+1,CurCell.Y);
					}
				}
				if(startedOnSkipped)//since we started on a skipped tooth
					return;//we can continue entry on a skipped tooth.
				intTooth=(int)Math.Ceiling((double)CurCell.X/3);
				if(newSection==2 || newSection==3){//if on mand
					intTooth=33-intTooth;
				}
				locIsValid=true;
				if(skippedTeeth.Contains(intTooth)){//if we are on a skipped tooth
					locIsValid=false;
				}
				//MessageBox.Show(GetSectionRow(CurCell.Y).ToString());
				if(RowTypes[newSection][GetSectionRow(CurCell.Y)]==PerioSequenceType.Mobility){
					if(Math.IEEERemainder(((double)CurCell.X+1),3) != 0){//{2,5,8,11};examples of acceptable cols
						locIsValid=false;//for mobility, not allowed to click on anything but B
					}
				}
				if(locIsValid){
					return;
				}
				//otherwise, continue to loop in search of a valid loc
			}//while
		}

		private void AdvanceCell(){
			AdvanceCell(false);
		}

		private void ClearValue(){
			//MessageBox.Show(DataArray.GetLength(0).ToString());
			//MessageBox.Show(DataArray.GetLength(1).ToString());
			PerioCell cur=DataArray[CurCell.X,CurCell.Y];
			cur.Text="";
			DataArray[CurCell.X,CurCell.Y]=cur;
			SetHasChanged(CurCell.X,CurCell.Y);
			Invalidate(Rectangle.Ceiling(GetBounds(CurCell.X,CurCell.Y)));
			if(RowTypes[GetSection(CurCell.Y)][GetSectionRow(CurCell.Y)]==PerioSequenceType.Probing){ 
				CalculateCAL(CurCell,true);
			}
			else if(RowTypes[GetSection(CurCell.Y)][GetSectionRow(CurCell.Y)]==PerioSequenceType.GingMargin){
				CalculateCAL(new Point(CurCell.X,GetTableRow
					(selectedExam,GetSection(CurCell.Y),PerioSequenceType.Probing)),true);
			}
		}

		///<summary></summary>
		public void ToggleSkip(int perioExamNum){
			if(selectedTeeth.Count==0){
				MessageBox.Show(Lan.g(this,"Please select teeth first."));
				return;
			}
			for(int i=0;i<selectedTeeth.Count;i++){
				if(skippedTeeth.Contains(selectedTeeth[i])){//if the tooth was already marked skipped
					skippedTeeth.Remove(selectedTeeth[i]);
				}
				else{//tooth was not already marked skipped
					skippedTeeth.Add(selectedTeeth[i]);
				}
			}
			PerioMeasures.SetSkipped(perioExamNum,skippedTeeth);
			selectedTeeth=new ArrayList();
			Invalidate();
		}

		///<summary></summary>
		protected void OnDirectionChangedRight(){
			if(DirectionChangedRight != null){
				DirectionIsRight=true;
				EventArgs eArgs=new EventArgs();
				DirectionChangedRight(this,eArgs);
			}
		}

		///<summary></summary>
		protected void OnDirectionChangedLeft(){
			if(DirectionChangedLeft != null){
				DirectionIsRight=false;
				EventArgs eArgs=new EventArgs();
				DirectionChangedLeft(this,eArgs);
			}
		}

		///<summary></summary>
		public string ComputeIndex(BleedingFlags bleedFlag){
			if(this.selectedExam==-1){
				return "";
			}
			int counter=0;
			for(int section=0;section<4;section++){
				for(int x=1;x<1+3*16;x++){
					if((DataArray[x,GetTableRow(selectedExam,section,PerioSequenceType.Probing)].Bleeding 
						& bleedFlag)>0)
					{
						counter++;
					}
				}
			}
			return (100*counter/((32-skippedTeeth.Count)*6)).ToString("F0");
		}

		///<summary>Returns a list of strings, each between "1" and "32" (or similar international #'s), representing the teeth with red values based on prefs.  The result can be used to print summary, or can be counted to show # of teeth.</summary>
		public ArrayList CountTeeth(PerioSequenceType seqType){
			if(selectedExam==-1){
				return new ArrayList();
			}
			int prefVal=0;
			switch(seqType){
				case PerioSequenceType.Probing:
					prefVal=PrefB.GetInt("PerioRedProb");
					break;
				case PerioSequenceType.MGJ:
					prefVal=PrefB.GetInt("PerioRedMGJ");
					break;
				case PerioSequenceType.GingMargin:
					prefVal=PrefB.GetInt("PerioRedGing");
					break;
				case PerioSequenceType.CAL:
					prefVal=PrefB.GetInt("PerioRedCAL");
					break;
				case PerioSequenceType.Furcation:
					prefVal=PrefB.GetInt("PerioRedFurc");
					break;
				case PerioSequenceType.Mobility:
					prefVal=PrefB.GetInt("PerioRedMob");
					break;
			}
			ArrayList retList=new ArrayList();
			string cellText="";
			int intTooth=0;
			int row=0;
			for(int section=0;section<4;section++){
				for(int x=1;x<1+3*16;x++){
					row=GetTableRow(selectedExam,section,seqType);
					if(row==-1)
						continue;//eg MGJ or Mobility
					cellText=DataArray[x,row].Text;
					if(cellText==null || cellText==""){
						continue;
					}
					if((seqType==PerioSequenceType.MGJ && PIn.PInt(cellText)<=prefVal)
						|| (seqType!=PerioSequenceType.MGJ && PIn.PInt(cellText)>=prefVal)){
						intTooth=(int)Math.Ceiling((double)x/3);
						if(section==2 || section==3){//if mand
							intTooth=33-intTooth;
						}
						if(!retList.Contains(Tooth.ToInternat(intTooth.ToString()))){
							retList.Add(Tooth.ToInternat(intTooth.ToString()));
						}
					}
				}
			}
			return retList;
		}

		///<summary>Uses this control to draw onto the specified graphics object (the printer).</summary>
		public void DrawChart(Graphics g) {
			PaintEventArgs e=new PaintEventArgs(g,ClientRectangle);
			DrawBackground(e);
			DrawSkippedTeeth(e);
			//DrawSelectedTeeth(e);
			//DrawCurCell(e);
			DrawGridlines(e);
			DrawText(e);
		}

		


		






	}

	

	///<summary>Blood,pus,plaque,and calculus. Used in ContrPerio.PerioCell</summary>
	[Flags]
	public enum BleedingFlags{
		///<summary>0</summary>
		None=0,
		///<summary>1</summary>
		Blood=1,
		///<summary>2</summary>
		Suppuration=2,
		///<summary>4</summary>
		Plaque=4,
		///<summary>8</summary>
		Calculus=8
	}

	///<summary></summary>
	public struct PerioCell{
		///<summary>The value to display for this exam. Overwrites any oldText from previous exams.</summary>
		public string Text;
		///<summary>None, blood, pus, or both</summary>
		public BleedingFlags Bleeding;
		///<summary>Values from previous exams. Slightly greyed out.</summary>
		public string OldText;
		//<summary></summary>
		//public Rectangle Bounds;
		//<summary></summary>
		//public Color BackColor;
		//<summary></summary>
		//public Color ForeColor;
	}

	///<summary>Currently, only six surfaces are supported, but more can always be added.</summary>
	public enum PerioSurf{
		///<summary>Might be used for things such as mobility or missing tooth.</summary>
		None,
		///<summary>1</summary>
		MB,
		///<summary>2</summary>
		B,
		///<summary>3</summary>
		DB,
		///<summary>4</summary>
		ML,
		///<summary>5</summary>
		L,
		///<summary>6</summary>
		DL
	}


}
















