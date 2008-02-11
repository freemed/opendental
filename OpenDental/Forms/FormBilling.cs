using System;
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
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.ODGrid gridBill;
		///<summary>Set this list externally before openning the billing window.</summary>
		public PatAging[] AgingList;
		private Label labelTotal;
		private Label label1;
		private RadioButton radioUnsent;
		private RadioButton radioSent;
		private GroupBox groupBox1;
		private Label labelSelected;
		private Label labelEmailed;
		private Label labelPrinted;
		///<summary></summary>
		public string GeneralNote;
		private OpenDental.UI.Button butEdit;
		private List<Statement> stmtList;

		///<summary></summary>
		public FormBilling(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBilling));
			this.butCancel = new OpenDental.UI.Button();
			this.butSend = new OpenDental.UI.Button();
			this.butNone = new OpenDental.UI.Button();
			this.butAll = new OpenDental.UI.Button();
			this.gridBill = new OpenDental.UI.ODGrid();
			this.labelTotal = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.radioUnsent = new System.Windows.Forms.RadioButton();
			this.radioSent = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labelEmailed = new System.Windows.Forms.Label();
			this.labelPrinted = new System.Windows.Forms.Label();
			this.labelSelected = new System.Windows.Forms.Label();
			this.butEdit = new OpenDental.UI.Button();
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
			this.butNone.Location = new System.Drawing.Point(142,656);
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
			// FormBilling
			// 
			this.AcceptButton = this.butSend;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(891,688);
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
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBilling";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Bills";
			this.Load += new System.EventHandler(this.FormBilling_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormBilling_Load(object sender, System.EventArgs e) {
			//contrAccount1.checkShowAll.Checked=false;
			//textDate.Text=Ledgers.GetClosestFirst(DateTime.Today).ToShortDateString();
			//Patients.GetAgingList();
			Statement stmt;
			for(int i=0;i<AgingList.Length;i++){
				stmt=new Statement();
				//stmt.DateRangeFrom
				//stmt.DateRangeTo
				stmt.DateSent=DateTime.Today;
				//stmt.DocNum
				//stmt.HidePayment
				//stmt.Intermingled
				//stmt.IsSent
				//stmt.Mode_
				//stmt.Note
				//stmt.NoteBold
				//stmt.PatNum
				//stmt.SinglePatient
			
			}




			FillGrid();
			gridBill.SetSelected(true);
			labelSelected.Text=Lan.g(this,"Selected=")+gridBill.SelectedIndices.Length.ToString();
			labelPrinted.Text=Lan.g(this,"Printed=")+"0";
			labelEmailed.Text=Lan.g(this,"Emailed=")+"0";
		}

		private void FillGrid(){
			stmtList=Statements.GetList(radioSent.Checked);
			gridBill.BeginUpdate();
			gridBill.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableBilling","Name"),180);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","Total"),100,HorizontalAlignment.Right);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","-InsuranceEst"),100,HorizontalAlignment.Right);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","=Amount"),100,HorizontalAlignment.Right);
			gridBill.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableBilling","LastStatement"),111);
			gridBill.Columns.Add(col);
			gridBill.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			for(int i=0;i<AgingList.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(AgingList[i].PatName);
				row.Cells.Add(AgingList[i].BalTotal.ToString("F"));
				row.Cells.Add(AgingList[i].InsEst.ToString("F"));
				row.Cells.Add(AgingList[i].AmountDue.ToString("F"));
				if(AgingList[i].DateLastStatement.Year<1880){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(AgingList[i].DateLastStatement.ToShortDateString());
				}
				gridBill.Rows.Add(row);
			}
			gridBill.EndUpdate();
			labelTotal.Text=Lan.g(this,"Total=")+AgingList.Length.ToString();
			labelSelected.Text=Lan.g(this,"Selected=")+"0";
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

		private void butEdit_Click(object sender,EventArgs e) {
			if(gridBill.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select one or more bills first.");
				return;
			}
			FormStatementOptions FormSO=new FormStatementOptions();
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
			FormS.LoadAndPrint(guarNums,GeneralNote);
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
					commlog.IsStatementSent=true;
					commlog.PatNum=guarNums[i];//uaually the guarantor
					Commlogs.Insert(commlog);
				}
			}
			FillGrid();
			if(gridBill.Rows.Count>0 && MsgBox.Show(this,true,"Delete all unsent bills?")){
				MessageBox.Show("Not functional yet.");
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			if(gridBill.Rows.Count>0 && MsgBox.Show(this,true,"Delete all unsent bills?")){
				MessageBox.Show("Not functional yet.");
			}
			DialogResult=DialogResult.Cancel;
		}

		

		

		

	}
}

















