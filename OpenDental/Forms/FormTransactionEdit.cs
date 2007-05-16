using System;
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
	public class FormTransactionEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.ODGrid gridMain;
		private ValidDate textDate;
		private Label label1;
		private Label label2;
		private TextBox textDateTimeEntry;
		private OpenDental.UI.Button butAttachDep;
		private OpenDental.UI.Button butDelete;
		private Transaction TransCur;
		private ArrayList JournalList;
		private ArrayList JournalListOld;
		private Label label3;
		private TextBox textDebit;
		private TextBox textCredit;
		private CheckBox checkMemoSame;
		private OpenDental.UI.Button butAdd;
		private CheckBox checkSimple;
		private Label label5;
		private Label label4;
		private ValidDouble textAmount;
		private Label label6;
		private Panel panelSimple;
		private Panel panelCompound;
		private TextBox textCheckNumber;
		private Label label7;
		private TextBox textMemo;
		private OpenDental.UI.Button butChange;
		private TextBox textAccount;
		///<summary>The account where the edit originated from.  This affects how the simple version gets displayed, and the signs on debit and credit.</summary>
		private Account AccountOfOrigin;
		///<summary>When in simple mode, this is the 'other' account, not the one of origin.  It can be null.</summary>
		private Account AccountPicked;
		private GroupBox groupBox1;
		private TextBox textSourceDeposit;
		private Label label9;
		private Label label8;
		private TextBox textSourcePay;
		private OpenDental.UI.Button butAttachPay;
		private Label labelReconcile;
		private TextBox textReconcile;
		///<summary>Just used for security.</summary>
		public bool IsNew;

		///<summary></summary>
		public FormTransactionEdit(int transNum,int accountNum)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			TransCur=Transactions.GetTrans(transNum);
			AccountOfOrigin=Accounts.GetAccount(accountNum);
			//AccountNumOrigin=accountNumOrigin;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTransactionEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textDateTimeEntry = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textDebit = new System.Windows.Forms.TextBox();
			this.textCredit = new System.Windows.Forms.TextBox();
			this.checkMemoSame = new System.Windows.Forms.CheckBox();
			this.checkSimple = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.panelSimple = new System.Windows.Forms.Panel();
			this.butChange = new OpenDental.UI.Button();
			this.textAccount = new System.Windows.Forms.TextBox();
			this.textMemo = new System.Windows.Forms.TextBox();
			this.textCheckNumber = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textAmount = new OpenDental.ValidDouble();
			this.panelCompound = new System.Windows.Forms.Panel();
			this.butAdd = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.textSourcePay = new System.Windows.Forms.TextBox();
			this.butAttachPay = new OpenDental.UI.Button();
			this.textSourceDeposit = new System.Windows.Forms.TextBox();
			this.butAttachDep = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.textDate = new OpenDental.ValidDate();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.labelReconcile = new System.Windows.Forms.Label();
			this.textReconcile = new System.Windows.Forms.TextBox();
			this.panelSimple.SuspendLayout();
			this.panelCompound.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28,34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,20);
			this.label1.TabIndex = 4;
			this.label1.Text = "Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6,10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(123,20);
			this.label2.TabIndex = 5;
			this.label2.Text = "Date/Time  Entered";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDateTimeEntry
			// 
			this.textDateTimeEntry.Location = new System.Drawing.Point(132,7);
			this.textDateTimeEntry.Name = "textDateTimeEntry";
			this.textDateTimeEntry.ReadOnly = true;
			this.textDateTimeEntry.Size = new System.Drawing.Size(147,20);
			this.textDateTimeEntry.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(74,221);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77,20);
			this.label3.TabIndex = 9;
			this.label3.Text = "Totals";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDebit
			// 
			this.textDebit.Location = new System.Drawing.Point(153,218);
			this.textDebit.Name = "textDebit";
			this.textDebit.ReadOnly = true;
			this.textDebit.Size = new System.Drawing.Size(70,20);
			this.textDebit.TabIndex = 10;
			// 
			// textCredit
			// 
			this.textCredit.Location = new System.Drawing.Point(223,218);
			this.textCredit.Name = "textCredit";
			this.textCredit.ReadOnly = true;
			this.textCredit.Size = new System.Drawing.Size(70,20);
			this.textCredit.TabIndex = 11;
			// 
			// checkMemoSame
			// 
			this.checkMemoSame.Location = new System.Drawing.Point(328,218);
			this.checkMemoSame.Name = "checkMemoSame";
			this.checkMemoSame.Size = new System.Drawing.Size(165,20);
			this.checkMemoSame.TabIndex = 12;
			this.checkMemoSame.Text = "Memo Same For All";
			this.checkMemoSame.UseVisualStyleBackColor = true;
			// 
			// checkSimple
			// 
			this.checkSimple.Location = new System.Drawing.Point(392,7);
			this.checkSimple.Name = "checkSimple";
			this.checkSimple.Size = new System.Drawing.Size(154,20);
			this.checkSimple.TabIndex = 14;
			this.checkSimple.Text = "Simple";
			this.checkSimple.UseVisualStyleBackColor = true;
			this.checkSimple.Click += new System.EventHandler(this.checkSimple_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8,37);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,20);
			this.label5.TabIndex = 21;
			this.label5.Text = "Other Account";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8,65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,20);
			this.label4.TabIndex = 19;
			this.label4.Text = "Memo";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(7,11);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100,20);
			this.label6.TabIndex = 17;
			this.label6.Text = "Amount";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// panelSimple
			// 
			this.panelSimple.Controls.Add(this.butChange);
			this.panelSimple.Controls.Add(this.textAccount);
			this.panelSimple.Controls.Add(this.textMemo);
			this.panelSimple.Controls.Add(this.textCheckNumber);
			this.panelSimple.Controls.Add(this.label7);
			this.panelSimple.Controls.Add(this.textAmount);
			this.panelSimple.Controls.Add(this.label5);
			this.panelSimple.Controls.Add(this.label6);
			this.panelSimple.Controls.Add(this.label4);
			this.panelSimple.Location = new System.Drawing.Point(21,81);
			this.panelSimple.Name = "panelSimple";
			this.panelSimple.Size = new System.Drawing.Size(494,186);
			this.panelSimple.TabIndex = 16;
			// 
			// butChange
			// 
			this.butChange.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChange.Autosize = true;
			this.butChange.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChange.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChange.CornerRadius = 4F;
			this.butChange.Location = new System.Drawing.Point(347,31);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(75,26);
			this.butChange.TabIndex = 28;
			this.butChange.Text = "Change";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// textAccount
			// 
			this.textAccount.Location = new System.Drawing.Point(111,34);
			this.textAccount.Name = "textAccount";
			this.textAccount.ReadOnly = true;
			this.textAccount.Size = new System.Drawing.Size(230,20);
			this.textAccount.TabIndex = 27;
			// 
			// textMemo
			// 
			this.textMemo.Location = new System.Drawing.Point(111,62);
			this.textMemo.Multiline = true;
			this.textMemo.Name = "textMemo";
			this.textMemo.Size = new System.Drawing.Size(230,43);
			this.textMemo.TabIndex = 25;
			// 
			// textCheckNumber
			// 
			this.textCheckNumber.Location = new System.Drawing.Point(111,111);
			this.textCheckNumber.Name = "textCheckNumber";
			this.textCheckNumber.Size = new System.Drawing.Size(133,20);
			this.textCheckNumber.TabIndex = 24;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8,114);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100,20);
			this.label7.TabIndex = 23;
			this.label7.Text = "Check Number";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(111,8);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(89,20);
			this.textAmount.TabIndex = 18;
			// 
			// panelCompound
			// 
			this.panelCompound.Controls.Add(this.butAdd);
			this.panelCompound.Controls.Add(this.gridMain);
			this.panelCompound.Controls.Add(this.label3);
			this.panelCompound.Controls.Add(this.textDebit);
			this.panelCompound.Controls.Add(this.textCredit);
			this.panelCompound.Controls.Add(this.checkMemoSame);
			this.panelCompound.Location = new System.Drawing.Point(42,81);
			this.panelCompound.Name = "panelCompound";
			this.panelCompound.Size = new System.Drawing.Size(504,262);
			this.panelCompound.TabIndex = 17;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(3,216);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,26);
			this.butAdd.TabIndex = 13;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(3,10);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(490,199);
			this.gridMain.TabIndex = 2;
			this.gridMain.Title = "Splits";
			this.gridMain.TranslationName = "TableTransSplits";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.textSourcePay);
			this.groupBox1.Controls.Add(this.butAttachPay);
			this.groupBox1.Controls.Add(this.textSourceDeposit);
			this.groupBox1.Controls.Add(this.butAttachDep);
			this.groupBox1.Location = new System.Drawing.Point(119,367);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(416,80);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Source Documents";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(6,48);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(87,20);
			this.label9.TabIndex = 12;
			this.label9.Text = "Payment";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(6,19);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(87,20);
			this.label8.TabIndex = 11;
			this.label8.Text = "Deposit";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSourcePay
			// 
			this.textSourcePay.Location = new System.Drawing.Point(95,49);
			this.textSourcePay.Multiline = true;
			this.textSourcePay.Name = "textSourcePay";
			this.textSourcePay.ReadOnly = true;
			this.textSourcePay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textSourcePay.Size = new System.Drawing.Size(231,20);
			this.textSourcePay.TabIndex = 10;
			// 
			// butAttachPay
			// 
			this.butAttachPay.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAttachPay.Autosize = true;
			this.butAttachPay.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAttachPay.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAttachPay.CornerRadius = 4F;
			this.butAttachPay.Location = new System.Drawing.Point(329,46);
			this.butAttachPay.Name = "butAttachPay";
			this.butAttachPay.Size = new System.Drawing.Size(80,26);
			this.butAttachPay.TabIndex = 9;
			this.butAttachPay.Text = "Attach";
			this.butAttachPay.Click += new System.EventHandler(this.butAttachPay_Click);
			// 
			// textSourceDeposit
			// 
			this.textSourceDeposit.Location = new System.Drawing.Point(95,20);
			this.textSourceDeposit.Name = "textSourceDeposit";
			this.textSourceDeposit.ReadOnly = true;
			this.textSourceDeposit.Size = new System.Drawing.Size(231,20);
			this.textSourceDeposit.TabIndex = 8;
			// 
			// butAttachDep
			// 
			this.butAttachDep.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAttachDep.Autosize = true;
			this.butAttachDep.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAttachDep.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAttachDep.CornerRadius = 4F;
			this.butAttachDep.Location = new System.Drawing.Point(329,17);
			this.butAttachDep.Name = "butAttachDep";
			this.butAttachDep.Size = new System.Drawing.Size(80,26);
			this.butAttachDep.TabIndex = 7;
			this.butAttachDep.Text = "Attach";
			this.butAttachDep.Click += new System.EventHandler(this.butAttachDep_Click);
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
			this.butDelete.Location = new System.Drawing.Point(31,414);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,26);
			this.butDelete.TabIndex = 8;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(132,31);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(100,20);
			this.textDate.TabIndex = 3;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(568,384);
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
			this.butCancel.Location = new System.Drawing.Point(568,413);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// labelReconcile
			// 
			this.labelReconcile.Location = new System.Drawing.Point(9,57);
			this.labelReconcile.Name = "labelReconcile";
			this.labelReconcile.Size = new System.Drawing.Size(120,20);
			this.labelReconcile.TabIndex = 19;
			this.labelReconcile.Text = "Reconcile Date";
			this.labelReconcile.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelReconcile.Visible = false;
			// 
			// textReconcile
			// 
			this.textReconcile.Location = new System.Drawing.Point(132,54);
			this.textReconcile.Name = "textReconcile";
			this.textReconcile.ReadOnly = true;
			this.textReconcile.Size = new System.Drawing.Size(100,20);
			this.textReconcile.TabIndex = 20;
			this.textReconcile.Visible = false;
			// 
			// FormTransactionEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(677,459);
			this.Controls.Add(this.textReconcile);
			this.Controls.Add(this.labelReconcile);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panelSimple);
			this.Controls.Add(this.panelCompound);
			this.Controls.Add(this.checkSimple);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textDateTimeEntry);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTransactionEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Transaction";
			this.Load += new System.EventHandler(this.FormTransactionEdit_Load);
			this.panelSimple.ResumeLayout(false);
			this.panelSimple.PerformLayout();
			this.panelCompound.ResumeLayout(false);
			this.panelCompound.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormTransactionEdit_Load(object sender,EventArgs e) {
			JournalList=JournalEntries.GetForTrans(TransCur.TransactionNum);//Count might be 0
			if(IsNew) {
				if(!Security.IsAuthorized(Permissions.AccountingCreate,DateTime.Today)) {
					//we will check the date again when saving
					DialogResult=DialogResult.Cancel;
					return;
				}
			}
			else {
				//for an existing transaction, there will always be at least 2 entries.
				//We enforce security here based on date displayed, not date entered
				if(!Security.IsAuthorized(Permissions.AccountingEdit,((JournalEntry)JournalList[0]).DateDisplayed)) {
					butOK.Enabled=false;
					butDelete.Enabled=false;
				}
			}
			JournalListOld=new ArrayList();
			for(int i=0;i<JournalList.Count;i++) {
				JournalListOld.Add(((JournalEntry)JournalList[i]).Copy());
			}
			textDateTimeEntry.Text=TransCur.DateTimeEntry.ToString();
			if(JournalList.Count>0) {
				textDate.Text=((JournalEntry)JournalList[0]).DateDisplayed.ToShortDateString();
			}
			else {
				textDate.Text=DateTime.Today.ToShortDateString();
			}
			if(AccountOfOrigin==null){//if accessed from within a payment screen instead of through accounting
				checkSimple.Checked=false;
				checkSimple.Visible=false;//don't allow user to switch back to simple view
				FillCompound();
			}
			else if(JournalEntries.AttachedToReconcile(JournalList)){
				labelReconcile.Visible=true;
				textReconcile.Visible=true;
				textReconcile.Text=JournalEntries.GetReconcileDate(JournalList).ToShortDateString();
				checkSimple.Checked=false;
				checkSimple.Visible=false;//don't allow user to switch back to simple view
				FillCompound();
			}
			else if(JournalList.Count>2){//compound
				checkSimple.Checked=false;
				FillCompound();
			}
			//so count must be 0 or 2
			else if(JournalList.Count==2 && ((JournalEntry)JournalList[0]).Memo != ((JournalEntry)JournalList[1]).Memo){
				//would be simple view, except memo's are different
				checkSimple.Checked=false;
				FillCompound();
			}
			else{//simple
				checkSimple.Checked=true;
				FillSimple();
			}
			if(TransCur.DepositNum==0){
				butAttachDep.Text=Lan.g(this,"Attach");
			}
			else{
				Deposit dep=Deposits.GetOne(TransCur.DepositNum);
				textSourceDeposit.Text=dep.DateDeposit.ToShortDateString()
					+"  "+dep.Amount.ToString("c");
				butAttachDep.Text=Lan.g(this,"Detach");
			}
			if(TransCur.PayNum==0) {
				//no way to attach yet.  This can be added later.
				butAttachPay.Visible=false;
			}
			else {
				Payment pay=Payments.GetPayment(TransCur.PayNum);
				textSourcePay.Text=Patients.GetPat(pay.PatNum).GetNameFL()+" "
					+pay.PayDate.ToShortDateString()+" "+pay.PayAmt.ToString("c");
				butAttachPay.Text=Lan.g(this,"Detach");
			}
		}

		///<summary>Used when compound view (3 or more journal entries).  Account nums allowed to be 0.</summary>
		private void FillCompound(){
			panelSimple.Visible=false;
			panelCompound.Visible=true;
			bool memoSame=true;
			string memo="";
			double debits=0;
			double credits=0;
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableTransSplits","Account"),150);
			gridMain.Columns.Add(col);
			string str=Lan.g("TableTransSplits","Debit");
			if(AccountOfOrigin!=null){
				if(Accounts.DebitIsPos(AccountOfOrigin.AcctType)) {
					str+=Lan.g(this,"(+)");
				}
				else {
					str+=Lan.g(this,"(-)");
				}
			}
			col=new ODGridColumn(str,70,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			str=Lan.g("TableTransSplits","Credit");
			if(AccountOfOrigin!=null){
				if(Accounts.DebitIsPos(AccountOfOrigin.AcctType)) {
					str+=Lan.g(this,"(-)");
				}
				else {
					str+=Lan.g(this,"(+)");
				}
			}
			col=new ODGridColumn(str,70,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTransSplits","Memo"),200);
			gridMain.Columns.Add(col);			 
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<JournalList.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(Accounts.GetDescript(((JournalEntry)JournalList[i]).AccountNum));
				if(((JournalEntry)JournalList[i]).DebitAmt==0){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(((JournalEntry)JournalList[i]).DebitAmt.ToString("n"));
				}
				if(((JournalEntry)JournalList[i]).CreditAmt==0){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(((JournalEntry)JournalList[i]).CreditAmt.ToString("n"));
				}
				row.Cells.Add(((JournalEntry)JournalList[i]).Memo);
				gridMain.Rows.Add(row);  
				if(i==0){
					memo=((JournalEntry)JournalList[i]).Memo;
				}
				else{
					if(memo!=((JournalEntry)JournalList[i]).Memo){
						memoSame=false;
					}
				}
				credits+=((JournalEntry)JournalList[i]).CreditAmt;
				debits+=((JournalEntry)JournalList[i]).DebitAmt;
			}
			gridMain.EndUpdate();
			checkMemoSame.Checked=memoSame;
			textCredit.Text=credits.ToString("n");
			textDebit.Text=debits.ToString("n");
		}

		///<summary>Used when switching simple view (0, 1, or 2 journal entries with identical notes).  This function fills in the correct fields in the simple view, and then deletes any journal entries.  2 journal entries will be recreated upon leaving for compound view by CreateTwoEntries.  This is only called once upon going to simple view.  It's not called repeatedly as a way of refreshing the screen.</summary>
		private void FillSimple(){
			panelSimple.Visible=true;
			panelCompound.Visible=false;
			if(JournalList.Count==0){
				AccountPicked=null;
				textAccount.Text="";
				butChange.Text=Lan.g(this,"Pick");
				textAmount.Text="";
				textMemo.Text="";
			}
			else if(JournalList.Count==1){
				double amt=0;
				//first we assume that the sole entry is for the current account
				if(Accounts.DebitIsPos(AccountOfOrigin.AcctType)) {//this is used for checking account
					if(((JournalEntry)JournalList[0]).DebitAmt>0) {
						amt=((JournalEntry)JournalList[0]).DebitAmt;
					}
					else {//use the credit
						amt=-((JournalEntry)JournalList[0]).CreditAmt;
					}
				}
				else {//false for checking acct
					if(((JournalEntry)JournalList[0]).DebitAmt>0) {
						amt=-((JournalEntry)JournalList[0]).DebitAmt;
					}
					else {
						amt=((JournalEntry)JournalList[0]).CreditAmt;
					}
				}
				//then, if we assumed wrong, change the sign
				if(((JournalEntry)JournalList[0]).AccountNum!=AccountOfOrigin.AccountNum){
					amt=-amt;
				}
				textAmount.Text=amt.ToString("n");
				if(((JournalEntry)JournalList[0]).AccountNum==0){
					AccountPicked=null;
					textAccount.Text="";
					butChange.Text=Lan.g(this,"Pick");
				}
				else if(((JournalEntry)JournalList[0]).AccountNum==AccountOfOrigin.AccountNum){
					AccountPicked=null;
					textAccount.Text="";
					butChange.Text=Lan.g(this,"Pick");
				}
				else{//the sole entry is not for the current account
					AccountPicked=Accounts.GetAccount(((JournalEntry)JournalList[0]).AccountNum);
					textAccount.Text=AccountPicked.Description;
					butChange.Text=Lan.g(this,"Change");
				}
				textMemo.Text=((JournalEntry)JournalList[0]).Memo;
				textCheckNumber.Text=((JournalEntry)JournalList[0]).CheckNumber;
			}
			else{//count=2
				JournalEntry journalCur;
				JournalEntry journalOther;
				if(((JournalEntry)JournalList[0]).AccountNum==AccountOfOrigin.AccountNum){
					//if the first entry is for the account of origin
					journalCur=(JournalEntry)JournalList[0];
					journalOther=(JournalEntry)JournalList[1];
				}
				else{
					journalCur=(JournalEntry)JournalList[1];
					journalOther=(JournalEntry)JournalList[0];
				}
				if(Accounts.DebitIsPos(AccountOfOrigin.AcctType)){//this is used for checking account
					if(journalCur.DebitAmt>0){
						textAmount.Text=journalCur.DebitAmt.ToString("n");
					}
					else{//use the credit
						textAmount.Text=(-journalCur.CreditAmt).ToString("n");
					}
				}
				else{//false for checking acct
					if(journalCur.DebitAmt>0) {
						textAmount.Text=(-journalCur.DebitAmt).ToString("n");
					}
					else {
						textAmount.Text=journalCur.CreditAmt.ToString("n");
					}
				}
				if(journalOther.AccountNum==0){
					AccountPicked=null;
					textAccount.Text="";
					butChange.Text=Lan.g(this,"Pick");
				}
				else{
					AccountPicked=Accounts.GetAccount(journalOther.AccountNum);
					textAccount.Text=AccountPicked.Description;
					butChange.Text=Lan.g(this,"Change");
				}
				textMemo.Text=journalCur.Memo;
				if(journalCur.CheckNumber!=""){
					textCheckNumber.Text=journalCur.CheckNumber;
				}
				if(journalOther.CheckNumber!="") {
					textCheckNumber.Text=journalOther.CheckNumber;
				}
			}
			JournalList=new ArrayList();
		}

		private void butChange_Click(object sender,EventArgs e) {
			FormAccountPick FormA=new FormAccountPick();
			FormA.ShowDialog();
			if(FormA.DialogResult!=DialogResult.OK) {
				return;
			}
			AccountPicked=FormA.SelectedAccount.Copy();
			textAccount.Text=AccountPicked.Description;
			butChange.Text=Lan.g(this,"Change");
		}

		private void checkSimple_Click(object sender,EventArgs e) {
			if(checkSimple.Checked){
				if(JournalList.Count>2){//do not allow switching to simple if there are more than 2 entries
					MsgBox.Show(this,"Not allowed to switch to simple view when there are more then two entries.");
					checkSimple.Checked=false;
					return;
				}
				if(JournalList.Count==2 && ((JournalEntry)JournalList[0]).Memo != ((JournalEntry)JournalList[1]).Memo ) {
					//warn if notes are different
					if(!MsgBox.Show(this,true,"Note might be lost. Continue?")){
						checkSimple.Checked=false;
						return;
					}
				}
				FillSimple();
			}
			else{//switching from simple to compound view
				CreateTwoEntries();//never fails.  Just does the best it can
				FillCompound();
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			JournalEntry entry=new JournalEntry();
			entry.TransactionNum=TransCur.TransactionNum;
			if(checkMemoSame.Checked && JournalList.Count>0){
				entry.Memo=((JournalEntry)JournalList[0]).Memo;
			}
			//date gets set when closing.  Everthing else gets set within following form.
			FormJournalEntryEdit FormJ=new FormJournalEntryEdit();
			FormJ.IsNew=true;
			FormJ.EntryCur=entry;
			FormJ.ShowDialog();
			if(FormJ.DialogResult==DialogResult.OK) {
				JournalList.Add(FormJ.EntryCur);
				if(checkMemoSame.Checked) {
					for(int i=0;i<JournalList.Count;i++) {
						((JournalEntry)JournalList[i]).Memo=FormJ.EntryCur.Memo;
					}
				}
			}
			FillCompound();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormJournalEntryEdit FormJ=new FormJournalEntryEdit();
			FormJ.EntryCur=(JournalEntry)JournalList[e.Row];
			FormJ.ShowDialog();
			if(FormJ.EntryCur==null){//user deleted
				JournalList.RemoveAt(e.Row);
			}
			else if(FormJ.DialogResult==DialogResult.OK){
				if(checkMemoSame.Checked) {
					for(int i=0;i<JournalList.Count;i++) {
						((JournalEntry)JournalList[i]).Memo=FormJ.EntryCur.Memo;
					}
				}
			}
			FillCompound();
		}

		private void butAttachDep_Click(object sender,EventArgs e) {
			if(TransCur.DepositNum==0){//trying to attach
				FormDeposits FormD=new FormDeposits();
				FormD.IsSelectionMode=true;
				FormD.ShowDialog();
				if(FormD.DialogResult==DialogResult.Cancel){
					return;
				}
				TransCur.DepositNum=FormD.SelectedDeposit.DepositNum;
				textSourceDeposit.Text=FormD.SelectedDeposit.DateDeposit.ToShortDateString()
					+"  "+FormD.SelectedDeposit.Amount.ToString("c");
				butAttachDep.Text=Lan.g(this,"Detach");
			}
			else{//trying to detach
				TransCur.DepositNum=0;
				textSourceDeposit.Text="";
				butAttachDep.Text=Lan.g(this,"Attach");
			}
		}

		private void butAttachPay_Click(object sender,EventArgs e) {
			if(TransCur.PayNum==0) {//trying to attach
				//no way to do this yet.
			}
			else {//trying to detach
				TransCur.PayNum=0;
				textSourcePay.Text="";
				butAttachPay.Visible=false;
			}
		}

		/*//<summary>If the journalList is 0 or 1 in length, then this function creates either 1 or 2 entries so that the simple view can display properly.</summary>
		private void CreateTwoEntries() {
			JournalEntry entry;
			if(JournalList.Count>=2) {
				return;
			}
			if(JournalList.Count==0) {
				//first, for account of origin
				entry=new JournalEntry();
				entry.TransactionNum=TransCur.TransactionNum;
				if(textDate.Text=="" || textDate.errorProvider1.GetError(textDate)!="") {
					entry.DateDisplayed=DateTime.Today;
				}
				else {
					entry.DateDisplayed=PIn.PDate(textDate.Text);
				}
				entry.AccountNum=AccountOfOrigin.AccountNum;
				JournalList.Add(entry);
				//then, for the other
				entry=new JournalEntry();
				entry.TransactionNum=TransCur.TransactionNum;
				if(textDate.Text=="" || textDate.errorProvider1.GetError(textDate)!="") {
					entry.DateDisplayed=DateTime.Today;
				}
				else {
					entry.DateDisplayed=PIn.PDate(textDate.Text);
				}
				entry.AccountNum=0;
				JournalList.Add(entry);
				return;
			}
			//otherwise, count=1
			entry=new JournalEntry();
			entry.TransactionNum=TransCur.TransactionNum;
			entry.DateDisplayed=((JournalEntry)JournalList[0]).DateDisplayed;
			if(((JournalEntry)JournalList[0]).AccountNum==AccountOfOrigin.AccountNum) {
				entry.AccountNum=0;
			}
			else {
				entry.AccountNum=AccountOfOrigin.AccountNum;
			}
			entry.DebitAmt=((JournalEntry)JournalList[0]).CreditAmt;
			entry.CreditAmt=((JournalEntry)JournalList[0]).DebitAmt;
			JournalList.Add(entry);
		}*/

		
		///<summary>When leaving simple view, this function takes the info from the screen and creates two journal entries.  Never fails.  One of the journal entries might accountNum=0, and they might both have amounts of 0.  This is all in preparation for either saving or for viewing as compound.</summary>
		private void CreateTwoEntries(){
			JournalEntry entry;
			//first, for account of origin
			entry=new JournalEntry();
			entry.TransactionNum=TransCur.TransactionNum;
			if(textDate.Text=="" || textDate.errorProvider1.GetError(textDate)!="") {
				entry.DateDisplayed=DateTime.Today;
			}
			else {
				entry.DateDisplayed=PIn.PDate(textDate.Text);
			}
			entry.AccountNum=AccountOfOrigin.AccountNum;
			double amt=0;
			if(textAmount.errorProvider1.GetError(textAmount)==""){//if no error
				amt=PIn.PDouble(textAmount.Text);
			}
			//if amt==0, then both credit and debit remain 0
			if(amt>0){
				if(Accounts.DebitIsPos(AccountOfOrigin.AcctType)) {//used for checking
					entry.DebitAmt=amt;
				}
				else {
					entry.CreditAmt=amt;
				}
			}
			else if(amt<0){
				if(Accounts.DebitIsPos(AccountOfOrigin.AcctType)) {//used for checking
					entry.CreditAmt=-amt;
				}
				else {
					entry.DebitAmt=-amt;
				}
			}
			entry.Memo=textMemo.Text;
			entry.CheckNumber=textCheckNumber.Text;
			JournalList.Add(entry);
			//then, for the other
			entry=new JournalEntry();
			entry.TransactionNum=TransCur.TransactionNum;
			entry.DateDisplayed=((JournalEntry)JournalList[0]).DateDisplayed;
			entry.DebitAmt=((JournalEntry)JournalList[0]).CreditAmt;
			entry.CreditAmt=((JournalEntry)JournalList[0]).DebitAmt;
			if(AccountPicked==null){
				entry.AccountNum=0;
			}
			else{
				entry.AccountNum=AccountPicked.AccountNum;
			}
			entry.Memo=textMemo.Text;
			entry.CheckNumber=textCheckNumber.Text;
			JournalList.Add(entry);
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"Delete this entire transaction?")) {
				return;
			}
			string securityentry="";
			if(!IsNew){
				//we need this data before it's gone
				JournalList=JournalEntries.GetForTrans(TransCur.TransactionNum);//because they were cleared when laying out
				securityentry=Lan.g(this,"Deleted: ")
					+((JournalEntry)JournalList[0]).DateDisplayed.ToShortDateString()+" ";
				double tot=0;
				for(int i=0;i<JournalList.Count;i++) {
					tot+=((JournalEntry)JournalList[i]).DebitAmt;
					if(i>0){
						securityentry+=", ";
					}
					securityentry+=Accounts.GetDescript(((JournalEntry)JournalList[i]).AccountNum);
				}
				securityentry+=". "+tot.ToString("c");
				JournalList=new ArrayList();//in case it fails, we don't want to leave this list around.
			}
			try {
				Transactions.Delete(TransCur);
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			if(!IsNew){
				SecurityLogs.MakeLogEntry(Permissions.AccountingEdit,0,securityentry);
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDate.errorProvider1.GetError(textDate) !="") {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			DateTime date=PIn.PDate(textDate.Text);
			//Prevent backdating----------------------------------------------------------------------------------------
			if(IsNew) {
				if(!Security.IsAuthorized(Permissions.AccountingCreate,date)) {
					return;
				}
			}
			else {
				//We enforce security here based on date displayed, not date entered
				if(!Security.IsAuthorized(Permissions.AccountingEdit,date)) {
					return;
				}
			}
			if(checkSimple.Checked){//simple view
				//even though CreateTwoEntries could handle any errors, it makes more sense to catch errors before calling it
				if(textAmount.errorProvider1.GetError(textAmount)!="")
				{
					MsgBox.Show(this,"Please fix data entry errors first.");
					return;
				}
				if(AccountPicked==null){
					MsgBox.Show(this,"Please select an account first.");
					return;
				}
				double amt=PIn.PDouble(textAmount.Text);
				if(amt==0){
					MsgBox.Show(this,"Amount not allowed to be zero.");
					return;
				}
				CreateTwoEntries();
			}
			else{//compound view
				if(textCredit.Text!=textDebit.Text){
					MsgBox.Show(this,"Debits and Credits must be equal.");
					return;
				}
				for(int i=0;i<JournalList.Count;i++){
					if(((JournalEntry)JournalList[i]).AccountNum==0){
						MsgBox.Show(this,"Accounts must be selected for each entry first.");
						return;
					}
				}
			}
			string splits="";
			for(int i=0;i<JournalList.Count;i++) {
				((JournalEntry)JournalList[i]).DateDisplayed=date;
				splits="";
				for(int j=0;j<JournalList.Count;j++) {
					if(i==j) {
						continue;
					}
					if(splits !="") {
						splits+="\r\n";
					}
					splits+=Accounts.GetDescript(((JournalEntry)JournalList[j]).AccountNum);
					if(JournalList.Count<3){
						continue;//don't show the amount if there is only two splits, because the amount is the same.
					}
					if(((JournalEntry)JournalList[j]).CreditAmt>0) {
						splits+="  "+((JournalEntry)JournalList[j]).CreditAmt.ToString("n");
					}
					else if(((JournalEntry)JournalList[j]).DebitAmt>0) {
						splits+="  "+((JournalEntry)JournalList[j]).DebitAmt.ToString("n");
					}
				}
				((JournalEntry)JournalList[i]).Splits=splits;
			}
			//try{
				JournalEntries.UpdateList(JournalListOld,JournalList);
			//}
			//catch{

			//}
			Transactions.Update(TransCur);//this catches DepostitNum, the only user-editable field.
			double tot=0;
			for(int i=0;i<JournalList.Count;i++){
				tot+=((JournalEntry)JournalList[i]).DebitAmt;
			}
			if(IsNew) {
				SecurityLogs.MakeLogEntry(Permissions.AccountingCreate,0,
					date.ToShortDateString()+" "+AccountOfOrigin.Description+" "+tot.ToString("c"));
			}
			else {
				string txt=date.ToShortDateString();
				if(AccountOfOrigin!=null){
					txt+=" "+AccountOfOrigin.Description;
				}
				txt+=" "+tot.ToString("c");
				SecurityLogs.MakeLogEntry(Permissions.AccountingEdit,0,txt);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

		

		

		

		
		

	

		


	}
}





















