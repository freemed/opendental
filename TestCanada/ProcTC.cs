using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace TestCanada {
	public class ProcTC {
		///<summary>The tooth number passed in should be in international format.</summary>
		public static void SetExtracted(string toothNumInternat, DateTime procDate,long patNum) {
			Procedure proc=new Procedure();
			proc.CodeNum=ProcedureCodes.GetCodeNum("71101");
			proc.PatNum=patNum;
			proc.ProcDate=procDate;
			proc.ToothNum=Tooth.FromInternat(toothNumInternat);
			proc.ProcStatus=ProcStat.EO;
			Procedures.Insert(proc);
			ToothInitialTC.SetMissing(toothNumInternat,patNum);
		}

		///<summary>Procedure will have a completed status.  For surfaces, since the scripts are faulty, pass in the exact surfaces that should be in the db, no validation will be done, and those exact same surfaces are what will go out on claim.</summary>
		public static Procedure AddProc(string procCode,long patNum,DateTime procDate,string toothNum,string surf,double fee,string typeCodes,long provNum) {
			Procedure proc=new Procedure();
			//procnum
			proc.PatNum=patNum;
			//aptnum
			proc.CodeNum=ProcedureCodes.GetCodeNum(procCode);
			proc.ProcDate=procDate;
			proc.DateTP=proc.ProcDate;
			proc.ProcFee=fee;
			if(toothNum=="") {
				proc.ToothNum="";
			}
			else {
				proc.ToothNum=Tooth.FromInternat(toothNum);
			}
			proc.Surf=surf;//Tooth.SurfTidyFromDisplayToDb(surf,proc.ToothNum);
			//ToothRange
			proc.Priority=0;
			proc.ProcStatus=ProcStat.C;
			proc.ProvNum=provNum;
			proc.Note="";
			proc.ClinicNum=0;
			//proc.Dx
			proc.MedicalCode="";
			proc.BaseUnits=0;
			proc.SiteNum=0;
			//nextaptnum
			proc.CanadianTypeCodes=typeCodes;
			Procedures.Insert(proc);
			//if an extraction, then mark previous procs hidden.  Skip.
			//Recalls.Synch(PatCur.PatNum);//skip
			//Procedures.ComputeEstimates(proc,patNum,new List<ClaimProc>(),true,planList,patPlanList,benefitList,age);
			return proc;
		}

		public static void AttachLabProc(long procNum,Procedure procLab) {
			Procedure oldProc=procLab.Copy();
			procLab.ProcNumLab=procNum;
			Procedures.Update(procLab,oldProc);
		}

	}
}
