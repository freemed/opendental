using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Employees{
		///<summary></summary>
		public static Employee[] ListLong;
		///<summary></summary>
		public static Employee[] ListShort;//does not include hidden employees

		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * FROM employee";
			DataTable table=General.GetTable(command);
			ListLong=new Employee[table.Rows.Count];
			ArrayList tempList=new ArrayList();
			//Employee temp;
			for(int i=0;i<table.Rows.Count;i++){
				ListLong[i]=new Employee();
				ListLong[i].EmployeeNum = PIn.PInt   (table.Rows[i][0].ToString());
				ListLong[i].LName =       PIn.PString(table.Rows[i][1].ToString());
				ListLong[i].FName =       PIn.PString(table.Rows[i][2].ToString());
				ListLong[i].MiddleI =     PIn.PString(table.Rows[i][3].ToString());
				ListLong[i].IsHidden =    PIn.PBool  (table.Rows[i][4].ToString());
				ListLong[i].ClockStatus =	PIn.PString(table.Rows[i][5].ToString());
				if(!ListLong[i].IsHidden){
					tempList.Add(ListLong[i]);
				}
			}
			ListShort=new Employee[tempList.Count];
			for(int i=0;i<tempList.Count;i++){
				ListShort[i]=(Employee)tempList[i];
			}
		}

		///<summary></summary>
		public static void Update(Employee Cur){
			string command="UPDATE employee SET " 
				+ "lname = '"       +POut.PString(Cur.LName)+"' "
				+ ",fname = '"      +POut.PString(Cur.FName)+"' "
				+ ",middlei = '"    +POut.PString(Cur.MiddleI)+"' "
				+ ",ishidden = '"   +POut.PBool  (Cur.IsHidden)+"' "
				+ ",ClockStatus = '"+POut.PString(Cur.ClockStatus)+"' "
				+"WHERE EmployeeNum = '"+POut.PInt(Cur.EmployeeNum)+"'";
			//MessageBox.Show(string command);
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(Employee Cur){
			string command = "INSERT INTO employee (lname,fname,middlei,ishidden"
				+",ClockStatus) "
				+"VALUES("
				+"'"+POut.PString(Cur.LName)+"', "
				+"'"+POut.PString(Cur.FName)+"', "
				+"'"+POut.PString(Cur.MiddleI)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PString(Cur.ClockStatus)+"')";
			Cur.EmployeeNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Delete(Employee Cur){
			string command= "DELETE from employee WHERE EmployeeNum = '"+Cur.EmployeeNum.ToString()+"'";
			General.NonQ(command);
		}

		///<summary>Returns LName,FName MiddleI for the provided employee.</summary>
		public static string GetName(Employee emp){
			return(emp.LName+", "+emp.FName+" "+emp.MiddleI);
		}

		///<summary>Loops through List to find matching employee, and returns LName,FName MiddleI.</summary>
		public static string GetName(int employeeNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].EmployeeNum==employeeNum){
					return GetName(ListLong[i]);
				}
			}
			return "";
		}

		///<summary>Loops through List to find matching employee, and returns first 2 letters of first name.  Will later be improved with abbr field.</summary>
		public static string GetAbbr(int employeeNum){
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



	}

	

	
	

}













