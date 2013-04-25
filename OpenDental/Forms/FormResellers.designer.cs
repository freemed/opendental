namespace OpenDental{
	partial class FormResellers {
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
			this.butClose = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkShowInactive = new System.Windows.Forms.CheckBox();
			this.textEmail = new System.Windows.Forms.TextBox();
			this.labelEmail = new System.Windows.Forms.Label();
			this.textPatNum = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textState = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textCity = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textHmPhone = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textFName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textLName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.menuRightClick = new System.Windows.Forms.ContextMenu();
			this.menuItemAccount = new System.Windows.Forms.MenuItem();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(960, 460);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(960, 270);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 26);
			this.butAdd.TabIndex = 35;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
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
			this.gridMain.Size = new System.Drawing.Size(780, 472);
			this.gridMain.TabIndex = 38;
			this.gridMain.Title = "Resellers";
			this.gridMain.TranslationName = "FormMedications";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.checkShowInactive);
			this.groupBox2.Controls.Add(this.textEmail);
			this.groupBox2.Controls.Add(this.labelEmail);
			this.groupBox2.Controls.Add(this.textPatNum);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.textState);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.textCity);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.textAddress);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.textHmPhone);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.textFName);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.textLName);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(798, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(237, 232);
			this.groupBox2.TabIndex = 39;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Search by:";
			// 
			// checkShowInactive
			// 
			this.checkShowInactive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowInactive.Location = new System.Drawing.Point(14, 206);
			this.checkShowInactive.Name = "checkShowInactive";
			this.checkShowInactive.Size = new System.Drawing.Size(161, 17);
			this.checkShowInactive.TabIndex = 45;
			this.checkShowInactive.Text = "Show Inactive Resellers";
			this.checkShowInactive.CheckedChanged += new System.EventHandler(this.checkShowInactive_CheckedChanged);
			// 
			// textEmail
			// 
			this.textEmail.Location = new System.Drawing.Point(135, 178);
			this.textEmail.Name = "textEmail";
			this.textEmail.Size = new System.Drawing.Size(90, 20);
			this.textEmail.TabIndex = 11;
			this.textEmail.TextChanged += new System.EventHandler(this.textEmail_TextChanged);
			// 
			// labelEmail
			// 
			this.labelEmail.Location = new System.Drawing.Point(11, 182);
			this.labelEmail.Name = "labelEmail";
			this.labelEmail.Size = new System.Drawing.Size(125, 12);
			this.labelEmail.TabIndex = 43;
			this.labelEmail.Text = "E-mail";
			this.labelEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPatNum
			// 
			this.textPatNum.Location = new System.Drawing.Point(135, 158);
			this.textPatNum.Name = "textPatNum";
			this.textPatNum.Size = new System.Drawing.Size(90, 20);
			this.textPatNum.TabIndex = 7;
			this.textPatNum.TextChanged += new System.EventHandler(this.textPatNum_TextChanged);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(35, 162);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(101, 12);
			this.label9.TabIndex = 18;
			this.label9.Text = "Patient Number";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(135, 138);
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(90, 20);
			this.textState.TabIndex = 5;
			this.textState.TextChanged += new System.EventHandler(this.textState_TextChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(34, 142);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 12);
			this.label8.TabIndex = 16;
			this.label8.Text = "State";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(135, 118);
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(90, 20);
			this.textCity.TabIndex = 4;
			this.textCity.TextChanged += new System.EventHandler(this.textCity_TextChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(34, 120);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(98, 14);
			this.label7.TabIndex = 14;
			this.label7.Text = "City";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(200, 16);
			this.label6.TabIndex = 10;
			this.label6.Text = "Hint: enter values in multiple boxes.";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(135, 98);
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(90, 20);
			this.textAddress.TabIndex = 3;
			this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(34, 101);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 12);
			this.label5.TabIndex = 9;
			this.label5.Text = "Address";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textHmPhone
			// 
			this.textHmPhone.Location = new System.Drawing.Point(135, 78);
			this.textHmPhone.Name = "textHmPhone";
			this.textHmPhone.Size = new System.Drawing.Size(90, 20);
			this.textHmPhone.TabIndex = 2;
			this.textHmPhone.TextChanged += new System.EventHandler(this.textHmPhone_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(129, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "Phone (any)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(135, 58);
			this.textFName.Name = "textFName";
			this.textFName.Size = new System.Drawing.Size(90, 20);
			this.textFName.TabIndex = 1;
			this.textFName.TextChanged += new System.EventHandler(this.textFName_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(34, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "First Name";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(135, 38);
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(90, 20);
			this.textLName.TabIndex = 0;
			this.textLName.TextChanged += new System.EventHandler(this.textLName_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(31, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "Last Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(832, 299);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(209, 38);
			this.label2.TabIndex = 40;
			this.label2.Text = "Add as a customer first.\r\nThey must also be the guarantor.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// menuRightClick
			// 
			this.menuRightClick.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAccount});
			// 
			// menuItemAccount
			// 
			this.menuItemAccount.Index = 0;
			this.menuItemAccount.Text = "Go to Account";
			this.menuItemAccount.Click += new System.EventHandler(this.menuItemAccount_Click);
			// 
			// FormResellers
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1047, 496);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Name = "FormResellers";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Resellers";
			this.Load += new System.EventHandler(this.FormResellers_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private UI.Button butAdd;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textEmail;
		private System.Windows.Forms.Label labelEmail;
		private System.Windows.Forms.TextBox textPatNum;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textHmPhone;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textFName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkShowInactive;
		private System.Windows.Forms.ContextMenu menuRightClick;
		private System.Windows.Forms.MenuItem menuItemAccount;
	}
}