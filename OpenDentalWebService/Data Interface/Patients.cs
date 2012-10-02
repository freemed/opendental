using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenDentalWebService {
	public class Patients {
		///<summary>Gets the patient and provider balances for all patients in the family.  Used from the payment window to help visualize and automate the family splits.</summary>
		public static DataTable GetPaymentStartingBalances(long guarNum,long excludePayNum) {
			return OpenDentBusiness.Patients.GetPaymentStartingBalances(guarNum,excludePayNum);
		}

		///<summary>Gets the patient and provider balances for all patients in the family.  Used from the payment window to help visualize and automate the family splits.  groupByProv means group by provider only not provider/clinic.</summary>
		public static DataTable GetPaymentStartingBalances(long guarNum,long excludePayNum,bool groupByProv) {
			return OpenDentBusiness.Patients.GetPaymentStartingBalances(guarNum,excludePayNum,groupByProv);
		}


	}
}