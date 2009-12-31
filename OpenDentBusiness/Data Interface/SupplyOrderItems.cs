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

		public static DataTable GetItemsForOrder(long orderNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),orderNum);
			}
			string command="SELECT CatalogNumber,Descript,Qty,supplyorderitem.Price,SupplyOrderItemNum,supplyorderitem.SupplyNum "
				+"FROM supplyorderitem,definition,supply "
				+"WHERE definition.DefNum=supply.Category "
				+"AND supply.SupplyNum=supplyorderitem.SupplyNum "
				+"AND supplyorderitem.SupplyOrderNum="+POut.Long(orderNum)+" "
				+"ORDER BY definition.ItemOrder,supply.ItemOrder";
			return Db.GetTable(command);
		}

		public static SupplyOrderItem CreateObject(long supplyOrderItemNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<SupplyOrderItem>(MethodBase.GetCurrentMethod(),supplyOrderItemNum);
			}
			string command="SELECT * FROM supplyorderitem WHERE SupplyOrderItemNum="+POut.Long(supplyOrderItemNum);
			return DataObjectFactory<SupplyOrderItem>.CreateObject(command);
		}

		///<summary></summary>
		public static long WriteObject(SupplyOrderItem supp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				supp.SupplyOrderItemNum=Meth.GetLong(MethodBase.GetCurrentMethod(),supp);
				return supp.SupplyOrderItemNum;
			}
			DataObjectFactory<SupplyOrderItem>.WriteObject(supp);
			return supp.SupplyOrderItemNum;
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(SupplyOrderItem supp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),supp);
				return;
			}
			//validate that not already in use.

			DataObjectFactory<SupplyOrderItem>.DeleteObject(supp);
		}

		

		
		


	}
	
	


	


}









