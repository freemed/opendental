using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

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
			return Crud.SupplyOrderItemCrud.SelectOne(command);
		}

		///<summary></summary>
		public static long Insert(SupplyOrderItem supp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				supp.SupplyOrderItemNum=Meth.GetLong(MethodBase.GetCurrentMethod(),supp);
				return supp.SupplyOrderItemNum;
			}
			return Crud.SupplyOrderItemCrud.Insert(supp);
		}

		///<summary></summary>
		public static void Update(SupplyOrderItem supp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),supp);
				return;
			}
			Crud.SupplyOrderItemCrud.Update(supp);
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(SupplyOrderItem supp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),supp);
				return;
			}
			//validate that not already in use.
			Crud.SupplyOrderItemCrud.Delete(supp.SupplyOrderItemNum);
		}

		

		
		


	}
	
	


	


}









