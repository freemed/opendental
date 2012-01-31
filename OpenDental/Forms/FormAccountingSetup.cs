using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.Bridges;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormAccountingSetup : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private Label label1;
		private OpenDental.UI.Button butAdd;
		private GroupBox groupBox1;
		private Label label2;
		private OpenDental.UI.Button butChange;
		private Label label3;
		private TextBox textAccountInc;
		private OpenDental.UI.Button butRemove;
		private ListBox listAccountsDep;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary>Each item in the array list is an int for an AccountNum for the deposit accounts.</summary>
		private ArrayList depAL;
		private GroupBox groupAutomaticPayment;
		private OpenDental.UI.Button butChangeCash;
		private Label label4;
		private TextBox textAccountCashInc;
		private Label label5;
		private OpenDental.UI.Button butAddPay;
		private long PickedDepAccountNum;
		//private ArrayList cashAL;
		private long PickedPayAccountNum;
		private OpenDental.UI.ODGrid gridMain;
		private ListBox listSoftware;
		private Label labelSoftware;
		private Panel panelQB;
		private Panel panelOD;
		private GroupBox groupQB;
		private Label labelDepositsQB;
		private ListBox listBoxAccountsQB;
		private UI.Button butRemoveAccountsQB;
		private UI.Button butChangeIncomeAccountQB;
		private TextBox textIncomeAccountQB;
		private UI.Button butAddAccountsQB;
		private Label label7;
		private UI.Button butConnectQB;
		private UI.Button butBrowseQB;
		private Label labelCompanyFile;
		private TextBox textCompanyFileQB;
		private Label labelWarning;
		private Label labelIncomeAccountQB;
		private Label labelConnectQB;
		///<summary>Arraylist of AccountingAutoPays.</summary>
		private List<AccountingAutoPay> payList;
		private AccountingSoftware AcctSoftware;
		private List<string> listAccountsQB;

		///<summary></summary>
		public FormAccountingSetup()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAccountingSetup));
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listAccountsDep = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textAccountInc = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupAutomaticPayment = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textAccountCashInc = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.listSoftware = new System.Windows.Forms.ListBox();
			this.labelSoftware = new System.Windows.Forms.Label();
			this.panelQB = new System.Windows.Forms.Panel();
			this.groupQB = new System.Windows.Forms.GroupBox();
			this.labelIncomeAccountQB = new System.Windows.Forms.Label();
			this.labelWarning = new System.Windows.Forms.Label();
			this.labelCompanyFile = new System.Windows.Forms.Label();
			this.textCompanyFileQB = new System.Windows.Forms.TextBox();
			this.listBoxAccountsQB = new System.Windows.Forms.ListBox();
			this.textIncomeAccountQB = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.labelDepositsQB = new System.Windows.Forms.Label();
			this.panelOD = new System.Windows.Forms.Panel();
			this.labelConnectQB = new System.Windows.Forms.Label();
			this.butRemove = new OpenDental.UI.Button();
			this.butChange = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.butConnectQB = new OpenDental.UI.Button();
			this.butBrowseQB = new OpenDental.UI.Button();
			this.butRemoveAccountsQB = new OpenDental.UI.Button();
			this.butChangeIncomeAccountQB = new OpenDental.UI.Button();
			this.butAddAccountsQB = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butChangeCash = new OpenDental.UI.Button();
			this.butAddPay = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupAutomaticPayment.SuspendLayout();
			this.panelQB.SuspendLayout();
			this.groupQB.SuspendLayout();
			this.panelOD.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 53);
			this.label1.TabIndex = 2;
			this.label1.Text = "User will get to pick from this list of accounts to deposit into";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.listAccountsDep);
			this.groupBox1.Controls.Add(this.butRemove);
			this.groupBox1.Controls.Add(this.butChange);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textAccountInc);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.butAdd);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(519, 222);
			this.groupBox1.TabIndex = 32;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Automatic Deposit Entries";
			// 
			// listAccountsDep
			// 
			this.listAccountsDep.FormattingEnabled = true;
			this.listAccountsDep.Location = new System.Drawing.Point(182, 61);
			this.listAccountsDep.Name = "listAccountsDep";
			this.listAccountsDep.Size = new System.Drawing.Size(230, 108);
			this.listAccountsDep.TabIndex = 37;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 179);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(168, 19);
			this.label3.TabIndex = 33;
			this.label3.Text = "Income Account";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAccountInc
			// 
			this.textAccountInc.Location = new System.Drawing.Point(182, 179);
			this.textAccountInc.Name = "textAccountInc";
			this.textAccountInc.ReadOnly = true;
			this.textAccountInc.Size = new System.Drawing.Size(230, 20);
			this.textAccountInc.TabIndex = 34;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(19, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(492, 27);
			this.label2.TabIndex = 32;
			this.label2.Text = "Every time a deposit is created, an accounting transaction will also be automatic" +
    "ally created.";
			// 
			// groupAutomaticPayment
			// 
			this.groupAutomaticPayment.Controls.Add(this.gridMain);
			this.groupAutomaticPayment.Controls.Add(this.butChangeCash);
			this.groupAutomaticPayment.Controls.Add(this.label4);
			this.groupAutomaticPayment.Controls.Add(this.textAccountCashInc);
			this.groupAutomaticPayment.Controls.Add(this.label5);
			this.groupAutomaticPayment.Controls.Add(this.butAddPay);
			this.groupAutomaticPayment.Location = new System.Drawing.Point(27, 280);
			this.groupAutomaticPayment.Name = "groupAutomaticPayment";
			this.groupAutomaticPayment.Size = new System.Drawing.Size(519, 353);
			this.groupAutomaticPayment.TabIndex = 33;
			this.groupAutomaticPayment.TabStop = false;
			this.groupAutomaticPayment.Text = "Automatic Payment Entries";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 309);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(168, 19);
			this.label4.TabIndex = 33;
			this.label4.Text = "Income Account";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAccountCashInc
			// 
			this.textAccountCashInc.Location = new System.Drawing.Point(182, 309);
			this.textAccountCashInc.Name = "textAccountCashInc";
			this.textAccountCashInc.ReadOnly = true;
			this.textAccountCashInc.Size = new System.Drawing.Size(230, 20);
			this.textAccountCashInc.TabIndex = 34;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(19, 26);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(492, 47);
			this.label5.TabIndex = 32;
			this.label5.Text = "Some payment types do not use deposit slips.  An example is cashbox entries.  For" +
    " these types, an accounting transaction will be automatically created each time " +
    "a patient payment is entered.";
			// 
			// listSoftware
			// 
			this.listSoftware.FormattingEnabled = true;
			this.listSoftware.Items.AddRange(new object[] {
            "None",
            "Open Dental",
            "QuickBooks"});
			this.listSoftware.Location = new System.Drawing.Point(580, 53);
			this.listSoftware.Name = "listSoftware";
			this.listSoftware.Size = new System.Drawing.Size(107, 43);
			this.listSoftware.TabIndex = 34;
			this.listSoftware.Click += new System.EventHandler(this.listSoftware_Click);
			// 
			// labelSoftware
			// 
			this.labelSoftware.Location = new System.Drawing.Point(552, 31);
			this.labelSoftware.Name = "labelSoftware";
			this.labelSoftware.Size = new System.Drawing.Size(135, 19);
			this.labelSoftware.TabIndex = 38;
			this.labelSoftware.Text = "Accounting Software";
			this.labelSoftware.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelQB
			// 
			this.panelQB.Controls.Add(this.groupQB);
			this.panelQB.Location = new System.Drawing.Point(123, 53);
			this.panelQB.Name = "panelQB";
			this.panelQB.Size = new System.Drawing.Size(152, 129);
			this.panelQB.TabIndex = 39;
			// 
			// groupQB
			// 
			this.groupQB.Controls.Add(this.labelConnectQB);
			this.groupQB.Controls.Add(this.labelIncomeAccountQB);
			this.groupQB.Controls.Add(this.labelWarning);
			this.groupQB.Controls.Add(this.butConnectQB);
			this.groupQB.Controls.Add(this.butBrowseQB);
			this.groupQB.Controls.Add(this.labelCompanyFile);
			this.groupQB.Controls.Add(this.textCompanyFileQB);
			this.groupQB.Controls.Add(this.listBoxAccountsQB);
			this.groupQB.Controls.Add(this.butRemoveAccountsQB);
			this.groupQB.Controls.Add(this.butChangeIncomeAccountQB);
			this.groupQB.Controls.Add(this.textIncomeAccountQB);
			this.groupQB.Controls.Add(this.butAddAccountsQB);
			this.groupQB.Controls.Add(this.label7);
			this.groupQB.Controls.Add(this.labelDepositsQB);
			this.groupQB.Location = new System.Drawing.Point(0, 0);
			this.groupQB.Name = "groupQB";
			this.groupQB.Size = new System.Drawing.Size(519, 606);
			this.groupQB.TabIndex = 0;
			this.groupQB.TabStop = false;
			this.groupQB.Text = "QuickBooks";
			// 
			// labelIncomeAccountQB
			// 
			this.labelIncomeAccountQB.Location = new System.Drawing.Point(12, 273);
			this.labelIncomeAccountQB.Name = "labelIncomeAccountQB";
			this.labelIncomeAccountQB.Size = new System.Drawing.Size(168, 19);
			this.labelIncomeAccountQB.TabIndex = 51;
			this.labelIncomeAccountQB.Text = "Income Account";
			this.labelIncomeAccountQB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelWarning
			// 
			this.labelWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelWarning.Location = new System.Drawing.Point(35, 569);
			this.labelWarning.Name = "labelWarning";
			this.labelWarning.Size = new System.Drawing.Size(448, 27);
			this.labelWarning.TabIndex = 50;
			this.labelWarning.Text = "(Open Dental will run faster if your QuickBooks company file is open in the backg" +
    "round)";
			this.labelWarning.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelCompanyFile
			// 
			this.labelCompanyFile.Location = new System.Drawing.Point(12, 22);
			this.labelCompanyFile.Name = "labelCompanyFile";
			this.labelCompanyFile.Size = new System.Drawing.Size(105, 19);
			this.labelCompanyFile.TabIndex = 46;
			this.labelCompanyFile.Text = "Company File";
			this.labelCompanyFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCompanyFileQB
			// 
			this.textCompanyFileQB.Location = new System.Drawing.Point(123, 22);
			this.textCompanyFileQB.Name = "textCompanyFileQB";
			this.textCompanyFileQB.ReadOnly = true;
			this.textCompanyFileQB.Size = new System.Drawing.Size(289, 20);
			this.textCompanyFileQB.TabIndex = 47;
			// 
			// listAccountsQB
			// 
			this.listBoxAccountsQB.FormattingEnabled = true;
			this.listBoxAccountsQB.Location = new System.Drawing.Point(182, 145);
			this.listBoxAccountsQB.Name = "listAccountsQB";
			this.listBoxAccountsQB.Size = new System.Drawing.Size(230, 108);
			this.listBoxAccountsQB.TabIndex = 44;
			// 
			// textIncomeAccountQB
			// 
			this.textIncomeAccountQB.Location = new System.Drawing.Point(182, 272);
			this.textIncomeAccountQB.Name = "textIncomeAccountQB";
			this.textIncomeAccountQB.ReadOnly = true;
			this.textIncomeAccountQB.Size = new System.Drawing.Size(230, 20);
			this.textIncomeAccountQB.TabIndex = 41;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(3, 145);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(177, 53);
			this.label7.TabIndex = 38;
			this.label7.Text = "User will get to pick from this list of accounts to deposit to.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelDepositsQB
			// 
			this.labelDepositsQB.Location = new System.Drawing.Point(12, 115);
			this.labelDepositsQB.Name = "labelDepositsQB";
			this.labelDepositsQB.Size = new System.Drawing.Size(492, 27);
			this.labelDepositsQB.TabIndex = 33;
			this.labelDepositsQB.Text = "Every time a deposit is created, a deposit will be created within QuickBooks usin" +
    "g these settings.";
			this.labelDepositsQB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// panelOD
			// 
			this.panelOD.Controls.Add(this.groupBox1);
			this.panelOD.Location = new System.Drawing.Point(348, 53);
			this.panelOD.Name = "panelOD";
			this.panelOD.Size = new System.Drawing.Size(161, 142);
			this.panelOD.TabIndex = 40;
			// 
			// labelConnectQB
			// 
			this.labelConnectQB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelConnectQB.Location = new System.Drawing.Point(15, 45);
			this.labelConnectQB.Name = "labelConnectQB";
			this.labelConnectQB.Size = new System.Drawing.Size(397, 46);
			this.labelConnectQB.TabIndex = 52;
			this.labelConnectQB.Text = "Push the connect button with QuickBooks running in the background if this is your" +
    " first time using QuickBooks with Open Dental. (Only need to do this once)";
			this.labelConnectQB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butRemove
			// 
			this.butRemove.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRemove.Autosize = true;
			this.butRemove.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRemove.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRemove.CornerRadius = 4F;
			this.butRemove.Location = new System.Drawing.Point(418, 88);
			this.butRemove.Name = "butRemove";
			this.butRemove.Size = new System.Drawing.Size(75, 24);
			this.butRemove.TabIndex = 36;
			this.butRemove.Text = "Remove";
			this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
			// 
			// butChange
			// 
			this.butChange.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butChange.Autosize = true;
			this.butChange.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChange.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChange.CornerRadius = 4F;
			this.butChange.Location = new System.Drawing.Point(418, 176);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(75, 24);
			this.butChange.TabIndex = 35;
			this.butChange.Text = "Change";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Location = new System.Drawing.Point(418, 58);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 24);
			this.butAdd.TabIndex = 30;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butConnectQB
			// 
			this.butConnectQB.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butConnectQB.Autosize = true;
			this.butConnectQB.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butConnectQB.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butConnectQB.CornerRadius = 4F;
			this.butConnectQB.Location = new System.Drawing.Point(418, 56);
			this.butConnectQB.Name = "butConnectQB";
			this.butConnectQB.Size = new System.Drawing.Size(75, 24);
			this.butConnectQB.TabIndex = 49;
			this.butConnectQB.Text = "Connect";
			this.butConnectQB.Click += new System.EventHandler(this.butConnectQB_Click);
			// 
			// butBrowseQB
			// 
			this.butBrowseQB.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBrowseQB.Autosize = true;
			this.butBrowseQB.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseQB.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseQB.CornerRadius = 4F;
			this.butBrowseQB.Location = new System.Drawing.Point(418, 19);
			this.butBrowseQB.Name = "butBrowseQB";
			this.butBrowseQB.Size = new System.Drawing.Size(75, 24);
			this.butBrowseQB.TabIndex = 48;
			this.butBrowseQB.Text = "Browse";
			this.butBrowseQB.Click += new System.EventHandler(this.butBrowseQB_Click);
			// 
			// butRemoveAccountsQB
			// 
			this.butRemoveAccountsQB.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRemoveAccountsQB.Autosize = true;
			this.butRemoveAccountsQB.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRemoveAccountsQB.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRemoveAccountsQB.CornerRadius = 4F;
			this.butRemoveAccountsQB.Location = new System.Drawing.Point(418, 172);
			this.butRemoveAccountsQB.Name = "butRemoveAccountsQB";
			this.butRemoveAccountsQB.Size = new System.Drawing.Size(75, 24);
			this.butRemoveAccountsQB.TabIndex = 43;
			this.butRemoveAccountsQB.Text = "Remove";
			this.butRemoveAccountsQB.Click += new System.EventHandler(this.butRemoveAccountsQB_Click);
			// 
			// butChangeIncomeAccountQB
			// 
			this.butChangeIncomeAccountQB.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butChangeIncomeAccountQB.Autosize = true;
			this.butChangeIncomeAccountQB.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChangeIncomeAccountQB.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChangeIncomeAccountQB.CornerRadius = 4F;
			this.butChangeIncomeAccountQB.Location = new System.Drawing.Point(418, 270);
			this.butChangeIncomeAccountQB.Name = "butChangeIncomeAccountQB";
			this.butChangeIncomeAccountQB.Size = new System.Drawing.Size(75, 24);
			this.butChangeIncomeAccountQB.TabIndex = 42;
			this.butChangeIncomeAccountQB.Text = "Change";
			this.butChangeIncomeAccountQB.Click += new System.EventHandler(this.butChangeIncomeAccountQB_Click);
			// 
			// butAddAccountsQB
			// 
			this.butAddAccountsQB.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddAccountsQB.Autosize = true;
			this.butAddAccountsQB.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddAccountsQB.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddAccountsQB.CornerRadius = 4F;
			this.butAddAccountsQB.Location = new System.Drawing.Point(418, 142);
			this.butAddAccountsQB.Name = "butAddAccountsQB";
			this.butAddAccountsQB.Size = new System.Drawing.Size(75, 24);
			this.butAddAccountsQB.TabIndex = 39;
			this.butAddAccountsQB.Text = "Add";
			this.butAddAccountsQB.Click += new System.EventHandler(this.butAddAccountsQB_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(22, 104);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(471, 177);
			this.gridMain.TabIndex = 40;
			this.gridMain.Title = "Auto Payment Entries";
			this.gridMain.TranslationName = "TableAccountingAutoPay";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butChangeCash
			// 
			this.butChangeCash.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butChangeCash.Autosize = true;
			this.butChangeCash.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChangeCash.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChangeCash.CornerRadius = 4F;
			this.butChangeCash.Location = new System.Drawing.Point(418, 306);
			this.butChangeCash.Name = "butChangeCash";
			this.butChangeCash.Size = new System.Drawing.Size(75, 24);
			this.butChangeCash.TabIndex = 35;
			this.butChangeCash.Text = "Change";
			this.butChangeCash.Click += new System.EventHandler(this.butChangeCash_Click);
			// 
			// butAddPay
			// 
			this.butAddPay.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddPay.Autosize = true;
			this.butAddPay.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddPay.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddPay.CornerRadius = 4F;
			this.butAddPay.Location = new System.Drawing.Point(418, 75);
			this.butAddPay.Name = "butAddPay";
			this.butAddPay.Size = new System.Drawing.Size(75, 24);
			this.butAddPay.TabIndex = 30;
			this.butAddPay.Text = "Add";
			this.butAddPay.Click += new System.EventHandler(this.butAddPay_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(607, 565);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(607, 606);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormAccountingSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(715, 657);
			this.Controls.Add(this.panelOD);
			this.Controls.Add(this.panelQB);
			this.Controls.Add(this.labelSoftware);
			this.Controls.Add(this.listSoftware);
			this.Controls.Add(this.groupAutomaticPayment);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAccountingSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Accounting";
			this.Load += new System.EventHandler(this.FormAccountingSetup_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupAutomaticPayment.ResumeLayout(false);
			this.groupAutomaticPayment.PerformLayout();
			this.panelQB.ResumeLayout(false);
			this.groupQB.ResumeLayout(false);
			this.groupQB.PerformLayout();
			this.panelOD.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAccountingSetup_Load(object sender,EventArgs e) {
			AcctSoftware=(AccountingSoftware)PrefC.GetInt(PrefName.AccountingSoftware);
			if(AcctSoftware==AccountingSoftware.QuickBooks) {
				PanelLayoutQB();
			}
			else {
				PanelLayoutOD();
			}
			listSoftware.SelectedIndex=(int)AcctSoftware;
		}

		private void FillDepList(){
			listAccountsDep.Items.Clear();
			for(int i=0;i<depAL.Count;i++){
				listAccountsDep.Items.Add(Accounts.GetDescript((long)depAL[i]));
			}
		}

		private void FillPayGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableAccountingAutoPay","Payment Type"),200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAccountingAutoPay","Pick List"),250);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<payList.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(DefC.GetName(DefCat.PaymentTypes,payList[i].PayType));
				row.Cells.Add(AccountingAutoPays.GetPickListDesc(payList[i]));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void FillAccountListQB() {
			listBoxAccountsQB.Items.Clear();
			for(int i=0;i<listAccountsQB.Count;i++) {
			  listBoxAccountsQB.Items.Add(listAccountsQB[i]);
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormAccountPick FormA=new FormAccountPick();
			FormA.ShowDialog();
			if(FormA.DialogResult!=DialogResult.OK) {
				return;
			}
			depAL.Add(FormA.SelectedAccount.AccountNum);
			FillDepList();
		}

		private void butRemove_Click(object sender,EventArgs e) {
			if(listAccountsDep.SelectedIndex==-1){
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			depAL.RemoveAt(listAccountsDep.SelectedIndex);
			FillDepList();
		}

		private void butChange_Click(object sender,EventArgs e) {
			FormAccountPick FormA=new FormAccountPick();
			FormA.ShowDialog();
			if(FormA.DialogResult!=DialogResult.OK) {
				return;
			}
			PickedDepAccountNum=FormA.SelectedAccount.AccountNum;
			textAccountInc.Text=Accounts.GetDescript(PickedDepAccountNum);
		}

		private void butBrowseQB_Click(object sender,EventArgs e) {
			OpenFileDialog fdlg=new OpenFileDialog();
			fdlg.Title="QuickBooks Company File";
			fdlg.InitialDirectory=@"C:\";
			fdlg.Filter="QuickBooks|*.qbw";
			fdlg.RestoreDirectory=true;
			if(fdlg.ShowDialog()==DialogResult.OK) {
				textCompanyFileQB.Text=fdlg.FileName;
			}
		}

		private void butConnectQB_Click(object sender,EventArgs e) {
			if(textCompanyFileQB.Text.Trim()=="") {
				MsgBox.Show(this,"Browse to your QuickBooks company file first.");
				return;
			}
			Cursor.Current=Cursors.WaitCursor;
			string result=QuickBooks.TestConnection(textCompanyFileQB.Text);
			Cursor.Current=Cursors.Default;
			MessageBox.Show(result);
		}

		private void butAddAccountsQB_Click(object sender,EventArgs e) {
			if(textCompanyFileQB.Text.Trim()=="") {
				MsgBox.Show(this,"Browse to your QuickBooks company file first.");
				return;
			}
			if(Prefs.UpdateString(PrefName.QuickBooksCompanyFile,textCompanyFileQB.Text)) {
				DataValid.SetInvalid(InvalidType.Prefs);
			}
			FormAccountPick FormA=new FormAccountPick();
			FormA.IsQuickBooks=true;
			FormA.ShowDialog();
			if(FormA.DialogResult!=DialogResult.OK) {
			  return;
			}
			listAccountsQB.AddRange(FormA.SelectedAccountsQB);
			FillAccountListQB();
		}

		private void butRemoveAccountsQB_Click(object sender,EventArgs e) {
			if(listBoxAccountsQB.SelectedIndex==-1){
			  MsgBox.Show(this,"Please select an item first.");
			  return;
			}
			listAccountsQB.RemoveAt(listBoxAccountsQB.SelectedIndex);
			FillAccountListQB();
		}

		private void butChangeIncomeAccountQB_Click(object sender,EventArgs e) {
			if(textCompanyFileQB.Text.Trim()=="") {
				MsgBox.Show(this,"Browse to your QuickBooks company file first.");
				return;
			}
			if(Prefs.UpdateString(PrefName.QuickBooksCompanyFile,textCompanyFileQB.Text)) {
				DataValid.SetInvalid(InvalidType.Prefs);
			}
			FormAccountPick FormA=new FormAccountPick();
			FormA.IsQuickBooks=true;
			FormA.ShowDialog();
			if(FormA.DialogResult!=DialogResult.OK) {
			  return;
			}
			listAccountsQB.Add(FormA.SelectedAccountsQB[0]);
			textIncomeAccountQB.Text=FormA.SelectedAccountsQB[0];
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormAccountingAutoPayEdit FormA=new FormAccountingAutoPayEdit();
			FormA.AutoPayCur=payList[e.Row];
			FormA.ShowDialog();
			if(FormA.AutoPayCur==null){//user deleted
				payList.RemoveAt(e.Row);
			}
			FillPayGrid();
		}

		private void listSoftware_Click(object sender,EventArgs e) {
			int index=listSoftware.SelectedIndex;
			if(index==-1){
				return;
			}
			else if(index==2){//QuickBooks
				PanelLayoutQB();
				return;
			}
			else {
				PanelLayoutOD();
				return;
			}
		}

		///<summary>Changes the window visually for Open Dental accounting users.</summary>
		private void PanelLayoutOD() {
			groupAutomaticPayment.Visible=true;
			panelQB.Visible=false;
			panelOD.Visible=true;
			panelOD.Location=new Point(27,27);
			panelOD.Size=new Size(519,222);
			groupAutomaticPayment.Visible=true;
			//Update the grids for this layout.
			string depStr=PrefC.GetString(PrefName.AccountingDepositAccounts);
			string[] depStrArray=depStr.Split(new char[] { ',' });
			depAL=new ArrayList();
			for(int i=0;i<depStrArray.Length;i++) {
				if(depStrArray[i]=="") {
					continue;
				}
				depAL.Add(PIn.Long(depStrArray[i]));
			}
			FillDepList();
			PickedDepAccountNum=PrefC.GetLong(PrefName.AccountingIncomeAccount);
			textAccountInc.Text=Accounts.GetDescript(PickedDepAccountNum);
			//pay----------------------------------------------------------
			payList=new List<AccountingAutoPay>();
			payList.AddRange(AccountingAutoPays.Listt);//Count might be 0
			FillPayGrid();
			PickedPayAccountNum=PrefC.GetLong(PrefName.AccountingCashIncomeAccount);
			textAccountCashInc.Text=Accounts.GetDescript(PickedPayAccountNum);
		}

		///<summary>Changes the window visually for QuickBooks users.</summary>
		private void PanelLayoutQB() {
			groupAutomaticPayment.Visible=false;
			panelOD.Visible=false;
			panelQB.Visible=true;
			panelQB.Location=new Point(27,27);
			panelQB.Size=new Size(519,606);
			groupAutomaticPayment.Visible=false;
			textCompanyFileQB.Text=PrefC.GetString(PrefName.QuickBooksCompanyFile);
			textIncomeAccountQB.Text=PrefC.GetString(PrefName.QuickBooksIncomeAccount);
			string depStr=PrefC.GetString(PrefName.QuickBooksDepositAccounts);
			string[] depStrArray=depStr.Split(new char[] { ',' });
			listAccountsQB=new List<string>();
			for(int i=0;i<depStrArray.Length;i++) {
				if(depStrArray[i]=="") {
					continue;
				}
				listAccountsQB.Add(depStrArray[i]);
			}
			FillAccountListQB();
		}

		private void butAddPay_Click(object sender,EventArgs e) {
			AccountingAutoPay autoPay=new AccountingAutoPay();
			FormAccountingAutoPayEdit FormA=new FormAccountingAutoPayEdit();
			FormA.AutoPayCur=autoPay;
			FormA.IsNew=true;
			if(FormA.ShowDialog()!=DialogResult.OK) {
				return;
			}
			payList.Add(autoPay);
			FillPayGrid();
		}

		private void butChangeCash_Click(object sender,EventArgs e) {
			FormAccountPick FormA=new FormAccountPick();
			FormA.ShowDialog();
			if(FormA.DialogResult!=DialogResult.OK) {
				return;
			}
			PickedPayAccountNum=FormA.SelectedAccount.AccountNum;
			textAccountCashInc.Text=Accounts.GetDescript(PickedPayAccountNum);
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listSoftware.SelectedIndex==-1) {
				MsgBox.Show(this,"Must select an accounting software.");
				return;
			}
			if(listSoftware.SelectedIndex!=2) {//Not QuickBooks.
				string depStr="";
				for(int i=0;i<depAL.Count;i++) {
					if(i>0) {
						depStr+=",";
					}
					depStr+=depAL[i].ToString();
				}
				if(Prefs.UpdateString(PrefName.AccountingDepositAccounts,depStr)) {
					DataValid.SetInvalid(InvalidType.Prefs);
				}
				if(Prefs.UpdateLong(PrefName.AccountingIncomeAccount,PickedDepAccountNum)) {
					DataValid.SetInvalid(InvalidType.Prefs);
				}
				//pay------------------------------------------------------------------------------------------
				AccountingAutoPays.SaveList(payList);//just deletes them all and starts over
				DataValid.SetInvalid(InvalidType.AccountingAutoPays);
				if(Prefs.UpdateLong(PrefName.AccountingCashIncomeAccount,PickedPayAccountNum)) {
					DataValid.SetInvalid(InvalidType.Prefs);
				}
			}
			else {
				//QuickBooks-----------------------------------------------------------------------------------
				string depStr="";
				for(int i=0;i<listBoxAccountsQB.Items.Count;i++) {
					if(i>0) {
						depStr+=",";
					}
					depStr+=listBoxAccountsQB.Items[i].ToString();
				}
				if(Prefs.UpdateString(PrefName.QuickBooksCompanyFile,textCompanyFileQB.Text)
					| Prefs.UpdateString(PrefName.QuickBooksDepositAccounts,depStr)
					| Prefs.UpdateString(PrefName.QuickBooksIncomeAccount,textIncomeAccountQB.Text)) 
				{
					DataValid.SetInvalid(InvalidType.Prefs);
				}
			}
			//Update the selected accounting software.
			if(Prefs.UpdateInt(PrefName.AccountingSoftware,listSoftware.SelectedIndex)) {
				DataValid.SetInvalid(InvalidType.Prefs);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		


	}
}





















