namespace OpenDental{
	partial class FormWikiListEdit {
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
			this.butAddItem = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butColumnDelete = new OpenDental.UI.Button();
			this.butHeaders = new OpenDental.UI.Button();
			this.butColumnInsert = new OpenDental.UI.Button();
			this.butColumnRight = new OpenDental.UI.Button();
			this.butColumnLeft = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.butColumnDelete);
			this.groupBox1.Controls.Add(this.butHeaders);
			this.groupBox1.Controls.Add(this.butColumnInsert);
			this.groupBox1.Controls.Add(this.butColumnRight);
			this.groupBox1.Controls.Add(this.butColumnLeft);
			this.groupBox1.Location = new System.Drawing.Point(861, 66);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(88, 141);
			this.groupBox1.TabIndex = 28;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Columns";
			// 
			// butAddItem
			// 
			this.butAddItem.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddItem.Autosize = true;
			this.butAddItem.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddItem.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddItem.CornerRadius = 4F;
			this.butAddItem.Location = new System.Drawing.Point(869, 213);
			this.butAddItem.Name = "butAddItem";
			this.butAddItem.Size = new System.Drawing.Size(71, 24);
			this.butAddItem.TabIndex = 31;
			this.butAddItem.Text = "Add Item";
			this.butAddItem.Click += new System.EventHandler(this.butAddItem_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Enabled = false;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(12, 589);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 24);
			this.butDelete.TabIndex = 36;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butColumnDelete
			// 
			this.butColumnDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butColumnDelete.Autosize = true;
			this.butColumnDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butColumnDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butColumnDelete.CornerRadius = 4F;
			this.butColumnDelete.Location = new System.Drawing.Point(8, 109);
			this.butColumnDelete.Name = "butColumnDelete";
			this.butColumnDelete.Size = new System.Drawing.Size(71, 24);
			this.butColumnDelete.TabIndex = 34;
			this.butColumnDelete.Text = "Delete";
			this.butColumnDelete.Click += new System.EventHandler(this.butColumnDelete_Click);
			// 
			// butHeaders
			// 
			this.butHeaders.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butHeaders.Autosize = true;
			this.butHeaders.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHeaders.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHeaders.CornerRadius = 4F;
			this.butHeaders.Location = new System.Drawing.Point(8, 49);
			this.butHeaders.Name = "butHeaders";
			this.butHeaders.Size = new System.Drawing.Size(71, 24);
			this.butHeaders.TabIndex = 31;
			this.butHeaders.Text = "Col Edit";
			this.butHeaders.Click += new System.EventHandler(this.butHeaders_Click);
			// 
			// butColumnInsert
			// 
			this.butColumnInsert.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butColumnInsert.Autosize = true;
			this.butColumnInsert.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butColumnInsert.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butColumnInsert.CornerRadius = 4F;
			this.butColumnInsert.Location = new System.Drawing.Point(8, 79);
			this.butColumnInsert.Name = "butColumnInsert";
			this.butColumnInsert.Size = new System.Drawing.Size(71, 24);
			this.butColumnInsert.TabIndex = 33;
			this.butColumnInsert.Text = "Add Column";
			this.butColumnInsert.Click += new System.EventHandler(this.butColumnAdd_Click);
			// 
			// butColumnRight
			// 
			this.butColumnRight.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butColumnRight.Autosize = true;
			this.butColumnRight.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butColumnRight.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butColumnRight.CornerRadius = 4F;
			this.butColumnRight.Enabled = false;
			this.butColumnRight.Location = new System.Drawing.Point(49, 19);
			this.butColumnRight.Name = "butColumnRight";
			this.butColumnRight.Size = new System.Drawing.Size(30, 24);
			this.butColumnRight.TabIndex = 30;
			this.butColumnRight.Text = "R";
			this.butColumnRight.Click += new System.EventHandler(this.butColumnRight_Click);
			// 
			// butColumnLeft
			// 
			this.butColumnLeft.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butColumnLeft.Autosize = true;
			this.butColumnLeft.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butColumnLeft.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butColumnLeft.CornerRadius = 4F;
			this.butColumnLeft.Enabled = false;
			this.butColumnLeft.Location = new System.Drawing.Point(8, 19);
			this.butColumnLeft.Name = "butColumnLeft";
			this.butColumnLeft.Size = new System.Drawing.Size(30, 24);
			this.butColumnLeft.TabIndex = 29;
			this.butColumnLeft.Text = "L";
			this.butColumnLeft.Click += new System.EventHandler(this.butColumnLeft_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.EditableAcceptsCR = true;
			this.gridMain.HScrollVisible = true;
			this.gridMain.Location = new System.Drawing.Point(12, 12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.OneCell;
			this.gridMain.Size = new System.Drawing.Size(842, 574);
			this.gridMain.TabIndex = 26;
			this.gridMain.Title = "";
			this.gridMain.TranslationName = "";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			this.gridMain.CellTextChanged += new System.EventHandler(this.gridMain_CellTextChanged);
			this.gridMain.CellLeave += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellLeave);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(865, 589);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 20;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormWikiListEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(952, 613);
			this.Controls.Add(this.butAddItem);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Name = "FormWikiListEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Wiki List";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormWikiListEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private UI.Button butClose;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.GroupBox groupBox1;
		private UI.Button butHeaders;
		private UI.Button butColumnRight;
		private UI.Button butColumnLeft;
		private UI.Button butAddItem;
		private UI.Button butColumnDelete;
		private UI.Button butColumnInsert;
		private UI.Button butDelete;


	}
}