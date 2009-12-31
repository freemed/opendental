using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness
{
	///<summary></summary>
	public class Family{
		///<summary></summary>
		public Family(){
			
		}

		///<summary>List of patients in the family.</summary>
		public Patient[] ListPats;

		///<summary>Tries to get the LastName,FirstName of the patient from this family.  If not found, then gets the name from the database.</summary>
		public string GetNameInFamLF(long myPatNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListPats.Length;i++){
				if(ListPats[i].PatNum==myPatNum){
					return ListPats[i].GetNameLF();
				}
			}
			return GetLim(myPatNum).GetNameLF();
		}

		///<summary>Gets last, (preferred) first middle</summary>
		public string GetNameInFamLFI(int myi){
			//No need to check RemotingRole; no call to db.
			string retStr="";
			if(ListPats[myi].Preferred==""){
				retStr=ListPats[myi].LName+", "+ListPats[myi].FName+" "+ListPats[myi].MiddleI; 
			}
			else{
				retStr=ListPats[myi].LName+", '"+ListPats[myi].Preferred+"' "+ListPats[myi].FName+" "+ListPats[myi].MiddleI;
			}
			return retStr;
		}

		///<summary>Gets a formatted name from the family list.  If the patient is not in the family list, then it gets that info from the database.</summary>
		public string GetNameInFamFL(long myPatNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListPats.Length;i++){
				if(ListPats[i].PatNum==myPatNum){
					return ListPats[i].GetNameFL();
				}
			}
			return GetLim(myPatNum).GetNameFL();
		}

		///<summary>Gets a formatted name from the family list.  If the patient is not in the family list, then it gets that info from the database.</summary>
		public string GetNameInFamFLnoPref(long myPatNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListPats.Length;i++) {
				if(ListPats[i].PatNum==myPatNum) {
					return ListPats[i].GetNameFLnoPref();
				}
			}
			return GetLim(myPatNum).GetNameFLnoPref();
		}

		///<summary>Gets (preferred)first middle last</summary>
		public string GetNameInFamFLI(int myi){
			//No need to check RemotingRole; no call to db.
			string retStr="";
			if(ListPats[myi].Preferred==""){
				retStr=ListPats[myi].FName+" "+ListPats[myi].MiddleI+" "+ListPats[myi].LName; 
			}
			else{
				retStr="'"+ListPats[myi].Preferred+"' "+ListPats[myi].FName+" "+ListPats[myi].MiddleI+" "+ListPats[myi].LName;
			}
			return retStr;
		}

		///<summary>Gets first name from the family list.  If the patient is not in the family list, then it gets that info from the database.</summary>
		public string GetNameInFamFirst(long myPatNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListPats.Length;i++){
				if(ListPats[i].PatNum==myPatNum){
					return ListPats[i].GetNameFirst();
				}
			}
			return GetLim(myPatNum).GetNameFirst();
		}

		///<summary>The index of the patient within the family.  Returns -1 if not found.</summary>
		public int GetIndex(long patNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListPats.Length;i++){
				if(ListPats[i].PatNum==patNum){
					return i;
				}
			}
			return -1;
		}

		///<summary>Gets a copy of a specific patient from within the family. Does not make a call to the database.</summary>
		public Patient GetPatient(long patNum) {
			//No need to check RemotingRole; no call to db.
			Patient retVal=null;
			for(int i=0;i<ListPats.Length;i++){
				if(ListPats[i].PatNum==patNum){
					retVal=ListPats[i].Copy();
				}
			}
			return retVal;
		}

		/// <summary>Duplicate of the same class in Patients.  Gets nine of the most useful fields from the db for the given patnum.</summary>
		public static Patient GetLim(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Patient>(MethodBase.GetCurrentMethod(),patNum);
			}
			if(patNum==0){
				return new Patient();
			}
			string command= 
				"SELECT PatNum,LName,FName,MiddleI,Preferred,CreditType,Guarantor,HasIns,SSN " 
				+"FROM patient "
				+"WHERE PatNum = '"+patNum.ToString()+"'";
 			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return new Patient();
			}
			Patient Lim=new Patient();
			Lim.PatNum     = PIn.Long   (table.Rows[0][0].ToString());
			Lim.LName      = PIn.String(table.Rows[0][1].ToString());
			Lim.FName      = PIn.String(table.Rows[0][2].ToString());
			Lim.MiddleI    = PIn.String(table.Rows[0][3].ToString());
			Lim.Preferred  = PIn.String(table.Rows[0][4].ToString());
			Lim.CreditType = PIn.String(table.Rows[0][5].ToString());
			Lim.Guarantor  = PIn.Long   (table.Rows[0][6].ToString());
			Lim.HasIns     = PIn.String(table.Rows[0][7].ToString());
			Lim.SSN        = PIn.String(table.Rows[0][8].ToString());
			return Lim;
		}

	}
}
