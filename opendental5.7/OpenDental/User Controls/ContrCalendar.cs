using System;
using System.Collections;
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

namespace OpenDental{

	///<summary></summary>
	public class ContrCalendar : System.Windows.Forms.UserControl	{
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butPrevious;
		private OpenDental.UI.Button butNext; 

    //private Graphics grfx;
    private int RowCount;
    private int ColCount;
    private float RowHeight;
    private float ColWidth;
    private float FixedHeight;
    private float FixedWidth;
    private float DayHeadHeight; 
    private Rectangle RecBG;
    private Rectangle RecHead;
    private Rectangle RecDayHead;
    private Rectangle RecFoot;
    private Rectangle RecBG2;
    private Rectangle RecActDay;
    private Color BGColor;
    private Color BG2Color;
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
    public OneCalendarDay[] List;
		///<summary></summary>
    public int SelectedDay;
		///<summary>Was called CurrentDay</summary>
    public OneCalendarDay Today;
		///<summary></summary>
    public int MaxRowsText; 
		//private int count=0;

		///<summary></summary>
		public ContrCalendar(){
			InitializeComponent();
      RowCount=6;
      ColCount=7;
      FixedHeight=25;
      FixedWidth=25;
      BGColor=SystemColors.Window;
      BG2Color=SystemColors.Control; 
      HeadColor=SystemColors.Control;
      FootColor=SystemColors.Control;
      DayHeadColor=SystemColors.Window;
      TextColor=Color.Black;//SystemColors.ControlText;
      DayOpenColor=Color.White;//SystemColors.Window; Was ActiveDayColor
      //SelectedDayColor=Color.White;//SystemColors.Highlight;  
      LinePen=new Pen(Color.SlateGray,2);
			FontText=new Font("Microsoft Sans Serif",8,FontStyle.Bold);
 			FontText2=new Font("Microsoft Sans Serif",8,FontStyle.Regular);
      DayHeadHeight=FontText.GetHeight()+6;
			FontHeader = new Font("Microsoft Sans Serif",9,FontStyle.Bold);
      MaxRowsText=5;
      List=new OneCalendarDay[32];      
      for(int i=0;i<List.Length;i++){
				List[i]=new OneCalendarDay();
        List[i].RowsOfText=new string[MaxRowsText];
      }   
    }

		///<summary></summary>
		protected override void Dispose(bool disposing){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContrCalendar));
			this.butPrevious = new OpenDental.UI.Button();
			this.butNext = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butPrevious
			// 
			this.butPrevious.Image = ((System.Drawing.Image)(resources.GetObject("butPrevious.Image")));
			this.butPrevious.Location = new System.Drawing.Point(32, 34);
			this.butPrevious.Name = "butPrevious";
			this.butPrevious.Size = new System.Drawing.Size(22, 22);
			this.butPrevious.TabIndex = 0;
			this.butPrevious.Click += new System.EventHandler(this.butPrevious_Click);
			// 
			// butNext
			// 
			this.butNext.Image = ((System.Drawing.Image)(resources.GetObject("butNext.Image")));
			this.butNext.Location = new System.Drawing.Point(538, 22);
			this.butNext.Name = "butNext";
			this.butNext.Size = new System.Drawing.Size(22, 22);
			this.butNext.TabIndex = 1;
			this.butNext.Click += new System.EventHandler(this.butNext_Click);
			// 
			// ContrCalendar
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.butNext);
			this.Controls.Add(this.butPrevious);
			this.Name = "ContrCalendar";
			this.Size = new System.Drawing.Size(600, 600);
			this.Resize += new System.EventHandler(this.ContrCalendar_Resize);
			this.Load += new System.EventHandler(this.ContrCalendar_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ContrCalendar_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContrCalendar_MouseDown);
			this.ResumeLayout(false);

		}
		#endregion

		
		///<summary></summary>
		[Category("AAA Custom"),
			Description("SelectedDate. Keeps track of the date selected")
		]
		public DateTime SelectedDate{
			get{ 
				return selectedDate; 
			}
			set{ 
				selectedDate = value;
			}
		}

		private void ContrCalendar_Load(object sender, System.EventArgs e) {
      selectedDate=DateTime.Today;
      SelectedDay=SelectedDate.Day;
			//this needs work.  should be a simple invalidate instead:
			Graphics g=this.CreateGraphics();
      this.OnPaint(new PaintEventArgs(g,this.ClientRectangle)); 
			g.Dispose();
			//this.Invalidate();
    }

		private void ContrCalendar_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
      RecBG=new Rectangle(0,0,Width-1,Height-1); 
      RecBG2=new Rectangle((int)FixedWidth,(int)FixedHeight,(int)(Width-(FixedWidth*2))
				,(int)(Height-(FixedHeight*2))); 
      RecHead=new Rectangle((int)FixedWidth,(int)FixedHeight,(int)(Width-(FixedWidth*2))
				,(int)FixedHeight); 
      RecFoot=new Rectangle((int)FixedWidth,(int)(Height-(FixedHeight*2)),(int)(Width-(FixedWidth*2))
				,(int)FixedHeight);
      RecDayHead=new Rectangle((int)FixedWidth,(int)(FixedHeight*2),(int)(Width-(FixedWidth*2))
				,(int)DayHeadHeight); 
      RowHeight=(Height-(FixedHeight*4)-DayHeadHeight)/RowCount;
      ColWidth=(Width-FixedHeight*2)/ColCount;       
  		//Graphics grfx=e.Graphics;
      //Draw Shade and Border
			e.Graphics.FillRectangle(new SolidBrush(BGColor),RecBG);
      e.Graphics.FillRectangle(new SolidBrush(BG2Color),RecBG2);    
      e.Graphics.DrawRectangle(LinePen,RecBG);
			//Debug.WriteLine(count);
			//count++;
      Display(e.Graphics);
    }
 
    private void Display(Graphics grfx){
      DisplayHeader(grfx);//Draws and Displays Header  
      DisplayDaysInMonth(grfx);//Display Days in Month
      DrawDays(grfx);
      DisplayDayHeader(grfx); //Draws and Displays Day Header
      PositionButtons();//Position Buttons  
      DisplayFooter(grfx);//Draw Footer
      MarkTodayDate(grfx); 
   	} 

    private void PositionButtons(){
      butPrevious.Location=new Point((int)(FixedWidth+2),(int)(FixedHeight+2));
      butNext.Location=new Point((int)(Width-FixedWidth-butNext.Size.Width-1),(int)(FixedHeight+2));
      butPrevious.BackColor=SystemColors.Control;
      butNext.BackColor=SystemColors.Control; 
    }

    private void DisplayHeader(Graphics grfx){
      grfx.FillRectangle(new SolidBrush(HeadColor),RecHead); 
      grfx.DrawRectangle(LinePen,RecHead);  
      Header=selectedDate.ToString("MMMM, yyyy");  
      float xPos=(int)(FixedWidth+((Width-(FixedWidth*2))/2)
				-(grfx.MeasureString(Header,FontHeader).Width/2));
      float yPos=(int)((FixedHeight)+((FixedHeight-FontHeader.GetHeight())/2));
      grfx.DrawString(Header,FontHeader,Brushes.Black,xPos,yPos);  
    }

		///<summary>Draws the days of the week</summary>
    private void DisplayDayHeader(Graphics grfx){
      grfx.FillRectangle(new SolidBrush(DayHeadColor),RecDayHead); 
      grfx.DrawRectangle(LinePen,RecDayHead);
      for(int i=0;i<=ColCount;i++){
         grfx.DrawLine(LinePen,ColWidth*i+FixedWidth,FixedHeight*2,ColWidth*i+FixedWidth
					 ,(FixedHeight*2)+DayHeadHeight); 
      }
			string[] daysOfWeek=CultureInfo.CurrentCulture.DateTimeFormat.DayNames;//already translated
			for(int i=0;i<daysOfWeek.Length;i++){
				float xPos=(int)(FixedWidth+(i*(ColWidth)+(ColWidth/2))
					-(grfx.MeasureString(daysOfWeek[i],FontText).Width/2));
				float yPos=(int)((FixedHeight*2)+((DayHeadHeight-FontText.GetHeight())/2));
				grfx.DrawString(daysOfWeek[i],FontText,Brushes.Black,xPos,yPos); 
      } 
    }

    private void DisplayFooter(Graphics grfx){
      grfx.FillRectangle(new SolidBrush(FootColor),RecFoot); 
      grfx.DrawRectangle(LinePen,RecFoot);
      Footer="Today: "+DateTime.Today.ToShortDateString();  
      float xPos=(int)(FixedWidth+((Width-(FixedWidth*2))/2)
				-(grfx.MeasureString(Footer,FontText).Width/2));
      float yPos=(int)((Height-(FixedHeight*2))+((FixedHeight-FontText.GetHeight())/2));
      grfx.DrawString(Lan.g(this,Footer),FontText,Brushes.Black,xPos,yPos);
    }
    
    private void DisplayDaysInMonth(Graphics grfx){
      int dayOfWeek;
      int column;
      int row=0;
      float fHeight;
      float fWidth;
      float fHeight2;
      float fWidth2;
      fWidth=FixedWidth+ColWidth-1;
      fWidth2=FixedWidth+ColWidth;
      fHeight=(FixedHeight*2)+DayHeadHeight+2;
      fHeight2=(FixedHeight*2)+DayHeadHeight;
      DateTime TempDate=new DateTime(selectedDate.Year,selectedDate.Month,1);
      dayOfWeek=(int)TempDate.DayOfWeek;  
      column=dayOfWeek;
      DaysInMonth=DateTime.DaysInMonth(selectedDate.Year,selectedDate.Month);
      for(int i=1;i<=DaysInMonth;i++){
        if(column==7){
          row++;
          column=0;
        }
 				float xPos=(int)(fWidth+(ColWidth*column)-grfx.MeasureString(i.ToString(),FontText).Width-3);
				float yPos=(int)(fHeight+(RowHeight*row)+2);
        RecActDay=new Rectangle((int)Math.Round((fWidth2+(ColWidth*(column-1)))),(int)Math.Round((fHeight2+(RowHeight*row))),
          (int)Math.Round(ColWidth),(int)Math.Round(RowHeight));
        List[i].Rec=RecActDay;
        List[i].Day=i;
        List[i].xPos=xPos;
        List[i].yPos=yPos;
        List[i].Date=new DateTime(selectedDate.Year,selectedDate.Month,i);
        if(i==DateTime.Today.Day
					&& selectedDate.Month==DateTime.Today.Month 
					&& selectedDate.Year==DateTime.Today.Year)
				{
          Today=List[i];
          Today.Rec.X+=1;
          Today.Rec.Y+=2;
          Today.Rec.Width-=2;
          Today.Rec.Height-=3;
        } 
        if(i==SelectedDay){
          SelectedDay=i; 
          List[i].IsSelected=true; 
        }
        column++;
      }      
    }

		///<summary></summary>
    public void DrawDays(Graphics g){
      for(int i=1;i<=DaysInMonth;i++){
				//if(i==SelectedDay){
				//	g.FillRectangle(new SolidBrush(SelectedDayColor),List[i].Rec);
				//	g.DrawString(List[i].Day.ToString()
				//		,FontText,Brushes.Black,List[i].xPos,List[i].yPos);
				//}
				//else{
          if(List[i].color.Equals(Color.Empty)){   
					  g.FillRectangle(new SolidBrush(DayOpenColor),List[i].Rec);
					  g.DrawString(List[i].Day.ToString()
							,FontText,Brushes.Black,List[i].xPos,List[i].yPos);
          }
          else{
					  g.FillRectangle(new SolidBrush(List[i].color),List[i].Rec);
					  g.DrawString(List[i].Day.ToString()
							,FontText,Brushes.Black,List[i].xPos,List[i].yPos);
          }           
				//}
        DrawRowsOfText(i,g);
      }
      DrawMonthGrid(g); 
      MarkTodayDate(g);
    }
  
    private void DrawRowsOfText(int day,Graphics grfx){
			float extra=2;
      float xCoord=(int)(List[day].Rec.X+extra);
      float yCoord=(float)(List[day].Rec.Y+(FontText2.GetHeight()*1.5));
      RectangleF r=new RectangleF(xCoord,yCoord,List[day].Rec.Width-(2*extra)
				,(float)(FontText2.GetHeight())); 
      grfx.DrawString(List[day].RowsOfText[0],FontText2,Brushes.Black,r);
			for(int i=0;i<List[day].RowsOfText.Length;i++){
				if(List[day].RowsOfText[i]==""){
        }
        else{ 
          grfx.DrawString(List[day].RowsOfText[i],FontText2,Brushes.Black
            ,new RectangleF(xCoord,yCoord,List[day].Rec.Width-(2*extra),FontText2.GetHeight()));
					  yCoord+=FontText2.GetHeight();
				}  
			}     
    }

    private void DrawMonthGrid(Graphics grfx){ 
      //draw Rows  
      float height=(FixedHeight*2)+DayHeadHeight;
      for(int i=1;i<=RowCount;i++){
        grfx.DrawLine(LinePen,FixedWidth,(height+(RowHeight*i))
					,(Width-FixedWidth),(height)+(RowHeight*i)); 
      } 
      //draw Columns
      for(int i=0;i<=ColCount;i++){
         grfx.DrawLine(LinePen,ColWidth*i+FixedWidth,height
					 ,ColWidth*i+FixedWidth,Height-FixedHeight*2); 
      }
    }

    private void MarkTodayDate(Graphics grfx){
      if(List[SelectedDay].Date.Month==DateTime.Today.Month){
        grfx.DrawRectangle(new Pen(Color.Red),Today.Rec); 
      }  
    }     

		private void ContrCalendar_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
		  Graphics grfx=this.CreateGraphics();
      int OldSelected=SelectedDay;
      for(int i=1;i<=DaysInMonth;i++){
        if(e.X >= List[i].Rec.X && e.X <= List[i].Rec.X+List[i].Rec.Width 
					&& e.Y >= List[i].Rec.Y && e.Y <= List[i].Rec.Y+List[i].Rec.Height){
          SelectedDay=List[i].Day; 
          selectedDate=List[i].Date;
          List[i].IsSelected=true;
          List[OldSelected].IsSelected=false; 
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
			grfx.Dispose();
		}
 
		///<summary></summary>
		public void ChangeColor(int day, Color color){
			List[day].color=color;
		}

		///<summary></summary>
    public void AddText(int day,string s){
      if(List[day].NumRowsText!=MaxRowsText){ 
				for(int i=0;i<List[day].RowsOfText.Length;i++){ 
					if(List[day].RowsOfText[i]=="" || List[day].RowsOfText[i]==null){
						List[day].RowsOfText[i]=s;
						List[day].NumRowsText++;  
						return;          
					}        
				} 
      }
      //else{
        //MessageBox.Show(Lan.g(this,"Too Many Rows of Text.  Can Only Have "+MaxRowsText.ToString()));  
      //}  
    } 

		///<summary></summary>
    public void ResetList(){
      for(int i=1;i<List.Length;i++){
        List[i].color=Color.Empty;
        List[i].RowsOfText=new string[MaxRowsText];
        List[i].NumRowsText=0;
      }
    }

		private void ContrCalendar_Resize(object sender, System.EventArgs e) {
			this.Invalidate();
      //this.OnPaint(new PaintEventArgs(this.CreateGraphics(),this.ClientRectangle)); 
		}

		private void butPrevious_Click(object sender, System.EventArgs e) {
      Graphics grfx=this.CreateGraphics();
			selectedDate=selectedDate.AddMonths(-1);
      DisplayDaysInMonth(grfx); 
      OnChangeMonth(e);
			grfx.Dispose();
			this.Invalidate();
      //this.OnPaint(new PaintEventArgs(this.CreateGraphics(),this.ClientRectangle)); 
		}

		private void butNext_Click(object sender, System.EventArgs e) {
      Graphics grfx=this.CreateGraphics();
      selectedDate=selectedDate.AddMonths(1);
      DisplayDaysInMonth(grfx); 
			grfx.Dispose();
      OnChangeMonth(e);
			this.Invalidate();
      //this.OnPaint(new PaintEventArgs(this.CreateGraphics(),this.ClientRectangle)); 	
		}

		///<summary></summary>
		public delegate void CellEventHandler(object sender, CellEventArgs e);
		///<summary></summary>
    public delegate void EventHandler(object sender, EventArgs e);
		///<summary></summary>
		public event CellEventHandler CellClicked;
		///<summary></summary>
		public event CellEventHandler CellDoubleClicked;
		///<summary></summary>
    public event EventHandler ChangeMonth;

		///<summary></summary>
		protected virtual void OnCellClicked(CellEventArgs e){
			if(CellClicked !=null){
				CellClicked(this,e);
			}
		}

		///<summary></summary>
		protected virtual void OnCellDoubleClicked(CellEventArgs e){
			if(CellDoubleClicked !=null){
				CellDoubleClicked(this,e);
			}
		}

		///<summary></summary>
    protected virtual void OnChangeMonth(EventArgs e){
      if(ChangeMonth !=null){
        ChangeMonth(this,e);  
      }
    }
  }

	
  //Object keeps track of drawing coords,text, and date.  each day is stored in List to draw each month
	///<summary></summary>
  public class OneCalendarDay{
		///<summary></summary>
    public Rectangle Rec;
		///<summary></summary>
    public float xPos;
		///<summary></summary>
    public float yPos;
		///<summary></summary>
    public int Day;
		///<summary></summary>
    public DateTime Date;
		///<summary></summary>
    public Color color;
		///<summary></summary>
    public bool IsSelected;
		///<summary></summary>
    public string[] RowsOfText;
		///<summary></summary>
    public int NumRowsText; 
  }	
}
