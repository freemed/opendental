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
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label3;
		private OpenDental.ODtextBox textNote;
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
		private ODtextBox textNoteBold;
		private Label label1;
		private Label label2;
		private ListBox listMode;
		private CheckBox checkSinglePatient;
		private CheckBox checkIntermingled;
		private GroupBox groupDateRange;
		private ValidDate textDate;
		private Label label4;
		public Statement StmtCur;

		///<summary></summary>
		public FormStatementOptions()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			this.textNoteBold = new OpenDental.ODtextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.listMode = new System.Windows.Forms.ListBox();
			this.checkIntermingled = new System.Windows.Forms.CheckBox();
			this.checkSinglePatient = new System.Windows.Forms.CheckBox();
			this.groupDateRange = new System.Windows.Forms.GroupBox();
			this.textDate = new OpenDental.ValidDate();
			this.label4 = new System.Windows.Forms.Label();
			this.groupFuchs.SuspendLayout();
			this.groupDateRange.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(639,400);
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
			this.butOK.Location = new System.Drawing.Point(639,360);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// checkHidePayment
			// 
			this.checkHidePayment.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkHidePayment.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHidePayment.Location = new System.Drawing.Point(-2,107);
			this.checkHidePayment.Name = "checkHidePayment";
			this.checkHidePayment.Size = new System.Drawing.Size(162,20);
			this.checkHidePayment.TabIndex = 11;
			this.checkHidePayment.Text = "Hide payment options";
			this.checkHidePayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(55,200);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90,17);
			this.label3.TabIndex = 13;
			this.label3.Text = "Note";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(146,200);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Statement;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(462,147);
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
			this.buttonFuchs2.Location = new System.Drawing.Point(6,46);
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
			this.buttonFuchs3.Location = new System.Drawing.Point(6,73);
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
			this.groupFuchs.Location = new System.Drawing.Point(297,81);
			this.groupFuchs.Name = "groupFuchs";
			this.groupFuchs.Size = new System.Drawing.Size(129,105);
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
			this.butToday.Location = new System.Drawing.Point(75,64);
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
			this.butDatesAll.Location = new System.Drawing.Point(75,142);
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
			this.but90days.Location = new System.Drawing.Point(75,116);
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
			this.but45days.Location = new System.Drawing.Point(75,90);
			this.but45days.Name = "but45days";
			this.but45days.Size = new System.Drawing.Size(77,24);
			this.but45days.TabIndex = 226;
			this.but45days.Text = "Last 45 Days";
			this.but45days.Click += new System.EventHandler(this.but45days_Click);
			// 
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(75,41);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(77,20);
			this.textDateEnd.TabIndex = 224;
			// 
			// textDateStart
			// 
			this.textDateStart.BackColor = System.Drawing.SystemColors.Window;
			this.textDateStart.ForeColor = System.Drawing.SystemColors.WindowText;
			this.textDateStart.Location = new System.Drawing.Point(75,18);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77,20);
			this.textDateStart.TabIndex = 223;
			// 
			// labelEndDate
			// 
			this.labelEndDate.Location = new System.Drawing.Point(3,44);
			this.labelEndDate.Name = "labelEndDate";
			this.labelEndDate.Size = new System.Drawing.Size(69,14);
			this.labelEndDate.TabIndex = 222;
			this.labelEndDate.Text = "End Date";
			this.labelEndDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelStartDate
			// 
			this.labelStartDate.Location = new System.Drawing.Point(3,21);
			this.labelStartDate.Name = "labelStartDate";
			this.labelStartDate.Size = new System.Drawing.Size(69,14);
			this.labelStartDate.TabIndex = 221;
			this.labelStartDate.Text = "Start Date";
			this.labelStartDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textNoteBold
			// 
			this.textNoteBold.AcceptsReturn = true;
			this.textNoteBold.Location = new System.Drawing.Point(146,353);
			this.textNoteBold.Multiline = true;
			this.textNoteBold.Name = "textNoteBold";
			this.textNoteBold.QuickPasteType = OpenDentBusiness.QuickPasteType.Statement;
			this.textNoteBold.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNoteBold.Size = new System.Drawing.Size(462,74);
			this.textNoteBold.TabIndex = 231;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(35,354);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(110,17);
			this.label1.TabIndex = 230;
			this.label1.Text = "Bold Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(64,40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81,14);
			this.label2.TabIndex = 232;
			this.label2.Text = "Mode";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listMode
			// 
			this.listMode.FormattingEnabled = true;
			this.listMode.Items.AddRange(new object[] {
            "Unsent",
            "Email",
            "Mail",
            "InPerson"});
			this.listMode.Location = new System.Drawing.Point(146,40);
			this.listMode.Name = "listMode";
			this.listMode.Size = new System.Drawing.Size(113,56);
			this.listMode.TabIndex = 233;
			// 
			// checkIntermingled
			// 
			this.checkIntermingled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIntermingled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIntermingled.Location = new System.Drawing.Point(-2,157);
			this.checkIntermingled.Name = "checkIntermingled";
			this.checkIntermingled.Size = new System.Drawing.Size(162,20);
			this.checkIntermingled.TabIndex = 234;
			this.checkIntermingled.Text = "Intermingle family members";
			this.checkIntermingled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkSinglePatient
			// 
			this.checkSinglePatient.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkSinglePatient.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSinglePatient.Location = new System.Drawing.Point(-2,132);
			this.checkSinglePatient.Name = "checkSinglePatient";
			this.checkSinglePatient.Size = new System.Drawing.Size(162,20);
			this.checkSinglePatient.TabIndex = 235;
			this.checkSinglePatient.Text = "Single patient only";
			this.checkSinglePatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupDateRange
			// 
			this.groupDateRange.Controls.Add(this.textDateStart);
			this.groupDateRange.Controls.Add(this.labelStartDate);
			this.groupDateRange.Controls.Add(this.labelEndDate);
			this.groupDateRange.Controls.Add(this.textDateEnd);
			this.groupDateRange.Controls.Add(this.but45days);
			this.groupDateRange.Controls.Add(this.but90days);
			this.groupDateRange.Controls.Add(this.butDatesAll);
			this.groupDateRange.Controls.Add(this.butToday);
			this.groupDateRange.Location = new System.Drawing.Point(446,12);
			this.groupDateRange.Name = "groupDateRange";
			this.groupDateRange.Size = new System.Drawing.Size(162,174);
			this.groupDateRange.TabIndex = 236;
			this.groupDateRange.TabStop = false;
			this.groupDateRange.Text = "Date Range";
			// 
			// textDate
			// 
			this.textDate.BackColor = System.Drawing.SystemColors.Window;
			this.textDate.ForeColor = System.Drawing.SystemColors.WindowText;
			this.textDate.Location = new System.Drawing.Point(146,10);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(77,20);
			this.textDate.TabIndex = 238;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(67,13);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77,14);
			this.label4.TabIndex = 237;
			this.label4.Text = "Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormStatementOptions
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(745,460);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.groupDateRange);
			this.Controls.Add(this.checkSinglePatient);
			this.Controls.Add(this.checkIntermingled);
			this.Controls.Add(this.listMode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textNoteBold);
			this.Controls.Add(this.label1);
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
			this.groupDateRange.ResumeLayout(false);
			this.groupDateRange.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormStatementOptions_Load(object sender, System.EventArgs e) {
			textDate.Text=StmtCur.DateSent.ToShortDateString();
			listMode.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(StatementMode)).Length;i++){
				listMode.Items.Add(Lan.g("enumStatementMode",Enum.GetNames(typeof(StatementMode))[i]));
				if((int)StmtCur.Mode_==i){
					listMode.SelectedIndex=i;
				}
			}
			checkHidePayment.Checked=StmtCur.HidePayment;
			checkSinglePatient.Checked=StmtCur.SinglePatient;
			checkIntermingled.Checked=StmtCur.Intermingled;
			if(StmtCur.DateRangeFrom.Year>1880){
				textDateStart.Text=StmtCur.DateRangeFrom.ToShortDateString();
			}
			if(StmtCur.DateRangeTo.Year<2100){
				textDateEnd.Text=StmtCur.DateRangeTo.ToShortDateString();
			}
			if(PrefB.GetBool("FuchsOptionsOn")){
				//textDateFrom.Text=DateTime.Today.AddDays(-90).ToShortDateString();
				//textDateTo.Text=DateTime.Today.ToShortDateString();
				groupFuchs.Visible=true;
				buttonFuchs1.Visible=true;
				buttonFuchs2.Visible=true;
				buttonFuchs3.Visible=true;
			}
			textNote.Text=StmtCur.Note;
			textNoteBold.Text=StmtCur.NoteBold;
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

		private void buttonFuchs1_Click(object sender,EventArgs e) {
			textNote.Text="Your insurance has not yet paid for your claims, they are still pending. This is to keep you informed of the status of your account. Thank You "+textNote.Text;
		}

		private void buttonFuchs2_Click(object sender,EventArgs e) {
			textNote.Text="Your insurance paid and the remaining balance is yours. Thank You! "+textNote.Text;
		}

		private void buttonFuchs3_Click(object sender,EventArgs e) {
			textNote.Text="This credit is on your account. We look forward to seeing you on your next apptointment! "+textNote.Text;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			SaveAndClose();
		}

		private void SaveAndClose(){
			if(  textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateEnd.errorProvider1.GetError(textDateEnd)!=""
				|| textDate.errorProvider1.GetError(textDate)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textDate.Text==""){
				MsgBox.Show(this,"Please enter a date.");
				return;
			}
			StmtCur.DateSent=PIn.PDate(textDate.Text);
			StmtCur.Mode_=(StatementMode)listMode.SelectedIndex;
			StmtCur.HidePayment=checkHidePayment.Checked;
			StmtCur.SinglePatient=checkSinglePatient.Checked;
			StmtCur.Intermingled=checkIntermingled.Checked;
			StmtCur.DateRangeFrom=PIn.PDate(textDateStart.Text);//handles blank
			if(textDateEnd.Text==""){
				StmtCur.DateRangeTo=new DateTime(2100,1,1);//max val
			}
			else{
				StmtCur.DateRangeTo=PIn.PDate(textDateEnd.Text);
			}
			StmtCur.Note=textNote.Text;
			StmtCur.NoteBold=textNoteBold.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

		

		



	}
}





















