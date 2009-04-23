using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PatPlans {
		///<summary>Gets a list of all patplans for a given patient</summary>
		public static List<PatPlan> Refresh(int patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PatPlan>>(MethodBase.GetCurrentMethod(),patNum);
			} 
			string command="SELECT * from patplan"
				+" WHERE PatNum = "+patNum.ToString()
				+" ORDER BY Ordinal";
			DataTable table=Db.GetTable(command);
			return RefreshAndFill(table);
		}

		private static List<PatPlan> RefreshAndFill(DataTable table){
			PatPlan patplan;
			List<PatPlan> retVal=new List<PatPlan>();
			for(int i=0;i<table.Rows.Count;i++) {
				patplan=new PatPlan();
				patplan.PatPlanNum  = PIn.PInt(table.Rows[i][0].ToString());
				patplan.PatNum      = PIn.PInt(table.Rows[i][1].ToString());
				patplan.PlanNum     = PIn.PInt(table.Rows[i][2].ToString());
				patplan.Ordinal     = PIn.PInt(table.Rows[i][3].ToString());
				patplan.IsPending   = PIn.PBool(table.Rows[i][4].ToString());
				patplan.Relationship= (Relat)PIn.PInt(table.Rows[i][5].ToString());
				patplan.PatID       = PIn.PString(table.Rows[i][6].ToString());
				retVal.Add(patplan);
			}
			return retVal;
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
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(PatPlan p){
			if(PrefC.RandomKeys){
				p.PatPlanNum=MiscData.GetKey("patplan","PatPlanNum");
			}
			string command="INSERT INTO patplan (";
			if(PrefC.RandomKeys){
				command+="PatPlanNum,";
			}
			command+="PatNum,PlanNum,Ordinal,IsPending,Relationship,PatID) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(p.PatPlanNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (p.PatNum)+"', "
				+"'"+POut.PInt   (p.PlanNum)+"', "
				+"'"+POut.PInt   (p.Ordinal)+"', "
				+"'"+POut.PBool  (p.IsPending)+"', "
				+"'"+POut.PInt   ((int)p.Relationship)+"', "
				+"'"+POut.PString(p.PatID)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				p.PatPlanNum=Db.NonQ(command,true);
			}
		}

		/*  Do NOT use this.  Use PatPlans.Delete() instead.
		///<summary></summary>
		public void Delete(){
			string command="DELETE FROM patplan WHERE PatPlanNum ="+POut.PInt(PatPlanNum);
			DataConnection dcon=new DataConnection();
			Db.NonQ(command);
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

		///<summary>Sets the ordinal of the specified patPlan.  Rearranges the other patplans for the patient to keep the ordinal sequence contiguous.  Estimates must be recomputed after this.  FormInsPlan currently updates estimates every time it closes.</summary>
		public static void SetOrdinal(int patPlanNum,int newOrdinal){
			string command="SELECT PatNum FROM patplan WHERE PatPlanNum="+POut.PInt(patPlanNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			int patNum=PIn.PInt(table.Rows[0][0].ToString());
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
				command="UPDATE patplan SET Ordinal="+POut.PInt(curOrdinal)
					+" WHERE PatPlanNum="+POut.PInt(patPlans[i].PatPlanNum);
				Db.NonQ(command);
				curOrdinal++;
			}
			command="UPDATE patplan SET Ordinal="+POut.PInt(newOrdinal)
				+" WHERE PatPlanNum="+POut.PInt(patPlanNum);
			Db.NonQ(command);
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

		public static PatPlan[] GetByPlanNum(int planNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PatPlan[]>(MethodBase.GetCurrentMethod(),planNum);
			} 
			string command="SELECT * FROM patplan WHERE PlanNum='"+POut.PInt(planNum)+"'";
			DataTable table=Db.GetTable(command);
			return RefreshAndFill(table).ToArray();
		}

		///<summary>Will return null if none exists.</summary>
		public static PatPlan GetPatPlan(int patNum,int ordinal) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PatPlan>(MethodBase.GetCurrentMethod(),patNum,ordinal);
			} 
			string command="SELECT * FROM patplan WHERE PatNum="+POut.PInt(patNum)
				+" AND Ordinal="+POut.PInt(ordinal);
			DataTable table=Db.GetTable(command);
			List<PatPlan> list=RefreshAndFill(table);
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}
		
		
	}

	

	


}










