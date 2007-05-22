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
		private GroupBox groupDentalSchools;
		private ComboBox comboClass;
		private Label label1;
		private CheckBox checkAlphabetical;
		private bool changed;
		private OpenDental.UI.Button butCreateUsers;
		private GroupBox groupCreateUsers;
		private Label label3;
		private ComboBox comboUserGroup;
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
			this.groupDentalSchools = new System.Windows.Forms.GroupBox();
			this.checkAlphabetical = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboClass = new System.Windows.Forms.ComboBox();
			this.butCreateUsers = new OpenDental.UI.Button();
			this.groupCreateUsers = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboUserGroup = new System.Windows.Forms.ComboBox();
			this.groupDentalSchools.SuspendLayout();
			this.groupCreateUsers.SuspendLayout();
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
			this.butDown.Location = new System.Drawing.Point(587,450);
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
			this.butUp.Location = new System.Drawing.Point(587,411);
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
			this.butAdd.Location = new System.Drawing.Point(587,522);
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
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(466,642);
			this.gridMain.TabIndex = 13;
			this.gridMain.Title = "Providers";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// groupDentalSchools
			// 
			this.groupDentalSchools.Controls.Add(this.checkAlphabetical);
			this.groupDentalSchools.Controls.Add(this.label1);
			this.groupDentalSchools.Controls.Add(this.comboClass);
			this.groupDentalSchools.Location = new System.Drawing.Point(494,6);
			this.groupDentalSchools.Name = "groupDentalSchools";
			this.groupDentalSchools.Size = new System.Drawing.Size(184,100);
			this.groupDentalSchools.TabIndex = 14;
			this.groupDentalSchools.TabStop = false;
			this.groupDentalSchools.Text = "Dental Schools";
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
			// butCreateUsers
			// 
			this.butCreateUsers.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCreateUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCreateUsers.Autosize = true;
			this.butCreateUsers.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCreateUsers.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCreateUsers.CornerRadius = 4F;
			this.butCreateUsers.Location = new System.Drawing.Point(93,64);
			this.butCreateUsers.Name = "butCreateUsers";
			this.butCreateUsers.Size = new System.Drawing.Size(82,26);
			this.butCreateUsers.TabIndex = 15;
			this.butCreateUsers.Text = "Create";
			this.butCreateUsers.Click += new System.EventHandler(this.butCreateUsers_Click);
			// 
			// groupCreateUsers
			// 
			this.groupCreateUsers.Controls.Add(this.label3);
			this.groupCreateUsers.Controls.Add(this.comboUserGroup);
			this.groupCreateUsers.Controls.Add(this.butCreateUsers);
			this.groupCreateUsers.Location = new System.Drawing.Point(494,114);
			this.groupCreateUsers.Name = "groupCreateUsers";
			this.groupCreateUsers.Size = new System.Drawing.Size(184,100);
			this.groupCreateUsers.TabIndex = 18;
			this.groupCreateUsers.TabStop = false;
			this.groupCreateUsers.Text = "Create Users";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(7,14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(151,18);
			this.label3.TabIndex = 18;
			this.label3.Text = "User Group";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboUserGroup
			// 
			this.comboUserGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboUserGroup.FormattingEnabled = true;
			this.comboUserGroup.Location = new System.Drawing.Point(9,35);
			this.comboUserGroup.Name = "comboUserGroup";
			this.comboUserGroup.Size = new System.Drawing.Size(166,21);
			this.comboUserGroup.TabIndex = 17;
			// 
			// FormProviderSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(690,670);
			this.Controls.Add(this.groupCreateUsers);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.groupDentalSchools);
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
			this.groupDentalSchools.ResumeLayout(false);
			this.groupCreateUsers.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProviderSelect_Load(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.SecurityAdmin,DateTime.MinValue,true)){
				groupCreateUsers.Enabled=false;
			}
			else{
				for(int i=0;i<UserGroups.List.Length;i++){
					comboUserGroup.Items.Add(UserGroups.List[i].Description);
				}
			}
			if(PrefB.GetBool("EasyHideDentalSchools")){
				groupDentalSchools.Visible=false;
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
			if(gridMain.SelectedIndices.Length==1){
				selectedProvNum=PIn.PInt(table.Rows[gridMain.SelectedIndices[0]]["ProvNum"].ToString());
			}
			int scroll=gridMain.ScrollValue;
			Providers.Refresh();
			int schoolClass=0;
			if(groupDentalSchools.Visible && comboClass.SelectedIndex>0){
				schoolClass=SchoolClasses.List[comboClass.SelectedIndex-1].SchoolClassNum;
			}
			bool isAlph=false;
			if(groupDentalSchools.Visible && checkAlphabetical.Checked){
				isAlph=true;
			}
			table=Providers.Refresh(schoolClass,isAlph);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableProviders","Abbrev"),60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProviders","Last Name"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProviders","First Name"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProviders","User Name"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProviders","Hidden"),50,HorizontalAlignment.Center);
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
				row.Cells.Add(table.Rows[i]["UserName"].ToString());
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
			FillGrid();
		}

		private void comboClass_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormProvEdit FormP=new FormProvEdit();
			FormP.ProvCur=new Provider();
			FormP.ProvCur.ItemOrder=Providers.GetNextItemOrder();
			if(groupDentalSchools.Visible && comboClass.SelectedIndex>0){
				FormP.ProvCur.SchoolClassNum=SchoolClasses.List[comboClass.SelectedIndex-1].SchoolClassNum;
			}
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
			if(gridMain.SelectedIndices.Length!=1) {
				MsgBox.Show(this,"Please select exactly one provider first.");
				return;
			}
			if(gridMain.SelectedIndices[0]==0) {//already at top
				return;
			}
			Provider prov=Providers.GetProv(PIn.PInt(table.Rows[gridMain.SelectedIndices[0]]["ProvNum"].ToString()));
				//.ListLong[gridMain.GetSelectedIndex()];
			Provider otherprov=Providers.GetProv(PIn.PInt(table.Rows[gridMain.SelectedIndices[0]-1]["ProvNum"].ToString()));
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
			if(gridMain.SelectedIndices.Length!=1) {
				MsgBox.Show(this,"Please select exactly one provider first.");
				return;
			}
			if(gridMain.SelectedIndices[0]==Providers.ListLong.Length-1) {//already at bottom
				return;
			}
			Provider prov=Providers.GetProv(PIn.PInt(table.Rows[gridMain.SelectedIndices[0]]["ProvNum"].ToString()));
				//Providers.ListLong[gridMain.GetSelectedIndex()];
			Provider otherprov=Providers.GetProv(PIn.PInt(table.Rows[gridMain.SelectedIndices[0]+1]["ProvNum"].ToString()));
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
			FormP.ProvCur=Providers.GetProv(PIn.PInt(table.Rows[gridMain.SelectedIndices[0]]["ProvNum"].ToString()));
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK) {
				return;
			}
			changed=true;
			FillGrid();
		}

		///<summary>Not possible if no security admin.</summary>
		private void butCreateUsers_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select one or more providers first.");
				return;
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				if(table.Rows[i]["UserName"].ToString()!=""){
					MsgBox.Show(this,"Not allowed to create users on providers which already have users.");
					return;
				}
			}
			if(comboUserGroup.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a User Group first.");
				return;
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				Userod user=new Userod();
				user.UserGroupNum=UserGroups.List[comboUserGroup.SelectedIndex].UserGroupNum;
				user.ProvNum=PIn.PInt(table.Rows[gridMain.SelectedIndices[i]]["ProvNum"].ToString());
				user.UserName=GetUniqueUserName(table.Rows[gridMain.SelectedIndices[i]]["LName"].ToString(),
					table.Rows[gridMain.SelectedIndices[i]]["FName"].ToString());
				user.Password=user.UserName;//this will be enhanced later.
				try{
					Userods.InsertOrUpdate(true,user);
				}
				catch(ApplicationException ex){
					MessageBox.Show(ex.Message);
					changed=true;
					return;
				}
			}
			changed=true;
			FillGrid();
		}

		private string GetUniqueUserName(string lname,string fname){
			string name=lname;
			if(fname.Length>0){
				name+=fname.Substring(0,1);
			}
			if(Userods.IsUserNameUnique(name,0)){
				return name;
			}
			int fnameI=1;
			while(fnameI<fname.Length){
				name+=fname.Substring(fnameI,1);
				if(Userods.IsUserNameUnique(name,0)) {
					return name;
				}
				fnameI++;
			}
			//should be entire lname+fname at this point, but still not unique
			do{
				name+="x";
			}
			while(!Userods.IsUserNameUnique(name,0));
			return name;
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
				DataValid.SetInvalid(InvalidTypes.Providers | InvalidTypes.Security);
			}
			//SecurityLogs.MakeLogEntry("Providers","Altered Providers",user);
		}

		

		

		

	

	}
}
