using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDental.ReportingOld2;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormTerminal : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butSubmit;
		private TabControl tabMain;
		private TabPage tabPage1;
		private TabPage tabPage2;
		private ValidDate textBirthdate;
		private TextBox textEmail;
		private TextBox textWirelessPhone;
		private TextBox textWkPhone;
		private TextBox textSSN;
		private TextBox textPreferred;
		private TextBox textMiddleI;
		private TextBox textFName;
		private TextBox textLName;
		private Label label7;
		private ODtextBox textAddrNotes;
		private GroupBox groupBox1;
		private TextBox textHmPhone;
		private TextBox textZip;
		private TextBox textBox1;
		private CheckBox checkSame;
		private TextBox textState;
		private Label labelST;
		private TextBox textAddress;
		private Label label12;
		private Label labelCity;
		private TextBox textAddress2;
		private Label labelZip;
		private Label label16;
		private TextBox textCity;
		private Label label11;
		private Label label22;
		private Label label18;
		private Label label17;
		private Label labelSSN;
		private Label label9;
		private Label label8;
		private Label label5;
		private Label label4;
		private Label label3;
		private Label label2;
		private CheckBox checkInsYes;
		private Label labelAddrNotes;
		private Timer timer1;
		private TextBox textWelcome;
		private IContainer components;
		//private bool IsChangingTab;
		private TerminalStatusEnum TerminalStatus;
		private TextBox textReferral;
		private Label labelReferral;
		private GroupBox groupBox2;
		private TextBox textSchool;
		private Label label30;
		private Patient PatCur;
		private ComboBox comboPosition;
		private ComboBox comboGender;
		private Label label1;
		private ComboBox comboStudentStatus;
		private Label labelConnection;
		private CheckedListBox listDiseases;
		private Family FamCur;
		private Label label6;
		private OpenDental.UI.ContrMultInput multInput;
		private Label labelIns;
		private CheckBox checkInsNo;
		private QuestionDef[] QuestionDefList;

		///<summary></summary>
		public FormTerminal()
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
			this.components = new System.ComponentModel.Container();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.checkInsNo = new System.Windows.Forms.CheckBox();
			this.labelIns = new System.Windows.Forms.Label();
			this.comboPosition = new System.Windows.Forms.ComboBox();
			this.comboGender = new System.Windows.Forms.ComboBox();
			this.textReferral = new System.Windows.Forms.TextBox();
			this.labelReferral = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboStudentStatus = new System.Windows.Forms.ComboBox();
			this.textSchool = new System.Windows.Forms.TextBox();
			this.label30 = new System.Windows.Forms.Label();
			this.checkInsYes = new System.Windows.Forms.CheckBox();
			this.labelAddrNotes = new System.Windows.Forms.Label();
			this.textAddrNotes = new OpenDental.ODtextBox();
			this.textBirthdate = new OpenDental.ValidDate();
			this.textEmail = new System.Windows.Forms.TextBox();
			this.textWirelessPhone = new System.Windows.Forms.TextBox();
			this.textWkPhone = new System.Windows.Forms.TextBox();
			this.textSSN = new System.Windows.Forms.TextBox();
			this.textPreferred = new System.Windows.Forms.TextBox();
			this.textMiddleI = new System.Windows.Forms.TextBox();
			this.textFName = new System.Windows.Forms.TextBox();
			this.textLName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textHmPhone = new System.Windows.Forms.TextBox();
			this.textZip = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.checkSame = new System.Windows.Forms.CheckBox();
			this.textState = new System.Windows.Forms.TextBox();
			this.labelST = new System.Windows.Forms.Label();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.labelCity = new System.Windows.Forms.Label();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.labelZip = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.textCity = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.labelSSN = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.listDiseases = new System.Windows.Forms.CheckedListBox();
			this.label6 = new System.Windows.Forms.Label();
			this.multInput = new OpenDental.UI.ContrMultInput();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.textWelcome = new System.Windows.Forms.TextBox();
			this.labelConnection = new System.Windows.Forms.Label();
			this.butSubmit = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.tabMain.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.tabPage1);
			this.tabMain.Controls.Add(this.tabPage2);
			this.tabMain.Location = new System.Drawing.Point(12,12);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(992,713);
			this.tabMain.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.checkInsNo);
			this.tabPage1.Controls.Add(this.labelIns);
			this.tabPage1.Controls.Add(this.comboPosition);
			this.tabPage1.Controls.Add(this.comboGender);
			this.tabPage1.Controls.Add(this.textReferral);
			this.tabPage1.Controls.Add(this.labelReferral);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.checkInsYes);
			this.tabPage1.Controls.Add(this.labelAddrNotes);
			this.tabPage1.Controls.Add(this.textAddrNotes);
			this.tabPage1.Controls.Add(this.textBirthdate);
			this.tabPage1.Controls.Add(this.textEmail);
			this.tabPage1.Controls.Add(this.textWirelessPhone);
			this.tabPage1.Controls.Add(this.textWkPhone);
			this.tabPage1.Controls.Add(this.textSSN);
			this.tabPage1.Controls.Add(this.textPreferred);
			this.tabPage1.Controls.Add(this.textMiddleI);
			this.tabPage1.Controls.Add(this.textFName);
			this.tabPage1.Controls.Add(this.textLName);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.label22);
			this.tabPage1.Controls.Add(this.label18);
			this.tabPage1.Controls.Add(this.label17);
			this.tabPage1.Controls.Add(this.labelSSN);
			this.tabPage1.Controls.Add(this.label9);
			this.tabPage1.Controls.Add(this.label8);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Location = new System.Drawing.Point(4,22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(984,687);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Patient Information";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// checkInsNo
			// 
			this.checkInsNo.Location = new System.Drawing.Point(180,78);
			this.checkInsNo.Name = "checkInsNo";
			this.checkInsNo.Size = new System.Drawing.Size(102,16);
			this.checkInsNo.TabIndex = 141;
			this.checkInsNo.Text = "No";
			this.checkInsNo.UseVisualStyleBackColor = true;
			this.checkInsNo.Click += new System.EventHandler(this.checkInsNo_Click);
			// 
			// labelIns
			// 
			this.labelIns.Location = new System.Drawing.Point(177,44);
			this.labelIns.Name = "labelIns";
			this.labelIns.Size = new System.Drawing.Size(403,15);
			this.labelIns.TabIndex = 140;
			this.labelIns.Text = "Do you have dental insurance?";
			// 
			// comboPosition
			// 
			this.comboPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPosition.FormattingEnabled = true;
			this.comboPosition.Location = new System.Drawing.Point(180,215);
			this.comboPosition.Name = "comboPosition";
			this.comboPosition.Size = new System.Drawing.Size(121,21);
			this.comboPosition.TabIndex = 6;
			// 
			// comboGender
			// 
			this.comboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboGender.FormattingEnabled = true;
			this.comboGender.Location = new System.Drawing.Point(180,193);
			this.comboGender.Name = "comboGender";
			this.comboGender.Size = new System.Drawing.Size(121,21);
			this.comboGender.TabIndex = 5;
			// 
			// textReferral
			// 
			this.textReferral.Location = new System.Drawing.Point(668,315);
			this.textReferral.MaxLength = 100;
			this.textReferral.Name = "textReferral";
			this.textReferral.Size = new System.Drawing.Size(268,20);
			this.textReferral.TabIndex = 14;
			// 
			// labelReferral
			// 
			this.labelReferral.Location = new System.Drawing.Point(475,318);
			this.labelReferral.Name = "labelReferral";
			this.labelReferral.Size = new System.Drawing.Size(191,17);
			this.labelReferral.TabIndex = 139;
			this.labelReferral.Text = "How did you hear about our office?";
			this.labelReferral.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.comboStudentStatus);
			this.groupBox2.Controls.Add(this.textSchool);
			this.groupBox2.Controls.Add(this.label30);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(574,203);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(362,68);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Student Status if Dependent Over 19 (for Ins)";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4,23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116,16);
			this.label1.TabIndex = 142;
			this.label1.Text = "Status";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboStudentStatus
			// 
			this.comboStudentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStudentStatus.FormattingEnabled = true;
			this.comboStudentStatus.Location = new System.Drawing.Point(122,20);
			this.comboStudentStatus.Name = "comboStudentStatus";
			this.comboStudentStatus.Size = new System.Drawing.Size(121,21);
			this.comboStudentStatus.TabIndex = 0;
			// 
			// textSchool
			// 
			this.textSchool.Location = new System.Drawing.Point(122,42);
			this.textSchool.MaxLength = 30;
			this.textSchool.Name = "textSchool";
			this.textSchool.Size = new System.Drawing.Size(226,20);
			this.textSchool.TabIndex = 1;
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(4,46);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(116,16);
			this.label30.TabIndex = 9;
			this.label30.Text = "College Name";
			this.label30.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkInsYes
			// 
			this.checkInsYes.Location = new System.Drawing.Point(180,61);
			this.checkInsYes.Name = "checkInsYes";
			this.checkInsYes.Size = new System.Drawing.Size(327,18);
			this.checkInsYes.TabIndex = 0;
			this.checkInsYes.Text = "Yes    (Please present card to receptionist before continuing)";
			this.checkInsYes.UseVisualStyleBackColor = true;
			this.checkInsYes.Click += new System.EventHandler(this.checkInsYes_Click);
			// 
			// labelAddrNotes
			// 
			this.labelAddrNotes.Location = new System.Drawing.Point(517,386);
			this.labelAddrNotes.Name = "labelAddrNotes";
			this.labelAddrNotes.Size = new System.Drawing.Size(151,44);
			this.labelAddrNotes.TabIndex = 135;
			this.labelAddrNotes.Text = "Notes regarding address or telephone";
			this.labelAddrNotes.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAddrNotes
			// 
			this.textAddrNotes.AcceptsReturn = true;
			this.textAddrNotes.Location = new System.Drawing.Point(668,386);
			this.textAddrNotes.Multiline = true;
			this.textAddrNotes.Name = "textAddrNotes";
			this.textAddrNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.PatAddressNote;
			this.textAddrNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textAddrNotes.Size = new System.Drawing.Size(268,126);
			this.textAddrNotes.TabIndex = 15;
			// 
			// textBirthdate
			// 
			this.textBirthdate.Location = new System.Drawing.Point(180,237);
			this.textBirthdate.Name = "textBirthdate";
			this.textBirthdate.Size = new System.Drawing.Size(82,20);
			this.textBirthdate.TabIndex = 7;
			// 
			// textEmail
			// 
			this.textEmail.Location = new System.Drawing.Point(180,279);
			this.textEmail.MaxLength = 100;
			this.textEmail.Name = "textEmail";
			this.textEmail.Size = new System.Drawing.Size(226,20);
			this.textEmail.TabIndex = 9;
			// 
			// textWirelessPhone
			// 
			this.textWirelessPhone.Location = new System.Drawing.Point(180,300);
			this.textWirelessPhone.MaxLength = 30;
			this.textWirelessPhone.Name = "textWirelessPhone";
			this.textWirelessPhone.Size = new System.Drawing.Size(174,20);
			this.textWirelessPhone.TabIndex = 10;
			this.textWirelessPhone.TextChanged += new System.EventHandler(this.textWirelessPhone_TextChanged);
			// 
			// textWkPhone
			// 
			this.textWkPhone.Location = new System.Drawing.Point(180,321);
			this.textWkPhone.MaxLength = 30;
			this.textWkPhone.Name = "textWkPhone";
			this.textWkPhone.Size = new System.Drawing.Size(174,20);
			this.textWkPhone.TabIndex = 11;
			this.textWkPhone.TextChanged += new System.EventHandler(this.textWkPhone_TextChanged);
			// 
			// textSSN
			// 
			this.textSSN.Location = new System.Drawing.Point(180,258);
			this.textSSN.MaxLength = 100;
			this.textSSN.Name = "textSSN";
			this.textSSN.Size = new System.Drawing.Size(82,20);
			this.textSSN.TabIndex = 8;
			this.textSSN.Validating += new System.ComponentModel.CancelEventHandler(this.textSSN_Validating);
			// 
			// textPreferred
			// 
			this.textPreferred.Location = new System.Drawing.Point(180,172);
			this.textPreferred.MaxLength = 100;
			this.textPreferred.Name = "textPreferred";
			this.textPreferred.Size = new System.Drawing.Size(228,20);
			this.textPreferred.TabIndex = 4;
			this.textPreferred.TextChanged += new System.EventHandler(this.textPreferred_TextChanged);
			// 
			// textMiddleI
			// 
			this.textMiddleI.Location = new System.Drawing.Point(180,151);
			this.textMiddleI.MaxLength = 100;
			this.textMiddleI.Name = "textMiddleI";
			this.textMiddleI.Size = new System.Drawing.Size(75,20);
			this.textMiddleI.TabIndex = 3;
			this.textMiddleI.TextChanged += new System.EventHandler(this.textMiddleI_TextChanged);
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(180,130);
			this.textFName.MaxLength = 100;
			this.textFName.Name = "textFName";
			this.textFName.Size = new System.Drawing.Size(228,20);
			this.textFName.TabIndex = 2;
			this.textFName.TextChanged += new System.EventHandler(this.textFName_TextChanged);
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(180,109);
			this.textLName.MaxLength = 100;
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(228,20);
			this.textLName.TabIndex = 1;
			this.textLName.TextChanged += new System.EventHandler(this.textLName_TextChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(72,194);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(106,16);
			this.label7.TabIndex = 104;
			this.label7.Text = "Gender";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
			this.groupBox1.Controls.Add(this.textHmPhone);
			this.groupBox1.Controls.Add(this.textZip);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.checkSame);
			this.groupBox1.Controls.Add(this.textState);
			this.groupBox1.Controls.Add(this.labelST);
			this.groupBox1.Controls.Add(this.textAddress);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.labelCity);
			this.groupBox1.Controls.Add(this.textAddress2);
			this.groupBox1.Controls.Add(this.labelZip);
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.textCity);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(75,347);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(373,185);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Address and Phone";
			// 
			// textHmPhone
			// 
			this.textHmPhone.Location = new System.Drawing.Point(105,51);
			this.textHmPhone.MaxLength = 30;
			this.textHmPhone.Name = "textHmPhone";
			this.textHmPhone.Size = new System.Drawing.Size(174,20);
			this.textHmPhone.TabIndex = 1;
			this.textHmPhone.TextChanged += new System.EventHandler(this.textHmPhone_TextChanged);
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(105,151);
			this.textZip.MaxLength = 100;
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(116,20);
			this.textZip.TabIndex = 6;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Window;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(123,19);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(230,33);
			this.textBox1.TabIndex = 57;
			this.textBox1.TabStop = false;
			this.textBox1.Text = "This info is the same for everyone in the family";
			// 
			// checkSame
			// 
			this.checkSame.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSame.Location = new System.Drawing.Point(105,16);
			this.checkSame.Name = "checkSame";
			this.checkSame.Size = new System.Drawing.Size(18,21);
			this.checkSame.TabIndex = 0;
			this.checkSame.TabStop = false;
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(105,131);
			this.textState.MaxLength = 100;
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(61,20);
			this.textState.TabIndex = 5;
			this.textState.TabStop = false;
			// 
			// labelST
			// 
			this.labelST.Location = new System.Drawing.Point(7,135);
			this.labelST.Name = "labelST";
			this.labelST.Size = new System.Drawing.Size(96,14);
			this.labelST.TabIndex = 13;
			this.labelST.Text = "ST";
			this.labelST.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(105,71);
			this.textAddress.MaxLength = 100;
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(254,20);
			this.textAddress.TabIndex = 2;
			this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(5,95);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(99,14);
			this.label12.TabIndex = 11;
			this.label12.Text = "Address2";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelCity
			// 
			this.labelCity.Location = new System.Drawing.Point(4,115);
			this.labelCity.Name = "labelCity";
			this.labelCity.Size = new System.Drawing.Size(98,14);
			this.labelCity.TabIndex = 12;
			this.labelCity.Text = "City";
			this.labelCity.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(105,91);
			this.textAddress2.MaxLength = 100;
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.Size = new System.Drawing.Size(253,20);
			this.textAddress2.TabIndex = 3;
			this.textAddress2.TextChanged += new System.EventHandler(this.textAddress2_TextChanged);
			// 
			// labelZip
			// 
			this.labelZip.Location = new System.Drawing.Point(7,156);
			this.labelZip.Name = "labelZip";
			this.labelZip.Size = new System.Drawing.Size(96,14);
			this.labelZip.TabIndex = 14;
			this.labelZip.Text = "Zip";
			this.labelZip.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(7,53);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(98,14);
			this.label16.TabIndex = 15;
			this.label16.Text = "Home Phone";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(105,111);
			this.textCity.MaxLength = 100;
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(191,20);
			this.textCity.TabIndex = 4;
			this.textCity.TabStop = false;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(5,74);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(98,14);
			this.label11.TabIndex = 10;
			this.label11.Text = "Address";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(74,284);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(104,14);
			this.label22.TabIndex = 125;
			this.label22.Text = "E-mail";
			this.label22.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(55,304);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(124,14);
			this.label18.TabIndex = 120;
			this.label18.Text = "Wireless Phone";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(61,325);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(118,14);
			this.label17.TabIndex = 119;
			this.label17.Text = "Work Phone";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelSSN
			// 
			this.labelSSN.Location = new System.Drawing.Point(35,263);
			this.labelSSN.Name = "labelSSN";
			this.labelSSN.Size = new System.Drawing.Size(142,14);
			this.labelSSN.TabIndex = 111;
			this.labelSSN.Text = "Social Security Number";
			this.labelSSN.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(73,241);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(105,14);
			this.label9.TabIndex = 109;
			this.label9.Text = "Birthdate";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(78,216);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100,16);
			this.label8.TabIndex = 106;
			this.label8.Text = "Marital Status";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(32,175);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(146,16);
			this.label5.TabIndex = 100;
			this.label5.Text = "Preferred Name (nickname)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(54,154);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(124,16);
			this.label4.TabIndex = 98;
			this.label4.Text = "Middle Initial";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(51,133);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(127,16);
			this.label3.TabIndex = 96;
			this.label3.Text = "First Name";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(53,111);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(125,16);
			this.label2.TabIndex = 95;
			this.label2.Text = "Last Name";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.listDiseases);
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Controls.Add(this.multInput);
			this.tabPage2.Location = new System.Drawing.Point(4,22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(984,687);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Medical History";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// listDiseases
			// 
			this.listDiseases.CheckOnClick = true;
			this.listDiseases.FormattingEnabled = true;
			this.listDiseases.Location = new System.Drawing.Point(3,20);
			this.listDiseases.MultiColumn = true;
			this.listDiseases.Name = "listDiseases";
			this.listDiseases.Size = new System.Drawing.Size(975,199);
			this.listDiseases.TabIndex = 0;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6,4);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(317,13);
			this.label6.TabIndex = 5;
			this.label6.Text = "Please mark any of the following medical conditions that you have";
			// 
			// multInput
			// 
			this.multInput.Location = new System.Drawing.Point(3,223);
			this.multInput.Name = "multInput";
			this.multInput.Size = new System.Drawing.Size(975,440);
			this.multInput.TabIndex = 4;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 4000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// textWelcome
			// 
			this.textWelcome.BackColor = System.Drawing.SystemColors.Control;
			this.textWelcome.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif",18F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textWelcome.Location = new System.Drawing.Point(330,237);
			this.textWelcome.Multiline = true;
			this.textWelcome.Name = "textWelcome";
			this.textWelcome.ReadOnly = true;
			this.textWelcome.Size = new System.Drawing.Size(377,289);
			this.textWelcome.TabIndex = 3;
			this.textWelcome.TabStop = false;
			this.textWelcome.Text = "Welcome!\r\n\r\nThis  terminal is used to enter new patient information.\r\n\r\nThe recep" +
    "tionist will prepare the screen for you.";
			this.textWelcome.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelConnection
			// 
			this.labelConnection.Font = new System.Drawing.Font("Microsoft Sans Serif",18F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelConnection.Location = new System.Drawing.Point(51,728);
			this.labelConnection.Name = "labelConnection";
			this.labelConnection.Size = new System.Drawing.Size(444,29);
			this.labelConnection.TabIndex = 4;
			this.labelConnection.Text = "Connection to server has been lost";
			// 
			// butSubmit
			// 
			this.butSubmit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSubmit.Autosize = true;
			this.butSubmit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSubmit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSubmit.Location = new System.Drawing.Point(929,731);
			this.butSubmit.Name = "butSubmit";
			this.butSubmit.Size = new System.Drawing.Size(75,26);
			this.butSubmit.TabIndex = 1;
			this.butSubmit.Text = "Submit";
			this.butSubmit.Click += new System.EventHandler(this.butSubmit_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.Location = new System.Drawing.Point(501,731);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormTerminal
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(1016,760);
			this.ControlBox = false;
			this.Controls.Add(this.labelConnection);
			this.Controls.Add(this.tabMain);
			this.Controls.Add(this.butSubmit);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.textWelcome);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTerminal";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.FormTerminal_Load);
			this.tabMain.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormTerminal_Load(object sender,EventArgs e) {
			multInput.IsQuestionnaire=true;
			labelConnection.Visible=false;
			TerminalActives.DeleteAllForComputer(Environment.MachineName);
			TerminalActive terminal=new TerminalActive();
			terminal.ComputerName=Environment.MachineName;
			terminal.TerminalStatus=TerminalStatusEnum.Standby;
			TerminalActives.Insert(terminal);
			tabMain.Visible=false;
			butSubmit.Visible=false;
			TerminalStatus=TerminalStatusEnum.Standby;
		}

		///<summary>Occurs every 4 seconds. Checks database for status changes.</summary>
		private void timer1_Tick(object sender,EventArgs e) {
			TerminalActive terminal;
			try{
				terminal=TerminalActives.GetTerminal(Environment.MachineName);
				labelConnection.Visible=false;
			}
			catch{//SocketException if db connection gets lost.
				labelConnection.Visible=true;
				return;
			}
			if(terminal==null){
				return;
			}
			if(terminal.TerminalStatus==TerminalStatus){
				return;
			}
			//someone changed the status from the terminal manager.
			TerminalStatus=terminal.TerminalStatus;
			//set tab visibility
			tabMain.TabPages.Clear();
			if(TerminalStatus==TerminalStatusEnum.UpdateOnly){
				tabMain.TabPages.Add(this.tabPage1);
			}
			else if(TerminalStatus==TerminalStatusEnum.Standby) {
				//
			}
			else if(TerminalStatus==TerminalStatusEnum.PatientInfo) {
				tabMain.TabPages.Add(this.tabPage1);
			}
			else if(TerminalStatus==TerminalStatusEnum.Medical) {
				tabMain.TabPages.Add(this.tabPage2);
			}
			if(TerminalStatus==TerminalStatusEnum.Standby){//force move to standby (rare)
				textWelcome.Visible=true;
				tabMain.Visible=false;
				butClose.Visible=true;
				butSubmit.Visible=false;
				return;
			}
			//all the other three types show the tabMain
			textWelcome.Visible=false;
			tabMain.Visible=true;
			butClose.Visible=false;
			butSubmit.Visible=true;
			FamCur=Patients.GetFamily(terminal.PatNum);
			PatCur=FamCur.GetPatient(terminal.PatNum);
			FillForm();
			/*IsChangingTab=true;
			//this is very common.  This is the typical signal to load up a patient.
			if(TerminalStatus==TerminalStatusEnum.PatientInfo){
				tabMain.SelectedIndex=0;
			}
			//This is the typical signal to load up a patient who needs to change their existing info.
			else if(TerminalStatus==TerminalStatusEnum.UpdateOnly) {
				tabMain.SelectedIndex=0;
			}
			//receptionist forces move to medical tab. Rare.
			else if(TerminalStatus==TerminalStatusEnum.Medical) {
				tabMain.SelectedIndex=1;
			}
			//receptionist forces move to questions tab. Rare.
			//else if(TerminalStatus==TerminalStatusEnum.Questions) {
			//	tabMain.SelectedIndex=2;
			//}
			IsChangingTab=false;*/
		}

		///<summary></summary>
		private void FillForm(){
			FillPatientInfo();
			FillDiseases();
			FillQuestions();
		}

		///<summary>Fills the first tab page with info.</summary>
		private void FillPatientInfo(){
			if(TerminalStatus==TerminalStatusEnum.UpdateOnly){
				labelIns.Visible=false;
				checkInsNo.Visible=false;
				checkInsYes.Visible=false;
				labelReferral.Visible=false;
				textReferral.Visible=false;
				labelAddrNotes.Visible=false;
				textAddrNotes.Visible=false;
			}
			else{
				labelIns.Visible=true;
				checkInsNo.Visible=true;
				checkInsYes.Visible=true;
				labelReferral.Visible=true;
				textReferral.Visible=true;
				labelAddrNotes.Visible=true;
				textAddrNotes.Visible=true;
			}
			checkSame.Checked=true;
			for(int i=0;i<FamCur.List.Length;i++) {
				if(PatCur.HmPhone!=FamCur.List[i].HmPhone
					|| PatCur.Address!=FamCur.List[i].Address
					|| PatCur.Address2!=FamCur.List[i].Address2
					|| PatCur.City!=FamCur.List[i].City
					|| PatCur.State!=FamCur.List[i].State
					|| PatCur.Zip!=FamCur.List[i].Zip)
				{
					checkSame.Checked=false;
				}
			}
			checkInsNo.Checked=false;
			checkInsYes.Checked=false;
			textLName.Text=PatCur.LName;
			textFName.Text=PatCur.FName;
			textMiddleI.Text=PatCur.MiddleI;
			textPreferred.Text=PatCur.Preferred;
			comboGender.Items.Clear();
			comboGender.Items.Add(Lan.g("enumPatientGender","Male"));
			comboGender.Items.Add(Lan.g("enumPatientGender","Female"));
			comboPosition.Items.Clear();
			comboPosition.Items.Add(Lan.g("enumPatientPosition","Single"));
			comboPosition.Items.Add(Lan.g("enumPatientPosition","Married"));
			comboPosition.Items.Add(Lan.g("enumPatientPosition","Child"));
			comboPosition.Items.Add(Lan.g("enumPatientPosition","Widowed"));
			comboPosition.Items.Add(Lan.g("enumPatientPosition","Divorced"));
			switch(PatCur.Gender) {
				case PatientGender.Male: comboGender.SelectedIndex=0; break;
				case PatientGender.Female: comboGender.SelectedIndex=1; break;
				case PatientGender.Unknown: comboGender.SelectedIndex=0; break;
			}
			switch(PatCur.Position) {
				case PatientPosition.Single: comboPosition.SelectedIndex=0; break;
				case PatientPosition.Married: comboPosition.SelectedIndex=1; break;
				case PatientPosition.Child: comboPosition.SelectedIndex=2; break;
				case PatientPosition.Widowed: comboPosition.SelectedIndex=3; break;
				case PatientPosition.Divorced: comboPosition.SelectedIndex=4; break;
			}
			if(PatCur.Birthdate.Year < 1880)
				textBirthdate.Text="";
			else
				textBirthdate.Text=PatCur.Birthdate.ToShortDateString();
			if(CultureInfo.CurrentCulture.Name=="en-US"//if USA
				&& PatCur.SSN!=null//the null catches new patients
				&& PatCur.SSN.Length==9)//and length exactly 9 (no data gets lost in formatting)
			{
				textSSN.Text=PatCur.SSN.Substring(0,3)+"-"
					+PatCur.SSN.Substring(3,2)+"-"+PatCur.SSN.Substring(5,4);
			}
			else
				textSSN.Text=PatCur.SSN;
			textAddress.Text=PatCur.Address;
			textAddress2.Text=PatCur.Address2;
			textCity.Text=PatCur.City;
			textState.Text=PatCur.State;
			textZip.Text=PatCur.Zip;
			textHmPhone.Text=PatCur.HmPhone;
			textWkPhone.Text=PatCur.WkPhone;
			textWirelessPhone.Text=PatCur.WirelessPhone;
			textEmail.Text=PatCur.Email;
			comboStudentStatus.Items.Clear();
			comboStudentStatus.Items.Add(Lan.g(this,"Nonstudent"));
			comboStudentStatus.Items.Add(Lan.g(this,"Parttime"));
			comboStudentStatus.Items.Add(Lan.g(this,"Fulltime"));
			switch(PatCur.StudentStatus) {
				case "N"://non
				case "": 
					comboStudentStatus.SelectedIndex=0; break;
				case "P"://parttime
					comboStudentStatus.SelectedIndex=1; break;
				case "F"://fulltime
					comboStudentStatus.SelectedIndex=2; break;
			}
			textSchool.Text=PatCur.SchoolName;
			textReferral.Text="";
			textAddrNotes.Text="";//because only used for new patients, and we never want patient to see old note.
			textMiddleI.Select();//because the first and last names are probably already entered correctly
		}

		private void FillDiseases(){
			//this never gets filled with existing patient info.  Only blank list.
			listDiseases.Items.Clear();
			for(int i=0;i<DiseaseDefs.List.Length;i++){
				listDiseases.Items.Add(DiseaseDefs.List[i].DiseaseName);
			}
		}

		private void FillQuestions(){
			QuestionDefList=QuestionDefs.Refresh();
			multInput.ClearInputItems();
			for(int i=0;i<QuestionDefList.Length;i++) {
				if(QuestionDefList[i].QuestType==QuestionType.FreeformText) {
					multInput.AddInputItem(QuestionDefList[i].Description,FieldValueType.String,"");
				}
				else if(QuestionDefList[i].QuestType==QuestionType.YesNoUnknown) {
					multInput.AddInputItem(QuestionDefList[i].Description,FieldValueType.YesNoUnknown,YN.Unknown);
				}
			}
		}

		private void checkInsYes_Click(object sender,EventArgs e) {
			if(checkInsYes.Checked){
				checkInsNo.Checked=false;
			}
		}

		private void checkInsNo_Click(object sender,EventArgs e) {
			if(checkInsNo.Checked) {
				checkInsYes.Checked=false;
			}
		}

		private void textLName_TextChanged(object sender,System.EventArgs e) {
			if(textLName.Text.Length==1) {
				textLName.Text=textLName.Text.ToUpper();
				textLName.SelectionStart=1;
			}
		}

		private void textFName_TextChanged(object sender,System.EventArgs e) {
			if(textFName.Text.Length==1) {
				textFName.Text=textFName.Text.ToUpper();
				textFName.SelectionStart=1;
			}
		}

		private void textMiddleI_TextChanged(object sender,System.EventArgs e) {
			if(textMiddleI.Text.Length==1) {
				textMiddleI.Text=textMiddleI.Text.ToUpper();
				textMiddleI.SelectionStart=1;
			}
		}

		private void textPreferred_TextChanged(object sender,System.EventArgs e) {
			if(textPreferred.Text.Length==1) {
				textPreferred.Text=textPreferred.Text.ToUpper();
				textPreferred.SelectionStart=1;
			}
		}

		private void textAddress_TextChanged(object sender,System.EventArgs e) {
			if(textAddress.Text.Length==1) {
				textAddress.Text=textAddress.Text.ToUpper();
				textAddress.SelectionStart=1;
			}
		}

		private void textAddress2_TextChanged(object sender,System.EventArgs e) {
			if(textAddress2.Text.Length==1) {
				textAddress2.Text=textAddress2.Text.ToUpper();
				textAddress2.SelectionStart=1;
			}
		}

		private void textSSN_Validating(object sender,System.ComponentModel.CancelEventArgs e) {
			if(CultureInfo.CurrentCulture.Name!="en-US") {
				return;
			}
			//only reformats if in USA and exactly 9 digits.
			if(textSSN.Text=="") {
				return;
			}
			if(textSSN.Text.Length==9) {//if just numbers, try to reformat.
				bool SSNisValid=true;
				for(int i=0;i<textSSN.Text.Length;i++) {
					if(!Char.IsNumber(textSSN.Text,i)) {
						SSNisValid=false;
					}
				}
				if(SSNisValid) {
					textSSN.Text=textSSN.Text.Substring(0,3)+"-"
						+textSSN.Text.Substring(3,2)+"-"+textSSN.Text.Substring(5,4);
				}
			}
			if(!Regex.IsMatch(textSSN.Text,@"^\d\d\d-\d\d-\d\d\d\d$")) {
				if(MessageBox.Show("SSN not valid. Continue anyway?","",MessageBoxButtons.OKCancel)
					!=DialogResult.OK) {
					e.Cancel=true;
				}
			}
		}

		private void textWirelessPhone_TextChanged(object sender,System.EventArgs e) {
			int cursor=textWirelessPhone.SelectionStart;
			int length=textWirelessPhone.Text.Length;
			textWirelessPhone.Text=TelephoneNumbers.AutoFormat(textWirelessPhone.Text);
			if(textWirelessPhone.Text.Length>length)
				cursor++;
			textWirelessPhone.SelectionStart=cursor;
		}

		private void textWkPhone_TextChanged(object sender,System.EventArgs e) {
			int cursor=textWkPhone.SelectionStart;
			int length=textWkPhone.Text.Length;
			textWkPhone.Text=TelephoneNumbers.AutoFormat(textWkPhone.Text);
			if(textWkPhone.Text.Length>length)
				cursor++;
			textWkPhone.SelectionStart=cursor;
		}

		private void textHmPhone_TextChanged(object sender,System.EventArgs e) {
			int cursor=textHmPhone.SelectionStart;
			int length=textHmPhone.Text.Length;
			textHmPhone.Text=TelephoneNumbers.AutoFormat(textHmPhone.Text);
			if(textHmPhone.Text.Length>length)
				cursor++;
			textHmPhone.SelectionStart=cursor;
		}

		private void textCity_TextChanged(object sender,System.EventArgs e) {
			if(textCity.Text.Length==1) {
				textCity.Text=textCity.Text.ToUpper();
				textCity.SelectionStart=1;
			}
		}

		private void textState_TextChanged(object sender,System.EventArgs e) {
			if(CultureInfo.CurrentCulture.Name=="en-US" //if USA or Canada, capitalize first 2 letters
				|| CultureInfo.CurrentCulture.Name.Substring(3)=="CA") {
				if(textState.Text.Length==1 || textState.Text.Length==2) {
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=2;
				}
			}
			else {
				if(textState.Text.Length==1) {
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=1;
				}
			}
		}

		private void butSubmit_Click(object sender,EventArgs e) {
			TerminalActive terminal=TerminalActives.GetTerminal(Environment.MachineName);
			if(TerminalStatus==TerminalStatusEnum.PatientInfo || TerminalStatus==TerminalStatusEnum.UpdateOnly){
				try{
					SavePtInfo();
				}
				catch(ApplicationException ex){
					MessageBox.Show(ex.Message);
					return;
				}
			}
			if(TerminalStatus==TerminalStatusEnum.PatientInfo){
				tabMain.TabPages.Clear();
				tabMain.TabPages.Add(this.tabPage2);
				//IsChangingTab=true;
				//tabMain.SelectedIndex=1;
				//IsChangingTab=false;
				TerminalStatus=TerminalStatusEnum.Medical;
				terminal.TerminalStatus=TerminalStatusEnum.Medical;
				TerminalActives.Update(terminal);
			}
			else if(TerminalStatus==TerminalStatusEnum.UpdateOnly){
				textWelcome.Visible=true;
				tabMain.Visible=false;
				butClose.Visible=true;
				butSubmit.Visible=false;
				TerminalStatus=TerminalStatusEnum.Standby;
				terminal.TerminalStatus=TerminalStatusEnum.Standby;
				terminal.PatNum=0;
				TerminalActives.Update(terminal);
			}
			else if(TerminalStatus==TerminalStatusEnum.Medical){
				SaveDiseases();
				SaveQuestions();
				textWelcome.Visible=true;
				tabMain.Visible=false;
				butClose.Visible=true;
				butSubmit.Visible=false;
				TerminalStatus=TerminalStatusEnum.Standby;
				terminal.TerminalStatus=TerminalStatusEnum.Standby;
				terminal.PatNum=0;
				TerminalActives.Update(terminal);
			}
		}

		///<summary>Throws an exception if error on the page.</summary>
		private void SavePtInfo(){
			if(TerminalStatus!=TerminalStatusEnum.UpdateOnly && !checkInsYes.Checked && !checkInsNo.Checked) {
				throw new ApplicationException(Lan.g(this,"Please indicate whether you have insurance."));
			}
			if(textLName.Text=="") {
				throw new ApplicationException(Lan.g(this,"Last Name must be entered."));
			}
			if(textFName.Text=="") {
				throw new ApplicationException(Lan.g(this,"First Name must be entered."));
			}
			if(textBirthdate.errorProvider1.GetError(textBirthdate)!=""){
				throw new ApplicationException(Lan.g(this,"Please fix data entry errors first."));
			}
			if(textBirthdate.Text==""){
				throw new ApplicationException(Lan.g(this,"Birthdate must be entered."));
			}
			if(TerminalStatus!=TerminalStatusEnum.UpdateOnly && textReferral.Text=="") {
				textReferral.BackColor=Color.Yellow;
				throw new ApplicationException(Lan.g(this,"Referral must be entered (on the right)."));
			}
			Patient PatOld=PatCur.Copy();
			PatCur.LName=textLName.Text;
			PatCur.FName=textFName.Text;
			PatCur.MiddleI=textMiddleI.Text;
			PatCur.Preferred=textPreferred.Text;
			switch(comboGender.SelectedIndex) {
				case 0: PatCur.Gender=PatientGender.Male; break;
				case 1: PatCur.Gender=PatientGender.Female; break;
			}
			switch(comboPosition.SelectedIndex) {
				case 0: PatCur.Position=PatientPosition.Single; break;
				case 1: PatCur.Position=PatientPosition.Married; break;
				case 2: PatCur.Position=PatientPosition.Child; break;
				case 3: PatCur.Position=PatientPosition.Widowed; break;
				case 4: PatCur.Position=PatientPosition.Divorced; break;
			}
			PatCur.Birthdate=PIn.PDate(textBirthdate.Text);
			if(CultureInfo.CurrentCulture.Name=="en-US") {
				if(Regex.IsMatch(textSSN.Text,@"^\d\d\d-\d\d-\d\d\d\d$")) {
					PatCur.SSN=textSSN.Text.Substring(0,3)+textSSN.Text.Substring(4,2)
						+textSSN.Text.Substring(7,4);
				}
				else {
					PatCur.SSN=textSSN.Text;
				}
			}
			else {//other cultures
				PatCur.SSN=textSSN.Text;
			}
			PatCur.WkPhone=textWkPhone.Text;
			PatCur.WirelessPhone=textWirelessPhone.Text;
			PatCur.Email=textEmail.Text;
			switch(comboStudentStatus.SelectedIndex) {
				case 0: PatCur.StudentStatus="N"; break;
				case 1: PatCur.StudentStatus="P"; break;
				case 2: PatCur.StudentStatus="F"; break;
			}
			PatCur.SchoolName=textSchool.Text;
			//address:
			PatCur.HmPhone=textHmPhone.Text;
			PatCur.Address=textAddress.Text;
			PatCur.Address2=textAddress2.Text;
			PatCur.City=textCity.Text;
			PatCur.State=textState.Text;
			PatCur.Zip=textZip.Text;
			if(TerminalStatus!=TerminalStatusEnum.UpdateOnly){
				if(checkInsYes.Checked){
					PatCur.AddrNote=Lan.g(this,"Insurance: Yes")+"\r\n"+PatCur.AddrNote;
				}
				else if(checkInsNo.Checked) {
					PatCur.AddrNote=Lan.g(this,"Insurance: No")+"\r\n"+PatCur.AddrNote;
				}
				if(textReferral.Text!=""){
					PatCur.AddrNote=Lan.g(this,"Referral: ")+textReferral.Text+"\r\n"+PatCur.AddrNote;
				}
				if(textAddrNotes.Text!=""){
					PatCur.AddrNote=textAddrNotes.Text+"\r\n"+PatCur.AddrNote;
				}
			}
			Patients.Update(PatCur,PatOld);
			if(checkSame.Checked) {
				Patients.UpdateAddressForFamTerminal(PatCur);
			}
			//If this patient is also a referral source,
			//keep address info synched:
			for(int i=0;i<Referrals.List.Length;i++) {
				if(Referrals.List[i].PatNum==PatCur.PatNum) {
					//Referrals.Cur=Referrals.List[i];
					Referrals.List[i].LName=PatCur.LName;
					Referrals.List[i].FName=PatCur.FName;
					Referrals.List[i].MName=PatCur.MiddleI;
					Referrals.List[i].Address=PatCur.Address;
					Referrals.List[i].Address2=PatCur.Address2;
					Referrals.List[i].City=PatCur.City;
					Referrals.List[i].ST=PatCur.State;
					Referrals.List[i].SSN=PatCur.SSN;
					Referrals.List[i].Zip=PatCur.Zip;
					Referrals.List[i].Telephone=TelephoneNumbers.FormatNumbersExactTen(PatCur.HmPhone);
					Referrals.List[i].EMail=PatCur.Email;
					Referrals.Update(Referrals.List[i]);
					Referrals.Refresh();
					break;
				}
			}
		}

		private void SaveDiseases(){
			Disease disease;
			for(int i=0;i<listDiseases.CheckedIndices.Count;i++){
				disease=new Disease();
				disease.PatNum=PatCur.PatNum;
				disease.DiseaseDefNum=DiseaseDefs.List[listDiseases.CheckedIndices[i]].DiseaseDefNum;
				Diseases.Insert(disease);
			}
		}

		private void SaveQuestions(){
			Question quest;
			ArrayList ALval;
			for(int i=0;i<QuestionDefList.Length;i++) {
				quest=new Question();
				quest.PatNum=PatCur.PatNum;
				quest.ItemOrder=QuestionDefList[i].ItemOrder;
				quest.Description=QuestionDefList[i].Description;
				if(QuestionDefList[i].QuestType==QuestionType.FreeformText) {
					ALval=multInput.GetCurrentValues(i);
					if(ALval.Count>0) {
						quest.Answer=ALval[0].ToString();
					}
					//else it will just be blank
				}
				else if(QuestionDefList[i].QuestType==QuestionType.YesNoUnknown) {
					quest.Answer=Lan.g("enumYN",multInput.GetCurrentValues(i)[0].ToString());
				}
				Questions.Insert(quest);
			}
		}

		private void butClose_Click(object sender,EventArgs e) {
			InputBox input=new InputBox("Password");
			input.ShowDialog();
			if(input.DialogResult!=DialogResult.OK){
				return;
			}
			if(input.textResult.Text!=PrefB.GetString("TerminalClosePassword")){
				MsgBox.Show(this,"Invalid password.");
				return;
			}
			TerminalActives.DeleteAllForComputer(Environment.MachineName);
			Close();
		}

		

		

		

		

		


		


	}
}





















