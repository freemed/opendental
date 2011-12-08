namespace CentralManager {
	partial class FormCentralManager {
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
			this.butConSetup = new System.Windows.Forms.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// butConSetup
			// 
			this.butConSetup.Location = new System.Drawing.Point(12,12);
			this.butConSetup.Name = "butConSetup";
			this.butConSetup.Size = new System.Drawing.Size(117,24);
			this.butConSetup.TabIndex = 0;
			this.butConSetup.Text = "Setup Connections";
			this.butConSetup.UseVisualStyleBackColor = true;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(11,60);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(613,410);
			this.gridMain.TabIndex = 5;
			this.gridMain.Title = "Connections - click to launch";
			this.gridMain.TranslationName = "";
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			// 
			// FormCentralManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(637,482);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butConSetup);
			this.Name = "FormCentralManager";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Central Manager";
			this.Load += new System.EventHandler(this.FormCentralManager_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button butConSetup;
		private OpenDental.UI.ODGrid gridMain;
	}
}

