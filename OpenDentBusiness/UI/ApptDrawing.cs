using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Text;

namespace OpenDentBusiness.UI {
	public class ApptDrawing {
		///<summary>Set on mouse down or from Appt module</summary>
		public static long ClickedAptNum;
		/// <summary>This is not the best place for this, but changing it now would cause bugs.  Set manually</summary>
		public static long SelectedAptNum;
		/////<summary>True if this control is on the pinboard</summary>
		//public bool ThisIsPinBoard;
		///<summary>Stores the shading info for the provider bars on the left of the appointments module</summary>
		public static int[][] ProvBar;
		///<summary>Stores the background bitmap for this control</summary>
		public Bitmap Shadow;
		///<summary>This is a datarow that stores most of the info necessary to draw appt.  It comes from the table obtained in Appointments.GetPeriodApptsTable().</summary>
		public DataRow DataRoww;
		///<summary>This table contains all appointment fields for all appointments in the period. It's obtained in Appointments.GetApptFields().</summary>
		public DataTable TableApptFields;
		///<summary>This table contains all appointment fields for all appointments in the period. It's obtained in Appointments.GetApptFields().</summary>
		public DataTable TablePatFields;
		///<summary>Indicator that account has procedures with no ins claim</summary>
		public bool FamHasInsNotSent;
		///<Summary>Will show the highlight around the edges.  For now, this is only used for pinboard.  The ordinary selected appt is set with SelectedAptNum.</Summary>
		public bool IsSelected;
		///<summary>The width of each operatory.</summary>
		public static float ColWidth;
		///<summary></summary>
		public static float TimeWidth;
		///<summary></summary>
		public static float ProvWidth;
		///<summary>Line height.  This is currently treated like a constant that the user has no control over.</summary>
		public static float LineH;
		///<summary>The number of columns.  Stays consistent even if weekly view.  The number of colums showing for one day.</summary>
		public static float ColCount;
		///<summary></summary>
		public static float ProvCount;
		///<summary>Based on the view.  If no view, then it is set to 1. Different computers can be showing different views.</summary>
		public static float RowsPerIncr;
		///<summary>Pulled from Prefs AppointmentTimeIncrement.  Either 5, 10, or 15. An increment can be one or more rows.</summary>
		public static float MinPerIncr;
		///<summary>Typical values would be 10,15,5,or 7.5.</summary>
		public static float MinPerRow;
		///<summary>Rows per hour, based on RowsPerIncr and MinPerIncr</summary>
		public static float RowsPerHr;
		///<summary></summary>
		public bool IsScrolling=false;
		///<summary>This gets set externally each time the module is selected.  It is the background schedule for the entire period.  Includes all types.</summary>
		public List<Schedule> SchedListPeriod;
		///<summary></summary>
		public static bool IsWeeklyView;
		///<summary>Typically 5 or 7. Only used with weekview.</summary>
		public static float NumOfWeekDaysToDisplay=7;
		///<summary>The width of an entire day if using week view.</summary>
		public static float ColDayWidth;
		///<summary>Only used with weekview. The width of individual appointments within each day.  There might be rounding errors for now.</summary>
		public static float ColAptWidth;

		///<summary>Draws the entire Appt background.  Used for main Appt module, for printing, and for mobile app.</summary>
		public static void DrawAllButAppts(Graphics g,int fontSize,int lineH,int rowsPerIncr,int rowsPerHr,int minPerIncr,float minPerRow,int colWidth,int colDayWidth,float colAptWidth,int colCount,int timeWidth,int totalWidth,int totalHeight,int provWidth,int provCount,int numOfWeekDaysToDisplay,bool showRedTimeLine,bool isWeeklyView,int[][] provBar,List<Provider> visProvs,List<Operatory> visOps,List<Schedule> schedListPeriod,DateTime startTime,DateTime stopTime) 
		{
			g.FillRectangle(new SolidBrush(Color.LightGray),0,0,timeWidth,totalHeight);//L time bar
			g.FillRectangle(new SolidBrush(Color.LightGray),timeWidth+colWidth*colCount+provWidth*provCount,0,timeWidth,totalHeight);//R time bar
			DrawMainBackground(g,lineH,rowsPerHr,minPerRow,timeWidth,colCount,colWidth,colDayWidth,totalHeight,provWidth,provCount,colAptWidth,isWeeklyView,numOfWeekDaysToDisplay,schedListPeriod,visOps);
			DrawBlockouts(g,fontSize,lineH,rowsPerHr,minPerRow,timeWidth,colWidth,colDayWidth,provWidth,provCount,colAptWidth,isWeeklyView,schedListPeriod,visOps,startTime,stopTime);
			if(!isWeeklyView) {
				DrawProvScheds(g,lineH,rowsPerHr,timeWidth,provWidth,minPerRow,visProvs,schedListPeriod,startTime,stopTime);
				DrawProvBars(g,lineH,rowsPerHr,timeWidth,provWidth,provBar,visProvs,startTime,stopTime);
			}
			DrawGridLines(g,lineH,rowsPerHr,timeWidth,colWidth,colCount,colDayWidth,provWidth,provCount,rowsPerIncr,totalHeight,numOfWeekDaysToDisplay,isWeeklyView);
			if(showRedTimeLine) {
				DrawRedTimeIndicator(g,lineH,rowsPerHr,timeWidth,colWidth,colCount,provWidth,provCount);
			}
			DrawMinutes(g,lineH,rowsPerHr,timeWidth,colWidth,colCount,provWidth,provCount,minPerIncr,rowsPerIncr,startTime,stopTime);
		}

		///<summary>Including the practice schedule.</summary>
		public static void DrawMainBackground(Graphics g,int lineH,int rowsPerHr,float minPerRow,int timeWidth,int colCount,int colWidth,int colDayWidth,int totalHeight,int provWidth,int provCount,float colAptWidth,bool isWeeklyView,int numOfWeekDaysToDisplay,List<Schedule> schedListPeriod,List<Operatory> visOps) {
			Brush openBrush;
			Brush closedBrush;
			Brush holidayBrush;
			try {
				openBrush=new SolidBrush(DefC.Long[(int)DefCat.AppointmentColors][0].ItemColor);
				closedBrush=new SolidBrush(DefC.Long[(int)DefCat.AppointmentColors][1].ItemColor);
				holidayBrush=new SolidBrush(DefC.Long[(int)DefCat.AppointmentColors][4].ItemColor);
			}
			catch {//this is just for design-time
				openBrush=new SolidBrush(Color.White);
				closedBrush=new SolidBrush(Color.LightGray);
				holidayBrush=new SolidBrush(Color.FromArgb(255,128,128));
			}
			List<Schedule> schedsForOp;
			//one giant rectangle for everything closed
			g.FillRectangle(closedBrush,timeWidth,0,colWidth*colCount+provWidth*provCount,totalHeight);
			//then, loop through each day and operatory
			//Operatory curOp;
			bool isHoliday;
			if(isWeeklyView) {
				for(int d=0;d<numOfWeekDaysToDisplay;d++) {
					isHoliday=false;
					for(int i=0;i<schedListPeriod.Count;i++) {
						if(schedListPeriod[i].SchedType!=ScheduleType.Practice) {
							continue;
						}
						if(schedListPeriod[i].Status!=SchedStatus.Holiday) {
							continue;
						}
						if((int)schedListPeriod[i].SchedDate.DayOfWeek!=d+1) {
							continue;
						}
						isHoliday=true;
						break;
					}
					if(isHoliday) {
						g.FillRectangle(holidayBrush,timeWidth+1+d*colDayWidth,0,colDayWidth,totalHeight);
					}
					//this is a workaround because we start on Monday:
					DayOfWeek dayofweek;
					if(d==6) {
						dayofweek=(DayOfWeek)(0);
					}
					else {
						dayofweek=(DayOfWeek)(d+1);
					}
					for(int j=0;j<colCount;j++) {
						schedsForOp=Schedules.GetSchedsForOp(schedListPeriod,dayofweek,visOps[j]);//OperatoryC.ListShort[ApptViewItemL.visOps[j]]);
						for(int i=0;i<schedsForOp.Count;i++) {
							g.FillRectangle(openBrush
								,timeWidth+1+d*colDayWidth+(float)j*colAptWidth
								,schedsForOp[i].StartTime.Hours*lineH*rowsPerHr+(int)schedsForOp[i].StartTime.Minutes*lineH/minPerRow//6RowsPerHr 10MinPerRow
								,colAptWidth
								,(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Hours*lineH*rowsPerHr//6
								+(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Minutes*lineH/minPerRow);//10
						}
					}
				}
			}
			else {//only one day showing
				isHoliday=false;
				//Schedule[] schedForType;
				for(int i=0;i<schedListPeriod.Count;i++) {
					if(schedListPeriod[i].SchedType!=ScheduleType.Practice) {
						continue;
					}
					if(schedListPeriod[i].Status!=SchedStatus.Holiday) {
						continue;
					}
					isHoliday=true;
					break;
				}
				if(isHoliday) {
					g.FillRectangle(holidayBrush,timeWidth+1,0,colWidth*colCount+provWidth*provCount,totalHeight);
				}
				for(int j=0;j<colCount;j++) {
					if(j==visOps.Count) {//For printing.  This allows printing blank columns on the last page if not enough providers to fill columns.
						break;
					}
					schedsForOp=Schedules.GetSchedsForOp(schedListPeriod,visOps[j]);//OperatoryC.ListShort[ApptViewItemL.visOps[j]]);
					//first, do all the backgrounds
					for(int i=0;i<schedsForOp.Count;i++) {
						g.FillRectangle(openBrush
							,timeWidth+provWidth*provCount+j*colWidth
							,schedsForOp[i].StartTime.Hours*lineH*rowsPerHr+(int)schedsForOp[i].StartTime.Minutes*lineH/minPerRow//6RowsPerHr 10MinPerRow
							,colWidth
							,(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Hours*lineH*rowsPerHr//6
							+(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Minutes*lineH/minPerRow);//10
					}
					//now, fill up to 2 timebars along the left side of each rectangle.
					for(int i=0;i<schedsForOp.Count;i++) {
						if(schedsForOp[i].Ops.Count==0) {//if this schedule is not assigned to specific ops, skip
							continue;
						}
						if(!Providers.GetIsSec(schedsForOp[i].ProvNum)) {//if the provider is a dentist
							g.FillRectangle(new SolidBrush(Providers.GetColor(schedsForOp[i].ProvNum))
								,timeWidth+provWidth*provCount+j*colWidth
								,schedsForOp[i].StartTime.Hours*lineH*rowsPerHr+(int)schedsForOp[i].StartTime.Minutes*lineH/minPerRow//6RowsPerHr 10MinPerRow
								,provWidth
								,(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Hours*lineH*rowsPerHr//6
								+(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Minutes*lineH/minPerRow);//10
						}
						else {//hygienist
							g.FillRectangle(new SolidBrush(Providers.GetColor(schedsForOp[i].ProvNum))
								,timeWidth+provWidth*provCount+j*colWidth+provWidth
								,schedsForOp[i].StartTime.Hours*lineH*rowsPerHr+(int)schedsForOp[i].StartTime.Minutes*lineH/minPerRow//6RowsPerHr 10MinPerRow
								,provWidth
								,(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Hours*lineH*rowsPerHr//6
								+(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Minutes*lineH/minPerRow);//10
						}
					}
				}
			}
			openBrush.Dispose();
			closedBrush.Dispose();
			holidayBrush.Dispose();
		}

		///<summary>Draws all the blockouts for the entire period.</summary>
		public static void DrawBlockouts(Graphics g,int fontSize,int lineH,int rowsPerHr,float minPerRow,int timeWidth,int colWidth,int colDayWidth,int provWidth,int provCount,float colAptWidth,bool isWeeklyView,List<Schedule> schedListPeriod,List<Operatory> visOps,DateTime startTime,DateTime stopTime) {
			Schedule[] schedForType;
			schedForType=Schedules.GetForType(schedListPeriod,ScheduleType.Blockout,0);
			SolidBrush blockBrush;
			Pen blockOutlinePen=new Pen(Color.Black,1);
			Pen penOutline;
			Font blockFont=new Font("Arial",fontSize);
			string blockText;
			RectangleF rect;
			for(int i=0;i<schedForType.Length;i++) {
				blockBrush=new SolidBrush(DefC.GetColor(DefCat.BlockoutTypes,schedForType[i].BlockoutType));
				penOutline = new Pen(DefC.GetColor(DefCat.BlockoutTypes,schedForType[i].BlockoutType),2);
				blockText=DefC.GetName(DefCat.BlockoutTypes,schedForType[i].BlockoutType)+"\r\n"+schedForType[i].Note;
				for(int o=0;o<schedForType[i].Ops.Count;o++) {
					//Put logic here for deciding to draw blockout in time frame.
					//int stopHour=stopTime.Hour;
					//if(stopHour==0) {
					//  stopHour=24;
					//}
					//if(aptDateTime.Hour>=startTime.Hour && aptDateTime.Hour<stopHour) {

					if(isWeeklyView) {
						//this is a workaround because we start on Monday:
						int dayofweek=(int)schedForType[i].SchedDate.DayOfWeek-1;
						if(dayofweek==-1) {
							dayofweek=6;
						}
						rect=new RectangleF(
							timeWidth+1+(dayofweek)*colDayWidth
							+colAptWidth*GetIndexOp(schedForType[i].Ops[o],visOps)+1
							,schedForType[i].StartTime.Hours*lineH*rowsPerHr
							+schedForType[i].StartTime.Minutes*lineH/minPerRow
							,colAptWidth-1
							,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*lineH*rowsPerHr
							+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*lineH/minPerRow);
					}
					else {
						rect=new RectangleF(
							timeWidth+provWidth*provCount
							+colWidth*GetIndexOp(schedForType[i].Ops[o],visOps)+1
							+provWidth*2//so they don't overlap prov bars
							,schedForType[i].StartTime.Hours*lineH*rowsPerHr
							+schedForType[i].StartTime.Minutes*lineH/minPerRow
							,colWidth-1-provWidth*2
							,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*lineH*rowsPerHr
							+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*lineH/minPerRow);
					}
					//paint either solid block or outline
					if(PrefC.GetBool(PrefName.SolidBlockouts)) {
						g.FillRectangle(blockBrush,rect);
						g.DrawLine(blockOutlinePen,rect.X,rect.Y+1,rect.Right-1,rect.Y+1);
					}
					else {
						g.DrawRectangle(penOutline,rect.X+1,rect.Y+2,rect.Width-2,rect.Height-3);
					}
					g.DrawString(blockText,blockFont,new SolidBrush(DefC.Short[(int)DefCat.AppointmentColors][5].ItemColor),rect);
				}
				blockBrush.Dispose();
				penOutline.Dispose();
			}
			blockOutlinePen.Dispose();
		}

		///<summary>Returns the index of the opNum within VisOps.  Returns -1 if not in visOps.</summary>
		public static int GetIndexOp(long opNum,List<Operatory> visOps) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<visOps.Count;i++) {
				if(visOps[i].OperatoryNum==opNum)
					return i;
			}
			return -1;
		}
		
		///<summary>The background provider schedules for the provider bars on the left.</summary>
		public static void DrawProvScheds(Graphics g,int lineH,int rowsPerHr,int timeWidth,int provWidth,float minPerRow,List<Provider> visProvs,List<Schedule> schedListPeriod,DateTime startTime,DateTime stopTime) {
			Brush openBrush;
			try {
				openBrush=new SolidBrush(DefC.Long[(int)DefCat.AppointmentColors][0].ItemColor);
			}
			catch {//this is just for design-time
				openBrush=new SolidBrush(Color.White);
			}
			Provider provCur;
			Schedule[] schedForType;
			int stopHour=stopTime.Hour;
			if(stopHour==0) {
				stopHour=24;
			}
			for(int j=0;j<visProvs.Count;j++) {
				provCur=visProvs[j];
				schedForType=Schedules.GetForType(schedListPeriod,ScheduleType.Provider,provCur.ProvNum);
				for(int i=0;i<schedForType.Length;i++) {
					//Put logic here for drawing prov scheds for the time frame.
					//if(schedForType[i].StartTime.Hour>=startTime.Hour) {
					//  continue;
					//}
					g.FillRectangle(openBrush
						,timeWidth+provWidth*j
						,schedForType[i].StartTime.Hours*lineH*rowsPerHr//6
						+(int)schedForType[i].StartTime.Minutes*lineH/minPerRow//10
						,provWidth
						,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*lineH*rowsPerHr//6
						+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*lineH/minPerRow);//10
				}
			}
			openBrush.Dispose();
		}

		///<summary>Not the schedule, but just the indicators of scheduling.</summary>
		public static void DrawProvBars(Graphics g,int lineH,int rowsPerHr,int timeWidth,int provWidth,int[][] provBar,List<Provider> visProvs,DateTime startTime,DateTime stopTime) {
			//Put logic here for deciding how to draw the provider bars within the time frame.
			//int stopHour=stopTime.Hour;
			//if(stopHour==0) {//12AM, but we want to end on the next day so set to 24
			//  stopHour=24;
			//}
			//int index=0;//This will cause drawing times to always start at the top.
			//for(int i=startTime.Hour;i<stopHour;i++)


			for(int j=0;j<provBar.Length;j++) {
				for(int i=0;i<24*rowsPerHr;i++) {
					switch(provBar[j][i]) {
						case 0:
							break;
						case 1:
							try {
								g.FillRectangle(new SolidBrush(visProvs[j].ProvColor)
									,timeWidth+provWidth*j+1,(i*lineH)+1,provWidth-1,lineH-1);
							}
							catch {//design-time
								g.FillRectangle(new SolidBrush(Color.White)
									,timeWidth+provWidth*j+1,(i*lineH)+1,provWidth-1,lineH-1);
							}
							break;
						case 2:
							g.FillRectangle(new HatchBrush(HatchStyle.DarkUpwardDiagonal
								,Color.Black,visProvs[j].ProvColor)
								,timeWidth+provWidth*j+1,(i*lineH)+1,provWidth-1,lineH-1);
							break;
						default://more than 2
							g.FillRectangle(new SolidBrush(Color.Black)
								,timeWidth+provWidth*j+1,(i*lineH)+1,provWidth-1,lineH-1);
							break;
					}
				}
			}
		}

		///<summary></summary>
		public static void DrawGridLines(Graphics g,int lineH,int rowsPerHr,int timeWidth,int colWidth,int colCount,int colDayWidth,int provWidth,int provCount,int rowsPerIncr,int totalHeight,int numOfWeekDaysToDisplay,bool isWeeklyView) {
			//Vert
			if(isWeeklyView) {
				g.DrawLine(new Pen(Color.DarkGray),0,0,0,totalHeight);
				g.DrawLine(new Pen(Color.White),timeWidth-1,0,timeWidth-1,totalHeight);
				g.DrawLine(new Pen(Color.DarkGray),timeWidth,0,timeWidth,totalHeight);
				for(int d=0;d<numOfWeekDaysToDisplay;d++) {
					g.DrawLine(new Pen(Color.DarkGray),timeWidth+colDayWidth*d,0
						,timeWidth+colDayWidth*d,totalHeight);
				}
				g.DrawLine(new Pen(Color.DarkGray),timeWidth+colDayWidth*numOfWeekDaysToDisplay,0
					,timeWidth+1+colDayWidth*numOfWeekDaysToDisplay,totalHeight);
				g.DrawLine(new Pen(Color.DarkGray),timeWidth*2+colDayWidth*numOfWeekDaysToDisplay,0
					,timeWidth*2+1+colDayWidth*numOfWeekDaysToDisplay,totalHeight);
			}
			else {
				g.DrawLine(new Pen(Color.DarkGray),0,0,0,totalHeight);
				g.DrawLine(new Pen(Color.White),timeWidth-2,0,timeWidth-2,totalHeight);
				g.DrawLine(new Pen(Color.DarkGray),timeWidth-1,0,timeWidth-1,totalHeight);
				for(int i=0;i<provCount;i++) {
					g.DrawLine(new Pen(Color.DarkGray),timeWidth+provWidth*i,0,timeWidth+provWidth*i,totalHeight);
				}
				for(int i=0;i<colCount;i++) {
					g.DrawLine(new Pen(Color.DarkGray),timeWidth+provWidth*provCount+colWidth*i,0
						,timeWidth+provWidth*provCount+colWidth*i,totalHeight);
				}
				g.DrawLine(new Pen(Color.DarkGray),timeWidth+provWidth*provCount+colWidth*colCount,0
					,timeWidth+provWidth*provCount+colWidth*colCount,totalHeight);
				g.DrawLine(new Pen(Color.DarkGray),timeWidth*2+provWidth*provCount+colWidth*colCount,0
					,timeWidth*2+provWidth*provCount+colWidth*colCount,totalHeight);
			}
			//horiz gray
			for(int i=0;i<(totalHeight);i+=lineH*rowsPerIncr) {
				g.DrawLine(new Pen(Color.LightGray),timeWidth,i
					,timeWidth+colWidth*colCount+provWidth*provCount,i);
			}
			//horiz Hour lines
			for(int i=0;i<totalHeight;i+=lineH*rowsPerHr) {
				g.DrawLine(new Pen(Color.LightGray),0,i-1//was white
					,timeWidth*2+colWidth*colCount+provWidth*provCount,i-1);
				g.DrawLine(new Pen(Color.DarkSlateGray),0,i,timeWidth,i);
				g.DrawLine(new Pen(Color.Black),timeWidth,i
					,timeWidth+colWidth*colCount+provWidth*provCount,i);
				g.DrawLine(new Pen(Color.DarkSlateGray),timeWidth+colWidth*colCount+provWidth*provCount,i
					,timeWidth*2+colWidth*colCount+provWidth*provCount,i);
			}
		}

		///<summary></summary>
		public static void DrawRedTimeIndicator(Graphics g,int lineH,int rowsPerHr,int timeWidth,int colWidth,int colCount,int provWidth,int provCount) {
			int curTimeY=(int)(DateTime.Now.Hour*lineH*rowsPerHr+DateTime.Now.Minute/60f*(float)lineH*rowsPerHr);
			g.DrawLine(new Pen(Color.Red),0,curTimeY
				,timeWidth*2+provWidth*provCount+colWidth*colCount,curTimeY);
			g.DrawLine(new Pen(Color.Red),0,curTimeY+1
				,timeWidth*2+provWidth*provCount+colWidth*colCount,curTimeY+1);
		}

		///<summary></summary>
		public static void DrawMinutes(Graphics g,int lineH,int rowsPerHr,int timeWidth,int colWidth,int colCount,int provWidth,int provCount,int minPerIncr,int rowsPerIncr,DateTime startTime,DateTime stopTime) {
			Font font=new Font(FontFamily.GenericSansSerif,8);//was msSans
			Font bfont=new Font(FontFamily.GenericSansSerif,8,FontStyle.Bold);//was Arial
			g.TextRenderingHint=TextRenderingHint.SingleBitPerPixelGridFit;//to make printing clearer
			DateTime hour;
			CultureInfo ci=(CultureInfo)CultureInfo.CurrentCulture.Clone();
			string hFormat=Lans.GetShortTimeFormat(ci);
			string sTime;
			int stop=stopTime.Hour;
			if(stop==0) {//12AM, but we want to end on the next day so set to 24
				stop=24;
			}
			int index=0;//This will cause drawing times to always start at the top.
			for(int i=startTime.Hour;i<stop;i++) {
				hour=new DateTime(2000,1,1,i,0,0);//hour is the only important part of this time.
				sTime=hour.ToString(hFormat,ci);
				SizeF sizef=g.MeasureString(sTime,bfont);
				g.DrawString(sTime,bfont,new SolidBrush(Color.Black),timeWidth-sizef.Width-2,index*lineH*rowsPerHr+1);
				g.DrawString(sTime,bfont,new SolidBrush(Color.Black)
					,timeWidth+colWidth*colCount+provWidth*provCount,index*lineH*rowsPerHr+1);
				if(minPerIncr==5) {
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,timeWidth-19,index*lineH*rowsPerHr+lineH*rowsPerIncr*3);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,timeWidth-19,index*lineH*rowsPerHr+lineH*rowsPerIncr*6);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,timeWidth-19,index*lineH*rowsPerHr+lineH*rowsPerIncr*9);
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,index*lineH*rowsPerHr+lineH*rowsPerIncr*3);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,index*lineH*rowsPerHr+lineH*rowsPerIncr*6);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,index*lineH*rowsPerHr+lineH*rowsPerIncr*9);
				}
				else if(minPerIncr==10) {
					g.DrawString(":10",font,new SolidBrush(Color.Black)
						,timeWidth-19,index*lineH*rowsPerHr+lineH*rowsPerIncr);
					g.DrawString(":20",font,new SolidBrush(Color.Black)
						,timeWidth-19,index*lineH*rowsPerHr+lineH*rowsPerIncr*2);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,timeWidth-19,index*lineH*rowsPerHr+lineH*rowsPerIncr*3);
					g.DrawString(":40",font,new SolidBrush(Color.Black)
						,timeWidth-19,index*lineH*rowsPerHr+lineH*rowsPerIncr*4);
					g.DrawString(":50",font,new SolidBrush(Color.Black)
						,timeWidth-19,index*lineH*rowsPerHr+lineH*rowsPerIncr*5);
					g.DrawString(":10",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,index*lineH*rowsPerHr+lineH*rowsPerIncr);
					g.DrawString(":20",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,index*lineH*rowsPerHr+lineH*rowsPerIncr*2);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,index*lineH*rowsPerHr+lineH*rowsPerIncr*3);
					g.DrawString(":40",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,index*lineH*rowsPerHr+lineH*rowsPerIncr*4);
					g.DrawString(":50",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,index*lineH*rowsPerHr+lineH*rowsPerIncr*5);
				}
				else {//15
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,timeWidth-19,index*lineH*rowsPerHr+lineH*rowsPerIncr);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,timeWidth-19,index*lineH*rowsPerHr+lineH*rowsPerIncr*2);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,timeWidth-19,index*lineH*rowsPerHr+lineH*rowsPerIncr*3);
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,index*lineH*rowsPerHr+lineH*rowsPerIncr);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,index*lineH*rowsPerHr+lineH*rowsPerIncr*2);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,index*lineH*rowsPerHr+lineH*rowsPerIncr*3);
				}
				index++;
			}
		}

		///<summary></summary>
		public static int ComputeColWidth(int totalWidth,int colsPerPage,int timeWidth,int provWidth,int provCount) {
			if(colsPerPage<1) {
			  return 0;
			}
			return (totalWidth-timeWidth*2-provWidth*provCount)/colsPerPage;
		}
		
		///<summary></summary>
		public static int ComputeColDayWidth(int totalWidth,int timeWidth,int numOfWeekDaysToDisplay) {
			return (totalWidth-timeWidth*2)/numOfWeekDaysToDisplay;
		}
		
		///<summary></summary>
		public static float ComputeColAptWidth(int colDayWidth,int colsPerPage) {
			return (float)(colDayWidth-1)/(float)colsPerPage;
		}
		
		///<summary></summary>
		public static int ComputeLineHeight(int fontSize) {
			Font baseFont=new Font("Arial",fontSize);
			return baseFont.Height;
		}

	}
}