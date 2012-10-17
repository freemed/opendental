using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace OpenDental {
	public class ProcedureL {
		public static void SetCompleteInAppt(Appointment apt,List<InsPlan> PlanList,List<PatPlan> patPlans,long siteNum,int patientAge,List<InsSub> subList) {
			List<Procedure> procsInAppt=Procedures.GetProcsForSingle(apt.AptNum,false);
			Procedures.SetCompleteInAppt(apt,PlanList,patPlans,siteNum,patientAge,procsInAppt,subList);
			if(Programs.UsingOrion) {
				OrionProcs.SetCompleteInAppt(procsInAppt);
			}
			//automation
			List<string> procCodes=new List<string>();
			for(int i=0;i<procsInAppt.Count;i++){
				procCodes.Add(ProcedureCodes.GetStringProcCode(procsInAppt[i].CodeNum));
			}
			AutomationL.Trigger(AutomationTrigger.CompleteProcedure,procCodes,apt.PatNum);
		}

		///<summary>Returns empty string if no duplicates, otherwise returns duplicate procedure information.  In all places where this is called, we are guaranteed to have the eCW bridge turned on.  So this is an eCW peculiarity rather than an HL7 restriction.  Other HL7 interfaces will not be checking for duplicate procedures unless we intentionally add that as a feature later.</summary>
		public static string ProcsContainDuplicates(List<Procedure> procs) {
			string info="";
			List<Procedure> procsChecked=new List<Procedure>();
			for(int i=0;i<procs.Count;i++) {
				Procedure proc=procs[i];
				ProcedureCode procCode=ProcedureCodes.GetProcCode(procs[i].CodeNum);
				string procCodeStr=procCode.ProcCode;
				if(procCodeStr.Length>5 && procCodeStr.StartsWith("D")) {
					procCodeStr=procCodeStr.Substring(0,5);
				}
				for(int j=0;j<procsChecked.Count;j++) {
					Procedure procDup=procsChecked[j];
					ProcedureCode procCodeDup=ProcedureCodes.GetProcCode(procsChecked[j].CodeNum);
					string procCodeDupStr=procCodeDup.ProcCode;
					if(procCodeDupStr.Length>5 && procCodeDupStr.StartsWith("D")) {
						procCodeDupStr=procCodeDupStr.Substring(0,5);
					}
					if(procCodeDupStr!=procCodeStr) {
						continue;
					}
					if(procDup.ToothNum!=proc.ToothNum) {
						continue;
					}
					if(procDup.ToothRange!=proc.ToothRange) {
						continue;
					}
					if(procDup.ProcFee!=proc.ProcFee) {
						continue;
					}
					if(procDup.Surf!=proc.Surf) {
						continue;
					}
					if(info!="") {
						info+=", ";
					}
					info+=procCodeDupStr;
				}
				procsChecked.Add(proc);
			}
			if(info!="") {
				info=Lan.g("ProcedureL","Duplicate procedures")+": "+info;
			}
			return info;
		}

	}
}
