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
			string command="SELECT * FROM anesthmedsinventory ORDER BY AnestheticMedNum";
			return new List<AnesthMedsInventory>(DataObjectFactory<AnesthMedsInventory>.CreateObjects(command));
		}

		public static void WriteObject(AnesthMedsInventory med){

			DataObjectFactory<AnesthMedsInventory>.WriteObject(med);
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(AnesthMedsInventory med){

			//validate that not already in use.
			string command="SELECT COUNT(*) FROM anesthmedsinventory WHERE AnestheticMedNum="+POut.PInt(med.AnestheticMedNum);
			int count=PIn.PInt(General.GetCount(command));
            //disabled during development, will probably need to enable for release
			/*if(count>0) {
				throw new ApplicationException(Lan.g("AnestheticMeds","Anesthetic Medication is already in use. Not allowed to delete."));
			}*/
			command="SELECT COUNT(*) FROM anesthmedsinventory WHERE AnestheticMedNum="+POut.PInt(med.AnestheticMedNum);
			count=PIn.PInt(General.GetCount(command));
            //disabled for now...
			/*if(count>0) {
				throw new ApplicationException(Lan.g("AnestheticMeds","Anesthetic Medication is already in use. Not allowed to delete."));
			}*/
            DataObjectFactory<AnesthMedsInventory>.DeleteObject(med);
		}

		public static string GetName(List<AnesthMedsInventory> listAnesthMedInventory,int anestheticMedNum){
			for (int i = 0; i < listAnesthMedInventory.Count; i++){

				if (listAnesthMedInventory[i].AnestheticMedNum == anestheticMedNum){

					return listAnesthMedInventory[i].AnesthMedName;
				}
			}
			return "";
		}

		
	}


}

	











