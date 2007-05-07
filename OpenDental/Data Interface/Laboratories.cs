using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Laboratories {

		///<summary>Refresh all Laboratories</summary>
		public static List<Laboratory> Refresh() {
			string command="SELECT * FROM laboratory ORDER BY Description";
			return FillFromCommand(command);
		}

		///<summary>Gets one laboratory from database</summary>
		public static Laboratory GetOne(int laboratoryNum) {
			string command="SELECT * FROM laboratory WHERE LaboratoryNum="+POut.PInt(laboratoryNum);
			return FillFromCommand(command)[0];
		}

		private static List<Laboratory> FillFromCommand(string command){
			DataTable table=General.GetTable(command);
			List<Laboratory> ListLabs=new List<Laboratory>();
			Laboratory lab;
			for(int i=0;i<table.Rows.Count;i++) {
				lab=new Laboratory();
				lab.LaboratoryNum= PIn.PInt   (table.Rows[i][0].ToString());
				lab.Description  = PIn.PString(table.Rows[i][1].ToString());
				lab.Phone        = PIn.PString(table.Rows[i][2].ToString());
				lab.Notes        = PIn.PString(table.Rows[i][3].ToString());
				lab.LabSlip      = PIn.PString(table.Rows[i][4].ToString());
				ListLabs.Add(lab);
			}
			return ListLabs;
		}

		///<summary></summary>
		public static void Insert(Laboratory lab){
			if(PrefB.RandomKeys) {
				lab.LaboratoryNum=MiscData.GetKey("laboratory","LaboratoryNum");
			}
			string command="INSERT INTO laboratory (";
			if(PrefB.RandomKeys) {
				command+="LaboratoryNum,";
			}
			command+="Description,Phone,Notes,LabSlip) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(lab.LaboratoryNum)+"', ";
			}
			command+=
				 "'"+POut.PString(lab.Description)+"', "
				+"'"+POut.PString(lab.Phone)+"', "
				+"'"+POut.PString(lab.Notes)+"', "
				+"'"+POut.PString(lab.LabSlip)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				lab.LaboratoryNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(Laboratory lab){
			string command= "UPDATE laboratory SET " 
				+ "Description = '"    +POut.PString(lab.Description)+"'"
				+ ",Phone = '"         +POut.PString(lab.Phone)+"'"
				+ ",Notes = '"         +POut.PString(lab.Notes)+"'"
				+ ",LabSlip = '"       +POut.PString(lab.LabSlip)+"'"
				+" WHERE LaboratoryNum = '" +POut.PInt(lab.LaboratoryNum)+"'";
 			General.NonQ(command);
		}

		///<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		public static void Delete(int labNum){
			string command;
			//check patients for dependencies
			/*string command="SELECT LName,FName FROM patient WHERE LaboratoryNum ="
				+POut.PInt(Laboratory.LaboratoryNum);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lan.g("Laboratories","Cannot delete Laboratory because ")+pats);
			}*/
			//delete
			command= "DELETE FROM laboratory" 
				+" WHERE LaboratoryNum = "+POut.PInt(labNum);
 			General.NonQ(command);
		}

		/*
		///<summary>Returns null if Laboratory not found.</summary>
		public static Laboratory GetLaboratory(int LaboratoryNum){
			for(int i=0;i<ListLabs.Count;i++){
				if(ListLabs[i].LaboratoryNum==LaboratoryNum){
					return ListLabs[i].Copy();
				}
			}
			return null;
		}

		///<summary>Returns an empty string for invalid LaboratoryNum.</summary>
		public static string GetDesc(int LaboratoryNum){
			for(int i=0;i<ListLabs.Count;i++){
				if(ListLabs[i].LaboratoryNum==LaboratoryNum){
					return ListLabs[i].Description;
				}
			}
			return "";
		}*/
	
		

	}
	


}













