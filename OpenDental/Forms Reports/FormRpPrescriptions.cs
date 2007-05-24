using System;
using System.Drawing;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpPrescriptions : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton radioDrug;
		private System.Windows.Forms.RadioButton radioPatient;
		private System.Windows.Forms.Label labelInstruct;
		private System.Windows.Forms.TextBox textBoxInput;
		private System.ComponentModel.Container components = null;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpPrescriptions(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpPrescriptions));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.radioDrug = new System.Windows.Forms.RadioButton();
			this.radioPatient = new System.Windows.Forms.RadioButton();
			this.labelInstruct = new System.Windows.Forms.Label();
			this.textBoxInput = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(506,204);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76,26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(506,169);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76,26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.radioDrug);
			this.panel1.Controls.Add(this.radioPatient);
			this.panel1.Location = new System.Drawing.Point(478,18);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(104,60);
			this.panel1.TabIndex = 1;
			// 
			// radioDrug
			// 
			this.radioDrug.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioDrug.Location = new System.Drawing.Point(8,32);
			this.radioDrug.Name = "radioDrug";
			this.radioDrug.Size = new System.Drawing.Size(88,24);
			this.radioDrug.TabIndex = 1;
			this.radioDrug.Text = "Drug";
			// 
			// radioPatient
			// 
			this.radioPatient.Checked = true;
			this.radioPatient.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioPatient.Location = new System.Drawing.Point(8,8);
			this.radioPatient.Name = "radioPatient";
			this.radioPatient.Size = new System.Drawing.Size(88,24);
			this.radioPatient.TabIndex = 0;
			this.radioPatient.TabStop = true;
			this.radioPatient.Text = "Patient";
			// 
			// labelInstruct
			// 
			this.labelInstruct.Location = new System.Drawing.Point(20,44);
			this.labelInstruct.Name = "labelInstruct";
			this.labelInstruct.Size = new System.Drawing.Size(258,16);
			this.labelInstruct.TabIndex = 40;
			this.labelInstruct.Text = "Enter the first few letters or leave blank to view all:";
			this.labelInstruct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textBoxInput
			// 
			this.textBoxInput.Location = new System.Drawing.Point(282,40);
			this.textBoxInput.Name = "textBoxInput";
			this.textBoxInput.Size = new System.Drawing.Size(116,20);
			this.textBoxInput.TabIndex = 0;
			// 
			// FormRpPrescriptions
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(594,240);
			this.Controls.Add(this.labelInstruct);
			this.Controls.Add(this.textBoxInput);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpPrescriptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Prescriptions Report";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void butOK_Click(object sender, System.EventArgs e) {
/*
SELECT CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),rxpat.rxdate,
rxpat.drug,rxpat.sig,rxpat.disp,provider.abbr FROM patient,rxpat,provider
WHERE patient.patnum=rxpat.patnum && provider.provnum=rxpat.provnum		
*/
			Queries.CurReport=new ReportOld();
			Queries.CurReport.Query="SELECT CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),"+
				"' '),patient.MiddleI),rxpat.rxdate,"
				+"rxpat.drug,rxpat.sig,rxpat.disp,provider.abbr FROM patient,rxpat,provider "
				+"WHERE patient.patnum=rxpat.patnum AND provider.provnum=rxpat.provnum ";

			if(radioPatient.Checked==true){
				Queries.CurReport.Query
					+="AND patient.lname like '"+textBoxInput.Text+"%'"
	        +" ORDER BY patient.lname,patient.fname,rxpat.rxdate";		
			}
			else{
				Queries.CurReport.Query
					+="AND rxpat.drug like '"+textBoxInput.Text+"%'"
			    +" ORDER BY patient.lname,rxpat.drug,rxpat.rxdate";
			}

			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();			

			Queries.CurReport.Title="Prescriptions";
			Queries.CurReport.SubTitle=new string[2];
			Queries.CurReport.SubTitle[0]=((Pref)PrefB.HList["PracticeTitle"]).ValueString;
			if(radioPatient.Checked==true){
				Queries.CurReport.SubTitle[1]="By Patient";
			}
			else{
				Queries.CurReport.SubTitle[1]="By Drug";
			}			
			Queries.CurReport.ColPos=new int[7];
			Queries.CurReport.ColCaption=new string[6];
			Queries.CurReport.ColAlign=new HorizontalAlignment[6];

			Queries.CurReport.ColPos[0]=10;
			Queries.CurReport.ColPos[1]=130;
			Queries.CurReport.ColPos[2]=225;
			Queries.CurReport.ColPos[3]=325;
			Queries.CurReport.ColPos[4]=625;
			Queries.CurReport.ColPos[5]=725;
			Queries.CurReport.ColPos[6]=825;

			Queries.CurReport.ColCaption[0]="Patient Name";
			Queries.CurReport.ColCaption[1]="Date";			
			Queries.CurReport.ColCaption[2]="Drug Name";
			Queries.CurReport.ColCaption[3]="Sig";
			Queries.CurReport.ColCaption[4]="Disp";
			Queries.CurReport.ColCaption[5]="Prov Name";

			Queries.CurReport.Summary=new string[0];
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}
	}
}
