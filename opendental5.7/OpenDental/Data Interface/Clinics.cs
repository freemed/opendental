using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Clinics {
		///<summary></summary>
		public static Clinic[] List;

		///<summary>Refresh all clinics</summary>
		public static void Refresh() {
			string command="SELECT * FROM clinic";
			DataTable table=General.GetTable(command);
			List=new Clinic[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Clinic();
				List[i].ClinicNum       = PIn.PInt(table.Rows[i][0].ToString());
				List[i].Description     = PIn.PString(table.Rows[i][1].ToString());
				List[i].Address         = PIn.PString(table.Rows[i][2].ToString());
				List[i].Address2        = PIn.PString(table.Rows[i][3].ToString());
				List[i].City            = PIn.PString(table.Rows[i][4].ToString());
				List[i].State           = PIn.PString(table.Rows[i][5].ToString());
				List[i].Zip             = PIn.PString(table.Rows[i][6].ToString());
				List[i].Phone           = PIn.PString(table.Rows[i][7].ToString());
				List[i].BankNumber      = PIn.PString(table.Rows[i][8].ToString());
				List[i].DefaultPlaceService=(PlaceOfService)PIn.PInt(table.Rows[i][9].ToString());
			}
		}

		///<summary></summary>
		private static void Insert(Clinic clinic){
			string command= "INSERT INTO clinic (Description,Address,Address2,City,State,Zip,Phone,"
				+"BankNumber,DefaultPlaceService) VALUES("
				+"'"+POut.PString(clinic.Description)+"', "
				+"'"+POut.PString(clinic.Address)+"', "
				+"'"+POut.PString(clinic.Address2)+"', "
				+"'"+POut.PString(clinic.City)+"', "
				+"'"+POut.PString(clinic.State)+"', "
				+"'"+POut.PString(clinic.Zip)+"', "
				+"'"+POut.PString(clinic.Phone)+"', "
				+"'"+POut.PString(clinic.BankNumber)+"', "
				+"'"+POut.PInt   ((int)clinic.DefaultPlaceService)+"')";
 			clinic.ClinicNum=General.NonQ(command,true);
		}

		///<summary></summary>
		private static void Update(Clinic clinic){
			string command= "UPDATE clinic SET " 
				+ "Description = '"       +POut.PString(clinic.Description)+"'"
				+ ",Address = '"          +POut.PString(clinic.Address)+"'"
				+ ",Address2 = '"         +POut.PString(clinic.Address2)+"'"
				+ ",City = '"             +POut.PString(clinic.City)+"'"
				+ ",State = '"            +POut.PString(clinic.State)+"'"
				+ ",Zip = '"              +POut.PString(clinic.Zip)+"'"
				+ ",Phone = '"            +POut.PString(clinic.Phone)+"'"
				+ ",BankNumber = '"       +POut.PString(clinic.BankNumber)+"'"
				+ ",DefaultPlaceService='"+POut.PInt   ((int)clinic.DefaultPlaceService)+"'"
				+" WHERE ClinicNum = '" +POut.PInt(clinic.ClinicNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void InsertOrUpdate(Clinic clinic, bool IsNew){
			//if(){
				//throw new Exception(Lan.g(this,""));
			//}
			if(IsNew){
				Insert(clinic);
			}
			else{
				Update(clinic);
			}
		}

		///<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		public static void Delete(Clinic clinic){
			//check patients for dependencies
			string command="SELECT LName,FName FROM patient WHERE ClinicNum ="
				+POut.PInt(clinic.ClinicNum);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lan.g("Clinics","Cannot delete clinic because it is in use by the following patients:")+pats);
			}
			//check payments for dependencies
			command="SELECT patient.LName,patient.FName FROM patient,payment "
				+"WHERE payment.ClinicNum ="+POut.PInt(clinic.ClinicNum)
				+" AND patient.PatNum=payment.PatNum";
			table=General.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lan.g("Clinics","Cannot delete clinic because the following patients have payments using it:")+pats);
			}
			//check claimpayments for dependencies
			command="SELECT patient.LName,patient.FName FROM patient,claimproc,claimpayment "
				+"WHERE claimpayment.ClinicNum ="+POut.PInt(clinic.ClinicNum)
				+" AND patient.PatNum=claimproc.PatNum"
				+" AND claimproc.ClaimPaymentNum=claimpayment.ClaimPaymentNum "
				+"GROUP BY claimpayment.ClaimPaymentNum";
			table=General.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lan.g("Clinics","Cannot delete clinic because the following patients have claim payments using it:")+pats);
			}
			//check appointments for dependencies
			command="SELECT patient.LName,patient.FName FROM patient,appointment "
				+"WHERE appointment.ClinicNum ="+POut.PInt(clinic.ClinicNum)
				+" AND patient.PatNum=appointment.PatNum";
			table=General.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lan.g("Clinics","Cannot delete clinic because the following patients have appointments using it:")+pats);
			}
			//check procedures for dependencies
			command="SELECT patient.LName,patient.FName FROM patient,procedurelog "
				+"WHERE procedurelog.ClinicNum ="+POut.PInt(clinic.ClinicNum)
				+" AND patient.PatNum=procedurelog.PatNum";
			table=General.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lan.g("Clinics","Cannot delete clinic because the following patients have procedures using it:")+pats);
			}
			//check operatories for dependencies
			command="SELECT OpName FROM operatory "
				+"WHERE ClinicNum ="+POut.PInt(clinic.ClinicNum);
			table=General.GetTable(command);
			if(table.Rows.Count>0){
				string ops="";
				for(int i=0;i<table.Rows.Count;i++){
					ops+="\r";
					ops+=table.Rows[i][0].ToString();
				}
				throw new Exception(Lan.g("Clinics","Cannot delete clinic because the following operatories are using it:")+ops);
			}
			//delete
			command= "DELETE FROM clinic" 
				+" WHERE ClinicNum = "+POut.PInt(clinic.ClinicNum);
 			General.NonQ(command);
		}

		///<summary>Returns null if clinic not found.</summary>
		public static Clinic GetClinic(int clinicNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ClinicNum==clinicNum){
					return List[i].Copy();
				}
			}
			return null;
		}

		///<summary>Returns an empty string for invalid clinicNums.</summary>
		public static string GetDesc(int clinicNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ClinicNum==clinicNum){
					return List[i].Description;
				}
			}
			return "";
		}
	
		///<summary>Returns office for invalid clinicNums.</summary>
		public static PlaceOfService GetPlaceService(int clinicNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ClinicNum==clinicNum){
					return List[i].DefaultPlaceService;
				}
			}
			return PlaceOfService.Office;
		}

	}
	


}













