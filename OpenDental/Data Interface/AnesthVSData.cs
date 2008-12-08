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


namespace OpenDental{
	///<summary></summary>
	public class AnesthVSData{

		public MySqlCommand cmd;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private static MySqlConnection con;

		///<summary>Gets those vital signs tied to a unique AnestheticRecordNum from the database</summary>
		public static List<AnestheticVSData> CreateObjects(int anestheticRecordNum) {
			string command="SELECT * FROM anesthvsdata WHERE AnestheticRecordNum='" + anestheticRecordNum + "'" + "ORDER BY VSTimeStamp DESC";
			return new List<AnestheticVSData>(DataObjectFactory<AnestheticVSData>.CreateObjects(command));
		}

		/*public static void WriteObject(AnesthVSData med){

			DataObjectFactory<AnesthVSData>.WriteObject(med);
		}*/

		///<summary>Surround with try-catch. Shouldn't need this</summary>
		/*public static void DeleteObject(AnestheticMedsGiven med){

			//validate that not already in use.
			string command="SELECT COUNT(*) FROM anesthmedsgiven WHERE DoseTimeStamp="+POut.PString(med.DoseTimeStamp);
			int count=PIn.PInt(General.GetCount(command));
            //disabled during development, will probably need to enable for release
			/*if(count>0) {
				throw new ApplicationException(Lan.g("AnestheticMeds","Anesthetic Medication is already in use. Not allowed to delete."));
			}*/
			/*command="SELECT COUNT(*) FROM anesthmedsgiven WHERE DoseTimeStamp="+POut.PString(med.DoseTimeStamp);
			count=PIn.PInt(General.GetCount(command));*/
            //disabled for now...
			/*if(count>0) {
				throw new ApplicationException(Lan.g("AnestheticMeds","Anesthetic Medication is already in use. Not allowed to delete."));
			}*/
            /*DataObjectFactory<AnestheticMedsGiven>.DeleteObject(med);
		}*/

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
			command2.CommandText = "SELECT Max(AnestheticRecordNum)  FROM anestheticrecord a, Patient p where a.Patnum = p.Patnum and p.patnum = " + patnum + "";
			command2.Connection = con;
			int supplierID = Convert.ToInt32(command2.ExecuteScalar());
			return supplierID;
			con.Close();
		}


	}

}

	











