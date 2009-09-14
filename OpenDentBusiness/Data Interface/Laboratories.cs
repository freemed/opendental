using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Laboratories {

		///<summary>Refresh all Laboratories</summary>
		public static List<Laboratory> Refresh() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Laboratory>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM laboratory ORDER BY Description";
			return FillFromTable(Db.GetTable(command));
		}

		///<summary>Gets one laboratory from database</summary>
		public static Laboratory GetOne(long laboratoryNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Laboratory>(MethodBase.GetCurrentMethod(),laboratoryNum);
			}
			string command="SELECT * FROM laboratory WHERE LaboratoryNum="+POut.PInt(laboratoryNum);
			return FillFromTable(Db.GetTable(command))[0];
		}

		private static List<Laboratory> FillFromTable(DataTable table){
			//No need to check RemotingRole; no call to db.
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
		public static long Insert(Laboratory lab) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				lab.LaboratoryNum=Meth.GetInt(MethodBase.GetCurrentMethod(),lab);
				return lab.LaboratoryNum;
			}
			if(PrefC.RandomKeys) {
				lab.LaboratoryNum=ReplicationServers.GetKey("laboratory","LaboratoryNum");
			}
			string command="INSERT INTO laboratory (";
			if(PrefC.RandomKeys) {
				command+="LaboratoryNum,";
			}
			command+="Description,Phone,Notes,LabSlip) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PInt(lab.LaboratoryNum)+"', ";
			}
			command+=
				 "'"+POut.PString(lab.Description)+"', "
				+"'"+POut.PString(lab.Phone)+"', "
				+"'"+POut.PString(lab.Notes)+"', "
				+"'"+POut.PString(lab.LabSlip)+"')";
			lab.LaboratoryNum=Db.NonQ(command,true);
			return lab.LaboratoryNum;
		}

		///<summary></summary>
		public static void Update(Laboratory lab){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),lab);
				return;
			}
			string command= "UPDATE laboratory SET " 
				+ "Description = '"    +POut.PString(lab.Description)+"'"
				+ ",Phone = '"         +POut.PString(lab.Phone)+"'"
				+ ",Notes = '"         +POut.PString(lab.Notes)+"'"
				+ ",LabSlip = '"       +POut.PString(lab.LabSlip)+"'"
				+" WHERE LaboratoryNum = '" +POut.PInt(lab.LaboratoryNum)+"'";
 			Db.NonQ(command);
		}

		///<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		public static void Delete(long labNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),labNum);
				return;
			}
			string command;
			//check lab cases for dependencies
			command="SELECT LName,FName FROM patient,labcase "
				+"WHERE patient.PatNum=labcase.PatNum "
				+"AND LaboratoryNum ="+POut.PInt(labNum)+" "
				+"LIMIT 30";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lans.g("Laboratories","Cannot delete Laboratory because cases exist for")+pats);
			}
			//delete
			command= "DELETE FROM laboratory WHERE LaboratoryNum = "+POut.PInt(labNum);
 			Db.NonQ(command);
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













