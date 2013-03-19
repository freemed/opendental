namespace OpenDental{
	partial class FormWikiTableEdit {
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
			this.butColumnDelete = new OpenDental.UI.Button();
			this.butHeaders = new OpenDental.UI.Button();
			this.butColumnInsert = new OpenDental.UI.Button();
			this.butColumnRight = new OpenDental.UI.Button();
			this.butColumnLeft = new OpenDental.UI.Button();
			this.groupRow = new System.Windows.Forms.GroupBox();
			this.butRowDelete = new OpenDental.UI.Button();
			this.butRowInsert = new OpenDental.UI.Button();
			this.butRowDown = new OpenDental.UI.Button();
			this.butRowUp = new OpenDental.UI.Button();
			this.butManEdit = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupRow.SuspendLayout();
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
			this.groupBox1.Text = "Column";
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
			this.butHeaders.Text = "Headers";
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
			this.butColumnRight.Location = new System.Drawing.Point(47, 19);
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
			this.butColumnLeft.Location = new System.Drawing.Point(8, 19);
			this.butColumnLeft.Name = "butColumnLeft";
			this.butColumnLeft.Size = new System.Drawing.Size(30, 24);
			this.butColumnLeft.TabIndex = 29;
			this.butColumnLeft.Text = "L";
			this.butColumnLeft.Click += new System.EventHandler(this.butColumnLeft_Click);
			// 
			// groupRow
			// 
			this.groupRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupRow.Controls.Add(this.butRowDelete);
			this.groupRow.Controls.Add(this.butRowInsert);
			this.groupRow.Controls.Add(this.butRowDown);
			this.groupRow.Controls.Add(this.butRowUp);
			this.groupRow.Location = new System.Drawing.Point(861, 214);
			this.groupRow.Name = "groupRow";
			this.groupRow.Size = new System.Drawing.Size(88, 141);
			this.groupRow.TabIndex = 32;
			this.groupRow.TabStop = false;
			this.groupRow.Text = "Row";
			// 
			// butRowDelete
			// 
			this.butRowDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRowDelete.Autosize = true;
			this.butRowDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRowDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRowDelete.CornerRadius = 4F;
			this.butRowDelete.Location = new System.Drawing.Point(8, 109);
			this.butRowDelete.Name = "butRowDelete";
			this.butRowDelete.Size = new System.Drawing.Size(71, 24);
			this.butRowDelete.TabIndex = 32;
			this.butRowDelete.Text = "Delete";
			this.butRowDelete.Click += new System.EventHandler(this.butRowDelete_Click);
			// 
			// butRowInsert
			// 
			this.butRowInsert.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRowInsert.Autosize = true;
			this.butRowInsert.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRowInsert.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRowInsert.CornerRadius = 4F;
			this.butRowInsert.Location = new System.Drawing.Point(8, 79);
			this.butRowInsert.Name = "butRowInsert";
			this.butRowInsert.Size = new System.Drawing.Size(71, 24);
			this.butRowInsert.TabIndex = 31;
			this.butRowInsert.Text = "Add Row";
			this.butRowInsert.Click += new System.EventHandler(this.butRowAdd_Click);
			// 
			// butRowDown
			// 
			this.butRowDown.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRowDown.Autosize = true;
			this.butRowDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRowDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRowDown.CornerRadius = 4F;
			this.butRowDown.Location = new System.Drawing.Point(8, 49);
			this.butRowDown.Name = "butRowDown";
			this.butRowDown.Size = new System.Drawing.Size(44, 24);
			this.butRowDown.TabIndex = 30;
			this.butRowDown.Text = "Down";
			this.butRowDown.Click += new System.EventHandler(this.butRowDown_Click);
			// 
			// butRowUp
			// 
			this.butRowUp.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRowUp.Autosize = true;
			this.butRowUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRowUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRowUp.CornerRadius = 4F;
			this.butRowUp.Location = new System.Drawing.Point(8, 19);
			this.butRowUp.Name = "butRowUp";
			this.butRowUp.Size = new System.Drawing.Size(44, 24);
			this.butRowUp.TabIndex = 29;
			this.butRowUp.Text = "Up";
			this.butRowUp.Click += new System.EventHandler(this.butRowUp_Click);
			// 
			// butManEdit
			// 
			this.butManEdit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butManEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butManEdit.Autosize = true;
			this.butManEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butManEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butManEdit.CornerRadius = 4F;
			this.butManEdit.Location = new System.Drawing.Point(865, 12);
			this.butManEdit.Name = "butManEdit";
			this.butManEdit.Size = new System.Drawing.Size(75, 24);
			this.butManEdit.TabIndex = 27;
			this.butManEdit.Text = "Man Edit";
			this.butManEdit.Click += new System.EventHandler(this.butManEdit_Click);
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
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(784, 589);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 21;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(865, 589);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 20;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(12, 589);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 24);
			this.butDelete.TabIndex = 36;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormWikiTableEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(952, 613);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.groupRow);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butManEdit);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormWikiTableEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Wiki Table";
			this.Load += new System.EventHandler(this.FormWikiTableEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupRow.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private UI.Button butOK;
		private UI.Button butCancel;
		private UI.ODGrid gridMain;
		private UI.Button butManEdit;
		private System.Windows.Forms.GroupBox groupBox1;
		private UI.Button butHeaders;
		private UI.Button butColumnRight;
		private UI.Button butColumnLeft;
		private System.Windows.Forms.GroupBox groupRow;
		private UI.Button butRowDelete;
		private UI.Button butRowInsert;
		private UI.Button butRowDown;
		private UI.Button butRowUp;
		private UI.Button butColumnDelete;
		private UI.Button butColumnInsert;
		private UI.Button butDelete;


	}
}