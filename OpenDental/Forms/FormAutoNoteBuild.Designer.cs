namespace OpenDental {
	partial class FormAutoNoteBuild {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoNoteBuild));
			this.listBoxAutoNotes = new System.Windows.Forms.ListBox();
			this.listBoxControls = new System.Windows.Forms.ListBox();
			this.butOK = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// listBoxAutoNotes
			// 
			this.listBoxAutoNotes.FormattingEnabled = true;
			this.listBoxAutoNotes.HorizontalScrollbar = true;
			this.listBoxAutoNotes.Location = new System.Drawing.Point(108, 12);
			this.listBoxAutoNotes.Name = "listBoxAutoNotes";
			this.listBoxAutoNotes.Size = new System.Drawing.Size(197, 108);
			this.listBoxAutoNotes.TabIndex = 0;
			this.listBoxAutoNotes.SelectedIndexChanged += new System.EventHandler(this.listBoxAutoNotes_SelectedIndexChanged);
			// 
			// listBoxControls
			// 
			this.listBoxControls.FormattingEnabled = true;
			this.listBoxControls.Location = new System.Drawing.Point(331, 25);
			this.listBoxControls.Name = "listBoxControls";
			this.listBoxControls.Size = new System.Drawing.Size(120, 95);
			this.listBoxControls.TabIndex = 10;
			this.listBoxControls.Visible = false;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(26, 24);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76, 25);
			this.butOK.TabIndex = 9;
			this.butOK.Text = "OK";
			this.butOK.Visible = false;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormAutoNoteBuild
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoScrollMargin = new System.Drawing.Size(0, 50);
			this.ClientSize = new System.Drawing.Size(490, 633);
			this.Controls.Add(this.listBoxControls);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.listBoxAutoNotes);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAutoNoteBuild";
			this.Text = " ";
			this.Load += new System.EventHandler(this.FormAutoNoteBuild_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxAutoNotes;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.ListBox listBoxControls;
	}
}