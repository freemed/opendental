using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ClockEvents {
		///<summary>isBreaks is ignored if getAll is true.</summary>
		public static ClockEvent[] Refresh(int empNum,DateTime fromDate,DateTime toDate,bool getAll,bool isBreaks) {
			string command=
				"SELECT * from clockevent WHERE"
				+" EmployeeNum = '"+POut.PInt(empNum)+"'"
				+" AND TimeDisplayed >= "+POut.PDate(fromDate)
				//adding a day takes it to midnight of the specified toDate
				+" AND TimeDisplayed <= "+POut.PDate(toDate.AddDays(1));
			if(!getAll) {
				if(isBreaks)
					command+=" AND ClockStatus = '2'";
				else
					command+=" AND (ClockStatus = '0' OR ClockStatus = '1')";
			}
			command+=" ORDER BY TimeDisplayed";
			DataTable table=General.GetTable(command);
			ClockEvent[] List=new ClockEvent[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new ClockEvent();
				List[i].ClockEventNum = PIn.PInt(table.Rows[i][0].ToString());
				List[i].EmployeeNum   = PIn.PInt(table.Rows[i][1].ToString());
				List[i].TimeEntered   = PIn.PDateT(table.Rows[i][2].ToString());
				List[i].TimeDisplayed = PIn.PDateT(table.Rows[i][3].ToString());
				List[i].ClockIn       = PIn.PBool(table.Rows[i][4].ToString());
				List[i].ClockStatus   =(TimeClockStatus)PIn.PInt(table.Rows[i][5].ToString());
				List[i].Note          = PIn.PString(table.Rows[i][6].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Insert(ClockEvent ce) {
			if(PrefB.RandomKeys) {
				ce.ClockEventNum=MiscData.GetKey("clockevent","ClockEventNum");
			}
			string command="INSERT INTO clockevent (";
			if(PrefB.RandomKeys) {
				command+="ClockEventNum,";
			}
			command+="EmployeeNum,TimeEntered,TimeDisplayed,ClockIn"
				+",ClockStatus,Note) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(ce.ClockEventNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (ce.EmployeeNum)+"', "
				+POut.PDateT (ce.TimeEntered)+", "
				+POut.PDateT (ce.TimeDisplayed)+", "
				+"'"+POut.PBool  (ce.ClockIn)+"', "
				+"'"+POut.PInt   ((int)ce.ClockStatus)+"', "
				+"'"+POut.PString(ce.Note)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				ce.ClockEventNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(ClockEvent ce) {
			string command= "UPDATE clockevent SET "
				+"EmployeeNum = '"    +POut.PInt   (ce.EmployeeNum)+"' "
				+",TimeEntered = "   +POut.PDateT (ce.TimeEntered)+" "
				+",TimeDisplayed = " +POut.PDateT (ce.TimeDisplayed)+" "
				+",ClockIn = '"       +POut.PBool  (ce.ClockIn)+"' "
				+",ClockStatus = '"   +POut.PInt   ((int)ce.ClockStatus)+"' "
				+",Note = '"          +POut.PString(ce.Note)+"' "
				+"WHERE ClockEventNum = '"+POut.PInt(ce.ClockEventNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ClockEvent ce) {
			string command= "DELETE FROM clockevent WHERE ClockEventNum = "+POut.PInt(ce.ClockEventNum);
			General.NonQ(command);
		}

		///<summary>Gets directly from the database.  Returns true if the last time clock entry for this employee was a clockin.</summary>
		public static bool IsClockedIn(int employeeNum){
			string command="SELECT ClockIn FROM clockevent WHERE EmployeeNum="+POut.PInt(employeeNum)
				+" ORDER BY TimeDisplayed DESC ";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
				command="SELECT * FROM ("+command+") WHERE ROWNUM<=1";
			}else{//Assume MySQL
				command+=" LIMIT 1";
			}
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0)//if this employee has never clocked in or out.
				return false;
			if(PIn.PBool(table.Rows[0][0].ToString())){//if the last clockevent was a clockin
				return true;
			}
			return false;
		}

		///<summary>Gets info directly from database.  If the employee is clocked out, this gets the status for clockin is based on how they last clocked out.  Also used to determine how to initially display timecard.</summary>
		public static TimeClockStatus GetLastStatus(int employeeNum){
			string command="SELECT ClockStatus FROM clockevent WHERE EmployeeNum="+POut.PInt(employeeNum)
				+" ORDER BY TimeDisplayed DESC ";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
				command="SELECT * FROM ("+command+") WHERE ROWNUM<=1";
			}else{//Assum MySQL
				command+="LIMIT 1";
			}
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0)//if this employee has never clocked in or out.
				return TimeClockStatus.Home;
			return (TimeClockStatus)PIn.PInt(table.Rows[0][0].ToString());
		}

		///<summary></summary>
		public static DateTime GetServerTime(){
			return MiscData.GetNowDateTime();
		}

		///<summary>Used in the timecard to track hours worked per week when the week started in a previous time period.  This gets all the hours of the first week before the date listed.  Also adds in any adjustments for that week.</summary>
		public static TimeSpan GetWeekTotal(int empNum,DateTime date){
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




