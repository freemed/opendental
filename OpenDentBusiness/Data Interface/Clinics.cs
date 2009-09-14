using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary>There is no cache for clinics.  We assume they will change almost never.</summary>
	public class Clinics {
		///<summary></summary>
		private static Clinic[] list;

		public static Clinic[] List{
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		///<summary>Refresh all clinics.  Not actually part of official cache.</summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM clinic";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="clinic";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=new Clinic[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				list[i]=new Clinic();
				list[i].ClinicNum       = PIn.PLong(table.Rows[i][0].ToString());
				list[i].Description     = PIn.PString(table.Rows[i][1].ToString());
				list[i].Address         = PIn.PString(table.Rows[i][2].ToString());
				list[i].Address2        = PIn.PString(table.Rows[i][3].ToString());
				list[i].City            = PIn.PString(table.Rows[i][4].ToString());
				list[i].State           = PIn.PString(table.Rows[i][5].ToString());
				list[i].Zip             = PIn.PString(table.Rows[i][6].ToString());
				list[i].Phone           = PIn.PString(table.Rows[i][7].ToString());
				list[i].BankNumber      = PIn.PString(table.Rows[i][8].ToString());
				list[i].DefaultPlaceService=(PlaceOfService)PIn.PLong(table.Rows[i][9].ToString());
				list[i].InsBillingProv  = PIn.PLong(table.Rows[i][10].ToString());
			}
		}

		///<summary></summary>
		public static long Insert(Clinic clinic){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				clinic.ClinicNum=Meth.GetInt(MethodBase.GetCurrentMethod(),clinic);
				return clinic.ClinicNum;
			}
			if(PrefC.RandomKeys) {
				clinic.ClinicNum=ReplicationServers.GetKey("clinic","ClinicNum");
			}
			string command="INSERT INTO clinic (";
			if(PrefC.RandomKeys) {
				command+="ClinicNum,";
			}
			command+="Description,Address,Address2,City,State,Zip,Phone,"
				+"BankNumber,DefaultPlaceService,InsBillingProv) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PLong(clinic.ClinicNum)+", ";
			}
			command+=
				 "'"+POut.PString(clinic.Description)+"', "
				+"'"+POut.PString(clinic.Address)+"', "
				+"'"+POut.PString(clinic.Address2)+"', "
				+"'"+POut.PString(clinic.City)+"', "
				+"'"+POut.PString(clinic.State)+"', "
				+"'"+POut.PString(clinic.Zip)+"', "
				+"'"+POut.PString(clinic.Phone)+"', "
				+"'"+POut.PString(clinic.BankNumber)+"', "
				+"'"+POut.PLong   ((int)clinic.DefaultPlaceService)+"', "
				+"'"+POut.PLong   (clinic.InsBillingProv)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				clinic.ClinicNum=Db.NonQ(command,true);
			}
			return clinic.ClinicNum;
		}

		///<summary></summary>
		public static void Update(Clinic clinic){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clinic);
				return;
			}
			string command= "UPDATE clinic SET " 
				+ "Description = '"       +POut.PString(clinic.Description)+"'"
				+ ",Address = '"          +POut.PString(clinic.Address)+"'"
				+ ",Address2 = '"         +POut.PString(clinic.Address2)+"'"
				+ ",City = '"             +POut.PString(clinic.City)+"'"
				+ ",State = '"            +POut.PString(clinic.State)+"'"
				+ ",Zip = '"              +POut.PString(clinic.Zip)+"'"
				+ ",Phone = '"            +POut.PString(clinic.Phone)+"'"
				+ ",BankNumber = '"       +POut.PString(clinic.BankNumber)+"'"
				+ ",DefaultPlaceService='"+POut.PLong   ((int)clinic.DefaultPlaceService)+"'"
				+ ",InsBillingProv='"     +POut.PLong   (clinic.InsBillingProv)+"'"
				+" WHERE ClinicNum = '" +POut.PLong(clinic.ClinicNum)+"'";
 			Db.NonQ(command);
		}

		///<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		public static void Delete(Clinic clinic){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clinic);
				return;
			}
			//check patients for dependencies
			string command="SELECT LName,FName FROM patient WHERE ClinicNum ="
				+POut.PLong(clinic.ClinicNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lans.g("Clinics","Cannot delete clinic because it is in use by the following patients:")+pats);
			}
			//check payments for dependencies
			command="SELECT patient.LName,patient.FName FROM patient,payment "
				+"WHERE payment.ClinicNum ="+POut.PLong(clinic.ClinicNum)
				+" AND patient.PatNum=payment.PatNum";
			table=Db.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lans.g("Clinics","Cannot delete clinic because the following patients have payments using it:")+pats);
			}
			//check claimpayments for dependencies
			command="SELECT patient.LName,patient.FName FROM patient,claimproc,claimpayment "
				+"WHERE claimpayment.ClinicNum ="+POut.PLong(clinic.ClinicNum)
				+" AND patient.PatNum=claimproc.PatNum"
				+" AND claimproc.ClaimPaymentNum=claimpayment.ClaimPaymentNum "
				+"GROUP BY claimpayment.ClaimPaymentNum";
			table=Db.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lans.g("Clinics","Cannot delete clinic because the following patients have claim payments using it:")+pats);
			}
			//check appointments for dependencies
			command="SELECT patient.LName,patient.FName FROM patient,appointment "
				+"WHERE appointment.ClinicNum ="+POut.PLong(clinic.ClinicNum)
				+" AND patient.PatNum=appointment.PatNum";
			table=Db.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lans.g("Clinics","Cannot delete clinic because the following patients have appointments using it:")+pats);
			}
			//check procedures for dependencies
			command="SELECT patient.LName,patient.FName FROM patient,procedurelog "
				+"WHERE procedurelog.ClinicNum ="+POut.PLong(clinic.ClinicNum)
				+" AND patient.PatNum=procedurelog.PatNum";
			table=Db.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lans.g("Clinics","Cannot delete clinic because the following patients have procedures using it:")+pats);
			}
			//check operatories for dependencies
			command="SELECT OpName FROM operatory "
				+"WHERE ClinicNum ="+POut.PLong(clinic.ClinicNum);
			table=Db.GetTable(command);
			if(table.Rows.Count>0){
				string ops="";
				for(int i=0;i<table.Rows.Count;i++){
					ops+="\r";
					ops+=table.Rows[i][0].ToString();
				}
				throw new Exception(Lans.g("Clinics","Cannot delete clinic because the following operatories are using it:")+ops);
			}
			//delete
			command= "DELETE FROM clinic" 
				+" WHERE ClinicNum = "+POut.PLong(clinic.ClinicNum);
 			Db.NonQ(command);
		}

		///<summary>Returns null if clinic not found.</summary>
		public static Clinic GetClinic(long clinicNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++){
				if(List[i].ClinicNum==clinicNum){
					return List[i].Copy();
				}
			}
			return null;
		}

		///<summary>Returns an empty string for invalid clinicNums.</summary>
		public static string GetDesc(long clinicNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++){
				if(List[i].ClinicNum==clinicNum){
					return List[i].Description;
				}
			}
			return "";
		}
	
		///<summary>Returns practice default for invalid clinicNums.</summary>
		public static PlaceOfService GetPlaceService(long clinicNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++){
				if(List[i].ClinicNum==clinicNum){
					return List[i].DefaultPlaceService;
				}
			}
			return (PlaceOfService)PrefC.GetInt("DefaultProcedurePlaceService");
			//return PlaceOfService.Office;
		}

	}
	


}













