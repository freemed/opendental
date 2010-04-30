using System;
using System.Collections;
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
				+" AND TimeDisplayedIn >= "+POut.Date(fromDate)
				//adding a day takes it to midnight of the specified toDate
				+" AND TimeDisplayedIn <= "+POut.Date(toDate.AddDays(1));
			if(!getAll) {
				if(isBreaks)
					command+=" AND ClockStatus = '2'";
				else
					command+=" AND (ClockStatus = '0' OR ClockStatus = '1')";
			}
			command+=" ORDER BY TimeDisplayed";
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
		public static void Delete(ClockEvent ce) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ce);
				return;
			}
			string command= "DELETE FROM clockevent WHERE ClockEventNum = "+POut.Long(ce.ClockEventNum);
			Db.NonQ(command);
		}

		///<summary>Gets directly from the database.  Returns true if the last time clock entry for this employee was a clockin.</summary>
		public static bool IsClockedIn(long employeeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),employeeNum);
			}
			string command="SELECT ClockIn FROM clockevent WHERE EmployeeNum="+POut.Long(employeeNum)
				+" ORDER BY TimeDisplayed DESC ";
			if(DataConnection.DBtype==DatabaseType.Oracle){
				command="SELECT * FROM ("+command+") WHERE ROWNUM<=1";
			}else{//Assume MySQL
				command+=" LIMIT 1";
			}
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0)//if this employee has never clocked in or out.
				return false;
			if(PIn.Bool(table.Rows[0][0].ToString())){//if the last clockevent was a clockin
				return true;
			}
			return false;
		}

		///<summary>Gets info directly from database.  If the employee is clocked out, this gets the status for clockin is based on how they last clocked out.  Also used to determine how to initially display timecard.</summary>
		public static TimeClockStatus GetLastStatus(long employeeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<TimeClockStatus>(MethodBase.GetCurrentMethod(),employeeNum);
			}
			string command="SELECT ClockStatus FROM clockevent WHERE EmployeeNum="+POut.Long(employeeNum)
				+" ORDER BY TimeDisplayed DESC ";
			if(DataConnection.DBtype==DatabaseType.Oracle){
				command="SELECT * FROM ("+command+") WHERE ROWNUM<=1";
			}else{//Assum MySQL
				command+="LIMIT 1";
			}
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0)//if this employee has never clocked in or out.
				return TimeClockStatus.Home;
			return (TimeClockStatus)PIn.Long(table.Rows[0][0].ToString());
		}

		///<summary></summary>
		public static DateTime GetServerTime(){
			//No need to check RemotingRole; no call to db.
			return MiscData.GetNowDateTime();
		}

		///<summary>Used in the timecard to track hours worked per week when the week started in a previous time period.  This gets all the hours of the first week before the date listed.  Also adds in any adjustments for that week.</summary>
		public static TimeSpan GetWeekTotal(long empNum,DateTime date) {
			//No need to check RemotingRole; no call to db.
			ClockEvent[] events=Refresh(empNum,date.AddDays(-6),date.AddDays(-1),false,false);
			//eg, if this is Thursday, then we are getting last Friday through this Wed.
			TimeSpan retVal=new TimeSpan(0);
			for(int i=0;i<events.Length;i++){
				if(events[i].TimeDisplayedIn.DayOfWeek > date.DayOfWeek){//eg, Friday > Thursday, so ignore
					continue;
				}
				if(i>0 && !events[i].ClockIn){
					retVal+=events[i].TimeDisplayedIn-events[i-1].TimeDisplayedIn;
				}
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




	}

	
}




