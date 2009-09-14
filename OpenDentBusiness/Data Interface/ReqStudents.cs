using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
///<summary></summary>
	public class ReqStudents{

		public static List<ReqStudent> GetForAppt(long aptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ReqStudent>>(MethodBase.GetCurrentMethod(),aptNum);
			}
			string command="SELECT * FROM reqstudent WHERE AptNum="+POut.PLong(aptNum)+" ORDER BY ProvNum,Descript";
			return RefreshAndFill(Db.GetTable(command));
		}

		public static ReqStudent GetOne(long ReqStudentNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ReqStudent>(MethodBase.GetCurrentMethod(),ReqStudentNum);
			}
			string command="SELECT * FROM reqstudent WHERE ReqStudentNum="+POut.PLong(ReqStudentNum);
			List<ReqStudent> reqList=RefreshAndFill(Db.GetTable(command));
			if(reqList.Count==0) {
				return null;
			}
			return reqList[0];
		}

		private static List<ReqStudent> RefreshAndFill(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List<ReqStudent> reqList=new List<ReqStudent>();
			ReqStudent req;
			for(int i=0;i<table.Rows.Count;i++) {
				req=new ReqStudent();
				req.ReqStudentNum  = PIn.PLong(table.Rows[i][0].ToString());
				req.ReqNeededNum   = PIn.PLong(table.Rows[i][1].ToString());
				req.Descript       = PIn.PString(table.Rows[i][2].ToString());
				req.SchoolCourseNum= PIn.PLong(table.Rows[i][3].ToString());
				req.ProvNum        = PIn.PLong(table.Rows[i][4].ToString());
				req.AptNum         = PIn.PLong(table.Rows[i][5].ToString());
				req.PatNum         = PIn.PLong(table.Rows[i][6].ToString());
				req.InstructorNum  = PIn.PLong(table.Rows[i][7].ToString());
				req.DateCompleted  = PIn.PDate(table.Rows[i][8].ToString());
				reqList.Add(req);
			}
			return reqList;
		}

		///<summary></summary>
		public static void Update(ReqStudent req) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),req);
				return;
			}
			string command = "UPDATE reqstudent SET "
				+ " ReqNeededNum = '"   +POut.PLong(req.ReqNeededNum)+"'"
				+ ",Descript = '"       +POut.PString(req.Descript)+"'"
				+ ",SchoolCourseNum = '"+POut.PLong(req.SchoolCourseNum)+"'"
				+ ",ProvNum = '"        +POut.PLong(req.ProvNum)+"'"
				+ ",AptNum = '"         +POut.PLong(req.AptNum)+"'"   
				+ ",PatNum = '"         +POut.PLong(req.PatNum)+"'"   
				+ ",InstructorNum = '"  +POut.PLong(req.InstructorNum)+"'"   
				+ ",DateCompleted = "   +POut.PDate(req.DateCompleted)      
				+" WHERE ReqStudentNum = '" +POut.PLong(req.ReqStudentNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(ReqStudent req) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				req.ReqStudentNum=Meth.GetInt(MethodBase.GetCurrentMethod(),req);
				return req.ReqStudentNum;
			}
			if(PrefC.RandomKeys) {
				req.ReqStudentNum=ReplicationServers.GetKey("reqstudent","ReqStudentNum");
			}
			string command= "INSERT INTO reqstudent (";
			if(PrefC.RandomKeys) {
				command+="ReqStudentNum,";
			}
			command+="ReqNeededNum,Descript,SchoolCourseNum,ProvNum,AptNum,PatNum,InstructorNum,DateCompleted) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PLong(req.ReqStudentNum)+"', ";
			}
			command+=
				 "'"+POut.PLong(req.ReqNeededNum)+"', "
				+"'"+POut.PString(req.Descript)+"', "
				+"'"+POut.PLong(req.SchoolCourseNum)+"', "
				+"'"+POut.PLong(req.ProvNum)+"', "
				+"'"+POut.PLong(req.AptNum)+"', "
				+"'"+POut.PLong(req.PatNum)+"', "
				+"'"+POut.PLong(req.InstructorNum)+"', "
				    +POut.PDate(req.DateCompleted)+")";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				req.ReqStudentNum=Db.NonQ(command,true);
			}
			return req.ReqStudentNum;
		}

		///<summary>Surround with try/catch.</summary>
		public static void Delete(long reqStudentNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),reqStudentNum);
				return;
			}
			ReqStudent req=GetOne(reqStudentNum);
			//if a reqneeded exists, then disallow deletion.
			if(ReqNeededs.GetReq(req.ReqNeededNum)==null) {
				throw new Exception(Lans.g("ReqStudents","Cannot delete requirement.  Delete the requirement needed instead."));
			}
			string command= "DELETE FROM reqstudent WHERE ReqStudentNum = "+POut.PLong(reqStudentNum);
			Db.NonQ(command);
		}

		public static DataTable RefreshOneStudent(long provNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),provNum);
			}
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
				+"WHERE reqstudent.ProvNum="+POut.PLong(provNum)
				+" ORDER BY CourseID,ReqDescript";
			DataTable raw=Db.GetTable(command);
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
				row["patient"]=PatientLogic.GetNameLF(raw.Rows[i]["LName"].ToString(),raw.Rows[i]["FName"].ToString(),
					raw.Rows[i]["Preferred"].ToString(),raw.Rows[i]["MiddleI"].ToString());
				row["ReqStudentNum"]=raw.Rows[i]["ReqStudentNum"].ToString();
				row["requirement"]=raw.Rows[i]["ReqDescript"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		public static DataTable RefreshManyStudents(long classNum,long courseNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),classNum,courseNum);
			}
			DataTable table=new DataTable();
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("donereq");
			table.Columns.Add("FName");
			table.Columns.Add("LName");
			table.Columns.Add("studentNum");//ProvNum
			table.Columns.Add("totalreq");//not used yet.  It will be changed to be based upon reqneeded. Or not used at all.
			string command="SELECT COUNT(DISTINCT req2.ReqStudentNum) donereq,FName,LName,provider.ProvNum,"
				+"COUNT(DISTINCT req1.ReqStudentNum) totalreq "
				+"FROM provider "
				+"LEFT JOIN reqstudent req1 ON req1.ProvNum=provider.ProvNum AND req1.SchoolCourseNum="+POut.PLong(courseNum)+" "
				+"LEFT JOIN reqstudent req2 ON req2.ProvNum=provider.ProvNum AND YEAR(req2.DateCompleted) > 1880 "
				+"AND req2.SchoolCourseNum="+POut.PLong(courseNum)+" "
				+"WHERE provider.SchoolClassNum="+POut.PLong(classNum)
				+" GROUP BY provider.ProvNum "
				+"ORDER BY LName,FName";
			DataTable raw=Db.GetTable(command);
			for(int i=0;i<raw.Rows.Count;i++) {
				row=table.NewRow();
				row["donereq"]=raw.Rows[i]["donereq"].ToString();
				row["FName"]=raw.Rows[i]["FName"].ToString();
				row["LName"]=raw.Rows[i]["LName"].ToString();
				row["studentNum"]=raw.Rows[i]["ProvNum"].ToString();
				row["totalreq"]=raw.Rows[i]["totalreq"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		public static List<Provider> GetStudents(long classNum) {
			//No need to check RemotingRole; no call to db.
			List<Provider> retVal=new List<Provider>();
			for(int i=0;i<ProviderC.List.Length;i++){
				if(ProviderC.List[i].SchoolClassNum==classNum){
					retVal.Add(ProviderC.List[i]);
				}
			}
			return retVal;
		}

		///<summary>Provider(student) is required.</summary>
		public static DataTable GetForCourseClass(long schoolCourse,long schoolClass) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),schoolCourse,schoolClass);
			}
			string command="SELECT Descript,ReqNeededNum "
				+"FROM reqneeded ";
			//if(schoolCourse==0){
			//	command+="WHERE ProvNum="+POut.PInt(provNum);
			//}
			//else{
				command+="WHERE SchoolCourseNum="+POut.PLong(schoolCourse)
					//+" AND ProvNum="+POut.PInt(provNum);
			//}
			+" AND SchoolClassNum="+POut.PLong(schoolClass);
			command+=" ORDER BY Descript";
			return Db.GetTable(command);
		}

		
		///<summary>All fields for all reqs will have already been set.  All except for reqstudent.ReqStudentNum if new.  Now, they just have to be persisted to the database.</summary>
		public static void SynchApt(List<ReqStudent> reqsAttached,long aptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),reqsAttached,aptNum);
				return;
			}
			//first, detach all from this appt
			string command="UPDATE reqstudent SET AptNum=0 WHERE AptNum="+POut.PLong(aptNum);
			Db.NonQ(command);
			if(reqsAttached.Count==0) {
				return;
			}
			for(int i=0;i<reqsAttached.Count;i++){
				if(reqsAttached[i].ReqStudentNum==0){
					ReqStudents.Insert(reqsAttached[i]);
				}
				else{
					ReqStudents.Update(reqsAttached[i]);
				}
			}
		}

		///<summary>Before reqneeded.Delete, this checks to make sure that req is not in use by students.  Used to prompt user.</summary>
		public static string InUseBy(long reqNeededNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),reqNeededNum);
			}
			string command="SELECT LName,FName FROM provider,reqstudent "
				+"WHERE provider.ProvNum=reqstudent.ProvNum "
				+"AND reqstudent.ReqNeededNum="+POut.PLong(reqNeededNum)
				+" AND reqstudent.DateCompleted > "+POut.PDate(new DateTime(1880,1,1));
			DataTable table=Db.GetTable(command);
			string retVal="";
			for(int i=0;i<table.Rows.Count;i++){
				retVal+=table.Rows[i]["LName"].ToString()+", "+table.Rows[i]["FName"].ToString()+"\r\n";
			}
			return retVal;
		}

		/*
		///<summary>Attaches a req to an appointment.  Importantly, it also sets the patNum to match the apt.</summary>
		public static void AttachToApt(int reqStudentNum,int aptNum) {
			string command="SELECT PatNum FROM appointment WHERE AptNum="+POut.PInt(aptNum);
			string patNum=Db.GetCount(command);
			command="UPDATE reqstudent SET AptNum="+POut.PInt(aptNum)
				+", PatNum="+patNum
				+" WHERE ReqStudentNum="+POut.PInt(reqStudentNum);
			Db.NonQ(command);
		}*/




	}

	

	


}













