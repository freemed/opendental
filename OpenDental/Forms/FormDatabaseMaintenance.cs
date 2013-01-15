using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	/// <summary>
	/// Summary description for FormCheckDatabase.
	/// </summary>
	public class FormDatabaseMaintenance:System.Windows.Forms.Form {
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textBox1;
		private OpenDental.UI.Button buttonCheck;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Drawing.Printing.PrintDocument pd2;
		private TextBox textLog;
		private Label label1;
		private CheckBox checkShow;
		private UI.Button butFix;
		private GroupBox groupBox1;
		private Label label6;
		private UI.Button butConvertDb;
		private Label label5;
		private Label label4;
		private Label label3;
		private Label label2;
		private UI.Button butSpecChar;
		private UI.Button butApptProcs;
		private UI.Button butOptimize;
		private UI.Button butInsPayFix;
		private Label label7;
		private UI.Button butTokens;
		private OpenDental.UI.Button butPrint;
		///<summary>Holds any text from the log that still needs to be printed when the log spans multiple pages.</summary>
		private string LogTextPrint;

		///<summary></summary>
		public FormDatabaseMaintenance() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C(this,new System.Windows.Forms.Control[]{
				this.textBox1,
				//this.textBox2
			});
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDatabaseMaintenance));
			this.butClose = new OpenDental.UI.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.buttonCheck = new OpenDental.UI.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.textLog = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkShow = new System.Windows.Forms.CheckBox();
			this.butPrint = new OpenDental.UI.Button();
			this.butFix = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.butTokens = new OpenDental.UI.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.butConvertDb = new OpenDental.UI.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butSpecChar = new OpenDental.UI.Button();
			this.butApptProcs = new OpenDental.UI.Button();
			this.butOptimize = new OpenDental.UI.Button();
			this.butInsPayFix = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(874,671);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(27,12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(847,24);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "This tool will check the entire database for any improper settings, inconsistenci" +
    "es, or corruption.  If any problems are found, they will be fixed.";
			// 
			// buttonCheck
			// 
			this.buttonCheck.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCheck.Autosize = true;
			this.buttonCheck.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonCheck.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonCheck.CornerRadius = 4F;
			this.buttonCheck.Location = new System.Drawing.Point(670,671);
			this.buttonCheck.Name = "buttonCheck";
			this.buttonCheck.Size = new System.Drawing.Size(75,26);
			this.buttonCheck.TabIndex = 5;
			this.buttonCheck.Text = "C&heck";
			this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
			// 
			// textLog
			// 
			this.textLog.Font = new System.Drawing.Font("Courier New",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textLog.Location = new System.Drawing.Point(27,77);
			this.textLog.Multiline = true;
			this.textLog.Name = "textLog";
			this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textLog.Size = new System.Drawing.Size(931,447);
			this.textLog.TabIndex = 14;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24,53);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(361,20);
			this.label1.TabIndex = 15;
			this.label1.Text = "Log (automatically saved in RepairLog.txt if user has permission)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkShow
			// 
			this.checkShow.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShow.Location = new System.Drawing.Point(27,35);
			this.checkShow.Name = "checkShow";
			this.checkShow.Size = new System.Drawing.Size(847,20);
			this.checkShow.TabIndex = 16;
			this.checkShow.Text = "Show me everything in the log  (only for advanced users)";
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(532,671);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(87,26);
			this.butPrint.TabIndex = 18;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butFix
			// 
			this.butFix.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butFix.Autosize = true;
			this.butFix.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFix.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFix.CornerRadius = 4F;
			this.butFix.Location = new System.Drawing.Point(750,671);
			this.butFix.Name = "butFix";
			this.butFix.Size = new System.Drawing.Size(75,26);
			this.butFix.TabIndex = 20;
			this.butFix.Text = "Fix";
			this.butFix.Click += new System.EventHandler(this.butFix_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.butTokens);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.butConvertDb);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.butSpecChar);
			this.groupBox1.Controls.Add(this.butApptProcs);
			this.groupBox1.Controls.Add(this.butOptimize);
			this.groupBox1.Controls.Add(this.butInsPayFix);
			this.groupBox1.Location = new System.Drawing.Point(27,528);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(470,178);
			this.groupBox1.TabIndex = 31;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Database Tools";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(103,150);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(355,20);
			this.label7.TabIndex = 42;
			this.label7.Text = "Validates tokens on file with the X-Charge server.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butTokens
			// 
			this.butTokens.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTokens.Autosize = true;
			this.butTokens.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTokens.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTokens.CornerRadius = 4F;
			this.butTokens.Location = new System.Drawing.Point(10,146);
			this.butTokens.Name = "butTokens";
			this.butTokens.Size = new System.Drawing.Size(87,26);
			this.butTokens.TabIndex = 41;
			this.butTokens.Text = "Tokens";
			this.butTokens.Click += new System.EventHandler(this.butTokens_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(103,123);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(355,20);
			this.label6.TabIndex = 40;
			this.label6.Text = "Converts database storage engine to/from InnoDb.";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butConvertDb
			// 
			this.butConvertDb.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butConvertDb.Autosize = true;
			this.butConvertDb.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butConvertDb.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butConvertDb.CornerRadius = 4F;
			this.butConvertDb.Location = new System.Drawing.Point(10,120);
			this.butConvertDb.Name = "butConvertDb";
			this.butConvertDb.Size = new System.Drawing.Size(87,26);
			this.butConvertDb.TabIndex = 39;
			this.butConvertDb.Text = "InnoDb";
			this.butConvertDb.Click += new System.EventHandler(this.butConvertDb_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(103,97);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(355,20);
			this.label5.TabIndex = 38;
			this.label5.Text = "Removes special characters from appt notes and appt proc descriptions.";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(103,71);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(355,20);
			this.label4.TabIndex = 37;
			this.label4.Text = "Fixes procs in the Appt module that aren\'t correctly showing tooth nums.";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(103,45);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(355,20);
			this.label3.TabIndex = 36;
			this.label3.Text = "Repairs and Optimizes tables.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(103,19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(355,20);
			this.label2.TabIndex = 35;
			this.label2.Text = "Creates checks for insurance payments that are not attached to a check.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butSpecChar
			// 
			this.butSpecChar.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSpecChar.Autosize = true;
			this.butSpecChar.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSpecChar.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSpecChar.CornerRadius = 4F;
			this.butSpecChar.Location = new System.Drawing.Point(10,94);
			this.butSpecChar.Name = "butSpecChar";
			this.butSpecChar.Size = new System.Drawing.Size(87,26);
			this.butSpecChar.TabIndex = 33;
			this.butSpecChar.Text = "Spec Char";
			this.butSpecChar.Click += new System.EventHandler(this.butSpecChar_Click);
			// 
			// butApptProcs
			// 
			this.butApptProcs.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butApptProcs.Autosize = true;
			this.butApptProcs.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butApptProcs.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butApptProcs.CornerRadius = 4F;
			this.butApptProcs.Location = new System.Drawing.Point(10,68);
			this.butApptProcs.Name = "butApptProcs";
			this.butApptProcs.Size = new System.Drawing.Size(87,26);
			this.butApptProcs.TabIndex = 34;
			this.butApptProcs.Text = "Appt Procs";
			this.butApptProcs.Click += new System.EventHandler(this.butApptProcs_Click);
			// 
			// butOptimize
			// 
			this.butOptimize.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOptimize.Autosize = true;
			this.butOptimize.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOptimize.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOptimize.CornerRadius = 4F;
			this.butOptimize.Location = new System.Drawing.Point(10,42);
			this.butOptimize.Name = "butOptimize";
			this.butOptimize.Size = new System.Drawing.Size(87,26);
			this.butOptimize.TabIndex = 32;
			this.butOptimize.Text = "Optimize";
			this.butOptimize.Click += new System.EventHandler(this.butOptimize_Click);
			// 
			// butInsPayFix
			// 
			this.butInsPayFix.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butInsPayFix.Autosize = true;
			this.butInsPayFix.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInsPayFix.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInsPayFix.CornerRadius = 4F;
			this.butInsPayFix.Location = new System.Drawing.Point(10,16);
			this.butInsPayFix.Name = "butInsPayFix";
			this.butInsPayFix.Size = new System.Drawing.Size(87,26);
			this.butInsPayFix.TabIndex = 31;
			this.butInsPayFix.Text = "Ins Pay Fix";
			this.butInsPayFix.Click += new System.EventHandler(this.butInsPayFix_Click);
			// 
			// FormDatabaseMaintenance
			// 
			this.AcceptButton = this.buttonCheck;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(982,707);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butFix);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.checkShow);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textLog);
			this.Controls.Add(this.buttonCheck);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDatabaseMaintenance";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Database Maintenance";
			this.Load += new System.EventHandler(this.FormDatabaseMaintenance_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void butClose_Click(object sender,System.EventArgs e) {
			Close();
		}

		private void FormDatabaseMaintenance_Load(object sender,System.EventArgs e) {

		}

		private void butOptimize_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			textLog.Text=DateTime.Now.ToString()+"\r\n";
			DatabaseMaintenance.RepairAndOptimize();
			textLog.Text+=Lan.g("FormDatabaseMaintenance","Optimization Done");
			SaveLogToFile();
			Cursor=Cursors.Default;
		}

		private void buttonCheck_Click(object sender,System.EventArgs e) {
			Run(true);
		}

		private void butFix_Click(object sender,EventArgs e) {
			Run(false);
		}

		private void Run(bool isCheck){
			Cursor=Cursors.WaitCursor;
			bool verbose=checkShow.Checked;
			StringBuilder strB=new StringBuilder();
			strB.Append('-',65);
			textLog.Text=DateTime.Now.ToString()+strB.ToString()+"\r\n";
			Application.DoEvents();
//#if DEBUG
//      textLog.Text+=DatabaseMaintenance.AppointmentSpecialCharactersInNotes(verbose,isCheck);
//      Application.DoEvents();
//      textLog.Text+=Lan.g("FormDatabaseMaintenance","Done");
//      SaveLogToFile();
//      Cursor=Cursors.Default;
//      return;
//#endif
			textLog.Text+=DatabaseMaintenance.MySQLTables(verbose,isCheck);
			if(!DatabaseMaintenance.GetSuccess()) {
				Cursor=Cursors.Default;
				return;
			}
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.DatesNoZeros(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.DecimalValues(verbose,isCheck);
			Application.DoEvents();
			//Now, methods that apply to specific tables----------------------------------------------------------------------------
			textLog.Text+=DatabaseMaintenance.AppointmentsNoPattern(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.AppointmentsNoDateOrProcs(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.AppointmentsNoPatients(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.AppointmentPlannedNoPlannedApt(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.AppointmentSpecialCharactersInNotes(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.AutoCodesDeleteWithNoItems(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.AutomationTriggersWithNoSheetDefs(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.BillingTypesInvalid(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.CanadaCarriersCdaMissingInfo(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimDeleteWithNoClaimProcs(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimWithInvalidInsSubNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimWriteoffSum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimPaymentCheckAmt(verbose,isCheck);//also fixes resulting deposit misbalances.
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimPaymentDetachMissingDeposit(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDateNotMatchCapComplete(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDateNotMatchPayment(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcWithInvalidClaimNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDeleteWithInvalidClaimNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDeleteMismatchPatNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDeleteEstimateWithInvalidProcNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDeleteCapEstimateWithProcComplete(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcEstNoBillIns(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcEstWithInsPaidAmt(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcPatNumMissing(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcProvNumMissing(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcPreauthNotMatchClaim(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcStatusNotMatchClaim(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcWithInvalidClaimPaymentNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcWriteOffNegative(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClockEventInFuture(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.DocumentWithNoCategory(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.FeeDeleteDuplicates(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.InsPlanInvalidCarrier(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.InsPlanNoClaimForm(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.InsPlanInvalidNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.InsSubInvalidSubscriber(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.InsSubNumMismatchPlanNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.LabCaseWithInvalidLaboratory(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.LaboratoryWithInvalidSlip(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.MedicationPatDeleteWithInvalidMedNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.MessageButtonDuplicateButtonIndex(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatFieldsDeleteDuplicates(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatientBadGuarantor(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatientDeletedWithClinicNumSet(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatientsNoClinicSet(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatientPriProvHidden(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatientPriProvMissing(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatientUnDeleteWithBalance(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatPlanDeleteWithInvalidInsSubNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatPlanDeleteWithInvalidPatNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatPlanOrdinalDuplicates(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatPlanOrdinalZeroToOne(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatPlanOrdinalTwoToOne(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PaymentDetachMissingDeposit(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PaymentMissingPaySplit(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PayPlanChargeGuarantorMatch(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PayPlanChargeProvNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PayPlanSetGuarantorToPatForIns(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PaySplitWithInvalidPayNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PaySplitAttachedToPayPlan(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PerioMeasureWithInvalidIntTooth(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PreferenceDateDepositsStarted(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PreferencePracticeProv(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcButtonItemsDeleteWithInvalidAutoCode(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurecodeCategoryNotSet(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogAttachedToApptWithProcStatusDeleted(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogAttachedToWrongAppts(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogAttachedToWrongApptDate(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogBaseUnitsZero(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogCodeNumInvalid(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogLabAttachedToDeletedProc(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogProvNumMissing(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogToothNums(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogTpAttachedToClaim(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogTpAttachedToCompleteLabFeesCanada(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogUnitQtyZero(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProviderHiddenWithClaimPayments(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProviderWithInvalidFeeSched(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.RecallDuplicatesWarn(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.RecallTriggerDeleteBadCodeNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.RefAttachDeleteWithInvalidReferral(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.SchedulesDeleteHiddenProviders(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.SchedulesDeleteShort(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.SchedulesDeleteProvClosed(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.SignalInFuture(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.StatementDateRangeMax(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.TaskSubscriptionsInvalid(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.TimeCardRuleEmployeeNumInvalid(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=Lan.g("FormDatabaseMaintenance","Done");
			SaveLogToFile();
			Cursor=Cursors.Default;
		}

		private void SaveLogToFile() {
			string path=CodeBase.ODFileUtils.CombinePaths(Application.StartupPath,"RepairLog.txt");
			try {
				File.AppendAllText(path,textLog.Text);
			}
			catch(SecurityException) {
				MsgBox.Show(this,"Log not saved to Repairlog.txt because user does not have permission to access that file.");
			}
			catch(UnauthorizedAccessException) {
				MsgBox.Show(this,"Log not saved to Repairlog.txt because user does not have permission to access that file.");
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butPrint_Click(object sender,EventArgs e) {
			LogTextPrint=textLog.Text;
			pd2 = new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.DefaultPageSettings.Margins=new Margins(40,50,50,60);
			try {
				#if DEBUG
				FormPrintPreview printPreview=new FormPrintPreview(PrintSituation.Default,pd2,0);
				printPreview.ShowDialog();
				#else
				pd2.Print();
				#endif
			}
			catch {
				MessageBox.Show("Printer not available");
			}
		}

		private void pd2_PrintPage(object sender,PrintPageEventArgs ev) {//raised for each page to be printed.
			int charsOnPage=0;
			int linesPerPage=0;
			Font font=new Font("Courier New",10);
			ev.Graphics.MeasureString(LogTextPrint,font,ev.MarginBounds.Size,StringFormat.GenericTypographic,out charsOnPage,out linesPerPage);
			ev.Graphics.DrawString(LogTextPrint,font,Brushes.Black,ev.MarginBounds,StringFormat.GenericTypographic);
			LogTextPrint=LogTextPrint.Substring(charsOnPage);
			ev.HasMorePages=(LogTextPrint.Length > 0);
		}

		private void butTemp_Click(object sender,EventArgs e) {
			FormDatabaseMaintTemp form=new FormDatabaseMaintTemp();
			form.ShowDialog();
		}

		private void butInsPayFix_Click(object sender,EventArgs e) {
			FormInsPayFix formIns=new FormInsPayFix();
			formIns.ShowDialog();
		}

		private void butApptProcs_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This will fix procedure descriptions in the Appt module that aren't correctly showing tooth numbers.\r\nContinue?")) 
			{
				return;
			}
			Cursor=Cursors.WaitCursor;
			//The ApptProcDescript region is also in FormProcEdit.SaveAndClose() and FormApptEdit.UpdateToDB()  Make any changes there as well.
			#region ApptProcDescript
			List<long> aptNums=new List<long>();
			Appointment[] aptList=Appointments.GetForPeriod(DateTime.Now.Date.AddMonths(-6),DateTime.MaxValue.AddDays(-10));
			for(int i=0;i<aptList.Length;i++) {
				aptNums.Add(aptList[i].AptNum);
			}
			List<Procedure> procsMultApts=Procedures.GetProcsMultApts(aptNums);
			for(int i=0;i<aptList.Length;i++) {
				Appointment newApt=aptList[i].Clone();
				newApt.ProcDescript="";
				Procedure[] procsForOne=Procedures.GetProcsOneApt(aptList[i].AptNum,procsMultApts);
				string procDescript="";
				for(int j=0;j<procsForOne.Length;j++) {
					ProcedureCode procCode=ProcedureCodes.GetProcCodeFromDb(procsForOne[j].CodeNum);
					if(j>0) {
						procDescript+=", ";
					}
					switch(procCode.TreatArea) {
						case TreatmentArea.Surf:
							procDescript+="#"+Tooth.GetToothLabel(procsForOne[j].ToothNum)+"-"
							+procsForOne[j].Surf+"-";//""#12-MOD-"
							break;
						case TreatmentArea.Tooth:
							procDescript+="#"+Tooth.GetToothLabel(procsForOne[j].ToothNum)+"-";//"#12-"
							break;
						case TreatmentArea.Quad:
							procDescript+=procsForOne[j].Surf+"-";//"UL-"
							break;
						case TreatmentArea.Sextant:
							procDescript+="S"+procsForOne[j].Surf+"-";//"S2-"
							break;
						case TreatmentArea.Arch:
							procDescript+=procsForOne[j].Surf+"-";//"U-"
							break;
						case TreatmentArea.ToothRange:
							break;
						default://area 3 or 0 (mouth)
							break;
					}
					procDescript+=procCode.AbbrDesc;
				}
				newApt.ProcDescript=procDescript;
				Appointments.Update(newApt,aptList[i]);
			}
			#endregion
			Cursor=Cursors.Default;
			MsgBox.Show(this,"Done. Please refresh Appt module to see the changes.");
		}

		private void butSpecChar_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This is only used if your mobile synch is failing.  This cannot be undone.  Do you wish to continue?")) {
				return;
			}
			InputBox box=new InputBox("In our online manual, on the database maintenance page, look for the password and enter it below.");
			if(box.ShowDialog()!=DialogResult.OK) {
				return;
			}
			if(box.textResult.Text!="fix") {
				MessageBox.Show("Wrong password.");
				return;
			}
			DatabaseMaintenance.FixSpecialCharacters();
			MsgBox.Show(this,"Special Characters have been removed from Appointment Notes and Appointment Procedure Descriptions.  Mobile synch should now function properly.");
		}

		private void butConvertDb_Click(object sender,EventArgs e) {
			FormInnoDb form=new FormInnoDb();
			form.ShowDialog();
		}

		private void butTokens_Click(object sender,EventArgs e) {
			FormXchargeTokenTool FormXCT=new FormXchargeTokenTool();
			FormXCT.ShowDialog();
		}

	}
}
