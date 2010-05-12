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
			ListLong=new Employee[table.Rows.Count];
			ArrayList tempList=new ArrayList();
			//Employee temp;
			for(int i=0;i<table.Rows.Count;i++) {
				ListLong[i]=new Employee();
				ListLong[i].EmployeeNum=PIn.Long(table.Rows[i][0].ToString());
				ListLong[i].LName=PIn.String(table.Rows[i][1].ToString());
				ListLong[i].FName=PIn.String(table.Rows[i][2].ToString());
				ListLong[i].MiddleI=PIn.String(table.Rows[i][3].ToString());
				ListLong[i].IsHidden=PIn.Bool(table.Rows[i][4].ToString());
				ListLong[i].ClockStatus=PIn.String(table.Rows[i][5].ToString());
				ListLong[i].PhoneExt=PIn.Long(table.Rows[i][6].ToString());
				if(!ListLong[i].IsHidden) {
					tempList.Add(ListLong[i]);
				}
			}
			ListShort=new Employee[tempList.Count];
			for(int i=0;i<tempList.Count;i++) {
				ListShort[i]=(Employee)tempList[i];
			}
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
			string command="UPDATE employee SET " 
				+ "lname = '"       +POut.String(Cur.LName)+"' "
				+ ",fname = '"      +POut.String(Cur.FName)+"' "
				+ ",middlei = '"    +POut.String(Cur.MiddleI)+"' "
				+ ",ishidden = '"   +POut.Bool  (Cur.IsHidden)+"' "
				+ ",ClockStatus = '"+POut.String(Cur.ClockStatus)+"' "
				+ ",PhoneExt = '"   +POut.Long   (Cur.PhoneExt)+"' "
				+"WHERE EmployeeNum = '"+POut.Long(Cur.EmployeeNum)+"'";
			//MessageBox.Show(string command);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(Employee Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.EmployeeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.EmployeeNum;
			}
			if(Cur.LName=="" && Cur.FName=="") {
				throw new ApplicationException(Lans.g("FormEmployeeEdit","Must include either first name or last name"));
			}
			if(PrefC.RandomKeys) {
				Cur.EmployeeNum=ReplicationServers.GetKey("employee","EmployeeNum");
			}
			string command="INSERT INTO employee (";
			if(PrefC.RandomKeys) {
				command+="EmployeeNum,";
			}
			command+="lname,fname,middlei,ishidden"
				+",ClockStatus,PhoneExt) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(Cur.EmployeeNum)+", ";
			}
			command+=
				 "'"+POut.String(Cur.LName)+"', "
				+"'"+POut.String(Cur.FName)+"', "
				+"'"+POut.String(Cur.MiddleI)+"', "
				+"'"+POut.Bool  (Cur.IsHidden)+"', "
				+"'"+POut.String(Cur.ClockStatus)+"', "
				+"'"+POut.Long   (Cur.PhoneExt)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				Cur.EmployeeNum=Db.NonQ(command,true);
			}
			return Cur.EmployeeNum;
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

		public static DataTable GetPhoneTable(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM phone";
			try{
				return Db.GetTable(command);
			}
			catch{
				return new DataTable();
			}
		}

		///<summary>this code is similar to code in the phone tracking server.  But here, we frequently only change clockStatus and ColorBar by setting employeeNum=-1.  If employeeNum is not -1, then EmployeeName also gets set.</summary>
		public static void SetPhoneStatus(string clockStatus,int extens){
			SetPhoneStatus(clockStatus,extens,-1);
		}

		///<summary>this code is similar to code in the phone tracking server.  But here, we frequently only change clockStatus and ColorBar by setting employeeNum=-1.  If employeeNum is not -1, then EmployeeName also gets set.  If employeeNum==0, then clears employee from that row.</summary>
		public static void SetPhoneStatus(string clockStatus,int extens,long employeeNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clockStatus,extens);
				return;
			}
			//
			string command=@"SELECT EmployeeNum,Description,EmployeeName,
				IFNULL(IsAvailable,1) isAvail, COUNT(IsAvailable) overridden
				FROM phone
				LEFT JOIN phoneoverride ON phone.Extension=phoneoverride.Extension
				WHERE phone.Extension="+POut.Long(extens)
				+" GROUP BY phone.Extension";
			DataTable tablePhone=Db.GetTable(command);
			if(tablePhone.Rows.Count==0){
				return;
			}
			long empNum=PIn.Long(tablePhone.Rows[0]["EmployeeNum"].ToString());
			string empName=PIn.String(tablePhone.Rows[0]["EmployeeName"].ToString());
			if(employeeNum==0){
				empNum=0;
				empName="";
			}
			else if(employeeNum>0){
				empNum=employeeNum;
				empName=Employees.GetEmp(empNum).FName;
			}
			bool isAvailable=PIn.Bool(tablePhone.Rows[0]["isAvail"].ToString());
			bool overridden=PIn.Bool(tablePhone.Rows[0]["overridden"].ToString());
			bool isInUse=false;
			if(tablePhone.Rows[0]["Description"].ToString()=="In use"){
				isInUse=true;
			}
			Color colorBar=GetColorBar(clockStatus,overridden,isAvailable,empNum,isInUse);
			command="UPDATE phone SET ClockStatus='"+POut.String(clockStatus)+"', "
				+"ColorBar="+colorBar.ToArgb().ToString()+", "
				+"EmployeeNum="+POut.Long(empNum)+", "
				+"EmployeeName='"+POut.String(empName)+"' "
				+"WHERE Extension="+extens;
			Db.NonQ(command);
		}

		///<summary>Used when clocking in and out, but not through the phone grid.  Keeps the phone grid current. Handles situations where employee is listed on two different extensions.</summary>
		public static void SetPhoneClockStatus(long employeeNum,string clockStatus){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),employeeNum,clockStatus);
				return;
			}
			string command="SELECT Extension,ClockStatus FROM phone WHERE employeeNum="+POut.Long(employeeNum);
			DataTable table=Db.GetTable(command);
			int extension;
			string curClockStatus;
			for(int i=0;i<table.Rows.Count;i++){
				extension=PIn.Int(table.Rows[i]["Extension"].ToString());
				curClockStatus=table.Rows[i]["ClockStatus"].ToString();
				if(curClockStatus=="Unavailable"){
					continue;//don't change "Unavailable" to anything else.
				}
				SetPhoneStatus(clockStatus,extension);
			}
		}

		private static Color GetColorBar(string clockStatus,bool overridden,bool isAvailable,long empNum,bool isInUse){
			//No need to check RemotingRole; no call to db.
			//there is an exact duplicate of this function in the phone server.
			Color colorBar=Color.White;
			if(empNum==0){
				//no colors
			}
			else if(overridden && !isAvailable){
				//no colors
			}
			else if(!overridden
				&& (empNum==22//jordan
				|| empNum==15//derek
				|| empNum==18//spike
				|| empNum==17//nathan
				))
			{
				//no colors
			}
			else if(isInUse){
				colorBar=Color.Salmon;
			}
			//the rest are for idle:
			else if(clockStatus=="Available"){
				colorBar=Color.FromArgb(153,220,153);//green
			}
			//"Unavailable" already handled above
			else if(clockStatus=="off"){
				//colorText=Color.Gray;
				//no colorBar
			}
			else if(clockStatus=="Break"){
				//no color
			}
			else if(clockStatus=="Training"
				|| clockStatus=="TeamAssist"
				|| clockStatus=="WrapUp"
				|| clockStatus=="OfflineAssist")
			{
				colorBar=Color.FromArgb(255,255,145);//yellow
			}
			else if(clockStatus=="Backup") {
				colorBar=Color.FromArgb(217,255,217);//pale green
			}
			return colorBar;
		}

		///<summary>For a given date, gets a list of dateTimes of missed calls.  Gets directly from the Asterisk database, hard-coded.</summary>
		public static List<DateTime> GetAsteriskMissedCalls(DateTime date){
			DataConnection dcon=new DataConnection("Asterisk","asteriskcdrdb","opendentalui","secret",DatabaseType.MySql);
			string command="SELECT calldate FROM cdr WHERE DATE(calldate) = "+POut.Date(date)+" "
				+"AND (dcontext='ext-group' OR dcontext='ext-local') AND dst='vmu998'";
			List<DateTime> retVal=new List<DateTime>();
			DataTable table=dcon.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				retVal.Add(PIn.DateT(table.Rows[i][0].ToString()));
			}
			return retVal;
		}


	}

	

	
	

}













