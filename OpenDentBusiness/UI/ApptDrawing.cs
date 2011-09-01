using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace OpenDentBusiness.UI {
	public class ApptDrawing {
		///<summary>Stores the shading info for the provider bars on the left of the appointments module</summary>
		public static int[][] ProvBar;
		///<summary>The width of each operatory.</summary>
		public static float ColWidth;
		///<summary></summary>
		public static float TimeWidth=37;
		///<summary></summary>
		public static float ProvWidth=8;
		///<summary>Line height.  This is currently treated like a constant that the user has no control over.</summary>
		public static int LineH=12;
		///<summary>The number of columns.  Stays consistent even if weekly view.  The number of colums showing for one day.</summary>
		public static int ColCount;
		///<summary></summary>
		public static int ProvCount;
		///<summary>Based on the view.  If no view, then it is set to 1. Different computers can be showing different views.</summary>
		public static int RowsPerIncr;
		///<summary>Pulled from Prefs AppointmentTimeIncrement.  Either 5, 10, or 15. An increment can be one or more rows.</summary>
		public static int MinPerIncr;
		///<summary>Typical values would be 10,15,5,or 7.5.</summary>
		public static int MinPerRow;
		///<summary>Rows per hour, based on RowsPerIncr and MinPerIncr</summary>
		public static int RowsPerHr;
		///<summary>This gets set externally each time the module is selected.  It is the background schedule for the entire period.  Includes all types.</summary>
		public static List<Schedule> SchedListPeriod;
		///<summary></summary>
		public static bool IsWeeklyView;
		///<summary>Typically 5 or 7. Only used with weekview.</summary>
		public static int NumOfWeekDaysToDisplay=7;
		///<summary>The width of an entire day if using week view.</summary>
		public static float ColDayWidth;
		///<summary>Only used with weekview. The width of individual appointments within each day.  There might be rounding errors for now.</summary>
		public static float ColAptWidth;
		//these two are subsets of provs and ops. You can't include hidden prov or op in this list.
		///<summary>Visible provider bars in appt module.  This is a subset of the available provs.  You can't include a hidden prov in this list.</summary>
		public static List<Provider> VisProvs;
		///<summary>Visible ops in appt module.  List of visible operatories.  This is a subset of the available ops.  You can't include a hidden op in this list.  If user has set View.OnlyScheduledProvs, and not isWeekly, then the only ops to show will be for providers that have schedules for the day and ops with no provs assigned.</summary>
		public static List<Operatory> VisOps;
		public static float ApptSheetHeight;//Temp name so I don't mix up Heights.
		public static float ApptSheetWidth;//Temp name so I don't mix up Widths.

		///<summary>Draws the entire Appt background.  Used for main Appt module, for printing, and for mobile app.</summary>
		public static void DrawAllButAppts(Graphics g,float totalHeight,int fontSize,bool showRedTimeLine,DateTime startTime,DateTime stopTime) 
		{
			g.FillRectangle(new SolidBrush(Color.LightGray),0,0,TimeWidth,totalHeight);//L time bar
			g.FillRectangle(new SolidBrush(Color.LightGray),TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,0,TimeWidth,totalHeight);//R time bar
			DrawMainBackground(g,totalHeight);
			DrawBlockouts(g,fontSize,startTime,stopTime);
			if(!IsWeeklyView) {
				DrawProvScheds(g,startTime,stopTime);
				DrawProvBars(g,startTime,stopTime);
			}
			DrawGridLines(g,totalHeight);
			if(showRedTimeLine) {
				DrawRedTimeIndicator(g);
			}
			DrawMinutes(g,startTime,stopTime);
		}

		///<summary>Including the practice schedule.</summary>
		public static void DrawMainBackground(Graphics g,float totalHeight) {
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
			g.FillRectangle(closedBrush,TimeWidth,0,ColWidth*ColCount+ProvWidth*ProvCount,totalHeight);
			//then, loop through each day and operatory
			//Operatory curOp;
			bool isHoliday;
			if(IsWeeklyView) {
				for(int d=0;d<NumOfWeekDaysToDisplay;d++) {
					isHoliday=false;
					for(int i=0;i<SchedListPeriod.Count;i++) {
						if(SchedListPeriod[i].SchedType!=ScheduleType.Practice) {
							continue;
						}
						if(SchedListPeriod[i].Status!=SchedStatus.Holiday) {
							continue;
						}
						if((int)SchedListPeriod[i].SchedDate.DayOfWeek!=d+1) {
							continue;
						}
						isHoliday=true;
						break;
					}
					if(isHoliday) {
						g.FillRectangle(holidayBrush,TimeWidth+1+d*ColDayWidth,0,ColDayWidth,totalHeight);
					}
					//this is a workaround because we start on Monday:
					DayOfWeek dayofweek;
					if(d==6) {
						dayofweek=(DayOfWeek)(0);
					}
					else {
						dayofweek=(DayOfWeek)(d+1);
					}
					for(int j=0;j<ColCount;j++) {
						schedsForOp=Schedules.GetSchedsForOp(SchedListPeriod,dayofweek,VisOps[j]);//OperatoryC.ListShort[ApptViewItemL.VisOps[j]]);
						for(int i=0;i<schedsForOp.Count;i++) {
							g.FillRectangle(openBrush
								,TimeWidth+1+d*ColDayWidth+(float)j*ColAptWidth
								,schedsForOp[i].StartTime.Hours*LineH*RowsPerHr+(int)schedsForOp[i].StartTime.Minutes*LineH/MinPerRow//6RowsPerHr 10MinPerRow
								,ColAptWidth
								,(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Hours*LineH*RowsPerHr//6
								+(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Minutes*LineH/MinPerRow);//10
						}
					}
				}
			}
			else {//only one day showing
				isHoliday=false;
				//Schedule[] schedForType;
				for(int i=0;i<SchedListPeriod.Count;i++) {
					if(SchedListPeriod[i].SchedType!=ScheduleType.Practice) {
						continue;
					}
					if(SchedListPeriod[i].Status!=SchedStatus.Holiday) {
						continue;
					}
					isHoliday=true;
					break;
				}
				if(isHoliday) {
					g.FillRectangle(holidayBrush,TimeWidth+1,0,ColWidth*ColCount+ProvWidth*ProvCount,totalHeight);
				}
				for(int j=0;j<ColCount;j++) {
					if(j==VisOps.Count) {//For printing.  This allows printing blank columns on the last page if not enough providers to fill columns.
						break;
					}
					schedsForOp=Schedules.GetSchedsForOp(SchedListPeriod,VisOps[j]);//OperatoryC.ListShort[ApptViewItemL.VisOps[j]]);
					//first, do all the backgrounds
					for(int i=0;i<schedsForOp.Count;i++) {
						g.FillRectangle(openBrush
							,TimeWidth+ProvWidth*ProvCount+j*ColWidth
							,schedsForOp[i].StartTime.Hours*LineH*RowsPerHr+(int)schedsForOp[i].StartTime.Minutes*LineH/MinPerRow//6RowsPerHr 10MinPerRow
							,ColWidth
							,(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Hours*LineH*RowsPerHr//6
							+(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Minutes*LineH/MinPerRow);//10
					}
					//now, fill up to 2 timebars along the left side of each rectangle.
					for(int i=0;i<schedsForOp.Count;i++) {
						if(schedsForOp[i].Ops.Count==0) {//if this schedule is not assigned to specific ops, skip
							continue;
						}
						if(!Providers.GetIsSec(schedsForOp[i].ProvNum)) {//if the provider is a dentist
							g.FillRectangle(new SolidBrush(Providers.GetColor(schedsForOp[i].ProvNum))
								,TimeWidth+ProvWidth*ProvCount+j*ColWidth
								,schedsForOp[i].StartTime.Hours*LineH*RowsPerHr+(int)schedsForOp[i].StartTime.Minutes*LineH/MinPerRow//6RowsPerHr 10MinPerRow
								,ProvWidth
								,(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Hours*LineH*RowsPerHr//6
								+(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Minutes*LineH/MinPerRow);//10
						}
						else {//hygienist
							g.FillRectangle(new SolidBrush(Providers.GetColor(schedsForOp[i].ProvNum))
								,TimeWidth+ProvWidth*ProvCount+j*ColWidth+ProvWidth
								,schedsForOp[i].StartTime.Hours*LineH*RowsPerHr+(int)schedsForOp[i].StartTime.Minutes*LineH/MinPerRow//6RowsPerHr 10MinPerRow
								,ProvWidth
								,(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Hours*LineH*RowsPerHr//6
								+(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Minutes*LineH/MinPerRow);//10
						}
					}
				}
			}
			openBrush.Dispose();
			closedBrush.Dispose();
			holidayBrush.Dispose();
		}

		///<summary>Draws all the blockouts for the entire period.</summary>
		public static void DrawBlockouts(Graphics g,int fontSize,DateTime startTime,DateTime stopTime) {
			Schedule[] schedForType;
			schedForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Blockout,0);
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

					if(IsWeeklyView) {
						//this is a workaround because we start on Monday:
						int dayofweek=(int)schedForType[i].SchedDate.DayOfWeek-1;
						if(dayofweek==-1) {
							dayofweek=6;
						}
						rect=new RectangleF(
							TimeWidth+1+(dayofweek)*ColDayWidth
							+ColAptWidth*GetIndexOp(schedForType[i].Ops[o],VisOps)+1
							,schedForType[i].StartTime.Hours*LineH*RowsPerHr
							+schedForType[i].StartTime.Minutes*LineH/MinPerRow
							,ColAptWidth-1
							,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*LineH*RowsPerHr
							+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*LineH/MinPerRow);
					}
					else {
						if(GetIndexOp(schedForType[i].Ops[o])==-1){
							continue;//don't display if op not visible
						}
						rect=new RectangleF(
							TimeWidth+ProvWidth*ProvCount
							+ColWidth*GetIndexOp(schedForType[i].Ops[o],VisOps)+1
							+ProvWidth*2//so they don't overlap prov bars
							,schedForType[i].StartTime.Hours*LineH*RowsPerHr
							+schedForType[i].StartTime.Minutes*LineH/MinPerRow
							,ColWidth-1-ProvWidth*2
							,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*LineH*RowsPerHr
							+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*LineH/MinPerRow);
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

		///<summary>Returns the index of the opNum within VisOps.  Returns -1 if not in VisOps.</summary>
		public static int GetIndexOp(long opNum,List<Operatory> VisOps) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<VisOps.Count;i++) {
				if(VisOps[i].OperatoryNum==opNum)
					return i;
			}
			return -1;
		}
		
		///<summary>The background provider schedules for the provider bars on the left.</summary>
		public static void DrawProvScheds(Graphics g,DateTime startTime,DateTime stopTime) {
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
			for(int j=0;j<VisProvs.Count;j++) {
				provCur=VisProvs[j];
				schedForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Provider,provCur.ProvNum);
				for(int i=0;i<schedForType.Length;i++) {
					//Put logic here for drawing prov scheds for the time frame.
					//if(schedForType[i].StartTime.Hour>=startTime.Hour) {
					//  continue;
					//}
					g.FillRectangle(openBrush
						,TimeWidth+ProvWidth*j
						,schedForType[i].StartTime.Hours*LineH*RowsPerHr//6
						+(int)schedForType[i].StartTime.Minutes*LineH/MinPerRow//10
						,ProvWidth
						,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*LineH*RowsPerHr//6
						+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*LineH/MinPerRow);//10
				}
			}
			openBrush.Dispose();
		}

		///<summary>Not the schedule, but just the indicators of scheduling.</summary>
		public static void DrawProvBars(Graphics g,DateTime startTime,DateTime stopTime) {
			//Put logic here for deciding how to draw the provider bars within the time frame.
			//int stopHour=stopTime.Hour;
			//if(stopHour==0) {//12AM, but we want to end on the next day so set to 24
			//  stopHour=24;
			//}
			//int index=0;//This will cause drawing times to always start at the top.
			//for(int i=startTime.Hour;i<stopHour;i++)


			for(int j=0;j<ProvBar.Length;j++) {
				for(int i=0;i<24*RowsPerHr;i++) {
					switch(ProvBar[j][i]) {
						case 0:
							break;
						case 1:
							try {
								g.FillRectangle(new SolidBrush(VisProvs[j].ProvColor)
									,TimeWidth+ProvWidth*j+1,(i*LineH)+1,ProvWidth-1,LineH-1);
							}
							catch {//design-time
								g.FillRectangle(new SolidBrush(Color.White)
									,TimeWidth+ProvWidth*j+1,(i*LineH)+1,ProvWidth-1,LineH-1);
							}
							break;
						case 2:
							g.FillRectangle(new HatchBrush(HatchStyle.DarkUpwardDiagonal
								,Color.Black,VisProvs[j].ProvColor)
								,TimeWidth+ProvWidth*j+1,(i*LineH)+1,ProvWidth-1,LineH-1);
							break;
						default://more than 2
							g.FillRectangle(new SolidBrush(Color.Black)
								,TimeWidth+ProvWidth*j+1,(i*LineH)+1,ProvWidth-1,LineH-1);
							break;
					}
				}
			}
		}

		///<summary></summary>
		public static void DrawGridLines(Graphics g,float totalHeight) {
			//Vert
			if(IsWeeklyView) {
				g.DrawLine(new Pen(Color.DarkGray),0,0,0,totalHeight);
				g.DrawLine(new Pen(Color.White),TimeWidth-1,0,TimeWidth-1,totalHeight);
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth,0,TimeWidth,totalHeight);
				for(int d=0;d<NumOfWeekDaysToDisplay;d++) {
					g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ColDayWidth*d,0
						,TimeWidth+ColDayWidth*d,totalHeight);
				}
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ColDayWidth*NumOfWeekDaysToDisplay,0
					,TimeWidth+1+ColDayWidth*NumOfWeekDaysToDisplay,totalHeight);
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth*2+ColDayWidth*NumOfWeekDaysToDisplay,0
					,TimeWidth*2+1+ColDayWidth*NumOfWeekDaysToDisplay,totalHeight);
			}
			else {
				g.DrawLine(new Pen(Color.DarkGray),0,0,0,totalHeight);
				g.DrawLine(new Pen(Color.White),TimeWidth-2,0,TimeWidth-2,totalHeight);
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth-1,0,TimeWidth-1,totalHeight);
				for(int i=0;i<ProvCount;i++) {
					g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ProvWidth*i,0,TimeWidth+ProvWidth*i,totalHeight);
				}
				for(int i=0;i<ColCount;i++) {
					g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ProvWidth*ProvCount+ColWidth*i,0
						,TimeWidth+ProvWidth*ProvCount+ColWidth*i,totalHeight);
				}
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ProvWidth*ProvCount+ColWidth*ColCount,0
					,TimeWidth+ProvWidth*ProvCount+ColWidth*ColCount,totalHeight);
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,0
					,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,totalHeight);
			}
			//horiz gray
			for(int i=0;i<(totalHeight);i+=LineH*RowsPerIncr) {
				g.DrawLine(new Pen(Color.LightGray),TimeWidth,i
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i);
			}
			//horiz Hour lines
			for(int i=0;i<totalHeight;i+=LineH*RowsPerHr) {
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
		public static void DrawRedTimeIndicator(Graphics g) {
			int curTimeY=(int)(DateTime.Now.Hour*LineH*RowsPerHr+DateTime.Now.Minute/60f*(float)LineH*RowsPerHr);
			g.DrawLine(new Pen(Color.Red),0,curTimeY
				,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,curTimeY);
			g.DrawLine(new Pen(Color.Red),0,curTimeY+1
				,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,curTimeY+1);
		}

		///<summary></summary>
		public static void DrawMinutes(Graphics g,DateTime startTime,DateTime stopTime) {
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
				g.DrawString(sTime,bfont,new SolidBrush(Color.Black),TimeWidth-sizef.Width-2,index*LineH*RowsPerHr+1);
				g.DrawString(sTime,bfont,new SolidBrush(Color.Black)
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,index*LineH*RowsPerHr+1);
				if(MinPerIncr==5) {
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,TimeWidth-19,index*LineH*RowsPerHr+LineH*RowsPerIncr*3);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth-19,index*LineH*RowsPerHr+LineH*RowsPerIncr*6);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,TimeWidth-19,index*LineH*RowsPerHr+LineH*RowsPerIncr*9);
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,index*LineH*RowsPerHr+LineH*RowsPerIncr*3);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,index*LineH*RowsPerHr+LineH*RowsPerIncr*6);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,index*LineH*RowsPerHr+LineH*RowsPerIncr*9);
				}
				else if(MinPerIncr==10) {
					g.DrawString(":10",font,new SolidBrush(Color.Black)
						,TimeWidth-19,index*LineH*RowsPerHr+LineH*RowsPerIncr);
					g.DrawString(":20",font,new SolidBrush(Color.Black)
						,TimeWidth-19,index*LineH*RowsPerHr+LineH*RowsPerIncr*2);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth-19,index*LineH*RowsPerHr+LineH*RowsPerIncr*3);
					g.DrawString(":40",font,new SolidBrush(Color.Black)
						,TimeWidth-19,index*LineH*RowsPerHr+LineH*RowsPerIncr*4);
					g.DrawString(":50",font,new SolidBrush(Color.Black)
						,TimeWidth-19,index*LineH*RowsPerHr+LineH*RowsPerIncr*5);
					g.DrawString(":10",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,index*LineH*RowsPerHr+LineH*RowsPerIncr);
					g.DrawString(":20",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,index*LineH*RowsPerHr+LineH*RowsPerIncr*2);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,index*LineH*RowsPerHr+LineH*RowsPerIncr*3);
					g.DrawString(":40",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,index*LineH*RowsPerHr+LineH*RowsPerIncr*4);
					g.DrawString(":50",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,index*LineH*RowsPerHr+LineH*RowsPerIncr*5);
				}
				else {//15
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,TimeWidth-19,index*LineH*RowsPerHr+LineH*RowsPerIncr);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth-19,index*LineH*RowsPerHr+LineH*RowsPerIncr*2);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,TimeWidth-19,index*LineH*RowsPerHr+LineH*RowsPerIncr*3);
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,index*LineH*RowsPerHr+LineH*RowsPerIncr);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,index*LineH*RowsPerHr+LineH*RowsPerIncr*2);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,index*LineH*RowsPerHr+LineH*RowsPerIncr*3);
				}
				index++;
			}
		}

		///<summary></summary>
		public static float ComputeColWidth(float totalWidth,int colsPerPage) {
		  if(colsPerPage<1) {
		    return 0;
		  }
		  return (totalWidth-TimeWidth*2-ProvWidth*ProvCount)/colsPerPage;
		}
		
		///<summary></summary>
		public static float ComputeColDayWidth(float totalWidth) {
			return (totalWidth-TimeWidth*2)/NumOfWeekDaysToDisplay;
		}
		
		///<summary></summary>
		public static float ComputeColAptWidth(float ColDayWidth,int colsPerPage) {
			return (float)(ColDayWidth-1)/(float)colsPerPage;
		}
		
		///<summary></summary>
		public static int ComputeLineHeight(int fontSize) {
			Font baseFont=new Font("Arial",fontSize);
			return baseFont.Height;
		}



		#region PulledOut

		//ContrApptSheet.cs line 147
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

		//ContrApptSheet.cs line 164
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
		
		//ContrApptSheet.cs line 177
		///<summary>Called when mouse down anywhere on apptSheet. Automatically rounds down.</summary>
		public static int YPosToHour(int yPos){
			int retVal=yPos/LineH/RowsPerHr;//newY/LineH/6;
			return retVal;
		}
		
		//ContrApptSheet.cs line 183
		///<summary>Called when mouse down anywhere on apptSheet. This will give very precise minutes. It is not rounded for accuracy.</summary>
		public static int YPosToMin(int yPos){
			int hourPortion=YPosToHour(yPos)*LineH*RowsPerHr;
			float MinPerPixel=60/(float)LineH/(float)RowsPerHr;
			int minutes=(int)((yPos-hourPortion)*MinPerPixel);
			return minutes;
		}
		
		//ContrApptSheet.cs line 191
		///<summary>Used when dropping an appointment to a new location.  Converts x-coordinate to operatory index of ApptCatItems.VisOps, rounding to the nearest.  In this respect it is very different from XPosToOp.</summary>
		public static int ConvertToOp(int newX){
			int retVal=0;
			if(IsWeeklyView){
				int dayI=XPosToDay(newX);//does not round
				int deltaDay=dayI*(int)ColDayWidth;
				int adjustedX=newX-(int)TimeWidth-deltaDay;
				retVal=(int)Math.Round((double)(adjustedX)/ColAptWidth);
				//when there are multiple days, special situation where x is within the last op for the day, so it goes to next day.
				if(retVal>VisOps.Count-1 && dayI<NumOfWeekDaysToDisplay-1) {
					retVal=0;
				}
			}
			else{
				retVal=(int)Math.Round((double)(newX-TimeWidth-ProvWidth*ProvCount)/ColWidth);
			}
			//make sure it's not outside bounds of array:
			if(retVal > VisOps.Count-1)
				retVal=VisOps.Count-1;
			if(retVal<0)
				retVal=0;
			return retVal;
		}
		
		//ContrApptSheet.cs line 215
		///<summary>Used when dropping an appointment to a new location.  Converts x-coordinate to day index.  Only used in weekly view.</summary>
		public static int ConvertToDay(int newX) {
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
		
		//ContrApptSheet.cs line 230
		///<summary>Used when dropping an appointment to a new location. Rounds to the nearest increment.</summary>
		public static int ConvertToHour(int newY){
			//return (int)((newY+LineH/2)/6/LineH);
			return (int)(((double)newY+(double)LineH*(double)RowsPerIncr/2)/(double)RowsPerHr/(double)LineH);
		}
		
		//ContrApptSheet.cs line 236
		///<summary>Used when dropping an appointment to a new location. Rounds to the nearest increment.</summary>
		public static int ConvertToMin(int newY){
			//int retVal=(int)(Decimal.Remainder(newY,6*LineH)/LineH)*10;
			//first, add pixels equivalent to 1/2 increment: newY+LineH*RowsPerIncr/2
			//Yloc     Height     Rows      1
			//---- + ( ------ x --------- x - )
			//  1       Row     Increment   2
			//then divide by height per hour: RowsPerHr*LineH
			//Rows   Height
			//---- * ------
			//Hour    Row
			int pixels=(int)Decimal.Remainder(
				(decimal)newY+(decimal)LineH*(decimal)RowsPerIncr/2
				,(decimal)RowsPerHr*(decimal)LineH);
			//We are only interested in the remainder, and this is called pixels.
			//Convert pixels to increments. Round down to nearest increment when converting to int.
			//pixels/LineH/RowsPerIncr:
			//pixels    Rows    Increment
			//------ x ------ x ---------
			//  1      pixels     Rows
			int increments=(int)((double)pixels/(double)LineH/(double)RowsPerIncr);
			//Convert increments to minutes: increments*MinPerIncr
      int retVal=increments*MinPerIncr;
			if(retVal==60)
				return 0;
			return retVal;
		}

		//ContrApptSheet.cs line 299
		///<summary>Called from ContrAppt.comboView_SelectedIndexChanged and ContrAppt.RefreshVisops. So, whenever appt Module layout and when comboView is changed.</summary>
		public static void ComputeColWidth(int totalWidth){
		  if(VisOps==null || VisProvs==null){
		    return;
		  }
		  try{
		    if(RowsPerIncr==0)
		      RowsPerIncr=1;
		    ColCount=VisOps.Count;
		    if(IsWeeklyView){
		      //ColCount=NumOfWeekDaysToDisplay;
		      ProvCount=0;
		    }
		    else{
		      ProvCount=VisProvs.Count;
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
		    MinPerRow=MinPerIncr/RowsPerIncr;
		    RowsPerHr=60/MinPerIncr*RowsPerIncr;
		    //if(TwoRowsPerIncrement){
		      //MinPerRow=MinPerRow/2;
		      //RowsPerHr=RowsPerHr*2;
		    //}
		    ApptSheetHeight=LineH*24*RowsPerHr;
		    if(IsWeeklyView){
		      ApptSheetWidth=TimeWidth*2+ColDayWidth*NumOfWeekDaysToDisplay;
		    }
		    else{
		      ApptSheetWidth=TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount;
		    }
		  }
		  catch{
		    MessageBox.Show("error computing width");
		  }
		}

		//ApptViewItemL.cs line 212
		///<summary>Returns the index of the opNum within VisOps.  Returns -1 if not in VisOps.</summary>
		public static int GetIndexOp(long opNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<VisOps.Count;i++) {
				if(VisOps[i].OperatoryNum==opNum)
					return i;
			}
			return -1;
		}

		#endregion



	}
}