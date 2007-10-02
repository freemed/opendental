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
		///<summary>The date currently selected in the appointment module.</summary>
		public static DateTime DateSelected;

		///<summary>Gets a list of appointments for a period of time in the schedule, whether hidden or not.</summary>
		public static Appointment[] GetForPeriod(DateTime startDate,DateTime endDate){
			//DateSelected = thisDay;
			string command=
				"SELECT * from appointment "
				+"WHERE AptDateTime BETWEEN '"+POut.PDate(startDate,false)+"' AND '"+POut.PDate(endDate.AddDays(1),false)+"'"
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
		public static Appointment GetScheduledPlannedApt(int aptNum) {
			if(aptNum==0) {
				return null;
			}
			string command="SELECT * FROM appointment "
				+"WHERE NextAptNum = '"+POut.PInt(aptNum)+"'";
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
			return AppointmentB.TableToObjects(table);
		}

		public static DataSet GetApptEdit(int aptNum){
			return General.GetDS("Appointment.GetApptEdit",aptNum.ToString());
		}

		public static DataTable GetApptEditProcs(int aptNum) {
			//this is quick and dirty. Get all the tables, but only use the one we are interested in.
			return General.GetDS("Appointment.GetApptEdit",aptNum.ToString()).Tables["Procedure"].Copy();
		}

		public static DataTable GetApptEditComm(int aptNum) {
			return General.GetDS("Appointment.GetApptEdit",aptNum.ToString()).Tables["Comm"].Copy();
		}

		public static DataTable GetApptEditMisc(int aptNum) {
			return General.GetDS("Appointment.GetApptEdit",aptNum.ToString()).Tables["Misc"].Copy();
		}

		///<summary>Contains all data needed to display appointments for a period.</summary>
		public static DataSet RefreshPeriod(DateTime dateStart,DateTime dateEnd){
			return General.GetDS("Appointment.RefreshPeriod",dateStart.ToShortDateString(),dateEnd.ToShortDateString());
		}

		///<summary>The resulting datatable will have just one row in it.</summary>
		public static DataTable RefreshOneApt(int aptNum,bool isPlanned){
			return General.GetDS("Appointment.RefreshOneApt",aptNum.ToString(),POut.PBool(isPlanned)).Tables["Appointments"].Copy();
		}

		///<summary>Called when closing FormApptEdit with an OK in order to reattach the procedures to the appointment.</summary>
		public static void UpdateAttached(int aptNum,int[] procNums,bool isPlanned){
			//detach all procs from this appt.
			string command;
			if(isPlanned){
				command="UPDATE procedurelog SET PlannedAptNum=0 WHERE PlannedAptNum="+POut.PInt(aptNum);
			}
			else{
				command="UPDATE procedurelog SET AptNum=0 WHERE AptNum="+POut.PInt(aptNum);
			}
			General.NonQ(command);
			//now, attach all
			for(int i=0;i<procNums.Length;i++){
				if(isPlanned) {
					command="UPDATE procedurelog SET PlannedAptNum="+POut.PInt(aptNum)+" WHERE ProcNum="+POut.PInt(procNums[i]);
				}
				else {
					command="UPDATE procedurelog SET AptNum="+POut.PInt(aptNum)+" WHERE ProcNum="+POut.PInt(procNums[i]);
				}
				General.NonQ(command);
			}
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
			//make sure all fields are properly filled:
			if(appt.Confirmed==0){
				appt.Confirmed=DefB.Short[(int)DefCat.ApptConfirmed][0].DefNum;
			}
			if(appt.ProvNum==0){
				appt.ProvNum=Providers.List[0].ProvNum;
			}
			//now, save to db----------------------------------------------------------------------------------------
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
				+"'"+POut.PInt   (appt.LabOld)+"', "
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

		//public static void SaveData(Appointment apt) {

		//}

		///<summary>Updates only the changed columns and returns the number of rows affected.  Supply an oldApt for comparison.</summary>
		public static int Update(Appointment appt, Appointment oldApt){
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
			//if(appt.Lab!=oldApt.Lab){
			//	if(comma) c+=",";
			//	c+="Lab = '"         +POut.PInt   ((int)appt.Lab)+"'";
			//	comma=true;
			//}
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
		public static void Delete(int aptNum){
			string command;
			command="SELECT PatNum,IsNewPatient,AptStatus FROM appointment WHERE AptNum="+POut.PInt(aptNum);
			DataTable table=General.GetTable(command);
			Patient pat=Patients.GetPat(PIn.PInt(table.Rows[0]["PatNum"].ToString()));
			if(table.Rows[0]["IsNewPatient"].ToString()=="1"){
				Procedures.SetDateFirstVisit(DateTime.MinValue,3,pat);
			}
			//procs
			if(table.Rows[0]["AptStatus"].ToString()=="6"){//planned
				command="UPDATE procedurelog SET PlannedAptNum =0 WHERE PlannedAptNum = "+POut.PInt(aptNum);
			}
			else{
				command="UPDATE procedurelog SET AptNum =0 WHERE AptNum = "+POut.PInt(aptNum);
			}
			General.NonQ(command);
			//labcases
			if(table.Rows[0]["AptStatus"].ToString()=="6") {//planned
				command="UPDATE labcase SET PlannedAptNum =0 WHERE PlannedAptNum = "+POut.PInt(aptNum);
			}
			else {
				command="UPDATE labcase SET AptNum =0 WHERE AptNum = "+POut.PInt(aptNum);
			}
			General.NonQ(command);
			command="DELETE FROM appointment WHERE AptNum = "+POut.PInt(aptNum);
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

		///<summary>Used by appt search function.  Returns the next available time for the appointment.  Starts searching on lastSlot, which can be tonight at midnight for the first search.  Then, each subsequent search will start at the time of the previous search plus the length of the appointment.  Provider array cannot be length 0.  Might return array of 0 if it goes more than 2 years into the future.</summary>
		public static DateTime[] GetSearchResults(int aptNum,DateTime afterDate,int[] providers,int resultCount,
			TimeSpan beforeTime,TimeSpan afterTime)
		{
			Appointment apt=GetOneApt(aptNum);
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
			bool aptIsMatch=false;
			while(ALresults.Count<resultCount//stops when the specified number of results are retrieved
				&& dayEvaluating<afterDate.AddYears(2))
			{
				for(int i=0;i<providers.Length;i++){
					provBar[i]=new int[24*ContrApptSheet.RowsPerHr];//[144]; or 24*6
					provBarSched[i]=new bool[24*ContrApptSheet.RowsPerHr];
				}
				//get appointments for one day
				aptList=GetForPeriod(dayEvaluating,dayEvaluating);
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
					startIndex=(int)(((double)aptList[i].AptDateTime.Hour*(double)60/ContrApptSheet.MinPerRow
						+(double)aptList[i].AptDateTime.Minute/ContrApptSheet.MinPerRow)
						*(double)ContrApptSheet.Lh)/ContrApptSheet.Lh;//rounds down
					for(int k=0;k<pattern.Length;k++){
						if(pattern.Substring(k,1)=="X"){
							provBar[provIndex][startIndex+k]++;
						}
					}
				}
				//handle all schedules by setting element of provBarSched to true if provider schedule shows open.
				schedDay=Schedules.RefreshPeriod(dayEvaluating,dayEvaluating);
				for(int p=0;p<providers.Length;p++){
					for(int i=0;i<schedDay.Length;i++){
						if(schedDay[i].SchedType!=ScheduleType.Provider){
							continue;
						}
						if(providers[p]!=schedDay[i].ProvNum){
							continue;
						}
						SetProvBarSched(ref provBarSched[p],schedDay[i].StartTime,schedDay[i].StopTime);
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
						hourFound=(int)((double)(i)/(float)60*ContrApptSheet.MinPerRow);//8am=48/60*10
						timeFound=new TimeSpan(
							hourFound,
							//minutes. eg. (13-(2*60/10))*10
							(int)((i-((double)hourFound*(float)60/ContrApptSheet.MinPerRow))*ContrApptSheet.MinPerRow),
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
			if(patCur.PriProv==0){
				AptCur.ProvNum=PrefB.GetInt("PracticeDefaultProv");
			}
			else{
				AptCur.ProvNum=patCur.PriProv;
			}
			AptCur.ProvHyg=patCur.SecProv;
			if(AptCur.ProvHyg!=0){
				AptCur.IsHygiene=true;
			}
			AptCur.ClinicNum=patCur.ClinicNum;
			bool perioPt=false;
			StringBuilder savePattern=new StringBuilder();
			string[] procs;
			if(patCur.Birthdate.AddYears(12) < recallCur.DateDue) {//pt is over 12 at recall date)
				if(!PrefB.GetBool("RecallDisablePerioAlt")) {
					//if pt is perio pt RecallPerioTriggerProcs
					string[] PerioProc;
					if(PrefB.GetString("RecallPerioTriggerProcs")==""){
						PerioProc=new string[0];
					}
					else{
						PerioProc=PrefB.GetString("RecallPerioTriggerProcs").Split(',');
					}
					for (int q=0;q<PerioProc.Length;q++) {//see if pt has had any perio procs in the past
						for (int i=0;i<procList.Length;i++) {
							if (PerioProc[q]==ProcedureCodes.GetStringProcCode(procList[i].CodeNum)
								&&procList[i].ProcStatus.ToString()=="C") {
								perioPt=true;
							}
						}
					}
				}
				if(perioPt) {
					if(PrefB.GetString("RecallProceduresPerio")==""){
						procs=new string[0];
					}
					else{
						procs=PrefB.GetString("RecallProceduresPerio").Split(',');
					}
					//convert time pattern to 5 minute increment
					for(int i=0;i<PrefB.GetString("RecallPatternPerio").Length;i++){
						savePattern.Append(PrefB.GetString("RecallPatternPerio").Substring(i,1));
						savePattern.Append(PrefB.GetString("RecallPatternPerio").Substring(i,1));
						if(PrefB.GetInt("AppointmentTimeIncrement")==15){
						savePattern.Append(PrefB.GetString("RecallPatternPerio").Substring(i,1));
						}
					}
				}
				else {//not perio pt
					if(PrefB.GetString("RecallProcedures")=="") {
						procs=new string[0];
					}
					else {
						procs=PrefB.GetString("RecallProcedures").Split(',');
					}
					//convert time pattern to 5 minute increment
					for(int i=0;i<PrefB.GetString("RecallPattern").Length;i++){
						savePattern.Append(PrefB.GetString("RecallPattern").Substring(i,1));
						savePattern.Append(PrefB.GetString("RecallPattern").Substring(i,1));
						if(PrefB.GetInt("AppointmentTimeIncrement")==15){
						savePattern.Append(PrefB.GetString("RecallPattern").Substring(i,1));
						}
					}
				}
			}
			else {//child under 12 years
				if(PrefB.GetString("RecallProceduresChild")=="") {
					procs=new string[0];
				}
				else {
					procs=PrefB.GetString("RecallProceduresChild").Split(',');
				}
				for (int i=0;i<PrefB.GetString("RecallPatternChild").Length;i++) {
					savePattern.Append(PrefB.GetString("RecallPatternChild").Substring(i, 1));
					savePattern.Append(PrefB.GetString("RecallPatternChild").Substring(i, 1));
					if (PrefB.GetInt("AppointmentTimeIncrement")==15) {
						savePattern.Append(PrefB.GetString("RecallPatternChild").Substring(i, 1));
					}
				}
			}
			if(savePattern.ToString()==""){
				if(PrefB.GetInt("AppointmentTimeIncrement")==15){
					savePattern.Append("///XXX///");
				}
				else{
					savePattern.Append("//XX//");
				}
			}
			AptCur.Pattern=savePattern.ToString();
			if(!PrefB.GetBool("RecallDisableAutoFilms")) {//Add Films
				bool dueBW=true;
				bool dueFMXPano=true;
				bool dueBW_w_FMXPano=false;
				bool skipFMXPano=false;
				//DateTime dueDate=PIn.PDate(listFamily.Items[
				for(int i=0;i<procList.Length;i++){//loop through all procedures for this pt.
					//if enabled, and any BW/Panos not found within last specifed time period, then 
					//dueFMXPano=true and dueBW=false because we don't want to take both.
					//also skip this is pt is less than 18 years old.
					//later, might add here check for FMX freq based on ins information
					if ((PrefB.GetInt("RecallFMXPanoYrInterval").ToString() != "") && (patCur.Birthdate.AddYears(18) < recallCur.DateDue)) {
						if (PrefB.GetString("RecallFMXPanoProc") == ProcedureCodes.GetStringProcCode(procList[i].CodeNum)
							&& (procList[i].ProcStatus.ToString() == "C" | procList[i].ProcStatus.ToString() == "EO")
							&& recallCur.DateDue.Year > 1880
							&& procList[i].ProcDate > recallCur.DateDue.AddYears(-(PrefB.GetInt("RecallFMXPanoYrInterval")))) 
						{
							dueFMXPano=false;
							if (procList[i].ProcDate > recallCur.DateDue.AddYears(-1)) {//if FMX was taken w/ year, then we don't need BW's either
								dueBW=false;
							}
							else {//FMXPano between specified interval and 1 year...BW should be due
								dueBW_w_FMXPano=true;
							}
						}
					}
					else { //entry is blank or pt is <12 years old, so don't even try to include FMX
						dueFMXPano=false;
						skipFMXPano=true;
					}
					//if any BW found within last year, then dueBW=false, dueBW_w_FMXPano=false.
					if(PrefB.GetString("RecallBW")==ProcedureCodes.GetStringProcCode(procList[i].CodeNum)
						&& (procList[i].ProcStatus.ToString() == "C" | procList[i].ProcStatus.ToString() == "EO")
						&& recallCur.DateDue.Year > 1880
						&& procList[i].ProcDate > recallCur.DateDue.AddYears(-1)){
							dueFMXPano=false;
							dueBW=false;
							dueBW_w_FMXPano=false;
					}
				}
				//if FMXPano has been taken instead of BW, then we don't need any new films
				if (dueFMXPano | (!dueBW_w_FMXPano && !skipFMXPano)){
					dueBW=false;
				}
				if(dueBW_w_FMXPano){
					dueBW=true;
					dueFMXPano=false;
				}
				if(dueBW){
					if(PrefB.GetString("RecallBW")!=""){
						string[] procs2=new string[procs.Length+1];
						procs.CopyTo(procs2,0);
						procs2[procs2.Length-1]=PrefB.GetString("RecallBW");
						procs=new string[procs2.Length];
						procs2.CopyTo(procs,0);
					}
				}
				if(dueFMXPano) {
					if(PrefB.GetString("RecallFMXPanoProc")!=""){
						string[] procs2=new string[procs.Length+1];
						procs.CopyTo(procs2,0);
						procs2[procs2.Length-1]=PrefB.GetString("RecallFMXPanoProc");
						procs=new string[procs2.Length];
						procs2.CopyTo(procs,0);
					}
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
				ProcCur.CodeNum=ProcedureCodes.GetCodeNum(procs[i]);
				ProcCur.ProcDate=DateTime.Now;
				ProcCur.ProcFee=Fees.GetAmount0(ProcCur.CodeNum,Fees.GetFeeSched(patCur,planList,patPlanList));
				//ProcCur.OverridePri=-1;
				//ProcCur.OverrideSec=-1;
				//surf
				//toothnum
				//Procedures.Cur.ToothRange="";
				//ProcCur.NoBillIns=ProcedureCodes.GetProcCode(ProcCur.CodeNum).NoBillIns;
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
				ProcCur.MedicalCode=ProcedureCodes.GetProcCode(ProcCur.CodeNum).MedicalCode;
				ProcCur.BaseUnits = ProcedureCodes.GetProcCode(ProcCur.CodeNum).BaseUnits;
				Procedures.Insert(ProcCur);//no recall synch required
				Procedures.ComputeEstimates(ProcCur,patCur.PatNum,new ClaimProc[0],false,planList,patPlanList,benefitList);
			}
			return AptCur;
		}

		///<summary>Tests to see if this appointment will create a double booking. Returns arrayList with no items in it if no double bookings for this appt.  But if double booking, then it returns an arrayList of codes which would be double booked.  You must supply the appointment being scheduled as well as a list of all appointments for that day.  The list can include the appointment being tested if user is moving it to a different time on the same day.  The ProcsForOne list of procedures needs to contain the procedures for the apt becauese procsMultApts won't necessarily, especially if it's a planned appt on the pinboard.</summary>
		public static ArrayList GetDoubleBookedCodes(Appointment apt,DataTable dayTable,Procedure[] procsMultApts,Procedure[] procsForOne) {
			ArrayList retVal=new ArrayList();//codes
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
			DateTime aptDateTime;
			for(int i=0;i<dayTable.Rows.Count;i++){
				if(dayTable.Rows[i]["AptNum"].ToString()==apt.AptNum.ToString()){//ignore current apt in its old location
					continue;
				}
				//ignore other providers
				if(dayTable.Rows[i]["IsHygiene"].ToString()=="1" && dayTable.Rows[i]["ProvHyg"].ToString()!=provNum.ToString()){
					continue;
				}
				if(dayTable.Rows[i]["IsHygiene"].ToString()=="0" && dayTable.Rows[i]["ProvNum"].ToString()!=provNum.ToString()){
					continue;
				}
				if(dayTable.Rows[i]["AptStatus"].ToString()==((int)ApptStatus.Broken).ToString()){//ignore broken appts
					continue;
				}
				aptDateTime=PIn.PDateT(dayTable.Rows[i]["AptDateTime"].ToString());
				if(ContrApptSheet.IsWeeklyView && aptDateTime.Date==apt.AptDateTime.Date){
					continue;
				}
				//calculate starting row
				//this math is copied from another section of the program, so it's sloppy. Safer than trying to rewrite it:
				convertToY=(int)(((double)aptDateTime.Hour*(double)60
					/(double)PrefB.GetInt("AppointmentTimeIncrement")
					+(double)aptDateTime.Minute
					/(double)PrefB.GetInt("AppointmentTimeIncrement")
					)*(double)ContrApptSheet.Lh*ContrApptSheet.RowsPerIncr);
				startIndex=convertToY/ContrApptSheet.Lh;//rounds down
				pattern=ContrApptSingle.GetPatternShowing(dayTable.Rows[i]["Pattern"].ToString());
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
					//we need to add all codes for this appt to retVal
					procs=Procedures.GetProcsOneApt(PIn.PInt(dayTable.Rows[i]["AptNum"].ToString()),procsMultApts);
					for(int j=0;j<procs.Length;j++){
						retVal.Add(ProcedureCodes.GetStringProcCode(procs[j].CodeNum));
					}
				}
			}
			//now, retVal contains all double booked procs except for this appt
			//need to all procs for this appt.
			if(doubleBooked){
				for(int j=0;j<procsForOne.Length;j++) {
					retVal.Add(ProcedureCodes.GetStringProcCode(procsForOne[j].CodeNum));
				}
			}
			return retVal;
		}

		///<summary>The newStatus will be a DefNum or 0.</summary>
		public static void SetConfirmed(int aptNum,int newStatus){
			string command="UPDATE appointment SET Confirmed="+POut.PInt(newStatus)
				+" WHERE AptNum="+POut.PInt(aptNum);
			General.NonQ(command);
		}

		///<summary>Sets the new pattern for an appointment.  This is how resizing is done.  Must contain only / and X, with each char representing 5 minutes.</summary>
		public static void SetPattern(int aptNum,string newPattern) {
			string command="UPDATE appointment SET Pattern='"+POut.PString(newPattern)+"' WHERE AptNum="+POut.PInt(aptNum);
			General.NonQ(command);
		}

		///<summary>Use to send to unscheduled list, or to set broken.</summary>
		public static void SetAptStatus(int aptNum,ApptStatus newStatus) {
			string command="UPDATE appointment SET AptStatus="+POut.PInt((int)newStatus)
				+" WHERE AptNum="+POut.PInt(aptNum);
			General.NonQ(command);
		}



	}
	
	


}









