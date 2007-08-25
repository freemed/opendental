using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Schedules {
		///<summary>Used in the schedule setup window</summary>
		public static Schedule[] RefreshMonth(DateTime CurDate,ScheduleType schedType,int provNum) {
			string command=
				//"SELECT * FROM schedule WHERE SchedDate > '"+POut.PDate(startDate.AddDays(-1))+"' "
				//+"AND SchedDate < '"+POut.PDate(stopDate.AddDays(1))+"' "
				/*"SELECT * FROM schedule WHERE MONTH(SchedDate)='"+CurDate.Month.ToString()
				+"' AND YEAR(SchedDate)='"+CurDate.Year.ToString()+"' "
				+"AND SchedType="+POut.PInt((int)schedType)
				+" AND ProvNum="+POut.PInt(provNum)
				+" ORDER BY starttime";*/
				"SELECT * FROM schedule WHERE ";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
				command+="TO_CHAR(SchedDate,'MM')='"+CurDate.Month.ToString()+"' AND TO_CHAR(SchedDate,'YYYY')";
			}else{//Assume MySQL
				command+="MONTH(SchedDate)='"+CurDate.Month.ToString()+"' AND YEAR(SchedDate)";
			}
			command+="='"+CurDate.Year.ToString()+"'"
				+" AND SchedType="+POut.PInt((int)schedType)
				+" AND ProvNum="+POut.PInt(provNum)
				+" ORDER BY starttime";
			return RefreshAndFill(command).ToArray();
		}

		/*
		///<summary>Called every time the day is refreshed or changed in Appointments module.  Gets the data directly from the database.</summary>
		public static Schedule[] RefreshDay(DateTime thisDay) {
			string command=
				"SELECT * FROM schedule WHERE SchedDate= "+POut.PDate(thisDay)
				+" ORDER BY StartTime";
			return RefreshAndFill(command).ToArray();
		}*/

		///<summary>Called every time the period is refreshed or changed in Appointments module.  Gets the data directly from the database.</summary>
		public static Schedule[] RefreshPeriod(DateTime startDate,DateTime endDate) {
			string command="SELECT * FROM schedule "
				+"WHERE SchedDate >= "+POut.PDate(startDate)+" "
				+"AND SchedDate <= "+POut.PDate(endDate)+" "
				+"ORDER BY StartTime";
			return RefreshAndFill(command).ToArray();
		}

		///<summary>Used in the Schedules edit window to get a filtered list of schedule items in preparation for paste or repeat.</summary>
		public static List<Schedule> RefreshPeriod(DateTime dateStart,DateTime dateEnd,int[] provNums,int[] empNums,
			bool includePractice)
		{
			if(provNums.Length==0 && empNums.Length==0 && !includePractice) {
				return new List<Schedule>();
			}
			string command="SELECT * FROM schedule "
				+"WHERE SchedDate >= "+POut.PDate(dateStart)+" "
				+"AND SchedDate <= "+POut.PDate(dateEnd)+" "
				+"AND (";
			string orClause="";//this is guaranteed to be non empty by the time the command is assembled.
			if(includePractice) {
				orClause="SchedType=0 ";
			}
			for(int i=0;i<provNums.Length;i++) {
				if(orClause!="") {
					orClause+="OR ";
				}
				orClause+="schedule.ProvNum="+POut.PInt(provNums[i])+" ";
			}
			for(int i=0;i<empNums.Length;i++) {
				if(orClause!="") {
					orClause+="OR ";
				}
				orClause+="schedule.EmployeeNum="+POut.PInt(empNums[i])+" ";
			}
			command+=orClause+")";
			return RefreshAndFill(command);
		}

		///<summary>The opNums array does not include 0.  0 indicates all ops and so it's always included in the output list.</summary>
		public static List<Schedule> RefreshPeriodBlockouts(DateTime dateStart,DateTime dateEnd,int[] opNums){
			string command="SELECT * FROM schedule "
				+"WHERE SchedDate >= "+POut.PDate(dateStart)+" "
				+"AND SchedDate <= "+POut.PDate(dateEnd)+" "
				+"AND SchedType=2 "//blockouts
				+"AND (Op=0 ";
			for(int i=0;i<opNums.Length;i++) {
				command+="OR Op="+POut.PInt(opNums[i])+" ";
			}
			command+=")";
			return RefreshAndFill(command);
		}

		///<summary></summary>
		public static List<Schedule> RefreshDayEdit(DateTime dateSched){
			string command="SELECT schedule.* "
				+"FROM schedule "//,provider "
				+"WHERE SchedDate = "+POut.PDate(dateSched)+" "
				+"AND (SchedType=0 OR SchedType=1 OR SchedType=3)";//Practice or Provider or Employee
			return RefreshAndFill(command);
		}

		///<summary>Used in the check database integrity tool.</summary>
		public static Schedule[] RefreshAll() {
			string command="SELECT * FROM schedule";
			return RefreshAndFill(command).ToArray();
		}

		private static List<Schedule> RefreshAndFill(string command) {
			DataTable table=General.GetTableEx(command);
			List<Schedule> retVal=new List<Schedule>();
			//Schedule[] List=new Schedule[table.Rows.Count];
			Schedule sched;
			for(int i=0;i<table.Rows.Count;i++) {
				sched=new Schedule();
				sched.ScheduleNum    = PIn.PInt(table.Rows[i][0].ToString());
				sched.SchedDate      = PIn.PDate(table.Rows[i][1].ToString());
				sched.StartTime      = PIn.PDateT(table.Rows[i][2].ToString());
				sched.StopTime       = PIn.PDateT(table.Rows[i][3].ToString());
				sched.SchedType      = (ScheduleType)PIn.PInt(table.Rows[i][4].ToString());
				sched.ProvNum        = PIn.PInt(table.Rows[i][5].ToString());
				sched.BlockoutType   = PIn.PInt(table.Rows[i][6].ToString());
				sched.Note           = PIn.PString(table.Rows[i][7].ToString());
				sched.Status         = (SchedStatus)PIn.PInt(table.Rows[i][8].ToString());
				sched.Op             = PIn.PInt(table.Rows[i][9].ToString());
				sched.EmployeeNum    = PIn.PInt(table.Rows[i][10].ToString());
				retVal.Add(sched);
			}
			return retVal;
		}

		///<summary></summary>
		private static void Update(Schedule sched){
			string command= "UPDATE schedule SET " 
				+ "scheddate = "    +POut.PDate  (sched.SchedDate)
				+ ",starttime = "   +POut.PDateT (sched.StartTime)
				+ ",stoptime = "    +POut.PDateT (sched.StopTime)
				+ ",SchedType = '"   +POut.PInt   ((int)sched.SchedType)+"'"
				+ ",ProvNum = '"     +POut.PInt   (sched.ProvNum)+"'"
				+ ",BlockoutType = '"+POut.PInt   (sched.BlockoutType)+"'"
				+ ",note = '"        +POut.PString(sched.Note)+"'"
				+ ",status = '"      +POut.PInt   ((int)sched.Status)+"'"
				+ ",Op = '"          +POut.PInt   (sched.Op)+"'"
				+ ",EmployeeNum = '" +POut.PInt   (sched.EmployeeNum)+"'"
				+" WHERE ScheduleNum = '" +POut.PInt (sched.ScheduleNum)+"'";
 			General.NonQ(command);
		}

		///<summary>This should not be used from outside this class unless proper validation is written similar to InsertOrUpdate.</summary>
		public static void Insert(Schedule sched){
			if(PrefB.RandomKeys){
				sched.ScheduleNum=MiscData.GetKey("schedule","ScheduleNum");
			}
			string command= "INSERT INTO schedule (";
			if(PrefB.RandomKeys){
				command+="ScheduleNum,";
			}
			command+="scheddate,starttime,stoptime,"
				+"SchedType,ProvNum,BlockoutType,Note,Status,Op,EmployeeNum) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(sched.ScheduleNum)+"', ";
			}
			command+=
				 POut.PDate  (sched.SchedDate)+", "
				+POut.PDateT (sched.StartTime)+", "
				+POut.PDateT (sched.StopTime)+", "
				+"'"+POut.PInt   ((int)sched.SchedType)+"', "
				+"'"+POut.PInt   (sched.ProvNum)+"', "
				+"'"+POut.PInt   (sched.BlockoutType)+"', "
				+"'"+POut.PString(sched.Note)+"', "
				+"'"+POut.PInt   ((int)sched.Status)+"', "
				+"'"+POut.PInt   (sched.Op)+"', "
				+"'"+POut.PInt   (sched.EmployeeNum)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				sched.ScheduleNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void InsertOrUpdate(Schedule sched, bool isNew){
			if(sched.StartTime.TimeOfDay > sched.StopTime.TimeOfDay){
				throw new Exception(Lan.g("Schedule","Stop time must be later than start time."));
			}
			if(sched.StartTime.TimeOfDay+TimeSpan.FromMinutes(5) > sched.StopTime.TimeOfDay
				&& sched.Status==SchedStatus.Open)
			{
				throw new Exception(Lan.g("Schedule","Stop time cannot be the same as the start time."));
			}
			if(Overlaps(sched)){
				throw new Exception(Lan.g("Schedule","Cannot overlap another time block."));
			}
			if(isNew){
				Insert(sched);
			}
			else{
				Update(sched);
			}
		}

		///<summary></summary>
		private static bool Overlaps(Schedule sched){
			Schedule[] SchedListDay=Schedules.RefreshPeriod(sched.SchedDate,sched.SchedDate);
			Schedule[] ListForType=Schedules.GetForType(SchedListDay,sched.SchedType,sched.ProvNum);
			for(int i=0;i<ListForType.Length;i++){
				if(ListForType[i].SchedType==ScheduleType.Blockout){
					//skip if blockout, and ops don't interfere
					if(sched.Op!=0 && ListForType[i].Op!=0){//neither op can be zero, or they will interfere
						if(sched.Op != ListForType[i].Op){
							continue;
						}
					}
				}
				if(sched.ScheduleNum!=ListForType[i].ScheduleNum
					&& sched.StartTime.TimeOfDay >= ListForType[i].StartTime.TimeOfDay
					&& sched.StartTime.TimeOfDay < ListForType[i].StopTime.TimeOfDay)
				{
					return true;
				}
				if(sched.ScheduleNum!=ListForType[i].ScheduleNum
					&& sched.StopTime.TimeOfDay > ListForType[i].StartTime.TimeOfDay
					&& sched.StopTime.TimeOfDay <= ListForType[i].StopTime.TimeOfDay)
				{
					return true;
				}
				if(sched.ScheduleNum!=ListForType[i].ScheduleNum
					&& sched.StartTime.TimeOfDay <= ListForType[i].StartTime.TimeOfDay
					&& sched.StopTime.TimeOfDay >= ListForType[i].StopTime.TimeOfDay)
				{
					return true;
				}
			}
			return false;
		}

		///<summary></summary>
		public static void Delete(Schedule sched){
			string command= "DELETE from schedule WHERE schedulenum = '"+POut.PInt(sched.ScheduleNum)+"'";
 			General.NonQ(command);
			//if this was the last blockout for a day, then create a blockout for 'closed'
			//if(sched.SchedType==ScheduleType.Blockout){
			//	Schedules.CheckIfDeletedLastBlockout(sched.SchedDate);
			//}
		}
	
		///<summary>Supply a list of all Schedule for one day. Then, this filters out for one type.</summary>
		public static Schedule[] GetForType(Schedule[] ListDay,ScheduleType schedType,int provNum){
			ArrayList AL=new ArrayList();
			for(int i=0;i<ListDay.Length;i++){
				if(ListDay[i].SchedType==schedType && ListDay[i].ProvNum==provNum){
					AL.Add(ListDay[i]);
				}
			}
			Schedule[] retVal=new Schedule[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		/*
		///<summary>If a particular day already has some non-default schedule items, then this does nothing and returns false.  But if the day is all default, then it converts each default entry into an actual schedule entry and returns true.  The user would not notice this change, but now they can edit or add.</summary>
		public static bool ConvertFromDefault(DateTime forDate,ScheduleType schedType,int provNum){
			Schedule[] List=RefreshDay(forDate);
			Schedule[] ListForType=GetForType(List,schedType,provNum);
			if(ListForType.Length>0){
				return false;//do nothing, since it has already been converted from default.
				//it is also possible there will be no default entries to convert, but that's ok.
			}
			SchedDefault[] ListDefaults=SchedDefaults.GetForType(schedType,provNum);
			for(int i=0;i<ListDefaults.Length;i++){
				if(ListDefaults[i].DayOfWeek!=(int)forDate.DayOfWeek){
					continue;//if day of week doesn't match, then skip
				}
				Schedule SchedCur=new Schedule();
				SchedCur.Status=SchedStatus.Open;
				SchedCur.SchedDate=forDate;
				SchedCur.StartTime=ListDefaults[i].StartTime;
				SchedCur.StopTime=ListDefaults[i].StopTime; 
				SchedCur.SchedType=schedType;
				SchedCur.ProvNum=provNum;
				SchedCur.Op=ListDefaults[i].Op;
				SchedCur.BlockoutType=ListDefaults[i].BlockoutType;
				InsertOrUpdate(SchedCur,true);     
			}
			return true;
		}

		///<summary></summary>
		public static void SetAllDefault(DateTime forDate,ScheduleType schedType,int provNum){
			string command="DELETE from schedule WHERE "
				+"SchedDate="    +POut.PDate (forDate)+" "
				+"AND SchedType='"+POut.PInt((int)schedType)+"' "
				+"AND ProvNum='"  +POut.PInt(provNum)+"'";
 			General.NonQ(command);
		}*/

		///<summary>Clears all blockouts for day.  But then defaults would show.  So adds a "closed" blockout.</summary>
		public static void ClearBlockoutsForDay(DateTime date){
			//SetAllDefault(date,ScheduleType.Blockout,0);
			string command="DELETE from schedule WHERE "
				+"SchedDate="    +POut.PDate(date)+" "
				+"AND SchedType='"+POut.PInt((int)ScheduleType.Blockout)+"' ";
				//+"AND ProvNum='"  +POut.PInt(provNum)+"'";
			General.NonQ(command);
			//CheckIfDeletedLastBlockout(date);
		}

		/*
		///<summary></summary>
		public static void CheckIfDeletedLastBlockout(DateTime schedDate){
			string command="SELECT COUNT(*) FROM schedule WHERE SchedType='"+POut.PInt((int)ScheduleType.Blockout)+"' "
					+"AND SchedDate="+POut.PDate(schedDate);
			DataTable table=General.GetTable(command);
			if(table.Rows[0][0].ToString()=="0") {
				Schedule sched=new Schedule();
				sched.SchedDate=schedDate;
				sched.SchedType=ScheduleType.Blockout;
				sched.Status=SchedStatus.Closed;
				Insert(sched);
			}
		}*/

		public static bool DateIsHoliday(DateTime date){
			string command="SELECT COUNT(*) FROM schedule WHERE Status=2 "//holiday
				+"AND SchedType=0 "//practice
				+"AND SchedDate= "+POut.PDate(date);
			string result=General.GetCount(command);
			if(result=="0"){
				return false;
			}
			return true;
		}

		///<summary>Returns a 7 column data table in a calendar layout so all you have to do is draw it on the screen.  If includePractice is true, then practice notes and holidays will be included.</summary>
		public static DataTable GetPeriod(DateTime dateStart,DateTime dateEnd,int[] provNums,int[] empNums,bool includePractice){
			DataTable table=new DataTable();
			DataRow row;
			table.Columns.Add("sun");
			table.Columns.Add("mon");
			table.Columns.Add("tues");
			table.Columns.Add("wed");
			table.Columns.Add("thurs");
			table.Columns.Add("fri");
			table.Columns.Add("sat");
			if(provNums.Length==0 && empNums.Length==0 && !includePractice){
				return table;
			}
			string command="SELECT Abbr,employee.FName,Note,SchedDate,SchedType,Status,StartTime,StopTime "
				+"FROM schedule "
				+"LEFT JOIN provider ON schedule.ProvNum=provider.ProvNum "
				+"LEFT JOIN employee ON schedule.EmployeeNum=employee.EmployeeNum "
				+"WHERE SchedDate >= "+POut.PDate(dateStart)+" "
				+"AND SchedDate <= "+POut.PDate(dateEnd)+" "
				+"AND (";
			string orClause="";//this is guaranteed to be non empty by the time the command is assembled.
			if(includePractice){
				orClause="SchedType=0 ";
			}
			for(int i=0;i<provNums.Length;i++){
				if(orClause!=""){
					orClause+="OR ";
				}
				orClause+="schedule.ProvNum="+POut.PInt(provNums[i])+" ";
			}
			for(int i=0;i<empNums.Length;i++) {
				if(orClause!="") {
					orClause+="OR ";
				}
				orClause+="schedule.EmployeeNum="+POut.PInt(empNums[i])+" ";
			}
			command+=orClause+") ";
			//if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
			//	command+="";
			//}
			//else{
				command+="ORDER BY SchedDate,employee.FName,provider.ItemOrder,StartTime";
			//}
			DataTable raw=General.GetTable(command);
			DateTime dateSched;
			DateTime startTime;
			DateTime stopTime;
			int rowsInGrid=GetRowCal(dateStart,dateEnd)+1;//because 0-based
			for(int i=0;i<rowsInGrid;i++){
				row=table.NewRow();
				table.Rows.Add(row);
			}
			dateSched=dateStart;
			while(dateSched<=dateEnd){
				table.Rows[GetRowCal(dateStart,dateSched)][(int)dateSched.DayOfWeek]=
					dateSched.ToString("MMM d");
				dateSched=dateSched.AddDays(1);
			}
			int rowI;
			for(int i=0;i<raw.Rows.Count;i++){
				dateSched=PIn.PDate(raw.Rows[i]["SchedDate"].ToString());
				startTime=PIn.PDateT(raw.Rows[i]["StartTime"].ToString());
				stopTime=PIn.PDateT(raw.Rows[i]["StopTime"].ToString());
				rowI=GetRowCal(dateStart,dateSched);
				if(i!=0//not first row
					&& raw.Rows[i-1]["Abbr"].ToString()==raw.Rows[i]["Abbr"].ToString()//same provider as previous row
					&& raw.Rows[i-1]["FName"].ToString()==raw.Rows[i]["FName"].ToString()//same employee as previous row
					&& raw.Rows[i-1]["SchedDate"].ToString()==raw.Rows[i]["SchedDate"].ToString())//and same date as previous row
				{
					table.Rows[rowI][(int)dateSched.DayOfWeek]+=", ";
					if(startTime.TimeOfDay==PIn.PDateT("12 AM").TimeOfDay
						&& stopTime.TimeOfDay==PIn.PDateT("12 AM").TimeOfDay)
					{
						if(raw.Rows[i]["Status"].ToString()=="2") {//if holiday
							table.Rows[rowI][(int)dateSched.DayOfWeek]+=Lan.g("Schedules","Holiday:");
						}
					}
					else{
						table.Rows[rowI][(int)dateSched.DayOfWeek]+=startTime.ToString("h:mm")+"-"+stopTime.ToString("h:mm");
					}
				}
				else{
					table.Rows[rowI][(int)dateSched.DayOfWeek]+="\r\n";
					if(startTime.TimeOfDay==PIn.PDateT("12 AM").TimeOfDay
						&& stopTime.TimeOfDay==PIn.PDateT("12 AM").TimeOfDay)
					{
						if(raw.Rows[i]["Status"].ToString()=="2"){//if holiday
							table.Rows[rowI][(int)dateSched.DayOfWeek]+=Lan.g("Schedules","Holiday:");//+raw.Rows[i]["Note"].ToString();
						}
						else{//note
							if(raw.Rows[i]["Abbr"].ToString()!=""){
								table.Rows[rowI][(int)dateSched.DayOfWeek]+=raw.Rows[i]["Abbr"].ToString()+" ";
							}
							if(raw.Rows[i]["FName"].ToString()!="") {
								table.Rows[rowI][(int)dateSched.DayOfWeek]+=raw.Rows[i]["FName"].ToString()+" ";
							}
							//table.Rows[rowI][(int)dateSched.DayOfWeek]+=raw.Rows[i]["Note"].ToString();
						}
					}
					else{
						if(raw.Rows[i]["Abbr"].ToString()!="") {
							table.Rows[rowI][(int)dateSched.DayOfWeek]+=raw.Rows[i]["Abbr"].ToString()+" ";
						}
						if(raw.Rows[i]["FName"].ToString()!="") {
							table.Rows[rowI][(int)dateSched.DayOfWeek]+=raw.Rows[i]["FName"].ToString()+" ";
						}
						table.Rows[rowI][(int)dateSched.DayOfWeek]+=
							startTime.ToString("h:mm")+"-"+stopTime.ToString("h:mm");
					}
				}
				if(raw.Rows[i]["Note"].ToString()!="") {
					table.Rows[rowI][(int)dateSched.DayOfWeek]+=" "+raw.Rows[i]["Note"].ToString();
				}
			}
			return table;
		}

		///<summary>Returns the 0-based row where endDate will fall in a calendar grid.  It is not necessary to have a function to retrieve the column, because that is simply (int)myDate.DayOfWeek</summary>
		public static int GetRowCal(DateTime startDate,DateTime endDate){
			TimeSpan span=endDate-startDate;
			int dayInterval=span.Days;
			int daysFirstWeek=7-(int)startDate.DayOfWeek;//eg Monday=7-1=6.  or Sat=7-6=1.
			dayInterval=dayInterval-daysFirstWeek;
			if(dayInterval<0){
				return 0;
			}
			return (int)Math.Ceiling((dayInterval+1)/7d);
		}

		///<summary>When click on a calendar grid, this is used to calculate the date clicked on.  StartDate is the first date in the Calendar, which does not have to be Sun.</summary>
		public static DateTime GetDateCal(DateTime startDate,int row,int col){
			DateTime dateFirstRow;//the first date of row 0. Typically a few days before startDate. Always a Sun.
			dateFirstRow=startDate.AddDays(-(int)startDate.DayOfWeek);//example: (Tues,May 9).AddDays(-2)=Sun,May 7.
			return dateFirstRow.AddDays(row*7+col);
		}

		///<summary>Surround with try/catch.  Deletes all existing practice, provider, and employee schedules for a day and then saves the provided list.</summary>
		public static void SetForDay(List<Schedule> SchedList,DateTime schedDate){
			for(int i=0;i<SchedList.Count;i++){
				if(SchedList[i].StartTime.TimeOfDay > SchedList[i].StopTime.TimeOfDay) {
					throw new Exception(Lan.g("Schedule","Stop time must be later than start time."));
				}
			}
			string command="DELETE FROM schedule WHERE SchedDate= "+POut.PDate(schedDate)+" "
				+"AND (SchedType=0 OR SchedType=1 OR SchedType=3)";
			General.NonQ(command);
			for(int i=0;i<SchedList.Count;i++){
				Insert(SchedList[i]);
			}
		}

		///<summary>Clears all schedule entries for the given date range and the given providers, employees, and practice.</summary>
		public static void Clear(DateTime dateStart,DateTime dateEnd,int[] provNums,int[] empNums,bool includePractice){
			if(provNums.Length==0 && empNums.Length==0 && !includePractice) {
				return;
			}
			string command="DELETE FROM schedule "
				+"WHERE SchedDate >= "+POut.PDate(dateStart)+" "
				+"AND SchedDate <= "+POut.PDate(dateEnd)+" "
				+"AND (";
			string orClause="";//this is guaranteed to be non empty by the time the command is assembled.
			if(includePractice) {
				orClause="SchedType=0 ";
			}
			for(int i=0;i<provNums.Length;i++) {
				if(orClause!="") {
					orClause+="OR ";
				}
				orClause+="schedule.ProvNum="+POut.PInt(provNums[i])+" ";
			}
			for(int i=0;i<empNums.Length;i++) {
				if(orClause!="") {
					orClause+="OR ";
				}
				orClause+="schedule.EmployeeNum="+POut.PInt(empNums[i])+" ";
			}
			command+=orClause+")";
			General.NonQ(command);
		}

		///<summary>Clears all Blockout schedule entries for the given date range and the given ops.</summary>
		public static void ClearBlockouts(DateTime dateStart,DateTime dateEnd,int[] opNums) {
			string command="DELETE FROM schedule "
				+"WHERE SchedDate >= "+POut.PDate(dateStart)+" "
				+"AND SchedDate <= "+POut.PDate(dateEnd)+" "
				+"AND SchedType=2 "//blockouts
				+"AND (Op=0 ";
			for(int i=0;i<opNums.Length;i++) {
				command+="OR Op="+POut.PInt(opNums[i])+" ";
			}
			command+=")";
			General.NonQ(command);
		}

		
	}

	

	

}













