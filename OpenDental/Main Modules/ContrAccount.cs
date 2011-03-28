/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {

	///<summary></summary>
	public class ContrAccount:System.Windows.Forms.UserControl {
		private System.Windows.Forms.Label labelFamFinancial;
		private System.ComponentModel.IContainer components=null;// Required designer variable.
		private System.Windows.Forms.Label labelUrgFinNote;
		private OpenDental.ODtextBox textUrgFinNote;
		private System.Windows.Forms.ContextMenu contextMenuIns;
		private System.Windows.Forms.MenuItem menuInsOther;
		private System.Windows.Forms.MenuItem menuInsPri;
		private System.Windows.Forms.MenuItem menuInsSec;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.ImageList imageListMain;
		private System.Windows.Forms.Panel panelSplitter;
		private System.Windows.Forms.Panel panelCommButs;
		private OpenDental.ODtextBox textFinNotes;
		private System.Windows.Forms.ContextMenu contextMenuStatement;
		private System.Windows.Forms.MenuItem menuItemStatementWalkout;
		private System.Windows.Forms.MenuItem menuItemStatementMore;
		private OpenDental.UI.ODGrid gridComm;
		private OpenDental.UI.ODGrid gridAcctPat;
		private OpenDental.UI.ODGrid gridAccount;
		private OpenDental.UI.ODGrid gridRepeat;
		private System.Windows.Forms.MenuItem menuInsMedical;
		private ContextMenu contextMenuRepeat;
		private MenuItem menuItemRepeatStand;
		private MenuItem menuItemRepeatEmail;
		private Panel panelProgNotes;
		private ODGrid gridProg;
		private GroupBox groupBox6;
		private CheckBox checkShowE;
		private CheckBox checkShowR;
		private CheckBox checkShowC;
		private CheckBox checkShowTP;
		private GroupBox groupBox7;
		private CheckBox checkAppt;
		private CheckBox checkLabCase;
		private CheckBox checkRx;
		private CheckBox checkComm;
		private CheckBox checkNotes;
		private OpenDental.UI.Button butShowAll;
		private OpenDental.UI.Button butShowNone;
		private CheckBox checkExtraNotes;
		private CheckBox checkShowTeeth;
		private CheckBox checkAudit;
		private Panel panelAging;
		private Label labelBalance;
		private Label labelInsEst;
		private Label labelTotal;
		private Label label7;
		private TextBox text0_30;
		private Label label6;
		private TextBox text31_60;
		private Label label5;
		private TextBox text61_90;
		private Label label3;
		private TextBox textOver90;
		private Label label2;
		private OpenDental.UI.Button butTrojan;
		private MenuItem menuItemStatementEmail;
		private Label labelBalanceAmt;
		private TabControl tabControlShow;
		private TabPage tabMain;
		private TabPage tabShow;
		private ODGrid gridPayPlan;
		private ValidDate textDateEnd;
		private ValidDate textDateStart;
		private Label labelEndDate;
		private Label labelStartDate;
		private OpenDental.UI.Button butRefresh;
		private OpenDental.UI.Button but90days;
		private OpenDental.UI.Button but45days;
		private OpenDental.UI.Button butDatesAll;
		private CheckBox checkShowDetail;
		private OpenDental.UI.Button butToday;
		private CheckBox checkShowFamilyComm;
		private FormPayPlan FormPayPlan2;
		private Label labelTotalAmt;
		private Label labelInsEstAmt;
		private Panel panelAgeLine;
		private Panel panel2;
		private Panel panel1;
		private ToolTip toolTip1;
		private Label labelPatEstBal;
		private Label labelPatEstBalAmt;
		private Panel panelTotalOwes;
		private Label label21;
		private Label labelTotalPtOwes;
		private Label labelUnearnedAmt;
		private Label labelUnearned;
		private Label labelInsRem;
		private double PPBalanceTotal;

		#region UserVariables
		///<summary>This holds nearly all of the data needed for display.  It is retrieved in one call to the database.</summary>
		private DataSet DataSetMain;
		private Family FamCur;
		///<summary></summary>
		private Patient PatCur;
		private PatientNote PatientNoteCur;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		private RepeatCharge[] RepeatChargeList;
		private int OriginalMousePos;
		private bool MouseIsDownOnSplitter;
		private int SplitterOriginalY;
		private bool FinNoteChanged;
		private bool CCChanged;
		private bool UrgFinNoteChanged;
		private int Actscrollval;
		///<summary>Set to true if this control is placed in the recall edit window. This affects the control behavior.</summary>
		public bool ViewingInRecall=false;
		private List<DisplayField> fieldsForMainGrid;
		private OpenDental.UI.Button butComm;
		private GroupBox groupBoxIndIns;
		private TextBox textPriDed;
		private TextBox textPriUsed;
		private TextBox textPriDedRem;
		private TextBox textPriPend;
		private TextBox textPriRem;
		private TextBox textPriMax;
		private TextBox textSecRem;
		private Label label10;
		private TextBox textSecPend;
		private Label label11;
		private Label label18;
		private Label label12;
		private Label label13;
		private TextBox textSecDedRem;
		private Label label14;
		private Label label15;
		private TextBox textSecMax;
		private Label label16;
		private TextBox textSecDed;
		private TextBox textSecUsed;
		private GroupBox groupBoxFamilyIns;
		private TextBox textFamPriMax;
		private TextBox textFamPriDed;
		private Label label4;
		private Label label8;
		private TextBox textFamSecMax;
		private Label label9;
		private TextBox textFamSecDed;
		private Label label17;
		private UI.Button butCreditCard;
		private MenuItem menuItemRepeatMobile;
		private bool InitializedOnStartup;
		#endregion UserVariables

		///<summary></summary>
		public ContrAccount() {
			Logger.openlog.Log("Initializing account module...",Logger.Severity.INFO);
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
		}

		///<summary></summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components!= null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContrAccount));
			this.labelFamFinancial = new System.Windows.Forms.Label();
			this.labelUrgFinNote = new System.Windows.Forms.Label();
			this.contextMenuIns = new System.Windows.Forms.ContextMenu();
			this.menuInsPri = new System.Windows.Forms.MenuItem();
			this.menuInsSec = new System.Windows.Forms.MenuItem();
			this.menuInsMedical = new System.Windows.Forms.MenuItem();
			this.menuInsOther = new System.Windows.Forms.MenuItem();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.panelSplitter = new System.Windows.Forms.Panel();
			this.panelCommButs = new System.Windows.Forms.Panel();
			this.contextMenuStatement = new System.Windows.Forms.ContextMenu();
			this.menuItemStatementWalkout = new System.Windows.Forms.MenuItem();
			this.menuItemStatementEmail = new System.Windows.Forms.MenuItem();
			this.menuItemStatementMore = new System.Windows.Forms.MenuItem();
			this.contextMenuRepeat = new System.Windows.Forms.ContextMenu();
			this.menuItemRepeatStand = new System.Windows.Forms.MenuItem();
			this.menuItemRepeatEmail = new System.Windows.Forms.MenuItem();
			this.menuItemRepeatMobile = new System.Windows.Forms.MenuItem();
			this.panelProgNotes = new System.Windows.Forms.Panel();
			this.checkNotes = new System.Windows.Forms.CheckBox();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.checkShowTeeth = new System.Windows.Forms.CheckBox();
			this.checkAudit = new System.Windows.Forms.CheckBox();
			this.checkExtraNotes = new System.Windows.Forms.CheckBox();
			this.checkAppt = new System.Windows.Forms.CheckBox();
			this.checkLabCase = new System.Windows.Forms.CheckBox();
			this.checkRx = new System.Windows.Forms.CheckBox();
			this.checkComm = new System.Windows.Forms.CheckBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.checkShowE = new System.Windows.Forms.CheckBox();
			this.checkShowR = new System.Windows.Forms.CheckBox();
			this.checkShowC = new System.Windows.Forms.CheckBox();
			this.checkShowTP = new System.Windows.Forms.CheckBox();
			this.panelAging = new System.Windows.Forms.Panel();
			this.labelInsRem = new System.Windows.Forms.Label();
			this.labelUnearnedAmt = new System.Windows.Forms.Label();
			this.labelUnearned = new System.Windows.Forms.Label();
			this.labelBalanceAmt = new System.Windows.Forms.Label();
			this.labelTotalAmt = new System.Windows.Forms.Label();
			this.panelTotalOwes = new System.Windows.Forms.Panel();
			this.label21 = new System.Windows.Forms.Label();
			this.labelTotalPtOwes = new System.Windows.Forms.Label();
			this.labelPatEstBalAmt = new System.Windows.Forms.Label();
			this.labelPatEstBal = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelAgeLine = new System.Windows.Forms.Panel();
			this.labelInsEstAmt = new System.Windows.Forms.Label();
			this.labelBalance = new System.Windows.Forms.Label();
			this.labelInsEst = new System.Windows.Forms.Label();
			this.labelTotal = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.text0_30 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.text31_60 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.text61_90 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textOver90 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tabControlShow = new System.Windows.Forms.TabControl();
			this.tabMain = new System.Windows.Forms.TabPage();
			this.tabShow = new System.Windows.Forms.TabPage();
			this.checkShowFamilyComm = new System.Windows.Forms.CheckBox();
			this.checkShowDetail = new System.Windows.Forms.CheckBox();
			this.labelEndDate = new System.Windows.Forms.Label();
			this.labelStartDate = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBoxIndIns = new System.Windows.Forms.GroupBox();
			this.textPriDed = new System.Windows.Forms.TextBox();
			this.textPriUsed = new System.Windows.Forms.TextBox();
			this.textPriDedRem = new System.Windows.Forms.TextBox();
			this.textPriPend = new System.Windows.Forms.TextBox();
			this.textPriRem = new System.Windows.Forms.TextBox();
			this.textPriMax = new System.Windows.Forms.TextBox();
			this.textSecRem = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textSecPend = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.textSecDedRem = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.textSecMax = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textSecDed = new System.Windows.Forms.TextBox();
			this.textSecUsed = new System.Windows.Forms.TextBox();
			this.groupBoxFamilyIns = new System.Windows.Forms.GroupBox();
			this.textFamPriMax = new System.Windows.Forms.TextBox();
			this.textFamPriDed = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.textFamSecMax = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textFamSecDed = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.gridPayPlan = new OpenDental.UI.ODGrid();
			this.butCreditCard = new OpenDental.UI.Button();
			this.textUrgFinNote = new OpenDental.ODtextBox();
			this.gridAcctPat = new OpenDental.UI.ODGrid();
			this.textFinNotes = new OpenDental.ODtextBox();
			this.butToday = new OpenDental.UI.Button();
			this.butDatesAll = new OpenDental.UI.Button();
			this.but90days = new OpenDental.UI.Button();
			this.but45days = new OpenDental.UI.Button();
			this.butRefresh = new OpenDental.UI.Button();
			this.textDateEnd = new OpenDental.ValidDate();
			this.textDateStart = new OpenDental.ValidDate();
			this.butShowNone = new OpenDental.UI.Button();
			this.butShowAll = new OpenDental.UI.Button();
			this.gridProg = new OpenDental.UI.ODGrid();
			this.gridRepeat = new OpenDental.UI.ODGrid();
			this.gridAccount = new OpenDental.UI.ODGrid();
			this.gridComm = new OpenDental.UI.ODGrid();
			this.butComm = new OpenDental.UI.Button();
			this.butTrojan = new OpenDental.UI.Button();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.panelCommButs.SuspendLayout();
			this.panelProgNotes.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.panelAging.SuspendLayout();
			this.panelTotalOwes.SuspendLayout();
			this.tabControlShow.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.tabShow.SuspendLayout();
			this.groupBoxIndIns.SuspendLayout();
			this.groupBoxFamilyIns.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelFamFinancial
			// 
			this.labelFamFinancial.Font = new System.Drawing.Font("Microsoft Sans Serif",9.75F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelFamFinancial.Location = new System.Drawing.Point(0,259);
			this.labelFamFinancial.Name = "labelFamFinancial";
			this.labelFamFinancial.Size = new System.Drawing.Size(154,16);
			this.labelFamFinancial.TabIndex = 9;
			this.labelFamFinancial.Text = "Family Financial Notes";
			this.labelFamFinancial.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelUrgFinNote
			// 
			this.labelUrgFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif",9.75F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelUrgFinNote.Location = new System.Drawing.Point(0,0);
			this.labelUrgFinNote.Name = "labelUrgFinNote";
			this.labelUrgFinNote.Size = new System.Drawing.Size(165,17);
			this.labelUrgFinNote.TabIndex = 10;
			this.labelUrgFinNote.Text = "Fam Urgent Fin Note";
			this.labelUrgFinNote.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// contextMenuIns
			// 
			this.contextMenuIns.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuInsPri,
            this.menuInsSec,
            this.menuInsMedical,
            this.menuInsOther});
			// 
			// menuInsPri
			// 
			this.menuInsPri.Index = 0;
			this.menuInsPri.Text = "Primary";
			this.menuInsPri.Click += new System.EventHandler(this.menuInsPri_Click);
			// 
			// menuInsSec
			// 
			this.menuInsSec.Index = 1;
			this.menuInsSec.Text = "Secondary";
			this.menuInsSec.Click += new System.EventHandler(this.menuInsSec_Click);
			// 
			// menuInsMedical
			// 
			this.menuInsMedical.Index = 2;
			this.menuInsMedical.Text = "Medical";
			this.menuInsMedical.Click += new System.EventHandler(this.menuInsMedical_Click);
			// 
			// menuInsOther
			// 
			this.menuInsOther.Index = 3;
			this.menuInsOther.Text = "Other";
			this.menuInsOther.Click += new System.EventHandler(this.menuInsOther_Click);
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"Pat.gif");
			this.imageListMain.Images.SetKeyName(1,"");
			this.imageListMain.Images.SetKeyName(2,"");
			this.imageListMain.Images.SetKeyName(3,"Umbrella.gif");
			this.imageListMain.Images.SetKeyName(4,"");
			// 
			// panelSplitter
			// 
			this.panelSplitter.Cursor = System.Windows.Forms.Cursors.SizeNS;
			this.panelSplitter.Location = new System.Drawing.Point(0,425);
			this.panelSplitter.Name = "panelSplitter";
			this.panelSplitter.Size = new System.Drawing.Size(769,5);
			this.panelSplitter.TabIndex = 49;
			this.panelSplitter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseDown);
			this.panelSplitter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseMove);
			this.panelSplitter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseUp);
			// 
			// panelCommButs
			// 
			this.panelCommButs.Controls.Add(this.butComm);
			this.panelCommButs.Controls.Add(this.butTrojan);
			this.panelCommButs.Location = new System.Drawing.Point(749,429);
			this.panelCommButs.Name = "panelCommButs";
			this.panelCommButs.Size = new System.Drawing.Size(163,63);
			this.panelCommButs.TabIndex = 69;
			// 
			// contextMenuStatement
			// 
			this.contextMenuStatement.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemStatementWalkout,
            this.menuItemStatementEmail,
            this.menuItemStatementMore});
			// 
			// menuItemStatementWalkout
			// 
			this.menuItemStatementWalkout.Index = 0;
			this.menuItemStatementWalkout.Text = "Walkout";
			this.menuItemStatementWalkout.Click += new System.EventHandler(this.menuItemStatementWalkout_Click);
			// 
			// menuItemStatementEmail
			// 
			this.menuItemStatementEmail.Index = 1;
			this.menuItemStatementEmail.Text = "Email";
			this.menuItemStatementEmail.Click += new System.EventHandler(this.menuItemStatementEmail_Click);
			// 
			// menuItemStatementMore
			// 
			this.menuItemStatementMore.Index = 2;
			this.menuItemStatementMore.Text = "More Options";
			this.menuItemStatementMore.Click += new System.EventHandler(this.menuItemStatementMore_Click);
			// 
			// contextMenuRepeat
			// 
			this.contextMenuRepeat.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemRepeatStand,
            this.menuItemRepeatEmail,
            this.menuItemRepeatMobile});
			// 
			// menuItemRepeatStand
			// 
			this.menuItemRepeatStand.Index = 0;
			this.menuItemRepeatStand.Text = "Standard Monthly";
			this.menuItemRepeatStand.Click += new System.EventHandler(this.MenuItemRepeatStand_Click);
			// 
			// menuItemRepeatEmail
			// 
			this.menuItemRepeatEmail.Index = 1;
			this.menuItemRepeatEmail.Text = "Email Monthly";
			this.menuItemRepeatEmail.Click += new System.EventHandler(this.MenuItemRepeatEmail_Click);
			// 
			// menuItemRepeatMobile
			// 
			this.menuItemRepeatMobile.Index = 2;
			this.menuItemRepeatMobile.Text = "Mobile Monthly";
			this.menuItemRepeatMobile.Click += new System.EventHandler(this.menuItemRepeatMobile_Click);
			// 
			// panelProgNotes
			// 
			this.panelProgNotes.Controls.Add(this.butShowNone);
			this.panelProgNotes.Controls.Add(this.butShowAll);
			this.panelProgNotes.Controls.Add(this.checkNotes);
			this.panelProgNotes.Controls.Add(this.groupBox7);
			this.panelProgNotes.Controls.Add(this.groupBox6);
			this.panelProgNotes.Controls.Add(this.gridProg);
			this.panelProgNotes.Location = new System.Drawing.Point(0,461);
			this.panelProgNotes.Name = "panelProgNotes";
			this.panelProgNotes.Size = new System.Drawing.Size(749,227);
			this.panelProgNotes.TabIndex = 211;
			// 
			// checkNotes
			// 
			this.checkNotes.AllowDrop = true;
			this.checkNotes.Checked = true;
			this.checkNotes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNotes.Location = new System.Drawing.Point(624,191);
			this.checkNotes.Name = "checkNotes";
			this.checkNotes.Size = new System.Drawing.Size(102,13);
			this.checkNotes.TabIndex = 214;
			this.checkNotes.Text = "Proc Notes";
			this.checkNotes.Visible = false;
			this.checkNotes.Click += new System.EventHandler(this.checkNotes_Click);
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.checkShowTeeth);
			this.groupBox7.Controls.Add(this.checkAudit);
			this.groupBox7.Controls.Add(this.checkExtraNotes);
			this.groupBox7.Controls.Add(this.checkAppt);
			this.groupBox7.Controls.Add(this.checkLabCase);
			this.groupBox7.Controls.Add(this.checkRx);
			this.groupBox7.Controls.Add(this.checkComm);
			this.groupBox7.Location = new System.Drawing.Point(614,88);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(121,101);
			this.groupBox7.TabIndex = 213;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Object Types";
			this.groupBox7.Visible = false;
			// 
			// checkShowTeeth
			// 
			this.checkShowTeeth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowTeeth.Location = new System.Drawing.Point(44,63);
			this.checkShowTeeth.Name = "checkShowTeeth";
			this.checkShowTeeth.Size = new System.Drawing.Size(104,13);
			this.checkShowTeeth.TabIndex = 217;
			this.checkShowTeeth.Text = "Selected Teeth";
			this.checkShowTeeth.Visible = false;
			// 
			// checkAudit
			// 
			this.checkAudit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAudit.Location = new System.Drawing.Point(44,79);
			this.checkAudit.Name = "checkAudit";
			this.checkAudit.Size = new System.Drawing.Size(73,13);
			this.checkAudit.TabIndex = 218;
			this.checkAudit.Text = "Audit";
			this.checkAudit.Visible = false;
			// 
			// checkExtraNotes
			// 
			this.checkExtraNotes.AllowDrop = true;
			this.checkExtraNotes.Checked = true;
			this.checkExtraNotes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkExtraNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExtraNotes.Location = new System.Drawing.Point(9,82);
			this.checkExtraNotes.Name = "checkExtraNotes";
			this.checkExtraNotes.Size = new System.Drawing.Size(102,13);
			this.checkExtraNotes.TabIndex = 215;
			this.checkExtraNotes.Text = "Extra Notes";
			this.checkExtraNotes.Visible = false;
			this.checkExtraNotes.Click += new System.EventHandler(this.checkExtraNotes_Click);
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
			this.checkAppt.Click += new System.EventHandler(this.checkAppt_Click);
			// 
			// checkLabCase
			// 
			this.checkLabCase.Checked = true;
			this.checkLabCase.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkLabCase.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkLabCase.Location = new System.Drawing.Point(10,49);
			this.checkLabCase.Name = "checkLabCase";
			this.checkLabCase.Size = new System.Drawing.Size(102,13);
			this.checkLabCase.TabIndex = 17;
			this.checkLabCase.Text = "Lab Cases";
			this.checkLabCase.Click += new System.EventHandler(this.checkLabCase_Click);
			// 
			// checkRx
			// 
			this.checkRx.Checked = true;
			this.checkRx.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkRx.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRx.Location = new System.Drawing.Point(10,65);
			this.checkRx.Name = "checkRx";
			this.checkRx.Size = new System.Drawing.Size(102,13);
			this.checkRx.TabIndex = 8;
			this.checkRx.Text = "Rx";
			this.checkRx.Click += new System.EventHandler(this.checkRx_Click);
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
			this.checkComm.Click += new System.EventHandler(this.checkComm_Click);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.checkShowE);
			this.groupBox6.Controls.Add(this.checkShowR);
			this.groupBox6.Controls.Add(this.checkShowC);
			this.groupBox6.Controls.Add(this.checkShowTP);
			this.groupBox6.Location = new System.Drawing.Point(614,1);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(121,85);
			this.groupBox6.TabIndex = 212;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Procedures";
			this.groupBox6.Visible = false;
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
			this.checkShowE.Click += new System.EventHandler(this.checkShowE_Click);
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
			this.checkShowR.Click += new System.EventHandler(this.checkShowR_Click);
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
			this.checkShowC.Click += new System.EventHandler(this.checkShowC_Click);
			// 
			// checkShowTP
			// 
			this.checkShowTP.Checked = true;
			this.checkShowTP.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowTP.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowTP.Location = new System.Drawing.Point(9,17);
			this.checkShowTP.Name = "checkShowTP";
			this.checkShowTP.Size = new System.Drawing.Size(101,13);
			this.checkShowTP.TabIndex = 8;
			this.checkShowTP.Text = "Treat Plan";
			this.checkShowTP.Click += new System.EventHandler(this.checkShowTP_Click);
			// 
			// panelAging
			// 
			this.panelAging.Controls.Add(this.labelInsRem);
			this.panelAging.Controls.Add(this.labelUnearnedAmt);
			this.panelAging.Controls.Add(this.labelUnearned);
			this.panelAging.Controls.Add(this.labelBalanceAmt);
			this.panelAging.Controls.Add(this.labelTotalAmt);
			this.panelAging.Controls.Add(this.panelTotalOwes);
			this.panelAging.Controls.Add(this.labelPatEstBalAmt);
			this.panelAging.Controls.Add(this.labelPatEstBal);
			this.panelAging.Controls.Add(this.panel2);
			this.panelAging.Controls.Add(this.panel1);
			this.panelAging.Controls.Add(this.panelAgeLine);
			this.panelAging.Controls.Add(this.labelInsEstAmt);
			this.panelAging.Controls.Add(this.labelBalance);
			this.panelAging.Controls.Add(this.labelInsEst);
			this.panelAging.Controls.Add(this.labelTotal);
			this.panelAging.Controls.Add(this.label7);
			this.panelAging.Controls.Add(this.text0_30);
			this.panelAging.Controls.Add(this.label6);
			this.panelAging.Controls.Add(this.text31_60);
			this.panelAging.Controls.Add(this.label5);
			this.panelAging.Controls.Add(this.text61_90);
			this.panelAging.Controls.Add(this.label3);
			this.panelAging.Controls.Add(this.textOver90);
			this.panelAging.Controls.Add(this.label2);
			this.panelAging.Location = new System.Drawing.Point(0,25);
			this.panelAging.Name = "panelAging";
			this.panelAging.Size = new System.Drawing.Size(749,37);
			this.panelAging.TabIndex = 213;
			// 
			// labelInsRem
			// 
			this.labelInsRem.BackColor = System.Drawing.Color.LightGray;
			this.labelInsRem.Location = new System.Drawing.Point(700,4);
			this.labelInsRem.Name = "labelInsRem";
			this.labelInsRem.Size = new System.Drawing.Size(45,29);
			this.labelInsRem.TabIndex = 0;
			this.labelInsRem.Text = "Ins\r\nRem";
			this.labelInsRem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelInsRem.MouseEnter += new System.EventHandler(this.labelInsRem_MouseEnter);
			this.labelInsRem.MouseLeave += new System.EventHandler(this.labelInsRem_MouseLeave);
			// 
			// labelUnearnedAmt
			// 
			this.labelUnearnedAmt.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelUnearnedAmt.ForeColor = System.Drawing.Color.Firebrick;
			this.labelUnearnedAmt.Location = new System.Drawing.Point(636,18);
			this.labelUnearnedAmt.Name = "labelUnearnedAmt";
			this.labelUnearnedAmt.Size = new System.Drawing.Size(60,13);
			this.labelUnearnedAmt.TabIndex = 228;
			this.labelUnearnedAmt.Text = "25000.00";
			this.labelUnearnedAmt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelUnearned
			// 
			this.labelUnearned.Font = new System.Drawing.Font("Microsoft Sans Serif",8F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelUnearned.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelUnearned.Location = new System.Drawing.Point(632,2);
			this.labelUnearned.Name = "labelUnearned";
			this.labelUnearned.Size = new System.Drawing.Size(68,13);
			this.labelUnearned.TabIndex = 227;
			this.labelUnearned.Text = "Unearned";
			this.labelUnearned.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelBalanceAmt
			// 
			this.labelBalanceAmt.Font = new System.Drawing.Font("Microsoft Sans Serif",11F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelBalanceAmt.ForeColor = System.Drawing.Color.Firebrick;
			this.labelBalanceAmt.Location = new System.Drawing.Point(456,16);
			this.labelBalanceAmt.Name = "labelBalanceAmt";
			this.labelBalanceAmt.Size = new System.Drawing.Size(80,19);
			this.labelBalanceAmt.TabIndex = 60;
			this.labelBalanceAmt.Text = "25000.00";
			this.labelBalanceAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTotalAmt
			// 
			this.labelTotalAmt.Font = new System.Drawing.Font("Microsoft Sans Serif",11F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelTotalAmt.ForeColor = System.Drawing.Color.Firebrick;
			this.labelTotalAmt.Location = new System.Drawing.Point(294,16);
			this.labelTotalAmt.Name = "labelTotalAmt";
			this.labelTotalAmt.Size = new System.Drawing.Size(80,19);
			this.labelTotalAmt.TabIndex = 61;
			this.labelTotalAmt.Text = "25000.00";
			this.labelTotalAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelTotalOwes
			// 
			this.panelTotalOwes.Controls.Add(this.label21);
			this.panelTotalOwes.Controls.Add(this.labelTotalPtOwes);
			this.panelTotalOwes.Location = new System.Drawing.Point(560,-38);
			this.panelTotalOwes.Name = "panelTotalOwes";
			this.panelTotalOwes.Size = new System.Drawing.Size(126,37);
			this.panelTotalOwes.TabIndex = 226;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(3,0);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(123,12);
			this.label21.TabIndex = 223;
			this.label21.Text = "TOTAL  Owed w/ Plan:";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolTip1.SetToolTip(this.label21,"Total balance owed on all payment plans ");
			// 
			// labelTotalPtOwes
			// 
			this.labelTotalPtOwes.Font = new System.Drawing.Font("Microsoft Sans Serif",14.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelTotalPtOwes.ForeColor = System.Drawing.Color.Firebrick;
			this.labelTotalPtOwes.Location = new System.Drawing.Point(6,12);
			this.labelTotalPtOwes.Name = "labelTotalPtOwes";
			this.labelTotalPtOwes.Size = new System.Drawing.Size(112,23);
			this.labelTotalPtOwes.TabIndex = 222;
			this.labelTotalPtOwes.Text = "2500.00";
			this.labelTotalPtOwes.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelPatEstBalAmt
			// 
			this.labelPatEstBalAmt.Location = new System.Drawing.Point(550,19);
			this.labelPatEstBalAmt.Name = "labelPatEstBalAmt";
			this.labelPatEstBalAmt.Size = new System.Drawing.Size(65,13);
			this.labelPatEstBalAmt.TabIndex = 89;
			this.labelPatEstBalAmt.Text = "25000.00";
			this.labelPatEstBalAmt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelPatEstBal
			// 
			this.labelPatEstBal.Location = new System.Drawing.Point(550,3);
			this.labelPatEstBal.Name = "labelPatEstBal";
			this.labelPatEstBal.Size = new System.Drawing.Size(65,13);
			this.labelPatEstBal.TabIndex = 88;
			this.labelPatEstBal.Text = "Pat Est Bal";
			this.labelPatEstBal.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel2.Location = new System.Drawing.Point(624,3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(2,32);
			this.panel2.TabIndex = 87;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel1.Location = new System.Drawing.Point(541,3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(2,32);
			this.panel1.TabIndex = 86;
			// 
			// panelAgeLine
			// 
			this.panelAgeLine.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panelAgeLine.Location = new System.Drawing.Point(379,2);
			this.panelAgeLine.Name = "panelAgeLine";
			this.panelAgeLine.Size = new System.Drawing.Size(2,32);
			this.panelAgeLine.TabIndex = 63;
			// 
			// labelInsEstAmt
			// 
			this.labelInsEstAmt.Location = new System.Drawing.Point(387,18);
			this.labelInsEstAmt.Name = "labelInsEstAmt";
			this.labelInsEstAmt.Size = new System.Drawing.Size(65,13);
			this.labelInsEstAmt.TabIndex = 62;
			this.labelInsEstAmt.Text = "2500.00";
			this.labelInsEstAmt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelBalance
			// 
			this.labelBalance.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelBalance.Location = new System.Drawing.Point(454,2);
			this.labelBalance.Name = "labelBalance";
			this.labelBalance.Size = new System.Drawing.Size(80,13);
			this.labelBalance.TabIndex = 59;
			this.labelBalance.Text = "= Balance";
			this.labelBalance.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelInsEst
			// 
			this.labelInsEst.Location = new System.Drawing.Point(387,2);
			this.labelInsEst.Name = "labelInsEst";
			this.labelInsEst.Size = new System.Drawing.Size(65,13);
			this.labelInsEst.TabIndex = 57;
			this.labelInsEst.Text = "- InsEst";
			this.labelInsEst.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelTotal
			// 
			this.labelTotal.Location = new System.Drawing.Point(294,2);
			this.labelTotal.Name = "labelTotal";
			this.labelTotal.Size = new System.Drawing.Size(80,13);
			this.labelTotal.TabIndex = 55;
			this.labelTotal.Text = "Total";
			this.labelTotal.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(69,2);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(55,13);
			this.label7.TabIndex = 53;
			this.label7.Text = "0-30";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text0_30
			// 
			this.text0_30.Location = new System.Drawing.Point(67,15);
			this.text0_30.Name = "text0_30";
			this.text0_30.ReadOnly = true;
			this.text0_30.Size = new System.Drawing.Size(55,20);
			this.text0_30.TabIndex = 52;
			this.text0_30.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(122,2);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(55,13);
			this.label6.TabIndex = 51;
			this.label6.Text = "31-60";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text31_60
			// 
			this.text31_60.Location = new System.Drawing.Point(122,15);
			this.text31_60.Name = "text31_60";
			this.text31_60.ReadOnly = true;
			this.text31_60.Size = new System.Drawing.Size(55,20);
			this.text31_60.TabIndex = 50;
			this.text31_60.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(177,2);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(55,13);
			this.label5.TabIndex = 49;
			this.label5.Text = "61-90";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text61_90
			// 
			this.text61_90.Location = new System.Drawing.Point(177,15);
			this.text61_90.Name = "text61_90";
			this.text61_90.ReadOnly = true;
			this.text61_90.Size = new System.Drawing.Size(55,20);
			this.text61_90.TabIndex = 48;
			this.text61_90.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(232,2);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55,13);
			this.label3.TabIndex = 47;
			this.label3.Text = "over 90";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textOver90
			// 
			this.textOver90.Location = new System.Drawing.Point(232,15);
			this.textOver90.Name = "textOver90";
			this.textOver90.ReadOnly = true;
			this.textOver90.Size = new System.Drawing.Size(55,20);
			this.textOver90.TabIndex = 46;
			this.textOver90.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label2.Location = new System.Drawing.Point(14,2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50,33);
			this.label2.TabIndex = 45;
			this.label2.Text = "Family\r\nAging";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tabControlShow
			// 
			this.tabControlShow.Controls.Add(this.tabMain);
			this.tabControlShow.Controls.Add(this.tabShow);
			this.tabControlShow.Location = new System.Drawing.Point(749,27);
			this.tabControlShow.Name = "tabControlShow";
			this.tabControlShow.SelectedIndex = 0;
			this.tabControlShow.Size = new System.Drawing.Size(186,397);
			this.tabControlShow.TabIndex = 216;
			// 
			// tabMain
			// 
			this.tabMain.BackColor = System.Drawing.Color.Transparent;
			this.tabMain.Controls.Add(this.butCreditCard);
			this.tabMain.Controls.Add(this.labelUrgFinNote);
			this.tabMain.Controls.Add(this.labelFamFinancial);
			this.tabMain.Controls.Add(this.textUrgFinNote);
			this.tabMain.Controls.Add(this.gridAcctPat);
			this.tabMain.Controls.Add(this.textFinNotes);
			this.tabMain.Location = new System.Drawing.Point(4,22);
			this.tabMain.Name = "tabMain";
			this.tabMain.Padding = new System.Windows.Forms.Padding(3);
			this.tabMain.Size = new System.Drawing.Size(178,371);
			this.tabMain.TabIndex = 0;
			this.tabMain.Text = "Main";
			this.tabMain.UseVisualStyleBackColor = true;
			// 
			// tabShow
			// 
			this.tabShow.BackColor = System.Drawing.Color.Transparent;
			this.tabShow.Controls.Add(this.checkShowFamilyComm);
			this.tabShow.Controls.Add(this.butToday);
			this.tabShow.Controls.Add(this.checkShowDetail);
			this.tabShow.Controls.Add(this.butDatesAll);
			this.tabShow.Controls.Add(this.but90days);
			this.tabShow.Controls.Add(this.but45days);
			this.tabShow.Controls.Add(this.butRefresh);
			this.tabShow.Controls.Add(this.labelEndDate);
			this.tabShow.Controls.Add(this.labelStartDate);
			this.tabShow.Controls.Add(this.textDateEnd);
			this.tabShow.Controls.Add(this.textDateStart);
			this.tabShow.Location = new System.Drawing.Point(4,22);
			this.tabShow.Name = "tabShow";
			this.tabShow.Padding = new System.Windows.Forms.Padding(3);
			this.tabShow.Size = new System.Drawing.Size(178,371);
			this.tabShow.TabIndex = 1;
			this.tabShow.Text = "Show";
			this.tabShow.UseVisualStyleBackColor = true;
			// 
			// checkShowFamilyComm
			// 
			this.checkShowFamilyComm.AutoSize = true;
			this.checkShowFamilyComm.Location = new System.Drawing.Point(8,220);
			this.checkShowFamilyComm.Name = "checkShowFamilyComm";
			this.checkShowFamilyComm.Size = new System.Drawing.Size(152,17);
			this.checkShowFamilyComm.TabIndex = 221;
			this.checkShowFamilyComm.Text = "Show Family Comm Entries";
			this.checkShowFamilyComm.UseVisualStyleBackColor = true;
			this.checkShowFamilyComm.Click += new System.EventHandler(this.checkShowFamilyComm_Click);
			// 
			// checkShowDetail
			// 
			this.checkShowDetail.Checked = true;
			this.checkShowDetail.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowDetail.Location = new System.Drawing.Point(8,196);
			this.checkShowDetail.Name = "checkShowDetail";
			this.checkShowDetail.Size = new System.Drawing.Size(164,18);
			this.checkShowDetail.TabIndex = 219;
			this.checkShowDetail.Text = "Show Proc Breakdowns";
			this.checkShowDetail.UseVisualStyleBackColor = true;
			this.checkShowDetail.Click += new System.EventHandler(this.checkShowDetail_Click);
			// 
			// labelEndDate
			// 
			this.labelEndDate.Location = new System.Drawing.Point(2,34);
			this.labelEndDate.Name = "labelEndDate";
			this.labelEndDate.Size = new System.Drawing.Size(91,14);
			this.labelEndDate.TabIndex = 211;
			this.labelEndDate.Text = "End Date";
			this.labelEndDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelStartDate
			// 
			this.labelStartDate.Location = new System.Drawing.Point(8,11);
			this.labelStartDate.Name = "labelStartDate";
			this.labelStartDate.Size = new System.Drawing.Size(84,14);
			this.labelStartDate.TabIndex = 210;
			this.labelStartDate.Text = "Start Date";
			this.labelStartDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBoxIndIns
			// 
			this.groupBoxIndIns.Controls.Add(this.textPriDed);
			this.groupBoxIndIns.Controls.Add(this.textPriUsed);
			this.groupBoxIndIns.Controls.Add(this.textPriDedRem);
			this.groupBoxIndIns.Controls.Add(this.textPriPend);
			this.groupBoxIndIns.Controls.Add(this.textPriRem);
			this.groupBoxIndIns.Controls.Add(this.textPriMax);
			this.groupBoxIndIns.Controls.Add(this.textSecRem);
			this.groupBoxIndIns.Controls.Add(this.label10);
			this.groupBoxIndIns.Controls.Add(this.textSecPend);
			this.groupBoxIndIns.Controls.Add(this.label11);
			this.groupBoxIndIns.Controls.Add(this.label18);
			this.groupBoxIndIns.Controls.Add(this.label12);
			this.groupBoxIndIns.Controls.Add(this.label13);
			this.groupBoxIndIns.Controls.Add(this.textSecDedRem);
			this.groupBoxIndIns.Controls.Add(this.label14);
			this.groupBoxIndIns.Controls.Add(this.label15);
			this.groupBoxIndIns.Controls.Add(this.textSecMax);
			this.groupBoxIndIns.Controls.Add(this.label16);
			this.groupBoxIndIns.Controls.Add(this.textSecDed);
			this.groupBoxIndIns.Controls.Add(this.textSecUsed);
			this.groupBoxIndIns.Location = new System.Drawing.Point(556,144);
			this.groupBoxIndIns.Name = "groupBoxIndIns";
			this.groupBoxIndIns.Size = new System.Drawing.Size(193,160);
			this.groupBoxIndIns.TabIndex = 219;
			this.groupBoxIndIns.TabStop = false;
			this.groupBoxIndIns.Text = "Individual Insurance";
			this.groupBoxIndIns.Visible = false;
			// 
			// textPriDed
			// 
			this.textPriDed.BackColor = System.Drawing.Color.White;
			this.textPriDed.Location = new System.Drawing.Point(71,55);
			this.textPriDed.Name = "textPriDed";
			this.textPriDed.ReadOnly = true;
			this.textPriDed.Size = new System.Drawing.Size(60,20);
			this.textPriDed.TabIndex = 45;
			this.textPriDed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriUsed
			// 
			this.textPriUsed.BackColor = System.Drawing.Color.White;
			this.textPriUsed.Location = new System.Drawing.Point(71,95);
			this.textPriUsed.Name = "textPriUsed";
			this.textPriUsed.ReadOnly = true;
			this.textPriUsed.Size = new System.Drawing.Size(60,20);
			this.textPriUsed.TabIndex = 44;
			this.textPriUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriDedRem
			// 
			this.textPriDedRem.BackColor = System.Drawing.Color.White;
			this.textPriDedRem.Location = new System.Drawing.Point(71,75);
			this.textPriDedRem.Name = "textPriDedRem";
			this.textPriDedRem.ReadOnly = true;
			this.textPriDedRem.Size = new System.Drawing.Size(60,20);
			this.textPriDedRem.TabIndex = 51;
			this.textPriDedRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriPend
			// 
			this.textPriPend.BackColor = System.Drawing.Color.White;
			this.textPriPend.Location = new System.Drawing.Point(71,115);
			this.textPriPend.Name = "textPriPend";
			this.textPriPend.ReadOnly = true;
			this.textPriPend.Size = new System.Drawing.Size(60,20);
			this.textPriPend.TabIndex = 43;
			this.textPriPend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriRem
			// 
			this.textPriRem.BackColor = System.Drawing.Color.White;
			this.textPriRem.Location = new System.Drawing.Point(71,135);
			this.textPriRem.Name = "textPriRem";
			this.textPriRem.ReadOnly = true;
			this.textPriRem.Size = new System.Drawing.Size(60,20);
			this.textPriRem.TabIndex = 42;
			this.textPriRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriMax
			// 
			this.textPriMax.BackColor = System.Drawing.Color.White;
			this.textPriMax.Location = new System.Drawing.Point(71,35);
			this.textPriMax.Name = "textPriMax";
			this.textPriMax.ReadOnly = true;
			this.textPriMax.Size = new System.Drawing.Size(60,20);
			this.textPriMax.TabIndex = 38;
			this.textPriMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecRem
			// 
			this.textSecRem.BackColor = System.Drawing.Color.White;
			this.textSecRem.Location = new System.Drawing.Point(130,135);
			this.textSecRem.Name = "textSecRem";
			this.textSecRem.ReadOnly = true;
			this.textSecRem.Size = new System.Drawing.Size(60,20);
			this.textSecRem.TabIndex = 46;
			this.textSecRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(73,16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(60,15);
			this.label10.TabIndex = 31;
			this.label10.Text = "Primary";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textSecPend
			// 
			this.textSecPend.BackColor = System.Drawing.Color.White;
			this.textSecPend.Location = new System.Drawing.Point(130,115);
			this.textSecPend.Name = "textSecPend";
			this.textSecPend.ReadOnly = true;
			this.textSecPend.Size = new System.Drawing.Size(60,20);
			this.textSecPend.TabIndex = 47;
			this.textSecPend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(4,37);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(66,15);
			this.label11.TabIndex = 32;
			this.label11.Text = "Annual Max";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(2,77);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(69,15);
			this.label18.TabIndex = 50;
			this.label18.Text = "Ded Remain";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(4,57);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(66,15);
			this.label12.TabIndex = 33;
			this.label12.Text = "Deductible";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(4,97);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(66,15);
			this.label13.TabIndex = 34;
			this.label13.Text = "Ins Used";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSecDedRem
			// 
			this.textSecDedRem.BackColor = System.Drawing.Color.White;
			this.textSecDedRem.Location = new System.Drawing.Point(130,75);
			this.textSecDedRem.Name = "textSecDedRem";
			this.textSecDedRem.ReadOnly = true;
			this.textSecDedRem.Size = new System.Drawing.Size(60,20);
			this.textSecDedRem.TabIndex = 52;
			this.textSecDedRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(4,137);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(66,15);
			this.label14.TabIndex = 35;
			this.label14.Text = "Remaining";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(4,117);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(66,15);
			this.label15.TabIndex = 36;
			this.label15.Text = "Pending";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSecMax
			// 
			this.textSecMax.BackColor = System.Drawing.Color.White;
			this.textSecMax.Location = new System.Drawing.Point(130,35);
			this.textSecMax.Name = "textSecMax";
			this.textSecMax.ReadOnly = true;
			this.textSecMax.Size = new System.Drawing.Size(60,20);
			this.textSecMax.TabIndex = 41;
			this.textSecMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(130,16);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(60,14);
			this.label16.TabIndex = 37;
			this.label16.Text = "Secondary";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textSecDed
			// 
			this.textSecDed.BackColor = System.Drawing.Color.White;
			this.textSecDed.Location = new System.Drawing.Point(130,55);
			this.textSecDed.Name = "textSecDed";
			this.textSecDed.ReadOnly = true;
			this.textSecDed.Size = new System.Drawing.Size(60,20);
			this.textSecDed.TabIndex = 40;
			this.textSecDed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecUsed
			// 
			this.textSecUsed.BackColor = System.Drawing.Color.White;
			this.textSecUsed.Location = new System.Drawing.Point(130,95);
			this.textSecUsed.Name = "textSecUsed";
			this.textSecUsed.ReadOnly = true;
			this.textSecUsed.Size = new System.Drawing.Size(60,20);
			this.textSecUsed.TabIndex = 39;
			this.textSecUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// groupBoxFamilyIns
			// 
			this.groupBoxFamilyIns.Controls.Add(this.textFamPriMax);
			this.groupBoxFamilyIns.Controls.Add(this.textFamPriDed);
			this.groupBoxFamilyIns.Controls.Add(this.label4);
			this.groupBoxFamilyIns.Controls.Add(this.label8);
			this.groupBoxFamilyIns.Controls.Add(this.textFamSecMax);
			this.groupBoxFamilyIns.Controls.Add(this.label9);
			this.groupBoxFamilyIns.Controls.Add(this.textFamSecDed);
			this.groupBoxFamilyIns.Controls.Add(this.label17);
			this.groupBoxFamilyIns.Location = new System.Drawing.Point(556,64);
			this.groupBoxFamilyIns.Name = "groupBoxFamilyIns";
			this.groupBoxFamilyIns.Size = new System.Drawing.Size(193,80);
			this.groupBoxFamilyIns.TabIndex = 218;
			this.groupBoxFamilyIns.TabStop = false;
			this.groupBoxFamilyIns.Text = "Family Insurance";
			this.groupBoxFamilyIns.Visible = false;
			// 
			// textFamPriMax
			// 
			this.textFamPriMax.BackColor = System.Drawing.Color.White;
			this.textFamPriMax.Location = new System.Drawing.Point(72,35);
			this.textFamPriMax.Name = "textFamPriMax";
			this.textFamPriMax.ReadOnly = true;
			this.textFamPriMax.Size = new System.Drawing.Size(60,20);
			this.textFamPriMax.TabIndex = 69;
			this.textFamPriMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textFamPriDed
			// 
			this.textFamPriDed.BackColor = System.Drawing.Color.White;
			this.textFamPriDed.Location = new System.Drawing.Point(72,55);
			this.textFamPriDed.Name = "textFamPriDed";
			this.textFamPriDed.ReadOnly = true;
			this.textFamPriDed.Size = new System.Drawing.Size(60,20);
			this.textFamPriDed.TabIndex = 65;
			this.textFamPriDed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(74,16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(60,15);
			this.label4.TabIndex = 66;
			this.label4.Text = "Primary";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(4,37);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(66,15);
			this.label8.TabIndex = 67;
			this.label8.Text = "Annual Max";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFamSecMax
			// 
			this.textFamSecMax.BackColor = System.Drawing.Color.White;
			this.textFamSecMax.Location = new System.Drawing.Point(131,35);
			this.textFamSecMax.Name = "textFamSecMax";
			this.textFamSecMax.ReadOnly = true;
			this.textFamSecMax.Size = new System.Drawing.Size(60,20);
			this.textFamSecMax.TabIndex = 70;
			this.textFamSecMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(131,16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(60,14);
			this.label9.TabIndex = 68;
			this.label9.Text = "Secondary";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textFamSecDed
			// 
			this.textFamSecDed.BackColor = System.Drawing.Color.White;
			this.textFamSecDed.Location = new System.Drawing.Point(131,55);
			this.textFamSecDed.Name = "textFamSecDed";
			this.textFamSecDed.ReadOnly = true;
			this.textFamSecDed.Size = new System.Drawing.Size(60,20);
			this.textFamSecDed.TabIndex = 64;
			this.textFamSecDed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(4,57);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(66,15);
			this.label17.TabIndex = 63;
			this.label17.Text = "Fam Ded";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// gridPayPlan
			// 
			this.gridPayPlan.HScrollVisible = false;
			this.gridPayPlan.Location = new System.Drawing.Point(0,144);
			this.gridPayPlan.Name = "gridPayPlan";
			this.gridPayPlan.ScrollValue = 0;
			this.gridPayPlan.Size = new System.Drawing.Size(749,93);
			this.gridPayPlan.TabIndex = 217;
			this.gridPayPlan.Title = "Payment Plans";
			this.gridPayPlan.TranslationName = "TablePaymentPlans";
			this.gridPayPlan.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPayPlan_CellDoubleClick);
			// 
			// butCreditCard
			// 
			this.butCreditCard.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCreditCard.Autosize = true;
			this.butCreditCard.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCreditCard.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCreditCard.CornerRadius = 4F;
			this.butCreditCard.Location = new System.Drawing.Point(22,103);
			this.butCreditCard.Name = "butCreditCard";
			this.butCreditCard.Size = new System.Drawing.Size(137,26);
			this.butCreditCard.TabIndex = 216;
			this.butCreditCard.Text = "Credit Card Manage";
			this.butCreditCard.UseVisualStyleBackColor = true;
			this.butCreditCard.Click += new System.EventHandler(this.butCreditCard_Click);
			// 
			// textUrgFinNote
			// 
			this.textUrgFinNote.AcceptsReturn = true;
			this.textUrgFinNote.BackColor = System.Drawing.Color.White;
			this.textUrgFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textUrgFinNote.ForeColor = System.Drawing.Color.Red;
			this.textUrgFinNote.Location = new System.Drawing.Point(0,20);
			this.textUrgFinNote.Multiline = true;
			this.textUrgFinNote.Name = "textUrgFinNote";
			this.textUrgFinNote.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textUrgFinNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textUrgFinNote.Size = new System.Drawing.Size(178,77);
			this.textUrgFinNote.TabIndex = 11;
			this.textUrgFinNote.TextChanged += new System.EventHandler(this.textUrgFinNote_TextChanged);
			this.textUrgFinNote.Leave += new System.EventHandler(this.textUrgFinNote_Leave);
			// 
			// gridAcctPat
			// 
			this.gridAcctPat.HScrollVisible = false;
			this.gridAcctPat.Location = new System.Drawing.Point(0,135);
			this.gridAcctPat.Name = "gridAcctPat";
			this.gridAcctPat.ScrollValue = 0;
			this.gridAcctPat.SelectedRowColor = System.Drawing.Color.DarkSalmon;
			this.gridAcctPat.Size = new System.Drawing.Size(178,121);
			this.gridAcctPat.TabIndex = 72;
			this.gridAcctPat.Title = "Select Patient";
			this.gridAcctPat.TranslationName = "TableAccountPat";
			this.gridAcctPat.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAcctPat_CellClick);
			// 
			// textFinNotes
			// 
			this.textFinNotes.AcceptsReturn = true;
			this.textFinNotes.Location = new System.Drawing.Point(0,278);
			this.textFinNotes.Multiline = true;
			this.textFinNotes.Name = "textFinNotes";
			this.textFinNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.FinancialNotes;
			this.textFinNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textFinNotes.Size = new System.Drawing.Size(178,78);
			this.textFinNotes.TabIndex = 70;
			this.textFinNotes.TextChanged += new System.EventHandler(this.textFinNotes_TextChanged);
			this.textFinNotes.Leave += new System.EventHandler(this.textFinNotes_Leave);
			// 
			// butToday
			// 
			this.butToday.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butToday.Autosize = true;
			this.butToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butToday.CornerRadius = 4F;
			this.butToday.Location = new System.Drawing.Point(95,54);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(77,24);
			this.butToday.TabIndex = 220;
			this.butToday.Text = "Today";
			this.butToday.Click += new System.EventHandler(this.butToday_Click);
			// 
			// butDatesAll
			// 
			this.butDatesAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDatesAll.Autosize = true;
			this.butDatesAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDatesAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDatesAll.CornerRadius = 4F;
			this.butDatesAll.Location = new System.Drawing.Point(95,132);
			this.butDatesAll.Name = "butDatesAll";
			this.butDatesAll.Size = new System.Drawing.Size(77,24);
			this.butDatesAll.TabIndex = 218;
			this.butDatesAll.Text = "All Dates";
			this.butDatesAll.Click += new System.EventHandler(this.butDatesAll_Click);
			// 
			// but90days
			// 
			this.but90days.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but90days.Autosize = true;
			this.but90days.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but90days.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but90days.CornerRadius = 4F;
			this.but90days.Location = new System.Drawing.Point(95,106);
			this.but90days.Name = "but90days";
			this.but90days.Size = new System.Drawing.Size(77,24);
			this.but90days.TabIndex = 217;
			this.but90days.Text = "Last 90 Days";
			this.but90days.Click += new System.EventHandler(this.but90days_Click);
			// 
			// but45days
			// 
			this.but45days.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but45days.Autosize = true;
			this.but45days.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but45days.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but45days.CornerRadius = 4F;
			this.but45days.Location = new System.Drawing.Point(95,80);
			this.but45days.Name = "but45days";
			this.but45days.Size = new System.Drawing.Size(77,24);
			this.but45days.TabIndex = 216;
			this.but45days.Text = "Last 45 Days";
			this.but45days.Click += new System.EventHandler(this.but45days_Click);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(95,158);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(77,24);
			this.butRefresh.TabIndex = 214;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(95,31);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(77,20);
			this.textDateEnd.TabIndex = 213;
			// 
			// textDateStart
			// 
			this.textDateStart.BackColor = System.Drawing.SystemColors.Window;
			this.textDateStart.ForeColor = System.Drawing.SystemColors.WindowText;
			this.textDateStart.Location = new System.Drawing.Point(95,8);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77,20);
			this.textDateStart.TabIndex = 212;
			// 
			// butShowNone
			// 
			this.butShowNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShowNone.Autosize = true;
			this.butShowNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowNone.CornerRadius = 4F;
			this.butShowNone.Location = new System.Drawing.Point(677,207);
			this.butShowNone.Name = "butShowNone";
			this.butShowNone.Size = new System.Drawing.Size(58,16);
			this.butShowNone.TabIndex = 216;
			this.butShowNone.Text = "None";
			this.butShowNone.Visible = false;
			this.butShowNone.Click += new System.EventHandler(this.butShowNone_Click);
			// 
			// butShowAll
			// 
			this.butShowAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShowAll.Autosize = true;
			this.butShowAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowAll.CornerRadius = 4F;
			this.butShowAll.Location = new System.Drawing.Point(614,207);
			this.butShowAll.Name = "butShowAll";
			this.butShowAll.Size = new System.Drawing.Size(53,16);
			this.butShowAll.TabIndex = 215;
			this.butShowAll.Text = "All";
			this.butShowAll.Visible = false;
			this.butShowAll.Click += new System.EventHandler(this.butShowAll_Click);
			// 
			// gridProg
			// 
			this.gridProg.HScrollVisible = true;
			this.gridProg.Location = new System.Drawing.Point(3,0);
			this.gridProg.Name = "gridProg";
			this.gridProg.ScrollValue = 0;
			this.gridProg.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridProg.Size = new System.Drawing.Size(603,230);
			this.gridProg.TabIndex = 211;
			this.gridProg.Title = "Progress Notes";
			this.gridProg.TranslationName = "TableProg";
			this.gridProg.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridProg_CellDoubleClick);
			// 
			// gridRepeat
			// 
			this.gridRepeat.HScrollVisible = false;
			this.gridRepeat.Location = new System.Drawing.Point(0,63);
			this.gridRepeat.Name = "gridRepeat";
			this.gridRepeat.ScrollValue = 0;
			this.gridRepeat.Size = new System.Drawing.Size(749,75);
			this.gridRepeat.TabIndex = 74;
			this.gridRepeat.Title = "Repeating Charges";
			this.gridRepeat.TranslationName = "TableRepeatCharges";
			this.gridRepeat.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridRepeat_CellDoubleClick);
			// 
			// gridAccount
			// 
			this.gridAccount.HScrollVisible = true;
			this.gridAccount.Location = new System.Drawing.Point(0,243);
			this.gridAccount.Name = "gridAccount";
			this.gridAccount.ScrollValue = 0;
			this.gridAccount.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridAccount.Size = new System.Drawing.Size(749,179);
			this.gridAccount.TabIndex = 73;
			this.gridAccount.Title = "Patient Account";
			this.gridAccount.TranslationName = "TableAccount";
			this.gridAccount.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAccount_CellDoubleClick);
			this.gridAccount.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAccount_CellClick);
			// 
			// gridComm
			// 
			this.gridComm.HScrollVisible = false;
			this.gridComm.Location = new System.Drawing.Point(0,440);
			this.gridComm.Name = "gridComm";
			this.gridComm.ScrollValue = 0;
			this.gridComm.Size = new System.Drawing.Size(749,226);
			this.gridComm.TabIndex = 71;
			this.gridComm.Title = "Communications Log";
			this.gridComm.TranslationName = "TableCommLogAccount";
			this.gridComm.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridComm_CellDoubleClick);
			// 
			// butComm
			// 
			this.butComm.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butComm.Autosize = true;
			this.butComm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butComm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butComm.CornerRadius = 4F;
			this.butComm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butComm.Location = new System.Drawing.Point(3,31);
			this.butComm.Name = "butComm";
			this.butComm.Size = new System.Drawing.Size(98,26);
			this.butComm.TabIndex = 94;
			this.butComm.Text = "Questionnaire";
			this.butComm.Click += new System.EventHandler(this.butComm_Click);
			// 
			// butTrojan
			// 
			this.butTrojan.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTrojan.Autosize = true;
			this.butTrojan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTrojan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTrojan.CornerRadius = 4F;
			this.butTrojan.Location = new System.Drawing.Point(3,3);
			this.butTrojan.Name = "butTrojan";
			this.butTrojan.Size = new System.Drawing.Size(146,25);
			this.butTrojan.TabIndex = 93;
			this.butTrojan.Text = "Send Transaction to Trojan";
			this.butTrojan.Click += new System.EventHandler(this.butTrojan_Click);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939,25);
			this.ToolBarMain.TabIndex = 47;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// ContrAccount
			// 
			this.Controls.Add(this.groupBoxIndIns);
			this.Controls.Add(this.groupBoxFamilyIns);
			this.Controls.Add(this.gridPayPlan);
			this.Controls.Add(this.tabControlShow);
			this.Controls.Add(this.panelAging);
			this.Controls.Add(this.panelProgNotes);
			this.Controls.Add(this.gridRepeat);
			this.Controls.Add(this.gridAccount);
			this.Controls.Add(this.gridComm);
			this.Controls.Add(this.panelSplitter);
			this.Controls.Add(this.panelCommButs);
			this.Controls.Add(this.ToolBarMain);
			this.Name = "ContrAccount";
			this.Size = new System.Drawing.Size(939,732);
			this.Load += new System.EventHandler(this.ContrAccount_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrAccount_Layout);
			this.Resize += new System.EventHandler(this.ContrAccount_Resize);
			this.panelCommButs.ResumeLayout(false);
			this.panelProgNotes.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.panelAging.ResumeLayout(false);
			this.panelAging.PerformLayout();
			this.panelTotalOwes.ResumeLayout(false);
			this.tabControlShow.ResumeLayout(false);
			this.tabMain.ResumeLayout(false);
			this.tabMain.PerformLayout();
			this.tabShow.ResumeLayout(false);
			this.tabShow.PerformLayout();
			this.groupBoxIndIns.ResumeLayout(false);
			this.groupBoxIndIns.PerformLayout();
			this.groupBoxFamilyIns.ResumeLayout(false);
			this.groupBoxFamilyIns.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void InitializeOnStartup() {
			if(InitializedOnStartup && !ViewingInRecall) {
				return;
			}
			InitializedOnStartup=true;
			//can't use Lan.F(this);
			Lan.C(this,new Control[]
				{
          labelStartDate,
					labelEndDate,
					label2,
					label7,
					label6,
					label5,
					label3,
					labelUrgFinNote,
					labelFamFinancial,
					butRefresh,
					gridAccount,
					gridAcctPat,
					gridComm,
					labelInsEst,
					labelBalance,
					labelPatEstBal,
					labelUnearned,
					labelInsRem,
					tabMain,
					tabShow,
					butToday,
					but45days,
					but90days,
					butDatesAll,
					butRefresh,
					butComm
				});
			LayoutToolBar();
			if(ViewingInRecall) {
				panelSplitter.Top=300;//start the splitter higher for recall window.
			}
			LayoutPanels();
			checkShowFamilyComm.Checked=PrefC.GetBoolSilent(PrefName.ShowAccountFamilyCommEntries,true);
			Plugins.HookAddCode(this,"ContrAccount.InitializeOnStartup_end");
		}

		private void ContrAccount_Load(object sender,System.EventArgs e) {
			this.Parent.MouseWheel+=new MouseEventHandler(Parent_MouseWheel);
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ODToolBarButton button;
			button=new ODToolBarButton(Lan.g(this,"Payment"),1,"","Payment");
			//button.Style=ODToolBarButtonStyle.DropDownButton;
			//button.DropDownMenu=contextMenuPayment;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Adjustment"),2,"","Adjustment"));
			button=new ODToolBarButton(Lan.g(this,"New Claim"),3,"","Insurance");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=contextMenuIns;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Payment Plan"),-1,"","PayPlan"));
			if(!PrefC.GetBool(PrefName.EasyHideRepeatCharges)) {
				button=new ODToolBarButton(Lan.g(this,"Repeating Charge"),-1,"","RepeatCharge");
				button.Style=ODToolBarButtonStyle.DropDownButton;
				button.DropDownMenu=contextMenuRepeat;
				ToolBarMain.Buttons.Add(button);
			}
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton(Lan.g(this,"Statement"),4,"","Statement");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=contextMenuStatement;
			ToolBarMain.Buttons.Add(button);
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.AccountModule);
			for(int i=0;i<toolButItems.Count;i++) {
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
			Plugins.HookAddCode(this,"ContrAccount.LayoutToolBar_end",PatCur);
		}

		private void ContrAccount_Layout(object sender,System.Windows.Forms.LayoutEventArgs e) {
			//see LayoutPanels()
		}

		private void ContrAccount_Resize(object sender,EventArgs e) {
			if(PrefC.HListIsNull()){
				return;//helps on startup.
			}
			LayoutPanels();
		}

		///<summary>This used to be a layout event, but that was making it get called far too frequently.  Now, this must explicitly and intelligently be called.</summary>
		private void LayoutPanels(){
			gridAccount.Height=panelSplitter.Top-gridAccount.Location.Y+1;
			gridAccount.Invalidate();
			gridComm.Top=panelSplitter.Bottom-1;
			gridComm.Height=Height-gridComm.Top;
			gridComm.Invalidate();
			panelCommButs.Top=panelSplitter.Bottom-1;
			panelProgNotes.Top=panelSplitter.Bottom-1;
			panelProgNotes.Height=Height-panelProgNotes.Top;
			gridProg.Top=0;
			gridProg.Height=panelProgNotes.Height;
			/*
			panelBoldBalance.Left=329;
			panelBoldBalance.Top=29;
			panelInsInfoDetail.Top = panelBoldBalance.Top + panelBoldBalance.Height;
			panelInsInfoDetail.Left = panelBoldBalance.Left + panelBoldBalance.Width - panelInsInfoDetail.Width;*/
			int left=textUrgFinNote.Left;//769;
			labelFamFinancial.Location=new Point(left,gridAcctPat.Bottom);
			textFinNotes.Location=new Point(left,labelFamFinancial.Bottom);
			tabControlShow.Height=panelCommButs.Top-tabControlShow.Top;
			textFinNotes.Height=tabMain.Height-textFinNotes.Top;
		}

		///<summary></summary>
		public void ModuleSelected(long patNum) {
			ModuleSelected(patNum,false);
		}

		///<summary></summary>
		public void ModuleSelected(long patNum,bool isSelectingFamily) {
			RefreshModuleData(patNum,isSelectingFamily);
			RefreshModuleScreen(isSelectingFamily);
			Plugins.HookAddCode(this,"ContrAccount.ModuleSelected_end",patNum,isSelectingFamily);
		}

		///<summary>Used when jumping to this module and directly to a claim.</summary>
		public void ModuleSelected(long patNum,long claimNum) {
			ModuleSelected(patNum);
/*
			for(int i=0;i<AcctLineList.Count;i++){
				if(AcctLineList[i].Type != AcctModType.Claim){
					continue;
				}
				if(arrayClaim[AcctLineList[i].Index].ClaimNum!=claimNum){
					continue;
				}
				gridAccount.SetSelected(i,true);
			}
 */
		}

		///<summary></summary>
		public void ModuleUnselected() {
			if(FamCur==null)
				return;
			if(UrgFinNoteChanged) {
				//Patient tempPat=Patients.Cur;
				Patient patOld=FamCur.ListPats[0].Copy();
				//Patients.CurOld=Patients.Cur.Clone();//important
				FamCur.ListPats[0].FamFinUrgNote=textUrgFinNote.Text;
				Patients.Update(FamCur.ListPats[0],patOld);
				//Patients.GetFamily(tempPat.PatNum);
				UrgFinNoteChanged=false;
			}
			if(FinNoteChanged) {
				PatientNoteCur.FamFinancial=textFinNotes.Text;
				PatientNotes.Update(PatientNoteCur, PatCur.Guarantor);
				FinNoteChanged=false;
			}
			//if(CCChanged){
			//  CCSave();
			//  CCChanged=false;
			//}
			FamCur=null;
			RepeatChargeList=null;
			Plugins.HookAddCode(this,"ContrAccount.ModuleUnselected_end");
		}

		///<summary></summary>
		private void RefreshModuleData(long patNum,bool isSelectingFamily) {
			if (patNum == 0)
			{
				PatCur=null;
				FamCur=null;
				DataSetMain=null;
				Plugins.HookAddCode(this,"ContrAccount.RefreshModuleData_null");
				return;
			}
			DateTime fromDate=DateTime.MinValue;
			DateTime toDate=DateTime.MaxValue;
			if(textDateStart.errorProvider1.GetError(textDateStart)==""
				&& textDateEnd.errorProvider1.GetError(textDateEnd)=="")
			{
				if(textDateStart.Text!=""){
					fromDate=PIn.Date(textDateStart.Text);
				}
				if(textDateEnd.Text!=""){
					toDate=PIn.Date(textDateEnd.Text);
				}
			}
			bool viewingInRecall=ViewingInRecall;
			if(PrefC.GetBool(PrefName.FuchsOptionsOn)) {
				panelTotalOwes.Top=-38;
				viewingInRecall=true;
			}
			DataSetMain=AccountModules.GetAll(patNum,viewingInRecall,fromDate,toDate,isSelectingFamily,checkShowDetail.Checked,true);
			FamCur=Patients.GetFamily(patNum);//for now, have to get family after dataset due to aging calc.
			PatCur=FamCur.GetPatient(patNum);
			PatientNoteCur=PatientNotes.Refresh(PatCur.PatNum,PatCur.Guarantor);
			FillSummary();
			Plugins.HookAddCode(this,"ContrAccount.RefreshModuleData_end",FamCur,PatCur,DataSetMain,PPBalanceTotal,isSelectingFamily);
		}

		private void RefreshModuleScreen(bool isSelectingFamily) {
			if(PatCur==null) {
				gridAccount.Enabled=false;
				ToolBarMain.Buttons["Payment"].Enabled=false;
				ToolBarMain.Buttons["Adjustment"].Enabled=false;
				ToolBarMain.Buttons["Insurance"].Enabled=false;
				ToolBarMain.Buttons["PayPlan"].Enabled=false;
				ToolBarMain.Buttons["Statement"].Enabled=false;
				ToolBarMain.Invalidate();
				textUrgFinNote.Enabled=false;
				textFinNotes.Enabled=false;
				//butComm.Enabled=false;
				tabControlShow.Enabled=false;
				Plugins.HookAddCode(this,"ContrAccount.RefreshModuleScreen_null");
			}
			else{
				gridAccount.Enabled=true;
				ToolBarMain.Buttons["Payment"].Enabled=true;
				ToolBarMain.Buttons["Adjustment"].Enabled=true;
				ToolBarMain.Buttons["Insurance"].Enabled=true;
				ToolBarMain.Buttons["PayPlan"].Enabled=true;
				ToolBarMain.Buttons["Statement"].Enabled=true;
				ToolBarMain.Invalidate();
				textUrgFinNote.Enabled=true;
				textFinNotes.Enabled=true;
				//butComm.Enabled=true;
				tabControlShow.Enabled=true;
			}
			FillPats(isSelectingFamily);
			FillMisc();
			FillAging(isSelectingFamily);
			FillRepeatCharges();//must be in this order. 1.
			FillPaymentPlans();//2.
			FillMain();//3.
			LayoutPanels();
			if(ViewingInRecall || PrefC.GetBoolSilent(PrefName.FuchsOptionsOn,false)) {
				panelProgNotes.Visible = true;
				FillProgNotes();
				if(PrefC.GetBool(PrefName.FuchsOptionsOn) && PatCur!=null){//show prog note options
					groupBox6.Visible = true;
					groupBox7.Visible = true;
					butShowAll.Visible = true;
					butShowNone.Visible = true;
					//FillInsInfo();
				}
			}
			else{
				panelProgNotes.Visible = false;
				FillComm();
			}
			Plugins.HookAddCode(this,"ContrAccount.RefreshModuleScreen_end",FamCur,PatCur,DataSetMain,PPBalanceTotal,isSelectingFamily);
		}

		//private void FillPatientButton() {
		//	Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),PatCur,FamCur);
		//}

		private void FillPats(bool isSelectingFamily) {
			if(PatCur==null) {
				gridAcctPat.BeginUpdate();
				gridAcctPat.Rows.Clear();
				gridAcctPat.EndUpdate();
				return;
			}
			gridAcctPat.BeginUpdate();
			gridAcctPat.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableAccountPat","Patient"),105);
			gridAcctPat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAccountPat","Bal"),49,HorizontalAlignment.Right);
			gridAcctPat.Columns.Add(col);
			gridAcctPat.Rows.Clear();
			ODGridRow row;
			DataTable table=DataSetMain.Tables["patient"];
			double bal=0;
			for(int i=0;i<table.Rows.Count;i++) {
				bal+=(double)table.Rows[i]["balanceDouble"];
				row = new ODGridRow();
				row.Cells.Add(table.Rows[i]["name"].ToString());
				row.Cells.Add(table.Rows[i]["balance"].ToString());
				if(i==0 || i==table.Rows.Count-1) {
					row.Bold=true;
				}
				gridAcctPat.Rows.Add(row);
			}
			gridAcctPat.EndUpdate();
			if(isSelectingFamily){
				gridAcctPat.SetSelected(FamCur.ListPats.Length,true);
			}
			else{
				for(int i=0;i<FamCur.ListPats.Length;i++) {
					if(FamCur.ListPats[i].PatNum==PatCur.PatNum) {
						gridAcctPat.SetSelected(i,true);
					}
				}
			}
			if(isSelectingFamily){
				ToolBarMain.Buttons["Insurance"].Enabled=false;
			}
			else{
				ToolBarMain.Buttons["Insurance"].Enabled=true;
			}
		}

		private void FillMisc() {
			//textCC.Text="";
			//textCCexp.Text="";
			if(PatCur==null) {
				textUrgFinNote.Text="";
				textFinNotes.Text="";
			}
			else{
				textUrgFinNote.Text=FamCur.ListPats[0].FamFinUrgNote;
				textFinNotes.Text=PatientNoteCur.FamFinancial;
				textFinNotes.Select(textFinNotes.Text.Length+2,1);
				textFinNotes.ScrollToCaret();
				textUrgFinNote.SelectionStart=0;
				textUrgFinNote.ScrollToCaret();
				//if(PrefC.GetBool(PrefName.StoreCCnumbers)) {
					//string cc=PatientNoteCur.CCNumber;
					//if(Regex.IsMatch(cc,@"^\d{16}$")){
					//  textCC.Text=cc.Substring(0,4)+"-"+cc.Substring(4,4)+"-"+cc.Substring(8,4)+"-"+cc.Substring(12,4);
					//}
					//else{
					//  textCC.Text=cc;
					//}
					//if(PatientNoteCur.CCExpiration.Year>2000){
					//  textCCexp.Text=PatientNoteCur.CCExpiration.ToString("MM/yy");
					//}
					//else{
					//  textCCexp.Text="";
					//}
				//}
			}
			UrgFinNoteChanged=false;
			FinNoteChanged=false;
			CCChanged=false;
			if(ViewingInRecall) {
				textUrgFinNote.ReadOnly=true;
				textFinNotes.ReadOnly=true;
			}
			else {
				textUrgFinNote.ReadOnly=false;
				textFinNotes.ReadOnly=false;
			}
		}

		private void FillAging(bool isSelectingFamily) {
			if(Plugins.HookMethod(this,"ContrAccount.FillAging",FamCur,PatCur,DataSetMain,isSelectingFamily)) {
				return;
			}
			if(PatCur!=null) {
				textOver90.Text=FamCur.ListPats[0].BalOver90.ToString("F");
				text61_90.Text=FamCur.ListPats[0].Bal_61_90.ToString("F");
				text31_60.Text=FamCur.ListPats[0].Bal_31_60.ToString("F");
				text0_30.Text=FamCur.ListPats[0].Bal_0_30.ToString("F");
				double total=FamCur.ListPats[0].BalTotal;
				labelTotalAmt.Text=total.ToString("F");
				labelInsEstAmt.Text=FamCur.ListPats[0].InsEst.ToString("F");
				labelBalanceAmt.Text = (total - FamCur.ListPats[0].InsEst).ToString("f");
				labelPatEstBalAmt.Text="";
				DataTable tableMisc=DataSetMain.Tables["misc"];
				if(!isSelectingFamily){
					for(int i=0;i<tableMisc.Rows.Count;i++){
						if(tableMisc.Rows[i]["descript"].ToString()=="patInsEst"){
							double estBal=PatCur.EstBalance-PIn.Double(tableMisc.Rows[i]["value"].ToString());
							labelPatEstBalAmt.Text=estBal.ToString("F");
						}
					}
				}
				labelUnearnedAmt.Text="";
				for(int i=0;i<tableMisc.Rows.Count;i++){
					if(tableMisc.Rows[i]["descript"].ToString()=="unearnedIncome") {
						double unearned=PIn.Double(tableMisc.Rows[i]["value"].ToString());
						if(unearned!=0) {
							labelUnearnedAmt.Text=unearned.ToString("F");
						}
					}
				}
				//labelInsLeft.Text=Lan.g(this,"Ins Left");
				//labelInsLeftAmt.Text="";//etc. Will be same for everyone
				Font fontBold=new Font(FontFamily.GenericSansSerif,11,FontStyle.Bold);
				//In the new way of doing it, they are all visible and calculated identically,
				//but the emphasis simply changes by slight renaming of labels
				//and by font size changes.
				if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)){
					labelTotal.Text=Lan.g(this,"Balance");
					labelTotalAmt.Font=fontBold;
					labelTotalAmt.ForeColor=Color.Firebrick;
					panelAgeLine.Visible=true;//verical line
					labelInsEst.Text=Lan.g(this,"Ins Pending");
					labelBalance.Text=Lan.g(this,"After Ins");
					labelBalanceAmt.Font=this.Font;
					labelBalanceAmt.ForeColor=Color.Black;
				}
				else{//this is more common
					labelTotal.Text=Lan.g(this,"Total");
					labelTotalAmt.Font=this.Font;
					labelTotalAmt.ForeColor = Color.Black;
					panelAgeLine.Visible=false;
					labelInsEst.Text=Lan.g(this,"-InsEst");
					labelBalance.Text=Lan.g(this,"=Est Bal");
					labelBalanceAmt.Font=fontBold;
					labelBalanceAmt.ForeColor=Color.Firebrick;
					if(PrefC.GetBool(PrefName.FuchsOptionsOn)){
						labelTotal.Text=Lan.g(this,"Balance");
						labelBalance.Text=Lan.g(this,"=Owed Now");
						labelTotalAmt.Font = fontBold;
					}
				}
			}
			else {
				textOver90.Text="";
				text61_90.Text="";
				text31_60.Text="";
				text0_30.Text="";
				labelTotalAmt.Text="";
				labelInsEstAmt.Text="";
				labelBalanceAmt.Text="";
				labelPatEstBalAmt.Text="";
				labelUnearnedAmt.Text="";
				//labelInsLeftAmt.Text="";
			}
		}

		///<summary></summary>
		private void FillRepeatCharges() {
			if(PatCur==null) {
				gridRepeat.Visible=false;
				return;
			}
			RepeatChargeList=RepeatCharges.Refresh(PatCur.PatNum);
			if(RepeatChargeList.Length==0) {
				gridRepeat.Visible=false;
				return;
			}
			gridRepeat.Visible=true;
			gridRepeat.Height=75;
			gridRepeat.BeginUpdate();
			gridRepeat.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRepeatCharges","Description"),150);
			gridRepeat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRepeatCharges","Amount"),70,HorizontalAlignment.Right);
			gridRepeat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRepeatCharges","Start Date"),90);
			gridRepeat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRepeatCharges","Stop Date"),90);
			gridRepeat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRepeatCharges","Note"),350);
			gridRepeat.Columns.Add(col);
			gridRepeat.Rows.Clear();
			UI.ODGridRow row;
			ProcedureCode procCode;
			for(int i=0;i<RepeatChargeList.Length;i++) {
				row=new ODGridRow();
				procCode=ProcedureCodes.GetProcCode(RepeatChargeList[i].ProcCode);
				row.Cells.Add(procCode.Descript);
				row.Cells.Add(RepeatChargeList[i].ChargeAmt.ToString("F"));
				if(RepeatChargeList[i].DateStart.Year>1880) {
					row.Cells.Add(RepeatChargeList[i].DateStart.ToShortDateString());
				}
				else {
					row.Cells.Add("");
				}
				if(RepeatChargeList[i].DateStop.Year>1880) {
					row.Cells.Add(RepeatChargeList[i].DateStop.ToShortDateString());
				}
				else {
					row.Cells.Add("");
				}
				row.Cells.Add(RepeatChargeList[i].Note);
				gridRepeat.Rows.Add(row);
			}
			gridRepeat.EndUpdate();
		}

		private void FillPaymentPlans(){
			PPBalanceTotal=0;
			if(PatCur==null) {
				gridPayPlan.Visible=false;
				return;
			}
			DataTable table=DataSetMain.Tables["payplan"];
			if (table.Rows.Count == 0) {
				gridPayPlan.Visible=false;
				return;
			}
			if(gridRepeat.Visible){
				gridPayPlan.Location=new Point(0,gridRepeat.Bottom+3);
			}
			else{
				gridPayPlan.Location=gridRepeat.Location;
			}
			gridPayPlan.Visible = true;
			gridPayPlan.Height=100;
			gridPayPlan.BeginUpdate();
			gridPayPlan.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePaymentPlans","Date"),65);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Guarantor"),100);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Patient"),100);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Ins"),30,HorizontalAlignment.Center);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Principal"),70,HorizontalAlignment.Right);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Total Cost"),70,HorizontalAlignment.Right);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Paid"),70,HorizontalAlignment.Right);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","PrincPaid"),70,HorizontalAlignment.Right);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Balance"),70,HorizontalAlignment.Right);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Due Now"),70,HorizontalAlignment.Right);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn("",70);//filler
			gridPayPlan.Columns.Add(col);
			gridPayPlan.Rows.Clear();
			UI.ODGridRow row;
			UI.ODGridCell cell;
			double PPDueTotal=0;
			for(int i=0;i<table.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["date"].ToString());
				row.Cells.Add(table.Rows[i]["guarantor"].ToString());
				row.Cells.Add(table.Rows[i]["patient"].ToString());
				row.Cells.Add(table.Rows[i]["isIns"].ToString());
				row.Cells.Add(table.Rows[i]["principal"].ToString());
				row.Cells.Add(table.Rows[i]["totalCost"].ToString());
				row.Cells.Add(table.Rows[i]["paid"].ToString());
				row.Cells.Add(table.Rows[i]["princPaid"].ToString());
				row.Cells.Add(table.Rows[i]["balance"].ToString());
				cell=new ODGridCell(table.Rows[i]["due"].ToString());
				if(table.Rows[i]["isIns"].ToString()==""){
					cell.Bold=YN.Yes;
					cell.ColorText=Color.Red;
				}
				row.Cells.Add(cell);
				row.Cells.Add("");
				gridPayPlan.Rows.Add(row);
				PPBalanceTotal += (Convert.ToDouble((table.Rows[i]["balance"]).ToString()));
				PPDueTotal += (Convert.ToDouble((table.Rows[i]["due"]).ToString()));
			}
			gridPayPlan.EndUpdate();
			if(PrefC.GetBool(PrefName.FuchsOptionsOn)) {
				panelTotalOwes.Top=1;
				labelTotalPtOwes.Text=(PPBalanceTotal + FamCur.ListPats[0].BalTotal -FamCur.ListPats[0].InsEst).ToString("F");
			}
		}

		/// <summary>Fills the commlog grid on this form.  It does not refresh the data from the database.</summary>
		private void FillComm() {
			if (DataSetMain == null) {
				gridComm.BeginUpdate();
				gridComm.Rows.Clear();
				gridComm.EndUpdate();
				panelCommButs.Enabled = false;
				return;
			}
			panelCommButs.Enabled = true;
			gridComm.BeginUpdate();
			gridComm.Columns.Clear();
			ODGridColumn col = new ODGridColumn(Lan.g("TableCommLogAccount", "Date"), 70);
			gridComm.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableCommLogAccount","Time"),42);//,HorizontalAlignment.Right);
			gridComm.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableCommLogAccount","Name"),80);
			gridComm.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableCommLogAccount", "Type"), 80);
			gridComm.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableCommLogAccount", "Mode"), 55);
			gridComm.Columns.Add(col);
			//col = new ODGridColumn(Lan.g("TableCommLogAccount", "Sent/Recd"), 75);
			//gridComm.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableCommLogAccount", "Note"), 455);
			gridComm.Columns.Add(col);
			gridComm.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			DataTable table = DataSetMain.Tables["Commlog"];
			for (int i = 0; i < table.Rows.Count; i++) {
				//Skip commlog entries which belong to other family members per user option.
				if(!this.checkShowFamilyComm.Checked && table.Rows[i]["patName"].ToString()!=""){
					continue;
				}
				row = new ODGridRow();
				row.Cells.Add(table.Rows[i]["commDate"].ToString());
				row.Cells.Add(table.Rows[i]["commTime"].ToString());
				row.Cells.Add(table.Rows[i]["patName"].ToString());
				row.Cells.Add(table.Rows[i]["commType"].ToString());
				row.Cells.Add(table.Rows[i]["mode"].ToString());
				//row.Cells.Add(table.Rows[i]["sentOrReceived"].ToString());
				row.Cells.Add(table.Rows[i]["Note"].ToString());
				gridComm.Rows.Add(row);
			}
			gridComm.EndUpdate();
			gridComm.ScrollToEnd();
		}

		private void FillMain(){
			if(gridPayPlan.Visible){
				gridAccount.Location=new Point(0,gridPayPlan.Bottom+3);
			}
			else if(gridRepeat.Visible){
				gridAccount.Location=new Point(0,gridRepeat.Bottom+3);
			}
			else{
				gridAccount.Location=gridRepeat.Location;
			}
			gridAccount.BeginUpdate();
			gridAccount.Columns.Clear();
			ODGridColumn col;
			fieldsForMainGrid=DisplayFields.GetForCategory(DisplayFieldCategory.AccountModule);
			HorizontalAlignment align;
			for(int i=0;i<fieldsForMainGrid.Count;i++) {
				align=HorizontalAlignment.Left;
				if(fieldsForMainGrid[i].InternalName=="Charges"
					|| fieldsForMainGrid[i].InternalName=="Credits"
					|| fieldsForMainGrid[i].InternalName=="Balance") 
				{
					align=HorizontalAlignment.Right;
				}
				if(fieldsForMainGrid[i].Description=="") {
					col=new ODGridColumn(fieldsForMainGrid[i].InternalName,fieldsForMainGrid[i].ColumnWidth,align);
				}
				else {
					col=new ODGridColumn(fieldsForMainGrid[i].Description,fieldsForMainGrid[i].ColumnWidth,align);
				}
				gridAccount.Columns.Add(col);
			}
			gridAccount.Rows.Clear();
			ODGridRow row;
			DataTable table=null;
			if(PatCur==null){
				table=new DataTable();
			}
			else{
				table=DataSetMain.Tables["account"];
			}
			for(int i=0;i<table.Rows.Count;i++) {
				row=new ODGridRow();
				for(int f=0;f<fieldsForMainGrid.Count;f++) {
					switch(fieldsForMainGrid[f].InternalName) {
						case "Date":
							row.Cells.Add(table.Rows[i]["date"].ToString());
							break;
						case "Patient":
							row.Cells.Add(table.Rows[i]["patient"].ToString());
							break;
						case "Prov":
							row.Cells.Add(table.Rows[i]["prov"].ToString());
							break;
						case "Clinic":
							row.Cells.Add(table.Rows[i]["clinic"].ToString());
							break;
						case "Code":
							row.Cells.Add(table.Rows[i]["ProcCode"].ToString());
							break;
						case "Tth":
							row.Cells.Add(table.Rows[i]["tth"].ToString());
							break;
						case "Description":
							row.Cells.Add(table.Rows[i]["description"].ToString());
							break;
						case "Charges":
							row.Cells.Add(table.Rows[i]["charges"].ToString());
							break;
						case "Credits":
							row.Cells.Add(table.Rows[i]["credits"].ToString());
							break;
						case "Balance":
							row.Cells.Add(table.Rows[i]["balance"].ToString());
							break;
					}
				}
				row.ColorText=Color.FromArgb(PIn.Int(table.Rows[i]["colorText"].ToString()));
				if(i==table.Rows.Count-1//last row
					|| (DateTime)table.Rows[i]["DateTime"]!=(DateTime)table.Rows[i+1]["DateTime"])
				{
					row.ColorLborder=Color.Black;
				}
				gridAccount.Rows.Add(row);
			}
			gridAccount.EndUpdate();
			if(Actscrollval==0) {
				gridAccount.ScrollToEnd();
			}
			else {
				gridAccount.ScrollValue=Actscrollval;
				Actscrollval=0;
			}
		}

		private void FillSummary() {
			textFamPriMax.Text="";
			textFamPriDed.Text="";
			textFamSecMax.Text="";
			textFamSecDed.Text="";
			textPriMax.Text="";
			textPriDed.Text="";
			textPriDedRem.Text="";
			textPriUsed.Text="";
			textPriPend.Text="";
			textPriRem.Text="";
			textSecMax.Text="";
			textSecDed.Text="";
			textSecDedRem.Text="";
			textSecUsed.Text="";
			textSecPend.Text="";
			textSecRem.Text="";
			if(PatCur==null) {
				return;
			}
			double maxFam=0;
			double maxInd=0;
			double ded=0;
			double dedFam=0;
			double dedUsed=0;
			double remain=0;
			double pend=0;
			double used=0;
			InsPlan PlanCur;//=new InsPlan();
			List<InsSub> subList=InsSubs.RefreshForFam(FamCur);
			List<InsPlan> InsPlanList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			List<Benefit> BenefitList=Benefits.Refresh(PatPlanList);
			List<Claim> ClaimList=Claims.Refresh(PatCur.PatNum);
			List<ClaimProcHist> HistList=ClaimProcs.GetHistList(PatCur.PatNum,BenefitList,PatPlanList,InsPlanList,DateTime.Today);
			if(PatPlanList.Count>0) {
				PlanCur=InsPlans.GetPlan(PatPlanList[0].PlanNum,InsPlanList);
				pend=InsPlans.GetPendingDisplay(HistList,DateTime.Today,PlanCur,PatPlanList[0].PatPlanNum,-1,PatCur.PatNum,PatPlanList[0].InsSubNum);
				used=InsPlans.GetInsUsedDisplay(HistList,DateTime.Today,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,-1,InsPlanList,BenefitList,PatCur.PatNum,PatPlanList[0].InsSubNum);
				textPriPend.Text=pend.ToString("F");
				textPriUsed.Text=used.ToString("F");
				maxFam=Benefits.GetAnnualMaxDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,true);
				maxInd=Benefits.GetAnnualMaxDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,false);
				if(maxFam==-1) {
					textFamPriMax.Text="";
				}
				else {
					textFamPriMax.Text=maxFam.ToString("F");
				}
				if(maxInd==-1) {//if annual max is blank
					textPriMax.Text="";
					textPriRem.Text="";
				}
				else {
					remain=maxInd-used-pend;
					if(remain<0) {
						remain=0;
					}
					//textFamPriMax.Text=max.ToString("F");
					textPriMax.Text=maxInd.ToString("F");
					textPriRem.Text=remain.ToString("F");
				}
				//deductible:
				ded=Benefits.GetDeductGeneralDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,BenefitCoverageLevel.Individual);
				dedFam=Benefits.GetDeductGeneralDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,BenefitCoverageLevel.Family);
				if(ded!=-1) {
					textPriDed.Text=ded.ToString("F");
					dedUsed=InsPlans.GetDedUsedDisplay(HistList,DateTime.Today,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,-1,InsPlanList,
						BenefitCoverageLevel.Individual,PatCur.PatNum);
					textPriDedRem.Text=(ded-dedUsed).ToString("F");
				}
				if(dedFam!=-1) {
					textFamPriDed.Text=dedFam.ToString("F");
				}
			}
			if(PatPlanList.Count>1) {
				PlanCur=InsPlans.GetPlan(PatPlanList[1].PlanNum,InsPlanList);
				pend=InsPlans.GetPendingDisplay(HistList,DateTime.Today,PlanCur,PatPlanList[1].PatPlanNum,-1,PatCur.PatNum,PatPlanList[1].InsSubNum);
				textSecPend.Text=pend.ToString("F");
				used=InsPlans.GetInsUsedDisplay(HistList,DateTime.Today,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,-1,InsPlanList,BenefitList,PatCur.PatNum,PatPlanList[1].InsSubNum);
				textSecUsed.Text=used.ToString("F");
				//max=Benefits.GetAnnualMaxDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum);
				maxFam=Benefits.GetAnnualMaxDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,true);
				maxInd=Benefits.GetAnnualMaxDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,false);
				if(maxFam==-1) {
					textFamSecMax.Text="";
				}
				else {
					textFamSecMax.Text=maxFam.ToString("F");
				}
				if(maxInd==-1) {//if annual max is blank
					textSecMax.Text="";
					textSecRem.Text="";
				}
				else {
					remain=maxInd-used-pend;
					if(remain<0) {
						remain=0;
					}
					//textFamSecMax.Text=max.ToString("F");
					textSecMax.Text=maxInd.ToString("F");
					textSecRem.Text=remain.ToString("F");
				}
				//deductible:
				ded=Benefits.GetDeductGeneralDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,BenefitCoverageLevel.Individual);
				dedFam=Benefits.GetDeductGeneralDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,BenefitCoverageLevel.Family);
				if(ded!=-1) {
					textSecDed.Text=ded.ToString("F");
					dedUsed=InsPlans.GetDedUsedDisplay(HistList,DateTime.Today,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,-1,InsPlanList,
						BenefitCoverageLevel.Individual,PatCur.PatNum);
					textSecDedRem.Text=(ded-dedUsed).ToString("F");
				}
				if(dedFam!=-1) {
					textFamSecDed.Text=dedFam.ToString("F");
				}
			}
		}		

		private void gridAccount_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			DataTable table=DataSetMain.Tables["account"];
			//this seems to fire after a doubleclick, so this prevents error:
			if(e.Row>=table.Rows.Count){
				return;
			}
			if(ViewingInRecall) return;
			if(table.Rows[e.Row]["ClaimNum"].ToString()!="0"){//claims and claimpayments
				//Claim ClaimCur=Claims.GetClaim(
				//	arrayClaim[AcctLineList[e.Row].Index];
				string[] procsOnClaim=table.Rows[e.Row]["procsOnObj"].ToString().Split(',');
				for(int i=0;i<table.Rows.Count;i++){//loop through all rows
					if(table.Rows[i]["ClaimNum"].ToString()==table.Rows[e.Row]["ClaimNum"].ToString()){
						gridAccount.SetSelected(i,true);//for the claim payments
					}
					else if(table.Rows[i]["ProcNum"].ToString()=="0"){//if not a procedure, then skip
						continue;
					}
					for(int j=0;j<procsOnClaim.Length;j++){
						if(table.Rows[i]["ProcNum"].ToString()==procsOnClaim[j]){
							gridAccount.SetSelected(i,true);
						}
					}
				}
			}
			if(table.Rows[e.Row]["PayNum"].ToString()!="0"){
				string[] procsOnPayment=table.Rows[e.Row]["procsOnObj"].ToString().Split(',');
				for(int i=0;i<table.Rows.Count;i++){//loop through all rows
					if(table.Rows[i]["PayNum"].ToString()==table.Rows[e.Row]["PayNum"].ToString()){
						gridAccount.SetSelected(i,true);//for other splits in family view
					}
					if(table.Rows[i]["ProcNum"].ToString()=="0"){//if not a procedure, then skip
						continue;
					}
					for(int j=0;j<procsOnPayment.Length;j++){
						if(table.Rows[i]["ProcNum"].ToString()==procsOnPayment[j]){
							gridAccount.SetSelected(i,true);
						}
					}
				}
			}
		}

		private void gridAccount_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(ViewingInRecall) return;
			Actscrollval=gridAccount.ScrollValue;
			DataTable table=DataSetMain.Tables["account"];
			if(table.Rows[e.Row]["ProcNum"].ToString()!="0"){
				Procedure proc=Procedures.GetOneProc(PIn.Long(table.Rows[e.Row]["ProcNum"].ToString()),true);
				Patient pat=FamCur.GetPatient(proc.PatNum);
				FormProcEdit FormPE=new FormProcEdit(proc,pat,FamCur);
				FormPE.ShowDialog();
			}
			else if(table.Rows[e.Row]["AdjNum"].ToString()!="0"){
				Adjustment adj=Adjustments.GetOne(PIn.Long(table.Rows[e.Row]["AdjNum"].ToString()));
				FormAdjust FormAdj=new FormAdjust(PatCur,adj);
				FormAdj.ShowDialog();
			}
			else if(table.Rows[e.Row]["PayNum"].ToString()!="0"){
				Payment PaymentCur=Payments.GetPayment(PIn.Long(table.Rows[e.Row]["PayNum"].ToString()));
				/*
				if(PaymentCur.PayType==0){//provider income transfer
					FormProviderIncTrans FormPIT=new FormProviderIncTrans();
					FormPIT.PatNum=PatCur.PatNum;
					FormPIT.PaymentCur=PaymentCur;
					FormPIT.IsNew=false;
					FormPIT.ShowDialog();
				}
				else{*/
				FormPayment FormPayment2=new FormPayment(PatCur,FamCur,PaymentCur);
				FormPayment2.IsNew=false;
				FormPayment2.ShowDialog();
				//}
			}
			else if(table.Rows[e.Row]["ClaimNum"].ToString()!="0"){//claims and claimpayments
				Claim claim=Claims.GetClaim(PIn.Long(table.Rows[e.Row]["ClaimNum"].ToString()));
				Patient pat=FamCur.GetPatient(claim.PatNum);
				FormClaimEdit FormClaimEdit2=new FormClaimEdit(claim,pat,FamCur);
				FormClaimEdit2.IsNew=false;
				FormClaimEdit2.ShowDialog();
			}
			else if(table.Rows[e.Row]["StatementNum"].ToString()!="0"){
				Statement statement=Statements.CreateObject(PIn.Long(table.Rows[e.Row]["StatementNum"].ToString()));
				FormStatementOptions FormS=new FormStatementOptions();
				FormS.StmtCur=statement;
				FormS.ShowDialog();
			}
			else if(table.Rows[e.Row]["PayPlanNum"].ToString()!="0"){
				PayPlan payplan=PayPlans.GetOne(PIn.Long(table.Rows[e.Row]["PayPlanNum"].ToString()));
				FormPayPlan2=new FormPayPlan(PatCur,payplan);
				FormPayPlan2.ShowDialog();
				if(FormPayPlan2.GotoPatNum!=0){
					ModuleSelected(FormPayPlan2.GotoPatNum,false);
					return;
				}
			}
			bool isSelectingFamily=gridAcctPat.GetSelectedIndex()==this.DataSetMain.Tables["patient"].Rows.Count-1;
			ModuleSelected(PatCur.PatNum,isSelectingFamily);
		}

		private void gridPayPlan_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			DataTable table=DataSetMain.Tables["payplan"];
			PayPlan payplan=PayPlans.GetOne(PIn.Long(table.Rows[e.Row]["PayPlanNum"].ToString()));
			FormPayPlan2=new FormPayPlan(PatCur,payplan);
			FormPayPlan2.ShowDialog();
			if(FormPayPlan2.GotoPatNum!=0){
				ModuleSelected(FormPayPlan2.GotoPatNum,false);
				return;
			}
			bool isSelectingFamily=gridAcctPat.GetSelectedIndex()==this.DataSetMain.Tables["patient"].Rows.Count-1;
			ModuleSelected(PatCur.PatNum,isSelectingFamily);
		}

		private void gridAcctPat_CellClick(object sender,ODGridClickEventArgs e) {
			if(ViewingInRecall){
				return;
			}
			if(e.Row==FamCur.ListPats.Length){//last row
				OnPatientSelected(FamCur.ListPats[0].PatNum,FamCur.ListPats[0].GetNameLF(),FamCur.ListPats[0].Email!="",
					FamCur.ListPats[0].ChartNumber);
				ModuleSelected(FamCur.ListPats[0].PatNum,true);
			}
			else{
				OnPatientSelected(FamCur.ListPats[e.Row].PatNum,FamCur.ListPats[e.Row].GetNameLF(),FamCur.ListPats[e.Row].Email!="",
					FamCur.ListPats[e.Row].ChartNumber);
				ModuleSelected(FamCur.ListPats[e.Row].PatNum);
			}
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					//case "Patient":
					//	OnPat_Click();
					//	break;
					case "Payment":
						OnPay_Click();
						break;
					case "Adjustment":
						OnAdj_Click();
						break;
					case "Insurance":
						OnIns_Click();
						break;
					case "PayPlan":
						OnPayPlan_Click();
						break;
					case "RepeatCharge":
						OnRepeatCharge_Click();
						break;
					case "Statement":
						OnStatement_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(long)){
				ProgramL.Execute((long)e.Button.Tag,PatCur);
			}
		}

		///<summary></summary>
		private void OnPatientSelected(long patNum,string patName,bool hasEmail,string chartNumber) {
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum,patName,hasEmail,chartNumber);
			if(PatientSelected!=null){
				PatientSelected(this,eArgs);
			}
		}

		private void OnPay_Click() {
			Payment PaymentCur=new Payment();
			PaymentCur.PayDate=DateTime.Today;
			PaymentCur.PatNum=PatCur.PatNum;
			PaymentCur.ClinicNum=PatCur.ClinicNum;
			if(DefC.Short[(int)DefCat.PaymentTypes].Length>0){
				PaymentCur.PayType=DefC.Short[(int)DefCat.PaymentTypes][0].DefNum;
			}
			Payments.Insert(PaymentCur);
			FormPayment FormPayment2=new FormPayment(PatCur,FamCur,PaymentCur);
			FormPayment2.IsNew=true;
			FormPayment2.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void OnAdj_Click() {
			Adjustment AdjustmentCur=new Adjustment();
			AdjustmentCur.DateEntry=DateTime.Today;//cannot be changed. Handled automatically
			AdjustmentCur.AdjDate=DateTime.Today;
			AdjustmentCur.ProcDate=DateTime.Today;
			AdjustmentCur.ProvNum=PatCur.PriProv;
			AdjustmentCur.PatNum=PatCur.PatNum;
			AdjustmentCur.ClinicNum=PatCur.ClinicNum;
			FormAdjust FormAdjust2=new FormAdjust(PatCur,AdjustmentCur);
			FormAdjust2.IsNew=true;
			FormAdjust2.ShowDialog();
			//Shared.ComputeBalances();
			ModuleSelected(PatCur.PatNum);
		}

		private void OnIns_Click() {
			List <PatPlan> PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			List<InsSub> SubList=InsSubs.RefreshForFam(FamCur);
			List<InsPlan> InsPlanList=InsPlans.RefreshForSubList(SubList);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			List <Benefit> BenefitList=Benefits.Refresh(PatPlanList);
			List<Procedure> procsForPat=Procedures.Refresh(PatCur.PatNum);
			if(PatPlanList.Count==0){
				MsgBox.Show(this,"Patient does not have insurance.");
				return;
			}
			int countSelected=0;
			bool countIsOverMaxCanadian=false;
			DataTable table=DataSetMain.Tables["account"];
			if(gridAccount.SelectedIndices.Length==0){
				//autoselect procedures
				for(int i=0;i<table.Rows.Count;i++){//loop through every line showing on screen
					if(table.Rows[i]["ProcNum"].ToString()=="0"){
						continue;//ignore non-procedures
					}
					if((double)table.Rows[i]["chargesDouble"]==0){
						continue;//ignore zero fee procedures, but user can explicitly select them
					}
					if(Procedures.NeedsSent(PIn.Long(table.Rows[i]["ProcNum"].ToString()),ClaimProcList,PatPlans.GetPlanNum(PatPlanList,1))){
						if(CultureInfo.CurrentCulture.Name.EndsWith("CA") && countSelected==7) {//Canadian. en-CA or fr-CA
							countIsOverMaxCanadian=true;
							continue;//only send 7.  
						}
						countSelected++;
						gridAccount.SetSelected(i,true);
					}
				}
				if(gridAccount.SelectedIndices.Length==0){//if still none selected
					MessageBox.Show(Lan.g(this,"Please select procedures first."));
					return;
				}
			}
			bool allAreProcedures=true;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()=="0"){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MsgBox.Show(this,"You can only select procedures.");
				return;
			}
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA") && gridAccount.SelectedIndices.Length>7) {//Canadian. en-CA or fr-CA
				countIsOverMaxCanadian=true;
				List<int> selectedIndicies=new List<int>(gridAccount.SelectedIndices);
				selectedIndicies.Sort();
				for(int i=0;i<selectedIndicies.Count;i++) { //Unselect all but the first 7 procedures with the smallest index numbers.
					gridAccount.SetSelected(selectedIndicies[i],(i<7));
				}
			}
			if(countIsOverMaxCanadian) {
				MsgBox.Show(this,"Only the first 7 procedures will be selected.  You will need to create a second claim for the remaining procedures.");
			}
			Claim ClaimCur=CreateClaim("P",PatPlanList,InsPlanList,ClaimProcList,procsForPat,SubList);
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTime.Today;
			ClaimL.CalculateAndUpdate(procsForPat,InsPlanList,ClaimCur,PatPlanList,BenefitList,PatCur.Age,SubList);
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK){
				ModuleSelected(PatCur.PatNum);
				return;//will have already been deleted
			}
			if(PatPlans.GetPlanNum(PatPlanList,2)>0 && !CultureInfo.CurrentCulture.Name.EndsWith("CA")){//don't create secondary claim for Canada
				InsPlan plan=InsPlans.GetPlan(PatPlans.GetPlanNum(PatPlanList,2),InsPlanList);
				if(!plan.IsMedical){
					ClaimCur=CreateClaim("S",PatPlanList,InsPlanList,ClaimProcList,procsForPat,SubList);
					if(ClaimCur.ClaimNum==0){
						ModuleSelected(PatCur.PatNum);
						return;
					}
					ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
					ClaimCur.ClaimStatus="H";
					ClaimL.CalculateAndUpdate(procsForPat,InsPlanList,ClaimCur,PatPlanList,BenefitList,PatCur.Age,SubList);
				}
			}
			ModuleSelected(PatCur.PatNum);
		}

		///<summary>The only validation that's been done is just to make sure that only procedures are selected.  All validation on the procedures selected is done here.  Creates and saves claim initially, attaching all selected procedures.  But it does not refresh any data. Does not do a final update of the new claim.  Does not enter fee amounts.  claimType=P,S,Med,or Other</summary>
		private Claim CreateClaim(string claimType,List <PatPlan> PatPlanList,List <InsPlan> planList,List<ClaimProc> ClaimProcList,List<Procedure> procsForPat,List<InsSub> subList){
			long claimFormNum = 0;
			EtransType eFormat = 0;
			InsPlan PlanCur=new InsPlan();
			InsSub SubCur=new InsSub();
			Relat relatOther=Relat.Self;
			switch(claimType){
				case "P":
					PlanCur=InsPlans.GetPlan(PatPlans.GetPlanNum(PatPlanList,1),planList);
					SubCur=InsSubs.GetSub(PatPlans.GetInsSubNum(PatPlanList,1),subList);
					break;
				case "S":
					PlanCur=InsPlans.GetPlan(PatPlans.GetPlanNum(PatPlanList,2),planList);
					SubCur=InsSubs.GetSub(PatPlans.GetInsSubNum(PatPlanList,2),subList);
					break;
				case "Med":
					//It's already been verified that a med plan exists
					for(int i=0;i<PatPlanList.Count;i++){
						if(InsPlans.GetPlan(PatPlanList[i].PlanNum,planList).IsMedical) {
							PlanCur=InsPlans.GetPlan(PatPlanList[i].PlanNum,planList);
							SubCur=InsSubs.GetSub(PatPlanList[i].InsSubNum,subList);
							break;
						}
					}
					break;
				case "Other":
					FormClaimCreate FormCC=new FormClaimCreate(PatCur.PatNum);
					//FormCC.ViewRelat=true;
					FormCC.ShowDialog();
					if(FormCC.DialogResult!=DialogResult.OK){
						return new Claim();
					}
					PlanCur=FormCC.SelectedPlan;
					SubCur=FormCC.SelectedSub;
					relatOther=FormCC.PatRelat;
					claimFormNum=FormCC.ClaimFormNum;
					eFormat=FormCC.EFormat;
					break;
			}
			DataTable table=DataSetMain.Tables["account"];
			//List<int> procNums=new List<int>();
			//for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
			//	procNums.Add(PIn.PInt(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()));
			//}
			//List<Procedure> procList=Procedures.GetProcFromList(procsForPat,  //.GetManyProcs(procNums);
			Procedure proc;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				proc=Procedures.GetProcFromList(procsForPat,PIn.Long(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()));
				if(Procedures.NoBillIns(proc,ClaimProcList,PlanCur.PlanNum)){
					MsgBox.Show(this,"Not allowed to send procedures to insurance that are marked 'Do not bill to ins'.");
					return new Claim();
				}
			}
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				proc=Procedures.GetProcFromList(procsForPat,PIn.Long(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()));
				if(Procedures.IsAlreadyAttachedToClaim(proc,ClaimProcList,SubCur.InsSubNum)){
					MsgBox.Show(this,"Not allowed to send a procedure to the same insurance company twice.");
					return new Claim();
				}
			}
			proc=Procedures.GetProcFromList(procsForPat,PIn.Long(table.Rows[gridAccount.SelectedIndices[0]]["ProcNum"].ToString()));
			long clinicNum=proc.ClinicNum;
			for(int i=1;i<gridAccount.SelectedIndices.Length;i++){//skips 0
				proc=Procedures.GetProcFromList(procsForPat,PIn.Long(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()));
				if(clinicNum!=proc.ClinicNum){
					MsgBox.Show(this,"All procedures do not have the same clinic.");
					return new Claim();
				}
			}
			ClaimProc[] claimProcs=new ClaimProc[gridAccount.SelectedIndices.Length];//1:1 with selectedIndices
			long procNum;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){//loop through selected procs
				//and try to find an estimate that can be used
				procNum=PIn.Long(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString());
				claimProcs[i]=Procedures.GetClaimProcEstimate(procNum,ClaimProcList,PlanCur,SubCur.InsSubNum);
			}
			for(int i=0;i<claimProcs.Length;i++){//loop through each claimProc
				//and create any missing estimates. This handles claims to 3rd and 4th ins co's.
				if(claimProcs[i]==null){
					claimProcs[i]=new ClaimProc();
					proc=Procedures.GetProcFromList(procsForPat,PIn.Long(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()));//1:1
					ClaimProcs.CreateEst(claimProcs[i],proc,PlanCur,SubCur);
				}
			}
			Claim ClaimCur=new Claim();
			Claims.Insert(ClaimCur);//to retreive a key for new Claim.ClaimNum
			//now, all claimProcs have a valid value
			//for any CapComplete, need to make a copy so that original doesn't get attached.
			for(int i=0;i<claimProcs.Length;i++){
				if(claimProcs[i].Status==ClaimProcStatus.CapComplete){
					claimProcs[i].ClaimNum=ClaimCur.ClaimNum;
					claimProcs[i]=claimProcs[i].Copy();
					claimProcs[i].WriteOff=0;
					claimProcs[i].CopayAmt=-1;
					claimProcs[i].CopayOverride=-1;
					//status will get changed down below
					ClaimProcs.Insert(claimProcs[i]);//this makes a duplicate in db with different claimProcNum
				}
			}
			//Claim ClaimCur=Claims.Cur;
			ClaimCur.PatNum=PatCur.PatNum;
			ClaimCur.DateService=claimProcs[claimProcs.Length-1].ProcDate;
			ClaimCur.ClinicNum=clinicNum;
			//datesent
			ClaimCur.ClaimStatus="U";
			//datereceived
			switch(claimType){
				case "P":
					ClaimCur.PlanNum=PatPlans.GetPlanNum(PatPlanList,1);
					ClaimCur.InsSubNum=PatPlans.GetInsSubNum(PatPlanList,1);
					ClaimCur.PatRelat=PatPlans.GetRelat(PatPlanList,1);
					ClaimCur.ClaimType="P";
					ClaimCur.PlanNum2=PatPlans.GetPlanNum(PatPlanList,2);//might be 0 if no sec ins
					ClaimCur.InsSubNum2=PatPlans.GetInsSubNum(PatPlanList,2);
					ClaimCur.PatRelat2=PatPlans.GetRelat(PatPlanList,2);
					break;
				case "S":
					ClaimCur.PlanNum=PatPlans.GetPlanNum(PatPlanList,2);
					ClaimCur.InsSubNum=PatPlans.GetInsSubNum(PatPlanList,2);
					ClaimCur.PatRelat=PatPlans.GetRelat(PatPlanList,2);
					ClaimCur.ClaimType="S";
					ClaimCur.PlanNum2=PatPlans.GetPlanNum(PatPlanList,1);
					ClaimCur.InsSubNum2=PatPlans.GetInsSubNum(PatPlanList,1);
					ClaimCur.PatRelat2=PatPlans.GetRelat(PatPlanList,1);
					break;
				case "Med":
					ClaimCur.PlanNum=PlanCur.PlanNum;
					ClaimCur.InsSubNum=SubCur.InsSubNum;
					ClaimCur.PatRelat=PatPlans.GetFromList(PatPlanList,PlanCur.PlanNum).Relationship;
					ClaimCur.ClaimType="Other";
					break;
				case "Other":
					ClaimCur.PlanNum=PlanCur.PlanNum;
					ClaimCur.InsSubNum=SubCur.InsSubNum;
					ClaimCur.PatRelat=relatOther;
					ClaimCur.ClaimType="Other";
					//plannum2 is not automatically filled in.
					ClaimCur.ClaimForm=claimFormNum;
					ClaimCur.EFormat=eFormat;
					break;
			}
			//InsPlans.GetCur(ClaimCur.PlanNum);
			if(PlanCur.PlanType=="c"){//if capitation
				ClaimCur.ClaimType="Cap";
			}
			ClaimCur.ProvTreat=Procedures.GetProcFromList(procsForPat,PIn.Long(table.Rows[gridAccount.SelectedIndices[0]]["ProcNum"].ToString())).ProvNum;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				proc=Procedures.GetProcFromList(procsForPat,PIn.Long(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()));
				if(!Providers.GetIsSec(proc.ProvNum)){//if not a hygienist
					ClaimCur.ProvTreat=proc.ProvNum;
				}
			}
			if(Providers.GetIsSec(ClaimCur.ProvTreat)){
				ClaimCur.ProvTreat=PatCur.PriProv;
				//OK if 0, because auto select first in list when open claim
			}
			//claimfee calcs in ClaimEdit
			//inspayest ''
			//inspayamt
			//ClaimCur.DedApplied=0;//calcs in ClaimEdit.
			//preauthstring, etc, etc
			ClaimCur.IsProsthesis="N";
			//int clinicInsBillingProv=0;
			//bool useClinic=false;
			//if(ClaimCur.ClinicNum>0){
			//	useClinic=true;
			//	clinicInsBillingProv=Clinics.GetClinic(ClaimCur.ClinicNum).InsBillingProv;
			//}
			ClaimCur.ProvBill=Providers.GetBillingProvNum(ClaimCur.ProvTreat,ClaimCur.ClinicNum);//,useClinic,clinicInsBillingProv);//OK if zero, because it will get fixed in claim
			ClaimCur.EmployRelated=YN.No;
			//attach procedures
			Procedure ProcCur;
			//for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
			for(int i=0;i<claimProcs.Length;i++){
				ProcCur=Procedures.GetProcFromList(procsForPat,PIn.Long(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()));//1:1
				//ClaimProc ClaimProcCur=new ClaimProc();
				//ClaimProcCur.ProcNum=ProcCur.ProcNum;
				claimProcs[i].ClaimNum=ClaimCur.ClaimNum;
				//ClaimProcCur.PatNum=Patients.Cur.PatNum;
				//ClaimProcCur.ProvNum=ProcCur.ProvNum;
				//ClaimProcs.Cur.FeeBilled=;//handle in call to ClaimL.CalculateAndUpdate()
				//inspayest ''
				//dedapplied ''
				if(PlanCur.PlanType=="c")//if capitation
					claimProcs[i].Status=ClaimProcStatus.CapClaim;
				else
					claimProcs[i].Status=ClaimProcStatus.NotReceived;
				//inspayamt=0
				//remarks
				//claimpaymentnum=0
				//ClaimProcCur.PlanNum=Claims.Cur.PlanNum;
				//ClaimProcCur.DateCP=ProcCur.ProcDate;
				//writeoff handled in ClaimL.CalculateAndUpdate()
				if(PlanCur.UseAltCode && (ProcedureCodes.GetProcCode(ProcCur.CodeNum).AlternateCode1!="")){
					claimProcs[i].CodeSent=ProcedureCodes.GetProcCode(ProcCur.CodeNum).AlternateCode1;
				}
				else if(PlanCur.IsMedical && ProcCur.MedicalCode!=""){
					claimProcs[i].CodeSent=ProcCur.MedicalCode;
				}
				else{
					claimProcs[i].CodeSent=ProcedureCodes.GetProcCode(ProcCur.CodeNum).ProcCode;
					if(claimProcs[i].CodeSent.Length>5 && claimProcs[i].CodeSent.Substring(0,1)=="D"){
						claimProcs[i].CodeSent=claimProcs[i].CodeSent.Substring(0,5);
					}
				}
				claimProcs[i].LineNumber=(byte)(i+1);
				ClaimProcs.Update(claimProcs[i]);
			}//for claimProc
			return ClaimCur;
			//return null;
		}

		private void menuInsPri_Click(object sender, System.EventArgs e) {
			List <PatPlan> PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			List<InsSub> SubList=InsSubs.RefreshForFam(FamCur);
			List<InsPlan> InsPlanList=InsPlans.RefreshForSubList(SubList);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			List <Benefit> BenefitList=Benefits.Refresh(PatPlanList);
			List<Procedure> procsForPat=Procedures.Refresh(PatCur.PatNum);
			if(PatPlanList.Count==0){
				MessageBox.Show(Lan.g(this,"Patient does not have insurance."));
				return;
			}
			if(gridAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			DataTable table=DataSetMain.Tables["account"];
			bool allAreProcedures=true;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()=="0"){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			Claim ClaimCur=CreateClaim("P",PatPlanList,InsPlanList,ClaimProcList,procsForPat,SubList);
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTime.Today;
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			ClaimL.CalculateAndUpdate(procsForPat,InsPlanList,ClaimCur,PatPlanList,BenefitList,PatCur.Age,SubList);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void menuInsSec_Click(object sender, System.EventArgs e) {
			List <PatPlan> PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			List<InsSub> SubList=InsSubs.RefreshForFam(FamCur);
			List<InsPlan> InsPlanList=InsPlans.RefreshForSubList(SubList);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			List <Benefit> BenefitList=Benefits.Refresh(PatPlanList);
			List<Procedure> procsForPat=Procedures.Refresh(PatCur.PatNum);
			if(PatPlanList.Count<2){
				MessageBox.Show(Lan.g(this,"Patient does not have secondary insurance."));
				return;
			}
			if(gridAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			DataTable table=DataSetMain.Tables["account"];
			bool allAreProcedures=true;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()=="0"){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			Claim ClaimCur=CreateClaim("S",PatPlanList,InsPlanList,ClaimProcList,procsForPat,SubList);
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTime.Today;
			ClaimL.CalculateAndUpdate(procsForPat,InsPlanList,ClaimCur,PatPlanList,BenefitList,PatCur.Age,SubList);
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void menuInsMedical_Click(object sender, System.EventArgs e) {
			List <PatPlan> PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			List<InsSub> SubList=InsSubs.RefreshForFam(FamCur);
			List<InsPlan> InsPlanList=InsPlans.RefreshForSubList(SubList);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			List <Benefit> BenefitList=Benefits.Refresh(PatPlanList);
			List<Procedure> procsForPat=Procedures.Refresh(PatCur.PatNum);
			long medPlanNum=0;
			for(int i=0;i<PatPlanList.Count;i++){
				if(InsPlans.GetPlan(PatPlanList[i].PlanNum,InsPlanList).IsMedical){
					medPlanNum=PatPlanList[i].PlanNum;
					break;
				}
			}
			if(medPlanNum==0){
				MsgBox.Show(this,"Patient does not have medical insurance.");
				return;
			}
			DataTable table=DataSetMain.Tables["account"];
			Procedure proc;
			if(gridAccount.SelectedIndices.Length==0){
				//autoselect procedures
				for(int i=0;i<table.Rows.Count;i++){//loop through every line showing on screen
					if(table.Rows[i]["ProcNum"].ToString()=="0"){
						continue;//ignore non-procedures
					}
					proc=Procedures.GetProcFromList(procsForPat,PIn.Long(table.Rows[i]["ProcNum"].ToString()));
					if(proc.ProcFee==0){
						continue;//ignore zero fee procedures, but user can explicitly select them
					}
					if(proc.MedicalCode==""){
						continue;//ignore non-medical procedures
					}
					if(Procedures.NeedsSent(proc.ProcNum,ClaimProcList,medPlanNum)){
						gridAccount.SetSelected(i,true);
					}
				}
				if(gridAccount.SelectedIndices.Length==0){//if still none selected
					MsgBox.Show(this,"Please select procedures first.");
					return;
				}
			}
			bool allAreProcedures=true;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()=="0"){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MsgBox.Show(this,"You can only select procedures.");
				return;
			}
			Claim ClaimCur=CreateClaim("Med",PatPlanList,InsPlanList,ClaimProcList,procsForPat,SubList);
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTime.Today;
			ClaimL.CalculateAndUpdate(procsForPat,InsPlanList,ClaimCur,PatPlanList,BenefitList,PatCur.Age,SubList);
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void menuInsOther_Click(object sender, System.EventArgs e) {
			List <PatPlan> PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			List<InsSub> SubList=InsSubs.RefreshForFam(FamCur);
			List<InsPlan> InsPlanList=InsPlans.RefreshForSubList(SubList);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			List <Benefit> BenefitList=Benefits.Refresh(PatPlanList);
			List<Procedure> procsForPat=Procedures.Refresh(PatCur.PatNum);
			if(gridAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			DataTable table=DataSetMain.Tables["account"];
			bool allAreProcedures=true;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()=="0"){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			Claim ClaimCur=CreateClaim("Other",PatPlanList,InsPlanList,ClaimProcList,procsForPat,SubList);
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimL.CalculateAndUpdate(procsForPat,InsPlanList,ClaimCur,PatPlanList,BenefitList,PatCur.Age,SubList);
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void OnPayPlan_Click() {
			PayPlan payPlan=new PayPlan();
			payPlan.PatNum=PatCur.PatNum;
			payPlan.Guarantor=PatCur.Guarantor;
			payPlan.PayPlanDate=DateTime.Today;
			payPlan.CompletedAmt=PatCur.EstBalance;
			PayPlans.Insert(payPlan);
			FormPayPlan FormPP=new FormPayPlan(PatCur,payPlan);
			FormPP.TotalAmt=PatCur.EstBalance;
			FormPP.IsNew=true;
			FormPP.ShowDialog();
			if(FormPP.GotoPatNum!=0){
				ModuleSelected(FormPP.GotoPatNum);//switches to other patient.
			}
			else{
				ModuleSelected(PatCur.PatNum);
			}
		}

		private void OnRepeatCharge_Click(){
			RepeatCharge repeat=new RepeatCharge();
			repeat.PatNum=PatCur.PatNum;
			repeat.DateStart=DateTime.Today;
			FormRepeatChargeEdit FormR=new FormRepeatChargeEdit(repeat);
			FormR.IsNew=true;
			FormR.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void MenuItemRepeatStand_Click(object sender,System.EventArgs e) {
			if(!ProcedureCodeC.HList.ContainsKey("001")) {
				return;
			}
			RepeatCharge repeat=new RepeatCharge();
			repeat.PatNum=PatCur.PatNum;
			repeat.ProcCode="001";
			repeat.ChargeAmt=149;
			repeat.DateStart=DateTime.Today;
			repeat.DateStop=DateTime.Today.AddMonths(11);
			RepeatCharges.Insert(repeat);
			repeat=new RepeatCharge();
			repeat.PatNum=PatCur.PatNum;
			repeat.ProcCode="001";
			repeat.ChargeAmt=99;
			repeat.DateStart=DateTime.Today.AddYears(1);
			RepeatCharges.Insert(repeat);
			ModuleSelected(PatCur.PatNum);
		}

		private void MenuItemRepeatEmail_Click(object sender,System.EventArgs e) {
			if(!ProcedureCodeC.HList.ContainsKey("008")) {
				return;
			}
			RepeatCharge repeat=new RepeatCharge();
			repeat.PatNum=PatCur.PatNum;
			repeat.ProcCode="008";
			repeat.ChargeAmt=89;
			repeat.DateStart=DateTime.Today;
			RepeatCharges.Insert(repeat);
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemRepeatMobile_Click(object sender,EventArgs e) {
			if(!ProcedureCodeC.HList.ContainsKey("027")) {
				return;
			}
			RepeatCharge repeat=new RepeatCharge();
			repeat.PatNum=PatCur.PatNum;
			repeat.ProcCode="027";
			repeat.ChargeAmt=10;
			repeat.DateStart=DateTime.Today;
			RepeatCharges.Insert(repeat);
			ModuleSelected(PatCur.PatNum);
		}

		private void OnStatement_Click() {
			Statement stmt=new Statement();
			stmt.PatNum=PatCur.Guarantor;
			stmt.DateSent=DateTime.Today;
			stmt.IsSent=true;
			stmt.Mode_=StatementMode.InPerson;
			stmt.HidePayment=false;
			stmt.SinglePatient=false;
			stmt.Intermingled=false;
			stmt.DateRangeFrom=DateTime.MinValue;
			if (PrefC.GetBool(PrefName.IntermingleFamilyDefault)){
				stmt.Intermingled = true;
			}
			if (PrefC.GetBool(PrefName.FuchsOptionsOn)){
				stmt.DateRangeFrom = PIn.Date(DateTime.Today.AddDays(-45).ToShortDateString());
				stmt.DateRangeTo = PIn.Date(DateTime.Today.ToShortDateString());
			} 
			else{
				if (textDateStart.errorProvider1.GetError(textDateStart) == "") {
					if (textDateStart.Text != "") {
						stmt.DateRangeFrom = PIn.Date(textDateStart.Text);
					}
				}
			}
			stmt.DateRangeTo = DateTime.Today;//This is needed for payment plan accuracy.//new DateTime(2200,1,1);
			if (textDateEnd.errorProvider1.GetError(textDateEnd) == "") {
				if (textDateEnd.Text != "") {
					stmt.DateRangeTo = PIn.Date(textDateEnd.Text);
				}
			}
			stmt.Note = "";
			stmt.NoteBold = "";
			PrintStatement(stmt);
			ModuleSelected(PatCur.PatNum);
		}
		
		private void menuItemStatementWalkout_Click(object sender, System.EventArgs e) {
			Statement stmt=new Statement();
			stmt.PatNum=PatCur.PatNum;
			stmt.DateSent=DateTime.Today;
			stmt.IsSent=true;
			stmt.Mode_=StatementMode.InPerson;
			stmt.HidePayment=true;
			stmt.SinglePatient=true;
			stmt.Intermingled=false;
			if(PrefC.GetBool(PrefName.IntermingleFamilyDefault)) {
				stmt.Intermingled = true;
				stmt.SinglePatient=false;
			}
			stmt.DateRangeFrom=DateTime.Today;
			stmt.DateRangeTo=DateTime.Today;
			stmt.Note="";
			stmt.NoteBold="";
			PrintStatement(stmt);
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemStatementEmail_Click(object sender,EventArgs e) {
			Statement stmt=new Statement();
			stmt.PatNum=PatCur.Guarantor;
			stmt.DateSent=DateTime.Today;
			stmt.IsSent=true;
			stmt.Mode_=StatementMode.Email;
			stmt.HidePayment=false;
			stmt.SinglePatient=false;
			stmt.Intermingled = false;
			if (PrefC.GetBool(PrefName.IntermingleFamilyDefault))
			{
				stmt.Intermingled=true;
			}
			stmt.DateRangeFrom=DateTime.MinValue;
			if(textDateStart.errorProvider1.GetError(textDateStart)==""){
				if(textDateStart.Text!=""){
					stmt.DateRangeFrom=PIn.Date(textDateStart.Text);
				}
			}
			stmt.DateRangeTo=new DateTime(2200,1,1);
			if(textDateEnd.errorProvider1.GetError(textDateEnd)==""){
				if(textDateEnd.Text!=""){
					stmt.DateRangeTo=PIn.Date(textDateEnd.Text);
				}
			}
			stmt.Note="";
			stmt.NoteBold="";
			PrintStatement(stmt);
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemStatementMore_Click(object sender, System.EventArgs e) {
			Statement stmt=new Statement();
			stmt.PatNum=PatCur.PatNum;
			stmt.DateSent=DateTime.Today;
			stmt.IsSent=false;
			stmt.Mode_=StatementMode.InPerson;
			stmt.HidePayment=false;
			stmt.SinglePatient=false;
			if(PrefC.GetBool(PrefName.IntermingleFamilyDefault)) {
				stmt.Intermingled=true;
			}
			else {
				stmt.Intermingled=false;
			} 
			stmt.DateRangeFrom=DateTime.MinValue;
			stmt.DateRangeFrom=DateTime.MinValue;
			if(textDateStart.errorProvider1.GetError(textDateStart)==""){
				if(textDateStart.Text!=""){
					stmt.DateRangeFrom=PIn.Date(textDateStart.Text);
				}
			}
			if(PrefC.GetBool(PrefName.FuchsOptionsOn)) {
				stmt.DateRangeFrom=DateTime.Today.AddDays(-90);
			}
			stmt.DateRangeTo=DateTime.Today;//Needed for payplan accuracy.//new DateTime(2200,1,1);
			if(textDateEnd.errorProvider1.GetError(textDateEnd)==""){
				if(textDateEnd.Text!=""){
					stmt.DateRangeTo=PIn.Date(textDateEnd.Text);
				}
			}
			stmt.Note="";
			stmt.NoteBold="";
			//All printing and emailing will be done from within the form:
			FormStatementOptions FormSO=new FormStatementOptions();
			stmt.IsNew=true;
			FormSO.StmtCur=stmt;
			FormSO.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		/// <summary>Saves the statement.  Attaches a pdf to it by creating a doc object.  Prints it or emails it.  </summary>
		private void PrintStatement(Statement stmt){
			Cursor=Cursors.WaitCursor;
			Statements.Insert(stmt);
			FormRpStatement FormST=new FormRpStatement();
			DataSet dataSet=AccountModules.GetStatementDataSet(stmt);
			FormST.CreateStatementPdf(stmt,PatCur,FamCur,dataSet);
			//if(ImageStore.UpdatePatient == null){
			//	ImageStore.UpdatePatient = new FileStore.UpdatePatientDelegate(Patients.Update);
			//}
			Patient guar=Patients.GetPat(stmt.PatNum);
			string guarFolder=ImageStore.GetPatientFolder(guar);
			//OpenDental.Imaging.ImageStoreBase imageStore = OpenDental.Imaging.ImageStore.GetImageStore(guar);
			if(stmt.Mode_==StatementMode.Email){
				string attachPath=FormEmailMessageEdit.GetAttachPath();
				Random rnd=new Random();
				string fileName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()+".pdf";
				string filePathAndName=ODFileUtils.CombinePaths(attachPath,fileName);
				File.Copy(ImageStore.GetFilePath(Documents.GetByNum(stmt.DocNum),guarFolder),filePathAndName);
				//Process.Start(filePathAndName);
				EmailMessage message=new EmailMessage();
				message.PatNum=guar.PatNum;
				message.ToAddress=guar.Email;
				message.FromAddress=PrefC.GetString(PrefName.EmailSenderAddress);
				message.Subject=Lan.g(this,"Statement");
				//message.BodyText=Lan.g(this,"");
				EmailAttach attach=new EmailAttach();
				attach.DisplayedFileName="Statement.pdf";
				attach.ActualFileName=fileName;
				message.Attachments.Add(attach);
				FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
				FormE.IsNew=true;
				FormE.ShowDialog();
			}
			else{
				#if DEBUG
					//don't bother to check valid path because it's just debug.
					string imgPath=ImageStore.GetFilePath(Documents.GetByNum(stmt.DocNum),guarFolder);
					DateTime now=DateTime.Now;
					while(DateTime.Now<now.AddSeconds(5) && !File.Exists(imgPath)) {//wait up to 5 seconds.
						Application.DoEvents();
					}
					Process.Start(imgPath);
				#else
					FormST.PrintStatement(stmt,false,dataSet,FamCur,PatCur);
				#endif
			}
			Cursor=Cursors.Default;
		}

		private void textUrgFinNote_TextChanged(object sender, System.EventArgs e) {
			UrgFinNoteChanged=true;
		}

		private void textFinNotes_TextChanged(object sender, System.EventArgs e) {
			FinNoteChanged=true;
		}

		//private void textCC_TextChanged(object sender,EventArgs e) {
		//  CCChanged=true;
		//  if(Regex.IsMatch(textCC.Text,@"^\d{4}$")
		//    || Regex.IsMatch(textCC.Text,@"^\d{4}-\d{4}$")
		//    || Regex.IsMatch(textCC.Text,@"^\d{4}-\d{4}-\d{4}$")) 
		//  {
		//    textCC.Text=textCC.Text+"-";
		//    textCC.Select(textCC.Text.Length,0);
		//  }
		//}

		//private void textCCexp_TextChanged(object sender,EventArgs e) {
		//  CCChanged=true;
		//}

		private void textUrgFinNote_Leave(object sender, System.EventArgs e) {
			//need to skip this if selecting another module. Handled in ModuleUnselected due to click event
			if(FamCur==null)
				return;
			if(UrgFinNoteChanged){
				Patient PatOld=FamCur.ListPats[0].Copy();
				FamCur.ListPats[0].FamFinUrgNote=textUrgFinNote.Text;
				Patients.Update(FamCur.ListPats[0],PatOld);
				UrgFinNoteChanged=false;
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void textFinNotes_Leave(object sender, System.EventArgs e) {
			if(FamCur==null)
				return;
			if(FinNoteChanged){
				PatientNoteCur.FamFinancial=textFinNotes.Text;
				PatientNotes.Update(PatientNoteCur,PatCur.Guarantor);
				FinNoteChanged=false;
				ModuleSelected(PatCur.PatNum);
			}
		}

		//private void textCC_Leave(object sender,EventArgs e) {
		//  if(FamCur==null)
		//    return;
		//  if(CCChanged) {
		//    CCSave();
		//    CCChanged=false;
		//    ModuleSelected(PatCur.PatNum);
		//  }
		//}

		//private void textCCexp_Leave(object sender,EventArgs e) {
		//  if(FamCur==null)
		//    return;
		//  if(CCChanged){
		//    CCSave();
		//    CCChanged=false;
		//    ModuleSelected(PatCur.PatNum);
		//  }
		//}

		//private void CCSave(){
		//  string cc=textCC.Text;
		//  if(Regex.IsMatch(cc,@"^\d{4}-\d{4}-\d{4}-\d{4}$")){
		//    PatientNoteCur.CCNumber=cc.Substring(0,4)+cc.Substring(5,4)+cc.Substring(10,4)+cc.Substring(15,4);
		//  }
		//  else{
		//    PatientNoteCur.CCNumber=cc;
		//  }
		//  string exp=textCCexp.Text;
		//  if(Regex.IsMatch(exp,@"^\d\d[/\- ]\d\d$")){//08/07 or 08-07 or 08 07
		//    PatientNoteCur.CCExpiration=new DateTime(Convert.ToInt32("20"+exp.Substring(3,2)),Convert.ToInt32(exp.Substring(0,2)),1);
		//  }
		//  else if(Regex.IsMatch(exp,@"^\d{4}$")){//0807
		//    PatientNoteCur.CCExpiration=new DateTime(Convert.ToInt32("20"+exp.Substring(2,2)),Convert.ToInt32(exp.Substring(0,2)),1);
		//  } 
		//  else if(exp=="") {
		//    PatientNoteCur.CCExpiration=new DateTime();//Allow the experation date to be deleted.
		//  } 
		//  else {
		//    MsgBox.Show(this,"Expiration format invalid.");
		//  }
		//  PatientNotes.Update(PatientNoteCur,PatCur.Guarantor);
		//}

		private void butToday_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
			ModuleSelected(PatCur.PatNum);
		}

		private void but45days_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.AddDays(-45).ToShortDateString();
			textDateEnd.Text="";
			ModuleSelected(PatCur.PatNum);
		}

		private void but90days_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.AddDays(-90).ToShortDateString();
			textDateEnd.Text="";
			ModuleSelected(PatCur.PatNum);
		}

		private void butDatesAll_Click(object sender,EventArgs e) {
			textDateStart.Text="";
			textDateEnd.Text="";
			ModuleSelected(PatCur.PatNum);
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			if(PatCur==null){
				return;
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void checkShowDetail_Click(object sender,EventArgs e) {
			if(PatCur==null){
				return;
			}
			ModuleSelected(PatCur.PatNum);
		}

		//private void checkShowNotes_Click(object sender,EventArgs e) {
			//checkShowNotes.Tag="JustClicked";		
			//RefreshModuleScreen();
			//checkShowNotes.Tag = "";		
		//	ModuleSelected(PatCur.PatNum);
		//}

		private void panelSplitter_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			MouseIsDownOnSplitter=true;
			SplitterOriginalY=panelSplitter.Top;
			OriginalMousePos=panelSplitter.Top+e.Y;
		}

		private void panelSplitter_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!MouseIsDownOnSplitter)
				return;
			int splitterNewLoc=SplitterOriginalY+(panelSplitter.Top+e.Y)-OriginalMousePos;
			if(splitterNewLoc<gridAcctPat.Bottom)
				splitterNewLoc=gridAcctPat.Bottom;//keeps it from going too high
			if(splitterNewLoc>Height)
				splitterNewLoc=Height-panelSplitter.Height;//keeps it from going off the bottom edge
			panelSplitter.Top=splitterNewLoc;
			LayoutPanels();
		}

		private void panelSplitter_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			MouseIsDownOnSplitter=false;
			//tbAccount.LayoutTables();
		}

		private void butComm_Click(object sender, System.EventArgs e) {
			FormPat form=new FormPat();
			form.PatNum=PatCur.PatNum;
			form.FormDateTime=DateTime.Now;
			FormFormPatEdit FormP=new FormFormPatEdit();
			FormP.FormPatCur=form;
			FormP.IsNew=true;
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.OK) {
				ModuleSelected(PatCur.PatNum);
			}
		}

		/*private void butTask_Click(object sender, System.EventArgs e) {
			//FormTaskListSelect FormT=new FormTaskListSelect(TaskObjectType.Patient,PatCur.PatNum);
			//FormT.ShowDialog();
		}*/

		private void butCreditCard_Click(object sender,EventArgs e) {
			FormCreditCardManage FormCCM=new FormCreditCardManage(PatCur);
			FormCCM.ShowDialog();
		}

		private void butTrojan_Click(object sender,EventArgs e) {
			FormTrojanCollect FormT=new FormTrojanCollect();
			FormT.PatNum=PatCur.PatNum;
			FormT.ShowDialog();
		}

		private void gridComm_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			//string commlognum=DataSetMain.Tables["Commlog"].Rows[e.Row]["CommlogNum"].ToString();
			int row=e.Row;
			if(!this.checkShowFamilyComm.Checked){
				int i;
				for(row=0,i=0;row<DataSetMain.Tables["Commlog"].Rows.Count;row++){
					if(DataSetMain.Tables["Commlog"].Rows[row]["patName"].ToString()==""){
						if(i==e.Row){
							break;
						}
						i++;
					}
				}
			}
			if(DataSetMain.Tables["Commlog"].Rows[row]["CommlogNum"].ToString()!="0"){
				Commlog CommlogCur=
					Commlogs.GetOne(PIn.Long(DataSetMain.Tables["Commlog"].Rows[row]["CommlogNum"].ToString()));
				FormCommItem FormCI=new FormCommItem(CommlogCur);
				FormCI.ShowDialog();
				if(FormCI.DialogResult==DialogResult.OK) {
					ModuleSelected(PatCur.PatNum);
				}
			}
			else if(DataSetMain.Tables["Commlog"].Rows[row]["EmailMessageNum"].ToString()!="0") {
				EmailMessage email=
					EmailMessages.GetOne(PIn.Long(DataSetMain.Tables["Commlog"].Rows[row]["EmailMessageNum"].ToString()));
				FormEmailMessageEdit FormE=new FormEmailMessageEdit(email);
				FormE.ShowDialog();
				if(FormE.DialogResult==DialogResult.OK) {
					ModuleSelected(PatCur.PatNum);
				}
			}
			else if(DataSetMain.Tables["Commlog"].Rows[row]["FormPatNum"].ToString()!="0") {
				FormPat form=FormPats.GetOne(PIn.Long(DataSetMain.Tables["Commlog"].Rows[row]["FormPatNum"].ToString()));
				FormFormPatEdit FormP=new FormFormPatEdit();
				FormP.FormPatCur=form;
				FormP.ShowDialog();
				if(FormP.DialogResult==DialogResult.OK) {
					ModuleSelected(PatCur.PatNum);
				}
			}
			else if(DataSetMain.Tables["Commlog"].Rows[row]["SheetNum"].ToString()!="0") {
				Sheet sheet=Sheets.GetSheet(PIn.Long(DataSetMain.Tables["Commlog"].Rows[row]["SheetNum"].ToString()));
				FormSheetFillEdit FormSFE=new FormSheetFillEdit(sheet);
				FormSFE.ShowDialog();
				if(FormSFE.DialogResult==DialogResult.OK) {
					ModuleSelected(PatCur.PatNum);
				}
			}
		}

		private void Parent_MouseWheel(Object sender,MouseEventArgs e){
			if(Visible){
				this.OnMouseWheel(e);
			}
		}

		private void gridRepeat_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			FormRepeatChargeEdit FormR=new FormRepeatChargeEdit(RepeatChargeList[e.Row]);
			FormR.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		#region ProgressNotes
		///<summary>The supplied procedure row must include these columns: ProcDate,ProcStatus,ProcCode,Surf,ToothNum, and ToothRange, all in raw database format.</summary>
		private bool ShouldDisplayProc(DataRow row) {
			switch ((ProcStat)PIn.Long(row["ProcStatus"].ToString())) {
				case ProcStat.TP:
					if (checkShowTP.Checked) {
						return true;
					}
					break;
				case ProcStat.C:
					if (checkShowC.Checked) {
						return true;
					}
					break;
				case ProcStat.EC:
					if (checkShowE.Checked) {
						return true;
					}
					break;
				case ProcStat.EO:
					if (checkShowE.Checked) {
						return true;
					}
					break;
				case ProcStat.R:
					if (checkShowR.Checked) {
						return true;
					}
					break;
				case ProcStat.D:
					if (checkAudit.Checked) {
						return true;
					}
					break;
			}
			return false;
		}

		private void FillProgNotes() {
			ArrayList selectedTeeth = new ArrayList();//integers 1-32
			for(int i = 0;i < 32;i++) {
				selectedTeeth.Add(i);
			}
			gridProg.BeginUpdate();
			gridProg.Columns.Clear();
			ODGridColumn col = new ODGridColumn(Lan.g("TableProg", "Date"), 67);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "Th"), 27);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "Surf"), 40);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "Dx"), 28);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "Description"), 218);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "Stat"), 25);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "Prov"), 42);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "Amount"), 48, HorizontalAlignment.Right);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "ADA Code"), 62, HorizontalAlignment.Center);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "User"), 62, HorizontalAlignment.Center);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "Signed"), 55, HorizontalAlignment.Center);
			gridProg.Columns.Add(col);
			gridProg.NoteSpanStart = 2;
			gridProg.NoteSpanStop = 7;
			gridProg.Rows.Clear();
			ODGridRow row;
			//Type type;
			if (DataSetMain == null) {
				gridProg.EndUpdate();
				return;
			}
			DataTable table = DataSetMain.Tables["ProgNotes"];
			//ProcList = new List<DataRow>();
			for (int i = 0; i < table.Rows.Count; i++) {
				if (table.Rows[i]["ProcNum"].ToString() != "0") {//if this is a procedure
					if (ShouldDisplayProc(table.Rows[i])) {
						//ProcList.Add(table.Rows[i]);//show it in the graphical tooth chart
						//and add it to the grid below.
					}
					else {
						continue;
					}
				}
				else if (table.Rows[i]["CommlogNum"].ToString() != "0") {//if this is a commlog
					if (!checkComm.Checked) {
						continue;
					}
				}
				else if (table.Rows[i]["RxNum"].ToString() != "0") {//if this is an Rx
					if (!checkRx.Checked) {
						continue;
					}
				}
				else if (table.Rows[i]["LabCaseNum"].ToString() != "0") {//if this is a LabCase
					if (!checkLabCase.Checked) {
						continue;
					}
				}
				else if (table.Rows[i]["AptNum"].ToString() != "0") {//if this is an Appointment
					if (!checkAppt.Checked) {
						continue;
					}
				}
				row = new ODGridRow();
				row.ColorLborder = Color.Black;
				//remember that columns that start with lowercase are already altered for display rather than being raw data.
				row.Cells.Add(table.Rows[i]["procDate"].ToString());
				row.Cells.Add(table.Rows[i]["toothNum"].ToString());
				row.Cells.Add(table.Rows[i]["Surf"].ToString());
				row.Cells.Add(table.Rows[i]["dx"].ToString());
				row.Cells.Add(table.Rows[i]["description"].ToString());
				row.Cells.Add(table.Rows[i]["procStatus"].ToString());
				row.Cells.Add(table.Rows[i]["prov"].ToString());
				row.Cells.Add(table.Rows[i]["procFee"].ToString());
				row.Cells.Add(table.Rows[i]["ProcCode"].ToString());
				row.Cells.Add(table.Rows[i]["user"].ToString());
				row.Cells.Add(table.Rows[i]["signature"].ToString());
				if (checkNotes.Checked) {
					row.Note = table.Rows[i]["note"].ToString();
				}
				row.ColorText = Color.FromArgb(PIn.Int(table.Rows[i]["colorText"].ToString()));
				row.ColorBackG = Color.FromArgb(PIn.Int(table.Rows[i]["colorBackG"].ToString()));
				row.Tag = table.Rows[i];
				gridProg.Rows.Add(row);
			
			}
			gridProg.EndUpdate();
			gridProg.ScrollToEnd();
		}

		private void gridProg_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//Chartscrollval = gridProg.ScrollValue;
			DataRow row = (DataRow)gridProg.Rows[e.Row].Tag;
			if(row["ProcNum"].ToString() != "0") {
				if(checkAudit.Checked) {
					MsgBox.Show(this,"Not allowed to edit procedures when in audit mode.");
					return;
				}
				Procedure proc = Procedures.GetOneProc(PIn.Long(row["ProcNum"].ToString()),true);
				FormProcEdit FormP = new FormProcEdit(proc,PatCur,FamCur);
				FormP.ShowDialog();
				if(FormP.DialogResult != DialogResult.OK) {
					return;
				}
			}
			else if(row["CommlogNum"].ToString() != "0") {
				Commlog comm = Commlogs.GetOne(PIn.Long(row["CommlogNum"].ToString()));
				FormCommItem FormC = new FormCommItem(comm);
				FormC.ShowDialog();
				if(FormC.DialogResult != DialogResult.OK) {
					return;
				}
			}
			else if(row["RxNum"].ToString() != "0") {
				RxPat rx = RxPats.GetRx(PIn.Long(row["RxNum"].ToString()));
				FormRxEdit FormRxE = new FormRxEdit(PatCur,rx);
				FormRxE.ShowDialog();
				if(FormRxE.DialogResult != DialogResult.OK) {
					return;
				}
			}
			else if(row["LabCaseNum"].ToString() != "0") {
				LabCase lab = LabCases.GetOne(PIn.Long(row["LabCaseNum"].ToString()));
				FormLabCaseEdit FormL = new FormLabCaseEdit();
				FormL.CaseCur = lab;
				FormL.ShowDialog();
				if(FormL.DialogResult != DialogResult.OK) {
					return;
				}
			}
			else if(row["TaskNum"].ToString() != "0") {
				Task task = Tasks.GetOne(PIn.Long(row["TaskNum"].ToString()));
				FormTaskEdit FormT = new FormTaskEdit(task,task.Copy());
				FormT.Closing+=new CancelEventHandler(TaskGoToEvent);
				FormT.Show();//non-modal
			}
			else if(row["AptNum"].ToString() != "0") {
				//Appointment apt=Appointments.GetOneApt(
				FormApptEdit FormA = new FormApptEdit(PIn.Long(row["AptNum"].ToString()));
				//PinIsVisible=false
				FormA.ShowDialog();
				if(FormA.DialogResult != DialogResult.OK) {
					return;
				}
			}
			else if(row["EmailMessageNum"].ToString() != "0") {
				EmailMessage msg = EmailMessages.GetOne(PIn.Long(row["EmailMessageNum"].ToString()));
				FormEmailMessageEdit FormE = new FormEmailMessageEdit(msg);
				FormE.ShowDialog();
				if(FormE.DialogResult != DialogResult.OK) {
					return;
				}
			}
			ModuleSelected(PatCur.PatNum);
		}

		public void TaskGoToEvent(object sender,CancelEventArgs e) {
			FormTaskEdit FormT=(FormTaskEdit)sender;
			TaskObjectType GotoType=FormT.GotoType;
			long keyNum=FormT.GotoKeyNum;
			if(GotoType==TaskObjectType.None) {
				return;
			}
			if(GotoType == TaskObjectType.Patient) {
				if(keyNum != 0) {
					Patient pat = Patients.GetPat(keyNum);
					OnPatientSelected(pat.PatNum,pat.GetNameLF(),pat.Email != "",pat.ChartNumber);
					ModuleSelected(pat.PatNum);
					return;
				}
			}
			if(GotoType == TaskObjectType.Appointment) {
				//There's nothing to do here, since we're not in the appt module.
				return;
			}
		}

		private void checkShowTP_Click(object sender,EventArgs e) {
			FillProgNotes();
		}

		private void checkShowC_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkShowE_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkShowR_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkAppt_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkComm_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkLabCase_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkRx_Click(object sender,EventArgs e) {
		if (checkRx.Checked)//since there is no double click event...this allows almost the same thing
            {
                checkShowTP.Checked=false;
                checkShowC.Checked=false;
                checkShowE.Checked=false;
                checkShowR.Checked=false;
                checkNotes.Checked=true;
                checkRx.Checked=true;
                checkComm.Checked=false;
                checkAppt.Checked=false;
				checkLabCase.Checked=false;
                checkExtraNotes.Checked=false;

            }

			FillProgNotes();

		}

		private void checkExtraNotes_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkNotes_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void butShowNone_Click(object sender,EventArgs e) {
			checkShowTP.Checked=false;
			checkShowC.Checked=false;
			checkShowE.Checked=false;
			checkShowR.Checked=false;
			checkAppt.Checked=false;
			checkComm.Checked=false;
			checkLabCase.Checked=false;
			checkRx.Checked=false;
			checkShowTeeth.Checked=false;

			FillProgNotes();

		}

		private void butShowAll_Click(object sender,EventArgs e) {
			checkShowTP.Checked=true;
			checkShowC.Checked=true;
			checkShowE.Checked=true;
			checkShowR.Checked=true;
			checkAppt.Checked=true;
			checkComm.Checked=true;
			checkLabCase.Checked=true;
			checkRx.Checked=true;
			checkShowTeeth.Checked=false;
			FillProgNotes();

		}

		private void gridProg_MouseUp(object sender,MouseEventArgs e) {

		}
		#endregion ProgressNotes

		private void checkShowFamilyComm_Click(object sender,EventArgs e) {
			FillComm();
		}

		private void labelInsRem_MouseEnter(object sender,EventArgs e) {
			groupBoxFamilyIns.Visible=true;
			groupBoxIndIns.Visible=true;
		}

		private void labelInsRem_MouseLeave(object sender,EventArgs e) {
			groupBoxFamilyIns.Visible=false;
			groupBoxIndIns.Visible=false;
		}

	

		

		

		

		

	

		

	}

}











