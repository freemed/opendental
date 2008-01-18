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
		private System.Windows.Forms.CheckBox checkShowAll;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.ImageList imageListMain;
		private System.Windows.Forms.Panel panelSplitter;
		private OpenDental.UI.Button butComm;
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
		private CheckBox checkShowNotes;
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
		private Label label10;
		private Label labelAgeInsEst;
		private TextBox textAgeInsEst;
		private Label label8;
		private TextBox textAgeTotal;
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
		private TextBox textCC;
		private Panel panelCC;
		private Label labelCC;
		private Label label1;
		private TextBox textCCexp;
		private ContextMenu contextMenuPayment;
		private MenuItem menuItemProvIncTrans;
		private MenuItem menuItemStatementEmail;

		#region user variables
		///<summary>This will eventually hold all data needed for display.  It will be retrieved in one call to the database.</summary>
		private DataSet DataSetMain;
		public static bool PrintingStatement = false;
		///<summary>Indices of the items within CommLogs.List of items to actually show in the commlog list on this page. Right now, does not include Statement Sent entries.</summary>
		//private ArrayList CommIndices;
		private Family FamCur;
		///<summary>Public because used by FormRpStatement</summary>
		public Patient PatCur;
		private Benefit[] BenefitList;
		//private Commlog[] CommlogList;
		private PatientNote PatientNoteCur;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		private RepeatCharge[] RepeatChargeList;
		private PatPlan[] PatPlanList;
		//private Procedure[] AccProcList;
		private int OriginalMousePos;
		private ClaimProc[] ClaimProcList;
		///<summary>Used only in printing reports that show subtotal only.  Public because used by FormRpStatement</summary>
		public double SubTotal;
		private bool MouseIsDownOnSplitter;
		private int SplitterOriginalY;
		private bool FinNoteChanged;
		private bool CCChanged;
		private bool UrgFinNoteChanged;
		///<summary>Set to true if this control is placed in the recall edit window. This affects the control behavior.</summary>
		public bool ViewingInRecall=false;
		//private Procedure[] arrayProc;
		///<summary></summary>
		//private List<AcctLine> AcctLineList;
		///<summary>A single line of payment info in the Account.  Might be an actual payment, or an unattached paysplit, or a daily summary of attached paysplits.</summary>
		//private PayInfo[] arrayPay;
		//private Adjustment[] arrayAdj;
		//private Claim[] arrayClaim;
		//private Commlog[] arrayComm;
		//private PayPlan[] arrayPayPlan;
		private InsPlan[] InsPlanList;
		private FormPayment FormPayment2;
		private FormCommItem FormCommItem2;
		private Label labelAgeBalance;
		private FormPayPlan FormPayPlan2;
		#endregion

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
			this.checkShowAll = new System.Windows.Forms.CheckBox();
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
			this.checkShowNotes = new System.Windows.Forms.CheckBox();
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
			this.label10 = new System.Windows.Forms.Label();
			this.labelAgeInsEst = new System.Windows.Forms.Label();
			this.textAgeInsEst = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textAgeTotal = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.text0_30 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.text31_60 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.text61_90 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textOver90 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textCC = new System.Windows.Forms.TextBox();
			this.panelCC = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.textCCexp = new System.Windows.Forms.TextBox();
			this.labelCC = new System.Windows.Forms.Label();
			this.contextMenuPayment = new System.Windows.Forms.ContextMenu();
			this.menuItemProvIncTrans = new System.Windows.Forms.MenuItem();
			this.butShowNone = new OpenDental.UI.Button();
			this.butShowAll = new OpenDental.UI.Button();
			this.gridProg = new OpenDental.UI.ODGrid();
			this.gridRepeat = new OpenDental.UI.ODGrid();
			this.gridAccount = new OpenDental.UI.ODGrid();
			this.gridAcctPat = new OpenDental.UI.ODGrid();
			this.gridComm = new OpenDental.UI.ODGrid();
			this.textFinNotes = new OpenDental.ODtextBox();
			this.butTrojan = new OpenDental.UI.Button();
			this.butComm = new OpenDental.UI.Button();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.textUrgFinNote = new OpenDental.ODtextBox();
			this.labelAgeBalance = new System.Windows.Forms.Label();
			this.panelCommButs.SuspendLayout();
			this.panelProgNotes.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.panelAging.SuspendLayout();
			this.panelCC.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelFamFinancial
			// 
			this.labelFamFinancial.Font = new System.Drawing.Font("Microsoft Sans Serif",9.75F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelFamFinancial.Location = new System.Drawing.Point(767,313);
			this.labelFamFinancial.Name = "labelFamFinancial";
			this.labelFamFinancial.Size = new System.Drawing.Size(154,16);
			this.labelFamFinancial.TabIndex = 9;
			this.labelFamFinancial.Text = "Family Financial Notes";
			this.labelFamFinancial.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelUrgFinNote
			// 
			this.labelUrgFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif",9.75F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelUrgFinNote.Location = new System.Drawing.Point(766,31);
			this.labelUrgFinNote.Name = "labelUrgFinNote";
			this.labelUrgFinNote.Size = new System.Drawing.Size(165,21);
			this.labelUrgFinNote.TabIndex = 10;
			this.labelUrgFinNote.Text = "Fam Urgent Fin Note";
			this.labelUrgFinNote.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkShowAll
			// 
			this.checkShowAll.Checked = true;
			this.checkShowAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowAll.Font = new System.Drawing.Font("Microsoft Sans Serif",8F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.checkShowAll.Location = new System.Drawing.Point(4,27);
			this.checkShowAll.Name = "checkShowAll";
			this.checkShowAll.Size = new System.Drawing.Size(81,15);
			this.checkShowAll.TabIndex = 29;
			this.checkShowAll.Text = "Show history";
			this.checkShowAll.Click += new System.EventHandler(this.checkShowAll_Click);
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
			this.panelSplitter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseMove);
			this.panelSplitter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseDown);
			this.panelSplitter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseUp);
			// 
			// panelCommButs
			// 
			this.panelCommButs.Controls.Add(this.butTrojan);
			this.panelCommButs.Controls.Add(this.butComm);
			this.panelCommButs.Location = new System.Drawing.Point(749,429);
			this.panelCommButs.Name = "panelCommButs";
			this.panelCommButs.Size = new System.Drawing.Size(163,242);
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
            this.menuItemRepeatEmail});
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
			// checkShowNotes
			// 
			this.checkShowNotes.BackColor = System.Drawing.SystemColors.Control;
			this.checkShowNotes.Checked = true;
			this.checkShowNotes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowNotes.Font = new System.Drawing.Font("Microsoft Sans Serif",8F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.checkShowNotes.Location = new System.Drawing.Point(4,46);
			this.checkShowNotes.Name = "checkShowNotes";
			this.checkShowNotes.Size = new System.Drawing.Size(81,15);
			this.checkShowNotes.TabIndex = 209;
			this.checkShowNotes.Text = "Show Notes";
			this.checkShowNotes.UseVisualStyleBackColor = false;
			this.checkShowNotes.Click += new System.EventHandler(this.checkShowNotes_Click);
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
			this.panelAging.Controls.Add(this.labelAgeBalance);
			this.panelAging.Controls.Add(this.label10);
			this.panelAging.Controls.Add(this.labelAgeInsEst);
			this.panelAging.Controls.Add(this.textAgeInsEst);
			this.panelAging.Controls.Add(this.label8);
			this.panelAging.Controls.Add(this.textAgeTotal);
			this.panelAging.Controls.Add(this.label7);
			this.panelAging.Controls.Add(this.text0_30);
			this.panelAging.Controls.Add(this.label6);
			this.panelAging.Controls.Add(this.text31_60);
			this.panelAging.Controls.Add(this.label5);
			this.panelAging.Controls.Add(this.text61_90);
			this.panelAging.Controls.Add(this.label3);
			this.panelAging.Controls.Add(this.textOver90);
			this.panelAging.Controls.Add(this.label2);
			this.panelAging.Location = new System.Drawing.Point(193,25);
			this.panelAging.Name = "panelAging";
			this.panelAging.Size = new System.Drawing.Size(549,37);
			this.panelAging.TabIndex = 213;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label10.Location = new System.Drawing.Point(460,2);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(60,13);
			this.label10.TabIndex = 59;
			this.label10.Text = "= Balance";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelAgeInsEst
			// 
			this.labelAgeInsEst.Location = new System.Drawing.Point(389,2);
			this.labelAgeInsEst.Name = "labelAgeInsEst";
			this.labelAgeInsEst.Size = new System.Drawing.Size(60,13);
			this.labelAgeInsEst.TabIndex = 57;
			this.labelAgeInsEst.Text = "- InsEst";
			this.labelAgeInsEst.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textAgeInsEst
			// 
			this.textAgeInsEst.Location = new System.Drawing.Point(389,15);
			this.textAgeInsEst.Name = "textAgeInsEst";
			this.textAgeInsEst.ReadOnly = true;
			this.textAgeInsEst.Size = new System.Drawing.Size(60,20);
			this.textAgeInsEst.TabIndex = 56;
			this.textAgeInsEst.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(329,2);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(60,13);
			this.label8.TabIndex = 55;
			this.label8.Text = "Total";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textAgeTotal
			// 
			this.textAgeTotal.Location = new System.Drawing.Point(329,15);
			this.textAgeTotal.Name = "textAgeTotal";
			this.textAgeTotal.ReadOnly = true;
			this.textAgeTotal.Size = new System.Drawing.Size(60,20);
			this.textAgeTotal.TabIndex = 54;
			this.textAgeTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(91,2);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(60,13);
			this.label7.TabIndex = 53;
			this.label7.Text = "0-30";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text0_30
			// 
			this.text0_30.Location = new System.Drawing.Point(89,15);
			this.text0_30.Name = "text0_30";
			this.text0_30.ReadOnly = true;
			this.text0_30.Size = new System.Drawing.Size(60,20);
			this.text0_30.TabIndex = 52;
			this.text0_30.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(149,2);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60,13);
			this.label6.TabIndex = 51;
			this.label6.Text = "31-60";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text31_60
			// 
			this.text31_60.Location = new System.Drawing.Point(149,15);
			this.text31_60.Name = "text31_60";
			this.text31_60.ReadOnly = true;
			this.text31_60.Size = new System.Drawing.Size(60,20);
			this.text31_60.TabIndex = 50;
			this.text31_60.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(209,2);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60,13);
			this.label5.TabIndex = 49;
			this.label5.Text = "61-90";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text61_90
			// 
			this.text61_90.Location = new System.Drawing.Point(209,15);
			this.text61_90.Name = "text61_90";
			this.text61_90.ReadOnly = true;
			this.text61_90.Size = new System.Drawing.Size(60,20);
			this.text61_90.TabIndex = 48;
			this.text61_90.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(269,2);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60,13);
			this.label3.TabIndex = 47;
			this.label3.Text = "over 90";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textOver90
			// 
			this.textOver90.Location = new System.Drawing.Point(269,15);
			this.textOver90.Name = "textOver90";
			this.textOver90.ReadOnly = true;
			this.textOver90.Size = new System.Drawing.Size(60,20);
			this.textOver90.TabIndex = 46;
			this.textOver90.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label2.Location = new System.Drawing.Point(9,17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78,18);
			this.label2.TabIndex = 45;
			this.label2.Text = "Family Aging";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCC
			// 
			this.textCC.Location = new System.Drawing.Point(31,0);
			this.textCC.Name = "textCC";
			this.textCC.Size = new System.Drawing.Size(131,20);
			this.textCC.TabIndex = 214;
			this.textCC.Text = "1234-5678-1234-5678";
			this.textCC.TextChanged += new System.EventHandler(this.textCC_TextChanged);
			this.textCC.Leave += new System.EventHandler(this.textCC_Leave);
			// 
			// panelCC
			// 
			this.panelCC.Controls.Add(this.label1);
			this.panelCC.Controls.Add(this.textCCexp);
			this.panelCC.Controls.Add(this.labelCC);
			this.panelCC.Controls.Add(this.textCC);
			this.panelCC.Location = new System.Drawing.Point(769,130);
			this.panelCC.Name = "panelCC";
			this.panelCC.Size = new System.Drawing.Size(162,41);
			this.panelCC.TabIndex = 215;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0,23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70,15);
			this.label1.TabIndex = 217;
			this.label1.Text = "CC Expire";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCCexp
			// 
			this.textCCexp.Location = new System.Drawing.Point(71,20);
			this.textCCexp.Name = "textCCexp";
			this.textCCexp.Size = new System.Drawing.Size(91,20);
			this.textCCexp.TabIndex = 216;
			this.textCCexp.TextChanged += new System.EventHandler(this.textCCexp_TextChanged);
			this.textCCexp.Leave += new System.EventHandler(this.textCCexp_Leave);
			// 
			// labelCC
			// 
			this.labelCC.Location = new System.Drawing.Point(0,3);
			this.labelCC.Name = "labelCC";
			this.labelCC.Size = new System.Drawing.Size(30,15);
			this.labelCC.TabIndex = 215;
			this.labelCC.Text = "CC#";
			this.labelCC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// contextMenuPayment
			// 
			this.contextMenuPayment.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemProvIncTrans});
			// 
			// menuItemProvIncTrans
			// 
			this.menuItemProvIncTrans.Index = 0;
			this.menuItemProvIncTrans.Text = "Provider Income Transfer";
			this.menuItemProvIncTrans.Click += new System.EventHandler(this.menuItemProvIncTrans_Click);
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
			this.gridRepeat.Location = new System.Drawing.Point(0,62);
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
			this.gridAccount.HScrollVisible = false;
			this.gridAccount.Location = new System.Drawing.Point(0,138);
			this.gridAccount.Name = "gridAccount";
			this.gridAccount.ScrollValue = 0;
			this.gridAccount.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridAccount.Size = new System.Drawing.Size(749,284);
			this.gridAccount.TabIndex = 73;
			this.gridAccount.Title = "Patient Account";
			this.gridAccount.TranslationName = "TableAccount";
			this.gridAccount.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAccount_CellClick);
			this.gridAccount.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAccount_CellDoubleClick);
			// 
			// gridAcctPat
			// 
			this.gridAcctPat.HScrollVisible = false;
			this.gridAcctPat.Location = new System.Drawing.Point(749,173);
			this.gridAcctPat.Name = "gridAcctPat";
			this.gridAcctPat.ScrollValue = 0;
			this.gridAcctPat.SelectedRowColor = System.Drawing.Color.DarkSalmon;
			this.gridAcctPat.Size = new System.Drawing.Size(183,137);
			this.gridAcctPat.TabIndex = 72;
			this.gridAcctPat.Title = "Select Patient";
			this.gridAcctPat.TranslationName = "TableAccountPat";
			this.gridAcctPat.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAcctPat_CellClick);
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
			// textFinNotes
			// 
			this.textFinNotes.AcceptsReturn = true;
			this.textFinNotes.Location = new System.Drawing.Point(769,332);
			this.textFinNotes.Multiline = true;
			this.textFinNotes.Name = "textFinNotes";
			this.textFinNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.FinancialNotes;
			this.textFinNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textFinNotes.Size = new System.Drawing.Size(162,90);
			this.textFinNotes.TabIndex = 70;
			this.textFinNotes.TextChanged += new System.EventHandler(this.textFinNotes_TextChanged);
			this.textFinNotes.Leave += new System.EventHandler(this.textFinNotes_Leave);
			// 
			// butTrojan
			// 
			this.butTrojan.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTrojan.Autosize = true;
			this.butTrojan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTrojan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTrojan.CornerRadius = 4F;
			this.butTrojan.Location = new System.Drawing.Point(3,29);
			this.butTrojan.Name = "butTrojan";
			this.butTrojan.Size = new System.Drawing.Size(146,25);
			this.butTrojan.TabIndex = 93;
			this.butTrojan.Text = "Send Transaction to Trojan";
			this.butTrojan.Click += new System.EventHandler(this.butTrojan_Click);
			// 
			// butComm
			// 
			this.butComm.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butComm.Autosize = true;
			this.butComm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butComm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butComm.CornerRadius = 4F;
			this.butComm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butComm.Location = new System.Drawing.Point(3,2);
			this.butComm.Name = "butComm";
			this.butComm.Size = new System.Drawing.Size(98,26);
			this.butComm.TabIndex = 68;
			this.butComm.Text = "Questionnaire";
			this.butComm.Click += new System.EventHandler(this.butComm_Click);
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
			// textUrgFinNote
			// 
			this.textUrgFinNote.AcceptsReturn = true;
			this.textUrgFinNote.BackColor = System.Drawing.Color.White;
			this.textUrgFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textUrgFinNote.ForeColor = System.Drawing.Color.Red;
			this.textUrgFinNote.Location = new System.Drawing.Point(769,53);
			this.textUrgFinNote.Multiline = true;
			this.textUrgFinNote.Name = "textUrgFinNote";
			this.textUrgFinNote.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textUrgFinNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textUrgFinNote.Size = new System.Drawing.Size(163,77);
			this.textUrgFinNote.TabIndex = 11;
			this.textUrgFinNote.TextChanged += new System.EventHandler(this.textUrgFinNote_TextChanged);
			this.textUrgFinNote.Leave += new System.EventHandler(this.textUrgFinNote_Leave);
			// 
			// labelAgeBalance
			// 
			this.labelAgeBalance.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelAgeBalance.ForeColor = System.Drawing.Color.Firebrick;
			this.labelAgeBalance.Location = new System.Drawing.Point(452,15);
			this.labelAgeBalance.Name = "labelAgeBalance";
			this.labelAgeBalance.Size = new System.Drawing.Size(78,18);
			this.labelAgeBalance.TabIndex = 60;
			this.labelAgeBalance.Text = "100.00";
			this.labelAgeBalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ContrAccount
			// 
			this.Controls.Add(this.panelCC);
			this.Controls.Add(this.labelFamFinancial);
			this.Controls.Add(this.checkShowNotes);
			this.Controls.Add(this.checkShowAll);
			this.Controls.Add(this.panelAging);
			this.Controls.Add(this.panelProgNotes);
			this.Controls.Add(this.gridRepeat);
			this.Controls.Add(this.gridAccount);
			this.Controls.Add(this.gridAcctPat);
			this.Controls.Add(this.gridComm);
			this.Controls.Add(this.textFinNotes);
			this.Controls.Add(this.panelSplitter);
			this.Controls.Add(this.panelCommButs);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.textUrgFinNote);
			this.Controls.Add(this.labelUrgFinNote);
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
			this.panelCC.ResumeLayout(false);
			this.panelCC.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		///<summary></summary>
		public void InitializeOnStartup() {
			//can't use Lan.F(this);
			Lan.C(this,new Control[]
				{
          checkShowAll,
					label2,
					label7,
					label6,
					label5,
					label3,
					label8,
					labelAgeInsEst,
					label10,
					labelUrgFinNote,
					labelFamFinancial,
					butComm,
					//butLetterSimple,
					//butLetterMerge,
					//butLabel,
					//butEmail,
					gridAccount,
					gridAcctPat,
					gridComm
				});
			LayoutToolBar();
			if(ViewingInRecall) {
				panelSplitter.Top=300;//start the splitter higher for recall window.
			}
			LayoutPanels();
		}

		private void ContrAccount_Load(object sender,System.EventArgs e) {
			this.Parent.MouseWheel+=new MouseEventHandler(Parent_MouseWheel);
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ODToolBarButton button;
			button=new ODToolBarButton(Lan.g(this,"Payment"),1,"","Payment");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=contextMenuPayment;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Adjustment"),2,"","Adjustment"));
			button=new ODToolBarButton(Lan.g(this,"New Claim"),3,"","Insurance");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=contextMenuIns;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Payment Plan"),-1,"","PayPlan"));
			if(!PrefB.GetBool("EasyHideRepeatCharges")) {
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
		}

		private void ContrAccount_Layout(object sender,System.Windows.Forms.LayoutEventArgs e) {
			//see LayoutPanels()
		}

		private void ContrAccount_Resize(object sender,EventArgs e) {
			if(PrefB.HList==null){
				return;//helps on startup.
			}
			LayoutPanels();
		}

		///<summary>This used to be a layout event, but that was making it get called far too frequently.  Now, this must explicitly and intelligently be called.</summary>
		private void LayoutPanels(){
			gridAccount.Height=panelSplitter.Top-gridAccount.Location.Y+1;
			gridComm.Top=panelSplitter.Bottom-1;
			gridComm.Height=Height-gridComm.Top;
			panelCommButs.Top=panelSplitter.Bottom-1;
			panelProgNotes.Top=panelSplitter.Bottom-1;
			panelProgNotes.Height=Height-panelProgNotes.Top;
			gridProg.Top=0;
			gridProg.Height=panelProgNotes.Height;
			/*
			panelBoldBalance.Left=329;
			panelBoldBalance.Top=29;
			panelInsInfo.Top = panelBoldBalance.Top + panelBoldBalance.Height;
			panelInsInfo.Left = panelBoldBalance.Left + panelBoldBalance.Width - panelInsInfo.Width;*/
			int left=textUrgFinNote.Left;//769;
			if(PrefB.GetBool("StoreCCnumbers")){
				panelCC.Visible=true;
				panelCC.Location=new Point(left,textUrgFinNote.Bottom);
				gridAcctPat.Location=new Point(left,panelCC.Bottom);
			}
			else{
				panelCC.Visible=false;
				gridAcctPat.Location=new Point(left,textUrgFinNote.Bottom);
			}
			labelFamFinancial.Location=new Point(left,gridAcctPat.Bottom);
			textFinNotes.Location=new Point(left,labelFamFinancial.Bottom);
			textFinNotes.Height=panelSplitter.Top-textFinNotes.Top;
		}

		///<summary></summary>
		public void ModuleSelected(int patNum) {
			RefreshModuleData(patNum,false);
			RefreshModuleScreen(false);
		}

		///<summary></summary>
		public void ModuleSelected(int patNum,bool isSelectingFamily) {
			RefreshModuleData(patNum,isSelectingFamily);
			RefreshModuleScreen(isSelectingFamily);
		}

		///<summary>Used when jumping to this module and directly to a claim.</summary>
		public void ModuleSelected(int patNum,int claimNum) {
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
				Patient patOld=FamCur.List[0].Copy();
				//Patients.CurOld=Patients.Cur.Copy();//important
				FamCur.List[0].FamFinUrgNote=textUrgFinNote.Text;
				Patients.Update(FamCur.List[0],patOld);
				//Patients.GetFamily(tempPat.PatNum);
				UrgFinNoteChanged=false;
			}
			if(FinNoteChanged) {
				PatientNoteCur.FamFinancial=textFinNotes.Text;
				PatientNotes.Update(PatientNoteCur, PatCur.Guarantor);
				FinNoteChanged=false;
			}
			if(CCChanged){
				CCSave();
				CCChanged=false;
			}
			FamCur=null;
			//Claims.List=null;
			//Commlogs.List=null;
			ClaimProcList=null;
			RepeatChargeList=null;
		}

		///<summary></summary>
		private void RefreshModuleData(int patNum,bool isSelectingFamily) {
			if(patNum==0) {
				PatCur=null;
				FamCur=null;
				DataSetMain=null;
				return;
			}
			FamCur=Patients.GetFamily(patNum);
			PatCur=FamCur.GetPatient(patNum);
			InsPlanList=InsPlans.Refresh(FamCur);
			PatPlanList=PatPlans.Refresh(patNum);
			BenefitList=Benefits.Refresh(PatPlanList);
			//CovPats.Refresh(InsPlanList,PatPlanList);
			PatientNoteCur=PatientNotes.Refresh(PatCur.PatNum,PatCur.Guarantor);
			//other tables are refreshed in FillAcctLineAL
			DateTime fromDate=DateTime.MinValue;
			if(!checkShowAll.Checked){
				fromDate=DateTime.Today.AddDays(-45);
			}
			DateTime toDate=DateTime.MaxValue;
			bool viewingInRecall=ViewingInRecall;
			if(PrefB.GetBool("FuchsOptionsOn")) {
				viewingInRecall=true;
			}
			DataSetMain=AccountModule.GetAll(patNum,viewingInRecall,fromDate,toDate,isSelectingFamily);
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
				butComm.Enabled=false;
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
				butComm.Enabled=true;
			}
			if(checkShowNotes.Tag!=null && checkShowNotes.Tag.ToString()!="JustClicked"){
				checkShowNotes.Checked=PrefB.GetBool("ShowNotesInAccount");
			}
			FillMain();
			FillPats(isSelectingFamily);
			FillMisc();
			FillAging();
			//FillInsInfo();
			FillRepeatCharges();
			LayoutPanels();
			if(ViewingInRecall || PrefB.GetBool("FuchsOptionsOn")) {
				panelProgNotes.Visible = true;
				FillProgNotes();
				if(PrefB.GetBool("FuchsOptionsOn")) {//show prog note options
					groupBox6.Visible = true;
					groupBox7.Visible = true;
					butShowAll.Visible = true;
					butShowNone.Visible = true;
				}
			}
			else{
				panelProgNotes.Visible = false;
				FillComm();
			}
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
			col=new ODGridColumn(Lan.g("TableAccountPat","Est Bal"),49,HorizontalAlignment.Right);
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
				gridAcctPat.SetSelected(FamCur.List.Length,true);
			}
			else{
				for(int i=0;i<FamCur.List.Length;i++) {
					if(FamCur.List[i].PatNum==PatCur.PatNum) {
						gridAcctPat.SetSelected(i,true);
					}
				}
			}
		}

		private void FillMisc() {
			textCC.Text="";
			textCCexp.Text="";
			if(PatCur==null) {
				textUrgFinNote.Text="";
				textFinNotes.Text="";
			}
			else{
				textUrgFinNote.Text=FamCur.List[0].FamFinUrgNote;
				textFinNotes.Text=PatientNoteCur.FamFinancial;
				textFinNotes.Select(textFinNotes.Text.Length+2,1);
				textFinNotes.ScrollToCaret();
				textUrgFinNote.SelectionStart=0;
				textUrgFinNote.ScrollToCaret();
				if(PrefB.GetBool("StoreCCnumbers")) {
					string cc=PatientNoteCur.CCNumber;
					if(Regex.IsMatch(cc,@"^\d{16}$")){
						textCC.Text=cc.Substring(0,4)+"-"+cc.Substring(4,4)+"-"+cc.Substring(8,4)+"-"+cc.Substring(12,4);
					}
					else{
						textCC.Text=cc;
					}
					if(PatientNoteCur.CCExpiration.Year>2000){
						textCCexp.Text=PatientNoteCur.CCExpiration.ToString("MM/yy");
					}
					else{
						textCCexp.Text="";
					}
				}
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

		private void FillAging() {
			if(PatCur!=null) {
				textOver90.Text=FamCur.List[0].BalOver90.ToString("F");
				text61_90.Text=FamCur.List[0].Bal_61_90.ToString("F");
				text31_60.Text=FamCur.List[0].Bal_31_60.ToString("F");
				text0_30.Text=FamCur.List[0].Bal_0_30.ToString("F");
				double total=FamCur.List[0].BalTotal;
				textAgeTotal.Text=total.ToString("F");
				if(PrefB.GetBool("BalancesDontSubtractIns")) {
					labelAgeInsEst.Visible=false;
					textAgeInsEst.Visible=false;
					labelAgeBalance.Text=total.ToString("f");
					//textBoldBalance.Text = textAgeBalance.Text;
					//labelAfterIns.Text = (total - FamCur.List[0].InsEst).ToString("F");
					//labelIns1.Text = "Est. After Ins:";
				}
				else {//this is much more common
					labelAgeInsEst.Visible=true;
					textAgeInsEst.Visible=true;
					textAgeInsEst.Text=FamCur.List[0].InsEst.ToString("F");
					labelAgeBalance.Text=(total-FamCur.List[0].InsEst).ToString("f");
					//textBoldBalance.Text = textAgeBalance.Text;
					//labelAfterIns.Text = total.ToString("F");
					//labelIns1.Text = "Total w/o Ins:";
				}
			}
			else {
				textOver90.Text="";
				text61_90.Text="";
				text31_60.Text="";
				text0_30.Text="";
				textAgeTotal.Text="";
				textAgeInsEst.Text="";
				labelAgeBalance.Text="";
			}
		}

		///<summary></summary>
		private void FillRepeatCharges() {
			if(PatCur==null) {
				gridRepeat.Visible=false;
				gridAccount.Location=gridRepeat.Location;
				return;
			}
			RepeatChargeList=RepeatCharges.Refresh(PatCur.PatNum);
			if(RepeatChargeList.Length==0) {
				gridRepeat.Visible=false;
				gridAccount.Location=gridRepeat.Location;
				return;
			}
			gridRepeat.Visible=true;
			gridRepeat.Height=75;
			gridAccount.Location=new Point(0,gridRepeat.Bottom+3);
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
		}

		/*
		///<summary>Used once in FillAcctLineAL. Returns a list of PayInfos organized by date.</summary>
		private PayInfo[] GetPayInfoList(PaySplit[] PaySplitList,Payment[] PaymentList){
			ArrayList retAL=new ArrayList();
			PayInfo payInfo;
			ArrayList splitsGroupedForPayment;
			//first, loop through all payments.  All payments will be included, but only part of the amount might be included.
			for(int i=0;i<PaymentList.Length;i++){
				payInfo=new PayInfo();
				payInfo.Type=PayInfoType.Payment;
				payInfo.Date=PaymentList[i].PayDate;
				payInfo.Description=DefB.GetName(DefCat.PaymentTypes,PaymentList[i].PayType)+" ";
				if(PaymentList[i].CheckNum!="") {
					payInfo.Description+="#"+PaymentList[i].CheckNum+" ";
				}
				if(PaymentList[i].PayType==0){//provider income transfer
					payInfo.Description+=Lan.g(this,"(internal provider income transfer $0)");
				}
				else{
					payInfo.Description+=PaymentList[i].PayAmt.ToString("c");
				}
				//if(PaymentList[i].IsSplit){
				//	payInfo.Description+=" "+Lan.g(this,"(split)");
				//}
				payInfo.Amount=0;
				payInfo.PayNum=PaymentList[i].PayNum;
				payInfo.PayPlanNum=PaySplits.GetPayPlanNum(PaymentList[i].PayNum,PatCur.PatNum,PaySplitList);
				//loop through all paysplits for this payment.
				splitsGroupedForPayment=PaySplits.GetGroupedForPayment(PaymentList[i].PayNum,PaySplitList);
				if(splitsGroupedForPayment.Count>1){
					payInfo.Description+="  "+Lan.g(this,"Splits:");
				}
				payInfo.Amount=PaySplits.GetAmountForPayment(PaymentList[i].PayNum,PaymentList[i].PayDate,PatCur.PatNum,PaySplitList);
				for(int j=0;j<splitsGroupedForPayment.Count;j++){
					//Amount: only those amounts that have the same procDate and patNum as the payment, and are not attached to procedures.
					//(The payment total amount and split detail will show in the description)
					//if(((PaySplit)splitsGroupedForPayment[j]).ProcDate==PaymentList[i].PayDate 
					//	&& ((PaySplit)splitsGroupedForPayment[j]).ProcNum==0
					//	&& ((PaySplit)splitsGroupedForPayment[j]).PatNum==PatCur.PatNum)
					//{
					//	payInfo.Amount+=((PaySplit)splitsGroupedForPayment[j]).SplitAmt;
					//}
					//then the description of each split, but don't include descriptions for splits with same date and patnum
					if(((PaySplit)splitsGroupedForPayment[j]).ProcDate==PaymentList[i].PayDate 
						&& ((PaySplit)splitsGroupedForPayment[j]).PatNum==PatCur.PatNum)
					{
						continue;
					}
					payInfo.Description+="\r\n"+((PaySplit)splitsGroupedForPayment[j]).SplitAmt.ToString("c")//$ amount
						+" "+((PaySplit)splitsGroupedForPayment[j]).ProcDate.ToShortDateString()
						+" "+FamCur.GetNameInFamFL(((PaySplit)splitsGroupedForPayment[j]).PatNum);//formatted name
				}
				retAL.Add(payInfo);
			}
			//then, paysplits
			//loop through to get all payments to find those needed for display
			ArrayList neededPayNums=new ArrayList();
			bool isOtherPat;
			for(int i=0;i<PaySplitList.Length;i++){
				//see the next loop for an explanation of these if statements
				if(PaySplitList[i].PatNum!=PatCur.PatNum){
					continue;
				}
				if(PaySplitList[i].ProcDate==PaySplitList[i].DatePay){
					isOtherPat=true;
					for(int j=0;j<PaymentList.Length;j++){
						if(PaymentList[j].PayNum==PaySplitList[i].PayNum){
							//if this split is attached to a payment by this pat
							isOtherPat=false;
						}
					}
					if(!isOtherPat){
						continue;
					}
				}
				if(PaySplitList[i].ProcNum>0){
					continue;
				}
				if(!neededPayNums.Contains(PaySplitList[i].PayNum)){
					neededPayNums.Add(PaySplitList[i].PayNum);
				}
			}
			int[] otherPayNums=new int[neededPayNums.Count];
			neededPayNums.CopyTo(otherPayNums);
			Payment[] otherPayList=Payments.GetPayments(otherPayNums);
			Payment payment;
			for(int i=0;i<PaySplitList.Length;i++){
				//if(PaySplitList[i].SplitAmt==38){
				//	MessageBox.Show("");
				//}
				if(PaySplitList[i].PatNum!=PatCur.PatNum){
					continue;
				}
				//don't include PaySplits with the same date as the payment.
				//Those will either be proc line items or part of the payment
				//UNLESS payment is in a different patient.
				if(PaySplitList[i].ProcDate==PaySplitList[i].DatePay){
					isOtherPat=true;
					for(int j=0;j<PaymentList.Length;j++){
						if(PaymentList[j].PayNum==PaySplitList[i].PayNum){
							//if this split is attached to a payment by this pat
							isOtherPat=false;
						}
					}
					if(!isOtherPat){
						continue;
					}
					//otherwise, if isOtherPat, then the payment will show
				}
				//don't include PaySplits that are attached to procs
				if(PaySplitList[i].ProcNum>0){
					continue;
				}
				payInfo=new PayInfo();
				payInfo.Type=PayInfoType.PaySplit;
				payInfo.Date=PaySplitList[i].ProcDate;
				payment=Payments.GetFromList(PaySplitList[i].PayNum,otherPayList);
				if(payment.PayType==0){//provider transfer
					payInfo.Description=Lan.g(this,"Provider transfer");//+" "
						//+" "+PaySplitList[i].DatePay.ToShortDateString();
				}
				else{
					payInfo.Description=Lan.g(this,"Split of")+" "+payment.PayAmt.ToString("c")
						+" "+Lan.g(this,"payment by")+"\r\n"
						+FamCur.GetNameInFamFL(payment.PatNum)//formatted name
						+" "+PaySplitList[i].DatePay.ToShortDateString();
				}
				payInfo.Amount=PaySplitList[i].SplitAmt;
				payInfo.PayNum=PaySplitList[i].PayNum;
				payInfo.PayPlanNum=PaySplitList[i].PayPlanNum;
				retAL.Add(payInfo);
			}
			//convert to array
			PayInfo[] retVal=new PayInfo[retAL.Count];
			DateTime[] dateArray=new DateTime[retAL.Count];
			for(int i=0;i<retVal.Length;i++){
				retVal[i]=(PayInfo)retAL[i];
				dateArray[i]=retVal[i].Date;
			}
			//order everything by date
			Array.Sort(dateArray,retVal);
			return retVal;
		}*/

		/*
		///<summary></summary>
		private void FillAcctLineList(DateTime fromDate, DateTime toDate){//,bool includeUnclearedClaims,bool subtotalsOnly){
			AccProcList=Procedures.Refresh(PatCur.PatNum);
			List<Claim> ClaimList=Claims.Refresh(PatCur.PatNum);
			Adjustment[] AdjustmentList=Adjustments.Refresh(PatCur.PatNum);
			PaySplit[] PaySplitListAll=PaySplits.Refresh(PatCur.PatNum);//Also contains splits that are not for this patient
			PaySplit[] PaySplitList=PaySplits.GetForPatient(PatCur.PatNum,PaySplitListAll);
			Payment[] PaymentList=Payments.Refresh(PatCur.PatNum);//for display purposes only.
			PayInfo[] PayInfoList=GetPayInfoList(PaySplitListAll,PaymentList);
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			CommlogList=Commlogs.Refresh(PatCur.PatNum);
			PayPlan[] PayPlanList=PayPlans.Refresh(PatCur.PatNum,PatCur.PatNum);//where this patient is either guar or pat
			PayPlanCharge[] PayPlanChargeList=PayPlanCharges.Refresh(PatCur.PatNum);
			//if the computed balance does not match the balance on record,
			//5/3/05 changed this to always compute aging. Accuracy is more important than speed.
			//if(Shared.ComputeBalances(AccProcList,ClaimProcList,PatCur,PaySplitList,AdjustmentList)){
			Shared.ComputeBalances(AccProcList,ClaimProcList,PatCur,PaySplitList,AdjustmentList,PayPlanList,PayPlanChargeList);
			//if(!PrefB.GetBool("SkipComputeAgingInAccount")){
				//then recompute aging for family. This is time consuming, about 1/2 second.
				//Compute aging involves about 10 to 12 database calls.  (We plan to reduce this later)
				if(PrefB.GetBool("AgingCalculatedMonthlyInsteadOfDaily")){
					Ledgers.ComputeAging(PatCur.Guarantor,PIn.PDate(PrefB.GetString("DateLastAging")));
				}
				else{
					Ledgers.ComputeAging(PatCur.Guarantor,DateTime.Today);
				}
				Patients.UpdateAging(PatCur.Guarantor,Ledgers.Bal[0],Ledgers.Bal[1],Ledgers.Bal[2]
					,Ledgers.Bal[3],Ledgers.InsEst,Ledgers.BalTotal);
				FamCur=Patients.GetFamily(PatCur.PatNum);
				PatCur=FamCur.GetPatient(PatCur.PatNum);
			//}
			arrayProc = new Procedure[AccProcList.Length];
			arrayClaim=new Claim[ClaimList.Count];
			arrayAdj = new Adjustment[AdjustmentList.Length];
			arrayPay =new PayInfo[PayInfoList.Length];
			arrayComm =new Commlog[CommlogList.Length];
			arrayPayPlan=new PayPlan[PayPlanList.Length];
			//step through all procedures for patient and move selected ones (completed) to
			//arrayProc, also arrayClaim, arrayAdj ,arrayPay, all ordered by date.
			//Pull from all 4 into AcctLineAL array for display.  Every AcctLineAL entry
			//contains type and index to access original array. Notes are handled like any
			//other line, just no numbers.(actually no notes yet)
			int countProc=0;
			int countClaim=0;
			int countAdj=0;
			int countPay=0;
			int countComm=0;
			int countPayPlan=0;
			for(int i=0;i<AccProcList.Length;i++){
				if(AccProcList[i].ProcStatus==ProcStat.C)//Only add if proc is Complete
				{
					arrayProc[countProc]=AccProcList[i];
					countProc++;
				}
			}
			for(int i=0;i<ClaimList.Count;i++){
				if(ClaimList[i].ClaimStatus!="A"//don't show ins adjustments
					&& ClaimList[i].ClaimType!="PreAuth")//don't show preauthorizations.
				{
					arrayClaim[countClaim]=ClaimList[i];
					countClaim++;
				}
			}
			for(int i=0;i<AdjustmentList.Length;i++){
				if(AdjustmentList[i].ProcNum==0)//only if not attached to a proc
				{
					arrayAdj[countAdj]=AdjustmentList[i];
					countAdj++;
				}
			}
			for(int i=0;i<PayInfoList.Length;i++){
				arrayPay[countPay]=PayInfoList[i];
				countPay++;
			}
			for(int i=0;i<CommlogList.Length;i++){
				if(CommlogList[i].IsStatementSent){//only show statementSents.
					arrayComm[countComm]=CommlogList[i];
					countComm++;
				}
			}
			for(int i=0;i<PayPlanList.Length;i++){
				arrayPayPlan[countPayPlan]=PayPlanList[i];
				countPayPlan++;
			}
			int tempCountProc=0;
			int tempCountClaim=0;
			int tempCountAdj=0;
			int tempCountPay=0;
			int tempCountComm=0;
			int tempCountPayPlan=0;
			AcctLineList=new List<AcctLine>();
			AcctLine tempAcctLine=new AcctLine();
			//This is where to transfer arrays to AcctLineAL:
			DateTime lineDate=DateTime.MinValue;
				//tempAcctLine.Description="Starting Balance";
			double runBal=0;
				//tempAcctLine.Balance=runBal.ToString("F");
				//AcctLineAL.Add(tempAcctLine);
			ProcedureCode procedurecode;
			for(int j=0;j<countProc+countClaim+countAdj+countPay+countComm+countPayPlan+1;j++){
			//for(int i=0;i<AcctLineAL.Length;i++){
				//set lineDate to the value of the first array that is not maxed out:
				if     (tempCountProc<countProc) lineDate=arrayProc[tempCountProc].ProcDate;
				else if(tempCountClaim<countClaim) lineDate=arrayClaim[tempCountClaim].DateService;
				else if(tempCountAdj<countAdj) lineDate=arrayAdj[tempCountAdj].AdjDate;
				else if(tempCountPay<countPay) lineDate=arrayPay[tempCountPay].Date;
				else if(tempCountComm<countComm) lineDate=arrayComm[tempCountComm].CommDateTime.Date;
				else if(tempCountPayPlan<countPayPlan) lineDate=arrayPayPlan[tempCountPayPlan].PayPlanDate;
				//find next date
				if(tempCountProc<countProc && DateTime.Compare(arrayProc[tempCountProc].ProcDate,lineDate)<=0)
					lineDate=arrayProc[tempCountProc].ProcDate;
				if(tempCountClaim<countClaim && DateTime.Compare(arrayClaim[tempCountClaim].DateService,lineDate)<0)
					lineDate=arrayClaim[tempCountClaim].DateService;
				if(tempCountAdj<countAdj && DateTime.Compare(arrayAdj[tempCountAdj].AdjDate,lineDate)<0)
					lineDate=arrayAdj[tempCountAdj].AdjDate;
				if(tempCountPay<countPay 
					&& DateTime.Compare(arrayPay[tempCountPay].Date,lineDate)<=0)
					lineDate=arrayPay[tempCountPay].Date;
				if(tempCountComm<countComm
					&& DateTime.Compare(arrayComm[tempCountComm].CommDateTime.Date,lineDate)<=0)
					lineDate=arrayComm[tempCountComm].CommDateTime.Date;
				if(tempCountPayPlan<countPayPlan
					&& DateTime.Compare(arrayPayPlan[tempCountPayPlan].PayPlanDate,lineDate)<=0)
					lineDate=arrayPayPlan[tempCountPayPlan].PayPlanDate;
				//1. Procedure
				if(tempCountProc<countProc && arrayProc[tempCountProc].ProcDate==lineDate){
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctModType.Proc;
					tempAcctLine.Index=tempCountProc;
					tempAcctLine.Date=arrayProc[tempCountProc].ProcDate.ToString("d");
					tempAcctLine.Provider=Providers.GetAbbr(arrayProc[tempCountProc].ProvNum);
					procedurecode=ProcedureCodes.GetProcCode(arrayProc[tempCountProc].CodeNum);
					tempAcctLine.Code=procedurecode.ProcCode;
					tempAcctLine.Tooth=Tooth.ToInternat(arrayProc[tempCountProc].ToothNum);
					if(procedurecode.LaymanTerm=="") {
						tempAcctLine.Description=procedurecode.Descript;
					}
					else {
						tempAcctLine.Description=procedurecode.LaymanTerm;
					}
					if(arrayProc[tempCountProc].MedicalCode!=""){
						tempAcctLine.Description=Lan.g(this,"(medical)")+" "+tempAcctLine.Description;
					}
					double fee=arrayProc[tempCountProc].ProcFee;
					int qty = arrayProc[tempCountProc].BaseUnits + arrayProc[tempCountProc].UnitQty;
					if(qty > 0){
						fee*=qty;
					}
					double insEst=ClaimProcs.ProcEstNotReceived(ClaimProcList,arrayProc[tempCountProc].ProcNum);
					double insPay=ClaimProcs.ProcInsPay(ClaimProcList,arrayProc[tempCountProc].ProcNum);
					double discount=0;
					//if(!PrefB.GetBool("BalancesDontSubtractIns")){//this is the typical situation
					//this applies to everyone regardless of 'balancesDontSubtractIns', because it's not an insurance item.
					discount=Procedures.GetWriteOffC(arrayProc[tempCountProc],ClaimProcList);//this is for CapComplete and all other writeoffs
					//}
					double pat=fee-insPay;
					if(!PrefB.GetBool("BalancesDontSubtractIns")){//this is the typical situation
						pat-=insEst;
					}			
					double adj=Adjustments.GetTotForProc(arrayProc[tempCountProc].ProcNum,AdjustmentList)-discount;
					double paid=PaySplits.GetTotForProc(arrayProc[tempCountProc].ProcNum,PaySplitList);
					double subtot=pat+adj-paid;
					tempAcctLine.Fee=fee.ToString("F");
					tempAcctLine.InsEst=ClaimProcs.ProcDisplayInsEst(ClaimProcList,arrayProc[tempCountProc].ProcNum);
					tempAcctLine.InsPay=insPay.ToString("F");
					tempAcctLine.Patient=pat.ToString("F");
					if(adj!=0){
						tempAcctLine.Adj=adj.ToString("F");
					}
					if(paid!=0){
						tempAcctLine.Paid=(-paid).ToString("F");
					}		
					if(!Procedures.IsCoveredIns(arrayProc[tempCountProc],ClaimProcList)){//not covered by ins
						tempAcctLine.InsEst="";
						tempAcctLine.InsPay="";
					}
					else if(Procedures.NoBillIns(arrayProc[tempCountProc],ClaimProcList)){//should not bill to ins
						tempAcctLine.InsEst="";
						tempAcctLine.InsPay="No Bill";
					}
					else if(Procedures.IsUnsent(arrayProc[tempCountProc],ClaimProcList)
						&& arrayProc[tempCountProc].ProcFee>0)
					{
						tempAcctLine.InsPay="Unsent";
					}
					if(arrayProc[tempCountProc].ProcDate >= fromDate 
						&& arrayProc[tempCountProc].ProcDate <= toDate)//within date range
					{
						runBal+=subtot;
						tempAcctLine.Balance=runBal.ToString("F");
						if (simpleStatement)
							{
								tempAcctLine.InsEst="";
								tempAcctLine.InsPay="";
								tempAcctLine.Patient="";
								tempAcctLine.Balance="";
							}
   
						AcctLineList.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal+=subtot;//add to the running balance, but do not display it.
					}
					if(tempCountProc<countProc){
						tempCountProc++;
					}
				}//end Proc
				//2. Claim
				else if(tempCountClaim<countClaim && DateTime.Compare(arrayClaim[tempCountClaim].DateService,lineDate)==0){
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctModType.Claim;
					tempAcctLine.Index=tempCountClaim;
					tempAcctLine.Date=arrayClaim[tempCountClaim].DateService.ToString("d");
					tempAcctLine.Provider=Providers.GetAbbr(arrayClaim[tempCountClaim].ProvTreat);
					tempAcctLine.Code="Ins";
					tempAcctLine.Tooth="";
					if(arrayClaim[tempCountClaim].ClaimType=="P"){
						tempAcctLine.Description=Lan.g(this,"Pri Claim")+" ";
					}
					else if(arrayClaim[tempCountClaim].ClaimType=="S"){
						tempAcctLine.Description=Lan.g(this,"Sec Claim")+" ";
					}
					Claim ClaimCur=arrayClaim[tempCountClaim];
					tempAcctLine.Description+=ClaimCur.ClaimFee.ToString("c")+" ";
					tempAcctLine.Description+=InsPlans.GetCarrierName(arrayClaim[tempCountClaim].PlanNum,InsPlanList);
					if(arrayClaim[tempCountClaim].DedApplied>0){
						tempAcctLine.Description+="\r\n"+Lan.g(this,"Ded applied $")+arrayClaim[tempCountClaim].DedApplied.ToString("F");
					}
					double fee;
					double insPay;
					double subTotal=0;
					fee=ClaimCur.ClaimFee;
					//insPay is always subtracted from bal no matter what is displayed.
					insPay=ClaimProcs.ClaimByTotalOnly(ClaimProcList,arrayClaim[tempCountClaim].ClaimNum);
					subTotal-=insPay;
					if(arrayClaim[tempCountClaim].ClaimStatus=="R"){//if claim is received
						//show the insurance payment. Just for display.
						//The byTotal is the only number that affects the balance.
						if(insPay>0){
							tempAcctLine.InsPay=insPay.ToString("F");//only the parts not by proc
						}
						tempAcctLine.Description+="\r\n"+Lan.g(this,"Received")+" "+ClaimCur.DateReceived.ToShortDateString();
						tempAcctLine.Description+="\r\n"+Lan.g(this,"Paid")+" "+ClaimCur.InsPayAmt.ToString("c");
					}
					else{//claim not received, so it is an estimate
						switch(arrayClaim[tempCountClaim].ClaimStatus){
							case "U":
								tempAcctLine.InsPay=Lan.g(this,"Unsent");
								break;
							case "H":
								tempAcctLine.InsPay=Lan.g(this,"Hold");
								break;
							case "W":
								tempAcctLine.InsPay=Lan.g(this,"In Queue");
								break;
							case "P":
								tempAcctLine.InsPay=Lan.g(this,"In Queue");
								break;
							case "S":
								tempAcctLine.InsPay=Lan.g(this,"Sent");
								break;
						}
						//subTotal-=insEst;
						//FamInsEst+=insEst;//for printing family
					}
					//runBal-=arrayClaim[tempCountClaim].WriteOff;
					if(arrayClaim[tempCountClaim].WriteOff>0){
						double writeoff=ClaimProcs.ClaimWriteoffByTotalOnly(ClaimProcList,arrayClaim[tempCountClaim].ClaimNum);
						if(writeoff>0){
							tempAcctLine.Adj="-"+writeoff.ToString("F");
							subTotal-=writeoff;
						}
					}
					if(arrayClaim[tempCountClaim].ReasonUnderPaid!=""){
						tempAcctLine.Description+="\r\n"+arrayClaim[tempCountClaim].ReasonUnderPaid;
					}
					tempAcctLine.Patient="";
					if(arrayClaim[tempCountClaim].DateService >= fromDate
						&& arrayClaim[tempCountClaim].DateService <= toDate){//within date range
						runBal+=subTotal;
						tempAcctLine.Balance=runBal.ToString("F");
						if (simpleStatement)
							{
								tempAcctLine.InsEst="";
								//tempAcctLine.InsPay="";//we want to show the status of the claim
								tempAcctLine.Patient="";
								tempAcctLine.Balance="";
							}
 						AcctLineList.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal+=subTotal;//add to the running balance, but do not display it.
					}
					//uncleared claims that are not within the daterange
					else if(includeUnclearedClaims && arrayClaim[tempCountClaim].ClaimStatus != "R"){
						tempAcctLine.Balance="";//don't show running balance
						if (simpleStatement)
							{
								tempAcctLine.InsEst="";
								tempAcctLine.InsPay="";
								tempAcctLine.Patient="";
								tempAcctLine.Balance="";
							}
						AcctLineList.Add(tempAcctLine);
					}
					if(tempCountClaim<countClaim) tempCountClaim++;
				}//end Claim
				//3. Adjustment
				else if(tempCountAdj<countAdj && DateTime.Compare(arrayAdj[tempCountAdj].AdjDate,lineDate)==0){
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctModType.Adj;
					tempAcctLine.Index=tempCountAdj;
					tempAcctLine.Date=arrayAdj[tempCountAdj].AdjDate.ToShortDateString();
					tempAcctLine.Provider=Providers.GetAbbr(arrayAdj[tempCountAdj].ProvNum);
					tempAcctLine.Code="Adjust";
					tempAcctLine.Tooth="";
					if (checkShowNotes.Checked && !PrintingStatement){
						tempAcctLine.Description = DefB.GetName(DefCat.AdjTypes, arrayAdj[tempCountAdj].AdjType) + " - " + arrayAdj[tempCountAdj].AdjNote;
					}
					else{
						tempAcctLine.Description = DefB.GetName(DefCat.AdjTypes, arrayAdj[tempCountAdj].AdjType);
					}

					tempAcctLine.Fee="";
					tempAcctLine.InsEst="";
					tempAcctLine.InsPay="";
					//can be a positive or negative number:
					tempAcctLine.Adj=arrayAdj[tempCountAdj].AdjAmt.ToString("F");
					if(arrayAdj[tempCountAdj].AdjDate >= fromDate && arrayAdj[tempCountAdj].AdjDate <= toDate){
						runBal+=arrayAdj[tempCountAdj].AdjAmt;
						tempAcctLine.Balance=runBal.ToString("F");
						if (simpleStatement)
							{
								tempAcctLine.InsEst="";
								tempAcctLine.InsPay="";
								tempAcctLine.Patient="";
								tempAcctLine.Balance="";
							}
						AcctLineList.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal+=arrayAdj[tempCountAdj].AdjAmt;//add to the running balance, but do not display it.
					}
					if(tempCountAdj<countAdj) tempCountAdj++;
				}//end Adjustment
				//4. Payment:
				else if(tempCountPay<countPay && DateTime.Compare(arrayPay[tempCountPay].Date,lineDate)==0){
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctModType.Pay;
					tempAcctLine.Code="Pay";
                        //techchange  trying to show notes..but can't figure out how to do it here
                        if (checkShowNotes.Checked && !PrintingStatement)
                        {
                            tempAcctLine.Description = arrayPay[tempCountPay].Description;
                        }
                        else
                        {
                            tempAcctLine.Description = arrayPay[tempCountPay].Description;

                        }
						//Payments.GetInfo(arrayPay[tempCountPay].PayNum);
					tempAcctLine.Index=tempCountPay;
					tempAcctLine.Date=arrayPay[tempCountPay].Date.ToString("d");
					tempAcctLine.Provider="";//since payments are usually split, leave empty
					tempAcctLine.Tooth="";
					tempAcctLine.Fee="";
					tempAcctLine.InsEst="";
					tempAcctLine.InsPay="";
					tempAcctLine.Patient="";
					tempAcctLine.Adj="";
					if(arrayPay[tempCountPay].Amount!=0){
						tempAcctLine.Paid=(-arrayPay[tempCountPay].Amount).ToString("F");
					}
					if(arrayPay[tempCountPay].Date >= fromDate
						&& arrayPay[tempCountPay].Date <= toDate){
						runBal-=arrayPay[tempCountPay].Amount;
						tempAcctLine.Balance=runBal.ToString("F");
						if (simpleStatement)
							{
								tempAcctLine.InsEst="";
								tempAcctLine.InsPay="";
								tempAcctLine.Patient="";
								tempAcctLine.Balance="";
							}
						AcctLineList.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal-=arrayPay[tempCountPay].Amount;//add to the running balance, but do not display it.
					}
					if(tempCountPay<countPay) tempCountPay++;
				}//end Payment
				//5. Comm:
				else if(tempCountComm<countComm 
					&& arrayComm[tempCountComm].CommDateTime.Date==lineDate)
				{
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctModType.Comm;
					tempAcctLine.Code="Comm";
					tempAcctLine.Description=Lan.g(this,"Sent Statement");
					if(arrayComm[tempCountComm].Mode_!=CommItemMode.None){
						tempAcctLine.Description+="-"+Lan.g("enumCommItemMode",arrayComm[tempCountComm].Mode_.ToString());
					}
					if (checkShowNotes.Checked && !PrintingStatement && (arrayComm[tempCountComm].Note!="")){
						tempAcctLine.Description+=" - "+(arrayComm[tempCountComm].Note);
					}
					tempAcctLine.Index=tempCountComm;
					tempAcctLine.Date=arrayComm[tempCountComm].CommDateTime.ToShortDateString();
					tempAcctLine.Provider="";
					tempAcctLine.Tooth="";
					tempAcctLine.Fee="";
					tempAcctLine.InsEst="";
					tempAcctLine.InsPay="";
					tempAcctLine.Patient="";
					//adj
					//paid
					tempAcctLine.Balance="";
					if(arrayComm[tempCountComm].CommDateTime.Date >= fromDate
						&& arrayComm[tempCountComm].CommDateTime.Date <= toDate){
						if (simpleStatement)
							{
								tempAcctLine.InsEst="";
								tempAcctLine.InsPay="";
								tempAcctLine.Patient="";
								tempAcctLine.Balance="";
							}
						AcctLineList.Add(tempAcctLine);
					}
					if(tempCountComm<countComm) tempCountComm++;
				}//end Comm
				//6. PayPlan:
				else if(tempCountPayPlan<countPayPlan 
					&& DateTime.Compare(arrayPayPlan[tempCountPayPlan].PayPlanDate,lineDate)==0)
				{
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctModType.PayPlan;
					tempAcctLine.Code="PayPln";
					double amtPaid=PayPlans.GetAmtPaid(arrayPayPlan[tempCountPayPlan].PayPlanNum);
					if(arrayPayPlan[tempCountPayPlan].PlanNum>0){
						tempAcctLine.Description=Lan.g(this,"Expected payments from ")
							+InsPlans.GetDescript(arrayPayPlan[tempCountPayPlan].PlanNum,FamCur,InsPlanList)+"\r\n";
					}
					else{
						tempAcctLine.Description=Lan.g(this,"Payment Plan for ")+FamCur.GetNameInFamFL(arrayPayPlan[tempCountPayPlan].PatNum)+"\r\n";
					}
					tempAcctLine.Description+=
						Lan.g(this,"Principal")+" "+PayPlans.GetTotalPrinc
							(arrayPayPlan[tempCountPayPlan].PayPlanNum,PayPlanChargeList).ToString("c")+"\r\n"
						+Lan.g(this,"Accumulated amt due")+" "+PayPlans.GetAccumDue
							(arrayPayPlan[tempCountPayPlan].PayPlanNum,PayPlanChargeList).ToString("c")+"\r\n";
					if(arrayPayPlan[tempCountPayPlan].PlanNum==0){//this line doesn't show for ins pay plans
						tempAcctLine.Description+=
							Lan.g(this,"Principal paid")+" "+PayPlans.GetPrincPaid
								(amtPaid,arrayPayPlan[tempCountPayPlan].PayPlanNum,PayPlanChargeList).ToString("c")+"\r\n";
					}
					tempAcctLine.Index=tempCountPayPlan;
					tempAcctLine.Date=arrayPayPlan[tempCountPayPlan].PayPlanDate.ToShortDateString();
					tempAcctLine.Provider="";
					tempAcctLine.Tooth="";
					tempAcctLine.Fee="";//arrayPayPlan[tempCountPayPlan].TotalAmount.ToString("F");
					tempAcctLine.InsEst="";
					tempAcctLine.InsPay="";
					double subTotal=0;
					//either or both of these conditions might be true (3 possible scenarios)
					//1.If this is guarantor
					if(arrayPayPlan[tempCountPayPlan].Guarantor==PatCur.PatNum){
						tempAcctLine.Patient=PayPlans.GetAccumDue(arrayPayPlan[tempCountPayPlan].PayPlanNum,PayPlanChargeList).ToString("F");
						subTotal+=PayPlans.GetAccumDue(arrayPayPlan[tempCountPayPlan].PayPlanNum,PayPlanChargeList);
					}
					//2.If this is the patient
					if(arrayPayPlan[tempCountPayPlan].PatNum==PatCur.PatNum){
						tempAcctLine.Adj=(-PayPlans.GetTotalPrinc(arrayPayPlan[tempCountPayPlan].PayPlanNum,PayPlanChargeList)).ToString("F");
						subTotal-=PayPlans.GetTotalPrinc(arrayPayPlan[tempCountPayPlan].PayPlanNum,PayPlanChargeList);
					}
					
					if(arrayPayPlan[tempCountPayPlan].PayPlanDate >= fromDate
						&& arrayPayPlan[tempCountPayPlan].PayPlanDate <= toDate)
						//|| arrayPayPlan[tempCountPayPlan].CurrentDue>0)
					{
						runBal+=subTotal;
						tempAcctLine.Balance=runBal.ToString("F");
						if (simpleStatement)
							{
								tempAcctLine.InsEst="";
								tempAcctLine.InsPay="";
								tempAcctLine.Patient="";
								tempAcctLine.Balance="";
							}
						AcctLineList.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal+=subTotal;//add to the running balance, but do not display it.
					}
					if(tempCountPayPlan<countPayPlan) tempCountPayPlan++;
				}//end PayPlan
			}//end for line
			SubTotal=runBal;
			//for (int i=0;i<countProc;i++){
			//}//end for i countProc
		}*/

		private void FillMain(){
			gridAccount.BeginUpdate();
			gridAccount.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableAccount","Date"),65);
			gridAccount.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAccount","Patient"),100);
			gridAccount.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAccount","Prov"),40);
			gridAccount.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAccount","Code"),46);
			gridAccount.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAccount","Tth"),26);
			gridAccount.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAccount","Description"),270);
			gridAccount.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAccount","Charges"),60,HorizontalAlignment.Right);
			gridAccount.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAccount","Credits"),60,HorizontalAlignment.Right);
			gridAccount.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAccount","Balance"),60,HorizontalAlignment.Right);
			gridAccount.Columns.Add(col);
			gridAccount.Rows.Clear();
			ODGridRow row;
			//ODGridCell cell;
			DataTable table=DataSetMain.Tables["account"];
			for(int i=0;i<table.Rows.Count;i++) {
				row = new ODGridRow();
				row.Cells.Add(table.Rows[i]["date"].ToString());
				row.Cells.Add(table.Rows[i]["patient"].ToString());
				row.Cells.Add(table.Rows[i]["prov"].ToString());
				row.Cells.Add(table.Rows[i]["ProcCode"].ToString());
				row.Cells.Add(table.Rows[i]["tth"].ToString());
				row.Cells.Add(table.Rows[i]["description"].ToString());
				row.Cells.Add(table.Rows[i]["charges"].ToString());
				row.Cells.Add(table.Rows[i]["credits"].ToString());
				row.Cells.Add(table.Rows[i]["balance"].ToString());
				row.ColorText=Color.FromArgb(PIn.PInt(table.Rows[i]["colorText"].ToString()));
				if(i==table.Rows.Count-1//last row
					|| (DateTime)table.Rows[i]["DateTime"]!=(DateTime)table.Rows[i+1]["DateTime"])
				{
					row.ColorLborder=Color.Black;
				}
				gridAccount.Rows.Add(row);
			}
			/*
					case AcctModType.PayPlan:
						row.ColorText=DefB.Long[(int)DefCat.AccountColors][6].ItemColor;
						break;
			 */
			gridAccount.EndUpdate();
			gridAccount.ScrollToEnd();
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
				string[] procsOnClaim=table.Rows[e.Row]["procsOnClaim"].ToString().Split(',');
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
			/*
			switch (AcctLineList[e.Row].Type){
				default://procedure
					break;
				case AcctModType.Claim:
					Claim ClaimCur=arrayClaim[AcctLineList[e.Row].Index];
					for(int i=0;i<AcctLineList.Count;i++){//loop through all rows
						if(AcctLineList[i].Type!=AcctModType.Proc)
							//if not a procedure, then skip
							continue;
						for(int j=0;j<ClaimProcList.Length;j++){//loop through all claimprocs
							//if there is a claim proc for this procedure that matches
							if(arrayProc[AcctLineList[i].Index].ProcNum==ClaimProcList[j].ProcNum
								&& ClaimProcList[j].ClaimNum==ClaimCur.ClaimNum)
							{
								gridAccount.SetSelected(i,true);
							}
						}
					}
					break;
				case AcctModType.Adj:
					//Adjustments.Cur=arrayAdj[((AcctLine)AcctLineAL[e.Row]).Index];
					break;
				case AcctModType.Pay:
					PaySplit[] PaySplitList=PaySplits.Refresh(PatCur.PatNum);
					for(int i=0;i<AcctLineList.Count;i++){//loop through all rows
						if(AcctLineList[i].Type!=AcctModType.Proc) {
							continue;//if not a procedure, then skip
						}
						for(int j=0;j<PaySplitList.Length;j++) {//loop through all paysplits
							//if there is a paysplit for this procedure that matches
							if(arrayProc[AcctLineList[i].Index].ProcNum==PaySplitList[j].ProcNum
								&& PaySplitList[j].PayNum==arrayPay[AcctLineList[e.Row].Index].PayNum) {
								gridAccount.SetSelected(i,true);
							}
						}
					}
					break;
				case AcctModType.Comm:
					//Commlogs.Cur=arrayComm[((AcctLine)AcctLineAL[e.Row]).Index];
					break;
				case AcctModType.PayPlan:
					PayPlan payPlanCur=arrayPayPlan[AcctLineList[e.Row].Index];
					//ArrayList payPlanRows=new ArrayList();
					for(int i=0;i<AcctLineList.Count;i++){
						if(AcctLineList[i].Type==AcctModType.Pay
							&& arrayPay[AcctLineList[i].Index].PayPlanNum==payPlanCur.PayPlanNum)
						{
							gridAccount.SetSelected(i,true);
						}
					}
					break;	
			}//end switch*/
			
		}

		private void gridAccount_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(ViewingInRecall) return;
			DataTable table=DataSetMain.Tables["Account"];
			if(table.Rows[e.Row]["ProcNum"].ToString()!="0"){
				Procedure proc=Procedures.GetOneProc(PIn.PInt(table.Rows[e.Row]["ProcNum"].ToString()),true);
				FormProcEdit FormPE=new FormProcEdit(proc,PatCur,FamCur,InsPlanList);
				FormPE.ShowDialog();
			}
			else if(table.Rows[e.Row]["AdjNum"].ToString()!="0"){
				Adjustment adj=Adjustments.GetOne(PIn.PInt(table.Rows[e.Row]["AdjNum"].ToString()));
				FormAdjust FormAdj=new FormAdjust(PatCur,adj);
				FormAdj.ShowDialog();
			}
			else if(table.Rows[e.Row]["PayNum"].ToString()!="0"){
				Payment PaymentCur=Payments.GetPayment(PIn.PInt(table.Rows[e.Row]["PayNum"].ToString()));
				if(PaymentCur.PayType==0){//provider income transfer
					FormProviderIncTrans FormPIT=new FormProviderIncTrans();
					FormPIT.PatNum=PatCur.PatNum;
					FormPIT.PaymentCur=PaymentCur;
					FormPIT.IsNew=false;
					FormPIT.ShowDialog();
				}
				else{
					FormPayment2=new FormPayment(PatCur,FamCur,PaymentCur);
					FormPayment2.IsNew=false;
					FormPayment2.ShowDialog();
				}
			}
			else if(table.Rows[e.Row]["ClaimNum"].ToString()!="0"){//claims and claimpayments
				Claim claim=Claims.GetClaim(PIn.PInt(table.Rows[e.Row]["ClaimNum"].ToString()));
				FormClaimEdit FormClaimEdit2=new FormClaimEdit(claim,PatCur,FamCur);
				FormClaimEdit2.IsNew=false;
				FormClaimEdit2.ShowDialog();
			}
			else if(table.Rows[e.Row]["CommlogNum"].ToString()!="0"){
				Commlog CommlogCur=Commlogs.GetOne(PIn.PInt(table.Rows[e.Row]["CommlogNum"].ToString()));
				FormCommItem2=new FormCommItem(CommlogCur);
				FormCommItem2.IsNew=false;
				FormCommItem2.ShowDialog();
			}
			/*			
				case AcctModType.PayPlan:
					FormPayPlan2=new FormPayPlan(PatCur,arrayPayPlan[AcctLineList[e.Row].Index]);
					FormPayPlan2.ShowDialog();
					if(FormPayPlan2.GotoPatNum!=0){
						PatCur.PatNum=FormPayPlan2.GotoPatNum;//switches to other patient.
					}
					break;	
			}//end switch*/
			//Shared.ComputeBalances();//use whenever a change would affect the total
			bool isSelectingFamily=gridAcctPat.GetSelectedIndex()==this.DataSetMain.Tables["patient"].Rows.Count-1;
			ModuleSelected(PatCur.PatNum,isSelectingFamily);
		}

		/*private void tbAccount_CellDoubleClicked(object sender, CellEventArgs e){
			if(ViewingInRecall) return;
			switch (((AcctLine)AcctLineAL[e.Row]).Type){
				default:
					Procedure ProcCur=arrayProc[((AcctLine)AcctLineAL[e.Row]).Index];
					FormProcEdit FormPE=new FormProcEdit(ProcCur,PatCur,FamCur,InsPlanList);
					FormPE.ShowDialog();
					break;
				case AcctType.Claim:
					Claims.Cur=arrayClaim[((AcctLine)AcctLineAL[e.Row]).Index];
					FormClaimEdit FormClaimEdit2=new FormClaimEdit(PatCur,FamCur);
					FormClaimEdit2.IsNew=false;
					FormClaimEdit2.ShowDialog();
					break;
				case AcctType.Adj:
					Adjustment AdjustmentCur=arrayAdj[((AcctLine)AcctLineAL[e.Row]).Index];
					FormAdjust FormAdj=new FormAdjust(PatCur,AdjustmentCur);
					FormAdj.ShowDialog();
					break;
				case AcctType.Pay:
					Payment PaymentCur=Payments.GetPayment(arrayPay[((AcctLine)AcctLineAL[e.Row]).Index].PayNum);
					FormPayment2=new FormPayment(PatCur,FamCur,PaymentCur);
					FormPayment2.IsNew=false;
					FormPayment2.ShowDialog();
					break;
				case AcctType.Comm:
					Commlogs.Cur=arrayComm[((AcctLine)AcctLineAL[e.Row]).Index];
					FormCommItem2=new FormCommItem();
					FormCommItem2.IsNew=false;
					FormCommItem2.ShowDialog();
					break;				
				case AcctType.PayPlan:
					PayPlans.Cur=arrayPayPlan[((AcctLine)AcctLineAL[e.Row]).Index];
					FormPayPlan2=new FormPayPlan(PatCur);
					FormPayPlan2.IsNew=false;
					FormPayPlan2.ShowDialog();
					if(FormPayPlan2.GotoPatNum!=0){
						//Patient PatCur=PatCur;
						
						PatCur.PatNum=FormPayPlan2.GotoPatNum;//switches to other patient.
						//Patients.Cur=PatCur;
					}
					break;	
			}//end switch
			//Shared.ComputeBalances();//use whenever a change would affect the total
			ModuleSelected(PatCur.PatNum);
		}*/

		private void gridAcctPat_CellClick(object sender,ODGridClickEventArgs e) {
			if(ViewingInRecall){
				return;
			}
			if(e.Row==FamCur.List.Length){//last row
				OnPatientSelected(FamCur.List[0].PatNum,FamCur.List[0].GetNameLF(),FamCur.List[0].Email!="",
					FamCur.List[0].ChartNumber);
				ModuleSelected(FamCur.List[0].PatNum,true);
			}
			else{
				OnPatientSelected(FamCur.List[e.Row].PatNum,FamCur.List[e.Row].GetNameLF(),FamCur.List[e.Row].Email!="",
					FamCur.List[e.Row].ChartNumber);
				ModuleSelected(FamCur.List[e.Row].PatNum);
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
			else if(e.Button.Tag.GetType()==typeof(int)){
				Programs.Execute((int)e.Button.Tag,PatCur);
			}
		}

		///<summary></summary>
		private void OnPatientSelected(int patNum,string patName,bool hasEmail,string chartNumber){
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
			Payments.Insert(PaymentCur);
			FormPayment FormPayment2=new FormPayment(PatCur,FamCur,PaymentCur);
			FormPayment2.IsNew=true;
			FormPayment2.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemProvIncTrans_Click(object sender,EventArgs e) {
			Payment PaymentCur=new Payment();
			PaymentCur.PayDate=DateTime.Today;
			PaymentCur.PatNum=PatCur.PatNum;
			//PaymentCur.ClinicNum=PatCur.ClinicNum;
			Payments.Insert(PaymentCur);
			FormProviderIncTrans FormP=new FormProviderIncTrans();
			FormP.IsNew=true;
			FormP.PaymentCur=PaymentCur;
			FormP.PatNum=PatCur.PatNum;
			FormP.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void OnAdj_Click() {
			Adjustment AdjustmentCur=new Adjustment();
			AdjustmentCur.DateEntry=DateTime.Today;//cannot be changed. Handled automatically
			AdjustmentCur.AdjDate=DateTime.Today;
			AdjustmentCur.ProcDate=DateTime.Today;
			AdjustmentCur.ProvNum=PatCur.PriProv;
			AdjustmentCur.PatNum=PatCur.PatNum;
			FormAdjust FormAdjust2=new FormAdjust(PatCur,AdjustmentCur);
			FormAdjust2.IsNew=true;
			FormAdjust2.ShowDialog();
			//Shared.ComputeBalances();
			ModuleSelected(PatCur.PatNum);
		}

		private void OnIns_Click() {
			/*
			if(PatPlanList.Length==0){
				MsgBox.Show(this,"Patient does not have insurance.");
				return;
			}
			int countSelected=0;
			bool countIsOverMax=false;
			if(gridAccount.SelectedIndices.Length==0){
				//autoselect procedures
				for(int i=0;i<AcctLineList.Count;i++){//loop through every line showing on screen
					if(AcctLineList[i].Type!=AcctModType.Proc){
						continue;//ignore non-procedures
					}
					if(arrayProc[AcctLineList[i].Index].ProcFee==0){
						continue;//ignore zero fee procedures, but user can explicitly select them
					}
					if(Procedures.NeedsSent(arrayProc[AcctLineList[i].Index],ClaimProcList,PatPlans.GetPlanNum(PatPlanList,1))){
						if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA" && countSelected==7){//en-CA or fr-CA
							countIsOverMax=true;
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
				if(AcctLineList[gridAccount.SelectedIndices[i]].Type!=AcctModType.Proc){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MsgBox.Show(this,"You can only select procedures.");
				return;
			}
			if(countIsOverMax){
				MsgBox.Show(this,"Only the first 7 procedures will be selected.  You will need to also create a second claim.");
			}
			Claim ClaimCur=CreateClaim("P");
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			//ClaimProc[] ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,ClaimCur.ClaimNum);
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTime.Today;
			bool isFamMax=Benefits.GetIsFamMax(BenefitList,ClaimCur.PlanNum);
			bool isFamDed=Benefits.GetIsFamDed(BenefitList,ClaimCur.PlanNum);
			ClaimProc[] claimProcsFam=null;
			if(isFamMax || isFamDed){
				claimProcsFam=ClaimProcs.RefreshFam(ClaimCur.PlanNum);
				Claims.CalculateAndUpdate(claimProcsFam,AccProcList,InsPlanList,ClaimCur,PatPlanList,BenefitList);
			}
			else{
				Claims.CalculateAndUpdate(ClaimProcList,AccProcList,InsPlanList,ClaimCur,PatPlanList,BenefitList);
			}
			//Claims.Cur=ClaimCur;
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK){
				ModuleSelected(PatCur.PatNum);
				return;//will have already been deleted
			}
			//Claim priClaim=Claims.Cur;//for use a few lines down to display dialog
			if(PatPlans.GetPlanNum(PatPlanList,2)>0){
				InsPlan plan=InsPlans.GetPlan(PatPlans.GetPlanNum(PatPlanList,2),InsPlanList);
				if(!plan.IsMedical){
					ClaimCur=CreateClaim("S");
					if(ClaimCur.ClaimNum==0){
						ModuleSelected(PatCur.PatNum);
						return;
					}
					ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
					ClaimCur.ClaimStatus="H";
					isFamMax=Benefits.GetIsFamMax(BenefitList,ClaimCur.PlanNum);
					isFamDed=Benefits.GetIsFamDed(BenefitList,ClaimCur.PlanNum);
					if(isFamMax || isFamDed) {
						claimProcsFam=ClaimProcs.RefreshFam(ClaimCur.PlanNum);
						Claims.CalculateAndUpdate(claimProcsFam,AccProcList,InsPlanList,ClaimCur,PatPlanList,BenefitList);
					}
					else {
						Claims.CalculateAndUpdate(ClaimProcList,AccProcList,InsPlanList,ClaimCur,PatPlanList,BenefitList);
					}
					//Claims.Cur=ClaimCur;
				}
			}
			ModuleSelected(PatCur.PatNum);*/
		}

		///<summary>The only validation that's been done is just to make sure that only procedures are selected.  All validation on the procedures selected is done here.  Creates and saves claim initially, attaching all selected procedures.  But it does not refresh any data. Does not do a final update of the new claim.  Does not enter fee amounts.  claimType=P,S,Med,or Other</summary>
		private Claim CreateClaim(string claimType){
			/*
			int claimFormNum = 0;
			EtransType eFormat = 0;
			InsPlan PlanCur=new InsPlan();
			Relat relatOther=Relat.Self;
			switch(claimType){
				case "P":
					PlanCur=InsPlans.GetPlan(PatPlans.GetPlanNum(PatPlanList,1),InsPlanList);
					break;
				case "S":
					PlanCur=InsPlans.GetPlan(PatPlans.GetPlanNum(PatPlanList,2),InsPlanList);
					break;
				case "Med":
					//It's already been verified that a med plan exists
					for(int i=0;i<PatPlanList.Length;i++){
						if(InsPlans.GetPlan(PatPlanList[i].PlanNum,InsPlanList).IsMedical){
							PlanCur=InsPlans.GetPlan(PatPlanList[i].PlanNum,InsPlanList);
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
					relatOther=FormCC.PatRelat;
					claimFormNum=FormCC.ClaimFormNum;
					eFormat=FormCC.EFormat;
					break;
			}
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(Procedures.NoBillIns(arrayProc[AcctLineList[gridAccount.SelectedIndices[i]].Index],ClaimProcList,PlanCur.PlanNum)){
					MsgBox.Show(this,"Not allowed to send procedures to insurance that are marked 'Do not bill to ins'.");
					return new Claim();
				}
			}
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(Procedures.IsAlreadyAttachedToClaim(arrayProc[AcctLineList[gridAccount.SelectedIndices[i]].Index],ClaimProcList,PlanCur.PlanNum)){
					MsgBox.Show(this,"Not allowed to send a procedure to the same insurance company twice.");
					return new Claim();
				}
			}
			int clinic=arrayProc[AcctLineList[gridAccount.SelectedIndices[0]].Index].ClinicNum;
			for(int i=1;i<gridAccount.SelectedIndices.Length;i++){//skips 0
				if(clinic!=arrayProc[AcctLineList[gridAccount.SelectedIndices[i]].Index].ClinicNum){
					MsgBox.Show(this,"All procedures do not have the same clinic.");
					return new Claim();
				}
			}
			ClaimProc[] claimProcs=new ClaimProc[gridAccount.SelectedIndices.Length];
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){//loop through selected procs
				//and try to find an estimate that can be used
				claimProcs[i]=Procedures.GetClaimProcEstimate(arrayProc[AcctLineList[gridAccount.SelectedIndices[i]].Index],ClaimProcList,PlanCur);
			}
			for(int i=0;i<claimProcs.Length;i++){//loop through each claimProc
				//and create any missing estimates. This handles claims to 3rd and 4th ins co's.
				if(claimProcs[i]==null){
					claimProcs[i]=new ClaimProc();
					ClaimProcs.CreateEst(claimProcs[i],arrayProc[AcctLineList[gridAccount.SelectedIndices[i]].Index],PlanCur);
				}
			}
			//now, all claimProcs have a valid value
			//for any CapComplete, need to make a copy so that original doesn't get attached.
			for(int i=0;i<claimProcs.Length;i++){
				if(claimProcs[i].Status==ClaimProcStatus.CapComplete){
					claimProcs[i]=claimProcs[i].Copy();
					claimProcs[i].WriteOff=0;
					claimProcs[i].CopayAmt=-1;
					claimProcs[i].CopayOverride=-1;
					//status will get changed down below
					ClaimProcs.Insert(claimProcs[i]);//this makes a duplicate in db with different claimProcNum
				}
			}
			Claim ClaimCur=new Claim();
			Claims.Insert(ClaimCur);//to retreive a key for new Claim.ClaimNum
			//Claim ClaimCur=Claims.Cur;
			ClaimCur.PatNum=PatCur.PatNum;
			ClaimCur.DateService=claimProcs[claimProcs.Length-1].ProcDate;
			ClaimCur.ClinicNum=clinic;
			//datesent
			ClaimCur.ClaimStatus="U";
			//datereceived
			switch(claimType){
				case "P":
					ClaimCur.PlanNum=PatPlans.GetPlanNum(PatPlanList,1);
					ClaimCur.PatRelat=PatPlans.GetRelat(PatPlanList,1);
					ClaimCur.ClaimType="P";
					ClaimCur.PlanNum2=PatPlans.GetPlanNum(PatPlanList,2);//might be 0 if no sec ins
					ClaimCur.PatRelat2=PatPlans.GetRelat(PatPlanList,2);
					break;
				case "S":
					ClaimCur.PlanNum=PatPlans.GetPlanNum(PatPlanList,2);
					ClaimCur.PatRelat=PatPlans.GetRelat(PatPlanList,2);
					ClaimCur.ClaimType="S";
					ClaimCur.PlanNum2=PatPlans.GetPlanNum(PatPlanList,1);
					ClaimCur.PatRelat2=PatPlans.GetRelat(PatPlanList,1);
					break;
				case "Med":
					ClaimCur.PlanNum=PlanCur.PlanNum;
					ClaimCur.PatRelat=Relat.Self;
					ClaimCur.ClaimType="Other";
					break;
				case "Other":
					ClaimCur.PlanNum=PlanCur.PlanNum;
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
			ClaimCur.ProvTreat=arrayProc[AcctLineList[gridAccount.SelectedIndices[0]].Index].ProvNum;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(!Providers.GetIsSec//if not a hygienist
					(arrayProc[AcctLineList[gridAccount.SelectedIndices[i]].Index].ProvNum))
				{
					ClaimCur.ProvTreat
						=arrayProc[AcctLineList[gridAccount.SelectedIndices[i]].Index].ProvNum;
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
			ClaimCur.ProvBill=Providers.GetBillingProvNum(ClaimCur.ProvTreat);//OK if zero, because it will get fixed in claim
			ClaimCur.EmployRelated=YN.No;
			//attach procedures
			Procedure ProcCur;
			//for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
			for(int i=0;i<claimProcs.Length;i++){
				ProcCur=arrayProc[AcctLineList[gridAccount.SelectedIndices[i]].Index];
				//ClaimProc ClaimProcCur=new ClaimProc();
				//ClaimProcCur.ProcNum=ProcCur.ProcNum;
				claimProcs[i].ClaimNum=ClaimCur.ClaimNum;
				//ClaimProcCur.PatNum=Patients.Cur.PatNum;
				//ClaimProcCur.ProvNum=ProcCur.ProvNum;
				//ClaimProcs.Cur.FeeBilled=;//handle in call to FormClaimEdit.CalculateEstimates
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
				//writeoff
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
				claimProcs[i].LineNumber=i+1;
				ClaimProcs.Update(claimProcs[i]);
			}//for claimProc
			return ClaimCur;*/
			return null;
		}

		private void menuInsPri_Click(object sender, System.EventArgs e) {
			/*
			if(PatPlanList.Length==0){
				MessageBox.Show(Lan.g(this,"Patient does not have insurance."));
				return;
			}
			if(gridAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			bool allAreProcedures=true;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(AcctLineList[gridAccount.SelectedIndices[i]].Type!=AcctModType.Proc){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			Claim ClaimCur=CreateClaim("P");
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			//ClaimProc[] ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,ClaimCur.ClaimNum);
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTime.Today;
			//Claims.Cur=ClaimCur;
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			bool isFamMax=Benefits.GetIsFamMax(BenefitList,ClaimCur.PlanNum);
			bool isFamDed=Benefits.GetIsFamDed(BenefitList,ClaimCur.PlanNum);
			if(isFamMax || isFamDed) {
				ClaimProc[] claimProcsFam=ClaimProcs.RefreshFam(ClaimCur.PlanNum);
				Claims.CalculateAndUpdate(claimProcsFam,AccProcList,InsPlanList,ClaimCur,PatPlanList,BenefitList);
			}
			else {
				Claims.CalculateAndUpdate(ClaimProcList,AccProcList,InsPlanList,ClaimCur,PatPlanList,BenefitList);
			}
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);*/
		}

		private void menuInsSec_Click(object sender, System.EventArgs e) {
			/*
			if(PatPlanList.Length<2){
				MessageBox.Show(Lan.g(this,"Patient does not have secondary insurance."));
				return;
			}
			if(gridAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			bool allAreProcedures=true;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(AcctLineList[gridAccount.SelectedIndices[i]].Type!=AcctModType.Proc){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			Claim ClaimCur=CreateClaim("S");
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			//ClaimProc[] ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,ClaimCur.ClaimNum);
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTime.Today;
			bool isFamMax=Benefits.GetIsFamMax(BenefitList,ClaimCur.PlanNum);
			bool isFamDed=Benefits.GetIsFamDed(BenefitList,ClaimCur.PlanNum);
			if(isFamMax || isFamDed) {
				ClaimProc[] claimProcsFam=ClaimProcs.RefreshFam(ClaimCur.PlanNum);
				Claims.CalculateAndUpdate(claimProcsFam,AccProcList,InsPlanList,ClaimCur,PatPlanList,BenefitList);
			}
			else {
				Claims.CalculateAndUpdate(ClaimProcList,AccProcList,InsPlanList,ClaimCur,PatPlanList,BenefitList);
			}
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);*/
		}

		private void menuInsMedical_Click(object sender, System.EventArgs e) {
			/*
			int medPlanNum=0;
			for(int i=0;i<PatPlanList.Length;i++){
				if(InsPlans.GetPlan(PatPlanList[i].PlanNum,InsPlanList).IsMedical){
					medPlanNum=PatPlanList[i].PlanNum;
					break;
				}
			}
			if(medPlanNum==0){
				MsgBox.Show(this,"Patient does not have medical insurance.");
				return;
			}
			if(gridAccount.SelectedIndices.Length==0){
				//autoselect procedures
				for(int i=0;i<AcctLineList.Count;i++){//loop through every line showing on screen
					if(AcctLineList[i].Type!=AcctModType.Proc){
						continue;//ignore non-procedures
					}
					if(arrayProc[AcctLineList[i].Index].ProcFee==0){
						continue;//ignore zero fee procedures, but user can explicitly select them
					}
					if(arrayProc[AcctLineList[i].Index].MedicalCode==""){
						continue;//ignore non-medical procedures
					}
					if(Procedures.NeedsSent(arrayProc[AcctLineList[i].Index],ClaimProcList,medPlanNum)){
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
				if(AcctLineList[gridAccount.SelectedIndices[i]].Type!=AcctModType.Proc){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MsgBox.Show(this,"You can only select procedures.");
				return;
			}
			Claim ClaimCur=CreateClaim("Med");
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTime.Today;
			bool isFamMax=Benefits.GetIsFamMax(BenefitList,ClaimCur.PlanNum);
			bool isFamDed=Benefits.GetIsFamDed(BenefitList,ClaimCur.PlanNum);
			if(isFamMax || isFamDed) {
				ClaimProc[] claimProcsFam=ClaimProcs.RefreshFam(ClaimCur.PlanNum);
				Claims.CalculateAndUpdate(claimProcsFam,AccProcList,InsPlanList,ClaimCur,PatPlanList,BenefitList);
			}
			else {
				Claims.CalculateAndUpdate(ClaimProcList,AccProcList,InsPlanList,ClaimCur,PatPlanList,BenefitList);
			}
			//Claims.Cur=ClaimCur;
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);*/
		}

		private void menuInsOther_Click(object sender, System.EventArgs e) {
			/*
			if(gridAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			bool allAreProcedures=true;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(AcctLineList[gridAccount.SelectedIndices[i]].Type!=AcctModType.Proc){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			Claim ClaimCur=CreateClaim("Other");
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			//ClaimProc[] ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,ClaimCur.ClaimNum);
			bool isFamMax=Benefits.GetIsFamMax(BenefitList,ClaimCur.PlanNum);
			bool isFamDed=Benefits.GetIsFamDed(BenefitList,ClaimCur.PlanNum);
			if(isFamMax || isFamDed) {
				ClaimProc[] claimProcsFam=ClaimProcs.RefreshFam(ClaimCur.PlanNum);
				Claims.CalculateAndUpdate(claimProcsFam,AccProcList,InsPlanList,ClaimCur,PatPlanList,BenefitList);
			}
			else {
				Claims.CalculateAndUpdate(ClaimProcList,AccProcList,InsPlanList,ClaimCur,PatPlanList,BenefitList);
			}
			//Claims.Cur=ClaimCur;
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);*/
		}

		private void OnPayPlan_Click() {
			PayPlan payPlan=new PayPlan();
			payPlan.PatNum=PatCur.PatNum;
			payPlan.Guarantor=PatCur.Guarantor;
			payPlan.PayPlanDate=DateTime.Today;
			try{
				PayPlans.InsertOrUpdate(payPlan,true);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return;
			}
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
			if(!ProcedureCodes.HList.ContainsKey("001")) {
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
			if(!ProcedureCodes.HList.ContainsKey("008")) {
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

		private void OnStatement_Click() {
			int[] patNums=new int[FamCur.List.Length];
			for(int i=0;i<FamCur.List.Length;i++){
				patNums[i]=FamCur.List[i].PatNum;
			}
			DateTime fromDate;
			if(checkShowAll.Checked){
				fromDate=DateTime.MinValue;
			}
			else{
				fromDate=DateTime.Today.AddDays(-45);
			}
			PrintStatement(patNums,fromDate,DateTime.MaxValue,true,false,false,false,"",false,"");
			ModuleSelected(PatCur.PatNum);
		}
		
		private void menuItemStatementWalkout_Click(object sender, System.EventArgs e) {
			PrintStatement(new int[] {PatCur.PatNum},DateTime.Today,DateTime.Today,false,false,true,true,"",false,"");
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemStatementEmail_Click(object sender,EventArgs e) {
			int[] patNums=new int[FamCur.List.Length];
			for(int i=0;i<FamCur.List.Length;i++) {
				patNums[i]=FamCur.List[i].PatNum;
			}
			DateTime fromDate;
			if(checkShowAll.Checked) {
				fromDate=DateTime.MinValue;
			}
			else {
				fromDate=DateTime.Today.AddDays(-45);
			}
			string attachPath=FormEmailMessageEdit.GetAttachPath();
			Random rnd=new Random();
			string fileName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()+".pdf";
			string filePathAndName=ODFileUtils.CombinePaths(attachPath,fileName);
			PrintStatement(patNums,fromDate,DateTime.MaxValue,true,false,false,false,"",false,filePathAndName);
			//Process.Start(filePathAndName);
			EmailMessage message=new EmailMessage();
			message.PatNum=PatCur.PatNum;
			message.ToAddress=PatCur.Email;
			message.FromAddress=PrefB.GetString("EmailSenderAddress");
			message.Subject=Lan.g(this,"Statement");
			EmailAttach attach=new EmailAttach();
			attach.DisplayedFileName="Statement.pdf";
			attach.ActualFileName=fileName;
			message.Attachments.Add(attach);
			FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
			FormE.IsNew=true;
			FormE.ShowDialog();
			//if(FormE.DialogResult==DialogResult.OK) {
			//	RefreshCurrentModule();
			//}
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemStatementMore_Click(object sender, System.EventArgs e) {
			FormStatementOptions FormSO=new FormStatementOptions(PatCur,FamCur);
			if(checkShowAll.Checked){
				FormSO.FromDate=DateTime.MinValue;
			}
			else{
				FormSO.FromDate=DateTime.Today.AddDays(-45);
			}
			FormSO.ToDate=DateTime.MaxValue;
			FormSO.ShowDialog();
			if(FormSO.DialogResult!=DialogResult.OK){
				return;
			}
			//FillMain(FormSO.FromDate,FormSO.ToDate,FormSO.IncludeClaims,FormSO.SubtotalsOnly);
			//if Email button pushed then make statement to email
			if(FormSO.Email) {
				int[] patNums=new int[FamCur.List.Length];
				for(int i=0;i<FamCur.List.Length;i++) {
					patNums[i]=FamCur.List[i].PatNum;
				}
				DateTime fromDate;
				if(checkShowAll.Checked) {
					fromDate=DateTime.MinValue;
				}
				else {
					fromDate=DateTime.Today.AddDays(-45);
				}
				string attachPath=FormEmailMessageEdit.GetAttachPath();
				Random rnd=new Random();
				string fileName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()+".pdf";
				string filePathAndName=ODFileUtils.CombinePaths(attachPath,fileName);
				//PrintStatement(patNums,fromDate,DateTime.MaxValue,true,false,false,false,"",false,PrefB.GetBool("PrintSimpleStatements"),
				//	filePathAndName);
				PrintStatement(FormSO.PatNums,FormSO.FromDate,FormSO.ToDate,FormSO.IncludeClaims,
				FormSO.SubtotalsOnly,FormSO.HidePayment,FormSO.NextAppt,FormSO.Note,FormSO.IsBill,
				filePathAndName);

				//Process.Start(filePathAndName);
				EmailMessage message=new EmailMessage();
				message.PatNum=PatCur.PatNum;
				message.ToAddress=PatCur.Email;
				message.FromAddress=PrefB.GetString("EmailSenderAddress");
				message.Subject=Lan.g(this,"Statement");
				EmailAttach attach=new EmailAttach();
				attach.DisplayedFileName="Statement.pdf";
				attach.ActualFileName=fileName;
				message.Attachments.Add(attach);
				FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
				FormE.IsNew=true;
				FormE.ShowDialog();

			}
			else {
				PrintStatement(FormSO.PatNums,FormSO.FromDate,FormSO.ToDate,FormSO.IncludeClaims
					,FormSO.SubtotalsOnly,FormSO.HidePayment,FormSO.NextAppt,FormSO.Note,FormSO.IsBill,"");
				ModuleSelected(PatCur.PatNum);
			}
		}

		private void textUrgFinNote_TextChanged(object sender, System.EventArgs e) {
			UrgFinNoteChanged=true;
		}

		private void textFinNotes_TextChanged(object sender, System.EventArgs e) {
			FinNoteChanged=true;
		}

		private void textCC_TextChanged(object sender,EventArgs e) {
			CCChanged=true;
			if(Regex.IsMatch(textCC.Text,@"^\d{4}$")
				|| Regex.IsMatch(textCC.Text,@"^\d{4}-\d{4}$")
				|| Regex.IsMatch(textCC.Text,@"^\d{4}-\d{4}-\d{4}$")) 
			{
				textCC.Text=textCC.Text+"-";
				textCC.Select(textCC.Text.Length,0);
			}
		}

		private void textCCexp_TextChanged(object sender,EventArgs e) {
			CCChanged=true;
		}

		private void textUrgFinNote_Leave(object sender, System.EventArgs e) {
			//need to skip this if selecting another module. Handled in ModuleUnselected due to click event
			if(FamCur==null)
				return;
			if(UrgFinNoteChanged){
				Patient PatOld=FamCur.List[0].Copy();
				FamCur.List[0].FamFinUrgNote=textUrgFinNote.Text;
				Patients.Update(FamCur.List[0],PatOld);
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

		private void textCC_Leave(object sender,EventArgs e) {
			if(FamCur==null)
				return;
			if(CCChanged) {
				CCSave();
				CCChanged=false;
				ModuleSelected(PatCur.PatNum);
			}
		}

		private void textCCexp_Leave(object sender,EventArgs e) {
			if(FamCur==null)
				return;
			if(CCChanged){
				CCSave();
				CCChanged=false;
				ModuleSelected(PatCur.PatNum);
			}
		}

		private void CCSave(){
			string cc=textCC.Text;
			if(Regex.IsMatch(cc,@"^\d{4}-\d{4}-\d{4}-\d{4}$")){
				PatientNoteCur.CCNumber=cc.Substring(0,4)+cc.Substring(5,4)+cc.Substring(10,4)+cc.Substring(15,4);
			}
			else{
				PatientNoteCur.CCNumber=cc;
			}
			string exp=textCCexp.Text;
			if(Regex.IsMatch(exp,@"^\d\d[/\- ]\d\d$")){//08/07 or 08-07 or 08 07
				PatientNoteCur.CCExpiration=new DateTime(Convert.ToInt32("20"+exp.Substring(3,2)),Convert.ToInt32(exp.Substring(0,2)),1);
			}
			else if(Regex.IsMatch(exp,@"^\d{4}$")){//0807
				PatientNoteCur.CCExpiration=new DateTime(Convert.ToInt32("20"+exp.Substring(2,2)),Convert.ToInt32(exp.Substring(0,2)),1);
			} 
			else if(exp=="") {
				PatientNoteCur.CCExpiration=new DateTime();//Allow the experation date to be deleted.
			} 
			else {
				MsgBox.Show(this,"Expiration format invalid.");
			}
			PatientNotes.Update(PatientNoteCur,PatCur.Guarantor);
		}

		private void checkShowAll_Click(object sender, System.EventArgs e) {
			//RefreshModuleScreen();
			ModuleSelected(PatCur.PatNum);
		}

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

		/// <summary>Prints a single statement.  Or, if pdfFullFileName is specified (including full path), then it saves as pdf to that file.</summary>
		private void PrintStatement(int[] famPatNums,DateTime fromDate,DateTime toDate,bool includeClaims, bool subtotalsOnly,
			bool hidePayment,bool nextAppt,string note, bool isBill,string pdfFullFileName)
		{
			FormRpStatement FormST=new FormRpStatement();
			int[][] patNums=new int[1][];
			patNums[0]=new int[famPatNums.Length];
			for(int i=0;i<famPatNums.Length;i++){
				patNums[0][i]=famPatNums[i];
			}
			PrintingStatement = true; 
			FormST.PrintStatements(patNums,fromDate,toDate,includeClaims,subtotalsOnly,hidePayment,nextAppt,
				new string[] {note}, isBill,pdfFullFileName);
			if(pdfFullFileName==""){
				#if DEBUG
					FormST.ShowDialog();
				#endif
			}
			PrintingStatement = false; 
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
			/*FormCommunications FormC=new FormCommunications();
			FormC.PatCur=PatCur.Copy();
			FormC.ViewingInRecall=ViewingInRecall;
			FormC.ShowDialog();
			if(FormC.DialogResult==DialogResult.OK){
				ModuleSelected(PatCur.PatNum);
			}*/
		}

		/*private void butLetterSimple_Click(object sender, System.EventArgs e) {
			FormLetters FormL=new FormLetters(PatCur);
			FormL.ShowDialog();
			CommlogList=Commlogs.Refresh(PatCur.PatNum);
			ModuleSelected(PatCur.PatNum);
		}

		private void butLetterMerge_Click(object sender, System.EventArgs e) {
			FormLetterMerges FormL=new FormLetterMerges(PatCur);
			FormL.ShowDialog();
			CommlogList=Commlogs.Refresh(PatCur.PatNum);
			ModuleSelected(PatCur.PatNum);
		}

		private void butQuest_Click(object sender,EventArgs e) {
			FormPat form=new FormPat();
			form.PatNum=PatCur.PatNum;
			form.FormDateTime=DateTime.Now;
			FormFormPatEdit FormP=new FormFormPatEdit();
			FormP.FormPatCur=form;
			FormP.IsNew=true;
			FormP.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void butLabel_Click(object sender, System.EventArgs e) {
			LabelSingle label=new LabelSingle();
			label.PrintPat(PatCur);
		}

		private void butEmail_Click(object sender, System.EventArgs e) {
			EmailMessage message=new EmailMessage();
			message.PatNum=PatCur.PatNum;
			message.ToAddress=PatCur.Email;
			message.FromAddress=PrefB.GetString("EmailSenderAddress");
			FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
			FormE.IsNew=true;
			FormE.ShowDialog();
			if(FormE.DialogResult==DialogResult.OK) {
				ModuleSelected(PatCur.PatNum);
			}
			//CommlogList=Commlogs.Refresh(PatCur.PatNum);
			//FillComm();
		}*/

		private void butTask_Click(object sender, System.EventArgs e) {
			//FormTaskListSelect FormT=new FormTaskListSelect(TaskObjectType.Patient,PatCur.PatNum);
			//FormT.ShowDialog();
		}

		private void butTrojan_Click(object sender,EventArgs e) {
			FormTrojanCollect FormT=new FormTrojanCollect();
			FormT.PatNum=PatCur.PatNum;
			FormT.ShowDialog();
		}

		/*private void tbComm_CellDoubleClicked(object sender, OpenDental.CellEventArgs e) {
			Commlog CommlogCur=CommlogList[(int)CommIndices[e.Row]].Copy();
			FormCommItem FormCI=new FormCommItem(CommlogCur);
			FormCI.ShowDialog();
			CommlogList=Commlogs.Refresh(PatCur.PatNum);
			FillComm();
		}*/

		private void gridComm_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			//string commlognum=DataSetMain.Tables["Commlog"].Rows[e.Row]["CommlogNum"].ToString();
			if(DataSetMain.Tables["Commlog"].Rows[e.Row]["CommlogNum"].ToString()!="0"){
				Commlog CommlogCur=
					Commlogs.GetOne(PIn.PInt(DataSetMain.Tables["Commlog"].Rows[e.Row]["CommlogNum"].ToString()));
				FormCommItem FormCI=new FormCommItem(CommlogCur);
				FormCI.ShowDialog();
				if(FormCI.DialogResult==DialogResult.OK) {
					ModuleSelected(PatCur.PatNum);
				}
			}
			else if(DataSetMain.Tables["Commlog"].Rows[e.Row]["EmailMessageNum"].ToString()!="0") {
				EmailMessage email=
					EmailMessages.GetOne(PIn.PInt(DataSetMain.Tables["Commlog"].Rows[e.Row]["EmailMessageNum"].ToString()));
				FormEmailMessageEdit FormE=new FormEmailMessageEdit(email);
				FormE.ShowDialog();
				if(FormE.DialogResult==DialogResult.OK) {
					ModuleSelected(PatCur.PatNum);
				}
			}
			else if(DataSetMain.Tables["Commlog"].Rows[e.Row]["FormPatNum"].ToString()!="0") {
				FormPat form=FormPats.GetOne(PIn.PInt(DataSetMain.Tables["Commlog"].Rows[e.Row]["FormPatNum"].ToString()));
				FormFormPatEdit FormP=new FormFormPatEdit();
				FormP.FormPatCur=form;
				FormP.ShowDialog();
				if(FormP.DialogResult==DialogResult.OK) {
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

		///<summary>The supplied procedure row must include these columns: ProcDate,ProcStatus,ProcCode,Surf,ToothNum, and ToothRange, all in raw database format.</summary>
		private bool ShouldDisplayProc(DataRow row) {
			switch ((ProcStat)PIn.PInt(row["ProcStatus"].ToString())) {
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

		/*
		private void FillInsInfo() {
			textPriMax.Text = "";
			textPriDed.Text = "";
			textPriDedRem.Text = "";
			textPriUsed.Text = "";
			textPriPend.Text = "";
			textPriRem.Text = "";
			textSecMax.Text = "";
			textSecDed.Text = "";
			textSecDedRem.Text = "";
			textSecUsed.Text = "";
			textSecPend.Text = "";
			textSecRem.Text = "";
			labelFamily.Visible = false;
			if(PatCur == null) {
				return;
			}
			double max = 0;
			double ded = 0;
			double dedUsed = 0;
			double remain = 0;
			double pend = 0;
			double used = 0;
			InsPlan PlanCur;//=new InsPlan();
			if(PatPlanList.Length > 0) {
				PlanCur = InsPlans.GetPlan(PatPlanList[0].PlanNum,InsPlanList);
				bool isFamMax = Benefits.GetIsFamMax(BenefitList,PlanCur.PlanNum);
				bool isFamDed = Benefits.GetIsFamDed(BenefitList,PlanCur.PlanNum);
				if(isFamMax || isFamDed) {
					labelFamily.Visible = true;
				}
				else {
					labelFamily.Visible = false;
				}
				ClaimProc[] claimProcsFam = null;
				if(isFamMax || isFamDed) {
					claimProcsFam = ClaimProcs.RefreshFam(PlanCur.PlanNum);
					pend = InsPlans.GetPending
						(claimProcsFam,DateTime.Today,PlanCur,PatPlanList[0].PatPlanNum,-1,BenefitList);
					used = InsPlans.GetInsUsed
						(claimProcsFam,DateTime.Today,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,-1,InsPlanList,BenefitList);
				}
				else {
					pend = InsPlans.GetPending
						(ClaimProcList,DateTime.Today,PlanCur,PatPlanList[0].PatPlanNum,-1,BenefitList);
					used = InsPlans.GetInsUsed
						(ClaimProcList,DateTime.Today,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,-1,InsPlanList,BenefitList);
				}
				textPriPend.Text = pend.ToString("F");
				textPriUsed.Text = used.ToString("F");

				max = Benefits.GetAnnualMax(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum);
				if(max == -1) {//if annual max is blank
					textPriMax.Text = "";
					textPriRem.Text = "";
				}
				else {
					remain = max - used - pend;
					if(remain < 0) {
						remain = 0;
					}
					textPriMax.Text = max.ToString("F");
					textPriRem.Text = remain.ToString("F");

				}
				//deductible:
				ded = Benefits.GetDeductible(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum);
				if(ded != -1) {
					textPriDed.Text = ded.ToString("F");
					if(isFamMax || isFamDed) {//claimProcsFam was already filled
						dedUsed = InsPlans.GetDedUsed
							(claimProcsFam,DateTime.Today,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,-1,InsPlanList,BenefitList);
					}
					else {
						dedUsed = InsPlans.GetDedUsed
							(ClaimProcList,DateTime.Today,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,-1,InsPlanList,BenefitList);
					}
					textPriDedRem.Text = (ded - dedUsed).ToString("F");
				}
			}
			if(PatPlanList.Length > 1) {
				PlanCur = InsPlans.GetPlan(PatPlanList[1].PlanNum,InsPlanList);
				pend = InsPlans.GetPending
					(ClaimProcList,DateTime.Today,PlanCur,PatPlanList[1].PatPlanNum,-1,BenefitList);
				textSecPend.Text = pend.ToString("F");
				used = InsPlans.GetInsUsed
					(ClaimProcList,DateTime.Today,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,-1,InsPlanList,BenefitList);
				textSecUsed.Text = used.ToString("F");
				max = Benefits.GetAnnualMax(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum);
				if(max == -1) {
					textSecMax.Text = "";
					textSecRem.Text = "";
				}
				else {
					remain = max - used - pend;
					if(remain < 0) {
						remain = 0;
					}
					textSecMax.Text = max.ToString("F");
					textSecRem.Text = remain.ToString("F");
				}
				ded = Benefits.GetDeductible(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum);
				if(ded != -1) {
					textSecDed.Text = ded.ToString("F");
					dedUsed = InsPlans.GetDedUsed
						(ClaimProcList,DateTime.Today,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,-1,InsPlanList,BenefitList);
					textSecDedRem.Text = (ded - dedUsed).ToString("F");
				}
			}
			//**only different lines from tx pl routine fillsummary
			labelPriRem.Text = textPriRem.Text;
			labelPriPend.Text = textPriPend.Text;
		}*/

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
				row.ColorText = Color.FromArgb(PIn.PInt(table.Rows[i]["colorText"].ToString()));
				row.ColorBackG = Color.FromArgb(PIn.PInt(table.Rows[i]["colorBackG"].ToString()));
				row.Tag = table.Rows[i];
				gridProg.Rows.Add(row);
			
			}
			gridProg.EndUpdate();
			gridProg.ScrollToEnd();
		}

		private void gridProg_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			DataRow row=(DataRow)gridProg.Rows[e.Row].Tag;
			if(row["ProcNum"].ToString()!="0"){
				if(checkAudit.Checked){
					MsgBox.Show(this,"Not allowed to edit procedures when in audit mode.");
					return;
				}
				Procedure proc=Procedures.GetOneProc(PIn.PInt(row["ProcNum"].ToString()),true);
				FormProcEdit FormP=new FormProcEdit(proc,PatCur,FamCur,InsPlanList);
				FormP.ShowDialog();
				if(FormP.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			else if(row["CommlogNum"].ToString()!="0"){
				Commlog comm=Commlogs.GetOne(PIn.PInt(row["CommlogNum"].ToString()));
				FormCommItem FormC=new FormCommItem(comm);
				FormC.ShowDialog();
				if(FormC.DialogResult!=DialogResult.OK){
					return;
				}
			}
			else if(row["RxNum"].ToString()!="0") {
				RxPat rx=RxPats.GetRx(PIn.PInt(row["RxNum"].ToString()));
				FormRxEdit FormRxE=new FormRxEdit(PatCur,rx);
				FormRxE.ShowDialog();
				if(FormRxE.DialogResult!=DialogResult.OK){
					return;
				}
			}
			else if(row["LabCaseNum"].ToString()!="0") {
				LabCase lab=LabCases.GetOne(PIn.PInt(row["LabCaseNum"].ToString()));
				FormLabCaseEdit FormL=new FormLabCaseEdit();
				FormL.CaseCur=lab;
				FormL.ShowDialog();
				if(FormL.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			else if(row["AptNum"].ToString()!="0") {
				//Appointment apt=Appointments.GetOneApt(
				FormApptEdit FormA=new FormApptEdit(PIn.PInt(row["AptNum"].ToString()));
				//PinIsVisible=false
				FormA.ShowDialog();
				if(FormA.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			ModuleSelected(PatCur.PatNum);
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

		private void checkShowNotes_Click(object sender,EventArgs e) {
			//checkShowNotes.Tag="JustClicked";		
			//RefreshModuleScreen();
			//checkShowNotes.Tag = "";		
			ModuleSelected(PatCur.PatNum);
		}


		//private void buttonLabelxray_Click(object sender, EventArgs e) {
		//	LabelSingle label = new LabelSingle();
		//	label.PrintPatXRay(PatCur);
		//}

		//private void buttonLabelChart_Click(object sender, EventArgs e) {
		//	LabelSingle label = new LabelSingle();
		//	label.PrintPatLF(PatCur);
		//}

		/*private void butCommLog_Click(object sender, EventArgs e) {
			Commlog CommlogCur = new Commlog();
			CommlogCur.PatNum = PatCur.PatNum;
			CommlogCur.CommDateTime = DateTime.Now;
			if (ViewingInRecall)
				CommlogCur.CommType =Commlogs.GetTypeAuto(CommItemTypeAuto.RECALL);
			else
				CommlogCur.CommType =Commlogs.GetTypeAuto(CommItemTypeAuto.FIN);
			CommlogCur.Mode_=CommItemMode.Phone;
			CommlogCur.SentOrReceived=CommSentOrReceived.Received;
			CommlogCur.UserNum=Security.CurUser.UserNum;
			FormCommItem FormCI = new FormCommItem(CommlogCur);
			FormCI.IsNew = true;
			FormCI.ShowDialog();
			if (FormCI.DialogResult == DialogResult.OK){
				ModuleSelected(PatCur.PatNum);
			}
		}*/
		/*
		private void panelInsInfo_Click(object sender,EventArgs e) {
			panelInsInfo.Visible = false;
		}

		private void labelPriRem_MouseHover(object sender,EventArgs e) {
			panelInsInfo.Visible = true;
		}

		private void labelPriPend_MouseHover(object sender,EventArgs e) {
			panelInsInfo.Visible = true;
		}

		private void labelAfterIns_MouseHover(object sender,EventArgs e) {
			panelInsInfo.Visible = true;
		}

		private void labelAfterIns_MouseLeave(object sender,EventArgs e) {
			panelInsInfo.Visible = false;
		}

		private void labelPriRem_MouseLeave(object sender,EventArgs e) {
			panelInsInfo.Visible = false;
		}

		private void labelPriPend_MouseLeave(object sender,EventArgs e) {
			panelInsInfo.Visible = false;
		}

		private void label19_MouseHover(object sender,EventArgs e) {
			panelInsInfo.Visible = true;
		}

		private void label19_MouseLeave(object sender,EventArgs e) {
			panelInsInfo.Visible = false;
		}

		private void labelIns3_MouseHover(object sender,EventArgs e) {
			panelInsInfo.Visible = true;
		}

		private void labelIns3_MouseLeave(object sender,EventArgs e) {
			panelInsInfo.Visible = false;
		}*/

		

		
		

		

		

		

		





		

		

		

		

		

		

		

		

		

		

		

		

		

		/*private void butPendingClaims_Click(object sender, System.EventArgs e) {
			int OldPatNum=0;
			if(Patients.PatIsLoaded){
				OldPatNum=Patients.Cur.PatNum;
			}
			FormClaimsPending FormPending=new FormClaimsPending();
			FormPending.ShowDialog();
			if(Patients.PatIsLoaded){
				Patients.Cur.PatNum=OldPatNum;
				ModuleSelected();
			}
			else{
				Patients.Cur=new Patient();
			}
		}*/

	}//end class

	///<summary>A single line of data in ContrAccount.</summary>
	public class AcctLine{
		///<summary></summary>
		public AcctModType Type;
		//public bool IsProc;
		///<summary></summary>
		public int Index;
		///<summary></summary>
		public string Date;
		///<summary></summary>
		public string Provider;
		///<summary></summary>
		public string Code;
		///<summary></summary>
		public string Tooth;
		///<summary></summary>
		public string Description;
		///<summary></summary>
		public string Fee;
		///<summary></summary>
		public string InsEst;
		///<summary></summary>
		public string InsPay;
		///<summary></summary>
		public string Patient;
		///<summary></summary>
		public string Adj;
		///<summary></summary>
		public string Paid;
		///<summary></summary>
		public string Balance;

		///<summary>Set all fields to "".  Nulls mess up the printing framework.</summary>
		public AcctLine(){
			Date="";
			Provider="";
			Code="";
			Tooth="";
			Description="";
			Fee="";
			InsEst="";
			InsPay="";
			Patient="";
			Adj="";
			Paid="";
			Balance="";
		}
	}

	///<summary>A set of data representing a statement for one family.  Could also be represented by a Dataset at a lower level in the program.  "general" table would have general fields for the statement.  "patient" table would have a row for each patient in the family with data about that patient, like name, balance, appt.  The remaining tables would be named similar to this: "account234", where 234 would be the patNum.  Those tables would hold the data for the actual grids for each patient.  If the table is combined for the entire family, it would be called "account".</summary>
	public class FamilyStatementData{
		///<summary>PatNum of the guarantor.</summary>
		public int GuarNum;
		public List<PatStatementData> PatDataList;
		public List<PatStatementAbout> PatAboutList;

		public FamilyStatementData(){
			PatDataList=new List<PatStatementData>();
			PatAboutList=new List<PatStatementAbout>();
		}
	}

	///<summary>Used in FamilyStatementData.  Essentially, a collection of named tables.  But strongly typed.</summary>
	public class PatStatementData{
		public int PatNum;
		public List<AcctLine> PatData;

		public PatStatementData(){
			PatData=new List<AcctLine>();
		}
	}

	///<summary>Used in FamilyStatementData.  Data about one patient in the family which would not be part of the grid for that patient.</summary>
	public class PatStatementAbout{
		public int PatNum;
		public string PatName;
		public double Balance;
		public string ApptDescript;
	}

	///<summary>Account line type used when displaying lines in the Account module.</summary>
	public enum AcctModType{
		///<summary>1</summary>
		Proc=1,
		///<summary>2</summary>
		Adj,
		///<summary>3</summary>
		Pay,
		///<summary>4</summary>
		Claim,
		///<summary>5</summary>
		Comm,
		///<summary>6</summary>
		PayPlan}

	///<summary>A single line of payment info in the Account.  Might be an actual payment, or an unattached paysplit, or a daily summary of attached paysplits.</summary>
	public class PayInfo{
		///<summary>Payment,PaySplit, or DailySummary.</summary>
		public PayInfoType Type;
		///<summary></summary>
		public DateTime Date;
		///<summary></summary>
		public string Description;
		///<summary>The amount that will affect the balance. Other amounts should be included in the description instead.</summary>
		public double Amount;
		///<summary>The payment to which this info refers.</summary>
		public int PayNum;
		///<summary></summary>
		public int PayPlanNum;

		///<summary></summary>
		public PayInfo Copy(){
			PayInfo pi=new PayInfo();
			pi.Type=Type;
			pi.Date=Date;
			pi.Description=Description;
			pi.Amount=Amount;
			pi.PayNum=PayNum;
			pi.PayPlanNum=PayPlanNum;
			return pi;
		}
	}

	///<summary>Used by PayInfo to display lines in Account.</summary>
	public enum PayInfoType{
		///<summary>A payment line. The entire payment amount does not necessarily affect the balance.  Splits only affect the balance here if they have the same date and patNum as the payment.  Whether or not they are attached to procedures, they will be included.  DailySummaries never show for paysplits with same date as payment.</summary>
		Payment,
		///<summary>A single paysplit. The date or patNum is different than the payment, but not attached to a procedure.  So balance is affected.</summary>
		PaySplit
		//<summary>Depracated now that we have text wrap.  Informational only.  Does not affect balance. A group of all paysplits with the same date and patnum. All are attached to procedures.  Balance is affected by individual line item entries on the procedure lines.</summary>
		//DailySummary
	}

}











