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
		private TextBox textTime;
		private ODtextBox textNote;
		private Label labelApptNote;
		private OpenDental.UI.Button butAddComm;
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
		private List <InsPlan> PlanList;
		private Patient pat;
		private Family fam;
		private OpenDental.UI.Button butLab;
		private TextBox textLabCase;
		private TextBox textRequirement;
		private OpenDental.UI.Button butRequirement;
		private ToolTip toolTip1;
		private Label labelTimeArrived;
		private TextBox textTimeDismissed;
		private Label label7;
		private TextBox textTimeSeated;
		private Label label1;
		private TextBox textTimeArrived;
		private ContextMenu contextMenuTimeArrived;
		private MenuItem menuItemArrivedNow;
		private ContextMenu contextMenuTimeSeated;
		private MenuItem menuItemSeatedNow;
		private ContextMenu contextMenuTimeDismissed;
		private MenuItem menuItemDismissedNow;
		private ListBox listQuickAdd;
		private Label labelQuickAdd;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butDeleteProc;
		private OpenDental.UI.Button butComplete;
		private CheckBox checkTimeLocked;
		private TextBox textInsPlan2;
		private Label label9;
		private TextBox textInsPlan1;
		private Label label8;
		private OpenDental.UI.Button butInsPlan1;
		private OpenDental.UI.Button butInsPlan2;
		private OpenDental.UI.Button butPickHyg;
		private OpenDental.UI.Button butPickDentist;
		///<summary>This is the way to pass a "signal" up to the parent form that OD is to close.</summary>
		public bool CloseOD;

		///<summary></summary>
		public FormApptEdit(long aptNum)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			DS=Appointments.GetApptEdit(aptNum);
			AptCur=Appointments.TableToObject(DS.Tables["Appointment"]);
			AptOld=AptCur.Clone();
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
			this.label6 = new System.Windows.Forms.Label();
			this.textTime = new System.Windows.Forms.TextBox();
			this.butSlider = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.butInsPlan2 = new OpenDental.UI.Button();
			this.butInsPlan1 = new OpenDental.UI.Button();
			this.textInsPlan2 = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textInsPlan1 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textTimeDismissed = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textTimeSeated = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textTimeArrived = new System.Windows.Forms.TextBox();
			this.labelTimeArrived = new System.Windows.Forms.Label();
			this.textRequirement = new System.Windows.Forms.TextBox();
			this.butRequirement = new OpenDental.UI.Button();
			this.textLabCase = new System.Windows.Forms.TextBox();
			this.butLab = new OpenDental.UI.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.checkTimeLocked = new System.Windows.Forms.CheckBox();
			this.contextMenuTimeArrived = new System.Windows.Forms.ContextMenu();
			this.menuItemArrivedNow = new System.Windows.Forms.MenuItem();
			this.contextMenuTimeSeated = new System.Windows.Forms.ContextMenu();
			this.menuItemSeatedNow = new System.Windows.Forms.MenuItem();
			this.contextMenuTimeDismissed = new System.Windows.Forms.ContextMenu();
			this.menuItemDismissedNow = new System.Windows.Forms.MenuItem();
			this.listQuickAdd = new System.Windows.Forms.ListBox();
			this.labelQuickAdd = new System.Windows.Forms.Label();
			this.butDeleteProc = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.textNote = new OpenDental.ODtextBox();
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
			this.butComplete = new OpenDental.UI.Button();
			this.butPickDentist = new OpenDental.UI.Button();
			this.butPickHyg = new OpenDental.UI.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboConfirmed
			// 
			this.comboConfirmed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboConfirmed.Location = new System.Drawing.Point(118,43);
			this.comboConfirmed.MaxDropDownItems = 30;
			this.comboConfirmed.Name = "comboConfirmed";
			this.comboConfirmed.Size = new System.Drawing.Size(126,21);
			this.comboConfirmed.TabIndex = 84;
			// 
			// comboUnschedStatus
			// 
			this.comboUnschedStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboUnschedStatus.Location = new System.Drawing.Point(118,22);
			this.comboUnschedStatus.MaxDropDownItems = 100;
			this.comboUnschedStatus.Name = "comboUnschedStatus";
			this.comboUnschedStatus.Size = new System.Drawing.Size(126,21);
			this.comboUnschedStatus.TabIndex = 83;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(5,25);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111,15);
			this.label4.TabIndex = 82;
			this.label4.Text = "Unscheduled Status";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboStatus
			// 
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Location = new System.Drawing.Point(118,1);
			this.comboStatus.MaxDropDownItems = 10;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(126,21);
			this.comboStatus.TabIndex = 81;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(5,45);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(111,16);
			this.label5.TabIndex = 80;
			this.label5.Text = "Confirmed";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelStatus
			// 
			this.labelStatus.Location = new System.Drawing.Point(5,4);
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
			this.comboClinic.Location = new System.Drawing.Point(118,81);
			this.comboClinic.MaxDropDownItems = 100;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(126,21);
			this.comboClinic.TabIndex = 136;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(17,84);
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
			this.comboProvHyg.Size = new System.Drawing.Size(107,21);
			this.comboProvHyg.TabIndex = 132;
			// 
			// comboProvNum
			// 
			this.comboProvNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvNum.Location = new System.Drawing.Point(118,102);
			this.comboProvNum.MaxDropDownItems = 100;
			this.comboProvNum.Name = "comboProvNum";
			this.comboProvNum.Size = new System.Drawing.Size(107,21);
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
			this.checkIsNewPatient.Location = new System.Drawing.Point(21,64);
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
			this.label2.Location = new System.Drawing.Point(18,105);
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
			// butSlider
			// 
			this.butSlider.BackColor = System.Drawing.SystemColors.ControlDark;
			this.butSlider.Location = new System.Drawing.Point(6,168);
			this.butSlider.Name = "butSlider";
			this.butSlider.Size = new System.Drawing.Size(12,15);
			this.butSlider.TabIndex = 60;
			this.butSlider.UseVisualStyleBackColor = false;
			this.butSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseMove);
			this.butSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseDown);
			this.butSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseUp);
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.AutoScrollMargin = new System.Drawing.Size(0,3);
			this.panel1.Controls.Add(this.butPickHyg);
			this.panel1.Controls.Add(this.butPickDentist);
			this.panel1.Controls.Add(this.butInsPlan2);
			this.panel1.Controls.Add(this.butInsPlan1);
			this.panel1.Controls.Add(this.textInsPlan2);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.textInsPlan1);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.textTimeDismissed);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.textTimeSeated);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.textTimeArrived);
			this.panel1.Controls.Add(this.labelTimeArrived);
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
			// butInsPlan2
			// 
			this.butInsPlan2.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butInsPlan2.Autosize = false;
			this.butInsPlan2.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInsPlan2.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInsPlan2.CornerRadius = 2F;
			this.butInsPlan2.Location = new System.Drawing.Point(225,300);
			this.butInsPlan2.Name = "butInsPlan2";
			this.butInsPlan2.Size = new System.Drawing.Size(18,20);
			this.butInsPlan2.TabIndex = 156;
			this.butInsPlan2.Text = "...";
			this.butInsPlan2.Click += new System.EventHandler(this.butInsPlan2_Click);
			// 
			// butInsPlan1
			// 
			this.butInsPlan1.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butInsPlan1.Autosize = false;
			this.butInsPlan1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInsPlan1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInsPlan1.CornerRadius = 2F;
			this.butInsPlan1.Location = new System.Drawing.Point(225,279);
			this.butInsPlan1.Name = "butInsPlan1";
			this.butInsPlan1.Size = new System.Drawing.Size(18,20);
			this.butInsPlan1.TabIndex = 155;
			this.butInsPlan1.Text = "...";
			this.butInsPlan1.Click += new System.EventHandler(this.butInsPlan1_Click);
			// 
			// textInsPlan2
			// 
			this.textInsPlan2.Location = new System.Drawing.Point(66,300);
			this.textInsPlan2.Name = "textInsPlan2";
			this.textInsPlan2.ReadOnly = true;
			this.textInsPlan2.Size = new System.Drawing.Size(158,20);
			this.textInsPlan2.TabIndex = 154;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(5,302);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(59,16);
			this.label9.TabIndex = 153;
			this.label9.Text = "InsPlan 2";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textInsPlan1
			// 
			this.textInsPlan1.Location = new System.Drawing.Point(66,279);
			this.textInsPlan1.Name = "textInsPlan1";
			this.textInsPlan1.ReadOnly = true;
			this.textInsPlan1.Size = new System.Drawing.Size(158,20);
			this.textInsPlan1.TabIndex = 152;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(5,281);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(59,16);
			this.label8.TabIndex = 151;
			this.label8.Text = "InsPlan 1";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTimeDismissed
			// 
			this.textTimeDismissed.Location = new System.Drawing.Point(118,258);
			this.textTimeDismissed.Name = "textTimeDismissed";
			this.textTimeDismissed.Size = new System.Drawing.Size(126,20);
			this.textTimeDismissed.TabIndex = 150;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(5,260);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(111,16);
			this.label7.TabIndex = 149;
			this.label7.Text = "Time Dismissed";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTimeSeated
			// 
			this.textTimeSeated.Location = new System.Drawing.Point(118,238);
			this.textTimeSeated.Name = "textTimeSeated";
			this.textTimeSeated.Size = new System.Drawing.Size(126,20);
			this.textTimeSeated.TabIndex = 148;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5,240);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111,16);
			this.label1.TabIndex = 147;
			this.label1.Text = "Time Seated";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTimeArrived
			// 
			this.textTimeArrived.Location = new System.Drawing.Point(118,218);
			this.textTimeArrived.Name = "textTimeArrived";
			this.textTimeArrived.Size = new System.Drawing.Size(126,20);
			this.textTimeArrived.TabIndex = 146;
			// 
			// labelTimeArrived
			// 
			this.labelTimeArrived.Location = new System.Drawing.Point(5,220);
			this.labelTimeArrived.Name = "labelTimeArrived";
			this.labelTimeArrived.Size = new System.Drawing.Size(111,16);
			this.labelTimeArrived.TabIndex = 145;
			this.labelTimeArrived.Text = "Time Arrived";
			this.labelTimeArrived.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textRequirement
			// 
			this.textRequirement.Location = new System.Drawing.Point(56,321);
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
			this.butRequirement.Location = new System.Drawing.Point(4,321);
			this.butRequirement.Name = "butRequirement";
			this.butRequirement.Size = new System.Drawing.Size(46,20);
			this.butRequirement.TabIndex = 143;
			this.butRequirement.Text = "Req";
			this.butRequirement.Click += new System.EventHandler(this.butRequirement_Click);
			// 
			// textLabCase
			// 
			this.textLabCase.AcceptsReturn = true;
			this.textLabCase.Location = new System.Drawing.Point(56,183);
			this.textLabCase.Multiline = true;
			this.textLabCase.Name = "textLabCase";
			this.textLabCase.ReadOnly = true;
			this.textLabCase.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
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
			// checkTimeLocked
			// 
			this.checkTimeLocked.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTimeLocked.Location = new System.Drawing.Point(4,41);
			this.checkTimeLocked.Name = "checkTimeLocked";
			this.checkTimeLocked.Size = new System.Drawing.Size(70,33);
			this.checkTimeLocked.TabIndex = 148;
			this.checkTimeLocked.Text = "Time Locked";
			this.checkTimeLocked.Click += new System.EventHandler(this.checkTimeLocked_Click);
			// 
			// contextMenuTimeArrived
			// 
			this.contextMenuTimeArrived.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemArrivedNow});
			// 
			// menuItemArrivedNow
			// 
			this.menuItemArrivedNow.Index = 0;
			this.menuItemArrivedNow.Text = "Now";
			this.menuItemArrivedNow.Click += new System.EventHandler(this.menuItemArrivedNow_Click);
			// 
			// contextMenuTimeSeated
			// 
			this.contextMenuTimeSeated.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSeatedNow});
			// 
			// menuItemSeatedNow
			// 
			this.menuItemSeatedNow.Index = 0;
			this.menuItemSeatedNow.Text = "Now";
			this.menuItemSeatedNow.Click += new System.EventHandler(this.menuItemSeatedNow_Click);
			// 
			// contextMenuTimeDismissed
			// 
			this.contextMenuTimeDismissed.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemDismissedNow});
			// 
			// menuItemDismissedNow
			// 
			this.menuItemDismissedNow.Index = 0;
			this.menuItemDismissedNow.Text = "Now";
			this.menuItemDismissedNow.Click += new System.EventHandler(this.menuItemDismissedNow_Click);
			// 
			// listQuickAdd
			// 
			this.listQuickAdd.IntegralHeight = false;
			this.listQuickAdd.Location = new System.Drawing.Point(338,42);
			this.listQuickAdd.Name = "listQuickAdd";
			this.listQuickAdd.Size = new System.Drawing.Size(146,322);
			this.listQuickAdd.TabIndex = 150;
			this.listQuickAdd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listQuickAdd_MouseDown);
			// 
			// labelQuickAdd
			// 
			this.labelQuickAdd.Location = new System.Drawing.Point(338,1);
			this.labelQuickAdd.Name = "labelQuickAdd";
			this.labelQuickAdd.Size = new System.Drawing.Size(143,39);
			this.labelQuickAdd.TabIndex = 149;
			this.labelQuickAdd.Text = "Single click on items in the list below to add them to this appointment.";
			// 
			// butDeleteProc
			// 
			this.butDeleteProc.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDeleteProc.Autosize = true;
			this.butDeleteProc.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDeleteProc.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDeleteProc.CornerRadius = 4F;
			this.butDeleteProc.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDeleteProc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDeleteProc.Location = new System.Drawing.Point(485,2);
			this.butDeleteProc.Name = "butDeleteProc";
			this.butDeleteProc.Size = new System.Drawing.Size(75,24);
			this.butDeleteProc.TabIndex = 154;
			this.butDeleteProc.Text = "Delete";
			this.butDeleteProc.Click += new System.EventHandler(this.butDeleteProc_Click);
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
			this.butAdd.Location = new System.Drawing.Point(561,2);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,24);
			this.butAdd.TabIndex = 152;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
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
			// butAddComm
			// 
			this.butAddComm.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddComm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddComm.Autosize = true;
			this.butAddComm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddComm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddComm.CornerRadius = 4F;
			this.butAddComm.Image = global::OpenDental.Properties.Resources.commlog;
			this.butAddComm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddComm.Location = new System.Drawing.Point(871,366);
			this.butAddComm.Name = "butAddComm";
			this.butAddComm.Size = new System.Drawing.Size(92,24);
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
			this.gridPatient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridPatient.HScrollVisible = false;
			this.gridPatient.Location = new System.Drawing.Point(23,366);
			this.gridPatient.Name = "gridPatient";
			this.gridPatient.ScrollValue = 0;
			this.gridPatient.Size = new System.Drawing.Size(313,336);
			this.gridPatient.TabIndex = 0;
			this.gridPatient.Title = "Patient Info";
			this.gridPatient.TranslationName = "TableApptPtInfo";
			this.gridPatient.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridPatient_MouseMove);
			// 
			// gridComm
			// 
			this.gridComm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridComm.HScrollVisible = false;
			this.gridComm.Location = new System.Drawing.Point(338,366);
			this.gridComm.Name = "gridComm";
			this.gridComm.ScrollValue = 0;
			this.gridComm.Size = new System.Drawing.Size(525,336);
			this.gridComm.TabIndex = 1;
			this.gridComm.Title = "Communications Log - Appointment Scheduling";
			this.gridComm.TranslationName = "TableCommLog";
			this.gridComm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridComm_MouseMove);
			this.gridComm.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridComm_CellDoubleClick);
			// 
			// gridProc
			// 
			this.gridProc.AllowSelection = false;
			this.gridProc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridProc.HScrollVisible = false;
			this.gridProc.Location = new System.Drawing.Point(485,28);
			this.gridProc.Name = "gridProc";
			this.gridProc.ScrollValue = 0;
			this.gridProc.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridProc.Size = new System.Drawing.Size(488,336);
			this.gridProc.TabIndex = 139;
			this.gridProc.Title = "Procedures on this Appointment";
			this.gridProc.TranslationName = "TableApptProcs";
			this.gridProc.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridProc_CellClick);
			this.gridProc.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridProc_CellDoubleClick);
			// 
			// butAudit
			// 
			this.butAudit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAudit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAudit.Autosize = true;
			this.butAudit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAudit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAudit.CornerRadius = 4F;
			this.butAudit.Location = new System.Drawing.Point(871,522);
			this.butAudit.Name = "butAudit";
			this.butAudit.Size = new System.Drawing.Size(92,24);
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
			this.butTask.Location = new System.Drawing.Point(871,552);
			this.butTask.Name = "butTask";
			this.butTask.Size = new System.Drawing.Size(92,24);
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
			this.butDelete.Location = new System.Drawing.Point(871,612);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(92,24);
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
			this.butPin.Location = new System.Drawing.Point(871,582);
			this.butPin.Name = "butPin";
			this.butPin.Size = new System.Drawing.Size(92,24);
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
			this.butOK.Location = new System.Drawing.Point(871,642);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(92,24);
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
			this.butCancel.Location = new System.Drawing.Point(871,672);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(92,24);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butComplete
			// 
			this.butComplete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butComplete.Autosize = true;
			this.butComplete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butComplete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butComplete.CornerRadius = 4F;
			this.butComplete.Location = new System.Drawing.Point(871,492);
			this.butComplete.Name = "butComplete";
			this.butComplete.Size = new System.Drawing.Size(92,24);
			this.butComplete.TabIndex = 155;
			this.butComplete.Text = "Complete";
			this.butComplete.Visible = false;
			this.butComplete.Click += new System.EventHandler(this.butComplete_Click);
			// 
			// butPickDentist
			// 
			this.butPickDentist.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPickDentist.Autosize = false;
			this.butPickDentist.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickDentist.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickDentist.CornerRadius = 2F;
			this.butPickDentist.Location = new System.Drawing.Point(226,103);
			this.butPickDentist.Name = "butPickDentist";
			this.butPickDentist.Size = new System.Drawing.Size(18,20);
			this.butPickDentist.TabIndex = 157;
			this.butPickDentist.Text = "...";
			this.butPickDentist.Click += new System.EventHandler(this.butPickDentist_Click);
			// 
			// butPickHyg
			// 
			this.butPickHyg.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPickHyg.Autosize = false;
			this.butPickHyg.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickHyg.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickHyg.CornerRadius = 2F;
			this.butPickHyg.Location = new System.Drawing.Point(226,125);
			this.butPickHyg.Name = "butPickHyg";
			this.butPickHyg.Size = new System.Drawing.Size(18,20);
			this.butPickHyg.TabIndex = 158;
			this.butPickHyg.Text = "...";
			this.butPickHyg.Click += new System.EventHandler(this.butPickHyg_Click);
			// 
			// FormApptEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(975,704);
			this.Controls.Add(this.butComplete);
			this.Controls.Add(this.butDeleteProc);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listQuickAdd);
			this.Controls.Add(this.labelQuickAdd);
			this.Controls.Add(this.checkTimeLocked);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.labelApptNote);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textTime);
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
			this.Load += new System.EventHandler(this.FormApptEdit_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormApptEdit_FormClosing);
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
					//butRemove.Enabled=false;
					butAdd.Enabled=false;
					butDeleteProc.Enabled=false;
				}
			}
			//The four objects below are needed when adding procs to this appt.
			fam=Patients.GetFamily(AptCur.PatNum);
			pat=fam.GetPatient(AptCur.PatNum);
			PlanList=InsPlans.RefreshForFam(fam);
			if(PrefC.GetBool(PrefName.EasyHideDentalSchools)) {
				butRequirement.Visible=false;
				textRequirement.Visible=false;
			}
			if(PrefC.GetBool(PrefName.EasyNoClinics)) {
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
			}
			if(AptCur.AptStatus==ApptStatus.UnschedList) {
				comboStatus.Enabled=false;
			}
			//convert time pattern from 5 to current increment.
			strBTime=new StringBuilder();
			for(int i=0;i<AptCur.Pattern.Length;i++) {
				strBTime.Append(AptCur.Pattern.Substring(i,1));
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==10) {
					i++;
				}
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==15) {
					i++;
					i++;
				}
			}
			comboUnschedStatus.Items.Add(Lan.g(this,"none"));
			comboUnschedStatus.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.RecallUnschedStatus].Length;i++) {
				comboUnschedStatus.Items.Add(DefC.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
				if(DefC.Short[(int)DefCat.RecallUnschedStatus][i].DefNum==AptCur.UnschedStatus)
					comboUnschedStatus.SelectedIndex=i+1;
			}
			for(int i=0;i<DefC.Short[(int)DefCat.ApptConfirmed].Length;i++) {
				comboConfirmed.Items.Add(DefC.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(DefC.Short[(int)DefCat.ApptConfirmed][i].DefNum==AptCur.Confirmed)
					comboConfirmed.SelectedIndex=i;
			}
			checkTimeLocked.Checked=AptCur.TimeLocked;
			textNote.Text=AptCur.Note;
			for(int i=0;i<DefC.Short[(int)DefCat.ApptProcsQuickAdd].Length;i++) {
				listQuickAdd.Items.Add(DefC.Short[(int)DefCat.ApptProcsQuickAdd][i].ItemName);
			}
			comboClinic.Items.Add(Lan.g(this,"none"));
			comboClinic.SelectedIndex=0;
			for(int i=0;i<Clinics.List.Length;i++) {
				comboClinic.Items.Add(Clinics.List[i].Description);
				if(Clinics.List[i].ClinicNum==AptCur.ClinicNum)
					comboClinic.SelectedIndex=i+1;
			}
			for(int i=0;i<ProviderC.List.Length;i++) {
				comboProvNum.Items.Add(ProviderC.List[i].Abbr);
				if(ProviderC.List[i].ProvNum==AptCur.ProvNum)
					comboProvNum.SelectedIndex=i;
			}
			comboProvHyg.Items.Add(Lan.g(this,"none"));
			comboProvHyg.SelectedIndex=0;
			for(int i=0;i<ProviderC.List.Length;i++) {
				comboProvHyg.Items.Add(ProviderC.List[i].Abbr);
				if(ProviderC.List[i].ProvNum==AptCur.ProvHyg)
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
			textTimeArrived.ContextMenu=contextMenuTimeArrived;
			textTimeSeated.ContextMenu=contextMenuTimeSeated;
			textTimeDismissed.ContextMenu=contextMenuTimeDismissed;
			if(AptCur.DateTimeArrived.TimeOfDay>TimeSpan.FromHours(0)){
				textTimeArrived.Text=AptCur.DateTimeArrived.ToShortTimeString();
			}
			if(AptCur.DateTimeSeated.TimeOfDay>TimeSpan.FromHours(0)){
				textTimeSeated.Text=AptCur.DateTimeSeated.ToShortTimeString();
			}
			if(AptCur.DateTimeDismissed.TimeOfDay>TimeSpan.FromHours(0)){
				textTimeDismissed.Text=AptCur.DateTimeDismissed.ToShortTimeString();
			}
			textInsPlan1.Text=InsPlans.GetDescript(AptCur.InsPlan1,fam,PlanList);
			textInsPlan2.Text=InsPlans.GetDescript(AptCur.InsPlan2,fam,PlanList);
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
			if(Programs.IsEnabled("eClinicalWorks") && ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") {
				butComplete.Visible=true;
				//for eCW, we need to hide some things--------------------
				butDelete.Visible=false;
				butPin.Visible=false;
				butTask.Visible=false;
				butAddComm.Visible=false;
				if(HL7Msgs.MessageWasSent(AptCur.AptNum)) {
					butComplete.Text="Revise";
					if(!Security.IsAuthorized(Permissions.Setup,true)) {
						butComplete.Enabled=false;
					}
					butOK.Enabled=false;
					gridProc.Enabled=false;
					listQuickAdd.Enabled=false;
					butAdd.Enabled=false;
					butDeleteProc.Enabled=false;
				}
				else {//hl7 was not sent for this appt
					butComplete.Text="Complete";
					if(Bridges.ECW.AptNum != AptCur.AptNum) {
						butComplete.Enabled=false;
					}
				}
			}
			else {
				butComplete.Visible=false;
			}
			FillProcedures();
			FillPatient();//Must be after FillProcedures(), so that the initial amount for the appointment can be calculated.
			FillTime();
			FillComm();
			textNote.Focus();
			textNote.SelectionStart = 0;
			#if DEBUG
				Text="AptNum"+AptCur.AptNum;
			#endif
		}

		private void butPickDentist_Click(object sender,EventArgs e) {
			FormProviderPick formp=new FormProviderPick();
			if(comboProvNum.SelectedIndex>-1) {
				formp.SelectedProvNum=ProviderC.List[comboProvNum.SelectedIndex].ProvNum;
			}
			formp.ShowDialog();
			if(formp.DialogResult!=DialogResult.OK) {
				return;
			}
			comboProvNum.SelectedIndex=Providers.GetIndex(formp.SelectedProvNum);
		}

		private void butPickHyg_Click(object sender,EventArgs e) {
			FormProviderPick formp=new FormProviderPick();
			if(comboProvHyg.SelectedIndex>0) {
				formp.SelectedProvNum=ProviderC.List[comboProvHyg.SelectedIndex-1].ProvNum;
			}
			formp.ShowDialog();
			if(formp.DialogResult!=DialogResult.OK) {
				return;
			}
			comboProvHyg.SelectedIndex=Providers.GetIndex(formp.SelectedProvNum)+1;
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
			row.Cells.Add(Lan.g(this,"Fee This Appt"));
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
				feeThisAppt+=PIn.Double(gridProc.Rows[gridProc.SelectedIndices[i]].Cells[6].Text);
			}
			gridPatient.Rows[gridPatient.Rows.Count-1].Cells[1].Text=POut.Double(feeThisAppt);
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
					row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][7].ItemColor;
				}
				gridComm.Rows.Add(row);
			}
			gridComm.EndUpdate();
			gridComm.ScrollToEnd();
		}

		private void gridComm_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Commlog item=Commlogs.GetOne(PIn.Long(DS.Tables["Comm"].Rows[e.Row]["CommlogNum"].ToString()));
			FormCommItem FormCI=new FormCommItem(item);
			FormCI.ShowDialog();
			DS.Tables.Remove("Comm");
			DS.Tables.Add(Appointments.GetApptEdit(AptCur.AptNum).Tables["Comm"].Copy());
				//AppointmentL.GetApptEditComm(AptCur.AptNum));
			FillComm();
		}

		private void butAddComm_Click(object sender,EventArgs e) {
			Commlog CommlogCur=new Commlog();
			CommlogCur.PatNum=AptCur.PatNum;
			CommlogCur.CommDateTime=DateTime.Now;
			CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.APPT);
			CommlogCur.UserNum=Security.CurUser.UserNum;
			FormCommItem FormCI=new FormCommItem(CommlogCur);
			FormCI.IsNew=true;
			FormCI.ShowDialog();
			DS.Tables.Remove("Comm");
			DS.Tables.Add(Appointments.GetApptEdit(AptCur.AptNum).Tables["Comm"].Copy());
				//AppointmentL.GetApptEditComm(AptCur.AptNum));
			FillComm();
		}

		private void FillProcedures(){
			gridProc.BeginUpdate();
			gridProc.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableApptProcs","Stat"),35);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Priority"),45);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Tth"),25);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Surf"),50);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Code"),50);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Description"),200);
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
				row.Cells.Add(DS.Tables["Procedure"].Rows[i]["ProcCode"].ToString());
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
			bool isSelected=false;
			for(int i=0;i<gridProc.SelectedIndices.Length;i++){
				if(gridProc.SelectedIndices[i]==e.Row){
					isSelected=true;
				}
			}
			bool isPlanned=AptCur.AptStatus==ApptStatus.Planned;
			List<long> procNums=new List<long>();
			procNums.Add(PIn.Long(DS.Tables["Procedure"].Rows[e.Row]["ProcNum"].ToString()));
			if(isSelected){
				//gridProc.SetSelected(e.Row,false);
				Procedures.DetachFromApt(procNums,isPlanned);
			}
			else{
				//gridProc.SetSelected(e.Row,true);
				Procedures.AttachToApt(procNums,AptCur.AptNum,isPlanned);
			}
			Recalls.Synch(AptCur.PatNum);//Maybe we should move this to the closing event?
			//manually change existing table instead of refreshing from db?
			DS.Tables.Remove("Procedure");
			DS.Tables.Add(Appointments.GetApptEdit(AptCur.AptNum).Tables["Procedure"].Copy());
			FillProcedures();
			CalculateTime();
			FillTime();
			CalcPatientFeeThisAppt();
		}

		private void gridProc_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			long procNum=PIn.Long(DS.Tables["Procedure"].Rows[e.Row]["ProcNum"].ToString());
			Procedure proc=Procedures.GetOneProc(procNum,true);
			FormProcEdit FormP=new FormProcEdit(proc,pat,fam);
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
			DS.Tables.Remove("Procedure");
			DS.Tables.Add(Appointments.GetApptEdit(AptCur.AptNum).Tables["Procedure"].Copy());
			FillProcedures();
			CalculateTime();
			FillTime();
			//make sure the one we double clicked on is highlighted if found
			bool isPlanned=AptCur.AptStatus==ApptStatus.Planned;
			for(int i=0;i<DS.Tables["Procedure"].Rows.Count;i++){
				if(DS.Tables["Procedure"].Rows[i]["attached"].ToString()=="1"){
					//if already attached, skip
					continue;
				}
				if(DS.Tables["Procedure"].Rows[i]["ProcNum"].ToString()==procNum.ToString()){
					Procedures.AttachToApt(procNum,AptCur.AptNum,isPlanned);
					Recalls.Synch(AptCur.PatNum);
					DS.Tables.Remove("Procedure");
					DS.Tables.Add(Appointments.GetApptEdit(AptCur.AptNum).Tables["Procedure"].Copy());
					FillProcedures();
					CalculateTime();
					FillTime();
					break;
				}
			}
			
		}

		/*private void butRemove_Click(object sender,EventArgs e) {
			if(gridProc.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select one or more procedures first.");
				return;
			}
			List<int> procNums=new List<int>();
			for(int i=0;i<gridProc.SelectedIndices.Length;i++){
				procNums.Add(PIn.PInt(DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[i]]["ProcNum"].ToString()));
			}
			bool isPlanned=AptCur.AptStatus==ApptStatus.Planned;
			Procedures.DetachFromApt(procNums,isPlanned);
			Recalls.Synch(AptCur.PatNum);//needs to be moved into Procedures.Delete
			DS.Tables.Remove("Procedure");
			DS.Tables.Add(Appointments.GetApptEdit(AptCur.AptNum).Tables["Procedure"].Clone());
			FillProcedures();
		}*/

		private void butDeleteProc_Click(object sender,EventArgs e) {
			//this button will not be visible if user does not have permission
			if(gridProc.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select one or more procedures first.");
				return;
			}
			if(!MsgBox.Show(this,true,"Permanently delete all selected procedure(s)?")){
				return;
			}
			int skipped=0;
			try{
				for(int i=0;i<gridProc.SelectedIndices.Length;i++){
					if(!Security.IsAuthorized(Permissions.ProcComplEdit,AptCur.AptDateTime.Date,true)) {
						if(DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[i]]["ProcStatus"].ToString()==((int)ProcStat.C).ToString()) {
							skipped++;
							continue;
						}
					}
					//also deletes the claimProcs and adjustments. Might throw exception.
					Procedures.Delete(PIn.Long(DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[i]]["ProcNum"].ToString()));
				}
			}
			catch(Exception ex){
				Recalls.Synch(AptCur.PatNum);//needs to be moved into Procedures.Delete
				DS.Tables.Remove("Procedure");
				DS.Tables.Add(Appointments.GetApptEdit(AptCur.AptNum).Tables["Procedure"].Copy());
				FillProcedures();
				MessageBox.Show(ex.Message);
			}
			Recalls.Synch(AptCur.PatNum);//needs to be moved into Procedures.Delete
			DS.Tables.Remove("Procedure");
			DS.Tables.Add(Appointments.GetApptEdit(AptCur.AptNum).Tables["Procedure"].Copy());
			FillProcedures();
			CalculateTime();
			FillTime();
			if(skipped>0) {
				MessageBox.Show(Lan.g(this,"Procedures skipped due to lack of permission to edit completed procedures: ")+skipped.ToString());
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormProcCodes FormP=new FormProcCodes();
			FormP.IsSelectionMode=true;
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
			Procedure ProcCur;
			ProcCur=new Procedure();//going to be an insert, so no need to set Procedures.CurOld
			ProcCur.CodeNum = FormP.SelectedCodeNum;
			//procnum
			ProcCur.PatNum=AptCur.PatNum;
			//aptnum
			//proccode
			//ProcCur.CodeNum=ProcedureCodes.GetProcCode(ProcCur.OldCode).CodeNum;//already set
			ProcCur.ProcDate=DateTime.Today;
			ProcCur.DateTP=ProcCur.ProcDate;
			//int totUnits = ProcCur.BaseUnits + ProcCur.UnitQty;
			InsPlan priplan=null;
			//Family fam=Patients.GetFamily(AptCur.PatNum);
			//Patient pat=fam.GetPatient(AptCur.PatNum);
			//InsPlan[] planList=InsPlans.Refresh(fam);
			List <PatPlan> patPlanList=PatPlans.Refresh(pat.PatNum);
			if(patPlanList.Count>0) {
				priplan=InsPlans.GetPlan(patPlanList[0].PlanNum,PlanList);
			}
			double insfee=Fees.GetAmount0(ProcCur.CodeNum,Fees.GetFeeSched(pat,PlanList,patPlanList));
			if(priplan!=null && priplan.PlanType=="p") {//PPO
				double standardfee=Fees.GetAmount0(ProcCur.CodeNum,Providers.GetProv(Patients.GetProvNum(pat)).FeeSched);
				if(standardfee>insfee) {
					ProcCur.ProcFee=standardfee;
				}
				else {
					ProcCur.ProcFee=insfee;
				}
			}
			else {
				ProcCur.ProcFee=insfee;
			}
			//surf
			//ToothNum
			//Procedures.Cur.ToothRange
			//ProcCur.NoBillIns=ProcedureCodes.GetProcCode(ProcCur.ProcCode).NoBillIns;
			ProcCur.Priority=0;
			ProcCur.ProcStatus=ProcStat.TP;
			long aptProvNum=ProviderC.List[0].ProvNum;
			if(comboProvNum.SelectedIndex!=-1) {
				aptProvNum=ProviderC.List[comboProvNum.SelectedIndex].ProvNum;
			}
			long aptProvHyg=0;
			if(comboProvHyg.SelectedIndex>0) {
				aptProvHyg=ProviderC.List[comboProvHyg.SelectedIndex-1].ProvNum;
			}
			if(ProcedureCodes.GetProcCode(ProcCur.CodeNum).IsHygiene
				&& aptProvHyg != 0)
			{
				ProcCur.ProvNum=aptProvHyg;
			}
			else{
				ProcCur.ProvNum=aptProvNum;
			}
			ProcCur.Note="";
			ProcCur.ClinicNum=pat.ClinicNum;
			//dx
			//nextaptnum
			ProcCur.DateEntryC=DateTime.Now;
			ProcCur.MedicalCode=ProcedureCodes.GetProcCode(ProcCur.CodeNum).MedicalCode;
			ProcCur.BaseUnits=ProcedureCodes.GetProcCode(ProcCur.CodeNum).BaseUnits;
			ProcCur.SiteNum=pat.SiteNum;
			Procedures.Insert(ProcCur);
			List <Benefit> benefitList=Benefits.Refresh(patPlanList);
			Procedures.ComputeEstimates(ProcCur,pat.PatNum,new List<ClaimProc>(),true,PlanList,patPlanList,benefitList,pat.Age);
			FormProcEdit FormPE=new FormProcEdit(ProcCur,pat.Copy(),fam);
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.Cancel){
				//any created claimprocs are automatically deleted from within procEdit window.
				try{
					Procedures.Delete(ProcCur.ProcNum);//also deletes the claimprocs
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
				}
			}
			else{
				//not needed because always TP
				//Recalls.Synch(PatCur.PatNum);
			}
			/*
			FormApptProcs FormAP=new FormApptProcs();
			FormAP.AptCur=AptCur.Clone();
			//but we do need the status to be accurate:
			if (AptCur.AptStatus == ApptStatus.Planned) {
				;
			}
			else if(comboStatus.SelectedIndex==-1) {
				FormAP.AptCur.AptStatus=ApptStatus.Scheduled;
			}
			else if (AptCur.AptStatus == ApptStatus.PtNote | AptCur.AptStatus == ApptStatus.PtNoteCompleted){
				FormAP.AptCur.AptStatus = (ApptStatus)comboStatus.SelectedIndex + 7;
			}
			else {
				FormAP.AptCur.AptStatus=(ApptStatus)comboStatus.SelectedIndex+1;
			}
			FormAP.ShowDialog();
			if(FormAP.DialogResult!=DialogResult.OK){
				return;
			}*/
			bool isPlanned=AptCur.AptStatus==ApptStatus.Planned;
			Procedures.AttachToApt(ProcCur.ProcNum,AptCur.AptNum,isPlanned);
			Recalls.Synch(AptCur.PatNum);//might not be needed because TP?
			DS.Tables.Remove("Procedure");
			DS.Tables.Add(Appointments.GetApptEdit(AptCur.AptNum).Tables["Procedure"].Copy());
			FillProcedures();
			CalculateTime();
			FillTime();
		}

		private void FillTime() {
			Color provColor=Color.Gray;
			if(comboProvNum.SelectedIndex!=-1) {
				provColor=ProviderC.List[comboProvNum.SelectedIndex].ProvColor;
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
			if(checkTimeLocked.Checked){
				return;
			}
			//We are using the providers selected for the appt rather than the providers for the procs.
			//Providers for the procs get reset when closing this form.
			long provDent=Patients.GetProvNum(pat);
			long provHyg=Patients.GetProvNum(pat);
			if(comboProvNum.SelectedIndex!=-1){
				provDent=ProviderC.List[comboProvNum.SelectedIndex].ProvNum;
				provHyg=ProviderC.List[comboProvNum.SelectedIndex].ProvNum;
			}
			if(comboProvHyg.SelectedIndex!=0) {
				provHyg=ProviderC.List[comboProvHyg.SelectedIndex-1].ProvNum;
			}
			List<long> codeNums=new List<long>();
			for(int i=0;i<gridProc.SelectedIndices.Length;i++) {
				codeNums.Add(PIn.Long(DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[i]]["CodeNum"].ToString()));
			}
			strBTime=new StringBuilder(Appointments.CalculatePattern(provDent,provHyg,codeNums,false));
			Plugins.HookAddCode(this,"FormApptEdit.CalculateTime_end",strBTime);//set strBTime, but without using the 'new' keyword.
		}

		private void checkTimeLocked_Click(object sender,EventArgs e) {
			CalculateTime();
			FillTime();
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
			mouseOrigin=new Point(e.X+butSlider.Location.X,e.Y+butSlider.Location.Y);
			sliderOrigin=butSlider.Location;
		}

		private void butSlider_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(!mouseIsDown){
				return;
			}
			//tempPoint represents the new location of button of smooth dragging.
			Point tempPoint=new Point(sliderOrigin.X,sliderOrigin.Y+(e.Y+butSlider.Location.Y)-mouseOrigin.Y);
			int step=(int)(Math.Round((Decimal)(tempPoint.Y-tbTime.Location.Y)/14));
			if(step==strBTime.Length){
				return;
			}
			if(step<1){
				return;
			}
			if(step>tbTime.MaxRows-1){
				return;
			}
			if(step>strBTime.Length) {
				strBTime.Append('/');
			}
			if(step<strBTime.Length) {
				strBTime.Remove(step,1);
			}
			checkTimeLocked.Checked=true;
			FillTime();
		}

		private void butSlider_MouseUp(object sender,System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=false;
		}
		
		private void gridComm_MouseMove(object sender,MouseEventArgs e) {
			
		}

		private void gridPatient_MouseMove(object sender,MouseEventArgs e) {
			
		}

		private void listQuickAdd_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(listQuickAdd.IndexFromPoint(e.X,e.Y)==-1) {
				return;
			}
			if(AptCur.AptStatus==ApptStatus.Complete) {
				//added procedures would be marked complete when form closes. We'll just stop it here.
				if(!Security.IsAuthorized(Permissions.ProcComplCreate)) {
					return;
				}
			}
			Procedures.SetDateFirstVisit(AptCur.AptDateTime.Date,1,pat);
			List <PatPlan> PatPlanList=PatPlans.Refresh(AptCur.PatNum);
			List <Benefit> benefitList=Benefits.Refresh(PatPlanList);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(AptCur.PatNum);
			string[] codes=DefC.Short[(int)DefCat.ApptProcsQuickAdd][listQuickAdd.IndexFromPoint(e.X,e.Y)].ItemValue.Split(',');
			for(int i=0;i<codes.Length;i++) {
				if(!ProcedureCodeC.HList.ContainsKey(codes[i])){
					MsgBox.Show(this,"Definition contains invalid code.");
					return;
				}
			}
			for(int i=0;i<codes.Length;i++) {
				Procedure ProcCur=new Procedure();
				ProcCur.PatNum=AptCur.PatNum;
				if(AptCur.AptStatus!=ApptStatus.Planned){
					ProcCur.AptNum=AptCur.AptNum;
				}
				ProcCur.CodeNum=ProcedureCodes.GetProcCode(codes[i]).CodeNum;
				ProcCur.ProcDate=AptCur.AptDateTime.Date;
				ProcCur.DateTP=AptCur.AptDateTime.Date;
				InsPlan priplan=null;
				if(PatPlanList.Count>0) {
					priplan=InsPlans.GetPlan(PatPlanList[0].PlanNum,PlanList);
				}
				double insfee=Fees.GetAmount0(ProcCur.CodeNum,Fees.GetFeeSched(pat,PlanList,PatPlanList));
				if(priplan!=null && priplan.PlanType=="p") {//PPO
					double standardfee=Fees.GetAmount0(ProcCur.CodeNum,Providers.GetProv(Patients.GetProvNum(pat)).FeeSched);
					if(standardfee>insfee) {
						ProcCur.ProcFee=standardfee;
					}
					else {
						ProcCur.ProcFee=insfee;
					}
				}
				else {
					ProcCur.ProcFee=insfee;
				}
				//surf
				//toothnum
				//toothrange
				//priority
				ProcCur.ProcStatus=ProcStat.TP;
				//procnote
				ProcCur.ProvNum=AptCur.ProvNum;
				//Dx
				ProcCur.ClinicNum=AptCur.ClinicNum;
				ProcCur.SiteNum=pat.SiteNum;
				if(AptCur.AptStatus==ApptStatus.Planned){
					ProcCur.PlannedAptNum=AptCur.AptNum;
				}
				ProcCur.MedicalCode=ProcedureCodes.GetProcCode(ProcCur.CodeNum).MedicalCode;
				ProcCur.BaseUnits=ProcedureCodes.GetProcCode(ProcCur.CodeNum).BaseUnits;
				Procedures.Insert(ProcCur);//recall synch not required
				Procedures.ComputeEstimates(ProcCur,pat.PatNum,ClaimProcList,false,PlanList,PatPlanList,benefitList,pat.Age);
			}
			listQuickAdd.SelectedIndex=-1;
			string[] selectedProcs=new string[gridProc.SelectedIndices.Length];
			for(int i=0;i<selectedProcs.Length;i++){
				selectedProcs[i]=DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[i]]["ProcNum"].ToString();
			}
			DS.Tables.Remove("Procedure");
			DS.Tables.Add(Appointments.GetApptEdit(AptCur.AptNum).Tables["Procedure"].Copy());
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
				FormLCE.CaseCur=LabCases.GetOne(PIn.Long(DS.Tables["Misc"].Rows[0]["LabCaseNum"].ToString()));
				FormLCE.ShowDialog();
				if(FormLCE.DialogResult!=DialogResult.OK){
					return;
				}
				//Deleting or detaching labcase would have been done from in that window
			}
			DS.Tables.Remove("Misc");
			DS.Tables.Add(Appointments.GetApptEdit(AptCur.AptNum).Tables["Misc"].Copy());
			textLabCase.Text=DS.Tables["Misc"].Rows[0]["labDescript"].ToString();
		}

		private void butInsPlan1_Click(object sender,EventArgs e) {
			FormInsPlanSelect FormIPS=new FormInsPlanSelect(AptCur.PatNum);
			FormIPS.ShowNoneButton=true;
			FormIPS.ViewRelat=false;
			FormIPS.ShowDialog();
			if(FormIPS.DialogResult!=DialogResult.OK) {
				return;
			}
			if(FormIPS.SelectedPlan==null) {
				AptCur.InsPlan1=0;
				textInsPlan1.Text="";
				return;
			}
			AptCur.InsPlan1=FormIPS.SelectedPlan.PlanNum;
			textInsPlan1.Text=InsPlans.GetDescript(AptCur.InsPlan1,fam,PlanList);
		}

		private void butInsPlan2_Click(object sender,EventArgs e) {
			FormInsPlanSelect FormIPS=new FormInsPlanSelect(AptCur.PatNum);
			FormIPS.ShowNoneButton=true;
			FormIPS.ViewRelat=false;
			FormIPS.ShowDialog();
			if(FormIPS.DialogResult!=DialogResult.OK) {
				return;
			}
			if(FormIPS.SelectedPlan==null) {
				AptCur.InsPlan2=0;
				textInsPlan2.Text="";
				return;
			}
			AptCur.InsPlan2=FormIPS.SelectedPlan.PlanNum;
			textInsPlan2.Text=InsPlans.GetDescript(AptCur.InsPlan2,fam,PlanList);
		}

		private void butRequirement_Click(object sender,EventArgs e) {
			FormReqAppt FormR=new FormReqAppt();
			FormR.AptNum=AptCur.AptNum;
			FormR.PatNum=AptCur.PatNum;
			FormR.ShowDialog();
			if(FormR.DialogResult!=DialogResult.OK){
				return;
			}
			DS.Tables.Remove("Misc");
			DS.Tables.Add(Appointments.GetApptEdit(AptCur.AptNum).Tables["Misc"].Copy());
			textRequirement.Text=DS.Tables["Misc"].Rows[0]["requirements"].ToString();
		}

		private void menuItemArrivedNow_Click(object sender,EventArgs e) {
			textTimeArrived.Text=DateTime.Now.ToShortTimeString();
		}

		private void menuItemSeatedNow_Click(object sender,EventArgs e) {
			textTimeSeated.Text=DateTime.Now.ToShortTimeString();
		}

		private void menuItemDismissedNow_Click(object sender,EventArgs e) {
			textTimeDismissed.Text=DateTime.Now.ToShortTimeString();
		}

		///<summary>Called from butOK_Click and butPin_Click</summary>
		private bool UpdateToDB(){
			DateTime dateTimeArrived=AptCur.AptDateTime.Date;
			if(textTimeArrived.Text!=""){
				try{
					dateTimeArrived=AptCur.AptDateTime.Date+DateTime.Parse(textTimeArrived.Text).TimeOfDay;
				}
				catch{
					MsgBox.Show(this,"Time Arrived invalid.");
					return false;
				}
			}
			DateTime dateTimeSeated=AptCur.AptDateTime.Date;
			if(textTimeSeated.Text!=""){
				try{
					dateTimeSeated=AptCur.AptDateTime.Date+DateTime.Parse(textTimeSeated.Text).TimeOfDay;
				}
				catch{
					MsgBox.Show(this,"Time Seated invalid.");
					return false;
				}
			}
			DateTime dateTimeDismissed=AptCur.AptDateTime.Date;
			if(textTimeDismissed.Text!=""){
				try{
					dateTimeDismissed=AptCur.AptDateTime.Date+DateTime.Parse(textTimeDismissed.Text).TimeOfDay;
				}
				catch{
					MsgBox.Show(this,"Time Arrived invalid.");
					return false;
				}
			}
			//This change was just slightly too risky to make to 6.9, so 7.0 only
			if(AptCur.AptStatus!=ApptStatus.Complete//was not originally complete
				&& AptCur.AptStatus!=ApptStatus.PtNote
				&& AptCur.AptStatus!=ApptStatus.PtNoteCompleted
				&& comboStatus.SelectedIndex==1 //making it complete
				&& AptCur.AptDateTime.Date > DateTime.Today)//and future appt
			{
				MsgBox.Show(this,"Not allowed to set complete future appointments.");
				return false;
			}
			if(AptCur.AptStatus == ApptStatus.Planned) {
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
			AptCur.Pattern=Appointments.ConvertPatternTo5(strBTime.ToString());
			if(comboUnschedStatus.SelectedIndex==0){//none
				AptCur.UnschedStatus=0;
			}
			else{
				AptCur.UnschedStatus=DefC.Short[(int)DefCat.RecallUnschedStatus][comboUnschedStatus.SelectedIndex-1].DefNum;
			}
			if(comboConfirmed.SelectedIndex!=-1){
				AptCur.Confirmed=DefC.Short[(int)DefCat.ApptConfirmed][comboConfirmed.SelectedIndex].DefNum;
			}
			AptCur.TimeLocked=checkTimeLocked.Checked;
			AptCur.Note=textNote.Text;
			if(comboClinic.SelectedIndex==0) {//none
				AptCur.ClinicNum=0;
			}
			else {
				AptCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			}
			//there should always be a non-hidden primary provider for an appt.
			if(comboProvNum.SelectedIndex==-1) {
				AptCur.ProvNum=ProviderC.List[0].ProvNum;
			}
			else {
				AptCur.ProvNum=ProviderC.List[comboProvNum.SelectedIndex].ProvNum;
			}
			if(comboProvHyg.SelectedIndex==0) {//none
				AptCur.ProvHyg=0;
			}
			else {
				AptCur.ProvHyg=ProviderC.List[comboProvHyg.SelectedIndex-1].ProvNum;
			}
			AptCur.IsHygiene=checkIsHygiene.Checked;
			if(comboAssistant.SelectedIndex==0) {//none
				AptCur.Assistant=0;
			}
			else {
				AptCur.Assistant=Employees.ListShort[comboAssistant.SelectedIndex-1].EmployeeNum;
			}
			AptCur.IsNewPatient=checkIsNewPatient.Checked;
			AptCur.DateTimeArrived=dateTimeArrived;
			AptCur.DateTimeSeated=dateTimeSeated;
			AptCur.DateTimeDismissed=dateTimeDismissed;
			//AptCur.InsPlan1 and InsPlan2 already handled 
			AptCur.ProcDescript="";
			for(int i=0;i<gridProc.SelectedIndices.Length;i++) {
				if(i>0){
					AptCur.ProcDescript+=", ";
				}
				AptCur.ProcDescript+=ProcedureCodes.GetProcCode(
					PIn.Long(DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[i]]["CodeNum"].ToString())).AbbrDesc;
			}
			//int[] procNums=new int[gridProc.SelectedIndices.Length];
			//for(int i=0;i<procNums.Length;i++){
			//	procNums[i]=PIn.PInt(DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[i]]["ProcNum"].ToString());
			//}
			bool isPlanned=AptCur.AptStatus==ApptStatus.Planned;
			try {
				Appointments.Update(AptCur,AptOld);
				//Appointments.UpdateAttached(AptCur.AptNum,procNums,isPlanned);
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
					if(!Security.IsAuthorized(Permissions.ProcComplCreate,AptCur.AptDateTime)) {
						return false;
					}
					List <PatPlan> PatPlanList=PatPlans.Refresh(AptCur.PatNum);
					ProcedureL.SetCompleteInAppt(AptCur,PlanList,PatPlanList,pat.SiteNum,pat.Age);
					SecurityLogs.MakeLogEntry(Permissions.ProcComplCreate,pat.PatNum,
						pat.GetNameLF()+" "+AptCur.AptDateTime.ToShortDateString());
				}
			}
			else{
				Procedures.SetProvidersInAppointment(AptCur,Procedures.GetProcsForSingle(AptCur.AptNum,false));
			}
			return true;
		}

		private void butComplete_Click(object sender,EventArgs e) {
			//This is only used with eCW.
			if(butComplete.Text=="Complete") {
				//user can only get this far if aptNum matches visit num previously passed in by eCW.
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Send attached procedures to eClinicalWorks and exit?")) {
					return;
				}
				if(!UpdateToDB()) {
					return;
				}
				Bridges.ECW.SendHL7(AptCur,pat);
				CloseOD=true;
				if(IsNew) {
					SecurityLogs.MakeLogEntry(Permissions.AppointmentCreate,pat.PatNum,pat.GetNameLF()+", "
					+AptCur.AptDateTime.ToString()+", "
					+AptCur.ProcDescript);
				}
				DialogResult=DialogResult.OK;
			}
			else if(butComplete.Text=="Revise") {
				MsgBox.Show(this,"Any changes that you make will not be sent to eCW.  You will also have to make the same changes in eCW.");
				//revise is only clickable if user has permission
				butOK.Enabled=true;
				gridProc.Enabled=true;
				listQuickAdd.Enabled=true;
				butAdd.Enabled=true;
				butDeleteProc.Enabled=true;
			}
		}

		private void butAudit_Click(object sender,EventArgs e) {
			List<Permissions> perms=new List<Permissions>();
			perms.Add(Permissions.AppointmentCreate);
			perms.Add(Permissions.AppointmentEdit);
			perms.Add(Permissions.AppointmentMove);
			FormAuditOneType FormA=new FormAuditOneType(pat.PatNum,perms,Lan.g(this,"All Appointments for")+pat.GetNameFL());
			FormA.ShowDialog();
		}

		private void butTask_Click(object sender,EventArgs e) {
			if(!UpdateToDB())
				return;
			FormTaskListSelect FormT=new FormTaskListSelect(TaskObjectType.Appointment,AptCur.AptNum);
			FormT.ShowDialog();
		}

		private void butPin_Click(object sender,System.EventArgs e) {
			if(!UpdateToDB())
				return;
			PinClicked=true;
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if (AptCur.AptStatus == ApptStatus.PtNote || AptCur.AptStatus == ApptStatus.PtNoteCompleted) {
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
			else {//ordinary appointment
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
			Appointments.Delete(AptCur.AptNum);
			SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,pat.PatNum,
				"Delete for patient: "
				+pat.GetNameLF()+", "
				+AptCur.AptDateTime.ToString());
			if(IsNew){
				//The dialog is considered cancelled when a new appointment is immediately deleted.
			  DialogResult=DialogResult.Cancel;
			}
			else{
				DialogResult=DialogResult.OK;
			}
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
			if(DialogResult==DialogResult.OK){
				return;
			}
			if(IsNew) {
				Appointments.Delete(AptCur.AptNum);
			}
		}

		

		

		

		

		
		

		

	

	
	}
}








