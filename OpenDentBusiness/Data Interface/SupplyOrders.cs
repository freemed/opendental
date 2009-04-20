using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
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

		///<summary>No need to surround with try-catch.</summary>
		public static void DeleteObject(SupplyOrder order){
			//validate that not already in use-no
			//delete associated orderItems
			string command="DELETE FROM supplyorderitem WHERE SupplyOrderNum="+POut.PInt(order.SupplyOrderNum);
			Db.NonQ(command);
			DataObjectFactory<SupplyOrder>.DeleteObject(order);
		}

		

	}

	


	


}









