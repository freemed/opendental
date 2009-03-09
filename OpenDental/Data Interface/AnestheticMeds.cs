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
	public class AnestheticMeds {

		public MySqlCommand cmd;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private static MySqlConnection con;

		///<summary>Gets all Anesthetic Medications from the database</summary>
		public static List<AnesthMedsInventory> CreateObjects() {

			string command="SELECT * FROM anesthmedsinventory ORDER BY AnesthMedName";
			return new List<AnesthMedsInventory>(DataObjectFactory<AnesthMedsInventory>.CreateObjects(command));
		}

		public static void WriteObject(AnesthMedsInventory med){

			DataObjectFactory<AnesthMedsInventory>.WriteObject(med);
		}

		///<summary>Deletes and Anesthetic Medication from inventory if it has never been given to a patient</summary>
		public static void DeleteObject(AnesthMedsInventory med){

			//validate that anesthetic med is not already in use.
			string command = "SELECT COUNT(*) FROM anesthmedsgiven WHERE AnesthMedNum=" + POut.PInt(med.AnestheticMedNum);
			
			int count=PIn.PInt(General.GetCount(command));
            
			if (count > 0)
			{
				MessageBox.Show(Lan.g("AnestheticMeds", "Anesthetic Medication is already in use. Not allowed to delete."));
			}

			else
			{
				DataObjectFactory<AnesthMedsInventory>.DeleteObject(med);
			}
		}

		public static string GetName(List<AnesthMedsInventory> listAnesthMedInventory, int anestheticMedNum){

			for (int i = 0; i < listAnesthMedInventory.Count; i++)
			{

				if (listAnesthMedInventory[i].AnestheticMedNum == anestheticMedNum)
				{

					return listAnesthMedInventory[i].AnesthMedName;
				}
			}
			return "";
		}

		public static string GetQtyOnHand(List<AnesthMedsInventory> listAnesthMedInventory,int anestheticMedNum){
			
			for (int i = 0; i < listAnesthMedInventory.Count; i++){

				if (listAnesthMedInventory[i].AnestheticMedNum == anestheticMedNum){

					return listAnesthMedInventory[i].QtyOnHand;
				}
			}
			return "";
		}

		/// <summary>
		/// Gets the Anesthetic Record number from the anestheticrecord table.
		/// </summary>
		public static int getRecordNum(int patnum){

			MySqlCommand command2 = new MySqlCommand();
			con.Open();
			command2.CommandText = "SELECT Max(AnestheticRecordNum)  FROM opendental_test.anestheticrecord a,opendental_test.Patient p where a.Patnum = p.Patnum and p.patnum = " + patnum + "";
			command2.Connection = con;
			int supplierID = Convert.ToInt32(command2.ExecuteScalar());
			return supplierID;
			con.Close();
		}


	}

}

	











