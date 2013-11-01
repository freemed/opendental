namespace OpenDental {
	partial class FormMapSetup {
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
			this.components = new System.ComponentModel.Container();
			this.checkShowGrid = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.numFloorWidthFeet = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.numFloorHeightFeet = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.numPixelsPerFoot = new System.Windows.Forms.NumericUpDown();
			this.checkShowOutline = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.newCubicleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.gridEmployees = new OpenDental.UI.ODGrid();
			this.mapAreaPanel = new OpenDental.MapAreaPanel();
			this.butCancel = new OpenDental.UI.Button();
			this.butAddMapArea = new OpenDental.UI.Button();
			((System.ComponentModel.ISupportInitialize)(this.numFloorWidthFeet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFloorHeightFeet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPixelsPerFoot)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkShowGrid
			// 
			this.checkShowGrid.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowGrid.Location = new System.Drawing.Point(15, 93);
			this.checkShowGrid.Name = "checkShowGrid";
			this.checkShowGrid.Size = new System.Drawing.Size(225, 16);
			this.checkShowGrid.TabIndex = 3;
			this.checkShowGrid.Text = "Show Grid";
			this.checkShowGrid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowGrid.UseVisualStyleBackColor = true;
			this.checkShowGrid.CheckedChanged += new System.EventHandler(this.checkShowGrid_CheckedChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(11, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(209, 16);
			this.label2.TabIndex = 15;
			this.label2.Text = "Floor Width (in feet)";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numFloorWidthFeet
			// 
			this.numFloorWidthFeet.Location = new System.Drawing.Point(226, 20);
			this.numFloorWidthFeet.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numFloorWidthFeet.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numFloorWidthFeet.Name = "numFloorWidthFeet";
			this.numFloorWidthFeet.Size = new System.Drawing.Size(86, 20);
			this.numFloorWidthFeet.TabIndex = 14;
			this.numFloorWidthFeet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numFloorWidthFeet.Value = new decimal(new int[] {
            71,
            0,
            0,
            0});
			this.numFloorWidthFeet.ValueChanged += new System.EventHandler(this.numericFloorWidthFeet_ValueChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(11, 47);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(209, 16);
			this.label3.TabIndex = 17;
			this.label3.Text = "Floor Height (in feet)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numFloorHeightFeet
			// 
			this.numFloorHeightFeet.Location = new System.Drawing.Point(226, 45);
			this.numFloorHeightFeet.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numFloorHeightFeet.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numFloorHeightFeet.Name = "numFloorHeightFeet";
			this.numFloorHeightFeet.Size = new System.Drawing.Size(86, 20);
			this.numFloorHeightFeet.TabIndex = 16;
			this.numFloorHeightFeet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numFloorHeightFeet.Value = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.numFloorHeightFeet.ValueChanged += new System.EventHandler(this.numericFloorHeightFeet_ValueChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(11, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(209, 16);
			this.label4.TabIndex = 18;
			this.label4.Text = "Pixels Per Foot";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numPixelsPerFoot
			// 
			this.numPixelsPerFoot.Location = new System.Drawing.Point(226, 70);
			this.numPixelsPerFoot.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numPixelsPerFoot.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numPixelsPerFoot.Name = "numPixelsPerFoot";
			this.numPixelsPerFoot.Size = new System.Drawing.Size(86, 20);
			this.numPixelsPerFoot.TabIndex = 19;
			this.numPixelsPerFoot.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numPixelsPerFoot.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
			this.numPixelsPerFoot.ValueChanged += new System.EventHandler(this.numericPixelsPerFoot_ValueChanged);
			// 
			// checkShowOutline
			// 
			this.checkShowOutline.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowOutline.Checked = true;
			this.checkShowOutline.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowOutline.Location = new System.Drawing.Point(14, 113);
			this.checkShowOutline.Name = "checkShowOutline";
			this.checkShowOutline.Size = new System.Drawing.Size(225, 16);
			this.checkShowOutline.TabIndex = 21;
			this.checkShowOutline.Text = "Show Outline";
			this.checkShowOutline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowOutline.UseVisualStyleBackColor = true;
			this.checkShowOutline.CheckedChanged += new System.EventHandler(this.checkShowOutline_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.checkShowGrid);
			this.groupBox1.Controls.Add(this.checkShowOutline);
			this.groupBox1.Controls.Add(this.numFloorWidthFeet);
			this.groupBox1.Controls.Add(this.numFloorHeightFeet);
			this.groupBox1.Controls.Add(this.numPixelsPerFoot);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Location = new System.Drawing.Point(1230, 34);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(334, 138);
			this.groupBox1.TabIndex = 23;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Preview Different Options (will not persist after you close this form)";
			// 
			// menu
			// 
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCubicleToolStripMenuItem,
            this.newLabelToolStripMenuItem});
			this.menu.Name = "menu";
			this.menu.Size = new System.Drawing.Size(142, 48);
			// 
			// newCubicleToolStripMenuItem
			// 
			this.newCubicleToolStripMenuItem.Name = "newCubicleToolStripMenuItem";
			this.newCubicleToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			this.newCubicleToolStripMenuItem.Text = "New Cubicle";
			this.newCubicleToolStripMenuItem.Click += new System.EventHandler(this.newCubicleToolStripMenuItem_Click);
			// 
			// newLabelToolStripMenuItem
			// 
			this.newLabelToolStripMenuItem.Name = "newLabelToolStripMenuItem";
			this.newLabelToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			this.newLabelToolStripMenuItem.Text = "New Label";
			this.newLabelToolStripMenuItem.Click += new System.EventHandler(this.newLabelToolStripMenuItem_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(378, 13);
			this.label1.TabIndex = 26;
			this.label1.Text = "Double-click an item on the map to edit. Right-click the map to add a new item.";
			// 
			// gridEmployees
			// 
			this.gridEmployees.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridEmployees.HScrollVisible = false;
			this.gridEmployees.Location = new System.Drawing.Point(1230, 178);
			this.gridEmployees.Name = "gridEmployees";
			this.gridEmployees.ScrollValue = 0;
			this.gridEmployees.Size = new System.Drawing.Size(334, 729);
			this.gridEmployees.TabIndex = 25;
			this.gridEmployees.Title = "Employees";
			this.gridEmployees.TranslationName = "TableEmployees";
			// 
			// mapAreaPanel
			// 
			this.mapAreaPanel.AllowDragging = true;
			this.mapAreaPanel.AllowEditing = true;
			this.mapAreaPanel.AutoScroll = true;
			this.mapAreaPanel.AutoScrollMinSize = new System.Drawing.Size(1207, 1003);
			this.mapAreaPanel.FloorColor = System.Drawing.Color.White;
			this.mapAreaPanel.FloorHeightFeet = 59;
			this.mapAreaPanel.FloorWidthFeet = 71;
			this.mapAreaPanel.Font = new System.Drawing.Font("Calibri", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mapAreaPanel.FontCubicle = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mapAreaPanel.FontCubicleHeader = new System.Drawing.Font("Calibri", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mapAreaPanel.FontLabel = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mapAreaPanel.GridColor = System.Drawing.Color.LightGray;
			this.mapAreaPanel.Location = new System.Drawing.Point(12, 34);
			this.mapAreaPanel.Name = "mapAreaPanel";
			this.mapAreaPanel.PixelsPerFoot = 17;
			this.mapAreaPanel.ShowGrid = false;
			this.mapAreaPanel.ShowOutline = true;
			this.mapAreaPanel.Size = new System.Drawing.Size(1212, 1008);
			this.mapAreaPanel.TabIndex = 4;
			this.mapAreaPanel.MapAreaChanged += new System.EventHandler(this.mapAreaPanel_MapAreaChanged);
			this.mapAreaPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapAreaPanel_MouseUp);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(1489, 1016);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 28;
			this.butCancel.Text = "&Close";
			this.butCancel.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAddMapArea
			// 
			this.butAddMapArea.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddMapArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddMapArea.Autosize = true;
			this.butAddMapArea.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddMapArea.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddMapArea.CornerRadius = 4F;
			this.butAddMapArea.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddMapArea.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddMapArea.Location = new System.Drawing.Point(1490, 913);
			this.butAddMapArea.Name = "butAddMapArea";
			this.butAddMapArea.Size = new System.Drawing.Size(75, 24);
			this.butAddMapArea.TabIndex = 49;
			this.butAddMapArea.Text = "Add";
			this.butAddMapArea.Click += new System.EventHandler(this.butAddMapArea_Click);
			// 
			// FormMapSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(1577, 1042);
			this.Controls.Add(this.butAddMapArea);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridEmployees);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.mapAreaPanel);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "FormMapSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Map Setup";
			this.Load += new System.EventHandler(this.FormMapSetup_Load);
			((System.ComponentModel.ISupportInitialize)(this.numFloorWidthFeet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFloorHeightFeet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPixelsPerFoot)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.menu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MapAreaPanel mapAreaPanel;
		private System.Windows.Forms.CheckBox checkShowGrid;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numFloorWidthFeet;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numFloorHeightFeet;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numPixelsPerFoot;
		private System.Windows.Forms.CheckBox checkShowOutline;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ContextMenuStrip menu;
		private System.Windows.Forms.ToolStripMenuItem newCubicleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newLabelToolStripMenuItem;
		private UI.ODGrid gridEmployees;
		private System.Windows.Forms.Label label1;
		private UI.Button butCancel;
		private UI.Button butAddMapArea;
	}
}