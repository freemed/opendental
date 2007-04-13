using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormScreenings : System.Windows.Forms.Form{
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView listMain;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.TextBox textDateFrom;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butRefresh;
		private System.Windows.Forms.TextBox textDateTo;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butDelete;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormScreenings()
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
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("00/00/0000");
			this.listMain = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.textDateFrom = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textDateTo = new System.Windows.Forms.TextBox();
			this.butRefresh = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// listMain
			// 
			this.listMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4});
			this.listMain.FullRowSelect = true;
			this.listMain.GridLines = true;
			this.listMain.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listMain.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
			this.listMain.Location = new System.Drawing.Point(-1,7);
			this.listMain.Name = "listMain";
			this.listMain.Size = new System.Drawing.Size(231,237);
			this.listMain.TabIndex = 73;
			this.listMain.UseCompatibleStateImageBehavior = false;
			this.listMain.View = System.Windows.Forms.View.Details;
			this.listMain.DoubleClick += new System.EventHandler(this.listMain_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Date";
			this.columnHeader1.Width = 68;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Description";
			this.columnHeader4.Width = 136;
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(0,251);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(69,20);
			this.textDateFrom.TabIndex = 74;
			this.textDateFrom.Validating += new System.ComponentModel.CancelEventHandler(this.textDateFrom_Validating);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(63,255);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(25,13);
			this.label2.TabIndex = 77;
			this.label2.Text = "To";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(90,251);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(75,20);
			this.textDateTo.TabIndex = 76;
			this.textDateTo.Validating += new System.ComponentModel.CancelEventHandler(this.textDateTo_Validating);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.Location = new System.Drawing.Point(175,252);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(55,21);
			this.butRefresh.TabIndex = 78;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.Location = new System.Drawing.Point(5,274);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(55,21);
			this.butAdd.TabIndex = 79;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Location = new System.Drawing.Point(87,275);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(55,21);
			this.butDelete.TabIndex = 80;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormScreenings
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(234,296);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.textDateFrom);
			this.Controls.Add(this.textDateTo);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormScreenings";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Screening Groups";
			this.Load += new System.EventHandler(this.FormScreenings_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormScreenings_Load(object sender, System.EventArgs e) {
			Location=new Point(200,200);
			textDateFrom.Text=DateTime.Today.AddMonths(-1).ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
			Counties.GetListNames();
			Schools.GetListNames();
			FillGrid();
		}

		private void FillGrid(){
			ScreenGroups.Refresh(PIn.PDate(textDateFrom.Text),PIn.PDate(textDateTo.Text));
			ListViewItem[] items=new ListViewItem[ScreenGroups.List.Length];
			for(int i=0;i<items.Length;i++){
				items[i]=new ListViewItem();
				items[i].Text=ScreenGroups.List[i].SGDate.ToShortDateString();
				items[i].SubItems.Add(ScreenGroups.List[i].Description);
			}
			listMain.Items.Clear();
			listMain.Items.AddRange(items);
		}

		private void listMain_DoubleClick(object sender, System.EventArgs e) {
			FormScreenGroup FormSG=new FormScreenGroup();
			FormSG.ScreenGroupCur=ScreenGroups.List[listMain.SelectedIndices[0]];
			FormSG.ShowDialog();
			//if(FormSG.DialogResult!=DialogResult.OK){
			//	return;
			//}
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
			FormScreenGroup FormSG=new FormScreenGroup();
			FormSG.IsNew=true;
			//ScreenGroups.Cur=new ScreenGroup();
			if(ScreenGroups.List.Length==0){
				FormSG.ScreenGroupCur=new ScreenGroup();
			}
			else{
				FormSG.ScreenGroupCur=ScreenGroups.List[ScreenGroups.List.Length-1];//'remembers' the last entry
			}
			FormSG.ScreenGroupCur.SGDate=DateTime.Today;//except date will be today
			FormSG.ShowDialog();
			//if(FormSG.DialogResult!=DialogResult.OK){
			//	return;
			//}
			FillGrid();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listMain.SelectedIndices.Count!=1){
				MessageBox.Show("Please select one item first.");
				return;
			}
			ScreenGroup ScreenGroupCur=ScreenGroups.List[listMain.SelectedIndices[0]];
			Screens.Refresh(ScreenGroupCur.ScreenGroupNum);
			if(Screens.List.Length>0){
				MessageBox.Show("Not allowed to delete a screening group with items in it.");
				return;
			}
			ScreenGroups.Delete(ScreenGroupCur);
			FillGrid();
		}

		

		

		

		

		


	}
}





















