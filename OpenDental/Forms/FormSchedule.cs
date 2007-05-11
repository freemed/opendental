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
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormSchedule:System.Windows.Forms.Form {
		private OpenDental.UI.ODGrid gridMain;
		private OpenDental.UI.Button butRefresh;
		private ValidDate textDateTo;
		private Label label2;
		private ValidDate textDateFrom;
		private Label label1;
		private ListBox listProv;
		private Label labelProv;
		private CheckBox checkWeekend;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormSchedule()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSchedule));
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.listProv = new System.Windows.Forms.ListBox();
			this.labelProv = new System.Windows.Forms.Label();
			this.checkWeekend = new System.Windows.Forms.CheckBox();
			this.butRefresh = new OpenDental.UI.Button();
			this.textDateTo = new OpenDental.ValidDate();
			this.textDateFrom = new OpenDental.ValidDate();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(-1,63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,18);
			this.label2.TabIndex = 9;
			this.label2.Text = "To Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0,25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,18);
			this.label1.TabIndex = 7;
			this.label1.Text = "From Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(2,125);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(103,303);
			this.listProv.TabIndex = 23;
			// 
			// labelProv
			// 
			this.labelProv.Location = new System.Drawing.Point(-1,105);
			this.labelProv.Name = "labelProv";
			this.labelProv.Size = new System.Drawing.Size(100,18);
			this.labelProv.TabIndex = 22;
			this.labelProv.Text = "Providers";
			this.labelProv.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkWeekend
			// 
			this.checkWeekend.Location = new System.Drawing.Point(2,434);
			this.checkWeekend.Name = "checkWeekend";
			this.checkWeekend.Size = new System.Drawing.Size(104,18);
			this.checkWeekend.TabIndex = 24;
			this.checkWeekend.Text = "Weekends";
			this.checkWeekend.UseVisualStyleBackColor = true;
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(2,1);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(75,26);
			this.butRefresh.TabIndex = 11;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(2,83);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(100,20);
			this.textDateTo.TabIndex = 10;
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(2,44);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(100,20);
			this.textDateFrom.TabIndex = 8;
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(111,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.OneCell;
			this.gridMain.Size = new System.Drawing.Size(859,676);
			this.gridMain.TabIndex = 0;
			this.gridMain.Title = "Schedule";
			this.gridMain.TranslationName = null;
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// FormSchedule
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(982,700);
			this.Controls.Add(this.textDateFrom);
			this.Controls.Add(this.checkWeekend);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.labelProv);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.textDateTo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSchedule";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Schedule";
			this.Load += new System.EventHandler(this.FormSchedule_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormSchedule_Load(object sender,EventArgs e) {
			DateTime dateFrom=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1);
			textDateFrom.Text=dateFrom.ToShortDateString();
			textDateTo.Text=dateFrom.AddMonths(2).AddDays(-1).ToShortDateString();
			//DateTime dateFrom=new DateTime(2007,5,6);
			//textDateFrom.Text=dateFrom.ToShortDateString();
			//textDateTo.Text=dateFrom.AddDays(6).ToShortDateString();
			for(int i=0;i<Providers.List.Length;i++){
				listProv.Items.Add(Providers.List[i].Abbr);
				listProv.SetSelected(i,true);
			}
			FillGrid();
		}

		private void FillGrid(){
			if(textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!="")
			{
				//MsgBox.Show(this,"Please fix errors first.");
				return;
			}
			List<int> provNums=new List<int>();
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				provNums.Add(Providers.List[listProv.SelectedIndices[i]].ProvNum);
			}
			DataTable table=Schedules.GetPeriod(PIn.PDate(textDateFrom.Text),PIn.PDate(textDateTo.Text),provNums);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			int colW;
			if(checkWeekend.Checked){
				colW=(gridMain.Width-20)/7;
			}
			else{
				colW=(gridMain.Width-20)/5;
			}
			ODGridColumn col;
			if(checkWeekend.Checked){
				col=new ODGridColumn(Lan.g("TableSchedule","Sunday"),colW);
				gridMain.Columns.Add(col);
			}
			col=new ODGridColumn(Lan.g("TableSchedule","Monday"),colW);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSchedule","Tuesday"),colW);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSchedule","Wednesday"),colW);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSchedule","Thursday"),colW);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSchedule","Friday"),colW);
			gridMain.Columns.Add(col);
			if(checkWeekend.Checked){
				col=new ODGridColumn(Lan.g("TableSchedule","Saturday"),colW);
				gridMain.Columns.Add(col);
			}
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				if(checkWeekend.Checked){
					row.Cells.Add(table.Rows[i][0].ToString());
				}
				row.Cells.Add(table.Rows[i][1].ToString());
				row.Cells.Add(table.Rows[i][2].ToString());
				row.Cells.Add(table.Rows[i][3].ToString());
				row.Cells.Add(table.Rows[i][4].ToString());
				row.Cells.Add(table.Rows[i][5].ToString());
				if(checkWeekend.Checked){
					row.Cells.Add(table.Rows[i][6].ToString());
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			if(textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!="")
			{
				MsgBox.Show(this,"Please fix errors first.");
				return;
			}
			FillGrid();
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			//
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!="")
			{
				MsgBox.Show(this,"Please fix errors first.");
				return;
			}
			int clickedCol=e.Col;
			if(!checkWeekend.Checked){
				clickedCol++;
			}
			DateTime selectedDate=Schedules.GetDateCal(PIn.PDate(textDateFrom.Text),e.Row,clickedCol);
			if(selectedDate<PIn.PDate(textDateFrom.Text) || selectedDate>PIn.PDate(textDateTo.Text)){
				return;
			}
			//MessageBox.Show(selectedDate.ToShortDateString());
			FormScheduleDayEdit FormS=new FormScheduleDayEdit(selectedDate);
			FormS.ShowDialog();
		}




	}
}





















