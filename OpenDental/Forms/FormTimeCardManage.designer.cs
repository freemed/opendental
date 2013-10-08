namespace OpenDental{
	partial class FormTimeCardManage {
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textDatePaycheck = new System.Windows.Forms.TextBox();
			this.textDateStop = new System.Windows.Forms.TextBox();
			this.textDateStart = new System.Windows.Forms.TextBox();
			this.butRight = new OpenDental.UI.Button();
			this.butLeft = new OpenDental.UI.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butDaily = new OpenDental.UI.Button();
			this.butCompute = new OpenDental.UI.Button();
			this.butPrintAll = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.butPrintGrid = new OpenDental.UI.Button();
			this.butExportADP = new OpenDental.UI.Button();
			this.butClearAuto = new OpenDental.UI.Button();
			this.butClearManual = new OpenDental.UI.Button();
			this.butPrintSelected = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.butExportGrid = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textDatePaycheck);
			this.groupBox1.Controls.Add(this.textDateStop);
			this.groupBox1.Controls.Add(this.textDateStart);
			this.groupBox1.Controls.Add(this.butRight);
			this.groupBox1.Controls.Add(this.butLeft);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(12, 9);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(659, 51);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Pay Period";
			// 
			// textDatePaycheck
			// 
			this.textDatePaycheck.Location = new System.Drawing.Point(473, 19);
			this.textDatePaycheck.Name = "textDatePaycheck";
			this.textDatePaycheck.ReadOnly = true;
			this.textDatePaycheck.Size = new System.Drawing.Size(100, 20);
			this.textDatePaycheck.TabIndex = 14;
			// 
			// textDateStop
			// 
			this.textDateStop.Location = new System.Drawing.Point(244, 28);
			this.textDateStop.Name = "textDateStop";
			this.textDateStop.ReadOnly = true;
			this.textDateStop.Size = new System.Drawing.Size(100, 20);
			this.textDateStop.TabIndex = 13;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(244, 8);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.ReadOnly = true;
			this.textDateStart.Size = new System.Drawing.Size(100, 20);
			this.textDateStart.TabIndex = 12;
			// 
			// butRight
			// 
			this.butRight.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRight.Autosize = true;
			this.butRight.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRight.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRight.CornerRadius = 4F;
			this.butRight.Image = global::OpenDental.Properties.Resources.Right;
			this.butRight.Location = new System.Drawing.Point(63, 18);
			this.butRight.Name = "butRight";
			this.butRight.Size = new System.Drawing.Size(39, 24);
			this.butRight.TabIndex = 11;
			this.butRight.Click += new System.EventHandler(this.butRight_Click);
			// 
			// butLeft
			// 
			this.butLeft.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butLeft.Autosize = true;
			this.butLeft.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLeft.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLeft.CornerRadius = 4F;
			this.butLeft.Image = global::OpenDental.Properties.Resources.Left;
			this.butLeft.Location = new System.Drawing.Point(13, 18);
			this.butLeft.Name = "butLeft";
			this.butLeft.Size = new System.Drawing.Size(39, 24);
			this.butLeft.TabIndex = 10;
			this.butLeft.Click += new System.EventHandler(this.butLeft_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(354, 19);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(117, 18);
			this.label4.TabIndex = 9;
			this.label4.Text = "Paycheck Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(146, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 18);
			this.label2.TabIndex = 6;
			this.label2.Text = "Start Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(143, 28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99, 18);
			this.label3.TabIndex = 8;
			this.label3.Text = "End Date";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12, 66);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(931, 562);
			this.gridMain.TabIndex = 16;
			this.gridMain.Title = "Employee Time Cards";
			this.gridMain.TranslationName = "TableTimeCard";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butDaily
			// 
			this.butDaily.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDaily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDaily.Autosize = true;
			this.butDaily.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDaily.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDaily.CornerRadius = 4F;
			this.butDaily.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDaily.Location = new System.Drawing.Point(6, 18);
			this.butDaily.Name = "butDaily";
			this.butDaily.Size = new System.Drawing.Size(69, 24);
			this.butDaily.TabIndex = 119;
			this.butDaily.Text = "Daily";
			this.butDaily.Click += new System.EventHandler(this.butDaily_Click);
			// 
			// butCompute
			// 
			this.butCompute.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCompute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butCompute.Autosize = true;
			this.butCompute.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCompute.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCompute.CornerRadius = 4F;
			this.butCompute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCompute.Location = new System.Drawing.Point(81, 18);
			this.butCompute.Name = "butCompute";
			this.butCompute.Size = new System.Drawing.Size(72, 24);
			this.butCompute.TabIndex = 118;
			this.butCompute.Text = "Weekly";
			this.butCompute.Click += new System.EventHandler(this.butWeekly_Click);
			// 
			// butPrintAll
			// 
			this.butPrintAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrintAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrintAll.Autosize = true;
			this.butPrintAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrintAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrintAll.CornerRadius = 4F;
			this.butPrintAll.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrintAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrintAll.Location = new System.Drawing.Point(6, 18);
			this.butPrintAll.Name = "butPrintAll";
			this.butPrintAll.Size = new System.Drawing.Size(87, 24);
			this.butPrintAll.TabIndex = 116;
			this.butPrintAll.Text = "&Print All";
			this.butPrintAll.Click += new System.EventHandler(this.butPrintAll_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(868, 652);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butPrintGrid
			// 
			this.butPrintGrid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrintGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrintGrid.Autosize = true;
			this.butPrintGrid.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrintGrid.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrintGrid.CornerRadius = 4F;
			this.butPrintGrid.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrintGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrintGrid.Location = new System.Drawing.Point(6, 18);
			this.butPrintGrid.Name = "butPrintGrid";
			this.butPrintGrid.Size = new System.Drawing.Size(96, 24);
			this.butPrintGrid.TabIndex = 120;
			this.butPrintGrid.Text = "Print Grid";
			this.butPrintGrid.Click += new System.EventHandler(this.butPrintGrid_Click);
			// 
			// butExportADP
			// 
			this.butExportADP.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butExportADP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butExportADP.Autosize = true;
			this.butExportADP.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butExportADP.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butExportADP.CornerRadius = 4F;
			this.butExportADP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butExportADP.Location = new System.Drawing.Point(196, 18);
			this.butExportADP.Name = "butExportADP";
			this.butExportADP.Size = new System.Drawing.Size(79, 24);
			this.butExportADP.TabIndex = 121;
			this.butExportADP.Text = "Export ADP";
			this.butExportADP.Click += new System.EventHandler(this.butExportADP_Click);
			// 
			// butClearAuto
			// 
			this.butClearAuto.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClearAuto.Autosize = true;
			this.butClearAuto.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClearAuto.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClearAuto.CornerRadius = 4F;
			this.butClearAuto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClearAuto.Location = new System.Drawing.Point(677, 36);
			this.butClearAuto.Name = "butClearAuto";
			this.butClearAuto.Size = new System.Drawing.Size(117, 24);
			this.butClearAuto.TabIndex = 122;
			this.butClearAuto.Text = "Clear Auto Adjusts";
			this.butClearAuto.Click += new System.EventHandler(this.butClearAuto_Click);
			// 
			// butClearManual
			// 
			this.butClearManual.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClearManual.Autosize = true;
			this.butClearManual.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClearManual.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClearManual.CornerRadius = 4F;
			this.butClearManual.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClearManual.Location = new System.Drawing.Point(677, 9);
			this.butClearManual.Name = "butClearManual";
			this.butClearManual.Size = new System.Drawing.Size(117, 24);
			this.butClearManual.TabIndex = 123;
			this.butClearManual.Text = "Clear Manual Adjusts";
			this.butClearManual.Click += new System.EventHandler(this.butClearManual_Click);
			// 
			// butPrintSelected
			// 
			this.butPrintSelected.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrintSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrintSelected.Autosize = true;
			this.butPrintSelected.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrintSelected.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrintSelected.CornerRadius = 4F;
			this.butPrintSelected.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrintSelected.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrintSelected.Location = new System.Drawing.Point(99, 18);
			this.butPrintSelected.Name = "butPrintSelected";
			this.butPrintSelected.Size = new System.Drawing.Size(109, 24);
			this.butPrintSelected.TabIndex = 124;
			this.butPrintSelected.Text = "Print Selected";
			this.butPrintSelected.Click += new System.EventHandler(this.butPrintSelected_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.butDaily);
			this.groupBox2.Controls.Add(this.butCompute);
			this.groupBox2.Location = new System.Drawing.Point(12, 634);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(160, 48);
			this.groupBox2.TabIndex = 16;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Calculations";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox3.Controls.Add(this.butPrintAll);
			this.groupBox3.Controls.Add(this.butPrintSelected);
			this.groupBox3.Location = new System.Drawing.Point(178, 634);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(215, 48);
			this.groupBox3.TabIndex = 125;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Time Cards";
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox4.Controls.Add(this.butExportGrid);
			this.groupBox4.Controls.Add(this.butPrintGrid);
			this.groupBox4.Controls.Add(this.butExportADP);
			this.groupBox4.Location = new System.Drawing.Point(497, 634);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(282, 48);
			this.groupBox4.TabIndex = 126;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Payroll Reports";
			// 
			// butExportGrid
			// 
			this.butExportGrid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butExportGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butExportGrid.Autosize = true;
			this.butExportGrid.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butExportGrid.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butExportGrid.CornerRadius = 4F;
			this.butExportGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butExportGrid.Location = new System.Drawing.Point(108, 18);
			this.butExportGrid.Name = "butExportGrid";
			this.butExportGrid.Size = new System.Drawing.Size(82, 24);
			this.butExportGrid.TabIndex = 127;
			this.butExportGrid.Text = "Export Grid";
			this.butExportGrid.Click += new System.EventHandler(this.butExportGrid_Click);
			// 
			// FormTimeCardManage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(955, 692);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butClearManual);
			this.Controls.Add(this.butClearAuto);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butClose);
			this.Name = "FormTimeCardManage";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Time Card Manage";
			this.Load += new System.EventHandler(this.FormTimeCardManage_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textDatePaycheck;
		private System.Windows.Forms.TextBox textDateStop;
		private System.Windows.Forms.TextBox textDateStart;
		private UI.Button butRight;
		private UI.Button butLeft;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private UI.ODGrid gridMain;
		private UI.Button butPrintAll;
		private UI.Button butDaily;
		private UI.Button butCompute;
		private UI.Button butPrintGrid;
		private UI.Button butExportADP;
		private UI.Button butClearAuto;
		private UI.Button butClearManual;
		private UI.Button butPrintSelected;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private UI.Button butExportGrid;
	}
}