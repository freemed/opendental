using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace OpenDental {
	public class InsPlanL {
		/// <summary>Only used from FormInsPlan. Throws ApplicationException if any dependencies. This is quite complex, because it also must update all claimprocs for all patients affected by the deletion.  Also deletes patplans, benefits, and claimprocs.</summary>
		public static void Delete(InsPlan plan) {
			//first, check claims
			string command="SELECT PatNum FROM claim "
				+"WHERE plannum = '"+plan.PlanNum.ToString()+"' ";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				command+="AND ROWNUM<=1";
			}
			else {//Assume MySQL
				command+="LIMIT 1";
			}
			DataTable table=General.GetTable(command);
			if(table.Rows.Count!=0) {
				throw new ApplicationException(Lan.g("FormInsPlan","Not allowed to delete a plan with existing claims."));
			}
			//then, check claimprocs
			command="SELECT PatNum FROM claimproc "
				+"WHERE PlanNum = "+POut.PInt(plan.PlanNum)
				+" AND Status != 6 ";//ignore estimates
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				command+="AND ROWNUM<=1";
			}
			else {//Assume MySQL
				command+="LIMIT 1";
			}
			table=General.GetTable(command);
			if(table.Rows.Count!=0) {
				throw new ApplicationException(Lan.g("FormInsPlan","Not allowed to delete a plan attached to procedures."));
			}
			//get a list of all patplans with this planNum
			command="SELECT PatPlanNum FROM patplan "
				+"WHERE PlanNum = "+plan.PlanNum.ToString();
			table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				//benefits with this PatPlanNum are also deleted here
				PatPlanL.Delete(PIn.PInt(table.Rows[i][0].ToString()));
			}
			command="DELETE FROM benefit WHERE PlanNum="+POut.PInt(plan.PlanNum);
			General.NonQ(command);
			command="DELETE FROM claimproc WHERE PlanNum="+POut.PInt(plan.PlanNum);//just estimates
			General.NonQ(command);
			command="DELETE FROM insplan "
				+"WHERE PlanNum = '"+plan.PlanNum.ToString()+"'";
			General.NonQ(command);
		}

		///<summary>Returns -1 if no copay feeschedule.  Can return -1 if copay amount is blank.</summary>
		public static double GetCopay(string myCode,InsPlan plan) {
			if(plan==null) {
				return -1;
			}
			if(plan.CopayFeeSched==0) {
				return -1;
			}
			double retVal=Fees.GetAmount(ProcedureCodes.GetCodeNum(myCode),plan.CopayFeeSched);
			if(retVal==-1) {//blank co-pay
				if(PrefC.GetBool("CoPay_FeeSchedule_BlankLikeZero")) {
					return -1;//will act like zero.  No patient co-pay.
				}
				else {
					//The amount from the regular fee schedule
					//In other words, the patient is responsible for procs that are not specified in a managed care fee schedule.
					return Fees.GetAmount(ProcedureCodes.GetCodeNum(myCode),plan.FeeSched);
				}
			}
			return retVal;
		}

		///<summary>Returns -1 if no allowed feeschedule or fee unknown for this procCode. Otherwise, returns the allowed fee including 0. Can handle a planNum of 0.  Tooth num is used for posterior composites.  It can be left blank in some situations.  Provider must be supplied in case plan has no assigned fee schedule.  Then it will use the fee schedule for the provider.</summary>
		public static double GetAllowed(string procCode,int planNum,InsPlan[] PlanList,string toothNum,int provNum) {
			if(planNum==0) {
				return -1;
			}
			InsPlan plan=InsPlans.GetPlan(planNum,PlanList);
			if(plan==null) {
				return -1;
			}
			int codeNum=ProcedureCodes.GetCodeNum(procCode);
			int substCodeNum=codeNum;
			if(!plan.CodeSubstNone) {
				substCodeNum=ProcedureCodes.GetSubstituteCodeNum(procCode,toothNum);//for posterior composites
			}
			//PPO always returns the PPO fee for the code or substituted code.
			if(plan.PlanType=="p") {
				return Fees.GetAmount(substCodeNum,plan.FeeSched);
			}
			//or, if not PPO, and an allowed fee schedule exists, then we use that.
			if(plan.AllowedFeeSched!=0) {
				return Fees.GetAmount(substCodeNum,plan.AllowedFeeSched);//whether post composite or not
			}
			//must be an ordinary fee schedule, so if no substitution code, then no allowed override
			if(codeNum==substCodeNum) {
				return -1;
			}
			//must be posterior composite with an ordinary fee schedule
			//Although it won't happen very often, it's possible that there is no fee schedule assigned to the plan.
			if(plan.FeeSched==0) {
				return Fees.GetAmount(substCodeNum,Providers.GetProv(provNum).FeeSched);
			}
			return Fees.GetAmount(substCodeNum,plan.FeeSched);
		}

		///<summary>Used when closing the edit plan window to find all patients using this plan and to update all claimProcs for each patient.  This keeps estimates correct.</summary>
		public static void ComputeEstimatesForPlan(int planNum) {
			string command="SELECT PatNum FROM patplan WHERE PlanNum="+POut.PInt(planNum);
			DataTable table=General.GetTable(command);
			int patNum=0;
			for(int i=0;i<table.Rows.Count;i++) {
				patNum=PIn.PInt(table.Rows[i][0].ToString());
				Family fam=Patients.GetFamily(patNum);
				Patient pat=fam.GetPatient(patNum);
				ClaimProc[] claimProcs=ClaimProcs.Refresh(patNum);
				Procedure[] procs=Procedures.Refresh(patNum);
				InsPlan[] plans=InsPlans.Refresh(fam);
				PatPlan[] patPlans=PatPlans.Refresh(patNum);
				Benefit[] benefitList=Benefits.Refresh(patPlans);
				ProcedureL.ComputeEstimatesForAll(patNum,claimProcs,procs,plans,patPlans,benefitList);
				Patients.SetHasIns(patNum);
			}
		}









	}
}
