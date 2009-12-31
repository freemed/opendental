using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpAdjSheet : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;
		private System.ComponentModel.Container components = null;
		private ListBox listProv;
		private Label label1;
		private ListBox listType;
		private Label label2;
		private CheckBox checkAllProv;
		private CheckBox checkAllClin;
		private ListBox listClin;
		private Label labelClin;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpAdjSheet(){
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
			OpenDental.UI.Button butTypeAll;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpAdjSheet));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkAllProv = new System.Windows.Forms.CheckBox();
			this.checkAllClin = new System.Windows.Forms.CheckBox();
			this.listClin = new System.Windows.Forms.ListBox();
			this.labelClin = new System.Windows.Forms.Label();
			butTypeAll = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butTypeAll
			// 
			butTypeAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			butTypeAll.Autosize = true;
			butTypeAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			butTypeAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			butTypeAll.CornerRadius = 4F;
			butTypeAll.Location = new System.Drawing.Point(32,366);
			butTypeAll.Name = "butTypeAll";
			butTypeAll.Size = new System.Drawing.Size(75,26);
			butTypeAll.TabIndex = 40;
			butTypeAll.Text = "&All";
			butTypeAll.Click += new System.EventHandler(this.butTypeAll_Click);
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
			this.butCancel.Location = new System.Drawing.Point(634,390);
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
			this.butOK.Location = new System.Drawing.Point(634,358);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
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
			this.listProv.Location = new System.Drawing.Point(533,51);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(181,147);
			this.listProv.TabIndex = 36;
			this.listProv.Click += new System.EventHandler(this.listProv_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(530,18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104,16);
			this.label1.TabIndex = 35;
			this.label1.Text = "Providers";
			// 
			// listType
			// 
			this.listType.Location = new System.Drawing.Point(32,263);
			this.listType.Name = "listType";
			this.listType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listType.Size = new System.Drawing.Size(120,95);
			this.listType.TabIndex = 39;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32,244);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120,16);
			this.label2.TabIndex = 38;
			this.label2.Text = "Adjustment Types";
			// 
			// checkAllProv
			// 
			this.checkAllProv.Checked = true;
			this.checkAllProv.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAllProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllProv.Location = new System.Drawing.Point(533,34);
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
			this.checkAllClin.Location = new System.Drawing.Point(311,225);
			this.checkAllClin.Name = "checkAllClin";
			this.checkAllClin.Size = new System.Drawing.Size(95,16);
			this.checkAllClin.TabIndex = 51;
			this.checkAllClin.Text = "All";
			this.checkAllClin.Click += new System.EventHandler(this.checkAllClin_Click);
			// 
			// listClin
			// 
			this.listClin.Location = new System.Drawing.Point(311,244);
			this.listClin.Name = "listClin";
			this.listClin.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listClin.Size = new System.Drawing.Size(154,147);
			this.listClin.TabIndex = 50;
			this.listClin.Click += new System.EventHandler(this.listClin_Click);
			// 
			// labelClin
			// 
			this.labelClin.Location = new System.Drawing.Point(308,207);
			this.labelClin.Name = "labelClin";
			this.labelClin.Size = new System.Drawing.Size(104,16);
			this.labelClin.TabIndex = 49;
			this.labelClin.Text = "Clinics";
			this.labelClin.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormRpAdjSheet
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(743,442);
			this.Controls.Add(this.checkAllClin);
			this.Controls.Add(this.listClin);
			this.Controls.Add(this.labelClin);
			this.Controls.Add(this.checkAllProv);
			this.Controls.Add(butTypeAll);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.label2);
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
			this.Name = "FormRpAdjSheet";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Daily Adjustment Report";
			this.Load += new System.EventHandler(this.FormDailyAdjustment_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDailyAdjustment_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;
			for(int i=0;i<ProviderC.List.Length;i++) {
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
			for(int i=0;i<DefC.Short[(int)DefCat.AdjTypes].Length;i++) {
				listType.Items.Add(DefC.Short[(int)DefCat.AdjTypes][i].ItemName);
				listType.SetSelected(i,true);
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

		private void butAll_Click(object sender,EventArgs e) {
			for(int i=0;i<listProv.Items.Count;i++) {
				listProv.SetSelected(i,true);
			}
		}

		private void butTypeAll_Click(object sender,EventArgs e) {
			for(int i=0;i<listType.Items.Count;i++) {
				listType.SetSelected(i,true);
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
			if(listType.SelectedIndices.Count==0) {
				MsgBox.Show(this,"At least one type must be selected.");
				return;
			}
			string whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++) {
					if(i==0) {
						whereProv+=" AND (";
					}
					else {
						whereProv+="OR ";
					}
					whereProv+="adjustment.ProvNum = "+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
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
						whereClin+="adjustment.ClinicNum = 0 ";
					}
					else {
						whereClin+="adjustment.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			string whereType="(";
			for(int i=0;i<listType.SelectedIndices.Count;i++) {
				if(i>0) {
					whereType+="OR ";
				}
				whereType+="adjustment.AdjType = '"
					+POut.Long(DefC.Short[(int)DefCat.AdjTypes][listType.SelectedIndices[i]].DefNum)+"' ";
			}
			whereType+=")";
			ReportSimpleGrid report=new ReportSimpleGrid();
			report.Query="SELECT adjustment.AdjDate,"
				+"CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),"
				+"adjustment.ProvNum,Adjustment.ClinicNum,"
				+"definition.ItemName,adjustment.AdjNote,adjustment.AdjAmt FROM "
				+"adjustment,patient,definition WHERE adjustment.AdjType=definition.DefNum "
			  +"AND patient.PatNum=adjustment.PatNum "
				+whereProv
				+whereClin
				+"AND "+whereType+" "
				+"AND adjustment.AdjDate >= "+POut.Date(date1.SelectionStart)+" "
				+"AND adjustment.AdjDate <= "+POut.Date(date2.SelectionStart);
			report.Query += " ORDER BY adjustment.AdjDate";
			FormQuery2=new FormQuery(report);
			FormQuery2.IsReport=true;
			DataTable table=report.GetTempTable();
			report.TableQ=new DataTable(null);
			int colI=6;
			if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
				colI=7;
			}
			for(int i=0;i<colI;i++) { //add columns
				report.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			report.InitializeColumns();
			DataRow row;
			double dbl;
			for(int i=0;i<table.Rows.Count;i++) {
				row = report.TableQ.NewRow();//create new row called 'row' based on structure of TableQ
				row[0]=PIn.Date(table.Rows[i][0].ToString()).ToShortDateString();
				row[1]=table.Rows[i][1].ToString();//name
				row[2]=Providers.GetAbbr(PIn.Long(table.Rows[i][2].ToString()));//prov
				colI=3;
				if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
					row[colI]=Clinics.GetDesc(PIn.Long(table.Rows[i][3].ToString()));//clinic
					colI++;
				}
				row[colI]=table.Rows[i][4].ToString();//Type
				colI++;
				row[colI]=table.Rows[i][5].ToString();//Note
				colI++;
				dbl=PIn.Double(table.Rows[i][6].ToString());//Amount
				row[colI]=dbl.ToString("n");
				report.ColTotal[colI]+=dbl;
				report.TableQ.Rows.Add(row);
			}
			FormQuery2.ResetGrid();		
			report.Title="Daily Adjustments";
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
			report.SetColumn(this,0,"Date",90);
			report.SetColumn(this,1,"Patient Name",130);
			report.SetColumn(this,2,"Prov",60);
			if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
				report.SetColumn(this,3,"Clinic",70);
				report.SetColumn(this,4,"Adjustment Type",150);
				report.SetColumn(this,5,"Note",150);
				report.SetColumn(this,6,"Amount",75,HorizontalAlignment.Right);
			}
			else {
				report.SetColumn(this,3,"Adjustment Type",150);
				report.SetColumn(this,4,"Note",150);
				report.SetColumn(this,5,"Amount",75,HorizontalAlignment.Right);
			}
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}
		

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}
