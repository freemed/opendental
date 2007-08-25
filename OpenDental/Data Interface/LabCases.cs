using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class LabCases {

		///<summary>Gets a filtered list of all labcases.</summary>
		public static DataTable Refresh(DateTime aptStartDate,DateTime aptEndDate) {
			DataTable table=new DataTable();
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("aptDateTime");
			table.Columns.Add("lab");
			table.Columns.Add("LabCaseNum");
			table.Columns.Add("patient");
			table.Columns.Add("phone");
			table.Columns.Add("ProcDescript");
			table.Columns.Add("status");
			string command="SELECT AptDateTime,DateTimeChecked,DateTimeRecd,DateTimeSent,"
				+"LabCaseNum,laboratory.Description,LName,FName,Preferred,MiddleI,Phone,ProcDescript "
				+"FROM labcase,appointment,patient,laboratory "
				+"WHERE labcase.AptNum=appointment.AptNum "
				+"AND labcase.PatNum=patient.PatNum "
				+"AND labcase.LaboratoryNum=laboratory.LaboratoryNum "
				+"AND AptDateTime > "+POut.PDate(aptStartDate)+" "
				+"AND AptDateTime < "+POut.PDate(aptEndDate.AddDays(1))+" "
				+"ORDER BY AptDateTime";
			DataTable raw=General.GetTable(command);
			DateTime AptDateTime;
			DateTime date;
			for(int i=0;i<raw.Rows.Count;i++) {
				row=table.NewRow();
		    AptDateTime=PIn.PDateT(raw.Rows[i]["AptDateTime"].ToString());
				row["aptDateTime"]=AptDateTime.ToShortDateString()+" "+AptDateTime.ToShortTimeString();
				row["lab"]=raw.Rows[i]["Description"].ToString();
				row["LabCaseNum"]=raw.Rows[i]["LabCaseNum"].ToString();
				row["patient"]=PatientB.GetNameLF(raw.Rows[i]["LName"].ToString(),raw.Rows[i]["FName"].ToString(),
					raw.Rows[i]["Preferred"].ToString(),raw.Rows[i]["MiddleI"].ToString());
				row["phone"]=raw.Rows[i]["Phone"].ToString();
				row["ProcDescript"]=raw.Rows[i]["ProcDescript"].ToString();
				date=PIn.PDateT(raw.Rows[i]["DateTimeChecked"].ToString());
				if(date.Year>1880) {
					row["status"]=Lan.g("FormLabCases","Quality Checked");
				}
				else {
					date=PIn.PDateT(raw.Rows[i]["DateTimeRecd"].ToString());
					if(date.Year>1880) {
						row["status"]=Lan.g("FormLabCases","Received");
					}
					else {
						date=PIn.PDateT(raw.Rows[i]["DateTimeSent"].ToString());
						if(date.Year>1880) {
							row["status"]=Lan.g("FormLabCases","Sent");//sent but not received
						}
						else {
							row["status"]=Lan.g("FormLabCases","Not Sent");
						}
					}
				}
				table.Rows.Add(row);
			}
			return table;
		}

		///<summary>Used when drawing the appointments for a day.</summary>
		public static List<LabCase> GetForPeriod(DateTime startDate,DateTime endDate) {
			string command="SELECT labcase.* FROM labcase,appointment "
				+"WHERE labcase.AptNum=appointment.AptNum "
				+"AND (appointment.AptStatus=1 OR appointment.AptStatus=2 OR appointment.AptStatus=4) "//scheduled,complete,or ASAP
				+"AND AptDateTime >= "+POut.PDate(startDate)
				+" AND AptDateTime < "+POut.PDate(endDate.AddDays(1));//midnight of the next morning.
			return FillFromCommand(command);
		}

		///<summary>Used when drawing the planned appointment.</summary>
		public static LabCase GetForPlanned(int aptNum) {
			string command="SELECT labcase.* FROM labcase,appointment "
				+"WHERE labcase.PlannedAptNum="+POut.PInt(aptNum);
			List<LabCase> list=FillFromCommand(command);
			if(list.Count==0){
				return null;
			}
			return list[0];
		}

		///<summary>Gets one labcase from database.</summary>
		public static LabCase GetOne(int labCaseNum){
			string command="SELECT * FROM labcase WHERE LabCaseNum="+POut.PInt(labCaseNum);
			return FillFromCommand(command)[0];
		}

		///<summary>Gets all labcases for a patient which have not been attached to an appointment.  Usually one or none.  Only used when attaching a labcase from within an appointment.</summary>
		public static List<LabCase> GetForPat(int patNum,bool isPlanned) {
			string command="SELECT * FROM labcase WHERE PatNum="+POut.PInt(patNum)+" AND ";
			if(isPlanned){
				command+="PlannedAptNum=0 AND AptNum=0";//We only show lab cases that have not been attached to any kind of appt.
			}
			else{
				command+="AptNum=0";
			}
			return FillFromCommand(command);
		}

		public static List<LabCase> FillFromCommand(string command){
			DataTable table=General.GetTable(command);
			LabCase lab;
			List<LabCase> retVal=new List<LabCase>();
			for(int i=0;i<table.Rows.Count;i++) {
				lab=new LabCase();
				lab.LabCaseNum     = PIn.PInt   (table.Rows[i][0].ToString());
				lab.PatNum         = PIn.PInt   (table.Rows[i][1].ToString());
				lab.LaboratoryNum  = PIn.PInt   (table.Rows[i][2].ToString());
				lab.AptNum         = PIn.PInt   (table.Rows[i][3].ToString());
				lab.PlannedAptNum  = PIn.PInt   (table.Rows[i][4].ToString());
				lab.DateTimeDue    = PIn.PDateT (table.Rows[i][5].ToString());
				lab.DateTimeCreated= PIn.PDateT (table.Rows[i][6].ToString());
				lab.DateTimeSent   = PIn.PDateT (table.Rows[i][7].ToString());
				lab.DateTimeRecd   = PIn.PDateT (table.Rows[i][8].ToString());
				lab.DateTimeChecked= PIn.PDateT (table.Rows[i][9].ToString());
				lab.ProvNum        = PIn.PInt   (table.Rows[i][10].ToString());
				lab.Instructions   = PIn.PString(table.Rows[i][11].ToString());
				retVal.Add(lab);
			}
			return retVal;
		}

		///<summary></summary>
		public static void Insert(LabCase lab){
			if(PrefB.RandomKeys) {
				lab.LabCaseNum=MiscData.GetKey("labcase","LabCaseNum");
			}
			string command="INSERT INTO labcase (";
			if(PrefB.RandomKeys) {
				command+="LabCaseNum,";
			}
			command+="PatNum,LaboratoryNum,AptNum,PlannedAptNum,DateTimeDue,DateTimeCreated,"
				+"DateTimeSent,DateTimeRecd,DateTimeChecked,ProvNum,Instructions) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(lab.LabCaseNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (lab.PatNum)+"', "
				+"'"+POut.PInt   (lab.LaboratoryNum)+"', "
				+"'"+POut.PInt   (lab.AptNum)+"', "
				+"'"+POut.PInt   (lab.PlannedAptNum)+"', "
				    +POut.PDateT (lab.DateTimeDue)+", "
				    +POut.PDateT (lab.DateTimeCreated)+", "
				    +POut.PDateT (lab.DateTimeSent)+", "
				    +POut.PDateT (lab.DateTimeRecd)+", "
				    +POut.PDateT (lab.DateTimeChecked)+", "
				+"'"+POut.PInt   (lab.ProvNum)+"', "
				+"'"+POut.PString(lab.Instructions)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				lab.LabCaseNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(LabCase lab){
			string command= "UPDATE labcase SET " 
				+ "PatNum = '"          +POut.PInt   (lab.PatNum)+"'"
				+ ",LaboratoryNum = '"  +POut.PInt   (lab.LaboratoryNum)+"'"
				+ ",AptNum = '"         +POut.PInt   (lab.AptNum)+"'"
				+ ",PlannedAptNum = '"  +POut.PInt   (lab.PlannedAptNum)+"'"
				+ ",DateTimeDue = "     +POut.PDateT (lab.DateTimeDue)
				+ ",DateTimeCreated = " +POut.PDateT (lab.DateTimeCreated)
				+ ",DateTimeSent = "    +POut.PDateT (lab.DateTimeSent)
				+ ",DateTimeRecd = "    +POut.PDateT (lab.DateTimeRecd)
				+ ",DateTimeChecked = " +POut.PDateT (lab.DateTimeChecked)
				+ ",ProvNum = '"        +POut.PInt   (lab.ProvNum)+"'"
				+ ",Instructions = '"   +POut.PString(lab.Instructions)+"'"
				+" WHERE LabCaseNum = '"+POut.PInt(lab.LabCaseNum)+"'";
 			General.NonQ(command);
		}


		///<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		public static void Delete(int labCaseNum){
			string command;
			/*
			//check patients for dependencies
			string command="SELECT LName,FName FROM patient WHERE LabCaseNum ="
				+POut.PInt(LabCase.LabCaseNum);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lan.g("LabCases","Cannot delete LabCase because ")+pats);
			}*/
			//delete
			command= "DELETE FROM labcase WHERE LabCaseNum = "+POut.PInt(labCaseNum);
 			General.NonQ(command);
		}

		///<summary>Attaches a labcase to an appointment.</summary>
		public static void AttachToAppt(int labCaseNum,int aptNum){
			string command="UPDATE labcase SET AptNum="+POut.PInt(aptNum)+" WHERE LabCaseNum="+POut.PInt(labCaseNum);
			General.NonQ(command);
		}

		///<summary>Attaches a labcase to a planned appointment.</summary>
		public static void AttachToPlannedAppt(int labCaseNum,int plannedAptNum) {
			string command="UPDATE labcase SET PlannedAptNum="+POut.PInt(plannedAptNum)+" WHERE LabCaseNum="+POut.PInt(labCaseNum);
			General.NonQ(command);
		}

		///<summary>Frequently returns null.</summary>
		public static LabCase GetOneFromList(List<LabCase> labCaseList,int aptNum){
			for(int i=0;i<labCaseList.Count;i++){
				if(labCaseList[i].AptNum==aptNum){
					return labCaseList[i];
				}
			}
			return null;
		}

	}
	


}













