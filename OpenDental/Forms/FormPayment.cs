/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormPayment : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textCheckNum;
		private System.Windows.Forms.TextBox textBankBranch;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textTotal;
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew=false;
		private OpenDental.ValidDate textDate;
		private OpenDental.ValidDouble textAmount;
		private Adjustments Adjustments=new Adjustments();
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ListBox listPayType;
		private double tot=0;
		private System.Windows.Forms.Label label9;
		private OpenDental.UI.Button butDeleteAll;
		//private double[] startBal;
		//private double[] newBal;
		//private int patI;
		private System.Windows.Forms.Label label10;
		//private int paymentCount;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.CheckBox checkPayPlan;
		private OpenDental.ODtextBox textNote;//(not including discounts)
		//private bool NoPermission=false;
		private Patient PatCur;
		private Family FamCur;
		//private PaySplit[] PaySplitPaymentList;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textPaidBy;
		private Payment PaymentCur;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		private OpenDental.ValidDate textDateEntry;
		private System.Windows.Forms.Label label12;
		private Label labelDepositAccount;
		private ComboBox comboDepositAccount;
		///<summary>Set this value to a PaySplitNum if you want one of the splits highlighted when opening this form.</summary>
		public int InitialPaySplit;
		///<summary>An array list of PaySplits</summary>
		private ArrayList SplitList;
		private OpenDental.UI.ODGrid gridMain;
		private TextBox textDepositAccount;
		private ArrayList SplitListOld;
		private OpenDental.UI.Button butEditAccounting;
		private Panel panelXcharge;
		private ContextMenu contextMenuXcharge;
		private MenuItem menuXcharge;
		private int[] DepositAccounts;

		///<summary>PatCur and FamCur are not for the PatCur of the payment.  They are for the patient and family from which this window was accessed.</summary>
		public FormPayment(Patient patCur,Family famCur,Payment paymentCur){
			InitializeComponent();// Required for Windows Form Designer support
			PatCur=patCur;
			FamCur=famCur;
			PaymentCur=paymentCur;
			Lan.F(this);
			panelXcharge.ContextMenu=contextMenuXcharge;
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPayment));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textCheckNum = new System.Windows.Forms.TextBox();
			this.textBankBranch = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textTotal = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.listPayType = new System.Windows.Forms.ListBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.checkPayPlan = new System.Windows.Forms.CheckBox();
			this.textPaidBy = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.textDepositAccount = new System.Windows.Forms.TextBox();
			this.labelDepositAccount = new System.Windows.Forms.Label();
			this.comboDepositAccount = new System.Windows.Forms.ComboBox();
			this.panelXcharge = new System.Windows.Forms.Panel();
			this.contextMenuXcharge = new System.Windows.Forms.ContextMenu();
			this.menuXcharge = new System.Windows.Forms.MenuItem();
			this.butEditAccounting = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.textDateEntry = new OpenDental.ValidDate();
			this.textNote = new OpenDental.ODtextBox();
			this.textAmount = new OpenDental.ValidDouble();
			this.textDate = new OpenDental.ValidDate();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butDeleteAll = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(405,2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(154,16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Payment Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12,162);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92,16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4,144);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Bank-Branch";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(4,124);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Check #";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4,104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,16);
			this.label5.TabIndex = 11;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4,84);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100,16);
			this.label6.TabIndex = 12;
			this.label6.Text = "Payment Date";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCheckNum
			// 
			this.textCheckNum.Location = new System.Drawing.Point(106,120);
			this.textCheckNum.Name = "textCheckNum";
			this.textCheckNum.Size = new System.Drawing.Size(100,20);
			this.textCheckNum.TabIndex = 2;
			// 
			// textBankBranch
			// 
			this.textBankBranch.Location = new System.Drawing.Point(106,140);
			this.textBankBranch.Name = "textBankBranch";
			this.textBankBranch.Size = new System.Drawing.Size(100,20);
			this.textBankBranch.TabIndex = 3;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(292,474);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(362,14);
			this.label7.TabIndex = 18;
			this.label7.Text = "(must match total amount of payment)";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textTotal
			// 
			this.textTotal.Location = new System.Drawing.Point(576,448);
			this.textTotal.Name = "textTotal";
			this.textTotal.ReadOnly = true;
			this.textTotal.Size = new System.Drawing.Size(70,20);
			this.textTotal.TabIndex = 19;
			this.textTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(474,452);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100,16);
			this.label8.TabIndex = 22;
			this.label8.Text = "Total Splits:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listPayType
			// 
			this.listPayType.Location = new System.Drawing.Point(407,22);
			this.listPayType.Name = "listPayType";
			this.listPayType.Size = new System.Drawing.Size(120,95);
			this.listPayType.TabIndex = 4;
			this.listPayType.Click += new System.EventHandler(this.listPayType_Click);
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label9.Location = new System.Drawing.Point(42,541);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(126,48);
			this.label9.TabIndex = 28;
			this.label9.Text = "Deletes entire payment and all splits";
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.Location = new System.Drawing.Point(688,541);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(116,46);
			this.label10.TabIndex = 29;
			this.label10.Text = "Cancel does not undo changes to Splits";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkPayPlan
			// 
			this.checkPayPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkPayPlan.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPayPlan.Location = new System.Drawing.Point(322,528);
			this.checkPayPlan.Name = "checkPayPlan";
			this.checkPayPlan.Size = new System.Drawing.Size(216,18);
			this.checkPayPlan.TabIndex = 30;
			this.checkPayPlan.Text = "Attached to Payment Plan";
			this.checkPayPlan.Click += new System.EventHandler(this.checkPayPlan_Click);
			// 
			// textPaidBy
			// 
			this.textPaidBy.Location = new System.Drawing.Point(106,40);
			this.textPaidBy.Name = "textPaidBy";
			this.textPaidBy.ReadOnly = true;
			this.textPaidBy.Size = new System.Drawing.Size(242,20);
			this.textPaidBy.TabIndex = 32;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(4,42);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100,16);
			this.label11.TabIndex = 33;
			this.label11.Text = "Paid By";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(106,18);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(198,21);
			this.comboClinic.TabIndex = 92;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(16,22);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(86,14);
			this.labelClinic.TabIndex = 91;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(4,64);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100,16);
			this.label12.TabIndex = 94;
			this.label12.Text = "Entry Date";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDepositAccount
			// 
			this.textDepositAccount.Location = new System.Drawing.Point(405,189);
			this.textDepositAccount.Name = "textDepositAccount";
			this.textDepositAccount.ReadOnly = true;
			this.textDepositAccount.Size = new System.Drawing.Size(260,20);
			this.textDepositAccount.TabIndex = 115;
			// 
			// labelDepositAccount
			// 
			this.labelDepositAccount.Location = new System.Drawing.Point(405,144);
			this.labelDepositAccount.Name = "labelDepositAccount";
			this.labelDepositAccount.Size = new System.Drawing.Size(260,18);
			this.labelDepositAccount.TabIndex = 114;
			this.labelDepositAccount.Text = "Pay into Account";
			this.labelDepositAccount.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboDepositAccount
			// 
			this.comboDepositAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDepositAccount.FormattingEnabled = true;
			this.comboDepositAccount.Location = new System.Drawing.Point(405,164);
			this.comboDepositAccount.Name = "comboDepositAccount";
			this.comboDepositAccount.Size = new System.Drawing.Size(260,21);
			this.comboDepositAccount.TabIndex = 113;
			// 
			// panelXcharge
			// 
			this.panelXcharge.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelXcharge.BackgroundImage")));
			this.panelXcharge.Location = new System.Drawing.Point(556,22);
			this.panelXcharge.Name = "panelXcharge";
			this.panelXcharge.Size = new System.Drawing.Size(59,26);
			this.panelXcharge.TabIndex = 118;
			this.panelXcharge.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelXcharge_MouseClick);
			// 
			// contextMenuXcharge
			// 
			this.contextMenuXcharge.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuXcharge});
			// 
			// menuXcharge
			// 
			this.menuXcharge.Index = 0;
			this.menuXcharge.Text = "Settings";
			this.menuXcharge.Click += new System.EventHandler(this.menuXcharge_Click);
			// 
			// butEditAccounting
			// 
			this.butEditAccounting.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEditAccounting.Autosize = true;
			this.butEditAccounting.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditAccounting.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditAccounting.CornerRadius = 4F;
			this.butEditAccounting.Location = new System.Drawing.Point(671,161);
			this.butEditAccounting.Name = "butEditAccounting";
			this.butEditAccounting.Size = new System.Drawing.Size(75,26);
			this.butEditAccounting.TabIndex = 117;
			this.butEditAccounting.Text = "Edit";
			this.butEditAccounting.Click += new System.EventHandler(this.butEditAccounting_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(106,244);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(559,198);
			this.gridMain.TabIndex = 116;
			this.gridMain.Title = "Payment Splits (optional)";
			this.gridMain.TranslationName = "TablePaySplits";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// textDateEntry
			// 
			this.textDateEntry.Location = new System.Drawing.Point(106,60);
			this.textDateEntry.Name = "textDateEntry";
			this.textDateEntry.ReadOnly = true;
			this.textDateEntry.Size = new System.Drawing.Size(100,20);
			this.textDateEntry.TabIndex = 93;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(106,162);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Payment;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(290,80);
			this.textNote.TabIndex = 31;
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(106,100);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(84,20);
			this.textAmount.TabIndex = 1;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(106,80);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(100,20);
			this.textDate.TabIndex = 0;
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
			this.butCancel.Location = new System.Drawing.Point(728,507);
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
			this.butOK.Location = new System.Drawing.Point(728,471);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 8;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDeleteAll
			// 
			this.butDeleteAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDeleteAll.Autosize = true;
			this.butDeleteAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDeleteAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDeleteAll.CornerRadius = 4F;
			this.butDeleteAll.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDeleteAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDeleteAll.Location = new System.Drawing.Point(45,507);
			this.butDeleteAll.Name = "butDeleteAll";
			this.butDeleteAll.Size = new System.Drawing.Size(84,26);
			this.butDeleteAll.TabIndex = 7;
			this.butDeleteAll.Text = "&Delete";
			this.butDeleteAll.Click += new System.EventHandler(this.butDeleteAll_Click);
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
			this.butAdd.Location = new System.Drawing.Point(105,445);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(92,26);
			this.butAdd.TabIndex = 30;
			this.butAdd.Text = "&Add Split";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormPayment
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(840,588);
			this.Controls.Add(this.panelXcharge);
			this.Controls.Add(this.butEditAccounting);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.textDepositAccount);
			this.Controls.Add(this.labelDepositAccount);
			this.Controls.Add(this.comboDepositAccount);
			this.Controls.Add(this.textDateEntry);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.textPaidBy);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.textTotal);
			this.Controls.Add(this.textBankBranch);
			this.Controls.Add(this.textCheckNum);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butDeleteAll);
			this.Controls.Add(this.checkPayPlan);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.listPayType);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPayment";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Payment";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormPayment_Closing);
			this.Load += new System.EventHandler(this.FormPayment_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPayment_Load(object sender, System.EventArgs e) {
			if(IsNew){
				if(!Security.IsAuthorized(Permissions.PaymentCreate)){
					DialogResult=DialogResult.Cancel;
					return;
				}
			}
			else{
				if(!Security.IsAuthorized(Permissions.PaymentEdit,PaymentCur.DateEntry)){
					butOK.Enabled=false;
					butDeleteAll.Enabled=false;
					butAdd.Enabled=false;
					gridMain.Enabled=false;
				}
			}
			if(PrefB.GetBool("EasyNoClinics")){
				comboClinic.Visible=false;
				labelClinic.Visible=false;
			}
			else{
				comboClinic.Items.Clear();
				comboClinic.Items.Add(Lan.g(this,"none"));
				comboClinic.SelectedIndex=0;
				for(int i=0;i<Clinics.List.Length;i++){
					comboClinic.Items.Add(Clinics.List[i].Description);
					if(Clinics.List[i].ClinicNum==PaymentCur.ClinicNum){
						comboClinic.SelectedIndex=i+1;
					}
				}
			}
			//this works even if patient not if family
			textPaidBy.Text=FamCur.GetNameInFamFL(PaymentCur.PatNum);
			textDateEntry.Text=PaymentCur.DateEntry.ToShortDateString();
			textDate.Text=PaymentCur.PayDate.ToShortDateString();
			textAmount.Text=PaymentCur.PayAmt.ToString("F");
			textCheckNum.Text=PaymentCur.CheckNum;
			textBankBranch.Text=PaymentCur.BankBranch;
			for(int i=0;i<DefB.Short[(int)DefCat.PaymentTypes].Length;i++){
				listPayType.Items.Add(DefB.Short[(int)DefCat.PaymentTypes][i].ItemName);
				if(DefB.Short[(int)DefCat.PaymentTypes][i].DefNum==PaymentCur.PayType)
					listPayType.SelectedIndex=i;
			}
			if(listPayType.SelectedIndex==-1)
				listPayType.SelectedIndex=0;
			textNote.Text=PaymentCur.PayNote;
			SplitList=PaySplits.GetForPayment(PaymentCur.PayNum);//Count might be 0
			SplitListOld=new ArrayList();
			for(int i=0;i<SplitList.Count;i++) {
				SplitListOld.Add(((PaySplit)SplitList[i]).Copy());
			}
			if(IsNew) {
				PayPlan payPlanCur=PayPlans.GetValidPlan(PatCur.PatNum,false);
				if(payPlanCur!=null) {//a valid payPlan was located
					AddOneSplit();//the amount and date will be updated upon closing
					((PaySplit)SplitList[SplitList.Count-1]).PayPlanNum=payPlanCur.PayPlanNum;
				}
			}
			FillTable();
			if(InitialPaySplit!=0){
				for(int i=0;i<SplitList.Count;i++){
					if(InitialPaySplit==((PaySplit)SplitList[i]).SplitNum){
						gridMain.SetSelected(i,true);
					}
				}
			}
			if(IsNew) {
				textDepositAccount.Visible=false;//this is never visible for new. It's a description if already attached.
				butEditAccounting.Visible=false;//there's no transaction to edit since not attached yet.
				if(Accounts.PaymentsLinked()) {
					AccountingAutoPay autoPay=AccountingAutoPays.GetForPayType(
						DefB.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndex].DefNum);
					if(autoPay==null){
						labelDepositAccount.Visible=false;
						comboDepositAccount.Visible=false;
					}
					else{
						DepositAccounts=AccountingAutoPays.GetPickListAccounts(autoPay);
						for(int i=0;i<DepositAccounts.Length;i++) {
							comboDepositAccount.Items.Add(Accounts.GetDescript(DepositAccounts[i]));
						}
						comboDepositAccount.SelectedIndex=0;
					}
				}
				else {
					labelDepositAccount.Visible=false;
					comboDepositAccount.Visible=false;
				}
			}
			else {
				//User will have to use the edit button.  Not allowed to pick from list like when new.
				Transaction trans=Transactions.GetAttachedToPayment(PaymentCur.PayNum);
				if(trans==null) {
					labelDepositAccount.Visible=false;
					butEditAccounting.Visible=false;
					comboDepositAccount.Visible=false;
					textDepositAccount.Visible=false;
				}
				else {
					comboDepositAccount.Enabled=false;
					labelDepositAccount.Text=Lan.g(this,"Payed into Account");
					ArrayList jeAL=JournalEntries.GetForTrans(trans.TransactionNum);
					for(int i=0;i<jeAL.Count;i++) {
						if(Accounts.GetAccount(((JournalEntry)jeAL[i]).AccountNum).AcctType==AccountType.Asset) {
							comboDepositAccount.Items.Add(Accounts.GetDescript(((JournalEntry)jeAL[i]).AccountNum));
							comboDepositAccount.SelectedIndex=0;
							textDepositAccount.Text=((JournalEntry)jeAL[i]).DateDisplayed.ToShortDateString();
							if(((JournalEntry)jeAL[i]).DebitAmt>0){
								textDepositAccount.Text+=" "+((JournalEntry)jeAL[i]).DebitAmt.ToString("c");
							}
							else{//negative
								textDepositAccount.Text+=" "+(-((JournalEntry)jeAL[i]).CreditAmt).ToString("c");
							}
							break;
						}
					}
				}
			}
		}

		///<summary>This does not make any calls to db (except one tiny one).  Simply refreshes screen for SplitList.</summary>
		private void FillTable(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePaySplits","Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Prov"),45);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Patient"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Tth"),40,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Procedure"),170);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Amount"),65,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			tot=0;
			Procedure proc;
			for(int i=0;i<SplitList.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(((PaySplit)SplitList[i]).ProcDate.ToShortDateString());
				row.Cells.Add(Providers.GetAbbr(((PaySplit)SplitList[i]).ProvNum));
				row.Cells.Add(FamCur.GetNameInFamLF(((PaySplit)SplitList[i]).PatNum));
				if(((PaySplit)SplitList[i]).ProcNum>0){
					proc=Procedures.GetOneProc(((PaySplit)SplitList[i]).ProcNum,false);
					row.Cells.Add(Tooth.ToInternat(proc.ToothNum));
					row.Cells.Add(ProcedureCodes.GetProcCode(proc.CodeNum).Descript);
				}
				else{
					row.Cells.Add("");
					row.Cells.Add("");
				}
				row.Cells.Add(((PaySplit)SplitList[i]).SplitAmt.ToString("F"));
				tot+=((PaySplit)SplitList[i]).SplitAmt;
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			textTotal.Text=tot.ToString("F");
			if(SplitList.Count==1){
				checkPayPlan.Enabled=true;
				if(((PaySplit)SplitList[0]).PayPlanNum>0){
					checkPayPlan.Checked=true;
				}
				else{
					checkPayPlan.Checked=false;
				}
			}
			else{
				checkPayPlan.Checked=false;
				checkPayPlan.Enabled=false;
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormPaySplitEdit FormPS=new FormPaySplitEdit(FamCur);
			FormPS.PaySplitCur=(PaySplit)SplitList[e.Row];
			FormPS.Remain=PaymentCur.PayAmt-PIn.PDouble(textTotal.Text)+((PaySplit)SplitList[e.Row]).SplitAmt;
			FormPS.ShowDialog();
			if(FormPS.PaySplitCur==null) {//user deleted
				SplitList.RemoveAt(e.Row);
			}
			//if(FormPS.ShowDialog()==DialogResult.OK){
			FillTable();
			//}
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			PaySplit PaySplitCur=new PaySplit();
			PaySplitCur.PayNum=PaymentCur.PayNum;
			PaySplitCur.DatePay=PIn.PDate(textDate.Text);//this may be updated upon closing
			PaySplitCur.ProcDate=PIn.PDate(textDate.Text);//this may be updated upon closing
			PaySplitCur.ProvNum=Patients.GetProvNum(PatCur);
			PaySplitCur.PatNum=PatCur.PatNum;
			FormPaySplitEdit FormPS=new FormPaySplitEdit(FamCur);
			FormPS.PaySplitCur=PaySplitCur;
			FormPS.IsNew=true;
			FormPS.Remain=PaymentCur.PayAmt-PIn.PDouble(textTotal.Text);
			if(FormPS.ShowDialog()!=DialogResult.OK){
				return;
			}
			SplitList.Add(PaySplitCur);
			FillTable();
		}

		private void checkPayPlan_Click(object sender, System.EventArgs e) {
			//*****if there is more than one split, then this checkbox is not even available.
			if(SplitList.Count==0){
				AddOneSplit();//won't use returned value
				FillTable();
				checkPayPlan.Checked=true;
				//now there is exactly one.  The amount will be updated as the form closes.
			}
			if(checkPayPlan.Checked){
				PayPlan payPlanCur=PayPlans.GetValidPlan(((PaySplit)SplitList[0]).PatNum,false);
				if(payPlanCur==null){//no valid plans
					MsgBox.Show(this,"The selected patient is not the guarantor for any payment plans.");
					checkPayPlan.Checked=false;
					return;
				}
				((PaySplit)SplitList[0]).PayPlanNum=payPlanCur.PayPlanNum;
			}
			else{//payPlan unchecked
				((PaySplit)SplitList[0]).PayPlanNum=0;
			}
			FillTable();
		}

		/// <summary>Adds one split to work with.  Called when checkPayPlan click, or upon load if auto attaching to payplan.</summary>
		private void AddOneSplit(){
			PaySplit PaySplitCur=new PaySplit();
			PaySplitCur.PatNum=PatCur.PatNum;
			PaySplitCur.PayNum=PaymentCur.PayNum;
			PaySplitCur.ProcDate=PaymentCur.PayDate;//this may be updated upon closing
			PaySplitCur.DatePay=PaymentCur.PayDate;//this may be updated upon closing
			PaySplitCur.ProvNum=Patients.GetProvNum(PatCur);
			PaySplitCur.SplitAmt=PIn.PDouble(textAmount.Text);
			SplitList.Add(PaySplitCur);
		}

		private void listPayType_Click(object sender,EventArgs e) {
			if(!Accounts.PaymentsLinked()) {
				return;
			}
			if(!IsNew){
				//we should probably do something here if user is switching to a type that is linked
				//like if they entered check payment at first, and then realized it should be cash
				return;
			}
			AccountingAutoPay autoPay=AccountingAutoPays.GetForPayType(
				DefB.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndex].DefNum);
			if(autoPay==null) {
				labelDepositAccount.Visible=false;
				comboDepositAccount.Visible=false;
			}
			else {
				labelDepositAccount.Visible=true;
				comboDepositAccount.Visible=true;
				DepositAccounts=AccountingAutoPays.GetPickListAccounts(autoPay);
				comboDepositAccount.Items.Clear();
				for(int i=0;i<DepositAccounts.Length;i++) {
					comboDepositAccount.Items.Add(Accounts.GetDescript(DepositAccounts[i]));
				}
				comboDepositAccount.SelectedIndex=0;
			}
		}

		private void panelXcharge_MouseClick(object sender,MouseEventArgs e) {
			if(e.Button != MouseButtons.Left){
				return;
			}
			Program prog=Programs.GetCur("Xcharge");
			if(prog==null){
				MsgBox.Show(this,"X-Charge entry is missing from the database.");//should never happen
				return;
			}
			if(!prog.Enabled){
				if(Security.IsAuthorized(Permissions.Setup)){
					FormXchargeSetup FormX=new FormXchargeSetup();
					FormX.ShowDialog();
				}
				return;
			}
			if(!File.Exists(prog.Path)){
				MsgBox.Show(this,"Path is not valid.");
				if(Security.IsAuthorized(Permissions.Setup)){
					FormXchargeSetup FormX=new FormXchargeSetup();
					FormX.ShowDialog();
				}
				return;
			}
			ProgramProperty prop=(ProgramProperty)ProgramProperties.GetForProgram(prog.ProgramNum)[0];
			//still need to add functionality for accountingAutoPay
			listPayType.SelectedIndex=DefB.GetOrder(DefCat.PaymentTypes,PIn.PInt(prop.PropertyValue));
			/*XCharge.exe [/TRANSACTIONTYPE:type] [/AMOUNT:amount] [/ACCOUNT:account] [/EXP:exp]
				[“/TRACK:track”] [/ZIP:zip] [/ADDRESS:address] [/RECEIPT:receipt] [/CLERK:clerk]
				[/APPROVALCODE:approval] [/AUTOPROCESS] [/AUTOCLOSE] [/STAYONTOP] [/MID]
				[/RESULTFILE:”C:\Program Files\X-Charge\LocalTran\XCResult.txt”*/
			ProcessStartInfo info=new ProcessStartInfo(prog.Path);
			info.Arguments="";
			double amt=PIn.PDouble(textAmount.Text);
			if(amt>0){
				info.Arguments+="/AMOUNT:"+amt.ToString("F2")+" ";
			}
			Patient pat=Patients.GetPat(PaymentCur.PatNum);
			info.Arguments+="\"/ZIP:"+pat.Zip+"\" ";
			info.Arguments+="\"/ADDRESS:"+pat.Address+"\" ";
			info.Arguments+="/RECEIPT:"+PaymentCur.PayNum.ToString()+" ";//aka invoice#
			info.Arguments+="\"/CLERK:"+Security.CurUser.UserName+"\" ";
			info.Arguments+="/AUTOCLOSE ";
			string resultfile=Path.GetDirectoryName(prog.Path)+"XResult.txt";
			info.Arguments+="/RESULTFILE:\""+resultfile+"\"";
			//info.Arguments+="/MID:223496";//what's this?
			Cursor=Cursors.WaitCursor;
			Process process=new Process();
			process.StartInfo=info;
			process.EnableRaisingEvents=true;
			process.Start();
			process.WaitForExit();
			Cursor=Cursors.Default;
			string resulttext="";
			string line="";
			using(TextReader reader=new StreamReader(resultfile)){
				line=reader.ReadLine();
				while(line!=null){
					if(resulttext!=""){
						resulttext+="\r\n";
					}
					resulttext+=line;
					if(line.StartsWith("RESULT=")){
						if(line!="RESULT=SUCCESS"){
							break;
						}
					}
					if(line.StartsWith("AMOUNT=")){
						amt=PIn.PDouble(line.Substring(7));
						textAmount.Text=amt.ToString("F2");
					}
					line=reader.ReadLine();
				}
				//resulttext+=reader.ReadToEnd();
				//MessageBox.Show("ResultFile:\r\n"+resultText);
			}
			/*Example of successful transaction:
				RESULT=SUCCESS
				TYPE=Purchase
				APPROVALCODE=000064
				ACCOUNT=XXXXXXXXXXXX6781
				ACCOUNTTYPE=VISA*
				AMOUNT=1.77
				AVSRESULT=Y
				CVRESULT=M
			*/
			if(textNote.Text!=""){
				textNote.Text+="\r\n";
			}
			textNote.Text+=resulttext;
		}

		private void menuXcharge_Click(object sender,EventArgs e) {
			if(Security.IsAuthorized(Permissions.Setup)) {
				FormXchargeSetup FormX=new FormXchargeSetup();
				FormX.ShowDialog();
			}
		}

		private void butEditAccounting_Click(object sender,EventArgs e){
			Transaction trans=Transactions.GetAttachedToPayment(PaymentCur.PayNum);
			if(trans==null) {//this should never happen.  But just in case...
				MsgBox.Show(this,"No transaction to edit.");
				return;
			}
			if(!Security.IsAuthorized(Permissions.AccountingEdit,trans.DateTimeEntry)){
				return;
			}
			FormTransactionEdit FormT=new FormTransactionEdit(trans.TransactionNum,0);
			FormT.ShowDialog();
			//labelDepositAccount.Text=Lan.g(this,"Payed into Account");
			ArrayList jeAL=JournalEntries.GetForTrans(trans.TransactionNum);
			comboDepositAccount.Items.Clear();
			for(int i=0;i<jeAL.Count;i++) {
				if(Accounts.GetAccount(((JournalEntry)jeAL[i]).AccountNum).AcctType==AccountType.Asset) {
					comboDepositAccount.Items.Add(Accounts.GetDescript(((JournalEntry)jeAL[i]).AccountNum));
					comboDepositAccount.SelectedIndex=0;
					textDepositAccount.Text=((JournalEntry)jeAL[i]).DateDisplayed.ToShortDateString()
								+" "+((JournalEntry)jeAL[i]).DebitAmt.ToString("c");
					break;
				}
			}
		}

		private void butDeleteAll_Click(object sender, System.EventArgs e) {
			if(!MsgBox.Show(this,true,"This will delete the entire payment and all splits.")){
				return;
			}
			//If payment is attached to a transaction which is more than 48 hours old, then not allowed to delete.
			//This is hard coded.  User would have to delete or detach from within transaction rather than here.
			Transaction trans=Transactions.GetAttachedToPayment(PaymentCur.PayNum);
			if(trans != null) {
				if(trans.DateTimeEntry < MiscData.GetNowDateTime().AddDays(-2)) {
					MsgBox.Show(this,"Not allowed to delete.  This payment is already attached to an accounting transaction.  You will need to detach it from within the accounting section of the program.");
					return;
				}
				if(Transactions.IsReconciled(trans)){
					MsgBox.Show(this,"Not allowed to delete.  This payment is attached to an accounting transaction that has been reconciled.  You will need to detach it from within the accounting section of the program.");
					return;
				}
				try {
					Transactions.Delete(trans);
				}
				catch(ApplicationException ex) {
					MessageBox.Show(ex.Message);
					return;
				}
			}
			try{
				Payments.Delete(PaymentCur);
			}
			catch(ApplicationException ex){//error if attached to deposit slip
				MessageBox.Show(ex.Message);
				return;
			}
			SecurityLogs.MakeLogEntry(Permissions.PaymentEdit,PaymentCur.PatNum,
				"Delete for: "
				+Patients.GetLim(PaymentCur.PatNum).GetNameLF()+", "
				+PaymentCur.PayAmt.ToString("c"));
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e){
			if(  textDate.errorProvider1.GetError(textDate)!=""
				|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(textAmount.Text==""){
				MessageBox.Show(Lan.g(this,"Please enter an amount."));	
				return;
			}
			if(PIn.PDouble(textAmount.Text)==0) {
				MessageBox.Show(Lan.g(this,"Amount must not be zero."));
				return;
			}
			//If user is trying to change the amount of an entry that was already copied and linked to accounting section
			//(category can only be changed by clicking the edit button.)
			if(!IsNew){
				try{
					Payments.AlterLinkedEntries(PaymentCur,PIn.PDouble(textAmount.Text));
				}
				catch(ApplicationException ex){
					MessageBox.Show(ex.Message);//not able to alter, so must not allow user to continue.
					return;
				}
			}
			PaymentCur.PayAmt=PIn.PDouble(textAmount.Text);
			PaymentCur.PayDate=PIn.PDate(textDate.Text);
			PaymentCur.CheckNum=textCheckNum.Text;
			PaymentCur.BankBranch=textBankBranch.Text;
			PaymentCur.PayNote=textNote.Text;
			PaymentCur.PayType=DefB.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndex].DefNum;
			PaymentCur.PatNum=PatCur.PatNum;
			if(!comboClinic.Visible || Clinics.List.Length==0 || comboClinic.SelectedIndex==0) {
				PaymentCur.ClinicNum=0;
			}
			else {
				PaymentCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			}
			if(SplitList.Count==0) {
				if(Payments.AllocationRequired(PaymentCur.PayAmt,PaymentCur.PatNum)
					&& MsgBox.Show(this,true,"Apply part of payment to other family members?"))
				{
					SplitList=Payments.Allocate(PaymentCur);//PayAmt needs to be set first
				}
				else{//Either no allocation required, or user does not want to allocate.  Just add one split.
					PaySplit split=new PaySplit();
					split.PatNum=PaymentCur.PatNum;
					split.PayNum=PaymentCur.PayNum;
					split.ProcDate=PaymentCur.PayDate;
					split.DatePay=PaymentCur.PayDate;
					split.ProvNum=Patients.GetProvNum(PatCur);
					split.SplitAmt=PaymentCur.PayAmt;
					SplitList.Add(split);
				}
			}
			else if(SplitList.Count==1//if one split
				&& PIn.PDouble(textAmount.Text) != ((PaySplit)SplitList[0]).SplitAmt)//and amount doesn't match payment
			{
				((PaySplit)SplitList[0]).SplitAmt=PIn.PDouble(textAmount.Text);//make amounts match
			}
			else if(SplitList.Count==1//if one split
				&& PaymentCur.PayDate != ((PaySplit)SplitList[0]).ProcDate
				&& ((PaySplit)SplitList[0]).ProcNum==0)//not attached to procedure
			{
				if(MsgBox.Show(this,true,"Change split date to match payment date?")) {
					((PaySplit)SplitList[0]).ProcDate=PaymentCur.PayDate;
				}
			}
			else if(PaymentCur.PayAmt!=PIn.PDouble(textTotal.Text)) {
				MsgBox.Show(this,"Split totals must equal payment amount.");
				//work on reallocation schemes here later
				return;
			}
			if(SplitList.Count>1) {
				PaymentCur.IsSplit=true;
			}
			else{
				PaymentCur.IsSplit=false;
			}
			try{
				Payments.Update(PaymentCur);
			}
			catch(ApplicationException ex){//this catches bad dates.
				MessageBox.Show(ex.Message);
				return;
			}
			//Set all DatePays the same.
			for(int i=0;i<SplitList.Count;i++){
				((PaySplit)SplitList[i]).DatePay=PaymentCur.PayDate;
			}
			PaySplits.UpdateList(SplitListOld,SplitList);
			if(IsNew && Accounts.PaymentsLinked() && comboDepositAccount.Visible && PaymentCur.PayAmt>0) {
				//create a transaction here
				Transaction trans=new Transaction();
				trans.PayNum=PaymentCur.PayNum;
				trans.UserNum=Security.CurUser.UserNum;
				Transactions.Insert(trans);
				//first the deposit entry
				JournalEntry je=new JournalEntry();
				je.AccountNum=DepositAccounts[comboDepositAccount.SelectedIndex];
				je.CheckNumber=Lan.g(this,"DEP");
				je.DateDisplayed=PaymentCur.PayDate;//it would be nice to add security here.
				
				je.DebitAmt=PaymentCur.PayAmt;
				je.Memo=Lan.g(this,"Payment -")+" "+FamCur.GetNameInFamFL(PaymentCur.PatNum);
				je.Splits=Accounts.GetDescript(PrefB.GetInt("AccountingCashIncomeAccount"));
				je.TransactionNum=trans.TransactionNum;
				JournalEntries.Insert(je);
				//then, the income entry
				je=new JournalEntry();
				je.AccountNum=PrefB.GetInt("AccountingCashIncomeAccount");
				//je.CheckNumber=;
				je.DateDisplayed=PaymentCur.PayDate;//it would be nice to add security here.
				je.CreditAmt=PaymentCur.PayAmt;
				je.Memo=Lan.g(this,"Payment -")+" "+FamCur.GetNameInFamFL(PaymentCur.PatNum);
				je.Splits=Accounts.GetDescript(DepositAccounts[comboDepositAccount.SelectedIndex]);
				je.TransactionNum=trans.TransactionNum;
				JournalEntries.Insert(je);
			}
			if(IsNew){
				SecurityLogs.MakeLogEntry(Permissions.PaymentCreate,PaymentCur.PatNum,
					Patients.GetLim(PaymentCur.PatNum).GetNameLF()+", "
					+PaymentCur.PayAmt.ToString("c"));
			}
			else{
			  SecurityLogs.MakeLogEntry(Permissions.PaymentEdit,PaymentCur.PatNum,
					Patients.GetLim(PaymentCur.PatNum).GetNameLF()+", "
					+PaymentCur.PayAmt.ToString("c"));
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {			
			DialogResult=DialogResult.Cancel;
		}

		private void FormPayment_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){ 
				Payments.Delete(PaymentCur);
			}
			//else if(PaymentCur.PayAmt!=tot){
			//	MessageBox.Show(Lan.g(this,"Splits have been altered.  Payment must match splits."));
			//	e.Cancel=true;
			//	return;
			//}	
		}

		

		

		

		

		

		


		
		
	}
}
