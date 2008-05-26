using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormJournal:System.Windows.Forms.Form {
		private OpenDental.UI.ODToolBar ToolBarMain;
		private OpenDental.UI.ODGrid gridMain;
		private IContainer components;
		private Account AccountCur;
		private ImageList imageListMain;
		private JournalEntry[] JournalList;
		private PrintDocument pd2;
		private bool headingPrinted;
		private int pagesPrinted;
		private int headingPrintH;
		private Label label1;
		private ValidDate textDateFrom;
		private ValidDate textDateTo;
		private Label label2;
		private OpenDental.UI.Button butRefresh;
		private MonthCalendar calendarFrom;
		private OpenDental.UI.Button butDropFrom;
		private OpenDental.UI.Button butDropTo;
		private MonthCalendar calendarTo;
		private bool isPrinting;
		private ValidDouble textAmt;
		private Label label3;
		private Label label4;
		private TextBox textFindText;
		//set this externally so that the ending balances will match what's showing in the Chart of Accounts.
		public DateTime InitialAsOfDate;

		///<summary></summary>
		public FormJournal(Account accountCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			AccountCur=accountCur;
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormJournal));
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.gridMain = new OpenDental.UI.ODGrid();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.label1 = new System.Windows.Forms.Label();
			this.textDateFrom = new OpenDental.ValidDate();
			this.textDateTo = new OpenDental.ValidDate();
			this.label2 = new System.Windows.Forms.Label();
			this.butRefresh = new OpenDental.UI.Button();
			this.calendarFrom = new System.Windows.Forms.MonthCalendar();
			this.butDropFrom = new OpenDental.UI.Button();
			this.butDropTo = new OpenDental.UI.Button();
			this.calendarTo = new System.Windows.Forms.MonthCalendar();
			this.textAmt = new OpenDental.ValidDouble();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textFindText = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"Add.gif");
			this.imageListMain.Images.SetKeyName(1,"print.gif");
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(0,56);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(844,615);
			this.gridMain.TabIndex = 1;
			this.gridMain.Title = null;
			this.gridMain.TranslationName = "TableJournal";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(844,29);
			this.ToolBarMain.TabIndex = 0;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(2,31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75,18);
			this.label1.TabIndex = 2;
			this.label1.Text = "From";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(78,32);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(81,20);
			this.textDateFrom.TabIndex = 3;
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(268,32);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(81,20);
			this.textDateTo.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(195,31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72,18);
			this.label2.TabIndex = 4;
			this.label2.Text = "To";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(711,31);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(75,23);
			this.butRefresh.TabIndex = 6;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.UseVisualStyleBackColor = true;
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// calendarFrom
			// 
			this.calendarFrom.Location = new System.Drawing.Point(5,56);
			this.calendarFrom.MaxSelectionCount = 1;
			this.calendarFrom.Name = "calendarFrom";
			this.calendarFrom.TabIndex = 7;
			this.calendarFrom.Visible = false;
			this.calendarFrom.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calendarFrom_DateSelected);
			// 
			// butDropFrom
			// 
			this.butDropFrom.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDropFrom.Autosize = true;
			this.butDropFrom.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDropFrom.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDropFrom.CornerRadius = 4F;
			this.butDropFrom.Location = new System.Drawing.Point(161,30);
			this.butDropFrom.Name = "butDropFrom";
			this.butDropFrom.Size = new System.Drawing.Size(22,23);
			this.butDropFrom.TabIndex = 8;
			this.butDropFrom.Text = "V";
			this.butDropFrom.UseVisualStyleBackColor = true;
			this.butDropFrom.Click += new System.EventHandler(this.butDropFrom_Click);
			// 
			// butDropTo
			// 
			this.butDropTo.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDropTo.Autosize = true;
			this.butDropTo.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDropTo.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDropTo.CornerRadius = 4F;
			this.butDropTo.Location = new System.Drawing.Point(351,30);
			this.butDropTo.Name = "butDropTo";
			this.butDropTo.Size = new System.Drawing.Size(22,23);
			this.butDropTo.TabIndex = 9;
			this.butDropTo.Text = "V";
			this.butDropTo.UseVisualStyleBackColor = true;
			this.butDropTo.Click += new System.EventHandler(this.butDropTo_Click);
			// 
			// calendarTo
			// 
			this.calendarTo.Location = new System.Drawing.Point(195,56);
			this.calendarTo.MaxSelectionCount = 1;
			this.calendarTo.Name = "calendarTo";
			this.calendarTo.TabIndex = 10;
			this.calendarTo.Visible = false;
			this.calendarTo.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calendarTo_DateSelected);
			// 
			// textAmt
			// 
			this.textAmt.ForeColor = System.Drawing.SystemColors.WindowText;
			this.textAmt.Location = new System.Drawing.Point(450,32);
			this.textAmt.Name = "textAmt";
			this.textAmt.Size = new System.Drawing.Size(81,20);
			this.textAmt.TabIndex = 11;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(387,32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(63,18);
			this.label3.TabIndex = 12;
			this.label3.Text = "Find Amt";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(537,32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68,18);
			this.label4.TabIndex = 13;
			this.label4.Text = "Find Text";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// textFindText
			// 
			this.textFindText.Location = new System.Drawing.Point(605,32);
			this.textFindText.Name = "textFindText";
			this.textFindText.Size = new System.Drawing.Size(78,20);
			this.textFindText.TabIndex = 14;
			// 
			// FormJournal
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(844,671);
			this.Controls.Add(this.textFindText);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textAmt);
			this.Controls.Add(this.calendarTo);
			this.Controls.Add(this.butDropTo);
			this.Controls.Add(this.butDropFrom);
			this.Controls.Add(this.calendarFrom);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.textDateTo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textDateFrom);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.ToolBarMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormJournal";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Transaction History";
			this.Load += new System.EventHandler(this.FormJournal_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormJournal_Load(object sender,EventArgs e) {
			DateTime firstofYear=new DateTime(InitialAsOfDate.Year,1,1);
			textDateTo.Text=InitialAsOfDate.ToShortDateString();
			if(AccountCur.AcctType==AccountType.Income || AccountCur.AcctType==AccountType.Expense){
				textDateFrom.Text=firstofYear.ToShortDateString();
			}
			LayoutToolBar();
			FillGrid();
			gridMain.ScrollToEnd();
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add Entry"),0,"","Add"));
			if(AccountCur.AcctType==AccountType.Asset){
				ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Reconcile"),-1,"","Reconcile"));
			}
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Print"),1,"","Print"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Edit"),-1,Lan.g(this,"Edit Selected Account"),"Edit"));
			//ODToolBarButton button=new ODToolBarButton("",-1,"","PageNum");
			//button.Style=ODToolBarButtonStyle.Label;
			//ToolBarMain.Buttons.Add(button);
			//ToolBarMain.Buttons.Add(new ODToolBarButton("",2,"Go Forward One Page","Fwd"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Close"),-1,"Close This Window","Close"));
		}

		private void ToolBarMain_ButtonClick(object sender,OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			switch(e.Button.Tag.ToString()) {
				case "Add":
					Add_Click();
					break;
				case "Reconcile":
					Reconcile_Click();
					break;
				case "Print":
					Print_Click();
					break;
				case "Close":
					this.Close();
					break;
			}
		}

		private void FillGrid(){
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				return;
			}
			DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			DateTime dateTo;
			if(textDateTo.Text==""){
				dateTo=DateTime.MaxValue;
			}
			else{
				dateTo=PIn.PDate(textDateTo.Text);
			}
			double filterAmt=0;
			if(textAmt.errorProvider1.GetError(textAmt)==""){
				filterAmt=PIn.PDouble(textAmt.Text);
			}
			JournalList=JournalEntries.GetForAccount(AccountCur.AccountNum);
			int scroll=gridMain.ScrollValue;
			gridMain.BeginUpdate();
			gridMain.Title=AccountCur.Description+" ("+Lan.g("enumAccountType",AccountCur.AcctType.ToString())+")";
			gridMain.Columns.Clear();
			string str="";
			ODGridColumn col=new ODGridColumn(Lan.g("TableJournal","Chk #"),60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableJournal","Date"),80);
			gridMain.Columns.Add(col);
			if(isPrinting){
				col=new ODGridColumn(Lan.g("TableJournal","Memo"),200);
			}
			else{
				col=new ODGridColumn(Lan.g("TableJournal","Memo"),220);
			}
			gridMain.Columns.Add(col);
			if(isPrinting){
				col=new ODGridColumn(Lan.g("TableJournal","Splits"),200);
			}
			else{
				col=new ODGridColumn(Lan.g("TableJournal","Splits"),220);
			}
			gridMain.Columns.Add(col);
			str=Lan.g("TableJournal","Debit");
			if(Accounts.DebitIsPos(AccountCur.AcctType)){
				str+=Lan.g("TableJournal","(+)");
			}
			else{
				str+=Lan.g("TableJournal","(-)");
			}
			col=new ODGridColumn(str,65,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			str=Lan.g("TableJournal","Credit");
			if(Accounts.DebitIsPos(AccountCur.AcctType)) {
				str+=Lan.g("TableJournal","(-)");
			}
			else {
				str+=Lan.g("TableJournal","(+)");
			}
			col=new ODGridColumn(str,65,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableJournal","Balance"),65,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableJournal","Clear"),55,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			double bal=0;
			for(int i=0;i<JournalList.Length;i++){
				if(JournalList[i].DateDisplayed > dateTo) {
					break;
				}
				if(AccountCur.AcctType==AccountType.Income
					|| AccountCur.AcctType==AccountType.Expense)
				{
					if(JournalList[i].DateDisplayed < dateFrom) {
						continue;
						//for income and expense accounts, previous balances are not included. Only the current timespan.
					}
				}
				if(JournalList[i].DebitAmt!=0) {
					if(Accounts.DebitIsPos(AccountCur.AcctType)) {//this one is used for checking account
						bal+=JournalList[i].DebitAmt;
					}
					else {
						bal-=JournalList[i].DebitAmt;
					}
				}
				if(JournalList[i].CreditAmt!=0) {
					if(Accounts.DebitIsPos(AccountCur.AcctType)) {//this one is used for checking account
						bal-=JournalList[i].CreditAmt;
					}
					else {
						bal+=JournalList[i].CreditAmt;
					}
				}
				if(AccountCur.AcctType==AccountType.Asset
					|| AccountCur.AcctType==AccountType.Liability
					|| AccountCur.AcctType==AccountType.Equity)
				{
					if(JournalList[i].DateDisplayed < dateFrom) {
						continue;
						//for asset, liability, and equity accounts, older entries do affect the current balance.
					}
				}
				if(filterAmt!=0 && filterAmt!=JournalList[i].CreditAmt && filterAmt!=JournalList[i].DebitAmt){
					continue;
				}
				if(textFindText.Text!="" 
					&& !JournalList[i].Memo.ToUpper().Contains(textFindText.Text.ToUpper()) 
					&& !JournalList[i].CheckNumber.ToUpper().Contains(textFindText.Text.ToUpper())
					&& !JournalList[i].Splits.ToUpper().Contains(textFindText.Text.ToUpper()))
				{
					continue;
				}
				row=new ODGridRow();
				row.Cells.Add(JournalList[i].CheckNumber);
				row.Cells.Add(JournalList[i].DateDisplayed.ToShortDateString());
				row.Cells.Add(JournalList[i].Memo);
				row.Cells.Add(JournalList[i].Splits);
				if(JournalList[i].DebitAmt==0){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(JournalList[i].DebitAmt.ToString("n"));
				}
				if(JournalList[i].CreditAmt==0) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(JournalList[i].CreditAmt.ToString("n"));
				}
				row.Cells.Add(bal.ToString("n"));
				if(JournalList[i].ReconcileNum==0){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add("X");
				}
				row.Tag=JournalList[i].Copy();
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.ScrollToEnd();
		}

		private void Add_Click(){
			Transaction trans=new Transaction();
			trans.UserNum=Security.CurUser.UserNum;
			Transactions.Insert(trans);//we now have a TransactionNum, and datetimeEntry has been set
			FormTransactionEdit FormT=new FormTransactionEdit(trans.TransactionNum,AccountCur.AccountNum);
			FormT.IsNew=true;
			FormT.ShowDialog();
			if(FormT.DialogResult==DialogResult.Cancel){
				//no need to try-catch, since no journal entries were saved.
				Transactions.Delete(trans);
			}
			FillGrid();
		}

		private void Reconcile_Click() {
			FormReconciles FormR=new FormReconciles(AccountCur.AccountNum);
			FormR.ShowDialog();
			FillGrid();
		}

		private void Print_Click(){
			pagesPrinted=0;
			headingPrinted=false;
			#if DEBUG
				PrintReport(true);
			#else
				PrintReport(false);	
			#endif
		}

		///<summary>Preview is only used for debugging.</summary>
		public void PrintReport(bool justPreview) {
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd2.OriginAtMargins=true;
			if(pd2.DefaultPageSettings.PaperSize.Height==0) {
				pd2.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			isPrinting=true;
			FillGrid();
			try {
				if(justPreview) {
					FormRpPrintPreview pView = new FormRpPrintPreview();
					pView.printPreviewControl2.Document=pd2;
					pView.ShowDialog();
				}
				else {
					if(Printers.SetPrinter(pd2,PrintSituation.Default)) {
						pd2.Print();
					}
				}
			}
			catch {
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
			isPrinting=false;
			FillGrid();
		}

		private void pd2_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=new Rectangle(50,40,800,1035);//Some printers can handle up to 1042
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			int yPos=bounds.Top;
			int center=bounds.X+bounds.Width/2;
			#region printHeading
			if(!headingPrinted) {
				text=AccountCur.Description+" ("+Lan.g("enumAccountType",AccountCur.AcctType.ToString())+")";
				g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,headingFont).Height;
				text=DateTime.Today.ToShortDateString();
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

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			int selectedRow=e.Row;
			FormTransactionEdit FormT=new FormTransactionEdit(
				((JournalEntry)gridMain.Rows[e.Row].Tag).TransactionNum,AccountCur.AccountNum);
			FormT.ShowDialog();
			if(FormT.DialogResult==DialogResult.Cancel) {
				return;
			}
			FillGrid();
			gridMain.SetSelected(selectedRow,true);
		}

		private void butDropFrom_Click(object sender,EventArgs e) {
			ToggleCalendars();
		}

		private void butDropTo_Click(object sender,EventArgs e) {
			ToggleCalendars();
		}

		private void ToggleCalendars(){
			if(calendarFrom.Visible){
				//hide the calendars
				calendarFrom.Visible=false;
				calendarTo.Visible=false;
			}
			else{
				//set the date on the calendars to match what's showing in the boxes
				if(textDateFrom.errorProvider1.GetError(textDateFrom)==""
					&& textDateTo.errorProvider1.GetError(textDateTo)=="")
				{//if no date errors
					if(textDateFrom.Text==""){
						calendarFrom.SetDate(DateTime.Today);
					}
					else{
						calendarFrom.SetDate(PIn.PDate(textDateFrom.Text));
					}
					if(textDateTo.Text=="") {
						calendarTo.SetDate(DateTime.Today);
					}
					else {
						calendarTo.SetDate(PIn.PDate(textDateTo.Text));
					}
				}
				//show the calendars
				calendarFrom.Visible=true;
				calendarTo.Visible=true;
			}
		}

		private void calendarFrom_DateSelected(object sender,DateRangeEventArgs e) {
			textDateFrom.Text=calendarFrom.SelectionStart.ToShortDateString();
		}

		private void calendarTo_DateSelected(object sender,DateRangeEventArgs e) {
			textDateTo.Text=calendarTo.SelectionStart.ToShortDateString();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			if(textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				|| textAmt.errorProvider1.GetError(textAmt)!=""
				)
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			calendarFrom.Visible=false;
			calendarTo.Visible=false;
			FillGrid();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		


	}
}





















