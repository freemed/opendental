namespace OpenDental{
	partial class FormChartViewDateFilter {
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.listPresetDateRanges = new System.Windows.Forms.ListBox();
			this.textDateStop = new OpenDental.ValidDate();
			this.textDateStart = new OpenDental.ValidDate();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(19,142);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84,16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Start Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(19,168);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84,16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Stop Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listPresetDateRanges
			// 
			this.listPresetDateRanges.FormattingEnabled = true;
			this.listPresetDateRanges.Location = new System.Drawing.Point(105,12);
			this.listPresetDateRanges.Name = "listPresetDateRanges";
			this.listPresetDateRanges.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listPresetDateRanges.Size = new System.Drawing.Size(91,121);
			this.listPresetDateRanges.TabIndex = 8;
			this.listPresetDateRanges.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listPresetDateRanges_MouseClick);
			// 
			// textDateStop
			// 
			this.textDateStop.Location = new System.Drawing.Point(105,167);
			this.textDateStop.Name = "textDateStop";
			this.textDateStop.Size = new System.Drawing.Size(91,20);
			this.textDateStop.TabIndex = 5;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(105,141);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(91,20);
			this.textDateStart.TabIndex = 4;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(142,224);
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
			this.butCancel.Location = new System.Drawing.Point(223,224);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormChartViewDateFilter
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(316,262);
			this.Controls.Add(this.listPresetDateRanges);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDateStop);
			this.Controls.Add(this.textDateStart);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormChartViewDateFilter";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Progress Notes Date Range Filter";
			this.Load += new System.EventHandler(this.FormChartViewDateFilter_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private ValidDate textDateStart;
		private ValidDate textDateStop;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listPresetDateRanges;
	}
}