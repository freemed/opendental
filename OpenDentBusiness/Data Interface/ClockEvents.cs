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

		///<summary>Validates list and throws exceptions.  Returns a list of clock events within the date range for employee.</summary>
		public static List<ClockEvent> GetValidList(long empNum,DateTime fromDate,DateTime toDate,bool isBreaks) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClockEvent>>(MethodBase.GetCurrentMethod(),empNum,fromDate,toDate,isBreaks);
			}
			List<ClockEvent> retVal=new List<ClockEvent>();
			string errors="";
			//Fill list-----------------------------------------------------------------------------------------------------------------------------
			string command=
				"SELECT * FROM clockevent WHERE"
				+" EmployeeNum = '"+POut.Long(empNum)+"'"
				+" AND TimeDisplayed1 >= "+POut.Date(fromDate)
				//adding a day takes it to midnight of the specified toDate
				+" AND TimeDisplayed1 < "+POut.Date(toDate.AddDays(1));
			if(isBreaks) {
				command+=" AND ClockStatus = '2'";
			}
			else {
				command+=" AND (ClockStatus = '0' OR ClockStatus = '1')";
			}
			command+=" ORDER BY TimeDisplayed1";
			retVal=Crud.ClockEventCrud.SelectMany(command);
			//Validate Pay Period------------------------------------------------------------------------------------------------------------------
			for(int i=0;i<retVal.Count;i++) {
				if(retVal[i].TimeDisplayed2.Year<1880) {
					errors+="  "+retVal[i].TimeDisplayed1.ToShortDateString()+" : the employee did not clock "+(isBreaks?"in from break":"out")+".\r\n";
				}
				if(retVal[i].TimeDisplayed1.Date!=retVal[i].TimeDisplayed2.Date) {
					errors+="  "+retVal[i].TimeDisplayed1.ToShortDateString()+" : "+(isBreaks?"break":"entry")+" spans multiple days.\r\n";
				}
			}
			if(errors!="") {
				throw new Exception((isBreaks?"Break":"Clock")+" event errors :\r\n"+errors);
			}
			return retVal;
		}



		///<summary>Validates list and throws exceptions.  Returns a list of clock events (not breaks) within the date range for employee. No option for breaks because this is just used in summing for time card report; use GetValidList instead.</summary>
		public static List<ClockEvent> GetListForTimeCardManage(long empNum,DateTime fromDate,DateTime toDate) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClockEvent>>(MethodBase.GetCurrentMethod(),empNum,fromDate,toDate);
			}
			List<ClockEvent> retVal=new List<ClockEvent>();
			string errors="";
			//Fill list-----------------------------------------------------------------------------------------------------------------------------
			string command=
				"SELECT * FROM clockevent WHERE"
				+" EmployeeNum = '"+POut.Long(empNum)+"'"
				+" AND TimeDisplayed1 >= "+POut.Date(fromDate)
				+" AND TimeDisplayed1 < "+POut.Date(toDate.AddDays(1))//adding a day takes it to midnight of the specified toDate
				+" AND (ClockStatus = 0 OR ClockStatus = 1)"
				+" ORDER BY TimeDisplayed1";
			retVal=Crud.ClockEventCrud.SelectMany(command);
			//Validate Pay Period------------------------------------------------------------------------------------------------------------------
			for(int i=0;i<retVal.Count;i++) {
				if(retVal[i].TimeDisplayed2.Year<1880) {
					errors+="  "+retVal[i].TimeDisplayed1.ToShortDateString()+" : the employee did not clock out.\r\n";
				}
				if(retVal[i].TimeDisplayed1.Date!=retVal[i].TimeDisplayed2.Date) {
					errors+="  "+retVal[i].TimeDisplayed1.ToShortDateString()+" : entry spans multiple days.\r\n";
				}
			}
			if(errors!="") {
				throw new Exception("Clock event errors :\r\n"+errors);
			}
			return retVal;
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
		/// <param name="IsPrintReport">Only applicable to ODHQ. If true, will add ADP pay numer and note. The query takes about 9 seconds if this is set top true vs. about 2 seconds if set to false.</param>
		public static DataTable GetTimeCardManage(DateTime startDate,DateTime stopDate) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<DataTable>(MethodBase.GetCurrentMethod(),startDate,stopDate);
			}
			//Construct empty table------------------------------------------------------------------------------------------------------------------------
			DataTable retVal=new DataTable();
			retVal.Columns.Add("PayrollID");
			retVal.Columns.Add("EmployeeNum");
			retVal.Columns.Add("firstName");
			retVal.Columns.Add("lastName");
			retVal.Columns.Add("totalHours");//should be sum of RegularHours and OvertimeHours
			retVal.Columns.Add("rate1Hours");
			retVal.Columns.Add("rate1OTHours");
			retVal.Columns.Add("rate2Hours");
			retVal.Columns.Add("rate2OTHours");
			//retVal.Columns.Add("ClockEventAdjustTotal");
			//retVal.Columns.Add("TimeAdjust");//adjustments to regular time from timeAdjust entries. Not a net zero effect on total hours worked.
			//retVal.Columns.Add("TimeAdjustOT");//OT adjustments from timeAdjust entries. OT time adjust has net zero effect. Non-OT time adjust alters total hours worked.
			//retVal.Columns.Add("PaidBreakTime");//paid breaks. Is adjusted by pressing calcDaily.
			retVal.Columns.Add("Note");
			//Loop through employees.  Each employee adds one row to table --------------------------------------------------------------------------------
			List<ClockEvent> listClockEventsAllPlusPevWeek			=new List<ClockEvent>();
			List<ClockEvent> listClockEventsBreakAllPlusPrevWeek=new List<ClockEvent>();
			List<TimeAdjust> listTimeAdjustAllPlusPrevWeek			=new List<TimeAdjust>();
			List<Employee> listEmployees=Employees.GetForTimeCard();
			for(int e=0;e<listEmployees.Count;e++) {
				string employeeErrors="";
				string note="";
				DataRow dataRowCur=retVal.NewRow();
				dataRowCur.ItemArray.Initialize();//changes all nulls to blanks and zeros.
				//PayrollID-------------------------------------------------------------------------------------------------------------------------------------
				dataRowCur["PayrollID"]=listEmployees[e].PayrollID;
				//EmployeeNum and Name----------------------------------------------------------------------------------------------------------------------------------
				dataRowCur["EmployeeNum"]=listEmployees[e].EmployeeNum;
				dataRowCur["firstName"]=listEmployees[e].FName;
				dataRowCur["lastName"]=listEmployees[e].LName;
				//Begin calculations------------------------------------------------------------------------------------------------------------------------------------
				//each list below will contain one entry per week.
				List<TimeSpan> listTsRegularHoursWeekly			=new List<TimeSpan>();//Total non-overtime hours.  Used for calculation, not displayed or part of dataTable.
				List<TimeSpan> listTsOTHoursWeekly					=new List<TimeSpan>();//Total overtime hours.  Used for calculation, not displayed or part of dataTable.
				List<TimeSpan> listTsDifferentialHoursWeekly=new List<TimeSpan>();//Not included in total hours worked.  tsDifferentialHours is differant than r2Hours and r2OTHours
				//TimeSpan tsClockEventNetAdjust    =new TimeSpan();
				//TimeSpan tsTimeAdjust             =new TimeSpan();
				//TimeSpan tsTimeAdjustOT           =new TimeSpan();
				//TimeSpan tsPaidBreakTime          =new TimeSpan();
				List<ClockEvent> listClockEvents	=new List<ClockEvent>();
				List<TimeAdjust> listTimeAdjusts	=new List<TimeAdjust>();
				try {listClockEvents=ClockEvents.GetListForTimeCardManage(listEmployees[e].EmployeeNum,startDate,stopDate);}catch(Exception ex) {employeeErrors+=ex.Message;}
				try {listTimeAdjusts=TimeAdjusts.GetListForTimeCardManage(listEmployees[e].EmployeeNum,startDate,stopDate);}catch(Exception ex) {employeeErrors+=ex.Message;}
				//report errors in note column and move to next employee.----------------------------------------------------------------
				if(employeeErrors!="") {
					dataRowCur["Note"]=employeeErrors.Trim();
					retVal.Rows.Add(dataRowCur);
					continue;//display employee errors in note field for employee. All columns will be blank for just this employee.
				}
				//sum values for each week----------------------------------------------------------------------------------------------------
				List<DateTime> weekStartDates=weekStartHelper(startDate,stopDate);
				for(int i=0;i<weekStartDates.Count;i++) {
					listTsRegularHoursWeekly.Add(TimeSpan.Zero);			
					listTsOTHoursWeekly.Add(TimeSpan.Zero);
					listTsDifferentialHoursWeekly.Add(TimeSpan.Zero);
				}
				int weekCur=0;
				for(int i=0;i<listClockEvents.Count;i++) {
					//set current week for clock event
					for(int j=0;j<weekStartDates.Count;j++) {
						if(listClockEvents[i].TimeDisplayed1<weekStartDates[j].AddDays(7)) {
							weekCur=j;//clock event occurs during the week "j"
							break;
						}
					}
					if(i==0) {//we only want the comment from the first clock event entry.
						note=listClockEvents[i].Note;
					}
					//TimeDisplayed-----
					listTsRegularHoursWeekly[weekCur]+=listClockEvents[i].TimeDisplayed2-listClockEvents[i].TimeDisplayed1;
					//Adjusts-----
					if(listClockEvents[i].AdjustIsOverridden) {
						listTsRegularHoursWeekly[weekCur]+=listClockEvents[i].Adjust;
					}
					else {
						listTsRegularHoursWeekly[weekCur]+=listClockEvents[i].AdjustAuto;
					}
					//OverTime-----
					if(listClockEvents[i].OTimeHours!=TimeSpan.FromHours(-1)) {//Manual override
						listTsOTHoursWeekly[weekCur]+=listClockEvents[i].OTimeHours;
						listTsRegularHoursWeekly[weekCur]+=-listClockEvents[i].OTimeHours;
					}
					else {
						listTsOTHoursWeekly[weekCur]+=listClockEvents[i].OTimeAuto;
						listTsRegularHoursWeekly[weekCur]+=-listClockEvents[i].OTimeAuto;
					}
					//Differential/Rate2
					if(listClockEvents[i].Rate2Hours!=TimeSpan.FromHours(-1)) {//Manual override
						listTsDifferentialHoursWeekly[weekCur]+=listClockEvents[i].Rate2Hours;
					}
					else {
						listTsDifferentialHoursWeekly[weekCur]+=listClockEvents[i].Rate2Auto;
					}
				}
				//reset current week to itterate through time adjusts
				weekCur=0;
				for(int i=0;i<listTimeAdjusts.Count;i++) {//list of timeAdjusts have already been filtered. All timeAdjusts in this list are applicable.
					//set current week for time adjusts-----
					for(int j=0;j<weekStartDates.Count;j++) {
						if(listTimeAdjusts[i].TimeEntry<weekStartDates[j].AddDays(7)) {
							weekCur=j;//clock event occurs during the week "j"
							break;
						}
					}
					listTsRegularHoursWeekly[weekCur]+=listTimeAdjusts[i].RegHours;
					listTsOTHoursWeekly[weekCur]+=listTimeAdjusts[i].OTimeHours;
				}
				//Overtime should have already been calculated by CalcWeekly(); No calculations needed, just sum values.------------------------------------------------------
				double totalHoursWorked=0;
				double totalRegularHoursWorked=0;
				double totalOTHoursWorked=0;
				double totalDiffHoursWorked=0;
				//sum weekly totals.
				for(int i=0;i<weekStartDates.Count;i++){
					totalHoursWorked+=listTsRegularHoursWeekly[i].TotalHours;
					totalHoursWorked+=listTsOTHoursWeekly[i].TotalHours;
					totalRegularHoursWorked+=listTsRegularHoursWeekly[i].TotalHours;
					totalOTHoursWorked+=listTsOTHoursWeekly[i].TotalHours;
					totalDiffHoursWorked+=listTsDifferentialHoursWeekly[i].TotalHours;
				}
				//Regular time at R1 and R2
				double rate1ratio=0;
				if(totalHoursWorked!=0) {
					rate1ratio=1-totalDiffHoursWorked/totalHoursWorked;
				}
				dataRowCur["totalHours"]  =TimeSpan.FromHours(totalHoursWorked).ToString();
				double r1Hours=rate1ratio*totalRegularHoursWorked;
				double r2Hours=totalRegularHoursWorked-r1Hours;
				double r1OTHours=rate1ratio*totalOTHoursWorked;
				double r2OTHours=totalHoursWorked-r1Hours-r2Hours-r1OTHours;//"self correcting math" uses guaranteed to total correctly.
				dataRowCur["rate1Hours"]  =TimeSpan.FromHours(r1Hours).ToString();
				dataRowCur["rate2Hours"]  =TimeSpan.FromHours(r2Hours).ToString();
				dataRowCur["rate1OTHours"]=TimeSpan.FromHours(r1OTHours).ToString();
				dataRowCur["rate2OTHours"]=TimeSpan.FromHours(r2OTHours).ToString();
				dataRowCur["Note"]=note;
				retVal.Rows.Add(dataRowCur);
				continue;
			}
			return retVal;
		}

		/// <summary>Used to sum a partial weeks worth of regular hours from clock events and time spans.</summary>
		private static TimeSpan prevWeekRegHoursHelper(long employeeNum,DateTime startDate,DateTime endDate) {
			string errors="";
			List<ClockEvent> listCE=new List<ClockEvent>();
			List<TimeAdjust> listTA=new List<TimeAdjust>();
			try { listCE=ClockEvents.GetListForTimeCardManage(employeeNum,startDate,endDate); }catch(Exception ex) {	errors+=ex.Message;	}
			try { listTA=TimeAdjusts.GetListForTimeCardManage(employeeNum,startDate,endDate); }catch(Exception ex) { errors+=ex.Message; }
			TimeSpan retVal=TimeSpan.Zero;
			for(int i=0;i<listCE.Count;i++) {
				retVal+=listCE[i].TimeDisplayed2-listCE[i].TimeDisplayed1;
				if(listCE[i].AdjustIsOverridden) {//Manual override
					retVal+=listCE[i].Adjust;
				}
				else {
					retVal+=listCE[i].AdjustAuto;
				}
			}
			for(int i=0;i<listTA.Count;i++) {
				retVal+=listTA[i].RegHours;
			}
			return retVal;
		}

		/// <summary>Used to sum a partial weeks worth of OT hours from clock events and time spans.</summary>
		private static TimeSpan prevWeekOTHoursHelper(long employeeNum,DateTime startDate,DateTime endDate) {
			List<ClockEvent> listCE=ClockEvents.GetListForTimeCardManage(employeeNum,startDate,endDate);
			List<TimeAdjust> listTA=TimeAdjusts.GetListForTimeCardManage(employeeNum,startDate,endDate);
			TimeSpan retVal=TimeSpan.Zero;
			for(int i=0;i<listCE.Count;i++) {
				if(listCE[i].OTimeHours!=TimeSpan.FromHours(-1)) {//Manual override
					retVal+=listCE[i].OTimeHours;
				}
				else {
					retVal+=listCE[i].OTimeAuto;
				}
			}
			for(int i=0;i<listTA.Count;i++) {
				retVal+=listTA[i].OTimeHours;
			}
			return retVal;
		}

		/// <summary>Used to sum a partial weeks worth of rate2 hours from clock events.</summary>
		private static TimeSpan prevWeekDiffHoursHelper(long employeeNum,DateTime startDate,DateTime endDate) {
			List<ClockEvent> listCE=ClockEvents.GetListForTimeCardManage(employeeNum,startDate,endDate);
			TimeSpan retVal=TimeSpan.Zero;
			for(int i=0;i<listCE.Count;i++) {
				if(listCE[i].Rate2Hours!=TimeSpan.FromHours(-1)) {//Manual override
					retVal+=listCE[i].Rate2Hours;
				}
				else {
					retVal+=listCE[i].Rate2Auto;
				}
			}
			return retVal;
		}

		///<summary>Returns number of work weeks spanned by dates.  Example: "11-01-2013"(Friday), to "11-14-2013"(Thursday) spans 3 weeks, if the workweek starts on Sunday it would
		///return a list containing "10-27-2013"(Sunday),"11-03-2013"(Sunday),and"11-10-2013"(Sunday).  Used to determine which week time adjustments and clock events belong to when totalling timespans.</summary>
		private static List<DateTime> weekStartHelper(DateTime startDate,DateTime stopDate) {
			List<DateTime> retVal=new List<DateTime>();
			DayOfWeek fdow=(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek);
			for(int i=0;i<7;i++) {//start date of first week.
				if(startDate.AddDays(-i).DayOfWeek==fdow) {
					retVal.Add(startDate.AddDays(-i));//found and added start date of first week.
					break;
				}
			}
			while(retVal[retVal.Count-1].AddDays(7)<stopDate) {//add start of each workweek until we are past the dateStop
				retVal.Add(retVal[retVal.Count-1].AddDays(7));
			}
			return retVal;
		}

		/// <param name="isPrintReport">Only applicable to ODHQ. If true, will add ADP pay numer and note. The query takes about 9 seconds if this is set top true vs. about 2 seconds if set to false.</param>
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
					IFNULL(tempclockevent.Rate2Hours,'00:00:00') AS differential,
					SEC_TO_TIME(TIME_TO_SEC(tempbreak.BreakTime)+TIME_TO_SEC(AdjEvent)) AS BreakTime ";
			if(isPrintReport){
				command+=",tempclockevent.Note ";
			}
			command+=@"FROM clockevent	
				LEFT JOIN (SELECT ce.EmployeeNum,SEC_TO_TIME(IFNULL(SUM(UNIX_TIMESTAMP(ce.TimeDisplayed2)),0)-IFNULL(SUM(UNIX_TIMESTAMP(ce.TimeDisplayed1)),0)) AS TotalTime,
					SEC_TO_TIME(IFNULL(SUM(TIME_TO_SEC(CASE WHEN ce.OTimeHours='-01:00:00' THEN ce.OTimeAuto ELSE ce.OTimeHours END)),0)) AS OverTime,
					SEC_TO_TIME(IFNULL(SUM(TIME_TO_SEC(CASE WHEN ce.AdjustIsOverridden='1' THEN ce.Adjust ELSE ce.AdjustAuto END)),0)) AS AdjEvent,
					SEC_TO_TIME(SUM(UNIX_TIMESTAMP(CASE WHEN ce.Rate2Hours='-01:00:00' THEN ce.Rate2Auto ELSE ce.Rate2Hours END))) AS Rate2Hours";
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
			//TODO:add Rate2Hours and Rate2Auto Columns to report.
			command+=@"GROUP BY EmployeeNum
				ORDER BY employee.LName";
			return command;
		}

		///<summary>Returns all clock events, of all statuses, for a given employee between the date range (inclusive).</summary>
		internal static List<ClockEvent> GetSimpleList(long employeeNum,DateTime StartDate,DateTime StopDate) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClockEvent>>(MethodBase.GetCurrentMethod(),employeeNum,StartDate,StopDate);
			}
			//Fill list-----------------------------------------------------------------------------------------------------------------------------
			string command=
				"SELECT * FROM clockevent WHERE"
				+" EmployeeNum = '"+POut.Long(employeeNum)+"'"
				+" AND TimeDisplayed1 >= "+POut.Date(StartDate)
				+" AND TimeDisplayed1 < "+POut.Date(StopDate.AddDays(1))//adding a day takes it to midnight of the specified toDate
				+" ORDER BY TimeDisplayed1";
			return Crud.ClockEventCrud.SelectMany(command);
		}


	}

	
}




