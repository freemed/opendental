using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Data;
using System.Drawing.Imaging;
using System.IO;

namespace OpenDental
{
	public class FormAnestheticRecord : System.Windows.Forms.Form
	{
		public static Userod CurUser;
        private bool IsStartingUp;
		private AnestheticRecord AnestheticRecordCur;
		private AnestheticData AnestheticDataCur;
        private Patient PatCur;
        public bool IsNew;
        private GroupBox groupBoxVS;
		private Label labelVSM;
		private TextBox textVSMName;
		private Label labelVSMSerNum;
        private TextBox textVSMSerNum;
		private GroupBox groupBoxSidebarRt;
		private Label labelASA;
		private ComboBox comboASA;
		private Label labelInh;
		private Label labelLperMinN2O;
		private Label labelLperMinO2;
		private ComboBox comboO2LMin;
		private ComboBox comboN2OLMin;
		private CheckBox checkInhN2O;
		private CheckBox checkInhO2;
		private Label labelIVF;
		private ComboBox comboIVF;
		private Label labelIVFVol;
		private TextBox textIVFVol;
		private ComboBox comboIVSite;
		private Label labelIVAtt;
		private ComboBox comboIVAtt;
		private RadioButton radIVSiteL;
		private RadioButton radIVSiteR;
		private OpenDental.UI.Button butAnesthScore;
		private GroupBox groupBoxNotes;
		private RichTextBox richTextNotes;
		private Label labelEscortName;
		private TextBox textEscortName;
		private TextBox textEscortRel;
		private Label labelEscortRel;
        private GroupBox groupBoxHgtWgt;
		private GroupBox groupBoxSig;
		private OpenDental.UI.SignatureBox sigBox;
		private OpenDental.UI.Button butSignTopaz;
		private OpenDental.UI.Button butClearSig;
		private OpenDental.UI.Button butPrint;
		private GroupBox groupBoxDeliveryMethod;
		private RadioButton radRteETT;
		private RadioButton radRteNasCan;
		private RadioButton radRteNasHood;
		private GroupBox groupBoxIVSite;
		private GroupBox groupBoxMedRoute;
		private RadioButton radMedRouteIVButtFly;
		private RadioButton radMedRouteIVCath;
        private PrintDialog printDialog1;
		private OpenDental.UI.Button butClose;
        private OpenDental.UI.Button butCancel;
		private PrintDialog printDialog;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.PrintDialog printDialog2;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDlg;
		private OpenDental.UI.Button butOK;
		private Label labelEMod;
		private ComboBox comboASA_EModifier;
		private Label labelEscortCellNum;
		private TextBox textEscortCellNum;
		private RadioButton radMedRouteIM;
		private RadioButton radMedRoutePO;
		private RadioButton radMedRouteRectal;
        private GroupBox groupBoxTimes;
        private TextBox textAnesthOpen;
        private OpenDental.UI.Button butSurgClose;
        private TextBox textSurgClose;
        private TextBox textAnesthClose;
        private OpenDental.UI.Button butAnesthOpen;
        private OpenDental.UI.Button butAnesthClose;
        private OpenDental.UI.Button butSurgOpen;
        private TextBox textSurgOpen;
        private Label labelIVAnesthetics;
        private OpenDental.UI.Button butDelAnesthMeds;
        private Label labelAsst;
        private ComboBox comboCirc;
        private OpenDental.UI.Button butDelAnesthetic;
        private Label labelCirc;
        private ComboBox comboAsst;
        private OpenDental.UI.Button butAddAnesthetic;
        private ListBox listAnesthetics;
        private Label labelAnesthMed;
        private ODGrid gridAnesthMeds;
        private GroupBox groupBoxDoseCalc;
        private OpenDental.UI.Button butDose10;
        private OpenDental.UI.Button butDose7;
        private OpenDental.UI.Button butDose8;
        private OpenDental.UI.Button butDose9;
        private OpenDental.UI.Button butDose6;
        private OpenDental.UI.Button butDose5;
        private OpenDental.UI.Button butDose4;
        private OpenDental.UI.Button butDose3;
        private OpenDental.UI.Button butDose2;
        private OpenDental.UI.Button butDose1;
        private OpenDental.UI.Button butDose0;
        private OpenDental.UI.Button butDose25;
        private OpenDental.UI.Button butDose50;
		private OpenDental.UI.Button butDoseWaste;
        private OpenDental.UI.Button butDoseEnter;
        private OpenDental.UI.Button butDoseDecPoint;
        private ComboBox comboAnesthMed;
        private ComboBox comboSurgeon;
        private Label labelSurgeon;
        private ComboBox comboAnesthetist;
        private TextBox textAnesthDose;
        private Label labelDose;
        private Label labelAnesthetist;
        private TextBox textPatient;
        private Label labelPatient;
        private Label labelPatID;
        private TextBox textPatID;
        private Label label2;
        private OpenDental.UI.Button butWasteQty;
        private GroupBox groupBoxAnesthMeds;
        private Label label1;
		private RadioButton radWgtUnitsKgs;
        private ComboBox comboNPOTime;
		private RadioButton radWgtUnitsLbs;
        private TextBox textPatHgt;
        private Label labelPatWgt;
        private Label labelPatHgt;
        private TextBox textPatWgt;
        private ODGrid gridVitalSigns;
		private RadioButton radMedRouteNasal;
		private List<AnestheticMedsGiven> listAnestheticMedsGiven;
		private GroupBox groupBoxMonitors;
		private CheckBox checkMonPrecordial;
		private CheckBox checkMonEtCO2;
		private CheckBox checkMonSpO2;
		private CheckBox checkMonBP;
		private Label labelGauge;
		private Label labelIVGauge;
		private ComboBox comboIVGauge;
		private RadioButton radIVSideL;
		private RadioButton radIVSideR;
		private CheckBox checkMonEKG;
		private CheckBox checkMonTemp;
		private Label labelInvalidSig;
		private Userod userNum;
		private bool SigChanged;
		private Control sigBoxTopaz;
		private bool allowTopaz;
		private OpenDental.UI.Button butTopazSign;
		//private PrintDialog printDialog;........CLIENT.......
		private bool inputStatus = true; //if the decimal button is clicked
		private bool hasDecimal = false; //if the dose text has the decimal
		private List<DisplayField> fields;
		public int anestheticRecordCur;
		//private AnestheticRecord AnestheticRecordCur;........CLIENT.......
		
		//
		//Variables used for printing functionality..
		//
		private System.IO.Stream streamToPrint;
		private PrintDocument printDocument1;
		private PrintDocument printDocument2;
		private RadioButton radHgtUnitsCm;
		private RadioButton radHgtUnitsIn;
		private GroupBox groupBoxWgt;
		private GroupBox groupBoxHgt;
		private int patNum;
		string streamType;
		[System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
		private static extern bool BitBlt(
			IntPtr hdcDest, // handle to destination DC
			int nXDest, // x-coord of destination upper-left corner
			int nYDest, // y-coord of destination upper-left corner
			int nWidth, // width of destination rectangle
			int nHeight, // height of destination rectangle
			IntPtr hdcSrc, // handle to source DC
			int nXSrc, // x-coordinate of source upper-left corner
			int nYSrc, // y-coordinate of source upper-left corner
			System.Int32 dwRop); // raster operation code

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnestheticRecord));
			this.labelVSM = new System.Windows.Forms.Label();
			this.labelVSMSerNum = new System.Windows.Forms.Label();
			this.richTextNotes = new System.Windows.Forms.RichTextBox();
			this.textEscortName = new System.Windows.Forms.TextBox();
			this.labelEscortName = new System.Windows.Forms.Label();
			this.textEscortRel = new System.Windows.Forms.TextBox();
			this.labelEscortRel = new System.Windows.Forms.Label();
			this.groupBoxSidebarRt = new System.Windows.Forms.GroupBox();
			this.groupBoxMonitors = new System.Windows.Forms.GroupBox();
			this.checkMonTemp = new System.Windows.Forms.CheckBox();
			this.checkMonEKG = new System.Windows.Forms.CheckBox();
			this.checkMonPrecordial = new System.Windows.Forms.CheckBox();
			this.checkMonEtCO2 = new System.Windows.Forms.CheckBox();
			this.checkMonSpO2 = new System.Windows.Forms.CheckBox();
			this.checkMonBP = new System.Windows.Forms.CheckBox();
			this.labelEMod = new System.Windows.Forms.Label();
			this.comboASA_EModifier = new System.Windows.Forms.ComboBox();
			this.groupBoxIVSite = new System.Windows.Forms.GroupBox();
			this.radIVSideL = new System.Windows.Forms.RadioButton();
			this.radIVSideR = new System.Windows.Forms.RadioButton();
			this.labelGauge = new System.Windows.Forms.Label();
			this.labelIVGauge = new System.Windows.Forms.Label();
			this.comboIVGauge = new System.Windows.Forms.ComboBox();
			this.comboIVSite = new System.Windows.Forms.ComboBox();
			this.radIVSiteR = new System.Windows.Forms.RadioButton();
			this.radIVSiteL = new System.Windows.Forms.RadioButton();
			this.comboIVAtt = new System.Windows.Forms.ComboBox();
			this.labelIVAtt = new System.Windows.Forms.Label();
			this.groupBoxMedRoute = new System.Windows.Forms.GroupBox();
			this.radMedRouteRectal = new System.Windows.Forms.RadioButton();
			this.radMedRouteNasal = new System.Windows.Forms.RadioButton();
			this.radMedRouteIM = new System.Windows.Forms.RadioButton();
			this.radMedRoutePO = new System.Windows.Forms.RadioButton();
			this.radMedRouteIVButtFly = new System.Windows.Forms.RadioButton();
			this.radMedRouteIVCath = new System.Windows.Forms.RadioButton();
			this.groupBoxDeliveryMethod = new System.Windows.Forms.GroupBox();
			this.radRteETT = new System.Windows.Forms.RadioButton();
			this.radRteNasCan = new System.Windows.Forms.RadioButton();
			this.radRteNasHood = new System.Windows.Forms.RadioButton();
			this.labelLperMinN2O = new System.Windows.Forms.Label();
			this.labelLperMinO2 = new System.Windows.Forms.Label();
			this.comboO2LMin = new System.Windows.Forms.ComboBox();
			this.labelIVFVol = new System.Windows.Forms.Label();
			this.textIVFVol = new System.Windows.Forms.TextBox();
			this.labelIVF = new System.Windows.Forms.Label();
			this.comboIVF = new System.Windows.Forms.ComboBox();
			this.labelInh = new System.Windows.Forms.Label();
			this.comboN2OLMin = new System.Windows.Forms.ComboBox();
			this.checkInhN2O = new System.Windows.Forms.CheckBox();
			this.checkInhO2 = new System.Windows.Forms.CheckBox();
			this.comboASA = new System.Windows.Forms.ComboBox();
			this.labelASA = new System.Windows.Forms.Label();
			this.groupBoxNotes = new System.Windows.Forms.GroupBox();
			this.groupBoxWgt = new System.Windows.Forms.GroupBox();
			this.radWgtUnitsKgs = new System.Windows.Forms.RadioButton();
			this.radWgtUnitsLbs = new System.Windows.Forms.RadioButton();
			this.groupBoxHgt = new System.Windows.Forms.GroupBox();
			this.radHgtUnitsCm = new System.Windows.Forms.RadioButton();
			this.radHgtUnitsIn = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.comboNPOTime = new System.Windows.Forms.ComboBox();
			this.textPatHgt = new System.Windows.Forms.TextBox();
			this.labelPatWgt = new System.Windows.Forms.Label();
			this.labelPatHgt = new System.Windows.Forms.Label();
			this.textPatWgt = new System.Windows.Forms.TextBox();
			this.groupBoxSig = new System.Windows.Forms.GroupBox();
			this.labelInvalidSig = new System.Windows.Forms.Label();
			this.groupBoxHgtWgt = new System.Windows.Forms.GroupBox();
			this.labelEscortCellNum = new System.Windows.Forms.Label();
			this.textEscortCellNum = new System.Windows.Forms.TextBox();
			this.groupBoxVS = new System.Windows.Forms.GroupBox();
			this.textVSMSerNum = new System.Windows.Forms.TextBox();
			this.textVSMName = new System.Windows.Forms.TextBox();
			this.printDialog = new System.Windows.Forms.PrintDialog();
			this.groupBoxTimes = new System.Windows.Forms.GroupBox();
			this.textAnesthOpen = new System.Windows.Forms.TextBox();
			this.textSurgClose = new System.Windows.Forms.TextBox();
			this.textAnesthClose = new System.Windows.Forms.TextBox();
			this.textSurgOpen = new System.Windows.Forms.TextBox();
			this.labelIVAnesthetics = new System.Windows.Forms.Label();
			this.labelAsst = new System.Windows.Forms.Label();
			this.comboCirc = new System.Windows.Forms.ComboBox();
			this.labelCirc = new System.Windows.Forms.Label();
			this.comboAsst = new System.Windows.Forms.ComboBox();
			this.listAnesthetics = new System.Windows.Forms.ListBox();
			this.labelAnesthMed = new System.Windows.Forms.Label();
			this.groupBoxDoseCalc = new System.Windows.Forms.GroupBox();
			this.comboAnesthMed = new System.Windows.Forms.ComboBox();
			this.comboSurgeon = new System.Windows.Forms.ComboBox();
			this.labelSurgeon = new System.Windows.Forms.Label();
			this.comboAnesthetist = new System.Windows.Forms.ComboBox();
			this.textAnesthDose = new System.Windows.Forms.TextBox();
			this.labelDose = new System.Windows.Forms.Label();
			this.labelAnesthetist = new System.Windows.Forms.Label();
			this.textPatient = new System.Windows.Forms.TextBox();
			this.labelPatient = new System.Windows.Forms.Label();
			this.labelPatID = new System.Windows.Forms.Label();
			this.textPatID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBoxAnesthMeds = new System.Windows.Forms.GroupBox();
			this.gridVitalSigns = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.sigBox = new OpenDental.UI.SignatureBox();
			this.butSignTopaz = new OpenDental.UI.Button();
			this.butClearSig = new OpenDental.UI.Button();
			this.butAnesthScore = new OpenDental.UI.Button();
			this.gridAnesthMeds = new OpenDental.UI.ODGrid();
			this.butWasteQty = new OpenDental.UI.Button();
			this.butAddAnesthetic = new OpenDental.UI.Button();
			this.butDelAnesthetic = new OpenDental.UI.Button();
			this.butDelAnesthMeds = new OpenDental.UI.Button();
			this.butSurgClose = new OpenDental.UI.Button();
			this.butAnesthOpen = new OpenDental.UI.Button();
			this.butAnesthClose = new OpenDental.UI.Button();
			this.butSurgOpen = new OpenDental.UI.Button();
			this.butDose10 = new OpenDental.UI.Button();
			this.butDose7 = new OpenDental.UI.Button();
			this.butDose8 = new OpenDental.UI.Button();
			this.butDose9 = new OpenDental.UI.Button();
			this.butDose6 = new OpenDental.UI.Button();
			this.butDose5 = new OpenDental.UI.Button();
			this.butDose4 = new OpenDental.UI.Button();
			this.butDose3 = new OpenDental.UI.Button();
			this.butDose2 = new OpenDental.UI.Button();
			this.butDose1 = new OpenDental.UI.Button();
			this.butDose0 = new OpenDental.UI.Button();
			this.butDose25 = new OpenDental.UI.Button();
			this.butDose50 = new OpenDental.UI.Button();
			this.butDoseWaste = new OpenDental.UI.Button();
			this.butDoseEnter = new OpenDental.UI.Button();
			this.butDoseDecPoint = new OpenDental.UI.Button();
			this.groupBoxSidebarRt.SuspendLayout();
			this.groupBoxMonitors.SuspendLayout();
			this.groupBoxIVSite.SuspendLayout();
			this.groupBoxMedRoute.SuspendLayout();
			this.groupBoxDeliveryMethod.SuspendLayout();
			this.groupBoxNotes.SuspendLayout();
			this.groupBoxWgt.SuspendLayout();
			this.groupBoxHgt.SuspendLayout();
			this.groupBoxSig.SuspendLayout();
			this.groupBoxHgtWgt.SuspendLayout();
			this.groupBoxVS.SuspendLayout();
			this.groupBoxTimes.SuspendLayout();
			this.groupBoxDoseCalc.SuspendLayout();
			this.groupBoxAnesthMeds.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelVSM
			// 
			this.labelVSM.AutoSize = true;
			this.labelVSM.Location = new System.Drawing.Point(72, 22);
			this.labelVSM.Name = "labelVSM";
			this.labelVSM.Size = new System.Drawing.Size(92, 13);
			this.labelVSM.TabIndex = 80;
			this.labelVSM.Text = "Vital Sign Monitor:";
			// 
			// labelVSMSerNum
			// 
			this.labelVSMSerNum.AutoSize = true;
			this.labelVSMSerNum.Location = new System.Drawing.Point(295, 22);
			this.labelVSMSerNum.Name = "labelVSMSerNum";
			this.labelVSMSerNum.Size = new System.Drawing.Size(46, 13);
			this.labelVSMSerNum.TabIndex = 81;
			this.labelVSMSerNum.Text = "Serial #:";
			// 
			// richTextNotes
			// 
			this.richTextNotes.Location = new System.Drawing.Point(23, 19);
			this.richTextNotes.Name = "richTextNotes";
			this.richTextNotes.Size = new System.Drawing.Size(180, 80);
			this.richTextNotes.TabIndex = 67;
			this.richTextNotes.Text = "";
			// 
			// textEscortName
			// 
			this.textEscortName.Location = new System.Drawing.Point(88, 17);
			this.textEscortName.MaxLength = 32;
			this.textEscortName.Name = "textEscortName";
			this.textEscortName.Size = new System.Drawing.Size(170, 20);
			this.textEscortName.TabIndex = 79;
			// 
			// labelEscortName
			// 
			this.labelEscortName.AutoSize = true;
			this.labelEscortName.Location = new System.Drawing.Point(13, 21);
			this.labelEscortName.Name = "labelEscortName";
			this.labelEscortName.Size = new System.Drawing.Size(66, 13);
			this.labelEscortName.TabIndex = 77;
			this.labelEscortName.Text = "Escort name";
			// 
			// textEscortRel
			// 
			this.textEscortRel.Location = new System.Drawing.Point(88, 63);
			this.textEscortRel.MaxLength = 16;
			this.textEscortRel.Name = "textEscortRel";
			this.textEscortRel.Size = new System.Drawing.Size(170, 20);
			this.textEscortRel.TabIndex = 81;
			// 
			// labelEscortRel
			// 
			this.labelEscortRel.AutoSize = true;
			this.labelEscortRel.Location = new System.Drawing.Point(15, 66);
			this.labelEscortRel.Name = "labelEscortRel";
			this.labelEscortRel.Size = new System.Drawing.Size(65, 13);
			this.labelEscortRel.TabIndex = 82;
			this.labelEscortRel.Text = "Relationship";
			// 
			// groupBoxSidebarRt
			// 
			this.groupBoxSidebarRt.Controls.Add(this.groupBoxMonitors);
			this.groupBoxSidebarRt.Controls.Add(this.labelEMod);
			this.groupBoxSidebarRt.Controls.Add(this.comboASA_EModifier);
			this.groupBoxSidebarRt.Controls.Add(this.groupBoxIVSite);
			this.groupBoxSidebarRt.Controls.Add(this.groupBoxMedRoute);
			this.groupBoxSidebarRt.Controls.Add(this.groupBoxDeliveryMethod);
			this.groupBoxSidebarRt.Controls.Add(this.labelLperMinN2O);
			this.groupBoxSidebarRt.Controls.Add(this.labelLperMinO2);
			this.groupBoxSidebarRt.Controls.Add(this.butAnesthScore);
			this.groupBoxSidebarRt.Controls.Add(this.comboO2LMin);
			this.groupBoxSidebarRt.Controls.Add(this.labelIVFVol);
			this.groupBoxSidebarRt.Controls.Add(this.textIVFVol);
			this.groupBoxSidebarRt.Controls.Add(this.labelIVF);
			this.groupBoxSidebarRt.Controls.Add(this.comboIVF);
			this.groupBoxSidebarRt.Controls.Add(this.labelInh);
			this.groupBoxSidebarRt.Controls.Add(this.comboN2OLMin);
			this.groupBoxSidebarRt.Controls.Add(this.checkInhN2O);
			this.groupBoxSidebarRt.Controls.Add(this.checkInhO2);
			this.groupBoxSidebarRt.Controls.Add(this.comboASA);
			this.groupBoxSidebarRt.Controls.Add(this.labelASA);
			this.groupBoxSidebarRt.Location = new System.Drawing.Point(612, -10);
			this.groupBoxSidebarRt.Name = "groupBoxSidebarRt";
			this.groupBoxSidebarRt.Size = new System.Drawing.Size(265, 569);
			this.groupBoxSidebarRt.TabIndex = 136;
			this.groupBoxSidebarRt.TabStop = false;
			this.groupBoxSidebarRt.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// groupBoxMonitors
			// 
			this.groupBoxMonitors.Controls.Add(this.checkMonTemp);
			this.groupBoxMonitors.Controls.Add(this.checkMonEKG);
			this.groupBoxMonitors.Controls.Add(this.checkMonPrecordial);
			this.groupBoxMonitors.Controls.Add(this.checkMonEtCO2);
			this.groupBoxMonitors.Controls.Add(this.checkMonSpO2);
			this.groupBoxMonitors.Controls.Add(this.checkMonBP);
			this.groupBoxMonitors.Location = new System.Drawing.Point(9, 429);
			this.groupBoxMonitors.Name = "groupBoxMonitors";
			this.groupBoxMonitors.Size = new System.Drawing.Size(247, 62);
			this.groupBoxMonitors.TabIndex = 58;
			this.groupBoxMonitors.TabStop = false;
			this.groupBoxMonitors.Text = "Monitors";
			// 
			// checkMonTemp
			// 
			this.checkMonTemp.AutoSize = true;
			this.checkMonTemp.Location = new System.Drawing.Point(169, 35);
			this.checkMonTemp.Name = "checkMonTemp";
			this.checkMonTemp.Size = new System.Drawing.Size(53, 17);
			this.checkMonTemp.TabIndex = 64;
			this.checkMonTemp.Text = "Temp";
			this.checkMonTemp.UseVisualStyleBackColor = true;
			// 
			// checkMonEKG
			// 
			this.checkMonEKG.AutoSize = true;
			this.checkMonEKG.Location = new System.Drawing.Point(117, 17);
			this.checkMonEKG.Name = "checkMonEKG";
			this.checkMonEKG.Size = new System.Drawing.Size(48, 17);
			this.checkMonEKG.TabIndex = 61;
			this.checkMonEKG.Text = "EKG";
			this.checkMonEKG.UseVisualStyleBackColor = true;
			// 
			// checkMonPrecordial
			// 
			this.checkMonPrecordial.AutoSize = true;
			this.checkMonPrecordial.Location = new System.Drawing.Point(16, 35);
			this.checkMonPrecordial.Name = "checkMonPrecordial";
			this.checkMonPrecordial.Size = new System.Drawing.Size(134, 17);
			this.checkMonPrecordial.TabIndex = 63;
			this.checkMonPrecordial.Text = "Precordial stethoscope";
			this.checkMonPrecordial.UseVisualStyleBackColor = true;
			// 
			// checkMonEtCO2
			// 
			this.checkMonEtCO2.AutoSize = true;
			this.checkMonEtCO2.Location = new System.Drawing.Point(169, 17);
			this.checkMonEtCO2.Name = "checkMonEtCO2";
			this.checkMonEtCO2.Size = new System.Drawing.Size(57, 17);
			this.checkMonEtCO2.TabIndex = 62;
			this.checkMonEtCO2.Text = "EtCO2";
			this.checkMonEtCO2.UseVisualStyleBackColor = true;
			// 
			// checkMonSpO2
			// 
			this.checkMonSpO2.AutoSize = true;
			this.checkMonSpO2.Location = new System.Drawing.Point(59, 17);
			this.checkMonSpO2.Name = "checkMonSpO2";
			this.checkMonSpO2.Size = new System.Drawing.Size(53, 17);
			this.checkMonSpO2.TabIndex = 60;
			this.checkMonSpO2.Text = "SpO2";
			this.checkMonSpO2.UseVisualStyleBackColor = true;
			// 
			// checkMonBP
			// 
			this.checkMonBP.AutoSize = true;
			this.checkMonBP.Location = new System.Drawing.Point(16, 17);
			this.checkMonBP.Name = "checkMonBP";
			this.checkMonBP.Size = new System.Drawing.Size(40, 17);
			this.checkMonBP.TabIndex = 59;
			this.checkMonBP.Text = "BP";
			this.checkMonBP.UseVisualStyleBackColor = true;
			this.checkMonBP.CheckedChanged += new System.EventHandler(this.checkBP_CheckedChanged);
			// 
			// labelEMod
			// 
			this.labelEMod.AutoSize = true;
			this.labelEMod.Location = new System.Drawing.Point(146, 36);
			this.labelEMod.Name = "labelEMod";
			this.labelEMod.Size = new System.Drawing.Size(54, 13);
			this.labelEMod.TabIndex = 26;
			this.labelEMod.Text = "E Modifier";
			// 
			// comboASA_EModifier
			// 
			this.comboASA_EModifier.AutoCompleteCustomSource.AddRange(new string[] {
            "",
            "E"});
			this.comboASA_EModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboASA_EModifier.FormattingEnabled = true;
			this.comboASA_EModifier.Items.AddRange(new object[] {
            "",
            "E"});
			this.comboASA_EModifier.Location = new System.Drawing.Point(93, 33);
			this.comboASA_EModifier.Name = "comboASA_EModifier";
			this.comboASA_EModifier.Size = new System.Drawing.Size(50, 21);
			this.comboASA_EModifier.TabIndex = 25;
			// 
			// groupBoxIVSite
			// 
			this.groupBoxIVSite.Controls.Add(this.radIVSideL);
			this.groupBoxIVSite.Controls.Add(this.radIVSideR);
			this.groupBoxIVSite.Controls.Add(this.labelGauge);
			this.groupBoxIVSite.Controls.Add(this.labelIVGauge);
			this.groupBoxIVSite.Controls.Add(this.comboIVGauge);
			this.groupBoxIVSite.Controls.Add(this.comboIVSite);
			this.groupBoxIVSite.Controls.Add(this.radIVSiteR);
			this.groupBoxIVSite.Controls.Add(this.radIVSiteL);
			this.groupBoxIVSite.Controls.Add(this.comboIVAtt);
			this.groupBoxIVSite.Controls.Add(this.labelIVAtt);
			this.groupBoxIVSite.Location = new System.Drawing.Point(17, 287);
			this.groupBoxIVSite.Name = "groupBoxIVSite";
			this.groupBoxIVSite.Size = new System.Drawing.Size(232, 82);
			this.groupBoxIVSite.TabIndex = 45;
			this.groupBoxIVSite.TabStop = false;
			this.groupBoxIVSite.Text = "IV Site";
			// 
			// radIVSideL
			// 
			this.radIVSideL.AutoSize = true;
			this.radIVSideL.Location = new System.Drawing.Point(71, 49);
			this.radIVSideL.Name = "radIVSideL";
			this.radIVSideL.Size = new System.Drawing.Size(43, 17);
			this.radIVSideL.TabIndex = 50;
			this.radIVSideL.TabStop = true;
			this.radIVSideL.Text = "Left";
			this.radIVSideL.UseVisualStyleBackColor = true;
			// 
			// radIVSideR
			// 
			this.radIVSideR.AutoSize = true;
			this.radIVSideR.Location = new System.Drawing.Point(21, 49);
			this.radIVSideR.Name = "radIVSideR";
			this.radIVSideR.Size = new System.Drawing.Size(50, 17);
			this.radIVSideR.TabIndex = 49;
			this.radIVSideR.TabStop = true;
			this.radIVSideR.Text = "Right";
			this.radIVSideR.UseVisualStyleBackColor = true;
			// 
			// labelGauge
			// 
			this.labelGauge.AutoSize = true;
			this.labelGauge.Location = new System.Drawing.Point(207, 25);
			this.labelGauge.Name = "labelGauge";
			this.labelGauge.Size = new System.Drawing.Size(22, 13);
			this.labelGauge.TabIndex = 53;
			this.labelGauge.Text = "ga.";
			// 
			// labelIVGauge
			// 
			this.labelIVGauge.AutoSize = true;
			this.labelIVGauge.Location = new System.Drawing.Point(140, 0);
			this.labelIVGauge.Name = "labelIVGauge";
			this.labelIVGauge.Size = new System.Drawing.Size(39, 13);
			this.labelIVGauge.TabIndex = 46;
			this.labelIVGauge.Text = "Gauge";
			// 
			// comboIVGauge
			// 
			this.comboIVGauge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboIVGauge.FormattingEnabled = true;
			this.comboIVGauge.Items.AddRange(new object[] {
            "18",
            "20",
            "21",
            "22"});
			this.comboIVGauge.Location = new System.Drawing.Point(138, 21);
			this.comboIVGauge.Name = "comboIVGauge";
			this.comboIVGauge.Size = new System.Drawing.Size(65, 21);
			this.comboIVGauge.TabIndex = 48;
			// 
			// comboIVSite
			// 
			this.comboIVSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboIVSite.FormattingEnabled = true;
			this.comboIVSite.Items.AddRange(new object[] {
            "Antecubital fossa",
            "Forearm (dorsal)",
            "Forearm (ventral)",
            "Hand",
            "Wrist",
            "Other (list in Notes)"});
			this.comboIVSite.Location = new System.Drawing.Point(11, 21);
			this.comboIVSite.Name = "comboIVSite";
			this.comboIVSite.Size = new System.Drawing.Size(119, 21);
			this.comboIVSite.TabIndex = 47;
			// 
			// radIVSiteR
			// 
			this.radIVSiteR.AutoSize = true;
			this.radIVSiteR.Location = new System.Drawing.Point(10, 90);
			this.radIVSiteR.Name = "radIVSiteR";
			this.radIVSiteR.Size = new System.Drawing.Size(50, 17);
			this.radIVSiteR.TabIndex = 133;
			this.radIVSiteR.TabStop = true;
			this.radIVSiteR.Text = "Right";
			this.radIVSiteR.UseVisualStyleBackColor = true;
			// 
			// radIVSiteL
			// 
			this.radIVSiteL.AutoSize = true;
			this.radIVSiteL.Location = new System.Drawing.Point(80, 90);
			this.radIVSiteL.Name = "radIVSiteL";
			this.radIVSiteL.Size = new System.Drawing.Size(43, 17);
			this.radIVSiteL.TabIndex = 154;
			this.radIVSiteL.TabStop = true;
			this.radIVSiteL.Text = "Left";
			this.radIVSiteL.UseVisualStyleBackColor = true;
			// 
			// comboIVAtt
			// 
			this.comboIVAtt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboIVAtt.FormattingEnabled = true;
			this.comboIVAtt.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.comboIVAtt.Location = new System.Drawing.Point(138, 47);
			this.comboIVAtt.Name = "comboIVAtt";
			this.comboIVAtt.Size = new System.Drawing.Size(30, 21);
			this.comboIVAtt.TabIndex = 51;
			// 
			// labelIVAtt
			// 
			this.labelIVAtt.AutoSize = true;
			this.labelIVAtt.Location = new System.Drawing.Point(174, 50);
			this.labelIVAtt.Name = "labelIVAtt";
			this.labelIVAtt.Size = new System.Drawing.Size(48, 13);
			this.labelIVAtt.TabIndex = 52;
			this.labelIVAtt.Text = "Attempts";
			// 
			// groupBoxMedRoute
			// 
			this.groupBoxMedRoute.Controls.Add(this.radMedRouteRectal);
			this.groupBoxMedRoute.Controls.Add(this.radMedRouteNasal);
			this.groupBoxMedRoute.Controls.Add(this.radMedRouteIM);
			this.groupBoxMedRoute.Controls.Add(this.radMedRoutePO);
			this.groupBoxMedRoute.Controls.Add(this.radMedRouteIVButtFly);
			this.groupBoxMedRoute.Controls.Add(this.radMedRouteIVCath);
			this.groupBoxMedRoute.Location = new System.Drawing.Point(43, 203);
			this.groupBoxMedRoute.Name = "groupBoxMedRoute";
			this.groupBoxMedRoute.Size = new System.Drawing.Size(165, 78);
			this.groupBoxMedRoute.TabIndex = 38;
			this.groupBoxMedRoute.TabStop = false;
			this.groupBoxMedRoute.Text = "Administration Route";
			// 
			// radMedRouteRectal
			// 
			this.radMedRouteRectal.AutoSize = true;
			this.radMedRouteRectal.Location = new System.Drawing.Point(84, 50);
			this.radMedRouteRectal.Name = "radMedRouteRectal";
			this.radMedRouteRectal.Size = new System.Drawing.Size(56, 17);
			this.radMedRouteRectal.TabIndex = 44;
			this.radMedRouteRectal.TabStop = true;
			this.radMedRouteRectal.Text = "Rectal";
			this.radMedRouteRectal.UseVisualStyleBackColor = true;
			// 
			// radMedRouteNasal
			// 
			this.radMedRouteNasal.AutoSize = true;
			this.radMedRouteNasal.Location = new System.Drawing.Point(6, 50);
			this.radMedRouteNasal.Name = "radMedRouteNasal";
			this.radMedRouteNasal.Size = new System.Drawing.Size(52, 17);
			this.radMedRouteNasal.TabIndex = 43;
			this.radMedRouteNasal.TabStop = true;
			this.radMedRouteNasal.Text = "Nasal";
			this.radMedRouteNasal.UseVisualStyleBackColor = true;
			// 
			// radMedRouteIM
			// 
			this.radMedRouteIM.AutoSize = true;
			this.radMedRouteIM.Location = new System.Drawing.Point(84, 34);
			this.radMedRouteIM.Name = "radMedRouteIM";
			this.radMedRouteIM.Size = new System.Drawing.Size(37, 17);
			this.radMedRouteIM.TabIndex = 42;
			this.radMedRouteIM.TabStop = true;
			this.radMedRouteIM.Text = "IM";
			this.radMedRouteIM.UseVisualStyleBackColor = true;
			// 
			// radMedRoutePO
			// 
			this.radMedRoutePO.AutoSize = true;
			this.radMedRoutePO.Location = new System.Drawing.Point(6, 34);
			this.radMedRoutePO.Name = "radMedRoutePO";
			this.radMedRoutePO.Size = new System.Drawing.Size(40, 17);
			this.radMedRoutePO.TabIndex = 41;
			this.radMedRoutePO.TabStop = true;
			this.radMedRoutePO.Text = "PO";
			this.radMedRoutePO.UseVisualStyleBackColor = true;
			// 
			// radMedRouteIVButtFly
			// 
			this.radMedRouteIVButtFly.AutoSize = true;
			this.radMedRouteIVButtFly.Location = new System.Drawing.Point(84, 17);
			this.radMedRouteIVButtFly.Name = "radMedRouteIVButtFly";
			this.radMedRouteIVButtFly.Size = new System.Drawing.Size(76, 17);
			this.radMedRouteIVButtFly.TabIndex = 40;
			this.radMedRouteIVButtFly.TabStop = true;
			this.radMedRouteIVButtFly.Text = "IV Butterfly";
			this.radMedRouteIVButtFly.UseVisualStyleBackColor = true;
			// 
			// radMedRouteIVCath
			// 
			this.radMedRouteIVCath.AutoSize = true;
			this.radMedRouteIVCath.Location = new System.Drawing.Point(6, 17);
			this.radMedRouteIVCath.Name = "radMedRouteIVCath";
			this.radMedRouteIVCath.Size = new System.Drawing.Size(78, 17);
			this.radMedRouteIVCath.TabIndex = 39;
			this.radMedRouteIVCath.TabStop = true;
			this.radMedRouteIVCath.Text = "IV Catheter";
			this.radMedRouteIVCath.UseVisualStyleBackColor = true;
			// 
			// groupBoxDeliveryMethod
			// 
			this.groupBoxDeliveryMethod.Controls.Add(this.radRteETT);
			this.groupBoxDeliveryMethod.Controls.Add(this.radRteNasCan);
			this.groupBoxDeliveryMethod.Controls.Add(this.radRteNasHood);
			this.groupBoxDeliveryMethod.Location = new System.Drawing.Point(43, 125);
			this.groupBoxDeliveryMethod.Name = "groupBoxDeliveryMethod";
			this.groupBoxDeliveryMethod.Size = new System.Drawing.Size(124, 72);
			this.groupBoxDeliveryMethod.TabIndex = 34;
			this.groupBoxDeliveryMethod.TabStop = false;
			this.groupBoxDeliveryMethod.Text = "Delivery method";
			// 
			// radRteETT
			// 
			this.radRteETT.AutoSize = true;
			this.radRteETT.Location = new System.Drawing.Point(8, 47);
			this.radRteETT.Name = "radRteETT";
			this.radRteETT.Size = new System.Drawing.Size(112, 17);
			this.radRteETT.TabIndex = 37;
			this.radRteETT.TabStop = true;
			this.radRteETT.Text = "Endotracheal tube";
			this.radRteETT.UseVisualStyleBackColor = true;
			// 
			// radRteNasCan
			// 
			this.radRteNasCan.AutoSize = true;
			this.radRteNasCan.Location = new System.Drawing.Point(8, 15);
			this.radRteNasCan.Name = "radRteNasCan";
			this.radRteNasCan.Size = new System.Drawing.Size(93, 17);
			this.radRteNasCan.TabIndex = 35;
			this.radRteNasCan.TabStop = true;
			this.radRteNasCan.Text = "Nasal cannula";
			this.radRteNasCan.UseVisualStyleBackColor = true;
			// 
			// radRteNasHood
			// 
			this.radRteNasHood.AutoSize = true;
			this.radRteNasHood.Location = new System.Drawing.Point(8, 31);
			this.radRteNasHood.Name = "radRteNasHood";
			this.radRteNasHood.Size = new System.Drawing.Size(79, 17);
			this.radRteNasHood.TabIndex = 36;
			this.radRteNasHood.TabStop = true;
			this.radRteNasHood.Text = "Nasal hood";
			this.radRteNasHood.UseVisualStyleBackColor = true;
			// 
			// labelLperMinN2O
			// 
			this.labelLperMinN2O.AutoSize = true;
			this.labelLperMinN2O.Location = new System.Drawing.Point(135, 100);
			this.labelLperMinN2O.Name = "labelLperMinN2O";
			this.labelLperMinN2O.Size = new System.Drawing.Size(34, 13);
			this.labelLperMinN2O.TabIndex = 33;
			this.labelLperMinN2O.Text = "L/min";
			// 
			// labelLperMinO2
			// 
			this.labelLperMinO2.AutoSize = true;
			this.labelLperMinO2.Location = new System.Drawing.Point(134, 78);
			this.labelLperMinO2.Name = "labelLperMinO2";
			this.labelLperMinO2.Size = new System.Drawing.Size(34, 13);
			this.labelLperMinO2.TabIndex = 30;
			this.labelLperMinO2.Text = "L/min";
			// 
			// comboO2LMin
			// 
			this.comboO2LMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboO2LMin.FormattingEnabled = true;
			this.comboO2LMin.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.comboO2LMin.Location = new System.Drawing.Point(89, 75);
			this.comboO2LMin.Name = "comboO2LMin";
			this.comboO2LMin.Size = new System.Drawing.Size(40, 21);
			this.comboO2LMin.TabIndex = 29;
			// 
			// labelIVFVol
			// 
			this.labelIVFVol.AutoSize = true;
			this.labelIVFVol.Location = new System.Drawing.Point(211, 396);
			this.labelIVFVol.Name = "labelIVFVol";
			this.labelIVFVol.Size = new System.Drawing.Size(21, 13);
			this.labelIVFVol.TabIndex = 57;
			this.labelIVFVol.Text = "mL";
			// 
			// textIVFVol
			// 
			this.textIVFVol.Location = new System.Drawing.Point(155, 393);
			this.textIVFVol.MaxLength = 5;
			this.textIVFVol.Name = "textIVFVol";
			this.textIVFVol.Size = new System.Drawing.Size(51, 20);
			this.textIVFVol.TabIndex = 56;
			this.textIVFVol.TextChanged += new System.EventHandler(this.textIVFVol_TextChanged);
			// 
			// labelIVF
			// 
			this.labelIVF.AutoSize = true;
			this.labelIVF.Location = new System.Drawing.Point(24, 376);
			this.labelIVF.Name = "labelIVF";
			this.labelIVF.Size = new System.Drawing.Size(42, 13);
			this.labelIVF.TabIndex = 54;
			this.labelIVF.Text = "IV Fluid";
			// 
			// comboIVF
			// 
			this.comboIVF.AutoCompleteCustomSource.AddRange(new string[] {
            "D5(1/2)NS"});
			this.comboIVF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboIVF.FormattingEnabled = true;
			this.comboIVF.Items.AddRange(new object[] {
            "D5(1/2)NS",
            "D5NS",
            "D5LR",
            "D5W",
            "LR",
            "NS"});
			this.comboIVF.Location = new System.Drawing.Point(28, 393);
			this.comboIVF.Name = "comboIVF";
			this.comboIVF.Size = new System.Drawing.Size(119, 21);
			this.comboIVF.TabIndex = 55;
			// 
			// labelInh
			// 
			this.labelInh.AutoSize = true;
			this.labelInh.Location = new System.Drawing.Point(38, 59);
			this.labelInh.Name = "labelInh";
			this.labelInh.Size = new System.Drawing.Size(96, 13);
			this.labelInh.TabIndex = 27;
			this.labelInh.Text = "Inhalational agents";
			// 
			// comboN2OLMin
			// 
			this.comboN2OLMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboN2OLMin.FormattingEnabled = true;
			this.comboN2OLMin.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.comboN2OLMin.Location = new System.Drawing.Point(89, 97);
			this.comboN2OLMin.Name = "comboN2OLMin";
			this.comboN2OLMin.Size = new System.Drawing.Size(40, 21);
			this.comboN2OLMin.TabIndex = 32;
			// 
			// checkInhN2O
			// 
			this.checkInhN2O.AutoSize = true;
			this.checkInhN2O.Location = new System.Drawing.Point(42, 101);
			this.checkInhN2O.Name = "checkInhN2O";
			this.checkInhN2O.Size = new System.Drawing.Size(46, 17);
			this.checkInhN2O.TabIndex = 31;
			this.checkInhN2O.Text = "N20";
			this.checkInhN2O.UseVisualStyleBackColor = true;
			// 
			// checkInhO2
			// 
			this.checkInhO2.AutoSize = true;
			this.checkInhO2.Checked = true;
			this.checkInhO2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkInhO2.Location = new System.Drawing.Point(42, 77);
			this.checkInhO2.Name = "checkInhO2";
			this.checkInhO2.Size = new System.Drawing.Size(40, 17);
			this.checkInhO2.TabIndex = 28;
			this.checkInhO2.Text = "O2";
			this.checkInhO2.UseVisualStyleBackColor = true;
			// 
			// comboASA
			// 
			this.comboASA.AutoCompleteCustomSource.AddRange(new string[] {
            "I"});
			this.comboASA.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboASA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboASA.FormattingEnabled = true;
			this.comboASA.Items.AddRange(new object[] {
            "I",
            "II",
            "III",
            "IV",
            "V"});
			this.comboASA.Location = new System.Drawing.Point(30, 32);
			this.comboASA.Name = "comboASA";
			this.comboASA.Size = new System.Drawing.Size(50, 21);
			this.comboASA.TabIndex = 24;
			// 
			// labelASA
			// 
			this.labelASA.AutoSize = true;
			this.labelASA.Location = new System.Drawing.Point(28, 16);
			this.labelASA.Name = "labelASA";
			this.labelASA.Size = new System.Drawing.Size(95, 13);
			this.labelASA.TabIndex = 23;
			this.labelASA.Text = "ASA  Classification";
			// 
			// groupBoxNotes
			// 
			this.groupBoxNotes.Controls.Add(this.groupBoxWgt);
			this.groupBoxNotes.Controls.Add(this.groupBoxHgt);
			this.groupBoxNotes.Controls.Add(this.label1);
			this.groupBoxNotes.Controls.Add(this.comboNPOTime);
			this.groupBoxNotes.Controls.Add(this.textPatHgt);
			this.groupBoxNotes.Controls.Add(this.labelPatWgt);
			this.groupBoxNotes.Controls.Add(this.labelPatHgt);
			this.groupBoxNotes.Controls.Add(this.textPatWgt);
			this.groupBoxNotes.Controls.Add(this.butOK);
			this.groupBoxNotes.Controls.Add(this.butClose);
			this.groupBoxNotes.Controls.Add(this.butCancel);
			this.groupBoxNotes.Controls.Add(this.richTextNotes);
			this.groupBoxNotes.Controls.Add(this.butPrint);
			this.groupBoxNotes.Controls.Add(this.groupBoxSig);
			this.groupBoxNotes.Controls.Add(this.groupBoxHgtWgt);
			this.groupBoxNotes.Location = new System.Drawing.Point(14, 565);
			this.groupBoxNotes.Name = "groupBoxNotes";
			this.groupBoxNotes.Size = new System.Drawing.Size(865, 160);
			this.groupBoxNotes.TabIndex = 66;
			this.groupBoxNotes.TabStop = false;
			this.groupBoxNotes.Text = "Notes (record additional meds/routes/times here)";
			// 
			// groupBoxWgt
			// 
			this.groupBoxWgt.Controls.Add(this.radWgtUnitsKgs);
			this.groupBoxWgt.Controls.Add(this.radWgtUnitsLbs);
			this.groupBoxWgt.Location = new System.Drawing.Point(330, 36);
			this.groupBoxWgt.Name = "groupBoxWgt";
			this.groupBoxWgt.Size = new System.Drawing.Size(105, 33);
			this.groupBoxWgt.TabIndex = 155;
			this.groupBoxWgt.TabStop = false;
			this.groupBoxWgt.Enter += new System.EventHandler(this.groupBox1_Enter_1);
			// 
			// radWgtUnitsKgs
			// 
			this.radWgtUnitsKgs.AutoSize = true;
			this.radWgtUnitsKgs.Location = new System.Drawing.Point(56, 10);
			this.radWgtUnitsKgs.Name = "radWgtUnitsKgs";
			this.radWgtUnitsKgs.Size = new System.Drawing.Size(37, 17);
			this.radWgtUnitsKgs.TabIndex = 75;
			this.radWgtUnitsKgs.Text = "kg";
			this.radWgtUnitsKgs.UseVisualStyleBackColor = true;
			this.radWgtUnitsKgs.CheckedChanged += new System.EventHandler(this.radPatWgtKgs_CheckedChanged);
			// 
			// radWgtUnitsLbs
			// 
			this.radWgtUnitsLbs.AutoSize = true;
			this.radWgtUnitsLbs.Checked = true;
			this.radWgtUnitsLbs.Location = new System.Drawing.Point(13, 10);
			this.radWgtUnitsLbs.Name = "radWgtUnitsLbs";
			this.radWgtUnitsLbs.Size = new System.Drawing.Size(41, 17);
			this.radWgtUnitsLbs.TabIndex = 74;
			this.radWgtUnitsLbs.TabStop = true;
			this.radWgtUnitsLbs.Text = "lbs.";
			this.radWgtUnitsLbs.UseVisualStyleBackColor = true;
			this.radWgtUnitsLbs.CheckedChanged += new System.EventHandler(this.radPatWgtLbs_CheckedChanged);
			// 
			// groupBoxHgt
			// 
			this.groupBoxHgt.Controls.Add(this.radHgtUnitsCm);
			this.groupBoxHgt.Controls.Add(this.radHgtUnitsIn);
			this.groupBoxHgt.Location = new System.Drawing.Point(330, 9);
			this.groupBoxHgt.Name = "groupBoxHgt";
			this.groupBoxHgt.Size = new System.Drawing.Size(105, 33);
			this.groupBoxHgt.TabIndex = 154;
			this.groupBoxHgt.TabStop = false;
			// 
			// radHgtUnitsCm
			// 
			this.radHgtUnitsCm.AutoSize = true;
			this.radHgtUnitsCm.Location = new System.Drawing.Point(55, 10);
			this.radHgtUnitsCm.Name = "radHgtUnitsCm";
			this.radHgtUnitsCm.Size = new System.Drawing.Size(39, 17);
			this.radHgtUnitsCm.TabIndex = 72;
			this.radHgtUnitsCm.Text = "cm";
			this.radHgtUnitsCm.UseVisualStyleBackColor = true;
			this.radHgtUnitsCm.CheckedChanged += new System.EventHandler(this.radHgtCm_CheckedChanged);
			// 
			// radHgtUnitsIn
			// 
			this.radHgtUnitsIn.AutoSize = true;
			this.radHgtUnitsIn.Checked = true;
			this.radHgtUnitsIn.Location = new System.Drawing.Point(13, 10);
			this.radHgtUnitsIn.Name = "radHgtUnitsIn";
			this.radHgtUnitsIn.Size = new System.Drawing.Size(36, 17);
			this.radHgtUnitsIn.TabIndex = 71;
			this.radHgtUnitsIn.TabStop = true;
			this.radHgtUnitsIn.Text = "in.";
			this.radHgtUnitsIn.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(456, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 82;
			this.label1.Text = "NPO Since";
			// 
			// comboNPOTime
			// 
			this.comboNPOTime.FormattingEnabled = true;
			this.comboNPOTime.Items.AddRange(new object[] {
            "12 MN",
            "1 AM",
            "2 AM",
            "3 AM",
            "4 AM",
            "5 AM",
            "6 AM",
            "7 AM",
            "8 AM",
            "9 AM",
            "10 AM",
            "11 AM",
            "12 PM",
            "1 PM",
            "2 PM",
            "3 PM",
            "4 PM",
            "5 PM",
            "6 PM",
            "7 PM",
            "8 PM",
            "9 PM",
            "10 PM",
            "11 PM"});
			this.comboNPOTime.Location = new System.Drawing.Point(459, 36);
			this.comboNPOTime.Name = "comboNPOTime";
			this.comboNPOTime.Size = new System.Drawing.Size(54, 21);
			this.comboNPOTime.TabIndex = 82;
			// 
			// textPatHgt
			// 
			this.textPatHgt.Location = new System.Drawing.Point(270, 16);
			this.textPatHgt.MaxLength = 10;
			this.textPatHgt.Name = "textPatHgt";
			this.textPatHgt.Size = new System.Drawing.Size(60, 20);
			this.textPatHgt.TabIndex = 70;
			this.textPatHgt.TextChanged += new System.EventHandler(this.textPatHgt_TextChanged);
			// 
			// labelPatWgt
			// 
			this.labelPatWgt.AutoSize = true;
			this.labelPatWgt.Location = new System.Drawing.Point(223, 45);
			this.labelPatWgt.Name = "labelPatWgt";
			this.labelPatWgt.Size = new System.Drawing.Size(41, 13);
			this.labelPatWgt.TabIndex = 69;
			this.labelPatWgt.Text = "Weight";
			// 
			// labelPatHgt
			// 
			this.labelPatHgt.AutoSize = true;
			this.labelPatHgt.Location = new System.Drawing.Point(226, 19);
			this.labelPatHgt.Name = "labelPatHgt";
			this.labelPatHgt.Size = new System.Drawing.Size(38, 13);
			this.labelPatHgt.TabIndex = 68;
			this.labelPatHgt.Text = "Height";
			// 
			// textPatWgt
			// 
			this.textPatWgt.Location = new System.Drawing.Point(270, 42);
			this.textPatWgt.MaxLength = 3;
			this.textPatWgt.Name = "textPatWgt";
			this.textPatWgt.Size = new System.Drawing.Size(60, 20);
			this.textPatWgt.TabIndex = 73;
			// 
			// groupBoxSig
			// 
			this.groupBoxSig.Controls.Add(this.labelInvalidSig);
			this.groupBoxSig.Controls.Add(this.sigBox);
			this.groupBoxSig.Controls.Add(this.butSignTopaz);
			this.groupBoxSig.Controls.Add(this.butClearSig);
			this.groupBoxSig.Location = new System.Drawing.Point(588, 0);
			this.groupBoxSig.Name = "groupBoxSig";
			this.groupBoxSig.Size = new System.Drawing.Size(277, 110);
			this.groupBoxSig.TabIndex = 139;
			this.groupBoxSig.TabStop = false;
			this.groupBoxSig.Text = "Signature/Initials";
			// 
			// labelInvalidSig
			// 
			this.labelInvalidSig.BackColor = System.Drawing.SystemColors.Window;
			this.labelInvalidSig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelInvalidSig.Location = new System.Drawing.Point(23, 27);
			this.labelInvalidSig.Name = "labelInvalidSig";
			this.labelInvalidSig.Size = new System.Drawing.Size(133, 59);
			this.labelInvalidSig.TabIndex = 83;
			this.labelInvalidSig.Text = "Invalid Signature - Anesthesia Close Time has changed since it was signed.";
			this.labelInvalidSig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelInvalidSig.Click += new System.EventHandler(this.labelInvalidSig_Click);
			// 
			// groupBoxHgtWgt
			// 
			this.groupBoxHgtWgt.Controls.Add(this.labelEscortCellNum);
			this.groupBoxHgtWgt.Controls.Add(this.textEscortCellNum);
			this.groupBoxHgtWgt.Controls.Add(this.labelEscortRel);
			this.groupBoxHgtWgt.Controls.Add(this.labelEscortName);
			this.groupBoxHgtWgt.Controls.Add(this.textEscortName);
			this.groupBoxHgtWgt.Controls.Add(this.textEscortRel);
			this.groupBoxHgtWgt.Location = new System.Drawing.Point(215, 64);
			this.groupBoxHgtWgt.Name = "groupBoxHgtWgt";
			this.groupBoxHgtWgt.Size = new System.Drawing.Size(276, 91);
			this.groupBoxHgtWgt.TabIndex = 76;
			this.groupBoxHgtWgt.TabStop = false;
			this.groupBoxHgtWgt.Enter += new System.EventHandler(this.groupBoxHgtWgt_Enter);
			// 
			// labelEscortCellNum
			// 
			this.labelEscortCellNum.AutoSize = true;
			this.labelEscortCellNum.Location = new System.Drawing.Point(13, 43);
			this.labelEscortCellNum.Name = "labelEscortCellNum";
			this.labelEscortCellNum.Size = new System.Drawing.Size(67, 13);
			this.labelEscortCellNum.TabIndex = 78;
			this.labelEscortCellNum.Text = "Escort Cell #";
			// 
			// textEscortCellNum
			// 
			this.textEscortCellNum.Location = new System.Drawing.Point(88, 40);
			this.textEscortCellNum.MaxLength = 13;
			this.textEscortCellNum.Name = "textEscortCellNum";
			this.textEscortCellNum.Size = new System.Drawing.Size(170, 20);
			this.textEscortCellNum.TabIndex = 80;
			// 
			// groupBoxVS
			// 
			this.groupBoxVS.Controls.Add(this.gridVitalSigns);
			this.groupBoxVS.Controls.Add(this.textVSMSerNum);
			this.groupBoxVS.Controls.Add(this.textVSMName);
			this.groupBoxVS.Controls.Add(this.labelVSM);
			this.groupBoxVS.Controls.Add(this.labelVSMSerNum);
			this.groupBoxVS.Location = new System.Drawing.Point(14, 352);
			this.groupBoxVS.Name = "groupBoxVS";
			this.groupBoxVS.Size = new System.Drawing.Size(592, 207);
			this.groupBoxVS.TabIndex = 139;
			this.groupBoxVS.TabStop = false;
			this.groupBoxVS.Text = "Vital Signs";
			// 
			// textVSMSerNum
			// 
			this.textVSMSerNum.Location = new System.Drawing.Point(347, 19);
			this.textVSMSerNum.MaxLength = 20;
			this.textVSMSerNum.Name = "textVSMSerNum";
			this.textVSMSerNum.Size = new System.Drawing.Size(88, 20);
			this.textVSMSerNum.TabIndex = 132;
			// 
			// textVSMName
			// 
			this.textVSMName.Location = new System.Drawing.Point(170, 19);
			this.textVSMName.MaxLength = 20;
			this.textVSMName.Name = "textVSMName";
			this.textVSMName.Size = new System.Drawing.Size(88, 20);
			this.textVSMName.TabIndex = 130;
			// 
			// groupBoxTimes
			// 
			this.groupBoxTimes.Controls.Add(this.textAnesthOpen);
			this.groupBoxTimes.Controls.Add(this.butSurgClose);
			this.groupBoxTimes.Controls.Add(this.textSurgClose);
			this.groupBoxTimes.Controls.Add(this.textAnesthClose);
			this.groupBoxTimes.Controls.Add(this.butAnesthOpen);
			this.groupBoxTimes.Controls.Add(this.butAnesthClose);
			this.groupBoxTimes.Controls.Add(this.butSurgOpen);
			this.groupBoxTimes.Controls.Add(this.textSurgOpen);
			this.groupBoxTimes.Location = new System.Drawing.Point(173, 11);
			this.groupBoxTimes.Name = "groupBoxTimes";
			this.groupBoxTimes.Size = new System.Drawing.Size(413, 76);
			this.groupBoxTimes.TabIndex = 6;
			this.groupBoxTimes.TabStop = false;
			this.groupBoxTimes.Text = "Times";
			// 
			// textAnesthOpen
			// 
			this.textAnesthOpen.Location = new System.Drawing.Point(16, 49);
			this.textAnesthOpen.Name = "textAnesthOpen";
			this.textAnesthOpen.Size = new System.Drawing.Size(100, 20);
			this.textAnesthOpen.TabIndex = 8;
			this.textAnesthOpen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textAnesthOpen.TextChanged += new System.EventHandler(this.textBoxAnesthOpen_TextChanged);
			// 
			// textSurgClose
			// 
			this.textSurgClose.Location = new System.Drawing.Point(212, 49);
			this.textSurgClose.Name = "textSurgClose";
			this.textSurgClose.Size = new System.Drawing.Size(86, 20);
			this.textSurgClose.TabIndex = 12;
			this.textSurgClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textSurgClose.TextChanged += new System.EventHandler(this.textBoxSurgClose_TextChanged_1);
			// 
			// textAnesthClose
			// 
			this.textAnesthClose.Location = new System.Drawing.Point(303, 49);
			this.textAnesthClose.Name = "textAnesthClose";
			this.textAnesthClose.ShortcutsEnabled = false;
			this.textAnesthClose.Size = new System.Drawing.Size(100, 20);
			this.textAnesthClose.TabIndex = 14;
			this.textAnesthClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textAnesthClose.TextChanged += new System.EventHandler(this.textBoxAnesthClose_TextChanged);
			// 
			// textSurgOpen
			// 
			this.textSurgOpen.Location = new System.Drawing.Point(121, 49);
			this.textSurgOpen.Name = "textSurgOpen";
			this.textSurgOpen.Size = new System.Drawing.Size(86, 20);
			this.textSurgOpen.TabIndex = 10;
			this.textSurgOpen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textSurgOpen.TextChanged += new System.EventHandler(this.textBoxSurgOpen_TextChanged);
			// 
			// labelIVAnesthetics
			// 
			this.labelIVAnesthetics.AutoSize = true;
			this.labelIVAnesthetics.Location = new System.Drawing.Point(25, 76);
			this.labelIVAnesthetics.Name = "labelIVAnesthetics";
			this.labelIVAnesthetics.Size = new System.Drawing.Size(75, 13);
			this.labelIVAnesthetics.TabIndex = 5;
			this.labelIVAnesthetics.Text = "IV Anesthetics";
			// 
			// labelAsst
			// 
			this.labelAsst.AutoSize = true;
			this.labelAsst.Location = new System.Drawing.Point(432, 88);
			this.labelAsst.Name = "labelAsst";
			this.labelAsst.Size = new System.Drawing.Size(49, 13);
			this.labelAsst.TabIndex = 19;
			this.labelAsst.Text = "Assistant";
			// 
			// comboCirc
			// 
			this.comboCirc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCirc.FormattingEnabled = true;
			this.comboCirc.Location = new System.Drawing.Point(483, 105);
			this.comboCirc.Name = "comboCirc";
			this.comboCirc.Size = new System.Drawing.Size(100, 21);
			this.comboCirc.TabIndex = 22;
			// 
			// labelCirc
			// 
			this.labelCirc.AutoSize = true;
			this.labelCirc.Location = new System.Drawing.Point(533, 88);
			this.labelCirc.Name = "labelCirc";
			this.labelCirc.Size = new System.Drawing.Size(51, 13);
			this.labelCirc.TabIndex = 21;
			this.labelCirc.Text = "Circulator";
			// 
			// comboAsst
			// 
			this.comboAsst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAsst.FormattingEnabled = true;
			this.comboAsst.Location = new System.Drawing.Point(381, 105);
			this.comboAsst.Name = "comboAsst";
			this.comboAsst.Size = new System.Drawing.Size(100, 21);
			this.comboAsst.TabIndex = 20;
			// 
			// listAnesthetics
			// 
			this.listAnesthetics.Location = new System.Drawing.Point(25, 95);
			this.listAnesthetics.Name = "listAnesthetics";
			this.listAnesthetics.Size = new System.Drawing.Size(139, 43);
			this.listAnesthetics.TabIndex = 0;
			this.listAnesthetics.SelectedIndexChanged += new System.EventHandler(this.listAnesthetics_SelectedIndexChanged);
			// 
			// labelAnesthMed
			// 
			this.labelAnesthMed.AutoSize = true;
			this.labelAnesthMed.Location = new System.Drawing.Point(184, 131);
			this.labelAnesthMed.Name = "labelAnesthMed";
			this.labelAnesthMed.Size = new System.Drawing.Size(111, 13);
			this.labelAnesthMed.TabIndex = 55;
			this.labelAnesthMed.Text = "Anesthetic medication";
			// 
			// groupBoxDoseCalc
			// 
			this.groupBoxDoseCalc.Controls.Add(this.butDose10);
			this.groupBoxDoseCalc.Controls.Add(this.butDose7);
			this.groupBoxDoseCalc.Controls.Add(this.butDose8);
			this.groupBoxDoseCalc.Controls.Add(this.butDose9);
			this.groupBoxDoseCalc.Controls.Add(this.butDose6);
			this.groupBoxDoseCalc.Controls.Add(this.butDose5);
			this.groupBoxDoseCalc.Controls.Add(this.butDose4);
			this.groupBoxDoseCalc.Controls.Add(this.butDose3);
			this.groupBoxDoseCalc.Controls.Add(this.butDose2);
			this.groupBoxDoseCalc.Controls.Add(this.butDose1);
			this.groupBoxDoseCalc.Controls.Add(this.butDose0);
			this.groupBoxDoseCalc.Controls.Add(this.butDose25);
			this.groupBoxDoseCalc.Controls.Add(this.butDose50);
			this.groupBoxDoseCalc.Controls.Add(this.butDoseWaste);
			this.groupBoxDoseCalc.Controls.Add(this.butDoseEnter);
			this.groupBoxDoseCalc.Controls.Add(this.butDoseDecPoint);
			this.groupBoxDoseCalc.Location = new System.Drawing.Point(379, 141);
			this.groupBoxDoseCalc.Name = "groupBoxDoseCalc";
			this.groupBoxDoseCalc.Size = new System.Drawing.Size(200, 177);
			this.groupBoxDoseCalc.TabIndex = 54;
			this.groupBoxDoseCalc.TabStop = false;
			this.groupBoxDoseCalc.Text = "Click to add dose ";
			this.groupBoxDoseCalc.Enter += new System.EventHandler(this.groupBox5_Enter);
			// 
			// comboAnesthMed
			// 
			this.comboAnesthMed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAnesthMed.FormattingEnabled = true;
			this.comboAnesthMed.Location = new System.Drawing.Point(174, 147);
			this.comboAnesthMed.Name = "comboAnesthMed";
			this.comboAnesthMed.Size = new System.Drawing.Size(139, 21);
			this.comboAnesthMed.TabIndex = 77;
			this.comboAnesthMed.SelectedIndexChanged += new System.EventHandler(this.comboAnesthMed_SelectedIndexChanged);
			// 
			// comboSurgeon
			// 
			this.comboSurgeon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSurgeon.FormattingEnabled = true;
			this.comboSurgeon.Location = new System.Drawing.Point(277, 105);
			this.comboSurgeon.Name = "comboSurgeon";
			this.comboSurgeon.Size = new System.Drawing.Size(100, 21);
			this.comboSurgeon.TabIndex = 18;
			// 
			// labelSurgeon
			// 
			this.labelSurgeon.AutoSize = true;
			this.labelSurgeon.Location = new System.Drawing.Point(329, 88);
			this.labelSurgeon.Name = "labelSurgeon";
			this.labelSurgeon.Size = new System.Drawing.Size(47, 13);
			this.labelSurgeon.TabIndex = 17;
			this.labelSurgeon.Text = "Surgeon";
			this.labelSurgeon.Click += new System.EventHandler(this.label3_Click);
			// 
			// comboAnesthetist
			// 
			this.comboAnesthetist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAnesthetist.FormattingEnabled = true;
			this.comboAnesthetist.Location = new System.Drawing.Point(174, 105);
			this.comboAnesthetist.Name = "comboAnesthetist";
			this.comboAnesthetist.Size = new System.Drawing.Size(100, 21);
			this.comboAnesthetist.TabIndex = 16;
			this.comboAnesthetist.SelectedIndexChanged += new System.EventHandler(this.comboAnesthetist_SelectedIndexChanged);
			// 
			// textAnesthDose
			// 
			this.textAnesthDose.Location = new System.Drawing.Point(318, 148);
			this.textAnesthDose.MaxLength = 7;
			this.textAnesthDose.Name = "textAnesthDose";
			this.textAnesthDose.Size = new System.Drawing.Size(54, 20);
			this.textAnesthDose.TabIndex = 99;
			// 
			// labelDose
			// 
			this.labelDose.AutoSize = true;
			this.labelDose.Location = new System.Drawing.Point(319, 131);
			this.labelDose.Name = "labelDose";
			this.labelDose.Size = new System.Drawing.Size(55, 13);
			this.labelDose.TabIndex = 100;
			this.labelDose.Text = "Dose (mL)";
			// 
			// labelAnesthetist
			// 
			this.labelAnesthetist.AutoSize = true;
			this.labelAnesthetist.Location = new System.Drawing.Point(215, 88);
			this.labelAnesthetist.Name = "labelAnesthetist";
			this.labelAnesthetist.Size = new System.Drawing.Size(59, 13);
			this.labelAnesthetist.TabIndex = 15;
			this.labelAnesthetist.Text = "Anesthetist";
			this.labelAnesthetist.Click += new System.EventHandler(this.labelAnesthetist_Click);
			// 
			// textPatient
			// 
			this.textPatient.Location = new System.Drawing.Point(12, 16);
			this.textPatient.Name = "textPatient";
			this.textPatient.ReadOnly = true;
			this.textPatient.Size = new System.Drawing.Size(150, 20);
			this.textPatient.TabIndex = 2;
			this.textPatient.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textPatient.TextChanged += new System.EventHandler(this.textBoxPatient_TextChanged);
			// 
			// labelPatient
			// 
			this.labelPatient.AutoSize = true;
			this.labelPatient.Location = new System.Drawing.Point(3, 19);
			this.labelPatient.Name = "labelPatient";
			this.labelPatient.Size = new System.Drawing.Size(0, 13);
			this.labelPatient.TabIndex = 103;
			// 
			// labelPatID
			// 
			this.labelPatID.AutoSize = true;
			this.labelPatID.Location = new System.Drawing.Point(8, 47);
			this.labelPatID.Name = "labelPatID";
			this.labelPatID.Size = new System.Drawing.Size(38, 13);
			this.labelPatID.TabIndex = 3;
			this.labelPatID.Text = "ID No.";
			// 
			// textPatID
			// 
			this.textPatID.Location = new System.Drawing.Point(49, 44);
			this.textPatID.Name = "textPatID";
			this.textPatID.ReadOnly = true;
			this.textPatID.Size = new System.Drawing.Size(113, 20);
			this.textPatID.TabIndex = 4;
			this.textPatID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textPatID.TextChanged += new System.EventHandler(this.textBoxPatID_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(406, 321);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(150, 13);
			this.label2.TabIndex = 107;
			this.label2.Text = "(Doses must be entered in mL)";
			// 
			// groupBoxAnesthMeds
			// 
			this.groupBoxAnesthMeds.Controls.Add(this.gridAnesthMeds);
			this.groupBoxAnesthMeds.Controls.Add(this.butWasteQty);
			this.groupBoxAnesthMeds.Controls.Add(this.label2);
			this.groupBoxAnesthMeds.Controls.Add(this.textPatID);
			this.groupBoxAnesthMeds.Controls.Add(this.labelPatID);
			this.groupBoxAnesthMeds.Controls.Add(this.labelPatient);
			this.groupBoxAnesthMeds.Controls.Add(this.textPatient);
			this.groupBoxAnesthMeds.Controls.Add(this.labelAnesthetist);
			this.groupBoxAnesthMeds.Controls.Add(this.labelDose);
			this.groupBoxAnesthMeds.Controls.Add(this.textAnesthDose);
			this.groupBoxAnesthMeds.Controls.Add(this.comboAnesthetist);
			this.groupBoxAnesthMeds.Controls.Add(this.labelSurgeon);
			this.groupBoxAnesthMeds.Controls.Add(this.comboSurgeon);
			this.groupBoxAnesthMeds.Controls.Add(this.comboAnesthMed);
			this.groupBoxAnesthMeds.Controls.Add(this.labelAnesthMed);
			this.groupBoxAnesthMeds.Controls.Add(this.listAnesthetics);
			this.groupBoxAnesthMeds.Controls.Add(this.butAddAnesthetic);
			this.groupBoxAnesthMeds.Controls.Add(this.comboAsst);
			this.groupBoxAnesthMeds.Controls.Add(this.labelCirc);
			this.groupBoxAnesthMeds.Controls.Add(this.butDelAnesthetic);
			this.groupBoxAnesthMeds.Controls.Add(this.comboCirc);
			this.groupBoxAnesthMeds.Controls.Add(this.labelAsst);
			this.groupBoxAnesthMeds.Controls.Add(this.butDelAnesthMeds);
			this.groupBoxAnesthMeds.Controls.Add(this.labelIVAnesthetics);
			this.groupBoxAnesthMeds.Controls.Add(this.groupBoxTimes);
			this.groupBoxAnesthMeds.Controls.Add(this.groupBoxDoseCalc);
			this.groupBoxAnesthMeds.Location = new System.Drawing.Point(12, 6);
			this.groupBoxAnesthMeds.Name = "groupBoxAnesthMeds";
			this.groupBoxAnesthMeds.Size = new System.Drawing.Size(592, 342);
			this.groupBoxAnesthMeds.TabIndex = 1;
			this.groupBoxAnesthMeds.TabStop = false;
			this.groupBoxAnesthMeds.Text = "Patient";
			// 
			// gridVitalSigns
			// 
			this.gridVitalSigns.HScrollVisible = false;
			this.gridVitalSigns.Location = new System.Drawing.Point(23, 50);
			this.gridVitalSigns.Name = "gridVitalSigns";
			this.gridVitalSigns.ScrollValue = 0;
			this.gridVitalSigns.Size = new System.Drawing.Size(547, 143);
			this.gridVitalSigns.TabIndex = 133;
			this.gridVitalSigns.Title = "Vital Signs";
			this.gridVitalSigns.TranslationName = "TableAnestheticData";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butOK.Location = new System.Drawing.Point(684, 115);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 143;
			this.butOK.Text = "&OK";
			this.butOK.UseVisualStyleBackColor = true;
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClose.Location = new System.Drawing.Point(765, 115);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(96, 26);
			this.butClose.TabIndex = 142;
			this.butClose.Text = "&Save and Close";
			this.butClose.UseVisualStyleBackColor = true;
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCancel.Location = new System.Drawing.Point(594, 115);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(66, 26);
			this.butCancel.TabIndex = 141;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(72, 116);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(88, 26);
			this.butPrint.TabIndex = 102;
			this.butPrint.Text = "Print";
			this.butPrint.UseVisualStyleBackColor = true;
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// sigBox
			// 
			this.sigBox.Location = new System.Drawing.Point(12, 19);
			this.sigBox.Name = "sigBox";
			this.sigBox.Size = new System.Drawing.Size(158, 74);
			this.sigBox.TabIndex = 135;
			this.sigBox.Click += new System.EventHandler(this.sigBox_Click);
			// 
			// butSignTopaz
			// 
			this.butSignTopaz.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSignTopaz.Autosize = true;
			this.butSignTopaz.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSignTopaz.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSignTopaz.CornerRadius = 4F;
			this.butSignTopaz.Location = new System.Drawing.Point(195, 19);
			this.butSignTopaz.Name = "butSignTopaz";
			this.butSignTopaz.Size = new System.Drawing.Size(75, 26);
			this.butSignTopaz.TabIndex = 136;
			this.butSignTopaz.Text = "Sign Topaz";
			this.butSignTopaz.Click += new System.EventHandler(this.butSignTopaz_Click);
			// 
			// butClearSig
			// 
			this.butClearSig.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClearSig.Autosize = true;
			this.butClearSig.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClearSig.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClearSig.CornerRadius = 4F;
			this.butClearSig.Location = new System.Drawing.Point(195, 53);
			this.butClearSig.Name = "butClearSig";
			this.butClearSig.Size = new System.Drawing.Size(75, 26);
			this.butClearSig.TabIndex = 134;
			this.butClearSig.Text = "Clear";
			this.butClearSig.Click += new System.EventHandler(this.butClearSig_Click);
			// 
			// butAnesthScore
			// 
			this.butAnesthScore.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAnesthScore.Autosize = true;
			this.butAnesthScore.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAnesthScore.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAnesthScore.CornerRadius = 4F;
			this.butAnesthScore.Location = new System.Drawing.Point(71, 517);
			this.butAnesthScore.Name = "butAnesthScore";
			this.butAnesthScore.Size = new System.Drawing.Size(131, 26);
			this.butAnesthScore.TabIndex = 65;
			this.butAnesthScore.Text = "Post-anesthesia score";
			this.butAnesthScore.UseVisualStyleBackColor = true;
			this.butAnesthScore.Click += new System.EventHandler(this.butAnesthScore_Click);
			// 
			// gridAnesthMeds
			// 
			this.gridAnesthMeds.HScrollVisible = false;
			this.gridAnesthMeds.Location = new System.Drawing.Point(25, 175);
			this.gridAnesthMeds.Name = "gridAnesthMeds";
			this.gridAnesthMeds.ScrollValue = 0;
			this.gridAnesthMeds.Size = new System.Drawing.Size(346, 127);
			this.gridAnesthMeds.TabIndex = 11;
			this.gridAnesthMeds.Title = "Anesthetic Medications";
			this.gridAnesthMeds.TranslationName = "TableAnesthMedsGiven";
			this.gridAnesthMeds.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAnesthMeds_CellDoubleClick);
			// 
			// butWasteQty
			// 
			this.butWasteQty.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butWasteQty.Autosize = true;
			this.butWasteQty.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butWasteQty.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butWasteQty.CornerRadius = 4F;
			this.butWasteQty.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butWasteQty.Location = new System.Drawing.Point(37, 308);
			this.butWasteQty.Name = "butWasteQty";
			this.butWasteQty.Size = new System.Drawing.Size(116, 26);
			this.butWasteQty.TabIndex = 108;
			this.butWasteQty.Text = "Waste from inventory";
			this.butWasteQty.UseVisualStyleBackColor = true;
			this.butWasteQty.Click += new System.EventHandler(this.butWasteQty_Click);
			// 
			// butAddAnesthetic
			// 
			this.butAddAnesthetic.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddAnesthetic.Autosize = true;
			this.butAddAnesthetic.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddAnesthetic.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddAnesthetic.CornerRadius = 4F;
			this.butAddAnesthetic.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddAnesthetic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddAnesthetic.Location = new System.Drawing.Point(26, 144);
			this.butAddAnesthetic.Name = "butAddAnesthetic";
			this.butAddAnesthetic.Size = new System.Drawing.Size(65, 26);
			this.butAddAnesthetic.TabIndex = 53;
			this.butAddAnesthetic.Text = "New";
			this.butAddAnesthetic.UseVisualStyleBackColor = true;
			this.butAddAnesthetic.Click += new System.EventHandler(this.butAddAnesthetic_Click);
			// 
			// butDelAnesthetic
			// 
			this.butDelAnesthetic.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelAnesthetic.Autosize = true;
			this.butDelAnesthetic.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelAnesthetic.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelAnesthetic.CornerRadius = 4F;
			this.butDelAnesthetic.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelAnesthetic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelAnesthetic.Location = new System.Drawing.Point(97, 144);
			this.butDelAnesthetic.Name = "butDelAnesthetic";
			this.butDelAnesthetic.Size = new System.Drawing.Size(65, 26);
			this.butDelAnesthetic.TabIndex = 3;
			this.butDelAnesthetic.Text = "Delete";
			this.butDelAnesthetic.UseVisualStyleBackColor = true;
			this.butDelAnesthetic.Click += new System.EventHandler(this.butDelAnesthetic_Click);
			// 
			// butDelAnesthMeds
			// 
			this.butDelAnesthMeds.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelAnesthMeds.Autosize = true;
			this.butDelAnesthMeds.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelAnesthMeds.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelAnesthMeds.CornerRadius = 4F;
			this.butDelAnesthMeds.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelAnesthMeds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelAnesthMeds.Location = new System.Drawing.Point(290, 308);
			this.butDelAnesthMeds.Name = "butDelAnesthMeds";
			this.butDelAnesthMeds.Size = new System.Drawing.Size(82, 26);
			this.butDelAnesthMeds.TabIndex = 74;
			this.butDelAnesthMeds.Text = "Delete";
			this.butDelAnesthMeds.UseVisualStyleBackColor = true;
			this.butDelAnesthMeds.Click += new System.EventHandler(this.butDelAnesthMeds_Click);
			// 
			// butSurgClose
			// 
			this.butSurgClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSurgClose.Autosize = true;
			this.butSurgClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSurgClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSurgClose.CornerRadius = 4F;
			this.butSurgClose.Location = new System.Drawing.Point(211, 17);
			this.butSurgClose.Name = "butSurgClose";
			this.butSurgClose.Size = new System.Drawing.Size(86, 26);
			this.butSurgClose.TabIndex = 11;
			this.butSurgClose.Text = "Surgery Close";
			this.butSurgClose.UseVisualStyleBackColor = true;
			this.butSurgClose.Click += new System.EventHandler(this.butSurgClose_Click);
			// 
			// butAnesthOpen
			// 
			this.butAnesthOpen.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAnesthOpen.Autosize = true;
			this.butAnesthOpen.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAnesthOpen.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAnesthOpen.CornerRadius = 4F;
			this.butAnesthOpen.Location = new System.Drawing.Point(14, 17);
			this.butAnesthOpen.Name = "butAnesthOpen";
			this.butAnesthOpen.Size = new System.Drawing.Size(100, 26);
			this.butAnesthOpen.TabIndex = 7;
			this.butAnesthOpen.Text = "Anesthesia Open";
			this.butAnesthOpen.UseVisualStyleBackColor = true;
			this.butAnesthOpen.Click += new System.EventHandler(this.butAnesthOpen_Click);
			// 
			// butAnesthClose
			// 
			this.butAnesthClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAnesthClose.Autosize = true;
			this.butAnesthClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAnesthClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAnesthClose.CornerRadius = 4F;
			this.butAnesthClose.Location = new System.Drawing.Point(303, 17);
			this.butAnesthClose.Name = "butAnesthClose";
			this.butAnesthClose.Size = new System.Drawing.Size(100, 26);
			this.butAnesthClose.TabIndex = 13;
			this.butAnesthClose.Text = "Anesthesia Close";
			this.butAnesthClose.UseVisualStyleBackColor = true;
			this.butAnesthClose.Click += new System.EventHandler(this.butAnesthClose_Click);
			// 
			// butSurgOpen
			// 
			this.butSurgOpen.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSurgOpen.Autosize = true;
			this.butSurgOpen.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSurgOpen.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSurgOpen.CornerRadius = 4F;
			this.butSurgOpen.Location = new System.Drawing.Point(120, 17);
			this.butSurgOpen.Name = "butSurgOpen";
			this.butSurgOpen.Size = new System.Drawing.Size(86, 26);
			this.butSurgOpen.TabIndex = 9;
			this.butSurgOpen.Text = "Surgery Open";
			this.butSurgOpen.UseVisualStyleBackColor = true;
			this.butSurgOpen.Click += new System.EventHandler(this.butSurgOpen_Click);
			// 
			// butDose10
			// 
			this.butDose10.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose10.Autosize = true;
			this.butDose10.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose10.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose10.CornerRadius = 4F;
			this.butDose10.Location = new System.Drawing.Point(47, 136);
			this.butDose10.Name = "butDose10";
			this.butDose10.Size = new System.Drawing.Size(32, 32);
			this.butDose10.TabIndex = 67;
			this.butDose10.Text = "10";
			this.butDose10.UseVisualStyleBackColor = true;
			// 
			// butDose7
			// 
			this.butDose7.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose7.Autosize = true;
			this.butDose7.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose7.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose7.CornerRadius = 4F;
			this.butDose7.Location = new System.Drawing.Point(9, 22);
			this.butDose7.Name = "butDose7";
			this.butDose7.Size = new System.Drawing.Size(32, 32);
			this.butDose7.TabIndex = 57;
			this.butDose7.Text = "7";
			this.butDose7.UseVisualStyleBackColor = true;
			this.butDose7.Click += new System.EventHandler(this.butDose7_Click);
			// 
			// butDose8
			// 
			this.butDose8.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose8.Autosize = true;
			this.butDose8.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose8.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose8.CornerRadius = 4F;
			this.butDose8.Location = new System.Drawing.Point(47, 22);
			this.butDose8.Name = "butDose8";
			this.butDose8.Size = new System.Drawing.Size(32, 32);
			this.butDose8.TabIndex = 58;
			this.butDose8.Text = "8";
			this.butDose8.UseVisualStyleBackColor = true;
			// 
			// butDose9
			// 
			this.butDose9.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose9.Autosize = true;
			this.butDose9.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose9.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose9.CornerRadius = 4F;
			this.butDose9.Location = new System.Drawing.Point(85, 22);
			this.butDose9.Name = "butDose9";
			this.butDose9.Size = new System.Drawing.Size(32, 32);
			this.butDose9.TabIndex = 59;
			this.butDose9.Text = "9";
			this.butDose9.UseVisualStyleBackColor = true;
			this.butDose9.Click += new System.EventHandler(this.butDose9_Click);
			// 
			// butDose6
			// 
			this.butDose6.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose6.Autosize = true;
			this.butDose6.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose6.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose6.CornerRadius = 4F;
			this.butDose6.Location = new System.Drawing.Point(9, 60);
			this.butDose6.Name = "butDose6";
			this.butDose6.Size = new System.Drawing.Size(32, 32);
			this.butDose6.TabIndex = 60;
			this.butDose6.Text = "6";
			this.butDose6.UseVisualStyleBackColor = true;
			this.butDose6.Click += new System.EventHandler(this.butDose6_Click);
			// 
			// butDose5
			// 
			this.butDose5.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose5.Autosize = true;
			this.butDose5.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose5.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose5.CornerRadius = 4F;
			this.butDose5.Location = new System.Drawing.Point(47, 60);
			this.butDose5.Name = "butDose5";
			this.butDose5.Size = new System.Drawing.Size(32, 32);
			this.butDose5.TabIndex = 61;
			this.butDose5.Text = "5";
			this.butDose5.UseVisualStyleBackColor = true;
			this.butDose5.Click += new System.EventHandler(this.butDose5_Click);
			// 
			// butDose4
			// 
			this.butDose4.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose4.Autosize = true;
			this.butDose4.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose4.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose4.CornerRadius = 4F;
			this.butDose4.Location = new System.Drawing.Point(85, 60);
			this.butDose4.Name = "butDose4";
			this.butDose4.Size = new System.Drawing.Size(32, 32);
			this.butDose4.TabIndex = 62;
			this.butDose4.Text = "4";
			this.butDose4.UseVisualStyleBackColor = true;
			this.butDose4.Click += new System.EventHandler(this.butDose4_Click);
			// 
			// butDose3
			// 
			this.butDose3.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose3.Autosize = true;
			this.butDose3.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose3.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose3.CornerRadius = 4F;
			this.butDose3.Location = new System.Drawing.Point(9, 98);
			this.butDose3.Name = "butDose3";
			this.butDose3.Size = new System.Drawing.Size(32, 32);
			this.butDose3.TabIndex = 63;
			this.butDose3.Text = "3";
			this.butDose3.UseVisualStyleBackColor = true;
			this.butDose3.Click += new System.EventHandler(this.butDose3_Click);
			// 
			// butDose2
			// 
			this.butDose2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose2.Autosize = true;
			this.butDose2.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose2.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose2.CornerRadius = 4F;
			this.butDose2.Location = new System.Drawing.Point(47, 98);
			this.butDose2.Name = "butDose2";
			this.butDose2.Size = new System.Drawing.Size(32, 32);
			this.butDose2.TabIndex = 64;
			this.butDose2.Text = "2";
			this.butDose2.UseVisualStyleBackColor = true;
			this.butDose2.Click += new System.EventHandler(this.butDose2_Click);
			// 
			// butDose1
			// 
			this.butDose1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose1.Autosize = true;
			this.butDose1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose1.CornerRadius = 4F;
			this.butDose1.Location = new System.Drawing.Point(85, 98);
			this.butDose1.Name = "butDose1";
			this.butDose1.Size = new System.Drawing.Size(32, 32);
			this.butDose1.TabIndex = 65;
			this.butDose1.Text = "1";
			this.butDose1.UseVisualStyleBackColor = true;
			this.butDose1.Click += new System.EventHandler(this.butDose1_Click);
			// 
			// butDose0
			// 
			this.butDose0.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose0.Autosize = true;
			this.butDose0.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose0.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose0.CornerRadius = 4F;
			this.butDose0.Location = new System.Drawing.Point(9, 136);
			this.butDose0.Name = "butDose0";
			this.butDose0.Size = new System.Drawing.Size(32, 32);
			this.butDose0.TabIndex = 66;
			this.butDose0.Text = "0";
			this.butDose0.UseVisualStyleBackColor = true;
			this.butDose0.Click += new System.EventHandler(this.butDose0_Click);
			// 
			// butDose25
			// 
			this.butDose25.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose25.Autosize = true;
			this.butDose25.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose25.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose25.CornerRadius = 4F;
			this.butDose25.Location = new System.Drawing.Point(123, 22);
			this.butDose25.Name = "butDose25";
			this.butDose25.Size = new System.Drawing.Size(70, 32);
			this.butDose25.TabIndex = 68;
			this.butDose25.Text = ".25";
			this.butDose25.UseVisualStyleBackColor = true;
			this.butDose25.Click += new System.EventHandler(this.butDose25_Click);
			// 
			// butDose50
			// 
			this.butDose50.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose50.Autosize = true;
			this.butDose50.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose50.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose50.CornerRadius = 4F;
			this.butDose50.Location = new System.Drawing.Point(123, 60);
			this.butDose50.Name = "butDose50";
			this.butDose50.Size = new System.Drawing.Size(70, 32);
			this.butDose50.TabIndex = 69;
			this.butDose50.Text = ".50";
			this.butDose50.UseVisualStyleBackColor = true;
			this.butDose50.Click += new System.EventHandler(this.butDose50_Click);
			// 
			// butDoseWaste
			// 
			this.butDoseWaste.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDoseWaste.Autosize = true;
			this.butDoseWaste.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDoseWaste.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDoseWaste.CornerRadius = 4F;
			this.butDoseWaste.Location = new System.Drawing.Point(123, 98);
			this.butDoseWaste.Name = "butDoseWaste";
			this.butDoseWaste.Size = new System.Drawing.Size(70, 32);
			this.butDoseWaste.TabIndex = 70;
			this.butDoseWaste.Text = "Waste";
			this.butDoseWaste.UseVisualStyleBackColor = true;
			this.butDoseWaste.Click += new System.EventHandler(this.butDoseWaste_Click);
			// 
			// butDoseEnter
			// 
			this.butDoseEnter.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDoseEnter.Autosize = true;
			this.butDoseEnter.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDoseEnter.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDoseEnter.CornerRadius = 4F;
			this.butDoseEnter.Location = new System.Drawing.Point(123, 136);
			this.butDoseEnter.Name = "butDoseEnter";
			this.butDoseEnter.Size = new System.Drawing.Size(70, 32);
			this.butDoseEnter.TabIndex = 71;
			this.butDoseEnter.Text = "Enter";
			this.butDoseEnter.UseVisualStyleBackColor = true;
			this.butDoseEnter.Click += new System.EventHandler(this.butDoseEnter_Click);
			// 
			// butDoseDecPoint
			// 
			this.butDoseDecPoint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDoseDecPoint.Autosize = true;
			this.butDoseDecPoint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDoseDecPoint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDoseDecPoint.CornerRadius = 4F;
			this.butDoseDecPoint.Location = new System.Drawing.Point(85, 136);
			this.butDoseDecPoint.Name = "butDoseDecPoint";
			this.butDoseDecPoint.Size = new System.Drawing.Size(32, 32);
			this.butDoseDecPoint.TabIndex = 86;
			this.butDoseDecPoint.Text = ".";
			this.butDoseDecPoint.UseVisualStyleBackColor = true;
			this.butDoseDecPoint.Click += new System.EventHandler(this.butDoseDecPoint_Click);
			// 
			// FormAnestheticRecord
			// 
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(891, 732);
			this.Controls.Add(this.groupBoxVS);
			this.Controls.Add(this.groupBoxNotes);
			this.Controls.Add(this.groupBoxSidebarRt);
			this.Controls.Add(this.groupBoxAnesthMeds);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormAnestheticRecord";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Anesthetic Record";
			this.Load += new System.EventHandler(this.FormAnestheticRecord_Load);
			this.groupBoxSidebarRt.ResumeLayout(false);
			this.groupBoxSidebarRt.PerformLayout();
			this.groupBoxMonitors.ResumeLayout(false);
			this.groupBoxMonitors.PerformLayout();
			this.groupBoxIVSite.ResumeLayout(false);
			this.groupBoxIVSite.PerformLayout();
			this.groupBoxMedRoute.ResumeLayout(false);
			this.groupBoxMedRoute.PerformLayout();
			this.groupBoxDeliveryMethod.ResumeLayout(false);
			this.groupBoxDeliveryMethod.PerformLayout();
			this.groupBoxNotes.ResumeLayout(false);
			this.groupBoxNotes.PerformLayout();
			this.groupBoxWgt.ResumeLayout(false);
			this.groupBoxWgt.PerformLayout();
			this.groupBoxHgt.ResumeLayout(false);
			this.groupBoxHgt.PerformLayout();
			this.groupBoxSig.ResumeLayout(false);
			this.groupBoxHgtWgt.ResumeLayout(false);
			this.groupBoxHgtWgt.PerformLayout();
			this.groupBoxVS.ResumeLayout(false);
			this.groupBoxVS.PerformLayout();
			this.groupBoxTimes.ResumeLayout(false);
			this.groupBoxTimes.PerformLayout();
			this.groupBoxDoseCalc.ResumeLayout(false);
			this.groupBoxAnesthMeds.ResumeLayout(false);
			this.groupBoxAnesthMeds.PerformLayout();
			this.ResumeLayout(false);

		}
		public AnesthMedsGivens medCur;
		public FormAnestheticRecord(Patient patCur)
		

		{

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			PatCur = patCur;
			Lan.F(this);
			allowTopaz = (Environment.OSVersion.Platform != PlatformID.Unix && !CodeBase.ODEnvironment.Is64BitOperatingSystem());
			sigBox.SetTabletState(1);
			if (!allowTopaz)
			{
				//butTopazSign.Visible = false; //this throws an exception on x86
				sigBox.Visible = true;
			}
			else
			{
				//Add signature box for Topaz signatures.
				sigBoxTopaz = CodeBase.TopazWrapper.GetTopaz();
				sigBoxTopaz.Location = sigBox.Location;//this puts both boxes in the same spot.
				sigBoxTopaz.Name = "sigBoxTopaz";
				sigBoxTopaz.Size = new System.Drawing.Size(158, 74);
				sigBoxTopaz.TabIndex = 135;
				sigBoxTopaz.Text = "sigPlusNET";
				sigBoxTopaz.Visible = false;
				Controls.Add(sigBoxTopaz);
				//It starts out accepting input. It will be set to 0 if a sig is already present.  It will be set back to 1 if note changes or if user clicks Clear.
				CodeBase.TopazWrapper.SetTopazState(sigBoxTopaz, 1);
			}
		}

		private void FormAnestheticRecord_Load(object sender, EventArgs e)
		{	
			
			RefreshListAnesthetics();
			listAnesthetics.SelectedIndex = AnestheticRecords.List.Length - 1;
			
			//disables these buttons until AnesthOpen button is clicked. 
			butSurgOpen.Enabled = false;
			butSurgClose.Enabled = false;
			butAnesthClose.Enabled = false;
			butAnesthScore.Enabled = false;
			IsStartingUp = true;
			//display Patient name
			textPatient.Text = Patients.GetPat(PatCur.PatNum).GetNameFL();
			//display Patient ID number
			textPatID.Text = PatCur.PatNum.ToString();

			//RefreshListAnesthetics();

			//listAnesthetics.SelectedIndex = AnestheticRecords.List.Length - 1;//This works even if no items.......CLIENT........
			//listAnesthetics.SelectedIndex = 0;//Selects the first item in the listAnesthetics

			//.........  Client Code......................//
			//Fills provider and assistant comboboxes

			//Anesthetist comboBox
			
			comboAnesthetist.Items.Add(Lan.g(this, ""));
			for (int i = 0; i < ProviderC.List.Length; i++)
			{
				comboAnesthetist.Items.Add(ProviderC.List[i].LName + "," + ProviderC.List[i].FName);

				if (ProviderC.List[i].ProvNum == PatCur.PriProv)
					comboAnesthetist.SelectedIndex = i;
			}

			if (comboAnesthetist.SelectedIndex == -1)
			{
				int defaultindex = Providers.GetIndex(PrefC.GetInt("PriProv"));
				if (defaultindex == -1)
				{//default provider hidden
					comboAnesthetist.SelectedIndex = 0;
				}
				else
				{
					comboAnesthetist.SelectedIndex = defaultindex;
				}
			}
			 //Surgeon comboBox
			comboSurgeon.Items.Add(Lan.g(this, ""));
			for (int i = 0; i < ProviderC.List.Length; i++)
			{
			    comboSurgeon.Items.Add(ProviderC.List[i].LName + "," + ProviderC.List[i].FName);

			   if (ProviderC.List[i].ProvNum == PatCur.PriProv)
			        comboSurgeon.SelectedIndex = i;
			}

			if (comboSurgeon.SelectedIndex == -1)
			{
			    int defaultindex = Providers.GetIndex(PrefC.GetInt("PriProv"));
			    if (defaultindex == -1)
			    {//default provider hidden
			        comboSurgeon.SelectedIndex = 0;
			    }
			    else
			    {
			        comboSurgeon.SelectedIndex = defaultindex;
			    }
			}
			//Surgical assistant comboBox
			comboAsst.Items.Add(Lan.g(this, ""));
			for (int i = 0; i < ProviderC.List.Length; i++)
			{
			    comboAsst.Items.Add(ProviderC.List[i].LName + "," + ProviderC.List[i].FName);

			   if (ProviderC.List[i].ProvNum == PatCur.PriProv)
			        comboAsst.SelectedIndex = i;
			}

			if (comboAsst.SelectedIndex == -1)
			{
			    int defaultindex = Providers.GetIndex(PrefC.GetInt("PriProv"));
			    if (defaultindex == -1)
			    {//default provider hidden
			        comboAsst.SelectedIndex = 0;
			    }
			    else
			    {
			        comboAsst.SelectedIndex = defaultindex;
			    }
			}
			//Circulator comboBox
			comboCirc.Items.Add(Lan.g(this, ""));
			for (int i = 0; i < ProviderC.List.Length; i++)
			{
				comboCirc.Items.Add(ProviderC.List[i].LName + "," + ProviderC.List[i].FName);

				if (ProviderC.List[i].ProvNum == PatCur.PriProv)
					comboSurgeon.SelectedIndex = i;
			}

			if (comboCirc.SelectedIndex == -1)
			{
				int defaultindex = Providers.GetIndex(PrefC.GetInt("PriProv"));
				if (defaultindex == -1)
				{//default provider hidden
					comboCirc.SelectedIndex = 0;
				}
				else
				{
					comboCirc.SelectedIndex = defaultindex;
				}
			//FillControls(anestheticRecordCur);
			}


			//......... Code Client......................//
			//......... Code RH......................//
			/*this.comboAnesthMed.Items.Clear();
			this.comboAnesthMed.Items.Insert(0, "");
			int noOfRows = bindComboQueries.bindAMedName().Tables[0].Rows.Count;
			for (int i = 0; i <= noOfRows - 1; i++)
			{
				this.comboAnesthMed.Items.Add(bindComboQueries.bindAMedName().Tables[0].Rows[i][0].ToString());
				this.comboAnesthMed.SelectedIndex = 0;
			}
			//
			//Fills Anesthetist comboboxes
			//
			this.comboAnesthetist.Items.Clear();
			this.comboAnesthetist.Items.Insert(0, "");
			int noOfRows2 = bindComboQueries.bindDropDowns().Tables[0].Rows.Count;
			for (int i = 0; i <= noOfRows2 - 1; i++)
			{
				this.comboAnesthetist.Items.Add(bindComboQueries.bindDropDowns().Tables[0].Rows[i][0].ToString());
				this.comboAnesthetist.SelectedIndex = 0;
			}
			//
			//Fills Surgeon comboboxes
			//
			this.comboSurgeon.Items.Clear();
			this.comboSurgeon.Items.Insert(0, "");
			int noOfRows3 = bindComboQueries.bindDropDowns().Tables[0].Rows.Count;
			for (int i = 0; i <= noOfRows3 - 1; i++)
			{
				this.comboSurgeon.Items.Add(bindComboQueries.bindDropDowns().Tables[0].Rows[i][0].ToString());
				this.comboSurgeon.SelectedIndex = 0;
			}
			//
			//Fills Assistant comboboxes
			//
			this.comboAsst.Items.Clear();
			this.comboAsst.Items.Insert(0, "");
			int noOfRows4 = bindComboQueries.bindDropDowns().Tables[0].Rows.Count;
			for (int i = 0; i <= noOfRows4 - 1; i++)
			{
				this.comboAsst.Items.Add(bindComboQueries.bindDropDowns().Tables[0].Rows[i][0].ToString());
				this.comboAsst.SelectedIndex = 0;
			}
			//
			//Fills Circulator comboboxes
			//
			this.comboCirc.Items.Clear();
			this.comboCirc.Items.Insert(0, "");
			int noOfRows5 = bindComboQueries.bindDropDowns().Tables[0].Rows.Count;
			for (int i = 0; i <= noOfRows5 - 1; i++)
			{
				this.comboCirc.Items.Add(bindComboQueries.bindDropDowns().Tables[0].Rows[i][0].ToString());
				this.comboCirc.SelectedIndex = 0;
			}*/

			FillGridAnesthMeds();

			}


		private void RefreshListAnesthetics(){

			//most recent date at the top
			AnestheticRecords.Refresh(PatCur.PatNum);
			listAnesthetics.Items.Clear();

			for (int i = 0; i < AnestheticRecords.List.Length; i++)
			{

				listAnesthetics.Items.Add(AnestheticRecords.List[i].AnestheticDate);
				anestheticRecordCur = AnestheticRecords.List[i].AnestheticRecordNum;
			}
		}

		//Load saved data into form for selected Anesthetic Record
		private void FillControls(int anestheticRecordCur){

			string command =
	"SELECT * "                   
	+ " FROM anestheticdata"					
	+ " WHERE AnestheticRecordNum = " + anestheticRecordCur;
	
			DataTable table = General.GetTable(command);
			AnestheticData Cur;
			for (int i = 0; i < table.Rows.Count; i++)
			{
				Cur = new AnestheticData();
				Cur.AnestheticDataNum = PIn.PInt(table.Rows[i][0].ToString());
				Cur.AnestheticRecordNum = PIn.PInt(table.Rows[i][1].ToString());
				Cur.AnesthOpen = PIn.PString(table.Rows[i][2].ToString());
				Cur.AnesthClose = PIn.PString(table.Rows[i][3].ToString());
				Cur.SurgOpen = PIn.PString(table.Rows[i][4].ToString());
				Cur.SurgClose = PIn.PString(table.Rows[i][5].ToString());
				Cur.Anesthetist = PIn.PString(table.Rows[i][6].ToString());
				Cur.Surgeon = PIn.PString(table.Rows[i][7].ToString());
				Cur.Asst = PIn.PString(table.Rows[i][8].ToString());
				Cur.Circulator = PIn.PString(table.Rows[i][9].ToString());
				Cur.VSMName = PIn.PString(table.Rows[i][10].ToString());
				Cur.VSMSerNum = PIn.PString(table.Rows[i][11].ToString());
				Cur.ASA = PIn.PString(table.Rows[i][12].ToString());
				Cur.ASA_EModifier = PIn.PString(table.Rows[i][13].ToString());
				Cur.InhO2 = PIn.PBool(table.Rows[i][14].ToString());
				Cur.InhN2O = PIn.PBool(table.Rows[i][15].ToString());
				Cur.O2LMin = PIn.PInt(table.Rows[i][16].ToString());
				Cur.N2OLMin = PIn.PInt(table.Rows[i][17].ToString());
				Cur.RteNasCan = PIn.PBool(table.Rows[i][18].ToString());
				Cur.RteNasHood = PIn.PBool(table.Rows[i][19].ToString());
				Cur.RteETT = PIn.PBool(table.Rows[i][20].ToString());
				Cur.MedRouteIVCath = PIn.PBool(table.Rows[i][21].ToString());
				Cur.MedRouteIVButtFly = PIn.PBool(table.Rows[i][22].ToString());
				Cur.MedRouteIM = PIn.PBool(table.Rows[i][23].ToString());
				Cur.MedRoutePO = PIn.PBool(table.Rows[i][24].ToString());
				Cur.MedRouteNasal = PIn.PBool(table.Rows[i][25].ToString());
				Cur.MedRouteRectal = PIn.PBool(table.Rows[i][26].ToString());
				Cur.IVSite = PIn.PString(table.Rows[i][27].ToString());
				Cur.IVGauge = PIn.PInt(table.Rows[i][28].ToString());
				Cur.IVSideR = PIn.PBool(table.Rows[i][29].ToString());
				Cur.IVSideL = PIn.PBool(table.Rows[i][30].ToString());
				Cur.IVAtt = PIn.PInt(table.Rows[i][31].ToString());
				Cur.IVF = PIn.PString(table.Rows[i][32].ToString());
				Cur.IVFVol = PIn.PInt(table.Rows[i][33].ToString());
				Cur.MonBP = PIn.PBool(table.Rows[i][34].ToString());
				Cur.MonSpO2 = PIn.PBool(table.Rows[i][35].ToString());
				Cur.MonEtCO2 = PIn.PBool(table.Rows[i][36].ToString());
				Cur.MonTemp = PIn.PBool(table.Rows[i][37].ToString());
				Cur.MonPrecordial = PIn.PBool(table.Rows[i][38].ToString());
				Cur.MonEKG = PIn.PBool(table.Rows[i][39].ToString());
				Cur.Notes = PIn.PString(table.Rows[i][40].ToString());
				Cur.PatWgt = PIn.PInt(table.Rows[i][41].ToString());
				Cur.WgtUnitsLbs = PIn.PBool(table.Rows[i][42].ToString());
				Cur.WgtUnitsKgs = PIn.PBool(table.Rows[i][43].ToString());
				Cur.PatHgt = PIn.PInt(table.Rows[i][44].ToString());
				Cur.EscortName = PIn.PString(table.Rows[i][45].ToString());
				Cur.EscortCellNum = PIn.PString(table.Rows[i][46].ToString());
				Cur.EscortRel = PIn.PString(table.Rows[i][47].ToString());
				Cur.NPOTime = PIn.PString(table.Rows[i][48].ToString());
				//Cur.SigIsTopaz= PIn.PBool(table.Rows[i][50].ToString());
				//Cur.Signature = PIn.PString(table.Rows[i][51].ToString());
				Cur.HgtUnitsIn = PIn.PBool(table.Rows[i][49].ToString());
				Cur.HgtUnitsCm = PIn.PBool(table.Rows[i][50].ToString());
				
				//Disable buttons to disallow editing
				butAnesthOpen.Enabled = false;
				butSurgOpen.Enabled = false;
				butSurgClose.Enabled = false;
				butAnesthClose.Enabled = false;
				butAnesthScore.Enabled = false;
				butDoseEnter.Enabled = false;
				butDoseWaste.Enabled = false;
				butDelAnesthMeds.Enabled = false;
				butWasteQty.Enabled = false;
				butAnesthScore.Enabled = false;

				//Populate controls from db

				textAnesthOpen.Text = Cur.AnesthOpen;
				textSurgOpen.Text = Cur.SurgOpen;
				textSurgClose.Text = Cur.SurgClose;
				textAnesthClose.Text = Cur.AnesthClose;
				//comboAnesthetist
				comboAnesthetist.SelectedItem = Cur.Anesthetist.ToString();
				//comboSurgeon
				comboSurgeon.SelectedItem = Cur.Surgeon.ToString();
				//comboAsst
				comboAsst.SelectedItem = Cur.Asst.ToString();
				//comboCirc
				comboCirc.SelectedItem = Cur.Circulator.ToString();

				//VSM, set to "" if no networked monitor has written its name to the db
				if (Cur.VSMName == null)
				{
					textVSMName.Text = "";
				}
				else
				{
					textVSMName.Text = Cur.VSMName;
				}
				//VSMSerNum, set to "" if no networked monitor has written its serial number to the db
				if (Cur.VSMSerNum == null)
				{
					textVSMSerNum.Text = "";
				}
				else
				{
					textVSMSerNum.Text = Cur.VSMSerNum;
				}
				//load comboASA

				comboASA.SelectedItem = Cur.ASA;
				
				//load comboASA_EModifier
					if (Cur.ASA_EModifier == "E")
					{
						comboASA_EModifier.SelectedIndex = 1;
					}
				//load checkInhO2
					if (Cur.InhO2 == true)
					{
						checkInhO2.Checked = true;

					}
					
				//load checkInhN2O
					if (Cur.InhN2O == true)
					{
						checkInhN2O.Checked = true;

					}
				//comboO2LMin
					comboO2LMin.SelectedIndex = Convert.ToInt32(Cur.O2LMin)-1;
				//comboN2OLMin
					comboN2OLMin.SelectedIndex = Convert.ToInt32(Cur.N2OLMin)-1;
				//radRteNasCan
				if (Cur.RteNasCan == true)
					{
						radRteNasCan.Checked = true;
					}
				//radNasHood
				if (Cur.RteNasHood == true)
				{
					radRteNasHood.Checked = true;
				}

				//radRteETT
				if (Cur.RteETT == true)
				{
					radRteETT.Checked = true;
				}
				//radMedRouteIVCath
				if (Cur.MedRouteIVCath == true)
				{
					radMedRouteIVCath.Checked = true;
				}
				//radMedRouteIVButtFly
				if (Cur.MedRouteIVButtFly == true)
				{
					radMedRouteIVButtFly.Checked = true;
				}
				//radMedRouteIM
				if (Cur.MedRouteIM == true)
				{
					radMedRouteIM.Checked = true;
				}
				//radMedRoutePO
				if (Cur.MedRoutePO == true)
				{
					radMedRoutePO.Checked = true;
				}
				//radMedRouteNasal
				if (Cur.MedRouteNasal == true)
				{
					radMedRouteNasal.Checked = true;
				}
				//radMedRouteRectal
				if (Cur.MedRouteRectal == true)
				{
					radMedRouteRectal.Checked = true;
				}
				//comboIVSite
				comboIVSite.SelectedItem = Cur.IVSite.ToString();
				//combIVGauge
				comboIVGauge.SelectedItem = Cur.IVGauge.ToString();
				//radIVSideR
				if (Cur.IVSideR == true)
				{
					radIVSideR.Checked = true;
				}
				//radIVSideL
				if (Cur.IVSideL == true)
				{
					radIVSideL.Checked = true;
				}
				
				//comboIVAtt
				comboIVAtt.SelectedItem = Cur.IVAtt.ToString();
				
				//comboIVF
				comboIVF.SelectedItem = Cur.IVF.ToString();

				//textIVFVol
				textIVFVol.Text = Cur.IVFVol.ToString();

				//checkMonBP
				if (Cur.MonBP == true)
				{
					checkMonBP.Checked = true;
				}
				//checkMonSpO2
				if (Cur.MonSpO2 == true)
				{
					checkMonSpO2.Checked = true;
				}
				//checkMonEtCO2
				if (Cur.MonEtCO2 == true)
				{
					checkMonEtCO2.Checked = true;
				}
				//checkMonTemp
				if (Cur.MonTemp == true)
				{
					checkMonTemp.Checked = true;
				}
				//checkMonPrecordial
				if (Cur.MonPrecordial == true)
				{
					checkMonPrecordial.Checked = true;
				}
				//checkMonEKG
				if (Cur.MonEKG == true)
				{
					checkMonEKG.Checked = true;
				}
				//notes
				richTextNotes.Text = Cur.Notes;

				//PatWgt
				textPatWgt.Text = Cur.PatWgt.ToString();

				//radWgtUnitsLbs
				if (Cur.WgtUnitsLbs == true)
				{
					radWgtUnitsLbs.Checked = true;
				}

				//comboWgtUnitsKgs
				if (Cur.WgtUnitsKgs == true)
				{
					radWgtUnitsKgs.Checked = true;
				}
				textPatHgt.Text = Cur.PatHgt.ToString();
				//radHgtUnitsIn
				if (Cur.HgtUnitsIn == true)
				{
					radHgtUnitsIn.Checked = true;
				}
				//radHgtUnitsCm
				textEscortName.Text = Cur.EscortName;
				textEscortCellNum.Text = Cur.EscortCellNum;
				textEscortRel.Text = Cur.EscortRel;
				//comboNPOTime
				comboNPOTime.SelectedItem = Cur.NPOTime.ToString();
				
				
			}
		}
			private void listAnesthetics_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {

			FillControls(anestheticRecordCur);
		
		}

		/*private void FillGridAnesthMeds()
		{

			//listAnestheticMedsGiven = AnestheticMedsGiven.CreateObjects();
			//AnestheticMedsGiven.RefreshCache(); //Don't know if we need this
			gridAnesthMeds.BeginUpdate();
			gridAnesthMeds.Columns.Clear();
			ODGridColumn col = new ODGridColumn(Lan.g(this, "Medication"), 130);
			gridAnesthMeds.Columns.Add(col);
			col = new ODGridColumn(Lan.g(this, "Dose (mL)"), 90);
			gridAnesthMeds.Columns.Add(col);
			col = new ODGridColumn(Lan.g(this, "Waste(mL)"), 90);
			gridAnesthMeds.Columns.Add(col);
			col = new ODGridColumn(Lan.g(this, "DoseTimeStamp"), 120);
			gridAnesthMeds.Columns.Add(col);
			gridAnesthMeds.Rows.Clear();
			ODGridRow row;
			string txt;
			for (int i = 0; i < AnestheticMedsGivenC.Listt.Count; i++)
			{
				row = new ODGridRow();
				row.Cells.Add(AnestheticMedsGivenC.Listt[i].AnesthMedName);
				row.Cells.Add(AnestheticMedsGivenC.Listt[i].QtyGiven);
				row.Cells.Add(AnestheticMedsGivenC.Listt[i].QtyWasted);
				txt = AnestheticMedsGivenC.Listt[i].DoseTimeStamp;
				row.Cells.Add(txt);
				gridAnesthMeds.Rows.Add(row);
			}
			gridAnesthMeds.EndUpdate();
		}*/

			//this really needs to be in the FillControl() method when we get it added
			/*labelInvalidSig.Visible=false;
			sigBox.Visible=true;
			if (AnestheticDataCur.SigIsTopaz)
			{
				if (AnestheticDataCur.Signature != "")
				{
					if (allowTopaz)
					{
						sigBox.Visible = false;
						sigBoxTopaz.Visible = true;
						CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz);
						CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz, 0);
						CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz, 0);
						CodeBase.TopazWrapper.SetTopazKeyString(sigBoxTopaz, "0000000000000000");
						CodeBase.TopazWrapper.SetTopazAutoKeyData(sigBoxTopaz, AnestheticDataCur.AnesthClose + AnestheticDataCur.AnestheticRecordNum.ToString());
						CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz, 2);//high encryption
						CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz, 2);//high compression
						CodeBase.TopazWrapper.SetTopazSigString(sigBoxTopaz, AnestheticDataCur.Signature);
						/* if (CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(sigBoxTopaz) == 0)
						{
							labelInvalidSig.Visible = true;
						}
					}
				}
			}
			else
			{
				if (AnestheticDataCur.Signature != null && AnestheticDataCur.Signature != "")
				{
					sigBox.Visible = true;
					sigBoxTopaz.Visible = false;
					sigBox.ClearTablet();
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString("0000000000000000");
					sigBox.SetAutoKeyData(AnestheticDataCur.AnesthClose + AnestheticDataCur.AnestheticRecordNum.ToString());
					//sigBox.SetEncryptionMode(2);//high encryption
					//sigBox.SetSigCompressionMode(2);//high compression
					sigBox.SetSigString(AnestheticDataCur.Signature);
					/* if (sigBox.NumberOfTabletPoints() == 0)
					{
						labelInvalidSig.Visible = true;
					}
					sigBox.SetTabletState(0);//not accepting input.  To accept input, change the note, or clear the sig.
				}
			}
		}*/

		private void butAddAnesthetic_Click(object sender, EventArgs e)
		{

			AnestheticRecordCur = new AnestheticRecord();
			AnestheticRecordCur.PatNum = PatCur.PatNum;
			AnestheticRecordCur.AnestheticDate = DateTime.Now;
			AnestheticRecordCur.ProvNum = PatCur.PriProv;
			AnestheticRecords.Insert(AnestheticRecordCur);
			RefreshListAnesthetics();
			listAnesthetics.SelectedIndex = AnestheticRecords.List.Length - 1;//Add -1 after List.Length to select in ascending order
			//listAnesthetics.SelectedIndex = 0;
		}

		private void butDelAnesthetic_Click(object sender, System.EventArgs e)
		{
			if (listAnesthetics.SelectedIndex == -1)
			{
				MessageBox.Show(Lan.g(this, "Please select an item first."));
				return;
			}
			if (MessageBox.Show(Lan.g(this, "Delete Anesthetic?"), "", MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			//deletes an Anesthetic from the list of saved Anesthetics
			Userod curUser = Security.CurUser;

			if (GroupPermissions.HasPermission(curUser.UserGroupNum, Permissions.AnesthesiaControlMeds)){
				
			//if (Security.IsAuthorized(Permissions.AnesthesiaControlMeds)) //this works too
        
				AnestheticRecords.Delete(AnestheticRecords.List[listAnesthetics.SelectedIndex]);
				RefreshListAnesthetics();
				return;
			}

			else{

				MessageBox.Show(this, "You must be an administrator to unlock this action");
				return;
              
			}
	


		}

		private void butCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void FillGridAnesthMeds()
		{
			AnesthMedsGivens.RefreshCache();
			gridAnesthMeds.BeginUpdate();
			gridAnesthMeds.Columns.Clear();
			ODGridColumn col = new ODGridColumn(Lan.g(this, "Medication"), 130);
			gridAnesthMeds.Columns.Add(col);
			col = new ODGridColumn(Lan.g(this, "Dose"),60);
			gridAnesthMeds.Columns.Add(col);
			col = new ODGridColumn(Lan.g(this, "Waste"), 60);
			gridAnesthMeds.Columns.Add(col);
			col = new ODGridColumn(Lan.g(this, "TimeStamp"),50);
			gridAnesthMeds.Columns.Add(col);
			gridAnesthMeds.Rows.Clear();
			ODGridRow row;
			for (int i = 0; i < AnestheticMedsGivenC.Listt.Count; i++)
			{
				row = new ODGridRow();
				row.Cells.Add(AnestheticMedsGivenC.Listt[i].AnesthMedName);
				row.Cells.Add(AnestheticMedsGivenC.Listt[i].QtyGiven);
				row.Cells.Add(AnestheticMedsGivenC.Listt[i].QtyWasted);
				row.Cells.Add(AnestheticMedsGivenC.Listt[i].DoseTimeStamp);
				gridAnesthMeds.Rows.Add(row);
			}
			gridAnesthMeds.EndUpdate();
		}

		private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void checkBox7_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void label26_Click(object sender, EventArgs e)
		{

		}


		private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
		{

		}



		private void listAnesthetics_SelectedIndexChanged(object sender, EventArgs e)
		{
			FillControls(anestheticRecordCur);

		}

		private void sigBox_Click(object sender, EventArgs e)
		{

		}

		private void button20_Click(object sender, EventArgs e)
		{

		}

		private void Text1_TextChanged(object sender, EventArgs e)
		{

		}

		private void Text3_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBoxSurgOpen_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBoxAnesthClose_TextChanged(object sender, EventArgs e)
		{
			//CheckForCompleteNote();
			/*if (!IsStartingUp//so this happens only if user changes the note
				&& !SigChanged)//and the original signature is still showing.
				{
					sigBox.ClearTablet();
					if (allowTopaz)
						{
							CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz);
							sigBoxTopaz.Visible = false;//until user explicitly starts it.
				}
				sigBox.SetTabletState(1);//on-screen box is now accepting input.
				SigChanged = true;
				// AnestheticDataCur.UserNum = Security.CurUser.UserNum;
				// textUser.Text = Userods.GetName(ProcCur.UserNum);
				}*/
		}

		private void textBoxSurgClose_TextChanged_1(object sender, EventArgs e)
		{

		}

		private void butAnesthOpen_Click(object sender, EventArgs e)
		{
			butAnesthOpen.Enabled = false;
			textAnesthOpen.Text = MiscData.GetNowDateTime().ToString("hh:mm:ss tt"); //tt shows "AM/PM", change to "HH:mm:ss" for military time
			//To enable Surgeon Open Button
			butSurgOpen.Enabled = true;
			textSurgOpen.Enabled = true;
        }

		private void butSurgOpen_Click(object sender, EventArgs e)
		{
			butSurgOpen.Enabled = false;
			textSurgOpen.Text = MiscData.GetNowDateTime().ToString("hh:mm:ss tt");
			//To enable Surgeon Close Button 
			butSurgClose.Enabled = true;
			textSurgClose.Enabled = true;
			butAnesthOpen.Enabled = false;
		}

		private void butSurgClose_Click(object sender, EventArgs e)
		{	
			butSurgOpen.Enabled = false;
			textSurgClose.Text = MiscData.GetNowDateTime().ToString("hh:mm:ss tt");
			//To enable Anesthetic Close Button 
			butAnesthClose.Enabled = true;
			textAnesthClose.Enabled = true;
			butSurgClose.Enabled = false;
			
		}

		private void butAnesthClose_Click(object sender, EventArgs e)
		{	butSurgClose.Enabled = false;
			textAnesthClose.Text = MiscData.GetNowDateTime().ToString("hh:mm:ss tt");
			//To enable Anesthetic Score Button 
			butAnesthScore.Enabled = true;
			butAnesthClose.Enabled = false;
			butAnesthScore.Focus();
		}

		private void butDelAnesthMeds_Click(object sender, EventArgs e)
		{
			Userod curUser = Security.CurUser;

			if (GroupPermissions.HasPermission(curUser.UserGroupNum, Permissions.AnesthesiaControlMeds))
			{

				return;

			}
			else
			{
				MessageBox.Show(this, "You must be an administrator to unlock this action");
				return;
			}
			//AnestheticMedsGiven MedCur = medCur;
			//AnesthMedsGiven.DeleteObject(MedCur);
			//DialogResult = DialogResult.OK;
			/*if (textAnesthDose.Text != "" && comboAnesthMed.SelectedIndex != 0)
			{
				AMedication.deleteRow(AnesthMedName.Text, QtyGiven.Text, DoseTimeStamp.Text));
			}*/
		}

		private void butAddAnesthMeds_Click(object sender, EventArgs e)
		{

		}

        //Print Button Functionality.
        private void butPrint_Click(object sender, EventArgs e)
        {
			Graphics g1 = this.CreateGraphics();
			Image MyImage = new Bitmap(778, 741, g1);
			Graphics g2 = Graphics.FromImage(MyImage);
			IntPtr dc1 = g1.GetHdc();
			IntPtr dc2 = g2.GetHdc();
			BitBlt(dc2, 0, 0, 778, 741, dc1, 0, 0, 13369376);
			g1.ReleaseHdc(dc1);
			g2.ReleaseHdc(dc2);
			MyImage.Save(@"c:\PrintPage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
			FileStream fileStream = new FileStream(@"c:\PrintPage.jpg", FileMode.Open, FileAccess.Read);
			StartPrint(fileStream, "Image");
			fileStream.Close();
			if (System.IO.File.Exists(@"c:\PrintPage.jpg"))
			{
				System.IO.File.Delete(@"c:\PrintPage.jpg");
			}
        }
		public void StartPrint(Stream streamToPrint, string streamType)
		{
			/* this.printDocument2.PrintPage += new PrintPageEventHandler(printDocument2_PrintPage);
			this.streamToPrint = streamToPrint;
			this.streamType = streamType;
			System.Windows.Forms.PrintDialog PrintDialog1 = new PrintDialog();
			PrintDialog1.AllowSomePages = true;
			PrintDialog1.ShowHelp = true;
			PrintDialog1.Document = printDocument2;
			DialogResult result = PrintDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				printDocument2.Print();
			}*/
        }

		private void label3_Click(object sender, EventArgs e)
		{

		}


		private void groupBox5_Enter(object sender, EventArgs e)
		{

		}


		private void textBoxPatHgt_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBoxPatWgt_TextChanged(object sender, EventArgs e)
		{

		}

		private void groupBoxHgtWgt_Enter(object sender, EventArgs e)
		{

		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

		private void comboAnesthMed_SelectedIndexChanged(object sender, EventArgs e)
		{
			textAnesthDose.Enabled = true;
			textAnesthDose.ReadOnly = false;
		}

		private void textBoxPatient_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBoxPatID_TextChanged(object sender, EventArgs e)
		{

		}

		private void labelAnesthetist_Click(object sender, EventArgs e)
		{

		}

		private void butSignTopaz_Click(object sender, EventArgs e)
		{
			sigBox.Visible = false;
			sigBoxTopaz.Visible = true;
			if (allowTopaz)
			{
				CodeBase.TopazWrapper.SetTopazState(sigBoxTopaz, 1);
			}
			SigChanged = true;
			labelInvalidSig.Visible = false;
			//AnestheticDataCur.UserNum = Security.CurUser.UserNum;
			//textUser.Text = Userods.GetName(AnestheticDataCur.UserNum);
		}

		private void sigBox_MouseUp(object sender, MouseEventArgs e)
		{
			//this is done on mouse up so that the initial pen capture won't be delayed.
			if (sigBox.GetTabletState() == 1//if accepting input.
				&& !SigChanged)//and sig not changed yet
			{
				//sigBox handles its own pen input.
				SigChanged = true;
				//AnestheticDataCur.UserNum = Security.CurUser.UserNum;
				//textUser.Text = Userods.GetName(AnestheticDataCur.UserNum);
			}
		}
		private void butClearSig_Click(object sender, EventArgs e)
		{
			sigBox.ClearTablet();
			sigBox.Visible = true;
			if (allowTopaz)
			{
				CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz);
				sigBoxTopaz.Visible = false;//until user explicitly starts it.
			}
			sigBox.SetTabletState(1);//on-screen box is now accepting input.
			SigChanged = true;
			labelInvalidSig.Visible = false;
			//AnestheticDataCur.UserNum = Security.CurUser.UserNum;
			//textUser.Text = Userods.GetName(AnestheticDataCur.UserNum);
		}

		private void butDoseDecPoint_Click(object sender, EventArgs e)
		{
			if (textAnesthDose.Text.Contains("."))
			{
				hasDecimal = true;
			}
			else
			{
				hasDecimal = false;
			}
			if (inputStatus)
			{
				if (!hasDecimal)
				{
					if (textAnesthDose.Text.Length != 0 && (!textAnesthDose.ReadOnly))
					{
						textAnesthDose.Text += butDoseDecPoint.Text;
						hasDecimal = true;

					}
					else
					{
						textAnesthDose.Text = "0.";
					}
				}
			}
		}

		private void butDose0_Click(object sender, EventArgs e)
		{
			//Append '0' to textAnesthDose
			if ((!textAnesthDose.ReadOnly))
				textAnesthDose.Text += butDose0.Text;
		}

		private void butDose1_Click(object sender, EventArgs e)
		{
			//Append '1' to textAnesthDose
			if ((!textAnesthDose.ReadOnly))
				textAnesthDose.Text += butDose1.Text;
		}

		private void butDose2_Click(object sender, EventArgs e)
		{
			//Append '2' to textAnesthDose
			if ((!textAnesthDose.ReadOnly))
				textAnesthDose.Text += butDose2.Text;
		}

		private void butDose3_Click(object sender, EventArgs e)
		{
			//Append '3' to textAnesthDose
			if ((!textAnesthDose.ReadOnly))
				textAnesthDose.Text += butDose3.Text;
		}

		private void butDose4_Click(object sender, EventArgs e)
		{
			//Append '4' to textAnesthDose
			if ((!textAnesthDose.ReadOnly))
				textAnesthDose.Text += butDose4.Text;
		}

		private void butDose5_Click(object sender, EventArgs e)
		{
			//Append '5' to textAnesthDose
			if ((!textAnesthDose.ReadOnly))
				textAnesthDose.Text += butDose5.Text;
		}

		private void butDose6_Click(object sender, EventArgs e)
		{
			//Append '6' to textAnesthDose
			if ((!textAnesthDose.ReadOnly))
				textAnesthDose.Text += butDose6.Text;
		}

		private void butDose7_Click(object sender, EventArgs e)
		{
			//Append '7' to textAnesthDose
			if ((!textAnesthDose.ReadOnly))
				textAnesthDose.Text += butDose7.Text;
		}

		private void butDose8_Click(object sender, EventArgs e)
		{
			//Append '8' to textAnesthDose
			if ((!textAnesthDose.ReadOnly))
				textAnesthDose.Text += butDose8.Text;
		}

		private void butDose9_Click(object sender, EventArgs e)
		{
			//Append '9' to textAnesthDose
			if ((!textAnesthDose.ReadOnly))
				textAnesthDose.Text += butDose9.Text;
		}

		private void butDose10_Click(object sender, EventArgs e)
		{
			//Append '10' to textAnesthDose
			if ((!textAnesthDose.ReadOnly))
				textAnesthDose.Text += butDose10.Text;
		}

		private void butDose25_Click(object sender, EventArgs e)
		{
			//Append '25' to textAnesthDose
			if ((!textAnesthDose.ReadOnly) && (!textAnesthDose.Text.Contains(".")))
				textAnesthDose.Text += butDose25.Text;
		}

		private void butDose50_Click(object sender, EventArgs e)
		{
			//Append '50' to textAnesthDose
			if ((!textAnesthDose.ReadOnly) && (!textAnesthDose.Text.Contains(".")))
				textAnesthDose.Text += butDose50.Text;
		}

		private void butDoseEnter_Click(object sender, EventArgs e)
		{
			patNum = PatCur.PatNum;
			 if (textAnesthDose.Text != "" && comboAnesthMed.SelectedIndex != 0)
			{
				AMedications.InsertAMedDose(comboAnesthMed.SelectedItem.ToString(), Convert.ToDecimal(textAnesthDose.Text), Convert.ToInt32(patNum));
			}
			else
			{
				if (textAnesthDose.Text == "")
					{
						MessageBox.Show("Dose Field Can Not Be Empty");
						textAnesthDose.Focus();
					}
				else if (comboAnesthMed.SelectedIndex == 0)
				{
					MessageBox.Show("Please select an Anesthetic Medication");
					comboAnesthMed.Focus();
				}
			}
			//Regular Expression to validate the format of the entered value in the textAnesthDose
			Regex regex = new Regex("^\\d{1,4}(\\.\\d{1,2})?$");
			if (!(regex.IsMatch(textAnesthDose.Text)) && textAnesthDose.Text != "")
			{
				MessageBox.Show("Dose should be xxxx.xx format");
				textAnesthDose.Focus();
			}
			FillGridAnesthMeds();
			textAnesthDose.Text = "";
			comboAnesthMed.SelectedIndex = 0;
		}

		private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
		{
			System.Drawing.Image image = System.Drawing.Image.FromStream(this.streamToPrint);
			int x = e.MarginBounds.X;
			int y = e.MarginBounds.Y;
			int width = image.Width;
			int height = image.Height;
			if ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
			{
				width = e.MarginBounds.Width;
				height = image.Height * e.MarginBounds.Width / image.Width;
			}
			else
			{
				height = e.MarginBounds.Height;
				width = image.Width * e.MarginBounds.Height / image.Height;
			}
			System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(x, y, width, height);
			e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
		 }

		private void FormAnestheticRecord_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (listAnesthetics.SelectedIndex != -1)
			{
				//GridPSaveCurAnesthetic(AnestheticRecordCur.AnestheticRecordNum);
			}
		}

		private void textBoxAnesthOpen_TextChanged(object sender, EventArgs e)
		{

		}


		private void labelRoute_Click(object sender, EventArgs e)
		{

		}

		private void butAnesthScore_Click(object sender, EventArgs e)
		{

			FormAnesthesiaScore FormAS = new FormAnesthesiaScore(PatCur);
			FormAS.ShowDialog();

		}




		private void butWasteQty_Click(object sender, EventArgs e)
        {

                /*FormAnesthElevateSecurityPriv FormES = new FormAnesthElevateSecurityPriv();
                FormES.ShowDialog();*/
			
				Userod curUser = Security.CurUser;
				
				if (GroupPermissions.HasPermission(curUser.UserGroupNum, Permissions.AnesthesiaControlMeds))
				{
					
					FormAnestheticMedsWasteQty FormW = new FormAnestheticMedsWasteQty();
					FormW.ShowDialog();
		
					return;
					
				}
				else
				{
					FormAnesthElevateSecurityPriv FormES = new FormAnesthElevateSecurityPriv();
					FormES.ShowDialog();
					//MessageBox.Show(this, "You must be an administrator to unlock this action");
					return;
				}
			    
					
		}

		private void butOK_Click(object sender, EventArgs e)
		{

			Userod curUser = Security.CurUser;

			if (GroupPermissions.HasPermission(curUser.UserGroupNum, Permissions.AnesthesiaControlMeds))
			{

				DialogResult=DialogResult.OK;
				return;

			}
			else
			{
				//FormAnesthElevateSecurityPriv ES = new FormAnesthElevateSecurityPriv();
				//ShowDialog = FormES
				//MessageBox.Show(this, "You must be an administrator to unlock this action");
				return;
			}

		}

		private void button1_Click(object sender, EventArgs e)
		{

		}

		private void comboBoxO2LMin_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void textEscortName_TextChanged(object sender, EventArgs e)
		{

		}

		private void textEscortCellNum_TextChanged(object sender, EventArgs e)
		{
			int cursor = textEscortCellNum.SelectionStart;
			int length = textEscortCellNum.Text.Length;
			textEscortCellNum.Text = TelephoneNumbers.AutoFormat(textEscortCellNum.Text);
			if (textEscortCellNum.Text.Length > length)
				cursor++;
			textEscortCellNum.SelectionStart = cursor;

		}

        private void gridAnesthMeds_CellDoubleClick(object sender, ODGridClickEventArgs e)
        {

        }

        private void gridVitalSigns_CellDoubleClick(object sender, ODGridClickEventArgs e)
        {

        }


		private void butDoseWaste_Click(object sender, EventArgs e)
		{
			Userod curUser = Security.CurUser;

			if (GroupPermissions.HasPermission(curUser.UserGroupNum, Permissions.AnesthesiaControlMeds))
			{

				FormAnestheticMedsWasteQty FormW = new FormAnestheticMedsWasteQty();
				FormW.ShowDialog();

				return;

			}
			else
			{
				MessageBox.Show(this, "You must be an administrator to unlock this action");
				return;
			}
		}

		private void checkBP_CheckedChanged(object sender, EventArgs e)
		{

		}
		private void SaveSignature()
		{
			if (SigChanged)
			{
				//Topaz boxes are written in Windows native code.
				if (allowTopaz && sigBoxTopaz.Visible)
				{
					AnestheticDataCur.SigIsTopaz = true;
					if (CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(sigBoxTopaz) == 0)
					{
						AnestheticDataCur.Signature = "";
						return;
					}
					CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz, 0);
					CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz, 0);
					CodeBase.TopazWrapper.SetTopazKeyString(sigBoxTopaz, "0000000000000000");
					CodeBase.TopazWrapper.SetTopazAutoKeyData(sigBoxTopaz, AnestheticDataCur.AnesthClose + AnestheticDataCur.AnestheticRecordNum.ToString());
					CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz, 2);
					CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz, 2);
					AnestheticDataCur.Signature = CodeBase.TopazWrapper.GetTopazString(sigBoxTopaz);
				}
				else
				{
					AnestheticDataCur.SigIsTopaz = false;
					if (sigBox.NumberOfTabletPoints() == 0)
					{
						AnestheticDataCur.Signature = "";
						return;
					}
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString("0000000000000000");
					sigBox.SetAutoKeyData(AnestheticDataCur.Notes + AnestheticDataCur.AnestheticRecordNum.ToString());
					//sigBox.SetEncryptionMode(2);
					//sigBox.SetSigCompressionMode(2);
					AnestheticDataCur.Signature = sigBox.GetSigString();
				}
			}
		}
		private void butClose_Click(object sender, EventArgs e)
        {
			/*Regex regex = new Regex("\\d?\\d'(\\d|1[01])?$");
			if (!regex.IsMatch(textPatHgt.Text) && textPatHgt.Text != "")
			{
				MessageBox.Show("The height field should be a two digit integer");
				textPatHgt.Focus();
			}
			else
			{*/
			if (textPatID.Text != null && comboAnesthetist.SelectedIndex != 0 && comboSurgeon.SelectedIndex != 0 && comboAsst.SelectedIndex != 0 && comboCirc.SelectedIndex != 0)
			{
				int chkInhO2 = 0, chkInhN2O = 0, radCan = 0, radHood = 0, radEtt = 0, radIVCath = 0, radIVButtfly = 0, radPO = 0, radIM = 0, radRectal = 0, radNasal = 0, IVSideR = 0, IVSideL = 0, MonBP = 0, MonSpO2 = 0, MonEKG = 0, MonEtCO2 = 0, MonPrecordial = 0, MonTemp = 0, wgtUnitsLbs = 0, wgtUnitsKgs = 0, hgtUnitsIn = 0, hgtUnitsCm = 0;

				if (checkInhO2.Checked)
						chkInhO2 = 1;
					if (checkInhN2O.Checked)
						chkInhN2O = 1;
					if (radRteNasCan.Checked)
						radCan = 1;
					if (radRteNasHood.Checked)
						radHood = 1;
					if (radRteETT.Checked)
						radEtt = 1;
					if (radMedRouteIVCath.Checked)
						radIVCath = 1;
					if (radMedRouteIVButtFly.Checked)
						radIVButtfly = 1;
					if (radMedRouteIM.Checked)
						radIM = 1;
					if (radMedRouteNasal.Checked)
						radNasal = 1;
					if (radMedRoutePO.Checked)
						radPO = 1;
					if (radMedRouteRectal.Checked)
						radRectal = 1;
					if (radIVSideR.Checked)
						IVSideR = 1;
					if (radIVSideL.Checked)
						IVSideL = 1;
					if (checkMonBP.Checked)
						MonBP = 1;
					if (checkMonEKG.Checked)
						MonEKG = 1;
					if (checkMonEtCO2.Checked)
						MonEtCO2 = 1;
					if (checkMonPrecordial.Checked)
						MonPrecordial = 1;
					if (checkMonSpO2.Checked)
						MonSpO2 = 1;
					if (checkMonTemp.Checked)
						MonTemp = 1;
					if (radWgtUnitsLbs.Checked)
						wgtUnitsLbs = 1;
					if (radWgtUnitsKgs.Checked)
						wgtUnitsKgs = 1;
					if (radHgtUnitsIn.Checked)
						hgtUnitsIn = 1;
					if (radHgtUnitsCm.Checked)
						hgtUnitsCm = 1;
					if (comboASA_EModifier.SelectedItem == "E")
						{
							comboASA_EModifier.SelectedItem = "E";
						}
					else 
						{
							comboASA_EModifier.SelectedItem = "";
						}

					int value = AMedications.InsertAnesth_Data(Convert.ToInt32(textPatID.Text.Trim()), textAnesthOpen.Text.Trim(), textAnesthClose.Text.Trim(), textSurgOpen.Text.Trim(), textSurgClose.Text.Trim(), comboAnesthetist.SelectedItem.ToString(), comboSurgeon.SelectedItem.ToString(), comboAsst.SelectedItem.ToString(), comboCirc.SelectedItem.ToString(), textVSMName.Text, textVSMSerNum.Text, comboASA.SelectedItem.ToString(), comboASA_EModifier.SelectedItem.ToString(), chkInhO2, chkInhN2O, Convert.ToInt32(comboO2LMin.SelectedItem.ToString()), Convert.ToInt32(comboN2OLMin.SelectedItem.ToString()), radCan, Convert.ToInt32(radHood), radEtt, radIVCath, radIVButtfly, radIM, radPO, radNasal, radRectal, comboIVSite.SelectedItem.ToString(), Convert.ToInt32(comboIVGauge.SelectedItem.ToString()), IVSideR, IVSideL, Convert.ToInt32(comboIVAtt.SelectedItem.ToString()), comboIVF.SelectedItem.ToString(), Convert.ToInt32(textIVFVol.Text.Trim()), MonBP, MonSpO2,MonEtCO2, MonTemp, MonPrecordial, MonEKG, richTextNotes.Text, Convert.ToInt32(textPatWgt.Text), wgtUnitsLbs, wgtUnitsKgs, Convert.ToInt32(textPatHgt.Text), textEscortName.Text.Trim(), textEscortCellNum.Text.Trim(), textEscortRel.Text, comboNPOTime.SelectedItem.ToString(), hgtUnitsIn, hgtUnitsCm);

					if (value != 0)
					{
						Close();
					}
					/*else
					{
						MessageBox.Show("Enter Proper Value");
					}
				}
				else
				{
					MessageBox.Show("Please Enter The Value");
				}*/
				Userod curUser = Security.CurUser;

				if (GroupPermissions.HasPermission(curUser.UserGroupNum, Permissions.AnesthesiaControlMeds))
				{
					Close();

				}
				else
				{
					MessageBox.Show(this, "You must be an administrator to unlock this action");
					return;
				}
				if (allowTopaz)
				{
					sigBoxTopaz.Dispose();
				}
			 }
		}

		private void labelInvalidSig_Click(object sender, EventArgs e)
		{

		}



        private void radPatWgtKgs_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void radPatWgtLbs_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radHgtCm_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textPatHgt_TextChanged(object sender, EventArgs e)
        {

        }

		private void comboAnesthetist_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void textIVFVol_TextChanged(object sender, EventArgs e)
		{

		}			

	}
}