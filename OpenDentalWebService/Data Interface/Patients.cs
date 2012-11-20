using System;
using System.Collections.Generic;
using System.Data;

namespace OpenDentalWebService {
	public class Patients {

		///<summary></summary>
		public static DataTable GetPtDataTableTest(long guarNum,long excludePayNum) {
			DataTable table=new DataTable();
			table=OpenDentBusiness.Patients.GetPaymentStartingBalances(guarNum,excludePayNum);
			return table;
		}


	}
}