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
	public class FormProviderSelect:System.Windows.Forms.Form {
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butUp;
		private OpenDental.UI.Button butAdd;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.ODGrid gridMain;
		private GroupBox groupView;
		private ComboBox comboClass;
		private Label label1;
		private CheckBox checkAlphabetical;
		private bool changed;
		//private User user;
		private DataTable table;
		
		///<summary></summary>
		public FormProviderSelect(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProviderSelect));
			this.butClose = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.groupView = new System.Windows.Forms.GroupBox();
			this.checkAlphabetical = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboClass = new System.Windows.Forms.ComboBox();
			this.groupView.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(587,628);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(82,26);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(587,359);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(82,26);
			this.butDown.TabIndex = 12;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(587,320);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(82,26);
			this.butUp.TabIndex = 11;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
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
			this.butAdd.Location = new System.Drawing.Point(587,507);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(82,26);
			this.butAdd.TabIndex = 10;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(16,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(466,642);
			this.gridMain.TabIndex = 13;
			this.gridMain.Title = "Providers";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// groupView
			// 
			this.groupView.Controls.Add(this.checkAlphabetical);
			this.groupView.Controls.Add(this.label1);
			this.groupView.Controls.Add(this.comboClass);
			this.groupView.Location = new System.Drawing.Point(494,6);
			this.groupView.Name = "groupView";
			this.groupView.Size = new System.Drawing.Size(184,100);
			this.groupView.TabIndex = 14;
			this.groupView.TabStop = false;
			this.groupView.Text = "View";
			// 
			// checkAlphabetical
			// 
			this.checkAlphabetical.Checked = true;
			this.checkAlphabetical.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAlphabetical.Location = new System.Drawing.Point(10,73);
			this.checkAlphabetical.Name = "checkAlphabetical";
			this.checkAlphabetical.Size = new System.Drawing.Size(165,18);
			this.checkAlphabetical.TabIndex = 17;
			this.checkAlphabetical.Text = "Alphabetical";
			this.checkAlphabetical.UseVisualStyleBackColor = true;
			this.checkAlphabetical.Click += new System.EventHandler(this.checkAlphabetical_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7,16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,18);
			this.label1.TabIndex = 16;
			this.label1.Text = "Classes";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboClass
			// 
			this.comboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClass.FormattingEnabled = true;
			this.comboClass.Location = new System.Drawing.Point(9,37);
			this.comboClass.Name = "comboClass";
			this.comboClass.Size = new System.Drawing.Size(166,21);
			this.comboClass.TabIndex = 0;
			this.comboClass.SelectionChangeCommitted += new System.EventHandler(this.comboClass_SelectionChangeCommitted);
			// 
			// FormProviderSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(690,670);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.groupView);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProviderSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Providers";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProviderSelect_Closing);
			this.Load += new System.EventHandler(this.FormProviderSelect_Load);
			this.groupView.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProviderSelect_Load(object sender, System.EventArgs e) {
			if(PrefB.GetBool("EasyHideDentalSchools")){
				groupView.Visible=false;
			}
			else{
				comboClass.Items.Add(Lan.g(this,"All"));
				comboClass.SelectedIndex=0;
				for(int i=0;i<SchoolClasses.List.Length;i++){
					comboClass.Items.Add(SchoolClasses.GetDescript(SchoolClasses.List[i]));
				}
				butUp.Visible=false;
				butDown.Visible=false;
			}
			FillGrid();
		}

		private void FillGrid(){
			int selectedProvNum=0;
			if(gridMain.GetSelectedIndex()!=-1){
				selectedProvNum=PIn.PInt(table.Rows[gridMain.GetSelectedIndex()]["ProvNum"].ToString());
			}
			int scroll=gridMain.ScrollValue;
			Providers.Refresh();
			int schoolClass=0;
			if(groupView.Visible && comboClass.SelectedIndex>0){
				schoolClass=SchoolClasses.List[comboClass.SelectedIndex-1].SchoolClassNum;
			}
			bool isAlph=false;
			if(groupView.Visible && checkAlphabetical.Checked){
				isAlph=true;
			}
			table=Providers.Refresh(schoolClass,isAlph);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableProviders","Abbrev"),65);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProviders","Last Name"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProviders","First Name"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProviders","Hidden"),55,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			if(!PrefB.GetBool("EasyHideDentalSchools")) {
				col=new ODGridColumn(Lan.g("TableProviders","Class"),90);
				gridMain.Columns.Add(col);
			}
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["Abbr"].ToString());
				row.Cells.Add(table.Rows[i]["LName"].ToString());
				row.Cells.Add(table.Rows[i]["FName"].ToString());
				if(table.Rows[i]["IsHidden"].ToString()=="1"){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
				if(!PrefB.GetBool("EasyHideDentalSchools")) {
					if(table.Rows[i]["GradYear"].ToString()!=""){
						row.Cells.Add(table.Rows[i]["GradYear"].ToString()+"-"+table.Rows[i]["Descript"].ToString());
					}
					else{
						row.Cells.Add("");
					}
				}
				//row.Tag
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i]["ProvNum"].ToString()==selectedProvNum.ToString()){
					gridMain.SetSelected(i,true);
					break;
				}
			}
			gridMain.ScrollValue=scroll;
		}

		private void checkAlphabetical_Click(object sender,EventArgs e) {
			//just always leave them  not visible if dental school
			/*if(checkAlphabetical.Checked){
				butUp.Enabled=false;
				butDown.Enabled=false;
			}
			else{
				butUp.Enabled=true;
				butDown.Enabled=true;
			}*/
			FillGrid();
		}

		private void comboClass_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormProvEdit FormP=new FormProvEdit();
			FormP.ProvCur=new Provider();
			FormP.ProvCur.ItemOrder=Providers.GetNextItemOrder();
			FormP.IsNew=true;
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			FillGrid();
			gridMain.ScrollToEnd();
			for(int i=0;i<table.Rows.Count;i++){//Providers.ListLong.Length;i++) {
				if(table.Rows[i]["ProvNum"].ToString()==FormP.ProvCur.ProvNum.ToString()){
					//Providers.ListLong[i].ProvNum==FormP.ProvCur.ProvNum) {
					gridMain.SetSelected(i,true);
					break;
				}
			}
		}

		///<summary>Won't be visible if using Dental Schools.</summary>
		private void butUp_Click(object sender, System.EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a provider first.");
				return;
			}
			if(gridMain.GetSelectedIndex()==0) {//already at top
				return;
			}
			Provider prov=Providers.GetProv(PIn.PInt(table.Rows[gridMain.GetSelectedIndex()]["ProvNum"].ToString()));
				//.ListLong[gridMain.GetSelectedIndex()];
			Provider otherprov=Providers.GetProv(PIn.PInt(table.Rows[gridMain.GetSelectedIndex()-1]["ProvNum"].ToString()));
				//Providers.ListLong[gridMain.GetSelectedIndex()-1];
			prov.ItemOrder--;
			Providers.Update(prov);
			otherprov.ItemOrder++;
			Providers.Update(otherprov);
			changed=true;
			FillGrid();
			gridMain.SetSelected(prov.ItemOrder,true);
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a provider first.");
				return;
			}
			if(gridMain.GetSelectedIndex()==Providers.ListLong.Length-1) {//already at bottom
				return;
			}
			Provider prov=Providers.GetProv(PIn.PInt(table.Rows[gridMain.GetSelectedIndex()]["ProvNum"].ToString()));
				//Providers.ListLong[gridMain.GetSelectedIndex()];
			Provider otherprov=Providers.GetProv(PIn.PInt(table.Rows[gridMain.GetSelectedIndex()+1]["ProvNum"].ToString()));
				//Providers.ListLong[gridMain.GetSelectedIndex()+1];
			prov.ItemOrder++;
			Providers.Update(prov);
			otherprov.ItemOrder--;
			Providers.Update(otherprov);
			changed=true;
			FillGrid();
			gridMain.SetSelected(prov.ItemOrder,true);
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormProvEdit FormP=new FormProvEdit();
			FormP.ProvCur=Providers.GetProv(PIn.PInt(table.Rows[gridMain.GetSelectedIndex()]["ProvNum"].ToString()));
				//Providers.ListLong[e.Row].Copy();
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK) {
				return;
			}
			changed=true;
			FillGrid();
		}

		/*
		private void listProviders_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listProviders.IndexFromPoint(e.X,e.Y)<0)
				return;
			Providers.Selected=listProviders.IndexFromPoint(e.X,e.Y);
			listProviders.SelectedIndex=listProviders.IndexFromPoint(e.X,e.Y);
		}

		private void listProviders_DoubleClick(object sender, System.EventArgs e) {
			if(listProviders.SelectedIndex<0)
				return;
			FormProvEdit FormP=new FormProvEdit(Providers.ListLong[Providers.Selected]);
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			FillList();
		}*/

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormProviderSelect_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Providers);
			}
			//SecurityLogs.MakeLogEntry("Providers","Altered Providers",user);
		}

		

		

	

	}
}
