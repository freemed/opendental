using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PlannedAppts{

		///<summary>Gets all planned appt objects for a patient.</summary>
		public static List<PlannedAppt> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PlannedAppt>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM plannedappt WHERE PatNum="+POut.PLong(patNum);
			return new List<PlannedAppt>(DataObjectFactory<PlannedAppt>.CreateObjects(command));
		}

		///<Summary>Gets one plannedAppt from the database.</Summary>
		public static PlannedAppt CreateObject(long plannedApptNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PlannedAppt>(MethodBase.GetCurrentMethod(),plannedApptNum);
			}
			return DataObjectFactory<PlannedAppt>.CreateObject(plannedApptNum);
		}
/*
		public static List<plannedAppt> GetplannedAppts(int[] plannedApptNums){
			Collection<plannedAppt> collectState=DataObjectFactory<plannedAppt>.CreateObjects(plannedApptNums);
			return new List<plannedAppt>(collectState);		
		}*/

		///<summary></summary>
		public static long WriteObject(PlannedAppt plannedAppt) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				plannedAppt.PlannedApptNum=Meth.GetLong(MethodBase.GetCurrentMethod(),plannedAppt);
				return plannedAppt.PlannedApptNum;
			}
			DataObjectFactory<PlannedAppt>.WriteObject(plannedAppt);
			return plannedAppt.PlannedApptNum;
		}

		/*
		///<summary></summary>
		public static void DeleteObject(int plannedApptNum){
			//validate that not already in use.
			string command="SELECT LName,FName FROM patient WHERE plannedApptNum="+POut.PInt(plannedApptNum);
			DataTable table=Db.GetTable(command);
			//int count=PIn.PInt(Db.GetCount(command));
			string pats="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					pats+=", ";
				}
				pats+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lans.g("plannedAppts","plannedAppt is already in use by patient(s). Not allowed to delete. ")+pats);
			}
			DataObjectFactory<plannedAppt>.DeleteObject(plannedApptNum);
		}*/

		//public static void DeleteObject(int plannedApptNum){
		//	DataObjectFactory<plannedAppt>.DeleteObject(plannedApptNum);
		//}

		

	}
}