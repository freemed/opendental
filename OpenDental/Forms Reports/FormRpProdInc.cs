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
		private ListBox listClin;
		private Label labelClin;
		private DateTime dateTo;
		///<summary>Can be set externally when automating.</summary>
		public string DailyMonthlyAnnual;
		///<summary>If set externally, then this sets the date on startup.</summary>
		public DateTime DateStart;
		private GroupBox groupBox3;
		private RadioButton radioWriteoffPay;
		private RadioButton radioWriteoffProc;
		private Label label5;
		private CheckBox checkAllProv;
		private CheckBox checkAllClin;
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
			this.label1 = new System.Windows.Forms.Label();
			this.listProv = new System.Windows.Forms.ListBox();
			this.radioMonthly = new System.Windows.Forms.RadioButton();
			this.radioDaily = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioAnnual = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textToday = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butRight = new OpenDental.UI.Button();
			this.butThis = new OpenDental.UI.Button();
			this.textDateFrom = new OpenDental.ValidDate();
			this.textDateTo = new OpenDental.ValidDate();
			this.butLeft = new OpenDental.UI.Button();
			this.listClin = new System.Windows.Forms.ListBox();
			this.labelClin = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.radioWriteoffProc = new System.Windows.Forms.RadioButton();
			this.radioWriteoffPay = new System.Windows.Forms.RadioButton();
			this.checkAllProv = new System.Windows.Forms.CheckBox();
			this.checkAllClin = new System.Windows.Forms.CheckBox();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
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
			this.listProv.Location = new System.Drawing.Point(37,165);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(154,186);
			this.listProv.TabIndex = 30;
			this.listProv.Click += new System.EventHandler(this.listProv_Click);
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
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(356,66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(127,20);
			this.label4.TabIndex = 41;
			this.label4.Text = "Today\'s Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textToday
			// 
			this.textToday.Location = new System.Drawing.Point(485,64);
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
			this.groupBox2.Location = new System.Drawing.Point(390,90);
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
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(95,77);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(100,20);
			this.textDateFrom.TabIndex = 43;
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(95,104);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(100,20);
			this.textDateTo.TabIndex = 44;
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
			// listClin
			// 
			this.listClin.Location = new System.Drawing.Point(215,165);
			this.listClin.Name = "listClin";
			this.listClin.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listClin.Size = new System.Drawing.Size(154,186);
			this.listClin.TabIndex = 45;
			this.listClin.Click += new System.EventHandler(this.listClin_Click);
			// 
			// labelClin
			// 
			this.labelClin.Location = new System.Drawing.Point(212,128);
			this.labelClin.Name = "labelClin";
			this.labelClin.Size = new System.Drawing.Size(104,16);
			this.labelClin.TabIndex = 44;
			this.labelClin.Text = "Clinics";
			this.labelClin.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.radioWriteoffProc);
			this.groupBox3.Controls.Add(this.radioWriteoffPay);
			this.groupBox3.Location = new System.Drawing.Point(390,256);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(281,95);
			this.groupBox3.TabIndex = 46;
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
			// checkAllProv
			// 
			this.checkAllProv.Checked = true;
			this.checkAllProv.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAllProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllProv.Location = new System.Drawing.Point(38,146);
			this.checkAllProv.Name = "checkAllProv";
			this.checkAllProv.Size = new System.Drawing.Size(95,16);
			this.checkAllProv.TabIndex = 47;
			this.checkAllProv.Text = "All";
			this.checkAllProv.Click += new System.EventHandler(this.checkAllProv_Click);
			// 
			// checkAllClin
			// 
			this.checkAllClin.Checked = true;
			this.checkAllClin.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAllClin.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllClin.Location = new System.Drawing.Point(215,146);
			this.checkAllClin.Name = "checkAllClin";
			this.checkAllClin.Size = new System.Drawing.Size(95,16);
			this.checkAllClin.TabIndex = 48;
			this.checkAllClin.Text = "All";
			this.checkAllClin.Click += new System.EventHandler(this.checkAllClin_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(710,365);
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
			this.butOK.Location = new System.Drawing.Point(710,330);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormRpProdInc
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(818,417);
			this.Controls.Add(this.checkAllClin);
			this.Controls.Add(this.checkAllProv);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.listClin);
			this.Controls.Add(this.labelClin);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.textToday);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.groupBox1);
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
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		private void FormProduction_Load(object sender, System.EventArgs e) {
			textToday.Text=DateTime.Today.ToShortDateString();
			for(int i=0;i<ProviderC.List.Length;i++){
				listProv.Items.Add(ProviderC.List[i].GetLongDesc());
			}
			if(PrefC.GetBool(PrefName.EasyNoClinics)){
				listClin.Visible=false;
				labelClin.Visible=false;
				checkAllClin.Visible=false;
			}
			else{
				listClin.Items.Add(Lan.g(this,"Unassigned"));
				for(int i=0;i<Clinics.List.Length;i++) {
					listClin.Items.Add(Clinics.List[i].Description);
				}
			}
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
			dateFrom=PIn.Date(textDateFrom.Text);
			dateTo=PIn.Date(textDateTo.Text);
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
				dateTo=PIn.Date(textDateTo.Text);
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
			dateFrom=PIn.Date(textDateFrom.Text);
			dateTo=PIn.Date(textDateTo.Text);
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
				dateTo=PIn.Date(textDateTo.Text);
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
			dateFrom=PIn.Date(textDateFrom.Text);
			dateTo=PIn.Date(textDateTo.Text);
			//Date
			//PatientName
			//Description
			//Prov
			//Clinic
			//Production
			//Adjustments
			//Pt Income
			//Ins Income
			//last column is a unique id that is not displayed
			string whereProv="";
			if(!checkAllProv.Checked){
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="procedurelog.ProvNum = "+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
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
						whereClin+="procedurelog.ClinicNum = 0 ";
					}
					else {
						whereClin+="procedurelog.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			ReportSimpleGrid report=new ReportSimpleGrid();
			//Procedures------------------------------------------------------------------------------
			report.Query="(SELECT "
				+"procedurelog.ProcDate AS procdate,"
				+"CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI) AS namelf,"
				+"procedurecode.Descript,"
				+"provider.Abbr,"
				+"procedurelog.ClinicNum,"
				+"procedurelog.ProcFee*(CASE procedurelog.UnitQty+procedurelog.BaseUnits WHEN 0 THEN 1 ELSE procedurelog.UnitQty+procedurelog.BaseUnits END)-IFNULL(SUM(claimproc.WriteOff),0) $fee,"//if no writeoff, then subtract 0
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
				+whereProv
				+whereClin
				+"AND procedurelog.ProcDate >= " +POut.Date(dateFrom)+" "
				+"AND procedurelog.ProcDate <= " +POut.Date(dateTo)+" "
				+"GROUP BY procedurelog.ProcNum "
				+") UNION ALL (";
			//Adjustments-----------------------------------------------------------------------------
			whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="adjustment.ProvNum = "+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
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
						whereClin+="adjustment.ClinicNum = 0 ";
					}
					else {
						whereClin+="adjustment.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			report.Query+="SELECT "
				+"adjustment.AdjDate,"
				+"CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI),"
				+"definition.ItemName,"
				+"provider.Abbr,"
				+"adjustment.ClinicNum,"
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
				+whereProv
				+whereClin
				+"AND adjustment.AdjDate >= "+POut.Date(dateFrom)+" "
				+"AND adjustment.AdjDate <= "+POut.Date(dateTo)
				+") UNION ALL (";
			//Insurance Writeoff----------------------------------------------------------
			whereProv="";
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
						whereClin+="claimproc.ClinicNum = 0 ";
					}
					else {
						whereClin+="claimproc.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			if(radioWriteoffPay.Checked){
				report.Query+="SELECT claimproc.DateCP,"
					+"CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI),"
					+"carrier.CarrierName,"
					+"provider.Abbr,"
					+"claimproc.ClinicNum,"
					+"'0',"
					+"'0',"
					+"-SUM(claimproc.WriteOff),"
					+"'0',"
					+"'0',"
					+"claimproc.ClaimNum "
					+"FROM claimproc,insplan,patient,carrier,provider "
					+"WHERE provider.ProvNum = claimproc.ProvNum "
					+"AND claimproc.PlanNum = insplan.PlanNum "
					+"AND claimproc.PatNum = patient.PatNum "
					+"AND carrier.CarrierNum = insplan.CarrierNum "
					+whereProv
					+whereClin
					+"AND (claimproc.Status=1 OR claimproc.Status=4) "//received or supplemental
					+"AND claimproc.WriteOff > '.0001' "
					+"AND claimproc.DateCP >= "+POut.Date(dateFrom)+" "
					+"AND claimproc.DateCP <= "+POut.Date(dateTo)+" ";
			}
			else{
				report.Query+="SELECT claimproc.ProcDate,"
					+"CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI),"
					+"carrier.CarrierName,"
					+"provider.Abbr,"
					+"claimproc.ClinicNum,"
					+"'0',"
					+"'0',"
					+"-SUM(claimproc.WriteOff),"
					+"'0',"
					+"'0',"
					+"claimproc.ClaimNum "
					+"FROM claimproc,insplan,patient,carrier,provider "
					+"WHERE provider.ProvNum = claimproc.ProvNum "
					+"AND claimproc.PlanNum = insplan.PlanNum "
					+"AND claimproc.PatNum = patient.PatNum "
					+"AND carrier.CarrierNum = insplan.CarrierNum "
					+whereProv
					+whereClin
					+"AND (claimproc.Status=1 OR claimproc.Status=4 OR claimproc.Status=0) "//received or supplemental or notreceived
					+"AND claimproc.WriteOff > '.0001' "
					+"AND claimproc.ProcDate >= "+POut.Date(dateFrom)+" "
					+"AND claimproc.ProcDate <= "+POut.Date(dateTo)+" ";
			}
			report.Query+="GROUP BY claimproc.ClaimProcNum"
				+") UNION ALL (";
			//Patient Income------------------------------------------------------------------------------
			whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="paysplit.ProvNum = "
						+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
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
						whereClin+="paysplit.ClinicNum = 0 ";
					}
					else {
						whereClin+="paysplit.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			report.Query+="SELECT "
				+"paysplit.DatePay,"
				//+"GROUP_CONCAT(DISTINCT CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI)),"
				+"CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),"
				+"definition.ItemName,"
				//+"GROUP_CONCAT(DISTINCT provider.Abbr),"
				+"provider.Abbr,"
				+"paysplit.ClinicNum,"
				+"'0',"
				+"'0',"
				+"'0',"
				+"SUM(paysplit.SplitAmt),"
				+"'0',"
				+"payment.PayNum "
				+"FROM paysplit "//can't do cartesian join because income transfers would be excluded
				+"LEFT JOIN payment ON payment.PayNum=paysplit.PayNum "
				+"LEFT JOIN patient ON patient.PatNum=paysplit.PatNum "
				+"LEFT JOIN provider ON provider.ProvNum=paysplit.ProvNum "
				+"LEFT JOIN definition ON payment.PayType=definition.DefNum "
				//notice that patient and prov are accurate, but if more than one, then only one shows//should be 'fixed'
				+"WHERE payment.PayDate >= "+POut.Date(dateFrom)+" "
				+"AND payment.PayDate <= "+POut.Date(dateTo)+" "
				+whereProv
				+whereClin
				//+"GROUP BY payment.PayNum"
				+"GROUP BY paysplit.PatNum,paysplit.ProvNum,paysplit.ClinicNum,PayType"
				+") UNION ALL (";
			//Insurance Income----------------------------------------------------------------------------
			whereProv="";
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
						whereClin+="claimproc.ClinicNum = 0 ";
					}
					else {
						whereClin+="claimproc.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			report.Query+="SELECT "
				+"claimpayment.CheckDate,"
				+"CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),"
				+"carrier.CarrierName,"
				+"provider.Abbr,"
				+"claimproc.ClinicNum,"
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
				+whereProv
				+whereClin
				+"AND (claimproc.Status=1 OR claimproc.Status=4) "//received or supplemental
				+"AND claimpayment.CheckDate >= "+POut.Date(dateFrom)+" "
				+"AND claimpayment.CheckDate <= "+POut.Date(dateTo)+" "
				//+"GROUP BY claimproc.ClaimNum"
				+"GROUP BY claimproc.PatNum,claimproc.ProvNum,claimproc.PlanNum,claimproc.ClinicNum"
				+") ORDER BY procdate,namelf";
			//MessageBox.Show(report.Query);
			FormQuery2=new FormQuery(report);
			FormQuery2.IsReport=true;
			DataTable table=report.GetTempTable();
			report.TableQ=new DataTable(null);
			int colI=10;
			if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
				colI=11;
			}
			for(int i=0;i<colI;i++) { //add columns
				report.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			report.InitializeColumns();
			DataRow row;
			Double dbl;
			for(int i=0;i<table.Rows.Count;i++) {
				row = report.TableQ.NewRow();//create new row called 'row' based on structure of TableQ
				row[0]=PIn.Date(table.Rows[i][0].ToString()).ToShortDateString();
				row[1]=table.Rows[i][1].ToString();//name
				row[2]=table.Rows[i][2].ToString();//desc
				row[3]=table.Rows[i][3].ToString();//prov
				colI=4;
				if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
					row[colI]=Clinics.GetDesc(PIn.Long(table.Rows[i][4].ToString()));//clinic
					colI++;
				}
				dbl=PIn.Double(table.Rows[i][5].ToString());//Prod
				row[colI]=dbl.ToString("n");
				report.ColTotal[colI]+=dbl;
				colI++;
				dbl=PIn.Double(table.Rows[i][6].ToString());//Adj
				row[colI]=dbl.ToString("n");
				report.ColTotal[colI]+=dbl;
				colI++;
				dbl=PIn.Double(table.Rows[i][7].ToString());//Writeoff
				row[colI]=dbl.ToString("n");
				report.ColTotal[colI]+=dbl;
				colI++;
				dbl=PIn.Double(table.Rows[i][8].ToString());//PtInc
				row[colI]=dbl.ToString("n");
				report.ColTotal[colI]+=dbl;
				colI++;
				dbl=PIn.Double(table.Rows[i][9].ToString());//InsInc
				row[colI]=dbl.ToString("n");
				report.ColTotal[colI]+=dbl;
				colI++;
				row[colI]=table.Rows[i][10].ToString();
				report.TableQ.Rows.Add(row);
			}
			FormQuery2.ResetGrid();
			//FormQuery2.SubmitReportQuery();
			report.Title="Daily Production and Income";
			report.SubTitle.Add(PrefC.GetString(PrefName.PracticeTitle));
			report.SubTitle.Add(dateFrom.ToString("d")+" - "+dateTo.ToString("d"));
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
				report.IsLandscape=true;
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
			report.SetColumn(this,2,"Description",165);
			report.SetColumn(this,3,"Prov",55);
			colI=4;
			if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
				report.SetColumn(this,colI,"Clinic",70);
				colI++;
			}
			report.SetColumn(this,colI,"Production",65,HorizontalAlignment.Right);
			colI++;
			report.SetColumn(this,colI,"Adjust",70,HorizontalAlignment.Right);
			colI++;
			report.SetColumn(this,colI,"Writeoff",65,HorizontalAlignment.Right);
			colI++;
			report.SetColumn(this,colI,"Pt Income",65,HorizontalAlignment.Right);
			colI++;
			report.SetColumn(this,colI,"Ins Income",65,HorizontalAlignment.Right);
			colI++;
			report.SetColumn(this,colI,"",540,HorizontalAlignment.Right);
			double total;
			if(PrefC.GetBool(PrefName.EasyNoClinics)) {
				total=report.ColTotal[4]+report.ColTotal[5]+report.ColTotal[6];
			}
			else {
				total=report.ColTotal[5]+report.ColTotal[6]+report.ColTotal[7];
			}
			report.Summary.Add(Lan.g(this,"Total Production (Production + Adjustments - Writeoffs):")+" "+total.ToString("c"));
			report.Summary.Add("");
			if(PrefC.GetBool(PrefName.EasyNoClinics)) {
				total=report.ColTotal[7]+report.ColTotal[8];
			}
			else {
				total=report.ColTotal[8]+report.ColTotal[9];
			}
			report.Summary.Add(Lan.g(this,"Total Income (Pt Income + Ins Income):")+" "+total.ToString("c"));
			FormQuery2.ShowDialog();
		}

		private void RunMonthly(){
			dateFrom=PIn.Date(textDateFrom.Text);
			dateTo=PIn.Date(textDateTo.Text);
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
			ReportSimpleGrid report=new ReportSimpleGrid();
			string whereProv,whereClin;//used as the provider portion of the where clauses.
				//each whereProv needs to be set up separately for each query
			whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="procedurelog.ProvNum = "
						+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
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
			report.Query="SELECT procedurelog.ProcDate, "
				+"SUM(procedurelog.ProcFee*(CASE procedurelog.UnitQty+procedurelog.BaseUnits WHEN 0 THEN 1 ELSE procedurelog.UnitQty+procedurelog.BaseUnits END)) "
				+"FROM procedurelog "
				+"WHERE procedurelog.ProcDate >= "+POut.Date(dateFrom)+" "
				+"AND procedurelog.ProcDate <= "+POut.Date(dateTo)+" "
				+"AND procedurelog.ProcStatus = '2' "//complete
				+whereProv
				+whereClin
				+"GROUP BY procedurelog.ProcDate "
				+"ORDER BY procedurelog.ProcDate";
			TableCharge=report.GetTempTable();

//NEXT is TableCapWriteoff--------------------------------------------------------------------------
/*
SELECT DateCP, SUM(WriteOff) From claimproc
WHERE Status='7'
GROUP BY DateCP Order by DateCP  
*/
			report=new ReportSimpleGrid();
			whereProv="";
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
						whereClin+="claimproc.ClinicNum = 0 ";
					}
					else {
						whereClin+="claimproc.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			report.Query="SELECT DateCP, SUM(WriteOff) FROM claimproc WHERE "
				+"DateCP >= "+POut.Date(dateFrom)+" "
				+"AND DateCP <= "+POut.Date(dateTo)+" "
				+"AND Status = '7' "//CapComplete
				+whereProv
				+whereClin
				+" GROUP BY DateCP "
				+"ORDER BY DateCP"; 
			TableCapWriteoff=report.GetTempTable();

//NEXT is TableInsWriteoff--------------------------------------------------------------------------
/*
SELECT DateCP, SUM(WriteOff) From claimproc
WHERE Status='1'
GROUP BY DateCP Order by DateCP  
*/
			report=new ReportSimpleGrid();
			whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="ProvNum = "
						+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
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
						whereClin+="claimproc.ClinicNum = 0 ";
					}
					else {
						whereClin+="claimproc.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			if(radioWriteoffPay.Checked){
				report.Query="SELECT DateCP,SUM(WriteOff) FROM claimproc WHERE "
					+"DateCP >= "+POut.Date(dateFrom)+" "
					+"AND DateCP <= "+POut.Date(dateTo)+" "
					+"AND (Status = '1' OR Status = 4) "//Recieved or supplemental. Otherwise, it's only an estimate.
					+whereProv
					+whereClin
					+" GROUP BY DateCP "
					+"ORDER BY DateCP"; 
			}
			else{
				report.Query="SELECT ProcDate,SUM(WriteOff) FROM claimproc WHERE "
					+"ProcDate >= "+POut.Date(dateFrom)+" "
					+"AND ProcDate <= "+POut.Date(dateTo)+" "
					+"AND (claimproc.Status=1 OR claimproc.Status=4 OR claimproc.Status=0) " //received or supplemental or notreceived
					+whereProv
					+whereClin
					+" GROUP BY ProcDate "
					+"ORDER BY ProcDate"; 
			}
			TableInsWriteoff=report.GetTempTable();

// NEXT is TableSched------------------------------------------------------------------------------
/*
SELECT FROM_DAYS(TO_DAYS(Appointment.AptDateTime)) AS
SchedDate,SUM(Procedurelog.procfee) FROM Appointment, Procedurelog 
Where Appointment.aptnum = Procedurelog.aptnum && Appointment.AptStatus = 1
|| Appointment.AptStatus=4 && FROM_DAYS(TO_DAYS(Appointment.AptDateTime)) <= '2003-05-12'    
GROUP BY SchedDate
*/
			report=new ReportSimpleGrid();
			whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="procedurelog.ProvNum = "
						+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
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
			report.Query= "SELECT FROM_DAYS(TO_DAYS(appointment.AptDateTime)) "//gets rid of time
			  +"SchedDate,SUM(procedurelog.ProcFee) FROM appointment,procedurelog WHERE "
        +"appointment.AptNum = procedurelog.AptNum "
				+"AND (appointment.AptStatus = 1 OR "//stat=scheduled
        +"appointment.AptStatus = 4) "//or stat=ASAP
				+"AND FROM_DAYS(TO_DAYS(appointment.AptDateTime)) >= "
				+POut.Date(dateFrom)+" "
				+"AND FROM_DAYS(TO_DAYS(appointment.AptDateTime)) <= "
				+POut.Date(dateTo)+" "
				+whereProv
				+whereClin
				+" GROUP BY SchedDate "
				+"ORDER BY SchedDate"; 
			TableSched=report.GetTempTable();

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
			report=new ReportSimpleGrid();
			whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="paysplit.ProvNum = "
						+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
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
						whereClin+="paysplit.ClinicNum = 0 ";
					}
					else {
						whereClin+="paysplit.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			report.Query= "SELECT paysplit.DatePay,SUM(paysplit.splitamt) FROM paysplit "
				+"WHERE paysplit.IsDiscount = '0' "
				+"AND paysplit.DatePay >= "+POut.Date(dateFrom)+" "
				+"AND paysplit.DatePay <= "+POut.Date(dateTo)+" "
				+whereProv
				+whereClin
				+" GROUP BY paysplit.DatePay ORDER BY DatePay";
			TablePay=report.GetTempTable();

// NEXT is TableIns, added by SPK 3/16/04-----------------------------------------------------------
/*
Select claimpayment.checkdate,sum(claimproc.inspayamt) from claimpayment,claimproc where 
claimproc.claimpaymentnum = claimpayment.claimpaymentnum
&& claimpayment.checkdate < '2003-08-12'
group by claimpayment.checkdate order by procdate
*/
			report=new ReportSimpleGrid();
			whereProv="";
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
						whereClin+="claimproc.ClinicNum = 0 ";
					}
					else {
						whereClin+="claimproc.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			report.Query= "SELECT claimpayment.CheckDate,SUM(claimproc.InsPayamt) "
				+"FROM claimpayment,claimproc WHERE "
				+"claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum "
				+"AND (claimproc.Status=1 OR claimproc.Status=4) "//received or supplemental
				+"AND claimpayment.CheckDate >= " + POut.Date(dateFrom)+" "
				+"AND claimpayment.CheckDate <= " + POut.Date(dateTo)+" "
				+whereProv
				+whereClin
				+" GROUP BY claimpayment.CheckDate ORDER BY checkdate";			
			TableIns=report.GetTempTable();
// End TableIns, SPK 3/16/04


// LAST is TableAdj--------------------------------------------------------------------------------
/*
SELECT adjustment.adjdate,CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),adjustment.adjtype,adjustment.adjnote,adjustment.adjamt
FROM adjustment,patient,definition 
WHERE adjustment.adjtype=definition.defnum && patient.patnum=adjustment.patnum
ORDER BY adjdate DESC
*/ 
  		report=new ReportSimpleGrid();
			whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="ProvNum = "
						+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
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
						whereClin+="adjustment.ClinicNum = 0 ";
					}
					else {
						whereClin+="adjustment.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			report.Query="SELECT adjdate, SUM(adjamt) FROM adjustment WHERE "
				+"adjdate >= "+POut.Date(dateFrom)+" "
				+"AND adjdate <= "+POut.Date(dateTo)+" "
				+whereProv
				+whereClin
				+" GROUP BY adjdate ORDER BY adjdate"; 
			TableAdj=report.GetTempTable();

//Now to fill Table Q from the temp tables
			report.TableQ=new DataTable(null);//new table with 10 columns
			for(int i=0;i<10;i++){ //add columns
				report.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			report.InitializeColumns();
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
				DataRow row = report.TableQ.NewRow();
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
				  if(dates[i]==(PIn.Date(TableCharge.Rows[j][0].ToString()))){
		 			  production+=PIn.Double(TableCharge.Rows[j][1].ToString());
					}
   			}
				for(int j=0;j<TableCapWriteoff.Rows.Count;j++)  {
				  if(dates[i]==(PIn.Date(TableCapWriteoff.Rows[j][0].ToString()))){
		 			  production-=PIn.Double(TableCapWriteoff.Rows[j][1].ToString());
					}
   			}
				for(int j=0; j<TableSched.Rows.Count; j++)  {
				  if(dates[i]==(PIn.Date(TableSched.Rows[j][0].ToString()))){
			 	    scheduled+=PIn.Double(TableSched.Rows[j][1].ToString());
					}
   			}		
				for(int j=0; j<TableAdj.Rows.Count; j++){
				  if(dates[i]==(PIn.Date(TableAdj.Rows[j][0].ToString()))){
						adjust+=PIn.Double(TableAdj.Rows[j][1].ToString());
					}
   			}
				// ***** spk 5/19/05
				for(int j=0; j<TableInsWriteoff.Rows.Count; j++) { // added for ins. writeoff, spk 5/19/05
				  if(dates[i]==(PIn.Date(TableInsWriteoff.Rows[j][0].ToString()))){
						inswriteoff-=PIn.Double(TableInsWriteoff.Rows[j][1].ToString());
					}
				}
				for(int j=0; j<TablePay.Rows.Count; j++){
				  if(dates[i]==(PIn.Date(TablePay.Rows[j][0].ToString()))){
						ptincome+=PIn.Double(TablePay.Rows[j][1].ToString());
					}																																						 
   			}
				for(int j=0; j<TableIns.Rows.Count; j++){// new TableIns, SPK
					if(dates[i]==(PIn.Date(TableIns.Rows[j][0].ToString()))){
						insincome+=PIn.Double(TableIns.Rows[j][1].ToString());
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
				report.ColTotal[2]+=production;
				report.ColTotal[3]+=scheduled;
				report.ColTotal[4]+=adjust;
				report.ColTotal[5]+=inswriteoff; //spk 5/19/05
				report.ColTotal[6]+=totalproduction;
				report.ColTotal[7]+=ptincome;	// spk
				report.ColTotal[8]+=insincome;	// spk
				report.ColTotal[9]+=totalincome;
				report.TableQ.Rows.Add(row);  //adds row to table Q
      }
			FormQuery2=new FormQuery(report);
			FormQuery2.IsReport=true;
			FormQuery2.ResetGrid();//necessary won't work without
			report.Title="Production and Income";
			report.SubTitle.Add(PrefC.GetString(PrefName.PracticeTitle));
			report.SubTitle.Add(textDateFrom.Text+" - "+textDateTo.Text);
			if(checkAllProv.Checked) {
				report.SubTitle.Add(Lan.g(this,"All Providers"));
			}
			else{
				string str="";
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i>0){
						str+=", ";
					}
					str+=ProviderC.List[listProv.SelectedIndices[i]].Abbr;
				}
				report.SubTitle.Add(str);
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
			report.Summary.Add(
				//=Lan.g(this,"Total Production (Production + Scheduled + Adjustments):")+" "
				//+(report.ColTotal[2]+report.ColTotal[3]
				//+report.ColTotal[4]).ToString("c"); //spk 5/19/05
				Lan.g(this,"Total Production (Production + Scheduled + Adj - Writeoff):")+" "
				+(report.ColTotal[2]+report.ColTotal[3]+report.ColTotal[4]
				+report.ColTotal[5]).ToString("c"));
			report.Summary.Add("");
			report.Summary.Add(
				Lan.g(this,"Total Income (Pt Income + Ins Income):")+" "
				+(report.ColTotal[7]+report.ColTotal[8]).ToString("c"));
			report.ColPos[0]=20;
			report.ColPos[1]=110;
			report.ColPos[2]=190;
			report.ColPos[3]=270;
			report.ColPos[4]=350;
			report.ColPos[5]=420;
			report.ColPos[6]=490;
			report.ColPos[7]=560;
			report.ColPos[8]=630;
			report.ColPos[9]=700;
			report.ColPos[10]=770;
			report.ColCaption[0]="Date";
			report.ColCaption[1]="Weekday";
			report.ColCaption[2]="Production";
			report.ColCaption[3]="Sched";
			report.ColCaption[4]="Adj";
			report.ColCaption[5]="Writeoff";		//spk 5/19/05
			report.ColCaption[6]="Tot Prod";
			report.ColCaption[7]="Pt Income";		// spk
			report.ColCaption[8]="Ins Income";		// spk
			report.ColCaption[9]="Tot Income";
      report.ColAlign[2]=HorizontalAlignment.Right;
			report.ColAlign[3]=HorizontalAlignment.Right;
			report.ColAlign[4]=HorizontalAlignment.Right;
			report.ColAlign[5]=HorizontalAlignment.Right;
			report.ColAlign[6]=HorizontalAlignment.Right;
			report.ColAlign[7]=HorizontalAlignment.Right;
			report.ColAlign[8]=HorizontalAlignment.Right;
			report.ColAlign[9]=HorizontalAlignment.Right;
			FormQuery2.ShowDialog();
		}

		private void RunAnnual(){
			dateFrom=PIn.Date(textDateFrom.Text);
			dateTo=PIn.Date(textDateTo.Text);
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
			ReportSimpleGrid report=new ReportSimpleGrid();
			//Procedures------------------------------------------------------------------------------
			string whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="procedurelog.ProvNum = "
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
						whereClin+="procedurelog.ClinicNum = 0 ";
					}
					else {
						whereClin+="procedurelog.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			report.Query="SELECT "
				+"procedurelog.ProcDate,"
				+"SUM(procedurelog.ProcFee*(CASE procedurelog.UnitQty+procedurelog.BaseUnits WHEN 0 THEN 1 ELSE procedurelog.UnitQty+procedurelog.BaseUnits END))-IFNULL(SUM(claimproc.WriteOff),0) "
				+"FROM procedurelog "
				+"LEFT JOIN claimproc ON procedurelog.ProcNum=claimproc.ProcNum "
				+"AND claimproc.Status='7' "//only CapComplete writeoffs are subtracted here.
				+"WHERE procedurelog.ProcStatus = '2' "
				+whereProv
				+whereClin
				+"AND procedurelog.ProcDate >= " +POut.Date(dateFrom)+" "
				+"AND procedurelog.ProcDate <= " +POut.Date(dateTo)+" "
				+"GROUP BY MONTH(procedurelog.ProcDate)";
			//MessageBox.Show(report.Query);
			TableProduction=report.GetTempTable();
			//Adjustments----------------------------------------------------------------------------
			whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="adjustment.ProvNum = "
						+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
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
						whereClin+="adjustment.ClinicNum = 0 ";
					}
					else {
						whereClin+="adjustment.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			report.Query="SELECT "
				+"adjustment.AdjDate,"
				+"SUM(adjustment.AdjAmt) "
				+"FROM adjustment "
				+"WHERE adjustment.AdjDate >= "+POut.Date(dateFrom)+" "
				+"AND adjustment.AdjDate <= "+POut.Date(dateTo)+" "
				+whereProv
				+whereClin
				+"GROUP BY MONTH(adjustment.AdjDate)";
			TableAdj=report.GetTempTable();
			//TableInsWriteoff--------------------------------------------------------------------------
			whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="ProvNum = "
						+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
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
						whereClin+="claimproc.ClinicNum = 0 ";
					}
					else {
						whereClin+="claimproc.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			if(radioWriteoffPay.Checked){
				report.Query="SELECT "
					+"claimproc.DateCP," 
					+"SUM(claimproc.WriteOff) "
					+"FROM claimproc "
					+"WHERE claimproc.DateCP >= "+POut.Date(dateFrom)+" "
					+"AND claimproc.DateCP <= "+POut.Date(dateTo)+" "
					+whereProv
					+whereClin
					+"AND (claimproc.Status=1 OR claimproc.Status=4) "//Received or supplemental
					+"GROUP BY MONTH(claimproc.DateCP)";
			}
			else{
				report.Query="SELECT "
					+"claimproc.ProcDate," 
					+"SUM(claimproc.WriteOff) "
					+"FROM claimproc "
					+"WHERE claimproc.ProcDate >= "+POut.Date(dateFrom)+" "
					+"AND claimproc.ProcDate <= "+POut.Date(dateTo)+" "
					+whereProv
					+whereClin
					+"AND (claimproc.Status=1 OR claimproc.Status=4 OR claimproc.Status=0) " //received or supplemental or notreceived
					+"GROUP BY MONTH(claimproc.ProcDate)";
			}
			TableInsWriteoff=report.GetTempTable();
			//PtIncome--------------------------------------------------------------------------------
			whereProv="";
			if(!checkAllProv.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="paysplit.ProvNum = "
						+POut.Long(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
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
						whereClin+="paysplit.ClinicNum = 0 ";
					}
					else {
						whereClin+="paysplit.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			report.Query="SELECT "
				+"paysplit.DatePay,"
				+"SUM(paysplit.SplitAmt) "
				+"FROM paysplit "
				+"WHERE paysplit.IsDiscount=0 "//AND paysplit.PayNum=payment.PayNum "
				+whereProv
				+whereClin
				+"AND paysplit.DatePay >= "+POut.Date(dateFrom)+" "
				+"AND paysplit.DatePay <= "+POut.Date(dateTo)+" "
				+"GROUP BY MONTH(paysplit.DatePay)";
			TablePay=report.GetTempTable();
			//InsIncome---------------------------------------------------------------------------------
			whereProv="";
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
						whereClin+="claimproc.ClinicNum = 0 ";
					}
					else {
						whereClin+="claimproc.ClinicNum = "+POut.Long(Clinics.List[listClin.SelectedIndices[i]-1].ClinicNum)+" ";
					}
				}
				whereClin+=") ";
			}
			report.Query= "SELECT claimpayment.CheckDate,SUM(claimproc.InsPayamt) "
				+"FROM claimpayment,claimproc WHERE "
				+"claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum "
				+"AND claimpayment.CheckDate >= " + POut.Date(dateFrom)+" "
				+"AND claimpayment.CheckDate <= " + POut.Date(dateTo)+" "
				+whereProv
				+whereClin
				+" GROUP BY claimpayment.CheckDate ORDER BY checkdate";
			TableIns=report.GetTempTable(); 
			report.TableQ=new DataTable(null);//new table with 7 columns
			for(int i=0;i<8;i++){ //add columns
				report.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			report.InitializeColumns();
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
				DataRow row=report.TableQ.NewRow();
				row[0]=dates[i].ToString("MMM yy");
				production=0;
				adjust=0;
				inswriteoff=0;	//spk 5/19/05
				totalproduction=0;
				ptincome=0;
				insincome=0;
				totalincome=0;
				for(int j=0;j<TableProduction.Rows.Count;j++)  {
				  if(dates[i].Year==PIn.Date(TableProduction.Rows[j][0].ToString()).Year
						&& dates[i].Month==PIn.Date(TableProduction.Rows[j][0].ToString()).Month){
		 			  production+=PIn.Double(TableProduction.Rows[j][1].ToString());
					}
   			}
				for(int j=0;j<TableAdj.Rows.Count; j++){
				  if(dates[i].Year==PIn.Date(TableAdj.Rows[j][0].ToString()).Year
						&& dates[i].Month==PIn.Date(TableAdj.Rows[j][0].ToString()).Month){
						adjust+=PIn.Double(TableAdj.Rows[j][1].ToString());
					}
   			}
				for(int j=0;j<TableInsWriteoff.Rows.Count; j++){
					if(dates[i].Year==PIn.Date(TableInsWriteoff.Rows[j][0].ToString()).Year
						&& dates[i].Month==PIn.Date(TableInsWriteoff.Rows[j][0].ToString()).Month){
						inswriteoff-=PIn.Double(TableInsWriteoff.Rows[j][1].ToString());
					}
				}
				for(int j=0;j<TablePay.Rows.Count; j++){
				  if(dates[i].Year==PIn.Date(TablePay.Rows[j][0].ToString()).Year
						&& dates[i].Month==PIn.Date(TablePay.Rows[j][0].ToString()).Month){
						ptincome+=PIn.Double(TablePay.Rows[j][1].ToString());
					}																																						 
   			}
				for(int j=0; j<TableIns.Rows.Count; j++){//
					if(dates[i].Year==PIn.Date(TableIns.Rows[j][0].ToString()).Year
						&& dates[i].Month==PIn.Date(TableIns.Rows[j][0].ToString()).Month){
						insincome+=PIn.Double(TableIns.Rows[j][1].ToString());
					}																																						 
				}
				totalproduction=production+adjust+inswriteoff;
				totalincome=ptincome+insincome;
				row[1]=production.ToString("n");
				row[2]=adjust.ToString("n");
				row[3]=inswriteoff.ToString("n");
				row[4]=totalproduction.ToString("n");
				row[5]=ptincome.ToString("n");
				row[6]=insincome.ToString("n");		
				row[7]=totalincome.ToString("n");
				report.ColTotal[1]+=production;
				report.ColTotal[2]+=adjust;	
				report.ColTotal[3]+=inswriteoff;
				report.ColTotal[4]+=totalproduction;	
				report.ColTotal[5]+=ptincome;	
				report.ColTotal[6]+=insincome;	
				report.ColTotal[7]+=totalincome;
				report.TableQ.Rows.Add(row);  //adds row to table Q
      }
			FormQuery2=new FormQuery(report);
			FormQuery2.IsReport=true;
			FormQuery2.ResetGrid();//necessary won't work without
			report.Title="Annual Production and Income";
			report.SubTitle.Add(PrefC.GetString(PrefName.PracticeTitle));
			report.SubTitle.Add(textDateFrom.Text+" - "+textDateTo.Text);
			if(checkAllProv.Checked) {
				report.SubTitle.Add(Lan.g(this,"All Providers"));
			}
			else{
				string str="";
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i>0){
						str+=", ";
					}
					str+=ProviderC.List[listProv.SelectedIndices[i]].Abbr;
				}
				report.SubTitle.Add(str);
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
			report.ColPos[0]=20;
			report.ColPos[1]=120;
			report.ColPos[2]=210;
			report.ColPos[3]=300;
			report.ColPos[4]=390;
			report.ColPos[5]=480;
			report.ColPos[6]=570;
			report.ColPos[7]=660;
			report.ColPos[8]=750;
			//Month
			//Production
			//Adjustments
			//Total Production
			//Pt Income
			//Ins Income
			//Total Income
			report.ColCaption[0]="Month";
			report.ColCaption[1]="Production";
			report.ColCaption[2]="Adjustments";
			report.ColCaption[3]="Writeoff";	//spk
			report.ColCaption[4]="Tot Prod";
			report.ColCaption[5]="Pt Income";
			report.ColCaption[6]="Ins Income";
			report.ColCaption[7]="Total Income";
			report.ColAlign[1]=HorizontalAlignment.Right;
      report.ColAlign[2]=HorizontalAlignment.Right;
			report.ColAlign[3]=HorizontalAlignment.Right;
			report.ColAlign[4]=HorizontalAlignment.Right;
			report.ColAlign[5]=HorizontalAlignment.Right;
			report.ColAlign[6]=HorizontalAlignment.Right;
			report.ColAlign[7]=HorizontalAlignment.Right;
			FormQuery2.ShowDialog();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(!checkAllProv.Checked && listProv.SelectedIndices.Count==0){
				MsgBox.Show(this,"At least one provider must be selected.");
				return;
			}
			if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
				if(!checkAllClin.Checked && listClin.SelectedIndices.Count==0) {
					MsgBox.Show(this,"At least one clinic must be selected.");
					return;
				}
			}
			dateFrom=PIn.Date(textDateFrom.Text);
			dateTo=PIn.Date(textDateTo.Text);
			if(radioDaily.Checked){
				RunDaily();
			}
			else if(radioMonthly.Checked){
				RunMonthly();
			}
			else{//annual
				if(dateFrom.AddYears(1) <= dateTo || dateFrom.Year != dateTo.Year) {
					MsgBox.Show(this,"Date range for annual report cannot be greater than one year and must be within the same year.");
					return;
				}
				RunAnnual();
			}
			//DialogResult=DialogResult.OK;//Stay here so that a series of similar reports can be run
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

	

		

		

		

		


		
	}
}








