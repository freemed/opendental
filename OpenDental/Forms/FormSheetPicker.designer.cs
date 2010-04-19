namespace OpenDental{
	partial class FormSheetPicker {
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
			this.label2 = new System.Windows.Forms.Label();
			this.labelSheetType = new System.Windows.Forms.Label();
			this.listMain = new System.Windows.Forms.ListBox();
			this.butTerminal = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.labelTerminal = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(29,9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(167,40);
			this.label2.TabIndex = 15;
			this.label2.Text = "Sheets can be added and edited from Setup Sheets.";
			// 
			// labelSheetType
			// 
			this.labelSheetType.Location = new System.Drawing.Point(29,62);
			this.labelSheetType.Name = "labelSheetType";
			this.labelSheetType.Size = new System.Drawing.Size(239,14);
			this.labelSheetType.TabIndex = 14;
			this.labelSheetType.Text = "Patient Forms and Medical Histories";
			this.labelSheetType.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listMain
			// 
			this.listMain.Location = new System.Drawing.Point(30,81);
			this.listMain.Name = "listMain";
			this.listMain.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listMain.Size = new System.Drawing.Size(164,329);
			this.listMain.TabIndex = 13;
			this.listMain.DoubleClick += new System.EventHandler(this.listMain_DoubleClick);
			// 
			// butTerminal
			// 
			this.butTerminal.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTerminal.Autosize = true;
			this.butTerminal.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTerminal.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTerminal.CornerRadius = 4F;
			this.butTerminal.Location = new System.Drawing.Point(284,175);
			this.butTerminal.Name = "butTerminal";
			this.butTerminal.Size = new System.Drawing.Size(75,24);
			this.butTerminal.TabIndex = 16;
			this.butTerminal.Text = "To Kiosk";
			this.butTerminal.Click += new System.EventHandler(this.butTerminal_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(284,351);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(284,385);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// labelTerminal
			// 
			this.labelTerminal.Location = new System.Drawing.Point(282,205);
			this.labelTerminal.Name = "labelTerminal";
			this.labelTerminal.Size = new System.Drawing.Size(88,30);
			this.labelTerminal.TabIndex = 19;
			this.labelTerminal.Text = "Multiple sheets can be sent";
			// 
			// FormSheetPicker
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(394,436);
			this.Controls.Add(this.labelTerminal);
			this.Controls.Add(this.butTerminal);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelSheetType);
			this.Controls.Add(this.listMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butClose);
			this.Name = "FormSheetPicker";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Pick Sheet";
			this.Load += new System.EventHandler(this.FormSheetPicker_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelSheetType;
		private System.Windows.Forms.ListBox listMain;
		private OpenDental.UI.Button butTerminal;
		private System.Windows.Forms.Label labelTerminal;
	}
}