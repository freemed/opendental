using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Schedules {		
		///<summary>Gets a list of Schedule items for one date.</summary>
		public static List<Schedule> GetDayList(DateTime date){
			string command="SELECT * FROM schedule "
				+"WHERE SchedDate = "+POut.PDate(date)+" "
				+"ORDER BY StartTime";
			return RefreshAndFill(command);
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

		///<summary></summary>
		public static List<Schedule> RefreshPeriodBlockouts(DateTime dateStart,DateTime dateEnd,List<int> opNums){
			if(opNums.Count==0){
				return new List<Schedule>();
			}
			string command="SELECT schedule.* "
				+"FROM schedule,scheduleop "
				+"WHERE schedule.ScheduleNum=scheduleop.ScheduleNum "
				+"AND SchedDate >= "+POut.PDate(dateStart)+" "
				+"AND SchedDate <= "+POut.PDate(dateEnd)+" "
				+"AND SchedType=2 "//blockouts
				+"AND (";//OperatoryNum=0 ";
			for(int i=0;i<opNums.Count;i++) {
				if(i>0){
					command+=" OR ";
				}
				command+="OperatoryNum="+POut.PInt(opNums[i]);
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

		public static List<Schedule> GetUAppoint(DateTime changedSince){
			string command="SELECT * FROM schedule WHERE DateTStamp > "+POut.PDateT(changedSince)
				+" AND SchedType="+POut.PInt((int)ScheduleType.Provider);
			return RefreshAndFill(command);
		}

		private static List<Schedule> RefreshAndFill(string command) {
			//The GROUP_CONCAT() function returns a comma separated list of items.
			//In this case, the ops column is filled with a comma separated list of
			//operatories for the corresponding schedule record.
			command="SELECT s.*,"+
				"IFNULL(CAST((SELECT GROUP_CONCAT(so.OperatoryNum) "+
					"FROM scheduleop so "+
					"WHERE so.ScheduleNum=s.ScheduleNum "+
					"GROUP BY so.ScheduleNum) AS CHAR(4000)),'') ops "+
				"FROM ("+command+") s";
			DataTable table=Db.GetTable(command);
			return ConvertTableToList(table);
		}

		public static List<Schedule> ConvertTableToList(DataTable table){
			List<Schedule> retVal=new List<Schedule>();
			//Schedule[] List=new Schedule[table.Rows.Count];
			Schedule sched;
			string opstr;
			string[] oparray;
			for(int i=0;i<table.Rows.Count;i++) {
				sched=new Schedule();
				sched.ScheduleNum    = PIn.PInt   (table.Rows[i]["ScheduleNum"].ToString());
				sched.SchedDate      = PIn.PDate  (table.Rows[i]["SchedDate"].ToString());
				sched.StartTime      = PIn.PDateT (table.Rows[i]["StartTime"].ToString());
				sched.StopTime       = PIn.PDateT (table.Rows[i]["StopTime"].ToString());
				sched.SchedType      = (ScheduleType)PIn.PInt(table.Rows[i]["SchedType"].ToString());
				sched.ProvNum        = PIn.PInt   (table.Rows[i]["ProvNum"].ToString());
				sched.BlockoutType   = PIn.PInt   (table.Rows[i]["BlockoutType"].ToString());
				sched.Note           = PIn.PString(table.Rows[i]["Note"].ToString());
				sched.Status         = (SchedStatus)PIn.PInt(table.Rows[i]["Status"].ToString());
				sched.EmployeeNum    = PIn.PInt   (table.Rows[i]["EmployeeNum"].ToString());
				if(table.Columns.Contains("ops")){
					sched.Ops=new List<int>();
					opstr=PIn.PString(table.Rows[i]["ops"].ToString());
					if(opstr!=""){
						oparray=opstr.Split(',');
						for(int o=0;o<oparray.Length;o++){
							sched.Ops.Add(PIn.PInt(oparray[o]));
						}
					}
				}
				retVal.Add(sched);
			}
			return retVal;
		}

		///<summary></summary>
		private static void Update(Schedule sched){
			string command= "UPDATE schedule SET " 
				+ "SchedDate = "    +POut.PDate  (sched.SchedDate)
				+ ",StartTime = "   +POut.PDateT (sched.StartTime)
				+ ",StopTime = "    +POut.PDateT (sched.StopTime)
				+ ",SchedType = '"   +POut.PInt   ((int)sched.SchedType)+"'"
				+ ",ProvNum = '"     +POut.PInt   (sched.ProvNum)+"'"
				+ ",BlockoutType = '"+POut.PInt   (sched.BlockoutType)+"'"
				+ ",Note = '"        +POut.PString(sched.Note)+"'"
				+ ",Status = '"      +POut.PInt   ((int)sched.Status)+"'"
				+ ",EmployeeNum = '" +POut.PInt   (sched.EmployeeNum)+"'"
				+" WHERE ScheduleNum = '" +POut.PInt (sched.ScheduleNum)+"'";
 			Db.NonQ(command);
			command="DELETE FROM scheduleop WHERE ScheduleNum="+POut.PInt (sched.ScheduleNum);
			Db.NonQ(command);
			ScheduleOp op;
			for(int i=0;i<sched.Ops.Count;i++){
				op=new ScheduleOp();
				op.ScheduleNum=sched.ScheduleNum;
				op.OperatoryNum=sched.Ops[i];
				ScheduleOps.Insert(op);
			}
		}

		///<summary>This should not be used from outside this class unless proper validation is written similar to InsertOrUpdate.  It's currently used a lot for copy/paste situations, where most of the validation is not needed.</summary>
		public static void Insert(Schedule sched){
			if(PrefC.RandomKeys){
				sched.ScheduleNum=MiscData.GetKey("schedule","ScheduleNum");
			}
			string command= "INSERT INTO schedule (";
			if(PrefC.RandomKeys){
				command+="ScheduleNum,";
			}
			command+="scheddate,starttime,stoptime,"
				+"SchedType,ProvNum,BlockoutType,Note,Status,EmployeeNum) VALUES(";
			if(PrefC.RandomKeys){
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
				+"'"+POut.PInt   (sched.EmployeeNum)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				sched.ScheduleNum=Db.NonQ(command,true);
			}
			ScheduleOp op;
			for(int i=0;i<sched.Ops.Count;i++){
				op=new ScheduleOp();
				op.ScheduleNum=sched.ScheduleNum;
				op.OperatoryNum=sched.Ops[i];
				ScheduleOps.Insert(op);
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
			List<Schedule> SchedListDay=Schedules.GetDayList(sched.SchedDate);
			Schedule[] ListForType=Schedules.GetForType(SchedListDay,sched.SchedType,sched.ProvNum);
			bool opsMatch;
			for(int i=0;i<ListForType.Length;i++){
				if(ListForType[i].SchedType==ScheduleType.Blockout){
					//skip if blockout, and ops don't interfere
					opsMatch=false;
					for(int s1=0;s1<sched.Ops.Count;s1++){
						if(ListForType[i].Ops.Contains(sched.Ops[s1])){
							opsMatch=true;
							break;
						}
					}
					if(!opsMatch){
						continue;
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
 			Db.NonQ(command);
			if(sched.SchedType==ScheduleType.Provider){
				DeletedObjects.SetDeleted(DeletedObjectType.ScheduleProv,sched.ScheduleNum);
			}
		}
	
		///<summary>Supply a list of all Schedule for one day. Then, this filters out for one type.</summary>
		public static Schedule[] GetForType(List<Schedule> ListDay,ScheduleType schedType,int provNum){
			ArrayList AL=new ArrayList();
			for(int i=0;i<ListDay.Count;i++){
				if(ListDay[i].SchedType==schedType && ListDay[i].ProvNum==provNum){
					AL.Add(ListDay[i]);
				}
			}
			Schedule[] retVal=new Schedule[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>This overload is for when the listForPeriod includes multiple days.</summary>
		public static List<Schedule> GetSchedsForOp(List<Schedule> listForPeriod,DayOfWeek dayOfWeek,Operatory op){
			List<Schedule> listForDay=new List<Schedule>();
			for(int i=0;i<listForPeriod.Count;i++){
				//if day of week doesn't match, then skip
				if(listForPeriod[i].SchedDate.DayOfWeek!=dayOfWeek){
					continue;
				}
				listForDay.Add(listForPeriod[i].Copy());
			}
			return GetSchedsForOp(listForDay,op);
		}

		///<summary>This overload is for when the listForPeriod includes only one day.</summary>
		public static List<Schedule> GetSchedsForOp(List<Schedule> listForPeriod,Operatory op){
			List<Schedule> retVal=new List<Schedule>();
			for(int i=0;i<listForPeriod.Count;i++){
				//if a schedule is not a provider type, then skip it
				if(listForPeriod[i].SchedType!=ScheduleType.Provider){
					continue;
				}
				//if the schedule has ops set, then only apply the schedule to those ops
				if(listForPeriod[i].Ops.Count>0){
					if(listForPeriod[i].Ops.Contains(op.OperatoryNum)){
						retVal.Add(listForPeriod[i].Copy());
					}
				}
				//but if the schedule does not have ops set, then look at the op settings to determine whether to use it.
				else{
					if(op.ProvDentist!=0 && !op.IsHygiene) {//op uses dentist
						if(listForPeriod[i].ProvNum==op.ProvDentist){
							retVal.Add(listForPeriod[i].Copy());
						}
					}
					else if(op.ProvHygienist!=0 && op.IsHygiene) {//op uses hygienist
						if(listForPeriod[i].ProvNum==op.ProvHygienist){
							retVal.Add(listForPeriod[i].Copy());
						}
					}
					else {//op has no provider set
						//so use the provider that's set for unassigned ops
						if(listForPeriod[i].ProvNum==PrefC.GetInt("ScheduleProvUnassigned")){
							retVal.Add(listForPeriod[i].Copy());
						}
					}
				}
			}
			return retVal;
		}

		public static int GetAssignedProvNumForSpot(List<Schedule> listForPeriod,Operatory op,bool isSecondary,DateTime aptDateTime){
			//first, look for a sched assigned specifically to that spot
			for(int i=0;i<listForPeriod.Count;i++){
				if(listForPeriod[i].SchedType!=ScheduleType.Provider){
					continue;
				}
				if(aptDateTime.Date!=listForPeriod[i].SchedDate){
					continue;
				}
				if(!listForPeriod[i].Ops.Contains(op.OperatoryNum)){
					continue;
				}
				if(isSecondary && !Providers.GetIsSec(listForPeriod[i].ProvNum)){
					continue;
				}
				if(!isSecondary && Providers.GetIsSec(listForPeriod[i].ProvNum)){
					continue;
				}
				//for the time, if the sched starts later than the apt starts
				if(listForPeriod[i].StartTime.TimeOfDay > aptDateTime.TimeOfDay){
					continue;
				}
				//or if the sched ends (before or at same time) as the apt starts
				if(listForPeriod[i].StopTime.TimeOfDay <= aptDateTime.TimeOfDay){
					continue;
				}
				//matching sched found
				return listForPeriod[i].ProvNum;
			}
			//if no matching sched found, then use the operatory
			if(isSecondary){
				return op.ProvHygienist;
			}
			else{
				return op.ProvDentist;
			}
			return 0;//none
		}

		///<summary>Clears all blockouts for day.  But then defaults would show.  So adds a "closed" blockout.</summary>
		public static void ClearBlockoutsForDay(DateTime date){
			//SetAllDefault(date,ScheduleType.Blockout,0);
			string command="DELETE from schedule WHERE "
				+"SchedDate="    +POut.PDate(date)+" "
				+"AND SchedType='"+POut.PInt((int)ScheduleType.Blockout)+"' ";
				//+"AND ProvNum='"  +POut.PInt(provNum)+"'";
			Db.NonQ(command);
			//CheckIfDeletedLastBlockout(date);
		}

		public static bool DateIsHoliday(DateTime date){
			string command="SELECT COUNT(*) FROM schedule WHERE Status=2 "//holiday
				+"AND SchedType=0 "//practice
				+"AND SchedDate= "+POut.PDate(date);
			string result=Db.GetCount(command);
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
			DataTable raw=Db.GetTable(command);
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
			//make deleted entries for synch purposes:
			string command="SELECT ScheduleNum FROM schedule WHERE SchedDate= "+POut.PDate(schedDate)+" "
				+"AND SchedType="+POut.PInt((int)ScheduleType.Provider);
			DataTable table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				DeletedObjects.SetDeleted(DeletedObjectType.ScheduleProv,PIn.PInt(table.Rows[i][0].ToString()));
			}
			//Then, bulk delete.
			command="DELETE FROM schedule WHERE SchedDate= "+POut.PDate(schedDate)+" "
				+"AND (SchedType=0 OR SchedType=1 OR SchedType=3)";
			Db.NonQ(command);
			for(int i=0;i<SchedList.Count;i++){
				Insert(SchedList[i]);
			}
		}

		///<summary>Clears all schedule entries for the given date range and the given providers, employees, and practice.</summary>
		public static void Clear(DateTime dateStart,DateTime dateEnd,int[] provNums,int[] empNums,bool includePractice){
			if(provNums.Length==0 && empNums.Length==0 && !includePractice) {
				return;
			}
			string command;
			string orClause="";
			//make deleted entries for synch purposes:
			if(provNums.Length>0){
				for(int i=0;i<provNums.Length;i++) {
					if(orClause!="") {
						orClause+="OR ";
					}
					orClause+="schedule.ProvNum="+POut.PInt(provNums[i])+" ";
				}
				command="SELECT ScheduleNum FROM schedule "
					+"WHERE SchedDate >= "+POut.PDate(dateStart)+" "
					+"AND SchedDate <= "+POut.PDate(dateEnd)+" "
					+"AND SchedType="+POut.PInt((int)ScheduleType.Provider)
					+" AND ("+orClause+")";
				DataTable table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++){
					DeletedObjects.SetDeleted(DeletedObjectType.ScheduleProv,PIn.PInt(table.Rows[i][0].ToString()));
				}
			}
			//Then, the usual deletion for everything
			command="DELETE FROM schedule "
				+"WHERE SchedDate >= "+POut.PDate(dateStart)+" "
				+"AND SchedDate <= "+POut.PDate(dateEnd)+" "
				+"AND (";
			orClause="";//this is guaranteed to be non empty by the time the command is assembled.
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
			Db.NonQ(command);
		}

		///<summary>Clears all Blockout schedule entries for the given date range and the given ops.</summary>
		public static void ClearBlockouts(DateTime dateStart,DateTime dateEnd,List<int> opNums) {
			string command="SELECT * FROM schedule WHERE "
				+"SchedDate >= "+POut.PDate(dateStart)+" "
				+"AND SchedDate <= "+POut.PDate(dateEnd)+" "
				+"AND SchedType=2";//blockouts
			List<Schedule> listSched=RefreshAndFill(command);
			//First, remove all the given ScheduleOps.
			for(int i=0;i<listSched.Count;i++){
				for(int o=0;o<opNums.Count;o++){
					if(listSched[i].Ops.Contains(opNums[o])){
						command="DELETE FROM scheduleop "
							+"WHERE ScheduleNum="+POut.PInt(listSched[i].ScheduleNum)+" "
							+"AND OperatoryNum="+POut.PInt(opNums[o]);
						Db.NonQ(command);
						listSched[i].Ops.Remove(opNums[o]);
					}
				}
			}
			//Then, delete any blockouts that no longer have any opnums.
			for(int i=0;i<listSched.Count;i++){
				if(listSched[i].Ops.Count>0){
					continue;
				}
				command="DELETE FROM schedule WHERE ScheduleNum="+POut.PInt(listSched[i].ScheduleNum);
				Db.NonQ(command);
			}			
		}

		
	}

	

	

}













