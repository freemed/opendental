using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpFinanceCharge : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpFinanceCharge(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpFinanceCharge));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textDate = new OpenDental.ValidDate();
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
			this.butCancel.Location = new System.Drawing.Point(336,176);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 19;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(336,142);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 18;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(154,62);
			this.textDate.Name = "textDate";
			this.textDate.ReadOnly = true;
			this.textDate.Size = new System.Drawing.Size(116,20);
			this.textDate.TabIndex = 16;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,66);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136,14);
			this.label1.TabIndex = 17;
			this.label1.Text = "Date of Charges";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormRpFinanceCharge
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(436,230);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpFinanceCharge";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Finance Charge Report";
			this.Load += new System.EventHandler(this.FormRpFinanceCharge_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRpFinanceCharge_Load(object sender, System.EventArgs e) {
			textDate.Text=PIn.PDate(((Pref)PrefC.HList["FinanceChargeLastRun"]).ValueString).ToShortDateString();
			/*if(DateTime.Today.Day > 15){
				if(DateTime.Today.Month!=12){
					textDate.Text=(new DateTime(DateTime.Today.Year,DateTime.Today.Month+1,1)).ToShortDateString();		
				}
				else{ 
					textDate.Text=(new DateTime(DateTime.Today.Year+1,1,1)).ToShortDateString();	
				}
			}
			else{
				textDate.Text=(new DateTime(DateTime.Today.Year,DateTime.Today.Month,1)).ToShortDateString();
			}	*/	
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//if(textDate.errorProvider1.GetError(textDate)!=""){
			//	MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
			//	return;
			//}
			ReportSimpleGrid report=new ReportSimpleGrid();
			report.Query=
				"SELECT CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI),adjamt "
				+"FROM patient,adjustment "
				+"WHERE patient.patnum=adjustment.patnum "
				+"AND adjustment.adjdate = '"+((Pref)PrefC.HList["FinanceChargeLastRun"]).ValueString+"'"
				+"AND adjustment.adjtype = '"+((Pref)PrefC.HList["FinanceChargeAdjustmentType"]).ValueString+"'";
			FormQuery2=new FormQuery(report);
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();		
			report.Title="FINANCE CHARGE REPORT";
			report.SubTitle=new string[4];
			report.SubTitle[0]=((Pref)PrefC.HList["PracticeTitle"]).ValueString;
			report.SubTitle[1]="Date of Charges: "
				+PIn.PDate(((Pref)PrefC.HList["FinanceChargeLastRun"]).ValueString).ToShortDateString();
			//report.SubTitle[2]="Adjustment type: "+PIn.PDate(textDate.Text).ToShortDateString();

			report.ColPos=new int[3];
			report.ColCaption=new string[2];
			report.ColAlign=new HorizontalAlignment[2];

			report.ColPos[0]=20;
			report.ColPos[1]=200;
			report.ColPos[2]=300;

			report.ColCaption[0]="Patient Name";
			report.ColCaption[1]="Amount";

			report.ColAlign[1]=HorizontalAlignment.Right;

			report.Summary=new string[0];
			FormQuery2.ShowDialog();		
			DialogResult=DialogResult.OK;
		}

	}
}
