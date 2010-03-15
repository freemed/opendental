using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	class ClaimProcT {
		/// <summary>Tests 1,2</summary>
		public static string TestDualPPO(int specificTest) {
			if(specificTest != 0 && specificTest != 1 && specificTest != 2){
				return"";
			}
			//Patient
			Patient pat=new Patient();
			pat.IsNew=true;
			pat.LName="DualPPOPat";
			pat.FName="John";
			pat.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
			pat.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);//This causes standard fee sched to be 53.
			long patNum=Patients.Insert(pat,false);
			//Fee Schedules
			FeeSched feeSched=new FeeSched();
			feeSched.IsNew=true;
			feeSched.FeeSchedType=FeeScheduleType.Normal;
			feeSched.Description="DualPPO1";
			long feeSchedNum1=FeeScheds.WriteObject(feeSched);
			feeSched=new FeeSched();
			feeSched.IsNew=true;
			feeSched.FeeSchedType=FeeScheduleType.Normal;
			feeSched.Description="DualPPO2";
			long feeSchedNum2=FeeScheds.WriteObject(feeSched);
			FeeScheds.RefreshCache();
			//Standard Fee
			Fees.RefreshCache();
			long codeNum=ProcedureCodes.GetCodeNum("D2750");
			Fee fee=Fees.GetFee(codeNum,53);
			if(fee==null) {
				fee=new Fee();
				fee.CodeNum=codeNum;
				fee.FeeSched=53;
				fee.Amount=1200;
				Fees.Insert(fee);
			}
			else {
				fee.Amount=1200;
				Fees.Update(fee);
			}
			//PPO fees
			fee=new Fee();
			fee.CodeNum=codeNum;
			fee.FeeSched=feeSchedNum1;
			fee.Amount=900;
			Fees.Insert(fee);
			fee=new Fee();
			fee.CodeNum=codeNum;
			fee.FeeSched=feeSchedNum2;
			fee.Amount=650;
			Fees.Insert(fee);
			Fees.RefreshCache();
			//Carrier
			Carrier carrier=new Carrier();
			carrier.CarrierName="CarrierDualPPO";
			long carrierNum=Carriers.Insert(carrier);
			//InsPlans
			InsPlan plan=new InsPlan();
			plan.Subscriber=patNum;
			plan.CarrierNum=carrierNum;
			plan.SubscriberID="1234";
			plan.PlanType="p";
			plan.FeeSched=feeSchedNum1;
			long planNum1=InsPlans.Insert(plan);
			plan=new InsPlan();
			plan.Subscriber=patNum;
			plan.CarrierNum=carrierNum;
			plan.SubscriberID="1234";
			plan.PlanType="p";
			plan.FeeSched=feeSchedNum2;
			long planNum2=InsPlans.Insert(plan);
			//Benefits
			Benefit ben=new Benefit();
			ben.PlanNum=planNum1;
			ben.BenefitType=InsBenefitType.CoInsurance;
			ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Crowns).CovCatNum;
			ben.CoverageLevel=BenefitCoverageLevel.None;
			ben.Percent=50;
			ben.MonetaryAmt=-1;
			ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			Benefits.Insert(ben);
			ben=new Benefit();
			ben.PlanNum=planNum2;
			ben.BenefitType=InsBenefitType.CoInsurance;
			ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Crowns).CovCatNum;
			ben.CoverageLevel=BenefitCoverageLevel.None;
			ben.Percent=50;
			ben.MonetaryAmt=-1;
			ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			Benefits.Insert(ben);
			//PatPlans
			PatPlan patPlan=new PatPlan();
			patPlan.Ordinal=1;
			patPlan.PatNum=patNum;
			patPlan.PlanNum=planNum1;
			PatPlans.Insert(patPlan);
			patPlan=new PatPlan();
			patPlan.Ordinal=2;
			patPlan.PatNum=patNum;
			patPlan.PlanNum=planNum2;
			PatPlans.Insert(patPlan);
			//Procedure
			Procedure proc=new Procedure();
			proc.CodeNum=codeNum;
			proc.PatNum=patNum;
			proc.ProcDate=DateTime.Today;
			proc.ProcStatus=ProcStat.TP;
			proc.ProvNum=pat.PriProv;
			proc.ToothNum="8";
			proc.ProcFee=Fees.GetAmount0(codeNum,53);
			long procNum=Procedures.Insert(proc);
			//ClaimProcs
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
			Family fam=Patients.GetFamily(patNum);
			List<InsPlan> planList=InsPlans.Refresh(fam);
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			//Validate
			string retVal="";
			ClaimProc claimProc;
			if(specificTest==0 || specificTest==1){
				Procedures.ComputeEstimates(proc,patNum,ref claimProcs,false,planList,patPlans,benefitList,histList,loopList,true,pat.Age);
				claimProcs=ClaimProcs.Refresh(patNum);
				claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum1);
				//I don't think allowed can be easily tested on the fly, and it's not that important.
				if(claimProc.InsEstTotal!=450) {
					throw new Exception("Should be 450");
				}
				if(claimProc.WriteOffEst!=300) {
					throw new Exception("Should be 300");
				}
				claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum2);
				if(claimProc.InsEstTotal!=200) {
					throw new Exception("Should be 200");
				}
				if(claimProc.WriteOffEst!=0) {
					throw new Exception("Should be 0");
				}
				retVal+="1: Passed.  Claim proc estimates for dual PPO ins.  Allowed1 greater than Allowed2.\r\n";
			}
			//Test 2----------------------------------------------------------------------------------------------------
			if(specificTest==0 || specificTest==2){
				//switch the fees
				fee=Fees.GetFee(codeNum,feeSchedNum1);
				fee.Amount=650;
				Fees.Update(fee);
				fee=Fees.GetFee(codeNum,feeSchedNum2);
				fee.Amount=900;
				Fees.Update(fee);
				Fees.RefreshCache();
				Procedures.ComputeEstimates(proc,patNum,ref claimProcs,false,planList,patPlans,benefitList,histList,loopList,true,pat.Age);
				//Validate
				claimProcs=ClaimProcs.Refresh(patNum);
				claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum1);
				if(claimProc.InsEstTotal!=325) {
					throw new Exception("Should be 325");
				}
				if(claimProc.WriteOffEst!=425) {
					throw new Exception("Should be 425");
				}
				claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum2);
				if(claimProc.InsEstTotal!=450) {
					throw new Exception("Should be 450");
				}
				if(claimProc.WriteOffEst!=0) {
					throw new Exception("Should be 0");
				}
				retVal+="2: Passed.  Claim proc estimates for dual PPO ins.  Allowed2 greater than Allowed1.\r\n";
			}
			return retVal;
		}

	}
}
