namespace OpenDental{
	partial class FormTimeCardRuleEdit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTimeCardRuleEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textOverHoursPerDay = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textAfterTimeOfDay = new System.Windows.Forms.TextBox();
			this.listEmployees = new System.Windows.Forms.ListBox();
			this.but6am = new OpenDental.UI.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.textBeforeTimeOfDay = new System.Windows.Forms.TextBox();
			this.but5pm = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.butRight = new OpenDental.UI.Button();
			this.butLeft = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9, 127);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(253, 18);
			this.label1.TabIndex = 4;
			this.label1.Text = "Exempt Employees";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(18, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(153, 18);
			this.label2.TabIndex = 6;
			this.label2.Text = "Overtime if over Hours Per Day";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textOverHoursPerDay
			// 
			this.textOverHoursPerDay.Location = new System.Drawing.Point(174, 10);
			this.textOverHoursPerDay.Name = "textOverHoursPerDay";
			this.textOverHoursPerDay.Size = new System.Drawing.Size(62, 20);
			this.textOverHoursPerDay.TabIndex = 7;
			this.textOverHoursPerDay.Text = "8:00";
			this.textOverHoursPerDay.TextChanged += new System.EventHandler(this.textOverHoursPerDay_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(27, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(111, 18);
			this.label3.TabIndex = 8;
			this.label3.Text = "After Time of Day";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAfterTimeOfDay
			// 
			this.textAfterTimeOfDay.Location = new System.Drawing.Point(141, 44);
			this.textAfterTimeOfDay.Name = "textAfterTimeOfDay";
			this.textAfterTimeOfDay.Size = new System.Drawing.Size(62, 20);
			this.textAfterTimeOfDay.TabIndex = 9;
			this.textAfterTimeOfDay.Text = "17:00";
			this.textAfterTimeOfDay.TextChanged += new System.EventHandler(this.textAfterTimeOfDay_TextChanged);
			// 
			// listEmployees
			// 
			this.listEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listEmployees.FormattingEnabled = true;
			this.listEmployees.Location = new System.Drawing.Point(12, 150);
			this.listEmployees.Name = "listEmployees";
			this.listEmployees.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listEmployees.Size = new System.Drawing.Size(250, 446);
			this.listEmployees.TabIndex = 156;
			// 
			// but6am
			// 
			this.but6am.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.but6am.Autosize = true;
			this.but6am.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but6am.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but6am.CornerRadius = 4F;
			this.but6am.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.but6am.Location = new System.Drawing.Point(209, 15);
			this.but6am.Name = "but6am";
			this.but6am.Size = new System.Drawing.Size(35, 22);
			this.but6am.TabIndex = 161;
			this.but6am.Text = "6 AM";
			this.but6am.Click += new System.EventHandler(this.but6am_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(27, 17);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111, 18);
			this.label4.TabIndex = 159;
			this.label4.Text = "Before Time of Day";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBeforeTimeOfDay
			// 
			this.textBeforeTimeOfDay.Location = new System.Drawing.Point(141, 17);
			this.textBeforeTimeOfDay.Name = "textBeforeTimeOfDay";
			this.textBeforeTimeOfDay.Size = new System.Drawing.Size(62, 20);
			this.textBeforeTimeOfDay.TabIndex = 160;
			this.textBeforeTimeOfDay.Text = "6:00";
			this.textBeforeTimeOfDay.TextChanged += new System.EventHandler(this.textBeforeTimeOfDay_TextChanged);
			// 
			// but5pm
			// 
			this.but5pm.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.but5pm.Autosize = true;
			this.but5pm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but5pm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but5pm.CornerRadius = 4F;
			this.but5pm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.but5pm.Location = new System.Drawing.Point(209, 43);
			this.but5pm.Name = "but5pm";
			this.but5pm.Size = new System.Drawing.Size(35, 22);
			this.but5pm.TabIndex = 158;
			this.but5pm.Text = "5 PM";
			this.but5pm.Click += new System.EventHandler(this.but5pm_Click);
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
			this.butDelete.Location = new System.Drawing.Point(13, 615);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 24);
			this.butDelete.TabIndex = 155;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(403, 615);
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
			this.butCancel.Location = new System.Drawing.Point(484, 615);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(309, 150);
			this.listBox1.Name = "listBox1";
			this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBox1.Size = new System.Drawing.Size(250, 446);
			this.listBox1.TabIndex = 159;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(306, 127);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(253, 18);
			this.label5.TabIndex = 158;
			this.label5.Text = "Participating Employees";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butRight
			// 
			this.butRight.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRight.Autosize = true;
			this.butRight.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRight.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRight.CornerRadius = 4F;
			this.butRight.Image = global::OpenDental.Properties.Resources.Right;
			this.butRight.Location = new System.Drawing.Point(268, 295);
			this.butRight.Name = "butRight";
			this.butRight.Size = new System.Drawing.Size(35, 26);
			this.butRight.TabIndex = 161;
			// 
			// butLeft
			// 
			this.butLeft.AdjustImageLocation = new System.Drawing.Point(-1, 0);
			this.butLeft.Autosize = true;
			this.butLeft.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLeft.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLeft.CornerRadius = 4F;
			this.butLeft.Image = global::OpenDental.Properties.Resources.Left;
			this.butLeft.Location = new System.Drawing.Point(268, 329);
			this.butLeft.Name = "butLeft";
			this.butLeft.Size = new System.Drawing.Size(35, 26);
			this.butLeft.TabIndex = 160;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.but6am);
			this.groupBox2.Controls.Add(this.textAfterTimeOfDay);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.but5pm);
			this.groupBox2.Controls.Add(this.textBeforeTimeOfDay);
			this.groupBox2.Location = new System.Drawing.Point(33, 41);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(270, 75);
			this.groupBox2.TabIndex = 158;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "or Differential Hours";
			// 
			// FormTimeCardRuleEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(571, 651);
			this.Controls.Add(this.textOverHoursPerDay);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butRight);
			this.Controls.Add(this.butLeft);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.listEmployees);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormTimeCardRuleEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Time Card Rule Edit";
			this.Load += new System.EventHandler(this.FormTimeCardRuleEdit_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textOverHoursPerDay;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textAfterTimeOfDay;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.ListBox listEmployees;
		private OpenDental.UI.Button but5pm;
		private UI.Button but6am;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBeforeTimeOfDay;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label5;
		private UI.Button butRight;
		private UI.Button butLeft;
		private System.Windows.Forms.GroupBox groupBox2;
	}
}