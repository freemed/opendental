using System;
using System.Collections.Generic;
using System.Data;

namespace OpenDentalWebService{
	///<summary></summary>
	public class Diseases {

		#region Only used for the patient portal

		///<summary>Returns the ICD9 Descriptions for all problems that the patient currently has.</summary>
		public static DataTable GetActiveDiseasesPatientPortal(long patNum) {
			string command="SELECT  icd9.Description FROM disease  LEFT JOIN icd9 on icd9.ICD9Num=disease.ICD9Num "
				+"WHERE disease.PatNum = "+OpenDentBusiness.POut.Long(patNum)
					+" AND disease.ProbStatus = "+OpenDentBusiness.POut.Int((int)OpenDentBusiness.ProblemStatus.Active) //Get only active diseases.
					+" AND disease.ICD9Num!=0";//Get only ICD9NUM which are not zero. ICD9NUM and DiseaseDefNum are mutually exculsive. If one is zero the other is not.
			return OpenDentBusiness.DataCore.GetTable(command);
		}

		#endregion


	}
}