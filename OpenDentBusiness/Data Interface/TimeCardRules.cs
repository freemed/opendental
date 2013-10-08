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

		///<summary>will be deprecated with overhaul 9/13/2013. Validates pay period before making any adjustments.</summary>
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

		///<summary>Clears automatic adjustment/adjustOT values and deletes automatic TimeAdjusts for period.</summary>
		public static void ClearAuto(long employeeNum,DateTime dateStart,DateTime dateStop) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),employeeNum,dateStart,dateStop);
				return;
			}
			List<ClockEvent> ListCE=ClockEvents.GetSimpleList(employeeNum,dateStart,dateStop);
			for(int i=0;i<ListCE.Count;i++) {
				ListCE[i].AdjustAuto=TimeSpan.Zero;
				ListCE[i].OTimeAuto=TimeSpan.Zero;
				ListCE[i].Rate2Auto=TimeSpan.Zero;
				ClockEvents.Update(ListCE[i]);
			}
			List<TimeAdjust> ListTA=TimeAdjusts.GetSimpleListAuto(employeeNum,dateStart,dateStop);
			for(int i=0;i<ListTA.Count;i++) {
				TimeAdjusts.Delete(ListTA[i]);
			}
		}

		///<summary>Clears all manual adjustments/Adjust OT values from clock events. Does not alter adjustments to clockevent.TimeDisplayed1/2 nor does it delete or alter any TimeAdjusts.</summary>
		public static void ClearManual(long employeeNum,DateTime dateStart,DateTime dateStop) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),employeeNum,dateStart,dateStop);
				return;
			}
			List<ClockEvent> ListCE=ClockEvents.GetSimpleList(employeeNum,dateStart,dateStop);
			for(int i=0;i<ListCE.Count;i++) {
				ListCE[i].Adjust=TimeSpan.Zero;
				ListCE[i].AdjustIsOverridden=false;
				ListCE[i].OTimeHours=TimeSpan.FromHours(-1);
				ListCE[i].Rate2Hours=TimeSpan.FromHours(-1);
				ClockEvents.Update(ListCE[i]);
			}
		}

		///<summary>Validates list and throws exceptions.  Gets a list of time card rules for a given employee.</summary>
		public static List<TimeCardRule> GetValidList(Employee employeeCur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TimeCardRule>>(MethodBase.GetCurrentMethod(),employeeCur);
			}
			List<TimeCardRule> retVal = new List<TimeCardRule>();
			List<TimeSpan> listTimeSpansAM=new List<TimeSpan>();
			List<TimeSpan> listTimeSpansPM=new List<TimeSpan>();
			List<TimeSpan> listTimeSpansOver=new List<TimeSpan>();
			RefreshCache();
			string errors="";
			//Fill Rules list and time span list-------------------------------------------------------------------------------------------
			for(int i=0;i<listt.Count;i++) {
				if(listt[i].EmployeeNum==employeeCur.EmployeeNum || listt[i].EmployeeNum==0) {//specific rule for employee or rules that apply to all employees.
					retVal.Add(listt[i]);
					if(listt[i].BeforeTimeOfDay>TimeSpan.FromHours(0)) {
						listTimeSpansAM.Add(listt[i].BeforeTimeOfDay);
					}
					if(listt[i].AfterTimeOfDay>TimeSpan.FromHours(0)) {
						listTimeSpansPM.Add(listt[i].AfterTimeOfDay);
					}
					if(listt[i].OverHoursPerDay>TimeSpan.FromHours(0)) {
						listTimeSpansOver.Add(listt[i].OverHoursPerDay);
					}
				}
			}
			//Validate Rules---------------------------------------------------------------------------------------------------------------
			if(listTimeSpansAM.Count>1) {
				errors+="Multiple matches of BeforeTimeOfDay found, only one allowed.\r\n";
			}
			if(listTimeSpansPM.Count>1) {
				errors+="Multiple matches of AfterTimeOfDay found, only one allowed.\r\n";
			}
			if(listTimeSpansOver.Count>1) {
				errors+="Multiple matches of OverHoursPerDay found, only one allowed.\r\n";
			}
			if(listTimeSpansAM.Count+listTimeSpansPM.Count>0 && listTimeSpansOver.Count>0) {
				errors+="Both OverHoursPerDay and Rate2 rules found.\r\n";
			}
			if(errors!=""){
				throw new Exception("Time card rule errors:\r\n"+errors);
			}
			return retVal;
		}

		///<summary>Calculates daily overtime. Throws exceptions when encountering errors, though all errors SHOULD have been caught already by using the ValidatePayPeriod() function and generating a msgbox.</summary>
		public static void CalculateDailyOvertime_Old(Employee EmployeeCur,DateTime StartDate,DateTime StopDate) {
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
					ClockEventList[i].OTimeAuto=afterTime;
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
					ClockEventList[i].OTimeAuto+=beforeTime;
				}
				if(overHoursRule != null) {
					//test dailyTotal
					TimeSpan overHours=TimeSpan.Zero;
					if(dailyTotal > overHoursRule.OverHoursPerDay) {
						overHours=dailyTotal-overHoursRule.OverHoursPerDay;
						dailyTotal=overHoursRule.OverHoursPerDay;//e.g. reset to 8.  Any further pairs on this date will be wholly OT
						ClockEventList[i].OTimeAuto+=overHours;
					}
				}
				ClockEvents.Update(ClockEventList[i]);
			}
			AdjustBreaksHelper(EmployeeCur,StartDate,StopDate);
		}

		///<summary>Calculates daily overtime.  Daily overtime does not take into account any time adjust events.  All manually entered time adjust events are assumed to be entered correctly and should not be used in calculating automatic totals.  Throws exceptions when encountering errors.</summary>
		public static void CalculateDailyOvertime(Employee employee,DateTime dateStart,DateTime dateStop) {
			#region Fill Lists, validate data sets, generate error messages.
			List<ClockEvent> listClockEvent=new List<ClockEvent>();
			List<ClockEvent> listClockEventBreak=new List<ClockEvent>();
			List<TimeCardRule> listTimeCardRule=new List<TimeCardRule>();
			string errors="";
			string clockErrors="";
			string breakErrors="";
			string ruleErrors="";
			//Fill lists and catch validation error messages------------------------------------------------------------------------------------------------------------
			try{ listClockEvent			=ClockEvents	.GetValidList(employee.EmployeeNum,dateStart,dateStop,false); } catch(Exception ex){clockErrors			+=ex.Message;}
			try{ listClockEventBreak=ClockEvents	.GetValidList(employee.EmployeeNum,dateStart,dateStop,true);	} catch(Exception ex){breakErrors			+=ex.Message;}
			try{ listTimeCardRule		=TimeCardRules.GetValidList(employee);																			} catch(Exception ex){ruleErrors			+=ex.Message;}
			//Validation between two or more lists above----------------------------------------------------------------------------------------------------------------
			for(int b=0;b<listClockEventBreak.Count;b++) {
				bool isValidBreak=false;
				for(int c=0;c<listClockEvent.Count;c++) {
					if(timeClockEventsOverlapHelper(listClockEventBreak[b],listClockEvent[c])) {
						isValidBreak=true;
						break;
					}
				}
				if(isValidBreak) {
					continue;
				}
				breakErrors+="  "+listClockEventBreak[b].TimeDisplayed1.ToString()+" : break found during non-working hours.\r\n";//ToString() instead of ToShortDateString() to show time.
			}
			//Report Errors---------------------------------------------------------------------------------------------------------------------------------------------
			errors=ruleErrors+clockErrors+breakErrors;
			if(errors!="") {
				throw new Exception(Employees.GetNameFL(employee)+" has the following errors:\r\n"+errors);
			}
			#endregion
			#region Fill time card rules
			//Begin calculations=========================================================================================================================================
			TimeSpan tsHoursWorkedTotal			=new TimeSpan()				;
			TimeSpan tsOvertimeHoursRule		=new TimeSpan(24,0,0)	;//Example 10:00 for overtime rule after 10 hours per day.
			TimeSpan tsDifferentialAMRule		=new TimeSpan()				;//Example 06:00 for differential rule before 6am.
			TimeSpan tsDifferentialPMRule		=new TimeSpan(24,0,0)	;//Example 17:00 for differential rule after  5pm.
			//Fill over hours rule from list-------------------------------------------------------------------------------------
			for(int i=0;i<listTimeCardRule.Count;i++){//loop through rules for this one employee, including any that apply to all emps.
				if(listTimeCardRule[i].OverHoursPerDay!=TimeSpan.Zero) {//OverHours Rule
					tsOvertimeHoursRule=listTimeCardRule[i].OverHoursPerDay;//at most, one non-zero OverHours rule available at this point.
				}
				if(listTimeCardRule[i].BeforeTimeOfDay!=TimeSpan.Zero) {//AM Rule
					tsDifferentialAMRule=listTimeCardRule[i].BeforeTimeOfDay;//at most, one non-zero AM rule available at this point.
				}
				if(listTimeCardRule[i].AfterTimeOfDay!=TimeSpan.Zero) {//PM Rule
					tsDifferentialPMRule=listTimeCardRule[i].AfterTimeOfDay;//at most, one non-zero PM rule available at this point.
				}
			}
			#endregion
			//Calculations: Regular Time, Overtime, Rate2 time---------------------------------------------------------------------------------------------------
			TimeSpan tsDailyBreaksAdjustTotal=new TimeSpan();//used to adjust the clock event
			TimeSpan tsDailyBreaksTotal=new TimeSpan();//used in calculating breaks over 30 minutes per day.
			TimeSpan tsDailyDifferentialTotal=new TimeSpan();//hours before and after AM/PM diff rules. Adjusted for overbreaks.
			//Note: If TimeCardsMakesAdjustmentsForOverBreaks is true, only the first 30 minutes of break per day are paid. 
			//All breaktime thereafter will be calculated as if the employee was clocked out at that time.
			for(int i=0;i<listClockEvent.Count;i++) {
				#region  Differential pay (including overbreak adjustments)--------------------------------------------------------------
				if(i==0 || listClockEvent[i].TimeDisplayed1.Date!=listClockEvent[i-1].TimeDisplayed1.Date) {
					tsDailyDifferentialTotal=TimeSpan.Zero;
				}
				//AM-----------------------------------
				if(listClockEvent[i].TimeDisplayed1.TimeOfDay<tsDifferentialAMRule) {//clocked in before AM differential rule
					tsDailyDifferentialTotal+=tsDifferentialAMRule-listClockEvent[i].TimeDisplayed1.TimeOfDay;
					if(listClockEvent[i].TimeDisplayed2.TimeOfDay<tsDifferentialAMRule) {//clocked out before AM differential rule also
						tsDailyDifferentialTotal+=listClockEvent[i].TimeDisplayed1.TimeOfDay-tsDifferentialAMRule;//add a negative timespan
					}
					//Adjust AM differential by overbreaks-----
					TimeSpan tsAMBreakTimeCounter=new TimeSpan();
					for(int b=0;b<listClockEventBreak.Count;b++) {
						if(tsAMBreakTimeCounter>TimeSpan.FromMinutes(30)) {
							tsAMBreakTimeCounter=TimeSpan.FromMinutes(30);//reset overages for next calculation.
						}
						if(listClockEventBreak[b].TimeDisplayed1.Date!=listClockEvent[i].TimeDisplayed1.Date) {
							continue;//skip breaks for other days.
						}
						tsAMBreakTimeCounter+=listClockEventBreak[b].TimeDisplayed2-listClockEventBreak[b].TimeDisplayed1;
						if(tsAMBreakTimeCounter<TimeSpan.FromMinutes(30)) {
							continue;//not over thirty minutes yet.
						}
						if(timeClockEventsOverlapHelper(listClockEvent[i],listClockEventBreak[b])) {
							continue;//There must be multiple clock events for this day, and we have gone over breaks during a later clock event period
						}
						if(listClockEventBreak[b].TimeDisplayed1.TimeOfDay>tsDifferentialAMRule) {
							continue;//this break started after the AM differential so there is nothing left to do in this loop. break out of the entire loop.
						}
						if(listClockEventBreak[b].TimeDisplayed2.TimeOfDay-(tsAMBreakTimeCounter-TimeSpan.FromMinutes(30))>tsDifferentialAMRule) {
							continue;//entirety of break overage occured after AM differential time.
						}
						//Make adjustments because: 30+ minutes of break, break occured during clockEvent, break started before the AM rule.
						TimeSpan tsAMAdjustAmount=TimeSpan.Zero;
						tsAMAdjustAmount+=tsDifferentialAMRule-(listClockEventBreak[b].TimeDisplayed2.TimeOfDay-(tsAMBreakTimeCounter-TimeSpan.FromMinutes(30)));//should be negative timespan
						tsDailyDifferentialTotal+=tsAMAdjustAmount;
					}
				}
				//PM-------------------------------------
				if(listClockEvent[i].TimeDisplayed2.TimeOfDay>tsDifferentialPMRule) {//clocked out after PM differential rule
					tsDailyDifferentialTotal+=listClockEvent[i].TimeDisplayed2.TimeOfDay-tsDifferentialPMRule;
					if(listClockEvent[i].TimeDisplayed1.TimeOfDay>tsDifferentialPMRule) {//clocked in after PM differential rule also
						tsDailyDifferentialTotal+=tsDifferentialPMRule-listClockEvent[i].TimeDisplayed1.TimeOfDay;//add a negative timespan
					}
					//Adjust PM differential by overbreaks-----
					TimeSpan tsPMBreakTimeCounter=new TimeSpan();
					for(int b=0;b<listClockEventBreak.Count;b++) {
						if(tsPMBreakTimeCounter>TimeSpan.FromMinutes(30)) {
							tsPMBreakTimeCounter=TimeSpan.FromMinutes(30);//reset overages for next calculation.
						}
						if(listClockEventBreak[b].TimeDisplayed1.Date!=listClockEvent[i].TimeDisplayed1.Date) {
							continue;//skip breaks for other days.
						}
						tsPMBreakTimeCounter+=listClockEventBreak[b].TimeDisplayed2-listClockEventBreak[b].TimeDisplayed1;
						if(tsPMBreakTimeCounter<TimeSpan.FromMinutes(30)) {
							continue;//not over thirty minutes yet.
						}
						if(!timeClockEventsOverlapHelper(listClockEvent[i],listClockEventBreak[b])) {
							continue;//There must be multiple clock events for this day, and we have gone over breaks during a different clock event period
						}
						if(listClockEventBreak[b].TimeDisplayed2.TimeOfDay<tsDifferentialPMRule) {
							continue;//this break ended before the PM differential so there is nothing left to do in this loop. break out of the entire loop.
						}
						if(listClockEventBreak[b].TimeDisplayed2.TimeOfDay<tsDifferentialPMRule) {
							continue;//entirety of break overage occured before PM differential time.
						}
						//Make adjustments because: 30+ minutes of break, break occured during clockEvent, break ended after the PM rule.
						TimeSpan tsPMAdjustAmount=TimeSpan.Zero;
						tsPMAdjustAmount+=tsDifferentialPMRule-(listClockEventBreak[b].TimeDisplayed2.TimeOfDay-(tsPMBreakTimeCounter-TimeSpan.FromMinutes(30)));//should be negative timespan
						tsDailyDifferentialTotal+=tsPMAdjustAmount;
					}
				}
				//Apply differential to clock event-----------------------------------------------------------------------------------
				if(tsDailyDifferentialTotal<TimeSpan.Zero) {
					//this should never happen. If it ever does, we need to know about it, because that means some math has been miscalculated.
					throw new Exception(" - "+listClockEvent[i].TimeDisplayed1.Date.ToShortDateString()+" : calculated differential hours was negative.");
				}
				listClockEvent[i].Rate2Auto=tsDailyDifferentialTotal;//should be zero or greater.
				#endregion
				#region Regular hours and OT hours calulations (including overbreak adjustments)----------------------------------------
				listClockEvent[i].OTimeAuto	=TimeSpan.Zero;
				listClockEvent[i].AdjustAuto=TimeSpan.Zero;
				if(i==0 || listClockEvent[i].TimeDisplayed1.Date!=listClockEvent[i-1].TimeDisplayed1.Date) {
					tsHoursWorkedTotal=TimeSpan.Zero;
					tsDailyBreaksAdjustTotal=TimeSpan.Zero;
					tsDailyBreaksTotal=TimeSpan.Zero;
					tsDailyDifferentialTotal=TimeSpan.Zero;
				}
				tsHoursWorkedTotal+=listClockEvent[i].TimeDisplayed2-listClockEvent[i].TimeDisplayed1;//Hours worked
				if(tsHoursWorkedTotal>tsOvertimeHoursRule) {//if OverHoursPerDay then make AutoOTAdjustments.
					listClockEvent[i].OTimeAuto	+=tsHoursWorkedTotal-tsOvertimeHoursRule;//++OTimeAuto
					//listClockEvent[i].AdjustAuto-=tsHoursWorkedTotal-tsOvertimeHoursRule;//--AdjustAuto
				}
				if(i==listClockEvent.Count-1 || listClockEvent[i].TimeDisplayed1.Date!=listClockEvent[i+1].TimeDisplayed1.Date) {
					//Either the last clock event in the list or last clock event for the day.
					//OVERBREAKS--------------------------------------------------------------------------------------------------------
					if(PrefC.GetBool(PrefName.TimeCardsMakesAdjustmentsForOverBreaks)) {//Apply overbreaks to this clockEvent.
						tsDailyBreaksAdjustTotal=new TimeSpan();//used to adjust the clock event
						tsDailyBreaksTotal=new TimeSpan();//used in calculating breaks over 30 minutes per day.
						for(int b=0;b<listClockEventBreak.Count;b++) {//check all breaks for current day.
							if(listClockEventBreak[b].TimeDisplayed1.Date!=listClockEvent[i].TimeDisplayed1.Date) {
								continue;//skip breaks for other dates than current ClockEvent
							}
							tsDailyBreaksTotal+=(listClockEventBreak[b].TimeDisplayed2.TimeOfDay-listClockEventBreak[b].TimeDisplayed1.TimeOfDay);
							if(tsDailyBreaksTotal>TimeSpan.FromMinutes(31)) {//over 31 to avoid adjustments less than 1 minutes.
								listClockEventBreak[b].AdjustAuto=TimeSpan.FromMinutes(30)-tsDailyBreaksTotal;
								ClockEvents.Update(listClockEventBreak[b]);//save adjustments to breaks.
								tsDailyBreaksAdjustTotal+=listClockEventBreak[b].AdjustAuto;
								tsDailyBreaksTotal=TimeSpan.FromMinutes(30);//reset daily breaks to 30 minutes so the next break is all adjustment.
							}//end overBreaks>31 minutes
						}//end checking all breaks for current day
						//OverBreaks applies to overtime and then to RegularTime
						listClockEvent[i].OTimeAuto+=tsDailyBreaksAdjustTotal;//tsDailyBreaksTotal<=TimeSpan.Zero
						listClockEvent[i].AdjustAuto+=tsDailyBreaksAdjustTotal;//tsDailyBreaksTotal is less than or equal to zero
						if(listClockEvent[i].OTimeAuto<TimeSpan.Zero) {//we have adjusted OT too far
							//listClockEvent[i].AdjustAuto+=listClockEvent[i].OTimeAuto;
							listClockEvent[i].OTimeAuto=TimeSpan.Zero;
						}
						tsDailyBreaksTotal=TimeSpan.Zero;//zero out for the next day.
						tsHoursWorkedTotal=TimeSpan.Zero;//zero out for next day.
					}//end overbreaks
				}
				#endregion
				ClockEvents.Update(listClockEvent[i]);
			}//end clockEvent loop.
		}

		///<summary>Returns true if two clock events overlap. Useful for determining if a break applies to a given clock event.  
		///Does not matter which order clock events are provided.</summary>
		private static bool timeClockEventsOverlapHelper(ClockEvent clockEvent1,ClockEvent clockEvent2) {
			//Visual representation
			//ClockEvent1:            o----------------o
			//ClockEvent2:o---------------o   or  o-------------------o
			if(clockEvent2.TimeDisplayed2>clockEvent1.TimeDisplayed1 
				&& clockEvent2.TimeDisplayed1<clockEvent1.TimeDisplayed2) {
					return true;
			}
			return false;
		}

		///<summary>Updates OTimeAuto, AdjustAuto (calculated and set above., and Rate2Auto based on the rules passed in, and calculated break time overages.</summary>
		private static void AdjustAutoClockEventEntriesHelper(List<ClockEvent> listClockEvent,List<ClockEvent> listClockEventBreak,TimeSpan tsDifferentialAMRule,TimeSpan tsDifferentialPMRule,TimeSpan tsOvertimeHoursRule) {
			for(int i=0;i<listClockEvent.Count;i++) {
				//listClockEvent[i].OTimeAuto	=TimeSpan.Zero;
				listClockEvent[i].AdjustAuto	=TimeSpan.Zero;
				listClockEvent[i].Rate2Auto		=TimeSpan.Zero;
				//OTimeAuto and AdjustAuto---------------------------------------------------------------------------------
				//if((listClockEvent[i].TimeDisplayed2.TimeOfDay-listClockEvent[i].TimeDisplayed1.TimeOfDay)>tsOvertimeHoursRule) {
				//	listClockEvent[i].OTimeAuto+=listClockEvent[i].TimeDisplayed2.TimeOfDay-listClockEvent[i].TimeDisplayed1.TimeOfDay-tsOvertimeHoursRule;
				//listClockEvent[i].AdjustAuto+=-listClockEvent[i].OTimeAuto;
				//}
				//AdjustAuto due to break overages-------------------------------------------------------------------------
				if(PrefC.GetBool(PrefName.TimeCardsMakesAdjustmentsForOverBreaks)) {
					if(i==listClockEvent.Count-1 || listClockEvent[i].TimeDisplayed1.Date!=listClockEvent[i+1].TimeDisplayed1.Date) {//last item or last item for a given day.
						TimeSpan tsTotalBreaksToday=TimeSpan.Zero;
						for(int j=0;j<listClockEventBreak.Count;j++) {
							if(listClockEventBreak[j].TimeDisplayed1.Date!=listClockEvent[i].TimeDisplayed1.Date) {//skip breaks that occured on different days.
								continue;
							}
							tsTotalBreaksToday+=listClockEventBreak[j].TimeDisplayed2.TimeOfDay-listClockEventBreak[j].TimeDisplayed1.TimeOfDay;
						}
						if(tsTotalBreaksToday>TimeSpan.FromMinutes(31)) {
							listClockEvent[i].AdjustAuto+=TimeSpan.FromMinutes(30)-tsTotalBreaksToday;//should add a negative time span.
							listClockEvent[i].OTimeAuto+=TimeSpan.FromMinutes(30)-tsTotalBreaksToday;//should add a negative time span.
							if(listClockEvent[i].OTimeAuto<TimeSpan.Zero) {//if we removed too much overbreak from otAuto, remove it from adjust auto instead and set otauto to zero
								listClockEvent[i].AdjustAuto+=listClockEvent[i].OTimeAuto;
								listClockEvent[i].OTimeAuto=TimeSpan.Zero;
							}
							tsTotalBreaksToday=TimeSpan.FromMinutes(30);//reset break today to 30 minutes, so next break is entirely overBreak.
						}
					}
				}
				//Rate2Auto-------------------------------------------------------------------------------------------------
				if(listClockEvent[i].TimeDisplayed1.TimeOfDay<tsDifferentialAMRule) {//AM, example rule before 8am, work from 5am to 7am
					listClockEvent[i].Rate2Auto+=tsDifferentialAMRule-listClockEvent[i].TimeDisplayed1.TimeOfDay;//8am-5am=3hrs
					if(listClockEvent[i].TimeDisplayed2.TimeOfDay<tsDifferentialAMRule) {
						listClockEvent[i].Rate2Auto+=listClockEvent[i].TimeDisplayed2.TimeOfDay-tsDifferentialAMRule;//8am-7am=-1hr =>2hrs total
					}
				}
				if(listClockEvent[i].TimeDisplayed2.TimeOfDay>tsDifferentialPMRule) {//PM, example diffRule after 8pm, work from 9 to 11pm. 
					listClockEvent[i].Rate2Auto+=listClockEvent[i].TimeDisplayed2.TimeOfDay-tsDifferentialPMRule;//11pm-8pm = 3hrs 
					if(listClockEvent[i].TimeDisplayed1.TimeOfDay>tsDifferentialPMRule) {
						listClockEvent[i].Rate2Auto+=tsDifferentialPMRule-listClockEvent[i].TimeDisplayed1.TimeOfDay;//8pm-9pm = -1hr =>2hrs total
					}
				}
				ClockEvents.Update(listClockEvent[i]);
			}//end ClockEvent list
		}

		///<summary>Deprecated.  This function is aesthetic and has no bearing on actual OT calculations. It adds adjustments to breaks so that when viewing them you can see if they went over 30 minutes.</summary>
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
		public static void CalculateWeeklyOvertime_Old(Employee EmployeeCur,DateTime StartDate,DateTime StopDate) {
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

		///<summary>Calculates weekly overtime and inserts TimeAdjustments accordingly.</summary>
		public static void CalculateWeeklyOvertime(Employee EmployeeCur,DateTime StartDate,DateTime StopDate) {
			List<ClockEvent> listClockEvent=new List<ClockEvent>();
			List<TimeAdjust> listTimeAdjust=new List<TimeAdjust>();
			string errors="";
			string clockErrors="";
			string timeAdjustErrors="";
			//Fill lists and catch validation error messages------------------------------------------------------------------------------------------------------------
			try{listClockEvent=ClockEvents.GetValidList(EmployeeCur.EmployeeNum,StartDate,StopDate,false)	;}catch(Exception ex) {clockErrors+=ex.Message;}
			try{listTimeAdjust=TimeAdjusts.GetValidList(EmployeeCur.EmployeeNum,StartDate,StopDate)				;}catch(Exception ex) {timeAdjustErrors+=ex.Message;}
			//Report Errors---------------------------------------------------------------------------------------------------------------------------------------------
			errors=clockErrors+timeAdjustErrors;
			if(errors!="") {
				throw new Exception(Employees.GetNameFL(EmployeeCur)+" has the following errors:\r\n"+errors);
			}
			


			//first, delete all existing non manual overtime entries
			for(int i=0;i<listTimeAdjust.Count;i++) {
				if(listTimeAdjust[i].IsAuto) {
					TimeAdjusts.Delete(listTimeAdjust[i]);
				}
			}
			//refresh list after it has been cleaned up.
			
			
			listTimeAdjust=TimeAdjusts.Refresh(EmployeeCur.EmployeeNum,StartDate,StopDate);
			ArrayList mergedAL = new ArrayList();
			foreach(ClockEvent clockEvent in listClockEvent) {
				mergedAL.Add(clockEvent);
			}
			foreach(TimeAdjust timeAdjust in listTimeAdjust) {
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