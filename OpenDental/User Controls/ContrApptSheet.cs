/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class ContrApptSheet : System.Windows.Forms.UserControl{
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary>The width of each operatory.</summary>
		public static int ColWidth;
		///<summary></summary>
		public static int TimeWidth;
		///<summary></summary>
		public static int ProvWidth;
		///<summary>Line height.</summary>
		public static int Lh;
		///<summary>The number of columns.  Stays consistent even if weekly view.  The number of colums showing for one day.</summary>
		public static int ColCount;
		///<summary></summary>
		public static int ProvCount;
		///<summary>Based on the view.  If no view, then it is set to 1. Different computers can be showing different views.</summary>
		public static int RowsPerIncr;
		///<summary>Pulled from Prefs AppointmentTimeIncrement.  For now, either 10 or 15. An increment can be one or more rows.</summary>
		public static int MinPerIncr;
		///<summary>Typical values would be 10,15,5,or 7.5.</summary>
		public static float MinPerRow;
		///<summary>Rows per hour, based on RowsPerIncr and MinPerIncr</summary>
		public static int RowsPerHr;
		///<summary></summary>
		public Bitmap Shadow;
		///<summary></summary>
		public bool IsScrolling=false;
		//public int selectedCat;//selected ApptCategory.
		private SolidBrush openBrush;
		private SolidBrush closedBrush;
    private SolidBrush holidayBrush;
		///<summary>This gets set externally each time the module is selected.  It is the background schedule for the entire period.  Includes all types.</summary>
		public Schedule[] SchedListPeriod;
		public static bool IsWeeklyView;
		///<summary>Typically 5. Only used with weekview.</summary>
		public static int NumOfWeekDaysToDisplay=5;
		///<summary>The width of an entire day if using week view.</summary>
		public static int ColDayWidth;
		///<summary>Only used with weekview. The width of individual appointments within each day.  There might be rounding errors for now.</summary>
		public static float ColAptWidth;
		//<summary>Monday for now if week view. This should have been added earlier, but it's just assumed in many places instead.</summary>
		//public static int WeekStartDay=1;

		///<summary></summary>
		public ContrApptSheet(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			//might need to add following for design-time support??(would currently cause bugs):
				//ColWidth=100;
				//ColCount=4;
				//ProvCount=3;
				//ContrApptSingle.ProvBar = new int[ProvCount][];//design-time support
				//for(int i=0;i<ProvCount;i++){
				//	ContrApptSingle.ProvBar[i] = new int[144];
				//}
			TimeWidth=37;
			ProvWidth=8;
			Lh=12;
			//selectedCat=-1;
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
			// ContrApptSheet
			// 
			this.Name = "ContrApptSheet";
			this.Size = new System.Drawing.Size(482, 540);
			this.Load += new System.EventHandler(this.ContrApptSheet_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrApptSheet_Layout);

		}
		#endregion

		private void ContrApptSheet_Load(object sender, System.EventArgs e) {
			
		}

		private void ContrApptSheet_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			//Height=Lh*24*6;
			//Width=TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount;
		}

		/*public int ConvertToX (int op){
				return timeWidth+ProvWidth*ProvCount+ColWidth*(op-1);
		}

		public int ConvertToY (double timeApt){//decimal port. of timeApt used as base 6, not 10
			int myHours=0;
			int myMins=0;
			if (timeApt > 7.5){ //after 7:50 is am appt
				//myHours=(int)Decimal.Truncate(timeApt);
				myMins=(int)((timeApt-myHours)*10);
				return ((myHours-startTime)*6+myMins)*Lh;
			}
			else{//pm appt
				return 100;
			}
		}*/

		///<summary></summary>
		public static int XPosToOp(int xPos) {
			int retVal;
			if(IsWeeklyView){
				int day=XPosToDay(xPos);
				retVal=(int)Math.Floor((double)(xPos-TimeWidth-day*ColDayWidth)/ColAptWidth);
			}
			else{
				retVal=(int)Math.Floor((double)(xPos-TimeWidth-ProvWidth*ProvCount)/ColWidth);
			}
			if(retVal>ColCount-1)
				retVal=ColCount-1;
			if(retVal<0)
				retVal=0;
			return retVal;
		}

		///<summary>If not weekview, then it always returns 0.  If weekview, then it gives the dayofweek as int. Always based on current view, so 0 will be first day showing.</summary>
		public static int XPosToDay(int xPos){
			if(!IsWeeklyView){
				return 0;
			}
			int retVal=(int)Math.Floor((double)(xPos-TimeWidth)/ColDayWidth);
			if(retVal>NumOfWeekDaysToDisplay-1)
				retVal=NumOfWeekDaysToDisplay-1;
			if(retVal<0)
				retVal=0;
			return retVal;
		}

		///<summary>Called when mouse down anywhere on apptSheet. Automatically rounds down.</summary>
		public static int YPosToHour(int yPos){
			int retVal=yPos/Lh/RowsPerHr;//newY/Lh/6;
			return retVal;
		}

		///<summary>Called when mouse down anywhere on apptSheet. This will give very precise minutes. It is not rounded for accuracy.</summary>
		public static int YPosToMin(int yPos){
			int hourPortion=YPosToHour(yPos)*Lh*RowsPerHr;
			float MinPerPixel=60/(float)Lh/(float)RowsPerHr;
			int minutes=(int)((yPos-hourPortion)*MinPerPixel);
			return minutes;
		}

		///<summary>Used when dropping an appointment to a new location.  Converts x-coordinate to operatory index of ApptCatItems.VisOps, rounding to the nearest.  In this respect it is very different from XPosToOp.</summary>
		public int ConvertToOp(int newX){
			int retVal=0;
			if(IsWeeklyView){
				int dayI=XPosToDay(newX);//does not round
				int deltaDay=dayI*ColDayWidth;
				int adjustedX=newX-TimeWidth-deltaDay;
				retVal=(int)Math.Round((double)(adjustedX)/ColAptWidth);
				//when there are multiple days, special situation where x is within the last op for the day, so it goes to next day.
				if(retVal>ApptViewItems.VisOps.Length-1 && dayI<NumOfWeekDaysToDisplay-1){
					retVal=0;
				}
			}
			else{
				retVal=(int)Math.Round((double)(newX-TimeWidth-ProvWidth*ProvCount)/ColWidth);
			}
			//make sure it's not outside bounds of array:
			if(retVal > ApptViewItems.VisOps.Length-1)
				retVal=ApptViewItems.VisOps.Length-1;
			if(retVal<0)
				retVal=0;
			return retVal;
		}

		///<summary>Used when dropping an appointment to a new location.  Converts x-coordinate to day index.  Only used in weekly view.</summary>
		public int ConvertToDay(int newX) {
			int retVal=(int)Math.Floor((double)(newX-TimeWidth)/(double)ColDayWidth);
			//the above works for every situation except when in the right half of the last op for a day. Test for that situation:
			if(newX-TimeWidth > (retVal+1)*ColDayWidth-ColAptWidth/2){
				retVal++;
			}
			//make sure it's not outside bounds of array:
			if(retVal>NumOfWeekDaysToDisplay-1)
				retVal=NumOfWeekDaysToDisplay-1;
			if(retVal<0)
				retVal=0;
			return retVal;
		}

		///<summary>Used when dropping an appointment to a new location. Rounds to the nearest increment.</summary>
		public int ConvertToHour(int newY){
			//return (int)((newY+Lh/2)/6/Lh);
			return (int)(((double)newY+(double)Lh*(double)RowsPerIncr/2)/(double)RowsPerHr/(double)Lh);
		}

		///<summary>Used when dropping an appointment to a new location. Rounds to the nearest increment.</summary>
		public int ConvertToMin(int newY){
			//int retVal=(int)(Decimal.Remainder(newY,6*Lh)/Lh)*10;
			//first, add pixels equivalent to 1/2 increment: newY+Lh*RowsPerIncr/2
			//Yloc     Height     Rows      1
			//---- + ( ------ x --------- x - )
			//  1       Row     Increment   2
			//then divide by height per hour: RowsPerHr*Lh
			//Rows   Height
			//---- * ------
			//Hour    Row
			int pixels=(int)Decimal.Remainder(
				(decimal)newY+(decimal)Lh*(decimal)RowsPerIncr/2
				,(decimal)RowsPerHr*(decimal)Lh);
			//We are only interested in the remainder, and this is called pixels.
			//Convert pixels to increments. Round down to nearest increment when converting to int.
			//pixels/Lh/RowsPerIncr:
			//pixels    Rows    Increment
			//------ x ------ x ---------
			//  1      pixels     Rows
			int increments=(int)((double)pixels/(double)Lh/(double)RowsPerIncr);
			//Convert increments to minutes: increments*MinPerIncr
      int retVal=increments*MinPerIncr;
			if(retVal==60)
				return 0;
			return retVal;
		}

		///<summary></summary>
		protected override void OnPaint(PaintEventArgs pea){
			DrawShadow();
		}

		///<summary>This technique is just stupid.  Needs to be rewritten to use OnDraw with double buffering.</summary>
		public void CreateShadow(){
			if(Shadow!=null){
				Shadow=null;
			}
			if(Width<2)
				return;
			Shadow=new Bitmap(Width,Height);
			if(RowsPerIncr==0)
				RowsPerIncr=1;
			if(SchedListPeriod==null){
				return;//not sure if this is necessary
			}
			Graphics g=Graphics.FromImage(Shadow);
			//background
			g.FillRectangle(new SolidBrush(Color.LightGray),0,0,TimeWidth,Height);//L time bar
			g.FillRectangle(new SolidBrush(Color.LightGray),TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,0,TimeWidth,Height);//R time bar
			try{
				openBrush=new SolidBrush(DefB.Long[(int)DefCat.AppointmentColors][0].ItemColor);
				closedBrush=new SolidBrush(DefB.Long[(int)DefCat.AppointmentColors][1].ItemColor);
        holidayBrush=new SolidBrush(DefB.Long[(int)DefCat.AppointmentColors][4].ItemColor);  
			}
			catch{//this is just for design-time
				openBrush=new SolidBrush(Color.White);
				closedBrush=new SolidBrush(Color.LightGray);
        holidayBrush=new SolidBrush(Color.FromArgb(255,128,128));
			}
			DrawMainBackground(g);
			DrawBlockouts(g);
			if(!IsWeeklyView){
				DrawProvSchedInTimebar(g);
				DrawProvTimebar(g);
			}
			DrawGridLines(g);
			DrawRedTimeIndicator(g);
			DrawMinutes(g);
			g.Dispose();
		}

		///<summary>Including the practice schedule</summary>
		private void DrawMainBackground(Graphics g){
			//SchedDefault[] schedDefs;//for one type at a time
			Schedule[] schedForType;
			//one giant rectangle for everything closed
			g.FillRectangle(closedBrush,TimeWidth,0,ColWidth*ColCount+ProvWidth*ProvCount,Height);
			//then, loop through each day and operatory
			Operatory curOp;
			bool isHoliday;
			if(IsWeeklyView){
				for(int d=0;d<NumOfWeekDaysToDisplay;d++){
					isHoliday=false;
					for(int i=0;i<SchedListPeriod.Length;i++) {
						if(SchedListPeriod[i].SchedType!=ScheduleType.Practice){
							continue;	
						}
						if(SchedListPeriod[i].Status!=SchedStatus.Holiday) {
							continue;
						}
						if((int)SchedListPeriod[i].SchedDate.DayOfWeek!=d+1){
							continue;
						}
						isHoliday=true;
						break;
					}
					if(isHoliday){
						g.FillRectangle(holidayBrush,TimeWidth+1+d*ColDayWidth,0,ColDayWidth,Height);
					}
					for(int j=0;j<ColCount;j++) {
						curOp=Operatories.ListShort[ApptViewItems.VisOps[j]];
						if(curOp.ProvDentist!=0 && !curOp.IsHygiene) {//dentist
							schedForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Provider,curOp.ProvDentist);
						}
						else if(curOp.ProvHygienist!=0 && curOp.IsHygiene) {//hygienist
							schedForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Provider,curOp.ProvHygienist);
						}
						else {//no provider set
							schedForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Provider,PrefB.GetInt("ScheduleProvUnassigned"));
						}
						for(int i=0;i<schedForType.Length;i++) {
							if((int)schedForType[i].SchedDate.DayOfWeek!=d+1){
								continue;
							}
							g.FillRectangle(openBrush
								,TimeWidth+1+d*ColDayWidth+(float)j*ColAptWidth
								,schedForType[i].StartTime.Hour*Lh*RowsPerHr+(int)schedForType[i].StartTime.Minute*Lh/MinPerRow//6RowsPerHr 10MinPerRow
								,ColAptWidth
								,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*Lh*RowsPerHr//6
								+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*Lh/MinPerRow);//10
						}
					}
				}
			}
			else{//only one day showing
				isHoliday=false;
				for(int i=0;i<SchedListPeriod.Length;i++) {
					if(SchedListPeriod[i].SchedType!=ScheduleType.Practice) {
						continue;
					}
					if(SchedListPeriod[i].Status!=SchedStatus.Holiday) {
						continue;
					}
					isHoliday=true;
					break;
				}
				for(int j=0;j<ColCount;j++) {
					curOp=Operatories.ListShort[ApptViewItems.VisOps[j]];
					if(curOp.ProvDentist!=0 && !curOp.IsHygiene) {//dentist
						schedForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Provider,curOp.ProvDentist);
					}
					else if(curOp.ProvHygienist!=0 && curOp.IsHygiene) {//hygienist
						schedForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Provider,curOp.ProvHygienist);
					}
					else {//no provider set
						schedForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Provider,PrefB.GetInt("ScheduleProvUnassigned"));
					}
					if(isHoliday) {
						g.FillRectangle(holidayBrush,TimeWidth+ProvWidth*ProvCount+j*ColWidth,0,ColWidth,Height);
					}
					else{
						for(int i=0;i<schedForType.Length;i++) {
							g.FillRectangle(openBrush
								,TimeWidth+ProvWidth*ProvCount+j*ColWidth
								,schedForType[i].StartTime.Hour*Lh*RowsPerHr+(int)schedForType[i].StartTime.Minute*Lh/MinPerRow//6RowsPerHr 10MinPerRow
								,ColWidth
								,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*Lh*RowsPerHr//6
							+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*Lh/MinPerRow);//10
						}
					}
				}
			}			
		}

		///<summary>Draws all the blockouts for the entire period.</summary>
		private void DrawBlockouts(Graphics g){
			Schedule[] schedForType;
			schedForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Blockout,0);
			SolidBrush blockBrush;
			Pen blockOutlinePen=new Pen(Color.Black,1);
			Pen penOutline;
			Font blockFont=new Font("Arial",8);
			string blockText;
			RectangleF rect;
			//g.TextRenderingHint=TextRenderingHint.SingleBitPerPixelGridFit;//to make printing clearer
			for(int i=0;i<schedForType.Length;i++){	
				blockBrush=new SolidBrush(DefB.GetColor(DefCat.BlockoutTypes,schedForType[i].BlockoutType));
				penOutline = new Pen(DefB.GetColor(DefCat.BlockoutTypes, schedForType[i].BlockoutType),2);
				blockText=DefB.GetName(DefCat.BlockoutTypes,schedForType[i].BlockoutType)+"\r\n"+schedForType[i].Note;
				if(IsWeeklyView){
					if(schedForType[i].Op==0) {//all ops
						rect=new RectangleF(
							TimeWidth+1+((int)schedForType[i].SchedDate.DayOfWeek-1)*ColDayWidth
							,schedForType[i].StartTime.Hour*Lh*RowsPerHr//6
							+schedForType[i].StartTime.Minute*Lh/MinPerRow//10
							,ColDayWidth-1
							,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*Lh*RowsPerHr
							+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*Lh/MinPerRow);
					}
					else {//just one op
						if(ApptViewItems.GetIndexOp(schedForType[i].Op)==-1) {
							continue;//don't display if op not visible
						}
						rect=new RectangleF(
							TimeWidth+1+((int)schedForType[i].SchedDate.DayOfWeek-1)*ColDayWidth
							+ColAptWidth*ApptViewItems.GetIndexOp(schedForType[i].Op)+1
							,schedForType[i].StartTime.Hour*Lh*RowsPerHr
							+schedForType[i].StartTime.Minute*Lh/MinPerRow
							,ColAptWidth-1
							,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*Lh*RowsPerHr
							+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*Lh/MinPerRow);
					}
				}
				else{
					if(schedForType[i].Op==0){//all ops
						rect=new RectangleF(
							TimeWidth+ProvWidth*ProvCount+1
							,schedForType[i].StartTime.Hour*Lh*RowsPerHr//6
							+schedForType[i].StartTime.Minute*Lh/MinPerRow//10
							,ColWidth*ColCount-1
							,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*Lh*RowsPerHr
							+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*Lh/MinPerRow);
					}
					else{//just one op
						if(ApptViewItems.GetIndexOp(schedForType[i].Op)==-1){
							continue;//don't display if op not visible
						}
						rect=new RectangleF(
							TimeWidth+ProvWidth*ProvCount
							+ColWidth*ApptViewItems.GetIndexOp(schedForType[i].Op)+1
							,schedForType[i].StartTime.Hour*Lh*RowsPerHr
							+schedForType[i].StartTime.Minute*Lh/MinPerRow
							,ColWidth-1
							,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*Lh*RowsPerHr
							+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*Lh/MinPerRow);
					}
				}
				//paint either solid block or outline
				if(PrefB.GetBool("SolidBlockouts")){
					g.FillRectangle(blockBrush,rect);
					g.DrawLine(blockOutlinePen,rect.X,rect.Y+1,rect.Right-1,rect.Y+1);
				}
				else{
					g.DrawRectangle(penOutline,rect.X+1,rect.Y+2,rect.Width-2,rect.Height-3);
				}				
				g.DrawString(blockText,blockFont,new SolidBrush(DefB.Short[(int)DefCat.AppointmentColors][5].ItemColor),rect);
			}         
		}

		///<summary>The background provider schedule for the timebar on the left</summary>
		private void DrawProvSchedInTimebar(Graphics g){
			Provider provCur;
			//SchedDefault[] schedDefs;//for one type at a time
			Schedule[] schedForType;
			for(int j=0;j<ApptViewItems.VisProvs.Length;j++){
				provCur=Providers.List[ApptViewItems.VisProvs[j]];
				schedForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Provider,provCur.ProvNum);
				for(int i=0;i<schedForType.Length;i++){	
					g.FillRectangle(openBrush
						,TimeWidth+ProvWidth*j
						,schedForType[i].StartTime.Hour*Lh*RowsPerHr//6
						+(int)schedForType[i].StartTime.Minute*Lh/MinPerRow//10
						,ProvWidth
						,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*Lh*RowsPerHr//6
						+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*Lh/MinPerRow);//10
				}         
			}
		}

		///<summary>Not the schedule, but just the indicators of scheduling in the ops.</summary>
		private void DrawProvTimebar(Graphics g){			
			for(int j=0;j<ContrApptSingle.ProvBar.Length;j++){
				for(int i=0;i<24*RowsPerHr;i++){
					//144;i++){//ContrApptSingle.TimeBar.Length;i++){
					switch(ContrApptSingle.ProvBar[j][i]){
						case 0:
							break;
						case 1:
							try{
								g.FillRectangle(new SolidBrush(Providers.List[ApptViewItems.VisProvs[j]].ProvColor)
									,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							}
							catch{//design-time
								g.FillRectangle(new SolidBrush(Color.White)
									,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							}
							break;
						case 2:
							g.FillRectangle(new HatchBrush(HatchStyle.DarkUpwardDiagonal
								,Color.Black,Providers.List[ApptViewItems.VisProvs[j]].ProvColor)
								,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							break;
						default://more than 2
							g.FillRectangle(new SolidBrush(Color.Black)
								,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							break;
					}
				}
			}
		}

		///<summary></summary>
		private void DrawGridLines(Graphics g){
			//Vert
			if(IsWeeklyView){
				g.DrawLine(new Pen(Color.DarkGray),0,0,0,Height);
				g.DrawLine(new Pen(Color.White),TimeWidth-1,0,TimeWidth-1,Height);
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth,0,TimeWidth,Height);
				for(int d=0;d<NumOfWeekDaysToDisplay;d++){
					g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ColDayWidth*d,0
						,TimeWidth+ColDayWidth*d,Height);
				}
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ColDayWidth*NumOfWeekDaysToDisplay,0
					,TimeWidth+1+ColDayWidth*NumOfWeekDaysToDisplay,Height);
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth*2+ColDayWidth*NumOfWeekDaysToDisplay,0
					,TimeWidth*2+1+ColDayWidth*NumOfWeekDaysToDisplay,Height);
			}
			else{
				g.DrawLine(new Pen(Color.DarkGray),0,0,0,Height);
				g.DrawLine(new Pen(Color.White),TimeWidth-2,0,TimeWidth-2,Height);
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth-1,0,TimeWidth-1,Height);
				for(int i=0;i<ProvCount;i++){
					g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ProvWidth*i,0,TimeWidth+ProvWidth*i,Height);
				}
				for(int i=0;i<ColCount;i++){
					g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ProvWidth*ProvCount+ColWidth*i,0
						,TimeWidth+ProvWidth*ProvCount+ColWidth*i,Height);
				}
				g.DrawLine(new Pen(Color.DarkGray), TimeWidth+ProvWidth*ProvCount+ColWidth*ColCount,0
					,TimeWidth+ProvWidth*ProvCount+ColWidth*ColCount,Height);
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,0
					,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,Height);
			}
			//horiz gray
			for(int i=0;i<(Height);i+=Lh*RowsPerIncr){
				g.DrawLine(new Pen(Color.LightGray),TimeWidth,i
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i);
			}
			//horiz Hour lines
			for(int i=0;i<Height;i+=Lh*RowsPerHr){
				g.DrawLine(new Pen(Color.LightGray),0,i-1//was white
					,TimeWidth*2+ColWidth*ColCount+ProvWidth*ProvCount,i-1);
				g.DrawLine(new Pen(Color.DarkSlateGray),0,i,TimeWidth,i);
				g.DrawLine(new Pen(Color.Black),TimeWidth,i
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i);
				g.DrawLine(new Pen(Color.DarkSlateGray),TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i
					,TimeWidth*2+ColWidth*ColCount+ProvWidth*ProvCount,i);
			}
		}

		///<summary></summary>
		private void DrawRedTimeIndicator(Graphics g){
			int curTimeY=(int)(DateTime.Now.Hour*Lh*RowsPerHr+DateTime.Now.Minute/60f*(float)Lh*RowsPerHr);
			g.DrawLine(new Pen(Color.Red),0,curTimeY
				,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,curTimeY);
			g.DrawLine(new Pen(Color.Red),0,curTimeY+1
				,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,curTimeY+1);
		}

		/// <summary></summary>
		private void DrawMinutes(Graphics g){
			Font font=new Font(FontFamily.GenericSansSerif,8);//was msSans
			Font bfont=new Font(FontFamily.GenericSansSerif,8,FontStyle.Bold);//was Arial
			g.TextRenderingHint=TextRenderingHint.SingleBitPerPixelGridFit;//to make printing clearer
			DateTime hour;
			CultureInfo ci=(CultureInfo)CultureInfo.CurrentCulture.Clone();
			string hFormat=Lan.GetShortTimeFormat(ci);
			string sTime;
			for(int i=0;i<24;i++){
				hour=new DateTime(2000,1,1,i,0,0);//hour is the only important part of this time.
				sTime=hour.ToString(hFormat,ci);
				SizeF sizef=g.MeasureString(sTime,bfont);
				g.DrawString(sTime,bfont,new SolidBrush(Color.Black),TimeWidth-sizef.Width-2,i*Lh*RowsPerHr+1);
				g.DrawString(sTime,bfont,new SolidBrush(Color.Black)
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+1);
				if(MinPerIncr==10){
					g.DrawString(":10",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr);
					g.DrawString(":20",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*2);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*3);
					g.DrawString(":40",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*4);
					g.DrawString(":50",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*5);
					g.DrawString(":10",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr);
					g.DrawString(":20",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*2);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*3);
					g.DrawString(":40",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*4);
					g.DrawString(":50",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*5);
				}
				else{//15
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*2);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*3);
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*2);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*3);
				}
			}
		}

		///<summary></summary>
		public void DrawShadow(){
			if(Shadow!=null){
				Graphics grfx2=this.CreateGraphics();
				grfx2.DrawImage(Shadow,0,0);
				grfx2.Dispose();
			}
		}

		///<summary>Called from ContrAppt.comboView_SelectedIndexChanged and ContrAppt.RefreshVisops. So, whenever appt Module layout and when comboView is changed.</summary>
		public void ComputeColWidth(int totalWidth){
			if(ApptViewItems.VisOps==null || ApptViewItems.VisProvs==null){
				return;
			}
			try{
				if(RowsPerIncr==0)
					RowsPerIncr=1;
				ColCount=ApptViewItems.VisOps.Length;
				if(IsWeeklyView){
					//ColCount=NumOfWeekDaysToDisplay;
					ProvCount=0;
				}
				else{
					ProvCount=ApptViewItems.VisProvs.Length;
				}
				if(ColCount==0) {
					ColWidth=0;
				}
				else {
					if(IsWeeklyView){
						ColDayWidth=(totalWidth-TimeWidth*2)/NumOfWeekDaysToDisplay;
						ColAptWidth=(float)(ColDayWidth-1)/(float)ColCount;
						ColWidth=(totalWidth-TimeWidth*2-ProvWidth*ProvCount)/ColCount;
					}
					else{
						ColWidth=(totalWidth-TimeWidth*2-ProvWidth*ProvCount)/ColCount;
					}
				}
				MinPerIncr=PrefB.GetInt("AppointmentTimeIncrement");
				MinPerRow=(float)MinPerIncr/(float)RowsPerIncr;
				RowsPerHr=60/MinPerIncr*RowsPerIncr;
				//if(TwoRowsPerIncrement){
					//MinPerRow=MinPerRow/2;
					//RowsPerHr=RowsPerHr*2;
				//}
				Height=Lh*24*RowsPerHr;
				if(IsWeeklyView){
					Width=TimeWidth*2+ColDayWidth*NumOfWeekDaysToDisplay;
				}
				else{
					Width=TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount;
				}
			}
			catch{
				MessageBox.Show("error computing width");
			}
		}
		


	}
}
