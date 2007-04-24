using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;
using System.Data;

namespace OpenDental {
	public class ProcLicenses {

		///<summary>Performs a query to the database to see if the given proclicense already exists (excluding the primary key). Any exact matches in the db will also force this function to return true.</summary>
		public static bool Unique(ProcLicense procLicense){
			string command="SELECT * FROM proclicense WHERE "
				+"ADACode='"+POut.PString(procLicense.ADACode)+"' OR "
				+"Descript='"+POut.PString(procLicense.Description)+"'";
			DataTable dt=General.GetTableEx(command);
			for(int i=0;i<dt.Rows.Count;i++){
				if(dt.Rows[i]["ProcLicenseNum"].ToString()!=procLicense.ProcLicenseNum.ToString()){
					return false;
				}
			}
			return true;
		}

		///<summary>Creates a new proc license row in the database.</summary>
		public static void Insert(ProcLicense procLicense){
			string command="INSERT INTO proclicense (ADACode,Descript) VALUES("
				+"'"+POut.PString(procLicense.ADACode)+"',"
				+"'"+POut.PString(procLicense.Description)+"')";
			General.NonQEx(command);
		}

		public static void Update(ProcLicense procLicense) {
			string command="UPDATE proclicense SET "
				+"ADACode='"+POut.PString(procLicense.ADACode)+"',"
				+"Descript='"+POut.PString(procLicense.Description)+"'"
				+" WHERE ProcLicenseNum='"+POut.PInt(procLicense.ProcLicenseNum)+"'";
			General.NonQEx(command);
		}

		public static void Delete(ProcLicense procLicense) {
			string command="DELETE FROM proclicense WHERE ProcLicenseNum='"
				+POut.PInt(procLicense.ProcLicenseNum)+"'";
			General.NonQEx(command);
		}

		public static ProcLicense Fill(DataRow procLicense) {
			ProcLicense pl=new ProcLicense();
			pl.ProcLicenseNum=PIn.PInt(procLicense["ProcLicenseNum"].ToString());
			pl.ADACode=PIn.PString(procLicense["ADACode"].ToString());
			pl.Description=PIn.PString(procLicense["Descript"].ToString());
			return pl;
		}

		public static ProcLicense[] Refresh() {
			string command="SELECT * FROM proclicense ORDER BY ADACode";
			DataTable dt=General.GetTableEx(command);
			ProcLicense[] procLicenses=new ProcLicense[dt.Rows.Count];
			for(int i=0;i<procLicenses.Length;i++){
				procLicenses[i]=Fill(dt.Rows[i]);
			}
			return procLicenses;
		}

	}
}
