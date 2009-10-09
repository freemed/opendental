using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpInsCo : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label labelPatName;
		private System.Windows.Forms.TextBox textBoxCarrier;
		private System.ComponentModel.Container components = null;
		private FormQuery FormQuery2;
		private string carrier;

		///<summary></summary>
		public FormRpInsCo(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpInsCo));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.labelPatName = new System.Windows.Forms.Label();
			this.textBoxCarrier = new System.Windows.Forms.TextBox();
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
			this.butCancel.Location = new System.Drawing.Point(490,173);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(490,138);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// labelPatName
			// 
			this.labelPatName.Location = new System.Drawing.Point(24,7);
			this.labelPatName.Name = "labelPatName";
			this.labelPatName.Size = new System.Drawing.Size(256,70);
			this.labelPatName.TabIndex = 37;
			this.labelPatName.Text = "Enter the first few letters of the Insurance Carrier name, or leave blank to view" +
    " all carriers:";
			this.labelPatName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxCarrier
			// 
			this.textBoxCarrier.Location = new System.Drawing.Point(281,33);
			this.textBoxCarrier.Name = "textBoxCarrier";
			this.textBoxCarrier.Size = new System.Drawing.Size(100,20);
			this.textBoxCarrier.TabIndex = 0;
			// 
			// FormRpInsCo
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(591,229);
			this.Controls.Add(this.labelPatName);
			this.Controls.Add(this.textBoxCarrier);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpInsCo";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Insurance Plan Report";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void butOK_Click(object sender, System.EventArgs e) {
			carrier= PIn.PString(textBoxCarrier.Text);
			ReportSimpleGrid report=new ReportSimpleGrid();

/*
SELECT insplan.subscriber,insplan.carrier,patient.hmphone,
insplan.groupname FROM insplan,patient WHERE insplan.subscriber=patient.patnum 
&& insplan.carrier like +carrier+'%'
Order By patient.lname,patient.fname

*/
			report.Query= "SELECT carrier.CarrierName"
				+",CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI),carrier.Phone,"
				+"insplan.Groupname "
				+"FROM insplan,patient,carrier,patplan "
				+"WHERE insplan.Subscriber=patient.PatNum "
				+"AND insplan.PlanNum=patplan.PlanNum "
				+"AND patplan.PatNum=patient.PatNum "
				+"AND patplan.Ordinal=1 "
				+"AND carrier.CarrierNum=insplan.CarrierNum "
				+"AND carrier.CarrierName LIKE '"+carrier+"%' "
				+"ORDER BY carrier.CarrierName,patient.LName";
			//Debug.WriteLine(report.Query);
			FormQuery2=new FormQuery(report);
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();			
			report.Title="Insurance Plan List";
			report.SubTitle.Add(PrefC.GetString(PrefName.PracticeTitle));
			report.SetColumn(this,0,"Carrier Name",230);
			report.SetColumn(this,1,"Subscriber Name",175);
			report.SetColumn(this,2,"Carrier Phone#",175);
			report.SetColumn(this,3,"Group Name",165);
			report.Summary.Add(Lan.g(this,"Total: ")+report.TableQ.Rows.Count.ToString());
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;		
		}
	}
}


















