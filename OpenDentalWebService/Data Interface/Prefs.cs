using System;
using System.Collections.Generic;
using System.Data;

namespace OpenDentalWebService{
	///<summary></summary>
	public class Prefs{

		public static DataTable RefreshCache() {
			string command= "SELECT * FROM preference";
			return OpenDentBusiness.DataCore.GetTable(command);
		}

	}
}