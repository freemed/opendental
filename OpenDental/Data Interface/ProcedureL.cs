using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ProcedureL{
		///<summary>Loops through each proc. Does not add notes to a procedure that already has notes. Used twice, security checked in both places before calling this.  Also sets provider for each proc.</summary>
		public static void SetCompleteInAppt(Appointment apt,List<InsPlan> PlanList,List<PatPlan> patPlans,int siteNum) {
			List<Procedure> ProcList=Procedures.Refresh(apt.PatNum);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(apt.PatNum);
			List <Benefit> benefitList=Benefits.Refresh(patPlans);
			//this query could be improved slightly to only get notes of interest.
			string command="SELECT * FROM procnote WHERE PatNum="+POut.PInt(apt.PatNum)+" ORDER BY EntryDateTime";
			DataTable rawNotes=Db.GetTable(command);
			//CovPats.Refresh(PlanList,patPlans);
			//bool doResetRecallStatus=false;
			ProcedureCode procCode;
			Procedure oldProc;
			//int siteNum=0;
			//if(!PrefC.GetBool("EasyHidePublicHealth")){
			//	siteNum=Patients.GetPat(apt.PatNum).SiteNum;
			//}
			for(int i=0;i<ProcList.Count;i++) {
				if(ProcList[i].AptNum!=apt.AptNum) {
					continue;
				}
				//attach the note, if it exists.
				for(int n=rawNotes.Rows.Count-1;n>=0;n--) {//loop through each note, backwards.
					if(ProcList[i].ProcNum.ToString()!=rawNotes.Rows[n]["ProcNum"].ToString()) {
						continue;
					}
					ProcList[i].UserNum=PIn.PInt(rawNotes.Rows[n]["UserNum"].ToString());
					ProcList[i].Note=PIn.PString(rawNotes.Rows[n]["Note"].ToString());
					ProcList[i].SigIsTopaz=PIn.PBool(rawNotes.Rows[n]["SigIsTopaz"].ToString());
					ProcList[i].Signature=PIn.PString(rawNotes.Rows[n]["Signature"].ToString());
					break;//out of note loop.
				}
				oldProc=ProcList[i].Copy();
				procCode=ProcedureCodes.GetProcCode(ProcList[i].CodeNum);
				if(procCode.PaintType==ToothPaintingType.Extraction) {//if an extraction, then mark previous procs hidden
					//SetHideGraphical(ProcList[i]);//might not matter anymore
					ToothInitials.SetValue(apt.PatNum,ProcList[i].ToothNum,ToothInitialType.Missing);
				}
				ProcList[i].ProcStatus=ProcStat.C;
				ProcList[i].ProcDate=apt.AptDateTime.Date;
				if(oldProc.ProcStatus!=ProcStat.C) {
					ProcList[i].DateEntryC=DateTime.Now;//this triggers it to set to server time NOW().
				}
				ProcList[i].PlaceService=(PlaceOfService)PrefC.GetInt("DefaultProcedurePlaceService");
				ProcList[i].ClinicNum=apt.ClinicNum;
				ProcList[i].SiteNum=siteNum;
				ProcList[i].PlaceService=Clinics.GetPlaceService(apt.ClinicNum);
				if(apt.ProvHyg!=0) {//if the appointment has a hygiene provider
					if(procCode.IsHygiene) {//hyg proc
						ProcList[i].ProvNum=apt.ProvHyg;
					} else {//regular proc
						ProcList[i].ProvNum=apt.ProvNum;
					}
				} else {//same provider for every procedure
					ProcList[i].ProvNum=apt.ProvNum;
				}
				//if procedure was already complete, then don't add more notes.
				if(oldProc.ProcStatus!=ProcStat.C) {
					ProcList[i].Note+=ProcCodeNotes.GetNote(ProcList[i].ProvNum,ProcList[i].CodeNum);
				}
				Procedures.Update(ProcList[i],oldProc);
				Procedures.ComputeEstimates(ProcList[i],apt.PatNum,ClaimProcList,false,PlanList,patPlans,benefitList);
			}
			//if(doResetRecallStatus){
			//	Recalls.Reset(apt.PatNum);//this also synchs recall
			//}
			Recalls.Synch(apt.PatNum);
			Patient pt=Patients.GetPat(apt.PatNum);
			Reporting.Allocators.AllocatorCollection.CallAll_Allocators(pt.Guarantor);
		}

		

	}

}