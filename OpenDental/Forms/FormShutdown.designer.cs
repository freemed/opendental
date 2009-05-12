namespace OpenDental{
	partial class FormShutdown {
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
			this.butCancel = new OpenDental.UI.Button();
			this.listMain = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butShutdown = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(326,490);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// listMain
			// 
			this.listMain.FormattingEnabled = true;
			this.listMain.Location = new System.Drawing.Point(12,29);
			this.listMain.Name = "listMain";
			this.listMain.Size = new System.Drawing.Size(278,485);
			this.listMain.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11,10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(214,16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Workstations that might be running";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butShutdown
			// 
			this.butShutdown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShutdown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butShutdown.Autosize = true;
			this.butShutdown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShutdown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShutdown.CornerRadius = 4F;
			this.butShutdown.Location = new System.Drawing.Point(326,450);
			this.butShutdown.Name = "butShutdown";
			this.butShutdown.Size = new System.Drawing.Size(75,24);
			this.butShutdown.TabIndex = 6;
			this.butShutdown.Text = "Shutdown";
			this.butShutdown.Click += new System.EventHandler(this.butShutdown_Click);
			// 
			// FormShutdown
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(422,534);
			this.Controls.Add(this.butShutdown);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listMain);
			this.Controls.Add(this.butCancel);
			this.Name = "FormShutdown";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Shutdown Workstations";
			this.Load += new System.EventHandler(this.FormShutdown_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.ListBox listMain;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butShutdown;
	}
}