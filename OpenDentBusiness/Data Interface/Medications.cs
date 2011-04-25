using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{

	///<summary></summary>
	public class Medications{
		///<summary>All medications.  Not refreshed with local data.  Only refreshed as needed.  Only used in UI layer.</summary>
		public static Medication[] Listt;
		///<summary></summary>
		private static Hashtable HList;

		///<summary></summary>
		public static void Refresh() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command ="SELECT * from medication ORDER BY MedName";// WHERE MedName LIKE '%"+POut.String(str)+"%' ORDER BY MedName";
			DataTable table=Db.GetTable(command);
			HList=new Hashtable();
			List<Medication> list=new List<Medication>();
			Medication med;
			for(int i=0;i<table.Rows.Count;i++) {
				med=new Medication();
				med.MedicationNum=PIn.Long(table.Rows[i][0].ToString());
				med.MedName      =PIn.String(table.Rows[i][1].ToString());
				med.GenericNum   =PIn.Long(table.Rows[i][2].ToString());
				med.Notes        =PIn.String(table.Rows[i][3].ToString());
				list.Add(med);
				HList.Add(list[i].MedicationNum,list[i]);
			}
			Listt=list.ToArray();
		}

		public static List<Medication> GetList(string str) {
			//No need to check RemotingRole; no call to db.
			Refresh();
			List<Medication> retVal=new List<Medication>();
			for(int i=0;i<Listt.Length;i++) {
				if(str=="" || Listt[i].MedName.ToUpper().Contains(str.ToUpper())) {
					retVal.Add(Listt[i]);
				}
			}
			return retVal;
		}

		///<summary></summary>
		public static void Update(Medication Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			Crud.MedicationCrud.Update(Cur);
		}

		///<summary></summary>
		public static long Insert(Medication Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.MedicationNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.MedicationNum;
			}
			return Crud.MedicationCrud.Insert(Cur);
		}

		///<summary>Dependent brands and patients will already be checked.</summary>
		public static void Delete(Medication Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE from medication WHERE medicationNum = '"+Cur.MedicationNum.ToString()+"'";
			Db.NonQ(command);
			DeletedObjects.SetDeleted(DeletedObjectType.Medication,Cur.MedicationNum);
		}

		///<summary>Returns a list of all patients using this medication.</summary>
		public static string[] GetPats(long medicationNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<string[]>(MethodBase.GetCurrentMethod(),medicationNum);
			}
			string command =
				"SELECT CONCAT(CONCAT(CONCAT(CONCAT(LName,', '),FName),' '),Preferred) FROM medicationpat,patient "
				+"WHERE medicationpat.PatNum=patient.PatNum "
				+"AND medicationpat.MedicationNum="+medicationNum.ToString();
			DataTable table=Db.GetTable(command);
			string[] retVal=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retVal[i]=PIn.String(table.Rows[i][0].ToString());
			}
			return retVal;
		}

		///<summary>Returns a list of all brands dependend on this generic. Only gets run if this is a generic.</summary>
		public static string[] GetBrands(long medicationNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<string[]>(MethodBase.GetCurrentMethod(),medicationNum);
			}
			string command =
				"SELECT MedName FROM medication "
				+"WHERE GenericNum="+medicationNum.ToString()
				+" AND MedicationNum !="+medicationNum.ToString();//except this med
			DataTable table=Db.GetTable(command);
			string[] retVal=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retVal[i]=PIn.String(table.Rows[i][0].ToString());
			}
			return retVal;
		}

		///<summary></summary>
		public static Medication GetMedication(long medNum) {
			//No need to check RemotingRole; no call to db.
			return (Medication)HList[medNum];
		}

		///<summary>Gets the generic medication for the specified medication Num.</summary>
		public static Medication GetGeneric(long medNum) {
			//No need to check RemotingRole; no call to db.
			return (Medication)HList[((Medication)HList[medNum]).GenericNum];
		}

		///<summary>Gets the generic medication name, given it's generic Num.</summary>
		public static string GetGenericName(long genericNum) {
			//No need to check RemotingRole; no call to db.
			if(HList.ContainsKey(genericNum)){
				return ((Medication)HList[genericNum]).MedName;
			}
			return "";
		}

		public static List<long> GetChangedSinceMedicationNums(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<long>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT MedicationNum FROM medication WHERE DateTStamp > "+POut.DateT(changedSince);
			DataTable dt=Db.GetTable(command);
			List<long> medicationNums = new List<long>(dt.Rows.Count);
			for(int i=0;i<dt.Rows.Count;i++) {
				medicationNums.Add(PIn.Long(dt.Rows[i]["MedicationNum"].ToString()));
			}
			return medicationNums;
		}

		///<summary>Used along with GetChangedSinceMedicationNums</summary>
		public static List<Medication> GetMultMedications(List<long> medicationNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Medication>>(MethodBase.GetCurrentMethod(),medicationNums);
			}
			string strMedicationNums="";
			DataTable table;
			if(medicationNums.Count>0) {
				for(int i=0;i<medicationNums.Count;i++) {
					if(i>0) {
						strMedicationNums+="OR ";
					}
					strMedicationNums+="MedicationNum='"+medicationNums[i].ToString()+"' ";
				}
				string command="SELECT * FROM medication WHERE "+strMedicationNums;
				table=Db.GetTable(command);
			}
			else {
				table=new DataTable();
			}
			Medication[] multMedications=Crud.MedicationCrud.TableToList(table).ToArray();
			List<Medication> MedicationList=new List<Medication>(multMedications);
			return MedicationList;
		}
		
		///<summary>Returns medication list for a specific patient.</summary
		public static List<Medication> GetMedicationsByPat(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Medication>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT m.* FROM medication m,medicationpat mp "
				+"WHERE m.MedicationNum=mp.MedicationNum "
				+"AND mp.PatNum="+POut.Long(patNum);
			return Crud.MedicationCrud.SelectMany(command);
		}

		///<summary>Changes the value of the DateTStamp column to the current time stamp for all medications of a patient</summary>
		public static void ResetTimeStamps(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			string command="UPDATE medication SET DateTStamp = CURRENT_TIMESTAMP WHERE PatNum ="+POut.Long(patNum);
			Db.NonQ(command);
		}

	}

	




}










