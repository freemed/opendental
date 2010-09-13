using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormChartView : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.ODGrid gridMain;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butDefault;
		private Label label2;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butUp;
		private ListBox listAvailable;
		private Label label3;
		private OpenDental.UI.Button butRight;
		private OpenDental.UI.Button butLeft;
		private bool changed;
		private OpenDental.UI.Button butOK;
		private List<DisplayField> ListShowing;
		private CheckBox checkShowOnlyFilmsAndExams;
		private CheckBox checkShowOnlyHygieneProcs;
		private GroupBox groupBox7;
		private CheckBox checkSheets;
		private CheckBox checkTasks;
		private CheckBox checkEmail;
		private CheckBox checkCommFamily;
		private CheckBox checkAppt;
		private CheckBox checkLabCase;
		private CheckBox checkRx;
		private CheckBox checkComm;
		private GroupBox groupBox6;
		private CheckBox checkShowCn;
		private CheckBox checkShowE;
		private CheckBox checkShowR;
		private CheckBox checkShowC;
		private CheckBox checkShowTP;
		private CheckBox checkShowTeeth;
		private CheckBox checkNotes;
		private CheckBox checkAudit;
		private OpenDental.UI.Button butShowAll;
		private OpenDental.UI.Button butShowNone;
		private TextBox textBoxViewDesc;
		private Label labelDescription;
		private GroupBox groupBoxProperties;
		//private List<DisplayField> ListAvailable;
		public DisplayFieldCategory category;
		private OpenDental.UI.Button butDelete;
		public ChartView ChartViewCur;

		///<summary></summary>
		public FormChartView()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChartView));
			this.label2 = new System.Windows.Forms.Label();
			this.listAvailable = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butRight = new OpenDental.UI.Button();
			this.butLeft = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butDefault = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butCancel = new OpenDental.UI.Button();
			this.checkShowOnlyFilmsAndExams = new System.Windows.Forms.CheckBox();
			this.checkShowOnlyHygieneProcs = new System.Windows.Forms.CheckBox();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.checkSheets = new System.Windows.Forms.CheckBox();
			this.checkTasks = new System.Windows.Forms.CheckBox();
			this.checkEmail = new System.Windows.Forms.CheckBox();
			this.checkCommFamily = new System.Windows.Forms.CheckBox();
			this.checkAppt = new System.Windows.Forms.CheckBox();
			this.checkLabCase = new System.Windows.Forms.CheckBox();
			this.checkRx = new System.Windows.Forms.CheckBox();
			this.checkComm = new System.Windows.Forms.CheckBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.checkShowCn = new System.Windows.Forms.CheckBox();
			this.checkShowE = new System.Windows.Forms.CheckBox();
			this.checkShowR = new System.Windows.Forms.CheckBox();
			this.checkShowC = new System.Windows.Forms.CheckBox();
			this.checkShowTP = new System.Windows.Forms.CheckBox();
			this.checkShowTeeth = new System.Windows.Forms.CheckBox();
			this.checkNotes = new System.Windows.Forms.CheckBox();
			this.checkAudit = new System.Windows.Forms.CheckBox();
			this.butShowAll = new OpenDental.UI.Button();
			this.butShowNone = new OpenDental.UI.Button();
			this.textBoxViewDesc = new System.Windows.Forms.TextBox();
			this.labelDescription = new System.Windows.Forms.Label();
			this.groupBoxProperties = new System.Windows.Forms.GroupBox();
			this.butDelete = new OpenDental.UI.Button();
			this.groupBox7.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBoxProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(126,286);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(213,25);
			this.label2.TabIndex = 5;
			this.label2.Text = "Sets entire list to the default.";
			// 
			// listAvailable
			// 
			this.listAvailable.FormattingEnabled = true;
			this.listAvailable.IntegralHeight = false;
			this.listAvailable.Location = new System.Drawing.Point(388,327);
			this.listAvailable.Name = "listAvailable";
			this.listAvailable.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listAvailable.Size = new System.Drawing.Size(158,412);
			this.listAvailable.TabIndex = 15;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(385,307);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(158,17);
			this.label3.TabIndex = 16;
			this.label3.Text = "Available Fields";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(390,795);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 56;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butRight
			// 
			this.butRight.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRight.Autosize = true;
			this.butRight.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRight.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRight.CornerRadius = 4F;
			this.butRight.Image = global::OpenDental.Properties.Resources.Right;
			this.butRight.Location = new System.Drawing.Point(335,530);
			this.butRight.Name = "butRight";
			this.butRight.Size = new System.Drawing.Size(35,24);
			this.butRight.TabIndex = 55;
			this.butRight.Click += new System.EventHandler(this.butRight_Click);
			// 
			// butLeft
			// 
			this.butLeft.AdjustImageLocation = new System.Drawing.Point(-1,0);
			this.butLeft.Autosize = true;
			this.butLeft.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLeft.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLeft.CornerRadius = 4F;
			this.butLeft.Image = global::OpenDental.Properties.Resources.Left;
			this.butLeft.Location = new System.Drawing.Point(335,490);
			this.butLeft.Name = "butLeft";
			this.butLeft.Size = new System.Drawing.Size(35,24);
			this.butLeft.TabIndex = 54;
			this.butLeft.Click += new System.EventHandler(this.butLeft_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(124,745);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(82,24);
			this.butDown.TabIndex = 14;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(27,745);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(82,24);
			this.butUp.TabIndex = 13;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butDefault
			// 
			this.butDefault.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDefault.Autosize = true;
			this.butDefault.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDefault.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDefault.CornerRadius = 4F;
			this.butDefault.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDefault.Location = new System.Drawing.Point(27,280);
			this.butDefault.Name = "butDefault";
			this.butDefault.Size = new System.Drawing.Size(91,24);
			this.butDefault.TabIndex = 4;
			this.butDefault.Text = "Set to Default";
			this.butDefault.Click += new System.EventHandler(this.butDefault_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(27,314);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(292,425);
			this.gridMain.TabIndex = 3;
			this.gridMain.Title = "Fields Showing";
			this.gridMain.TranslationName = "FormDisplayFields";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(471,795);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 57;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// checkShowOnlyFilmsAndExams
			// 
			this.checkShowOnlyFilmsAndExams.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowOnlyFilmsAndExams.Location = new System.Drawing.Point(16,151);
			this.checkShowOnlyFilmsAndExams.Name = "checkShowOnlyFilmsAndExams";
			this.checkShowOnlyFilmsAndExams.Size = new System.Drawing.Size(104,30);
			this.checkShowOnlyFilmsAndExams.TabIndex = 66;
			this.checkShowOnlyFilmsAndExams.Text = "Show Only Films and Exams";
			this.checkShowOnlyFilmsAndExams.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// checkShowOnlyHygieneProcs
			// 
			this.checkShowOnlyHygieneProcs.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowOnlyHygieneProcs.Location = new System.Drawing.Point(16,136);
			this.checkShowOnlyHygieneProcs.Name = "checkShowOnlyHygieneProcs";
			this.checkShowOnlyHygieneProcs.Size = new System.Drawing.Size(120,13);
			this.checkShowOnlyHygieneProcs.TabIndex = 65;
			this.checkShowOnlyHygieneProcs.Text = "Show Only Hygiene";
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.checkSheets);
			this.groupBox7.Controls.Add(this.checkTasks);
			this.groupBox7.Controls.Add(this.checkEmail);
			this.groupBox7.Controls.Add(this.checkCommFamily);
			this.groupBox7.Controls.Add(this.checkAppt);
			this.groupBox7.Controls.Add(this.checkLabCase);
			this.groupBox7.Controls.Add(this.checkRx);
			this.groupBox7.Controls.Add(this.checkComm);
			this.groupBox7.Location = new System.Drawing.Point(145,19);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(125,148);
			this.groupBox7.TabIndex = 64;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Object Types";
			// 
			// checkSheets
			// 
			this.checkSheets.Checked = true;
			this.checkSheets.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkSheets.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSheets.Location = new System.Drawing.Point(10,130);
			this.checkSheets.Name = "checkSheets";
			this.checkSheets.Size = new System.Drawing.Size(102,13);
			this.checkSheets.TabIndex = 219;
			this.checkSheets.Text = "Sheets";
			// 
			// checkTasks
			// 
			this.checkTasks.Checked = true;
			this.checkTasks.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkTasks.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTasks.Location = new System.Drawing.Point(10,66);
			this.checkTasks.Name = "checkTasks";
			this.checkTasks.Size = new System.Drawing.Size(102,13);
			this.checkTasks.TabIndex = 218;
			this.checkTasks.Text = "Tasks";
			// 
			// checkEmail
			// 
			this.checkEmail.Checked = true;
			this.checkEmail.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkEmail.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEmail.Location = new System.Drawing.Point(10,82);
			this.checkEmail.Name = "checkEmail";
			this.checkEmail.Size = new System.Drawing.Size(102,13);
			this.checkEmail.TabIndex = 217;
			this.checkEmail.Text = "Email";
			// 
			// checkCommFamily
			// 
			this.checkCommFamily.Checked = true;
			this.checkCommFamily.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkCommFamily.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCommFamily.Location = new System.Drawing.Point(26,49);
			this.checkCommFamily.Name = "checkCommFamily";
			this.checkCommFamily.Size = new System.Drawing.Size(88,13);
			this.checkCommFamily.TabIndex = 20;
			this.checkCommFamily.Text = "Family";
			// 
			// checkAppt
			// 
			this.checkAppt.Checked = true;
			this.checkAppt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAppt.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAppt.Location = new System.Drawing.Point(10,17);
			this.checkAppt.Name = "checkAppt";
			this.checkAppt.Size = new System.Drawing.Size(102,13);
			this.checkAppt.TabIndex = 20;
			this.checkAppt.Text = "Appointments";
			// 
			// checkLabCase
			// 
			this.checkLabCase.Checked = true;
			this.checkLabCase.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkLabCase.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkLabCase.Location = new System.Drawing.Point(10,98);
			this.checkLabCase.Name = "checkLabCase";
			this.checkLabCase.Size = new System.Drawing.Size(102,13);
			this.checkLabCase.TabIndex = 17;
			this.checkLabCase.Text = "Lab Cases";
			// 
			// checkRx
			// 
			this.checkRx.Checked = true;
			this.checkRx.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkRx.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRx.Location = new System.Drawing.Point(10,114);
			this.checkRx.Name = "checkRx";
			this.checkRx.Size = new System.Drawing.Size(102,13);
			this.checkRx.TabIndex = 8;
			this.checkRx.Text = "Rx";
			// 
			// checkComm
			// 
			this.checkComm.Checked = true;
			this.checkComm.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkComm.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkComm.Location = new System.Drawing.Point(10,33);
			this.checkComm.Name = "checkComm";
			this.checkComm.Size = new System.Drawing.Size(102,13);
			this.checkComm.TabIndex = 16;
			this.checkComm.Text = "Comm Log";
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.checkShowCn);
			this.groupBox6.Controls.Add(this.checkShowE);
			this.groupBox6.Controls.Add(this.checkShowR);
			this.groupBox6.Controls.Add(this.checkShowC);
			this.groupBox6.Controls.Add(this.checkShowTP);
			this.groupBox6.Location = new System.Drawing.Point(6,19);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(121,99);
			this.groupBox6.TabIndex = 63;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Procedures";
			// 
			// checkShowCn
			// 
			this.checkShowCn.Checked = true;
			this.checkShowCn.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowCn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowCn.Location = new System.Drawing.Point(9,81);
			this.checkShowCn.Name = "checkShowCn";
			this.checkShowCn.Size = new System.Drawing.Size(101,13);
			this.checkShowCn.TabIndex = 15;
			this.checkShowCn.Text = "Conditions";
			// 
			// checkShowE
			// 
			this.checkShowE.Checked = true;
			this.checkShowE.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowE.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowE.Location = new System.Drawing.Point(9,49);
			this.checkShowE.Name = "checkShowE";
			this.checkShowE.Size = new System.Drawing.Size(101,13);
			this.checkShowE.TabIndex = 10;
			this.checkShowE.Text = "Existing";
			// 
			// checkShowR
			// 
			this.checkShowR.Checked = true;
			this.checkShowR.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowR.Location = new System.Drawing.Point(9,65);
			this.checkShowR.Name = "checkShowR";
			this.checkShowR.Size = new System.Drawing.Size(101,13);
			this.checkShowR.TabIndex = 14;
			this.checkShowR.Text = "Referred";
			// 
			// checkShowC
			// 
			this.checkShowC.Checked = true;
			this.checkShowC.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowC.Location = new System.Drawing.Point(9,33);
			this.checkShowC.Name = "checkShowC";
			this.checkShowC.Size = new System.Drawing.Size(101,13);
			this.checkShowC.TabIndex = 9;
			this.checkShowC.Text = "Completed";
			// 
			// checkShowTP
			// 
			this.checkShowTP.Checked = true;
			this.checkShowTP.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowTP.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowTP.Location = new System.Drawing.Point(28,17);
			this.checkShowTP.Name = "checkShowTP";
			this.checkShowTP.Size = new System.Drawing.Size(101,13);
			this.checkShowTP.TabIndex = 8;
			this.checkShowTP.Text = "Treat Plan";
			// 
			// checkShowTeeth
			// 
			this.checkShowTeeth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowTeeth.Location = new System.Drawing.Point(171,183);
			this.checkShowTeeth.Name = "checkShowTeeth";
			this.checkShowTeeth.Size = new System.Drawing.Size(99,13);
			this.checkShowTeeth.TabIndex = 61;
			this.checkShowTeeth.Text = "Selected Teeth";
			// 
			// checkNotes
			// 
			this.checkNotes.AllowDrop = true;
			this.checkNotes.Checked = true;
			this.checkNotes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNotes.Location = new System.Drawing.Point(16,120);
			this.checkNotes.Name = "checkNotes";
			this.checkNotes.Size = new System.Drawing.Size(102,13);
			this.checkNotes.TabIndex = 58;
			this.checkNotes.Text = "Proc Notes";
			// 
			// checkAudit
			// 
			this.checkAudit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAudit.Location = new System.Drawing.Point(171,202);
			this.checkAudit.Name = "checkAudit";
			this.checkAudit.Size = new System.Drawing.Size(73,13);
			this.checkAudit.TabIndex = 62;
			this.checkAudit.Text = "Audit";
			// 
			// butShowAll
			// 
			this.butShowAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShowAll.Autosize = true;
			this.butShowAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowAll.CornerRadius = 4F;
			this.butShowAll.Location = new System.Drawing.Point(10,192);
			this.butShowAll.Name = "butShowAll";
			this.butShowAll.Size = new System.Drawing.Size(53,23);
			this.butShowAll.TabIndex = 59;
			this.butShowAll.Text = "All";
			// 
			// butShowNone
			// 
			this.butShowNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShowNone.Autosize = true;
			this.butShowNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowNone.CornerRadius = 4F;
			this.butShowNone.Location = new System.Drawing.Point(69,192);
			this.butShowNone.Name = "butShowNone";
			this.butShowNone.Size = new System.Drawing.Size(58,23);
			this.butShowNone.TabIndex = 60;
			this.butShowNone.Text = "None";
			// 
			// textBoxViewDesc
			// 
			this.textBoxViewDesc.Location = new System.Drawing.Point(164,12);
			this.textBoxViewDesc.Name = "textBoxViewDesc";
			this.textBoxViewDesc.Size = new System.Drawing.Size(367,20);
			this.textBoxViewDesc.TabIndex = 1;
			// 
			// labelDescription
			// 
			this.labelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelDescription.Location = new System.Drawing.Point(12,9);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(146,25);
			this.labelDescription.TabIndex = 57;
			this.labelDescription.Text = "View Description";
			this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBoxProperties
			// 
			this.groupBoxProperties.Controls.Add(this.groupBox6);
			this.groupBoxProperties.Controls.Add(this.butShowNone);
			this.groupBoxProperties.Controls.Add(this.checkShowOnlyFilmsAndExams);
			this.groupBoxProperties.Controls.Add(this.butShowAll);
			this.groupBoxProperties.Controls.Add(this.checkShowOnlyHygieneProcs);
			this.groupBoxProperties.Controls.Add(this.checkAudit);
			this.groupBoxProperties.Controls.Add(this.groupBox7);
			this.groupBoxProperties.Controls.Add(this.checkNotes);
			this.groupBoxProperties.Controls.Add(this.checkShowTeeth);
			this.groupBoxProperties.Location = new System.Drawing.Point(27,37);
			this.groupBoxProperties.Name = "groupBoxProperties";
			this.groupBoxProperties.Size = new System.Drawing.Size(277,227);
			this.groupBoxProperties.TabIndex = 67;
			this.groupBoxProperties.TabStop = false;
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
			this.butDelete.Location = new System.Drawing.Point(27,795);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(99,24);
			this.butDelete.TabIndex = 68;
			this.butDelete.Text = "&Delete View";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormChartView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(570,848);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.groupBoxProperties);
			this.Controls.Add(this.textBoxViewDesc);
			this.Controls.Add(this.labelDescription);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butRight);
			this.Controls.Add(this.butLeft);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listAvailable);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butDefault);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormChartView";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Chart Views";
			this.Load += new System.EventHandler(this.FormChartView_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormChartView_FormClosing);
			this.groupBox7.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBoxProperties.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormChartView_Load(object sender,EventArgs e) {
			textBoxViewDesc.Text=ChartViewCur.Description;
			//if(!ChartViewCur.IsNew) {
			//}
			//else {
			//}
			DisplayFields.RefreshCache();
			ListShowing=DisplayFields.GetForCategory(category);
			FillGrids();
		}

		private void FillGrids() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("FormDisplayFields","FieldName"),110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormDisplayFields","New Descript"),110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormDisplayFields","Width"),60);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListShowing.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ListShowing[i].InternalName);
				row.Cells.Add(ListShowing[i].Description);
				row.Cells.Add(ListShowing[i].ColumnWidth.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			List<DisplayField> availList=DisplayFields.GetAllAvailableList(category);
			for(int i=0;i<ListShowing.Count;i++) {
				for(int j=0;j<availList.Count;j++) {
					if(ListShowing[i].InternalName==availList[j].InternalName) {
						availList.RemoveAt(j);
						break;
					}
				}
			}
			listAvailable.Items.Clear();
			for(int i=0;i<availList.Count;i++) {
				listAvailable.Items.Add(availList[i]);
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
		//  FormDisplayFieldEdit formD=new FormDisplayFieldEdit();
		//  formD.FieldCur=ListShowing[e.Row];
		//  formD.ShowDialog();
		//  FillGrids();
		//  changed=true;
		}

		private void butDefault_Click(object sender,EventArgs e) {
		//  ListShowing=DisplayFields.GetDefaultList(category);
		//  FillGrids();
		//  changed=true;
		}

		private void butLeft_Click(object sender,EventArgs e) {
		//  if(listAvailable.SelectedItems.Count==0){
		//    MsgBox.Show(this,"Please select an item in the list on the right first.");
		//    return;
		//  }
		//  DisplayField field;
		//  for(int i=0;i<listAvailable.SelectedItems.Count;i++){
		//    field=(DisplayField)listAvailable.SelectedItems[i];
		//    ListShowing.Add(field);
		//  }
		//  FillGrids();
		//  changed=true;
		}

		private void butRight_Click(object sender,EventArgs e) {
		//  if(gridMain.SelectedIndices.Length==0) {
		//    MsgBox.Show(this,"Please select an item in the grid on the left first.");
		//    return;
		//  }
		//  for(int i=gridMain.SelectedIndices.Length-1;i>=0;i--){//go backwards
		//    ListShowing.RemoveAt(gridMain.SelectedIndices[i]);
		//  }
		//  FillGrids();
		//  changed=true;
		}

		private void butUp_Click(object sender,EventArgs e) {
		//  if(gridMain.SelectedIndices.Length==0) {
		//    MsgBox.Show(this,"Please select an item in the grid first.");
		//    return;
		//  }
		//  int[] selected=new int[gridMain.SelectedIndices.Length];
		//  for(int i=0;i<gridMain.SelectedIndices.Length;i++){
		//    selected[i]=gridMain.SelectedIndices[i];
		//  }
		//  if(selected[0]==0){
		//    return;
		//  }
		//  for(int i=0;i<selected.Length;i++){
		//    ListShowing.Reverse(selected[i]-1,2);
		//  }
		//  FillGrids();
		//  for(int i=0;i<selected.Length;i++){
		//    gridMain.SetSelected(selected[i]-1,true);
		//  }
		//  changed=true;
		}

		private void butDown_Click(object sender,EventArgs e) {
		//  if(gridMain.SelectedIndices.Length==0) {
		//    MsgBox.Show(this,"Please select an item in the grid first.");
		//    return;
		//  }
		//  int[] selected=new int[gridMain.SelectedIndices.Length];
		//  for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
		//    selected[i]=gridMain.SelectedIndices[i];
		//  }
		//  if(selected[selected.Length-1]==ListShowing.Count-1) {
		//    return;
		//  }
		//  for(int i=selected.Length-1;i>=0;i--) {//go backwards
		//    ListShowing.Reverse(selected[i],2);
		//  }
		//  FillGrids();
		//  for(int i=0;i<selected.Length;i++) {
		//    gridMain.SetSelected(selected[i]+1,true);
		//  }
		//  changed=true;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(ChartViewCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,true,"Delete this chart view?")) {
				return;
			}
			try {
				ChartViews.Delete(ChartViewCur.ChartViewNum);
				DialogResult=DialogResult.OK;
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(!changed) {
				DialogResult=DialogResult.OK;
				return;
			}
		//  DisplayFields.SaveListForCategory(ListShowing,category);
		//  DataValid.SetInvalid(InvalidType.DisplayFields);
		//  DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
		//  DialogResult=DialogResult.Cancel;
		}

		private void FormChartView_FormClosing(object sender,FormClosingEventArgs e) {

		}


		

		

		

		

		

		


	}
}





















