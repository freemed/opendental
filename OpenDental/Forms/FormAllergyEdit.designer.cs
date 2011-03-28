namespace OpenDental{
	partial class FormAllergyEdit {
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
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textReaction = new System.Windows.Forms.TextBox();
			this.labelReaction = new System.Windows.Forms.Label();
			this.labelAllergy = new System.Windows.Forms.Label();
			this.textAllergy = new System.Windows.Forms.TextBox();
			this.checkActive = new System.Windows.Forms.CheckBox();
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
			this.butOK.Location = new System.Drawing.Point(370,194);
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
			this.butCancel.Location = new System.Drawing.Point(370,235);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textReaction
			// 
			this.textReaction.Location = new System.Drawing.Point(96,49);
			this.textReaction.Multiline = true;
			this.textReaction.Name = "textReaction";
			this.textReaction.Size = new System.Drawing.Size(276,121);
			this.textReaction.TabIndex = 4;
			// 
			// labelReaction
			// 
			this.labelReaction.Location = new System.Drawing.Point(5,49);
			this.labelReaction.Name = "labelReaction";
			this.labelReaction.Size = new System.Drawing.Size(85,20);
			this.labelReaction.TabIndex = 5;
			this.labelReaction.Text = "Reaction";
			this.labelReaction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelAllergy
			// 
			this.labelAllergy.Location = new System.Drawing.Point(5,15);
			this.labelAllergy.Name = "labelAllergy";
			this.labelAllergy.Size = new System.Drawing.Size(85,20);
			this.labelAllergy.TabIndex = 6;
			this.labelAllergy.Text = "Allergy";
			this.labelAllergy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAllergy
			// 
			this.textAllergy.Location = new System.Drawing.Point(96,18);
			this.textAllergy.Name = "textAllergy";
			this.textAllergy.ReadOnly = true;
			this.textAllergy.Size = new System.Drawing.Size(276,20);
			this.textAllergy.TabIndex = 7;
			// 
			// checkActive
			// 
			this.checkActive.Location = new System.Drawing.Point(8,176);
			this.checkActive.Name = "checkActive";
			this.checkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkActive.Size = new System.Drawing.Size(104,24);
			this.checkActive.TabIndex = 8;
			this.checkActive.Text = "Is Active";
			this.checkActive.UseVisualStyleBackColor = true;
			// 
			// FormAllergyEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(467,275);
			this.Controls.Add(this.checkActive);
			this.Controls.Add(this.textAllergy);
			this.Controls.Add(this.labelAllergy);
			this.Controls.Add(this.labelReaction);
			this.Controls.Add(this.textReaction);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormAllergyEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Allergy Edit";
			this.Load += new System.EventHandler(this.FormAllergyEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textReaction;
		private System.Windows.Forms.Label labelReaction;
		private System.Windows.Forms.Label labelAllergy;
		private System.Windows.Forms.TextBox textAllergy;
		private System.Windows.Forms.CheckBox checkActive;
	}
}