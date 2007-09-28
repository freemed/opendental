using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness{
	public class AppointmentB {
		public static Appointment TableToObject(DataTable table) {
			if(table.Rows.Count==0) {
				return null;
			}
			return TableToObjects(table)[0];
		}

		public static Appointment[] TableToObjects(DataTable table) {
			Appointment[] list=new Appointment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				list[i]=new Appointment();
				list[i].AptNum         =PIn.PInt(table.Rows[i][0].ToString());
				list[i].PatNum         =PIn.PInt(table.Rows[i][1].ToString());
				list[i].AptStatus      =(ApptStatus)PIn.PInt(table.Rows[i][2].ToString());
				list[i].Pattern        =PIn.PString(table.Rows[i][3].ToString());
				list[i].Confirmed      =PIn.PInt(table.Rows[i][4].ToString());
				list[i].AddTime        =PIn.PInt(table.Rows[i][5].ToString());
				list[i].Op             =PIn.PInt(table.Rows[i][6].ToString());
				list[i].Note           =PIn.PString(table.Rows[i][7].ToString());
				list[i].ProvNum        =PIn.PInt(table.Rows[i][8].ToString());
				list[i].ProvHyg        =PIn.PInt(table.Rows[i][9].ToString());
				list[i].AptDateTime    =PIn.PDateT(table.Rows[i][10].ToString());
				list[i].NextAptNum     =PIn.PInt(table.Rows[i][11].ToString());
				list[i].UnschedStatus  =PIn.PInt(table.Rows[i][12].ToString());
				//list[i].Lab            =PIn.PInt(table.Rows[i][13].ToString());
				list[i].IsNewPatient   =PIn.PBool(table.Rows[i][14].ToString());
				list[i].ProcDescript   =PIn.PString(table.Rows[i][15].ToString());
				list[i].Assistant      =PIn.PInt(table.Rows[i][16].ToString());
				list[i].InstructorNum  =PIn.PInt(table.Rows[i][17].ToString());
				list[i].SchoolClassNum =PIn.PInt(table.Rows[i][18].ToString());
				list[i].SchoolCourseNum=PIn.PInt(table.Rows[i][19].ToString());
				list[i].GradePoint     =PIn.PFloat(table.Rows[i][20].ToString());
				list[i].ClinicNum      =PIn.PInt(table.Rows[i][21].ToString());
				list[i].IsHygiene      =PIn.PBool(table.Rows[i][22].ToString());
			}
			return list;
		}

		///<summary>Parameters: 1:dateStart, 2:dateEnd</summary>
		public static DataSet RefreshPeriod(string[] parameters) {
			DataSet retVal=new DataSet();
			retVal.Tables.Add(GetPeriodApptsTable(parameters[0],parameters[1],"0","0"));
			retVal.Tables.Add(GetPeriodEmployeeSchedTable(parameters[0],parameters[1]));
			return retVal;
		}

		///<summary>Parameters: 1:AptNum 2:IsPlanned</summary>
		public static DataSet RefreshOneApt(string[] parameters) {
			DataSet retVal=new DataSet();
			retVal.Tables.Add(GetPeriodApptsTable("","",parameters[0],parameters[1]));
			return retVal;
		}

		///<summary>If aptnum is specified, then the dates are ignored.  If getting data for one planned appt, then pass isPlanned=true.  This changes which procedures are retrieved.</summary>
		private static DataTable GetPeriodApptsTable(string strDateStart,string strDateEnd,string strAptNum,string strIsPlanned) {
			DateTime dateStart=PIn.PDate(strDateStart);
			DateTime dateEnd=PIn.PDate(strDateEnd);
			int aptNum=PIn.PInt(strAptNum);
			bool isPlanned=PIn.PBool(strIsPlanned);
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
				+"patient.BillingType,patient.BirthDate,patient.Guarantor,"
				+"patient.ChartNumber,Confirmed,patient.CreditType,DateTimeChecked,DateTimeRecd,DateTimeSent,"
				+"guar.FamFinUrgNote,patient.FName,patient.HmPhone,patient.ImageFolder,IsHygiene,IsNewPatient,"
				+"LabCaseNum,patient.LName,patient.MedUrgNote,patient.MiddleI,Note,Op,appointment.PatNum,"
				+"Pattern,patplan.PlanNum,patient.PreferConfirmMethod,patient.PreferContactMethod,patient.Preferred,"
				+"patient.PreferRecallMethod,patient.Premed,"
				+"(SELECT SUM(ProcFee) FROM procedurelog ";
			if(isPlanned){
				command+="WHERE procedurelog.PlannedAptNum=appointment.AptNum) Production, ";
			}
			else{
				command+="WHERE procedurelog.AptNum=appointment.AptNum) Production, ";
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
				+"LEFT JOIN patplan ON patplan.PatNum=patient.PatNum ";
			if(aptNum==0){
				command+="WHERE AptDateTime >= "+POut.PDate(dateStart)+" "
					+"AND AptDateTime < "+POut.PDate(dateEnd.AddDays(1))+" "
					+ "AND (AptStatus=1 OR AptStatus=2 OR AptStatus=4 OR AptStatus=5 OR AptStatus=7 OR AptStatus=8) ";
			}
			else{
				command+="WHERE appointment.AptNum="+POut.PInt(aptNum);
			}
			command+=" GROUP BY appointment.AptNum";
			DataTable raw=dcon.GetTable(command);
			command="SELECT AbbrDesc,procedurelog.AptNum,procedurelog.CodeNum,PlannedAptNum,Surf,ToothNum,TreatArea "
				+"FROM procedurelog,appointment,procedurecode ";
			if(isPlanned){
				command+="WHERE procedurelog.PlannedAptNum=appointment.AptNum ";
			}
			else{
				command+="WHERE procedurelog.AptNum=appointment.AptNum ";
			}
			command+="AND procedurelog.CodeNum=procedurecode.CodeNum ";
			if(aptNum==0) {
				command+="AND AptDateTime >= "+POut.PDate(dateStart)+" "
					+"AND AptDateTime < "+POut.PDate(dateEnd.AddDays(1))+" ";
			}
			else {
				command+="AND appointment.AptNum="+POut.PInt(aptNum);
			}
			DataTable rawProc=dcon.GetTable(command);
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
			DataTable rawInsProc=dcon.GetTable(command);
			DateTime aptDate;
			TimeSpan span;
			int hours;
			int minutes;
			DateTime labDate;
			DateTime birthdate;
			for(int i=0;i<raw.Rows.Count;i++) {
				row=table.NewRow();
				if(raw.Rows[i]["AddrNote"].ToString()!=""){
					row["addrNote"]=Lan.g("Appointments","AddrNote: ")+raw.Rows[i]["AddrNote"].ToString();
				}
				aptDate=PIn.PDateT(raw.Rows[i]["AptDateTime"].ToString());
				row["AptDateTime"]=aptDate;
				birthdate=PIn.PDate(raw.Rows[i]["Birthdate"].ToString());
				row["age"]=Lan.g("Appointments","Age: ");
				if(birthdate.Year>1880){
					row["age"]+=PatientB.DateToAgeString(birthdate);
				}
				else{
					row["age"]+="?";
				}
				if(raw.Rows[i]["ApptModNote"].ToString()!="") {
					row["apptModNote"]=Lan.g("Appointments","ApptModNote: ")+raw.Rows[i]["ApptModNote"].ToString();
				}
				row["aptDate"]=aptDate.ToShortDateString();
				row["aptDay"]=aptDate.ToString("dddd");
				span=TimeSpan.FromMinutes(raw.Rows[i]["Pattern"].ToString().Length*5);
				hours=span.Hours;
				minutes=span.Minutes;
				if(hours==0){
					row["aptLength"]=minutes.ToString()+Lan.g("Appointments"," Min");
				}
				else if(hours==1){
					row["aptLength"]=hours.ToString()+Lan.g("Appointments"," Hr, ")
						+minutes.ToString()+Lan.g("Appointments"," Min");
				}
				else{
					row["aptLength"]=hours.ToString()+Lan.g("Appointments"," Hrs, ")
						+minutes.ToString()+Lan.g("Appointments"," Min");
				}
				row["aptTime"]=aptDate.ToShortTimeString();
				row["AptNum"]=raw.Rows[i]["AptNum"].ToString();
				row["AptStatus"]=raw.Rows[i]["AptStatus"].ToString();
				row["Assistant"]=raw.Rows[i]["Assistant"].ToString();
				row["billingType"]=DefB.GetName(DefCat.BillingTypes,PIn.PInt(raw.Rows[i]["BillingType"].ToString()));
				if(raw.Rows[i]["ChartNumber"].ToString()!=""){
					row["chartNumber"]=raw.Rows[i]["ChartNumber"].ToString();
				}
				//row["ChartNumber"]=raw.Rows[i]["ChartNumber"].ToString();
				row["chartNumAndName"]="";
				if(raw.Rows[i]["IsNewPatient"].ToString()=="1") {
					row["chartNumAndName"]="NP-";
				}
				row["chartNumAndName"]+=raw.Rows[i]["ChartNumber"].ToString()+" "
					+PatientB.GetNameLF(raw.Rows[i]["LName"].ToString(),raw.Rows[i]["FName"].ToString(),
					raw.Rows[i]["Preferred"].ToString(),raw.Rows[i]["MiddleI"].ToString());
				row["confirmed"]=DefB.GetName(DefCat.ApptConfirmed,PIn.PInt(raw.Rows[i]["Confirmed"].ToString()));
				row["Confirmed"]=raw.Rows[i]["Confirmed"].ToString();
				row["contactMethods"]="";
				if(raw.Rows[i]["PreferConfirmMethod"].ToString()!="0"){
					row["contactMethods"]+=Lan.g("Appointments","Confirm Method: ")
						+((ContactMethod)PIn.PInt(raw.Rows[i]["PreferConfirmMethod"].ToString())).ToString();
				}
				if(raw.Rows[i]["PreferContactMethod"].ToString()!="0"){
					if(row["contactMethods"].ToString()!="") {
						row["contactMethods"]+="\r\n";
					}
					row["contactMethods"]+=Lan.g("Appointments","Contact Method: ")
						+((ContactMethod)PIn.PInt(raw.Rows[i]["PreferContactMethod"].ToString())).ToString();
				}
				if(raw.Rows[i]["PreferRecallMethod"].ToString()!="0"){
					if(row["contactMethods"].ToString()!="") {
						row["contactMethods"]+="\r\n";
					}
					row["contactMethods"]+=Lan.g("Appointments","Recall Method: ")
						+((ContactMethod)PIn.PInt(raw.Rows[i]["PreferRecallMethod"].ToString())).ToString();
				}
				row["creditIns"]=raw.Rows[i]["CreditType"].ToString();
				//figure out if pt's family has ins claims that need to be created
				bool InsToSend=false;
				for(int j=0;j<rawInsProc.Rows.Count;j++){
					if(raw.Rows[i]["PlanNum"].ToString()!="" && raw.Rows[i]["PlanNum"].ToString()!="0") {
						if (raw.Rows[i]["Guarantor"].ToString()==rawInsProc.Rows[j]["Guarantor"].ToString() 
							|| raw.Rows[i]["Guarantor"].ToString()==rawInsProc.Rows[j]["PatNum"].ToString())
						{
							InsToSend=true;
						}
					}
				}
				if (InsToSend){
					row["creditIns"]+="!";
				}
				else if(raw.Rows[i]["PlanNum"].ToString()!="" && raw.Rows[i]["PlanNum"].ToString()!="0"){
					row["creditIns"]+="I";
				}
				if(raw.Rows[i]["FamFinUrgNote"].ToString()!="") {
					row["famFinUrgNote"]=Lan.g("Appointments","FamFinUrgNote: ")+raw.Rows[i]["FamFinUrgNote"].ToString();
				}
				row["hmPhone"]=Lan.g("Appointments","Hm: ")+raw.Rows[i]["HmPhone"].ToString();
				row["ImageFolder"]=raw.Rows[i]["ImageFolder"].ToString();
				if(raw.Rows[i]["PlanNum"].ToString()!="" && raw.Rows[i]["PlanNum"].ToString()!="0"){
					row["insurance"]=Lan.g("Appointments","Insured");
				}
				row["IsHygiene"]=raw.Rows[i]["IsHygiene"].ToString();
				row["lab"]="";
				if(raw.Rows[i]["LabCaseNum"].ToString()!=""){
					labDate=PIn.PDateT(raw.Rows[i]["DateTimeChecked"].ToString());
					if(labDate.Year>1880) {
						row["lab"]=Lan.g("Appointments","Lab Quality Checked");
					}
					else {
						labDate=PIn.PDateT(raw.Rows[i]["DateTimeRecd"].ToString());
						if(labDate.Year>1880) {
							row["lab"]=Lan.g("Appointments","Lab Received");
						}
						else {
							labDate=PIn.PDateT(raw.Rows[i]["DateTimeSent"].ToString());
							if(labDate.Year>1880) {
								row["lab"]=Lan.g("Appointments","Lab Sent");//sent but not received
							}
							else {
								row["lab"]=Lan.g("Appointments","Lab Not Sent");
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
				row["patientName"]+=PatientB.GetNameLF(raw.Rows[i]["LName"].ToString(),raw.Rows[i]["FName"].ToString(),
					raw.Rows[i]["Preferred"].ToString(),raw.Rows[i]["MiddleI"].ToString());
				row["PatNum"]=raw.Rows[i]["PatNum"].ToString();
				row["patNum"]="PatNum: "+raw.Rows[i]["PatNum"].ToString();
				row["GuarNum"]=raw.Rows[i]["Guarantor"].ToString();
				row["patNumAndName"]="";
				if(raw.Rows[i]["IsNewPatient"].ToString()=="1") {
					row["patNumAndName"]="NP-";
				}
				row["patNumAndName"]+=raw.Rows[i]["PatNum"].ToString()+" "
					+PatientB.GetNameLF(raw.Rows[i]["LName"].ToString(),raw.Rows[i]["FName"].ToString(),
					raw.Rows[i]["Preferred"].ToString(),raw.Rows[i]["MiddleI"].ToString());
				row["Pattern"]=raw.Rows[i]["Pattern"].ToString();
				if(raw.Rows[i]["Premed"].ToString()=="1"){
					row["preMedFlag"]=Lan.g("Appointments","Premedicate");
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
							row["procs"]+="#"+Tooth.ToInternat(rawProc.Rows[p]["ToothNum"].ToString())+"-"
								+rawProc.Rows[p]["Surf"].ToString()+"-";//""#12-MOD-"
							break;
						case "2"://TreatmentArea.Tooth:
							row["procs"]+="#"+Tooth.ToInternat(rawProc.Rows[p]["ToothNum"].ToString())+"-";//"#12-"
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
				row["wirelessPhone"]=Lan.g("Appointments","Cell: ")+raw.Rows[i]["WirelessPhone"].ToString();
				row["wkPhone"]=Lan.g("Appointments","Wk: ")+raw.Rows[i]["WkPhone"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		private static DataTable GetPeriodEmployeeSchedTable(string strDateStart,string strDateEnd) {
			DateTime dateStart=PIn.PDate(strDateStart);
			DateTime dateEnd=PIn.PDate(strDateEnd);
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

		//Get DS for one appointment in Edit window--------------------------------------------------------------------------------
		//-------------------------------------------------------------------------------------------------------------------------

		///<summary>Parameters: 1:AptNum</summary>
		public static DataSet GetApptEdit(string[] parameters){
			DataSet retVal=new DataSet();
			retVal.Tables.Add(GetApptTable(parameters[0]));
			retVal.Tables.Add(GetPatTable(retVal.Tables["Appointment"].Rows[0]["PatNum"].ToString()));
			retVal.Tables.Add(GetProcTable(retVal.Tables["Appointment"].Rows[0]["PatNum"].ToString(),parameters[0],
				retVal.Tables["Appointment"].Rows[0]["AptStatus"].ToString()));
			retVal.Tables.Add(GetCommTable(retVal.Tables["Appointment"].Rows[0]["PatNum"].ToString()));
			retVal.Tables.Add(GetMiscTable(parameters[0],retVal.Tables["Appointment"].Rows[0]["AptStatus"].ToString()));
			return retVal;
		}

		private static DataTable GetApptTable(string aptNum){
			DataConnection dcon=new DataConnection();
			string command="SELECT * FROM appointment WHERE AptNum="+aptNum;
			DataTable table=dcon.GetTable(command);
			table.TableName="Appointment";
			return table;
		}

		private static DataTable GetPatTable(string patNum) {
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("Patient");
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("field");
			table.Columns.Add("value");
			string command="SELECT * FROM patient WHERE PatNum="+patNum;
			DataTable rawPat=dcon.GetTable(command);
			DataRow row;
			//Patient Name--------------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lan.g("FormApptEdit","Name");
			row["value"]=PatientB.GetNameLF(rawPat.Rows[0]["LName"].ToString(),rawPat.Rows[0]["FName"].ToString(),
				rawPat.Rows[0]["Preferred"].ToString(),rawPat.Rows[0]["MiddleI"].ToString());
			table.Rows.Add(row);
			//Patient First Name--------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lan.g("FormApptEdit","First Name");
			row["value"]=rawPat.Rows[0]["FName"];
			table.Rows.Add(row);
			//Patient Last name---------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lan.g("FormApptEdit","Last Name");
			row["value"]=rawPat.Rows[0]["LName"];
			table.Rows.Add(row);
			//Patient middle initial----------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lan.g("FormApptEdit","Middle Initial");
			row["value"]=rawPat.Rows[0]["MiddleI"];
			table.Rows.Add(row);
			//Patient home phone--------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lan.g("FormApptEdit","Home Phone");
			row["value"]=rawPat.Rows[0]["HmPhone"];
			table.Rows.Add(row);
			//Patient work phone--------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lan.g("FormApptEdit","Work Phone");
			row["value"]=rawPat.Rows[0]["WkPhone"];
			table.Rows.Add(row);
			//Patient wireless phone----------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lan.g("FormApptEdit","Wireless Phone");
			row["value"]=rawPat.Rows[0]["WirelessPhone"];
			table.Rows.Add(row);
			//Patient credit type-------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lan.g("FormApptEdit","Credit Type");
			row["value"]=rawPat.Rows[0]["CreditType"];
			table.Rows.Add(row);
			//Patient billing type------------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lan.g("FormApptEdit","Billing Type");
			row["value"]=DefB.GetName(DefCat.BillingTypes,PIn.PInt(rawPat.Rows[0]["BillingType"].ToString()));
			table.Rows.Add(row);
			//Patient total balance-----------------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lan.g("FormApptEdit","Total Balance");
			double totalBalance=PIn.PDouble(rawPat.Rows[0]["EstBalance"].ToString());
			row["value"]=totalBalance.ToString("F");
			table.Rows.Add(row);
			//Patient address and phone notes-------------------------------------------------------
			row=table.NewRow();
			row["field"]=Lan.g("FormApptEdit","Address and Phone Notes");
			row["value"]=rawPat.Rows[0]["AddrNote"];
			table.Rows.Add(row);
			//Patient family balance----------------------------------------------------------------
			command="SELECT BalTotal,InsEst FROM patient WHERE Guarantor='"
				+rawPat.Rows[0]["Guarantor"].ToString()+"'";
			DataTable familyBalance=dcon.GetTable(command);
			row=table.NewRow();
			row["field"]=Lan.g("FormApptEdit","Family Balance");
			double balance=PIn.PDouble(familyBalance.Rows[0]["BalTotal"].ToString())
				-PIn.PDouble(familyBalance.Rows[0]["InsEst"].ToString());
			row["value"]=balance.ToString("F");
			table.Rows.Add(row);
			return table;
		}

		private static DataTable GetProcTable(string patNum,string aptNum,string apptStatus) {
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("Procedure");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("attached");//0 or 1
			table.Columns.Add("CodeNum");
			table.Columns.Add("descript");
			table.Columns.Add("fee");
			table.Columns.Add("priority");
			table.Columns.Add("ProcCode");
			table.Columns.Add("ProcNum");
			table.Columns.Add("ProcStatus");
			table.Columns.Add("ProvNum");
			table.Columns.Add("status");
			table.Columns.Add("toothNum");
			table.Columns.Add("Surf");
			string command="SELECT procedurecode.ProcCode,AptNum,PlannedAptNum,Priority,ProcFee,ProcNum,ProcStatus,Surf,ToothNum, "
				+"procedurecode.Descript,procedurelog.CodeNum,procedurelog.ProvNum "
				+"FROM procedurelog LEFT JOIN procedurecode ON procedurelog.CodeNum=procedurecode.CodeNum "
				+"WHERE PatNum="+patNum//sort later
				+" AND (ProcStatus=1 OR ";//tp
			if(apptStatus=="6"){//planned
				command+="PlannedAptNum="+aptNum+")";
			}
			else{
				command+="AptNum="+aptNum+") ";
					//+"AND (AptNum=0 OR AptNum="+aptNum+")";//exclude procs attached to other appts.
			}
			DataTable rawProc=dcon.GetTable(command);
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
					row["descript"]=Lan.g("FormApptEdit","(other appt)");
				}
				row["descript"]+=rawProc.Rows[i]["Descript"].ToString();
				row["fee"]=PIn.PDouble(rawProc.Rows[i]["ProcFee"].ToString()).ToString("F");
				row["priority"]=DefB.GetName(DefCat.TxPriorities,PIn.PInt(rawProc.Rows[i]["Priority"].ToString()));
				row["ProcCode"]=rawProc.Rows[i]["ProcCode"].ToString();
				row["ProcNum"]=rawProc.Rows[i]["ProcNum"].ToString();
				row["ProcStatus"]=rawProc.Rows[i]["ProcStatus"].ToString();
				row["ProvNum"]=rawProc.Rows[i]["ProvNum"].ToString();
				row["status"]=((ProcStat)PIn.PInt(rawProc.Rows[i]["ProcStatus"].ToString())).ToString();
				row["toothNum"]=Tooth.ToInternat(rawProc.Rows[i]["ToothNum"].ToString());
				row["Surf"]=rawProc.Rows[i]["Surf"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		private static DataTable GetCommTable(string patNum) {
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("Comm");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("commDateTime");
			table.Columns.Add("CommlogNum");
			table.Columns.Add("CommType");
			table.Columns.Add("Note");
			string command="SELECT * FROM commlog WHERE PatNum="+patNum+" AND IsStatementSent=0 "//don't include StatementSent
				+"ORDER BY CommDateTime";
			DataTable rawComm=dcon.GetTable(command);
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

		private static DataTable GetMiscTable(string aptNum,string apptStatus) {
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("Misc");
			DataRow row;
			table.Columns.Add("LabCaseNum");
			table.Columns.Add("labDescript");
			table.Columns.Add("requirements");
			string command="SELECT LabCaseNum,DateTimeDue,DateTimeChecked,DateTimeRecd,DateTimeSent,"
				+"laboratory.Description FROM labcase,laboratory "
				+"WHERE labcase.LaboratoryNum=laboratory.LaboratoryNum AND ";
			if(apptStatus=="6") {//planned
				command+="labcase.PlannedAptNum="+aptNum;
			}
			else {
				command+="labcase.AptNum="+aptNum;
			}
			DataTable raw=dcon.GetTable(command);
			DateTime date;
			//always return one row:
			row=table.NewRow();
			row["LabCaseNum"]="0";
			row["labDescript"]="";
			if(raw.Rows.Count>0){
				row["LabCaseNum"]=raw.Rows[0]["LabCaseNum"].ToString();
				row["labDescript"]=raw.Rows[0]["Description"].ToString();
				//DateTime date=PIn.PDateT(raw.Rows[0]["DateTimeDue"].ToString());
				//if(date.Year>1880) {
				//	row["labDescript"]+="\r\n"+Lan.g("FormAppEdit","Due: ")+date.ToString("ddd")+" "
				//	+date.ToShortDateString()+" "+date.ToShortTimeString();
				//}
				date=PIn.PDateT(raw.Rows[0]["DateTimeChecked"].ToString());
				if(date.Year>1880){
					row["labDescript"]+="\r\n"+Lan.g("FormApptEdit","Quality Checked");
				}
				else{
					date=PIn.PDateT(raw.Rows[0]["DateTimeRecd"].ToString());
					if(date.Year>1880){
						row["labDescript"]+="\r\n"+Lan.g("FormApptEdit","Received");
					}
					else{
						date=PIn.PDateT(raw.Rows[0]["DateTimeSent"].ToString());
						if(date.Year>1880){
							row["labDescript"]+="\r\n"+Lan.g("FormApptEdit","Sent");//sent but not received
						}
						else{
							row["labDescript"]+="\r\n"+Lan.g("FormApptEdit","Not Sent");
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
			raw=dcon.GetTable(command);
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

	}
}
