/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDentBusiness.UI;

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
		///<summary>Line height.  This is currently treated like a constant that the user has no control over.</summary>
		public static int Lh;
		///<summary>The number of columns.  Stays consistent even if weekly view.  The number of colums showing for one day.</summary>
		public static int ColCount;
		///<summary></summary>
		public static int ProvCount;
		///<summary>Based on the view.  If no view, then it is set to 1. Different computers can be showing different views.</summary>
		public static int RowsPerIncr;
		///<summary>Pulled from Prefs AppointmentTimeIncrement.  Either 5, 10, or 15. An increment can be one or more rows.</summary>
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
		//private SolidBrush openBrush;
		//private SolidBrush closedBrush;
		//private SolidBrush holidayBrush;
		///<summary>This gets set externally each time the module is selected.  It is the background schedule for the entire period.  Includes all types.</summary>
		public List<Schedule> SchedListPeriod;
		public static bool IsWeeklyView;
		///<summary>Typically 5 or 7. Only used with weekview.</summary>
		public static int NumOfWeekDaysToDisplay=7;
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
				if(Shadow!=null) {
					Shadow.Dispose();
					Shadow=null;
				}
				//if(openBrush!=null) {
				//  openBrush.Dispose();
				//}
				//if(closedBrush!=null) {
				//  closedBrush.Dispose();
				//}
				//if(holidayBrush!=null) {
				//  holidayBrush.Dispose();
				//}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		private void InitializeComponent(){
			this.SuspendLayout();
			// 
			// ContrApptSheet
			// 
			this.Name = "ContrApptSheet";
			this.Size = new System.Drawing.Size(482,540);
			this.Load += new System.EventHandler(this.ContrApptSheet_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrApptSheet_Layout);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ContrApptSheet_MouseMove);
			this.ResumeLayout(false);

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
		public static int XPosToOpIdx(int xPos) {
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
				if(retVal>ApptViewItemL.VisOps.Count-1 && dayI<NumOfWeekDaysToDisplay-1) {
					retVal=0;
				}
			}
			else{
				retVal=(int)Math.Round((double)(newX-TimeWidth-ProvWidth*ProvCount)/ColWidth);
			}
			//make sure it's not outside bounds of array:
			if(retVal > ApptViewItemL.VisOps.Count-1)
				retVal=ApptViewItemL.VisOps.Count-1;
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
			//Debug.WriteLine("DrawShadow:"+DateTime.Now.ToLongTimeString());
			DrawShadow();
		}

		///<summary></summary>
		public void CreateShadow(){
			if(Shadow!=null){
				Shadow.Dispose();
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
			using(Graphics g=Graphics.FromImage(Shadow)) {
				ApptDrawing.DrawAllButAppts(g,Lh,RowsPerIncr,MinPerIncr,RowsPerHr,MinPerRow,TimeWidth,ColCount,
					ColWidth,ColDayWidth,Width,Height,ProvWidth,ProvCount,ColAptWidth,IsWeeklyView,NumOfWeekDaysToDisplay,
					SchedListPeriod,ApptViewItemL.VisProvs,ApptViewItemL.VisOps,ContrApptSingle.ProvBar,new DateTime(2011,1,1,0,0,0),new DateTime(2011,1,1,0,0,0),true);
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
			if(ApptViewItemL.VisOps==null || ApptViewItemL.VisProvs==null){
				return;
			}
			try{
				if(RowsPerIncr==0)
					RowsPerIncr=1;
				ColCount=ApptViewItemL.VisOps.Count;
				if(IsWeeklyView){
					//ColCount=NumOfWeekDaysToDisplay;
					ProvCount=0;
				}
				else{
					ProvCount=ApptViewItemL.VisProvs.Count;
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
				MinPerIncr=PrefC.GetInt(PrefName.AppointmentTimeIncrement);
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

		private void ContrApptSheet_MouseMove(object sender,MouseEventArgs e) {
			//Debug.WriteLine("ContrApptSheet_MouseMove:"+DateTime.Now.ToLongTimeString()+", loc:"+e.Location.ToString());
		}

		//protected override void OnMouseMove(MouseEventArgs e) {
		//	Debug.WriteLine("ContrApptSheet_MouseMove:"+DateTime.Now.ToLongTimeString()+", sender:"+);
		//	base.OnMouseMove(e);
		//}

	}
}
