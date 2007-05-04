using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class LabCases {

		///<summary>Gets a filtered list of all labcases.</summary>
		public static DataTable Refresh() {
			string command="SELECT * FROM LabCase ORDER BY DateTimeCreated";
			DataTable table=General.GetTable(command);
			/*for(int i=0;i<table.Rows.Count;i++) {
				lab=new LabCase();
				lab.LabCaseNum= PIn.PInt   (table.Rows[i][0].ToString());
				lab.Description  = PIn.PString(table.Rows[i][1].ToString());
				lab.Phone        = PIn.PString(table.Rows[i][2].ToString());
				lab.Notes        = PIn.PString(table.Rows[i][3].ToString());
				lab.LabSlip      = PIn.PString(table.Rows[i][4].ToString());
				ListLabs.Add(lab);
			}*/
			return table;
		}

		///<Summary>Gets one labcase from database.</Summary>
		public static LabCase GetOne(int labCaseNum){
			string command="SELECT * FROM LabCase WHERE LabCaseNum="+POut.PInt(labCaseNum);
			DataTable table=General.GetTable(command);
			LabCase lab=new LabCase();
			//for(int i=0;i<table.Rows.Count;i++) {
			if(table.Rows.Count==0){
				return null;//this will never happen
			}
			lab=new LabCase();
			lab.LabCaseNum     = PIn.PInt   (table.Rows[0][0].ToString());
			lab.PatNum         = PIn.PInt   (table.Rows[0][1].ToString());
			lab.LaboratoryNum  = PIn.PInt   (table.Rows[0][2].ToString());
			lab.AptNum         = PIn.PInt   (table.Rows[0][3].ToString());
			lab.PlannedAptNum  = PIn.PInt   (table.Rows[0][4].ToString());
			lab.DateTimeDue    = PIn.PDateT (table.Rows[0][5].ToString());
			lab.DateTimeCreated= PIn.PDateT (table.Rows[0][6].ToString());
			lab.DateTimeSent   = PIn.PDateT (table.Rows[0][7].ToString());
			lab.DateTimeRecd   = PIn.PDateT (table.Rows[0][8].ToString());
			lab.DateTimeChecked= PIn.PDateT (table.Rows[0][9].ToString());
			lab.ProvNum        = PIn.PInt   (table.Rows[0][10].ToString());
			return lab;
		}

		///<summary></summary>
		public static void Insert(LabCase lab){
			if(PrefB.RandomKeys) {
				lab.LabCaseNum=MiscData.GetKey("labcase","LabCaseNum");
			}
			string command="INSERT INTO labcase (";
			if(PrefB.RandomKeys) {
				command+="LabCaseNum,";
			}
			command+="PatNum,LaboratoryNum,AptNum,PlannedAptNum,DateTimeDue,DateTimeCreated,"
				+"DateTimeSent,DateTimeRecd,DateTimeChecked,ProvNum) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(lab.LabCaseNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (lab.PatNum)+"', "
				+"'"+POut.PInt   (lab.LaboratoryNum)+"', "
				+"'"+POut.PInt   (lab.AptNum)+"', "
				+"'"+POut.PInt   (lab.PlannedAptNum)+"', "
				+"'"+POut.PDateT (lab.DateTimeDue)+"', "
				+"'"+POut.PDateT (lab.DateTimeCreated)+"', "
				+"'"+POut.PDateT (lab.DateTimeSent)+"', "
				+"'"+POut.PDateT (lab.DateTimeRecd)+"', "
				+"'"+POut.PDateT (lab.DateTimeChecked)+"', "
				+"'"+POut.PInt   (lab.ProvNum)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				lab.LabCaseNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(LabCase lab){
			string command= "UPDATE labcase SET " 
				+ "PatNum = '"          +POut.PInt   (lab.PatNum)+"'"
				+ ",LaboratoryNum = '"  +POut.PInt   (lab.LaboratoryNum)+"'"
				+ ",AptNum = '"         +POut.PInt   (lab.AptNum)+"'"
				+ ",PlannedAptNum = '"  +POut.PInt   (lab.PlannedAptNum)+"'"
				+ ",DateTimeDue = '"    +POut.PDateT (lab.DateTimeDue)+"'"
				+ ",DateTimeCreated = '"+POut.PDateT (lab.DateTimeCreated)+"'"
				+ ",DateTimeSent = '"   +POut.PDateT (lab.DateTimeSent)+"'"
				+ ",DateTimeRecd = '"   +POut.PDateT (lab.DateTimeRecd)+"'"
				+ ",DateTimeChecked = '"+POut.PDateT (lab.DateTimeChecked)+"'"
				+ ",ProvNum = '"        +POut.PInt   (lab.ProvNum)+"'"
				+" WHERE LabCaseNum = '"+POut.PInt(lab.LabCaseNum)+"'";
 			General.NonQ(command);
		}


		///<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		public static void Delete(int labCaseNum){
			string command;
			/*
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
			}*/
			//delete
			command= "DELETE FROM labcase" 
				+" WHERE LabCaseNum = "+POut.PInt(labCaseNum);
 			General.NonQ(command);
		}

		
	
		

	}
	


}













