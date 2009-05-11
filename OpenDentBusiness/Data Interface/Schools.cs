using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{

  ///<summary></summary>
	public class Schools{
		///<summary>Used in screening window. Simpler interface.  Only used in UI.</summary>
		public static string[] ListNames;
		/*
		///<summary>This list is only refreshed as needed rather than being part of the local data.</summary>
		private static School[] list;
		

		public static School[] List {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					Refresh();
				}
				return list;
			}
			set {
				list=value;
			}
		}*/

		///<summary>Refreshes List as needed directly from the database.  List only includes items that will show in dropdown list.</summary>
		public static List<School> Refresh(string name){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<School>>(MethodBase.GetCurrentMethod(),name);
			}
			string command =
				"SELECT * from school "
				+"WHERE SchoolName LIKE '"+name+"%' "
				+"ORDER BY SchoolName";
			DataTable table=Db.GetTable(command);;
			List<School> retVal=new List<School>();
			School school;
			for(int i=0;i<table.Rows.Count;i++){
				school=new School();
				school.SchoolName =PIn.PString(table.Rows[i][0].ToString());
				school.SchoolCode =PIn.PString(table.Rows[i][1].ToString());
				school.OldSchoolName =PIn.PString(table.Rows[i][0].ToString());
				retVal.Add(school);
			}
			return retVal;
		}

		///<summary></summary>
		public static List<School> Refresh() {
			//No need to check RemotingRole; no call to db.
			return Refresh("");
		}

		///<summary>Gets an array of strings containing all the schools in alphabetical order.  Used for the screening interface which must be simpler than the usual interface.</summary>
		public static void GetListNames(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return; 
			}
			string command =
				"SELECT SchoolName from school "
				+"ORDER BY SchoolName";
			DataTable table=Db.GetTable(command);;
			ListNames=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListNames[i]=PIn.PString(table.Rows[i][0].ToString());
			}
		}

		///<summary>Need to make sure schoolname not already in db.</summary>
		public static void Insert(School Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "INSERT INTO school (SchoolName,SchoolCode) "
				+"VALUES ("
				+"'"+POut.PString(Cur.SchoolName)+"', "
				+"'"+POut.PString(Cur.SchoolCode)+"')";
			//MessageBox.Show(string command);
			Db.NonQ(command);
		}

		///<summary>Updates the schoolname and code in the school table, and also updates all patients that were using the oldschool name.</summary>
		public static void Update(School Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "UPDATE school SET "
				+"SchoolName ='"  +POut.PString(Cur.SchoolName)+"'"
				+",SchoolCode ='" +POut.PString(Cur.SchoolCode)+"'"
				+" WHERE SchoolName = '"+POut.PString(Cur.OldSchoolName)+"'";
			Db.NonQ(command);
			//then, update all patients using that school
			command = "UPDATE patient SET "
				+"GradeSchool ='"  +POut.PString(Cur.SchoolName)+"'"
				+" WHERE GradeSchool = '"+POut.PString(Cur.OldSchoolName)+"'";
			Db.NonQ(command);
		}

		///<summary>Must run UsedBy before running this.</summary>
		public static void Delete(School Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE from school WHERE SchoolName = '"+POut.PString(Cur.SchoolName)+"'";
			Db.NonQ(command);
		}

		///<summary>Use before DeleteCur to determine if this school name is in use. Returns a formatted string that can be used to quickly display the names of all patients using the schoolname.</summary>
		public static string UsedBy(string schoolName){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),schoolName);
			}
			string command =
				"SELECT LName,FName from patient "
				+"WHERE GradeSchool = '"+POut.PString(schoolName)+"' ";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0)
				return "";
			string retVal="";
			for(int i=0;i<table.Rows.Count;i++){
				retVal+=PIn.PString(table.Rows[i][0].ToString())+", "
					+PIn.PString(table.Rows[i][1].ToString());
				if(i<table.Rows.Count-1){//if not the last row
					retVal+="\r";
				}
			}
			return retVal;
		}

		///<summary>Use before InsertCur to determine if this school name already exists. Also used when closing patient edit window to validate that the schoolname exists.</summary>
		public static bool DoesExist(string schoolName){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),schoolName);
			}
			string command =
				"SELECT * from school "
				+"WHERE SchoolName = '"+POut.PString(schoolName)+"' ";
			DataTable table=Db.GetTable(command);;
			if(table.Rows.Count==0)
				return false;
			else
				return true;
		}

	}

	

}













