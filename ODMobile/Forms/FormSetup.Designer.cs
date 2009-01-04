namespace OpenDentMobile {
	partial class FormSetup {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MainMenu mainMenu1;

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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.label1 = new System.Windows.Forms.Label();
			this.textPath = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3,11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,18);
			this.label1.Text = "Import Path";
			// 
			// textPath
			// 
			this.textPath.Location = new System.Drawing.Point(3,29);
			this.textPath.Name = "textPath";
			this.textPath.Size = new System.Drawing.Size(234,21);
			this.textPath.TabIndex = 1;
			// 
			// FormSetup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F,96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240,268);
			this.Controls.Add(this.textPath);
			this.Controls.Add(this.label1);
			this.Menu = this.mainMenu1;
			this.Name = "FormSetup";
			this.Text = "FormSetup";
			this.Load += new System.EventHandler(this.FormSetup_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormSetup_Closing);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textPath;
	}
}