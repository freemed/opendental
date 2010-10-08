using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{

	///<summary></summary>
	public class Medications{
		///<summary>All medications.  Not refreshed with local data.  Only refreshed as needed.  Only used in UI layer.</summary>
		public static Medication[] List;
		///<summary></summary>
		private static Hashtable HList;

		///<summary></summary>
		public static void Refresh(string str){
			//No need to check RemotingRole; no call to db.
			List<Medication> list=GetList(str);
			HList=new Hashtable();
			List=list.ToArray();
			for(int i=0;i<list.Count;i++) {
				HList.Add(list[i].MedicationNum,list[i]);
			}
		}

		public static List<Medication> GetList(string str) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Medication>>(MethodBase.GetCurrentMethod());
			}
			string command ="SELECT * from medication WHERE MedName LIKE '%"+POut.String(str)+"%' ORDER BY MedName";
			DataTable table=Db.GetTable(command);
			List<Medication> retVal=new List<Medication>();
			Medication med;
			for(int i=0;i<table.Rows.Count;i++) {
				med=new Medication();
				med.MedicationNum=PIn.Long(table.Rows[i][0].ToString());
				med.MedName      =PIn.String(table.Rows[i][1].ToString());
				med.GenericNum   =PIn.Long(table.Rows[i][2].ToString());
				med.Notes        =PIn.String(table.Rows[i][3].ToString());
				retVal.Add(med);
			}
			return retVal;
		}

		///<summary></summary>
		public static void Update(Medication Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "UPDATE medication SET " 
				+ "medname = '"      +POut.String(Cur.MedName)+"'"
				+ ",genericnum = '"  +POut.Long   (Cur.GenericNum)+"'"
				+ ",notes = '"       +POut.String(Cur.Notes)+"'"
				+" WHERE medicationnum = '" +POut.Long   (Cur.MedicationNum)+"'";
			//MessageBox.Show(command);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(Medication Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.MedicationNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.MedicationNum;
			}
			if(PrefC.RandomKeys){
				Cur.MedicationNum=ReplicationServers.GetKey("medication","MedicationNum");
			}
			string command="INSERT INTO medication (";
			if(PrefC.RandomKeys){
				command+="MedicationNum,";
			}
			command+="medname,genericnum,notes"
				+") VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.Long(Cur.MedicationNum)+"', ";
			}
			command+=
				 "'"+POut.String(Cur.MedName)+"', "
				+"'"+POut.Long   (Cur.GenericNum)+"', "
				+"'"+POut.String(Cur.Notes)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				Cur.MedicationNum=Db.NonQ(command,true);
			}
			return Cur.MedicationNum;
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

		
	}

	




}










