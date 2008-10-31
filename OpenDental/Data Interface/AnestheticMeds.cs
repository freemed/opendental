using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.DataAccess;

namespace OpenDental{
	///<summary></summary>
	public class AnestheticMeds {

		///<summary>Gets all AnestheticMeds</summary>
		public static List<AnesthMedInventory> CreateObjects() {
			string command="SELECT * FROM anesthmedsinventory ORDER BY AnestheticMedNum";
			return new List<AnesthMedInventory>(DataObjectFactory<AnesthMedInventory>.CreateObjects(command));
		}

		///<summary></summary>
		public static void WriteObject(AnesthMedInventory med){
			DataObjectFactory<AnesthMedInventory>.WriteObject(med);
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(AnesthMedInventory med){
			//validate that not already in use.
			string command="SELECT COUNT(*) FROM anesthmedsinventory WHERE AnestheticMedNum="+POut.PInt(med.AnestheticMedNum);
			int count=PIn.PInt(General.GetCount(command));
			if(count>0) {
				throw new ApplicationException(Lan.g("AnestheticMeds","Anesthetic Medication is already in use. Not allowed to delete."));
			}
			command="SELECT COUNT(*) FROM anesthmedsinventory WHERE AnestheticMedNum="+POut.PInt(med.AnestheticMedNum);
			count=PIn.PInt(General.GetCount(command));
			if(count>0) {
				throw new ApplicationException(Lan.g("AnestheticMeds","Anesthetic Medication is already in use. Not allowed to delete."));
			}
            DataObjectFactory<AnesthMedInventory>.DeleteObject(med);
		}

		public static string GetName(List<AnesthMedInventory> listAnesthMedInventory,int anestheticMedNum){
            for (int i = 0; i < listAnesthMedInventory.Count; i++)
            {
                if (listAnesthMedInventory[i].AnestheticMedNum == anestheticMedNum)
                {
                    return listAnesthMedInventory[i].AnesthMedName;
                }
            }
			return "";
		}


	}

	


	


}









