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
		private Procedure ProcCur;
		private Procedure ProcOld;
		public static string Changes;


		public FormProcEditExplain() {
			InitializeComponent();
			Lan.F(this);
		}


		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		public static string GetChanges(Procedure procNew, Procedure procOld){
			if(procOld.PatNum != procNew.PatNum) {
		    Changes+="Patient Num: "+POut.Long(procOld.PatNum)+" => "+POut.Long(procNew.PatNum)+"";
		  }
		  if(procOld.AptNum != procNew.AptNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Apt Num: "+POut.Long(procOld.AptNum)+" => "+POut.Long(procNew.AptNum)+"";
		  }
		  if(procOld.ProcDate != procNew.ProcDate) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Proc Date: "+POut.Date(procOld.ProcDate)+" => "+POut.Date(procNew.ProcDate)+"";
		  }
		  if(procOld.ProcFee != procNew.ProcFee) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Proc Fee: '"+POut.Double(procOld.ProcFee)+"' => '"+POut.Double(procNew.ProcFee)+"'";
		  }
		  if(procOld.Surf != procNew.Surf) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Surface: '"+POut.String(procOld.Surf)+"' => '"+POut.String(procNew.Surf)+"'";
		  }
		  if(procOld.ToothNum != procNew.ToothNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Tooth Num: '"+POut.String(procOld.ToothNum)+"' => '"+POut.String(procNew.ToothNum)+"'";
		  }
		  if(procOld.ToothRange != procNew.ToothRange) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Tooth Range: '"+POut.String(procOld.ToothRange)+"' => '"+POut.String(procNew.ToothRange)+"'";
		  }
		  if(procOld.Priority != procNew.Priority) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Priority: "+POut.Long(procOld.Priority)+" => "+POut.Long(procNew.Priority)+"";
		  }
		  if(procOld.ProcStatus != procNew.ProcStatus) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Proc Status: "+POut.Int((int)procOld.ProcStatus)+" => "+POut.Int((int)procNew.ProcStatus)+"";
		  }
		  if(procOld.ProvNum != procNew.ProvNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Prov Num: "+POut.Long(procOld.ProvNum)+" => "+POut.Long(procNew.ProvNum)+"";
		  }
		  if(procOld.Dx != procNew.Dx) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Dx: "+POut.Long(procOld.Dx)+" => "+POut.Long(procNew.Dx)+"";
		  }
		  if(procOld.PlannedAptNum != procNew.PlannedAptNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Planned AptNum: "+POut.Long(procOld.PlannedAptNum)+" => "+POut.Long(procNew.PlannedAptNum)+"";
		  }
		  if(procOld.PlaceService != procNew.PlaceService) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Place Service: "+POut.Int((int)procOld.PlaceService)+" => "+POut.Int((int)procNew.PlaceService)+"";
		  }
		  if(procOld.Prosthesis != procNew.Prosthesis) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Prosthesis: '"+POut.String(procOld.Prosthesis)+"' => '"+POut.String(procNew.Prosthesis)+"'";
		  }
		  if(procOld.DateOriginalProsth != procNew.DateOriginalProsth) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Date Original Prosth: "+POut.Date(procOld.DateOriginalProsth)+" => "+POut.Date(procNew.DateOriginalProsth)+"";
		  }
		  if(procOld.ClaimNote != procNew.ClaimNote) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Claim Note: '"+POut.String(procOld.ClaimNote)+"' => '"+POut.String(procNew.ClaimNote)+"'";
		  }
		  if(procOld.DateEntryC != procNew.DateEntryC) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Date Entry: "+POut.Date(procOld.DateEntryC)+" => "+POut.Date(procNew.DateEntryC)+"";
		  }
		  if(procOld.ClinicNum != procNew.ClinicNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Clinic Num: "+POut.Long(procOld.ClinicNum)+" => "+POut.Long(procNew.ClinicNum)+"";
		  }
		  if(procOld.MedicalCode != procNew.MedicalCode) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Medical Code: '"+POut.String(procOld.MedicalCode)+"' => '"+POut.String(procNew.MedicalCode)+"'";
		  }
		  if(procOld.DiagnosticCode != procNew.DiagnosticCode) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Diagnostic Code: '"+POut.String(procOld.DiagnosticCode)+"' => '"+POut.String(procNew.DiagnosticCode)+"'";
		  }
		  if(procOld.IsPrincDiag != procNew.IsPrincDiag) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Is Princ Diag: "+POut.Bool(procOld.IsPrincDiag)+" => "+POut.Bool(procNew.IsPrincDiag)+"";
		  }
		  if(procOld.ProcNumLab != procNew.ProcNumLab) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Proc Num Lab: "+POut.Long(procOld.ProcNumLab)+" => "+POut.Long(procNew.ProcNumLab)+"";
		  }
		  if(procOld.BillingTypeOne != procNew.BillingTypeOne) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Billing Type One: "+POut.Long(procOld.BillingTypeOne)+" => "+POut.Long(procNew.BillingTypeOne)+"";
		  }
		  if(procOld.BillingTypeTwo != procNew.BillingTypeTwo) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Billing Type Two: "+POut.Long(procOld.BillingTypeTwo)+" => "+POut.Long(procNew.BillingTypeTwo)+"";
		  }
		  if(procOld.CodeNum != procNew.CodeNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Code Num: "+POut.Long(procOld.CodeNum)+" => "+POut.Long(procNew.CodeNum)+"";
		  }
		  if(procOld.RevCode != procNew.RevCode) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Rev Code: '"+POut.String(procOld.RevCode)+"' => '"+POut.String(procNew.RevCode)+"'";
		  }
		  if(procOld.UnitCode != procNew.UnitCode) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Unit Code: '"+POut.String(procOld.UnitCode)+"' => '"+POut.String(procNew.UnitCode)+"'";
		  }
		  if(procOld.UnitQty != procNew.UnitQty) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Unit Quantity: "+POut.Int(procOld.UnitQty)+" => "+POut.Int(procNew.UnitQty)+"";
		  }
		  if(procOld.StartTime != procNew.StartTime) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Start Time: "+POut.Int(procOld.StartTime)+" => "+POut.Int(procNew.StartTime)+"";
		  }
		  if(procOld.StopTime != procNew.StopTime) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Stop Time: "+POut.Int(procOld.StopTime)+" => "+POut.Int(procNew.StopTime)+"";
		  }
		  if(procOld.DateTP != procNew.DateTP) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Date TP: "+POut.Date(procOld.DateTP)+" => "+POut.Date(procNew.DateTP)+"";
		  }
		  if(procOld.SiteNum != procNew.SiteNum) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Site Num: "+POut.Long(procOld.SiteNum)+" => "+POut.Long(procNew.SiteNum)+"";
		  }
		  if(procOld.CanadianTypeCodes != procNew.CanadianTypeCodes) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Canadian Code Type: '"+POut.String(procOld.CanadianTypeCodes)+"' => '"+POut.String(procNew.CanadianTypeCodes)+"'";
		  }
		  if(procOld.ProcTime != procNew.ProcTime) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Procedure Time: "+POut.TimeSpan(procOld.ProcTime)+" => "+POut.TimeSpan(procNew.ProcTime)+"";
		  }
		  if(procOld.ProcTimeEnd != procNew.ProcTimeEnd) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="End Time: "+POut.TimeSpan(procOld.ProcTimeEnd)+" => "+POut.TimeSpan(procNew.ProcTimeEnd)+"";
		  }
			if(procOld.Note != procNew.Note) {
		    if(Changes!=""){ Changes+="\r\n";}
		    Changes+="Note: '"+POut.String(procOld.Note)+"' => '"+POut.String(procNew.Note)+"'";
		  }
			return Changes;
		}

		private void FormProcEditExplain_Load(object sender,EventArgs e) {
			textSummary.Text=Changes;
		}






	}
}