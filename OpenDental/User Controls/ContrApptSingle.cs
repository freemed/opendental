/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{

	///<summary></summary>
	public class ContrApptSingle : System.Windows.Forms.UserControl{
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary>Set on mouse down or from Appt module</summary>
		public static int ClickedAptNum;
		///<summary>set manually</summary>
		public static int SelectedAptNum;
		///<summary>True if this control is on the pinboard</summary>
		public bool ThisIsPinBoard;
		///<summary>True if pinboard is selected</summary>
		public static bool PinBoardIsSelected;
		///<summary>Stores the shading info for the provider bars on the left of the appointments module</summary>
		public static int[][] ProvBar;
		//<summary>Contains info inluding the lab, procs, and all other items that need to be displayed. Also contains Appointment object</summary>
		//public InfoApt Info;
		//<summary>Set to true if this appointment is simply being displayed in the Chart module Planned apt section rather than in the Appointments module.</summary>
		//public bool IsPlanned;
		///<summary>Stores the background bitmap for this control</summary>
		public Bitmap Shadow;
		private Font baseFont=new Font("Arial",8);
		private string patternShowing;
		public bool IsWeeklyView;
		///<Summary>This is a datarow that stores all info necessary to draw appt.  Replacing Info.</Summary>
		public DataRow DataRoww;

		///<summary></summary>
		public ContrApptSingle(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			//Info=new InfoApt();
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		private void InitializeComponent(){
			// 
			// ContrApptSingle
			// 
			this.Name = "ContrApptSingle";
			this.Size = new System.Drawing.Size(85, 72);
			this.Load += new System.EventHandler(this.ContrApptSingle_Load);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContrApptSingle_MouseDown);

		}
		#endregion
		
		///<summary></summary>
		protected override void OnPaint(PaintEventArgs pea){
			//Graphics grfx=pea.Graphics;
			//grfx.DrawImage(shadow,0,0);
		}

		
		///<summary>This is only called when viewing appointments on the Appt module.  For Next apt and pinboard, use SetHeight instead so that the location won't change.</summary>
		public void SetLocation(){
			Location=new Point(ConvertToX()+2,ConvertToY());
			Width=ContrApptSheet.ColWidth-5;
			SetSize();
		}

		///<summary>Used from SetLocation. Also used for Next apt and pinboard instead of SetLocation so that the location won't be altered.</summary>
		public void SetSize(){
			patternShowing=GetPatternShowing(DataRoww["Pattern"].ToString());
			//height is based on original 5 minute pattern. Might result in half-rows
			Height=DataRoww["Pattern"].ToString().Length*ContrApptSheet.Lh*ContrApptSheet.RowsPerIncr;
			//if(ContrApptSheet.TwoRowsPerIncrement){
			//	Height=Height*2;
			//}
			if(PrefB.GetInt("AppointmentTimeIncrement")==10){
				Height=Height/2;
			}
			else{//15 minute increments
				Height=Height/3;
			}
			if(ThisIsPinBoard){
				if(Height > ContrAppt.PinboardSize.Height-4)
					Height=ContrAppt.PinboardSize.Height-4;
				if(Width > ContrAppt.PinboardSize.Width-4)
					Width=ContrAppt.PinboardSize.Width-4;
			}
		}
		
		///<summary>Called from SetLocation to establish X position of control.</summary>
		private int ConvertToX(){
			if(IsWeeklyView) {
				return ContrApptSheet.TimeWidth+ContrApptSheet.ProvWidth*ContrApptSheet.ProvCount
					+ContrApptSheet.ColWidth*((int)PIn.PDateT(DataRoww["AptDateTime"].ToString()).DayOfWeek-1)+1;
			}
			else {
				return ContrApptSheet.TimeWidth+ContrApptSheet.ProvWidth*ContrApptSheet.ProvCount
					+ContrApptSheet.ColWidth*(ApptViewItems.GetIndexOp(PIn.PInt(DataRoww["Op"].ToString())))+1;
					//Info.MyApt.Op))+1;
			}
		}

		///<summary>Called from SetLocation to establish Y position of control.  Also called from ContrAppt.RefreshDay when determining provBar markings. Does not round to the nearest row.</summary>
		public int ConvertToY(){
			DateTime aptDateTime=PIn.PDateT(DataRoww["AptDateTime"].ToString());
			int retVal=(int)(((double)aptDateTime.Hour*(double)60
				/(double)PrefB.GetInt("AppointmentTimeIncrement")
				+(double)aptDateTime.Minute
				/(double)PrefB.GetInt("AppointmentTimeIncrement")
				)*(double)ContrApptSheet.Lh*ContrApptSheet.RowsPerIncr);
			//if(ContrApptSheet.TwoRowsPerIncrement){
			//	retVal=retVal*2;
			//}
			return retVal;//(Info.MyApt.AptDateTime.Hour*6+)*ContrApptSheet.Lh;
		}

		///<summary>This converts the dbPattern in 5 minute interval into the pattern that will be viewed based on RowsPerIncrement and AppointmentTimeIncrement.  So it will always depend on the current view.Therefore, it should only be used for visual display purposes rather than within the FormAptEdit. If height of appointment allows a half row, then this includes an increment for that half row.</summary>
		public static string GetPatternShowing(string dbPattern){
			StringBuilder strBTime=new StringBuilder();
			for(int i=0;i<dbPattern.Length;i++){
				//strBTime.Append(dbPattern.Substring(i,1));
				for(int j=0;j<ContrApptSheet.RowsPerIncr;j++){
					strBTime.Append(dbPattern.Substring(i,1));
				}
				//if(ContrApptSheet.TwoRowsPerIncrement){
				//	strBTime.Append(dbPattern.Substring(i,1));
				//}
				i++;//skip
				if(PrefB.GetInt("AppointmentTimeIncrement")==15){
					i++;//skip another
				}
			}
			return strBTime.ToString();
		}

		///<summary>It is planned to move some of this logic to OnPaint and use a true double buffer.</summary>
		public void CreateShadow(){
			if(this.Parent is ContrApptSheet) {
				bool isVisible=false;
				for(int j=0;j<ApptViewItems.VisOps.Length;j++){
					if(this.DataRoww["Op"].ToString()==Operatories.ListShort[ApptViewItems.VisOps[j]].OperatoryNum.ToString()){
						isVisible=true;
					}
				}
				if(!isVisible){
					return;
				}
			}
			if(Shadow!=null){
				Shadow=null;
			}
			if(Width<4){
				return;
			}
			if(Height<4){
				return;
			}
			Shadow=new Bitmap(Width,Height);
			Graphics g=Graphics.FromImage(Shadow);
			Pen penB=new Pen(Color.Black);
			Pen penW=new Pen(Color.White);
			Pen penGr=new Pen(Color.SlateGray);
			Pen penDG=new Pen(Color.DarkSlateGray);
			Pen penO;//provider outline color
			Color backColor;
			Color provColor;
			Color confirmColor;
			confirmColor=DefB.GetColor(DefCat.ApptConfirmed,PIn.PInt(DataRoww["Confirmed"].ToString()));
			if(DataRoww["ProvNum"].ToString()!="0" && DataRoww["IsHygiene"].ToString()=="0"){//dentist
				provColor=Providers.GetColor(PIn.PInt(DataRoww["ProvNum"].ToString()));
				penO=new Pen(Providers.GetOutlineColor(PIn.PInt(DataRoww["ProvNum"].ToString())));
			}
			else if(DataRoww["ProvHyg"].ToString()!="0" && DataRoww["IsHygiene"].ToString()=="1"){//hygienist
				provColor=Providers.GetColor(PIn.PInt(DataRoww["ProvHyg"].ToString()));
				penO=new Pen(Providers.GetOutlineColor(PIn.PInt(DataRoww["ProvHyg"].ToString())));
			}
			else{//unknown
				provColor=Color.White;
				penO=new Pen(Color.Black);
			}
			if(PIn.PInt(DataRoww["AptStatus"].ToString())==(int)ApptStatus.Complete){
				backColor=DefB.Long[(int)DefCat.AppointmentColors][3].ItemColor;
			}
			else{
				backColor=provColor;
				//We might want to do something interesting here.
			}
			g.FillRectangle(new SolidBrush(backColor),7,0,Width-7,Height);
			g.FillRectangle(Brushes.White,0,0,7,Height);
			g.DrawLine(penB,7,0,7,Height);
			//Highlighting border
			if(PinBoardIsSelected && ThisIsPinBoard
				|| (DataRoww["AptNum"].ToString()==SelectedAptNum.ToString() && !ThisIsPinBoard))
			{
				//Left
				g.DrawLine(penO,8,1,8,Height-2);
				g.DrawLine(penO,9,1,9,Height-3);
				//g.DrawLine(penO,14,1,14,ContrApptSheet.Lh);
				//Right
				g.DrawLine(penO,Width-2,1,Width-2,Height-2);
				g.DrawLine(penO,Width-3,2,Width-3,Height-3);
				//Top
				//g.DrawLine(penO,8,ContrApptSheet.Lh,14,ContrApptSheet.Lh);
				g.DrawLine(penO,8,1,Width-2,1);
				g.DrawLine(penO,8,2,Width-3,2);
				//bottom
				g.DrawLine(penO,9,Height-2,Width-2,Height-2);
				g.DrawLine(penO,10,Height-3,Width-3,Height-3);
			}
			//Font fontSF=new Font("Arial",8);
			Pen penTimediv=Pens.Silver;
			g.TextRenderingHint=TextRenderingHint.SingleBitPerPixelGridFit;//to make printing clearer
			for(int i=0;i<patternShowing.Length;i++){//Info.MyApt.Pattern.Length;i++){
				if(patternShowing.Substring(i,1)=="X"){	
					g.FillRectangle(new SolidBrush(provColor),1,i*ContrApptSheet.Lh+1,6,ContrApptSheet.Lh);
					//g.DrawLine(penB,6,i*ContrApptSheet.Lh+1,1,(i+1)*ContrApptSheet.Lh-2);
					//g.DrawLine(penB,1,i*ContrApptSheet.Lh+1,6,(i+1)*ContrApptSheet.Lh-2);
				}
				else{
					//g.DrawLine(penB,6,i*ContrApptSheet.Lh+1,1,(i+1)*ContrApptSheet.Lh-2);
				}
				//if(i!=0){//gray line
					//example. For 2 rowsPerIncr, only draw on i=2,4,6, etc. for 1 rowPerIncr, only draw on i=1,2,3,4,etc
				if(Math.IEEERemainder((double)i,(double)ContrApptSheet.RowsPerIncr)==0){//0/1
					g.DrawLine(penTimediv,1,i*ContrApptSheet.Lh,6,i*ContrApptSheet.Lh);
				}	
				//}
			}
			//elements=new string[] {"PatientName","Note","Lab","Procs"};
			int row=0;
			int elementI=0;
			while(row<patternShowing.Length && elementI<ApptViewItems.ApptRows.Length){
				row+=OnDrawElement(g,elementI,row);
				elementI++;
			}
			//Main outline
			g.DrawRectangle(new Pen(Color.Black),0,0,Width-1,Height-1);
			//Credit and ins
			//g.FillRectangle(new SolidBrush(Color.White),1,1,12,ContrApptSheet.Lh-2);
			g.FillRectangle(new SolidBrush(confirmColor),Width-13,1,12,ContrApptSheet.Lh-2);
			//g.DrawRectangle(new Pen(Color.Black),0,0,13,ContrApptSheet.Lh-1);
			g.DrawRectangle(new Pen(Color.Black),Width-13,0,13,ContrApptSheet.Lh-1);
//broken
//g.DrawString(Info.MyPatient.GetCreditIns(),baseFont,new SolidBrush(Color.Black),Width-13,-1);//0,-1);
			//assistant box
/*if(Info.MyApt.Assistant!=0){
	g.FillRectangle(new SolidBrush(Color.White)
		,Width-18,Height-ContrApptSheet.Lh,17,ContrApptSheet.Lh-1);
	g.DrawLine(Pens.Gray,Width-18,Height-ContrApptSheet.Lh,Width,Height-ContrApptSheet.Lh);
	g.DrawLine(Pens.Gray,Width-18,Height-ContrApptSheet.Lh,Width-18,Height);
	g.DrawString(Employees.GetAbbr(Info.MyApt.Assistant)
		,baseFont,new SolidBrush(Color.Black),Width-18,Height-ContrApptSheet.Lh-1);
}*/
			//g.DrawString(":10",font,new SolidBrush(Color.Black),timeWidth-19,i*Lh*6+Lh-1);
/*if(Info.MyApt.AptStatus==ApptStatus.Broken){
	g.DrawLine(new Pen(Color.Black),8,1,Width-1,Height-1);
	g.DrawLine(new Pen(Color.Black),8,Height-1,Width-1,1);
}*/
			this.BackgroundImage=Shadow;
			//Shadow=null;
			g.Dispose();
		}

		///<summary>Called from createShadow once for each element. The rowI is specified so that this method knows where to start drawing.  Returns the number of rows that this element fills.</summary>
		private int OnDrawElement(Graphics g,int elementI,int rowI){
			int xPos=9;
			//if(rowI==0)
				//xPos=13;//the first row is indented because of CreditIns
			int yPos=rowI*ContrApptSheet.Lh;
			SolidBrush brush=new SolidBrush(ApptViewItems.ApptRows[elementI].ElementColor);
			StringFormat format=new StringFormat();
			format.Alignment=StringAlignment.Near;
			int charactersFitted;//not used
			int linesFilled;
			SizeF noteSize;
			RectangleF rect;
			string text;
			switch(ApptViewItems.ApptRows[elementI].ElementDesc){
				case "AddrNote":
					text=DataRoww["addrNote"].ToString();
					if(rowI==0)
						noteSize=g.MeasureString(text,baseFont,ContrApptSheet.ColWidth-9-4);
					else
						noteSize=g.MeasureString(text,baseFont,ContrApptSheet.ColWidth-9);
					g.MeasureString(text,baseFont,noteSize,format,out charactersFitted,out linesFilled);
					rect=new RectangleF(new PointF(xPos,yPos),noteSize);
					g.DrawString(text,baseFont,brush,rect,format);
					return linesFilled;
				case "ChartNumAndName":
					text=DataRoww["chartNumAndName"].ToString();
					g.DrawString(text,baseFont,brush,xPos,yPos);
					return 1;
				case "ChartNumber":
					g.DrawString(DataRoww["chartNumber"].ToString(),baseFont,brush,xPos,yPos);
					return 1;
				case "HmPhone":
					g.DrawString(DataRoww["hmPhone"].ToString(),baseFont,brush,xPos,yPos);
					return 1;
				case "Lab":
					text=DataRoww["lab"].ToString();
					if(text==""){
						return 0;
					}
					else {
						g.DrawString(text,baseFont,brush,xPos,yPos);
						return 1;
					}
				case "MedUrgNote":
					text=DataRoww["MedUrgNote"].ToString();
					if(rowI==0)
						noteSize=g.MeasureString(text,baseFont,ContrApptSheet.ColWidth-9-4);
					else
						noteSize=g.MeasureString(text,baseFont,ContrApptSheet.ColWidth-9);
					g.MeasureString(text,baseFont,noteSize,format,out charactersFitted,out linesFilled);
					rect=new RectangleF(new PointF(xPos,yPos),noteSize);
					g.DrawString(text,baseFont,brush,rect,format);
					return linesFilled;
				case "Note":
					text=DataRoww["Note"].ToString();
					if(rowI==0)
						noteSize=g.MeasureString(text,baseFont,ContrApptSheet.ColWidth-9-4);
					else
						noteSize=g.MeasureString(text,baseFont,ContrApptSheet.ColWidth-9);
					g.MeasureString(text,baseFont,noteSize,format,out charactersFitted,out linesFilled);
					rect=new RectangleF(new PointF(xPos,yPos),noteSize);
					g.DrawString(text,baseFont,brush,rect,format);
					return linesFilled;
				case "PatientName":
					g.DrawString(DataRoww["patientName"].ToString(),baseFont,brush,xPos,yPos);
					return 1;
				case "PatNum":
					g.DrawString(DataRoww["patNum"].ToString(),baseFont,brush,xPos,yPos);
					return 1;
				case "PatNumAndName":
					g.DrawString(DataRoww["patNumAndName"].ToString(),baseFont,brush,xPos,yPos);
					return 1;
				case "PremedFlag":
					if(DataRoww["preMedFlag"].ToString()==""){
						return 0;
					}
					else{
						g.DrawString(DataRoww["preMedFlag"].ToString(),baseFont,brush,xPos,yPos);
						return 1;
					}
				/*case "Procs":
					//text=DataRoww[""].ToString();
					int rowsUsed=0;
					for(int j=0;j<Info.Procs.Length;j++){
						g.DrawString(Procedures.GetDescription(Info.Procs[j]),baseFont,brush,xPos,yPos);
						yPos+=ContrApptSheet.Lh;
						rowsUsed++;
					}
					return rowsUsed;
				//case "ProcDescript":
					//no longer used
					//g.DrawString(Info.MyApt.ProcDescript,baseFont,brush,xPos,yPos);
					//return 1;
				case "Production":
					//text=DataRoww[""].ToString();
					g.DrawString(Info.Production.ToString("c"),baseFont,brush,xPos,yPos);
					return 1;
				case "Provider":
					//text=DataRoww[""].ToString();
					if(Info.MyApt.IsHygiene && Providers.GetProv(Info.MyApt.ProvHyg)!=null){
						g.DrawString(Providers.GetNameLF(Info.MyApt.ProvHyg),baseFont,brush,xPos,yPos);
					}
					else{
						g.DrawString(Providers.GetNameLF(Info.MyApt.ProvNum),baseFont,brush,xPos,yPos);
					}
					return 1;
				case "WirelessPhone":
					//text=DataRoww[""].ToString();
					g.DrawString("Cell:"+Info.MyPatient.WirelessPhone,baseFont,brush,xPos,yPos);
					return 1;
				case "WkPhone":
					//text=DataRoww[""].ToString();
					g.DrawString("Wk:"+Info.MyPatient.WkPhone,baseFont,brush,xPos,yPos);
					return 1;
				case "Age":
					//text=DataRoww[""].ToString();
					g.DrawString("Age:"+Info.MyPatient.Age.ToString(),baseFont,brush,xPos,yPos);
					return 1;*/
			}
			return 0;
		}

		private void ContrApptSingle_Load(object sender, System.EventArgs e){
			/*
			if(Info.IsNext){
				Width=110;
				//don't change location
			}
			else{
				Location=new Point(ConvertToX(),ConvertToY());
				Width=ContrApptSheet.ColWidth-1;
				Height=Info.MyApt.Pattern.Length*ContrApptSheet.Lh;
			}
			*/
		}

		private void ContrApptSingle_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			ClickedAptNum=PIn.PInt(DataRoww["AptNum"].ToString());
		}




	}//end class
}//end namespace
