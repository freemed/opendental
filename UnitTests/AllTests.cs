using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;
using OpenDental;

namespace UnitTests {
	public class AllTests {
		/// <summary></summary>
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
			long planNum1=InsPlanT.CreateInsPlanPPO(carrier.CarrierNum,feeSchedNum1).PlanNum;
			long planNum2=InsPlanT.CreateInsPlanPPO(carrier.CarrierNum,feeSchedNum2).PlanNum;
			InsSub sub1=InsSubT.CreateInsSub(pat.PatNum,planNum1);
			long subNum1=sub1.InsSubNum;
			InsSub sub2=InsSubT.CreateInsSub(pat.PatNum,planNum2);
			long subNum2=sub2.InsSubNum;
			BenefitT.CreateCategoryPercent(planNum1,EbenefitCategory.Crowns,50);
			BenefitT.CreateCategoryPercent(planNum2,EbenefitCategory.Crowns,50);
			PatPlanT.CreatePatPlan(1,patNum,subNum1);
			PatPlanT.CreatePatPlan(2,patNum,subNum2);
			Procedure proc=ProcedureT.CreateProcedure(pat,"D2750",ProcStat.TP,"8",Fees.GetAmount0(codeNum,53));//crown on 8
			long procNum=proc.ProcNum;
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
			Family fam=Patients.GetFamily(patNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			//Validate
			string retVal="";
			ClaimProc claimProc;
			if(specificTest==0 || specificTest==1){
				Procedures.ComputeEstimates(proc,patNum,ref claimProcs,false,planList,patPlans,benefitList,histList,loopList,true,pat.Age,subList);
				claimProcs=ClaimProcs.Refresh(patNum);
				claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum1,subNum1);
				//I don't think allowed can be easily tested on the fly, and it's not that important.
				if(claimProc.InsEstTotal!=450) {
					throw new Exception("Should be 450. \r\n");
				}
				if(claimProc.WriteOffEst!=300) {
					throw new Exception("Should be 300. \r\n");
				}
				claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum2,subNum2);
				if(claimProc.InsEstTotal!=200) {
					throw new Exception("Should be 200. \r\n");
				}
				if(claimProc.WriteOffEst!=0) {
					throw new Exception("Should be 0. \r\n");
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
				Procedures.ComputeEstimates(proc,patNum,ref claimProcs,false,planList,patPlans,benefitList,histList,loopList,true,pat.Age,subList);
				//Validate
				claimProcs=ClaimProcs.Refresh(patNum);
				claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum1,subNum1);
				if(claimProc.InsEstTotal!=325) {
					throw new Exception("Should be 325. \r\n");
				}
				if(claimProc.WriteOffEst!=425) {
					throw new Exception("Should be 425.\r\n");
				}
				claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum2,subNum2);
				if(claimProc.InsEstTotal!=450) {
					throw new Exception("Should be 450.\r\n");
				}
				if(claimProc.WriteOffEst!=0) {
					throw new Exception("Should be 0. \r\n");
				}
				retVal+="2: Passed.  Basic COB with PPOs.  Allowed2 greater than Allowed1.\r\n";
			}
			return retVal;
		}

		///<summary></summary>
		public static string TestThree(int specificTest) {
			if(specificTest != 0 && specificTest !=3){
				return"";
			}
			string suffix="3";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,plan.PlanNum);//guarantor is subscriber
			BenefitT.CreateAnnualMax(plan.PlanNum,1000);	
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Crowns,100);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Diagnostic,100);
			BenefitT.CreateFrequencyProc(plan.PlanNum,"D0274",BenefitQuantity.Years,1);//BW frequency every 1 year
			PatPlanT.CreatePatPlan(1,pat.PatNum,sub.InsSubNum);
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
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			List<Procedure>	ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			string retVal="";
			for(int i=0;i<ProcListTP.Length;i++){
				Procedures.ComputeEstimates(ProcListTP[i],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
					histList,loopList,false,pat.Age,subList);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
			}
			//save changes in the list to the database
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			ClaimProc claimProc=ClaimProcs.GetEstimate(claimProcs,proc2.ProcNum,plan.PlanNum,sub.InsSubNum);
			//I don't think allowed can be easily tested on the fly, and it's not that important.
			if(claimProc.InsEstTotal!=0) {//Insurance should not cover because over annual max.
				throw new Exception("Should be 0. \r\n");
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
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			long planNum=plan.PlanNum;
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,planNum);//guarantor is subscriber
			long subNum=sub.InsSubNum;
			PatPlanT.CreatePatPlan(1,pat.PatNum,subNum);
			PatPlanT.CreatePatPlan(1,pat2.PatNum,subNum);//both patients have the same plan
			BenefitT.CreateAnnualMax(planNum,1000);	
			BenefitT.CreateAnnualMaxFamily(planNum,2500);	
			BenefitT.CreateCategoryPercent(planNum,EbenefitCategory.Crowns,100);
			Procedure proc=ProcedureT.CreateProcedure(pat,"D2750",ProcStat.TP,"8",830);
			long procNum=proc.ProcNum;
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
			Family fam=Patients.GetFamily(patNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			//Validate
			Procedures.ComputeEstimates(proc,patNum,ref claimProcs,false,planList,patPlans,benefitList,histList,loopList,true,pat.Age,subList);
			claimProcs=ClaimProcs.Refresh(patNum);
			ClaimProc claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum,subNum);
			if(claimProc.InsEstTotal!=830) {
				throw new Exception("Should be 830. \r\n");
			}
			if(claimProc.EstimateNote!="") {
				throw new Exception("EstimateNote should be blank.");
			}
			return "4: Passed.  When family benefits, does not show 'over annual max' until max reached.\r\n";
		}

		///<summary></summary>
		public static string TestFive(int specificTest) {
			if(specificTest != 0 && specificTest !=5){
				return"";
			}
			string suffix="5";
			Patient pat=PatientT.CreatePatient(suffix);
			long patNum=pat.PatNum;
			Patient pat2=PatientT.CreatePatient(suffix);
			PatientT.SetGuarantor(pat2,pat.PatNum);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			long planNum=plan.PlanNum;
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,planNum);//guarantor is subscriber
			long subNum=sub.InsSubNum;
			PatPlanT.CreatePatPlan(1,pat.PatNum,subNum);
			PatPlanT.CreatePatPlan(1,pat2.PatNum,subNum);//both patients have the same plan
			BenefitT.CreateAnnualMax(planNum,1000);	
			BenefitT.CreateAnnualMaxFamily(planNum,2500);	
			BenefitT.CreateCategoryPercent(planNum,EbenefitCategory.Crowns,100);
			ClaimProcT.AddInsUsedAdjustment(pat2.PatNum,planNum,2000,subNum,0);//Adjustment goes on the second patient
			Procedure proc=ProcedureT.CreateProcedure(pat2,"D2750",ProcStat.TP,"8",830);//crown and testing is for the first patient
			long procNum=proc.ProcNum;
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
			Family fam=Patients.GetFamily(patNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=ClaimProcs.GetHistList(patNum,benefitList,patPlans,planList,DateTime.Today,subList);
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			//Validate
			Procedures.ComputeEstimates(proc,patNum,ref claimProcs,false,planList,patPlans,benefitList,histList,loopList,true,pat.Age,subList);
			claimProcs=ClaimProcs.Refresh(patNum);
			ClaimProc claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum,subNum);
			if(claimProc.InsEstTotal!=500) {
				throw new Exception("Should be 500. \r\n");
			}
			if(claimProc.EstimateNote!="Over family annual max") {//this explains estimate was reduced.
				throw new Exception("EstimateNote not matching expected.");
			}
			return "5: Passed.  Both individual and family max taken into account.\r\n"; 
		}

		///<summary></summary>
		public static string TestSix(int specificTest) {
			if(specificTest != 0 && specificTest !=6){
				return"";
			}
			string suffix="6";
			Patient pat=PatientT.CreatePatient(suffix);
			long patNum=pat.PatNum;
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			long planNum=plan.PlanNum;
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,planNum);//guarantor is subscriber
			long subNum=sub.InsSubNum;
			long patPlanNum=PatPlanT.CreatePatPlan(1,pat.PatNum,subNum).PatPlanNum;
			BenefitT.CreateAnnualMax(planNum,1000);	
			BenefitT.CreateLimitation(planNum,EbenefitCategory.Diagnostic,1000);	
			Procedure proc=ProcedureT.CreateProcedure(pat,"D0120",ProcStat.C,"",50);//An exam
			long procNum=proc.ProcNum;
			Procedure proc2=ProcedureT.CreateProcedure(pat,"D2750",ProcStat.C,"8",830);//create a crown
			ClaimProcT.AddInsPaid(patNum,planNum,procNum,50,subNum,0,0);
			ClaimProcT.AddInsPaid(patNum,planNum,proc2.ProcNum,400,subNum,0,0);
			//Lists
			Family fam=Patients.GetFamily(patNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=ClaimProcs.GetHistList(patNum,benefitList,patPlans,planList,DateTime.Today,subList);
			//Validate
			double insUsed=InsPlans.GetInsUsedDisplay(histList,DateTime.Today,planNum,patPlanNum,-1,planList,benefitList,patNum,subNum);
			if(insUsed!=400){
				throw new Exception("Should be 400. \r\n");
			}
			//Patient has one insurance plan, subscriber self. Benefits: annual max 1000, diagnostic max 1000. One completed procedure, an exam for $50. Sent to insurance and insurance paid $50. Ins used should still show 0 because the ins used value should only be concerned with annual max . 
			return "6: Passed.  Limitations override more general limitations.\r\n"; 
		}

		///<summary></summary>
		public static string TestSeven(int specificTest) {
			if(specificTest != 0 && specificTest !=7){
				return"";
			}
			string suffix="7";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,plan.PlanNum);
			long subNum=sub.InsSubNum;
			BenefitT.CreateAnnualMax(plan.PlanNum,1000);	
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.RoutinePreventive,100);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Diagnostic,100);
			BenefitT.CreateDeductibleGeneral(plan.PlanNum,BenefitCoverageLevel.Individual,50);
			BenefitT.CreateDeductible(plan.PlanNum,EbenefitCategory.RoutinePreventive,25);
			BenefitT.CreateDeductible(plan.PlanNum,EbenefitCategory.Diagnostic,25);
			PatPlanT.CreatePatPlan(1,pat.PatNum,subNum);
			//proc1 - PerExam
			Procedure proc1=ProcedureT.CreateProcedure(pat,"D0120",ProcStat.TP,"",60);
			ProcedureT.SetPriority(proc1,0);
			//proc2 - Prophy
			Procedure proc2=ProcedureT.CreateProcedure(pat,"D1110",ProcStat.TP,"",70);
			ProcedureT.SetPriority(proc2,1);
			//Lists:
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			List<Procedure>	ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			string retVal="";
			for(int i=0;i<ProcListTP.Length;i++){
				Procedures.ComputeEstimates(ProcListTP[i],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
					histList,loopList,false,pat.Age,subList);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
			}
			//save changes in the list to the database
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			ClaimProc claimProc=ClaimProcs.GetEstimate(claimProcs,proc2.ProcNum,plan.PlanNum,subNum);
			if(claimProc.DedEst!=0) {//Second procedure should show no deductible.
				throw new Exception("Should be 0. \r\n");
			}
			retVal+="7: Passed.  A deductible for preventive/diagnostic is only included once.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestEight(int specificTest) {
			if(specificTest != 0 && specificTest !=8){
				return"";
			}
			string suffix="8";
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
			fee.Amount=600;
			Fees.Insert(fee);
			fee=new Fee();
			fee.CodeNum=codeNum;
			fee.FeeSched=feeSchedNum2;
			fee.Amount=800;
			Fees.Insert(fee);
			Fees.RefreshCache();
			//Carrier
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			long planNum1=InsPlanT.CreateInsPlanPPO(carrier.CarrierNum,feeSchedNum1).PlanNum;
			long planNum2=InsPlanT.CreateInsPlanPPO(carrier.CarrierNum,feeSchedNum2).PlanNum;
			InsSub sub1=InsSubT.CreateInsSub(pat.PatNum,planNum1);
			long subNum1=sub1.InsSubNum;
			InsSub sub2=InsSubT.CreateInsSub(pat.PatNum,planNum2);
			long subNum2=sub2.InsSubNum;
			BenefitT.CreateCategoryPercent(planNum1,EbenefitCategory.Crowns,50);
			BenefitT.CreateCategoryPercent(planNum2,EbenefitCategory.Crowns,50);
			BenefitT.CreateAnnualMax(planNum1,1000);
			BenefitT.CreateAnnualMax(planNum2,1000);
			PatPlanT.CreatePatPlan(1,patNum,subNum1);
			PatPlanT.CreatePatPlan(2,patNum,subNum2);
			Procedure proc=ProcedureT.CreateProcedure(pat,"D2750",ProcStat.TP,"8",Fees.GetAmount0(codeNum,53));//crown on 8
			long procNum=proc.ProcNum;
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
			Family fam=Patients.GetFamily(patNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<Procedure> procList=Procedures.Refresh(patNum);
			//Set complete and attach to claim
			ProcedureT.SetComplete(proc,pat,planList,patPlans,claimProcs,benefitList,subList);
			claimProcs=ClaimProcs.Refresh(patNum);
			List<Procedure> procsForClaim=new List<Procedure>();
			procsForClaim.Add(proc);
			Claim claim=ClaimT.CreateClaim("P",patPlans,planList,claimProcs,procList,pat,procsForClaim,benefitList,subList);
			//Validate
			string retVal="";
			if(claim.WriteOff!=500) {
				throw new Exception("Should be 500. \r\n");
			}
			retVal+="8: Passed.  Completed writeoffs same as estimates for dual PPO ins when Allowed2 greater than Allowed1.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestNine(int specificTest) {
			if(specificTest != 0 && specificTest !=9) {
				return "";
			}
			string suffix="9";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,plan.PlanNum);
			long subNum=sub.InsSubNum;
			BenefitT.CreateAnnualMax(plan.PlanNum,200);
			BenefitT.CreateLimitationProc(plan.PlanNum,"D2161",2000);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Restorative,80);
			PatPlanT.CreatePatPlan(1,pat.PatNum,subNum);
			//proc1 - D2161 (4-surf amalgam)
			Procedure proc1=ProcedureT.CreateProcedure(pat,"D2161",ProcStat.TP,"3",300);
			ProcedureT.SetPriority(proc1,0);
			//proc2 - D2160 (3-surf amalgam)
			Procedure proc2=ProcedureT.CreateProcedure(pat,"D2160",ProcStat.TP,"4",300);
			ProcedureT.SetPriority(proc2,1);
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			List<Procedure> ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			string retVal="";
			for(int i=0;i<ProcListTP.Length;i++) {
				Procedures.ComputeEstimates(ProcListTP[i],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
					histList,loopList,false,pat.Age,subList);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
			}
			//save changes in the list to the database
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			ClaimProc claimProc=ClaimProcs.GetEstimate(claimProcs,proc2.ProcNum,plan.PlanNum,subNum);
			if(claimProc.InsEstTotal!=200) {//Insurance should cover.
				throw new Exception("Should be 200. \r\n");
			}
			retVal+="9: Passed.  Limitations should override more general limitations for any benefit.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestTen(int specificTest) {
			if(specificTest != 0 && specificTest !=10) {
				return "";
			}
			string suffix="10";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,plan.PlanNum);
			long subNum=sub.InsSubNum;
			BenefitT.CreateAnnualMax(plan.PlanNum,400);
			BenefitT.CreateFrequencyCategory(plan.PlanNum,EbenefitCategory.RoutinePreventive,BenefitQuantity.Years,2);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.RoutinePreventive,100);
			PatPlanT.CreatePatPlan(1,pat.PatNum,subNum);
			//procs - D1515 (space maintainers)
			Procedure proc1=ProcedureT.CreateProcedure(pat,"D1515",ProcStat.TP,"3",500);
			ProcedureT.SetPriority(proc1,0);
			Procedure proc2=ProcedureT.CreateProcedure(pat,"D1515",ProcStat.TP,"3",500);
			ProcedureT.SetPriority(proc2,1);
			//Procedure proc3=ProcedureT.CreateProcedure(pat,"D1515",ProcStat.TP,"3",500);
			//ProcedureT.SetPriority(proc3,2);
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			List<Procedure> ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			string retVal="";
			for(int i=0;i<ProcListTP.Length;i++) {
				Procedures.ComputeEstimates(ProcListTP[i],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
					histList,loopList,false,pat.Age,subList);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
			}
			//save changes in the list to the database
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			ClaimProc claimProc1=ClaimProcs.GetEstimate(claimProcs,proc1.ProcNum,plan.PlanNum,subNum);
			if(claimProc1.InsEstTotal!=400) {//Insurance should partially cover.
				throw new Exception("Should be 400. \r\n");
			}
			ClaimProc claimProc2=ClaimProcs.GetEstimate(claimProcs,proc2.ProcNum,plan.PlanNum,subNum);
			if(claimProc2.InsEstTotal!=0) {//Insurance should not cover.
				throw new Exception("Should be 0. \r\n");
			}
			retVal+="10: Passed.  Once max is reached, additional procs show 0 coverage even if preventive frequency exists.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestEleven(int specificTest) {
			if(specificTest != 0 && specificTest !=11) {
				return "";
			}
			string suffix="11";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,plan.PlanNum);
			long subNum=sub.InsSubNum;
			BenefitT.CreateAnnualMaxFamily(plan.PlanNum,400);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Restorative,100);
			PatPlanT.CreatePatPlan(1,pat.PatNum,subNum);
			//procs - D2140 (amalgum fillings)
			Procedure proc1=ProcedureT.CreateProcedure(pat,"D2140",ProcStat.TP,"18",500);
			//Procedure proc1=ProcedureT.CreateProcedure(pat,"D1515",ProcStat.TP,"3",500);
			ProcedureT.SetPriority(proc1,0);
			Procedure proc2=ProcedureT.CreateProcedure(pat,"D2140",ProcStat.TP,"19",500);
			//Procedure proc2=ProcedureT.CreateProcedure(pat,"D1515",ProcStat.TP,"3",500);
			ProcedureT.SetPriority(proc2,1);
			//Procedure proc3=ProcedureT.CreateProcedure(pat,"D1515",ProcStat.TP,"3",500);
			//ProcedureT.SetPriority(proc3,2);
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			List<Procedure> ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			string retVal="";
			for(int i=0;i<ProcListTP.Length;i++) {
				Procedures.ComputeEstimates(ProcListTP[i],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
					histList,loopList,false,pat.Age,subList);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
			}
			//save changes in the list to the database
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			ClaimProc claimProc1=ClaimProcs.GetEstimate(claimProcs,proc1.ProcNum,plan.PlanNum,patPlans[0].InsSubNum);
			if(claimProc1.InsEstTotal!=400) {//Insurance should partially cover.
				throw new Exception("Claim 1 was "+claimProc1.InsEstTotal+", should be 400.\r\n");
			}
			ClaimProc claimProc2=ClaimProcs.GetEstimate(claimProcs,proc2.ProcNum,plan.PlanNum,patPlans[0].InsSubNum);
			if(claimProc2.InsEstTotal!=0) {//Insurance should not cover.
				throw new Exception("Claim 2 was "+claimProc2.InsEstTotal+", should be 0.\r\n");
			}
			retVal+="11: Passed.  Once family max is reached, additional procs show 0 coverage.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestTwelve(int specificTest) {
			if(specificTest != 0 && specificTest !=12){
				return"";
			}
			string suffix="12";
			Patient pat=PatientT.CreatePatient(suffix);
			long patNum=pat.PatNum;
			Patient pat2=PatientT.CreatePatient(suffix);
			PatientT.SetGuarantor(pat2,pat.PatNum);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			long feeSchedNum=FeeSchedT.CreateFeeSched(FeeScheduleType.Normal,suffix);
			//Standard Fee
			Fees.RefreshCache();
			long codeNum=ProcedureCodes.GetCodeNum("D2750");
			Fee fee=Fees.GetFee(codeNum,53);
			if(fee==null) {
				fee=new Fee();
				fee.CodeNum=codeNum;
				fee.FeeSched=53;
				fee.Amount=1400;
				Fees.Insert(fee);
			}
			else {
				fee.Amount=1400;
				Fees.Update(fee);
			}
			//PPO fees
			fee=new Fee();
			fee.CodeNum=codeNum;
			fee.FeeSched=feeSchedNum;
			fee.Amount=1100;
			Fees.Insert(fee);
			Fees.RefreshCache();
			InsPlan plan=InsPlanT.CreateInsPlanPPO(carrier.CarrierNum,feeSchedNum);
			long planNum=plan.PlanNum;
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,planNum);//patient is subscriber for plan 1
			long subNum=sub.InsSubNum;
			PatPlanT.CreatePatPlan(1,pat.PatNum,subNum);
			InsSub sub2=InsSubT.CreateInsSub(pat2.PatNum,planNum);//spouse is subscriber for plan 2
			long subNum2=sub2.InsSubNum;
			PatPlanT.CreatePatPlan(2,pat.PatNum,subNum2);//patient also has spouse's coverage
			BenefitT.CreateAnnualMax(planNum,1200);
			BenefitT.CreateDeductibleGeneral(planNum,BenefitCoverageLevel.Individual,0);
			BenefitT.CreateCategoryPercent(planNum,EbenefitCategory.Crowns,100);//2700-2799
			Procedure proc=ProcedureT.CreateProcedure(pat,"D2750",ProcStat.TP,"19",1400);
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();//empty, not used for calcs.
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();//empty, not used for calcs.
			List<Procedure> ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			string retVal="";
			Procedures.ComputeEstimates(ProcListTP[0],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
				histList,loopList,false,pat.Age,subList);
			//save changes in the list to the database
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			ClaimProc claimProc1=ClaimProcs.GetEstimate(claimProcs,proc.ProcNum,plan.PlanNum,subNum);
			if(claimProc1.InsEstTotal!=1100) {
				throw new Exception("Primary Estimate was "+claimProc1.InsEstTotal+", should be 1100.\r\n");
			}
			ClaimProc claimProc2=ClaimProcs.GetEstimate(claimProcs,proc.ProcNum,plan.PlanNum,subNum2);
			if(claimProc2.InsEstTotal!=0) {//Insurance should not cover.
				throw new Exception("Secondary Estimate was "+claimProc2.InsEstTotal+", should be 0.\r\n");
			}
			retVal+="12: Passed.  Once family max is reached, additional procs show 0 coverage.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestThirteen(int specificTest) {
			if(specificTest != 0 && specificTest !=13){
				return"";
			}
			string suffix="13";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,plan.PlanNum);
			long subNum=sub.InsSubNum;
			PatPlan patPlan=PatPlanT.CreatePatPlan(1,pat.PatNum,subNum);
			BenefitT.CreateAnnualMax(plan.PlanNum,100);
			BenefitT.CreateOrthoMax(plan.PlanNum,500);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Diagnostic,100);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Orthodontics,100);
			Procedure proc1=ProcedureT.CreateProcedure(pat,"D0140",ProcStat.C,"",59);//limEx
			Procedure proc2=ProcedureT.CreateProcedure(pat,"D8090",ProcStat.C,"",348);//Comprehensive ortho
			ClaimProcT.AddInsPaid(pat.PatNum,plan.PlanNum,proc1.ProcNum,59,subNum,0,0);
			ClaimProcT.AddInsPaid(pat.PatNum,plan.PlanNum,proc2.ProcNum,348,subNum,0,0);
			//Lists
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=ClaimProcs.GetHistList(pat.PatNum,benefitList,patPlans,planList,DateTime.Today,subList);
			//Validate
			double insUsed=InsPlans.GetInsUsedDisplay(histList,DateTime.Today,plan.PlanNum,patPlan.PatPlanNum,-1,planList,benefitList,pat.PatNum,subNum);
			if(insUsed!=59) {
				throw new Exception("Should be 59. \r\n");
			}
			return "13: Passed.  Ortho procedures should not affect insurance used section at lower right of TP module.\r\n"; 
		}

		//public static string TestFourteen(int specificTest) {//This was taken out of the manual because it was a duplicate of Unit Test 1 but expected a different result.
		//  if(specificTest != 0 && specificTest !=14){
		//    return"";
		//  }
		//  string suffix="14";
		//  Patient pat=PatientT.CreatePatient(suffix);
		//  long patNum=pat.PatNum;
		//  long feeSchedNum1=FeeSchedT.CreateFeeSched(FeeScheduleType.Normal,suffix);
		//  long feeSchedNum2=FeeSchedT.CreateFeeSched(FeeScheduleType.Normal,suffix+"b");
		//  //Standard Fee
		//  Fees.RefreshCache();
		//  long codeNum=ProcedureCodes.GetCodeNum("D7140");
		//  Fee fee=Fees.GetFee(codeNum,53);
		//  if(fee==null) {
		//    fee=new Fee();
		//    fee.CodeNum=codeNum;
		//    fee.FeeSched=53;
		//    fee.Amount=140;
		//    Fees.Insert(fee);
		//  }
		//  else {
		//    fee.Amount=140;
		//    Fees.Update(fee);
		//  }
		//  //PPO fees
		//  fee=new Fee();
		//  fee.CodeNum=codeNum;
		//  fee.FeeSched=feeSchedNum1;
		//  fee.Amount=136;
		//  Fees.Insert(fee);
		//  fee=new Fee();
		//  fee.CodeNum=codeNum;
		//  fee.FeeSched=feeSchedNum2;
		//  fee.Amount=77;
		//  Fees.Insert(fee);
		//  Fees.RefreshCache();
		//  //Carrier
		//  Carrier carrier=CarrierT.CreateCarrier(suffix);
		//  long planNum1=InsPlanT.CreateInsPlanPPO(carrier.CarrierNum,feeSchedNum1).PlanNum;
		//  long planNum2=InsPlanT.CreateInsPlanPPO(carrier.CarrierNum,feeSchedNum2).PlanNum;
		//  InsSub sub1=InsSubT.CreateInsSub(pat.PatNum,planNum1);
		//  long subNum1=sub1.InsSubNum;
		//  InsSub sub2=InsSubT.CreateInsSub(pat.PatNum,planNum2);
		//  long subNum2=sub2.InsSubNum;
		//  BenefitT.CreateCategoryPercent(planNum1,EbenefitCategory.OralSurgery,50);
		//  BenefitT.CreateCategoryPercent(planNum2,EbenefitCategory.OralSurgery,100);
		//  PatPlanT.CreatePatPlan(1,patNum,subNum1);
		//  PatPlanT.CreatePatPlan(2,patNum,subNum2);
		//  Procedure proc=ProcedureT.CreateProcedure(pat,"D7140",ProcStat.TP,"8",Fees.GetAmount0(codeNum,53));//extraction on 8
		//  long procNum=proc.ProcNum;
		//  //Lists
		//  List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
		//  Family fam=Patients.GetFamily(patNum);
		//  List<InsSub> subList=InsSubs.RefreshForFam(fam);
		//  List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
		//  List<PatPlan> patPlans=PatPlans.Refresh(patNum);
		//  List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
		//  List<ClaimProcHist> histList=new List<ClaimProcHist>();
		//  List<ClaimProcHist> loopList=new List<ClaimProcHist>();
		//  //Validate
		//  ClaimProc claimProc;
		//  Procedures.ComputeEstimates(proc,patNum,ref claimProcs,false,planList,patPlans,benefitList,histList,loopList,true,pat.Age,subList);
		//  claimProcs=ClaimProcs.Refresh(patNum);
		//  claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum1,subNum1);
		//  if(claimProc.InsEstTotal!=68) {
		//    throw new Exception("Should be 68. \r\n");
		//  }
		//  if(claimProc.WriteOffEst!=4) {
		//    throw new Exception("Should be 4. \r\n");
		//  }
		//  claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum2,subNum2);
		//  if(claimProc.InsEstTotal!=9) {
		//    throw new Exception("Should be 9. \r\n");
		//  }
		//  if(claimProc.WriteOffEst!=59) {
		//    throw new Exception("Writeoff should be 59. \r\n");
		//  }
		//  return "14: Passed.  Claim proc estimates for dual PPO ins.  Writeoff2 not zero.\r\n";
		//}

		///<summary></summary>
		public static string TestFourteen(int specificTest) {
			if(specificTest != 0 && specificTest !=15) {
				return "";
			}
			string suffix="14";
			Patient pat=PatientT.CreatePatient(suffix);
			long patNum=pat.PatNum;
			long feeSchedNum1=FeeSchedT.CreateFeeSched(FeeScheduleType.Normal,suffix);
			long feeSchedNum2=FeeSchedT.CreateFeeSched(FeeScheduleType.Normal,suffix+"b");
			//Standard Fee
			Fees.RefreshCache();
			long codeNum=ProcedureCodes.GetCodeNum("D2160");
			Fee fee=Fees.GetFee(codeNum,53);
			if(fee==null) {
				fee=new Fee();
				fee.CodeNum=codeNum;
				fee.FeeSched=53;
				fee.Amount=1279;
				Fees.Insert(fee);
			}
			else {
				fee.Amount=1279;
				Fees.Update(fee);
			}
			//PPO fees
			fee=new Fee();
			fee.CodeNum=codeNum;
			fee.FeeSched=feeSchedNum1;
			fee.Amount=1279;
			Fees.Insert(fee);
			fee=new Fee();
			fee.CodeNum=codeNum;
			fee.FeeSched=feeSchedNum2;
			fee.Amount=110;
			Fees.Insert(fee);
			Fees.RefreshCache();
			//Carrier
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			long planNum1=InsPlanT.CreateInsPlanPPO(carrier.CarrierNum,feeSchedNum1).PlanNum;
			long planNum2=InsPlanT.CreateInsPlanPPO(carrier.CarrierNum,feeSchedNum2).PlanNum;
			InsSub sub1=InsSubT.CreateInsSub(pat.PatNum,planNum1);
			long subNum1=sub1.InsSubNum;
			InsSub sub2=InsSubT.CreateInsSub(pat.PatNum,planNum2);
			long subNum2=sub2.InsSubNum;
			BenefitT.CreateCategoryPercent(planNum1,EbenefitCategory.Restorative,80);
			BenefitT.CreateCategoryPercent(planNum2,EbenefitCategory.Restorative,80);
			BenefitT.CreateAnnualMax(planNum1,1200);
			BenefitT.CreateAnnualMax(planNum2,1200);
			PatPlanT.CreatePatPlan(1,patNum,subNum1);
			PatPlanT.CreatePatPlan(2,patNum,subNum2);
			Procedure proc=ProcedureT.CreateProcedure(pat,"D2160",ProcStat.TP,"19",Fees.GetAmount0(codeNum,53));//amalgam on 19
			long procNum=proc.ProcNum;
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(patNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();//empty, not used for calcs.
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();//empty, not used for calcs.
			List<Procedure> procList=Procedures.Refresh(patNum);
			Procedure[] ProcListTP=Procedures.GetListTP(procList);//sorted by priority, then toothnum
			//Set complete and attach to claim
			ProcedureT.SetComplete(proc,pat,planList,patPlans,claimProcs,benefitList,subList);
			claimProcs=ClaimProcs.Refresh(patNum);
			List<Procedure> procsForClaim=new List<Procedure>();
			procsForClaim.Add(proc);
			Claim claim1=ClaimT.CreateClaim("P",patPlans,planList,claimProcs,procList,pat,procsForClaim,benefitList,subList);
			Claim claim2=ClaimT.CreateClaim("S",patPlans,planList,claimProcs,procList,pat,procsForClaim,benefitList,subList);
			//Validate
			string retVal="";
			Procedures.ComputeEstimates(ProcListTP[0],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
				histList,loopList,false,pat.Age,subList);
			//save changes in the list to the database
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			ClaimProc claimProc1=ClaimProcs.GetEstimate(claimProcs,proc.ProcNum,planNum1,subNum1);
			if(claimProc1.InsEstTotal!=1023.20) {
				throw new Exception("Primary Estimate was "+claimProc1.InsEstTotal+", should be 1023.20.\r\n");
			}
			ClaimProc claimProc2=ClaimProcs.GetEstimate(claimProcs,proc.ProcNum,planNum2,subNum2);
			/*Is this ok, or do we need to take another look?
			if(claimProc2.WriteOff!=0) {//Insurance should not cover.
				throw new Exception("Secondary writeoff was "+claimProc2.WriteOff+", should be 0.\r\n");
			}
			if(claimProc2.InsEstTotal!=0) {//Insurance should not cover.
				throw new Exception("Secondary Estimate was "+claimProc2.InsEstTotal+", should be 0.\r\n");
			}*/
			retVal+="14: Passed. Primary estimate are not affected by secondary claim.\r\n";
			return retVal;
		}

				///<summary></summary>
		public static string TestFifteen(int specificTest) {
			if(specificTest != 0 && specificTest !=15){
				return"";
			}
			string suffix="15";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,plan.PlanNum);
			BenefitT.CreateAnnualMax(plan.PlanNum,1000);
			BenefitT.CreateDeductibleGeneral(plan.PlanNum,BenefitCoverageLevel.Individual,50);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.RoutinePreventive,100);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Diagnostic,100);
			BenefitT.CreateDeductible(plan.PlanNum,EbenefitCategory.RoutinePreventive,0);
			BenefitT.CreateDeductible(plan.PlanNum,EbenefitCategory.Diagnostic,0);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Restorative,80);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Endodontics,80);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Periodontics,80);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.OralSurgery,80);
			BenefitT.CreateDeductible(plan.PlanNum,"D0330",45);
			PatPlanT.CreatePatPlan(1,pat.PatNum,sub.InsSubNum);
			//proc1 - Pano
			Procedure proc1=ProcedureT.CreateProcedure(pat,"D0330",ProcStat.TP,"",95);
			ProcedureT.SetPriority(proc1,0);
			//proc2 - Amalg
			Procedure proc2=ProcedureT.CreateProcedure(pat,"D2150",ProcStat.TP,"30",200);
			ProcedureT.SetPriority(proc2,1);
			//Lists:
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			List<Procedure>	ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			string retVal="";
			for(int i=0;i<ProcListTP.Length;i++){
				Procedures.ComputeEstimates(ProcListTP[i],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
					histList,loopList,false,pat.Age,subList);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
			}
			//save changes in the list to the database
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			ClaimProc claimProc1=ClaimProcs.GetEstimate(claimProcs,proc1.ProcNum,plan.PlanNum,sub.InsSubNum);
			ClaimProc claimProc2=ClaimProcs.GetEstimate(claimProcs,proc2.ProcNum,plan.PlanNum,sub.InsSubNum);
			if(claimProc1.DedEst!=45){
				throw new Exception("Estimate 1 should be 45. Is " + claimProc1.DedEst + ".\r\n");
			}
			if(claimProc2.DedEst!=5) {
				throw new Exception("Estimate 2 should be 5. Is " + claimProc2.DedEst + ".\r\n");
			}
			retVal+="15: Passed. Deductibles can be created to override the regular deductible.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestSixteen(int specificTest) {
			if(specificTest != 0 && specificTest !=16){
				return"";
			}
			string suffix="16";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,plan.PlanNum);//guarantor is subscriber
			//BenefitT.CreateAnnualMax(plan.PlanNum,1000);//Irrelevant benefits bog down debugging.
			BenefitT.CreateDeductibleGeneral(plan.PlanNum,BenefitCoverageLevel.Individual,50);
			//BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.RoutinePreventive,100);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Diagnostic,100);
			//BenefitT.CreateDeductible(plan.PlanNum,EbenefitCategory.RoutinePreventive,0);
			BenefitT.CreateDeductible(plan.PlanNum,EbenefitCategory.Diagnostic,0);
			//BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Restorative,80);
			//BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Endodontics,80);
			//BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Periodontics,80);
			//BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.OralSurgery,80);
			BenefitT.CreateDeductible(plan.PlanNum,"D0330",45);
			BenefitT.CreateDeductible(plan.PlanNum,"D0220",25);
			PatPlanT.CreatePatPlan(1,pat.PatNum,sub.InsSubNum);
			//proc1 - Pano
			Procedure proc1=ProcedureT.CreateProcedure(pat,"D0330",ProcStat.TP,"",100);
			ProcedureT.SetPriority(proc1,0);
			//proc2 - Intraoral - periapical first film
			Procedure proc2=ProcedureT.CreateProcedure(pat,"D0220",ProcStat.TP,"",75);
			ProcedureT.SetPriority(proc2,1);
			//Lists:
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			List<Procedure>	ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			string retVal="";
			for(int i=0;i<ProcListTP.Length;i++){
				Procedures.ComputeEstimates(ProcListTP[i],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
					histList,loopList,false,pat.Age,subList);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
			}
			//save changes in the list to the database
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			ClaimProc claimProc1=ClaimProcs.GetEstimate(claimProcs,proc1.ProcNum,plan.PlanNum,sub.InsSubNum);
			ClaimProc claimProc2=ClaimProcs.GetEstimate(claimProcs,proc2.ProcNum,plan.PlanNum,sub.InsSubNum);
			//
			if(claimProc1.DedEst!=45){
				throw new Exception("Estimate 1 should be 45. Is " + claimProc1.DedEst + ".\r\n");
			}
			if(claimProc2.DedEst!=5) {
				throw new Exception("Estimate 2 should be 5. Is " + claimProc2.DedEst + ".\r\n");
			}
			retVal+="16: Passed. Multiple deductibles for categories do not exceed the regular deductible.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestSeventeen(int specificTest) {
			if(specificTest != 0 && specificTest != 17){
				return"";
			}
			string suffix="17";
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
			long planNum1=InsPlanT.CreateInsPlanPPO(carrier.CarrierNum,feeSchedNum1,EnumCobRule.Standard).PlanNum;
			long planNum2=InsPlanT.CreateInsPlanPPO(carrier.CarrierNum,feeSchedNum2,EnumCobRule.Standard).PlanNum;
			InsSub sub1=InsSubT.CreateInsSub(pat.PatNum,planNum1);
			long subNum1=sub1.InsSubNum;
			InsSub sub2=InsSubT.CreateInsSub(pat.PatNum,planNum2);
			long subNum2=sub2.InsSubNum;
			BenefitT.CreateCategoryPercent(planNum1,EbenefitCategory.Crowns,50);
			BenefitT.CreateCategoryPercent(planNum2,EbenefitCategory.Crowns,50);
			PatPlanT.CreatePatPlan(1,patNum,subNum1);
			PatPlanT.CreatePatPlan(2,patNum,subNum2);
			Procedure proc=ProcedureT.CreateProcedure(pat,"D2750",ProcStat.TP,"8",Fees.GetAmount0(codeNum,53));//crown on 8
			long procNum=proc.ProcNum;
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
			Family fam=Patients.GetFamily(patNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			//Validate
			string retVal="";
			ClaimProc claimProc;
			//Test 17 Part 1 (copied from Unit Test 1)----------------------------------------------------------------------------------------------------
			Procedures.ComputeEstimates(proc,patNum,ref claimProcs,false,planList,patPlans,benefitList,histList,loopList,true,pat.Age,subList);
			claimProcs=ClaimProcs.Refresh(patNum);
			claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum1,subNum1);
			//I don't think allowed can be easily tested on the fly, and it's not that important.
			if(claimProc.InsEstTotal!=450) {
				throw new Exception("Should be 450. \r\n");
			}
			if(claimProc.WriteOffEst!=300) {
				throw new Exception("Should be 300. \r\n");
			}
			claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum2,subNum2);
			if(claimProc.InsEstTotal!=325) {
				throw new Exception("Should be 325. \r\n");
			}
			if(claimProc.WriteOffEst!=0) {
				throw new Exception("Should be 0. \r\n");
			}
			//Test 17 Part 2 (copied from Unit Test 2)----------------------------------------------------------------------------------------------------
			//switch the fees
			fee=Fees.GetFee(codeNum,feeSchedNum1);
			fee.Amount=650;
			Fees.Update(fee);
			fee=Fees.GetFee(codeNum,feeSchedNum2);
			fee.Amount=900;
			Fees.Update(fee);
			Fees.RefreshCache();
			Procedures.ComputeEstimates(proc,patNum,ref claimProcs,false,planList,patPlans,benefitList,histList,loopList,true,pat.Age,subList);
			//Validate
			claimProcs=ClaimProcs.Refresh(patNum);
			claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum1,subNum1);
			if(claimProc.InsEstTotal!=325) {
				throw new Exception("Should be 325. \r\n");
			}
			if(claimProc.WriteOffEst!=550) {
				throw new Exception("Should be 550. \r\n");
			}
			claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum2,subNum2);
			if(claimProc.InsEstTotal!=325) {
				throw new Exception("Should be 325. \r\n");
			}
			if(claimProc.WriteOffEst!=0) {
				throw new Exception("Should be 0. \r\n");
			}
			retVal+="17: Passed.  Standard COB with PPOs.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestEighteen(int specificTest) {
			if(specificTest != 0 && specificTest != 18){
				return"";
			}
			string suffix="18";
			Patient pat=PatientT.CreatePatient(suffix);
			long patNum=pat.PatNum;
			//long feeSchedNum1=FeeSchedT.CreateFeeSched(FeeScheduleType.Normal,suffix);
			//long feeSchedNum2=FeeSchedT.CreateFeeSched(FeeScheduleType.Normal,suffix+"b");
			//Carrier
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			long planNum1=InsPlanT.CreateInsPlan(carrier.CarrierNum,EnumCobRule.CarveOut).PlanNum;
			long planNum2=InsPlanT.CreateInsPlan(carrier.CarrierNum,EnumCobRule.CarveOut).PlanNum;
			InsSub sub1=InsSubT.CreateInsSub(pat.PatNum,planNum1);
			long subNum1=sub1.InsSubNum;
			InsSub sub2=InsSubT.CreateInsSub(pat.PatNum,planNum2);
			long subNum2=sub2.InsSubNum;
			BenefitT.CreateCategoryPercent(planNum1,EbenefitCategory.Crowns,50);
			BenefitT.CreateCategoryPercent(planNum2,EbenefitCategory.Crowns,75);
			PatPlanT.CreatePatPlan(1,patNum,subNum1);
			PatPlanT.CreatePatPlan(2,patNum,subNum2);
			Procedure proc=ProcedureT.CreateProcedure(pat,"D2750",ProcStat.TP,"8",1200);//crown on 8
			long procNum=proc.ProcNum;
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
			Family fam=Patients.GetFamily(patNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			//Validate
			string retVal="";
			ClaimProc claimProc;
			//Test 18 Part 1 (copied from Unit Test 1)----------------------------------------------------------------------------------------------------
			Procedures.ComputeEstimates(proc,patNum,ref claimProcs,false,planList,patPlans,benefitList,histList,loopList,true,pat.Age,subList);
			claimProcs=ClaimProcs.Refresh(patNum);
			claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum1,subNum1);
			//I don't think allowed can be easily tested on the fly, and it's not that important.
			if(claimProc.InsEstTotal!=600) {
				throw new Exception("Should be 600. \r\n");
			}
			claimProc=ClaimProcs.GetEstimate(claimProcs,procNum,planNum2,subNum2);
			if(claimProc.InsEstTotal!=300) {
				throw new Exception("Should be 300. \r\n");
			}
			retVal+="18: Passed. CarveOut using category percentage.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestNineteen(int specificTest) {
			if(specificTest != 0 && specificTest !=19){
				return"";
			}
			string suffix="19";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier1=CarrierT.CreateCarrier(suffix);
			Carrier carrier2=CarrierT.CreateCarrier(suffix);
			InsPlan plan1=InsPlanT.CreateInsPlan(carrier1.CarrierNum);
			InsPlan plan2=InsPlanT.CreateInsPlan(carrier2.CarrierNum);
			InsSub sub1=InsSubT.CreateInsSub(pat.PatNum,plan1.PlanNum);
			InsSub sub2=InsSubT.CreateInsSub(pat.PatNum,plan2.PlanNum);
			long subNum1=sub1.InsSubNum;
			long subNum2=sub2.InsSubNum;
			//plans
			BenefitT.CreateCategoryPercent(plan1.PlanNum,EbenefitCategory.Diagnostic,50);
			BenefitT.CreateCategoryPercent(plan2.PlanNum,EbenefitCategory.Diagnostic,50);
			BenefitT.CreateDeductibleGeneral(plan1.PlanNum,BenefitCoverageLevel.Individual,50);
			BenefitT.CreateDeductibleGeneral(plan2.PlanNum,BenefitCoverageLevel.Individual,50);
			PatPlanT.CreatePatPlan(1,pat.PatNum,subNum1);
			PatPlanT.CreatePatPlan(2,pat.PatNum,subNum2);
			//proc1 - PerExam
			Procedure proc=ProcedureT.CreateProcedure(pat,"D0120",ProcStat.TP,"",150);
			//Lists:
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			List<Procedure>	ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			string retVal="";
			for(int i=0;i<ProcListTP.Length;i++){
				Procedures.ComputeEstimates(ProcListTP[i],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
					histList,loopList,false,pat.Age,subList);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
			}
			//save changes in the list to the database
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			ClaimProc claimProc1=ClaimProcs.GetEstimate(claimProcs,proc.ProcNum,plan1.PlanNum,subNum1);
			ClaimProc claimProc2=ClaimProcs.GetEstimate(claimProcs,proc.ProcNum,plan2.PlanNum,subNum2);
			if(claimProc1.DedEst!=50) {//$50 deductible
				throw new Exception("Should be 50. \r\n");
			}
			if(claimProc1.InsEstTotal!=50) {//Ins1 pays 40% of (fee - deductible) = .4 * (150 - 50)
				throw new Exception("Should be 50. \r\n");
			}
			if(claimProc2.DedEst!=50) {//$50 deductible
				throw new Exception("Should be 50. \r\n");
			}
			if(claimProc2.InsEstTotal!=50) {//Ins2 pays 
				throw new Exception("Should be 50. \r\n");
			}
			retVal+="19: Passed.  Multiple deductibles are accounted for.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestTwenty(int specificTest) {
			if(specificTest != 0 && specificTest !=20) {
				return "";
			}
			string suffix="20";
			Patient pat=PatientT.CreatePatient(suffix);//guarantor
			long patNum=pat.PatNum;
			Patient pat2=PatientT.CreatePatient(suffix);
			PatientT.SetGuarantor(pat2,pat.PatNum);
			Patient pat3=PatientT.CreatePatient(suffix);
			PatientT.SetGuarantor(pat3,pat.PatNum);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			long planNum=plan.PlanNum;
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,planNum);//guarantor is subscriber
			long subNum=sub.InsSubNum;
			PatPlan patPlan=PatPlanT.CreatePatPlan(1,pat.PatNum,subNum);//all three patients have the same plan
			PatPlan patPlan2=PatPlanT.CreatePatPlan(1,pat2.PatNum,subNum);//all three patients have the same plan
			PatPlan patPlan3=PatPlanT.CreatePatPlan(1,pat3.PatNum,subNum);//all three patients have the same plan
			BenefitT.CreateDeductibleGeneral(planNum,BenefitCoverageLevel.Individual,75);
			BenefitT.CreateDeductibleGeneral(planNum,BenefitCoverageLevel.Family,150);
			ClaimProcT.AddInsUsedAdjustment(pat3.PatNum,planNum,0,subNum,75);//Adjustment goes on the third patient
			Procedure proc=ProcedureT.CreateProcedure(pat2,"D2750",ProcStat.C,"20",1280);//proc for second patient with a deductible already applied.
			ClaimProcT.AddInsPaid(pat2.PatNum,planNum,proc.ProcNum,304,subNum,50,597);
			proc=ProcedureT.CreateProcedure(pat,"D4355",ProcStat.TP,"",135);//proc is for the first patient
			long procNum=proc.ProcNum;
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
			Family fam=Patients.GetFamily(patNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=ClaimProcs.GetHistList(patNum,benefitList,patPlans,planList,DateTime.Today,subList);
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			//Validate
			List<ClaimProcHist> HistList=ClaimProcs.GetHistList(pat.PatNum,benefitList,patPlans,planList,DateTime.Today,subList);
			double dedFam=Benefits.GetDeductGeneralDisplay(benefitList,planNum,patPlan.PatPlanNum,BenefitCoverageLevel.Family);
			double ded=Benefits.GetDeductGeneralDisplay(benefitList,planNum,patPlan.PatPlanNum,BenefitCoverageLevel.Individual);
			double dedRem=InsPlans.GetDedRemainDisplay(HistList,DateTime.Today,planNum,patPlan.PatPlanNum,-1,planList,pat.PatNum,ded,dedFam);//test family and individual deductible together
			if(dedRem!=25) { //guarantor deductible
				throw new Exception("Guarantor combination deductible remaining should be 25.\r\n");
			}
			dedRem=InsPlans.GetDedRemainDisplay(HistList,DateTime.Today,planNum,patPlan.PatPlanNum,-1,planList,pat.PatNum,ded,-1);//test individual deductible by itself
			if(dedRem!=75) { //guarantor deductible
				throw new Exception("Guarantor individual deductible remaining should be 75.\r\n");
			}
			return "20: Passed.  Both individual and family deductibles taken into account.\r\n";
		}

		///<summary></summary>
		public static string TestTwentyOne(int specificTest) {
			if(specificTest != 0 && specificTest !=21){
				return"";
			}
			string suffix="21";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,plan.PlanNum);//guarantor is subscriber
			BenefitT.CreateAnnualMax(plan.PlanNum,1000);
			BenefitT.CreateDeductibleGeneral(plan.PlanNum,BenefitCoverageLevel.Individual,50);
			PatPlanT.CreatePatPlan(1,pat.PatNum,sub.InsSubNum);
			//proc - Exam
			Procedure proc1=ProcedureT.CreateProcedure(pat,"D0120",ProcStat.TP,"",55);
			ProcedureT.SetPriority(proc1,0);
			//Lists:
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			List<Procedure>	ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			string retVal="";
			for(int i=0;i<ProcListTP.Length;i++){
				Procedures.ComputeEstimates(ProcListTP[i],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
					histList,loopList,false,pat.Age,subList);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
			}
			//save changes in the list to the database
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			ClaimProc claimProc1=ClaimProcs.GetEstimate(claimProcs,proc1.ProcNum,plan.PlanNum,sub.InsSubNum);
			//Check
			if(claimProc1.DedEst!=0){
				throw new Exception("Estimated deduction should be 0, is " + claimProc1.DedEst + ".\r\n");
			}
			retVal+="21: Passed. Deductibles are not applied to procedures that are not covered.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestTwentyTwo(int specificTest) {
			if(specificTest != 0 && specificTest !=22) {
				return "";
			}
			string suffix="22";
			DateTime startDate=DateTime.Parse("2001-01-01");
			Employee emp = EmployeeT.CreateEmployee(suffix);
			PayPeriod payP1 = PayPeriodT.CreateTwoWeekPayPeriodIfNotExists(startDate);
			PayPeriods.RefreshCache();
			Prefs.UpdateInt(PrefName.TimeCardOvertimeFirstDayOfWeek,0);
			Prefs.UpdateBool(PrefName.TimeCardsMakesAdjustmentsForOverBreaks,true);
			Prefs.RefreshCache();
			TimeCardRuleT.CreatePMTimeRule(emp.EmployeeNum,TimeSpan.FromHours(16));
			TimeCardRules.RefreshCache();
			long clockEvent1 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddHours(8),startDate.AddHours(16).AddMinutes(40));
			ClockEventT.InsertBreak(emp.EmployeeNum,startDate.AddHours(11),40);
			TimeCardRules.CalculateDailyOvertime(emp,payP1.DateStart,payP1.DateStop);
			//Validate
			string retVal="";
			//Check
			if(ClockEvents.GetOne(clockEvent1).AdjustAuto!=TimeSpan.FromMinutes(-10)) {
				throw new Exception("Clock adjustment should be -10 minutes, instead it is " + ClockEvents.GetOne(clockEvent1).AdjustAuto.TotalMinutes + " minutes.\r\n");
			}
			if(ClockEvents.GetOne(clockEvent1).OTimeAuto!=TimeSpan.FromMinutes(40)) {
				throw new Exception("Clock ovetime should be 40 minutes, instead it is " + ClockEvents.GetOne(clockEvent1).OTimeAuto.TotalMinutes + " minutes.\r\n");
			}
			retVal+="22: Passed. Overtime calculated properly after time of day.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestTwentyThree(int specificTest) {
			if(specificTest != 0 && specificTest !=23) {
				return "";
			}
			string suffix="23";
			DateTime startDate=DateTime.Parse("2001-01-01");
			Employee emp = EmployeeT.CreateEmployee(suffix);
			PayPeriod payP1 = PayPeriodT.CreateTwoWeekPayPeriodIfNotExists(startDate);
			PayPeriods.RefreshCache();
			Prefs.UpdateInt(PrefName.TimeCardOvertimeFirstDayOfWeek,0);
			Prefs.UpdateBool(PrefName.TimeCardsMakesAdjustmentsForOverBreaks,true);
			TimeCardRuleT.CreateAMTimeRule(emp.EmployeeNum,TimeSpan.FromHours(7.5));
			TimeCardRules.RefreshCache();
			long clockEvent1 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddHours(6),startDate.AddHours(16));
			ClockEventT.InsertBreak(emp.EmployeeNum,startDate.AddHours(11),40);
			TimeCardRules.CalculateDailyOvertime(emp,payP1.DateStart,payP1.DateStop);
			//Validate
			string retVal="";
			//Check
			if(ClockEvents.GetOne(clockEvent1).AdjustAuto!=TimeSpan.FromMinutes(-10)) {
				throw new Exception("Clock adjustment should be -10 minutes, instead it is " + ClockEvents.GetOne(clockEvent1).AdjustAuto.TotalMinutes + " minutes.\r\n");
			}
			if(ClockEvents.GetOne(clockEvent1).OTimeAuto!=TimeSpan.FromMinutes(90)) {
				throw new Exception("Clock ovetime should be 90 minutes, instead it is " + ClockEvents.GetOne(clockEvent1).OTimeAuto.TotalMinutes + " minutes.\r\n");
			}
			retVal+="23: Passed. Overtime calculated properly before time of day.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestTwentyFour(int specificTest) {
			if(specificTest != 0 && specificTest !=24) {
				return "";
			}
			string suffix="24";
			DateTime startDate=DateTime.Parse("2001-01-01");
			Employee emp = EmployeeT.CreateEmployee(suffix);
			PayPeriod payP1 = PayPeriodT.CreateTwoWeekPayPeriodIfNotExists(startDate);
			PayPeriods.RefreshCache();
			Prefs.UpdateInt(PrefName.TimeCardOvertimeFirstDayOfWeek,0);
			Prefs.UpdateBool(PrefName.TimeCardsMakesAdjustmentsForOverBreaks,true);
			TimeCardRuleT.CreateHoursTimeRule(emp.EmployeeNum,TimeSpan.FromHours(10));
			TimeCardRules.RefreshCache();
			long clockEvent1 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddHours(8),startDate.AddHours(13));
			long clockEvent2 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddHours(14),startDate.AddHours(21));
			ClockEventT.InsertBreak(emp.EmployeeNum,startDate.AddHours(10),20);
			ClockEventT.InsertBreak(emp.EmployeeNum,startDate.AddHours(16),20);
			TimeCardRules.CalculateDailyOvertime(emp,payP1.DateStart,payP1.DateStop);
			//Validate
			string retVal="";
			//Check
			if(ClockEvents.GetOne(clockEvent2).AdjustAuto!=TimeSpan.FromMinutes(-10)) {
				throw new Exception("Clock adjustment should be -10 minutes, instead it is " + ClockEvents.GetOne(clockEvent2).AdjustAuto.TotalMinutes + " minutes.\r\n");
			}
			if(ClockEvents.GetOne(clockEvent2).OTimeAuto!=TimeSpan.FromMinutes(110)) {
				throw new Exception("Clock ovetime should be 110 minutes, instead it is " + ClockEvents.GetOne(clockEvent2).OTimeAuto.TotalMinutes + " minutes.\r\n");
			}
			retVal+="24: Passed. Overtime calculated properly for total hours per day.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestTwentyFive(int specificTest) {
			if(specificTest != 0 && specificTest !=25) {
				return "";
			}
			string suffix="25";
			DateTime startDate=DateTime.Parse("2001-01-01");
			Employee emp = EmployeeT.CreateEmployee(suffix);
			PayPeriod payP1 = PayPeriodT.CreateTwoWeekPayPeriodIfNotExists(startDate);
			PayPeriods.RefreshCache();
			Prefs.UpdateInt(PrefName.TimeCardOvertimeFirstDayOfWeek,0);
			TimeCardRules.RefreshCache();
			long clockEvent1 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(0).AddHours(6),startDate.AddDays(0).AddHours(17));
			long clockEvent2 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(1).AddHours(6),startDate.AddDays(1).AddHours(17));
			long clockEvent3 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(2).AddHours(6),startDate.AddDays(2).AddHours(17));
			long clockEvent4 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(3).AddHours(6),startDate.AddDays(3).AddHours(17));
			TimeCardRules.CalculateWeeklyOvertime(emp,payP1.DateStart,payP1.DateStop);
			//Validate
			string retVal="";
			//Check
			TimeAdjust result = TimeAdjusts.Refresh(emp.EmployeeNum,startDate,startDate.AddDays(13))[0];
			if(result.RegHours!=TimeSpan.FromHours(-4)){
				throw new Exception("Time adjustment to regular hours should be -4 hours, instead it is " + result.RegHours.TotalHours + " hours.\r\n");
			}
			if(result.OTimeHours!=TimeSpan.FromHours(4)) {
				throw new Exception("Time adjustment to OT hours should be 4 hours, instead it is " + result.OTimeHours.TotalHours + " hours.\r\n");
			}
			retVal+="25: Passed. Overtime calculated properly for normal 40 hour work week.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestTwentySix(int specificTest) {
			if(specificTest != 0 && specificTest !=26) {
				return "";
			}
			string suffix="26";
			DateTime startDate=DateTime.Parse("2001-02-01");//This will create a pay period that splits a work week.
			Employee emp = EmployeeT.CreateEmployee(suffix);
			PayPeriod payP1 = PayPeriodT.CreateTwoWeekPayPeriodIfNotExists(startDate);
			PayPeriod payP2 = PayPeriodT.CreateTwoWeekPayPeriodIfNotExists(startDate.AddDays(14));
			PayPeriods.RefreshCache();
			Prefs.UpdateInt(PrefName.TimeCardOvertimeFirstDayOfWeek,0);
			TimeCardRules.RefreshCache();
			long clockEvent1 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(10).AddHours(6),startDate.AddDays(10).AddHours(17));
			long clockEvent2 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(11).AddHours(6),startDate.AddDays(11).AddHours(17));
			long clockEvent3 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(12).AddHours(6),startDate.AddDays(12).AddHours(17));
			//new pay period
			long clockEvent4 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(14).AddHours(6),startDate.AddDays(14).AddHours(17));
			TimeCardRules.CalculateWeeklyOvertime(emp,payP1.DateStart,payP1.DateStop);
			TimeCardRules.CalculateWeeklyOvertime(emp,payP2.DateStart,payP2.DateStop);
			//Validate
			string retVal="";
			//Check
			
			TimeAdjust result = TimeAdjusts.Refresh(emp.EmployeeNum,startDate,startDate.AddDays(28))[0];
			if(result.RegHours!=TimeSpan.FromHours(-4)) {
				throw new Exception("Time adjustment to regular hours should be -4 hours, instead it is " + result.RegHours.TotalHours + " hours.\r\n");
			}
			if(result.OTimeHours!=TimeSpan.FromHours(4)) {
				throw new Exception("Time adjustment to OT hours should be 4 hours, instead it is " + result.OTimeHours.TotalHours + " hours.\r\n");
			}
			retVal+="26: Passed. Overtime calculated properly for work week spanning 2 pay periods.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestTwentySeven(int specificTest) {
			if(specificTest != 0 && specificTest !=27) {
				return "";
			}
			string suffix="27";
			DateTime startDate=DateTime.Parse("2001-01-01");
			Employee emp = EmployeeT.CreateEmployee(suffix);
			PayPeriod payP1 = PayPeriodT.CreateTwoWeekPayPeriodIfNotExists(startDate);
			PayPeriods.RefreshCache();
			Prefs.UpdateInt(PrefName.TimeCardOvertimeFirstDayOfWeek,3);
			TimeCardRules.RefreshCache();
			long clockEvent1 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(0).AddHours(6),startDate.AddDays(0).AddHours(17));
			long clockEvent2 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(1).AddHours(6),startDate.AddDays(1).AddHours(17));
			//new work week
			long clockEvent3 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(2).AddHours(6),startDate.AddDays(2).AddHours(17));
			long clockEvent4 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(3).AddHours(6),startDate.AddDays(3).AddHours(17));
			long clockEvent5 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(4).AddHours(6),startDate.AddDays(4).AddHours(17));
			long clockEvent6 = ClockEventT.InsertWorkPeriod(emp.EmployeeNum,startDate.AddDays(5).AddHours(6),startDate.AddDays(5).AddHours(17));
			TimeCardRules.CalculateWeeklyOvertime(emp,payP1.DateStart,payP1.DateStop);
			//Validate
			string retVal="";
			//Check
			TimeAdjust result = TimeAdjusts.Refresh(emp.EmployeeNum,startDate,startDate.AddDays(28))[0];
			if(result.RegHours!=TimeSpan.FromHours(-4)) {
				throw new Exception("Time adjustment to regular hours should be -4 hours, instead it is " + result.RegHours.TotalHours + " hours.\r\n");
			}
			if(result.OTimeHours!=TimeSpan.FromHours(4)) {
				throw new Exception("Time adjustment to OT hours should be 4 hours, instead it is " + result.OTimeHours.TotalHours + " hours.\r\n");
			}
			retVal+="27: Passed. Overtime calculated properly for work week not starting on Sunday.\r\n";
			return retVal;
		}

		///<summary>This unit test is the first one that looks at the values showing in the claimproc window.  This catches situations where the only "bug" is just a display issue in that window. Validates the values in the claimproc window when opened from the Chart module.</summary>
		public static string TestTwentyEight(int specificTest) {
			if(specificTest != 0 && specificTest !=28) {
				return "";
			}
			string suffix="28";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,plan.PlanNum);
			long subNum=sub.InsSubNum;
			BenefitT.CreateAnnualMax(plan.PlanNum,1300);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Crowns,50);
			BenefitT.CreateDeductibleGeneral(plan.PlanNum,BenefitCoverageLevel.Individual,25);
			PatPlanT.CreatePatPlan(1,pat.PatNum,subNum);
			//proc1 - crown
			Procedure proc1=ProcedureT.CreateProcedure(pat,"D2790",ProcStat.TP,"1",800);//Tooth 1
			ProcedureT.SetPriority(proc1,0);//Priority 1
			//proc2 - crown
			Procedure proc2=ProcedureT.CreateProcedure(pat,"D2790",ProcStat.TP,"9",800);//Tooth 9
			ProcedureT.SetPriority(proc2,1);//Priority 2
			//Lists:
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			List<Procedure> ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			//Mimick the TP module estimate calculations when the TP module is loaded. We expect the user to refresh the TP module to calculate insurance estimates for all other areas of the program.
			string retVal="";
			for(int i=0;i<ProcListTP.Length;i++) {
				Procedures.ComputeEstimates(ProcListTP[i],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
					histList,loopList,false,pat.Age,subList);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
			}
			//Save changes in the list to the database, just like the TP module does when loaded. Then the values can be referenced elsewhere in the program instead of recalculating.
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			//Validate the estimates within the Edit Claim Proc window are correct when opened from inside of the Chart module by passing in a null histlist and a null looplist just like the Chart module would.
			List<ClaimProcHist> histListNull=null;
			List<ClaimProcHist> loopListNull=null;
			ClaimProc claimProc1=ClaimProcs.GetEstimate(claimProcs,proc1.ProcNum,plan.PlanNum,subNum);
			FormClaimProc formCP1=new FormClaimProc(claimProc1,proc1,fam,pat,planList,histListNull,ref loopListNull,patPlans,false,subList);
			formCP1.Initialize();
			string dedEst1=formCP1.GetTextValue("textDedEst");
			if(dedEst1!="25.00") {
				throw new Exception("Deductible estimate in Claim Proc Edit window is $"+dedEst1+" but should be $25.00 for proc1 from Chart module. \r\n");
			}
			string patPortCP1=formCP1.GetTextValue("textPatPortion1");
			if(patPortCP1!="412.50") {
				throw new Exception("Estimated patient portion in Claim Proc Edit window is $"+patPortCP1+" but should be $412.50 for proc1 from Chart module. \r\n");
			}
			ClaimProc claimProc2=ClaimProcs.GetEstimate(claimProcs,proc2.ProcNum,plan.PlanNum,subNum);
			FormClaimProc formCP2=new FormClaimProc(claimProc2,proc2,fam,pat,planList,histListNull,ref loopListNull,patPlans,false,subList);
			formCP2.Initialize();
			string dedEst2=formCP2.GetTextValue("textDedEst");
			if(dedEst2!="0.00") {
				throw new Exception("Deductible estimate in Claim Proc Edit window is $"+dedEst2+" but should be $0.00 for proc2 from Chart module. \r\n");
			}
			string patPortCP2=formCP2.GetTextValue("textPatPortion1");
			if(patPortCP2!="400.00") {
				throw new Exception("Estimated patient portion in Claim Proc Edit window is $"+patPortCP2+" but should be $400.00 for proc2 from Chart module. \r\n");
			}
			retVal+="28: Passed.  Claim Procedure Edit window estimates correct from Chart module.\r\n";
			return retVal;
		}

		///<summary>Validates the values in the claimproc window when opened from the Claim Edit window.</summary>
		public static string TestTwentyNine(int specificTest) {
			if(specificTest != 0 && specificTest !=29) {
				return "";
			}
			string suffix="29";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,plan.PlanNum);
			long subNum=sub.InsSubNum;
			BenefitT.CreateAnnualMax(plan.PlanNum,1300);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Crowns,50);
			BenefitT.CreateDeductibleGeneral(plan.PlanNum,BenefitCoverageLevel.Individual,25);
			PatPlanT.CreatePatPlan(1,pat.PatNum,subNum);
			//proc1 - crown
			Procedure proc1=ProcedureT.CreateProcedure(pat,"D2790",ProcStat.C,"1",800);//Tooth 1
			ProcedureT.SetPriority(proc1,0);//Priority 1
			//proc2 - crown
			Procedure proc2=ProcedureT.CreateProcedure(pat,"D2790",ProcStat.C,"9",800);//Tooth 9
			ProcedureT.SetPriority(proc2,1);//Priority 2
			//Lists:
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<Procedure> ProcList=Procedures.Refresh(pat.PatNum);
			string retVal="";
			Claim claim=ClaimT.CreateClaim("P",patPlans,planList,claimProcs,ProcList,pat,ProcList,benefitList,subList);//Creates the claim in the same manner as the account module, including estimates.
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			//Validate the estimates as they would appear inside of the Claim Proc Edit window when opened from inside of the Edit Claim window by passing in the null histlist and null looplist that the Claim Edit window would send in.
			List<ClaimProcHist> histList=null;
			List<ClaimProcHist> loopList=null;
			ClaimProc claimProc1=ClaimProcs.GetEstimate(claimProcs,proc1.ProcNum,plan.PlanNum,subNum);
			FormClaimProc formCP1=new FormClaimProc(claimProc1,proc1,fam,pat,planList,histList,ref loopList,patPlans,false,subList);
			formCP1.IsInClaim=true;
			formCP1.Initialize();
			string dedEst1=formCP1.GetTextValue("textDedEst");
			if(dedEst1!="25.00") {
				throw new Exception("Deductible estimate in Claim Proc Edit window is $"+dedEst1+" but should be $25.00 for proc1 from Edit Claim Window. \r\n");
			}
			string patPortCP1=formCP1.GetTextValue("textPatPortion1");
			if(patPortCP1!="412.50") {
				throw new Exception("Estimated patient portion in Claim Proc Edit window is $"+patPortCP1+" but should be $412.50 for proc1 from Edit Claim Window. \r\n");
			}
			ClaimProc claimProc2=ClaimProcs.GetEstimate(claimProcs,proc2.ProcNum,plan.PlanNum,subNum);
			FormClaimProc formCP2=new FormClaimProc(claimProc2,proc2,fam,pat,planList,histList,ref loopList,patPlans,false,subList);
			formCP2.IsInClaim=true;
			formCP2.Initialize();
			string dedEst2=formCP2.GetTextValue("textDedEst");
			if(dedEst2!="0.00") {
				throw new Exception("Deductible estimate in Claim Proc Edit window is $"+dedEst2+" but should be $0.00 for proc2 from Edit Claim Window. \r\n");
			}
			string patPortCP2=formCP2.GetTextValue("textPatPortion1");
			if(patPortCP2!="400.00") {
				throw new Exception("Estimated patient portion in Claim Proc Edit window is $"+patPortCP2+" but should be $400.00 for proc2 from Edit Claim Window. \r\n");
			}
			retVal+="29: Passed.  Claim Procedure Edit window estimates correct from Claim Edit window.\r\n";
			return retVal;
		}

		///<summary>Validates the values in the claimproc window when opened from the TP module.</summary>
		public static string TestThirty(int specificTest) {
			if(specificTest != 0 && specificTest !=30) {
				return "";
			}
			string suffix="30";
			Patient pat=PatientT.CreatePatient(suffix);
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,plan.PlanNum);
			long subNum=sub.InsSubNum;
			BenefitT.CreateAnnualMax(plan.PlanNum,1300);
			BenefitT.CreateCategoryPercent(plan.PlanNum,EbenefitCategory.Crowns,50);
			BenefitT.CreateDeductibleGeneral(plan.PlanNum,BenefitCoverageLevel.Individual,25);
			PatPlanT.CreatePatPlan(1,pat.PatNum,subNum);
			//proc1 - crown
			Procedure proc1=ProcedureT.CreateProcedure(pat,"D2790",ProcStat.TP,"1",800);//Tooth 1
			ProcedureT.SetPriority(proc1,0);//Priority 1
			//proc2 - crown
			Procedure proc2=ProcedureT.CreateProcedure(pat,"D2790",ProcStat.TP,"9",800);//Tooth 9
			ProcedureT.SetPriority(proc2,1);//Priority 2
			//Lists:
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(pat.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=new List<ClaimProcHist>();
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			List<Procedure> ProcList=Procedures.Refresh(pat.PatNum);
			Procedure[] ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			//Validate
			//Mimick the TP module estimate calculations when the TP module is loaded.
			string retVal="";
			for(int i=0;i<ProcListTP.Length;i++) {
				Procedures.ComputeEstimates(ProcListTP[i],pat.PatNum,ref claimProcs,false,planList,patPlans,benefitList,
					histList,loopList,false,pat.Age,subList);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
			}
			//Save changes in the list to the database, just like the TP module does when loaded.
			ClaimProcs.Synch(ref claimProcs,claimProcListOld);
			claimProcs=ClaimProcs.Refresh(pat.PatNum);
			//Validate the estimates within the Edit Claim Proc window are correct when opened from inside of the TP module by passing in same histlist and loop list that the TP module would.
			histList=ClaimProcs.GetHistList(pat.PatNum,benefitList,patPlans,planList,DateTime.Today,subList);//The history list is fetched when the TP module is loaded and is passed in the same for all claimprocs.
			loopList=new List<ClaimProcHist>();//Always empty for the first claimproc.
			ClaimProc claimProc1=ClaimProcs.GetEstimate(claimProcs,proc1.ProcNum,plan.PlanNum,subNum);
			FormClaimProc formCP1=new FormClaimProc(claimProc1,proc1,fam,pat,planList,histList,ref loopList,patPlans,false,subList);
			formCP1.Initialize();
			string dedEst1=formCP1.GetTextValue("textDedEst");
			if(dedEst1!="25.00") {
				throw new Exception("Deductible estimate in Claim Proc Edit window is $"+dedEst1+" but should be $25.00 for proc1 from TP module. \r\n");
			}
			string patPortCP1=formCP1.GetTextValue("textPatPortion1");
			if(patPortCP1!="412.50") {
				throw new Exception("Estimated patient portion in Claim Proc Edit window is $"+patPortCP1+" but should be $412.50 for proc1 from TP module. \r\n");
			}
			ClaimProc claimProc2=ClaimProcs.GetEstimate(claimProcs,proc2.ProcNum,plan.PlanNum,subNum);
			histList=ClaimProcs.GetHistList(pat.PatNum,benefitList,patPlans,planList,DateTime.Today,subList);//The history list is fetched when the TP module is loaded and is passed in the same for all claimprocs.
			loopList=new List<ClaimProcHist>();
			loopList.AddRange(ClaimProcs.GetHistForProc(claimProcs,proc1.ProcNum,proc1.CodeNum));
			FormClaimProc formCP2=new FormClaimProc(claimProc2,proc2,fam,pat,planList,histList,ref loopList,patPlans,false,subList);
			formCP2.Initialize();
			string dedEst2=formCP2.GetTextValue("textDedEst");
			if(dedEst2!="0.00") {
				throw new Exception("Deductible estimate in Claim Proc Edit window is $"+dedEst2+" but should be $0.00 for proc2 from TP module. \r\n");
			}
			string patPortCP2=formCP2.GetTextValue("textPatPortion1");
			if(patPortCP2!="400.00") {
				throw new Exception("Estimated patient portion in Claim Proc Edit window is $"+patPortCP2+" but should be $400.00 for proc2 from TP module. \r\n");
			}
			retVal+="30: Passed.  Claim Procedure Edit window estimates correct from TP module.\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string TestThirtyOne(int specificTest) {
			if(specificTest != 0 && specificTest !=311) {
				return "";
			}
			string suffix="31";
			Patient pat=PatientT.CreatePatient(suffix);
			long patNum=pat.PatNum;
			Carrier carrier=CarrierT.CreateCarrier(suffix);
			InsPlan plan=InsPlanT.CreateInsPlan(carrier.CarrierNum);
			long planNum=plan.PlanNum;
			InsSub sub=InsSubT.CreateInsSub(pat.PatNum,planNum);//guarantor is subscriber
			long subNum=sub.InsSubNum;
			long patPlanNum=PatPlanT.CreatePatPlan(1,pat.PatNum,subNum).PatPlanNum;
			BenefitT.CreateAnnualMax(planNum,1000);
			BenefitT.CreateCategoryPercent(planNum,EbenefitCategory.RoutinePreventive,100);
			BenefitT.CreateLimitation(planNum,EbenefitCategory.RoutinePreventive,0);//The amount has no effect.
			Procedure proc=ProcedureT.CreateProcedure(pat,"D1110",ProcStat.C,"",125);//Prophy
			long procNum=proc.ProcNum;
			//Lists
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(pat.PatNum);
			Family fam=Patients.GetFamily(patNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans,subList);
			List<ClaimProcHist> histList=ClaimProcs.GetHistList(patNum,benefitList,patPlans,planList,DateTime.Today,subList);
			List<Procedure> ProcList=Procedures.Refresh(pat.PatNum);
			Claim claim=ClaimT.CreateClaim("P",patPlans,planList,claimProcs,ProcList,pat,ProcList,benefitList,subList);//Creates the claim in the same manner as the account module, including estimates and status NotReceived.
			//Validate
			double pending=InsPlans.GetPendingDisplay(histList,DateTime.Today,plan,patPlanNum,-1,patNum,subNum);
			if(pending!=0) {
				throw new Exception("Pending amount should be 0.\r\n");
			}
			return "31: Passed.  \r\n";
		}


	}
}
