using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Employees{
		private static Employee[] listLong;
		private static Employee[] listShort;

		public static Employee[] ListLong {
			//No need to check RemotingRole; no call to db.
			get {
				if(listLong==null) {
					RefreshCache();
				}
				return listLong;
			}
			set {
				listLong=value;
			}
		}

		///<summary>Does not include hidden employees</summary>
		public static Employee[] ListShort {
			//No need to check RemotingRole; no call to db.
			get {
				if(listShort==null) {
					RefreshCache();
				}
				return listShort;
			}
			set {
				listShort=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM employee ORDER BY IsHidden,FName,LName";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Employee";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			listLong=Crud.EmployeeCrud.TableToList(table).ToArray();
			List<Employee> tempList=new List<Employee>();
			for(int i=0;i<listLong.Length;i++) {
				if(!listLong[i].IsHidden) {
					tempList.Add(listLong[i]);
				}
			}
			listShort=tempList.ToArray();
		}

		/*public static Employee[] GetListByExtension(){
			if(ListShort==null){
				return new Employee[0];
			}
			Employee[] arrayCopy=new Employee[ListShort.Length];
			ListShort.CopyTo(arrayCopy,0);
			int[] arrayKeys=new int[ListShort.Length];
			for(int i=0;i<ListShort.Length;i++){
				arrayKeys[i]=ListShort[i].PhoneExt;
			}
			Array.Sort(arrayKeys,arrayCopy);
			//List<Employee> retVal=new List<Employee>(ListShort);
			//retVal.Sort(
			return arrayCopy;
		}*/

		///<summary></summary>
		public static void Update(Employee Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			if(Cur.LName=="" && Cur.FName=="") {
				throw new ApplicationException(Lans.g("FormEmployeeEdit","Must include either first name or last name"));
			}
			Crud.EmployeeCrud.Update(Cur);
		}

		///<summary></summary>
		public static long Insert(Employee Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.EmployeeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.EmployeeNum;
			}if(Cur.LName=="" && Cur.FName=="") {
				throw new ApplicationException(Lans.g("FormEmployeeEdit","Must include either first name or last name"));
			}
			return Crud.EmployeeCrud.Insert(Cur);
		}

		///<summary>Surround with try-catch</summary>
		public static void Delete(long employeeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),employeeNum);
				return;
			}
			//appointment.Assistant will not block deletion
			//schedule.EmployeeNum will not block deletion
			string command="SELECT COUNT(*) FROM clockevent WHERE EmployeeNum="+POut.Long(employeeNum);
			if(Db.GetCount(command)!="0"){
				throw new ApplicationException(Lans.g("FormEmployeeSelect",
					"Not allowed to delete employee because of attached clock events."));
			}
			command="SELECT COUNT(*) FROM timeadjust WHERE EmployeeNum="+POut.Long(employeeNum);
			if(Db.GetCount(command)!="0") {
				throw new ApplicationException(Lans.g("FormEmployeeSelect",
					"Not allowed to delete employee because of attached time adjustments."));
			}
			command="SELECT COUNT(*) FROM userod WHERE EmployeeNum="+POut.Long(employeeNum);
			if(Db.GetCount(command)!="0") {
				throw new ApplicationException(Lans.g("FormEmployeeSelect",
					"Not allowed to delete employee because of attached user."));
			}
			command="UPDATE appointment SET Assistant=0 WHERE Assistant="+POut.Long(employeeNum);
			Db.NonQ(command);
			command="DELETE FROM schedule WHERE EmployeeNum="+POut.Long(employeeNum);
			Db.NonQ(command);
			command= "DELETE FROM employee WHERE EmployeeNum ="+POut.Long(employeeNum);
			Db.NonQ(command);
		}

		/*
		///<summary>Returns LName,FName MiddleI for the provided employee.</summary>
		public static string GetNameLF(Employee emp){
			return(emp.LName+", "+emp.FName+" "+emp.MiddleI);
		}

		///<summary>Loops through List to find matching employee, and returns LName,FName MiddleI.</summary>
		public static string GetNameLF(int employeeNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].EmployeeNum==employeeNum){
					return GetNameLF(ListLong[i]);
				}
			}
			return "";
		}*/

		///<summary>Returns FName MiddleI LName for the provided employee.</summary>
		public static string GetNameFL(Employee emp) {
			//No need to check RemotingRole; no call to db.
			return (emp.FName+" "+emp.MiddleI+" "+emp.LName);
		}

		///<summary>Loops through List to find matching employee, and returns FName MiddleI LName.</summary>
		public static string GetNameFL(long employeeNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].EmployeeNum==employeeNum) {
					return GetNameFL(ListLong[i]);
				}
			}
			return "";
		}

		///<summary>Loops through List to find matching employee, and returns first 2 letters of first name.  Will later be improved with abbr field.</summary>
		public static string GetAbbr(long employeeNum) {
			//No need to check RemotingRole; no call to db.
			string retVal="";
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].EmployeeNum==employeeNum){
					retVal=ListLong[i].FName;
					if(retVal.Length>2)
						retVal=retVal.Substring(0,2);
					return retVal;
				}
			}
			return "";
		}

		public static Employee GetEmp(long employeeNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].EmployeeNum==employeeNum) {
					return ListLong[i];
				}
			}
			return null;
		}

		/// <summary> Returns -1 if employeeNum is not found.  0 if not hidden and 1 if hidden </summary>		
		public static int IsHidden(long employeeNum) {
			//No need to check RemotingRole; no call to db.
			int rValue = -1;
			if (ListLong != null){
				for (int i = 0; i < ListLong.Length; i++){
					if (ListLong[i].EmployeeNum == employeeNum){
						rValue = (ListLong[i].IsHidden ? 1 : 0);
						i = ListLong.Length;
					}
				}
			}
			return rValue;
		}

		///<summary>Loops through List to find the given extension and returns the employeeNum if found.  Otherwise, returns -1;</summary>
		public static long GetEmpNumAtExtension(int phoneExt) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].PhoneExt==phoneExt){
					return ListLong[i].EmployeeNum;
				}
			}
			return -1;
		}

		
		


	}

	

	
	

}













