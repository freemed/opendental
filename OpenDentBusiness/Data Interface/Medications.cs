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

		///<summary>This must refresh Listt on client, not on server.</summary>
		public static void Refresh() {
			//No need to check RemotingRole; no call to db.
			List<Medication> list=GetListFromDb();
			HList=new Hashtable();
			for(int i=0;i<list.Count;i++) {
				HList.Add(list[i].MedicationNum,list[i]);
			}
			Listt=list.ToArray();
		}

		///<summary>Only public so that the remoting works.  Do not call this from anywhere except in this class.</summary>
		public static List<Medication> GetListFromDb() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Medication>>(MethodBase.GetCurrentMethod());
			}
			string command ="SELECT * FROM medication ORDER BY MedName";// WHERE MedName LIKE '%"+POut.String(str)+"%' ORDER BY MedName";
			return Crud.MedicationCrud.SelectMany(command);
		}

		public static List<Medication> TableToList(DataTable table) {
			//No need to check RemotingRole; no call to db.
			return Crud.MedicationCrud.TableToList(table);
		}

		///<summary>Returns medications that contain the passed in string.  Blank for all.</summary>
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
			//If this ever fails some place, it's probably an indicator that we should consider using GetMedicationFromDb instead.
			return (Medication)HList[medNum];
		}

		///<summary></summary>
		public static Medication GetMedicationFromDb(long medicationNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Medication>(MethodBase.GetCurrentMethod(),medicationNum);
			}
			string command="SELECT * FROM medication WHERE MedicationNum="+POut.Long(medicationNum);
			return Crud.MedicationCrud.SelectOne(command);
		}

		///<summary>//Returns first medication with matching MedName, if not found returns null.</summary>
		public static Medication GetMedicationFromDbByName(string medicationName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Medication>(MethodBase.GetCurrentMethod(),medicationName);
			}
			string command="SELECT * FROM medication WHERE MedName='"+POut.String(medicationName)+"' ORDER BY MedicationNum";
			List<Medication> retVal=Crud.MedicationCrud.SelectMany(command);
			if(retVal.Count>0) {
				return retVal[0];
			}
			return null;
		}

		///<summary>Gets the generic medication for the specified medication Num.</summary>
		public static Medication GetGeneric(long medNum) {
			//No need to check RemotingRole; no call to db.
			return (Medication)HList[((Medication)HList[medNum]).GenericNum];
		}

		///<summary>Gets the medication name.  Also, generic in () if applicable.</summary>
		public static string GetDescription(long medNum) {
			//No need to check RemotingRole; no call to db.
			if(HList==null) {
				Refresh();
			}
			if(!HList.ContainsKey(medNum)) {
				return "";
			}
			Medication med=(Medication)HList[medNum];
			string retVal=med.MedName;
			if(med.GenericNum==med.MedicationNum){//this is generic
				return retVal;
			}
			if(!HList.ContainsKey(med.GenericNum)) {
				return retVal;
			}
			Medication generic=(Medication)HList[med.GenericNum];
			return retVal+"("+generic.MedName+")";
		}

		///<summary>Gets the medication name. Copied from GetDescription.</summary>
		public static string GetNameOnly(long medNum) {
			//No need to check RemotingRole; no call to db.
			if(HList==null) {
				Refresh();
			}
			if(!HList.ContainsKey(medNum)) {
				return "";
			}
			Medication med=(Medication)HList[medNum];
			return med.MedName;
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
		
		///<summary>Returns medication list for a specific patient.</summary>
		public static List<Medication> GetMedicationsByPat(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Medication>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT medication.* "
				+"FROM medication, medicationpat "
				+"WHERE medication.MedicationNum=medicationpat.MedicationNum "
				+"AND medicationpat.PatNum="+POut.Long(patNum);
			return Crud.MedicationCrud.SelectMany(command);
		}


	}

	




}










