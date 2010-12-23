using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpInsOverpaid:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private Label label1;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRpInsOverpaid() {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpInsOverpaid));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
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
			this.butCancel.Location = new System.Drawing.Point(336,105);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 19;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(240,105);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 18;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(21,20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(390,64);
			this.label1.TabIndex = 20;
			this.label1.Text = "Helps find situations where the insurance payment plus any writeoff exceeds the f" +
    "ee.  See the manual for suggestions on how to handle the results.";
			// 
			// FormRpInsOverpaid
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(436,145);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpInsOverpaid";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Insurance Overpaid Report";
			this.Load += new System.EventHandler(this.FormRpInsOverpaid_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpInsOverpaid_Load(object sender, System.EventArgs e) {
			
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			ReportSimpleGrid report=new ReportSimpleGrid();
			report.Query=@"SELECT procedurelog.PatNum,"+DbHelper.Concat("patient.LName","', '","patient.FName")+@" patname,
procedurelog.ProcDate,
SUM(procedurelog.ProcFee) $sumfee,
SUM((SELECT SUM(claimproc.InsPayAmt + claimproc.Writeoff) FROM claimproc WHERE claimproc.ProcNum=procedurelog.ProcNum)) AS
$PaidAndWriteoff
FROM procedurelog
LEFT JOIN procedurecode ON procedurelog.CodeNum=procedurecode.CodeNum
LEFT JOIN patient ON patient.PatNum=procedurelog.PatNum
WHERE procedurelog.ProcStatus=2/*complete*/
AND procedurelog.ProcFee > 0
GROUP BY procedurelog.PatNum,procedurelog.ProcDate
HAVING $sumfee < $PaidAndWriteoff
ORDER BY patname,ProcDate";
			FormQuery FormQuery2=new FormQuery(report);
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();		
			report.Title="INSURANCE OVERPAID REPORT";
			report.SubTitle.Add(PrefC.GetString(PrefName.PracticeTitle));
			report.SetColumn(this,0,"PatNum",60);
			report.SetColumn(this,1,"Pat Name",150);
			report.SetColumn(this,2,"Date",80);
			report.SetColumn(this,3,"Fee",80,HorizontalAlignment.Right);
			report.SetColumn(this,4,"InsPd+W/O",90,HorizontalAlignment.Right);
			Cursor=Cursors.Default;
			FormQuery2.ShowDialog();		
			DialogResult=DialogResult.OK;
		}

	}
}
