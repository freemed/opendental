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
	public class FormUserEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textUserName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listUserGroup;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butPassword;
		private System.Windows.Forms.ListBox listEmployee;
		private System.Windows.Forms.Label label2;
		///<summary></summary>
		public Userod UserCur;

		///<summary></summary>
		public FormUserEdit(Userod userCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			UserCur=userCur.Copy();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUserEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textUserName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.listUserGroup = new System.Windows.Forms.ListBox();
			this.butPassword = new OpenDental.UI.Button();
			this.listEmployee = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(666,381);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(666,340);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(17,26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118,20);
			this.label1.TabIndex = 2;
			this.label1.Text = "Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textUserName
			// 
			this.textUserName.Location = new System.Drawing.Point(140,26);
			this.textUserName.Name = "textUserName";
			this.textUserName.Size = new System.Drawing.Size(271,20);
			this.textUserName.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(17,57);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(118,20);
			this.label3.TabIndex = 6;
			this.label3.Text = "User Group";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listUserGroup
			// 
			this.listUserGroup.Location = new System.Drawing.Point(141,58);
			this.listUserGroup.Name = "listUserGroup";
			this.listUserGroup.Size = new System.Drawing.Size(270,225);
			this.listUserGroup.TabIndex = 7;
			// 
			// butPassword
			// 
			this.butPassword.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butPassword.Autosize = true;
			this.butPassword.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPassword.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPassword.CornerRadius = 4F;
			this.butPassword.Location = new System.Drawing.Point(412,381);
			this.butPassword.Name = "butPassword";
			this.butPassword.Size = new System.Drawing.Size(103,26);
			this.butPassword.TabIndex = 8;
			this.butPassword.Text = "Change Password";
			this.butPassword.Click += new System.EventHandler(this.butPassword_Click);
			// 
			// listEmployee
			// 
			this.listEmployee.Location = new System.Drawing.Point(455,58);
			this.listEmployee.Name = "listEmployee";
			this.listEmployee.Size = new System.Drawing.Size(124,225);
			this.listEmployee.TabIndex = 11;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(454,34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(222,20);
			this.label2.TabIndex = 10;
			this.label2.Text = "Employee (for timecards)";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormUserEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(777,432);
			this.Controls.Add(this.listEmployee);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butPassword);
			this.Controls.Add(this.textUserName);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.listUserGroup);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormUserEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "User Edit";
			this.Load += new System.EventHandler(this.FormUserEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormUserEdit_Load(object sender, System.EventArgs e) {
			textUserName.Text=UserCur.UserName;
			for(int i=0;i<UserGroups.List.Length;i++){
				listUserGroup.Items.Add(UserGroups.List[i].Description);
				if(UserCur.UserGroupNum==UserGroups.List[i].UserGroupNum){
					listUserGroup.SelectedIndex=i;
				}
			}
			if(listUserGroup.SelectedIndex==-1){//never allowed to delete last group, so this won't fail
				listUserGroup.SelectedIndex=0;
			}
			listEmployee.Items.Clear();
			listEmployee.Items.Add(Lan.g(this,"none"));
			listEmployee.SelectedIndex=0;
			for(int i=0;i<Employees.ListShort.Length;i++){
				listEmployee.Items.Add(Employees.GetNameFL(Employees.ListShort[i]));
				if(UserCur.EmployeeNum==Employees.ListShort[i].EmployeeNum){
					listEmployee.SelectedIndex=i+1;
				}
			}
			if(UserCur.Password==""){
				butPassword.Text=Lan.g(this,"Create Password");
			}
			
		}

		private void butPassword_Click(object sender, System.EventArgs e) {
			bool isCreate=UserCur.Password=="";
			FormUserPassword FormU=new FormUserPassword(isCreate);
			FormU.ShowDialog();
			if(FormU.DialogResult==DialogResult.Cancel){
				return;
			}
			UserCur.Password=FormU.hashedResult;
			if(UserCur.Password==""){
				butPassword.Text=Lan.g(this,"Create Password");
			}
			else{
				butPassword.Text=Lan.g(this,"Change Password");
			}
		}

		/*private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			try{
				UserCur.Delete();
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}*/

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textUserName.Text==""){
				MsgBox.Show(this,"Please enter a username.");
				return;
			}
			UserCur.UserName=textUserName.Text;
			UserCur.UserGroupNum=UserGroups.List[listUserGroup.SelectedIndex].UserGroupNum;
			if(listEmployee.SelectedIndex==0){
				UserCur.EmployeeNum=0;
			}
			else{
				UserCur.EmployeeNum=Employees.ListShort[listEmployee.SelectedIndex-1].EmployeeNum;
			}
			try{
				Userods.InsertOrUpdate(IsNew,UserCur);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}





















