using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;
using OpenDental.Eclaims;

namespace TestCanada {
	class PaymentReconciliation {

		private static string Run(int scriptNum,bool sendToItrans,Carrier carrier,Provider treatProv,Provider billingProv,DateTime reconciliationDate,out List<Etrans> etransAcks) { 
			string retVal="";
			etransAcks=CanadianOutput.GetPaymentReconciliations(sendToItrans,carrier,treatProv,billingProv,reconciliationDate);
			retVal+="Payment Reconciliation#"+scriptNum.ToString()+" successful.\r\n";
			return retVal;
		}

		public static string RunOne() {
			long carrierNum=CarrierTC.GetCarrierNumById("666666");
			Carrier carrier=Carriers.GetCarrier(carrierNum);
			Provider prov=Providers.GetProv(PrefC.GetLong(PrefName.PracticeDefaultProv));
			List <Etrans> etransAcks;
			return Run(1,false,carrier,prov,prov,new DateTime(1999,6,16),out etransAcks);
		}

		public static string RunTwo() {
			CanadianNetwork network=new CanadianNetwork();
			network.Descript="Network 2";
			network.Abbrev="Network 2";
			network.CanadianNetworkNum=2;
			network.CanadianTransactionPrefix="A";
			Provider prov=Providers.GetProv(PrefC.GetLong(PrefName.PracticeDefaultProv));
			List<Etrans> etransAcks;
			return Run(2,true,null,prov,prov,new DateTime(1999,6,16),out etransAcks);
		}

		public static string RunThree() {
			long carrierNum=CarrierTC.GetCarrierNumById("111555");
			Carrier carrier=Carriers.GetCarrier(carrierNum);
			Provider prov=Providers.GetProv(PrefC.GetLong(PrefName.PracticeDefaultProv));
			List<Etrans> etransAcks;
			return Run(3,false,carrier,prov,prov,new DateTime(1999,6,16),out etransAcks);
		}

	}
}
