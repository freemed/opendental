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
	public class FormUserGroups : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private ListBox listGroups;
		private OpenDental.UI.Button butAddGroup;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormUserGroups()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUserGroups));
			this.butClose = new OpenDental.UI.Button();
			this.listGroups = new System.Windows.Forms.ListBox();
			this.butAddGroup = new OpenDental.UI.Button();
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
			this.butClose.Location = new System.Drawing.Point(295,369);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(80,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// listGroups
			// 
			this.listGroups.FormattingEnabled = true;
			this.listGroups.Location = new System.Drawing.Point(36,39);
			this.listGroups.Name = "listGroups";
			this.listGroups.Size = new System.Drawing.Size(183,355);
			this.listGroups.TabIndex = 1;
			this.listGroups.DoubleClick += new System.EventHandler(this.listGroups_DoubleClick);
			// 
			// butAddGroup
			// 
			this.butAddGroup.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddGroup.Autosize = true;
			this.butAddGroup.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddGroup.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddGroup.CornerRadius = 4F;
			this.butAddGroup.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddGroup.Location = new System.Drawing.Point(295,248);
			this.butAddGroup.Name = "butAddGroup";
			this.butAddGroup.Size = new System.Drawing.Size(80,25);
			this.butAddGroup.TabIndex = 2;
			this.butAddGroup.Text = "Add";
			this.butAddGroup.Click += new System.EventHandler(this.butAddGroup_Click);
			// 
			// FormUserGroups
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(404,420);
			this.Controls.Add(this.butAddGroup);
			this.Controls.Add(this.listGroups);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormUserGroups";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "User Groups";
			this.Load += new System.EventHandler(this.FormUserGroups_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormUserGroups_Load(object sender,EventArgs e) {
			FillList();
		}

		private void FillList(){
			UserGroups.Refresh();
			listGroups.Items.Clear();
			for(int i=0;i<UserGroups.List.Length;i++){
				listGroups.Items.Add(UserGroups.List[i].Description);
			}
		}

		private void butAddGroup_Click(object sender,EventArgs e) {
			UserGroup group=new UserGroup();
			FormUserGroupEdit FormU=new FormUserGroupEdit(group);
			FormU.IsNew=true;
			FormU.ShowDialog();
			if(FormU.DialogResult==DialogResult.Cancel) {
				return;
			}
			FillList();
		}

		private void listGroups_DoubleClick(object sender,EventArgs e) {
			if(listGroups.SelectedIndex==-1) {
				return;
			}
			UserGroup group=UserGroups.List[listGroups.SelectedIndex];
			FormUserGroupEdit FormU=new FormUserGroupEdit(group);
			FormU.ShowDialog();
			if(FormU.DialogResult==DialogResult.Cancel) {
				return;
			}
			FillList();
			//reselect group
			/*for(int i=0;i<treeUsers.Nodes.Count;i++) {
				if((int)treeUsers.Nodes[i].Tag==group.UserGroupNum) {
					treeUsers.SelectedNode=treeUsers.Nodes[i];
					SelectedGroupNum=group.UserGroupNum;
				}
			}*/
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		

		

		


	}
}





















