using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SchoolCourses {
		///<summary>A list of all schoolcourses, organized by course ID.</summary>
		private static SchoolCourse[] list;

		public static SchoolCourse[] List {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=
				"SELECT * FROM schoolcourse "
				+"ORDER BY CourseID";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="SchoolCourse";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=new SchoolCourse[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				list[i]=new SchoolCourse();
				list[i].SchoolCourseNum=PIn.Long(table.Rows[i][0].ToString());
				list[i].CourseID=PIn.String(table.Rows[i][1].ToString());
				list[i].Descript=PIn.String(table.Rows[i][2].ToString());
			}
		}

		///<summary></summary>
		private static void Update(SchoolCourse sc){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sc);
				return;
			}
			Crud.SchoolCourseCrud.Update(sc);
		}

		///<summary></summary>
		private static long Insert(SchoolCourse sc) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				sc.SchoolCourseNum=Meth.GetLong(MethodBase.GetCurrentMethod(),sc);
				return sc.SchoolCourseNum;
			}
			return Crud.SchoolCourseCrud.Insert(sc);
		}

		///<summary></summary>
		public static void InsertOrUpdate(SchoolCourse sc, bool isNew){
			//No need to check RemotingRole; no call to db.
			//if(IsRepeating && DateTask.Year>1880){
			//	throw new Exception(Lans.g(this,"Task cannot be tagged repeating and also have a date."));
			//}
			if(isNew){
				Insert(sc);
			}
			else{
				Update(sc);
			}
		}

		///<summary></summary>
		public static void Delete(long courseNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),courseNum);
				return;
			}
			//check for attached reqneededs---------------------------------------------------------------------
			string command="SELECT COUNT(*) FROM reqneeded WHERE SchoolCourseNum = '"
				+POut.Long(courseNum)+"'";
			DataTable table=Db.GetTable(command);
			if(PIn.String(table.Rows[0][0].ToString())!="0") {
				throw new Exception(Lans.g("SchoolCourses","Course already in use by 'requirements needed' table."));
			}
			//check for attached reqstudents--------------------------------------------------------------------------
			command="SELECT COUNT(*) FROM reqstudent WHERE SchoolCourseNum = '"
				+POut.Long(courseNum)+"'";
			table=Db.GetTable(command);
			if(PIn.String(table.Rows[0][0].ToString())!="0") {
				throw new Exception(Lans.g("SchoolCourses","Course already in use by 'student requirements' table."));
			}
			//delete---------------------------------------------------------------------------------------------
			command= "DELETE from schoolcourse WHERE SchoolCourseNum = '"
				+POut.Long(courseNum)+"'";
 			Db.NonQ(command);
		}

		///<summary>Description is CourseID Descript.</summary>
		public static string GetDescript(long schoolCourseNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++) {
				if(List[i].SchoolCourseNum==schoolCourseNum) {
					return GetDescript(List[i]);
				}
			}
			return "";
		}

		public static string GetDescript(SchoolCourse course){
			//No need to check RemotingRole; no call to db.
			return course.CourseID+" "+course.Descript;
		}

		public static string GetCourseID(long schoolCourseNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++) {
				if(List[i].SchoolCourseNum==schoolCourseNum) {
					return List[i].CourseID;
				}
			}
			return "";
		}

	
	}

	

	


}




















