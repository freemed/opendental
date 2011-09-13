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
		private OpenDental.UI.Button butClose;
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
		private UI.Button butDetach;
		private ValidDouble textTotal;
		private Label label8;
		private Label labelOut;
		private UI.Button butDown;
		private UI.Button butUp;
		private bool IsDeleting;

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
			this.butClose = new OpenDental.UI.Button();
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
			this.butDetach = new OpenDental.UI.Button();
			this.labelOut = new System.Windows.Forms.Label();
			this.textTotal = new OpenDental.ValidDouble();
			this.label8 = new System.Windows.Forms.Label();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(105,88);
			this.textAmount.Name = "textAmount";
			this.textAmount.ReadOnly = true;
			this.textAmount.Size = new System.Drawing.Size(68,20);
			this.textAmount.TabIndex = 0;
			this.textAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(105,46);
			this.textDate.Name = "textDate";
			this.textDate.ReadOnly = true;
			this.textDate.Size = new System.Drawing.Size(68,20);
			this.textDate.TabIndex = 3;
			this.textDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBankBranch
			// 
			this.textBankBranch.Location = new System.Drawing.Point(330,45);
			this.textBankBranch.MaxLength = 25;
			this.textBankBranch.Name = "textBankBranch";
			this.textBankBranch.ReadOnly = true;
			this.textBankBranch.Size = new System.Drawing.Size(100,20);
			this.textBankBranch.TabIndex = 2;
			// 
			// textCheckNum
			// 
			this.textCheckNum.Location = new System.Drawing.Point(330,25);
			this.textCheckNum.MaxLength = 25;
			this.textCheckNum.Name = "textCheckNum";
			this.textCheckNum.ReadOnly = true;
			this.textCheckNum.Size = new System.Drawing.Size(100,20);
			this.textCheckNum.TabIndex = 1;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(569,25);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.ReadOnly = true;
			this.textNote.Size = new System.Drawing.Size(244,35);
			this.textNote.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(7,50);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96,16);
			this.label6.TabIndex = 37;
			this.label6.Text = "Payment Date";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8,92);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(95,16);
			this.label5.TabIndex = 36;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(237,27);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(90,16);
			this.label4.TabIndex = 35;
			this.label4.Text = "Check #";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(238,48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(91,16);
			this.label3.TabIndex = 34;
			this.label3.Text = "Bank-Branch";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(461,26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104,16);
			this.label2.TabIndex = 33;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(803,646);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
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
			this.butDelete.Location = new System.Drawing.Point(44,646);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(79,24);
			this.butDelete.TabIndex = 52;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(16,28);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(86,14);
			this.labelClinic.TabIndex = 91;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCarrierName
			// 
			this.textCarrierName.Location = new System.Drawing.Point(330,70);
			this.textCarrierName.MaxLength = 25;
			this.textCarrierName.Name = "textCarrierName";
			this.textCarrierName.ReadOnly = true;
			this.textCarrierName.Size = new System.Drawing.Size(212,20);
			this.textCarrierName.TabIndex = 93;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(220,68);
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
			this.gridAttached.Location = new System.Drawing.Point(83,129);
			this.gridAttached.Name = "gridAttached";
			this.gridAttached.ScrollValue = 0;
			this.gridAttached.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridAttached.Size = new System.Drawing.Size(660,222);
			this.gridAttached.TabIndex = 95;
			this.gridAttached.Title = "Attached to this Payment";
			this.gridAttached.TranslationName = "TableClaimPaySplits";
			this.gridAttached.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAttached_CellDoubleClick);
			// 
			// textDateIssued
			// 
			this.textDateIssued.Location = new System.Drawing.Point(105,67);
			this.textDateIssued.Name = "textDateIssued";
			this.textDateIssued.ReadOnly = true;
			this.textDateIssued.Size = new System.Drawing.Size(68,20);
			this.textDateIssued.TabIndex = 96;
			this.textDateIssued.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// labelDateIssued
			// 
			this.labelDateIssued.Location = new System.Drawing.Point(7,71);
			this.labelDateIssued.Name = "labelDateIssued";
			this.labelDateIssued.Size = new System.Drawing.Size(96,16);
			this.labelDateIssued.TabIndex = 97;
			this.labelDateIssued.Text = "Date Issued";
			this.labelDateIssued.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textClinic
			// 
			this.textClinic.Location = new System.Drawing.Point(105,25);
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
			this.groupBox1.Location = new System.Drawing.Point(33,6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(831,117);
			this.groupBox1.TabIndex = 98;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Payment Details";
			// 
			// butClaimPayEdit
			// 
			this.butClaimPayEdit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClaimPayEdit.Autosize = true;
			this.butClaimPayEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClaimPayEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClaimPayEdit.CornerRadius = 4F;
			this.butClaimPayEdit.Location = new System.Drawing.Point(738,85);
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
			this.gridOut.Location = new System.Drawing.Point(83,387);
			this.gridOut.Name = "gridOut";
			this.gridOut.ScrollValue = 0;
			this.gridOut.Size = new System.Drawing.Size(660,243);
			this.gridOut.TabIndex = 99;
			this.gridOut.Title = "All Outstanding Claims";
			this.gridOut.TranslationName = "TableClaimPaySplits";
			this.gridOut.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridOut_CellDoubleClick);
			// 
			// butDetach
			// 
			this.butDetach.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDetach.Autosize = true;
			this.butDetach.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDetach.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDetach.CornerRadius = 4F;
			this.butDetach.Image = global::OpenDental.Properties.Resources.down;
			this.butDetach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDetach.Location = new System.Drawing.Point(372,357);
			this.butDetach.Name = "butDetach";
			this.butDetach.Size = new System.Drawing.Size(79,24);
			this.butDetach.TabIndex = 101;
			this.butDetach.Text = "Detach";
			this.butDetach.Click += new System.EventHandler(this.butDetach_Click);
			// 
			// labelOut
			// 
			this.labelOut.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelOut.Location = new System.Drawing.Point(256,633);
			this.labelOut.Name = "labelOut";
			this.labelOut.Size = new System.Drawing.Size(338,18);
			this.labelOut.TabIndex = 102;
			this.labelOut.Text = "Double click on an outstanding claim to enter payments for this check.";
			// 
			// textTotal
			// 
			this.textTotal.Location = new System.Drawing.Point(606,359);
			this.textTotal.Name = "textTotal";
			this.textTotal.ReadOnly = true;
			this.textTotal.Size = new System.Drawing.Size(68,20);
			this.textTotal.TabIndex = 0;
			this.textTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(499,363);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(105,16);
			this.label8.TabIndex = 36;
			this.label8.Text = "Total Payments";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(771,225);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(75,24);
			this.butDown.TabIndex = 104;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(771,195);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(75,24);
			this.butUp.TabIndex = 103;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// FormClaimPayBatch
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(902,676);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.labelOut);
			this.Controls.Add(this.butDetach);
			this.Controls.Add(this.gridOut);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridAttached);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textTotal);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimPayBatch";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Insurance Payment (EOB)";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClaimPayBatch_FormClosing);
			this.Load += new System.EventHandler(this.FormClaimPayEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClaimPayEdit_Load(object sender, System.EventArgs e) {
			FillClaimPayment();
			FillGrids();
			if(ClaimPaymentCur.IsPartial){
				//an incomplete payment that's not yet locked
			}
			else{//locked
				if(!Security.IsAuthorized(Permissions.InsPayEdit,ClaimPaymentCur.CheckDate)) {
					butDelete.Enabled=false;
					gridAttached.Enabled=false;
					butClaimPayEdit.Enabled=false;
					butUp.Visible=false;
					butDown.Visible=false;
				}
				//someone with permission can double click on the top grid to edit amounts and can edit the object fields as well.
				butDetach.Visible=false;
				gridOut.Visible=false;
				labelOut.Visible=false;
			}
		}

		private void FillClaimPayment() {
			textClinic.Text=Clinics.GetDesc(ClaimPaymentCur.ClinicNum);
			if(ClaimPaymentCur.CheckDate.Year>1800) {
				textDate.Text=ClaimPaymentCur.CheckDate.ToShortDateString();
			}
			if(ClaimPaymentCur.DateIssued.Year>1800) {
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
			//Get all claims attached to this check
			ClaimsAttached=Claims.GetAttachedToPayment(ClaimPaymentCur.ClaimPaymentNum);
			//List<int> ProcsAttached=new List<int>();//one item per claimAttached object.
			//for(int i=0;i<ClaimsAttached.Count;i++){
			//	ProcsAttached.Add(Procedures.GetCountForClaim(ClaimsAttached[i].ClaimNum));
			//}
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
			//col=new ODGridColumn(Lan.g("TableClaimPayClaims","Procs"),65,HorizontalAlignment.Right);
			//gridAttached.Columns.Add(col);		 
			gridAttached.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ClaimsAttached.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(ClaimsAttached[i].DateClaim.ToShortDateString());
				row.Cells.Add(ClaimsAttached[i].PatName);
				row.Cells.Add(ClaimsAttached[i].Carrier);
				row.Cells.Add(ClaimsAttached[i].FeeBilled.ToString("F"));
				row.Cells.Add(ClaimsAttached[i].InsPayAmt.ToString("F"));
				//row.Cells.Add(ProcsAttached[i].ToString());
				gridAttached.Rows.Add(row);
			}
			gridAttached.EndUpdate();
			//gridOutstanding-------------------------------------------------------------------------------------------------
//todo: needs work
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
			double total=0;
			for(int i=0;i<ClaimsAttached.Count;i++) {
				total+=ClaimsAttached[i].InsPayAmt;
			}
			textTotal.Text=total.ToString("F");
		}

		private void butClaimPayEdit_Click(object sender,EventArgs e) {
			FormClaimPayEdit FormCPE=new FormClaimPayEdit(ClaimPaymentCur);
			FormCPE.ShowDialog();
			FillClaimPayment();
		}

		private void butDetach_Click(object sender,EventArgs e) {
			if(gridAttached.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a claim from the attached claims grid above.");
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Remove selected claims from this check?")) {
				return;
			}
//todo: reset PaymentRows because they are now out of order
			for(int i=0;i<gridAttached.SelectedIndices.Length;i++) {
				ClaimProcs.DetachFromPayment(ClaimsAttached[gridAttached.SelectedIndices[i]].ClaimNum,ClaimPaymentCur.ClaimPaymentNum);
			}
			FillGrids();			
		}

		private void gridAttached_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//top grid
			//bring up claimedit window.  User should be able to edit if not locked.
			Claim claimCur=Claims.GetClaim(ClaimsAttached[gridAttached.GetSelectedIndex()].ClaimNum);
			FormClaimEdit FormCE=new FormClaimEdit(claimCur,Patients.GetPat(claimCur.PatNum),Patients.GetFamily(claimCur.PatNum));
			FormCE.ShowDialog();
			FillGrids();	
		}

		private void butUp_Click(object sender,EventArgs e) {
			if(gridAttached.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select an item in the grid first.");
				return;
			}
			int[] selected=new int[gridAttached.SelectedIndices.Length];//remember the selected rows so that we can reselect them
			for(int i=0;i<gridAttached.SelectedIndices.Length;i++) {
				selected[i]=gridAttached.SelectedIndices[i];
			}
			if(selected[0]==0) {//can't go up
				return;
			}
			for(int i=0;i<selected.Length;i++) {
				//In the db, move the one above down to the current pos
				ClaimProcs.SetPaymentRow(ClaimsAttached[selected[i]-1].ClaimNum,ClaimPaymentCur.ClaimPaymentNum,selected[i]);
				//and move this row up one
				ClaimProcs.SetPaymentRow(ClaimsAttached[selected[i]].ClaimNum,ClaimPaymentCur.ClaimPaymentNum,selected[i]-1);
				//Then, swap them in the cached list.
				ClaimsAttached.Reverse(selected[i]-1,2);
			}
			FillGrids();
			for(int i=0;i<selected.Length;i++) {
				gridAttached.SetSelected(selected[i]-1,true);
			}
		}

		private void butDown_Click(object sender,EventArgs e) {
			if(gridAttached.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select an item in the grid first.");
				return;
			}
			int[] selected=new int[gridAttached.SelectedIndices.Length];
			for(int i=0;i<gridAttached.SelectedIndices.Length;i++) {
				selected[i]=gridAttached.SelectedIndices[i];
			}
			if(selected[selected.Length-1]==ClaimsAttached.Count-1) {//already at the bottom
				return;
			}
			for(int i=selected.Length-1;i>=0;i--) {//go backwards
				//In the db, move the one below up to the current pos
				ClaimProcs.SetPaymentRow(ClaimsAttached[selected[i]+1].ClaimNum,ClaimPaymentCur.ClaimPaymentNum,selected[i]);
				//and move this row down one
				ClaimProcs.SetPaymentRow(ClaimsAttached[selected[i]].ClaimNum,ClaimPaymentCur.ClaimPaymentNum,selected[i]+1);
				//Then, swap them in the cached list.
				ClaimsAttached.Reverse(selected[i],2);
			}
			FillGrids();
			for(int i=0;i<selected.Length;i++) {
				gridAttached.SetSelected(selected[i]+1,true);
			}
		}

		private void gridOut_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//bottom grid
			//bring up claimedit window
			//after returning from the claim edit window, use a query to get a list of all the claimprocs that have amounts entered for that claim, but have ClaimPaymentNumber of 0.
			//Set all those claimprocs to be attached.
			Claim claimCur=Claims.GetClaim(ClaimsOutstanding[gridOut.GetSelectedIndex()].ClaimNum);
			FormClaimEdit FormCE=new FormClaimEdit(claimCur,Patients.GetPat(claimCur.PatNum),Patients.GetFamily(claimCur.PatNum));
			FormCE.ShowDialog();
			ClaimProcs.AttachToPayment(claimCur.ClaimNum,ClaimPaymentCur.ClaimPaymentNum,ClaimPaymentCur.CheckDate,ClaimsAttached.Count);
			FillGrids();			
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(!MsgBox.Show(this,true,"Delete this insurance check?")){
				return;
			}
			if(ClaimPaymentCur.IsPartial) {//probably new
				//everyone should have permission to delete a partial payment
			}
			else {//locked
				//this delete button already disabled if no permission
			}
			try{
				ClaimPayments.Delete(ClaimPaymentCur);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			IsDeleting=true;
			Close();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormClaimPayBatch_FormClosing(object sender,FormClosingEventArgs e) {
			if(IsDeleting){
				return;
			}
			if(ClaimPaymentCur.IsPartial) {
				if(textAmount.Text==textTotal.Text) {
					ClaimPaymentCur.IsPartial=false;
					ClaimPayments.Update(ClaimPaymentCur);
				}
			}
			else {//locked
				if(textAmount.Text!=textTotal.Text) {//someone edited a locked payment
					if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Amounts do not match.  Continue anyway?")) {
						e.Cancel=true;
						return;
					}
				}
			}
		}

		


		
		



	}
}













