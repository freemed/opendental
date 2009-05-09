using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormProcEditAll:Form {
		public List<Procedure> ProcList;
		private List<Procedure> ProcOldList;
		//private bool StartedAttachedToClaim;
		private bool AnyAreC;

		public FormProcEditAll() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormProcEditAll_Load(object sender,EventArgs e) {
			ProcOldList=new List<Procedure>();
			for(int i=0;i<ProcList.Count;i++){
				ProcOldList.Add(ProcList[i].Copy());
			}
			AnyAreC=false;
			DateTime oldestDateEntryC=DateTime.Today;
			bool datesAreSame=true;
			for(int i=0;i<ProcList.Count;i++){
				if(ProcList[i].ProcStatus==ProcStat.C){
					AnyAreC=true;
					if(ProcList[i].ProcDate < oldestDateEntryC){
						oldestDateEntryC=ProcList[i].ProcDate;
					}
				}
				if(ProcList[0].ProcDate!=ProcList[i].ProcDate){
					datesAreSame=false;
				}
			}
			if(AnyAreC){
				if(!Security.IsAuthorized(Permissions.ProcComplEdit,oldestDateEntryC)){
					butOK.Enabled=false;
					butEditAnyway.Enabled=false;
				}
			}
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(ProcList[0].PatNum);
			if(Procedures.IsAttachedToClaim(ProcList,ClaimProcList)){
				//StartedAttachedToClaim=true;
				//however, this doesn't stop someone from creating a claim while this window is open,
				//so this is checked at the end, too.
				textDate.Enabled=false;
				butToday.Enabled=false;
				butEditAnyway.Visible=true;
				labelClaim.Visible=true;
			}
			if(datesAreSame){
				textDate.Text=ProcList[0].ProcDate.ToShortDateString();
			}
		}

		private void butToday_Click(object sender,EventArgs e) {
			if(textDate.Enabled){
				textDate.Text=DateTime.Today.ToShortDateString();
			}
		}

		private void butEditAnyway_Click(object sender,EventArgs e) {
			textDate.Enabled=true;
			butToday.Enabled=true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDate.errorProvider1.GetError(textDate)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textDate.Text!=""){
				DateTime procDate=PIn.PDate(textDate.Text);
				Appointment apt;
				for(int i=0;i<ProcList.Count;i++){
					if(ProcList[i].AptNum==0){
						continue;
					}
					apt=Appointments.GetOneApt(ProcList[i].AptNum);
					if(ProcList[i].ProcDate!=procDate){
						if(!MsgBox.Show(this,true,"Date does not match appointment date.  Continue anyway?")){
							return;
						}
						break;
					}
				}
				for(int i=0;i<ProcList.Count;i++){
					ProcList[i].ProcDate=procDate;
					Procedures.Update(ProcList[i],ProcOldList[i]);
				}
				Recalls.Synch(ProcList[0].PatNum);
				if(AnyAreC){
					Patient pat=Patients.GetPat(ProcList[0].PatNum);
					string codes="";
					ProcedureCode ProcedureCode2;
					for(int i=0;i<ProcList.Count;i++){
						if(i>0){
							codes+=", ";
						}
						ProcedureCode2=ProcedureCodes.GetProcCode(ProcList[i].CodeNum);
						codes+=ProcedureCode2.ProcCode;
					}
					SecurityLogs.MakeLogEntry(Permissions.ProcComplEdit,ProcList[0].PatNum,
						pat.GetNameLF()+codes+", New date:"+procDate.ToShortDateString());
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		
	}
}