using System;
using System.Data;
using System.Drawing;
using System.Collections;
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
		private ComboBox comboAssistant;
		private ComboBox comboProvHyg;
		private ComboBox comboProvNum;
		private Label label13;
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
		private Label label7;
		private OpenDental.UI.Button butAddComm;
		private Label label8;
		private ListBox listQuickAdd;
		public bool PinIsVisible;
		public bool PinClicked;
		private Panel panel1;
		private GroupBox groupDentalSchools;
		private TextBox textGradePoint;
		private ComboBox comboSchoolCourse;
		private ComboBox comboSchoolClass;
		private ComboBox comboInstructor;
		private Label label20;
		private Label label21;
		private Label label22;
		private Label label23;
		private TextBox textLab;
		public bool IsNew;
		private DataSet DS;
		private Appointment AptCur;
		private Appointment AptOld;
		///<summary>The string time pattern in the current increment. Not in the 5 minute increment.</summary>
		private StringBuilder strBTime;

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
			this.panel1 = new System.Windows.Forms.Panel();
			this.textLab = new System.Windows.Forms.TextBox();
			this.groupDentalSchools = new System.Windows.Forms.GroupBox();
			this.textGradePoint = new System.Windows.Forms.TextBox();
			this.comboSchoolCourse = new System.Windows.Forms.ComboBox();
			this.comboSchoolClass = new System.Windows.Forms.ComboBox();
			this.comboInstructor = new System.Windows.Forms.ComboBox();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
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
			this.panel1.SuspendLayout();
			this.groupDentalSchools.SuspendLayout();
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
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(17,185);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(98,16);
			this.label13.TabIndex = 130;
			this.label13.Text = "Lab Case";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(23,247);
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
			this.label8.Text = "Single click on items in the list below to add them to the treatment plan.";
			// 
			// listQuickAdd
			// 
			this.listQuickAdd.IntegralHeight = false;
			this.listQuickAdd.Location = new System.Drawing.Point(338,42);
			this.listQuickAdd.Name = "listQuickAdd";
			this.listQuickAdd.Size = new System.Drawing.Size(146,306);
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
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.AutoScrollMargin = new System.Drawing.Size(0,3);
			this.panel1.Controls.Add(this.textLab);
			this.panel1.Controls.Add(this.groupDentalSchools);
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
			this.panel1.Controls.Add(this.label13);
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
			// textLab
			// 
			this.textLab.Location = new System.Drawing.Point(118,184);
			this.textLab.Name = "textLab";
			this.textLab.ReadOnly = true;
			this.textLab.Size = new System.Drawing.Size(126,20);
			this.textLab.TabIndex = 140;
			// 
			// groupDentalSchools
			// 
			this.groupDentalSchools.Controls.Add(this.textGradePoint);
			this.groupDentalSchools.Controls.Add(this.comboSchoolCourse);
			this.groupDentalSchools.Controls.Add(this.comboSchoolClass);
			this.groupDentalSchools.Controls.Add(this.comboInstructor);
			this.groupDentalSchools.Controls.Add(this.label20);
			this.groupDentalSchools.Controls.Add(this.label21);
			this.groupDentalSchools.Controls.Add(this.label22);
			this.groupDentalSchools.Controls.Add(this.label23);
			this.groupDentalSchools.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupDentalSchools.Location = new System.Drawing.Point(17,204);
			this.groupDentalSchools.Name = "groupDentalSchools";
			this.groupDentalSchools.Size = new System.Drawing.Size(228,101);
			this.groupDentalSchools.TabIndex = 139;
			this.groupDentalSchools.TabStop = false;
			this.groupDentalSchools.Text = "Dental Schools";
			// 
			// textGradePoint
			// 
			this.textGradePoint.Location = new System.Drawing.Point(93,75);
			this.textGradePoint.Name = "textGradePoint";
			this.textGradePoint.Size = new System.Drawing.Size(63,20);
			this.textGradePoint.TabIndex = 90;
			// 
			// comboSchoolCourse
			// 
			this.comboSchoolCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSchoolCourse.Location = new System.Drawing.Point(93,54);
			this.comboSchoolCourse.MaxDropDownItems = 30;
			this.comboSchoolCourse.Name = "comboSchoolCourse";
			this.comboSchoolCourse.Size = new System.Drawing.Size(130,21);
			this.comboSchoolCourse.TabIndex = 89;
			// 
			// comboSchoolClass
			// 
			this.comboSchoolClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSchoolClass.Location = new System.Drawing.Point(93,33);
			this.comboSchoolClass.MaxDropDownItems = 30;
			this.comboSchoolClass.Name = "comboSchoolClass";
			this.comboSchoolClass.Size = new System.Drawing.Size(130,21);
			this.comboSchoolClass.TabIndex = 88;
			// 
			// comboInstructor
			// 
			this.comboInstructor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboInstructor.Location = new System.Drawing.Point(93,12);
			this.comboInstructor.MaxDropDownItems = 30;
			this.comboInstructor.Name = "comboInstructor";
			this.comboInstructor.Size = new System.Drawing.Size(130,21);
			this.comboInstructor.TabIndex = 87;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(9,78);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(82,16);
			this.label20.TabIndex = 86;
			this.label20.Text = "Grade";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(9,58);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(82,16);
			this.label21.TabIndex = 85;
			this.label21.Text = "Course";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(9,36);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(82,16);
			this.label22.TabIndex = 84;
			this.label22.Text = "Class";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(9,16);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(82,16);
			this.label23.TabIndex = 83;
			this.label23.Text = "Instructor";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.textNote.Location = new System.Drawing.Point(24,264);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Appointment;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(312,85);
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
			this.gridProc.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
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
			// FormApptEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(974,695);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label6);
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
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupDentalSchools.ResumeLayout(false);
			this.groupDentalSchools.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormApptEdit_Load(object sender, System.EventArgs e){
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
			if(PrefB.GetBool("EasyHideDentalSchools")) {
				groupDentalSchools.Visible=false;
			}
			if(PrefB.GetBool("EasyNoClinics")) {
				labelClinic.Visible=false;
				comboClinic.Visible=false;
			}
			if(!PinIsVisible){
				butPin.Visible=false;
			}
			if(AptCur.AptStatus==ApptStatus.Planned) {
				Text=Lan.g(this,"Edit Planned Appointment")+" - "+DS.Tables["Patient"].Rows[0]["nameLF"].ToString();
				labelStatus.Visible=false;
				comboStatus.Visible=false;
				butDelete.Visible=false;
			}
			else {
				Text=Lan.g(this,"Edit Appointment")+" - "+DS.Tables["Patient"].Rows[0]["nameLF"].ToString();
				comboStatus.Items.Add(Lan.g("enumApptStatus","Scheduled"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","Complete"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","UnschedList"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","ASAP"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","Broken"));
				comboStatus.SelectedIndex=(int)AptCur.AptStatus-1;
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
			//SchoolClassNum must be filled before provider
			comboSchoolClass.Items.Add(Lan.g(this,"none"));
			for(int i=0;i<SchoolClasses.List.Length;i++) {
				comboSchoolClass.Items.Add(SchoolClasses.List[i].GradYear.ToString()+"-"+SchoolClasses.List[i].Descript);
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
			/*string[] enumLab=Enum.GetNames(typeof(LabCaseOld));
			for(int i=0;i<enumLab.Length;i++) {
				comboLab.Items.Add(Lan.g("enumLab",enumLab[i]));
			}
			comboLab.SelectedIndex=(int)AptCur.Lab;*/
			comboInstructor.Items.Add(Lan.g(this,"none"));
			comboInstructor.SelectedIndex=0;
			for(int i=0;i<Instructors.List.Length;i++) {
				comboInstructor.Items.Add(Instructors.List[i].LName+", "+Instructors.List[i].FName+", "+Instructors.List[i].Suffix);
				if(Instructors.List[i].InstructorNum==AptCur.InstructorNum)
					comboInstructor.SelectedIndex=i+1;
			}
			//SchoolClassNum was filled earlier.  Now selected:
			comboSchoolClass.SelectedIndex=0;
			for(int i=0;i<SchoolClasses.List.Length;i++) {
				if(SchoolClasses.List[i].SchoolClassNum==AptCur.SchoolClassNum)
					comboSchoolClass.SelectedIndex=i+1;
			}
			comboSchoolCourse.Items.Add(Lan.g(this,"none"));
			comboSchoolCourse.SelectedIndex=0;
			for(int i=0;i<SchoolCourses.List.Length;i++) {
				comboSchoolCourse.Items.Add(SchoolCourses.List[i].CourseID+"  "+SchoolCourses.List[i].Descript);
				if(SchoolCourses.List[i].SchoolCourseNum==AptCur.SchoolCourseNum)
					comboSchoolCourse.SelectedIndex=i+1;
			}
			textGradePoint.Text=AptCur.GradePoint.ToString();
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
			FillPatient();
			FillProcedures();
			//FillTime();
			//FillComm();
		}

		private void FillPatient(){
			/*textHmPhone.Text=pat.HmPhone;
			textWkPhone.Text=pat.WkPhone;
			textWirelessPhone.Text=pat.WirelessPhone;
			textAddrNote.Text=pat.AddrNote;
			textCreditType.Text=pat.CreditType;
			textBillingType.Text=DefB.GetName(DefCat.BillingTypes,pat.BillingType);
			textBalance.Text=pat.EstBalance.ToString("F");
			textFamilyBal.Text=fam.List[0].BalTotal.ToString("F");*/
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

		///<summary>Called from butOK_Click and butPin_Click</summary>
		private bool UpdateToDB(){
			AptCur.IsNewPatient=checkIsNewPatient.Checked;
			try {
				Appointments.Update(AptCur,AptOld);
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);
				return false;
			}
			return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!UpdateToDB()){
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

	

		


	}
}





















