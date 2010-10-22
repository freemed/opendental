using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Collections;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormRpOutIns:Form {
		private ODGrid gridMain;
		private CheckBox checkPreauth;
		private CheckBox checkProvAll;
		private ListBox listProv;
		private Label label3;
		private ValidNum textDaysOldMin;
		private Label labelDaysOldMin;
		private UI.Button butCancel;
		private ValidNum textDaysOldMax;
    private Label labelDaysOldMax;
		private UI.Button butOK;
    private DateTime dateMin;
    private DateTime dateMax;
    private List<long> provNumList;
    private bool isAllProv;
    private bool isPreauth;
		private DataTable Table;
	
		public FormRpOutIns() {
			InitializeComponent();
			Lan.F(this);

		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
		  DialogResult=DialogResult.Cancel;
		}

		private void InitializeComponent() {
			this.checkPreauth = new System.Windows.Forms.CheckBox();
			this.checkProvAll = new System.Windows.Forms.CheckBox();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.labelDaysOldMin = new System.Windows.Forms.Label();
			this.labelDaysOldMax = new System.Windows.Forms.Label();
			this.textDaysOldMax = new OpenDental.ValidNum();
			this.textDaysOldMin = new OpenDental.ValidNum();
			this.butCancel = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// checkPreauth
			// 
			this.checkPreauth.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPreauth.Checked = true;
			this.checkPreauth.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkPreauth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPreauth.Location = new System.Drawing.Point(17,71);
			this.checkPreauth.Name = "checkPreauth";
			this.checkPreauth.Size = new System.Drawing.Size(145,18);
			this.checkPreauth.TabIndex = 51;
			this.checkPreauth.Text = "Include Preauths";
			this.checkPreauth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkProvAll
			// 
			this.checkProvAll.Checked = true;
			this.checkProvAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkProvAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkProvAll.Location = new System.Drawing.Point(525,28);
			this.checkProvAll.Name = "checkProvAll";
			this.checkProvAll.Size = new System.Drawing.Size(145,18);
			this.checkProvAll.TabIndex = 50;
			this.checkProvAll.Text = "All";
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(525,52);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(163,160);
			this.listProv.TabIndex = 49;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(522,9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104,16);
			this.label3.TabIndex = 48;
			this.label3.Text = "Providers";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelDaysOldMin
			// 
			this.labelDaysOldMin.Location = new System.Drawing.Point(47,38);
			this.labelDaysOldMin.Name = "labelDaysOldMin";
			this.labelDaysOldMin.Size = new System.Drawing.Size(98,18);
			this.labelDaysOldMin.TabIndex = 46;
			this.labelDaysOldMin.Text = "Days Old (min)";
			this.labelDaysOldMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelDaysOldMax
			// 
			this.labelDaysOldMax.Location = new System.Drawing.Point(159,38);
			this.labelDaysOldMax.Name = "labelDaysOldMax";
			this.labelDaysOldMax.Size = new System.Drawing.Size(98,18);
			this.labelDaysOldMax.TabIndex = 46;
			this.labelDaysOldMax.Text = "(max)";
			this.labelDaysOldMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDaysOldMax
			// 
			this.textDaysOldMax.Location = new System.Drawing.Point(263,38);
			this.textDaysOldMax.MaxVal = 255;
			this.textDaysOldMax.MinVal = 0;
			this.textDaysOldMax.Name = "textDaysOldMax";
			this.textDaysOldMax.Size = new System.Drawing.Size(60,20);
			this.textDaysOldMax.TabIndex = 47;
			// 
			// textDaysOldMin
			// 
			this.textDaysOldMin.Location = new System.Drawing.Point(149,37);
			this.textDaysOldMin.MaxVal = 255;
			this.textDaysOldMin.MinVal = 0;
			this.textDaysOldMin.Name = "textDaysOldMin";
			this.textDaysOldMin.Size = new System.Drawing.Size(60,20);
			this.textDaysOldMin.TabIndex = 47;
			this.textDaysOldMin.Text = "30";
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
			this.butCancel.Location = new System.Drawing.Point(606,555);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,23);
			this.butCancel.TabIndex = 45;
			this.butCancel.Text = "&Cancel";
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,218);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(676,322);
			this.gridMain.TabIndex = 1;
			this.gridMain.Title = null;
			this.gridMain.TranslationName = null;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(512,555);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,23);
			this.butOK.TabIndex = 0;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormRpOutIns
			// 
			this.ClientSize = new System.Drawing.Size(700,590);
			this.Controls.Add(this.checkPreauth);
			this.Controls.Add(this.checkProvAll);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDaysOldMax);
			this.Controls.Add(this.textDaysOldMin);
			this.Controls.Add(this.labelDaysOldMax);
			this.Controls.Add(this.labelDaysOldMin);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Name = "FormRpOutIns";
			this.Load += new System.EventHandler(this.FormRpOutIns_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void FormRpOutIns_Load(object sender,EventArgs e) {
			RefreshGrid();
		}

		private void RefreshGrid(){
			if(textDaysOldMin.Text.Trim() == "") {
				dateMin=DateTime.MinValue;
			}
			else {
				dateMin = DateTime.Today.AddDays(-1 * PIn.Int(textDaysOldMin.Text));
			}
			if(textDaysOldMax.Text.Trim() == "") {
				dateMax=DateTime.MinValue;
			}
			else {
				dateMax = DateTime.Today.AddDays(-1 * PIn.Int(textDaysOldMax.Text));
			}
			provNumList=new List<long>();
			for(int i=0;i<listProv.SelectedIndices.Count;i++) {
				provNumList.Add(listProv.SelectedIndices[i]);
			}
			Table=Claims.GetOutInsClaims(isAllProv,provNumList,dateMin,dateMax,isPreauth);
			FillGrid();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g(this,"Carrier"),200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Phone"),85);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Type"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Patient Name"),200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Date of Service"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Date Sent"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Amount"),100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<Table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(Table.Rows[i]["CarrierName"].ToString());
				row.Cells.Add(Table.Rows[i]["HmPhone"].ToString());
				row.Cells.Add(Table.Rows[i]["ClaimType"].ToString());
				row.Cells.Add(Table.Rows[i]["FName"].ToString()+" "+Table.Rows[i]["LName"].ToString());
				row.Cells.Add(PIn.Date(Table.Rows[i]["DateService"].ToString()).ToShortDateString());
				row.Cells.Add(Table.Rows[i]["DateSent"].ToString());
				row.Cells.Add(PIn.Double(Table.Rows[i]["ClaimFee"].ToString()).ToString("F"));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}



	}
}