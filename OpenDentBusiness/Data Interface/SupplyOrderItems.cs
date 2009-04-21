using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	
	///<summary></summary>
	public class SupplyOrderItems {

		public static DataTable GetItemsForOrder(int orderNum){
			string command="SELECT CatalogNumber,Descript,Qty,supplyorderitem.Price,SupplyOrderItemNum,supplyorderitem.SupplyNum "
				+"FROM supplyorderitem,definition,supply "
				+"WHERE definition.DefNum=supply.Category "
				+"AND supply.SupplyNum=supplyorderitem.SupplyNum "
				+"AND supplyorderitem.SupplyOrderNum="+POut.PInt(orderNum)+" "
				+"ORDER BY definition.ItemOrder,supply.ItemOrder";
			return Db.GetTable(command);
		}

		public static SupplyOrderItem CreateObject(int supplyOrderItemNum){
			string command="SELECT * FROM supplyorderitem WHERE SupplyOrderItemNum="+POut.PInt(supplyOrderItemNum);
			return DataObjectFactory<SupplyOrderItem>.CreateObject(command);
		}

		///<summary></summary>
		public static void WriteObject(SupplyOrderItem supp){
			DataObjectFactory<SupplyOrderItem>.WriteObject(supp);
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(SupplyOrderItem supp){
			//validate that not already in use.

			DataObjectFactory<SupplyOrderItem>.DeleteObject(supp);
		}

		

		
		


	}
	
	


	


}









