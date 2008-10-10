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

namespace OpenDental
{
	public class FormAnestheticRecord : System.Windows.Forms.Form
	{
		private AnestheticRecord AnestheticRecordCur;
		private AnestheticData AnestheticDataCur;
        private Patient PatCur;
        public bool IsNew;
        private GroupBox groupBoxVS;
		private Label labelVSM;
		private TextBox textVSM;
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
		private CheckBox checkBoxInhN20;
		private CheckBox CheckBoxInhO2;
		private Label labelIVF;
		private ComboBox comboIVF;
		private Label labelIVFVol;
		private TextBox textIVFVol;
		private ComboBox comboIVSite;
		private Label labelIVAtt;
		private ComboBox comboIVAtt;
		private Label labelIVGauge;
		private Label labelGauge;
		private ComboBox comboIVGauge;
		private RadioButton radButIVSiteL;
		private RadioButton radButIVSiteR;
		private OpenDental.UI.Button butAnesthScore;
		private GroupBox groupBoxNotes;
		private RichTextBox richTextBoxNotes;
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
		private RadioButton radButRteETT;
		private RadioButton radButRteNasCan;
		private RadioButton radButRteNasHood;
		private GroupBox groupBoxIVSite;
		private GroupBox groupBoxMedRoute;
		private RadioButton radButMedRouteIVButFly;
		private RadioButton radButMedRouteIVCath;
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
		private RadioButton radioButMedRouteIM;
		private RadioButton radioButMedRoutePO;
		private RadioButton radioButMedRouteRectal;
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
        private OpenDental.UI.Button butDose75;
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
        private RadioButton radioButton2;
        private ComboBox comboNPOTime;
        private RadioButton radioButton1;
        private TextBox textPatHgt;
        private Label labelPatWgt;
        private Label labelPatHgt;
        private TextBox textPatWgt;
        private ODGrid gridVitalSigns;
		private RadioButton radioButMedRouteNasal;
		
		


		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnestheticRecord));
            this.labelVSM = new System.Windows.Forms.Label();
            this.labelVSMSerNum = new System.Windows.Forms.Label();
            this.richTextBoxNotes = new System.Windows.Forms.RichTextBox();
            this.textEscortName = new System.Windows.Forms.TextBox();
            this.labelEscortName = new System.Windows.Forms.Label();
            this.textEscortRel = new System.Windows.Forms.TextBox();
            this.labelEscortRel = new System.Windows.Forms.Label();
            this.groupBoxSidebarRt = new System.Windows.Forms.GroupBox();
            this.labelEMod = new System.Windows.Forms.Label();
            this.comboASA_EModifier = new System.Windows.Forms.ComboBox();
            this.groupBoxIVSite = new System.Windows.Forms.GroupBox();
            this.comboIVSite = new System.Windows.Forms.ComboBox();
            this.radButIVSiteR = new System.Windows.Forms.RadioButton();
            this.radButIVSiteL = new System.Windows.Forms.RadioButton();
            this.comboIVAtt = new System.Windows.Forms.ComboBox();
            this.labelIVAtt = new System.Windows.Forms.Label();
            this.groupBoxMedRoute = new System.Windows.Forms.GroupBox();
            this.radioButMedRouteRectal = new System.Windows.Forms.RadioButton();
            this.radioButMedRouteNasal = new System.Windows.Forms.RadioButton();
            this.radioButMedRouteIM = new System.Windows.Forms.RadioButton();
            this.radioButMedRoutePO = new System.Windows.Forms.RadioButton();
            this.radButMedRouteIVButFly = new System.Windows.Forms.RadioButton();
            this.radButMedRouteIVCath = new System.Windows.Forms.RadioButton();
            this.groupBoxDeliveryMethod = new System.Windows.Forms.GroupBox();
            this.radButRteETT = new System.Windows.Forms.RadioButton();
            this.radButRteNasCan = new System.Windows.Forms.RadioButton();
            this.radButRteNasHood = new System.Windows.Forms.RadioButton();
            this.labelLperMinN2O = new System.Windows.Forms.Label();
            this.labelLperMinO2 = new System.Windows.Forms.Label();
            this.labelGauge = new System.Windows.Forms.Label();
            this.labelIVGauge = new System.Windows.Forms.Label();
            this.comboIVGauge = new System.Windows.Forms.ComboBox();
            this.butAnesthScore = new OpenDental.UI.Button();
            this.comboO2LMin = new System.Windows.Forms.ComboBox();
            this.labelIVFVol = new System.Windows.Forms.Label();
            this.textIVFVol = new System.Windows.Forms.TextBox();
            this.labelIVF = new System.Windows.Forms.Label();
            this.comboIVF = new System.Windows.Forms.ComboBox();
            this.labelInh = new System.Windows.Forms.Label();
            this.comboN2OLMin = new System.Windows.Forms.ComboBox();
            this.checkBoxInhN20 = new System.Windows.Forms.CheckBox();
            this.CheckBoxInhO2 = new System.Windows.Forms.CheckBox();
            this.comboASA = new System.Windows.Forms.ComboBox();
            this.labelASA = new System.Windows.Forms.Label();
            this.groupBoxNotes = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.comboNPOTime = new System.Windows.Forms.ComboBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.textPatHgt = new System.Windows.Forms.TextBox();
            this.labelPatWgt = new System.Windows.Forms.Label();
            this.labelPatHgt = new System.Windows.Forms.Label();
            this.textPatWgt = new System.Windows.Forms.TextBox();
            this.butOK = new OpenDental.UI.Button();
            this.butClose = new OpenDental.UI.Button();
            this.butCancel = new OpenDental.UI.Button();
            this.butPrint = new OpenDental.UI.Button();
            this.groupBoxSig = new System.Windows.Forms.GroupBox();
            this.sigBox = new OpenDental.UI.SignatureBox();
            this.butSignTopaz = new OpenDental.UI.Button();
            this.butClearSig = new OpenDental.UI.Button();
            this.groupBoxHgtWgt = new System.Windows.Forms.GroupBox();
            this.labelEscortCellNum = new System.Windows.Forms.Label();
            this.textEscortCellNum = new System.Windows.Forms.TextBox();
            this.groupBoxVS = new System.Windows.Forms.GroupBox();
            this.gridVitalSigns = new OpenDental.UI.ODGrid();
            this.textVSMSerNum = new System.Windows.Forms.TextBox();
            this.textVSM = new System.Windows.Forms.TextBox();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.groupBoxTimes = new System.Windows.Forms.GroupBox();
            this.textAnesthOpen = new System.Windows.Forms.TextBox();
            this.butSurgClose = new OpenDental.UI.Button();
            this.textSurgClose = new System.Windows.Forms.TextBox();
            this.textAnesthClose = new System.Windows.Forms.TextBox();
            this.butAnesthOpen = new OpenDental.UI.Button();
            this.butAnesthClose = new OpenDental.UI.Button();
            this.butSurgOpen = new OpenDental.UI.Button();
            this.textSurgOpen = new System.Windows.Forms.TextBox();
            this.labelIVAnesthetics = new System.Windows.Forms.Label();
            this.labelAsst = new System.Windows.Forms.Label();
            this.comboCirc = new System.Windows.Forms.ComboBox();
            this.labelCirc = new System.Windows.Forms.Label();
            this.comboAsst = new System.Windows.Forms.ComboBox();
            this.listAnesthetics = new System.Windows.Forms.ListBox();
            this.labelAnesthMed = new System.Windows.Forms.Label();
            this.groupBoxDoseCalc = new System.Windows.Forms.GroupBox();
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
            this.butDose75 = new OpenDental.UI.Button();
            this.butDoseEnter = new OpenDental.UI.Button();
            this.butDoseDecPoint = new OpenDental.UI.Button();
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
            this.gridAnesthMeds = new OpenDental.UI.ODGrid();
            this.butWasteQty = new OpenDental.UI.Button();
            this.butAddAnesthetic = new OpenDental.UI.Button();
            this.butDelAnesthetic = new OpenDental.UI.Button();
            this.butDelAnesthMeds = new OpenDental.UI.Button();
            this.groupBoxSidebarRt.SuspendLayout();
            this.groupBoxIVSite.SuspendLayout();
            this.groupBoxMedRoute.SuspendLayout();
            this.groupBoxDeliveryMethod.SuspendLayout();
            this.groupBoxNotes.SuspendLayout();
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
            // richTextBoxNotes
            // 
            this.richTextBoxNotes.Location = new System.Drawing.Point(10, 16);
            this.richTextBoxNotes.Name = "richTextBoxNotes";
            this.richTextBoxNotes.Size = new System.Drawing.Size(180, 80);
            this.richTextBoxNotes.TabIndex = 103;
            this.richTextBoxNotes.Text = "";
            // 
            // textEscortName
            // 
            this.textEscortName.Location = new System.Drawing.Point(88, 17);
            this.textEscortName.MaxLength = 32;
            this.textEscortName.Name = "textEscortName";
            this.textEscortName.Size = new System.Drawing.Size(170, 20);
            this.textEscortName.TabIndex = 122;
            this.textEscortName.TextChanged += new System.EventHandler(this.textEscortName_TextChanged);
            // 
            // labelEscortName
            // 
            this.labelEscortName.AutoSize = true;
            this.labelEscortName.Location = new System.Drawing.Point(13, 21);
            this.labelEscortName.Name = "labelEscortName";
            this.labelEscortName.Size = new System.Drawing.Size(66, 13);
            this.labelEscortName.TabIndex = 126;
            this.labelEscortName.Text = "Escort name";
            // 
            // textEscortRel
            // 
            this.textEscortRel.Location = new System.Drawing.Point(88, 63);
            this.textEscortRel.MaxLength = 16;
            this.textEscortRel.Name = "textEscortRel";
            this.textEscortRel.Size = new System.Drawing.Size(170, 20);
            this.textEscortRel.TabIndex = 127;
            // 
            // labelEscortRel
            // 
            this.labelEscortRel.AutoSize = true;
            this.labelEscortRel.Location = new System.Drawing.Point(15, 66);
            this.labelEscortRel.Name = "labelEscortRel";
            this.labelEscortRel.Size = new System.Drawing.Size(65, 13);
            this.labelEscortRel.TabIndex = 128;
            this.labelEscortRel.Text = "Relationship";
            // 
            // groupBoxSidebarRt
            // 
            this.groupBoxSidebarRt.Controls.Add(this.labelEMod);
            this.groupBoxSidebarRt.Controls.Add(this.comboASA_EModifier);
            this.groupBoxSidebarRt.Controls.Add(this.groupBoxIVSite);
            this.groupBoxSidebarRt.Controls.Add(this.groupBoxMedRoute);
            this.groupBoxSidebarRt.Controls.Add(this.groupBoxDeliveryMethod);
            this.groupBoxSidebarRt.Controls.Add(this.labelLperMinN2O);
            this.groupBoxSidebarRt.Controls.Add(this.labelLperMinO2);
            this.groupBoxSidebarRt.Controls.Add(this.labelGauge);
            this.groupBoxSidebarRt.Controls.Add(this.labelIVGauge);
            this.groupBoxSidebarRt.Controls.Add(this.comboIVGauge);
            this.groupBoxSidebarRt.Controls.Add(this.butAnesthScore);
            this.groupBoxSidebarRt.Controls.Add(this.comboO2LMin);
            this.groupBoxSidebarRt.Controls.Add(this.labelIVFVol);
            this.groupBoxSidebarRt.Controls.Add(this.textIVFVol);
            this.groupBoxSidebarRt.Controls.Add(this.labelIVF);
            this.groupBoxSidebarRt.Controls.Add(this.comboIVF);
            this.groupBoxSidebarRt.Controls.Add(this.labelInh);
            this.groupBoxSidebarRt.Controls.Add(this.comboN2OLMin);
            this.groupBoxSidebarRt.Controls.Add(this.checkBoxInhN20);
            this.groupBoxSidebarRt.Controls.Add(this.CheckBoxInhO2);
            this.groupBoxSidebarRt.Controls.Add(this.comboASA);
            this.groupBoxSidebarRt.Controls.Add(this.labelASA);
            this.groupBoxSidebarRt.Location = new System.Drawing.Point(612, -10);
            this.groupBoxSidebarRt.Name = "groupBoxSidebarRt";
            this.groupBoxSidebarRt.Size = new System.Drawing.Size(190, 555);
            this.groupBoxSidebarRt.TabIndex = 136;
            this.groupBoxSidebarRt.TabStop = false;
            this.groupBoxSidebarRt.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // labelEMod
            // 
            this.labelEMod.AutoSize = true;
            this.labelEMod.Location = new System.Drawing.Point(133, 36);
            this.labelEMod.Name = "labelEMod";
            this.labelEMod.Size = new System.Drawing.Size(54, 13);
            this.labelEMod.TabIndex = 164;
            this.labelEMod.Text = "E Modifier";
            // 
            // comboASA_EModifier
            // 
            this.comboASA_EModifier.AutoCompleteCustomSource.AddRange(new string[] {
            "I"});
            this.comboASA_EModifier.FormattingEnabled = true;
            this.comboASA_EModifier.Items.AddRange(new object[] {
            "",
            "E"});
            this.comboASA_EModifier.Location = new System.Drawing.Point(80, 33);
            this.comboASA_EModifier.Name = "comboASA_EModifier";
            this.comboASA_EModifier.Size = new System.Drawing.Size(50, 21);
            this.comboASA_EModifier.TabIndex = 163;
            // 
            // groupBoxIVSite
            // 
            this.groupBoxIVSite.Controls.Add(this.comboIVSite);
            this.groupBoxIVSite.Controls.Add(this.radButIVSiteR);
            this.groupBoxIVSite.Controls.Add(this.radButIVSiteL);
            this.groupBoxIVSite.Controls.Add(this.comboIVAtt);
            this.groupBoxIVSite.Controls.Add(this.labelIVAtt);
            this.groupBoxIVSite.Location = new System.Drawing.Point(29, 332);
            this.groupBoxIVSite.Name = "groupBoxIVSite";
            this.groupBoxIVSite.Size = new System.Drawing.Size(153, 73);
            this.groupBoxIVSite.TabIndex = 133;
            this.groupBoxIVSite.TabStop = false;
            this.groupBoxIVSite.Text = "IV Site";
            // 
            // comboIVSite
            // 
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
            this.comboIVSite.TabIndex = 142;
            this.comboIVSite.SelectedIndexChanged += new System.EventHandler(this.comboBox8_SelectedIndexChanged);
            // 
            // radButIVSiteR
            // 
            this.radButIVSiteR.AutoSize = true;
            this.radButIVSiteR.Location = new System.Drawing.Point(10, 90);
            this.radButIVSiteR.Name = "radButIVSiteR";
            this.radButIVSiteR.Size = new System.Drawing.Size(50, 17);
            this.radButIVSiteR.TabIndex = 133;
            this.radButIVSiteR.TabStop = true;
            this.radButIVSiteR.Text = "Right";
            this.radButIVSiteR.UseVisualStyleBackColor = true;
            // 
            // radButIVSiteL
            // 
            this.radButIVSiteL.AutoSize = true;
            this.radButIVSiteL.Location = new System.Drawing.Point(80, 90);
            this.radButIVSiteL.Name = "radButIVSiteL";
            this.radButIVSiteL.Size = new System.Drawing.Size(43, 17);
            this.radButIVSiteL.TabIndex = 154;
            this.radButIVSiteL.TabStop = true;
            this.radButIVSiteL.Text = "Left";
            this.radButIVSiteL.UseVisualStyleBackColor = true;
            // 
            // comboIVAtt
            // 
            this.comboIVAtt.FormattingEnabled = true;
            this.comboIVAtt.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboIVAtt.Location = new System.Drawing.Point(11, 48);
            this.comboIVAtt.Name = "comboIVAtt";
            this.comboIVAtt.Size = new System.Drawing.Size(30, 21);
            this.comboIVAtt.TabIndex = 144;
            // 
            // labelIVAtt
            // 
            this.labelIVAtt.AutoSize = true;
            this.labelIVAtt.Location = new System.Drawing.Point(47, 51);
            this.labelIVAtt.Name = "labelIVAtt";
            this.labelIVAtt.Size = new System.Drawing.Size(61, 13);
            this.labelIVAtt.TabIndex = 145;
            this.labelIVAtt.Text = "IV Attempts";
            // 
            // groupBoxMedRoute
            // 
            this.groupBoxMedRoute.Controls.Add(this.radioButMedRouteRectal);
            this.groupBoxMedRoute.Controls.Add(this.radioButMedRouteNasal);
            this.groupBoxMedRoute.Controls.Add(this.radioButMedRouteIM);
            this.groupBoxMedRoute.Controls.Add(this.radioButMedRoutePO);
            this.groupBoxMedRoute.Controls.Add(this.radButMedRouteIVButFly);
            this.groupBoxMedRoute.Controls.Add(this.radButMedRouteIVCath);
            this.groupBoxMedRoute.Location = new System.Drawing.Point(12, 203);
            this.groupBoxMedRoute.Name = "groupBoxMedRoute";
            this.groupBoxMedRoute.Size = new System.Drawing.Size(165, 78);
            this.groupBoxMedRoute.TabIndex = 162;
            this.groupBoxMedRoute.TabStop = false;
            this.groupBoxMedRoute.Text = "Administration Route";
            // 
            // radioButMedRouteRectal
            // 
            this.radioButMedRouteRectal.AutoSize = true;
            this.radioButMedRouteRectal.Location = new System.Drawing.Point(84, 50);
            this.radioButMedRouteRectal.Name = "radioButMedRouteRectal";
            this.radioButMedRouteRectal.Size = new System.Drawing.Size(56, 17);
            this.radioButMedRouteRectal.TabIndex = 162;
            this.radioButMedRouteRectal.TabStop = true;
            this.radioButMedRouteRectal.Text = "Rectal";
            this.radioButMedRouteRectal.UseVisualStyleBackColor = true;
            // 
            // radioButMedRouteNasal
            // 
            this.radioButMedRouteNasal.AutoSize = true;
            this.radioButMedRouteNasal.Location = new System.Drawing.Point(6, 50);
            this.radioButMedRouteNasal.Name = "radioButMedRouteNasal";
            this.radioButMedRouteNasal.Size = new System.Drawing.Size(52, 17);
            this.radioButMedRouteNasal.TabIndex = 161;
            this.radioButMedRouteNasal.TabStop = true;
            this.radioButMedRouteNasal.Text = "Nasal";
            this.radioButMedRouteNasal.UseVisualStyleBackColor = true;
            // 
            // radioButMedRouteIM
            // 
            this.radioButMedRouteIM.AutoSize = true;
            this.radioButMedRouteIM.Location = new System.Drawing.Point(84, 34);
            this.radioButMedRouteIM.Name = "radioButMedRouteIM";
            this.radioButMedRouteIM.Size = new System.Drawing.Size(37, 17);
            this.radioButMedRouteIM.TabIndex = 160;
            this.radioButMedRouteIM.TabStop = true;
            this.radioButMedRouteIM.Text = "IM";
            this.radioButMedRouteIM.UseVisualStyleBackColor = true;
            // 
            // radioButMedRoutePO
            // 
            this.radioButMedRoutePO.AutoSize = true;
            this.radioButMedRoutePO.Location = new System.Drawing.Point(6, 34);
            this.radioButMedRoutePO.Name = "radioButMedRoutePO";
            this.radioButMedRoutePO.Size = new System.Drawing.Size(40, 17);
            this.radioButMedRoutePO.TabIndex = 159;
            this.radioButMedRoutePO.TabStop = true;
            this.radioButMedRoutePO.Text = "PO";
            this.radioButMedRoutePO.UseVisualStyleBackColor = true;
            // 
            // radButMedRouteIVButFly
            // 
            this.radButMedRouteIVButFly.AutoSize = true;
            this.radButMedRouteIVButFly.Location = new System.Drawing.Point(84, 17);
            this.radButMedRouteIVButFly.Name = "radButMedRouteIVButFly";
            this.radButMedRouteIVButFly.Size = new System.Drawing.Size(76, 17);
            this.radButMedRouteIVButFly.TabIndex = 158;
            this.radButMedRouteIVButFly.TabStop = true;
            this.radButMedRouteIVButFly.Text = "IV Butterfly";
            this.radButMedRouteIVButFly.UseVisualStyleBackColor = true;
            // 
            // radButMedRouteIVCath
            // 
            this.radButMedRouteIVCath.AutoSize = true;
            this.radButMedRouteIVCath.Location = new System.Drawing.Point(6, 17);
            this.radButMedRouteIVCath.Name = "radButMedRouteIVCath";
            this.radButMedRouteIVCath.Size = new System.Drawing.Size(78, 17);
            this.radButMedRouteIVCath.TabIndex = 157;
            this.radButMedRouteIVCath.TabStop = true;
            this.radButMedRouteIVCath.Text = "IV Catheter";
            this.radButMedRouteIVCath.UseVisualStyleBackColor = true;
            // 
            // groupBoxDeliveryMethod
            // 
            this.groupBoxDeliveryMethod.Controls.Add(this.radButRteETT);
            this.groupBoxDeliveryMethod.Controls.Add(this.radButRteNasCan);
            this.groupBoxDeliveryMethod.Controls.Add(this.radButRteNasHood);
            this.groupBoxDeliveryMethod.Location = new System.Drawing.Point(35, 124);
            this.groupBoxDeliveryMethod.Name = "groupBoxDeliveryMethod";
            this.groupBoxDeliveryMethod.Size = new System.Drawing.Size(124, 72);
            this.groupBoxDeliveryMethod.TabIndex = 161;
            this.groupBoxDeliveryMethod.TabStop = false;
            this.groupBoxDeliveryMethod.Text = "Delivery method";
            // 
            // radButRteETT
            // 
            this.radButRteETT.AutoSize = true;
            this.radButRteETT.Location = new System.Drawing.Point(8, 47);
            this.radButRteETT.Name = "radButRteETT";
            this.radButRteETT.Size = new System.Drawing.Size(112, 17);
            this.radButRteETT.TabIndex = 162;
            this.radButRteETT.TabStop = true;
            this.radButRteETT.Text = "Endotracheal tube";
            this.radButRteETT.UseVisualStyleBackColor = true;
            // 
            // radButRteNasCan
            // 
            this.radButRteNasCan.AutoSize = true;
            this.radButRteNasCan.Location = new System.Drawing.Point(8, 15);
            this.radButRteNasCan.Name = "radButRteNasCan";
            this.radButRteNasCan.Size = new System.Drawing.Size(93, 17);
            this.radButRteNasCan.TabIndex = 161;
            this.radButRteNasCan.TabStop = true;
            this.radButRteNasCan.Text = "Nasal cannula";
            this.radButRteNasCan.UseVisualStyleBackColor = true;
            // 
            // radButRteNasHood
            // 
            this.radButRteNasHood.AutoSize = true;
            this.radButRteNasHood.Location = new System.Drawing.Point(8, 31);
            this.radButRteNasHood.Name = "radButRteNasHood";
            this.radButRteNasHood.Size = new System.Drawing.Size(79, 17);
            this.radButRteNasHood.TabIndex = 160;
            this.radButRteNasHood.TabStop = true;
            this.radButRteNasHood.Text = "Nasal hood";
            this.radButRteNasHood.UseVisualStyleBackColor = true;
            // 
            // labelLperMinN2O
            // 
            this.labelLperMinN2O.AutoSize = true;
            this.labelLperMinN2O.Location = new System.Drawing.Point(122, 100);
            this.labelLperMinN2O.Name = "labelLperMinN2O";
            this.labelLperMinN2O.Size = new System.Drawing.Size(34, 13);
            this.labelLperMinN2O.TabIndex = 160;
            this.labelLperMinN2O.Text = "L/min";
            // 
            // labelLperMinO2
            // 
            this.labelLperMinO2.AutoSize = true;
            this.labelLperMinO2.Location = new System.Drawing.Point(121, 78);
            this.labelLperMinO2.Name = "labelLperMinO2";
            this.labelLperMinO2.Size = new System.Drawing.Size(34, 13);
            this.labelLperMinO2.TabIndex = 107;
            this.labelLperMinO2.Text = "L/min";
            // 
            // labelGauge
            // 
            this.labelGauge.AutoSize = true;
            this.labelGauge.Location = new System.Drawing.Point(106, 304);
            this.labelGauge.Name = "labelGauge";
            this.labelGauge.Size = new System.Drawing.Size(22, 13);
            this.labelGauge.TabIndex = 107;
            this.labelGauge.Text = "ga.";
            // 
            // labelIVGauge
            // 
            this.labelIVGauge.AutoSize = true;
            this.labelIVGauge.Location = new System.Drawing.Point(35, 284);
            this.labelIVGauge.Name = "labelIVGauge";
            this.labelIVGauge.Size = new System.Drawing.Size(39, 13);
            this.labelIVGauge.TabIndex = 153;
            this.labelIVGauge.Text = "Gauge";
            // 
            // comboIVGauge
            // 
            this.comboIVGauge.FormattingEnabled = true;
            this.comboIVGauge.Items.AddRange(new object[] {
            "18",
            "20",
            "21",
            "22"});
            this.comboIVGauge.Location = new System.Drawing.Point(37, 301);
            this.comboIVGauge.Name = "comboIVGauge";
            this.comboIVGauge.Size = new System.Drawing.Size(65, 21);
            this.comboIVGauge.TabIndex = 152;
            // 
            // butAnesthScore
            // 
            this.butAnesthScore.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butAnesthScore.Autosize = true;
            this.butAnesthScore.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butAnesthScore.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butAnesthScore.CornerRadius = 4F;
            this.butAnesthScore.Location = new System.Drawing.Point(29, 508);
            this.butAnesthScore.Name = "butAnesthScore";
            this.butAnesthScore.Size = new System.Drawing.Size(131, 26);
            this.butAnesthScore.TabIndex = 129;
            this.butAnesthScore.Text = "Post-anesthesia score";
            this.butAnesthScore.UseVisualStyleBackColor = true;
            this.butAnesthScore.Click += new System.EventHandler(this.butAnesthScore_Click);
            // 
            // comboO2LMin
            // 
            this.comboO2LMin.FormattingEnabled = true;
            this.comboO2LMin.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboO2LMin.Location = new System.Drawing.Point(76, 75);
            this.comboO2LMin.Name = "comboO2LMin";
            this.comboO2LMin.Size = new System.Drawing.Size(40, 21);
            this.comboO2LMin.TabIndex = 150;
            this.comboO2LMin.SelectedIndexChanged += new System.EventHandler(this.comboBoxO2LMin_SelectedIndexChanged);
            // 
            // labelIVFVol
            // 
            this.labelIVFVol.AutoSize = true;
            this.labelIVFVol.Location = new System.Drawing.Point(97, 462);
            this.labelIVFVol.Name = "labelIVFVol";
            this.labelIVFVol.Size = new System.Drawing.Size(57, 13);
            this.labelIVFVol.TabIndex = 149;
            this.labelIVFVol.Text = "mL\'s given";
            // 
            // textIVFVol
            // 
            this.textIVFVol.Location = new System.Drawing.Point(40, 459);
            this.textIVFVol.MaxLength = 5;
            this.textIVFVol.Name = "textIVFVol";
            this.textIVFVol.Size = new System.Drawing.Size(51, 20);
            this.textIVFVol.TabIndex = 148;
            // 
            // labelIVF
            // 
            this.labelIVF.AutoSize = true;
            this.labelIVF.Location = new System.Drawing.Point(37, 412);
            this.labelIVF.Name = "labelIVF";
            this.labelIVF.Size = new System.Drawing.Size(42, 13);
            this.labelIVF.TabIndex = 147;
            this.labelIVF.Text = "IV Fluid";
            // 
            // comboIVF
            // 
            this.comboIVF.AutoCompleteCustomSource.AddRange(new string[] {
            "D5(1/2)NS"});
            this.comboIVF.FormattingEnabled = true;
            this.comboIVF.Items.AddRange(new object[] {
            "D5(1/2)NS",
            "D5NS",
            "D5LR",
            "D5W",
            "LR",
            "NS"});
            this.comboIVF.Location = new System.Drawing.Point(39, 431);
            this.comboIVF.Name = "comboIVF";
            this.comboIVF.Size = new System.Drawing.Size(119, 21);
            this.comboIVF.TabIndex = 146;
            // 
            // labelInh
            // 
            this.labelInh.AutoSize = true;
            this.labelInh.Location = new System.Drawing.Point(25, 59);
            this.labelInh.Name = "labelInh";
            this.labelInh.Size = new System.Drawing.Size(96, 13);
            this.labelInh.TabIndex = 132;
            this.labelInh.Text = "Inhalational agents";
            // 
            // comboN2OLMin
            // 
            this.comboN2OLMin.FormattingEnabled = true;
            this.comboN2OLMin.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboN2OLMin.Location = new System.Drawing.Point(76, 97);
            this.comboN2OLMin.Name = "comboN2OLMin";
            this.comboN2OLMin.Size = new System.Drawing.Size(40, 21);
            this.comboN2OLMin.TabIndex = 130;
            // 
            // checkBoxInhN20
            // 
            this.checkBoxInhN20.AutoSize = true;
            this.checkBoxInhN20.Location = new System.Drawing.Point(29, 101);
            this.checkBoxInhN20.Name = "checkBoxInhN20";
            this.checkBoxInhN20.Size = new System.Drawing.Size(46, 17);
            this.checkBoxInhN20.TabIndex = 128;
            this.checkBoxInhN20.Text = "N20";
            this.checkBoxInhN20.UseVisualStyleBackColor = true;
            // 
            // CheckBoxInhO2
            // 
            this.CheckBoxInhO2.AutoSize = true;
            this.CheckBoxInhO2.Checked = true;
            this.CheckBoxInhO2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxInhO2.Location = new System.Drawing.Point(29, 77);
            this.CheckBoxInhO2.Name = "CheckBoxInhO2";
            this.CheckBoxInhO2.Size = new System.Drawing.Size(40, 17);
            this.CheckBoxInhO2.TabIndex = 127;
            this.CheckBoxInhO2.Text = "O2";
            this.CheckBoxInhO2.UseVisualStyleBackColor = true;
            // 
            // comboASA
            // 
            this.comboASA.AutoCompleteCustomSource.AddRange(new string[] {
            "I"});
            this.comboASA.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboASA.FormattingEnabled = true;
            this.comboASA.Items.AddRange(new object[] {
            "I",
            "II",
            "III",
            "IV",
            "V"});
            this.comboASA.Location = new System.Drawing.Point(17, 32);
            this.comboASA.Name = "comboASA";
            this.comboASA.Size = new System.Drawing.Size(50, 21);
            this.comboASA.TabIndex = 125;
            // 
            // labelASA
            // 
            this.labelASA.AutoSize = true;
            this.labelASA.Location = new System.Drawing.Point(15, 16);
            this.labelASA.Name = "labelASA";
            this.labelASA.Size = new System.Drawing.Size(95, 13);
            this.labelASA.TabIndex = 126;
            this.labelASA.Text = "ASA  Classification";
            // 
            // groupBoxNotes
            // 
            this.groupBoxNotes.Controls.Add(this.label1);
            this.groupBoxNotes.Controls.Add(this.radioButton2);
            this.groupBoxNotes.Controls.Add(this.comboNPOTime);
            this.groupBoxNotes.Controls.Add(this.radioButton1);
            this.groupBoxNotes.Controls.Add(this.textPatHgt);
            this.groupBoxNotes.Controls.Add(this.labelPatWgt);
            this.groupBoxNotes.Controls.Add(this.labelPatHgt);
            this.groupBoxNotes.Controls.Add(this.textPatWgt);
            this.groupBoxNotes.Controls.Add(this.butOK);
            this.groupBoxNotes.Controls.Add(this.butClose);
            this.groupBoxNotes.Controls.Add(this.butCancel);
            this.groupBoxNotes.Controls.Add(this.richTextBoxNotes);
            this.groupBoxNotes.Controls.Add(this.butPrint);
            this.groupBoxNotes.Controls.Add(this.groupBoxSig);
            this.groupBoxNotes.Controls.Add(this.groupBoxHgtWgt);
            this.groupBoxNotes.Location = new System.Drawing.Point(14, 565);
            this.groupBoxNotes.Name = "groupBoxNotes";
            this.groupBoxNotes.Size = new System.Drawing.Size(769, 160);
            this.groupBoxNotes.TabIndex = 138;
            this.groupBoxNotes.TabStop = false;
            this.groupBoxNotes.Text = "Notes (record additional meds/routes/times here)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(421, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 151;
            this.label1.Text = "NPO Since";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(363, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(37, 17);
            this.radioButton2.TabIndex = 145;
            this.radioButton2.Text = "kg";
            this.radioButton2.UseVisualStyleBackColor = true;
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
            this.comboNPOTime.Location = new System.Drawing.Point(424, 33);
            this.comboNPOTime.Name = "comboNPOTime";
            this.comboNPOTime.Size = new System.Drawing.Size(54, 21);
            this.comboNPOTime.TabIndex = 146;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(322, 42);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(41, 17);
            this.radioButton1.TabIndex = 144;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "lbs.";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // textPatHgt
            // 
            this.textPatHgt.Location = new System.Drawing.Point(257, 16);
            this.textPatHgt.MaxLength = 10;
            this.textPatHgt.Name = "textPatHgt";
            this.textPatHgt.Size = new System.Drawing.Size(60, 20);
            this.textPatHgt.TabIndex = 147;
            // 
            // labelPatWgt
            // 
            this.labelPatWgt.AutoSize = true;
            this.labelPatWgt.Location = new System.Drawing.Point(210, 42);
            this.labelPatWgt.Name = "labelPatWgt";
            this.labelPatWgt.Size = new System.Drawing.Size(41, 13);
            this.labelPatWgt.TabIndex = 150;
            this.labelPatWgt.Text = "Weight";
            // 
            // labelPatHgt
            // 
            this.labelPatHgt.AutoSize = true;
            this.labelPatHgt.Location = new System.Drawing.Point(213, 19);
            this.labelPatHgt.Name = "labelPatHgt";
            this.labelPatHgt.Size = new System.Drawing.Size(38, 13);
            this.labelPatHgt.TabIndex = 148;
            this.labelPatHgt.Text = "Height";
            // 
            // textPatWgt
            // 
            this.textPatWgt.Location = new System.Drawing.Point(257, 39);
            this.textPatWgt.MaxLength = 3;
            this.textPatWgt.Name = "textPatWgt";
            this.textPatWgt.Size = new System.Drawing.Size(60, 20);
            this.textPatWgt.TabIndex = 149;
            // 
            // butOK
            // 
            this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butOK.Autosize = true;
            this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butOK.CornerRadius = 4F;
            this.butOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butOK.Location = new System.Drawing.Point(588, 116);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 26);
            this.butOK.TabIndex = 143;
            this.butOK.Text = "&OK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butClose
            // 
            this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butClose.Autosize = true;
            this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butClose.CornerRadius = 4F;
            this.butClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butClose.Location = new System.Drawing.Point(669, 116);
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
            this.butCancel.Location = new System.Drawing.Point(498, 116);
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
            this.butPrint.Location = new System.Drawing.Point(59, 113);
            this.butPrint.Name = "butPrint";
            this.butPrint.Size = new System.Drawing.Size(88, 26);
            this.butPrint.TabIndex = 102;
            this.butPrint.Text = "Print";
            this.butPrint.UseVisualStyleBackColor = true;
            this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
            // 
            // groupBoxSig
            // 
            this.groupBoxSig.Controls.Add(this.sigBox);
            this.groupBoxSig.Controls.Add(this.butSignTopaz);
            this.groupBoxSig.Controls.Add(this.butClearSig);
            this.groupBoxSig.Location = new System.Drawing.Point(490, 0);
            this.groupBoxSig.Name = "groupBoxSig";
            this.groupBoxSig.Size = new System.Drawing.Size(281, 110);
            this.groupBoxSig.TabIndex = 139;
            this.groupBoxSig.TabStop = false;
            this.groupBoxSig.Text = "Signature/Initials";
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
            // groupBoxHgtWgt
            // 
            this.groupBoxHgtWgt.Controls.Add(this.labelEscortCellNum);
            this.groupBoxHgtWgt.Controls.Add(this.textEscortCellNum);
            this.groupBoxHgtWgt.Controls.Add(this.labelEscortRel);
            this.groupBoxHgtWgt.Controls.Add(this.labelEscortName);
            this.groupBoxHgtWgt.Controls.Add(this.textEscortName);
            this.groupBoxHgtWgt.Controls.Add(this.textEscortRel);
            this.groupBoxHgtWgt.Location = new System.Drawing.Point(202, 58);
            this.groupBoxHgtWgt.Name = "groupBoxHgtWgt";
            this.groupBoxHgtWgt.Size = new System.Drawing.Size(276, 91);
            this.groupBoxHgtWgt.TabIndex = 138;
            this.groupBoxHgtWgt.TabStop = false;
            this.groupBoxHgtWgt.Enter += new System.EventHandler(this.groupBoxHgtWgt_Enter);
            // 
            // labelEscortCellNum
            // 
            this.labelEscortCellNum.AutoSize = true;
            this.labelEscortCellNum.Location = new System.Drawing.Point(13, 43);
            this.labelEscortCellNum.Name = "labelEscortCellNum";
            this.labelEscortCellNum.Size = new System.Drawing.Size(67, 13);
            this.labelEscortCellNum.TabIndex = 135;
            this.labelEscortCellNum.Text = "Escort Cell #";
            // 
            // textEscortCellNum
            // 
            this.textEscortCellNum.Location = new System.Drawing.Point(88, 40);
            this.textEscortCellNum.MaxLength = 13;
            this.textEscortCellNum.Name = "textEscortCellNum";
            this.textEscortCellNum.Size = new System.Drawing.Size(170, 20);
            this.textEscortCellNum.TabIndex = 134;
            this.textEscortCellNum.TextChanged += new System.EventHandler(this.textEscortCellNum_TextChanged);
            // 
            // groupBoxVS
            // 
            this.groupBoxVS.Controls.Add(this.gridVitalSigns);
            this.groupBoxVS.Controls.Add(this.textVSMSerNum);
            this.groupBoxVS.Controls.Add(this.textVSM);
            this.groupBoxVS.Controls.Add(this.labelVSM);
            this.groupBoxVS.Controls.Add(this.labelVSMSerNum);
            this.groupBoxVS.Location = new System.Drawing.Point(14, 352);
            this.groupBoxVS.Name = "groupBoxVS";
            this.groupBoxVS.Size = new System.Drawing.Size(592, 207);
            this.groupBoxVS.TabIndex = 139;
            this.groupBoxVS.TabStop = false;
            this.groupBoxVS.Text = "Vital Signs";
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
            this.gridVitalSigns.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridVitalSigns_CellDoubleClick);
            // 
            // textVSMSerNum
            // 
            this.textVSMSerNum.Location = new System.Drawing.Point(347, 19);
            this.textVSMSerNum.MaxLength = 20;
            this.textVSMSerNum.Name = "textVSMSerNum";
            this.textVSMSerNum.Size = new System.Drawing.Size(88, 20);
            this.textVSMSerNum.TabIndex = 132;
            // 
            // textVSM
            // 
            this.textVSM.Location = new System.Drawing.Point(170, 19);
            this.textVSM.MaxLength = 20;
            this.textVSM.Name = "textVSM";
            this.textVSM.Size = new System.Drawing.Size(88, 20);
            this.textVSM.TabIndex = 130;
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
            this.groupBoxTimes.TabIndex = 96;
            this.groupBoxTimes.TabStop = false;
            this.groupBoxTimes.Text = "Times";
            // 
            // textAnesthOpen
            // 
            this.textAnesthOpen.Location = new System.Drawing.Point(16, 49);
            this.textAnesthOpen.Name = "textAnesthOpen";
            this.textAnesthOpen.Size = new System.Drawing.Size(100, 20);
            this.textAnesthOpen.TabIndex = 97;
            this.textAnesthOpen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textAnesthOpen.TextChanged += new System.EventHandler(this.textBoxAnesthOpen_TextChanged);
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
            this.butSurgClose.TabIndex = 84;
            this.butSurgClose.Text = "Surgery Close";
            this.butSurgClose.UseVisualStyleBackColor = true;
            this.butSurgClose.Click += new System.EventHandler(this.butSurgClose_Click);
            // 
            // textSurgClose
            // 
            this.textSurgClose.Location = new System.Drawing.Point(212, 49);
            this.textSurgClose.Name = "textSurgClose";
            this.textSurgClose.Size = new System.Drawing.Size(86, 20);
            this.textSurgClose.TabIndex = 95;
            this.textSurgClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textSurgClose.TextChanged += new System.EventHandler(this.textBoxSurgClose_TextChanged_1);
            // 
            // textAnesthClose
            // 
            this.textAnesthClose.Location = new System.Drawing.Point(303, 49);
            this.textAnesthClose.Name = "textAnesthClose";
            this.textAnesthClose.ShortcutsEnabled = false;
            this.textAnesthClose.Size = new System.Drawing.Size(100, 20);
            this.textAnesthClose.TabIndex = 96;
            this.textAnesthClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textAnesthClose.TextChanged += new System.EventHandler(this.textBoxAnesthClose_TextChanged);
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
            this.butAnesthOpen.TabIndex = 82;
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
            this.butAnesthClose.TabIndex = 85;
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
            this.butSurgOpen.TabIndex = 83;
            this.butSurgOpen.Text = "Surgery Open";
            this.butSurgOpen.UseVisualStyleBackColor = true;
            this.butSurgOpen.Click += new System.EventHandler(this.butSurgOpen_Click);
            // 
            // textSurgOpen
            // 
            this.textSurgOpen.Location = new System.Drawing.Point(121, 49);
            this.textSurgOpen.Name = "textSurgOpen";
            this.textSurgOpen.Size = new System.Drawing.Size(86, 20);
            this.textSurgOpen.TabIndex = 94;
            this.textSurgOpen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textSurgOpen.TextChanged += new System.EventHandler(this.textBoxSurgOpen_TextChanged);
            // 
            // labelIVAnesthetics
            // 
            this.labelIVAnesthetics.AutoSize = true;
            this.labelIVAnesthetics.Location = new System.Drawing.Point(25, 76);
            this.labelIVAnesthetics.Name = "labelIVAnesthetics";
            this.labelIVAnesthetics.Size = new System.Drawing.Size(75, 13);
            this.labelIVAnesthetics.TabIndex = 106;
            this.labelIVAnesthetics.Text = "IV Anesthetics";
            // 
            // labelAsst
            // 
            this.labelAsst.AutoSize = true;
            this.labelAsst.Location = new System.Drawing.Point(432, 88);
            this.labelAsst.Name = "labelAsst";
            this.labelAsst.Size = new System.Drawing.Size(49, 13);
            this.labelAsst.TabIndex = 90;
            this.labelAsst.Text = "Assistant";
            // 
            // comboCirc
            // 
            this.comboCirc.FormattingEnabled = true;
            this.comboCirc.Location = new System.Drawing.Point(483, 105);
            this.comboCirc.Name = "comboCirc";
            this.comboCirc.Size = new System.Drawing.Size(100, 21);
            this.comboCirc.TabIndex = 91;
            // 
            // labelCirc
            // 
            this.labelCirc.AutoSize = true;
            this.labelCirc.Location = new System.Drawing.Point(533, 88);
            this.labelCirc.Name = "labelCirc";
            this.labelCirc.Size = new System.Drawing.Size(51, 13);
            this.labelCirc.TabIndex = 92;
            this.labelCirc.Text = "Circulator";
            // 
            // comboAsst
            // 
            this.comboAsst.FormattingEnabled = true;
            this.comboAsst.Location = new System.Drawing.Point(381, 105);
            this.comboAsst.Name = "comboAsst";
            this.comboAsst.Size = new System.Drawing.Size(100, 21);
            this.comboAsst.TabIndex = 89;
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
            this.groupBoxDoseCalc.Controls.Add(this.butDose75);
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
            // butDose75
            // 
            this.butDose75.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butDose75.Autosize = true;
            this.butDose75.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butDose75.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butDose75.CornerRadius = 4F;
            this.butDose75.Location = new System.Drawing.Point(123, 98);
            this.butDose75.Name = "butDose75";
            this.butDose75.Size = new System.Drawing.Size(70, 32);
            this.butDose75.TabIndex = 70;
            this.butDose75.Text = ".75";
            this.butDose75.UseVisualStyleBackColor = true;
            this.butDose75.Click += new System.EventHandler(this.butDose75_Click);
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
            // comboAnesthMed
            // 
            this.comboAnesthMed.FormattingEnabled = true;
            this.comboAnesthMed.Location = new System.Drawing.Point(174, 147);
            this.comboAnesthMed.Name = "comboAnesthMed";
            this.comboAnesthMed.Size = new System.Drawing.Size(139, 21);
            this.comboAnesthMed.TabIndex = 77;
            this.comboAnesthMed.SelectedIndexChanged += new System.EventHandler(this.comboAnesthMed_SelectedIndexChanged);
            // 
            // comboSurgeon
            // 
            this.comboSurgeon.FormattingEnabled = true;
            this.comboSurgeon.Location = new System.Drawing.Point(277, 105);
            this.comboSurgeon.Name = "comboSurgeon";
            this.comboSurgeon.Size = new System.Drawing.Size(100, 21);
            this.comboSurgeon.TabIndex = 97;
            // 
            // labelSurgeon
            // 
            this.labelSurgeon.AutoSize = true;
            this.labelSurgeon.Location = new System.Drawing.Point(329, 88);
            this.labelSurgeon.Name = "labelSurgeon";
            this.labelSurgeon.Size = new System.Drawing.Size(47, 13);
            this.labelSurgeon.TabIndex = 98;
            this.labelSurgeon.Text = "Surgeon";
            this.labelSurgeon.Click += new System.EventHandler(this.label3_Click);
            // 
            // comboAnesthetist
            // 
            this.comboAnesthetist.FormattingEnabled = true;
            this.comboAnesthetist.Location = new System.Drawing.Point(174, 105);
            this.comboAnesthetist.Name = "comboAnesthetist";
            this.comboAnesthetist.Size = new System.Drawing.Size(100, 21);
            this.comboAnesthetist.TabIndex = 87;
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
            this.labelAnesthetist.TabIndex = 88;
            this.labelAnesthetist.Text = "Anesthetist";
            this.labelAnesthetist.Click += new System.EventHandler(this.labelAnesthetist_Click);
            // 
            // textPatient
            // 
            this.textPatient.Location = new System.Drawing.Point(12, 16);
            this.textPatient.Name = "textPatient";
            this.textPatient.ReadOnly = true;
            this.textPatient.Size = new System.Drawing.Size(150, 20);
            this.textPatient.TabIndex = 102;
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
            this.labelPatID.TabIndex = 104;
            this.labelPatID.Text = "ID No.";
            // 
            // textPatID
            // 
            this.textPatID.Location = new System.Drawing.Point(49, 44);
            this.textPatID.Name = "textPatID";
            this.textPatID.ReadOnly = true;
            this.textPatID.Size = new System.Drawing.Size(113, 20);
            this.textPatID.TabIndex = 105;
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
            this.groupBoxAnesthMeds.TabIndex = 137;
            this.groupBoxAnesthMeds.TabStop = false;
            this.groupBoxAnesthMeds.Text = "Patient";
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
            this.gridAnesthMeds.TranslationName = "TableAnestheticData";
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
            this.butWasteQty.Size = new System.Drawing.Size(84, 26);
            this.butWasteQty.TabIndex = 108;
            this.butWasteQty.Text = "Waste";
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
            // FormAnestheticRecord
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(816, 732);
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
            this.groupBoxIVSite.ResumeLayout(false);
            this.groupBoxIVSite.PerformLayout();
            this.groupBoxMedRoute.ResumeLayout(false);
            this.groupBoxMedRoute.PerformLayout();
            this.groupBoxDeliveryMethod.ResumeLayout(false);
            this.groupBoxDeliveryMethod.PerformLayout();
            this.groupBoxNotes.ResumeLayout(false);
            this.groupBoxNotes.PerformLayout();
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
		public FormAnestheticRecord(Patient patCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			PatCur = patCur;
			Lan.F(this);
		}


		private void FormAnestheticRecord_Load(object sender, EventArgs e)
		{
			//display Patient name
			textPatient.Text = Patients.GetPat(PatCur.PatNum).GetNameFL();
			//display Patient ID number
			textPatID.Text = PatCur.PatNum.ToString();

			string escortphone = textEscortCellNum.Text;

			if (escortphone != null && escortphone.Length == 10 && Application.CurrentCulture.Name == "en-US")
			{
				textEscortCellNum.Text = "(" + escortphone.Substring(0, 3) + ")" + escortphone.Substring(3, 3) + "-" + escortphone.Substring(6);
			}
			else
			{
				textEscortCellNum.Text = escortphone;
			}

			RefreshListAnesthetics();
			listAnesthetics.SelectedIndex = AnestheticRecords.List.Length - 1;//This works even if no items.


			//Fills provider and assistant comboboxes

			comboSurgeon.Items.Add(Lan.g(this, ""));
			for (int i = 0; i < ProviderC.List.Length; i++)
			{
				comboSurgeon.Items.Add(ProviderC.List[i].Abbr);
				
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
			//Change to suit for Anesthetist, Circulator, Assistant
			{
				comboAsst.Items.Clear();
				comboAsst.Items.Add(Lan.g(this, ""));
				comboAsst.SelectedIndex = 0;
				for (int i = 0; i < ProviderC.List.Length; i++)
				{
					comboAsst.Items.Add(ProviderC.List[i].Abbr);
					if (ProviderC.List[i].ProvNum == PatCur.SecProv)
						comboAsst.SelectedIndex = i + 1;

				}
			}
		}

		private void RefreshListAnesthetics()
		{
			//most recent date at the top
			AnestheticRecords.Refresh(PatCur.PatNum);
			listAnesthetics.Items.Clear();

			for (int i = 0; i < AnestheticRecords.List.Length; i++)
			{

				listAnesthetics.Items.Add(AnestheticRecords.List[i].AnestheticDate);
			}
		}

        private void FillGridAnesthMeds()
        {
            /*AnestheticData.RefreshCache();
            gridAnesthMeds.BeginUpdate();
            gridAnesthMeds.Columns.Clear();
            ODGridColumn col = new ODGridColumn(Lan.g("TableAnestheticData", "Medication"), 130);
            gridAnesthMeds.Columns.Add(col);
            col = new ODGridColumn(Lan.g("TableAnestheticData", "Dose (mL)"), 90);
            gridAnesthMeds.Columns.Add(col);
            col = new ODGridColumn(Lan.g("TableAnestheticData", "Waste(mL)"), 90);
            gridAnesthMeds.Columns.Add(col);
            col = new ODGridColumn(Lan.g("TableAnestheticData", "TimeStamp"), 120);
            gridAnesthMeds.Columns.Add(col);
            gridAnesthMeds.Rows.Clear();
            ODGridRow row;
            string txt;
            for (int i = 0; i < AnestheticDataC.Listt.Count; i++)
            {
                row = new ODGridRow();
                row.Cells.Add(AnestheticDataC.Listt[i].Medication);
                row.Cells.Add(AnestheticDataC.Listt[i].Dose);
                row.Cells.Add(AnestheticDataC.Listt[i].Waste);
                txt = AnestheticDataC.Listt[i].TimeStamp;
                row.Cells.Add(txt);
                gridAnesthMeds.Rows.Add(row);
            }
            gridAnesthMeds.EndUpdate();*/
        }
		private void butAddAnesthetic_Click(object sender, EventArgs e)
		{

			AnestheticRecordCur = new AnestheticRecord();
			AnestheticRecordCur.PatNum = PatCur.PatNum;
			AnestheticRecordCur.AnestheticDate = DateTime.Now;
			AnestheticRecordCur.ProvNum = PatCur.PriProv;
			AnestheticRecords.Insert(AnestheticRecordCur);
			RefreshListAnesthetics();
			listAnesthetics.SelectedIndex = AnestheticRecords.List.Length - 1;//Add -1 after List.Length to select in ascending order
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
            if (Security.IsAuthorized(Permissions.AnesthesiaControlMeds))
            {
                AnestheticRecords.Delete(AnestheticRecords.List[listAnesthetics.SelectedIndex]);
                RefreshListAnesthetics();
                return;
            }

            else
            {
                FormAnesthSecurity FormAS = new FormAnesthSecurity();
                FormAS.ShowDialog();
                AnestheticRecords.Delete(AnestheticRecords.List[listAnesthetics.SelectedIndex]);
                RefreshListAnesthetics();
              
            }
	


		}

		private void butCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

        private void FillGrid()
        {
            //AnestheticData.RefreshCache();
            gridAnesthMeds.BeginUpdate();
            gridAnesthMeds.Columns.Clear();
            ODGridColumn col = new ODGridColumn(Lan.g("TableAnestheticData", "Medication"), 200);
            gridAnesthMeds.Columns.Add(col);
            col = new ODGridColumn(Lan.g("TableAnestheticData", "Dose"),46);
            gridAnesthMeds.Columns.Add(col);
            col = new ODGridColumn(Lan.g("TableAnestheticData", "TimeStamp"),46);
            gridAnesthMeds.Columns.Add(col);
            gridAnesthMeds.Rows.Clear();
            ODGridRow row;
            string txt;
           /* for (int i = 0; i < AnestheticDataC.Listt.Count; i++)
            {
                row = new ODGridRow();
                row.Cells.Add(AnestheticDataC.Listt[i].AnestheticMed);
                row.Cells.Add(AnestheticDataC.Listt[i].Dose);
                row.Cells.Add(AnestheticDataC.Listt[i].TimeStamp);
                gridAnesthMeds.Rows.Add(row);
            }*/
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

		}

		private void sigBox_Click(object sender, EventArgs e)
		{

		}

		private void button20_Click_1(object sender, EventArgs e)
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

		}

		private void textBoxSurgClose_TextChanged_1(object sender, EventArgs e)
		{

		}

		private void butAnesthOpen_Click(object sender, EventArgs e)
		{
			textAnesthOpen.Text = MiscData.GetNowDateTime().ToString("hh:mm:ss tt"); //tt shows "AM/PM", change to "HH:mm:ss" for military time
		}

		private void butSurgOpen_Click(object sender, EventArgs e)
		{
			textSurgOpen.Text = MiscData.GetNowDateTime().ToString("hh:mm:ss tt");
		}

		private void butSurgClose_Click(object sender, EventArgs e)
		{
			textSurgClose.Text = MiscData.GetNowDateTime().ToString("hh:mm:ss tt");
		}

		private void butAnesthClose_Click(object sender, EventArgs e)
		{
			textAnesthClose.Text = MiscData.GetNowDateTime().ToString("hh:mm:ss tt");
		}

		private void butDelAnesthMeds_Click(object sender, EventArgs e)
		{
            FormAnesthSecurity FormAS = new FormAnesthSecurity();
            FormAS.ShowDialog();

		}

		private void butAddAnesthMeds_Click(object sender, EventArgs e)
		{

		}

		private void butPrint_Click(object sender, EventArgs e)
		{
			//pagesPrinted=0;
			pd2=new PrintDocument();
			pd2.PrintPage+=new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.OriginAtMargins=true;
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			//if(!Printers.SetPrinter(pd2,PrintSituation.TPPerio)){
				//return;
			//}

			/*
			printDialog2=new PrintDialog();
			printDialog2.PrinterSettings=new PrinterSettings();
			printDialog2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			if(printDialog2.ShowDialog()!=DialogResult.OK){
				return;
			}
			if(printDialog2.PrinterSettings.IsValid){
				pd2.PrinterSettings=printDialog2.PrinterSettings;
			}
			//uses default printer if selected printer not valid
			*/
			try{
				pd2.Print();
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
			
		}

		private void pd2_PrintPage(object sender, PrintPageEventArgs ev)
		{//raised for each page to be printed.
			Graphics grfx = ev.Graphics;
			//MessageBox.Show(grfx.
			float yPos = 67 + 25 + 20 + 20 + 6;
			float xPos = 100;
			grfx.TranslateTransform(xPos, yPos);
			//gridP.DrawChart(grfx);//have to print graphics first, or they cover up title.
			grfx.TranslateTransform(-xPos, -yPos);
			yPos = 67;
			xPos = 100;
			Font font = new Font("Arial", 9);
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Center;
			//pagesPrinted++;
			ev.HasMorePages = false;
			grfx.Dispose();
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

		}

		private void butClearSig_Click(object sender, EventArgs e)
		{

		}

		private void butDoseDecPoint_Click(object sender, EventArgs e)
		{

		}

		private void butDose0_Click(object sender, EventArgs e)
		{

		}

		private void butDose1_Click(object sender, EventArgs e)
		{

		}

		private void butDose2_Click(object sender, EventArgs e)
		{

		}

		private void butDose3_Click(object sender, EventArgs e)
		{

		}

		private void butDose4_Click(object sender, EventArgs e)
		{

		}

		private void butDose5_Click(object sender, EventArgs e)
		{

		}

		private void butDose6_Click(object sender, EventArgs e)
		{

		}

		private void butDose7_Click(object sender, EventArgs e)
		{

		}

		private void butDose8_Click(object sender, EventArgs e)
		{

		}

		private void butDose9_Click(object sender, EventArgs e)
		{

		}

		private void butDose10_Click(object sender, EventArgs e)
		{

		}

		private void butDose25_Click(object sender, EventArgs e)
		{

		}

		private void butDose50_Click(object sender, EventArgs e)
		{

		}

		private void butDose75_Click(object sender, EventArgs e)
		{

		}

		private void butDoseEnter_Click(object sender, EventArgs e)
		{

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

			FormAnesthesiaScore FormAS = new FormAnesthesiaScore();
			FormAS.ShowDialog();

		}

		private void butWasteQty_Click(object sender, EventArgs e)
		{
            FormAnesthSecurity FormAS = new FormAnesthSecurity();
            FormAS.ShowDialog();
            return;
			FormAnestheticMedsWasteQty FormW = new FormAnestheticMedsWasteQty();
			FormW.ShowDialog();
		}

		private void butClose_Click(object sender, EventArgs e)
		{
            if (Security.IsAuthorized(Permissions.AnesthesiaControlMeds)) {

                Close();
                return;
            }

            else {
                FormAnesthSecurity FormAS = new FormAnesthSecurity();
                FormAS.ShowDialog();

                Close();
            }
	
		}

		private void butOK_Click(object sender, EventArgs e)
		{

			if (!Security.IsAuthorized(Permissions.AnesthesiaControlMeds))
			{
                FormAnesthSecurity FormAS = new FormAnesthSecurity();
                FormAS.ShowDialog();
				return;
			}

			else
            {
                FormAnesthSecurity FormAS = new FormAnesthSecurity();
                FormAS.ShowDialog();

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

        private void odGrid1_CellDoubleClick(object sender, ODGridClickEventArgs e)
        {

        }

        private void gridAnesthMeds_CellDoubleClick_1(object sender, ODGridClickEventArgs e)
        {

        }			

	}
}