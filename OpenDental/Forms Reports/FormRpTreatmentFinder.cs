using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Printing;


namespace OpenDental{
///<summary></summary>
	public class FormRpTreatmentFinder:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;
		private Label label1;
		private CheckBox checkIncludeNoIns;
		private UI.ODGrid gridMain;
		private GroupBox groupBox1;
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
		private Label label4;
		private Label label3;
		private Label label5;
		private TextBox textCodeRange;
		private Label label6;
		private UI.Button butLabelSingle;
		private UI.Button butLabelPreview;
		private Label label7;
		private UI.Button butLettersPreview;
		private UI.Button buttonExport;
		private Label label8;
		private ComboBox comboMonthStart;
		private ValidDouble textOverAmount;
		private ComboBoxMulti comboBoxMultiProv;
		private ComboBoxMulti comboBoxMultiBilling;
		private int pagesPrinted;
		private int patientsPrinted;

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
			this.comboMonthStart = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textCodeRange = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.contextRightClick = new System.Windows.Forms.ContextMenu();
			this.menuItemFamily = new System.Windows.Forms.MenuItem();
			this.menuItemAccount = new System.Windows.Forms.MenuItem();
			this.buttonExport = new OpenDental.UI.Button();
			this.butLettersPreview = new OpenDental.UI.Button();
			this.butLabelSingle = new OpenDental.UI.Button();
			this.butLabelPreview = new OpenDental.UI.Button();
			this.butGotoAccount = new OpenDental.UI.Button();
			this.butGotoFamily = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.comboBoxMultiBilling = new OpenDental.UI.ComboBoxMulti();
			this.comboBoxMultiProv = new OpenDental.UI.ComboBoxMulti();
			this.textOverAmount = new OpenDental.ValidDouble();
			this.textDateStart = new OpenDental.ValidDate();
			this.butRefresh = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butCancel = new OpenDental.UI.Button();
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
			this.groupBox1.Controls.Add(this.comboBoxMultiBilling);
			this.groupBox1.Controls.Add(this.comboBoxMultiProv);
			this.groupBox1.Controls.Add(this.textOverAmount);
			this.groupBox1.Controls.Add(this.comboMonthStart);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textCodeRange);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label6);
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
			// comboMonthStart
			// 
			this.comboMonthStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboMonthStart.Items.AddRange(new object[] {
            "Calendar Year",
            "01 - January",
            "02 - February",
            "03 - March",
            "04 - April",
            "05 - May",
            "06 - June",
            "07 - July",
            "08 - August",
            "09 - September",
            "10 - October",
            "11 - November",
            "12 - December",
            "All"});
			this.comboMonthStart.Location = new System.Drawing.Point(342,32);
			this.comboMonthStart.MaxDropDownItems = 40;
			this.comboMonthStart.Name = "comboMonthStart";
			this.comboMonthStart.Size = new System.Drawing.Size(98,21);
			this.comboMonthStart.TabIndex = 47;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(13,36);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(140,14);
			this.label8.TabIndex = 46;
			this.label8.Text = "Amount remaining over";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(443,35);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(70,14);
			this.label7.TabIndex = 43;
			this.label7.Text = "Billing Type";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(233,36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(105,14);
			this.label3.TabIndex = 37;
			this.label3.Text = "Ins Month Start";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(443,14);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(70,14);
			this.label4.TabIndex = 35;
			this.label4.Text = "Provider";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			// buttonExport
			// 
			this.buttonExport.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonExport.Autosize = true;
			this.buttonExport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonExport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonExport.CornerRadius = 4F;
			this.buttonExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonExport.Location = new System.Drawing.Point(3,613);
			this.buttonExport.Name = "buttonExport";
			this.buttonExport.Size = new System.Drawing.Size(119,24);
			this.buttonExport.TabIndex = 72;
			this.buttonExport.Text = "Export to File";
			this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
			// 
			// butLettersPreview
			// 
			this.butLettersPreview.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLettersPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butLettersPreview.Autosize = true;
			this.butLettersPreview.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLettersPreview.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLettersPreview.CornerRadius = 4F;
			this.butLettersPreview.Image = global::OpenDental.Properties.Resources.butPreview;
			this.butLettersPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLettersPreview.Location = new System.Drawing.Point(3,587);
			this.butLettersPreview.Name = "butLettersPreview";
			this.butLettersPreview.Size = new System.Drawing.Size(119,24);
			this.butLettersPreview.TabIndex = 71;
			this.butLettersPreview.Text = "Letters Preview";
			this.butLettersPreview.Click += new System.EventHandler(this.butLettersPreview_Click);
			// 
			// butLabelSingle
			// 
			this.butLabelSingle.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLabelSingle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butLabelSingle.Autosize = true;
			this.butLabelSingle.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabelSingle.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabelSingle.CornerRadius = 4F;
			this.butLabelSingle.Image = global::OpenDental.Properties.Resources.butLabel;
			this.butLabelSingle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabelSingle.Location = new System.Drawing.Point(128,587);
			this.butLabelSingle.Name = "butLabelSingle";
			this.butLabelSingle.Size = new System.Drawing.Size(119,24);
			this.butLabelSingle.TabIndex = 70;
			this.butLabelSingle.Text = "Single Labels";
			this.butLabelSingle.Click += new System.EventHandler(this.butLabelSingle_Click);
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
			this.butLabelPreview.Location = new System.Drawing.Point(128,613);
			this.butLabelPreview.Name = "butLabelPreview";
			this.butLabelPreview.Size = new System.Drawing.Size(119,24);
			this.butLabelPreview.TabIndex = 69;
			this.butLabelPreview.Text = "Label Preview";
			this.butLabelPreview.Click += new System.EventHandler(this.butLabelPreview_Click);
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
			this.butGotoAccount.Location = new System.Drawing.Point(661,613);
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
			this.butGotoFamily.Location = new System.Drawing.Point(661,587);
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
			this.butPrint.Location = new System.Drawing.Point(418,613);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(87,24);
			this.butPrint.TabIndex = 34;
			this.butPrint.Text = "Print List";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// comboBoxMultiBilling
			// 
			this.comboBoxMultiBilling.BackColor = System.Drawing.SystemColors.Window;
			this.comboBoxMultiBilling.DroppedDown = false;
			this.comboBoxMultiBilling.Items = ((System.Collections.ArrayList)(resources.GetObject("comboBoxMultiBilling.Items")));
			this.comboBoxMultiBilling.Location = new System.Drawing.Point(513,32);
			this.comboBoxMultiBilling.Name = "comboBoxMultiBilling";
			this.comboBoxMultiBilling.SelectedIndices = ((System.Collections.ArrayList)(resources.GetObject("comboBoxMultiBilling.SelectedIndices")));
			this.comboBoxMultiBilling.Size = new System.Drawing.Size(160,21);
			this.comboBoxMultiBilling.TabIndex = 50;
			this.comboBoxMultiBilling.UseCommas = true;
			this.comboBoxMultiBilling.Leave += new System.EventHandler(this.comboBoxMultiBilling_Leave);
			// 
			// comboBoxMultiProv
			// 
			this.comboBoxMultiProv.BackColor = System.Drawing.SystemColors.Window;
			this.comboBoxMultiProv.DroppedDown = false;
			this.comboBoxMultiProv.Items = ((System.Collections.ArrayList)(resources.GetObject("comboBoxMultiProv.Items")));
			this.comboBoxMultiProv.Location = new System.Drawing.Point(513,10);
			this.comboBoxMultiProv.Name = "comboBoxMultiProv";
			this.comboBoxMultiProv.SelectedIndices = ((System.Collections.ArrayList)(resources.GetObject("comboBoxMultiProv.SelectedIndices")));
			this.comboBoxMultiProv.Size = new System.Drawing.Size(160,21);
			this.comboBoxMultiProv.TabIndex = 49;
			this.comboBoxMultiProv.UseCommas = true;
			this.comboBoxMultiProv.Leave += new System.EventHandler(this.comboBoxMultiProv_Leave);
			// 
			// textOverAmount
			// 
			this.textOverAmount.Location = new System.Drawing.Point(159,33);
			this.textOverAmount.Name = "textOverAmount";
			this.textOverAmount.Size = new System.Drawing.Size(68,20);
			this.textOverAmount.TabIndex = 48;
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
			this.butRefresh.Location = new System.Drawing.Point(342,55);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(98,24);
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
			this.gridMain.Size = new System.Drawing.Size(917,453);
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
			this.butCancel.Location = new System.Drawing.Point(844,613);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormRpTreatmentFinder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(923,641);
			this.Controls.Add(this.buttonExport);
			this.Controls.Add(this.butLettersPreview);
			this.Controls.Add(this.butLabelSingle);
			this.Controls.Add(this.butLabelPreview);
			this.Controls.Add(this.butGotoAccount);
			this.Controls.Add(this.butGotoFamily);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
			comboBoxMultiProv.Items.Add("All");
			for(int i=0;i<ProviderC.List.Length;i++){
			  comboBoxMultiProv.Items.Add(ProviderC.List[i].GetLongDesc());
			}
			comboBoxMultiProv.SetSelected(0,true);
			comboBoxMultiProv.RefreshText();
			comboBoxMultiBilling.Items.Add("All");
			for(int i=0;i<DefC.Short[(int)DefCat.BillingTypes].Length;i++){
				comboBoxMultiBilling.Items.Add(DefC.Short[(int)DefCat.BillingTypes][i].ItemName);
			}
			comboBoxMultiBilling.SetSelected(0,true);
			comboBoxMultiBilling.RefreshText();
			comboMonthStart.SelectedIndex=13;
			FillGrid();
		}

		private void FillGrid() {
			if(textDateStart.errorProvider1.GetError(textDateStart)!="") {
				return;
			}
			DateTime dateSince;
			if(textDateStart.Text.Trim()=="") {
				dateSince=DateTime.MinValue;
			}
			else {
				dateSince=PIn.Date(textDateStart.Text);
			}
			int monthStart=comboMonthStart.SelectedIndex;
			double aboveAmount;
			if(textOverAmount.errorProvider1.GetError(textOverAmount)!="") {
				return;
			}
			if(textOverAmount.Text.Trim()!="") {
				aboveAmount=PIn.Double(textOverAmount.Text);
			}
			else {
				aboveAmount=0;
			}
			ArrayList provFilter=new ArrayList();
			ArrayList billFilter=new ArrayList();
			if(comboBoxMultiProv.SelectedIndices[0].ToString()!="0") {
				provFilter=comboBoxMultiProv.SelectedIndices;
			}
			if(comboBoxMultiBilling.SelectedIndices[0].ToString()!="0") {
				billFilter=comboBoxMultiBilling.SelectedIndices;
			}
			string code1="";
			string code2="";
			if(textCodeRange.Text.Trim()!="") {
				if(textCodeRange.Text.Contains("-")) {
					string[] codeSplit=textCodeRange.Text.Split('-');
					code1=codeSplit[0].Trim();
					code2=codeSplit[1].Trim();
				}
				else {
					code1=textCodeRange.Text.Trim();
					code2=textCodeRange.Text.Trim();
				}
			}
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			//ODGridColumn col=new ODGridColumn(Lan.g("TableTreatmentFinder","PatNum"),100);
			//col.TextAlign=HorizontalAlignment.Center;
			//gridMain.Columns.Add(col);
			ODGridColumn col=new ODGridColumn(Lan.g("TableTreatmentFinder","LName"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTreatmentFinder","FName"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTreatmentFinder","Contact"),120);
			gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g("TableTreatmentFinder","address"),120);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g("TableTreatmentFinder","cityStateZip"),120);
			//gridMain.Columns.Add(col);
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
			Cursor=Cursors.WaitCursor;
			table=Patients.GetTreatmentFinderList(checkIncludeNoIns.Checked,monthStart,dateSince,aboveAmount,provFilter,billFilter,code1,code2);
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++) {
			  row=new ODGridRow();
				//Temporary filter just showing columns wanted. Probable it will become user defined.
			  for(int j=0;j<table.Columns.Count;j++) {
					if(j==1 || j==2 || j==3	|| j==6 || j==7 || j==8 || j==9) {
						row.Cells.Add(table.Rows[i][j].ToString());
					}
			  }
			  gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			Cursor=Cursors.Default;
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			GotoModule.GotoChart(PIn.Long(table.Rows[e.Row]["PatNum"].ToString()));
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//Might not need cellDoubleClick
		}

		private void comboBoxMultiProv_Leave(object sender,EventArgs e) {
			for(int i=0;i<comboBoxMultiProv.SelectedIndices.Count;i++) {
				if(comboBoxMultiProv.SelectedIndices[i].ToString()=="0") {
					comboBoxMultiProv.SelectedIndices.Clear();
					comboBoxMultiProv.SetSelected(0,true);
					comboBoxMultiProv.RefreshText();
				}
			}
			if(comboBoxMultiProv.SelectedIndices.Count==0) {
				comboBoxMultiProv.SelectedIndices.Clear();
				comboBoxMultiProv.SetSelected(0,true);
				comboBoxMultiProv.RefreshText();
			}
		}

		private void comboBoxMultiBilling_Leave(object sender,EventArgs e) {
			for(int i=0;i<comboBoxMultiBilling.SelectedIndices.Count;i++) {
				if(comboBoxMultiBilling.SelectedIndices[i].ToString()=="0") {
					comboBoxMultiBilling.SelectedIndices.Clear();
					comboBoxMultiBilling.SetSelected(0,true);
					comboBoxMultiBilling.RefreshText();
				}
			}
			if(comboBoxMultiBilling.SelectedIndices.Count==0) {
				comboBoxMultiBilling.SelectedIndices.Clear();
				comboBoxMultiBilling.SetSelected(0,true);
				comboBoxMultiBilling.RefreshText();
			}
		}

		private void butLettersPreview_Click(object sender,EventArgs e) {
			//Create letters. loop through each row and insert information into sheets,
			//take all the sheets and add to one giant pdf for preview.
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select patient(s) first.");
				return;
			}
			FormSheetPicker FormS=new FormSheetPicker();
			FormS.SheetType=SheetTypeEnum.PatientLetter;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			SheetDef sheetDef;
			Sheet sheet=null;
			for(int i=0;i<FormS.SelectedSheetDefs.Count;i++) {
				PdfDocument document=new PdfDocument();
				FormSheetFillEdit FormSF=null;
				PdfPage page=new PdfPage();
				string filePathAndName="";
				for(int j=0;j<gridMain.SelectedIndices.Length;j++) {
					page=document.AddPage();
					sheetDef=FormS.SelectedSheetDefs[i];
					sheet=SheetUtil.CreateSheet(sheetDef,PIn.Long(table.Rows[gridMain.SelectedIndices[j]]["PatNum"].ToString()));
					SheetParameter.SetParameter(sheet,"PatNum",PIn.Long(table.Rows[gridMain.SelectedIndices[j]]["PatNum"].ToString()));
					SheetFiller.FillFields(sheet);
					SheetUtil.CalculateHeights(sheet,this.CreateGraphics());
					FormSF=new FormSheetFillEdit(sheet);
					SheetPrinting.CreatePdfPage(sheet,page);
				}
				filePathAndName=Path.ChangeExtension(Path.GetTempFileName(),".pdf");
				document.Save(filePathAndName);
				Process.Start(filePathAndName);
				DialogResult=DialogResult.OK;
			}
		}

		private void butLabelSingle_Click(object sender,EventArgs e) {
		  if(gridMain.SelectedIndices.Length==0) {
		    MsgBox.Show(this,"Please select patient(s) first.");
		    return;
		  }
		  int patientsPrinted=0;
		  string text;
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				text="";
		    //print single label
		    text=table.Rows[gridMain.SelectedIndices[i]]["FName"].ToString()+" "
					+table.Rows[gridMain.SelectedIndices[i]]["LName"].ToString()+"\r\n";
		    text+=table.Rows[gridMain.SelectedIndices[i]]["address"].ToString()+"\r\n";
		    text+=table.Rows[gridMain.SelectedIndices[i]]["cityStZip"].ToString()+"\r\n";
		    LabelSingle.PrintText(0,text);
		    patientsPrinted++;
			}
		}

		private void butLabelPreview_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select patient(s) first.");
		    return;
			}
			pagesPrinted=0;
			patientsPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage+=new PrintPageEventHandler(this.pdLabels_PrintPage);
			pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			PrintPreview printPreview=new OpenDental.UI.PrintPreview(PrintSituation.LabelSheet
			  ,pd,(int)Math.Ceiling((double)gridMain.SelectedIndices.Length/30));
			printPreview.ShowDialog();
		}

		private void pdLabels_PrintPage(object sender, PrintPageEventArgs ev){
			int totalPages=(int)Math.Ceiling((double)gridMain.SelectedIndices.Length/30);
			Graphics g=ev.Graphics;
			float yPos=63;
			float xPos=50;
			string text="";
			while(yPos<1000 && patientsPrinted<gridMain.SelectedIndices.Length){
				text="";
				text=table.Rows[gridMain.SelectedIndices[patientsPrinted]]["FName"].ToString()+" "
					+table.Rows[gridMain.SelectedIndices[patientsPrinted]]["LName"].ToString()+"\r\n";
				text+=table.Rows[gridMain.SelectedIndices[patientsPrinted]]["address"].ToString()+"\r\n";
				text+=table.Rows[gridMain.SelectedIndices[patientsPrinted]]["cityStZip"].ToString()+"\r\n";
				g.DrawString(text,new Font(FontFamily.GenericSansSerif,11),Brushes.Black,xPos,yPos);
				//reposition for next label
				xPos+=275;
				if(xPos>850){//drop a line
					xPos=50;
					yPos+=100;
				}
				patientsPrinted++;
			}
			pagesPrinted++;
			if(pagesPrinted==totalPages){
				ev.HasMorePages=false;
				pagesPrinted=0;//because it has to print again from the print preview
				patientsPrinted=0;
			}
			else{
				ev.HasMorePages=true;
			}
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

		private void buttonExport_Click(object sender,EventArgs e) {
			SaveFileDialog saveFileDialog2=new SaveFileDialog();
      saveFileDialog2.AddExtension=true;
			saveFileDialog2.Title=Lan.g(this,"Treatment Finder");
			saveFileDialog2.FileName=Lan.g(this,"Treatment Finder");
			if(!Directory.Exists(PrefC.GetString(PrefName.ExportPath) )){
				try{
					Directory.CreateDirectory(PrefC.GetString(PrefName.ExportPath) );
					saveFileDialog2.InitialDirectory=PrefC.GetString(PrefName.ExportPath);
				}
				catch{
					//initialDirectory will be blank
				}
			}
			else saveFileDialog2.InitialDirectory=PrefC.GetString(PrefName.ExportPath);
			saveFileDialog2.Filter="Text files(*.txt)|*.txt|Excel Files(*.xls)|*.xls|All files(*.*)|*.*";
      saveFileDialog2.FilterIndex=0;
		  if(saveFileDialog2.ShowDialog()!=DialogResult.OK){
	   	  return;
			}
			try{
			  using(StreamWriter sw=new StreamWriter(saveFileDialog2.FileName,false))
				{
					String line="";  
					for(int i=0;i<gridMain.Columns.Count;i++){
						if(i>0) {
							line+="\t";
						}
					  line+=gridMain.Columns[i].Heading;
					}
					sw.WriteLine(line);
					string cell;
					for(int i=0;i<table.Rows.Count;i++){
					  line="";
					  for(int j=0;j<table.Columns.Count;j++){
							if(j>0) {
								line+="\t";
							}
					    cell=table.Rows[i][j].ToString();
					    cell=cell.Replace("\r","");
					    cell=cell.Replace("\n","");
					    cell=cell.Replace("\t","");
					    cell=cell.Replace("\"","");
					    line+=cell;
					  }
					  sw.WriteLine(line);
					}
				}
      }
      catch{
        MessageBox.Show(Lan.g(this,"File in use by another program.  Close and try again."));
				return;
			}
			MessageBox.Show(Lan.g(this,"File created successfully"));
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
			Close();
		}

		

		

		

		

		

		

		

		


		

		

		

		

		

		





	}
}
