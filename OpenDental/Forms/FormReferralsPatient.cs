using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormReferralsPatient : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.ODGrid gridMain;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public long PatNum;
		private OpenDental.UI.Button butSlip;
		private RefAttach[] RefAttachList;

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
			this.butAdd = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.butSlip = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(655,229);
			this.gridMain.TabIndex = 74;
			this.gridMain.Title = "Referrals Attached";
			this.gridMain.TranslationName = "TableRefList";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(12,266);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,24);
			this.butAdd.TabIndex = 72;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(592,266);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butSlip
			// 
			this.butSlip.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSlip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSlip.Autosize = true;
			this.butSlip.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSlip.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSlip.CornerRadius = 4F;
			this.butSlip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSlip.Location = new System.Drawing.Point(214,266);
			this.butSlip.Name = "butSlip";
			this.butSlip.Size = new System.Drawing.Size(86,24);
			this.butSlip.TabIndex = 90;
			this.butSlip.Text = "Referral Slip";
			this.butSlip.Click += new System.EventHandler(this.butSlip_Click);
			// 
			// FormReferralsPatient
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(695,309);
			this.Controls.Add(this.butSlip);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormReferralsPatient";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Referrals for Patient";
			this.Load += new System.EventHandler(this.FormReferralsPatient_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormReferralsPatient_Load(object sender,EventArgs e) {
			FillGrid();
			if(RefAttachList.Length>0){
				gridMain.SetSelected(0,true);
			}
		}

		private void FillGrid() {
			RefAttachList=RefAttaches.Refresh(PatNum);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRefList","From/To"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRefList","Name"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRefList","Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRefList","Status"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRefList","Note"),200);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			//Referral referral;
			for(int i=0;i<RefAttachList.Length;i++){
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
				row.Cells.Add(RefAttachList[i].Note);
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
			for(int i=0;i<RefAttachList.Length;i++){
				if(RefAttachList[i].ReferralNum==refattach.ReferralNum){
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void butAdd_Click(object sender,System.EventArgs e) {
			FormReferralSelect FormRS=new FormReferralSelect();
			FormRS.IsSelectionMode=true;
			FormRS.ShowDialog();
			if(FormRS.DialogResult!=DialogResult.OK) {
				return;
			}
			FormRefAttachEdit FormRA=new FormRefAttachEdit();
			RefAttach refattach=new RefAttach();
			refattach.ReferralNum=FormRS.SelectedReferral.ReferralNum;
			refattach.PatNum=PatNum;
			refattach.IsFrom=true;
			refattach.RefDate=DateTime.Today;
			int order=0;
			for(int i=0;i<RefAttachList.Length;i++) {
				if(RefAttachList[i].ItemOrder > order) {
					order=RefAttachList[i].ItemOrder;
				}
			}
			refattach.ItemOrder=order+1;
			FormRA.RefAttachCur=refattach;
			FormRA.IsNew=true;
			FormRA.ShowDialog();
			if(FormRA.DialogResult!=DialogResult.OK) {
				return;
			}
			FillGrid();
			//select
			for(int i=0;i<RefAttachList.Length;i++){
				if(RefAttachList[i].ReferralNum==refattach.ReferralNum){
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





















