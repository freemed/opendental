using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using System.Data;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpProdInc : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;

		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listProv;
		private OpenDental.UI.Button butAll;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton radioDaily;
		private System.Windows.Forms.RadioButton radioMonthly;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textToday;
		private OpenDental.ValidDate textDateFrom;
		private OpenDental.ValidDate textDateTo;
		private System.Windows.Forms.RadioButton radioAnnual;
		private System.Windows.Forms.GroupBox groupBox2;
		private OpenDental.UI.Button butThis;
		private OpenDental.UI.Button butLeft;
		private OpenDental.UI.Button butRight;
		private FormQuery FormQuery2;
		private DateTime dateFrom;
		private ListBox listClinic;
		private Label labelClinic;
		private DateTime dateTo;
		///<summary>Can be set externally when automating.</summary>
		public string DailyMonthlyAnnual;
		///<summary>If set externally, then this sets the date on startup.</summary>
		public DateTime DateStart;
		///<summary>If set externally, then this sets the date on startup.</summary>
		public DateTime DateEnd;

		///<summary></summary>
		public FormRpProdInc(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpProdInc));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listProv = new System.Windows.Forms.ListBox();
			this.butAll = new OpenDental.UI.Button();
			this.radioMonthly = new System.Windows.Forms.RadioButton();
			this.radioDaily = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioAnnual = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textDateTo = new OpenDental.ValidDate();
			this.textDateFrom = new OpenDental.ValidDate();
			this.label4 = new System.Windows.Forms.Label();
			this.textToday = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butRight = new OpenDental.UI.Button();
			this.butThis = new OpenDental.UI.Button();
			this.butLeft = new OpenDental.UI.Button();
			this.listClinic = new System.Windows.Forms.ListBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(554,342);
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
			this.butOK.Location = new System.Drawing.Point(554,307);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(35,128);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104,16);
			this.label1.TabIndex = 29;
			this.label1.Text = "Providers";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(37,147);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(123,186);
			this.listProv.TabIndex = 30;
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(37,342);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(70,25);
			this.butAll.TabIndex = 31;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// radioMonthly
			// 
			this.radioMonthly.Checked = true;
			this.radioMonthly.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioMonthly.Location = new System.Drawing.Point(14,40);
			this.radioMonthly.Name = "radioMonthly";
			this.radioMonthly.Size = new System.Drawing.Size(104,17);
			this.radioMonthly.TabIndex = 33;
			this.radioMonthly.TabStop = true;
			this.radioMonthly.Text = "Monthly";
			this.radioMonthly.Click += new System.EventHandler(this.radioMonthly_Click);
			// 
			// radioDaily
			// 
			this.radioDaily.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioDaily.Location = new System.Drawing.Point(14,21);
			this.radioDaily.Name = "radioDaily";
			this.radioDaily.Size = new System.Drawing.Size(104,17);
			this.radioDaily.TabIndex = 34;
			this.radioDaily.Text = "Daily";
			this.radioDaily.Click += new System.EventHandler(this.radioDaily_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioAnnual);
			this.groupBox1.Controls.Add(this.radioDaily);
			this.groupBox1.Controls.Add(this.radioMonthly);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(37,13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(123,84);
			this.groupBox1.TabIndex = 35;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Report Type";
			// 
			// radioAnnual
			// 
			this.radioAnnual.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioAnnual.Location = new System.Drawing.Point(14,59);
			this.radioAnnual.Name = "radioAnnual";
			this.radioAnnual.Size = new System.Drawing.Size(104,17);
			this.radioAnnual.TabIndex = 35;
			this.radioAnnual.Text = "Annual";
			this.radioAnnual.Click += new System.EventHandler(this.radioAnnual_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9,79);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82,18);
			this.label2.TabIndex = 37;
			this.label2.Text = "From";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(7,105);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82,18);
			this.label3.TabIndex = 39;
			this.label3.Text = "To";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(95,104);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(100,20);
			this.textDateTo.TabIndex = 44;
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(95,77);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(100,20);
			this.textDateFrom.TabIndex = 43;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(305,23);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(127,20);
			this.label4.TabIndex = 41;
			this.label4.Text = "Today\'s Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textToday
			// 
			this.textToday.Location = new System.Drawing.Point(434,21);
			this.textToday.Name = "textToday";
			this.textToday.Size = new System.Drawing.Size(100,20);
			this.textToday.TabIndex = 42;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.butRight);
			this.groupBox2.Controls.Add(this.butThis);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.textDateFrom);
			this.groupBox2.Controls.Add(this.textDateTo);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.butLeft);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(348,70);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(281,144);
			this.groupBox2.TabIndex = 43;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Date Range";
			// 
			// butRight
			// 
			this.butRight.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRight.Autosize = true;
			this.butRight.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRight.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRight.CornerRadius = 4F;
			this.butRight.Image = global::OpenDental.Properties.Resources.Right;
			this.butRight.Location = new System.Drawing.Point(205,30);
			this.butRight.Name = "butRight";
			this.butRight.Size = new System.Drawing.Size(45,26);
			this.butRight.TabIndex = 46;
			this.butRight.Click += new System.EventHandler(this.butRight_Click);
			// 
			// butThis
			// 
			this.butThis.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butThis.Autosize = true;
			this.butThis.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butThis.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butThis.CornerRadius = 4F;
			this.butThis.Location = new System.Drawing.Point(95,30);
			this.butThis.Name = "butThis";
			this.butThis.Size = new System.Drawing.Size(101,26);
			this.butThis.TabIndex = 45;
			this.butThis.Text = "This";
			this.butThis.Click += new System.EventHandler(this.butThis_Click);
			// 
			// butLeft
			// 
			this.butLeft.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLeft.Autosize = true;
			this.butLeft.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLeft.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLeft.CornerRadius = 4F;
			this.butLeft.Image = global::OpenDental.Properties.Resources.Left;
			this.butLeft.Location = new System.Drawing.Point(41,30);
			this.butLeft.Name = "butLeft";
			this.butLeft.Size = new System.Drawing.Size(45,26);
			this.butLeft.TabIndex = 44;
			this.butLeft.Click += new System.EventHandler(this.butLeft_Click);
			// 
			// listClinic
			// 
			this.listClinic.Location = new System.Drawing.Point(179,147);
			this.listClinic.Name = "listClinic";
			this.listClinic.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listClinic.Size = new System.Drawing.Size(123,186);
			this.listClinic.TabIndex = 45;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(177,128);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(104,16);
			this.labelClinic.TabIndex = 44;
			this.labelClinic.Text = "Clinics";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormRpProdInc
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(662,394);
			this.Controls.Add(this.listClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.textToday);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpProdInc";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Production and Income Report";
			this.Load += new System.EventHandler(this.FormProduction_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		private void FormProduction_Load(object sender, System.EventArgs e) {
			textToday.Text=DateTime.Today.ToShortDateString();
			for(int i=0;i<Providers.List.Length;i++){
				listProv.Items.Add(Providers.List[i].Abbr+" - "+Providers.List[i].LName+", "
					+Providers.List[i].FName);
				listProv.SetSelected(i,true);
			}
			//if(PrefB.GetBool("EasyNoClinics")){
				listClinic.Visible=false;
				labelClinic.Visible=false;
			/*}
			else{
				listClinic.Items.Add(Lan.g(this,"Unassigned"));
				listClinic.SetSelected(0,true);
				for(int i=0;i<Clinics.List.Length;i++) {
					listClinic.Items.Add(Clinics.List[i].Description);
					listClinic.SetSelected(i+1,true);
				}
			}*/
			switch(DailyMonthlyAnnual){
				case "Daily":
					radioDaily.Checked=true;
					break;
				case "Monthly":
					radioMonthly.Checked=true;
					break;
				case "Annual":
					radioAnnual.Checked=true;
					break;
			}
			SetDates();
			if(DateStart.Year>1880){
				textDateFrom.Text=DateStart.ToShortDateString();
				textDateTo.Text=DateEnd.ToShortDateString();
				switch(DailyMonthlyAnnual) {
					case "Daily":
						RunDaily();
						break;
					case "Monthly":
						RunMonthly();
						break;
					case "Annual":
						RunAnnual();
						break;
				}
				Close();
			}
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			for(int i=0;i<listProv.Items.Count;i++){
				listProv.SetSelected(i,true);
			}
		}

		private void radioDaily_Click(object sender, System.EventArgs e) {
			SetDates();
		}

		private void radioMonthly_Click(object sender, System.EventArgs e) {
			SetDates();
		}

		private void radioAnnual_Click(object sender, System.EventArgs e) {
			SetDates();
		}

		private void SetDates(){
			if(radioDaily.Checked){
				textDateFrom.Text=DateTime.Today.ToShortDateString();
				textDateTo.Text=DateTime.Today.ToShortDateString();
				butThis.Text=Lan.g(this,"Today");
			}
			else if(radioMonthly.Checked){
				textDateFrom.Text=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1).ToShortDateString();
				textDateTo.Text=new DateTime(DateTime.Today.Year,DateTime.Today.Month
					,DateTime.DaysInMonth(DateTime.Today.Year,DateTime.Today.Month)).ToShortDateString();
				butThis.Text=Lan.g(this,"This Month");
			}
			else{//annual
				textDateFrom.Text=new DateTime(DateTime.Today.Year,1,1).ToShortDateString();
				textDateTo.Text=new DateTime(DateTime.Today.Year,12,31).ToShortDateString();
				butThis.Text=Lan.g(this,"This Year");
			}
		}

		private void butThis_Click(object sender, System.EventArgs e) {
			SetDates();
		}

		private void butLeft_Click(object sender, System.EventArgs e) {
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			dateFrom=PIn.PDate(textDateFrom.Text);
			dateTo=PIn.PDate(textDateTo.Text);
			if(radioDaily.Checked){
				textDateFrom.Text=dateFrom.AddDays(-1).ToShortDateString();
				textDateTo.Text=dateTo.AddDays(-1).ToShortDateString();
			}
			else if(radioMonthly.Checked){
				bool toLastDay=false;
				if(CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(dateTo.Year,dateTo.Month)==dateTo.Day){
					toLastDay=true;
				}
				textDateFrom.Text=dateFrom.AddMonths(-1).ToShortDateString();
				textDateTo.Text=dateTo.AddMonths(-1).ToShortDateString();
				dateTo=PIn.PDate(textDateTo.Text);
				if(toLastDay){
					textDateTo.Text=new DateTime(dateTo.Year,dateTo.Month,
						CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(dateTo.Year,dateTo.Month))
						.ToShortDateString();
				}
			}
			else{//annual
				textDateFrom.Text=dateFrom.AddYears(-1).ToShortDateString();
				textDateTo.Text=dateTo.AddYears(-1).ToShortDateString();
			}
		}

		private void butRight_Click(object sender, System.EventArgs e) {
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			dateFrom=PIn.PDate(textDateFrom.Text);
			dateTo=PIn.PDate(textDateTo.Text);
			if(radioDaily.Checked){
				textDateFrom.Text=dateFrom.AddDays(1).ToShortDateString();
				textDateTo.Text=dateTo.AddDays(1).ToShortDateString();
			}
			else if(radioMonthly.Checked){
				bool toLastDay=false;
				if(CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(dateTo.Year,dateTo.Month)==dateTo.Day){
					toLastDay=true;
				}
				textDateFrom.Text=dateFrom.AddMonths(1).ToShortDateString();
				textDateTo.Text=dateTo.AddMonths(1).ToShortDateString();
				dateTo=PIn.PDate(textDateTo.Text);
				if(toLastDay){
					textDateTo.Text=new DateTime(dateTo.Year,dateTo.Month,
						CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(dateTo.Year,dateTo.Month))
						.ToShortDateString();
				}
			}
			else{//annual
				textDateFrom.Text=dateFrom.AddYears(1).ToShortDateString();
				textDateTo.Text=dateTo.AddYears(1).ToShortDateString();
			}
		}

		private void RunDaily(){
			dateFrom=PIn.PDate(textDateFrom.Text);
			dateTo=PIn.PDate(textDateTo.Text);
			//Date
			//PatientName
			//Description
			//Prov
			//Production
			//Adjustments
			//Pt Income
			//Ins Income
			//last column is a unique id that is not displayed
			string whereProv="(";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+="OR ";
				}
				whereProv+="procedurelog.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
			}
			whereProv+=")";
			Queries.CurReport=new ReportOld();
			//Procedures------------------------------------------------------------------------------
			Queries.CurReport.Query="(SELECT "
				+"procedurelog.ProcDate AS procdate,"
				+"CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI) AS namelf,"
				+"procedurecode.Descript,"
				+"provider.Abbr,"
				+"procedurelog.ProcFee-IFNULL(SUM(claimproc.WriteOff),0) $fee,"//if no writeoff, then subtract 0
				+"'0000000000000' $Adj,"
				+"'0000000000000' $InsW,"
				+"'0000000000000' $PtInc,"
				+"'0000000000000' $InsInc,"
				+"procedurelog.ProcNum "
				+"FROM patient,procedurecode,provider,procedurelog "
				+"LEFT JOIN claimproc ON procedurelog.ProcNum=claimproc.ProcNum "
				+"AND claimproc.Status='7' "//only CapComplete writeoffs are subtracted here.
				+"WHERE procedurelog.ProcStatus = '2' "
				+"AND patient.PatNum=procedurelog.PatNum "
				+"AND procedurelog.CodeNum=procedurecode.CodeNum "
				+"AND provider.ProvNum=procedurelog.ProvNum "
				+"AND "+whereProv+" "
				+"AND procedurelog.ProcDate >= " +POut.PDate(dateFrom)+" "
				+"AND procedurelog.ProcDate <= " +POut.PDate(dateTo)+" "
				+"GROUP BY procedurelog.ProcNum "
				+") UNION (";
			//Adjustments-----------------------------------------------------------------------------
			whereProv="(";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+="OR ";
				}
				whereProv+="adjustment.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
			}
			whereProv+=")";
			Queries.CurReport.Query+="SELECT "
				+"adjustment.AdjDate,"
				+"CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI),"
				+"definition.ItemName,"
				+"provider.Abbr,"
				+"'0',"
				+"adjustment.AdjAmt,"
				+"'0',"
				+"'0',"
				+"'0',"
				+"adjustment.AdjNum "
				+"FROM adjustment,patient,definition,provider "
				+"WHERE adjustment.AdjType=definition.DefNum "
				+"AND provider.ProvNum=adjustment.ProvNum "
			  +"AND patient.PatNum=adjustment.PatNum "
				+"AND "+whereProv+" "
				+"AND adjustment.AdjDate >= "+POut.PDate(dateFrom)+" "
				+"AND adjustment.AdjDate <= "+POut.PDate(dateTo)
				+") UNION (";
			//Insurance Writeoff---added spk 5/19/05--------------------------------------
			whereProv="(";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+="OR ";
				}
				whereProv+="claimproc.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
			}
			whereProv+=")";
			Queries.CurReport.Query+="SELECT "
				+"claimproc.DateCP,"
				+"CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI),"
				+"carrier.CarrierName,"
				+"provider.Abbr,"
				+"'0',"
				+"'0',"
				+"-SUM(claimproc.WriteOff),"
				+"'0',"
				+"'0',"
				+"claimproc.ClaimNum "
				+"FROM claimproc,insplan,patient,carrier,provider "//,claimpayment "
				//claimpayment date is used because DateCP was not forced to be the same until version 3.0
				//+"WHERE claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum "
				+"WHERE provider.ProvNum = claimproc.ProvNum "
				+"AND claimproc.PlanNum = insplan.PlanNum "
				+"AND claimproc.PatNum = patient.PatNum "
				+"AND carrier.CarrierNum = insplan.CarrierNum "
				+"AND "+whereProv+" "
				+"AND (claimproc.Status=1 OR claimproc.Status=4) "//received or supplemental
				+"AND claimproc.WriteOff > '.0001' "
				+"AND claimproc.DateCP >= "+POut.PDate(dateFrom)+" "
				+"AND claimproc.DateCP <= "+POut.PDate(dateTo)+" "
				+"GROUP BY claimproc.ClaimNum"
				+") UNION (";
			//Patient Income------------------------------------------------------------------------------
			whereProv="(";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+="OR ";
				}
				whereProv+="paysplit.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
			}
			whereProv+=")";
			Queries.CurReport.Query+="SELECT "
				+"paysplit.DatePay,"
				+"CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI),"
				+"definition.ItemName,"
				+"provider.Abbr,"
				+"'0',"
				+"'0',"
				+"'0',"
				+"SUM(paysplit.SplitAmt),"
				+"'0',"
				+"payment.PayNum "
				+"FROM payment,patient,provider,paysplit,definition "
				+"WHERE payment.PayNum=paysplit.PayNum "
				+"AND patient.PatNum=paysplit.PatNum "
				//notice that patient and prov are accurate, but if more than one, then only one shows
				+"AND provider.ProvNum=paysplit.ProvNum "
				+"AND payment.PayType=definition.DefNum "
				+"AND "+whereProv+" "
				+"AND payment.PayDate >= "+POut.PDate(dateFrom)+" "
				+"AND payment.PayDate <= "+POut.PDate(dateTo)+" "
				+"GROUP BY payment.PayNum"
				+") UNION (";
			//Insurance Income----------------------------------------------------------------------------
			whereProv="(";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+="OR ";
				}
				whereProv+="claimproc.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
			}
			whereProv+=")";
			Queries.CurReport.Query+="SELECT "
				+"claimpayment.CheckDate,"
				+"CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI),"
				+"carrier.CarrierName,"
				+"provider.Abbr,"
				+"'0',"
				+"'0',"
				+"'0',"
				+"'0',"
				+"SUM(claimproc.InsPayAmt),"
				+"claimproc.ClaimNum "
				+"FROM claimproc,insplan,patient,carrier,provider,claimpayment "
				//claimpayment date is used because DateCP was not forced to be the same until version 3.0
				+"WHERE claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum "
				+"AND provider.ProvNum=claimproc.ProvNum "
				+"AND claimproc.PlanNum = insplan.PlanNum "
				+"AND claimproc.PatNum = patient.PatNum "
				+"AND carrier.CarrierNum = insplan.CarrierNum "
				+"AND "+whereProv+" "
				+"AND (claimproc.Status=1 OR claimproc.Status=4) "//received or supplemental
				+"AND claimpayment.CheckDate >= "+POut.PDate(dateFrom)+" "
				+"AND claimpayment.CheckDate <= "+POut.PDate(dateTo)+" "
				+"GROUP BY claimproc.ClaimNum"
				//+") ORDER BY procdate,namelf";//FIXME:UNION-ORDER-BY
				+") ORDER BY 1,2";
			//MessageBox.Show(Queries.CurReport.Query);
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();			
			Queries.CurReport.Title="Daily Production and Income";
			Queries.CurReport.SubTitle=new string[2];
			Queries.CurReport.SubTitle[0]=((Pref)PrefB.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=dateFrom.ToString("d")
				+" - "+dateTo.ToString("d");	
			Queries.CurReport.ColPos=new int[11];
			Queries.CurReport.ColCaption=new string[10];
			Queries.CurReport.ColAlign=new HorizontalAlignment[10];
			Queries.CurReport.ColPos[0]=10;
			Queries.CurReport.ColPos[1]=90;
			Queries.CurReport.ColPos[2]=220;
			Queries.CurReport.ColPos[3]=385;
			Queries.CurReport.ColPos[4]=440;
			Queries.CurReport.ColPos[5]=505;
			Queries.CurReport.ColPos[6]=575;
			Queries.CurReport.ColPos[7]=640;
			Queries.CurReport.ColPos[8]=705;
			Queries.CurReport.ColPos[9]=770;  // added spk 5/19/05	
			Queries.CurReport.ColPos[10]=1050;// way off the righthand side
			Queries.CurReport.ColCaption[0]="Date";
			Queries.CurReport.ColCaption[1]="Patient Name";			
			Queries.CurReport.ColCaption[2]="Description";
			Queries.CurReport.ColCaption[3]="Prov";
			Queries.CurReport.ColCaption[4]="Production";
			Queries.CurReport.ColCaption[5]="Adjustments";
			Queries.CurReport.ColCaption[6]="Writeoff";	// added spk 
			Queries.CurReport.ColCaption[7]="Pt Income";
			Queries.CurReport.ColCaption[8]="Ins Income";
			Queries.CurReport.ColCaption[9]="";
			Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[5]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[6]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[7]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[8]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[9]=HorizontalAlignment.Right;
			Queries.CurReport.Summary=new string[3];
			Queries.CurReport.Summary[0]
				//=Lan.g(this,"Total Production (Production + Adjustments):")+" "
				//+(Queries.CurReport.ColTotal[4]+Queries.CurReport.ColTotal[5]).ToString("c");
				// added spk 5/19/05
				=Lan.g(this,"Total Production (Production + Adjustments - Writeoffs):")+" "
				+(Queries.CurReport.ColTotal[4]+Queries.CurReport.ColTotal[5]+Queries.CurReport.ColTotal[6])
				.ToString("c");
			Queries.CurReport.Summary[2]
				=Lan.g(this,"Total Income (Pt Income + Ins Income):")+" "
				+(Queries.CurReport.ColTotal[7]+Queries.CurReport.ColTotal[8]).ToString("c");
			FormQuery2.ShowDialog();

		}

		private void RunMonthly(){
			dateFrom=PIn.PDate(textDateFrom.Text);
			dateTo=PIn.PDate(textDateTo.Text);
/*  There are 8 temp tables  
 *  TableCharge: Holds sum of all charges for a certain date.
 *  TableCapWriteoff: Holds capComplete writeoffs which will be subtracted from Charges.  They are subtracted from charges to give the illusion of not so much being charged in the first place. Calculated using dateCP, which should be same as DateProc. 
 *  TableInsWriteoff: (not implemented yet) Writeoffs from claims received. Displayed in separate column so it won't mysteriously change the charges and all reports will match.
 *  TableSched: Holds Scheduled but not charged procedures
 *  TableCapEstWriteoff: (not implemented yet) capEstimate writeoffs. These will be subtracted from scheduled treatment
 *  TablePay: Holds sum of all Patient payments for a certain date.
 *  TableIns: Holds sum of all Insurance payments for a certain date.--- added by SPK 3/16/04
 *  TableAdj: Holds sum of all adjustments for a certain date.
 * GROUP BY is used to group dates together so that amounts are summed for each date
 */
			DataTable TableCharge=     new DataTable();  //charges
			DataTable TableCapWriteoff=new DataTable();  //capComplete writeoffs
			DataTable TableInsWriteoff=new DataTable();  //ins writeoffs
			DataTable TablePay=        new DataTable();  //payments - Patient
			DataTable TableIns=        new DataTable();  //payments - Ins, added SPK 
			DataTable TableAdj=        new DataTable();  //adjustments
			DataTable TableSched=      new DataTable();
		
//TableCharge---------------------------------------------------------------------------------------
/*
Select procdate, sum(procfee) From procedurelog
Group By procdate Order by procdate desc  
*/
			Queries.CurReport=new ReportOld();
			string whereProv;//used as the provider portion of the where clauses.
				//each whereProv needs to be set up separately for each query
			whereProv="AND (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=" OR";
				}
				whereProv+=" procedurelog.ProvNum = '"+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query="SELECT procedurelog.ProcDate, "
				+"SUM(procedurelog.ProcFee) "
				+"FROM procedurelog "
				+"WHERE procedurelog.ProcDate >= "+POut.PDate(dateFrom)+" "
				+"AND procedurelog.ProcDate <= "+POut.PDate(dateTo)+" "
				+"AND procedurelog.ProcStatus = '2' "//complete
				+whereProv
				+" GROUP BY procedurelog.ProcDate "
				+"ORDER BY procedurelog.ProcDate"; 
			Queries.SubmitTemp(); //create TableTemp
      TableCharge=Queries.TableTemp; //must create datatable obj since Queries.TempTable is static

//NEXT is TableCapWriteoff--------------------------------------------------------------------------
/*
SELECT DateCP, SUM(WriteOff) From claimproc
WHERE Status='7'
GROUP BY DateCP Order by DateCP  
*/
			Queries.CurReport=new ReportOld();
			whereProv="AND (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=" OR";
				}
				whereProv+=" ProvNum = '"+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query="SELECT DateCP, SUM(WriteOff) FROM claimproc WHERE "
				+"DateCP >= "+POut.PDate(dateFrom)+" "
				+"AND DateCP <= "+POut.PDate(dateTo)+" "
				+"AND Status = '7' "//CapComplete
				+whereProv
				+" GROUP BY DateCP "
				+"ORDER BY DateCP"; 
			Queries.SubmitTemp(); //create TableTemp
      TableCapWriteoff=Queries.TableTemp.Copy();

//NEXT is TableInsWriteoff--------------------------------------------------------------------------
/*
SELECT DateCP, SUM(WriteOff) From claimproc
WHERE Status='1'
GROUP BY DateCP Order by DateCP  
*/
			Queries.CurReport=new ReportOld();
			whereProv="AND (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=" OR";
				}
				whereProv+=" ProvNum = '"+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query="SELECT DateCP, SUM(WriteOff) FROM claimproc WHERE "
				+"DateCP >= "+POut.PDate(dateFrom)+" "
				+"AND DateCP <= "+POut.PDate(dateTo)+" "
				+"AND (Status = '1' OR Status = 4) "//Recieved or supplemental. Otherwise, it's only an estimate.
				+whereProv
				+" GROUP BY DateCP "
				+"ORDER BY DateCP"; 
			Queries.SubmitTemp(); //create TableTemp
      TableInsWriteoff=Queries.TableTemp.Copy();

// NEXT is TableSched------------------------------------------------------------------------------
/*
SELECT FROM_DAYS(TO_DAYS(Appointment.AptDateTime)) AS
SchedDate,SUM(Procedurelog.procfee) FROM Appointment, Procedurelog 
Where Appointment.aptnum = Procedurelog.aptnum && Appointment.AptStatus = 1
|| Appointment.AptStatus=4 && FROM_DAYS(TO_DAYS(Appointment.AptDateTime)) <= '2003-05-12'    
GROUP BY SchedDate
*/
			Queries.CurReport=new ReportOld();
			whereProv="AND (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=" OR";
				}
				whereProv+=" procedurelog.provnum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query= "SELECT FROM_DAYS(TO_DAYS(appointment.AptDateTime)) "//gets rid of time
			  +"SchedDate,SUM(procedurelog.ProcFee) FROM appointment,procedurelog WHERE "
        +"appointment.AptNum = procedurelog.AptNum "
				+"AND (appointment.AptStatus = 1 OR "//stat=scheduled
        +"appointment.AptStatus = 4) "//or stat=ASAP
				+"AND FROM_DAYS(TO_DAYS(appointment.AptDateTime)) >= "
				+POut.PDate(dateFrom)+" "
				+"AND FROM_DAYS(TO_DAYS(appointment.AptDateTime)) <= "
				+POut.PDate(dateTo)+" "
				+whereProv
				+" GROUP BY SchedDate "
				+"ORDER BY SchedDate"; 
			Queries.SubmitTemp(); //create TableTemp
      TableSched=Queries.TableTemp.Copy(); //must create datatable obj since Queries.TempTable is static

// NEXT is TablePay----------------------------------------------------------------------------------
//must join the paysplit to the payment to eliminate the discounts.
/*
Select paysplit.procdate,sum(paysplit.splitamt) from paysplit,payment where paysplit.procdate < '2003-08-12'
&& paysplit.paynum = payment.paynum
group by procdate union all 
Select claimpayment.checkdate,sum(claimproc.inspayamt) from claimpayment,claimproc where 
claimproc.claimpaymentnum = claimpayment.claimpaymentnum
&& claimpayment.checkdate < '2003-08-12'
group by claimpayment.checkdate order by procdate
*/
			Queries.CurReport=new ReportOld();
			whereProv="AND (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=" OR";
				}
				whereProv+=" paysplit.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query= "SELECT paysplit.DatePay,SUM(paysplit.splitamt) FROM paysplit "
				+"WHERE paysplit.IsDiscount = '0' "
				+"AND paysplit.DatePay >= "+POut.PDate(dateFrom)+" "
				+"AND paysplit.DatePay <= "+POut.PDate(dateTo)+" "
				+whereProv
				+" GROUP BY paysplit.DatePay ORDER BY DatePay";
			Queries.SubmitTemp(); //create TableTemp
      TablePay=Queries.TableTemp.Copy(); //must create datatable obj since Queries.TempTable is static

// NEXT is TableIns, added by SPK 3/16/04-----------------------------------------------------------
/*
Select claimpayment.checkdate,sum(claimproc.inspayamt) from claimpayment,claimproc where 
claimproc.claimpaymentnum = claimpayment.claimpaymentnum
&& claimpayment.checkdate < '2003-08-12'
group by claimpayment.checkdate order by procdate
*/
			Queries.CurReport=new ReportOld();
			whereProv="AND (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=" OR";
				}
				whereProv+=" claimproc.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query= "SELECT claimpayment.CheckDate,SUM(claimproc.InsPayamt) "
				+"FROM claimpayment,claimproc WHERE "
				+"claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum "
				+"AND claimpayment.CheckDate >= " + POut.PDate(dateFrom)+" "
				+"AND claimpayment.CheckDate <= " + POut.PDate(dateTo)+" "
				+whereProv
				+" GROUP BY claimpayment.CheckDate ORDER BY checkdate";			
			Queries.SubmitTemp(); //create TableIns
			TableIns=Queries.TableTemp; //must create datatable obj since Queries.TempTable is static
// End TableIns, SPK 3/16/04


// LAST is TableAdj--------------------------------------------------------------------------------
/*
SELECT adjustment.adjdate,CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),adjustment.adjtype,adjustment.adjnote,adjustment.adjamt
FROM adjustment,patient,definition 
WHERE adjustment.adjtype=definition.defnum && patient.patnum=adjustment.patnum
ORDER BY adjdate DESC
*/ 
  		Queries.CurReport=new ReportOld();
			whereProv="AND (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=" OR";
				}
				whereProv+=" provnum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query="SELECT adjdate, SUM(adjamt) FROM adjustment WHERE "
				+"adjdate >= "+POut.PDate(dateFrom)+" "
				+"AND adjdate <= "+POut.PDate(dateTo)+" "
				+whereProv
				+" GROUP BY adjdate ORDER BY adjdate"; 
			Queries.SubmitTemp(); //create TableTemp
      TableAdj=Queries.TableTemp; //must create datatable obj since Queries.TempTable is static 

//Now to fill Table Q from the temp tables
			Queries.TableQ=new DataTable(null);//new table with 10 columns
			for(int i=0;i<10;i++){ //add columns
				Queries.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			Queries.CurReport.ColTotal=new double[Queries.TableQ.Columns.Count];
			double production;
			double scheduled;
			double adjust;
			double inswriteoff; //spk 5/19/05
			double totalproduction;
			double ptincome;
			double insincome;
			double totalincome;
			DateTime[] dates=new DateTime[(dateTo-dateFrom).Days+1];
			//MessageBox.Show(dates.Length.ToString());
				//.ToString("yyyy-MM-dd")+"' "
				//	+"&& procdate <= '" + datePickerTo.Value
			for(int i=0;i<dates.Length;i++){//usually 31 days in loop
				dates[i]=dateFrom.AddDays(i);
				//create new row called 'row' based on structure of TableQ
				DataRow row = Queries.TableQ.NewRow();
				row[0]=dates[i].ToShortDateString();
				row[1]=dates[i].DayOfWeek.ToString();
				production=0;
				scheduled=0;
				adjust=0;
				inswriteoff=0; //spk 5/19/05
				totalproduction=0;
				ptincome=0;//spk
				insincome=0;
				totalincome=0;
				for(int j=0;j<TableCharge.Rows.Count;j++)  {
				  if(dates[i]==(PIn.PDate(TableCharge.Rows[j][0].ToString()))){
		 			  production+=PIn.PDouble(TableCharge.Rows[j][1].ToString());
					}
   			}
				for(int j=0;j<TableCapWriteoff.Rows.Count;j++)  {
				  if(dates[i]==(PIn.PDate(TableCapWriteoff.Rows[j][0].ToString()))){
		 			  production-=PIn.PDouble(TableCapWriteoff.Rows[j][1].ToString());
					}
   			}
				for(int j=0; j<TableSched.Rows.Count; j++)  {
				  if(dates[i]==(PIn.PDate(TableSched.Rows[j][0].ToString()))){
			 	    scheduled+=PIn.PDouble(TableSched.Rows[j][1].ToString());
					}
   			}		
				for(int j=0; j<TableAdj.Rows.Count; j++){
				  if(dates[i]==(PIn.PDate(TableAdj.Rows[j][0].ToString()))){
						adjust+=PIn.PDouble(TableAdj.Rows[j][1].ToString());
					}
   			}
				// ***** spk 5/19/05
				for(int j=0; j<TableInsWriteoff.Rows.Count; j++) { // added for ins. writeoff, spk 5/19/05
				  if(dates[i]==(PIn.PDate(TableInsWriteoff.Rows[j][0].ToString()))){
						inswriteoff-=PIn.PDouble(TableInsWriteoff.Rows[j][1].ToString());
					}
				}
				for(int j=0; j<TablePay.Rows.Count; j++){
				  if(dates[i]==(PIn.PDate(TablePay.Rows[j][0].ToString()))){
						ptincome+=PIn.PDouble(TablePay.Rows[j][1].ToString());
					}																																						 
   			}
				for(int j=0; j<TableIns.Rows.Count; j++){// new TableIns, SPK
					if(dates[i]==(PIn.PDate(TableIns.Rows[j][0].ToString()))){
						insincome+=PIn.PDouble(TableIns.Rows[j][1].ToString());
					}																																						 
				}
				totalproduction=production+scheduled+adjust+inswriteoff;
				totalincome=ptincome+insincome;
				row[2]=production.ToString("n");
				row[3]=scheduled.ToString("n");
				row[4]=adjust.ToString("n");
				row[5]=inswriteoff.ToString("n"); //spk 5/19/05
				row[6]=totalproduction.ToString("n");
				row[7]=ptincome.ToString("n");				// spk
				row[8]=insincome.ToString("n");				// spk
				row[9]=totalincome.ToString("n");
				Queries.CurReport.ColTotal[2]+=production;
				Queries.CurReport.ColTotal[3]+=scheduled;
				Queries.CurReport.ColTotal[4]+=adjust;
				Queries.CurReport.ColTotal[5]+=inswriteoff; //spk 5/19/05
				Queries.CurReport.ColTotal[6]+=totalproduction;
				Queries.CurReport.ColTotal[7]+=ptincome;	// spk
				Queries.CurReport.ColTotal[8]+=insincome;	// spk
				Queries.CurReport.ColTotal[9]+=totalincome;
				Queries.TableQ.Rows.Add(row);  //adds row to table Q
      }//end for row
			//done filling now set up table
			Queries.CurReport.ColWidth=new int[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColPos=new int[Queries.TableQ.Columns.Count+1];
			Queries.CurReport.ColPos[0]=0;
			Queries.CurReport.ColCaption=new string[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColAlign=new HorizontalAlignment[Queries.TableQ.Columns.Count];
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.ResetGrid();//necessary won't work without
			Queries.CurReport.Title="Production and Income";
			Queries.CurReport.SubTitle=new string[3];
			Queries.CurReport.SubTitle[0]=((Pref)PrefB.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=textDateFrom.Text+" - "
				+textDateTo.Text;
			bool allProv=true;
			string sProv="";
			for(int i=0;i<listProv.Items.Count;i++){
				if(listProv.SelectedIndices.Contains(i)){
					if(sProv!="")
						sProv+=", ";
					sProv+=Providers.List[i].Abbr;
				}
				else{
					allProv=false;
				}
			}
			if(allProv){
				Queries.CurReport.SubTitle[2]="All Providers";
			}
			else{
				Queries.CurReport.SubTitle[2]=sProv;
			}
			Queries.CurReport.Summary=new string[3];
			Queries.CurReport.Summary[0]
				//=Lan.g(this,"Total Production (Production + Scheduled + Adjustments):")+" "
				//+(Queries.CurReport.ColTotal[2]+Queries.CurReport.ColTotal[3]
				//+Queries.CurReport.ColTotal[4]).ToString("c"); //spk 5/19/05
				=Lan.g(this,"Total Production (Production + Scheduled + Adj - Writeoff):")+" "
				+(Queries.CurReport.ColTotal[2]+Queries.CurReport.ColTotal[3]+Queries.CurReport.ColTotal[4]
				+Queries.CurReport.ColTotal[5]).ToString("c");
			Queries.CurReport.Summary[2]
				=Lan.g(this,"Total Income (Pt Income + Ins Income):")+" "
				+(Queries.CurReport.ColTotal[7]+Queries.CurReport.ColTotal[8]).ToString("c");
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=110;
			Queries.CurReport.ColPos[2]=190;
			Queries.CurReport.ColPos[3]=270;
			Queries.CurReport.ColPos[4]=350;
			Queries.CurReport.ColPos[5]=420;
			Queries.CurReport.ColPos[6]=490;
			Queries.CurReport.ColPos[7]=560;
			Queries.CurReport.ColPos[8]=630;
			Queries.CurReport.ColPos[9]=700;
			Queries.CurReport.ColPos[10]=770;
			Queries.CurReport.ColCaption[0]="Date";
			Queries.CurReport.ColCaption[1]="Weekday";
			Queries.CurReport.ColCaption[2]="Production";
			Queries.CurReport.ColCaption[3]="Sched";
			Queries.CurReport.ColCaption[4]="Adj";
			Queries.CurReport.ColCaption[5]="Writeoff";		//spk 5/19/05
			Queries.CurReport.ColCaption[6]="Tot Prod";
			Queries.CurReport.ColCaption[7]="Pt Income";		// spk
			Queries.CurReport.ColCaption[8]="Ins Income";		// spk
			Queries.CurReport.ColCaption[9]="Tot Income";
      Queries.CurReport.ColAlign[2]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[3]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[5]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[6]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[7]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[8]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[9]=HorizontalAlignment.Right;
			FormQuery2.ShowDialog();
		}

		private void RunAnnual(){
			dateFrom=PIn.PDate(textDateFrom.Text);
			dateTo=PIn.PDate(textDateTo.Text);
			/*  There are 4 temp tables  
			*  TableProduction: Sum of all charges for each month - CapComplete Writeoffs
			*  TableAdj: Sum of all adjustments for each month
			*  TablePay: Sum of all Patient payments for each month
			*  TableIns: Sum of all Insurance payments for each month
			* GROUP BY is used to group dates together so that amounts are summed for each month
			*/
			DataTable TableProduction= new DataTable();
			DataTable TableAdj=        new DataTable();
			DataTable TableInsWriteoff=new DataTable();  //ins writeoffs, added spk 5/19/05
			DataTable TablePay=        new DataTable();
			DataTable TableIns=        new DataTable();
			//Month
			//Production
			//Adjustments
			//InsWriteoff
			//Total Production
			//Pt Income
			//Ins Income
			//Total Income
			Queries.CurReport=new ReportOld();
			//Procedures------------------------------------------------------------------------------
			string whereProv="(";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+="OR ";
				}
				whereProv+="procedurelog.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
			}
			whereProv+=")";
			Queries.CurReport.Query="SELECT "
				+"procedurelog.ProcDate,"
				+"SUM(procedurelog.ProcFee)-IFNULL(SUM(claimproc.WriteOff),0) "
				+"FROM procedurelog "
				+"LEFT JOIN claimproc ON procedurelog.ProcNum=claimproc.ProcNum "
				+"AND claimproc.Status='7' "//only CapComplete writeoffs are subtracted here.
				+"WHERE procedurelog.ProcStatus = '2' "
				+"AND "+whereProv+" "
				+"AND procedurelog.ProcDate >= " +POut.PDate(dateFrom)+" "
				+"AND procedurelog.ProcDate <= " +POut.PDate(dateTo)+" "
				+"GROUP BY MONTH(procedurelog.ProcDate)";
			//MessageBox.Show(Queries.CurReport.Query);
			Queries.SubmitTemp(); //create TableTemp
      TableProduction=Queries.TableTemp.Copy(); 
			//Adjustments----------------------------------------------------------------------------
			whereProv="(";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+="OR ";
				}
				whereProv+="adjustment.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
			}
			whereProv+=")";
			Queries.CurReport.Query="SELECT "
				+"adjustment.AdjDate,"
				+"SUM(adjustment.AdjAmt) "
				+"FROM adjustment "
				+"WHERE "
				+whereProv+" "
				+"AND adjustment.AdjDate >= "+POut.PDate(dateFrom)+" "
				+"AND adjustment.AdjDate <= "+POut.PDate(dateTo)
				+"GROUP BY MONTH(adjustment.AdjDate)";
			Queries.SubmitTemp();
      TableAdj=Queries.TableTemp.Copy();
			//****** added, spk 5/19/05
			//TableInsWriteoff--------------------------------------------------------------------------
			whereProv="(";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0)	{
					whereProv+=" OR";
				}
				whereProv+=" ProvNum = '"+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query="SELECT "
				+"claimproc.DateCP," 
				+"SUM(claimproc.WriteOff) "
				+"FROM claimproc "
				+"WHERE "
				+whereProv+" "
				+"AND claimproc.DateCP >= "+POut.PDate(dateFrom)+" "
				+"AND claimproc.DateCP <= "+POut.PDate(dateTo)+" "
				+"AND claimproc.Status = '1' "//Received. 
				+"GROUP BY MONTH(claimproc.DateCP)";
			Queries.SubmitTemp(); //create TableTemp
			TableInsWriteoff=Queries.TableTemp.Copy();
			//PtIncome--------------------------------------------------------------------------------
			whereProv="(";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+="OR ";
				}
				whereProv+="paysplit.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
			}
			whereProv+=")";
			Queries.CurReport.Query="SELECT "
				+"paysplit.DatePay,"
				+"SUM(paysplit.SplitAmt) "
				+"FROM paysplit "
				+"WHERE paysplit.IsDiscount=0 AND "//paysplit.PayNum=payment.PayNum "
				+whereProv+" "
				+"AND paysplit.DatePay >= "+POut.PDate(dateFrom)+" "
				+"AND paysplit.DatePay <= "+POut.PDate(dateTo)+" "
				+"GROUP BY MONTH(paysplit.DatePay)";
			Queries.SubmitTemp();
      TablePay=Queries.TableTemp.Copy(); 
			//InsIncome---------------------------------------------------------------------------------
			whereProv="AND (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=" OR";
				}
				whereProv+=" claimproc.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query= "SELECT claimpayment.CheckDate,SUM(claimproc.InsPayamt) "
				+"FROM claimpayment,claimproc WHERE "
				+"claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum "
				+"AND claimpayment.CheckDate >= " + POut.PDate(dateFrom)+" "
				+"AND claimpayment.CheckDate <= " + POut.PDate(dateTo)+" "
				+whereProv
				+" GROUP BY claimpayment.CheckDate ORDER BY checkdate";	
			Queries.SubmitTemp();
      TableIns=Queries.TableTemp.Copy(); 
			Queries.TableQ=new DataTable(null);//new table with 7 columns
			for(int i=0;i<8;i++){ //add columns
				Queries.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			Queries.CurReport.ColTotal=new double[Queries.TableQ.Columns.Count];
			double production;
			double adjust;
			double inswriteoff;	//spk 5/19/05
			double totalproduction;
			double ptincome;
			double insincome;
			double totalincome;
			//lenth of array is number of months between the two dates plus one.
			//MessageBox.Show((dateTo.Year*12+dateTo.Month-dateFrom.Year*12-dateFrom.Month+1).ToString());
			DateTime[] dates=new DateTime[dateTo.Year*12+dateTo.Month-dateFrom.Year*12-dateFrom.Month+1];//1st of each month
			//MessageBox.Show(dates.Length.ToString());
				//.ToString("yyyy-MM-dd")+"' "
				//	+"&& procdate <= '" + datePickerTo.Value
			for(int i=0;i<dates.Length;i++){//usually 12 months in loop
				dates[i]=dateFrom.AddMonths(i);//only the month and year are important
				//create new row called 'row' based on structure of TableQ
				DataRow row=Queries.TableQ.NewRow();
				row[0]=dates[i].ToString("MMM yy");
				production=0;
				adjust=0;
				inswriteoff=0;	//spk 5/19/05
				totalproduction=0;
				ptincome=0;
				insincome=0;
				totalincome=0;
				for(int j=0;j<TableProduction.Rows.Count;j++)  {
				  if(dates[i].Year==PIn.PDate(TableProduction.Rows[j][0].ToString()).Year
						&& dates[i].Month==PIn.PDate(TableProduction.Rows[j][0].ToString()).Month){
		 			  production+=PIn.PDouble(TableProduction.Rows[j][1].ToString());
					}
   			}
				for(int j=0;j<TableAdj.Rows.Count; j++){
				  if(dates[i].Year==PIn.PDate(TableAdj.Rows[j][0].ToString()).Year
						&& dates[i].Month==PIn.PDate(TableAdj.Rows[j][0].ToString()).Month){
						adjust+=PIn.PDouble(TableAdj.Rows[j][1].ToString());
					}
   			}
				// ***** added for inswriteoff, spk 5/19/05
				for(int j=0;j<TableInsWriteoff.Rows.Count; j++){
					if(dates[i].Year==PIn.PDate(TableInsWriteoff.Rows[j][0].ToString()).Year
						&& dates[i].Month==PIn.PDate(TableInsWriteoff.Rows[j][0].ToString()).Month){
						inswriteoff-=PIn.PDouble(TableInsWriteoff.Rows[j][1].ToString());
					}
				}
				for(int j=0;j<TablePay.Rows.Count; j++){
				  if(dates[i].Year==PIn.PDate(TablePay.Rows[j][0].ToString()).Year
						&& dates[i].Month==PIn.PDate(TablePay.Rows[j][0].ToString()).Month){
						ptincome+=PIn.PDouble(TablePay.Rows[j][1].ToString());
					}																																						 
   			}
				for(int j=0; j<TableIns.Rows.Count; j++){//
					if(dates[i].Year==PIn.PDate(TableIns.Rows[j][0].ToString()).Year
						&& dates[i].Month==PIn.PDate(TableIns.Rows[j][0].ToString()).Month){
						insincome+=PIn.PDouble(TableIns.Rows[j][1].ToString());
					}																																						 
				}
				totalproduction=production+adjust+inswriteoff;
				totalincome=ptincome+insincome;
				row[1]=production.ToString("n");
				row[2]=adjust.ToString("n");
				row[3]=inswriteoff.ToString("n");	//spk
				row[4]=totalproduction.ToString("n");
				row[5]=ptincome.ToString("n");
				row[6]=insincome.ToString("n");		
				row[7]=totalincome.ToString("n");
				Queries.CurReport.ColTotal[1]+=production;
				Queries.CurReport.ColTotal[2]+=adjust;	
				Queries.CurReport.ColTotal[3]+=inswriteoff;	//spk	
				Queries.CurReport.ColTotal[4]+=totalproduction;	
				Queries.CurReport.ColTotal[5]+=ptincome;	
				Queries.CurReport.ColTotal[6]+=insincome;	
				Queries.CurReport.ColTotal[7]+=totalincome;
				Queries.TableQ.Rows.Add(row);  //adds row to table Q
      }//end for row
			//done filling now set up table
			Queries.CurReport.ColWidth=new int[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColPos=new int[Queries.TableQ.Columns.Count+1];
			Queries.CurReport.ColPos[0]=0;
			Queries.CurReport.ColCaption=new string[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColAlign=new HorizontalAlignment[Queries.TableQ.Columns.Count];
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.ResetGrid();//necessary won't work without
			Queries.CurReport.Title="Annual Production and Income";
			Queries.CurReport.SubTitle=new string[3];
			Queries.CurReport.SubTitle[0]=((Pref)PrefB.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=textDateFrom.Text+" - "
				+textDateTo.Text;
			bool allProv=true;
			string sProv="";
			for(int i=0;i<listProv.Items.Count;i++){
				if(listProv.SelectedIndices.Contains(i)){
					if(sProv!="")
						sProv+=", ";
					sProv+=Providers.List[i].Abbr;
				}
				else{
					allProv=false;
				}
			}
			if(allProv){
				Queries.CurReport.SubTitle[2]="All Providers";
			}
			else{
				Queries.CurReport.SubTitle[2]=sProv;
			}
			Queries.CurReport.Summary=new string[0];
			/*Queries.CurReport.Summary[0]
				=Lan.g(this,"Total Production (Production + Scheduled + Adjustments):")+" "
				+(Queries.CurReport.ColTotal[2]+Queries.CurReport.ColTotal[3]
				+Queries.CurReport.ColTotal[4]).ToString("c");
			Queries.CurReport.Summary[2]
				=Lan.g(this,"Total Income (Pt Income + Ins Income):")+" "
				+(Queries.CurReport.ColTotal[5]+Queries.CurReport.ColTotal[6]).ToString("c");*/
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=120;
			Queries.CurReport.ColPos[2]=210;
			Queries.CurReport.ColPos[3]=300;
			Queries.CurReport.ColPos[4]=390;
			Queries.CurReport.ColPos[5]=480;
			Queries.CurReport.ColPos[6]=570;
			Queries.CurReport.ColPos[7]=660;
			Queries.CurReport.ColPos[8]=750;
			//Month
			//Production
			//Adjustments
			//Total Production
			//Pt Income
			//Ins Income
			//Total Income
			Queries.CurReport.ColCaption[0]="Month";
			Queries.CurReport.ColCaption[1]="Production";
			Queries.CurReport.ColCaption[2]="Adjustments";
			Queries.CurReport.ColCaption[3]="Writeoff";	//spk
			Queries.CurReport.ColCaption[4]="Tot Prod";
			Queries.CurReport.ColCaption[5]="Pt Income";
			Queries.CurReport.ColCaption[6]="Ins Income";
			Queries.CurReport.ColCaption[7]="Total Income";
			Queries.CurReport.ColAlign[1]=HorizontalAlignment.Right;
      Queries.CurReport.ColAlign[2]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[3]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[5]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[6]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[7]=HorizontalAlignment.Right;
			FormQuery2.ShowDialog();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(listProv.SelectedIndices.Count==0){
				MessageBox.Show(Lan.g(this,"You must select at least one provider."));
				return;
			}
			//if(!PrefB.GetBool("EasyNoClinics") && listClinic.SelectedIndices.Count==0) {
			//	MessageBox.Show(Lan.g(this,"You must select at least one clinic."));
			//	return;
			//}
			dateFrom=PIn.PDate(textDateFrom.Text);
			dateTo=PIn.PDate(textDateTo.Text);
			if(radioDaily.Checked){
				RunDaily();
			}
			else if(radioMonthly.Checked){
				RunMonthly();
			}
			else{
				RunAnnual();
			}
			DialogResult=DialogResult.OK;	
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


		
	}
}








