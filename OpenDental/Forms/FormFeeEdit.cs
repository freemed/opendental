using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormFeeEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private OpenDental.ValidDouble textFee;
		private System.Windows.Forms.CheckBox checkDefFee;
		private System.Windows.Forms.CheckBox checkDefCov;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary></summary>
		public Fee FeeCur;
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormFeeEdit(){//(Fee feeCur){
			InitializeComponent();// Required for Windows Form Designer support
			//FeeCur=feeCur;
			//checkDefFee.Checked=FeeCur.UseDefaultFee;
			//checkDefCov.Checked=FeeCur.UseDefaultCov;
			Lan.F(this, new System.Windows.Forms.Control[] {
				//exclude:
				this.checkDefCov,
				this.checkDefFee,
			});
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFeeEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.textFee = new OpenDental.ValidDouble();
			this.checkDefFee = new System.Windows.Forms.CheckBox();
			this.checkDefCov = new System.Windows.Forms.CheckBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(2,26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72,16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Fee";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFee
			// 
			this.textFee.Location = new System.Drawing.Point(78,22);
			this.textFee.Name = "textFee";
			this.textFee.Size = new System.Drawing.Size(72,20);
			this.textFee.TabIndex = 0;
			// 
			// checkDefFee
			// 
			this.checkDefFee.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDefFee.Location = new System.Drawing.Point(54,54);
			this.checkDefFee.Name = "checkDefFee";
			this.checkDefFee.Size = new System.Drawing.Size(172,24);
			this.checkDefFee.TabIndex = 1;
			this.checkDefFee.Text = "Use Default Fee (not visible)";
			this.checkDefFee.Visible = false;
			// 
			// checkDefCov
			// 
			this.checkDefCov.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDefCov.Location = new System.Drawing.Point(54,84);
			this.checkDefCov.Name = "checkDefCov";
			this.checkDefCov.Size = new System.Drawing.Size(208,24);
			this.checkDefCov.TabIndex = 2;
			this.checkDefCov.Text = "Use Default Coverage (not visible)";
			this.checkDefCov.Visible = false;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(16,132);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(512,208);
			this.textBox1.TabIndex = 29;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			this.textBox1.Visible = false;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(282,54);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(282,92);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormFeeEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(374,132);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.textFee);
			this.Controls.Add(this.checkDefCov);
			this.Controls.Add(this.checkDefFee);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFeeEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Fee";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormFeeEdit_Closing);
			this.Load += new System.EventHandler(this.FormFeeEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormFeeEdit_Load(object sender, System.EventArgs e) {
			Location=new Point(Location.X-190,Location.Y-20);
			textFee.Text=FeeCur.Amount.ToString("F");
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textFee.errorProvider1.GetError(textFee)!=""){
				MessageBox.Show(Lan.g(this,"Please fix data entry error first."));
				return;
			}
			if(textFee.Text==""){
				Fees.Delete(FeeCur);
			}
			else{
				FeeCur.Amount=PIn.PDouble(textFee.Text);
				//Fee object always created and inserted externally first
				Fees.Update(FeeCur);
			}
			//FeeCur.UseDefaultCov=checkDefCov.Checked;
			//FeeCur.UseDefaultFee=checkDefFee.Checked;			
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormFeeEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK){
				return;
			}
			if(IsNew){
				Fees.Delete(FeeCur);
			}
		}

	}
}
