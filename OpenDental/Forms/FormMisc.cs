using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormMisc : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private IContainer components;
		private System.Windows.Forms.TextBox textTreatNote;
		private System.Windows.Forms.CheckBox checkShowCC;
		private System.Windows.Forms.TextBox textMainWindowTitle;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.CheckBox checkITooth;
		private System.Windows.Forms.CheckBox checkTreatPlanShowGraphics;
		private System.Windows.Forms.CheckBox checkTreatPlanShowCompleted;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkTreatPlanShowIns;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioUsePatNum;
		private System.Windows.Forms.RadioButton radioUseChartNumber;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidNumber textStatementsCalcDueDate;
		private System.Windows.Forms.CheckBox checkEclaimsSeparateTreatProv;
		private System.Windows.Forms.CheckBox checkRandomPrimaryKeys;
		private System.Windows.Forms.CheckBox checkBalancesDontSubtractIns;
		private System.Windows.Forms.Label label3;
		private OpenDental.ValidNumber textSigInterval;
		private System.Windows.Forms.CheckBox checkInsurancePlansShared;
		private CheckBox checkMedicalEclaimsEnabled;
		private CheckBox checkStatementShowReturnAddress;
		private GroupBox groupBox3;
		private RadioButton radioShowIDpatNum;
		private RadioButton radioShowIDchartNum;
		private RadioButton radioShowIDnone;
		private OpenDental.UI.Button butLanguages;
		private Label label4;
        private CheckBox checkSolidBlockouts;
		private CheckBox checkAgingMonthly;
		private GroupBox groupBox4;
		private CheckBox checkBoldBalance;
		private CheckBox checkShowAccountNotes;
		private CheckBox checkSimpleStatement;
		private CheckBox checkBox5;
		private GroupBox groupBox6;
		private CheckBox checkBrokenApptNote;
		private GroupBox groupBox7;
		private Label label5;
		private TextBox textBoxStationary;
		private TextBox textBoxLogo;
		private Label label6;
		private Label label7;
		private Label label8;
		private TextBox textBoxDocPath;
		private ToolTip toolTip1;
		private PictureBox pictureBox1;
		private Label label9;
		private CheckBox checkApptBubbleDelay;
		private CheckBox checkStoreCCnumbers;
		private CheckBox checkAppointmentBubblesDisabled;
		private CheckBox checkDeductibleBeforePercent;
		private System.Windows.Forms.Label label1;// Required designer variable.

		///<summary></summary>
		public FormMisc(){
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMisc));
			this.textTreatNote = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkShowCC = new System.Windows.Forms.CheckBox();
			this.textMainWindowTitle = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.checkITooth = new System.Windows.Forms.CheckBox();
			this.checkTreatPlanShowGraphics = new System.Windows.Forms.CheckBox();
			this.checkTreatPlanShowCompleted = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.checkTreatPlanShowIns = new System.Windows.Forms.CheckBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.checkSimpleStatement = new System.Windows.Forms.CheckBox();
			this.checkAgingMonthly = new System.Windows.Forms.CheckBox();
			this.checkStatementShowReturnAddress = new System.Windows.Forms.CheckBox();
			this.checkBalancesDontSubtractIns = new System.Windows.Forms.CheckBox();
			this.textStatementsCalcDueDate = new OpenDental.ValidNumber();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioUsePatNum = new System.Windows.Forms.RadioButton();
			this.radioUseChartNumber = new System.Windows.Forms.RadioButton();
			this.checkEclaimsSeparateTreatProv = new System.Windows.Forms.CheckBox();
			this.checkRandomPrimaryKeys = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.checkInsurancePlansShared = new System.Windows.Forms.CheckBox();
			this.checkMedicalEclaimsEnabled = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.radioShowIDnone = new System.Windows.Forms.RadioButton();
			this.radioShowIDpatNum = new System.Windows.Forms.RadioButton();
			this.radioShowIDchartNum = new System.Windows.Forms.RadioButton();
			this.label4 = new System.Windows.Forms.Label();
			this.checkSolidBlockouts = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.checkDeductibleBeforePercent = new System.Windows.Forms.CheckBox();
			this.checkStoreCCnumbers = new System.Windows.Forms.CheckBox();
			this.checkBoldBalance = new System.Windows.Forms.CheckBox();
			this.checkShowAccountNotes = new System.Windows.Forms.CheckBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.checkAppointmentBubblesDisabled = new System.Windows.Forms.CheckBox();
			this.checkApptBubbleDelay = new System.Windows.Forms.CheckBox();
			this.checkBrokenApptNote = new System.Windows.Forms.CheckBox();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxDocPath = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBoxStationary = new System.Windows.Forms.TextBox();
			this.textBoxLogo = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.butLanguages = new OpenDental.UI.Button();
			this.textSigInterval = new OpenDental.ValidNumber();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// textTreatNote
			// 
			this.textTreatNote.AcceptsReturn = true;
			this.textTreatNote.Location = new System.Drawing.Point(17,40);
			this.textTreatNote.Multiline = true;
			this.textTreatNote.Name = "textTreatNote";
			this.textTreatNote.Size = new System.Drawing.Size(346,114);
			this.textTreatNote.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18,22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(268,15);
			this.label1.TabIndex = 35;
			this.label1.Text = "Default Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkShowCC
			// 
			this.checkShowCC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowCC.Location = new System.Drawing.Point(19,43);
			this.checkShowCC.Name = "checkShowCC";
			this.checkShowCC.Size = new System.Drawing.Size(216,17);
			this.checkShowCC.TabIndex = 36;
			this.checkShowCC.Text = "Show Credit Card Info";
			// 
			// textMainWindowTitle
			// 
			this.textMainWindowTitle.Location = new System.Drawing.Point(128,413);
			this.textMainWindowTitle.Name = "textMainWindowTitle";
			this.textMainWindowTitle.Size = new System.Drawing.Size(431,20);
			this.textMainWindowTitle.TabIndex = 38;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(-23,415);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(149,17);
			this.label14.TabIndex = 39;
			this.label14.Text = "Main Window Title";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkITooth
			// 
			this.checkITooth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkITooth.Location = new System.Drawing.Point(448,463);
			this.checkITooth.Name = "checkITooth";
			this.checkITooth.Size = new System.Drawing.Size(239,18);
			this.checkITooth.TabIndex = 42;
			this.checkITooth.Text = "Use International Tooth Numbers (11-48)";
			// 
			// checkTreatPlanShowGraphics
			// 
			this.checkTreatPlanShowGraphics.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanShowGraphics.Location = new System.Drawing.Point(16,159);
			this.checkTreatPlanShowGraphics.Name = "checkTreatPlanShowGraphics";
			this.checkTreatPlanShowGraphics.Size = new System.Drawing.Size(339,17);
			this.checkTreatPlanShowGraphics.TabIndex = 46;
			this.checkTreatPlanShowGraphics.Text = "Show Graphical Tooth Chart";
			// 
			// checkTreatPlanShowCompleted
			// 
			this.checkTreatPlanShowCompleted.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanShowCompleted.Location = new System.Drawing.Point(16,178);
			this.checkTreatPlanShowCompleted.Name = "checkTreatPlanShowCompleted";
			this.checkTreatPlanShowCompleted.Size = new System.Drawing.Size(334,17);
			this.checkTreatPlanShowCompleted.TabIndex = 47;
			this.checkTreatPlanShowCompleted.Text = "Show Completed Work on Graphical Tooth Chart";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBox5);
			this.groupBox1.Controls.Add(this.checkTreatPlanShowIns);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textTreatNote);
			this.groupBox1.Controls.Add(this.checkTreatPlanShowGraphics);
			this.groupBox1.Controls.Add(this.checkTreatPlanShowCompleted);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(32,26);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(381,240);
			this.groupBox1.TabIndex = 48;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Treatment Plans";
			// 
			// checkBox5
			// 
			this.checkBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox5.Location = new System.Drawing.Point(16,216);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(334,17);
			this.checkBox5.TabIndex = 49;
			this.checkBox5.Text = "Show Tx Plan colors in chart module graphical tooth chart";
			this.checkBox5.Visible = false;
			// 
			// checkTreatPlanShowIns
			// 
			this.checkTreatPlanShowIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanShowIns.Location = new System.Drawing.Point(16,197);
			this.checkTreatPlanShowIns.Name = "checkTreatPlanShowIns";
			this.checkTreatPlanShowIns.Size = new System.Drawing.Size(334,17);
			this.checkTreatPlanShowIns.TabIndex = 48;
			this.checkTreatPlanShowIns.Text = "Show Insurance Estimates";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.checkSimpleStatement);
			this.groupBox5.Controls.Add(this.checkAgingMonthly);
			this.groupBox5.Controls.Add(this.checkStatementShowReturnAddress);
			this.groupBox5.Controls.Add(this.checkBalancesDontSubtractIns);
			this.groupBox5.Controls.Add(this.textStatementsCalcDueDate);
			this.groupBox5.Controls.Add(this.label2);
			this.groupBox5.Controls.Add(this.groupBox2);
			this.groupBox5.Controls.Add(this.checkShowCC);
			this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox5.Location = new System.Drawing.Point(436,26);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(339,240);
			this.groupBox5.TabIndex = 52;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Statements";
			// 
			// checkSimpleStatement
			// 
			this.checkSimpleStatement.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSimpleStatement.Location = new System.Drawing.Point(19,214);
			this.checkSimpleStatement.Name = "checkSimpleStatement";
			this.checkSimpleStatement.Size = new System.Drawing.Size(288,17);
			this.checkSimpleStatement.TabIndex = 58;
			this.checkSimpleStatement.Text = "Print simiple statements with less detail ";
			// 
			// checkAgingMonthly
			// 
			this.checkAgingMonthly.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAgingMonthly.Location = new System.Drawing.Point(19,194);
			this.checkAgingMonthly.Name = "checkAgingMonthly";
			this.checkAgingMonthly.Size = new System.Drawing.Size(288,17);
			this.checkAgingMonthly.TabIndex = 57;
			this.checkAgingMonthly.Text = "Aging calculated monthly instead of daily";
			// 
			// checkStatementShowReturnAddress
			// 
			this.checkStatementShowReturnAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStatementShowReturnAddress.Location = new System.Drawing.Point(19,22);
			this.checkStatementShowReturnAddress.Name = "checkStatementShowReturnAddress";
			this.checkStatementShowReturnAddress.Size = new System.Drawing.Size(216,17);
			this.checkStatementShowReturnAddress.TabIndex = 56;
			this.checkStatementShowReturnAddress.Text = "Show return address";
			// 
			// checkBalancesDontSubtractIns
			// 
			this.checkBalancesDontSubtractIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBalancesDontSubtractIns.Location = new System.Drawing.Point(19,174);
			this.checkBalancesDontSubtractIns.Name = "checkBalancesDontSubtractIns";
			this.checkBalancesDontSubtractIns.Size = new System.Drawing.Size(288,17);
			this.checkBalancesDontSubtractIns.TabIndex = 55;
			this.checkBalancesDontSubtractIns.Text = "Balances don\'t subtract insurance estimate";
			// 
			// textStatementsCalcDueDate
			// 
			this.textStatementsCalcDueDate.Location = new System.Drawing.Point(19,138);
			this.textStatementsCalcDueDate.MaxVal = 255;
			this.textStatementsCalcDueDate.MinVal = 0;
			this.textStatementsCalcDueDate.Name = "textStatementsCalcDueDate";
			this.textStatementsCalcDueDate.Size = new System.Drawing.Size(60,20);
			this.textStatementsCalcDueDate.TabIndex = 54;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(80,137);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(235,31);
			this.label2.TabIndex = 53;
			this.label2.Text = "Days to calculate due date.  Usually 10 or 15.  Leave blank to show \"Due on Recei" +
    "pt\"";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioUsePatNum);
			this.groupBox2.Controls.Add(this.radioUseChartNumber);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(12,71);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200,61);
			this.groupBox2.TabIndex = 52;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Account Numbers Use";
			// 
			// radioUsePatNum
			// 
			this.radioUsePatNum.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUsePatNum.Location = new System.Drawing.Point(7,17);
			this.radioUsePatNum.Name = "radioUsePatNum";
			this.radioUsePatNum.Size = new System.Drawing.Size(144,19);
			this.radioUsePatNum.TabIndex = 1;
			this.radioUsePatNum.Text = "Patient Number";
			// 
			// radioUseChartNumber
			// 
			this.radioUseChartNumber.Checked = true;
			this.radioUseChartNumber.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUseChartNumber.Location = new System.Drawing.Point(7,36);
			this.radioUseChartNumber.Name = "radioUseChartNumber";
			this.radioUseChartNumber.Size = new System.Drawing.Size(144,19);
			this.radioUseChartNumber.TabIndex = 0;
			this.radioUseChartNumber.TabStop = true;
			this.radioUseChartNumber.Text = "Chart Number";
			// 
			// checkEclaimsSeparateTreatProv
			// 
			this.checkEclaimsSeparateTreatProv.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkEclaimsSeparateTreatProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEclaimsSeparateTreatProv.Location = new System.Drawing.Point(448,486);
			this.checkEclaimsSeparateTreatProv.Name = "checkEclaimsSeparateTreatProv";
			this.checkEclaimsSeparateTreatProv.Size = new System.Drawing.Size(394,18);
			this.checkEclaimsSeparateTreatProv.TabIndex = 53;
			this.checkEclaimsSeparateTreatProv.Text = "On e-claims, send treating provider info for each separate procedure";
			this.checkEclaimsSeparateTreatProv.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// checkRandomPrimaryKeys
			// 
			this.checkRandomPrimaryKeys.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRandomPrimaryKeys.Location = new System.Drawing.Point(39,483);
			this.checkRandomPrimaryKeys.Name = "checkRandomPrimaryKeys";
			this.checkRandomPrimaryKeys.Size = new System.Drawing.Size(403,21);
			this.checkRandomPrimaryKeys.TabIndex = 55;
			this.checkRandomPrimaryKeys.Text = "Use Random Primary Keys (BE VERY CAREFUL.  IRREVERSIBLE)";
			this.checkRandomPrimaryKeys.Click += new System.EventHandler(this.checkRandomPrimaryKeys_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(36,434);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(377,18);
			this.label3.TabIndex = 56;
			this.label3.Text = "Process Signal Interval in seconds.  Usually every 6 to 20 seconds. ";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkInsurancePlansShared
			// 
			this.checkInsurancePlansShared.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInsurancePlansShared.Location = new System.Drawing.Point(39,507);
			this.checkInsurancePlansShared.Name = "checkInsurancePlansShared";
			this.checkInsurancePlansShared.Size = new System.Drawing.Size(617,18);
			this.checkInsurancePlansShared.TabIndex = 58;
			this.checkInsurancePlansShared.Text = "Many patients have identical insurance plans.  Change behavior of program slightl" +
    "y to optimize for identical plans.";
			// 
			// checkMedicalEclaimsEnabled
			// 
			this.checkMedicalEclaimsEnabled.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkMedicalEclaimsEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkMedicalEclaimsEnabled.Location = new System.Drawing.Point(448,442);
			this.checkMedicalEclaimsEnabled.Name = "checkMedicalEclaimsEnabled";
			this.checkMedicalEclaimsEnabled.Size = new System.Drawing.Size(142,18);
			this.checkMedicalEclaimsEnabled.TabIndex = 60;
			this.checkMedicalEclaimsEnabled.Text = "Enable medical e-claims";
			this.checkMedicalEclaimsEnabled.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.radioShowIDnone);
			this.groupBox3.Controls.Add(this.radioShowIDpatNum);
			this.groupBox3.Controls.Add(this.radioShowIDchartNum);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(693,406);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(136,77);
			this.groupBox3.TabIndex = 62;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Show ID in title bar";
			// 
			// radioShowIDnone
			// 
			this.radioShowIDnone.Checked = true;
			this.radioShowIDnone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioShowIDnone.Location = new System.Drawing.Point(9,16);
			this.radioShowIDnone.Name = "radioShowIDnone";
			this.radioShowIDnone.Size = new System.Drawing.Size(121,19);
			this.radioShowIDnone.TabIndex = 2;
			this.radioShowIDnone.TabStop = true;
			this.radioShowIDnone.Text = "None";
			// 
			// radioShowIDpatNum
			// 
			this.radioShowIDpatNum.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioShowIDpatNum.Location = new System.Drawing.Point(9,35);
			this.radioShowIDpatNum.Name = "radioShowIDpatNum";
			this.radioShowIDpatNum.Size = new System.Drawing.Size(121,19);
			this.radioShowIDpatNum.TabIndex = 1;
			this.radioShowIDpatNum.Text = "Patient Number";
			// 
			// radioShowIDchartNum
			// 
			this.radioShowIDchartNum.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioShowIDchartNum.Location = new System.Drawing.Point(9,54);
			this.radioShowIDchartNum.Name = "radioShowIDchartNum";
			this.radioShowIDchartNum.Size = new System.Drawing.Size(121,19);
			this.radioShowIDchartNum.TabIndex = 0;
			this.radioShowIDchartNum.Text = "Chart Number";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(142,674);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(298,17);
			this.label4.TabIndex = 64;
			this.label4.Text = "Languages used by patients.";
			// 
			// checkSolidBlockouts
			// 
			this.checkSolidBlockouts.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSolidBlockouts.Location = new System.Drawing.Point(16,59);
			this.checkSolidBlockouts.Name = "checkSolidBlockouts";
			this.checkSolidBlockouts.Size = new System.Drawing.Size(346,18);
			this.checkSolidBlockouts.TabIndex = 66;
			this.checkSolidBlockouts.Text = "Use solid blockouts instead of outlines on the appointment book";
			this.checkSolidBlockouts.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.checkDeductibleBeforePercent);
			this.groupBox4.Controls.Add(this.checkStoreCCnumbers);
			this.groupBox4.Controls.Add(this.checkSolidBlockouts);
			this.groupBox4.Controls.Add(this.checkBoldBalance);
			this.groupBox4.Controls.Add(this.checkShowAccountNotes);
			this.groupBox4.Location = new System.Drawing.Point(32,272);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(381,128);
			this.groupBox4.TabIndex = 67;
			this.groupBox4.TabStop = false;
			// 
			// checkDeductibleBeforePercent
			// 
			this.checkDeductibleBeforePercent.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDeductibleBeforePercent.Location = new System.Drawing.Point(16,99);
			this.checkDeductibleBeforePercent.Name = "checkDeductibleBeforePercent";
			this.checkDeductibleBeforePercent.Size = new System.Drawing.Size(346,18);
			this.checkDeductibleBeforePercent.TabIndex = 68;
			this.checkDeductibleBeforePercent.Text = "Ins Plans default to apply deductible before percent.";
			this.checkDeductibleBeforePercent.UseVisualStyleBackColor = true;
			this.checkDeductibleBeforePercent.Click += new System.EventHandler(this.checkDeductibleBeforePercent_Click);
			// 
			// checkStoreCCnumbers
			// 
			this.checkStoreCCnumbers.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStoreCCnumbers.Location = new System.Drawing.Point(16,79);
			this.checkStoreCCnumbers.Name = "checkStoreCCnumbers";
			this.checkStoreCCnumbers.Size = new System.Drawing.Size(346,18);
			this.checkStoreCCnumbers.TabIndex = 67;
			this.checkStoreCCnumbers.Text = "Store credit card numbers (this is a security risk)";
			this.checkStoreCCnumbers.UseVisualStyleBackColor = true;
			// 
			// checkBoldBalance
			// 
			this.checkBoldBalance.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBoldBalance.Location = new System.Drawing.Point(16,39);
			this.checkBoldBalance.Name = "checkBoldBalance";
			this.checkBoldBalance.Size = new System.Drawing.Size(215,17);
			this.checkBoldBalance.TabIndex = 57;
			this.checkBoldBalance.Text = "Use bold balance view in account";
			this.toolTip1.SetToolTip(this.checkBoldBalance,"Generally used with \"Balances don\'t subtract insurance estimate\"\r\nchecked to the " +
        "upper right of this option in the \"Statements\" section.\r\nHowever, it will work w" +
        "ell either way.");
			// 
			// checkShowAccountNotes
			// 
			this.checkShowAccountNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowAccountNotes.Location = new System.Drawing.Point(16,20);
			this.checkShowAccountNotes.Name = "checkShowAccountNotes";
			this.checkShowAccountNotes.Size = new System.Drawing.Size(239,17);
			this.checkShowAccountNotes.TabIndex = 56;
			this.checkShowAccountNotes.Text = "Show item notes in account module grid ";
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.checkAppointmentBubblesDisabled);
			this.groupBox6.Controls.Add(this.checkApptBubbleDelay);
			this.groupBox6.Controls.Add(this.checkBrokenApptNote);
			this.groupBox6.Location = new System.Drawing.Point(436,272);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(393,128);
			this.groupBox6.TabIndex = 68;
			this.groupBox6.TabStop = false;
			// 
			// checkAppointmentBubblesDisabled
			// 
			this.checkAppointmentBubblesDisabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAppointmentBubblesDisabled.Location = new System.Drawing.Point(19,57);
			this.checkAppointmentBubblesDisabled.Name = "checkAppointmentBubblesDisabled";
			this.checkAppointmentBubblesDisabled.Size = new System.Drawing.Size(297,18);
			this.checkAppointmentBubblesDisabled.TabIndex = 70;
			this.checkAppointmentBubblesDisabled.Text = "Appointment bubble popup disabled";
			this.checkAppointmentBubblesDisabled.UseVisualStyleBackColor = true;
			// 
			// checkApptBubbleDelay
			// 
			this.checkApptBubbleDelay.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkApptBubbleDelay.Location = new System.Drawing.Point(19,38);
			this.checkApptBubbleDelay.Name = "checkApptBubbleDelay";
			this.checkApptBubbleDelay.Size = new System.Drawing.Size(297,18);
			this.checkApptBubbleDelay.TabIndex = 69;
			this.checkApptBubbleDelay.Text = "Appointment bubble popup delay";
			this.checkApptBubbleDelay.UseVisualStyleBackColor = true;
			// 
			// checkBrokenApptNote
			// 
			this.checkBrokenApptNote.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBrokenApptNote.Location = new System.Drawing.Point(19,19);
			this.checkBrokenApptNote.Name = "checkBrokenApptNote";
			this.checkBrokenApptNote.Size = new System.Drawing.Size(297,18);
			this.checkBrokenApptNote.TabIndex = 67;
			this.checkBrokenApptNote.Text = "Place broken appt. note in Comm. Log,  not account";
			this.checkBrokenApptNote.UseVisualStyleBackColor = true;
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.label9);
			this.groupBox7.Controls.Add(this.textBoxDocPath);
			this.groupBox7.Controls.Add(this.label8);
			this.groupBox7.Controls.Add(this.textBoxStationary);
			this.groupBox7.Controls.Add(this.textBoxLogo);
			this.groupBox7.Controls.Add(this.label6);
			this.groupBox7.Controls.Add(this.label5);
			this.groupBox7.Location = new System.Drawing.Point(39,537);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(476,124);
			this.groupBox7.TabIndex = 69;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Communications Files (located in document path A-Z folder)";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(450,46);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(20,18);
			this.label9.TabIndex = 63;
			this.label9.Text = "-->";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxDocPath
			// 
			this.textBoxDocPath.Location = new System.Drawing.Point(106,22);
			this.textBoxDocPath.Name = "textBoxDocPath";
			this.textBoxDocPath.ReadOnly = true;
			this.textBoxDocPath.Size = new System.Drawing.Size(338,20);
			this.textBoxDocPath.TabIndex = 62;
			this.toolTip1.SetToolTip(this.textBoxDocPath,"This shows you where you have your document path setup and where you should place" +
        " your files specified below.");
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(13,22);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(90,20);
			this.label8.TabIndex = 61;
			this.label8.Text = "A-Z path";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxStationary
			// 
			this.textBoxStationary.Location = new System.Drawing.Point(215,72);
			this.textBoxStationary.Name = "textBoxStationary";
			this.textBoxStationary.Size = new System.Drawing.Size(229,20);
			this.textBoxStationary.TabIndex = 60;
			this.toolTip1.SetToolTip(this.textBoxStationary,resources.GetString("textBoxStationary.ToolTip"));
			// 
			// textBoxLogo
			// 
			this.textBoxLogo.Location = new System.Drawing.Point(215,47);
			this.textBoxLogo.Name = "textBoxLogo";
			this.textBoxLogo.Size = new System.Drawing.Size(229,20);
			this.textBoxLogo.TabIndex = 59;
			this.toolTip1.SetToolTip(this.textBoxLogo,resources.GetString("textBoxLogo.ToolTip"));
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(30,74);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(181,15);
			this.label6.TabIndex = 58;
			this.label6.Text = "Stationery Document (*.doc)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12,47);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(199,18);
			this.label5.TabIndex = 57;
			this.label5.Text = "Letter Background (*.jpg)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(117,458);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(286,20);
			this.label7.TabIndex = 70;
			this.label7.Text = "Leave blank to disable autorefresh.";
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 0;
			this.toolTip1.AutoPopDelay = 600000;
			this.toolTip1.InitialDelay = 0;
			this.toolTip1.ReshowDelay = 0;
			this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::OpenDental.Properties.Resources.stationary_sample1;
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pictureBox1.Location = new System.Drawing.Point(521,539);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(101,124);
			this.pictureBox1.TabIndex = 71;
			this.pictureBox1.TabStop = false;
			// 
			// butLanguages
			// 
			this.butLanguages.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLanguages.Autosize = true;
			this.butLanguages.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLanguages.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLanguages.CornerRadius = 4F;
			this.butLanguages.Location = new System.Drawing.Point(48,667);
			this.butLanguages.Name = "butLanguages";
			this.butLanguages.Size = new System.Drawing.Size(88,26);
			this.butLanguages.TabIndex = 63;
			this.butLanguages.Text = "Edit Languages";
			this.butLanguages.Click += new System.EventHandler(this.butLanguages_Click);
			// 
			// textSigInterval
			// 
			this.textSigInterval.Location = new System.Drawing.Point(37,455);
			this.textSigInterval.MaxVal = 1000000;
			this.textSigInterval.MinVal = 1;
			this.textSigInterval.Name = "textSigInterval";
			this.textSigInterval.Size = new System.Drawing.Size(74,20);
			this.textSigInterval.TabIndex = 57;
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
			this.butCancel.Location = new System.Drawing.Point(742,653);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 8;
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
			this.butOK.Location = new System.Drawing.Point(742,615);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 7;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormMisc
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(863,699);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.groupBox7);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butLanguages);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.checkMedicalEclaimsEnabled);
			this.Controls.Add(this.checkInsurancePlansShared);
			this.Controls.Add(this.textSigInterval);
			this.Controls.Add(this.textMainWindowTitle);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkRandomPrimaryKeys);
			this.Controls.Add(this.checkEclaimsSeparateTreatProv);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.checkITooth);
			this.Controls.Add(this.label14);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMisc";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Miscellaneous Setup";
			this.Load += new System.EventHandler(this.FormMisc_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormMisc_Load(object sender, System.EventArgs e) {
			textTreatNote.Text=PrefB.GetString("TreatmentPlanNote");
			checkTreatPlanShowGraphics.Checked=PrefB.GetBool("TreatPlanShowGraphics");
			checkTreatPlanShowCompleted.Checked=PrefB.GetBool("TreatPlanShowCompleted");
			checkTreatPlanShowIns.Checked=PrefB.GetBool("TreatPlanShowIns");
			checkStatementShowReturnAddress.Checked=PrefB.GetBool("StatementShowReturnAddress");
			checkShowCC.Checked=PrefB.GetBool("StatementShowCreditCard");
			if(PrefB.GetBool("StatementAccountsUseChartNumber")){
				radioUseChartNumber.Checked=true;
			}
			else{
				radioUsePatNum.Checked=true;
			}
			if(PrefB.GetInt("StatementsCalcDueDate")!=-1){
				textStatementsCalcDueDate.Text=PrefB.GetInt("StatementsCalcDueDate").ToString();
			}
			checkBalancesDontSubtractIns.Checked=PrefB.GetBool("BalancesDontSubtractIns");
			checkAgingMonthly.Checked=PrefB.GetBool("AgingCalculatedMonthlyInsteadOfDaily");
			if(PrefB.GetInt("ProcessSigsIntervalInSecs")==0){
				textSigInterval.Text="";
			}
			else{
				textSigInterval.Text=PrefB.GetInt("ProcessSigsIntervalInSecs").ToString();
			}
			checkRandomPrimaryKeys.Checked=PrefB.GetBool("RandomPrimaryKeys");
			if(checkRandomPrimaryKeys.Checked){
				//not allowed to uncheck it
				checkRandomPrimaryKeys.Enabled=false;
			}
			textMainWindowTitle.Text=PrefB.GetString("MainWindowTitle");
			if(PrefB.GetInt("ShowIDinTitleBar")==0){
				radioShowIDnone.Checked=true;
			}
			else if(PrefB.GetInt("ShowIDinTitleBar")==1){
				radioShowIDpatNum.Checked=true;
			}
			else if(PrefB.GetInt("ShowIDinTitleBar")==2) {
				radioShowIDchartNum.Checked=true;
			}
			checkEclaimsSeparateTreatProv.Checked=PrefB.GetBool("EclaimsSeparateTreatProv");
			checkMedicalEclaimsEnabled.Checked=PrefB.GetBool("MedicalEclaimsEnabled");
			checkITooth.Checked=PrefB.GetBool("UseInternationalToothNumbers");
			checkInsurancePlansShared.Checked=PrefB.GetBool("InsurancePlansShared");
			checkSolidBlockouts.Checked=PrefB.GetBool("SolidBlockouts");
			checkStoreCCnumbers.Checked=PrefB.GetBool("StoreCCnumbers");
			checkDeductibleBeforePercent.Checked=PrefB.GetBool("DeductibleBeforePercentAsDefault");
			checkBoldBalance.Checked=PrefB.GetBool("BoldFamilyAccountBalanceView");
			checkShowAccountNotes.Checked=PrefB.GetBool("ShowNotesInAccount");
			checkSimpleStatement.Checked=PrefB.GetBool("PrintSimpleStatements");
			checkBrokenApptNote.Checked=PrefB.GetBool("BrokenApptCommLogNotAdjustment");
			string AZpath=FormPath.GetPreferredImagePath();
			if(AZpath!=null){
				textBoxDocPath.Text =AZpath;
			}
			textBoxLogo.Text = PrefB.GetString("StationaryImage");
			textBoxStationary.Text = PrefB.GetString("StationaryDocument");
			checkApptBubbleDelay.Checked = PrefB.GetBool("ApptBubbleDelay");
			checkAppointmentBubblesDisabled.Checked=PrefB.GetBool("AppointmentBubblesDisabled");
		}

		private void checkRandomPrimaryKeys_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show("Are you absolutely sure you want to enable random primary keys?\r\n"
				+"Advantages:\r\n"
				+"Multiple servers can stay synchronized using merge replication.\r\n"
				+"Realtime connection between servers not required.\r\n"
				+"Data can be entered on all servers and synchronized later.\r\n"
				+"Disadvantages:\r\n"
				+"Slightly slower.\r\n"
				+"Difficult to set up.\r\n"
				+"Primary keys much longer, so not as user friendly.","",MessageBoxButtons.OKCancel)==DialogResult.Cancel)
			{
				checkRandomPrimaryKeys.Checked=false;
			}
		}

		private void butLanguages_Click(object sender,EventArgs e) {
			FormLanguagesUsed FormL=new FormLanguagesUsed();
			FormL.ShowDialog();
			if(FormL.DialogResult==DialogResult.OK){
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
		}

		private void checkDeductibleBeforePercent_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"Change all insurance plans right now?")){
				return;
			}
			string command="UPDATE insplan SET DedBeforePerc=";
			if(checkDeductibleBeforePercent.Checked){
				command+="1";
			}
			else{
				command+="0";
			}
			int result=General.NonQ(command);
			MessageBox.Show(Lan.g(this,"Plans changed: ")+result.ToString());
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textStatementsCalcDueDate.errorProvider1.GetError(textStatementsCalcDueDate)!="")
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			bool changed=false;
			if( Prefs.UpdateString("TreatmentPlanNote",textTreatNote.Text)
				| Prefs.UpdateBool("TreatPlanShowGraphics",checkTreatPlanShowGraphics.Checked)
				| Prefs.UpdateBool("TreatPlanShowCompleted",checkTreatPlanShowCompleted.Checked)
				| Prefs.UpdateBool("TreatPlanShowIns",checkTreatPlanShowIns.Checked)
				| Prefs.UpdateBool("StatementShowReturnAddress",checkStatementShowReturnAddress.Checked)
				| Prefs.UpdateBool("StatementShowCreditCard",checkShowCC.Checked)
				| Prefs.UpdateBool("StatementAccountsUseChartNumber",radioUseChartNumber.Checked)
				| Prefs.UpdateBool("BalancesDontSubtractIns",checkBalancesDontSubtractIns.Checked)
				| Prefs.UpdateBool("AgingCalculatedMonthlyInsteadOfDaily",checkAgingMonthly.Checked)
				| Prefs.UpdateBool("RandomPrimaryKeys",checkRandomPrimaryKeys.Checked)
				| Prefs.UpdateString("MainWindowTitle",textMainWindowTitle.Text)
				| Prefs.UpdateBool("EclaimsSeparateTreatProv",checkEclaimsSeparateTreatProv.Checked)
				| Prefs.UpdateBool("MedicalEclaimsEnabled",checkMedicalEclaimsEnabled.Checked)
				| Prefs.UpdateBool("UseInternationalToothNumbers",checkITooth.Checked)
				| Prefs.UpdateBool("InsurancePlansShared",checkInsurancePlansShared.Checked)
				| Prefs.UpdateBool("SolidBlockouts", checkSolidBlockouts.Checked)
				| Prefs.UpdateBool("StoreCCnumbers", checkStoreCCnumbers.Checked)
				| Prefs.UpdateBool("DeductibleBeforePercentAsDefault",checkDeductibleBeforePercent.Checked)
				| Prefs.UpdateBool("BoldFamilyAccountBalanceView", checkBoldBalance.Checked)
				| Prefs.UpdateBool("ShowNotesInAccount", checkShowAccountNotes.Checked)
				| Prefs.UpdateBool("PrintSimpleStatements", checkSimpleStatement.Checked)
				| Prefs.UpdateBool("BrokenApptCommLogNotAdjustment", checkBrokenApptNote.Checked)
				| Prefs.UpdateString("StationaryImage", textBoxLogo.Text)
				| Prefs.UpdateString("StationaryDocument", textBoxStationary.Text)
				| Prefs.UpdateBool("ApptBubbleDelay", checkApptBubbleDelay.Checked)
				| Prefs.UpdateBool("AppointmentBubblesDisabled", checkAppointmentBubblesDisabled.Checked))
			{
				changed=true;
			}
			if(textStatementsCalcDueDate.Text==""){
				if(Prefs.UpdateInt("StatementsCalcDueDate",-1)){
					changed=true;
				}
			}
			else{
				if(Prefs.UpdateInt("StatementsCalcDueDate",PIn.PInt(textStatementsCalcDueDate.Text))){
					changed=true;
				}
			}
			if(textSigInterval.Text==""){
				if(Prefs.UpdateInt("ProcessSigsIntervalInSecs",0)){
					changed=true;
				}
			}
			else{
				if(Prefs.UpdateInt("ProcessSigsIntervalInSecs",PIn.PInt(textSigInterval.Text))){
					changed=true;
				}
			}
			//ShowIDinTitleBar
			if(radioShowIDnone.Checked){
				if(Prefs.UpdateInt("ShowIDinTitleBar",0)) {
					changed=true;
				}
			}
			else if(radioShowIDpatNum.Checked) {
				if(Prefs.UpdateInt("ShowIDinTitleBar",1)) {
					changed=true;
				}
			}
			else if(radioShowIDchartNum.Checked) {
				if(Prefs.UpdateInt("ShowIDinTitleBar",2)) {
					changed=true;
				}
			}
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

	

	}
}




