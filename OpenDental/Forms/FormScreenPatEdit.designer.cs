namespace OpenDental{
	partial class FormScreenPatEdit {
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
			this.textPatient = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textScreenGroup = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textSheet = new System.Windows.Forms.TextBox();
			this.button1 = new OpenDental.UI.Button();
			this.butScreenGroupSelect = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butPatSelect = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textPatient
			// 
			this.textPatient.Location = new System.Drawing.Point(130, 28);
			this.textPatient.Name = "textPatient";
			this.textPatient.ReadOnly = true;
			this.textPatient.Size = new System.Drawing.Size(190, 20);
			this.textPatient.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(58, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 14);
			this.label1.TabIndex = 5;
			this.label1.Text = "Patient";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textScreenGroup
			// 
			this.textScreenGroup.Location = new System.Drawing.Point(130, 54);
			this.textScreenGroup.Name = "textScreenGroup";
			this.textScreenGroup.ReadOnly = true;
			this.textScreenGroup.Size = new System.Drawing.Size(190, 20);
			this.textScreenGroup.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(111, 14);
			this.label2.TabIndex = 5;
			this.label2.Text = "Screening Group";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(13, 83);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(111, 14);
			this.label3.TabIndex = 5;
			this.label3.Text = "Screening Sheet";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSheet
			// 
			this.textSheet.Location = new System.Drawing.Point(130, 80);
			this.textSheet.Name = "textSheet";
			this.textSheet.ReadOnly = true;
			this.textSheet.Size = new System.Drawing.Size(190, 20);
			this.textSheet.TabIndex = 4;
			// 
			// button1
			// 
			this.button1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button1.Autosize = true;
			this.button1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button1.CornerRadius = 4F;
			this.button1.Location = new System.Drawing.Point(326, 80);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(22, 20);
			this.button1.TabIndex = 2;
			this.button1.Text = "...";
			this.button1.Click += new System.EventHandler(this.butScreenGroupSelect_Click);
			// 
			// butScreenGroupSelect
			// 
			this.butScreenGroupSelect.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butScreenGroupSelect.Autosize = true;
			this.butScreenGroupSelect.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butScreenGroupSelect.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butScreenGroupSelect.CornerRadius = 4F;
			this.butScreenGroupSelect.Location = new System.Drawing.Point(326, 53);
			this.butScreenGroupSelect.Name = "butScreenGroupSelect";
			this.butScreenGroupSelect.Size = new System.Drawing.Size(22, 20);
			this.butScreenGroupSelect.TabIndex = 2;
			this.butScreenGroupSelect.Text = "...";
			this.butScreenGroupSelect.Click += new System.EventHandler(this.butScreenGroupSelect_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(233, 138);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butPatSelect
			// 
			this.butPatSelect.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPatSelect.Autosize = true;
			this.butPatSelect.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPatSelect.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPatSelect.CornerRadius = 4F;
			this.butPatSelect.Location = new System.Drawing.Point(326, 27);
			this.butPatSelect.Name = "butPatSelect";
			this.butPatSelect.Size = new System.Drawing.Size(22, 20);
			this.butPatSelect.TabIndex = 2;
			this.butPatSelect.Text = "...";
			this.butPatSelect.Click += new System.EventHandler(this.butPatSelect_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(323, 138);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormScreenPatEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(421, 183);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textSheet);
			this.Controls.Add(this.textScreenGroup);
			this.Controls.Add(this.textPatient);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.butScreenGroupSelect);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butPatSelect);
			this.Controls.Add(this.butCancel);
			this.Name = "FormScreenPatEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Screening Patient Edit";
			this.Load += new System.EventHandler(this.FormScreenPatEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textPatient;
		private System.Windows.Forms.Label label1;
		private UI.Button butPatSelect;
		private UI.Button butScreenGroupSelect;
		private System.Windows.Forms.TextBox textScreenGroup;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textSheet;
		private UI.Button button1;
	}
}