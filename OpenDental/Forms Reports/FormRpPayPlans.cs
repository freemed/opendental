using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Globalization;
using System.Drawing.Printing;
using System.Windows.Forms;
using OpenDental.ReportingOld2;
using OpenDental.UI;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormRpApptWithPhones.
	/// </summary>
	public class FormRpPayPlans:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private int pagesPrinted;
		private ODErrorProvider errorProvider1=new ODErrorProvider();
		private DataTable BirthdayTable;
		private int patientsPrinted;
		private PrintDocument pd;
		private OpenDental.UI.PrintPreview printPreview;

		///<summary></summary>
		public FormRpPayPlans()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpPayPlans));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
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
			this.butCancel.Location = new System.Drawing.Point(546,216);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 44;
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
			this.butOK.Location = new System.Drawing.Point(546,176);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 43;
			this.butOK.Text = "Report";
			this.butOK.Click += new System.EventHandler(this.butReport_Click);
			// 
			// FormRpPayPlans
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(660,264);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormRpPayPlans";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Payment Plans Report";
			this.Load += new System.EventHandler(this.FormRpPayPlans_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpPayPlans_Load(object sender, System.EventArgs e){
			
		}

		


		private void butReport_Click(object sender, System.EventArgs e){
			/*if(errorProvider1.GetError(textDateFrom) != ""
				|| errorProvider1.GetError(textDateTo) != "") 
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			DateTime dateTo=PIn.PDate(textDateTo.Text);
			if(dateTo < dateFrom) 
			{
				MsgBox.Show(this,"To date cannot be before From date.");
				return;
			}
			ReportOld2 report=new ReportOld2();
			report.ReportName=Lan.g(this,"Birthdays");
			report.AddTitle(Lan.g(this,"Birthdays"));
			report.AddSubTitle(PrefB.GetString("PracticeTitle"));
			report.AddSubTitle(dateFrom.ToString("MM/dd")+" - "+dateTo.ToString("MM/dd"));*/
			/*report.Query=@"SELECT LName,FName,Address,Address2,City,State,Zip,Birthdate,Birthdate
				FROM patient 
				WHERE SUBSTRING(Birthdate,6,5) >= '"+dateFrom.ToString("MM-dd")+"' "
				+"AND SUBSTRING(Birthdate,6,5) <= '"+dateTo.ToString("MM-dd")+"' "
				+"AND PatStatus=0	ORDER BY LName,FName";*/
			/*report.AddColumn("LName",90,FieldValueType.String);
			report.AddColumn("FName",90,FieldValueType.String);
			report.AddColumn("Preferred",90,FieldValueType.String);
			report.AddColumn("Address",90,FieldValueType.String);
			report.AddColumn("Address2",90,FieldValueType.String);
			report.AddColumn("City",75,FieldValueType.String);
			report.AddColumn("State",60,FieldValueType.String);
			report.AddColumn("Zip",75,FieldValueType.String);
			report.AddColumn("Birthdate", 75, FieldValueType.Date);
			report.GetLastRO(ReportObjectKind.FieldObject).FormatString="d";
			report.AddColumn("Age", 45, FieldValueType.Integer);
			report.AddPageNum();
			report.ReportTable=Patients.GetBirthdayList(dateFrom,dateTo);
			//if(!report.SubmitQuery()){
			//	return;
			//}
			FormReportOld2 FormR=new FormReportOld2(report);
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;*/
		}

		

		













		

		

		
	}
}
