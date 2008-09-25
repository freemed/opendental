using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Drawing.Text;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental {

	///<summary></summary>
	public class ContrCalendar:System.Windows.Forms.UserControl {
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butPrevious;
		private OpenDental.UI.Button butNext;
		private int RowCount;
		private int ColCount;
		private int RowHeight;
		private int ColWidth;
		private int HeaderHeight;
		//private float FixedWidth;
		private int DayHeadHeight;
		//private Rectangle RecBG;
		private Rectangle RecHead;
		private Rectangle RecDayHead;
		private Rectangle RecFoot;
		//private Rectangle RecBG2;
		private Color colorBG;
		private Color HeadColor;
		private Color DayHeadColor;
		private Color FootColor;
		private Color TextColor;
		private Color DayOpenColor;
		//private Color SelectedDayColor;
		private Pen LinePen;
		private Font FontText;
		private Font FontText2;
		private Font FontHeader;
		private string Header;
		private string Footer;
		private int DaysInMonth;
		private DateTime selectedDate;
		///<summary></summary>
		public List<OneCalendarDay> ListDays;
		///<summary></summary>
		public int SelectedDay;
		///<summary>Was called CurrentDay</summary>
		public OneCalendarDay Today;
		///<summary></summary>
		public int MaxRowsText;
		//private int count=0;

		///<summary></summary>
		public ContrCalendar() {
			InitializeComponent();
			RowCount=6;
			ColCount=7;
			HeaderHeight=25;
			//FixedWidth=25;
			//BGColor=SystemColors.Window;
			colorBG=SystemColors.Control;
			HeadColor=SystemColors.Control;
			FootColor=SystemColors.Control;
			DayHeadColor=SystemColors.Window;
			TextColor=Color.Black;//SystemColors.ControlText;
			DayOpenColor=Color.White;//SystemColors.Window; Was ActiveDayColor
			//SelectedDayColor=Color.White;//SystemColors.Highlight;  
			LinePen=new Pen(Color.SlateGray,2);
			FontText=new Font("Microsoft Sans Serif",8,FontStyle.Bold);
			FontText2=new Font("Microsoft Sans Serif",8,FontStyle.Regular);
			DayHeadHeight=(int)FontText.GetHeight()+6;
			FontHeader = new Font("Microsoft Sans Serif",9,FontStyle.Bold);
			MaxRowsText=5;
			ListDays=new List<OneCalendarDay>();
			//for(int i=0;i<List.Length;i++){
			//	List[i]=new OneCalendarDay();
			//  List[i].RowsOfText=new string[MaxRowsText];
			//}   
		}

		///<summary></summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContrCalendar));
			this.butPrevious = new OpenDental.UI.Button();
			this.butNext = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butPrevious
			// 
			this.butPrevious.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrevious.Autosize = true;
			this.butPrevious.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrevious.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrevious.CornerRadius = 4F;
			this.butPrevious.Image = ((System.Drawing.Image)(resources.GetObject("butPrevious.Image")));
			this.butPrevious.Location = new System.Drawing.Point(32,34);
			this.butPrevious.Name = "butPrevious";
			this.butPrevious.Size = new System.Drawing.Size(22,22);
			this.butPrevious.TabIndex = 0;
			this.butPrevious.Click += new System.EventHandler(this.butPrevious_Click);
			// 
			// butNext
			// 
			this.butNext.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNext.Autosize = true;
			this.butNext.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNext.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNext.CornerRadius = 4F;
			this.butNext.Image = ((System.Drawing.Image)(resources.GetObject("butNext.Image")));
			this.butNext.Location = new System.Drawing.Point(538,22);
			this.butNext.Name = "butNext";
			this.butNext.Size = new System.Drawing.Size(22,22);
			this.butNext.TabIndex = 1;
			this.butNext.Click += new System.EventHandler(this.butNext_Click);
			// 
			// ContrCalendar
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.butNext);
			this.Controls.Add(this.butPrevious);
			this.DoubleBuffered = true;
			this.Name = "ContrCalendar";
			this.Size = new System.Drawing.Size(600,600);
			this.Load += new System.EventHandler(this.ContrCalendar_Load);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContrCalendar_MouseDown);
			this.Resize += new System.EventHandler(this.ContrCalendar_Resize);
			this.ResumeLayout(false);

		}
		#endregion


		///<summary></summary>
		[Category("AAA Custom"),
			Description("SelectedDate. Keeps track of the date selected")
		]
		public DateTime SelectedDate {
			get {
				return selectedDate;
			}
			set {
				selectedDate = value;
			}
		}

		private void ContrCalendar_Load(object sender,System.EventArgs e) {
			selectedDate=DateTime.Today;
			SelectedDay=SelectedDate.Day;
			//this needs work.  should be a simple invalidate instead:
			//Graphics g=this.CreateGraphics();
			//this.OnPaint(new PaintEventArgs(g,this.ClientRectangle));
			//g.Dispose();
			FillDaysInMonth();
			this.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
		}
		private void ContrCalendar_Paint(object sender,System.Windows.Forms.PaintEventArgs e) {
			//background:
			Graphics g=e.Graphics;
			g.FillRectangle(new SolidBrush(colorBG),new Rectangle(0,0,Width,Height));
			RecHead=new Rectangle(0,0,Width,HeaderHeight);
			RecFoot=new Rectangle(0,Height-HeaderHeight,Width,HeaderHeight);
			RecDayHead=new Rectangle(0,HeaderHeight,Width,DayHeadHeight);
			RowHeight=(Height-(HeaderHeight*2)-DayHeadHeight)/RowCount;
			ColWidth=Width/ColCount;
			g.DrawRectangle(LinePen,new Rectangle(0,0,Width-1,Height-1));
			DrawHeader(g);
			//FillDaysInMonth();
			DrawDays(g);
			DrawDayHeaders(g);
			PositionButtons();  
			DrawFooter(g);
			MarkTodayDate(g);
			g.Dispose();
		}

		private void DrawHeader(Graphics g) {
			g.FillRectangle(new SolidBrush(HeadColor),RecHead);
			g.DrawRectangle(LinePen,RecHead);
			Header=selectedDate.ToString("MMMM, yyyy");
			int xPos=Width/2-(int)(g.MeasureString(Header,FontHeader).Width/2f);
			int yPos=5;
			g.DrawString(Header,FontHeader,Brushes.Black,xPos,yPos);
		}

		private void FillDaysInMonth() {
			int row=0;
			int column=(int)(new DateTime(selectedDate.Year,selectedDate.Month,1).DayOfWeek)-1;
			DaysInMonth=DateTime.DaysInMonth(selectedDate.Year,selectedDate.Month);
			OneCalendarDay oneday;
			for(int i=1;i<=DaysInMonth;i++) {
				oneday=new OneCalendarDay();
				oneday.Bounds=new Rectangle(ColWidth*column,HeaderHeight+DayHeadHeight+RowHeight*row,ColWidth,RowHeight);
				oneday.Date=new DateTime(selectedDate.Year,selectedDate.Month,i);
				if(i==DateTime.Today.Day
					&& selectedDate.Month==DateTime.Today.Month 
					&& selectedDate.Year==DateTime.Today.Year) 
				{
					Today=oneday.Copy();
					Today.Bounds.X+=1;
					Today.Bounds.Y+=1;
					Today.Bounds.Width-=2;
					Today.Bounds.Height-=2;
				}
				if(i==SelectedDay) {
					oneday.IsSelected=true;
				}
				ListDays.Add(oneday);
				if(column==6) {
					row++;
					column=0;
				}
				else {
					column++;
				}
			}
		}

		///<summary></summary>
		public void DrawDays(Graphics g) {
			StringFormat format=new StringFormat();
			format.LineAlignment=StringAlignment.Far;//right
			format.Alignment=StringAlignment.Near;//top
			RectangleF rectF;
			for(int i=0;i<ListDays.Count;i++) {
				//if(i==SelectedDay){
				//	g.FillRectangle(new SolidBrush(SelectedDayColor),List[i].Rec);
				//	g.DrawString(List[i].Day.ToString()
				//		,FontText,Brushes.Black,List[i].xPos,List[i].yPos);
				//}
				//else{
				if(ListDays[i].color.Equals(Color.Empty)) {
					g.FillRectangle(new SolidBrush(DayOpenColor),ListDays[i].Bounds);
				}
				else {
					g.FillRectangle(new SolidBrush(ListDays[i].color),ListDays[i].Bounds);
				}
				rectF=new RectangleF(ListDays[i].Bounds.X,ListDays[i].Bounds.Y,ListDays[i].Bounds.Width,ListDays[i].Bounds.Height);
				g.DrawString(ListDays[i].Date.Day.ToString(),FontText,Brushes.Black,rectF,format);
				//}
				DrawRowsOfText(i,g);
			}
			DrawMonthGrid(g);
			MarkTodayDate(g);
		}

		///<summary>Draws the names of the days of the week</summary>
		private void DrawDayHeaders(Graphics g) {
			g.FillRectangle(new SolidBrush(DayHeadColor),RecDayHead);
			g.DrawRectangle(LinePen,RecDayHead);
			for(int i=0;i<=ColCount;i++) {
				g.DrawLine(LinePen,ColWidth*i,HeaderHeight,ColWidth*i,HeaderHeight+DayHeadHeight);
			}
			string[] daysOfWeek=CultureInfo.CurrentCulture.DateTimeFormat.DayNames;//already translated
			for(int i=0;i<daysOfWeek.Length;i++) {
				int xPos=i*ColWidth+ColWidth/2-(int)(g.MeasureString(daysOfWeek[i],FontText).Width/2f);
				int yPos=HeaderHeight+DayHeadHeight-(int)(FontText.GetHeight()/2f);
				g.DrawString(daysOfWeek[i],FontText,Brushes.Black,xPos,yPos);
			}
		}

		private void PositionButtons() {
			butPrevious.Location=new Point(2,2);
			butNext.Location=new Point(Width-butNext.Size.Width-1,2);
			butPrevious.BackColor=SystemColors.Control;
			butNext.BackColor=SystemColors.Control;
		}

		private void DrawFooter(Graphics g) {
			g.FillRectangle(new SolidBrush(FootColor),RecFoot);
			g.DrawRectangle(LinePen,RecFoot);
			Footer="Today: "+DateTime.Today.ToShortDateString();
			int xPos=Width/2-(int)(g.MeasureString(Footer,FontText).Width/2f);
			int yPos=Height-(int)(FontText.GetHeight()/2f);
			g.DrawString(Lan.g(this,Footer),FontText,Brushes.Black,xPos,yPos);
		}

		private void MarkTodayDate(Graphics g) {
			if(ListDays[SelectedDay].Date.Month==DateTime.Today.Month) {
				g.DrawRectangle(new Pen(Color.Red),Today.Bounds);
			}
		}

		private void DrawRowsOfText(int day,Graphics g) {
			float extra=2;
			float xCoord=(int)(ListDays[day].Bounds.X+extra);
			float yCoord=(float)(ListDays[day].Bounds.Y+(FontText2.GetHeight()*1.5));
			RectangleF r=new RectangleF(xCoord,yCoord,ListDays[day].Bounds.Width-(2*extra)
				,(float)(FontText2.GetHeight()));
			if(ListDays[day].RowsOfText.Count>0) {
				g.DrawString(ListDays[day].RowsOfText[0],FontText2,Brushes.Black,r);
			}
			for(int i=0;i<ListDays[day].RowsOfText.Count;i++) {
				if(ListDays[day].RowsOfText[i]=="") {
				}
				else {
					g.DrawString(ListDays[day].RowsOfText[i],FontText2,Brushes.Black
						,new RectangleF(xCoord,yCoord,ListDays[day].Bounds.Width-(2*extra),FontText2.GetHeight()));
					yCoord+=FontText2.GetHeight();
				}
			}
		}

		private void DrawMonthGrid(Graphics g) {
			//horizontal lines:  
			for(int i=1;i<=RowCount;i++) {
				g.DrawLine(LinePen,0,HeaderHeight+DayHeadHeight+RowHeight*i,Width,HeaderHeight+DayHeadHeight+RowHeight*i);
			}
			//vertical lines
			for(int i=0;i<=ColCount;i++) {
				g.DrawLine(LinePen,ColWidth*i,HeaderHeight,ColWidth*i,Height-HeaderHeight);
			}
		}

		private void ContrCalendar_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			//Graphics g=this.CreateGraphics();
			int OldSelected=SelectedDay;
			for(int i=1;i<=DaysInMonth;i++) {
				if(e.X >= ListDays[i].Bounds.X && e.X <= ListDays[i].Bounds.X+ListDays[i].Bounds.Width 
					&& e.Y >= ListDays[i].Bounds.Y && e.Y <= ListDays[i].Bounds.Y+ListDays[i].Bounds.Height) {
					SelectedDay=ListDays[i].Date.Day;
					selectedDate=ListDays[i].Date;
					ListDays[i].IsSelected=true;
					ListDays[OldSelected].IsSelected=false;
					/*
					//unpainting selected and repainting Windows color
					if(List[OldSelected].color.Equals(Color.Empty)){
						grfx.FillRectangle(new SolidBrush(DayOpenColor),List[OldSelected].Rec);
						grfx.DrawString(List[OldSelected].Day.ToString()
							,FontText,Brushes.Black,List[OldSelected].xPos,List[OldSelected].yPos);
						DrawRowsOfText(OldSelected,grfx); 
					}
					else{
						grfx.FillRectangle(new SolidBrush(List[OldSelected].color),List[OldSelected].Rec);
						grfx.DrawString(List[OldSelected].Day.ToString()
							,FontText,Brushes.Black,List[OldSelected].xPos,List[OldSelected].yPos);
						DrawRowsOfText(OldSelected,grfx);            
					}
					//painting Selected   
					grfx.FillRectangle(new SolidBrush(SelectedDayColor),List[i].Rec); 
					grfx.DrawString(List[i].Day.ToString()
						,FontText,Brushes.Black,List[i].xPos,List[i].yPos);
					DrawRowsOfText(i,grfx);
					DrawMonthGrid(grfx); 
					MarkTodayDate(grfx); */
				}
			}
			//g.Dispose();
		}

		///<summary></summary>
		public void ChangeColor(int day,Color color) {
			ListDays[day].color=color;
		}

		///<summary></summary>
		public void AddText(int day,string s) {
			if(ListDays[day].NumRowsText!=MaxRowsText) {
				for(int i=0;i<ListDays[day].RowsOfText.Count;i++) {
					if(ListDays[day].RowsOfText[i]=="" || ListDays[day].RowsOfText[i]==null) {
						ListDays[day].RowsOfText[i]=s;
						ListDays[day].NumRowsText++;
						return;
					}
				}
			}
			//else{
			//MessageBox.Show(Lan.g(this,"Too Many Rows of Text.  Can Only Have "+MaxRowsText.ToString()));  
			//}  
		}

		///<summary></summary>
		public void ResetList() {
			for(int i=1;i<ListDays.Count;i++) {
				ListDays[i].color=Color.Empty;
				ListDays[i].RowsOfText=new List<string>();
				ListDays[i].NumRowsText=0;
			}
		}

		private void ContrCalendar_Resize(object sender,System.EventArgs e) {
			this.Invalidate();
			//this.OnPaint(new PaintEventArgs(this.CreateGraphics(),this.ClientRectangle)); 
		}

		private void butPrevious_Click(object sender,System.EventArgs e) {
			//Graphics g=this.CreateGraphics();
			selectedDate=selectedDate.AddMonths(-1);
			//DisplayDaysInMonth(g);
			OnChangeMonth(e);
			//grfx.Dispose();
			this.Invalidate();
			//this.OnPaint(new PaintEventArgs(this.CreateGraphics(),this.ClientRectangle)); 
		}

		private void butNext_Click(object sender,System.EventArgs e) {
			//Graphics grfx=this.CreateGraphics();
			selectedDate=selectedDate.AddMonths(1);
			//DisplayDaysInMonth(grfx);
			//grfx.Dispose();
			OnChangeMonth(e);
			this.Invalidate();
			//this.OnPaint(new PaintEventArgs(this.CreateGraphics(),this.ClientRectangle)); 	
		}

		///<summary></summary>
		public delegate void CellEventHandler(object sender,CellEventArgs e);
		///<summary></summary>
		public delegate void EventHandler(object sender,EventArgs e);
		///<summary></summary>
		public event CellEventHandler CellClicked;
		///<summary></summary>
		public event CellEventHandler CellDoubleClicked;
		///<summary></summary>
		public event EventHandler ChangeMonth;

		///<summary></summary>
		protected virtual void OnCellClicked(CellEventArgs e) {
			if(CellClicked !=null) {
				CellClicked(this,e);
			}
		}

		///<summary></summary>
		protected virtual void OnCellDoubleClicked(CellEventArgs e) {
			if(CellDoubleClicked !=null) {
				CellDoubleClicked(this,e);
			}
		}

		///<summary></summary>
		protected virtual void OnChangeMonth(EventArgs e) {
			if(ChangeMonth !=null) {
				ChangeMonth(this,e);
			}
		}
	}


	//Object keeps track of drawing coords,text, and date.  each day is stored in List to draw each month
	///<summary></summary>
	public class OneCalendarDay {
		///<summary></summary>
		public Rectangle Bounds;
		///<summary></summary>
		public DateTime Date;
		///<summary></summary>
		public Color color;
		///<summary></summary>
		public bool IsSelected;
		///<summary></summary>
		public List<string> RowsOfText;
		///<summary></summary>
		public int NumRowsText;

		public OneCalendarDay() {
			RowsOfText=new List<string>();
		}

		public OneCalendarDay Copy() {
			return (OneCalendarDay)this.MemberwiseClone();
		}
	}
}
