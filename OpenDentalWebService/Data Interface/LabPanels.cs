using System;
using System.Collections.Generic;
using System.Data;

namespace OpenDentalWebService{
	///<summary></summary>
	public class LabPanels {

		#region Only used for the patient portal

		///<summary></summary>
		public static List<OpenDentBusiness.LabPanel> GetAllPatientPortal(long patNum) {
			string command="SELECT * FROM labpanel WHERE PatNum = "+OpenDentBusiness.POut.Long(patNum);
			return OpenDentBusiness.Crud.LabPanelCrud.SelectMany(command);
		}

		#endregion


	}
}