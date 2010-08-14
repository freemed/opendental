﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Phones{
		public static Color ColorRed=Color.Salmon;
		public static Color ColorGreen=Color.FromArgb(153,220,153);
		public static Color ColorYellow=Color.FromArgb(255,255,145);
		public static Color ColorPaleGreen=Color.FromArgb(217,255,217);

		public static DataTable GetPhoneTable() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM phone ORDER BY Extension";
			try {
				return Db.GetTable(command);
			}
			catch {
				return new DataTable();
			}
		}

		///<summary>Converts from string to enum and also handles conversion of Working to Available</summary>
		public static ClockStatusEnum GetClockStatusFromEmp(string empClockStatus) {
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
			SetPhoneStatus(clockStatus,extens,-1);
		}

		///<summary>this code is similar to code in the phone tracking server.  But here, we frequently only change clockStatus and ColorBar by setting employeeNum=-1.  If employeeNum is not -1, then EmployeeName also gets set.  If employeeNum==0, then clears employee from that row.</summary>
		public static void SetPhoneStatus(ClockStatusEnum clockStatus,int extens,long employeeNum) {
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

		/*No longer allowed to clock in and out anyplace but phone grid
		///<summary>Used when clocking in and out, but not through the phone grid.  Keeps the phone grid current. Handles situations where employee is listed on two different extensions.</summary>
		public static void SetPhoneClockStatus(long employeeNum,ClockStatusEnum clockStatus) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),employeeNum,clockStatus);
				return;
			}
			string command="SELECT Extension,ClockStatus FROM phone WHERE employeeNum="+POut.Long(employeeNum);
			DataTable table=Db.GetTable(command);
			int extension;
			string curClockStatus;
			for(int i=0;i<table.Rows.Count;i++) {
				extension=PIn.Int(table.Rows[i]["Extension"].ToString());
				curClockStatus=table.Rows[i]["ClockStatus"].ToString();
				if(curClockStatus=="Unavailable") {
					continue;//don't change "Unavailable" to anything else.
				}
				SetPhoneStatus(clockStatus,extension);
			}
		}*/

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


	}
}