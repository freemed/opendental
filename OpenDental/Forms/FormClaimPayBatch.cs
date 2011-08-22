using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
///<summary></summary>
	public class FormClaimPayBatch:System.Windows.Forms.Form {
		private OpenDental.ValidDouble textAmount;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.TextBox textBankBranch;
		private System.Windows.Forms.TextBox textCheckNum;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.ComponentModel.Container components = null;
		//private bool ControlDown;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butDelete;
		///<summary>The list of splits to display in the grid.</summary>
		private System.Windows.Forms.Label labelClinic;
		private System.Windows.Forms.TextBox textCarrierName;
		private System.Windows.Forms.Label label7;
		private ClaimPayment ClaimPaymentCur;
		private OpenDental.UI.ODGrid gridAttached;
		private ValidDate textDateIssued;
		private Label labelDateIssued;
		private TextBox textClinic;
		private GroupBox groupBox1;
		private UI.Button butClaimPayEdit;
		private ODGrid gridOut;
		List<ClaimPaySplit> ClaimsAttached;
		List<ClaimPaySplit> ClaimsOutstanding;
		List<int> ProcsAttached;

		///<summary></summary>
		public FormClaimPayBatch(ClaimPayment claimPaymentCur) {
			InitializeComponent();// Required for Windows Form Designer support
			ClaimPaymentCur=claimPaymentCur;
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClaimPayBatch));
			this.textAmount = new OpenDental.ValidDouble();
			this.textDate = new OpenDental.ValidDate();
			this.textBankBranch = new System.Windows.Forms.TextBox();
			this.textCheckNum = new System.Windows.Forms.TextBox();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.labelClinic = new System.Windows.Forms.Label();
			this.textCarrierName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.gridAttached = new OpenDental.UI.ODGrid();
			this.textDateIssued = new OpenDental.ValidDate();
			this.labelDateIssued = new System.Windows.Forms.Label();
			this.textClinic = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butClaimPayEdit = new OpenDental.UI.Button();
			this.gridOut = new OpenDental.UI.ODGrid();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(114,88);
			this.textAmount.Name = "textAmount";
			this.textAmount.ReadOnly = true;
			this.textAmount.Size = new System.Drawing.Size(68,20);
			this.textAmount.TabIndex = 0;
			this.textAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(114,46);
			this.textDate.Name = "textDate";
			this.textDate.ReadOnly = true;
			this.textDate.Size = new System.Drawing.Size(68,20);
			this.textDate.TabIndex = 3;
			this.textDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBankBranch
			// 
			this.textBankBranch.Location = new System.Drawing.Point(351,48);
			this.textBankBranch.MaxLength = 25;
			this.textBankBranch.Name = "textBankBranch";
			this.textBankBranch.ReadOnly = true;
			this.textBankBranch.Size = new System.Drawing.Size(100,20);
			this.textBankBranch.TabIndex = 2;
			// 
			// textCheckNum
			// 
			this.textCheckNum.Location = new System.Drawing.Point(351,28);
			this.textCheckNum.MaxLength = 25;
			this.textCheckNum.Name = "textCheckNum";
			this.textCheckNum.ReadOnly = true;
			this.textCheckNum.Size = new System.Drawing.Size(100,20);
			this.textCheckNum.TabIndex = 1;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(569,27);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.ReadOnly = true;
			this.textNote.Size = new System.Drawing.Size(244,61);
			this.textNote.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16,50);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96,16);
			this.label6.TabIndex = 37;
			this.label6.Text = "Payment Date";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(17,92);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(95,16);
			this.label5.TabIndex = 36;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(258,30);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(90,16);
			this.label4.TabIndex = 35;
			this.label4.Text = "Check #";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(259,51);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(91,16);
			this.label3.TabIndex = 34;
			this.label3.Text = "Bank-Branch";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(457,28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104,16);
			this.label2.TabIndex = 33;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(803,631);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(803,593);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 0;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(45,626);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(92,24);
			this.butDelete.TabIndex = 52;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(25,28);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(86,14);
			this.labelClinic.TabIndex = 91;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCarrierName
			// 
			this.textCarrierName.Location = new System.Drawing.Point(351,68);
			this.textCarrierName.MaxLength = 25;
			this.textCarrierName.Name = "textCarrierName";
			this.textCarrierName.ReadOnly = true;
			this.textCarrierName.Size = new System.Drawing.Size(212,20);
			this.textCarrierName.TabIndex = 93;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(241,71);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(109,16);
			this.label7.TabIndex = 94;
			this.label7.Text = "Carrier Name";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// gridAttached
			// 
			this.gridAttached.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridAttached.HScrollVisible = false;
			this.gridAttached.Location = new System.Drawing.Point(83,158);
			this.gridAttached.Name = "gridAttached";
			this.gridAttached.ScrollValue = 0;
			this.gridAttached.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridAttached.Size = new System.Drawing.Size(660,200);
			this.gridAttached.TabIndex = 95;
			this.gridAttached.Title = "Attached to this Payment";
			this.gridAttached.TranslationName = "TableClaimPaySplits";
			this.gridAttached.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			// 
			// textDateIssued
			// 
			this.textDateIssued.Location = new System.Drawing.Point(114,67);
			this.textDateIssued.Name = "textDateIssued";
			this.textDateIssued.ReadOnly = true;
			this.textDateIssued.Size = new System.Drawing.Size(68,20);
			this.textDateIssued.TabIndex = 96;
			this.textDateIssued.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// labelDateIssued
			// 
			this.labelDateIssued.Location = new System.Drawing.Point(16,71);
			this.labelDateIssued.Name = "labelDateIssued";
			this.labelDateIssued.Size = new System.Drawing.Size(96,16);
			this.labelDateIssued.TabIndex = 97;
			this.labelDateIssued.Text = "Date Issued";
			this.labelDateIssued.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textClinic
			// 
			this.textClinic.Location = new System.Drawing.Point(114,25);
			this.textClinic.MaxLength = 25;
			this.textClinic.Name = "textClinic";
			this.textClinic.ReadOnly = true;
			this.textClinic.Size = new System.Drawing.Size(123,20);
			this.textClinic.TabIndex = 93;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labelClinic);
			this.groupBox1.Controls.Add(this.textDateIssued);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.labelDateIssued);
			this.groupBox1.Controls.Add(this.butClaimPayEdit);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textClinic);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textCarrierName);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textNote);
			this.groupBox1.Controls.Add(this.textCheckNum);
			this.groupBox1.Controls.Add(this.textBankBranch);
			this.groupBox1.Controls.Add(this.textAmount);
			this.groupBox1.Controls.Add(this.textDate);
			this.groupBox1.Location = new System.Drawing.Point(33,12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(831,126);
			this.groupBox1.TabIndex = 98;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Claim Payment";
			// 
			// butClaimPayEdit
			// 
			this.butClaimPayEdit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClaimPayEdit.Autosize = true;
			this.butClaimPayEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClaimPayEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClaimPayEdit.CornerRadius = 4F;
			this.butClaimPayEdit.Location = new System.Drawing.Point(738,92);
			this.butClaimPayEdit.Name = "butClaimPayEdit";
			this.butClaimPayEdit.Size = new System.Drawing.Size(75,24);
			this.butClaimPayEdit.TabIndex = 6;
			this.butClaimPayEdit.Text = "Edit";
			this.butClaimPayEdit.Click += new System.EventHandler(this.butClaimPayEdit_Click);
			// 
			// gridOut
			// 
			this.gridOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridOut.HScrollVisible = false;
			this.gridOut.Location = new System.Drawing.Point(83,402);
			this.gridOut.Name = "gridOut";
			this.gridOut.ScrollValue = 0;
			this.gridOut.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridOut.Size = new System.Drawing.Size(660,211);
			this.gridOut.TabIndex = 99;
			this.gridOut.Title = "All Outstanding Claims";
			this.gridOut.TranslationName = "TableClaimPaySplits";
			// 
			// FormClaimPayBatch
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(902,676);
			this.Controls.Add(this.gridOut);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridAttached);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimPayBatch";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Batch Claim Payment (EOB)";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClaimPayEdit_FormClosing);
			this.Load += new System.EventHandler(this.FormClaimPayEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimPayEdit_Load(object sender, System.EventArgs e) {
			FillClaimPayment();
			FillGrids();
		}

		private void FillClaimPayment() {
			textClinic.Text=Clinics.GetDesc(ClaimPaymentCur.ClinicNum);
			if(ClaimPaymentCur.CheckDate.Year<1800) {
				textDate.Text=ClaimPaymentCur.CheckDate.ToShortDateString();
			}
			if(ClaimPaymentCur.DateIssued.Year<1800) {
				textDateIssued.Text=ClaimPaymentCur.DateIssued.ToShortDateString();
			}
			textAmount.Text=ClaimPaymentCur.CheckAmt.ToString("F");
			textCheckNum.Text=ClaimPaymentCur.CheckNum;
			textBankBranch.Text=ClaimPaymentCur.BankBranch;
			textCarrierName.Text=ClaimPaymentCur.CarrierName;
			textNote.Text=ClaimPaymentCur.Note;
		}

		private void FillGrids(){
			Cursor.Current=Cursors.WaitCursor;
			//gridAttached:
			//Get all claims with claimprocs that have "this check attached" i.e. ClaimPaymentNum matches ClaimPaymentCur
			ClaimsAttached=Claims.GetByClaimPayment(ClaimPaymentCur.ClaimPaymentNum);
			ProcsAttached=new List<int>();
			for(int i=0;i<ClaimsAttached.Count;i++){
				ProcsAttached.Add(Procedures.GetCountForClaim(ClaimsAttached[i].ClaimNum));
			}
			gridAttached.BeginUpdate();
			gridAttached.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableClaimPayClaims","Service Date"),90);
			gridAttached.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayClaims","Patient"),140);
			gridAttached.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayClaims","Carrier"),230);
			gridAttached.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayClaims","Fee"),65,HorizontalAlignment.Right);
			gridAttached.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayClaims","Payment"),65,HorizontalAlignment.Right);
			gridAttached.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayClaims","Procs"),65,HorizontalAlignment.Right);
			gridAttached.Columns.Add(col);		 
			gridAttached.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ClaimsAttached.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(ClaimsAttached[i].DateClaim.ToShortDateString());
				row.Cells.Add(ClaimsAttached[i].PatName);
				row.Cells.Add(ClaimsAttached[i].Carrier);
				row.Cells.Add(ClaimsAttached[i].FeeBilled.ToString("F"));
				row.Cells.Add(ClaimsAttached[i].InsPayAmt.ToString("F"));
				row.Cells.Add(ProcsAttached[i].ToString());
				gridAttached.Rows.Add(row);
			}
			gridAttached.EndUpdate();
			//gridOutstanding:
			//Get all claims with claimprocs that have SUM(InsPayAmt)<ClaimFee for that claim
			ClaimsOutstanding=Claims.GetOutstandingClaims();
			gridOut.BeginUpdate();
			gridOut.Columns.Clear();
			col=new ODGridColumn(Lan.g("TableClaimPayClaims","Service Date"),90);
			gridOut.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayClaims","Patient"),140);
			gridOut.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayClaims","Carrier"),300);
			gridOut.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayClaims","Fee"),65,HorizontalAlignment.Right);
			gridOut.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayClaims","Payment"),65,HorizontalAlignment.Right);
			gridOut.Columns.Add(col);
			gridOut.Rows.Clear();
			for(int i=0;i<ClaimsOutstanding.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(ClaimsOutstanding[i].DateClaim.ToShortDateString());
				row.Cells.Add(ClaimsOutstanding[i].PatName);
				row.Cells.Add(ClaimsOutstanding[i].Carrier);
				row.Cells.Add(ClaimsOutstanding[i].FeeBilled.ToString("F"));
				row.Cells.Add(ClaimsOutstanding[i].InsPayAmt.ToString("F"));
				gridOut.Rows.Add(row);
			}
			gridOut.EndUpdate();
			Cursor.Current=Cursors.Default;
		}


		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			//splitTot=0;
			//for(int i=0;i<gridMain.SelectedIndices.Length;i++){
			//  splitTot+=(decimal)splits[gridMain.SelectedIndices[i]].InsPayAmt;
			//}
			//textAmount.Text=splitTot.ToString("F");
		}

		private void checkShowUn_Click(object sender, System.EventArgs e) {
			//FillGrid();
		}

		private void butClaimPayEdit_Click(object sender,EventArgs e) {
			FormClaimPayEdit FormCPE=new FormClaimPayEdit(ClaimPaymentCur);
			FormCPE.ShowDialog();
			FillClaimPayment();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			//this button is disabled if user does not have permision to edit.
			if(IsNew){
				DialogResult=DialogResult.Cancel;//causes claimPayment to be deleted.
				return;
			}
			if(!MsgBox.Show(this,true,"Delete this insurance check?")){
				return;
			}
			try{
				ClaimPayments.Delete(ClaimPaymentCur);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			/*
			if(textDate.Text=="") {
				MsgBox.Show(this,"Please enter a date first.");
				return;
			}
			if(textDate.errorProvider1.GetError(textDate)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(gridMain.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"At least one item must be selected, or use the delete button."));	
				return;
			}
			if(IsNew){
				//prevents backdating of initial check
				if(!Security.IsAuthorized(Permissions.InsPayCreate,PIn.Date(textDate.Text))){
					return;
				}
				//prevents attaching claimprocs with a date that is older than allowed by security.



			}
			else{
				//Editing an old entry will already be blocked if the date was too old, and user will not be able to click OK button.
				//This catches it if user changed the date to be older.
				if(!Security.IsAuthorized(Permissions.InsPayEdit,PIn.Date(textDate.Text))){
					return;
				}
			}
			if(comboClinic.SelectedIndex==0){
				ClaimPaymentCur.ClinicNum=0;
			}
			else{
				ClaimPaymentCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			}
			ClaimPaymentCur.CheckAmt=PIn.Double(textAmount.Text);
			ClaimPaymentCur.CheckDate=PIn.Date(textDate.Text);
			ClaimPaymentCur.CheckNum=textCheckNum.Text;
			ClaimPaymentCur.BankBranch=textBankBranch.Text;
			ClaimPaymentCur.CarrierName=textCarrierName.Text;
			ClaimPaymentCur.Note=textNote.Text;
			try{
				ClaimPayments.Update(ClaimPaymentCur);//error thrown if trying to change amount and already attached to a deposit.
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			//this could be optimized to only save changes.
			//Would require a starting list to compare to.
			//But this isn't bad, since changes all saved at the very end
			List<int> selectedRows=new List<int>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				selectedRows.Add(gridMain.SelectedIndices[i]);
			}
			for(int i=0;i<splits.Count;i++){
				if(selectedRows.Contains(i)){//row is selected
					ClaimProcs.SetForClaim(splits[i].ClaimNum,ClaimPaymentCur.ClaimPaymentNum,ClaimPaymentCur.CheckDate,true);
					//Audit trail isn't perfect, since it doesn't make an entry if you remove a claim from a payment.
					//And it always makes more audit trail entries when you click OK, even if you didn't actually attach new claims.
					//But since this will cover the vast majority if situations.
					if(IsNew){
						SecurityLogs.MakeLogEntry(Permissions.InsPayCreate,splits[i].PatNum,
							Patients.GetLim(splits[i].PatNum).GetNameLF()+", "
							+Lan.g(this,"Total Amt: ")+ClaimPaymentCur.CheckAmt.ToString("c")+", "
							+Lan.g(this,"Claim Split: ")+splits[i].InsPayAmt.ToString("c"));
					}
					else{
						SecurityLogs.MakeLogEntry(Permissions.InsPayEdit,splits[i].PatNum,
							Patients.GetLim(splits[i].PatNum).GetNameLF()+", "
							+Lan.g(this,"Total Amt: ")+ClaimPaymentCur.CheckAmt.ToString("c")+", "
							+Lan.g(this,"Claim Split: ")+splits[i].InsPayAmt.ToString("c"));
					}
				}
				else{//row not selected
					//If user had not been attaching their inspayments to checks, then this will cause such payments to annoyingly have their
					//date changed to the current date.  This prompts them to call us.  Then, we tell them to attach to checks.
					ClaimProcs.SetForClaim(splits[i].ClaimNum,ClaimPaymentCur.ClaimPaymentNum,ClaimPaymentCur.CheckDate,false);
				}
			}
			DialogResult=DialogResult.OK;*/
		}

		private void FormClaimPayEdit_FormClosing(object sender,FormClosingEventArgs e) {
			if(DialogResult==DialogResult.OK){
				return;
			}
			if(IsNew){//cancel
				//ClaimProcs never saved in the first place
				ClaimPayments.Delete(ClaimPaymentCur);
			}
		}

		
		



	}
}













