using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PatPlans {
		///<summary>Gets a list of all patplans for a given patient</summary>
		public static List<PatPlan> Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PatPlan>>(MethodBase.GetCurrentMethod(),patNum);
			} 
			string command="SELECT * from patplan"
				+" WHERE PatNum = "+patNum.ToString()
				+" ORDER BY Ordinal";
			return Crud.PatPlanCrud.SelectMany(command);
		}

		///<summary></summary>
		public static void Update(PatPlan patPlan) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patPlan);
				return;
			}
			//ordinal was already set using SetOrdinal, but it's harmless to set it again.
			Crud.PatPlanCrud.Update(patPlan);
		}

		///<summary></summary>
		public static long Insert(PatPlan patPlan) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				patPlan.PatPlanNum=Meth.GetLong(MethodBase.GetCurrentMethod(),patPlan);
				return patPlan.PatPlanNum;
			}
			return Crud.PatPlanCrud.Insert(patPlan);
		}

		///<summary>Supply a PatPlan list.  This function loops through the list and returns the plan num of the specified ordinal.  If ordinal not valid, then it returns 0.  The main purpose of this function is so we don't have to check the length of the list.</summary>
		public static long GetPlanNum(List<PatPlan> list,int ordinal) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<list.Count;i++){
				if(list[i].Ordinal==ordinal){
					return list[i].PlanNum;
				}
			}
			return 0;
		}

		///<summary>Supply a PatPlan list.  This function loops through the list and returns the insSubNum of the specified ordinal.  If ordinal not valid, then it returns 0.  The main purpose of this function is so we don't have to check the length of the list.</summary>
		public static long GetInsSubNum(List<PatPlan> list,int ordinal) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<list.Count;i++) {
				if(list[i].Ordinal==ordinal) {
					return list[i].InsSubNum;
				}
			}
			return 0;
		}

		///<summary>Supply a PatPlan list.  This function loops through the list and returns the relationship of the specified ordinal.  If ordinal not valid, then it returns self (0).</summary>
		public static Relat GetRelat(List <PatPlan> list,int ordinal){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<list.Count;i++){
				if(list[i].Ordinal==ordinal){
					return list[i].Relationship;
				}
			}
			return Relat.Self;
		}

		public static string GetPatID(List<PatPlan> patPlans,long planNum) {
			//No need to check RemotingRole; no call to db.
			for(int p=0;p<patPlans.Count;p++) {
				if(patPlans[p].PlanNum==planNum) {
					return patPlans[p].PatID;
				}
			}
			return "";
		}

		///<summary>Will return 1 for primary insurance, etc.  Will return 0 if planNum not found in the list.</summary>
		public static int GetOrdinal(List<PatPlan> patPlans,long planNum) {
			//No need to check RemotingRole; no call to db.
			for(int p=0;p<patPlans.Count;p++) {
				if(patPlans[p].PlanNum==planNum) {
					return patPlans[p].Ordinal;
				}
			}
			return 0;
		}

		///<summary>Will return null if planNum not found in the list.</summary>
		public static PatPlan GetFromList(List<PatPlan> patPlans,long planNum) {
			//No need to check RemotingRole; no call to db.
			for(int p=0;p<patPlans.Count;p++) {
				if(patPlans[p].PlanNum==planNum) {
					return patPlans[p];
				}
			}
			return null;
		}

		///<summary>Sets the ordinal of the specified patPlan.  Rearranges the other patplans for the patient to keep the ordinal sequence contiguous.  Estimates must be recomputed after this.  FormInsPlan currently updates estimates every time it closes.  Only used in one place.  Returns the new ordinal.</summary>
		public static int SetOrdinal(long patPlanNum,int newOrdinal) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),patPlanNum,newOrdinal);
			}
			string command="SELECT PatNum FROM patplan WHERE PatPlanNum="+POut.Long(patPlanNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return 1;
			}
			long patNum=PIn.Long(table.Rows[0][0].ToString());
			List<PatPlan> patPlans=Refresh(patNum);
			//int oldOrdinal=GetFromList(patPlans,patPlanNum).Ordinal;
			if(newOrdinal>patPlans.Count){
				newOrdinal=patPlans.Count;
			}
			if(newOrdinal<1){
				newOrdinal=1;
			}
			int curOrdinal=1;
			for(int i=0;i<patPlans.Count;i++){//Loop through each patPlan.
				if(patPlans[i].PatPlanNum==patPlanNum){
					continue;//the one we are setting will be handled later
				}
				if(curOrdinal==newOrdinal){
					curOrdinal++;//skip the newOrdinal when setting the sequence for the others.
				}
				command="UPDATE patplan SET Ordinal="+POut.Long(curOrdinal)
					+" WHERE PatPlanNum="+POut.Long(patPlans[i].PatPlanNum);
				Db.NonQ(command);
				curOrdinal++;
			}
			command="UPDATE patplan SET Ordinal="+POut.Long(newOrdinal)
				+" WHERE PatPlanNum="+POut.Long(patPlanNum);
			Db.NonQ(command);
			return newOrdinal;
		}

		///<summary>Loops through the supplied list to find the one patplan needed.</summary>
		public static PatPlan GetFromList(PatPlan[] patPlans,long patPlanNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<patPlans.Length;i++){
				if(patPlans[i].PatPlanNum==patPlanNum){
					return patPlans[i].Copy();
				}
			}
			return null;
		}

		///<summary>Loops through the supplied list to find the one patplanNum needed based on the planNum.  Returns 0 if patient is not currently covered by the planNum supplied.</summary>
		public static long GetPatPlanNum(List<PatPlan> patPlanList,long planNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<patPlanList.Count;i++) {
				if(patPlanList[i].PlanNum==planNum) {
					return patPlanList[i].PatPlanNum;
				}
			}
			return 0;
		}

		///<summary>Gets one patPlanNum directly from database.  Only used once in FormClaimProc.</summary>
		public static long GetPatPlanNum(long patNum,long planNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),patNum,planNum);
			}
			string command="SELECT PatPlanNum FROM patplan WHERE PatNum="+POut.Long(patNum)+" AND PlanNum="+POut.Long(planNum);
			return PIn.Long(Db.GetScalar(command));
		}

		///<summary>Gets directly from database.  Used by Trojan. Also used in a few loops where it shouldn't be.  It will be faster to replace this with a single query instead of those loops.</summary>
		public static PatPlan[] GetByPlanNum(long planNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PatPlan[]>(MethodBase.GetCurrentMethod(),planNum);
			} 
			string command="SELECT * FROM patplan WHERE PlanNum='"+POut.Long(planNum)+"'";
			return Crud.PatPlanCrud.SelectMany(command).ToArray();
		}

		///<summary>Will return null if none exists.</summary>
		public static PatPlan GetPatPlan(long patNum,int ordinal) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PatPlan>(MethodBase.GetCurrentMethod(),patNum,ordinal);
			} 
			string command="SELECT * FROM patplan WHERE PatNum="+POut.Long(patNum)
				+" AND Ordinal="+POut.Long(ordinal);
			return Crud.PatPlanCrud.SelectOne(command);
		}

		///<summary>Deletes the patplan with the specified patPlanNum.  Rearranges the other patplans for the patient to keep the ordinal sequence contiguous.  Then, recomputes all estimates for this patient because their coverage is now different.  Also sets patient.HasIns to the correct value.</summary>
		public static void Delete(long patPlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patPlanNum);
				return;
			} 
			string command="SELECT PatNum FROM patplan WHERE PatPlanNum="+POut.Long(patPlanNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0) {
				return;
			}
			long patNum=PIn.Long(table.Rows[0][0].ToString());
			List<PatPlan> patPlans=PatPlans.Refresh(patNum);
			bool doDecrement=false;
			for(int i=0;i<patPlans.Count;i++) {
				if(doDecrement) {//patPlan has already been deleted, so decrement the rest.
					command="UPDATE patplan SET Ordinal="+POut.Long(patPlans[i].Ordinal-1)
						+" WHERE PatPlanNum="+POut.Long(patPlans[i].PatPlanNum);
					Db.NonQ(command);
					continue;
				}
				if(patPlans[i].PatPlanNum==patPlanNum) {
					command="DELETE FROM patplan WHERE PatPlanNum="+POut.Long(patPlanNum);
					Db.NonQ(command);
					command="DELETE FROM benefit WHERE PatPlanNum=" +POut.Long(patPlanNum);
					Db.NonQ(command);
					doDecrement=true;
				}
			}
			Family fam=Patients.GetFamily(patNum);
			Patient pat=fam.GetPatient(patNum);
			List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
			List<Procedure> procs=Procedures.Refresh(patNum);
			patPlans=PatPlans.Refresh(patNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<Benefit> benList=Benefits.Refresh(patPlans);
			Procedures.ComputeEstimatesForAll(patNum,claimProcs,procs,planList,patPlans,benList,pat.Age);
			Patients.SetHasIns(patNum);
		}
		
	}
}










