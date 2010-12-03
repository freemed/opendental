/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using System.Data;
using Microsoft.Win32;
using OpenDentBusiness;
using CodeBase;
using SparksToothChart;
using OpenDental.UI;

namespace OpenDental{
///<summary></summary>
	public class FormProcGroup:System.Windows.Forms.Form {
		private System.Windows.Forms.Label label7;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butDelete;
		private ODErrorProvider errorProvider2=new ODErrorProvider();
		private OpenDental.ODtextBox textNotes;
		private Label label15;
		private Label label16;
		private TextBox textUser;
		private OpenDental.UI.Button buttonUseAutoNote;
		public List<ClaimProcHist> HistList;
		private ODGrid gridProc;
		private SignatureBoxWrapper signatureBoxWrapper;
		private Label label12;
		private ValidDate textDateEntry;
		private Label label26;
		private ValidDate textProcDate;
		private Label label2;
		public Procedure GroupCur;
		public Procedure GroupOld;
		public List<ProcGroupItem> GroupItemList;
		public List<Procedure> ProcList;
		private List<Procedure> ProcListOld;
		private List<OrionProc> OrionProcList;
		///<summary>This keeps the noteChanged event from erasing the signature when first loading.</summary>
		private bool IsStartingUp;
		private bool SigChanged;
		private PatField[] PatFieldList;
		private Patient PatCur;
		private Family FamCur;
		public static bool IsOpen;
		public static long RxNum;
		private ODGrid gridPat;
		private UI.Button butRx;
		private UI.Button butExamSheets;
		private Label labelRepair;
		private System.Windows.Forms.Button butRepairY;
		private System.Windows.Forms.Button butRepairN;
		private System.Windows.Forms.Button butOnCallY;
		private System.Windows.Forms.Button butOnCallN;
		private System.Windows.Forms.Button butEffectiveCommN;
		private Label labelOnCall;
		private Label labelEffectiveComm;
		private System.Windows.Forms.Button butEffectiveCommY;
		private ODGrid gridPlanned;
		private UI.Button butNew;
		private UI.Button butClear;
		private UI.Button butUp;
		private UI.Button butDown;
		private Panel panelPlanned;
		private Label labelDPCpost;
		private ComboBox comboDPCpost;
		private DataTable TablePlanned;

		public FormProcGroup() {
			InitializeComponent();
			Lan.F(this);
		}

		///<summary>Inserts are no longer done within this dialog, but must be done ahead of time from outside.You must specify a procedure to edit, and only the changes that are made in this dialog get saved.  Only used when double click in Account, Chart, TP, and in ContrChart.AddProcedure().  The procedure may be deleted if new, and user hits Cancel.</summary>

		//Constructor from ProcEdit. Lots of this will need to be copied into the new Load function.
		/*public FormProcGroup(long groupNum) {
			GroupCur=Procedures.GetOneProc(groupNum,true);
			ProcGroupItem=ProcGroupItems.Refresh(groupNum);
			//Proc
			InitializeComponent();
			Lan.F(this);
		}*/

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcGroup));
			this.label7 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.textUser = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textProcDate = new OpenDental.ValidDate();
			this.signatureBoxWrapper = new OpenDental.UI.SignatureBoxWrapper();
			this.gridProc = new OpenDental.UI.ODGrid();
			this.textDateEntry = new OpenDental.ValidDate();
			this.buttonUseAutoNote = new OpenDental.UI.Button();
			this.textNotes = new OpenDental.ODtextBox();
			this.butDelete = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.gridPat = new OpenDental.UI.ODGrid();
			this.butRx = new OpenDental.UI.Button();
			this.butExamSheets = new OpenDental.UI.Button();
			this.labelRepair = new System.Windows.Forms.Label();
			this.butRepairY = new System.Windows.Forms.Button();
			this.butRepairN = new System.Windows.Forms.Button();
			this.butOnCallY = new System.Windows.Forms.Button();
			this.butOnCallN = new System.Windows.Forms.Button();
			this.butEffectiveCommN = new System.Windows.Forms.Button();
			this.labelOnCall = new System.Windows.Forms.Label();
			this.labelEffectiveComm = new System.Windows.Forms.Label();
			this.butEffectiveCommY = new System.Windows.Forms.Button();
			this.gridPlanned = new OpenDental.UI.ODGrid();
			this.butNew = new OpenDental.UI.Button();
			this.butClear = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.panelPlanned = new System.Windows.Forms.Panel();
			this.labelDPCpost = new System.Windows.Forms.Label();
			this.comboDPCpost = new System.Windows.Forms.ComboBox();
			this.panelPlanned.SuspendLayout();
			this.SuspendLayout();
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(23,78);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73,16);
			this.label7.TabIndex = 0;
			this.label7.Text = "&Notes";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(-17,278);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(110,41);
			this.label15.TabIndex = 79;
			this.label15.Text = "Signature /\r\nInitials";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(23,55);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(73,16);
			this.label16.TabIndex = 80;
			this.label16.Text = "User";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(98,52);
			this.textUser.Name = "textUser";
			this.textUser.ReadOnly = true;
			this.textUser.Size = new System.Drawing.Size(119,20);
			this.textUser.TabIndex = 101;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(-25,34);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(125,14);
			this.label12.TabIndex = 96;
			this.label12.Text = "Date Entry";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(178,34);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(83,18);
			this.label26.TabIndex = 97;
			this.label26.Text = "(for security)";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(2,14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96,14);
			this.label2.TabIndex = 101;
			this.label2.Text = "Procedure Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textProcDate
			// 
			this.textProcDate.Location = new System.Drawing.Point(98,12);
			this.textProcDate.Name = "textProcDate";
			this.textProcDate.ReadOnly = true;
			this.textProcDate.Size = new System.Drawing.Size(76,20);
			this.textProcDate.TabIndex = 100;
			// 
			// signatureBoxWrapper
			// 
			this.signatureBoxWrapper.BackColor = System.Drawing.SystemColors.ControlDark;
			this.signatureBoxWrapper.LabelText = "Invalid Signature";
			this.signatureBoxWrapper.Location = new System.Drawing.Point(98,276);
			this.signatureBoxWrapper.Name = "signatureBoxWrapper";
			this.signatureBoxWrapper.Size = new System.Drawing.Size(364,81);
			this.signatureBoxWrapper.TabIndex = 194;
			this.signatureBoxWrapper.SignatureChanged += new System.EventHandler(this.signatureBoxWrapper_SignatureChanged);
			// 
			// gridProc
			// 
			this.gridProc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridProc.HScrollVisible = true;
			this.gridProc.Location = new System.Drawing.Point(10,367);
			this.gridProc.Name = "gridProc";
			this.gridProc.ScrollValue = 0;
			this.gridProc.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridProc.Size = new System.Drawing.Size(858,222);
			this.gridProc.TabIndex = 193;
			this.gridProc.Title = "Procedures";
			this.gridProc.TranslationName = "TableProg";
			// 
			// textDateEntry
			// 
			this.textDateEntry.Location = new System.Drawing.Point(98,32);
			this.textDateEntry.Name = "textDateEntry";
			this.textDateEntry.ReadOnly = true;
			this.textDateEntry.Size = new System.Drawing.Size(76,20);
			this.textDateEntry.TabIndex = 95;
			// 
			// buttonUseAutoNote
			// 
			this.buttonUseAutoNote.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonUseAutoNote.Autosize = true;
			this.buttonUseAutoNote.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonUseAutoNote.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonUseAutoNote.CornerRadius = 4F;
			this.buttonUseAutoNote.Location = new System.Drawing.Point(382,46);
			this.buttonUseAutoNote.Name = "buttonUseAutoNote";
			this.buttonUseAutoNote.Size = new System.Drawing.Size(80,24);
			this.buttonUseAutoNote.TabIndex = 106;
			this.buttonUseAutoNote.Text = "Auto Note";
			this.buttonUseAutoNote.Click += new System.EventHandler(this.buttonUseAutoNote_Click);
			// 
			// textNotes
			// 
			this.textNotes.AcceptsReturn = true;
			this.textNotes.AcceptsTab = true;
			this.textNotes.Location = new System.Drawing.Point(98,72);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.Procedure;
			this.textNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNotes.Size = new System.Drawing.Size(364,200);
			this.textNotes.TabIndex = 1;
			this.textNotes.TextChanged += new System.EventHandler(this.textNotes_TextChanged);
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
			this.butDelete.Location = new System.Drawing.Point(19,606);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(83,24);
			this.butDelete.TabIndex = 8;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
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
			this.butCancel.Location = new System.Drawing.Point(792,609);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76,24);
			this.butCancel.TabIndex = 13;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(710,609);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76,24);
			this.butOK.TabIndex = 12;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// gridPat
			// 
			this.gridPat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridPat.HScrollVisible = false;
			this.gridPat.Location = new System.Drawing.Point(468,276);
			this.gridPat.Name = "gridPat";
			this.gridPat.ScrollValue = 0;
			this.gridPat.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridPat.Size = new System.Drawing.Size(400,81);
			this.gridPat.TabIndex = 195;
			this.gridPat.Title = "Patient Fields";
			this.gridPat.TranslationName = "TablePatient";
			this.gridPat.Visible = false;
			this.gridPat.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPat_CellDoubleClick);
			// 
			// butRx
			// 
			this.butRx.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRx.Autosize = true;
			this.butRx.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRx.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRx.CornerRadius = 4F;
			this.butRx.Location = new System.Drawing.Point(792,41);
			this.butRx.Name = "butRx";
			this.butRx.Size = new System.Drawing.Size(75,24);
			this.butRx.TabIndex = 106;
			this.butRx.Text = "Rx";
			this.butRx.Visible = false;
			this.butRx.Click += new System.EventHandler(this.butRx_Click);
			// 
			// butExamSheets
			// 
			this.butExamSheets.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butExamSheets.Autosize = true;
			this.butExamSheets.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butExamSheets.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butExamSheets.CornerRadius = 4F;
			this.butExamSheets.Location = new System.Drawing.Point(792,14);
			this.butExamSheets.Name = "butExamSheets";
			this.butExamSheets.Size = new System.Drawing.Size(76,24);
			this.butExamSheets.TabIndex = 106;
			this.butExamSheets.Text = "Exam Sheets";
			this.butExamSheets.Visible = false;
			this.butExamSheets.Click += new System.EventHandler(this.butExamSheets_Click);
			// 
			// labelRepair
			// 
			this.labelRepair.Location = new System.Drawing.Point(498,85);
			this.labelRepair.Name = "labelRepair";
			this.labelRepair.Size = new System.Drawing.Size(90,16);
			this.labelRepair.TabIndex = 196;
			this.labelRepair.Text = "Repair";
			this.labelRepair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelRepair.Visible = false;
			// 
			// butRepairY
			// 
			this.butRepairY.Location = new System.Drawing.Point(590,83);
			this.butRepairY.Name = "butRepairY";
			this.butRepairY.Size = new System.Drawing.Size(23,20);
			this.butRepairY.TabIndex = 198;
			this.butRepairY.Text = "Y";
			this.butRepairY.UseVisualStyleBackColor = true;
			this.butRepairY.Visible = false;
			this.butRepairY.Click += new System.EventHandler(this.butRepairY_Click);
			// 
			// butRepairN
			// 
			this.butRepairN.Location = new System.Drawing.Point(614,83);
			this.butRepairN.Name = "butRepairN";
			this.butRepairN.Size = new System.Drawing.Size(23,20);
			this.butRepairN.TabIndex = 198;
			this.butRepairN.Text = "N";
			this.butRepairN.UseVisualStyleBackColor = true;
			this.butRepairN.Visible = false;
			this.butRepairN.Click += new System.EventHandler(this.butRepairN_Click);
			// 
			// butOnCallY
			// 
			this.butOnCallY.Location = new System.Drawing.Point(590,41);
			this.butOnCallY.Name = "butOnCallY";
			this.butOnCallY.Size = new System.Drawing.Size(23,20);
			this.butOnCallY.TabIndex = 198;
			this.butOnCallY.Text = "Y";
			this.butOnCallY.UseVisualStyleBackColor = true;
			this.butOnCallY.Visible = false;
			this.butOnCallY.Click += new System.EventHandler(this.butOnCallY_Click);
			// 
			// butOnCallN
			// 
			this.butOnCallN.Location = new System.Drawing.Point(614,41);
			this.butOnCallN.Name = "butOnCallN";
			this.butOnCallN.Size = new System.Drawing.Size(23,20);
			this.butOnCallN.TabIndex = 198;
			this.butOnCallN.Text = "N";
			this.butOnCallN.UseVisualStyleBackColor = true;
			this.butOnCallN.Visible = false;
			this.butOnCallN.Click += new System.EventHandler(this.butOnCallN_Click);
			// 
			// butEffectiveCommN
			// 
			this.butEffectiveCommN.Location = new System.Drawing.Point(614,62);
			this.butEffectiveCommN.Name = "butEffectiveCommN";
			this.butEffectiveCommN.Size = new System.Drawing.Size(23,20);
			this.butEffectiveCommN.TabIndex = 198;
			this.butEffectiveCommN.Text = "N";
			this.butEffectiveCommN.UseVisualStyleBackColor = true;
			this.butEffectiveCommN.Visible = false;
			this.butEffectiveCommN.Click += new System.EventHandler(this.butEffectiveCommN_Click);
			// 
			// labelOnCall
			// 
			this.labelOnCall.Location = new System.Drawing.Point(498,43);
			this.labelOnCall.Name = "labelOnCall";
			this.labelOnCall.Size = new System.Drawing.Size(90,16);
			this.labelOnCall.TabIndex = 196;
			this.labelOnCall.Text = "On Call";
			this.labelOnCall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelOnCall.Visible = false;
			// 
			// labelEffectiveComm
			// 
			this.labelEffectiveComm.Location = new System.Drawing.Point(498,64);
			this.labelEffectiveComm.Name = "labelEffectiveComm";
			this.labelEffectiveComm.Size = new System.Drawing.Size(90,16);
			this.labelEffectiveComm.TabIndex = 196;
			this.labelEffectiveComm.Text = "Effective Comm";
			this.labelEffectiveComm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelEffectiveComm.Visible = false;
			// 
			// butEffectiveCommY
			// 
			this.butEffectiveCommY.Location = new System.Drawing.Point(590,62);
			this.butEffectiveCommY.Name = "butEffectiveCommY";
			this.butEffectiveCommY.Size = new System.Drawing.Size(23,20);
			this.butEffectiveCommY.TabIndex = 198;
			this.butEffectiveCommY.Text = "Y";
			this.butEffectiveCommY.UseVisualStyleBackColor = true;
			this.butEffectiveCommY.Visible = false;
			this.butEffectiveCommY.Click += new System.EventHandler(this.butEffectiveCommY_Click);
			// 
			// gridPlanned
			// 
			this.gridPlanned.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridPlanned.HScrollVisible = false;
			this.gridPlanned.Location = new System.Drawing.Point(0,28);
			this.gridPlanned.Name = "gridPlanned";
			this.gridPlanned.ScrollValue = 0;
			this.gridPlanned.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridPlanned.Size = new System.Drawing.Size(400,131);
			this.gridPlanned.TabIndex = 204;
			this.gridPlanned.Title = "Planned Appointments";
			this.gridPlanned.TranslationName = "TablePlannedAppts";
			this.gridPlanned.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPlanned_CellDoubleClick);
			// 
			// butNew
			// 
			this.butNew.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNew.Autosize = false;
			this.butNew.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNew.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNew.CornerRadius = 4F;
			this.butNew.Image = global::OpenDental.Properties.Resources.Add;
			this.butNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butNew.Location = new System.Drawing.Point(43,3);
			this.butNew.Name = "butNew";
			this.butNew.Size = new System.Drawing.Size(75,23);
			this.butNew.TabIndex = 205;
			this.butNew.Text = "Add";
			this.butNew.Click += new System.EventHandler(this.butNew_Click);
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClear.Autosize = false;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClear.CornerRadius = 4F;
			this.butClear.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClear.Location = new System.Drawing.Point(123,3);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(75,23);
			this.butClear.TabIndex = 206;
			this.butClear.Text = "Delete";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butUp.Autosize = false;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(203,3);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(75,23);
			this.butUp.TabIndex = 207;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDown.Autosize = false;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(283,3);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(75,23);
			this.butDown.TabIndex = 208;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// panelPlanned
			// 
			this.panelPlanned.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelPlanned.Controls.Add(this.butDown);
			this.panelPlanned.Controls.Add(this.butUp);
			this.panelPlanned.Controls.Add(this.butClear);
			this.panelPlanned.Controls.Add(this.butNew);
			this.panelPlanned.Controls.Add(this.gridPlanned);
			this.panelPlanned.Location = new System.Drawing.Point(468,111);
			this.panelPlanned.Name = "panelPlanned";
			this.panelPlanned.Size = new System.Drawing.Size(400,159);
			this.panelPlanned.TabIndex = 199;
			this.panelPlanned.Visible = false;
			// 
			// labelDPCpost
			// 
			this.labelDPCpost.Location = new System.Drawing.Point(488,15);
			this.labelDPCpost.Name = "labelDPCpost";
			this.labelDPCpost.Size = new System.Drawing.Size(100,16);
			this.labelDPCpost.TabIndex = 201;
			this.labelDPCpost.Text = "DPC Post Visit";
			this.labelDPCpost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelDPCpost.Visible = false;
			// 
			// comboDPCpost
			// 
			this.comboDPCpost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDPCpost.DropDownWidth = 177;
			this.comboDPCpost.FormattingEnabled = true;
			this.comboDPCpost.Location = new System.Drawing.Point(590,14);
			this.comboDPCpost.MaxDropDownItems = 30;
			this.comboDPCpost.Name = "comboDPCpost";
			this.comboDPCpost.Size = new System.Drawing.Size(177,21);
			this.comboDPCpost.TabIndex = 200;
			this.comboDPCpost.Visible = false;
			this.comboDPCpost.SelectionChangeCommitted += new System.EventHandler(this.comboDPCpost_SelectionChangeCommitted);
			// 
			// FormProcGroup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(880,645);
			this.Controls.Add(this.labelDPCpost);
			this.Controls.Add(this.comboDPCpost);
			this.Controls.Add(this.panelPlanned);
			this.Controls.Add(this.butRepairN);
			this.Controls.Add(this.butEffectiveCommN);
			this.Controls.Add(this.butOnCallN);
			this.Controls.Add(this.butRepairY);
			this.Controls.Add(this.butEffectiveCommY);
			this.Controls.Add(this.butOnCallY);
			this.Controls.Add(this.labelRepair);
			this.Controls.Add(this.labelEffectiveComm);
			this.Controls.Add(this.labelOnCall);
			this.Controls.Add(this.gridPat);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textProcDate);
			this.Controls.Add(this.signatureBoxWrapper);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.gridProc);
			this.Controls.Add(this.textDateEntry);
			this.Controls.Add(this.butRx);
			this.Controls.Add(this.butExamSheets);
			this.Controls.Add(this.buttonUseAutoNote);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.textUser);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcGroup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Group Note";
			this.Load += new System.EventHandler(this.FormProcGroup_Load);
			this.panelPlanned.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormProcGroup_Load(object sender, System.EventArgs e){
			IsOpen=true;
			IsStartingUp=true;
			//ProcList gets set in ContrChart where this form is created.
			PatCur=Patients.GetPat(GroupCur.PatNum);
			FamCur=Patients.GetFamily(GroupCur.PatNum);
			GroupOld=GroupCur.Copy();
			ProcListOld=new List<Procedure>();
			for(int i=0;i<ProcList.Count;i++){
				ProcListOld.Add(ProcList[i].Copy());
			}
			ModifyForOrionMode();
			textProcDate.Text=GroupCur.ProcDate.ToShortDateString();
			textDateEntry.Text=GroupCur.DateEntryC.ToShortDateString();
			textUser.Text=Userods.GetName(GroupCur.UserNum);//might be blank. Will change automatically if user changes note or alters sig.
			textNotes.Text=GroupCur.Note;
			FillProcedures();
			textNotes.Select();
			string keyData=GetSignatureKey();
			signatureBoxWrapper.FillSignature(GroupCur.SigIsTopaz,keyData,GroupCur.Signature);
			signatureBoxWrapper.BringToFront();
			FillPatientData();
			FillPlanned();
			IsStartingUp=false;
		}

		private void FillPatientData(){
			if(PatCur==null){
				gridPat.BeginUpdate();
				gridPat.Rows.Clear();
				gridPat.Columns.Clear();
				gridPat.EndUpdate();
				return;
			}
			gridPat.BeginUpdate();
			gridPat.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",150);
			gridPat.Columns.Add(col);
			col=new ODGridColumn("",250);
			gridPat.Columns.Add(col);
			gridPat.Rows.Clear();
			ODGridRow row;
			PatFieldList=PatFields.Refresh(PatCur.PatNum);
			List<DisplayField> fields=DisplayFields.GetForCategory(DisplayFieldCategory.PatientInformation);
			for(int f=0;f<fields.Count;f++) {
				row=new ODGridRow();
				if(fields[f].Description==""){
					//...
				}
				else{
					if(fields[f].InternalName=="PatFields") {
						//don't add a cell
					}
					else {
						row.Cells.Add(fields[f].Description);
					}
				}
				switch(fields[f].InternalName){
					//...
					case "PatFields":
						PatField field;
						for(int i=0;i<PatFieldDefs.List.Length;i++){
							if(i>0){
								row=new ODGridRow();
							}
							row.Cells.Add(PatFieldDefs.List[i].FieldName);
							field=PatFields.GetByName(PatFieldDefs.List[i].FieldName,PatFieldList);
							if(field==null){
								row.Cells.Add("");
							}
							else{
								row.Cells.Add(field.FieldValue);
							}
							row.Tag="PatField"+i.ToString();
							gridPat.Rows.Add(row);
						}
						break;
				}
				if(fields[f].InternalName=="PatFields"){
					//don't add the row here
				}
				else{
					gridPat.Rows.Add(row);
				}
			}
			gridPat.EndUpdate();
		}

		private void FillProcedures(){
			gridProc.BeginUpdate();
			gridProc.Columns.Clear();
			ODGridColumn col;
			DisplayFields.RefreshCache();
			List<DisplayField> fields=DisplayFields.GetForCategory(DisplayFieldCategory.ProcedureGroupNote);
			for(int i=0;i<fields.Count;i++) {
				if(fields[i].Description=="") {
					col=new ODGridColumn(fields[i].InternalName,fields[i].ColumnWidth);
				}
				else {
					col=new ODGridColumn(fields[i].Description,fields[i].ColumnWidth);
				}
				if(fields[i].InternalName=="Amount") {
					col.TextAlign=HorizontalAlignment.Right;
				}
				if(fields[i].InternalName=="ADA Code") {
					col.TextAlign=HorizontalAlignment.Center;
				}
				gridProc.Columns.Add(col);
			}
			gridProc.Rows.Clear();
			for(int i=0;i<ProcList.Count;i++) {
				ODGridRow row=new ODGridRow();
				for(int f=0;f<fields.Count;f++) {
					switch(fields[f].InternalName) {
						case "Date":
							row.Cells.Add(ProcList[i].ProcDate.ToShortDateString());
							break;
						case "Th":
							row.Cells.Add(ProcList[i].ToothNum.ToString());
							break;
						case "Surf":
							row.Cells.Add(ProcList[i].Surf.ToString());
							break;
						case "Description":
							row.Cells.Add(ProcedureCodes.GetLaymanTerm(ProcList[i].CodeNum));
							break;
						case "Stat":
							row.Cells.Add(ProcList[i].ProcStatus.ToString());
							break;
						case "Prov":
							row.Cells.Add(Providers.GetAbbr(ProcList[i].ProvNum));
							break;
						case "Amount":
							row.Cells.Add(ProcList[i].ProcFee.ToString("F"));
							break;
						case "ADA Code":
							row.Cells.Add(ProcedureCodes.GetStringProcCode(ProcList[i].CodeNum));
							break;
						case "Stat 2":
							row.Cells.Add(((OrionStatus)OrionProcList[i].Status2).ToString());
							break;
						case "On Call":
							if(OrionProcList[i].IsOnCall) {
								row.Cells.Add("Y");
							}
							else {
								row.Cells.Add("");
							}
							break;
						case "Effective Comm":
							if(OrionProcList[i].IsEffectiveComm) {
								row.Cells.Add("Y");
							}
							else {
								row.Cells.Add("");
							}
							break;
						case "Repair":
							if(OrionProcList[i].IsRepair) {
								row.Cells.Add("Y");
							}
							else {
								row.Cells.Add("");
							}
							break;
						case "DPCpost":
							row.Cells.Add(((OrionDPC)OrionProcList[i].DPCpost).ToString());
							break;
					}
				}
				gridProc.Rows.Add(row);
			}
			gridProc.EndUpdate();
		}

		private void ModifyForOrionMode(){
			if(Programs.UsingOrion){
				OrionProcList=new List<OrionProc>();
				for(int i=0;i<ProcList.Count;i++){
					OrionProcList.Add(OrionProcs.GetOneByProcNum(ProcList[i].ProcNum));
				}
				labelOnCall.Visible=true;
				butOnCallY.Visible=true;
				butOnCallN.Visible=true;
				labelEffectiveComm.Visible=true;
				butEffectiveCommY.Visible=true;
				butEffectiveCommN.Visible=true;
				for(int i=0;i<ProcList.Count;i++){
					if(ProcedureCodes.GetProcCodeFromDb(ProcList[i].CodeNum).IsProsth){
						labelRepair.Visible=true;
						butRepairY.Visible=true;
						butRepairN.Visible=true;
					}
				}
				butRx.Visible=true;
				butExamSheets.Visible=true;
				panelPlanned.Visible=true;
				gridPat.Visible=true;
				textProcDate.ReadOnly=false;
				labelDPCpost.Visible=true;
				comboDPCpost.Visible=true;
				comboDPCpost.Items.Clear();
				comboDPCpost.Items.Add("Not Specified");
				comboDPCpost.Items.Add("None");
				comboDPCpost.Items.Add("1A-within 1 day");
				comboDPCpost.Items.Add("1B-within 30 days");
				comboDPCpost.Items.Add("1C-within 60 days");
				comboDPCpost.Items.Add("2-within 120 days");
				comboDPCpost.Items.Add("3-within 1 year");
				comboDPCpost.Items.Add("4-no further treatment/appt");
				comboDPCpost.Items.Add("5-no appointment needed");
			}
			else{
				this.ClientSize = new System.Drawing.Size(556,645);
			}
		}

		private void Refresh(){
			FillPatientData();
			FillProcedures();
			FillPlanned();
		}
		
		#region Planned
		private void FillPlanned(){
			if(PatCur==null){
				butNew.Enabled=false;
				butClear.Enabled=false;
				butUp.Enabled=false;
				butDown.Enabled=false;
				gridPlanned.Enabled=false;
				return;
			}
			else{
				butNew.Enabled=true;
				butClear.Enabled=true;
				butUp.Enabled=true;
				butDown.Enabled=true;
				gridPlanned.Enabled=true;
			}
			//Fill grid
			gridPlanned.BeginUpdate();
			gridPlanned.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g("TablePlannedAppts","#"),15,HorizontalAlignment.Center);
			gridPlanned.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePlannedAppts","Min"),25);
			gridPlanned.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePlannedAppts","Procedures"),160);
			gridPlanned.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePlannedAppts","Note"),115);
			gridPlanned.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePlannedAppts","SchedBy"),50);
			gridPlanned.Columns.Add(col);
			gridPlanned.Rows.Clear();
			ODGridRow row;
			TablePlanned=ChartModules.GetAll(PatCur.PatNum,false).Tables["Planned"];
			//This gets done in the business layer:
			/*
			bool iochanged=false;
			for(int i=0;i<table.Rows.Count;i++) {
				if(table.Rows[i]["ItemOrder"].ToString()!=i.ToString()) {
					PlannedAppt planned=PlannedAppts.CreateObject(PIn.PLong(table.Rows[i]["PlannedApptNum"].ToString()));
					planned.ItemOrder=i;
					PlannedAppts.InsertOrUpdate(planned);
					iochanged=true;
				}
			}
			if(iochanged) {
				DataSetMain=ChartModules.GetAll(PatCur.PatNum,checkAudit.Checked);
				table=DataSetMain.Tables["Planned"];
			}*/
			for(int i=0;i<TablePlanned.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(TablePlanned.Rows[i]["ItemOrder"].ToString());
				row.Cells.Add(TablePlanned.Rows[i]["minutes"].ToString());
				row.Cells.Add(TablePlanned.Rows[i]["ProcDescript"].ToString());
				row.Cells.Add(TablePlanned.Rows[i]["Note"].ToString());
				string text;
				List<Procedure> procsList=Procedures.Refresh(PatCur.PatNum);
				DateTime newDateSched=new DateTime();
				for(int p=0;p<procsList.Count;p++) {
					if(procsList[p].PlannedAptNum==PIn.Long(TablePlanned.Rows[i]["AptNum"].ToString())) {
						OrionProc op=OrionProcs.GetOneByProcNum(procsList[p].ProcNum);
						if(op!=null && op.DateScheduleBy.Year>1880) {
							if(newDateSched.Year<1880) {
								newDateSched=op.DateScheduleBy;
							}
							else {
								if(op.DateScheduleBy<newDateSched) {
									newDateSched=op.DateScheduleBy;
								}
							}
						}
					}
				}
				if(newDateSched.Year>1880) {
					text=newDateSched.ToShortDateString();
				}
				else {
					text="None";
				}
				row.Cells.Add(text);
				row.ColorText=Color.FromArgb(PIn.Int(TablePlanned.Rows[i]["colorText"].ToString()));
				row.ColorBackG=Color.FromArgb(PIn.Int(TablePlanned.Rows[i]["colorBackG"].ToString()));
				gridPlanned.Rows.Add(row);
			}
			gridPlanned.EndUpdate();
		}

		private void butNew_Click(object sender,EventArgs e) {
			/*if(ApptPlanned.Visible){
				if(MessageBox.Show(Lan.g(this,"Replace existing planned appointment?")
					,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
					return;
				//Procedures.UnattachProcsInPlannedAppt(ApptPlanned.Info.MyApt.AptNum);
				AppointmentL.Delete(PIn.PInt(ApptPlanned.DataRoww["AptNum"].ToString()));
			}*/
			Appointment AptCur=new Appointment();
			AptCur.PatNum=PatCur.PatNum;
			AptCur.ProvNum=PatCur.PriProv;
			AptCur.ClinicNum=PatCur.ClinicNum;
			AptCur.AptStatus=ApptStatus.Planned;
			AptCur.AptDateTime=DateTime.Today;
			AptCur.Pattern="/X/";
			Appointments.Insert(AptCur);
			PlannedAppt plannedAppt=new PlannedAppt();
			plannedAppt.AptNum=AptCur.AptNum;
			plannedAppt.PatNum=PatCur.PatNum;
			plannedAppt.ItemOrder=TablePlanned.Rows.Count+1;
			PlannedAppts.Insert(plannedAppt);
			FormApptEdit FormApptEdit2=new FormApptEdit(AptCur.AptNum);
			FormApptEdit2.IsNew=true;
			FormApptEdit2.ShowDialog();
			if(FormApptEdit2.DialogResult!=DialogResult.OK){
				//delete new appt, delete plannedappt, and unattach procs already handled in dialog
				Refresh();
				return;
			}
			List<Procedure> myProcList=Procedures.Refresh(PatCur.PatNum);
			bool allProcsHyg=true;
			for(int i=0;i<myProcList.Count;i++){
				if(myProcList[i].PlannedAptNum!=AptCur.AptNum)
					continue;//only concerned with procs on this plannedAppt
				if(!ProcedureCodes.GetProcCode(myProcList[i].CodeNum).IsHygiene){
					allProcsHyg=false;
					break;
				}
			}
			if(allProcsHyg && PatCur.SecProv!=0){
				Appointment aptOld=AptCur.Clone();
				AptCur.ProvNum=PatCur.SecProv;
				Appointments.Update(AptCur,aptOld);
			}
			Patient patOld=PatCur.Copy();
			//PatCur.NextAptNum=AptCur.AptNum;
			PatCur.PlannedIsDone=false;
			Patients.Update(PatCur,patOld);
			Refresh();//if procs were added in appt, then this will display them
		}

		private void butClear_Click(object sender,EventArgs e) {
			if(gridPlanned.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select an item first");
				return;
			}
			if(!MsgBox.Show(this,true,"Delete planned appointment(s)?")){
				return;
			}
			for(int i=0;i<gridPlanned.SelectedIndices.Length;i++){
				Appointments.Delete(PIn.Long(TablePlanned.Rows[gridPlanned.SelectedIndices[i]]["AptNum"].ToString()));
			}
			Refresh();
		}

		private void butUp_Click(object sender,EventArgs e) {
			if(gridPlanned.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			if(gridPlanned.SelectedIndices.Length>1) {
				MsgBox.Show(this,"Please only select one item first.");
				return;
			}
			int idx=gridPlanned.SelectedIndices[0];
			if(idx==0) {
				return;
			}
			PlannedAppt planned;
			planned=PlannedAppts.GetOne(PIn.Long(TablePlanned.Rows[idx]["PlannedApptNum"].ToString()));
			planned.ItemOrder=idx-1;
			PlannedAppts.Update(planned);
			planned=PlannedAppts.GetOne(PIn.Long(TablePlanned.Rows[idx-1]["PlannedApptNum"].ToString()));
			planned.ItemOrder=idx;
			PlannedAppts.Update(planned);
			TablePlanned=ChartModules.GetAll(PatCur.PatNum,false).Tables["Planned"];
			Refresh();
			gridPlanned.SetSelected(idx-1,true);
		}

		private void butDown_Click(object sender,EventArgs e) {
			if(gridPlanned.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			if(gridPlanned.SelectedIndices.Length>1) {
				MsgBox.Show(this,"Please only select one item first.");
				return;
			}
			int idx=gridPlanned.SelectedIndices[0];
			if(idx==TablePlanned.Rows.Count-1) {
				return;
			}
			PlannedAppt planned;
			planned=PlannedAppts.GetOne(PIn.Long(TablePlanned.Rows[idx]["PlannedApptNum"].ToString()));
			planned.ItemOrder=idx+1;
			PlannedAppts.Update(planned);
			planned=PlannedAppts.GetOne(PIn.Long(TablePlanned.Rows[idx+1]["PlannedApptNum"].ToString()));
			planned.ItemOrder=idx;
			PlannedAppts.Update(planned);
			TablePlanned=ChartModules.GetAll(PatCur.PatNum,false).Tables["Planned"];
			Refresh();
			gridPlanned.SetSelected(idx+1,true);
		}

		private void gridPlanned_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			long aptnum=PIn.Long(TablePlanned.Rows[e.Row]["AptNum"].ToString());
			FormApptEdit FormAE=new FormApptEdit(aptnum);
			FormAE.ShowDialog();
			if(FormAE.DialogResult==DialogResult.OK) {
				Refresh();
			}
			for(int i=0;i<TablePlanned.Rows.Count;i++){
				if(TablePlanned.Rows[i]["AptNum"].ToString()==aptnum.ToString()){
					gridPlanned.SetSelected(i,true);
				}
			}
		}
		#endregion Planned


		private void butRx_Click(object sender,EventArgs e) {
			/*?
			if(UsingEcwTight()) {
				VBbridges.Ecw.LoadRxForm((int)Bridges.ECW.UserId,Bridges.ECW.EcwConfigPath,(int)Bridges.ECW.AptNum);
				//refresh the right panel:
				try {
					string strAppServer=VBbridges.Ecw.GetAppServer((int)Bridges.ECW.UserId,Bridges.ECW.EcwConfigPath);
					webBrowserEcw.Url=new Uri("http://"+strAppServer+"/mobiledoc/jsp/dashboard/Overview.jsp?ptId="
							+PatCur.PatNum.ToString()+"&panelName=overview&pnencid="
							+Bridges.ECW.AptNum.ToString()+"&context=progressnotes&TrUserId="+Bridges.ECW.UserId.ToString());
					labelECWerror.Visible=false;
				}
				catch(Exception ex) {
					webBrowserEcw.Url=null;
					labelECWerror.Text="Error: "+ex.Message;
					labelECWerror.Visible=true;
				}
			}
			else {
			?*/
				if(!Security.IsAuthorized(Permissions.RxCreate)) {
					return;
				}
				FormRxSelect FormRS=new FormRxSelect(PatCur);
				FormRS.ShowDialog();
				if(FormRS.DialogResult!=DialogResult.OK) return;
				//ModuleSelected(PatCur.PatNum);
				SecurityLogs.MakeLogEntry(Permissions.RxCreate,PatCur.PatNum,PatCur.GetNameLF());
			//}
			if(FormRS.DialogResult==DialogResult.OK){
				RxPat Rx=RxPats.GetRx(RxNum);
				if(textNotes.Text!=""){
					textNotes.Text+="\r\n";
				}
				textNotes.Text+="Rx - "+Rx.Drug+" - #"+Rx.Disp;
				string rxNote=Pharmacies.GetDescription(RxNum);
				if(rxNote!=""){
					textNotes.Text+="\r\n"+rxNote;
				}
			}
		}

		private void buttonUseAutoNote_Click(object sender,EventArgs e) {
			FormAutoNoteCompose FormA=new FormAutoNoteCompose();
			FormA.ShowDialog();
			if(FormA.DialogResult==DialogResult.OK) {
				textNotes.AppendText(FormA.CompletedNote);
			}
		}

		private void butExamSheets_Click(object sender,EventArgs e) {
			FormExamSheets fes=new FormExamSheets();
			fes.PatNum=GroupCur.PatNum;
			fes.ShowDialog();
			//TODO: Print a note about Exam Sheet added.
		}

		private void textNotes_TextChanged(object sender,EventArgs e) {
			if(!IsStartingUp//so this happens only if user changes the note
				&& !SigChanged)//and the original signature is still showing.
			{
				//SigChanged=true;//happens automatically through the event.
				signatureBoxWrapper.ClearSignature();
			}
		}

		private string GetSignatureKey(){
			string keyData=GroupCur.ProcDate.ToShortDateString();
			keyData+=GroupCur.DateEntryC.ToShortDateString();
			keyData+=GroupCur.UserNum.ToString();//Security.CurUser.UserName;
			keyData+=GroupCur.Note;
			GroupItemList=ProcGroupItems.Refresh(GroupCur.ProcNum);//Orders the list to ensure same key in all cases.
			for(int i=0;i<GroupItemList.Count;i++){
				keyData+=GroupItemList[i].ProcGroupItemNum.ToString();
			}
			return keyData;
		}

		private void SaveSignature(){
			if(SigChanged){
				string keyData=GetSignatureKey();
				GroupCur.Signature=signatureBoxWrapper.GetSignature(keyData);
				GroupCur.SigIsTopaz=signatureBoxWrapper.GetSigIsTopaz();
			}
		}

		private void signatureBoxWrapper_SignatureChanged(object sender,EventArgs e) {
			GroupCur.UserNum=Security.CurUser.UserNum;
			textUser.Text=Userods.GetName(GroupCur.UserNum);
			SigChanged=true;
		}

		private void butOnCallY_Click(object sender,EventArgs e) {			
			for(int i=0;i<OrionProcList.Count;i++){
				OrionProcList[i].IsOnCall=true;
			}
			FillProcedures();
		}

		private void butOnCallN_Click(object sender,EventArgs e) {
			for(int i=0;i<OrionProcList.Count;i++){
				OrionProcList[i].IsOnCall=false;
			}
			FillProcedures();
		}

		private void butEffectiveCommY_Click(object sender,EventArgs e) {
			for(int i=0;i<OrionProcList.Count;i++){
				OrionProcList[i].IsEffectiveComm=true;
			}
			FillProcedures();
		}

		private void butEffectiveCommN_Click(object sender,EventArgs e) {
			for(int i=0;i<OrionProcList.Count;i++){
				OrionProcList[i].IsEffectiveComm=false;
			}
			FillProcedures();
		}

		private void butRepairY_Click(object sender,EventArgs e) {
			for(int i=0;i<OrionProcList.Count;i++){
				if(ProcedureCodes.GetProcCodeFromDb(ProcList[i].CodeNum).IsProsth){//OrionProcList[i] corresponds to ProcList[i]
					OrionProcList[i].IsRepair=true;
				}
			}
			FillProcedures();
		}

		private void butRepairN_Click(object sender,EventArgs e) {
			for(int i=0;i<OrionProcList.Count;i++){
				if(ProcedureCodes.GetProcCodeFromDb(ProcList[i].CodeNum).IsProsth){//OrionProcList[i] corresponds to ProcList[i]
					OrionProcList[i].IsRepair=false;
				}
			}
			FillProcedures();
		}

		private void comboDPCpost_SelectionChangeCommitted(object sender,EventArgs e) {
			for(int i=0;i<OrionProcList.Count;i++) {
				OrionProcList[i].DPCpost=(OrionDPC)comboDPCpost.SelectedIndex;
			}
			FillProcedures();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			bool result=MsgBox.Show(this,MsgBoxButtons.YesNo,"Are you sure you want delete this group note?");
			if(result){
				Procedures.Delete(GroupCur.ProcNum);
				for(int i=0;i<GroupItemList.Count;i++){
					ProcGroupItems.Delete(GroupItemList[i].ProcGroupItemNum);
				}
				DialogResult=DialogResult.Cancel;
				IsOpen=false;
			}
			return;
		}		

		private void butOK_Click(object sender,System.EventArgs e) {
			if(textProcDate.errorProvider1.GetError(textProcDate)!=""){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			GroupCur.Note=textNotes.Text;
			GroupCur.ProcDate=PIn.Date(this.textProcDate.Text);
			for(int i=0;i<ProcList.Count;i++){
				ProcList[i].ProcDate=GroupCur.ProcDate;
			}
			if(!signatureBoxWrapper.IsValid){
				MsgBox.Show(this,"Your signature is invalid. Please sign and click OK again.");
				return;
			}
			try {
				SaveSignature();
			}
			catch(Exception ex){
				MessageBox.Show(Lan.g(this,"Error saving signature.")+"\r\n"+ex.Message);
			}
			Procedures.Update(GroupCur,GroupOld);
			for(int i=0;i<ProcList.Count;i++){
				Procedures.Update(ProcList[i],ProcListOld[i]);
			}
			if(Programs.UsingOrion){
				for(int i=0;i<OrionProcList.Count;i++){
					OrionProcs.Update(OrionProcList[i]);
				}
			}
			DialogResult=DialogResult.OK;
			IsOpen=false;
		}

		private void butCancel_Click(object sender,System.EventArgs e) {
			if(GroupCur.IsNew){
				Procedures.Delete(GroupCur.ProcNum);
				for(int i=0;i<GroupItemList.Count;i++){
					ProcGroupItems.Delete(GroupItemList[i].ProcGroupItemNum);
				}
			}
			DialogResult=DialogResult.Cancel;
			IsOpen=false;
		}

		private void gridPat_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			string tag=gridPat.Rows[e.Row].Tag.ToString();
			tag=tag.Substring(8);//strips off all but the number: PatField1
			int index=PIn.Int(tag);
			PatField field=PatFields.GetByName(PatFieldDefs.List[index].FieldName,PatFieldList);
			if(field==null) {
				field=new PatField();
				field.PatNum=PatCur.PatNum;
				field.FieldName=PatFieldDefs.List[index].FieldName;
				if(PatFieldDefs.List[index].FieldType==PatFieldType.Text) {
					FormPatFieldEdit FormPF=new FormPatFieldEdit(field);
					FormPF.IsNew=true;
					FormPF.ShowDialog();
				}
				if(PatFieldDefs.List[index].FieldType==PatFieldType.PickList) {
					FormPatFieldPickEdit FormPF=new FormPatFieldPickEdit(field);
					FormPF.IsNew=true;
					FormPF.ShowDialog();
				}
				if(PatFieldDefs.List[index].FieldType==PatFieldType.Date) {
					FormPatFieldDateEdit FormPF=new FormPatFieldDateEdit(field);
					FormPF.IsNew=true;
					FormPF.ShowDialog();
				}
			}
			else{
				if(PatFieldDefs.List[index].FieldType==PatFieldType.Text) {
					FormPatFieldEdit FormPF=new FormPatFieldEdit(field);
					FormPF.ShowDialog();
				}
				if(PatFieldDefs.List[index].FieldType==PatFieldType.PickList) {
					FormPatFieldPickEdit FormPF=new FormPatFieldPickEdit(field);
					FormPF.ShowDialog();
				}
				if(PatFieldDefs.List[index].FieldType==PatFieldType.Date) {
					FormPatFieldDateEdit FormPF=new FormPatFieldDateEdit(field);
					FormPF.ShowDialog();
				}
			}
			FillPatientData();
		}










	}
}
