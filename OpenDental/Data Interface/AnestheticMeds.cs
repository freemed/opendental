using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.DataAccess;

namespace OpenDental{
	///<summary></summary>
	public class AnestheticMeds {

		///<summary>Gets all AnestheticMeds</summary>
		public static List<AnesthMedsInventory> CreateObjects() {
			string command="SELECT * FROM anesthmedsinventory ORDER BY AnestheticMedNum";
			return new List<AnesthMedsInventory>(DataObjectFactory<AnesthMedsInventory>.CreateObjects(command));
		}

		///<summary></summary>
		public static void WriteObject(AnesthMedsInventory med){
			DataObjectFactory<AnesthMedsInventory>.WriteObject(med);
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(AnesthMedsInventory med){
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
			DataObjectFactory<AnesthMedsInventory>.DeleteObject(med);
		}

		public static string GetName(List<AnesthMedsInventory>listAnesthMedsInventory,int anestheticMedNum){
            for (int i = 0; i < listAnesthMedsInventory.Count; i++)
            {
                if (listAnesthMedsInventory[i].AnestheticMedNum == anestheticMedNum)
                {
                    return listAnesthMedsInventory[i].AnesthMedName;
                }
            }
			return "";
		}


	}

	


	


}









