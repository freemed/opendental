using System;
using System.Collections;
using System.Data;
using OpenDentBusiness;

namespace OpenDental{
	
	
	///<summary></summary>
	public class PatientNotes{
		//<summary>We need to eliminate this global variable.</summary>
		//public static PatientNote Cur;
		
		///<summary></summary>
		public static PatientNote Refresh(int patNum,int guarantor){
			string command="SELECT COUNT(*) FROM patientnote WHERE patnum = '"+POut.PInt(patNum)+"'";
			DataTable table=General.GetTable(command);
			if(table.Rows[0][0].ToString()=="0"){
				InsertRow(patNum);
			}
			command ="SELECT PatNum,ApptPhone,Medical,Service,MedicalComp,Treatment,CCNumber,CCExpiration "
				+"FROM patientnote WHERE patnum ='"+POut.PInt(patNum)+"'";
			table=General.GetTable(command);
			PatientNote Cur=new PatientNote();
			Cur.PatNum      = PIn.PInt   (table.Rows[0][0].ToString());
			Cur.ApptPhone   = PIn.PString(table.Rows[0][1].ToString());
			Cur.Medical     = PIn.PString(table.Rows[0][2].ToString());
			Cur.Service     = PIn.PString(table.Rows[0][3].ToString());
			Cur.MedicalComp = PIn.PString(table.Rows[0][4].ToString());
			Cur.Treatment   = PIn.PString(table.Rows[0][5].ToString());
			Cur.CCNumber    = PIn.PString(table.Rows[0][6].ToString());
			Cur.CCExpiration= PIn.PDate  (table.Rows[0][7].ToString());
			//fam financial note:
			command = 
				"SELECT * FROM patientnote WHERE patnum ='"+POut.PInt(guarantor)+"'";
			table=General.GetTable(command);
			if(table.Rows.Count==0){
				InsertRow(guarantor);
			}
			command = 
				"SELECT famfinancial "
				+"FROM patientnote WHERE patnum ='"+POut.PInt(guarantor)+"'";
			//MessageBox.Show(command);
			table=General.GetTable(command);
			Cur.FamFinancial= PIn.PString(table.Rows[0][0].ToString());
			return Cur;
		}

		///<summary></summary>
		public static void Update(PatientNote Cur, int guarantor){
			string command = "UPDATE patientnote SET "
				//+ "apptphone = '"   +POut.PString(Cur.ApptPhone)+"'"
				+ "Medical = '"      +POut.PString(Cur.Medical)+"'"
				+ ",Service = '"     +POut.PString(Cur.Service)+"'"
				+ ",MedicalComp = '" +POut.PString(Cur.MedicalComp)+"'"
				+ ",Treatment = '"   +POut.PString(Cur.Treatment)+"'"
				+ ",CCNumber = '"    +POut.PString(Cur.CCNumber)+"'"
				+ ",CCExpiration = "+POut.PDate  (Cur.CCExpiration)
				+" WHERE patnum = '"+POut.PInt   (Cur.PatNum)+"'";
			//MessageBox.Show(command);
			General.NonQ(command);
			command = "UPDATE patientnote SET "
				+ "famfinancial = '"+POut.PString(Cur.FamFinancial)+"'"
				+" WHERE patnum = '"+POut.PInt   (guarantor)+"'";
			//MessageBox.Show(command);
			General.NonQ(command);
		}

		///<summary></summary>
		private static void InsertRow(int patNum){
			string command = "INSERT INTO patientnote (patnum"
				+") VALUES('"+patNum+"')";
			//MessageBox.Show(command);
			General.NonQ(command);
		}

		//public static void ChangeGuarantor(int newGuarantor){
		//	 command = "UPDATE familynote SET "
		//		+ "guarantor = '"+POut.PInt(newGuarantor)+"'"
		//		+" WHERE guarantor = '" +POut.PInt(Cur.Guarantor)+"'";
		//	//MessageBox.Show(command);
		//	General.NonQ(command);
		//}

	}//end class FamilyNote

	

	

}










