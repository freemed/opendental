namespace OpenDental{
	partial class FormReference {
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
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textSuperFamily = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textSpecialty = new System.Windows.Forms.TextBox();
			this.textAge = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textAreaCode = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textLName = new System.Windows.Forms.TextBox();
			this.textZip = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.textFName = new System.Windows.Forms.TextBox();
			this.textPatNum = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textState = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textCity = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.checkBadRefs = new System.Windows.Forms.CheckBox();
			this.groupFilter = new System.Windows.Forms.GroupBox();
			this.checkUsedRefs = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkRefresh = new System.Windows.Forms.CheckBox();
			this.butGetAll = new OpenDental.UI.Button();
			this.butSearch = new OpenDental.UI.Button();
			this.groupBox2.SuspendLayout();
			this.groupFilter.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(904, 565);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 3;
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
			this.butCancel.Location = new System.Drawing.Point(985, 565);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = true;
			this.gridMain.Location = new System.Drawing.Point(12, 12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(768, 577);
			this.gridMain.TabIndex = 11;
			this.gridMain.Title = "Select a Reference";
			this.gridMain.TranslationName = "FormPatientSelect";
			this.gridMain.WrapText = false;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			this.gridMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridMain_KeyDown);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.textSuperFamily);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.textSpecialty);
			this.groupBox2.Controls.Add(this.textAge);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.textAreaCode);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.textLName);
			this.groupBox2.Controls.Add(this.textZip);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.textFName);
			this.groupBox2.Controls.Add(this.textPatNum);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.textState);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.textCity);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(791, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(269, 228);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Search by:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 119);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(154, 20);
			this.label6.TabIndex = 31;
			this.label6.Text = "# (or more) in Super Family";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSuperFamily
			// 
			this.textSuperFamily.Location = new System.Drawing.Point(163, 119);
			this.textSuperFamily.Name = "textSuperFamily";
			this.textSuperFamily.Size = new System.Drawing.Size(90, 20);
			this.textSuperFamily.TabIndex = 6;
			this.textSuperFamily.TextChanged += new System.EventHandler(this.textSuperFamily_TextChanged);
			this.textSuperFamily.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textSuperFamily_KeyDown);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(59, 99);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(103, 20);
			this.label5.TabIndex = 29;
			this.label5.Text = "Specialty";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSpecialty
			// 
			this.textSpecialty.Location = new System.Drawing.Point(163, 99);
			this.textSpecialty.Name = "textSpecialty";
			this.textSpecialty.Size = new System.Drawing.Size(90, 20);
			this.textSpecialty.TabIndex = 5;
			this.textSpecialty.TextChanged += new System.EventHandler(this.textSpecialty_TextChanged);
			this.textSpecialty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textSpecialty_KeyDown);
			// 
			// textAge
			// 
			this.textAge.Location = new System.Drawing.Point(163, 199);
			this.textAge.Name = "textAge";
			this.textAge.Size = new System.Drawing.Size(90, 20);
			this.textAge.TabIndex = 10;
			this.textAge.TextChanged += new System.EventHandler(this.textAge_TextChanged);
			this.textAge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAge_KeyDown);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(65, 199);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 20);
			this.label2.TabIndex = 27;
			this.label2.Text = "Age At Least";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAreaCode
			// 
			this.textAreaCode.Location = new System.Drawing.Point(163, 79);
			this.textAreaCode.Name = "textAreaCode";
			this.textAreaCode.Size = new System.Drawing.Size(90, 20);
			this.textAreaCode.TabIndex = 4;
			this.textAreaCode.TextChanged += new System.EventHandler(this.textAreaCode_TextChanged);
			this.textAreaCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAreaCode_KeyDown);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(34, 79);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(129, 20);
			this.label4.TabIndex = 7;
			this.label4.Text = "Area Code";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(59, 139);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "Last Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(163, 139);
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(90, 20);
			this.textLName.TabIndex = 7;
			this.textLName.TextChanged += new System.EventHandler(this.textLName_TextChanged);
			this.textLName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textLName_KeyDown);
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(163, 59);
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(90, 20);
			this.textZip.TabIndex = 3;
			this.textZip.TextChanged += new System.EventHandler(this.textZip_TextChanged);
			this.textZip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textZip_KeyDown);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(62, 159);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 20);
			this.label3.TabIndex = 5;
			this.label3.Text = "First Name";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(62, 59);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(101, 20);
			this.label12.TabIndex = 24;
			this.label12.Text = "Zip";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(163, 159);
			this.textFName.Name = "textFName";
			this.textFName.Size = new System.Drawing.Size(90, 20);
			this.textFName.TabIndex = 8;
			this.textFName.TextChanged += new System.EventHandler(this.textFName_TextChanged);
			this.textFName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textFName_KeyDown);
			// 
			// textPatNum
			// 
			this.textPatNum.Location = new System.Drawing.Point(163, 179);
			this.textPatNum.Name = "textPatNum";
			this.textPatNum.Size = new System.Drawing.Size(90, 20);
			this.textPatNum.TabIndex = 9;
			this.textPatNum.TextChanged += new System.EventHandler(this.textPatNum_TextChanged);
			this.textPatNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textPatNum_KeyDown);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(63, 179);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(101, 20);
			this.label9.TabIndex = 18;
			this.label9.Text = "Patient Number";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(163, 39);
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(90, 20);
			this.textState.TabIndex = 2;
			this.textState.TextChanged += new System.EventHandler(this.textState_TextChanged);
			this.textState.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textState_KeyDown);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(62, 39);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 20);
			this.label8.TabIndex = 16;
			this.label8.Text = "State";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(163, 19);
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(90, 20);
			this.textCity.TabIndex = 1;
			this.textCity.TextChanged += new System.EventHandler(this.textCity_TextChanged);
			this.textCity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textCity_KeyDown);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(62, 19);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(98, 20);
			this.label7.TabIndex = 14;
			this.label7.Text = "City";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkBadRefs
			// 
			this.checkBadRefs.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBadRefs.Location = new System.Drawing.Point(9, 38);
			this.checkBadRefs.Name = "checkBadRefs";
			this.checkBadRefs.Size = new System.Drawing.Size(187, 17);
			this.checkBadRefs.TabIndex = 12;
			this.checkBadRefs.Text = "Include bad references";
			this.checkBadRefs.Click += new System.EventHandler(this.checkBadRefs_Click);
			// 
			// groupFilter
			// 
			this.groupFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupFilter.Controls.Add(this.checkUsedRefs);
			this.groupFilter.Controls.Add(this.checkBadRefs);
			this.groupFilter.Location = new System.Drawing.Point(791, 246);
			this.groupFilter.Name = "groupFilter";
			this.groupFilter.Size = new System.Drawing.Size(269, 65);
			this.groupFilter.TabIndex = 45;
			this.groupFilter.TabStop = false;
			this.groupFilter.Text = "Filters:";
			// 
			// checkUsedRefs
			// 
			this.checkUsedRefs.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkUsedRefs.Location = new System.Drawing.Point(9, 19);
			this.checkUsedRefs.Name = "checkUsedRefs";
			this.checkUsedRefs.Size = new System.Drawing.Size(187, 17);
			this.checkUsedRefs.TabIndex = 11;
			this.checkUsedRefs.Text = "Used references only";
			this.checkUsedRefs.Click += new System.EventHandler(this.checkUsedRefs_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.checkRefresh);
			this.groupBox1.Controls.Add(this.butGetAll);
			this.groupBox1.Controls.Add(this.butSearch);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(791, 317);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(269, 76);
			this.groupBox1.TabIndex = 47;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Search";
			// 
			// checkRefresh
			// 
			this.checkRefresh.Checked = true;
			this.checkRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkRefresh.Location = new System.Drawing.Point(43, 51);
			this.checkRefresh.Name = "checkRefresh";
			this.checkRefresh.Size = new System.Drawing.Size(195, 18);
			this.checkRefresh.TabIndex = 11;
			this.checkRefresh.Text = "Refresh while typing";
			this.checkRefresh.UseVisualStyleBackColor = true;
			// 
			// butGetAll
			// 
			this.butGetAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butGetAll.Autosize = true;
			this.butGetAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butGetAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butGetAll.CornerRadius = 4F;
			this.butGetAll.Location = new System.Drawing.Point(163, 19);
			this.butGetAll.Name = "butGetAll";
			this.butGetAll.Size = new System.Drawing.Size(75, 23);
			this.butGetAll.TabIndex = 10;
			this.butGetAll.Text = "Get All";
			this.butGetAll.Click += new System.EventHandler(this.butGetAll_Click);
			// 
			// butSearch
			// 
			this.butSearch.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSearch.Autosize = true;
			this.butSearch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearch.CornerRadius = 4F;
			this.butSearch.Location = new System.Drawing.Point(43, 19);
			this.butSearch.Name = "butSearch";
			this.butSearch.Size = new System.Drawing.Size(75, 23);
			this.butSearch.TabIndex = 7;
			this.butSearch.Text = "&Search";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
			// 
			// FormReference
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1072, 601);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupFilter);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormReference";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " Reference Select";
			this.Load += new System.EventHandler(this.FormReference_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupFilter.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textAge;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textPatNum;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textAreaCode;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textFName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBadRefs;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textSuperFamily;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textSpecialty;
		private System.Windows.Forms.GroupBox groupFilter;
		private System.Windows.Forms.CheckBox checkUsedRefs;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkRefresh;
		private UI.Button butGetAll;
		private UI.Button butSearch;
	}
}