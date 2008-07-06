using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpReferralAnalysis:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.ComponentModel.Container components = null;
		private ListBox listProv;
		private Label label1;
		private GroupBox groupBox2;
		private OpenDental.UI.Button butRight;
		private OpenDental.UI.Button butThis;
		private Label label2;
		private ValidDate textDateFrom;
		private ValidDate textDateTo;
		private Label label3;
		private OpenDental.UI.Button butLeft;
		private TextBox textToday;
		private Label label4;
		private CheckBox checkAddress;
		private Label label5;
		private CheckBox checkNewPat;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpReferralAnalysis() {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpReferralAnalysis));
			this.listProv = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textToday = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.checkAddress = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.butRight = new OpenDental.UI.Button();
			this.butThis = new OpenDental.UI.Button();
			this.textDateFrom = new OpenDental.ValidDate();
			this.textDateTo = new OpenDental.ValidDate();
			this.butLeft = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.checkNewPat = new System.Windows.Forms.CheckBox();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(42,95);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(165,186);
			this.listProv.TabIndex = 36;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(41,76);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104,16);
			this.label1.TabIndex = 35;
			this.label1.Text = "Providers";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
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
			this.groupBox2.Location = new System.Drawing.Point(271,89);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(281,144);
			this.groupBox2.TabIndex = 46;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Date Range";
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
			// textToday
			// 
			this.textToday.Location = new System.Drawing.Point(366,63);
			this.textToday.Name = "textToday";
			this.textToday.ReadOnly = true;
			this.textToday.Size = new System.Drawing.Size(100,20);
			this.textToday.TabIndex = 45;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(237,66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(127,20);
			this.label4.TabIndex = 44;
			this.label4.Text = "Today\'s Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkAddress
			// 
			this.checkAddress.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkAddress.Location = new System.Drawing.Point(42,307);
			this.checkAddress.Name = "checkAddress";
			this.checkAddress.Size = new System.Drawing.Size(381,32);
			this.checkAddress.TabIndex = 47;
			this.checkAddress.Text = "Include address information.  It won\'t fit on the printout, but is useful if you " +
    "are exporting for letter merge.";
			this.checkAddress.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkAddress.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(41,9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(585,36);
			this.label5.TabIndex = 48;
			this.label5.Text = "This report is based on procedures completed rather than appointments.  Also, the" +
    " production numbers will only include production for patients with referrals, no" +
    "t necessarily everyone.";
			// 
			// butRight
			// 
			this.butRight.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRight.Autosize = true;
			this.butRight.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRight.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRight.CornerRadius = 4F;
			this.butRight.Image = global::OpenDental.Properties.Resources.Right;
			this.butRight.Location = new System.Drawing.Point(205,33);
			this.butRight.Name = "butRight";
			this.butRight.Size = new System.Drawing.Size(45,24);
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
			this.butThis.Location = new System.Drawing.Point(95,33);
			this.butThis.Name = "butThis";
			this.butThis.Size = new System.Drawing.Size(101,24);
			this.butThis.TabIndex = 45;
			this.butThis.Text = "This Month";
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
			this.butLeft.Location = new System.Drawing.Point(41,33);
			this.butLeft.Name = "butLeft";
			this.butLeft.Size = new System.Drawing.Size(45,24);
			this.butLeft.TabIndex = 44;
			this.butLeft.Click += new System.EventHandler(this.butLeft_Click);
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
			this.butCancel.Location = new System.Drawing.Point(569,371);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
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
			this.butOK.Location = new System.Drawing.Point(569,339);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// checkNewPat
			// 
			this.checkNewPat.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkNewPat.Location = new System.Drawing.Point(42,345);
			this.checkNewPat.Name = "checkNewPat";
			this.checkNewPat.Size = new System.Drawing.Size(350,20);
			this.checkNewPat.TabIndex = 49;
			this.checkNewPat.Text = "Only include new patients.";
			this.checkNewPat.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkNewPat.UseVisualStyleBackColor = true;
			// 
			// FormRpReferralAnalysis
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(678,423);
			this.Controls.Add(this.checkNewPat);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.checkAddress);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.textToday);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpReferralAnalysis";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Referral Analysis";
			this.Load += new System.EventHandler(this.FormReferralAnalysis_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormReferralAnalysis_Load(object sender, System.EventArgs e) {
			textToday.Text=DateTime.Today.ToShortDateString();
			//always defaults to the current month
			textDateFrom.Text=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1).ToShortDateString();
			textDateTo.Text=new DateTime(DateTime.Today.Year,DateTime.Today.Month
				,DateTime.DaysInMonth(DateTime.Today.Year,DateTime.Today.Month)).ToShortDateString();
			listProv.Items.Add(Lan.g(this,"all"));
			for(int i=0;i<ProviderC.List.Length;i++){
				listProv.Items.Add(ProviderC.List[i].GetLongDesc());
			}
			listProv.SetSelected(0,true);
		}

		private void butThis_Click(object sender,EventArgs e) {
			textDateFrom.Text=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1).ToShortDateString();
			textDateTo.Text=new DateTime(DateTime.Today.Year,DateTime.Today.Month
				,DateTime.DaysInMonth(DateTime.Today.Year,DateTime.Today.Month)).ToShortDateString();
		}

		private void butLeft_Click(object sender,EventArgs e) {
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			DateTime dateTo=PIn.PDate(textDateTo.Text);
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

		private void butRight_Click(object sender,EventArgs e) {
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			DateTime dateTo=PIn.PDate(textDateTo.Text);
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

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(listProv.SelectedIndices.Count==0) {
				MsgBox.Show(this,"At least one provider must be selected.");
				return;
			}
			if(listProv.SelectedIndices[0]==0 && listProv.SelectedIndices.Count>1){
				MsgBox.Show(this,"You cannot select 'all' providers as well as specific providers.");
				return;
			}
			DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			DateTime dateTo=PIn.PDate(textDateTo.Text);
			string whereProv="";
			if(listProv.SelectedIndices[0]!=0){
				for(int i=0;i<listProv.SelectedIndices.Count;i++){
					if(i==0){
						whereProv+=" AND (";
					}
					else{
						whereProv+="OR ";
					}
					whereProv+="claimproc.ProvNum = "
						+POut.PInt(ProviderC.List[listProv.SelectedIndices[i]-1].ProvNum)+" ";
				}
				whereProv+=") ";
			}
			Queries.CurReport=new ReportOld();
			Queries.CurReport.Query=@"SELECT referral.LName,referral.FName,
COUNT(DISTINCT refattach.PatNum) HowMany,
SUM(procedurelog.ProcFee) $HowMuch";
			if(checkAddress.Checked){
				Queries.CurReport.Query+=",referral.Title,referral.Address,referral.Address2,referral.City,"
					+"referral.ST,referral.Zip,referral.Specialty";
			}
			Queries.CurReport.Query+=@" FROM referral,refattach,procedurelog,patient
WHERE referral.ReferralNum=refattach.ReferralNum
AND procedurelog.PatNum=refattach.PatNum
AND procedurelog.PatNum=patient.PatNum
AND refattach.IsFrom=1
AND procedurelog.ProcStatus=2
AND procedurelog.ProcDate >= "+POut.PDate(dateFrom)+" "
				+"AND procedurelog.ProcDate <= "+POut.PDate(dateTo)+" ";
			if(checkNewPat.Checked){
				Queries.CurReport.Query+="AND patient.DateFirstVisit >= "+POut.PDate(dateFrom)+" "
					+"AND patient.DateFirstVisit <= "+POut.PDate(dateTo)+" ";
			}
			Queries.CurReport.Query+=@"GROUP BY referral.ReferralNum
ORDER BY HowMany Desc";
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();			
			Queries.CurReport.Title="Referral Analysis";
			Queries.CurReport.SubTitle=new string[2];
			Queries.CurReport.SubTitle[0]=((Pref)PrefC.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=dateFrom.ToString("d")+" - "+dateTo.ToString("d");
			if(checkAddress.Checked){
				Queries.CurReport.ColPos=new int[12];
				Queries.CurReport.ColCaption=new string[11];
				Queries.CurReport.ColAlign=new HorizontalAlignment[11];
				Queries.CurReport.ColPos[0]=20;
				Queries.CurReport.ColPos[1]=120;
				Queries.CurReport.ColPos[2]=220;
				Queries.CurReport.ColPos[3]=290;
				Queries.CurReport.ColPos[4]=370;
				Queries.CurReport.ColPos[5]=410;
				Queries.CurReport.ColPos[6]=510;
				Queries.CurReport.ColPos[7]=550;
				Queries.CurReport.ColPos[8]=610;
				Queries.CurReport.ColPos[9]=650;
				Queries.CurReport.ColPos[10]=700;
				Queries.CurReport.ColPos[11]=900;//off the right side
				Queries.CurReport.ColCaption[0]="Last Name";
				Queries.CurReport.ColCaption[1]="First Name";			
				Queries.CurReport.ColCaption[2]="Count";
				Queries.CurReport.ColCaption[3]="Production";
				Queries.CurReport.ColCaption[4]="Title";
				Queries.CurReport.ColCaption[5]="Address";
				Queries.CurReport.ColCaption[6]="Add2";
				Queries.CurReport.ColCaption[7]="City";
				Queries.CurReport.ColCaption[8]="ST";
				Queries.CurReport.ColCaption[9]="Zip";
				Queries.CurReport.ColCaption[10]="Specialty";
				Queries.CurReport.ColAlign[3]=HorizontalAlignment.Right;
			}
			else{
				Queries.CurReport.ColPos=new int[5];
				Queries.CurReport.ColCaption=new string[4];
				Queries.CurReport.ColAlign=new HorizontalAlignment[4];
				Queries.CurReport.ColPos[0]=20;
				Queries.CurReport.ColPos[1]=120;
				Queries.CurReport.ColPos[2]=220;
				Queries.CurReport.ColPos[3]=290;
				Queries.CurReport.ColPos[4]=370;
				//Queries.CurReport.ColPos[5]=900;//off the right side
				Queries.CurReport.ColCaption[0]="Last Name";
				Queries.CurReport.ColCaption[1]="First Name";			
				Queries.CurReport.ColCaption[2]="Count";
				Queries.CurReport.ColCaption[3]="Production";
				Queries.CurReport.ColAlign[3]=HorizontalAlignment.Right;
			}
			Queries.CurReport.Summary=new string[0];
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}
		

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		
	}
}
