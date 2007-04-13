using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class PatPlans {
		///<summary>Gets a list of all patplans for a given patient</summary>
		public static PatPlan[] Refresh(int patNum) {
			string command="SELECT * from patplan"
				+" WHERE PatNum = "+patNum.ToString()
				+" ORDER BY Ordinal";
			DataTable table=General.GetTable(command);
			PatPlan[] List=new PatPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new PatPlan();
				List[i].PatPlanNum  = PIn.PInt(table.Rows[i][0].ToString());
				List[i].PatNum      = PIn.PInt(table.Rows[i][1].ToString());
				List[i].PlanNum     = PIn.PInt(table.Rows[i][2].ToString());
				List[i].Ordinal     = PIn.PInt(table.Rows[i][3].ToString());
				List[i].IsPending   = PIn.PBool(table.Rows[i][4].ToString());
				List[i].Relationship= (Relat)PIn.PInt(table.Rows[i][5].ToString());
				List[i].PatID       = PIn.PString(table.Rows[i][6].ToString());
			}
			return List;
		}
	
		///<summary></summary>
		public static void Update(PatPlan p){
			string command="UPDATE patplan SET " 
				+"PatNum = '"       +POut.PInt   (p.PatNum)+"'"
				+",PlanNum = '"     +POut.PInt   (p.PlanNum)+"'"
				//+",Ordinal = '"     +POut.PInt   (Ordinal)+"'"//ordinal always set using SetOrdinal
				+",IsPending = '"   +POut.PBool  (p.IsPending)+"'"
				+",Relationship = '"+POut.PInt   ((int)p.Relationship)+"'"
				+",PatID = '"       +POut.PString(p.PatID)+"'"
				+" WHERE PatPlanNum = '" +POut.PInt(p.PatPlanNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(PatPlan p){
			if(PrefB.RandomKeys){
				p.PatPlanNum=MiscData.GetKey("patplan","PatPlanNum");
			}
			string command="INSERT INTO patplan (";
			if(PrefB.RandomKeys){
				command+="PatPlanNum,";
			}
			command+="PatNum,PlanNum,Ordinal,IsPending,Relationship,PatID) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(p.PatPlanNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (p.PatNum)+"', "
				+"'"+POut.PInt   (p.PlanNum)+"', "
				+"'"+POut.PInt   (p.Ordinal)+"', "
				+"'"+POut.PBool  (p.IsPending)+"', "
				+"'"+POut.PInt   ((int)p.Relationship)+"', "
				+"'"+POut.PString(p.PatID)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				p.PatPlanNum=General.NonQ(command,true);
			}
		}

		/*  Do NOT use this.  Use PatPlans.Delete() instead.
		///<summary></summary>
		public void Delete(){
			string command="DELETE FROM patplan WHERE PatPlanNum ="+POut.PInt(PatPlanNum);
			DataConnection dcon=new DataConnection();
			General.NonQ(command);
		}*/

		///<summary>Supply a PatPlan list.  This function loops through the list and returns the plan num of the specified ordinal.  If ordinal not valid, then it returns 0.  The main purpose of this function is so we don't have to check the length of the list.</summary>
		public static int GetPlanNum(PatPlan[] list,int ordinal){
			for(int i=0;i<list.Length;i++){
				if(list[i].Ordinal==ordinal){
					return list[i].PlanNum;
				}
			}
			return 0;
		}

		///<summary>Supply a PatPlan list.  This function loops through the list and returns the relationship of the specified ordinal.  If ordinal not valid, then it returns self (0).</summary>
		public static Relat GetRelat(PatPlan[] list,int ordinal){
			for(int i=0;i<list.Length;i++){
				if(list[i].Ordinal==ordinal){
					return list[i].Relationship;
				}
			}
			return Relat.Self;
		}

		public static string GetPatID(PatPlan[] patPlans,int planNum) {
			for(int p=0;p<patPlans.Length;p++) {
				if(patPlans[p].PlanNum==planNum) {
					return patPlans[p].PatID;
				}
			}
			return "";
		}

		///<summary>Deletes the patplan with the specified patPlanNum.  Rearranges the other patplans for the patient to keep the ordinal sequence contiguous.  Then, recomputes all estimates for this patient because their coverage is now different.  Also sets patient.HasIns to the correct value.</summary>
		public static void Delete(int patPlanNum){
			string command="SELECT PatNum FROM patplan WHERE PatPlanNum="+POut.PInt(patPlanNum);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			int patNum=PIn.PInt(table.Rows[0][0].ToString());
			PatPlan[] patPlans=Refresh(patNum);
			bool doDecrement=false;
			for(int i=0;i<patPlans.Length;i++){
				if(doDecrement){//patPlan has already been deleted, so decrement the rest.
					command="UPDATE patplan SET Ordinal="+POut.PInt(patPlans[i].Ordinal-1)
						+" WHERE PatPlanNum="+POut.PInt(patPlans[i].PatPlanNum);
					General.NonQ(command);
					continue;
				}
				if(patPlans[i].PatPlanNum==patPlanNum){
					command="DELETE FROM patplan WHERE PatPlanNum="+POut.PInt(patPlanNum);
					General.NonQ(command);
					command="DELETE FROM benefit WHERE PatPlanNum=" +POut.PInt(patPlanNum);
					General.NonQ(command);
					doDecrement=true;
				}
			}
			Family fam=Patients.GetFamily(patNum);
			Patient pat=fam.GetPatient(patNum);
			ClaimProc[] claimProcs=ClaimProcs.Refresh(patNum);
			Procedure[] procs=Procedures.Refresh(patNum);
			patPlans=PatPlans.Refresh(patNum);
			InsPlan[] planList=InsPlans.Refresh(fam);
			Benefit[] benList=Benefits.Refresh(patPlans);
			Procedures.ComputeEstimatesForAll(patNum,claimProcs,procs,planList,patPlans,benList);
			Patients.SetHasIns(patNum);
		}

		///<summary>Sets the ordinal of the specified patPlan.  Rearranges the other patplans for the patient to keep the ordinal sequence contiguous.  Estimates must be recomputed after this.  FormInsPlan currently updates estimates every time it closes.</summary>
		public static void SetOrdinal(int patPlanNum,int newOrdinal){
			string command="SELECT PatNum FROM patplan WHERE PatPlanNum="+POut.PInt(patPlanNum);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			int patNum=PIn.PInt(table.Rows[0][0].ToString());
			PatPlan[] patPlans=Refresh(patNum);
			//int oldOrdinal=GetFromList(patPlans,patPlanNum).Ordinal;
			if(newOrdinal>patPlans.Length){
				newOrdinal=patPlans.Length;
			}
			if(newOrdinal<1){
				newOrdinal=1;
			}
			int curOrdinal=1;
			for(int i=0;i<patPlans.Length;i++){//Loop through each patPlan.
				if(patPlans[i].PatPlanNum==patPlanNum){
					continue;//the one we are setting will be handled later
				}
				if(curOrdinal==newOrdinal){
					curOrdinal++;//skip the newOrdinal when setting the sequence for the others.
				}
				command="UPDATE patplan SET Ordinal="+POut.PInt(curOrdinal)
					+" WHERE PatPlanNum="+POut.PInt(patPlans[i].PatPlanNum);
				General.NonQ(command);
				curOrdinal++;
			}
			command="UPDATE patplan SET Ordinal="+POut.PInt(newOrdinal)
				+" WHERE PatPlanNum="+POut.PInt(patPlanNum);
			General.NonQ(command);
		}

		///<summary>Loops through the supplied list to find the one patplan needed.</summary>
		public static PatPlan GetFromList(PatPlan[] patPlans,int patPlanNum){
			for(int i=0;i<patPlans.Length;i++){
				if(patPlans[i].PatPlanNum==patPlanNum){
					return patPlans[i].Copy();
				}
			}
			return null;
		}

		///<summary>Loops through the supplied list to find the one patplanNum needed based on the planNum.  Returns 0 if patient is not currently covered by the planNum supplied.  Only used once in Claims.cs.</summary>
		public static int GetPatPlanNum(PatPlan[] patPlans,int planNum) {
			for(int i=0;i<patPlans.Length;i++) {
				if(patPlans[i].PlanNum==planNum) {
					return patPlans[i].PatPlanNum;
				}
			}
			return 0;
		}

		
		
	}

	

	


}










