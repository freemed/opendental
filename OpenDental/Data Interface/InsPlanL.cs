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