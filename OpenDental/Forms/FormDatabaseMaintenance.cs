using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

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
		private UI.Button butInsPayFix;
		private OpenDental.UI.Button butPrint;

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
			this.butInsPayFix = new OpenDental.UI.Button();
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
			this.butClose.Location = new System.Drawing.Point(787,631);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(87,26);
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
			this.buttonCheck.Location = new System.Drawing.Point(555,631);
			this.buttonCheck.Name = "buttonCheck";
			this.buttonCheck.Size = new System.Drawing.Size(87,26);
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
			this.textLog.Size = new System.Drawing.Size(847,543);
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
			this.butPrint.Location = new System.Drawing.Point(298,631);
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
			this.butFix.Location = new System.Drawing.Point(648,631);
			this.butFix.Name = "butFix";
			this.butFix.Size = new System.Drawing.Size(87,26);
			this.butFix.TabIndex = 20;
			this.butFix.Text = "Fix";
			this.butFix.Click += new System.EventHandler(this.butFix_Click);
			// 
			// butInsPayFix
			// 
			this.butInsPayFix.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butInsPayFix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butInsPayFix.Autosize = true;
			this.butInsPayFix.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInsPayFix.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInsPayFix.CornerRadius = 4F;
			this.butInsPayFix.Location = new System.Drawing.Point(27,631);
			this.butInsPayFix.Name = "butInsPayFix";
			this.butInsPayFix.Size = new System.Drawing.Size(87,26);
			this.butInsPayFix.TabIndex = 21;
			this.butInsPayFix.Text = "Ins Pay Fix";
			this.butInsPayFix.Click += new System.EventHandler(this.butInsPayFix_Click);
			// 
			// FormDatabaseMaintenance
			// 
			this.AcceptButton = this.buttonCheck;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(895,667);
			this.Controls.Add(this.butInsPayFix);
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
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void butClose_Click(object sender,System.EventArgs e) {
			Close();
		}

		private void FormDatabaseMaintenance_Load(object sender,System.EventArgs e) {

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
			strB.Append('-',90);
			textLog.Text=DateTime.Now.ToString()+strB.ToString()+"\r\n";
			Application.DoEvents();
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
			textLog.Text+=DatabaseMaintenance.AutoCodesDeleteWithNoItems(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.AutomationTriggersWithNoSheetDefs(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimDeleteWithNoClaimProcs(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimWriteoffSum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimPaymentCheckAmt(verbose,isCheck);//also fixes resulting deposit misbalances.
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimPaymentDeleteWithNoSplits(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDateNotMatchCapComplete(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDateNotMatchPayment(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcWithInvalidClaimNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDeleteWithInvalidClaimNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDeleteEstimateWithInvalidProcNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcEstNoBillIns(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcEstWithInsPaidAmt(verbose,isCheck);
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
			textLog.Text+=DatabaseMaintenance.InsPlanCheckNoCarrier(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.InsPlanNoClaimForm(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.InsPlanInvalidNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.InsSubNumMismatchPlanNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.LaboratoryWithInvalidSlip(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.MedicationPatDeleteWithInvalidMedNum(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatFieldsDeleteDuplicates(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatientBadGuarantor(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatientPriProvMissing(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatientUnDeleteWithBalance(verbose,isCheck);
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
			textLog.Text+=DatabaseMaintenance.PreferenceDateDepositsStarted(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PreferencePracticeBillingType(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PreferencePracticeProv(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcButtonItemsDeleteWithInvalidAutoCode(verbose,isCheck);
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
			textLog.Text+=DatabaseMaintenance.ProcedurelogProvNumMissing(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogToothNums(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogTpAttachedToClaim(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogUnitQtyZero(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProviderHiddenWithClaimPayments(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.RecallDuplicatesWarn(verbose,isCheck);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.RecallTriggerDeleteBadCodeNum(verbose,isCheck);
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
			pd2 = new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.DefaultPageSettings.Margins=new Margins(40,50,50,60);
			try {
				pd2.Print();
			}
			catch {
				MessageBox.Show("Printer not available");
			}
		}

		private void pd2_PrintPage(object sender,PrintPageEventArgs ev) {//raised for each page to be printed.
			int yPos = ev.MarginBounds.Top;
			int xPos=ev.MarginBounds.Left;
			ev.Graphics.DrawString(textLog.Text,new Font("Courier New",10),Brushes.Black,xPos,yPos);
			ev.HasMorePages = false;
		}

		private void butTemp_Click(object sender,EventArgs e) {
			FormDatabaseMaintTemp form=new FormDatabaseMaintTemp();
			form.ShowDialog();
		}

		private void butInsPayFix_Click(object sender,EventArgs e) {
			FormInsPayFix formIns=new FormInsPayFix();
			formIns.ShowDialog();
		}






	}
}
