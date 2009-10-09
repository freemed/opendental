using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.ReportingOld2;
using OpenDentBusiness;

//using System.IO;
//using System.Text;
//using System.Xml.Serialization;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormRpPPOwriteoffs : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private MonthCalendar date2;
		private MonthCalendar date1;
		private Label labelTO;
		private RadioButton radioIndividual;
		private RadioButton radioGroup;
		private TextBox textCarrier;
		private Label label1;
		private GroupBox groupBox3;
		private Label label5;
		private RadioButton radioWriteoffProc;
		private RadioButton radioWriteoffPay;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRpPPOwriteoffs()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpPPOwriteoffs));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.radioIndividual = new System.Windows.Forms.RadioButton();
			this.radioGroup = new System.Windows.Forms.RadioButton();
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.radioWriteoffProc = new System.Windows.Forms.RadioButton();
			this.radioWriteoffPay = new System.Windows.Forms.RadioButton();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(625,330);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
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
			this.butOK.Location = new System.Drawing.Point(625,289);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(289,36);
			this.date2.MaxSelectionCount = 1;
			this.date2.Name = "date2";
			this.date2.TabIndex = 30;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(33,36);
			this.date1.MaxSelectionCount = 1;
			this.date1.Name = "date1";
			this.date1.TabIndex = 29;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(223,36);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(51,23);
			this.labelTO.TabIndex = 31;
			this.labelTO.Text = "TO";
			this.labelTO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// radioIndividual
			// 
			this.radioIndividual.Checked = true;
			this.radioIndividual.Location = new System.Drawing.Point(33,227);
			this.radioIndividual.Name = "radioIndividual";
			this.radioIndividual.Size = new System.Drawing.Size(200,19);
			this.radioIndividual.TabIndex = 32;
			this.radioIndividual.TabStop = true;
			this.radioIndividual.Text = "Individual Claims";
			this.radioIndividual.UseVisualStyleBackColor = true;
			// 
			// radioGroup
			// 
			this.radioGroup.Location = new System.Drawing.Point(33,250);
			this.radioGroup.Name = "radioGroup";
			this.radioGroup.Size = new System.Drawing.Size(200,19);
			this.radioGroup.TabIndex = 33;
			this.radioGroup.Text = "Group by Carrier";
			this.radioGroup.UseVisualStyleBackColor = true;
			// 
			// textCarrier
			// 
			this.textCarrier.Location = new System.Drawing.Point(33,309);
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.Size = new System.Drawing.Size(178,20);
			this.textCarrier.TabIndex = 34;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32,283);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(257,22);
			this.label1.TabIndex = 35;
			this.label1.Text = "Enter a few letters of the carrier to limit results";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.radioWriteoffProc);
			this.groupBox3.Controls.Add(this.radioWriteoffPay);
			this.groupBox3.Location = new System.Drawing.Point(289,227);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(281,95);
			this.groupBox3.TabIndex = 48;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Show Insurance Writeoffs";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6,71);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(269,17);
			this.label5.TabIndex = 2;
			this.label5.Text = "(this is discussed in the PPO section of the manual)";
			// 
			// radioWriteoffProc
			// 
			this.radioWriteoffProc.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.radioWriteoffProc.Location = new System.Drawing.Point(9,41);
			this.radioWriteoffProc.Name = "radioWriteoffProc";
			this.radioWriteoffProc.Size = new System.Drawing.Size(244,23);
			this.radioWriteoffProc.TabIndex = 1;
			this.radioWriteoffProc.Text = "Using procedure date.";
			this.radioWriteoffProc.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.radioWriteoffProc.UseVisualStyleBackColor = true;
			// 
			// radioWriteoffPay
			// 
			this.radioWriteoffPay.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.radioWriteoffPay.Checked = true;
			this.radioWriteoffPay.Location = new System.Drawing.Point(9,20);
			this.radioWriteoffPay.Name = "radioWriteoffPay";
			this.radioWriteoffPay.Size = new System.Drawing.Size(244,23);
			this.radioWriteoffPay.TabIndex = 0;
			this.radioWriteoffPay.TabStop = true;
			this.radioWriteoffPay.Text = "Using insurance payment date.";
			this.radioWriteoffPay.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.radioWriteoffPay.UseVisualStyleBackColor = true;
			// 
			// FormRpPPOwriteoffs
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(725,379);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textCarrier);
			this.Controls.Add(this.radioGroup);
			this.Controls.Add(this.radioIndividual);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.labelTO);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpPPOwriteoffs";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PPO Writeoffs";
			this.Load += new System.EventHandler(this.FormRpPPOwriteoffs_Load);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRpPPOwriteoffs_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1).AddMonths(-1);
			date2.SelectionStart=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1).AddDays(-1);
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(radioIndividual.Checked){
				ExecuteIndividual();
			}
			else{
				ExecuteGroup();
			}
		}

		private void ExecuteIndividual(){
			ReportLikeCrystal report=new ReportLikeCrystal();
			report.AddTitle(Lan.g(this,"PPO WRITEOFFS"));
			report.AddSubTitle(PrefC.GetString(PrefName.PracticeTitle));
			report.AddSubTitle(date1.SelectionStart.ToShortDateString()+" - "+date2.SelectionStart.ToShortDateString());
			report.AddSubTitle(Lan.g(this,"Individual Claims"));
			if(textCarrier.Text!=""){
				report.AddSubTitle(Lan.g(this,"Carrier like: ")+textCarrier.Text);
			}
			report.Query="SET @DateFrom="+POut.PDate(date1.SelectionStart)+", @DateTo="+POut.PDate(date2.SelectionStart)
				+", @CarrierName='%"+POut.PString(textCarrier.Text)+"%';";
			if(radioWriteoffPay.Checked){
				report.Query+=@"SELECT claimproc.DateCP,
					CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI),
					carrier.CarrierName,
					provider.Abbr,
					SUM(claimproc.FeeBilled),
					SUM(claimproc.FeeBilled-claimproc.WriteOff),
					SUM(claimproc.WriteOff),
					claimproc.ClaimNum
					FROM claimproc,insplan,patient,carrier,provider
					WHERE provider.ProvNum = claimproc.ProvNum
					AND claimproc.PlanNum = insplan.PlanNum
					AND claimproc.PatNum = patient.PatNum
					AND carrier.CarrierNum = insplan.CarrierNum
					AND (claimproc.Status=1 OR claimproc.Status=4) /*received or supplemental*/
					AND claimproc.DateCP >= @DateFrom
					AND claimproc.DateCP <= @DateTo
					AND insplan.PlanType='p'
					AND carrier.CarrierName LIKE @CarrierName
					GROUP BY claimproc.ClaimNum 
					ORDER BY claimproc.DateCP";
			}
			else{//use procedure date
				report.Query+=@"SELECT claimproc.ProcDate,
					CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI),
					carrier.CarrierName,
					provider.Abbr,
					SUM(claimproc.FeeBilled),
					SUM(claimproc.FeeBilled-claimproc.WriteOff),
					SUM(claimproc.WriteOff),
					claimproc.ClaimNum
					FROM claimproc,insplan,patient,carrier,provider
					WHERE provider.ProvNum = claimproc.ProvNum
					AND claimproc.PlanNum = insplan.PlanNum
					AND claimproc.PatNum = patient.PatNum
					AND carrier.CarrierNum = insplan.CarrierNum
					AND (claimproc.Status=1 OR claimproc.Status=4 OR claimproc.Status=0) /*received or supplemental or notreceived*/
					AND claimproc.ProcDate >= @DateFrom
					AND claimproc.ProcDate <= @DateTo
					AND insplan.PlanType='p'
					AND carrier.CarrierName LIKE @CarrierName
					GROUP BY claimproc.ClaimNum 
					ORDER BY claimproc.ProcDate";
			}
			report.AddColumn("Date",80,FieldValueType.Date);
			report.AddColumn("Patient",120,FieldValueType.String);
			report.AddColumn("Carrier",150,FieldValueType.String);
			report.AddColumn("Provider",60,FieldValueType.String);
			report.AddColumn("Stand Fee",80,FieldValueType.Number);
			report.AddColumn("PPO Fee",80,FieldValueType.Number);
			report.AddColumn("Writeoff",80,FieldValueType.Number);
      if(!report.SubmitQuery()){
				DialogResult=DialogResult.Cancel;
				return;
			}
			FormReportLikeCrystal FormR=new FormReportLikeCrystal(report);
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void ExecuteGroup() {
			ReportLikeCrystal report=new ReportLikeCrystal();
			report.AddTitle(Lan.g(this,"PPO WRITEOFFS"));
			report.AddSubTitle(PrefC.GetString(PrefName.PracticeTitle));
			report.AddSubTitle(date1.SelectionStart.ToShortDateString()+" - "+date2.SelectionStart.ToShortDateString());
			report.AddSubTitle(Lan.g(this,"Grouped by Carrier"));
			if(textCarrier.Text!="") {
				report.AddSubTitle(Lan.g(this,"Carrier like: ")+textCarrier.Text);
			}
			if(radioWriteoffPay.Checked){
				report.Query="SET @DateFrom="+POut.PDate(date1.SelectionStart)+", @DateTo="+POut.PDate(date2.SelectionStart)
					+", @CarrierName='%"+POut.PString(textCarrier.Text)+"%';"
					+@"SELECT carrier.CarrierName,
					SUM(claimproc.FeeBilled),
					SUM(claimproc.FeeBilled-claimproc.WriteOff),
					SUM(claimproc.WriteOff),
					claimproc.ClaimNum
					FROM claimproc,insplan,carrier
					WHERE claimproc.PlanNum = insplan.PlanNum
					AND carrier.CarrierNum = insplan.CarrierNum
					AND (claimproc.Status=1 OR claimproc.Status=4) /*received or supplemental*/
					AND claimproc.DateCP >= @DateFrom
					AND claimproc.DateCP <= @DateTo
					AND insplan.PlanType='p'
					AND carrier.CarrierName LIKE @CarrierName
					GROUP BY carrier.CarrierNum 
					ORDER BY carrier.CarrierName";
			}
			else{
				report.Query="SET @DateFrom="+POut.PDate(date1.SelectionStart)+", @DateTo="+POut.PDate(date2.SelectionStart)
					+", @CarrierName='%"+POut.PString(textCarrier.Text)+"%';"
					+@"SELECT carrier.CarrierName,
					SUM(claimproc.FeeBilled),
					SUM(claimproc.FeeBilled-claimproc.WriteOff),
					SUM(claimproc.WriteOff),
					claimproc.ClaimNum
					FROM claimproc,insplan,carrier
					WHERE claimproc.PlanNum = insplan.PlanNum
					AND carrier.CarrierNum = insplan.CarrierNum
					AND (claimproc.Status=1 OR claimproc.Status=4 OR claimproc.Status=0) /*received or supplemental or notreceived*/
					AND claimproc.ProcDate >= @DateFrom
					AND claimproc.ProcDate <= @DateTo
					AND insplan.PlanType='p'
					AND carrier.CarrierName LIKE @CarrierName
					GROUP BY carrier.CarrierNum 
					ORDER BY carrier.CarrierName";
			}
			report.AddColumn("Carrier",180,FieldValueType.String);
			report.AddColumn("Stand Fee",80,FieldValueType.Number);
			report.AddColumn("PPO Fee",80,FieldValueType.Number);
			report.AddColumn("Writeoff",80,FieldValueType.Number);
			if(!report.SubmitQuery()) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			FormReportLikeCrystal FormR=new FormReportLikeCrystal(report);
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}




















