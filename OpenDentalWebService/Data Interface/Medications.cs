using System;
using System.Collections.Generic;
using System.Data;

namespace OpenDentalWebService{
	///<summary></summary>
	public class Medications {

		#region Only used for the patient portal

		///<summary>Returns the MedName for all medications that the patient is currently taking.</summary>
		public static DataTable GetAllMedNamesPatientPortal(long patNum) {
			string command="SELECT  medication.MedName FROM medicationpat"
				+" LEFT JOIN medication on medicationpat.MedicationNum=medication.MedicationNum"
				+" WHERE medicationpat.PatNum = "+OpenDentBusiness.POut.Long(patNum)
				+" AND medicationpat.DateStop = "+OpenDentBusiness.POut.Date(DateTime.MinValue);//Filter out discontinued medications.
			return OpenDentBusiness.DataCore.GetTable(command);
		}

		#endregion


	}
}