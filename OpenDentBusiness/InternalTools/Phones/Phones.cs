using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Threading;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Phones{
		public static Color ColorRed=Color.Salmon;
		public static Color ColorGreen=Color.FromArgb(153,220,153);
		public static Color ColorYellow=Color.FromArgb(255,255,145);
		public static Color ColorPaleGreen=Color.FromArgb(217,255,217);

		public static List<Phone> GetPhoneList() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Phone>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM phone ORDER BY Extension";
			return Crud.PhoneCrud.SelectMany(command);
		}

		///<summary>Converts from string to enum and also handles conversion of Working to Available</summary>
		public static ClockStatusEnum GetClockStatusFromEmp(string empClockStatus) {
			//No need to check RemotingRole; no call to db.
			switch(empClockStatus) {
				case "Home":
					return ClockStatusEnum.Home;
				case "Lunch":
					return ClockStatusEnum.Lunch;
				case "Break":
					return ClockStatusEnum.Break;
				case "Working":
					return ClockStatusEnum.Available;
				default:
					return ClockStatusEnum.None;
			}
		}

		///<summary>this code is similar to code in the phone tracking server.  But here, we frequently only change clockStatus and ColorBar by setting employeeNum=-1.  If employeeNum is not -1, then EmployeeName also gets set.</summary>
		public static void SetPhoneStatus(ClockStatusEnum clockStatus,int extens) {
			//No need to check RemotingRole; no call to db.
			SetPhoneStatus(clockStatus,extens,-1);
		}

		///<summary>this code is similar to code in the phone tracking server.  But here, we frequently only change clockStatus and ColorBar by setting employeeNum=-1.  If employeeNum is not -1, then EmployeeName also gets set.  If employeeNum==0, then clears employee from that row.</summary>
		public static void SetPhoneStatus(ClockStatusEnum clockStatus,int extens,long employeeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clockStatus,extens,employeeNum);
				return;
			}
			//
			string command=@"SELECT EmployeeNum,Description,EmployeeName,
				IFNULL(IsAvailable,1) isAvail, COUNT(IsAvailable) overridden
				FROM phone
				LEFT JOIN phoneoverride ON phone.Extension=phoneoverride.Extension
				WHERE phone.Extension="+POut.Long(extens)
				//+" phone.Extension";
				+" GROUP BY EmployeeNum,Description,EmployeeName,phone.Extension";
			DataTable tablePhone=Db.GetTable(command);
			if(tablePhone.Rows.Count==0) {
				return;
			}
			long empNum=PIn.Long(tablePhone.Rows[0]["EmployeeNum"].ToString());
			string empName=PIn.String(tablePhone.Rows[0]["EmployeeName"].ToString());
			if(employeeNum==0) {
				empNum=0;
				empName="";
			}
			else if(employeeNum>0) {
				empNum=employeeNum;
				empName=Employees.GetEmp(empNum).FName;
			}
			bool isAvailable=PIn.Bool(tablePhone.Rows[0]["isAvail"].ToString());
			bool overridden=PIn.Bool(tablePhone.Rows[0]["overridden"].ToString());
			bool isInUse=false;
			if(tablePhone.Rows[0]["Description"].ToString()=="In use") {
				isInUse=true;
			}
			Color colorBar=GetColorBar(clockStatus,overridden,isAvailable,empNum,isInUse);
			string clockStatusStr=clockStatus.ToString();
			if(clockStatus==ClockStatusEnum.None) {
				clockStatusStr="";
			}
			command="UPDATE phone SET ClockStatus='"+POut.String(clockStatusStr)+"', "
				+"ColorBar="+colorBar.ToArgb().ToString()+", "
				+"EmployeeNum="+POut.Long(empNum)+", "
				+"EmployeeName='"+POut.String(empName)+"' "
				+"WHERE Extension="+extens;
			Db.NonQ(command);
		}

		public static Color GetColorBar(ClockStatusEnum clockStatus,bool overridden,bool isAvailable,long empNum,bool isInUse) {
			//No need to check RemotingRole; no call to db.
			Color colorBar=Color.White;
			if(empNum==0) {
				//no colors
			}
			else if(overridden && !isAvailable) {
				//no colors
			}
			else if(!overridden && PhoneEmpDefaults.IsNoColor(empNum)) {
				//no colors
			}
			else if(isInUse) {
				colorBar=ColorRed;
			}
			//the rest are for idle:
			else if(clockStatus==ClockStatusEnum.Available) {
				colorBar=ColorGreen;
			}
			//"Unavailable" already handled above
			else if(clockStatus==ClockStatusEnum.Off) {
				//colorText=Color.Gray;
				//no colorBar
			}
			else if(clockStatus==ClockStatusEnum.Break) {
				//no color
			}
			else if(clockStatus==ClockStatusEnum.Training
				|| clockStatus==ClockStatusEnum.TeamAssist
				|| clockStatus==ClockStatusEnum.WrapUp
				|| clockStatus==ClockStatusEnum.OfflineAssist) {
				colorBar=ColorYellow;
			}
			else if(clockStatus==ClockStatusEnum.Backup) {
				colorBar=ColorPaleGreen;
			}
			return colorBar;
		}

		public static Phone GetPhoneForExtension(List<Phone> phoneList,int extens){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<phoneList.Count;i++) {
				if(phoneList[i].Extension==extens) {
					return phoneList[i];
				}
			}
			return null;
		}

		/*
		///<summary>Gets the phoneNum which is the primary key, not the phone number.</summary>
		public static long GetPhoneNum(int extension){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),extension);
			}
			string command="SELECT PhoneNum FROM phone WHERE Extension ="+POut.Long(extension);
			string result= Db.GetScalar(command);
			return PIn.Long(result);
		}*/

		///<summary>Can handle null for either parameter.</summary>
		public static void SetWebCamImage(long extension,Bitmap bitmap) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),extension,bitmap);
				return;
			}
			if(extension==0) {
				return;
			}
			string command="UPDATE phone SET "
				+"WebCamImage   = '"+POut.Bitmap(bitmap)+"' "//handles null
				+"WHERE Extension = "+POut.Long(extension);
			Db.NonQ(command);
		}

		public static void ClearImages() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="UPDATE phone SET WebCamImage=''";
			Db.NonQ(command);
		}



	}

	
}