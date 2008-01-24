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
		private System.Windows.Forms.Label label1;
		private OpenDental.ValidDate textDateFrom;
		private OpenDental.ValidDate textDateTo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioFamAll;
		private System.Windows.Forms.RadioButton radioFamCur;
		private OpenDental.UI.Button butAll;
		private OpenDental.UI.Button but30;
		private OpenDental.UI.Button but45;
		private System.Windows.Forms.CheckBox checkIncludeClaims;
		private OpenDental.UI.Button butToday;
		private System.Windows.Forms.CheckBox checkSubtotals;
		private OpenDental.UI.Button butWalkout;
		private System.Windows.Forms.CheckBox checkNextAppt;
		private System.Windows.Forms.CheckBox checkHidePayment;
		///<summary></summary>
		public int[] PatNums;
		///<summary>Dates are the only parameters that get passed to this form. But all the parameters are sent back out from this form.</summary>
		public DateTime FromDate;
		///<summary></summary>
		public DateTime ToDate;
		///<summary></summary>
		public bool IncludeClaims;
		///<summary></summary>
		public bool SubtotalsOnly;
		///<summary></summary>
		public bool HidePayment;
		///<summary></summary>
		public bool NextAppt;
		//<summary></summary>
		//public bool SimpleStatement;
		///<summary>When user checks box in this form for Mailing, then this gets set to true.  This results in the commlog entry having mode=mail instead of none.</summary>
		public bool IsBill;
		///<summary></summary>
		public string Note;
		///<summary></summary>
		public bool EmailOnClose;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label3;
		private OpenDental.ODtextBox textNote;
		private Patient PatCur;
		private CheckBox checkMailing;
		private OpenDental.UI.Button but90;
		private OpenDental.UI.Button butYTD;
		private OpenDental.UI.Button butLastYear;
		private Label label4;
		private OpenDental.UI.Button buttonEmail;
		private OpenDental.UI.Button buttonFuchs1;
		private OpenDental.UI.Button buttonFuchs2;
		private OpenDental.UI.Button buttonFuchs3;
		private Family FamCur;

		///<summary></summary>
		public FormStatementOptions(Patient patCur,Family famCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			PatCur=patCur;
			FamCur=famCur;
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
			this.label1 = new System.Windows.Forms.Label();
			this.textDateFrom = new OpenDental.ValidDate();
			this.textDateTo = new OpenDental.ValidDate();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioFamCur = new System.Windows.Forms.RadioButton();
			this.radioFamAll = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butLastYear = new OpenDental.UI.Button();
			this.butYTD = new OpenDental.UI.Button();
			this.but90 = new OpenDental.UI.Button();
			this.butToday = new OpenDental.UI.Button();
			this.but45 = new OpenDental.UI.Button();
			this.but30 = new OpenDental.UI.Button();
			this.butAll = new OpenDental.UI.Button();
			this.butWalkout = new OpenDental.UI.Button();
			this.checkIncludeClaims = new System.Windows.Forms.CheckBox();
			this.checkSubtotals = new System.Windows.Forms.CheckBox();
			this.checkNextAppt = new System.Windows.Forms.CheckBox();
			this.checkHidePayment = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this.textNote = new OpenDental.ODtextBox();
			this.checkMailing = new System.Windows.Forms.CheckBox();
			this.buttonEmail = new OpenDental.UI.Button();
			this.buttonFuchs1 = new OpenDental.UI.Button();
			this.buttonFuchs2 = new OpenDental.UI.Button();
			this.buttonFuchs3 = new OpenDental.UI.Button();
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
			this.butCancel.Location = new System.Drawing.Point(590,444);
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
			this.butOK.Location = new System.Drawing.Point(590,404);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(30,26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,17);
			this.label1.TabIndex = 2;
			this.label1.Text = "From Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(137,27);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(84,20);
			this.textDateFrom.TabIndex = 3;
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(137,55);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(85,20);
			this.textDateTo.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(30,54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,17);
			this.label2.TabIndex = 4;
			this.label2.Text = "To Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioFamCur);
			this.groupBox1.Controls.Add(this.radioFamAll);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(343,151);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200,61);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Family Members";
			// 
			// radioFamCur
			// 
			this.radioFamCur.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioFamCur.Location = new System.Drawing.Point(17,34);
			this.radioFamCur.Name = "radioFamCur";
			this.radioFamCur.Size = new System.Drawing.Size(144,19);
			this.radioFamCur.TabIndex = 1;
			this.radioFamCur.Text = "Current Patient";
			// 
			// radioFamAll
			// 
			this.radioFamAll.Checked = true;
			this.radioFamAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioFamAll.Location = new System.Drawing.Point(17,16);
			this.radioFamAll.Name = "radioFamAll";
			this.radioFamAll.Size = new System.Drawing.Size(144,19);
			this.radioFamAll.TabIndex = 0;
			this.radioFamAll.TabStop = true;
			this.radioFamAll.Text = "All";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.butLastYear);
			this.groupBox2.Controls.Add(this.butYTD);
			this.groupBox2.Controls.Add(this.but90);
			this.groupBox2.Controls.Add(this.butToday);
			this.groupBox2.Controls.Add(this.but45);
			this.groupBox2.Controls.Add(this.but30);
			this.groupBox2.Controls.Add(this.butAll);
			this.groupBox2.Controls.Add(this.textDateFrom);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.textDateTo);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(26,8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(517,127);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Date Range";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(124,78);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(110,20);
			this.label4.TabIndex = 13;
			this.label4.Text = "(Blank means All)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// butLastYear
			// 
			this.butLastYear.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLastYear.Autosize = true;
			this.butLastYear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLastYear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLastYear.CornerRadius = 4F;
			this.butLastYear.Location = new System.Drawing.Point(401,39);
			this.butLastYear.Name = "butLastYear";
			this.butLastYear.Size = new System.Drawing.Size(96,23);
			this.butLastYear.TabIndex = 12;
			this.butLastYear.Text = "Last Year";
			this.butLastYear.Click += new System.EventHandler(this.butLastYear_Click);
			// 
			// butYTD
			// 
			this.butYTD.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butYTD.Autosize = true;
			this.butYTD.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butYTD.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butYTD.CornerRadius = 4F;
			this.butYTD.Location = new System.Drawing.Point(401,13);
			this.butYTD.Name = "butYTD";
			this.butYTD.Size = new System.Drawing.Size(96,23);
			this.butYTD.TabIndex = 11;
			this.butYTD.Text = "Year To Date";
			this.butYTD.Click += new System.EventHandler(this.butYTD_Click);
			// 
			// but90
			// 
			this.but90.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but90.Autosize = true;
			this.but90.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but90.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but90.CornerRadius = 4F;
			this.but90.Location = new System.Drawing.Point(285,91);
			this.but90.Name = "but90";
			this.but90.Size = new System.Drawing.Size(96,23);
			this.but90.TabIndex = 10;
			this.but90.Text = "Last 90 Days";
			this.but90.Click += new System.EventHandler(this.but90_Click);
			// 
			// butToday
			// 
			this.butToday.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butToday.Autosize = true;
			this.butToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butToday.CornerRadius = 4F;
			this.butToday.Location = new System.Drawing.Point(285,13);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(96,23);
			this.butToday.TabIndex = 9;
			this.butToday.Text = "Today Only";
			this.butToday.Click += new System.EventHandler(this.butToday_Click);
			// 
			// but45
			// 
			this.but45.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but45.Autosize = true;
			this.but45.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but45.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but45.CornerRadius = 4F;
			this.but45.Location = new System.Drawing.Point(285,65);
			this.but45.Name = "but45";
			this.but45.Size = new System.Drawing.Size(96,23);
			this.but45.TabIndex = 8;
			this.but45.Text = "Last 45 Days";
			this.but45.Click += new System.EventHandler(this.but45_Click);
			// 
			// but30
			// 
			this.but30.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but30.Autosize = true;
			this.but30.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but30.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but30.CornerRadius = 4F;
			this.but30.Location = new System.Drawing.Point(285,39);
			this.but30.Name = "but30";
			this.but30.Size = new System.Drawing.Size(96,23);
			this.but30.TabIndex = 7;
			this.but30.Text = "Last 30 Days";
			this.but30.Click += new System.EventHandler(this.but30_Click);
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(401,91);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(96,23);
			this.butAll.TabIndex = 6;
			this.butAll.Text = "All Dates";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// butWalkout
			// 
			this.butWalkout.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butWalkout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butWalkout.Autosize = true;
			this.butWalkout.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butWalkout.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butWalkout.CornerRadius = 4F;
			this.butWalkout.Location = new System.Drawing.Point(569,304);
			this.butWalkout.Name = "butWalkout";
			this.butWalkout.Size = new System.Drawing.Size(96,26);
			this.butWalkout.TabIndex = 10;
			this.butWalkout.Text = "Walkout";
			this.toolTip1.SetToolTip(this.butWalkout,"Automatically sets the options and prints with a single click");
			this.butWalkout.Click += new System.EventHandler(this.butWalkout_Click);
			// 
			// checkIncludeClaims
			// 
			this.checkIncludeClaims.Checked = true;
			this.checkIncludeClaims.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkIncludeClaims.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIncludeClaims.Location = new System.Drawing.Point(26,150);
			this.checkIncludeClaims.Name = "checkIncludeClaims";
			this.checkIncludeClaims.Size = new System.Drawing.Size(257,20);
			this.checkIncludeClaims.TabIndex = 8;
			this.checkIncludeClaims.Text = "Include Uncleared Claims";
			// 
			// checkSubtotals
			// 
			this.checkSubtotals.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSubtotals.Location = new System.Drawing.Point(26,171);
			this.checkSubtotals.Name = "checkSubtotals";
			this.checkSubtotals.Size = new System.Drawing.Size(257,20);
			this.checkSubtotals.TabIndex = 9;
			this.checkSubtotals.Text = "Subtotals Only";
			// 
			// checkNextAppt
			// 
			this.checkNextAppt.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNextAppt.Location = new System.Drawing.Point(26,213);
			this.checkNextAppt.Name = "checkNextAppt";
			this.checkNextAppt.Size = new System.Drawing.Size(257,20);
			this.checkNextAppt.TabIndex = 10;
			this.checkNextAppt.Text = "Next Appointment";
			// 
			// checkHidePayment
			// 
			this.checkHidePayment.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHidePayment.Location = new System.Drawing.Point(26,192);
			this.checkHidePayment.Name = "checkHidePayment";
			this.checkHidePayment.Size = new System.Drawing.Size(257,20);
			this.checkHidePayment.TabIndex = 11;
			this.checkHidePayment.Text = "Hide Payment Options";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(26,304);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(195,17);
			this.label3.TabIndex = 13;
			this.label3.Text = "Note";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(25,323);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Statement;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(518,147);
			this.textNote.TabIndex = 14;
			// 
			// checkMailing
			// 
			this.checkMailing.Checked = true;
			this.checkMailing.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkMailing.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkMailing.Location = new System.Drawing.Point(590,378);
			this.checkMailing.Name = "checkMailing";
			this.checkMailing.Size = new System.Drawing.Size(93,20);
			this.checkMailing.TabIndex = 16;
			this.checkMailing.Text = "Mailing This";
			// 
			// buttonEmail
			// 
			this.buttonEmail.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEmail.Autosize = true;
			this.buttonEmail.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonEmail.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonEmail.CornerRadius = 4F;
			this.buttonEmail.Location = new System.Drawing.Point(569,336);
			this.buttonEmail.Name = "buttonEmail";
			this.buttonEmail.Size = new System.Drawing.Size(96,26);
			this.buttonEmail.TabIndex = 17;
			this.buttonEmail.Text = "E-Mail This";
			this.buttonEmail.Click += new System.EventHandler(this.buttonEmail_Click);
			// 
			// buttonFuchs1
			// 
			this.buttonFuchs1.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonFuchs1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFuchs1.Autosize = true;
			this.buttonFuchs1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonFuchs1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonFuchs1.CornerRadius = 4F;
			this.buttonFuchs1.Location = new System.Drawing.Point(59,296);
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
			this.buttonFuchs2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFuchs2.Autosize = true;
			this.buttonFuchs2.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonFuchs2.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonFuchs2.CornerRadius = 4F;
			this.buttonFuchs2.Location = new System.Drawing.Point(146,296);
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
			this.buttonFuchs3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFuchs3.Autosize = true;
			this.buttonFuchs3.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonFuchs3.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonFuchs3.CornerRadius = 4F;
			this.buttonFuchs3.Location = new System.Drawing.Point(249,296);
			this.buttonFuchs3.Name = "buttonFuchs3";
			this.buttonFuchs3.Size = new System.Drawing.Size(112,21);
			this.buttonFuchs3.TabIndex = 20;
			this.buttonFuchs3.Text = "Ins. Paid, Credit Left";
			this.buttonFuchs3.Visible = false;
			this.buttonFuchs3.Click += new System.EventHandler(this.buttonFuchs3_Click);
			// 
			// FormStatementOptions
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(686,493);
			this.Controls.Add(this.buttonFuchs3);
			this.Controls.Add(this.buttonFuchs2);
			this.Controls.Add(this.buttonFuchs1);
			this.Controls.Add(this.buttonEmail);
			this.Controls.Add(this.checkMailing);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkHidePayment);
			this.Controls.Add(this.checkNextAppt);
			this.Controls.Add(this.checkSubtotals);
			this.Controls.Add(this.checkIncludeClaims);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butWalkout);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormStatementOptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Statement Options";
			this.Load += new System.EventHandler(this.FormStatementOptions_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormStatementOptions_Load(object sender, System.EventArgs e) {
			if(FromDate.Year<1880){
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
			}
			//default to last 45 days if dates are blank
			/*if (textDateTo.Text=="" && textDateFrom.Text==""){
				textDateFrom.Text=DateTime.Today.AddDays(-45).ToShortDateString();
				textDateTo.Text=DateTime.Today.ToShortDateString();
			}*/
			if(PrefB.GetBool("FuchsOptionsOn")){
				textDateFrom.Text=DateTime.Today.AddDays(-90).ToShortDateString();
				textDateTo.Text=DateTime.Today.ToShortDateString();
				buttonFuchs1.Visible=true;
				buttonFuchs2.Visible=true;
				buttonFuchs3.Visible=true;
			}
			EmailOnClose=false;
			//checkSimpleStatement.Checked=(PrefB.GetBool("PrintSimpleStatements"));
		}

		private void butToday_Click(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
		}

		private void butWalkout_Click(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
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
		private void buttonEmail_Click(object sender,EventArgs e) {
			EmailOnClose=true;
			SaveAndClose();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			SaveAndClose();
		}

		private void SaveAndClose(){
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
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
				ToDate=PIn.PDate(textDateTo.Text);
			IncludeClaims=checkIncludeClaims.Checked;
			SubtotalsOnly=checkSubtotals.Checked;
			HidePayment=checkHidePayment.Checked;
			NextAppt=checkNextAppt.Checked;
			Note=textNote.Text;
			//SimpleStatement=checkSimpleStatement.Checked;
			IsBill=checkMailing.Checked;
			if(radioFamAll.Checked){
				PatNums=new int[FamCur.List.Length];
				for(int i=0;i<FamCur.List.Length;i++){
					PatNums[i]=FamCur.List[i].PatNum;
				}
			}
			else{
				PatNums=new int[1];
				PatNums[0]=PatCur.PatNum;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void checkSimpleStatement_CheckedChanged(object sender,EventArgs e) {

		}



	}
}





















