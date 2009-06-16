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
using Microsoft.Win32;
using OpenDentBusiness;
using CodeBase;
using SparksToothChart;
using OpenDental.UI;


namespace OpenDental{
///<summary></summary>
	public class FormProcEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textProc;
		private System.Windows.Forms.TextBox textSurfaces;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textDesc;
		private System.Windows.Forms.Label label7;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.TextBox textRange;
		private System.Windows.Forms.Label labelTooth;
		private System.Windows.Forms.Label labelRange;
		private System.Windows.Forms.Label labelSurfaces;
		private System.Windows.Forms.GroupBox groupQuadrant;
		private System.Windows.Forms.RadioButton radioUR;
		private System.Windows.Forms.RadioButton radioUL;
		private System.Windows.Forms.RadioButton radioLL;
		private System.Windows.Forms.RadioButton radioLR;
		private System.Windows.Forms.GroupBox groupArch;
		private System.Windows.Forms.RadioButton radioU;
		private System.Windows.Forms.RadioButton radioL;
		private System.Windows.Forms.GroupBox groupSextant;
		private System.Windows.Forms.RadioButton radioS1;
		private System.Windows.Forms.RadioButton radioS3;
		private System.Windows.Forms.RadioButton radioS2;
		private System.Windows.Forms.RadioButton radioS4;
		private System.Windows.Forms.RadioButton radioS5;
		private System.Windows.Forms.RadioButton radioS6;
		private System.Windows.Forms.Label label9;
		private OpenDental.ValidDate textDate;
		///<summary>Mostly used for permissions.</summary>
		public bool IsNew;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelClaim;
		private System.Windows.Forms.ListBox listBoxTeeth;
		private System.Windows.Forms.ListBox listBoxTeeth2;
		private OpenDental.UI.Button butChange;
		//private ProcStat OriginalStatus;
		private ODErrorProvider errorProvider2=new ODErrorProvider();
		private System.Windows.Forms.TextBox textTooth;
		private OpenDental.UI.Button butEditAnyway;
		private System.Windows.Forms.Label labelDx;
		private System.Windows.Forms.ComboBox comboPlaceService;
		private System.Windows.Forms.Label labelPlaceService;
		private OpenDental.UI.Button butSetComplete;
		private System.Windows.Forms.Label label10;
		private ProcedureCode ProcedureCode2;
		private System.Windows.Forms.Label label13;
		private OpenDental.UI.Button butAddEstimate;
		private Procedure ProcCur;
		private Procedure ProcOld;
		//private List<ClaimProc> ClaimProcList;
		private OpenDental.ValidDouble textProcFee;
		private System.Windows.Forms.CheckBox checkNoBillIns;
		private OpenDental.ODtextBox textNotes;
		private System.Windows.Forms.GroupBox groupProsth;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelClaimNote;
		private System.Windows.Forms.ListBox listProsth;
		private OpenDental.ValidDate textDateOriginalProsth;
		private OpenDental.ODtextBox textClaimNote;
		private List<ClaimProc> ClaimProcsForProc;
		//private Adjustment[] AdjForProc;
		private ArrayList PaySplitsForProc;
		private ArrayList AdjustmentsForProc;
		private Patient PatCur;
		private Family FamCur;
		private OpenDental.UI.Button butAddAdjust;
		private OpenDental.TableProcAdj tbAdj;
		private OpenDental.TableProcPay tbPay;
		private List <InsPlan> PlanList;
		private System.Windows.Forms.Label labelIncomplete;
		private OpenDental.ValidDate textDateEntry;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		///<summary>List of all payments (not paysplits) that this procedure is attached to.</summary>
		private List<Payment> PaymentsForProc;
		//private User user;
		//private uint m_autoAPIMsg;//ENP
		private const string APPBAR_AUTOMATION_API_MESSAGE = "EZNotes.AppBarStandalone.Auto.API.Message"; 
		private const uint MSG_RESTORE=2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textMedicalCode;
		private System.Windows.Forms.GroupBox groupMedical;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textDiagnosticCode;//ENP
		private const uint MSG_GETLASTNOTE=3;
		private System.Windows.Forms.CheckBox checkIsPrincDiag;//ENP
		private List <PatPlan> PatPlanList;
		private ListBox listProcStatus;
		private Label label14;
		private Label label15;
		private Label label16;
		private OpenDental.UI.Button butClearSig;
		private OpenDental.UI.SignatureBox sigBox;
		private List <Benefit> BenefitList;
		private bool SigChanged;
		private ComboBox comboProvNum;
		private ComboBox comboDx;
		private ComboBox comboPriority;
		private TextBox textUser;
		//private Label label17;
		//private Label label18;
		private Label label19;
		private TextBox textCodeMod1;
		private ComboBox comboBillingTypeTwo;
		private Label labelBillingTypeTwo;
		private ComboBox comboBillingTypeOne;
		private Label labelBillingTypeOne;
		private Label label20;
		private TextBox textCodeMod4;
		private TextBox textCodeMod3;
		private TextBox textCodeMod2;
		private TextBox textRevCode;
		private Label label22;
		private TextBox textUnitQty;
		private TextBox textUnitCode;
		private Label label21;
		private OpenDental.UI.Button buttonUseAutoNote;
		private Label label24;
		private Label label23;
		private TextBox textStop;
		private TextBox textStart;
		private ToolTip toolTip1;
		private IContainer components;
		private TextBox textTotal;
		private Label label25;
		private ValidDate textDateTP;
		private Label label27;
		private Label label26;
		///<summary>This keeps the noteChanged event from erasing the signature when first loading.</summary>
		private bool IsStartingUp;
		private List<Claim> ClaimList;
		//private OpenDental.UI.Button butTopazSign;
		private Panel panelSurfaces;
		private OpenDental.UI.Button butD;
		private OpenDental.UI.Button butBF;
		private OpenDental.UI.Button butL;
		private OpenDental.UI.Button butM;
		private OpenDental.UI.Button butV;
		private OpenDental.UI.Button butOI;
		private OpenDental.UI.Button butTopazSign;
		private Label labelInvalidSig;
		private Control sigBoxTopaz;
    private bool allowTopaz;
		private OpenDental.UI.Button butPickSite;
		private TextBox textSite;
		private Label labelSite;
		private Label label17;
		private OpenDental.UI.Button butShowMedical;
		private Label label18;
		private ODGrid gridIns;
		private bool StartedAttachedToClaim;
		public List<ClaimProcHist> HistList;
		public List<ClaimProcHist> LoopList;

		///<summary>Inserts are no longer done within this dialog, but must be done ahead of time from outside.You must specify a procedure to edit, and only the changes that are made in this dialog get saved.  Only used when double click in Account, Chart, TP, and in ContrChart.AddProcedure().  The procedure may be deleted if new, and user hits Cancel.</summary>
		public FormProcEdit(Procedure proc,Patient patCur,Family famCur){
			ProcCur=proc;
			ProcOld=proc.Copy();
			PatCur=patCur;
			FamCur=famCur;
			PlanList=InsPlans.Refresh(FamCur);
			//HistList=null;
			//LoopList=null;
			InitializeComponent();
			Lan.F(this);
			allowTopaz=(Environment.OSVersion.Platform!=PlatformID.Unix && !CodeBase.ODEnvironment.Is64BitOperatingSystem());
			sigBox.SetTabletState(1);
			if(!allowTopaz) {
				butTopazSign.Visible=false;
				sigBox.Visible=true;
			}
			else{
				//Add signature box for Topaz signatures.
				sigBoxTopaz=CodeBase.TopazWrapper.GetTopaz();
				sigBoxTopaz.Location=sigBox.Location;//this puts both boxes in the same spot.
				sigBoxTopaz.Name="sigBoxTopaz";
				sigBoxTopaz.Size=new System.Drawing.Size(362,79);
				sigBoxTopaz.TabIndex=92;
				sigBoxTopaz.Text="sigPlusNET1";
				sigBoxTopaz.Visible=false;
				Controls.Add(sigBoxTopaz);
				//It starts out accepting input. It will be set to 0 if a sig is already present.  It will be set back to 1 if note changes or if user clicks Clear.
				CodeBase.TopazWrapper.SetTopazState(sigBoxTopaz,1);
			}
			if(!PrefC.GetBool("ShowFeatureMedicalInsurance")) {
				groupMedical.Visible=false;
			}
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelTooth = new System.Windows.Forms.Label();
			this.labelSurfaces = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textProc = new System.Windows.Forms.TextBox();
			this.textTooth = new System.Windows.Forms.TextBox();
			this.textSurfaces = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textDesc = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.labelRange = new System.Windows.Forms.Label();
			this.textRange = new System.Windows.Forms.TextBox();
			this.groupQuadrant = new System.Windows.Forms.GroupBox();
			this.radioLR = new System.Windows.Forms.RadioButton();
			this.radioLL = new System.Windows.Forms.RadioButton();
			this.radioUL = new System.Windows.Forms.RadioButton();
			this.radioUR = new System.Windows.Forms.RadioButton();
			this.panelSurfaces = new System.Windows.Forms.Panel();
			this.groupArch = new System.Windows.Forms.GroupBox();
			this.radioL = new System.Windows.Forms.RadioButton();
			this.radioU = new System.Windows.Forms.RadioButton();
			this.groupSextant = new System.Windows.Forms.GroupBox();
			this.radioS6 = new System.Windows.Forms.RadioButton();
			this.radioS5 = new System.Windows.Forms.RadioButton();
			this.radioS4 = new System.Windows.Forms.RadioButton();
			this.radioS2 = new System.Windows.Forms.RadioButton();
			this.radioS3 = new System.Windows.Forms.RadioButton();
			this.radioS1 = new System.Windows.Forms.RadioButton();
			this.label9 = new System.Windows.Forms.Label();
			this.labelDx = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label27 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.listBoxTeeth = new System.Windows.Forms.ListBox();
			this.label12 = new System.Windows.Forms.Label();
			this.listBoxTeeth2 = new System.Windows.Forms.ListBox();
			this.groupMedical = new System.Windows.Forms.GroupBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.textTotal = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.textStop = new System.Windows.Forms.TextBox();
			this.textStart = new System.Windows.Forms.TextBox();
			this.textRevCode = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.textUnitQty = new System.Windows.Forms.TextBox();
			this.textUnitCode = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.textCodeMod4 = new System.Windows.Forms.TextBox();
			this.textCodeMod3 = new System.Windows.Forms.TextBox();
			this.textCodeMod2 = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.textCodeMod1 = new System.Windows.Forms.TextBox();
			this.checkIsPrincDiag = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textDiagnosticCode = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textMedicalCode = new System.Windows.Forms.TextBox();
			this.labelClaim = new System.Windows.Forms.Label();
			this.comboPlaceService = new System.Windows.Forms.ComboBox();
			this.labelPlaceService = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.checkNoBillIns = new System.Windows.Forms.CheckBox();
			this.groupProsth = new System.Windows.Forms.GroupBox();
			this.listProsth = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.labelClaimNote = new System.Windows.Forms.Label();
			this.labelIncomplete = new System.Windows.Forms.Label();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.listProcStatus = new System.Windows.Forms.ListBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.comboPriority = new System.Windows.Forms.ComboBox();
			this.comboDx = new System.Windows.Forms.ComboBox();
			this.comboProvNum = new System.Windows.Forms.ComboBox();
			this.textUser = new System.Windows.Forms.TextBox();
			this.comboBillingTypeTwo = new System.Windows.Forms.ComboBox();
			this.labelBillingTypeTwo = new System.Windows.Forms.Label();
			this.comboBillingTypeOne = new System.Windows.Forms.ComboBox();
			this.labelBillingTypeOne = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.labelInvalidSig = new System.Windows.Forms.Label();
			this.textSite = new System.Windows.Forms.TextBox();
			this.labelSite = new System.Windows.Forms.Label();
			this.gridIns = new OpenDental.UI.ODGrid();
			this.butShowMedical = new OpenDental.UI.Button();
			this.butPickSite = new OpenDental.UI.Button();
			this.butTopazSign = new OpenDental.UI.Button();
			this.buttonUseAutoNote = new OpenDental.UI.Button();
			this.sigBox = new OpenDental.UI.SignatureBox();
			this.butClearSig = new OpenDental.UI.Button();
			this.textDateOriginalProsth = new OpenDental.ValidDate();
			this.textNotes = new OpenDental.ODtextBox();
			this.butAddAdjust = new OpenDental.UI.Button();
			this.textClaimNote = new OpenDental.ODtextBox();
			this.butAddEstimate = new OpenDental.UI.Button();
			this.butSetComplete = new OpenDental.UI.Button();
			this.butEditAnyway = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.tbPay = new OpenDental.TableProcPay();
			this.tbAdj = new OpenDental.TableProcAdj();
			this.butD = new OpenDental.UI.Button();
			this.butBF = new OpenDental.UI.Button();
			this.butL = new OpenDental.UI.Button();
			this.butM = new OpenDental.UI.Button();
			this.butV = new OpenDental.UI.Button();
			this.butOI = new OpenDental.UI.Button();
			this.textDateTP = new OpenDental.ValidDate();
			this.textDateEntry = new OpenDental.ValidDate();
			this.textDate = new OpenDental.ValidDate();
			this.textProcFee = new OpenDental.ValidDouble();
			this.butChange = new OpenDental.UI.Button();
			this.groupQuadrant.SuspendLayout();
			this.panelSurfaces.SuspendLayout();
			this.groupArch.SuspendLayout();
			this.groupSextant.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupMedical.SuspendLayout();
			this.groupProsth.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8,44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96,14);
			this.label1.TabIndex = 0;
			this.label1.Text = "Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(26,63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79,12);
			this.label2.TabIndex = 1;
			this.label2.Text = "Procedure";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelTooth
			// 
			this.labelTooth.Location = new System.Drawing.Point(68,107);
			this.labelTooth.Name = "labelTooth";
			this.labelTooth.Size = new System.Drawing.Size(36,12);
			this.labelTooth.TabIndex = 2;
			this.labelTooth.Text = "Tooth";
			this.labelTooth.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelTooth.Visible = false;
			// 
			// labelSurfaces
			// 
			this.labelSurfaces.Location = new System.Drawing.Point(33,135);
			this.labelSurfaces.Name = "labelSurfaces";
			this.labelSurfaces.Size = new System.Drawing.Size(73,16);
			this.labelSurfaces.TabIndex = 3;
			this.labelSurfaces.Text = "Surfaces";
			this.labelSurfaces.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelSurfaces.Visible = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(30,158);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(75,16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textProc
			// 
			this.textProc.Location = new System.Drawing.Point(106,61);
			this.textProc.Name = "textProc";
			this.textProc.ReadOnly = true;
			this.textProc.Size = new System.Drawing.Size(76,20);
			this.textProc.TabIndex = 6;
			// 
			// textTooth
			// 
			this.textTooth.Location = new System.Drawing.Point(106,105);
			this.textTooth.Name = "textTooth";
			this.textTooth.Size = new System.Drawing.Size(35,20);
			this.textTooth.TabIndex = 7;
			this.textTooth.Visible = false;
			this.textTooth.Validating += new System.ComponentModel.CancelEventHandler(this.textTooth_Validating);
			// 
			// textSurfaces
			// 
			this.textSurfaces.Location = new System.Drawing.Point(106,133);
			this.textSurfaces.Name = "textSurfaces";
			this.textSurfaces.Size = new System.Drawing.Size(68,20);
			this.textSurfaces.TabIndex = 4;
			this.textSurfaces.Visible = false;
			this.textSurfaces.TextChanged += new System.EventHandler(this.textSurfaces_TextChanged);
			this.textSurfaces.Validating += new System.ComponentModel.CancelEventHandler(this.textSurfaces_Validating);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(0,81);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(105,16);
			this.label6.TabIndex = 13;
			this.label6.Text = "Description";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDesc
			// 
			this.textDesc.BackColor = System.Drawing.SystemColors.Control;
			this.textDesc.Location = new System.Drawing.Point(106,81);
			this.textDesc.Name = "textDesc";
			this.textDesc.Size = new System.Drawing.Size(216,20);
			this.textDesc.TabIndex = 16;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(429,167);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73,16);
			this.label7.TabIndex = 0;
			this.label7.Text = "&Notes";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelRange
			// 
			this.labelRange.Location = new System.Drawing.Point(24,107);
			this.labelRange.Name = "labelRange";
			this.labelRange.Size = new System.Drawing.Size(82,16);
			this.labelRange.TabIndex = 33;
			this.labelRange.Text = "Tooth Range";
			this.labelRange.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelRange.Visible = false;
			// 
			// textRange
			// 
			this.textRange.Location = new System.Drawing.Point(106,105);
			this.textRange.Name = "textRange";
			this.textRange.Size = new System.Drawing.Size(100,20);
			this.textRange.TabIndex = 34;
			this.textRange.Visible = false;
			// 
			// groupQuadrant
			// 
			this.groupQuadrant.Controls.Add(this.radioLR);
			this.groupQuadrant.Controls.Add(this.radioLL);
			this.groupQuadrant.Controls.Add(this.radioUL);
			this.groupQuadrant.Controls.Add(this.radioUR);
			this.groupQuadrant.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupQuadrant.Location = new System.Drawing.Point(104,99);
			this.groupQuadrant.Name = "groupQuadrant";
			this.groupQuadrant.Size = new System.Drawing.Size(108,56);
			this.groupQuadrant.TabIndex = 36;
			this.groupQuadrant.TabStop = false;
			this.groupQuadrant.Text = "Quadrant";
			this.groupQuadrant.Visible = false;
			// 
			// radioLR
			// 
			this.radioLR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioLR.Location = new System.Drawing.Point(12,36);
			this.radioLR.Name = "radioLR";
			this.radioLR.Size = new System.Drawing.Size(40,16);
			this.radioLR.TabIndex = 3;
			this.radioLR.Text = "LR";
			this.radioLR.Click += new System.EventHandler(this.radioLR_Click);
			// 
			// radioLL
			// 
			this.radioLL.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioLL.Location = new System.Drawing.Point(64,36);
			this.radioLL.Name = "radioLL";
			this.radioLL.Size = new System.Drawing.Size(40,16);
			this.radioLL.TabIndex = 1;
			this.radioLL.Text = "LL";
			this.radioLL.Click += new System.EventHandler(this.radioLL_Click);
			// 
			// radioUL
			// 
			this.radioUL.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUL.Location = new System.Drawing.Point(64,16);
			this.radioUL.Name = "radioUL";
			this.radioUL.Size = new System.Drawing.Size(40,16);
			this.radioUL.TabIndex = 0;
			this.radioUL.Text = "UL";
			this.radioUL.Click += new System.EventHandler(this.radioUL_Click);
			// 
			// radioUR
			// 
			this.radioUR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUR.Location = new System.Drawing.Point(12,16);
			this.radioUR.Name = "radioUR";
			this.radioUR.Size = new System.Drawing.Size(40,16);
			this.radioUR.TabIndex = 0;
			this.radioUR.Text = "UR";
			this.radioUR.Click += new System.EventHandler(this.radioUR_Click);
			// 
			// panelSurfaces
			// 
			this.panelSurfaces.Controls.Add(this.butD);
			this.panelSurfaces.Controls.Add(this.butBF);
			this.panelSurfaces.Controls.Add(this.butL);
			this.panelSurfaces.Controls.Add(this.butM);
			this.panelSurfaces.Controls.Add(this.butV);
			this.panelSurfaces.Controls.Add(this.butOI);
			this.panelSurfaces.Location = new System.Drawing.Point(188,106);
			this.panelSurfaces.Name = "panelSurfaces";
			this.panelSurfaces.Size = new System.Drawing.Size(96,66);
			this.panelSurfaces.TabIndex = 100;
			this.panelSurfaces.Visible = false;
			// 
			// groupArch
			// 
			this.groupArch.Controls.Add(this.radioL);
			this.groupArch.Controls.Add(this.radioU);
			this.groupArch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupArch.Location = new System.Drawing.Point(104,99);
			this.groupArch.Name = "groupArch";
			this.groupArch.Size = new System.Drawing.Size(60,56);
			this.groupArch.TabIndex = 3;
			this.groupArch.TabStop = false;
			this.groupArch.Text = "Arch";
			this.groupArch.Visible = false;
			// 
			// radioL
			// 
			this.radioL.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioL.Location = new System.Drawing.Point(12,36);
			this.radioL.Name = "radioL";
			this.radioL.Size = new System.Drawing.Size(28,16);
			this.radioL.TabIndex = 1;
			this.radioL.Text = "L";
			this.radioL.Click += new System.EventHandler(this.radioL_Click);
			// 
			// radioU
			// 
			this.radioU.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioU.Location = new System.Drawing.Point(12,16);
			this.radioU.Name = "radioU";
			this.radioU.Size = new System.Drawing.Size(32,16);
			this.radioU.TabIndex = 0;
			this.radioU.Text = "U";
			this.radioU.Click += new System.EventHandler(this.radioU_Click);
			// 
			// groupSextant
			// 
			this.groupSextant.Controls.Add(this.radioS6);
			this.groupSextant.Controls.Add(this.radioS5);
			this.groupSextant.Controls.Add(this.radioS4);
			this.groupSextant.Controls.Add(this.radioS2);
			this.groupSextant.Controls.Add(this.radioS3);
			this.groupSextant.Controls.Add(this.radioS1);
			this.groupSextant.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupSextant.Location = new System.Drawing.Point(104,99);
			this.groupSextant.Name = "groupSextant";
			this.groupSextant.Size = new System.Drawing.Size(156,56);
			this.groupSextant.TabIndex = 5;
			this.groupSextant.TabStop = false;
			this.groupSextant.Text = "Sextant";
			this.groupSextant.Visible = false;
			// 
			// radioS6
			// 
			this.radioS6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS6.Location = new System.Drawing.Point(12,36);
			this.radioS6.Name = "radioS6";
			this.radioS6.Size = new System.Drawing.Size(36,16);
			this.radioS6.TabIndex = 5;
			this.radioS6.Text = "6";
			this.radioS6.Click += new System.EventHandler(this.radioS6_Click);
			// 
			// radioS5
			// 
			this.radioS5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS5.Location = new System.Drawing.Point(60,36);
			this.radioS5.Name = "radioS5";
			this.radioS5.Size = new System.Drawing.Size(36,16);
			this.radioS5.TabIndex = 4;
			this.radioS5.Text = "5";
			this.radioS5.Click += new System.EventHandler(this.radioS5_Click);
			// 
			// radioS4
			// 
			this.radioS4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS4.Location = new System.Drawing.Point(108,36);
			this.radioS4.Name = "radioS4";
			this.radioS4.Size = new System.Drawing.Size(36,16);
			this.radioS4.TabIndex = 1;
			this.radioS4.Text = "4";
			this.radioS4.Click += new System.EventHandler(this.radioS4_Click);
			// 
			// radioS2
			// 
			this.radioS2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS2.Location = new System.Drawing.Point(60,16);
			this.radioS2.Name = "radioS2";
			this.radioS2.Size = new System.Drawing.Size(36,16);
			this.radioS2.TabIndex = 2;
			this.radioS2.Text = "2";
			this.radioS2.Click += new System.EventHandler(this.radioS2_Click);
			// 
			// radioS3
			// 
			this.radioS3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS3.Location = new System.Drawing.Point(108,16);
			this.radioS3.Name = "radioS3";
			this.radioS3.Size = new System.Drawing.Size(36,16);
			this.radioS3.TabIndex = 0;
			this.radioS3.Text = "3";
			this.radioS3.Click += new System.EventHandler(this.radioS3_Click);
			// 
			// radioS1
			// 
			this.radioS1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS1.Location = new System.Drawing.Point(12,16);
			this.radioS1.Name = "radioS1";
			this.radioS1.Size = new System.Drawing.Size(36,16);
			this.radioS1.TabIndex = 0;
			this.radioS1.Text = "1";
			this.radioS1.Click += new System.EventHandler(this.radioS1_Click);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(5,181);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100,14);
			this.label9.TabIndex = 45;
			this.label9.Text = "Provider";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelDx
			// 
			this.labelDx.Location = new System.Drawing.Point(5,202);
			this.labelDx.Name = "labelDx";
			this.labelDx.Size = new System.Drawing.Size(100,14);
			this.labelDx.TabIndex = 46;
			this.labelDx.Text = "Diagnosis";
			this.labelDx.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel1
			// 
			this.panel1.AllowDrop = true;
			this.panel1.Controls.Add(this.panelSurfaces);
			this.panel1.Controls.Add(this.textDateTP);
			this.panel1.Controls.Add(this.label27);
			this.panel1.Controls.Add(this.label26);
			this.panel1.Controls.Add(this.listBoxTeeth);
			this.panel1.Controls.Add(this.textDesc);
			this.panel1.Controls.Add(this.textDateEntry);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.labelTooth);
			this.panel1.Controls.Add(this.labelSurfaces);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.textSurfaces);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.groupArch);
			this.panel1.Controls.Add(this.textDate);
			this.panel1.Controls.Add(this.groupQuadrant);
			this.panel1.Controls.Add(this.textProcFee);
			this.panel1.Controls.Add(this.textTooth);
			this.panel1.Controls.Add(this.labelRange);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.textProc);
			this.panel1.Controls.Add(this.listBoxTeeth2);
			this.panel1.Controls.Add(this.textRange);
			this.panel1.Controls.Add(this.butChange);
			this.panel1.Controls.Add(this.groupSextant);
			this.panel1.Location = new System.Drawing.Point(0,0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(383,177);
			this.panel1.TabIndex = 2;
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(34,25);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(70,14);
			this.label27.TabIndex = 98;
			this.label27.Text = "Date TP";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(184,3);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(125,18);
			this.label26.TabIndex = 97;
			this.label26.Text = "(for security)";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// listBoxTeeth
			// 
			this.listBoxTeeth.AllowDrop = true;
			this.listBoxTeeth.ColumnWidth = 16;
			this.listBoxTeeth.Font = new System.Drawing.Font("Microsoft Sans Serif",8F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.listBoxTeeth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
			this.listBoxTeeth.Location = new System.Drawing.Point(106,101);
			this.listBoxTeeth.MultiColumn = true;
			this.listBoxTeeth.Name = "listBoxTeeth";
			this.listBoxTeeth.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.listBoxTeeth.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBoxTeeth.Size = new System.Drawing.Size(272,17);
			this.listBoxTeeth.TabIndex = 1;
			this.listBoxTeeth.Visible = false;
			this.listBoxTeeth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxTeeth_MouseDown);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(-19,3);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(125,18);
			this.label12.TabIndex = 96;
			this.label12.Text = "Date Entry";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listBoxTeeth2
			// 
			this.listBoxTeeth2.ColumnWidth = 16;
			this.listBoxTeeth2.Items.AddRange(new object[] {
            "32",
            "31",
            "30",
            "29",
            "28",
            "27",
            "26",
            "25",
            "24",
            "23",
            "22",
            "21",
            "20",
            "19",
            "18",
            "17"});
			this.listBoxTeeth2.Location = new System.Drawing.Point(106,115);
			this.listBoxTeeth2.MultiColumn = true;
			this.listBoxTeeth2.Name = "listBoxTeeth2";
			this.listBoxTeeth2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBoxTeeth2.Size = new System.Drawing.Size(272,17);
			this.listBoxTeeth2.TabIndex = 2;
			this.listBoxTeeth2.Visible = false;
			this.listBoxTeeth2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxTeeth2_MouseDown);
			// 
			// groupMedical
			// 
			this.groupMedical.Controls.Add(this.butShowMedical);
			this.groupMedical.Controls.Add(this.label18);
			this.groupMedical.Controls.Add(this.label17);
			this.groupMedical.Controls.Add(this.textTotal);
			this.groupMedical.Controls.Add(this.label25);
			this.groupMedical.Controls.Add(this.label24);
			this.groupMedical.Controls.Add(this.label23);
			this.groupMedical.Controls.Add(this.textStop);
			this.groupMedical.Controls.Add(this.textStart);
			this.groupMedical.Controls.Add(this.textRevCode);
			this.groupMedical.Controls.Add(this.label22);
			this.groupMedical.Controls.Add(this.textUnitQty);
			this.groupMedical.Controls.Add(this.textUnitCode);
			this.groupMedical.Controls.Add(this.label21);
			this.groupMedical.Controls.Add(this.label20);
			this.groupMedical.Controls.Add(this.textCodeMod4);
			this.groupMedical.Controls.Add(this.textCodeMod3);
			this.groupMedical.Controls.Add(this.textCodeMod2);
			this.groupMedical.Controls.Add(this.label19);
			this.groupMedical.Controls.Add(this.textCodeMod1);
			this.groupMedical.Controls.Add(this.checkIsPrincDiag);
			this.groupMedical.Controls.Add(this.label11);
			this.groupMedical.Controls.Add(this.textDiagnosticCode);
			this.groupMedical.Controls.Add(this.label8);
			this.groupMedical.Controls.Add(this.textMedicalCode);
			this.groupMedical.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupMedical.Location = new System.Drawing.Point(400,1);
			this.groupMedical.Name = "groupMedical";
			this.groupMedical.Size = new System.Drawing.Size(321,98);
			this.groupMedical.TabIndex = 97;
			this.groupMedical.TabStop = false;
			this.groupMedical.Text = "Medical";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(146,182);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(152,16);
			this.label18.TabIndex = 120;
			this.label18.Text = "Examples: 910,1430";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(146,164);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(152,16);
			this.label17.TabIndex = 119;
			this.label17.Text = "All time in military format";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textTotal
			// 
			this.textTotal.Location = new System.Drawing.Point(103,202);
			this.textTotal.Name = "textTotal";
			this.textTotal.ReadOnly = true;
			this.textTotal.Size = new System.Drawing.Size(36,20);
			this.textTotal.TabIndex = 118;
			this.toolTip1.SetToolTip(this.textTotal,"Total Minutes");
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(24,204);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(78,16);
			this.label25.TabIndex = 117;
			this.label25.Text = "Total Time";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(37,184);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(65,16);
			this.label24.TabIndex = 116;
			this.label24.Text = "Stop Time";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(37,163);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(65,16);
			this.label23.TabIndex = 115;
			this.label23.Text = "Start Time";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textStop
			// 
			this.textStop.Location = new System.Drawing.Point(103,181);
			this.textStop.MaxLength = 4;
			this.textStop.Name = "textStop";
			this.textStop.Size = new System.Drawing.Size(38,20);
			this.textStop.TabIndex = 114;
			this.toolTip1.SetToolTip(this.textStop,"Military time with no colon.");
			this.textStop.TextChanged += new System.EventHandler(this.textStop_TextChanged);
			// 
			// textStart
			// 
			this.textStart.Location = new System.Drawing.Point(103,160);
			this.textStart.MaxLength = 4;
			this.textStart.Name = "textStart";
			this.textStart.Size = new System.Drawing.Size(38,20);
			this.textStart.TabIndex = 113;
			this.toolTip1.SetToolTip(this.textStart,"Military time with no colon.");
			this.textStart.TextChanged += new System.EventHandler(this.textStart_TextChanged);
			// 
			// textRevCode
			// 
			this.textRevCode.Location = new System.Drawing.Point(103,139);
			this.textRevCode.MaxLength = 48;
			this.textRevCode.Name = "textRevCode";
			this.textRevCode.Size = new System.Drawing.Size(59,20);
			this.textRevCode.TabIndex = 112;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(6,141);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(96,17);
			this.label22.TabIndex = 111;
			this.label22.Text = "Revenue Code";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textUnitQty
			// 
			this.textUnitQty.Location = new System.Drawing.Point(103,97);
			this.textUnitQty.MaxLength = 15;
			this.textUnitQty.Name = "textUnitQty";
			this.textUnitQty.Size = new System.Drawing.Size(29,20);
			this.textUnitQty.TabIndex = 110;
			// 
			// textUnitCode
			// 
			this.textUnitCode.Location = new System.Drawing.Point(103,118);
			this.textUnitCode.MaxLength = 2;
			this.textUnitCode.Name = "textUnitCode";
			this.textUnitCode.Size = new System.Drawing.Size(29,20);
			this.textUnitCode.TabIndex = 109;
			this.textUnitCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(17,99);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(85,17);
			this.label21.TabIndex = 108;
			this.label21.Text = "Unit Quantity";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(29,120);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(73,17);
			this.label20.TabIndex = 107;
			this.label20.Text = "Unit Code";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCodeMod4
			// 
			this.textCodeMod4.Location = new System.Drawing.Point(193,76);
			this.textCodeMod4.MaxLength = 2;
			this.textCodeMod4.Name = "textCodeMod4";
			this.textCodeMod4.Size = new System.Drawing.Size(29,20);
			this.textCodeMod4.TabIndex = 106;
			this.textCodeMod4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCodeMod3
			// 
			this.textCodeMod3.Location = new System.Drawing.Point(163,76);
			this.textCodeMod3.MaxLength = 2;
			this.textCodeMod3.Name = "textCodeMod3";
			this.textCodeMod3.Size = new System.Drawing.Size(29,20);
			this.textCodeMod3.TabIndex = 105;
			this.textCodeMod3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCodeMod2
			// 
			this.textCodeMod2.Location = new System.Drawing.Point(133,76);
			this.textCodeMod2.MaxLength = 2;
			this.textCodeMod2.Name = "textCodeMod2";
			this.textCodeMod2.Size = new System.Drawing.Size(29,20);
			this.textCodeMod2.TabIndex = 104;
			this.textCodeMod2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(27,78);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(75,17);
			this.label19.TabIndex = 102;
			this.label19.Text = "Mods";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCodeMod1
			// 
			this.textCodeMod1.Location = new System.Drawing.Point(103,76);
			this.textCodeMod1.MaxLength = 2;
			this.textCodeMod1.Name = "textCodeMod1";
			this.textCodeMod1.Size = new System.Drawing.Size(29,20);
			this.textCodeMod1.TabIndex = 103;
			this.textCodeMod1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// checkIsPrincDiag
			// 
			this.checkIsPrincDiag.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsPrincDiag.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsPrincDiag.Location = new System.Drawing.Point(2,57);
			this.checkIsPrincDiag.Name = "checkIsPrincDiag";
			this.checkIsPrincDiag.Size = new System.Drawing.Size(114,18);
			this.checkIsPrincDiag.TabIndex = 101;
			this.checkIsPrincDiag.Text = "Principal Diagnosis";
			this.checkIsPrincDiag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8,37);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(94,16);
			this.label11.TabIndex = 99;
			this.label11.Text = "ICD-9 Code";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDiagnosticCode
			// 
			this.textDiagnosticCode.Location = new System.Drawing.Point(103,35);
			this.textDiagnosticCode.Name = "textDiagnosticCode";
			this.textDiagnosticCode.Size = new System.Drawing.Size(76,20);
			this.textDiagnosticCode.TabIndex = 100;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8,18);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(94,16);
			this.label8.TabIndex = 97;
			this.label8.Text = "Medical Code";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMedicalCode
			// 
			this.textMedicalCode.Location = new System.Drawing.Point(103,15);
			this.textMedicalCode.Name = "textMedicalCode";
			this.textMedicalCode.Size = new System.Drawing.Size(76,20);
			this.textMedicalCode.TabIndex = 98;
			// 
			// labelClaim
			// 
			this.labelClaim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelClaim.Font = new System.Drawing.Font("Microsoft Sans Serif",9F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelClaim.Location = new System.Drawing.Point(111,624);
			this.labelClaim.Name = "labelClaim";
			this.labelClaim.Size = new System.Drawing.Size(480,44);
			this.labelClaim.TabIndex = 50;
			this.labelClaim.Text = "This procedure is attached to a claim, so certain fields should not be edited.  Y" +
    "ou should reprint the claim if any significant changes are made.";
			this.labelClaim.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.labelClaim.Visible = false;
			// 
			// comboPlaceService
			// 
			this.comboPlaceService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPlaceService.Location = new System.Drawing.Point(106,240);
			this.comboPlaceService.MaxDropDownItems = 30;
			this.comboPlaceService.Name = "comboPlaceService";
			this.comboPlaceService.Size = new System.Drawing.Size(177,21);
			this.comboPlaceService.TabIndex = 6;
			// 
			// labelPlaceService
			// 
			this.labelPlaceService.Location = new System.Drawing.Point(-9,243);
			this.labelPlaceService.Name = "labelPlaceService";
			this.labelPlaceService.Size = new System.Drawing.Size(114,16);
			this.labelPlaceService.TabIndex = 53;
			this.labelPlaceService.Text = "Place of Service";
			this.labelPlaceService.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(32,223);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72,16);
			this.label10.TabIndex = 56;
			this.label10.Text = "Priority";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(856,62);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(103,42);
			this.label13.TabIndex = 58;
			this.label13.Text = "Also changes date and adds note.";
			// 
			// checkNoBillIns
			// 
			this.checkNoBillIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoBillIns.Location = new System.Drawing.Point(141,414);
			this.checkNoBillIns.Name = "checkNoBillIns";
			this.checkNoBillIns.Size = new System.Drawing.Size(152,18);
			this.checkNoBillIns.TabIndex = 9;
			this.checkNoBillIns.Text = "Do Not Bill to Ins";
			this.checkNoBillIns.ThreeState = true;
			this.checkNoBillIns.Click += new System.EventHandler(this.checkNoBillIns_Click);
			// 
			// groupProsth
			// 
			this.groupProsth.Controls.Add(this.listProsth);
			this.groupProsth.Controls.Add(this.textDateOriginalProsth);
			this.groupProsth.Controls.Add(this.label4);
			this.groupProsth.Controls.Add(this.label3);
			this.groupProsth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupProsth.Location = new System.Drawing.Point(15,281);
			this.groupProsth.Name = "groupProsth";
			this.groupProsth.Size = new System.Drawing.Size(275,80);
			this.groupProsth.TabIndex = 7;
			this.groupProsth.TabStop = false;
			this.groupProsth.Text = "Prosthesis Replacement";
			// 
			// listProsth
			// 
			this.listProsth.Location = new System.Drawing.Point(91,14);
			this.listProsth.Name = "listProsth";
			this.listProsth.Size = new System.Drawing.Size(163,43);
			this.listProsth.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(5,61);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84,16);
			this.label4.TabIndex = 4;
			this.label4.Text = "Original Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(2,14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90,41);
			this.label3.TabIndex = 0;
			this.label3.Text = "Crown, Bridge, Denture, or RPD";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelClaimNote
			// 
			this.labelClaimNote.Location = new System.Drawing.Point(0,362);
			this.labelClaimNote.Name = "labelClaimNote";
			this.labelClaimNote.Size = new System.Drawing.Size(104,41);
			this.labelClaimNote.TabIndex = 65;
			this.labelClaimNote.Text = "E-claim Note (keep it very short)";
			this.labelClaimNote.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelIncomplete
			// 
			this.labelIncomplete.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelIncomplete.ForeColor = System.Drawing.Color.DarkRed;
			this.labelIncomplete.Location = new System.Drawing.Point(379,185);
			this.labelIncomplete.Name = "labelIncomplete";
			this.labelIncomplete.Size = new System.Drawing.Size(123,18);
			this.labelIncomplete.TabIndex = 73;
			this.labelIncomplete.Text = "Incomplete";
			this.labelIncomplete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(106,261);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(177,21);
			this.comboClinic.TabIndex = 74;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(-10,263);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(114,16);
			this.labelClinic.TabIndex = 75;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listProcStatus
			// 
			this.listProcStatus.FormattingEnabled = true;
			this.listProcStatus.Location = new System.Drawing.Point(730,20);
			this.listProcStatus.Name = "listProcStatus";
			this.listProcStatus.Size = new System.Drawing.Size(120,95);
			this.listProcStatus.TabIndex = 76;
			this.listProcStatus.Click += new System.EventHandler(this.listProcStatus_Click);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(727,1);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(133,16);
			this.label14.TabIndex = 77;
			this.label14.Text = "Procedure Status";
			this.label14.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(389,349);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(110,23);
			this.label15.TabIndex = 79;
			this.label15.Text = "Signature / Initials";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(429,142);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(73,16);
			this.label16.TabIndex = 80;
			this.label16.Text = "User";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboPriority
			// 
			this.comboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPriority.Location = new System.Drawing.Point(106,219);
			this.comboPriority.MaxDropDownItems = 30;
			this.comboPriority.Name = "comboPriority";
			this.comboPriority.Size = new System.Drawing.Size(177,21);
			this.comboPriority.TabIndex = 98;
			// 
			// comboDx
			// 
			this.comboDx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDx.Location = new System.Drawing.Point(106,198);
			this.comboDx.MaxDropDownItems = 30;
			this.comboDx.Name = "comboDx";
			this.comboDx.Size = new System.Drawing.Size(177,21);
			this.comboDx.TabIndex = 99;
			// 
			// comboProvNum
			// 
			this.comboProvNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvNum.Location = new System.Drawing.Point(106,177);
			this.comboProvNum.MaxDropDownItems = 30;
			this.comboProvNum.Name = "comboProvNum";
			this.comboProvNum.Size = new System.Drawing.Size(177,21);
			this.comboProvNum.TabIndex = 100;
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(504,141);
			this.textUser.Name = "textUser";
			this.textUser.ReadOnly = true;
			this.textUser.Size = new System.Drawing.Size(119,20);
			this.textUser.TabIndex = 101;
			// 
			// comboBillingTypeTwo
			// 
			this.comboBillingTypeTwo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBillingTypeTwo.FormattingEnabled = true;
			this.comboBillingTypeTwo.Location = new System.Drawing.Point(504,120);
			this.comboBillingTypeTwo.MaxDropDownItems = 30;
			this.comboBillingTypeTwo.Name = "comboBillingTypeTwo";
			this.comboBillingTypeTwo.Size = new System.Drawing.Size(198,21);
			this.comboBillingTypeTwo.TabIndex = 102;
			// 
			// labelBillingTypeTwo
			// 
			this.labelBillingTypeTwo.Location = new System.Drawing.Point(385,122);
			this.labelBillingTypeTwo.Name = "labelBillingTypeTwo";
			this.labelBillingTypeTwo.Size = new System.Drawing.Size(117,16);
			this.labelBillingTypeTwo.TabIndex = 103;
			this.labelBillingTypeTwo.Text = "Billing Type 2";
			this.labelBillingTypeTwo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBillingTypeOne
			// 
			this.comboBillingTypeOne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBillingTypeOne.FormattingEnabled = true;
			this.comboBillingTypeOne.Location = new System.Drawing.Point(504,99);
			this.comboBillingTypeOne.MaxDropDownItems = 30;
			this.comboBillingTypeOne.Name = "comboBillingTypeOne";
			this.comboBillingTypeOne.Size = new System.Drawing.Size(198,21);
			this.comboBillingTypeOne.TabIndex = 104;
			// 
			// labelBillingTypeOne
			// 
			this.labelBillingTypeOne.Location = new System.Drawing.Point(385,101);
			this.labelBillingTypeOne.Name = "labelBillingTypeOne";
			this.labelBillingTypeOne.Size = new System.Drawing.Size(117,16);
			this.labelBillingTypeOne.TabIndex = 105;
			this.labelBillingTypeOne.Text = "Billing Type 1";
			this.labelBillingTypeOne.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelInvalidSig
			// 
			this.labelInvalidSig.BackColor = System.Drawing.SystemColors.Window;
			this.labelInvalidSig.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelInvalidSig.Location = new System.Drawing.Point(589,359);
			this.labelInvalidSig.Name = "labelInvalidSig";
			this.labelInvalidSig.Size = new System.Drawing.Size(196,59);
			this.labelInvalidSig.TabIndex = 109;
			this.labelInvalidSig.Text = "Invalid Signature -  Note or user has changed since it was signed.";
			this.labelInvalidSig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textSite
			// 
			this.textSite.AcceptsReturn = true;
			this.textSite.Location = new System.Drawing.Point(286,261);
			this.textSite.Name = "textSite";
			this.textSite.ReadOnly = true;
			this.textSite.Size = new System.Drawing.Size(153,20);
			this.textSite.TabIndex = 111;
			// 
			// labelSite
			// 
			this.labelSite.Location = new System.Drawing.Point(283,243);
			this.labelSite.Name = "labelSite";
			this.labelSite.Size = new System.Drawing.Size(114,17);
			this.labelSite.TabIndex = 110;
			this.labelSite.Text = "Site";
			this.labelSite.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// gridIns
			// 
			this.gridIns.HScrollVisible = false;
			this.gridIns.Location = new System.Drawing.Point(2,434);
			this.gridIns.Name = "gridIns";
			this.gridIns.ScrollValue = 0;
			this.gridIns.Size = new System.Drawing.Size(958,102);
			this.gridIns.TabIndex = 113;
			this.gridIns.Title = "Insurance Estimates and Payments";
			this.gridIns.TranslationName = "TableProcIns";
			this.gridIns.WrapText = false;
			this.gridIns.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridIns_CellDoubleClick);
			// 
			// butShowMedical
			// 
			this.butShowMedical.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShowMedical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butShowMedical.Autosize = true;
			this.butShowMedical.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowMedical.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowMedical.CornerRadius = 4F;
			this.butShowMedical.Location = new System.Drawing.Point(263,76);
			this.butShowMedical.Name = "butShowMedical";
			this.butShowMedical.Size = new System.Drawing.Size(58,21);
			this.butShowMedical.TabIndex = 121;
			this.butShowMedical.TabStop = false;
			this.butShowMedical.Text = "Show";
			this.butShowMedical.Click += new System.EventHandler(this.butShowMedical_Click);
			// 
			// butPickSite
			// 
			this.butPickSite.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPickSite.Autosize = true;
			this.butPickSite.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickSite.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickSite.CornerRadius = 4F;
			this.butPickSite.Location = new System.Drawing.Point(336,239);
			this.butPickSite.Name = "butPickSite";
			this.butPickSite.Size = new System.Drawing.Size(58,21);
			this.butPickSite.TabIndex = 112;
			this.butPickSite.TabStop = false;
			this.butPickSite.Text = "Pick";
			this.butPickSite.Click += new System.EventHandler(this.butPickSite_Click);
			// 
			// butTopazSign
			// 
			this.butTopazSign.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTopazSign.Autosize = true;
			this.butTopazSign.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTopazSign.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTopazSign.CornerRadius = 4F;
			this.butTopazSign.Location = new System.Drawing.Point(873,378);
			this.butTopazSign.Name = "butTopazSign";
			this.butTopazSign.Size = new System.Drawing.Size(81,24);
			this.butTopazSign.TabIndex = 108;
			this.butTopazSign.Text = "Sign Topaz";
			this.butTopazSign.UseVisualStyleBackColor = true;
			this.butTopazSign.Click += new System.EventHandler(this.butTopazSign_Click);
			// 
			// buttonUseAutoNote
			// 
			this.buttonUseAutoNote.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonUseAutoNote.Autosize = true;
			this.buttonUseAutoNote.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonUseAutoNote.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonUseAutoNote.CornerRadius = 4F;
			this.buttonUseAutoNote.Location = new System.Drawing.Point(873,132);
			this.buttonUseAutoNote.Name = "buttonUseAutoNote";
			this.buttonUseAutoNote.Size = new System.Drawing.Size(80,24);
			this.buttonUseAutoNote.TabIndex = 106;
			this.buttonUseAutoNote.Text = "Auto Note";
			this.buttonUseAutoNote.Click += new System.EventHandler(this.buttonUseAutoNote_Click);
			// 
			// sigBox
			// 
			this.sigBox.Location = new System.Drawing.Point(505,347);
			this.sigBox.Name = "sigBox";
			this.sigBox.Size = new System.Drawing.Size(362,79);
			this.sigBox.TabIndex = 86;
			this.sigBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sigBox_MouseUp);
			// 
			// butClearSig
			// 
			this.butClearSig.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClearSig.Autosize = true;
			this.butClearSig.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClearSig.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClearSig.CornerRadius = 4F;
			this.butClearSig.Location = new System.Drawing.Point(873,347);
			this.butClearSig.Name = "butClearSig";
			this.butClearSig.Size = new System.Drawing.Size(81,24);
			this.butClearSig.TabIndex = 85;
			this.butClearSig.Text = "Clear Sig";
			this.butClearSig.Click += new System.EventHandler(this.butClearSig_Click);
			// 
			// textDateOriginalProsth
			// 
			this.textDateOriginalProsth.Location = new System.Drawing.Point(91,58);
			this.textDateOriginalProsth.Name = "textDateOriginalProsth";
			this.textDateOriginalProsth.Size = new System.Drawing.Size(73,20);
			this.textDateOriginalProsth.TabIndex = 1;
			// 
			// textNotes
			// 
			this.textNotes.AcceptsReturn = true;
			this.textNotes.AcceptsTab = true;
			this.textNotes.Location = new System.Drawing.Point(504,161);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.Procedure;
			this.textNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNotes.Size = new System.Drawing.Size(450,180);
			this.textNotes.TabIndex = 1;
			this.textNotes.TextChanged += new System.EventHandler(this.textNotes_TextChanged);
			// 
			// butAddAdjust
			// 
			this.butAddAdjust.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddAdjust.Autosize = true;
			this.butAddAdjust.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddAdjust.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddAdjust.CornerRadius = 4F;
			this.butAddAdjust.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddAdjust.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddAdjust.Location = new System.Drawing.Point(466,538);
			this.butAddAdjust.Name = "butAddAdjust";
			this.butAddAdjust.Size = new System.Drawing.Size(126,24);
			this.butAddAdjust.TabIndex = 72;
			this.butAddAdjust.Text = "Add Adjustment";
			this.butAddAdjust.Click += new System.EventHandler(this.butAddAdjust_Click);
			// 
			// textClaimNote
			// 
			this.textClaimNote.AcceptsReturn = true;
			this.textClaimNote.Location = new System.Drawing.Point(106,362);
			this.textClaimNote.MaxLength = 80;
			this.textClaimNote.Multiline = true;
			this.textClaimNote.Name = "textClaimNote";
			this.textClaimNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Procedure;
			this.textClaimNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textClaimNote.Size = new System.Drawing.Size(277,43);
			this.textClaimNote.TabIndex = 10;
			// 
			// butAddEstimate
			// 
			this.butAddEstimate.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddEstimate.Autosize = true;
			this.butAddEstimate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddEstimate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddEstimate.CornerRadius = 4F;
			this.butAddEstimate.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddEstimate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddEstimate.Location = new System.Drawing.Point(2,408);
			this.butAddEstimate.Name = "butAddEstimate";
			this.butAddEstimate.Size = new System.Drawing.Size(111,24);
			this.butAddEstimate.TabIndex = 60;
			this.butAddEstimate.Text = "Add Estimate";
			this.butAddEstimate.Click += new System.EventHandler(this.butAddEstimate_Click);
			// 
			// butSetComplete
			// 
			this.butSetComplete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSetComplete.Autosize = true;
			this.butSetComplete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetComplete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetComplete.CornerRadius = 4F;
			this.butSetComplete.Location = new System.Drawing.Point(856,29);
			this.butSetComplete.Name = "butSetComplete";
			this.butSetComplete.Size = new System.Drawing.Size(91,24);
			this.butSetComplete.TabIndex = 54;
			this.butSetComplete.Text = "Set Complete";
			this.butSetComplete.Click += new System.EventHandler(this.butSetComplete_Click);
			// 
			// butEditAnyway
			// 
			this.butEditAnyway.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEditAnyway.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butEditAnyway.Autosize = true;
			this.butEditAnyway.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditAnyway.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditAnyway.CornerRadius = 4F;
			this.butEditAnyway.Location = new System.Drawing.Point(594,643);
			this.butEditAnyway.Name = "butEditAnyway";
			this.butEditAnyway.Size = new System.Drawing.Size(104,24);
			this.butEditAnyway.TabIndex = 51;
			this.butEditAnyway.Text = "&Edit Anyway";
			this.butEditAnyway.Visible = false;
			this.butEditAnyway.Click += new System.EventHandler(this.butEditAnyway_Click);
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
			this.butDelete.Location = new System.Drawing.Point(2,643);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(83,24);
			this.butDelete.TabIndex = 8;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(870,643);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76,24);
			this.butCancel.TabIndex = 13;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(779,643);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76,24);
			this.butOK.TabIndex = 12;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// tbPay
			// 
			this.tbPay.BackColor = System.Drawing.SystemColors.Window;
			this.tbPay.Location = new System.Drawing.Point(1,564);
			this.tbPay.Name = "tbPay";
			this.tbPay.ScrollValue = 33;
			this.tbPay.SelectedIndices = new int[0];
			this.tbPay.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbPay.Size = new System.Drawing.Size(449,72);
			this.tbPay.TabIndex = 71;
			this.tbPay.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.tbPay_CellDoubleClicked);
			// 
			// tbAdj
			// 
			this.tbAdj.BackColor = System.Drawing.SystemColors.Window;
			this.tbAdj.Location = new System.Drawing.Point(466,564);
			this.tbAdj.Name = "tbAdj";
			this.tbAdj.ScrollValue = 33;
			this.tbAdj.SelectedIndices = new int[0];
			this.tbAdj.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbAdj.Size = new System.Drawing.Size(494,72);
			this.tbAdj.TabIndex = 70;
			this.tbAdj.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.tbAdj_CellDoubleClicked);
			// 
			// butD
			// 
			this.butD.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butD.Autosize = true;
			this.butD.BackColor = System.Drawing.SystemColors.Control;
			this.butD.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butD.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butD.CornerRadius = 4F;
			this.butD.Location = new System.Drawing.Point(61,23);
			this.butD.Name = "butD";
			this.butD.Size = new System.Drawing.Size(24,20);
			this.butD.TabIndex = 27;
			this.butD.Text = "D";
			this.butD.UseVisualStyleBackColor = false;
			this.butD.Click += new System.EventHandler(this.butD_Click);
			// 
			// butBF
			// 
			this.butBF.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBF.Autosize = true;
			this.butBF.BackColor = System.Drawing.SystemColors.Control;
			this.butBF.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBF.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBF.CornerRadius = 4F;
			this.butBF.Location = new System.Drawing.Point(22,3);
			this.butBF.Name = "butBF";
			this.butBF.Size = new System.Drawing.Size(28,20);
			this.butBF.TabIndex = 28;
			this.butBF.Text = "B/F";
			this.butBF.UseVisualStyleBackColor = false;
			this.butBF.Click += new System.EventHandler(this.butBF_Click);
			// 
			// butL
			// 
			this.butL.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butL.Autosize = true;
			this.butL.BackColor = System.Drawing.SystemColors.Control;
			this.butL.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butL.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butL.CornerRadius = 4F;
			this.butL.Location = new System.Drawing.Point(32,43);
			this.butL.Name = "butL";
			this.butL.Size = new System.Drawing.Size(24,20);
			this.butL.TabIndex = 29;
			this.butL.Text = "L";
			this.butL.UseVisualStyleBackColor = false;
			this.butL.Click += new System.EventHandler(this.butL_Click);
			// 
			// butM
			// 
			this.butM.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butM.Autosize = true;
			this.butM.BackColor = System.Drawing.SystemColors.Control;
			this.butM.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butM.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butM.CornerRadius = 4F;
			this.butM.Location = new System.Drawing.Point(3,23);
			this.butM.Name = "butM";
			this.butM.Size = new System.Drawing.Size(24,20);
			this.butM.TabIndex = 25;
			this.butM.Text = "M";
			this.butM.UseVisualStyleBackColor = false;
			this.butM.Click += new System.EventHandler(this.butM_Click);
			// 
			// butV
			// 
			this.butV.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butV.Autosize = true;
			this.butV.BackColor = System.Drawing.SystemColors.Control;
			this.butV.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butV.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butV.CornerRadius = 4F;
			this.butV.Location = new System.Drawing.Point(50,3);
			this.butV.Name = "butV";
			this.butV.Size = new System.Drawing.Size(17,20);
			this.butV.TabIndex = 30;
			this.butV.Text = "V";
			this.butV.UseVisualStyleBackColor = false;
			this.butV.Click += new System.EventHandler(this.butV_Click);
			// 
			// butOI
			// 
			this.butOI.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOI.Autosize = true;
			this.butOI.BackColor = System.Drawing.SystemColors.Control;
			this.butOI.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOI.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOI.CornerRadius = 4F;
			this.butOI.Location = new System.Drawing.Point(27,23);
			this.butOI.Name = "butOI";
			this.butOI.Size = new System.Drawing.Size(34,20);
			this.butOI.TabIndex = 26;
			this.butOI.Text = "O/I";
			this.butOI.UseVisualStyleBackColor = false;
			this.butOI.Click += new System.EventHandler(this.butOI_Click);
			// 
			// textDateTP
			// 
			this.textDateTP.Location = new System.Drawing.Point(106,21);
			this.textDateTP.Name = "textDateTP";
			this.textDateTP.Size = new System.Drawing.Size(76,20);
			this.textDateTP.TabIndex = 99;
			// 
			// textDateEntry
			// 
			this.textDateEntry.Location = new System.Drawing.Point(106,1);
			this.textDateEntry.Name = "textDateEntry";
			this.textDateEntry.ReadOnly = true;
			this.textDateEntry.Size = new System.Drawing.Size(76,20);
			this.textDateEntry.TabIndex = 95;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(106,41);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(76,20);
			this.textDate.TabIndex = 0;
			// 
			// textProcFee
			// 
			this.textProcFee.Location = new System.Drawing.Point(106,155);
			this.textProcFee.Name = "textProcFee";
			this.textProcFee.Size = new System.Drawing.Size(68,20);
			this.textProcFee.TabIndex = 6;
			this.textProcFee.Validating += new System.ComponentModel.CancelEventHandler(this.textProcFee_Validating);
			// 
			// butChange
			// 
			this.butChange.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChange.Autosize = true;
			this.butChange.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChange.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChange.CornerRadius = 4F;
			this.butChange.Location = new System.Drawing.Point(184,57);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(74,24);
			this.butChange.TabIndex = 37;
			this.butChange.Text = "C&hange";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// FormProcEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(962,675);
			this.Controls.Add(this.gridIns);
			this.Controls.Add(this.groupMedical);
			this.Controls.Add(this.comboPlaceService);
			this.Controls.Add(this.butPickSite);
			this.Controls.Add(this.textSite);
			this.Controls.Add(this.labelSite);
			this.Controls.Add(this.labelInvalidSig);
			this.Controls.Add(this.butTopazSign);
			this.Controls.Add(this.buttonUseAutoNote);
			this.Controls.Add(this.comboBillingTypeOne);
			this.Controls.Add(this.labelBillingTypeOne);
			this.Controls.Add(this.comboBillingTypeTwo);
			this.Controls.Add(this.labelBillingTypeTwo);
			this.Controls.Add(this.textUser);
			this.Controls.Add(this.comboProvNum);
			this.Controls.Add(this.comboDx);
			this.Controls.Add(this.comboPriority);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.labelPlaceService);
			this.Controls.Add(this.labelDx);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.sigBox);
			this.Controls.Add(this.butClearSig);
			this.Controls.Add(this.groupProsth);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.labelIncomplete);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.listProcStatus);
			this.Controls.Add(this.butAddAdjust);
			this.Controls.Add(this.textClaimNote);
			this.Controls.Add(this.butAddEstimate);
			this.Controls.Add(this.butSetComplete);
			this.Controls.Add(this.butEditAnyway);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.tbPay);
			this.Controls.Add(this.tbAdj);
			this.Controls.Add(this.labelClaimNote);
			this.Controls.Add(this.checkNoBillIns);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.labelClaim);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Procedure Info";
			this.Load += new System.EventHandler(this.FormProcInfo_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProcEdit_FormClosing);
			this.groupQuadrant.ResumeLayout(false);
			this.panelSurfaces.ResumeLayout(false);
			this.groupArch.ResumeLayout(false);
			this.groupSextant.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupMedical.ResumeLayout(false);
			this.groupMedical.PerformLayout();
			this.groupProsth.ResumeLayout(false);
			this.groupProsth.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormProcInfo_Load(object sender, System.EventArgs e){
			//richTextBox1.Text="This is a test of the functions of a rich text box.";
			//webBrowser1.
			//richTextBox1.Select(10,4);
			//richTextBox1.SelectionFont=new Font(FontFamily.GenericMonospace,8);
			//richTextBox1.Select(22,9);
			//richTextBox1.SelectionFont=new Font(FontFamily.GenericMonospace,8,FontStyle.Underline);
			textDateEntry.Text=ProcCur.DateEntryC.ToShortDateString();
			if(PrefC.GetBool("EasyHidePublicHealth")){
				labelPlaceService.Visible=false;
				comboPlaceService.Visible=false;
				labelSite.Visible=false;
				textSite.Visible=false;
				butPickSite.Visible=false;
			}
			if(PrefC.GetInt("UseInternationalToothNumbers")==1){
				listBoxTeeth.Items.Clear();
				listBoxTeeth.Items.AddRange(new string[] {"18","17","16","15","14","13","12","11","21","22","23","24","25","26","27","28"});
				listBoxTeeth2.Items.Clear();
				listBoxTeeth2.Items.AddRange(new string[] {"48","47","46","45","44","43","42","41","31","32","33","34","35","36","37","38"});
			}
			if(PrefC.GetInt("UseInternationalToothNumbers")==3){
				listBoxTeeth.Items.Clear();
				listBoxTeeth.Items.AddRange(new string[] {"8","7","6","5","4","3","2","1","1","2","3","4","5","6","7","8"});
				listBoxTeeth2.Items.Clear();
				listBoxTeeth2.Items.AddRange(new string[] {"8","7","6","5","4","3","2","1","1","2","3","4","5","6","7","8"});
			}
			ClaimList=Claims.Refresh(PatCur.PatNum);
			ProcedureCode2=ProcedureCodes.GetProcCode(ProcCur.CodeNum);
			if(IsNew){
				if(ProcCur.ProcStatus==ProcStat.C){
					if(!Security.IsAuthorized(Permissions.ProcComplCreate)){
						DialogResult=DialogResult.Cancel;
						return;
					}
				}
				//SetControls();
				//return;
			}
			else{
				if(ProcCur.ProcStatus==ProcStat.C){
					if(!Security.IsAuthorized(Permissions.ProcComplEdit,ProcCur.DateEntryC)){
						butOK.Enabled=false;//use this state to cascade permission to any form openned from here
						butDelete.Enabled=false;
						butChange.Enabled=false;
						butEditAnyway.Enabled=false;
						butSetComplete.Enabled=false;
					}
				}
			}
			//ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			ClaimProcsForProc=ClaimProcs.RefreshForProc(ProcCur.ProcNum);
			PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			BenefitList=Benefits.Refresh(PatPlanList);
			if(Procedures.IsAttachedToClaim(ProcCur,ClaimProcsForProc)){
				StartedAttachedToClaim=true;
				//however, this doesn't stop someone from creating a claim while this window is open,
				//so this is checked at the end, too.
				panel1.Enabled=false;
				listProcStatus.Enabled=false;
				checkNoBillIns.Enabled=false;
				butChange.Enabled=false;
				butDelete.Enabled=false;
				butEditAnyway.Visible=true;
				labelClaim.Visible=true;
				butSetComplete.Enabled=false;
			}
			if(PrefC.GetBool("EasyHideClinical")){
				labelDx.Visible=false;
				comboDx.Visible=false;
			}
			if(PrefC.GetBool("EasyNoClinics")){
				comboClinic.Visible=false;
				labelClinic.Visible=false;
			}
			if(PrefC.GetBool("EasyHideMedicaid")) {
				comboBillingTypeOne.Visible=false;
				labelBillingTypeOne.Visible=false;
				comboBillingTypeTwo.Visible=false;
				labelBillingTypeTwo.Visible=false;
			}
			/*if(CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				textLabFee.Text=ProcCur.LabFee.ToString("n");
				textLabCode.Text=ProcCur.LabProcCode;
			}
			else{
				labelLabFee.Visible=false;
				textLabFee.Visible=false;
				labelLabCode.Visible=false;
				textLabCode.Visible=false;
			}*/
			IsStartingUp=true;
			FillControls();
			SetControls();
			FillIns(false);
			FillPayments();
			FillAdj();
			IsStartingUp=false;
		}		

		///<summary>Only run on startup. Fills the basic controls, except not the ones in the upper left panel which are handled in SetControls.</summary>
		private void FillControls(){
			listProcStatus.Items.Clear();
			listProcStatus.Items.Add(Lan.g(this,"Treatment Planned"));
			listProcStatus.Items.Add(Lan.g(this,"Complete"));
			if(!PrefC.GetBool("EasyHideClinical")) {
				listProcStatus.Items.Add(Lan.g(this,"Existing-Current Prov"));
				listProcStatus.Items.Add(Lan.g(this,"Existing-Other Prov"));
				listProcStatus.Items.Add(Lan.g(this,"Referred Out"));
			}
			listProcStatus.Items.Add(Lan.g(this,"Deleted"));
			listProcStatus.Items.Add(Lan.g(this,"Condition"));
			if(ProcCur.ProcStatus==ProcStat.TP){
				listProcStatus.SelectedIndex=0;
			}
			if(ProcCur.ProcStatus==ProcStat.C) {
				listProcStatus.SelectedIndex=1;
			}
			if(PrefC.GetBool("EasyHideClinical")) {
				if(ProcCur.ProcStatus==ProcStat.D) {
					listProcStatus.SelectedIndex=2;
				}
			}
			else{
				if(ProcCur.ProcStatus==ProcStat.EC) {
					listProcStatus.SelectedIndex=2;
				}
				if(ProcCur.ProcStatus==ProcStat.EO) {
					listProcStatus.SelectedIndex=3;
				}
				if(ProcCur.ProcStatus==ProcStat.R) {
					listProcStatus.SelectedIndex=4;
				}
				if(ProcCur.ProcStatus==ProcStat.D) {
					listProcStatus.SelectedIndex=5;
				}
				if(ProcCur.ProcStatus==ProcStat.Cn) {
					listProcStatus.SelectedIndex=6;
				}
			}
			if(ProcCur.ProcStatus==ProcStat.D){
				butOK.Enabled=false;
				butDelete.Enabled=false;
				butChange.Enabled=false;
				butEditAnyway.Enabled=false;
				butSetComplete.Enabled=false;
				butAddEstimate.Enabled=false;
				butAddAdjust.Enabled=false;
			}
			//if clinical is hidden, then there's a chance that no item is selected at this point.
			comboDx.Items.Clear();
			for(int i=0;i<DefC.Short[(int)DefCat.Diagnosis].Length;i++){
				comboDx.Items.Add(DefC.Short[(int)DefCat.Diagnosis][i].ItemName);
				if(DefC.Short[(int)DefCat.Diagnosis][i].DefNum==ProcCur.Dx)
					comboDx.SelectedIndex=i;
			}
			comboProvNum.Items.Clear();
			for(int i=0;i<ProviderC.List.Length;i++){
				comboProvNum.Items.Add(ProviderC.List[i].Abbr);
				if(ProviderC.List[i].ProvNum==ProcCur.ProvNum)
					comboProvNum.SelectedIndex=i;
			}
			comboPriority.Items.Clear();
			comboPriority.Items.Add(Lan.g(this,"no priority"));
			comboPriority.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.TxPriorities].Length;i++){
				comboPriority.Items.Add(DefC.Short[(int)DefCat.TxPriorities][i].ItemName);
				if(DefC.Short[(int)DefCat.TxPriorities][i].DefNum==ProcCur.Priority)
					comboPriority.SelectedIndex=i+1;
			}
			comboBillingTypeOne.Items.Clear();
			comboBillingTypeOne.Items.Add(Lan.g(this,"none"));
			comboBillingTypeOne.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.BillingTypes].Length;i++) {
				comboBillingTypeOne.Items.Add(DefC.Short[(int)DefCat.BillingTypes][i].ItemName);
				if(DefC.Short[(int)DefCat.BillingTypes][i].DefNum==ProcCur.BillingTypeOne)
					comboBillingTypeOne.SelectedIndex=i+1;
			}
			comboBillingTypeTwo.Items.Clear();
			comboBillingTypeTwo.Items.Add(Lan.g(this,"none"));
			comboBillingTypeTwo.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.BillingTypes].Length;i++) {
				comboBillingTypeTwo.Items.Add(DefC.Short[(int)DefCat.BillingTypes][i].ItemName);
				if(DefC.Short[(int)DefCat.BillingTypes][i].DefNum==ProcCur.BillingTypeTwo)
					comboBillingTypeTwo.SelectedIndex=i+1;
			}
			textNotes.Text=ProcCur.Note;
			textNotes.Select(textNotes.Text.Length,0);
			CheckForCompleteNote();
			comboPlaceService.Items.Clear();
			comboPlaceService.Items.AddRange(Enum.GetNames(typeof(PlaceOfService)));
			comboPlaceService.SelectedIndex=(int)ProcCur.PlaceService;
			//checkHideGraphical.Checked=ProcCur.HideGraphical;
			comboClinic.Items.Clear();
			comboClinic.Items.Add(Lan.g(this,"none"));
			comboClinic.SelectedIndex=0;
			for(int i=0;i<Clinics.List.Length;i++){
				comboClinic.Items.Add(Clinics.List[i].Description);
				if(Clinics.List[i].ClinicNum==ProcCur.ClinicNum){
					comboClinic.SelectedIndex=i+1;
				}
			}
			textSite.Text=Sites.GetDescription(ProcCur.SiteNum);
			if(ProcedureCode2.IsProsth){
				listProsth.Items.Add(Lan.g(this,"No"));
				listProsth.Items.Add(Lan.g(this,"Initial"));
				listProsth.Items.Add(Lan.g(this,"Replacement"));
				switch(ProcCur.Prosthesis){
					case "":
						listProsth.SelectedIndex=0;
						break;
					case "I":
						listProsth.SelectedIndex=1;
						break;
					case "R":
						listProsth.SelectedIndex=2;
						break;
				}
				if(ProcCur.DateOriginalProsth.Year>1880){
					textDateOriginalProsth.Text=ProcCur.DateOriginalProsth.ToShortDateString();
				}
			}
			else{
				groupProsth.Visible=false;
			}
			textClaimNote.Text=ProcCur.ClaimNote;
			textUser.Text=Userods.GetName(ProcCur.UserNum);//might be blank. Will change automatically if user changes note or alters sig.
			labelInvalidSig.Visible=false;
			sigBox.Visible=true;
			if(ProcCur.SigIsTopaz){
				if(ProcCur.Signature!=""){
					if(allowTopaz){
						sigBox.Visible=false;
						sigBoxTopaz.Visible=true;
						CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz);
						CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,0);
						CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,0);
						CodeBase.TopazWrapper.SetTopazKeyString(sigBoxTopaz,"0000000000000000");
						CodeBase.TopazWrapper.SetTopazAutoKeyData(sigBoxTopaz,ProcCur.Note+ProcCur.UserNum.ToString());
						CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,2);//high encryption
						CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,2);//high compression
						CodeBase.TopazWrapper.SetTopazSigString(sigBoxTopaz,ProcCur.Signature);
						if(CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(sigBoxTopaz)==0) {
							labelInvalidSig.Visible=true;
						}
					}
				}
			}
			else{
				if(ProcCur.Signature!=null && ProcCur.Signature!="") {
					sigBox.Visible=true;
					sigBoxTopaz.Visible=false;
					sigBox.ClearTablet();
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString("0000000000000000");
					sigBox.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
					//sigBox.SetEncryptionMode(2);//high encryption
					//sigBox.SetSigCompressionMode(2);//high compression
					sigBox.SetSigString(ProcCur.Signature);
					if(sigBox.NumberOfTabletPoints()==0) {
						labelInvalidSig.Visible=true;
					}
					sigBox.SetTabletState(0);//not accepting input.  To accept input, change the note, or clear the sig.
				}
			}
			
			/*if(ProcCur.DateLocked.Year>1880){//if locked
				butLock.Visible=false;
				//textNotes.ReadOnly=true;
				textNotes.Enabled=false;
				butDelete.Enabled=false;
				groupStatus.Enabled=false;
				butSetComplete.Enabled=false;
				textDateLocked.Text=ProcCur.DateLocked.ToShortDateString();
			}
			else{
				labelLocked.Visible=false;
				textDateLocked.Visible=false;
			}*/
		}

		private void SetSurfButtons(){
			if(textSurfaces.Text.Contains("B") | textSurfaces.Text.Contains("F")) butBF.BackColor=Color.White;
			if(textSurfaces.Text.Contains("O") | textSurfaces.Text.Contains("I")) butOI.BackColor=Color.White;
			if(textSurfaces.Text.Contains("M")) butM.BackColor=Color.White;
			if(textSurfaces.Text.Contains("D")) butD.BackColor=Color.White;
			if(textSurfaces.Text.Contains("L")) butL.BackColor=Color.White;
			if(textSurfaces.Text.Contains("V")) butV.BackColor=Color.White;
		}
		///<summary>Called on open and after changing code.  Sets the visibilities and the data of all the fields in the upper left panel.</summary>
		private void SetControls(){
			textDateTP.Text=ProcCur.DateTP.ToString("d");
			textDate.Text=ProcCur.ProcDate.ToString("d");
			textProc.Text=ProcedureCode2.ProcCode;
			textDesc.Text=ProcedureCode2.Descript;
			textMedicalCode.Text=ProcCur.MedicalCode;
			textDiagnosticCode.Text=ProcCur.DiagnosticCode;
			checkIsPrincDiag.Checked=ProcCur.IsPrincDiag;
			textCodeMod1.Text = ProcCur.CodeMod1;
			textCodeMod2.Text = ProcCur.CodeMod2;
			textCodeMod3.Text = ProcCur.CodeMod3;
			textCodeMod4.Text = ProcCur.CodeMod4;
			textRevCode.Text = ProcCur.RevCode;
			textUnitCode.Text = ProcCur.UnitCode;
			textUnitQty.Text = ProcCur.UnitQty.ToString();
			textStart.Text=ProcCur.StartTime.ToString();
			textStop.Text=ProcCur.StopTime.ToString();
			int timeTotal=ProcCur.StopTime-ProcCur.StartTime;
			textTotal.Text=timeTotal.ToString();
			switch (ProcedureCode2.TreatArea){
				case TreatmentArea.Surf:
					this.textTooth.Visible=true;
					this.labelTooth.Visible=true;
					this.textSurfaces.Visible=true;
					this.labelSurfaces.Visible=true;
					this.panelSurfaces.Visible=true;
					if(Tooth.IsValidDB(ProcCur.ToothNum)) {
						errorProvider2.SetError(textTooth,"");
						textTooth.Text=Tooth.ToInternat(ProcCur.ToothNum);
						textSurfaces.Text=Tooth.SurfTidy(ProcCur.Surf,ProcCur.ToothNum,false);
						SetSurfButtons();
					}
					else{
						errorProvider2.SetError(textTooth,Lan.g(this,"Invalid tooth number."));
						textTooth.Text=ProcCur.ToothNum;
						//textSurfaces.Text=Tooth.SurfTidy(ProcCur.Surf,"");//only valid toothnums allowed
					}
					if(textSurfaces.Text=="")
						errorProvider2.SetError(textSurfaces,"No surfaces selected.");
					else
						errorProvider2.SetError(textSurfaces,"");
					break;
				case TreatmentArea.Tooth:
					this.textTooth.Visible=true;
					this.labelTooth.Visible=true;
					if(Tooth.IsValidDB(ProcCur.ToothNum)){
						errorProvider2.SetError(textTooth,"");
						textTooth.Text=Tooth.ToInternat(ProcCur.ToothNum);
					}
					else{
						errorProvider2.SetError(textTooth,Lan.g(this,"Invalid tooth number."));
						textTooth.Text=ProcCur.ToothNum;
					}
					break;
				case TreatmentArea.Mouth:
						break;
				case TreatmentArea.Quad:
					this.groupQuadrant.Visible=true;
					switch (ProcCur.Surf){
						case "UR": this.radioUR.Checked=true; break;
						case "UL": this.radioUL.Checked=true; break;
						case "LR": this.radioLR.Checked=true; break;
						case "LL": this.radioLL.Checked=true; break;
						//default : 
					}
					break;
				case TreatmentArea.Sextant:
					this.groupSextant.Visible=true;
					switch (ProcCur.Surf){
						case "1": this.radioS1.Checked=true; break;
						case "2": this.radioS2.Checked=true; break;
						case "3": this.radioS3.Checked=true; break;
						case "4": this.radioS4.Checked=true; break;
						case "5": this.radioS5.Checked=true; break;
						case "6": this.radioS6.Checked=true; break;
						//default:
					}
					break;
				case TreatmentArea.Arch:
					this.groupArch.Visible=true;
					switch (ProcCur.Surf){
						case "U": this.radioU.Checked=true; break;
						case "L": this.radioL.Checked=true; break;
					}
					break;
				case TreatmentArea.ToothRange:
					this.labelRange.Visible=true;
					this.listBoxTeeth.Visible=true;
					this.listBoxTeeth2.Visible=true;
					listBoxTeeth.SelectionMode=SelectionMode.MultiExtended;
					listBoxTeeth2.SelectionMode=SelectionMode.MultiExtended;
					if(ProcCur.ToothRange==null){
						break;
					}
   			  string[] sArray=ProcCur.ToothRange.Split(',');//in american
					int idxAmer;
          for(int i=0;i<sArray.Length;i++)  {
						idxAmer=Array.IndexOf(Tooth.labelsUniversal,sArray[i]);
						if(idxAmer==-1){
							continue;
						}
						if(idxAmer<16){
							listBoxTeeth.SetSelected(idxAmer,true);
						}
						else if(idxAmer<32){//ignore anything after 32.
							listBoxTeeth2.SetSelected(idxAmer-16,true);
						}
            /*for(int j=0;j<listBoxTeeth.Items.Count;j++)  {
              if(Tooth.ToInternat(sArray[i])==listBoxTeeth.Items[j].ToString())
				 		    listBoxTeeth.SelectedItem=Tooth.ToInternat(sArray[i]);
					  }
  			    for(int j=0;j<listBoxTeeth2.Items.Count;j++)  {
              if(Tooth.ToInternat(sArray[i])==listBoxTeeth2.Items[j].ToString())
				 		    listBoxTeeth2.SelectedItem=Tooth.ToInternat(sArray[i]);
            }*/
					} 
					break;
			}//end switch
			textProcFee.Text=ProcCur.ProcFee.ToString("n");
		}//end SetControls

		private void FillIns(){
			FillIns(true);
		}

		private void FillIns(bool refreshClaimProcsFirst){
			if(refreshClaimProcsFirst) {
				//ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
				//}
				ClaimProcsForProc=ClaimProcs.RefreshForProc(ProcCur.ProcNum);
			}
			gridIns.BeginUpdate();
			gridIns.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableProcIns","Ins Plan"),190);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Pri/Sec"),50,HorizontalAlignment.Center);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Status"),50,HorizontalAlignment.Center);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","NoBill"),45,HorizontalAlignment.Center);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Copay"),55,HorizontalAlignment.Right);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Deduct"),55,HorizontalAlignment.Right);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Percent"),55,HorizontalAlignment.Center);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Ins Est"),55,HorizontalAlignment.Right);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Ins Pay"),55,HorizontalAlignment.Right);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","WriteOff"),55,HorizontalAlignment.Right);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Estimate Note"),100);
			gridIns.Columns.Add(col);		 
			col=new ODGridColumn(Lan.g("TableProcIns","Remarks"),165);
			gridIns.Columns.Add(col);		 
			gridIns.Rows.Clear();
			ODGridRow row;
			checkNoBillIns.CheckState=CheckState.Unchecked;
			bool allNoBillIns=true;
			InsPlan plan;
			ODGridCell cell;
			for(int i=0;i<ClaimProcsForProc.Count;i++) {
				if(ClaimProcsForProc[i].NoBillIns){
					checkNoBillIns.CheckState=CheckState.Indeterminate;
				}
				else{
					allNoBillIns=false;
				}
				row=new ODGridRow();
				
				row.Cells.Add(InsPlans.GetDescript(ClaimProcsForProc[i].PlanNum,FamCur,PlanList));
				plan=InsPlans.GetPlan(ClaimProcsForProc[i].PlanNum,PlanList);
				if(plan.IsMedical) {
					row.Cells.Add("Med");
				}
				else if(ClaimProcsForProc[i].PlanNum==PatPlans.GetPlanNum(PatPlanList,1)){
					row.Cells.Add("Pri");
				}
				else if(ClaimProcsForProc[i].PlanNum==PatPlans.GetPlanNum(PatPlanList,2)) {
					row.Cells.Add("Sec");
				}
				else {
					row.Cells.Add("");
				}
				switch(ClaimProcsForProc[i].Status) {
					case ClaimProcStatus.Received:
						row.Cells.Add("Recd");
						break;
					case ClaimProcStatus.NotReceived:
						row.Cells.Add("NotRec");
						break;
					//adjustment would never show here
					case ClaimProcStatus.Preauth:
						row.Cells.Add("PreA");
						break;
					case ClaimProcStatus.Supplemental:
						row.Cells.Add("Supp");
						break;
					case ClaimProcStatus.CapClaim:
						row.Cells.Add("CapClaim");
						break;
					case ClaimProcStatus.Estimate:
						row.Cells.Add("Est");
						break;
					case ClaimProcStatus.CapEstimate:
						row.Cells.Add("CapEst");
						break;
					case ClaimProcStatus.CapComplete:
						row.Cells.Add("CapComp");
						break;
					default:
						row.Cells.Add("");
						break;
				}
				if(ClaimProcsForProc[i].NoBillIns) {
					row.Cells.Add("X");
					if(ClaimProcsForProc[i].Status!=ClaimProcStatus.CapComplete	&& ClaimProcsForProc[i].Status!=ClaimProcStatus.CapEstimate) {
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						gridIns.Rows.Add(row);
						continue;
					}
				}
				else {
					row.Cells.Add("");
				}
				row.Cells.Add(ClaimProcs.GetCopayDisplay(ClaimProcsForProc[i]));
				double ded=ClaimProcs.GetDeductibleDisplay(ClaimProcsForProc[i]);
				if(ded>0) {
					row.Cells.Add(ded.ToString("n"));
				}
				else {
					row.Cells.Add("");
				}
				row.Cells.Add(ClaimProcs.GetPercentageDisplay(ClaimProcsForProc[i]));
				row.Cells.Add(ClaimProcs.GetEstimateDisplay(ClaimProcsForProc[i]));
				if(ClaimProcsForProc[i].Status==ClaimProcStatus.Estimate) {
					row.Cells.Add("");
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(ClaimProcsForProc[i].InsPayAmt.ToString("n"));
					row.Cells.Add(ClaimProcsForProc[i].WriteOff.ToString("n"));
				}
				row.Cells.Add(ClaimProcsForProc[i].EstimateNote);
				row.Cells.Add(ClaimProcsForProc[i].Remarks);			  
				gridIns.Rows.Add(row);
			}
			gridIns.EndUpdate();
			if(ClaimProcsForProc.Count==0) {
				checkNoBillIns.CheckState=CheckState.Unchecked;
			}
			else if(allNoBillIns) {
				checkNoBillIns.CheckState=CheckState.Checked;
			}
		}

		private void gridIns_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormClaimProc FormC=new FormClaimProc(ClaimProcsForProc[e.Row],ProcCur,FamCur,PatCur,PlanList,HistList,ref LoopList);
			if(!butOK.Enabled){
				FormC.NoPermissionProc=true;
			}
			FormC.ShowDialog();
			FillIns();
		}

		private void butAddEstimate_Click(object sender, System.EventArgs e) {
			FormInsPlanSelect FormIS=new FormInsPlanSelect(PatCur.PatNum);
			FormIS.ShowDialog();
			if(FormIS.DialogResult==DialogResult.Cancel){
				return;
			}
			List <Benefit> benList=Benefits.Refresh(PatPlanList);
			ClaimProc cp=new ClaimProc();
			ClaimProcs.CreateEst(cp,ProcCur,FormIS.SelectedPlan);
			if(FormIS.SelectedPlan.PlanNum==PatPlans.GetPlanNum(PatPlanList,1)){
				ClaimProcs.ComputeBaseEst(cp,ProcCur.ProcFee,ProcCur.ToothNum,ProcCur.CodeNum,FormIS.SelectedPlan,PatPlanList[0].PatPlanNum,benList,HistList,LoopList);
			}
			else if(FormIS.SelectedPlan.PlanNum==PatPlans.GetPlanNum(PatPlanList,2)){
				ClaimProcs.ComputeBaseEst(cp,ProcCur.ProcFee,ProcCur.ToothNum,ProcCur.CodeNum,FormIS.SelectedPlan,PatPlanList[1].PatPlanNum,benList,HistList,LoopList);
			}
			FormClaimProc FormC=new FormClaimProc(cp,ProcCur,FamCur,PatCur,PlanList,HistList,ref LoopList);
			//FormC.NoPermission not needed because butAddEstimate not enabled
			FormC.ShowDialog();
			if(FormC.DialogResult==DialogResult.Cancel){
				ClaimProcs.Delete(cp);
			}
			FillIns();
		}

		private void FillPayments(){
			PaySplit[] PaySplitList=PaySplits.Refresh(ProcCur.PatNum);
			PaySplitsForProc=PaySplits.GetForProc(ProcCur.ProcNum,PaySplitList);
			List<int> payNums=new List<int>();//[];
			for(int i=0;i<PaySplitsForProc.Count;i++) {
				payNums.Add(((PaySplit)PaySplitsForProc[i]).PayNum);
			}
			PaymentsForProc=Payments.GetPayments(payNums);
			tbPay.ResetRows(PaySplitsForProc.Count);
			Payment PaymentCur;//used in loop
			for(int i=0;i<PaySplitsForProc.Count;i++){
				tbPay.Cell[0,i]=((PaySplit)PaySplitsForProc[i]).DatePay.ToShortDateString();
				tbPay.Cell[1,i]=((PaySplit)PaySplitsForProc[i]).SplitAmt.ToString("F");
				tbPay.FontBold[1,i]=true;
				PaymentCur=Payments.GetFromList(((PaySplit)PaySplitsForProc[i]).PayNum,PaymentsForProc);
				tbPay.Cell[2,i]=PaymentCur.PayAmt.ToString("F");
				tbPay.Cell[3,i]=PaymentCur.PayNote;
			}
			tbPay.SetGridColor(Color.LightGray);
			tbPay.LayoutTables();
		}

		private void tbPay_CellDoubleClicked(object sender, OpenDental.CellEventArgs e) {
			Payment PaymentCur=Payments.GetFromList(((PaySplit)PaySplitsForProc[e.Row]).PayNum,PaymentsForProc);
			FormPayment FormP=new FormPayment(PatCur,FamCur,PaymentCur);
			FormP.InitialPaySplit=((PaySplit)PaySplitsForProc[e.Row]).SplitNum;
			FormP.ShowDialog();
			FillPayments();
		}

		private void FillAdj(){
			Adjustment[] AdjustmentList=Adjustments.Refresh(ProcCur.PatNum);
			AdjustmentsForProc=Adjustments.GetForProc(ProcCur.ProcNum,AdjustmentList);
			tbAdj.ResetRows(AdjustmentsForProc.Count);
			for(int i=0;i<AdjustmentsForProc.Count;i++){
				tbAdj.Cell[0,i]=((Adjustment)AdjustmentsForProc[i]).AdjDate.ToShortDateString();
				tbAdj.Cell[1,i]=((Adjustment)AdjustmentsForProc[i]).AdjAmt.ToString("F");
				tbAdj.FontBold[1,i]=true;
				tbAdj.Cell[2,i]=DefC.GetName(DefCat.AdjTypes,((Adjustment)AdjustmentsForProc[i]).AdjType);
				tbAdj.Cell[3,i]=((Adjustment)AdjustmentsForProc[i]).AdjNote;
			}
			tbAdj.SetGridColor(Color.LightGray);
			tbAdj.LayoutTables();
		}

		private void butAddAdjust_Click(object sender, System.EventArgs e) {
			if(ProcCur.ProcStatus!=ProcStat.C){
				MsgBox.Show(this,"Adjustments may only be added to completed procedures.");
				return;
			}
			Adjustment adj=new Adjustment();
			adj.PatNum=PatCur.PatNum;
			adj.ProvNum=ProcCur.ProvNum;
			adj.DateEntry=DateTime.Today;//but will get overwritten to server date
			adj.AdjDate=DateTime.Today;
			adj.ProcDate=ProcCur.ProcDate;
			adj.ProcNum=ProcCur.ProcNum;
			FormAdjust FormA=new FormAdjust(PatCur,adj);
			FormA.IsNew=true;
			FormA.ShowDialog();
			FillAdj();
		}

		private void tbAdj_CellDoubleClicked(object sender, OpenDental.CellEventArgs e) {
			FormAdjust FormA=new FormAdjust(PatCur,(Adjustment)AdjustmentsForProc[e.Row]);
			FormA.ShowDialog();
			FillAdj();
		}

		private void textProcFee_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(textProcFee.errorProvider1.GetError(textProcFee)!=""){
				return;
			}
			double procFee;
			if(textProcFee.Text==""){
				procFee=0;
			}
			else{
				procFee=PIn.PDouble(textProcFee.Text);
			}
			if(ProcCur.ProcFee==procFee){
				return;
			}
			ProcCur.ProcFee=procFee;
			Procedures.ComputeEstimates(ProcCur,PatCur.PatNum,ClaimProcsForProc,false,PlanList,PatPlanList,BenefitList);
			FillIns();
		}

		private void textTooth_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			textTooth.Text=textTooth.Text.ToUpper();
			if(!Tooth.IsValidEntry(textTooth.Text))
				errorProvider2.SetError(textTooth,Lan.g(this,"Invalid tooth number."));
			else
				errorProvider2.SetError(textTooth,"");
		}

		private void textSurfaces_TextChanged(object sender, System.EventArgs e) {
			int cursorPos = textSurfaces.SelectionStart;
			textSurfaces.Text=textSurfaces.Text.ToUpper();
			textSurfaces.SelectionStart=cursorPos;
		}

		private void textSurfaces_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(Tooth.IsValidEntry(textTooth.Text)){
				textSurfaces.Text=Tooth.SurfTidy(textSurfaces.Text,Tooth.FromInternat(textTooth.Text),false);
			}
			else{
				textSurfaces.Text=Tooth.SurfTidy(textSurfaces.Text,"",false);
			}
			if(textSurfaces.Text=="")
				errorProvider2.SetError(textSurfaces,"No surfaces selected.");
			else
				errorProvider2.SetError(textSurfaces,"");
		}

		private void listBoxTeeth2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
		  listBoxTeeth.SelectedIndex=-1;
		}

		private void listBoxTeeth_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
		  listBoxTeeth2.SelectedIndex=-1;
		}

		private void butChange_Click(object sender, System.EventArgs e) {
			FormProcCodes FormP=new FormProcCodes();
      FormP.IsSelectionMode=true;
      FormP.ShowDialog();
      if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
      ProcCur.CodeNum=FormP.SelectedCodeNum;
      ProcedureCode2=ProcedureCodes.GetProcCode(FormP.SelectedCodeNum);
      textDesc.Text=ProcedureCode2.Descript;
			int priPlanNum=PatPlans.GetPlanNum(PatPlanList,1);
			InsPlan priplan=InsPlans.GetPlan(priPlanNum,PlanList);//can handle a plannum=0
			double insfee=Fees.GetAmount0(ProcCur.CodeNum,Fees.GetFeeSched(PatCur,PlanList,PatPlanList));
			if(priplan!=null && priplan.PlanType=="p") {//PPO
				double standardfee=Fees.GetAmount0(ProcCur.CodeNum,Providers.GetProv(Patients.GetProvNum(PatCur)).FeeSched);
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
			switch(ProcedureCode2.TreatArea){ 
				case TreatmentArea.Quad:
					ProcCur.Surf="UR";
					radioUR.Checked=true;
					break;
				case TreatmentArea.Sextant:
					ProcCur.Surf="1";
					radioS1.Checked=true;
					break;
				case TreatmentArea.Arch:
					ProcCur.Surf="U";
					radioU.Checked=true;
					break;
			}
			Procedures.ComputeEstimates(ProcCur,PatCur.PatNum,ClaimProcsForProc,false,PlanList,PatPlanList,BenefitList);
			FillIns();
      SetControls();
		}

		private void butEditAnyway_Click(object sender, System.EventArgs e) {
			panel1.Enabled=true;
			listProcStatus.Enabled=true;
			checkNoBillIns.Enabled=true;
			butDelete.Enabled=true;
			butChange.Enabled=true;
			//checkIsCovIns.Enabled=true;
		}

		private void listProcStatus_Click(object sender,EventArgs e) {
			if(listProcStatus.SelectedIndex==0){
				ProcCur.ProcStatus=ProcStat.TP;
				//fee starts out 0 if EO, EC, etc.  This updates fee if changing to TP so it won't stay 0.
				if(ProcCur.ProcFee==0) {
					ProcCur.ProcFee=Fees.GetAmount0(ProcCur.CodeNum,Fees.GetFeeSched(PatCur,PlanList,PatPlanList));
					textProcFee.Text=ProcCur.ProcFee.ToString("f");
				}
			}
			if(listProcStatus.SelectedIndex==1){
				if(!Security.IsAuthorized(Permissions.ProcComplCreate)) {
					//set it back to whatever it was before
					if(ProcCur.ProcStatus==ProcStat.TP) {
						listProcStatus.SelectedIndex=0;
					}
					else if(PrefC.GetBool("EasyHideClinical")) {
						if(ProcCur.ProcStatus==ProcStat.D) {
							listProcStatus.SelectedIndex=2;
						}
						else{
							listProcStatus.SelectedIndex=-1;//original status must not be visible
						}
					}
					else {
						if(ProcCur.ProcStatus==ProcStat.EO) {
							listProcStatus.SelectedIndex=2;
						}
						if(ProcCur.ProcStatus==ProcStat.EC) {
							listProcStatus.SelectedIndex=3;
						}
						if(ProcCur.ProcStatus==ProcStat.R) {
							listProcStatus.SelectedIndex=4;
						}
						if(ProcCur.ProcStatus==ProcStat.D) {
							listProcStatus.SelectedIndex=5;
						}
						if(ProcCur.ProcStatus==ProcStat.Cn) {
							listProcStatus.SelectedIndex=6;
						}
					}
					return;
				}
				Procedures.SetDateFirstVisit(DateTime.Today,2,PatCur);
				ProcCur.ProcStatus=ProcStat.C;
			}
			if(listProcStatus.SelectedIndex==2) {
				if(PrefC.GetBool("EasyHideClinical")){
					ProcCur.ProcStatus=ProcStat.D;
				}
				else{
					ProcCur.ProcStatus=ProcStat.EC;
				}
			}
			if(listProcStatus.SelectedIndex==3) {
				ProcCur.ProcStatus=ProcStat.EO;
			}
			if(listProcStatus.SelectedIndex==4) {
				ProcCur.ProcStatus=ProcStat.R;
			}
			if(listProcStatus.SelectedIndex==5) {
				ProcCur.ProcStatus=ProcStat.D;
			}
			if(listProcStatus.SelectedIndex==6) {
				ProcCur.ProcStatus=ProcStat.Cn;
			}
		}

		private void butSetComplete_Click(object sender, System.EventArgs e) {
			//can't get to here if attached to a claim, even if use the Edit Anyway button.
			if(ProcOld.ProcStatus==ProcStat.C){
				MsgBox.Show(this,"Procedure was already set complete.");
				return;
			}
			if(!Security.IsAuthorized(Permissions.ProcComplCreate)){
				return;
			}
			Procedures.SetDateFirstVisit(DateTime.Today,2,PatCur);
			if(ProcCur.AptNum!=0){//if attached to an appointment
				textDate.Text=Appointments.GetOneApt(ProcCur.AptNum).AptDateTime.ToShortDateString();
			}
			else{
				textDate.Text=DateTime.Today.ToShortDateString();
			}
			if(ProcedureCode2.PaintType==ToothPaintingType.Extraction){//if an extraction, then mark previous procs hidden
				//Procedures.SetHideGraphical(ProcCur);//might not matter anymore
				ToothInitials.SetValue(ProcCur.PatNum,ProcCur.ToothNum,ToothInitialType.Missing);
			}
			int provNum=ProviderC.List[comboProvNum.SelectedIndex].ProvNum;
			textNotes.Text+=ProcCodeNotes.GetNote(provNum,ProcCur.CodeNum);
			listProcStatus.SelectedIndex=-1;
			//radioStatusC.Checked=true;
			ProcCur.ProcStatus=ProcStat.C;
			ProcCur.SiteNum=PatCur.SiteNum;
			comboPlaceService.SelectedIndex=PrefC.GetInt("DefaultProcedurePlaceService");
			if(EntriesAreValid()){
				SaveAndClose();
			}
		}

		private void radioUR_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="UR";
		}

		private void radioUL_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="UL";
		}

		private void radioLR_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="LR";
		}

		private void radioLL_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="LL";
		}

		private void radioU_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="U";
		}

		private void radioL_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="L";
		}

		private void radioS1_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="1";
		}

		private void radioS2_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="2";
		}

		private void radioS3_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="3";
		}

		private void radioS4_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="4";
		}

		private void radioS5_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="5";
		}

		private void radioS6_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="6";
		}

		private void checkNoBillIns_Click(object sender, System.EventArgs e) {
			if(checkNoBillIns.CheckState==CheckState.Indeterminate){
				//not allowed to set to indeterminate, so move on
				checkNoBillIns.CheckState=CheckState.Unchecked;
			}
			for(int i=0;i<ClaimProcsForProc.Count;i++) {
				//ignore CapClaim,NotReceived,PreAuth,Recieved,Supplemental
				if(ClaimProcsForProc[i].Status==ClaimProcStatus.Estimate
					|| ClaimProcsForProc[i].Status==ClaimProcStatus.CapComplete
					|| ClaimProcsForProc[i].Status==ClaimProcStatus.CapEstimate)
				{
					if(checkNoBillIns.CheckState==CheckState.Checked){
						ClaimProcsForProc[i].NoBillIns=true;
					}
					else{//unchecked
						ClaimProcsForProc[i].NoBillIns=false;
					}
					ClaimProcs.Update(ClaimProcsForProc[i]);
				}
			}
			//next line is needed to recalc BaseEst, etc, for claimprocs that are no longer NoBillIns
			//also, if they are NoBillIns, then it clears out the other values.
			Procedures.ComputeEstimates(ProcCur,PatCur.PatNum,ClaimProcsForProc,false,PlanList,PatPlanList,BenefitList);
			FillIns();
		}

		private void textNotes_TextChanged(object sender, System.EventArgs e) {
			CheckForCompleteNote();
			if(!IsStartingUp//so this happens only if user changes the note
				&& !SigChanged)//and the original signature is still showing.
			{
				sigBox.ClearTablet();
				if(allowTopaz){
					CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz);
					sigBoxTopaz.Visible=false;//until user explicitly starts it.
				}
				sigBox.SetTabletState(1);//on-screen box is now accepting input.
				SigChanged=true;
				ProcCur.UserNum=Security.CurUser.UserNum;
				textUser.Text=Userods.GetName(ProcCur.UserNum);
			}
		}

		private void CheckForCompleteNote(){
			if(textNotes.Text.IndexOf("\"\"")==-1){
				//no occurances of ""
				labelIncomplete.Visible=false;
			}
			else{
				labelIncomplete.Visible=true;
			}
		}

		private void butClearSig_Click(object sender,EventArgs e) {
			sigBox.ClearTablet();
			sigBox.Visible=true;
			if(allowTopaz) {
				CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz);
				sigBoxTopaz.Visible=false;//until user explicitly starts it.
			}
			sigBox.SetTabletState(1);//on-screen box is now accepting input.
			SigChanged=true;
			labelInvalidSig.Visible=false;
			ProcCur.UserNum=Security.CurUser.UserNum;
			textUser.Text=Userods.GetName(ProcCur.UserNum);
		}

		private void butTopazSign_Click(object sender,EventArgs e) {
			sigBox.Visible=false;
			sigBoxTopaz.Visible=true;
			if(allowTopaz){
				CodeBase.TopazWrapper.SetTopazState(sigBoxTopaz,1);
			}
			SigChanged=true;
			labelInvalidSig.Visible=false;
			ProcCur.UserNum=Security.CurUser.UserNum;
			textUser.Text=Userods.GetName(ProcCur.UserNum);
		}

		private void sigBox_MouseUp(object sender,MouseEventArgs e) {
			//this is done on mouse up so that the initial pen capture won't be delayed.
			if(sigBox.GetTabletState()==1//if accepting input.
				&& !SigChanged)//and sig not changed yet
			{
				//sigBox handles its own pen input.
				SigChanged=true;
				ProcCur.UserNum=Security.CurUser.UserNum;
				textUser.Text=Userods.GetName(ProcCur.UserNum);
			}
		}

		private void buttonUseAutoNote_Click(object sender,EventArgs e) {
			FormAutoNoteCompose FormA=new FormAutoNoteCompose();
			FormA.ShowDialog();
			if(FormA.DialogResult==DialogResult.OK) {
				textNotes.AppendText(FormA.CompletedNote);
			}
		}

		private void textStart_TextChanged(object sender, EventArgs e){
			if(textStart.Text!="" && textStop.Text!=""){
				updateTotalMin();
			}
		}

		private void updateTotalMin(){
			int startTime = Int16.Parse(textStart.Text);
			int stopTime = Int16.Parse(textStop.Text);
			int total=(((stopTime/100)*60)+(stopTime%100))-(((startTime/100)*60)+(startTime%100));
			textTotal.Text=total.ToString();
		}

		private void butShowMedical_Click(object sender,EventArgs e) {
			if(groupMedical.Height<200) {
				groupMedical.Height=226;
				butShowMedical.Text=Lan.g(this,"Hide");
			}
			else {
				groupMedical.Height=97;
				butShowMedical.Text=Lan.g(this,"Show");
			}
		}

		private void textStop_TextChanged(object sender, EventArgs e){
			if (textStart.Text != "" && textStop.Text != ""){
				updateTotalMin();
			}
		}

		private void UpdateSurf() {
			if(!Tooth.IsValidEntry(textTooth.Text)){
				return;
			}
			errorProvider2.SetError(textSurfaces,"");
			textSurfaces.Text="";
			if(butM.BackColor==Color.White) {
				textSurfaces.AppendText("M");
			}
			if(butOI.BackColor==Color.White) {
				if(ToothGraphic.IsAnterior(Tooth.FromInternat(textTooth.Text))) {
					textSurfaces.AppendText("I");
				}
				else {
					textSurfaces.AppendText("O");
				}
			}
			if(butD.BackColor==Color.White) {
				textSurfaces.AppendText("D");
			}
			if(butV.BackColor==Color.White) {
				textSurfaces.AppendText("V");
			}
			if(butBF.BackColor==Color.White) {
				if(ToothGraphic.IsAnterior(Tooth.FromInternat(textTooth.Text))) {
					textSurfaces.AppendText("F");
				}
				else {
					textSurfaces.AppendText("B");
				}
			}
			if(butL.BackColor==Color.White) {
				textSurfaces.AppendText("L");
			}
		}

		private void butM_Click(object sender,EventArgs e) {
			if(butM.BackColor==Color.White) {
				butM.BackColor=SystemColors.Control;
			}
			else {
				butM.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butOI_Click(object sender,EventArgs e) {
			if(butOI.BackColor==Color.White) {
				butOI.BackColor=SystemColors.Control;
			}
			else {
				butOI.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butL_Click(object sender,EventArgs e) {
			if(butL.BackColor==Color.White) {
				butL.BackColor=SystemColors.Control;
			}
			else {
				butL.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butV_Click(object sender,EventArgs e) {
			if(butV.BackColor==Color.White) {
				butV.BackColor=SystemColors.Control;
			}
			else {
				butV.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butBF_Click(object sender,EventArgs e) {
			if(butBF.BackColor==Color.White) {
				butBF.BackColor=SystemColors.Control;
			}
			else {
				butBF.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butD_Click(object sender,EventArgs e) {
			if(butD.BackColor==Color.White) {
				butD.BackColor=SystemColors.Control;
			}
			else {
				butD.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butPickSite_Click(object sender,EventArgs e) {
			FormSites FormS=new FormSites();
			FormS.IsSelectionMode=true;
			FormS.SelectedSiteNum=ProcCur.SiteNum;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			ProcCur.SiteNum=FormS.SelectedSiteNum;
			textSite.Text=Sites.GetDescription(ProcCur.SiteNum);
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete Procedure?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			try{
				Procedures.Delete(ProcCur.ProcNum);//also deletes the claimProcs and adjustments. Might throw exception.
				Recalls.Synch(ProcCur.PatNum);//needs to be moved into Procedures.Delete
				SecurityLogs.MakeLogEntry(Permissions.ProcComplEdit,ProcCur.PatNum,
					"Delete for: "
					+PatCur.GetNameLF()+", "+ProcedureCodes.GetProcCode(ProcCur.CodeNum).ProcCode+", "
					+ProcCur.ProcFee.ToString("c"));
				DialogResult=DialogResult.OK;	
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}		

		private bool EntriesAreValid(){
			if(  textDateTP.errorProvider1.GetError(textDateTP)!=""
				|| textDate.errorProvider1.GetError(textDate)!=""
				|| textProcFee.errorProvider1.GetError(textProcFee)!=""
				//|| textLabFee.errorProvider1.GetError(textLabFee)!=""
				|| textDateOriginalProsth.errorProvider1.GetError(textDateOriginalProsth)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			try{
				int unitqty=int.Parse(textUnitQty.Text);
				if(unitqty<1){
					MsgBox.Show(this,"Qty not valid.  Typical value is 1.");
					return false;
				}
			}
			catch{
				MsgBox.Show(this,"Qty not valid.  Typical value is 1.");
				return false;
			}
			if(errorProvider2.GetError(textSurfaces)!=""
				|| errorProvider2.GetError(textTooth)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			if(textMedicalCode.Text!="" && !ProcedureCodeC.HList.Contains(textMedicalCode.Text)){
				MsgBox.Show(this,"Invalid medical code.  It must refer to an existing procedure code.");
				return false;
			}
			/*if(CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				if(textLabCode.Text!="" && !ProcedureCodes.HList.Contains(textLabCode.Text)) {
					MsgBox.Show(this,"Invalid lab code.  It must refer to an existing procedure code.");
					return false;
				}
				if(PIn.PDouble(textLabFee.Text) >0 && textLabCode.Text=="") {
					MsgBox.Show(this,"Must enter a lab code if a lab fee is entered.");
					return false;
				}
			}*/
			if(ProcOld.ProcStatus!=ProcStat.C && ProcCur.ProcStatus==ProcStat.C){//if status was changed to complete
				if(!Security.IsAuthorized(Permissions.ProcComplCreate,PIn.PDate(textDate.Text))){//use the new date
					return false;
				}
			}
			else if(IsNew && ProcCur.ProcStatus==ProcStat.C){//if new procedure is complete
				if(!Security.IsAuthorized(Permissions.ProcComplCreate,PIn.PDate(textDate.Text))){
					return false;
				}
			}
			else if(!IsNew){//an old procedure
				if(ProcOld.ProcStatus==ProcStat.C){//that was already complete
					if(!Security.IsAuthorized(Permissions.ProcComplEdit,ProcOld.ProcDate)){//block old date
						return false;
					}
					if(ProcCur.ProcStatus==ProcStat.C){//if it's still complete
						if(!Security.IsAuthorized(Permissions.ProcComplEdit,PIn.PDate(textDate.Text))){//block new date, too
							return false;
						}
					}
				}
			}
			if(ProcedureCode2.IsProsth){
				if(listProsth.SelectedIndex==0
					|| (listProsth.SelectedIndex==2 && textDateOriginalProsth.Text==""))
				{
					if(MessageBox.Show(Lan.g(this,"Prosthesis date not entered. Continue anyway?")
						,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
					{
						return false;
					}
				}
			}
			return true;
		}

		///<summary>MUST call EntriesAreValid first.  Used from OK_Click and from butSetComplete_Click</summary>
		private void SaveAndClose(){
			if(textProcFee.Text==""){
				textProcFee.Text="0";
			}
			ProcCur.PatNum=PatCur.PatNum;
			//ProcCur.Code=this.textProc.Text;
			ProcedureCode2=ProcedureCodes.GetProcCode(textProc.Text);
			ProcCur.CodeNum=ProcedureCode2.CodeNum;
			ProcCur.MedicalCode=textMedicalCode.Text;
			ProcCur.DiagnosticCode=textDiagnosticCode.Text;
			ProcCur.IsPrincDiag=checkIsPrincDiag.Checked;
			ProcCur.CodeMod1 = textCodeMod1.Text;
			ProcCur.CodeMod2 = textCodeMod2.Text;
			ProcCur.CodeMod3 = textCodeMod3.Text;
			ProcCur.CodeMod4 = textCodeMod4.Text;
			ProcCur.RevCode = textRevCode.Text;
			ProcCur.UnitCode = textUnitCode.Text;
			ProcCur.UnitQty = Int16.Parse(textUnitQty.Text);
			ProcCur.StartTime=Int16.Parse(textStart.Text);
			ProcCur.StopTime=Int16.Parse(textStop.Text);
			if(ProcOld.ProcStatus!=ProcStat.C && ProcCur.ProcStatus==ProcStat.C){
				ProcCur.DateEntryC=DateTime.Now;//this triggers it to set to server time NOW().
			}
			ProcCur.DateTP=PIn.PDate(this.textDateTP.Text);
			ProcCur.ProcDate=PIn.PDate(this.textDate.Text);
			ProcCur.ProcFee=PIn.PDouble(textProcFee.Text);
			//ProcCur.LabFee=PIn.PDouble(textLabFee.Text);
			//ProcCur.LabProcCode=textLabCode.Text;
			//MessageBox.Show(ProcCur.ProcFee.ToString());
			//Dx taken care of when radio pushed
			switch(ProcedureCode2.TreatArea){
				case TreatmentArea.Surf:
					ProcCur.Surf=textSurfaces.Text;
					ProcCur.ToothNum=Tooth.FromInternat(textTooth.Text);
					break;
				case TreatmentArea.Tooth:
					ProcCur.Surf="";
					ProcCur.ToothNum=Tooth.FromInternat(textTooth.Text);
					break;
				case TreatmentArea.Mouth:
					ProcCur.Surf="";
					ProcCur.ToothNum="";	
					break;
				case TreatmentArea.Quad:
					//surf set when radio pushed
					ProcCur.ToothNum="";	
					break;
				case TreatmentArea.Sextant:
					//surf taken care of when radio pushed
					ProcCur.ToothNum="";	
					break;
				case TreatmentArea.Arch:
					//don't HAVE to select arch
					//taken care of when radio pushed
					ProcCur.ToothNum="";	
					break;
				case TreatmentArea.ToothRange:
					if (listBoxTeeth.SelectedItems.Count<1 && listBoxTeeth2.SelectedItems.Count<1) {
						MessageBox.Show(Lan.g(this,"Must pick at least 1 tooth"));
						return;
					}
          string range="";
					int idxAmer;
					for(int j=0;j<listBoxTeeth.SelectedIndices.Count;j++){
						idxAmer=listBoxTeeth.SelectedIndices[j];
						if(j!=0){
							range+=",";
						}
            range+=Tooth.labelsUniversal[idxAmer];
					}
					for(int j=0;j<listBoxTeeth2.SelectedIndices.Count;j++){
						idxAmer=listBoxTeeth2.SelectedIndices[j]+16;
						if(j!=0){
							range+=",";
						}
            range+=Tooth.labelsUniversal[idxAmer];
          }
			    ProcCur.ToothRange=range;
					ProcCur.Surf="";
					ProcCur.ToothNum="";	
					break;
			}
			//Status taken care of when list pushed
			ProcCur.Note=this.textNotes.Text;
			SaveSignature();
			if(comboProvNum.SelectedIndex!=-1)
				ProcCur.ProvNum=ProviderC.List[comboProvNum.SelectedIndex].ProvNum;
			if(comboDx.SelectedIndex!=-1)
				ProcCur.Dx=DefC.Short[(int)DefCat.Diagnosis][comboDx.SelectedIndex].DefNum;
			if(comboPriority.SelectedIndex==0)
				ProcCur.Priority=0;
			else
				ProcCur.Priority=DefC.Short[(int)DefCat.TxPriorities][comboPriority.SelectedIndex-1].DefNum;
			ProcCur.PlaceService=(PlaceOfService)comboPlaceService.SelectedIndex;
			if(comboClinic.SelectedIndex==0){
				ProcCur.ClinicNum=0;
			}
			else{
				ProcCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			}
			//site set when user picks from list.
			if(comboBillingTypeOne.SelectedIndex==0){
				ProcCur.BillingTypeOne=0;
			}
			else{
				ProcCur.BillingTypeOne=DefC.Short[(int)DefCat.BillingTypes][comboBillingTypeOne.SelectedIndex-1].DefNum;
			}
			if(comboBillingTypeTwo.SelectedIndex==0) {
				ProcCur.BillingTypeTwo=0;
			}
			else {
				ProcCur.BillingTypeTwo=DefC.Short[(int)DefCat.BillingTypes][comboBillingTypeTwo.SelectedIndex-1].DefNum;
			}
			//ProcCur.HideGraphical=checkHideGraphical.Checked;
			if(ProcedureCode2.IsProsth){
				switch(listProsth.SelectedIndex){
					case 0:
						ProcCur.Prosthesis="";
						break;
					case 1:
						ProcCur.Prosthesis="I";
						break;
					case 2:
						ProcCur.Prosthesis="R";
						break;
				}
				ProcCur.DateOriginalProsth=PIn.PDate(textDateOriginalProsth.Text);
			}
			else{
				ProcCur.Prosthesis="";
				ProcCur.DateOriginalProsth=DateTime.MinValue;
			}
			ProcCur.ClaimNote=textClaimNote.Text;
			Procedures.Update(ProcCur,ProcOld);
			Recalls.Synch(ProcCur.PatNum);
			if(ProcOld.ProcStatus!=ProcStat.C && ProcCur.ProcStatus==ProcStat.C){
				//if status was changed to complete
				SecurityLogs.MakeLogEntry(Permissions.ProcComplCreate,PatCur.PatNum,
					PatCur.GetNameLF()+", "+ProcedureCodes.GetProcCode(ProcCur.CodeNum).ProcCode+", "
					+ProcCur.ProcFee.ToString("c"));
			}
			else if(IsNew && ProcCur.ProcStatus==ProcStat.C){
				//if new procedure is complete
				SecurityLogs.MakeLogEntry(Permissions.ProcComplCreate,PatCur.PatNum,
					PatCur.GetNameLF()+", "+ProcedureCodes.GetProcCode(ProcCur.CodeNum).ProcCode+", "
					+ProcCur.ProcFee.ToString("c"));
			}
			else if(!IsNew){
				if(ProcOld.ProcStatus==ProcStat.C){
					SecurityLogs.MakeLogEntry(Permissions.ProcComplEdit,PatCur.PatNum,
						PatCur.GetNameLF()+", "+ProcedureCodes.GetProcCode(ProcCur.CodeNum).ProcCode+", "
						+ProcCur.ProcFee.ToString("c"));
				}
			}
			if((ProcCur.ProcStatus==ProcStat.C || ProcCur.ProcStatus==ProcStat.EC || ProcCur.ProcStatus==ProcStat.EO)
				&& ProcedureCodes.GetProcCode(ProcCur.CodeNum).PaintType==ToothPaintingType.Extraction) {
				//if an extraction, then mark previous procs hidden
				//Procedures.SetHideGraphical(ProcCur);//might not matter anymore
				ToothInitials.SetValue(ProcCur.PatNum,ProcCur.ToothNum,ToothInitialType.Missing);
			}
			ProcOld=ProcCur.Copy();//in case we now make more changes.
			//these areas have no autocodes
			if(ProcedureCode2.TreatArea==TreatmentArea.Mouth
				|| ProcedureCode2.TreatArea==TreatmentArea.Quad
				|| ProcedureCode2.TreatArea==TreatmentArea.Sextant)
			{
				DialogResult=DialogResult.OK;
				return;
			}
			//this represents the suggested code based on the autocodes set up.
			int verifyCode;
			AutoCode AutoCodeCur=null;
			if(ProcedureCode2.TreatArea==TreatmentArea.Arch){
				if(ProcCur.Surf==""){
					DialogResult=DialogResult.OK;
					return;
				}
				if(ProcCur.Surf=="U"){
					verifyCode=AutoCodeItems.VerifyCode
						(ProcedureCode2.CodeNum,"1","",false,PatCur.PatNum,PatCur.Age,out AutoCodeCur);//max
				}
				else{
					verifyCode=AutoCodeItems.VerifyCode
						(ProcedureCode2.CodeNum,"32","",false,PatCur.PatNum,PatCur.Age,out AutoCodeCur);//mand
				}
			}
			else if(ProcedureCode2.TreatArea==TreatmentArea.ToothRange){
				//test for max or mand.
				if(listBoxTeeth.SelectedItems.Count<1)
					verifyCode=AutoCodeItems.VerifyCode
						(ProcedureCode2.CodeNum,"32","",false,PatCur.PatNum,PatCur.Age,out AutoCodeCur);//mand
				else
					verifyCode=AutoCodeItems.VerifyCode
						(ProcedureCode2.CodeNum,"1","",false,PatCur.PatNum,PatCur.Age,out AutoCodeCur);//max
			}
			else{//surf or tooth
				verifyCode=AutoCodeItems.VerifyCode
					(ProcedureCode2.CodeNum,ProcCur.ToothNum,ProcCur.Surf,false,PatCur.PatNum,PatCur.Age,out AutoCodeCur);
			}
			if(ProcedureCode2.CodeNum!=verifyCode){
				string desc=ProcedureCodes.GetProcCode(verifyCode).Descript;
				FormAutoCodeLessIntrusive FormA=new FormAutoCodeLessIntrusive();
				FormA.mainText=ProcedureCodes.GetProcCode(verifyCode).ProcCode
					+" ("+desc+") "+Lan.g(this,"is the recommended procedure code for this procedure.  Change procedure code and fee?");
				FormA.ShowDialog();
				if(FormA.DialogResult!=DialogResult.OK){
					DialogResult=DialogResult.OK;
					return;
				}
				if(FormA.CheckedBox){
					AutoCodeCur.LessIntrusive=true;
					AutoCodes.Update(AutoCodeCur);
					DataValid.SetInvalid(InvalidType.AutoCodes);
				}
				ProcCur.CodeNum=verifyCode;
				//ProcedureCode2=ProcedureCodes.GetProcCode(ProcCur.CodeNum);
				//ProcCur.Code=verifyCode;
				InsPlan priplan=null;
				if(PatPlanList.Count>0) {
					priplan=InsPlans.GetPlan(PatPlanList[0].PlanNum,PlanList);
				}
				double insfee=Fees.GetAmount0(ProcCur.CodeNum,Fees.GetFeeSched(PatCur,PlanList,PatPlanList));
				if(priplan!=null && priplan.PlanType=="p") {//PPO
					double standardfee=Fees.GetAmount0(ProcCur.CodeNum,Providers.GetProv(Patients.GetProvNum(PatCur)).FeeSched);
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
				//ProcCur.ProcFee=Fees.GetAmount0(ProcedureCode2.CodeNum,Fees.GetFeeSched(PatCur,PlanList,PatPlanList));
				Procedures.Update(ProcCur,ProcOld);
				Recalls.Synch(ProcCur.PatNum);
				if(ProcCur.ProcStatus==ProcStat.C){
					SecurityLogs.MakeLogEntry(Permissions.ProcComplEdit,PatCur.PatNum,
						PatCur.GetNameLF()+", "+ProcedureCode2.ProcCode+", "
						+ProcCur.ProcFee.ToString("c"));
				}
			}
      DialogResult=DialogResult.OK;
			//it is assumed that we will do an immediate refresh after closing this window.
		}

		private void SaveSignature(){
			if(SigChanged){
				//Topaz boxes are written in Windows native code.
				if(allowTopaz && sigBoxTopaz.Visible){
					ProcCur.SigIsTopaz=true;
					if(CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(sigBoxTopaz)==0){
						ProcCur.Signature="";
						return;
					}
					CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,0);
					CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,0);
					CodeBase.TopazWrapper.SetTopazKeyString(sigBoxTopaz,"0000000000000000");
					CodeBase.TopazWrapper.SetTopazAutoKeyData(sigBoxTopaz,ProcCur.Note+ProcCur.UserNum.ToString());
					CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,2);
					CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,2);
					ProcCur.Signature=CodeBase.TopazWrapper.GetTopazString(sigBoxTopaz);
				}
				else{
					ProcCur.SigIsTopaz=false;
					if(sigBox.NumberOfTabletPoints()==0) {
						ProcCur.Signature="";
						return;
					}
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString("0000000000000000");
					sigBox.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
					//sigBox.SetEncryptionMode(2);
					//sigBox.SetSigCompressionMode(2);
					ProcCur.Signature=sigBox.GetSigString();
				}
			}
		}

		private void FormProcEdit_FormClosing(object sender,FormClosingEventArgs e) {
			if(allowTopaz){
				sigBoxTopaz.Dispose();
			}
			if(DialogResult==DialogResult.OK){
				//this catches date,prov,fee,status,etc for all claimProcs attached to this proc.
				if(!StartedAttachedToClaim
					&& Procedures.IsAttachedToClaim(ProcCur.ProcNum))
				{
					return;//unless they got attached to a claim while this window was open.  Then it doesn't touch them.
				}
				Procedures.ComputeEstimates(ProcCur,PatCur.PatNum,ClaimProcsForProc,false,PlanList,PatPlanList,BenefitList);
				return;
			}
			if(IsNew){//if cancelling on a new procedure
				//delete any newly created claimprocs
				for(int i=0;i<ClaimProcsForProc.Count;i++) {
					//if(ClaimProcsForProc[i].ProcNum==ProcCur.ProcNum) {
					ClaimProcs.Delete(ClaimProcsForProc[i]);
					//}
				}
			}
		}

		private void butOK_Click(object sender,System.EventArgs e) {
			if(EntriesAreValid()) {
				SaveAndClose();
			}
		}

		private void butCancel_Click(object sender,System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

	

	



	}
}
