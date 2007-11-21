namespace OpenDental{
	partial class FormSupplies {
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
			this.label3 = new System.Windows.Forms.Label();
			this.comboSupplier = new System.Windows.Forms.ComboBox();
			this.checkShowHidden = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textFind = new System.Windows.Forms.TextBox();
			this.butRefresh = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(521,17);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67,18);
			this.label3.TabIndex = 14;
			this.label3.Text = "Supplier";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboSupplier
			// 
			this.comboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSupplier.FormattingEnabled = true;
			this.comboSupplier.Location = new System.Drawing.Point(589,16);
			this.comboSupplier.Name = "comboSupplier";
			this.comboSupplier.Size = new System.Drawing.Size(173,21);
			this.comboSupplier.TabIndex = 13;
			this.comboSupplier.SelectionChangeCommitted += new System.EventHandler(this.comboSupplier_SelectionChangeCommitted);
			// 
			// checkShowHidden
			// 
			this.checkShowHidden.Location = new System.Drawing.Point(89,19);
			this.checkShowHidden.Name = "checkShowHidden";
			this.checkShowHidden.Size = new System.Drawing.Size(95,18);
			this.checkShowHidden.TabIndex = 12;
			this.checkShowHidden.Text = "Show Hidden";
			this.checkShowHidden.UseVisualStyleBackColor = true;
			this.checkShowHidden.Click += new System.EventHandler(this.checkShowHidden_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butUp);
			this.groupBox1.Controls.Add(this.butDown);
			this.groupBox1.Location = new System.Drawing.Point(187,0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(75,41);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Move";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(268,17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53,18);
			this.label1.TabIndex = 18;
			this.label1.Text = "Find";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textFind
			// 
			this.textFind.Location = new System.Drawing.Point(322,17);
			this.textFind.Name = "textFind";
			this.textFind.Size = new System.Drawing.Size(67,20);
			this.textFind.TabIndex = 19;
			this.textFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textFind_KeyDown);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butRefresh.Location = new System.Drawing.Point(395,15);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(64,24);
			this.butRefresh.TabIndex = 20;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.Location = new System.Drawing.Point(10,14);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(24,24);
			this.butUp.TabIndex = 15;
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.Location = new System.Drawing.Point(40,14);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(24,24);
			this.butDown.TabIndex = 16;
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(12,13);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(69,24);
			this.butAdd.TabIndex = 11;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,43);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(750,501);
			this.gridMain.TabIndex = 5;
			this.gridMain.Title = "Supplies";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(687,550);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormSupplies
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(771,588);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.textFind);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboSupplier);
			this.Controls.Add(this.checkShowHidden);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Name = "FormSupplies";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Supplies";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormSupplies_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private OpenDental.UI.ODGrid gridMain;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboSupplier;
		private System.Windows.Forms.CheckBox checkShowHidden;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butUp;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textFind;
		private OpenDental.UI.Button butRefresh;
	}
}