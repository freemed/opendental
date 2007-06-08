using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class ReqStudents{

		/*public static DataTable Refresh(int schoolClass,int schoolCourse){
			string command="SELECT * FROM ReqStudent WHERE SchoolClassNum="+POut.PInt(schoolClass)
				+" AND SchoolCourseNum="+POut.PInt(schoolCourse)
				+" ORDER BY Descript";
			return General.GetTable(command);
		}

		public static ReqStudent GetReq(int ReqStudentNum){
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













