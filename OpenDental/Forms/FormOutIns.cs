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
		private UI.Button butPrint;
		private long selectedPatNum;
	
		public FormRpOutIns() {
			InitializeComponent();
			Lan.F(this);

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
			this.butPrint = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// checkPreauth
			// 
			this.checkPreauth.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPreauth.Checked = true;
			this.checkPreauth.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkPreauth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPreauth.Location = new System.Drawing.Point(140,84);
			this.checkPreauth.Name = "checkPreauth";
			this.checkPreauth.Size = new System.Drawing.Size(145,18);
			this.checkPreauth.TabIndex = 51;
			this.checkPreauth.Text = "Include Preauths";
			this.checkPreauth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPreauth.CheckedChanged += new System.EventHandler(this.checkPreauth_CheckedChanged);
			// 
			// checkProvAll
			// 
			this.checkProvAll.Checked = true;
			this.checkProvAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkProvAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkProvAll.Location = new System.Drawing.Point(425,28);
			this.checkProvAll.Name = "checkProvAll";
			this.checkProvAll.Size = new System.Drawing.Size(145,18);
			this.checkProvAll.TabIndex = 50;
			this.checkProvAll.Text = "All";
			this.checkProvAll.CheckedChanged += new System.EventHandler(this.checkProvAll_CheckedChanged);
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(425,52);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(163,160);
			this.listProv.TabIndex = 49;
			this.listProv.SelectedIndexChanged += new System.EventHandler(this.listProv_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(422,9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104,16);
			this.label3.TabIndex = 48;
			this.label3.Text = "Providers";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelDaysOldMin
			// 
			this.labelDaysOldMin.Location = new System.Drawing.Point(140,27);
			this.labelDaysOldMin.Name = "labelDaysOldMin";
			this.labelDaysOldMin.Size = new System.Drawing.Size(127,18);
			this.labelDaysOldMin.TabIndex = 46;
			this.labelDaysOldMin.Text = "Days Old (min)";
			this.labelDaysOldMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelDaysOldMax
			// 
			this.labelDaysOldMax.Location = new System.Drawing.Point(140,52);
			this.labelDaysOldMax.Name = "labelDaysOldMax";
			this.labelDaysOldMax.Size = new System.Drawing.Size(127,18);
			this.labelDaysOldMax.TabIndex = 46;
			this.labelDaysOldMax.Text = "(max)";
			this.labelDaysOldMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDaysOldMax
			// 
			this.textDaysOldMax.Location = new System.Drawing.Point(271,52);
			this.textDaysOldMax.MaxVal = 255;
			this.textDaysOldMax.MinVal = 0;
			this.textDaysOldMax.Name = "textDaysOldMax";
			this.textDaysOldMax.Size = new System.Drawing.Size(60,20);
			this.textDaysOldMax.TabIndex = 47;
			this.textDaysOldMax.TextChanged += new System.EventHandler(this.textDaysOldMax_TextChanged);
			// 
			// textDaysOldMin
			// 
			this.textDaysOldMin.Location = new System.Drawing.Point(271,27);
			this.textDaysOldMin.MaxVal = 255;
			this.textDaysOldMin.MinVal = 0;
			this.textDaysOldMin.Name = "textDaysOldMin";
			this.textDaysOldMin.Size = new System.Drawing.Size(60,20);
			this.textDaysOldMin.TabIndex = 47;
			this.textDaysOldMin.Text = "30";
			this.textDaysOldMin.TextChanged += new System.EventHandler(this.textDaysOldMin_TextChanged);
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
			this.butCancel.Location = new System.Drawing.Point(670,555);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,23);
			this.butCancel.TabIndex = 45;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,218);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(740,322);
			this.gridMain.TabIndex = 1;
			this.gridMain.Title = null;
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(576,555);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,23);
			this.butOK.TabIndex = 0;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(17,555);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(79,23);
			this.butPrint.TabIndex = 52;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// FormRpOutIns
			// 
			this.ClientSize = new System.Drawing.Size(764,590);
			this.Controls.Add(this.butPrint);
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
			this.Text = "Outstanding Insurance Claims";
			this.Load += new System.EventHandler(this.FormRpOutIns_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void FormRpOutIns_Load(object sender,EventArgs e) {
			FillProvs();
			RefreshGrid();
		}

		private void FillProvs() {
			for(int i=0;i<ProviderC.List.Length;i++) {
				listProv.Items.Add(ProviderC.List[i].GetLongDesc());
			}
			if(listProv.Items.Count>0) {
				listProv.SelectedIndex=0;
			}
			checkProvAll.Checked=true;
			listProv.Visible=false;
		}

		private void RefreshGrid(){
			if(textDaysOldMin.Text.Trim()=="" || PIn.Double(textDaysOldMin.Text)==0) {
				dateMin=DateTime.MinValue;
			}
			else {
				dateMin = DateTime.Today.AddDays(-1 * PIn.Int(textDaysOldMin.Text));
			}
			if(textDaysOldMax.Text.Trim()=="" || PIn.Double(textDaysOldMax.Text)==0) {
				dateMax=DateTime.MinValue;
			}
			else {
				dateMax = DateTime.Today.AddDays(-1 * PIn.Int(textDaysOldMax.Text));
			}
			isAllProv=checkProvAll.Checked;
			if(!isAllProv) {
				provNumList=new List<long>();
				for(int i=0;i<listProv.SelectedIndices.Count;i++) {
					provNumList.Add(listProv.SelectedIndices[i]);
				}
			}
			isPreauth=checkPreauth.Checked;
			Table=Claims.GetOutInsClaims(isAllProv,provNumList,dateMin,dateMax,isPreauth);
			FillGrid();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g(this,"Carrier"),165);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Phone"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Type"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Patient Name"),165);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Date of Service"),88);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Date Sent"),88);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Amount"),65,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			string type;
			for(int i=0;i<Table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(Table.Rows[i]["CarrierName"].ToString());
				row.Cells.Add(Table.Rows[i]["HmPhone"].ToString());
				type=Table.Rows[i]["ClaimType"].ToString();
				switch(type){
					case "P":
						type="Primary";
						break;
					case "S":
						type="Secondary";
						break;
					case "Preauth":
						type="Preauth";
						break;
					case "Other":
						type="Other";
						break;
					case "Cap":
						type="Capitation";
						break;
					case "Med":
						type="Medical";//For possible future use.
						break;
					default:
						type="Error";//Not allowed to be blank.
						break;
				}
				row.Cells.Add(type);
				row.Cells.Add(Table.Rows[i]["LName"].ToString()+", "+Table.Rows[i]["FName"].ToString()+" "+Table.Rows[i]["MiddleI"].ToString());
				row.Cells.Add(PIn.Date(Table.Rows[i]["DateService"].ToString()).ToShortDateString());
				row.Cells.Add(PIn.Date(Table.Rows[i]["DateSent"].ToString()).ToShortDateString());
				row.Cells.Add("$"+PIn.Double(Table.Rows[i]["ClaimFee"].ToString()).ToString("F"));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void listProv_SelectedIndexChanged(object sender,EventArgs e) {
			RefreshGrid();
		}

		private void checkProvAll_CheckedChanged(object sender,EventArgs e) {
			if(checkProvAll.Checked) {
				listProv.Visible=false;
			}
			else {
				listProv.Visible=true;
			}
			RefreshGrid();
		}

		private void label3_Click(object sender,EventArgs e) {
			RefreshGrid();
		}

		private void textDaysOldMin_TextChanged(object sender,EventArgs e) {
			RefreshGrid();
		}

		private void checkPreauth_CheckedChanged(object sender,EventArgs e) {
			RefreshGrid();
		}

		private void textDaysOldMax_TextChanged(object sender,EventArgs e) {
			RefreshGrid();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Claim claim=Claims.GetClaim(PIn.Long(Table.Rows[e.Row]["ClaimNum"].ToString()));
			Patient pat=Patients.GetPat(claim.PatNum);
			Family fam=Patients.GetFamily(pat.PatNum);
			FormClaimEdit FormCE=new FormClaimEdit(claim,pat,fam);
			FormCE.IsNew=false;
			FormCE.ShowDialog();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
			Close();
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
			Close();
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			GotoModule.GotoAccount(PIn.Long(Table.Rows[e.Row]["PatNum"].ToString()));
		}

		private void butPrint_Click(object sender,EventArgs e) {
			//Validating of parameters is done during RefreshGrid().
			ReportSimpleGrid report=new ReportSimpleGrid();
			report.Query = "SELECT carrier.CarrierName,patient.HmPhone,claim.ClaimType,patient.FName,patient.LName,patient.MiddleI,patient.PatNum,claim.DateService,claim.DateSent,claim.ClaimFee,claim.ClaimNum "
				+"FROM carrier,patient,claim,insplan "
				+"WHERE carrier.CarrierNum = insplan.CarrierNum "
				+"AND claim.PlanNum = insplan.PlanNum "
				+"AND claim.PatNum = patient.PatNum "
				+"AND claim.ClaimStatus='S' ";
			if(dateMin!=DateTime.MinValue) {
				report.Query+="AND claim.DateSent <= "+POut.Date(dateMin)+" ";
			}
			if(dateMax!=DateTime.MinValue) {
				report.Query+="AND claim.DateSent >= "+POut.Date(dateMax)+" ";
			}
			if(!isAllProv) {
				if(provNumList.Count>0) {
					report.Query+="AND claim.ProvBill IN (";
					report.Query+=""+provNumList[0];
					for(int i=1;i<provNumList.Count;i++) {
						report.Query+=","+provNumList[i];
					}
					report.Query+=") ";
				}
			}
			if(!isPreauth) {
				report.Query+="AND claim.ClaimType!='Preauth' ";
			}
			report.Query+="ORDER BY carrier.Phone,insplan.PlanNum, carrier.Phone,insplan.PlanNum";
			FormQuery FormQuery2=new FormQuery(report);
			FormQuery2.IsReport=true;
			DataTable tableTemp= report.GetTempTable();
			report.TableQ=new DataTable(null);//new table no name
			for(int i=0;i<6;i++) {//add columns
				report.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			report.InitializeColumns();
			for(int i=0;i<tableTemp.Rows.Count;i++) {//loop through data rows
				DataRow row = report.TableQ.NewRow();//create new row called 'row' based on structure of TableQ
				//start filling 'row'. First column is carrier:
				row[0]=tableTemp.Rows[i][0];
				row[1]=tableTemp.Rows[i][7];
				if(PIn.String(tableTemp.Rows[i][2].ToString())=="P")
					row[2]="Primary";
				if(PIn.String(tableTemp.Rows[i][2].ToString())=="S")
					row[2]="Secondary";
				if(PIn.String(tableTemp.Rows[i][2].ToString())=="PreAuth")
					row[2]="PreAuth";
				if(PIn.String(tableTemp.Rows[i][2].ToString())=="Other")
					row[2]="Other";
				row[3]=tableTemp.Rows[i][4];
				row[4]=(PIn.Date(tableTemp.Rows[i][3].ToString())).ToString("d");
				row[5]=PIn.Double(tableTemp.Rows[i][6].ToString()).ToString("F");
				//TimeSpan d = DateTime.Today.Subtract((PIn.PDate(tableTemp.Rows[i][5].ToString())));
				//if(d.Days>5000)
				//	row[4]="";
				//else
				//	row[4]=d.Days.ToString();
				report.ColTotal[5]+=PIn.Double(tableTemp.Rows[i][6].ToString());
				report.TableQ.Rows.Add(row);
			}
			FormQuery2.ResetGrid();//this is a method in FormQuery;
			report.Title="OUTSTANDING INSURANCE CLAIMS";
			report.SubTitle.Add(PrefC.GetString(PrefName.PracticeTitle));
			report.SubTitle.Add("Sent before "+dateMin.Date.ToShortDateString());
			report.ColPos[0]=20;
			report.ColPos[1]=210;
			report.ColPos[2]=330;
			report.ColPos[3]=430;
			report.ColPos[4]=600;
			report.ColPos[5]=690;
			report.ColPos[6]=770;
			report.ColCaption[0]=Lan.g(this,"Carrier");
			report.ColCaption[1]=Lan.g(this,"Phone");
			report.ColCaption[2]=Lan.g(this,"Type");
			report.ColCaption[3]=Lan.g(this,"Patient Name");
			report.ColCaption[4]=Lan.g(this,"Date of Service");
			report.ColCaption[5]=Lan.g(this,"Amount");
			report.ColAlign[5]=HorizontalAlignment.Right;
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}





	}
}