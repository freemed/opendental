using System;
using System.Drawing;
using System.Collections;
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
		private OpenDental.UI.Button butPrint;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.ODGrid gridBill;
		///<summary>Set this list externally before openning the billing window.</summary>
		public PatAging[] AgingList;
		private Label labelCount;
		private Label label1;
		private RadioButton radioUnsent;
		private RadioButton radioButton1;
		///<summary></summary>
		public string GeneralNote;

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
			this.butPrint = new OpenDental.UI.Button();
			this.butNone = new OpenDental.UI.Button();
			this.butAll = new OpenDental.UI.Button();
			this.gridBill = new OpenDental.UI.ODGrid();
			this.labelCount = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.radioUnsent = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
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
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BackColor = System.Drawing.SystemColors.Control;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Location = new System.Drawing.Point(805,620);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(75,24);
			this.butPrint.TabIndex = 0;
			this.butPrint.Text = "Send";
			this.butPrint.UseVisualStyleBackColor = false;
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
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
			this.gridBill.Size = new System.Drawing.Size(610,615);
			this.gridBill.TabIndex = 28;
			this.gridBill.Title = "Bills";
			this.gridBill.TranslationName = "TableBilling";
			// 
			// labelCount
			// 
			this.labelCount.Location = new System.Drawing.Point(504,660);
			this.labelCount.Name = "labelCount";
			this.labelCount.Size = new System.Drawing.Size(130,16);
			this.labelCount.TabIndex = 29;
			this.labelCount.Text = "Count=20";
			this.labelCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(771,547);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(110,68);
			this.label1.TabIndex = 30;
			this.label1.Text = "This will immediately print or email all selected bills";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// radioUnsent
			// 
			this.radioUnsent.Location = new System.Drawing.Point(42,7);
			this.radioUnsent.Name = "radioUnsent";
			this.radioUnsent.Size = new System.Drawing.Size(102,20);
			this.radioUnsent.TabIndex = 31;
			this.radioUnsent.TabStop = true;
			this.radioUnsent.Text = "Unsent";
			this.radioUnsent.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.Location = new System.Drawing.Point(145,7);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(89,20);
			this.radioButton1.TabIndex = 32;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Sent";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// FormBilling
			// 
			this.AcceptButton = this.butPrint;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(891,688);
			this.Controls.Add(this.radioButton1);
			this.Controls.Add(this.radioUnsent);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.labelCount);
			this.Controls.Add(this.gridBill);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butPrint);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBilling";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Bills";
			this.Load += new System.EventHandler(this.FormBilling_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormBilling_Load(object sender, System.EventArgs e) {
			//contrAccount1.checkShowAll.Checked=false;
			//textDate.Text=Ledgers.GetClosestFirst(DateTime.Today).ToShortDateString();
			//Patients.GetAgingList();
			FillTable();
			gridBill.SetSelected(true);	
		}

		private void FillTable(){
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
			labelCount.Text=Lan.g(this,"Count=")+AgingList.Length.ToString();
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			gridBill.SetSelected(true);
		}

		private void butNone_Click(object sender, System.EventArgs e) {	
			gridBill.SetSelected(false);
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
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
			DialogResult=DialogResult.OK;
		}

		

	}
}

















