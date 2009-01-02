namespace OpenDentMobile {
	partial class ContrFamily {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.gridPat = new OpenDentMobile.UI.ODGrid();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridPat
			// 
			this.gridPat.Location = new System.Drawing.Point(0,0);
			this.gridPat.Name = "gridPat";
			this.gridPat.Size = new System.Drawing.Size(237,265);
			this.gridPat.TabIndex = 0;
			this.gridPat.Text = "odGrid1";
			this.gridPat.WrapText = true;
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.gridPat);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0,0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(240,268);
			// 
			// ContrFamily
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F,96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.panel1);
			this.Name = "ContrFamily";
			this.Size = new System.Drawing.Size(240,268);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDentMobile.UI.ODGrid gridPat;
		private System.Windows.Forms.Panel panel1;

	}
}
