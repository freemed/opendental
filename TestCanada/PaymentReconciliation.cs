using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;
using OpenDental.Eclaims;

namespace TestCanada {
	class PaymentReconciliation {

		private static string Run(int scriptNum,Carrier carrier,Provider treatProv,Provider billingProv,DateTime reconciliationDate,out List<Etrans> etransAcks) { 
			string retVal="";
			etransAcks=CanadianOutput.GetPaymentReconciliations(carrier,treatProv,billingProv,reconciliationDate);
			retVal+="Payment Reconciliation#"+scriptNum.ToString()+" successful.\r\n";
			return retVal;
		}

		public static string RunOne() {
			long carrierNum=CarrierTC.GetCarrierNumById("666666");
			Carrier carrier=Carriers.GetCarrier(carrierNum);
			Provider prov=Providers.GetProv(PrefC.GetLong(PrefName.PracticeDefaultProv));
			List <Etrans> etransAcks;
			return Run(1,carrier,prov,prov,new DateTime(1999,6,16),out etransAcks);
		}

		public static string RunTwo() {
			long carrierNum=CarrierTC.GetCarrierNumById("777777");
			Carrier carrier=Carriers.GetCarrier(carrierNum);
			Provider prov=Providers.GetProv(PrefC.GetLong(PrefName.PracticeDefaultProv));
			List<Etrans> etransAcks;
			return Run(1,carrier,prov,prov,new DateTime(1999,6,16),out etransAcks);
		}

		public static string RunThree() {
			long carrierNum=CarrierTC.GetCarrierNumById("111555");
			Carrier carrier=Carriers.GetCarrier(carrierNum);
			Provider prov=Providers.GetProv(PrefC.GetLong(PrefName.PracticeDefaultProv));
			List<Etrans> etransAcks;
			return Run(1,carrier,prov,prov,new DateTime(1999,6,16),out etransAcks);
		}

	}
}
