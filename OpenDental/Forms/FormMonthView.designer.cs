namespace OpenDental{
	partial class FormMonthView {
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
			this.butClose = new OpenDental.UI.Button();
			this.odGrid1 = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(844,585);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// odGrid1
			// 
			this.odGrid1.HScrollVisible = false;
			this.odGrid1.Location = new System.Drawing.Point(12,12);
			this.odGrid1.Name = "odGrid1";
			this.odGrid1.ScrollValue = 0;
			this.odGrid1.Size = new System.Drawing.Size(907,564);
			this.odGrid1.TabIndex = 3;
			this.odGrid1.Title = null;
			this.odGrid1.TranslationName = null;
			// 
			// FormMonthView
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(931,618);
			this.Controls.Add(this.odGrid1);
			this.Controls.Add(this.butClose);
			this.Name = "FormMonthView";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Month View";
			this.Load += new System.EventHandler(this.FormMonthView_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private OpenDental.UI.ODGrid odGrid1;
	}
}