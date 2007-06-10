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
///<summary></summary>
	public class FormReqNeededs:System.Windows.Forms.Form {
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butAdd;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.ODGrid gridMain;
		private ComboBox comboClass;
		private Label label1;
		private Label label2;
		private ComboBox comboCourse;
		private OpenDental.UI.Button butSynch;
		private Label label3;
		private DataTable table;
		
		///<summary></summary>
		public FormReqNeededs(){
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}    

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReqNeededs));
			this.label1 = new System.Windows.Forms.Label();
			this.comboClass = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboCourse = new System.Windows.Forms.ComboBox();
			this.butAdd = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.butSynch = new OpenDental.UI.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(22,7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81,18);
			this.label1.TabIndex = 16;
			this.label1.Text = "Class";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClass
			// 
			this.comboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClass.FormattingEnabled = true;
			this.comboClass.Location = new System.Drawing.Point(106,7);
			this.comboClass.Name = "comboClass";
			this.comboClass.Size = new System.Drawing.Size(234,21);
			this.comboClass.TabIndex = 0;
			this.comboClass.SelectionChangeCommitted += new System.EventHandler(this.comboClass_SelectionChangeCommitted);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(2,34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,18);
			this.label2.TabIndex = 18;
			this.label2.Text = "Course";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboCourse
			// 
			this.comboCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCourse.FormattingEnabled = true;
			this.comboCourse.Location = new System.Drawing.Point(106,34);
			this.comboCourse.Name = "comboCourse";
			this.comboCourse.Size = new System.Drawing.Size(234,21);
			this.comboCourse.TabIndex = 17;
			this.comboCourse.SelectionChangeCommitted += new System.EventHandler(this.comboCourse_SelectionChangeCommitted);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(511,392);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(82,26);
			this.butAdd.TabIndex = 10;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(16,61);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(433,593);
			this.gridMain.TabIndex = 13;
			this.gridMain.Title = "Requirements Needed";
			this.gridMain.TranslationName = "TableRequirementsNeeded";
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
			this.butClose.Location = new System.Drawing.Point(511,628);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(82,26);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butSynch
			// 
			this.butSynch.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSynch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSynch.Autosize = true;
			this.butSynch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSynch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSynch.CornerRadius = 4F;
			this.butSynch.Location = new System.Drawing.Point(511,449);
			this.butSynch.Name = "butSynch";
			this.butSynch.Size = new System.Drawing.Size(82,26);
			this.butSynch.TabIndex = 19;
			this.butSynch.Text = "Synch";
			this.butSynch.Click += new System.EventHandler(this.butSynch_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(455,490);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(157,84);
			this.label3.TabIndex = 20;
			this.label3.Text = "Synch after editing the requirements needed list.  This makes the requirements fo" +
    "r each student match this list.";
			// 
			// FormReqNeededs
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(614,670);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butSynch);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboCourse);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.comboClass);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormReqNeededs";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Requirements Needed";
			this.Load += new System.EventHandler(this.FormRequirementsNeeded_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRequirementsNeeded_Load(object sender, System.EventArgs e) {
			//comboClass.Items.Add(Lan.g(this,"All"));
			//comboClass.SelectedIndex=0;
			for(int i=0;i<SchoolClasses.List.Length;i++){
				comboClass.Items.Add(SchoolClasses.GetDescript(SchoolClasses.List[i]));
			}
			if(comboClass.Items.Count>0){
				comboClass.SelectedIndex=0;
			}
			for(int i=0;i<SchoolCourses.List.Length;i++) {
				comboCourse.Items.Add(SchoolCourses.GetDescript(SchoolCourses.List[i]));
			}
			if(comboCourse.Items.Count>0) {
				comboCourse.SelectedIndex=0;
			}
			FillGrid();
		}

		private void FillGrid(){
			if(comboClass.SelectedIndex==-1 || comboCourse.SelectedIndex==-1){
				return;
			}
			int selected=0;
			if(gridMain.GetSelectedIndex()!=-1){
				selected=PIn.PInt(table.Rows[gridMain.GetSelectedIndex()]["ReqNeededNum"].ToString());
			}
			int scroll=gridMain.ScrollValue;
			int schoolClass=SchoolClasses.List[comboClass.SelectedIndex].SchoolClassNum;
			int schoolCourse=SchoolCourses.List[comboCourse.SelectedIndex].SchoolCourseNum;
			table=ReqNeededs.Refresh(schoolClass,schoolCourse);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			//col=new ODGridColumn(Lan.g("TableRequirementsNeeded","Class"),100);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g("TableRequirementsNeeded","Course"),100);
			//gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRequirementsNeeded","Description"),200);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				//row.Cells.Add(SchoolClasses.GetDescript(PIn.PInt(table.Rows[i]["SchoolClassNum"].ToString())));
				//row.Cells.Add(SchoolCourses.GetCourseID(PIn.PInt(table.Rows[i]["SchoolCourseNum"].ToString())));
				row.Cells.Add(table.Rows[i]["Descript"].ToString());
				//row.Tag
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i]["ReqNeededNum"].ToString()==selected.ToString()){
					gridMain.SetSelected(i,true);
					break;
				}
			}
			gridMain.ScrollValue=scroll;
		}

		private void comboClass_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		private void comboCourse_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			if(comboClass.SelectedIndex==-1 || comboCourse.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a Class and Course first.");
				return;
			}
			FormReqNeededEdit FormR=new FormReqNeededEdit();
			FormR.ReqCur=new ReqNeeded();
			FormR.ReqCur.SchoolClassNum=SchoolClasses.List[comboClass.SelectedIndex].SchoolClassNum;
			FormR.ReqCur.SchoolCourseNum=SchoolCourses.List[comboCourse.SelectedIndex].SchoolCourseNum;
			FormR.IsNew=true;
			FormR.ShowDialog();
			if(FormR.DialogResult!=DialogResult.OK){
				return;
			}
			FillGrid();
			gridMain.ScrollToEnd();
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i]["ReqNeededNum"].ToString()==FormR.ReqCur.ReqNeededNum.ToString()){
					gridMain.SetSelected(i,true);
					break;
				}
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormReqNeededEdit FormR=new FormReqNeededEdit();
			FormR.ReqCur=ReqNeededs.GetReq(PIn.PInt(table.Rows[e.Row]["ReqNeededNum"].ToString()));
			FormR.ShowDialog();
			if(FormR.DialogResult!=DialogResult.OK) {
				return;
			}
			FillGrid();
		}

		private void butSynch_Click(object sender,EventArgs e) {
			if(comboClass.SelectedIndex==-1 || comboCourse.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a Class and Course first.");
				return;
			}
			ReqNeededs.Synch(SchoolClasses.List[comboClass.SelectedIndex].SchoolClassNum,
				SchoolCourses.List[comboCourse.SelectedIndex].SchoolCourseNum);
			MsgBox.Show(this,"Done.");
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		



		

		

		

	

	}
}
