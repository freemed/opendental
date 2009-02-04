using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CodeBase;
using OpenDental.UI;
using OpenDentBusiness;
using OpenDental.Imaging;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Printing;

namespace OpenDental{
///<summary></summary>
	public class FormBilling : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butAll;
		private OpenDental.UI.Button butNone;
		private OpenDental.UI.Button butSend;
		private IContainer components;
		private OpenDental.UI.ODGrid gridBill;
		private Label labelTotal;
		private Label label1;
		private RadioButton radioUnsent;
		private RadioButton radioSent;
		private GroupBox groupBox1;
		private Label labelSelected;
		private Label labelEmailed;
		private Label labelPrinted;
		private OpenDental.UI.Button butEdit;
		private ValidDate textDateEnd;
		private ValidDate textDateStart;
		private Label label2;
		private Label label3;
		private OpenDental.UI.Button butRefresh;
		private ComboBox comboOrder;
		private Label label4;
		private OpenDental.UI.Button butPrintList;
		private ContextMenuStrip contextMenu;
		private ToolStripMenuItem menuItemGoTo;
		private bool headingPrinted;
		private int headingPrintH;
		private Label label5;
		private int pagesPrinted;
		///<summary>Used in the Activated event.</summary>
		private bool isPrinting=false;
		private DataTable table;
		private Label labelSentElect;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when user wants to go to a patient or related object.")]
		public event PatientSelectedEventHandler GoToChanged=null;
		private bool isInitial=true;

		protected void OnGoToChanged(int patNum) {
			if(GoToChanged!=null) {
				Patient pat=Patients.GetPat(patNum);
				GoToChanged(this,new PatientSelectedEventArgs(patNum,pat.GetNameLF(),pat.Email!="",pat.ChartNumber));
			}
		}

		///<summary></summary>
		public FormBilling(){
			InitializeComponent();
			//this.listMain.ContextMenu = this.menuEdit;
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBilling));
			this.butCancel = new OpenDental.UI.Button();
			this.butSend = new OpenDental.UI.Button();
			this.butNone = new OpenDental.UI.Button();
			this.butAll = new OpenDental.UI.Button();
			this.gridBill = new OpenDental.UI.ODGrid();
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemGoTo = new System.Windows.Forms.ToolStripMenuItem();
			this.labelTotal = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.radioUnsent = new System.Windows.Forms.RadioButton();
			this.radioSent = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labelSentElect = new System.Windows.Forms.Label();
			this.labelEmailed = new System.Windows.Forms.Label();
			this.labelPrinted = new System.Windows.Forms.Label();
			this.labelSelected = new System.Windows.Forms.Label();
			this.butEdit = new OpenDental.UI.Button();
			this.textDateEnd = new OpenDental.ValidDate();
			this.textDateStart = new OpenDental.ValidDate();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.butRefresh = new OpenDental.UI.Button();
			this.comboOrder = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butPrintList = new OpenDental.UI.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.contextMenu.SuspendLayout();
			this.groupBox1.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(802,656);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSend.Autosize = true;
			this.butSend.BackColor = System.Drawing.SystemColors.Control;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.CornerRadius = 4F;
			this.butSend.Location = new System.Drawing.Point(802,622);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(75,24);
			this.butSend.TabIndex = 0;
			this.butSend.Text = "Send";
			this.butSend.UseVisualStyleBackColor = false;
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// butNone
			// 
			this.butNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNone.Autosize = true;
			this.butNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNone.CornerRadius = 4F;
			this.butNone.Location = new System.Drawing.Point(103,656);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(75,24);
			this.butNone.TabIndex = 23;
			this.butNone.Text = "&None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(12,656);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(75,24);
			this.butAll.TabIndex = 22;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// gridBill
			// 
			this.gridBill.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridBill.ContextMenuStrip = this.contextMenu;
			this.gridBill.HScrollVisible = false;
			this.gridBill.Location = new System.Drawing.Point(12,33);
			this.gridBill.Name = "gridBill";
			this.gridBill.ScrollValue = 0;
			this.gridBill.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridBill.Size = new System.Drawing.Size(772,615);
			this.gridBill.TabIndex = 28;
			this.gridBill.Title = "Bills";
			this.gridBill.TranslationName = "TableBilling";
			this.gridBill.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridBill_CellClick);
			this.gridBill.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridBill_MouseDown);
			this.gridBill.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridBill_CellDoubleClick);
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemGoTo});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(107,26);
			// 
			// menuItemGoTo
			// 
			this.menuItemGoTo.Name = "menuItemGoTo";
			this.menuItemGoTo.Size = new System.Drawing.Size(106,22);
			this.menuItemGoTo.Text = "Go To";
			this.menuItemGoTo.Click += new System.EventHandler(this.menuItemGoTo_Click);
			// 
			// labelTotal
			// 
			this.labelTotal.Location = new System.Drawing.Point(4,19);
			this.labelTotal.Name = "labelTotal";
			this.labelTotal.Size = new System.Drawing.Size(89,16);
			this.labelTotal.TabIndex = 29;
			this.labelTotal.Text = "Total=20";
			this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(787,530);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91,87);
			this.label1.TabIndex = 30;
			this.label1.Text = "This will immediately print or email all selected bills";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// radioUnsent
			// 
			this.radioUnsent.Checked = true;
			this.radioUnsent.Location = new System.Drawing.Point(15,7);
			this.radioUnsent.Name = "radioUnsent";
			this.radioUnsent.Size = new System.Drawing.Size(75,20);
			this.radioUnsent.TabIndex = 31;
			this.radioUnsent.TabStop = true;
			this.radioUnsent.Text = "Unsent";
			this.radioUnsent.UseVisualStyleBackColor = true;
			this.radioUnsent.Click += new System.EventHandler(this.radioUnsent_Click);
			// 
			// radioSent
			// 
			this.radioSent.Location = new System.Drawing.Point(94,7);
			this.radioSent.Name = "radioSent";
			this.radioSent.Size = new System.Drawing.Size(79,20);
			this.radioSent.TabIndex = 32;
			this.radioSent.Text = "Sent";
			this.radioSent.UseVisualStyleBackColor = true;
			this.radioSent.Click += new System.EventHandler(this.radioSent_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.labelSentElect);
			this.groupBox1.Controls.Add(this.labelEmailed);
			this.groupBox1.Controls.Add(this.labelPrinted);
			this.groupBox1.Controls.Add(this.labelSelected);
			this.groupBox1.Controls.Add(this.labelTotal);
			this.groupBox1.Location = new System.Drawing.Point(788,233);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(96,119);
			this.groupBox1.TabIndex = 33;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Counts";
			// 
			// labelSentElect
			// 
			this.labelSentElect.Location = new System.Drawing.Point(4,95);
			this.labelSentElect.Name = "labelSentElect";
			this.labelSentElect.Size = new System.Drawing.Size(89,16);
			this.labelSentElect.TabIndex = 33;
			this.labelSentElect.Text = "SentElect=20";
			this.labelSentElect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelEmailed
			// 
			this.labelEmailed.Location = new System.Drawing.Point(4,76);
			this.labelEmailed.Name = "labelEmailed";
			this.labelEmailed.Size = new System.Drawing.Size(89,16);
			this.labelEmailed.TabIndex = 32;
			this.labelEmailed.Text = "Emailed=20";
			this.labelEmailed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelPrinted
			// 
			this.labelPrinted.Location = new System.Drawing.Point(4,57);
			this.labelPrinted.Name = "labelPrinted";
			this.labelPrinted.Size = new System.Drawing.Size(89,16);
			this.labelPrinted.TabIndex = 31;
			this.labelPrinted.Text = "Printed=20";
			this.labelPrinted.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelSelected
			// 
			this.labelSelected.Location = new System.Drawing.Point(4,38);
			this.labelSelected.Name = "labelSelected";
			this.labelSelected.Size = new System.Drawing.Size(89,16);
			this.labelSelected.TabIndex = 30;
			this.labelSelected.Text = "Selected=20";
			this.labelSelected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butEdit.Autosize = true;
			this.butEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEdit.CornerRadius = 4F;
			this.butEdit.Location = new System.Drawing.Point(796,67);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(82,24);
			this.butEdit.TabIndex = 34;
			this.butEdit.Text = "Edit Selected";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(615,8);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(77,20);
			this.textDateEnd.TabIndex = 38;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(471,8);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77,20);
			this.textDateStart.TabIndex = 37;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(549,11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64,14);
			this.label2.TabIndex = 36;
			this.label2.Text = "End Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(398,11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70,14);
			this.label3.TabIndex = 35;
			this.label3.Text = "Start Date";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(796,7);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(82,24);
			this.butRefresh.TabIndex = 39;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// comboOrder
			// 
			this.comboOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboOrder.Location = new System.Drawing.Point(249,7);
			this.comboOrder.MaxDropDownItems = 40;
			this.comboOrder.Name = "comboOrder";
			this.comboOrder.Size = new System.Drawing.Size(133,21);
			this.comboOrder.TabIndex = 41;
			this.comboOrder.SelectionChangeCommitted += new System.EventHandler(this.comboOrder_SelectionChangeCommitted);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(164,11);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(83,14);
			this.label4.TabIndex = 40;
			this.label4.Text = "Order by";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butPrintList
			// 
			this.butPrintList.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrintList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrintList.Autosize = true;
			this.butPrintList.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrintList.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrintList.CornerRadius = 4F;
			this.butPrintList.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrintList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrintList.Location = new System.Drawing.Point(364,656);
			this.butPrintList.Name = "butPrintList";
			this.butPrintList.Size = new System.Drawing.Size(88,24);
			this.butPrintList.TabIndex = 42;
			this.butPrintList.Text = "Print List";
			this.butPrintList.Click += new System.EventHandler(this.butPrintList_Click);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.Location = new System.Drawing.Point(456,651);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(165,29);
			this.label5.TabIndex = 43;
			this.label5.Text = "Does not print individual bills.  Just prints the list of bills.";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormBilling
			// 
			this.AcceptButton = this.butSend;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(888,688);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.comboOrder);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.butPrintList);
			this.Controls.Add(this.textDateEnd);
			this.Controls.Add(this.textDateStart);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butEdit);
			this.Controls.Add(this.radioSent);
			this.Controls.Add(this.radioUnsent);
			this.Controls.Add(this.gridBill);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBilling";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Bills";
			this.Load += new System.EventHandler(this.FormBilling_Load);
			this.Activated += new System.EventHandler(this.FormBilling_Activated);
			this.contextMenu.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormBilling_Load(object sender, System.EventArgs e) {
			labelPrinted.Text=Lan.g(this,"Printed=")+"0";
			labelEmailed.Text=Lan.g(this,"E-mailed=")+"0";
			labelSentElect.Text=Lan.g(this,"SentElect=")+"0";
			comboOrder.Items.Add(Lan.g(this,"BillingType"));
			comboOrder.Items.Add(Lan.g(this,"PatientName"));
			comboOrder.SelectedIndex=0;
		}

		private void FormBilling_Activated(object sender,EventArgs e) {
			//this gets fired very frequently, including right in the middle of printing a batch.
			if(!isPrinting){
				FillGrid();
			}
		}

		///<summary>We will always try to preserve the selected bills as well as the scroll postition.</summary>
		private void FillGrid(){
			if(textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateEnd.errorProvider1.GetError(textDateEnd)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			int scrollPos=gridBill.ScrollValue;
			List<int> selectedKeys=new List<int>();
			for(int i=0;i<gridBill.SelectedIndices.Length;i++){
				selectedKeys.Add(PIn.PInt(table.Rows[gridBill.SelectedIndices[i]]["StatementNum"].ToString()));
			}
			DateTime dateFrom=DateTime.MinValue;
			DateTime dateTo=new DateTime(2200,1,1);
			if(textDateStart.Text!=""){
				dateFrom=PIn.PDate(textDateStart.Text);
			}
			if(textDateEnd.Text!=""){
				dateTo=PIn.PDate(textDateEnd.Text);
			}
			table=Statements.GetBilling(radioSent.Checked,comboOrder.SelectedIndex,dateFrom,dateTo);
			gridBill.BeginUpdate();
			gridBill.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableBilling","Name"),180);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","BillType"),110);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","Mode"),80);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","LastStatement"),100);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","BalTot"),70,HorizontalAlignment.Right);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","-InsEst"),70,HorizontalAlignment.Right);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","=AmtDue"),70,HorizontalAlignment.Right);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","PayPlanDue"),70,HorizontalAlignment.Right);
			gridBill.Columns.Add(col);
			gridBill.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(table.Rows[i]["name"].ToString());
				row.Cells.Add(table.Rows[i]["billingType"].ToString());
				row.Cells.Add(table.Rows[i]["mode"].ToString());
				row.Cells.Add(table.Rows[i]["lastStatement"].ToString());
				row.Cells.Add(table.Rows[i]["balTotal"].ToString());
				row.Cells.Add(table.Rows[i]["insEst"].ToString());
				row.Cells.Add(table.Rows[i]["amountDue"].ToString());
				row.Cells.Add(table.Rows[i]["payPlanDue"].ToString());
				gridBill.Rows.Add(row);
			}
			gridBill.EndUpdate();
			if(isInitial){
				gridBill.SetSelected(true);
				isInitial=false;
			}
			else{
				for(int i=0;i<selectedKeys.Count;i++){
					for(int j=0;j<table.Rows.Count;j++){
						if(table.Rows[j]["StatementNum"].ToString()==selectedKeys[i].ToString()){
							gridBill.SetSelected(j,true);
						}
					}
				}
			}
			gridBill.ScrollValue=scrollPos;
			labelTotal.Text=Lan.g(this,"Total=")+table.Rows.Count.ToString();
			labelSelected.Text=Lan.g(this,"Selected=")+gridBill.SelectedIndices.Length.ToString();
			//labelSelected.Text=Lan.g(this,"Selected=")+"0";
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			gridBill.SetSelected(true);
			labelSelected.Text=Lan.g(this,"Selected=")+gridBill.SelectedIndices.Length.ToString();
		}

		private void butNone_Click(object sender, System.EventArgs e) {	
			gridBill.SetSelected(false);
			labelSelected.Text=Lan.g(this,"Selected=")+gridBill.SelectedIndices.Length.ToString();
		}

		private void radioUnsent_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void radioSent_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
			FillGrid();
		}

		private void comboOrder_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			if(textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateEnd.errorProvider1.GetError(textDateEnd)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			FillGrid();
		}

		private void gridBill_CellClick(object sender,ODGridClickEventArgs e) {
			labelSelected.Text=Lan.g(this,"Selected=")+gridBill.SelectedIndices.Length.ToString();
		}

		private void gridBill_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormStatementOptions FormSO=new FormStatementOptions();
			Statement stmt;
			stmt=Statements.CreateObject(PIn.PInt(table.Rows[e.Row]["StatementNum"].ToString()));
			//FormSO.StmtList=stmtList;
			FormSO.StmtCur=stmt;
			FormSO.ShowDialog();
		}

		private void gridBill_MouseDown(object sender,MouseEventArgs e) {
			if(e.Button==MouseButtons.Right){
				gridBill.SetSelected(false);
			}
		}

		private void menuItemGoTo_Click(object sender,EventArgs e) {
			if(gridBill.SelectedIndices.Length==0){
				//I don't think this could happen
				MsgBox.Show(this,"Please select one bill first.");
				return;
			}
			else{
				int patNum=PIn.PInt(table.Rows[gridBill.SelectedIndices[0]]["PatNum"].ToString());
				OnGoToChanged(patNum);
				SendToBack();//??
			}
		}

		private void butEdit_Click(object sender,EventArgs e) {
			if(gridBill.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select one or more bills first.");
				return;
			}
			FormStatementOptions FormSO=new FormStatementOptions();
			List<Statement> stmtList=new List<Statement>();
			Statement stmt;
			for(int i=0;i<gridBill.SelectedIndices.Length;i++){
				stmt=Statements.CreateObject(PIn.PInt(table.Rows[gridBill.SelectedIndices[i]]["StatementNum"].ToString()));
				stmtList.Add(stmt.Copy());
			}
			FormSO.StmtList=stmtList;
			//Statement stmt=new Statement();
			//stmt.DateRangeFrom=DateTime.
			//FormSO.StmtCur=stmt;
			FormSO.ShowDialog();
			//FillGrid happens automatically through Activated event.
		}

		private void butPrintList_Click(object sender,EventArgs e) {
			pagesPrinted=0;
			PrintDocument pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(25,25,40,40);
			//pd.OriginAtMargins=true;
			if(pd.DefaultPageSettings.PaperSize.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			headingPrinted=false;
			#if DEBUG
				FormRpPrintPreview pView = new FormRpPrintPreview();
				pView.printPreviewControl2.Document=pd;
				pView.ShowDialog();
			#else
				if(!Printers.SetPrinter(pd,PrintSituation.Default)) {
					return;
				}
				try{
					pd.Print();
				}
				catch {
					MsgBox.Show(this,"Printer not available");
				}
			#endif			
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
				text=Lan.g(this,"Billing List");
				g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,yPos);
				//yPos+=(int)g.MeasureString(text,headingFont).Height;
				//text=textDateFrom.Text+" "+Lan.g(this,"to")+" "+textDateTo.Text;
				//g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=25;
				headingPrinted=true;
				headingPrintH=yPos;
			}
			#endregion
			int totalPages=gridBill.GetNumberOfPages(bounds,headingPrintH);
			yPos=gridBill.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			pagesPrinted++;
			if(pagesPrinted < totalPages) {
				e.HasMorePages=true;
			}
			else {
				e.HasMorePages=false;
			}
			g.Dispose();
		}

		private void butSend_Click(object sender, System.EventArgs e) {
			if(gridBill.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select items first."));
				return;
			}
			labelPrinted.Text=Lan.g(this,"Printed=")+"0";
			labelEmailed.Text=Lan.g(this,"E-mailed=")+"0";
			labelSentElect.Text=Lan.g(this,"SentElect=")+"0";
			if(!MsgBox.Show(this,true,"Please be prepared to wait up to ten minutes while all the bills get processed.\r\nOnce complete, the pdf print preview will be launched in Adobe Reader.  You will print from that program.  Continue?")){
				return;
			}
			Cursor=Cursors.WaitCursor;
			isPrinting=true;
			FormRpStatement FormST=new FormRpStatement();
			Statement stmt;
			Random rnd;
			string fileName;
			string filePathAndName;
			string attachPath;
			EmailMessage message;
			EmailAttach attach;
			Family fam;
			Patient pat;
			int skipped=0;
			int emailed=0;
			int printed=0;
			int sentelect=0;
			//FormEmailMessageEdit FormEME=new FormEmailMessageEdit();
			if(ImageStore.UpdatePatient == null){
				ImageStore.UpdatePatient = new FileStore.UpdatePatientDelegate(Patients.Update);
			}
			OpenDental.Imaging.IImageStore imageStore;
			//Concat all the pdf's together to create one print job.
			//Also, if a statement is to be emailed, it does that here and does not attach it to the print job.
			//If something fails badly, it's no big deal, because we can click the radio button to see "sent" bills, and unsend them from there.
      PdfDocument outputDocument=new PdfDocument();
			PdfDocument inputDocument;
			PdfPage page;
			string savedPdfPath;
			PrintDocument pd=null;
			XmlWriterSettings xmlSettings=new XmlWriterSettings();
			xmlSettings.OmitXmlDeclaration=true;
			xmlSettings.Encoding=Encoding.UTF8;
			xmlSettings.Indent=true;
			xmlSettings.IndentChars="   ";
			StringBuilder strBuildElect=new StringBuilder();
			XmlWriter writerElect=XmlWriter.Create(strBuildElect,xmlSettings);
			OpenDental.Bridges.EHG_statements.GeneratePracticeInfo(writerElect);
			DataSet dataSet;
			List<int> stateNumsElect=new List<int>();
			for(int i=0;i<gridBill.SelectedIndices.Length;i++){
				stmt=Statements.CreateObject(PIn.PInt(table.Rows[gridBill.SelectedIndices[i]]["StatementNum"].ToString()));
				fam=Patients.GetFamily(stmt.PatNum);
				pat=fam.GetPatient(stmt.PatNum);
				dataSet=AccountModuleL.GetStatement(stmt.PatNum,stmt.SinglePatient,stmt.DateRangeFrom,stmt.DateRangeTo,stmt.Intermingled);
				if(stmt.Mode_==StatementMode.Email){
					if(PrefC.GetString("EmailSMTPserver")==""){
						MsgBox.Show(this,"You need to enter an SMTP server name in e-mail setup before you can send e-mail.");
						Cursor=Cursors.Default;
						isPrinting=false;
						//FillGrid();//automatic
						return;
					}
					if(pat.Email==""){
						skipped++;
						continue;
					}
				}
				stmt.IsSent=true;
				stmt.DateSent=DateTime.Today;
				FormST.CreateStatementPdf(stmt,pat,fam,dataSet);
				if(stmt.DocNum==0){
					MsgBox.Show(this,"Failed to save PDF.  In Setup, DataPaths, please make sure the top radio button is checked.");
					Cursor=Cursors.Default;
					isPrinting=false;
					return;
				}
				imageStore = OpenDental.Imaging.ImageStore.GetImageStore(pat);
				savedPdfPath=imageStore.GetFilePath(Documents.GetByNum(stmt.DocNum));
				if(stmt.Mode_==StatementMode.InPerson || stmt.Mode_==StatementMode.Mail){
					if(pd==null){
						pd=new PrintDocument();
					}
					inputDocument=PdfReader.Open(savedPdfPath,PdfDocumentOpenMode.Import);
					for(int idx=0;idx<inputDocument.PageCount;idx++){
						page=inputDocument.Pages[idx];
						outputDocument.AddPage(page);
					}
					printed++;
					labelPrinted.Text=Lan.g(this,"Printed=")+printed.ToString();
					Application.DoEvents();
					Statements.MarkSent(stmt.StatementNum,stmt.DateSent);
				}
				if(stmt.Mode_==StatementMode.Email){
					attachPath=FormEmailMessageEdit.GetAttachPath();
					rnd=new Random();
					fileName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()+".pdf";
					filePathAndName=ODFileUtils.CombinePaths(attachPath,fileName);
					File.Copy(savedPdfPath,filePathAndName);
					//Process.Start(filePathAndName);
					message=new EmailMessage();
					message.PatNum=pat.PatNum;
					message.ToAddress=pat.Email;
					message.FromAddress=PrefC.GetString("EmailSenderAddress");
					message.Subject=Lan.g(this,"Statement");
					message.BodyText=Lan.g(this,"Statement");
					attach=new EmailAttach();
					attach.DisplayedFileName="Statement.pdf";
					attach.ActualFileName=fileName;
					message.Attachments.Add(attach);
					try{
						FormEmailMessageEdit.SendEmail(message);
						emailed++;
						labelEmailed.Text=Lan.g(this,"E-mailed=")+emailed.ToString();
						Application.DoEvents();
					}
					catch(Exception ex){
						//stmt.IsSent=false;
						//Statements.WriteObject(stmt);
						Cursor=Cursors.Default;
						MessageBox.Show(ex.Message);
						return;
					}
					Statements.MarkSent(stmt.StatementNum,stmt.DateSent);
				}
				if(stmt.Mode_==StatementMode.Electronic) {
					stateNumsElect.Add(stmt.StatementNum);
					OpenDental.Bridges.EHG_statements.GenerateOneStatement(writerElect,stmt,pat,fam,dataSet);
					sentelect++;
					labelSentElect.Text=Lan.g(this,"SentElect=")+sentelect.ToString();
					Application.DoEvents();
					//do this later:
					//Statements.MarkSent(stmt.StatementNum,stmt.DateSent);
				}
			}
			//now print-------------------------------------------------------------------------------------
			if(pd!=null){
				string tempFileOutputDocument=Path.GetTempFileName()+".pdf";
				outputDocument.Save(tempFileOutputDocument);
				try{
					Process.Start(tempFileOutputDocument);
				}
				catch(Exception ex){
					MessageBox.Show(Lan.g(this,"Error: Please make sure Adobe Reader is installed.")+ex.Message);
				}
				//}
			}
			//finish up elect and send if needed------------------------------------------------------------
			if(sentelect>0) {
				OpenDental.Bridges.EHG_statements.GenerateWrapUp(writerElect);
				writerElect.Close();
				OpenDental.Bridges.EHG_statements.Send(strBuildElect.ToString());
				CodeBase.MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(strBuildElect.ToString());
				msgbox.ShowDialog();
				//loop through all statements and mark sent
				for(int i=0;i<stateNumsElect.Count;i++) {
					Statements.MarkSent(stateNumsElect[i],DateTime.Today);
				}
			}
			else {
				writerElect.Close();
			}
			string msg="";
			if(skipped>0){
				msg+=Lan.g(this,"Skipped due to missing email address: ")+skipped.ToString()+"\r\n";
			}
			msg+=Lan.g(this,"Printed: ")+printed.ToString()+"\r\n"
				+Lan.g(this,"E-mailed: ")+emailed.ToString()+"\r\n"
				+Lan.g(this,"SentElect: ")+sentelect.ToString();
			MessageBox.Show(msg);
			Cursor=Cursors.Default;
			isPrinting=false;
			FillGrid();//not automatic
		}

		private void butCancel_Click(object sender,EventArgs e) {
			if(gridBill.Rows.Count>0){
				DialogResult result=MessageBox.Show(Lan.g(this,"You may leave this window open while you work.  If you do close it, do you want to delete all unsent bills?"),
					"",MessageBoxButtons.YesNoCancel);
				if(result==DialogResult.Yes){
					int rowsChanged=0;
					for(int i=0;i<table.Rows.Count;i++){
						if(table.Rows[i]["IsSent"].ToString()=="0"){
							Statements.DeleteObject(PIn.PInt(table.Rows[i]["StatementNum"].ToString()));
							rowsChanged++;
						}
					}
					MessageBox.Show(Lan.g(this,"Unsent statements deleted: ")+rowsChanged.ToString());
				}
				else if(result==DialogResult.No){
					DialogResult=DialogResult.Cancel;
					Close();
				}
				else{//cancel
					return;
				}
			}
			DialogResult=DialogResult.Cancel;
			Close();
		}

	
		/// <summary></summary>
		private void butSendEbill_Click(object sender,EventArgs e) {
			if (gridBill.SelectedIndices.Length == 0){
				MessageBox.Show(Lan.g(this, "Please select items first."));
				return;
			}
			Cursor.Current = Cursors.WaitCursor;
			// Populate Array And Open eBill Form
			ArrayList PatientList = new ArrayList();
			for (int i = 0; i < gridBill.SelectedIndices.Length; i++)
					PatientList.Add(PIn.PInt(table.Rows[gridBill.SelectedIndices[i]]["PatNum"].ToString()));
			// Open eBill form
			FormPatienteBill FormPatienteBill = new FormPatienteBill(PatientList); 
			FormPatienteBill.ShowDialog();
			Cursor.Current = Cursors.Default;
		}

		

		


		

		
		

		

		

	}
}

















