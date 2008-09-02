namespace OpenDental{
	partial class FormRecallsPat {
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
			this.butAdd = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.butPerio = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(12,340);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,24);
			this.butAdd.TabIndex = 34;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridMain.Size = new System.Drawing.Size(721,304);
			this.gridMain.TabIndex = 33;
			this.gridMain.Title = "Recalls";
			this.gridMain.TranslationName = "TableRecallsPat";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(658,340);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butPerio
			// 
			this.butPerio.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPerio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPerio.Autosize = true;
			this.butPerio.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPerio.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPerio.CornerRadius = 4F;
			this.butPerio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPerio.Location = new System.Drawing.Point(267,340);
			this.butPerio.Name = "butPerio";
			this.butPerio.Size = new System.Drawing.Size(83,24);
			this.butPerio.TabIndex = 35;
			this.butPerio.Text = "Set Perio";
			this.butPerio.Click += new System.EventHandler(this.butPerio_Click);
			// 
			// FormRecallsPat
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(758,391);
			this.Controls.Add(this.butPerio);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Name = "FormRecallsPat";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Recalls for Patient";
			this.Load += new System.EventHandler(this.FormRecallsPat_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private OpenDental.UI.ODGrid gridMain;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butPerio;
	}
}