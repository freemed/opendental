using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormProcEditExplain:Form {
		public static string Changes;
		public static string Explanation;

		public FormProcEditExplain() {
			InitializeComponent();
			Lan.F(this);
			textSummary.Text=Changes;
		}

		private void FormProcEditExplain_Load(object sender,EventArgs e) {
			textSummary.Text=Changes;
		}

		public static string GetChanges(Procedure procCur, Procedure procOld, OrionProc orionProcCur, OrionProc orionProcOld){
			Changes="";
			if(procOld.PatNum != procCur.PatNum) {
				if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Patient Num changed from "+POut.Long(procOld.PatNum)+" to "+POut.Long(procCur.PatNum)+".";
		  }
		  if(procOld.AptNum != procCur.AptNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Apt Num changed from "+POut.Long(procOld.AptNum)+" to "+POut.Long(procCur.AptNum)+".";
		  }
		  if(procOld.PlannedAptNum != procCur.PlannedAptNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Planned Apt Num changed from "+POut.Long(procOld.PlannedAptNum)+" to "+POut.Long(procCur.PlannedAptNum)+".";
		  }
		  if(procOld.DateEntryC != procCur.DateEntryC) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Date Entry changed from "+POut.Date(procOld.DateEntryC)+" to "+POut.Date(procCur.DateEntryC)+".";
		  }
		  if(procOld.ProcDate != procCur.ProcDate) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Proc Date changed from "+POut.Date(procOld.ProcDate)+" to "+POut.Date(procCur.ProcDate)+".";
		  }
		  if(procOld.StartTime != procCur.StartTime) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Start Time changed from "+POut.Int(procOld.StartTime)+" to "+POut.Int(procCur.StartTime)+".";
		  }
		  if(procOld.StopTime != procCur.StopTime) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Stop Time changed from "+POut.Int(procOld.StopTime)+" to "+POut.Int(procCur.StopTime)+".";
		  }
		  if(procOld.ProcTime != procCur.ProcTime) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Procedure Time changed from "+POut.TimeSpan(procOld.ProcTime)+" to "+POut.TimeSpan(procCur.ProcTime)+".";
		  }
		  if(procOld.ProcTimeEnd != procCur.ProcTimeEnd) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="End Time changed from "+POut.TimeSpan(procOld.ProcTimeEnd)+" to "+POut.TimeSpan(procCur.ProcTimeEnd)+".";
		  }
		  if(procOld.CodeNum != procCur.CodeNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Procedure changed from "+ProcedureCodes.GetLaymanTerm(procOld.CodeNum)+" to "+ProcedureCodes.GetLaymanTerm(procCur.CodeNum)+".";
		  }
		  if(procOld.ProcFee != procCur.ProcFee) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Proc Fee changed from $"+POut.Double(procOld.ProcFee)+" to $"+POut.Double(procCur.ProcFee)+".";
		  }
		  if(procOld.ToothNum != procCur.ToothNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Tooth Num changed from "+POut.String(procOld.ToothNum)+" to "+POut.String(procCur.ToothNum)+".";
		  }
		  if(procOld.Surf != procCur.Surf) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Surface changed from "+POut.String(procOld.Surf)+" to "+POut.String(procCur.Surf)+".";
		  }
		  if(procOld.ToothRange != procCur.ToothRange) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Tooth Range changed from "+POut.String(procOld.ToothRange)+" to "+POut.String(procCur.ToothRange)+".";
		  }
			if(procOld.HideGraphics != procCur.HideGraphics) {
				if(Changes!=""){ Changes+="\r\n";}
				Changes="Hide Graphics changed from "+(procOld.HideGraphics?"Hide Graphics":"Do Not Hide Graphics")
					+" to "+(procCur.HideGraphics?"Hide Graphics":"Do Not Hide Graphics")+".";
			}
		  if(procOld.ProvNum != procCur.ProvNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Provider changed from "+Providers.GetAbbr(procOld.ProvNum)+" to "+Providers.GetAbbr(procCur.ProvNum)+".";
		  }
		  if(procOld.Dx != procCur.Dx) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Diagnosis changed from "+DefC.GetDef(DefCat.Diagnosis,procOld.Dx).ItemName.ToString()
					+" to "+DefC.GetDef(DefCat.Diagnosis,procCur.Dx).ItemName.ToString()+".";
		  }
		  if(procOld.Priority != procCur.Priority) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Priority changed from "+((procOld.Priority!=0)?DefC.GetDef(DefCat.TxPriorities,procOld.Priority).ItemName:"no priority")
					+" to "+((procCur.Priority!=0)?DefC.GetDef(DefCat.TxPriorities,procCur.Priority).ItemName:"no priority")+".";
		  }
		  if(procOld.PlaceService != procCur.PlaceService) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Place Service changed from "+procOld.PlaceService.ToString()+" to "+procCur.PlaceService.ToString()+".";
		  }
		  if(procOld.ClinicNum != procCur.ClinicNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Clinic changed from "+Clinics.GetDesc(procOld.ClinicNum)+" to "+Clinics.GetDesc(procCur.ClinicNum)+".";
		  }
		  if(procOld.SiteNum != procCur.SiteNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Site changed from "+Sites.GetDescription(procOld.SiteNum)+" to "+Sites.GetDescription(procCur.SiteNum)+".";
		  }
		  if(procOld.Prosthesis != procCur.Prosthesis) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Prosthesis changed from "+procOld.Prosthesis.ToString()+" to "+procCur.Prosthesis.ToString()+".";
		  }
		  if(procOld.DateOriginalProsth != procCur.DateOriginalProsth) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Date of Original Prosthesis changed from "+POut.Date(procOld.DateOriginalProsth)+" to "+POut.Date(procCur.DateOriginalProsth)+".";
		  }
		  if(procOld.ClaimNote != procCur.ClaimNote) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Claim Note changed from "+POut.String(procOld.ClaimNote)+" to "+POut.String(procCur.ClaimNote)+".";
		  }
			if(orionProcOld.OrionProcNum != orionProcCur.OrionProcNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Orion Proc Num changed from "+POut.Long(orionProcOld.OrionProcNum)+" to "+POut.Long(orionProcCur.OrionProcNum)+".";
		  }
			if(orionProcOld.ProcNum != orionProcCur.ProcNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Proc Num changed from "+POut.Long(orionProcOld.ProcNum)+" to "+POut.Long(orionProcCur.ProcNum)+".";
		  }
			if(orionProcOld.DPC != orionProcCur.DPC) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="DPC changed from "+POut.String(orionProcOld.DPC.ToString())+" to "+POut.String(orionProcCur.DPC.ToString())+".";
		  }
			if(orionProcOld.Status2 != orionProcCur.Status2) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Status 2 changed from "+orionProcOld.Status2.ToString()+" to "+orionProcCur.Status2.ToString()+".";
		  }
			if(orionProcOld.DateScheduleBy != orionProcCur.DateScheduleBy) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Date Schedule By changed from "+POut.Date(orionProcOld.DateScheduleBy)+" to "+POut.Date(orionProcCur.DateScheduleBy)+".";
		  }
			if(orionProcOld.DateStopClock != orionProcCur.DateStopClock) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Date Stop Clock changed from "+POut.Date(orionProcOld.DateStopClock)+" to "+POut.Date(orionProcCur.DateStopClock)+".";
		  }
			if(orionProcOld.IsOnCall != orionProcCur.IsOnCall) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Is On Call changed from "+(orionProcOld.IsOnCall?"Is On Call":"Is Not On Call")
					+" to "+(orionProcCur.IsOnCall?"Is On Call":"Is Not On Call")+".";
		  }
			if(orionProcOld.IsEffectiveComm != orionProcCur.IsEffectiveComm) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Is Effective Comm changed from "+(orionProcOld.IsEffectiveComm?"Is an Effective Communicator":"Is Not an Effective Communicator")
					+" to "+(orionProcCur.IsEffectiveComm?"Is an Effective Communicator":"Is Not an Effective Communicator")+".";
		  }
			if(orionProcOld.IsRepair != orionProcCur.IsRepair) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Is Repair changed from "+(orionProcOld.IsRepair?"Is a Repair":"Is Not a Repair")
					+" to "+(orionProcCur.IsRepair?"Is a Repair":"Is Not a Repair")+".";
		  }
		  if(procOld.MedicalCode != procCur.MedicalCode) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Medical Code changed from "+POut.String(procOld.MedicalCode)+" to "+POut.String(procCur.MedicalCode)+".";
		  }
		  if(procOld.DiagnosticCode != procCur.DiagnosticCode) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Diagnostic Code changed from "+procOld.DiagnosticCode.ToString()+" to "+procCur.DiagnosticCode.ToString()+".";
		  }
		  if(procOld.IsPrincDiag != procCur.IsPrincDiag) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Is Princ Diag changed from "+(procOld.IsPrincDiag?"Principal Diagnosis":"Not Principal Diagnosis")
					+" to "+(procCur.IsPrincDiag?"Principal Diagnosis":"Not Principal Diagnosis")+".";
		  }
		  if(procOld.ProcNumLab != procCur.ProcNumLab) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Proc Num Lab changed from "+POut.Long(procOld.ProcNumLab)+" to "+POut.Long(procCur.ProcNumLab)+".";
		  }
		  if(procOld.BillingTypeOne != procCur.BillingTypeOne) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Billing Type One changed from "+(procOld.BillingTypeOne!=0?DefC.GetDef(DefCat.BillingTypes,procOld.BillingTypeOne).ItemName:"none")
					+" to "+(procCur.BillingTypeOne!=0?DefC.GetDef(DefCat.BillingTypes,procCur.BillingTypeOne).ItemName:"none")+".";
		  }
		  if(procOld.BillingTypeTwo != procCur.BillingTypeTwo) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Billing Type Two changed from "+(procOld.BillingTypeTwo!=0?DefC.GetDef(DefCat.BillingTypes,procOld.BillingTypeTwo).ItemName:"none")
					+" to "+(procCur.BillingTypeTwo!=0?DefC.GetDef(DefCat.BillingTypes,procCur.BillingTypeTwo).ItemName:"none")+".";
		  }
		  if(procOld.ProcStatus != procCur.ProcStatus) {
		    if(Changes!=""){ Changes+="\r\n";}
				string procStatOld=Enum.GetName(typeof(ProcStat),procOld.ProcStatus);
				string procStatCur=Enum.GetName(typeof(ProcStat),procOld.ProcStatus);
		    Changes+="Proc Status changed from "+procStatOld+" to "+procStatCur+".";
		  }
		  if(procOld.RevCode != procCur.RevCode) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Rev Code changed from "+POut.String(procOld.RevCode)+"' to '"+POut.String(procCur.RevCode)+".";
		  }
		  if(procOld.UnitCode != procCur.UnitCode) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Unit Code changed from "+POut.String(procOld.UnitCode)+" to "+POut.String(procCur.UnitCode)+".";
		  }
		  if(procOld.UnitQty != procCur.UnitQty) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Unit Quantity changed from "+POut.Int(procOld.UnitQty)+" to "+POut.Int(procCur.UnitQty)+".";
		  }
		  if(procOld.DateTP != procCur.DateTP) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Date TP changed from "+POut.Date(procOld.DateTP)+" to "+POut.Date(procCur.DateTP)+".";
		  }
		  if(procOld.CanadianTypeCodes != procCur.CanadianTypeCodes) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Canadian Code Type changed from "+POut.String(procOld.CanadianTypeCodes)+" to "+POut.String(procCur.CanadianTypeCodes)+".";
		  }
			if(procOld.Note != procCur.Note) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Note changed from "+((procOld.Note=="")?"none":"'"+procOld.Note+"'")
					+" to "+((procCur.Note=="")?"none":"'"+procCur.Note+"'");
		  }
			return Changes;
		}

		private void butOK_Click(object sender,EventArgs e) {
			Explanation="Values Changed:\r\n"+Changes+"\r\nExplanation:\r\n"+textExplanation.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	






	}
}