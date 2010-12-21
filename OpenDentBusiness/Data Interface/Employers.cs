using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary>Employers are refreshed as needed. A full refresh is frequently triggered if an employerNum cannot be found in the HList.  Important retrieval is done directly from the db.</summary>
	public class Employers{
		private static Employer[] list;
		private static Hashtable hList;

		public static Employer[] List {
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

		///<summary>A hashtable of all employers.</summary>
		public static Hashtable HList {
			//No need to check RemotingRole; no call to db.
			get {
				if(hList==null) {
					RefreshCache();
				}
				return hList;
			}
			set {
				hList=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * from employer ORDER BY EmpName";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Employer";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			HList=new Hashtable();
			List=new Employer[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Employer();
				List[i].EmployerNum =PIn.Long   (table.Rows[i][0].ToString());
				List[i].EmpName     =PIn.String(table.Rows[i][1].ToString());
				List[i].Address     =PIn.String(table.Rows[i][2].ToString());
				List[i].Address2    =PIn.String(table.Rows[i][3].ToString());
				List[i].City        =PIn.String(table.Rows[i][4].ToString());
				List[i].State       =PIn.String(table.Rows[i][5].ToString());
				List[i].Zip         =PIn.String(table.Rows[i][6].ToString());
				List[i].Phone       =PIn.String(table.Rows[i][7].ToString());
				HList.Add(List[i].EmployerNum,List[i]);
			}
		}

		/*
		 * Not using this because it turned out to be more efficient to refresh the whole
		 * list if an empnum could not be found.
		///<summary>Just refreshes Cur from the db with info for one employer.</summary>
		public static void Refresh(int employerNum){
			Cur=new Employer();//just in case no rows are returned
			if(employerNum==0) return;
			string command="SELECT * FROM employer WHERE EmployerNum = '"+employerNum+"'";
			DataTable table=Db.GetTable(command);;
			for(int i=0;i<table.Rows.Count;i++){//almost always just 1 row, but sometimes 0
				Cur.EmployerNum   =PIn.PInt   (table.Rows[i][0].ToString());
				Cur.EmpName       =PIn.PString(table.Rows[i][1].ToString());
			}
		}*/

		///<summary></summary>
		public static void Update(Employer Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			Crud.EmployerCrud.Update(Cur);
		}

		///<summary></summary>
		public static long Insert(Employer Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.EmployerNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.EmployerNum;
			}
			return Crud.EmployerCrud.Insert(Cur);
		}

		///<summary>There MUST not be any dependencies before calling this or there will be invalid foreign keys.  This is only called from FormEmployers after proper validation.</summary>
		public static void Delete(Employer Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command="DELETE from employer WHERE EmployerNum = '"+Cur.EmployerNum.ToString()+"'";
			Db.NonQ(command);
		}

		///<summary>Returns a list of patients that are dependent on the Cur employer. The list includes carriage returns for easy display.  Used before deleting an employer to make sure employer is not in use.</summary>
		public static string DependentPatients(Employer Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),Cur);
			}
			string command="SELECT CONCAT(CONCAT(LName,', '),FName) FROM patient" 
				+" WHERE EmployerNum = '"+POut.Long(Cur.EmployerNum)+"'";
			DataTable table=Db.GetTable(command);
			string retStr="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					retStr+="\r\n";//return, newline for multiple names.
				}
				retStr+=PIn.String(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Returns a list of insplans that are dependent on the Cur employer. The list includes carriage returns for easy display.  Used before deleting an employer to make sure employer is not in use.</summary>
		public static string DependentInsPlans(Employer Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),Cur);
			}
			string command="SELECT carrier.CarrierName,CONCAT(CONCAT(patient.LName,', '),patient.FName) "
				+"FROM insplan "
				+"LEFT JOIN inssub ON insplan.PlanNum=inssub.PlanNum "
				+"LEFT JOIN patient ON inssub.Subscriber=patient.PatNum "
				+"LEFT JOIN carrier ON insplan.CarrierNum=carrier.CarrierNum "
				+"WHERE insplan.EmployerNum = "+POut.Long(Cur.EmployerNum);
			DataTable table=Db.GetTable(command);
			string retStr="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					retStr+="\r\n";//return, newline for multiple names.
				}
				retStr+=PIn.String(table.Rows[i][1].ToString())+": "+PIn.String(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Gets the name of an employer based on the employerNum.  This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently.</summary>
		public static string GetName(long employerNum) {
			//No need to check RemotingRole; no call to db.
			if(employerNum==0){
				return "";
			}
			if(HList.ContainsKey(employerNum)){
				return ((Employer)HList[employerNum]).EmpName;
			}
			//if the employerNum could not be found:
			RefreshCache();
			if(HList.ContainsKey(employerNum)){
				return ((Employer)HList[employerNum]).EmpName;
			}
			//this could only happen if corrupted:
			return "";
		}

		///<summary>Gets an employer based on the employerNum. This will work even if the list has not been refreshed recently, but if you are going to need a lot of names all at once, then it is faster to refresh first.</summary>
		public static Employer GetEmployer(long employerNum) {
			//No need to check RemotingRole; no call to db.
			if(employerNum==0){
				return new Employer();
			}
			if(HList.ContainsKey(employerNum)){
				return (Employer)HList[employerNum];
			}
			//if the employerNum could not be found:
			RefreshCache();
			if(HList.ContainsKey(employerNum)){
				return (Employer)HList[employerNum];
			}
			//this could only happen if corrupted:
			return new Employer();
		}

		///<summary>Gets an employerNum from the database based on the supplied name.  If that empName does not exist, then a new employer is created, and the employerNum for the new employer is returned.</summary>
		public static long GetEmployerNum(string empName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),empName);
			}
			if(empName==""){
				return 0;
			}
			string command="SELECT EmployerNum FROM employer" 
				+" WHERE EmpName = '"+POut.String(empName)+"'";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count>0){
				return PIn.Long(table.Rows[0][0].ToString());
			}
			Employer Cur=new Employer();
			Cur.EmpName=empName;
			Insert(Cur);
			//MessageBox.Show(Cur.EmployerNum.ToString());
			return Cur.EmployerNum;
		}

		///<summary>Returns an arraylist of Employers with names similar to the supplied string.  Used in dropdown list from employer field for faster entry.  There is a small chance that the list will not be completely refreshed when this is run, but it won't really matter if one employer doesn't show in dropdown.</summary>
		public static List<Employer> GetSimilarNames(string empName){
			//No need to check RemotingRole; no call to db.
			List<Employer> retVal=new List<Employer>();
			for(int i=0;i<List.Length;i++){
				//if(Regex.IsMatch(List[i].EmpName,"^"+empName,RegexOptions.IgnoreCase))
				if(List[i].EmpName.StartsWith(empName,StringComparison.CurrentCultureIgnoreCase)){
					retVal.Add(List[i]);
				}
			}
			return retVal;
		}

		///<summary>Combines all the given employers into one. Updates patient and insplan. Then deletes all the others.</summary>
		public static void Combine(List<long> employerNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),employerNums);
				return;
			}
			string newNum=employerNums[0].ToString();
			for(int i=1;i<employerNums.Count;i++){
				string command="UPDATE patient SET EmployerNum = '"+newNum
					+"' WHERE EmployerNum = '"+employerNums[i].ToString()+"'";
				//MessageBox.Show(string command);
				Db.NonQ(command);
				command="UPDATE insplan SET EmployerNum = '"+newNum
					+"' WHERE EmployerNum = '"+employerNums[i].ToString()+"'";
				Db.NonQ(command);
				command="DELETE FROM employer"
					+" WHERE EmployerNum = '"+employerNums[i].ToString()+"'";
				Db.NonQ(command);
			}
		}

	}

	
	

}













