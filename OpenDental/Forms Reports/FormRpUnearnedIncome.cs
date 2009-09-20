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
			this.SuspendLayout();
			// 
			// FormRpUnearnedIncome
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(390,168);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpUnearnedIncome";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Unearned Income";
			this.Load += new System.EventHandler(this.FormUnearnedIncome_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormUnearnedIncome_Load(object sender, System.EventArgs e) {

		

			ReportSimpleGrid report=new ReportSimpleGrid();
			report.Query="SELECT SUM(SplitAmt) Amount FROM paysplit WHERE UnearnedType > 0";
			FormQuery2=new FormQuery(report);
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();
			report.Title="Unearned Income";
			report.SubTitle.Add(((Pref)PrefC.HList["PracticeTitle"]).ValueString);
			report.SetColumn(this,0,"Amount",80,HorizontalAlignment.Right);
			
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {

		}

	
		

		



	}
}
