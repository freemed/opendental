using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormPasswordReset : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textMasterPass;
		private OpenDental.UI.Button butCancel;
		private string masterHash;

		///<summary></summary>
		public FormPasswordReset(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPasswordReset));
			this.textMasterPass = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textMasterPass
			// 
			this.textMasterPass.Location = new System.Drawing.Point(120,24);
			this.textMasterPass.MaxLength = 100;
			this.textMasterPass.Name = "textMasterPass";
			this.textMasterPass.Size = new System.Drawing.Size(212,20);
			this.textMasterPass.TabIndex = 35;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(384,96);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76,26);
			this.butOK.TabIndex = 37;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18,26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,50);
			this.label1.TabIndex = 38;
			this.label1.Text = "Master Password (you must call us)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(384,132);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76,26);
			this.butCancel.TabIndex = 39;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormPasswordReset
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(478,176);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.textMasterPass);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPasswordReset";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reset Password";
			this.Load += new System.EventHandler(this.FormRP_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRP_Load(object sender, System.EventArgs e) {
			//it does not compromise security to include the hash to the master password in the code
			//because the user must still enter the password, not the hash.
			masterHash="78sfTin/RP0rI84zv2Xc8Q==";
				//version 3.5: "1251671001032231238111186944262869879186";
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Debug.WriteLine(UserodB.EncryptPassword(textMasterPass.Text));
			if(!UserodB.CheckPassword(textMasterPass.Text,masterHash)){
				MessageBox.Show(Lan.g(this,"Master password incorrect."));
				return;
			}
			Security.ResetPassword();
			//PermissionsOld.Refresh();
			//MessageBox.Show(Lan.g(this,"Security Administration permission has been reset."));
			//DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}




	}
}








