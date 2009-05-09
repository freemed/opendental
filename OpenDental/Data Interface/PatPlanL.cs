using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace OpenDental {
	public class PatPlanL {
		///<summary>Deletes the patplan with the specified patPlanNum.  Rearranges the other patplans for the patient to keep the ordinal sequence contiguous.  Then, recomputes all estimates for this patient because their coverage is now different.  Also sets patient.HasIns to the correct value.</summary>
		public static void Delete(int patPlanNum) {
			string command="SELECT PatNum FROM patplan WHERE PatPlanNum="+POut.PInt(patPlanNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0) {
				return;
			}
			int patNum=PIn.PInt(table.Rows[0][0].ToString());
			List <PatPlan> patPlans=PatPlans.Refresh(patNum);
			bool doDecrement=false;
			for(int i=0;i<patPlans.Count;i++) {
				if(doDecrement) {//patPlan has already been deleted, so decrement the rest.
					command="UPDATE patplan SET Ordinal="+POut.PInt(patPlans[i].Ordinal-1)
						+" WHERE PatPlanNum="+POut.PInt(patPlans[i].PatPlanNum);
					Db.NonQ(command);
					continue;
				}
				if(patPlans[i].PatPlanNum==patPlanNum) {
					command="DELETE FROM patplan WHERE PatPlanNum="+POut.PInt(patPlanNum);
					Db.NonQ(command);
					command="DELETE FROM benefit WHERE PatPlanNum=" +POut.PInt(patPlanNum);
					Db.NonQ(command);
					doDecrement=true;
				}
			}
			Family fam=Patients.GetFamily(patNum);
			Patient pat=fam.GetPatient(patNum);
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
			List<Procedure> procs=Procedures.Refresh(patNum);
			patPlans=PatPlans.Refresh(patNum);
			List <InsPlan> planList=InsPlans.Refresh(fam);
			List <Benefit> benList=Benefits.Refresh(patPlans);
			ProcedureL.ComputeEstimatesForAll(patNum,claimProcs,procs,planList,patPlans,benList);
			Patients.SetHasIns(patNum);
		}





	}
}
