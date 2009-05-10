using System;
using System.Collections;
using System.Data;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class RefAttachL{

		///<summary>Pass in all the refattaches for the patient.  This funtion finds the first referral from a Dr and returns that Dr's name.  Used in specialty practices.  Function is only used right now in the Dr. Ceph bridge.</summary>
		public static string GetReferringDr(RefAttach[] attachList){
			if(attachList.Length==0){
				return "";
			}
			if(!attachList[0].IsFrom){
				return "";
			}
			Referral referral=Referrals.GetReferral(attachList[0].ReferralNum);
			if(referral.PatNum!=0){
				return "";
			}
			string retVal=referral.FName+" "+referral.MName+" "+referral.LName;
			if(referral.Title!=""){
				retVal+=", "+referral.Title;
			}
			return retVal;
		}		

	}
}