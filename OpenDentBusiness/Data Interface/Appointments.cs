using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	public class Appointments {
		
		///<summary>Gets a list of appointments for a period of time in the schedule, whether hidden or not.</summary>
		public static Appointment[] GetForPeriod(DateTime startDate,DateTime endDate){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Appointment[]>(MethodBase.GetCurrentMethod(),startDate,endDate);
			}
			//DateSelected = thisDay;
			string command=
				"SELECT * from appointment "
				+"WHERE AptDateTime BETWEEN '"+POut.PDate(startDate,false)+"' AND '"+POut.PDate(endDate.AddDays(1),false)+"'"
				+"AND aptstatus != '"+(int)ApptStatus.UnschedList+"' "
				+"AND aptstatus != '"+(int)ApptStatus.Planned+"'";
			DataTable table=Db.GetTable(command);
			return TableToObjects(table).ToArray();
		}

		///<summary>Gets list of unscheduled appointments.  Allowed orderby: status, alph, date</summary>
		public static Appointment[] RefreshUnsched(string orderby,int provNum,int siteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Appointment[]>(MethodBase.GetCurrentMethod(),orderby,provNum,siteNum);
			}
			string command="SELECT * FROM appointment "
				+"LEFT JOIN patient ON patient.PatNum=appointment.PatNum "
				+"WHERE AptStatus = "+POut.PInt((int)ApptStatus.UnschedList)+" ";
			if(provNum>0) {
				command+="AND (appointment.ProvNum="+POut.PInt(provNum)+" OR appointment.ProvHyg="+POut.PInt(provNum)+") ";
			}
			if(siteNum>0) {
				command+="AND patient.SiteNum="+POut.PInt(siteNum)+" ";
			}
			command+="HAVING patient.PatStatus= "+POut.PInt((int)PatientStatus.Patient)+" ";			
			if(orderby=="status") {
				command+="ORDER BY UnschedStatus,AptDateTime";
			}
			else if(orderby=="alph") {
				command+="ORDER BY LName,FName";
			}
			else { //if(orderby=="date"){
				command+="ORDER BY AptDateTime";
			}
			DataTable table=Db.GetTable(command);
			return TableToObjects(table).ToArray();
		}

		///<summary>Gets list of asap appointments.</summary>
		public static List<Appointment> RefreshASAP(int provNum,int siteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Appointment>>(MethodBase.GetCurrentMethod(),provNum,siteNum);
			}
			string command="SELECT * FROM appointment ";
			//if(orderby=="alph" || siteNum>0) {
			//command+="LEFT JOIN patient ON patient.PatNum=appointment.PatNum ";
			//}
			command+="WHERE AptStatus = "+POut.PInt((int)ApptStatus.ASAP)+" ";
			if(provNum>0) {
				command+="AND (appointment.ProvNum="+POut.PInt(provNum)+" OR appointment.ProvHyg="+POut.PInt(provNum)+") ";
			}
			if(siteNum>0) {
				command+="AND patient.SiteNum="+POut.PInt(siteNum)+" ";
			}
			/*if(orderby=="status") {
				command+="ORDER BY UnschedStatus,AptDateTime";
			}
			else if(orderby=="alph") {
				command+="ORDER BY LName,FName";
			}
			else { //if(orderby=="date"){
				command+="ORDER BY AptDateTime";
			}*/
			command+="ORDER BY AptDateTime";
			DataTable table=Db.GetTable(command);
			return TableToObjects(table);
		}

		///<summary>Allowed orderby: status, alph, date</summary>
		public static List<Appointment> RefreshPlannedTracker(string orderby,int provNum,int siteNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Appointment>>(MethodBase.GetCurrentMethod(),orderby,provNum,siteNum);
			}
			string command="SELECT tplanned.*,tregular.aptnum "
				+"FROM appointment tplanned "
				+"LEFT JOIN appointment tregular ON tplanned.aptnum = tregular.nextaptnum "
				+"LEFT JOIN patient ON patient.PatNum=tplanned.PatNum "
				+"WHERE tplanned.aptstatus = "+POut.PInt((int)ApptStatus.Planned)+" "
				+"AND tregular.aptnum IS NULL "
				+"AND patient.PatStatus="+POut.PInt((int)PatientStatus.Patient)+" ";
			if(provNum>0) {
				command+="AND (tplanned.ProvNum="+POut.PInt(provNum)+" OR tplanned.ProvHyg="+POut.PInt(provNum)+") ";
			}
			if(siteNum>0) {
				command+="AND patient.SiteNum="+POut.PInt(siteNum)+" ";
			}
			if(orderby=="status"){
				command+="ORDER BY tplanned.UnschedStatus,tplanned.AptDateTime";
			}
			else if(orderby=="alph"){
				command+="ORDER BY LName,FName";
			}
			else{ //if(orderby=="date"){
				command+="ORDER BY tplanned.AptDateTime";
			}
			DataTable table=Db.GetTable(command);
			return TableToObjects(table);
		}

		///<summary>Returns all appointments for the given patient, ordered from earliest to latest.  Used in statements, appt cards, OtherAppts window, etc.</summary>
		public static Appointment[] GetForPat(int patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Appointment[]>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * FROM appointment "
				+"WHERE patnum = '"+patNum.ToString()+"' "
				+"ORDER BY AptDateTime";
			DataTable table=Db.GetTable(command);
			return TableToObjects(table).ToArray();
		}

		public static List<Appointment> GetListForPat(int patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Appointment>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * FROM appointment "
				+"WHERE patnum = '"+patNum.ToString()+"' "
				+"ORDER BY AptDateTime";
			DataTable table=Db.GetTable(command);
			return TableToObjects(table);
		}

		///<summary>Gets one appointment from db.  Returns null if not found.</summary>
		public static Appointment GetOneApt(int aptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Appointment>(MethodBase.GetCurrentMethod(),aptNum);
			}
			if(aptNum==0) {
				return null;
			}
			string command="SELECT * FROM appointment "
				+"WHERE AptNum = '"+POut.PInt(aptNum)+"'";
			DataTable table=Db.GetTable(command);
			List<Appointment> list=TableToObjects(table);
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary></summary>
		public static Appointment GetScheduledPlannedApt(int nextAptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Appointment>(MethodBase.GetCurrentMethod(),nextAptNum);
			}
			if(nextAptNum==0) {
				return null;
			}
			string command="SELECT * FROM appointment "
				+"WHERE NextAptNum = '"+POut.PInt(nextAptNum)+"'";
			DataTable table=Db.GetTable(command);
			List<Appointment> list=TableToObjects(table);
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of all future appointments which are either sched or ASAP.  Ordered by dateTime</summary>
		public static List<Appointment> GetFutureSchedApts(int patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Appointment>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM appointment "
				+"WHERE PatNum = "+POut.PInt(patNum)+" "
				+"AND AptDateTime > NOW() "
				+"AND (aptstatus = "+(int)ApptStatus.Scheduled+" "
				+"OR aptstatus = "+(int)ApptStatus.ASAP+") "
				+"ORDER BY AptDateTime";
			DataTable table=Db.GetTable(command);
			return TableToObjects(table);
		}

		///<summary>Gets a list of appointments for one day in the schedule for a given set of providers.</summary>
		public static Appointment[] GetRouting(DateTime date,List <int> provNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Appointment[]>(MethodBase.GetCurrentMethod(),date,provNums);
			}
			string command=
				"SELECT * FROM appointment "
				+"WHERE AptDateTime LIKE '"+POut.PDate(date,false)+"%' "
				+"AND aptstatus != '"+(int)ApptStatus.UnschedList+"' "
				+"AND aptstatus != '"+(int)ApptStatus.Planned+"' "
				+"AND (";
			for(int i=0;i<provNums.Count;i++) {
				if(i>0) {
					command+=" OR";
				}
				command+=" ProvNum="+POut.PInt(provNums[i])
					+" OR ProvHyg="+POut.PInt(provNums[i]);
			}
			command+=") ORDER BY AptDateTime";
			DataTable table=Db.GetTable(command);
			return TableToObjects(table).ToArray();
		}

		public static List<Appointment> GetUAppoint(DateTime changedSince,DateTime excludeOlderThan){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Appointment>>(MethodBase.GetCurrentMethod(),changedSince,excludeOlderThan);
			}
			string command="SELECT * FROM appointment WHERE DateTStamp > "+POut.PDateT(changedSince)
				+" AND AptDateTime > "+POut.PDateT(excludeOlderThan);
			DataTable table=Db.GetTable(command);
			return TableToObjects(table);
		}

		///<summary>A list of strings.  Each string corresponds to one appointment in the supplied list.  Each string is a comma delimited list of codenums of the procedures attached to the appointment.</summary>
		public static List<string> GetUAppointProcs(List<Appointment> appts){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod(),appts);
			}
			List<string> retVal=new List<string>();
			if(appts.Count==0){
				return retVal;
			}
			string command="SELECT AptNum,CodeNum FROM procedurelog WHERE AptNum IN(";
			for(int i=0;i<appts.Count;i++){
				if(i>0){
					command+=",";
				}
				command+=POut.PInt(appts[i].AptNum);
			}
			command+=")";
			DataTable table=Db.GetTable(command);
			string str;
			for(int i=0;i<appts.Count;i++){
				str="";
				for(int p=0;p<table.Rows.Count;p++){
					if(table.Rows[p]["AptNum"].ToString()==appts[i].AptNum.ToString()){
						if(str!=""){
							str+=",";
						}
						str+=table.Rows[p]["CodeNum"].ToString();
					}
				}
				retVal.Add(str);
			}
			return retVal;
		}

		/*
		///<summary>Returns a list of Appointments using the supplied SQL command.</summary>
		private static List<Appointment> FillList(string command) {
			DataTable table=Db.GetTable(command);
			return TableToObjects(table);
		}*/

		/*//<summary>Called when closing FormApptEdit with an OK in order to reattach the procedures to the appointment.</summary>
		public static void UpdateAttached(int aptNum,int[] procNums,bool isPlanned){
			//detach all procs from this appt.
			string command;
			if(isPlanned){
				command="UPDATE procedurelog SET PlannedAptNum=0 WHERE PlannedAptNum="+POut.PInt(aptNum);
			}
			else{
				command="UPDATE procedurelog SET AptNum=0 WHERE AptNum="+POut.PInt(aptNum);
			}
			Db.NonQ(command);
			//now, attach all
			for(int i=0;i<procNums.Length;i++){
				if(isPlanned) {
					command="UPDATE procedurelog SET PlannedAptNum="+POut.PInt(aptNum)+" WHERE ProcNum="+POut.PInt(procNums[i]);
				}
				else {
					command="UPDATE procedurelog SET AptNum="+POut.PInt(aptNum)+" WHERE ProcNum="+POut.PInt(procNums[i]);
				}
				Db.NonQ(command);
			}
		}*/

		/*

		///<summary></summary>
		public static void InsertOrUpdate(Appointment appt,Appointment oldApt,bool IsNew) {
			//if(){
			//throw new Exception(Lans.g(this,""));
			//}
			if(IsNew) {
				Insert(appt);
			}
			else {
				if(oldApt==null) {
					throw new ApplicationException("oldApt cannot be null if updating.");
				}
				Update(appt,oldApt);
			}
		}*/

		public static void Insert(Appointment appt) {
			Insert(appt,false);
		}

		///<summary>Set includeAptNum to true only in rare situations.  Like when we are inserting for eCW.</summary>
		public static int Insert(Appointment appt,bool includeAptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				appt.AptNum=Meth.GetInt(MethodBase.GetCurrentMethod(),appt,includeAptNum);
				return appt.AptNum;
			}
			//make sure all fields are properly filled:
			if(appt.Confirmed==0){
				appt.Confirmed=DefC.GetList(DefCat.ApptConfirmed)[0].DefNum;
			}
			if(appt.ProvNum==0){
				appt.ProvNum=ProviderC.List[0].ProvNum;
			}
			//now, save to db----------------------------------------------------------------------------------------
			if(!includeAptNum && PrefC.RandomKeys) {
				appt.AptNum=MiscData.GetKey("appointment","AptNum");
			}
			string command="INSERT INTO appointment (";
			if(includeAptNum || PrefC.RandomKeys) {
				command+="AptNum,";
			}
			command+="patnum,aptstatus, "
				+"pattern,confirmed,TimeLocked,op,note,provnum,"
				+"provhyg,aptdatetime,nextaptnum,unschedstatus,lab,isnewpatient,procdescript,"
				+"Assistant,InstructorNum,SchoolClassNum,SchoolCourseNum,GradePoint,ClinicNum,IsHygiene,"//DateTStamp
				+"DateTimeArrived,DateTimeSeated,DateTimeDismissed,InsPlan1,InsPlan2) VALUES(";
			if(includeAptNum || PrefC.RandomKeys) {
				command+="'"+POut.PInt(appt.AptNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (appt.PatNum)+"', "
				+"'"+POut.PInt   ((int)appt.AptStatus)+"', "
				+"'"+POut.PString(appt.Pattern)+"', "
				+"'"+POut.PInt   (appt.Confirmed)+"', "
				+"'"+POut.PBool  (appt.TimeLocked)+"', "
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
				+"'"+POut.PBool  (appt.IsHygiene)+"', "
				    +POut.PDateT (appt.DateTimeArrived)+", "
				    +POut.PDateT (appt.DateTimeSeated)+", "
				    +POut.PDateT (appt.DateTimeDismissed)+", "
				+"'"+POut.PInt		(appt.InsPlan1)+"', "
				+"'"+POut.PInt		(appt.InsPlan2)+"')";
				//DateTStamp
			if(includeAptNum || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else{
				appt.AptNum=Db.NonQ(command,true);
			}
			return appt.AptNum;
		}

		//public static void SaveData(Appointment apt) {

		//}

		///<summary>Updates only the changed columns and returns the number of rows affected.  Supply an oldApt for comparison.</summary>
		public static int Update(Appointment appt, Appointment oldApt){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),appt,oldApt);
			}
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
			if(appt.TimeLocked!=oldApt.TimeLocked){
				if(comma) c+=",";
				c+="TimeLocked = '"     +POut.PBool (appt.TimeLocked)+"'";
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
			//DateTStamp
			if(appt.DateTimeArrived!=oldApt.DateTimeArrived){
				if(comma) c+=",";
				c+="DateTimeArrived = "   +POut.PDateT(appt.DateTimeArrived);
				comma=true;
			}
			if(appt.DateTimeSeated!=oldApt.DateTimeSeated){
				if(comma) c+=",";
				c+="DateTimeSeated = "   +POut.PDateT(appt.DateTimeSeated);
				comma=true;
			}
			if(appt.DateTimeDismissed!=oldApt.DateTimeDismissed){
				if(comma) c+=",";
				c+="DateTimeDismissed = "   +POut.PDateT(appt.DateTimeDismissed);
				comma=true;
			}
			if(appt.InsPlan1!=oldApt.InsPlan1){
				if(comma) c+=",";
				c+="InsPlan1 = "+POut.PInt(appt.InsPlan1);
				comma=true;
			}
			if(appt.InsPlan2!=oldApt.InsPlan1) {
				if(comma) c+=",";
				c+="InsPlan2 = "+POut.PInt(appt.InsPlan2);
				comma=true;
			}
			if(!comma)
				return 0;//this means no change is actually required.
			c+=" WHERE AptNum = '"+POut.PInt(appt.AptNum)+"'";
 			int rowsChanged=Db.NonQ(c);
			//MessageBox.Show(c);
			return rowsChanged;
		}

		///<summary>Used in Chart module to test whether a procedure is attached to an appointment with today's date. The procedure might have a different date if still TP status.  ApptList should include all appointments for this patient. Does not make a call to db.</summary>
		public static bool ProcIsToday(Appointment[] apptList,Procedure proc){
			//No need to check RemotingRole; no call to db.
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
		public static DataTable GetConfirmList(DateTime dateFrom,DateTime dateTo,int provNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),dateFrom,dateTo,provNum);
			}
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
				+"OR AptStatus=4) ";//ASAP
			if(provNum>0){
				command+="AND (appointment.ProvNum="+POut.PInt(provNum)+" OR appointment.ProvHyg="+POut.PInt(provNum)+") ";
			}
			command+="ORDER BY AptDateTime";
			DataTable rawtable=Db.GetTable(command);
			DateTime dateT;
			Patient pat;
			ContactMethod contmeth;
			for(int i=0;i<rawtable.Rows.Count;i++) {
				row=table.NewRow();
				row["AddrNote"]=rawtable.Rows[i]["AddrNote"].ToString();
				row["AptNum"]=rawtable.Rows[i]["AptNum"].ToString();
				row["age"]=Patients.DateToAge(PIn.PDate(rawtable.Rows[i]["Birthdate"].ToString())).ToString();//we don't care about m/y.
				dateT=PIn.PDateT(rawtable.Rows[i]["AptDateTime"].ToString());
				row["aptDateTime"]=dateT.ToShortDateString()+"\r\n"+dateT.ToShortTimeString();
				row["confirmed"]=DefC.GetName(DefCat.ApptConfirmed,PIn.PInt(rawtable.Rows[i]["Confirmed"].ToString()));
				contmeth=(ContactMethod)PIn.PInt(rawtable.Rows[i]["PreferConfirmMethod"].ToString());
				if(contmeth==ContactMethod.None || contmeth==ContactMethod.HmPhone) {
					row["contactMethod"]=Lans.g("FormConfirmList","Hm:")+rawtable.Rows[i]["HmPhone"].ToString();
				}
				if(contmeth==ContactMethod.WkPhone) {
					row["contactMethod"]=Lans.g("FormConfirmList","Wk:")+rawtable.Rows[i]["WkPhone"].ToString();
				}
				if(contmeth==ContactMethod.WirelessPh) {
					row["contactMethod"]=Lans.g("FormConfirmList","Cell:")+rawtable.Rows[i]["WirelessPhone"].ToString();
				}
				if(contmeth==ContactMethod.Email) {
					row["contactMethod"]=rawtable.Rows[i]["Email"].ToString();
				}
				if(contmeth==ContactMethod.DoNotCall || contmeth==ContactMethod.SeeNotes) {
					row["contactMethod"]=Lans.g("enumContactMethod",contmeth.ToString());
				}
				row["Guarantor"]=rawtable.Rows[i]["Guarantor"].ToString();
				row["medNotes"]="";
				if(rawtable.Rows[i]["Premed"].ToString()=="1"){
					row["medNotes"]=Lans.g("FormConfirmList","Premedicate");
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
		public static DataTable GetAddrTable(List <int> aptNums){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),aptNums);
			}
			string command="SELECT patient.LName,patient.FName,patient.MiddleI,patient.Preferred,"
				+"patient.Address,patient.Address2,patient.City,patient.State,patient.Zip,appointment.AptDateTime "
				+"FROM patient,appointment "
				+"WHERE patient.PatNum=appointment.PatNum "
				+"AND (";
			for(int i=0;i<aptNums.Count;i++){
				if(i>0){
					command+=" OR ";
				}
				command+="appointment.AptNum="+aptNums[i].ToString();
			}
			command+=") ORDER BY patient.LName,patient.FName";
			return Db.GetTable(command);
		}

		///<summary>The newStatus will be a DefNum or 0.  Only called from one place.</summary>
		public static void SetConfirmed(int aptNum,int newStatus){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aptNum,newStatus);
				return;
			}
			string command="UPDATE appointment SET Confirmed="+POut.PInt(newStatus);
			if(PrefC.GetInt("AppointmentTimeArrivedTrigger")==newStatus){
				command+=",DateTimeArrived=NOW()";
			}
			else if(PrefC.GetInt("AppointmentTimeSeatedTrigger")==newStatus){
				command+=",DateTimeSeated=NOW()";
			}
			else if(PrefC.GetInt("AppointmentTimeDismissedTrigger")==newStatus){
				command+=",DateTimeDismissed=NOW()";
			}
			command+=" WHERE AptNum="+POut.PInt(aptNum);
			Db.NonQ(command);
		}

		///<summary>Sets the new pattern for an appointment.  This is how resizing is done.  Must contain only / and X, with each char representing 5 minutes.</summary>
		public static void SetPattern(int aptNum,string newPattern) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aptNum,newPattern);
				return;
			}
			string command="UPDATE appointment SET Pattern='"+POut.PString(newPattern)+"' WHERE AptNum="+POut.PInt(aptNum);
			Db.NonQ(command);
		}

		///<summary>Use to send to unscheduled list, to set broken, etc.  Do not use to set complete.</summary>
		public static void SetAptStatus(int aptNum,ApptStatus newStatus) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aptNum,newStatus);
				return;
			}
			string command="UPDATE appointment SET AptStatus="+POut.PInt((int)newStatus)
				+" WHERE AptNum="+POut.PInt(aptNum);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void SetAptStatusComplete(int aptNum,int planNum1,int planNum2) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aptNum,planNum1,planNum2);
				return;
			}
			string command="UPDATE appointment SET "
				+"AptStatus="+POut.PInt((int)ApptStatus.Complete)+", "
				+"InsPlan1="+POut.PInt(planNum1)+", "
				+"InsPlan2="+POut.PInt(planNum2)+" "
				+"WHERE AptNum="+POut.PInt(aptNum);
			Db.NonQ(command);
		}

		public static Appointment TableToObject(DataTable table) {
			//No need to check RemotingRole; no call to db.
			if(table.Rows.Count==0) {
				return null;
			}
			return TableToObjects(table)[0];
		}

		public static List<Appointment> TableToObjects(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List<Appointment> list=new List<Appointment>();
			Appointment apt;
			for(int i=0;i<table.Rows.Count;i++) {
				apt=new Appointment();
				apt.AptNum         =PIn.PInt(table.Rows[i][0].ToString());
				apt.PatNum         =PIn.PInt(table.Rows[i][1].ToString());
				apt.AptStatus      =(ApptStatus)PIn.PInt(table.Rows[i][2].ToString());
				apt.Pattern        =PIn.PString(table.Rows[i][3].ToString());
				apt.Confirmed      =PIn.PInt(table.Rows[i][4].ToString());
				apt.TimeLocked     =PIn.PBool(table.Rows[i][5].ToString());
				apt.Op             =PIn.PInt(table.Rows[i][6].ToString());
				apt.Note           =PIn.PString(table.Rows[i][7].ToString());
				apt.ProvNum        =PIn.PInt(table.Rows[i][8].ToString());
				apt.ProvHyg        =PIn.PInt(table.Rows[i][9].ToString());
				apt.AptDateTime    =PIn.PDateT(table.Rows[i][10].ToString());
				apt.NextAptNum     =PIn.PInt(table.Rows[i][11].ToString());
				apt.UnschedStatus  =PIn.PInt(table.Rows[i][12].ToString());
				//apt.Lab            =PIn.PInt(table.Rows[i][13].ToString());
				apt.IsNewPatient   =PIn.PBool(table.Rows[i][14].ToString());
				apt.ProcDescript   =PIn.PString(table.Rows[i][15].ToString());
				apt.Assistant      =PIn.PInt(table.Rows[i][16].ToString());
				apt.InstructorNum  =PIn.PInt(table.Rows[i][17].ToString());
				apt.SchoolClassNum =PIn.PInt(table.Rows[i][18].ToString());
				apt.SchoolCourseNum=PIn.PInt(table.Rows[i][19].ToString());
				apt.GradePoint     =PIn.PFloat(table.Rows[i][20].ToString());
				apt.ClinicNum      =PIn.PInt(table.Rows[i][21].ToString());
				apt.IsHygiene      =PIn.PBool(table.Rows[i][22].ToString());
				//DateTStamp
				apt.DateTimeArrived=PIn.PDateT(table.Rows[i][24].ToString());
				apt.DateTimeSeated =PIn.PDateT(table.Rows[i][25].ToString());
				apt.DateTimeDismissed=PIn.PDateT(table.Rows[i][26].ToString());
				apt.InsPlan1=PIn.PInt(table.Rows[i][27].ToString());
				apt.InsPlan2=PIn.PInt(table.Rows[i][28].ToString());
				list.Add(apt);
			}
			return list;
		}

		///<summary></summary>
		public static DataSet RefreshPeriod(DateTime dateStart,DateTime dateEnd) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetDS(MethodBase.GetCurrentMethod(),dateStart,dateEnd);
			} 
			DataSet retVal=new DataSet();
			retVal.Tables.Add(GetPeriodApptsTable(dateStart,dateEnd,0,false));//parameters[0],parameters[1],"0","0"));
			retVal.Tables.Add(GetPeriodEmployeeSchedTable(dateStart,dateEnd));
			retVal.Tables.Add(GetPeriodWaitingRoomTable(dateStart,dateEnd));
			retVal.Tables.Add(GetPeriodSchedule(dateStart,dateEnd));
			return retVal;
		}

		///<summary></summary>
		public static DataSet RefreshOneApt(int aptNum,bool isPlanned) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetDS(MethodBase.GetCurrentMethod(),aptNum,isPlanned);
			} 
			DataSet retVal=new DataSet();
			retVal.Tables.Add(GetPeriodApptsTable(DateTime.MinValue,DateTime.MinValue,aptNum,isPlanned));
			return retVal;
		}

		///<summary>If aptnum is specified, then the dates are ignored.  If getting data for one planned appt, then pass isPlanned=1.  This changes which procedures are retrieved.</summary>
		private static DataTable GetPeriodApptsTable(DateTime dateStart,DateTime dateEnd,int aptNum,bool isPlanned) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),dateStart,dateEnd,aptNum,isPlanned);
			} 
			//DateTime dateStart=PIn.PDate(strDateStart);
			//DateTime dateEnd=PIn.PDate(strDateEnd);
			//int aptNum=PIn.PInt(strAptNum);
			//bool isPlanned=PIn.PBool(strIsPlanned);
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("Appointments");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("age");
			table.Columns.Add("addrNote");
			table.Columns.Add("apptModNote");
			table.Columns.Add("aptDate");
			table.Columns.Add("aptDay");
			table.Columns.Add("aptLength");
			table.Columns.Add("aptTime");
			table.Columns.Add("AptDateTime");
			table.Columns.Add("AptNum");
			table.Columns.Add("AptStatus");
			table.Columns.Add("Assistant");
			table.Columns.Add("billingType");
			table.Columns.Add("chartNumber");
			table.Columns.Add("chartNumAndName");
			table.Columns.Add("confirmed");
			table.Columns.Add("Confirmed");
			table.Columns.Add("contactMethods");
			table.Columns.Add("creditIns");
			table.Columns.Add("famFinUrgNote");
			table.Columns.Add("hmPhone");
			table.Columns.Add("ImageFolder");
			table.Columns.Add("insurance");
			table.Columns.Add("IsHygiene");
			table.Columns.Add("lab");
			table.Columns.Add("MedUrgNote");
			table.Columns.Add("Note");
			table.Columns.Add("Op");
			table.Columns.Add("patientName");
			table.Columns.Add("PatNum");
			table.Columns.Add("patNum");
			table.Columns.Add("GuarNum");
			table.Columns.Add("patNumAndName");
			table.Columns.Add("Pattern");
			table.Columns.Add("preMedFlag");
			table.Columns.Add("procs");
			table.Columns.Add("production");
			table.Columns.Add("productionVal");
			table.Columns.Add("provider");
			table.Columns.Add("ProvHyg");
			table.Columns.Add("ProvNum");
			table.Columns.Add("wkPhone");
			table.Columns.Add("wirelessPhone");
			string command="SELECT p1.Abbr ProvAbbr,p2.Abbr HygAbbr,patient.AddrNote,"
				+"patient.ApptModNote,AptDateTime,appointment.AptNum,AptStatus,Assistant,"
				+"patient.BillingType,patient.BirthDate,"
				+"carrier1.CarrierName carrierName1,carrier2.CarrierName carrierName2,"
				+"patient.ChartNumber,Confirmed,patient.CreditType,DateTimeChecked,DateTimeDue,DateTimeRecd,DateTimeSent,"
				+"guar.FamFinUrgNote,patient.FName,patient.Guarantor,patient.HmPhone,patient.ImageFolder,IsHygiene,IsNewPatient,"
				+"LabCaseNum,patient.LName,patient.MedUrgNote,patient.MiddleI,Note,Op,appointment.PatNum,"
				+"Pattern,patplan.PlanNum,patient.PreferConfirmMethod,patient.PreferContactMethod,patient.Preferred,"
				+"patient.PreferRecallMethod,patient.Premed,"
				+"(SELECT SUM(ProcFee) FROM procedurelog ";
			if(isPlanned){
				command+="WHERE procedurelog.PlannedAptNum=appointment.AptNum AND procedurelog.PlannedAptNum!=0) Production, ";
			}
			else{
				command+="WHERE procedurelog.AptNum=appointment.AptNum AND procedurelog.AptNum!=0) Production, ";
			}
			command+="ProvHyg,appointment.ProvNum,patient.WirelessPhone,patient.WkPhone "
				+"FROM appointment LEFT JOIN patient ON patient.PatNum=appointment.PatNum "
				+"LEFT JOIN provider p1 ON p1.ProvNum=appointment.ProvNum "
				+"LEFT JOIN provider p2 ON p2.ProvNum=appointment.ProvHyg ";
			if(isPlanned){
				command+="LEFT JOIN labcase ON labcase.PlannedAptNum=appointment.AptNum ";
			}
			else{
				command+="LEFT JOIN labcase ON labcase.AptNum=appointment.AptNum ";
			}
			command+="LEFT JOIN patient guar ON guar.PatNum=patient.Guarantor "
				+"LEFT JOIN patplan ON patplan.PatNum=patient.PatNum "
				+"LEFT JOIN insplan plan1 ON InsPlan1=plan1.PlanNum "
				+"LEFT JOIN insplan plan2 ON InsPlan2=plan2.PlanNum "
				+"LEFT JOIN carrier carrier1 ON plan1.CarrierNum=carrier1.CarrierNum "
				+"LEFT JOIN carrier carrier2 ON plan2.CarrierNum=carrier2.CarrierNum ";
			if(aptNum==0){
				command+="WHERE AptDateTime >= "+POut.PDate(dateStart)+" "
					+"AND AptDateTime < "+POut.PDate(dateEnd.AddDays(1))+" "
					+ "AND AptStatus IN (1, 2, 4, 5, 7, 8) ";
			}
			else{
				command+="WHERE appointment.AptNum="+POut.PInt(aptNum);
			}
			command+=" GROUP BY appointment.AptNum";
			DataTable raw=dcon.GetTable(command);
			DataTable rawProc;
			if(raw.Rows.Count==0){
				rawProc=new DataTable();
			}
			else{
				command="SELECT pc.AbbrDesc,p.AptNum,p.CodeNum,p.PlannedAptNum,p.Surf,p.ToothNum,pc.TreatArea  "
					+"FROM procedurelog p,procedurecode pc "
					+"WHERE p.CodeNum=pc.CodeNum AND ";
				if(isPlanned) {
					command+="p.PlannedAptNum!=0 AND p.PlannedAptNum ";
				} 
				else {
					command+="p.AptNum!=0 AND p.AptNum ";
				}
				command+="IN(";//this was far too slow:SELECT a.AptNum FROM appointment a WHERE ";
				if(aptNum==0) {
					for(int a=0;a<raw.Rows.Count;a++){
						if(a>0){
							command+=",";
						}
						command+=raw.Rows[a]["AptNum"].ToString();
					}
					//command+="a.AptDateTime >= "+POut.PDate(dateStart)+" "
					//	+"AND a.AptDateTime < "+POut.PDate(dateEnd.AddDays(1));
				}
				else {
					command+=POut.PInt(aptNum);
				}
				command+=")";
				rawProc=dcon.GetTable(command);
			}
			DataTable rawInsProc=null;
			if(PrefC.GetBool("ApptExclamationShowForUnsentIns")){
				//procs for flag, InsNotSent
				command ="SELECT patient.PatNum, patient.Guarantor "
					+"FROM patient,procedurecode,procedurelog,claimproc "
					+"WHERE claimproc.procnum=procedurelog.procnum "
					+"AND patient.PatNum=procedurelog.PatNum "
					+"AND procedurelog.CodeNum=procedurecode.CodeNum "
					+"AND claimproc.NoBillIns=0 "
					+"AND procedurelog.ProcFee>0 "
					+"AND claimproc.Status=6 "//estimate
					+"AND procedurelog.procstatus=2 "
					+"AND procedurelog.ProcDate >= "+POut.PDate(DateTime.Now.AddYears(-1))+" "
					+"AND procedurelog.ProcDate <= "+POut.PDate(DateTime.Now)+ " "
					+"GROUP BY patient.Guarantor"; 
				rawInsProc=dcon.GetTable(command);
			}
			DateTime aptDate;
			TimeSpan span;
			int hours;
			int minutes;
			DateTime labDate;
			DateTime labDueDate;
			DateTime birthdate;
			for(int i=0;i<raw.Rows.Count;i++) {
				row=table.NewRow();
				row["addrNote"]="";
				if(raw.Rows[i]["AddrNote"].ToString()!=""){
					row["addrNote"]=Lans.g("Appointments","AddrNote: ")+raw.Rows[i]["AddrNote"].ToString();
				}
				aptDate=PIn.PDateT(raw.Rows[i]["AptDateTime"].ToString());
				row["AptDateTime"]=aptDate;
				birthdate=PIn.PDate(raw.Rows[i]["Birthdate"].ToString());
				row["age"]=Lans.g("Appointments","Age: ");
				if(birthdate.Year>1880){
					row["age"]+=PatientLogic.DateToAgeString(birthdate);
				}
				else{
					row["age"]+="?";
				}
				row["apptModNote"]="";
				if(raw.Rows[i]["ApptModNote"].ToString()!="") {
					row["apptModNote"]=Lans.g("Appointments","ApptModNote: ")+raw.Rows[i]["ApptModNote"].ToString();
				}
				row["aptDate"]=aptDate.ToShortDateString();
				row["aptDay"]=aptDate.ToString("dddd");
				span=TimeSpan.FromMinutes(raw.Rows[i]["Pattern"].ToString().Length*5);
				hours=span.Hours;
				minutes=span.Minutes;
				if(hours==0){
					row["aptLength"]=minutes.ToString()+Lans.g("Appointments"," Min");
				}
				else if(hours==1){
					row["aptLength"]=hours.ToString()+Lans.g("Appointments"," Hr, ")
						+minutes.ToString()+Lans.g("Appointments"," Min");
				}
				else{
					row["aptLength"]=hours.ToString()+Lans.g("Appointments"," Hrs, ")
						+minutes.ToString()+Lans.g("Appointments"," Min");
				}
				row["aptTime"]=aptDate.ToShortTimeString();
				row["AptNum"]=raw.Rows[i]["AptNum"].ToString();
				row["AptStatus"]=raw.Rows[i]["AptStatus"].ToString();
				row["Assistant"]=raw.Rows[i]["Assistant"].ToString();
				row["billingType"]=DefC.GetName(DefCat.BillingTypes,PIn.PInt(raw.Rows[i]["BillingType"].ToString()));
				row["chartNumber"]=raw.Rows[i]["ChartNumber"].ToString();
				row["chartNumAndName"]="";
				if(raw.Rows[i]["IsNewPatient"].ToString()=="1") {
					row["chartNumAndName"]="NP-";
				}
				row["chartNumAndName"]+=raw.Rows[i]["ChartNumber"].ToString()+" "
					+PatientLogic.GetNameLF(raw.Rows[i]["LName"].ToString(),raw.Rows[i]["FName"].ToString(),
					raw.Rows[i]["Preferred"].ToString(),raw.Rows[i]["MiddleI"].ToString());
				row["confirmed"]=DefC.GetName(DefCat.ApptConfirmed,PIn.PInt(raw.Rows[i]["Confirmed"].ToString()));
				row["Confirmed"]=raw.Rows[i]["Confirmed"].ToString();
				row["contactMethods"]="";
				if(raw.Rows[i]["PreferConfirmMethod"].ToString()!="0"){
					row["contactMethods"]+=Lans.g("Appointments","Confirm Method: ")
						+((ContactMethod)PIn.PInt(raw.Rows[i]["PreferConfirmMethod"].ToString())).ToString();
				}
				if(raw.Rows[i]["PreferContactMethod"].ToString()!="0"){
					if(row["contactMethods"].ToString()!="") {
						row["contactMethods"]+="\r\n";
					}
					row["contactMethods"]+=Lans.g("Appointments","Contact Method: ")
						+((ContactMethod)PIn.PInt(raw.Rows[i]["PreferContactMethod"].ToString())).ToString();
				}
				if(raw.Rows[i]["PreferRecallMethod"].ToString()!="0"){
					if(row["contactMethods"].ToString()!="") {
						row["contactMethods"]+="\r\n";
					}
					row["contactMethods"]+=Lans.g("Appointments","Recall Method: ")
						+((ContactMethod)PIn.PInt(raw.Rows[i]["PreferRecallMethod"].ToString())).ToString();
				}
				bool InsToSend=false;
				if(rawInsProc!=null){
					//figure out if pt's family has ins claims that need to be created
					for(int j=0;j<rawInsProc.Rows.Count;j++){
						if(raw.Rows[i]["PlanNum"].ToString()!="" && raw.Rows[i]["PlanNum"].ToString()!="0") {
							if (raw.Rows[i]["Guarantor"].ToString()==rawInsProc.Rows[j]["Guarantor"].ToString() 
								|| raw.Rows[i]["Guarantor"].ToString()==rawInsProc.Rows[j]["PatNum"].ToString())
							{
								InsToSend=true;
							}
						}
					}
				}
				row["creditIns"]=raw.Rows[i]["CreditType"].ToString();
				if(InsToSend) {
					row["creditIns"]+="!";
				}
				else if(raw.Rows[i]["PlanNum"].ToString()!="" && raw.Rows[i]["PlanNum"].ToString()!="0") {
					row["creditIns"]+="I";
				}
				row["famFinUrgNote"]="";
				if(raw.Rows[i]["FamFinUrgNote"].ToString()!="") {
					row["famFinUrgNote"]=Lans.g("Appointments","FamFinUrgNote: ")+raw.Rows[i]["FamFinUrgNote"].ToString();
				}
				row["hmPhone"]=Lans.g("Appointments","Hm: ")+raw.Rows[i]["HmPhone"].ToString();
				row["ImageFolder"]=raw.Rows[i]["ImageFolder"].ToString();
				row["insurance"]="";
				if(raw.Rows[i]["carrierName1"].ToString()!="") {
					row["insurance"]+=raw.Rows[i]["carrierName1"].ToString();
					if(raw.Rows[i]["carrierName2"].ToString()!="") {
						//if(row["insurance"].ToString()!="") {
						row["insurance"]+="\r\n";
						//}
						row["insurance"]+=raw.Rows[i]["carrierName2"].ToString();
					}
				}
				else if(raw.Rows[i]["PlanNum"].ToString()!="" && raw.Rows[i]["PlanNum"].ToString()!="0"){
					row["insurance"]=Lans.g("Appointments","Insured");
				}
				row["IsHygiene"]=raw.Rows[i]["IsHygiene"].ToString();
				row["lab"]="";
				if(raw.Rows[i]["LabCaseNum"].ToString()!=""){
					labDate=PIn.PDateT(raw.Rows[i]["DateTimeChecked"].ToString());
					if(labDate.Year>1880) {
						row["lab"]=Lans.g("Appointments","Lab Quality Checked");
					}
					else {
						labDate=PIn.PDateT(raw.Rows[i]["DateTimeRecd"].ToString());
						if(labDate.Year>1880) {
							row["lab"]=Lans.g("Appointments","Lab Received");
						}
						else {
							labDate=PIn.PDateT(raw.Rows[i]["DateTimeSent"].ToString());
							if(labDate.Year>1880) {
								row["lab"]=Lans.g("Appointments","Lab Sent");//sent but not received
							}
							else {
								row["lab"]=Lans.g("Appointments","Lab Not Sent");
							}
							labDueDate=PIn.PDateT(raw.Rows[i]["DateTimeDue"].ToString());
							if(labDueDate.Year>1880) {
								row["lab"]+=", "+Lans.g("Appointments","Due: ")//+dateDue.ToString("ddd")+" "
									+labDueDate.ToShortDateString();//+" "+dateDue.ToShortTimeString();
							}
						}
					}
				}
				row["MedUrgNote"]=raw.Rows[i]["MedUrgNote"].ToString();
				row["Note"]=raw.Rows[i]["Note"].ToString();
				row["Op"]=raw.Rows[i]["Op"].ToString();
				if(raw.Rows[i]["IsNewPatient"].ToString()=="1"){
					row["patientName"]="NP-";
				}
				row["patientName"]+=PatientLogic.GetNameLF(raw.Rows[i]["LName"].ToString(),raw.Rows[i]["FName"].ToString(),
					raw.Rows[i]["Preferred"].ToString(),raw.Rows[i]["MiddleI"].ToString());
				row["PatNum"]=raw.Rows[i]["PatNum"].ToString();
				row["patNum"]="PatNum: "+raw.Rows[i]["PatNum"].ToString();
				row["GuarNum"]=raw.Rows[i]["Guarantor"].ToString();
				row["patNumAndName"]="";
				if(raw.Rows[i]["IsNewPatient"].ToString()=="1") {
					row["patNumAndName"]="NP-";
				}
				row["patNumAndName"]+=raw.Rows[i]["PatNum"].ToString()+" "
					+PatientLogic.GetNameLF(raw.Rows[i]["LName"].ToString(),raw.Rows[i]["FName"].ToString(),
					raw.Rows[i]["Preferred"].ToString(),raw.Rows[i]["MiddleI"].ToString());
				row["Pattern"]=raw.Rows[i]["Pattern"].ToString();
				row["preMedFlag"]="";
				if(raw.Rows[i]["Premed"].ToString()=="1"){
					row["preMedFlag"]=Lans.g("Appointments","Premedicate");
				}
				row["procs"]="";
				for(int p=0;p<rawProc.Rows.Count;p++){
					if(!isPlanned && rawProc.Rows[p]["AptNum"].ToString()!=raw.Rows[i]["AptNum"].ToString()){
						continue;
					}
					if(isPlanned && rawProc.Rows[p]["PlannedAptNum"].ToString()!=raw.Rows[i]["AptNum"].ToString()) {
						continue;
					}
					if(row["procs"].ToString()!=""){
						row["procs"]+=", ";
					}
					switch(rawProc.Rows[p]["TreatArea"].ToString()) {
						case "1"://TreatmentArea.Surf:
							row["procs"]+="#"+Tooth.GetToothLabel(rawProc.Rows[p]["ToothNum"].ToString())+"-"
								+rawProc.Rows[p]["Surf"].ToString()+"-";//""#12-MOD-"
							break;
						case "2"://TreatmentArea.Tooth:
							row["procs"]+="#"+Tooth.GetToothLabel(rawProc.Rows[p]["ToothNum"].ToString())+"-";//"#12-"
							break;
						default://area 3 or 0 (mouth)
							break;
						case "4"://TreatmentArea.Quad:
							row["procs"]+=rawProc.Rows[p]["Surf"].ToString()+"-";//"UL-"
							break;
						case "5"://TreatmentArea.Sextant:
							row["procs"]+="S"+rawProc.Rows[p]["Surf"].ToString()+"-";//"S2-"
							break;
						case "6"://TreatmentArea.Arch:
							row["procs"]+=rawProc.Rows[p]["Surf"].ToString()+"-";//"U-"
							break;
						case "7"://TreatmentArea.ToothRange:
							//strLine+=table.Rows[j][13].ToString()+" ";//don't show range
							break;
					}
					row["procs"]+=rawProc.Rows[p]["AbbrDesc"].ToString();	
				}
				row["production"]=PIn.PDouble(raw.Rows[i]["Production"].ToString()).ToString("c");
				row["productionVal"]=raw.Rows[i]["Production"].ToString();
				if(raw.Rows[i]["IsHygiene"].ToString()=="1"){
					row["provider"]=raw.Rows[i]["HygAbbr"].ToString();
					if(raw.Rows[i]["ProvAbbr"].ToString()!=""){
						row["provider"]+=" ("+raw.Rows[i]["ProvAbbr"].ToString()+")";
					}
				}
				else{
					row["provider"]=raw.Rows[i]["ProvAbbr"].ToString();
					if(raw.Rows[i]["HygAbbr"].ToString()!="") {
						row["provider"]+=" ("+raw.Rows[i]["HygAbbr"].ToString()+")";
					}
				}
				row["ProvNum"]=raw.Rows[i]["ProvNum"].ToString();
				row["ProvHyg"]=raw.Rows[i]["ProvHyg"].ToString();
				row["wirelessPhone"]=Lans.g("Appointments","Cell: ")+raw.Rows[i]["WirelessPhone"].ToString();
				row["wkPhone"]=Lans.g("Appointments","Wk: ")+raw.Rows[i]["WkPhone"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		private static DataTable GetPeriodEmployeeSchedTable(DateTime dateStart,DateTime dateEnd) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),dateStart,dateEnd);
			}
			//DateTime dateStart=PIn.PDate(strDateStart);
			//DateTime dateEnd=PIn.PDate(strDateEnd);
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("EmpSched");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("empName");
			table.Columns.Add("schedule");
			if(dateStart!=dateEnd) {
				return table;
			}
			string command="SELECT StartTime,StopTime,FName,employee.EmployeeNum "
				+"FROM employee,schedule "
				+"WHERE schedule.EmployeeNum=employee.EmployeeNum "
				+"AND SchedType=3 "//employee
				+"AND SchedDate = "+POut.PDate(dateStart)+" "
				+"ORDER BY schedule.EmployeeNum,StartTime";
			DataTable raw=dcon.GetTable(command);
			DateTime startTime;
			DateTime stopTime;
			for(int i=0;i<raw.Rows.Count;i++) {
				row=table.NewRow();
				if(i==0 || raw.Rows[i]["EmployeeNum"].ToString()!=raw.Rows[i-1]["EmployeeNum"].ToString()){
					row["empName"]=raw.Rows[i]["FName"].ToString();
				}
				else{
					row["empName"]="";
				}
				if(row["schedule"].ToString()!=""){
					row["schedule"]+=",";
				}
				startTime=PIn.PDateT(raw.Rows[i]["StartTime"].ToString());
				stopTime=PIn.PDateT(raw.Rows[i]["StopTime"].ToString());
				row["schedule"]+=startTime.ToString("h:mm")+"-"+stopTime.ToString("h:mm");
				table.Rows.Add(row);
			}
			return table;
		}

		private static DataTable GetPeriodWaitingRoomTable(DateTime dateStart,DateTime dateEnd) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),dateStart,dateEnd);
			}
			//DateTime dateStart=PIn.PDate(strDateStart);
			//DateTime dateEnd=PIn.PDate(strDateEnd);
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("WaitingRoom");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("patName");
			table.Columns.Add("waitTime");
			if(dateStart!=dateEnd) {
				return table;
			}
			string command="SELECT DateTimeArrived,DateTimeSeated,LName,FName,Preferred,NOW() dateTimeNow "
				+"FROM appointment,patient "
				+"WHERE appointment.PatNum=patient.PatNum "
				+"AND DATE(AptDateTime) = "+POut.PDate(dateStart)+" "
				+"AND TIME(DateTimeArrived) > 0 "
				+"AND TIME(DateTimeArrived) < CURTIME() "
				+"AND TIME(DateTimeSeated) = 0 "
				+"ORDER BY AptDateTime";
			DataTable raw=dcon.GetTable(command);
			TimeSpan timeArrived;
			//DateTime timeSeated;
			DateTime waitTime;
			Patient pat;
			DateTime dateTimeNow;
			//int minutes;
			for(int i=0;i<raw.Rows.Count;i++) {
				row=table.NewRow();
				pat=new Patient();
				pat.LName=raw.Rows[i]["LName"].ToString();
				pat.FName=raw.Rows[i]["FName"].ToString();
				pat.Preferred=raw.Rows[i]["Preferred"].ToString();
				row["patName"]=pat.GetNameLF();
				dateTimeNow=PIn.PDateT(raw.Rows[i]["dateTimeNow"].ToString());
				timeArrived=(PIn.PDateT(raw.Rows[i]["DateTimeArrived"].ToString())).TimeOfDay;
				waitTime=dateTimeNow-timeArrived;
				row["waitTime"]=waitTime.ToString("H:mm:ss");
				//minutes=waitTime.Minutes;
				//if(waitTime.Hours>0){
				//	row["waitTime"]+=waitTime.Hours.ToString()+"h ";
					//minutes-=60*waitTime.Hours;
				//}
				//row["waitTime"]+=waitTime.Minutes.ToString()+"m";
				table.Rows.Add(row);
			}
			return table;
		}

		private static DataTable GetPeriodSchedule(DateTime dateStart,DateTime dateEnd){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),dateStart,dateEnd);
			}
			DataTable table=new DataTable("Schedule");
			table.Columns.Add("ScheduleNum");
			table.Columns.Add("SchedDate");
			table.Columns.Add("StartTime");
			table.Columns.Add("StopTime");
			table.Columns.Add("SchedType");
			table.Columns.Add("ProvNum");
			table.Columns.Add("BlockoutType");
			table.Columns.Add("Note");
			table.Columns.Add("Status");
			table.Columns.Add("ops");
			table.Columns.Add("EmployeeNum");
			string command="SELECT schedule.*,GROUP_CONCAT(scheduleop.OperatoryNum) _ops "
				+"FROM schedule "
				+"LEFT JOIN scheduleop ON schedule.ScheduleNum=scheduleop.ScheduleNum "
				+"WHERE SchedDate >= "+POut.PDate(dateStart)+" "
				+"AND SchedDate <= "+POut.PDate(dateEnd)+" "
				+"GROUP BY schedule.ScheduleNum "
				+"ORDER BY StartTime";
			DataTable raw=Db.GetTable(command);
			//the times come back as times rather than datetimes.  This causes problems.  That's why we're not just returning raw.
			DataRow row;
			for(int i=0;i<raw.Rows.Count;i++){
				row=table.NewRow();
				row["ScheduleNum"]=raw.Rows[i]["ScheduleNum"].ToString();
				row["SchedDate"]=POut.PDate(PIn.PDate(raw.Rows[i]["SchedDate"].ToString()),false);
				row["StartTime"]=POut.PDateT(PIn.PDateT(raw.Rows[i]["StartTime"].ToString()),false);
				row["StopTime"]=POut.PDateT(PIn.PDateT(raw.Rows[i]["StopTime"].ToString()),false);
				row["SchedType"]=raw.Rows[i]["SchedType"].ToString();
				row["ProvNum"]=raw.Rows[i]["ProvNum"].ToString();
				row["BlockoutType"]=raw.Rows[i]["BlockoutType"].ToString();
				row["Note"]=raw.Rows[i]["Note"].ToString();
				row["Status"]=raw.Rows[i]["Status"].ToString();
				row["ops"]=PIn.PByteArray(raw.Rows[i]["_ops"]);
				row["EmployeeNum"]=raw.Rows[i]["EmployeeNum"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		//Get DS for one appointment in Edit window--------------------------------------------------------------------------------
		//-------------------------------------------------------------------------------------------------------------------------

		///<summary></summary>
		public static DataSet GetApptEdit(int aptNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetDS(MethodBase.GetCurrentMethod(),aptNum);
			}
			DataSet retVal=new DataSet();
			retVal.Tables.Add(GetApptTable(aptNum));
			retVal.Tables.Add(GetPatTable(retVal.Tables["Appointment"].Rows[0]["PatNum"].ToString()));
			retVal.Tables.Add(GetProcTable(retVal.Tables["Appointment"].Rows[0]["PatNum"].ToString(),aptNum.ToString(),
				retVal.Tables["Appointment"].Rows[0]["AptStatus"].ToString(),
				retVal.Tables["Appointment"].Rows[0]["AptDateTime"].ToString()
				));
			retVal.Tables.Add(GetCommTable(retVal.Tables["Appointment"].Rows[0]["PatNum"].ToString()));
			bool isPlanned=false;
			if(retVal.Tables["Appointment"].Rows[0]["AptStatus"].ToString()=="6"){
				isPlanned=true;
			}
			retVal.Tables.Add(GetMiscTable(aptNum.ToString(),isPlanned));
			return retVal;
		}

		private static DataTable GetApptTable(int aptNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),aptNum);
			}
			string command="SELECT * FROM appointment WHERE AptNum="+aptNum.ToString();
			DataTable table=Db.GetTable(command);
			table.TableName="Appointment";
			return table;
		}

		private static DataTable GetPatTable(string patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),patNum);
			}
			DataTable table=new DataTable("Patient");
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("field");
			table.Columns.Add("value");
			string command="SELECT * FROM patient WHERE PatNum="+patNum;
			DataTable rawPat=Db.GetTable(command);
			DataRow row;
			//Patient Name--------------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","Name");
			row["value"]=PatientLogic.GetNameLF(rawPat.Rows[0]["LName"].ToString(),rawPat.Rows[0]["FName"].ToString(),
				rawPat.Rows[0]["Preferred"].ToString(),rawPat.Rows[0]["MiddleI"].ToString());
			table.Rows.Add(row);
			//Patient First Name--------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","First Name");
			row["value"]=rawPat.Rows[0]["FName"];
			table.Rows.Add(row);
			//Patient Last name---------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","Last Name");
			row["value"]=rawPat.Rows[0]["LName"];
			table.Rows.Add(row);
			//Patient middle initial----------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","Middle Initial");
			row["value"]=rawPat.Rows[0]["MiddleI"];
			table.Rows.Add(row);
			//Patient home phone--------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","Home Phone");
			row["value"]=rawPat.Rows[0]["HmPhone"];
			table.Rows.Add(row);
			//Patient work phone--------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","Work Phone");
			row["value"]=rawPat.Rows[0]["WkPhone"];
			table.Rows.Add(row);
			//Patient wireless phone----------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","Wireless Phone");
			row["value"]=rawPat.Rows[0]["WirelessPhone"];
			table.Rows.Add(row);
			//Patient credit type-------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","Credit Type");
			row["value"]=rawPat.Rows[0]["CreditType"];
			table.Rows.Add(row);
			//Patient billing type------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","Billing Type");
			row["value"]=DefC.GetName(DefCat.BillingTypes,PIn.PInt(rawPat.Rows[0]["BillingType"].ToString()));
			table.Rows.Add(row);
			//Patient total balance-----------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","Total Balance");
			double totalBalance=PIn.PDouble(rawPat.Rows[0]["EstBalance"].ToString());
			row["value"]=totalBalance.ToString("F");
			table.Rows.Add(row);
			//Patient address and phone notes-------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","Address and Phone Notes");
			row["value"]=rawPat.Rows[0]["AddrNote"];
			table.Rows.Add(row);
			//Patient family balance----------------------------------------------------------------
			command="SELECT BalTotal,InsEst FROM patient WHERE Guarantor='"
				+rawPat.Rows[0]["Guarantor"].ToString()+"'";
			DataTable familyBalance=Db.GetTable(command);
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","Family Balance");
			double balance=PIn.PDouble(familyBalance.Rows[0]["BalTotal"].ToString())
				-PIn.PDouble(familyBalance.Rows[0]["InsEst"].ToString());
			row["value"]=balance.ToString("F");
			table.Rows.Add(row);
			//Site----------------------------------------------------------------------------------
			if(!PrefC.GetBool("EasyHidePublicHealth")){
				row=table.NewRow();
				row["field"]=Lans.g("FormApptEdit","Site");
				row["value"]=Sites.GetDescription(PIn.PInt(rawPat.Rows[0]["SiteNum"].ToString()));
				table.Rows.Add(row);
			}
			return table;
		}

		private static DataTable GetProcTable(string patNum,string aptNum,string apptStatus,string aptDateTime) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),patNum,aptNum,apptStatus,aptDateTime);
			}
			DataTable table=new DataTable("Procedure");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("attached");//0 or 1
			table.Columns.Add("CodeNum");
			table.Columns.Add("descript");
			table.Columns.Add("fee");
			table.Columns.Add("priority");
			table.Columns.Add("Priority");
			table.Columns.Add("ProcCode");
			table.Columns.Add("ProcNum");
			table.Columns.Add("ProcStatus");
			table.Columns.Add("ProvNum");
			table.Columns.Add("status");
			table.Columns.Add("toothNum");
			table.Columns.Add("ToothNum");
			table.Columns.Add("ToothRange");
			table.Columns.Add("Surf");
			//but we won't actually fill this table with rows until the very end.  It's more useful to use a List<> for now.
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT procedurecode.ProcCode,AptNum,LaymanTerm,"
				+"PlannedAptNum,Priority,ProcFee,ProcNum,ProcStatus,Surf,ToothNum, "
				+"ToothRange,procedurecode.Descript,procedurelog.CodeNum,procedurelog.ProvNum,ToothRange "
				+"FROM procedurelog LEFT JOIN procedurecode ON procedurelog.CodeNum=procedurecode.CodeNum "
				+"WHERE PatNum="+patNum//sort later
			//1. All TP procs
				+" AND (ProcStatus=1 OR ";//tp
			//2. All attached procs
				//+" AND ";
			if(apptStatus=="6"){//planned
				command+="PlannedAptNum="+aptNum;
			}
			else{
				command+="(AptNum="+aptNum
					+" AND (AptNum=0 OR AptNum="+aptNum+"))";//exclude procs attached to other appts.
			}
			//3. All unattached completed procs with same date as appt.
			//but only if one of these types
			if(apptStatus=="1" || apptStatus=="2" || apptStatus=="4" || apptStatus=="5"){//sched,C,ASAP,broken
				DateTime aptDate=PIn.PDateT(aptDateTime);
				command+=" OR (AptNum=0 "//unattached
					+"AND ProcStatus=2 "//complete
					+"AND Date(ProcDate)="+POut.PDate(aptDate)+")";//same date
			}
			command+=")";
			DataTable rawProc=Db.GetTable(command);
			for(int i=0;i<rawProc.Rows.Count;i++) {
				row=table.NewRow();
				if(apptStatus=="6"){//planned
					row["attached"]=(rawProc.Rows[i]["PlannedAptNum"].ToString()==aptNum) ? "1" : "0";
				}
				else{
					row["attached"]=(rawProc.Rows[i]["AptNum"].ToString()==aptNum) ? "1" : "0";
				}
				row["CodeNum"]=rawProc.Rows[i]["CodeNum"].ToString();
				row["descript"]="";
				if(rawProc.Rows[i]["AptNum"].ToString()!="0" && rawProc.Rows[i]["AptNum"].ToString()!=aptNum) {
					row["descript"]=Lans.g("FormApptEdit","(other appt)");
				}
				if(rawProc.Rows[i]["LaymanTerm"].ToString()==""){
					row["descript"]+=rawProc.Rows[i]["Descript"].ToString();
				}
				else{
					row["descript"]+=rawProc.Rows[i]["LaymanTerm"].ToString();
				}
				if(rawProc.Rows[i]["ToothRange"].ToString()!=""){
					row["descript"]+=" #"+Tooth.FormatRangeForDisplay(rawProc.Rows[i]["ToothRange"].ToString());
				}
				row["fee"]=PIn.PDouble(rawProc.Rows[i]["ProcFee"].ToString()).ToString("F");
				row["priority"]=DefC.GetName(DefCat.TxPriorities,PIn.PInt(rawProc.Rows[i]["Priority"].ToString()));
				row["Priority"]=rawProc.Rows[i]["Priority"].ToString();
				row["ProcCode"]=rawProc.Rows[i]["ProcCode"].ToString();
				row["ProcNum"]=rawProc.Rows[i]["ProcNum"].ToString();
				row["ProcStatus"]=rawProc.Rows[i]["ProcStatus"].ToString();
				row["ProvNum"]=rawProc.Rows[i]["ProvNum"].ToString();
				row["status"]=((ProcStat)PIn.PInt(rawProc.Rows[i]["ProcStatus"].ToString())).ToString();
				row["toothNum"]=Tooth.GetToothLabel(rawProc.Rows[i]["ToothNum"].ToString());
				row["ToothNum"]=rawProc.Rows[i]["ToothNum"].ToString();
				row["ToothRange"]=rawProc.Rows[i]["ToothRange"].ToString();
				row["Surf"]=rawProc.Rows[i]["Surf"].ToString();
				rows.Add(row);
			}
			//Sorting
			rows.Sort(CompareRows);
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		///<summary>The supplied DataRows must include the following columns: attached,Priority,ToothRange,ToothNum,ProcCode. This sorts all objects in Chart module based on their dates, times, priority, and toothnum.  For time comparisons, procs are not included.  But if other types such as comm have a time component in ProcDate, then they will be sorted by time as well.</summary>
		public static int CompareRows(DataRow x,DataRow y) {
			//No need to check RemotingRole; no call to db.
			/*if(x["attached"].ToString()!=y["attached"].ToString()){//if one is attached and the other is not
				if(x["attached"].ToString()=="1"){
					return -1;
				}
				else{
					return 1;
				}
			}*/
			return ProcedureLogic.CompareProcedures(x,y);//sort by priority, toothnum, procCode
		}

		private static DataTable GetCommTable(string patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),patNum);
			}
			DataTable table=new DataTable("Comm");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("commDateTime");
			table.Columns.Add("CommlogNum");
			table.Columns.Add("CommType");
			table.Columns.Add("Note");
			string command="SELECT * FROM commlog WHERE PatNum="+patNum+" AND IsStatementSent=0 "//don't include StatementSent
				+"ORDER BY CommDateTime";
			DataTable rawComm=Db.GetTable(command);
			for(int i=0;i<rawComm.Rows.Count;i++) {
				row=table.NewRow();
				row["commDateTime"]=PIn.PDateT(rawComm.Rows[i]["commDateTime"].ToString()).ToShortDateString();
				row["CommlogNum"]=rawComm.Rows[i]["CommlogNum"].ToString();
				row["CommType"]=rawComm.Rows[i]["CommType"].ToString();
				row["Note"]=rawComm.Rows[i]["Note"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		private static DataTable GetMiscTable(string aptNum,bool isPlanned) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),aptNum,isPlanned);
			}
			DataTable table=new DataTable("Misc");
			DataRow row;
			table.Columns.Add("LabCaseNum");
			table.Columns.Add("labDescript");
			table.Columns.Add("requirements");
			string command="SELECT LabCaseNum,DateTimeDue,DateTimeChecked,DateTimeRecd,DateTimeSent,"
				+"laboratory.Description FROM labcase,laboratory "
				+"WHERE labcase.LaboratoryNum=laboratory.LaboratoryNum AND ";
			if(isPlanned){
				command+="labcase.PlannedAptNum="+aptNum;
			}
			else {
				command+="labcase.AptNum="+aptNum;
			}
			DataTable raw=Db.GetTable(command);
			DateTime date;
			DateTime dateDue;
			//for(int i=0;i<raw.Rows.Count;i++) {//always return one row:
			row=table.NewRow();
			row["LabCaseNum"]="0";
			row["labDescript"]="";
			if(raw.Rows.Count>0){
				row["LabCaseNum"]=raw.Rows[0]["LabCaseNum"].ToString();
				row["labDescript"]=raw.Rows[0]["Description"].ToString();
				date=PIn.PDateT(raw.Rows[0]["DateTimeChecked"].ToString());
				if(date.Year>1880){
					row["labDescript"]+=", "+Lans.g("FormApptEdit","Quality Checked");
				}
				else{
					date=PIn.PDateT(raw.Rows[0]["DateTimeRecd"].ToString());
					if(date.Year>1880){
						row["labDescript"]+=", "+Lans.g("FormApptEdit","Received");
					}
					else{
						date=PIn.PDateT(raw.Rows[0]["DateTimeSent"].ToString());
						if(date.Year>1880){
							row["labDescript"]+=", "+Lans.g("FormApptEdit","Sent");//sent but not received
						}
						else{
							row["labDescript"]+=", "+Lans.g("FormApptEdit","Not Sent");
						}
						dateDue=PIn.PDateT(raw.Rows[0]["DateTimeDue"].ToString());
						if(dateDue.Year>1880) {
							row["labDescript"]+=", "+Lans.g("FormAppEdit","Due: ")+dateDue.ToString("ddd")+" "
								+dateDue.ToShortDateString()+" "+dateDue.ToShortTimeString();
						}
					}
				}
			}
			//requirements-------------------------------------------------------------------------------------------
			command="SELECT "
				+"reqstudent.Descript,LName,FName "
				+"FROM reqstudent,provider "//schoolcourse "
				+"WHERE reqstudent.ProvNum=provider.ProvNum "
				+"AND reqstudent.AptNum="+aptNum;
			raw=Db.GetTable(command);
			row["requirements"]="";
			for(int i=0;i<raw.Rows.Count;i++){
				if(i!=0){
					row["requirements"]+="\r\n";
				}
				row["requirements"]+=raw.Rows[i]["LName"].ToString()+", "+raw.Rows[i]["FName"].ToString()
					+": "+raw.Rows[i]["Descript"].ToString();
			}
			table.Rows.Add(row);
			return table;
		}

		//private static DataRow GetRowFromTable(
		///<summary></summary>
		public static void Delete(int aptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aptNum);
				return;
			}
			string command;
			command="SELECT PatNum,IsNewPatient,AptStatus FROM appointment WHERE AptNum="+POut.PInt(aptNum);
			DataTable table=Db.GetTable(command);
			Patient pat=Patients.GetPat(PIn.PInt(table.Rows[0]["PatNum"].ToString()));
			if(table.Rows[0]["IsNewPatient"].ToString()=="1") {
				Procedures.SetDateFirstVisit(DateTime.MinValue,3,pat);
			}
			//procs
			if(table.Rows[0]["AptStatus"].ToString()=="6") {//planned
				command="UPDATE procedurelog SET PlannedAptNum =0 WHERE PlannedAptNum = "+POut.PInt(aptNum);
			}
			else {
				command="UPDATE procedurelog SET AptNum =0 WHERE AptNum = "+POut.PInt(aptNum);
			}
			Db.NonQ(command);
			//labcases
			if(table.Rows[0]["AptStatus"].ToString()=="6") {//planned
				command="UPDATE labcase SET PlannedAptNum =0 WHERE PlannedAptNum = "+POut.PInt(aptNum);
			}
			else {
				command="UPDATE labcase SET AptNum =0 WHERE AptNum = "+POut.PInt(aptNum);
			}
			Db.NonQ(command);
			//plannedappt
			command="DELETE FROM plannedappt WHERE AptNum="+POut.PInt(aptNum);
			Db.NonQ(command);
			//we will not reset item orders here
			command="DELETE FROM appointment WHERE AptNum = "+POut.PInt(aptNum);
			Db.NonQ(command);
			DeletedObjects.SetDeleted(DeletedObjectType.Appointment,aptNum);
		}

	}
}
