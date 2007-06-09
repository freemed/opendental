using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class ReqStudents{

		public static DataTable RefreshOneStudent(int provNum){
			DataTable table=new DataTable();
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("appointment");
			table.Columns.Add("course");
			table.Columns.Add("grade");
			table.Columns.Add("patient");
			table.Columns.Add("requirement");
			string command="SELECT AptDateTime,CourseID,reqStudent.Descript ReqDescript,"
				+"schoolcourse.Descript CourseDescript,reqstudent.GradePoint, "
				+"patient.LName,patient.FName,patient.MiddleI,patient.Preferred,ProcDescript "
				+"FROM reqstudent "
				+"LEFT JOIN schoolcourse ON reqstudent.SchoolCourseNum=schoolcourse.SchoolCourseNum "
				+"LEFT JOIN patient ON reqstudent.PatNum=patient.PatNum "
				+"LEFT JOIN appointment ON reqstudent.AptNum=appointment.AptNum "
				+"WHERE reqstudent.ProvNum="+POut.PInt(provNum)
				+" ORDER BY CourseID,ReqDescript";
			DataTable raw=General.GetTable(command);
			DateTime AptDateTime;
			//DateTime date;
			for(int i=0;i<raw.Rows.Count;i++) {
				row=table.NewRow();
				AptDateTime=PIn.PDateT(raw.Rows[i]["AptDateTime"].ToString());
				row["appointment"]=AptDateTime.ToShortDateString()+" "+AptDateTime.ToShortTimeString()
					+" "+raw.Rows[i]["ProcDescript"].ToString();
				row["course"]=raw.Rows[i]["CourseID"].ToString()+" "+raw.Rows[i]["CourseDescript"].ToString();
				//for now, this is pass/fail
				if(raw.Rows[i]["GradePoint"].ToString()=="0"){
					row["grade"]="";
				}
				else{
					row["grade"]="X";
				}
				row["patient"]=PatientB.GetNameLF(raw.Rows[i]["LName"].ToString(),raw.Rows[i]["FName"].ToString(),
					raw.Rows[i]["Preferred"].ToString(),raw.Rows[i]["MiddleI"].ToString());
				row["requirement"]=raw.Rows[i]["ReqDescript"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		///<summary>Before reqneeded.Delete, this checks to make sure that req is not in use by students.  Used to prompt user.</summary>
		public static string InUseBy(int reqNeededNum){
			string command="SELECT LName,FName FROM provider,reqstudent "
				+"WHERE provider.ProvNum=reqstudent.ProvNum "
				+"AND reqstudent.ReqNeededNum="+POut.PInt(reqNeededNum);
			DataTable table=General.GetTable(command);
			string retVal="";
			for(int i=0;i<table.Rows.Count;i++){
				retVal+=table.Rows[i]["LName"].ToString()+", "+table.Rows[i]["FName"].ToString()+"\r\n";
			}
			return retVal;
		}

		/*public static ReqStudent GetReq(int ReqStudentNum){
			string command="SELECT * FROM ReqStudent WHERE ReqStudentNum="+POut.PInt(ReqStudentNum);
 			DataTable table=General.GetTable(command);
			ReqStudent req=new ReqStudent();
			//for(int i=0;i<table.Rows.Count;i++){
			req.ReqStudentNum   = PIn.PInt   (table.Rows[0][0].ToString());
			req.Descript       = PIn.PString(table.Rows[0][1].ToString());
			req.SchoolCourseNum= PIn.PInt   (table.Rows[0][2].ToString());
			req.SchoolClassNum = PIn.PInt   (table.Rows[0][3].ToString());
			return req;
		}

		///<summary></summary>
		public static void Update(ReqStudent req) {
			string command = "UPDATE ReqStudent SET " 
				+ "Descript = '"        +POut.PString(req.Descript)+"'"
				+ ",SchoolCourseNum = '"+POut.PInt   (req.SchoolCourseNum)+"'"
				+ ",SchoolClassNum = '" +POut.PInt   (req.SchoolClassNum)+"'"   
				+" WHERE ReqStudentNum = '" +POut.PInt(req.ReqStudentNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(ReqStudent req) {
			if(PrefB.RandomKeys) {
				req.ReqStudentNum=MiscData.GetKey("ReqStudent","ReqStudentNum");
			}
			string command= "INSERT INTO ReqStudent (";
			if(PrefB.RandomKeys) {
				command+="ReqStudentNum,";
			}
			command+="Descript,SchoolCourseNum,SchoolClassNum) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(req.ReqStudentNum)+"', ";
			}
			command+=
				 "'"+POut.PString(req.Descript)+"', "
				+"'"+POut.PInt   (req.SchoolCourseNum)+"', "
				+"'"+POut.PInt   (req.SchoolClassNum)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				req.ReqStudentNum=General.NonQ(command,true);
			}
		}

		///<summary>Surround with try/catch.</summary>
		public static void Delete(int ReqStudentNum) {
			//still need to validate
			string command= "DELETE FROM ReqStudent "
				+"WHERE ReqStudentNum = "+POut.PInt(ReqStudentNum);
			General.NonQ(command);
		}
		*/

		




	}

	

	


}













