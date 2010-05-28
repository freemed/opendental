using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormCarrierEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label labelElectID;
		private System.Windows.Forms.TextBox textCarrierName;
		private System.Windows.Forms.TextBox textPhone;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.TextBox textAddress2;
		private System.Windows.Forms.TextBox textElectID;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboPlans;
		private System.Windows.Forms.TextBox textPlans;
		private System.Windows.Forms.Label label9;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.CheckBox checkNoSendElect;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.Label labelCitySt;
		///<summary></summary>
		public bool IsNew;
		private CheckBox checkIsCDAnet;
		private GroupBox groupCDAnet;
		private CheckBox checkPMP;
		private TextBox textModemReconcile;
		private Label label10;
		private TextBox textModemSummary;
		private Label label8;
		private TextBox textModem;
		private Label label7;
		private ComboBox comboNetwork;
		private Label label5;
		private TextBox textVersion;
		private Label label1;
		private GroupBox groupBox3;
		private CheckBox check08;
		private CheckBox check03;
		private CheckBox check07;
		private CheckBox check06;
		private CheckBox check04;
		private CheckBox check05;
		private CheckBox check02;
		private CheckBox checkIsHidden;
		private Label label11;
		private CheckBox check11e;
		private CheckBox check18;
		private CheckBox check21e;
		private CheckBox check12;
		private CheckBox check16;
		private CheckBox check15;
		private CheckBox check24;
		private CheckBox check14;
		private CheckBox check13e;
		private CheckBox check13;
		private CheckBox check03m;
		private Label label12;
		private TextBox textEncryptionMethod;
		private Label label13;
		private TextBox textTransactionPrefix;
		private Label label14;
		public Carrier CarrierCur;

		///<summary></summary>
		public FormCarrierEdit(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCarrierEdit));
			this.textCarrierName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textPhone = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.labelElectID = new System.Windows.Forms.Label();
			this.textElectID = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboPlans = new System.Windows.Forms.ComboBox();
			this.textPlans = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.checkNoSendElect = new System.Windows.Forms.CheckBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.textState = new System.Windows.Forms.TextBox();
			this.textZip = new System.Windows.Forms.TextBox();
			this.labelCitySt = new System.Windows.Forms.Label();
			this.checkIsCDAnet = new System.Windows.Forms.CheckBox();
			this.groupCDAnet = new System.Windows.Forms.GroupBox();
			this.textTransactionPrefix = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.textEncryptionMethod = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.check16 = new System.Windows.Forms.CheckBox();
			this.check15 = new System.Windows.Forms.CheckBox();
			this.check24 = new System.Windows.Forms.CheckBox();
			this.check14 = new System.Windows.Forms.CheckBox();
			this.check13e = new System.Windows.Forms.CheckBox();
			this.check13 = new System.Windows.Forms.CheckBox();
			this.check03m = new System.Windows.Forms.CheckBox();
			this.check12 = new System.Windows.Forms.CheckBox();
			this.check21e = new System.Windows.Forms.CheckBox();
			this.check11e = new System.Windows.Forms.CheckBox();
			this.check18 = new System.Windows.Forms.CheckBox();
			this.check03 = new System.Windows.Forms.CheckBox();
			this.check07 = new System.Windows.Forms.CheckBox();
			this.check06 = new System.Windows.Forms.CheckBox();
			this.check04 = new System.Windows.Forms.CheckBox();
			this.check05 = new System.Windows.Forms.CheckBox();
			this.check02 = new System.Windows.Forms.CheckBox();
			this.check08 = new System.Windows.Forms.CheckBox();
			this.textModemReconcile = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textModemSummary = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textModem = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.comboNetwork = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textVersion = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkPMP = new System.Windows.Forms.CheckBox();
			this.checkIsHidden = new System.Windows.Forms.CheckBox();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupCDAnet.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// textCarrierName
			// 
			this.textCarrierName.Location = new System.Drawing.Point(180,10);
			this.textCarrierName.MaxLength = 255;
			this.textCarrierName.Name = "textCarrierName";
			this.textCarrierName.Size = new System.Drawing.Size(226,20);
			this.textCarrierName.TabIndex = 3;
			this.textCarrierName.TextChanged += new System.EventHandler(this.textCarrierName_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(28,33);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(151,17);
			this.label2.TabIndex = 6;
			this.label2.Text = "Phone";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(180,30);
			this.textPhone.MaxLength = 255;
			this.textPhone.Name = "textPhone";
			this.textPhone.Size = new System.Drawing.Size(157,20);
			this.textPhone.TabIndex = 5;
			this.textPhone.TextChanged += new System.EventHandler(this.textPhone_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(27,52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(151,17);
			this.label3.TabIndex = 8;
			this.label3.Text = "Address";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(180,50);
			this.textAddress.MaxLength = 255;
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(291,20);
			this.textAddress.TabIndex = 7;
			this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(27,74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(151,17);
			this.label4.TabIndex = 10;
			this.label4.Text = "Address2";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(180,70);
			this.textAddress2.MaxLength = 255;
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.Size = new System.Drawing.Size(291,20);
			this.textAddress2.TabIndex = 9;
			this.textAddress2.TextChanged += new System.EventHandler(this.textAddress2_TextChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(27,13);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(151,17);
			this.label6.TabIndex = 14;
			this.label6.Text = "Carrier Name";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelElectID
			// 
			this.labelElectID.Location = new System.Drawing.Point(27,116);
			this.labelElectID.Name = "labelElectID";
			this.labelElectID.Size = new System.Drawing.Size(151,17);
			this.labelElectID.TabIndex = 20;
			this.labelElectID.Text = "Electronic ID";
			this.labelElectID.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textElectID
			// 
			this.textElectID.Location = new System.Drawing.Point(180,112);
			this.textElectID.Name = "textElectID";
			this.textElectID.Size = new System.Drawing.Size(59,20);
			this.textElectID.TabIndex = 19;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboPlans);
			this.groupBox1.Controls.Add(this.textPlans);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(31,161);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(448,49);
			this.groupBox1.TabIndex = 23;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "In Use By";
			// 
			// comboPlans
			// 
			this.comboPlans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPlans.Location = new System.Drawing.Point(188,18);
			this.comboPlans.MaxDropDownItems = 30;
			this.comboPlans.Name = "comboPlans";
			this.comboPlans.Size = new System.Drawing.Size(238,21);
			this.comboPlans.TabIndex = 68;
			// 
			// textPlans
			// 
			this.textPlans.BackColor = System.Drawing.Color.White;
			this.textPlans.Location = new System.Drawing.Point(149,18);
			this.textPlans.Name = "textPlans";
			this.textPlans.ReadOnly = true;
			this.textPlans.Size = new System.Drawing.Size(35,20);
			this.textPlans.TabIndex = 67;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(4,20);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(142,17);
			this.label9.TabIndex = 66;
			this.label9.Text = "Ins Plan Subscribers";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkNoSendElect
			// 
			this.checkNoSendElect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoSendElect.Location = new System.Drawing.Point(247,115);
			this.checkNoSendElect.Name = "checkNoSendElect";
			this.checkNoSendElect.Size = new System.Drawing.Size(246,17);
			this.checkNoSendElect.TabIndex = 93;
			this.checkNoSendElect.Text = "Don\'t Usually Send Electronically";
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(180,91);
			this.textCity.MaxLength = 255;
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(155,20);
			this.textCity.TabIndex = 94;
			this.textCity.TextChanged += new System.EventHandler(this.textCity_TextChanged);
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(335,91);
			this.textState.MaxLength = 255;
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(65,20);
			this.textState.TabIndex = 96;
			this.textState.TextChanged += new System.EventHandler(this.textState_TextChanged);
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(400,91);
			this.textZip.MaxLength = 255;
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(71,20);
			this.textZip.TabIndex = 97;
			// 
			// labelCitySt
			// 
			this.labelCitySt.Location = new System.Drawing.Point(15,95);
			this.labelCitySt.Name = "labelCitySt";
			this.labelCitySt.Size = new System.Drawing.Size(163,15);
			this.labelCitySt.TabIndex = 95;
			this.labelCitySt.Text = "City, ST, Zip";
			this.labelCitySt.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkIsCDAnet
			// 
			this.checkIsCDAnet.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsCDAnet.Location = new System.Drawing.Point(31,216);
			this.checkIsCDAnet.Name = "checkIsCDAnet";
			this.checkIsCDAnet.Size = new System.Drawing.Size(168,17);
			this.checkIsCDAnet.TabIndex = 98;
			this.checkIsCDAnet.Text = "Is CDAnet Carrier";
			this.checkIsCDAnet.Click += new System.EventHandler(this.checkIsCDAnet_Click);
			// 
			// groupCDAnet
			// 
			this.groupCDAnet.Controls.Add(this.textTransactionPrefix);
			this.groupCDAnet.Controls.Add(this.label14);
			this.groupCDAnet.Controls.Add(this.label12);
			this.groupCDAnet.Controls.Add(this.textEncryptionMethod);
			this.groupCDAnet.Controls.Add(this.label13);
			this.groupCDAnet.Controls.Add(this.label11);
			this.groupCDAnet.Controls.Add(this.groupBox3);
			this.groupCDAnet.Controls.Add(this.textModemReconcile);
			this.groupCDAnet.Controls.Add(this.label10);
			this.groupCDAnet.Controls.Add(this.textModemSummary);
			this.groupCDAnet.Controls.Add(this.label8);
			this.groupCDAnet.Controls.Add(this.textModem);
			this.groupCDAnet.Controls.Add(this.label7);
			this.groupCDAnet.Controls.Add(this.comboNetwork);
			this.groupCDAnet.Controls.Add(this.label5);
			this.groupCDAnet.Controls.Add(this.textVersion);
			this.groupCDAnet.Controls.Add(this.label1);
			this.groupCDAnet.Controls.Add(this.checkPMP);
			this.groupCDAnet.Location = new System.Drawing.Point(29,239);
			this.groupCDAnet.Name = "groupCDAnet";
			this.groupCDAnet.Size = new System.Drawing.Size(664,396);
			this.groupCDAnet.TabIndex = 99;
			this.groupCDAnet.TabStop = false;
			this.groupCDAnet.Text = "CDAnet";
			// 
			// textTransactionPrefix
			// 
			this.textTransactionPrefix.Location = new System.Drawing.Point(191,115);
			this.textTransactionPrefix.Name = "textTransactionPrefix";
			this.textTransactionPrefix.Size = new System.Drawing.Size(121,20);
			this.textTransactionPrefix.TabIndex = 116;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(38,120);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(151,17);
			this.label14.TabIndex = 117;
			this.label14.Text = "Transaction Prefix";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(235,92);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(92,17);
			this.label12.TabIndex = 115;
			this.label12.Text = "(1, 2, or 3)";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textEncryptionMethod
			// 
			this.textEncryptionMethod.Location = new System.Drawing.Point(191,89);
			this.textEncryptionMethod.Name = "textEncryptionMethod";
			this.textEncryptionMethod.Size = new System.Drawing.Size(42,20);
			this.textEncryptionMethod.TabIndex = 113;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(38,94);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(151,17);
			this.label13.TabIndex = 114;
			this.label13.Text = "Encryption Method";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(235,44);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(92,17);
			this.label11.TabIndex = 112;
			this.label11.Text = "(02, 03, or 04)";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.check16);
			this.groupBox3.Controls.Add(this.check15);
			this.groupBox3.Controls.Add(this.check24);
			this.groupBox3.Controls.Add(this.check14);
			this.groupBox3.Controls.Add(this.check13e);
			this.groupBox3.Controls.Add(this.check13);
			this.groupBox3.Controls.Add(this.check03m);
			this.groupBox3.Controls.Add(this.check12);
			this.groupBox3.Controls.Add(this.check21e);
			this.groupBox3.Controls.Add(this.check11e);
			this.groupBox3.Controls.Add(this.check18);
			this.groupBox3.Controls.Add(this.check03);
			this.groupBox3.Controls.Add(this.check07);
			this.groupBox3.Controls.Add(this.check06);
			this.groupBox3.Controls.Add(this.check04);
			this.groupBox3.Controls.Add(this.check05);
			this.groupBox3.Controls.Add(this.check02);
			this.groupBox3.Controls.Add(this.check08);
			this.groupBox3.Location = new System.Drawing.Point(337,39);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(321,350);
			this.groupBox3.TabIndex = 111;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Supported Transaction Types";
			// 
			// check16
			// 
			this.check16.Location = new System.Drawing.Point(16,325);
			this.check16.Name = "check16";
			this.check16.Size = new System.Drawing.Size(299,18);
			this.check16.TabIndex = 19;
			this.check16.Text = "Payment Reconciliation";
			this.check16.UseVisualStyleBackColor = true;
			// 
			// check15
			// 
			this.check15.Location = new System.Drawing.Point(16,289);
			this.check15.Name = "check15";
			this.check15.Size = new System.Drawing.Size(299,18);
			this.check15.TabIndex = 18;
			this.check15.Text = "Summary Reconciliation";
			this.check15.UseVisualStyleBackColor = true;
			// 
			// check24
			// 
			this.check24.Location = new System.Drawing.Point(16,253);
			this.check24.Name = "check24";
			this.check24.Size = new System.Drawing.Size(299,18);
			this.check24.TabIndex = 17;
			this.check24.Text = "E-mail Transaction";
			this.check24.UseVisualStyleBackColor = true;
			// 
			// check14
			// 
			this.check14.Location = new System.Drawing.Point(16,235);
			this.check14.Name = "check14";
			this.check14.Size = new System.Drawing.Size(299,18);
			this.check14.TabIndex = 16;
			this.check14.Text = "Outstanding Transaction Ack";
			this.check14.UseVisualStyleBackColor = true;
			// 
			// check13e
			// 
			this.check13e.Location = new System.Drawing.Point(16,199);
			this.check13e.Name = "check13e";
			this.check13e.Size = new System.Drawing.Size(299,18);
			this.check13e.TabIndex = 15;
			this.check13e.Text = "Predetermination Ack Embedded";
			this.check13e.UseVisualStyleBackColor = true;
			// 
			// check13
			// 
			this.check13.Location = new System.Drawing.Point(16,181);
			this.check13.Name = "check13";
			this.check13.Size = new System.Drawing.Size(299,18);
			this.check13.TabIndex = 14;
			this.check13.Text = "Predetermination Ack";
			this.check13.UseVisualStyleBackColor = true;
			// 
			// check03m
			// 
			this.check03m.Location = new System.Drawing.Point(16,163);
			this.check03m.Name = "check03m";
			this.check03m.Size = new System.Drawing.Size(299,18);
			this.check03m.TabIndex = 13;
			this.check03m.Text = "Predetermination Multi-page";
			this.check03m.UseVisualStyleBackColor = true;
			// 
			// check12
			// 
			this.check12.Location = new System.Drawing.Point(16,127);
			this.check12.Name = "check12";
			this.check12.Size = new System.Drawing.Size(299,18);
			this.check12.TabIndex = 12;
			this.check12.Text = "Claim Reversal Response";
			this.check12.UseVisualStyleBackColor = true;
			// 
			// check21e
			// 
			this.check21e.Location = new System.Drawing.Point(16,91);
			this.check21e.Name = "check21e";
			this.check21e.Size = new System.Drawing.Size(299,18);
			this.check21e.TabIndex = 11;
			this.check21e.Text = "Claim EOB Embedded";
			this.check21e.UseVisualStyleBackColor = true;
			// 
			// check11e
			// 
			this.check11e.Location = new System.Drawing.Point(16,73);
			this.check11e.Name = "check11e";
			this.check11e.Size = new System.Drawing.Size(299,18);
			this.check11e.TabIndex = 10;
			this.check11e.Text = "Claim Ack Embedded";
			this.check11e.UseVisualStyleBackColor = true;
			// 
			// check18
			// 
			this.check18.Location = new System.Drawing.Point(16,37);
			this.check18.Name = "check18";
			this.check18.Size = new System.Drawing.Size(299,18);
			this.check18.TabIndex = 9;
			this.check18.Text = "Eligibility Response";
			this.check18.UseVisualStyleBackColor = true;
			// 
			// check03
			// 
			this.check03.Location = new System.Drawing.Point(16,145);
			this.check03.Name = "check03";
			this.check03.Size = new System.Drawing.Size(299,18);
			this.check03.TabIndex = 8;
			this.check03.Text = "Predetermination Single Page";
			this.check03.UseVisualStyleBackColor = true;
			// 
			// check07
			// 
			this.check07.Location = new System.Drawing.Point(16,55);
			this.check07.Name = "check07";
			this.check07.Size = new System.Drawing.Size(299,18);
			this.check07.TabIndex = 5;
			this.check07.Text = "COB Claim Transaction";
			this.check07.UseVisualStyleBackColor = true;
			// 
			// check06
			// 
			this.check06.Location = new System.Drawing.Point(16,307);
			this.check06.Name = "check06";
			this.check06.Size = new System.Drawing.Size(299,18);
			this.check06.TabIndex = 4;
			this.check06.Text = "Request for Payment Reconciliation";
			this.check06.UseVisualStyleBackColor = true;
			// 
			// check04
			// 
			this.check04.Location = new System.Drawing.Point(16,217);
			this.check04.Name = "check04";
			this.check04.Size = new System.Drawing.Size(299,18);
			this.check04.TabIndex = 3;
			this.check04.Text = "Request for Outstanding Transactions [Mailbox]";
			this.check04.UseVisualStyleBackColor = true;
			// 
			// check05
			// 
			this.check05.Location = new System.Drawing.Point(16,271);
			this.check05.Name = "check05";
			this.check05.Size = new System.Drawing.Size(299,18);
			this.check05.TabIndex = 2;
			this.check05.Text = "Request for Summary Reconciliation";
			this.check05.UseVisualStyleBackColor = true;
			// 
			// check02
			// 
			this.check02.Location = new System.Drawing.Point(16,109);
			this.check02.Name = "check02";
			this.check02.Size = new System.Drawing.Size(299,18);
			this.check02.TabIndex = 1;
			this.check02.Text = "Claim Reversal";
			this.check02.UseVisualStyleBackColor = true;
			// 
			// check08
			// 
			this.check08.Location = new System.Drawing.Point(16,19);
			this.check08.Name = "check08";
			this.check08.Size = new System.Drawing.Size(299,18);
			this.check08.TabIndex = 0;
			this.check08.Text = "Eligibility Transaction";
			this.check08.UseVisualStyleBackColor = true;
			// 
			// textModemReconcile
			// 
			this.textModemReconcile.Location = new System.Drawing.Point(191,243);
			this.textModemReconcile.Name = "textModemReconcile";
			this.textModemReconcile.Size = new System.Drawing.Size(121,20);
			this.textModemReconcile.TabIndex = 109;
			this.textModemReconcile.Visible = false;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(4,242);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(185,32);
			this.label10.TabIndex = 110;
			this.label10.Text = "Modem Phone Number - Request for Payment Reconciliation";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.label10.Visible = false;
			// 
			// textModemSummary
			// 
			this.textModemSummary.Location = new System.Drawing.Point(191,207);
			this.textModemSummary.Name = "textModemSummary";
			this.textModemSummary.Size = new System.Drawing.Size(121,20);
			this.textModemSummary.TabIndex = 107;
			this.textModemSummary.Visible = false;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(1,207);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(188,33);
			this.label8.TabIndex = 108;
			this.label8.Text = "Modem Phone Number - Request for Summary Reconciliation";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.label8.Visible = false;
			// 
			// textModem
			// 
			this.textModem.Location = new System.Drawing.Point(191,181);
			this.textModem.Name = "textModem";
			this.textModem.Size = new System.Drawing.Size(121,20);
			this.textModem.TabIndex = 105;
			this.textModem.Visible = false;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(38,186);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(151,17);
			this.label7.TabIndex = 106;
			this.label7.Text = "Modem Phone Number";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.label7.Visible = false;
			// 
			// comboNetwork
			// 
			this.comboNetwork.FormattingEnabled = true;
			this.comboNetwork.Location = new System.Drawing.Point(191,13);
			this.comboNetwork.Name = "comboNetwork";
			this.comboNetwork.Size = new System.Drawing.Size(259,21);
			this.comboNetwork.TabIndex = 104;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(38,16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(151,17);
			this.label5.TabIndex = 103;
			this.label5.Text = "Network";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textVersion
			// 
			this.textVersion.Location = new System.Drawing.Point(191,41);
			this.textVersion.Name = "textVersion";
			this.textVersion.Size = new System.Drawing.Size(42,20);
			this.textVersion.TabIndex = 100;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(38,46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(151,17);
			this.label1.TabIndex = 101;
			this.label1.Text = "Version Number";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkPMP
			// 
			this.checkPMP.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPMP.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPMP.Location = new System.Drawing.Point(37,68);
			this.checkPMP.Name = "checkPMP";
			this.checkPMP.Size = new System.Drawing.Size(168,17);
			this.checkPMP.TabIndex = 99;
			this.checkPMP.Text = "Provincial Medical Plan";
			this.checkPMP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIsHidden
			// 
			this.checkIsHidden.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsHidden.Location = new System.Drawing.Point(89,137);
			this.checkIsHidden.Name = "checkIsHidden";
			this.checkIsHidden.Size = new System.Drawing.Size(104,17);
			this.checkIsHidden.TabIndex = 100;
			this.checkIsHidden.Text = "Hidden";
			this.checkIsHidden.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.butDelete.Location = new System.Drawing.Point(29,642);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(90,26);
			this.butDelete.TabIndex = 24;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(752,641);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(78,26);
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(854,641);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(78,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormCarrierEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(962,675);
			this.Controls.Add(this.checkIsHidden);
			this.Controls.Add(this.groupCDAnet);
			this.Controls.Add(this.textCity);
			this.Controls.Add(this.textState);
			this.Controls.Add(this.textZip);
			this.Controls.Add(this.textElectID);
			this.Controls.Add(this.textAddress2);
			this.Controls.Add(this.textAddress);
			this.Controls.Add(this.textPhone);
			this.Controls.Add(this.textCarrierName);
			this.Controls.Add(this.labelCitySt);
			this.Controls.Add(this.checkNoSendElect);
			this.Controls.Add(this.checkIsCDAnet);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.labelElectID);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCarrierEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Carrier";
			this.Load += new System.EventHandler(this.FormCarrierEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupCDAnet.ResumeLayout(false);
			this.groupCDAnet.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormCarrierEdit_Load(object sender, System.EventArgs e) {
			textCarrierName.Text=CarrierCur.CarrierName;
			textPhone.Text=CarrierCur.Phone;
			textAddress.Text=CarrierCur.Address;
			textAddress2.Text=CarrierCur.Address2;
			textCity.Text=CarrierCur.City;
			textState.Text=CarrierCur.State;
			textZip.Text=CarrierCur.Zip;
			textElectID.Text=CarrierCur.ElectID;
			checkNoSendElect.Checked=CarrierCur.NoSendElect;
			checkIsHidden.Checked=CarrierCur.IsHidden;
			string[] dependentPlans=Carriers.DependentPlans(CarrierCur);
			textPlans.Text=dependentPlans.Length.ToString();
			comboPlans.Items.Clear();
			for(int i=0;i<dependentPlans.Length;i++){
				comboPlans.Items.Add(dependentPlans[i]);
			}
			if(dependentPlans.Length>0){
				comboPlans.SelectedIndex=0;
			}
			//textTemplates.Text=Carriers.DependentTemplates().ToString();
			checkIsCDAnet.Checked=CarrierCur.IsCDA;//Can be checked but not visible.
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA")){//en-CA or fr-CA
				labelCitySt.Text="City,Province,PostalCode";
				labelElectID.Text="Carrier Identification Number";
				groupCDAnet.Visible=checkIsCDAnet.Checked;
			}
			else{//everyone but Canada
				checkIsCDAnet.Visible=false;
				groupCDAnet.Visible=false;
				this.Size=new Size(525,300);//make it smaller
			}
			//Canadian stuff is filled in for everyone, because a Canadian user might sometimes have a computer set to American.
			//So a computer set to American would not be able to SEE the Canadian fields, but they at least would not be damaged.
			comboNetwork.Items.Add(Lan.g(this,"none"));
			comboNetwork.SelectedIndex=0;
			for(int i=0;i<CanadianNetworks.Listt.Count;i++) {
				comboNetwork.Items.Add(CanadianNetworks.Listt[i].Abbrev+" - "+CanadianNetworks.Listt[i].Descript);
				if(CarrierCur.CanadianNetworkNum==CanadianNetworks.Listt[i].CanadianNetworkNum) {
					comboNetwork.SelectedIndex=i+1;
				}
			}
			textVersion.Text=CarrierCur.CDAnetVersion;
			checkPMP.Checked=CarrierCur.IsPMP;
			if(CarrierCur.CanadianEncryptionMethod==(byte)0) {
				textEncryptionMethod.Text="";
			}
			else {
				textEncryptionMethod.Text=CarrierCur.CanadianEncryptionMethod.ToString();
			}
			textTransactionPrefix.Text=CarrierCur.CanadianTransactionPrefix;
			check08.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.EligibilityTransaction_08) == CanSupTransTypes.EligibilityTransaction_08;
			check18.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.EligibilityResponse_18) == CanSupTransTypes.EligibilityResponse_18;
			check07.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.CobClaimTransaction_07) == CanSupTransTypes.CobClaimTransaction_07;
			check11e.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.ClaimAckEmbedded_11e) == CanSupTransTypes.ClaimAckEmbedded_11e;
			check21e.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.ClaimEobEmbedded_21e) == CanSupTransTypes.ClaimEobEmbedded_21e;
			check02.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.ClaimReversal_02) == CanSupTransTypes.ClaimReversal_02;
			check12.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.ClaimReversalResponse_12) == CanSupTransTypes.ClaimReversalResponse_12;
			check03.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.PredeterminationSinglePage_03) == CanSupTransTypes.PredeterminationSinglePage_03;
			check03m.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.PredeterminationMultiPage_03) == CanSupTransTypes.PredeterminationMultiPage_03;
			check13.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.PredeterminationAck_13) == CanSupTransTypes.PredeterminationAck_13;
			check13e.Checked
				=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.PredeterminationAckEmbedded_13e) == CanSupTransTypes.PredeterminationAckEmbedded_13e;
			check04.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.RequestForOutstandingTrans_04) == CanSupTransTypes.RequestForOutstandingTrans_04;
			check14.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.OutstandingTransAck_14) == CanSupTransTypes.OutstandingTransAck_14;
			check24.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.EmailTransaction_24) == CanSupTransTypes.EmailTransaction_24;
			check05.Checked
				=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.RequestForSummaryReconciliation_05) == CanSupTransTypes.RequestForSummaryReconciliation_05;
			check15.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.SummaryReconciliation_15) == CanSupTransTypes.SummaryReconciliation_15;
			check06.Checked
				=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.RequestForPaymentReconciliation_06) == CanSupTransTypes.RequestForPaymentReconciliation_06;
			check16.Checked=(CarrierCur.CanadianSupportedTypes & CanSupTransTypes.PaymentReconciliation_16) == CanSupTransTypes.PaymentReconciliation_16;
		}

		private void textCarrierName_TextChanged(object sender, System.EventArgs e) {
			if(textCarrierName.Text.Length==1){
				textCarrierName.Text=textCarrierName.Text.ToUpper();
				textCarrierName.SelectionStart=1;
			}
		}

		private void textPhone_TextChanged(object sender, System.EventArgs e) {
			int cursor=textPhone.SelectionStart;
			int length=textPhone.Text.Length;
			textPhone.Text=TelephoneNumbers.AutoFormat(textPhone.Text);
			if(textPhone.Text.Length>length)
				cursor++;
			textPhone.SelectionStart=cursor;		
		}

		private void textAddress_TextChanged(object sender, System.EventArgs e) {
			if(textAddress.Text.Length==1){
				textAddress.Text=textAddress.Text.ToUpper();
				textAddress.SelectionStart=1;
			}
		}

		private void textAddress2_TextChanged(object sender, System.EventArgs e) {
			if(textAddress2.Text.Length==1){
				textAddress2.Text=textAddress2.Text.ToUpper();
				textAddress2.SelectionStart=1;
			}
		}

		private void textCity_TextChanged(object sender, System.EventArgs e) {
			if(textCity.Text.Length==1){
				textCity.Text=textCity.Text.ToUpper();
				textCity.SelectionStart=1;
			}
		}

		private void textState_TextChanged(object sender, System.EventArgs e) {
			int cursor=textState.SelectionStart;
			//for all countries, capitalize the first letter
			if(textState.Text.Length==1){
				textState.Text=textState.Text.ToUpper();
				textState.SelectionStart=cursor;
				return;
			}
			//for US and Canada, capitalize second letter as well.
			if(CultureInfo.CurrentCulture.Name=="en-US"
				|| CultureInfo.CurrentCulture.Name=="en-CA"){
				if(textState.Text.Length==2){
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=cursor;
				}
			}
		}

		private void checkIsCDAnet_Click(object sender,EventArgs e) {
			groupCDAnet.Visible=checkIsCDAnet.Checked;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete Carrier?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			try{
				Carriers.Delete(CarrierCur);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textCarrierName.Text==""){
				MessageBox.Show(Lan.g(this,"Carrier Name cannot be blank."));
				return;
			}
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA") && checkIsCDAnet.Checked) {//if Canadian computer and Canadian carrier
				if(textVersion.Text!="02" && textVersion.Text!="03" && textVersion.Text!="04") {
					MsgBox.Show(this,"Version Number must be 02, 03, or 04.");
					return;
				}
				if(textEncryptionMethod.Text!="1" && textEncryptionMethod.Text!="2" && textEncryptionMethod.Text!="3") {
					MsgBox.Show(this,"Encryption method must be 1, 2, or 3.");
					return;
				}
				if(textTransactionPrefix.Text=="") {
					MsgBox.Show(this,"Transaction prefix must not be blank.");
					return;
				}
			}
			CarrierCur.CarrierName=textCarrierName.Text;
			CarrierCur.Phone=textPhone.Text;
			CarrierCur.Address=textAddress.Text;
			CarrierCur.Address2=textAddress2.Text;
			CarrierCur.City=textCity.Text;
			CarrierCur.State=textState.Text;
			CarrierCur.Zip=textZip.Text;
			CarrierCur.ElectID=textElectID.Text;
			CarrierCur.NoSendElect=checkNoSendElect.Checked;
			CarrierCur.IsHidden=checkIsHidden.Checked;
			if(checkIsCDAnet.Checked){//even if it's hidden
				CarrierCur.IsCDA=true;
				if(comboNetwork.SelectedIndex==0){
					CarrierCur.CanadianNetworkNum=0;
				}
				else{
					CarrierCur.CanadianNetworkNum=CanadianNetworks.Listt[comboNetwork.SelectedIndex-1].CanadianNetworkNum;
				}
				CarrierCur.CDAnetVersion=textVersion.Text;
				CarrierCur.IsPMP=checkPMP.Checked;
				CarrierCur.CanadianEncryptionMethod=PIn.Byte(textEncryptionMethod.Text);//validated.
				CarrierCur.CanadianTransactionPrefix=textTransactionPrefix.Text;
				CarrierCur.CanadianSupportedTypes=CanSupTransTypes.None;
				if(check08.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.EligibilityTransaction_08;
				}
				if(check18.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.EligibilityResponse_18;
				}
				if(check07.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.CobClaimTransaction_07;
				}
				if(check11e.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.ClaimAckEmbedded_11e;
				}
				if(check21e.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.ClaimEobEmbedded_21e;
				}
				if(check02.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.ClaimReversal_02;
				}
				if(check12.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.ClaimReversalResponse_12;
				}
				if(check03.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.PredeterminationSinglePage_03;
				}
				if(check03m.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.PredeterminationMultiPage_03;
				}
				if(check13.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.PredeterminationAck_13;
				}
				if(check13e.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.PredeterminationAckEmbedded_13e;
				}
				if(check04.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.RequestForOutstandingTrans_04;
				}
				if(check14.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.OutstandingTransAck_14;
				}
				if(check24.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.EmailTransaction_24;
				}
				if(check05.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.RequestForSummaryReconciliation_05;
				}
				if(check15.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.SummaryReconciliation_15;
				}
				if(check06.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.RequestForPaymentReconciliation_06;
				}
				if(check16.Checked) {
					CarrierCur.CanadianSupportedTypes=CarrierCur.CanadianSupportedTypes | CanSupTransTypes.PaymentReconciliation_16;
				}
			}
			else{
				CarrierCur.IsCDA=false;
				CarrierCur.CanadianNetworkNum=0;
				CarrierCur.CDAnetVersion="";
				CarrierCur.IsPMP=false;
				CarrierCur.CanadianEncryptionMethod=0;
				CarrierCur.CanadianTransactionPrefix="";
				CarrierCur.CanadianSupportedTypes=CanSupTransTypes.None;
			}
			if(IsNew){
				try{
					Carriers.Insert(CarrierCur);
				}
				catch(ApplicationException ex){
					MessageBox.Show(ex.Message);
					return;
				}
			}
			else{
				try{
					Carriers.Update(CarrierCur);
				}
				catch(ApplicationException ex){
					MessageBox.Show(ex.Message);
					return;
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


		

		

		

		

		

		

		


	}
}





















