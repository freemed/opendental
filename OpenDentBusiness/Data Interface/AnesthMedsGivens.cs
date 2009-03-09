using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;


namespace OpenDentBusiness{
	///<summary>A list of Anesthetic Medications</summary>
	public class AnesthMedsGivens{

        public bool IsNew;

		///<summary></summary> 
		public static DataTable RefreshCache(int anestheticRecordNum){
			int ARNum = anestheticRecordNum;
			string c="SELECT * FROM anesthmedsgiven WHERE AnestheticRecordNum ='" + anestheticRecordNum+ "'" + "ORDER BY DoseTimeStamp DESC"; //most recent at top of list
			DataTable table=General.GetTable(c);
			table.TableName="AnesthMedsGiven";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			AnestheticMedsGivenC.Listt=new List<AnestheticMedsGiven>();
			AnestheticMedsGiven medCur;
			for(int i=0;i<table.Rows.Count;i++){
				medCur=new AnestheticMedsGiven();
				medCur.IsNew = false;
				medCur.AnestheticMedNum  = PIn.PInt(table.Rows[i][0].ToString());
				medCur.AnestheticRecordNum   = PIn.PString(table.Rows[i][1].ToString());
				medCur.AnesthMedName         = PIn.PString(table.Rows[i][2].ToString());
                medCur.QtyGiven       = PIn.PString(table.Rows[i][3].ToString());
				medCur.QtyWasted           = PIn.PString(table.Rows[i][4].ToString());
				medCur.DoseTimeStamp = PIn.PString(table.Rows[i][5].ToString());
				medCur.AnesthMedNum = PIn.PString(table.Rows[i][6].ToString());
				AnestheticMedsGivenC.Listt.Add(medCur);
			}
		}

		///<Summary>Gets one Anesthetic Medication Supplier from the database.</Summary>
		public static AnestheticMedsGiven CreateObject(int AnestheticMedNum){
			return DataObjectFactory<AnestheticMedsGiven>.CreateObject(AnestheticMedNum);
		}

		public static List<AnestheticMedsGiven> GetAnesthMedsGiven(int[] AnestheticMedNums){
			Collection<AnestheticMedsGiven> collectState=DataObjectFactory<AnestheticMedsGiven>.CreateObjects(AnestheticMedNums);
			return new List<AnestheticMedsGiven>(collectState);		
		}

		///<summary></summary>
		public static void WriteObject(AnestheticMedsGiven AnesthMedName){
			DataObjectFactory<AnestheticMedsGiven>.WriteObject(AnesthMedName);
		}

		///<summary></summary>
		public static void DeleteObject(AnestheticMedsGiven AnesthMedName){
			//validate that not already in use.
			//string command="SELECT LName,FName FROM patient WHERE PharmacyNum="+POut.PInt(PharmacyNum);
			/*DataTable table=General.GetTable(command);
			string pats="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					pats+=", ";
				}
				pats+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			/*if(table.Rows.Count>0){
				throw new ApplicationException(Lan.g("Pharmacys","Pharmacy is already in use by patient(s). Not allowed to delete. "+pats));
			}*/
			//DataObjectFactory<AnestheticMedsGiven>.DeleteObject(medCur);
		}

		//public static void DeleteObject(int PharmacyNum){
		//	DataObjectFactory<Pharmacy>.DeleteObject(PharmacyNum);
		//}

		public static string GetAnestheticMedNum(int AnestheticMedNum){
			if(AnestheticMedNum==0){
				return "";
			}
			for(int i=0;i<AnestheticMedsGivenC.Listt.Count;i++){
				if(AnestheticMedsGivenC.Listt[i].AnestheticMedNum==AnestheticMedNum){
					return AnestheticMedsGivenC.Listt[i].AnesthMedName;
				}
			}
			return "";
		}


		/*public static int GetAnestheticMedNum (int AnestheticMedNum){
			int anestheticMedNum = AnestheticMedNum;
			return anestheticMedNum;
		}*/

		}
}


