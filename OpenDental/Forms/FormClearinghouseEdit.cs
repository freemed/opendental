using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormClearinghouseEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.CheckBox checkIsDefault;
		private System.Windows.Forms.TextBox textPayors;
		private System.Windows.Forms.Label label3;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.TextBox textExportPath;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textISA08;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox comboFormat;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textResponsePath;
		private System.Windows.Forms.ComboBox comboCommBridge;
		private System.Windows.Forms.TextBox textClientProgram;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textModemPort;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textLoginID;
		private System.Windows.Forms.Label label15;
		private GroupBox groupBox1;
		private Label label9;
		private TextBox textISA05;
		private Label label16;
		private TextBox textSenderTIN;
		private Label label19;
		private TextBox textISA07;
		private Label label20;
		private Label label21;
		private Label label22;
		private TextBox textISA15;
		private Label label23;
		private GroupBox groupBox2;
		private Label label27;
		private Label label26;
		private Label label25;
		private TextBox textSenderTelephone;
		private TextBox textSenderName;
		private RadioButton radioSenderBelow;
		private RadioButton radioSenderOD;
		private Label label17;
		private Label label18;
		private TextBox textGS03;
		private Label label24;
		private Label label29;
		private Label label30;
		///<summary>Set this externally before opening the form</summary>
		public Clearinghouse ClearinghouseCur;

		///<summary></summary>
		public FormClearinghouseEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			Lan.C(this, new System.Windows.Forms.Control[]
			{
				this.textBox2
			});
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClearinghouseEdit));
			this.textDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textExportPath = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.checkIsDefault = new System.Windows.Forms.CheckBox();
			this.textPayors = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textISA08 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textResponsePath = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.comboFormat = new System.Windows.Forms.ComboBox();
			this.comboCommBridge = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textClientProgram = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.textModemPort = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.textLoginID = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label18 = new System.Windows.Forms.Label();
			this.textGS03 = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label29 = new System.Windows.Forms.Label();
			this.textSenderTelephone = new System.Windows.Forms.TextBox();
			this.textSenderName = new System.Windows.Forms.TextBox();
			this.radioSenderBelow = new System.Windows.Forms.RadioButton();
			this.radioSenderOD = new System.Windows.Forms.RadioButton();
			this.label27 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.textSenderTIN = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.textISA15 = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.textISA07 = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.textISA05 = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.label30 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(251,10);
			this.textDescription.MaxLength = 255;
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(226,20);
			this.textDescription.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(78,394);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(172,17);
			this.label2.TabIndex = 6;
			this.label2.Text = "Claim Export Path";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textExportPath
			// 
			this.textExportPath.Location = new System.Drawing.Point(251,391);
			this.textExportPath.MaxLength = 255;
			this.textExportPath.Name = "textExportPath";
			this.textExportPath.Size = new System.Drawing.Size(317,20);
			this.textExportPath.TabIndex = 4;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(35,13);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(214,17);
			this.label6.TabIndex = 14;
			this.label6.Text = "Description";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkIsDefault
			// 
			this.checkIsDefault.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsDefault.Location = new System.Drawing.Point(101,526);
			this.checkIsDefault.Name = "checkIsDefault";
			this.checkIsDefault.Size = new System.Drawing.Size(163,17);
			this.checkIsDefault.TabIndex = 10;
			this.checkIsDefault.Text = "Is Default";
			this.checkIsDefault.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayors
			// 
			this.textPayors.Location = new System.Drawing.Point(251,545);
			this.textPayors.MaxLength = 255;
			this.textPayors.Multiline = true;
			this.textPayors.Name = "textPayors";
			this.textPayors.Size = new System.Drawing.Size(377,60);
			this.textPayors.TabIndex = 11;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(99,548);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(151,17);
			this.label1.TabIndex = 95;
			this.label1.Text = "Payors";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBox2
			// 
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Location = new System.Drawing.Point(251,609);
			this.textBox2.MaxLength = 255;
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(378,60);
			this.textBox2.TabIndex = 96;
			this.textBox2.Text = "The list of payor IDs should be separated by commas with no spaces or other punct" +
    "uation.  For instance: 01234,23456 is valid.  You do not have to enter any payor" +
    " ID\'s for a default Clearinghouse.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(270,527);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(366,15);
			this.label3.TabIndex = 97;
			this.label3.Text = "(if this is your main clearinghouse for most claims)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(99,437);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(151,17);
			this.label4.TabIndex = 98;
			this.label4.Text = "Format";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textISA08
			// 
			this.textISA08.Location = new System.Drawing.Point(242,207);
			this.textISA08.MaxLength = 255;
			this.textISA08.Name = "textISA08";
			this.textISA08.Size = new System.Drawing.Size(96,20);
			this.textISA08.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(9,208);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(232,17);
			this.label5.TabIndex = 101;
			this.label5.Text = "Clearinghouse ID (ISA08)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textResponsePath
			// 
			this.textResponsePath.Location = new System.Drawing.Point(251,413);
			this.textResponsePath.MaxLength = 255;
			this.textResponsePath.Name = "textResponsePath";
			this.textResponsePath.Size = new System.Drawing.Size(317,20);
			this.textResponsePath.TabIndex = 5;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(78,416);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(172,17);
			this.label10.TabIndex = 107;
			this.label10.Text = "Report Path";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(251,369);
			this.textPassword.MaxLength = 255;
			this.textPassword.Name = "textPassword";
			this.textPassword.Size = new System.Drawing.Size(96,20);
			this.textPassword.TabIndex = 3;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(98,372);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(151,17);
			this.label11.TabIndex = 109;
			this.label11.Text = "Password";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboFormat
			// 
			this.comboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFormat.Location = new System.Drawing.Point(251,435);
			this.comboFormat.Name = "comboFormat";
			this.comboFormat.Size = new System.Drawing.Size(145,21);
			this.comboFormat.TabIndex = 6;
			// 
			// comboCommBridge
			// 
			this.comboCommBridge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCommBridge.Location = new System.Drawing.Point(251,458);
			this.comboCommBridge.MaxDropDownItems = 20;
			this.comboCommBridge.Name = "comboCommBridge";
			this.comboCommBridge.Size = new System.Drawing.Size(145,21);
			this.comboCommBridge.TabIndex = 7;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(98,461);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(151,17);
			this.label7.TabIndex = 111;
			this.label7.Text = "Comm Bridge";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textClientProgram
			// 
			this.textClientProgram.Location = new System.Drawing.Point(251,503);
			this.textClientProgram.MaxLength = 255;
			this.textClientProgram.Name = "textClientProgram";
			this.textClientProgram.Size = new System.Drawing.Size(317,20);
			this.textClientProgram.TabIndex = 9;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(78,506);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(172,17);
			this.label8.TabIndex = 114;
			this.label8.Text = "Launch Client Program";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(248,324);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(358,20);
			this.label12.TabIndex = 115;
			this.label12.Text = "Not all values are required by each clearinghouse / carrier.";
			// 
			// textModemPort
			// 
			this.textModemPort.Location = new System.Drawing.Point(251,481);
			this.textModemPort.MaxLength = 255;
			this.textModemPort.Name = "textModemPort";
			this.textModemPort.Size = new System.Drawing.Size(32,20);
			this.textModemPort.TabIndex = 8;
			this.textModemPort.Visible = false;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(78,484);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(172,17);
			this.label13.TabIndex = 117;
			this.label13.Text = "Modem Port (1-4)";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.label13.Visible = false;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(287,485);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(267,17);
			this.label14.TabIndex = 118;
			this.label14.Text = "(only if dialup)";
			this.label14.Visible = false;
			// 
			// textLoginID
			// 
			this.textLoginID.Location = new System.Drawing.Point(251,347);
			this.textLoginID.MaxLength = 255;
			this.textLoginID.Name = "textLoginID";
			this.textLoginID.Size = new System.Drawing.Size(96,20);
			this.textLoginID.TabIndex = 2;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(98,350);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(151,17);
			this.label15.TabIndex = 120;
			this.label15.Text = "Login ID";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.textGS03);
			this.groupBox1.Controls.Add(this.label24);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.label22);
			this.groupBox1.Controls.Add(this.textISA15);
			this.groupBox1.Controls.Add(this.label23);
			this.groupBox1.Controls.Add(this.label21);
			this.groupBox1.Controls.Add(this.label19);
			this.groupBox1.Controls.Add(this.textISA07);
			this.groupBox1.Controls.Add(this.label20);
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.textISA05);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.textISA08);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new System.Drawing.Point(9,36);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(620,280);
			this.groupBox1.TabIndex = 121;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "X12 Required Fields - Provided by Clearinghouse or Carrier";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(339,231);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(231,15);
			this.label18.TabIndex = 118;
			this.label18.Text = "Usually the same as ISA08";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textGS03
			// 
			this.textGS03.Location = new System.Drawing.Point(242,229);
			this.textGS03.MaxLength = 255;
			this.textGS03.Name = "textGS03";
			this.textGS03.Size = new System.Drawing.Size(96,20);
			this.textGS03.TabIndex = 116;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(9,230);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(232,17);
			this.label24.TabIndex = 117;
			this.label24.Text = "GS03";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label30);
			this.groupBox2.Controls.Add(this.label29);
			this.groupBox2.Controls.Add(this.textSenderTelephone);
			this.groupBox2.Controls.Add(this.textSenderName);
			this.groupBox2.Controls.Add(this.radioSenderBelow);
			this.groupBox2.Controls.Add(this.radioSenderOD);
			this.groupBox2.Controls.Add(this.label27);
			this.groupBox2.Controls.Add(this.label26);
			this.groupBox2.Controls.Add(this.label25);
			this.groupBox2.Controls.Add(this.textSenderTIN);
			this.groupBox2.Location = new System.Drawing.Point(12,47);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(547,132);
			this.groupBox2.TabIndex = 115;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Sender ID - Used in ISA06, GS02, 1000A NM1, and 1000A PER";
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(248,37);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(231,15);
			this.label29.TabIndex = 117;
			this.label29.Text = "(much more common)";
			this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textSenderTelephone
			// 
			this.textSenderTelephone.Location = new System.Drawing.Point(230,104);
			this.textSenderTelephone.MaxLength = 255;
			this.textSenderTelephone.Name = "textSenderTelephone";
			this.textSenderTelephone.Size = new System.Drawing.Size(96,20);
			this.textSenderTelephone.TabIndex = 115;
			// 
			// textSenderName
			// 
			this.textSenderName.Location = new System.Drawing.Point(230,82);
			this.textSenderName.MaxLength = 255;
			this.textSenderName.Name = "textSenderName";
			this.textSenderName.Size = new System.Drawing.Size(226,20);
			this.textSenderName.TabIndex = 114;
			// 
			// radioSenderBelow
			// 
			this.radioSenderBelow.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioSenderBelow.Checked = true;
			this.radioSenderBelow.Location = new System.Drawing.Point(1,36);
			this.radioSenderBelow.Name = "radioSenderBelow";
			this.radioSenderBelow.Size = new System.Drawing.Size(242,18);
			this.radioSenderBelow.TabIndex = 113;
			this.radioSenderBelow.TabStop = true;
			this.radioSenderBelow.Text = "The information below identifies the sender";
			this.radioSenderBelow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioSenderBelow.UseVisualStyleBackColor = true;
			this.radioSenderBelow.Click += new System.EventHandler(this.radio_Click);
			// 
			// radioSenderOD
			// 
			this.radioSenderOD.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioSenderOD.Location = new System.Drawing.Point(22,17);
			this.radioSenderOD.Name = "radioSenderOD";
			this.radioSenderOD.Size = new System.Drawing.Size(221,18);
			this.radioSenderOD.TabIndex = 112;
			this.radioSenderOD.Text = "This software is the \"sender\"";
			this.radioSenderOD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioSenderOD.UseVisualStyleBackColor = true;
			this.radioSenderOD.Click += new System.EventHandler(this.radio_Click);
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(37,105);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(191,17);
			this.label27.TabIndex = 111;
			this.label27.Text = "Telephone Number";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(37,83);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(191,17);
			this.label26.TabIndex = 110;
			this.label26.Text = "Name";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(37,61);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(191,17);
			this.label25.TabIndex = 109;
			this.label25.Text = "Tax ID Number";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSenderTIN
			// 
			this.textSenderTIN.Location = new System.Drawing.Point(230,60);
			this.textSenderTIN.MaxLength = 255;
			this.textSenderTIN.Name = "textSenderTIN";
			this.textSenderTIN.Size = new System.Drawing.Size(96,20);
			this.textSenderTIN.TabIndex = 106;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(290,253);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(280,15);
			this.label22.TabIndex = 114;
			this.label22.Text = "\"P\" for Production.  \"T\" for Test.";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textISA15
			// 
			this.textISA15.Location = new System.Drawing.Point(242,251);
			this.textISA15.MaxLength = 255;
			this.textISA15.Name = "textISA15";
			this.textISA15.Size = new System.Drawing.Size(42,20);
			this.textISA15.TabIndex = 112;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(9,252);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(232,17);
			this.label23.TabIndex = 113;
			this.label23.Text = "Test or Production (ISA15)";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(339,209);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(231,15);
			this.label21.TabIndex = 111;
			this.label21.Text = "Also used in 1000B NM109. ";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(339,187);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(231,15);
			this.label19.TabIndex = 110;
			this.label19.Text = "Usually \"ZZ\", sometimes \"30\"";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textISA07
			// 
			this.textISA07.Location = new System.Drawing.Point(242,185);
			this.textISA07.MaxLength = 255;
			this.textISA07.Name = "textISA07";
			this.textISA07.Size = new System.Drawing.Size(96,20);
			this.textISA07.TabIndex = 109;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(6,186);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(235,17);
			this.label20.TabIndex = 108;
			this.label20.Text = "Receiver ID Qualifier (ISA07)";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(339,21);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(231,15);
			this.label16.TabIndex = 104;
			this.label16.Text = "Usually \"ZZ\", sometimes \"30\"";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textISA05
			// 
			this.textISA05.Location = new System.Drawing.Point(242,19);
			this.textISA05.MaxLength = 255;
			this.textISA05.Name = "textISA05";
			this.textISA05.Size = new System.Drawing.Size(96,20);
			this.textISA05.TabIndex = 103;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(6,20);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(235,17);
			this.label9.TabIndex = 102;
			this.label9.Text = "Sender ID Qualifier (ISA05)";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(479,12);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(366,15);
			this.label17.TabIndex = 122;
			this.label17.Text = "Also used in X12 1000B NM103";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.butDelete.Location = new System.Drawing.Point(35,621);
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
			this.butOK.Location = new System.Drawing.Point(713,578);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(78,26);
			this.butOK.TabIndex = 12;
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
			this.butCancel.Location = new System.Drawing.Point(713,621);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(78,26);
			this.butCancel.TabIndex = 13;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(248,17);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(231,15);
			this.label30.TabIndex = 118;
			this.label30.Text = "(use this for Emdeon)";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormClearinghouseEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(816,667);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textLoginID);
			this.Controls.Add(this.textModemPort);
			this.Controls.Add(this.textClientProgram);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.textResponsePath);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textPayors);
			this.Controls.Add(this.textExportPath);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.comboCommBridge);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.comboFormat);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkIsDefault);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClearinghouseEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Clearinghouse or Direct Carrier";
			this.Load += new System.EventHandler(this.FormClearinghouseEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClearinghouseEdit_Load(object sender, System.EventArgs e) {
			textDescription.Text=ClearinghouseCur.Description;
			textISA05.Text=ClearinghouseCur.ISA05;
			if(ClearinghouseCur.SenderTIN==""){
				radioSenderOD.Checked=true;
				radioSenderBelow.Checked=false;
				textSenderTIN.Text="";
				textSenderName.Text="";
				textSenderTelephone.Text="";
			}
			else{
				radioSenderOD.Checked=false;
				radioSenderBelow.Checked=true;
				textSenderTIN.Text=ClearinghouseCur.SenderTIN;
				textSenderName.Text=ClearinghouseCur.SenderName;
				textSenderTelephone.Text=ClearinghouseCur.SenderTelephone;
			}
			textISA07.Text=ClearinghouseCur.ISA07;
			textISA08.Text=ClearinghouseCur.ISA08;
			textISA15.Text=ClearinghouseCur.ISA15;
			textGS03.Text=ClearinghouseCur.GS03;
			textLoginID.Text=ClearinghouseCur.LoginID;
			textPassword.Text=ClearinghouseCur.Password;
			textExportPath.Text=ClearinghouseCur.ExportPath;
			textResponsePath.Text=ClearinghouseCur.ResponsePath;
			for(int i=0;i<Enum.GetNames(typeof(ElectronicClaimFormat)).Length;i++){
				comboFormat.Items.Add(Enum.GetNames(typeof(ElectronicClaimFormat))[i]);
			}
			comboFormat.SelectedIndex=(int)ClearinghouseCur.Eformat;
			for(int i=0;i<Enum.GetNames(typeof(EclaimsCommBridge)).Length;i++){
				comboCommBridge.Items.Add(Enum.GetNames(typeof(EclaimsCommBridge))[i]);
			}
			comboCommBridge.SelectedIndex=(int)ClearinghouseCur.CommBridge;
			textModemPort.Text=ClearinghouseCur.ModemPort.ToString();
			textClientProgram.Text=ClearinghouseCur.ClientProgram;
			checkIsDefault.Checked=ClearinghouseCur.IsDefault;
			textPayors.Text=ClearinghouseCur.Payors;
		}

		private void radio_Click(object sender,EventArgs e) {
			if(radioSenderOD.Checked) {
				textSenderTIN.Text="";
				textSenderName.Text="";
				textSenderTelephone.Text="";
			}
			else {
				textSenderTIN.Text=ClearinghouseCur.SenderTIN;
				textSenderName.Text=ClearinghouseCur.SenderName;
				textSenderTelephone.Text=ClearinghouseCur.SenderTelephone;
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(!MsgBox.Show(this,true,"Delete this Clearinghouse?")){
				return;
			}
			Clearinghouses.Delete(ClearinghouseCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDescription.Text==""){
				MsgBox.Show(this,"Description cannot be blank.");
				return;
			}
			if(textISA08.Text=="0135WCH00" && !radioSenderOD.Checked){
				MsgBox.Show(this,"When using Emdeon, this software must be the sender.");
				return;
			}
			if(comboFormat.SelectedIndex==(int)ElectronicClaimFormat.X12) {
				if(textISA05.Text==""){
					MsgBox.Show(this,"ISA05 is required.");
					return;
				}
				if(textISA05.Text!="01" && textISA05.Text!="14" && textISA05.Text!="20" && textISA05.Text!="27" && textISA05.Text!="28"
					&& textISA05.Text!="29" && textISA05.Text!="30" && textISA05.Text!="33" && textISA05.Text!="ZZ")
				{
					MsgBox.Show(this,"ISA05 is not valid.");
					return;
				}
				if(radioSenderBelow.Checked) {
					if(textSenderTIN.Text.Length<2) {
						MsgBox.Show(this,"Sender TIN is required.");
						return;
					}
					if(textSenderName.Text=="") {
						MsgBox.Show(this,"Sender Name is required.");
						return;
					}
					if(!Regex.IsMatch(textSenderTelephone.Text,@"^\d{10}$")) {
						MsgBox.Show(this,"Sender telephone must be 10 digits with no punctuation.");
						return;
					}
				}
				if(textISA07.Text=="") {
					MsgBox.Show(this,"ISA07 is required.");
					return;
				}
				if(textISA07.Text!="01" && textISA07.Text!="14" && textISA07.Text!="20" && textISA07.Text!="27" && textISA07.Text!="28"
					&& textISA07.Text!="29" && textISA07.Text!="30" && textISA07.Text!="33" && textISA07.Text!="ZZ")
				{
					MsgBox.Show(this,"ISA07 not valid.");
					return;
				}
				if(textISA08.Text.Length<2) {
					MsgBox.Show(this,"ISA08 not valid.");
					return;
				}
				if(textISA15.Text!="T" && textISA15.Text!="P") {
					MsgBox.Show(this,"ISA15 not valid.");
					return;
				}
				if(textGS03.Text.Length<2) {
					MsgBox.Show(this,"GS03 is required.");
					return;
				}
			}
//todo: Check all parts of program to allow either trailing slash or not
			if(!Directory.Exists(textExportPath.Text)){
				if(MsgBox.Show(this,true,"Export path does not exist. Attempt to create?")) {
					try{
						Directory.CreateDirectory(textExportPath.Text);
						MsgBox.Show(this,"Folder created.");
					}
					catch{
						if(!MsgBox.Show(this,true,"Not able to create folder. Continue anyway?")){
							return;
						}
					}
				}
			}
			if(!Directory.Exists(textResponsePath.Text)) {
				if(MsgBox.Show(this,true,"Report path does not exist. Attempt to create?")) {
					try {
						Directory.CreateDirectory(textResponsePath.Text);
						MsgBox.Show(this,"Folder created.");
					}
					catch {
						if(!MsgBox.Show(this,true,"Not able to create folder. Continue anyway?")) {
							return;
						}
					}
				}
			}
			if(comboFormat.SelectedIndex==0){
				if(!MsgBox.Show(this,true,"Format not selected. Claims will not send. Continue anyway?")){
					return;
				}
			}
			/*if(comboFormat.SelectedIndex==(int)ElectronicClaimFormat.X12){
				if(textISA08.Text!="BCBSGA"
					&& textISA08.Text!="100000"//Medicaid of GA
					&& textISA08.Text!="0135WCH00"//WebMD
					&& textISA08.Text!="330989922"//WebClaim
					&& textISA08.Text!="RECS"
					&& textISA08.Text!="AOS"
					&& textISA08.Text!="PostnTrack"
					)
				{
					if(!MsgBox.Show(this,true,"Clearinghouse ID not recognized. Continue anyway?")){
						return;
					}
				}
			}*/
			ClearinghouseCur.Description=textDescription.Text;
			ClearinghouseCur.ISA05=textISA05.Text;
			ClearinghouseCur.SenderTIN=textSenderTIN.Text;
			ClearinghouseCur.SenderName=textSenderName.Text;
			ClearinghouseCur.SenderTelephone=textSenderTelephone.Text;
			ClearinghouseCur.ISA07=textISA07.Text;
			ClearinghouseCur.ISA08=textISA08.Text;
			ClearinghouseCur.ISA15=textISA15.Text;
			ClearinghouseCur.GS03=textGS03.Text;
			ClearinghouseCur.LoginID=textLoginID.Text;
			ClearinghouseCur.Password=textPassword.Text;
			ClearinghouseCur.ExportPath=textExportPath.Text;
			ClearinghouseCur.ResponsePath=textResponsePath.Text;
			ClearinghouseCur.Eformat=(ElectronicClaimFormat)(comboFormat.SelectedIndex);
			ClearinghouseCur.CommBridge=(EclaimsCommBridge)(comboCommBridge.SelectedIndex);
			ClearinghouseCur.ModemPort=PIn.PInt(textModemPort.Text);
			ClearinghouseCur.ClientProgram=textClientProgram.Text;
			ClearinghouseCur.IsDefault=checkIsDefault.Checked;
			ClearinghouseCur.Payors=textPayors.Text;
			if(IsNew){
				Clearinghouses.Insert(ClearinghouseCur);
			}
			else{
				Clearinghouses.Update(ClearinghouseCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


		

		

		

		

		

		

		


	}
}





















