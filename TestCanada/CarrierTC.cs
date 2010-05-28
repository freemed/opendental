using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace TestCanada {
	public class CarrierTC {
		public static string SetInitialCarriers() {
			//We are starting with zero carriers
			Carrier carrier=new Carrier();
			carrier.CarrierName="Carrier 1";
			carrier.CanadianTransactionPrefix="CDANET14";
			carrier.ElectID="666666";
			carrier.CanadianEncryptionMethod=2;
			carrier.CanadianSupportedTypes
				//claim_01 is implied
				= CanSupTransTypes.CobClaimTransaction_07
				//claimAck_11 is implied
				| CanSupTransTypes.ClaimAckEmbedded_11e
				//claimEob_21 is implied
				| CanSupTransTypes.ClaimEobEmbedded_21e
				| CanSupTransTypes.ClaimReversal_02
				| CanSupTransTypes.ClaimReversalResponse_12
				| CanSupTransTypes.PredeterminationSinglePage_03
				| CanSupTransTypes.PredeterminationMultiPage_03
				| CanSupTransTypes.PredeterminationAck_13
				| CanSupTransTypes.PredeterminationAckEmbedded_13e
				| CanSupTransTypes.RequestForOutstandingTrans_04
				| CanSupTransTypes.EmailTransaction_24
				| CanSupTransTypes.RequestForSummaryReconciliation_05
				| CanSupTransTypes.SummaryReconciliation_15;
			Carriers.Insert(carrier);
			//Carrier2---------------------------------------------------



			Carriers.RefreshCache();
			return "Carrier objects set.";
		}

	}
}
