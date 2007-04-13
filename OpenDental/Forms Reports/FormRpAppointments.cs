using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using OpenDental.ReportingOld2;
using OpenDentBusiness;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormRpApptWithPhones.
	/// </summary>
	public class FormRpAppointments : System.Windows.Forms.Form
	{
		private OpenDental.UI.Button butAll;
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Label label1;
		private OpenDental.ValidDate textDateTo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidDate textDateFrom;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.GroupBox groupBox1;
		private OpenDental.UI.Button butTomorrow;
		private OpenDental.UI.Button butToday;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRpAppointments()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpAppointments));
			this.butAll = new OpenDental.UI.Button();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textDateTo = new OpenDental.ValidDate();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textDateFrom = new OpenDental.ValidDate();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butToday = new OpenDental.UI.Button();
			this.butTomorrow = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(28,243);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(75,26);
			this.butAll.TabIndex = 34;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(27,41);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(120,186);
			this.listProv.TabIndex = 33;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(27,18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104,16);
			this.label1.TabIndex = 32;
			this.label1.Text = "Providers";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(92,51);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(100,20);
			this.textDateTo.TabIndex = 44;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4,52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82,18);
			this.label3.TabIndex = 39;
			this.label3.Text = "To";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6,26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82,18);
			this.label2.TabIndex = 37;
			this.label2.Text = "From";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(92,24);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(100,20);
			this.textDateFrom.TabIndex = 43;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(502,336);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 44;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(502,296);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 43;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butToday);
			this.groupBox1.Controls.Add(this.butTomorrow);
			this.groupBox1.Controls.Add(this.textDateFrom);
			this.groupBox1.Controls.Add(this.textDateTo);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(200,35);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(380,158);
			this.groupBox1.TabIndex = 45;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Date Range";
			// 
			// butToday
			// 
			this.butToday.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butToday.Autosize = true;
			this.butToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butToday.CornerRadius = 4F;
			this.butToday.Location = new System.Drawing.Point(250,23);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(96,23);
			this.butToday.TabIndex = 46;
			this.butToday.Text = "Today";
			this.butToday.Click += new System.EventHandler(this.butToday_Click);
			// 
			// butTomorrow
			// 
			this.butTomorrow.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTomorrow.Autosize = true;
			this.butTomorrow.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTomorrow.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTomorrow.CornerRadius = 4F;
			this.butTomorrow.Location = new System.Drawing.Point(250,50);
			this.butTomorrow.Name = "butTomorrow";
			this.butTomorrow.Size = new System.Drawing.Size(96,23);
			this.butTomorrow.TabIndex = 45;
			this.butTomorrow.Text = "Tomorrow";
			this.butTomorrow.Click += new System.EventHandler(this.butTomorrow_Click);
			// 
			// FormRpAppointments
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(645,383);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormRpAppointments";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Appointments Report";
			this.Load += new System.EventHandler(this.FormRpApptWithPhones_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpApptWithPhones_Load(object sender, System.EventArgs e){
			for(int i=0;i<Providers.List.Length;i++){
				listProv.Items.Add(Providers.List[i].Abbr+" - "+Providers.List[i].LName+", "
					+Providers.List[i].FName);
				listProv.SetSelected(i,true);
			}
			SetTomorrow();
		}

		private void SetTomorrow(){
			textDateFrom.Text = DateTime.Today.AddDays(1).ToShortDateString();
			textDateTo.Text = DateTime.Today.AddDays(1).ToShortDateString();
		}

		private void butToday_Click(object sender, System.EventArgs e) {
			textDateFrom.Text = DateTime.Today.ToShortDateString();
			textDateTo.Text = DateTime.Today.ToShortDateString();
		}

		private void butTomorrow_Click(object sender, System.EventArgs e) {
			SetTomorrow();
		}

		private void butAll_Click(object sender, System.EventArgs e){
			for(int i=0;i<listProv.Items.Count;i++){
				listProv.SetSelected(i,true);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e){
			//validate user input
			if(textDateFrom.errorProvider1.GetError(textDateFrom) != ""
				|| textDateTo.errorProvider1.GetError(textDateTo) != "") 
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(textDateFrom.Text.Length == 0
				|| textDateTo.Text.Length == 0) 
			{
				MessageBox.Show(Lan.g(this,"From and To dates are required."));
				return;
			}
			DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			DateTime dateTo=PIn.PDate(textDateTo.Text);
			if(dateTo < dateFrom) 
			{
				MessageBox.Show(Lan.g(this,"To date cannot be before From date."));
				return;
			}
			if(listProv.SelectedIndices.Count==0)
			{
				MessageBox.Show(Lan.g(this,"You must select at least one provider."));
				return;
			}

			string whereProv;//used as the provider portion of the where clauses.
			//each whereProv needs to be set up separately for each query
			whereProv="appointment.ProvNum in (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=",";
				}
				whereProv+="'"+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv += ") ";
			//create the report
			ReportOld2 report=new ReportOld2();
			report.IsLandscape=true;
			report.ReportName="Appointments";
			report.AddTitle("Appointments");
			report.AddSubTitle(((Pref)PrefB.HList["PracticeTitle"]).ValueString);
			report.AddSubTitle(dateFrom.ToShortDateString()+" - "+dateTo.ToShortDateString());
			//setup query
			report.Query=@"SELECT appointment.AptDateTime, 
				trim(CONCAT(CONCAT(CONCAT(CONCAT(concat(patient.LName,', '),case when length(patient.Preferred) > 0 
				then CONCAT(concat('(',patient.Preferred),') ') else '' end),patient.fname), ' '),patient.middlei))
				AS PatName,
				patient.Birthdate,
				appointment.AptDateTime,
				length(appointment.Pattern)*5,
				appointment.ProcDescript,
				patient.HmPhone, patient.WkPhone, patient.WirelessPhone
				FROM appointment INNER JOIN patient ON appointment.PatNum = patient.PatNum
				WHERE appointment.AptDateTime between " + POut.PDate(dateFrom) + " AND "
				+POut.PDate(dateTo.AddDays(1)) + " AND " +
				"AptStatus != '" + (int)ApptStatus.UnschedList + "' AND " +
				"AptStatus != '" + (int)ApptStatus.Planned + "' AND " +
				whereProv + " " +
				"ORDER BY appointment.AptDateTime, 2";
			// add columns to report
			report.AddColumn("Date", 75, FieldValueType.Date);
			report.GetLastRO(ReportObjectKind.FieldObject).SuppressIfDuplicate = true;
			report.GetLastRO(ReportObjectKind.FieldObject).FormatString="d";
			report.AddColumn("Patient", 175, FieldValueType.String);
			report.AddColumn("Age", 45, FieldValueType.Age);
			// remove the total column
			//if(report.ReportObjects[report.ReportObjects.Count-1].SummarizedField == "Age")
			//	report.ReportObjects.RemoveAt(report.ReportObjects.Count-1);
			//report.GetLastRO(ReportObjectKind.FieldObject).FormatString = "###0";
			//report.GetLastRO(ReportObjectKind.FieldObject).TextAlign = ContentAlignment.MiddleCenter;
			//report.GetLastRO(ReportObjectKind.TextObject).TextAlign = ContentAlignment.MiddleCenter;
			report.AddColumn("Time", 65, FieldValueType.Date);
			report.GetLastRO(ReportObjectKind.FieldObject).FormatString="t";
			report.GetLastRO(ReportObjectKind.TextObject).TextAlign = ContentAlignment.MiddleRight;
			report.GetLastRO(ReportObjectKind.FieldObject).TextAlign = ContentAlignment.MiddleRight;
			report.AddColumn("Length", 60, FieldValueType.Integer);
			report.GetLastRO(ReportObjectKind.TextObject).Location=new Point(
				report.GetLastRO(ReportObjectKind.TextObject).Location.X+6,
				report.GetLastRO(ReportObjectKind.TextObject).Location.Y);
			report.GetLastRO(ReportObjectKind.FieldObject).Location=new Point(
				report.GetLastRO(ReportObjectKind.FieldObject).Location.X+8,
				report.GetLastRO(ReportObjectKind.FieldObject).Location.Y);
			report.AddColumn("Description", 170, FieldValueType.String);
			report.AddColumn("Home Ph.", 120, FieldValueType.String);
			report.AddColumn("Work Ph.", 120, FieldValueType.String);
			report.AddColumn("Cell Ph.", 120, FieldValueType.String);
			report.AddPageNum();
			// execute query
			if(!report.SubmitQuery()){
				return;
			}
			// display report
			FormReportOld2 FormR=new FormReportOld2(report);
			//FormR.MyReport=report;
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		

		
	}
}
