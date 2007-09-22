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
		private System.Windows.Forms.PrintDialog printDialog1;
		private MonthCalendar date2;
		private MonthCalendar date1;
		private Label labelTO;
		private RadioButton radioIndividual;
		private RadioButton radioGroup;
		private TextBox textCarrier;
		private Label label1;
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
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.radioIndividual = new System.Windows.Forms.RadioButton();
			this.radioGroup = new System.Windows.Forms.RadioButton();
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(510,338);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(510,297);
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
			this.label1.Size = new System.Drawing.Size(320,22);
			this.label1.TabIndex = 35;
			this.label1.Text = "Enter a few letters of the carrier to limit results";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormRpPPOwriteoffs
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(605,386);
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
			ReportOld2 report=new ReportOld2();
			report.AddTitle(Lan.g(this,"PPO WRITEOFFS"));
			report.AddSubTitle(PrefB.GetString("PracticeTitle"));
			report.AddSubTitle(date1.SelectionStart.ToShortDateString()+" - "+date2.SelectionStart.ToShortDateString());
			report.AddSubTitle(Lan.g(this,"Individual Claims"));
			if(textCarrier.Text!=""){
				report.AddSubTitle(Lan.g(this,"Carrier like: ")+textCarrier.Text);
			}
			report.Query="SET @DateFrom="+POut.PDate(date1.SelectionStart)+", @DateTo="+POut.PDate(date2.SelectionStart)
				+", @CarrierName='%"+POut.PString(textCarrier.Text)+"%';"
				+@"SELECT claimproc.DateCP,
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
			FormReportOld2 FormR=new FormReportOld2(report);
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void ExecuteGroup() {
			ReportOld2 report=new ReportOld2();
			report.AddTitle(Lan.g(this,"PPO WRITEOFFS"));
			report.AddSubTitle(PrefB.GetString("PracticeTitle"));
			report.AddSubTitle(date1.SelectionStart.ToShortDateString()+" - "+date2.SelectionStart.ToShortDateString());
			report.AddSubTitle(Lan.g(this,"Grouped by Carrier"));
			if(textCarrier.Text!="") {
				report.AddSubTitle(Lan.g(this,"Carrier like: ")+textCarrier.Text);
			}
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
			report.AddColumn("Carrier",180,FieldValueType.String);
			report.AddColumn("Stand Fee",80,FieldValueType.Number);
			report.AddColumn("PPO Fee",80,FieldValueType.Number);
			report.AddColumn("Writeoff",80,FieldValueType.Number);
			if(!report.SubmitQuery()) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			FormReportOld2 FormR=new FormReportOld2(report);
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}




















