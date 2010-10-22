using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;


namespace OpenDental{
///<summary></summary>
	public class FormRpTreatmentFinder:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.ComponentModel.Container components = null;
		private Label label1;
		private CheckBox checkIncludeNoIns;
		private UI.ODGrid gridMain;
		private GroupBox groupBox1;
		private FormQuery FormQuery2;
		private UI.Button butPrint;
		private UI.Button butRefresh;
		private DataTable table;
		private PrintDocument pd;
		private bool headingPrinted;
		private int headingPrintH;
		private ContextMenu contextRightClick;
		private MenuItem menuItemFamily;
		private MenuItem menuItemAccount;
		private UI.Button butGotoFamily;
		private UI.Button butGotoAccount;
		private ValidDate textDateStart;
		private Label label2;
		private ComboBox comboProv;
		private Label label4;
		private Label label3;
		private Label label5;
		private TextBox textCodeRange;
		private Label label6;
		private UI.Button butLabelSingle;
		private UI.Button butLabelPreview;
		private Label label7;
		private ComboBox comboBillingType;
		private UI.Button butPostcards;
		private UI.Button buttonExport;
		private Label label8;
		private CheckBox checkBox1;
		private ComboBox comboMonthStart;
		private ValidDouble textProcFee;
		private int pagesPrinted;

		///<summary></summary>
		public FormRpTreatmentFinder() {
			InitializeComponent();
			Lan.F(this);
			gridMain.ContextMenu=contextRightClick;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpTreatmentFinder));
			this.label1 = new System.Windows.Forms.Label();
			this.checkIncludeNoIns = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.contextRightClick = new System.Windows.Forms.ContextMenu();
			this.menuItemFamily = new System.Windows.Forms.MenuItem();
			this.menuItemAccount = new System.Windows.Forms.MenuItem();
			this.label2 = new System.Windows.Forms.Label();
			this.comboProv = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textCodeRange = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.comboBillingType = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.comboMonthStart = new System.Windows.Forms.ComboBox();
			this.buttonExport = new OpenDental.UI.Button();
			this.butPostcards = new OpenDental.UI.Button();
			this.butLabelSingle = new OpenDental.UI.Button();
			this.butLabelPreview = new OpenDental.UI.Button();
			this.butGotoAccount = new OpenDental.UI.Button();
			this.butGotoFamily = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.textDateStart = new OpenDental.ValidDate();
			this.butRefresh = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textProcFee = new OpenDental.ValidDouble();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(22,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(872,29);
			this.label1.TabIndex = 29;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// checkIncludeNoIns
			// 
			this.checkIncludeNoIns.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIncludeNoIns.Location = new System.Drawing.Point(13,14);
			this.checkIncludeNoIns.Name = "checkIncludeNoIns";
			this.checkIncludeNoIns.Size = new System.Drawing.Size(214,18);
			this.checkIncludeNoIns.TabIndex = 30;
			this.checkIncludeNoIns.Text = "Include patients without insurance";
			this.checkIncludeNoIns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIncludeNoIns.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textProcFee);
			this.groupBox1.Controls.Add(this.comboMonthStart);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.comboBillingType);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textCodeRange);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.comboProv);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textDateStart);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.butRefresh);
			this.groupBox1.Controls.Add(this.checkIncludeNoIns);
			this.groupBox1.Location = new System.Drawing.Point(3,41);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(916,83);
			this.groupBox1.TabIndex = 33;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "View";
			// 
			// contextRightClick
			// 
			this.contextRightClick.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFamily,
            this.menuItemAccount});
			// 
			// menuItemFamily
			// 
			this.menuItemFamily.Index = 0;
			this.menuItemFamily.Text = "See Family";
			this.menuItemFamily.Click += new System.EventHandler(this.menuItemFamily_Click);
			// 
			// menuItemAccount
			// 
			this.menuItemAccount.Index = 1;
			this.menuItemAccount.Text = "See Account";
			this.menuItemAccount.Click += new System.EventHandler(this.menuItemAccount_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(242,14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(119,14);
			this.label2.TabIndex = 33;
			this.label2.Text = "Proc Date Since";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboProv
			// 
			this.comboProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProv.Location = new System.Drawing.Point(513,11);
			this.comboProv.MaxDropDownItems = 40;
			this.comboProv.Name = "comboProv";
			this.comboProv.Size = new System.Drawing.Size(160,21);
			this.comboProv.TabIndex = 36;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(441,14);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(70,14);
			this.label4.TabIndex = 35;
			this.label4.Text = "Provider";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(248,36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88,14);
			this.label3.TabIndex = 37;
			this.label3.Text = "Month Start";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(758,34);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(150,13);
			this.label5.TabIndex = 41;
			this.label5.Text = "Ex: D1050-D1060";
			// 
			// textCodeRange
			// 
			this.textCodeRange.Location = new System.Drawing.Point(758,12);
			this.textCodeRange.Name = "textCodeRange";
			this.textCodeRange.Size = new System.Drawing.Size(150,20);
			this.textCodeRange.TabIndex = 39;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(679,12);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(77,17);
			this.label6.TabIndex = 40;
			this.label6.Text = "Code Range";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBillingType
			// 
			this.comboBillingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBillingType.Location = new System.Drawing.Point(513,32);
			this.comboBillingType.MaxDropDownItems = 40;
			this.comboBillingType.Name = "comboBillingType";
			this.comboBillingType.Size = new System.Drawing.Size(160,21);
			this.comboBillingType.TabIndex = 42;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(441,35);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(70,14);
			this.label7.TabIndex = 43;
			this.label7.Text = "Billing Type";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkBox1
			// 
			this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox1.Location = new System.Drawing.Point(13,35);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(214,18);
			this.checkBox1.TabIndex = 44;
			this.checkBox1.Text = "Patients with remaining insurance";
			this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(38,61);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(115,14);
			this.label8.TabIndex = 46;
			this.label8.Text = "Over Amount";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboMonthStart
			// 
			this.comboMonthStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboMonthStart.Items.AddRange(new object[] {
            "01 - January",
            "02 - February",
            "03 - March",
            "04 - April",
            "05 - June",
            "06 - July",
            "07 - August",
            "09 - September",
            "10 - October",
            "11 - November",
            "12 - December"});
			this.comboMonthStart.Location = new System.Drawing.Point(342,32);
			this.comboMonthStart.MaxDropDownItems = 40;
			this.comboMonthStart.Name = "comboMonthStart";
			this.comboMonthStart.Size = new System.Drawing.Size(98,21);
			this.comboMonthStart.TabIndex = 47;
			// 
			// buttonExport
			// 
			this.buttonExport.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonExport.Autosize = true;
			this.buttonExport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonExport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonExport.CornerRadius = 4F;
			this.buttonExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonExport.Location = new System.Drawing.Point(106,623);
			this.buttonExport.Name = "buttonExport";
			this.buttonExport.Size = new System.Drawing.Size(119,24);
			this.buttonExport.TabIndex = 72;
			this.buttonExport.Text = "Export to File";
			// 
			// butPostcards
			// 
			this.butPostcards.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPostcards.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPostcards.Autosize = true;
			this.butPostcards.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPostcards.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPostcards.CornerRadius = 4F;
			this.butPostcards.Image = global::OpenDental.Properties.Resources.butPreview;
			this.butPostcards.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPostcards.Location = new System.Drawing.Point(106,597);
			this.butPostcards.Name = "butPostcards";
			this.butPostcards.Size = new System.Drawing.Size(119,24);
			this.butPostcards.TabIndex = 71;
			this.butPostcards.Text = "Letters Preview";
			// 
			// butLabelSingle
			// 
			this.butLabelSingle.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLabelSingle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butLabelSingle.Autosize = true;
			this.butLabelSingle.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabelSingle.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabelSingle.CornerRadius = 4F;
			this.butLabelSingle.Image = global::OpenDental.Properties.Resources.butLabel;
			this.butLabelSingle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabelSingle.Location = new System.Drawing.Point(231,597);
			this.butLabelSingle.Name = "butLabelSingle";
			this.butLabelSingle.Size = new System.Drawing.Size(119,24);
			this.butLabelSingle.TabIndex = 70;
			this.butLabelSingle.Text = "Single Labels";
			// 
			// butLabelPreview
			// 
			this.butLabelPreview.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLabelPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butLabelPreview.Autosize = true;
			this.butLabelPreview.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabelPreview.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabelPreview.CornerRadius = 4F;
			this.butLabelPreview.Image = global::OpenDental.Properties.Resources.butLabel;
			this.butLabelPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabelPreview.Location = new System.Drawing.Point(231,623);
			this.butLabelPreview.Name = "butLabelPreview";
			this.butLabelPreview.Size = new System.Drawing.Size(119,24);
			this.butLabelPreview.TabIndex = 69;
			this.butLabelPreview.Text = "Label Preview";
			// 
			// butGotoAccount
			// 
			this.butGotoAccount.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butGotoAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butGotoAccount.Autosize = true;
			this.butGotoAccount.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butGotoAccount.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butGotoAccount.CornerRadius = 4F;
			this.butGotoAccount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butGotoAccount.Location = new System.Drawing.Point(516,623);
			this.butGotoAccount.Name = "butGotoAccount";
			this.butGotoAccount.Size = new System.Drawing.Size(96,24);
			this.butGotoAccount.TabIndex = 68;
			this.butGotoAccount.Text = "Go to Account";
			this.butGotoAccount.Click += new System.EventHandler(this.butGotoAccount_Click);
			// 
			// butGotoFamily
			// 
			this.butGotoFamily.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butGotoFamily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butGotoFamily.Autosize = true;
			this.butGotoFamily.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butGotoFamily.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butGotoFamily.CornerRadius = 4F;
			this.butGotoFamily.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butGotoFamily.Location = new System.Drawing.Point(516,597);
			this.butGotoFamily.Name = "butGotoFamily";
			this.butGotoFamily.Size = new System.Drawing.Size(96,24);
			this.butGotoFamily.TabIndex = 67;
			this.butGotoFamily.Text = "Go to Family";
			this.butGotoFamily.Click += new System.EventHandler(this.butGotoFamily_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(356,623);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(87,24);
			this.butPrint.TabIndex = 34;
			this.butPrint.Text = "Print List";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(363,11);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77,20);
			this.textDateStart.TabIndex = 34;
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(342,54);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(98,26);
			this.butRefresh.TabIndex = 32;
			this.butRefresh.Text = "&Refresh List";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = true;
			this.gridMain.Location = new System.Drawing.Point(3,130);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(917,463);
			this.gridMain.TabIndex = 31;
			this.gridMain.Title = "Treatment Finder";
			this.gridMain.TranslationName = "TableTreatmentFinder";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
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
			this.butCancel.Location = new System.Drawing.Point(844,623);
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
			this.butOK.Location = new System.Drawing.Point(356,597);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(87,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "R&un Query";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textProcFee
			// 
			this.textProcFee.Location = new System.Drawing.Point(159,58);
			this.textProcFee.Name = "textProcFee";
			this.textProcFee.Size = new System.Drawing.Size(68,20);
			this.textProcFee.TabIndex = 48;
			// 
			// FormRpTreatmentFinder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(923,651);
			this.Controls.Add(this.buttonExport);
			this.Controls.Add(this.butPostcards);
			this.Controls.Add(this.butLabelSingle);
			this.Controls.Add(this.butLabelPreview);
			this.Controls.Add(this.butGotoAccount);
			this.Controls.Add(this.butGotoFamily);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FormRpTreatmentFinder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Treatment Finder";
			this.Load += new System.EventHandler(this.FormRpTreatmentFinder_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpTreatmentFinder_Load(object sender, System.EventArgs e) {
			//DateTime today=DateTime.Today;
			//will start out 1st through 30th of previous month
			//date1.SelectionStart=new DateTime(today.Year,today.Month,1).AddMonths(-1);
			//date2.SelectionStart=new DateTime(today.Year,today.Month,1).AddDays(-1);
			comboMonthStart.SelectedIndex=0;
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableTreatmentFinder","PatNum"),100);
			col.TextAlign=HorizontalAlignment.Center;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTreatmentFinder","LName"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTreatmentFinder","FName"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTreatmentFinder","Annual Max"),100);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTreatmentFinder","Amount Used"),100);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTreatmentFinder","Amount Remaining"),140);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTreatmentFinder","Treatment Plan"),125);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			table=Patients.GetTreatmentFinderList(checkIncludeNoIns.Checked);
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++) {
			  row=new ODGridRow();
			  for(int j=0;j<table.Columns.Count;j++) {
			    row.Cells.Add(table.Rows[i][j].ToString());
			  }
			  gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			//Might not need the cellClick
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//Add functionality later
		}

		private void menuItemFamily_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.FamilyModule)) {
				return;
			}
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a patient first.");
				return;
			}
			long patNum=PIn.Long(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			GotoModule.GotoFamily(patNum);
		}

		private void menuItemAccount_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.AccountModule)) {
				return;
			}
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a patient first.");
				return;
			}
			long patNum=PIn.Long(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			GotoModule.GotoAccount(patNum);
		}

		private void butGotoFamily_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.FamilyModule)) {
				return;
			}
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a patient first.");
				return;
			}
			WindowState=FormWindowState.Minimized;
			long patNum=PIn.Long(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			GotoModule.GotoFamily(patNum);
		}

		private void butGotoAccount_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.AccountModule)) {
				return;
			}
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a patient first.");
				return;
			}
			WindowState=FormWindowState.Minimized;
			long patNum=PIn.Long(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			GotoModule.GotoAccount(patNum);
		}

		private void butPrint_Click(object sender,EventArgs e) {
			pagesPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(25,25,40,40);
			//pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Landscape=true;
			if(pd.DefaultPageSettings.PaperSize.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			headingPrinted=false;
			try {
				#if DEBUG
					FormRpPrintPreview pView = new FormRpPrintPreview();
					pView.printPreviewControl2.Document=pd;
					pView.ShowDialog();
				#else
					if(PrinterL.SetPrinter(pd,PrintSituation.Default)) {
						pd.Print();
					}
				#endif
			}
			catch {
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}
		
		private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=e.MarginBounds;
				//new Rectangle(50,40,800,1035);//Some printers can handle up to 1042
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			int yPos=bounds.Top;
			int center=bounds.X+bounds.Width/2;
			#region printHeading
			if(!headingPrinted) {
				text=Lan.g(this,"Treatment Finder");
				g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,headingFont).Height;
				if(checkIncludeNoIns.Checked) {
					text="Include patients without insurance";
				}
				else {
					text="Only patients with insurance";
				}
				g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=20;
				headingPrinted=true;
				headingPrintH=yPos;
			}
			#endregion
			int totalPages=gridMain.GetNumberOfPages(bounds,headingPrintH);
			yPos=gridMain.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			pagesPrinted++;
			if(pagesPrinted < totalPages) {
				e.HasMorePages=true;
			}
			else {
				e.HasMorePages=false;
			}
			g.Dispose();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			//DialogResult=DialogResult.Cancel;
			Close();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
      ReportSimpleGrid report=new ReportSimpleGrid();
      report.Query=@"
DROP TABLE IF EXISTS tempused;
DROP TABLE IF EXISTS tempplanned;
DROP TABLE IF EXISTS tempannualmax;

CREATE TABLE tempused(
PatPlanNum bigint unsigned NOT NULL,
AmtUsed double NOT NULL,
PRIMARY KEY (PatPlanNum));

CREATE TABLE tempplanned(
PatNum bigint unsigned NOT NULL,
AmtPlanned double NOT NULL,
PRIMARY KEY (PatNum));

CREATE TABLE tempannualmax(
PlanNum bigint unsigned NOT NULL,
AnnualMax double NOT NULL,
PRIMARY KEY (PlanNum));

INSERT INTO tempused
SELECT patplan.PatPlanNum,
SUM(IFNULL(claimproc.InsPayAmt,0))
FROM claimproc
LEFT JOIN patplan ON patplan.PatNum = claimproc.PatNum
AND patplan.PlanNum = claimproc.PlanNum
WHERE claimproc.Status IN (1, 3, 4)
AND claimproc.ProcDate BETWEEN makedate(year(curdate()), 1)
AND makedate(year(curdate())+1, 1) /*current calendar year*/
GROUP BY patplan.PatPlanNum;

INSERT INTO tempplanned
SELECT PatNum, SUM(ProcFee)
FROM procedurelog
WHERE ProcStatus = 1 /*treatment planned*/
GROUP BY PatNum;

INSERT INTO tempannualmax
SELECT benefit.PlanNum, benefit.MonetaryAmt
FROM benefit, covcat
WHERE covcat.CovCatNum = benefit.CovCatNum
AND benefit.BenefitType = 5 /* limitation */
AND (covcat.EbenefitCat=1 OR ISNULL(covcat.EbenefitCat))
AND benefit.MonetaryAmt <> 0
GROUP BY benefit.PlanNum
ORDER BY benefit.PlanNum;

SELECT patient.LName, patient.FName,
tempannualmax.AnnualMax $AnnualMax,
tempused.AmtUsed $AmountUsed,
tempannualmax.AnnualMax-IFNULL(tempused.AmtUsed,0) $AmtRemaining,
tempplanned.AmtPlanned $TreatmentPlan
FROM patient
LEFT JOIN tempplanned ON tempplanned.PatNum=patient.PatNum
LEFT JOIN patplan ON patient.PatNum=patplan.PatNum
LEFT JOIN tempused ON tempused.PatPlanNum=patplan.PatPlanNum
LEFT JOIN tempannualmax ON tempannualmax.PlanNum=patplan.PlanNum
	AND tempannualmax.AnnualMax>0
	/*AND tempannualmax.AnnualMax-tempused.AmtUsed>0*/
WHERE tempplanned.AmtPlanned>0 ";
      if(!checkIncludeNoIns.Checked){//if we don't want patients without insurance
        report.Query+="AND AnnualMax > 0 ";
      }
      report.Query+=@"
AND patient.PatStatus =0
ORDER BY tempplanned.AmtPlanned DESC;
DROP TABLE tempused;
DROP TABLE tempplanned;
DROP TABLE tempannualmax;";
      FormQuery2=new FormQuery(report);
      FormQuery2.textTitle.Text="Treatment Finder";
      //FormQuery2.IsReport=true;
      //FormQuery2.SubmitReportQuery();			
      FormQuery2.SubmitQuery();
      FormQuery2.ShowDialog();
			//DialogResult=DialogResult.OK;
		}

		

		

		

		

		





	}
}
