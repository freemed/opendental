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
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboSort = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.labelAptNum = new System.Windows.Forms.Label();
			this.labelPatNum = new System.Windows.Forms.Label();
			this.comboHL7Status = new System.Windows.Forms.ComboBox();
			this.labelHL7Status = new System.Windows.Forms.Label();
			this.textDateEnd = new OpenDental.ValidDate();
			this.textDateStart = new OpenDental.ValidDate();
			this.labelEndDate = new System.Windows.Forms.Label();
			this.labelStartDate = new System.Windows.Forms.Label();
			this.butRefresh = new OpenDental.UI.Button();
			this.textPatNum = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(754,612);
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
			this.butCancel.Location = new System.Drawing.Point(840,612);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(17,70);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(898,536);
			this.gridMain.TabIndex = 19;
			this.gridMain.Title = "HL7 Message Log";
			this.gridMain.TranslationName = null;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.textPatNum);
			this.groupBox1.Controls.Add(this.comboSort);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.labelAptNum);
			this.groupBox1.Controls.Add(this.labelPatNum);
			this.groupBox1.Controls.Add(this.comboHL7Status);
			this.groupBox1.Controls.Add(this.labelHL7Status);
			this.groupBox1.Controls.Add(this.textDateEnd);
			this.groupBox1.Controls.Add(this.textDateStart);
			this.groupBox1.Controls.Add(this.labelEndDate);
			this.groupBox1.Controls.Add(this.labelStartDate);
			this.groupBox1.Controls.Add(this.butRefresh);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(17,2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(755,62);
			this.groupBox1.TabIndex = 20;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "View";
			// 
			// comboSort
			// 
			this.comboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSort.Location = new System.Drawing.Point(292,35);
			this.comboSort.MaxDropDownItems = 40;
			this.comboSort.Name = "comboSort";
			this.comboSort.Size = new System.Drawing.Size(118,21);
			this.comboSort.TabIndex = 37;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(210,36);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80,18);
			this.label5.TabIndex = 36;
			this.label5.Text = "Sort";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelAptNum
			// 
			this.labelAptNum.Location = new System.Drawing.Point(425,36);
			this.labelAptNum.Name = "labelAptNum";
			this.labelAptNum.Size = new System.Drawing.Size(80,18);
			this.labelAptNum.TabIndex = 24;
			this.labelAptNum.Text = "AptNum";
			this.labelAptNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelPatNum
			// 
			this.labelPatNum.Location = new System.Drawing.Point(425,13);
			this.labelPatNum.Name = "labelPatNum";
			this.labelPatNum.Size = new System.Drawing.Size(80,18);
			this.labelPatNum.TabIndex = 22;
			this.labelPatNum.Text = "PatNum";
			this.labelPatNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboHL7Status
			// 
			this.comboHL7Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboHL7Status.Location = new System.Drawing.Point(292,12);
			this.comboHL7Status.MaxDropDownItems = 40;
			this.comboHL7Status.Name = "comboHL7Status";
			this.comboHL7Status.Size = new System.Drawing.Size(118,21);
			this.comboHL7Status.TabIndex = 21;
			// 
			// labelHL7Status
			// 
			this.labelHL7Status.Location = new System.Drawing.Point(210,13);
			this.labelHL7Status.Name = "labelHL7Status";
			this.labelHL7Status.Size = new System.Drawing.Size(80,18);
			this.labelHL7Status.TabIndex = 20;
			this.labelHL7Status.Text = "HL7Status";
			this.labelHL7Status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			// labelEndDate
			// 
			this.labelEndDate.Location = new System.Drawing.Point(29,36);
			this.labelEndDate.Name = "labelEndDate";
			this.labelEndDate.Size = new System.Drawing.Size(80,18);
			this.labelEndDate.TabIndex = 12;
			this.labelEndDate.Text = "End Date";
			this.labelEndDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelStartDate
			// 
			this.labelStartDate.Location = new System.Drawing.Point(29,13);
			this.labelStartDate.Name = "labelStartDate";
			this.labelStartDate.Size = new System.Drawing.Size(80,18);
			this.labelStartDate.TabIndex = 11;
			this.labelStartDate.Text = "Start Date";
			this.labelStartDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(650,12);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(98,24);
			this.butRefresh.TabIndex = 2;
			this.butRefresh.Text = "&Refresh List";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// textPatNum
			// 
			this.textPatNum.Location = new System.Drawing.Point(507,12);
			this.textPatNum.Name = "textPatNum";
			this.textPatNum.Size = new System.Drawing.Size(118,20);
			this.textPatNum.TabIndex = 38;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(507,35);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(118,20);
			this.textBox1.TabIndex = 39;
			// 
			// FormHL7Msgs
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(931,652);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormHL7Msgs";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HL7 Messages";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboSort;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label labelAptNum;
		private System.Windows.Forms.Label labelPatNum;
		private System.Windows.Forms.ComboBox comboHL7Status;
		private System.Windows.Forms.Label labelHL7Status;
		private ValidDate textDateEnd;
		private ValidDate textDateStart;
		private System.Windows.Forms.Label labelEndDate;
		private System.Windows.Forms.Label labelStartDate;
		private UI.Button butRefresh;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textPatNum;
	}
}