using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormApptEditNew : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.ODGrid gridPatient;
		private OpenDental.UI.ODGrid gridComm;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private ComboBox comboConfirmed;
		private ComboBox comboUnschedStatus;
		private Label label4;
		private ComboBox comboStatus;
		private Label label5;
		private Label labelStatus;
		private OpenDental.UI.Button butAudit;
		private OpenDental.UI.Button butTask;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butPin;
		private Label label24;
		private CheckBox checkIsHygiene;
		private ComboBox comboClinic;
		private Label labelClinic;
		private ComboBox comboLab;
		private ComboBox comboAssistant;
		private ComboBox comboProvHyg;
		private ComboBox comboProvNum;
		private Label label13;
		private Label label12;
		private CheckBox checkIsNewPatient;
		private Label label3;
		private Label label2;
		private OpenDental.UI.ODGrid gridProc;
		private Button butSlider;
		private TableTimeBar tbTime;
		private Label label6;
		private ValidNum textAddTime;
		private TextBox textTime;
		private OpenDental.UI.Button butCalcTime;
		private Label label1;
		private ODtextBox textNote;
		private Label label7;
		private OpenDental.UI.Button butAddComm;
		private Label label8;
		private ListBox listQuickAdd;
		private OpenDental.UI.Button button1;
		private Panel activePanel;

		///<summary></summary>
		public FormApptEditNew()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormApptEditNew));
			this.comboConfirmed = new System.Windows.Forms.ComboBox();
			this.comboUnschedStatus = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboStatus = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.labelStatus = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.checkIsHygiene = new System.Windows.Forms.CheckBox();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.comboLab = new System.Windows.Forms.ComboBox();
			this.comboAssistant = new System.Windows.Forms.ComboBox();
			this.comboProvHyg = new System.Windows.Forms.ComboBox();
			this.comboProvNum = new System.Windows.Forms.ComboBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.checkIsNewPatient = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.listQuickAdd = new System.Windows.Forms.ListBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textTime = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butSlider = new System.Windows.Forms.Button();
			this.button1 = new OpenDental.UI.Button();
			this.textAddTime = new OpenDental.ValidNum();
			this.butCalcTime = new OpenDental.UI.Button();
			this.butAddComm = new OpenDental.UI.Button();
			this.textNote = new OpenDental.ODtextBox();
			this.tbTime = new OpenDental.TableTimeBar();
			this.gridPatient = new OpenDental.UI.ODGrid();
			this.gridComm = new OpenDental.UI.ODGrid();
			this.gridProc = new OpenDental.UI.ODGrid();
			this.butAudit = new OpenDental.UI.Button();
			this.butTask = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butPin = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// comboConfirmed
			// 
			this.comboConfirmed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboConfirmed.Location = new System.Drawing.Point(207,42);
			this.comboConfirmed.MaxDropDownItems = 30;
			this.comboConfirmed.Name = "comboConfirmed";
			this.comboConfirmed.Size = new System.Drawing.Size(126,21);
			this.comboConfirmed.TabIndex = 84;
			// 
			// comboUnschedStatus
			// 
			this.comboUnschedStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboUnschedStatus.Location = new System.Drawing.Point(207,21);
			this.comboUnschedStatus.MaxDropDownItems = 100;
			this.comboUnschedStatus.Name = "comboUnschedStatus";
			this.comboUnschedStatus.Size = new System.Drawing.Size(126,21);
			this.comboUnschedStatus.TabIndex = 83;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(94,24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111,15);
			this.label4.TabIndex = 82;
			this.label4.Text = "Unscheduled Status";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboStatus
			// 
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Location = new System.Drawing.Point(207,0);
			this.comboStatus.MaxDropDownItems = 10;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(126,21);
			this.comboStatus.TabIndex = 81;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(94,44);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(111,16);
			this.label5.TabIndex = 80;
			this.label5.Text = "Confirmed";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelStatus
			// 
			this.labelStatus.Location = new System.Drawing.Point(94,3);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(111,15);
			this.labelStatus.TabIndex = 79;
			this.labelStatus.Text = "Status";
			this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(221,144);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(113,16);
			this.label24.TabIndex = 138;
			this.label24.Text = "(use hyg color)";
			// 
			// checkIsHygiene
			// 
			this.checkIsHygiene.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsHygiene.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsHygiene.Location = new System.Drawing.Point(116,144);
			this.checkIsHygiene.Name = "checkIsHygiene";
			this.checkIsHygiene.Size = new System.Drawing.Size(104,16);
			this.checkIsHygiene.TabIndex = 137;
			this.checkIsHygiene.Text = "Is Hygiene";
			this.checkIsHygiene.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(207,80);
			this.comboClinic.MaxDropDownItems = 100;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(126,21);
			this.comboClinic.TabIndex = 136;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(106,83);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(98,16);
			this.labelClinic.TabIndex = 135;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboLab
			// 
			this.comboLab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboLab.Location = new System.Drawing.Point(207,181);
			this.comboLab.MaxDropDownItems = 30;
			this.comboLab.Name = "comboLab";
			this.comboLab.Size = new System.Drawing.Size(126,21);
			this.comboLab.TabIndex = 134;
			// 
			// comboAssistant
			// 
			this.comboAssistant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAssistant.Location = new System.Drawing.Point(207,160);
			this.comboAssistant.MaxDropDownItems = 30;
			this.comboAssistant.Name = "comboAssistant";
			this.comboAssistant.Size = new System.Drawing.Size(126,21);
			this.comboAssistant.TabIndex = 133;
			// 
			// comboProvHyg
			// 
			this.comboProvHyg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvHyg.Location = new System.Drawing.Point(207,122);
			this.comboProvHyg.MaxDropDownItems = 30;
			this.comboProvHyg.Name = "comboProvHyg";
			this.comboProvHyg.Size = new System.Drawing.Size(126,21);
			this.comboProvHyg.TabIndex = 132;
			// 
			// comboProvNum
			// 
			this.comboProvNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvNum.Location = new System.Drawing.Point(207,101);
			this.comboProvNum.MaxDropDownItems = 100;
			this.comboProvNum.Name = "comboProvNum";
			this.comboProvNum.Size = new System.Drawing.Size(126,21);
			this.comboProvNum.TabIndex = 131;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(106,184);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(98,16);
			this.label13.TabIndex = 130;
			this.label13.Text = "Lab Case";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(106,163);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(98,16);
			this.label12.TabIndex = 129;
			this.label12.Text = "Assistant";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIsNewPatient
			// 
			this.checkIsNewPatient.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsNewPatient.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsNewPatient.Location = new System.Drawing.Point(110,63);
			this.checkIsNewPatient.Name = "checkIsNewPatient";
			this.checkIsNewPatient.Size = new System.Drawing.Size(110,17);
			this.checkIsNewPatient.TabIndex = 128;
			this.checkIsNewPatient.Text = "New Patient";
			this.checkIsNewPatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(108,124);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98,16);
			this.label3.TabIndex = 127;
			this.label3.Text = "Hygienist";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(107,104);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98,16);
			this.label2.TabIndex = 126;
			this.label2.Text = "Dentist";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(25,201);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(197,16);
			this.label7.TabIndex = 141;
			this.label7.Text = "Appointment Note";
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(336,1);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(143,39);
			this.label8.TabIndex = 145;
			this.label8.Text = "Single click on items in this list to add them to the treatment plan.";
			// 
			// listQuickAdd
			// 
			this.listQuickAdd.IntegralHeight = false;
			this.listQuickAdd.Location = new System.Drawing.Point(338,42);
			this.listQuickAdd.Name = "listQuickAdd";
			this.listQuickAdd.Size = new System.Drawing.Size(146,281);
			this.listQuickAdd.TabIndex = 144;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(0,1);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(75,14);
			this.label6.TabIndex = 65;
			this.label6.Text = "Time Length";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textTime
			// 
			this.textTime.BackColor = System.Drawing.Color.White;
			this.textTime.Location = new System.Drawing.Point(3,18);
			this.textTime.Name = "textTime";
			this.textTime.ReadOnly = true;
			this.textTime.Size = new System.Drawing.Size(66,20);
			this.textTime.TabIndex = 62;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(1,41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75,14);
			this.label1.TabIndex = 64;
			this.label1.Text = "Adj Time";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butSlider
			// 
			this.butSlider.BackColor = System.Drawing.SystemColors.ControlDark;
			this.butSlider.Location = new System.Drawing.Point(6,168);
			this.butSlider.Name = "butSlider";
			this.butSlider.Size = new System.Drawing.Size(12,15);
			this.butSlider.TabIndex = 60;
			this.butSlider.UseVisualStyleBackColor = false;
			// 
			// button1
			// 
			this.button1.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Autosize = true;
			this.button1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button1.CornerRadius = 4F;
			this.button1.Location = new System.Drawing.Point(350,324);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(123,24);
			this.button1.TabIndex = 146;
			this.button1.Text = "More Procedures";
			// 
			// textAddTime
			// 
			this.textAddTime.Location = new System.Drawing.Point(3,56);
			this.textAddTime.MaxVal = 255;
			this.textAddTime.MinVal = 0;
			this.textAddTime.Name = "textAddTime";
			this.textAddTime.Size = new System.Drawing.Size(65,20);
			this.textAddTime.TabIndex = 61;
			// 
			// butCalcTime
			// 
			this.butCalcTime.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCalcTime.Autosize = true;
			this.butCalcTime.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCalcTime.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCalcTime.CornerRadius = 4F;
			this.butCalcTime.Location = new System.Drawing.Point(22,85);
			this.butCalcTime.Name = "butCalcTime";
			this.butCalcTime.Size = new System.Drawing.Size(46,20);
			this.butCalcTime.TabIndex = 63;
			this.butCalcTime.Text = "Calc";
			// 
			// butAddComm
			// 
			this.butAddComm.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddComm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddComm.Autosize = true;
			this.butAddComm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddComm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddComm.CornerRadius = 4F;
			this.butAddComm.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddComm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddComm.Location = new System.Drawing.Point(870,350);
			this.butAddComm.Name = "butAddComm";
			this.butAddComm.Size = new System.Drawing.Size(92,26);
			this.butAddComm.TabIndex = 143;
			this.butAddComm.Text = "Co&mm";
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(24,219);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Appointment;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(309,104);
			this.textNote.TabIndex = 142;
			// 
			// tbTime
			// 
			this.tbTime.BackColor = System.Drawing.SystemColors.Window;
			this.tbTime.Location = new System.Drawing.Point(4,84);
			this.tbTime.Name = "tbTime";
			this.tbTime.ScrollValue = 150;
			this.tbTime.SelectedIndices = new int[0];
			this.tbTime.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbTime.Size = new System.Drawing.Size(15,561);
			this.tbTime.TabIndex = 59;
			// 
			// gridPatient
			// 
			this.gridPatient.HScrollVisible = false;
			this.gridPatient.Location = new System.Drawing.Point(23,350);
			this.gridPatient.Name = "gridPatient";
			this.gridPatient.ScrollValue = 0;
			this.gridPatient.Size = new System.Drawing.Size(400,345);
			this.gridPatient.TabIndex = 0;
			this.gridPatient.Title = "Patient Info";
			this.gridPatient.TranslationName = "TableApptPtInfo";
			this.gridPatient.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridPatient_MouseMove);
			// 
			// gridComm
			// 
			this.gridComm.HScrollVisible = false;
			this.gridComm.Location = new System.Drawing.Point(425,350);
			this.gridComm.Name = "gridComm";
			this.gridComm.ScrollValue = 0;
			this.gridComm.Size = new System.Drawing.Size(439,345);
			this.gridComm.TabIndex = 1;
			this.gridComm.Title = "Communications Log - Appointment Scheduling";
			this.gridComm.TranslationName = "TableCommLog";
			this.gridComm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridComm_MouseMove);
			// 
			// gridProc
			// 
			this.gridProc.HScrollVisible = false;
			this.gridProc.Location = new System.Drawing.Point(485,3);
			this.gridProc.Name = "gridProc";
			this.gridProc.ScrollValue = 0;
			this.gridProc.Size = new System.Drawing.Size(488,345);
			this.gridProc.TabIndex = 139;
			this.gridProc.Title = "Procedures - highlight to attach";
			this.gridProc.TranslationName = "TableApptProcs";
			// 
			// butAudit
			// 
			this.butAudit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAudit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAudit.Autosize = true;
			this.butAudit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAudit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAudit.CornerRadius = 4F;
			this.butAudit.Location = new System.Drawing.Point(870,498);
			this.butAudit.Name = "butAudit";
			this.butAudit.Size = new System.Drawing.Size(92,26);
			this.butAudit.TabIndex = 125;
			this.butAudit.Text = "Audit Trail";
			// 
			// butTask
			// 
			this.butTask.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butTask.Autosize = true;
			this.butTask.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTask.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTask.CornerRadius = 4F;
			this.butTask.Location = new System.Drawing.Point(870,530);
			this.butTask.Name = "butTask";
			this.butTask.Size = new System.Drawing.Size(92,26);
			this.butTask.TabIndex = 124;
			this.butTask.Text = "To Task List";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(870,594);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(92,26);
			this.butDelete.TabIndex = 123;
			this.butDelete.Text = "&Delete";
			// 
			// butPin
			// 
			this.butPin.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPin.Autosize = true;
			this.butPin.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPin.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPin.CornerRadius = 4F;
			this.butPin.Image = ((System.Drawing.Image)(resources.GetObject("butPin.Image")));
			this.butPin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPin.Location = new System.Drawing.Point(870,562);
			this.butPin.Name = "butPin";
			this.butPin.Size = new System.Drawing.Size(92,26);
			this.butPin.TabIndex = 122;
			this.butPin.Text = "&Pinboard";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(870,626);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(92,26);
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
			this.butCancel.Location = new System.Drawing.Point(870,658);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(92,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormApptEditNew
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(974,695);
			this.Controls.Add(this.comboLab);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textAddTime);
			this.Controls.Add(this.textTime);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.butCalcTime);
			this.Controls.Add(this.listQuickAdd);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butAddComm);
			this.Controls.Add(this.butSlider);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.tbTime);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.gridPatient);
			this.Controls.Add(this.gridComm);
			this.Controls.Add(this.gridProc);
			this.Controls.Add(this.label24);
			this.Controls.Add(this.checkIsHygiene);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.comboAssistant);
			this.Controls.Add(this.comboProvHyg);
			this.Controls.Add(this.comboProvNum);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.checkIsNewPatient);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butAudit);
			this.Controls.Add(this.butTask);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butPin);
			this.Controls.Add(this.comboConfirmed);
			this.Controls.Add(this.comboUnschedStatus);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboStatus);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.labelStatus);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptEditNew";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void gridComm_MouseMove(object sender,MouseEventArgs e) {
			if(gridPatient.Width==200){
				return;//already resized
			}
			int commright=gridComm.Right;
			this.SuspendLayout();
			gridPatient.Width=200;
			gridComm.Location=new Point(gridPatient.Right+2,gridComm.Top);
			gridComm.Width=commright-gridComm.Left;
			this.ResumeLayout();
		}

		private void gridPatient_MouseMove(object sender,MouseEventArgs e) {
			if(gridPatient.Width==400) {
				return;//already resized
			}
			int commright=gridComm.Right;
			this.SuspendLayout();
			gridPatient.Width=400;
			gridComm.Location=new Point(gridPatient.Right+2,gridComm.Top);
			gridComm.Width=commright-gridComm.Left;
			this.ResumeLayout();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

	

		


	}
}





















