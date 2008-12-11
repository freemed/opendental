using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class PhoneOverrides{
		public static PhoneOverride GetPhoneOverride(int phoneOverrideNum){
			string command="SELECT * FROM phoneoverride WHERE PhoneOverrideNum="+POut.PInt(phoneOverrideNum);
			List<PhoneOverride> list=SubmitAndFill(command);
			if(list.Count==0){
				return null;
			}
			return list[0];
		}

		///<summary>Could easily return null.</summary>
		public static PhoneOverride GetByExtAndEmp(int extension,int employeeNum){
			string command="SELECT * FROM phoneoverride "
				+"WHERE Extension="+POut.PInt(extension)+" "
				+"AND EmpCurrent="+POut.PInt(employeeNum);
			List<PhoneOverride> list=SubmitAndFill(command);
			if(list.Count==0){
				return null;
			}
			return list[0];
		}

		private static List<PhoneOverride> SubmitAndFill(string command){
			DataTable table=General.GetTable(command);
			List<PhoneOverride> list=new List<PhoneOverride>();
			PhoneOverride phoneCur;
			for(int i=0;i<table.Rows.Count;i++){
				phoneCur=new PhoneOverride();
				phoneCur.PhoneOverrideNum=PIn.PInt   (table.Rows[0]["PhoneOverrideNum"].ToString());
				phoneCur.Extension       =PIn.PInt   (table.Rows[0]["Extension"].ToString());
				phoneCur.EmpCurrent      =PIn.PInt   (table.Rows[0]["EmpCurrent"].ToString());
				phoneCur.IsAvailable     =PIn.PBool  (table.Rows[0]["IsAvailable"].ToString());
				phoneCur.Explanation     =PIn.PString(table.Rows[0]["Explanation"].ToString());
				list.Add(phoneCur);
			}
			return list;
		}

		///<summary></summary>
		public static void Insert(PhoneOverride phoneCur){
			string command="INSERT INTO phoneoverride(Extension,EmpCurrent,IsAvailable,Explanation) "
				+"VALUES("
				+POut.PInt(phoneCur.Extension)+","
				+POut.PInt(phoneCur.EmpCurrent)+","
				+POut.PBool(phoneCur.IsAvailable)+","
				+"'"+POut.PString(phoneCur.Explanation)+"')";
			phoneCur.PhoneOverrideNum=General.NonQ(command,true);
			if(phoneCur.IsAvailable){
				command="SELECT ClockStatus FROM employee WHERE EmployeeNum="+POut.PInt(phoneCur.EmpCurrent);
				DataTable tableEmp=General.GetTable(command);
				if(tableEmp.Rows.Count>0){
					string status=tableEmp.Rows[0][0].ToString();
					if(status=="Working"){
						status="Available";
					}
					Employees.SetPhoneStatus(status,phoneCur.Extension);
				}
			}
			else{
				Employees.SetPhoneStatus("Unavailable",phoneCur.Extension);
			}
		}

		///<summary></summary>
		public static void Update(PhoneOverride phoneCur){
			string command="UPDATE phoneoverride SET "
				+"Extension="+phoneCur.Extension.ToString()+","
				+"EmpCurrent="+phoneCur.EmpCurrent.ToString()+","
				+"IsAvailable="+POut.PBool(phoneCur.IsAvailable)+","
				+"Explanation='"+POut.PString(phoneCur.Explanation)+"' "
				+"WHERE PhoneOverrideNum="+POut.PInt(phoneCur.PhoneOverrideNum);
			General.NonQ(command);
			if(phoneCur.IsAvailable){
				command="SELECT ClockStatus FROM employee WHERE EmployeeNum="+POut.PInt(phoneCur.EmpCurrent);
				DataTable tableEmp=General.GetTable(command);
				if(tableEmp.Rows.Count>0){
					string status=tableEmp.Rows[0][0].ToString();
					if(status=="Working"){
						status="Available";
					}
					Employees.SetPhoneStatus(status,phoneCur.Extension);
				}
			}
			else{
				Employees.SetPhoneStatus("Unavailable",phoneCur.Extension);
			}
		}

		public static void Delete(PhoneOverride phoneCur){
			string command="DELETE FROM phoneoverride WHERE PhoneOverrideNum="+POut.PInt(phoneCur.PhoneOverrideNum);
			General.NonQ(command);
			command="SELECT ClockStatus FROM employee WHERE EmployeeNum="+POut.PInt(phoneCur.EmpCurrent);
			DataTable tableEmp=General.GetTable(command);
			if(tableEmp.Rows.Count>0){
				string status=tableEmp.Rows[0][0].ToString();
				if(status=="Working"){
					status="Available";
				}
				Employees.SetPhoneStatus(status,phoneCur.Extension);
			}
		}

		///<summary>If an existing override changes the extension of an employee, then this just changes IsAvailable to true.  But if the existing override has no effect on the extension, then it just gets deleted.</summary>
		public static void SetAvailable(int extension,int empNum){
			PhoneOverride phoneOR=GetByExtAndEmp(extension,empNum);
			if(phoneOR==null){
				return;//no override exists.
			}
			Employee emp=Employees.GetEmp(empNum);
			if(empNum==4//amber
				|| empNum==21//natalie
				|| empNum==20//britt
				|| empNum==22//jordan
				|| empNum==15//derek
				|| empNum==18)//james
			{
				phoneOR.IsAvailable=true;
				phoneOR.Explanation="";
				Update(phoneOR);
				return;
			}
			if(emp.PhoneExt==extension){
				Delete(phoneOR);
				return;
			}
			//phone extension doesn't match:
			phoneOR.IsAvailable=true;
			phoneOR.Explanation="";
			Update(phoneOR);
		}


	}

}









