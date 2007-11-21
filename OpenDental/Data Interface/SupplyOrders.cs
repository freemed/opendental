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
	public class SupplyOrders {

		///<summary>Gets all SupplyOrders for one supplier, ordered by date.</summary>
		public static List<SupplyOrder> CreateObjects(int supplierNum){
			string command="SELECT * FROM supplyorder "
				+"WHERE SupplierNum="+POut.PInt(supplierNum)
				+" ORDER BY DatePlaced";
			return new List<SupplyOrder>(DataObjectFactory<SupplyOrder>.CreateObjects(command));
		}

		///<summary></summary>
		public static void WriteObject(SupplyOrder order){
			DataObjectFactory<SupplyOrder>.WriteObject(order);
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(SupplyOrder order){
			//validate that not already in use.

			DataObjectFactory<SupplyOrder>.DeleteObject(order);
		}

		

	}

	


	


}









