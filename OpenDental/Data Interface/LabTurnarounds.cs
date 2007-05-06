using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class LabTurnarounds {

		///<summary></summary>
		public static List<LabTurnaround> GetForLab(int laboratoryNum) {
			string command="SELECT * FROM labturnaround WHERE LaboratoryNum="+POut.PInt(laboratoryNum);
			DataTable table=General.GetTable(command);
			List<LabTurnaround> retVal=new List<LabTurnaround>();
			LabTurnaround lab;
			for(int i=0;i<table.Rows.Count;i++) {
				lab=new LabTurnaround();
				lab.LabTurnaroundNum= PIn.PInt   (table.Rows[i][0].ToString());
				lab.LaboratoryNum   = PIn.PInt   (table.Rows[i][1].ToString());
				lab.Description     = PIn.PString(table.Rows[i][2].ToString());
				lab.DaysPublished   = PIn.PInt   (table.Rows[i][3].ToString());
				lab.DaysActual      = PIn.PInt   (table.Rows[i][4].ToString());
				retVal.Add(lab);
			}
			return retVal;
		}

		///<summary>This is used when saving a laboratory.  All labturnarounds for the lab are deleted and recreated.  So the list that's passed in will not have the correct keys set.  The key columns will be ignored.</summary>
		public static void SetForLab(int labNum,List<LabTurnaround> lablist){
			string command="DELETE FROM labturnaround WHERE LaboratoryNum="+POut.PInt(labNum);
			General.NonQ(command);
			for(int i=0;i<lablist.Count;i++){
				lablist[i].LaboratoryNum=labNum;
				Insert(lablist[i]);
			}
		}

		///<summary></summary>
		public static void Insert(LabTurnaround lab){
			if(PrefB.RandomKeys) {
				lab.LabTurnaroundNum=MiscData.GetKey("labturnaround","LabTurnaroundNum");
			}
			string command="INSERT INTO labturnaround (";
			if(PrefB.RandomKeys) {
				command+="LabTurnaroundNum,";
			}
			command+="LaboratoryNum,Description,DaysPublished,DaysActual) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(lab.LabTurnaroundNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (lab.LaboratoryNum)+"', "
				+"'"+POut.PString(lab.Description)+"', "
				+"'"+POut.PInt   (lab.DaysPublished)+"', "
				+"'"+POut.PInt   (lab.DaysActual)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				lab.LabTurnaroundNum=General.NonQ(command,true);
			}
		}

		///<summary>Calculates the due date by adding the number of business days listed.  Adds an additional day for each office holiday.</summary>
		public static DateTime ComputeDueDate(DateTime startDate,int days){
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













