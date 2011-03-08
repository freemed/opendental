using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;
using OpenDental.Eclaims;

namespace TestCanada {
	class OutstandingTrans {

		private static string Run(int scriptNum,Carrier carrier,out List <Etrans> etransRequests) { 
			string retVal="";
			etransRequests=CanadianOutput.GetOutstandingTransactions(carrier);
			retVal+="Outstanding Transactions#"+scriptNum.ToString()+" successful.\r\n";
			return retVal;
		}

		public static string RunOne() {
			List<Etrans> etransRequests;
			Claim claim=Claims.GetClaim(ClaimTC.ClaimNums[0]);
			Carrier carrier=Carriers.GetCanadian("666666");
			carrier.CanadianEncryptionMethod=1;
			string retval=Run(1,carrier,out etransRequests);
			//EOB
			etransRequests[0].PatNum=claim.PatNum;
			etransRequests[0].PlanNum=claim.PlanNum;
			etransRequests[0].InsSubNum=claim.InsSubNum;
			string message=EtransMessageTexts.GetMessageText(etransRequests[0].EtransMessageTextNum);
			FormCCDPrint FormP=new FormCCDPrint(etransRequests[0],message);//Print the form. 
			FormP.Print();
			//Email
			etransRequests[1].PatNum=claim.PatNum;
			etransRequests[1].PlanNum=claim.PlanNum;
			etransRequests[1].InsSubNum=claim.InsSubNum;
			message=EtransMessageTexts.GetMessageText(etransRequests[1].EtransMessageTextNum);
			FormP=new FormCCDPrint(etransRequests[1],message);//Print the form. 
			FormP.Print();
			return retval;
		}

		public static string RunTwo() {
			//Carrier carrier=Carriers.GetCanadian("888888");
			//return Run(2,carrier);
			return "";
		}

		public static string RunThree() {
			//Carrier carrier=Carriers.GetCanadian("777777");
			//return Run(3,carrier);
			return "";
		}

	}
}
