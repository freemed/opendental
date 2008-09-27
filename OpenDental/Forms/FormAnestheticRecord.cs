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
		private Patient PatCur;
		private TextBox textBoxPatient;
		private Label labelPatient;
		private Label labelPatID;
		private TextBox textBoxPatID;
		private Label labelIVAnesthetics;
		private ListBox listAnesthetics;
		private GroupBox groupBoxTimes;
		private OpenDental.UI.Button butAddAnesthetic;
		private OpenDental.UI.Button butDelAnesthetic;
		private OpenDental.UI.Button butAnesthOpen;
		private OpenDental.UI.Button butSurgOpen;
		private OpenDental.UI.Button butSurgClose;
		private OpenDental.UI.Button butAnesthClose;
		private TextBox textBoxAnesthOpen;
		private TextBox textBoxSurgOpen;
		private TextBox textBoxSurgClose;
		private TextBox textBoxAnesthClose;
		private Label labelAnesthMed;
		private ComboBox comboAnesthMed;
		public bool IsNew;
		private Label labelAnesthetist;
		private Label labelSurgeon;
		private Label labelAsst;
		private Label labelCirc;
		private ComboBox comboBoxAnesthetist;
		private ComboBox comboBoxSurgeon;
		private ComboBox comboBoxAsst;
		private ComboBox comboBoxCirc;
		private GroupBox groupBoxAnesthMeds;
		private DataGridView dataGridAnestheticMeds;
		private Label labelDose;
		private TextBox textBoxAnesthDose;
		private OpenDental.UI.Button butDelAnesthMeds;
		private GroupBox groupBoxDoseCalc;
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
		private OpenDental.UI.Button butDoseDecPoint;
		private OpenDental.UI.Button butDose10;
		private OpenDental.UI.Button butDose25;
		private OpenDental.UI.Button butDose50;
		private OpenDental.UI.Button butDose100;
		private OpenDental.UI.Button butDoseEnter;
		private GroupBox groupBoxVS;
		private DataGridView dataGridVS;
		private Label labelVSM;
		private TextBox textBoxVSM;
		private Label labelVSMSerNum;
		private TextBox textBoxVSMSerNum;
		private DataGridViewTextBoxColumn BP;
		private DataGridViewTextBoxColumn HR;
		private DataGridViewTextBoxColumn SpO2;
		private DataGridViewTextBoxColumn Temp;
		private DataGridViewTextBoxColumn EtCO2;
		private GroupBox groupBoxSidebarRt;
		private Label labelASA;
		private ComboBox comboBoxASA;
		private Label labelInh;
		private Label labelLperMinN2O;
		private Label labelLperMinO2;
		private ComboBox comboBoxO2LMin;
		private ComboBox comboBoxN2OLMin;
		private CheckBox checkBoxInhN20;
		private CheckBox CheckBoxInhO2;
		private Label labelIVF;
		private ComboBox comboBoxIVF;
		private Label labelIVFVol;
		private TextBox textBoxIVFVol;
		private ComboBox comboBoxIVSite;
		private Label labelIVAtt;
		private ComboBox comboBoxIVAtt;
		private Label labelIVGauge;
		private Label labelGauge;
		private ComboBox comboBoxIVGauge;
		private RadioButton radButIVSiteL;
		private RadioButton radButIVSiteR;
		private OpenDental.UI.Button butAnesthScore;
		private GroupBox groupBoxNotes;
		private RichTextBox richTextBoxNotes;
		private Label labelEscortName;
		private TextBox textBoxEscortName;
		private TextBox textBoxEscortRel;
		private Label labelEscortRel;
		private GroupBox groupBoxHgtWgt;
		private Label labelPatHgt;
		private TextBox textBoxPatHgt;
		private Label labelPatWgt;
		private TextBox textBoxPatWgt;
		private ComboBox comboBoxNPOTime;
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
		private GroupBox groupBoxIVAccess;
		private RadioButton radButIVButFly;
		private RadioButton radButIVCath;
		private PrintDialog printDialog1;
		private GroupBox groupBox1;
		private Label label1;
		private RadioButton radioButton2;
		private RadioButton radioButton1;
		private DataGridViewTextBoxColumn AnestheticMed;
		private DataGridViewTextBoxColumn AnesthDose;
		private DataGridViewTextBoxColumn AnesthTimeStamp;
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butCancel;
		private Label label2;
		private OpenDental.UI.Button butWasteQty;
		private PrintDialog printDialog;


		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnestheticRecord));
			this.listAnesthetics = new System.Windows.Forms.ListBox();
			this.labelAnesthMed = new System.Windows.Forms.Label();
			this.comboAnesthMed = new System.Windows.Forms.ComboBox();
			this.labelVSM = new System.Windows.Forms.Label();
			this.labelVSMSerNum = new System.Windows.Forms.Label();
			this.comboBoxAnesthetist = new System.Windows.Forms.ComboBox();
			this.comboBoxAsst = new System.Windows.Forms.ComboBox();
			this.labelAsst = new System.Windows.Forms.Label();
			this.comboBoxCirc = new System.Windows.Forms.ComboBox();
			this.labelCirc = new System.Windows.Forms.Label();
			this.textBoxSurgOpen = new System.Windows.Forms.TextBox();
			this.textBoxSurgClose = new System.Windows.Forms.TextBox();
			this.textBoxAnesthClose = new System.Windows.Forms.TextBox();
			this.richTextBoxNotes = new System.Windows.Forms.RichTextBox();
			this.textBoxEscortName = new System.Windows.Forms.TextBox();
			this.comboBoxNPOTime = new System.Windows.Forms.ComboBox();
			this.labelEscortName = new System.Windows.Forms.Label();
			this.textBoxEscortRel = new System.Windows.Forms.TextBox();
			this.labelEscortRel = new System.Windows.Forms.Label();
			this.labelPatHgt = new System.Windows.Forms.Label();
			this.textBoxPatHgt = new System.Windows.Forms.TextBox();
			this.labelPatWgt = new System.Windows.Forms.Label();
			this.textBoxPatWgt = new System.Windows.Forms.TextBox();
			this.groupBoxSidebarRt = new System.Windows.Forms.GroupBox();
			this.groupBoxIVSite = new System.Windows.Forms.GroupBox();
			this.comboBoxIVSite = new System.Windows.Forms.ComboBox();
			this.radButIVSiteR = new System.Windows.Forms.RadioButton();
			this.radButIVSiteL = new System.Windows.Forms.RadioButton();
			this.groupBoxIVAccess = new System.Windows.Forms.GroupBox();
			this.radButIVButFly = new System.Windows.Forms.RadioButton();
			this.radButIVCath = new System.Windows.Forms.RadioButton();
			this.groupBoxDeliveryMethod = new System.Windows.Forms.GroupBox();
			this.radButRteETT = new System.Windows.Forms.RadioButton();
			this.radButRteNasCan = new System.Windows.Forms.RadioButton();
			this.radButRteNasHood = new System.Windows.Forms.RadioButton();
			this.labelLperMinN2O = new System.Windows.Forms.Label();
			this.labelLperMinO2 = new System.Windows.Forms.Label();
			this.labelGauge = new System.Windows.Forms.Label();
			this.labelIVGauge = new System.Windows.Forms.Label();
			this.comboBoxIVGauge = new System.Windows.Forms.ComboBox();
			this.butAnesthScore = new OpenDental.UI.Button();
			this.comboBoxO2LMin = new System.Windows.Forms.ComboBox();
			this.labelIVFVol = new System.Windows.Forms.Label();
			this.textBoxIVFVol = new System.Windows.Forms.TextBox();
			this.labelIVF = new System.Windows.Forms.Label();
			this.comboBoxIVF = new System.Windows.Forms.ComboBox();
			this.labelIVAtt = new System.Windows.Forms.Label();
			this.comboBoxIVAtt = new System.Windows.Forms.ComboBox();
			this.labelInh = new System.Windows.Forms.Label();
			this.comboBoxN2OLMin = new System.Windows.Forms.ComboBox();
			this.checkBoxInhN20 = new System.Windows.Forms.CheckBox();
			this.CheckBoxInhO2 = new System.Windows.Forms.CheckBox();
			this.comboBoxASA = new System.Windows.Forms.ComboBox();
			this.labelASA = new System.Windows.Forms.Label();
			this.groupBoxAnesthMeds = new System.Windows.Forms.GroupBox();
			this.butWasteQty = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxPatID = new System.Windows.Forms.TextBox();
			this.labelPatID = new System.Windows.Forms.Label();
			this.labelPatient = new System.Windows.Forms.Label();
			this.textBoxPatient = new System.Windows.Forms.TextBox();
			this.labelAnesthetist = new System.Windows.Forms.Label();
			this.dataGridAnestheticMeds = new System.Windows.Forms.DataGridView();
			this.AnestheticMed = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.AnesthDose = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.AnesthTimeStamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.labelDose = new System.Windows.Forms.Label();
			this.textBoxAnesthDose = new System.Windows.Forms.TextBox();
			this.labelSurgeon = new System.Windows.Forms.Label();
			this.comboBoxSurgeon = new System.Windows.Forms.ComboBox();
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
			this.butDose100 = new OpenDental.UI.Button();
			this.butDoseEnter = new OpenDental.UI.Button();
			this.butDoseDecPoint = new OpenDental.UI.Button();
			this.butAddAnesthetic = new OpenDental.UI.Button();
			this.butDelAnesthetic = new OpenDental.UI.Button();
			this.butDelAnesthMeds = new OpenDental.UI.Button();
			this.labelIVAnesthetics = new System.Windows.Forms.Label();
			this.groupBoxTimes = new System.Windows.Forms.GroupBox();
			this.textBoxAnesthOpen = new System.Windows.Forms.TextBox();
			this.butSurgClose = new OpenDental.UI.Button();
			this.butAnesthOpen = new OpenDental.UI.Button();
			this.butAnesthClose = new OpenDental.UI.Button();
			this.butSurgOpen = new OpenDental.UI.Button();
			this.groupBoxNotes = new System.Windows.Forms.GroupBox();
			this.butClose = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.groupBoxSig = new System.Windows.Forms.GroupBox();
			this.butSignTopaz = new OpenDental.UI.Button();
			this.sigBox = new OpenDental.UI.SignatureBox();
			this.butClearSig = new OpenDental.UI.Button();
			this.groupBoxHgtWgt = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.groupBoxVS = new System.Windows.Forms.GroupBox();
			this.textBoxVSMSerNum = new System.Windows.Forms.TextBox();
			this.dataGridVS = new System.Windows.Forms.DataGridView();
			this.BP = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.HR = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SpO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Temp = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EtCO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.textBoxVSM = new System.Windows.Forms.TextBox();
			this.printDialog = new System.Windows.Forms.PrintDialog();
			this.groupBoxSidebarRt.SuspendLayout();
			this.groupBoxIVSite.SuspendLayout();
			this.groupBoxIVAccess.SuspendLayout();
			this.groupBoxDeliveryMethod.SuspendLayout();
			this.groupBoxAnesthMeds.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridAnestheticMeds)).BeginInit();
			this.groupBoxDoseCalc.SuspendLayout();
			this.groupBoxTimes.SuspendLayout();
			this.groupBoxNotes.SuspendLayout();
			this.groupBoxSig.SuspendLayout();
			this.groupBoxHgtWgt.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBoxVS.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridVS)).BeginInit();
			this.SuspendLayout();
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
			this.labelAnesthMed.Location = new System.Drawing.Point(200, 131);
			this.labelAnesthMed.Name = "labelAnesthMed";
			this.labelAnesthMed.Size = new System.Drawing.Size(111, 13);
			this.labelAnesthMed.TabIndex = 55;
			this.labelAnesthMed.Text = "Anesthetic medication";
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
			// comboBoxAnesthetist
			// 
			this.comboBoxAnesthetist.FormattingEnabled = true;
			this.comboBoxAnesthetist.Location = new System.Drawing.Point(174, 105);
			this.comboBoxAnesthetist.Name = "comboBoxAnesthetist";
			this.comboBoxAnesthetist.Size = new System.Drawing.Size(100, 21);
			this.comboBoxAnesthetist.TabIndex = 87;
			// 
			// comboBoxAsst
			// 
			this.comboBoxAsst.FormattingEnabled = true;
			this.comboBoxAsst.Location = new System.Drawing.Point(381, 105);
			this.comboBoxAsst.Name = "comboBoxAsst";
			this.comboBoxAsst.Size = new System.Drawing.Size(100, 21);
			this.comboBoxAsst.TabIndex = 89;
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
			// comboBoxCirc
			// 
			this.comboBoxCirc.FormattingEnabled = true;
			this.comboBoxCirc.Location = new System.Drawing.Point(483, 105);
			this.comboBoxCirc.Name = "comboBoxCirc";
			this.comboBoxCirc.Size = new System.Drawing.Size(100, 21);
			this.comboBoxCirc.TabIndex = 91;
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
			// textBoxSurgOpen
			// 
			this.textBoxSurgOpen.Location = new System.Drawing.Point(121, 49);
			this.textBoxSurgOpen.Name = "textBoxSurgOpen";
			this.textBoxSurgOpen.Size = new System.Drawing.Size(86, 20);
			this.textBoxSurgOpen.TabIndex = 94;
			this.textBoxSurgOpen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxSurgOpen.TextChanged += new System.EventHandler(this.textBoxSurgOpen_TextChanged);
			// 
			// textBoxSurgClose
			// 
			this.textBoxSurgClose.Location = new System.Drawing.Point(212, 49);
			this.textBoxSurgClose.Name = "textBoxSurgClose";
			this.textBoxSurgClose.Size = new System.Drawing.Size(86, 20);
			this.textBoxSurgClose.TabIndex = 95;
			this.textBoxSurgClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxSurgClose.TextChanged += new System.EventHandler(this.textBoxSurgClose_TextChanged_1);
			// 
			// textBoxAnesthClose
			// 
			this.textBoxAnesthClose.Location = new System.Drawing.Point(303, 49);
			this.textBoxAnesthClose.Name = "textBoxAnesthClose";
			this.textBoxAnesthClose.ShortcutsEnabled = false;
			this.textBoxAnesthClose.Size = new System.Drawing.Size(100, 20);
			this.textBoxAnesthClose.TabIndex = 96;
			this.textBoxAnesthClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxAnesthClose.TextChanged += new System.EventHandler(this.textBoxAnesthClose_TextChanged);
			// 
			// richTextBoxNotes
			// 
			this.richTextBoxNotes.Location = new System.Drawing.Point(12, 19);
			this.richTextBoxNotes.Name = "richTextBoxNotes";
			this.richTextBoxNotes.Size = new System.Drawing.Size(180, 127);
			this.richTextBoxNotes.TabIndex = 103;
			this.richTextBoxNotes.Text = "";
			// 
			// textBoxEscortName
			// 
			this.textBoxEscortName.Location = new System.Drawing.Point(74, 78);
			this.textBoxEscortName.Name = "textBoxEscortName";
			this.textBoxEscortName.Size = new System.Drawing.Size(170, 20);
			this.textBoxEscortName.TabIndex = 122;
			// 
			// comboBoxNPOTime
			// 
			this.comboBoxNPOTime.FormattingEnabled = true;
			this.comboBoxNPOTime.Items.AddRange(new object[] {
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
			this.comboBoxNPOTime.Location = new System.Drawing.Point(215, 35);
			this.comboBoxNPOTime.Name = "comboBoxNPOTime";
			this.comboBoxNPOTime.Size = new System.Drawing.Size(54, 21);
			this.comboBoxNPOTime.TabIndex = 124;
			// 
			// labelEscortName
			// 
			this.labelEscortName.AutoSize = true;
			this.labelEscortName.Location = new System.Drawing.Point(6, 82);
			this.labelEscortName.Name = "labelEscortName";
			this.labelEscortName.Size = new System.Drawing.Size(66, 13);
			this.labelEscortName.TabIndex = 126;
			this.labelEscortName.Text = "Escort name";
			// 
			// textBoxEscortRel
			// 
			this.textBoxEscortRel.Location = new System.Drawing.Point(74, 101);
			this.textBoxEscortRel.Name = "textBoxEscortRel";
			this.textBoxEscortRel.Size = new System.Drawing.Size(170, 20);
			this.textBoxEscortRel.TabIndex = 127;
			// 
			// labelEscortRel
			// 
			this.labelEscortRel.AutoSize = true;
			this.labelEscortRel.Location = new System.Drawing.Point(6, 101);
			this.labelEscortRel.Name = "labelEscortRel";
			this.labelEscortRel.Size = new System.Drawing.Size(65, 13);
			this.labelEscortRel.TabIndex = 128;
			this.labelEscortRel.Text = "Relationship";
			// 
			// labelPatHgt
			// 
			this.labelPatHgt.AutoSize = true;
			this.labelPatHgt.Location = new System.Drawing.Point(9, 16);
			this.labelPatHgt.Name = "labelPatHgt";
			this.labelPatHgt.Size = new System.Drawing.Size(38, 13);
			this.labelPatHgt.TabIndex = 130;
			this.labelPatHgt.Text = "Height";
			this.labelPatHgt.Click += new System.EventHandler(this.label26_Click);
			// 
			// textBoxPatHgt
			// 
			this.textBoxPatHgt.Location = new System.Drawing.Point(53, 13);
			this.textBoxPatHgt.Name = "textBoxPatHgt";
			this.textBoxPatHgt.Size = new System.Drawing.Size(60, 20);
			this.textBoxPatHgt.TabIndex = 129;
			this.textBoxPatHgt.TextChanged += new System.EventHandler(this.textBoxPatHgt_TextChanged);
			// 
			// labelPatWgt
			// 
			this.labelPatWgt.AutoSize = true;
			this.labelPatWgt.Location = new System.Drawing.Point(9, 41);
			this.labelPatWgt.Name = "labelPatWgt";
			this.labelPatWgt.Size = new System.Drawing.Size(41, 13);
			this.labelPatWgt.TabIndex = 132;
			this.labelPatWgt.Text = "Weight";
			// 
			// textBoxPatWgt
			// 
			this.textBoxPatWgt.Location = new System.Drawing.Point(53, 38);
			this.textBoxPatWgt.Name = "textBoxPatWgt";
			this.textBoxPatWgt.Size = new System.Drawing.Size(60, 20);
			this.textBoxPatWgt.TabIndex = 131;
			this.textBoxPatWgt.TextChanged += new System.EventHandler(this.textBoxPatWgt_TextChanged);
			// 
			// groupBoxSidebarRt
			// 
			this.groupBoxSidebarRt.Controls.Add(this.groupBoxIVSite);
			this.groupBoxSidebarRt.Controls.Add(this.groupBoxIVAccess);
			this.groupBoxSidebarRt.Controls.Add(this.groupBoxDeliveryMethod);
			this.groupBoxSidebarRt.Controls.Add(this.labelLperMinN2O);
			this.groupBoxSidebarRt.Controls.Add(this.labelLperMinO2);
			this.groupBoxSidebarRt.Controls.Add(this.labelGauge);
			this.groupBoxSidebarRt.Controls.Add(this.labelIVGauge);
			this.groupBoxSidebarRt.Controls.Add(this.comboBoxIVGauge);
			this.groupBoxSidebarRt.Controls.Add(this.butAnesthScore);
			this.groupBoxSidebarRt.Controls.Add(this.comboBoxO2LMin);
			this.groupBoxSidebarRt.Controls.Add(this.labelIVFVol);
			this.groupBoxSidebarRt.Controls.Add(this.textBoxIVFVol);
			this.groupBoxSidebarRt.Controls.Add(this.labelIVF);
			this.groupBoxSidebarRt.Controls.Add(this.comboBoxIVF);
			this.groupBoxSidebarRt.Controls.Add(this.labelIVAtt);
			this.groupBoxSidebarRt.Controls.Add(this.comboBoxIVAtt);
			this.groupBoxSidebarRt.Controls.Add(this.labelInh);
			this.groupBoxSidebarRt.Controls.Add(this.comboBoxN2OLMin);
			this.groupBoxSidebarRt.Controls.Add(this.checkBoxInhN20);
			this.groupBoxSidebarRt.Controls.Add(this.CheckBoxInhO2);
			this.groupBoxSidebarRt.Controls.Add(this.comboBoxASA);
			this.groupBoxSidebarRt.Controls.Add(this.labelASA);
			this.groupBoxSidebarRt.Location = new System.Drawing.Point(610, 3);
			this.groupBoxSidebarRt.Name = "groupBoxSidebarRt";
			this.groupBoxSidebarRt.Size = new System.Drawing.Size(171, 555);
			this.groupBoxSidebarRt.TabIndex = 136;
			this.groupBoxSidebarRt.TabStop = false;
			this.groupBoxSidebarRt.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// groupBoxIVSite
			// 
			this.groupBoxIVSite.Controls.Add(this.comboBoxIVSite);
			this.groupBoxIVSite.Controls.Add(this.radButIVSiteR);
			this.groupBoxIVSite.Controls.Add(this.radButIVSiteL);
			this.groupBoxIVSite.Location = new System.Drawing.Point(18, 287);
			this.groupBoxIVSite.Name = "groupBoxIVSite";
			this.groupBoxIVSite.Size = new System.Drawing.Size(153, 70);
			this.groupBoxIVSite.TabIndex = 133;
			this.groupBoxIVSite.TabStop = false;
			this.groupBoxIVSite.Text = "IV Site";
			// 
			// comboBoxIVSite
			// 
			this.comboBoxIVSite.FormattingEnabled = true;
			this.comboBoxIVSite.Items.AddRange(new object[] {
            "Antecubital fossa",
            "Forearm (dorsal)",
            "Forearm (ventral)",
            "Hand",
            "Wrist"});
			this.comboBoxIVSite.Location = new System.Drawing.Point(7, 20);
			this.comboBoxIVSite.Name = "comboBoxIVSite";
			this.comboBoxIVSite.Size = new System.Drawing.Size(119, 21);
			this.comboBoxIVSite.TabIndex = 142;
			this.comboBoxIVSite.SelectedIndexChanged += new System.EventHandler(this.comboBox8_SelectedIndexChanged);
			// 
			// radButIVSiteR
			// 
			this.radButIVSiteR.AutoSize = true;
			this.radButIVSiteR.Location = new System.Drawing.Point(10, 45);
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
			this.radButIVSiteL.Location = new System.Drawing.Point(80, 45);
			this.radButIVSiteL.Name = "radButIVSiteL";
			this.radButIVSiteL.Size = new System.Drawing.Size(43, 17);
			this.radButIVSiteL.TabIndex = 154;
			this.radButIVSiteL.TabStop = true;
			this.radButIVSiteL.Text = "Left";
			this.radButIVSiteL.UseVisualStyleBackColor = true;
			// 
			// groupBoxIVAccess
			// 
			this.groupBoxIVAccess.Controls.Add(this.radButIVButFly);
			this.groupBoxIVAccess.Controls.Add(this.radButIVCath);
			this.groupBoxIVAccess.Location = new System.Drawing.Point(17, 200);
			this.groupBoxIVAccess.Name = "groupBoxIVAccess";
			this.groupBoxIVAccess.Size = new System.Drawing.Size(146, 38);
			this.groupBoxIVAccess.TabIndex = 162;
			this.groupBoxIVAccess.TabStop = false;
			this.groupBoxIVAccess.Text = "IVAccess";
			// 
			// radButIVButFly
			// 
			this.radButIVButFly.AutoSize = true;
			this.radButIVButFly.Location = new System.Drawing.Point(83, 15);
			this.radButIVButFly.Name = "radButIVButFly";
			this.radButIVButFly.Size = new System.Drawing.Size(63, 17);
			this.radButIVButFly.TabIndex = 158;
			this.radButIVButFly.TabStop = true;
			this.radButIVButFly.Text = "Butterfly";
			this.radButIVButFly.UseVisualStyleBackColor = true;
			// 
			// radButIVCath
			// 
			this.radButIVCath.AutoSize = true;
			this.radButIVCath.Location = new System.Drawing.Point(18, 15);
			this.radButIVCath.Name = "radButIVCath";
			this.radButIVCath.Size = new System.Drawing.Size(65, 17);
			this.radButIVCath.TabIndex = 157;
			this.radButIVCath.TabStop = true;
			this.radButIVCath.Text = "Catheter";
			this.radButIVCath.UseVisualStyleBackColor = true;
			// 
			// groupBoxDeliveryMethod
			// 
			this.groupBoxDeliveryMethod.Controls.Add(this.radButRteETT);
			this.groupBoxDeliveryMethod.Controls.Add(this.radButRteNasCan);
			this.groupBoxDeliveryMethod.Controls.Add(this.radButRteNasHood);
			this.groupBoxDeliveryMethod.Location = new System.Drawing.Point(17, 124);
			this.groupBoxDeliveryMethod.Name = "groupBoxDeliveryMethod";
			this.groupBoxDeliveryMethod.Size = new System.Drawing.Size(117, 72);
			this.groupBoxDeliveryMethod.TabIndex = 161;
			this.groupBoxDeliveryMethod.TabStop = false;
			this.groupBoxDeliveryMethod.Text = "Delivery method";
			// 
			// radButRteETT
			// 
			this.radButRteETT.AutoSize = true;
			this.radButRteETT.Location = new System.Drawing.Point(5, 52);
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
			this.radButRteNasCan.Location = new System.Drawing.Point(5, 33);
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
			this.radButRteNasHood.Location = new System.Drawing.Point(5, 15);
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
			this.labelLperMinN2O.Location = new System.Drawing.Point(111, 100);
			this.labelLperMinN2O.Name = "labelLperMinN2O";
			this.labelLperMinN2O.Size = new System.Drawing.Size(34, 13);
			this.labelLperMinN2O.TabIndex = 160;
			this.labelLperMinN2O.Text = "L/min";
			// 
			// labelLperMinO2
			// 
			this.labelLperMinO2.AutoSize = true;
			this.labelLperMinO2.Location = new System.Drawing.Point(110, 78);
			this.labelLperMinO2.Name = "labelLperMinO2";
			this.labelLperMinO2.Size = new System.Drawing.Size(34, 13);
			this.labelLperMinO2.TabIndex = 107;
			this.labelLperMinO2.Text = "L/min";
			// 
			// labelGauge
			// 
			this.labelGauge.AutoSize = true;
			this.labelGauge.Location = new System.Drawing.Point(95, 259);
			this.labelGauge.Name = "labelGauge";
			this.labelGauge.Size = new System.Drawing.Size(22, 13);
			this.labelGauge.TabIndex = 107;
			this.labelGauge.Text = "ga.";
			// 
			// labelIVGauge
			// 
			this.labelIVGauge.AutoSize = true;
			this.labelIVGauge.Location = new System.Drawing.Point(24, 239);
			this.labelIVGauge.Name = "labelIVGauge";
			this.labelIVGauge.Size = new System.Drawing.Size(39, 13);
			this.labelIVGauge.TabIndex = 153;
			this.labelIVGauge.Text = "Gauge";
			// 
			// comboBoxIVGauge
			// 
			this.comboBoxIVGauge.FormattingEnabled = true;
			this.comboBoxIVGauge.Items.AddRange(new object[] {
            "18",
            "20",
            "21",
            "22"});
			this.comboBoxIVGauge.Location = new System.Drawing.Point(26, 256);
			this.comboBoxIVGauge.Name = "comboBoxIVGauge";
			this.comboBoxIVGauge.Size = new System.Drawing.Size(65, 21);
			this.comboBoxIVGauge.TabIndex = 152;
			// 
			// butAnesthScore
			// 
			this.butAnesthScore.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAnesthScore.Autosize = true;
			this.butAnesthScore.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAnesthScore.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAnesthScore.CornerRadius = 4F;
			this.butAnesthScore.Location = new System.Drawing.Point(18, 508);
			this.butAnesthScore.Name = "butAnesthScore";
			this.butAnesthScore.Size = new System.Drawing.Size(131, 26);
			this.butAnesthScore.TabIndex = 129;
			this.butAnesthScore.Text = "Post-anesthesia score";
			this.butAnesthScore.UseVisualStyleBackColor = true;
			this.butAnesthScore.Click += new System.EventHandler(this.butAnesthScore_Click_1);
			// 
			// comboBoxO2LMin
			// 
			this.comboBoxO2LMin.FormattingEnabled = true;
			this.comboBoxO2LMin.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.comboBoxO2LMin.Location = new System.Drawing.Point(65, 75);
			this.comboBoxO2LMin.Name = "comboBoxO2LMin";
			this.comboBoxO2LMin.Size = new System.Drawing.Size(40, 21);
			this.comboBoxO2LMin.TabIndex = 150;
			// 
			// labelIVFVol
			// 
			this.labelIVFVol.AutoSize = true;
			this.labelIVFVol.Location = new System.Drawing.Point(85, 450);
			this.labelIVFVol.Name = "labelIVFVol";
			this.labelIVFVol.Size = new System.Drawing.Size(26, 13);
			this.labelIVFVol.TabIndex = 149;
			this.labelIVFVol.Text = "cc\'s";
			// 
			// textBoxIVFVol
			// 
			this.textBoxIVFVol.Location = new System.Drawing.Point(29, 447);
			this.textBoxIVFVol.Name = "textBoxIVFVol";
			this.textBoxIVFVol.Size = new System.Drawing.Size(51, 20);
			this.textBoxIVFVol.TabIndex = 148;
			// 
			// labelIVF
			// 
			this.labelIVF.AutoSize = true;
			this.labelIVF.Location = new System.Drawing.Point(26, 400);
			this.labelIVF.Name = "labelIVF";
			this.labelIVF.Size = new System.Drawing.Size(42, 13);
			this.labelIVF.TabIndex = 147;
			this.labelIVF.Text = "IV Fluid";
			// 
			// comboBoxIVF
			// 
			this.comboBoxIVF.FormattingEnabled = true;
			this.comboBoxIVF.Items.AddRange(new object[] {
            "D5(1/2)NS",
            "D5NS",
            "D5LR",
            "D5W",
            "LR",
            "NS"});
			this.comboBoxIVF.Location = new System.Drawing.Point(28, 419);
			this.comboBoxIVF.Name = "comboBoxIVF";
			this.comboBoxIVF.Size = new System.Drawing.Size(119, 21);
			this.comboBoxIVF.TabIndex = 146;
			// 
			// labelIVAtt
			// 
			this.labelIVAtt.AutoSize = true;
			this.labelIVAtt.Location = new System.Drawing.Point(68, 369);
			this.labelIVAtt.Name = "labelIVAtt";
			this.labelIVAtt.Size = new System.Drawing.Size(48, 13);
			this.labelIVAtt.TabIndex = 145;
			this.labelIVAtt.Text = "Attempts";
			// 
			// comboBoxIVAtt
			// 
			this.comboBoxIVAtt.FormattingEnabled = true;
			this.comboBoxIVAtt.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.comboBoxIVAtt.Location = new System.Drawing.Point(29, 366);
			this.comboBoxIVAtt.Name = "comboBoxIVAtt";
			this.comboBoxIVAtt.Size = new System.Drawing.Size(30, 21);
			this.comboBoxIVAtt.TabIndex = 144;
			// 
			// labelInh
			// 
			this.labelInh.AutoSize = true;
			this.labelInh.Location = new System.Drawing.Point(14, 59);
			this.labelInh.Name = "labelInh";
			this.labelInh.Size = new System.Drawing.Size(96, 13);
			this.labelInh.TabIndex = 132;
			this.labelInh.Text = "Inhalational agents";
			// 
			// comboBoxN2OLMin
			// 
			this.comboBoxN2OLMin.FormattingEnabled = true;
			this.comboBoxN2OLMin.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.comboBoxN2OLMin.Location = new System.Drawing.Point(65, 97);
			this.comboBoxN2OLMin.Name = "comboBoxN2OLMin";
			this.comboBoxN2OLMin.Size = new System.Drawing.Size(40, 21);
			this.comboBoxN2OLMin.TabIndex = 130;
			// 
			// checkBoxInhN20
			// 
			this.checkBoxInhN20.AutoSize = true;
			this.checkBoxInhN20.Location = new System.Drawing.Point(18, 101);
			this.checkBoxInhN20.Name = "checkBoxInhN20";
			this.checkBoxInhN20.Size = new System.Drawing.Size(46, 17);
			this.checkBoxInhN20.TabIndex = 128;
			this.checkBoxInhN20.Text = "N20";
			this.checkBoxInhN20.UseVisualStyleBackColor = true;
			// 
			// CheckBoxInhO2
			// 
			this.CheckBoxInhO2.AutoSize = true;
			this.CheckBoxInhO2.Location = new System.Drawing.Point(18, 77);
			this.CheckBoxInhO2.Name = "CheckBoxInhO2";
			this.CheckBoxInhO2.Size = new System.Drawing.Size(40, 17);
			this.CheckBoxInhO2.TabIndex = 127;
			this.CheckBoxInhO2.Text = "O2";
			this.CheckBoxInhO2.UseVisualStyleBackColor = true;
			// 
			// comboBoxASA
			// 
			this.comboBoxASA.FormattingEnabled = true;
			this.comboBoxASA.Items.AddRange(new object[] {
            "I",
            "II",
            "III"});
			this.comboBoxASA.Location = new System.Drawing.Point(17, 32);
			this.comboBoxASA.Name = "comboBoxASA";
			this.comboBoxASA.Size = new System.Drawing.Size(50, 21);
			this.comboBoxASA.TabIndex = 125;
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
			// groupBoxAnesthMeds
			// 
			this.groupBoxAnesthMeds.Controls.Add(this.butWasteQty);
			this.groupBoxAnesthMeds.Controls.Add(this.label2);
			this.groupBoxAnesthMeds.Controls.Add(this.textBoxPatID);
			this.groupBoxAnesthMeds.Controls.Add(this.labelPatID);
			this.groupBoxAnesthMeds.Controls.Add(this.labelPatient);
			this.groupBoxAnesthMeds.Controls.Add(this.textBoxPatient);
			this.groupBoxAnesthMeds.Controls.Add(this.labelAnesthetist);
			this.groupBoxAnesthMeds.Controls.Add(this.dataGridAnestheticMeds);
			this.groupBoxAnesthMeds.Controls.Add(this.labelDose);
			this.groupBoxAnesthMeds.Controls.Add(this.textBoxAnesthDose);
			this.groupBoxAnesthMeds.Controls.Add(this.comboBoxAnesthetist);
			this.groupBoxAnesthMeds.Controls.Add(this.labelSurgeon);
			this.groupBoxAnesthMeds.Controls.Add(this.comboBoxSurgeon);
			this.groupBoxAnesthMeds.Controls.Add(this.comboAnesthMed);
			this.groupBoxAnesthMeds.Controls.Add(this.groupBoxDoseCalc);
			this.groupBoxAnesthMeds.Controls.Add(this.labelAnesthMed);
			this.groupBoxAnesthMeds.Controls.Add(this.listAnesthetics);
			this.groupBoxAnesthMeds.Controls.Add(this.butAddAnesthetic);
			this.groupBoxAnesthMeds.Controls.Add(this.comboBoxAsst);
			this.groupBoxAnesthMeds.Controls.Add(this.labelCirc);
			this.groupBoxAnesthMeds.Controls.Add(this.butDelAnesthetic);
			this.groupBoxAnesthMeds.Controls.Add(this.comboBoxCirc);
			this.groupBoxAnesthMeds.Controls.Add(this.labelAsst);
			this.groupBoxAnesthMeds.Controls.Add(this.butDelAnesthMeds);
			this.groupBoxAnesthMeds.Controls.Add(this.labelIVAnesthetics);
			this.groupBoxAnesthMeds.Controls.Add(this.groupBoxTimes);
			this.groupBoxAnesthMeds.Location = new System.Drawing.Point(12, 4);
			this.groupBoxAnesthMeds.Name = "groupBoxAnesthMeds";
			this.groupBoxAnesthMeds.Size = new System.Drawing.Size(592, 342);
			this.groupBoxAnesthMeds.TabIndex = 137;
			this.groupBoxAnesthMeds.TabStop = false;
			this.groupBoxAnesthMeds.Text = "Patient";
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
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(406, 321);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(150, 13);
			this.label2.TabIndex = 107;
			this.label2.Text = "(Doses must be entered in mL)";
			// 
			// textBoxPatID
			// 
			this.textBoxPatID.Location = new System.Drawing.Point(49, 44);
			this.textBoxPatID.Name = "textBoxPatID";
			this.textBoxPatID.ReadOnly = true;
			this.textBoxPatID.Size = new System.Drawing.Size(113, 20);
			this.textBoxPatID.TabIndex = 105;
			this.textBoxPatID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxPatID.TextChanged += new System.EventHandler(this.textBoxPatID_TextChanged);
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
			// labelPatient
			// 
			this.labelPatient.AutoSize = true;
			this.labelPatient.Location = new System.Drawing.Point(3, 19);
			this.labelPatient.Name = "labelPatient";
			this.labelPatient.Size = new System.Drawing.Size(0, 13);
			this.labelPatient.TabIndex = 103;
			// 
			// textBoxPatient
			// 
			this.textBoxPatient.Location = new System.Drawing.Point(12, 16);
			this.textBoxPatient.Name = "textBoxPatient";
			this.textBoxPatient.ReadOnly = true;
			this.textBoxPatient.Size = new System.Drawing.Size(150, 20);
			this.textBoxPatient.TabIndex = 102;
			this.textBoxPatient.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxPatient.TextChanged += new System.EventHandler(this.textBoxPatient_TextChanged);
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
			// dataGridAnestheticMeds
			// 
			this.dataGridAnestheticMeds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridAnestheticMeds.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AnestheticMed,
            this.AnesthDose,
            this.AnesthTimeStamp});
			this.dataGridAnestheticMeds.Location = new System.Drawing.Point(27, 176);
			this.dataGridAnestheticMeds.Name = "dataGridAnestheticMeds";
			this.dataGridAnestheticMeds.Size = new System.Drawing.Size(346, 127);
			this.dataGridAnestheticMeds.TabIndex = 101;
			this.dataGridAnestheticMeds.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridAnestheticMeds_CellContentClick_1);
			// 
			// AnestheticMed
			// 
			this.AnestheticMed.HeaderText = "Anesthetic medication";
			this.AnestheticMed.Name = "AnestheticMed";
			this.AnestheticMed.Width = 150;
			// 
			// AnesthDose
			// 
			this.AnesthDose.HeaderText = "Dose";
			this.AnesthDose.Name = "AnesthDose";
			this.AnesthDose.Width = 70;
			// 
			// AnesthTimeStamp
			// 
			this.AnesthTimeStamp.HeaderText = "Time Stamp";
			this.AnesthTimeStamp.Name = "AnesthTimeStamp";
			this.AnesthTimeStamp.Width = 90;
			// 
			// labelDose
			// 
			this.labelDose.AutoSize = true;
			this.labelDose.Location = new System.Drawing.Point(340, 131);
			this.labelDose.Name = "labelDose";
			this.labelDose.Size = new System.Drawing.Size(32, 13);
			this.labelDose.TabIndex = 100;
			this.labelDose.Text = "Dose";
			// 
			// textBoxAnesthDose
			// 
			this.textBoxAnesthDose.Location = new System.Drawing.Point(318, 148);
			this.textBoxAnesthDose.Name = "textBoxAnesthDose";
			this.textBoxAnesthDose.Size = new System.Drawing.Size(54, 20);
			this.textBoxAnesthDose.TabIndex = 99;
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
			// comboBoxSurgeon
			// 
			this.comboBoxSurgeon.FormattingEnabled = true;
			this.comboBoxSurgeon.Location = new System.Drawing.Point(277, 105);
			this.comboBoxSurgeon.Name = "comboBoxSurgeon";
			this.comboBoxSurgeon.Size = new System.Drawing.Size(100, 21);
			this.comboBoxSurgeon.TabIndex = 97;
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
			this.groupBoxDoseCalc.Controls.Add(this.butDose100);
			this.groupBoxDoseCalc.Controls.Add(this.butDoseEnter);
			this.groupBoxDoseCalc.Controls.Add(this.butDoseDecPoint);
			this.groupBoxDoseCalc.Location = new System.Drawing.Point(379, 141);
			this.groupBoxDoseCalc.Name = "groupBoxDoseCalc";
			this.groupBoxDoseCalc.Size = new System.Drawing.Size(200, 177);
			this.groupBoxDoseCalc.TabIndex = 54;
			this.groupBoxDoseCalc.TabStop = false;
			this.groupBoxDoseCalc.Text = "Click to add dose";
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
			this.butDose25.Text = "25";
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
			this.butDose50.Text = "50";
			this.butDose50.UseVisualStyleBackColor = true;
			this.butDose50.Click += new System.EventHandler(this.butDose50_Click);
			// 
			// butDose100
			// 
			this.butDose100.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDose100.Autosize = true;
			this.butDose100.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDose100.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDose100.CornerRadius = 4F;
			this.butDose100.Location = new System.Drawing.Point(123, 98);
			this.butDose100.Name = "butDose100";
			this.butDose100.Size = new System.Drawing.Size(70, 32);
			this.butDose100.TabIndex = 70;
			this.butDose100.Text = "100";
			this.butDose100.UseVisualStyleBackColor = true;
			this.butDose100.Click += new System.EventHandler(this.butDose100_Click);
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
			this.butAddAnesthetic.Text = "Add";
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
			// labelIVAnesthetics
			// 
			this.labelIVAnesthetics.AutoSize = true;
			this.labelIVAnesthetics.Location = new System.Drawing.Point(25, 76);
			this.labelIVAnesthetics.Name = "labelIVAnesthetics";
			this.labelIVAnesthetics.Size = new System.Drawing.Size(75, 13);
			this.labelIVAnesthetics.TabIndex = 106;
			this.labelIVAnesthetics.Text = "IV Anesthetics";
			// 
			// groupBoxTimes
			// 
			this.groupBoxTimes.Controls.Add(this.textBoxAnesthOpen);
			this.groupBoxTimes.Controls.Add(this.butSurgClose);
			this.groupBoxTimes.Controls.Add(this.textBoxSurgClose);
			this.groupBoxTimes.Controls.Add(this.textBoxAnesthClose);
			this.groupBoxTimes.Controls.Add(this.butAnesthOpen);
			this.groupBoxTimes.Controls.Add(this.butAnesthClose);
			this.groupBoxTimes.Controls.Add(this.butSurgOpen);
			this.groupBoxTimes.Controls.Add(this.textBoxSurgOpen);
			this.groupBoxTimes.Location = new System.Drawing.Point(173, 11);
			this.groupBoxTimes.Name = "groupBoxTimes";
			this.groupBoxTimes.Size = new System.Drawing.Size(413, 76);
			this.groupBoxTimes.TabIndex = 96;
			this.groupBoxTimes.TabStop = false;
			this.groupBoxTimes.Text = "Times";
			// 
			// textBoxAnesthOpen
			// 
			this.textBoxAnesthOpen.Location = new System.Drawing.Point(16, 49);
			this.textBoxAnesthOpen.Name = "textBoxAnesthOpen";
			this.textBoxAnesthOpen.Size = new System.Drawing.Size(100, 20);
			this.textBoxAnesthOpen.TabIndex = 97;
			this.textBoxAnesthOpen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxAnesthOpen.TextChanged += new System.EventHandler(this.textBoxAnesthOpen_TextChanged);
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
			// groupBoxNotes
			// 
			this.groupBoxNotes.Controls.Add(this.butClose);
			this.groupBoxNotes.Controls.Add(this.butCancel);
			this.groupBoxNotes.Controls.Add(this.richTextBoxNotes);
			this.groupBoxNotes.Controls.Add(this.butPrint);
			this.groupBoxNotes.Controls.Add(this.groupBoxSig);
			this.groupBoxNotes.Controls.Add(this.groupBoxHgtWgt);
			this.groupBoxNotes.Location = new System.Drawing.Point(12, 565);
			this.groupBoxNotes.Name = "groupBoxNotes";
			this.groupBoxNotes.Size = new System.Drawing.Size(769, 155);
			this.groupBoxNotes.TabIndex = 138;
			this.groupBoxNotes.TabStop = false;
			this.groupBoxNotes.Text = "Notes (record additional meds/routes/times here)";
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClose.Location = new System.Drawing.Point(673, 116);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(90, 26);
			this.butClose.TabIndex = 142;
			this.butClose.Text = "Save and Close";
			this.butClose.UseVisualStyleBackColor = true;
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
			this.butCancel.Location = new System.Drawing.Point(601, 116);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(66, 26);
			this.butCancel.TabIndex = 141;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
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
			this.butPrint.Location = new System.Drawing.Point(488, 116);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(88, 26);
			this.butPrint.TabIndex = 102;
			this.butPrint.Text = "Print";
			this.butPrint.UseVisualStyleBackColor = true;
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// groupBoxSig
			// 
			this.groupBoxSig.Controls.Add(this.butSignTopaz);
			this.groupBoxSig.Controls.Add(this.sigBox);
			this.groupBoxSig.Controls.Add(this.butClearSig);
			this.groupBoxSig.Location = new System.Drawing.Point(488, 0);
			this.groupBoxSig.Name = "groupBoxSig";
			this.groupBoxSig.Size = new System.Drawing.Size(281, 105);
			this.groupBoxSig.TabIndex = 139;
			this.groupBoxSig.TabStop = false;
			this.groupBoxSig.Text = "Signature/Initials";
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
			// sigBox
			// 
			this.sigBox.Location = new System.Drawing.Point(10, 19);
			this.sigBox.Name = "sigBox";
			this.sigBox.Size = new System.Drawing.Size(158, 74);
			this.sigBox.TabIndex = 135;
			this.sigBox.Click += new System.EventHandler(this.sigBox_Click);
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
			this.groupBoxHgtWgt.Controls.Add(this.groupBox1);
			this.groupBoxHgtWgt.Controls.Add(this.labelEscortRel);
			this.groupBoxHgtWgt.Controls.Add(this.labelEscortName);
			this.groupBoxHgtWgt.Controls.Add(this.textBoxEscortName);
			this.groupBoxHgtWgt.Controls.Add(this.textBoxEscortRel);
			this.groupBoxHgtWgt.Location = new System.Drawing.Point(203, 19);
			this.groupBoxHgtWgt.Name = "groupBoxHgtWgt";
			this.groupBoxHgtWgt.Size = new System.Drawing.Size(278, 130);
			this.groupBoxHgtWgt.TabIndex = 138;
			this.groupBoxHgtWgt.TabStop = false;
			this.groupBoxHgtWgt.Enter += new System.EventHandler(this.groupBoxHgtWgt_Enter);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.comboBoxNPOTime);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Controls.Add(this.textBoxPatHgt);
			this.groupBox1.Controls.Add(this.labelPatWgt);
			this.groupBox1.Controls.Add(this.labelPatHgt);
			this.groupBox1.Controls.Add(this.textBoxPatWgt);
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(278, 70);
			this.groupBox1.TabIndex = 133;
			this.groupBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(212, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 133;
			this.label1.Text = "NPO Since";
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(166, 39);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(37, 17);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "kg";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new System.Drawing.Point(122, 39);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(41, 17);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "lbs.";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// groupBoxVS
			// 
			this.groupBoxVS.Controls.Add(this.textBoxVSMSerNum);
			this.groupBoxVS.Controls.Add(this.dataGridVS);
			this.groupBoxVS.Controls.Add(this.textBoxVSM);
			this.groupBoxVS.Controls.Add(this.labelVSM);
			this.groupBoxVS.Controls.Add(this.labelVSMSerNum);
			this.groupBoxVS.Location = new System.Drawing.Point(12, 352);
			this.groupBoxVS.Name = "groupBoxVS";
			this.groupBoxVS.Size = new System.Drawing.Size(592, 207);
			this.groupBoxVS.TabIndex = 139;
			this.groupBoxVS.TabStop = false;
			this.groupBoxVS.Text = "Vital Signs";
			// 
			// textBoxVSMSerNum
			// 
			this.textBoxVSMSerNum.Location = new System.Drawing.Point(347, 19);
			this.textBoxVSMSerNum.Name = "textBoxVSMSerNum";
			this.textBoxVSMSerNum.Size = new System.Drawing.Size(88, 20);
			this.textBoxVSMSerNum.TabIndex = 132;
			// 
			// dataGridVS
			// 
			this.dataGridVS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridVS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BP,
            this.HR,
            this.SpO2,
            this.Temp,
            this.EtCO2});
			this.dataGridVS.Location = new System.Drawing.Point(37, 51);
			this.dataGridVS.Name = "dataGridVS";
			this.dataGridVS.Size = new System.Drawing.Size(542, 141);
			this.dataGridVS.TabIndex = 131;
			this.dataGridVS.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridVS_CellContentClick);
			// 
			// BP
			// 
			this.BP.HeaderText = "BP";
			this.BP.Name = "BP";
			// 
			// HR
			// 
			this.HR.HeaderText = "Heart Rate";
			this.HR.Name = "HR";
			// 
			// SpO2
			// 
			this.SpO2.HeaderText = "SpO2";
			this.SpO2.Name = "SpO2";
			// 
			// Temp
			// 
			this.Temp.HeaderText = "Temp";
			this.Temp.Name = "Temp";
			// 
			// EtCO2
			// 
			this.EtCO2.HeaderText = "EtCO2";
			this.EtCO2.Name = "EtCO2";
			// 
			// textBoxVSM
			// 
			this.textBoxVSM.Location = new System.Drawing.Point(170, 19);
			this.textBoxVSM.Name = "textBoxVSM";
			this.textBoxVSM.Size = new System.Drawing.Size(88, 20);
			this.textBoxVSM.TabIndex = 130;
			// 
			// FormAnestheticRecord
			// 
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(784, 732);
			this.Controls.Add(this.groupBoxAnesthMeds);
			this.Controls.Add(this.groupBoxSidebarRt);
			this.Controls.Add(this.groupBoxNotes);
			this.Controls.Add(this.groupBoxVS);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormAnestheticRecord";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " ";
			this.Load += new System.EventHandler(this.FormAnestheticRecord_Load);
			this.groupBoxSidebarRt.ResumeLayout(false);
			this.groupBoxSidebarRt.PerformLayout();
			this.groupBoxIVSite.ResumeLayout(false);
			this.groupBoxIVSite.PerformLayout();
			this.groupBoxIVAccess.ResumeLayout(false);
			this.groupBoxIVAccess.PerformLayout();
			this.groupBoxDeliveryMethod.ResumeLayout(false);
			this.groupBoxDeliveryMethod.PerformLayout();
			this.groupBoxAnesthMeds.ResumeLayout(false);
			this.groupBoxAnesthMeds.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridAnestheticMeds)).EndInit();
			this.groupBoxDoseCalc.ResumeLayout(false);
			this.groupBoxTimes.ResumeLayout(false);
			this.groupBoxTimes.PerformLayout();
			this.groupBoxNotes.ResumeLayout(false);
			this.groupBoxSig.ResumeLayout(false);
			this.groupBoxHgtWgt.ResumeLayout(false);
			this.groupBoxHgtWgt.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBoxVS.ResumeLayout(false);
			this.groupBoxVS.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridVS)).EndInit();
			this.ResumeLayout(false);

		}
		public FormAnestheticRecord(Patient patCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			PatCur = patCur;
			Lan.F(this);
		}



		private void FormAnestheticRecord_Load(object sender, EventArgs e)
		{
			//display Patient name
			textBoxPatient.Text = Patients.GetPat(PatCur.PatNum).GetNameFL();
			//display Patient ID number
			textBoxPatID.Text = PatCur.PatNum.ToString();
			RefreshListAnesthetics();
			listAnesthetics.SelectedIndex = AnestheticRecords.List.Length - 1;//This works even if no items.

			/*
			//ADDING FUNCTIONALITY 2008-03-18: fills provider and assistant comboboxes

			for (int i = 0; i < ProviderC.List.Length; i++)
			{
				comboBoxSurgeon.Items.Add(ProviderC.List[i].Abbr);
				if (ProviderC.List[i].ProvNum == PatCur.Surgeon)
					comboBoxSurgeon.SelectedIndex = i;
			}

			if (comboBoxSurgeon.SelectedIndex == -1)
			{
				int defaultindex = Providers.GetIndex(PrefC.GetInt("PriProv"));
				if (defaultindex == -1)
				{//default provider hidden
					comboBoxSurgeon.SelectedIndex = 0;
				}
				else
				{
					comboBoxSurgeon.SelectedIndex = defaultindex;
				}
			}
			//Change to suit for Anesthetist, Circulator, Assistant
			//comboSecProv.Items.Clear();
			//comboSecProv.Items.Add(Lan.g(this, "none"));
			//comboSecProv.SelectedIndex = 0;
			//for (int i = 0; i < ProviderC.List.Length; i++)
			//{
				//comboSecProv.Items.Add(ProviderC.List[i].Abbr);
				//if (ProviderC.List[i].ProvNum == PatCur.SecProv)
					//comboSecProv.SelectedIndex = i + 1;
			 */

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
			AnestheticRecords.Delete(AnestheticRecords.List[listAnesthetics.SelectedIndex]);
			RefreshListAnesthetics();

		}

		private void butOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void butCancel_Click(object sender, EventArgs e)
		{

		}

		private void butAnesthScore_Click(object sender, EventArgs e)
		{

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

		private void butClose_Click(object sender, System.EventArgs e)
		{
			Close();
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
			textBoxAnesthOpen.Text = MiscData.GetNowDateTime().ToString("hh:mm:ss tt"); //tt shows AM/PM, change to "HH:mm:ss" for military time
		}

		private void butSurgOpen_Click(object sender, EventArgs e)
		{
			textBoxSurgOpen.Text = MiscData.GetNowDateTime().ToString("hh:mm:ss tt");
		}

		private void butSurgClose_Click(object sender, EventArgs e)
		{
			textBoxSurgClose.Text = MiscData.GetNowDateTime().ToString("hh:mm:ss tt");
		}

		private void butAnesthClose_Click(object sender, EventArgs e)
		{
			textBoxAnesthClose.Text = MiscData.GetNowDateTime().ToString("hh:mm:ss tt");
		}

		private void butDelAnesthMeds_Click(object sender, EventArgs e)
		{

		}

		private void butAddAnesthMeds_Click(object sender, EventArgs e)
		{

		}

		private void butPrint_Click(object sender, EventArgs e)
		{

		}

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void dataGridAnestheticMeds_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void dataGridVS_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void groupBox5_Enter(object sender, EventArgs e)
		{

		}

		private void dataGridAnestheticMeds_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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

		private void butDose100_Click(object sender, EventArgs e)
		{

		}

		private void butDoseEnter_Click(object sender, EventArgs e)
		{

		}

		private void FormAnestheticRecord_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (listAnesthetics.SelectedIndex != -1)
			{
				//SaveCurAnesthetic(AnestheticRecordCur.AnestheticRecordNum);
			}
		}

		private void textBoxAnesthOpen_TextChanged(object sender, EventArgs e)
		{

		}


		private void labelRoute_Click(object sender, EventArgs e)
		{

		}

		private void butAnesthScore_Click_1(object sender, EventArgs e)
		{

			FormAnesthesiaScore FormAS = new FormAnesthesiaScore();
			FormAS.ShowDialog();

		}

		private void butWasteQty_Click(object sender, EventArgs e)
		{
			FormAnestheticMedsWasteQty FormW = new FormAnestheticMedsWasteQty();
			FormW.ShowDialog();
		}
	}
}