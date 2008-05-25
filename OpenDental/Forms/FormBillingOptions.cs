using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FormBillingOptions : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butCancel;
		//private FormQuery FormQuery2;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butSaveDefault;
		private OpenDental.ValidDouble textExcludeLessThan;
		private System.Windows.Forms.CheckBox checkExcludeInactive;
		private System.Windows.Forms.GroupBox groupBox2;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.ODGrid gridDun;
		private System.Windows.Forms.Label label4;
		private OpenDental.ODtextBox textNote;
		private System.Windows.Forms.CheckBox checkBadAddress;
		private System.Windows.Forms.CheckBox checkExcludeNegative;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkIncludeChanged;
		private OpenDental.ValidDate textLastStatement;
		private OpenDental.UI.Button butCreate;
		private CheckBox checkExcludeInsPending;
		private GroupBox groupDateRange;
		private ValidDate textDateStart;
		private Label labelStartDate;
		private Label labelEndDate;
		private ValidDate textDateEnd;
		private OpenDental.UI.Button but45days;
		private OpenDental.UI.Button but90days;
		private OpenDental.UI.Button butDatesAll;
		private CheckBox checkIntermingled;
		private OpenDental.UI.Button butDefaults;
		private OpenDental.UI.Button but30days;
		private ComboBox comboAge;
		private Label label6;
		private Label label7;
		private ListBox listBillType;
		private Label label2;
		private OpenDental.UI.Button butUndo;
		private Dunning[] dunningList;

		///<summary></summary>
		public FormBillingOptions(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBillingOptions));
			this.butCancel = new OpenDental.UI.Button();
			this.butCreate = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butSaveDefault = new OpenDental.UI.Button();
			this.textExcludeLessThan = new OpenDental.ValidDouble();
			this.checkExcludeInactive = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.comboAge = new System.Windows.Forms.ComboBox();
			this.checkExcludeInsPending = new System.Windows.Forms.CheckBox();
			this.checkIncludeChanged = new System.Windows.Forms.CheckBox();
			this.textLastStatement = new OpenDental.ValidDate();
			this.label5 = new System.Windows.Forms.Label();
			this.checkExcludeNegative = new System.Windows.Forms.CheckBox();
			this.checkBadAddress = new System.Windows.Forms.CheckBox();
			this.listBillType = new System.Windows.Forms.ListBox();
			this.gridDun = new OpenDental.UI.ODGrid();
			this.butAdd = new OpenDental.UI.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textNote = new OpenDental.ODtextBox();
			this.groupDateRange = new System.Windows.Forms.GroupBox();
			this.but30days = new OpenDental.UI.Button();
			this.textDateStart = new OpenDental.ValidDate();
			this.labelStartDate = new System.Windows.Forms.Label();
			this.labelEndDate = new System.Windows.Forms.Label();
			this.textDateEnd = new OpenDental.ValidDate();
			this.but45days = new OpenDental.UI.Button();
			this.but90days = new OpenDental.UI.Button();
			this.butDatesAll = new OpenDental.UI.Button();
			this.checkIntermingled = new System.Windows.Forms.CheckBox();
			this.butDefaults = new OpenDental.UI.Button();
			this.butUndo = new OpenDental.UI.Button();
			this.groupBox2.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(806,631);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(79,24);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butCreate
			// 
			this.butCreate.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCreate.Autosize = true;
			this.butCreate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCreate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCreate.CornerRadius = 4F;
			this.butCreate.Location = new System.Drawing.Point(693,631);
			this.butCreate.Name = "butCreate";
			this.butCreate.Size = new System.Drawing.Size(92,24);
			this.butCreate.TabIndex = 3;
			this.butCreate.Text = "Create &List";
			this.butCreate.Click += new System.EventHandler(this.butCreate_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(5,187);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(192,16);
			this.label1.TabIndex = 18;
			this.label1.Text = "Exclude if Balance is less than";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butSaveDefault
			// 
			this.butSaveDefault.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSaveDefault.Autosize = true;
			this.butSaveDefault.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSaveDefault.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSaveDefault.CornerRadius = 4F;
			this.butSaveDefault.Location = new System.Drawing.Point(168,561);
			this.butSaveDefault.Name = "butSaveDefault";
			this.butSaveDefault.Size = new System.Drawing.Size(108,24);
			this.butSaveDefault.TabIndex = 20;
			this.butSaveDefault.Text = "&Save As Default";
			this.butSaveDefault.Click += new System.EventHandler(this.butSaveDefault_Click);
			// 
			// textExcludeLessThan
			// 
			this.textExcludeLessThan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textExcludeLessThan.Location = new System.Drawing.Point(199,186);
			this.textExcludeLessThan.Name = "textExcludeLessThan";
			this.textExcludeLessThan.Size = new System.Drawing.Size(77,20);
			this.textExcludeLessThan.TabIndex = 22;
			// 
			// checkExcludeInactive
			// 
			this.checkExcludeInactive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkExcludeInactive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkExcludeInactive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeInactive.Location = new System.Drawing.Point(45,122);
			this.checkExcludeInactive.Name = "checkExcludeInactive";
			this.checkExcludeInactive.Size = new System.Drawing.Size(231,18);
			this.checkExcludeInactive.TabIndex = 23;
			this.checkExcludeInactive.Text = "Exclude inactive patients";
			this.checkExcludeInactive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.comboAge);
			this.groupBox2.Controls.Add(this.checkExcludeInsPending);
			this.groupBox2.Controls.Add(this.checkIncludeChanged);
			this.groupBox2.Controls.Add(this.textLastStatement);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.checkExcludeNegative);
			this.groupBox2.Controls.Add(this.checkBadAddress);
			this.groupBox2.Controls.Add(this.checkExcludeInactive);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.textExcludeLessThan);
			this.groupBox2.Controls.Add(this.butSaveDefault);
			this.groupBox2.Controls.Add(this.listBillType);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(7,12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(283,609);
			this.groupBox2.TabIndex = 24;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Filter";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(84,590);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(192,16);
			this.label2.TabIndex = 246;
			this.label2.Text = "(except the date at the top)";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.Location = new System.Drawing.Point(5,215);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(111,16);
			this.label7.TabIndex = 245;
			this.label7.Text = "Billing Types";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.Location = new System.Drawing.Point(3,75);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(128,16);
			this.label6.TabIndex = 243;
			this.label6.Text = "Age of Account";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboAge
			// 
			this.comboAge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboAge.FormattingEnabled = true;
			this.comboAge.Location = new System.Drawing.Point(132,73);
			this.comboAge.Name = "comboAge";
			this.comboAge.Size = new System.Drawing.Size(145,21);
			this.comboAge.TabIndex = 242;
			// 
			// checkExcludeInsPending
			// 
			this.checkExcludeInsPending.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkExcludeInsPending.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkExcludeInsPending.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeInsPending.Location = new System.Drawing.Point(45,162);
			this.checkExcludeInsPending.Name = "checkExcludeInsPending";
			this.checkExcludeInsPending.Size = new System.Drawing.Size(231,18);
			this.checkExcludeInsPending.TabIndex = 27;
			this.checkExcludeInsPending.Text = "Exclude if insurance pending";
			this.checkExcludeInsPending.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIncludeChanged
			// 
			this.checkIncludeChanged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkIncludeChanged.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIncludeChanged.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIncludeChanged.Location = new System.Drawing.Point(3,39);
			this.checkIncludeChanged.Name = "checkIncludeChanged";
			this.checkIncludeChanged.Size = new System.Drawing.Size(273,28);
			this.checkIncludeChanged.TabIndex = 26;
			this.checkIncludeChanged.Text = "Include any accounts with insurance payments or procedures since the last bill";
			this.checkIncludeChanged.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textLastStatement
			// 
			this.textLastStatement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textLastStatement.Location = new System.Drawing.Point(183,13);
			this.textLastStatement.Name = "textLastStatement";
			this.textLastStatement.Size = new System.Drawing.Size(94,20);
			this.textLastStatement.TabIndex = 25;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(6,15);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(176,16);
			this.label5.TabIndex = 24;
			this.label5.Text = "Include anyone not billed since";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkExcludeNegative
			// 
			this.checkExcludeNegative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkExcludeNegative.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkExcludeNegative.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeNegative.Location = new System.Drawing.Point(45,142);
			this.checkExcludeNegative.Name = "checkExcludeNegative";
			this.checkExcludeNegative.Size = new System.Drawing.Size(231,18);
			this.checkExcludeNegative.TabIndex = 17;
			this.checkExcludeNegative.Text = "Exclude negative balances (credits)";
			this.checkExcludeNegative.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkBadAddress
			// 
			this.checkBadAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBadAddress.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBadAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBadAddress.Location = new System.Drawing.Point(45,102);
			this.checkBadAddress.Name = "checkBadAddress";
			this.checkBadAddress.Size = new System.Drawing.Size(231,18);
			this.checkBadAddress.TabIndex = 16;
			this.checkBadAddress.Text = "Exclude bad addresses (no zipcode)";
			this.checkBadAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listBillType
			// 
			this.listBillType.Location = new System.Drawing.Point(118,215);
			this.listBillType.Name = "listBillType";
			this.listBillType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBillType.Size = new System.Drawing.Size(158,225);
			this.listBillType.TabIndex = 2;
			// 
			// gridDun
			// 
			this.gridDun.HScrollVisible = false;
			this.gridDun.Location = new System.Drawing.Point(331,31);
			this.gridDun.Name = "gridDun";
			this.gridDun.ScrollValue = 0;
			this.gridDun.Size = new System.Drawing.Size(561,366);
			this.gridDun.TabIndex = 0;
			this.gridDun.Title = "Dunning Messages";
			this.gridDun.TranslationName = "TableBillingMessages";
			this.gridDun.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridDun_CellDoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(768,403);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(124,24);
			this.butAdd.TabIndex = 5;
			this.butAdd.Text = "Add Dunning Msg";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(328,9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(564,16);
			this.label3.TabIndex = 25;
			this.label3.Text = "Items higher in the list are more general.  Items lower in the list take preceden" +
    "ce .";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(328,498);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(564,16);
			this.label4.TabIndex = 26;
			this.label4.Text = "General Message (in addition to any dunning messages and appointment reminders)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(331,518);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Statement;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(561,102);
			this.textNote.TabIndex = 28;
			// 
			// groupDateRange
			// 
			this.groupDateRange.Controls.Add(this.but30days);
			this.groupDateRange.Controls.Add(this.textDateStart);
			this.groupDateRange.Controls.Add(this.labelStartDate);
			this.groupDateRange.Controls.Add(this.labelEndDate);
			this.groupDateRange.Controls.Add(this.textDateEnd);
			this.groupDateRange.Controls.Add(this.but45days);
			this.groupDateRange.Controls.Add(this.but90days);
			this.groupDateRange.Controls.Add(this.butDatesAll);
			this.groupDateRange.Location = new System.Drawing.Point(331,403);
			this.groupDateRange.Name = "groupDateRange";
			this.groupDateRange.Size = new System.Drawing.Size(319,69);
			this.groupDateRange.TabIndex = 237;
			this.groupDateRange.TabStop = false;
			this.groupDateRange.Text = "Date Range";
			// 
			// but30days
			// 
			this.but30days.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but30days.Autosize = true;
			this.but30days.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but30days.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but30days.CornerRadius = 4F;
			this.but30days.Location = new System.Drawing.Point(154,13);
			this.but30days.Name = "but30days";
			this.but30days.Size = new System.Drawing.Size(77,24);
			this.but30days.TabIndex = 242;
			this.but30days.Text = "Last 30 Days";
			this.but30days.Click += new System.EventHandler(this.but30days_Click);
			// 
			// textDateStart
			// 
			this.textDateStart.BackColor = System.Drawing.SystemColors.Window;
			this.textDateStart.ForeColor = System.Drawing.SystemColors.WindowText;
			this.textDateStart.Location = new System.Drawing.Point(75,16);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77,20);
			this.textDateStart.TabIndex = 223;
			// 
			// labelStartDate
			// 
			this.labelStartDate.Location = new System.Drawing.Point(6,19);
			this.labelStartDate.Name = "labelStartDate";
			this.labelStartDate.Size = new System.Drawing.Size(69,14);
			this.labelStartDate.TabIndex = 221;
			this.labelStartDate.Text = "Start Date";
			this.labelStartDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelEndDate
			// 
			this.labelEndDate.Location = new System.Drawing.Point(6,42);
			this.labelEndDate.Name = "labelEndDate";
			this.labelEndDate.Size = new System.Drawing.Size(69,14);
			this.labelEndDate.TabIndex = 222;
			this.labelEndDate.Text = "End Date";
			this.labelEndDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(75,39);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(77,20);
			this.textDateEnd.TabIndex = 224;
			// 
			// but45days
			// 
			this.but45days.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but45days.Autosize = true;
			this.but45days.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but45days.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but45days.CornerRadius = 4F;
			this.but45days.Location = new System.Drawing.Point(154,37);
			this.but45days.Name = "but45days";
			this.but45days.Size = new System.Drawing.Size(77,24);
			this.but45days.TabIndex = 226;
			this.but45days.Text = "Last 45 Days";
			this.but45days.Click += new System.EventHandler(this.but45days_Click);
			// 
			// but90days
			// 
			this.but90days.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but90days.Autosize = true;
			this.but90days.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but90days.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but90days.CornerRadius = 4F;
			this.but90days.Location = new System.Drawing.Point(233,37);
			this.but90days.Name = "but90days";
			this.but90days.Size = new System.Drawing.Size(77,24);
			this.but90days.TabIndex = 227;
			this.but90days.Text = "Last 90 Days";
			this.but90days.Click += new System.EventHandler(this.but90days_Click);
			// 
			// butDatesAll
			// 
			this.butDatesAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDatesAll.Autosize = true;
			this.butDatesAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDatesAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDatesAll.CornerRadius = 4F;
			this.butDatesAll.Location = new System.Drawing.Point(233,13);
			this.butDatesAll.Name = "butDatesAll";
			this.butDatesAll.Size = new System.Drawing.Size(77,24);
			this.butDatesAll.TabIndex = 228;
			this.butDatesAll.Text = "All Dates";
			this.butDatesAll.Click += new System.EventHandler(this.butDatesAll_Click);
			// 
			// checkIntermingled
			// 
			this.checkIntermingled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIntermingled.Location = new System.Drawing.Point(331,475);
			this.checkIntermingled.Name = "checkIntermingled";
			this.checkIntermingled.Size = new System.Drawing.Size(150,20);
			this.checkIntermingled.TabIndex = 239;
			this.checkIntermingled.Text = "Intermingle family members";
			// 
			// butDefaults
			// 
			this.butDefaults.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDefaults.Autosize = true;
			this.butDefaults.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDefaults.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDefaults.CornerRadius = 4F;
			this.butDefaults.Location = new System.Drawing.Point(656,440);
			this.butDefaults.Name = "butDefaults";
			this.butDefaults.Size = new System.Drawing.Size(76,24);
			this.butDefaults.TabIndex = 241;
			this.butDefaults.Text = "Defaults";
			this.butDefaults.Click += new System.EventHandler(this.butDefaults_Click);
			// 
			// butUndo
			// 
			this.butUndo.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUndo.Autosize = true;
			this.butUndo.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUndo.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUndo.CornerRadius = 4F;
			this.butUndo.Location = new System.Drawing.Point(7,631);
			this.butUndo.Name = "butUndo";
			this.butUndo.Size = new System.Drawing.Size(88,25);
			this.butUndo.TabIndex = 243;
			this.butUndo.Text = "Undo Billing";
			this.butUndo.Click += new System.EventHandler(this.butUndo_Click);
			// 
			// FormBillingOptions
			// 
			this.AcceptButton = this.butCreate;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(898,666);
			this.Controls.Add(this.butUndo);
			this.Controls.Add(this.butDefaults);
			this.Controls.Add(this.checkIntermingled);
			this.Controls.Add(this.groupDateRange);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butCreate);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.gridDun);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBillingOptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Billing Options";
			this.Load += new System.EventHandler(this.FormBillingOptions_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupDateRange.ResumeLayout(false);
			this.groupDateRange.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormBillingOptions_Load(object sender, System.EventArgs e) {
			if(PIn.PDate(PrefC.GetString("DateLastAging")) < DateTime.Today){
				if(MessageBox.Show(Lan.g(this,"Update aging first?"),"",MessageBoxButtons.YesNo)==DialogResult.Yes){
					FormAging FormA=new FormAging();
					FormA.ShowDialog();
				}
			}
			textLastStatement.Text=DateTime.Today.AddMonths(-1).ToShortDateString();
			checkIncludeChanged.Checked=PrefC.GetBool("BillingIncludeChanged");
			comboAge.Items.Add(Lan.g(this,"Any Balance"));
			comboAge.Items.Add(Lan.g(this,"Over 30 Days"));
			comboAge.Items.Add(Lan.g(this,"Over 60 Days"));
			comboAge.Items.Add(Lan.g(this,"Over 90 Days"));
			switch(PrefC.GetString("BillingAgeOfAccount")){
				default:
					comboAge.SelectedIndex=0;
					break;
				case "30":
					comboAge.SelectedIndex=1;
					break;
				case "60":
					comboAge.SelectedIndex=2;
					break;
				case "90":
					comboAge.SelectedIndex=3;
					break;
			}
			checkBadAddress.Checked=PrefC.GetBool("BillingExcludeBadAddresses");
			checkExcludeInactive.Checked=PrefC.GetBool("BillingExcludeInactive");
			checkExcludeNegative.Checked=PrefC.GetBool("BillingExcludeNegative");
			checkExcludeInsPending.Checked=PrefC.GetBool("BillingExcludeInsPending");
			textExcludeLessThan.Text=PrefC.GetString("BillingExcludeLessThan");
			listBillType.Items.Add(Lan.g(this,"(all)"));
			for(int i=0;i<DefC.Short[(int)DefCat.BillingTypes].Length;i++){
				listBillType.Items.Add(DefC.Short[(int)DefCat.BillingTypes][i].ItemName);
			}
			string[] selectedBillTypes=PrefC.GetString("BillingSelectBillingTypes").Split(',');//might be blank
			for(int i=0;i<selectedBillTypes.Length;i++){
				try{
					int order=DefC.GetOrder(DefCat.BillingTypes,Convert.ToInt32(selectedBillTypes[i]));
					if(order!=-1){
						listBillType.SetSelected(order+1,true);
					}
				}
				catch{}
			}
			if(listBillType.SelectedIndices.Count==0){
				listBillType.SelectedIndex=0;
			}
			//blank is allowed
			FillDunning();
			SetDefaults();
		}

		//private void butAll_Click(object sender, System.EventArgs e) {
		//	for(int i=0;i<listBillType.Items.Count;i++){
		//		listBillType.SetSelected(i,true);
		//	}
		//}

		private void butSaveDefault_Click(object sender, System.EventArgs e) {
			if( textExcludeLessThan.errorProvider1.GetError(textExcludeLessThan)!=""
				|| textLastStatement.errorProvider1.GetError(textLastStatement)!=""
				)
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(listBillType.SelectedIndices.Count==0){
				MsgBox.Show(this,"Please select at least one billing type first.");
				return;
			}
			string selectedBillingTypes="";//indicates all.
			for(int i=0;i<listBillType.SelectedIndices.Count;i++){//will always be at least 1
				if(listBillType.SelectedIndices[i]==0){//all
					//it ignores any other selections.
					selectedBillingTypes="";
					break;
				}
				if(i>0){
					selectedBillingTypes+=",";
				}
				selectedBillingTypes+=DefC.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndices[i]-1].DefNum.ToString();
			}
			string ageOfAccount="";
			if(comboAge.SelectedIndex==0){
				ageOfAccount="";//the default
			}
			else if(comboAge.SelectedIndex==1){
				ageOfAccount="30";
			}
			else if(comboAge.SelectedIndex==2){
				ageOfAccount="60";
			}
			else if(comboAge.SelectedIndex==3){
				ageOfAccount="90";
			}
			if(Prefs.UpdateBool("BillingIncludeChanged",checkIncludeChanged.Checked)
				| Prefs.UpdateString("BillingSelectBillingTypes",selectedBillingTypes)
				| Prefs.UpdateString("BillingAgeOfAccount",ageOfAccount)
				| Prefs.UpdateBool("BillingExcludeBadAddresses",checkBadAddress.Checked)
				| Prefs.UpdateBool("BillingExcludeInactive",checkExcludeInactive.Checked)
				| Prefs.UpdateBool("BillingExcludeNegative",checkExcludeNegative.Checked)
				| Prefs.UpdateBool("BillingExcludeInsPending",checkExcludeInsPending.Checked)
				| Prefs.UpdateString("BillingExcludeLessThan",textExcludeLessThan.Text))
			{
				DataValid.SetInvalid(InvalidType.Prefs);
			}
		}

		private void FillDunning(){
			dunningList=Dunnings.Refresh();
			gridDun.BeginUpdate();
			gridDun.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Billing Type",100);
			gridDun.Columns.Add(col);
			col=new ODGridColumn("Aging",70);
			gridDun.Columns.Add(col);
			col=new ODGridColumn("Ins",40);
			gridDun.Columns.Add(col);
			col=new ODGridColumn("Message",190);
			gridDun.Columns.Add(col);
			col=new ODGridColumn("Bold Message",190);
			gridDun.Columns.Add(col);
			gridDun.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			//string text;
			OpenDental.UI.ODGridCell cell;
			for(int i=0;i<dunningList.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				if(dunningList[i].BillingType==0){
					row.Cells.Add(Lan.g(this,"all"));
				}
				else{
					row.Cells.Add(DefC.GetName(DefCat.BillingTypes,dunningList[i].BillingType));
				}
				if(dunningList[i].AgeAccount==0){
					row.Cells.Add(Lan.g(this,"any"));
				}
				else{
					row.Cells.Add(Lan.g(this,"Over ")+dunningList[i].AgeAccount.ToString());
				}
				if(dunningList[i].InsIsPending==YN.Unknown){
					row.Cells.Add(Lan.g(this,"any"));
				}
				else if(dunningList[i].InsIsPending==YN.Yes){
					row.Cells.Add(Lan.g(this,"Y"));
				}
				else if(dunningList[i].InsIsPending==YN.No){
					row.Cells.Add(Lan.g(this,"N"));
				}
				row.Cells.Add(dunningList[i].DunMessage);
				cell=new ODGridCell(dunningList[i].MessageBold);
				cell.Bold=YN.Yes;
				cell.ColorText=Color.DarkRed;
				row.Cells.Add(cell);
				gridDun.Rows.Add(row);
			}
			gridDun.EndUpdate();
		}

		private void gridDun_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			FormDunningEdit formD=new FormDunningEdit(dunningList[e.Row]);
			formD.ShowDialog();
			FillDunning();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Dunning dun=new Dunning();
			FormDunningEdit FormD=new FormDunningEdit(dun);
			FormD.IsNew=true;
			FormD.ShowDialog();
			if(FormD.DialogResult==DialogResult.Cancel){
				return;
			}
			FillDunning();
		}

		private void butDefaults_Click(object sender,EventArgs e) {
			FormBillingDefaults FormB=new FormBillingDefaults();
			FormB.ShowDialog();
			if(FormB.DialogResult==DialogResult.OK){
				SetDefaults();
			}
		}

		private void SetDefaults(){
			textDateStart.Text=DateTime.Today.AddDays(-PrefC.GetInt("BillingDefaultsLastDays")).ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
			checkIntermingled.Checked=PrefC.GetBool("BillingDefaultsIntermingle");
			textNote.Text=PrefC.GetString("BillingDefaultsNote");
		}

		private void but30days_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.AddDays(-30).ToShortDateString();
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

		private void butUndo_Click(object sender,EventArgs e) {
			MsgBox.Show(this,"When the billing list comes up, use the radio button at the top to show the 'Sent' bills.\r\nThen, change their status back to unsent.\r\nYou can edit them as a group using the button at the right.");
			DialogResult=DialogResult.OK;//will trigger ContrStaff to bring up the billing window
		}

		private void butCreate_Click(object sender, System.EventArgs e) {
			if( textExcludeLessThan.errorProvider1.GetError(textExcludeLessThan)!=""
				|| textLastStatement.errorProvider1.GetError(textLastStatement)!=""
				|| textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateEnd.errorProvider1.GetError(textDateEnd)!=""
				)
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			DateTime lastStatement=PIn.PDate(textLastStatement.Text);
			string getAge="";
			if(comboAge.SelectedIndex==1) getAge="30";
			else if(comboAge.SelectedIndex==2) getAge="60";
			else if(comboAge.SelectedIndex==3) getAge="90";
			List<int> billingNums=new List<int>();//[listBillType.SelectedIndices.Count];
			for(int i=0;i<listBillType.SelectedIndices.Count;i++){
				if(listBillType.SelectedIndices[i]==0){//if (all) is selected, then ignore any other selections
					billingNums.Clear();
					break;
				}
				billingNums.Add(DefC.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndices[i]-1].DefNum);
			}
			Cursor=Cursors.WaitCursor;
			List<PatAging> agingList=Patients.GetAgingList(getAge,lastStatement,billingNums,checkBadAddress.Checked
				,checkExcludeNegative.Checked,PIn.PDouble(textExcludeLessThan.Text)
				,checkExcludeInactive.Checked,checkIncludeChanged.Checked,checkExcludeInsPending.Checked);
			DateTime dateRangeFrom=DateTime.MinValue;
			DateTime dateRangeTo=DateTime.Today;//Needed for payplan accuracy.//new DateTime(2200,1,1);
			if(textDateStart.Text!=""){
				dateRangeFrom=PIn.PDate(textDateStart.Text);
			}
			if(textDateEnd.Text!=""){
				dateRangeTo=PIn.PDate(textDateEnd.Text);
			}
			if(agingList.Count==0){
				Cursor=Cursors.Default;
				MsgBox.Show(this,"List of created bills is empty.");
				return;
			}
			//if(agingList!=null){
			Statement stmt;
			int ageAccount=0;
			YN insIsPending=YN.Unknown;
			Dunning dunning;
			Dunning[] dunList=Dunnings.Refresh();
			for(int i=0;i<agingList.Count;i++){
				stmt=new Statement();
				stmt.DateRangeFrom=dateRangeFrom;
				stmt.DateRangeTo=dateRangeTo;
				stmt.DateSent=DateTime.Today;
				stmt.DocNum=0;
				stmt.HidePayment=false;
				stmt.Intermingled=checkIntermingled.Checked;
				stmt.IsSent=false;
				stmt.Mode_=StatementMode.Mail;
				if(DefC.GetDef(DefCat.BillingTypes,agingList[i].BillingType).ItemValue=="E"){
					stmt.Mode_=StatementMode.Email;
				}
				stmt.Note=textNote.Text;
				stmt.NoteBold="";
				//appointment reminders are not handled here since it would be too slow.
				//set dunning messages here
				if(agingList[i].BalOver90>0){
					ageAccount=90;
				}
				else if(agingList[i].Bal_61_90>0){
					ageAccount=60;
				}
				else if(agingList[i].Bal_31_60>0){
					ageAccount=30;
				}
				else{
					ageAccount=0;
				}
				if(agingList[i].InsEst>0){
					insIsPending=YN.Yes;
				}
				else{
					insIsPending=YN.No;
				}
				dunning=Dunnings.GetDunning(dunList,agingList[i].BillingType,ageAccount,insIsPending);
				if(dunning!=null){
					if(stmt.Note!=""){
						stmt.Note+="\r\n\r\n";//leave one empty line
					}
					stmt.Note+=dunning.DunMessage;
					//if(stmt.Note!=""){//there will never be anything in NoteBold already
					//	stmt.Note+="\r\n\r\n";//leave one empty line
					//}
					stmt.NoteBold+=dunning.MessageBold;
				}
				stmt.PatNum=agingList[i].PatNum;
				stmt.SinglePatient=false;
				Statements.WriteObject(stmt);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

		

		

		

	}




}
