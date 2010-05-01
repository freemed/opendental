using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClockEvents {
		///<summary></summary>
		public static List<ClockEvent> Refresh(long empNum,DateTime fromDate,DateTime toDate,bool getAll,bool isBreaks){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClockEvent>>(MethodBase.GetCurrentMethod(),empNum,fromDate,toDate,getAll,isBreaks);
			}
			string command=
				"SELECT * FROM clockevent WHERE"
				+" EmployeeNum = '"+POut.Long(empNum)+"'"
				+" AND TimeDisplayed1 >= "+POut.Date(fromDate)
				//adding a day takes it to midnight of the specified toDate
				+" AND TimeDisplayed1 <= "+POut.Date(toDate.AddDays(1));
			if(!getAll) {
				if(isBreaks)
					command+=" AND ClockStatus = '2'";
				else
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
				+" ORDER BY TimeDisplayed1 DESC LIMIT 1";
			ClockEvent ce=Crud.ClockEventCrud.SelectOne(command);
			if(ce!=null && ce.ClockStatus==TimeClockStatus.Break && ce.TimeDisplayed2.Year>1880) {
				command="SELECT * FROM clockevent WHERE EmployeeNum="+POut.Long(employeeNum)+" "
					+"AND ClockStatus != 2 "//not a break
					+"ORDER BY TimeDisplayed1 DESC LIMIT 1";
				ce=Crud.ClockEventCrud.SelectOne(command);
				return ce;
			}
			else {
				return ce;
			}
		}

		/*
		///<summary>Gets directly from the database.  Returns true if the last time clock entry for this employee indicates that they are clocked in.</summary>
		public static bool IsClockedIn(long employeeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),employeeNum);
			}
			string command="SELECT * FROM clockevent WHERE EmployeeNum="+POut.Long(employeeNum)
				+" ORDER BY TimeDisplayed1 DESC LIMIT 1";
			ClockEvent ce=Crud.ClockEventCrud.SelectOne(command);
			if(ce==null) {//if this employee has never clocked in or out.
				return false;
			}
			if(ce.ClockStatus==TimeClockStatus.Break) {
				if(ce.TimeDisplayed2.Year<1880) {//clocked out for break, but not clocked back in
					return false;
				}
				else {//clocked back in from break
					return true;
				}
			}
			else {
				if(ce.TimeDisplayed2.Year<1880) {//clocked in to work, but not clocked back out.
					return true;
				}
				else {//clocked out for home or lunch
					return false;
				}
			}
		}

		///<summary>Gets info directly from database.  Also used to determine how to initially display timecard.  This really only matters if they are clocked out.</summary>
		public static TimeClockStatus GetLastStatus(long employeeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<TimeClockStatus>(MethodBase.GetCurrentMethod(),employeeNum);
			}
			string command="SELECT ClockStatus FROM clockevent WHERE EmployeeNum="+POut.Long(employeeNum)
				+" ORDER BY TimeDisplayed1 DESC LIMIT 1";
			ClockEvent ce=Crud.ClockEventCrud.SelectOne(command);
			if(ce==null) {//if this employee has never clocked in or out.
				return TimeClockStatus.Home;
			}
			if(ce.ClockStatus==TimeClockStatus.Break) {
				if(ce.TimeDisplayed2.Year<1880) {//clocked out for break, but not clocked back in
					return TimeClockStatus.Break;
				}
				else {//clocked back in from break
					return TimeClockStatus.Break;//shouldn't this be "working"?
				}
			}
			else {//normal clock event
				if(ce.TimeDisplayed2.Year<1880) {//clocked in to work, but not clocked back out.
					return TimeClockStatus.Home;//meaningless.  Should be "working".
				}
				else {//clocked out for home or lunch
					return ce.ClockStatus;
				}
			}
		}*/

		///<summary>Used in the timecard to track hours worked per week when the week started in a previous time period.  This gets all the hours of the first week before the date listed.  Also adds in any adjustments for that week.</summary>
		public static TimeSpan GetWeekTotal(long empNum,DateTime date) {
			//No need to check RemotingRole; no call to db.
			List<ClockEvent> events=Refresh(empNum,date.AddDays(-6),date.AddDays(-1),false,false);
			//eg, if this is Thursday, then we are getting last Friday through this Wed.
			TimeSpan retVal=new TimeSpan(0);
			for(int i=0;i<events.Count;i++){
				if(events[i].TimeDisplayed1.DayOfWeek > date.DayOfWeek){//eg, Friday > Thursday, so ignore
					continue;
				}
				retVal+=events[i].TimeDisplayed2-events[i].TimeDisplayed1;
			}
			//now, adjustments
			TimeAdjust[] TimeAdjustList=TimeAdjusts.Refresh(empNum,date.AddDays(-6),date.AddDays(-1));
			for(int i=0;i<TimeAdjustList.Length;i++) {
				if(TimeAdjustList[i].TimeEntry.DayOfWeek > date.DayOfWeek) {//eg, Friday > Thursday, so ignore
					continue;
				}
				retVal+=TimeAdjustList[i].RegHours;
			}
			return retVal;
		}

		///<summary>Normally hh:mm, unless isMinutes=true.  Then, it's mm.</summary>
		public static string Format(TimeSpan span) {
			return Format(span,false);
		}

		///<summary>(hh):mm</summary>
		public static string Format(TimeSpan span,bool showSeconds) {
			string retVal="";
			if(span < TimeSpan.Zero) {
				retVal+="-";
				span=span.Negate();
			}
			int hours=span.Days*24+span.Hours;
			if(hours>0) {
				retVal+=hours.ToString();
			}
			retVal+=":"+span.Minutes.ToString().PadLeft(2,'0');
			if(showSeconds) {
				retVal+=":"+span.Seconds.ToString().PadLeft(2,'0');
			}
			return retVal;
		}


	}

	
}




