using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpTreatmentFinder:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.ComponentModel.Container components = null;
		private Label label1;
		private CheckBox checkIncludeNoIns;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpTreatmentFinder() {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpTreatmentFinder));
			this.label1 = new System.Windows.Forms.Label();
			this.checkIncludeNoIns = new System.Windows.Forms.CheckBox();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(31,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(567,49);
			this.label1.TabIndex = 29;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// checkIncludeNoIns
			// 
			this.checkIncludeNoIns.Location = new System.Drawing.Point(34,95);
			this.checkIncludeNoIns.Name = "checkIncludeNoIns";
			this.checkIncludeNoIns.Size = new System.Drawing.Size(323,18);
			this.checkIncludeNoIns.TabIndex = 30;
			this.checkIncludeNoIns.Text = "Include patients without insurance";
			this.checkIncludeNoIns.UseVisualStyleBackColor = true;
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
			this.butCancel.Location = new System.Drawing.Point(523,216);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 4;
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
			this.butOK.Location = new System.Drawing.Point(523,184);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormRpTreatmentFinder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(616,254);
			this.Controls.Add(this.checkIncludeNoIns);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpTreatmentFinder";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Treatment Finder";
			this.Load += new System.EventHandler(this.FormRpTreatmentFinder_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpTreatmentFinder_Load(object sender, System.EventArgs e) {
			//DateTime today=DateTime.Today;
			//will start out 1st through 30th of previous month
			//date1.SelectionStart=new DateTime(today.Year,today.Month,1).AddMonths(-1);
			//date2.SelectionStart=new DateTime(today.Year,today.Month,1).AddDays(-1);
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Queries.CurReport=new ReportOld();
			Queries.CurReport.Query=@"
DROP TABLE IF EXISTS tempused;
DROP TABLE IF EXISTS tempplanned;
DROP TABLE IF EXISTS tempannualmax;

CREATE TABLE tempused(
PatPlanNum mediumint unsigned NOT NULL,
AmtUsed double NOT NULL,
PRIMARY KEY (PatPlanNum));

CREATE TABLE tempplanned(
PatNum mediumint unsigned NOT NULL,
AmtPlanned double NOT NULL,
PRIMARY KEY (PatNum));

CREATE TABLE tempannualmax(
PlanNum mediumint unsigned NOT NULL,
AnnualMax double NOT NULL,
PRIMARY KEY (PlanNum));

INSERT INTO tempused
SELECT patplan.PatPlanNum,
SUM(IFNULL(claimproc.InsPayAmt,0))
FROM claimproc
LEFT JOIN patplan ON patplan.PatNum = claimproc.PatNum
AND patplan.PlanNum = claimproc.PlanNum
WHERE claimproc.Status IN (1, 3, 4)
AND claimproc.ProcDate BETWEEN makedate(year(curdate()), 1)
AND makedate(year(curdate())+1, 1) /*current calendar year*/
GROUP BY patplan.PatPlanNum;

INSERT INTO tempplanned
SELECT PatNum, SUM(ProcFee)
FROM procedurelog
WHERE ProcStatus = 1 /*treatment planned*/
GROUP BY PatNum;

INSERT INTO tempannualmax
SELECT benefit.PlanNum, benefit.MonetaryAmt
FROM benefit, covcat
WHERE covcat.CovCatNum = benefit.CovCatNum
AND benefit.BenefitType = 5 /* limitation */
AND benefit.TimePeriod = 2 /* calendar year */
AND covcat.EbenefitCat=1
AND benefit.MonetaryAmt <> 0
ORDER BY benefit.PlanNum;

SELECT patient.LName, patient.FName,
tempannualmax.AnnualMax $AnnualMax,
tempused.AmtUsed $AmountUsed,
tempannualmax.AnnualMax-IFNULL(tempused.AmtUsed,0) $AmtRemaining,
tempplanned.AmtPlanned $TreatmentPlan
FROM patient
LEFT JOIN tempplanned ON tempplanned.PatNum=patient.PatNum
LEFT JOIN patplan ON patient.PatNum=patplan.PatNum
LEFT JOIN tempused ON tempused.PatPlanNum=patplan.PatPlanNum
LEFT JOIN tempannualmax ON tempannualmax.PlanNum=patplan.PlanNum
	AND tempannualmax.AnnualMax>0
	/*AND tempannualmax.AnnualMax-tempused.AmtUsed>0*/
WHERE tempplanned.AmtPlanned>0 ";
			if(!checkIncludeNoIns.Checked){//if we don't want patients without insurance
				Queries.CurReport.Query+="AND AnnualMax > 0 ";
			}
			Queries.CurReport.Query+=@"
ORDER BY tempplanned.AmtPlanned DESC;
DROP TABLE tempused;
DROP TABLE tempplanned;
DROP TABLE tempannualmax;";
			FormQuery2=new FormQuery();
			FormQuery2.textTitle.Text="Treatment Finder";
			//FormQuery2.IsReport=true;
			//FormQuery2.SubmitReportQuery();			
			FormQuery2.SubmitQuery();
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}



	}
}
