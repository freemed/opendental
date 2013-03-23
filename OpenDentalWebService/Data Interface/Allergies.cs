using System;
using System.Collections.Generic;
using System.Data;

namespace OpenDentalWebService{
	///<summary></summary>
	public class Allergies {

		#region Only used for the patient portal

		///<summary>Returns the description and the reaction for all active allergies. </summary>
		public static DataTable GetActiveAllergiesPatientPortal(long patNum) {
			string command="SELECT  allergydef.Description,allergy.Reaction FROM allergy"
				+" LEFT JOIN allergydef on allergy.AllergyDefNum=allergydef.AllergyDefNum"
				+" WHERE allergy.PatNum = "+OpenDentBusiness.POut.Long(patNum)
				+" AND allergy.StatusIsActive = "+OpenDentBusiness.POut.Bool(true);//Get only active allergies.
			return OpenDentBusiness.DataCore.GetTable(command);
		}

		#endregion


	}
}