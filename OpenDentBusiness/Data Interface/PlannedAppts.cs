using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PlannedAppts{
		//<summary></summary>
		//public static List<PlannedAppt> Refresh(){
			//string c="SELECT * from plannedAppt ORDER BY Description";
			
		//}

		/*
		///<Summary>Gets one plannedAppt from the database.</Summary>
		public static plannedAppt CreateObject(int plannedApptNum){
			return DataObjectFactory<plannedAppt>.CreateObject(plannedApptNum);
		}

		public static List<plannedAppt> GetplannedAppts(int[] plannedApptNums){
			Collection<plannedAppt> collectState=DataObjectFactory<plannedAppt>.CreateObjects(plannedApptNums);
			return new List<plannedAppt>(collectState);		
		}*/

		///<summary></summary>
		public static void WriteObject(PlannedAppt plannedAppt){
			DataObjectFactory<PlannedAppt>.WriteObject(plannedAppt);
		}

		/*
		///<summary></summary>
		public static void DeleteObject(int plannedApptNum){
			//validate that not already in use.
			string command="SELECT LName,FName FROM patient WHERE plannedApptNum="+POut.PInt(plannedApptNum);
			DataTable table=General.GetTable(command);
			//int count=PIn.PInt(General.GetCount(command));
			string pats="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					pats+=", ";
				}
				pats+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lan.g("plannedAppts","plannedAppt is already in use by patient(s). Not allowed to delete. ")+pats);
			}
			DataObjectFactory<plannedAppt>.DeleteObject(plannedApptNum);
		}*/

		//public static void DeleteObject(int plannedApptNum){
		//	DataObjectFactory<plannedAppt>.DeleteObject(plannedApptNum);
		//}

		

	}
}