using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Text;
using System.Drawing.Drawing2D;

namespace OpenDentBusiness.UI {
	public class ApptDrawing {
		///<summary>Draws the entire Appt background.  Used for main Appt module, for printing, and for mobile app.</summary>
		public static void DrawAllButAppts(Graphics g) {

		}

		///<summary>Only called from the mobile server, not from any workstation.</summary>
		public static Bitmap GetMobileBitmap() {
			//check remoting role?
			return null;
		}

		///<summary>Including the practice schedule.</summary>
		public static void DrawMainBackground(Graphics g,int lineH,int RowsPerHr,int MinPerRow,int TimeWidth,int ColCount,int ColWidth,int ColDayWidth,int Height,int ProvWidth,int ProvCount,int ColAptWidth,bool IsWeeklyView,int NumOfWeekDaysToDisplay,List<Schedule> SchedListPeriod,List<Operatory> VisOps) {
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
			g.FillRectangle(closedBrush,TimeWidth,0,ColWidth*ColCount+ProvWidth*ProvCount,Height);
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
						g.FillRectangle(holidayBrush,TimeWidth+1+d*ColDayWidth,0,ColDayWidth,Height);
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
								,schedsForOp[i].StartTime.Hours*lineH*RowsPerHr+(int)schedsForOp[i].StartTime.Minutes*lineH/MinPerRow//6RowsPerHr 10MinPerRow
								,ColAptWidth
								,(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Hours*lineH*RowsPerHr//6
								+(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Minutes*lineH/MinPerRow);//10
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
					g.FillRectangle(holidayBrush,TimeWidth+1,0,ColWidth*ColCount+ProvWidth*ProvCount,Height);
				}
				for(int j=0;j<ColCount;j++) {
					schedsForOp=Schedules.GetSchedsForOp(SchedListPeriod,VisOps[j]);//OperatoryC.ListShort[ApptViewItemL.VisOps[j]]);
					//first, do all the backgrounds
					for(int i=0;i<schedsForOp.Count;i++) {
						g.FillRectangle(openBrush
							,TimeWidth+ProvWidth*ProvCount+j*ColWidth
							,schedsForOp[i].StartTime.Hours*lineH*RowsPerHr+(int)schedsForOp[i].StartTime.Minutes*lineH/MinPerRow//6RowsPerHr 10MinPerRow
							,ColWidth
							,(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Hours*lineH*RowsPerHr//6
							+(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Minutes*lineH/MinPerRow);//10
					}
					//now, fill up to 2 timebars along the left side of each rectangle.
					for(int i=0;i<schedsForOp.Count;i++) {
						if(schedsForOp[i].Ops.Count==0) {//if this schedule is not assigned to specific ops, skip
							continue;
						}
						if(!Providers.GetIsSec(schedsForOp[i].ProvNum)) {//if the provider is a dentist
							g.FillRectangle(new SolidBrush(Providers.GetColor(schedsForOp[i].ProvNum))
								,TimeWidth+ProvWidth*ProvCount+j*ColWidth
								,schedsForOp[i].StartTime.Hours*lineH*RowsPerHr+(int)schedsForOp[i].StartTime.Minutes*lineH/MinPerRow//6RowsPerHr 10MinPerRow
								,ProvWidth
								,(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Hours*lineH*RowsPerHr//6
								+(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Minutes*lineH/MinPerRow);//10
						}
						else {//hygienist
							g.FillRectangle(new SolidBrush(Providers.GetColor(schedsForOp[i].ProvNum))
								,TimeWidth+ProvWidth*ProvCount+j*ColWidth+ProvWidth
								,schedsForOp[i].StartTime.Hours*lineH*RowsPerHr+(int)schedsForOp[i].StartTime.Minutes*lineH/MinPerRow//6RowsPerHr 10MinPerRow
								,ProvWidth
								,(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Hours*lineH*RowsPerHr//6
								+(schedsForOp[i].StopTime-schedsForOp[i].StartTime).Minutes*lineH/MinPerRow);//10
						}
					}
				}
			}
			openBrush.Dispose();
			closedBrush.Dispose();
			holidayBrush.Dispose();
		}

		///<summary>Draws all the blockouts for the entire period.</summary>
		public static void DrawBlockouts(Graphics g,int Lh,int RowsPerHr,int MinPerRow,int TimeWidth,int ColWidth,int ColDayWidth,int ProvWidth,int ProvCount,int ColAptWidth,bool IsWeeklyView,List<Schedule> SchedListPeriod,List<Operatory> visOps) {
			Schedule[] schedForType;
			schedForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Blockout,0);
			SolidBrush blockBrush;
			Pen blockOutlinePen=new Pen(Color.Black,1);
			Pen penOutline;
			Font blockFont=new Font("Arial",8);
			string blockText;
			RectangleF rect;
			//g.TextRenderingHint=TextRenderingHint.SingleBitPerPixelGridFit;//to make printing clearer
			for(int i=0;i<schedForType.Length;i++) {
				blockBrush=new SolidBrush(DefC.GetColor(DefCat.BlockoutTypes,schedForType[i].BlockoutType));
				penOutline = new Pen(DefC.GetColor(DefCat.BlockoutTypes,schedForType[i].BlockoutType),2);
				blockText=DefC.GetName(DefCat.BlockoutTypes,schedForType[i].BlockoutType)+"\r\n"+schedForType[i].Note;
				for(int o=0;o<schedForType[i].Ops.Count;o++) {
					if(IsWeeklyView) {
						//New list of operatories will always be visible.
						//if(ApptViewItemL.GetIndexOp(schedForType[i].Ops[o])==-1) {
						//  continue;//don't display if op not visible
						//}
						//this is a workaround because we start on Monday:
						int dayofweek=(int)schedForType[i].SchedDate.DayOfWeek-1;
						if(dayofweek==-1) {
							dayofweek=6;
						}
						rect=new RectangleF(
							TimeWidth+1+(dayofweek)*ColDayWidth
							+ColAptWidth*o+1//ApptViewItemL.GetIndexOp(schedForType[i].Ops[o])+1
							,schedForType[i].StartTime.Hours*Lh*RowsPerHr
							+schedForType[i].StartTime.Minutes*Lh/MinPerRow
							,ColAptWidth-1
							,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*Lh*RowsPerHr
							+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*Lh/MinPerRow);
					}
					else {
						//New list of operatories will always be visible.
						//if(ApptViewItemL.GetIndexOp(schedForType[i].Ops[o])==-1) {
						//  continue;//don't display if op not visible
						//}
						rect=new RectangleF(
							TimeWidth+ProvWidth*ProvCount
							+ColWidth*o+1//ApptViewItemL.GetIndexOp(schedForType[i].Ops[o])+1
							+ProvWidth*2//so they don't overlap prov bars
							,schedForType[i].StartTime.Hours*Lh*RowsPerHr
							+schedForType[i].StartTime.Minutes*Lh/MinPerRow
							,ColWidth-1-ProvWidth*2
							,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*Lh*RowsPerHr
							+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*Lh/MinPerRow);
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

		///<summary>The background provider schedules for the provider bars on the left.</summary>
		public static void DrawProvScheds(Graphics g,int Lh,int RowsPerHr,int TimeWidth,int ProvWidth,int MinPerRow,List<Provider> VisProvs,List<Schedule> SchedListPeriod) {
			Brush openBrush;
			try {
				openBrush=new SolidBrush(DefC.Long[(int)DefCat.AppointmentColors][0].ItemColor);
			}
			catch {//this is just for design-time
				openBrush=new SolidBrush(Color.White);
			}
			Provider provCur;
			Schedule[] schedForType;
			for(int j=0;j<VisProvs.Count;j++) {
				provCur=VisProvs[j];
				schedForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Provider,provCur.ProvNum);
				for(int i=0;i<schedForType.Length;i++) {
					g.FillRectangle(openBrush
						,TimeWidth+ProvWidth*j
						,schedForType[i].StartTime.Hours*Lh*RowsPerHr//6
						+(int)schedForType[i].StartTime.Minutes*Lh/MinPerRow//10
						,ProvWidth
						,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*Lh*RowsPerHr//6
						+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*Lh/MinPerRow);//10
				}
			}
			openBrush.Dispose();
		}

		///<summary>Not the schedule, but just the indicators of scheduling.</summary>
		public static void DrawProvBars(Graphics g,int Lh,int RowsPerHr,int TimeWidth,int ProvWidth,int[][] ProvBar,List<Provider> VisProvs) {
			for(int j=0;j<ProvBar.Length;j++) {
				for(int i=0;i<24*RowsPerHr;i++) {
					switch(ProvBar[j][i]) {
						case 0:
							break;
						case 1:
							try {
								g.FillRectangle(new SolidBrush(VisProvs[j].ProvColor)
									,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							}
							catch {//design-time
								g.FillRectangle(new SolidBrush(Color.White)
									,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							}
							break;
						case 2:
							g.FillRectangle(new HatchBrush(HatchStyle.DarkUpwardDiagonal
								,Color.Black,VisProvs[j].ProvColor)
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
		public static void DrawGridLines(Graphics g,int Lh,int RowsPerHr,int TimeWidth,int ColWidth,int ColCount,int ColDayWidth,int ProvWidth,int ProvCount,int RowsPerIncr,int Height,int NumOfWeekDaysToDisplay,bool IsWeeklyView) {
			//Vert
			if(IsWeeklyView) {
				g.DrawLine(new Pen(Color.DarkGray),0,0,0,Height);
				g.DrawLine(new Pen(Color.White),TimeWidth-1,0,TimeWidth-1,Height);
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth,0,TimeWidth,Height);
				for(int d=0;d<NumOfWeekDaysToDisplay;d++) {
					g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ColDayWidth*d,0
						,TimeWidth+ColDayWidth*d,Height);
				}
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ColDayWidth*NumOfWeekDaysToDisplay,0
					,TimeWidth+1+ColDayWidth*NumOfWeekDaysToDisplay,Height);
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth*2+ColDayWidth*NumOfWeekDaysToDisplay,0
					,TimeWidth*2+1+ColDayWidth*NumOfWeekDaysToDisplay,Height);
			}
			else {
				g.DrawLine(new Pen(Color.DarkGray),0,0,0,Height);
				g.DrawLine(new Pen(Color.White),TimeWidth-2,0,TimeWidth-2,Height);
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth-1,0,TimeWidth-1,Height);
				for(int i=0;i<ProvCount;i++) {
					g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ProvWidth*i,0,TimeWidth+ProvWidth*i,Height);
				}
				for(int i=0;i<ColCount;i++) {
					g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ProvWidth*ProvCount+ColWidth*i,0
						,TimeWidth+ProvWidth*ProvCount+ColWidth*i,Height);
				}
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ProvWidth*ProvCount+ColWidth*ColCount,0
					,TimeWidth+ProvWidth*ProvCount+ColWidth*ColCount,Height);
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,0
					,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,Height);
			}
			//horiz gray
			for(int i=0;i<(Height);i+=Lh*RowsPerIncr) {
				g.DrawLine(new Pen(Color.LightGray),TimeWidth,i
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i);
			}
			//horiz Hour lines
			for(int i=0;i<Height;i+=Lh*RowsPerHr) {
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
		public static void DrawRedTimeIndicator(Graphics g,int Lh,int RowsPerHr,int TimeWidth,int ColWidth,int ColCount,int ProvWidth,int ProvCount) {
			int curTimeY=(int)(DateTime.Now.Hour*Lh*RowsPerHr+DateTime.Now.Minute/60f*(float)Lh*RowsPerHr);
			g.DrawLine(new Pen(Color.Red),0,curTimeY
				,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,curTimeY);
			g.DrawLine(new Pen(Color.Red),0,curTimeY+1
				,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,curTimeY+1);
		}

		///<summary></summary>
		public static void DrawMinutes(Graphics g,int Lh,int RowsPerHr,int TimeWidth,int ColWidth,int ColCount,int ProvWidth,int ProvCount,int MinPerIncr,int RowsPerIncr) {
			Font font=new Font(FontFamily.GenericSansSerif,8);//was msSans
			Font bfont=new Font(FontFamily.GenericSansSerif,8,FontStyle.Bold);//was Arial
			g.TextRenderingHint=TextRenderingHint.SingleBitPerPixelGridFit;//to make printing clearer
			DateTime hour;
			CultureInfo ci=(CultureInfo)CultureInfo.CurrentCulture.Clone();
			string hFormat=Lans.GetShortTimeFormat(ci);
			string sTime;
			for(int i=0;i<24;i++) {
				hour=new DateTime(2000,1,1,i,0,0);//hour is the only important part of this time.
				sTime=hour.ToString(hFormat,ci);
				SizeF sizef=g.MeasureString(sTime,bfont);
				g.DrawString(sTime,bfont,new SolidBrush(Color.Black),TimeWidth-sizef.Width-2,i*Lh*RowsPerHr+1);
				g.DrawString(sTime,bfont,new SolidBrush(Color.Black)
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+1);
				if(MinPerIncr==5) {
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*3);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*6);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*9);
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*3);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*6);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*9);
				}
				else if(MinPerIncr==10) {
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
				else {//15
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

	}
}