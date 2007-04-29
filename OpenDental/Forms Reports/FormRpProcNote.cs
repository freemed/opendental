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
	public class FormRpProcNote : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.PrintDialog printDialog1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRpProcNote()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//Lan.C("All", new System.Windows.Forms.Control[] {
			//	butOK,
			//	butCancel,
			//});
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpProcNote));
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
			// FormRpProcNote
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(605,386);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpProcNote";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Incomplete Procedure Notes Report";
			this.Load += new System.EventHandler(this.FormRpProcNote_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpProcNote_Load(object sender, System.EventArgs e) {
			//the user never sees this dialog.
			ExecuteReport();
			Close();
		}

		private void butOK_Click(object sender, System.EventArgs e) {

		}

		private void ExecuteReport(){
			ReportOld2 report=new ReportOld2();
			report.AddTitle("INCOMPLETE PROCEDURE NOTES");
			report.AddSubTitle(((Pref)PrefB.HList["PracticeTitle"]).ValueString);
			report.Query=@"SELECT procedurelog.ProcDate,
				CONCAT(CONCAT(patient.LName,', '),patient.FName),
				procedurecode.ProcCode,procedurecode.Descript,
				procedurelog.ToothNum,procedurelog.Surf
				FROM procedurelog,patient,procedurecode,procnote n1
				WHERE procedurelog.PatNum = patient.PatNum
				AND procedurelog.CodeNum = procedurecode.CodeNum
				AND procedurelog.ProcStatus = 2
				AND procedurelog.ProcNum=n1.ProcNum "
				+"AND n1.Note LIKE '%\"\"%' "//looks for ""
				+@"AND n1.EntryDateTime=(SELECT MAX(n2.EntryDateTime)
				FROM procnote n2
				WHERE n1.ProcNum = n2.ProcNum)
				ORDER BY procedurelog.ProcDate";
			report.AddColumn("Date",80,FieldValueType.Date);
			report.AddColumn("Patient",120,FieldValueType.String);
			report.AddColumn("Code",50,FieldValueType.String);
			report.AddColumn("Description",120,FieldValueType.String);
			report.AddColumn("Tth",30,FieldValueType.String);
			report.AddColumn("Surf",40,FieldValueType.String);
			report.AddPageNum();
      if(!report.SubmitQuery()){
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




















