using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormReconcileEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private ValidDate textDate;
		private Label label1;
		private Label label2;
		private ValidDouble textStart;
		private ValidDouble textEnd;
		private Label label3;
		private TextBox textTarget;
		private Label label4;
		private OpenDental.UI.ODGrid gridMain;
		private Label label5;
		private TextBox textSum;
		private CheckBox checkLocked;
		private Reconcile ReconcileCur;
		private JournalEntry[] JournalList;
		private OpenDental.UI.Button butDelete;
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormReconcileEdit(Reconcile reconcileCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			ReconcileCur=reconcileCur;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReconcileEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textTarget = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textSum = new System.Windows.Forms.TextBox();
			this.checkLocked = new System.Windows.Forms.CheckBox();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.textEnd = new OpenDental.ValidDouble();
			this.textStart = new OpenDental.ValidDouble();
			this.textDate = new OpenDental.ValidDate();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(370,4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96,20);
			this.label1.TabIndex = 3;
			this.label1.Text = "Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(342,47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(124,20);
			this.label2.TabIndex = 5;
			this.label2.Text = "Starting Balance";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(342,68);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(124,20);
			this.label3.TabIndex = 7;
			this.label3.Text = "Ending Balance";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTarget
			// 
			this.textTarget.Location = new System.Drawing.Point(467,90);
			this.textTarget.Name = "textTarget";
			this.textTarget.ReadOnly = true;
			this.textTarget.Size = new System.Drawing.Size(100,20);
			this.textTarget.TabIndex = 9;
			this.textTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(353,89);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(113,20);
			this.label4.TabIndex = 10;
			this.label4.Text = "Target Change";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(353,110);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(113,20);
			this.label5.TabIndex = 13;
			this.label5.Text = "Sum of Transactions";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSum
			// 
			this.textSum.Location = new System.Drawing.Point(467,111);
			this.textSum.Name = "textSum";
			this.textSum.ReadOnly = true;
			this.textSum.Size = new System.Drawing.Size(100,20);
			this.textSum.TabIndex = 12;
			this.textSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// checkLocked
			// 
			this.checkLocked.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkLocked.Location = new System.Drawing.Point(374,28);
			this.checkLocked.Name = "checkLocked";
			this.checkLocked.Size = new System.Drawing.Size(108,18);
			this.checkLocked.TabIndex = 14;
			this.checkLocked.Text = "Locked";
			this.checkLocked.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkLocked.UseVisualStyleBackColor = true;
			this.checkLocked.Click += new System.EventHandler(this.checkLocked_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(4,5);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(338,637);
			this.gridMain.TabIndex = 11;
			this.gridMain.Title = "Transactions";
			this.gridMain.TranslationName = "TableJournal";
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			// 
			// textEnd
			// 
			this.textEnd.Location = new System.Drawing.Point(467,69);
			this.textEnd.Name = "textEnd";
			this.textEnd.Size = new System.Drawing.Size(100,20);
			this.textEnd.TabIndex = 8;
			this.textEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textEnd.TextChanged += new System.EventHandler(this.textEnd_TextChanged);
			// 
			// textStart
			// 
			this.textStart.Location = new System.Drawing.Point(467,48);
			this.textStart.Name = "textStart";
			this.textStart.Size = new System.Drawing.Size(100,20);
			this.textStart.TabIndex = 6;
			this.textStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textStart.TextChanged += new System.EventHandler(this.textStart_TextChanged);
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(467,5);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(100,20);
			this.textDate.TabIndex = 2;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(494,616);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(494,648);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(4,648);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(84,26);
			this.butDelete.TabIndex = 15;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormReconcileEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(587,677);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.checkLocked);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textSum);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textTarget);
			this.Controls.Add(this.textEnd);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textStart);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormReconcileEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Reconcile";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormReconcileEdit_FormClosing);
			this.Load += new System.EventHandler(this.FormReconcileEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormReconcileEdit_Load(object sender,EventArgs e) {
			textDate.Text=ReconcileCur.DateReconcile.ToShortDateString();
			checkLocked.Checked=ReconcileCur.IsLocked;
			textStart.Text=ReconcileCur.StartingBal.ToString("n");
			textEnd.Text=ReconcileCur.EndingBal.ToString("n");
			textTarget.Text=(ReconcileCur.EndingBal-ReconcileCur.StartingBal).ToString("n");
			bool includeUncleared=!ReconcileCur.IsLocked;
			JournalList=JournalEntries.GetForReconcile(ReconcileCur.AccountNum,includeUncleared,ReconcileCur.ReconcileNum);
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableJournal","Chk #"),60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableJournal","Date"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableJournal","Deposits"),70,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableJournal","Withdrawals"),70,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("X",30,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			double sum=0;
			for(int i=0;i<JournalList.Length;i++) {
				row=new ODGridRow();
				row.Cells.Add(JournalList[i].CheckNumber);
				row.Cells.Add(JournalList[i].DateDisplayed.ToShortDateString());
				//row.Cells.Add(JournalList[i].Memo);
				//row.Cells.Add(JournalList[i].Splits);
				if(JournalList[i].DebitAmt==0) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(JournalList[i].DebitAmt.ToString("n"));
					if(JournalList[i].ReconcileNum!=0){
						sum+=JournalList[i].DebitAmt;
					}
				}
				if(JournalList[i].CreditAmt==0) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(JournalList[i].CreditAmt.ToString("n"));
					if(JournalList[i].ReconcileNum!=0){
						sum-=JournalList[i].CreditAmt;
					}
				}
				if(JournalList[i].ReconcileNum==0){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add("X");
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			textSum.Text=sum.ToString("n");
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			if(e.Col != 4){
				return;
			}
			if(checkLocked.Checked){
				return;
			}
			if(JournalList[e.Row].ReconcileNum==0){
				JournalList[e.Row].ReconcileNum=ReconcileCur.ReconcileNum;
			}
			else{
				JournalList[e.Row].ReconcileNum=0;
			}
			int rowClicked=e.Row;
			FillGrid();
			gridMain.SetSelected(rowClicked,true);
		}

		private void textStart_TextChanged(object sender,EventArgs e) {
			if(textStart.errorProvider1.GetError(textStart)!=""
				|| textEnd.errorProvider1.GetError(textEnd)!=""
				) {
				return;
			}
			textTarget.Text=(PIn.PDouble(textEnd.Text)-PIn.PDouble(textStart.Text)).ToString("n");
		}

		private void textEnd_TextChanged(object sender,EventArgs e) {
			if(textStart.errorProvider1.GetError(textStart)!=""
				|| textEnd.errorProvider1.GetError(textEnd)!=""
				) {
				return;
			}
			textTarget.Text=(PIn.PDouble(textEnd.Text)-PIn.PDouble(textStart.Text)).ToString("n");
		}

		private void checkLocked_Click(object sender,EventArgs e) {
			if(checkLocked.Checked){
				if(textTarget.Text != textSum.Text){
					MsgBox.Show(this,"Target change must match sum of transactions.");
					checkLocked.Checked=false;
					return;
				}
			}
			else{//unchecking
				//need to check permissions here.
			}
			SaveList();
			bool includeUncleared=!checkLocked.Checked;
			JournalList=JournalEntries.GetForReconcile(ReconcileCur.AccountNum,includeUncleared,ReconcileCur.ReconcileNum);
			FillGrid();
		}

		///<summary>Saves all changes to JournalList to database.  Can only be called once when closing form.</summary>
		private void SaveList(){
			JournalEntries.SaveList(JournalList,ReconcileCur.ReconcileNum);
		}

		private void butDelete_Click(object sender,EventArgs e) {
			try{
				Reconciles.Delete(ReconcileCur);
				DialogResult=DialogResult.OK;
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDate.errorProvider1.GetError(textDate)!=""
				|| textStart.errorProvider1.GetError(textStart)!=""
				|| textEnd.errorProvider1.GetError(textEnd)!=""
				) {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			ReconcileCur.DateReconcile=PIn.PDate(textDate.Text);
			ReconcileCur.StartingBal=PIn.PDouble(textStart.Text);
			ReconcileCur.EndingBal=PIn.PDouble(textEnd.Text);
			ReconcileCur.IsLocked=checkLocked.Checked;
			Reconciles.Update(ReconcileCur);
			SaveList();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormReconcileEdit_FormClosing(object sender,FormClosingEventArgs e) {
			if(DialogResult==DialogResult.OK){
				return;
			}
			if(IsNew){
				for(int i=0;i<JournalList.Length;i++){
					JournalList[i].ReconcileNum=0;
				}
				SaveList();//detaches all journal entries.
				Reconciles.Delete(ReconcileCur);
			}
		}

	

		

		

		

		


	}
}





















