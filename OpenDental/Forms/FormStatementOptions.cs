using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using CodeBase;
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
		private Label labelEndDate;
		private Label labelStartDate;
		private ODtextBox textNoteBold;
		private Label label1;
		private Label label2;
		private ListBox listMode;
		private CheckBox checkSinglePatient;
		private CheckBox checkIntermingled;
		private GroupBox groupDateRange;
		private Label label4;
		private CheckBox checkIsSent;
		public Statement StmtCur;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butPrint;
		private OpenDental.UI.Button butEmail;
		private OpenDental.UI.Button butPreview;
		private bool initiallySent;
		private TextBox textDateEnd;
		private TextBox textDateStart;
		private TextBox textDate;
		///<summary>This will be null for ordinary edits.  But sometimes this window is used to edit bulk statements.  In that case, this list contains the statements being edited.  Must contain at least one item.</summary>
		public List<Statement> StmtList;

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
			this.labelEndDate = new System.Windows.Forms.Label();
			this.labelStartDate = new System.Windows.Forms.Label();
			this.textNoteBold = new OpenDental.ODtextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.listMode = new System.Windows.Forms.ListBox();
			this.checkIntermingled = new System.Windows.Forms.CheckBox();
			this.checkSinglePatient = new System.Windows.Forms.CheckBox();
			this.groupDateRange = new System.Windows.Forms.GroupBox();
			this.textDateEnd = new System.Windows.Forms.TextBox();
			this.textDateStart = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.checkIsSent = new System.Windows.Forms.CheckBox();
			this.butDelete = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.butEmail = new OpenDental.UI.Button();
			this.butPreview = new OpenDental.UI.Button();
			this.textDate = new System.Windows.Forms.TextBox();
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
			this.butCancel.Location = new System.Drawing.Point(606,453);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
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
			this.butOK.Location = new System.Drawing.Point(514,453);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// checkHidePayment
			// 
			this.checkHidePayment.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkHidePayment.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHidePayment.Location = new System.Drawing.Point(-2,121);
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
			this.buttonFuchs1.Size = new System.Drawing.Size(86,24);
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
			this.buttonFuchs2.Size = new System.Drawing.Size(103,24);
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
			this.buttonFuchs3.Size = new System.Drawing.Size(117,24);
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
			this.textNoteBold.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textNoteBold.ForeColor = System.Drawing.Color.DarkRed;
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
			this.label2.Location = new System.Drawing.Point(64,62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81,14);
			this.label2.TabIndex = 232;
			this.label2.Text = "Mode";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listMode
			// 
			this.listMode.FormattingEnabled = true;
			this.listMode.Location = new System.Drawing.Point(146,60);
			this.listMode.Name = "listMode";
			this.listMode.Size = new System.Drawing.Size(113,56);
			this.listMode.TabIndex = 233;
			// 
			// checkIntermingled
			// 
			this.checkIntermingled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIntermingled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIntermingled.Location = new System.Drawing.Point(-2,165);
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
			this.checkSinglePatient.Location = new System.Drawing.Point(-2,143);
			this.checkSinglePatient.Name = "checkSinglePatient";
			this.checkSinglePatient.Size = new System.Drawing.Size(162,20);
			this.checkSinglePatient.TabIndex = 235;
			this.checkSinglePatient.Text = "Single patient only";
			this.checkSinglePatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupDateRange
			// 
			this.groupDateRange.Controls.Add(this.textDateEnd);
			this.groupDateRange.Controls.Add(this.labelStartDate);
			this.groupDateRange.Controls.Add(this.textDateStart);
			this.groupDateRange.Controls.Add(this.labelEndDate);
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
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(75,41);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(77,20);
			this.textDateEnd.TabIndex = 246;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(75,18);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77,20);
			this.textDateStart.TabIndex = 245;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(67,17);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77,14);
			this.label4.TabIndex = 237;
			this.label4.Text = "Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIsSent
			// 
			this.checkIsSent.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsSent.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsSent.Location = new System.Drawing.Point(1,39);
			this.checkIsSent.Name = "checkIsSent";
			this.checkIsSent.Size = new System.Drawing.Size(159,18);
			this.checkIsSent.TabIndex = 239;
			this.checkIsSent.Text = "Sent";
			this.checkIsSent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsSent.Click += new System.EventHandler(this.checkIsSent_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(40,453);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(79,24);
			this.butDelete.TabIndex = 240;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(195,453);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(79,24);
			this.butPrint.TabIndex = 241;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butEmail
			// 
			this.butEmail.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butEmail.Autosize = true;
			this.butEmail.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEmail.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEmail.CornerRadius = 4F;
			this.butEmail.Image = global::OpenDental.Properties.Resources.email1;
			this.butEmail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEmail.Location = new System.Drawing.Point(280,453);
			this.butEmail.Name = "butEmail";
			this.butEmail.Size = new System.Drawing.Size(79,24);
			this.butEmail.TabIndex = 242;
			this.butEmail.Text = "E-mail";
			this.butEmail.Click += new System.EventHandler(this.butEmail_Click);
			// 
			// butPreview
			// 
			this.butPreview.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPreview.Autosize = true;
			this.butPreview.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPreview.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPreview.CornerRadius = 4F;
			this.butPreview.Image = global::OpenDental.Properties.Resources.printPreview20;
			this.butPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPreview.Location = new System.Drawing.Point(365,453);
			this.butPreview.Name = "butPreview";
			this.butPreview.Size = new System.Drawing.Size(79,24);
			this.butPreview.TabIndex = 243;
			this.butPreview.Text = "View";
			this.butPreview.Click += new System.EventHandler(this.butPreview_Click);
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(146,15);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(77,20);
			this.textDate.TabIndex = 244;
			// 
			// FormStatementOptions
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(709,493);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butPreview);
			this.Controls.Add(this.butEmail);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.checkIsSent);
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
			if(StmtList==null){
				if(StmtCur.IsSent){
					checkIsSent.Checked=true;
					initiallySent=true;
					SetEnabled(false);
				}
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
				if(PrefC.GetBool("FuchsOptionsOn")) {
					textDateStart.Text=DateTime.Today.AddDays(-90).ToShortDateString();
					textDateEnd.Text=DateTime.Today.ToShortDateString();
					listMode.SelectedIndex=0;
					checkIntermingled.Checked=true;
					groupFuchs.Visible=true;
					buttonFuchs1.Visible=true;
					buttonFuchs2.Visible=true;
					buttonFuchs3.Visible=true;
				}
				textNote.Text=StmtCur.Note;
				textNoteBold.Text=StmtCur.NoteBold;
			}
			else{
				//bulk edit
				//DateSent-------------------------------------------------------------------------------------
				textDate.Text="?";
				bool allSame=true;
				for(int i=0;i<StmtList.Count;i++){
					if(StmtList[0].DateSent!=StmtList[i].DateSent){//if any are different from the first element
						allSame=false;
					}
				}
				if(allSame){
					textDate.Text=StmtList[0].DateSent.ToShortDateString();
				}
				//IsSent----------------------------------------------------------------------------------------
				checkIsSent.ThreeState=true;
				checkIsSent.CheckState=CheckState.Indeterminate;
				allSame=true;
				for(int i=0;i<StmtList.Count;i++){
					if(StmtList[0].IsSent!=StmtList[i].IsSent){
						allSame=false;
					}
				}
				if(allSame){
					checkIsSent.ThreeState=false;
					checkIsSent.CheckState=CheckState.Unchecked;
					checkIsSent.Checked=StmtList[0].IsSent;
				}
				//Mode------------------------------------------------------------------------------------------
				allSame=true;
				for(int i=0;i<StmtList.Count;i++){
					if(StmtList[0].Mode_!=StmtList[i].Mode_){
						allSame=false;
					}
				}
				listMode.Items.Clear();
				for(int i=0;i<Enum.GetNames(typeof(StatementMode)).Length;i++){
					listMode.Items.Add(Lan.g("enumStatementMode",Enum.GetNames(typeof(StatementMode))[i]));
					if(allSame && (int)StmtList[0].Mode_==i){
						listMode.SelectedIndex=i;
					}
				}
				//HidePayment------------------------------------------------------------------------------------
				checkHidePayment.ThreeState=true;
				checkHidePayment.CheckState=CheckState.Indeterminate;
				allSame=true;
				for(int i=0;i<StmtList.Count;i++){
					if(StmtList[0].HidePayment!=StmtList[i].HidePayment){
						allSame=false;
					}
				}
				if(allSame){
					checkHidePayment.ThreeState=false;
					checkHidePayment.CheckState=CheckState.Unchecked;
					checkHidePayment.Checked=StmtList[0].HidePayment;
				}
				//SinglePatient------------------------------------------------------------------------------------
				checkSinglePatient.ThreeState=true;
				checkSinglePatient.CheckState=CheckState.Indeterminate;
				allSame=true;
				for(int i=0;i<StmtList.Count;i++){
					if(StmtList[0].SinglePatient!=StmtList[i].SinglePatient){
						allSame=false;
					}
				}
				if(allSame){
					checkSinglePatient.ThreeState=false;
					checkSinglePatient.CheckState=CheckState.Unchecked;
					checkSinglePatient.Checked=StmtList[0].SinglePatient;
				}
				//Intermingled----------------------------------------------------------------------------------------
				checkIntermingled.ThreeState=true;
				checkIntermingled.CheckState=CheckState.Indeterminate;
				allSame=true;
				for(int i=0;i<StmtList.Count;i++){
					if(StmtList[0].Intermingled!=StmtList[i].Intermingled){
						allSame=false;
					}
				}
				if(allSame){
					checkIntermingled.ThreeState=false;
					checkIntermingled.CheckState=CheckState.Unchecked;
					checkIntermingled.Checked=StmtList[0].Intermingled;
				}
				//DateStart-------------------------------------------------------------------------------------
				textDateStart.Text="?";
				allSame=true;
				for(int i=0;i<StmtList.Count;i++){
					if(StmtList[0].DateRangeFrom!=StmtList[i].DateRangeFrom){
						allSame=false;
					}
				}
				if(allSame){
					if(StmtList[0].DateRangeFrom.Year<1880){
						textDateStart.Text="";
					}
					else{
						textDateStart.Text=StmtList[0].DateRangeFrom.ToShortDateString();
					}
				}
				//DateEnd-------------------------------------------------------------------------------------
				textDateEnd.Text="?";
				allSame=true;
				for(int i=0;i<StmtList.Count;i++){
					if(StmtList[0].DateRangeTo!=StmtList[i].DateRangeTo){
						allSame=false;
					}
				}
				if(allSame){
					if(StmtList[0].DateRangeTo.Year<1880){
						textDateEnd.Text="";
					}
					else{
						textDateEnd.Text=StmtList[0].DateRangeTo.ToShortDateString();
					}
				}
				//Note----------------------------------------------------------------------------------------
				textNote.Text="?";
				allSame=true;
				for(int i=0;i<StmtList.Count;i++){
					if(StmtList[0].Note!=StmtList[i].Note){
						allSame=false;
					}
				}
				if(allSame){
					textNote.Text=StmtList[0].Note;
				}
				//NoteBold----------------------------------------------------------------------------------------
				textNoteBold.Text="?";
				allSame=true;
				for(int i=0;i<StmtList.Count;i++){
					if(StmtList[0].NoteBold!=StmtList[i].NoteBold){
						allSame=false;
					}
				}
				if(allSame){
					textNoteBold.Text=StmtList[0].NoteBold;
				}
				butEmail.Enabled=false;
				butPrint.Enabled=false;
				butPreview.Enabled=false;
			}
		}

		private void butToday_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
		}

		private void but45days_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.AddDays(-45).ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
		}

		private void but90days_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.AddDays(-90).ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
		}

		private void butDatesAll_Click(object sender,EventArgs e) {
			textDateStart.Text="";
			textDateEnd.Text=DateTime.Today.ToShortDateString();
		}

		private void buttonFuchs1_Click(object sender,EventArgs e) {
			textNote.Text="Your insurance has not yet paid for your claims, they are still pending. This is to keep you informed of the status of your account. Thank You "+textNote.Text;
		}

		private void buttonFuchs2_Click(object sender,EventArgs e) {
			textNote.Text="Your insurance paid and the remaining balance is yours. Thank You! "+textNote.Text;
		}

		private void buttonFuchs3_Click(object sender,EventArgs e) {
			textNote.Text="This credit is on your account. We look forward to seeing you on your next appointment! "+textNote.Text;
		}

		private void checkIsSent_Click(object sender,EventArgs e) {
			if(initiallySent && !checkIsSent.Checked){//user unchecks the Sent box in order to edit
				if(!MsgBox.Show(this,true,"Warning.  This will immediately delete the archived copy of the statement.  Continue anyway?")){
					checkIsSent.Checked=true;
					return;
				}
				SetEnabled(true);
				//Delete the archived copy of the statement
				if(StmtCur.DocNum!=0){
					Patient pat=Patients.GetPat(StmtCur.PatNum);
					string patFolder=ImageStore.GetPatientFolder(pat);
					List<Document> listdocs=new List<Document>();
					listdocs.Add(Documents.GetByNum(StmtCur.DocNum));
					ImageStore.DeleteImage(listdocs,patFolder);
				}
			}
		}

		private void SetEnabled(bool boolval){
			textDate.Enabled=boolval;
			listMode.Enabled=boolval;
			checkHidePayment.Enabled=boolval;
			checkSinglePatient.Enabled=boolval;
			checkIntermingled.Enabled=boolval;
			groupDateRange.Enabled=boolval;
			textNote.Enabled=boolval;
			textNoteBold.Enabled=boolval;
		}

		private void butPrint_Click(object sender,EventArgs e) {
			if(StmtCur.DocNum!=0 && checkIsSent.Checked){
				//launch existing archive pdf. User can click print from within Acrobat.
				Cursor=Cursors.WaitCursor;
				Patient pat=Patients.GetPat(StmtCur.PatNum);
				string patFolder=ImageStore.GetPatientFolder(pat);
				Process.Start(ImageStore.GetFilePath(Documents.GetByNum(StmtCur.DocNum),patFolder));
				Cursor=Cursors.Default;
			}
			else{//was not initially sent, or else user has unchecked the sent box
				if(initiallySent && checkIsSent.Checked && StmtCur.DocNum==0){
					MsgBox.Show(this,"There was no archived image of this statement.  The printout will be based on current data.");
				}
				//So create an archive
				if(listMode.SelectedIndex==(int)StatementMode.Email){
					listMode.SelectedIndex=(int)StatementMode.InPerson;
				}
				checkIsSent.Checked=true;
				Cursor=Cursors.WaitCursor;
				if(!SaveToDb()){
					Cursor=Cursors.Default;
					return;
				}
				FormRpStatement FormST=new FormRpStatement();
				Family fam=Patients.GetFamily(StmtCur.PatNum);
				Patient pat=fam.GetPatient(StmtCur.PatNum);
				DataSet dataSet=AccountModules.GetStatement(StmtCur.PatNum,StmtCur.SinglePatient,StmtCur.DateRangeFrom,StmtCur.DateRangeTo,StmtCur.Intermingled);
				FormST.CreateStatementPdf(StmtCur,pat,fam,dataSet);
				#if DEBUG
					FormST.PrintStatement(StmtCur,true,dataSet,fam,pat);
					FormST.ShowDialog();
				#else
					FormST.PrintStatement(StmtCur,false,dataSet,fam,pat);
				#endif
				Cursor=Cursors.Default;
			}
			DialogResult=DialogResult.OK;
		}

		private void butEmail_Click(object sender,EventArgs e) {
			if(StmtCur.DocNum!=0 && checkIsSent.Checked){
				//remail existing archive pdf?
				//or maybe tell user they can't do that?
				MsgBox.Show(this,"Email was already sent.");
				return;
			}
			else{//was not initially sent, or else user has unchecked the sent box
				//So create an archive
				if(listMode.SelectedIndex!=(int)StatementMode.Email){
					listMode.SelectedIndex=(int)StatementMode.Email;
				}
				checkIsSent.Checked=true;
				Cursor=Cursors.WaitCursor;
				if(!SaveToDb()){
					return;
				}
				FormRpStatement FormST=new FormRpStatement();
				Family fam=Patients.GetFamily(StmtCur.PatNum);
				Patient pat=fam.GetPatient(StmtCur.PatNum);
				DataSet dataSet=AccountModules.GetStatement(StmtCur.PatNum,StmtCur.SinglePatient,StmtCur.DateRangeFrom,StmtCur.DateRangeTo,StmtCur.Intermingled);
				FormST.CreateStatementPdf(StmtCur,pat,fam,dataSet);
				if(!CreateEmailMessage()){
					Cursor=Cursors.Default;
					return;
				}
				Cursor=Cursors.Default;
			}
			DialogResult=DialogResult.OK;
		}

		/// <summary>Also displays the dialog for the email.  Must have already created and attached the pdf.  Returns false if it could not create the email.</summary>
		private bool CreateEmailMessage(){
			string attachPath=FormEmailMessageEdit.GetAttachPath();
			Random rnd=new Random();
			string fileName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()+".pdf";
			string filePathAndName=ODFileUtils.CombinePaths(attachPath,fileName);
			if(!PrefC.UsingAtoZfolder){
				MsgBox.Show(this,"Could not create email because no AtoZ folder.");
				return false;
			}
			Patient pat=Patients.GetPat(StmtCur.PatNum);
			string oldPath=ODFileUtils.CombinePaths(ImageStore.GetPatientFolder(pat),Documents.GetByNum(StmtCur.DocNum).FileName);
			File.Copy(oldPath,filePathAndName);//
			//Process.Start(filePathAndName);
			EmailMessage message=new EmailMessage();
			message.PatNum=pat.PatNum;
			message.ToAddress=pat.Email;
			message.FromAddress=PrefC.GetString("EmailSenderAddress");
			message.Subject=Lan.g(this,"Statement");
			//message.BodyText=Lan.g(this,"");
			EmailAttach attach=new EmailAttach();
			attach.DisplayedFileName="Statement.pdf";
			attach.ActualFileName=fileName;
			message.Attachments.Add(attach);
			FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
			FormE.IsNew=true;
			FormE.ShowDialog();
			if(FormE.DialogResult==DialogResult.OK){
				return true;
			}
			return false;
		}

		private void butPreview_Click(object sender,EventArgs e) {
			if(StmtCur.DocNum!=0 && checkIsSent.Checked){//initiallySent && checkIsSent.Checked){
				//launch existing archive pdf
				Cursor=Cursors.WaitCursor;
				Patient pat=Patients.GetPat(StmtCur.PatNum);
				string patFolder=ImageStore.GetPatientFolder(pat);
				Process.Start(ImageStore.GetFilePath(Documents.GetByNum(StmtCur.DocNum),patFolder));
				Cursor=Cursors.Default;
			}
			else{//was not initially sent, or else user has unchecked the sent box
				//No archive to use, so just preview on the fly
				if(initiallySent && checkIsSent.Checked && StmtCur.DocNum==0){
					MsgBox.Show(this,"There was no archived image of this statement.  The preview will be based on current data.");
				}
				Cursor=Cursors.WaitCursor;
				if(!SaveToDb()){
					Cursor=Cursors.Default;
					return;
				}
				FormRpStatement FormST=new FormRpStatement();
				Family fam=Patients.GetFamily(StmtCur.PatNum);
				Patient pat=fam.GetPatient(StmtCur.PatNum);
				DataSet dataSet=AccountModules.GetStatement(StmtCur.PatNum,StmtCur.SinglePatient,StmtCur.DateRangeFrom,StmtCur.DateRangeTo,StmtCur.Intermingled);
				FormST.PrintStatement(StmtCur,true,dataSet,fam,pat);
				FormST.ShowDialog();
				Cursor=Cursors.Default;
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(StmtList==null && StmtCur.IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,true,"Delete?")){
				return;
			}
			Patient pat;
			string patFolder;
			if(StmtList==null){
				if(StmtCur.DocNum!=0){
					//deleted the pdf
					pat=Patients.GetPat(StmtCur.PatNum);
					patFolder=ImageStore.GetPatientFolder(pat);
					List<Document> listdocs=new List<Document>();
					listdocs.Add(Documents.GetByNum(StmtCur.DocNum));
					ImageStore.DeleteImage(listdocs,patFolder);
				}
				Statements.DeleteObject(StmtCur);
			}
			else{//bulk edit
				for(int i=0;i<StmtList.Count;i++){
					if(StmtList[i].DocNum!=0){
						//deleted the pdf
						pat=Patients.GetPat(StmtList[i].PatNum);
						patFolder=ImageStore.GetPatientFolder(pat);
						List<Document> listdocs=new List<Document>();
						listdocs.Add(Documents.GetByNum(StmtList[i].DocNum));
						ImageStore.DeleteImage(listdocs,patFolder);
					}
					Statements.DeleteObject(StmtList[i]);
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!SaveToDb()){
				return;
			}
			//if(StmtCur.Mode_==StatementMode.Email){

			//}
			DialogResult=DialogResult.OK;
		}

		private bool SaveToDb(){
			bool isError;
			//Validate Date-------------------------------------------------------------------------------
			isError=false;
			if(textDate.Text==""){//not allowed to be blank.  Other two dates are allowed to be blank.
				if(StmtList==null){//if editing a List, blank indicates dates vary.
					MsgBox.Show(this,"Please enter a Date.");
					return false;
				}
			}
			else{//"?" not allowed here
				try{
					DateTime.Parse(textDate.Text);
				}
				catch{
					isError=true;
				}
			}
			if(isError){
				MsgBox.Show(this,"Please fix Date.");
				return false;
			}
			//Validate DateStart-------------------------------------------------------------------------------
			isError=false;
			if(textDateStart.Text==""){
				//no error
			}
			else if(textDateStart.Text=="?"){
				if(StmtList==null){
					isError=true;
				}
			}
			else{
				try{
					DateTime.Parse(textDateStart.Text);
				}
				catch{
					isError=true;
				}
			}
			if(isError){
				MsgBox.Show(this,"Please fix Start Date.");
				return false;
			}
			//Validate DateEnd-------------------------------------------------------------------------------
			isError=false;
			if(textDateEnd.Text==""){
				//no error
			}
			else if(textDateEnd.Text=="?"){
				if(StmtList==null){
					isError=true;
				}
			}
			else{
				try{
					DateTime.Parse(textDateEnd.Text);
				}
				catch{
					isError=true;
				}
			}
			if(isError){
				MsgBox.Show(this,"Please fix End Date.");
				return false;
			}
			//if(  textDateStart.Text .errorProvider1.GetError(textDateStart)!=""
			//	|| textDateEnd.errorProvider1.GetError(textDateEnd)!=""
			//	|| textDate.errorProvider1.GetError(textDate)!="")
			//{
			//	MsgBox.Show(this,"Please fix data entry errors first.");
			//	return false;
			//}
			if(StmtList==null){
				StmtCur.DateSent=PIn.PDate(textDate.Text);
				StmtCur.IsSent=checkIsSent.Checked;
				StmtCur.Mode_=(StatementMode)listMode.SelectedIndex;
				StmtCur.HidePayment=checkHidePayment.Checked;
				StmtCur.SinglePatient=checkSinglePatient.Checked;
				StmtCur.Intermingled=checkIntermingled.Checked;
				StmtCur.DateRangeFrom=PIn.PDate(textDateStart.Text);//handles blank
				if(textDateEnd.Text==""){
					StmtCur.DateRangeTo=new DateTime(2200,1,1);//max val
				}
				else{
					StmtCur.DateRangeTo=PIn.PDate(textDateEnd.Text);
				}
				StmtCur.Note=textNote.Text;
				StmtCur.NoteBold=textNoteBold.Text;
				Statements.WriteObject(StmtCur);
			}
			else{
				for(int i=0;i<StmtList.Count;i++){
					if(textDate.Text!=""){
						StmtList[i].DateSent=PIn.PDate(textDate.Text);
					}
					if(checkIsSent.CheckState!=CheckState.Indeterminate){
						StmtList[i].IsSent=checkIsSent.Checked;
					}
					if(listMode.SelectedIndex!=-1){
						StmtList[i].Mode_=(StatementMode)listMode.SelectedIndex;
					}
					if(checkHidePayment.CheckState!=CheckState.Indeterminate){
						StmtList[i].HidePayment=checkHidePayment.Checked;
					}
					if(checkSinglePatient.CheckState!=CheckState.Indeterminate){
						StmtList[i].SinglePatient=checkSinglePatient.Checked;
					}
					if(checkIntermingled.CheckState!=CheckState.Indeterminate){
						StmtList[i].Intermingled=checkIntermingled.Checked;
					}
					if(textDateStart.Text!="?"){
						StmtList[i].DateRangeFrom=PIn.PDate(textDateStart.Text);//handles blank
					}
					if(textDateStart.Text!="?"){
						if(textDateEnd.Text==""){
							StmtList[i].DateRangeTo=new DateTime(2200,1,1);//max val
						}
						else{
							StmtList[i].DateRangeTo=PIn.PDate(textDateEnd.Text);
						}
					}
					if(textNote.Text!="?"){
						StmtList[i].Note=textNote.Text;
					}
					if(textNoteBold.Text!="?"){
						StmtList[i].NoteBold=textNoteBold.Text;
					}
					Statements.WriteObject(StmtList[i]);
				}
			}
			return true;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

	

		

		



	}
}





















