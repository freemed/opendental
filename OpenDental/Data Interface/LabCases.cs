using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class LabCases {

		///<summary>Gets a filtered list of labcases.</summary>
		public static DataTable Refresh() {
			/*string command="SELECT * FROM LabCase ORDER BY Description";
			DataTable table=General.GetTable(command);
			List<LabCase> ListLabs=new List<LabCase>();
			LabCase lab;
			for(int i=0;i<table.Rows.Count;i++) {
				lab=new LabCase();
				lab.LabCaseNum= PIn.PInt   (table.Rows[i][0].ToString());
				lab.Description  = PIn.PString(table.Rows[i][1].ToString());
				lab.Phone        = PIn.PString(table.Rows[i][2].ToString());
				lab.Notes        = PIn.PString(table.Rows[i][3].ToString());
				lab.LabSlip      = PIn.PString(table.Rows[i][4].ToString());
				ListLabs.Add(lab);
			}
			return ListLabs;*/
			return null;
		}

		/*
		///<summary></summary>
		public static void Insert(LabCase lab){
			string command= "INSERT INTO LabCase (PatNum,LaboratoryNum,AptNum,PlannedAptNum) VALUES("
				+"'"+POut.PString(lab.Description)+"', "
				+"'"+POut.PString(lab.Phone)+"', "
				+"'"+POut.PString(lab.Notes)+"', "
				+"'"+POut.PString(lab.LabSlip)+"')";

			l.LabCaseNum=LabCaseNum;
			l.PatNum=PatNum;
			l.LaboratoryNum=LaboratoryNum;
			l.AptNum=AptNum;
			l.PlannedAptNum=PlannedAptNum;
			l.DateTimeDue=DateTimeDue;
			l.DateTimeCreated=DateTimeCreated;
			l.DateTimeSent=DateTimeSent;
			l.DateTimeRecd=DateTimeRecd;
			l.DateTimeChecked=DateTimeChecked;

 			lab.LabCaseNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(LabCase lab){
			string command= "UPDATE LabCase SET " 
				+ "Description = '"    +POut.PString(lab.Description)+"'"
				+ ",Phone = '"         +POut.PString(lab.Phone)+"'"
				+ ",Notes = '"         +POut.PString(lab.Notes)+"'"
				+ ",LabSlip = '"       +POut.PString(lab.LabSlip)+"'"
				+" WHERE LabCaseNum = '" +POut.PInt(lab.LabCaseNum)+"'";
 			General.NonQ(command);
		}*/

		/*
		///<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		public static void Delete(int labNum){
			string command;
			//check patients for dependencies
			string command="SELECT LName,FName FROM patient WHERE LabCaseNum ="
				+POut.PInt(LabCase.LabCaseNum);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lan.g("LabCases","Cannot delete LabCase because ")+pats);
			}
			//delete
			command= "DELETE FROM LabCase" 
				+" WHERE LabCaseNum = "+POut.PInt(labNum);
 			General.NonQ(command);
		}*/

		
	
		

	}
	


}













