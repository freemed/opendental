using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class ReferralL{
	
		///<summary>Gets Referral info from memory. Does not make a call to the database unless needed.</summary>
		public static Referral GetReferral(int referralNum){
			if(referralNum==0){
				return null;
			}
			for(int i=0;i<Referrals.List.Length;i++) {
				if(Referrals.List[i].ReferralNum==referralNum) {
					return Referrals.List[i].Copy();
				}
			}
			Referrals.Refresh();//must be outdated
			for(int i=0;i<Referrals.List.Length;i++) {
				if(Referrals.List[i].ReferralNum==referralNum) {
					return Referrals.List[i].Copy();
				}
			}
			MessageBox.Show("Error.  Referral not found: "+referralNum.ToString());
			return null;
		}

		///<summary>Gets the first referral "from" for the given patient.  Will return null if no "from" found for patient.</summary>
		public static Referral GetReferralForPat(int patNum) {
			RefAttach[] RefAttachList=RefAttaches.Refresh(patNum);
			for(int i=0;i<RefAttachList.Length;i++) {
				if(RefAttachList[i].IsFrom) {
					return GetReferral(RefAttachList[i].ReferralNum);
				}
			}
			return null;
		}

	}
}