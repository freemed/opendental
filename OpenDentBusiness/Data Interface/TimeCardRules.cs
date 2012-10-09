using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Collections;
using System.Globalization;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TimeCardRules{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all TimeCardRules.</summary>
		private static List<TimeCardRule> listt;

		///<summary>A list of all TimeCardRules.</summary>
		public static List<TimeCardRule> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM timecardrule";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="TimeCardRule";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.TimeCardRuleCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<TimeCardRule> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TimeCardRule>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM timecardrule WHERE PatNum = "+POut.Long(patNum);
			return Crud.TimeCardRuleCrud.SelectMany(command);
		}

		///<summary>Gets one TimeCardRule from the db.</summary>
		public static TimeCardRule GetOne(long timeCardRuleNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<TimeCardRule>(MethodBase.GetCurrentMethod(),timeCardRuleNum);
			}
			return Crud.TimeCardRuleCrud.SelectOne(timeCardRuleNum);
		}*/

		///<summary></summary>
		public static long Insert(TimeCardRule timeCardRule){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				timeCardRule.TimeCardRuleNum=Meth.GetLong(MethodBase.GetCurrentMethod(),timeCardRule);
				return timeCardRule.TimeCardRuleNum;
			}
			return Crud.TimeCardRuleCrud.Insert(timeCardRule);
		}

		///<summary></summary>
		public static void Update(TimeCardRule timeCardRule){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),timeCardRule);
				return;
			}
			Crud.TimeCardRuleCrud.Update(timeCardRule);
		}

		///<summary></summary>
		public static void Delete(long timeCardRuleNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),timeCardRuleNum);
				return;
			}
			string command= "DELETE FROM timecardrule WHERE TimeCardRuleNum = "+POut.Long(timeCardRuleNum);
			Db.NonQ(command);
		}

		///<summary>Calculates daily overtime</summary>
		public static void CalculateDailyOvertime(Employee EmployeeCur,DateTime StartDate,DateTime StopDate) {
			if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
				return;
			}
			//Cursor=Cursors.WaitCursor;
			DateTime previousDate;
			List<ClockEvent> ClockEventList=ClockEvents.Refresh(EmployeeCur.EmployeeNum,StartDate,StopDate,false);//PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text),IsBreaks);
			//Over breaks-------------------------------------------------------------------------------------------------
			if(PrefC.GetBool(PrefName.TimeCardsMakesAdjustmentsForOverBreaks)) {
				//set adj auto to zero for all.
				for(int i=0;i<ClockEventList.Count;i++) {
					ClockEventList[i].AdjustAuto=TimeSpan.Zero;
					ClockEvents.Update(ClockEventList[i]);
				}
				List<ClockEvent> breakList=ClockEvents.Refresh(EmployeeCur.EmployeeNum,StartDate,StopDate,true);//PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text),true);
				TimeSpan totalToday=TimeSpan.Zero;
				TimeSpan totalOne=TimeSpan.Zero;
				previousDate=DateTime.MinValue;
				for(int b=0;b<breakList.Count;b++) {
					if(breakList[b].TimeDisplayed2.Year<1880) {
						//Cursor=Cursors.Default;
						//MsgBox.Show(this,"Error. Employee break malformed.");
						throw new Exception("Error. Employee break malformed.");
						//FillMain(true);//in case some changes already made.
						//return;
					}
					if(breakList[b].TimeDisplayed1.Date != breakList[b].TimeDisplayed2.Date) {
						//Cursor=Cursors.Default;
						//MsgBox.Show(this,"Error. One break spans multiple dates.");
						throw new Exception("Error. One break spans multiple dates.");
						//FillMain(true);//in case some changes already made.
						//return;
					}
					//calc time for the one break
					totalOne=breakList[b].TimeDisplayed2-breakList[b].TimeDisplayed1;
					//calc daily total
					if(previousDate.Date != breakList[b].TimeDisplayed1.Date) {//if date changed, this is the first pair of the day
						totalToday=TimeSpan.Zero;//new day
						previousDate=breakList[b].TimeDisplayed1.Date;//for the next loop
					}
					totalToday+=totalOne;
					//decide if breaks for the day went over 30 min.
					if(totalToday > TimeSpan.FromMinutes(31)) {//31 to prevent silly fractions less than 1.
						//loop through all ClockEvents in this grid to find one to adjust.
						//Go backwards to find the last entry for a given date.
						for(int c=ClockEventList.Count-1;c>=0;c--) {
							if(ClockEventList[c].TimeDisplayed1.Date==breakList[b].TimeDisplayed1.Date) {
								ClockEventList[c].AdjustAuto-=(totalToday-TimeSpan.FromMinutes(30));
								ClockEvents.Update(ClockEventList[c]);
								totalToday=TimeSpan.FromMinutes(30);//reset to 30.  Therefore, any additional breaks will be wholly adjustments.
								break;
							}
							if(c==0) {//we never found a match
								//Cursor=Cursors.Default;
								//MessageBox.Show("Error. Over breaks, but could not adjust because not regular time entered for date:"
								//  +breakList[b].TimeDisplayed1.Date.ToShortDateString());
								throw new Exception("Error. Over breaks, but could not adjust because not regular time entered for date:"
								  +breakList[b].TimeDisplayed1.Date.ToShortDateString());
								//FillMain(true);//in case some changes already made.
								//return;
							}
						}
					}
				}
				//FillMain(true);
			}
			//OT-------------------------------------------------------------------------------------------------------------
			TimeSpan afterTime=TimeSpan.Zero;
			TimeSpan beforeTime=TimeSpan.Zero;
			TimeSpan overHours=TimeSpan.Zero;
			//loop through timecardrules to find one rule of each kind.
			for(int i=0;i<TimeCardRules.Listt.Count;i++) {
				if(TimeCardRules.Listt[i].EmployeeNum!=0 && TimeCardRules.Listt[i].EmployeeNum!=EmployeeCur.EmployeeNum) {
					continue;
				}
				if(TimeCardRules.Listt[i].AfterTimeOfDay > TimeSpan.Zero) {
					if(afterTime > TimeSpan.Zero) {//already found a match, and this is a second match
						//Cursor=Cursors.Default;
						//MsgBox.Show(this,"Error.  Multiple matches of AfterTimeOfDay found for this employee.  Only one allowed.");
						throw new Exception("Error.  Multiple matches of AfterTimeOfDay found for this employee.  Only one allowed.");
						//return;
					}
					afterTime=TimeCardRules.Listt[i].AfterTimeOfDay;
				}
				else if(TimeCardRules.Listt[i].OverHoursPerDay > TimeSpan.Zero) {
					if(overHours > TimeSpan.Zero) {//already found a match, and this is a second match
						//Cursor=Cursors.Default;
						//MsgBox.Show(this,"Error.  Multiple matches of OverHoursPerDay found for this employee.  Only one allowed.");
						throw new Exception("Error.  Multiple matches of OverHoursPerDay found for this employee.  Only one allowed.");
						//return;
					}
					overHours=TimeCardRules.Listt[i].OverHoursPerDay;
				}
				if(afterTime > TimeSpan.Zero && overHours > TimeSpan.Zero) {
					//Cursor=Cursors.Default;
					//MsgBox.Show(this,"Error.  Both an OverHoursPerDay and an AfterTimeOfDay found for this employee.  Only one or the other is allowed.");
					throw new Exception("Error.  Both an OverHoursPerDay and an AfterTimeOfDay found for this employee.  Only one or the other is allowed.");
					//return;
				}
				if(beforeTime > TimeSpan.Zero && overHours > TimeSpan.Zero) {
					//Cursor=Cursors.Default;
					//MsgBox.Show(this,"Error.  Both an OverHoursPerDay and an BeforeTimeOfDay found for this employee.  Only one or the other is allowed.");
					throw new Exception("Error.  Both an OverHoursPerDay and an BeforeTimeOfDay found for this employee.  Only one or the other is allowed.");
					//return;
				}
				if(TimeCardRules.Listt[i].BeforeTimeOfDay > TimeSpan.Zero) {
					if(beforeTime>TimeSpan.Zero) {//already found a match, and this is a second match
						//Cursor=Cursors.Default;
						//MsgBox.Show(this,"Error.  Multiple matches of BeforeTimeOfDay found for this employee.  Only one allowed.");
						throw new Exception("Error.  Multiple matches of BeforeTimeOfDay found for this employee.  Only one allowed.");
						//return;
					}
					beforeTime=TimeCardRules.Listt[i].BeforeTimeOfDay;
				}
			}
			//loop through all ClockEvents in this grid.
			TimeSpan dailyTotal=TimeSpan.Zero;
			TimeSpan pairTotal=TimeSpan.Zero;
			previousDate=DateTime.MinValue;
			for(int i=0;i<ClockEventList.Count;i++) {
				if(ClockEventList[i].TimeDisplayed2.Year<1880) {
					//Cursor=Cursors.Default;
					//MsgBox.Show(this,"Error. Employee not clocked out.");
					//FillMain(true);//in case some changes already made.
					throw new Exception("Error. Employee not clocked out.");
					//return;
				}
				if(ClockEventList[i].TimeDisplayed1.Date != ClockEventList[i].TimeDisplayed2.Date) {
					//Cursor=Cursors.Default;
					//MsgBox.Show(this,"Error. One clock pair spans multiple dates.");
					//FillMain(true);//in case some changes already made.
					throw new Exception("Error. One clock pair spans multiple dates.");
					//return;
				}
				pairTotal=ClockEventList[i].TimeDisplayed2-ClockEventList[i].TimeDisplayed1;
				//add any adjustments, manual or overrides.
				if(ClockEventList[i].AdjustIsOverridden) {
					pairTotal+=ClockEventList[i].Adjust;
				}
				else {
					pairTotal+=ClockEventList[i].AdjustAuto;
				}
				//calc daily total
				if(previousDate.Date != ClockEventList[i].TimeDisplayed1.Date) { //if date changed
					dailyTotal=TimeSpan.Zero;//new day
					previousDate=ClockEventList[i].TimeDisplayed1.Date;//for the next loop
				}
				dailyTotal+=pairTotal;
				//handle OT
				ClockEventList[i].OTimeAuto=TimeSpan.Zero;//set auto OT to zero.
				if(ClockEventList[i].OTimeHours != TimeSpan.FromHours(-1)) {//if OT is overridden
					//don't try to calc a time.
					ClockEvents.Update(ClockEventList[i]);//just to possibly clear autoOT, even though it doesn't count.
					//but still need to subtract OT from dailyTotal
					dailyTotal-=ClockEventList[i].OTimeHours;
					continue;
				}
				if(afterTime != TimeSpan.Zero) {
					//test to see if this span is after specified time
					if(ClockEventList[i].TimeDisplayed1.TimeOfDay > afterTime) {//the start time is after time, so the whole pairTotal is OT
						ClockEventList[i].OTimeAuto=pairTotal;
					}
					else if(ClockEventList[i].TimeDisplayed2.TimeOfDay > afterTime) {//only the second time is after time
						ClockEventList[i].OTimeAuto=ClockEventList[i].TimeDisplayed2.TimeOfDay-afterTime;//only a portion of the pairTotal is OT
					}
				}
				if(beforeTime!=TimeSpan.Zero) {
					//test to see if this span is after specified time
					if(ClockEventList[i].TimeDisplayed2.TimeOfDay < beforeTime) {//the end time is before time, so the whole pairTotal is OT
						ClockEventList[i].OTimeAuto+=pairTotal;
					}
					else if(ClockEventList[i].TimeDisplayed1.TimeOfDay < beforeTime) {//only the first time is before time
						ClockEventList[i].OTimeAuto+=beforeTime-ClockEventList[i].TimeDisplayed1.TimeOfDay;//only a portion of the pairTotal is OT
					}
				}
				if(overHours != TimeSpan.Zero) {
					//test dailyTotal
					if(dailyTotal > overHours) {
						ClockEventList[i].OTimeAuto=dailyTotal-overHours;
						dailyTotal=overHours;//e.g. reset to 8.  Any further pairs on this date will be wholly OT
					}
				}
				//TODO: fix negative OT bug. If you work from 9-5 but take a 2 hour lunch, clock out for home. Clock back in 
				//also, there is an issue with putting adjustments on the last time entry of the day if the adjustments are greater than the length of work for the day.
				ClockEvents.Update(ClockEventList[i]);
			}
			AdjustBreaksHelper(EmployeeCur,StartDate,StopDate);
			//FillMain(true);
			//Cursor=Cursors.Default;
		}

		///<summary>This function is aesthetic and has no bearing on actual OT calculations. It adds adjustments to breaks so that when viewing them you can see if they went over 30 minutes.</summary>
		private static void AdjustBreaksHelper(Employee EmployeeCur,DateTime StartDate,DateTime StopDate) {
			if(!PrefC.GetBool(PrefName.TimeCardsMakesAdjustmentsForOverBreaks)){
				//Only adjust breaks if preference is set.
				return;
			}
			List<ClockEvent> breakList=ClockEvents.Refresh(EmployeeCur.EmployeeNum,StartDate,StopDate,true);//PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text),true);
			TimeSpan totalToday=TimeSpan.Zero;
			TimeSpan totalOne=TimeSpan.Zero;
			DateTime previousDate=DateTime.MinValue;
			for(int b=0;b<breakList.Count;b++) {
				if(breakList[b].TimeDisplayed2.Year<1880) {
					//Cursor=Cursors.Default;
					//MsgBox.Show(this,"Error. Employee break malformed.");
					//FillMain(true);//in case some changes already made.
					return;
				}
				if(breakList[b].TimeDisplayed1.Date != breakList[b].TimeDisplayed2.Date) {
					//Cursor=Cursors.Default;
					//MsgBox.Show(this,"Error. One break spans multiple dates.");
					//FillMain(true);//in case some changes already made.
					return;
				}
				//calc time for the one break
				totalOne=breakList[b].TimeDisplayed2-breakList[b].TimeDisplayed1;
				//calc daily total
				if(previousDate.Date != breakList[b].TimeDisplayed1.Date) {//if date changed, this is the first pair of the day
					totalToday=TimeSpan.Zero;//new day
					previousDate=breakList[b].TimeDisplayed1.Date;//for the next loop
				}
				totalToday+=totalOne;
				//decide if breaks for the day went over 30 min.
				if(totalToday > TimeSpan.FromMinutes(31)) {//31 to prevent silly fractions less than 1.
					breakList[b].AdjustAuto=-(totalToday-TimeSpan.FromMinutes(30));
					ClockEvents.Update(breakList[b]);
					totalToday=TimeSpan.FromMinutes(30);//reset to 30.  Therefore, any additional breaks will be wholly adjustments.
				}
			}//end breaklist
			}

		///<summary>Calculates weekly overtime and inserts TimeAdjustments accordingly.</summary>
		public static void CalculateWeeklyOvertime(Employee EmployeeCur,DateTime StartDate,DateTime StopDate) {
			if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
				return;
			}
			List<TimeAdjust> TimeAdjustList=TimeAdjusts.Refresh(EmployeeCur.EmployeeNum,StartDate,StopDate);
			List<ClockEvent> ClockEventList=ClockEvents.Refresh(EmployeeCur.EmployeeNum,StartDate,StopDate,false);
			ArrayList mergedAL = new ArrayList();
			foreach(ClockEvent clockEvent in ClockEventList) {
				mergedAL.Add(clockEvent);
			}
			foreach(TimeAdjust timeAdjust in TimeAdjustList) {
				mergedAL.Add(timeAdjust);
			}
			//first, delete all existing overtime entries
			for(int i=0;i<TimeAdjustList.Count;i++) {
				if(TimeAdjustList[i].OTimeHours==TimeSpan.Zero) {
					continue;
				}
				if(!TimeAdjustList[i].IsAuto) {
					continue;
				}
				TimeAdjusts.Delete(TimeAdjustList[i]);
			}
			//then, fill grid
			//FillMain(true);
			Calendar cal=CultureInfo.CurrentCulture.Calendar;
			CalendarWeekRule rule=CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule;
			List<TimeSpan> WeeklyTotals = new List<TimeSpan>();
			WeeklyTotals = FillWeeklyTotalsHelper(true,EmployeeCur,mergedAL);//,StartDate,StopDate);
			//loop through all rows
			for(int i=0;i<mergedAL.Count;i++) {
				//ignore rows that aren't weekly totals
				if(i<mergedAL.Count-1//if not the last row
					//if the next row has the same week as this row
					&& cal.GetWeekOfYear(GetDateForRowHelper(mergedAL[i+1]),rule,(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek))//Default is 0-Sunday
					== cal.GetWeekOfYear(GetDateForRowHelper(mergedAL[i]),rule,(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek))) {
					continue;
				}
				if(WeeklyTotals[i]<=TimeSpan.FromHours(40)) {
					continue;
				}
				//found a weekly total over 40 hours
				TimeAdjust adjust=new TimeAdjust();
				adjust.IsAuto=true;
				adjust.EmployeeNum=EmployeeCur.EmployeeNum;
				adjust.TimeEntry=GetDateForRowHelper(mergedAL[i]).AddHours(20);//puts it at 8pm on the same day.
				adjust.OTimeHours=WeeklyTotals[i]-TimeSpan.FromHours(40);
				adjust.RegHours=-adjust.OTimeHours;
				TimeAdjusts.Insert(adjust);
			}
			//FillMain(true);

		}

		private static List<TimeSpan> FillWeeklyTotalsHelper(bool fromDB,Employee EmployeeCur,ArrayList mergedAL) {//,DateTime StartTime,DateTime StopTime) {
			List<TimeSpan> retVal = new List<TimeSpan>();
			//This used to be fillGrid()
			//if(fromDB) {
			//List<ClockEvent> ClockEventList=ClockEvents.Refresh(EmployeeCur.EmployeeNum,StartTime,StopTime,false);//PIn.Date(textDateStart.Text),	PIn.Date(textDateStop.Text),IsBreaks);
			//if(IsBreaks) {
			//  TimeAdjustList=new List<TimeAdjust>();
			//}
			//else {
			//List<TimeAdjust> TimeAdjustList=TimeAdjusts.Refresh(EmployeeCur.EmployeeNum,StartTime,StopTime);//PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text));
			//}
			//}
			//ArrayList mergedAL=new ArrayList();
			//for(int i=0;i<ClockEventList.Count;i++) {
			//  mergedAL.Add(ClockEventList[i]);
			//}
			//for(int i=0;i<TimeAdjustList.Count;i++) {
			//  mergedAL.Add(TimeAdjustList[i]);
			//}
			//IComparer myComparer=new ObjectDateComparer();
			//mergedAL.Sort(myComparer);
			//gridMain.BeginUpdate();
			//gridMain.Columns.Clear();
			//ODGridColumn col=new ODGridColumn(Lan.g(this,"Date"),70);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Weekday"),70);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Altered"),50,HorizontalAlignment.Center);
			//gridMain.Columns.Add(col);
			//if(IsBreaks) {
			//  col=new ODGridColumn(Lan.g(this,"Out"),60,HorizontalAlignment.Right);
			//  gridMain.Columns.Add(col);
			//  col=new ODGridColumn(Lan.g(this,"In"),60,HorizontalAlignment.Right);
			//  gridMain.Columns.Add(col);
			//}
			//else {
			//  col=new ODGridColumn(Lan.g(this,"In"),60,HorizontalAlignment.Right);
			//  gridMain.Columns.Add(col);
			//  col=new ODGridColumn(Lan.g(this,"Out"),60,HorizontalAlignment.Right);
			//  gridMain.Columns.Add(col);
			//}
			//col=new ODGridColumn(Lan.g(this,"Total"),50,HorizontalAlignment.Right);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Adjust"),55,HorizontalAlignment.Right);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Overtime"),55,HorizontalAlignment.Right);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Daily"),50,HorizontalAlignment.Right);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Weekly"),50,HorizontalAlignment.Right);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Note"),5);
			//gridMain.Columns.Add(col);
			//gridMain.Rows.Clear();
			//ODGridRow row;
			TimeSpan[] WeeklyTotals=new TimeSpan[mergedAL.Count];
			TimeSpan alteredSpan=new TimeSpan(0);//used to display altered times
			TimeSpan oneSpan=new TimeSpan(0);//used to sum one pair of clock-in/clock-out
			TimeSpan oneAdj;
			TimeSpan oneOT;
			TimeSpan daySpan=new TimeSpan(0);//used for daily totals.
			TimeSpan weekSpan=new TimeSpan(0);//used for weekly totals.
			if(mergedAL.Count>0) {
				weekSpan=ClockEvents.GetWeekTotal(EmployeeCur.EmployeeNum,GetDateForRowHelper(mergedAL[0]));
			}
			TimeSpan periodSpan=new TimeSpan(0);//used to add up totals for entire page.
			TimeSpan otspan=new TimeSpan(0);//overtime for the entire period
			Calendar cal=CultureInfo.CurrentCulture.Calendar;
			CalendarWeekRule rule=CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule;
			DateTime curDate=DateTime.MinValue;
			DateTime previousDate=DateTime.MinValue;
			Type type;
			ClockEvent clock;
			TimeAdjust adjust;
			for(int i=0;i<mergedAL.Count;i++) {
				//row=new ODGridRow();
				type=mergedAL[i].GetType();
				//row.Tag=mergedAL[i];
				previousDate=curDate;
				//clock event row---------------------------------------------------------------------------------------------
				if(type==typeof(ClockEvent)) {
					clock=(ClockEvent)mergedAL[i];
					curDate=clock.TimeDisplayed1.Date;
					//if(curDate==previousDate) {
					//  row.Cells.Add("");
					//  row.Cells.Add("");
					//}
					//else {
					//  row.Cells.Add(curDate.ToShortDateString());
					//  row.Cells.Add(curDate.DayOfWeek.ToString());
					//}
					//altered--------------------------------------
					//string str="";
					//if(clock.TimeEntered1!=clock.TimeDisplayed1) {
					//  if(IsBreaks) {
					//    str=Lan.g(this,"out");
					//  }
					//  else {
					//    str=Lan.g(this,"in");
					//  }
					//}
					//if(clock.TimeEntered2!=clock.TimeDisplayed2) {
					//  if(str!="") {
					//    str+="/";
					//  }
					//  if(IsBreaks) {
					//    str+=Lan.g(this,"in");
					//  }
					//  else {
					//    str+=Lan.g(this,"out");
					//  }
					//}
					//row.Cells.Add(str);
					//status--------------------------------------
					//row.Cells.Add(clock.ClockStatus.ToString());
					//in------------------------------------------
					//row.Cells.Add(clock.TimeDisplayed1.ToShortTimeString());
					//out-----------------------------
					//if(clock.TimeDisplayed2.Year<1880) {
					//  row.Cells.Add("");//not clocked out yet
					//}
					//else {
					//  row.Cells.Add(clock.TimeDisplayed2.ToShortTimeString());
					//}
					//total-------------------------------
					//if(IsBreaks) { //breaks
					//  if(clock.TimeDisplayed2.Year<1880) {
					//    row.Cells.Add("");
					//  }
					//  else {
					//    oneSpan=clock.TimeDisplayed2-clock.TimeDisplayed1;
					//    row.Cells.Add(ClockEvents.Format(oneSpan));
					//    daySpan+=oneSpan;
					//    periodSpan+=oneSpan;
					//  }
					//}
					//else {//regular hours
					if(clock.TimeDisplayed2.Year<1880) {
						//row.Cells.Add("");
					}
					else {
						oneSpan=clock.TimeDisplayed2-clock.TimeDisplayed1;
						//row.Cells.Add(ClockEvents.Format(oneSpan));
						daySpan+=oneSpan;
						weekSpan+=oneSpan;
						periodSpan+=oneSpan;
					}
					//}
					//Adjust---------------------------------
					oneAdj=TimeSpan.Zero;
					if(clock.AdjustIsOverridden) {
						oneAdj+=clock.Adjust;
					}
					else {
						oneAdj+=clock.AdjustAuto;//typically zero
					}
					daySpan+=oneAdj;
					weekSpan+=oneAdj;
					periodSpan+=oneAdj;
					//row.Cells.Add(ClockEvents.Format(oneAdj));
					//Overtime------------------------------
					oneOT=TimeSpan.Zero;
					if(clock.OTimeHours!=TimeSpan.FromHours(-1)) {//overridden
						oneOT=clock.OTimeHours;
					}
					else {
						oneOT=clock.OTimeAuto;//typically zero
					}
					otspan+=oneOT;
					daySpan-=oneOT;
					weekSpan-=oneOT;
					periodSpan-=oneOT;
					//row.Cells.Add(ClockEvents.Format(oneOT));
					//Daily-----------------------------------
					//if this is the last entry for a given date
					if(i==mergedAL.Count-1//if this is the last row
		        || GetDateForRowHelper(mergedAL[i+1]) != curDate)//or the next row is a different date
		      {
						//if(IsBreaks) {
						//  if(clock.TimeDisplayed2.Year<1880) {//if they have not clocked back in yet from break
						//    //display the timespan of oneSpan using current time as the other number.
						//    oneSpan=DateTime.Now-clock.TimeDisplayed1+TimeDelta;
						//    row.Cells.Add(oneSpan.ToStringHmmss());
						//    daySpan+=oneSpan;
						//  }
						//  else {
						//    row.Cells.Add(ClockEvents.Format(daySpan));
						//  }
						//}
						//else {
						//  row.Cells.Add(ClockEvents.Format(daySpan));
						//}
						daySpan=new TimeSpan(0);
					}
					else {//not the last entry for the day
						//row.Cells.Add("");
					}
					//Weekly-------------------------------------
					WeeklyTotals[i]=weekSpan;
					//if(IsBreaks) {
					//  row.Cells.Add("");
					//}
					//if this is the last entry for a given week
					//else 
					if(i==mergedAL.Count-1//if this is the last row 
		        || cal.GetWeekOfYear(GetDateForRowHelper(mergedAL[i+1]),rule,(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek))//or the next row has a
		        != cal.GetWeekOfYear(clock.TimeDisplayed1.Date,rule,(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek)))//different week of year
		      {
						//row.Cells.Add(ClockEvents.Format(weekSpan));
						weekSpan=new TimeSpan(0);
					}
					else {
						//row.Cells.Add(ClockEvents.Format(weekSpan));
						//row.Cells.Add("");
					}
					//Note-----------------------------------------
					//row.Cells.Add(clock.Note);
				}
				//adjustment row--------------------------------------------------------------------------------------
				else if(type==typeof(TimeAdjust)) {
					adjust=(TimeAdjust)mergedAL[i];
					curDate=adjust.TimeEntry.Date;
					//if(curDate==previousDate) {
					//  row.Cells.Add("");
					//  row.Cells.Add("");
					//}
					//else {
					//  row.Cells.Add(curDate.ToShortDateString());
					//  row.Cells.Add(curDate.DayOfWeek.ToString());
					//}
					//altered--------------------------------------
					//row.Cells.Add(Lan.g(this,"Adjust"));//2
					//row.ColorText=Color.Red;
					//status--------------------------------------
					//row.Cells.Add("");//3
					//in/out------------------------------------------
					//row.Cells.Add("");//4
					//time-----------------------------
					//rowCells.Add(adjust.TimeEntry.ToShortTimeString());//5
					//total-------------------------------
					//row.Cells.Add("");//
					//Adjust------------------------------
					daySpan+=adjust.RegHours;//might be negative
					weekSpan+=adjust.RegHours;
					periodSpan+=adjust.RegHours;
					//row.Cells.Add(ClockEvents.Format(adjust.RegHours));//6
					//Overtime------------------------------
					otspan+=adjust.OTimeHours;
					//row.Cells.Add(ClockEvents.Format(adjust.OTimeHours));//7
					//Daily-----------------------------------
					//if this is the last entry for a given date
					if(i==mergedAL.Count-1//if this is the last row
		        || GetDateForRowHelper(mergedAL[i+1]) != curDate)//or the next row is a different date
		      {
						//row.Cells.Add(ClockEvents.Format(daySpan));//
						daySpan=new TimeSpan(0);
					}
					else {
						//row.Cells.Add("");
					}
					//Weekly-------------------------------------
					WeeklyTotals[i]=weekSpan;
					//if(IsBreaks) {
					//  row.Cells.Add("");
					//}
					//if this is the last entry for a given week
					//else 
					if(i==mergedAL.Count-1//if this is the last row 
		        || cal.GetWeekOfYear(GetDateForRowHelper(mergedAL[i+1]),rule,DayOfWeek.Sunday)//or the next row has a
		        != cal.GetWeekOfYear(adjust.TimeEntry.Date,rule,DayOfWeek.Sunday))//different week of year
		      {
						//ODGridCell cell=new ODGridCell(ClockEvents.Format(weekSpan));
						//cell.ColorText=Color.Black;
						//row.Cells.Add(cell);
						weekSpan=new TimeSpan(0);
					}
					else {
						//row.Cells.Add("");
					}
					//Note-----------------------------------------
					//row.Cells.Add(adjust.Note);
				}
				//gridMain.Rows.Add(row);
			}
			//gridMain.EndUpdate();
			//if(IsBreaks) {
			//  textTotal.Text="";
			//}
			//else {
			//  textTotal.Text=periodSpan.ToStringHmm();
			//  textOvertime.Text=otspan.ToStringHmm();
			//  textTotal2.Text=periodSpan.TotalHours.ToString("n");
			//  textOvertime2.Text=otspan.TotalHours.ToString("n");
			//}
			foreach(TimeSpan week in WeeklyTotals) {
				retVal.Add(week);
			}
			return retVal;
		}

		private static DateTime GetDateForRowHelper(object timeEvent) {
			if(timeEvent.GetType()==typeof(ClockEvent)) {
				return ((ClockEvent)timeEvent).TimeDisplayed1.Date;
			}
			else if(timeEvent.GetType()==typeof(TimeAdjust)) {
				return ((TimeAdjust)timeEvent).TimeEntry.Date;
			}
			return DateTime.MinValue;
		}



	}
}