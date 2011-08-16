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
	public class FormClaimPayEdit:System.Windows.Forms.Form {
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
		private decimal splitTot;
		///<summary>The list of splits to display in the grid.</summary>
		private List<ClaimPaySplit> splits;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		private System.Windows.Forms.TextBox textCarrierName;
		private System.Windows.Forms.Label label7;
		private ClaimPayment ClaimPaymentCur;
		///<summary>Set this externally.</summary>
		public long OriginatingClaimNum;

		///<summary></summary>
		public FormClaimPayEdit(ClaimPayment claimPaymentCur) {
			InitializeComponent();// Required for Windows Form Designer support
			ClaimPaymentCur=claimPaymentCur;
			splits=new List<ClaimPaySplit>();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClaimPayEdit));
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
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.textCarrierName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(133,63);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(58,20);
			this.textAmount.TabIndex = 0;
			this.textAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(133,43);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(68,20);
			this.textDate.TabIndex = 3;
			this.textDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBankBranch
			// 
			this.textBankBranch.Location = new System.Drawing.Point(133,103);
			this.textBankBranch.MaxLength = 25;
			this.textBankBranch.Name = "textBankBranch";
			this.textBankBranch.Size = new System.Drawing.Size(100,20);
			this.textBankBranch.TabIndex = 2;
			// 
			// textCheckNum
			// 
			this.textCheckNum.Location = new System.Drawing.Point(133,83);
			this.textCheckNum.MaxLength = 25;
			this.textCheckNum.Name = "textCheckNum";
			this.textCheckNum.Size = new System.Drawing.Size(100,20);
			this.textCheckNum.TabIndex = 1;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(133,143);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(324,70);
			this.textNote.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(35,47);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96,16);
			this.label6.TabIndex = 37;
			this.label6.Text = "Payment Date";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(36,67);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(95,16);
			this.label5.TabIndex = 36;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(40,85);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(90,16);
			this.label4.TabIndex = 35;
			this.label4.Text = "Check #";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(41,106);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(91,16);
			this.label3.TabIndex = 34;
			this.label3.Text = "Bank-Branch";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12,144);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120,16);
			this.label2.TabIndex = 33;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(433,269);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 7;
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
			this.butOK.Location = new System.Drawing.Point(342,269);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 6;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(133,21);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(209,21);
			this.comboClinic.TabIndex = 92;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(44,25);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(86,14);
			this.labelClinic.TabIndex = 91;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCarrierName
			// 
			this.textCarrierName.Location = new System.Drawing.Point(133,123);
			this.textCarrierName.MaxLength = 25;
			this.textCarrierName.Name = "textCarrierName";
			this.textCarrierName.Size = new System.Drawing.Size(212,20);
			this.textCarrierName.TabIndex = 93;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(23,126);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(109,16);
			this.label7.TabIndex = 94;
			this.label7.Text = "Carrier Name";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormClaimPayEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(528,313);
			this.Controls.Add(this.textCarrierName);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.textBankBranch);
			this.Controls.Add(this.textCheckNum);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimPayEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Claim Payment";
			this.Load += new System.EventHandler(this.FormClaimPayEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClaimPayEdit_Load(object sender, System.EventArgs e) {
			/*
			//ClaimPayment created before opening this form
			if(IsNew){
				if(!Security.IsAuthorized(Permissions.InsPayCreate)){//date not checked here
					DialogResult=DialogResult.Cancel;//causes claimPayment to be deleted.
					return;
				}
			}
			else{
				if(!Security.IsAuthorized(Permissions.InsPayEdit,ClaimPaymentCur.CheckDate)){
					butOK.Enabled=false;
					butDelete.Enabled=false;
				}
			}
			if(IsNew){
				checkShowUn.Checked=true;
			}
			if(PrefC.GetBool(PrefName.EasyNoClinics)){
				comboClinic.Visible=false;
				labelClinic.Visible=false;
			}
			comboClinic.Items.Clear();
			comboClinic.Items.Add(Lan.g(this,"none"));
			comboClinic.SelectedIndex=0;
			for(int i=0;i<Clinics.List.Length;i++){
				comboClinic.Items.Add(Clinics.List[i].Description);
				if(Clinics.List[i].ClinicNum==ClaimPaymentCur.ClinicNum){
					comboClinic.SelectedIndex=i+1;
				}
			}
			textDate.Text=ClaimPaymentCur.CheckDate.ToShortDateString();
			textCheckNum.Text=ClaimPaymentCur.CheckNum;
			textBankBranch.Text=ClaimPaymentCur.BankBranch;
			textCarrierName.Text=ClaimPaymentCur.CarrierName;
			textNote.Text=ClaimPaymentCur.Note;
			FillGrid();
			if(IsNew){
				gridMain.SetSelected(true);
				splitTot=0;
				for(int i=0;i<gridMain.SelectedIndices.Length;i++){
					splitTot+=(decimal)splits[gridMain.SelectedIndices[i]].InsPayAmt;
				}
				textAmount.Text=splitTot.ToString("F");
			}*/
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

		private void butCancel_Click(object sender,System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
		



	}
}













