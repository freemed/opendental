using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class SchoolCourses {
		///<summary>A list of all schoolcourses, organized by course ID.</summary>
		public static SchoolCourse[] List;

		///<summary>Refreshes all SchoolCourses.</summary>
		public static void Refresh() {
			string command=
				"SELECT * FROM schoolcourse "
				+"ORDER BY CourseID";
			DataTable table=General.GetTable(command);
			List=new SchoolCourse[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new SchoolCourse();
				List[i].SchoolCourseNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].CourseID       = PIn.PString(table.Rows[i][1].ToString());
				List[i].Descript       = PIn.PString(table.Rows[i][2].ToString());
			}
		}

		///<summary></summary>
		private static void Update(SchoolCourse sc){
			string command= "UPDATE schoolcourse SET " 
				+"SchoolCourseNum = '" +POut.PInt   (sc.SchoolCourseNum)+"'"
				+",CourseID = '"       +POut.PString(sc.CourseID)+"'"
				+",Descript = '"       +POut.PString(sc.Descript)+"'"
				+" WHERE SchoolCourseNum = '"+POut.PInt(sc.SchoolCourseNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(SchoolCourse sc){
			if(PrefB.RandomKeys){
				sc.SchoolCourseNum=MiscData.GetKey("schoolcourse","SchoolCourseNum");
			}
			string command= "INSERT INTO schoolcourse (";
			if(PrefB.RandomKeys){
				command+="SchoolCourseNum,";
			}
			command+="CourseID,Descript) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(sc.SchoolCourseNum)+"', ";
			}
			command+=
				 "'"+POut.PString(sc.CourseID)+"', "
				+"'"+POut.PString(sc.Descript)+"')";
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				sc.SchoolCourseNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void InsertOrUpdate(SchoolCourse sc, bool isNew){
			//if(IsRepeating && DateTask.Year>1880){
			//	throw new Exception(Lan.g(this,"Task cannot be tagged repeating and also have a date."));
			//}
			if(isNew){
				Insert(sc);
			}
			else{
				Update(sc);
			}
		}

		///<summary></summary>
		public static void Delete(int courseNum){
			//check for attached reqneededs---------------------------------------------------------------------
			string command="SELECT COUNT(*) FROM reqneeded WHERE SchoolCourseNum = '"
				+POut.PInt(courseNum)+"'";
			DataTable table=General.GetTable(command);
			if(PIn.PString(table.Rows[0][0].ToString())!="0") {
				throw new Exception(Lan.g("SchoolCourses","Course already in use by 'requirements needed' table."));
			}
			//check for attached reqstudents--------------------------------------------------------------------------
			command="SELECT COUNT(*) FROM reqstudent WHERE SchoolCourseNum = '"
				+POut.PInt(courseNum)+"'";
			table=General.GetTable(command);
			if(PIn.PString(table.Rows[0][0].ToString())!="0") {
				throw new Exception(Lan.g("SchoolCourses","Course already in use by 'student requirements' table."));
			}
			//delete---------------------------------------------------------------------------------------------
			command= "DELETE from schoolcourse WHERE SchoolCourseNum = '"
				+POut.PInt(courseNum)+"'";
 			General.NonQ(command);
		}

		///<summary>Description is CourseID Descript.</summary>
		public static string GetDescript(int schoolCourseNum) {
			for(int i=0;i<List.Length;i++) {
				if(List[i].SchoolCourseNum==schoolCourseNum) {
					return GetDescript(List[i]);
				}
			}
			return "";
		}

		public static string GetDescript(SchoolCourse course){
			return course.CourseID+" "+course.Descript;
		}

		public static string GetCourseID(int schoolCourseNum) {
			for(int i=0;i<List.Length;i++) {
				if(List[i].SchoolCourseNum==schoolCourseNum) {
					return List[i].CourseID;
				}
			}
			return "";
		}

	
	}

	

	


}




















