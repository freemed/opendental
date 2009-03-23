using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.DataAccess;
using MySql.Data.MySqlClient;


namespace OpenDentBusiness{
	///<summary></summary>
	public class AnesthMedsGivens {

		public MySqlCommand cmd;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private static MySqlConnection con;

		///<summary>Gets those Anesthetic Medications tied to a unique AnestheticRecordNum from the database</summary>
		public static List<AnestheticMedsGiven> CreateObjects(int anestheticRecordNum) {
			string command="SELECT * FROM anesthmedsgiven WHERE AnestheticRecordNum='" + anestheticRecordNum + "'" + "ORDER BY DoseTimeStamp DESC";
			return new List<AnestheticMedsGiven>(DataObjectFactory<AnestheticMedsGiven>.CreateObjects(command));
		}

		public static void WriteObject(AnestheticMedsGiven med){

			DataObjectFactory<AnestheticMedsGiven>.WriteObject(med);
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(AnestheticMedsGiven med){

			//validate that not already in use.
			string command="SELECT COUNT(*) FROM anesthmedsgiven WHERE DoseTimeStamp="+POut.PString(med.DoseTimeStamp);
			int count=PIn.PInt(General.GetCount(command));
            //disabled during development, will probably need to enable for release
			/*if(count>0) {
				throw new ApplicationException(Lan.g("AnestheticMeds","Anesthetic Medication is already in use. Not allowed to delete."));
			}*/
			command="SELECT COUNT(*) FROM anesthmedsgiven WHERE DoseTimeStamp="+POut.PString(med.DoseTimeStamp);
			count=PIn.PInt(General.GetCount(command));
            //disabled for now...
			/*if(count>0) {
				throw new ApplicationException(Lan.g("AnestheticMeds","Anesthetic Medication is already in use. Not allowed to delete."));
			}*/
            DataObjectFactory<AnestheticMedsGiven>.DeleteObject(med);
		}

		public static string GetName(List<AnestheticMedsGiven> listAnesthMedsGiven, string doseTimeStamp)
		{
			for (int i = 0; i < listAnesthMedsGiven.Count; i++)
			{

				if (listAnesthMedsGiven[i].DoseTimeStamp == doseTimeStamp)
				{

					return listAnesthMedsGiven[i].AnesthMedName;
				}
			}
			return "";
		}

		public static string GetQtyGiven(List<AnestheticMedsGiven> listAnesthMedsGiven, string doseTimeStamp){
			for (int i = 0; i < listAnesthMedsGiven.Count; i++){

				if (listAnesthMedsGiven[i].DoseTimeStamp == doseTimeStamp){

					return listAnesthMedsGiven[i].QtyGiven;
				}
			}
			return "";
		}

		/// <summary>
		/// Gets the Anesthetic Record number from the anestheticrecord table.
		/// </summary>
		public static int getRecordNum(int patnum)
		{
			MySqlCommand command2 = new MySqlCommand();
			con.Open();
			command2.CommandText = "SELECT Max(AnestheticRecordNum)  FROM opendental_test.anestheticrecord a,opendental_test.Patient p where a.Patnum = p.Patnum and p.patnum = " + patnum + "";
			command2.Connection = con;
			int supplierID = Convert.ToInt32(command2.ExecuteScalar());
			return supplierID;
			con.Close();
		}

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
	}

}

	











