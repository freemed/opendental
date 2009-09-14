using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class LabTurnarounds {

		///<summary></summary>
		public static List<LabTurnaround> GetForLab(long laboratoryNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<LabTurnaround>>(MethodBase.GetCurrentMethod(),laboratoryNum);
			}
			string command="SELECT * FROM labturnaround WHERE LaboratoryNum="+POut.PLong(laboratoryNum);
			DataTable table=Db.GetTable(command);
			List<LabTurnaround> retVal=new List<LabTurnaround>();
			LabTurnaround lab;
			for(int i=0;i<table.Rows.Count;i++) {
				lab=new LabTurnaround();
				lab.LabTurnaroundNum= PIn.PLong   (table.Rows[i][0].ToString());
				lab.LaboratoryNum   = PIn.PLong   (table.Rows[i][1].ToString());
				lab.Description     = PIn.PString(table.Rows[i][2].ToString());
				lab.DaysPublished   = PIn.PInt   (table.Rows[i][3].ToString());
				lab.DaysActual      = PIn.PInt   (table.Rows[i][4].ToString());
				retVal.Add(lab);
			}
			return retVal;
		}

		///<summary>This is used when saving a laboratory.  All labturnarounds for the lab are deleted and recreated.  So the list that's passed in will not have the correct keys set.  The key columns will be ignored.</summary>
		public static void SetForLab(long labNum,List<LabTurnaround> lablist) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),labNum,lablist);
				return;
			}
			string command="DELETE FROM labturnaround WHERE LaboratoryNum="+POut.PLong(labNum);
			Db.NonQ(command);
			for(int i=0;i<lablist.Count;i++){
				lablist[i].LaboratoryNum=labNum;
				Insert(lablist[i]);
			}
		}

		///<summary></summary>
		public static long Insert(LabTurnaround lab) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				lab.LabTurnaroundNum=Meth.GetInt(MethodBase.GetCurrentMethod(),lab);
				return lab.LabTurnaroundNum;
			}
			if(PrefC.RandomKeys) {
				lab.LabTurnaroundNum=ReplicationServers.GetKey("labturnaround","LabTurnaroundNum");
			}
			string command="INSERT INTO labturnaround (";
			if(PrefC.RandomKeys) {
				command+="LabTurnaroundNum,";
			}
			command+="LaboratoryNum,Description,DaysPublished,DaysActual) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PLong(lab.LabTurnaroundNum)+"', ";
			}
			command+=
				 "'"+POut.PLong   (lab.LaboratoryNum)+"', "
				+"'"+POut.PString(lab.Description)+"', "
				+"'"+POut.PLong   (lab.DaysPublished)+"', "
				+"'"+POut.PLong   (lab.DaysActual)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				lab.LabTurnaroundNum=Db.NonQ(command,true);
			}
			return lab.LabTurnaroundNum;
		}

		///<summary>Calculates the due date by adding the number of business days listed.  Adds an additional day for each office holiday.</summary>
		public static DateTime ComputeDueDate(DateTime startDate,int days){
			//No need to check RemotingRole; no call to db.
			DateTime date=startDate;
			int counter=0;
			while(counter<days){
				date=date.AddDays(1);
				if(date.DayOfWeek==DayOfWeek.Saturday || date.DayOfWeek==DayOfWeek.Sunday){
					continue;
				}
				if(Schedules.DateIsHoliday(date)){
					continue;
				}
				counter++;
			}
			return date+TimeSpan.FromHours(17);//always due at 5pm on day specified.
		}
	
		

	}
	


}













