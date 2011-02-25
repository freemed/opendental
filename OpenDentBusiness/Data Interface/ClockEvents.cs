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
				+" AND TimeDisplayed1 <= "+POut.Date(toDate.AddDays(1));
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
			List<ClockEvent> events=Refresh(empNum,date.AddDays(-6),date.AddDays(-1),false);
			//eg, if this is Thursday, then we are getting last Friday through this Wed.
			TimeSpan retVal=new TimeSpan(0);
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


	}

	
}




