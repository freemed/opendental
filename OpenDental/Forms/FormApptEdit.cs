using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;


namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormApptEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.ODGrid gridPatient;
		private OpenDental.UI.ODGrid gridComm;
		private IContainer components;
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
		private ComboBox comboAssistant;
		private ComboBox comboProvHyg;
		private ComboBox comboProvNum;
		private Label label12;
		private CheckBox checkIsNewPatient;
		private Label label3;
		private Label label2;
		private OpenDental.UI.ODGrid gridProc;
		private System.Windows.Forms.Button butSlider;
		private TableTimeBar tbTime;
		private Label label6;
		private ValidNum textAddTime;
		private TextBox textTime;
		private OpenDental.UI.Button butCalcTime;
		private Label label1;
		private ODtextBox textNote;
		private Label labelApptNote;
		private OpenDental.UI.Button butAddComm;
		private Label labelQuickAdd;
		private ListBox listQuickAdd;
		public bool PinIsVisible;
		public bool PinClicked;
		private Panel panel1;
		public bool IsNew;
		private DataSet DS;
		private Appointment AptCur;
		private Appointment AptOld;
		///<summary>The string time pattern in the current increment. Not in the 5 minute increment.</summary>
		private StringBuilder strBTime;
		private bool mouseIsDown;
		private Point mouseOrigin;
		private Point sliderOrigin;
		//private bool procsHaveChanged;
		private InsPlan[] PlanList;
		private Patient pat;
		private Family fam;
		private OpenDental.UI.Button butLab;
		private TextBox textLabCase;
		private TextBox textRequirement;
		private OpenDental.UI.Button butRequirement;
		private ToolTip toolTip1;
		private PatPlan[] PatPlanList;

		///<summary></summary>
		public FormApptEdit(int aptNum)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			DS=Appointments.GetApptEdit(aptNum);
			AptCur=AppointmentB.TableToObject(DS.Tables["Appointment"]);
			AptOld=AptCur.Copy();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormApptEdit));
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
			this.comboAssistant = new System.Windows.Forms.ComboBox();
			this.comboProvHyg = new System.Windows.Forms.ComboBox();
			this.comboProvNum = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.checkIsNewPatient = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelApptNote = new System.Windows.Forms.Label();
			this.labelQuickAdd = new System.Windows.Forms.Label();
			this.listQuickAdd = new System.Windows.Forms.ListBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textTime = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butSlider = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.textRequirement = new System.Windows.Forms.TextBox();
			this.butRequirement = new OpenDental.UI.Button();
			this.textLabCase = new System.Windows.Forms.TextBox();
			this.butLab = new OpenDental.UI.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.textNote = new OpenDental.ODtextBox();
			this.textAddTime = new OpenDental.ValidNum();
			this.butCalcTime = new OpenDental.UI.Button();
			this.butAddComm = new OpenDental.UI.Button();
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
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboConfirmed
			// 
			this.comboConfirmed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboConfirmed.Location = new System.Drawing.Point(118,44);
			this.comboConfirmed.MaxDropDownItems = 30;
			this.comboConfirmed.Name = "comboConfirmed";
			this.comboConfirmed.Size = new System.Drawing.Size(126,21);
			this.comboConfirmed.TabIndex = 84;
			// 
			// comboUnschedStatus
			// 
			this.comboUnschedStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboUnschedStatus.Location = new System.Drawing.Point(118,23);
			this.comboUnschedStatus.MaxDropDownItems = 100;
			this.comboUnschedStatus.Name = "comboUnschedStatus";
			this.comboUnschedStatus.Size = new System.Drawing.Size(126,21);
			this.comboUnschedStatus.TabIndex = 83;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(5,26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111,15);
			this.label4.TabIndex = 82;
			this.label4.Text = "Unscheduled Status";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboStatus
			// 
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Location = new System.Drawing.Point(118,2);
			this.comboStatus.MaxDropDownItems = 10;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(126,21);
			this.comboStatus.TabIndex = 81;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(5,46);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(111,16);
			this.label5.TabIndex = 80;
			this.label5.Text = "Confirmed";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelStatus
			// 
			this.labelStatus.Location = new System.Drawing.Point(5,5);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(111,15);
			this.labelStatus.TabIndex = 79;
			this.labelStatus.Text = "Status";
			this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(132,146);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(113,16);
			this.label24.TabIndex = 138;
			this.label24.Text = "(use hyg color)";
			// 
			// checkIsHygiene
			// 
			this.checkIsHygiene.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsHygiene.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsHygiene.Location = new System.Drawing.Point(27,146);
			this.checkIsHygiene.Name = "checkIsHygiene";
			this.checkIsHygiene.Size = new System.Drawing.Size(104,16);
			this.checkIsHygiene.TabIndex = 137;
			this.checkIsHygiene.Text = "Is Hygiene";
			this.checkIsHygiene.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(118,82);
			this.comboClinic.MaxDropDownItems = 100;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(126,21);
			this.comboClinic.TabIndex = 136;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(17,85);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(98,16);
			this.labelClinic.TabIndex = 135;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboAssistant
			// 
			this.comboAssistant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAssistant.Location = new System.Drawing.Point(118,162);
			this.comboAssistant.MaxDropDownItems = 30;
			this.comboAssistant.Name = "comboAssistant";
			this.comboAssistant.Size = new System.Drawing.Size(126,21);
			this.comboAssistant.TabIndex = 133;
			// 
			// comboProvHyg
			// 
			this.comboProvHyg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvHyg.Location = new System.Drawing.Point(118,124);
			this.comboProvHyg.MaxDropDownItems = 30;
			this.comboProvHyg.Name = "comboProvHyg";
			this.comboProvHyg.Size = new System.Drawing.Size(126,21);
			this.comboProvHyg.TabIndex = 132;
			// 
			// comboProvNum
			// 
			this.comboProvNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvNum.Location = new System.Drawing.Point(118,103);
			this.comboProvNum.MaxDropDownItems = 100;
			this.comboProvNum.Name = "comboProvNum";
			this.comboProvNum.Size = new System.Drawing.Size(126,21);
			this.comboProvNum.TabIndex = 131;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(17,165);
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
			this.checkIsNewPatient.Location = new System.Drawing.Point(21,65);
			this.checkIsNewPatient.Name = "checkIsNewPatient";
			this.checkIsNewPatient.Size = new System.Drawing.Size(110,17);
			this.checkIsNewPatient.TabIndex = 128;
			this.checkIsNewPatient.Text = "New Patient";
			this.checkIsNewPatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(19,126);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98,16);
			this.label3.TabIndex = 127;
			this.label3.Text = "Hygienist";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(18,106);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98,16);
			this.label2.TabIndex = 126;
			this.label2.Text = "Dentist";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelApptNote
			// 
			this.labelApptNote.Location = new System.Drawing.Point(23,247);
			this.labelApptNote.Name = "labelApptNote";
			this.labelApptNote.Size = new System.Drawing.Size(197,16);
			this.labelApptNote.TabIndex = 141;
			this.labelApptNote.Text = "Appointment Note";
			this.labelApptNote.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelQuickAdd
			// 
			this.labelQuickAdd.Location = new System.Drawing.Point(336,1);
			this.labelQuickAdd.Name = "labelQuickAdd";
			this.labelQuickAdd.Size = new System.Drawing.Size(143,39);
			this.labelQuickAdd.TabIndex = 145;
			this.labelQuickAdd.Text = "Single click on items in the list below to add them to the treatment plan.";
			// 
			// listQuickAdd
			// 
			this.listQuickAdd.IntegralHeight = false;
			this.listQuickAdd.Location = new System.Drawing.Point(338,42);
			this.listQuickAdd.Name = "listQuickAdd";
			this.listQuickAdd.Size = new System.Drawing.Size(146,322);
			this.listQuickAdd.TabIndex = 144;
			this.listQuickAdd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listQuickAdd_MouseDown);
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
			this.butSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseDown);
			this.butSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseMove);
			this.butSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseUp);
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.AutoScrollMargin = new System.Drawing.Size(0,3);
			this.panel1.Controls.Add(this.textRequirement);
			this.panel1.Controls.Add(this.butRequirement);
			this.panel1.Controls.Add(this.textLabCase);
			this.panel1.Controls.Add(this.butLab);
			this.panel1.Controls.Add(this.labelStatus);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.comboStatus);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.comboUnschedStatus);
			this.panel1.Controls.Add(this.comboConfirmed);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.checkIsNewPatient);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.comboProvNum);
			this.panel1.Controls.Add(this.comboProvHyg);
			this.panel1.Controls.Add(this.comboAssistant);
			this.panel1.Controls.Add(this.labelClinic);
			this.panel1.Controls.Add(this.comboClinic);
			this.panel1.Controls.Add(this.checkIsHygiene);
			this.panel1.Controls.Add(this.label24);
			this.panel1.Location = new System.Drawing.Point(71,0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(265,247);
			this.panel1.TabIndex = 147;
			// 
			// textRequirement
			// 
			this.textRequirement.Location = new System.Drawing.Point(56,219);
			this.textRequirement.Multiline = true;
			this.textRequirement.Name = "textRequirement";
			this.textRequirement.ReadOnly = true;
			this.textRequirement.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textRequirement.Size = new System.Drawing.Size(188,150);
			this.textRequirement.TabIndex = 144;
			// 
			// butRequirement
			// 
			this.butRequirement.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRequirement.Autosize = true;
			this.butRequirement.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRequirement.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRequirement.CornerRadius = 4F;
			this.butRequirement.Location = new System.Drawing.Point(4,218);
			this.butRequirement.Name = "butRequirement";
			this.butRequirement.Size = new System.Drawing.Size(46,20);
			this.butRequirement.TabIndex = 143;
			this.butRequirement.Text = "Req";
			this.butRequirement.Click += new System.EventHandler(this.butRequirement_Click);
			// 
			// textLabCase
			// 
			this.textLabCase.Location = new System.Drawing.Point(56,183);
			this.textLabCase.Multiline = true;
			this.textLabCase.Name = "textLabCase";
			this.textLabCase.ReadOnly = true;
			this.textLabCase.Size = new System.Drawing.Size(188,34);
			this.textLabCase.TabIndex = 142;
			// 
			// butLab
			// 
			this.butLab.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLab.Autosize = true;
			this.butLab.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLab.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLab.CornerRadius = 4F;
			this.butLab.Location = new System.Drawing.Point(4,182);
			this.butLab.Name = "butLab";
			this.butLab.Size = new System.Drawing.Size(46,20);
			this.butLab.TabIndex = 141;
			this.butLab.Text = "Lab";
			this.butLab.Click += new System.EventHandler(this.butLab_Click);
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(24,263);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Appointment;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(312,101);
			this.textNote.TabIndex = 142;
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
			this.butCalcTime.Click += new System.EventHandler(this.butCalcTime_Click);
			// 
			// butAddComm
			// 
			this.butAddComm.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddComm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddComm.Autosize = true;
			this.butAddComm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddComm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddComm.CornerRadius = 4F;
			this.butAddComm.Image = global::OpenDental.Properties.Resources.commlog;
			this.butAddComm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddComm.Location = new System.Drawing.Point(870,366);
			this.butAddComm.Name = "butAddComm";
			this.butAddComm.Size = new System.Drawing.Size(92,26);
			this.butAddComm.TabIndex = 143;
			this.butAddComm.Text = "Co&mm";
			this.butAddComm.Click += new System.EventHandler(this.butAddComm_Click);
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
			this.gridPatient.Location = new System.Drawing.Point(23,366);
			this.gridPatient.Name = "gridPatient";
			this.gridPatient.ScrollValue = 0;
			this.gridPatient.Size = new System.Drawing.Size(400,336);
			this.gridPatient.TabIndex = 0;
			this.gridPatient.Title = "Patient Info";
			this.gridPatient.TranslationName = "TableApptPtInfo";
			this.gridPatient.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridPatient_MouseMove);
			// 
			// gridComm
			// 
			this.gridComm.HScrollVisible = false;
			this.gridComm.Location = new System.Drawing.Point(425,366);
			this.gridComm.Name = "gridComm";
			this.gridComm.ScrollValue = 0;
			this.gridComm.Size = new System.Drawing.Size(439,336);
			this.gridComm.TabIndex = 1;
			this.gridComm.Title = "Communications Log - Appointment Scheduling";
			this.gridComm.TranslationName = "TableCommLog";
			this.gridComm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridComm_MouseMove);
			this.gridComm.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridComm_CellDoubleClick);
			// 
			// gridProc
			// 
			this.gridProc.AllowSelection = false;
			this.gridProc.HScrollVisible = false;
			this.gridProc.Location = new System.Drawing.Point(485,3);
			this.gridProc.Name = "gridProc";
			this.gridProc.ScrollValue = 0;
			this.gridProc.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridProc.Size = new System.Drawing.Size(488,361);
			this.gridProc.TabIndex = 139;
			this.gridProc.Title = "Procedures - highlight to attach";
			this.gridProc.TranslationName = "TableApptProcs";
			this.gridProc.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridProc_CellClick);
			// 
			// butAudit
			// 
			this.butAudit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAudit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAudit.Autosize = true;
			this.butAudit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAudit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAudit.CornerRadius = 4F;
			this.butAudit.Location = new System.Drawing.Point(870,507);
			this.butAudit.Name = "butAudit";
			this.butAudit.Size = new System.Drawing.Size(92,26);
			this.butAudit.TabIndex = 125;
			this.butAudit.Text = "Audit Trail";
			this.butAudit.Click += new System.EventHandler(this.butAudit_Click);
			// 
			// butTask
			// 
			this.butTask.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butTask.Autosize = true;
			this.butTask.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTask.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTask.CornerRadius = 4F;
			this.butTask.Location = new System.Drawing.Point(870,539);
			this.butTask.Name = "butTask";
			this.butTask.Size = new System.Drawing.Size(92,26);
			this.butTask.TabIndex = 124;
			this.butTask.Text = "To Task List";
			this.butTask.Click += new System.EventHandler(this.butTask_Click);
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
			this.butDelete.Location = new System.Drawing.Point(870,603);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(92,26);
			this.butDelete.TabIndex = 123;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
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
			this.butPin.Location = new System.Drawing.Point(870,571);
			this.butPin.Name = "butPin";
			this.butPin.Size = new System.Drawing.Size(92,26);
			this.butPin.TabIndex = 122;
			this.butPin.Text = "&Pinboard";
			this.butPin.Click += new System.EventHandler(this.butPin_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(870,635);
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
			this.butCancel.Location = new System.Drawing.Point(870,667);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(92,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormApptEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(974,704);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.labelApptNote);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textAddTime);
			this.Controls.Add(this.textTime);
			this.Controls.Add(this.labelQuickAdd);
			this.Controls.Add(this.butCalcTime);
			this.Controls.Add(this.listQuickAdd);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butAddComm);
			this.Controls.Add(this.butSlider);
			this.Controls.Add(this.tbTime);
			this.Controls.Add(this.gridPatient);
			this.Controls.Add(this.gridComm);
			this.Controls.Add(this.gridProc);
			this.Controls.Add(this.butAudit);
			this.Controls.Add(this.butTask);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butPin);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Appointment";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormApptEdit_FormClosing);
			this.Load += new System.EventHandler(this.FormApptEdit_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormApptEdit_Load(object sender, System.EventArgs e){
			tbTime.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbTime_CellClicked);
			if(IsNew){
				if(!Security.IsAuthorized(Permissions.AppointmentCreate)){
					DialogResult=DialogResult.Cancel;
					return;
				}
			}
			else{
				if(!Security.IsAuthorized(Permissions.AppointmentEdit)){
					butOK.Enabled=false;
					butDelete.Enabled=false;
					butPin.Enabled=false;
					butTask.Enabled=false;
					gridProc.Enabled=false;
					listQuickAdd.Enabled=false;
				}
			}
			//The four objects below are needed when adding procs to this appt.
			fam=Patients.GetFamily(AptCur.PatNum);
			pat=fam.GetPatient(AptCur.PatNum);
			PlanList=InsPlans.Refresh(fam);
			PatPlanList=PatPlans.Refresh(AptCur.PatNum);
			if(PrefB.GetBool("EasyHideDentalSchools")) {
				butRequirement.Visible=false;
				textRequirement.Visible=false;
				textNote.Top = 238;
				textNote.Height=126;
				labelApptNote.Top = 219;
			}
			if(PrefB.GetBool("EasyNoClinics")) {
				labelClinic.Visible=false;
				comboClinic.Visible=false;
			}
			if(!PinIsVisible){
				butPin.Visible=false;
			}
			if(AptCur.AptStatus==ApptStatus.Planned) {
				Text = Lan.g(this, "Edit Planned Appointment") + " - " + DS.Tables["Patient"].Rows[0]["value"].ToString(); 
				labelStatus.Visible=false;
				comboStatus.Visible=false;
				butDelete.Visible=false;
			}
			else if (AptCur.AptStatus == ApptStatus.PtNote) {
				labelApptNote.Text="Patient NOTE:";
				Text = Lan.g(this, "Edit Patient Note") + " - " + DS.Tables["Patient"].Rows[0]["value"].ToString() + " on " + AptCur.AptDateTime.DayOfWeek + ", " + AptCur.AptDateTime;
				comboStatus.Items.Add(Lan.g("enumApptStatus", "Patient Note"));
				comboStatus.Items.Add(Lan.g("enumApptStatus", "Completed Pt. Note"));
				comboStatus.SelectedIndex = (int)AptCur.AptStatus - 7;
				labelQuickAdd.Visible = false;
				labelStatus.Visible=false;
				gridProc.Visible=false;
				listQuickAdd.Visible=false;
				textNote.Width = 400;
			}
			else if ( AptCur.AptStatus == ApptStatus.PtNoteCompleted) {
				labelApptNote.Text = "Completed Patient NOTE:";
				Text = Lan.g(this, "Edit Completed Patient Note") + " - " + DS.Tables["Patient"].Rows[0]["value"].ToString() + " on " + AptCur.AptDateTime.DayOfWeek + ", " + AptCur.AptDateTime;
				comboStatus.Items.Add(Lan.g("enumApptStatus", "Patient Note"));
				comboStatus.Items.Add(Lan.g("enumApptStatus", "Completed Pt. Note"));
				comboStatus.SelectedIndex = (int)AptCur.AptStatus - 7;
				labelQuickAdd.Visible = false;
				labelStatus.Visible = false;
				gridProc.Visible= false;
				listQuickAdd.Visible = false;
				textNote.Width = 400;
			}
			else {
				Text = Lan.g(this, "Edit Appointment") + " - " + DS.Tables["Patient"].Rows[0]["value"].ToString() + " on " + AptCur.AptDateTime.DayOfWeek + ", " + AptCur.AptDateTime ;
				comboStatus.Items.Add(Lan.g("enumApptStatus", "Scheduled"));
				comboStatus.Items.Add(Lan.g("enumApptStatus", "Complete"));
				comboStatus.Items.Add(Lan.g("enumApptStatus", "UnschedList"));
				comboStatus.Items.Add(Lan.g("enumApptStatus", "ASAP"));
				comboStatus.Items.Add(Lan.g("enumApptStatus", "Broken"));
				comboStatus.SelectedIndex=(int)AptCur.AptStatus-1;
				/*textNote.Left = 24;//normal
				textNote.Top=262;
				textNote.Height=85;
				textNote.Width=312;
				labelApptNote.Left=23;
				labelApptNote.Top=247;
				*/		
			}
			//convert time pattern from 5 to current increment.
			strBTime=new StringBuilder();
			for(int i=0;i<AptCur.Pattern.Length;i++) {
				strBTime.Append(AptCur.Pattern.Substring(i,1));
				if(PrefB.GetInt("AppointmentTimeIncrement")==10) {
					i++;
				}
				if(PrefB.GetInt("AppointmentTimeIncrement")==15) {
					i++;
					i++;
				}
			}
			comboUnschedStatus.Items.Add(Lan.g(this,"none"));
			comboUnschedStatus.SelectedIndex=0;
			for(int i=0;i<DefB.Short[(int)DefCat.RecallUnschedStatus].Length;i++) {
				comboUnschedStatus.Items.Add(DefB.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
				if(DefB.Short[(int)DefCat.RecallUnschedStatus][i].DefNum==AptCur.UnschedStatus)
					comboUnschedStatus.SelectedIndex=i+1;
			}
			for(int i=0;i<DefB.Short[(int)DefCat.ApptConfirmed].Length;i++) {
				comboConfirmed.Items.Add(DefB.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(DefB.Short[(int)DefCat.ApptConfirmed][i].DefNum==AptCur.Confirmed)
					comboConfirmed.SelectedIndex=i;
			}
			textAddTime.MinVal=-1200;
			textAddTime.MaxVal=1200;
			textAddTime.Text=POut.PInt(AptCur.AddTime*PIn.PInt(((Pref)PrefB.HList["AppointmentTimeIncrement"]).ValueString));
			textNote.Text=AptCur.Note;
			for(int i=0;i<DefB.Short[(int)DefCat.ApptProcsQuickAdd].Length;i++) {
				listQuickAdd.Items.Add(DefB.Short[(int)DefCat.ApptProcsQuickAdd][i].ItemName);
			}
			comboClinic.Items.Add(Lan.g(this,"none"));
			comboClinic.SelectedIndex=0;
			for(int i=0;i<Clinics.List.Length;i++) {
				comboClinic.Items.Add(Clinics.List[i].Description);
				if(Clinics.List[i].ClinicNum==AptCur.ClinicNum)
					comboClinic.SelectedIndex=i+1;
			}
			for(int i=0;i<Providers.List.Length;i++) {
				comboProvNum.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==AptCur.ProvNum)
					comboProvNum.SelectedIndex=i;
			}
			comboProvHyg.Items.Add(Lan.g(this,"none"));
			comboProvHyg.SelectedIndex=0;
			for(int i=0;i<Providers.List.Length;i++) {
				comboProvHyg.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==AptCur.ProvHyg)
					comboProvHyg.SelectedIndex=i+1;
			}
			checkIsHygiene.Checked=AptCur.IsHygiene;
			comboAssistant.Items.Add(Lan.g(this,"none"));
			comboAssistant.SelectedIndex=0;
			for(int i=0;i<Employees.ListShort.Length;i++) {
				comboAssistant.Items.Add(Employees.ListShort[i].FName);
				if(Employees.ListShort[i].EmployeeNum==AptCur.Assistant)
					comboAssistant.SelectedIndex=i+1;
			}
			textLabCase.Text=DS.Tables["Misc"].Rows[0]["labDescript"].ToString();
			textRequirement.Text=DS.Tables["Misc"].Rows[0]["requirements"].ToString();
			//IsNewPatient is set well before opening this form.
			checkIsNewPatient.Checked=AptCur.IsNewPatient;
			if(ContrApptSheet.MinPerIncr==10) {
				tbTime.TopBorder[0,6]=Color.Black;
				tbTime.TopBorder[0,12]=Color.Black;
				tbTime.TopBorder[0,18]=Color.Black;
				tbTime.TopBorder[0,24]=Color.Black;
				tbTime.TopBorder[0,30]=Color.Black;
				tbTime.TopBorder[0,36]=Color.Black;
			}
			else {
				tbTime.TopBorder[0,4]=Color.Black;
				tbTime.TopBorder[0,8]=Color.Black;
				tbTime.TopBorder[0,12]=Color.Black;
				tbTime.TopBorder[0,16]=Color.Black;
				tbTime.TopBorder[0,20]=Color.Black;
				tbTime.TopBorder[0,24]=Color.Black;
				tbTime.TopBorder[0,28]=Color.Black;
				tbTime.TopBorder[0,32]=Color.Black;
				tbTime.TopBorder[0,36]=Color.Black;
			}
			FillProcedures();
			FillPatient();//Must be after FillProcedures(), so that the initial amount for the appointment can be calculated.
			FillTime();
			FillComm();
			textNote.Focus();
			textNote.SelectionStart = 0;

		}

		private void FillPatient(){
			DataTable table=DS.Tables["Patient"];
			gridPatient.BeginUpdate();
			gridPatient.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",120);//Add 2 blank columns
			gridPatient.Columns.Add(col);
			col=new ODGridColumn("",120);
			gridPatient.Columns.Add(col);
			gridPatient.Rows.Clear();
			ODGridRow row;
			for(int i=1;i<table.Rows.Count;i++) {//starts with 1 to skip name
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["field"].ToString());
				row.Cells.Add(table.Rows[i]["value"].ToString());
				gridPatient.Rows.Add(row);
			}
			//Add a UI managed row to display the total fee for the selected procedures in this appointment.
			row=new ODGridRow();
			row.Cells.Add("Fee This Appt");
			row.Cells.Add("");//Calculated below
			gridPatient.Rows.Add(row);
			CalcPatientFeeThisAppt();
			gridPatient.EndUpdate();
			gridPatient.ScrollToEnd();
		}

		///<summary>Calculates the fee for this appointment using the highlighted procedures in the procedure list.</summary>
		private void CalcPatientFeeThisAppt() {
			double feeThisAppt=0;
			for(int i=0;i<gridProc.SelectedIndices.Length;i++) {
				feeThisAppt+=PIn.PDouble(gridProc.Rows[gridProc.SelectedIndices[i]].Cells[5].Text);
			}
			gridPatient.Rows[gridPatient.Rows.Count-1].Cells[1].Text=POut.PDouble(feeThisAppt);
			gridPatient.Invalidate();
		}

		private void FillComm(){
			gridComm.BeginUpdate();
			gridComm.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableCommLog","DateTime"),80);
			gridComm.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableCommLog","Description"),80);
			gridComm.Columns.Add(col);
			gridComm.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<DS.Tables["Comm"].Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(DS.Tables["Comm"].Rows[i]["commDateTime"].ToString());
				row.Cells.Add(DS.Tables["Comm"].Rows[i]["Note"].ToString());
				if(DS.Tables["Comm"].Rows[i]["CommType"].ToString()==Commlogs.GetTypeAuto(CommItemTypeAuto.APPT).ToString()){
					row.ColorBackG=DefB.Long[(int)DefCat.MiscColors][7].ItemColor;
				}
				gridComm.Rows.Add(row);
			}
			gridComm.EndUpdate();
			gridComm.ScrollToEnd();
		}

		private void gridComm_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Commlog item=Commlogs.GetOne(PIn.PInt(DS.Tables["Comm"].Rows[e.Row]["CommlogNum"].ToString()));
			FormCommItem FormCI=new FormCommItem(item);
			FormCI.ShowDialog();
			DS.Tables.Remove("Comm");
			DS.Tables.Add(Appointments.GetApptEditComm(AptCur.AptNum));
			FillComm();
		}

		private void butAddComm_Click(object sender,EventArgs e) {
			Commlog CommlogCur=new Commlog();
			CommlogCur.PatNum=AptCur.PatNum;
			CommlogCur.CommDateTime=DateTime.Now;
			CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.APPT);
			FormCommItem FormCI=new FormCommItem(CommlogCur);
			FormCI.IsNew=true;
			FormCI.ShowDialog();
			DS.Tables.Remove("Comm");
			DS.Tables.Add(Appointments.GetApptEditComm(AptCur.AptNum));
			FillComm();
		}

		private void FillProcedures(){
			gridProc.BeginUpdate();
			gridProc.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableApptProcs","Stat"),40);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Priority"),55);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Tth"),30);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Surf"),40);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Description"),175);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Fee"),60,HorizontalAlignment.Right);
			gridProc.Columns.Add(col);
			gridProc.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<DS.Tables["Procedure"].Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(DS.Tables["Procedure"].Rows[i]["status"].ToString());
				row.Cells.Add(DS.Tables["Procedure"].Rows[i]["priority"].ToString());
				row.Cells.Add(DS.Tables["Procedure"].Rows[i]["toothNum"].ToString());
				row.Cells.Add(DS.Tables["Procedure"].Rows[i]["Surf"].ToString());
				row.Cells.Add(DS.Tables["Procedure"].Rows[i]["descript"].ToString());
				row.Cells.Add(DS.Tables["Procedure"].Rows[i]["fee"].ToString());
				gridProc.Rows.Add(row);
			}
			gridProc.EndUpdate();
			for(int i=0;i<DS.Tables["Procedure"].Rows.Count;i++){
				if(DS.Tables["Procedure"].Rows[i]["attached"].ToString()=="1") {
					gridProc.SetSelected(i,true);
				}
			}
		}

		private void gridProc_CellClick(object sender,ODGridClickEventArgs e) {
			/*if(textAddTime.errorProvider1.GetError(textAddTime)!=""
				//|| textDateTerm.errorProvider1.GetError(textDateTerm)!=""
				) {
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(AptCur.AptStatus==ApptStatus.Complete) {
				//added procedures would be marked complete when form closes. We'll just stop it here.
				if(!Security.IsAuthorized(Permissions.ProcComplCreate)) {
					return;
				}
			}*/
			//procsHaveChanged=true;
			bool isSelected=false;
			for(int i=0;i<gridProc.SelectedIndices.Length;i++){
				if(gridProc.SelectedIndices[i]==e.Row){
					isSelected=true;
				}
			}
			if(isSelected){
				gridProc.SetSelected(e.Row,false);
			}
			else{
				gridProc.SetSelected(e.Row,true);
			}
			/*Procedure ProcCur=arrayProc[ApptProc2[e.Row].Index];
			//Procedure ProcOld=ProcCur.Copy();
			if(AptCur.AptStatus==ApptStatus.Planned) {
				if(ProcCur.PlannedAptNum==AptCur.AptNum) {
					ProcCur.PlannedAptNum=0;
				}
				else {
					ProcCur.PlannedAptNum=AptCur.AptNum;
				}
				Procedures.UpdatePlannedAptNum(ProcCur.ProcNum,ProcCur.PlannedAptNum);
			}
			else {//not Planned
				if(ProcCur.AptNum==AptCur.AptNum) {
					ProcCur.AptNum=0;
				}
				else {
					ProcCur.AptNum=AptCur.AptNum;
				}
				Procedures.UpdateAptNum(ProcCur.ProcNum,ProcCur.AptNum);
			}
			//changing the AptNum of a proc does not affect the recall synch, so no synch here.
			//Procedures.Update(ProcCur,ProcOld);
			//ProcCur.Update(ProcOld);
			int scroll=tbProc.ScrollValue;
			FillProcedures();
			tbProc.ScrollValue=scroll;*/
			CalculateTime();
			FillTime();
			CalcPatientFeeThisAppt();
		}

		private void FillTime() {
			Color provColor=Color.Gray;
			if(comboProvNum.SelectedIndex!=-1) {
				provColor=Providers.List[comboProvNum.SelectedIndex].ProvColor;
			}
			for(int i=0;i<strBTime.Length;i++) {
				if(strBTime.ToString(i,1)=="X") {
					tbTime.BackGColor[0,i]=provColor;
					//.Cell[0,i]=strBTime.ToString(i,1);
				}
				else {
					tbTime.BackGColor[0,i]=Color.White;
				}
			}
			for(int i=strBTime.Length;i<tbTime.MaxRows;i++) {
				//tbTime.Cell[0,i]="";
				tbTime.BackGColor[0,i]=Color.FromName("Control");
			}
			tbTime.Refresh();
			butSlider.Location=new Point(tbTime.Location.X+2
				,(tbTime.Location.Y+strBTime.Length*14+1));
			textTime.Text=(strBTime.Length*ContrApptSheet.MinPerIncr).ToString();
		}

		private void CalculateTime() {
			int adjTimeU=PIn.PInt(textAddTime.Text)/PrefB.GetInt("AppointmentTimeIncrement");
			strBTime=new StringBuilder("");
			string procTime="";
			int codeNum;
			int dentNum=Patients.GetProvNum(pat);
			int hygNum=Patients.GetProvNum(pat);
			if(comboProvNum.SelectedIndex!=-1){
				dentNum=Providers.List[comboProvNum.SelectedIndex].ProvNum;
				hygNum=Providers.List[comboProvNum.SelectedIndex].ProvNum;
			}
			if(comboProvHyg.SelectedIndex!=0) {
				hygNum=Providers.List[comboProvHyg.SelectedIndex-1].ProvNum;
			}
			ProcedureCode procCode;
			if(gridProc.SelectedIndices.Length==1) {
				codeNum=PIn.PInt(DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[0]]["CodeNum"].ToString());
				//we're not going to use the actual procedure.ProvNum, but instead base it on the providers selected for this appt.
				//The actual provNums will be reset on closing.
				procCode=ProcedureCodes.GetProcCode(codeNum);
				if(procCode.IsHygiene) {//hygiene proc
					procTime=ProcCodeNotes.GetTimePattern(hygNum,codeNum);
				}
				else {//dentist proc
					procTime=ProcCodeNotes.GetTimePattern(dentNum,codeNum);
				}
				strBTime.Append(procTime);
			}
			else {//multiple procs or no procs
				for(int i=0;i<gridProc.SelectedIndices.Length;i++) {
					codeNum=PIn.PInt(DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[i]]["CodeNum"].ToString());
					procCode=ProcedureCodes.GetProcCode(codeNum);
					if(procCode.IsHygiene) {//hygiene proc
						procTime=ProcCodeNotes.GetTimePattern(hygNum,codeNum);
					}
					else {//dentist proc
						procTime=ProcCodeNotes.GetTimePattern(dentNum,codeNum);
					}
					if(procTime.Length<2){
						continue;
					}
					for(int n=1;n<procTime.Length-1;n++) {
						if(procTime.Substring(n,1)=="/") {
							strBTime.Append("/");
						}
						else {
							strBTime.Insert(0,"X");
						}
					}
				}
			}
			//MessageBox.Show(strBTime.ToString());
			if(adjTimeU!=0) {
				if(strBTime.Length==0) {//might be useless.
					if(adjTimeU > 0) {
						strBTime.Insert(0,"X",adjTimeU);
					}
				}
				else {//not length 0
					double xRatio;
					if((double)strBTime.ToString().LastIndexOf("X")==0)
						xRatio=1;
					else
						xRatio=(double)strBTime.ToString().LastIndexOf("X")/(double)(strBTime.Length-1);
					if(adjTimeU<0) {//subtract time
						int xPort=(int)(-adjTimeU*xRatio);
						if(xPort > 0)
							if(xPort>=strBTime.Length)
								strBTime=new StringBuilder("");
							else
								strBTime.Remove(0,xPort);
						int iRemove=strBTime.Length-(-adjTimeU-xPort);
						if(iRemove < 0)
							strBTime=new StringBuilder("");
						else if(adjTimeU+xPort > strBTime.Length) {
							strBTime=new StringBuilder("");
						}
						else
							strBTime.Remove(iRemove,-adjTimeU-xPort);
					}
					else {//add time
						//MessageBox.Show("adjTimeU:"+adjTimeU.ToString()+"xratio:"+xRatio.ToString());
						int xPort=(int)Math.Ceiling(adjTimeU*xRatio);
						//MessageBox.Show("xPort:"+xPort.ToString());
						if(xPort > 0)
							strBTime.Insert(0,"X",xPort);
						if(adjTimeU-xPort > 0)
							strBTime.Insert(strBTime.Length-1,"/",adjTimeU-xPort);
					}
				}//end else not length 0
			}//if(adjTimeU!=0)
			if(gridProc.SelectedIndices.Length>1) {//multiple procs
				strBTime.Insert(0,"/");
				strBTime.Append("/");
			}
			else if(gridProc.SelectedIndices.Length==0) {//0 procs
				strBTime.Append("/");
			}
			if(strBTime.Length>39) {
				strBTime.Remove(39,strBTime.Length-39);
			}
		}

		private void tbTime_CellClicked(object sender,CellEventArgs e) {
			if(e.Row<strBTime.Length) {
				if(strBTime[e.Row]=='/') {
					strBTime.Replace('/','X',e.Row,1);
				}
				else {
					strBTime.Replace(strBTime[e.Row],'/',e.Row,1);
				}
			}
			FillTime();
		}

		private void butSlider_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=true;
			mouseOrigin=new Point(e.X+butSlider.Location.X
				,e.Y+butSlider.Location.Y);
			sliderOrigin=butSlider.Location;

		}

		private void butSlider_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(!mouseIsDown)
				return;
			//tempPoint represents the new location of button of smooth dragging.
			Point tempPoint=new Point(sliderOrigin.X
				,sliderOrigin.Y+(e.Y+butSlider.Location.Y)-mouseOrigin.Y);
			int step=(int)(Math.Round((Decimal)(tempPoint.Y-tbTime.Location.Y)/14));
			if(step==strBTime.Length)
				return;
			if(step<1)
				return;
			if(step>tbTime.MaxRows-1)
				return;
			if(step>strBTime.Length) {
				strBTime.Append('/');
			}
			if(step<strBTime.Length) {
				strBTime.Remove(step,1);
			}
			FillTime();
		}

		private void butSlider_MouseUp(object sender,System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=false;
		}

		private void butCalcTime_Click(object sender,System.EventArgs e) {
			if(textAddTime.errorProvider1.GetError(textAddTime)!=""
				) {
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			CalculateTime();
			FillTime();
		}
		
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
			gridComm.Width=commright-gridPatient.Right-2;//doing this before location prevents flicker
			gridComm.Location=new Point(gridPatient.Right+2,gridComm.Top);
			this.ResumeLayout();
		}

		private void listQuickAdd_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(listQuickAdd.IndexFromPoint(e.X,e.Y)==-1) {
				return;
			}
			if(textAddTime.errorProvider1.GetError(textAddTime)!="") {
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(AptCur.AptStatus==ApptStatus.Complete) {
				//added procedures would be marked complete when form closes. We'll just stop it here.
				if(!Security.IsAuthorized(Permissions.ProcComplCreate)) {
					return;
				}
			}
			Procedures.SetDateFirstVisit(AptCur.AptDateTime.Date,1,pat);
			Benefit[] benefitList=Benefits.Refresh(PatPlanList);
			ClaimProc[] ClaimProcList=ClaimProcs.Refresh(AptCur.PatNum);
			string[] codes=DefB.Short[(int)DefCat.ApptProcsQuickAdd][listQuickAdd.IndexFromPoint(e.X,e.Y)].ItemValue.Split(',');
			for(int i=0;i<codes.Length;i++) {
				if(!ProcedureCodes.HList.ContainsKey(codes[i])){
					MsgBox.Show(this,"Definition contains invalid code.");
					return;
				}
			}
			for(int i=0;i<codes.Length;i++) {
				Procedure ProcCur=new Procedure();
				//maybe test codes in defs before allowing them in the first place(no tooth num)
				//if(ProcCodes.GetProcCode(Procedures.Cur.ProcCode). 
				ProcCur.PatNum=AptCur.PatNum;
				if(AptCur.AptStatus!=ApptStatus.Planned)
					ProcCur.AptNum=AptCur.AptNum;
				ProcCur.CodeNum=ProcedureCodes.GetProcCode(codes[i]).CodeNum;
				ProcCur.ProcDate=AptCur.AptDateTime.Date;
				ProcCur.ProcFee=Fees.GetAmount0(ProcCur.CodeNum,Fees.GetFeeSched(pat,PlanList,PatPlanList));
				//surf
				//toothnum
				//toothrange
				//priority
				ProcCur.ProcStatus=ProcStat.TP;
				//procnote
				ProcCur.ProvNum=AptCur.ProvNum;
				//Dx
				ProcCur.ClinicNum=AptCur.ClinicNum;
				if(AptCur.AptStatus==ApptStatus.Planned)
					ProcCur.PlannedAptNum=AptCur.AptNum;
				ProcCur.MedicalCode=ProcedureCodes.GetProcCode(ProcCur.CodeNum).MedicalCode;
				ProcCur.BaseUnits=ProcedureCodes.GetProcCode(ProcCur.CodeNum).BaseUnits;
				Procedures.Insert(ProcCur);//recall synch not required
				Procedures.ComputeEstimates(ProcCur,pat.PatNum,ClaimProcList,false,PlanList,PatPlanList,benefitList);
			}
			listQuickAdd.SelectedIndex=-1;
			string[] selectedProcs=new string[gridProc.SelectedIndices.Length];
			for(int i=0;i<selectedProcs.Length;i++){
				selectedProcs[i]=DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[i]]["ProcNum"].ToString();
			}
			DS.Tables.Remove("Procedure");
			DS.Tables.Add(Appointments.GetApptEditProcs(AptCur.AptNum));
			FillProcedures();
			for(int i=0;i<gridProc.Rows.Count;i++){
				for(int j=0;j<selectedProcs.Length;j++){
					if(selectedProcs[j]==DS.Tables["Procedure"].Rows[i]["ProcNum"].ToString()){
						gridProc.SetSelected(i,true);
					}
				}
			}
			CalculateTime();
			FillTime();
			CalcPatientFeeThisAppt();
		}

		private void butLab_Click(object sender,EventArgs e) {
			if(DS.Tables["Misc"].Rows[0]["LabCaseNum"].ToString()=="0"){//no labcase
				//so let user pick one to add
				FormLabCaseSelect FormL=new FormLabCaseSelect();
				FormL.PatNum=AptCur.PatNum;
				FormL.IsPlanned=AptCur.AptStatus==ApptStatus.Planned;
				FormL.ShowDialog();
				if(FormL.DialogResult!=DialogResult.OK){
					return;
				}
				if(AptCur.AptStatus==ApptStatus.Planned){
					LabCases.AttachToPlannedAppt(FormL.SelectedLabCaseNum,AptCur.AptNum);
				}
				else{
					LabCases.AttachToAppt(FormL.SelectedLabCaseNum,AptCur.AptNum);
				}
			}
			else{//already a labcase attached
				FormLabCaseEdit FormLCE=new FormLabCaseEdit();
				FormLCE.CaseCur=LabCases.GetOne(PIn.PInt(DS.Tables["Misc"].Rows[0]["LabCaseNum"].ToString()));
				FormLCE.ShowDialog();
				if(FormLCE.DialogResult!=DialogResult.OK){
					return;
				}
				//Deleting or detaching labcase would have been done from in that window
			}
			DS.Tables.Remove("Misc");
			DS.Tables.Add(Appointments.GetApptEditMisc(AptCur.AptNum));
			textLabCase.Text=DS.Tables["Misc"].Rows[0]["labDescript"].ToString();
		}

		private void butRequirement_Click(object sender,EventArgs e) {
			FormReqAppt FormR=new FormReqAppt();
			FormR.AptNum=AptCur.AptNum;
			FormR.ShowDialog();
			if(FormR.DialogResult!=DialogResult.OK){
				return;
			}
			DS.Tables.Remove("Misc");
			DS.Tables.Add(Appointments.GetApptEditMisc(AptCur.AptNum));
			textRequirement.Text=DS.Tables["Misc"].Rows[0]["requirements"].ToString();
		}

		///<summary>Called from butOK_Click and butPin_Click</summary>
		private bool UpdateToDB(){
			if(textAddTime.errorProvider1.GetError(textAddTime)!=""
				//|| textDateTerm.errorProvider1.GetError(textDateTerm)!=""
				) {
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			if (AptCur.AptStatus == ApptStatus.Planned) {
				;
			}
			else if(comboStatus.SelectedIndex==-1) {
				AptCur.AptStatus=ApptStatus.Scheduled;
			}
			else if (AptCur.AptStatus == ApptStatus.PtNote | AptCur.AptStatus == ApptStatus.PtNoteCompleted){
				AptCur.AptStatus = (ApptStatus)comboStatus.SelectedIndex + 7;
			}
			else {
				AptCur.AptStatus=(ApptStatus)comboStatus.SelectedIndex+1;
			}
			//set procs complete was moved further down
			//convert from current increment into 5 minute increment
			//MessageBox.Show(strBTime.ToString());
			StringBuilder savePattern=new StringBuilder();
			for(int i=0;i<strBTime.Length;i++) {
				savePattern.Append(strBTime[i]);
				savePattern.Append(strBTime[i]);
				if(PrefB.GetInt("AppointmentTimeIncrement")==15) {
					savePattern.Append(strBTime[i]);
				}
			}
			if(savePattern.Length==0) {
				savePattern=new StringBuilder("/");
			}
			//MessageBox.Show(savePattern.ToString());
			AptCur.Pattern=savePattern.ToString();
			if(comboUnschedStatus.SelectedIndex==0)//none
				AptCur.UnschedStatus=0;
			else
				AptCur.UnschedStatus
					=DefB.Short[(int)DefCat.RecallUnschedStatus][comboUnschedStatus.SelectedIndex-1].DefNum;
			if(comboConfirmed.SelectedIndex!=-1)
				AptCur.Confirmed
					=DefB.Short[(int)DefCat.ApptConfirmed][comboConfirmed.SelectedIndex].DefNum;
			AptCur.AddTime=(int)(PIn.PInt(textAddTime.Text)/
				PIn.PInt(((Pref)PrefB.HList["AppointmentTimeIncrement"]).ValueString));
			AptCur.Note=textNote.Text;
			if(comboClinic.SelectedIndex==0)//none
				AptCur.ClinicNum=0;
			else
				AptCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			//there should always be a non-hidden primary provider for an appt.
			if(comboProvNum.SelectedIndex==-1)
				AptCur.ProvNum=Providers.List[0].ProvNum;
			else
				AptCur.ProvNum=Providers.List[comboProvNum.SelectedIndex].ProvNum;
			if(comboProvHyg.SelectedIndex==0)//none
				AptCur.ProvHyg=0;
			else
				AptCur.ProvHyg=Providers.List[comboProvHyg.SelectedIndex-1].ProvNum;
			AptCur.IsHygiene=checkIsHygiene.Checked;
			if(comboAssistant.SelectedIndex==0)//none
				AptCur.Assistant=0;
			else
				AptCur.Assistant=Employees.ListShort[comboAssistant.SelectedIndex-1].EmployeeNum;
			AptCur.IsNewPatient=checkIsNewPatient.Checked;
			AptCur.ProcDescript="";
			for(int i=0;i<gridProc.SelectedIndices.Length;i++) {
				if(i>0) AptCur.ProcDescript+=", ";
				AptCur.ProcDescript+=ProcedureCodes.GetProcCode(
					PIn.PInt(DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[i]]["CodeNum"].ToString())).AbbrDesc;
			}
			int[] procNums=new int[gridProc.SelectedIndices.Length];
			for(int i=0;i<procNums.Length;i++){
				procNums[i]=PIn.PInt(DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[i]]["ProcNum"].ToString());
			}
			bool isPlanned=AptCur.AptStatus==ApptStatus.Planned;
			try {
				Appointments.Update(AptCur,AptOld);
				Appointments.UpdateAttached(AptCur.AptNum,procNums,isPlanned);
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);
				return false;
			}
			//if appointment is marked complete and any procedures are not,
			//then set the remaining procedures complete
			if(AptCur.AptStatus==ApptStatus.Complete) {
				bool allProcsComplete=true;
				for(int i=0;i<gridProc.SelectedIndices.Length;i++){
					if(DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[i]]["ProcStatus"].ToString()!="2") {//Complete
						allProcsComplete=false;
						break;
					}
				}
				if(!allProcsComplete) {
					if(!Security.IsAuthorized(Permissions.ProcComplCreate)) {
						return false;
					}
					Procedures.SetCompleteInAppt(AptCur,PlanList,PatPlanList);
					SecurityLogs.MakeLogEntry(Permissions.ProcComplCreate,pat.PatNum,
						pat.GetNameLF()+" "+AptCur.AptDateTime.ToShortDateString());
				}
			}
			else{
				Procedures.SetProvidersInAppointment(AptCur,Procedures.GetProcsForSingle(AptCur.AptNum,false));
			}
			return true;
		}

		private void butAudit_Click(object sender,EventArgs e) {
			FormAuditOneType FormA=new FormAuditOneType(pat.PatNum,
				new Permissions[] { Permissions.AppointmentCreate,Permissions.AppointmentEdit,Permissions.AppointmentMove },
				Lan.g(this,"All Appointments for")+pat.GetNameFL());
			FormA.ShowDialog();
		}

		private void butTask_Click(object sender,EventArgs e) {
			if(!UpdateToDB())
				return;
			FormTaskListSelect FormT=new FormTaskListSelect(TaskObjectType.Patient,AptCur.AptNum);
			FormT.ShowDialog();
		}

		private void butPin_Click(object sender,System.EventArgs e) {
			if(!UpdateToDB())
				return;
			PinClicked=true;
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if (AptCur.AptStatus == ApptStatus.PtNote | AptCur.AptStatus == ApptStatus.PtNoteCompleted) {
				if (!MsgBox.Show(this, true, "Delete Patient Note?")) {
					return;
				}
				if (textNote.Text != "") {
					if (MessageBox.Show(Lan.g(this, "Save a copy of this note in CommLog? " + "\r\n" + "\r\n" + textNote.Text), "Question...",
							MessageBoxButtons.YesNo) == DialogResult.Yes) {
						Commlog CommlogCur = new Commlog();
						CommlogCur.PatNum = AptCur.PatNum;
						CommlogCur.CommDateTime = DateTime.Now;
						CommlogCur.CommType = Commlogs.GetTypeAuto(CommItemTypeAuto.APPT);
						CommlogCur.Note = "Deleted Pt NOTE from schedule, saved copy: ";
						CommlogCur.Note += textNote.Text;
						//there is no dialog here because it is just a simple entry
						Commlogs.Insert(CommlogCur);
					}
				}
			}
			else {
				if (MessageBox.Show(Lan.g(this, "Delete appointment?"), "", MessageBoxButtons.OKCancel) != DialogResult.OK) {
					return;
				}
				if (textNote.Text != "") {
					if (MessageBox.Show(Lan.g(this, "Save appointment note in CommLog? " + "\r\n" + "\r\n" + textNote.Text), "Question...",
							MessageBoxButtons.YesNo) == DialogResult.Yes) {
						Commlog CommlogCur = new Commlog();
						CommlogCur.PatNum = AptCur.PatNum;
						CommlogCur.CommDateTime = DateTime.Now;
						CommlogCur.CommType = Commlogs.GetTypeAuto(CommItemTypeAuto.APPT);
						CommlogCur.Note = "Deleted Appt. & saved note: ";
						if (AptCur.ProcDescript != "") {
							CommlogCur.Note += AptCur.ProcDescript + ": ";
						}
						CommlogCur.Note += textNote.Text;
						//there is no dialog here because it is just a simple entry
						Commlogs.Insert(CommlogCur);
					}
				}
			}

			/*if(AptCur.AptStatus==ApptStatus.Planned) {
				Procedures.UnattachProcsInPlannedAppt(AptCur.AptNum);
			}
			else {
				Procedures.UnattachProcsInAppt(AptCur.AptNum);
			}*/
			Appointments.Delete(AptCur.AptNum);
			SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,pat.PatNum,
				"Delete for patient: "
				+pat.GetNameLF()+", "
				+AptCur.AptDateTime.ToString());
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!UpdateToDB()){
				return;
			}
			if(IsNew) {
				SecurityLogs.MakeLogEntry(Permissions.AppointmentCreate,pat.PatNum,pat.GetNameLF()+", "
					+AptCur.AptDateTime.ToString()+", "
					+AptCur.ProcDescript);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormApptEdit_FormClosing(object sender,FormClosingEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew) {
				Appointments.Delete(AptCur.AptNum);
			}
		}





	

		

		

		

		

		

		

		

		

		

		

	

		


	}
}





















