/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FormJournalEntryEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label labelMemo;
		private System.Windows.Forms.Label labelDebit;
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butDelete;
		private OpenDental.ValidDouble textDebit;
		//private ArrayList PosIndex=new ArrayList();
		//private ArrayList NegIndex=new ArrayList();
		///<summary></summary>
		public JournalEntry EntryCur;
		private TextBox textMemo;
		private ValidDouble textCredit;
		private Label labelCredit;
		private TextBox textCheckNumber;
		private Label label2;
		private Label label5;
		private TextBox textAccount;
		private OpenDental.UI.Button butChange;
		private Label labelReconcile;
		private TextBox textReconcile;
		private Account AccountPicked;

		///<summary></summary>
		public FormJournalEntryEdit(){
			InitializeComponent();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormJournalEntryEdit));
			this.labelMemo = new System.Windows.Forms.Label();
			this.labelDebit = new System.Windows.Forms.Label();
			this.textMemo = new System.Windows.Forms.TextBox();
			this.labelCredit = new System.Windows.Forms.Label();
			this.textCheckNumber = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textAccount = new System.Windows.Forms.TextBox();
			this.butChange = new OpenDental.UI.Button();
			this.textCredit = new OpenDental.ValidDouble();
			this.textDebit = new OpenDental.ValidDouble();
			this.butDelete = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.labelReconcile = new System.Windows.Forms.Label();
			this.textReconcile = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// labelMemo
			// 
			this.labelMemo.Location = new System.Drawing.Point(16,94);
			this.labelMemo.Name = "labelMemo";
			this.labelMemo.Size = new System.Drawing.Size(93,16);
			this.labelMemo.TabIndex = 0;
			this.labelMemo.Text = "Memo";
			this.labelMemo.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelDebit
			// 
			this.labelDebit.Location = new System.Drawing.Point(110,14);
			this.labelDebit.Name = "labelDebit";
			this.labelDebit.Size = new System.Drawing.Size(90,16);
			this.labelDebit.TabIndex = 4;
			this.labelDebit.Text = "Debit";
			this.labelDebit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textMemo
			// 
			this.textMemo.Location = new System.Drawing.Point(110,90);
			this.textMemo.Multiline = true;
			this.textMemo.Name = "textMemo";
			this.textMemo.Size = new System.Drawing.Size(230,46);
			this.textMemo.TabIndex = 6;
			// 
			// labelCredit
			// 
			this.labelCredit.Location = new System.Drawing.Point(205,14);
			this.labelCredit.Name = "labelCredit";
			this.labelCredit.Size = new System.Drawing.Size(90,16);
			this.labelCredit.TabIndex = 19;
			this.labelCredit.Text = "Credit";
			this.labelCredit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textCheckNumber
			// 
			this.textCheckNumber.Location = new System.Drawing.Point(110,145);
			this.textCheckNumber.Name = "textCheckNumber";
			this.textCheckNumber.Size = new System.Drawing.Size(132,20);
			this.textCheckNumber.TabIndex = 21;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(5,149);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104,16);
			this.label2.TabIndex = 20;
			this.label2.Text = "Check Number";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7,63);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,20);
			this.label5.TabIndex = 23;
			this.label5.Text = "Account";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAccount
			// 
			this.textAccount.Location = new System.Drawing.Point(110,60);
			this.textAccount.Name = "textAccount";
			this.textAccount.ReadOnly = true;
			this.textAccount.Size = new System.Drawing.Size(230,20);
			this.textAccount.TabIndex = 25;
			// 
			// butChange
			// 
			this.butChange.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChange.Autosize = true;
			this.butChange.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChange.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChange.CornerRadius = 4F;
			this.butChange.Location = new System.Drawing.Point(346,57);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(75,26);
			this.butChange.TabIndex = 26;
			this.butChange.Text = "Change";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// textCredit
			// 
			this.textCredit.Location = new System.Drawing.Point(205,31);
			this.textCredit.Name = "textCredit";
			this.textCredit.Size = new System.Drawing.Size(90,20);
			this.textCredit.TabIndex = 18;
			// 
			// textDebit
			// 
			this.textDebit.Location = new System.Drawing.Point(110,31);
			this.textDebit.Name = "textDebit";
			this.textDebit.Size = new System.Drawing.Size(90,20);
			this.textDebit.TabIndex = 1;
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
			this.butDelete.Location = new System.Drawing.Point(12,219);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,26);
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
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
			this.butCancel.Location = new System.Drawing.Point(430,219);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 9;
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
			this.butOK.Location = new System.Drawing.Point(430,181);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 8;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// labelReconcile
			// 
			this.labelReconcile.Location = new System.Drawing.Point(110,199);
			this.labelReconcile.Name = "labelReconcile";
			this.labelReconcile.Size = new System.Drawing.Size(230,21);
			this.labelReconcile.TabIndex = 27;
			this.labelReconcile.Text = "Attached to Reconcile";
			this.labelReconcile.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textReconcile
			// 
			this.textReconcile.Location = new System.Drawing.Point(110,222);
			this.textReconcile.Name = "textReconcile";
			this.textReconcile.ReadOnly = true;
			this.textReconcile.Size = new System.Drawing.Size(112,20);
			this.textReconcile.TabIndex = 28;
			// 
			// FormJournalEntryEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(517,257);
			this.Controls.Add(this.textReconcile);
			this.Controls.Add(this.labelReconcile);
			this.Controls.Add(this.butChange);
			this.Controls.Add(this.textAccount);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textCheckNumber);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textCredit);
			this.Controls.Add(this.labelCredit);
			this.Controls.Add(this.textMemo);
			this.Controls.Add(this.textDebit);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.labelDebit);
			this.Controls.Add(this.labelMemo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormJournalEntryEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Journal Entry";
			this.Load += new System.EventHandler(this.FormJournalEntryEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormJournalEntryEdit_Load(object sender, System.EventArgs e) {
			if(EntryCur==null){
				MessageBox.Show("Entry cannot be null.");
			}
			AccountPicked=Accounts.GetAccount(EntryCur.AccountNum);//might be null
			/*
			for(int i=0;i<Accounts.ListShort.Length;i++) {
				comboAccount.Items.Add(Accounts.ListShort[i].Description);
				if(Accounts.ListShort[i].AccountNum==EntryCur.AccountNum){
					comboAccount.SelectedIndex=i;
				}
			}
			if(EntryCur.AccountNum !=0 && comboAccount.SelectedIndex==-1){//must be an inactive account
				
			}*/
			FillAccount();
			if(EntryCur.DebitAmt>0){
				textDebit.Text=EntryCur.DebitAmt.ToString("n");
			}
			if(EntryCur.CreditAmt>0) {
				textCredit.Text=EntryCur.CreditAmt.ToString("n");
			}
			textMemo.Text=EntryCur.Memo;
			textCheckNumber.Text=EntryCur.CheckNumber;
			if(EntryCur.ReconcileNum==0){//not attached
				labelReconcile.Visible=false;
				textReconcile.Visible=false;
			}
			else{//attached
				textReconcile.Text=Reconciles.GetOne(EntryCur.ReconcileNum).DateReconcile.ToShortDateString();
				textDebit.ReadOnly=true;
				textCredit.ReadOnly=true;
				butDelete.Enabled=false;
				butChange.Enabled=false;
			}
		}

		///<summary>Need to set AccountPicked before calling this.</summary>
		private void FillAccount(){
			if(AccountPicked==null){
				textAccount.Text="";
				butChange.Text=Lan.g(this,"Pick");
				labelDebit.Text=Lan.g(this,"Debit");
				labelCredit.Text=Lan.g(this,"Credit");
				return;
			}
			//AccountCur=Accounts.ListShort[comboAccount.SelectedIndex];
			textAccount.Text=AccountPicked.Description;
			butChange.Text=Lan.g(this,"Change");
			if(Accounts.DebitIsPos(AccountPicked.AcctType)) {
				labelDebit.Text=Lan.g(this,"Debit")+Lan.g(this,"(+)");
				labelCredit.Text=Lan.g(this,"Credit")+Lan.g(this,"(-)");
			}
			else {
				labelDebit.Text=Lan.g(this,"Debit")+Lan.g(this,"(-)");
				labelCredit.Text=Lan.g(this,"Credit")+Lan.g(this,"(+)");
			}
		}

		/*private void comboAccount_SelectedIndexChanged(object sender,EventArgs e) {
			FillAccount();
		}*/

		private void butChange_Click(object sender,EventArgs e) {
			FormAccountPick FormA=new FormAccountPick();
			FormA.ShowDialog();
			if(FormA.DialogResult!=DialogResult.OK){
				return;
			}
			AccountPicked=FormA.SelectedAccount;
			FillAccount();
		}

		private void butDelete_Click(object sender,System.EventArgs e) {
			EntryCur=null;
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if( textDebit.errorProvider1.GetError(textDebit)!=""
				|| textCredit.errorProvider1.GetError(textCredit)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			/*if(comboAccount.SelectedIndex==-1){
				MsgBox.Show(this,"Please select an account first.");
				return;
			}*/
			if(PIn.PDouble(textDebit.Text)<0 || PIn.PDouble(textCredit.Text)<0){
				MsgBox.Show(this,"Both amounts not allowed to be less than 0.");
				return;
			}
			if(PIn.PDouble(textDebit.Text)==0 && PIn.PDouble(textCredit.Text)==0) {
				MsgBox.Show(this,"One amount must be filled in.");
				return;
			}
			if(PIn.PDouble(textDebit.Text)>0 && PIn.PDouble(textCredit.Text)>0) {
				MsgBox.Show(this,"Only one amount can be filled in.");
				return;
			}
			EntryCur.AccountNum=AccountPicked.AccountNum;
			EntryCur.DebitAmt=PIn.PDouble(textDebit.Text);
			EntryCur.CreditAmt=PIn.PDouble(textCredit.Text);
			EntryCur.Memo=textMemo.Text;
			EntryCur.CheckNumber=textCheckNumber.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			if(IsNew){
				EntryCur=null;
			}
			DialogResult=DialogResult.Cancel;
		}

		

		

	

	}


}
