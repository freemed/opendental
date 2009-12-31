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
	public class FormLabCases : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private System.ComponentModel.Container components = null;
		private ODGrid gridMain;
		private Label label1;
		private ValidDate textDateFrom;
		private ValidDate textDateTo;
		private Label label2;
		private OpenDental.UI.Button butRefresh;// Required designer variable.
		private DataTable table;
		private CheckBox checkShowAll;
		private ContextMenu contextMenu1;
		private MenuItem menuItemGoTo;
		private CheckBox checkShowUnattached;
		//<summary>Set this to the selected date on the schedule, and date range will start out based on this date.</summary>
		//public DateTime DateViewing;
		///<summary>If this is zero, then it's an ordinary close.</summary>
		public long GoToAptNum;

		///<summary></summary>
		public FormLabCases()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLabCases));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.checkShowAll = new System.Windows.Forms.CheckBox();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItemGoTo = new System.Windows.Forms.MenuItem();
			this.butRefresh = new OpenDental.UI.Button();
			this.textDateTo = new OpenDental.ValidDate();
			this.textDateFrom = new OpenDental.ValidDate();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.checkShowUnattached = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,18);
			this.label1.TabIndex = 2;
			this.label1.Text = "From Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12,35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,18);
			this.label2.TabIndex = 4;
			this.label2.Text = "To Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkShowAll
			// 
			this.checkShowAll.Location = new System.Drawing.Point(361,37);
			this.checkShowAll.Name = "checkShowAll";
			this.checkShowAll.Size = new System.Drawing.Size(131,18);
			this.checkShowAll.TabIndex = 7;
			this.checkShowAll.Text = "Show Completed";
			this.checkShowAll.UseVisualStyleBackColor = true;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemGoTo});
			// 
			// menuItemGoTo
			// 
			this.menuItemGoTo.Index = 0;
			this.menuItemGoTo.Text = "Go To Appointment";
			this.menuItemGoTo.Click += new System.EventHandler(this.menuItemGoTo_Click);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(226,32);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(75,24);
			this.butRefresh.TabIndex = 6;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(116,35);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(100,20);
			this.textDateTo.TabIndex = 5;
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(116,9);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(100,20);
			this.textDateFrom.TabIndex = 3;
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(17,67);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(783,404);
			this.gridMain.TabIndex = 1;
			this.gridMain.Title = "Lab Cases";
			this.gridMain.TranslationName = "TableLabCases";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(725,481);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// checkShowUnattached
			// 
			this.checkShowUnattached.Location = new System.Drawing.Point(361,14);
			this.checkShowUnattached.Name = "checkShowUnattached";
			this.checkShowUnattached.Size = new System.Drawing.Size(131,18);
			this.checkShowUnattached.TabIndex = 8;
			this.checkShowUnattached.Text = "Show Unattached";
			this.checkShowUnattached.UseVisualStyleBackColor = true;
			// 
			// FormLabCases
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(812,517);
			this.Controls.Add(this.checkShowUnattached);
			this.Controls.Add(this.checkShowAll);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.textDateTo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textDateFrom);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLabCases";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Lab Cases";
			this.Load += new System.EventHandler(this.FormLabCases_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormLabCases_Load(object sender,EventArgs e) {
			gridMain.ContextMenu=contextMenu1;
			textDateFrom.Text="";//DateViewing.ToShortDateString();
			textDateTo.Text="";//DateViewing.AddDays(5).ToShortDateString();
			//checkShowAll.Checked=false
			FillGrid();
		}

		private void FillGrid(){
			if(textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateFrom.errorProvider1.GetError(textDateFrom)!="")
			{
				//MsgBox.Show(this,"Please fix errors first.");
				return;
			}
			DateTime dateMax=new DateTime(2100,1,1);
			if(textDateTo.Text!=""){
				dateMax=PIn.Date(textDateTo.Text);
			}
			table=LabCases.Refresh(PIn.Date(textDateFrom.Text),dateMax,checkShowAll.Checked,checkShowUnattached.Checked);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableLabCases","Appt Date Time"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableLabCases","Procedures"),200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableLabCases","Patient"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableLabCases","Status"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableLabCases","Lab"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableLabCases","Lab Phone"),100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["aptDateTime"].ToString());
				row.Cells.Add(table.Rows[i]["ProcDescript"].ToString());
				row.Cells.Add(table.Rows[i]["patient"].ToString());
				row.Cells.Add(table.Rows[i]["status"].ToString());
				row.Cells.Add(table.Rows[i]["lab"].ToString());
				row.Cells.Add(table.Rows[i]["phone"].ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			long selectedLabCase=PIn.Long(table.Rows[e.Row]["LabCaseNum"].ToString());
			FormLabCaseEdit FormL=new FormLabCaseEdit();
			FormL.CaseCur=LabCases.GetOne(selectedLabCase);
			FormL.ShowDialog();
			FillGrid();
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i]["LabCaseNum"].ToString()==selectedLabCase.ToString()){
					gridMain.SetSelected(i,true);
					break;
				}
			}
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			if(textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateFrom.errorProvider1.GetError(textDateFrom)!="")
			{
				MsgBox.Show(this,"Please fix errors first.");
				return;
			}
			FillGrid();
		}

		private void menuItemGoTo_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select a lab case first.");
				return;
			}
			Appointment apt=Appointments.GetOneApt(PIn.Long(table.Rows[gridMain.GetSelectedIndex()]["AptNum"].ToString()));
			if(apt.AptStatus==ApptStatus.UnschedList){
				MsgBox.Show(this,"Cannot go to an unscheduled appointment");
				return;
			}
			GoToAptNum=apt.AptNum;
			Close();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

	

		

		

		

		

		


	}
}





















