using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Schedules {		
		///<summary>Gets a list of Schedule items for one date.</summary>
		public static List<Schedule> GetDayList(DateTime date){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Schedule>>(MethodBase.GetCurrentMethod(),date);
			}
			string command="SELECT * FROM schedule "
				+"WHERE SchedDate = "+POut.Date(date)+" "
				+"ORDER BY StartTime";
			return RefreshAndFill(command);
		}

		///<summary>Used in the Schedules edit window to get a filtered list of schedule items in preparation for paste or repeat.</summary>
		public static List<Schedule> RefreshPeriod(DateTime dateStart,DateTime dateEnd,List<long> provNums,List<long> empNums,bool includePractice)
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Schedule>>(MethodBase.GetCurrentMethod(),dateStart,dateEnd,provNums,empNums,includePractice);
			}
			if(provNums.Count==0 && empNums.Count==0 && !includePractice) {
				return new List<Schedule>();
			}
			string command="SELECT * FROM schedule "
				+"WHERE SchedDate >= "+POut.Date(dateStart)+" "
				+"AND SchedDate <= "+POut.Date(dateEnd)+" "
				+"AND (";
			string orClause="";//this is guaranteed to be non empty by the time the command is assembled.
			if(includePractice) {
				orClause="SchedType=0 ";
			}
			for(int i=0;i<provNums.Count;i++) {
				if(orClause!="") {
					orClause+="OR ";
				}
				orClause+="schedule.ProvNum="+POut.Long(provNums[i])+" ";
			}
			for(int i=0;i<empNums.Count;i++) {
				if(orClause!="") {
					orClause+="OR ";
				}
				orClause+="schedule.EmployeeNum="+POut.Long(empNums[i])+" ";
			}
			command+=orClause+")";
			return RefreshAndFill(command);
		}

		///<summary></summary>
		public static List<Schedule> RefreshPeriodBlockouts(DateTime dateStart,DateTime dateEnd,List<long> opNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Schedule>>(MethodBase.GetCurrentMethod(),dateStart,dateEnd,opNums);
			}
			if(opNums.Count==0){
				return new List<Schedule>();
			}
			string command="SELECT schedule.* "
				+"FROM schedule,scheduleop "
				+"WHERE schedule.ScheduleNum=scheduleop.ScheduleNum "
				+"AND SchedDate >= "+POut.Date(dateStart)+" "
				+"AND SchedDate <= "+POut.Date(dateEnd)+" "
				+"AND SchedType=2 "//blockouts
				+"AND (";//OperatoryNum=0 ";
			for(int i=0;i<opNums.Count;i++) {
				if(i>0){
					command+=" OR ";
				}
				command+="OperatoryNum="+POut.Long(opNums[i]);
			}
			command+=") GROUP BY schedule.ScheduleNum";
			return RefreshAndFill(command);
		}

		///<summary></summary>
		public static List<Schedule> RefreshDayEdit(DateTime dateSched){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Schedule>>(MethodBase.GetCurrentMethod(),dateSched);
			}
			string command="SELECT schedule.* "
				+"FROM schedule "//,provider "
				+"WHERE SchedDate = "+POut.Date(dateSched)+" "
				+"AND (SchedType=0 OR SchedType=1 OR SchedType=3)";//Practice or Provider or Employee
			return RefreshAndFill(command);
		}

		///<summary>Used in the check database integrity tool.</summary>
		public static Schedule[] RefreshAll() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Schedule[]>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM schedule";
			return RefreshAndFill(command).ToArray();
		}

		public static List<Schedule> GetChangedSince(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Schedule>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT * schedule WHERE DateTStamp > "+POut.DateT(changedSince)
				+" AND SchedType="+POut.Long((int)ScheduleType.Provider);
			return RefreshAndFill(command);
		}

		///<summary>This is only allowed because it's PRIVATE.</summary>
		private static List<Schedule> RefreshAndFill(string command) {
			//Not a typical refreshandfill, as it contains a query.
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
			//No need to check RemotingRole; no call to db.
			List<Schedule> retVal=Crud.ScheduleCrud.TableToList(table);
			string opstr;
			string[] oparray;
			if(table.Columns.Contains("ops")){
				for(int i=0;i<retVal.Count;i++){
					retVal[i].Ops=new List<long>();
					opstr=PIn.String(table.Rows[i]["ops"].ToString());
					if(opstr!=""){
						oparray=opstr.Split(',');
						for(int o=0;o<oparray.Length;o++){
							retVal[i].Ops.Add(PIn.Long(oparray[o]));
						}
					}
				}
			}
			return retVal;
		}

		///<summary></summary>
		public static void Update(Schedule sched){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sched);
				return;
			}
			Validate(sched);
			Crud.ScheduleCrud.Update(sched);
			string command="DELETE FROM scheduleop WHERE ScheduleNum="+POut.Long (sched.ScheduleNum);
			Db.NonQ(command);
			ScheduleOp op;
			for(int i=0;i<sched.Ops.Count;i++){
				op=new ScheduleOp();
				op.ScheduleNum=sched.ScheduleNum;
				op.OperatoryNum=sched.Ops[i];
				ScheduleOps.Insert(op);
			}
		}

		///<summary></summary>
		public static long Insert(Schedule sched){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				sched.ScheduleNum=Meth.GetLong(MethodBase.GetCurrentMethod(),sched);
				return sched.ScheduleNum;
			}
			Validate(sched);
			Crud.ScheduleCrud.Insert(sched);
			ScheduleOp op;
			for(int i=0;i<sched.Ops.Count;i++){
				op=new ScheduleOp();
				op.ScheduleNum=sched.ScheduleNum;
				op.OperatoryNum=sched.Ops[i];
				ScheduleOps.Insert(op);
			}
			return sched.ScheduleNum;
		}

		///<summary></summary>
		private static void Validate(Schedule sched){
			if(sched.StartTime > sched.StopTime){
				throw new Exception(Lans.g("Schedule","Stop time must be later than start time."));
			}
			if(sched.StartTime+TimeSpan.FromMinutes(5) > sched.StopTime
				&& sched.Status==SchedStatus.Open)
			{
				throw new Exception(Lans.g("Schedule","Stop time cannot be the same as the start time."));
			}
		}

		///<summary></summary>
		private static bool Overlaps(Schedule sched){
			//No need to check RemotingRole; no call to db.
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
					&& sched.StartTime >= ListForType[i].StartTime
					&& sched.StartTime < ListForType[i].StopTime)
				{
					return true;
				}
				if(sched.ScheduleNum!=ListForType[i].ScheduleNum
					&& sched.StopTime > ListForType[i].StartTime
					&& sched.StopTime <= ListForType[i].StopTime)
				{
					return true;
				}
				if(sched.ScheduleNum!=ListForType[i].ScheduleNum
					&& sched.StartTime <= ListForType[i].StartTime
					&& sched.StopTime >= ListForType[i].StopTime)
				{
					return true;
				}
			}
			return false;
		}

		///<summary></summary>
		public static void Delete(Schedule sched){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sched);
				return;
			}
			string command= "DELETE from schedule WHERE schedulenum = '"+POut.Long(sched.ScheduleNum)+"'";
 			Db.NonQ(command);
			if(sched.SchedType==ScheduleType.Provider){
				DeletedObjects.SetDeleted(DeletedObjectType.ScheduleProv,sched.ScheduleNum);
			}
		}
	
		///<summary>Supply a list of all Schedule for one day. Then, this filters out for one type.</summary>
		public static Schedule[] GetForType(List<Schedule> ListDay,ScheduleType schedType,long provNum){
			//No need to check RemotingRole; no call to db.
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
			//No need to check RemotingRole; no call to db.
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
			//No need to check RemotingRole; no call to db.
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
						if(listForPeriod[i].ProvNum==PrefC.GetLong(PrefName.ScheduleProvUnassigned)){
							retVal.Add(listForPeriod[i].Copy());
						}
					}
				}
			}
			return retVal;
		}

		public static long GetAssignedProvNumForSpot(List<Schedule> listForPeriod,Operatory op,bool isSecondary,DateTime aptDateTime) {
			//No need to check RemotingRole; no call to db.
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
				if(listForPeriod[i].StartTime > aptDateTime.TimeOfDay){
					continue;
				}
				//or if the sched ends (before or at same time) as the apt starts
				if(listForPeriod[i].StopTime <= aptDateTime.TimeOfDay){
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
			//return 0;//none
		}

		///<summary>Clears all blockouts for day.</summary>
		public static void ClearBlockoutsForDay(DateTime date){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),date);
				return;
			}
			string command="DELETE from schedule WHERE "
				+"SchedDate="    +POut.Date(date)+" "
				+"AND SchedType='"+POut.Long((int)ScheduleType.Blockout)+"' ";
			Db.NonQ(command);
		}

		public static bool DateIsHoliday(DateTime date){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),date);
			}
			string command="SELECT COUNT(*) FROM schedule WHERE Status=2 "//holiday
				+"AND SchedType=0 "//practice
				+"AND SchedDate= "+POut.Date(date);
			string result=Db.GetCount(command);
			if(result=="0"){
				return false;
			}
			return true;
		}

		///<summary>Returns a 7 column data table in a calendar layout so all you have to do is draw it on the screen.  If includePractice is true, then practice notes and holidays will be included.</summary>
		public static DataTable GetPeriod(DateTime dateStart,DateTime dateEnd,List<long> provNums,List<long> empNums,bool includePractice) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),dateStart,dateEnd,provNums,empNums,includePractice);
			}
			DataTable table=new DataTable();
			DataRow row;
			table.Columns.Add("sun");
			table.Columns.Add("mon");
			table.Columns.Add("tues");
			table.Columns.Add("wed");
			table.Columns.Add("thurs");
			table.Columns.Add("fri");
			table.Columns.Add("sat");
			if(provNums.Count==0 && empNums.Count==0 && !includePractice){
				return table;
			}
			string command="SELECT Abbr,employee.FName,Note,SchedDate,SchedType,Status,StartTime,StopTime "
				+"FROM schedule "
				+"LEFT JOIN provider ON schedule.ProvNum=provider.ProvNum "
				+"LEFT JOIN employee ON schedule.EmployeeNum=employee.EmployeeNum "
				+"WHERE SchedDate >= "+POut.Date(dateStart)+" "
				+"AND SchedDate <= "+POut.Date(dateEnd)+" "
				+"AND (";
			string orClause="";//this is guaranteed to be non empty by the time the command is assembled.
			if(includePractice){
				orClause="SchedType=0 ";
			}
			for(int i=0;i<provNums.Count;i++){
				if(orClause!=""){
					orClause+="OR ";
				}
				orClause+="schedule.ProvNum="+POut.Long(provNums[i])+" ";
			}
			for(int i=0;i<empNums.Count;i++) {
				if(orClause!="") {
					orClause+="OR ";
				}
				orClause+="schedule.EmployeeNum="+POut.Long(empNums[i])+" ";
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
				dateSched=PIn.Date(raw.Rows[i]["SchedDate"].ToString());
				startTime=PIn.DateT(raw.Rows[i]["StartTime"].ToString());
				stopTime=PIn.DateT(raw.Rows[i]["StopTime"].ToString());
				rowI=GetRowCal(dateStart,dateSched);
				if(i!=0//not first row
					&& raw.Rows[i-1]["Abbr"].ToString()==raw.Rows[i]["Abbr"].ToString()//same provider as previous row
					&& raw.Rows[i-1]["FName"].ToString()==raw.Rows[i]["FName"].ToString()//same employee as previous row
					&& raw.Rows[i-1]["SchedDate"].ToString()==raw.Rows[i]["SchedDate"].ToString())//and same date as previous row
				{
					table.Rows[rowI][(int)dateSched.DayOfWeek]+=", ";
					if(startTime.TimeOfDay==PIn.DateT("12 AM").TimeOfDay
						&& stopTime.TimeOfDay==PIn.DateT("12 AM").TimeOfDay)
					{
						if(raw.Rows[i]["Status"].ToString()=="2") {//if holiday
							table.Rows[rowI][(int)dateSched.DayOfWeek]+=Lans.g("Schedules","Holiday:");
						}
					}
					else{
						table.Rows[rowI][(int)dateSched.DayOfWeek]+=startTime.ToString("h:mm")+"-"+stopTime.ToString("h:mm");
					}
				}
				else{
					table.Rows[rowI][(int)dateSched.DayOfWeek]+="\r\n";
					if(startTime.TimeOfDay==PIn.DateT("12 AM").TimeOfDay
						&& stopTime.TimeOfDay==PIn.DateT("12 AM").TimeOfDay)
					{
						if(raw.Rows[i]["Status"].ToString()=="2"){//if holiday
							table.Rows[rowI][(int)dateSched.DayOfWeek]+=Lans.g("Schedules","Holiday:");//+raw.Rows[i]["Note"].ToString();
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
			//No need to check RemotingRole; no call to db.
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
			//No need to check RemotingRole; no call to db.
			DateTime dateFirstRow;//the first date of row 0. Typically a few days before startDate. Always a Sun.
			dateFirstRow=startDate.AddDays(-(int)startDate.DayOfWeek);//example: (Tues,May 9).AddDays(-2)=Sun,May 7.
			int days=row*7+col;
			//peculiar bug.  When days=211 (startDate=4/1/10, row=30, col=1
			//and dateFirstRow=3/28/2010 and the current computer date is 4/14/10, and OS is Win7(possibly others),
			//dateFirstRow.AddDays(days)=10/24/10 00:59:58 (off by two seconds)
			//Spent hours trying to duplicate in isolated environment, but it behaves fine outside of this program.
			//Ticks are same, but result is different.
			//Commenting out the CultureInfo changes in FormOpenDental_Load did not help.
			//Not worth further debugging, so:
			DateTime retVal=dateFirstRow.AddDays(days).AddSeconds(5);
			return retVal.Date;
		}

		///<summary>Surround with try/catch.  Deletes all existing practice, provider, and employee schedules for a day and then saves the provided list.</summary>
		public static void SetForDay(List<Schedule> SchedList,DateTime schedDate){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),SchedList,schedDate);
				return;
			}
			for(int i=0;i<SchedList.Count;i++){
				if(SchedList[i].StartTime > SchedList[i].StopTime) {
					throw new Exception(Lans.g("Schedule","Stop time must be later than start time."));
				}
			}
			//make deleted entries for synch purposes:
			string command="SELECT ScheduleNum FROM schedule WHERE SchedDate= "+POut.Date(schedDate)+" "
				+"AND SchedType="+POut.Long((int)ScheduleType.Provider);
			DataTable table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				DeletedObjects.SetDeleted(DeletedObjectType.ScheduleProv,PIn.Long(table.Rows[i][0].ToString()));
			}
			//Then, bulk delete.
			command="DELETE FROM schedule WHERE SchedDate= "+POut.Date(schedDate)+" "
				+"AND (SchedType=0 OR SchedType=1 OR SchedType=3)";
			Db.NonQ(command);
			for(int i=0;i<SchedList.Count;i++){
				Insert(SchedList[i]);
			}
		}

		///<summary>Clears all schedule entries for the given date range and the given providers, employees, and practice.</summary>
		public static void Clear(DateTime dateStart,DateTime dateEnd,List<long> provNums,List<long> empNums,bool includePractice) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),dateStart,dateEnd,provNums,empNums,includePractice);
				return;
			}
			if(provNums.Count==0 && empNums.Count==0 && !includePractice) {
				return;
			}
			string command;
			string orClause="";
			//make deleted entries for synch purposes:
			if(provNums.Count>0){
				for(int i=0;i<provNums.Count;i++) {
					if(orClause!="") {
						orClause+="OR ";
					}
					orClause+="schedule.ProvNum="+POut.Long(provNums[i])+" ";
				}
				command="SELECT ScheduleNum FROM schedule "
					+"WHERE SchedDate >= "+POut.Date(dateStart)+" "
					+"AND SchedDate <= "+POut.Date(dateEnd)+" "
					+"AND SchedType="+POut.Long((int)ScheduleType.Provider)
					+" AND ("+orClause+")";
				DataTable table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++){
					DeletedObjects.SetDeleted(DeletedObjectType.ScheduleProv,PIn.Long(table.Rows[i][0].ToString()));
				}
			}
			//Then, the usual deletion for everything
			command="DELETE FROM schedule "
				+"WHERE SchedDate >= "+POut.Date(dateStart)+" "
				+"AND SchedDate <= "+POut.Date(dateEnd)+" "
				+"AND (";
			orClause="";//this is guaranteed to be non empty by the time the command is assembled.
			if(includePractice) {
				orClause="SchedType=0 ";
			}
			for(int i=0;i<provNums.Count;i++) {
				if(orClause!="") {
					orClause+="OR ";
				}
				orClause+="schedule.ProvNum="+POut.Long(provNums[i])+" ";
			}
			for(int i=0;i<empNums.Count;i++) {
				if(orClause!="") {
					orClause+="OR ";
				}
				orClause+="schedule.EmployeeNum="+POut.Long(empNums[i])+" ";
			}
			command+=orClause+")";
			Db.NonQ(command);
		}

		///<summary>Clears all Blockout schedule entries for the given date range and the given ops.</summary>
		public static void ClearBlockouts(DateTime dateStart,DateTime dateEnd,List<long> opNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),dateStart,dateEnd,opNums);
				return;
			}
			string command="SELECT * FROM schedule WHERE "
				+"SchedDate >= "+POut.Date(dateStart)+" "
				+"AND SchedDate <= "+POut.Date(dateEnd)+" "
				+"AND SchedType=2";//blockouts
			List<Schedule> listSched=RefreshAndFill(command);
			//First, remove all the given ScheduleOps.
			for(int i=0;i<listSched.Count;i++){
				for(int o=0;o<opNums.Count;o++){
					if(listSched[i].Ops.Contains(opNums[o])){
						command="DELETE FROM scheduleop "
							+"WHERE ScheduleNum="+POut.Long(listSched[i].ScheduleNum)+" "
							+"AND OperatoryNum="+POut.Long(opNums[o]);
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
				command="DELETE FROM schedule WHERE ScheduleNum="+POut.Long(listSched[i].ScheduleNum);
				Db.NonQ(command);
			}			
		}

		public static int GetDuplicateBlockoutCount() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod());
			}
			string command=@"SELECT COUNT(*) countDups,SchedDate,schedule.ScheduleNum,
				(SELECT GROUP_CONCAT(so1.OperatoryNum ORDER BY so1.OperatoryNum) FROM scheduleop so1 WHERE so1.ScheduleNum=schedule.ScheduleNum) AS ops				
				FROM schedule
				WHERE SchedType=2
				GROUP BY SchedDate,ops,StartTime,StopTime
				HAVING countDups > 1";
			DataTable table=Db.GetTable(command);
			int retVal=0;
			for(int i=0;i<table.Rows.Count;i++) {
				retVal+=PIn.Int(table.Rows[i][0].ToString())-1;
			}
			return retVal;
		}

		///<summary></summary>
		public static void ClearDuplicates() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			//First, create a temp table with operatories for all blockouts
			string command="DROP TABLE IF EXISTS tempBlockoutOps";
			Db.NonQ(command);
			command=@"CREATE TABLE tempBlockoutOps
				SELECT ScheduleNum,
				(SELECT GROUP_CONCAT(so1.OperatoryNum ORDER BY so1.OperatoryNum) FROM scheduleop so1 WHERE so1.ScheduleNum=schedule.ScheduleNum) AS ops
				FROM schedule
				WHERE SchedType=2
				GROUP BY ScheduleNum";
			Db.NonQ(command);
			command="ALTER TABLE tempBlockoutOps ADD INDEX (ScheduleNum)";
			Db.NonQ(command);
			//Get a list of scheduleNums that have duplicates
			//A duplicate is defined as a matching opsList and matching times
			//The result list will contain one random scheduleNum out of the many duplicates
			command=@"SELECT SchedDate,ScheduleNum,StartTime,StopTime,
				(SELECT ops FROM tempBlockoutOps WHERE tempBlockoutOps.ScheduleNum=schedule.ScheduleNum) ops_______________ops,
				COUNT(*) countDups
				FROM schedule
				WHERE SchedType=2
				GROUP BY SchedDate,ops_______________ops,StartTime,StopTime
				HAVING countDups > 1";
			DataTable table=Db.GetTable(command);
			DateTime schedDate;
			DateTime startTime;
			DateTime stopTime;
			string ops;
			long scheduleNum;
			for(int i=0;i<table.Rows.Count;i++){
				schedDate=PIn.Date(table.Rows[i][0].ToString());
				scheduleNum=PIn.Long(table.Rows[i][1].ToString());
				startTime=PIn.DateT(table.Rows[i][2].ToString());
				stopTime=PIn.DateT(table.Rows[i][3].ToString());
				ops=PIn.ByteArray(table.Rows[i][4]);
				command="DELETE FROM schedule WHERE "
					+"SchedDate = "+POut.Date(schedDate)+" "
					+"AND ScheduleNum != "+POut.Long(scheduleNum)+" "
					+"AND StartTime = '"+startTime.ToString("hh:mm",new DateTimeFormatInfo())+":00' "
					+"AND StopTime = '"+stopTime.ToString("hh:mm",new DateTimeFormatInfo())+":00' "
					+"AND (SELECT ops FROM tempBlockoutOps WHERE tempBlockoutOps.ScheduleNum=schedule.ScheduleNum) = '"+POut.String(ops)+"' ";
				Db.NonQ(command);
			}
			command="DROP TABLE IF EXISTS tempBlockoutOps";
			Db.NonQ(command);
			//clear all the orphaned scheduleops
			command="DELETE FROM scheduleop WHERE NOT EXISTS(SELECT * FROM schedule WHERE scheduleop.ScheduleNum=schedule.ScheduleNum)";
			long result=Db.NonQ(command);//we can test the result in debug
		}


	}

	

	

}













