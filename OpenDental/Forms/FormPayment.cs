/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
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
		private IContainer components;
		///<summary></summary>
		public bool IsNew=false;
		private OpenDental.ValidDate textDate;
		private OpenDental.ValidDouble textAmount;
		//private Adjustments Adjustments=new Adjustments();
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ListBox listPayType;
		private double tot=0;
		private System.Windows.Forms.Label label9;
		private OpenDental.UI.Button butDeleteAll;
		//private double[] startBal;
		//private double[] newBal;
		//private int patI;
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
		public long InitialPaySplit;
		///<summary></summary>
		private List<PaySplit> SplitList;
		private OpenDental.UI.ODGrid gridMain;
		private List<PaySplit> SplitListOld;
		private Panel panelXcharge;
		private ContextMenu contextMenuXcharge;
		private MenuItem menuXcharge;
		private TextBox textDepositAccount;
		private ODGrid gridBal;
		private long[] DepositAccounts;
		private TextBox textFamStart;
		private Label label10;
		private TextBox textFamEnd;
		private OpenDental.UI.Button butPay;
		private TextBox textDeposit;
		private Label labelDeposit;
		private TextBox textFamAfterIns;
		private CheckBox checkPayTypeNone;
		private OpenDental.UI.Button butPayConnect;
		private ContextMenu contextMenuPayConnect;
		private MenuItem menuPayConnect;
		private ComboBox comboCreditCards;
		private Label labelCreditCards;
		///<summary>This table gets created and filled once at the beginning.  After that, only the last column gets carefully updated.</summary>
		private DataTable tableBalances;

		///<summary>PatCur and FamCur are not for the PatCur of the payment.  They are for the patient and family from which this window was accessed.</summary>
		public FormPayment(Patient patCur,Family famCur,Payment paymentCur){
			InitializeComponent();// Required for Windows Form Designer support
			PatCur=patCur;
			FamCur=famCur;
			PaymentCur=paymentCur;
			Lan.F(this);
			panelXcharge.ContextMenu=contextMenuXcharge;
			butPayConnect.ContextMenu=contextMenuPayConnect;
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
			this.checkPayPlan = new System.Windows.Forms.CheckBox();
			this.textPaidBy = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.labelDepositAccount = new System.Windows.Forms.Label();
			this.comboDepositAccount = new System.Windows.Forms.ComboBox();
			this.panelXcharge = new System.Windows.Forms.Panel();
			this.contextMenuXcharge = new System.Windows.Forms.ContextMenu();
			this.menuXcharge = new System.Windows.Forms.MenuItem();
			this.textDepositAccount = new System.Windows.Forms.TextBox();
			this.textFamStart = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textFamEnd = new System.Windows.Forms.TextBox();
			this.textDeposit = new System.Windows.Forms.TextBox();
			this.labelDeposit = new System.Windows.Forms.Label();
			this.textFamAfterIns = new System.Windows.Forms.TextBox();
			this.checkPayTypeNone = new System.Windows.Forms.CheckBox();
			this.contextMenuPayConnect = new System.Windows.Forms.ContextMenu();
			this.menuPayConnect = new System.Windows.Forms.MenuItem();
			this.butPayConnect = new OpenDental.UI.Button();
			this.butPay = new OpenDental.UI.Button();
			this.gridBal = new OpenDental.UI.ODGrid();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.textDateEntry = new OpenDental.ValidDate();
			this.textNote = new OpenDental.ODtextBox();
			this.textAmount = new OpenDental.ValidDouble();
			this.textDate = new OpenDental.ValidDate();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butDeleteAll = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.comboCreditCards = new System.Windows.Forms.ComboBox();
			this.labelCreditCards = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(404,2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(154,16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Payment Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12,152);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92,16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4,134);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Bank-Branch";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(4,114);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Check #";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4,94);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,16);
			this.label5.TabIndex = 11;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4,74);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100,16);
			this.label6.TabIndex = 12;
			this.label6.Text = "Payment Date";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCheckNum
			// 
			this.textCheckNum.Location = new System.Drawing.Point(106,110);
			this.textCheckNum.Name = "textCheckNum";
			this.textCheckNum.Size = new System.Drawing.Size(100,20);
			this.textCheckNum.TabIndex = 2;
			// 
			// textBankBranch
			// 
			this.textBankBranch.Location = new System.Drawing.Point(106,130);
			this.textBankBranch.Name = "textBankBranch";
			this.textBankBranch.Size = new System.Drawing.Size(100,20);
			this.textBankBranch.TabIndex = 3;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(212,464);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(286,14);
			this.label7.TabIndex = 18;
			this.label7.Text = "(must match total amount of payment)";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textTotal
			// 
			this.textTotal.Location = new System.Drawing.Point(425,438);
			this.textTotal.Name = "textTotal";
			this.textTotal.ReadOnly = true;
			this.textTotal.Size = new System.Drawing.Size(67,20);
			this.textTotal.TabIndex = 19;
			this.textTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(324,442);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100,16);
			this.label8.TabIndex = 22;
			this.label8.Text = "Total Splits";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listPayType
			// 
			this.listPayType.Location = new System.Drawing.Point(407,39);
			this.listPayType.Name = "listPayType";
			this.listPayType.Size = new System.Drawing.Size(120,95);
			this.listPayType.TabIndex = 4;
			this.listPayType.Click += new System.EventHandler(this.listPayType_Click);
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label9.Location = new System.Drawing.Point(97,512);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(126,37);
			this.label9.TabIndex = 28;
			this.label9.Text = "Deletes entire payment and all splits";
			this.label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkPayPlan
			// 
			this.checkPayPlan.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPayPlan.Location = new System.Drawing.Point(694,108);
			this.checkPayPlan.Name = "checkPayPlan";
			this.checkPayPlan.Size = new System.Drawing.Size(196,18);
			this.checkPayPlan.TabIndex = 30;
			this.checkPayPlan.Text = "Attached to Payment Plan";
			this.checkPayPlan.Click += new System.EventHandler(this.checkPayPlan_Click);
			// 
			// textPaidBy
			// 
			this.textPaidBy.Location = new System.Drawing.Point(106,30);
			this.textPaidBy.Name = "textPaidBy";
			this.textPaidBy.ReadOnly = true;
			this.textPaidBy.Size = new System.Drawing.Size(242,20);
			this.textPaidBy.TabIndex = 32;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(4,32);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100,16);
			this.label11.TabIndex = 33;
			this.label11.Text = "Paid By";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(106,8);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(198,21);
			this.comboClinic.TabIndex = 92;
			this.comboClinic.SelectionChangeCommitted += new System.EventHandler(this.comboClinic_SelectionChangeCommitted);
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(16,12);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(86,14);
			this.labelClinic.TabIndex = 91;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(4,54);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100,16);
			this.label12.TabIndex = 94;
			this.label12.Text = "Entry Date";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelDepositAccount
			// 
			this.labelDepositAccount.Location = new System.Drawing.Point(407,138);
			this.labelDepositAccount.Name = "labelDepositAccount";
			this.labelDepositAccount.Size = new System.Drawing.Size(260,17);
			this.labelDepositAccount.TabIndex = 114;
			this.labelDepositAccount.Text = "Pay into Account";
			this.labelDepositAccount.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboDepositAccount
			// 
			this.comboDepositAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDepositAccount.FormattingEnabled = true;
			this.comboDepositAccount.Location = new System.Drawing.Point(407,157);
			this.comboDepositAccount.Name = "comboDepositAccount";
			this.comboDepositAccount.Size = new System.Drawing.Size(260,21);
			this.comboDepositAccount.TabIndex = 113;
			// 
			// panelXcharge
			// 
			this.panelXcharge.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelXcharge.BackgroundImage")));
			this.panelXcharge.Location = new System.Drawing.Point(694,12);
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
			// textDepositAccount
			// 
			this.textDepositAccount.Location = new System.Drawing.Point(407,181);
			this.textDepositAccount.Name = "textDepositAccount";
			this.textDepositAccount.ReadOnly = true;
			this.textDepositAccount.Size = new System.Drawing.Size(260,20);
			this.textDepositAccount.TabIndex = 119;
			// 
			// textFamStart
			// 
			this.textFamStart.Location = new System.Drawing.Point(773,438);
			this.textFamStart.Name = "textFamStart";
			this.textFamStart.ReadOnly = true;
			this.textFamStart.Size = new System.Drawing.Size(60,20);
			this.textFamStart.TabIndex = 121;
			this.textFamStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(672,441);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100,16);
			this.label10.TabIndex = 122;
			this.label10.Text = "Family Total";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFamEnd
			// 
			this.textFamEnd.Location = new System.Drawing.Point(893,438);
			this.textFamEnd.Name = "textFamEnd";
			this.textFamEnd.ReadOnly = true;
			this.textFamEnd.Size = new System.Drawing.Size(60,20);
			this.textFamEnd.TabIndex = 123;
			this.textFamEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDeposit
			// 
			this.textDeposit.Location = new System.Drawing.Point(694,158);
			this.textDeposit.Name = "textDeposit";
			this.textDeposit.ReadOnly = true;
			this.textDeposit.Size = new System.Drawing.Size(100,20);
			this.textDeposit.TabIndex = 125;
			// 
			// labelDeposit
			// 
			this.labelDeposit.ForeColor = System.Drawing.Color.Firebrick;
			this.labelDeposit.Location = new System.Drawing.Point(691,139);
			this.labelDeposit.Name = "labelDeposit";
			this.labelDeposit.Size = new System.Drawing.Size(199,16);
			this.labelDeposit.TabIndex = 126;
			this.labelDeposit.Text = "Attached to deposit";
			this.labelDeposit.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textFamAfterIns
			// 
			this.textFamAfterIns.Location = new System.Drawing.Point(833,438);
			this.textFamAfterIns.Name = "textFamAfterIns";
			this.textFamAfterIns.ReadOnly = true;
			this.textFamAfterIns.Size = new System.Drawing.Size(60,20);
			this.textFamAfterIns.TabIndex = 127;
			this.textFamAfterIns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// checkPayTypeNone
			// 
			this.checkPayTypeNone.Location = new System.Drawing.Point(407,21);
			this.checkPayTypeNone.Name = "checkPayTypeNone";
			this.checkPayTypeNone.Size = new System.Drawing.Size(204,18);
			this.checkPayTypeNone.TabIndex = 128;
			this.checkPayTypeNone.Text = "None (Income Transfer)";
			this.checkPayTypeNone.UseVisualStyleBackColor = true;
			this.checkPayTypeNone.CheckedChanged += new System.EventHandler(this.checkPayTypeNone_CheckedChanged);
			this.checkPayTypeNone.Click += new System.EventHandler(this.checkPayTypeNone_Click);
			// 
			// contextMenuPayConnect
			// 
			this.contextMenuPayConnect.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuPayConnect});
			// 
			// menuPayConnect
			// 
			this.menuPayConnect.Index = 0;
			this.menuPayConnect.Text = "Settings";
			this.menuPayConnect.Click += new System.EventHandler(this.menuPayConnect_Click);
			// 
			// butPayConnect
			// 
			this.butPayConnect.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPayConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPayConnect.Autosize = false;
			this.butPayConnect.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPayConnect.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPayConnect.CornerRadius = 4F;
			this.butPayConnect.Location = new System.Drawing.Point(782,13);
			this.butPayConnect.Name = "butPayConnect";
			this.butPayConnect.Size = new System.Drawing.Size(75,24);
			this.butPayConnect.TabIndex = 129;
			this.butPayConnect.Text = "PayConnect";
			this.butPayConnect.Click += new System.EventHandler(this.butPayConnect_Click);
			// 
			// butPay
			// 
			this.butPay.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPay.Autosize = true;
			this.butPay.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPay.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPay.CornerRadius = 4F;
			this.butPay.Image = global::OpenDental.Properties.Resources.Left;
			this.butPay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPay.Location = new System.Drawing.Point(588,208);
			this.butPay.Name = "butPay";
			this.butPay.Size = new System.Drawing.Size(79,24);
			this.butPay.TabIndex = 124;
			this.butPay.Text = "Pay";
			this.butPay.Click += new System.EventHandler(this.butPay_Click);
			// 
			// gridBal
			// 
			this.gridBal.HScrollVisible = false;
			this.gridBal.Location = new System.Drawing.Point(588,234);
			this.gridBal.Name = "gridBal";
			this.gridBal.ScrollValue = 0;
			this.gridBal.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridBal.Size = new System.Drawing.Size(381,198);
			this.gridBal.TabIndex = 120;
			this.gridBal.Title = "Family Balances";
			this.gridBal.TranslationName = "TablePaymentBal";
			this.gridBal.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridBal_CellDoubleClick);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(7,234);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(575,198);
			this.gridMain.TabIndex = 116;
			this.gridMain.Title = "Payment Splits (optional)";
			this.gridMain.TranslationName = "TablePaySplits";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// textDateEntry
			// 
			this.textDateEntry.Location = new System.Drawing.Point(106,50);
			this.textDateEntry.Name = "textDateEntry";
			this.textDateEntry.ReadOnly = true;
			this.textDateEntry.Size = new System.Drawing.Size(100,20);
			this.textDateEntry.TabIndex = 93;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(106,152);
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
			this.textAmount.Location = new System.Drawing.Point(106,90);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(84,20);
			this.textAmount.TabIndex = 1;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(106,70);
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
			this.butCancel.Location = new System.Drawing.Point(887,523);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
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
			this.butOK.Location = new System.Drawing.Point(806,523);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
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
			this.butDeleteAll.Location = new System.Drawing.Point(7,523);
			this.butDeleteAll.Name = "butDeleteAll";
			this.butDeleteAll.Size = new System.Drawing.Size(84,24);
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
			this.butAdd.Location = new System.Drawing.Point(7,435);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(92,24);
			this.butAdd.TabIndex = 30;
			this.butAdd.Text = "&Add Split";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// comboCreditCards
			// 
			this.comboCreditCards.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCreditCards.Location = new System.Drawing.Point(694,65);
			this.comboCreditCards.MaxDropDownItems = 30;
			this.comboCreditCards.Name = "comboCreditCards";
			this.comboCreditCards.Size = new System.Drawing.Size(198,21);
			this.comboCreditCards.TabIndex = 130;
			// 
			// labelCreditCards
			// 
			this.labelCreditCards.Location = new System.Drawing.Point(694,45);
			this.labelCreditCards.Name = "labelCreditCards";
			this.labelCreditCards.Size = new System.Drawing.Size(198,17);
			this.labelCreditCards.TabIndex = 131;
			this.labelCreditCards.Text = "Credit Card";
			this.labelCreditCards.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormPayment
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(974,562);
			this.Controls.Add(this.labelCreditCards);
			this.Controls.Add(this.comboCreditCards);
			this.Controls.Add(this.butPayConnect);
			this.Controls.Add(this.checkPayTypeNone);
			this.Controls.Add(this.textFamAfterIns);
			this.Controls.Add(this.textDeposit);
			this.Controls.Add(this.labelDeposit);
			this.Controls.Add(this.butPay);
			this.Controls.Add(this.textFamEnd);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textFamStart);
			this.Controls.Add(this.gridBal);
			this.Controls.Add(this.textDepositAccount);
			this.Controls.Add(this.panelXcharge);
			this.Controls.Add(this.gridMain);
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
				checkPayTypeNone.Enabled=true;
				if(!Security.IsAuthorized(Permissions.PaymentCreate)){//date not checked here
					DialogResult=DialogResult.Cancel;
					return;
				}
			}
			else{
				checkPayTypeNone.Enabled=false;
				if(!Security.IsAuthorized(Permissions.PaymentEdit,PaymentCur.PayDate)){
					butOK.Enabled=false;
					butDeleteAll.Enabled=false;
					butAdd.Enabled=false;
					gridMain.Enabled=false;
					butPay.Enabled=false;
				}
			}
			if(PrefC.GetBool(PrefName.EasyNoClinics)){
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
			List<CreditCard> creditCards=CreditCards.Refresh(PatCur.PatNum);
			for(int i=0;i<creditCards.Count;i++) {
				comboCreditCards.Items.Add(creditCards[i].CCNumberMasked);
			}
			if(creditCards.Count>0) {
				comboCreditCards.SelectedIndex=0;
			}
			tableBalances=Patients.GetPaymentStartingBalances(PatCur.Guarantor,PaymentCur.PayNum);
			//this works even if patient not in family
			textPaidBy.Text=FamCur.GetNameInFamFL(PaymentCur.PatNum);
			textDateEntry.Text=PaymentCur.DateEntry.ToShortDateString();
			textDate.Text=PaymentCur.PayDate.ToShortDateString();
			textAmount.Text=PaymentCur.PayAmt.ToString("F");
			textCheckNum.Text=PaymentCur.CheckNum;
			textBankBranch.Text=PaymentCur.BankBranch;
			for(int i=0;i<DefC.Short[(int)DefCat.PaymentTypes].Length;i++){
				listPayType.Items.Add(DefC.Short[(int)DefCat.PaymentTypes][i].ItemName);
				if(DefC.Short[(int)DefCat.PaymentTypes][i].DefNum==PaymentCur.PayType) {
					listPayType.SelectedIndex=i;
				}
			}
			if(PaymentCur.PayType==0) {
				checkPayTypeNone.Checked=true;
			}
			//if(listPayType.SelectedIndex==-1) {
			//	listPayType.SelectedIndex=0;
			//}
			textNote.Text=PaymentCur.PayNote;
			if(PaymentCur.DepositNum==0){
				labelDeposit.Visible=false;
				textDeposit.Visible=false;
			}
			else{
				textDeposit.Text=Deposits.GetOne(PaymentCur.DepositNum).DateDeposit.ToShortDateString();
				textAmount.ReadOnly=true;
				textAmount.BackColor=SystemColors.Control;
				butPay.Enabled=false;
			}
			SplitList=PaySplits.GetForPayment(PaymentCur.PayNum);//Count might be 0
			SplitListOld=new List<PaySplit>();
			//SplitListOld.AddRange(SplitList);//Do NOT do this.  It's a shallow copy only.  Not what we want.
			for(int i=0;i<SplitList.Count;i++) {
				SplitListOld.Add(SplitList[i].Copy());
			}
			if(IsNew) {
				List<PayPlan> payPlanList=PayPlans.GetValidPlansNoIns(PatCur.PatNum);
				if(payPlanList.Count==0){
					//
				}
				else if(payPlanList.Count==1){ //if there is only one valid payplan
					if(!PayPlans.PlanIsPaidOff(payPlanList[0].PayPlanNum)) {
						AddOneSplit();//the amount and date will be updated upon closing
						SplitList[SplitList.Count-1].PayPlanNum=payPlanList[0].PayPlanNum;
					}
				}
				else{
					List<PayPlanCharge> chargeList=PayPlanCharges.Refresh(PatCur.PatNum);
					//enhancement needed to weed out payment plans that are all paid off
					//more than one valid PayPlan
					FormPayPlanSelect FormPPS=new FormPayPlanSelect(payPlanList,chargeList);
					FormPPS.ShowDialog();
					if(FormPPS.DialogResult==DialogResult.OK){
						//return PayPlanList[FormPPS.IndexSelected].Clone();
						AddOneSplit();//the amount and date will be updated upon closing
						SplitList[SplitList.Count-1].PayPlanNum=payPlanList[FormPPS.IndexSelected].PayPlanNum;
					}
				}
				/*
				PayPlan payPlanCur=GetValidPlan(PatCur.PatNum,false);// PayPlans.GetValidPlan(PatCur.PatNum,false);
				if(payPlanCur!=null) {//a valid payPlan was located
					AddOneSplit();//the amount and date will be updated upon closing
					SplitList[SplitList.Count-1].PayPlanNum=payPlanCur.PayPlanNum;
				}*/
			}
			FillMain();
			if(InitialPaySplit!=0){
				for(int i=0;i<SplitList.Count;i++){
					if(InitialPaySplit==SplitList[i].SplitNum){
						gridMain.SetSelected(i,true);
					}
				}
			}
			if(IsNew){
				//Fill comboDepositAccount based on autopay for listPayType.SelectedIndex
				SetComboDepositAccounts();
				textDepositAccount.Visible=false;
			}
			else{
				//put a description in the textbox.  If the user clicks on the same or another item in listPayType,
				//then the textbox will go away, and be replaced by comboDepositAccount.
				labelDepositAccount.Visible=false;
				comboDepositAccount.Visible=false;
				Transaction trans=Transactions.GetAttachedToPayment(PaymentCur.PayNum);
				if(trans==null) {
					textDepositAccount.Visible=false;
				}
				else {
					//add only the description based on PaymentCur attached to transaction
					List<JournalEntry> jeL=JournalEntries.GetForTrans(trans.TransactionNum);
					for(int i=0;i<jeL.Count;i++) {
						if(Accounts.GetAccount(jeL[i].AccountNum).AcctType==AccountType.Asset) {
							textDepositAccount.Text=jeL[i].DateDisplayed.ToShortDateString();
							if(jeL[i].DebitAmt>0){
								textDepositAccount.Text+=" "+jeL[i].DebitAmt.ToString("c");
							}
							else{//negative
								textDepositAccount.Text+=" "+(-jeL[i].CreditAmt).ToString("c");
							}
							break;
						}
					}
				}
			}
			CheckUIState();
		}

		private void CheckUIState(){
			Program progXcharge=Programs.GetCur(ProgramName.Xcharge);
			Program progPayConnect=Programs.GetCur(ProgramName.PayConnect);
			if(progXcharge==null || progPayConnect==null){//Should not happen.
				panelXcharge.Visible=(progXcharge!=null);
				butPayConnect.Visible=(progPayConnect!=null);
				return;
			}
			panelXcharge.Visible=false;
			butPayConnect.Visible=false;
			if(!progPayConnect.Enabled && !progXcharge.Enabled) {//if neither enabled
				//show both so user can pick
				panelXcharge.Visible=true;
				butPayConnect.Visible=true;
			}
			//show if enabled.  User could have both enabled.
			if(progPayConnect.Enabled){
				butPayConnect.Visible=true;
			}
			else if(progXcharge.Enabled){
				panelXcharge.Visible=true;
			}
		}

		///<summary>This does not make any calls to db (except one tiny one).  Simply refreshes screen for SplitList.</summary>
		private void FillMain(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePaySplits","Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Prov"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Clinic"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Patient"),130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Procedure"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Amount"),60,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Unearned"),50);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			tot=0;
			Procedure proc;
			string procDesc;
			for(int i=0;i<SplitList.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(SplitList[i].ProcDate.ToShortDateString());
				row.Cells.Add(Providers.GetAbbr(SplitList[i].ProvNum));
				row.Cells.Add(Clinics.GetDesc(SplitList[i].ClinicNum));
				row.Cells.Add(FamCur.GetNameInFamFL(SplitList[i].PatNum));
				if(SplitList[i].ProcNum>0){
					proc=Procedures.GetOneProc(SplitList[i].ProcNum,false);
					procDesc=Procedures.GetDescription(proc);
					row.Cells.Add(procDesc);
				}
				else{
					row.Cells.Add("");
				}
				row.Cells.Add(SplitList[i].SplitAmt.ToString("F"));
				row.Cells.Add(DefC.GetName(DefCat.PaySplitUnearnedType,SplitList[i].UnearnedType));//handles 0 just fine
				tot+=SplitList[i].SplitAmt;
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
			FillGridBal();
		}

		///<summary></summary>
		private void FillGridBal(){
			double famstart=0;
			for(int i=0;i<tableBalances.Rows.Count;i++) {
				famstart+=PIn.Double(tableBalances.Rows[i]["StartBal"].ToString());
			}
			textFamStart.Text=famstart.ToString("N");
			double famafterins=0;
			for(int i=0;i<tableBalances.Rows.Count;i++) {
				famafterins+=PIn.Double(tableBalances.Rows[i]["AfterIns"].ToString());
			}
			if(!PrefC.GetBool(PrefName.BalancesDontSubtractIns)){
				textFamAfterIns.Text=famafterins.ToString("N");
			}
			//compute ending balances-----------------------------------------------------------------------------
			for(int i=0;i<tableBalances.Rows.Count;i++){
				if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)){
					tableBalances.Rows[i]["EndBal"]=tableBalances.Rows[i]["StartBal"].ToString();
				}
				else{
					tableBalances.Rows[i]["EndBal"]=tableBalances.Rows[i]["AfterIns"].ToString();
				}
			}
			double amt;
			for(int i=0;i<SplitList.Count;i++) {
				for(int f=0;f<tableBalances.Rows.Count;f++) {
					if(tableBalances.Rows[f]["PatNum"].ToString()!=SplitList[i].PatNum.ToString()) {
						continue;
					}
					if(tableBalances.Rows[f]["ProvNum"].ToString()!=SplitList[i].ProvNum.ToString()) {
						continue;
					}
					if(tableBalances.Rows[f]["ClinicNum"].ToString()!=SplitList[i].ClinicNum.ToString()) {
						continue;
					}
					amt=PIn.Double(tableBalances.Rows[f]["EndBal"].ToString())-SplitList[i].SplitAmt;
					tableBalances.Rows[f]["EndBal"]=amt.ToString("N");
				}
			}
			double famend=0;
			for(int i=0;i<tableBalances.Rows.Count;i++) {
				famend+=PIn.Double(tableBalances.Rows[i]["EndBal"].ToString());
			}
			textFamEnd.Text=famend.ToString("N");
			//fill grid--------------------------------------------------------------------------------------------
			gridBal.BeginUpdate();
			gridBal.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePaymentBal","Prov"),60);
			gridBal.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentBal","Clinic"),60);
			gridBal.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentBal","Patient"),62);
			gridBal.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentBal","Start"),60,HorizontalAlignment.Right);
			gridBal.Columns.Add(col);
			if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)){
				col=new ODGridColumn("",60);
			}
			else{
				col=new ODGridColumn(Lan.g("TablePaymentBal","After Ins"),60,HorizontalAlignment.Right);
			}
			gridBal.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentBal","End"),60,HorizontalAlignment.Right);
			gridBal.Columns.Add(col);
			gridBal.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<tableBalances.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(Providers.GetAbbr(PIn.Long(tableBalances.Rows[i]["ProvNum"].ToString())));
				row.Cells.Add(Clinics.GetDesc(PIn.Long(tableBalances.Rows[i]["ClinicNum"].ToString())));
				if(tableBalances.Rows[i]["Preferred"].ToString()==""){
					row.Cells.Add(tableBalances.Rows[i]["FName"].ToString());
				}
				else{
					row.Cells.Add("'"+tableBalances.Rows[i]["Preferred"].ToString()+"'");
				}
				row.Cells.Add(PIn.Double(tableBalances.Rows[i]["StartBal"].ToString()).ToString("N"));
				if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(PIn.Double(tableBalances.Rows[i]["AfterIns"].ToString()).ToString("N"));
				}
				row.Cells.Add(PIn.Double(tableBalances.Rows[i]["EndBal"].ToString()).ToString("N"));
				//row.ColorBackG=SystemColors.Control;//Color.FromArgb(240,240,240);
				gridBal.Rows.Add(row);
			}
			gridBal.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormPaySplitEdit FormPS=new FormPaySplitEdit(FamCur);
			FormPS.PaySplitCur=SplitList[e.Row];
			FormPS.Remain=PaymentCur.PayAmt-PIn.Double(textTotal.Text)+SplitList[e.Row].SplitAmt;
			FormPS.ShowDialog();
			if(FormPS.PaySplitCur==null) {//user deleted
				SplitList.RemoveAt(e.Row);
			}
			//if(FormPS.ShowDialog()==DialogResult.OK){
			FillMain();
			//}
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			PaySplit PaySplitCur=new PaySplit();
			PaySplitCur.PayNum=PaymentCur.PayNum;
			PaySplitCur.DatePay=PIn.Date(textDate.Text);//this may be updated upon closing
			PaySplitCur.ProcDate=PIn.Date(textDate.Text);//this may be updated upon closing
			PaySplitCur.ProvNum=Patients.GetProvNum(PatCur);
			PaySplitCur.PatNum=PatCur.PatNum;
			PaySplitCur.ClinicNum=PaymentCur.ClinicNum;
			FormPaySplitEdit FormPS=new FormPaySplitEdit(FamCur);
			FormPS.PaySplitCur=PaySplitCur;
			FormPS.IsNew=true;
			FormPS.Remain=PaymentCur.PayAmt-PIn.Double(textTotal.Text);
			if(FormPS.ShowDialog()!=DialogResult.OK){
				return;
			}
			SplitList.Add(PaySplitCur);
			FillMain();
		}

		private void butPay_Click(object sender,EventArgs e) {
			if(gridBal.SelectedIndices.Length==0){
				gridBal.SetSelected(true);
			}
			SplitList.Clear();
			double amt;
			PaySplit split;
			for(int i=0;i<gridBal.SelectedIndices.Length;i++){
				if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)){
					amt=PIn.Double(tableBalances.Rows[gridBal.SelectedIndices[i]]["StartBal"].ToString());
				}
				else{
					amt=PIn.Double(tableBalances.Rows[gridBal.SelectedIndices[i]]["AfterIns"].ToString());
				}
				if(amt==0){
					continue;
				}
				split=new PaySplit();
				split.PatNum=PIn.Long(tableBalances.Rows[gridBal.SelectedIndices[i]]["PatNum"].ToString());
				split.PayNum=PaymentCur.PayNum;
				split.ProcDate=PaymentCur.PayDate;//this may be updated upon closing
				split.DatePay=PaymentCur.PayDate;//this may be updated upon closing
				split.ProvNum=PIn.Long(tableBalances.Rows[gridBal.SelectedIndices[i]]["ProvNum"].ToString());
				split.ClinicNum=PIn.Long(tableBalances.Rows[gridBal.SelectedIndices[i]]["ClinicNum"].ToString());
				split.SplitAmt=amt;
				SplitList.Add(split);
			}
			FillMain();
			textAmount.Text=textTotal.Text;
		}

		private void checkPayPlan_Click(object sender, System.EventArgs e) {
			//*****if there is more than one split, then this checkbox is not even available.
			if(SplitList.Count==0){
				AddOneSplit();//won't use returned value
				FillMain();
				checkPayPlan.Checked=true;
				//now there is exactly one.  The amount will be updated as the form closes.
			}
			if(checkPayPlan.Checked){
				//PayPlan payPlanCur=PayPlans.GetValidPlan(SplitList[0].PatNum);
				List<PayPlan> payPlanList=PayPlans.GetValidPlansNoIns(SplitList[0].PatNum);
				if(payPlanList.Count==0){
					MsgBox.Show(this,"The selected patient is not the guarantor for any payment plans.");
					checkPayPlan.Checked=false;
					return;
				}
				else if(payPlanList.Count==1){ //if there is only one valid payplan
					SplitList[0].PayPlanNum=payPlanList[0].PayPlanNum;
				}
				else{//multiple valid plans
					List<PayPlanCharge> chargeList=PayPlanCharges.Refresh(SplitList[0].PatNum);
					//enhancement needed to weed out payment plans that are all paid off
					//more than one valid PayPlan
					FormPayPlanSelect FormPPS=new FormPayPlanSelect(payPlanList,chargeList);
					FormPPS.ShowDialog();
					if(FormPPS.DialogResult==DialogResult.OK){
						SplitList[0].PayPlanNum=payPlanList[FormPPS.IndexSelected].PayPlanNum;
					}
					else{
						checkPayPlan.Checked=false;
						return;
					}
				}
				/*
				if(payPlanCur==null){//no valid plans
					MsgBox.Show(this,"The selected patient is not the guarantor for any payment plans.");
					checkPayPlan.Checked=false;
					return;
				}
				SplitList[0].PayPlanNum=payPlanCur.PayPlanNum;*/
			}
			else{//payPlan unchecked
				SplitList[0].PayPlanNum=0;
			}
			FillMain();
		}

		/// <summary>Adds one split to work with.  Called when checkPayPlan click, or upon load if auto attaching to payplan.</summary>
		private void AddOneSplit(){
			PaySplit PaySplitCur=new PaySplit();
			PaySplitCur.PatNum=PatCur.PatNum;
			PaySplitCur.PayNum=PaymentCur.PayNum;
			PaySplitCur.ProcDate=PaymentCur.PayDate;//this may be updated upon closing
			PaySplitCur.DatePay=PaymentCur.PayDate;//this may be updated upon closing
			PaySplitCur.ProvNum=Patients.GetProvNum(PatCur);
			PaySplitCur.ClinicNum=PaymentCur.ClinicNum;
			PaySplitCur.SplitAmt=PIn.Double(textAmount.Text);
			SplitList.Add(PaySplitCur);
		}

		private void listPayType_Click(object sender,EventArgs e) {
			textDepositAccount.Visible=false;
			SetComboDepositAccounts();
		}

		///<summary>Called from all 3 places where listPayType gets changed.</summary>
		private void SetComboDepositAccounts(){
			if(listPayType.SelectedIndex==-1) {
				return;
			}
			AccountingAutoPay autoPay=AccountingAutoPays.GetForPayType(
				DefC.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndex].DefNum);
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
				if(comboDepositAccount.Items.Count>0){
					comboDepositAccount.SelectedIndex=0;
				}
			}
		}

		private void panelXcharge_MouseClick(object sender,MouseEventArgs e) {
			if(e.Button != MouseButtons.Left){
				return;
			}
			if(textAmount.Text=="" || textAmount.Text=="0.00") {
				MsgBox.Show(this,"Please enter an amount first.");
				return;
			}
			Program prog=Programs.GetCur(ProgramName.Xcharge);
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
			bool needToken=false;
			ProgramProperty prop=(ProgramProperty)ProgramProperties.GetForProgram(prog.ProgramNum)[0];
			//still need to add functionality for accountingAutoPay
			listPayType.SelectedIndex=DefC.GetOrder(DefCat.PaymentTypes,PIn.Long(prop.PropertyValue));
			SetComboDepositAccounts();
			/*XCharge.exe [/TRANSACTIONTYPE:type] [/AMOUNT:amount] [/ACCOUNT:account] [/EXP:exp]
				[“/TRACK:track”] [/ZIP:zip] [/ADDRESS:address] [/RECEIPT:receipt] [/CLERK:clerk]
				[/APPROVALCODE:approval] [/AUTOPROCESS] [/AUTOCLOSE] [/STAYONTOP] [/MID]
				[/RESULTFILE:”C:\Program Files\X-Charge\LocalTran\XCResult.txt”*/
			ProcessStartInfo info=new ProcessStartInfo(prog.Path);
			Patient pat=Patients.GetPat(PaymentCur.PatNum);
			PatientNote patnote=PatientNotes.Refresh(pat.PatNum,pat.Guarantor);
			string resultfile=Path.Combine(Path.GetDirectoryName(prog.Path),"XResult.txt");
			File.Delete(resultfile);//delete the old result file.
			info.Arguments="";
			double amt=PIn.Double(textAmount.Text);
			if(amt>0) {
				info.Arguments+="/AMOUNT:"+amt.ToString("F2")+" ";
			}
			CreditCard CCard=null;
			List<CreditCard> creditCards=CreditCards.Refresh(PatCur.PatNum);
			for(int i=0;i<creditCards.Count;i++) {
				if(i==comboCreditCards.SelectedIndex){
					CCard=creditCards[i];
				}
			}
			if(CCard!=null) {//Have credit card on file
				if(CCard.XChargeToken!="") {//Recurring charge
					/*       ***** An example of how recurring charges work***** 
					C:\Program Files\X-Charge\XCharge.exe /TRANSACTIONTYPE:Purchase /LOCKTRANTYPE
					/AMOUNT:10.00 /LOCKAMOUNT /XCACCOUNTID: XAW0JWtx5kjG8 /RECEIPT:RC001
					/LOCKRECEIPT /CLERK:Clerk /LOCKCLERK /RESULTFILE:C:\ResultFile.txt /USERID:system
					/PASSWORD:system /STAYONTOP /AUTOPROCESS /AUTOCLOSE /HIDEMAINWINDOW
					/RECURRING /SMALLWINDOW /NORESULTDIALOG
					*/
					info.Arguments+="/XCACCOUNTID:"+CCard.XChargeToken+" ";
					info.Arguments+="/RECEIPT:Pat"+PaymentCur.PatNum.ToString()+" ";//aka invoice#
					info.Arguments+="\"/CLERK:"+Security.CurUser.UserName+"\" ";
					info.Arguments+="/RESULTFILE:\""+resultfile+"\" ";
					info.Arguments+="/USERID:"+ProgramProperties.GetPropVal(prog.ProgramNum,"Username")+" ";
					info.Arguments+="/PASSWORD:"+ProgramProperties.GetPropVal(prog.ProgramNum,"Password")+" ";
					info.Arguments+="/AUTOPROCESS ";
					info.Arguments+="/AUTOCLOSE ";
					info.Arguments+="/HIDEMAINWINDOW ";
					info.Arguments+="/RECURRING ";
					info.Arguments+="/NORESULTDIALOG ";
				}
				else {//Not recurring charge
					needToken=true;//Will create a token from result file so credit card info isn't saved in our db.
					if(CCard.CCNumberMasked!="") {//Number won't be masked if not recurring
						info.Arguments+="/ACCOUNT:"+CCard.CCNumberMasked+" ";
					}
					if(CCard.CCExpiration!=null && CCard.CCExpiration.Year>2005) {
						info.Arguments+="/EXP:"+CCard.CCExpiration.ToString("MMyy")+" ";
					}
					if(CCard.Zip!="") {
						info.Arguments+="\"/ZIP:"+CCard.Zip+"\" ";
					}
					else {
						info.Arguments+="\"/ZIP:"+pat.Zip+"\" ";
					}
					if(CCard.Address!="") {
						info.Arguments+="\"/ADDRESS:"+CCard.Address+"\" ";
					}
					else {
						info.Arguments+="\"/ADDRESS:"+pat.Address+"\" ";
					}
					info.Arguments+="/RECEIPT:Pat"+PaymentCur.PatNum.ToString()+" ";//aka invoice#
					info.Arguments+="\"/CLERK:"+Security.CurUser.UserName+"\" ";
					info.Arguments+="/AUTOCLOSE ";
					info.Arguments+="/RESULTFILE:\""+resultfile+"\" ";
					info.Arguments+="/USERID:"+ProgramProperties.GetPropVal(prog.ProgramNum,"Username")+" ";
					info.Arguments+="/PASSWORD:"+ProgramProperties.GetPropVal(prog.ProgramNum,"Password")+" ";
					info.Arguments+="/NORESULTDIALOG ";
				}
			}
			else {//No credit cards in creditcard table so use they will manually type in information.
				info.Arguments+="\"/ZIP:"+pat.Zip+"\" ";
				info.Arguments+="\"/ADDRESS:"+pat.Address+"\" ";
				info.Arguments+="/RECEIPT:Pat"+PaymentCur.PatNum.ToString()+" ";//aka invoice#
				info.Arguments+="\"/CLERK:"+Security.CurUser.UserName+"\" ";
				info.Arguments+="/PARTIALAPPROVALSUPPORT:T ";
				info.Arguments+="/AUTOCLOSE ";
				info.Arguments+="/RESULTFILE:\""+resultfile+"\" ";
				info.Arguments+="/USERID:"+ProgramProperties.GetPropVal(prog.ProgramNum,"Username")+" ";
				info.Arguments+="/PASSWORD:"+ProgramProperties.GetPropVal(prog.ProgramNum,"Password")+" ";
			}
			Cursor=Cursors.WaitCursor;
			Process process=new Process();
			process.StartInfo=info;
			process.EnableRaisingEvents=true;
			process.Start();
			while(!process.HasExited) {
				Application.DoEvents();
			}
			Thread.Sleep(200);//Wait 2/10 second to give time for file to be created.
			Cursor=Cursors.Default;
			string resulttext="";
			string line="";
			//bool showReturnedNote=true;
			bool showAmountNotice=false;
			double amtReturned=0;
			string xChargeToken="";
			string accountMasked="";
			using(TextReader reader=new StreamReader(resultfile)) {
				line=reader.ReadLine();
				while(line!=null) {
					if(resulttext!="") {
						resulttext+="\r\n";
					}
					resulttext+=line;
					if(line.StartsWith("RESULT=")) {
						if(line!="RESULT=SUCCESS") {
							needToken=false;//Don't update CCard due to failure
							break;
						}
					}
					if(line.StartsWith("AMOUNT=")) {
						amtReturned=PIn.Double(line.Substring(7));
						if(amtReturned != amt) {
							//showReturnedNote=false;
							showAmountNotice=true;
						}
					}
					if(line.StartsWith("XCACCOUNTID=")) {
						xChargeToken=PIn.String(line.Substring(12));
					}
					if(line.StartsWith("ACCOUNT=")) {
						accountMasked=PIn.String(line.Substring(8));
					}
					line=reader.ReadLine();
				}
				if(needToken) {
					//Only way this code can be hit is if they have set up a credit card and it does not have a token.
					//So we'll use the created token from result file and assign it to the coresponding account.
					//Also will delete the credit card number and replace it with secure masked number.
					CCard.XChargeToken=xChargeToken;
					CCard.CCNumberMasked=accountMasked;
					CreditCards.Update(CCard);
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
			//if(showReturnedNote) {
			if(textNote.Text!="") {
				textNote.Text+="\r\n";
			}
			textNote.Text+=resulttext;
			//}
			if(showAmountNotice) {
				MessageBox.Show(Lan.g(this,"Warning: The amount you typed in: ")+amt.ToString("C")+" \r\n"+Lan.g(this,"does not match the amount charged: ")+amtReturned.ToString("C"));
			}
		}

		private void butPayConnect_Click(object sender,EventArgs e) {
			Program prog=Programs.GetCur(ProgramName.PayConnect);
			if(!prog.Enabled){
				FormPayConnectSetup fpcs=new FormPayConnectSetup();
				fpcs.ShowDialog();
				CheckUIState();
				return;
			}
			FormPayConnect FormP=new FormPayConnect(PaymentCur,PatCur,textAmount.Text);
			FormP.ShowDialog();
			ArrayList props=ProgramProperties.GetForProgram(prog.ProgramNum);
			ProgramProperty prop=null;
			for(int i=0;i<props.Count;i++){
				ProgramProperty curProp=(ProgramProperty)props[i];
				if(curProp.PropertyDesc=="PaymentType"){
					prop=curProp;
					break;
				}
			}
			//still need to add functionality for accountingAutoPay
			listPayType.SelectedIndex=DefC.GetOrder(DefCat.PaymentTypes,PIn.Long(prop.PropertyValue));
			SetComboDepositAccounts();
			if(FormP.Response!=null) {
				textNote.Text+=((textNote.Text=="")?"":Environment.NewLine)+Lan.g(this,"Transaction Type")+": "+Enum.GetName(typeof(PayConnectService.transType),FormP.TranType)+Environment.NewLine+
					Lan.g(this,"Status")+": "+FormP.Response.Status.description;
				if(FormP.Response.Status.code==0) { //The transaction succeeded.
					textNote.Text+=Environment.NewLine
						+Lan.g(this,"Amount")+": "+FormP.AmountCharged+Environment.NewLine
						+Lan.g(this,"Auth Code")+": "+FormP.Response.AuthCode+Environment.NewLine
						+Lan.g(this,"Ref Number")+": "+FormP.Response.RefNumber;
					textNote.Select(textNote.Text.Length-1,0);
					textNote.ScrollToCaret();//Scroll to the end of the text box to see the newest notes.
					if(FormP.TranType==PayConnectService.transType.VOID || FormP.TranType==PayConnectService.transType.RETURN) {
						textAmount.Text="-"+FormP.AmountCharged;
					}
					else if(FormP.TranType==PayConnectService.transType.AUTH) {
						textAmount.Text=FormP.AmountCharged;
					}
					else if(FormP.TranType==PayConnectService.transType.SALE) {
						textAmount.Text=FormP.AmountCharged;
						PaymentCur.Receipt=FormP.ReceiptStr; //There is only a receipt when a sale takes place.
					}
				}
			}
			if(FormP.Response==null || FormP.Response.Status.code!=0) { //The transaction failed.
				if(FormP.TranType==PayConnectService.transType.SALE || FormP.TranType==PayConnectService.transType.AUTH) {
					textAmount.Text=FormP.AmountCharged;//Preserve the amount so the user can try the payment again more easily.
				}
			}
		}

		private void menuXcharge_Click(object sender,EventArgs e) {
			if(Security.IsAuthorized(Permissions.Setup)) {
				FormXchargeSetup FormX=new FormXchargeSetup();
				if(FormX.ShowDialog()==DialogResult.OK){
					CheckUIState();
				}
			}
		}

		private void menuPayConnect_Click(object sender,EventArgs e) {
			if(Security.IsAuthorized(Permissions.Setup)) {
				FormPayConnectSetup fpcs=new FormPayConnectSetup();
				if(fpcs.ShowDialog()==DialogResult.OK){
					CheckUIState();
				}
			}
		}

		private void gridBal_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			MsgBox.Show(this,"This grid is not editable.  Family balances are altered by using splits in the grid to the left.");
		}

		private void comboClinic_SelectionChangeCommitted(object sender,EventArgs e) {
			if(comboClinic.SelectedIndex==0) {
				PaymentCur.ClinicNum=0;
			}
			else {
				PaymentCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			}
			if(SplitList.Count>0) {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Change clinic for all splits?")) {
					return;
				}
				for(int i=0;i<SplitList.Count;i++) {
					SplitList[i].ClinicNum=PaymentCur.ClinicNum;
				}
				FillMain();
			}
		}

		private void checkPayTypeNone_CheckedChanged(object sender,EventArgs e) {
			//this fires before the click event.  The Checked property also reflects the new value.
			if(checkPayTypeNone.Checked) {
				listPayType.Visible=false;
				panelXcharge.Visible=false;
				butPay.Text=Lan.g(this,"Transfer");
			}
			else {
				listPayType.Visible=true;
				panelXcharge.Visible=true;
				butPay.Text=Lan.g(this,"Pay");
			}
		}

		private void checkPayTypeNone_Click(object sender,EventArgs e) {
			//The Checked property reflects the new value.
			//Only possible if IsNew.

		}

		private void butDeleteAll_Click(object sender, System.EventArgs e) {
			if(textDeposit.Visible){//this will get checked again by the middle layer
				MsgBox.Show(this,"This payment is attached to a deposit.  Not allowed to delete.");
				return;
			}
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
				|| textAmount.errorProvider1.GetError(textAmount)!="")
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(checkPayTypeNone.Checked) {
				if(PIn.Double(textAmount.Text)!=0) {
					MsgBox.Show(this,"Amount must be zero for a transfer.");
					return;
				}
			}
			else{
				if(textAmount.Text=="") {
					MessageBox.Show(Lan.g(this,"Please enter an amount."));
					return;
				}
				if(PIn.Double(textAmount.Text)==0) {
					MessageBox.Show(Lan.g(this,"Amount must not be zero unless this is a transfer."));
					return;
				}
				if(listPayType.SelectedIndex==-1) {
					MsgBox.Show(this,"A payment type must be selected.");
				}
			}
			if(IsNew){
				//prevents backdating of initial payment
				if(!Security.IsAuthorized(Permissions.PaymentCreate,PIn.Date(textDate.Text))){
					return;
				}
			}
			else{
				//Editing an old entry will already be blocked if the date was too old, and user will not be able to click OK button
				//This catches it if user changed the date to be older.
				if(!Security.IsAuthorized(Permissions.PaymentEdit,PIn.Date(textDate.Text))){
					return;
				}
			}
			bool accountingSynchRequired=false;
			double accountingOldAmt=PaymentCur.PayAmt;
			long accountingNewAcct=-1;//the old acctNum will be retrieved inside the validation code.
			if(textDepositAccount.Visible){
				accountingNewAcct=-1;//indicates no change
			}
			else if(comboDepositAccount.Visible && comboDepositAccount.Items.Count>0 && comboDepositAccount.SelectedIndex!=-1){
				accountingNewAcct=DepositAccounts[comboDepositAccount.SelectedIndex];
			}
			else{//neither textbox nor combo visible. Or something's wrong with combobox
				accountingNewAcct=0;
			}
			try {
				accountingSynchRequired=Payments.ValidateLinkedEntries(accountingOldAmt,PIn.Double(textAmount.Text),IsNew,
					PaymentCur.PayNum,accountingNewAcct);
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);//not able to alter, so must not allow user to continue.
				return;
			}
			PaymentCur.PayAmt=PIn.Double(textAmount.Text);//handles blank
			PaymentCur.PayDate=PIn.Date(textDate.Text);
			PaymentCur.CheckNum=textCheckNum.Text;
			PaymentCur.BankBranch=textBankBranch.Text;
			PaymentCur.PayNote=textNote.Text;
			if(checkPayTypeNone.Checked) {
				PaymentCur.PayType=0;
			}
			else {
				PaymentCur.PayType=DefC.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndex].DefNum;
			}
			//PaymentCur.PatNum=PatCur.PatNum;//this is already done before opening this window.
			//PaymentCur.ClinicNum already handled
			if(SplitList.Count==0) {
				if(Payments.AllocationRequired(PaymentCur.PayAmt,PaymentCur.PatNum)
					&& MsgBox.Show(this,true,"Apply part of payment to other family members?"))
				{
					SplitList=Payments.Allocate(PaymentCur);//PayAmt needs to be set first
				}
				else{//Either no allocation required, or user does not want to allocate.  Just add one split.
					PaySplit split=new PaySplit();
					split.PatNum=PaymentCur.PatNum;
					split.ClinicNum=PaymentCur.ClinicNum;
					split.PayNum=PaymentCur.PayNum;
					split.ProcDate=PaymentCur.PayDate;
					split.DatePay=PaymentCur.PayDate;
					split.ProvNum=Patients.GetProvNum(PatCur);
					split.SplitAmt=PaymentCur.PayAmt;
					SplitList.Add(split);
				}
			}
			else if(SplitList.Count==1//if one split
				&& PIn.Double(textAmount.Text) != SplitList[0].SplitAmt)//and amount doesn't match payment
			{
				SplitList[0].SplitAmt=PIn.Double(textAmount.Text);//make amounts match
			}
			else if(SplitList.Count==1//if one split
				&& PaymentCur.PayDate != SplitList[0].ProcDate
				&& SplitList[0].ProcNum==0)//not attached to procedure
			{
				if(MsgBox.Show(this,MsgBoxButtons.YesNo,"Change split date to match payment date?")) {
					SplitList[0].ProcDate=PaymentCur.PayDate;
				}
			}
			else if(PaymentCur.PayAmt!=PIn.Double(textTotal.Text)) {
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
				Payments.Update(PaymentCur,true);
			}
			catch(ApplicationException ex){//this catches bad dates.
				MessageBox.Show(ex.Message);
				return;
			}
			//Set all DatePays the same.
			for(int i=0;i<SplitList.Count;i++){
				SplitList[i].DatePay=PaymentCur.PayDate;
			}
			PaySplits.UpdateList(SplitListOld,SplitList);
			//Accounting synch is done here.  All validation was done further up
			//If user is trying to change the amount or linked account of an entry that was already copied and linked to accounting section
			if(accountingSynchRequired){
				Payments.AlterLinkedEntries(accountingOldAmt,PIn.Double(textAmount.Text),IsNew,
					PaymentCur.PayNum,accountingNewAcct,PaymentCur.PayDate,FamCur.GetNameInFamFL(PaymentCur.PatNum));
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
			if(DialogResult==DialogResult.OK) {
				return;
			}
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
