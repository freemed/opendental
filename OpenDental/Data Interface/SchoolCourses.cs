using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class SchoolCourses {
		///<summary></summary>
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
		public static void Delete(SchoolCourse sc){
			//todo: check for dependencies

			string command= "DELETE from schoolcourse WHERE SchoolCourseNum = '"
				+POut.PInt(sc.SchoolCourseNum)+"'";
 			General.NonQ(command);
		}


	
	

	
	}

	

	


}




















