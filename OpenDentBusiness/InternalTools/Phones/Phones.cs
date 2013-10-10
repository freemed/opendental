using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Reflection;
using System.Threading;

namespace OpenDentBusiness {
	///<summary></summary>
	public class Phones {
		public static Color ColorRed=Color.Salmon;
		public static Color ColorGreen=Color.FromArgb(153,220,153);
		public static Color ColorYellow=Color.FromArgb(255,255,145);
		public static Color ColorPaleGreen=Color.FromArgb(217,255,217);
		public static Color ColorSkyBlue=Color.SkyBlue;
		public static Color ColorOrchid=Color.Orchid;

		public static List<Phone> GetPhoneList() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Phone>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM phone ORDER BY Extension";
			try {
				return Crud.PhoneCrud.SelectMany(command);
			}
			catch {
				return new List<Phone>();
			}
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
			string command=@"SELECT phoneempdefault.EmployeeNum,phoneempdefault.IsTriageOperator,Description,phoneempdefault.EmpName,HasColor,phone.ClockStatus "
				+"FROM phone "
				+"LEFT JOIN phoneempdefault ON phone.Extension=phoneempdefault.PhoneExt "
				+"WHERE phone.Extension="+POut.Long(extens);
			DataTable tablePhone=Db.GetTable(command);
			if(tablePhone.Rows.Count==0) {
				//It would be nice if we could create a phone row for this extension.
				return;
			}
			long empNum=PIn.Long(tablePhone.Rows[0]["EmployeeNum"].ToString());
			bool isTriageOperator=PIn.Bool(tablePhone.Rows[0]["IsTriageOperator"].ToString());
			string empName=PIn.String(tablePhone.Rows[0]["EmpName"].ToString());
			string clockStatusDb=PIn.String(tablePhone.Rows[0]["ClockStatus"].ToString());
			Employee emp=Employees.GetEmp(employeeNum);
			if(emp!=null) {//A new employee is going to take over this extension.
				empName=emp.FName;
				empNum=emp.EmployeeNum;
			}
			else if(employeeNum==0) {//Clear the employee from that row.
				empName="";
				empNum=0;
			}
			//if these values are null because of missing phoneempdefault row, they will default to false
			//PhoneEmpStatusOverride statusOverride=(PhoneEmpStatusOverride)PIn.Int(tablePhone.Rows[0]["StatusOverride"].ToString());
			bool hasColor=PIn.Bool(tablePhone.Rows[0]["HasColor"].ToString());
			bool isInUse=false;
			if(tablePhone.Rows[0]["Description"].ToString()=="In use") {
				isInUse=true;
			}
			#region DateTimeStart
			//When a user shows up as a color on the phone panel, we want a timer to be constantly going to show how long they've been off the phone.
			string dateTimeStart="";
			//It's possible that a new user has never clocked in before, therefore their clockStatus will be empty.  Simply set it to the status that they are trying to go to.
			if(clockStatusDb=="") {
				clockStatusDb=clockStatus.ToString();
			}
			//User is going to a status of Home, Break, or Lunch.  Always clear the DateTimeStart column no matter what.
			if(clockStatus==ClockStatusEnum.Home
				|| clockStatus==ClockStatusEnum.Lunch
				|| clockStatus==ClockStatusEnum.Break) 
			{
				//The user is going home or on break.  Simply clear the DateTimeStart column.
				dateTimeStart="DateTimeStart='0001-01-01', ";
			}
			else {//User shows as a color on big phones and is not going to a status of Home, Lunch, or Break.  Example: Available, Training etc.
				//Get the current clock status from the database.
				ClockStatusEnum clockStatusCur=(ClockStatusEnum)Enum.Parse(typeof(ClockStatusEnum),clockStatusDb);
				//Start the clock if the user is going from a break status to any other non-break status.
				if(clockStatusCur==ClockStatusEnum.Home
					|| clockStatusCur==ClockStatusEnum.Lunch
					|| clockStatusCur==ClockStatusEnum.Break) 
				{
					//The user is clocking in from home, lunch, or break.  Start the timer up.
					if(hasColor) {//Only start up the timer when someone with color clocks in.
						dateTimeStart="DateTimeStart=NOW(), ";
					}
				}
			}
			#endregion
			//Update the phone row to reflect the new clock status of the user.
			Color colorBarNew=GetColorBar(clockStatus,empNum,isInUse,hasColor,isTriageOperator);
			string clockStatusNew=clockStatus.ToString();
			if(clockStatus==ClockStatusEnum.None) {
				clockStatusNew="";
			}
			command="UPDATE phone SET ClockStatus='"+POut.String(clockStatusNew)+"', "
				+dateTimeStart
				+"ColorBar="+colorBarNew.ToArgb().ToString()+", "
				+"EmployeeNum="+POut.Long(empNum)+", "
				+"EmployeeName='"+POut.String(empName)+"' "
				+"WHERE Extension="+extens;
			Db.NonQ(command);

			if(PrefC.GetBool(PrefName.DockPhonePanelShow)){ //hq only
				//Zero out any duplicate phone table rows for this employee. 
				//This is possible if a user logged off and another employee logs into their computer. This would cause duplicate entries in the big phones window.
				UpdatePhoneToEmpty(employeeNum,extens);
			}
		}

		///<summary>Zero out any duplicate phone table rows for this employee. This is possible if a user logged off and another employee logs into their computer. This would cause duplicate entries in the big phones window. If ignoreExtension less than 1 (inavlid) then zero out all entries for this employeeNum.</summary>
		public static void UpdatePhoneToEmpty(long employeeNum,int ignoreExtension) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),employeeNum,ignoreExtension);
				return;
			}
			string command="UPDATE phone SET "
					+"EmployeeName='', "
					+"ClockStatus='', "
					+"Description='', "
					+"EmployeeNum=0 "
					+"WHERE EmployeeNum="+POut.Long(employeeNum)+" "
					+"AND Extension!="+POut.Int(ignoreExtension);
			Db.NonQ(command);
		}

		public static void UpdateColorBarForEmployee(long employeeNum,int extension,bool isTriageOperator) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),employeeNum,extension,isTriageOperator);
				return;
			}
			string command=@"SELECT phone.Description,phoneempdefault.HasColor,phone.ClockStatus "
				+"FROM phone "
				+"LEFT JOIN phoneempdefault ON phone.Extension=phoneempdefault.PhoneExt AND phone.EmployeeNum=phoneempdefault.EmployeeNum "
				+"WHERE phone.Extension="+POut.Long(extension);
			DataTable tablePhone=Db.GetTable(command);
			if(tablePhone.Rows.Count==0) {
				return;
			}
			bool isColor=PIn.Bool(tablePhone.Rows[0]["HasColor"].ToString());
			bool isInUse=false;
			if(tablePhone.Rows[0]["Description"].ToString()=="In use") {
				isInUse=true;
			}
			string clockStatusDb=PIn.String(tablePhone.Rows[0]["ClockStatus"].ToString());
			ClockStatusEnum clockStatus=ClockStatusEnum.Home;
			if(!Enum.TryParse<ClockStatusEnum>(clockStatusDb,out clockStatus)) {
				clockStatus=ClockStatusEnum.Home;
			}
			Color colorBarNew=GetColorBar(clockStatus,employeeNum,isInUse,isColor,isTriageOperator);			
			command="UPDATE phone SET "
					+"ColorBar="+colorBarNew.ToArgb().ToString()+" "
					+"WHERE EmployeeNum="+POut.Long(employeeNum);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static Color GetColorBar(ClockStatusEnum clockStatus,long empNum,bool isInUse,bool isColor,bool isTriageOperator) {
			//No need to check RemotingRole; no call to db.
			Color colorBar=Color.White;
			if(empNum==0) {
				//no colors
			}
			else if(!isColor) {
				//no colors
			}
			else if(isTriageOperator) {
				colorBar=ColorSkyBlue;
			}
			else if(isInUse) {
				colorBar=ColorRed;
				if(clockStatus==ClockStatusEnum.NeedsHelp) {
					colorBar=ColorOrchid;
				}
			}
			//the rest are for idle:
			else if(clockStatus==ClockStatusEnum.Available) {
				colorBar=ColorGreen;
			}
			else if(clockStatus==ClockStatusEnum.Unavailable) {
				//no color
			}
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
			else if(clockStatus==ClockStatusEnum.NeedsHelp) {
				colorBar=ColorOrchid;
			}
			return colorBar;
		}
		
		public static Phone GetPhoneForExtension(List<Phone> phoneList,int extens) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<phoneList.Count;i++) {
				if(phoneList[i].Extension==extens) {
					return phoneList[i];
				}
			}
			return null;
		}

		///<summary>Gets the extension for the employee.  Returns 0 if employee cannot be found.</summary>
		public static int GetExtensionForEmp(long employeeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),employeeNum);
			}
			string command="SELECT Extension FROM phone WHERE EmployeeNum="+POut.Long(employeeNum);
			DataTable table=Db.GetTable(command);
			int extension=0;
			if(table.Rows.Count>0){
				extension=PIn.Int(table.Rows[0]["Extension"].ToString());
			}
			return extension;
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

		///<summary>Sets the employeeName and employeeNum for when someone else logs into another persons computer.</summary>
		public static void SetPhoneForEmp(long empNum,string empName,long extension) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),empNum,empName,extension);
				return;
			}
			if(extension==0) {
				return;
			}
			string command="UPDATE phone SET "
				+"EmployeeName   = '"+POut.String(empName)+"', "
				+"EmployeeNum   = "+POut.Long(empNum)+" "
				+"WHERE Extension = "+POut.Long(extension);
			Db.NonQ(command);
		}

		///<summary>Bitmap can be null.  Computername will override ipAddress.</summary>
		public static void SetWebCamImage(string ipAddress,Bitmap bitmap,string computerName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ipAddress,bitmap,computerName);
				return;
			}
			//if(ipAddress=="") {
			//	return;
			//}
			string command="SELECT * FROM phoneempdefault WHERE ComputerName='"+POut.String(computerName)+"'";
			PhoneEmpDefault ped=Crud.PhoneEmpDefaultCrud.SelectOne(command);
			if(ped!=null) {//we found that computername entered as an override
				command="UPDATE phone SET "
					+"WebCamImage   = '"+POut.Bitmap(bitmap,ImageFormat.Png)+"' "//handles null
					+"WHERE Extension = "+POut.Long(ped.PhoneExt);
				Db.NonQ(command);
				return;
			}
			//there is no computername override entered by staff, so figure out what the extension should be
			int extension=0;
			if(ipAddress.Contains("10.10.1.2")) {
				extension=PIn.Int(ipAddress.ToString().Substring(8))-100;//eg 205-100=105
			}
			if(extension==0) {
				//we don't have a good extension
				return;
			}
			command="UPDATE phone SET "
				+"WebCamImage   = '"+POut.Bitmap(bitmap,ImageFormat.Png)+"' "//handles null
				+"WHERE Extension = "+POut.Long(extension)+" "
				//Example: this is computer .204, and ext 104 has a computername override. Don't update ext 104.
				+"AND NOT EXISTS(SELECT * FROM phoneempdefault WHERE PhoneExt= "+POut.Long(extension)+" "
				+"AND ComputerName!='')";//there exists a computername override for the extension
			Db.NonQ(command);
		}

		public static int GetPhoneExtension(string ipAddress,string computerName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),ipAddress,computerName);
			}
			string command="SELECT * FROM phoneempdefault WHERE ComputerName='"+POut.String(computerName)+"'";
			PhoneEmpDefault ped=Crud.PhoneEmpDefaultCrud.SelectOne(command);
			if(ped!=null) {
				return ped.PhoneExt;
			}
			//there is no computername override entered by staff, so figure out what the extension should be
			int extension=0;
			if(ipAddress.Contains("10.10.1.2")) {
				return PIn.Int(ipAddress.ToString().Substring(8))-100;//eg 205-100=105
			}
			return 0;//couldn't find good extension
		}

		public static void SetScreenshot(int extension,string path,Bitmap bitmap) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),extension);
				return;
			}
			string command="UPDATE phone SET "
				+"ScreenshotPath = '"+POut.String(path)+"', "
				+"ScreenshotImage   = '"+POut.Bitmap(bitmap,ImageFormat.Png)+"' "//handles null
				+"WHERE Extension = "+POut.Long(extension);
			Db.NonQ(command);
		}

		///<summary>Checks the phone.ClockStatus to determine if screenshot should be saved.  Returns extension if true and zero if false.</summary>
		public static int IsOnClock(string ipAddress,string computerName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),ipAddress,computerName);
			}
			string command="SELECT * FROM phoneempdefault WHERE ComputerName='"+POut.String(computerName)+"'";
			PhoneEmpDefault ped=Crud.PhoneEmpDefaultCrud.SelectOne(command);
			if(ped!=null) {//we found that computername entered as an override
				if(ped.IsPrivateScreen) {
					Phones.SetScreenshot(ped.PhoneExt,"",null);
					return 0;
				}
				command="SELECT ClockStatus FROM phone "
					+"WHERE Extension = "+POut.Long(ped.PhoneExt);
				try {
					ClockStatusEnum status=(ClockStatusEnum)Enum.Parse(typeof(ClockStatusEnum),PIn.String(Db.GetScalar(command)));
					if(status==ClockStatusEnum.Available
						|| status==ClockStatusEnum.Backup
						|| status==ClockStatusEnum.OfflineAssist
						|| status==ClockStatusEnum.TeamAssist
						|| status==ClockStatusEnum.Training
						|| status==ClockStatusEnum.WrapUp
						|| status==ClockStatusEnum.NeedsHelp) 
					{
						return ped.PhoneExt;
					}
				}
				catch {
					return 0;
				}
				return 0;//on break or clocked out
			}
			//there is no computername override entered by staff, so figure out what the extension should be
			int extension=0;
			if(ipAddress.Contains("10.10.1.2")) {
				extension=PIn.Int(ipAddress.ToString().Substring(8))-100;//eg 205-100=105
			}
			if(extension==0) {
				//we don't have a good extension
				return 0;
			}
			//make sure the extension isn't overridden with a computername
			//Example: this is computer .204, and ext 104 has a computername override. This computer should not save screenshot on behalf of 104.
			//command="SELECT COUNT(*) FROM phoneempdefault WHERE PhoneExt= "+POut.Long(extension)+" "
			//	+"AND ComputerName!=''";//there exists a computername override for the extension
			//if(Db.GetScalar(command)!="0") {
			//	return 0;
			//}
			command="SELECT * FROM phoneempdefault WHERE PhoneExt= "+POut.Long(extension);
			ped=Crud.PhoneEmpDefaultCrud.SelectOne(command);
			if(ped!=null && ped.IsPrivateScreen) {
				Phones.SetScreenshot(ped.PhoneExt,"",null);
				return 0;
			}
			command="SELECT ClockStatus FROM phone "
					+"WHERE Extension = "+POut.Long(extension);
			try {
				ClockStatusEnum status2=(ClockStatusEnum)Enum.Parse(typeof(ClockStatusEnum),PIn.String(Db.GetScalar(command)));
				if(status2==ClockStatusEnum.Available
					|| status2==ClockStatusEnum.Backup
					|| status2==ClockStatusEnum.OfflineAssist
					|| status2==ClockStatusEnum.TeamAssist
					|| status2==ClockStatusEnum.Training
					|| status2==ClockStatusEnum.WrapUp
					|| status2==ClockStatusEnum.NeedsHelp) 
				{
					return extension;
				}
			}
			catch {
				return 0;
			}
			return 0;//on break or clocked out
		}

		public static void ClearImages() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="UPDATE phone SET WebCamImage=''";
			Db.NonQ(command);
		}

		///<summary>Gets list of TaskNums for new and viewed tasks within the Triage task list.</summary>
		public static List<long> GetTriageTaskNums() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<long>>(MethodBase.GetCurrentMethod());
			}
			List<long> taskNums=new List<long>();
			string command="SELECT * FROM task "
				+"WHERE TaskListNum=1697 "//Triage task list.
				+"AND TaskStatus<>2";//Not done (new or viewed).
			List<Task> triageList=Crud.TaskCrud.SelectMany(command);
			for(int i=0;i<triageList.Count;i++) {
				taskNums.Add(triageList[i].TaskNum);
			}
			return taskNums;
		}

		///<summary>Returns the time of the oldest task within the Triage task list.  Returns 0 if there is no tasks in the list.</summary>
		public static DateTime GetTriageTime() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<DateTime>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT IFNULL(MIN(DateTimeEntry),0) AS triageTime "
				+"FROM task "
				+"WHERE TaskListNum=1697 "//Triage task list.
				+"AND TaskStatus<>2 "//Not done (new or viewed).
				+"AND TaskNum NOT IN (SELECT TaskNum FROM tasknote) "//Not waiting a call back.
				+"LIMIT 1";
			return PIn.DateT(Db.GetScalar(command));
		}

		/// <summary>sorting class used to sort Phone in various ways</summary>
		public class PhoneComparer:IComparer<Phone> {
			private SortBy SortOn=SortBy.name;

			public PhoneComparer(SortBy sortBy) {
				SortOn=sortBy;
			}

			public int Compare(Phone x,Phone y) {
				int retVal=0;
				switch(SortOn) {
					case SortBy.empNum:
						retVal=x.EmployeeNum.CompareTo(y.EmployeeNum);
						break;
					case SortBy.ext:
						retVal=x.Extension.CompareTo(y.Extension);
						break;
					case SortBy.name:
					default:
						retVal=x.EmployeeName.CompareTo(y.EmployeeName);
						break;
				}
				if(retVal==0) {//last name is primary tie breaker
					retVal=x.EmployeeName.CompareTo(y.EmployeeName);
					if(retVal==0) {//extension is seconary tie breaker
						retVal=x.Extension.CompareTo(y.Extension);
					}
				}
				//we got here so our sort was successful
				return retVal;
			}

			public enum SortBy {
				///<summary>0 - By Extension.</summary>
				ext,
				///<summary>1 - By EmployeeNum.</summary>
				empNum,
				///<summary>2 - By Name.</summary>
				name
			}
		}
	}


}