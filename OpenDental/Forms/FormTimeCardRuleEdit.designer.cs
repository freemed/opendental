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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.but5pm = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(14,108);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97,18);
			this.label1.TabIndex = 4;
			this.label1.Text = "Employee";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(23,19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112,18);
			this.label2.TabIndex = 6;
			this.label2.Text = "Over Hours Per Day";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textOverHoursPerDay
			// 
			this.textOverHoursPerDay.Location = new System.Drawing.Point(138,19);
			this.textOverHoursPerDay.Name = "textOverHoursPerDay";
			this.textOverHoursPerDay.Size = new System.Drawing.Size(62,20);
			this.textOverHoursPerDay.TabIndex = 7;
			this.textOverHoursPerDay.Text = "8:00";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24,50);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(111,18);
			this.label3.TabIndex = 8;
			this.label3.Text = "After Time of Day";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAfterTimeOfDay
			// 
			this.textAfterTimeOfDay.Location = new System.Drawing.Point(138,50);
			this.textAfterTimeOfDay.Name = "textAfterTimeOfDay";
			this.textAfterTimeOfDay.Size = new System.Drawing.Size(62,20);
			this.textAfterTimeOfDay.TabIndex = 9;
			this.textAfterTimeOfDay.Text = "16:00";
			// 
			// listEmployees
			// 
			this.listEmployees.FormattingEnabled = true;
			this.listEmployees.Location = new System.Drawing.Point(15,130);
			this.listEmployees.Name = "listEmployees";
			this.listEmployees.Size = new System.Drawing.Size(200,277);
			this.listEmployees.TabIndex = 156;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.but5pm);
			this.groupBox1.Controls.Add(this.textOverHoursPerDay);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textAfterTimeOfDay);
			this.groupBox1.Location = new System.Drawing.Point(15,12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(252,84);
			this.groupBox1.TabIndex = 157;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Overtime if";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(273,260);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,24);
			this.butDelete.TabIndex = 155;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(273,351);
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
			this.butCancel.Location = new System.Drawing.Point(273,383);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// but5pm
			// 
			this.but5pm.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but5pm.Autosize = true;
			this.but5pm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but5pm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but5pm.CornerRadius = 4F;
			this.but5pm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.but5pm.Location = new System.Drawing.Point(206,49);
			this.but5pm.Name = "but5pm";
			this.but5pm.Size = new System.Drawing.Size(35,22);
			this.but5pm.TabIndex = 158;
			this.but5pm.Text = "5 PM";
			this.but5pm.Click += new System.EventHandler(this.but5pm_Click);
			// 
			// FormTimeCardRuleEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(373,419);
			this.Controls.Add(this.groupBox1);
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
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

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
		private System.Windows.Forms.GroupBox groupBox1;
		private OpenDental.UI.Button but5pm;
	}
}