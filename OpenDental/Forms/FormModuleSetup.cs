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
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidNumber textStatementsCalcDueDate;
		private System.Windows.Forms.CheckBox checkEclaimsSeparateTreatProv;
		private System.Windows.Forms.CheckBox checkBalancesDontSubtractIns;
		private System.Windows.Forms.CheckBox checkInsurancePlansShared;
		private CheckBox checkMedicalEclaimsEnabled;
		private CheckBox checkStatementShowReturnAddress;
    private CheckBox checkSolidBlockouts;
		private CheckBox checkAgingMonthly;
		private CheckBox checkBrokenApptNote;
		private ToolTip toolTip1;
		private CheckBox checkApptBubbleDelay;
		private CheckBox checkStoreCCnumbers;
		private CheckBox checkAppointmentBubblesDisabled;
		private ComboBox comboUseChartNum;
		private Label label10;
		private Label label7;
		private ComboBox comboBrokenApptAdjType;
		private Label label12;
		private ComboBox comboFinanceChargeAdjType;
		private System.Windows.Forms.Label label1;
		private CheckBox checkApptExclamation;
		private CheckBox checkProviderIncomeShows;
		//private ComputerPref computerPref;
		private TextBox textClaimAttachPath;
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
		private Label label6;
		private ComboBox comboTimeDismissed;
		private Label label5;
		private ComboBox comboTimeSeated;
		private Label label3;
		private ComboBox comboTimeArrived;
		private CheckBox checkApptRefreshEveryMinute;
		private CheckBox checkChartQuickAddHideAmalgam;
		private ComboBox comboBillingChargeAdjType;
		private CheckBox checkAllowedFeeSchedsAutomate;
		private CheckBox checkIntermingleDefault;
		private CheckBox checkCoPayFeeScheduleBlankLikeZero;
		private CheckBox checkStatementShowProcBreakdown;
		private CheckBox checkStatementShowNotes;
		private CheckBox checkClaimsValidateACN;
		private CheckBox checkInsDefaultShowUCRonClaims;
		private CheckBox checkToothChartMoveMenuToRight;
		private CheckBox checkImagesModuleTreeIsCollapsed;
		private CheckBox checkRxSendNewToQueue;
		private TabControl tabControl1;
		private TabPage tabAppts;
		private TabPage tabFamily;
		private TabPage tabAccount;
		private TabPage tabTreatPlan;
		private TabPage tabChart;
		private TabPage tabImages;
		private TabPage tabManage;
		private UI.Button butProblemsIndicateNone;
		private TextBox textProblemsIndicateNone;
		private Label label8;
		private List<Def> posAdjTypes;
		private UI.Button butMedicationsIndicateNone;
		private TextBox textMedicationsIndicateNone;
		private Label label9;
		private CheckBox checkStoreCCTokens;
		private UI.Button butAllergiesIndicateNone;
		private TextBox textAllergiesIndicateNone;
		private Label label14;
		private bool changed;

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
			this.checkClaimsValidateACN = new System.Windows.Forms.CheckBox();
			this.checkStatementShowProcBreakdown = new System.Windows.Forms.CheckBox();
			this.checkStatementShowNotes = new System.Windows.Forms.CheckBox();
			this.checkIntermingleDefault = new System.Windows.Forms.CheckBox();
			this.comboBillingChargeAdjType = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.checkClaimFormTreatDentSaysSigOnFile = new System.Windows.Forms.CheckBox();
			this.checkStatementSummaryShowInsInfo = new System.Windows.Forms.CheckBox();
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
			this.label2 = new System.Windows.Forms.Label();
			this.checkInsurancePlansShared = new System.Windows.Forms.CheckBox();
			this.checkMedicalEclaimsEnabled = new System.Windows.Forms.CheckBox();
			this.checkSolidBlockouts = new System.Windows.Forms.CheckBox();
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
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.checkToothChartMoveMenuToRight = new System.Windows.Forms.CheckBox();
			this.checkChartQuickAddHideAmalgam = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.checkAllowSettingProcsComplete = new System.Windows.Forms.CheckBox();
			this.comboToothNomenclature = new System.Windows.Forms.ComboBox();
			this.checkAutoClearEntryStatus = new System.Windows.Forms.CheckBox();
			this.checkPPOpercentage = new System.Windows.Forms.CheckBox();
			this.checkInsDefaultShowUCRonClaims = new System.Windows.Forms.CheckBox();
			this.checkCoPayFeeScheduleBlankLikeZero = new System.Windows.Forms.CheckBox();
			this.checkAllowedFeeSchedsAutomate = new System.Windows.Forms.CheckBox();
			this.checkImagesModuleTreeIsCollapsed = new System.Windows.Forms.CheckBox();
			this.checkRxSendNewToQueue = new System.Windows.Forms.CheckBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabAppts = new System.Windows.Forms.TabPage();
			this.tabFamily = new System.Windows.Forms.TabPage();
			this.tabAccount = new System.Windows.Forms.TabPage();
			this.checkStoreCCTokens = new System.Windows.Forms.CheckBox();
			this.textStatementsCalcDueDate = new OpenDental.ValidNumber();
			this.textPayPlansBillInAdvanceDays = new OpenDental.ValidNum();
			this.tabTreatPlan = new System.Windows.Forms.TabPage();
			this.tabChart = new System.Windows.Forms.TabPage();
			this.butMedicationsIndicateNone = new OpenDental.UI.Button();
			this.textMedicationsIndicateNone = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.butProblemsIndicateNone = new OpenDental.UI.Button();
			this.textProblemsIndicateNone = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.tabImages = new System.Windows.Forms.TabPage();
			this.tabManage = new System.Windows.Forms.TabPage();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butAllergiesIndicateNone = new OpenDental.UI.Button();
			this.textAllergiesIndicateNone = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabAppts.SuspendLayout();
			this.tabFamily.SuspendLayout();
			this.tabAccount.SuspendLayout();
			this.tabTreatPlan.SuspendLayout();
			this.tabChart.SuspendLayout();
			this.tabImages.SuspendLayout();
			this.tabManage.SuspendLayout();
			this.SuspendLayout();
			// 
			// textTreatNote
			// 
			this.textTreatNote.AcceptsReturn = true;
			this.textTreatNote.Location = new System.Drawing.Point(77,7);
			this.textTreatNote.Multiline = true;
			this.textTreatNote.Name = "textTreatNote";
			this.textTreatNote.Size = new System.Drawing.Size(363,53);
			this.textTreatNote.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28,7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48,52);
			this.label1.TabIndex = 35;
			this.label1.Text = "Default Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkShowCC
			// 
			this.checkShowCC.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowCC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowCC.Location = new System.Drawing.Point(72,24);
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
			this.checkTreatPlanShowGraphics.Location = new System.Drawing.Point(81,62);
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
			this.checkTreatPlanShowCompleted.Location = new System.Drawing.Point(81,79);
			this.checkTreatPlanShowCompleted.Name = "checkTreatPlanShowCompleted";
			this.checkTreatPlanShowCompleted.Size = new System.Drawing.Size(359,17);
			this.checkTreatPlanShowCompleted.TabIndex = 47;
			this.checkTreatPlanShowCompleted.Text = "Show Completed Work on Graphical Tooth Chart";
			this.checkTreatPlanShowCompleted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkClaimsValidateACN
			// 
			this.checkClaimsValidateACN.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkClaimsValidateACN.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimsValidateACN.Location = new System.Drawing.Point(44,415);
			this.checkClaimsValidateACN.Name = "checkClaimsValidateACN";
			this.checkClaimsValidateACN.Size = new System.Drawing.Size(396,17);
			this.checkClaimsValidateACN.TabIndex = 194;
			this.checkClaimsValidateACN.Text = "Require ACN# in remarks on claims with ADDP group name";
			this.checkClaimsValidateACN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkStatementShowProcBreakdown
			// 
			this.checkStatementShowProcBreakdown.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStatementShowProcBreakdown.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStatementShowProcBreakdown.Location = new System.Drawing.Point(72,58);
			this.checkStatementShowProcBreakdown.Name = "checkStatementShowProcBreakdown";
			this.checkStatementShowProcBreakdown.Size = new System.Drawing.Size(368,17);
			this.checkStatementShowProcBreakdown.TabIndex = 202;
			this.checkStatementShowProcBreakdown.Text = "Show procedure breakdown on statements";
			this.checkStatementShowProcBreakdown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkStatementShowNotes
			// 
			this.checkStatementShowNotes.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStatementShowNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStatementShowNotes.Location = new System.Drawing.Point(72,41);
			this.checkStatementShowNotes.Name = "checkStatementShowNotes";
			this.checkStatementShowNotes.Size = new System.Drawing.Size(368,17);
			this.checkStatementShowNotes.TabIndex = 201;
			this.checkStatementShowNotes.Text = "Show notes for payments and adjustments on statements";
			this.checkStatementShowNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIntermingleDefault
			// 
			this.checkIntermingleDefault.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIntermingleDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIntermingleDefault.Location = new System.Drawing.Point(30,399);
			this.checkIntermingleDefault.Name = "checkIntermingleDefault";
			this.checkIntermingleDefault.Size = new System.Drawing.Size(410,16);
			this.checkIntermingleDefault.TabIndex = 200;
			this.checkIntermingleDefault.Text = "Default to all types of statements printing in intermingled mode";
			this.checkIntermingleDefault.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBillingChargeAdjType
			// 
			this.comboBillingChargeAdjType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBillingChargeAdjType.FormattingEnabled = true;
			this.comboBillingChargeAdjType.Location = new System.Drawing.Point(278,263);
			this.comboBillingChargeAdjType.MaxDropDownItems = 30;
			this.comboBillingChargeAdjType.Name = "comboBillingChargeAdjType";
			this.comboBillingChargeAdjType.Size = new System.Drawing.Size(163,21);
			this.comboBillingChargeAdjType.TabIndex = 199;
			// 
			// label4
			// 
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(56,244);
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
			this.checkClaimFormTreatDentSaysSigOnFile.Location = new System.Drawing.Point(59,318);
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
			this.checkStatementSummaryShowInsInfo.Location = new System.Drawing.Point(72,335);
			this.checkStatementSummaryShowInsInfo.Name = "checkStatementSummaryShowInsInfo";
			this.checkStatementSummaryShowInsInfo.Size = new System.Drawing.Size(368,17);
			this.checkStatementSummaryShowInsInfo.TabIndex = 195;
			this.checkStatementSummaryShowInsInfo.Text = "Show insurance pending and related balance info on statement summary";
			this.checkStatementSummaryShowInsInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label18
			// 
			this.label18.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label18.Location = new System.Drawing.Point(61,137);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(318,27);
			this.label18.TabIndex = 76;
			this.label18.Text = "Days in advance to bill payment plan amounts due.\r\nUsually 10 or 15.";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textClaimAttachPath
			// 
			this.textClaimAttachPath.Location = new System.Drawing.Point(243,359);
			this.textClaimAttachPath.Name = "textClaimAttachPath";
			this.textClaimAttachPath.Size = new System.Drawing.Size(197,20);
			this.textClaimAttachPath.TabIndex = 189;
			// 
			// label20
			// 
			this.label20.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label20.Location = new System.Drawing.Point(54,362);
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
			this.checkShowFamilyCommByDefault.Location = new System.Drawing.Point(59,301);
			this.checkShowFamilyCommByDefault.Name = "checkShowFamilyCommByDefault";
			this.checkShowFamilyCommByDefault.Size = new System.Drawing.Size(381,17);
			this.checkShowFamilyCommByDefault.TabIndex = 75;
			this.checkShowFamilyCommByDefault.Text = "Show Family Comm Entries By Default";
			this.checkShowFamilyCommByDefault.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkProviderIncomeShows
			// 
			this.checkProviderIncomeShows.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkProviderIncomeShows.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkProviderIncomeShows.Location = new System.Drawing.Point(59,284);
			this.checkProviderIncomeShows.Name = "checkProviderIncomeShows";
			this.checkProviderIncomeShows.Size = new System.Drawing.Size(381,17);
			this.checkProviderIncomeShows.TabIndex = 74;
			this.checkProviderIncomeShows.Text = "Show provider income transfer window after entering insurance payment";
			this.checkProviderIncomeShows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkEclaimsSeparateTreatProv
			// 
			this.checkEclaimsSeparateTreatProv.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkEclaimsSeparateTreatProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEclaimsSeparateTreatProv.Location = new System.Drawing.Point(94,381);
			this.checkEclaimsSeparateTreatProv.Name = "checkEclaimsSeparateTreatProv";
			this.checkEclaimsSeparateTreatProv.Size = new System.Drawing.Size(346,17);
			this.checkEclaimsSeparateTreatProv.TabIndex = 53;
			this.checkEclaimsSeparateTreatProv.Text = "On e-claims, send treating provider info for each separate procedure";
			this.checkEclaimsSeparateTreatProv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label12.Location = new System.Drawing.Point(56,266);
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
			this.comboFinanceChargeAdjType.Location = new System.Drawing.Point(278,239);
			this.comboFinanceChargeAdjType.MaxDropDownItems = 30;
			this.comboFinanceChargeAdjType.Name = "comboFinanceChargeAdjType";
			this.comboFinanceChargeAdjType.Size = new System.Drawing.Size(163,21);
			this.comboFinanceChargeAdjType.TabIndex = 72;
			// 
			// label10
			// 
			this.label10.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label10.Location = new System.Drawing.Point(114,81);
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
			this.checkStoreCCnumbers.Location = new System.Drawing.Point(72,203);
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
			this.comboUseChartNum.Location = new System.Drawing.Point(311,78);
			this.comboUseChartNum.Name = "comboUseChartNum";
			this.comboUseChartNum.Size = new System.Drawing.Size(130,21);
			this.comboUseChartNum.TabIndex = 68;
			// 
			// checkAgingMonthly
			// 
			this.checkAgingMonthly.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAgingMonthly.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAgingMonthly.Location = new System.Drawing.Point(72,186);
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
			this.checkStatementShowReturnAddress.Location = new System.Drawing.Point(72,7);
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
			this.checkBalancesDontSubtractIns.Location = new System.Drawing.Point(72,169);
			this.checkBalancesDontSubtractIns.Name = "checkBalancesDontSubtractIns";
			this.checkBalancesDontSubtractIns.Size = new System.Drawing.Size(368,17);
			this.checkBalancesDontSubtractIns.TabIndex = 55;
			this.checkBalancesDontSubtractIns.Text = "Balances don\'t subtract insurance estimate";
			this.checkBalancesDontSubtractIns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(60,105);
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
			this.checkInsurancePlansShared.Location = new System.Drawing.Point(34,24);
			this.checkInsurancePlansShared.Name = "checkInsurancePlansShared";
			this.checkInsurancePlansShared.Size = new System.Drawing.Size(406,17);
			this.checkInsurancePlansShared.TabIndex = 58;
			this.checkInsurancePlansShared.Text = "InsPlan option at bottom, \'Change Plan for all subscribers\', is default.";
			this.checkInsurancePlansShared.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkMedicalEclaimsEnabled
			// 
			this.checkMedicalEclaimsEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMedicalEclaimsEnabled.Enabled = false;
			this.checkMedicalEclaimsEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkMedicalEclaimsEnabled.Location = new System.Drawing.Point(94,7);
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
			this.checkSolidBlockouts.Location = new System.Drawing.Point(78,41);
			this.checkSolidBlockouts.Name = "checkSolidBlockouts";
			this.checkSolidBlockouts.Size = new System.Drawing.Size(362,17);
			this.checkSolidBlockouts.TabIndex = 66;
			this.checkSolidBlockouts.Text = "Use solid blockouts instead of outlines on the appointment book";
			this.checkSolidBlockouts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkSolidBlockouts.UseVisualStyleBackColor = true;
			// 
			// checkApptRefreshEveryMinute
			// 
			this.checkApptRefreshEveryMinute.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptRefreshEveryMinute.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkApptRefreshEveryMinute.Location = new System.Drawing.Point(34,188);
			this.checkApptRefreshEveryMinute.Name = "checkApptRefreshEveryMinute";
			this.checkApptRefreshEveryMinute.Size = new System.Drawing.Size(406,17);
			this.checkApptRefreshEveryMinute.TabIndex = 198;
			this.checkApptRefreshEveryMinute.Text = "Refresh every 60 seconds.  Keeps waiting room times refreshed.";
			this.checkApptRefreshEveryMinute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label6.Location = new System.Drawing.Point(30,167);
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
			this.comboTimeDismissed.Location = new System.Drawing.Point(278,163);
			this.comboTimeDismissed.MaxDropDownItems = 30;
			this.comboTimeDismissed.Name = "comboTimeDismissed";
			this.comboTimeDismissed.Size = new System.Drawing.Size(163,21);
			this.comboTimeDismissed.TabIndex = 77;
			// 
			// label5
			// 
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label5.Location = new System.Drawing.Point(30,145);
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
			this.comboTimeSeated.Location = new System.Drawing.Point(278,141);
			this.comboTimeSeated.MaxDropDownItems = 30;
			this.comboTimeSeated.Name = "comboTimeSeated";
			this.comboTimeSeated.Size = new System.Drawing.Size(163,21);
			this.comboTimeSeated.TabIndex = 75;
			// 
			// label3
			// 
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(30,123);
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
			this.comboTimeArrived.Location = new System.Drawing.Point(278,119);
			this.comboTimeArrived.MaxDropDownItems = 30;
			this.comboTimeArrived.Name = "comboTimeArrived";
			this.comboTimeArrived.Size = new System.Drawing.Size(163,21);
			this.comboTimeArrived.TabIndex = 73;
			// 
			// checkApptExclamation
			// 
			this.checkApptExclamation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptExclamation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkApptExclamation.Location = new System.Drawing.Point(55,99);
			this.checkApptExclamation.Name = "checkApptExclamation";
			this.checkApptExclamation.Size = new System.Drawing.Size(385,17);
			this.checkApptExclamation.TabIndex = 72;
			this.checkApptExclamation.Text = "Show ! at upper right of appts for ins not sent (might cause slowdown)";
			this.checkApptExclamation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptExclamation.UseVisualStyleBackColor = true;
			// 
			// label7
			// 
			this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label7.Location = new System.Drawing.Point(56,81);
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
			this.checkBrokenApptNote.Location = new System.Drawing.Point(78,58);
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
			this.comboBrokenApptAdjType.Location = new System.Drawing.Point(278,77);
			this.comboBrokenApptAdjType.MaxDropDownItems = 30;
			this.comboBrokenApptAdjType.Name = "comboBrokenApptAdjType";
			this.comboBrokenApptAdjType.Size = new System.Drawing.Size(163,21);
			this.comboBrokenApptAdjType.TabIndex = 70;
			// 
			// checkApptBubbleDelay
			// 
			this.checkApptBubbleDelay.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptBubbleDelay.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkApptBubbleDelay.Location = new System.Drawing.Point(78,24);
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
			this.checkAppointmentBubblesDisabled.Location = new System.Drawing.Point(78,7);
			this.checkAppointmentBubblesDisabled.Name = "checkAppointmentBubblesDisabled";
			this.checkAppointmentBubblesDisabled.Size = new System.Drawing.Size(362,17);
			this.checkAppointmentBubblesDisabled.TabIndex = 70;
			this.checkAppointmentBubblesDisabled.Text = "Appointment bubble popup disabled";
			this.checkAppointmentBubblesDisabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAppointmentBubblesDisabled.UseVisualStyleBackColor = true;
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
			// checkToothChartMoveMenuToRight
			// 
			this.checkToothChartMoveMenuToRight.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkToothChartMoveMenuToRight.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkToothChartMoveMenuToRight.Location = new System.Drawing.Point(59,89);
			this.checkToothChartMoveMenuToRight.Name = "checkToothChartMoveMenuToRight";
			this.checkToothChartMoveMenuToRight.Size = new System.Drawing.Size(381,15);
			this.checkToothChartMoveMenuToRight.TabIndex = 196;
			this.checkToothChartMoveMenuToRight.Text = "Button for tooth chart, move to the right to prevent overlap when saving";
			this.checkToothChartMoveMenuToRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkToothChartMoveMenuToRight.UseVisualStyleBackColor = true;
			// 
			// checkChartQuickAddHideAmalgam
			// 
			this.checkChartQuickAddHideAmalgam.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkChartQuickAddHideAmalgam.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkChartQuickAddHideAmalgam.Location = new System.Drawing.Point(59,72);
			this.checkChartQuickAddHideAmalgam.Name = "checkChartQuickAddHideAmalgam";
			this.checkChartQuickAddHideAmalgam.Size = new System.Drawing.Size(381,15);
			this.checkChartQuickAddHideAmalgam.TabIndex = 195;
			this.checkChartQuickAddHideAmalgam.Text = "Hide amalgam buttons in Quick Buttons section";
			this.checkChartQuickAddHideAmalgam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkChartQuickAddHideAmalgam.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			this.label11.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label11.Location = new System.Drawing.Point(41,48);
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
			this.checkAllowSettingProcsComplete.Location = new System.Drawing.Point(26,26);
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
			this.comboToothNomenclature.Location = new System.Drawing.Point(187,45);
			this.comboToothNomenclature.Name = "comboToothNomenclature";
			this.comboToothNomenclature.Size = new System.Drawing.Size(254,21);
			this.comboToothNomenclature.TabIndex = 193;
			// 
			// checkAutoClearEntryStatus
			// 
			this.checkAutoClearEntryStatus.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAutoClearEntryStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAutoClearEntryStatus.Location = new System.Drawing.Point(59,9);
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
			this.checkPPOpercentage.Location = new System.Drawing.Point(27,41);
			this.checkPPOpercentage.Name = "checkPPOpercentage";
			this.checkPPOpercentage.Size = new System.Drawing.Size(413,17);
			this.checkPPOpercentage.TabIndex = 192;
			this.checkPPOpercentage.Text = "Insurance defaults to PPO percentage instead of category percentage plan type";
			this.checkPPOpercentage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkInsDefaultShowUCRonClaims
			// 
			this.checkInsDefaultShowUCRonClaims.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkInsDefaultShowUCRonClaims.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInsDefaultShowUCRonClaims.Location = new System.Drawing.Point(27,92);
			this.checkInsDefaultShowUCRonClaims.Name = "checkInsDefaultShowUCRonClaims";
			this.checkInsDefaultShowUCRonClaims.Size = new System.Drawing.Size(413,17);
			this.checkInsDefaultShowUCRonClaims.TabIndex = 196;
			this.checkInsDefaultShowUCRonClaims.Text = "Insurance plans default to show UCR fee on claims.";
			this.checkInsDefaultShowUCRonClaims.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkInsDefaultShowUCRonClaims.Click += new System.EventHandler(this.checkInsDefaultShowUCRonClaims_Click);
			// 
			// checkCoPayFeeScheduleBlankLikeZero
			// 
			this.checkCoPayFeeScheduleBlankLikeZero.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkCoPayFeeScheduleBlankLikeZero.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCoPayFeeScheduleBlankLikeZero.Location = new System.Drawing.Point(27,75);
			this.checkCoPayFeeScheduleBlankLikeZero.Name = "checkCoPayFeeScheduleBlankLikeZero";
			this.checkCoPayFeeScheduleBlankLikeZero.Size = new System.Drawing.Size(413,17);
			this.checkCoPayFeeScheduleBlankLikeZero.TabIndex = 195;
			this.checkCoPayFeeScheduleBlankLikeZero.Text = "Co-pay fee schedules treat blank entries as zero.";
			this.checkCoPayFeeScheduleBlankLikeZero.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkAllowedFeeSchedsAutomate
			// 
			this.checkAllowedFeeSchedsAutomate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAllowedFeeSchedsAutomate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllowedFeeSchedsAutomate.Location = new System.Drawing.Point(27,58);
			this.checkAllowedFeeSchedsAutomate.Name = "checkAllowedFeeSchedsAutomate";
			this.checkAllowedFeeSchedsAutomate.Size = new System.Drawing.Size(413,17);
			this.checkAllowedFeeSchedsAutomate.TabIndex = 193;
			this.checkAllowedFeeSchedsAutomate.Text = "Automatically generate allowed fee schedules for category percentage plans";
			this.checkAllowedFeeSchedsAutomate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAllowedFeeSchedsAutomate.Click += new System.EventHandler(this.checkAllowedFeeSchedsAutomate_Click);
			// 
			// checkImagesModuleTreeIsCollapsed
			// 
			this.checkImagesModuleTreeIsCollapsed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkImagesModuleTreeIsCollapsed.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkImagesModuleTreeIsCollapsed.Location = new System.Drawing.Point(81,7);
			this.checkImagesModuleTreeIsCollapsed.Name = "checkImagesModuleTreeIsCollapsed";
			this.checkImagesModuleTreeIsCollapsed.Size = new System.Drawing.Size(359,17);
			this.checkImagesModuleTreeIsCollapsed.TabIndex = 47;
			this.checkImagesModuleTreeIsCollapsed.Text = "Document tree collapses when patient changes";
			this.checkImagesModuleTreeIsCollapsed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkRxSendNewToQueue
			// 
			this.checkRxSendNewToQueue.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkRxSendNewToQueue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRxSendNewToQueue.Location = new System.Drawing.Point(81,7);
			this.checkRxSendNewToQueue.Name = "checkRxSendNewToQueue";
			this.checkRxSendNewToQueue.Size = new System.Drawing.Size(359,17);
			this.checkRxSendNewToQueue.TabIndex = 47;
			this.checkRxSendNewToQueue.Text = "Send all new prescriptions to electronic queue";
			this.checkRxSendNewToQueue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabAppts);
			this.tabControl1.Controls.Add(this.tabFamily);
			this.tabControl1.Controls.Add(this.tabAccount);
			this.tabControl1.Controls.Add(this.tabTreatPlan);
			this.tabControl1.Controls.Add(this.tabChart);
			this.tabControl1.Controls.Add(this.tabImages);
			this.tabControl1.Controls.Add(this.tabManage);
			this.tabControl1.Location = new System.Drawing.Point(20,10);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(474,464);
			this.tabControl1.TabIndex = 196;
			// 
			// tabAppts
			// 
			this.tabAppts.Controls.Add(this.checkApptRefreshEveryMinute);
			this.tabAppts.Controls.Add(this.checkAppointmentBubblesDisabled);
			this.tabAppts.Controls.Add(this.label6);
			this.tabAppts.Controls.Add(this.checkSolidBlockouts);
			this.tabAppts.Controls.Add(this.comboTimeDismissed);
			this.tabAppts.Controls.Add(this.checkApptBubbleDelay);
			this.tabAppts.Controls.Add(this.label5);
			this.tabAppts.Controls.Add(this.comboBrokenApptAdjType);
			this.tabAppts.Controls.Add(this.comboTimeSeated);
			this.tabAppts.Controls.Add(this.checkBrokenApptNote);
			this.tabAppts.Controls.Add(this.label3);
			this.tabAppts.Controls.Add(this.label7);
			this.tabAppts.Controls.Add(this.comboTimeArrived);
			this.tabAppts.Controls.Add(this.checkApptExclamation);
			this.tabAppts.Location = new System.Drawing.Point(4,22);
			this.tabAppts.Name = "tabAppts";
			this.tabAppts.Padding = new System.Windows.Forms.Padding(3);
			this.tabAppts.Size = new System.Drawing.Size(466,438);
			this.tabAppts.TabIndex = 0;
			this.tabAppts.Text = "Appts";
			this.tabAppts.UseVisualStyleBackColor = true;
			// 
			// tabFamily
			// 
			this.tabFamily.Controls.Add(this.checkInsDefaultShowUCRonClaims);
			this.tabFamily.Controls.Add(this.checkMedicalEclaimsEnabled);
			this.tabFamily.Controls.Add(this.checkCoPayFeeScheduleBlankLikeZero);
			this.tabFamily.Controls.Add(this.checkInsurancePlansShared);
			this.tabFamily.Controls.Add(this.checkAllowedFeeSchedsAutomate);
			this.tabFamily.Controls.Add(this.checkPPOpercentage);
			this.tabFamily.Location = new System.Drawing.Point(4,22);
			this.tabFamily.Name = "tabFamily";
			this.tabFamily.Padding = new System.Windows.Forms.Padding(3);
			this.tabFamily.Size = new System.Drawing.Size(466,438);
			this.tabFamily.TabIndex = 1;
			this.tabFamily.Text = "Family";
			this.tabFamily.UseVisualStyleBackColor = true;
			// 
			// tabAccount
			// 
			this.tabAccount.Controls.Add(this.checkStoreCCTokens);
			this.tabAccount.Controls.Add(this.checkClaimsValidateACN);
			this.tabAccount.Controls.Add(this.checkStatementShowReturnAddress);
			this.tabAccount.Controls.Add(this.checkStatementShowProcBreakdown);
			this.tabAccount.Controls.Add(this.checkShowCC);
			this.tabAccount.Controls.Add(this.checkStatementShowNotes);
			this.tabAccount.Controls.Add(this.label2);
			this.tabAccount.Controls.Add(this.checkIntermingleDefault);
			this.tabAccount.Controls.Add(this.comboBillingChargeAdjType);
			this.tabAccount.Controls.Add(this.checkBalancesDontSubtractIns);
			this.tabAccount.Controls.Add(this.label4);
			this.tabAccount.Controls.Add(this.checkAgingMonthly);
			this.tabAccount.Controls.Add(this.checkClaimFormTreatDentSaysSigOnFile);
			this.tabAccount.Controls.Add(this.comboUseChartNum);
			this.tabAccount.Controls.Add(this.checkStatementSummaryShowInsInfo);
			this.tabAccount.Controls.Add(this.checkStoreCCnumbers);
			this.tabAccount.Controls.Add(this.label10);
			this.tabAccount.Controls.Add(this.label18);
			this.tabAccount.Controls.Add(this.comboFinanceChargeAdjType);
			this.tabAccount.Controls.Add(this.textClaimAttachPath);
			this.tabAccount.Controls.Add(this.label12);
			this.tabAccount.Controls.Add(this.label20);
			this.tabAccount.Controls.Add(this.checkEclaimsSeparateTreatProv);
			this.tabAccount.Controls.Add(this.checkShowFamilyCommByDefault);
			this.tabAccount.Controls.Add(this.checkProviderIncomeShows);
			this.tabAccount.Controls.Add(this.textStatementsCalcDueDate);
			this.tabAccount.Controls.Add(this.textPayPlansBillInAdvanceDays);
			this.tabAccount.Location = new System.Drawing.Point(4,22);
			this.tabAccount.Name = "tabAccount";
			this.tabAccount.Size = new System.Drawing.Size(466,438);
			this.tabAccount.TabIndex = 2;
			this.tabAccount.Text = "Account";
			this.tabAccount.UseVisualStyleBackColor = true;
			// 
			// checkStoreCCTokens
			// 
			this.checkStoreCCTokens.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStoreCCTokens.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStoreCCTokens.Location = new System.Drawing.Point(72,220);
			this.checkStoreCCTokens.Name = "checkStoreCCTokens";
			this.checkStoreCCTokens.Size = new System.Drawing.Size(368,17);
			this.checkStoreCCTokens.TabIndex = 203;
			this.checkStoreCCTokens.Text = "Automatically store X-Charge Tokens";
			this.checkStoreCCTokens.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStoreCCTokens.UseVisualStyleBackColor = true;
			// 
			// textStatementsCalcDueDate
			// 
			this.textStatementsCalcDueDate.Location = new System.Drawing.Point(381,109);
			this.textStatementsCalcDueDate.MaxVal = 255;
			this.textStatementsCalcDueDate.MinVal = 0;
			this.textStatementsCalcDueDate.Name = "textStatementsCalcDueDate";
			this.textStatementsCalcDueDate.Size = new System.Drawing.Size(60,20);
			this.textStatementsCalcDueDate.TabIndex = 54;
			this.textStatementsCalcDueDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPayPlansBillInAdvanceDays
			// 
			this.textPayPlansBillInAdvanceDays.Location = new System.Drawing.Point(381,141);
			this.textPayPlansBillInAdvanceDays.MaxVal = 255;
			this.textPayPlansBillInAdvanceDays.MinVal = 0;
			this.textPayPlansBillInAdvanceDays.Name = "textPayPlansBillInAdvanceDays";
			this.textPayPlansBillInAdvanceDays.Size = new System.Drawing.Size(60,20);
			this.textPayPlansBillInAdvanceDays.TabIndex = 193;
			this.textPayPlansBillInAdvanceDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// tabTreatPlan
			// 
			this.tabTreatPlan.Controls.Add(this.label1);
			this.tabTreatPlan.Controls.Add(this.textTreatNote);
			this.tabTreatPlan.Controls.Add(this.checkTreatPlanShowCompleted);
			this.tabTreatPlan.Controls.Add(this.checkTreatPlanShowGraphics);
			this.tabTreatPlan.Location = new System.Drawing.Point(4,22);
			this.tabTreatPlan.Name = "tabTreatPlan";
			this.tabTreatPlan.Size = new System.Drawing.Size(466,438);
			this.tabTreatPlan.TabIndex = 3;
			this.tabTreatPlan.Text = "Treat\' Plan";
			this.tabTreatPlan.UseVisualStyleBackColor = true;
			// 
			// tabChart
			// 
			this.tabChart.Controls.Add(this.butAllergiesIndicateNone);
			this.tabChart.Controls.Add(this.textAllergiesIndicateNone);
			this.tabChart.Controls.Add(this.label14);
			this.tabChart.Controls.Add(this.butMedicationsIndicateNone);
			this.tabChart.Controls.Add(this.textMedicationsIndicateNone);
			this.tabChart.Controls.Add(this.label9);
			this.tabChart.Controls.Add(this.butProblemsIndicateNone);
			this.tabChart.Controls.Add(this.textProblemsIndicateNone);
			this.tabChart.Controls.Add(this.label8);
			this.tabChart.Controls.Add(this.checkToothChartMoveMenuToRight);
			this.tabChart.Controls.Add(this.checkAutoClearEntryStatus);
			this.tabChart.Controls.Add(this.checkChartQuickAddHideAmalgam);
			this.tabChart.Controls.Add(this.comboToothNomenclature);
			this.tabChart.Controls.Add(this.label11);
			this.tabChart.Controls.Add(this.checkAllowSettingProcsComplete);
			this.tabChart.Location = new System.Drawing.Point(4,22);
			this.tabChart.Name = "tabChart";
			this.tabChart.Size = new System.Drawing.Size(466,438);
			this.tabChart.TabIndex = 4;
			this.tabChart.Text = "Chart";
			this.tabChart.UseVisualStyleBackColor = true;
			// 
			// butMedicationsIndicateNone
			// 
			this.butMedicationsIndicateNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butMedicationsIndicateNone.Autosize = true;
			this.butMedicationsIndicateNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMedicationsIndicateNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMedicationsIndicateNone.CornerRadius = 4F;
			this.butMedicationsIndicateNone.Location = new System.Drawing.Point(419,132);
			this.butMedicationsIndicateNone.Name = "butMedicationsIndicateNone";
			this.butMedicationsIndicateNone.Size = new System.Drawing.Size(22,21);
			this.butMedicationsIndicateNone.TabIndex = 202;
			this.butMedicationsIndicateNone.Text = "...";
			this.butMedicationsIndicateNone.Click += new System.EventHandler(this.butMedicationsIndicateNone_Click);
			// 
			// textMedicationsIndicateNone
			// 
			this.textMedicationsIndicateNone.Location = new System.Drawing.Point(270,133);
			this.textMedicationsIndicateNone.Name = "textMedicationsIndicateNone";
			this.textMedicationsIndicateNone.ReadOnly = true;
			this.textMedicationsIndicateNone.Size = new System.Drawing.Size(145,20);
			this.textMedicationsIndicateNone.TabIndex = 201;
			// 
			// label9
			// 
			this.label9.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label9.Location = new System.Drawing.Point(19,136);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(246,16);
			this.label9.TabIndex = 200;
			this.label9.Text = "Indicator that patient has No Medications";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butProblemsIndicateNone
			// 
			this.butProblemsIndicateNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butProblemsIndicateNone.Autosize = true;
			this.butProblemsIndicateNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butProblemsIndicateNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butProblemsIndicateNone.CornerRadius = 4F;
			this.butProblemsIndicateNone.Location = new System.Drawing.Point(419,109);
			this.butProblemsIndicateNone.Name = "butProblemsIndicateNone";
			this.butProblemsIndicateNone.Size = new System.Drawing.Size(22,21);
			this.butProblemsIndicateNone.TabIndex = 199;
			this.butProblemsIndicateNone.Text = "...";
			this.butProblemsIndicateNone.Click += new System.EventHandler(this.butProblemsIndicateNone_Click);
			// 
			// textProblemsIndicateNone
			// 
			this.textProblemsIndicateNone.Location = new System.Drawing.Point(270,110);
			this.textProblemsIndicateNone.Name = "textProblemsIndicateNone";
			this.textProblemsIndicateNone.ReadOnly = true;
			this.textProblemsIndicateNone.Size = new System.Drawing.Size(145,20);
			this.textProblemsIndicateNone.TabIndex = 198;
			// 
			// label8
			// 
			this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label8.Location = new System.Drawing.Point(19,113);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(246,16);
			this.label8.TabIndex = 197;
			this.label8.Text = "Indicator that patient has No Problems";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tabImages
			// 
			this.tabImages.Controls.Add(this.checkImagesModuleTreeIsCollapsed);
			this.tabImages.Location = new System.Drawing.Point(4,22);
			this.tabImages.Name = "tabImages";
			this.tabImages.Size = new System.Drawing.Size(466,438);
			this.tabImages.TabIndex = 5;
			this.tabImages.Text = "Images";
			this.tabImages.UseVisualStyleBackColor = true;
			// 
			// tabManage
			// 
			this.tabManage.Controls.Add(this.checkRxSendNewToQueue);
			this.tabManage.Location = new System.Drawing.Point(4,22);
			this.tabManage.Name = "tabManage";
			this.tabManage.Size = new System.Drawing.Size(466,438);
			this.tabManage.TabIndex = 6;
			this.tabManage.Text = "Manage";
			this.tabManage.UseVisualStyleBackColor = true;
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
			this.butCancel.Location = new System.Drawing.Point(441,498);
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
			this.butOK.Location = new System.Drawing.Point(336,498);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 7;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butAllergiesIndicateNone
			// 
			this.butAllergiesIndicateNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAllergiesIndicateNone.Autosize = true;
			this.butAllergiesIndicateNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAllergiesIndicateNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAllergiesIndicateNone.CornerRadius = 4F;
			this.butAllergiesIndicateNone.Location = new System.Drawing.Point(419,155);
			this.butAllergiesIndicateNone.Name = "butAllergiesIndicateNone";
			this.butAllergiesIndicateNone.Size = new System.Drawing.Size(22,21);
			this.butAllergiesIndicateNone.TabIndex = 205;
			this.butAllergiesIndicateNone.Text = "...";
			this.butAllergiesIndicateNone.Click += new System.EventHandler(this.butAllergiesIndicateNone_Click);
			// 
			// textAllergiesIndicateNone
			// 
			this.textAllergiesIndicateNone.Location = new System.Drawing.Point(270,156);
			this.textAllergiesIndicateNone.Name = "textAllergiesIndicateNone";
			this.textAllergiesIndicateNone.ReadOnly = true;
			this.textAllergiesIndicateNone.Size = new System.Drawing.Size(145,20);
			this.textAllergiesIndicateNone.TabIndex = 204;
			// 
			// label14
			// 
			this.label14.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label14.Location = new System.Drawing.Point(19,159);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(246,16);
			this.label14.TabIndex = 203;
			this.label14.Text = "Indicator that patient has No Allergies";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormModuleSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(543,535);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormModuleSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Module Setup";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormModuleSetup_FormClosing);
			this.Load += new System.EventHandler(this.FormModuleSetup_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabAppts.ResumeLayout(false);
			this.tabFamily.ResumeLayout(false);
			this.tabAccount.ResumeLayout(false);
			this.tabAccount.PerformLayout();
			this.tabTreatPlan.ResumeLayout(false);
			this.tabTreatPlan.PerformLayout();
			this.tabChart.ResumeLayout(false);
			this.tabChart.PerformLayout();
			this.tabImages.ResumeLayout(false);
			this.tabManage.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormModuleSetup_Load(object sender, System.EventArgs e) {
			changed=false;
			//Appointment module---------------------------------------------------------------
			checkSolidBlockouts.Checked=PrefC.GetBool(PrefName.SolidBlockouts);
			checkBrokenApptNote.Checked=PrefC.GetBool(PrefName.BrokenApptCommLogNotAdjustment);
			checkApptBubbleDelay.Checked = PrefC.GetBool(PrefName.ApptBubbleDelay);
			checkAppointmentBubblesDisabled.Checked=PrefC.GetBool(PrefName.AppointmentBubblesDisabled);
			posAdjTypes=DefC.GetPositiveAdjTypes();
			for(int i=0;i<posAdjTypes.Count;i++){
				comboFinanceChargeAdjType.Items.Add(posAdjTypes[i].ItemName);
				if(PrefC.GetLong(PrefName.FinanceChargeAdjustmentType)==posAdjTypes[i].DefNum){
					comboFinanceChargeAdjType.SelectedIndex=i;
				}
				comboBillingChargeAdjType.Items.Add(posAdjTypes[i].ItemName);
				if(PrefC.GetLong(PrefName.BillingChargeAdjustmentType)==posAdjTypes[i].DefNum) {
					comboBillingChargeAdjType.SelectedIndex=i;
				}
				comboBrokenApptAdjType.Items.Add(posAdjTypes[i].ItemName);
				if(PrefC.GetLong(PrefName.BrokenAppointmentAdjustmentType)==posAdjTypes[i].DefNum) {
					comboBrokenApptAdjType.SelectedIndex=i;
				}
			}
			checkApptExclamation.Checked=PrefC.GetBool(PrefName.ApptExclamationShowForUnsentIns);
			comboTimeArrived.Items.Add(Lan.g(this,"none"));
			comboTimeArrived.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.ApptConfirmed].Length;i++){
				comboTimeArrived.Items.Add(DefC.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(DefC.Short[(int)DefCat.ApptConfirmed][i].DefNum==PrefC.GetLong(PrefName.AppointmentTimeArrivedTrigger)){
					comboTimeArrived.SelectedIndex=i+1;
				}
			}
			comboTimeSeated.Items.Add(Lan.g(this,"none"));
			comboTimeSeated.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.ApptConfirmed].Length;i++){
				comboTimeSeated.Items.Add(DefC.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(DefC.Short[(int)DefCat.ApptConfirmed][i].DefNum==PrefC.GetLong(PrefName.AppointmentTimeSeatedTrigger)){
					comboTimeSeated.SelectedIndex=i+1;
				}
			}
			comboTimeDismissed.Items.Add(Lan.g(this,"none"));
			comboTimeDismissed.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.ApptConfirmed].Length;i++){
				comboTimeDismissed.Items.Add(DefC.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(DefC.Short[(int)DefCat.ApptConfirmed][i].DefNum==PrefC.GetLong(PrefName.AppointmentTimeDismissedTrigger)){
					comboTimeDismissed.SelectedIndex=i+1;
				}
			}
			checkApptRefreshEveryMinute.Checked=PrefC.GetBool(PrefName.ApptModuleRefreshesEveryMinute);
			//Family module-----------------------------------------------------------------------
			//checkMedicalEclaimsEnabled.Checked=PrefC.GetBool(PrefName.MedicalEclaimsEnabled);
			checkInsurancePlansShared.Checked=PrefC.GetBool(PrefName.InsurancePlansShared);
			checkPPOpercentage.Checked=PrefC.GetBool(PrefName.InsDefaultPPOpercent);
			checkAllowedFeeSchedsAutomate.Checked=PrefC.GetBool(PrefName.AllowedFeeSchedsAutomate);
			checkCoPayFeeScheduleBlankLikeZero.Checked=PrefC.GetBool(PrefName.CoPay_FeeSchedule_BlankLikeZero);
			checkInsDefaultShowUCRonClaims.Checked=PrefC.GetBool(PrefName.InsDefaultShowUCRonClaims);
			//Image module-----------------------------------------------------------------------
			checkImagesModuleTreeIsCollapsed.Checked=PrefC.GetBool(PrefName.ImagesModuleTreeIsCollapsed);
			//Manage module
			checkRxSendNewToQueue.Checked=PrefC.GetBool(PrefName.RxSendNewToQueue);
			//Account module-----------------------------------------------------------------------
			checkStatementShowReturnAddress.Checked=PrefC.GetBool(PrefName.StatementShowReturnAddress);
			checkShowCC.Checked=PrefC.GetBool(PrefName.StatementShowCreditCard);
			checkStatementShowNotes.Checked=PrefC.GetBool(PrefName.StatementShowNotes);
			checkStatementShowProcBreakdown.Checked=PrefC.GetBool(PrefName.StatementShowProcBreakdown);
			comboUseChartNum.Items.Add(Lan.g(this,"PatNum"));
			comboUseChartNum.Items.Add(Lan.g(this,"ChartNumber"));
			if(PrefC.GetBool(PrefName.StatementAccountsUseChartNumber)){
				comboUseChartNum.SelectedIndex=1;
			}
			else{
				comboUseChartNum.SelectedIndex=0;
			}
			if(PrefC.GetLong(PrefName.StatementsCalcDueDate)!=-1){
				textStatementsCalcDueDate.Text=PrefC.GetLong(PrefName.StatementsCalcDueDate).ToString();
			}
			textPayPlansBillInAdvanceDays.Text=PrefC.GetLong(PrefName.PayPlansBillInAdvanceDays).ToString();
			checkBalancesDontSubtractIns.Checked=PrefC.GetBool(PrefName.BalancesDontSubtractIns);
			checkAgingMonthly.Checked=PrefC.GetBool(PrefName.AgingCalculatedMonthlyInsteadOfDaily);
			checkEclaimsSeparateTreatProv.Checked=PrefC.GetBool(PrefName.EclaimsSeparateTreatProv);
			checkStoreCCnumbers.Checked=PrefC.GetBool(PrefName.StoreCCnumbers);
			checkStoreCCTokens.Checked=PrefC.GetBool(PrefName.StoreCCtokens);
			checkProviderIncomeShows.Checked=PrefC.GetBool(PrefName.ProviderIncomeTransferShows);
			textClaimAttachPath.Text=PrefC.GetString(PrefName.ClaimAttachExportPath);
			checkShowFamilyCommByDefault.Checked=PrefC.GetBool(PrefName.ShowAccountFamilyCommEntries);
			checkClaimFormTreatDentSaysSigOnFile.Checked=PrefC.GetBool(PrefName.ClaimFormTreatDentSaysSigOnFile);
			checkStatementSummaryShowInsInfo.Checked=PrefC.GetBool(PrefName.StatementSummaryShowInsInfo);
			checkIntermingleDefault.Checked=PrefC.GetBool(PrefName.IntermingleFamilyDefault);
			checkClaimsValidateACN.Checked=PrefC.GetBool(PrefName.ClaimsValidateACN);
			//TP module-----------------------------------------------------------------------
			textTreatNote.Text=PrefC.GetString(PrefName.TreatmentPlanNote);
			checkTreatPlanShowGraphics.Checked=PrefC.GetBool(PrefName.TreatPlanShowGraphics);
			checkTreatPlanShowCompleted.Checked=PrefC.GetBool(PrefName.TreatPlanShowCompleted);
			//Chart module-----------------------------------------------------------------------
			comboToothNomenclature.Items.Add(Lan.g(this, "Universal (Common in the US, 1-32)"));
			comboToothNomenclature.Items.Add(Lan.g(this, "FDI Notation (International, 11-48)"));
			comboToothNomenclature.Items.Add(Lan.g(this, "Haderup (Danish)"));
			comboToothNomenclature.Items.Add(Lan.g(this, "Palmer (Ortho)"));
			comboToothNomenclature.SelectedIndex = PrefC.GetInt(PrefName.UseInternationalToothNumbers);
			checkAutoClearEntryStatus.Checked=PrefC.GetBool(PrefName.AutoResetTPEntryStatus);
			checkAllowSettingProcsComplete.Checked=PrefC.GetBool(PrefName.AllowSettingProcsComplete);
			checkChartQuickAddHideAmalgam.Checked=PrefC.GetBool(PrefName.ChartQuickAddHideAmalgam);
			checkToothChartMoveMenuToRight.Checked=PrefC.GetBool(PrefName.ToothChartMoveMenuToRight);
			textProblemsIndicateNone.Text=DiseaseDefs.GetName(PrefC.GetLong(PrefName.ProblemsIndicateNone));
			textMedicationsIndicateNone.Text=Medications.GetDescription(PrefC.GetLong(PrefName.MedicationsIndicateNone));
			textAllergiesIndicateNone.Text=AllergyDefs.GetOne(PrefC.GetLong(PrefName.AllergiesIndicateNone)).Description;
		}

		private void checkAllowedFeeSchedsAutomate_Click(object sender,EventArgs e) {
			if(!checkAllowedFeeSchedsAutomate.Checked){
				return;
			}
			if(!MsgBox.Show(this,true,"Allowed fee schedules will now be set up for all insurance plans that do not already have one.\r\nThe name of each fee schedule will exactly match the name of the carrier.\r\nOnce created, allowed fee schedules can be easily managed from the fee schedules window.\r\nContinue?")){
				checkAllowedFeeSchedsAutomate.Checked=false;
				return;
			}
			Cursor=Cursors.WaitCursor;
			long schedsAdded=InsPlans.GenerateAllowedFeeSchedules();
			Cursor=Cursors.Default;
			MessageBox.Show(Lan.g(this,"Done.  Allowed fee schedules added: ")+schedsAdded.ToString());
			DataValid.SetInvalid(InvalidType.FeeScheds);
		}

		private void checkInsDefaultShowUCRonClaims_Click(object sender,EventArgs e) {
			if(!checkInsDefaultShowUCRonClaims.Checked) {
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Would you like to immediately change all category percentage plans to show office UCR fees on claims?")) {
				return;
			}
			long plansAffected=InsPlans.SetAllPlansToShowUCR();
			MessageBox.Show(Lan.g(this,"Plans affected: ")+plansAffected.ToString());
		}

		private void butProblemsIndicateNone_Click(object sender,EventArgs e) {
			FormDiseaseDefs formD=new FormDiseaseDefs();
			formD.IsSelectionMode=true;
			formD.ShowDialog();
			if(formD.DialogResult!=DialogResult.OK) {
				return;
			}
			if(Prefs.UpdateLong(PrefName.ProblemsIndicateNone,formD.SelectedDiseaseDefNum)) {
				changed=true;
			}
			textProblemsIndicateNone.Text=DiseaseDefs.GetName(formD.SelectedDiseaseDefNum);
		}

		private void butMedicationsIndicateNone_Click(object sender,EventArgs e) {
			FormMedications formM=new FormMedications();
			formM.IsSelectionMode=true;
			formM.ShowDialog();
			if(formM.DialogResult!=DialogResult.OK) {
				return;
			}
			if(Prefs.UpdateLong(PrefName.MedicationsIndicateNone,formM.SelectedMedicationNum)) {
				changed=true;
			}
			textMedicationsIndicateNone.Text=Medications.GetDescription(formM.SelectedMedicationNum);
		}

		private void butAllergiesIndicateNone_Click(object sender,EventArgs e) {
			FormAllergySetup formA=new FormAllergySetup();
			formA.IsSelectionMode=true;
			formA.ShowDialog();
			if(formA.DialogResult!=DialogResult.OK) {
				return;
			}
			if(Prefs.UpdateLong(PrefName.AllergiesIndicateNone,formA.SelectedAllergyDefNum)) {
				changed=true;
			}
			textAllergiesIndicateNone.Text=AllergyDefs.GetOne(formA.SelectedAllergyDefNum).Description;
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
			if( Prefs.UpdateString(PrefName.TreatmentPlanNote,textTreatNote.Text)
				| Prefs.UpdateBool(PrefName.TreatPlanShowGraphics,checkTreatPlanShowGraphics.Checked)
				| Prefs.UpdateBool(PrefName.TreatPlanShowCompleted,checkTreatPlanShowCompleted.Checked)
				| Prefs.UpdateBool(PrefName.StatementShowReturnAddress,checkStatementShowReturnAddress.Checked)
				| Prefs.UpdateBool(PrefName.StatementShowCreditCard,checkShowCC.Checked)
				| Prefs.UpdateBool(PrefName.StatementShowNotes,checkStatementShowNotes.Checked)
				| Prefs.UpdateBool(PrefName.StatementShowProcBreakdown,checkStatementShowProcBreakdown.Checked)
				| Prefs.UpdateBool(PrefName.StatementAccountsUseChartNumber,comboUseChartNum.SelectedIndex==1)
				| Prefs.UpdateBool(PrefName.BalancesDontSubtractIns,checkBalancesDontSubtractIns.Checked)
				| Prefs.UpdateLong(PrefName.PayPlansBillInAdvanceDays,PIn.Long(textPayPlansBillInAdvanceDays.Text))
				| Prefs.UpdateBool(PrefName.AgingCalculatedMonthlyInsteadOfDaily,checkAgingMonthly.Checked)
				| Prefs.UpdateBool(PrefName.EclaimsSeparateTreatProv,checkEclaimsSeparateTreatProv.Checked)
				| Prefs.UpdateBool(PrefName.MedicalEclaimsEnabled,checkMedicalEclaimsEnabled.Checked)
				| Prefs.UpdateLong(PrefName.UseInternationalToothNumbers, comboToothNomenclature.SelectedIndex)
				| Prefs.UpdateBool(PrefName.InsurancePlansShared,checkInsurancePlansShared.Checked)
				| Prefs.UpdateBool(PrefName.SolidBlockouts, checkSolidBlockouts.Checked)
				| Prefs.UpdateBool(PrefName.StoreCCnumbers, checkStoreCCnumbers.Checked)
				| Prefs.UpdateBool(PrefName.StoreCCtokens, checkStoreCCTokens.Checked)
				| Prefs.UpdateBool(PrefName.BrokenApptCommLogNotAdjustment, checkBrokenApptNote.Checked)
				| Prefs.UpdateBool(PrefName.ApptBubbleDelay, checkApptBubbleDelay.Checked)
				| Prefs.UpdateBool(PrefName.AppointmentBubblesDisabled, checkAppointmentBubblesDisabled.Checked)
				| Prefs.UpdateLong(PrefName.FinanceChargeAdjustmentType,posAdjTypes[comboFinanceChargeAdjType.SelectedIndex].DefNum)
				| Prefs.UpdateLong(PrefName.BillingChargeAdjustmentType,posAdjTypes[comboBillingChargeAdjType.SelectedIndex].DefNum)
				| Prefs.UpdateLong(PrefName.BrokenAppointmentAdjustmentType,posAdjTypes[comboBrokenApptAdjType.SelectedIndex].DefNum)
				| Prefs.UpdateBool(PrefName.ApptExclamationShowForUnsentIns, checkApptExclamation.Checked)
				| Prefs.UpdateBool(PrefName.ProviderIncomeTransferShows, checkProviderIncomeShows.Checked)
				| Prefs.UpdateString(PrefName.ClaimAttachExportPath,textClaimAttachPath.Text)
				| Prefs.UpdateBool(PrefName.AutoResetTPEntryStatus,checkAutoClearEntryStatus.Checked)
				| Prefs.UpdateBool(PrefName.AllowSettingProcsComplete,checkAllowSettingProcsComplete.Checked)
				| Prefs.UpdateBool(PrefName.ShowAccountFamilyCommEntries,checkShowFamilyCommByDefault.Checked)
				| Prefs.UpdateBool(PrefName.InsDefaultPPOpercent,checkPPOpercentage.Checked)
				| Prefs.UpdateBool(PrefName.ClaimFormTreatDentSaysSigOnFile,checkClaimFormTreatDentSaysSigOnFile.Checked)
				| Prefs.UpdateBool(PrefName.StatementSummaryShowInsInfo, checkStatementSummaryShowInsInfo.Checked)
				| Prefs.UpdateBool(PrefName.ApptModuleRefreshesEveryMinute, checkApptRefreshEveryMinute.Checked)
				| Prefs.UpdateBool(PrefName.ChartQuickAddHideAmalgam, checkChartQuickAddHideAmalgam.Checked)
				| Prefs.UpdateBool(PrefName.AllowedFeeSchedsAutomate,checkAllowedFeeSchedsAutomate.Checked)
				| Prefs.UpdateBool(PrefName.IntermingleFamilyDefault,checkIntermingleDefault.Checked)
				| Prefs.UpdateBool(PrefName.CoPay_FeeSchedule_BlankLikeZero,checkCoPayFeeScheduleBlankLikeZero.Checked)
				| Prefs.UpdateBool(PrefName.InsDefaultShowUCRonClaims,checkInsDefaultShowUCRonClaims.Checked)
				| Prefs.UpdateBool(PrefName.ClaimsValidateACN,checkClaimsValidateACN.Checked)
				| Prefs.UpdateBool(PrefName.ToothChartMoveMenuToRight,checkToothChartMoveMenuToRight.Checked)
				| Prefs.UpdateBool(PrefName.ImagesModuleTreeIsCollapsed,checkImagesModuleTreeIsCollapsed.Checked)
				| Prefs.UpdateBool(PrefName.RxSendNewToQueue,checkRxSendNewToQueue.Checked)
				)
			{
				changed=true;
			}
			if(textStatementsCalcDueDate.Text==""){
				if(Prefs.UpdateLong(PrefName.StatementsCalcDueDate,-1)){
					changed=true;
				}
			}
			else{
				if(Prefs.UpdateLong(PrefName.StatementsCalcDueDate,PIn.Long(textStatementsCalcDueDate.Text))){
					changed=true;
				}
			}
			long timeArrivedTrigger=0;
			if(comboTimeArrived.SelectedIndex>0){
				timeArrivedTrigger=DefC.Short[(int)DefCat.ApptConfirmed][comboTimeArrived.SelectedIndex-1].DefNum;
			}
			if(Prefs.UpdateLong(PrefName.AppointmentTimeArrivedTrigger,timeArrivedTrigger)){
				changed=true;
			}
			long timeSeatedTrigger=0;
			if(comboTimeSeated.SelectedIndex>0){
				timeSeatedTrigger=DefC.Short[(int)DefCat.ApptConfirmed][comboTimeSeated.SelectedIndex-1].DefNum;
			}
			if(Prefs.UpdateLong(PrefName.AppointmentTimeSeatedTrigger,timeSeatedTrigger)){
				changed=true;
			}
			long timeDismissedTrigger=0;
			if(comboTimeDismissed.SelectedIndex>0){
				timeDismissedTrigger=DefC.Short[(int)DefCat.ApptConfirmed][comboTimeDismissed.SelectedIndex-1].DefNum;
			}
			if(Prefs.UpdateLong(PrefName.AppointmentTimeDismissedTrigger,timeDismissedTrigger)){
				changed=true;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormModuleSetup_FormClosing(object sender,FormClosingEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidType.Prefs);
			}
		}

		

		

		

		

		
	}
}






