using System;
using System.Collections.Generic;
using System.Data;

namespace OpenDentalWebService{
	///<summary></summary>
	public class Prefs{

		///<summary>This should be enhanced to only grab the preferences that the web version will need to use.</summary>
		public static DataTable RefreshCache() {
			string command= "SELECT * FROM preference";
			return OpenDentBusiness.DataCore.GetTable(command);
		}

	}
}