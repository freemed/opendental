using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormHouseCalls : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.GroupBox groupBox2;
		private OpenDental.ValidDate textDateFrom;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private OpenDental.ValidDate textDateTo;
		private OpenDental.UI.Button butAll;
		private OpenDental.UI.Button but7;
		private System.Windows.Forms.RadioButton radioConfirm;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public Program ProgramCur;

		///<summary></summary>
		public FormHouseCalls()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHouseCalls));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butAll = new OpenDental.UI.Button();
			this.but7 = new OpenDental.UI.Button();
			this.textDateFrom = new OpenDental.ValidDate();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textDateTo = new OpenDental.ValidDate();
			this.radioConfirm = new System.Windows.Forms.RadioButton();
			this.groupBox2.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(359,267);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
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
			this.butOK.Location = new System.Drawing.Point(359,226);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.butAll);
			this.groupBox2.Controls.Add(this.but7);
			this.groupBox2.Controls.Add(this.textDateFrom);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.textDateTo);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(42,72);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(393,109);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Date Range";
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(263,61);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(93,23);
			this.butAll.TabIndex = 8;
			this.butAll.Text = "All Future";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// but7
			// 
			this.but7.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but7.Autosize = true;
			this.but7.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but7.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but7.CornerRadius = 4F;
			this.but7.Location = new System.Drawing.Point(263,35);
			this.but7.Name = "but7";
			this.but7.Size = new System.Drawing.Size(93,23);
			this.but7.TabIndex = 7;
			this.but7.Text = "Next 7 Days";
			this.but7.Click += new System.EventHandler(this.but7_Click);
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(137,37);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(84,20);
			this.textDateFrom.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(30,64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,17);
			this.label2.TabIndex = 4;
			this.label2.Text = "To Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(30,36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,17);
			this.label1.TabIndex = 2;
			this.label1.Text = "From Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(137,65);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(85,20);
			this.textDateTo.TabIndex = 5;
			// 
			// radioConfirm
			// 
			this.radioConfirm.Checked = true;
			this.radioConfirm.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioConfirm.Location = new System.Drawing.Point(43,30);
			this.radioConfirm.Name = "radioConfirm";
			this.radioConfirm.Size = new System.Drawing.Size(329,21);
			this.radioConfirm.TabIndex = 9;
			this.radioConfirm.TabStop = true;
			this.radioConfirm.Text = "Confirm Appointments";
			// 
			// FormHouseCalls
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(486,318);
			this.Controls.Add(this.radioConfirm);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormHouseCalls";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "House Calls";
			this.Load += new System.EventHandler(this.FormHouseCalls_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormHouseCalls_Load(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.AddDays(1).ToShortDateString();
			textDateTo.Text=DateTime.Today.AddDays(7).ToShortDateString();
		}

		private void but7_Click(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.AddDays(1).ToShortDateString();
			textDateTo.Text=DateTime.Today.AddDays(7).ToShortDateString();
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.AddDays(1).ToShortDateString();
			textDateTo.Text="";
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			DateTime FromDate;
			DateTime ToDate;
			if(textDateFrom.Text==""){
				MessageBox.Show(Lan.g(this,"From Date cannot be left blank."));
				return;
			}
			FromDate=PIn.PDate(textDateFrom.Text);
			if(textDateTo.Text=="")
				ToDate=DateTime.MaxValue.AddDays(-1);
			else
				ToDate=PIn.PDate(textDateTo.Text);
			//Create the file and first row--------------------------------------------------------
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Export Path");
			string fileName=PPCur.PropertyValue+"Appt.txt";
			if(!Directory.Exists(PPCur.PropertyValue)){
				Directory.CreateDirectory(PPCur.PropertyValue);
			}
			StreamWriter sr=File.CreateText(fileName);
			sr.WriteLine("\"LastName\",\"FirstName\",\"PatientNumber\",\"HomePhone\",\"WorkNumber\","
				+"\"EmailAddress\",\"SendEmail\",\"Address\",\"Address2\",\"City\",\"State\",\"Zip\","
				+"\"ApptDate\",\"ApptTime\",\"ApptReason\",\"DoctorNumber\",\"DoctorName\",\"IsNewPatient\"");
			//now, the query--------------------------------------------------------------------------
			//Appointment Reminder Fields- numbers are as they come back from db-----------------------
			//0-LastName
			//1-FirstName (or we substitute 2-Preferred Name if exists)
			// PatientNumber (Can be 3-PatNum or 4-ChartNumber, depending on what user selected)
			//5-HomePhone
			//6-WorkNumber
			//7-EmailAddress
			// SendEmail (this will be true if email address exists. Might change later)
			//8-Address
			//9-Address2 (although they did not offer this as an option)
			//10-City
			//11-State
			//12-Zip
			//13-ApptDate
			//13-ApptTime
			//14-ApptReason (procedures descriptions-user can't edit)
			//15-DoctorNumber (for the Doctor, we currently use the patient primary provider. Otherwise, we would run into trouble with appointments assigned to a specific hygienist.)
			//15-DoctorName
			//16-IsNewPatient
			string command=@"SELECT patient.LName,patient.FName,patient.Preferred
				,patient.PatNum,patient.ChartNumber,patient.HmPhone,patient.WkPhone
				,patient.Email,patient.Address,patient.Address2,patient.City,patient.State
				,patient.Zip
				,appointment.AptDateTime,appointment.ProcDescript
				,patient.PriProv
				,appointment.IsNewPatient
				FROM patient,appointment 
				WHERE patient.PatNum=appointment.PatNum "
				+"AND (appointment.AptStatus=1 OR appointment.AptStatus=4) "//sched or ASAP
				+"AND appointment.AptDateTime > "+POut.PDate(FromDate)//> midnight
				+" AND appointment.AptDateTime < "+POut.PDate(ToDate.AddDays(1));//< midnight
 			DataTable table=General.GetTable(command);
			bool usePatNum=false;
			PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");;
			if(PPCur.PropertyValue=="0"){
				usePatNum=true;
			}
			DateTime aptDT;
			for(int i=0;i<table.Rows.Count;i++){
				sr.Write("\""+Dequote(PIn.PString(table.Rows[i][0].ToString()))+"\",");//0-LastName
				if(table.Rows[i][2].ToString()!=""){//if Preferred Name exists
					sr.Write("\""+Dequote(PIn.PString(table.Rows[i][2].ToString()))+"\",");//2-PrefName
				}
				else{
					sr.Write("\""+Dequote(PIn.PString(table.Rows[i][1].ToString()))+"\",");//1-FirstName 
				}
				if(usePatNum){
					sr.Write("\""+table.Rows[i][3].ToString()+"\",");//3-PatNum
				}
				else{
					sr.Write("\""+Dequote(PIn.PString(table.Rows[i][4].ToString()))+"\",");//4-ChartNumber
				}
				sr.Write("\""+Dequote(PIn.PString(table.Rows[i][5].ToString()))+"\",");//5-HomePhone
				sr.Write("\""+Dequote(PIn.PString(table.Rows[i][6].ToString()))+"\",");//6-WorkNumber
				sr.Write("\""+Dequote(PIn.PString(table.Rows[i][7].ToString()))+"\",");//7-EmailAddress
				if(table.Rows[i][7].ToString()!=""){//if an email exists
					sr.Write("\"T\",");//SendEmail
				}
				else{
					sr.Write("\"F\",");
				}
				sr.Write("\""+Dequote(PIn.PString(table.Rows[i][8].ToString()))+"\",");//8-Address
				sr.Write("\""+Dequote(PIn.PString(table.Rows[i][9].ToString()))+"\",");//9-Address2
				sr.Write("\""+Dequote(PIn.PString(table.Rows[i][10].ToString()))+"\",");//10-City
				sr.Write("\""+Dequote(PIn.PString(table.Rows[i][11].ToString()))+"\",");//11-State
				sr.Write("\""+Dequote(PIn.PString(table.Rows[i][12].ToString()))+"\",");//12-Zip
				aptDT=PIn.PDateT(table.Rows[i][13].ToString());
				sr.Write("\""+aptDT.ToString("MM/dd/yyyy")+"\",");//13-ApptDate
				sr.Write("\""+aptDT.ToString("hh:mm tt")+"\",");//13-ApptTime eg 01:30 PM
				sr.Write("\""+Dequote(PIn.PString(table.Rows[i][14].ToString()))+"\",");//14-ApptReason
				sr.Write("\""+table.Rows[i][15].ToString()+"\",");//15-DoctorNumber. might possibly be 0
				//15-DoctorName. Can handle 0 without any problem.
				sr.Write("\""+Dequote(Providers.GetLName(PIn.PInt(table.Rows[i][15].ToString())))+"\",");
				if(table.Rows[i][16].ToString()=="1"){//16-IsNewPatient
					sr.WriteLine("\"T\"");//SendEmail
				}
				else{
					sr.WriteLine("\"F\"");
				}
			}
			sr.Close();
			MessageBox.Show("Done");
			DialogResult=DialogResult.OK;
		}

		private string Dequote(string inputStr){
			return inputStr.Replace("\"","");
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

	}
}





















