using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	
	
	///<summary></summary>
	public class PatientNotes{
		
		///<summary></summary>
		public static PatientNote Refresh(long patNum,long guarantor) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PatientNote>(MethodBase.GetCurrentMethod(),patNum,guarantor);
			}
			string command="SELECT COUNT(*) FROM patientnote WHERE patnum = '"+POut.Long(patNum)+"'";
			DataTable table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()=="0"){
				InsertRow(patNum);
			}
			command ="SELECT PatNum,ApptPhone,Medical,Service,MedicalComp,Treatment,CCNumber,CCExpiration "
				+"FROM patientnote WHERE patnum ='"+POut.Long(patNum)+"'";
			table=Db.GetTable(command);
			PatientNote Cur=new PatientNote();
			Cur.PatNum      = PIn.Long   (table.Rows[0][0].ToString());
			Cur.ApptPhone   = PIn.String(table.Rows[0][1].ToString());
			Cur.Medical     = PIn.String(table.Rows[0][2].ToString());
			Cur.Service     = PIn.String(table.Rows[0][3].ToString());
			Cur.MedicalComp = PIn.String(table.Rows[0][4].ToString());
			Cur.Treatment   = PIn.String(table.Rows[0][5].ToString());
			Cur.CCNumber    = PIn.String(table.Rows[0][6].ToString());
			Cur.CCExpiration= PIn.Date  (table.Rows[0][7].ToString());
			//fam financial note:
			command = 
				"SELECT * FROM patientnote WHERE patnum ='"+POut.Long(guarantor)+"'";
			table=Db.GetTable(command);
			if(table.Rows.Count==0){
				InsertRow(guarantor);
			}
			command = 
				"SELECT famfinancial "
				+"FROM patientnote WHERE patnum ='"+POut.Long(guarantor)+"'";
			//MessageBox.Show(command);
			table=Db.GetTable(command);
			Cur.FamFinancial= PIn.String(table.Rows[0][0].ToString());
			return Cur;
		}

		///<summary></summary>
		public static void Update(PatientNote Cur,long guarantor) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur,guarantor);
				return;
			}
			string command = "UPDATE patientnote SET "
				//+ "apptphone = '"   +POut.PString(Cur.ApptPhone)+"'"
				+ "Medical = '"      +POut.String(Cur.Medical)+"'"
				+ ",Service = '"     +POut.String(Cur.Service)+"'"
				+ ",MedicalComp = '" +POut.String(Cur.MedicalComp)+"'"
				+ ",Treatment = '"   +POut.String(Cur.Treatment)+"'"
				+ ",CCNumber = '"    +POut.String(Cur.CCNumber)+"'"
				+ ",CCExpiration = "+POut.Date  (Cur.CCExpiration)
				+" WHERE patnum = '"+POut.Long   (Cur.PatNum)+"'";
			//MessageBox.Show(command);
			Db.NonQ(command);
			command = "UPDATE patientnote SET "
				+ "famfinancial = '"+POut.String(Cur.FamFinancial)+"'"
				+" WHERE patnum = '"+POut.Long   (guarantor)+"'";
			//MessageBox.Show(command);
			Db.NonQ(command);
		}

		///<summary></summary>
		private static void InsertRow(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			//Random keys not necessary to check because of 1:1 patNum.
			//However, this is a lazy insert, so multiple locations might attempt it.
			//Just in case, we will have it fail silently.
			string command = "INSERT IGNORE INTO patientnote (patnum"
				+") VALUES('"+patNum+"')";
			Db.NonQ(command);
		}

	
	}

	

	

}










