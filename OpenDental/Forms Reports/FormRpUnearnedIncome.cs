using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpUnearnedIncome:System.Windows.Forms.Form {
		private System.ComponentModel.Container components = null;
		private MonthCalendar date2;
		private MonthCalendar date1;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private RadioButton radioDateRange;
		private RadioButton radioTotal;
		private FormQuery FormQuery2;
		
		///<summary></summary>
		public FormRpUnearnedIncome() {
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpUnearnedIncome));
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.radioDateRange = new System.Windows.Forms.RadioButton();
			this.radioTotal = new System.Windows.Forms.RadioButton();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(281,27);
			this.date2.MaxSelectionCount = 1;
			this.date2.Name = "date2";
			this.date2.TabIndex = 4;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(25,27);
			this.date1.MaxSelectionCount = 1;
			this.date1.Name = "date1";
			this.date1.TabIndex = 3;
			// 
			// radioDateRange
			// 
			this.radioDateRange.Checked = true;
			this.radioDateRange.Location = new System.Drawing.Point(26,220);
			this.radioDateRange.Name = "radioDateRange";
			this.radioDateRange.Size = new System.Drawing.Size(169,18);
			this.radioDateRange.TabIndex = 7;
			this.radioDateRange.TabStop = true;
			this.radioDateRange.Text = "Activity for Date Range";
			this.radioDateRange.UseVisualStyleBackColor = true;
			// 
			// radioTotal
			// 
			this.radioTotal.Location = new System.Drawing.Point(26,245);
			this.radioTotal.Name = "radioTotal";
			this.radioTotal.Size = new System.Drawing.Size(205,18);
			this.radioTotal.TabIndex = 8;
			this.radioTotal.Text = "Total Liability";
			this.radioTotal.UseVisualStyleBackColor = true;
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
			this.butCancel.Location = new System.Drawing.Point(491,292);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 6;
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
			this.butOK.Location = new System.Drawing.Point(491,260);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 5;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormRpUnearnedIncome
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(599,343);
			this.Controls.Add(this.radioTotal);
			this.Controls.Add(this.radioDateRange);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpUnearnedIncome";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Unearned Income Activity";
			this.Load += new System.EventHandler(this.FormUnearnedIncomeActivity_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormUnearnedIncomeActivity_Load(object sender, System.EventArgs e) {
			DateTime dateThisFirst=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1);
			date1.SelectionStart=dateThisFirst.AddMonths(-1);
			date2.SelectionStart=dateThisFirst.AddDays(-1);
		}

		private void butOK_Click(object sender,EventArgs e) {
			ReportSimpleGrid report=new ReportSimpleGrid();
			if(radioDateRange.Checked) {
				report.Query="SELECT DatePay,CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),SplitAmt "
					+"FROM paysplit,patient "
					+"WHERE paysplit.PatNum=patient.PatNum "
					+"AND paysplit.DatePay >= "+POut.PDate(date1.SelectionStart)+" "
					+"AND paysplit.DatePay <= "+POut.PDate(date2.SelectionStart)+" "
					+"AND UnearnedType > 0 GROUP BY paysplit.SplitNum "
					+"ORDER BY DatePay";
				FormQuery2=new FormQuery(report);
				FormQuery2.IsReport=true;
				FormQuery2.SubmitReportQuery();
				report.Title="Unearned Income Activity";
				report.SubTitle.Add(((Pref)PrefC.HList["PracticeTitle"]).ValueString);
				report.SetColumn(this,0,"Date",100);
				report.SetColumn(this,1,"Patient",140);
				report.SetColumn(this,2,"Amount",80,HorizontalAlignment.Right);
			}
			else {
				report.Query="SELECT CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),SUM(SplitAmt) Amount "
					+"FROM paysplit,patient "
					+"WHERE paysplit.PatNum=patient.PatNum "
					+"AND UnearnedType > 0 GROUP BY paysplit.PatNum HAVING Amount != 0";
				FormQuery2=new FormQuery(report);
				FormQuery2.IsReport=true;
				FormQuery2.SubmitReportQuery();
				report.Title="Unearned Income Liabilities";
				report.SubTitle.Add(((Pref)PrefC.HList["PracticeTitle"]).ValueString);
				report.SetColumn(this,0,"Patient",140);
				report.SetColumn(this,1,"Amount",80,HorizontalAlignment.Right);
			}
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

	
		

		



	}
}
