using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormReferralsPatient : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butAddFrom;
		private OpenDental.UI.ODGrid gridMain;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public long PatNum;
		private OpenDental.UI.Button butSlip;
		private UI.Button butAddTo;
		private List<RefAttach> RefAttachList;
		private CheckBox checkShowAll;
		///<summary>This number is normally zero.  If this number is set externally before opening this form, then this will behave differently.</summary>
		public long ProcNum;

		///<summary></summary>
		public FormReferralsPatient()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReferralsPatient));
			this.gridMain = new OpenDental.UI.ODGrid();
			this.checkShowAll = new System.Windows.Forms.CheckBox();
			this.butAddTo = new OpenDental.UI.Button();
			this.butSlip = new OpenDental.UI.Button();
			this.butAddFrom = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12, 42);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(839, 261);
			this.gridMain.TabIndex = 74;
			this.gridMain.Title = "Referrals Attached";
			this.gridMain.TranslationName = "TableRefList";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// checkShowAll
			// 
			this.checkShowAll.Location = new System.Drawing.Point(560, 18);
			this.checkShowAll.Name = "checkShowAll";
			this.checkShowAll.Size = new System.Drawing.Size(162, 20);
			this.checkShowAll.TabIndex = 92;
			this.checkShowAll.Text = "Show All";
			this.checkShowAll.UseVisualStyleBackColor = true;
			this.checkShowAll.Click += new System.EventHandler(this.checkShowAll_Click);
			// 
			// butAddTo
			// 
			this.butAddTo.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddTo.Autosize = true;
			this.butAddTo.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddTo.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddTo.CornerRadius = 4F;
			this.butAddTo.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddTo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddTo.Location = new System.Drawing.Point(127, 10);
			this.butAddTo.Name = "butAddTo";
			this.butAddTo.Size = new System.Drawing.Size(94, 24);
			this.butAddTo.TabIndex = 91;
			this.butAddTo.Text = "Refer To";
			this.butAddTo.Click += new System.EventHandler(this.butAddTo_Click);
			// 
			// butSlip
			// 
			this.butSlip.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSlip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSlip.Autosize = true;
			this.butSlip.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSlip.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSlip.CornerRadius = 4F;
			this.butSlip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSlip.Location = new System.Drawing.Point(12, 317);
			this.butSlip.Name = "butSlip";
			this.butSlip.Size = new System.Drawing.Size(86, 24);
			this.butSlip.TabIndex = 90;
			this.butSlip.Text = "Referral Slip";
			this.butSlip.Click += new System.EventHandler(this.butSlip_Click);
			// 
			// butAddFrom
			// 
			this.butAddFrom.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddFrom.Autosize = true;
			this.butAddFrom.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddFrom.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddFrom.CornerRadius = 4F;
			this.butAddFrom.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddFrom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddFrom.Location = new System.Drawing.Point(12, 10);
			this.butAddFrom.Name = "butAddFrom";
			this.butAddFrom.Size = new System.Drawing.Size(109, 24);
			this.butAddFrom.TabIndex = 72;
			this.butAddFrom.Text = "Referred From";
			this.butAddFrom.Click += new System.EventHandler(this.butAddFrom_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(776, 317);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormReferralsPatient
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(863, 352);
			this.Controls.Add(this.checkShowAll);
			this.Controls.Add(this.butAddTo);
			this.Controls.Add(this.butSlip);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butAddFrom);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(871, 200);
			this.Name = "FormReferralsPatient";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Referrals for Patient";
			this.Load += new System.EventHandler(this.FormReferralsPatient_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormReferralsPatient_Load(object sender,EventArgs e) {
			if(ProcNum!=0) {
				Text=Lan.g(this,"Referrals");
				butAddFrom.Visible=false;
			}
			else {//all for patient
				checkShowAll.Visible=false;//we will always show all
			}
			FillGrid();
			if(RefAttachList.Count>0){
				gridMain.SetSelected(0,true);
			}
		}

		private void checkShowAll_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			RefAttachList=RefAttaches.RefreshFiltered(PatNum,checkShowAll.Checked,ProcNum);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRefList","From/To"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRefList","Name"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRefList","Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRefList","Status"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRefList","Proc"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRefList","Note"),200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRefList","Email"),120);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			//Referral referral;
			for(int i=0;i<RefAttachList.Count;i++){
				row=new ODGridRow();
				if(RefAttachList[i].IsFrom){
					row.Cells.Add(Lan.g(this,"From"));
				}
				else{
					row.Cells.Add(Lan.g(this,"To"));
				}
				row.Cells.Add(Referrals.GetNameFL(RefAttachList[i].ReferralNum));
				//referral=ReferralL.GetReferral(RefAttachList[i].ReferralNum);
				if(RefAttachList[i].RefDate.Year < 1880){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(RefAttachList[i].RefDate.ToShortDateString());
				}
				row.Cells.Add(Lan.g("enumReferralToStatus",RefAttachList[i].RefToStatus.ToString()));
				if(RefAttachList[i].ProcNum==0) {
					row.Cells.Add("");
				}
				else {
					Procedure proc=Procedures.GetOneProc(RefAttachList[i].ProcNum,false);
					string str=Procedures.GetDescription(proc);
					row.Cells.Add(str);
				}
				row.Cells.Add(RefAttachList[i].Note);
				Referral referral=Referrals.GetReferral(RefAttachList[i].ReferralNum);
				row.Cells.Add(referral.EMail);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormRefAttachEdit FormRAE2=new FormRefAttachEdit();
			RefAttach refattach=RefAttachList[e.Row].Copy();
			FormRAE2.RefAttachCur=refattach;
			FormRAE2.ShowDialog();
			FillGrid();
			//reselect
			for(int i=0;i<RefAttachList.Count;i++){
				if(RefAttachList[i].RefAttachNum==refattach.RefAttachNum) {
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void butAddFrom_Click(object sender,System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.RefAttachAdd)) {
				return;
			}
			FormReferralSelect FormRS=new FormReferralSelect();
			FormRS.IsSelectionMode=true;
			FormRS.ShowDialog();
			if(FormRS.DialogResult!=DialogResult.OK) {
				return;
			}
			RefAttach refattach=new RefAttach();
			refattach.ReferralNum=FormRS.SelectedReferral.ReferralNum;
			refattach.PatNum=PatNum;
			refattach.IsFrom=true;
			refattach.RefDate=DateTimeOD.Today;
			if(FormRS.SelectedReferral.IsDoctor) {//whether using ehr or not
				//we're not going to ask.  That's stupid.
				//if(MsgBox.Show(this,MsgBoxButtons.YesNo,"Is this an incoming transition of care from another provider?")){
				refattach.IsTransitionOfCare=true;
			}
			int order=0;
			for(int i=0;i<RefAttachList.Count;i++) {
				if(RefAttachList[i].ItemOrder > order) {
					order=RefAttachList[i].ItemOrder;
				}
			}
			refattach.ItemOrder=order+1;
			RefAttaches.Insert(refattach);
			SecurityLogs.MakeLogEntry(Permissions.RefAttachAdd,PatNum,"Referred From "+Referrals.GetNameFL(refattach.ReferralNum));
			FillGrid();
			for(int i=0;i<RefAttachList.Count;i++){
				if(RefAttachList[i].RefAttachNum==refattach.RefAttachNum) {
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void butAddTo_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.RefAttachAdd)) {
				return;
			}
			FormReferralSelect FormRS=new FormReferralSelect();
			FormRS.IsSelectionMode=true;
			FormRS.ShowDialog();
			if(FormRS.DialogResult!=DialogResult.OK) {
				return;
			}
			RefAttach refattach=new RefAttach();
			refattach.ReferralNum=FormRS.SelectedReferral.ReferralNum;
			refattach.PatNum=PatNum;
			refattach.IsFrom=false;
			refattach.RefDate=DateTimeOD.Today;
			if(FormRS.SelectedReferral.IsDoctor) {
				refattach.IsTransitionOfCare=true;
			}
			int order=0;
			for(int i=0;i<RefAttachList.Count;i++) {
				if(RefAttachList[i].ItemOrder > order) {
					order=RefAttachList[i].ItemOrder;
				}
			}
			refattach.ItemOrder=order+1;
			refattach.ProcNum=ProcNum;
			RefAttaches.Insert(refattach);
			SecurityLogs.MakeLogEntry(Permissions.RefAttachAdd,PatNum,"Referred To "+Referrals.GetNameFL(refattach.ReferralNum));
			FillGrid();
			for(int i=0;i<RefAttachList.Count;i++) {
				if(RefAttachList[i].ReferralNum==refattach.ReferralNum) {
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void butSlip_Click(object sender,EventArgs e) {
			int idx=gridMain.GetSelectedIndex();
			if(idx==-1){
				MsgBox.Show(this,"Please select a referral first");
				return;
			}
			Referral referral=Referrals.GetReferral(RefAttachList[idx].ReferralNum);
			SheetDef sheetDef;
			if(referral.Slip==0){
				sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.ReferralSlip);
			}
			else{
				sheetDef=SheetDefs.GetSheetDef(referral.Slip);
			}
			Sheet sheet=SheetUtil.CreateSheet(sheetDef,PatNum);
			SheetParameter.SetParameter(sheet,"PatNum",PatNum);
			SheetParameter.SetParameter(sheet,"ReferralNum",referral.ReferralNum);
			SheetFiller.FillFields(sheet);
			SheetUtil.CalculateHeights(sheet,this.CreateGraphics());
			FormSheetFillEdit FormS=new FormSheetFillEdit(sheet);
			FormS.ShowDialog();
			//grid will not be refilled, so no need to reselect.
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

	

		

		

		

		


	}
}





















