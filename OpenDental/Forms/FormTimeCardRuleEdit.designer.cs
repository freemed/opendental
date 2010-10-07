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
			this.comboEmployee = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textOverHoursPerDay = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textAfterTimeOfDay = new System.Windows.Forms.TextBox();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(203,18);
			this.label1.TabIndex = 4;
			this.label1.Text = "Employee";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboEmployee
			// 
			this.comboEmployee.FormattingEnabled = true;
			this.comboEmployee.Location = new System.Drawing.Point(15,30);
			this.comboEmployee.Name = "comboEmployee";
			this.comboEmployee.Size = new System.Drawing.Size(200,21);
			this.comboEmployee.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13,54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(203,18);
			this.label2.TabIndex = 6;
			this.label2.Text = "Over Hours Per Day";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textOverHoursPerDay
			// 
			this.textOverHoursPerDay.Location = new System.Drawing.Point(15,75);
			this.textOverHoursPerDay.Name = "textOverHoursPerDay";
			this.textOverHoursPerDay.Size = new System.Drawing.Size(141,20);
			this.textOverHoursPerDay.TabIndex = 7;
			this.textOverHoursPerDay.Text = "8";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(14,98);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(203,18);
			this.label3.TabIndex = 8;
			this.label3.Text = "After Time of Day (Military Time)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textAfterTimeOfDay
			// 
			this.textAfterTimeOfDay.Location = new System.Drawing.Point(15,119);
			this.textAfterTimeOfDay.Name = "textAfterTimeOfDay";
			this.textAfterTimeOfDay.Size = new System.Drawing.Size(141,20);
			this.textAfterTimeOfDay.TabIndex = 9;
			this.textAfterTimeOfDay.Text = "16:00";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(15,206);
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
			this.butOK.Location = new System.Drawing.Point(211,165);
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
			this.butCancel.Location = new System.Drawing.Point(211,206);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormTimeCardRuleEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(311,257);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textAfterTimeOfDay);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textOverHoursPerDay);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboEmployee);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormTimeCardRuleEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Time Card Rule Edit";
			this.Load += new System.EventHandler(this.FormTimeCardRuleEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboEmployee;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textOverHoursPerDay;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textAfterTimeOfDay;
		private OpenDental.UI.Button butDelete;
	}
}