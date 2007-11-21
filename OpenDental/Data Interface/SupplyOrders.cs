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

		///<summary>Gets all SupplyOrders, ordered by date.</summary>
		public static List<SupplyOrder> CreateObjects(){
			string command="SELECT * FROM supplyorder "
				+"ORDER BY DatePlaced";
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









