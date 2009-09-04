using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FeeL {

		///<summary>If the named fee schedule does not exist, then it will be created.  It always returns the defnum for the feesched used, regardless of whether it already existed.  procCode must have already been tested for valid code, and feeSchedName must not be blank.</summary>
		public static long ImportTrojan(string procCode,double amt,string feeSchedName) {
			FeeSched feeSched=FeeScheds.GetByExactName(feeSchedName);
			//if isManaged, then this should be done differently from here on out.
			if(feeSched==null){
				//add the new fee schedule
				feeSched=new FeeSched();
				feeSched.ItemOrder=FeeSchedC.ListLong.Count;
				feeSched.Description=feeSchedName;
				feeSched.FeeSchedType=FeeScheduleType.Normal;
				feeSched.IsNew=true;
				FeeScheds.WriteObject(feeSched);
				//Cache.Refresh(InvalidType.FeeScheds);
				//Fees.Refresh();
				DataValid.SetInvalid(InvalidType.FeeScheds, InvalidType.Fees);
			}
			if(feeSched.IsHidden){
				feeSched.IsHidden=false;//unhide it
				FeeScheds.WriteObject(feeSched);
				DataValid.SetInvalid(InvalidType.FeeScheds);
			}
			Fee fee=Fees.GetFee(ProcedureCodes.GetCodeNum(procCode),feeSched.FeeSchedNum);
			if(fee==null) {
				fee=new Fee();
				fee.Amount=amt;
				fee.FeeSched=feeSched.FeeSchedNum;
				fee.CodeNum=ProcedureCodes.GetCodeNum(procCode);
				Fees.Insert(fee);
			}
			else{
				fee.Amount=amt;
				Fees.Update(fee);
			}
			return feeSched.FeeSchedNum;
		}	

	}
}