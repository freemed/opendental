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
	public class Supplies {

		///<summary>Gets all Supplies, ordered by category and itemOrder.  Optionally hides those marked IsHidden.</summary>
		public static List<Supply> CreateObjects(bool includeHidden,int supplierNum) {
			string command="SELECT supply.* FROM supply,definition "
				+"WHERE definition.DefNum=supply.Category "
				+"AND supply.SupplierNum="+POut.PInt(supplierNum)+" ";
			//if(!includeHiddenCats){//there's no such thing as hidden categories with supplies
			//	command+="AND definition.IsHidden=0 ";
			//}
			if(!includeHidden){
				command+="AND supply.IsHidden=0 ";
			}
			command+="ORDER BY definition.ItemOrder,supply.ItemOrder";
			return new List<Supply>(DataObjectFactory<Supply>.CreateObjects(command));
		}

		///<summary></summary>
		public static void WriteObject(Supply supp){
			DataObjectFactory<Supply>.WriteObject(supp);
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(Supply supp){
			//validate that not already in use.

			DataObjectFactory<Supply>.DeleteObject(supp);
		}

		


	}

	


	


}









