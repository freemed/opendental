using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDentBusiness {
	public class DisplayFields {

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command = "SELECT * FROM displayfield ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="DisplayField";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			DisplayFieldC.Listt=new List<DisplayField>();
			DisplayField field;
			for(int i=0;i<table.Rows.Count;i++){
				field = new DisplayField();
				field.DisplayFieldNum = PIn.Long   (table.Rows[i][0].ToString());
				field.InternalName    = PIn.String(table.Rows[i][1].ToString());
				field.ItemOrder       = PIn.Int   (table.Rows[i][2].ToString());
				field.Description     = PIn.String(table.Rows[i][3].ToString());
				field.ColumnWidth     = PIn.Int   (table.Rows[i][4].ToString());
				field.Category        = (DisplayFieldCategory)PIn.Long(table.Rows[i][5].ToString());
				field.ChartViewNum	  = PIn.Long(table.Rows[i][6].ToString());
				DisplayFieldC.Listt.Add(field);
			}
		}

		///<summary></summary>
		public static long Insert(DisplayField field) {
			return Crud.DisplayFieldCrud.Insert(field);
		}

		/*
		///<summary></summary>
		public static void Update(DisplayField field) {			
			string command="UPDATE displayfield SET "
			+"DisplayFieldName = '"+POut.PString(DisplayField.DisplayFieldName)+"', "
			+"ControlsToInc = '"+POut.PString(DisplayField.ControlsToInc)+"' "
			+"WHERE DisplayFieldNum = '"+POut.PInt(DisplayField.DisplayFieldNum)+"'";
			Db.NonQ(command);
		}
		*/

		///<summary></summary>
		public static void DeleteForChartView(long chartViewNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),chartViewNum);
				return;
			}
			string command="DELETE FROM displayfield WHERE ChartViewNum = "+POut.Long(chartViewNum);
			Db.NonQ(command);
		}

		///<Summary>Returns an ordered list for just one category</Summary>
		public static List<DisplayField> GetForCategory(DisplayFieldCategory category){
			//No need to check RemotingRole; no call to db.
			List<DisplayField> retVal=new List<DisplayField>();
			for(int i=0;i<DisplayFieldC.Listt.Count;i++){
				if(DisplayFieldC.Listt[i].Category==category){
					retVal.Add(DisplayFieldC.Listt[i].Copy());
				}
			}
			if(retVal.Count==0) {//default
				return DisplayFields.GetDefaultList(category);
			}
			return retVal;
		}

		///<Summary>Returns an ordered list for just one chart view</Summary>
		public static List<DisplayField> GetForChartView(long ChartViewNum) {
			//No need to check RemotingRole; no call to db.
			List<DisplayField> retVal=new List<DisplayField>();
			for(int i=0;i<DisplayFieldC.Listt.Count;i++) {
				if(DisplayFieldC.Listt[i].ChartViewNum==ChartViewNum && DisplayFieldC.Listt[i].Category==DisplayFieldCategory.None) {
					retVal.Add(DisplayFieldC.Listt[i].Copy());
				}
			}
			if(retVal.Count==0) {//default
				return DisplayFields.GetDefaultList(DisplayFieldCategory.None);
			}
			return retVal;
		}

		public static List<DisplayField> GetDefaultList(DisplayFieldCategory category){
			//No need to check RemotingRole; no call to db.
			List<DisplayField> list=new List<DisplayField>();
			if(category==DisplayFieldCategory.None) {
				list.Add(new DisplayField("Date",67,category));
				//list.Add(new DisplayField("Time",40));
				list.Add(new DisplayField("Th",27,category));
				list.Add(new DisplayField("Surf",40,category));
				list.Add(new DisplayField("Dx",28,category));
				list.Add(new DisplayField("Description",218,category));
				list.Add(new DisplayField("Stat",25,category));
				list.Add(new DisplayField("Prov",42,category));
				list.Add(new DisplayField("Amount",48,category));
				list.Add(new DisplayField("ADA Code",62,category));
				list.Add(new DisplayField("User",62,category));
				list.Add(new DisplayField("Signed",55,category));
				//list.Add(new DisplayField("Priority",65,category));
				//list.Add(new DisplayField("Date TP",67,category));
				//list.Add(new DisplayField("Date Entry",67,category));
				//if(Programs.UsingOrion){
					//list.Add(new DisplayField("DPC",33,category));
					//list.Add(new DisplayField("Schedule By",72,category));
					//list.Add(new DisplayField("Stop Clock",67,category));
					//list.Add(new DisplayField("Stat 2",36,category));
					//list.Add(new DisplayField("On Call",45,category));
					//list.Add(new DisplayField("Effective Comm",90,category));
					//list.Add(new DisplayField("End Time",56,category));
					//list.Add(new DisplayField("Quadrant",55,category));
				//}
				
			}
			else if(category==DisplayFieldCategory.PatientSelect){
				list.Add(new DisplayField("LastName",75,category));
				list.Add(new DisplayField("First Name",75,category));
				//list.Add(new DisplayField("MI",25,category));
				list.Add(new DisplayField("Pref Name",60,category));
				list.Add(new DisplayField("Age",30,category));
				list.Add(new DisplayField("SSN",65,category));
				list.Add(new DisplayField("Hm Phone",90,category));
				list.Add(new DisplayField("Wk Phone",90,category));
				list.Add(new DisplayField("PatNum",80,category));
				//list.Add(new DisplayField("ChartNum",60,category));
				list.Add(new DisplayField("Address",100,category));
				list.Add(new DisplayField("Status",65,category));
				//list.Add(new DisplayField("Bill Type",90,category));
				//list.Add(new DisplayField("City",80,category));
				//list.Add(new DisplayField("State",55,category));
				//list.Add(new DisplayField("Pri Prov",85,category));
				//list.Add(new DisplayField("Birthdate",70,category));
				//list.Add(new DisplayField("Site",90,category));
			}
			else if(category==DisplayFieldCategory.PatientInformation){
				list.Add(new DisplayField("Last",0,category));
				list.Add(new DisplayField("First",0,category));
				list.Add(new DisplayField("Middle",0,category));
				list.Add(new DisplayField("Preferred",0,category));
				list.Add(new DisplayField("Title",0,category));
				list.Add(new DisplayField("Salutation",0,category));
				list.Add(new DisplayField("Status",0,category));
				list.Add(new DisplayField("Gender",0,category));
				list.Add(new DisplayField("Position",0,category));
				list.Add(new DisplayField("Birthdate",0,category));
				list.Add(new DisplayField("Age",0,category));
				list.Add(new DisplayField("SS#",0,category));
				list.Add(new DisplayField("Address",0,category));
				list.Add(new DisplayField("Address2",0,category));
				list.Add(new DisplayField("City",0,category));
				list.Add(new DisplayField("State",0,category));
				list.Add(new DisplayField("Zip",0,category));
				list.Add(new DisplayField("Hm Phone",0,category));
				list.Add(new DisplayField("Wk Phone",0,category));
				list.Add(new DisplayField("Wireless Ph",0,category));
				list.Add(new DisplayField("E-mail",0,category));
				list.Add(new DisplayField("Contact Method",0,category));
				list.Add(new DisplayField("ABC0",0,category));
				//list.Add(new DisplayField("Chart Num",0,category));
				list.Add(new DisplayField("Billing Type",0,category));
				//list.Add(new DisplayField("Ward",0,category));
				//list.Add(new DisplayField("AdmitDate",0,category));
				list.Add(new DisplayField("Primary Provider",0,category));
				list.Add(new DisplayField("Sec. Provider",0,category));
				list.Add(new DisplayField("Language",0,category));
				//list.Add(new DisplayField("Clinic",0,category));
				//list.Add(new DisplayField("ResponsParty",0,category));
				list.Add(new DisplayField("Referrals",0,category));
				list.Add(new DisplayField("Addr/Ph Note",0,category));
				list.Add(new DisplayField("PatFields",0,category));
				//list.Add(new DisplayField("Guardians",0,category));
			}
			else if(category==DisplayFieldCategory.AccountModule) {
				list.Add(new DisplayField("Date",65,category));
				list.Add(new DisplayField("Patient",100,category));
				list.Add(new DisplayField("Prov",40,category));
				//list.Add(new DisplayField("Clinic",50,category));
				list.Add(new DisplayField("Code",46,category));
				list.Add(new DisplayField("Tth",26,category));
				list.Add(new DisplayField("Description",270,category));
				list.Add(new DisplayField("Charges",60,category));
				list.Add(new DisplayField("Credits",60,category));
				list.Add(new DisplayField("Balance",60,category));
			}
			else if(category==DisplayFieldCategory.RecallList) {
				list.Add(new DisplayField("Due Date",75,category));
				list.Add(new DisplayField("Patient",120,category));
				list.Add(new DisplayField("Age",30,category));
				list.Add(new DisplayField("Type",60,category));
				list.Add(new DisplayField("Interval",50,category));
				list.Add(new DisplayField("#Remind",55,category));
				list.Add(new DisplayField("LastRemind",75,category));
				list.Add(new DisplayField("Contact",120,category));
				list.Add(new DisplayField("Status",130,category));
				list.Add(new DisplayField("Note",215,category));
				//list.Add(new DisplayField("BillingType",100,category));
			}
			else if(category==DisplayFieldCategory.ChartPatientInformation) {
				list.Add(new DisplayField("Age",0,category));
				list.Add(new DisplayField("ABC0",0,category));
				list.Add(new DisplayField("Billing Type",0,category));
				list.Add(new DisplayField("Referred From",0,category));
				list.Add(new DisplayField("Date First Visit",0,category));
				list.Add(new DisplayField("Prov. (Pri, Sec)",0,category));
				list.Add(new DisplayField("Pri Ins",0,category));
				list.Add(new DisplayField("Sec Ins",0,category));
				if(PrefC.GetBool(PrefName.DistributorKey)) {
					list.Add(new DisplayField("Registration Keys",0,category));
				}
				//different default list for eCW:
				if(!Programs.UsingEcwTight()) {
					list.Add(new DisplayField("Premedicate",0,category));
					list.Add(new DisplayField("Diseases",0,category));
					list.Add(new DisplayField("Med Urgent",0,category));
					list.Add(new DisplayField("Medical Summary",0,category));
					list.Add(new DisplayField("Service Notes",0,category));
					list.Add(new DisplayField("Medications",0,category));
				}
				//list.Add(new DisplayField("PatFields",0,category));
			}
			else if(category==DisplayFieldCategory.ProcedureGroupNote) {
				list.Add(new DisplayField("Date",67,category));
				list.Add(new DisplayField("Th",27,category));
				list.Add(new DisplayField("Surf",40,category));
				list.Add(new DisplayField("Description",203,category));
				list.Add(new DisplayField("Stat",25,category));
				list.Add(new DisplayField("Prov",42,category));
				list.Add(new DisplayField("Amount",48,category));
				list.Add(new DisplayField("ADA Code",62,category));
				//if(Programs.UsingOrion){
				//  list.Add(new DisplayField("Stat 2",36,category));
				//  list.Add(new DisplayField("On Call",45,category));
				//  list.Add(new DisplayField("Effective Comm",90,category));
				//  list.Add(new DisplayField("Repair",45,category));
				//}
			}
			return list;
		}

		public static List<DisplayField> GetAllAvailableList(DisplayFieldCategory category){
			//No need to check RemotingRole; no call to db.
			List<DisplayField> list=new List<DisplayField>();
			if(category==DisplayFieldCategory.None) {
				list.Add(new DisplayField("Date",67,category));
				list.Add(new DisplayField("Time",40,category));
				list.Add(new DisplayField("Th",27,category));
				list.Add(new DisplayField("Surf",40,category));
				list.Add(new DisplayField("Dx",28,category));
				list.Add(new DisplayField("Description",218,category));
				list.Add(new DisplayField("Stat",25,category));
				list.Add(new DisplayField("Prov",42,category));
				list.Add(new DisplayField("Amount",48,category));
				list.Add(new DisplayField("ADA Code",62,category));
				list.Add(new DisplayField("User",62,category));
				list.Add(new DisplayField("Signed",55,category));
				list.Add(new DisplayField("Priority",44,category));
				list.Add(new DisplayField("Date TP",67,category));
				list.Add(new DisplayField("Date Entry",67,category));
				if(Programs.UsingOrion){
					list.Add(new DisplayField("DPC",33,category));
					list.Add(new DisplayField("Schedule By",72,category));
					list.Add(new DisplayField("Stop Clock",67,category));
					list.Add(new DisplayField("Stat 2",36,category));
					list.Add(new DisplayField("On Call",45,category));
					list.Add(new DisplayField("Effective Comm",90,category));
					list.Add(new DisplayField("End Time",56,category));//not visible unless orion
					list.Add(new DisplayField("Quadrant",55,category));//behavior is specific to orion
				}
			}
			else if(category==DisplayFieldCategory.PatientSelect){
				list.Add(new DisplayField("LastName",75,category));
				list.Add(new DisplayField("First Name",75,category));
				list.Add(new DisplayField("MI",25,category));
				list.Add(new DisplayField("Pref Name",60,category));
				list.Add(new DisplayField("Age",30,category));
				list.Add(new DisplayField("SSN",65,category));
				list.Add(new DisplayField("Hm Phone",90,category));
				list.Add(new DisplayField("Wk Phone",90,category));
				list.Add(new DisplayField("PatNum",80,category));
				list.Add(new DisplayField("ChartNum",60,category));
				list.Add(new DisplayField("Address",100,category));
				list.Add(new DisplayField("Status",65,category));
				list.Add(new DisplayField("Bill Type",90,category));
				list.Add(new DisplayField("City",80,category));
				list.Add(new DisplayField("State",55,category));
				list.Add(new DisplayField("Pri Prov",85,category));
				list.Add(new DisplayField("Birthdate",70,category));
				list.Add(new DisplayField("Site",90,category));
			}
			else if(category==DisplayFieldCategory.PatientInformation){
				list.Add(new DisplayField("Last",0,category));
				list.Add(new DisplayField("First",0,category));
				list.Add(new DisplayField("Middle",0,category));
				list.Add(new DisplayField("Preferred",0,category));
				list.Add(new DisplayField("Title",0,category));
				list.Add(new DisplayField("Salutation",0,category));
				list.Add(new DisplayField("Status",0,category));
				list.Add(new DisplayField("Gender",0,category));
				list.Add(new DisplayField("Position",0,category));
				list.Add(new DisplayField("Birthdate",0,category));
				list.Add(new DisplayField("Age",0,category));
				list.Add(new DisplayField("SS#",0,category));
				list.Add(new DisplayField("Address",0,category));
				list.Add(new DisplayField("Address2",0,category));
				list.Add(new DisplayField("City",0,category));
				list.Add(new DisplayField("State",0,category));
				list.Add(new DisplayField("Zip",0,category));
				list.Add(new DisplayField("Hm Phone",0,category));
				list.Add(new DisplayField("Wk Phone",0,category));
				list.Add(new DisplayField("Wireless Ph",0,category));
				list.Add(new DisplayField("E-mail",0,category));
				list.Add(new DisplayField("Contact Method",0,category));
				list.Add(new DisplayField("ABC0",0,category));
				list.Add(new DisplayField("Chart Num",0,category));
				list.Add(new DisplayField("Billing Type",0,category));
				list.Add(new DisplayField("Ward",0,category));
				list.Add(new DisplayField("AdmitDate",0,category));
				list.Add(new DisplayField("Primary Provider",0,category));
				list.Add(new DisplayField("Sec. Provider",0,category));
				list.Add(new DisplayField("Language",0,category));
				list.Add(new DisplayField("Clinic",0,category));
				list.Add(new DisplayField("ResponsParty",0,category));
				list.Add(new DisplayField("Referrals",0,category));
				list.Add(new DisplayField("Addr/Ph Note",0,category));
				list.Add(new DisplayField("PatFields",0,category));
				list.Add(new DisplayField("Guardians",0,category));
			}
			else if(category==DisplayFieldCategory.AccountModule){
				list.Add(new DisplayField("Date",65,category));
				list.Add(new DisplayField("Patient",100,category));
				list.Add(new DisplayField("Prov",40,category));
				list.Add(new DisplayField("Clinic",50,category));
				list.Add(new DisplayField("Code",46,category));
				list.Add(new DisplayField("Tth",26,category));
				list.Add(new DisplayField("Description",270,category));
				list.Add(new DisplayField("Charges",60,category));
				list.Add(new DisplayField("Credits",60,category));
				list.Add(new DisplayField("Balance",60,category));
			}
			else if(category==DisplayFieldCategory.RecallList) {
				list.Add(new DisplayField("Due Date",75,category));
				list.Add(new DisplayField("Patient",120,category));
				list.Add(new DisplayField("Age",30,category));
				list.Add(new DisplayField("Type",60,category));
				list.Add(new DisplayField("Interval",50,category));
				list.Add(new DisplayField("#Remind",55,category));
				list.Add(new DisplayField("LastRemind",75,category));
				list.Add(new DisplayField("Contact",120,category));
				list.Add(new DisplayField("Status",130,category));
				list.Add(new DisplayField("Note",215,category));
				list.Add(new DisplayField("BillingType",100,category));
			}
			else if(category==DisplayFieldCategory.ChartPatientInformation) {
				list.Add(new DisplayField("Age",0,category));
				list.Add(new DisplayField("ABC0",0,category));
				list.Add(new DisplayField("Billing Type",0,category));
				list.Add(new DisplayField("Referred From",0,category));
				list.Add(new DisplayField("Date First Visit",0,category));
				list.Add(new DisplayField("Prov. (Pri, Sec)",0,category));
				list.Add(new DisplayField("Pri Ins",0,category));
				list.Add(new DisplayField("Sec Ins",0,category));
				if(PrefC.GetBool(PrefName.DistributorKey)) {
					list.Add(new DisplayField("Registration Keys",0,category));
				}
				list.Add(new DisplayField("Premedicate",0,category));
				list.Add(new DisplayField("Diseases",0,category));
				list.Add(new DisplayField("Med Urgent",0,category));
				list.Add(new DisplayField("Medical Summary",0,category));
				list.Add(new DisplayField("Service Notes",0,category));
				list.Add(new DisplayField("Medications",0,category));
				list.Add(new DisplayField("PatFields",0,category));
			}
			else if(category==DisplayFieldCategory.ProcedureGroupNote) {
				list.Add(new DisplayField("Date",67,category));
				list.Add(new DisplayField("Th",27,category));
				list.Add(new DisplayField("Surf",40,category));
				list.Add(new DisplayField("Description",218,category));
				list.Add(new DisplayField("Stat",25,category));
				list.Add(new DisplayField("Prov",42,category));
				list.Add(new DisplayField("Amount",48,category));
				list.Add(new DisplayField("ADA Code",62,category));
				if(Programs.UsingOrion){
					list.Add(new DisplayField("Stat 2",36,category));
					list.Add(new DisplayField("On Call",45,category));
					list.Add(new DisplayField("Effective Comm",90,category));
					list.Add(new DisplayField("Repair",45,category));
				}
			}
			return list;
		}

		public static void SaveListForCategory(List<DisplayField> ListShowing,DisplayFieldCategory category){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ListShowing,category);
				return;
			}
			bool isDefault=true;
			List<DisplayField> defaultList=GetDefaultList(category);
			if(ListShowing.Count!=defaultList.Count){
				isDefault=false;
			}
			else{
				for(int i=0;i<ListShowing.Count;i++){
					if(ListShowing[i].Description!=""){
						isDefault=false;
						break;
					}
					if(ListShowing[i].InternalName!=defaultList[i].InternalName){
						isDefault=false;
						break;
					}
					if(ListShowing[i].ColumnWidth!=defaultList[i].ColumnWidth) {
						isDefault=false;
						break;
					}
				}
			}
			string command="DELETE FROM displayfield WHERE Category="+POut.Long((int)category);
			Db.NonQ(command);
			if(isDefault){
				return;
			}
			for(int i=0;i<ListShowing.Count;i++){
				ListShowing[i].ItemOrder=i;
				Insert(ListShowing[i]);
			}
		}

		public static void SaveListForChartView(List<DisplayField> ListShowing,long ChartViewNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ListShowing,ChartViewNum);
				return;
			}
			//Odds are, that if they are creating a custom view, that the fields are not default. If they are default, this code still works.
			string command="DELETE FROM displayfield WHERE ChartViewNum="+POut.Long((long)ChartViewNum);
			Db.NonQ(command);
			for(int i=0;i<ListShowing.Count;i++) {
				ListShowing[i].ItemOrder=i;
				ListShowing[i].ChartViewNum=ChartViewNum;
				Insert(ListShowing[i]);
			}
		}
		

	}
}
