using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	///<summary></summary>
	public class FormInsCatsSetup:System.Windows.Forms.Form {
		private System.ComponentModel.Container components=null;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butAddSpan;
		private OpenDental.UI.Button butUp;
		private OpenDental.UI.Button butAddCat;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.ODGrid gridMain;
		private GroupBox groupBox1;
		private TextBox textBox1;
		private OpenDental.UI.Button butDefaultsReset;
		private Label label1;
		private bool changed;

		///<summary></summary>
		public FormInsCatsSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components!=null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInsCatsSetup));
			this.butOK = new OpenDental.UI.Button();
			this.butAddSpan = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butAddCat = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.butDefaultsReset = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butOK.Location = new System.Drawing.Point(613,619);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(85,24);
			this.butOK.TabIndex = 6;
			this.butOK.Text = "&Close";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butAddSpan
			// 
			this.butAddSpan.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddSpan.Autosize = true;
			this.butAddSpan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddSpan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddSpan.CornerRadius = 4F;
			this.butAddSpan.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddSpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddSpan.Location = new System.Drawing.Point(512,286);
			this.butAddSpan.Name = "butAddSpan";
			this.butAddSpan.Size = new System.Drawing.Size(86,24);
			this.butAddSpan.TabIndex = 9;
			this.butAddSpan.Text = "Add Span";
			this.butAddSpan.Click += new System.EventHandler(this.butAddSpan_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(8,19);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(86,24);
			this.butUp.TabIndex = 12;
			this.butUp.Text = "Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butAddCat
			// 
			this.butAddCat.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddCat.Autosize = true;
			this.butAddCat.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddCat.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddCat.CornerRadius = 4F;
			this.butAddCat.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddCat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddCat.Location = new System.Drawing.Point(8,61);
			this.butAddCat.Name = "butAddCat";
			this.butAddCat.Size = new System.Drawing.Size(86,24);
			this.butAddCat.TabIndex = 11;
			this.butAddCat.Text = "A&dd";
			this.butAddCat.Click += new System.EventHandler(this.butAddCat_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(100,19);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(86,24);
			this.butDown.TabIndex = 13;
			this.butDown.Text = "Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,68);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(476,578);
			this.gridMain.TabIndex = 14;
			this.gridMain.Title = "Coverage Spans";
			this.gridMain.TranslationName = "TableCovSpans";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butUp);
			this.groupBox1.Controls.Add(this.butDown);
			this.groupBox1.Controls.Add(this.butAddCat);
			this.groupBox1.Location = new System.Drawing.Point(504,68);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(194,94);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Categories";
			// 
			// textBox1
			// 
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(12,12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(543,53);
			this.textBox1.TabIndex = 16;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			// 
			// butDefaultsReset
			// 
			this.butDefaultsReset.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDefaultsReset.Autosize = true;
			this.butDefaultsReset.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDefaultsReset.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDefaultsReset.CornerRadius = 4F;
			this.butDefaultsReset.Location = new System.Drawing.Point(512,451);
			this.butDefaultsReset.Name = "butDefaultsReset";
			this.butDefaultsReset.Size = new System.Drawing.Size(86,24);
			this.butDefaultsReset.TabIndex = 18;
			this.butDefaultsReset.Text = "Set to Defaults";
			this.butDefaultsReset.Click += new System.EventHandler(this.butDefaultsReset_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(511,479);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(148,51);
			this.label1.TabIndex = 20;
			this.label1.Text = "This safely fixes the orders and spans.";
			// 
			// FormInsCatsSetup
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butOK;
			this.ClientSize = new System.Drawing.Size(713,660);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDefaultsReset);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butAddSpan);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsCatsSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Insurance Categories";
			this.Load += new System.EventHandler(this.FormInsCatsSetup_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormInsCatsSetup_Closing);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormInsCatsSetup_Load(object sender,System.EventArgs e) {
			FillSpans();
		}

		private void FillSpans() {
			CovCats.RefreshCache();
			CovSpans.RefreshCache();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Category",90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("From ADA",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("To ADA",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Hidden",45);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("E-Benefit Category",100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			CovSpan[] spansForCat;
			for(int i=0;i<CovCatC.Listt.Count;i++){
				row=new ODGridRow();
				row.Tag=CovCatC.Listt[i].Copy();
				row.ColorBackG=Color.FromArgb(225,225,225);
				if(i!=0) {
					gridMain.Rows[gridMain.Rows.Count-1].ColorLborder=Color.Black;
				}
				row.Cells.Add(CovCatC.Listt[i].Description);
				row.Cells.Add("");
				row.Cells.Add("");
				if(CovCatC.Listt[i].IsHidden){
					row.Cells.Add("X");
				}
				else {
					row.Cells.Add("");
				}
				if(CovCatC.Listt[i].EbenefitCat==EbenefitCategory.None){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(CovCatC.Listt[i].EbenefitCat.ToString());
				}
				gridMain.Rows.Add(row);
				spansForCat=CovSpans.GetForCat(CovCatC.Listt[i].CovCatNum);
				for(int j=0;j<spansForCat.Length;j++){
					row=new ODGridRow();
					row.Tag=spansForCat[j].Copy();
					row.Cells.Add("");
					row.Cells.Add(spansForCat[j].FromCode);
					row.Cells.Add(spansForCat[j].ToCode);
					row.Cells.Add("");
					row.Cells.Add("");
					gridMain.Rows.Add(row);
				}
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			bool isCat=false;
			int selectedKey=0;
			if(gridMain.Rows[e.Row].Tag.GetType()==typeof(CovCat)){
				isCat=true;
				selectedKey=((CovCat)gridMain.Rows[e.Row].Tag).CovCatNum;
				FormInsCatEdit FormE=new FormInsCatEdit((CovCat)gridMain.Rows[e.Row].Tag);
				FormE.ShowDialog();
				if(FormE.DialogResult!=DialogResult.OK) {
					return;
				}	
			}
			else{//covSpan
				selectedKey=((CovSpan)gridMain.Rows[e.Row].Tag).CovSpanNum;
				FormInsSpanEdit FormE=new FormInsSpanEdit((CovSpan)gridMain.Rows[e.Row].Tag);
				FormE.ShowDialog();
				if(FormE.DialogResult!=DialogResult.OK){
					return;
				}
			}
			changed=true;
			FillSpans();
			for(int i=0;i<gridMain.Rows.Count;i++){
				if(isCat && gridMain.Rows[i].Tag.GetType()==typeof(CovCat) 
					&& selectedKey==((CovCat)gridMain.Rows[i].Tag).CovCatNum)
				{
					gridMain.SetSelected(i,true);
				}
				if(!isCat && gridMain.Rows[i].Tag.GetType()==typeof(CovSpan) 
					&& selectedKey==((CovSpan)gridMain.Rows[i].Tag).CovSpanNum)
				{
					gridMain.SetSelected(i,true);
				}
			}
		}		

		private void butAddSpan_Click(object sender, System.EventArgs e){
			if(gridMain.SelectedIndices.Length<1){
				MsgBox.Show(this,"Please select a category first.");
				return;
			}
			if(gridMain.Rows[gridMain.SelectedIndices[0]].Tag.GetType()!=typeof(CovCat)){
				MsgBox.Show(this,"Please select a category first.");
				return;
			}
			CovSpan covspan=new CovSpan();
			covspan.CovCatNum=((CovCat)gridMain.Rows[gridMain.SelectedIndices[0]].Tag).CovCatNum;
			FormInsSpanEdit FormE=new FormInsSpanEdit(covspan);
			FormE.IsNew=true;
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			FillSpans();
		}

		private void butUp_Click(object sender, System.EventArgs e) {
			if(gridMain.SelectedIndices.Length<1){
				MsgBox.Show(this,"Please select a category first.");
				return;
			}
			if(gridMain.Rows[gridMain.SelectedIndices[0]].Tag.GetType()!=typeof(CovCat)){
				MsgBox.Show(this,"Please select a category first.");
				return;
			}
			int catNum=((CovCat)gridMain.Rows[gridMain.SelectedIndices[0]].Tag).CovCatNum;
			CovCats.MoveUp((CovCat)gridMain.Rows[gridMain.SelectedIndices[0]].Tag);
			changed=true;
			FillSpans();
			for(int i=0;i<gridMain.Rows.Count;i++) {
				if(gridMain.Rows[i].Tag.GetType()==typeof(CovCat) && catNum==((CovCat)gridMain.Rows[i].Tag).CovCatNum){
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void butDown_Click(object sender, System.EventArgs e){
			if(gridMain.SelectedIndices.Length<1){
				MsgBox.Show(this,"Please select a category first.");
				return;
			}
			if(gridMain.Rows[gridMain.SelectedIndices[0]].Tag.GetType()!=typeof(CovCat)){
				MsgBox.Show(this,"Please select a category first.");
				return;
			}
			int catNum=((CovCat)gridMain.Rows[gridMain.SelectedIndices[0]].Tag).CovCatNum;
			CovCats.MoveDown((CovCat)gridMain.Rows[gridMain.SelectedIndices[0]].Tag);
			changed=true;
			FillSpans();
			for(int i=0;i<gridMain.Rows.Count;i++) {
				if(gridMain.Rows[i].Tag.GetType()==typeof(CovCat) && catNum==((CovCat)gridMain.Rows[i].Tag).CovCatNum) {
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void butAddCat_Click(object sender, System.EventArgs e) {
			CovCat covcat=new CovCat();
			covcat.CovOrder=CovCatC.Listt.Count;
			covcat.DefaultPercent=-1;
			FormInsCatEdit FormE=new FormInsCatEdit(covcat);
			FormE.IsNew=true;
			FormE.ShowDialog();
			if(FormE.DialogResult==DialogResult.OK){
				changed=true;
				FillSpans();
			}	
		}

		/*
		private void butDefaultsCheck_Click(object sender,EventArgs e) {
			string retVal=CheckDefaults();
			if(retVal=="") {
				MsgBox.Show(this,"Categories are set up correctly.  Spans have not been checked.  Push the Reset button to automatically reset the spans to default.");
			}
			else {
				MessageBox.Show(retVal);
			}
		}*/

		private string CheckDefaults(){
			//There needs to be at least 13 categories, each with an etype and no duplicates.
			string retVal="";
			int count;
			for(int i=1;i<14;i++) {//starts with 1 because we don't care about None category
				count=CovCats.CountForEbenCat((EbenefitCategory)i);
				if(count>1){
					retVal+="Duplicate category: "+((EbenefitCategory)i).ToString()+"\r\n";
				}
				if(count==0){
					retVal+="Missing category: "+((EbenefitCategory)i).ToString()+"\r\n";
				}
			}
			if(retVal != "") {
				retVal="The following errors must be fixed manually:\r\n\r\n"+retVal+"\r\n"
					+"Remember that any changes you make affect all current patients who are using those categories.";
			}
			return retVal;
		}

		private void butDefaultsReset_Click(object sender,EventArgs e) {
			string retVal=CheckDefaults();
			if(retVal!="") {
				MessageBox.Show(retVal);
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Reset orders and spans to default?")) {
				return;
			}
			CovCats.SetOrdersToDefault();
			CovCats.SetSpansToDefault();
			FillSpans();
			MsgBox.Show(this,"Done.");
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void FormInsCatsSetup_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidType.InsCats);
			}
		}

		

		

	}
}
