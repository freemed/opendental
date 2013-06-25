using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Globalization;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormRepeatChargesUpdate : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRepeatChargesUpdate()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRepeatChargesUpdate));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(393,238);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(393,197);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(43,28);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(426,146);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			// 
			// FormRepeatChargesUpdate
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(520,289);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRepeatChargesUpdate";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Update Repeating Charges";
			this.Load += new System.EventHandler(this.FormRepeatChargesUpdate_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRepeatChargesUpdate_Load(object sender, System.EventArgs e) {
		
		}

		private Claim CreateClaim(string claimType,List<PatPlan> patPlanList,List<InsPlan> planList,List<ClaimProc> claimProcList,Procedure proc,List<InsSub> subList) {
			long claimFormNum=0;
			InsPlan planCur=new InsPlan();
			InsSub subCur=new InsSub();
			Relat relatOther=Relat.Self;
			long clinicNum=proc.ClinicNum;
			PlaceOfService placeService=proc.PlaceService;
			switch(claimType) {
				case "P":
					subCur=InsSubs.GetSub(PatPlans.GetInsSubNum(patPlanList,PatPlans.GetOrdinal(PriSecMed.Primary,patPlanList,planList,subList)),subList);
					planCur=InsPlans.GetPlan(subCur.PlanNum,planList);
					break;
				case "S":
					subCur=InsSubs.GetSub(PatPlans.GetInsSubNum(patPlanList,PatPlans.GetOrdinal(PriSecMed.Secondary,patPlanList,planList,subList)),subList);
					planCur=InsPlans.GetPlan(subCur.PlanNum,planList);
					break;
				case "Med":
					//It's already been verified that a med plan exists
					subCur=InsSubs.GetSub(PatPlans.GetInsSubNum(patPlanList,PatPlans.GetOrdinal(PriSecMed.Medical,patPlanList,planList,subList)),subList);
					planCur=InsPlans.GetPlan(subCur.PlanNum,planList);
					break;
			}
			ClaimProc claimProcCur=Procedures.GetClaimProcEstimate(proc.ProcNum,claimProcList,planCur,subCur.InsSubNum);
			if(claimProcCur==null) {
				claimProcCur=new ClaimProc();
				ClaimProcs.CreateEst(claimProcCur,proc,planCur,subCur);
			}
			Claim claimCur=new Claim();
			claimCur.PatNum=proc.PatNum;
			claimCur.DateService=proc.ProcDate;
			claimCur.ClinicNum=proc.ClinicNum;
			claimCur.PlaceService=proc.PlaceService;
			claimCur.ClaimStatus="W";
			claimCur.DateSent=DateTimeOD.Today;
			claimCur.PlanNum=planCur.PlanNum;
			claimCur.InsSubNum=subCur.InsSubNum;
			InsSub sub;
			switch(claimType) {
				case "P":
					claimCur.PatRelat=PatPlans.GetRelat(patPlanList,PatPlans.GetOrdinal(PriSecMed.Primary,patPlanList,planList,subList));
					claimCur.ClaimType="P";
					claimCur.InsSubNum2=PatPlans.GetInsSubNum(patPlanList,PatPlans.GetOrdinal(PriSecMed.Secondary,patPlanList,planList,subList));
					sub=InsSubs.GetSub(claimCur.InsSubNum2,subList);
					if(sub.PlanNum>0 && InsPlans.RefreshOne(sub.PlanNum).IsMedical) {
						claimCur.PlanNum2=0;//no sec ins
						claimCur.PatRelat2=Relat.Self;
					}
					else {
						claimCur.PlanNum2=sub.PlanNum;//might be 0 if no sec ins
						claimCur.PatRelat2=PatPlans.GetRelat(patPlanList,PatPlans.GetOrdinal(PriSecMed.Secondary,patPlanList,planList,subList));
					}
					break;
				case "S":
					claimCur.PatRelat=PatPlans.GetRelat(patPlanList,PatPlans.GetOrdinal(PriSecMed.Secondary,patPlanList,planList,subList));
					claimCur.ClaimType="S";
					claimCur.InsSubNum2=PatPlans.GetInsSubNum(patPlanList,PatPlans.GetOrdinal(PriSecMed.Primary,patPlanList,planList,subList));
					sub=InsSubs.GetSub(claimCur.InsSubNum2,subList);
					claimCur.PlanNum2=sub.PlanNum;
					claimCur.PatRelat2=PatPlans.GetRelat(patPlanList,PatPlans.GetOrdinal(PriSecMed.Primary,patPlanList,planList,subList));
					break;
				case "Med":
					claimCur.PatRelat=PatPlans.GetFromList(patPlanList,subCur.InsSubNum).Relationship;
					claimCur.ClaimType="Other";
					if(PrefC.GetBool(PrefName.ClaimMedTypeIsInstWhenInsPlanIsMedical)){
						claimCur.MedType=EnumClaimMedType.Institutional;
					}
					else{
						claimCur.MedType=EnumClaimMedType.Medical;
					}
					break;
				case "Other":
					claimCur.PatRelat=relatOther;
					claimCur.ClaimType="Other";
					//plannum2 is not automatically filled in.
					claimCur.ClaimForm=claimFormNum;
					if(planCur.IsMedical){
						if(PrefC.GetBool(PrefName.ClaimMedTypeIsInstWhenInsPlanIsMedical)){
							claimCur.MedType=EnumClaimMedType.Institutional;
						}
						else{
							claimCur.MedType=EnumClaimMedType.Medical;
						}
					}
					break;
			}
			if(planCur.PlanType=="c"){//if capitation
				claimCur.ClaimType="Cap";
			}
			claimCur.ProvTreat=proc.ProvNum;
			if(Providers.GetIsSec(proc.ProvNum)) {
				claimCur.ProvTreat=Patients.GetPat(proc.PatNum).PriProv;
				//OK if zero, because auto select first in list when open claim
			}
			claimCur.IsProsthesis="N";
			claimCur.ProvBill=Providers.GetBillingProvNum(claimCur.ProvTreat,claimCur.ClinicNum);//OK if zero, because it will get fixed in claim
			claimCur.EmployRelated=YN.No;
			claimCur.ClaimForm=planCur.ClaimFormNum;
			Claims.Insert(claimCur);
			//attach procedure
			claimProcCur.ClaimNum=claimCur.ClaimNum;
			if(planCur.PlanType=="c") {//if capitation
				claimProcCur.Status=ClaimProcStatus.CapClaim;
			}
			else {
				claimProcCur.Status=ClaimProcStatus.NotReceived;
			}
			if(planCur.UseAltCode && (ProcedureCodes.GetProcCode(proc.CodeNum).AlternateCode1!="")) {
				claimProcCur.CodeSent=ProcedureCodes.GetProcCode(proc.CodeNum).AlternateCode1;
			}
			else if(planCur.IsMedical && proc.MedicalCode!="") {
				claimProcCur.CodeSent=proc.MedicalCode;
			}
			else {
				claimProcCur.CodeSent=ProcedureCodes.GetProcCode(proc.CodeNum).ProcCode;
				if(claimProcCur.CodeSent.Length>5 && claimProcCur.CodeSent.Substring(0,1)=="D") {
					claimProcCur.CodeSent=claimProcCur.CodeSent.Substring(0,5);
				}
				if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
					if(claimProcCur.CodeSent.Length>5) {//In Canadian e-claims, codes can contain letters or numbers and cannot be longer than 5 characters.
						claimProcCur.CodeSent=claimProcCur.CodeSent.Substring(0,5);
					}
				}
			}
			claimProcCur.LineNumber=(byte)1;
			ClaimProcs.Update(claimProcCur);
			return claimCur;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			RepeatCharge[] chargeList=RepeatCharges.Refresh(0);//Gets all repeating charges for all patients, they may be disabled
			int countAdded=0;
			int claimsAdded=0;
			DateTime startDate;
			Procedure proc;
			for(int i=0;i<chargeList.Length;i++){
				if(!chargeList[i].IsEnabled) {//first make sure it is not disabled
					continue;
				}
				if(chargeList[i].DateStart>DateTime.Today){//not started yet
					continue;
				}
				//if(chargeList[i].DateStop.Year>1880//not blank
				//	&& chargeList[i].DateStop<DateTime.Today)//but already ended
				//{
				//	continue;
				//}
				//get a list dates of all completed procedures with this Code and patNum
				ArrayList ALdates=RepeatCharges.GetDates(ProcedureCodes.GetCodeNum(chargeList[i].ProcCode),chargeList[i].PatNum);
				startDate=chargeList[i].DateStart;
				//This is the repeating date using the old methodology.  It is necessary for checking if the repeating procedure was already added using the old methodology.
				DateTime possibleDateOld=startDate;
				//This is a more accurate repeating date which will allow procedures to be added on the 28th and later.
				DateTime possibleDateNew=startDate;
				int countMonths=0;
				//start looping through possible dates, beginning with the start date of the repeating charge
				while(possibleDateNew<=DateTime.Today) {
					//Only allow back dating up to 20 days.
					if(possibleDateNew<DateTime.Today.AddDays(-20)) {
						possibleDateOld=possibleDateOld.AddMonths(1);
						countMonths++;
						possibleDateNew=startDate.AddMonths(countMonths);
						continue;//don't go back more than 20 days
					}
					//check to see if the possible date is present in the list
					if(ALdates.Contains(possibleDateNew)
						|| ALdates.Contains(possibleDateOld))
					{
						possibleDateOld=possibleDateOld.AddMonths(1);
						countMonths++;
						possibleDateNew=startDate.AddMonths(countMonths);
						continue;
					}
					if(chargeList[i].DateStop.Year>1880//not blank
						&& chargeList[i].DateStop < possibleDateNew)//but already ended
					{
						break;
					}
					//otherwise, insert a procedure to db
					proc=new Procedure();
					proc.CodeNum=ProcedureCodes.GetCodeNum(chargeList[i].ProcCode);
					proc.DateEntryC=DateTimeOD.Today;
					proc.PatNum=chargeList[i].PatNum;
					proc.ProcDate=possibleDateNew;
					proc.DateTP=possibleDateNew;
					proc.ProcFee=chargeList[i].ChargeAmt;
					proc.ProcStatus=ProcStat.C;
					proc.ProvNum=PrefC.GetLong(PrefName.PracticeDefaultProv);
					proc.MedicalCode=ProcedureCodes.GetProcCode(proc.CodeNum).MedicalCode;
					proc.BaseUnits = ProcedureCodes.GetProcCode(proc.CodeNum).BaseUnits;
					proc.DiagnosticCode=PrefC.GetString(PrefName.ICD9DefaultForNewProcs);
					//Check if the repeating charge has been flagged to copy it's note into the billing note of the procedure.
					if(chargeList[i].CopyNoteToProc) {
						proc.BillingNote=chargeList[i].Note;
					}
					Procedures.Insert(proc);//no recall synch needed because dental offices don't use this feature
					countAdded++;
					possibleDateOld=possibleDateOld.AddMonths(1);
					countMonths++;
					possibleDateNew=startDate.AddMonths(countMonths);
					if(chargeList[i].CreatesClaim && !ProcedureCodes.GetProcCode(chargeList[i].ProcCode).NoBillIns) {
						List<PatPlan> patPlanList=PatPlans.Refresh(chargeList[i].PatNum);
						List<InsSub> subList=InsSubs.RefreshForFam(Patients.GetFamily(chargeList[i].PatNum));
						List<InsPlan> insPlanList=InsPlans.RefreshForSubList(subList);;
						List<Benefit> benefitList=Benefits.Refresh(patPlanList,subList);
						Claim claimCur;
						List<Procedure> procCurList=new List<Procedure>();
						procCurList.Add(proc);
						if(patPlanList.Count==0) {//no current insurance, do not create a claim
							continue;
						}
						//create the claimprocs
						Procedures.ComputeEstimates(proc,proc.PatNum,new List<ClaimProc>(),true,insPlanList,patPlanList,benefitList,Patients.GetPat(proc.PatNum).Age,subList);
						//get claimprocs for this proc, may be more than one
						List<ClaimProc> claimProcList=ClaimProcs.GetForProc(ClaimProcs.Refresh(proc.PatNum),proc.ProcNum);
						string claimType="P";
						if(patPlanList.Count==1 && PatPlans.GetOrdinal(PriSecMed.Medical,patPlanList,insPlanList,subList)>0) {//if there's exactly one medical plan
							claimType="Med";
						}
						claimCur=CreateClaim(claimType,patPlanList,insPlanList,claimProcList,proc,subList);
						claimProcList=ClaimProcs.Refresh(proc.PatNum);
						if(claimCur.ClaimNum==0) {
							continue;
						}
						claimsAdded++;
						ClaimL.CalculateAndUpdate(procCurList,insPlanList,claimCur,patPlanList,benefitList,Patients.GetPat(proc.PatNum).Age,subList);
						if(PatPlans.GetOrdinal(PriSecMed.Secondary,patPlanList,insPlanList,subList)>0 //if there exists a secondary plan
							&& !CultureInfo.CurrentCulture.Name.EndsWith("CA"))//and not canada (don't create secondary claim for canada)
						{
							claimCur=CreateClaim("S",patPlanList,insPlanList,claimProcList,proc,subList);
							if(claimCur.ClaimNum==0) {
								continue;
							}
							claimsAdded++;
							claimProcList=ClaimProcs.Refresh(proc.PatNum);
							claimCur.ClaimStatus="H";
							ClaimL.CalculateAndUpdate(procCurList,insPlanList,claimCur,patPlanList,benefitList,Patients.GetPat(proc.PatNum).Age,subList);
						}
					}
				}
			}
			//MessageBox.Show(countAdded.ToString()+" "+Lan.g(this,"procedures added."));
			MessageBox.Show(countAdded.ToString()+" "+Lan.g(this,"procedures added.")+"\r\n"+claimsAdded.ToString()+" "+Lan.g(this,"claims added."));
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}





















