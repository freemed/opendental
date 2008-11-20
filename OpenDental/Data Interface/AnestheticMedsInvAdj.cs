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
	public class AnestheticMedsInvAdj {

		public MySqlCommand cmd;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private static MySqlConnection con;

		///<summary>Gets all Anesthetic Medications from the database</summary>
		public static List<AnesthMedsInventoryAdjT> CreateObjects() {
			string command="SELECT * FROM anesthmedsinventoryadj ORDER BY TimeStamp DESC";
			return new List<AnesthMedsInventoryAdjT>(DataObjectFactory<AnesthMedsInventoryAdjT>.CreateObjects(command));
		}

		public static void WriteObject(AnesthMedsInventoryAdjT adj){

			DataObjectFactory<AnesthMedsInventoryAdjT>.WriteObject(adj);
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(AnesthMedsInventoryAdjT adj){

			//validate that not already in use.
			string command="SELECT COUNT(*) FROM anesthmedsinventoryadj WHERE AnestheticMedNum="+POut.PInt(adj.AnestheticMedNum);
			int count=PIn.PInt(General.GetCount(command));
            //disabled during development, will probably need to enable for release
			/*if(count>0) {
				throw new ApplicationException(Lan.g("AnestheticMeds","Anesthetic Medication is already in use. Not allowed to delete."));
			}*/
			command="SELECT COUNT(*) FROM anesthmedsinventoryadj WHERE AnestheticMedNum="+POut.PInt(adj.AnestheticMedNum);
			count=PIn.PInt(General.GetCount(command));
            //disabled for now...
			/*if(count>0) {
				throw new ApplicationException(Lan.g("AnestheticMeds","Anesthetic Medication is already in use. Not allowed to delete."));
			}*/
            DataObjectFactory<AnesthMedsInventoryAdjT>.DeleteObject(adj);
		}

		/*public static string GetName(List<AnesthMedsInventoryAdj> listAnesthMedInventory, int anestheticMedNum)
		{
			for (int i = 0; i < listAnesthMedInventory.Count; i++)
			{

				if (listAnesthMedInventory[i].AnestheticMedNum == anestheticMedNum)
				{

					return listAnesthMedInventory[i].AnesthMedName;
				}
			}
			return "";
		}*/

		public static int GetQtyAdj(List<AnesthMedsInventoryAdjT> listAnesthMedInventoryAdj,int anestheticMedNum){
			for (int i = 0; i < listAnesthMedInventoryAdj.Count; i++){

				if (listAnesthMedInventoryAdj[i].AnestheticMedNum == anestheticMedNum){

					return listAnesthMedInventoryAdj[i].QtyAdj;
				}
			}
			return 0;
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

	}

}

	











