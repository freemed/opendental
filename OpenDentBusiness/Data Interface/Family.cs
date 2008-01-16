using System;
using System.Data;

namespace OpenDentBusiness
{
	///<summary></summary>
	public class Family{
		///<summary></summary>
		public Family(){
			
		}

		///<summary>List of patients in the family.</summary>
		public Patient[] List;

		///<summary>Tries to get the LastName,FirstName of the patient from this family.  If not found, then gets the name from the database.</summary>
		public string GetNameInFamLF(int myPatNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].PatNum==myPatNum){
					return List[i].GetNameLF();
				}
			}
			return GetLim(myPatNum).GetNameLF();
		}

		///<summary>Gets last, (preferred) first middle</summary>
		public string GetNameInFamLFI(int myi){
			string retStr="";
			if(List[myi].Preferred==""){
				retStr=List[myi].LName+", "+List[myi].FName+" "+List[myi].MiddleI; 
			}
			else{
				retStr=List[myi].LName+", '"+List[myi].Preferred+"' "+List[myi].FName+" "+List[myi].MiddleI;
			}
			return retStr;
		}

		///<summary>Gets a formatted name from the family list.  If the patient is not in the family list, then it gets that info from the database.</summary>
		public string GetNameInFamFL(int myPatNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].PatNum==myPatNum){
					return List[i].GetNameFL();
				}
			}
			return GetLim(myPatNum).GetNameFL();
		}

		///<summary>Gets (preferred)first middle last</summary>
		public string GetNameInFamFLI(int myi){
			string retStr="";
			if(List[myi].Preferred==""){
				retStr=List[myi].FName+" "+List[myi].MiddleI+" "+List[myi].LName; 
			}
			else{
				retStr="'"+List[myi].Preferred+"' "+List[myi].FName+" "+List[myi].MiddleI+" "+List[myi].LName;
			}
			return retStr;
		}

		///<summary>Gets first name from the family list.  If the patient is not in the family list, then it gets that info from the database.</summary>
		public string GetNameInFamFirst(int myPatNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].PatNum==myPatNum){
					return List[i].GetNameFirst();
				}
			}
			return GetLim(myPatNum).GetNameFirst();
		}

		///<summary>The index of the patient within the family.  Returns -1 if not found.</summary>
		public int GetIndex(int patNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].PatNum==patNum){
					return i;
				}
			}
			return -1;
		}

		///<summary>Gets a copy of a specific patient from within the family. Does not make a call to the database.</summary>
		public Patient GetPatient(int patNum){
			Patient retVal=null;
			for(int i=0;i<List.Length;i++){
				if(List[i].PatNum==patNum){
					retVal=List[i].Copy();
				}
			}
			return retVal;
		}

		/// <summary>Duplicate of the same class in Patients.  Gets nine of the most useful fields from the db for the given patnum.</summary>
		public static Patient GetLim(int patNum){
			if(patNum==0){
				return new Patient();
			}
			string command= 
				"SELECT PatNum,LName,FName,MiddleI,Preferred,CreditType,Guarantor,HasIns,SSN " 
				+"FROM patient "
				+"WHERE PatNum = '"+patNum.ToString()+"'";
 			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return new Patient();
			}
			Patient Lim=new Patient();
			Lim.PatNum     = PIn.PInt   (table.Rows[0][0].ToString());
			Lim.LName      = PIn.PString(table.Rows[0][1].ToString());
			Lim.FName      = PIn.PString(table.Rows[0][2].ToString());
			Lim.MiddleI    = PIn.PString(table.Rows[0][3].ToString());
			Lim.Preferred  = PIn.PString(table.Rows[0][4].ToString());
			Lim.CreditType = PIn.PString(table.Rows[0][5].ToString());
			Lim.Guarantor  = PIn.PInt   (table.Rows[0][6].ToString());
			Lim.HasIns     = PIn.PString(table.Rows[0][7].ToString());
			Lim.SSN        = PIn.PString(table.Rows[0][8].ToString());
			return Lim;
		}

	}
}
