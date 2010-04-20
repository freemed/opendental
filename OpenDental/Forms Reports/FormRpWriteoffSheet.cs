using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpWriteoffSheet : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;
		private System.ComponentModel.Container components = null;
		private ListBox listProv;
		private Label label1;
		private GroupBox groupBox3;
		private Label label5;
		private RadioButton radioWriteoffProc;
		private RadioButton radioWriteoffPay;
		private CheckBox checkAllProv;
		private CheckBox checkAllClin;
		private ListBox listClin;
		private Label labelClin;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpWriteoffSheet(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpWriteoffSheet));
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.radioWriteoffProc = new System.Windows.Forms.RadioButton();
			this.radioWriteoffPay = new System.Windows.Forms.RadioButton();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.checkAllProv = new System.Windows.Forms.CheckBox();
			this.checkAllClin = new System.Windows.Forms.CheckBox();
			this.listClin = new System.Windows.Forms.ListBox();
			this.labelClin = new System.Windows.Forms.Label();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(288,37);
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(32,37);
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(222,37);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(51,23);
			this.labelTO.TabIndex = 28;
			this.labelTO.Text = "TO";
			this.labelTO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(534,54);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(181,160);
			this.listProv.TabIndex = 36;
			this.listProv.Click += new System.EventHandler(this.listProv_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(532,18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104,16);
			this.label1.TabIndex = 35;
			this.label1.Text = "Providers";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.radioWriteoffProc);
			this.groupBox3.Controls.Add(this.radioWriteoffPay);
			this.groupBox3.Location = new System.Drawing.Point(32,246);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(281,95);
			this.groupBox3.TabIndex = 47;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Show Insurance Writeoffs";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6,71);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(269,17);
			this.label5.TabIndex = 2;
			this.label5.Text = "(this is discussed in the PPO section of the manual)";
			// 
			// radioWriteoffProc
			// 
			this.radioWriteoffProc.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.radioWriteoffProc.Location = new System.Drawing.Point(9,41);
			this.radioWriteoffProc.Name = "radioWriteoffProc";
			this.radioWriteoffProc.Size = new System.Drawing.Size(244,23);
			this.radioWriteoffProc.TabIndex = 1;
			this.radioWriteoffProc.Text = "Using procedure date.";
			this.radioWriteoffProc.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.radioWriteoffProc.UseVisualStyleBackColor = true;
			// 
			// radioWriteoffPay
			// 
			this.radioWriteoffPay.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.radioWriteoffPay.Checked = true;
			this.radioWriteoffPay.Location = new System.Drawing.Point(9,20);
			this.radioWriteoffPay.Name = "radioWriteoffPay";
			this.radioWriteoffPay.Size = new System.Drawing.Size(244,23);
			this.radioWriteoffPay.TabIndex = 0;
			this.radioWriteoffPay.TabStop = true;
			this.radioWriteoffPay.Text = "Using insurance payment date.";
			this.radioWriteoffPay.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.radioWriteoffPay.UseVisualStyleBackColor = true;
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
			this.butCancel.Location = new System.Drawing.Point(637,379);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 4;
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
			this.butOK.Location = new System.Drawing.Point(637,347);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// checkAllProv
			// 
			this.checkAllProv.Checked = true;
			this.checkAllProv.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAllProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllProv.Location = new System.Drawing.Point(534,35);
			this.checkAllProv.Name = "checkAllProv";
			this.checkAllProv.Size = new System.Drawing.Size(95,16);
			this.checkAllProv.TabIndex = 48;
			this.checkAllProv.Text = "All";
			this.checkAllProv.Click += new System.EventHandler(this.checkAllProv_Click);
			// 
			// checkAllClin
			// 
			this.checkAllClin.Checked = true;
			this.checkAllClin.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAllClin.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllClin.Location = new System.Drawing.Point(361,239);
			this.checkAllClin.Name = "checkAllClin";
			this.checkAllClin.Size = new System.Drawing.Size(95,16);
			this.checkAllClin.TabIndex = 54;
			this.checkAllClin.Text = "All";
			this.checkAllClin.Click += new System.EventHandler(this.checkAllClin_Click);
			// 
			// listClin
			// 
			this.listClin.Location = new System.Drawing.Point(361,258);
			this.listClin.Name = "listClin";
			this.listClin.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listClin.Size = new System.Drawing.Size(154,147);
			this.listClin.TabIndex = 53;
			this.listClin.Click += new System.EventHandler(this.listClin_Click);
			// 
			// labelClin
			// 
			this.labelClin.Location = new System.Drawing.Point(358,221);
			this.labelClin.Name = "labelClin";
			this.labelClin.Size = new System.Drawing.Size(104,16);
			this.labelClin.TabIndex = 52;
			this.labelClin.Text = "Clinics";
			this.labelClin.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormRpWriteoffSheet
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(746,431);
			this.Controls.Add(this.checkAllClin);
			this.Controls.Add(this.listClin);
			this.Controls.Add(this.labelClin);
			this.Controls.Add(this.checkAllProv);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.labelTO);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpWriteoffSheet";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Daily Writeoff Report";
			this.Load += new System.EventHandler(this.FormDailyWriteoff_Load);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDailyWriteoff_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;
			for(int i=0;i<ProviderC.List.Length;i++){
				listProv.Items.Add(ProviderC.List[i].GetLongDesc());
			}
			if(PrefC.GetBool(PrefName.EasyNoClinics)) {
				listClin.Visible=false;
				labelClin.Visible=false;
				checkAllClin.Visible=false;
			}
			else {
				listClin.Items.Add(Lan.g(this,"Unassigned"));
				for(int i=0;i<Clinics.List.Length;i++) {
					listClin.Items.Add(Clinics.List[i].Description);
				}
			}
			if(PrefC.GetBool(PrefName.ReportsPPOwriteoffDefaultToProcDate)){
				radioWriteoffProc.Checked=true;
			}
		}

		private void checkAllProv_Click(object sender,EventArgs e) {
			if(checkAllProv.Checked) {
				listProv.SelectedIndices.Clear();
			}
		}

		private void listProv_Click(object sender,EventArgs e) {
			if(listProv.SelectedIndices.Count>0) {
				checkAllProv.Checked=false;
			}
		}

		private void checkAllClin_Click(object sender,EventArgs e) {
			if(checkAllClin.Checked) {
				listClin.SelectedIndices.Clear();
			}
		}

		private void listClin_Click(object sender,EventArgs e) {
			if(listClin.SelectedIndices.Count>0) {
				checkAllClin.Checked=false;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!checkAllProv.Checked && listProv.SelectedIndices.Count==0) {
				MsgBox.Show(this,"At least one provider must be selected.");
				return;
			}
			if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
				if(!checkAllClin.Checked && listClin.SelectedIndices.Count==0) {
					MsgBox.Show(this,"At least one clinic must be selected.");
					return;
				}
			}
			string whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="claimproc.ProvNum = "
						+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
				}
				whereProv+=") ";
			}
			string whereClin="";
			if(!checkAllClin.Checked) {
				for(int i=0;i<listClin.SelectedIndices.Count;i++) {
					if(i==0) {
						whereClin+=" AND (";
					}
					else {
						whereClin+="OR ";
					}
					if(listClin.SelectedIndices[i]==0) {
						whereClin+="claimproc.ClinicNum = 0 ";
					}
					else {
						whereClin+="claimproc.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			ReportSimpleGrid report=new ReportSimpleGrid();
			report.Query="SET @FromDate="+POut.Date(date1.SelectionStart)+", @ToDate="+POut.Date(date2.SelectionStart)+";";
			if(radioWriteoffPay.Checked){
				report.Query+="SELECT DATE(claimproc.DateCP) date,"
					+"CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),"
					+"carrier.CarrierName,"
					+"provider.Abbr,";
				if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
					report.Query+="clinic.Description,";
				}
				report.Query+=
					"SUM(claimproc.WriteOff) $amount,"
					+"claimproc.ClaimNum "
					+"FROM claimproc "//,insplan,patient,carrier,provider "
					+"LEFT JOIN insplan ON claimproc.PlanNum = insplan.PlanNum "
					+"LEFT JOIN patient ON claimproc.PatNum = patient.PatNum "
					+"LEFT JOIN carrier ON carrier.CarrierNum = insplan.CarrierNum "
					+"LEFT JOIN provider ON provider.ProvNum = claimproc.ProvNum "
					+"LEFT JOIN clinic ON clinic.ClinicNum=claimproc.ClinicNum "
					+"WHERE (claimproc.Status=1 OR claimproc.Status=4) " /*received or supplemental*/
					+whereProv
					+whereClin
					+"AND claimproc.DateCP >= @FromDate "
					+"AND claimproc.DateCP <= @ToDate "
					+"AND claimproc.WriteOff > 0 "
					+"GROUP BY claimproc.ProvNum,claimproc.DateCP,claimproc.ClinicNum,claimproc.PatNum "
					+"ORDER BY claimproc.DateCP,claimproc.PatNum";
			}
			else{//using procedure date
				report.Query+="SELECT Date(claimproc.ProcDate) date, "
					+"CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI), "
					+"carrier.CarrierName, "
					+"provider.Abbr,";
				if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
					report.Query+="clinic.Description,";
				}
				report.Query+=
					"SUM(claimproc.WriteOff) $amount, "
					+"claimproc.ClaimNum "
					+"FROM claimproc "//,insplan,patient,carrier,provider "
					+"LEFT JOIN insplan ON claimproc.PlanNum = insplan.PlanNum "
					+"LEFT JOIN patient ON claimproc.PatNum = patient.PatNum "
					+"LEFT JOIN carrier ON carrier.CarrierNum = insplan.CarrierNum "
					+"LEFT JOIN provider ON provider.ProvNum = claimproc.ProvNum "
					+"LEFT JOIN clinic ON clinic.ClinicNum=claimproc.ClinicNum "
					+"WHERE (claimproc.Status=1 OR claimproc.Status=4 OR claimproc.Status=0) " /*received or supplemental or notreceived*/
					+whereProv
					+whereClin
					+"AND claimproc.ProcDate >= @FromDate "
					+"AND claimproc.ProcDate <= @ToDate "
					+"AND claimproc.WriteOff > 0 "
					+"GROUP BY claimproc.ProvNum,claimproc.ProcDate,claimproc.ClinicNum,claimproc.PatNum "
					+"ORDER BY claimproc.ProcDate,claimproc.PatNum";
			}
			FormQuery2=new FormQuery(report);
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();			
			report.Title="Daily Writeoffs";
			report.SubTitle.Add(PrefC.GetString(PrefName.PracticeTitle));
			report.SubTitle.Add(date1.SelectionStart.ToString("d")+" - "+date2.SelectionStart.ToString("d"));
			if(checkAllProv.Checked) {
				report.SubTitle.Add(Lan.g(this,"All Providers"));
			}
			else {
				string provNames="";
				for(int i=0;i<listProv.SelectedIndices.Count;i++) {
					if(i>0) {
						provNames+=", ";
					}
					provNames+=ProviderC.List[listProv.SelectedIndices[i]].Abbr;
				}
				report.SubTitle.Add(provNames);
			}
			if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
				if(checkAllClin.Checked) {
					report.SubTitle.Add(Lan.g(this,"All Clinics"));
				}
				else {
					string clinNames="";
					for(int i=0;i<listClin.SelectedIndices.Count;i++) {
						if(i>0) {
							clinNames+=", ";
						}
						if(listClin.SelectedIndices[i]==0) {
							clinNames+=Lan.g(this,"Unassigned");
						}
						else {
							clinNames+=Clinics.List[listClin.SelectedIndices[i]-1].Description;
						}
					}
					report.SubTitle.Add(clinNames);
				}
			}
			report.SetColumn(this,0,"Date",100);
			report.SetColumn(this,1,"Patient Name",150);
			report.SetColumn(this,2,"Carrier",225);
			report.SetColumn(this,3,"Provider",60);
			if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
				report.SetColumn(this,4,"Clinic",80);
				report.SetColumn(this,5,"Amount",75,HorizontalAlignment.Right);
				report.SetColumn(this,6,"",280,HorizontalAlignment.Right);
			}
			else {
				report.SetColumn(this,4,"Amount",75,HorizontalAlignment.Right);
				report.SetColumn(this,5,"",280,HorizontalAlignment.Right);
			}
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}
		

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}
