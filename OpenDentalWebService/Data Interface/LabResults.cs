using System;
using System.Collections.Generic;
using System.Data;

namespace OpenDentalWebService{
	///<summary></summary>
	public class LabResults {

		#region Only used for the patient portal

		///<summary></summary>
		public static List<OpenDentBusiness.LabResult> GetResultsFromPanelsPatientPortal(List<long> panelNums) {
			List<OpenDentBusiness.LabResult> results=new List<OpenDentBusiness.LabResult>();
			if(panelNums.Count==0) {
				return results;
			}
			string command="SELECT * FROM labresult WHERE LabPanelNum IN (";
			for(int i=0;i<panelNums.Count;i++) {
				if(i>0) {
					command+=",";
				}
				command+=OpenDentBusiness.POut.Long(panelNums[i]);
			}
			command+=")";
			return OpenDentBusiness.Crud.LabResultCrud.SelectMany(command);
		}

		#endregion


	}
}