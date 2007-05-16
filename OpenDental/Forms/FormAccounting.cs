using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormAccounting:System.Windows.Forms.Form {
		private OpenDental.UI.ODToolBar ToolBarMain;
		private OpenDental.UI.ODGrid gridMain;
		private IContainer components;
		private CheckBox checkInactive;
		private MainMenu mainMenu1;
		private MenuItem menuItemSetup;
		private MenuItem menuItem1;
		private MenuItem menuItemGL;
		private MenuItem menuItemBalSheet;
		private ImageList imageListMain;
		private OpenDental.UI.Button butRefresh;
		private ValidDate textDate;
		private Label label2;
		private OpenDental.UI.Button butToday;
		private MenuItem menuItemLock;
		//private Account[] AccountList;
		private DataTable table;

		///<summary></summary>
		public FormAccounting()
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAccounting));
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.checkInactive = new System.Windows.Forms.CheckBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemSetup = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemGL = new System.Windows.Forms.MenuItem();
			this.menuItemBalSheet = new System.Windows.Forms.MenuItem();
			this.label2 = new System.Windows.Forms.Label();
			this.butToday = new OpenDental.UI.Button();
			this.butRefresh = new OpenDental.UI.Button();
			this.textDate = new OpenDental.ValidDate();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.menuItemLock = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"Add.gif");
			this.imageListMain.Images.SetKeyName(1,"editPencil.gif");
			// 
			// checkInactive
			// 
			this.checkInactive.AutoSize = true;
			this.checkInactive.Location = new System.Drawing.Point(313,34);
			this.checkInactive.Name = "checkInactive";
			this.checkInactive.Size = new System.Drawing.Size(150,17);
			this.checkInactive.TabIndex = 2;
			this.checkInactive.Text = "Include Inactive Accounts";
			this.checkInactive.UseVisualStyleBackColor = true;
			this.checkInactive.Click += new System.EventHandler(this.checkInactive_Click);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSetup,
            this.menuItemLock,
            this.menuItem1});
			// 
			// menuItemSetup
			// 
			this.menuItemSetup.Index = 0;
			this.menuItemSetup.Text = "Setup";
			this.menuItemSetup.Click += new System.EventHandler(this.menuItemSetup_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemGL,
            this.menuItemBalSheet});
			this.menuItem1.Text = "Reports";
			// 
			// menuItemGL
			// 
			this.menuItemGL.Index = 0;
			this.menuItemGL.Text = "General Ledger Detail";
			this.menuItemGL.Click += new System.EventHandler(this.menuItemGL_Click);
			// 
			// menuItemBalSheet
			// 
			this.menuItemBalSheet.Index = 1;
			this.menuItemBalSheet.Text = "Balance Sheet";
			this.menuItemBalSheet.Click += new System.EventHandler(this.menuItemBalSheet_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3,33);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72,18);
			this.label2.TabIndex = 7;
			this.label2.Text = "As of Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// butToday
			// 
			this.butToday.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butToday.Autosize = true;
			this.butToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butToday.CornerRadius = 4F;
			this.butToday.Location = new System.Drawing.Point(238,32);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(70,23);
			this.butToday.TabIndex = 10;
			this.butToday.Text = "Today";
			this.butToday.UseVisualStyleBackColor = true;
			this.butToday.Click += new System.EventHandler(this.butToday_Click);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(163,32);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(70,23);
			this.butRefresh.TabIndex = 9;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.UseVisualStyleBackColor = true;
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(76,34);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(81,20);
			this.textDate.TabIndex = 8;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(0,57);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(492,597);
			this.gridMain.TabIndex = 1;
			this.gridMain.Title = "Chart of Accounts";
			this.gridMain.TranslationName = "TableChartOfAccounts";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(492,29);
			this.ToolBarMain.TabIndex = 0;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// menuItemLock
			// 
			this.menuItemLock.Index = 1;
			this.menuItemLock.Text = "Lock";
			this.menuItemLock.Click += new System.EventHandler(this.menuItemLock_Click);
			// 
			// FormAccounting
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(492,654);
			this.Controls.Add(this.butToday);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkInactive);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.ToolBarMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Name = "FormAccounting";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Accounting";
			this.Load += new System.EventHandler(this.FormAccounting_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormAccounting_Load(object sender,EventArgs e) {
			LayoutToolBar();
			textDate.Text=DateTime.Today.ToShortDateString();
			FillGrid();
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add"),0,"","Add"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Edit"),1,Lan.g(this,"Edit Selected Account"),"Edit"));
			/*ODToolBarButton button=new ODToolBarButton("",-1,"","PageNum");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton("",2,"Go Forward One Page","Fwd"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));*/
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Close"),-1,"Close This Window","Close"));
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			FormAccountingSetup FormA=new FormAccountingSetup();
			FormA.ShowDialog();
		}

		private void menuItemLock_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.SecurityAdmin)) {
				return;
			}
			FormAccountingLock FormA=new FormAccountingLock();
			FormA.ShowDialog();
			if(FormA.DialogResult==DialogResult.OK){
				SecurityLogs.MakeLogEntry(Permissions.SecurityAdmin,0,"Accounting Lock Changed");
			}
		}

		private void menuItemGL_Click(object sender,EventArgs e) {
			FormReport FormR=new FormReport();
			FormR.SourceRdlString=Properties.Resources.ReportAccountingGenLedger;
			FormR.ShowDialog();
		}

		private void menuItemBalSheet_Click(object sender,EventArgs e) {
			FormReport FormR=new FormReport();
			FormR.SourceRdlString=Properties.Resources.ReportAccountingBalanceSheet;
			FormR.ShowDialog();
		}
		
		private void ToolBarMain_ButtonClick(object sender,OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			switch(e.Button.Tag.ToString()) {
				case "Add":
					Add_Click();
					break;
				case "Edit":
					Edit_Click();
					break;
				case "Close":
					Close();
					break;
			}
			/*	case "Fwd":
					OnFwd_Click();
					break;
				
			}*/
		}

		private void FillGrid(){
			Accounts.Refresh();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableChartOfAccounts","Type"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableChartOfAccounts","Description"),170);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableChartOfAccounts","Balance"),80,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableChartOfAccounts","Bank Number"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableChartOfAccounts","Inactive"),70,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			if(textDate.errorProvider1.GetError(textDate) != ""){//error
				table=Accounts.GetFullList(DateTime.Today,checkInactive.Checked);
			}
			else{
				table=Accounts.GetFullList(PIn.PDate(textDate.Text),checkInactive.Checked);
			}
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["type"].ToString());
				row.Cells.Add(table.Rows[i]["Description"].ToString());
				row.Cells.Add(table.Rows[i]["balance"].ToString());
				row.Cells.Add(table.Rows[i]["BankNumber"].ToString());
				row.Cells.Add(table.Rows[i]["inactive"].ToString());				
				if(i<table.Rows.Count-1//if not the last row
					&& table.Rows[i]["type"].ToString() != table.Rows[i+1]["type"].ToString())
				{
					row.ColorLborder=Color.Black;
				}
				row.ColorBackG=Color.FromArgb(PIn.PInt(table.Rows[i]["color"].ToString()));
				gridMain.Rows.Add(row);
			}
			/*for(int i=0;i<Accounts.ListLong.Length;i++){
				if(!checkInactive.Checked && Accounts.ListLong[i].Inactive){
					continue;
				}
				row=new ODGridRow();
				row.Cells.Add(Lan.g("enumAccountType",Accounts.ListLong[i].AcctType.ToString()));
				row.Cells.Add(Accounts.ListLong[i].Description);
				if(Accounts.ListLong[i].AcctType==AccountType.Asset){
					row.Cells.Add(Accounts.GetBalance(Accounts.ListLong[i].AccountNum,Accounts.ListLong[i].AcctType).ToString("n"));
				}
				else{
					row.Cells.Add("");
				}
				row.Cells.Add(Accounts.ListLong[i].BankNumber);
				if(Accounts.ListLong[i].Inactive){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
				if(i<Accounts.ListLong.Length-1//if not the last row
					&& Accounts.ListLong[i].AcctType != Accounts.ListLong[i+1].AcctType){
					row.ColorLborder=Color.Black;
				}
				row.Tag=Accounts.ListLong[i].Copy();
				row.ColorBackG=Accounts.ListLong[i].AccountColor;
				gridMain.Rows.Add(row);
			}*/
			gridMain.EndUpdate();
		}

		private void Add_Click() {
			Account acct=new Account();
			acct.AcctType=AccountType.Asset;
			acct.AccountColor=Color.White;
			FormAccountEdit FormA=new FormAccountEdit(acct);
			FormA.IsNew=true;
			FormA.ShowDialog();
			FillGrid();
		}

		private void Edit_Click() {
			if(gridMain.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please pick an account first.");
				return;
			}
			int acctNum=PIn.PInt(table.Rows[gridMain.GetSelectedIndex()]["AccountNum"].ToString());
			if(acctNum==0) {
				MsgBox.Show(this,"This account is generated automatically, and cannot be edited.");
				return;
			}
			Account acct=Accounts.GetAccount(acctNum);
			FormAccountEdit FormA=new FormAccountEdit(acct);
			FormA.ShowDialog();
			FillGrid();
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i]["AccountNum"].ToString()==acctNum.ToString()){
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			int acctNum=PIn.PInt(table.Rows[gridMain.GetSelectedIndex()]["AccountNum"].ToString());
			if(acctNum==0) {
				MsgBox.Show(this,"This account is generated automatically, and there is currently no way to view the detail.  It is the sum of all income minus all expenses for all previous years.");
				return;
			}
			DateTime asofDate;
			if(textDate.errorProvider1.GetError(textDate) != ""){//error
				asofDate=DateTime.Today;
			}
			else{
				asofDate=PIn.PDate(textDate.Text);
			}
			Account acct=Accounts.GetAccount(acctNum);
			FormJournal FormJ=new FormJournal(acct);
			FormJ.InitialAsOfDate=asofDate;
			FormJ.ShowDialog();
			FillGrid();
			for(int i=0;i<table.Rows.Count;i++) {
				if(table.Rows[i]["AccountNum"].ToString()==acctNum.ToString()) {
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void checkInactive_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butToday_Click(object sender,EventArgs e) {
			textDate.Text=DateTime.Today.ToShortDateString();
			FillGrid();
		}

		

		

	

		


		

	

		

		

	


	}
}





















