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
		private OpenDental.UI.Button butTemp;
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
			this.butTemp = new OpenDental.UI.Button();
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
			this.buttonCheck.Location = new System.Drawing.Point(668,631);
			this.buttonCheck.Name = "buttonCheck";
			this.buttonCheck.Size = new System.Drawing.Size(87,26);
			this.buttonCheck.TabIndex = 5;
			this.buttonCheck.Text = "C&heck Now";
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
			// butTemp
			// 
			this.butTemp.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butTemp.Autosize = true;
			this.butTemp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTemp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTemp.CornerRadius = 4F;
			this.butTemp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butTemp.Location = new System.Drawing.Point(27,631);
			this.butTemp.Name = "butTemp";
			this.butTemp.Size = new System.Drawing.Size(87,26);
			this.butTemp.TabIndex = 19;
			this.butTemp.Text = "Temp Check";
			this.butTemp.Click += new System.EventHandler(this.butTemp_Click);
			// 
			// FormDatabaseMaintenance
			// 
			this.AcceptButton = this.buttonCheck;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(895,667);
			this.Controls.Add(this.butTemp);
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
			Cursor=Cursors.WaitCursor;
			bool verbose=checkShow.Checked;
			StringBuilder strB=new StringBuilder();
			strB.Append('-',90);
			textLog.Text=DateTime.Now.ToString()+strB.ToString()+"\r\n";
			Application.DoEvents();
			/*
			textLog.Text+=DatabaseMaintenance.MySQLTables(verbose);
			if(!DatabaseMaintenance.GetSuccess()) {
				Cursor=Cursors.Default;
				return;
			}
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.DatesNoZeros(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.DecimalValues(verbose);
			Application.DoEvents();
			//Now, methods that apply to specific tables----------------------------------------------------------------------------
			textLog.Text+=DatabaseMaintenance.AppointmentsNoPattern(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.AppointmentsNoDateOrProcs(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.AutoCodesDeleteWithNoItems(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimPlanNum2NotValid(verbose);
			Application.DoEvents();*/
			textLog.Text+=DatabaseMaintenance.ClaimDeleteWithInvalidPlanNums(verbose);
			Application.DoEvents();
			/*textLog.Text+=DatabaseMaintenance.ClaimDeleteWithNoClaimProcs(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimWriteoffSum(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimPaymentCheckAmt(verbose);//also fixes resulting deposit misbalances.
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimPaymentDeleteWithNoSplits(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDateNotMatchCapComplete(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDateNotMatchPayment(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDeleteDuplicates(verbose);//
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDeleteWithInvalidClaimNum(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDeleteWithInvalidPlanNum(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcDeleteWithInvalidProcNum(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcEstNoBillIns(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcEstWithInsPaidAmt(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcProvNumMissing(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcPreauthNotMatchClaim(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcStatusNotMatchClaim(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcWithInvalidClaimPaymentNum(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClaimProcWriteOffNegative(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ClockEventInFuture(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.DocumentWithNoCategory(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.InsPlanCheckNoCarrier(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.InsPlanNoClaimForm(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.MedicationPatDeleteWithInvalidMedNum(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatientBadGuarantor(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatientPriProvMissing(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatientUnDeleteWithBalance(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PatPlanOrdinalTwoToOne(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PaymentDetachMissingDeposit(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PayPlanChargeGuarantorMatch(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PayPlanChargeProvNum(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PayPlanSetGuarantorToPatForIns(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PaySplitDeleteWithInvalidPayNum(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PaySplitAttachedToPayPlan(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PreferenceDateDepositsStarted(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PreferencePracticeBillingType(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.PreferencePracticeProv(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcButtonItemsDeleteWithInvalidAutoCode(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogAttachedToWrongAppts(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogAttachedToWrongApptDate(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogBaseUnitsZero(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogCodeNumZero(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogProvNumMissing(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogToothNums(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogTpAttachedToClaim(verbose);
			Application.DoEvents();
			//textLog.Text+=DatabaseMaintenance.ProcedurelogUndeleteAttachedToClaim(verbose);
			//Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProcedurelogUnitQtyZero(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.ProviderHiddenWithClaimPayments(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.RecallDuplicatesWarn(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.RecallTriggerDeleteBadCodeNum(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.SchedulesDeleteShort(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.SchedulesDeleteProvClosed(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.SignalInFuture(verbose);
			Application.DoEvents();
			textLog.Text+=DatabaseMaintenance.StatementDateRangeMax(verbose);
			Application.DoEvents();*/
			textLog.Text+=Lan.g("FormDatabaseMaintenance","Done");
			SaveLogToFile();
			//textLog.ScrollToCaret
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
			//pagesPrinted=0;
			//linesPrinted=0;
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






	}
}
