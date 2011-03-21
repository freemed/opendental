using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;
using OpenDental.Eclaims;

namespace TestCanada {
	class PaymentReconciliation {

		private static string Run(int scriptNum,Carrier carrier,CanadianNetwork network,Provider prov,Provider billingProv,out List <Etrans> etransAcks,DateTime reconciliationDate) { 
			string retVal="";
			etransAcks=CanadianOutput.GetPaymentReconciliations(carrier,network,prov,billingProv,reconciliationDate);
			retVal+="Payment Reconciliation#"+scriptNum.ToString()+" successful.\r\n";
			return retVal;
		}

		public static string RunOne() {
			long carrierNum=CarrierTC.GetCarrierNumById("666666");
			Carrier carrier=Carriers.GetCarrier(carrierNum);
			//todo set reconciliation date for carrier.
			Provider prov=Providers.GetProv(PrefC.GetLong(PrefName.PracticeDefaultProv));
			List <Etrans> etransAcks;
			return Run(1,carrier,null,prov,prov,out etransAcks,new DateTime(1999,6,16));
		}

		public static string RunTwo() {
			CanadianNetwork network=new CanadianNetwork();
			network.Descript="Network 2";
			network.Abbrev="Network 2";
			network.CanadianNetworkNum=2;
			network.CanadianTransactionPrefix="A";
			//todo set reconciliation date for carrier.
			Provider prov=Providers.GetProv(PrefC.GetLong(PrefName.PracticeDefaultProv));
			List<Etrans> etransAcks;
			return Run(2,null,network,prov,prov,out etransAcks,new DateTime(1999,6,16));
		}

		public static string RunThree() {
			long carrierNum=CarrierTC.GetCarrierNumById("111555");
			Carrier carrier=Carriers.GetCarrier(carrierNum);
			//todo set reconciliation date for carrier.
			Provider prov=Providers.GetProv(PrefC.GetLong(PrefName.PracticeDefaultProv));
			List<Etrans> etransAcks;
			return Run(3,carrier,null,prov,prov,out etransAcks,new DateTime(1999,6,16));
		}

	}
}
