using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using OpenDentBusiness;


namespace OpenDental {
	public class ProcLicenses {
		public static ProcLicense[] Refresh() {
			string command="SELECT * FROM proclicense ORDER BY ADACode";
			DataTable table=General.GetTable(command);
			ProcLicense[] procLicenses=new ProcLicense[table.Rows.Count];
			for(int i=0;i<procLicenses.Length;i++) {
				procLicenses[i]=new ProcLicense();
				procLicenses[i].ProcLicenseNum=PIn.PInt   (table.Rows[i][0].ToString());
				procLicenses[i].ADACode=       PIn.PString(table.Rows[i][1].ToString());
				procLicenses[i].Descript=      PIn.PString(table.Rows[i][2].ToString());
			}
			return procLicenses;
		}

		///<summary>Creates a new proc license row in the database.</summary>
		public static void Insert(ProcLicense procLicense){
			string command="INSERT INTO proclicense (ADACode,Descript) VALUES("
				+"'"+POut.PString(procLicense.ADACode)+"',"
				+"'"+POut.PString(procLicense.Descript)+"')";
			General.NonQ(command);
		}

		public static void Update(ProcLicense procLicense) {
			string command="UPDATE proclicense SET "
				+"ADACode='"+POut.PString(procLicense.ADACode)+"',"
				+"Descript='"+POut.PString(procLicense.Descript)+"'"
				+" WHERE ProcLicenseNum='"+POut.PInt(procLicense.ProcLicenseNum)+"'";
			General.NonQ(command);
		}

		public static void Delete(ProcLicense procLicense) {
			string command="DELETE FROM proclicense WHERE ProcLicenseNum='"
				+POut.PInt(procLicense.ProcLicenseNum)+"'";
			General.NonQ(command);
		}

		///<summary>Performs a query to the database to see if the given proclicense code already exists.</summary>
		public static bool IsUniqueCode(string code){
			ProcLicense[] listProcLicenses=ProcLicenses.Refresh();
			for(int i=0;i<listProcLicenses.Length;i++) {
				if(listProcLicenses[i].ADACode==code) {
					return false;
				}
			}
			return true;
		}

		///<summary>ProcLicenses will have already been saved to database. This checks to make sure they've all been entered.  Returns a list with descriptions of all codes that still need to be entered.</summary>
		public static ArrayList CheckCompliance(){
			ArrayList retVal=new ArrayList();
			ProcLicense[] listProcLics=Refresh();
			string command="SELECT ADACode,PatNum,ProcDate FROM procedurelog GROUP BY ADACode";
			DataTable table=General.GetTable(command);
			string adacode;
			string comment;
			bool alreadyAdded;
			for(int i=0;i<table.Rows.Count;i++){
				adacode=PIn.PString(table.Rows[i][0].ToString());
				if(!Regex.IsMatch(adacode,"^D([0-9]{4})$")){
					continue;//ignore anything but D####
				}
				if(!ProcedureCodes.GetProcCode(adacode).PreExisting){//if user added it themselves
					continue;
				}
				alreadyAdded=false;
				for(int j=0;j<listProcLics.Length;j++){
					if(listProcLics[j].ADACode==adacode){
						alreadyAdded=true;
						break;
					}
				}
				if(alreadyAdded){
					continue;
				}
				if(ProcedureCodes.GetProcCode(adacode).AbbrDesc==""){
					comment=adacode;
				}
				else{
					comment=ProcedureCodes.GetProcCode(adacode).AbbrDesc;
				}
				comment+=" used by "
					+Patients.GetLim(PIn.PInt(table.Rows[i][1].ToString())).GetNameLF()+" on "
					+PIn.PDate(table.Rows[i][2].ToString()).ToShortDateString();
				retVal.Add(comment);
			}
			command="SELECT DISTINCT ADACode FROM fee";
			table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				adacode=PIn.PString(table.Rows[i][0].ToString());
				if(!Regex.IsMatch(adacode,"^D([0-9]{4})$")) {
					continue;//ignore anything but D####
				}
				if(!ProcedureCodes.GetProcCode(adacode).PreExisting) {//if user added it themselves
					continue;
				}
				alreadyAdded=false;
				for(int j=0;j<listProcLics.Length;j++) {
					if(listProcLics[j].ADACode==adacode) {
						alreadyAdded=true;
						break;
					}
				}
				if(alreadyAdded) {
					continue;
				}
				if(ProcedureCodes.GetProcCode(adacode).AbbrDesc==""){
					comment=adacode;
				}
				else{
					comment=ProcedureCodes.GetProcCode(adacode).AbbrDesc;
				}
				comment+=" used in a fee schedule.";
				retVal.Add(comment);
			}
			return retVal;
		}

		///<summary>Updates Descripts from the proclicense table into the procedurecode table.  Returns the number of rows affected.</summary>
		public static int MergeToProcedureCodes() {
			string command="SELECT ADACode,Descript FROM proclicense";
			DataTable table=General.GetTable(command);
			string adacode;
			string newdescript;
			int rowsaffected=0;
			for(int i=0;i<table.Rows.Count;i++) {
				adacode=PIn.PString(table.Rows[i]["ADACode"].ToString());
				newdescript=PIn.PString(table.Rows[i]["Descript"].ToString());
				command="UPDATE procedurecode SET Descript='"+POut.PString(newdescript)
						+"' WHERE ADACode='"+POut.PString(adacode)+"'";
				rowsaffected+=General.NonQ(command);
			}
			return rowsaffected;
		}

		///<summary>Deletes unused codes.  Returns the number of rows affected.</summary>
		public static int PurgeUnused() {
			string command=@"SELECT DISTINCT procedurecode.ADACode FROM procedurecode
				LEFT JOIN procedurelog ON procedurelog.ADACode=procedurecode.ADACode
				WHERE procedurelog.ADACode IS NULL";
			DataTable table=General.GetTable(command);
			string adacode;
			int rowsaffected=0;
			ProcLicense[] listProcLics=Refresh();
			for(int i=0;i<table.Rows.Count;i++) {
				adacode=PIn.PString(table.Rows[i]["ADACode"].ToString());
				if(!Regex.IsMatch(adacode,"^D([0-9]{4})$")) {
					continue;//ignore anything but D####
				}
				if(!ProcedureCodes.GetProcCode(adacode).PreExisting) {//if user added it themselves
					continue;
				}
				for(int j=0;j<listProcLics.Length;j++) {
					if(listProcLics[j].ADACode==adacode) {
						continue;//user has it in list of codes that they intend to use, so don't delete it.
					}
				}
				//make sure it's not used in fees
				command="SELECT COUNT(*) FROM fee WHERE ADACode='"+POut.PString(adacode)+"'";
				if(General.GetCount(command)!="0"){
					continue;
				}
				command="DELETE FROM procedurecode WHERE ADACode='"+POut.PString(adacode)+"'";
				rowsaffected+=General.NonQ(command);
			}
			return rowsaffected;
		}

	}
}
