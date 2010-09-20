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
				+"WHERE AptDateTime BETWEEN '"+POut.Date(startDate,false)+"' AND '"+POut.Date(endDate.AddDays(1),false)+"'"
				+"AND aptstatus != '"+(int)ApptStatus.UnschedList+"' "
				+"AND aptstatus != '"+(int)ApptStatus.Planned+"'";
			return Crud.AppointmentCrud.SelectMany(command).ToArray();
		}

		///<summary>Gets list of unscheduled appointments.  Allowed orderby: status, alph, date</summary>
		public static Appointment[] RefreshUnsched(string orderby,long provNum,long siteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Appointment[]>(MethodBase.GetCurrentMethod(),orderby,provNum,siteNum);
			}
			string command="SELECT * FROM appointment "
				+"LEFT JOIN patient ON patient.PatNum=appointment.PatNum "
				+"WHERE AptStatus = "+POut.Long((int)ApptStatus.UnschedList)+" ";
			if(provNum>0) {
				command+="AND (appointment.ProvNum="+POut.Long(provNum)+" OR appointment.ProvHyg="+POut.Long(provNum)+") ";
			}
			if(siteNum>0) {
				command+="AND patient.SiteNum="+POut.Long(siteNum)+" ";
			}
			command+="HAVING patient.PatStatus= "+POut.Long((int)PatientStatus.Patient)+" ";			
			if(orderby=="status") {
				command+="ORDER BY UnschedStatus,AptDateTime";
			}
			else if(orderby=="alph") {
				command+="ORDER BY LName,FName";
			}
			else { //if(orderby=="date"){
				command+="ORDER BY AptDateTime";
			}
			return Crud.AppointmentCrud.SelectMany(command).ToArray();
		}

		///<summary>Gets list of asap appointments.</summary>
		public static List<Appointment> RefreshASAP(long provNum,long siteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Appointment>>(MethodBase.GetCurrentMethod(),provNum,siteNum);
			}
			string command="SELECT * FROM appointment ";
			//if(orderby=="alph" || siteNum>0) {
			//command+="LEFT JOIN patient ON patient.PatNum=appointment.PatNum ";
			//}
			if(siteNum>0) {
				command+="LEFT JOIN patient ON patient.PatNum=appointment.PatNum ";
			}
			command+="WHERE AptStatus = "+POut.Long((int)ApptStatus.ASAP)+" ";
			if(provNum>0) {
				command+="AND (appointment.ProvNum="+POut.Long(provNum)+" OR appointment.ProvHyg="+POut.Long(provNum)+") ";
			}
			if(siteNum>0) {
				command+="AND patient.SiteNum="+POut.Long(siteNum)+" ";
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
			return Crud.AppointmentCrud.SelectMany(command);
		}

		///<summary>Allowed orderby: status, alph, date</summary>
		public static List<Appointment> RefreshPlannedTracker(string orderby,long provNum,long siteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Appointment>>(MethodBase.GetCurrentMethod(),orderby,provNum,siteNum);
			}
			//We create a in-memory temporary table by joining the appointment and patient
			//tables to get a list of planned appointments for active paients, then we
			//perform a left join on that temporary table against the appointment table
			//to exclude any appointments in the temporary table which are already refereced
			//by the NextAptNum column by any other appointment within the appointment table.
			//Using an in-memory temporary table reduces the number of row comparisons performed for
			//this query overall as compared to left joining the appointment table onto itself,
			//because the in-memory temporary table has many fewer rows than the appointment table
			//on average.
			string command="SELECT tplanned.*,tregular.AptNum "
				+"FROM (SELECT a.* FROM appointment a,patient p "
					+"WHERE a.AptStatus="+POut.Long((int)ApptStatus.Planned)
					+" AND p.PatStatus="+POut.Long((int)PatientStatus.Patient)+" AND a.PatNum=p.PatNum ";
			if(provNum>0) {
				command+="AND (a.ProvNum="+POut.Long(provNum)+" OR a.ProvHyg="+POut.Long(provNum)+") ";
			}
			if(siteNum>0) {
				command+="AND p.SiteNum="+POut.Long(siteNum)+" ";
			}
			if(orderby=="status") {
				command+="ORDER BY a.UnschedStatus,a.AptDateTime";
			} 
			else if(orderby=="alph") {
				command+="ORDER BY p.LName,p.FName";
			} 
			else { //if(orderby=="date"){
				command+="ORDER BY a.AptDateTime";
			}
			command+=") tplanned "
				+"LEFT JOIN appointment tregular ON tplanned.AptNum=tregular.NextAptNum "
				+"WHERE tregular.AptNum IS NULL";
			return Crud.AppointmentCrud.SelectMany(command);
		}

		///<summary>Returns all appointments for the given patient, ordered from earliest to latest.  Used in statements, appt cards, OtherAppts window, etc.</summary>
		public static Appointment[] GetForPat(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Appointment[]>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * FROM appointment "
				+"WHERE patnum = '"+patNum.ToString()+"' "
				+"ORDER BY AptDateTime";
			return Crud.AppointmentCrud.SelectMany(command).ToArray();
		}

		public static List<Appointment> GetListForPat(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Appointment>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * FROM appointment "
				+"WHERE patnum = '"+patNum.ToString()+"' "
				+"ORDER BY AptDateTime";
			return Crud.AppointmentCrud.SelectMany(command);
		}

		///<summary>Gets one appointment from db.  Returns null if not found.</summary>
		public static Appointment GetOneApt(long aptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Appointment>(MethodBase.GetCurrentMethod(),aptNum);
			}
			if(aptNum==0) {
				return null;
			}
			string command="SELECT * FROM appointment "
				+"WHERE AptNum = '"+POut.Long(aptNum)+"'";
			return Crud.AppointmentCrud.SelectOne(command);
		}

		///<summary></summary>
		public static Appointment GetScheduledPlannedApt(long nextAptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Appointment>(MethodBase.GetCurrentMethod(),nextAptNum);
			}
			if(nextAptNum==0) {
				return null;
			}
			string command="SELECT * FROM appointment "
				+"WHERE NextAptNum = '"+POut.Long(nextAptNum)+"'";
			return Crud.AppointmentCrud.SelectOne(command);
		}

		///<summary>Gets a list of all future appointments which are either sched or ASAP.  Ordered by dateTime</summary>
		public static List<Appointment> GetFutureSchedApts(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Appointment>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM appointment "
				+"WHERE PatNum = "+POut.Long(patNum)+" "
				+"AND AptDateTime > NOW() "
				+"AND (aptstatus = "+(int)ApptStatus.Scheduled+" "
				+"OR aptstatus = "+(int)ApptStatus.ASAP+") "
				+"ORDER BY AptDateTime";
			return Crud.AppointmentCrud.SelectMany(command);
		}

		///<summary>Gets a list of aptNums for one day in the schedule for a given set of providers.</summary>
		public static List<long> GetRouting(DateTime date,List<long> provNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<long>>(MethodBase.GetCurrentMethod(),date,provNums);
			}
			string command=
				"SELECT AptNum FROM appointment "
				+"WHERE AptDateTime LIKE '"+POut.Date(date,false)+"%' "
				+"AND aptstatus != '"+(int)ApptStatus.UnschedList+"' "
				+"AND aptstatus != '"+(int)ApptStatus.Planned+"' "
				+"AND (";
			for(int i=0;i<provNums.Count;i++) {
				if(i>0) {
					command+=" OR";
				}
				command+=" ProvNum="+POut.Long(provNums[i])
					+" OR ProvHyg="+POut.Long(provNums[i]);
			}
			command+=") ORDER BY AptDateTime";
			DataTable table=Db.GetTable(command);
			List<long> retVal=new List<long>();
			for(int i=0;i<table.Rows.Count;i++) {
				retVal.Add(PIn.Long(table.Rows[i][0].ToString()));
			}
			return retVal;
			//return TableToObjects(table).ToArray();
		}

		public static List<Appointment> GetUAppoint(DateTime changedSince,DateTime excludeOlderThan){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Appointment>>(MethodBase.GetCurrentMethod(),changedSince,excludeOlderThan);
			}
			string command="SELECT * FROM appointment WHERE DateTStamp > "+POut.DateT(changedSince)
				+" AND AptDateTime > "+POut.DateT(excludeOlderThan);
			return Crud.AppointmentCrud.SelectMany(command);
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
				command+=POut.Long(appts[i].AptNum);
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

		public static void Insert(Appointment appt) {
			InsertIncludeAptNum(appt,false);
		}

		///<summary>Set includeAptNum to true only in rare situations.  Like when we are inserting for eCW.</summary>
		public static long InsertIncludeAptNum(Appointment appt,bool useExistingPK) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				appt.AptNum=Meth.GetLong(MethodBase.GetCurrentMethod(),appt,useExistingPK);
				return appt.AptNum;
			}
			//make sure all fields are properly filled:
			if(appt.Confirmed==0){
				appt.Confirmed=DefC.GetList(DefCat.ApptConfirmed)[0].DefNum;
			}
			if(appt.ProvNum==0){
				appt.ProvNum=ProviderC.List[0].ProvNum;
			}
			return Crud.AppointmentCrud.Insert(appt,useExistingPK);
		}

		///<summary>Updates only the changed columns and returns the number of rows affected.  Supply an oldApt for comparison.</summary>
		public static void Update(Appointment appointment,Appointment oldAppointment) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),appointment,oldAppointment);
				return;
			}
			Crud.AppointmentCrud.Update(appointment,oldAppointment);
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
		public static DataTable GetConfirmList(DateTime dateFrom,DateTime dateTo,long provNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),dateFrom,dateTo,provNum);
			}
			DataTable table=new DataTable();
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("AddrNote");
			table.Columns.Add("AptNum");
			table.Columns.Add("age");
			table.Columns.Add("AptDateTime",typeof(DateTime));
			table.Columns.Add("aptDateTime");
			table.Columns.Add("confirmed");
			table.Columns.Add("contactMethod");
			table.Columns.Add("email");//could be patient or guarantor email.
			table.Columns.Add("Guarantor");
			table.Columns.Add("medNotes");
			table.Columns.Add("nameF");//or preferred.
			table.Columns.Add("nameFL");
			table.Columns.Add("Note");
			table.Columns.Add("patientName");
			table.Columns.Add("PatNum");
			table.Columns.Add("PreferConfirmMethod");
			table.Columns.Add("ProcDescript");
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT patient.PatNum,"
				+"patient.LName,"
				+"patient.FName,patient.Preferred,patient.LName, "
				+"patient.Guarantor,AptDateTime,patient.Birthdate,patient.HmPhone,"
				+"patient.WkPhone,patient.WirelessPhone,ProcDescript,Confirmed,Note,"
				+"patient.AddrNote,AptNum,patient.MedUrgNote,patient.PreferConfirmMethod,"
				+"guar.Email guarEmail,patient.Email,patient.Premed "
				+"FROM patient,appointment,patient guar "
				+"WHERE patient.PatNum=appointment.PatNum "
				+"AND patient.Guarantor=guar.PatNum "
				+"AND AptDateTime > "+POut.Date(dateFrom)+" "
				+"AND AptDateTime < "+POut.Date(dateTo.AddDays(1))+" "
				+"AND (AptStatus=1 "//scheduled
				+"OR AptStatus=4) ";//ASAP
			if(provNum>0){
				command+="AND (appointment.ProvNum="+POut.Long(provNum)+" OR appointment.ProvHyg="+POut.Long(provNum)+") ";
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
				row["age"]=Patients.DateToAge(PIn.Date(rawtable.Rows[i]["Birthdate"].ToString())).ToString();//we don't care about m/y.
				dateT=PIn.DateT(rawtable.Rows[i]["AptDateTime"].ToString());
				row["AptDateTime"]=dateT;
				row["aptDateTime"]=dateT.ToShortDateString()+"\r\n"+dateT.ToShortTimeString();
				row["confirmed"]=DefC.GetName(DefCat.ApptConfirmed,PIn.Long(rawtable.Rows[i]["Confirmed"].ToString()));
				contmeth=(ContactMethod)PIn.Int(rawtable.Rows[i]["PreferConfirmMethod"].ToString());
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
				if(rawtable.Rows[i]["Email"].ToString()=="" && rawtable.Rows[i]["guarEmail"].ToString()!="") {
					row["email"]=rawtable.Rows[i]["guarEmail"].ToString();
				}
				else {
					row["email"]=rawtable.Rows[i]["Email"].ToString();
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
				pat=new Patient();
				pat.LName=rawtable.Rows[i]["LName"].ToString();
				pat.FName=rawtable.Rows[i]["FName"].ToString();
				pat.Preferred=rawtable.Rows[i]["Preferred"].ToString();
				row["nameF"]=pat.GetNameFirstOrPreferred();
				row["nameFL"]=pat.GetNameFirstOrPrefL();
				row["Note"]=rawtable.Rows[i]["Note"].ToString();
				row["patientName"]=	pat.LName+"\r\n";
				if(pat.Preferred!=""){
					row["patientName"]+="'"+pat.Preferred+"'";
				}
				else{
					row["patientName"]+=pat.FName;
				}
				row["PatNum"]=rawtable.Rows[i]["PatNum"].ToString();
				row["PreferConfirmMethod"]=rawtable.Rows[i]["PreferConfirmMethod"].ToString();
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
		public static DataTable GetAddrTable(List<long> aptNums) {
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
			command+=") ORDER BY appointment.AptDateTime";
			return Db.GetTable(command);
		}

		///<summary>The newStatus will be a DefNum or 0.</summary>
		public static void SetConfirmed(long aptNum,long newStatus) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aptNum,newStatus);
				return;
			}
			string command="UPDATE appointment SET Confirmed="+POut.Long(newStatus);
			if(PrefC.GetLong(PrefName.AppointmentTimeArrivedTrigger)==newStatus){
				command+=",DateTimeArrived=NOW()";
			}
			else if(PrefC.GetLong(PrefName.AppointmentTimeSeatedTrigger)==newStatus){
				command+=",DateTimeSeated=NOW()";
			}
			else if(PrefC.GetLong(PrefName.AppointmentTimeDismissedTrigger)==newStatus){
				command+=",DateTimeDismissed=NOW()";
			}
			command+=" WHERE AptNum="+POut.Long(aptNum);
			Db.NonQ(command);
		}

		///<summary>Sets the new pattern for an appointment.  This is how resizing is done.  Must contain only / and X, with each char representing 5 minutes.</summary>
		public static void SetPattern(long aptNum,string newPattern) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aptNum,newPattern);
				return;
			}
			string command="UPDATE appointment SET Pattern='"+POut.String(newPattern)+"' WHERE AptNum="+POut.Long(aptNum);
			Db.NonQ(command);
		}

		///<summary>Use to send to unscheduled list, to set broken, etc.  Do not use to set complete.</summary>
		public static void SetAptStatus(long aptNum,ApptStatus newStatus) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aptNum,newStatus);
				return;
			}
			string command="UPDATE appointment SET AptStatus="+POut.Long((int)newStatus)
				+" WHERE AptNum="+POut.Long(aptNum);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void SetAptStatusComplete(long aptNum,long planNum1,long planNum2) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aptNum,planNum1,planNum2);
				return;
			}
			string command="UPDATE appointment SET "
				+"AptStatus="+POut.Long((int)ApptStatus.Complete)+", "
				+"InsPlan1="+POut.Long(planNum1)+", "
				+"InsPlan2="+POut.Long(planNum2)+" "
				+"WHERE AptNum="+POut.Long(aptNum);
			Db.NonQ(command);
		}
		
		public static Appointment TableToObject(DataTable table) {
			//No need to check RemotingRole; no call to db.
			if(table.Rows.Count==0) {
				return null;
			}
			return Crud.AppointmentCrud.TableToList(table)[0];
		}

		///<summary></summary>
		public static DataSet RefreshPeriod(DateTime dateStart,DateTime dateEnd) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetDS(MethodBase.GetCurrentMethod(),dateStart,dateEnd);
			} 
			DataSet retVal=new DataSet();
			DataTable tableAppt=GetPeriodApptsTable(dateStart,dateEnd,0,false);
			retVal.Tables.Add(tableAppt);//parameters[0],parameters[1],"0","0"));
			retVal.Tables.Add(GetPeriodEmployeeSchedTable(dateStart,dateEnd));
			retVal.Tables.Add(GetPeriodWaitingRoomTable(dateStart,dateEnd));
			retVal.Tables.Add(GetPeriodSchedule(dateStart,dateEnd));
			retVal.Tables.Add(GetApptFields(tableAppt));
			retVal.Tables.Add(GetPatFields(tableAppt));
			return retVal;
		}

		///<summary></summary>
		public static DataSet RefreshOneApt(long aptNum,bool isPlanned) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetDS(MethodBase.GetCurrentMethod(),aptNum,isPlanned);
			} 
			DataSet retVal=new DataSet();
			retVal.Tables.Add(GetPeriodApptsTable(DateTime.MinValue,DateTime.MinValue,aptNum,isPlanned));
			return retVal;
		}

		///<summary>If aptnum is specified, then the dates are ignored.  If getting data for one planned appt, then pass isPlanned=1.  This changes which procedures are retrieved.</summary>
		public static DataTable GetPeriodApptsTable(DateTime dateStart,DateTime dateEnd,long aptNum,bool isPlanned) {
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
			table.Columns.Add("address");
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
			table.Columns.Add("assistantAbbr");
			table.Columns.Add("billingType");
			table.Columns.Add("chartNumber");
			table.Columns.Add("chartNumAndName");
			table.Columns.Add("confirmed");
			table.Columns.Add("Confirmed");
			table.Columns.Add("contactMethods");
			//table.Columns.Add("creditIns");
			table.Columns.Add("CreditType");
			table.Columns.Add("famFinUrgNote");
			table.Columns.Add("guardians");
			table.Columns.Add("hasIns[I]");
			table.Columns.Add("hmPhone");
			table.Columns.Add("ImageFolder");
			table.Columns.Add("insurance");
			table.Columns.Add("insToSend[!]");
			table.Columns.Add("IsHygiene");
			table.Columns.Add("lab");
			table.Columns.Add("medOrPremed[+]");
			table.Columns.Add("MedUrgNote");
			table.Columns.Add("Note");
			table.Columns.Add("Op");
			table.Columns.Add("patientName");
			table.Columns.Add("patientNameF");
			table.Columns.Add("PatNum");
			table.Columns.Add("patNum");
			table.Columns.Add("GuarNum");
			table.Columns.Add("patNumAndName");
			table.Columns.Add("Pattern");
			table.Columns.Add("preMedFlag");
			table.Columns.Add("procs");
			table.Columns.Add("procsColored");
			table.Columns.Add("production");
			table.Columns.Add("productionVal");
			table.Columns.Add("provider");
			table.Columns.Add("ProvHyg");
			table.Columns.Add("ProvNum");
			table.Columns.Add("timeAskedToArrive");
			table.Columns.Add("wkPhone");
			table.Columns.Add("wirelessPhone");
			string command="SELECT p1.Abbr ProvAbbr,p2.Abbr HygAbbr,patient.Address,patient.Address2,patient.AddrNote,"
				+"patient.ApptModNote,AptDateTime,appointment.AptNum,AptStatus,Assistant,"
				+"patient.BillingType,patient.BirthDate,"
				+"carrier1.CarrierName carrierName1,carrier2.CarrierName carrierName2,"
				+"patient.ChartNumber,patient.City,Confirmed,patient.CreditType,DateTimeChecked,DateTimeDue,DateTimeRecd,DateTimeSent,DateTimeAskedToArrive,"
				+"COUNT(DiseaseNum) hasDisease,"
				+"guar.FamFinUrgNote,patient.FName,patient.Guarantor,patient.HmPhone,patient.ImageFolder,IsHygiene,IsNewPatient,"
				+"LabCaseNum,patient.LName,patient.MedUrgNote,patient.MiddleI,Note,Op,appointment.PatNum,"
				+"Pattern,patplan.PlanNum,patient.PreferConfirmMethod,patient.PreferContactMethod,patient.Preferred,"
				+"patient.PreferRecallMethod,patient.Premed,"
				+"ProcDescript,ProcsColored,"
				+"(SELECT SUM(ProcFee) FROM procedurelog ";
			if(isPlanned){
				command+="WHERE procedurelog.PlannedAptNum=appointment.AptNum AND procedurelog.PlannedAptNum!=0) Production, ";
			}
			else{
				command+="WHERE procedurelog.AptNum=appointment.AptNum AND procedurelog.AptNum!=0) Production, ";
			}
			command+="ProvHyg,appointment.ProvNum,patient.State,patient.WirelessPhone,patient.WkPhone,patient.Zip "
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
				+"LEFT JOIN carrier carrier2 ON plan2.CarrierNum=carrier2.CarrierNum "
				+"LEFT JOIN disease ON patient.PatNum=disease.PatNum ";
			if(aptNum==0){
				command+="WHERE AptDateTime >= "+POut.Date(dateStart)+" "
					+"AND AptDateTime < "+POut.Date(dateEnd.AddDays(1))+" "
					+ "AND AptStatus IN (1, 2, 4, 5, 7, 8) ";
			}
			else{
				command+="WHERE appointment.AptNum="+POut.Long(aptNum);
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
					command+=POut.Long(aptNum);
				}
				command+=")";
				rawProc=dcon.GetTable(command);
			}
			DataTable rawInsProc=null;
			if(PrefC.GetBool(PrefName.ApptExclamationShowForUnsentIns)){
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
					+"AND procedurelog.ProcDate >= "+POut.Date(DateTime.Now.AddYears(-1))+" "//I'm sure this is the slow part.  Should be easy to make faster with less range
					+"AND procedurelog.ProcDate <= "+POut.Date(DateTime.Now)+ " "
					+"GROUP BY patient.Guarantor"; 
				rawInsProc=dcon.GetTable(command);
			}
			//Guardians-----------------------------------------------------------------------
			command="SELECT PatNumChild,PatNumGuardian,Relationship,patient.FName,patient.Preferred "
				+"FROM guardian "
				+"LEFT JOIN patient ON patient.PatNum=guardian.PatNumGuardian "
				+"WHERE PatNumChild IN (";
			if(raw.Rows.Count==0){
				command+="0";
			}
			else for(int i=0;i<raw.Rows.Count;i++) {
				if(i>0) {
					command+=",";
				}
				command+=raw.Rows[i]["PatNum"].ToString();
			}
			command+=") ORDER BY Relationship";
			DataTable rawGuardians=dcon.GetTable(command);
			DateTime aptDate;
			TimeSpan span;
			int hours;
			int minutes;
			DateTime labDate;
			DateTime labDueDate;
			DateTime birthdate;
			DateTime timeAskedToArrive;
			for(int i=0;i<raw.Rows.Count;i++) {
				row=table.NewRow();
				row["address"]=Patients.GetAddressFull(raw.Rows[i]["Address"].ToString(),raw.Rows[i]["Address2"].ToString(),
					raw.Rows[i]["City"].ToString(),raw.Rows[i]["State"].ToString(),raw.Rows[i]["Zip"].ToString());
				row["addrNote"]="";
				if(raw.Rows[i]["AddrNote"].ToString()!=""){
					row["addrNote"]=Lans.g("Appointments","AddrNote: ")+raw.Rows[i]["AddrNote"].ToString();
				}
				aptDate=PIn.DateT(raw.Rows[i]["AptDateTime"].ToString());
				row["AptDateTime"]=aptDate;
				birthdate=PIn.Date(raw.Rows[i]["Birthdate"].ToString());
				row["age"]="";
				if(birthdate.AddYears(18)<DateTime.Today) {
					row["age"]=Lans.g("Appointments","Age: ");//only show if older than 18
				}
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
				row["assistantAbbr"]="";
				if(row["Assistant"].ToString()!="0") {
					row["assistantAbbr"]=Employees.GetAbbr(PIn.Long(row["Assistant"].ToString()));
				}
				row["billingType"]=DefC.GetName(DefCat.BillingTypes,PIn.Long(raw.Rows[i]["BillingType"].ToString()));
				row["chartNumber"]=raw.Rows[i]["ChartNumber"].ToString();
				row["chartNumAndName"]="";
				if(raw.Rows[i]["IsNewPatient"].ToString()=="1") {
					row["chartNumAndName"]="NP-";
				}
				row["chartNumAndName"]+=raw.Rows[i]["ChartNumber"].ToString()+" "
					+PatientLogic.GetNameLF(raw.Rows[i]["LName"].ToString(),raw.Rows[i]["FName"].ToString(),
					raw.Rows[i]["Preferred"].ToString(),raw.Rows[i]["MiddleI"].ToString());
				row["confirmed"]=DefC.GetName(DefCat.ApptConfirmed,PIn.Long(raw.Rows[i]["Confirmed"].ToString()));
				row["Confirmed"]=raw.Rows[i]["Confirmed"].ToString();
				row["contactMethods"]="";
				if(raw.Rows[i]["PreferConfirmMethod"].ToString()!="0"){
					row["contactMethods"]+=Lans.g("Appointments","Confirm Method: ")
						+((ContactMethod)PIn.Long(raw.Rows[i]["PreferConfirmMethod"].ToString())).ToString();
				}
				if(raw.Rows[i]["PreferContactMethod"].ToString()!="0"){
					if(row["contactMethods"].ToString()!="") {
						row["contactMethods"]+="\r\n";
					}
					row["contactMethods"]+=Lans.g("Appointments","Contact Method: ")
						+((ContactMethod)PIn.Long(raw.Rows[i]["PreferContactMethod"].ToString())).ToString();
				}
				if(raw.Rows[i]["PreferRecallMethod"].ToString()!="0"){
					if(row["contactMethods"].ToString()!="") {
						row["contactMethods"]+="\r\n";
					}
					row["contactMethods"]+=Lans.g("Appointments","Recall Method: ")
						+((ContactMethod)PIn.Long(raw.Rows[i]["PreferRecallMethod"].ToString())).ToString();
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
				row["CreditType"]=raw.Rows[i]["CreditType"].ToString();
				row["famFinUrgNote"]="";
				if(raw.Rows[i]["FamFinUrgNote"].ToString()!="") {
					row["famFinUrgNote"]=Lans.g("Appointments","FamFinUrgNote: ")+raw.Rows[i]["FamFinUrgNote"].ToString();
				}
				row["guardians"]="";
				GuardianRelationship guardRelat;
				for(int g=0;g<rawGuardians.Rows.Count;g++) {
					if(raw.Rows[i]["PatNum"].ToString()==rawGuardians.Rows[g]["PatNumChild"].ToString()) {
						if(row["guardians"].ToString()!="") {
							row["guardians"]+=",";
						}
						guardRelat=(GuardianRelationship)PIn.Int(rawGuardians.Rows[g]["Relationship"].ToString());
						row["guardians"]+=Patients.GetNameFirstOrPreferred(rawGuardians.Rows[g]["FName"].ToString(),rawGuardians.Rows[g]["Preferred"].ToString())
							+Guardians.GetGuardianRelationshipStr(guardRelat);
					}
				}
				row["hasIns[I]"]="";
				if(raw.Rows[i]["PlanNum"].ToString()!="" && raw.Rows[i]["PlanNum"].ToString()!="0") {
					row["hasIns[I]"]+="I";
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
				row["insToSend[!]"]="";
				if(InsToSend) {
					row["insToSend[!]"]="!";
				}
				row["IsHygiene"]=raw.Rows[i]["IsHygiene"].ToString();
				row["lab"]="";
				if(raw.Rows[i]["LabCaseNum"].ToString()!=""){
					labDate=PIn.DateT(raw.Rows[i]["DateTimeChecked"].ToString());
					if(labDate.Year>1880) {
						row["lab"]=Lans.g("Appointments","Lab Quality Checked");
					}
					else {
						labDate=PIn.DateT(raw.Rows[i]["DateTimeRecd"].ToString());
						if(labDate.Year>1880) {
							row["lab"]=Lans.g("Appointments","Lab Received");
						}
						else {
							labDate=PIn.DateT(raw.Rows[i]["DateTimeSent"].ToString());
							if(labDate.Year>1880) {
								row["lab"]=Lans.g("Appointments","Lab Sent");//sent but not received
							}
							else {
								row["lab"]=Lans.g("Appointments","Lab Not Sent");
							}
							labDueDate=PIn.DateT(raw.Rows[i]["DateTimeDue"].ToString());
							if(labDueDate.Year>1880) {
								row["lab"]+=", "+Lans.g("Appointments","Due: ")//+dateDue.ToString("ddd")+" "
									+labDueDate.ToShortDateString();//+" "+dateDue.ToShortTimeString();
							}
						}
					}
				}
				row["medOrPremed[+]"]="";
				if(raw.Rows[i]["MedUrgNote"].ToString()!="" || raw.Rows[i]["Premed"].ToString()=="1" || raw.Rows[i]["hasDisease"].ToString()!="0") {
					row["medOrPremed[+]"]="+";
				}
				row["MedUrgNote"]=raw.Rows[i]["MedUrgNote"].ToString();
				row["Note"]=raw.Rows[i]["Note"].ToString();
				row["Op"]=raw.Rows[i]["Op"].ToString();
				if(raw.Rows[i]["IsNewPatient"].ToString()=="1"){
					row["patientName"]="NP-";
				}
				row["patientName"]+=PatientLogic.GetNameLF(raw.Rows[i]["LName"].ToString(),raw.Rows[i]["FName"].ToString(),
					raw.Rows[i]["Preferred"].ToString(),raw.Rows[i]["MiddleI"].ToString());
				row["patientNameF"]=raw.Rows[i]["FName"].ToString();
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
				row["procs"]=raw.Rows[i]["ProcDescript"].ToString();
				row["procsColored"]+=raw.Rows[i]["ProcsColored"].ToString();
				row["production"]=PIn.Double(raw.Rows[i]["Production"].ToString()).ToString("c");
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
				row["timeAskedToArrive"]="";
				timeAskedToArrive=PIn.DateT(raw.Rows[i]["DateTimeAskedToArrive"].ToString());
				if(timeAskedToArrive.Year>1880) {
					row["timeAskedToArrive"]=timeAskedToArrive.ToShortTimeString();
				}
				row["wirelessPhone"]=Lans.g("Appointments","Cell: ")+raw.Rows[i]["WirelessPhone"].ToString();
				row["wkPhone"]=Lans.g("Appointments","Wk: ")+raw.Rows[i]["WkPhone"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		///<summary>Pass in the appointments table so that we can search based on appointments.</summary>
		public static DataTable GetApptFields(DataTable tableAppts) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),tableAppts);
			}
			string command="SELECT AptNum,FieldName,FieldValue "
				+"FROM apptfield "
				+"WHERE AptNum IN (";
			if(tableAppts.Rows.Count==0) {
				command+="0";
			}
			else for(int i=0;i<tableAppts.Rows.Count;i++) {
					if(i>0) {
						command+=",";
					}
					command+=tableAppts.Rows[i]["AptNum"].ToString();
				}
			command+=")";
			DataConnection dcon=new DataConnection();
			DataTable table= dcon.GetTable(command);
			table.TableName="ApptFields";
			return table;
		}

		///<summary>Pass in the appointments table so that we can search based on appointments.</summary>
		public static DataTable GetPatFields(DataTable tableAppts) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),tableAppts);
			}
			string command="SELECT PatNum,FieldName,FieldValue "
				+"FROM patfield "
				+"WHERE PatNum IN (";
			if(tableAppts.Rows.Count==0) {
				command+="0";
			}
			else for(int i=0;i<tableAppts.Rows.Count;i++) {
					if(i>0) {
						command+=",";
					}
					command+=tableAppts.Rows[i]["PatNum"].ToString();
				}
			command+=")";
			DataConnection dcon=new DataConnection();
			DataTable table= dcon.GetTable(command);
			table.TableName="PatFields";
			return table;
		}

		///<summary>Pass in one aptNum</summary>
		public static DataTable GetApptFields(long aptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),aptNum);
			}
			string command="SELECT ApptFieldNum,apptfielddef.FieldName,FieldValue "
				+"FROM apptfielddef "
				+"LEFT JOIN apptfield ON apptfielddef.FieldName=apptfield.FieldName "
				+"AND AptNum = "+POut.Long(aptNum)+" "
				+"ORDER BY apptfielddef.FieldName";
			DataConnection dcon=new DataConnection();
			DataTable table= dcon.GetTable(command);
			table.TableName="ApptFields";
			return table;
		}

		public static DataTable GetPeriodEmployeeSchedTable(DateTime dateStart,DateTime dateEnd) {
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
				+"AND SchedDate = "+POut.Date(dateStart)+" "
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
				startTime=PIn.DateT(raw.Rows[i]["StartTime"].ToString());
				stopTime=PIn.DateT(raw.Rows[i]["StopTime"].ToString());
				row["schedule"]+=startTime.ToString("h:mm")+"-"+stopTime.ToString("h:mm");
				table.Rows.Add(row);
			}
			return table;
		}

		public static DataTable GetPeriodWaitingRoomTable(DateTime dateStart,DateTime dateEnd) {
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
				+"AND DATE(AptDateTime) = "+POut.Date(dateStart)+" "
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
				dateTimeNow=PIn.DateT(raw.Rows[i]["dateTimeNow"].ToString());
				timeArrived=(PIn.DateT(raw.Rows[i]["DateTimeArrived"].ToString())).TimeOfDay;
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

		public static DataTable GetPeriodSchedule(DateTime dateStart,DateTime dateEnd){
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
				+"WHERE SchedDate >= "+POut.Date(dateStart)+" "
				+"AND SchedDate <= "+POut.Date(dateEnd)+" "
				+"GROUP BY schedule.ScheduleNum "
				+"ORDER BY StartTime";
			DataTable raw=Db.GetTable(command);
			//the times come back as times rather than datetimes.  This causes problems.  That's why we're not just returning raw.
			DataRow row;
			for(int i=0;i<raw.Rows.Count;i++){
				row=table.NewRow();
				row["ScheduleNum"]=raw.Rows[i]["ScheduleNum"].ToString();
				row["SchedDate"]=POut.Date(PIn.Date(raw.Rows[i]["SchedDate"].ToString()),false);
				row["StartTime"]=POut.DateT(PIn.DateT(raw.Rows[i]["StartTime"].ToString()),false);
				row["StopTime"]=POut.DateT(PIn.DateT(raw.Rows[i]["StopTime"].ToString()),false);
				row["SchedType"]=raw.Rows[i]["SchedType"].ToString();
				row["ProvNum"]=raw.Rows[i]["ProvNum"].ToString();
				row["BlockoutType"]=raw.Rows[i]["BlockoutType"].ToString();
				row["Note"]=raw.Rows[i]["Note"].ToString();
				row["Status"]=raw.Rows[i]["Status"].ToString();
				row["ops"]=PIn.ByteArray(raw.Rows[i]["_ops"]);
				row["EmployeeNum"]=raw.Rows[i]["EmployeeNum"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		//Get DS for one appointment in Edit window--------------------------------------------------------------------------------
		//-------------------------------------------------------------------------------------------------------------------------

		///<summary></summary>
		public static DataSet GetApptEdit(long aptNum) {
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
			retVal.Tables.Add(GetApptFields(aptNum));
			return retVal;
		}

		public static DataTable GetApptTable(long aptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),aptNum);
			}
			string command="SELECT * FROM appointment WHERE AptNum="+aptNum.ToString();
			DataTable table=Db.GetTable(command);
			table.TableName="Appointment";
			return table;
		}

		public static DataTable GetPatTable(string patNum) {
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
			//Patient birthdate----------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","Birthdate");
			row["value"]=PIn.Date(rawPat.Rows[0]["Birthdate"].ToString()).ToShortDateString();
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
			row["value"]=DefC.GetName(DefCat.BillingTypes,PIn.Long(rawPat.Rows[0]["BillingType"].ToString()));
			table.Rows.Add(row);
			//Patient total balance-----------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lans.g("FormApptEdit","Total Balance");
			double totalBalance=PIn.Double(rawPat.Rows[0]["EstBalance"].ToString());
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
			double balance=PIn.Double(familyBalance.Rows[0]["BalTotal"].ToString())
				-PIn.Double(familyBalance.Rows[0]["InsEst"].ToString());
			row["value"]=balance.ToString("F");
			table.Rows.Add(row);
			//Site----------------------------------------------------------------------------------
			if(!PrefC.GetBool(PrefName.EasyHidePublicHealth)){
				row=table.NewRow();
				row["field"]=Lans.g("FormApptEdit","Site");
				row["value"]=Sites.GetDescription(PIn.Long(rawPat.Rows[0]["SiteNum"].ToString()));
				table.Rows.Add(row);
			}
			return table;
		}

		public static DataTable GetProcTable(string patNum,string aptNum,string apptStatus,string aptDateTime) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),patNum,aptNum,apptStatus,aptDateTime);
			}
			DataTable table=new DataTable("Procedure");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("AbbrDesc");
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
			table.Columns.Add("Surf");
			table.Columns.Add("toothNum");
			table.Columns.Add("ToothNum");
			table.Columns.Add("ToothRange");
			table.Columns.Add("TreatArea");
			//but we won't actually fill this table with rows until the very end.  It's more useful to use a List<> for now.
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT AbbrDesc,procedurecode.ProcCode,AptNum,LaymanTerm,"
				+"PlannedAptNum,Priority,ProcFee,ProcNum,ProcStatus, "
				+"procedurecode.Descript,procedurelog.CodeNum,procedurelog.ProvNum,Surf,ToothNum,ToothRange,TreatArea "
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
				command+="AptNum="+aptNum;//exclude procs attached to other appts.
			}
			//3. All unattached completed procs with same date as appt.
			//but only if one of these types
			if(apptStatus=="1" || apptStatus=="2" || apptStatus=="4" || apptStatus=="5"){//sched,C,ASAP,broken
				DateTime aptDate=PIn.DateT(aptDateTime);
				command+=" OR (AptNum=0 "//unattached
					+"AND ProcStatus=2 "//complete
					+"AND Date(ProcDate)="+POut.Date(aptDate)+")";//same date
			}
			command+=")";
			DataTable rawProc=Db.GetTable(command);
			for(int i=0;i<rawProc.Rows.Count;i++) {
				row=table.NewRow();
				row["AbbrDesc"]=rawProc.Rows[i]["AbbrDesc"].ToString();
				if(apptStatus=="6"){//planned
					row["attached"]=(rawProc.Rows[i]["PlannedAptNum"].ToString()==aptNum) ? "1" : "0";
				}
				else{
					row["attached"]=(rawProc.Rows[i]["AptNum"].ToString()==aptNum) ? "1" : "0";
				}
				row["CodeNum"]=rawProc.Rows[i]["CodeNum"].ToString();
				row["descript"]="";
				if(apptStatus=="6") {//planned
					if(rawProc.Rows[i]["PlannedAptNum"].ToString()!="0" && rawProc.Rows[i]["PlannedAptNum"].ToString()!=aptNum) {
						row["descript"]=Lans.g("FormApptEdit","(other appt)");
					}
				}
				else {
					if(rawProc.Rows[i]["AptNum"].ToString()!="0" && rawProc.Rows[i]["AptNum"].ToString()!=aptNum) {
						row["descript"]=Lans.g("FormApptEdit","(other appt)");
					}
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
				row["fee"]=PIn.Double(rawProc.Rows[i]["ProcFee"].ToString()).ToString("F");
				row["priority"]=DefC.GetName(DefCat.TxPriorities,PIn.Long(rawProc.Rows[i]["Priority"].ToString()));
				row["Priority"]=rawProc.Rows[i]["Priority"].ToString();
				row["ProcCode"]=rawProc.Rows[i]["ProcCode"].ToString();
				row["ProcNum"]=rawProc.Rows[i]["ProcNum"].ToString();
				row["ProcStatus"]=rawProc.Rows[i]["ProcStatus"].ToString();
				row["ProvNum"]=rawProc.Rows[i]["ProvNum"].ToString();
				row["status"]=((ProcStat)PIn.Long(rawProc.Rows[i]["ProcStatus"].ToString())).ToString();
				row["Surf"]=rawProc.Rows[i]["Surf"].ToString();
				row["toothNum"]=Tooth.GetToothLabel(rawProc.Rows[i]["ToothNum"].ToString());
				row["ToothNum"]=rawProc.Rows[i]["ToothNum"].ToString();
				row["ToothRange"]=rawProc.Rows[i]["ToothRange"].ToString();
				row["TreatArea"]=rawProc.Rows[i]["TreatArea"].ToString();
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

		public static DataTable GetCommTable(string patNum) {
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
				row["commDateTime"]=PIn.DateT(rawComm.Rows[i]["commDateTime"].ToString()).ToShortDateString();
				row["CommlogNum"]=rawComm.Rows[i]["CommlogNum"].ToString();
				row["CommType"]=rawComm.Rows[i]["CommType"].ToString();
				row["Note"]=rawComm.Rows[i]["Note"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		public static DataTable GetMiscTable(string aptNum,bool isPlanned) {
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
				date=PIn.DateT(raw.Rows[0]["DateTimeChecked"].ToString());
				if(date.Year>1880){
					row["labDescript"]+=", "+Lans.g("FormApptEdit","Quality Checked");
				}
				else{
					date=PIn.DateT(raw.Rows[0]["DateTimeRecd"].ToString());
					if(date.Year>1880){
						row["labDescript"]+=", "+Lans.g("FormApptEdit","Received");
					}
					else{
						date=PIn.DateT(raw.Rows[0]["DateTimeSent"].ToString());
						if(date.Year>1880){
							row["labDescript"]+=", "+Lans.g("FormApptEdit","Sent");//sent but not received
						}
						else{
							row["labDescript"]+=", "+Lans.g("FormApptEdit","Not Sent");
						}
						dateDue=PIn.DateT(raw.Rows[0]["DateTimeDue"].ToString());
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
		public static void Delete(long aptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aptNum);
				return;
			}
			string command;
			command="SELECT PatNum,IsNewPatient,AptStatus FROM appointment WHERE AptNum="+POut.Long(aptNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count<1){
				return;//Already deleted or did not exist.
			}
			Patient pat=Patients.GetPat(PIn.Long(table.Rows[0]["PatNum"].ToString()));
			if(table.Rows[0]["IsNewPatient"].ToString()=="1") {
				Procedures.SetDateFirstVisit(DateTime.MinValue,3,pat);
			}
			//procs
			if(table.Rows[0]["AptStatus"].ToString()=="6") {//planned
				command="UPDATE procedurelog SET PlannedAptNum =0 WHERE PlannedAptNum = "+POut.Long(aptNum);
			}
			else {
				command="UPDATE procedurelog SET AptNum =0 WHERE AptNum = "+POut.Long(aptNum);
			}
			Db.NonQ(command);
			//labcases
			if(table.Rows[0]["AptStatus"].ToString()=="6") {//planned
				command="UPDATE labcase SET PlannedAptNum =0 WHERE PlannedAptNum = "+POut.Long(aptNum);
			}
			else {
				command="UPDATE labcase SET AptNum =0 WHERE AptNum = "+POut.Long(aptNum);
			}
			Db.NonQ(command);
			//plannedappt
			command="DELETE FROM plannedappt WHERE AptNum="+POut.Long(aptNum);
			Db.NonQ(command);
			//we will not reset item orders here
			command="DELETE FROM appointment WHERE AptNum = "+POut.Long(aptNum);
			Db.NonQ(command);
			//apptfield
			command="DELETE FROM apptfield WHERE AptNum = "+POut.Long(aptNum);
			Db.NonQ(command);
			DeletedObjects.SetDeleted(DeletedObjectType.Appointment,aptNum);
		}

		/// <summary>If make5minute is false, then result will be in 10 or 15 minutes blocks and will need a later conversion step before going to db.</summary>
		public static string CalculatePattern(long provDent,long provHyg,List<long> codeNums,bool make5minute) {
			StringBuilder strBTime=new StringBuilder("");
			string procTime="";
			ProcedureCode procCode;
			if(codeNums.Count==1) {
				procCode=ProcedureCodes.GetProcCode(codeNums[0]);
				if(procCode.IsHygiene) {//hygiene proc
					procTime=ProcCodeNotes.GetTimePattern(provHyg,codeNums[0]);
				}
				else {//dentist proc
					procTime=ProcCodeNotes.GetTimePattern(provDent,codeNums[0]);
				}
				strBTime.Append(procTime);
			}
			else {//multiple procs or no procs
				for(int i=0;i<codeNums.Count;i++) {
					procCode=ProcedureCodes.GetProcCode(codeNums[i]);
					if(procCode.IsHygiene) {//hygiene proc
						procTime=ProcCodeNotes.GetTimePattern(provHyg,codeNums[i]);
					}
					else {//dentist proc
						procTime=ProcCodeNotes.GetTimePattern(provDent,codeNums[i]);
					}
					if(procTime.Length<2) {
						continue;
					}
					for(int n=1;n<procTime.Length-1;n++) {
						if(procTime.Substring(n,1)=="/") {
							strBTime.Append("/");
						}
						else {
							strBTime.Insert(0,"X");
						}
					}
				}
			}
			if(codeNums.Count>1) {//multiple procs
				strBTime.Insert(0,"/");
				strBTime.Append("/");
			}
			else if(codeNums.Count==0) {//0 procs
				strBTime.Append("/");
			}
			if(strBTime.Length>39) {
				strBTime.Remove(39,strBTime.Length-39);
			}
			string pattern=strBTime.ToString();
			if(make5minute) {
				return ConvertPatternTo5(pattern);
			}
			return pattern;
		}

		public static string ConvertPatternTo5(string pattern) {
			StringBuilder savePattern=new StringBuilder();
			for(int i=0;i<pattern.Length;i++) {
				savePattern.Append(pattern.Substring(i,1));
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==10) {
					savePattern.Append(pattern.Substring(i,1));
				}
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==15) {
					savePattern.Append(pattern.Substring(i,1));
					savePattern.Append(pattern.Substring(i,1));
				}
			}
			if(savePattern.Length==0) {
				savePattern=new StringBuilder("/");
			}
			return savePattern.ToString();
		}

	}
}
