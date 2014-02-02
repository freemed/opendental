using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// </summary>
	public class FormDiseaseDefs:System.Windows.Forms.Form {
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butAdd;
		private System.ComponentModel.IContainer components;
		private Label label1;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butUp;
		private System.Windows.Forms.ToolTip toolTip1;
		private OpenDental.UI.Button butOK;
		///<summary>Set to true when user is using this to select a disease def. Currently used when adding Alerts to Rx.</summary>
		public bool IsSelectionMode;
		///<summary>Set to true when user is using FormMedical to allow multiple problems to be selected at once.</summary>
		public bool IsMultiSelect;
		///<summary>If IsSelectionMode, then after closing with OK, this will contain number.</summary>
		public long SelectedDiseaseDefNum;
		///<summary>If IsMultiSelect, then this will contain a list of numbers when closing with OK.</summary>
		public List<long> SelectedDiseaseDefNums;
		private ODGrid gridMain;
		private bool IsChanged;

		///<summary></summary>
		public FormDiseaseDefs()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiseaseDefs));
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(15, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(335, 20);
			this.label1.TabIndex = 8;
			this.label1.Text = "This is a list of medical problems that patients might have. ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(18, 35);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(548, 628);
			this.gridMain.TabIndex = 16;
			this.gridMain.Title = null;
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(584, 605);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(79, 26);
			this.butOK.TabIndex = 15;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(584, 464);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(79, 26);
			this.butDown.TabIndex = 14;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0, 1);
			this.butUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(584, 432);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(79, 26);
			this.butUp.TabIndex = 13;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(584, 637);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(79, 26);
			this.butClose.TabIndex = 1;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(584, 265);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79, 26);
			this.butAdd.TabIndex = 7;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormDiseaseDefs
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(682, 675);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.butAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDiseaseDefs";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Problems";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDiseaseDefs_FormClosing);
			this.Load += new System.EventHandler(this.FormDiseaseDefs_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDiseaseDefs_Load(object sender, System.EventArgs e) {
			if(IsSelectionMode){
				butClose.Text=Lan.g(this,"Cancel");
				if(IsMultiSelect) {
					gridMain.SelectionMode=GridSelectionMode.MultiExtended;
				}
			}
			else{
				butOK.Visible=false;
			}
			FillGrid();
		}

		private void FillGrid(){
			DiseaseDefs.RefreshCache();
			//listMain.SelectionMode=SelectionMode.MultiExtended;
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g(this,"ICD-9"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"ICD-10"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"SNOMED CT"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),250);
			gridMain.Columns.Add(col);
			if(!IsSelectionMode) {
				col=new ODGridColumn(Lan.g(this,"Hidden"),50);
				gridMain.Columns.Add(col);
			}
			gridMain.Rows.Clear();
			ODGridRow row;
			if(IsSelectionMode) {
				for(int i=0;i<DiseaseDefs.List.Length;i++) {
					row=new ODGridRow();
					row.Cells.Add(DiseaseDefs.List[i].ICD9Code);
					row.Cells.Add(DiseaseDefs.List[i].Icd10Code);
					row.Cells.Add(DiseaseDefs.List[i].SnomedCode);
					row.Cells.Add(DiseaseDefs.List[i].DiseaseName);
					gridMain.Rows.Add(row);
				}
			}
			else {//Not selection mode - show hidden
				for(int i=0;i<DiseaseDefs.ListLong.Length;i++) {
					row=new ODGridRow();
					row.Cells.Add(DiseaseDefs.ListLong[i].ICD9Code);
					row.Cells.Add(DiseaseDefs.List[i].Icd10Code);
					row.Cells.Add(DiseaseDefs.ListLong[i].SnomedCode);
					row.Cells.Add(DiseaseDefs.ListLong[i].DiseaseName);
					row.Cells.Add(DiseaseDefs.ListLong[i].IsHidden?"X":"");
					gridMain.Rows.Add(row);
				}
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(!IsSelectionMode && !Security.IsAuthorized(Permissions.ProblemEdit)) {//trying to double click to edit, but no permission.
				return;
			}
			if(gridMain.SelectedIndices.Length==0) {
				return;
			}
			if(IsSelectionMode) {
				if(IsMultiSelect) {
					SelectedDiseaseDefNums=new List<long>();
					SelectedDiseaseDefNums.Add(DiseaseDefs.List[gridMain.GetSelectedIndex()].DiseaseDefNum);
				}
				else {
					SelectedDiseaseDefNum=DiseaseDefs.List[gridMain.GetSelectedIndex()].DiseaseDefNum;
				}
				DialogResult=DialogResult.OK;
				return;
			}
			//everything below this point is _not_ selection mode.  User guaranteed to have permission for ProblemEdit.
			FormDiseaseDefEdit FormD=new FormDiseaseDefEdit(DiseaseDefs.ListLong[gridMain.GetSelectedIndex()]);
			FormD.ShowDialog();
			//Security log entry made inside that form.
			if(FormD.DialogResult!=DialogResult.OK) {
				return;
			}
			IsChanged=true;
			FillGrid();
		}

		private void butAdd_Click(object sender,System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.ProblemEdit)) {
				return;
			}
			DiseaseDef def=new DiseaseDef();
			def.ItemOrder=DiseaseDefs.ListLong.Length;
			FormDiseaseDefEdit FormD=new FormDiseaseDefEdit(def);
			FormD.IsNew=true;
			FormD.ShowDialog();
			//Security log entry made inside that form.
			if(FormD.DialogResult!=DialogResult.OK) {
				return;
			}
			IsChanged=true;
			FillGrid();
		}

		private void butUp_Click(object sender,EventArgs e) {
			//These aren't yet optimized for multiselection.
			int selected=gridMain.GetSelectedIndex();
			try{
				DiseaseDefs.MoveUp(gridMain.GetSelectedIndex());
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			FillGrid();
			if(selected==0) {
				gridMain.SetSelected(0,true);
			}
			else{
				gridMain.SetSelected(selected-1,true);
			}
			IsChanged=true;
		}

		private void butDown_Click(object sender,EventArgs e) {
			//These aren't yet optimized for multiselection.
			int selected=gridMain.GetSelectedIndex();
			try {
				DiseaseDefs.MoveDown(gridMain.GetSelectedIndex());
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			FillGrid();
			if(selected==DiseaseDefs.ListLong.Length-1) {
				gridMain.GetSelectedIndex();
			}
			else{
				gridMain.SetSelected(selected+1,true);
			}
			IsChanged=true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//not even visible unless IsSelectionMode
			if(gridMain.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			if(IsMultiSelect) {
				SelectedDiseaseDefNums=new List<long>();
				for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
					SelectedDiseaseDefNums.Add(DiseaseDefs.List[gridMain.SelectedIndices[i]].DiseaseDefNum);
				}
			}
			else if(IsSelectionMode) {
				SelectedDiseaseDefNum=DiseaseDefs.List[gridMain.GetSelectedIndex()].DiseaseDefNum;
			}
			else {
				SelectedDiseaseDefNum=DiseaseDefs.ListLong[gridMain.GetSelectedIndex()].DiseaseDefNum;
			}
			DialogResult=DialogResult.OK;
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;//also closes if not IsSelectionMode
		}

		private void FormDiseaseDefs_FormClosing(object sender,FormClosingEventArgs e) {
			if(IsChanged) {
				DataValid.SetInvalid(InvalidType.Diseases);
			}
		}

		

		

		

		

		

		

		

		


	}
}



























