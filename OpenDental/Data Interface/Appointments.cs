using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>Handles database commands related to the appointment table in the db.</summary>
	public class Appointments {
		///<summary>The appointment on the pinboard.</summary>
		public static Appointment PinBoard;
		///<summary>The date currently selected in the appointment module.</summary>
		public static DateTime DateSelected;

		///<summary>Gets a list of appointments for one day in the schedule, whether hidden or not.</summary>
		public static Appointment[] Refresh(DateTime thisDay) {
			DateSelected = thisDay;
			string command=
				"SELECT * from appointment "
				+"WHERE AptDateTime LIKE '"+POut.PDate(thisDay,false)+"%' "
				+"AND aptstatus != '"+(int)ApptStatus.UnschedList+"' "
				+"AND aptstatus != '"+(int)ApptStatus.Planned+"'";
			return FillList(command);
		}

		///<summary>Gets ListUn for both the unscheduled list and for planned appt tracker.  This is in transition, since the unscheduled list will probably eventually be phased out.  Set true if getting Planned appointments, false if getting Unscheduled appointments.</summary>
		public static Appointment[] RefreshUnsched(bool doGetPlanned) {
			string command="";
			if(doGetPlanned) {
				command="SELECT Tplanned.*,Tregular.aptnum FROM appointment Tplanned "
					+"LEFT JOIN appointment Tregular ON Tplanned.aptnum = Tregular.nextaptnum "
					+"WHERE Tplanned.aptstatus = '"+(int)ApptStatus.Planned+"' "
					+"AND Tregular.aptnum IS NULL "
					+"ORDER BY Tplanned.UnschedStatus,Tplanned.AptDateTime";
			}
			else {//unsched
				command="SELECT * FROM appointment "
					+"WHERE aptstatus = '"+(int)ApptStatus.UnschedList+"' "
					+"ORDER BY AptDateTime";
			}
			return FillList(command);
		}

		///<summary>Returns all appointments for the given patient, ordered from earliest to latest.  Used in statements, appt cards, OtherAppts window, etc.</summary>
		public static Appointment[] GetForPat(int patNum) {
			string command=
				"SELECT * FROM appointment "
				+"WHERE patnum = '"+patNum.ToString()+"' "
				+"ORDER BY AptDateTime";
			return FillList(command);
		}

		///<summary>Gets one appointment from db.  Returns null if not found.</summary>
		public static Appointment GetOneApt(int aptNum) {
			if(aptNum==0) {
				return null;
			}
			string command="SELECT * FROM appointment "
				+"WHERE AptNum = '"+POut.PInt(aptNum)+"'";
			Appointment[] list=FillList(command);
			if(list.Length==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of appointments for one day in the schedule for a given set of providers.</summary>
		public static Appointment[] GetRouting(DateTime date,int[] provNums) {
			string command=
				"SELECT * FROM appointment "
				+"WHERE AptDateTime LIKE '"+POut.PDate(date,false)+"%' "
				+"AND aptstatus != '"+(int)ApptStatus.UnschedList+"' "
				+"AND aptstatus != '"+(int)ApptStatus.Planned+"' "
				+"AND (";
			for(int i=0;i<provNums.Length;i++) {
				if(i>0) {
					command+=" OR";
				}
				command+=" ProvNum="+POut.PInt(provNums[i])
					+" OR ProvHyg="+POut.PInt(provNums[i]);
			}
			command+=") ORDER BY AptDateTime";
			return FillList(command);
		}

		///<summary>Fills the specified array of Appointments using the supplied SQL command.</summary>
		private static Appointment[] FillList(string command) {
			DataTable table=General.GetTable(command);
			Appointment[] list=new Appointment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				list[i]=new Appointment();
				list[i].AptNum         =PIn.PInt   (table.Rows[i][0].ToString());
				list[i].PatNum         =PIn.PInt   (table.Rows[i][1].ToString());
				list[i].AptStatus      =(ApptStatus)PIn.PInt(table.Rows[i][2].ToString());
				list[i].Pattern        =PIn.PString(table.Rows[i][3].ToString());
				list[i].Confirmed      =PIn.PInt   (table.Rows[i][4].ToString());
				list[i].AddTime        =PIn.PInt   (table.Rows[i][5].ToString());
				list[i].Op             =PIn.PInt   (table.Rows[i][6].ToString());
				list[i].Note           =PIn.PString(table.Rows[i][7].ToString());
				list[i].ProvNum        =PIn.PInt   (table.Rows[i][8].ToString());
				list[i].ProvHyg        =PIn.PInt   (table.Rows[i][9].ToString());
				list[i].AptDateTime    =PIn.PDateT (table.Rows[i][10].ToString());
				list[i].NextAptNum     =PIn.PInt   (table.Rows[i][11].ToString());
				list[i].UnschedStatus  =PIn.PInt   (table.Rows[i][12].ToString());
				list[i].Lab            =(LabCase)PIn.PInt(table.Rows[i][13].ToString());
				list[i].IsNewPatient   =PIn.PBool  (table.Rows[i][14].ToString());
				list[i].ProcDescript   =PIn.PString(table.Rows[i][15].ToString());
				list[i].Assistant      =PIn.PInt   (table.Rows[i][16].ToString());
				list[i].InstructorNum  =PIn.PInt   (table.Rows[i][17].ToString());
				list[i].SchoolClassNum =PIn.PInt   (table.Rows[i][18].ToString());
				list[i].SchoolCourseNum=PIn.PInt   (table.Rows[i][19].ToString());
				list[i].GradePoint     =PIn.PFloat (table.Rows[i][20].ToString());
				list[i].ClinicNum      =PIn.PInt   (table.Rows[i][21].ToString());
				list[i].IsHygiene      =PIn.PBool  (table.Rows[i][22].ToString());
			}
			return list;
		}

		///<summary>If IsNew, just supply null for oldApt.</summary>
		public static void InsertOrUpdate(Appointment appt, Appointment oldApt,bool IsNew){
			//if(){
				//throw new Exception(Lan.g(this,""));
			//}
			if(IsNew){
				Insert(appt);
			}
			else{
				if(oldApt==null){
					throw new ApplicationException("oldApt cannot be null if updating.");
				}
				Update(appt,oldApt);
			}
		}

		///<summary></summary>
		private static void Insert(Appointment appt){
			if(PrefB.RandomKeys){
				appt.AptNum=MiscData.GetKey("appointment","AptNum");
			}
			string command="INSERT INTO appointment (";
			if(PrefB.RandomKeys){
				command+="AptNum,";
			}
			command+="patnum,aptstatus, "
				+"pattern,confirmed,addtime,op,note,provnum,"
				+"provhyg,aptdatetime,nextaptnum,unschedstatus,lab,isnewpatient,procdescript,"
				+"Assistant,InstructorNum,SchoolClassNum,SchoolCourseNum,GradePoint,ClinicNum,IsHygiene) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(appt.AptNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (appt.PatNum)+"', "
				+"'"+POut.PInt   ((int)appt.AptStatus)+"', "
				+"'"+POut.PString(appt.Pattern)+"', "
				+"'"+POut.PInt   (appt.Confirmed)+"', "
				+"'"+POut.PInt   (appt.AddTime)+"', "
				+"'"+POut.PInt   (appt.Op)+"', "
				+"'"+POut.PString(appt.Note)+"', "
				+"'"+POut.PInt   (appt.ProvNum)+"', "
				+"'"+POut.PInt   (appt.ProvHyg)+"', "
				+POut.PDateT (appt.AptDateTime)+", "
				+"'"+POut.PInt   (appt.NextAptNum)+"', "
				+"'"+POut.PInt   (appt.UnschedStatus)+"', "
				+"'"+POut.PInt   ((int)appt.Lab)+"', "
				+"'"+POut.PBool  (appt.IsNewPatient)+"', "
				+"'"+POut.PString(appt.ProcDescript)+"', "
				+"'"+POut.PInt   (appt.Assistant)+"', "
				+"'"+POut.PInt   (appt.InstructorNum)+"', "
				+"'"+POut.PInt   (appt.SchoolClassNum)+"', "
				+"'"+POut.PInt   (appt.SchoolCourseNum)+"', "
				+"'"+POut.PFloat (appt.GradePoint)+"', "
				+"'"+POut.PInt   (appt.ClinicNum)+"', "
				+"'"+POut.PBool  (appt.IsHygiene)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
				appt.AptNum=General.NonQ(command,true);
			}
		}

		///<summary>Updates only the changed columns and returns the number of rows affected.  Supply an oldApt for comparison.</summary>
		private static int Update(Appointment appt, Appointment oldApt){
			bool comma=false;
			string c = "UPDATE appointment SET ";
			if(appt.PatNum!=oldApt.PatNum){
				c+="PatNum = '"      +POut.PInt   (appt.PatNum)+"'";
				comma=true;
			}
			if(appt.AptStatus!=oldApt.AptStatus){
				if(comma) c+=",";
				c+="AptStatus = '"   +POut.PInt   ((int)appt.AptStatus)+"'";
				comma=true;
			}
			if(appt.Pattern!=oldApt.Pattern){
				if(comma) c+=",";
				c+="Pattern = '"     +POut.PString(appt.Pattern)+"'";
				comma=true;
			}
			if(appt.Confirmed!=oldApt.Confirmed){
				if(comma) c+=",";
				c+="Confirmed = '"   +POut.PInt   (appt.Confirmed)+"'";
				comma=true;
			}
			if(appt.AddTime!=oldApt.AddTime){
				if(comma) c+=",";
				c+="AddTime = '"     +POut.PInt   (appt.AddTime)+"'";
				comma=true;
			}
			if(appt.Op!=oldApt.Op){
				if(comma) c+=",";
				c+="Op = '"          +POut.PInt   (appt.Op)+"'";
				comma=true;
			}
			if(appt.Note!=oldApt.Note){
				if(comma) c+=",";
				c+="Note = '"        +POut.PString(appt.Note)+"'";
				comma=true;
			}
			if(appt.ProvNum!=oldApt.ProvNum){
				if(comma) c+=",";
				c+="ProvNum = '"     +POut.PInt   (appt.ProvNum)+"'";
				comma=true;
			}
			if(appt.ProvHyg!=oldApt.ProvHyg){
				if(comma) c+=",";
				c+="ProvHyg = '"     +POut.PInt   (appt.ProvHyg)+"'";
				comma=true;
			}
			if(appt.AptDateTime!=oldApt.AptDateTime){
				if(comma) c+=",";
				c+="AptDateTime = " +POut.PDateT (appt.AptDateTime)+"";
				comma=true;
			}
			if(appt.NextAptNum!=oldApt.NextAptNum){
				if(comma) c+=",";
				c+="NextAptNum = '"  +POut.PInt   (appt.NextAptNum)+"'";
				comma=true;
			}
			if(appt.UnschedStatus!=oldApt.UnschedStatus){
				if(comma) c+=",";
				c+="UnschedStatus = '" +POut.PInt(appt.UnschedStatus)+"'";
				comma=true;
			}
			if(appt.Lab!=oldApt.Lab){
				if(comma) c+=",";
				c+="Lab = '"         +POut.PInt   ((int)appt.Lab)+"'";
				comma=true;
			}
			if(appt.IsNewPatient!=oldApt.IsNewPatient){
				if(comma) c+=",";
				c+="IsNewPatient = '"+POut.PBool  (appt.IsNewPatient)+"'";
				comma=true;
			}
			if(appt.ProcDescript!=oldApt.ProcDescript){
				if(comma) c+=",";
				c+="ProcDescript = '"+POut.PString(appt.ProcDescript)+"'";
				comma=true;
			}
			if(appt.Assistant!=oldApt.Assistant){
				if(comma) c+=",";
				c+="Assistant = '"   +POut.PInt   (appt.Assistant)+"'";
				comma=true;
			}
			if(appt.InstructorNum!=oldApt.InstructorNum){
				if(comma) c+=",";
				c+="InstructorNum = '"   +POut.PInt   (appt.InstructorNum)+"'";
				comma=true;
			}
			if(appt.SchoolClassNum!=oldApt.SchoolClassNum){
				if(comma) c+=",";
				c+="SchoolClassNum = '"   +POut.PInt   (appt.SchoolClassNum)+"'";
				comma=true;
			}
			if(appt.SchoolCourseNum!=oldApt.SchoolCourseNum){
				if(comma) c+=",";
				c+="SchoolCourseNum = '"   +POut.PInt   (appt.SchoolCourseNum)+"'";
				comma=true;
			}
			if(appt.GradePoint!=oldApt.GradePoint){
				if(comma) c+=",";
				c+="GradePoint = '"   +POut.PFloat  (appt.GradePoint)+"'";
				comma=true;
			}
			if(appt.ClinicNum!=oldApt.ClinicNum){
				if(comma) c+=",";
				c+="ClinicNum = '"   +POut.PInt  (appt.ClinicNum)+"'";
				comma=true;
			}
			if(appt.IsHygiene!=oldApt.IsHygiene){
				if(comma) c+=",";
				c+="IsHygiene = '"   +POut.PBool (appt.IsHygiene)+"'";
				comma=true;
			}
			if(!comma)
				return 0;//this means no change is actually required.
			c+=" WHERE AptNum = '"+POut.PInt(appt.AptNum)+"'";
 			int rowsChanged=General.NonQ(c);
			//MessageBox.Show(c);
			return rowsChanged;
		}

		///<summary></summary>
		public static void Delete(Appointment appt){
			Patient pat=Patients.GetPat(appt.PatNum);
			if(appt.IsNewPatient){
				Procedures.SetDateFirstVisit(DateTime.MinValue,3,pat);
			}
			string command="DELETE from appointment WHERE "
				+"AptNum = '"+POut.PInt(appt.AptNum)+"'";
 			General.NonQ(command);
		}

		///<summary>Used in Chart module to test whether a procedure is attached to an appointment with today's date. The procedure might have a different date if still TP status.  ApptList should include all appointments for this patient. Does not make a call to db.</summary>
		public static bool ProcIsToday(Appointment[] apptList,Procedure proc){
			for(int i=0;i<apptList.Length;i++){
				if(apptList[i].AptDateTime.Date==DateTime.Today
					&& apptList[i].AptNum==proc.AptNum
					&& (apptList[i].AptStatus==ApptStatus.Scheduled
					|| apptList[i].AptStatus==ApptStatus.ASAP
					|| apptList[i].AptStatus==ApptStatus.Broken
					|| apptList[i].AptStatus==ApptStatus.Complete))
				{
					return true;
				}
			}
			return false;
		}

		///<summary>Used in FormConfirmList</summary>
		public static DataTable GetConfirmList(DateTime dateFrom,DateTime dateTo){
			DataTable table=new DataTable();
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("AddrNote");
			table.Columns.Add("AptNum");
			table.Columns.Add("age");
			table.Columns.Add("aptDateTime");
			table.Columns.Add("confirmed");
			table.Columns.Add("contactMethod");
			table.Columns.Add("Guarantor");
			table.Columns.Add("medNotes");
			table.Columns.Add("Note");
			table.Columns.Add("patientName");
			table.Columns.Add("PatNum");
			table.Columns.Add("ProcDescript");
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT patient.PatNum,"//0
				+"patient.LName,"//1-LName
				+"patient.FName,patient.Preferred,patient.LName, "//2-patientName
				+"Guarantor,AptDateTime,Birthdate,HmPhone,"//3-6
				+"WkPhone,WirelessPhone,ProcDescript,Confirmed,Note,"//7-11
				+"AddrNote,AptNum,MedUrgNote,PreferConfirmMethod,Email,Premed "//12-14
				+"FROM patient,appointment "
				+"WHERE patient.PatNum=appointment.PatNum "
				+"AND AptDateTime > "+POut.PDate(dateFrom)+" "
				+"AND AptDateTime < "+POut.PDate(dateTo.AddDays(1))+" "
				+"AND (AptStatus=1 "//scheduled
				+"OR AptStatus=4) "//ASAP
				+"ORDER BY AptDateTime";
			DataTable rawtable=General.GetTable(command);
			DateTime dateT;
			Patient pat;
			ContactMethod contmeth;
			for(int i=0;i<rawtable.Rows.Count;i++) {
				row=table.NewRow();
				row["AddrNote"]=rawtable.Rows[i]["AddrNote"].ToString();
				row["AptNum"]=rawtable.Rows[i]["AptNum"].ToString();
				row["age"]=Shared.DateToAge(PIn.PDate(rawtable.Rows[i]["Birthdate"].ToString())).ToString();//we don't care about m/y.
				dateT=PIn.PDateT(rawtable.Rows[i]["AptDateTime"].ToString());
				row["aptDateTime"]=dateT.ToShortDateString()+"\r\n"+dateT.ToShortTimeString();
				row["confirmed"]=DefB.GetName(DefCat.ApptConfirmed,PIn.PInt(rawtable.Rows[i]["Confirmed"].ToString()));
				contmeth=(ContactMethod)PIn.PInt(rawtable.Rows[i]["PreferConfirmMethod"].ToString());
				if(contmeth==ContactMethod.None || contmeth==ContactMethod.HmPhone) {
					row["contactMethod"]=Lan.g("FormConfirmList","Hm:")+rawtable.Rows[i]["HmPhone"].ToString();
				}
				if(contmeth==ContactMethod.WkPhone) {
					row["contactMethod"]=Lan.g("FormConfirmList","Wk:")+rawtable.Rows[i]["WkPhone"].ToString();
				}
				if(contmeth==ContactMethod.WirelessPh) {
					row["contactMethod"]=Lan.g("FormConfirmList","Cell:")+rawtable.Rows[i]["WirelessPhone"].ToString();
				}
				if(contmeth==ContactMethod.Email) {
					row["contactMethod"]=rawtable.Rows[i]["Email"].ToString();
				}
				if(contmeth==ContactMethod.DoNotCall || contmeth==ContactMethod.SeeNotes) {
					row["contactMethod"]=Lan.g("enumContactMethod",contmeth.ToString());
				}
				row["Guarantor"]=rawtable.Rows[i]["Guarantor"].ToString();
				row["medNotes"]="";
				if(rawtable.Rows[i]["Premed"].ToString()=="1"){
					row["medNotes"]=Lan.g("FormConfirmList","Premedicate");
				}
				if(rawtable.Rows[i]["MedUrgNote"].ToString()!=""){
					if(row["medNotes"].ToString()!="") {
						row["medNotes"]+="\r\n";
					}
					row["medNotes"]+=rawtable.Rows[i]["MedUrgNote"].ToString();
				}
				row["Note"]=rawtable.Rows[i]["Note"].ToString();
				pat=new Patient();
				pat.LName=rawtable.Rows[i]["LName"].ToString();
				pat.FName=rawtable.Rows[i]["FName"].ToString();
				pat.Preferred=rawtable.Rows[i]["Preferred"].ToString();
				row["patientName"]=pat.LName+"\r\n";
				if(pat.Preferred!=""){
					row["patientName"]+="'"+pat.Preferred+"'";
				}
				else{
					row["patientName"]+=pat.FName;
				}
					//pat.GetNameLF();
				row["PatNum"]=rawtable.Rows[i]["PatNum"].ToString();
				row["ProcDescript"]=rawtable.Rows[i]["ProcDescript"].ToString();
				rows.Add(row);
			}
			//Array.Sort(orderDate,RecallList);
			//return RecallList;
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		///<summary>Used in Confirm list to just get addresses.</summary>
		public static DataTable GetAddrTable(int[] aptNums){
			string command="SELECT patient.LName,patient.FName,patient.MiddleI,patient.Preferred,"
				+"patient.Address,patient.Address2,patient.City,patient.State,patient.Zip,appointment.AptDateTime "
				+"FROM patient,appointment "
				+"WHERE patient.PatNum=appointment.PatNum "
				+"AND (";
			for(int i=0;i<aptNums.Length;i++){
				if(i>0){
					command+=" OR ";
				}
				command+="appointment.AptNum="+aptNums[i].ToString();
			}
			command+=") ORDER BY patient.LName,patient.FName";
			return General.GetTable(command);
		}

		///<summary>Used by appt search function.  Returns the next available time for the appointment.  Starts searching on lastSlot, which can be tonight at midnight for the first search.  Then, each subsequent search will start at the time of the previous search plus the length of the appointment.  Provider array cannot be length 0.  Might return array of 0 if it goes more than 1 year into the future.</summary>
		public static DateTime[] GetSearchResults(Appointment apt,DateTime afterDate,int[] providers,int resultCount,
			TimeSpan beforeTime,TimeSpan afterTime)
		{
			DateTime dayEvaluating=afterDate.AddDays(1);
			Appointment[] aptList;//list of appointments for one day
			ArrayList ALresults=new ArrayList();//result Date/Times
			TimeSpan timeFound;
			int hourFound;
			int[][] provBar=new int[providers.Length][];//dim 1 is for each provider.  Dim 2is the 10min increment
			bool[][] provBarSched=new bool[providers.Length][];//keeps track of the schedule of each provider. True means open, false is closed.
			int aptProv;
			string pattern;
			int startIndex;
			int provIndex;//the index of a provider within providers
			Schedule[] schedDay;//all schedule items for a given day.
			bool provHandled;
			bool aptIsMatch=false;
			//int afterIndex=0;//GetProvBarIndex(afterTime);
			//int beforeIndex=0;//GetProvBarIndex(beforeTime);
			while(ALresults.Count<resultCount//stops when the specified number of results are retrieved
				&& dayEvaluating<afterDate.AddYears(1))
			{
				for(int i=0;i<providers.Length;i++){
					provBar[i]=new int[24*ContrApptSheet.RowsPerHr];//[144]; or 24*6
					provBarSched[i]=new bool[24*ContrApptSheet.RowsPerHr];
				}
				//get appointments for one day
				aptList=Refresh(dayEvaluating);
				//fill provBar
				for(int i=0;i<aptList.Length;i++){
					if(aptList[i].IsHygiene){
						aptProv=aptList[i].ProvHyg;
					}
					else{
						aptProv=aptList[i].ProvNum;
					}
					provIndex=-1;
					for(int p=0;p<providers.Length;p++){
						if(providers[p]==aptProv){
							provIndex=p;
							break;
						}
					}
					if(provIndex==-1){
						continue;
					}
					pattern=ContrApptSingle.GetPatternShowing(aptList[i].Pattern);
					startIndex=(int)(((double)aptList[i].AptDateTime.Hour*(double)60/(double)PrefB.GetInt("AppointmentTimeIncrement")
						+(double)aptList[i].AptDateTime.Minute/(double)PrefB.GetInt("AppointmentTimeIncrement"))
						*(double)ContrApptSheet.Lh*ContrApptSheet.RowsPerIncr)
						/ContrApptSheet.Lh;//rounds down
					for(int k=0;k<pattern.Length;k++){
						if(pattern.Substring(k,1)=="X"){
							provBar[provIndex][startIndex+k]++;
						}
					}
				}
				//for(int i=0;i<provBar[0].Length;i++){
				//	Debug.Write(provBar[0][i].ToString());
				//}
				//Debug.WriteLine("");
				//handle all schedules by setting element of provBarSched to true if provider schedule shows open.
				schedDay=Schedules.RefreshDay(dayEvaluating);
				for(int p=0;p<providers.Length;p++){
					provHandled=false;
					//schedule for prov
					for(int i=0;i<schedDay.Length;i++){
						if(schedDay[i].SchedType!=ScheduleType.Provider){
							continue;
						}
						if(providers[p]!=schedDay[i].ProvNum){
							continue;
						}
						if(schedDay[i].Status==SchedStatus.Closed || schedDay[i].Status==SchedStatus.Holiday){
							provHandled=true;//all elements remain false.
							break;
						}
						SetProvBarSched(ref provBarSched[p],schedDay[i].StartTime,schedDay[i].StopTime);
						provHandled=true;
					}
					if(provHandled){
						continue;
					}
					//schedDefault for prov
					for(int i=0;i<SchedDefaults.List.Length;i++){
						if(SchedDefaults.List[i].DayOfWeek!=(int)dayEvaluating.DayOfWeek){
							continue;
						}
						if(SchedDefaults.List[i].SchedType!=ScheduleType.Provider){
							continue;
						}
						if(providers[p]!=SchedDefaults.List[i].ProvNum){
							continue;
						}
						SetProvBarSched(ref provBarSched[p],SchedDefaults.List[i].StartTime,SchedDefaults.List[i].StopTime);
						provHandled=true;
					}
					if(provHandled){
						continue;
					}
					//schedule for practice
					for(int i=0;i<schedDay.Length;i++){
						if(schedDay[i].SchedType!=ScheduleType.Practice){
							continue;
						}
						if(schedDay[i].Status==SchedStatus.Closed || schedDay[i].Status==SchedStatus.Holiday){
							provHandled=true;//all elements remain false.
							break;
						}
						SetProvBarSched(ref provBarSched[p],schedDay[i].StartTime,schedDay[i].StopTime);
						provHandled=true;
					}
					if(provHandled){
						continue;
					}
					//SchedDefault for practice
					for(int i=0;i<SchedDefaults.List.Length;i++){
						if(SchedDefaults.List[i].DayOfWeek!=(int)dayEvaluating.DayOfWeek){
							continue;
						}
						if(SchedDefaults.List[i].SchedType!=ScheduleType.Practice){
							continue;
						}
						SetProvBarSched(ref provBarSched[p],SchedDefaults.List[i].StartTime,SchedDefaults.List[i].StopTime);
					}
				}
				//step through day, one increment at a time, looking for a slot
				pattern=ContrApptSingle.GetPatternShowing(apt.Pattern);
				timeFound=new TimeSpan(0);
				for(int i=0;i<provBar[0].Length;i++){//144 if using 10 minute increments
					for(int p=0;p<providers.Length;p++){
						//assume apt will be placed here
						aptIsMatch=true;
						//test all apt increments for prov closed. If any found, continue
						for(int a=0;a<pattern.Length;a++){
							if(provBarSched[p].Length<i+a+1 || !provBarSched[p][i+a]){
								aptIsMatch=false;
								break;
							}
						}
						if(!aptIsMatch){
							continue;
						}
						//test all apt increments with an X for not scheduled. If scheduled, continue.
						for(int a=0;a<pattern.Length;a++){
							if(pattern.Substring(a,1)=="X" && (provBar[p].Length<i+a+1 || provBar[p][i+a]>0)){
								aptIsMatch=false;
								break;
							}
						}
						if(!aptIsMatch){
							continue;
						}
						//convert to valid time
						hourFound=(int)((double)(i)/60*PrefB.GetInt("AppointmentTimeIncrement"));
						timeFound=new TimeSpan(
							hourFound,
							//minutes. eg. (13-(2*60/10))*10
							(int)((i-((double)hourFound*60/(double)PrefB.GetInt("AppointmentTimeIncrement")))
								*PrefB.GetInt("AppointmentTimeIncrement")),
							0);
						//make sure it's after the time restricted
						//Debug.WriteLine(timeFound.ToString()+"  "+afterTime.ToString());
							//apt.AptDateTime.TimeOfDay+"  "+afterTime.ToString());
						if(afterTime!=TimeSpan.Zero && timeFound<afterTime){
							aptIsMatch=false;
							continue;
						}
						if(beforeTime!=TimeSpan.Zero && timeFound>beforeTime) {
							aptIsMatch=false;
							continue;
						}
						//match found
						ALresults.Add(dayEvaluating+timeFound);
					}//for p	
					if(aptIsMatch){
						break;
					}
				}
				dayEvaluating=dayEvaluating.AddDays(1);//move to the next day
			}
			DateTime[] retVal=new DateTime[ALresults.Count];
			ALresults.CopyTo(retVal);
			return retVal;
		}

		///<summary>Only used in GetSearchResults.  All times between start and stop get set to true in provBarSched.</summary>
		private static void SetProvBarSched(ref bool[] provBarSched,DateTime timeStart,DateTime timeStop){
			int startI=GetProvBarIndex(timeStart);
			int stopI=GetProvBarIndex(timeStop);
			for(int i=startI;i<=stopI;i++){
				provBarSched[i]=true;
			}
		}

		private static int GetProvBarIndex(DateTime time){
			return (int)(((double)time.Hour*(double)60/(double)PrefB.GetInt("AppointmentTimeIncrement")//aptTimeIncr=minutesPerIncr
				+(double)time.Minute/(double)PrefB.GetInt("AppointmentTimeIncrement"))
				*(double)ContrApptSheet.Lh*ContrApptSheet.RowsPerIncr)
				/ContrApptSheet.Lh;//rounds down
		}
 
		///<summary>Used by UI when it needs a recall appointment placed on the pinboard ready to schedule.  This method creates the appointment and attaches all appropriate procedures.  It's up to the calling class to then place the appointment on the pinboard.  If the appointment doesn't get scheduled, it's important to delete it.</summary>
		public static Appointment CreateRecallApt(Patient patCur,Procedure[] procList,Recall recallCur,InsPlan[] planList){
			Appointment AptCur=new Appointment();
			AptCur.PatNum=patCur.PatNum;
			AptCur.AptStatus=ApptStatus.Scheduled;
			//convert time pattern to 5 minute increment
			StringBuilder savePattern=new StringBuilder();
			for(int i=0;i<PrefB.GetString("RecallPattern").Length;i++){
				savePattern.Append(PrefB.GetString("RecallPattern").Substring(i,1));
				savePattern.Append(PrefB.GetString("RecallPattern").Substring(i,1));
				if(PrefB.GetInt("AppointmentTimeIncrement")==15){
					savePattern.Append(PrefB.GetString("RecallPattern").Substring(i,1));
				}
			}
			AptCur.Pattern=savePattern.ToString();
			if(patCur.PriProv==0)
				AptCur.ProvNum=PrefB.GetInt("PracticeDefaultProv");
			else
				AptCur.ProvNum=patCur.PriProv;
			AptCur.ProvHyg=patCur.SecProv;
			if(AptCur.ProvHyg!=0){
				AptCur.IsHygiene=true;
			}
			AptCur.ClinicNum=patCur.ClinicNum;
			string[] procs=PrefB.GetString("RecallProcedures").Split(',');
			if(PrefB.GetString("RecallBW")!=""){//BWs
				bool dueBW=true;
				//DateTime dueDate=PIn.PDate(listFamily.Items[
				for(int i=0;i<procList.Length;i++){//loop through all procedures for this pt.
					//if any BW found within last year, then dueBW=false.
					if(PrefB.GetString("RecallBW")==procList[i].ADACode
						&& recallCur.DateDue.Year>1880
						&& procList[i].ProcDate > recallCur.DateDue.AddYears(-1)){
						dueBW=false;
					}
				}
				if(dueBW){
					string[] procs2=new string[procs.Length+1];
					procs.CopyTo(procs2,0);
					procs2[procs2.Length-1]=PrefB.GetString("RecallBW");
					procs=new string[procs2.Length];
					procs2.CopyTo(procs,0);
				}
			}
			AptCur.ProcDescript="";
			for(int i=0;i<procs.Length;i++) {
				if(i>0){
					AptCur.ProcDescript+=", ";
				}
				AptCur.ProcDescript+=ProcedureCodes.GetProcCode(procs[i]).AbbrDesc;
			}
			Appointments.InsertOrUpdate(AptCur,null,true);	
			Procedure ProcCur;
			PatPlan[] patPlanList=PatPlans.Refresh(patCur.PatNum);
			Benefit[] benefitList=Benefits.Refresh(patPlanList);
			//ClaimProc[] claimProcs=ClaimProcs.Refresh(Patients.Cur.PatNum);
			for(int i=0;i<procs.Length;i++){
				ProcCur=new Procedure();//this will be an insert
				//procnum
				ProcCur.PatNum=patCur.PatNum;
				ProcCur.AptNum=AptCur.AptNum;
				ProcCur.ADACode=procs[i];
				ProcCur.ProcDate=DateTime.Now;
				ProcCur.ProcFee=Fees.GetAmount0(ProcCur.ADACode,Fees.GetFeeSched(patCur,planList,patPlanList));
				//ProcCur.OverridePri=-1;
				//ProcCur.OverrideSec=-1;
				//surf
				//toothnum
				//Procedures.Cur.ToothRange="";
				//ProcCur.NoBillIns=ProcedureCodes.GetProcCode(ProcCur.ADACode).NoBillIns;
				//priority
				ProcCur.ProcStatus=ProcStat.TP;
				ProcCur.Note="";
				//Procedures.Cur.PriEstim=
				//Procedures.Cur.SecEstim=
				//claimnum
				ProcCur.ProvNum=patCur.PriProv;
				//Procedures.Cur.Dx=
				ProcCur.ClinicNum=patCur.ClinicNum;
				//nextaptnum
				ProcCur.MedicalCode=ProcedureCodes.GetProcCode(ProcCur.ADACode).MedicalCode;
				Procedures.Insert(ProcCur);//no recall synch required
				Procedures.ComputeEstimates(ProcCur,patCur.PatNum,new ClaimProc[0],false,planList,patPlanList,benefitList);
			}
			return AptCur;
		}

		///<summary>Tests to see if this appointment will create a double booking. Returns arrayList with no items in it if no double bookings for this appt.  But if double booking, then it returns an arrayList of adacodes which would be double booked.  You must supply the appointment being scheduled as well as a list of all appointments for that day.  The list can include the appointment being tested if user is moving it to a different time on the same day.  The ProcsForOne list of procedures needs to contain the procedures for the apt becauese procsMultApts won't necessarily, especially if it's a planned appt on the pinboard.</summary>
		public static ArrayList GetDoubleBookedCodes(Appointment apt,Appointment[] dayList,Procedure[] procsMultApts,Procedure[] procsForOne) {
			ArrayList retVal=new ArrayList();//ADAcodes
			//figure out which provider we are testing for
			int provNum;
			if(apt.IsHygiene){
				provNum=apt.ProvHyg;
			}
			else{
				provNum=apt.ProvNum;
			}
			//compute the starting row of this appt
			int convertToY=(int)(((double)apt.AptDateTime.Hour*(double)60
				/(double)PrefB.GetInt("AppointmentTimeIncrement")
				+(double)apt.AptDateTime.Minute
				/(double)PrefB.GetInt("AppointmentTimeIncrement")
				)*(double)ContrApptSheet.Lh*ContrApptSheet.RowsPerIncr);
			int startIndex=convertToY/ContrApptSheet.Lh;//rounds down
			string pattern=ContrApptSingle.GetPatternShowing(apt.Pattern);
			//keep track of which rows in the entire day would be occupied by provider time for this appt
			ArrayList aptProvTime=new ArrayList();
			for(int k=0;k<pattern.Length;k++){
				if(pattern.Substring(k,1)=="X"){
					aptProvTime.Add(startIndex+k);//even if it extends past midnight, we don't care
				}
			}
			//Now, loop through all the other appointments for the day, and see if any would overlap this one
			bool overlaps;
			Procedure[] procs;
			bool doubleBooked=false;//applies to all appts, not just one at a time.
			for(int i=0;i<dayList.Length;i++){
				if(dayList[i].AptNum==apt.AptNum){//ignore current apt in its old location
					continue;
				}
				//ignore other providers
				if(dayList[i].IsHygiene && dayList[i].ProvHyg!=provNum){
					continue;
				}
				if(!dayList[i].IsHygiene && dayList[i].ProvNum!=provNum){
					continue;
				}
				if(dayList[i].AptStatus==ApptStatus.Broken){//ignore broken appts
					continue;
				}
				//calculate starting row
				//this math is copied from another section of the program, so it's sloppy. Safer than trying to rewrite it:
				convertToY=(int)(((double)dayList[i].AptDateTime.Hour*(double)60
					/(double)PrefB.GetInt("AppointmentTimeIncrement")
					+(double)dayList[i].AptDateTime.Minute
					/(double)PrefB.GetInt("AppointmentTimeIncrement")
					)*(double)ContrApptSheet.Lh*ContrApptSheet.RowsPerIncr);
				startIndex=convertToY/ContrApptSheet.Lh;//rounds down
				pattern=ContrApptSingle.GetPatternShowing(dayList[i].Pattern);
				//now compare it to apt
				overlaps=false;
				for(int k=0;k<pattern.Length;k++){
					if(pattern.Substring(k,1)=="X"){
						if(aptProvTime.Contains(startIndex+k)){
							overlaps=true;
							doubleBooked=true;
						}
					}
				}
				if(overlaps){
					//we need to add all ADACodes for this appt to retVal
					procs=Procedures.GetProcsOneApt(dayList[i].AptNum,procsMultApts);
					for(int j=0;j<procs.Length;j++){
						retVal.Add(procs[j].ADACode);
					}
				}
			}
			//now, retVal contains all double booked procs except for this appt
			//need to all procs for this appt.
			if(doubleBooked){
				for(int j=0;j<procsForOne.Length;j++) {
					retVal.Add(procsForOne[j].ADACode);
				}
			}
			return retVal;
		}

		
		






	}
	
	


}









