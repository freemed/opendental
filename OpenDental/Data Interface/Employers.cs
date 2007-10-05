using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>Employers are not refreshed as local data, but are refreshed as needed. A full refresh is frequently triggered if an employerNum cannot be found in the HList.  Important retrieval is done directly from the db.</summary>
	public class Employers{
		///<summary></summary>
		public static Employer[] List;
		///<summary>A hashtable of all employers.</summary>
		public static Hashtable HList;

		///<summary>The functions that use this are smart enought to refresh as needed.  So no need to invalidate local data for little stuff.</summary>
		public static void Refresh(){
			HList=new Hashtable();
			string command= "SELECT * from employer ORDER BY EmpName";
			DataTable table=General.GetTable(command);
			List=new Employer[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Employer();
				List[i].EmployerNum =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].EmpName     =PIn.PString(table.Rows[i][1].ToString());
				List[i].Address     =PIn.PString(table.Rows[i][2].ToString());
				List[i].Address2    =PIn.PString(table.Rows[i][3].ToString());
				List[i].City        =PIn.PString(table.Rows[i][4].ToString());
				List[i].State       =PIn.PString(table.Rows[i][5].ToString());
				List[i].Zip         =PIn.PString(table.Rows[i][6].ToString());
				List[i].Phone       =PIn.PString(table.Rows[i][7].ToString());
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
			DataTable table=General.GetTable(command);;
			for(int i=0;i<table.Rows.Count;i++){//almost always just 1 row, but sometimes 0
				Cur.EmployerNum   =PIn.PInt   (table.Rows[i][0].ToString());
				Cur.EmpName       =PIn.PString(table.Rows[i][1].ToString());
			}
		}*/

		///<summary></summary>
		public static void Update(Employer Cur){
			string command="UPDATE employer SET " 
				+ "EmpName= '"  +POut.PString(Cur.EmpName)+"' "
				+ ",Address= '"    +POut.PString(Cur.Address)+"' "
				+ ",Address2= '"   +POut.PString(Cur.Address2)+"' "
				+ ",City= '"       +POut.PString(Cur.City)+"' "
				+ ",State= '"      +POut.PString(Cur.State)+"' "
				+ ",Zip= '"        +POut.PString(Cur.Zip)+"' "
				+ ",Phone= '"      +POut.PString(Cur.Phone)+"' "
				+" WHERE EmployerNum = '"+POut.PInt(Cur.EmployerNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(Employer Cur){
			if(PrefB.RandomKeys){
				Cur.EmployerNum=MiscData.GetKey("employer","EmployerNum");
			}
			string command="INSERT INTO employer (";
			if(PrefB.RandomKeys){
				command+="EmployerNum,";
			}
			command+="EmpName,Address,Address2,City,State,Zip,Phone) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(Cur.EmployerNum)+"', ";
			}
			command+=
				 "'"+POut.PString(Cur.EmpName)+"', "
				+"'"+POut.PString(Cur.Address)+"', "
				+"'"+POut.PString(Cur.Address2)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PString(Cur.Zip)+"', "
				+"'"+POut.PString(Cur.Phone)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
				Cur.EmployerNum=General.NonQ(command,true);
			}
		}

		///<summary>There MUST not be any dependencies before calling this or there will be invalid foreign keys.  This is only called from FormEmployers after proper validation.</summary>
		public static void Delete(Employer Cur){
			string command="DELETE from employer WHERE EmployerNum = '"+Cur.EmployerNum.ToString()+"'";
			General.NonQ(command);
		}

		///<summary>Returns a list of patients that are dependent on the Cur employer. The list includes carriage returns for easy display.  Used before deleting an employer to make sure employer is not in use.</summary>
		public static string DependentPatients(Employer Cur){
			string command="SELECT CONCAT(CONCAT(LName,', '),FName) FROM patient" 
				+" WHERE EmployerNum = '"+POut.PInt(Cur.EmployerNum)+"'";
			DataTable table=General.GetTable(command);
			string retStr="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					retStr+="\r\n";//return, newline for multiple names.
				}
				retStr+=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Returns a list of insplans that are dependent on the Cur employer. The list includes carriage returns for easy display.  Used before deleting an employer to make sure employer is not in use.</summary>
		public static string DependentInsPlans(Employer Cur){
			string command="SELECT insplan.Carrier,CONCAT(patient.LName,patient.FName) FROM insplan,patient" 
				+" WHERE insplan.Subscriber=patient.PatNum"
				+" AND insplan.EmployerNum = '"+POut.PInt(Cur.EmployerNum)+"'";
			DataTable table=General.GetTable(command);
			string retStr="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					retStr+="\r\n";//return, newline for multiple names.
				}
				retStr+=PIn.PString(table.Rows[i][1].ToString())+": "+PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Gets the name of an employer based on the employerNum.  This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently.</summary>
		public static string GetName(int employerNum){
			if(employerNum==0){
				return "";
			}
			if(HList.ContainsKey(employerNum)){
				return ((Employer)HList[employerNum]).EmpName;
			}
			//if the employerNum could not be found:
			Refresh();
			if(HList.ContainsKey(employerNum)){
				return ((Employer)HList[employerNum]).EmpName;
			}
			//this could only happen if corrupted:
			return "";
		}

		///<summary>Gets an employer based on the employerNum. This will work even if the list has not been refreshed recently, but if you are going to need a lot of names all at once, then it is faster to refresh first.</summary>
		public static Employer GetEmployer(int employerNum){
			if(employerNum==0){
				return new Employer();
			}
			if(HList.ContainsKey(employerNum)){
				return (Employer)HList[employerNum];
			}
			//if the employerNum could not be found:
			Refresh();
			if(HList.ContainsKey(employerNum)){
				return (Employer)HList[employerNum];
			}
			//this could only happen if corrupted:
			return new Employer();
		}

		///<summary>Gets an employerNum from the database based on the supplied name.  If that empName does not exist, then a new employer is created, and the employerNum for the new employer is returned.</summary>
		public static int GetEmployerNum(string empName){
			if(empName==""){
				return 0;
			}
			string command="SELECT EmployerNum FROM employer" 
				+" WHERE EmpName = '"+POut.PString(empName)+"'";
			DataTable table=General.GetTable(command);
			if(table.Rows.Count>0){
				return PIn.PInt(table.Rows[0][0].ToString());
			}
			Employer Cur=new Employer();
			Cur.EmpName=empName;
			Insert(Cur);
			//MessageBox.Show(Cur.EmployerNum.ToString());
			return Cur.EmployerNum;
		}

		///<summary>Returns an arraylist of Employers with names similar to the supplied string.  Used in dropdown list from employer field for faster entry.  There is a small chance that the list will not be completely refreshed when this is run, but it won't really matter if one employer doesn't show in dropdown.</summary>
		public static ArrayList GetSimilarNames(string empName){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				//if(Regex.IsMatch(List[i].EmpName,"^"+empName,RegexOptions.IgnoreCase))
				if(List[i].EmpName.ToUpper().IndexOf(empName.ToUpper())==0)
					retVal.Add(List[i]);
			}
			return retVal;
		}

		///<summary>Combines all the given employers into one. Updates patient and insplan. Then deletes all the others.</summary>
		public static void Combine(int[] employerNums){
			string newNum=employerNums[0].ToString();
			for(int i=1;i<employerNums.Length;i++){
				string command="UPDATE patient SET EmployerNum = '"+newNum
					+"' WHERE EmployerNum = '"+employerNums[i].ToString()+"'";
				//MessageBox.Show(string command);
				General.NonQ(command);
				command="UPDATE insplan SET EmployerNum = '"+newNum
					+"' WHERE EmployerNum = '"+employerNums[i].ToString()+"'";
				General.NonQ(command);
				command="DELETE FROM employer"
					+" WHERE EmployerNum = '"+employerNums[i].ToString()+"'";
				General.NonQ(command);
			}
		}

	}

	
	

}













