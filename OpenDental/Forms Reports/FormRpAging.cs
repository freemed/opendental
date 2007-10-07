using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FormRpAging : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.GroupBox groupBox1;
		private FormQuery FormQuery2;
		private System.Windows.Forms.Label label1;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.ListBox listBillType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton radio30;
		private System.Windows.Forms.RadioButton radio90;
		private System.Windows.Forms.RadioButton radio60;
		private System.Windows.Forms.CheckBox checkIncludeNeg;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkOnlyNeg;
		private System.Windows.Forms.CheckBox checkExcludeInactive;
		private ListBox listProv;
		private Label label3;
		private CheckBox checkProvAll;
		private CheckBox checkBillTypesAll;
		private System.Windows.Forms.RadioButton radioAny;

		///<summary></summary>
		public FormRpAging(){
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose(bool disposing){
			if(disposing){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpAging));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radio30 = new System.Windows.Forms.RadioButton();
			this.radio90 = new System.Windows.Forms.RadioButton();
			this.radio60 = new System.Windows.Forms.RadioButton();
			this.radioAny = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.listBillType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkIncludeNeg = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkOnlyNeg = new System.Windows.Forms.CheckBox();
			this.checkExcludeInactive = new System.Windows.Forms.CheckBox();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.checkProvAll = new System.Windows.Forms.CheckBox();
			this.checkBillTypesAll = new System.Windows.Forms.CheckBox();
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(637,396);
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
			this.butOK.Location = new System.Drawing.Point(637,362);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radio30);
			this.groupBox1.Controls.Add(this.radio90);
			this.groupBox1.Controls.Add(this.radio60);
			this.groupBox1.Controls.Add(this.radioAny);
			this.groupBox1.Location = new System.Drawing.Point(57,109);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(175,120);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Age of Account";
			// 
			// radio30
			// 
			this.radio30.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio30.Location = new System.Drawing.Point(12,44);
			this.radio30.Name = "radio30";
			this.radio30.Size = new System.Drawing.Size(152,16);
			this.radio30.TabIndex = 1;
			this.radio30.Text = "Over 30 Days";
			// 
			// radio90
			// 
			this.radio90.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio90.Location = new System.Drawing.Point(12,90);
			this.radio90.Name = "radio90";
			this.radio90.Size = new System.Drawing.Size(152,18);
			this.radio90.TabIndex = 3;
			this.radio90.Text = "Over 90 Days";
			// 
			// radio60
			// 
			this.radio60.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio60.Location = new System.Drawing.Point(12,66);
			this.radio60.Name = "radio60";
			this.radio60.Size = new System.Drawing.Size(152,18);
			this.radio60.TabIndex = 2;
			this.radio60.Text = "Over 60 Days";
			// 
			// radioAny
			// 
			this.radioAny.Checked = true;
			this.radioAny.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioAny.Location = new System.Drawing.Point(12,20);
			this.radioAny.Name = "radioAny";
			this.radioAny.Size = new System.Drawing.Size(152,18);
			this.radioAny.TabIndex = 0;
			this.radioAny.TabStop = true;
			this.radioAny.Text = "Any Balance";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8,50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(114,14);
			this.label1.TabIndex = 11;
			this.label1.Text = "Last Updated";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(126,48);
			this.textDate.Name = "textDate";
			this.textDate.ReadOnly = true;
			this.textDate.Size = new System.Drawing.Size(78,20);
			this.textDate.TabIndex = 0;
			// 
			// listBillType
			// 
			this.listBillType.Location = new System.Drawing.Point(335,69);
			this.listBillType.Name = "listBillType";
			this.listBillType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBillType.Size = new System.Drawing.Size(158,186);
			this.listBillType.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(332,25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(176,16);
			this.label2.TabIndex = 14;
			this.label2.Text = "Billing Types";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkIncludeNeg
			// 
			this.checkIncludeNeg.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIncludeNeg.Location = new System.Drawing.Point(9,28);
			this.checkIncludeNeg.Name = "checkIncludeNeg";
			this.checkIncludeNeg.Size = new System.Drawing.Size(194,20);
			this.checkIncludeNeg.TabIndex = 17;
			this.checkIncludeNeg.Text = "Include negative balances";
			this.checkIncludeNeg.Click += new System.EventHandler(this.checkIncludeNeg_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkOnlyNeg);
			this.groupBox2.Controls.Add(this.checkIncludeNeg);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(57,256);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(225,84);
			this.groupBox2.TabIndex = 18;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Negative Balances";
			// 
			// checkOnlyNeg
			// 
			this.checkOnlyNeg.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkOnlyNeg.Location = new System.Drawing.Point(9,55);
			this.checkOnlyNeg.Name = "checkOnlyNeg";
			this.checkOnlyNeg.Size = new System.Drawing.Size(210,19);
			this.checkOnlyNeg.TabIndex = 18;
			this.checkOnlyNeg.Text = "Only show negative balances";
			this.checkOnlyNeg.Click += new System.EventHandler(this.checkOnlyNeg_Click);
			// 
			// checkExcludeInactive
			// 
			this.checkExcludeInactive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeInactive.Location = new System.Drawing.Point(66,363);
			this.checkExcludeInactive.Name = "checkExcludeInactive";
			this.checkExcludeInactive.Size = new System.Drawing.Size(248,18);
			this.checkExcludeInactive.TabIndex = 19;
			this.checkExcludeInactive.Text = "Exclude inactive patients";
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(544,69);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(163,186);
			this.listProv.TabIndex = 39;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(541,25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104,16);
			this.label3.TabIndex = 38;
			this.label3.Text = "Providers";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkProvAll
			// 
			this.checkProvAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkProvAll.Location = new System.Drawing.Point(544,46);
			this.checkProvAll.Name = "checkProvAll";
			this.checkProvAll.Size = new System.Drawing.Size(145,18);
			this.checkProvAll.TabIndex = 40;
			this.checkProvAll.Text = "All";
			this.checkProvAll.Click += new System.EventHandler(this.checkProvAll_Click);
			// 
			// checkBillTypesAll
			// 
			this.checkBillTypesAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBillTypesAll.Location = new System.Drawing.Point(335,46);
			this.checkBillTypesAll.Name = "checkBillTypesAll";
			this.checkBillTypesAll.Size = new System.Drawing.Size(145,18);
			this.checkBillTypesAll.TabIndex = 41;
			this.checkBillTypesAll.Text = "All";
			this.checkBillTypesAll.Click += new System.EventHandler(this.checkBillTypesAll_Click);
			// 
			// FormRpAging
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(742,450);
			this.Controls.Add(this.checkBillTypesAll);
			this.Controls.Add(this.checkProvAll);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkExcludeInactive);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listBillType);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpAging";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Aging Report";
			this.Load += new System.EventHandler(this.FormAging_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormAging_Load(object sender, System.EventArgs e) {
			textDate.Text=(PIn.PDate(PrefB.GetString("DateLastAging"))).ToShortDateString();
			if(PIn.PDate(PrefB.GetString("DateLastAging")) < DateTime.Today){
				if(MessageBox.Show(Lan.g(this,"Update aging first?"),"",MessageBoxButtons.YesNo)==DialogResult.Yes){
					FormAging FormA=new FormAging();
					FormA.ShowDialog();
					if(FormA.DialogResult==DialogResult.OK){
						textDate.Text=DateTime.Today.ToShortDateString();
					}
				}
			}
			for(int i=0;i<DefB.Short[(int)DefCat.BillingTypes].Length;i++){
				listBillType.Items.Add(DefB.Short[(int)DefCat.BillingTypes][i].ItemName);
			}
			if(listBillType.Items.Count>0){
				listBillType.SelectedIndex=0;
			}
			listBillType.Visible=false;
			checkBillTypesAll.Checked=true;
			for(int i=0;i<Providers.List.Length;i++){
				listProv.Items.Add(Providers.List[i].GetNameLF());
			}
			if(listProv.Items.Count>0) {
				listProv.SelectedIndex=0;
			}
			checkProvAll.Checked=true;
			listProv.Visible=false;
		}

		private void checkBillTypesAll_Click(object sender,EventArgs e) {
			if(checkBillTypesAll.Checked){
				listBillType.Visible=false;
			}
			else{
				listBillType.Visible=true;
			}
		}

		private void checkProvAll_Click(object sender,EventArgs e) {
			if(checkProvAll.Checked) {
				listProv.Visible=false;
			}
			else {
				listProv.Visible=true;
			}
		}

		private void checkIncludeNeg_Click(object sender, System.EventArgs e) {
			if(checkIncludeNeg.Checked){
				checkOnlyNeg.Checked=false;
			}
		}

		private void checkOnlyNeg_Click(object sender, System.EventArgs e) {
			if(checkOnlyNeg.Checked){
				checkIncludeNeg.Checked=false;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!checkBillTypesAll.Checked && listBillType.SelectedIndices.Count==0){
				MsgBox.Show(this,"At least one billing type must be selected.");
				return;
			}
			if(!checkProvAll.Checked && listProv.SelectedIndices.Count==0) {
				MsgBox.Show(this,"At least one provider must be selected.");
				return;
			}
			Queries.CurReport=new ReportOld();
			Queries.CurReport.Query="SELECT CONCAT(CONCAT(CONCAT(CONCAT(LName,', '),FName),' '),MiddleI)"
				+",Bal_0_30,Bal_31_60,Bal_61_90,BalOver90"
				+",BalTotal "
				+",InsEst"
				+",BalTotal-InsEst AS $pat "
				+"FROM patient ";
			if(checkOnlyNeg.Checked){
				Queries.CurReport.Query+="WHERE BalTotal < '-.005'";
			}
			else{
				Queries.CurReport.Query+="WHERE ";
				if(checkExcludeInactive.Checked){
					Queries.CurReport.Query+="(patstatus != 2) AND ";
				}
				if(radioAny.Checked){
					Queries.CurReport.Query+=
						"(Bal_0_30 > '.005' OR Bal_31_60 > '.005' OR Bal_61_90 > '.005' OR BalOver90 > '.005'";
				}
				else if(radio30.Checked){
					Queries.CurReport.Query+=
						"(Bal_31_60 > '.005' OR Bal_61_90 > '.005' OR BalOver90 > '.005'";
				}
				else if(radio60.Checked){
					Queries.CurReport.Query+=
						"(Bal_61_90 > '.005' OR BalOver90 > '.005'";
				}
				else if(radio90.Checked){
					Queries.CurReport.Query+=
						"(BalOver90 > '.005'";
				}
				if(checkIncludeNeg.Checked){
					Queries.CurReport.Query+=" OR BalTotal < '-.005'";
				}
				Queries.CurReport.Query+=") ";
			}
			if(!checkBillTypesAll.Checked){
				for(int i=0;i<listBillType.SelectedIndices.Count;i++) {
					if(i==0) {
						Queries.CurReport.Query+=" AND (billingtype = ";
					}
					else {
						Queries.CurReport.Query+=" OR billingtype = ";
					}
					Queries.CurReport.Query+=POut.PInt(DefB.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndices[i]].DefNum);
				}
				Queries.CurReport.Query+=") ";
			}
			if(!checkProvAll.Checked) {
				for(int i=0;i<listProv.SelectedIndices.Count;i++) {
					if(i==0) {
						Queries.CurReport.Query+=" AND (PriProv = ";
					}
					else {
						Queries.CurReport.Query+=" OR PriProv = ";
					}
					Queries.CurReport.Query+=POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum);
				}
				Queries.CurReport.Query+=") ";
			}
			Queries.CurReport.Query+="ORDER BY LName,FName";
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();		
			Queries.CurReport.Title="AGING REPORT";
			Queries.CurReport.SubTitle=new string[4];
			Queries.CurReport.SubTitle[0]=((Pref)PrefB.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]="As of "+textDate.Text;
			if(radioAny.Checked){
				Queries.CurReport.SubTitle[2]="Any Balance";
			}
			if(radio30.Checked){
				Queries.CurReport.SubTitle[2]="Over 30 Days";
			}
			if(radio60.Checked){
				Queries.CurReport.SubTitle[2]="Over 60 Days";
			}
			if(radio90.Checked){
				Queries.CurReport.SubTitle[2]="Over 90 Days";
			}
			if(listBillType.SelectedIndices.Count==DefB.Short[(int)DefCat.BillingTypes].Length){
				Queries.CurReport.SubTitle[3]="All Billing Types";
			}
			else{
				Queries.CurReport.SubTitle[3]=DefB.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndices[0]].ItemName;
				for(int i=1;i<listBillType.SelectedIndices.Count;i++){
					Queries.CurReport.SubTitle[3]+=", "+DefB.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndices[i]].ItemName;
				}
			}
			Queries.CurReport.ColPos=new int[9];
			Queries.CurReport.ColCaption=new string[8];
			Queries.CurReport.ColAlign=new HorizontalAlignment[8];

			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=180;
			Queries.CurReport.ColPos[2]=260;
			Queries.CurReport.ColPos[3]=340;
			Queries.CurReport.ColPos[4]=420;
			Queries.CurReport.ColPos[5]=500;
			Queries.CurReport.ColPos[6]=585;
			Queries.CurReport.ColPos[7]=670;
			Queries.CurReport.ColPos[8]=755;

			Queries.CurReport.ColCaption[0]="GUARANTOR";
			Queries.CurReport.ColCaption[1]="0-30 DAYS";
			Queries.CurReport.ColCaption[2]="30-60 DAYS";
			Queries.CurReport.ColCaption[3]="60-90 DAYS";
			Queries.CurReport.ColCaption[4]="> 90 DAYS";
			Queries.CurReport.ColCaption[5]="TOTAL";
			Queries.CurReport.ColCaption[6]="-INS EST";
			Queries.CurReport.ColCaption[7]="=PATIENT";

			Queries.CurReport.ColAlign[1]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[2]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[3]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[5]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[6]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[7]=HorizontalAlignment.Right;

			Queries.CurReport.Summary=new string[0];
			FormQuery2.ShowDialog();		
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
		
		}

		

		

		

		

	}

	///<summary></summary>
	public struct Aging{
		///<summary></summary>
		public double Charges;
		///<summary></summary>
		public double Payments;
		///<summary></summary>
		public double Aged;
	}



}
