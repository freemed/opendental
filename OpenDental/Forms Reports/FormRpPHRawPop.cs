using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpPHRawPop : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listAdjType;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpPHRawPop(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpPHRawPop));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.listAdjType = new System.Windows.Forms.ListBox();
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
			this.butCancel.Location = new System.Drawing.Point(665,392);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 4;
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
			this.butOK.Location = new System.Drawing.Point(665,360);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(290,67);
			this.date2.MaxSelectionCount = 1;
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(34,67);
			this.date1.MaxSelectionCount = 1;
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(217,75);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(69,23);
			this.labelTO.TabIndex = 28;
			this.labelTO.Text = "TO";
			this.labelTO.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(537,24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(195,43);
			this.label1.TabIndex = 29;
			this.label1.Text = "Adjustment types for broken appointments (hold down the control key to select mul" +
    "tiple)";
			// 
			// listAdjType
			// 
			this.listAdjType.Location = new System.Drawing.Point(538,67);
			this.listAdjType.Name = "listAdjType";
			this.listAdjType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listAdjType.Size = new System.Drawing.Size(149,238);
			this.listAdjType.TabIndex = 30;
			// 
			// FormRpPHRawPop
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(765,440);
			this.Controls.Add(this.listAdjType);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.labelTO);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpPHRawPop";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Raw Population Data";
			this.Load += new System.EventHandler(this.FormRpPHRawPop_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpPHRawPop_Load(object sender, System.EventArgs e) {
			DateTime today=DateTime.Today;
			//will start out 1st through 30th of previous month
			date1.SelectionStart=new DateTime(today.Year,today.Month,1).AddMonths(-1);
			date2.SelectionStart=new DateTime(today.Year,today.Month,1).AddDays(-1);
			for(int i=0;i<DefB.Short[(int)DefCat.AdjTypes].Length;i++){
				listAdjType.Items.Add(DefB.Short[(int)DefCat.AdjTypes][i].ItemName);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listAdjType.SelectedIndices.Count==0){
				MessageBox.Show("At least one adjustment type must be selected.");
				return;
			}
			Queries.CurReport=new ReportOld();
			string types="";
			for(int i=0;i<listAdjType.SelectedIndices.Count;i++){
				if(i==0){
					types+="(";
				}
				else{
					types+="OR ";
				}
				types+="AdjType='"
					+DefB.Short[(int)DefCat.AdjTypes][listAdjType.SelectedIndices[i]].DefNum.ToString()
					+"' ";
			}
			types+=")";
			Queries.CurReport.Query=@"
				CREATE TEMPORARY TABLE tempbroken(
					PatNum mediumint unsigned NOT NULL,
					NumberBroken smallint NOT NULL,
					PRIMARY KEY (PatNum)
				);
				INSERT INTO tempbroken SELECT PatNum,COUNT(*)
				FROM adjustment WHERE "+types
				+"AND AdjDate >= "+POut.PDate(date1.SelectionStart)+" "
				+"AND AdjDate <= " +POut.PDate(date2.SelectionStart)+" "
				+@"GROUP BY PatNum;
				SELECT patient.PatNum,MIN(procedurelog.ProcDate) AS ProcDate,
				CONCAT(CONCAT(provider.LName,', '),provider.FName) as ProvName,
				County,county.CountyCode,
				GradeSchool,school.SchoolCode,GradeLevel,Birthdate,Race,Gender,Urgency,BillingType,
				patient.NextAptNum='-1' AS Done,tempbroken.NumberBroken
				FROM patient,provider
				LEFT JOIN procedurelog ON procedurelog.PatNum=patient.PatNum
				LEFT JOIN school ON patient.GradeSchool=school.SchoolName
				LEFT JOIN county ON patient.County=county.CountyName
				LEFT JOIN tempbroken ON tempbroken.PatNum=patient.PatNum
				WHERE	(procedurelog.ProcStatus='2'
				AND procedurelog.ProvNum=provider.ProvNum
				AND procedurelog.ProcDate >= "+POut.PDate(date1.SelectionStart)+" "
				+"AND procedurelog.ProcDate <= " +POut.PDate(date2.SelectionStart)+" )"
				+"OR tempbroken.NumberBroken>0 "
				+@"GROUP BY patient.PatNum
				ORDER By ProcDate;
				DROP TABLE tempbroken;";
/*
CREATE TEMPORARY TABLE tempbroken(
  PatNum mediumint unsigned NOT NULL,
  NumberBroken smallint NOT NULL,
  PRIMARY KEY (PatNum)
);
INSERT INTO tempbroken
SELECT PatNum,COUNT(*)
FROM adjustment
WHERE AdjType='14'
&& AdjDate='2004-05-03'
GROUP BY PatNum;
SELECT MIN(procedurelog.ProcDate) AS ProcDate,
CONCAT(provider.LName,', ',provider.FName) as ProvName,
County,county.CountyCode,
GradeSchool,school.SchoolCode,GradeLevel,Birthdate,Race,Gender,Urgency,BillingType,
patient.NextAptNum='-1' AS Done,tempbroken.NumberBroken
FROM patient,procedurelog,provider,tempbroken
LEFT JOIN school ON patient.GradeSchool=school.SchoolName
LEFT JOIN county ON patient.County=county.CountyName
WHERE procedurelog.ProcStatus='2'
&& patient.PatNum=procedurelog.PatNum
&& procedurelog.ProvNum=provider.ProvNum
&& tempbroken.PatNum=patient.PatNum
&& procedurelog.ProcDate >= '2004-05-03'
&& procedurelog.ProcDate <= '2004-05-03'
GROUP BY procedurelog.PatNum
ORDER By ProcDate;
DROP TABLE tempbroken;


*/
			FormQuery2=new FormQuery();
			FormQuery2.textTitle.Text="RawPopulationData"+DateTime.Today.ToString("MMddyyyy");
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
