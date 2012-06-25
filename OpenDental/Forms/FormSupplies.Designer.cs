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
			this.label1 = new System.Windows.Forms.Label();
			this.textFind = new System.Windows.Forms.TextBox();
			this.butAdd = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butPrint = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(521, 17);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 18);
			this.label3.TabIndex = 14;
			this.label3.Text = "Supplier";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboSupplier
			// 
			this.comboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSupplier.FormattingEnabled = true;
			this.comboSupplier.Location = new System.Drawing.Point(589, 16);
			this.comboSupplier.Name = "comboSupplier";
			this.comboSupplier.Size = new System.Drawing.Size(170, 21);
			this.comboSupplier.TabIndex = 13;
			this.comboSupplier.SelectionChangeCommitted += new System.EventHandler(this.comboSupplier_SelectionChangeCommitted);
			// 
			// checkShowHidden
			// 
			this.checkShowHidden.Location = new System.Drawing.Point(89, 19);
			this.checkShowHidden.Name = "checkShowHidden";
			this.checkShowHidden.Size = new System.Drawing.Size(95, 18);
			this.checkShowHidden.TabIndex = 12;
			this.checkShowHidden.Text = "Show Hidden";
			this.checkShowHidden.UseVisualStyleBackColor = true;
			this.checkShowHidden.Click += new System.EventHandler(this.checkShowHidden_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(247, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 18);
			this.label1.TabIndex = 18;
			this.label1.Text = "Find";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textFind
			// 
			this.textFind.Location = new System.Drawing.Point(301, 17);
			this.textFind.Name = "textFind";
			this.textFind.Size = new System.Drawing.Size(168, 20);
			this.textFind.TabIndex = 19;
			this.textFind.TextChanged += new System.EventHandler(this.textFind_TextChanged);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(12, 13);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(69, 24);
			this.butAdd.TabIndex = 11;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12, 43);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(747, 501);
			this.gridMain.TabIndex = 5;
			this.gridMain.Title = "Supplies";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(345, 550);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(80, 26);
			this.butPrint.TabIndex = 25;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(603, 552);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 27;
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
			this.butCancel.Location = new System.Drawing.Point(684, 552);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 26;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormSupplies
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(771, 588);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.textFind);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboSupplier);
			this.Controls.Add(this.checkShowHidden);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.gridMain);
			this.Name = "FormSupplies";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Supplies";
			this.Load += new System.EventHandler(this.FormSupplies_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.ODGrid gridMain;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboSupplier;
		private System.Windows.Forms.CheckBox checkShowHidden;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textFind;
		private UI.Button butPrint;
		private UI.Button butOK;
		private UI.Button butCancel;
	}
}