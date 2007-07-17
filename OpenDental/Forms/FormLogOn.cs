using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormLogOn : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.ListBox listUser;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.Button butResetPassword;
		private CheckBox chkboxShowHidden;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormLogOn()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogOn));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.listUser = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.butResetPassword = new System.Windows.Forms.Button();
			this.chkboxShowHidden = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(361, 321);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "Exit";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(361, 280);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listUser
			// 
			this.listUser.Location = new System.Drawing.Point(51, 31);
			this.listUser.Name = "listUser";
			this.listUser.Size = new System.Drawing.Size(120, 316);
			this.listUser.TabIndex = 2;
			this.listUser.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listUser_MouseUp);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(50, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(122, 18);
			this.label1.TabIndex = 6;
			this.label1.Text = "User";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(188, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(122, 18);
			this.label2.TabIndex = 7;
			this.label2.Text = "Password";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(189, 31);
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '*';
			this.textPassword.Size = new System.Drawing.Size(215, 20);
			this.textPassword.TabIndex = 0;
			// 
			// butResetPassword
			// 
			this.butResetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.butResetPassword.ForeColor = System.Drawing.SystemColors.Control;
			this.butResetPassword.Location = new System.Drawing.Point(-1, 334);
			this.butResetPassword.Name = "butResetPassword";
			this.butResetPassword.Size = new System.Drawing.Size(50, 38);
			this.butResetPassword.TabIndex = 45;
			this.butResetPassword.Click += new System.EventHandler(this.butResetPassword_Click);
			// 
			// chkboxShowHidden
			// 
			this.chkboxShowHidden.AutoSize = true;
			this.chkboxShowHidden.Location = new System.Drawing.Point(191, 57);
			this.chkboxShowHidden.Name = "chkboxShowHidden";
			this.chkboxShowHidden.Size = new System.Drawing.Size(120, 17);
			this.chkboxShowHidden.TabIndex = 46;
			this.chkboxShowHidden.Text = "Show Hidden Users";
			this.chkboxShowHidden.UseVisualStyleBackColor = true;
			this.chkboxShowHidden.CheckedChanged += new System.EventHandler(this.chkboxShowHidden_CheckedChanged);
			// 
			// FormLogOn
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(464, 378);
			this.Controls.Add(this.chkboxShowHidden);
			this.Controls.Add(this.butResetPassword);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listUser);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FormLogOn";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Log On";
			this.MinimumSizeChanged += new System.EventHandler(this.FormLogOn_MinimumSizeChanged);
			this.Load += new System.EventHandler(this.FormLogOn_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormLogOn_Load(object sender, System.EventArgs e) {
			FillListBox();
		}

		private void listUser_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			textPassword.Focus();
		}

		private void butResetPassword_Click(object sender, System.EventArgs e) {
			FormPasswordReset FormPR=new FormPasswordReset();
			FormPR.ShowDialog();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Userod selectedUser = (Userod) listUser.SelectedItem;
			//if(Userods.Listt[listUser.SelectedIndex].Password!=""){
			//    if(!UserodB.CheckPassword(textPassword.Text,Userods.Listt[listUser.SelectedIndex].Password)){
			//        MsgBox.Show(this,"Incorrect password");
			//        return;
			//    }
			//}
			//Security.CurUser=Userods.Listt[listUser.SelectedIndex].Copy();
			if (selectedUser.Password != "")
			{
				if (!UserodB.CheckPassword(textPassword.Text, selectedUser.Password))
				{
					MsgBox.Show(this, "Incorrect password");
					return;
				}
			}
			Security.CurUser = selectedUser.Copy();
			//SecurityLogs.MakeLogEntry(Permissions.StartupSingleUser,"");
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormLogOn_MinimumSizeChanged(object sender, EventArgs e)
		{		
			this.Parent.MinimumSize = this.MinimumSize;
		}
		private void FillListBox()
		{
			listUser.BeginUpdate();
			listUser.Items.Clear();
			for (int i = 0; i < Userods.Listt.Count; i++)
			{
				// Note Employees should be refreshed at this point
				// Need to check if you add employee and then logout is the 
				// employee list refreshed.
				Userod user = Userods.Listt[i];

				if (user.EmployeeNum == 0 || this.chkboxShowHidden.Checked)
					listUser.Items.Add(user);
				else
				{
					if (Employees.IsHidden(user.EmployeeNum) != 1)
						listUser.Items.Add(user);
				}


				if (Security.CurUser != null && Userods.Listt[i].UserNum == Security.CurUser.UserNum)
				{
					if (listUser.Items.Count != 0)
						listUser.SelectedIndex = listUser.Items.Count - 1; // last item added
				}
			}
			if (listUser.SelectedIndex == -1)
			{
				listUser.SelectedIndex = 0;
			}
			listUser.EndUpdate();

		}

		private void chkboxShowHidden_CheckedChanged(object sender, EventArgs e)
		{
			FillListBox();
		}

		

		
		


	}

	//<summary></summary>
	//public class Passwords{

		
	//}


}





















