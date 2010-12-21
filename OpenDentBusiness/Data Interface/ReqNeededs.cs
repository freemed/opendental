using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
///<summary></summary>
	public class ReqNeededs{

		public static DataTable Refresh(long schoolClass,long schoolCourse) {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM reqneeded WHERE SchoolClassNum="+POut.Long(schoolClass)
				+" AND SchoolCourseNum="+POut.Long(schoolCourse)
				+" ORDER BY Descript";
			return Db.GetTable(command);
		}

		public static ReqNeeded GetReq(long reqNeededNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ReqNeeded>(MethodBase.GetCurrentMethod(),reqNeededNum);
			}
			string command="SELECT * FROM reqneeded WHERE ReqNeededNum="+POut.Long(reqNeededNum);
 			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return null;
			}
			ReqNeeded req=new ReqNeeded();
			//for(int i=0;i<table.Rows.Count;i++){
			req.ReqNeededNum   = PIn.Long   (table.Rows[0][0].ToString());
			req.Descript       = PIn.String(table.Rows[0][1].ToString());
			req.SchoolCourseNum= PIn.Long   (table.Rows[0][2].ToString());
			req.SchoolClassNum = PIn.Long   (table.Rows[0][3].ToString());
			return req;
		}

		///<summary></summary>
		public static void Update(ReqNeeded req) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),req);
				return;
			}
			Crud.ReqNeededCrud.Update(req);
		}

		///<summary></summary>
		public static long Insert(ReqNeeded req) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				req.ReqNeededNum=Meth.GetLong(MethodBase.GetCurrentMethod(),req);
				return req.ReqNeededNum;
			}
			return Crud.ReqNeededCrud.Insert(req);
		}

		///<summary>Surround with try/catch.</summary>
		public static void Delete(long reqNeededNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),reqNeededNum);
				return;
			}
			//still need to validate
			string command= "DELETE FROM reqneeded "
				+"WHERE ReqNeededNum = "+POut.Long(reqNeededNum);
			Db.NonQ(command);
		}

		/*
		///<summary></summary>
		public static void Synch(int schoolClassNum,int schoolCourseNum){
			//get list of all reqneededs for the given class and course.
			DataTable table=Refresh(schoolClassNum,schoolCourseNum);
			string command;
			int reqNeededNum;
			DataTable tStudent;
			string descript;
			ReqStudent req;
			//1. Delete any reqstudents that do not have gradepoint
			command="DELETE FROM reqstudent "
				+"WHERE NOT EXISTS(SELECT * FROM reqneeded WHERE reqstudent.ReqNeededNum=reqneeded.ReqNeededNum) "
				+"AND reqstudent.DateCompleted < "+POut.PDate(new DateTime(1880,1,1));
			Db.NonQ(command);
			for(int i=0;i<table.Rows.Count;i++){
				reqNeededNum=PIn.PInt(table.Rows[i]["ReqNeededNum"].ToString());
				descript=PIn.PString(table.Rows[i]["Descript"].ToString());
				//2. Update.  Update the description for all students using this requirement.
				command="UPDATE reqstudent SET Descript='"+POut.PString(descript)+"' "
					+"WHERE ReqNeededNum="+POut.PInt(reqNeededNum);
				Db.NonQ(command);
				//3. Insert.  Get list of students that do not have this req.  For each student, insert.
				command="SELECT ProvNum FROM provider WHERE SchoolClassNum="+POut.PInt(schoolClassNum)
					+" AND NOT EXISTS(SELECT * FROM reqstudent WHERE reqstudent.ProvNum=provider.ProvNum"
					+" AND ReqNeededNum="+POut.PInt(reqNeededNum)+")";
				tStudent=Db.GetTable(command);
				for(int s=0;s<tStudent.Rows.Count;s++){
					req=new ReqStudent();
					req.Descript=descript;
					req.ProvNum=PIn.PInt(tStudent.Rows[s]["ProvNum"].ToString());
					req.ReqNeededNum=reqNeededNum;
					req.SchoolCourseNum=PIn.PInt(table.Rows[i]["SchoolCourseNum"].ToString());
					ReqStudents.Insert(req);
				}
			}
		}*/


		




	}

	

	


}













