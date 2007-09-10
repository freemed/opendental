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
	public class FormSecurity:System.Windows.Forms.Form {
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butAddGroup;
		private OpenDental.UI.Button butAddUser;
		private System.Windows.Forms.TreeView treePermissions;
		private System.Windows.Forms.ImageList imageListPerm;
		private System.Windows.Forms.Label labelPerm;
		private System.ComponentModel.IContainer components;
		private int SelectedGroupNum;
		private TreeNode clickedPermNode;
		private System.Windows.Forms.CheckBox checkTimecardSecurityEnabled;
		private OpenDental.UI.Button butSetAll;
		private OpenDental.UI.ODGrid gridMain;
		private bool changed;
		private ComboBox comboUsers;
		private ComboBox comboSchoolClass;
		private Label labelSchoolClass;
		private DataTable table;

		///<summary></summary>
		public FormSecurity()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSecurity));
			this.treePermissions = new System.Windows.Forms.TreeView();
			this.imageListPerm = new System.Windows.Forms.ImageList(this.components);
			this.labelPerm = new System.Windows.Forms.Label();
			this.checkTimecardSecurityEnabled = new System.Windows.Forms.CheckBox();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butSetAll = new OpenDental.UI.Button();
			this.butAddUser = new OpenDental.UI.Button();
			this.butAddGroup = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.comboUsers = new System.Windows.Forms.ComboBox();
			this.comboSchoolClass = new System.Windows.Forms.ComboBox();
			this.labelSchoolClass = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// treePermissions
			// 
			this.treePermissions.HideSelection = false;
			this.treePermissions.ImageIndex = 0;
			this.treePermissions.ImageList = this.imageListPerm;
			this.treePermissions.Location = new System.Drawing.Point(470,29);
			this.treePermissions.Name = "treePermissions";
			this.treePermissions.SelectedImageIndex = 0;
			this.treePermissions.ShowPlusMinus = false;
			this.treePermissions.ShowRootLines = false;
			this.treePermissions.Size = new System.Drawing.Size(417,567);
			this.treePermissions.TabIndex = 6;
			this.treePermissions.DoubleClick += new System.EventHandler(this.treePermissions_DoubleClick);
			this.treePermissions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treePermissions_AfterSelect);
			this.treePermissions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treePermissions_MouseDown);
			// 
			// imageListPerm
			// 
			this.imageListPerm.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPerm.ImageStream")));
			this.imageListPerm.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListPerm.Images.SetKeyName(0,"grayBox.gif");
			this.imageListPerm.Images.SetKeyName(1,"checkBoxUnchecked.gif");
			this.imageListPerm.Images.SetKeyName(2,"checkBoxChecked.gif");
			// 
			// labelPerm
			// 
			this.labelPerm.Location = new System.Drawing.Point(468,5);
			this.labelPerm.Name = "labelPerm";
			this.labelPerm.Size = new System.Drawing.Size(285,19);
			this.labelPerm.TabIndex = 5;
			this.labelPerm.Text = "Permissions for group:";
			this.labelPerm.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkTimecardSecurityEnabled
			// 
			this.checkTimecardSecurityEnabled.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkTimecardSecurityEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTimecardSecurityEnabled.Location = new System.Drawing.Point(222,618);
			this.checkTimecardSecurityEnabled.Name = "checkTimecardSecurityEnabled";
			this.checkTimecardSecurityEnabled.Size = new System.Drawing.Size(192,19);
			this.checkTimecardSecurityEnabled.TabIndex = 57;
			this.checkTimecardSecurityEnabled.Text = "TimecardSecurityEnabled";
			this.checkTimecardSecurityEnabled.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(8,29);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(456,567);
			this.gridMain.TabIndex = 59;
			this.gridMain.Title = "Users";
			this.gridMain.TranslationName = "TableSecurity";
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butSetAll
			// 
			this.butSetAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSetAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSetAll.Autosize = true;
			this.butSetAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetAll.CornerRadius = 4F;
			this.butSetAll.Location = new System.Drawing.Point(470,610);
			this.butSetAll.Name = "butSetAll";
			this.butSetAll.Size = new System.Drawing.Size(79,25);
			this.butSetAll.TabIndex = 58;
			this.butSetAll.Text = "Set All";
			this.butSetAll.Click += new System.EventHandler(this.butSetAll_Click);
			// 
			// butAddUser
			// 
			this.butAddUser.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAddUser.Autosize = true;
			this.butAddUser.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddUser.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddUser.CornerRadius = 4F;
			this.butAddUser.Location = new System.Drawing.Point(118,610);
			this.butAddUser.Name = "butAddUser";
			this.butAddUser.Size = new System.Drawing.Size(75,25);
			this.butAddUser.TabIndex = 0;
			this.butAddUser.Text = "Add User";
			this.butAddUser.Click += new System.EventHandler(this.butAddUser_Click);
			// 
			// butAddGroup
			// 
			this.butAddGroup.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAddGroup.Autosize = true;
			this.butAddGroup.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddGroup.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddGroup.CornerRadius = 4F;
			this.butAddGroup.Location = new System.Drawing.Point(8,610);
			this.butAddGroup.Name = "butAddGroup";
			this.butAddGroup.Size = new System.Drawing.Size(75,25);
			this.butAddGroup.TabIndex = 1;
			this.butAddGroup.Text = "Edit Groups";
			this.butAddGroup.Click += new System.EventHandler(this.butEditGroups_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(812,610);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,25);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// comboUsers
			// 
			this.comboUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboUsers.FormattingEnabled = true;
			this.comboUsers.Location = new System.Drawing.Point(8,5);
			this.comboUsers.Name = "comboUsers";
			this.comboUsers.Size = new System.Drawing.Size(182,21);
			this.comboUsers.TabIndex = 60;
			this.comboUsers.SelectionChangeCommitted += new System.EventHandler(this.comboUsers_SelectionChangeCommitted);
			// 
			// comboSchoolClass
			// 
			this.comboSchoolClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSchoolClass.Location = new System.Drawing.Point(276,5);
			this.comboSchoolClass.MaxDropDownItems = 30;
			this.comboSchoolClass.Name = "comboSchoolClass";
			this.comboSchoolClass.Size = new System.Drawing.Size(168,21);
			this.comboSchoolClass.TabIndex = 90;
			this.comboSchoolClass.SelectionChangeCommitted += new System.EventHandler(this.comboSchoolClass_SelectionChangeCommitted);
			// 
			// labelSchoolClass
			// 
			this.labelSchoolClass.Location = new System.Drawing.Point(203,8);
			this.labelSchoolClass.Name = "labelSchoolClass";
			this.labelSchoolClass.Size = new System.Drawing.Size(72,16);
			this.labelSchoolClass.TabIndex = 91;
			this.labelSchoolClass.Text = "Class";
			this.labelSchoolClass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormSecurity
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(894,644);
			this.Controls.Add(this.comboSchoolClass);
			this.Controls.Add(this.labelSchoolClass);
			this.Controls.Add(this.comboUsers);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butSetAll);
			this.Controls.Add(this.treePermissions);
			this.Controls.Add(this.checkTimecardSecurityEnabled);
			this.Controls.Add(this.butAddUser);
			this.Controls.Add(this.butAddGroup);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.labelPerm);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSecurity";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Security";
			this.Load += new System.EventHandler(this.FormSecurity_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormSecurity_Load(object sender, System.EventArgs e) {
			comboUsers.Items.Add(Lan.g(this,"All Users"));
			comboUsers.Items.Add(Lan.g(this,"Providers"));
			comboUsers.Items.Add(Lan.g(this,"Employees"));
			comboUsers.Items.Add(Lan.g(this,"Other"));
			comboUsers.SelectedIndex=0;
			if(PrefB.GetBool("EasyHideDentalSchools")){
				comboSchoolClass.Visible=false;
				labelSchoolClass.Visible=false;
			}
			else{
				comboSchoolClass.Items.Add(Lan.g(this,"All"));
				comboSchoolClass.SelectedIndex=0;
				for(int i=0;i<SchoolClasses.List.Length;i++) {
					comboSchoolClass.Items.Add(SchoolClasses.GetDescript(SchoolClasses.List[i]));
				}
			}
			FillTreePermissionsInitial();
			FillUsers();
			FillTreePerm();
			checkTimecardSecurityEnabled.Checked=PrefB.GetBool("TimecardSecurityEnabled");
		}

		private void FillTreePermissionsInitial(){
			TreeNode node;
			TreeNode node2;//second level
			TreeNode node3;
			node=SetNode("Main Menu");
				node2=SetNode(Permissions.Setup);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.ChooseDatabase);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.SecurityAdmin);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.Schedules);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.Blockouts);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.UserQuery);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.Reports);
					node.Nodes.Add(node2);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.AppointmentsModule);
				node2=SetNode(Permissions.AppointmentCreate);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.AppointmentMove);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.AppointmentEdit);
					node.Nodes.Add(node2);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.FamilyModule);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.AccountModule);
				node2=SetNode(Permissions.ClaimsSentEdit);
					node.Nodes.Add(node2);
				node2=SetNode("Payment");
					node3=SetNode(Permissions.PaymentCreate);
						node2.Nodes.Add(node3);
					node3=SetNode(Permissions.PaymentEdit);
						node2.Nodes.Add(node3);
					node.Nodes.Add(node2);
				node2=SetNode("Adjustment");
					node3=SetNode(Permissions.AdjustmentCreate);
						node2.Nodes.Add(node3);
					node3=SetNode(Permissions.AdjustmentEdit);
						node2.Nodes.Add(node3);
					node.Nodes.Add(node2);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.TPModule);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.ChartModule);
				node2=SetNode("Procedure");
					node3=SetNode(Permissions.ProcComplCreate);
						node2.Nodes.Add(node3);
					node3=SetNode(Permissions.ProcComplEdit);
						node2.Nodes.Add(node3);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.RxCreate);
					node.Nodes.Add(node2);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.ImagesModule);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.ManageModule);
				node2=SetNode(Permissions.Accounting);
					node3=SetNode(Permissions.AccountingCreate);
						node2.Nodes.Add(node3);
					node3=SetNode(Permissions.AccountingEdit);
						node2.Nodes.Add(node3);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.DepositSlips);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.Backup);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.TimecardsEditAll);
					node.Nodes.Add(node2);
				treePermissions.Nodes.Add(node);
			treePermissions.ExpandAll();
		}

		///<summary>This just keeps FillTreePermissionsInitial looking cleaner.</summary>
		private TreeNode SetNode(Permissions perm){
			TreeNode retVal=new TreeNode();
			retVal.Text=GroupPermissions.GetDesc(perm);
			retVal.Tag=perm;
			retVal.ImageIndex=1;
			retVal.SelectedImageIndex=1;
			return retVal;
		}

		///<summary>Only called from FillTreePermissionsInitial</summary>
		private TreeNode SetNode(string text){
			TreeNode retVal=new TreeNode();
			retVal.Text=Lan.g(this,text);
			retVal.Tag=Permissions.None;
			retVal.ImageIndex=0;
			retVal.SelectedImageIndex=0;
			return retVal;
		}

		private void FillUsers(){
			UserGroups.Refresh();
			Userods.Refresh();
			SelectedGroupNum=0;
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableSecurity","Username"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSecurity","Group"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSecurity","Employee"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSecurity","Provider"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSecurity","Clinic"),90);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			string usertype="all";
			if(comboUsers.SelectedIndex==1){
				usertype="prov";
			}
			if(comboUsers.SelectedIndex==2) {
				usertype="emp";
			}
			if(comboUsers.SelectedIndex==3) {
				usertype="other";
			}
			int classNum=0;
			if(comboSchoolClass.Visible && comboSchoolClass.SelectedIndex>0){
				classNum=SchoolClasses.List[comboSchoolClass.SelectedIndex-1].SchoolClassNum;
			}
			table=Userods.RefreshSecurity(usertype,classNum);
			string userdesc;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				userdesc=table.Rows[i]["UserName"].ToString();
				if(table.Rows[i]["IsHidden"].ToString()=="1"){
					userdesc+=Lan.g(this,"(hidden)");
				}
				row.Cells.Add(userdesc);
				row.Cells.Add(UserGroups.GetGroup(PIn.PInt(table.Rows[i]["UserGroupNum"].ToString())).Description);
				row.Cells.Add(Employees.GetNameFL(PIn.PInt(table.Rows[i]["EmployeeNum"].ToString())));
				row.Cells.Add(Providers.GetNameLF(PIn.PInt(table.Rows[i]["ProvNum"].ToString())));
				row.Cells.Add(Clinics.GetDesc(PIn.PInt(table.Rows[i]["ClinicNum"].ToString())));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			SelectedGroupNum=PIn.PInt(table.Rows[e.Row]["UserGroupNum"].ToString());
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i]["UserGroupNum"].ToString()==SelectedGroupNum.ToString()){
					gridMain.Rows[i].ColorText=Color.Red;
				}
				else{
					gridMain.Rows[i].ColorText=Color.Black;
				}
			}
			gridMain.Invalidate();
			FillTreePerm();
		}

		private void comboUsers_SelectionChangeCommitted(object sender,EventArgs e) {
			FillUsers();
			FillTreePerm();
		}

		private void comboSchoolClass_SelectionChangeCommitted(object sender,EventArgs e) {
			FillUsers();
			FillTreePerm();
		}

		private void butEditGroups_Click(object sender, System.EventArgs e) {
			FormUserGroups FormU=new FormUserGroups();
			FormU.ShowDialog();
			FillUsers();
			FillTreePerm();
			changed=true;
		}

		private void butAddUser_Click(object sender, System.EventArgs e) {
			Userod user=new Userod();
			user.UserGroupNum=SelectedGroupNum;
			FormUserEdit FormU=new FormUserEdit(user);
			FormU.IsNew=true;
			FormU.ShowDialog();
			if(FormU.DialogResult==DialogResult.Cancel){
				return;
			}
			FillUsers();
			FillTreePerm();
			changed=true;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Userod user=UserodB.GetUser(PIn.PInt(table.Rows[e.Row]["UserNum"].ToString()));
			FormUserEdit FormU=new FormUserEdit(user);
			FormU.ShowDialog();
			if(FormU.DialogResult==DialogResult.Cancel){
				return;
			}
			FillUsers();
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i]["UserNum"].ToString()==FormU.UserCur.UserNum.ToString()){
					gridMain.SetSelected(i,true);
					SelectedGroupNum=FormU.UserCur.UserGroupNum;
				}
			}
			FillTreePerm();
			changed=true;
		}

		private void FillTreePerm(){
			GroupPermissions.Refresh();
			if(SelectedGroupNum==0){
				labelPerm.Text="";
				treePermissions.Enabled=false;
			}
			else{
				labelPerm.Text=Lan.g(this,"Permissions for group:")+"  "+UserGroups.GetGroup(SelectedGroupNum).Description;
				treePermissions.Enabled=true;
			}
			for(int i=0;i<treePermissions.Nodes.Count;i++){
				FillNodes(treePermissions.Nodes[i],SelectedGroupNum);
			}
		}

		///<summary>A recursive function that sets the checkbox for a node.  Also sets the text for the node.</summary>
		private void FillNodes(TreeNode node,int userGroupNum){
			//first, any child nodes
			for(int i=0;i<node.Nodes.Count;i++){
				FillNodes(node.Nodes[i],userGroupNum);
			}
			//then this node
			if(node.ImageIndex==0){
				return;
			}
			node.ImageIndex=1;
			node.Text=GroupPermissions.GetDesc((Permissions)node.Tag);
			for(int i=0;i<GroupPermissions.List.Length;i++){
				if(GroupPermissions.List[i].UserGroupNum==userGroupNum
					&& GroupPermissions.List[i].PermType==(Permissions)node.Tag)
				{
					node.ImageIndex=2;
					if(GroupPermissions.List[i].NewerDate.Year>1880){
						node.Text+=" ("+Lan.g(this,"if date newer than")+" "+GroupPermissions.List[i].NewerDate.ToShortDateString()+")";
					}
					else if(GroupPermissions.List[i].NewerDays>0){
						node.Text+=" ("+Lan.g(this,"if days newer than")+" "+GroupPermissions.List[i].NewerDays.ToString()+")";
					}
				}
			}
		}

		private void treePermissions_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			clickedPermNode=treePermissions.GetNodeAt(e.X,e.Y);
			if(clickedPermNode==null){
				return;
			}
			//treePermissions.BeginUpdate();
			if(clickedPermNode.Parent==null){//level 1
				if(e.X<5 || e.X>17){
					return;
				}
			}
			else if(clickedPermNode.Parent.Parent==null){//level 2
				if(e.X<24 || e.X>36){
					return;
				}
			}
			else if(clickedPermNode.Parent.Parent.Parent==null){//level 3
				if(e.X<43 || e.X>55){
					return;
				}
			}
			if(clickedPermNode.ImageIndex==1){//unchecked, so need to add a permission
				GroupPermission perm=new GroupPermission();
				perm.PermType=(Permissions)clickedPermNode.Tag;
				perm.UserGroupNum=SelectedGroupNum;
				if(GroupPermissions.PermTakesDates(perm.PermType)){
					FormGroupPermEdit FormG=new FormGroupPermEdit(perm);
					FormG.IsNew=true;
					FormG.ShowDialog();
					if(FormG.DialogResult==DialogResult.Cancel){
						treePermissions.EndUpdate();
						return;
					}
				}
				else{
					try{
						GroupPermissions.InsertOrUpdate(perm,true);
					}
					catch(Exception ex){
						MessageBox.Show(ex.Message);
						return;
					}
				}
			}
			else if(clickedPermNode.ImageIndex==2){//checked, so need to delete the perm
				try{
					GroupPermissions.RemovePermission(SelectedGroupNum,(Permissions)clickedPermNode.Tag);
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
					return;
				}
			}
			FillTreePerm();
			changed=true;		
		}

		private void treePermissions_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			treePermissions.SelectedNode=null;
			treePermissions.EndUpdate();
		}

		private void treePermissions_DoubleClick(object sender, System.EventArgs e) {
			if(clickedPermNode==null){
				return;
			}
			Permissions permType=(Permissions)clickedPermNode.Tag;
			if(!GroupPermissions.PermTakesDates(permType)){
				return;
			}
			GroupPermission perm=GroupPermissions.GetPerm(SelectedGroupNum,(Permissions)clickedPermNode.Tag);
			if(perm==null){
				return;
			}
			FormGroupPermEdit FormG=new FormGroupPermEdit(perm);
			FormG.ShowDialog();
			if(FormG.DialogResult==DialogResult.Cancel){
				return;
			}
			FillTreePerm();
			changed=true;
		}

		private void butSetAll_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select user first.");
				return;
			}
			GroupPermission perm;
			for(int i=0;i<Enum.GetNames(typeof(Permissions)).Length;i++){
				if(i==(int)Permissions.SecurityAdmin
					|| i==(int)Permissions.StartupMultiUserOld
					|| i==(int)Permissions.StartupSingleUserOld)
				{
					continue;
				}
				perm=GroupPermissions.GetPerm(SelectedGroupNum,(Permissions)i);
				if(perm==null){
					perm=new GroupPermission();
					perm.PermType=(Permissions)i;
					perm.UserGroupNum=SelectedGroupNum;
					try{
						GroupPermissions.InsertOrUpdate(perm,true);
					}
					catch(Exception ex){
						MessageBox.Show(ex.Message);
					}
					changed=true;
				}
			}
			FillTreePerm();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Security);
			}
			if(Prefs.UpdateBool("TimecardSecurityEnabled",checkTimecardSecurityEnabled.Checked)){
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			Close();
		}

		
		

		

		

	

		

		

		

		

		

		

		

		

		

		

		

		
		


	}
}





















