using System;
using OpenDentBusiness;

namespace OpenDental
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
			return Patients.GetLim(myPatNum).GetNameLF();
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
			return Patients.GetLim(myPatNum).GetNameFL();
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

	}
}
