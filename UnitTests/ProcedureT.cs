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



	}
}
