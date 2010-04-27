using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	class ClaimProcT {
		///<summary>Test 3</summary>
		public static string TestThree(int specificTest) {
			if(specificTest != 0 && specificTest !=3){
				return"";
			}
			//Patient
			Patient pat=new Patient();
			pat.IsNew=true;
			pat.LName="Kirk";
			pat.FName="Jim";
			pat.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
			pat.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);//This causes standard fee sched to be 53.
			long patNum=Patients.Insert(pat,false);
			//Carrier
			Carrier carrier=new Carrier();
			carrier.CarrierName="CarrierThree";
			long carrierNum=Carriers.Insert(carrier);
			//InsPlans
			InsPlan plan=new InsPlan();
			plan.Subscriber=patNum;
			plan.CarrierNum=carrierNum;
			plan.SubscriberID="1234";
			plan.PlanType="";
			plan.FeeSched=53;
			long planNum1=InsPlans.Insert(plan);
			//Benefits.  Annual max.
			Benefit ben=new Benefit();
			ben.PlanNum=planNum1;
			ben.BenefitType=InsBenefitType.Limitations;
			ben.CovCatNum=0;
			ben.CoverageLevel=BenefitCoverageLevel.Individual;
			ben.Percent=-1;
			ben.MonetaryAmt=1000;
			ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			Benefits.Insert(ben);
			//100% crowns
			ben=new Benefit();
			ben.PlanNum=planNum1;
			ben.BenefitType=InsBenefitType.CoInsurance;
			ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Crowns).CovCatNum;
			ben.CoverageLevel=BenefitCoverageLevel.None;
			ben.Percent=100;
			ben.MonetaryAmt=-1;
			ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			Benefits.Insert(ben);
			//100% diaganostic
			ben=new Benefit();
			ben.PlanNum=planNum1;
			ben.BenefitType=InsBenefitType.CoInsurance;
			ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Diagnostic).CovCatNum;
			ben.CoverageLevel=BenefitCoverageLevel.None;
			ben.Percent=100;
			ben.MonetaryAmt=-1;
			ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			Benefits.Insert(ben);
			//BW frequency every 1 year
			ben=new Benefit();
			ben.PlanNum=planNum1;
			ben.BenefitType=InsBenefitType.Limitations;
			ben.CovCatNum=0;
			ben.CodeNum=ProcedureCodes.GetCodeNum("D0274");
			ben.CoverageLevel=BenefitCoverageLevel.None;
			ben.Percent=-1;
			ben.MonetaryAmt=-1;
			ben.TimePeriod=BenefitTimePeriod.None;
			ben.Quantity=1;
			ben.QuantityQualifier=BenefitQuantity.Years;
			Benefits.Insert(ben);
			//PatPlans
			PatPlan patPlan=new PatPlan();
			patPlan.Ordinal=1;
			patPlan.PatNum=patNum;
			patPlan.PlanNum=planNum1;
			PatPlans.Insert(patPlan);
			//Procedure- crown
			Procedure proc=new Procedure();
			proc.CodeNum=ProcedureCodes.GetCodeNum("D2750");
			proc.PatNum=patNum;
			proc.ProcDate=DateTime.Today;
			proc.ProcStatus=ProcStat.TP;
			proc.ProvNum=pat.PriProv;
			proc.ToothNum="8";
			proc.ProcFee=1100;
			proc.Priority=DefC.Short[(int)DefCat.TxPriorities][0].DefNum;
			long procNum1=Procedures.Insert(proc);
			//Proc 4BW
			proc=new Procedure();
			proc.CodeNum=ProcedureCodes.GetCodeNum("D0274");
			proc.PatNum=patNum;
			proc.ProcDate=DateTime.Today;
			proc.ProcStatus=ProcStat.TP;
			proc.ProvNum=pat.PriProv;
			proc.ProcFee=50;
			proc.Priority=DefC.Short[(int)DefCat.TxPriorities][1].DefNum;
			long procNum2=Procedures.Insert(proc);
			//ClaimProcs
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(patNum);
			List<InsPlan> planList=InsPlans.Refresh(fam);
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			List<Procedure>	ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			string retVal="";
			//ClaimProc claimProc;
			//if(specificTest==0 || specificTest==1){
			for(int i=0;i<ProcListTP.Length;i++){
				Procedures.ComputeEstimates(ProcListTP[i],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
					histList,loopList,false,pat.Age);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
			}
			//save changes in the list to the database
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(patNum);
			ClaimProc claimProc=ClaimProcs.GetEstimate(claimProcs,procNum2,planNum1);
			//I don't think allowed can be easily tested on the fly, and it's not that important.
			if(claimProc.InsEstTotal!=0) {//Insurance should not cover because over annual max.
				throw new Exception("Should be 0");
			}
			retVal+="3: Passed.\r\n";
			//}*/
			return retVal;
		}

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
			//feeSched.IsNew=true;
			feeSched.FeeSchedType=FeeScheduleType.Normal;
			feeSched.Description="DualPPO1";
			long feeSchedNum1=FeeScheds.Insert(feeSched);
			feeSched=new FeeSched();
			//feeSched.IsNew=true;
			feeSched.FeeSchedType=FeeScheduleType.Normal;
			feeSched.Description="DualPPO2";
			long feeSchedNum2=FeeScheds.Insert(feeSched);
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
