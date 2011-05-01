using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormApptProcs:Form {
		//public int PatNum;
		private List<Procedure> ProcList;
		///<summary>If form closes with OK, this contains selected proc num.</summary>
		public List<long> SelectedProcNums;
		///<summary>It's OK if AptCur is not completely up-to-date.  We are going to use PatNum, isPlanned, AptDateTime, AptStatus, and AptNum.</summary>
		public Appointment AptCur;

		///<summary>Not currently used.  Might use again some day as a sub window of FormApptEdit.  Specify AptCur before opening this form.</summary>
		public FormApptProcs() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormApptProcs_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			List<Procedure> entireList=Procedures.Refresh(AptCur.PatNum);
			ProcList=new List<Procedure>();
			bool isPlanned=AptCur.AptStatus==ApptStatus.Planned;
			ApptStatus apptStatus=AptCur.AptStatus;
			for(int i=0;i<entireList.Count;i++){
				//We want all unattached completed procs with same date as appt.
				//but only if one of these types
				if(apptStatus==ApptStatus.Scheduled || apptStatus==ApptStatus.Complete || apptStatus==ApptStatus.ASAP || apptStatus==ApptStatus.Broken){
					if(entireList[i].AptNum==0
						&& entireList[i].ProcStatus==ProcStat.C
						&& entireList[i].ProcDate.Date==AptCur.AptDateTime.Date)
					{
						ProcList.Add(entireList[i]);
					}
				}
				//otherwise, we only want TP procs that are not attached to this appointment.
				//As for TP procs attached to other appointments, we will show this to the user and warn them about it,
				//but we won't filter them out.
				if(entireList[i].ProcStatus!=ProcStat.TP){
					continue;
				}
				if(isPlanned){
					if(entireList[i].PlannedAptNum==AptCur.AptNum){
						continue;
					}
				}
				else{
					if(entireList[i].AptNum==AptCur.AptNum){
						continue;
					}
				}
				ProcList.Add(entireList[i]);
			}
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g("TableProcSelect","OtherAppt"),70,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcSelect","Code"),55);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcSelect","Priority"),55);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcSelect","Tooth"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcSelect","Description"),250);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcSelect","Fee"),60,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ProcList.Count;i++){
				row=new ODGridRow();
				if(ProcList[i].ProcStatus==ProcStat.C){//so unattached
					row.Cells.Add("");
				}
				else if(isPlanned && ProcList[i].PlannedAptNum!=0){
					row.Cells.Add("X");
				}
				else if(!isPlanned && ProcList[i].AptNum!=0){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
				row.Cells.Add(ProcedureCodes.GetStringProcCode(ProcList[i].CodeNum));
				row.Cells.Add(DefC.GetName(DefCat.TxPriorities,ProcList[i].Priority));
				row.Cells.Add(Tooth.ToInternat(ProcList[i].ToothNum));
				row.Cells.Add(ProcedureCodes.GetLaymanTerm(ProcList[i].CodeNum));
				row.Cells.Add(ProcList[i].ProcFee.ToString("F"));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//SelectedProcNums=new List<int>();
			//SelectedProcNums.Add(ProcList[e.Row].ProcNum);
			//DialogResult=DialogResult.OK;
			//Maybe edit proc?
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormProcCodes FormP=new FormProcCodes();
			FormP.IsSelectionMode=true;
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
			Procedure ProcCur;
			ProcCur=new Procedure();//going to be an insert, so no need to set Procedures.CurOld
			ProcCur.CodeNum = FormP.SelectedCodeNum;
			//procnum
			ProcCur.PatNum=AptCur.PatNum;
			//aptnum
			//proccode
			//ProcCur.CodeNum=ProcedureCodes.GetProcCode(ProcCur.OldCode).CodeNum;//already set
			ProcCur.ProcDate=DateTime.Today;
			ProcCur.DateTP=ProcCur.ProcDate;
			//int totUnits = ProcCur.BaseUnits + ProcCur.UnitQty;
			InsPlan priplan=null;
			InsSub prisub=null;
			Family fam=Patients.GetFamily(AptCur.PatNum);
			Patient pat=fam.GetPatient(AptCur.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> patPlanList=PatPlans.Refresh(pat.PatNum);
			if(patPlanList.Count>0) {
				prisub=InsSubs.GetSub(patPlanList[0].InsSubNum,subList);
				priplan=InsPlans.GetPlan(prisub.PlanNum,planList);
			}
			double insfee=Fees.GetAmount0(ProcCur.CodeNum,Fees.GetFeeSched(pat,planList,patPlanList,subList));
			if(priplan!=null && priplan.PlanType=="p") {//PPO
				double standardfee=Fees.GetAmount0(ProcCur.CodeNum,Providers.GetProv(Patients.GetProvNum(pat)).FeeSched);
				if(standardfee>insfee) {
					ProcCur.ProcFee=standardfee;
				}
				else {
					ProcCur.ProcFee=insfee;
				}
			}
			else {
				ProcCur.ProcFee=insfee;
			}
			//surf
			//ToothNum
			//Procedures.Cur.ToothRange
			//ProcCur.NoBillIns=ProcedureCodes.GetProcCode(ProcCur.ProcCode).NoBillIns;
			ProcCur.Priority=0;
			ProcCur.ProcStatus=ProcStat.TP;
			if(ProcedureCodes.GetProcCode(ProcCur.CodeNum).IsHygiene
				&& pat.SecProv != 0){
				ProcCur.ProvNum=pat.SecProv;
			}
			else{
				ProcCur.ProvNum=pat.PriProv;
			}
			ProcCur.Note="";
			ProcCur.ClinicNum=pat.ClinicNum;
			//dx
			//nextaptnum
			ProcCur.DateEntryC=DateTime.Now;
			ProcCur.MedicalCode=ProcedureCodes.GetProcCode(ProcCur.CodeNum).MedicalCode;
			ProcCur.BaseUnits=ProcedureCodes.GetProcCode(ProcCur.CodeNum).BaseUnits;
			ProcCur.SiteNum=pat.SiteNum;
			Procedures.Insert(ProcCur);
			List <Benefit> benefitList=Benefits.Refresh(patPlanList,subList);
			Procedures.ComputeEstimates(ProcCur,pat.PatNum,new List<ClaimProc>(),true,planList,patPlanList,benefitList,pat.Age,subList);
			FormProcEdit FormPE=new FormProcEdit(ProcCur,pat.Copy(),fam);
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.Cancel){
				//any created claimprocs are automatically deleted from within procEdit window.
				try{
					Procedures.Delete(ProcCur.ProcNum);//also deletes the claimprocs
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
				}
			}
			else if(Programs.UsingOrion){
				//No need to synch with Orion mode.
			}
			else{
				//Default is set to TP, so Synch is usually not needed.
				if(ProcCur.ProcStatus==ProcStat.C||ProcCur.ProcStatus==ProcStat.EC||ProcCur.ProcStatus==ProcStat.EO){
					Recalls.Synch(pat.PatNum);
				}
			}
			FillGrid();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(gridMain.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select items first.");
				return;
			}
			bool isAttachedToOtherApt=false;
			bool isPlanned=AptCur.AptStatus==ApptStatus.Planned;
			SelectedProcNums=new List<long>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				SelectedProcNums.Add(ProcList[gridMain.SelectedIndices[i]].ProcNum);
				if(isPlanned && ProcList[gridMain.SelectedIndices[i]].PlannedAptNum!=0){
					isAttachedToOtherApt=true;
				}
				if(!isPlanned && ProcList[gridMain.SelectedIndices[i]].AptNum!=0){
					isAttachedToOtherApt=true;
				}
			}
			if(isAttachedToOtherApt){
				if(!MsgBox.Show(this,true,"One or more of the procedures is already attached to another appointment.  Attach to this appointment instead?")){
					SelectedProcNums=new List<long>();
					return;
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}