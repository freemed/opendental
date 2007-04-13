using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{

	///<summary></summary>
	public class Medications{
		//not refreshed with local data.  Only refreshed as needed.
		///<summary>All medications.</summary>
		public static Medication[] List;
		///<summary></summary>
		private static Hashtable HList;

		///<summary></summary>
		public static void Refresh(){
			string command =
				"SELECT * from medication ORDER BY MedName";
			FillList(command);
		}

		//<summary></summary>
		//public static void RefreshGeneric(){
		//	command =
		//		"SELECT * from medication WHERE medicationnum = genericnum ORDER BY MedName";
		//	FillList();
		//}

		private static void FillList(string command){
			DataTable table=General.GetTable(command);
			HList=new Hashtable();
			List=new Medication[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Medication();
				List[i].MedicationNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].MedName      =PIn.PString(table.Rows[i][1].ToString());
				List[i].GenericNum   =PIn.PInt   (table.Rows[i][2].ToString());
				List[i].Notes        =PIn.PString(table.Rows[i][3].ToString());
				HList.Add(List[i].MedicationNum,List[i]);
			}
		}

		///<summary></summary>
		public static void Update(Medication Cur){
			string command = "UPDATE medication SET " 
				+ "medname = '"      +POut.PString(Cur.MedName)+"'"
				+ ",genericnum = '"  +POut.PInt   (Cur.GenericNum)+"'"
				+ ",notes = '"       +POut.PString(Cur.Notes)+"'"
				+" WHERE medicationnum = '" +POut.PInt   (Cur.MedicationNum)+"'";
			//MessageBox.Show(command);
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(Medication Cur){
			if(PrefB.RandomKeys){
				Cur.MedicationNum=MiscData.GetKey("medication","MedicationNum");
			}
			string command="INSERT INTO medication (";
			if(PrefB.RandomKeys){
				command+="MedicationNum,";
			}
			command+="medname,genericnum,notes"
				+") VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(Cur.MedicationNum)+"', ";
			}
			command+=
				 "'"+POut.PString(Cur.MedName)+"', "
				+"'"+POut.PInt   (Cur.GenericNum)+"', "
				+"'"+POut.PString(Cur.Notes)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				Cur.MedicationNum=General.NonQ(command,true);
			}
		}

		///<summary>Dependent brands and patients will already be checked.</summary>
		public static void Delete(Medication Cur){
			string command = "DELETE from medication WHERE medicationNum = '"+Cur.MedicationNum.ToString()+"'";
			General.NonQ(command);
		}

		///<summary>Returns a list of all patients using this medication.</summary>
		public static string[] GetPats(int medicationNum){
			string command =
				"SELECT CONCAT(CONCAT(CONCAT(CONCAT(LName,', '),FName),' '),Preferred) FROM medicationpat,patient "
				+"WHERE medicationpat.PatNum=patient.PatNum "
				+"AND medicationpat.MedicationNum="+medicationNum.ToString();
			DataTable table=General.GetTable(command);
			string[] retVal=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retVal[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retVal;
		}

		///<summary>Returns a list of all brands dependend on this generic. Only gets run if this is a generic.</summary>
		public static string[] GetBrands(int medicationNum){
			string command =
				"SELECT MedName FROM medication "
				+"WHERE GenericNum="+medicationNum.ToString()
				+" AND MedicationNum !="+medicationNum.ToString();//except this med
			DataTable table=General.GetTable(command);
			string[] retVal=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retVal[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retVal;
		}

		///<summary></summary>
		public static Medication GetMedication(int medNum){
			return (Medication)HList[medNum];
		}

		///<summary>Gets the generic medication for the specified medication Num.</summary>
		public static Medication GetGeneric(int medNum) {
			return (Medication)HList[((Medication)HList[medNum]).GenericNum];
		}


		
	}

	




}










