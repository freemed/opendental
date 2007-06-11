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
			table.Columns.Add("done");
			table.Columns.Add("patient");
			table.Columns.Add("ReqStudentNum");
			table.Columns.Add("requirement");
			string command="SELECT AptDateTime,CourseID,reqStudent.Descript ReqDescript,"
				+"schoolcourse.Descript CourseDescript,reqstudent.DateCompleted, "
				+"patient.LName,patient.FName,patient.MiddleI,patient.Preferred,ProcDescript,reqstudent.ReqStudentNum "
				+"FROM reqstudent "
				+"LEFT JOIN schoolcourse ON reqstudent.SchoolCourseNum=schoolcourse.SchoolCourseNum "
				+"LEFT JOIN patient ON reqstudent.PatNum=patient.PatNum "
				+"LEFT JOIN appointment ON reqstudent.AptNum=appointment.AptNum "
				+"WHERE reqstudent.ProvNum="+POut.PInt(provNum)
				+" ORDER BY CourseID,ReqDescript";
			DataTable raw=General.GetTable(command);
			DateTime AptDateTime;
			DateTime dateCompleted;
			for(int i=0;i<raw.Rows.Count;i++) {
				row=table.NewRow();
				AptDateTime=PIn.PDateT(raw.Rows[i]["AptDateTime"].ToString());
				if(AptDateTime.Year>1880){
					row["appointment"]=AptDateTime.ToShortDateString()+" "+AptDateTime.ToShortTimeString()
						+" "+raw.Rows[i]["ProcDescript"].ToString();
				}
				row["course"]=raw.Rows[i]["CourseID"].ToString();//+" "+raw.Rows[i]["CourseDescript"].ToString();
				dateCompleted=PIn.PDate(raw.Rows[i]["DateCompleted"].ToString());
				if(dateCompleted.Year>1880){
					row["done"]="X";
				}
				row["patient"]=PatientB.GetNameLF(raw.Rows[i]["LName"].ToString(),raw.Rows[i]["FName"].ToString(),
					raw.Rows[i]["Preferred"].ToString(),raw.Rows[i]["MiddleI"].ToString());
				row["ReqStudentNum"]=raw.Rows[i]["ReqStudentNum"].ToString();
				row["requirement"]=raw.Rows[i]["ReqDescript"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		public static DataTable RefreshManyStudents(int classNum,int courseNum) {
			DataTable table=new DataTable();
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("appointment");
			table.Columns.Add("course");
			table.Columns.Add("done");
			table.Columns.Add("patient");
			table.Columns.Add("ReqStudentNum");
			table.Columns.Add("requirement");
			string command="SELECT AptDateTime,CourseID,reqStudent.Descript ReqDescript,"
				+"schoolcourse.Descript CourseDescript,reqstudent.DateCompleted, "
				+"patient.LName,patient.FName,patient.MiddleI,patient.Preferred,ProcDescript,reqstudent.ReqStudentNum "
				+"FROM reqstudent "
				+"LEFT JOIN schoolcourse ON reqstudent.SchoolCourseNum=schoolcourse.SchoolCourseNum "
				+"LEFT JOIN patient ON reqstudent.PatNum=patient.PatNum "
				+"LEFT JOIN appointment ON reqstudent.AptNum=appointment.AptNum "
				//+"WHERE reqstudent.ProvNum="+POut.PInt(provNum)
				+" ORDER BY CourseID,ReqDescript";
			DataTable raw=General.GetTable(command);
			DateTime AptDateTime;
			DateTime dateCompleted;
			for(int i=0;i<raw.Rows.Count;i++) {
				row=table.NewRow();
				AptDateTime=PIn.PDateT(raw.Rows[i]["AptDateTime"].ToString());
				if(AptDateTime.Year>1880) {
					row["appointment"]=AptDateTime.ToShortDateString()+" "+AptDateTime.ToShortTimeString()
						+" "+raw.Rows[i]["ProcDescript"].ToString();
				}
				row["course"]=raw.Rows[i]["CourseID"].ToString();//+" "+raw.Rows[i]["CourseDescript"].ToString();
				dateCompleted=PIn.PDate(raw.Rows[i]["DateCompleted"].ToString());
				if(dateCompleted.Year>1880) {
					row["done"]="X";
				}
				row["patient"]=PatientB.GetNameLF(raw.Rows[i]["LName"].ToString(),raw.Rows[i]["FName"].ToString(),
					raw.Rows[i]["Preferred"].ToString(),raw.Rows[i]["MiddleI"].ToString());
				row["ReqStudentNum"]=raw.Rows[i]["ReqStudentNum"].ToString();
				row["requirement"]=raw.Rows[i]["ReqDescript"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		///<summary>Before reqneeded.Delete, this checks to make sure that req is not in use by students.  Used to prompt user.</summary>
		public static string InUseBy(int reqNeededNum){
			string command="SELECT LName,FName FROM provider,reqstudent "
				+"WHERE provider.ProvNum=reqstudent.ProvNum "
				+"AND reqstudent.ReqNeededNum="+POut.PInt(reqNeededNum)
				+" AND reqstudent.DateCompleted > "+POut.PDate(new DateTime(1880,1,1));
			DataTable table=General.GetTable(command);
			string retVal="";
			for(int i=0;i<table.Rows.Count;i++){
				retVal+=table.Rows[i]["LName"].ToString()+", "+table.Rows[i]["FName"].ToString()+"\r\n";
			}
			return retVal;
		}

		public static ReqStudent GetOne(int ReqStudentNum){
			string command="SELECT * FROM reqstudent WHERE ReqStudentNum="+POut.PInt(ReqStudentNum);
 			DataTable table=General.GetTable(command);
			ReqStudent req=new ReqStudent();
			req.ReqStudentNum  = PIn.PInt   (table.Rows[0][0].ToString());
			req.ReqNeededNum   = PIn.PInt   (table.Rows[0][1].ToString());
			req.Descript       = PIn.PString(table.Rows[0][2].ToString());
			req.SchoolCourseNum= PIn.PInt   (table.Rows[0][3].ToString());
			req.ProvNum        = PIn.PInt   (table.Rows[0][4].ToString());
			req.AptNum         = PIn.PInt   (table.Rows[0][5].ToString());
			req.PatNum         = PIn.PInt   (table.Rows[0][6].ToString());
			req.InstructorNum  = PIn.PInt   (table.Rows[0][7].ToString());
			req.DateCompleted  = PIn.PDate  (table.Rows[0][8].ToString());
			return req;
		}

		///<summary></summary>
		public static void Update(ReqStudent req) {
			string command = "UPDATE reqstudent SET "
				+ " ReqNeededNum = '"   +POut.PInt   (req.ReqNeededNum)+"'"
				+ ",Descript = '"       +POut.PString(req.Descript)+"'"
				+ ",SchoolCourseNum = '"+POut.PInt   (req.SchoolCourseNum)+"'"
				+ ",ProvNum = '"        +POut.PInt   (req.ProvNum)+"'"
				+ ",AptNum = '"         +POut.PInt   (req.AptNum)+"'"   
				+ ",PatNum = '"         +POut.PInt   (req.PatNum)+"'"   
				+ ",InstructorNum = '"  +POut.PInt   (req.InstructorNum)+"'"   
				+ ",DateCompleted = "   +POut.PDate  (req.DateCompleted)      
				+" WHERE ReqStudentNum = '" +POut.PInt(req.ReqStudentNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(ReqStudent req) {
			if(PrefB.RandomKeys) {
				req.ReqStudentNum=MiscData.GetKey("reqstudent","ReqStudentNum");
			}
			string command= "INSERT INTO reqstudent (";
			if(PrefB.RandomKeys) {
				command+="ReqStudentNum,";
			}
			command+="ReqNeededNum,Descript,SchoolCourseNum,ProvNum,AptNum,PatNum,InstructorNum,DateCompleted) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(req.ReqStudentNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (req.ReqNeededNum)+"', "
				+"'"+POut.PString(req.Descript)+"', "
				+"'"+POut.PInt   (req.SchoolCourseNum)+"', "
				+"'"+POut.PInt   (req.ProvNum)+"', "
				+"'"+POut.PInt   (req.AptNum)+"', "
				+"'"+POut.PInt   (req.PatNum)+"', "
				+"'"+POut.PInt   (req.InstructorNum)+"', "
				    +POut.PDate  (req.DateCompleted)+")";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				req.ReqStudentNum=General.NonQ(command,true);
			}
		}

		///<summary>Surround with try/catch.</summary>
		public static void Delete(int reqStudentNum) {
			ReqStudent req=GetOne(reqStudentNum);
			//if a reqneeded exists, then disallow deletion.
			if(ReqNeededs.GetReq(req.ReqNeededNum)==null){
				throw new Exception(Lan.g("ReqStudents","Cannot delete requirement.  Delete the requirement needed instead."));
			}
			string command= "DELETE FROM reqstudent WHERE ReqStudentNum = "+POut.PInt(reqStudentNum);
			General.NonQ(command);
		}

		///<summary>Attaches a req to an appointment.  Importantly, it also sets the patNum to match the apt.</summary>
		public static void AttachToApt(int reqStudentNum,int aptNum) {
			string command="SELECT PatNum FROM appointment WHERE AptNum="+POut.PInt(aptNum);
			string patNum=General.GetCount(command);
			command="UPDATE reqstudent SET AptNum="+POut.PInt(aptNum)
				+", PatNum="+patNum
				+" WHERE ReqStudentNum="+POut.PInt(reqStudentNum);
			General.NonQ(command);
		}




	}

	

	


}













