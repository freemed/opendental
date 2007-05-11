using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormDepositEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		private System.Windows.Forms.ListBox listPayType;
		private System.Windows.Forms.Label label2;
		private Deposit DepositCur;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butDelete;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBankAccountInfo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textAmount;
		private System.Windows.Forms.GroupBox groupSelect;
		private OpenDental.UI.Button butPrint;
		private OpenDental.UI.ODGrid gridPat;
		private OpenDental.UI.ODGrid gridIns;
		///<summary></summary>
		public bool IsNew;
		private ClaimPayment[] ClaimPayList;
		private OpenDental.ValidDate textDateStart;
		private System.Windows.Forms.Label label5;
		private OpenDental.UI.Button butRefresh;
		private Payment[] PatPayList;
		private ComboBox comboDepositAccount;
		private Label labelDepositAccount;
		private bool changed;
		private TextBox textDepositAccount;
		///<summary>Only used if linking to accounts</summary>
		private int[] DepositAccounts;

		///<summary></summary>
		public FormDepositEdit(Deposit depositCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			DepositCur=depositCur;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDepositEdit));
			this.groupSelect = new System.Windows.Forms.GroupBox();
			this.butRefresh = new OpenDental.UI.Button();
			this.textDateStart = new OpenDental.ValidDate();
			this.label5 = new System.Windows.Forms.Label();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.listPayType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBankAccountInfo = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textAmount = new System.Windows.Forms.TextBox();
			this.comboDepositAccount = new System.Windows.Forms.ComboBox();
			this.labelDepositAccount = new System.Windows.Forms.Label();
			this.gridIns = new OpenDental.UI.ODGrid();
			this.butPrint = new OpenDental.UI.Button();
			this.textDate = new OpenDental.ValidDate();
			this.butDelete = new OpenDental.UI.Button();
			this.gridPat = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textDepositAccount = new System.Windows.Forms.TextBox();
			this.groupSelect.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupSelect
			// 
			this.groupSelect.Controls.Add(this.butRefresh);
			this.groupSelect.Controls.Add(this.textDateStart);
			this.groupSelect.Controls.Add(this.label5);
			this.groupSelect.Controls.Add(this.comboClinic);
			this.groupSelect.Controls.Add(this.labelClinic);
			this.groupSelect.Controls.Add(this.listPayType);
			this.groupSelect.Controls.Add(this.label2);
			this.groupSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupSelect.Location = new System.Drawing.Point(602,276);
			this.groupSelect.Name = "groupSelect";
			this.groupSelect.Size = new System.Drawing.Size(204,344);
			this.groupSelect.TabIndex = 99;
			this.groupSelect.TabStop = false;
			this.groupSelect.Text = "Show";
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(13,307);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(75,26);
			this.butRefresh.TabIndex = 106;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(14,36);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(94,20);
			this.textDateStart.TabIndex = 105;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(14,19);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(118,15);
			this.label5.TabIndex = 104;
			this.label5.Text = "Start Date";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(14,75);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(180,21);
			this.comboClinic.TabIndex = 94;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(14,59);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(102,15);
			this.labelClinic.TabIndex = 93;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listPayType
			// 
			this.listPayType.Location = new System.Drawing.Point(14,127);
			this.listPayType.Name = "listPayType";
			this.listPayType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listPayType.Size = new System.Drawing.Size(134,173);
			this.listPayType.TabIndex = 96;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14,105);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(171,18);
			this.label2.TabIndex = 97;
			this.label2.Text = "Patient Payment Types";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(602,8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95,15);
			this.label1.TabIndex = 102;
			this.label1.Text = "Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(602,95);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(127,18);
			this.label3.TabIndex = 104;
			this.label3.Text = "Bank Account Info";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textBankAccountInfo
			// 
			this.textBankAccountInfo.Location = new System.Drawing.Point(602,116);
			this.textBankAccountInfo.Multiline = true;
			this.textBankAccountInfo.Name = "textBankAccountInfo";
			this.textBankAccountInfo.Size = new System.Drawing.Size(289,75);
			this.textBankAccountInfo.TabIndex = 105;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(602,49);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(127,18);
			this.label4.TabIndex = 106;
			this.label4.Text = "Amount";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(602,70);
			this.textAmount.Name = "textAmount";
			this.textAmount.ReadOnly = true;
			this.textAmount.Size = new System.Drawing.Size(94,20);
			this.textAmount.TabIndex = 107;
			// 
			// comboDepositAccount
			// 
			this.comboDepositAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDepositAccount.FormattingEnabled = true;
			this.comboDepositAccount.Location = new System.Drawing.Point(602,216);
			this.comboDepositAccount.Name = "comboDepositAccount";
			this.comboDepositAccount.Size = new System.Drawing.Size(289,21);
			this.comboDepositAccount.TabIndex = 110;
			// 
			// labelDepositAccount
			// 
			this.labelDepositAccount.Location = new System.Drawing.Point(602,196);
			this.labelDepositAccount.Name = "labelDepositAccount";
			this.labelDepositAccount.Size = new System.Drawing.Size(289,18);
			this.labelDepositAccount.TabIndex = 111;
			this.labelDepositAccount.Text = "Deposit into Account";
			this.labelDepositAccount.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// gridIns
			// 
			this.gridIns.HScrollVisible = false;
			this.gridIns.Location = new System.Drawing.Point(8,319);
			this.gridIns.Name = "gridIns";
			this.gridIns.ScrollValue = 0;
			this.gridIns.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridIns.Size = new System.Drawing.Size(584,301);
			this.gridIns.TabIndex = 109;
			this.gridIns.Title = "Insurance Payments";
			this.gridIns.TranslationName = "TableDepositSlipIns";
			this.gridIns.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridIns_CellClick);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(583,631);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(81,26);
			this.butPrint.TabIndex = 108;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(602,25);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(94,20);
			this.textDate.TabIndex = 103;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(7,631);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(85,26);
			this.butDelete.TabIndex = 101;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// gridPat
			// 
			this.gridPat.HScrollVisible = false;
			this.gridPat.Location = new System.Drawing.Point(8,12);
			this.gridPat.Name = "gridPat";
			this.gridPat.ScrollValue = 0;
			this.gridPat.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridPat.Size = new System.Drawing.Size(584,299);
			this.gridPat.TabIndex = 100;
			this.gridPat.Title = "Patient Payments";
			this.gridPat.TranslationName = "TableDepositSlipPat";
			this.gridPat.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPat_CellClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(712,631);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(805,631);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textDepositAccount
			// 
			this.textDepositAccount.Location = new System.Drawing.Point(602,241);
			this.textDepositAccount.Name = "textDepositAccount";
			this.textDepositAccount.ReadOnly = true;
			this.textDepositAccount.Size = new System.Drawing.Size(289,20);
			this.textDepositAccount.TabIndex = 112;
			// 
			// FormDepositEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(897,667);
			this.Controls.Add(this.textDepositAccount);
			this.Controls.Add(this.labelDepositAccount);
			this.Controls.Add(this.comboDepositAccount);
			this.Controls.Add(this.gridIns);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBankAccountInfo);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.gridPat);
			this.Controls.Add(this.groupSelect);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDepositEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Deposit Slip";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormDepositEdit_Closing);
			this.Load += new System.EventHandler(this.FormDepositEdit_Load);
			this.groupSelect.ResumeLayout(false);
			this.groupSelect.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormDepositEdit_Load(object sender, System.EventArgs e) {
			if(IsNew) {
				if(!Security.IsAuthorized(Permissions.DepositSlips,DateTime.Today)){
					//we will check the date again when saving
					DialogResult=DialogResult.Cancel;
					return;
				}
			}
			else {
				//We enforce security here based on date displayed, not date entered
				if(!Security.IsAuthorized(Permissions.DepositSlips,DepositCur.DateDeposit)){
					butOK.Enabled=false;
					butDelete.Enabled=false;
				}
			}
			if(IsNew){
				textDateStart.Text=PIn.PDate(PrefB.GetString("DateDepositsStarted")).ToShortDateString();
				if(PrefB.GetBool("EasyNoClinics")){
					comboClinic.Visible=false;
					labelClinic.Visible=false;
				}
				comboClinic.Items.Clear();
				comboClinic.Items.Add(Lan.g(this,"all"));
				comboClinic.SelectedIndex=0;
				for(int i=0;i<Clinics.List.Length;i++){
					comboClinic.Items.Add(Clinics.List[i].Description);
				}
				for(int i=0;i<DefB.Short[(int)DefCat.PaymentTypes].Length;i++){
					listPayType.Items.Add(DefB.Short[(int)DefCat.PaymentTypes][i].ItemName);
					listPayType.SetSelected(i,true);
				}
				textDepositAccount.Visible=false;//this is never visible for new. It's a description if already attached.
				if(Accounts.DepositsLinked()) {
					DepositAccounts=Accounts.GetDepositAccounts();
					for(int i=0;i<DepositAccounts.Length;i++){
						comboDepositAccount.Items.Add(Accounts.GetDescript(DepositAccounts[i]));
					}
					comboDepositAccount.SelectedIndex=0;
				}
				else{
					labelDepositAccount.Visible=false;
					comboDepositAccount.Visible=false;
				}
			}
			else{
				groupSelect.Visible=false;
				gridIns.SelectionMode=GridSelectionMode.None;
				gridPat.SelectionMode=GridSelectionMode.None;
				//we never again let user change the deposit linking again from here.
				//They need to detach it from within the transaction
				//Might be enhanced later to allow, but that's very complex.
				Transaction trans=Transactions.GetAttachedToDeposit(DepositCur.DepositNum);
				if(trans==null){
					labelDepositAccount.Visible=false;
					comboDepositAccount.Visible=false;
					textDepositAccount.Visible=false;
				}
				else{
					comboDepositAccount.Enabled=false;
					labelDepositAccount.Text=Lan.g(this,"Deposited into Account");
					ArrayList jeAL=JournalEntries.GetForTrans(trans.TransactionNum);
					for(int i=0;i<jeAL.Count;i++){
						if(Accounts.GetAccount(((JournalEntry)jeAL[i]).AccountNum).AcctType==AccountType.Asset){
							comboDepositAccount.Items.Add(Accounts.GetDescript(((JournalEntry)jeAL[i]).AccountNum));
							comboDepositAccount.SelectedIndex=0;
							textDepositAccount.Text=((JournalEntry)jeAL[i]).DateDisplayed.ToShortDateString()
								+" "+((JournalEntry)jeAL[i]).DebitAmt.ToString("c");
							break;
						}
					}
				}
			}
			textDate.Text=DepositCur.DateDeposit.ToShortDateString();
			textAmount.Text=DepositCur.Amount.ToString("F");
			textBankAccountInfo.Text=DepositCur.BankAccountInfo;
			FillGrids();
			if(IsNew){
				gridPat.SetSelected(true);
				gridIns.SetSelected(true);
			}
			ComputeAmt();
		}

		///<summary></summary>
		private void FillGrids(){
			if(IsNew){
				DateTime dateStart=PIn.PDate(textDateStart.Text);
				int clinicNum=0;
				if(comboClinic.SelectedIndex!=0){
					clinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
				}
				int[] payTypes=new int[listPayType.SelectedIndices.Count];
				for(int i=0;i<payTypes.Length;i++){
					payTypes[i]=DefB.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndices[i]].DefNum;
				}
				PatPayList=Payments.GetForDeposit(dateStart,clinicNum,payTypes);
				ClaimPayList=ClaimPayments.GetForDeposit(dateStart,clinicNum);
			}
			else{
				PatPayList=Payments.GetForDeposit(DepositCur.DepositNum);
				ClaimPayList=ClaimPayments.GetForDeposit(DepositCur.DepositNum);
			}
			//Fill Patient Payment Grid---------------------------------------
			ArrayList patNumAL=new ArrayList();
			for(int i=0;i<PatPayList.Length;i++){
				patNumAL.Add(PatPayList[i].PatNum);
			}
			int[] patNums=new int[patNumAL.Count];
			patNumAL.CopyTo(patNums);
			Patient[] pats=Patients.GetMultPats(patNums);
			gridPat.BeginUpdate();
			gridPat.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableDepositSlipPat","Date"),80);
			gridPat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableDepositSlipPat","Patient"),130);
			gridPat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableDepositSlipPat","Type"),90);
			gridPat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableDepositSlipPat","Check Number"),95);
			gridPat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableDepositSlipPat","Bank-Branch"),80);
			gridPat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableDepositSlipPat","Amount"),80);
			gridPat.Columns.Add(col);
			gridPat.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			for(int i=0;i<PatPayList.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(PatPayList[i].PayDate.ToShortDateString());
				row.Cells.Add(Patients.GetOnePat(pats,PatPayList[i].PatNum).GetNameLF());
				row.Cells.Add(DefB.GetName(DefCat.PaymentTypes,PatPayList[i].PayType));
				row.Cells.Add(PatPayList[i].CheckNum);
				row.Cells.Add(PatPayList[i].BankBranch);
				row.Cells.Add(PatPayList[i].PayAmt.ToString("F"));
				gridPat.Rows.Add(row);
			}
			gridPat.EndUpdate();
			//Fill Insurance Payment Grid-------------------------------------
			gridIns.BeginUpdate();
			gridIns.Columns.Clear();
			col=new ODGridColumn(Lan.g("TableDepositSlipIns","Date"),80);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableDepositSlipIns","Carrier"),220);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableDepositSlipIns","Check Number"),95);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableDepositSlipIns","Bank-Branch"),80);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableDepositSlipIns","Amount"),90);
			gridIns.Columns.Add(col);
			gridIns.Rows.Clear();
			for(int i=0;i<ClaimPayList.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(ClaimPayList[i].CheckDate.ToShortDateString());
				row.Cells.Add(ClaimPayList[i].CarrierName);
				row.Cells.Add(ClaimPayList[i].CheckNum);
				row.Cells.Add(ClaimPayList[i].BankBranch);
				row.Cells.Add(ClaimPayList[i].CheckAmt.ToString("F"));
				gridIns.Rows.Add(row);
			}
			gridIns.EndUpdate();
		}

		///<summary>Usually run after any selected items changed. Recalculates amt based on selected items.</summary>
		private void ComputeAmt(){
			if(!IsNew){
				return;
			}
			double amount=0;
			for(int i=0;i<gridPat.SelectedIndices.Length;i++){
				amount+=PatPayList[gridPat.SelectedIndices[i]].PayAmt;
			}
			for(int i=0;i<gridIns.SelectedIndices.Length;i++){
				amount+=ClaimPayList[gridIns.SelectedIndices[i]].CheckAmt;
			}
			textAmount.Text=amount.ToString("F");
			DepositCur.Amount=amount;
		}

		private void gridPat_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			ComputeAmt();
		}

		private void gridIns_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			ComputeAmt();
		}

		///<summary>Remember that this can only happen if IsNew</summary>
		private void butRefresh_Click(object sender, System.EventArgs e) {
			if(textDateStart.errorProvider1.GetError(textDate)!=""){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			FillGrids();
			gridPat.SetSelected(true);
			gridIns.SetSelected(true);
			ComputeAmt();
			if(comboClinic.SelectedIndex==0){
				textBankAccountInfo.Text=PrefB.GetString("PracticeBankNumber");
			}
			else{
				textBankAccountInfo.Text=Clinics.List[comboClinic.SelectedIndex-1].BankNumber;
			}
			if(Prefs.UpdateString("DateDepositsStarted",POut.PDate(PIn.PDate(textDateStart.Text),false))){
				changed=true;
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			//If deposit is attached to a transaction which is more than 48 hours old, then not allowed to delete.
			//This is hard coded.  User would have to delete or detach from within transaction rather than here.
			Transaction trans=Transactions.GetAttachedToDeposit(DepositCur.DepositNum);
			if(trans != null){
				if(trans.DateTimeEntry < MiscData.GetNowDateTime().AddDays(-2) ){
					MsgBox.Show(this,"Not allowed to delete.  This deposit is already attached to an accounting transaction.  You will need to detach it from within the accounting section of the program.");
					return;
				}
				if(Transactions.IsReconciled(trans)) {
					MsgBox.Show(this,"Not allowed to delete.  This deposit is attached to an accounting transaction that has been reconciled.  You will need to detach it from within the accounting section of the program.");
					return;
				}
				try{
					Transactions.Delete(trans);
				}
				catch(ApplicationException ex){
					MessageBox.Show(ex.Message);
					return;
				}
			}
			if(!MsgBox.Show(this,true,"Delete?")){
				return;
			}
			Deposits.Delete(DepositCur);
			DialogResult=DialogResult.OK;
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			if(textDate.errorProvider1.GetError(textDate)!="") {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(IsNew){
				if(!SaveToDB()) {
					return;
				}
			}
			else{//not new
				//Only allowed to change date and bank account info, NOT attached checks.
				//We enforce security here based on date displayed, not date entered.
				//If user is trying to change date without permission:
				DateTime date=PIn.PDate(textDate.Text);
				if(Security.IsAuthorized(Permissions.DepositSlips,date,true)){
					if(!SaveToDB()) {
						return;
					}
				}
				//if security.NotAuthorized, then it simply skips the save process before printing
			}
			//refresh the lists because some items may not be highlighted
			PatPayList=Payments.GetForDeposit(DepositCur.DepositNum);
			ClaimPayList=ClaimPayments.GetForDeposit(DepositCur.DepositNum);
			Queries.TableQ=new DataTable();
			for(int i=0;i<5;i++){ //add 5 columns
				Queries.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			Queries.CurReport=new ReportOld();
			Queries.CurReport.ColTotal=new double[Queries.TableQ.Columns.Count];
			DataRow row;
			ArrayList patNumAL=new ArrayList();
			for(int i=0;i<PatPayList.Length;i++){
				patNumAL.Add(PatPayList[i].PatNum);
			}
			int[] patNums=new int[patNumAL.Count];
			patNumAL.CopyTo(patNums);
			Patient[] pats=Patients.GetMultPats(patNums);
			for(int i=0;i<PatPayList.Length;i++){
				row=Queries.TableQ.NewRow();
				row[0]=PatPayList[i].PayDate.ToShortDateString();
				row[1]=Patients.GetOnePat(pats,PatPayList[i].PatNum).GetNameLF();
				row[2]=PatPayList[i].CheckNum;
				row[3]=PatPayList[i].BankBranch;
				row[4]=PatPayList[i].PayAmt.ToString("F");
				Queries.TableQ.Rows.Add(row);
				Queries.CurReport.ColTotal[4]+=PatPayList[i].PayAmt;
			}
			for(int i=0;i<ClaimPayList.Length;i++){
				row=Queries.TableQ.NewRow();
				row[0]=ClaimPayList[i].CheckDate.ToShortDateString();
				row[1]=ClaimPayList[i].CarrierName;
				row[2]=ClaimPayList[i].CheckNum;
				row[3]=ClaimPayList[i].BankBranch;
				row[4]=ClaimPayList[i].CheckAmt.ToString("F");
				Queries.TableQ.Rows.Add(row);
				Queries.CurReport.ColTotal[4]+=ClaimPayList[i].CheckAmt;
			}
			//done filling now set up table
			Queries.CurReport.ColWidth=new int[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColPos=new int[Queries.TableQ.Columns.Count+1];
			Queries.CurReport.ColPos[0]=0;
			Queries.CurReport.ColCaption=new string[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColAlign=new HorizontalAlignment[Queries.TableQ.Columns.Count];
			FormQuery FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.ResetGrid();//necessary won't work without
			Queries.CurReport.Title="Deposit Slip";
			Queries.CurReport.SubTitle=new string[2];
			Queries.CurReport.SubTitle[0]=((Pref)PrefB.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=DepositCur.DateDeposit.ToShortDateString();
			Queries.CurReport.Summary=new string[1];
			Queries.CurReport.Summary[0]=DepositCur.BankAccountInfo;
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=110;
			Queries.CurReport.ColPos[2]=260;
			Queries.CurReport.ColPos[3]=350;
			Queries.CurReport.ColPos[4]=440;
			Queries.CurReport.ColPos[5]=530;
			Queries.CurReport.ColCaption[0]="Date";
			Queries.CurReport.ColCaption[1]="Name";
			Queries.CurReport.ColCaption[2]="Check Number";
			Queries.CurReport.ColCaption[3]="Bank-Branch";
			Queries.CurReport.ColCaption[4]="Amount";
			Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;//this is imporant, since we don't want to insert the deposit slip twice.
		}

		///<summary>Saves the selected rows to database.  MUST close window after this.</summary>
		private bool SaveToDB(){
			if(textDate.errorProvider1.GetError(textDate)!=""){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}
			//Prevent backdating----------------------------------------------------------------------------------------
			DateTime date=PIn.PDate(textDate.Text);
			if(IsNew) {
				if(!Security.IsAuthorized(Permissions.DepositSlips,date)) {
					return false;
				}
			}
			else {
				//We enforce security here based on date displayed, not date entered
				if(!Security.IsAuthorized(Permissions.DepositSlips,date)) {
					return false;
				}
			}
			DepositCur.DateDeposit=PIn.PDate(textDate.Text);
			//amount already handled.
			DepositCur.BankAccountInfo=PIn.PString(textBankAccountInfo.Text);
			if(IsNew){
				Deposits.Insert(DepositCur);
				if(Accounts.DepositsLinked() && DepositCur.Amount>0){
					//create a transaction here
					Transaction trans=new Transaction();
					trans.DepositNum=DepositCur.DepositNum;
					trans.UserNum=Security.CurUser.UserNum;
					Transactions.Insert(trans);
					//first the deposit entry
					JournalEntry je=new JournalEntry();
					je.AccountNum=DepositAccounts[comboDepositAccount.SelectedIndex];
					je.CheckNumber=Lan.g(this,"DEP");
					je.DateDisplayed=DepositCur.DateDeposit;//it would be nice to add security here.
					je.DebitAmt=DepositCur.Amount;
					je.Memo=Lan.g(this,"Deposit");
					je.Splits=Accounts.GetDescript(PrefB.GetInt("AccountingIncomeAccount"));
					je.TransactionNum=trans.TransactionNum;
					JournalEntries.Insert(je);
					//then, the income entry
					je=new JournalEntry();
					je.AccountNum=PrefB.GetInt("AccountingIncomeAccount");
					//je.CheckNumber=;
					je.DateDisplayed=DepositCur.DateDeposit;//it would be nice to add security here.
					je.CreditAmt=DepositCur.Amount;
					je.Memo=Lan.g(this,"Deposit");
					je.Splits=Accounts.GetDescript(DepositAccounts[comboDepositAccount.SelectedIndex]);
					je.TransactionNum=trans.TransactionNum;
					JournalEntries.Insert(je);
				}
			}
			else{
				Deposits.Update(DepositCur);
			}
			if(IsNew){//never allowed to change or attach more checks after initial creation of deposit slip
				for(int i=0;i<gridPat.SelectedIndices.Length;i++){
					PatPayList[gridPat.SelectedIndices[i]].DepositNum=DepositCur.DepositNum;
					Payments.Update(PatPayList[gridPat.SelectedIndices[i]]);
				}
				for(int i=0;i<gridIns.SelectedIndices.Length;i++){
					ClaimPayList[gridIns.SelectedIndices[i]].DepositNum=DepositCur.DepositNum;
					ClaimPayments.Update(ClaimPayList[gridIns.SelectedIndices[i]]);
				}
			}
			if(IsNew) {
				SecurityLogs.MakeLogEntry(Permissions.DepositSlips,0,
					DepositCur.DateDeposit.ToShortDateString()+" New "+DepositCur.Amount.ToString("c"));
			}
			else {
				SecurityLogs.MakeLogEntry(Permissions.AdjustmentEdit,0,
					DepositCur.DateDeposit.ToShortDateString()+" "+DepositCur.Amount.ToString("c"));
			}
			return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!SaveToDB()){
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			if(IsNew){
				//no need to worry about deposit links, since none made yet.
				Deposits.Delete(DepositCur);
			}
			DialogResult=DialogResult.Cancel;
		}

		private void FormDepositEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
		}

	

		

		

		

		

		

		

		


	}
}





















