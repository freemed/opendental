using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormProviderIncTrans : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private Label label1;
		private TextBox textFamEnd;
		private Label label10;
		private TextBox textFamStart;
		private OpenDental.UI.ODGrid gridBal;
		private OpenDental.UI.ODGrid gridMain;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary>This table gets created and filled once at the beginning.  After that, only the last column gets carefully updated.</summary>
		private DataTable tableBalances;
		///<summary>This needs to be set ahead of time externally.  It needs to be saved to the database before this form is opened.</summary>
		public Payment PaymentCur;
		///<summary>The patnum of the patient from which this window was opened.  There is a chance the family could be different from the one that most of the transfer refers to.</summary>
		public int PatNum;
		private OpenDental.UI.Button butDeleteAll;
		///<summary>The patient from which this window was accessed.</summary>
		private Patient PatCur;
		private ODtextBox textNote;
		private Label label2;
		public bool IsNew;
		///<summary></summary>
		private List<PaySplit> SplitList;
		private List<PaySplit> SplitListOld;
		private ValidDate textDateEntry;
		private Label label12;
		private ValidDate textDate;
		private Label label6;
		private GroupBox groupBox1;
		private OpenDental.UI.Button butTransfer;
		private Label label3;
		private ComboBox comboProv;
		private OpenDental.UI.Button butAdd;
		private Family FamCur;

		///<summary></summary>
		public FormProviderIncTrans()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProviderIncTrans));
			this.label1 = new System.Windows.Forms.Label();
			this.textFamEnd = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textFamStart = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboProv = new System.Windows.Forms.ComboBox();
			this.butAdd = new OpenDental.UI.Button();
			this.butTransfer = new OpenDental.UI.Button();
			this.textDateEntry = new OpenDental.ValidDate();
			this.textDate = new OpenDental.ValidDate();
			this.textNote = new OpenDental.ODtextBox();
			this.butDeleteAll = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.gridBal = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(95,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(554,48);
			this.label1.TabIndex = 2;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// textFamEnd
			// 
			this.textFamEnd.Location = new System.Drawing.Point(653,390);
			this.textFamEnd.Name = "textFamEnd";
			this.textFamEnd.ReadOnly = true;
			this.textFamEnd.Size = new System.Drawing.Size(56,20);
			this.textFamEnd.TabIndex = 127;
			this.textFamEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(491,393);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100,16);
			this.label10.TabIndex = 126;
			this.label10.Text = "Family Total";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFamStart
			// 
			this.textFamStart.Location = new System.Drawing.Point(592,390);
			this.textFamStart.Name = "textFamStart";
			this.textFamStart.ReadOnly = true;
			this.textFamStart.Size = new System.Drawing.Size(61,20);
			this.textFamStart.TabIndex = 125;
			this.textFamStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(4,100);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92,16);
			this.label2.TabIndex = 131;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(-4,62);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100,16);
			this.label12.TabIndex = 136;
			this.label12.Text = "Entry Date";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(-4,83);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100,16);
			this.label6.TabIndex = 134;
			this.label6.Text = "Payment Date";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboProv);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.butTransfer);
			this.groupBox1.Location = new System.Drawing.Point(464,94);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(265,88);
			this.groupBox1.TabIndex = 137;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Transfer";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4,18);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(260,33);
			this.label3.TabIndex = 137;
			this.label3.Text = "Highlight a row in the grid below.  Select the provider to transfer that amount t" +
    "o.  Click Transfer.";
			// 
			// comboProv
			// 
			this.comboProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProv.FormattingEnabled = true;
			this.comboProv.Location = new System.Drawing.Point(99,61);
			this.comboProv.MaxDropDownItems = 40;
			this.comboProv.Name = "comboProv";
			this.comboProv.Size = new System.Drawing.Size(160,21);
			this.comboProv.TabIndex = 138;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(98,390);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(92,24);
			this.butAdd.TabIndex = 138;
			this.butAdd.Text = "&Add Split";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butTransfer
			// 
			this.butTransfer.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butTransfer.Autosize = true;
			this.butTransfer.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTransfer.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTransfer.CornerRadius = 4F;
			this.butTransfer.Image = global::OpenDental.Properties.Resources.Left;
			this.butTransfer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butTransfer.Location = new System.Drawing.Point(6,59);
			this.butTransfer.Name = "butTransfer";
			this.butTransfer.Size = new System.Drawing.Size(79,24);
			this.butTransfer.TabIndex = 129;
			this.butTransfer.Text = "Transfer";
			this.butTransfer.Click += new System.EventHandler(this.butTransfer_Click);
			// 
			// textDateEntry
			// 
			this.textDateEntry.Location = new System.Drawing.Point(98,58);
			this.textDateEntry.Name = "textDateEntry";
			this.textDateEntry.ReadOnly = true;
			this.textDateEntry.Size = new System.Drawing.Size(100,20);
			this.textDateEntry.TabIndex = 135;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(98,79);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(100,20);
			this.textDate.TabIndex = 133;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(98,100);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Payment;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(290,80);
			this.textNote.TabIndex = 132;
			// 
			// butDeleteAll
			// 
			this.butDeleteAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDeleteAll.Autosize = true;
			this.butDeleteAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDeleteAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDeleteAll.CornerRadius = 4F;
			this.butDeleteAll.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDeleteAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDeleteAll.Location = new System.Drawing.Point(34,517);
			this.butDeleteAll.Name = "butDeleteAll";
			this.butDeleteAll.Size = new System.Drawing.Size(84,26);
			this.butDeleteAll.TabIndex = 130;
			this.butDeleteAll.Text = "&Delete";
			this.butDeleteAll.Click += new System.EventHandler(this.butDeleteAll_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(98,186);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(365,198);
			this.gridMain.TabIndex = 129;
			this.gridMain.Title = "Transfers";
			this.gridMain.TranslationName = "FormProviderIncTrans";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// gridBal
			// 
			this.gridBal.HScrollVisible = false;
			this.gridBal.Location = new System.Drawing.Point(469,186);
			this.gridBal.Name = "gridBal";
			this.gridBal.ScrollValue = 0;
			this.gridBal.Size = new System.Drawing.Size(254,198);
			this.gridBal.TabIndex = 124;
			this.gridBal.Title = "Family Balances";
			this.gridBal.TranslationName = "TablePaymentBal";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(648,476);
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
			this.butCancel.Location = new System.Drawing.Point(648,517);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormProviderIncTrans
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(769,564);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textDateEntry);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butDeleteAll);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.textFamEnd);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textFamStart);
			this.Controls.Add(this.gridBal);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProviderIncTrans";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Provider Income Transfer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProviderIncTrans_FormClosing);
			this.Load += new System.EventHandler(this.FormProviderIncTrans_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormProviderIncTrans_Load(object sender,EventArgs e) {
			if(IsNew) {
				if(!Security.IsAuthorized(Permissions.PaymentCreate)) {
					DialogResult=DialogResult.Cancel;
					return;
				}
				PaymentCur.DateEntry=DateTime.Now;
			}
			else {
				if(!Security.IsAuthorized(Permissions.PaymentEdit,PaymentCur.DateEntry)) {
					butOK.Enabled=false;
					butDeleteAll.Enabled=false;
					butAdd.Enabled=false;
					gridMain.Enabled=false;
					//butPay.Enabled=false;
				}
			}
			for(int i=0;i<Providers.List.Length;i++) {
				comboProv.Items.Add(Providers.List[i].Abbr);
			}
			FamCur=Patients.GetFamily(PatNum);
			PatCur=FamCur.GetPatient(PatNum);
			tableBalances=Patients.GetPaymentStartingBalances(PatCur.Guarantor,PaymentCur.PayNum);
			textDateEntry.Text=PaymentCur.DateEntry.ToShortDateString();
			textDate.Text=PaymentCur.PayDate.ToShortDateString();
			textNote.Text=PaymentCur.PayNote;
			SplitList=PaySplits.GetForPayment(PaymentCur.PayNum);//Count might be 0
			SplitListOld=new List<PaySplit>();
			SplitListOld.AddRange(SplitList);
			FillMain();
		}

		///<summary>This does not make any calls to db (except one tiny one).  Simply refreshes screen for SplitList.</summary>
		private void FillMain(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("FormProviderIncTrans","Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormProviderIncTrans","Prov"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormProviderIncTrans","Patient"),130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormProviderIncTrans","Amount"),60,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			Procedure proc;
			for(int i=0;i<SplitList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(SplitList[i].ProcDate.ToShortDateString());
				row.Cells.Add(Providers.GetAbbr(SplitList[i].ProvNum));
				row.Cells.Add(FamCur.GetNameInFamFL(SplitList[i].PatNum));
				row.Cells.Add(SplitList[i].SplitAmt.ToString("F"));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			FillGridBal();
		}

		///<summary></summary>
		private void FillGridBal() {
			double famstart=0;
			for(int i=0;i<tableBalances.Rows.Count;i++) {
				famstart+=PIn.PDouble(tableBalances.Rows[i]["StartBal"].ToString());
			}
			textFamStart.Text=famstart.ToString("N");
			//compute ending balances-----------------------------------------------------------------------------
			for(int i=0;i<tableBalances.Rows.Count;i++) {
				tableBalances.Rows[i]["EndBal"]=tableBalances.Rows[i]["StartBal"].ToString();
			}
			double amt;
			for(int i=0;i<SplitList.Count;i++) {
				for(int f=0;f<tableBalances.Rows.Count;f++) {
					if(tableBalances.Rows[f]["PatNum"].ToString()!=SplitList[i].PatNum.ToString()) {
						continue;
					}
					if(tableBalances.Rows[f]["ProvNum"].ToString()!=SplitList[i].ProvNum.ToString()) {
						continue;
					}
					amt=PIn.PDouble(tableBalances.Rows[f]["EndBal"].ToString())-SplitList[i].SplitAmt;
					tableBalances.Rows[f]["EndBal"]=amt.ToString("N");
				}
			}
			double famend=0;
			for(int i=0;i<tableBalances.Rows.Count;i++) {
				famend+=PIn.PDouble(tableBalances.Rows[i]["EndBal"].ToString());
			}
			textFamEnd.Text=famend.ToString("N");
			//fill grid--------------------------------------------------------------------------------------------
			gridBal.BeginUpdate();
			gridBal.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePaymentBal","Prov"),60);
			gridBal.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentBal","Patient"),62);
			gridBal.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentBal","Start"),60,HorizontalAlignment.Right);
			gridBal.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentBal","End"),60,HorizontalAlignment.Right);
			gridBal.Columns.Add(col);
			gridBal.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<tableBalances.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(Providers.GetAbbr(PIn.PInt(tableBalances.Rows[i]["ProvNum"].ToString())));
				if(tableBalances.Rows[i]["Preferred"].ToString()=="") {
					row.Cells.Add(tableBalances.Rows[i]["FName"].ToString());
				}
				else {
					row.Cells.Add("'"+tableBalances.Rows[i]["Preferred"].ToString()+"'");
				}
				row.Cells.Add(PIn.PDouble(tableBalances.Rows[i]["StartBal"].ToString()).ToString("N"));
				row.Cells.Add(PIn.PDouble(tableBalances.Rows[i]["EndBal"].ToString()).ToString("N"));
				//row.ColorBackG=SystemColors.Control;//Color.FromArgb(240,240,240);
				gridBal.Rows.Add(row);
			}
			gridBal.EndUpdate();
		}

		/*private void butAdd_Click(object sender,EventArgs e) {
			FormProvIncTransAdd FormP=new FormProvIncTransAdd();
			FormP.FamCur=FamCur;
			FormP.PayNum=PaymentCur.PayNum;
			FormP.PatNum=PatCur.PatNum;
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
			SplitList.AddRange(FormP.SplitList);
			FillMain();
		}*/

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormPaySplitEdit FormPS=new FormPaySplitEdit(FamCur);
			FormPS.PaySplitCur=SplitList[e.Row];
			double total=0;
			for(int i=0;i<SplitList.Count;i++) {
				total+=SplitList[i].SplitAmt;
			}
			FormPS.Remain=0-total+SplitList[e.Row].SplitAmt;
			FormPS.ShowDialog();
			if(FormPS.PaySplitCur==null) {//user deleted
				SplitList.RemoveAt(e.Row);
			}
			FillMain();
		}

		private void butTransfer_Click(object sender,EventArgs e) {
			if(gridBal.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select a row from the Family Balances grid first.");
				return;
			}
			if(comboProv.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a provider first.");
				return;
			}
			double amt=PIn.PDouble(tableBalances.Rows[gridBal.GetSelectedIndex()]["StartBal"].ToString());
			//From-----------------------------------------------------------------------------------
			PaySplit split=new PaySplit();
			split.PatNum=PatNum;
			split.PayNum=PaymentCur.PayNum;
			split.ProcDate=PIn.PDate(textDate.Text);//this may be updated upon closing
			split.DatePay=PIn.PDate(textDate.Text);//this may be updated upon closing
			split.ProvNum=PIn.PInt(tableBalances.Rows[gridBal.GetSelectedIndex()]["ProvNum"].ToString());
			split.SplitAmt=amt;
			SplitList.Add(split);
			//To-----------------------------------------------------------------------------------
			split=new PaySplit();
			split.PatNum=PatNum;
			split.PayNum=PaymentCur.PayNum;
			split.ProcDate=PIn.PDate(textDate.Text);
			split.DatePay=PIn.PDate(textDate.Text);
			split.ProvNum=Providers.List[comboProv.SelectedIndex].ProvNum;
			split.SplitAmt=-amt;
			SplitList.Add(split);
			FillMain();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			PaySplit PaySplitCur=new PaySplit();
			PaySplitCur.PayNum=PaymentCur.PayNum;
			PaySplitCur.DatePay=PIn.PDate(textDate.Text);//this may be updated upon closing
			PaySplitCur.ProcDate=PIn.PDate(textDate.Text);//this may be updated upon closing
			if(gridBal.GetSelectedIndex()==-1){
				PaySplitCur.ProvNum=Patients.GetProvNum(PatCur);
			}
			else{
				PaySplitCur.ProvNum=PIn.PInt(tableBalances.Rows[gridBal.GetSelectedIndex()]["ProvNum"].ToString());
				PaySplitCur.SplitAmt=-PIn.PDouble(tableBalances.Rows[gridBal.GetSelectedIndex()]["StartBal"].ToString());
			}
			PaySplitCur.PatNum=PatCur.PatNum;
			FormPaySplitEdit FormPS=new FormPaySplitEdit(FamCur);
			FormPS.PaySplitCur=PaySplitCur;
			FormPS.IsNew=true;
			double total=0;
			for(int i=0;i<SplitList.Count;i++) {
				total+=SplitList[i].SplitAmt;
			}
			FormPS.Remain=PaymentCur.PayAmt-total;
			if(FormPS.ShowDialog()!=DialogResult.OK) {
				return;
			}
			SplitList.Add(PaySplitCur);
			FillMain();
		}

		private void butDeleteAll_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"Delete?")) {
				return;
			}
			try {
				Payments.Delete(PaymentCur);
			}
			catch(ApplicationException ex) {//error if attached to deposit slip (not possible)
				MessageBox.Show(ex.Message);
				return;
			}
			if(!IsNew){
				SecurityLogs.MakeLogEntry(Permissions.PaymentEdit,0,Lan.g(this,"Delete Prov income transfer."));
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDate.errorProvider1.GetError(textDate)!="") {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(SplitList.Count==0){
				MsgBox.Show(this,"Please enter transfers first.");
				return;
			}
			double total=0;
			for(int i=0;i<SplitList.Count;i++){
				total+=SplitList[i].SplitAmt;
			}
			if(total!=0) {
				MsgBox.Show(this,"Total must equal zero.");
				return;
			}
			PaymentCur.PayNote=textNote.Text;
			PaymentCur.PayDate=PIn.PDate(textDate.Text);
			PaymentCur.IsSplit=true;
			try {
				Payments.Update(PaymentCur);
			}
			catch(ApplicationException ex) {//this catches bad dates.
				MessageBox.Show(ex.Message);
				return;
			}
			//Set all DatePays the same.
			for(int i=0;i<SplitList.Count;i++) {
				SplitList[i].DatePay=PaymentCur.PayDate;
				SplitList[i].ProcDate=PaymentCur.PayDate;
			}
			PaySplits.UpdateList(SplitListOld,SplitList);
			if(IsNew) {
				SecurityLogs.MakeLogEntry(Permissions.PaymentCreate,0,Lan.g(this,"Prov income transfer."));
			}
			else {
				SecurityLogs.MakeLogEntry(Permissions.PaymentEdit,0,Lan.g(this,"Prov income transfer."));
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormProviderIncTrans_FormClosing(object sender,FormClosingEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew) {
				Payments.Delete(PaymentCur);
			}
		}

		

		

		

		

		

		


	}
}





















