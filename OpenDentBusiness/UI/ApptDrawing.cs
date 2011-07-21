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
		public static void DrawAllButAppts(Graphics g,int lineH,int rowsPerIncr,int minPerIncr,int rowsPerHr,int minPerRow,int timeWidth,int colCount,int colWidth,int colDayWidth,int totalWidth,int totalHeight,int provWidth,int provCount,int[][] provBar,int colAptWidth,bool isWeeklyView,int numOfWeekDaysToDisplay,List<Schedule> schedListPeriod,List<Provider>visProvs,List<Operatory> visOps) {
			Bitmap shadow=new Bitmap(totalWidth,totalHeight);
			if(rowsPerIncr==0)
				rowsPerIncr=1;
			if(schedListPeriod==null) {
				return;//not sure if this is necessary
			}
			using(Graphics g=Graphics.FromImage(shadow)) {
				//background
				g.FillRectangle(new SolidBrush(Color.LightGray),0,0,timeWidth,totalHeight);//L time bar
				g.FillRectangle(new SolidBrush(Color.LightGray),timeWidth+colWidth*colCount+provWidth*provCount,0,timeWidth,totalHeight);//R time bar
				DrawMainBackground(g,lineH,rowsPerHr,minPerRow,timeWidth,colCount,colWidth,colDayWidth,totalHeight,provWidth,provCount,colAptWidth,isWeeklyView,numOfWeekDaysToDisplay,schedListPeriod,visOps);
				DrawBlockouts(g,lineH,rowsPerHr,minPerRow,timeWidth,colWidth,colDayWidth,provWidth,provCount,colAptWidth,isWeeklyView,schedListPeriod,visOps);
				if(!isWeeklyView) {
				  DrawProvScheds(g,lineH,rowsPerHr,timeWidth,provWidth,minPerRow,visProvs,schedListPeriod);
				  DrawProvBars(g,lineH,rowsPerHr,timeWidth,provWidth,provBar,visProvs);
				}
				DrawGridLines(g,lineH,rowsPerHr,timeWidth,colWidth,colCount,colDayWidth,provWidth,provCount,rowsPerHr,totalHeight,numOfWeekDaysToDisplay,isWeeklyView);
				DrawRedTimeIndicator(g,lineH,rowsPerHr,timeWidth,colWidth,colCount,provWidth,provCount);
				DrawMinutes(g,lineH,rowsPerHr,timeWidth,colWidth,colCount,provWidth,provCount,minPerIncr,rowsPerIncr);
			}
		}

		///<summary>Only called from the mobile server, not from any workstation.</summary>
		public static Bitmap GetMobileBitmap() {
			//check remoting role?
			return null;
		}

		///<summary>Including the practice schedule.</summary>
		public static void DrawMainBackground(Graphics g,int lineH,int rowsPerHr,int minPerRow,int timeWidth,int colCount,int colWidth,int colDayWidth,int totalHeight,int provWidth,int provCount,int colAptWidth,bool isWeeklyView,int numOfWeekDaysToDisplay,List<Schedule> schedListPeriod,List<Operatory> visOps) {
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
		public static void DrawBlockouts(Graphics g,int lineH,int rowsPerHr,int minPerRow,int timeWidth,int colWidth,int colDayWidth,int provWidth,int provCount,int colAptWidth,bool isWeeklyView,List<Schedule> schedListPeriod,List<Operatory> visOps) {
			Schedule[] schedForType;
			schedForType=Schedules.GetForType(schedListPeriod,ScheduleType.Blockout,0);
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
					if(isWeeklyView) {
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
							timeWidth+1+(dayofweek)*colDayWidth
							+colAptWidth*o+1//ApptViewItemL.GetIndexOp(schedForType[i].Ops[o])+1
							,schedForType[i].StartTime.Hours*lineH*rowsPerHr
							+schedForType[i].StartTime.Minutes*lineH/minPerRow
							,colAptWidth-1
							,(schedForType[i].StopTime-schedForType[i].StartTime).Hours*lineH*rowsPerHr
							+(schedForType[i].StopTime-schedForType[i].StartTime).Minutes*lineH/minPerRow);
					}
					else {
						//New list of operatories will always be visible.
						//if(ApptViewItemL.GetIndexOp(schedForType[i].Ops[o])==-1) {
						//  continue;//don't display if op not visible
						//}
						rect=new RectangleF(
							timeWidth+provWidth*provCount
							+colWidth*o+1//ApptViewItemL.GetIndexOp(schedForType[i].Ops[o])+1
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

		///<summary>The background provider schedules for the provider bars on the left.</summary>
		public static void DrawProvScheds(Graphics g,int lineH,int rowsPerHr,int timeWidth,int provWidth,int minPerRow,List<Provider> visProvs,List<Schedule> schedListPeriod) {
			Brush openBrush;
			try {
				openBrush=new SolidBrush(DefC.Long[(int)DefCat.AppointmentColors][0].ItemColor);
			}
			catch {//this is just for design-time
				openBrush=new SolidBrush(Color.White);
			}
			Provider provCur;
			Schedule[] schedForType;
			for(int j=0;j<visProvs.Count;j++) {
				provCur=visProvs[j];
				schedForType=Schedules.GetForType(schedListPeriod,ScheduleType.Provider,provCur.ProvNum);
				for(int i=0;i<schedForType.Length;i++) {
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
		public static void DrawProvBars(Graphics g,int lineH,int rowsPerHr,int timeWidth,int provWidth,int[][] provBar,List<Provider> visProvs) {
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
		public static void DrawMinutes(Graphics g,int lineH,int rowsPerHr,int timeWidth,int colWidth,int colCount,int provWidth,int provCount,int minPerIncr,int rowsPerIncr) {
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
				g.DrawString(sTime,bfont,new SolidBrush(Color.Black),timeWidth-sizef.Width-2,i*lineH*rowsPerHr+1);
				g.DrawString(sTime,bfont,new SolidBrush(Color.Black)
					,timeWidth+colWidth*colCount+provWidth*provCount,i*lineH*rowsPerHr+1);
				if(minPerIncr==5) {
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,timeWidth-19,i*lineH*rowsPerHr+lineH*rowsPerIncr*3);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,timeWidth-19,i*lineH*rowsPerHr+lineH*rowsPerIncr*6);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,timeWidth-19,i*lineH*rowsPerHr+lineH*rowsPerIncr*9);
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,i*lineH*rowsPerHr+lineH*rowsPerIncr*3);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,i*lineH*rowsPerHr+lineH*rowsPerIncr*6);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,i*lineH*rowsPerHr+lineH*rowsPerIncr*9);
				}
				else if(minPerIncr==10) {
					g.DrawString(":10",font,new SolidBrush(Color.Black)
						,timeWidth-19,i*lineH*rowsPerHr+lineH*rowsPerIncr);
					g.DrawString(":20",font,new SolidBrush(Color.Black)
						,timeWidth-19,i*lineH*rowsPerHr+lineH*rowsPerIncr*2);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,timeWidth-19,i*lineH*rowsPerHr+lineH*rowsPerIncr*3);
					g.DrawString(":40",font,new SolidBrush(Color.Black)
						,timeWidth-19,i*lineH*rowsPerHr+lineH*rowsPerIncr*4);
					g.DrawString(":50",font,new SolidBrush(Color.Black)
						,timeWidth-19,i*lineH*rowsPerHr+lineH*rowsPerIncr*5);
					g.DrawString(":10",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,i*lineH*rowsPerHr+lineH*rowsPerIncr);
					g.DrawString(":20",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,i*lineH*rowsPerHr+lineH*rowsPerIncr*2);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,i*lineH*rowsPerHr+lineH*rowsPerIncr*3);
					g.DrawString(":40",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,i*lineH*rowsPerHr+lineH*rowsPerIncr*4);
					g.DrawString(":50",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,i*lineH*rowsPerHr+lineH*rowsPerIncr*5);
				}
				else {//15
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,timeWidth-19,i*lineH*rowsPerHr+lineH*rowsPerIncr);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,timeWidth-19,i*lineH*rowsPerHr+lineH*rowsPerIncr*2);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,timeWidth-19,i*lineH*rowsPerHr+lineH*rowsPerIncr*3);
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,i*lineH*rowsPerHr+lineH*rowsPerIncr);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,i*lineH*rowsPerHr+lineH*rowsPerIncr*2);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,timeWidth+colWidth*colCount+provWidth*provCount,i*lineH*rowsPerHr+lineH*rowsPerIncr*3);
				}
			}
		}

	}
}