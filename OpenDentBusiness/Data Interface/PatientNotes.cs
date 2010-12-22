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
			command ="SELECT * FROM patientnote WHERE patnum ='"+POut.Long(patNum)+"'";
			PatientNote Cur=Crud.PatientNoteCrud.SelectOne(command);
			//fam financial note:
			command = "SELECT * FROM patientnote WHERE patnum ='"+POut.Long(guarantor)+"'";
			table=Db.GetTable(command);
			if(table.Rows.Count==0){
				InsertRow(guarantor);
			}
			command = "SELECT famfinancial FROM patientnote WHERE patnum ='"+POut.Long(guarantor)+"'";
			table=Db.GetTable(command);
			Cur.FamFinancial= PIn.String(table.Rows[0][0].ToString());//overrides original FamFinancial value.
			return Cur;
		}

		///<summary></summary>
		public static void Update(PatientNote Cur,long guarantor) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur,guarantor);
				return;
			}
			Crud.PatientNoteCrud.Update(Cur);//FamFinancial gets skipped
			string command = "UPDATE patientnote SET "
				+ "FamFinancial = '"+POut.String(Cur.FamFinancial)+"'"
				+" WHERE patnum = '"+POut.Long   (guarantor)+"'";
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










