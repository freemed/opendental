using System;
using System.Collections.Generic;
using System.Data;

namespace OpenDentalWebService{
	///<summary></summary>
	public class Prefs{

		///<summary>Only gets the preferences specifically used in the web application.  This query must be enhanced for new preferences desired in the web application.</summary>
		public static DataTable RefreshCache() {
			string command= "SELECT * FROM preference WHERE PrefName IN ("
				+"\""+OpenDentBusiness.PrefName.MainWindowTitle+"\""
				+",\""+OpenDentBusiness.PrefName.ShowIDinTitleBar+"\""
				+",\""+OpenDentBusiness.PrefName.PatientSelectUsesSearchButton+"\""
				+")";
			return OpenDentBusiness.DataCore.GetTable(command);
		}

	}
}