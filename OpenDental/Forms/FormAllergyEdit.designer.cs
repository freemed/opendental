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
			this.butDelete = new OpenDental.UI.Button();
			this.checkActive = new System.Windows.Forms.CheckBox();
			this.textReaction = new System.Windows.Forms.TextBox();
			this.labelReaction = new System.Windows.Forms.Label();
			this.labelAllergy = new System.Windows.Forms.Label();
			this.comboAllergies = new System.Windows.Forms.ComboBox();
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
			this.butOK.Location = new System.Drawing.Point(326,226);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Location = new System.Drawing.Point(19,226);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,24);
			this.butDelete.TabIndex = 2;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// checkActive
			// 
			this.checkActive.Location = new System.Drawing.Point(12,157);
			this.checkActive.Name = "checkActive";
			this.checkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkActive.Size = new System.Drawing.Size(100,24);
			this.checkActive.TabIndex = 11;
			this.checkActive.Text = "Is Active";
			this.checkActive.UseVisualStyleBackColor = true;
			// 
			// textReaction
			// 
			this.textReaction.Location = new System.Drawing.Point(96,58);
			this.textReaction.Multiline = true;
			this.textReaction.Name = "textReaction";
			this.textReaction.Size = new System.Drawing.Size(272,93);
			this.textReaction.TabIndex = 10;
			// 
			// labelReaction
			// 
			this.labelReaction.Location = new System.Drawing.Point(5,58);
			this.labelReaction.Name = "labelReaction";
			this.labelReaction.Size = new System.Drawing.Size(81,20);
			this.labelReaction.TabIndex = 9;
			this.labelReaction.Text = "Reaction";
			this.labelReaction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelAllergy
			// 
			this.labelAllergy.Location = new System.Drawing.Point(5,31);
			this.labelAllergy.Name = "labelAllergy";
			this.labelAllergy.Size = new System.Drawing.Size(81,20);
			this.labelAllergy.TabIndex = 12;
			this.labelAllergy.Text = "Allergy";
			this.labelAllergy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboAllergies
			// 
			this.comboAllergies.FormattingEnabled = true;
			this.comboAllergies.Location = new System.Drawing.Point(96,32);
			this.comboAllergies.Name = "comboAllergies";
			this.comboAllergies.Size = new System.Drawing.Size(272,21);
			this.comboAllergies.TabIndex = 13;
			// 
			// FormAllergyEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(413,262);
			this.Controls.Add(this.comboAllergies);
			this.Controls.Add(this.labelAllergy);
			this.Controls.Add(this.checkActive);
			this.Controls.Add(this.textReaction);
			this.Controls.Add(this.labelReaction);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butDelete);
			this.Name = "FormAllergyEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Allergy Edit";
			this.Load += new System.EventHandler(this.FormAllergyEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.CheckBox checkActive;
		private System.Windows.Forms.TextBox textReaction;
		private System.Windows.Forms.Label labelReaction;
		private System.Windows.Forms.Label labelAllergy;
		private System.Windows.Forms.ComboBox comboAllergies;
	}
}