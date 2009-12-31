using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClockEvents {
		///<summary>isBreaks is ignored if getAll is true.</summary>
		public static ClockEvent[] Refresh(long empNum,DateTime fromDate,DateTime toDate,bool getAll,bool isBreaks) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ClockEvent[]>(MethodBase.GetCurrentMethod(),empNum,fromDate,toDate,getAll,isBreaks);
			}
			string command=
				"SELECT * from clockevent WHERE"
				+" EmployeeNum = '"+POut.Long(empNum)+"'"
				+" AND TimeDisplayed >= "+POut.Date(fromDate)
				//adding a day takes it to midnight of the specified toDate
				+" AND TimeDisplayed <= "+POut.Date(toDate.AddDays(1));
			if(!getAll) {
				if(isBreaks)
					command+=" AND ClockStatus = '2'";
				else
					command+=" AND (ClockStatus = '0' OR ClockStatus = '1')";
			}
			command+=" ORDER BY TimeDisplayed";
			DataTable table=Db.GetTable(command);
			ClockEvent[] List=new ClockEvent[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new ClockEvent();
				List[i].ClockEventNum = PIn.Long(table.Rows[i][0].ToString());
				List[i].EmployeeNum   = PIn.Long(table.Rows[i][1].ToString());
				List[i].TimeEntered   = PIn.DateT(table.Rows[i][2].ToString());
				List[i].TimeDisplayed = PIn.DateT(table.Rows[i][3].ToString());
				List[i].ClockIn       = PIn.Bool(table.Rows[i][4].ToString());
				List[i].ClockStatus   =(TimeClockStatus)PIn.Long(table.Rows[i][5].ToString());
				List[i].Note          = PIn.String(table.Rows[i][6].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static long Insert(ClockEvent ce) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				ce.ClockEventNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ce);
				return ce.ClockEventNum;
			}
			DateTime serverTime=MiscData.GetNowDateTime();
			if(PrefC.RandomKeys) {
				ce.ClockEventNum=ReplicationServers.GetKey("clockevent","ClockEventNum");
			}
			string command="INSERT INTO clockevent (";
			if(PrefC.RandomKeys) {
				command+="ClockEventNum,";
			}
			command+="EmployeeNum,TimeEntered,TimeDisplayed,ClockIn"
				+",ClockStatus,Note) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.Long(ce.ClockEventNum)+"', ";
			}
			command+=
				 "'"+POut.Long   (ce.EmployeeNum)+"', "
				+POut.DateT (serverTime)+", "
				+POut.DateT (serverTime)+", "
				+"'"+POut.Bool  (ce.ClockIn)+"', "
				+"'"+POut.Long   ((int)ce.ClockStatus)+"', "
				+"'"+POut.String(ce.Note)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				ce.ClockEventNum=Db.NonQ(command,true);
			}
			return ce.ClockEventNum;
		}

		///<summary></summary>
		public static void Update(ClockEvent ce) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ce);
				return;
			}
			string command= "UPDATE clockevent SET "
				+"EmployeeNum = '"    +POut.Long   (ce.EmployeeNum)+"' "
				+",TimeEntered = "   +POut.DateT (ce.TimeEntered)+" "
				+",TimeDisplayed = " +POut.DateT (ce.TimeDisplayed)+" "
				+",ClockIn = '"       +POut.Bool  (ce.ClockIn)+"' "
				+",ClockStatus = '"   +POut.Long   ((int)ce.ClockStatus)+"' "
				+",Note = '"          +POut.String(ce.Note)+"' "
				+"WHERE ClockEventNum = '"+POut.Long(ce.ClockEventNum)+"'";
			Db.NonQ(command);
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
				if(events[i].TimeDisplayed.DayOfWeek > date.DayOfWeek){//eg, Friday > Thursday, so ignore
					continue;
				}
				if(i>0 && !events[i].ClockIn){
					retVal+=events[i].TimeDisplayed-events[i-1].TimeDisplayed;
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




