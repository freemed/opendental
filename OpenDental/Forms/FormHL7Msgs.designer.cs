namespace OpenDental{
	partial class FormHL7Msgs {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHL7Msgs));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labelHL7Status = new System.Windows.Forms.Label();
			this.labelEndDate = new System.Windows.Forms.Label();
			this.textPatient = new System.Windows.Forms.TextBox();
			this.labelPatient = new System.Windows.Forms.Label();
			this.comboHL7Status = new System.Windows.Forms.ComboBox();
			this.labelStartDate = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butAll = new OpenDental.UI.Button();
			this.butFind = new OpenDental.UI.Button();
			this.textDateEnd = new OpenDental.ValidDate();
			this.textDateStart = new OpenDental.ValidDate();
			this.butRefresh = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butAll);
			this.groupBox1.Controls.Add(this.butFind);
			this.groupBox1.Controls.Add(this.textDateEnd);
			this.groupBox1.Controls.Add(this.textDateStart);
			this.groupBox1.Controls.Add(this.labelHL7Status);
			this.groupBox1.Controls.Add(this.labelEndDate);
			this.groupBox1.Controls.Add(this.textPatient);
			this.groupBox1.Controls.Add(this.labelPatient);
			this.groupBox1.Controls.Add(this.comboHL7Status);
			this.groupBox1.Controls.Add(this.labelStartDate);
			this.groupBox1.Controls.Add(this.butRefresh);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(21,6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(816,62);
			this.groupBox1.TabIndex = 20;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "View";
			// 
			// labelHL7Status
			// 
			this.labelHL7Status.Location = new System.Drawing.Point(465,12);
			this.labelHL7Status.Name = "labelHL7Status";
			this.labelHL7Status.Size = new System.Drawing.Size(80,18);
			this.labelHL7Status.TabIndex = 20;
			this.labelHL7Status.Text = "HL7Status";
			this.labelHL7Status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelEndDate
			// 
			this.labelEndDate.Location = new System.Drawing.Point(29,35);
			this.labelEndDate.Name = "labelEndDate";
			this.labelEndDate.Size = new System.Drawing.Size(80,18);
			this.labelEndDate.TabIndex = 12;
			this.labelEndDate.Text = "End Date";
			this.labelEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPatient
			// 
			this.textPatient.BackColor = System.Drawing.SystemColors.Window;
			this.textPatient.Location = new System.Drawing.Point(292,12);
			this.textPatient.Name = "textPatient";
			this.textPatient.ReadOnly = true;
			this.textPatient.Size = new System.Drawing.Size(155,20);
			this.textPatient.TabIndex = 38;
			// 
			// labelPatient
			// 
			this.labelPatient.Location = new System.Drawing.Point(210,12);
			this.labelPatient.Name = "labelPatient";
			this.labelPatient.Size = new System.Drawing.Size(80,18);
			this.labelPatient.TabIndex = 22;
			this.labelPatient.Text = "Patient";
			this.labelPatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboHL7Status
			// 
			this.comboHL7Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboHL7Status.Location = new System.Drawing.Point(547,12);
			this.comboHL7Status.MaxDropDownItems = 40;
			this.comboHL7Status.Name = "comboHL7Status";
			this.comboHL7Status.Size = new System.Drawing.Size(155,21);
			this.comboHL7Status.TabIndex = 21;
			this.comboHL7Status.SelectedIndexChanged += new System.EventHandler(this.comboHL7Status_SelectedIndexChanged);
			// 
			// labelStartDate
			// 
			this.labelStartDate.Location = new System.Drawing.Point(29,12);
			this.labelStartDate.Name = "labelStartDate";
			this.labelStartDate.Size = new System.Drawing.Size(80,18);
			this.labelStartDate.TabIndex = 11;
			this.labelStartDate.Text = "Start Date";
			this.labelStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(21,74);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(906,547);
			this.gridMain.TabIndex = 19;
			this.gridMain.Title = "HL7 Message Log";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(382,34);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(65,24);
			this.butAll.TabIndex = 41;
			this.butAll.Text = "All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// butFind
			// 
			this.butFind.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFind.Autosize = true;
			this.butFind.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFind.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFind.CornerRadius = 4F;
			this.butFind.Location = new System.Drawing.Point(292,34);
			this.butFind.Name = "butFind";
			this.butFind.Size = new System.Drawing.Size(65,24);
			this.butFind.TabIndex = 40;
			this.butFind.Text = "Find";
			this.butFind.Click += new System.EventHandler(this.butFind_Click);
			// 
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(112,35);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(77,20);
			this.textDateEnd.TabIndex = 18;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(112,12);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77,20);
			this.textDateStart.TabIndex = 17;
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(736,10);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(75,24);
			this.butRefresh.TabIndex = 2;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(762,623);
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
			this.butCancel.Location = new System.Drawing.Point(848,623);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormHL7Msgs
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(939,663);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormHL7Msgs";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HL7 Messages";
			this.Load += new System.EventHandler(this.FormHL7Msgs_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label labelPatient;
		private System.Windows.Forms.ComboBox comboHL7Status;
		private System.Windows.Forms.Label labelHL7Status;
		private ValidDate textDateEnd;
		private ValidDate textDateStart;
		private System.Windows.Forms.Label labelEndDate;
		private System.Windows.Forms.Label labelStartDate;
		private UI.Button butRefresh;
		private System.Windows.Forms.TextBox textPatient;
		private UI.Button butFind;
		private UI.Button butAll;
	}
}