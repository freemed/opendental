using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpProcSheet : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radioIndividual;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioGrouped;
	  private FormQuery FormQuery2;
		private Label label2;
		private TextBox textCode;
		private CheckBox checkAllProv;
		private CheckBox checkAllClin;
		private ListBox listClin;
		private Label labelClin;
		///<summary>The where clause for the providers.</summary>
		private string whereProv;
		private string whereClin;

		///<summary></summary>
		public FormRpProcSheet(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpProcSheet));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.radioIndividual = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioGrouped = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			this.textCode = new System.Windows.Forms.TextBox();
			this.checkAllProv = new System.Windows.Forms.CheckBox();
			this.checkAllClin = new System.Windows.Forms.CheckBox();
			this.listClin = new System.Windows.Forms.ListBox();
			this.labelClin = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(634,382);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 4;
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
			this.butOK.Location = new System.Drawing.Point(634,346);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(284,33);
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(28,33);
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(210,41);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(72,23);
			this.labelTO.TabIndex = 22;
			this.labelTO.Text = "TO";
			this.labelTO.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(534,48);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(175,147);
			this.listProv.TabIndex = 33;
			this.listProv.Click += new System.EventHandler(this.listProv_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(532,14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104,16);
			this.label1.TabIndex = 32;
			this.label1.Text = "Providers";
			// 
			// radioIndividual
			// 
			this.radioIndividual.Checked = true;
			this.radioIndividual.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioIndividual.Location = new System.Drawing.Point(11,17);
			this.radioIndividual.Name = "radioIndividual";
			this.radioIndividual.Size = new System.Drawing.Size(207,21);
			this.radioIndividual.TabIndex = 35;
			this.radioIndividual.TabStop = true;
			this.radioIndividual.Text = "Individual Procedures";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioGrouped);
			this.groupBox1.Controls.Add(this.radioIndividual);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(28,229);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(239,70);
			this.groupBox1.TabIndex = 36;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Type";
			// 
			// radioGrouped
			// 
			this.radioGrouped.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioGrouped.Location = new System.Drawing.Point(11,40);
			this.radioGrouped.Name = "radioGrouped";
			this.radioGrouped.Size = new System.Drawing.Size(215,21);
			this.radioGrouped.TabIndex = 36;
			this.radioGrouped.Text = "Grouped By Procedure Code";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(26,324);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(290,20);
			this.label2.TabIndex = 37;
			this.label2.Text = "Only for procedure codes similar to:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textCode
			// 
			this.textCode.Location = new System.Drawing.Point(28,348);
			this.textCode.Name = "textCode";
			this.textCode.Size = new System.Drawing.Size(100,20);
			this.textCode.TabIndex = 38;
			// 
			// checkAllProv
			// 
			this.checkAllProv.Checked = true;
			this.checkAllProv.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAllProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllProv.Location = new System.Drawing.Point(534,30);
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
			this.checkAllClin.Location = new System.Drawing.Point(322,227);
			this.checkAllClin.Name = "checkAllClin";
			this.checkAllClin.Size = new System.Drawing.Size(95,16);
			this.checkAllClin.TabIndex = 54;
			this.checkAllClin.Text = "All";
			this.checkAllClin.Click += new System.EventHandler(this.checkAllClin_Click);
			// 
			// listClin
			// 
			this.listClin.Location = new System.Drawing.Point(322,246);
			this.listClin.Name = "listClin";
			this.listClin.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listClin.Size = new System.Drawing.Size(154,147);
			this.listClin.TabIndex = 53;
			this.listClin.Click += new System.EventHandler(this.listClin_Click);
			// 
			// labelClin
			// 
			this.labelClin.Location = new System.Drawing.Point(319,209);
			this.labelClin.Name = "labelClin";
			this.labelClin.Size = new System.Drawing.Size(104,16);
			this.labelClin.TabIndex = 52;
			this.labelClin.Text = "Clinics";
			this.labelClin.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormRpProcSheet
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(743,437);
			this.Controls.Add(this.checkAllClin);
			this.Controls.Add(this.listClin);
			this.Controls.Add(this.labelClin);
			this.Controls.Add(this.checkAllProv);
			this.Controls.Add(this.textCode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox1);
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
			this.Name = "FormRpProcSheet";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Daily Procedures Report";
			this.Load += new System.EventHandler(this.FormDailySummary_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormDailySummary_Load(object sender, System.EventArgs e) {
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
			whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++) {
					if(i==0) {
						whereProv+=" AND (";
					}
					else {
						whereProv+="OR ";
					}
					whereProv+="procedurelog.ProvNum = "+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
				}
				whereProv+=") ";
			}
			whereClin="";
			if(!checkAllClin.Checked) {
				for(int i=0;i<listClin.SelectedIndices.Count;i++) {
					if(i==0) {
						whereClin+=" AND (";
					}
					else {
						whereClin+="OR ";
					}
					if(listClin.SelectedIndices[i]==0) {
						whereClin+="procedurelog.ClinicNum = 0 ";
					}
					else {
						whereClin+="procedurelog.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			ReportSimpleGrid report=new ReportSimpleGrid();
			if(radioIndividual.Checked){
				CreateIndividual(report);
			}
			else{
				CreateGrouped(report);
			}
		}

		private void CreateIndividual(ReportSimpleGrid report) {
			//added Procnum to retrieve all codes
			report.Query="SELECT procedurelog.ProcDate,CONCAT("
				+"patient.LName,', ',patient.FName,' ',patient.MiddleI) AS plfname, procedurecode.ProcCode,"
				+"procedurelog.ToothNum,procedurecode.Descript,provider.Abbr,"
				+"procedurelog.ClinicNum,"
				+"procedurelog.ProcFee-IFNULL(SUM(claimproc.WriteOff),0) $fee "//if no writeoff, then subtract 0
				+"FROM patient,procedurecode,provider,procedurelog "
				+"LEFT JOIN claimproc ON procedurelog.ProcNum=claimproc.ProcNum "
				+"AND claimproc.Status='7' "//only CapComplete writeoffs are subtracted here.
				+"WHERE procedurelog.ProcStatus = '2' "
				+"AND patient.PatNum=procedurelog.PatNum "
				+"AND procedurelog.CodeNum=procedurecode.CodeNum "
				+"AND provider.ProvNum=procedurelog.ProvNum "
				+whereProv
				+whereClin
				+"AND procedurecode.ProcCode LIKE '%"+POut.String(textCode.Text)+"%' "
				+"AND DATE(procedurelog.ProcDate) >= " +POut.Date(date1.SelectionStart)+" "
				+"AND DATE(procedurelog.ProcDate) <= " +POut.Date(date2.SelectionStart)+" "
				+"GROUP BY procedurelog.ProcNum "
				+"ORDER BY DATE(procedurelog.ProcDate),plfname,procedurecode.ProcCode,ToothNum";
			FormQuery2=new FormQuery(report);
			FormQuery2.IsReport=true;
			DataTable table=report.GetTempTable();
			report.TableQ=new DataTable(null);
			int colI=7;
			if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
				colI=8;
			}
			for(int i=0;i<colI;i++) { //add columns
				report.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			report.InitializeColumns();
			DataRow row;
			double dbl=0;
			for(int i=0;i<table.Rows.Count;i++) {
				row = report.TableQ.NewRow();//create new row called 'row' based on structure of TableQ
				row[0]=PIn.Date(table.Rows[i][0].ToString()).ToShortDateString();
				row[1]=table.Rows[i][1].ToString();//name
				row[2]=table.Rows[i][2].ToString();//adacode
				row[3]=Tooth.ToInternat(table.Rows[i][3].ToString());//tooth
				row[4]=table.Rows[i][4].ToString();//descript
				row[5]=table.Rows[i][5].ToString();//prov
				if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
					row[6]=Clinics.GetDesc(PIn.Long(table.Rows[i][6].ToString()));//clinic
					dbl=PIn.Double(table.Rows[i][7].ToString());//fee
					row[7]=dbl.ToString("n");
					report.ColTotal[7]+=dbl;
				}
				else {
					dbl=PIn.Double(table.Rows[i][7].ToString());//fee
					row[6]=dbl.ToString("n");
					report.ColTotal[6]+=dbl;
				}
				report.TableQ.Rows.Add(row);
			}
			FormQuery2.ResetGrid();			
			report.Title="Daily Procedures";
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
			report.SetColumn(this,0,"Date",80);
			report.SetColumn(this,1,"Patient Name",130);
			report.SetColumn(this,2,"ADA Code",75);
			report.SetColumn(this,3,"Tooth",45);
			report.SetColumn(this,4,"Description",200);
			report.SetColumn(this,5,"Provider",50);
			if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
				report.SetColumn(this,6,"Clinic",70);
				report.SetColumn(this,7,"Fee",90,HorizontalAlignment.Right);
			}
			else{
				report.SetColumn(this,6,"Fee",90,HorizontalAlignment.Right);
			}
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void CreateGrouped(ReportSimpleGrid report) {
			//this would require a temporary table to be able to handle capitation.
			report.Query="SELECT definition.ItemName,procedurecode.ProcCode,procedurecode.Descript,"
        +"Count(*),AVG(procedurelog.ProcFee) $AvgFee,SUM(procedurelog.ProcFee) AS $TotFee "
				+"FROM procedurelog,procedurecode,definition "
				+"WHERE procedurelog.ProcStatus = '2' "
				+"AND procedurelog.CodeNum=procedurecode.CodeNum "
				+"AND definition.DefNum=procedurecode.ProcCat "
				+whereProv
				+whereClin
				+"AND procedurecode.ProcCode LIKE '%"+POut.String(textCode.Text)+"%' "
				+"AND DATE(procedurelog.ProcDate) >= '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
				+"AND DATE(procedurelog.ProcDate) <= '" + date2.SelectionStart.ToString("yyyy-MM-dd")+"' "
				+"GROUP BY procedurecode.ProcCode "
				+"ORDER BY definition.ItemOrder,procedurecode.ProcCode";
			FormQuery2=new FormQuery(report);
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();			
			report.Title="Procedures By Procedure Code";
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
			report.SetColumn(this,0,"Category",150);
			report.SetColumn(this,1,"Code",90);
			report.SetColumn(this,2,"Description",180);
			report.SetColumn(this,3,"Quantity",60,HorizontalAlignment.Right);
			report.SetColumn(this,4,"Average Fee",110,HorizontalAlignment.Right);
			report.SetColumn(this,5,"Total Fees",100,HorizontalAlignment.Right);
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}
		

		
	}
}
















