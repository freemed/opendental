namespace OpenDental{
	partial class FormEhrSetup {
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
			this.butICD9s = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.butAllergies = new OpenDental.UI.Button();
			this.butFormularies = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butICD9s
			// 
			this.butICD9s.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butICD9s.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butICD9s.Autosize = true;
			this.butICD9s.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butICD9s.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butICD9s.CornerRadius = 4F;
			this.butICD9s.Location = new System.Drawing.Point(12,61);
			this.butICD9s.Name = "butICD9s";
			this.butICD9s.Size = new System.Drawing.Size(175,24);
			this.butICD9s.TabIndex = 119;
			this.butICD9s.Text = "ICD9s";
			this.butICD9s.Click += new System.EventHandler(this.butICD9s_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(625,483);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butAllergies
			// 
			this.butAllergies.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAllergies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAllergies.Autosize = true;
			this.butAllergies.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAllergies.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAllergies.CornerRadius = 4F;
			this.butAllergies.Location = new System.Drawing.Point(12,103);
			this.butAllergies.Name = "butAllergies";
			this.butAllergies.Size = new System.Drawing.Size(175,24);
			this.butAllergies.TabIndex = 120;
			this.butAllergies.Text = "Allergies";
			this.butAllergies.Click += new System.EventHandler(this.butAllergies_Click);
			// 
			// butFormularies
			// 
			this.butFormularies.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFormularies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butFormularies.Autosize = true;
			this.butFormularies.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFormularies.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFormularies.CornerRadius = 4F;
			this.butFormularies.Location = new System.Drawing.Point(12,145);
			this.butFormularies.Name = "butFormularies";
			this.butFormularies.Size = new System.Drawing.Size(175,24);
			this.butFormularies.TabIndex = 121;
			this.butFormularies.Text = "Formularies";
			this.butFormularies.Click += new System.EventHandler(this.butFormularies_Click);
			// 
			// FormEhrSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(725,534);
			this.Controls.Add(this.butFormularies);
			this.Controls.Add(this.butAllergies);
			this.Controls.Add(this.butICD9s);
			this.Controls.Add(this.butClose);
			this.Name = "FormEhrSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Electronic Health Record (EHR) Setup";
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private UI.Button butICD9s;
		private UI.Button butAllergies;
		private UI.Button butFormularies;
	}
}