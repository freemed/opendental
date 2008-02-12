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
		private DataTable table;
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

		#region user fields
		//Must set all these externally before opening this form
		///<summary></summary>
		public List<PatAging> AgingList;
		public string Note;
		public DateTime DateRangeFrom;
		public DateTime DateRangeTo;
		public bool Intermingled;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when user wants to go to a patient or related object.")]
		public event PatientSelectedEventHandler GoToChanged=null;
		private bool isInitial=true;
		#endregion

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
			this.butCancel.Location = new System.Drawing.Point(805,654);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "&Cancel";
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
			this.butSend.Location = new System.Drawing.Point(805,620);
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
			this.butNone.Location = new System.Drawing.Point(133,656);
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
			this.butAll.Location = new System.Drawing.Point(42,656);
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
			this.gridBill.Location = new System.Drawing.Point(42,33);
			this.gridBill.Name = "gridBill";
			this.gridBill.ScrollValue = 0;
			this.gridBill.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridBill.Size = new System.Drawing.Size(742,615);
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
			this.contextMenu.Size = new System.Drawing.Size(103,26);
			// 
			// menuItemGoTo
			// 
			this.menuItemGoTo.Name = "menuItemGoTo";
			this.menuItemGoTo.Size = new System.Drawing.Size(102,22);
			this.menuItemGoTo.Text = "Go To";
			this.menuItemGoTo.Click += new System.EventHandler(this.menuItemGoTo_Click);
			// 
			// labelTotal
			// 
			this.labelTotal.Location = new System.Drawing.Point(4,19);
			this.labelTotal.Name = "labelTotal";
			this.labelTotal.Size = new System.Drawing.Size(83,16);
			this.labelTotal.TabIndex = 29;
			this.labelTotal.Text = "Total=20";
			this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(790,528);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91,87);
			this.label1.TabIndex = 30;
			this.label1.Text = "This will immediately print or email all selected bills";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// radioUnsent
			// 
			this.radioUnsent.Checked = true;
			this.radioUnsent.Location = new System.Drawing.Point(42,7);
			this.radioUnsent.Name = "radioUnsent";
			this.radioUnsent.Size = new System.Drawing.Size(102,20);
			this.radioUnsent.TabIndex = 31;
			this.radioUnsent.TabStop = true;
			this.radioUnsent.Text = "Unsent";
			this.radioUnsent.UseVisualStyleBackColor = true;
			// 
			// radioSent
			// 
			this.radioSent.Location = new System.Drawing.Point(145,7);
			this.radioSent.Name = "radioSent";
			this.radioSent.Size = new System.Drawing.Size(89,20);
			this.radioSent.TabIndex = 32;
			this.radioSent.Text = "Sent";
			this.radioSent.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labelEmailed);
			this.groupBox1.Controls.Add(this.labelPrinted);
			this.groupBox1.Controls.Add(this.labelSelected);
			this.groupBox1.Controls.Add(this.labelTotal);
			this.groupBox1.Location = new System.Drawing.Point(790,233);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(90,100);
			this.groupBox1.TabIndex = 33;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Counts";
			// 
			// labelEmailed
			// 
			this.labelEmailed.Location = new System.Drawing.Point(4,76);
			this.labelEmailed.Name = "labelEmailed";
			this.labelEmailed.Size = new System.Drawing.Size(83,16);
			this.labelEmailed.TabIndex = 32;
			this.labelEmailed.Text = "Emailed=20";
			this.labelEmailed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelPrinted
			// 
			this.labelPrinted.Location = new System.Drawing.Point(4,57);
			this.labelPrinted.Name = "labelPrinted";
			this.labelPrinted.Size = new System.Drawing.Size(83,16);
			this.labelPrinted.TabIndex = 31;
			this.labelPrinted.Text = "Printed=20";
			this.labelPrinted.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelSelected
			// 
			this.labelSelected.Location = new System.Drawing.Point(4,38);
			this.labelSelected.Name = "labelSelected";
			this.labelSelected.Size = new System.Drawing.Size(83,16);
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
			this.butEdit.Location = new System.Drawing.Point(798,369);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(82,24);
			this.butEdit.TabIndex = 34;
			this.butEdit.Text = "Edit Selected";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(707,8);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(77,20);
			this.textDateEnd.TabIndex = 38;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(561,8);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77,20);
			this.textDateStart.TabIndex = 37;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(641,11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64,14);
			this.label2.TabIndex = 36;
			this.label2.Text = "End Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(488,11);
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
			this.butRefresh.Location = new System.Drawing.Point(799,7);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(82,24);
			this.butRefresh.TabIndex = 39;
			this.butRefresh.Text = "Refresh";
			// 
			// comboOrder
			// 
			this.comboOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboOrder.Location = new System.Drawing.Point(317,7);
			this.comboOrder.MaxDropDownItems = 40;
			this.comboOrder.Name = "comboOrder";
			this.comboOrder.Size = new System.Drawing.Size(133,21);
			this.comboOrder.TabIndex = 41;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(224,11);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(91,14);
			this.label4.TabIndex = 40;
			this.label4.Text = "Order by";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butPrintList
			// 
			this.butPrintList.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrintList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrintList.Autosize = true;
			this.butPrintList.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrintList.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrintList.CornerRadius = 4F;
			this.butPrintList.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrintList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrintList.Location = new System.Drawing.Point(696,654);
			this.butPrintList.Name = "butPrintList";
			this.butPrintList.Size = new System.Drawing.Size(88,24);
			this.butPrintList.TabIndex = 42;
			this.butPrintList.Text = "Print List";
			// 
			// FormBilling
			// 
			this.AcceptButton = this.butSend;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(891,688);
			this.Controls.Add(this.butPrintList);
			this.Controls.Add(this.comboOrder);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.textDateEnd);
			this.Controls.Add(this.textDateStart);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butEdit);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.radioSent);
			this.Controls.Add(this.radioUnsent);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridBill);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butSend);
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
			//contrAccount1.checkShowAll.Checked=false;
			//textDate.Text=Ledgers.GetClosestFirst(DateTime.Today).ToShortDateString();
			//Patients.GetAgingList();
			Statement stmt;
			if(AgingList!=null){
				for(int i=0;i<AgingList.Count;i++){
					stmt=new Statement();
					stmt.DateRangeFrom=DateRangeFrom;
					stmt.DateRangeTo=DateRangeTo;
					stmt.DateSent=DateTime.Today;
					stmt.DocNum=0;
					stmt.HidePayment=false;
					stmt.Intermingled=Intermingled;
					stmt.IsSent=false;
					stmt.Mode_=StatementMode.Mail;
	//set email here
					stmt.Note=Note;
	//set appointment reminders here
					stmt.NoteBold="";
	//set dunning messages here
					stmt.PatNum=AgingList[i].PatNum;
					stmt.SinglePatient=false;
					//Statements.WriteObject(stmt);
				}
			}
			//FillGrid();
			//gridBill.SetSelected(true);
			//labelSelected.Text=Lan.g(this,"Selected=")+gridBill.SelectedIndices.Length.ToString();
			labelPrinted.Text=Lan.g(this,"Printed=")+"0";
			labelEmailed.Text=Lan.g(this,"Emailed=")+"0";
			comboOrder.Items.Add(Lan.g(this,"BillingType"));
			comboOrder.Items.Add(Lan.g(this,"LastName"));
			comboOrder.SelectedIndex=0;
		}

		private void FormBilling_Activated(object sender,EventArgs e) {
			//this seems to fire quite frequently, so it should auto refresh pretty well.
			FillGrid();
		}

		///<summary>We will always try to preserve the selected bills as well as the scroll postition.</summary>
		private void FillGrid(){
			int scrollPos=gridBill.ScrollValue;
			List<int> selectedKeys=new List<int>();
			for(int i=0;i<gridBill.SelectedIndices.Length;i++){
				selectedKeys.Add(PIn.PInt(table.Rows[gridBill.SelectedIndices[i]]["StatementNum"].ToString()));
			}
			table=Statements.GetBilling(radioSent.Checked);
			gridBill.BeginUpdate();
			gridBill.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableBilling","Name"),180);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","BillType"),100);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","Total"),90,HorizontalAlignment.Right);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","-InsuranceEst"),90,HorizontalAlignment.Right);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","=Amount"),90,HorizontalAlignment.Right);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","LastStatement"),100);
			gridBill.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableBilling","Mode"),100);
			gridBill.Columns.Add(col);
			gridBill.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){//AgingList.Count;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(table.Rows[i]["name"].ToString());
				row.Cells.Add(table.Rows[i]["billingType"].ToString());
				row.Cells.Add(table.Rows[i]["total"].ToString());
				row.Cells.Add(table.Rows[i]["insEst"].ToString());
				row.Cells.Add(table.Rows[i]["amount"].ToString());
				row.Cells.Add(table.Rows[i]["lastStatement"].ToString());
				row.Cells.Add(table.Rows[i]["mode"].ToString());
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
		}

		private void butNone_Click(object sender, System.EventArgs e) {	
			gridBill.SetSelected(false);
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
		}

		private void butSend_Click(object sender, System.EventArgs e) {
			if(gridBill.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select items first."));
				return;
			}
			if(!MsgBox.Show(this,true,"Please be prepared to wait up to ten minutes while all the bills get processed.  Continue?")){
				return;
			}
			Cursor=Cursors.WaitCursor;
			int[] guarNums=new int[gridBill.SelectedIndices.Length];
			for(int i=0;i<gridBill.SelectedIndices.Length;i++){
				guarNums[i]=AgingList[gridBill.SelectedIndices[i]].PatNum;
			}
			FormRpStatement FormS=new FormRpStatement();
			//FormS.LoadAndPrint(guarNums,GeneralNote);
			Cursor=Cursors.Default;
			#if DEBUG
				FormS.ShowDialog();
			#endif
			if(MsgBox.Show(this,true,"Printing Statements Complete.  OK to make Commlog entries?")){
				Commlog commlog;
				for(int i=0;i<guarNums.Length;i++){
					commlog=new Commlog();
					commlog.CommDateTime=DateTime.Now;
					commlog.CommType=0;
					commlog.SentOrReceived=CommSentOrReceived.Sent;
					commlog.Mode_=CommItemMode.Mail;
			//		commlog.IsStatementSent=true;
					commlog.PatNum=guarNums[i];//usually the guarantor
					Commlogs.Insert(commlog);
				}
			}
			FillGrid();
			if(gridBill.Rows.Count>0 && MsgBox.Show(this,true,"Delete all unsent bills?")){
				MessageBox.Show("Not functional yet.");
			}
			DialogResult=DialogResult.OK;
			Close();
		}

		private void butCancel_Click(object sender,EventArgs e) {
			if(gridBill.Rows.Count>0){
				DialogResult result=MessageBox.Show(Lan.g(this,"You may leave this window open while you work.  If you do close it, do you want to delete all unsent bills?"),
					"",MessageBoxButtons.YesNoCancel);
				if(result==DialogResult.Yes){
					MessageBox.Show("Not functional yet.");
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

		


		

		
		

		

		

	}
}

















