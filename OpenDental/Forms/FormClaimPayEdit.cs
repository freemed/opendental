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
	public class FormClaimPayEdit : System.Windows.Forms.Form{
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
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;
		//private bool ControlDown;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.CheckBox checkShowUn;
		private OpenDental.UI.Button butDelete;
		private double splitTot;
		///<summary>The list of splits to display in the grid.</summary>
		private List<ClaimPaySplit> splits;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		private System.Windows.Forms.TextBox textCarrierName;
		private System.Windows.Forms.Label label7;
		private ClaimPayment ClaimPaymentCur;
		private OpenDental.UI.ODGrid gridMain;
		///<summary>Set this externally.</summary>
		public long OriginatingClaimNum;

		///<summary></summary>
		public FormClaimPayEdit(ClaimPayment claimPaymentCur){
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
			this.checkShowUn = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.textCarrierName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(668,56);
			this.textAmount.Name = "textAmount";
			this.textAmount.ReadOnly = true;
			this.textAmount.Size = new System.Drawing.Size(58,20);
			this.textAmount.TabIndex = 44;
			this.textAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(668,36);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(68,20);
			this.textDate.TabIndex = 0;
			this.textDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBankBranch
			// 
			this.textBankBranch.Location = new System.Drawing.Point(668,96);
			this.textBankBranch.MaxLength = 25;
			this.textBankBranch.Name = "textBankBranch";
			this.textBankBranch.Size = new System.Drawing.Size(100,20);
			this.textBankBranch.TabIndex = 2;
			// 
			// textCheckNum
			// 
			this.textCheckNum.Location = new System.Drawing.Point(668,76);
			this.textCheckNum.MaxLength = 25;
			this.textCheckNum.Name = "textCheckNum";
			this.textCheckNum.Size = new System.Drawing.Size(100,20);
			this.textCheckNum.TabIndex = 1;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(558,174);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(324,70);
			this.textNote.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(570,40);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96,16);
			this.label6.TabIndex = 37;
			this.label6.Text = "Payment Date";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(571,60);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(95,16);
			this.label5.TabIndex = 36;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(575,78);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(90,16);
			this.label4.TabIndex = 35;
			this.label4.Text = "Check #";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(576,99);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(91,16);
			this.label3.TabIndex = 34;
			this.label3.Text = "Bank-Branch";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(560,154);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(132,16);
			this.label2.TabIndex = 33;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
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
			this.butCancel.TabIndex = 7;
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
			this.butOK.TabIndex = 6;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// checkShowUn
			// 
			this.checkShowUn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowUn.Location = new System.Drawing.Point(564,373);
			this.checkShowUn.Name = "checkShowUn";
			this.checkShowUn.Size = new System.Drawing.Size(215,24);
			this.checkShowUn.TabIndex = 4;
			this.checkShowUn.Text = "Show Unattached";
			this.checkShowUn.Click += new System.EventHandler(this.checkShowUn_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(562,632);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(129,35);
			this.label1.TabIndex = 51;
			this.label1.Text = "(Deletes this Check, but not any splits)";
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
			this.butDelete.Location = new System.Drawing.Point(565,597);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(92,24);
			this.butDelete.TabIndex = 52;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(668,14);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(209,21);
			this.comboClinic.TabIndex = 92;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(579,18);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(86,14);
			this.labelClinic.TabIndex = 91;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCarrierName
			// 
			this.textCarrierName.Location = new System.Drawing.Point(668,116);
			this.textCarrierName.MaxLength = 25;
			this.textCarrierName.Name = "textCarrierName";
			this.textCarrierName.Size = new System.Drawing.Size(212,20);
			this.textCarrierName.TabIndex = 93;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(558,119);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(109,16);
			this.label7.TabIndex = 94;
			this.label7.Text = "Carrier Name";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(8,14);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(539,641);
			this.gridMain.TabIndex = 95;
			this.gridMain.Title = "Claim Payment Splits";
			this.gridMain.TranslationName = "TableClaimPaySplits";
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			// 
			// FormClaimPayEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(902,676);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.textCarrierName);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkShowUn);
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
			this.Text = "Edit Insurance Claim Check";
			this.Load += new System.EventHandler(this.FormClaimPayEdit_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClaimPayEdit_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClaimPayEdit_Load(object sender, System.EventArgs e) {
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
			if(PrefC.GetBool("EasyNoClinics")){
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
					splitTot+=splits[gridMain.SelectedIndices[i]].InsPayAmt;
				}
				textAmount.Text=splitTot.ToString("F");
			}
		}

		private void FillGrid(){
			splits=Claims.RefreshByCheck(ClaimPaymentCur.ClaimPaymentNum,checkShowUn.Checked);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableClaimPaySplits","Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPaySplits","Prov"),40);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPaySplits","Patient"),140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPaySplits","Carrier"),140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPaySplits","Fee"),65,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPaySplits","Payment"),65,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);			 
			gridMain.Rows.Clear();
			ODGridRow row;
			splitTot=0;
			for(int i=0;i<splits.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(splits[i].DateClaim.ToShortDateString());
				row.Cells.Add(splits[i].ProvAbbr);
				row.Cells.Add(splits[i].PatName);
				row.Cells.Add(splits[i].Carrier);
				row.Cells.Add(splits[i].FeeBilled.ToString("F"));
				row.Cells.Add(splits[i].InsPayAmt.ToString("F"));
				if(splits[i].ClaimPaymentNum==ClaimPaymentCur.ClaimPaymentNum){
					gridMain.SetSelected(i,true);
					splitTot+=splits[i].InsPayAmt;
				}
				if(splits[i].ClaimNum==OriginatingClaimNum){
					row.Bold=true;
				}  
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			textAmount.Text=splitTot.ToString("F");
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			splitTot=0;
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				splitTot+=splits[gridMain.SelectedIndices[i]].InsPayAmt;
			}
			textAmount.Text=splitTot.ToString("F");
		}

		private void checkShowUn_Click(object sender, System.EventArgs e) {
			FillGrid();
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
			for(int i=0;i<splits.Count;i++){
				if(splits[i].ClaimPaymentNum==ClaimPaymentCur.ClaimPaymentNum){
					SecurityLogs.MakeLogEntry(Permissions.InsPayEdit,splits[i].PatNum,
						"Delete for patient: "
						+Patients.GetLim(splits[i].PatNum).GetNameLF()+", "
						+Lan.g(this,"Total Amt: ")+ClaimPaymentCur.CheckAmt.ToString("c")+", "
						+Lan.g(this,"Claim Split: ")+splits[i].InsPayAmt.ToString("c"));
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
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
				if(!Security.IsAuthorized(Permissions.InsPayCreate,PIn.PDate(textDate.Text))){
					return;
				}
			}
			else{
				//Editing an old entry will already be blocked if the date was too old, and user will not be able to click OK button.
				//This catches it if user changed the date to be older.
				if(!Security.IsAuthorized(Permissions.InsPayEdit,PIn.PDate(textDate.Text))){
					return;
				}
			}
			if(comboClinic.SelectedIndex==0){
				ClaimPaymentCur.ClinicNum=0;
			}
			else{
				ClaimPaymentCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			}
			ClaimPaymentCur.CheckAmt=PIn.PDouble(textAmount.Text);
			ClaimPaymentCur.CheckDate=PIn.PDate(textDate.Text);
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
					ClaimProcs.SetForClaim(splits[i].ClaimNum,ClaimPaymentCur.ClaimPaymentNum,ClaimPaymentCur.CheckDate,false);
				}
			}
			DialogResult=DialogResult.OK;
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













