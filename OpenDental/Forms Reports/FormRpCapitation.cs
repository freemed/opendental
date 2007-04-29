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
	public class FormRpCapitation : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.PrintDialog printDialog1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRpCapitation()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpCapitation));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
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
			// FormRpCapitation
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(605,386);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpCapitation";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Capitation Utilization Report";
			this.Load += new System.EventHandler(this.FormRpCapitation_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpCapitation_Load(object sender, System.EventArgs e) {
			//the user never sees this dialog.
			ExecuteReport();
			Close();
		}

		private void butOK_Click(object sender, System.EventArgs e) {

		}

		private void ExecuteReport(){
			ReportOld2 report=new ReportOld2();
			report.IsLandscape=true;
			report.AddTitle("CAPITATION UTILIZATION");
			report.AddSubTitle(((Pref)PrefB.HList["PracticeTitle"]).ValueString);
//incomplete: Need more flexible default values, eg based on current date instead of fixed date:
			DateTime DateTimeFirst=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1);
			report.AddParameter("carrier",FieldValueType.String,""
				,"Enter a few letters of the name of the insurance carrier"
				,"carrier.CarrierName LIKE '%?%'"); // SPK 8/04
			report.AddParameter("date1",FieldValueType.Date,DateTimeFirst
				,"From Date"
				,"procedurelog.ProcDate >= '?'");
			report.AddParameter("date2",FieldValueType.Date
				,DateTimeFirst.AddMonths(1).AddDays(-1)
				,"To Date"
				,"procedurelog.ProcDate <= '?'");		// added carrierNum, SPK
			report.Query=@"SELECT carrier.CarrierName,CONCAT(CONCAT(patSub.LName,', '),patSub.FName) 
				,patSub.SSN,CONCAT(CONCAT(patPat.LName,', '),patPat.FName)
				,patPat.Birthdate,procedurecode.ProcCode,procedurecode.Descript
				,procedurelog.ToothNum,procedurelog.Surf,procedurelog.ProcDate
				,procedurelog.ProcFee,procedurelog.ProcFee-claimproc.WriteOff
				FROM procedurelog,patient AS patSub,patient AS patPat
				,insplan,carrier,procedurecode,claimproc
				WHERE procedurelog.PatNum = patPat.PatNum
				AND claimproc.ProcNum = procedurelog.ProcNum
				AND claimproc.PlanNum = insplan.PlanNum
				AND claimproc.Status = 7
				AND claimproc.NoBillIns = 0 
				AND insplan.Subscriber = patSub.PatNum
				AND insplan.CarrierNum = carrier.CarrierNum	
				AND procedurelog.CodeNum = procedurecode.CodeNum
				AND ?carrier
				AND ?date1
				AND ?date2
				AND insplan.PlanType = 'c'
				AND procedurelog.ProcStatus = 2";
			report.AddColumn("Carrier",150,FieldValueType.String);
			report.GetLastRO(ReportObjectKind.FieldObject).SuppressIfDuplicate=true;
			report.AddColumn("Subscriber",120,FieldValueType.String);
			report.GetLastRO(ReportObjectKind.FieldObject).SuppressIfDuplicate=true;
			report.AddColumn("Subsc SSN",70,FieldValueType.String);
			report.GetLastRO(ReportObjectKind.FieldObject).SuppressIfDuplicate=true;
			report.AddColumn("Patient",120,FieldValueType.String);
			report.AddColumn("Pat DOB",80,FieldValueType.Date);
			report.AddColumn("Code",50,FieldValueType.String);
			report.AddColumn("Proc Description",120,FieldValueType.String);
			report.AddColumn("Tth",30,FieldValueType.String);
			report.AddColumn("Surf",40,FieldValueType.String);
			report.AddColumn("Date",80,FieldValueType.Date);
			report.AddColumn("UCR Fee",70,FieldValueType.Number);
			report.AddColumn("Co-Pay",70,FieldValueType.Number);
			report.AddPageNum();
      if(!report.SubmitQuery()){
				DialogResult=DialogResult.Cancel;
				return;
			}
//incomplete: Add functionality for using parameter values in textObjects, probably using inline XML:
			report.AddSubTitle(((DateTime)report.ParameterFields["date1"].CurrentValues[0]).ToShortDateString()+" - "+((DateTime)report.ParameterFields["date2"].CurrentValues[0]).ToShortDateString());
//incomplete: Implement formulas for situations like this:
			for(int i=0;i<report.ReportTable.Rows.Count;i++){
				if(PIn.PDouble(report.ReportTable.Rows[i][11].ToString())==-1){
					report.ReportTable.Rows[i][11]="0";
				}
			}
			FormReportOld2 FormR=new FormReportOld2(report);
			//FormR.MyReport=report;
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}




















