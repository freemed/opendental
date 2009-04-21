using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SupplyNeededs {

		///<summary>Gets all SupplyNeededs.</summary>
		public static List<SupplyNeeded> CreateObjects() {
			string command="SELECT * FROM supplyneeded ORDER BY DateAdded";
			return new List<SupplyNeeded>(DataObjectFactory<SupplyNeeded>.CreateObjects(command));
		}

		///<summary></summary>
		public static void WriteObject(SupplyNeeded supp){
			DataObjectFactory<SupplyNeeded>.WriteObject(supp);
		}

		///<summary></summary>
		public static void DeleteObject(SupplyNeeded supp){
			DataObjectFactory<SupplyNeeded>.DeleteObject(supp);
		}


	}

	


	


}









