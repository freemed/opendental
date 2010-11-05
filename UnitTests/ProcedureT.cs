using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class ProcedureT {
		/// <summary>Returns the procNum</summary>
		public static Procedure CreateProcedure(Patient pat,string procCodeStr,ProcStat procStatus,string toothNum,double procFee){
			Procedure proc=new Procedure();
			proc.CodeNum=ProcedureCodes.GetCodeNum(procCodeStr);
			proc.PatNum=pat.PatNum;
			proc.ProcDate=DateTime.Today;
			proc.ProcStatus=procStatus;
			proc.ProvNum=pat.PriProv;
			proc.ProcFee=procFee;
			proc.ToothNum=toothNum;
			proc.Prosthesis="I";
			Procedures.Insert(proc);
			return proc;
		}

		/*public static void SetToothNum(Procedure procedure,string toothNum){
			Procedure oldProcedure=procedure.Copy();
			procedure.ToothNum=toothNum;
			Procedures.Update(procedure,oldProcedure);
		}*/

		public static void SetPriority(Procedure procedure,int priority){
			Procedure oldProcedure=procedure.Copy();
			procedure.Priority=DefC.Short[(int)DefCat.TxPriorities][priority].DefNum;
			Procedures.Update(procedure,oldProcedure);
		}

		public static void SetComplete(Procedure proc,Patient pat,List<InsPlan> planList,List<PatPlan> patPlanList,List<ClaimProc> claimProcList,List<Benefit> benefitList,List<InsSub> subList) {
			Procedure procOld=proc.Copy();
			ProcedureCode procCode=ProcedureCodes.GetProcCode(proc.CodeNum);
			proc.DateEntryC=DateTime.Now;
			proc.ProcStatus=ProcStat.C;
			Procedures.Update(proc,procOld);
			Procedures.ComputeEstimates(proc,proc.PatNum,claimProcList,false,planList,patPlanList,benefitList,pat.Age,subList);

		}



	}
}
