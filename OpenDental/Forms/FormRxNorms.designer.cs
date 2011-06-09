namespace OpenDental{
	partial class FormRxNorms {
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
			this.butRxNorm = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.button1 = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butSearch = new OpenDental.UI.Button();
			this.labelCodeOrDesc = new System.Windows.Forms.Label();
			this.textCode = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butRxNorm
			// 
			this.butRxNorm.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRxNorm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butRxNorm.Autosize = true;
			this.butRxNorm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRxNorm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRxNorm.CornerRadius = 4F;
			this.butRxNorm.Location = new System.Drawing.Point(139,651);
			this.butRxNorm.Name = "butRxNorm";
			this.butRxNorm.Size = new System.Drawing.Size(98,24);
			this.butRxNorm.TabIndex = 5;
			this.butRxNorm.Text = "Create rxnorm";
			this.butRxNorm.Visible = false;
			this.butRxNorm.Click += new System.EventHandler(this.butRxNorm_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(26,34);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(642,599);
			this.gridMain.TabIndex = 4;
			this.gridMain.Title = "RxNorm Codes";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// button1
			// 
			this.button1.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Autosize = true;
			this.button1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button1.CornerRadius = 4F;
			this.button1.Location = new System.Drawing.Point(26,651);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75,24);
			this.button1.TabIndex = 3;
			this.button1.Text = "&None";
			this.button1.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(512,651);
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
			this.butCancel.Location = new System.Drawing.Point(593,651);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butSearch
			// 
			this.butSearch.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSearch.Autosize = true;
			this.butSearch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearch.CornerRadius = 4F;
			this.butSearch.Location = new System.Drawing.Point(284,5);
			this.butSearch.Name = "butSearch";
			this.butSearch.Size = new System.Drawing.Size(75,24);
			this.butSearch.TabIndex = 22;
			this.butSearch.Text = "Search";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
			// 
			// labelCodeOrDesc
			// 
			this.labelCodeOrDesc.Location = new System.Drawing.Point(3,10);
			this.labelCodeOrDesc.Name = "labelCodeOrDesc";
			this.labelCodeOrDesc.Size = new System.Drawing.Size(172,16);
			this.labelCodeOrDesc.TabIndex = 21;
			this.labelCodeOrDesc.Text = "Code or Description";
			this.labelCodeOrDesc.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCode
			// 
			this.textCode.Location = new System.Drawing.Point(178,7);
			this.textCode.Name = "textCode";
			this.textCode.Size = new System.Drawing.Size(100,20);
			this.textCode.TabIndex = 20;
			// 
			// FormRxNorms
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(693,702);
			this.Controls.Add(this.butSearch);
			this.Controls.Add(this.labelCodeOrDesc);
			this.Controls.Add(this.textCode);
			this.Controls.Add(this.butRxNorm);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormRxNorms";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RxNorms";
			this.Load += new System.EventHandler(this.FormRxNorms_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private UI.Button button1;
		private UI.ODGrid gridMain;
		private UI.Button butRxNorm;
		private UI.Button butSearch;
		private System.Windows.Forms.Label labelCodeOrDesc;
		private System.Windows.Forms.TextBox textCode;
	}
}