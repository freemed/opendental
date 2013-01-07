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

		///<summary>Validates pay period before making any adjustments.</summary>
		public static string ValidatePayPeriod(Employee EmployeeCur, DateTime StartDate,DateTime StopDate) {
			List<ClockEvent> breakList=ClockEvents.Refresh(EmployeeCur.EmployeeNum,StartDate,StopDate,true);
			List<ClockEvent> ClockEventList=ClockEvents.Refresh(EmployeeCur.EmployeeNum,StartDate,StopDate,false);
			bool errorFound=false;
			string retVal="Timecard errors for employee : "+Employees.GetNameFL(EmployeeCur)+"\r\n";
			//Validate clock events
			foreach(ClockEvent cCur in ClockEventList){
				if(cCur.TimeDisplayed2.Year < 1880){
					retVal+="  "+cCur.TimeDisplayed1.ToShortDateString()+" : Employee not clocked out.\r\n";
					errorFound=true;
				}
				else if(cCur.TimeDisplayed1.Date != cCur.TimeDisplayed2.Date){
					retVal+="  "+cCur.TimeDisplayed1.ToShortDateString()+" : Clock entry spans multiple days.\r\n";
					errorFound=true;
				}
			}
			//Validate Breaks
			foreach(ClockEvent bCur in breakList) {
				if(bCur.TimeDisplayed2.Year<1880) {
					retVal+="  "+bCur.TimeDisplayed1.ToShortDateString()+" : Employee not clocked in from break.\r\n";
					errorFound=true;
				}
				if(bCur.TimeDisplayed1.Date != bCur.TimeDisplayed2.Date) {
					retVal+="  "+bCur.TimeDisplayed1.ToShortDateString()+" : One break spans multiple days.\r\n";
					errorFound=true;
				}
				for(int c=ClockEventList.Count-1;c>=0;c--) {
					if(ClockEventList[c].TimeDisplayed1.Date==bCur.TimeDisplayed1.Date) {
						break;
					}
					if(c==0) {//we never found a match
						retVal+="  "+bCur.TimeDisplayed1.ToShortDateString()+" : Break found during non-working day.\r\n";
						errorFound=true;
					}
				}
			}
			//Validate OT Rules
			bool amRuleFound=false;
			bool pmRuleFound=false;
			bool hourRuleFound=false;
			TimeCardRules.RefreshCache();
			foreach(TimeCardRule tcrCur in TimeCardRules.listt){
				if(tcrCur.EmployeeNum!=EmployeeCur.EmployeeNum){
					continue;//Does not apply to this employee.
				}
				if(tcrCur.AfterTimeOfDay > TimeSpan.Zero){
					if(pmRuleFound){
						retVal+="  Multiple timecard rules for after time of day found. Only one allowed.\r\n";
						errorFound=true;
					}
					pmRuleFound=true;
				}
				if(tcrCur.BeforeTimeOfDay > TimeSpan.Zero){
					if(amRuleFound){
						retVal+="  Multiple timecard rules for before time of day found. Only one allowed.\r\n";
						errorFound=true;
					}
					amRuleFound=true;
				}
				if(tcrCur.OverHoursPerDay > TimeSpan.Zero){
					if(hourRuleFound){
						retVal+="  Multiple timecard rules for hours per day found. Only one allowed.\r\n";
						errorFound=true;
					}
					hourRuleFound=true;
				}
				if(pmRuleFound&&hourRuleFound){
					retVal+="  Both an OverHoursPerDay and an AfterTimeOfDay found for this employee.  Only one or the other is allowed.\r\n";
					errorFound=true;
				}
				if(amRuleFound&&hourRuleFound){
					retVal+="  Both an OverHoursPerDay and an BeforeTimeOfDay found for this employee.  Only one or the other is allowed.\r\n";
					errorFound=true;
				}
			}
			retVal+="\r\n";
			return (errorFound?retVal:"");
		}

		///<summary>Calculates daily overtime. Throws exceptions when encountering errors, though all errors SHOULD have been caught already by using the ValidatePayPeriod() function and generating a msgbox.</summary>
		public static void CalculateDailyOvertime(Employee EmployeeCur,DateTime StartDate,DateTime StopDate) {
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
						throw new Exception("Error. Employee break malformed.");
					}
					if(breakList[b].TimeDisplayed1.Date != breakList[b].TimeDisplayed2.Date) {
						throw new Exception("Error. One break spans multiple dates.");
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
								throw new Exception("Error. Over breaks, but could not adjust because not regular time entered for date:"
								  +breakList[b].TimeDisplayed1.Date.ToShortDateString());
							}
						}
					}
				}
				//FillMain(true);
			}
			//OT-------------------------------------------------------------------------------------------------------------
			TimeCardRule afterTimeRule=null;
			TimeCardRule beforeTimeRule=null;
			TimeCardRule overHoursRule=null;
			//loop through timecardrules to find one rule of each kind.
			for(int i=0;i<TimeCardRules.Listt.Count;i++) {
				if(TimeCardRules.Listt[i].EmployeeNum!=0 && TimeCardRules.Listt[i].EmployeeNum!=EmployeeCur.EmployeeNum) {
					continue;
				}
				if(TimeCardRules.Listt[i].AfterTimeOfDay > TimeSpan.Zero) {
					if(afterTimeRule != null) {//already found a match, and this is a second match
						throw new Exception("Error.  Multiple matches of AfterTimeOfDay found for this employee.  Only one allowed.");
						//return;
					}
					afterTimeRule=TimeCardRules.Listt[i];
				}
				else if(TimeCardRules.Listt[i].OverHoursPerDay > TimeSpan.Zero) {
					if(overHoursRule != null) {//already found a match, and this is a second match
						throw new Exception("Error.  Multiple matches of OverHoursPerDay found for this employee.  Only one allowed.");
						//return;
					}
					overHoursRule=TimeCardRules.Listt[i];
				}
				if(afterTimeRule!= null && overHoursRule != null) {
					throw new Exception("Error.  Both an OverHoursPerDay and an AfterTimeOfDay found for this employee.  Only one or the other is allowed.");
					//return;
				}
				if(beforeTimeRule != null && overHoursRule != null) {
					throw new Exception("Error.  Both an OverHoursPerDay and an BeforeTimeOfDay found for this employee.  Only one or the other is allowed.");
					//return;
				}
				if(TimeCardRules.Listt[i].BeforeTimeOfDay > TimeSpan.Zero) {
					if(beforeTimeRule != null) {//already found a match, and this is a second match
						throw new Exception("Error.  Multiple matches of BeforeTimeOfDay found for this employee.  Only one allowed.");
						//return;
					}
					beforeTimeRule=TimeCardRules.Listt[i];
				}
			}
			//loop through all ClockEvents in this grid.
			TimeSpan dailyTotal=TimeSpan.Zero;
			TimeSpan pairTotal=TimeSpan.Zero;
			previousDate=DateTime.MinValue;
			for(int i=0;i<ClockEventList.Count;i++) {
				if(ClockEventList[i].TimeDisplayed2.Year<1880) {
					throw new Exception("Error. Employee not clocked out.");
					//return;
				}
				if(ClockEventList[i].TimeDisplayed1.Date != ClockEventList[i].TimeDisplayed2.Date) {
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
				if(afterTimeRule != null) {
					//test to see if this span is after specified time
					TimeSpan afterTime=TimeSpan.Zero;
					if(ClockEventList[i].TimeDisplayed1.TimeOfDay > afterTimeRule.AfterTimeOfDay) {//the start time is after time, so the whole pairTotal is OT
						afterTime=pairTotal;
					}
					else if(ClockEventList[i].TimeDisplayed2.TimeOfDay > afterTimeRule.AfterTimeOfDay) {//only the second time is after time
						afterTime=ClockEventList[i].TimeDisplayed2.TimeOfDay-afterTimeRule.AfterTimeOfDay;//only a portion of the pairTotal is OT
					}
					if(afterTimeRule.AmtDiff>0) {
						ClockEventList[i].AmountBonusAuto=afterTimeRule.AmtDiff*((double)afterTime.Hours + (double)afterTime.Minutes/60);
					}
					else {
						ClockEventList[i].OTimeAuto=afterTime;
					}
				}
				if(beforeTimeRule != null) {
					//test to see if this span is after specified time
					TimeSpan beforeTime=TimeSpan.Zero;
					if(ClockEventList[i].TimeDisplayed2.TimeOfDay < beforeTimeRule.BeforeTimeOfDay) {//the end time is before time, so the whole pairTotal is OT
						beforeTime+=pairTotal;
					}
					else if(ClockEventList[i].TimeDisplayed1.TimeOfDay < beforeTimeRule.BeforeTimeOfDay) {//only the first time is before time
						beforeTime+=beforeTimeRule.BeforeTimeOfDay-ClockEventList[i].TimeDisplayed1.TimeOfDay;//only a portion of the pairTotal is OT
					} 
					if(beforeTimeRule.AmtDiff>0) {
						ClockEventList[i].AmountBonusAuto+=beforeTimeRule.AmtDiff*((double)beforeTime.Hours + (double)beforeTime.Minutes/60);
					}
					else {
						ClockEventList[i].OTimeAuto+=beforeTime;
					}
				}
				if(overHoursRule != null) {
					//test dailyTotal
					TimeSpan overHours=TimeSpan.Zero;
					if(dailyTotal > overHoursRule.OverHoursPerDay) {
						overHours=dailyTotal-overHoursRule.OverHoursPerDay;
						dailyTotal=overHoursRule.OverHoursPerDay;//e.g. reset to 8.  Any further pairs on this date will be wholly OT
						if(overHoursRule.AmtDiff>0) {
							ClockEventList[i].AmountBonus+=overHoursRule.AmtDiff*((double)overHours.Hours + (double)overHours.Minutes/60);
						}
						else {
							ClockEventList[i].OTimeAuto+=overHours;
						}
					}
				}
				ClockEvents.Update(ClockEventList[i]);
			}
			AdjustBreaksHelper(EmployeeCur,StartDate,StopDate);
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
					return;
				}
				if(breakList[b].TimeDisplayed1.Date != breakList[b].TimeDisplayed2.Date) {
					//MsgBox.Show(this,"Error. One break spans multiple dates.");
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
			List<TimeAdjust> TimeAdjustList=TimeAdjusts.Refresh(EmployeeCur.EmployeeNum,StartDate,StopDate);
			List<ClockEvent> ClockEventList=ClockEvents.Refresh(EmployeeCur.EmployeeNum,StartDate,StopDate,false);
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
			//refresh list after it has been cleaned up.
			TimeAdjustList=TimeAdjusts.Refresh(EmployeeCur.EmployeeNum,StartDate,StopDate);
			ArrayList mergedAL = new ArrayList();
			foreach(ClockEvent clockEvent in ClockEventList) {
				mergedAL.Add(clockEvent);
			}
			foreach(TimeAdjust timeAdjust in TimeAdjustList) {
				mergedAL.Add(timeAdjust);
			}
			//then, fill grid
			Calendar cal=CultureInfo.CurrentCulture.Calendar;
			CalendarWeekRule rule=CalendarWeekRule.FirstFullWeek;//CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule;
			//rule=CalendarWeekRule.FirstFullWeek;//CalendarWeekRule is an Enum. For these calculations, we want to use FirstFullWeek, not FirstDay;
			List<TimeSpan> WeeklyTotals = new List<TimeSpan>();
			WeeklyTotals = FillWeeklyTotalsHelper(true,EmployeeCur,mergedAL);
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

		}

		
		/// <summary>This was originally analogous to the FormTimeCard.FillGrid(), before this logic was moved to the business layer.</summary>
		private static List<TimeSpan> FillWeeklyTotalsHelper(bool fromDB,Employee EmployeeCur,ArrayList mergedAL) {
			List<TimeSpan> retVal = new List<TimeSpan>();
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
			CalendarWeekRule rule=CalendarWeekRule.FirstFullWeek;// CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule;
			DateTime curDate=DateTime.MinValue;
			DateTime previousDate=DateTime.MinValue;
			Type type;
			ClockEvent clock;
			TimeAdjust adjust;
			for(int i=0;i<mergedAL.Count;i++) {
				type=mergedAL[i].GetType();
				previousDate=curDate;
				//clock event row---------------------------------------------------------------------------------------------
				if(type==typeof(ClockEvent)) {
					clock=(ClockEvent)mergedAL[i];
					curDate=clock.TimeDisplayed1.Date;
					if(clock.TimeDisplayed2.Year<1880) {
					}
					else {
						oneSpan=clock.TimeDisplayed2-clock.TimeDisplayed1;
						daySpan+=oneSpan;
						weekSpan+=oneSpan;
						periodSpan+=oneSpan;
					}
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
					//Daily-----------------------------------
					//if this is the last entry for a given date
					if(i==mergedAL.Count-1//if this is the last row
		        || GetDateForRowHelper(mergedAL[i+1]) != curDate)//or the next row is a different date
		      {
						daySpan=new TimeSpan(0);
					}
					else {//not the last entry for the day
					}
					//Weekly-------------------------------------
					WeeklyTotals[i]=weekSpan;
					//if this is the last entry for a given week
					if(i==mergedAL.Count-1//if this is the last row 
		        || cal.GetWeekOfYear(GetDateForRowHelper(mergedAL[i+1]),rule,(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek))//or the next row has a
		        != cal.GetWeekOfYear(clock.TimeDisplayed1.Date,rule,(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek)))//different week of year
		      {
						weekSpan=new TimeSpan(0);
					}
				}
				//adjustment row--------------------------------------------------------------------------------------
				else if(type==typeof(TimeAdjust)) {
					adjust=(TimeAdjust)mergedAL[i];
					curDate=adjust.TimeEntry.Date;
					//Adjust------------------------------
					daySpan+=adjust.RegHours;//might be negative
					weekSpan+=adjust.RegHours;
					periodSpan+=adjust.RegHours;
					//Overtime------------------------------
					otspan+=adjust.OTimeHours;
					//Daily-----------------------------------
					//if this is the last entry for a given date
					if(i==mergedAL.Count-1//if this is the last row
		        || GetDateForRowHelper(mergedAL[i+1]) != curDate)//or the next row is a different date
		      {
						daySpan=new TimeSpan(0);
					}
					//Weekly-------------------------------------
					WeeklyTotals[i]=weekSpan;
					//if this is the last entry for a given week
					if(i==mergedAL.Count-1//if this is the last row 
		        || cal.GetWeekOfYear(GetDateForRowHelper(mergedAL[i+1]),rule,(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek))//or the next row has a
		        != cal.GetWeekOfYear(adjust.TimeEntry.Date,rule,(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek)))//different week of year
		      {
						weekSpan=new TimeSpan(0);
					}
				}
			}
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