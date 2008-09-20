using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using System.IO;

namespace OpenDental{
///<summary></summary>
	public class FormModuleSetup:System.Windows.Forms.Form {
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private IContainer components;
		private System.Windows.Forms.TextBox textTreatNote;
		private System.Windows.Forms.CheckBox checkShowCC;
		private System.Windows.Forms.CheckBox checkTreatPlanShowGraphics;
		private System.Windows.Forms.CheckBox checkTreatPlanShowCompleted;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidNumber textStatementsCalcDueDate;
		private System.Windows.Forms.CheckBox checkEclaimsSeparateTreatProv;
		private System.Windows.Forms.CheckBox checkBalancesDontSubtractIns;
		private System.Windows.Forms.CheckBox checkInsurancePlansShared;
		private CheckBox checkMedicalEclaimsEnabled;
		private CheckBox checkStatementShowReturnAddress;
    private CheckBox checkSolidBlockouts;
		private CheckBox checkAgingMonthly;
		private GroupBox groupBox4;
		private CheckBox checkBrokenApptNote;
		private ToolTip toolTip1;
		private CheckBox checkApptBubbleDelay;
		private CheckBox checkStoreCCnumbers;
		private CheckBox checkAppointmentBubblesDisabled;
		private CheckBox checkDeductibleBeforePercent;
		private ComboBox comboUseChartNum;
		private Label label10;
		private Label label7;
		private ComboBox comboBrokenApptAdjType;
		private Label label12;
		private ComboBox comboFinanceChargeAdjType;
		private System.Windows.Forms.Label label1;
		private CheckBox checkApptExclamation;
		private CheckBox checkProviderIncomeShows;
		private ComputerPref computerPref;
		private TextBox textClaimAttachPath;
		private GroupBox groupBox3;
		private CheckBox checkAutoClearEntryStatus;
		private CheckBox checkShowFamilyCommByDefault;
		private Label label20;
		private CheckBox checkPPOpercentage;
		private Label label18;
		private ValidNum textPayPlansBillInAdvanceDays;
		private CheckBox checkStatementSummaryShowInsInfo;
		private ComboBox comboToothNomenclature;
		private Label label11;
		private CheckBox checkClaimFormTreatDentSaysSigOnFile;
		private CheckBox checkAllowSettingProcsComplete;
		private Label label4;
		private GroupBox groupBox2;
		private Label label6;
		private ComboBox comboTimeDismissed;
		private Label label5;
		private ComboBox comboTimeSeated;
		private Label label3;
		private ComboBox comboTimeArrived;
		private CheckBox checkApptRefreshEveryMinute;
		private CheckBox checkChartQuickAddHideAmalgam;
		private ComboBox comboBillingChargeAdjType;
		private List<Def> posAdjTypes;

		///<summary></summary>
		public FormModuleSetup() {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormModuleSetup));
			this.textTreatNote = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkShowCC = new System.Windows.Forms.CheckBox();
			this.checkTreatPlanShowGraphics = new System.Windows.Forms.CheckBox();
			this.checkTreatPlanShowCompleted = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.checkClaimFormTreatDentSaysSigOnFile = new System.Windows.Forms.CheckBox();
			this.checkStatementSummaryShowInsInfo = new System.Windows.Forms.CheckBox();
			this.textPayPlansBillInAdvanceDays = new OpenDental.ValidNum();
			this.label18 = new System.Windows.Forms.Label();
			this.textClaimAttachPath = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.checkShowFamilyCommByDefault = new System.Windows.Forms.CheckBox();
			this.checkProviderIncomeShows = new System.Windows.Forms.CheckBox();
			this.checkEclaimsSeparateTreatProv = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.comboFinanceChargeAdjType = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.checkStoreCCnumbers = new System.Windows.Forms.CheckBox();
			this.comboUseChartNum = new System.Windows.Forms.ComboBox();
			this.checkAgingMonthly = new System.Windows.Forms.CheckBox();
			this.checkStatementShowReturnAddress = new System.Windows.Forms.CheckBox();
			this.checkBalancesDontSubtractIns = new System.Windows.Forms.CheckBox();
			this.textStatementsCalcDueDate = new OpenDental.ValidNumber();
			this.label2 = new System.Windows.Forms.Label();
			this.checkInsurancePlansShared = new System.Windows.Forms.CheckBox();
			this.checkMedicalEclaimsEnabled = new System.Windows.Forms.CheckBox();
			this.checkSolidBlockouts = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.checkApptRefreshEveryMinute = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.comboTimeDismissed = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.comboTimeSeated = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboTimeArrived = new System.Windows.Forms.ComboBox();
			this.checkApptExclamation = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.checkBrokenApptNote = new System.Windows.Forms.CheckBox();
			this.comboBrokenApptAdjType = new System.Windows.Forms.ComboBox();
			this.checkApptBubbleDelay = new System.Windows.Forms.CheckBox();
			this.checkAppointmentBubblesDisabled = new System.Windows.Forms.CheckBox();
			this.checkDeductibleBeforePercent = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.checkChartQuickAddHideAmalgam = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.checkAllowSettingProcsComplete = new System.Windows.Forms.CheckBox();
			this.comboToothNomenclature = new System.Windows.Forms.ComboBox();
			this.checkAutoClearEntryStatus = new System.Windows.Forms.CheckBox();
			this.checkPPOpercentage = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.comboBillingChargeAdjType = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// textTreatNote
			// 
			this.textTreatNote.AcceptsReturn = true;
			this.textTreatNote.Location = new System.Drawing.Point(45,34);
			this.textTreatNote.Multiline = true;
			this.textTreatNote.Name = "textTreatNote";
			this.textTreatNote.Size = new System.Drawing.Size(371,53);
			this.textTreatNote.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(42,16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(163,15);
			this.label1.TabIndex = 35;
			this.label1.Text = "Default Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkShowCC
			// 
			this.checkShowCC.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowCC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowCC.Location = new System.Drawing.Point(48,32);
			this.checkShowCC.Name = "checkShowCC";
			this.checkShowCC.Size = new System.Drawing.Size(368,17);
			this.checkShowCC.TabIndex = 36;
			this.checkShowCC.Text = "Show credit card info on statements";
			this.checkShowCC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkTreatPlanShowGraphics
			// 
			this.checkTreatPlanShowGraphics.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTreatPlanShowGraphics.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanShowGraphics.Location = new System.Drawing.Point(57,89);
			this.checkTreatPlanShowGraphics.Name = "checkTreatPlanShowGraphics";
			this.checkTreatPlanShowGraphics.Size = new System.Drawing.Size(359,17);
			this.checkTreatPlanShowGraphics.TabIndex = 46;
			this.checkTreatPlanShowGraphics.Text = "Show Graphical Tooth Chart";
			this.checkTreatPlanShowGraphics.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkTreatPlanShowCompleted
			// 
			this.checkTreatPlanShowCompleted.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTreatPlanShowCompleted.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanShowCompleted.Location = new System.Drawing.Point(57,106);
			this.checkTreatPlanShowCompleted.Name = "checkTreatPlanShowCompleted";
			this.checkTreatPlanShowCompleted.Size = new System.Drawing.Size(359,17);
			this.checkTreatPlanShowCompleted.TabIndex = 47;
			this.checkTreatPlanShowCompleted.Text = "Show Completed Work on Graphical Tooth Chart";
			this.checkTreatPlanShowCompleted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textTreatNote);
			this.groupBox1.Controls.Add(this.checkTreatPlanShowGraphics);
			this.groupBox1.Controls.Add(this.checkTreatPlanShowCompleted);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(12,351);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(422,132);
			this.groupBox1.TabIndex = 48;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Treatment Plan module";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.comboBillingChargeAdjType);
			this.groupBox5.Controls.Add(this.label4);
			this.groupBox5.Controls.Add(this.checkClaimFormTreatDentSaysSigOnFile);
			this.groupBox5.Controls.Add(this.checkStatementSummaryShowInsInfo);
			this.groupBox5.Controls.Add(this.textPayPlansBillInAdvanceDays);
			this.groupBox5.Controls.Add(this.label18);
			this.groupBox5.Controls.Add(this.textClaimAttachPath);
			this.groupBox5.Controls.Add(this.label20);
			this.groupBox5.Controls.Add(this.checkShowFamilyCommByDefault);
			this.groupBox5.Controls.Add(this.checkProviderIncomeShows);
			this.groupBox5.Controls.Add(this.checkEclaimsSeparateTreatProv);
			this.groupBox5.Controls.Add(this.label12);
			this.groupBox5.Controls.Add(this.comboFinanceChargeAdjType);
			this.groupBox5.Controls.Add(this.label10);
			this.groupBox5.Controls.Add(this.checkStoreCCnumbers);
			this.groupBox5.Controls.Add(this.comboUseChartNum);
			this.groupBox5.Controls.Add(this.checkAgingMonthly);
			this.groupBox5.Controls.Add(this.checkStatementShowReturnAddress);
			this.groupBox5.Controls.Add(this.checkBalancesDontSubtractIns);
			this.groupBox5.Controls.Add(this.textStatementsCalcDueDate);
			this.groupBox5.Controls.Add(this.label2);
			this.groupBox5.Controls.Add(this.checkShowCC);
			this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox5.Location = new System.Drawing.Point(454,9);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(422,366);
			this.groupBox5.TabIndex = 52;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Account module";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(32,203);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(221,15);
			this.label4.TabIndex = 198;
			this.label4.Text = "Finance charge adj type";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkClaimFormTreatDentSaysSigOnFile
			// 
			this.checkClaimFormTreatDentSaysSigOnFile.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkClaimFormTreatDentSaysSigOnFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimFormTreatDentSaysSigOnFile.Location = new System.Drawing.Point(35,277);
			this.checkClaimFormTreatDentSaysSigOnFile.Name = "checkClaimFormTreatDentSaysSigOnFile";
			this.checkClaimFormTreatDentSaysSigOnFile.Size = new System.Drawing.Size(381,17);
			this.checkClaimFormTreatDentSaysSigOnFile.TabIndex = 197;
			this.checkClaimFormTreatDentSaysSigOnFile.Text = "Claim Form treating dentist shows Signature On File rather than name";
			this.checkClaimFormTreatDentSaysSigOnFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkStatementSummaryShowInsInfo
			// 
			this.checkStatementSummaryShowInsInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStatementSummaryShowInsInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStatementSummaryShowInsInfo.Location = new System.Drawing.Point(48,294);
			this.checkStatementSummaryShowInsInfo.Name = "checkStatementSummaryShowInsInfo";
			this.checkStatementSummaryShowInsInfo.Size = new System.Drawing.Size(368,17);
			this.checkStatementSummaryShowInsInfo.TabIndex = 195;
			this.checkStatementSummaryShowInsInfo.Text = "Show insurance pending and related balance info on statement summary";
			this.checkStatementSummaryShowInsInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayPlansBillInAdvanceDays
			// 
			this.textPayPlansBillInAdvanceDays.Location = new System.Drawing.Point(357,116);
			this.textPayPlansBillInAdvanceDays.MaxVal = 255;
			this.textPayPlansBillInAdvanceDays.MinVal = 0;
			this.textPayPlansBillInAdvanceDays.Name = "textPayPlansBillInAdvanceDays";
			this.textPayPlansBillInAdvanceDays.Size = new System.Drawing.Size(60,20);
			this.textPayPlansBillInAdvanceDays.TabIndex = 193;
			this.textPayPlansBillInAdvanceDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(37,112);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(318,27);
			this.label18.TabIndex = 76;
			this.label18.Text = "Days in advance to bill payment plan amounts due.\r\nUsually 10 or 15.";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textClaimAttachPath
			// 
			this.textClaimAttachPath.Location = new System.Drawing.Point(219,318);
			this.textClaimAttachPath.Name = "textClaimAttachPath";
			this.textClaimAttachPath.Size = new System.Drawing.Size(197,20);
			this.textClaimAttachPath.TabIndex = 189;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(30,321);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(188,13);
			this.label20.TabIndex = 190;
			this.label20.Text = "Claim Attachment Export Path";
			this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkShowFamilyCommByDefault
			// 
			this.checkShowFamilyCommByDefault.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowFamilyCommByDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowFamilyCommByDefault.Location = new System.Drawing.Point(35,260);
			this.checkShowFamilyCommByDefault.Name = "checkShowFamilyCommByDefault";
			this.checkShowFamilyCommByDefault.Size = new System.Drawing.Size(381,17);
			this.checkShowFamilyCommByDefault.TabIndex = 75;
			this.checkShowFamilyCommByDefault.Text = "Show Family Comm Entries By Default";
			this.checkShowFamilyCommByDefault.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.checkShowFamilyCommByDefault,"Generally used with \"Balances don\'t subtract insurance estimate\"\r\nchecked to the " +
        "upper right of this option in the \"Statements\" section.\r\nHowever, it will work w" +
        "ell either way.");
			// 
			// checkProviderIncomeShows
			// 
			this.checkProviderIncomeShows.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkProviderIncomeShows.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkProviderIncomeShows.Location = new System.Drawing.Point(35,243);
			this.checkProviderIncomeShows.Name = "checkProviderIncomeShows";
			this.checkProviderIncomeShows.Size = new System.Drawing.Size(381,17);
			this.checkProviderIncomeShows.TabIndex = 74;
			this.checkProviderIncomeShows.Text = "Show provider income transfer window after entering insurance payment";
			this.checkProviderIncomeShows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.checkProviderIncomeShows,"Generally used with \"Balances don\'t subtract insurance estimate\"\r\nchecked to the " +
        "upper right of this option in the \"Statements\" section.\r\nHowever, it will work w" +
        "ell either way.");
			// 
			// checkEclaimsSeparateTreatProv
			// 
			this.checkEclaimsSeparateTreatProv.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkEclaimsSeparateTreatProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEclaimsSeparateTreatProv.Location = new System.Drawing.Point(70,340);
			this.checkEclaimsSeparateTreatProv.Name = "checkEclaimsSeparateTreatProv";
			this.checkEclaimsSeparateTreatProv.Size = new System.Drawing.Size(346,17);
			this.checkEclaimsSeparateTreatProv.TabIndex = 53;
			this.checkEclaimsSeparateTreatProv.Text = "On e-claims, send treating provider info for each separate procedure";
			this.checkEclaimsSeparateTreatProv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(32,225);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(221,15);
			this.label12.TabIndex = 73;
			this.label12.Text = "Billing charge adj type";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboFinanceChargeAdjType
			// 
			this.comboFinanceChargeAdjType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFinanceChargeAdjType.FormattingEnabled = true;
			this.comboFinanceChargeAdjType.Location = new System.Drawing.Point(254,198);
			this.comboFinanceChargeAdjType.MaxDropDownItems = 30;
			this.comboFinanceChargeAdjType.Name = "comboFinanceChargeAdjType";
			this.comboFinanceChargeAdjType.Size = new System.Drawing.Size(163,21);
			this.comboFinanceChargeAdjType.TabIndex = 72;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(90,56);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(195,15);
			this.label10.TabIndex = 69;
			this.label10.Text = "Account Numbers use";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkStoreCCnumbers
			// 
			this.checkStoreCCnumbers.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStoreCCnumbers.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStoreCCnumbers.Location = new System.Drawing.Point(48,178);
			this.checkStoreCCnumbers.Name = "checkStoreCCnumbers";
			this.checkStoreCCnumbers.Size = new System.Drawing.Size(368,17);
			this.checkStoreCCnumbers.TabIndex = 67;
			this.checkStoreCCnumbers.Text = "Allow storing credit card numbers (this is a security risk)";
			this.checkStoreCCnumbers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStoreCCnumbers.UseVisualStyleBackColor = true;
			// 
			// comboUseChartNum
			// 
			this.comboUseChartNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboUseChartNum.FormattingEnabled = true;
			this.comboUseChartNum.Location = new System.Drawing.Point(287,53);
			this.comboUseChartNum.Name = "comboUseChartNum";
			this.comboUseChartNum.Size = new System.Drawing.Size(130,21);
			this.comboUseChartNum.TabIndex = 68;
			// 
			// checkAgingMonthly
			// 
			this.checkAgingMonthly.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAgingMonthly.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAgingMonthly.Location = new System.Drawing.Point(48,161);
			this.checkAgingMonthly.Name = "checkAgingMonthly";
			this.checkAgingMonthly.Size = new System.Drawing.Size(368,17);
			this.checkAgingMonthly.TabIndex = 57;
			this.checkAgingMonthly.Text = "Aging calculated monthly instead of daily";
			this.checkAgingMonthly.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkStatementShowReturnAddress
			// 
			this.checkStatementShowReturnAddress.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStatementShowReturnAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStatementShowReturnAddress.Location = new System.Drawing.Point(48,15);
			this.checkStatementShowReturnAddress.Name = "checkStatementShowReturnAddress";
			this.checkStatementShowReturnAddress.Size = new System.Drawing.Size(368,17);
			this.checkStatementShowReturnAddress.TabIndex = 56;
			this.checkStatementShowReturnAddress.Text = "Show return address on statements";
			this.checkStatementShowReturnAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkBalancesDontSubtractIns
			// 
			this.checkBalancesDontSubtractIns.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBalancesDontSubtractIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBalancesDontSubtractIns.Location = new System.Drawing.Point(48,144);
			this.checkBalancesDontSubtractIns.Name = "checkBalancesDontSubtractIns";
			this.checkBalancesDontSubtractIns.Size = new System.Drawing.Size(368,17);
			this.checkBalancesDontSubtractIns.TabIndex = 55;
			this.checkBalancesDontSubtractIns.Text = "Balances don\'t subtract insurance estimate";
			this.checkBalancesDontSubtractIns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textStatementsCalcDueDate
			// 
			this.textStatementsCalcDueDate.Location = new System.Drawing.Point(357,84);
			this.textStatementsCalcDueDate.MaxVal = 255;
			this.textStatementsCalcDueDate.MinVal = 0;
			this.textStatementsCalcDueDate.Name = "textStatementsCalcDueDate";
			this.textStatementsCalcDueDate.Size = new System.Drawing.Size(60,20);
			this.textStatementsCalcDueDate.TabIndex = 54;
			this.textStatementsCalcDueDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(36,80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(318,27);
			this.label2.TabIndex = 53;
			this.label2.Text = "Days to calculate due date.  Usually 10 or 15.  Leave blank to show \"Due on Recei" +
    "pt\"";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkInsurancePlansShared
			// 
			this.checkInsurancePlansShared.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkInsurancePlansShared.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInsurancePlansShared.Location = new System.Drawing.Point(6,56);
			this.checkInsurancePlansShared.Name = "checkInsurancePlansShared";
			this.checkInsurancePlansShared.Size = new System.Drawing.Size(410,27);
			this.checkInsurancePlansShared.TabIndex = 58;
			this.checkInsurancePlansShared.Text = "Many patients have identical insurance plans.  Change behavior of program slightl" +
    "y to optimize for identical plans.";
			this.checkInsurancePlansShared.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkMedicalEclaimsEnabled
			// 
			this.checkMedicalEclaimsEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMedicalEclaimsEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkMedicalEclaimsEnabled.Location = new System.Drawing.Point(70,19);
			this.checkMedicalEclaimsEnabled.Name = "checkMedicalEclaimsEnabled";
			this.checkMedicalEclaimsEnabled.Size = new System.Drawing.Size(346,17);
			this.checkMedicalEclaimsEnabled.TabIndex = 60;
			this.checkMedicalEclaimsEnabled.Text = "Enable medical e-claims";
			this.checkMedicalEclaimsEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkSolidBlockouts
			// 
			this.checkSolidBlockouts.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkSolidBlockouts.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSolidBlockouts.Location = new System.Drawing.Point(54,48);
			this.checkSolidBlockouts.Name = "checkSolidBlockouts";
			this.checkSolidBlockouts.Size = new System.Drawing.Size(362,17);
			this.checkSolidBlockouts.TabIndex = 66;
			this.checkSolidBlockouts.Text = "Use solid blockouts instead of outlines on the appointment book";
			this.checkSolidBlockouts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkSolidBlockouts.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.checkApptRefreshEveryMinute);
			this.groupBox4.Controls.Add(this.label6);
			this.groupBox4.Controls.Add(this.comboTimeDismissed);
			this.groupBox4.Controls.Add(this.label5);
			this.groupBox4.Controls.Add(this.comboTimeSeated);
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Controls.Add(this.comboTimeArrived);
			this.groupBox4.Controls.Add(this.checkApptExclamation);
			this.groupBox4.Controls.Add(this.label7);
			this.groupBox4.Controls.Add(this.checkBrokenApptNote);
			this.groupBox4.Controls.Add(this.comboBrokenApptAdjType);
			this.groupBox4.Controls.Add(this.checkApptBubbleDelay);
			this.groupBox4.Controls.Add(this.checkAppointmentBubblesDisabled);
			this.groupBox4.Controls.Add(this.checkSolidBlockouts);
			this.groupBox4.Location = new System.Drawing.Point(12,9);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(422,219);
			this.groupBox4.TabIndex = 67;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Appointment module";
			// 
			// checkApptRefreshEveryMinute
			// 
			this.checkApptRefreshEveryMinute.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptRefreshEveryMinute.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkApptRefreshEveryMinute.Location = new System.Drawing.Point(10,195);
			this.checkApptRefreshEveryMinute.Name = "checkApptRefreshEveryMinute";
			this.checkApptRefreshEveryMinute.Size = new System.Drawing.Size(406,17);
			this.checkApptRefreshEveryMinute.TabIndex = 198;
			this.checkApptRefreshEveryMinute.Text = "Refresh every 60 seconds.  Keeps waiting room times refreshed.";
			this.checkApptRefreshEveryMinute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6,174);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(247,15);
			this.label6.TabIndex = 78;
			this.label6.Text = "Time Dismissed trigger";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboTimeDismissed
			// 
			this.comboTimeDismissed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboTimeDismissed.FormattingEnabled = true;
			this.comboTimeDismissed.Location = new System.Drawing.Point(254,170);
			this.comboTimeDismissed.MaxDropDownItems = 30;
			this.comboTimeDismissed.Name = "comboTimeDismissed";
			this.comboTimeDismissed.Size = new System.Drawing.Size(163,21);
			this.comboTimeDismissed.TabIndex = 77;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6,152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(247,15);
			this.label5.TabIndex = 76;
			this.label5.Text = "Time Seated (in op) trigger";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboTimeSeated
			// 
			this.comboTimeSeated.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboTimeSeated.FormattingEnabled = true;
			this.comboTimeSeated.Location = new System.Drawing.Point(254,148);
			this.comboTimeSeated.MaxDropDownItems = 30;
			this.comboTimeSeated.Name = "comboTimeSeated";
			this.comboTimeSeated.Size = new System.Drawing.Size(163,21);
			this.comboTimeSeated.TabIndex = 75;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6,130);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(247,15);
			this.label3.TabIndex = 74;
			this.label3.Text = "Time Arrived trigger";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboTimeArrived
			// 
			this.comboTimeArrived.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboTimeArrived.FormattingEnabled = true;
			this.comboTimeArrived.Location = new System.Drawing.Point(254,126);
			this.comboTimeArrived.MaxDropDownItems = 30;
			this.comboTimeArrived.Name = "comboTimeArrived";
			this.comboTimeArrived.Size = new System.Drawing.Size(163,21);
			this.comboTimeArrived.TabIndex = 73;
			// 
			// checkApptExclamation
			// 
			this.checkApptExclamation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptExclamation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkApptExclamation.Location = new System.Drawing.Point(31,106);
			this.checkApptExclamation.Name = "checkApptExclamation";
			this.checkApptExclamation.Size = new System.Drawing.Size(385,17);
			this.checkApptExclamation.TabIndex = 72;
			this.checkApptExclamation.Text = "Show ! at upper right of appts for ins not sent (might cause slowdown)";
			this.checkApptExclamation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptExclamation.UseVisualStyleBackColor = true;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(32,88);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(221,15);
			this.label7.TabIndex = 71;
			this.label7.Text = "Broken appt default adj type";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkBrokenApptNote
			// 
			this.checkBrokenApptNote.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBrokenApptNote.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBrokenApptNote.Location = new System.Drawing.Point(54,65);
			this.checkBrokenApptNote.Name = "checkBrokenApptNote";
			this.checkBrokenApptNote.Size = new System.Drawing.Size(362,17);
			this.checkBrokenApptNote.TabIndex = 67;
			this.checkBrokenApptNote.Text = "Put broken appt note in Commlog instead of Adj (not recommended)";
			this.checkBrokenApptNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBrokenApptNote.UseVisualStyleBackColor = true;
			// 
			// comboBrokenApptAdjType
			// 
			this.comboBrokenApptAdjType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBrokenApptAdjType.FormattingEnabled = true;
			this.comboBrokenApptAdjType.Location = new System.Drawing.Point(254,84);
			this.comboBrokenApptAdjType.MaxDropDownItems = 30;
			this.comboBrokenApptAdjType.Name = "comboBrokenApptAdjType";
			this.comboBrokenApptAdjType.Size = new System.Drawing.Size(163,21);
			this.comboBrokenApptAdjType.TabIndex = 70;
			// 
			// checkApptBubbleDelay
			// 
			this.checkApptBubbleDelay.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptBubbleDelay.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkApptBubbleDelay.Location = new System.Drawing.Point(54,31);
			this.checkApptBubbleDelay.Name = "checkApptBubbleDelay";
			this.checkApptBubbleDelay.Size = new System.Drawing.Size(362,17);
			this.checkApptBubbleDelay.TabIndex = 69;
			this.checkApptBubbleDelay.Text = "Appointment bubble popup delay";
			this.checkApptBubbleDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptBubbleDelay.UseVisualStyleBackColor = true;
			// 
			// checkAppointmentBubblesDisabled
			// 
			this.checkAppointmentBubblesDisabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAppointmentBubblesDisabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAppointmentBubblesDisabled.Location = new System.Drawing.Point(54,14);
			this.checkAppointmentBubblesDisabled.Name = "checkAppointmentBubblesDisabled";
			this.checkAppointmentBubblesDisabled.Size = new System.Drawing.Size(362,17);
			this.checkAppointmentBubblesDisabled.TabIndex = 70;
			this.checkAppointmentBubblesDisabled.Text = "Appointment bubble popup disabled";
			this.checkAppointmentBubblesDisabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAppointmentBubblesDisabled.UseVisualStyleBackColor = true;
			// 
			// checkDeductibleBeforePercent
			// 
			this.checkDeductibleBeforePercent.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkDeductibleBeforePercent.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDeductibleBeforePercent.Location = new System.Drawing.Point(70,36);
			this.checkDeductibleBeforePercent.Name = "checkDeductibleBeforePercent";
			this.checkDeductibleBeforePercent.Size = new System.Drawing.Size(346,17);
			this.checkDeductibleBeforePercent.TabIndex = 68;
			this.checkDeductibleBeforePercent.Text = "Ins Plans default to apply deductible before percent.";
			this.checkDeductibleBeforePercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkDeductibleBeforePercent.UseVisualStyleBackColor = true;
			this.checkDeductibleBeforePercent.Click += new System.EventHandler(this.checkDeductibleBeforePercent_Click);
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 0;
			this.toolTip1.AutoPopDelay = 600000;
			this.toolTip1.InitialDelay = 0;
			this.toolTip1.IsBalloon = true;
			this.toolTip1.ReshowDelay = 0;
			this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.toolTip1.ToolTipTitle = "Help";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.checkChartQuickAddHideAmalgam);
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Controls.Add(this.checkAllowSettingProcsComplete);
			this.groupBox3.Controls.Add(this.comboToothNomenclature);
			this.groupBox3.Controls.Add(this.checkAutoClearEntryStatus);
			this.groupBox3.Location = new System.Drawing.Point(454,381);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(422,117);
			this.groupBox3.TabIndex = 191;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Chart module";
			// 
			// checkChartQuickAddHideAmalgam
			// 
			this.checkChartQuickAddHideAmalgam.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkChartQuickAddHideAmalgam.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkChartQuickAddHideAmalgam.Location = new System.Drawing.Point(35,80);
			this.checkChartQuickAddHideAmalgam.Name = "checkChartQuickAddHideAmalgam";
			this.checkChartQuickAddHideAmalgam.Size = new System.Drawing.Size(381,15);
			this.checkChartQuickAddHideAmalgam.TabIndex = 195;
			this.checkChartQuickAddHideAmalgam.Text = "Hide amalgam buttons in Quick Buttons section";
			this.checkChartQuickAddHideAmalgam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkChartQuickAddHideAmalgam.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(17,56);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(144,13);
			this.label11.TabIndex = 194;
			this.label11.Text = "Tooth Nomenclature";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkAllowSettingProcsComplete
			// 
			this.checkAllowSettingProcsComplete.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAllowSettingProcsComplete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllowSettingProcsComplete.Location = new System.Drawing.Point(2,34);
			this.checkAllowSettingProcsComplete.Name = "checkAllowSettingProcsComplete";
			this.checkAllowSettingProcsComplete.Size = new System.Drawing.Size(414,15);
			this.checkAllowSettingProcsComplete.TabIndex = 74;
			this.checkAllowSettingProcsComplete.Text = "Allow setting procedures complete.  (It\'s better to only set appointments complet" +
    "e)";
			this.checkAllowSettingProcsComplete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAllowSettingProcsComplete.UseVisualStyleBackColor = true;
			// 
			// comboToothNomenclature
			// 
			this.comboToothNomenclature.FormattingEnabled = true;
			this.comboToothNomenclature.Location = new System.Drawing.Point(163,53);
			this.comboToothNomenclature.Name = "comboToothNomenclature";
			this.comboToothNomenclature.Size = new System.Drawing.Size(254,21);
			this.comboToothNomenclature.TabIndex = 193;
			// 
			// checkAutoClearEntryStatus
			// 
			this.checkAutoClearEntryStatus.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAutoClearEntryStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAutoClearEntryStatus.Location = new System.Drawing.Point(35,17);
			this.checkAutoClearEntryStatus.Name = "checkAutoClearEntryStatus";
			this.checkAutoClearEntryStatus.Size = new System.Drawing.Size(381,15);
			this.checkAutoClearEntryStatus.TabIndex = 73;
			this.checkAutoClearEntryStatus.Text = "Reset entry status to TreatPlan when switching patients";
			this.checkAutoClearEntryStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAutoClearEntryStatus.UseVisualStyleBackColor = true;
			// 
			// checkPPOpercentage
			// 
			this.checkPPOpercentage.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPPOpercentage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPPOpercentage.Location = new System.Drawing.Point(3,86);
			this.checkPPOpercentage.Name = "checkPPOpercentage";
			this.checkPPOpercentage.Size = new System.Drawing.Size(413,17);
			this.checkPPOpercentage.TabIndex = 192;
			this.checkPPOpercentage.Text = "Insurance defaults to PPO percentage instead of category percentage plan type";
			this.checkPPOpercentage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkMedicalEclaimsEnabled);
			this.groupBox2.Controls.Add(this.checkPPOpercentage);
			this.groupBox2.Controls.Add(this.checkDeductibleBeforePercent);
			this.groupBox2.Controls.Add(this.checkInsurancePlansShared);
			this.groupBox2.Location = new System.Drawing.Point(12,234);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(422,111);
			this.groupBox2.TabIndex = 193;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Family module";
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
			this.butCancel.Location = new System.Drawing.Point(768,533);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
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
			this.butOK.Location = new System.Drawing.Point(663,533);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 7;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// comboBillingChargeAdjType
			// 
			this.comboBillingChargeAdjType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBillingChargeAdjType.FormattingEnabled = true;
			this.comboBillingChargeAdjType.Location = new System.Drawing.Point(254,222);
			this.comboBillingChargeAdjType.MaxDropDownItems = 30;
			this.comboBillingChargeAdjType.Name = "comboBillingChargeAdjType";
			this.comboBillingChargeAdjType.Size = new System.Drawing.Size(163,21);
			this.comboBillingChargeAdjType.TabIndex = 199;
			// 
			// FormModuleSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(890,583);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormModuleSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Module Setup";
			this.Load += new System.EventHandler(this.FormModuleSetup_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormModuleSetup_Load(object sender, System.EventArgs e) {
			//Appointment module---------------------------------------------------------------
			checkSolidBlockouts.Checked=PrefC.GetBool("SolidBlockouts");
			checkBrokenApptNote.Checked=PrefC.GetBool("BrokenApptCommLogNotAdjustment");
			checkApptBubbleDelay.Checked = PrefC.GetBool("ApptBubbleDelay");
			checkAppointmentBubblesDisabled.Checked=PrefC.GetBool("AppointmentBubblesDisabled");
			posAdjTypes=DefC.GetPositiveAdjTypes();
			for(int i=0;i<posAdjTypes.Count;i++){
				comboFinanceChargeAdjType.Items.Add(posAdjTypes[i].ItemName);
				if(PrefC.GetInt("FinanceChargeAdjustmentType")==posAdjTypes[i].DefNum){
					comboFinanceChargeAdjType.SelectedIndex=i;
				}
				comboBillingChargeAdjType.Items.Add(posAdjTypes[i].ItemName);
				if(PrefC.GetInt("BillingChargeAdjustmentType")==posAdjTypes[i].DefNum) {
					comboBillingChargeAdjType.SelectedIndex=i;
				}
				comboBrokenApptAdjType.Items.Add(posAdjTypes[i].ItemName);
				if(PrefC.GetInt("BrokenAppointmentAdjustmentType")==posAdjTypes[i].DefNum) {
					comboBrokenApptAdjType.SelectedIndex=i;
				}
			}
			checkApptExclamation.Checked=PrefC.GetBool("ApptExclamationShowForUnsentIns");
			comboTimeArrived.Items.Add(Lan.g(this,"none"));
			comboTimeArrived.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.ApptConfirmed].Length;i++){
				comboTimeArrived.Items.Add(DefC.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(DefC.Short[(int)DefCat.ApptConfirmed][i].DefNum==PrefC.GetInt("AppointmentTimeArrivedTrigger")){
					comboTimeArrived.SelectedIndex=i+1;
				}
			}
			comboTimeSeated.Items.Add(Lan.g(this,"none"));
			comboTimeSeated.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.ApptConfirmed].Length;i++){
				comboTimeSeated.Items.Add(DefC.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(DefC.Short[(int)DefCat.ApptConfirmed][i].DefNum==PrefC.GetInt("AppointmentTimeSeatedTrigger")){
					comboTimeSeated.SelectedIndex=i+1;
				}
			}
			comboTimeDismissed.Items.Add(Lan.g(this,"none"));
			comboTimeDismissed.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.ApptConfirmed].Length;i++){
				comboTimeDismissed.Items.Add(DefC.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(DefC.Short[(int)DefCat.ApptConfirmed][i].DefNum==PrefC.GetInt("AppointmentTimeDismissedTrigger")){
					comboTimeDismissed.SelectedIndex=i+1;
				}
			}
			checkApptRefreshEveryMinute.Checked=PrefC.GetBool("ApptModuleRefreshesEveryMinute");
			//Family module-----------------------------------------------------------------------
			checkMedicalEclaimsEnabled.Checked=PrefC.GetBool("MedicalEclaimsEnabled");
			checkDeductibleBeforePercent.Checked=PrefC.GetBool("DeductibleBeforePercentAsDefault");
			checkInsurancePlansShared.Checked=PrefC.GetBool("InsurancePlansShared");
			checkPPOpercentage.Checked=PrefC.GetBool("InsDefaultPPOpercent");
			//Account module-----------------------------------------------------------------------
			checkStatementShowReturnAddress.Checked=PrefC.GetBool("StatementShowReturnAddress");
			checkShowCC.Checked=PrefC.GetBool("StatementShowCreditCard");
			comboUseChartNum.Items.Add(Lan.g(this,"PatNum"));
			comboUseChartNum.Items.Add(Lan.g(this,"ChartNumber"));
			if(PrefC.GetBool("StatementAccountsUseChartNumber")){
				comboUseChartNum.SelectedIndex=1;
			}
			else{
				comboUseChartNum.SelectedIndex=0;
			}
			if(PrefC.GetInt("StatementsCalcDueDate")!=-1){
				textStatementsCalcDueDate.Text=PrefC.GetInt("StatementsCalcDueDate").ToString();
			}
			textPayPlansBillInAdvanceDays.Text=PrefC.GetInt("PayPlansBillInAdvanceDays").ToString();
			checkBalancesDontSubtractIns.Checked=PrefC.GetBool("BalancesDontSubtractIns");
			checkAgingMonthly.Checked=PrefC.GetBool("AgingCalculatedMonthlyInsteadOfDaily");
			checkEclaimsSeparateTreatProv.Checked=PrefC.GetBool("EclaimsSeparateTreatProv");
			checkStoreCCnumbers.Checked=PrefC.GetBool("StoreCCnumbers");
			checkProviderIncomeShows.Checked=PrefC.GetBool("ProviderIncomeTransferShows");
			textClaimAttachPath.Text=PrefC.GetString("ClaimAttachExportPath");
			checkShowFamilyCommByDefault.Checked=PrefC.GetBool("ShowAccountFamilyCommEntries");
			checkClaimFormTreatDentSaysSigOnFile.Checked=PrefC.GetBool("ClaimFormTreatDentSaysSigOnFile");
			checkStatementSummaryShowInsInfo.Checked=PrefC.GetBool("StatementSummaryShowInsInfo");
			//TP module-----------------------------------------------------------------------
			textTreatNote.Text=PrefC.GetString("TreatmentPlanNote");
			checkTreatPlanShowGraphics.Checked=PrefC.GetBool("TreatPlanShowGraphics");
			checkTreatPlanShowCompleted.Checked=PrefC.GetBool("TreatPlanShowCompleted");
			//Chart module-----------------------------------------------------------------------
			comboToothNomenclature.Items.Add(Lan.g(this, "Universal (Common in the US, 1-32)"));
			comboToothNomenclature.Items.Add(Lan.g(this, "FDA Notation (International, 11-48)"));
			comboToothNomenclature.Items.Add(Lan.g(this, "Haderup (Danish)"));
			comboToothNomenclature.Items.Add(Lan.g(this, "Palmer (Ortho)"));
			comboToothNomenclature.SelectedIndex = PrefC.GetInt("UseInternationalToothNumbers");
			checkAutoClearEntryStatus.Checked=PrefC.GetBool("AutoResetTPEntryStatus");
			checkAllowSettingProcsComplete.Checked=PrefC.GetBool("AllowSettingProcsComplete");
			checkChartQuickAddHideAmalgam.Checked=PrefC.GetBool("ChartQuickAddHideAmalgam");
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
			if(comboBrokenApptAdjType.SelectedIndex==-1){
				MsgBox.Show(this,"Please enter an adjustment type for broken appointments.");
				return;
			}
			if(comboFinanceChargeAdjType.SelectedIndex==-1) {
				MsgBox.Show(this,"Please enter an adjustment type for finance charges.");
				return;
			}
			if(comboBillingChargeAdjType.SelectedIndex==-1) {
				MsgBox.Show(this,"Please enter an adjustment type for billing charges.");
				return;
			}
			if(textStatementsCalcDueDate.errorProvider1.GetError(textStatementsCalcDueDate)!=""
				| textPayPlansBillInAdvanceDays.errorProvider1.GetError(textPayPlansBillInAdvanceDays)!="")
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			bool changed=false;
			if( Prefs.UpdateString("TreatmentPlanNote",textTreatNote.Text)
				| Prefs.UpdateBool("TreatPlanShowGraphics",checkTreatPlanShowGraphics.Checked)
				| Prefs.UpdateBool("TreatPlanShowCompleted",checkTreatPlanShowCompleted.Checked)
				| Prefs.UpdateBool("StatementShowReturnAddress",checkStatementShowReturnAddress.Checked)
				| Prefs.UpdateBool("StatementShowCreditCard",checkShowCC.Checked)
				| Prefs.UpdateBool("StatementAccountsUseChartNumber",comboUseChartNum.SelectedIndex==1)
				| Prefs.UpdateBool("BalancesDontSubtractIns",checkBalancesDontSubtractIns.Checked)
				| Prefs.UpdateInt("PayPlansBillInAdvanceDays",PIn.PInt(textPayPlansBillInAdvanceDays.Text))
				| Prefs.UpdateBool("AgingCalculatedMonthlyInsteadOfDaily",checkAgingMonthly.Checked)
				| Prefs.UpdateBool("EclaimsSeparateTreatProv",checkEclaimsSeparateTreatProv.Checked)
				| Prefs.UpdateBool("MedicalEclaimsEnabled",checkMedicalEclaimsEnabled.Checked)
				| Prefs.UpdateInt("UseInternationalToothNumbers", comboToothNomenclature.SelectedIndex)
				| Prefs.UpdateBool("InsurancePlansShared",checkInsurancePlansShared.Checked)
				| Prefs.UpdateBool("SolidBlockouts", checkSolidBlockouts.Checked)
				| Prefs.UpdateBool("StoreCCnumbers", checkStoreCCnumbers.Checked)
				| Prefs.UpdateBool("DeductibleBeforePercentAsDefault",checkDeductibleBeforePercent.Checked)
				| Prefs.UpdateBool("BrokenApptCommLogNotAdjustment", checkBrokenApptNote.Checked)
				| Prefs.UpdateBool("ApptBubbleDelay", checkApptBubbleDelay.Checked)
				| Prefs.UpdateBool("AppointmentBubblesDisabled", checkAppointmentBubblesDisabled.Checked)
				| Prefs.UpdateInt("FinanceChargeAdjustmentType",posAdjTypes[comboFinanceChargeAdjType.SelectedIndex].DefNum)
				| Prefs.UpdateInt("BillingChargeAdjustmentType",posAdjTypes[comboBillingChargeAdjType.SelectedIndex].DefNum)
				| Prefs.UpdateInt("BrokenAppointmentAdjustmentType",posAdjTypes[comboBrokenApptAdjType.SelectedIndex].DefNum)
				| Prefs.UpdateBool("ApptExclamationShowForUnsentIns", checkApptExclamation.Checked)
				| Prefs.UpdateBool("ProviderIncomeTransferShows", checkProviderIncomeShows.Checked)
				| Prefs.UpdateString("ClaimAttachExportPath",textClaimAttachPath.Text)
				| Prefs.UpdateBool("AutoResetTPEntryStatus",checkAutoClearEntryStatus.Checked)
				| Prefs.UpdateBool("AllowSettingProcsComplete",checkAllowSettingProcsComplete.Checked)
				| Prefs.UpdateBool("ShowAccountFamilyCommEntries",checkShowFamilyCommByDefault.Checked)
				| Prefs.UpdateBool("InsDefaultPPOpercent",checkPPOpercentage.Checked)
				| Prefs.UpdateBool("StatementSummaryShowInsInfo", checkStatementSummaryShowInsInfo.Checked)
				| Prefs.UpdateBool("ApptModuleRefreshesEveryMinute", checkApptRefreshEveryMinute.Checked)
				| Prefs.UpdateBool("ChartQuickAddHideAmalgam", checkChartQuickAddHideAmalgam.Checked)
				)
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
			int timeArrivedTrigger=0;
			if(comboTimeArrived.SelectedIndex>0){
				timeArrivedTrigger=DefC.Short[(int)DefCat.ApptConfirmed][comboTimeArrived.SelectedIndex-1].DefNum;
			}
			if(Prefs.UpdateInt("AppointmentTimeArrivedTrigger",timeArrivedTrigger)){
				changed=true;
			}
			int timeSeatedTrigger=0;
			if(comboTimeSeated.SelectedIndex>0){
				timeSeatedTrigger=DefC.Short[(int)DefCat.ApptConfirmed][comboTimeSeated.SelectedIndex-1].DefNum;
			}
			if(Prefs.UpdateInt("AppointmentTimeSeatedTrigger",timeSeatedTrigger)){
				changed=true;
			}
			int timeDismissedTrigger=0;
			if(comboTimeDismissed.SelectedIndex>0){
				timeDismissedTrigger=DefC.Short[(int)DefCat.ApptConfirmed][comboTimeDismissed.SelectedIndex-1].DefNum;
			}
			if(Prefs.UpdateInt("AppointmentTimeDismissedTrigger",timeDismissedTrigger)){
				changed=true;
			}
			if(changed){
				DataValid.SetInvalid(InvalidType.Prefs);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}






