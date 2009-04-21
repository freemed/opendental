using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormReportsMore : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private Label label1;
		private Label label2;
		private Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.ListBoxClickable listLists;
		private OpenDental.UI.ListBoxClickable listPublicHealth;
		private OpenDental.UI.Button butUserQuery;
		private OpenDental.UI.Button butPW;
		private OpenDental.UI.ListBoxClickable listProdInc;
		private Label label4;
		private OpenDental.UI.ListBoxClickable listDaily;
		private Label label5;
		private Label label6;
        private OpenDental.UI.Button butLaserLabels;
				private OpenDental.UI.ListBoxClickable listArizonaPrimaryCare;
				private Label labelArizonaPrimaryCare;
		private OpenDental.UI.ListBoxClickable listMonthly;

		///<summary></summary>
		public FormReportsMore()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReportsMore));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.labelArizonaPrimaryCare = new System.Windows.Forms.Label();
			this.listArizonaPrimaryCare = new OpenDental.UI.ListBoxClickable();
			this.butLaserLabels = new OpenDental.UI.Button();
			this.listDaily = new OpenDental.UI.ListBoxClickable();
			this.listProdInc = new OpenDental.UI.ListBoxClickable();
			this.butPW = new OpenDental.UI.Button();
			this.butUserQuery = new OpenDental.UI.Button();
			this.listPublicHealth = new OpenDental.UI.ListBoxClickable();
			this.listLists = new OpenDental.UI.ListBoxClickable();
			this.listMonthly = new OpenDental.UI.ListBoxClickable();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(312,250);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118,18);
			this.label1.TabIndex = 2;
			this.label1.Text = "Public Health";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(312,43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(118,18);
			this.label2.TabIndex = 4;
			this.label2.Text = "Lists";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(9,276);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(118,18);
			this.label3.TabIndex = 6;
			this.label3.Text = "Monthly";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9,43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(207,18);
			this.label4.TabIndex = 13;
			this.label4.Text = "Production and Income";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(9,159);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(118,18);
			this.label5.TabIndex = 15;
			this.label5.Text = "Daily";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(9,435);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(479,100);
			this.label6.TabIndex = 17;
			this.label6.Text = resources.GetString("label6.Text");
			// 
			// labelArizonaPrimaryCare
			// 
			this.labelArizonaPrimaryCare.AutoSize = true;
			this.labelArizonaPrimaryCare.Location = new System.Drawing.Point(312,310);
			this.labelArizonaPrimaryCare.Name = "labelArizonaPrimaryCare";
			this.labelArizonaPrimaryCare.Size = new System.Drawing.Size(104,13);
			this.labelArizonaPrimaryCare.TabIndex = 20;
			this.labelArizonaPrimaryCare.Text = "Arizona Primary Care";
			this.labelArizonaPrimaryCare.Visible = false;
			// 
			// listArizonaPrimaryCare
			// 
			this.listArizonaPrimaryCare.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listArizonaPrimaryCare.FormattingEnabled = true;
			this.listArizonaPrimaryCare.ItemHeight = 15;
			this.listArizonaPrimaryCare.Location = new System.Drawing.Point(315,328);
			this.listArizonaPrimaryCare.Name = "listArizonaPrimaryCare";
			this.listArizonaPrimaryCare.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listArizonaPrimaryCare.Size = new System.Drawing.Size(204,34);
			this.listArizonaPrimaryCare.TabIndex = 19;
			this.listArizonaPrimaryCare.Visible = false;
			this.listArizonaPrimaryCare.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listArizonaPrimaryCare_MouseDown);
			// 
			// butLaserLabels
			// 
			this.butLaserLabels.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLaserLabels.Autosize = true;
			this.butLaserLabels.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLaserLabels.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLaserLabels.CornerRadius = 4F;
			this.butLaserLabels.Location = new System.Drawing.Point(315,12);
			this.butLaserLabels.Name = "butLaserLabels";
			this.butLaserLabels.Size = new System.Drawing.Size(75,24);
			this.butLaserLabels.TabIndex = 18;
			this.butLaserLabels.Text = "Laser Labels";
			this.butLaserLabels.UseVisualStyleBackColor = true;
			this.butLaserLabels.Click += new System.EventHandler(this.butLaserLabels_Click);
			// 
			// listDaily
			// 
			this.listDaily.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listDaily.FormattingEnabled = true;
			this.listDaily.ItemHeight = 15;
			this.listDaily.Location = new System.Drawing.Point(12,180);
			this.listDaily.Name = "listDaily";
			this.listDaily.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listDaily.Size = new System.Drawing.Size(204,94);
			this.listDaily.TabIndex = 16;
			this.listDaily.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listDaily_MouseDown);
			// 
			// listProdInc
			// 
			this.listProdInc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listProdInc.FormattingEnabled = true;
			this.listProdInc.ItemHeight = 15;
			this.listProdInc.Location = new System.Drawing.Point(12,64);
			this.listProdInc.Name = "listProdInc";
			this.listProdInc.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listProdInc.Size = new System.Drawing.Size(204,94);
			this.listProdInc.TabIndex = 14;
			this.listProdInc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listProdInc_MouseDown);
			// 
			// butPW
			// 
			this.butPW.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPW.Autosize = true;
			this.butPW.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPW.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPW.CornerRadius = 4F;
			this.butPW.Location = new System.Drawing.Point(135,12);
			this.butPW.Name = "butPW";
			this.butPW.Size = new System.Drawing.Size(84,24);
			this.butPW.TabIndex = 12;
			this.butPW.Text = "PW Reports";
			this.butPW.Click += new System.EventHandler(this.butPW_Click);
			// 
			// butUserQuery
			// 
			this.butUserQuery.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUserQuery.Autosize = true;
			this.butUserQuery.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUserQuery.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUserQuery.CornerRadius = 4F;
			this.butUserQuery.Location = new System.Drawing.Point(12,12);
			this.butUserQuery.Name = "butUserQuery";
			this.butUserQuery.Size = new System.Drawing.Size(84,24);
			this.butUserQuery.TabIndex = 11;
			this.butUserQuery.Text = "User Query";
			this.butUserQuery.Click += new System.EventHandler(this.butUserQuery_Click);
			// 
			// listPublicHealth
			// 
			this.listPublicHealth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listPublicHealth.FormattingEnabled = true;
			this.listPublicHealth.ItemHeight = 15;
			this.listPublicHealth.Location = new System.Drawing.Point(315,271);
			this.listPublicHealth.Name = "listPublicHealth";
			this.listPublicHealth.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listPublicHealth.Size = new System.Drawing.Size(204,34);
			this.listPublicHealth.TabIndex = 10;
			this.listPublicHealth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listPublicHealth_MouseDown);
			// 
			// listLists
			// 
			this.listLists.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listLists.FormattingEnabled = true;
			this.listLists.ItemHeight = 15;
			this.listLists.Location = new System.Drawing.Point(315,64);
			this.listLists.Name = "listLists";
			this.listLists.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listLists.Size = new System.Drawing.Size(204,184);
			this.listLists.TabIndex = 9;
			this.listLists.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listLists_MouseDown);
			// 
			// listMonthly
			// 
			this.listMonthly.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listMonthly.FormattingEnabled = true;
			this.listMonthly.ItemHeight = 15;
			this.listMonthly.Location = new System.Drawing.Point(12,297);
			this.listMonthly.Name = "listMonthly";
			this.listMonthly.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listMonthly.Size = new System.Drawing.Size(204,124);
			this.listMonthly.TabIndex = 8;
			this.listMonthly.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listMonthly_MouseDown);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(554,491);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormReportsMore
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(676,553);
			this.Controls.Add(this.labelArizonaPrimaryCare);
			this.Controls.Add(this.listArizonaPrimaryCare);
			this.Controls.Add(this.butLaserLabels);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.listDaily);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.listProdInc);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butPW);
			this.Controls.Add(this.butUserQuery);
			this.Controls.Add(this.listPublicHealth);
			this.Controls.Add(this.listLists);
			this.Controls.Add(this.listMonthly);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormReportsMore";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reports";
			this.Load += new System.EventHandler(this.FormReportsMore_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormReportsMore_Load(object sender,EventArgs e) {
			butPW.Visible=Programs.IsEnabled("PracticeWebReports");
			listProdInc.Items.AddRange(new string[] {
				Lan.g(this,"Today"),
				Lan.g(this,"Yesterday"),
				Lan.g(this,"This Month"),
				Lan.g(this,"Last Month"),
				Lan.g(this,"This Year"),
				Lan.g(this,"More Options")
			});
			listDaily.Items.AddRange(new string[] {
				Lan.g(this,"Adjustments"),
				Lan.g(this,"Payments"),
				Lan.g(this,"Procedures"),
				Lan.g(this,"Writeoffs"),
				Lan.g(this,"Incomplete Procedure Notes"),
				Lan.g(this,"Routing Slips")
			});
			listMonthly.Items.AddRange(new string[] {
				Lan.g(this,"Aging Report"),
				Lan.g(this,"Claims Not Sent"),
				Lan.g(this,"Capitation Utilization"),
				Lan.g(this,"Finance Charge Report"),
				Lan.g(this,"Outstanding Insurance Claims"),
				Lan.g(this,"Procedures Not Billed to Insurance"),
				Lan.g(this,"PPO Writeoffs"),
				Lan.g(this,"Payment Plans"),
                Lan.g(this,"Receivable Breakdown Report")// KS
			});
			listLists.Items.AddRange(new string[] {
				Lan.g(this,"Appointments"),
				Lan.g(this,"Birthdays"),
				Lan.g(this,"Insurance Plans"),
				Lan.g(this,"New Patients"),
				Lan.g(this,"Patients - Raw"),
				Lan.g(this,"Patients Notes"),
				Lan.g(this,"Prescriptions"),
				Lan.g(this,"Procedure Codes"),
				Lan.g(this,"Referrals - Raw"),
				Lan.g(this,"Referral Analysis"),
				Lan.g(this,"Treatment Finder"),
				Lan.g(this,"Treatment Plan Manager")
			});
			listPublicHealth.Items.AddRange(new string[] {
				Lan.g(this,"Raw Screening Data"),
				Lan.g(this,"Raw Population Data")
			});
			listArizonaPrimaryCare.Items.AddRange(new string[] {
				Lan.g(this,"Eligibility File"),
				Lan.g(this,"Encounter File")
			});
			//Arizona primary care list and label must only be visible when the Arizona primary
			//care option is checked in the miscellaneous options.
			if(UsingArizonaPrimaryCare()){
				labelArizonaPrimaryCare.Visible=true;
				listArizonaPrimaryCare.Visible=true;
			}
		}

		///<summary>When using Arizona Primary Care, there must be a handful of pre-defined patient fields which are required  to generate the Arizona Primary Care reports. This function will return true if all of the required patient fields exist which are necessary to run the Arizona Primary Care reports. Otherwise, false is returned.</summary>
		public static bool UsingArizonaPrimaryCare() {
			PatFieldDefs.RefreshCache();
			string[] patientFieldNames=new string[] {
				"SPID#",
				"Eligibility Status",
				"Household Gross Income",
				"Household % of Poverty",
			};
			int[] fieldCounts=new int[patientFieldNames.Length];
			foreach(PatFieldDef pfd in PatFieldDefs.List) {
				for(int i=0;i<patientFieldNames.Length;i++) {
					if(pfd.FieldName.ToLower()==patientFieldNames[i].ToLower()) {
						fieldCounts[i]++;
						break;
					}
				}
			}
			for(int i=0;i<fieldCounts.Length;i++) {
				//Each field must be defined exactly once. This verifies that each requied field
				//both exists and is not ambiguous with another field of the same name.
				if(fieldCounts[i]!=1) {
					return false;
				}
			}
			return true;
		}

		private void butUserQuery_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.UserQuery)) {
				return;
			}
			FormQuery FormQuery2=new FormQuery();
			FormQuery2.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.UserQuery,0,"");
		}

		private void butPW_Click(object sender,EventArgs e) {
			try {
				Process.Start("PWReports.exe");
			}
			catch {
				MessageBox.Show("PracticeWeb Reports module unavailable.");
			}
			SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Practice Web");
		}

		private void listProdInc_MouseDown(object sender,MouseEventArgs e) {
			int selected=listProdInc.IndexFromPoint(e.Location);
			if(selected==-1) {
				return;
			}
			FormRpProdInc FormPI=new FormRpProdInc();
			switch(selected) {
				case 0://Today
					FormPI.DailyMonthlyAnnual="Daily";
					FormPI.DateStart=DateTime.Today;
					FormPI.DateEnd=DateTime.Today;
					break;
				case 1://Yesterday
					FormPI.DailyMonthlyAnnual="Daily";
					if(DateTime.Today.DayOfWeek==DayOfWeek.Monday){
						FormPI.DateStart=DateTime.Today.AddDays(-3);
						FormPI.DateEnd=DateTime.Today.AddDays(-3);
					}
					else{
						FormPI.DateStart=DateTime.Today.AddDays(-1);
						FormPI.DateEnd=DateTime.Today.AddDays(-1);
					}
					break;
				case 2://This Month
					FormPI.DailyMonthlyAnnual="Monthly";
					FormPI.DateStart=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1);
					FormPI.DateEnd=new DateTime(DateTime.Today.AddMonths(1).Year,DateTime.Today.AddMonths(1).Month,1).AddDays(-1);
					break;
				case 3://Last Month
					FormPI.DailyMonthlyAnnual="Monthly";
					FormPI.DateStart=new DateTime(DateTime.Today.AddMonths(-1).Year,DateTime.Today.AddMonths(-1).Month,1);
					FormPI.DateEnd=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1).AddDays(-1);
					break;
				case 4://This Year
					FormPI.DailyMonthlyAnnual="Annual";
					FormPI.DateStart=new DateTime(DateTime.Today.Year,1,1);
					FormPI.DateEnd=new DateTime(DateTime.Today.Year,12,31);
					break;
				case 5://More Options
					//do nothing
					break;
			}
			FormPI.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Production and Income");
		}

		private void listDaily_MouseDown(object sender,MouseEventArgs e) {
			int selected=listDaily.IndexFromPoint(e.Location);
			if(selected==-1) {
				return;
			}
			switch(selected) {
				case 0://Adjustments
					FormRpAdjSheet FormAdjSheet=new FormRpAdjSheet();
					FormAdjSheet.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Adjustments");
					break;
				case 1://Payments
					FormRpPaySheet FormPaySheet=new FormRpPaySheet();
					FormPaySheet.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Daily Payments");
					break;
				case 2://Procedures
					FormRpProcSheet FormProcSheet=new FormRpProcSheet();
					FormProcSheet.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Daily Procedures");	
					break;
				case 3://Writeoffs
					FormRpWriteoffSheet FormW=new FormRpWriteoffSheet();
					FormW.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Daily Writeoffs");	
					break;
				case 4://Incomplete Procedure Notes
					FormRpProcNote FormPN=new FormRpProcNote();
					FormPN.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Daily Procedure Notes");
					break;
				case 5://Routing Slips
					FormRpRouting FormR=new FormRpRouting();
					FormR.ShowDialog();
					break;
			}
		}

		private void listMonthly_MouseDown(object sender,MouseEventArgs e) {
			int selected=listMonthly.IndexFromPoint(e.Location);
			if(selected==-1){
				return;
			}
			switch(selected){
				case 0://Aging Report
					FormRpAging FormA=new FormRpAging();
					FormA.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Aging");
					break;
				case 1://Claims Not Sent
					FormRpClaimNotSent FormClaim=new FormRpClaimNotSent();
					FormClaim.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Claims Not Sent");
					break;
				case 2://Capitation Utilization
					FormRpCapitation FormC=new FormRpCapitation();
					FormC.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Capitation");
					break;
				case 3://Finance Charge Report
					FormRpFinanceCharge FormRpFinance=new FormRpFinanceCharge();
					FormRpFinance.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Finance Charges");
					break;
				case 4://Outstanding Insurance Claims
					FormRpOutInsClaims FormOut=new FormRpOutInsClaims();
					FormOut.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Outstanding Insurance Claims");
					break;
				case 5://Procedures Not Billed to Insurance
					FormRpProcNotBilledIns FormProc=new FormRpProcNotBilledIns();
					FormProc.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Procedures not billed to insurance.");
					break;
				case 6://PPO Writeoffs
					FormRpPPOwriteoffs FormPPO=new FormRpPPOwriteoffs();
					FormPPO.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"PPO Writeoffs.");
					break;
				case 7://Payment Plans
					FormRpPayPlans FormPP=new FormRpPayPlans();
					FormPP.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Payment Plans.");
					break;
                case 8://Receivable Breakdown Report  KS
                    FormRpReceivablesBreakdown FormRcv = new FormRpReceivablesBreakdown();
                    FormRcv.ShowDialog();
                    SecurityLogs.MakeLogEntry(Permissions.Reports, 0, "Receivable Breakdown Report.");
                    break;
			}
		}

		private void listLists_MouseDown(object sender,MouseEventArgs e) {
			int selected=listLists.IndexFromPoint(e.Location);
			if(selected==-1){
				return;
			}
			switch(selected){
				case 0://Appointments
					FormRpAppointments FormA=new FormRpAppointments();
					FormA.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Appointments");
					break;
				case 1://Birthdays
					FormRpBirthday FormB=new FormRpBirthday();
					FormB.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Birthdays");
					break;
				case 2://Insurance Plans
					FormRpInsCo FormInsCo=new FormRpInsCo();
					FormInsCo.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Insurance Plans");
					break;
				case 3://New Patients
					FormRpNewPatients FormNewPats=new FormRpNewPatients();
					FormNewPats.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"New Patients");
					break;
				case 4://Patients - Raw
					FormRpPatients FormPatients=new FormRpPatients();
					FormPatients.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Patients - Raw");
					break;
				case 5://Patient Notes
					FormSearchPatNotes FormPN=new FormSearchPatNotes();
					FormPN.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Patient Notes");
					break;
				case 6://Prescriptions
					FormRpPrescriptions FormPrescript=new FormRpPrescriptions();
					FormPrescript.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Rx");
					break;
				case 7://Procedure Codes
					FormRpProcCodes FormProcCodes=new FormRpProcCodes();
					FormProcCodes.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Procedure Codes");
					break;
				case 8://Referrals - Raw
					FormRpReferrals FormReferral=new FormRpReferrals();
					FormReferral.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Referrals - Raw");
					break;
				case 9://Referral Analysis
					FormRpReferralAnalysis FormRA=new FormRpReferralAnalysis();
					FormRA.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Referral Analysis");
					break;
				case 10://Treatment Finder
					FormRpTreatmentFinder FormT=new FormRpTreatmentFinder();
					FormT.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Treatment Finder");
					break;
				case 11://Treatment Plan Manager
					FormTxPlanManager FormTM=new FormTxPlanManager();
					FormTM.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Treatment Plan Manager");
					break;
			}
		}

		private void listPublicHealth_MouseDown(object sender,MouseEventArgs e) {
			int selected=listPublicHealth.IndexFromPoint(e.Location);
			if(selected==-1){
				return;
			}
			switch(selected){
				case 0://Raw Screening Data
					FormRpPHRawScreen FormPH=new FormRpPHRawScreen();
					FormPH.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"PH Raw Screening");
					break;
				case 1://Raw Population Data
					FormRpPHRawPop FormPHR=new FormRpPHRawPop();
					FormPHR.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"PH Raw population");
					break;
				
			}
		}

		private void butClose_Click(object sender,System.EventArgs e) {
			Close();
		}

    private void butLaserLabels_Click(object sender, EventArgs e) {
        FormRpLaserLabels LaserLabels = new FormRpLaserLabels();
        LaserLabels.ShowDialog();
    }

		private void listArizonaPrimaryCare_MouseDown(object sender,MouseEventArgs e) {
			int selected=this.listArizonaPrimaryCare.IndexFromPoint(e.Location);
			if(selected==-1) {
				return;
			}
			switch(selected) {
				case 0://Elegibility File
					FormRpArizonaPrimaryCareEligibility frapce=new FormRpArizonaPrimaryCareEligibility();
					frapce.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Arizona Primary Care Eligibility");
					break;
				case 1://Encounter File
					FormRpArizonaPrimaryCareEncounter frapcn=new FormRpArizonaPrimaryCareEncounter();
					frapcn.ShowDialog();
					SecurityLogs.MakeLogEntry(Permissions.Reports,0,"Arizona Primary Care Encounter");
					break;
			}
		}

	}
}