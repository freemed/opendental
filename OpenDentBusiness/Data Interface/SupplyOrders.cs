using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SupplyOrders {

		///<summary>Gets all SupplyOrders for one supplier, ordered by date.</summary>
		public static List<SupplyOrder> CreateObjects(long supplierNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<SupplyOrder>>(MethodBase.GetCurrentMethod(),supplierNum);
			}
			string command="SELECT * FROM supplyorder "
				+"WHERE SupplierNum="+POut.Long(supplierNum)
				+" ORDER BY DatePlaced";
			return new List<SupplyOrder>(DataObjectFactory<SupplyOrder>.CreateObjects(command));
		}

		///<summary></summary>
		public static long WriteObject(SupplyOrder order) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				order.SupplyOrderNum=Meth.GetLong(MethodBase.GetCurrentMethod(),order);
				return order.SupplyOrderNum;
			}
			DataObjectFactory<SupplyOrder>.WriteObject(order);
			return order.SupplyOrderNum;
		}

		///<summary>No need to surround with try-catch.</summary>
		public static void DeleteObject(SupplyOrder order){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),order);
				return;
			}
			//validate that not already in use-no
			//delete associated orderItems
			string command="DELETE FROM supplyorderitem WHERE SupplyOrderNum="+POut.Long(order.SupplyOrderNum);
			Db.NonQ(command);
			DataObjectFactory<SupplyOrder>.DeleteObject(order);
		}

		

	}

	


	


}









