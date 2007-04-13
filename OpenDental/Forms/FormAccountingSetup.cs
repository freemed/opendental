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
		private GroupBox groupBox2;
		private OpenDental.UI.Button butChangeCash;
		private Label label4;
		private TextBox textAccountCashInc;
		private Label label5;
		private OpenDental.UI.Button butAddPay;
		private int PickedDepAccountNum;
		//private ArrayList cashAL;
		private int PickedPayAccountNum;
		private OpenDental.UI.ODGrid gridMain;
		///<summary>Arraylist of AccountingAutoPays.</summary>
		private ArrayList payAL;

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
			this.butRemove = new OpenDental.UI.Button();
			this.butChange = new OpenDental.UI.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.textAccountInc = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butChangeCash = new OpenDental.UI.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.textAccountCashInc = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.butAddPay = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168,53);
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
			this.groupBox1.Location = new System.Drawing.Point(27,27);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(519,222);
			this.groupBox1.TabIndex = 32;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Automatic Deposit Entries";
			// 
			// listAccountsDep
			// 
			this.listAccountsDep.FormattingEnabled = true;
			this.listAccountsDep.Location = new System.Drawing.Point(182,61);
			this.listAccountsDep.Name = "listAccountsDep";
			this.listAccountsDep.Size = new System.Drawing.Size(230,108);
			this.listAccountsDep.TabIndex = 37;
			// 
			// butRemove
			// 
			this.butRemove.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRemove.Autosize = true;
			this.butRemove.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRemove.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRemove.CornerRadius = 4F;
			this.butRemove.Location = new System.Drawing.Point(418,88);
			this.butRemove.Name = "butRemove";
			this.butRemove.Size = new System.Drawing.Size(75,26);
			this.butRemove.TabIndex = 36;
			this.butRemove.Text = "Remove";
			this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
			// 
			// butChange
			// 
			this.butChange.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChange.Autosize = true;
			this.butChange.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChange.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChange.CornerRadius = 4F;
			this.butChange.Location = new System.Drawing.Point(418,176);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(75,26);
			this.butChange.TabIndex = 35;
			this.butChange.Text = "Change";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12,179);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(168,19);
			this.label3.TabIndex = 33;
			this.label3.Text = "Income Account";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAccountInc
			// 
			this.textAccountInc.Location = new System.Drawing.Point(182,179);
			this.textAccountInc.Name = "textAccountInc";
			this.textAccountInc.ReadOnly = true;
			this.textAccountInc.Size = new System.Drawing.Size(230,20);
			this.textAccountInc.TabIndex = 34;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(19,26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(492,27);
			this.label2.TabIndex = 32;
			this.label2.Text = "Everytime a deposit is created, an accounting transaction will also be automatica" +
    "lly created.";
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Location = new System.Drawing.Point(418,58);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,26);
			this.butAdd.TabIndex = 30;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.gridMain);
			this.groupBox2.Controls.Add(this.butChangeCash);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.textAccountCashInc);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.butAddPay);
			this.groupBox2.Location = new System.Drawing.Point(27,280);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(519,353);
			this.groupBox2.TabIndex = 33;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Automatic Payment Entries";
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(22,104);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(471,177);
			this.gridMain.TabIndex = 40;
			this.gridMain.Title = "Auto Payment Entries";
			this.gridMain.TranslationName = "TableAccountingAutoPay";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butChangeCash
			// 
			this.butChangeCash.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChangeCash.Autosize = true;
			this.butChangeCash.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChangeCash.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChangeCash.CornerRadius = 4F;
			this.butChangeCash.Location = new System.Drawing.Point(418,306);
			this.butChangeCash.Name = "butChangeCash";
			this.butChangeCash.Size = new System.Drawing.Size(75,26);
			this.butChangeCash.TabIndex = 35;
			this.butChangeCash.Text = "Change";
			this.butChangeCash.Click += new System.EventHandler(this.butChangeCash_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12,309);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(168,19);
			this.label4.TabIndex = 33;
			this.label4.Text = "Income Account";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAccountCashInc
			// 
			this.textAccountCashInc.Location = new System.Drawing.Point(182,309);
			this.textAccountCashInc.Name = "textAccountCashInc";
			this.textAccountCashInc.ReadOnly = true;
			this.textAccountCashInc.Size = new System.Drawing.Size(230,20);
			this.textAccountCashInc.TabIndex = 34;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(19,26);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(492,50);
			this.label5.TabIndex = 32;
			this.label5.Text = resources.GetString("label5.Text");
			// 
			// butAddPay
			// 
			this.butAddPay.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddPay.Autosize = true;
			this.butAddPay.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddPay.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddPay.CornerRadius = 4F;
			this.butAddPay.Location = new System.Drawing.Point(418,75);
			this.butAddPay.Name = "butAddPay";
			this.butAddPay.Size = new System.Drawing.Size(75,26);
			this.butAddPay.TabIndex = 30;
			this.butAddPay.Text = "Add";
			this.butAddPay.Click += new System.EventHandler(this.butAddPay_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(607,565);
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
			this.butCancel.Location = new System.Drawing.Point(607,606);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormAccountingSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(715,657);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
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
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAccountingSetup_Load(object sender,EventArgs e) {
			string depStr=PrefB.GetString("AccountingDepositAccounts");
			string[] depStrArray=depStr.Split(new char[] {','});
			depAL=new ArrayList();
			for(int i=0;i<depStrArray.Length;i++){
				if(depStrArray[i]==""){
					continue;
				}
				depAL.Add(PIn.PInt(depStrArray[i]));
			}
			FillDepList();
			PickedDepAccountNum=PrefB.GetInt("AccountingIncomeAccount");
			textAccountInc.Text=Accounts.GetDescript(PickedDepAccountNum);
			//pay----------------------------------------------------------
			payAL=AccountingAutoPays.AList;//Count might be 0
			FillPayGrid();
			PickedPayAccountNum=PrefB.GetInt("AccountingCashIncomeAccount");
			textAccountCashInc.Text=Accounts.GetDescript(PickedPayAccountNum);
		}

		private void FillDepList(){
			listAccountsDep.Items.Clear();
			for(int i=0;i<depAL.Count;i++){
				listAccountsDep.Items.Add(Accounts.GetDescript((int)depAL[i]));
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
			for(int i=0;i<payAL.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(DefB.GetName(DefCat.PaymentTypes,((AccountingAutoPay)payAL[i]).PayType));
				row.Cells.Add(AccountingAutoPays.GetPickListDesc((AccountingAutoPay)payAL[i]));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
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

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormAccountingAutoPayEdit FormA=new FormAccountingAutoPayEdit();
			FormA.AutoPayCur=(AccountingAutoPay)payAL[e.Row];
			FormA.ShowDialog();
			if(FormA.AutoPayCur==null){//user deleted
				payAL.RemoveAt(e.Row);
			}
			FillPayGrid();
		}

		private void butAddPay_Click(object sender,EventArgs e) {
			AccountingAutoPay autoPay=new AccountingAutoPay();
			FormAccountingAutoPayEdit FormA=new FormAccountingAutoPayEdit();
			FormA.AutoPayCur=autoPay;
			FormA.IsNew=true;
			if(FormA.ShowDialog()!=DialogResult.OK) {
				return;
			}
			payAL.Add(autoPay);
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
			string depStr="";
			for(int i=0;i<depAL.Count;i++){
				if(i>0){
					depStr+=",";
				}
				depStr+=depAL[i].ToString();
			}
			if(Prefs.UpdateString("AccountingDepositAccounts",depStr)){
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			if(Prefs.UpdateInt("AccountingIncomeAccount",PickedDepAccountNum)) {
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			//pay------------------------------------------------------------------------------------------
			AccountingAutoPays.SaveList(payAL);//just deletes them all and starts over
			DataValid.SetInvalid(InvalidTypes.Operatories);
			if(Prefs.UpdateInt("AccountingCashIncomeAccount",PickedPayAccountNum)) {
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		


	}
}





















