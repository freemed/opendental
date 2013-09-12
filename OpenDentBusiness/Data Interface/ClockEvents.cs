using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClockEvents {
		///<summary></summary>
		public static List<ClockEvent> Refresh(long empNum,DateTime fromDate,DateTime toDate,bool isBreaks){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClockEvent>>(MethodBase.GetCurrentMethod(),empNum,fromDate,toDate,isBreaks);
			}
			string command=
				"SELECT * FROM clockevent WHERE"
				+" EmployeeNum = '"+POut.Long(empNum)+"'"
				+" AND TimeDisplayed1 >= "+POut.Date(fromDate)
				//adding a day takes it to midnight of the specified toDate
				+" AND TimeDisplayed1 < "+POut.Date(toDate.AddDays(1));
			if(isBreaks){
				command+=" AND ClockStatus = '2'";
			}
			else{
				command+=" AND (ClockStatus = '0' OR ClockStatus = '1')";
			}
			command+=" ORDER BY TimeDisplayed1";
			return Crud.ClockEventCrud.SelectMany(command);
		}

		///<summary>Gets one ClockEvent from the db.</summary>
		public static ClockEvent GetOne(long clockEventNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ClockEvent>(MethodBase.GetCurrentMethod(),clockEventNum);
			}
			return Crud.ClockEventCrud.SelectOne(clockEventNum);
		}

		///<summary></summary>
		public static long Insert(ClockEvent clockEvent){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				clockEvent.ClockEventNum=Meth.GetLong(MethodBase.GetCurrentMethod(),clockEvent);
				return clockEvent.ClockEventNum;
			}
			return Crud.ClockEventCrud.Insert(clockEvent);
		}

		///<summary></summary>
		public static void Update(ClockEvent clockEvent){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clockEvent);
				return;
			}
			Crud.ClockEventCrud.Update(clockEvent);
		}

		///<summary></summary>
		public static void Delete(long clockEventNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clockEventNum);
				return;
			}
			string command= "DELETE FROM clockevent WHERE ClockEventNum = "+POut.Long(clockEventNum);
			Db.NonQ(command);
		}

		///<summary>Gets directly from the database.  If the last event is a completed break, then it instead grabs the half-finished clock in.  Other possibilities include half-finished clock in which truly was the last event, a finished clock in/out, a half-finished clock out for break, or null for a new employee.</summary>
		public static ClockEvent GetLastEvent(long employeeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ClockEvent>(MethodBase.GetCurrentMethod(),employeeNum);
			}
			string command="SELECT * FROM clockevent WHERE EmployeeNum="+POut.Long(employeeNum)
				+" ORDER BY TimeDisplayed1 DESC";
			command=DbHelper.LimitOrderBy(command,1);
			ClockEvent ce=Crud.ClockEventCrud.SelectOne(command);
			if(ce!=null && ce.ClockStatus==TimeClockStatus.Break && ce.TimeDisplayed2.Year>1880) {
				command="SELECT * FROM clockevent WHERE EmployeeNum="+POut.Long(employeeNum)+" "
					+"AND ClockStatus != 2 "//not a break
					+"ORDER BY TimeDisplayed1 DESC";
				command=DbHelper.LimitOrderBy(command,1);
				ce=Crud.ClockEventCrud.SelectOne(command);
				return ce;
			}
			else {
				return ce;
			}
		}

		///<summary></summary>
		public static bool IsClockedIn(long employeeNum) {
			ClockEvent clockEvent=ClockEvents.GetLastEvent(employeeNum);
			if(clockEvent==null) {//new employee
				return false;
			}
			else if(clockEvent.ClockStatus==TimeClockStatus.Break) {//only incomplete breaks will have been returned.
				//so currently on break
				return false;
			}
			else {//normal clock in/out row found
				if(clockEvent.TimeDisplayed2.Year<1880) {//already clocked in
					return true;
				}
				else {//clocked out for home or lunch.
					return false;
				}
			}
		}

		///<summary>Will throw an exception if already clocked in.</summary>
		public static void ClockIn(long employeeNum) {
			//we'll get this again, because it may have been a while and may be out of date
			ClockEvent clockEvent=ClockEvents.GetLastEvent(employeeNum);
			if(clockEvent==null) {//new employee clocking in
				clockEvent=new ClockEvent();
				clockEvent.EmployeeNum=employeeNum;
				clockEvent.ClockStatus=TimeClockStatus.Home;
				ClockEvents.Insert(clockEvent);//times handled
			}
			else if(clockEvent.ClockStatus==TimeClockStatus.Break) {//only incomplete breaks will have been returned.
				//clocking back in from break
				clockEvent.TimeEntered2=MiscData.GetNowDateTime();
				clockEvent.TimeDisplayed2=clockEvent.TimeEntered2;
				ClockEvents.Update(clockEvent);
			}
			else {//normal clock in/out
				if(clockEvent.TimeDisplayed2.Year<1880) {//already clocked in
					throw new Exception(Lans.g("ClockEvents","Error.  Already clocked in."));
				}
				else {//clocked out for home or lunch.  Need to clock back in by starting a new row.
					TimeClockStatus tcs=clockEvent.ClockStatus;
					clockEvent=new ClockEvent();
					clockEvent.EmployeeNum=employeeNum;
					clockEvent.ClockStatus=tcs;
					ClockEvents.Insert(clockEvent);//times handled
				}
			}
		}

		///<summary>Will throw an exception if already clocked out.</summary>
		public static void ClockOut(long employeeNum,TimeClockStatus clockStatus) {
			ClockEvent clockEvent=ClockEvents.GetLastEvent(employeeNum);
			if(clockEvent==null) {//new employee never clocked in
				throw new Exception(Lans.g("ClockEvents","Error.  New employee never clocked in."));
			}
			else if(clockEvent.ClockStatus==TimeClockStatus.Break) {//only incomplete breaks will have been returned.
				throw new Exception(Lans.g("ClockEvents","Error.  Already clocked out for break."));;
			}
			else {//normal clock in/out
				if(clockEvent.TimeDisplayed2.Year<1880) {//clocked in.
					if(clockStatus==TimeClockStatus.Break) {//clocking out on break
						//leave the half-finished event alone and start a new one
						clockEvent=new ClockEvent();
						clockEvent.EmployeeNum=employeeNum;
						clockEvent.ClockStatus=TimeClockStatus.Break;
						ClockEvents.Insert(clockEvent);//times handled
					}
					else {//finish the existing event
						clockEvent.TimeEntered2=MiscData.GetNowDateTime();
						clockEvent.TimeDisplayed2=clockEvent.TimeEntered2;
						clockEvent.ClockStatus=clockStatus;//whatever the user selected
						ClockEvents.Update(clockEvent);
					}
				}
				else {//clocked out for home or lunch. 
					throw new Exception(Lans.g("ClockEvents","Error.  Already clocked out."));
				}
			}
		}

		///<summary>Used in the timecard to track hours worked per week when the week started in a previous time period.  This gets all the hours of the first week before the date listed.  Also adds in any adjustments for that week.</summary>
		public static TimeSpan GetWeekTotal(long empNum,DateTime date) {
			//No need to check RemotingRole; no call to db.
			TimeSpan retVal=new TimeSpan(0);
			//If the first day of the pay period is the starting date for the overtime, then there is no need to retrieve any times from the previous pay period.
			if(date.DayOfWeek==(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek)) {
				return retVal;
			}
			List<ClockEvent> events=Refresh(empNum,date.AddDays(-6),date.AddDays(-1),false);
			//eg, if this is Thursday, then we are getting last Friday through this Wed.
			for(int i=0;i<events.Count;i++){
				if(events[i].TimeDisplayed1.DayOfWeek > date.DayOfWeek){//eg, Friday > Thursday, so ignore
					continue;
				}
				retVal+=events[i].TimeDisplayed2-events[i].TimeDisplayed1;
				if(events[i].AdjustIsOverridden) {
					retVal+=events[i].Adjust;
				}
				else {
					retVal+=events[i].AdjustAuto;//typically zero
				}
				//ot
				if(events[i].OTimeHours!=TimeSpan.FromHours(-1)) {//overridden
					retVal-=events[i].OTimeHours;
				}
				else {
					retVal-=events[i].OTimeAuto;//typically zero
				}
			}
			//now, adjustments
			List<TimeAdjust> TimeAdjustList=TimeAdjusts.Refresh(empNum,date.AddDays(-6),date.AddDays(-1));
			for(int i=0;i<TimeAdjustList.Count;i++) {
				if(TimeAdjustList[i].TimeEntry.DayOfWeek > date.DayOfWeek) {//eg, Friday > Thursday, so ignore
					continue;
				}
				retVal+=TimeAdjustList[i].RegHours;
			}
			return retVal;
		}

		///<summary>-hh:mm or -hh.mm, depending on the pref.TimeCardsUseDecimalInsteadOfColon.  Blank if zero.</summary>
		public static string Format(TimeSpan span) {
			if(PrefC.GetBool(PrefName.TimeCardsUseDecimalInsteadOfColon)){
				if(span==TimeSpan.Zero){
					return "";
				}
				return span.TotalHours.ToString("n");
			}
			else{
				return span.ToStringHmm();//blank if zero
			}
		}

		///<summary>Returns clockevent information for all non-hidden employees.  Used only in the time card manage window.</summary>
		/// <param name="IsPrintReport">Only applicable to ODHQ. If true, will add ADP pay numer and notes. The query takes about 9 seconds if this is set top true vs. about 2 seconds if set to false.</param>
		public static DataTable GetTimeCardManage(DateTime startDate,DateTime stopDate,bool isPrintReport) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<DataTable>(MethodBase.GetCurrentMethod(),startDate,stopDate,isPrintReport);
			}
			return Db.GetTable(GetTimeCardManageCommand(startDate,stopDate,isPrintReport));
		}

		/// <param name="IsPrintReport">Only applicable to ODHQ. If true, will add ADP pay numer and notes. The query takes about 9 seconds if this is set top true vs. about 2 seconds if set to false.</param>
		public static string GetTimeCardManageCommand(DateTime startDate,DateTime stopDate,bool isPrintReport) {
			string command=@"SELECT clockevent.EmployeeNum,";
			if(PrefC.GetBool(PrefName.DistributorKey) && isPrintReport) {//OD HQ
				command+="COALESCE(wikilist_employees.ADPNum,'NotInList') AS ADPNum,";
			}
			command += @"employee.FName,employee.LName,
					SEC_TO_TIME((((TIME_TO_SEC(tempclockevent.TotalTime)-TIME_TO_SEC(tempclockevent.OverTime))
						+TIME_TO_SEC(tempclockevent.AdjEvent))+TIME_TO_SEC(IFNULL(temptimeadjust.AdjReg,0)))
						+(TIME_TO_SEC(tempclockevent.OverTime)+TIME_TO_SEC(IFNULL(temptimeadjust.AdjOTime,0)))) AS tempTotalTime,
					SEC_TO_TIME((TIME_TO_SEC(tempclockevent.TotalTime)-TIME_TO_SEC(tempclockevent.OverTime))
						+TIME_TO_SEC(tempclockevent.AdjEvent)+TIME_TO_SEC(IFNULL(temptimeadjust.AdjReg,0))) AS tempRegHrs,
					SEC_TO_TIME(TIME_TO_SEC(tempclockevent.OverTime)+TIME_TO_SEC(IFNULL(temptimeadjust.AdjOTime,0))) AS tempOverTime,
					tempclockevent.AdjEvent,
					temptimeadjust.AdjReg,
					temptimeadjust.AdjOTime,
					SEC_TO_TIME(TIME_TO_SEC(tempbreak.BreakTime)+TIME_TO_SEC(AdjEvent)) AS BreakTime";
			if(isPrintReport){
				command+=",tempclockevent.Note ";
			}
			command+=@"FROM clockevent	
				LEFT JOIN (SELECT ce.EmployeeNum,SEC_TO_TIME(IFNULL(SUM(UNIX_TIMESTAMP(ce.TimeDisplayed2)),0)-IFNULL(SUM(UNIX_TIMESTAMP(ce.TimeDisplayed1)),0)) AS TotalTime,
					SEC_TO_TIME(IFNULL(SUM(TIME_TO_SEC(CASE WHEN ce.OTimeHours='-01:00:00' THEN ce.OTimeAuto ELSE ce.OTimeHours END)),0)) AS OverTime,
					SEC_TO_TIME(IFNULL(SUM(TIME_TO_SEC(CASE WHEN ce.AdjustIsOverridden='1' THEN ce.Adjust ELSE ce.AdjustAuto END)),0)) AS AdjEvent";
			if(isPrintReport) {
				command+=@",
					(SELECT CASE WHEN cev.Note !="""" THEN cev.Note ELSE """" END FROM clockevent cev 
						WHERE cev.TimeDisplayed1 >= "+POut.Date(startDate)+@"
						AND cev.TimeDisplayed1 <= "+POut.Date(stopDate.AddDays(1))+@" 
						AND cev.TimeDisplayed2 > "+POut.Date(new DateTime(0001,1,1))+@"
						AND (cev.ClockStatus = '0' OR cev.ClockStatus = '1')
						AND cev.EmployeeNum=ce.EmployeeNum
						ORDER BY cev.TimeDisplayed2 LIMIT 1) AS Note";
			}
			command+=@"
					FROM clockevent ce
					WHERE ce.TimeDisplayed1 >= "+POut.Date(startDate)+@"
					AND ce.TimeDisplayed1 <= "+POut.Date(stopDate.AddDays(1))+@" 
					AND ce.TimeDisplayed2 > "+POut.Date(new DateTime(0001,1,1))+@"
					AND (ce.ClockStatus = '0' OR ce.ClockStatus = '1')
					GROUP BY ce.EmployeeNum) tempclockevent ON clockevent.EmployeeNum=tempclockevent.EmployeeNum
				LEFT JOIN (SELECT timeadjust.EmployeeNum,SEC_TO_TIME(SUM(TIME_TO_SEC(timeadjust.RegHours))) AS AdjReg,
					SEC_TO_TIME(SUM(TIME_TO_SEC(timeadjust.OTimeHours))) AdjOTime 
					FROM timeadjust 
					WHERE "+DbHelper.DateColumn("TimeEntry")+" >= "+POut.Date(startDate)+@" 
					AND "+DbHelper.DateColumn("TimeEntry")+" <= "+POut.Date(stopDate)+@"
					GROUP BY timeadjust.EmployeeNum) temptimeadjust ON clockevent.EmployeeNum=temptimeadjust.EmployeeNum
				LEFT JOIN (SELECT ceb.EmployeeNum,SEC_TO_TIME(IFNULL(SUM(UNIX_TIMESTAMP(ceb.TimeDisplayed2)),0)-IFNULL(SUM(UNIX_TIMESTAMP(ceb.TimeDisplayed1)),0)) AS BreakTime
					FROM clockevent ceb
					WHERE ceb.TimeDisplayed1 >= "+POut.Date(startDate)+@"
					AND ceb.TimeDisplayed1 <= "+POut.Date(stopDate.AddDays(1))+@" 
					AND ceb.TimeDisplayed2 > "+POut.Date(new DateTime(0001,1,1))+@"
					AND ceb.ClockStatus = '2'
					GROUP BY ceb.EmployeeNum) tempbreak ON clockevent.EmployeeNum=tempbreak.EmployeeNum
				INNER JOIN employee ON clockevent.EmployeeNum=employee.EmployeeNum AND IsHidden=0 ";
			if(PrefC.GetBool(PrefName.DistributorKey) && isPrintReport) {//OD HQ
				command+="LEFT JOIN wikilist_employees ON wikilist_employees.EmployeeNum=employee.EmployeeNum ";
			}
			command+=@"GROUP BY EmployeeNum
				ORDER BY employee.LName";
			return command;
		}


	}

	
}




