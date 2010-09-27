namespace OpenDental{
	partial class FormProcEditDPCExplain {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcEditDPCExplain));
			this.label1 = new System.Windows.Forms.Label();
			this.radioButtonError = new System.Windows.Forms.RadioButton();
			this.radioButtonReAssign = new System.Windows.Forms.RadioButton();
			this.radioButtonNewProv = new System.Windows.Forms.RadioButton();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(159,16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Reason for DPC change:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// radioButtonError
			// 
			this.radioButtonError.AutoSize = true;
			this.radioButtonError.Location = new System.Drawing.Point(39,47);
			this.radioButtonError.Name = "radioButtonError";
			this.radioButtonError.Size = new System.Drawing.Size(73,17);
			this.radioButtonError.TabIndex = 5;
			this.radioButtonError.Text = "Entry error";
			this.radioButtonError.UseVisualStyleBackColor = true;
			this.radioButtonError.CheckedChanged += new System.EventHandler(this.radioButtonError_CheckedChanged);
			// 
			// radioButtonReAssign
			// 
			this.radioButtonReAssign.AutoSize = true;
			this.radioButtonReAssign.Location = new System.Drawing.Point(39,71);
			this.radioButtonReAssign.Name = "radioButtonReAssign";
			this.radioButtonReAssign.Size = new System.Drawing.Size(95,17);
			this.radioButtonReAssign.TabIndex = 6;
			this.radioButtonReAssign.Text = "Re-assignment";
			this.radioButtonReAssign.UseVisualStyleBackColor = true;
			this.radioButtonReAssign.CheckedChanged += new System.EventHandler(this.radioButtonReAssign_CheckedChanged);
			// 
			// radioButtonNewProv
			// 
			this.radioButtonNewProv.AutoSize = true;
			this.radioButtonNewProv.Location = new System.Drawing.Point(39,95);
			this.radioButtonNewProv.Name = "radioButtonNewProv";
			this.radioButtonNewProv.Size = new System.Drawing.Size(88,17);
			this.radioButtonNewProv.TabIndex = 7;
			this.radioButtonNewProv.Text = "New provider";
			this.radioButtonNewProv.UseVisualStyleBackColor = true;
			this.radioButtonNewProv.CheckedChanged += new System.EventHandler(this.radioButtonNewProv_CheckedChanged);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Enabled = false;
			this.butOK.Location = new System.Drawing.Point(176,140);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
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
			this.butCancel.Location = new System.Drawing.Point(176,181);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormProcEditDPCExplain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(276,232);
			this.Controls.Add(this.radioButtonNewProv);
			this.Controls.Add(this.radioButtonReAssign);
			this.Controls.Add(this.radioButtonError);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormProcEditDPCExplain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DPC Edit Explanation";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radioButtonError;
		private System.Windows.Forms.RadioButton radioButtonReAssign;
		private System.Windows.Forms.RadioButton radioButtonNewProv;
	}
}