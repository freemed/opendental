using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;
using OpenDental.Eclaims;

namespace TestCanada {
	public class Reversal {

		public static string Run(int scriptNum,string responseExpected,Claim claim,bool showForms) {
			string retVal="";
			InsPlan insPlan=InsPlans.GetPlan(claim.PlanNum,null);
			InsSub insSub=InsSubs.GetOne(claim.InsSubNum);
			long etransNum=CanadianOutput.SendClaimReversal(claim,insPlan,insSub);
			Etrans etrans=Etranss.GetEtrans(etransNum);
			string message=EtransMessageTexts.GetMessageText(etrans.EtransMessageTextNum);
			CCDFieldInputter formData=new CCDFieldInputter(message);
			string responseStatus=formData.GetValue("G05");
			if(responseStatus!=responseExpected) {
			  return "G05 should be "+responseExpected+"\r\n";
			}
			retVal+="Reversal #"+scriptNum.ToString()+" successful.\r\n";
			return retVal;
		}

		public static string RunOne(bool showForms) {
			Claim claim=Claims.GetClaim(ClaimTC.ClaimNums[1]);
			return Run(1,"A",claim,showForms);
		}

		public static string RunTwo(bool showForms) {
			throw new ApplicationException("Not yet implemented.");
		}

		public static string RunThree(bool showForms) {
			throw new ApplicationException("Not yet implemented.");
		}

		public static string RunFour(bool showForms) {
			throw new ApplicationException("Not yet implemented.");
		}

	}
}
