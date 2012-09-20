using System;
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
	public class FormScreenGroups:System.Windows.Forms.Form {
		private System.Windows.Forms.TextBox textDateFrom;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butRefresh;
		private System.Windows.Forms.TextBox textDateTo;
		private OpenDental.UI.Button butAdd;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private UI.Button butClose;
		private UI.ODGrid gridMain;
		private List<ScreenGroup> ScreenGroupList;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem setupToolStripMenuItem;
		private MenuItem menuItemSetup;

		///<summary></summary>
		public FormScreenGroups()
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
			this.textDateFrom = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textDateTo = new System.Windows.Forms.TextBox();
			this.butRefresh = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(12, 27);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(69, 20);
			this.textDateFrom.TabIndex = 74;
			this.textDateFrom.Validating += new System.ComponentModel.CancelEventHandler(this.textDateFrom_Validating);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(75, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(25, 13);
			this.label2.TabIndex = 77;
			this.label2.Text = "To";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(102, 27);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(75, 20);
			this.textDateTo.TabIndex = 76;
			this.textDateTo.Validating += new System.ComponentModel.CancelEventHandler(this.textDateTo_Validating);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(187, 28);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(55, 21);
			this.butRefresh.TabIndex = 78;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(13, 336);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(70, 24);
			this.butAdd.TabIndex = 79;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClose.Location = new System.Drawing.Point(172, 336);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(70, 24);
			this.butClose.TabIndex = 79;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(13, 55);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(229, 267);
			this.gridMain.TabIndex = 80;
			this.gridMain.Title = "Screening Groups";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(255, 24);
			this.menuStrip1.TabIndex = 81;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// setupToolStripMenuItem
			// 
			this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
			this.setupToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
			this.setupToolStripMenuItem.Text = "Setup";
			this.setupToolStripMenuItem.Click += new System.EventHandler(this.setupToolStripMenuItem_Click);
			// 
			// FormScreenGroups
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(255, 375);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.textDateFrom);
			this.Controls.Add(this.textDateTo);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.menuStrip1);
			this.Name = "FormScreenGroups";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Screening Groups";
			this.Load += new System.EventHandler(this.FormScreenings_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormScreenings_Load(object sender, System.EventArgs e) {
			menuItemSetup=new MenuItem("Setup");
			Menu=menuItemSetup.GetMainMenu();
			textDateFrom.Text=DateTime.Today.AddMonths(-1).ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
			FillGrid();
		}

		private void FillGrid(){
			ScreenGroupList= ScreenGroups.Refresh(PIn.Date(textDateFrom.Text),PIn.Date(textDateTo.Text));
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g(this,"Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),140);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			ListViewItem[] items=new ListViewItem[ScreenGroupList.Count];
			for(int i=0;i<items.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(ScreenGroupList[i].SGDate.ToShortDateString());
				row.Cells.Add(ScreenGroupList[i].Description);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			FormScreenGroupEdit FormSG=new FormScreenGroupEdit();
			FormSG.ScreenGroupCur=ScreenGroupList[gridMain.SelectedIndices[0]];
			FormSG.ShowDialog();
			FillGrid();
		}

		private void textDateFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(textDateFrom.Text=="")
				return;
			try{
				DateTime.Parse(textDateFrom.Text);
			}
			catch{
				MessageBox.Show("Date invalid");
				e.Cancel=true;
			}
		}

		private void textDateTo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(textDateTo.Text=="")
				return;
			try{
				DateTime.Parse(textDateTo.Text);
			}
			catch{
				MessageBox.Show("Date invalid");
				e.Cancel=true;
			}
		}

		private void butRefresh_Click(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormScreenGroupEdit FormSG=new FormScreenGroupEdit();
			FormSG.IsNew=true;
			//ScreenGroups.Cur=new ScreenGroup();
			if(ScreenGroupList.Count==0){
				FormSG.ScreenGroupCur=new ScreenGroup();
			}
			else{
				FormSG.ScreenGroupCur=ScreenGroupList[ScreenGroupList.Count-1];//'remembers' the last entry
			}
			FormSG.ScreenGroupCur.SGDate=DateTime.Today;//except date will be today
			FormSG.ShowDialog();
			//if(FormSG.DialogResult!=DialogResult.OK){
			//	return;
			//}
			FillGrid();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(gridMain.SelectedIndices.Length!=1){
				MessageBox.Show("Please select one item first.");
				return;
			}
			ScreenGroup ScreenGroupCur=ScreenGroupList[gridMain.SelectedIndices[0]];
			OpenDentBusiness.Screen[] screenList=Screens.Refresh(ScreenGroupCur.ScreenGroupNum);
			if(screenList.Length>0) {
				MessageBox.Show("Not allowed to delete a screening group with items in it.");
				return;
			}
			ScreenGroups.Delete(ScreenGroupCur);
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void setupToolStripMenuItem_Click(object sender,EventArgs e) {
			MsgBox.Show(this,"Test");
		}

		

		

		

		

		


	}
}





















