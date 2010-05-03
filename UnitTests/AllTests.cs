using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class AllTests {
		/// <summary>Tests 1,2</summary>
		public static string TestOneTwo(int specificTest) {
			if(specificTest != 0 && specificTest != 1 && specificTest != 2){
				return"";
			}
			string suffix="1";
			Patient pat=PatientT.CreatePatient(suffix);
			long patNum=pat.PatNum;
			long feeSchedNum1=FeeSchedT.CreateFeeSched(FeeScheduleType.Normal,suffix);
			long feeSchedNum2=FeeSchedT.CreateFeeSched(FeeScheduleType.Normal,suffix+"b");
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
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			long planNum1=InsPlanT.CreateInsPlanPPO(pat.PatNum,carrier.CarrierNum,feeSchedNum1).PlanNum;
			long planNum2=InsPlanT.CreateInsPlanPPO(pat.PatNum,carrier.CarrierNum,feeSchedNum2).PlanNum;
			BenefitT.CreateCategoryPercent(planNum1,EbenefitCategory.Crowns,50);
			BenefitT.CreateCategoryPercent(planNum2,EbenefitCategory.Crowns,50);
			PatPlanT.CreatePatPlan(1,patNum,planNum1);
			PatPlanT.CreatePatPlan(2,patNum,planNum2);
			Procedure proc=ProcedureT.CreateProcedure(pat,"D2750",ProcStat.TP,"8",Fees.GetAmount0(codeNum,53));//crown on 8
			long procNum=proc.ProcNum;
			//Lists
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

		///<summary>Test 3</summary>
		public static string TestThree(int specificTest) {
			if(specificTest != 0 && specificTest !=3){
				return"";
			}
			string suffix="3";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(pat.PatNum,carrier.CarrierNum);
			BenefitT.CreateAnnualMax(plan.PlanNum,1000);	
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Crowns,100);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Diagnostic,100);
			BenefitT.CreateFrequency(plan.PlanNum,"D0274",BenefitQuantity.Years,1);//BW frequency every 1 year
			PatPlanT.CreatePatPlan(1,pat.PatNum,plan.PlanNum);
			//proc1 - Crown
			Procedure proc1=ProcedureT.CreateProcedure(pat,"D2750",ProcStat.TP,"8",1100);
			ProcedureT.SetPriority(proc1,0);
			//proc2 - 4BW
			Procedure proc2=ProcedureT.CreateProcedure(pat,"D0274",ProcStat.TP,"8",50);
			ProcedureT.SetPriority(proc2,1);
			//Lists:
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsPlan> planList=InsPlans.Refresh(fam);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			List<Procedure>	ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			string retVal="";
			for(int i=0;i<ProcListTP.Length;i++){
				Procedures.ComputeEstimates(ProcListTP[i],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
					histList,loopList,false,pat.Age);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
			}
			//save changes in the list to the database
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			ClaimProc claimProc=ClaimProcs.GetEstimate(claimProcs,proc2.ProcNum,plan.PlanNum);
			//I don't think allowed can be easily tested on the fly, and it's not that important.
			if(claimProc.InsEstTotal!=0) {//Insurance should not cover because over annual max.
				throw new Exception("Should be 0");
			}
			retVal+="3: Passed.  Insurance show zero coverage over annual max.  Not affected by a frequency.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestFour(int specificTest) {
			if(specificTest != 0 && specificTest !=4){
				return"";
			}
			string suffix="4";
			Patient pat=PatientT.CreatePatient(suffix);
			long patNum=pat.PatNum;
			Patient pat2=PatientT.CreatePatient(suffix);
			PatientT.SetGuarantor(pat2,pat.PatNum);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(pat.PatNum,carrier.CarrierNum);//guarantor is subscriber
			long planNum=plan.PlanNum;
			PatPlanT.CreatePatPlan(1,pat.PatNum,planNum);
			PatPlanT.CreatePatPlan(1,pat2.PatNum,planNum);//both patients have the same plan
			BenefitT.CreateAnnualMax(planNum,1000);	
			BenefitT.CreateAnnualMaxFamily(planNum,2500);	
			BenefitT.CreateCategoryPercent(planNum,EbenefitCategory.Crowns,100);
			Procedure proc=ProcedureT.CreateProcedure(pat,"D2750",ProcStat.TP,"8",830);
			long procNum=proc.ProcNum;
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
			Family fam=Patients.GetFamily(patNum);
			List<InsPlan> planList=InsPlans.Refresh(fam);
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			//Validate
			Procedures.ComputeEstimates(proc,patNum,ref claimProcs,false,planList,patPlans,benefitList,histList,loopList,true,pat.Age);
			claimProcs=ClaimProcs.Refresh(patNum);
			ClaimProc claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum);
			if(claimProc.InsEstTotal!=830) {
				throw new Exception("Should be 830.");
			}
			if(claimProc.EstimateNote!="") {
				throw new Exception("EstimateNote should be blank.");
			}
			return "4: Passed.  When family benefits, does not show 'over annual max' until max reached.\r\n";
		}





	}
}
