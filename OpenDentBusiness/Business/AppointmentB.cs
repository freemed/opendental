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
				list[i].Lab            =(LabCaseOld)PIn.PInt(table.Rows[i][13].ToString());
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

		///<Summary>Parameters: 1:AptNum</Summary>
		public static DataSet GetApptEdit(string[] parameters){
			DataSet retVal=new DataSet();
			retVal.Tables.Add(GetApptTable(parameters[0]));
			retVal.Tables.Add(GetPatTable(retVal.Tables["Appointment"].Rows[0]["PatNum"].ToString()));
			retVal.Tables.Add(GetProcTable(retVal.Tables["Appointment"].Rows[0]["PatNum"].ToString(),parameters[0],
				retVal.Tables["Appointment"].Rows[0]["AptStatus"].ToString()));
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
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("FName");
			table.Columns.Add("LName");
			table.Columns.Add("MiddleI");
			table.Columns.Add("nameLF");
			table.Columns.Add("Preferred");
			string command="SELECT * FROM patient WHERE PatNum="+patNum;
			DataTable rawPat=dcon.GetTable(command);
			for(int i=0;i<rawPat.Rows.Count;i++) {//there's just one row
				row=table.NewRow();
				row["FName"]=rawPat.Rows[i]["FName"].ToString();
				row["LName"]=rawPat.Rows[i]["LName"].ToString();
				row["MiddleI"]=rawPat.Rows[i]["MiddleI"].ToString();
				row["nameLF"]=PatientB.GetNameLF(rawPat.Rows[i]["LName"].ToString(),rawPat.Rows[i]["FName"].ToString(),
									rawPat.Rows[i]["Preferred"].ToString(),rawPat.Rows[i]["MiddleI"].ToString());
				row["Preferred"]=rawPat.Rows[i]["Preferred"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		private static DataTable GetProcTable(string patNum,string aptNum,string apptStatus) {
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("Procedure");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("attached");//0 or 1
			table.Columns.Add("descript");
			table.Columns.Add("fee");
			table.Columns.Add("priority");
			table.Columns.Add("ProcNum");
			table.Columns.Add("status");
			table.Columns.Add("toothNum");
			table.Columns.Add("Surf");
			string command="SELECT procedurelog.ADACode,AptNum,PlannedAptNum,Priority,ProcFee,ProcNum,ProcStatus,Surf,ToothNum, "
				+"procedurecode.Descript "
				+"FROM procedurelog LEFT JOIN procedurecode ON procedurelog.ADACode=procedurecode.ADACode "
				+"WHERE PatNum="+patNum//sort later
				+" AND (ProcStatus=1 OR ";//tp
			if(apptStatus=="6"){//planned
				command+="PlannedAptNum="+aptNum+")";
			}
			else{
				command+="AptNum="+aptNum+")";
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
				row["descript"]=rawProc.Rows[i]["Descript"].ToString();
				row["fee"]=PIn.PDouble(rawProc.Rows[i]["ProcFee"].ToString()).ToString("F");
				row["priority"]=DefB.GetName(DefCat.TxPriorities,PIn.PInt(rawProc.Rows[i]["Priority"].ToString()));
				row["ProcNum"]=rawProc.Rows[i]["ProcNum"].ToString();
				row["status"]=((ProcStat)PIn.PInt(rawProc.Rows[i]["ProcStatus"].ToString())).ToString();
				row["toothNum"]=Tooth.ToInternat(rawProc.Rows[i]["ToothNum"].ToString());
				row["Surf"]=rawProc.Rows[i]["Surf"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

	}
}
