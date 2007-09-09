//using Excel;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.Threading;
using OpenDentBusiness;

namespace OpenDental{
///<summary>This is getting very outdated.  I realize it is difficult to use and will be phased out soon. The report displayed will be based on Queries.TableQ and Queries.CurReport.</summary>
	public class FormQuery : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.DataGrid grid2;
		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.GroupBox groupBox1;
		private OpenDental.UI.Button butSubmit;
		private System.Windows.Forms.RadioButton radioRaw;
		///<summary></summary>
		public System.Windows.Forms.RadioButton radioHuman;
		private OpenDental.UI.Button butFormulate;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private OpenDental.UI.Button butAdd;
		private DataGridTableStyle myGridTS;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog2;
		private System.Drawing.Printing.PrintDocument pd2;
		private bool totalsPrinted;
		private bool summaryPrinted;
		private int linesPrinted;
		private int pagesPrinted;
		///<summary></summary>
		public bool IsReport;
		private bool headerPrinted;
		private System.Windows.Forms.PrintPreviewControl printPreviewControl2;
		private bool tablePrinted;
		private System.Drawing.Font titleFont = new System.Drawing.Font("Arial",17,FontStyle.Bold);
		private System.Drawing.Font subtitleFont=new System.Drawing.Font("Arial",10,FontStyle.Bold);
		private System.Drawing.Font colCaptFont=new System.Drawing.Font("Arial",8,FontStyle.Bold);
		private System.Drawing.Font bodyFont = new System.Drawing.Font("Arial", 9);
		private OpenDental.UI.Button butFullPage;
		private System.Windows.Forms.Panel panelZoom;
		private System.Windows.Forms.Label labelTotPages;
		private System.Windows.Forms.Label label1;
		///<summary></summary>
		public System.Windows.Forms.TextBox textTitle;
		private System.Windows.Forms.SaveFileDialog saveFileDialog2;
		private OpenDental.UI.Button butCopy;
		private OpenDental.UI.Button butPaste;
		private OpenDental.UI.Button butZoomIn;
		private OpenDental.UI.Button butPrint;
		private OpenDental.UI.Button butExport;
		private OpenDental.UI.Button butQView;
		private OpenDental.UI.Button butPrintPreview;
		private OpenDental.UI.Button butBack;
		private OpenDental.UI.Button butFwd;
		private OpenDental.UI.Button butExportExcel;
		///<summary></summary>
		public OpenDental.ODtextBox textQuery;
		private int totalPages=0;
		private static Hashtable hListPlans;
		private UserQuery UserQueryCur;//never gets used.  It's a holdover.

		///<summary></summary>
		public FormQuery(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.F(this,new System.Windows.Forms.Control[] {
				//exclude:
				labelTotPages,
			});
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQuery));
			this.butClose = new OpenDental.UI.Button();
			this.grid2 = new System.Windows.Forms.DataGrid();
			this.panelTop = new System.Windows.Forms.Panel();
			this.textQuery = new OpenDental.ODtextBox();
			this.butExportExcel = new OpenDental.UI.Button();
			this.butPaste = new OpenDental.UI.Button();
			this.butCopy = new OpenDental.UI.Button();
			this.textTitle = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.UI.Button();
			this.butFormulate = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioHuman = new System.Windows.Forms.RadioButton();
			this.radioRaw = new System.Windows.Forms.RadioButton();
			this.butSubmit = new OpenDental.UI.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.printPreviewDialog2 = new System.Windows.Forms.PrintPreviewDialog();
			this.printPreviewControl2 = new System.Windows.Forms.PrintPreviewControl();
			this.butFullPage = new OpenDental.UI.Button();
			this.panelZoom = new System.Windows.Forms.Panel();
			this.labelTotPages = new System.Windows.Forms.Label();
			this.butZoomIn = new OpenDental.UI.Button();
			this.butBack = new OpenDental.UI.Button();
			this.butFwd = new OpenDental.UI.Button();
			this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
			this.butPrint = new OpenDental.UI.Button();
			this.butExport = new OpenDental.UI.Button();
			this.butQView = new OpenDental.UI.Button();
			this.butPrintPreview = new OpenDental.UI.Button();
			((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
			this.panelTop.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panelZoom.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(878,755);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,27);
			this.butClose.TabIndex = 5;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// grid2
			// 
			this.grid2.DataMember = "";
			this.grid2.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.grid2.Location = new System.Drawing.Point(0,150);
			this.grid2.Name = "grid2";
			this.grid2.ReadOnly = true;
			this.grid2.Size = new System.Drawing.Size(958,590);
			this.grid2.TabIndex = 1;
			// 
			// panelTop
			// 
			this.panelTop.Controls.Add(this.textQuery);
			this.panelTop.Controls.Add(this.butExportExcel);
			this.panelTop.Controls.Add(this.butPaste);
			this.panelTop.Controls.Add(this.butCopy);
			this.panelTop.Controls.Add(this.textTitle);
			this.panelTop.Controls.Add(this.label1);
			this.panelTop.Controls.Add(this.butAdd);
			this.panelTop.Controls.Add(this.butFormulate);
			this.panelTop.Controls.Add(this.groupBox1);
			this.panelTop.Controls.Add(this.butSubmit);
			this.panelTop.Location = new System.Drawing.Point(0,0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(956,104);
			this.panelTop.TabIndex = 2;
			// 
			// textQuery
			// 
			this.textQuery.AcceptsReturn = true;
			this.textQuery.Font = new System.Drawing.Font("Courier New",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textQuery.Location = new System.Drawing.Point(2,3);
			this.textQuery.Multiline = true;
			this.textQuery.Name = "textQuery";
			this.textQuery.QuickPasteType = OpenDentBusiness.QuickPasteType.Query;
			this.textQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textQuery.Size = new System.Drawing.Size(551,76);
			this.textQuery.TabIndex = 16;
			// 
			// butExportExcel
			// 
			this.butExportExcel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butExportExcel.Autosize = true;
			this.butExportExcel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butExportExcel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butExportExcel.CornerRadius = 4F;
			this.butExportExcel.Image = global::OpenDental.Properties.Resources.butExportExcel;
			this.butExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butExportExcel.Location = new System.Drawing.Point(712,69);
			this.butExportExcel.Name = "butExportExcel";
			this.butExportExcel.Size = new System.Drawing.Size(79,26);
			this.butExportExcel.TabIndex = 15;
			this.butExportExcel.Text = "Excel";
			this.butExportExcel.Visible = false;
			this.butExportExcel.Click += new System.EventHandler(this.butExportExcel_Click);
			// 
			// butPaste
			// 
			this.butPaste.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPaste.Autosize = true;
			this.butPaste.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPaste.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPaste.CornerRadius = 4F;
			this.butPaste.Image = global::OpenDental.Properties.Resources.butPaste;
			this.butPaste.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPaste.Location = new System.Drawing.Point(631,54);
			this.butPaste.Name = "butPaste";
			this.butPaste.Size = new System.Drawing.Size(65,23);
			this.butPaste.TabIndex = 11;
			this.butPaste.Text = "Paste";
			this.butPaste.Click += new System.EventHandler(this.butPaste_Click);
			// 
			// butCopy
			// 
			this.butCopy.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCopy.Autosize = true;
			this.butCopy.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCopy.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCopy.CornerRadius = 4F;
			this.butCopy.Image = global::OpenDental.Properties.Resources.butCopy;
			this.butCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCopy.Location = new System.Drawing.Point(556,54);
			this.butCopy.Name = "butCopy";
			this.butCopy.Size = new System.Drawing.Size(72,23);
			this.butCopy.TabIndex = 10;
			this.butCopy.Text = "Copy";
			this.butCopy.Click += new System.EventHandler(this.butCopy_Click);
			// 
			// textTitle
			// 
			this.textTitle.Location = new System.Drawing.Point(66,82);
			this.textTitle.Name = "textTitle";
			this.textTitle.Size = new System.Drawing.Size(219,20);
			this.textTitle.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7,84);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54,13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Title";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Location = new System.Drawing.Point(556,30);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(140,23);
			this.butAdd.TabIndex = 3;
			this.butAdd.Text = "Add To Favorites";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butFormulate
			// 
			this.butFormulate.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFormulate.Autosize = true;
			this.butFormulate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFormulate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFormulate.CornerRadius = 4F;
			this.butFormulate.Location = new System.Drawing.Point(556,6);
			this.butFormulate.Name = "butFormulate";
			this.butFormulate.Size = new System.Drawing.Size(140,23);
			this.butFormulate.TabIndex = 2;
			this.butFormulate.Text = "Favorites";
			this.butFormulate.Click += new System.EventHandler(this.butFormulate_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioHuman);
			this.groupBox1.Controls.Add(this.radioRaw);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(715,6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(122,58);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Format";
			// 
			// radioHuman
			// 
			this.radioHuman.Checked = true;
			this.radioHuman.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioHuman.Location = new System.Drawing.Point(10,16);
			this.radioHuman.Name = "radioHuman";
			this.radioHuman.Size = new System.Drawing.Size(108,16);
			this.radioHuman.TabIndex = 0;
			this.radioHuman.TabStop = true;
			this.radioHuman.Text = "Human-readable";
			this.radioHuman.Click += new System.EventHandler(this.radioHuman_Click);
			// 
			// radioRaw
			// 
			this.radioRaw.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioRaw.Location = new System.Drawing.Point(10,34);
			this.radioRaw.Name = "radioRaw";
			this.radioRaw.Size = new System.Drawing.Size(104,16);
			this.radioRaw.TabIndex = 1;
			this.radioRaw.Text = "Raw";
			this.radioRaw.Click += new System.EventHandler(this.radioRaw_Click);
			// 
			// butSubmit
			// 
			this.butSubmit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSubmit.Autosize = true;
			this.butSubmit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSubmit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSubmit.CornerRadius = 4F;
			this.butSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butSubmit.Location = new System.Drawing.Point(556,78);
			this.butSubmit.Name = "butSubmit";
			this.butSubmit.Size = new System.Drawing.Size(102,23);
			this.butSubmit.TabIndex = 6;
			this.butSubmit.Text = "&Submit Query";
			this.butSubmit.Click += new System.EventHandler(this.butSubmit_Click);
			// 
			// printPreviewDialog2
			// 
			this.printPreviewDialog2.AutoScrollMargin = new System.Drawing.Size(0,0);
			this.printPreviewDialog2.AutoScrollMinSize = new System.Drawing.Size(0,0);
			this.printPreviewDialog2.ClientSize = new System.Drawing.Size(400,300);
			this.printPreviewDialog2.Enabled = true;
			this.printPreviewDialog2.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog2.Icon")));
			this.printPreviewDialog2.Name = "printPreviewDialog2";
			this.printPreviewDialog2.Visible = false;
			// 
			// printPreviewControl2
			// 
			this.printPreviewControl2.AutoZoom = false;
			this.printPreviewControl2.Location = new System.Drawing.Point(18,136);
			this.printPreviewControl2.Name = "printPreviewControl2";
			this.printPreviewControl2.Size = new System.Drawing.Size(842,538);
			this.printPreviewControl2.TabIndex = 5;
			this.printPreviewControl2.Zoom = 1;
			// 
			// butFullPage
			// 
			this.butFullPage.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFullPage.Autosize = true;
			this.butFullPage.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFullPage.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFullPage.CornerRadius = 4F;
			this.butFullPage.Location = new System.Drawing.Point(9,5);
			this.butFullPage.Name = "butFullPage";
			this.butFullPage.Size = new System.Drawing.Size(75,27);
			this.butFullPage.TabIndex = 9;
			this.butFullPage.Text = "&Full Page";
			this.butFullPage.Visible = false;
			this.butFullPage.Click += new System.EventHandler(this.butFullPage_Click);
			// 
			// panelZoom
			// 
			this.panelZoom.Controls.Add(this.labelTotPages);
			this.panelZoom.Controls.Add(this.butFullPage);
			this.panelZoom.Controls.Add(this.butZoomIn);
			this.panelZoom.Controls.Add(this.butBack);
			this.panelZoom.Controls.Add(this.butFwd);
			this.panelZoom.Location = new System.Drawing.Point(336,746);
			this.panelZoom.Name = "panelZoom";
			this.panelZoom.Size = new System.Drawing.Size(229,37);
			this.panelZoom.TabIndex = 0;
			this.panelZoom.Visible = false;
			// 
			// labelTotPages
			// 
			this.labelTotPages.Font = new System.Drawing.Font("Microsoft Sans Serif",9F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelTotPages.Location = new System.Drawing.Point(145,10);
			this.labelTotPages.Name = "labelTotPages";
			this.labelTotPages.Size = new System.Drawing.Size(42,18);
			this.labelTotPages.TabIndex = 11;
			this.labelTotPages.Text = "1 / 2";
			this.labelTotPages.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butZoomIn
			// 
			this.butZoomIn.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butZoomIn.Autosize = true;
			this.butZoomIn.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butZoomIn.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butZoomIn.CornerRadius = 4F;
			this.butZoomIn.Image = global::OpenDental.Properties.Resources.butZoomIn;
			this.butZoomIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butZoomIn.Location = new System.Drawing.Point(9,5);
			this.butZoomIn.Name = "butZoomIn";
			this.butZoomIn.Size = new System.Drawing.Size(91,26);
			this.butZoomIn.TabIndex = 12;
			this.butZoomIn.Text = "Zoom In";
			this.butZoomIn.Click += new System.EventHandler(this.butZoomIn_Click);
			// 
			// butBack
			// 
			this.butBack.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBack.Autosize = true;
			this.butBack.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBack.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBack.CornerRadius = 4F;
			this.butBack.Image = global::OpenDental.Properties.Resources.Left;
			this.butBack.Location = new System.Drawing.Point(123,7);
			this.butBack.Name = "butBack";
			this.butBack.Size = new System.Drawing.Size(18,23);
			this.butBack.TabIndex = 17;
			this.butBack.Click += new System.EventHandler(this.butBack_Click);
			// 
			// butFwd
			// 
			this.butFwd.AdjustImageLocation = new System.Drawing.Point(1,0);
			this.butFwd.Autosize = true;
			this.butFwd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFwd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFwd.CornerRadius = 4F;
			this.butFwd.Image = global::OpenDental.Properties.Resources.Right;
			this.butFwd.Location = new System.Drawing.Point(195,7);
			this.butFwd.Name = "butFwd";
			this.butFwd.Size = new System.Drawing.Size(18,23);
			this.butFwd.TabIndex = 18;
			this.butFwd.Click += new System.EventHandler(this.butFwd_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(784,755);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(79,26);
			this.butPrint.TabIndex = 13;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butExport
			// 
			this.butExport.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butExport.Autosize = true;
			this.butExport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butExport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butExport.CornerRadius = 4F;
			this.butExport.Image = global::OpenDental.Properties.Resources.butExport;
			this.butExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butExport.Location = new System.Drawing.Point(689,755);
			this.butExport.Name = "butExport";
			this.butExport.Size = new System.Drawing.Size(79,26);
			this.butExport.TabIndex = 14;
			this.butExport.Text = "&Export";
			this.butExport.Click += new System.EventHandler(this.butExport_Click);
			// 
			// butQView
			// 
			this.butQView.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butQView.Autosize = true;
			this.butQView.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butQView.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butQView.CornerRadius = 4F;
			this.butQView.Image = global::OpenDental.Properties.Resources.butQView;
			this.butQView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butQView.Location = new System.Drawing.Point(573,741);
			this.butQView.Name = "butQView";
			this.butQView.Size = new System.Drawing.Size(104,26);
			this.butQView.TabIndex = 15;
			this.butQView.Text = "&Query View";
			this.butQView.Click += new System.EventHandler(this.butQView_Click);
			// 
			// butPrintPreview
			// 
			this.butPrintPreview.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrintPreview.Autosize = true;
			this.butPrintPreview.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrintPreview.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrintPreview.CornerRadius = 4F;
			this.butPrintPreview.Image = global::OpenDental.Properties.Resources.butPreview;
			this.butPrintPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrintPreview.Location = new System.Drawing.Point(572,755);
			this.butPrintPreview.Name = "butPrintPreview";
			this.butPrintPreview.Size = new System.Drawing.Size(113,26);
			this.butPrintPreview.TabIndex = 16;
			this.butPrintPreview.Text = "P&rint Preview";
			this.butPrintPreview.Click += new System.EventHandler(this.butPrintPreview_Click);
			// 
			// FormQuery
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(963,788);
			this.Controls.Add(this.butPrintPreview);
			this.Controls.Add(this.butQView);
			this.Controls.Add(this.butExport);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.panelZoom);
			this.Controls.Add(this.printPreviewControl2);
			this.Controls.Add(this.grid2);
			this.Controls.Add(this.panelTop);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormQuery";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Query";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormQuery_Layout);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormQuery_Closing);
			this.Load += new System.EventHandler(this.FormQuery_Load);
			((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.panelZoom.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormQuery_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			printPreviewControl2.Location=new System.Drawing.Point(0,0);
			printPreviewControl2.Height=ClientSize.Height-39;
			printPreviewControl2.Width=ClientSize.Width;	
			panelTop.Width=ClientSize.Width;
			grid2.Location=new System.Drawing.Point(2,panelTop.Height);
			grid2.Height=ClientSize.Height-grid2.Location.Y-39;
			grid2.Width=ClientSize.Width-2;
			butClose.Location=new System.Drawing.Point(ClientSize.Width-90,ClientSize.Height-34);
			butExport.Location=new System.Drawing.Point(ClientSize.Width-180,ClientSize.Height-34);
			butPrint.Location=new System.Drawing.Point(ClientSize.Width-270,ClientSize.Height-34);
			butPrintPreview.Location=new System.Drawing.Point(ClientSize.Width-385,ClientSize.Height-34);
			butQView.Location=new System.Drawing.Point(ClientSize.Width-385,ClientSize.Height-34);
			panelZoom.Location=new System.Drawing.Point(ClientSize.Width-620,ClientSize.Height-38);
		}

		private void FormQuery_Load(object sender, System.EventArgs e) {
			//Queries.TableQ=null;//this will crash the program
			grid2.Font=bodyFont;
			if(IsReport){
				printPreviewControl2.Visible=true;
				Text="Report";
				butPrintPreview.Visible=false;
				panelZoom.Visible=true;
				PrintReport(true);
				labelTotPages.Text="/ "+totalPages.ToString();
				if(PrefB.GetBool("FuchsOptionsOn")) {
					butFullPage.Visible = true;
					butZoomIn.Visible = false;
					printPreviewControl2.Zoom = 1;
				}
				else {
					printPreviewControl2.Zoom = ((double)printPreviewControl2.ClientSize.Height
					/ (double)pd2.DefaultPageSettings.PaperSize.Height);
				}
            }
			else{
				printPreviewControl2.Visible=false;
				Text=Lan.g(this,"Query");
			}
		}

		private void butSubmit_Click(object sender, System.EventArgs e) {	
			Queries.CurReport=new ReportOld();
			Queries.CurReport.Query=textQuery.Text;
			SubmitQuery();
		}

		///<summary>This is used internally instead of SubmitReportQuery.  Can also be called externally if we want to automate a userquery.  Column names will be handled automatically.</summary>
		public void SubmitQuery(){
			Patients.GetHList();
      //hListPlans=InsPlans.GetHListAll();
			
			Queries.SubmitCur();
			/* (for later if more complex queries with loops:)
			//SubmitQueryThread();
			//Thread Thread2 = new Thread(new ThreadStart(SubmitQueryThread));
			//Thread2.Start();
      //FormProcessWaiting fpw = new FormProcessWaiting();
			//while(Thread2.ThreadState!=ThreadState.Stopped)				//{
			//	;
			//			if(!fpw.Created)  {
			//		    fpw.ShowDialog();
			//			}
			//			if(fpw.DialogResult==DialogResult.Abort)  {
			//				Thread2.Suspend();
			//		  break;
			//		}
			//		}
		  //  fpw.Close();
			//Thread2.Abort();
			//ThreadState.
			//if(MessageBox.Show("Waiting for Server","",MessageBoxButtons.
			//Wait for dialog result
			//If abort, then skip the rest of this.*/
			Queries.CurReport.ColWidth=new int[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColPos=new int[Queries.TableQ.Columns.Count+1];
			Queries.CurReport.ColPos[0]=0;
			Queries.CurReport.ColCaption=new string[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColAlign=new HorizontalAlignment[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColTotal=new double[Queries.TableQ.Columns.Count];
			grid2.TableStyles.Clear();
			grid2.SetDataBinding(Queries.TableQ,"");
			myGridTS = new DataGridTableStyle();
			grid2.TableStyles.Add(myGridTS);
			if(radioHuman.Checked){
				Queries.TableQ=MakeReadable(Queries.TableQ);
				grid2.SetDataBinding(Queries.TableQ,"");
			}
			//if(!IsReport){
				AutoSizeColumns();
				/*for(int i=0;i<doubleCount;i++){
					int colTotal=0;
					for(int iRow=0;iRow<Queries.TableQ.Rows.Count;i++){
						
					}
					Queries.CurReport.Summary[i]="TOTAL :"+;
				}*/
				Queries.CurReport.Title=textTitle.Text;
				Queries.CurReport.SubTitle=new string[1];
				Queries.CurReport.SubTitle[0]=((Pref)PrefB.HList["PracticeTitle"]).ValueString;
				for(int iCol=0;iCol<Queries.TableQ.Columns.Count;iCol++){
					Queries.CurReport.ColCaption[iCol]=Queries.TableQ.Columns[iCol].Caption;//myGridTS.GridColumnStyles[iCol].HeaderText;
					myGridTS.GridColumnStyles[iCol].Alignment=Queries.CurReport.ColAlign[iCol];
				}
				Queries.CurReport.Summary=new string[0];
			//}		
		}

		//private void SubmitQueryThread(){
			//Queries.SubmitCur();
		//}

		///<summary>When used as a report, this is called externally. Used instead of SubmitQuery(). Column names will be handled manually by the external form calling this report.</summary>
		public void SubmitReportQuery(){	
			Queries.SubmitCur();
			Queries.CurReport.ColWidth=new int[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColPos=new int[Queries.TableQ.Columns.Count+1];
			Queries.CurReport.ColPos[0]=0;
			Queries.CurReport.ColCaption=new string[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColAlign=new HorizontalAlignment[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColTotal=new double[Queries.TableQ.Columns.Count];
			grid2.TableStyles.Clear();
			grid2.SetDataBinding(Queries.TableQ,"");
			myGridTS = new DataGridTableStyle();
			grid2.TableStyles.Add(myGridTS);
			Queries.TableQ=MakeReadable(Queries.TableQ);//?
			grid2.SetDataBinding(Queries.TableQ,"");//because MakeReadable trashes the TableQ
		}

		///<summary></summary>
		public void ResetGrid(){
			grid2.TableStyles.Clear();
			grid2.SetDataBinding(Queries.TableQ,"");
			myGridTS = new DataGridTableStyle();
			grid2.TableStyles.Add(myGridTS);
		}

		private void AutoSizeColumns(){
			Graphics grfx=this.CreateGraphics();
			//int colWidth;
			int tempWidth;
			//for(int i=0;i<myGridTS.GridColumnStyles.Count;i++){
			for(int i=0;i<Queries.CurReport.ColWidth.Length;i++){
				Queries.CurReport.ColWidth[i]
					=(int)grfx.MeasureString(Queries.TableQ.Columns[i].Caption,grid2.Font).Width;
					//myGridTS.GridColumnStyles[i].HeaderText,grid2.Font).Width;
				for(int j=0;j<Queries.TableQ.Rows.Count;j++){
					tempWidth=(int)grfx.MeasureString(Queries.TableQ.Rows[j][i].ToString(),grid2.Font).Width;
					if(tempWidth>Queries.CurReport.ColWidth[i])
						Queries.CurReport.ColWidth[i]=tempWidth;
				}
				if(Queries.CurReport.ColWidth[i]>400) Queries.CurReport.ColWidth[i]=400;
				myGridTS.GridColumnStyles[i].Width=Queries.CurReport.ColWidth[i]+12;
				Queries.CurReport.ColWidth[i]+=6;//so the columns don't touch
				Queries.CurReport.ColPos[i+1]=Queries.CurReport.ColPos[i]+Queries.CurReport.ColWidth[i];
			}
		}

		///<summary>Starting to use this externally as well.</summary>
		public static DataTable MakeReadable(DataTable tableIn){
			//this can probably be improved upon later for speed
			if(hListPlans==null){
				hListPlans=InsPlans.GetHListAll();
			}
			DataTable tableOut=tableIn.Clone();//copies just the structure
			for(int j=0;j<tableOut.Columns.Count;j++){
				tableOut.Columns[j].DataType=typeof(string);
			}
			DataRow thisRow;
			//copy data from tableInput to tableOutput while converting to strings
			for(int i=0;i<tableIn.Rows.Count;i++){
				thisRow=tableOut.NewRow();//new row with new schema
				for(int j=0;j<tableIn.Columns.Count;j++){
					thisRow[j]=tableIn.Rows[i][j].ToString();
				}
				tableOut.Rows.Add(thisRow);
			}
			for(int j=0;j<tableOut.Columns.Count;j++){
				for(int i=0;i<tableOut.Rows.Count;i++){
					try{
					if(tableOut.Columns[j].Caption.Substring(0,1)=="$"){
						tableOut.Rows[i][j]=PIn.PDouble(tableOut.Rows[i][j].ToString()).ToString("F");
						Queries.CurReport.ColAlign[j]=HorizontalAlignment.Right;
						Queries.CurReport.ColTotal[j]+=PIn.PDouble(tableOut.Rows[i][j].ToString());
					}
					else switch(tableOut.Columns[j].Caption.ToLower())
					{
						//bool
						case "isprosthesis":
						case "ispreventive":
						case "ishidden":
						case "isrecall":
						case "usedefaultfee":
						case "usedefaultcov":
						case "isdiscount":
						case "removetooth":
						case "setrecall":
						case "nobillins":
						case "isprosth":
						case "ishygiene":
						case "issecondary":
						case "orpribool":
						case "orsecbool":
						case "issplit":
  					case "ispreauth":
 					  case "isortho":
            case "releaseinfo":
            case "assignben":
            case "enabled":
            case "issystem":
            case "usingtin":
            case "sigonfile": 
            case "notperson":
            case "isfrom":
							tableOut.Rows[i][j]=PIn.PBool(tableOut.Rows[i][j].ToString()).ToString();
							break;
						//date
						case "adjdate":
						case "baldate":
						case "dateservice":
						case "datesent":
						case "datereceived":
						case "priordate":
						case "date":
						case "dateviewing":
						case "datecreated":
						case "dateeffective":
						case "dateterm":
						case "paydate":
						case "procdate":
						case "rxdate":
						case "birthdate":
						case "monthyear":
            case "accidentdate":
						case "orthodate":
            case "checkdate":
						case "screendate":
						case "datedue":
						case "dateduecalc":
						case "mydate"://this is a workaround for the daily payment report
							tableOut.Rows[i][j]=PIn.PDate(tableOut.Rows[i][j].ToString()).ToString("d");
							break;
            //time 
						case "aptdatetime":
            case "starttime":
            case "stoptime":
							tableOut.Rows[i][j]=PIn.PDateT(tableOut.Rows[i][j].ToString()).ToString("t")+"   "
								+PIn.PDateT(tableOut.Rows[i][j].ToString()).ToString("d");
							break;
  					//double
						case "adjamt":
						case "monthbalance":
						case "claimfee":
						case "inspayest":
						case "inspayamt":
						case "dedapplied":
						case "amount":
						case "payamt":
						case "splitamt":
						case "balance":
						case "procfee":
						case "overridepri":
						case "overridesec":
						case "priestim":
						case "secestim":
						case "procfees":
						case "claimpays":
						case "insest":
						case "paysplits":
						case "adjustments":
						case "bal_0_30":
						case "bal_31_60":
						case "bal_61_90":
						case "balover90":
						case "baltotal":
							tableOut.Rows[i][j]=PIn.PDouble(tableOut.Rows[i][j].ToString()).ToString("F");
							Queries.CurReport.ColAlign[j]=HorizontalAlignment.Right;
							//myGridTS.GridColumnStyles[j].Alignment=HorizontalAlignment.Right;
							Queries.CurReport.ColTotal[j]+=PIn.PDouble(tableOut.Rows[i][j].ToString());
							break;
						//definitions:
						case "adjtype":
							tableOut.Rows[i][j]
								=DefB.GetName(DefCat.AdjTypes,PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;
						case "confirmed":
							tableOut.Rows[i][j]
								=DefB.GetValue(DefCat.ApptConfirmed,PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;
						//case "claimformat":
						//	tableOut.Rows[i][j]
						//		=DefB.GetName(DefCat.ClaimFormats,PIn.PInt(tableOut.Rows[i][j].ToString()));
						//	break;
						case "dx":
							tableOut.Rows[i][j]
								=DefB.GetName(DefCat.Diagnosis,PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;
						case "discounttype":
							tableOut.Rows[i][j]
								=DefB.GetName(DefCat.DiscountTypes,PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;
						case "doccategory":
							tableOut.Rows[i][j]
								=DefB.GetName(DefCat.ImageCats,PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;
						case "feesched":
							tableOut.Rows[i][j]
								=DefB.GetName(DefCat.FeeSchedNames,PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;
						case "op":
							tableOut.Rows[i][j]
								=Operatories.GetAbbrev(PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;
						case "paytype":
							tableOut.Rows[i][j]
								=DefB.GetName(DefCat.PaymentTypes,PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;
						case "proccat":
							tableOut.Rows[i][j]
								=DefB.GetName(DefCat.ProcCodeCats,PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;
						case "unschedstatus":
						case "recallstatus":
							tableOut.Rows[i][j]
								=DefB.GetName(DefCat.RecallUnschedStatus,PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;
						case "billingtype":
							tableOut.Rows[i][j]
								=DefB.GetName(DefCat.BillingTypes,PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;
						//patnums:
						case "patnum":
						case "guarantor":
						case "pripatnum":
						case "secpatnum":
						case "subscriber":
            case "withpat":
							 
							if(Patients.HList.ContainsKey(PIn.PInt(tableOut.Rows[i][j].ToString()))){
								//MessageBox.Show((string)Patients.HList[PIn.PInt(tableOut.Rows[i][j].ToString())]);
								tableOut.Rows[i][j]=Patients.HList[PIn.PInt(tableOut.Rows[i][j].ToString())];
							}
							else
								tableOut.Rows[i][j]="";
							break;
            //plannums:        
            case "plannum":
            case "priplannum":
            case "secplannum": 
							if(hListPlans.ContainsKey(PIn.PInt(tableOut.Rows[i][j].ToString())))
								tableOut.Rows[i][j]=hListPlans[PIn.PInt(tableOut.Rows[i][j].ToString())];
							else
								tableOut.Rows[i][j]="";
							break;
            //referralnum             
            case "referralnum":
							if(PIn.PInt(tableOut.Rows[i][j].ToString())!=0){
								Referral referral=Referrals.GetReferral
									(PIn.PInt(tableOut.Rows[i][j].ToString()));
								tableOut.Rows[i][j]
									=referral.LName+", "+referral.FName+" "+referral.MName;
							}
							else
								tableOut.Rows[i][j]="";
							break; 
						//enumerations:
						case "aptstatus":
							tableOut.Rows[i][j]
								=((ApptStatus)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "category":
							tableOut.Rows[i][j]=((DefCat)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "renewmonth":
							tableOut.Rows[i][j]=((Month)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "patstatus":
							tableOut.Rows[i][j]
								=((PatientStatus)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "gender":
							tableOut.Rows[i][j]
								=((PatientGender)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						//case "lab":
						//	tableOut.Rows[i][j]
						//		=((LabCaseOld)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
						//  break;
						case "position":
							tableOut.Rows[i][j]
								=((PatientPosition)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "deductwaivprev":
						case "flocovered":
						case "misstoothexcl":
						case "procstatus":
							tableOut.Rows[i][j]=((ProcStat)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "majorwait":
						case "hascaries":
						case "needssealants":
						case "cariesexperience":
						case "earlychildcaries":
						case "existingsealants":
						case "missingallteeth":
							tableOut.Rows[i][j]=((YN)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "prirelationship":
						case "secrelationship":
							tableOut.Rows[i][j]=((Relat)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "treatarea":
							tableOut.Rows[i][j]
								=((TreatmentArea)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "specialty":
							tableOut.Rows[i][j]
								=((DentalSpecialty)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "placeservice":
							tableOut.Rows[i][j]
								=((PlaceOfService)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
            case "employrelated": 
							tableOut.Rows[i][j]
								=((YN)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
            case "schedtype": 
							tableOut.Rows[i][j]
								=((ScheduleType)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
            case "dayofweek": 
							tableOut.Rows[i][j]
								=((DayOfWeek)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "race":
							tableOut.Rows[i][j]
								=((PatientRace)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "gradelevel":
							tableOut.Rows[i][j]
								=((PatientGrade)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						case "urgency":
							tableOut.Rows[i][j]
								=((TreatmentUrgency)PIn.PInt(tableOut.Rows[i][j].ToString())).ToString();
							break;
						//miscellaneous:
						case "provnum":
						case "provhyg":
						case "priprov":
						case "secprov":
            case "provtreat":
            case "provbill":   
							tableOut.Rows[i][j]=Providers.GetAbbr(PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;

						case "covcatnum":
							tableOut.Rows[i][j]=CovCats.GetDesc(PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;
            case "referringprov": 
	//					  tableOut.Rows[i][j]=CovCats.GetDesc(PIn.PInt(tableOut.Rows[i][j].ToString()));
							break;			
            case "addtime":
							if(tableOut.Rows[i][j].ToString()!="0")
								tableOut.Rows[i][j]+="0";
							break;
					}//end switch column caption
					}//end try
					catch{
						//return tableOut;
					}
				}//end for i rows
			}//end for j cols
			return tableOut;
		}

		private void butFormulate_Click(object sender, System.EventArgs e) {//is now the 'Favorites' button
			FormQueryFormulate FormQF=new FormQueryFormulate();
			FormQF.UserQueryCur=UserQueryCur;
			FormQF.ShowDialog();
			if(FormQF.DialogResult==DialogResult.OK){
				textQuery.Text=FormQF.UserQueryCur.QueryText;
				//grid2.CaptionText=UserQueries.Cur.Description;
				textTitle.Text=FormQF.UserQueryCur.Description;
				UserQueryCur=FormQF.UserQueryCur;
				SubmitQuery();
				//this.butSaveChanges.Enabled=true;
			}
			else{
				//butSaveChanges.Enabled=false;
			}
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormQueryEdit FormQE=new FormQueryEdit();
			FormQE.UserQueryCur=new UserQuery();
			FormQE.UserQueryCur.QueryText=textQuery.Text;
			FormQE.IsNew=true;
			FormQE.ShowDialog();
			if(FormQE.DialogResult==DialogResult.OK){
				textQuery.Text=FormQE.UserQueryCur.QueryText;
				grid2.CaptionText=FormQE.UserQueryCur.Description;
			}
		}

		private void radioRaw_Click(object sender, System.EventArgs e) {
			SubmitQuery();
		}

		private void radioHuman_Click(object sender, System.EventArgs e) {
			SubmitQuery();
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			if(Queries.TableQ==null){
				MessageBox.Show(Lan.g(this,"Please run query first"));
				return;
			}
			PrintReport(false);
			if(IsReport){
				DialogResult=DialogResult.Cancel;
			}
		}

		private void butPrintPreview_Click(object sender, System.EventArgs e) {
			if(Queries.TableQ==null){
				MessageBox.Show(Lan.g(this,"Please run query first"));
				return;
			}
			butFullPage.Visible=false;
			butZoomIn.Visible=true;
			printPreviewControl2.Visible=true;
			butPrintPreview.Visible=false;
			butQView.Visible=true;
			panelZoom.Visible=true;
			totalPages=0;
			printPreviewControl2.Zoom=((double)printPreviewControl2.ClientSize.Height
				/(double)pd2.DefaultPageSettings.PaperSize.Height);
			PrintReport(true);
			labelTotPages.Text="/ "+totalPages.ToString();
		}

		private void butQView_Click(object sender, System.EventArgs e) {
			printPreviewControl2.Visible=false;
			panelZoom.Visible=false;
			butPrintPreview.Visible=true;
			butQView.Visible=false;
		}

		///<summary></summary>
		public void PrintReport(bool justPreview){
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.DefaultPageSettings.Margins=new Margins(10,50,50,60);
			if(pd2.DefaultPageSettings.PaperSize.Height==0){
				pd2.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			pagesPrinted=0;
			linesPrinted=0;
			try{
				if(justPreview){
					printPreviewControl2.Document=pd2;
				}
				else if(Printers.SetPrinter(pd2,PrintSituation.Default)){
					pd2.Print();
				}
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}
		
		///<summary>raised for each page to be printed.</summary>
		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){
			Rectangle bounds=ev.MarginBounds;
			float yPos = bounds.Top;
			if(!headerPrinted){
				ev.Graphics.DrawString(Queries.CurReport.Title
					,titleFont,Brushes.Black
					,bounds.Width/2
					-ev.Graphics.MeasureString(Queries.CurReport.Title,titleFont).Width/2,yPos);
				yPos+=titleFont.GetHeight(ev.Graphics);
				for(int i=0;i<Queries.CurReport.SubTitle.Length;i++){
					ev.Graphics.DrawString(Queries.CurReport.SubTitle[i]
						,subtitleFont,Brushes.Black
						,bounds.Width/2
						-ev.Graphics.MeasureString(Queries.CurReport.SubTitle[i],subtitleFont).Width/2,yPos);
					yPos+=subtitleFont.GetHeight(ev.Graphics)+2;
				}
				headerPrinted=true;
			}
			yPos+=10;
			ev.Graphics.DrawString(Lan.g(this,"Date:")+" "+DateTime.Today.ToString("d")
				,bodyFont,Brushes.Black,bounds.Left,yPos);
			//if(totalPages==0){
			ev.Graphics.DrawString(Lan.g(this,"Page:")+" "+(pagesPrinted+1).ToString()
				,bodyFont,Brushes.Black
				,bounds.Right
				-ev.Graphics.MeasureString(Lan.g(this,"Page:")+" "+(pagesPrinted+1).ToString()
				,bodyFont).Width,yPos);
			/*}
			else{//maybe work on this later.  Need totalPages on first pass
				ev.Graphics.DrawString("Page: "+(pagesPrinted+1).ToString()+" / "+totalPages.ToString()
					,bodyFont,Brushes.Black
					,bounds.Right
					-ev.Graphics.MeasureString("Page: "+(pagesPrinted+1).ToString()+" / "
					+totalPages.ToString(),bodyFont).Width
					,yPos);
			}*/
			yPos+=bodyFont.GetHeight(ev.Graphics)+10;
			ev.Graphics.DrawLine(new Pen(Color.Black),bounds.Left,yPos-5,bounds.Right,yPos-5);
			//column captions:
			for(int i=0;i<Queries.CurReport.ColCaption.Length;i++){
				if(Queries.CurReport.ColAlign[i]==HorizontalAlignment.Right){
					ev.Graphics.DrawString(Queries.CurReport.ColCaption[i]
						,colCaptFont,Brushes.Black,new RectangleF(
						bounds.Left+Queries.CurReport.ColPos[i+1]
						-ev.Graphics.MeasureString(Queries.CurReport.ColCaption[i],colCaptFont).Width,yPos
						,Queries.CurReport.ColWidth[i],colCaptFont.GetHeight(ev.Graphics)));
				}
				else{
					ev.Graphics.DrawString(Queries.CurReport.ColCaption[i]
						,colCaptFont,Brushes.Black,bounds.Left+Queries.CurReport.ColPos[i],yPos);
				}
			}
			yPos+=bodyFont.GetHeight(ev.Graphics)+5;
			//table:
			while(yPos<bounds.Top+bounds.Height-18//The 18 is allowance for the line about to print. 
				&& linesPrinted < Queries.TableQ.Rows.Count)
			{
				for(int iCol=0;iCol<Queries.TableQ.Columns.Count;iCol++){
					if(Queries.CurReport.ColAlign[iCol]==HorizontalAlignment.Right){
						ev.Graphics.DrawString(grid2[linesPrinted,iCol].ToString()
							,bodyFont,Brushes.Black,new RectangleF(
							bounds.Left+Queries.CurReport.ColPos[iCol+1]
							-ev.Graphics.MeasureString(grid2[linesPrinted,iCol].ToString(),bodyFont).Width-1,yPos
							,Queries.CurReport.ColWidth[iCol],bodyFont.GetHeight(ev.Graphics)));
					}
					else{
						ev.Graphics.DrawString(grid2[linesPrinted,iCol].ToString()
							,bodyFont,Brushes.Black,new RectangleF(
							bounds.Left+Queries.CurReport.ColPos[iCol],yPos
							,Queries.CurReport.ColPos[iCol+1]-Queries.CurReport.ColPos[iCol]+6
							,bodyFont.GetHeight(ev.Graphics)));
					}
				}
				yPos+=bodyFont.GetHeight(ev.Graphics);
				linesPrinted++;
				if(linesPrinted==Queries.TableQ.Rows.Count){
					tablePrinted=true;
				}
			}
			if(Queries.TableQ.Rows.Count==0){
				tablePrinted=true;
			}
			//totals:
			if(tablePrinted){
				if(yPos<bounds.Bottom){
					ev.Graphics.DrawLine(new Pen(Color.Black),bounds.Left,yPos+3,bounds.Right,yPos+3);
					yPos+=4;
					for(int iCol=0;iCol<Queries.TableQ.Columns.Count;iCol++){
						if(Queries.CurReport.ColAlign[iCol]==HorizontalAlignment.Right){
							float textWidth=(float)(ev.Graphics.MeasureString
								(Queries.CurReport.ColTotal[iCol].ToString("n"),subtitleFont).Width);
							ev.Graphics.DrawString(Queries.CurReport.ColTotal[iCol].ToString("n")
								,subtitleFont,Brushes.Black,new RectangleF(
								bounds.Left+Queries.CurReport.ColPos[iCol+1]-textWidth+3,yPos//the 3 is arbitrary
								,textWidth,subtitleFont.GetHeight(ev.Graphics)));
						}
						//else{
						//	ev.Graphics.DrawString(grid2[linesPrinted,iCol].ToString()
						//		,bodyFont,Brushes.Black,new RectangleF(
						//		bounds.Left+Queries.CurReport.ColPos[iCol],yPos
						//		,Queries.CurReport.ColPos[iCol+1]-Queries.CurReport.ColPos[iCol]
						//,bodyFont.GetHeight(ev.Graphics)));
						//}
					}
					totalsPrinted=true;
					yPos+=subtitleFont.GetHeight(ev.Graphics);
				}
			}
			//Summary
			if(totalsPrinted){
				if(yPos+Queries.CurReport.Summary.Length*subtitleFont.GetHeight(ev.Graphics)
					< bounds.Top+bounds.Height){
					ev.Graphics.DrawLine(new Pen(Color.Black),bounds.Left,yPos+2,bounds.Right,yPos+2);
					yPos+=bodyFont.GetHeight(ev.Graphics);
					for(int i=0;i<Queries.CurReport.Summary.Length;i++){
					//while(yPos<bounds.Top+bounds.Height && linesPrinted<Queries.TableQ.Rows.Count){
						//if(yPos>=bounds.Top+bounds.Height) break;
						ev.Graphics.DrawString(Queries.CurReport.Summary[i]
							,subtitleFont,Brushes.Black,bounds.Left,yPos);
						yPos+=subtitleFont.GetHeight(ev.Graphics);
					}
					summaryPrinted=true;
				}
			}
			if(!summaryPrinted){//linesPrinted < Queries.TableQ.Rows.Count){
				ev.HasMorePages = true;
				pagesPrinted++;
			}
			else{
				ev.HasMorePages = false;
				//UpDownPage.Maximum=pagesPrinted+1;
				totalPages=pagesPrinted+1;
				labelTotPages.Text="1 / "+totalPages.ToString();
				pagesPrinted=0;
				linesPrinted=0;
				headerPrinted=false;
				tablePrinted=false;
				totalsPrinted=false;
				summaryPrinted=false;
			}
		}

		private void butZoomIn_Click(object sender, System.EventArgs e){
			butFullPage.Visible=true;
			butZoomIn.Visible=false;
			printPreviewControl2.Zoom=1;
		}

		private void butFullPage_Click(object sender, System.EventArgs e){
			butFullPage.Visible=false;
			butZoomIn.Visible=true;
			printPreviewControl2.Zoom=((double)printPreviewControl2.ClientSize.Height
				/(double)pd2.DefaultPageSettings.PaperSize.Height);
		}

		private void butBack_Click(object sender, System.EventArgs e){
			if(printPreviewControl2.StartPage==0) return;
			printPreviewControl2.StartPage--;
			labelTotPages.Text=(printPreviewControl2.StartPage+1).ToString()
				+" / "+totalPages.ToString();
		}

		private void butFwd_Click(object sender, System.EventArgs e){
			if(printPreviewControl2.StartPage==totalPages-1) return;
			printPreviewControl2.StartPage++;
			labelTotPages.Text=(printPreviewControl2.StartPage+1).ToString()
				+" / "+totalPages.ToString();
		}

		private void butExportExcel_Click(object sender, System.EventArgs e) {
			/*
			saveFileDialog2=new SaveFileDialog();
      saveFileDialog2.AddExtension=true;
			saveFileDialog2.Title=Lan.g(this,"Select Folder to Save File To");
		  if(IsReport){
				saveFileDialog2.FileName=Queries.CurReport.Title;
			}
      else{
        saveFileDialog2.FileName=UserQueries.Cur.FileName;
			}
			if(!Directory.Exists( ((Pref)PrefB.HList["ExportPath"]).ValueString )){
				try{
					Directory.CreateDirectory( ((Pref)PrefB.HList["ExportPath"]).ValueString );
					saveFileDialog2.InitialDirectory=((Pref)PrefB.HList["ExportPath"]).ValueString;
				}
				catch{
					//initialDirectory will be blank
				}
			}
			else saveFileDialog2.InitialDirectory=((Pref)PrefB.HList["ExportPath"]).ValueString;
			//saveFileDialog2.DefaultExt="xls";
			//saveFileDialog2.Filter="txt files(*.txt)|*.txt|All files(*.*)|*.*";
      //saveFileDialog2.FilterIndex=1;
		  if(saveFileDialog2.ShowDialog()!=DialogResult.OK){
	   	  return;
			}
			Excel.Application excel=new Excel.ApplicationClass();
			excel.Workbooks.Add(Missing.Value);
			Worksheet worksheet = (Worksheet) excel.ActiveSheet;
			Range range=(Excel.Range)excel.Cells[1,1];
			range.Value2="test";
			range.Font.Bold=true;
			range=(Excel.Range)excel.Cells[1,2];
			range.ColumnWidth=30;
			range.FormulaR1C1="12345";
			excel.Save(saveFileDialog2.FileName);
	//this test case worked, so now it is just a matter of finishing this off, and Excel export will be done.
			MessageBox.Show(Lan.g(this,"File created successfully"));
			*/
		}

		private void butExport_Click(object sender, System.EventArgs e){
			saveFileDialog2=new SaveFileDialog();
      saveFileDialog2.AddExtension=true;
			//saveFileDialog2.Title=Lan.g(this,"Select Folder to Save File To");
		  if(IsReport){
				saveFileDialog2.FileName=Queries.CurReport.Title;
			}
      else{
				if(UserQueryCur==null || UserQueryCur.FileName==null || UserQueryCur.FileName=="")//.FileName==null)
					saveFileDialog2.FileName=Queries.CurReport.Title;
				else
					saveFileDialog2.FileName=UserQueryCur.FileName;
			}
			if(!Directory.Exists( ((Pref)PrefB.HList["ExportPath"]).ValueString )){
				try{
					Directory.CreateDirectory( ((Pref)PrefB.HList["ExportPath"]).ValueString );
					saveFileDialog2.InitialDirectory=((Pref)PrefB.HList["ExportPath"]).ValueString;
				}
				catch{
					//initialDirectory will be blank
				}
			}
			else saveFileDialog2.InitialDirectory=((Pref)PrefB.HList["ExportPath"]).ValueString;
			//saveFileDialog2.DefaultExt="txt";
			saveFileDialog2.Filter="Text files(*.txt)|*.txt|Excel Files(*.xls)|*.xls|All files(*.*)|*.*";
      saveFileDialog2.FilterIndex=0;
		  if(saveFileDialog2.ShowDialog()!=DialogResult.OK){
	   	  return;
			}
			try{
			  using(StreamWriter sw=new StreamWriter(saveFileDialog2.FileName,false))
					//new FileStream(,FileMode.Create,FileAccess.Write,FileShare.Read)))
				{
					String line="";  
					for(int i=0;i<Queries.CurReport.ColCaption.Length;i++){
						line+=Queries.CurReport.ColCaption[i];
						if(i<Queries.TableQ.Columns.Count-1){
							line+="\t";
						}
					}
					sw.WriteLine(line);
					string cell;
					for(int i=0;i<Queries.TableQ.Rows.Count;i++){
						line="";
						for(int j=0;j<Queries.TableQ.Columns.Count;j++){
							cell=Queries.TableQ.Rows[i][j].ToString();
							cell=cell.Replace("\r","");
							cell=cell.Replace("\n","");
							cell=cell.Replace("\t","");
							cell=cell.Replace("\"","");
							line+=cell;
							if(j<Queries.TableQ.Columns.Count-1){
								line+="\t";
							}
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

		private void butCopy_Click(object sender, System.EventArgs e){
			Clipboard.SetDataObject(textQuery.Text);
		}

		private void butPaste_Click(object sender, System.EventArgs e){
			IDataObject iData=Clipboard.GetDataObject();
			if(iData.GetDataPresent(DataFormats.Text)){
				textQuery.Text=(String)iData.GetData(DataFormats.Text); 
			}
			else{
				MessageBox.Show(Lan.g(this,"Could not retrieve data off the clipboard."));
			}

		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormQuery_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			//SecurityLogs.MakeLogEntry("User Query","");
		}

		

		

	}
}
