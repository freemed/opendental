using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FormStatementOptions : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.ComponentModel.IContainer components;// Required designer variable.
		private System.Windows.Forms.CheckBox checkHidePayment;
		///<summary></summary>
		public int[] PatNums;
		//<summary>Dates are the only parameters that get passed to this form. But all the parameters are sent back out from this form.</summary>
		//public DateTime FromDate;
		//<summary></summary>
		//public DateTime ToDate;
		//<summary></summary>
		//public bool IncludeClaims;
		//<summary></summary>
		//public bool SubtotalsOnly;
		///<summary></summary>
		public bool HidePayment;
		//<summary></summary>
		//public bool NextAppt;
		//<summary></summary>
		//public bool SimpleStatement;
		//<summary>When user checks box in this form for Mailing, then this gets set to true.  This results in the commlog entry having mode=mail instead of none.</summary>
		//public bool IsBill;
		///<summary></summary>
		public string Note;
		//<summary></summary>
		//public bool EmailOnClose;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label3;
		private OpenDental.ODtextBox textNote;
		//private Patient PatCur;
		private OpenDental.UI.Button buttonFuchs1;
		private OpenDental.UI.Button buttonFuchs2;
		private OpenDental.UI.Button buttonFuchs3;
		private GroupBox groupFuchs;
		private OpenDental.UI.Button butToday;
		private OpenDental.UI.Button butDatesAll;
		private OpenDental.UI.Button but90days;
		private OpenDental.UI.Button but45days;
		private ValidDate textDateEnd;
		private ValidDate textDateStart;
		private Label labelEndDate;
		private Label labelStartDate;
		private ODtextBox oDtextBox1;
		private Label label1;
		private Label label2;
		private ListBox listMode;
		private CheckBox checkSinglePatient;
		private CheckBox checkIntermingled;
		//private Family FamCur;

		///<summary></summary>
		public FormStatementOptions()//Patient patCur,Family famCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//PatCur=patCur;
			//FamCur=famCur;
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStatementOptions));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.checkHidePayment = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this.textNote = new OpenDental.ODtextBox();
			this.buttonFuchs1 = new OpenDental.UI.Button();
			this.buttonFuchs2 = new OpenDental.UI.Button();
			this.buttonFuchs3 = new OpenDental.UI.Button();
			this.groupFuchs = new System.Windows.Forms.GroupBox();
			this.butToday = new OpenDental.UI.Button();
			this.butDatesAll = new OpenDental.UI.Button();
			this.but90days = new OpenDental.UI.Button();
			this.but45days = new OpenDental.UI.Button();
			this.textDateEnd = new OpenDental.ValidDate();
			this.textDateStart = new OpenDental.ValidDate();
			this.labelEndDate = new System.Windows.Forms.Label();
			this.labelStartDate = new System.Windows.Forms.Label();
			this.oDtextBox1 = new OpenDental.ODtextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.listMode = new System.Windows.Forms.ListBox();
			this.checkIntermingled = new System.Windows.Forms.CheckBox();
			this.checkSinglePatient = new System.Windows.Forms.CheckBox();
			this.groupFuchs.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(596,442);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
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
			this.butOK.Location = new System.Drawing.Point(596,402);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// checkHidePayment
			// 
			this.checkHidePayment.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHidePayment.Location = new System.Drawing.Point(29,99);
			this.checkHidePayment.Name = "checkHidePayment";
			this.checkHidePayment.Size = new System.Drawing.Size(215,20);
			this.checkHidePayment.TabIndex = 11;
			this.checkHidePayment.Text = "Hide payment options";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(30,206);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(195,17);
			this.label3.TabIndex = 13;
			this.label3.Text = "Note";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(29,225);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Statement;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(518,147);
			this.textNote.TabIndex = 14;
			// 
			// buttonFuchs1
			// 
			this.buttonFuchs1.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonFuchs1.Autosize = true;
			this.buttonFuchs1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonFuchs1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonFuchs1.CornerRadius = 4F;
			this.buttonFuchs1.Location = new System.Drawing.Point(6,19);
			this.buttonFuchs1.Name = "buttonFuchs1";
			this.buttonFuchs1.Size = new System.Drawing.Size(86,21);
			this.buttonFuchs1.TabIndex = 18;
			this.buttonFuchs1.Text = "Ins. Not Paid";
			this.buttonFuchs1.Visible = false;
			this.buttonFuchs1.Click += new System.EventHandler(this.buttonFuchs1_Click);
			// 
			// buttonFuchs2
			// 
			this.buttonFuchs2.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonFuchs2.Autosize = true;
			this.buttonFuchs2.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonFuchs2.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonFuchs2.CornerRadius = 4F;
			this.buttonFuchs2.Location = new System.Drawing.Point(95,19);
			this.buttonFuchs2.Name = "buttonFuchs2";
			this.buttonFuchs2.Size = new System.Drawing.Size(102,21);
			this.buttonFuchs2.TabIndex = 19;
			this.buttonFuchs2.Text = "Ins. Paid, Bal. Left";
			this.buttonFuchs2.Visible = false;
			this.buttonFuchs2.Click += new System.EventHandler(this.buttonFuchs2_Click);
			// 
			// buttonFuchs3
			// 
			this.buttonFuchs3.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonFuchs3.Autosize = true;
			this.buttonFuchs3.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonFuchs3.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonFuchs3.CornerRadius = 4F;
			this.buttonFuchs3.Location = new System.Drawing.Point(199,19);
			this.buttonFuchs3.Name = "buttonFuchs3";
			this.buttonFuchs3.Size = new System.Drawing.Size(112,21);
			this.buttonFuchs3.TabIndex = 20;
			this.buttonFuchs3.Text = "Ins. Paid, Credit Left";
			this.buttonFuchs3.Visible = false;
			this.buttonFuchs3.Click += new System.EventHandler(this.buttonFuchs3_Click);
			// 
			// groupFuchs
			// 
			this.groupFuchs.Controls.Add(this.buttonFuchs1);
			this.groupFuchs.Controls.Add(this.buttonFuchs3);
			this.groupFuchs.Controls.Add(this.buttonFuchs2);
			this.groupFuchs.Location = new System.Drawing.Point(138,173);
			this.groupFuchs.Name = "groupFuchs";
			this.groupFuchs.Size = new System.Drawing.Size(321,47);
			this.groupFuchs.TabIndex = 21;
			this.groupFuchs.TabStop = false;
			this.groupFuchs.Text = "Fuchs hidden options";
			this.groupFuchs.Visible = false;
			// 
			// butToday
			// 
			this.butToday.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butToday.Autosize = true;
			this.butToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butToday.CornerRadius = 4F;
			this.butToday.Location = new System.Drawing.Point(470,60);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(77,24);
			this.butToday.TabIndex = 229;
			this.butToday.Text = "Today";
			this.butToday.Click += new System.EventHandler(this.butToday_Click);
			// 
			// butDatesAll
			// 
			this.butDatesAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDatesAll.Autosize = true;
			this.butDatesAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDatesAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDatesAll.CornerRadius = 4F;
			this.butDatesAll.Location = new System.Drawing.Point(470,138);
			this.butDatesAll.Name = "butDatesAll";
			this.butDatesAll.Size = new System.Drawing.Size(77,24);
			this.butDatesAll.TabIndex = 228;
			this.butDatesAll.Text = "All Dates";
			this.butDatesAll.Click += new System.EventHandler(this.butDatesAll_Click);
			// 
			// but90days
			// 
			this.but90days.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but90days.Autosize = true;
			this.but90days.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but90days.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but90days.CornerRadius = 4F;
			this.but90days.Location = new System.Drawing.Point(470,112);
			this.but90days.Name = "but90days";
			this.but90days.Size = new System.Drawing.Size(77,24);
			this.but90days.TabIndex = 227;
			this.but90days.Text = "Last 90 Days";
			this.but90days.Click += new System.EventHandler(this.but90days_Click);
			// 
			// but45days
			// 
			this.but45days.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but45days.Autosize = true;
			this.but45days.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but45days.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but45days.CornerRadius = 4F;
			this.but45days.Location = new System.Drawing.Point(470,86);
			this.but45days.Name = "but45days";
			this.but45days.Size = new System.Drawing.Size(77,24);
			this.but45days.TabIndex = 226;
			this.but45days.Text = "Last 45 Days";
			this.but45days.Click += new System.EventHandler(this.but45days_Click);
			// 
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(470,37);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(77,20);
			this.textDateEnd.TabIndex = 224;
			// 
			// textDateStart
			// 
			this.textDateStart.BackColor = System.Drawing.SystemColors.Window;
			this.textDateStart.ForeColor = System.Drawing.SystemColors.WindowText;
			this.textDateStart.Location = new System.Drawing.Point(470,14);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77,20);
			this.textDateStart.TabIndex = 223;
			// 
			// labelEndDate
			// 
			this.labelEndDate.Location = new System.Drawing.Point(377,40);
			this.labelEndDate.Name = "labelEndDate";
			this.labelEndDate.Size = new System.Drawing.Size(91,14);
			this.labelEndDate.TabIndex = 222;
			this.labelEndDate.Text = "End Date";
			this.labelEndDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelStartDate
			// 
			this.labelStartDate.Location = new System.Drawing.Point(383,17);
			this.labelStartDate.Name = "labelStartDate";
			this.labelStartDate.Size = new System.Drawing.Size(84,14);
			this.labelStartDate.TabIndex = 221;
			this.labelStartDate.Text = "Start Date";
			this.labelStartDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// oDtextBox1
			// 
			this.oDtextBox1.AcceptsReturn = true;
			this.oDtextBox1.Location = new System.Drawing.Point(29,394);
			this.oDtextBox1.Multiline = true;
			this.oDtextBox1.Name = "oDtextBox1";
			this.oDtextBox1.QuickPasteType = OpenDentBusiness.QuickPasteType.Statement;
			this.oDtextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.oDtextBox1.Size = new System.Drawing.Size(518,74);
			this.oDtextBox1.TabIndex = 231;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(30,375);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(195,17);
			this.label1.TabIndex = 230;
			this.label1.Text = "Bold Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(28,14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84,14);
			this.label2.TabIndex = 232;
			this.label2.Text = "Mode";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listMode
			// 
			this.listMode.FormattingEnabled = true;
			this.listMode.Items.AddRange(new object[] {
            "Unsent",
            "Email",
            "Mail",
            "InPerson"});
			this.listMode.Location = new System.Drawing.Point(29,30);
			this.listMode.Name = "listMode";
			this.listMode.Size = new System.Drawing.Size(113,56);
			this.listMode.TabIndex = 233;
			// 
			// checkIntermingled
			// 
			this.checkIntermingled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIntermingled.Location = new System.Drawing.Point(29,149);
			this.checkIntermingled.Name = "checkIntermingled";
			this.checkIntermingled.Size = new System.Drawing.Size(215,20);
			this.checkIntermingled.TabIndex = 234;
			this.checkIntermingled.Text = "Intermingle family members";
			// 
			// checkSinglePatient
			// 
			this.checkSinglePatient.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSinglePatient.Location = new System.Drawing.Point(29,124);
			this.checkSinglePatient.Name = "checkSinglePatient";
			this.checkSinglePatient.Size = new System.Drawing.Size(215,20);
			this.checkSinglePatient.TabIndex = 235;
			this.checkSinglePatient.Text = "Single patient only";
			// 
			// FormStatementOptions
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(702,502);
			this.Controls.Add(this.checkSinglePatient);
			this.Controls.Add(this.checkIntermingled);
			this.Controls.Add(this.listMode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.oDtextBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butToday);
			this.Controls.Add(this.butDatesAll);
			this.Controls.Add(this.but90days);
			this.Controls.Add(this.but45days);
			this.Controls.Add(this.textDateEnd);
			this.Controls.Add(this.textDateStart);
			this.Controls.Add(this.labelEndDate);
			this.Controls.Add(this.labelStartDate);
			this.Controls.Add(this.groupFuchs);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkHidePayment);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormStatementOptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Statement";
			this.Load += new System.EventHandler(this.FormStatementOptions_Load);
			this.groupFuchs.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormStatementOptions_Load(object sender, System.EventArgs e) {
			/*if(FromDate.Year<1880){
				textDateFrom.Text="";
			}
			else{
				textDateFrom.Text=FromDate.ToShortDateString();
			}
			if(ToDate.Year>2100){
				textDateTo.Text="";
			}
			else{
				textDateTo.Text=ToDate.ToShortDateString();
			}*/
			//default to last 45 days if dates are blank
			/*if (textDateTo.Text=="" && textDateFrom.Text==""){
				textDateFrom.Text=DateTime.Today.AddDays(-45).ToShortDateString();
				textDateTo.Text=DateTime.Today.ToShortDateString();
			}*/
			if(PrefB.GetBool("FuchsOptionsOn")){
				//textDateFrom.Text=DateTime.Today.AddDays(-90).ToShortDateString();
				//textDateTo.Text=DateTime.Today.ToShortDateString();
				groupFuchs.Visible=true;
				buttonFuchs1.Visible=true;
				buttonFuchs2.Visible=true;
				buttonFuchs3.Visible=true;
			}
			//EmailOnClose=false;
			//checkSimpleStatement.Checked=(PrefB.GetBool("PrintSimpleStatements"));
		}

		private void butToday_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
		}

		private void but45days_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.AddDays(-45).ToShortDateString();
			textDateEnd.Text="";
		}

		private void but90days_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.AddDays(-90).ToShortDateString();
			textDateEnd.Text="";
		}

		private void butDatesAll_Click(object sender,EventArgs e) {
			textDateStart.Text="";
			textDateEnd.Text="";
		}

		/*private void butToday_Click(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
		}

		private void butWalkout_Click(object sender, System.EventArgs e) {
			//textDateFrom.Text=DateTime.Today.ToShortDateString();
			//textDateTo.Text=DateTime.Today.ToShortDateString();
			radioFamCur.Checked=true;
			checkIncludeClaims.Checked=false;
			checkSubtotals.Checked=false;
			checkHidePayment.Checked=true;
			checkNextAppt.Checked=true;
			checkMailing.Checked=false;
			SaveAndClose();
		}

		private void but30_Click(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.AddDays(-30).ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
		}

		private void but45_Click(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.AddDays(-45).ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			textDateFrom.Text="";
			textDateTo.Text="";//DateTime.Today.ToShortDateString();
		}
		private void but90_Click(object sender,EventArgs e) {
			textDateFrom.Text=DateTime.Today.AddDays(-90).ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();

		}

		private void butLastYear_Click(object sender,EventArgs e) {
			textDateFrom.Text="01/01/"+DateTime.Today.AddYears(-1).Year.ToString();
			textDateTo.Text="12/31/"+DateTime.Today.AddYears(-1).Year.ToString();

		}

		private void butYTD_Click(object sender,EventArgs e) {
			textDateFrom.Text="01/01/"+DateTime.Today.Year.ToString();
			textDateTo.Text=DateTime.Today.ToShortDateString();

		}*/

		private void buttonFuchs1_Click(object sender,EventArgs e) {
			textNote.Text="Your insurance has not yet paid for your claims, they are still pending. This is to keep you informed of the status of your account. Thank You "+textNote.Text;
		}

		private void buttonFuchs2_Click(object sender,EventArgs e) {
			textNote.Text="Your insurance paid and the remaining balance is yours. Thank You! "+textNote.Text;

		}

		private void buttonFuchs3_Click(object sender,EventArgs e) {
			textNote.Text="This credit is on your account. We look forward to seeing you on your next apptointment! "+textNote.Text;

		}

		//private void buttonEmail_Click(object sender,EventArgs e) {
		//	EmailOnClose=true;
		//	SaveAndClose();
		//}

		private void butOK_Click(object sender, System.EventArgs e) {
			SaveAndClose();
		}

		private void SaveAndClose(){
			/*if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(textDateFrom.Text=="")
				FromDate=DateTime.MinValue;
			else
				FromDate=PIn.PDate(textDateFrom.Text);
			if(textDateTo.Text=="")
				ToDate=DateTime.MaxValue;//Today;
			else
				ToDate=PIn.PDate(textDateTo.Text);*/
			//IncludeClaims=checkIncludeClaims.Checked;
			//SubtotalsOnly=checkSubtotals.Checked;
			HidePayment=checkHidePayment.Checked;
			//NextAppt=checkNextAppt.Checked;
			Note=textNote.Text;
			//SimpleStatement=checkSimpleStatement.Checked;
			//IsBill=checkMailing.Checked;
			/*if(radioFamAll.Checked){
				PatNums=new int[FamCur.List.Length];
				for(int i=0;i<FamCur.List.Length;i++){
					PatNums[i]=FamCur.List[i].PatNum;
				}
			}
			else{
				PatNums=new int[1];
				PatNums[0]=PatCur.PatNum;
			}*/
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

		

		



	}
}





















